using Chess.Domain.Game;
using Chess.Domain.Pieces;

using System.Windows;

namespace Chess.Desktop
{
    /// <summary>
    /// Interaction logic for PromotionSelection.xaml
    /// </summary>
    public partial class PromotionSelection : Window
    {
        #region Public Constructors

        public PromotionSelection(bool isWhite)
        {
            InitializeComponent();

            var margin = new Thickness(10, 0, 10, 0);

            var queen = new BoardPiece(new Queen { IsWhite = isWhite }, true) { Margin = margin };
            queen.MouseUp += (s, e) => HandleSelection(s);
            Pieces.Children.Add(queen);

            var rook = new BoardPiece(new Rook { IsWhite = isWhite }, true) { Margin = margin };
            rook.MouseUp += (s, e) => HandleSelection(s);
            Pieces.Children.Add(rook);

            var knight = new BoardPiece(new Knight { IsWhite = isWhite }, true) { Margin = margin };
            knight.MouseUp += (s, e) => HandleSelection(s);
            Pieces.Children.Add(knight);

            var bishop = new BoardPiece(new Bishop { IsWhite = isWhite }, true) { Margin = margin };
            bishop.MouseUp += (s, e) => HandleSelection(s);
            Pieces.Children.Add(bishop);
        }

        #endregion Public Constructors

        #region Public Properties

        public Promote Promotion { get; private set; }

        #endregion Public Properties

        #region Private Methods

        private void HandleSelection(object s)
        {
            if (s is not BoardPiece piece)
            {
                Close();
                return;
            }

            switch (piece.Piece)
            {
                case Queen:
                    Promotion = Promote.Queen;
                    break;

                case Knight:
                    Promotion = Promote.Knight;
                    break;

                case Bishop:
                    Promotion = Promote.Bishop;
                    break;

                case Rook:
                    Promotion = Promote.Rook;
                    break;
            }

            if (MessageBox.Show($"Are you sure to promote the pawn to: {Promotion}?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        #endregion Private Methods
    }
}