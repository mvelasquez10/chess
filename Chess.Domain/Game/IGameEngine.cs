using Chess.Domain.Game.GameEventArgs;
using Chess.Domain.Pieces;

namespace Chess.Domain.Game
{
    public interface IGameEngine
    {
        #region Public Events

        event EventHandler<CheckEventArgs>? OnCheck;

        event EventHandler<CheckEventArgs>? OnCheckmate;

        event EventHandler<CurrentPieceEventArgs>? OnCurrentPieceChanged;

        event EventHandler<GameOverEventArgs>? OnGameOver;

        event EventHandler<PieceMoveEventArgs>? OnPieceMoved;

        event EventHandler<PieceEventArgs>? OnPromotion;

        event EventHandler<CurrentTurnEventArgs>? OnTurnChanged;

        #endregion Public Events

        #region Public Properties

        CurrentPiece? CurrentPiece { get; }
        Turn CurrentTurn { get; }

        IReadOnlyCollection<Piece> Pieces { get; }

        #endregion Public Properties

        #region Public Methods

        void FinishInDraw();

        bool MoveCurrentPiece(Position e);

        void NewGame(IGame? game = null);

        void PerformPromotion(Pawn pawn, Promote promotion);

        void SelectCurrentPiece(Position position);

        #endregion Public Methods
    }
}