using Chess.Domain;
using Chess.Domain.Game;
using Chess.Domain.Game.GameEventArgs;
using Chess.Domain.Pieces;
using Chess.Engine;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Chess.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Fields

        private readonly ICollection<BoardPiece> _currentBoardPieces = new HashSet<BoardPiece>();

        private readonly GameEngine _engine = new();

        private readonly Player _playerBlack = new(false);

        private readonly Player _playerWhite = new(true);

        #endregion Private Fields

        #region Public Constructors

        public MainWindow()
        {
            InitializeComponent();

            Board.OnSquareClick += (s, e) => HandleBoardClick(e);

            LoadPlayers();

            LoadEngineHandlers();

            LoadBoardPieces();

            ResetPlayers();
        }

        #endregion Public Constructors

        #region Private Methods

        private void ClearHighlightMoves()
        {
            BoardHighlight.Children.Clear();
            var selected = _currentBoardPieces.FirstOrDefault(p => p.IsSelected);
            if (selected is not null)
            {
                selected.IsSelected = false;
            }
        }

        private void DrawHighlightMoves(IReadOnlyCollection<Position> positions)
        {
            BoardHighlight.Children.Clear();
            foreach (var position in positions)
            {
                var highlightRec = new Rectangle
                {
                    Height = 40,
                    Width = 40,
                    Fill = Brushes.Green,
                    Opacity = 0.3,
                    IsHitTestVisible = false
                };
                SetBoardPosition(position, highlightRec);
                BoardHighlight.Children.Add(highlightRec);
            }
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void FinishInDraw()
        {
            _engine.FinishInDraw();
        }

        private void HandleBoardClick(Position e)
        {
            if (_engine.MoveCurrentPiece(e))
            {
                return;
            }

            _engine.SelectCurrentPiece(e);
        }

        private void HandleCheck(CheckEventArgs e)
        {
            if (e.CurrentTurn == Turn.White)
            {
                _playerBlack.UpdateCheck(true);
            }
            else
            {
                _playerWhite.UpdateCheck(true);
            }
        }

        private Action? HandleCheckDraw() => () =>
                 {
                     if (!_playerBlack.IsDrawing || !_playerWhite.IsDrawing)
                     {
                         return;
                     }

                     if (MessageBox.Show("Both players requested a draw, are you sure to proceed?", "Draw was requested", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                     {
                         _engine.FinishInDraw();
                     }
                     else
                     {
                         _playerBlack.ResetDraw();
                         _playerWhite.ResetDraw();
                     }
                 };

        private void HandleCurrentPieceChanged(CurrentPieceEventArgs e)
        {
            var _currentBoardPiece = _currentBoardPieces.FirstOrDefault(p => p.IsSelected);
            if (_currentBoardPiece is not null)
            {
                _currentBoardPiece.IsSelected = false;
            }

            DrawHighlightMoves(e.CurrentPiece?.Positions ?? new List<Position>());

            _currentBoardPiece = _currentBoardPieces.FirstOrDefault(p => p.Piece == e.CurrentPiece?.Piece);
            if (_currentBoardPiece is not null)
            {
                _currentBoardPiece.IsSelected = true;
            }
        }

        private void HandleGameOver(GameOverEventArgs e)
        {
            switch (e.Status)
            {
                case GameStatus.BlackWins:
                    _playerWhite.UpdateText(Player.PlayerText.NONE);
                    _playerBlack.UpdateText(Player.PlayerText.WINS);
                    break;

                case GameStatus.WhiteWins:
                    _playerWhite.UpdateText(Player.PlayerText.WINS);
                    _playerBlack.UpdateText(Player.PlayerText.NONE);
                    break;

                case GameStatus.Draw:
                    _playerWhite.UpdateText(Player.PlayerText.DRAW);
                    _playerBlack.UpdateText(Player.PlayerText.DRAW);
                    break;
            }
            _playerBlack.IsEnabled = false;
            _playerWhite.IsEnabled = false;
        }

        private void HandlePieceMoved(PieceMoveEventArgs e)
        {
            if (e.PieceCaptured is not null)
            {
                var currentCaptureBoardPiece = _currentBoardPieces.First(p => p.Piece.Position == e.PieceCaptured.Position);
                _currentBoardPieces.Remove(currentCaptureBoardPiece);
                BoardPieces.Children.Remove(currentCaptureBoardPiece);
                if (e.PieceCaptured.IsWhite)
                {
                    _playerBlack.UpdateCapture(currentCaptureBoardPiece);
                }
                else
                {
                    _playerWhite.UpdateCapture(currentCaptureBoardPiece);
                }
            }

            foreach (var pieceMoved in e.PiecesMoved)
            {
                var currentBoardPiece = _currentBoardPieces.First(p => p.Piece == pieceMoved.OldPiece);
                var newBoardPiece = new BoardPiece(pieceMoved.NewPiece);
                _currentBoardPieces.Remove(currentBoardPiece);
                _currentBoardPieces.Add(newBoardPiece);

                SetBoardPosition(pieceMoved.NewPiece.Position, newBoardPiece);
                BoardPieces.Children.Remove(currentBoardPiece);
                BoardPieces.Children.Add(newBoardPiece);
            }

            ClearHighlightMoves();
        }

        private void HandlePromotion(PieceEventArgs e)
        {
            var promotionSelection = new PromotionSelection(e.Piece.IsWhite);
            promotionSelection.ShowDialog();
            if (e.Piece is Pawn pawn)
            {
                _engine.PerformPromotion(pawn, promotionSelection.Promotion);
            }
        }

        private void HandleTurnChanged(CurrentTurnEventArgs e)
        {
            _playerWhite.UpdateText(e.CurrentTurn == Turn.White ? Player.PlayerText.TURN : Player.PlayerText.NONE);
            if (_playerWhite.IsCheck && e.CurrentTurn != Turn.White)
            {
                _playerWhite.UpdateCheck(false);
            }
            _playerBlack.UpdateText(e.CurrentTurn != Turn.White ? Player.PlayerText.TURN : Player.PlayerText.NONE);
            if (_playerBlack.IsCheck && e.CurrentTurn != Turn.Black)
            {
                _playerBlack.UpdateCheck(false);
            }
        }

        private void LoadBoardPieces()
        {
            BoardPieces.Children.Clear();
            _currentBoardPieces.Clear();
            foreach (var piece in _engine.Pieces)
            {
                var boardPiece = new BoardPiece(piece);
                SetBoardPosition(piece.Position, boardPiece);
                BoardPieces.Children.Add(boardPiece);
                _currentBoardPieces.Add(boardPiece);
            }
            ClearHighlightMoves();
        }

        private void LoadEngineHandlers()
        {
            _engine.OnCheck += (s, e) => HandleCheck(e);

            _engine.OnCheckmate += (s, e) => HandleCheck(e);

            _engine.OnGameOver += (s, e) => HandleGameOver(e);

            _engine.OnCurrentPieceChanged += (s, e) => HandleCurrentPieceChanged(e);

            _engine.OnPieceMoved += (s, e) => HandlePieceMoved(e);

            _engine.OnPromotion += (s, e) => HandlePromotion(e);

            _engine.OnTurnChanged += (s, e) => HandleTurnChanged(e);
        }

        private void LoadPlayers()
        {
            var margin = new Thickness(0, 10, 0, 0);

            Grid.SetColumn(_playerWhite, 0);
            _playerWhite.Margin = margin;
            MainGrid.Children.Add(_playerWhite);
            _playerWhite.OnDrawChanged += HandleCheckDraw();

            Grid.SetColumn(_playerBlack, 2);
            _playerBlack.Margin = margin;
            MainGrid.Children.Add(_playerBlack);
            _playerBlack.OnDrawChanged += HandleCheckDraw();
        }

        private void NewClick(object sender, RoutedEventArgs e)
        {
            _engine.NewGame();
            LoadBoardPieces();
            ResetPlayers();
        }

        private void OpenClick(object sender, RoutedEventArgs e)
        {
        }

        private void ResetPlayers()
        {
            _playerBlack.Reset();
            _playerWhite.Reset();
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
        }

        private void SetBoardPosition(Position position, UIElement boardPiece)
        {
            var boardRect = Board.BoardRects.First(r => r.Tag as Position == position);
            Canvas.SetLeft(boardPiece, Canvas.GetLeft(boardRect) + 5);
            Canvas.SetTop(boardPiece, Canvas.GetTop(boardRect) + 5);
        }

        #endregion Private Methods
    }
}