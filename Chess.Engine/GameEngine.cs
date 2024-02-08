using Chess.Domain;
using Chess.Domain.Game;
using Chess.Domain.Game.GameEventArgs;
using Chess.Domain.Pieces;
using Chess.Domain.Rules;

using System.Collections.Immutable;

namespace Chess.Engine
{
    public sealed class GameEngine : IGameEngine
    {
        #region Private Fields

        private readonly CastlingRule _castlingRule = new();

        private readonly CheckRule _checkRule = new();

        private readonly PromotionRule _promotionRule = new();

        private readonly ValidMoveRule _validMoveRule = new();

        private CurrentPiece? _currentPiece = null;

        #endregion Private Fields

        #region Public Events

        public event EventHandler<CheckEventArgs>? OnCheck;

        public event EventHandler<CheckEventArgs>? OnCheckmate;

        public event EventHandler<CurrentPieceEventArgs>? OnCurrentPieceChanged;

        public event EventHandler<GameOverEventArgs>? OnGameOver;

        public event EventHandler<PieceMoveEventArgs>? OnPieceMoved;

        public event EventHandler<PieceEventArgs>? OnPromotion;

        public event EventHandler<CurrentTurnEventArgs>? OnTurnChanged;

        #endregion Public Events

        #region Public Properties

        public CurrentPiece? CurrentPiece { get => _currentPiece; }

        public Turn CurrentTurn => (CurrentGame.CurrentTurn % 2 != 0) ? Turn.White : Turn.Black;

        public IReadOnlyCollection<Piece> Pieces { get => CurrentGame.Pieces; }

        #endregion Public Properties

        #region Private Properties

        public IGame CurrentGame { get; internal set; } = new Game();

        #endregion Private Properties

        #region Public Methods

        public void FinishInDraw()
        {
            CurrentGame.Finish(null);
            OnGameOver?.Invoke(this, new GameOverEventArgs(CurrentGame.Status));
        }

        public bool MoveCurrentPiece(Position e)
        {
            if (CurrentGame.Status != GameStatus.Playing)
            {
                return false;
            }

            if (_currentPiece is null || !_currentPiece.Value.Positions.Contains(e))
            {
                return false;
            }

            var wasMove = PerformMovement(e);

            var wasCapture = CheckCapture(e, wasMove.First().NewPiece.IsWhite);

            OnPieceMoved?.Invoke(this, new(wasCapture, wasMove));

            var pieceWasMove = wasMove.First(m => !m.IsCastling).NewPiece;

            if (_promotionRule.Evaluate(pieceWasMove))
            {
                OnPromotion?.Invoke(this, new(pieceWasMove));
            }

            if (IsCheck(pieceWasMove))
            {
                if (IsCheckMate(pieceWasMove))
                {
                    OnCheckmate?.Invoke(this, new(CurrentTurn));
                    CurrentGame.Finish(CurrentTurn);
                    OnGameOver?.Invoke(this, new(CurrentGame.Status));
                    return true;
                }
                OnCheck?.Invoke(this, new(CurrentTurn));
            }

            var otherPieces = CurrentGame.Pieces.Where(p => p.IsWhite != wasMove.First().NewPiece.IsWhite);

            if (!otherPieces.Any(p => _validMoveRule.Evaluate(new(p, CurrentGame.Pieces)).Count > 0))
            {
                FinishInDraw();
                return true;
            }

            CurrentGame.CurrentTurn++;
            OnTurnChanged?.Invoke(this, new(CurrentTurn));
            return true;
        }

        public void NewGame(IGame? game = null)
        {
            CurrentGame = game ?? new Game();
        }

        public void PerformPromotion(Pawn pawn, Promote promotion)
        {
            Piece piece = pawn;

            switch (promotion)
            {
                case Promote.Queen:
                    piece = new Queen { Id = pawn.Id, IsWhite = pawn.IsWhite, LastPosition = pawn.LastPosition, Position = pawn.Position };
                    break;

                case Promote.Rook:
                    piece = new Rook { Id = pawn.Id, IsWhite = pawn.IsWhite, LastPosition = pawn.LastPosition, Position = pawn.Position };
                    break;

                case Promote.Knight:
                    piece = new Knight { Id = pawn.Id, IsWhite = pawn.IsWhite, LastPosition = pawn.LastPosition, Position = pawn.Position };
                    break;

                case Promote.Bishop:
                    piece = new Bishop { Id = pawn.Id, IsWhite = pawn.IsWhite, LastPosition = pawn.LastPosition, Position = pawn.Position };
                    break;
            }

            CurrentGame.UpdatePiece(pawn, piece);
            OnPieceMoved?.Invoke(this, new(null, new List<MoveStatus> { new(pawn, piece) }));
        }

        public void SelectCurrentPiece(Position position)
        {
            if (CurrentGame.Status != GameStatus.Playing)
            {
                return;
            }

            var piece = CurrentGame.Pieces
                .Where(p => !p.IsCaptured)
                .FirstOrDefault(p => p.IsWhite == (CurrentGame.CurrentTurn % 2 != 0) && p.Position == position);

            if (piece is null)
            {
                _currentPiece = null;
            }
            else
            {
                var validMoves = _validMoveRule.Evaluate(new(piece, CurrentGame.Pieces));

                if (piece is King king)
                {
                    validMoves = validMoves.Union(_castlingRule.Evaluate(new(king, CurrentGame.Pieces))).ToList();
                }

                _currentPiece = new(piece, validMoves);
            }

            OnCurrentPieceChanged?.Invoke(this, new(_currentPiece));
        }

        #endregion Public Methods

        #region Private Methods

        private Piece? CheckCapture(Position e, bool isWhite)
        {
            var capture = CurrentGame.Pieces.FirstOrDefault(p => p.Position == e && !p.IsCaptured && p.IsWhite != isWhite);
            if (capture is not null)
            {
                CurrentGame.UpdatePiece(capture, capture with { IsCaptured = true });
            }

            return capture;
        }

        private bool IsCheck(Piece wasMove)
        {
            var enemyKing = CurrentGame.Pieces.First(p => p.IsWhite != wasMove.IsWhite && p is King) as King;

            // Never is null on the current flow
#pragma warning disable CS8604 // Possible null reference argument.
            return _checkRule.Evaluate(new(enemyKing, enemyKing, CurrentGame.Pieces));
#pragma warning restore CS8604 // Possible null reference argument.
        }

        private bool IsCheckMate(Piece wasMove)
        {
            return !CurrentGame.Pieces.Where(p => p.IsWhite != wasMove.IsWhite && !p.IsCaptured && p is King).Any(p => _validMoveRule.Evaluate(new(p, CurrentGame.Pieces)).Count != 0);
        }

        private List<MoveStatus> PerformMovement(Position e)
        {
            var moveStatus = new List<MoveStatus>();

            // Never is null on the current flow
#pragma warning disable CS8629 // Nullable value type may be null.
            var oldPiece = _currentPiece.Value.Piece;
#pragma warning restore CS8629 // Nullable value type may be null.

            // Check castling
            if (oldPiece is King king && _castlingRule.Evaluate(new(king, CurrentGame.Pieces)).Contains(e))
            {
                var y = (short)(king.IsWhite ? 1 : 8);

                var oldRook = (e == new Position(3, y)) ?
                    CurrentGame.Pieces.First(p => p.Position == new Position(1, y))
                    : CurrentGame.Pieces.First(p => p.Position == new Position(8, y));

                var newRook = oldRook with { LastPosition = oldRook.Position, Position = new((short)(oldRook.Position.X == 1 ? 4 : 6), y) };
                CurrentGame.UpdatePiece(oldRook, newRook);

                moveStatus.Add(new(oldRook, newRook, true));
            }

            var newPiece = oldPiece with { LastPosition = oldPiece.Position, Position = e };
            CurrentGame.UpdatePiece(oldPiece, newPiece);

            moveStatus.Add(new(oldPiece, newPiece));

            _currentPiece = null;

            return moveStatus;
        }

        #endregion Private Methods
    }
}