using Chess.Domain;

using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Chess.Desktop
{
    /// <summary>
    /// Interaction logic for Piece.xaml
    /// </summary>
    public partial class BoardPiece : UserControl
    {
        #region Private Fields

        private bool _isSelected;

        #endregion Private Fields

        #region Public Constructors

        public BoardPiece(Piece piece, bool isHitTestVisible = false)
        {
            InitializeComponent();
            Piece = piece;
            Rectangle.IsHitTestVisible = isHitTestVisible;
            Rectangle.StrokeThickness = 2;
            Rectangle.Fill = new ImageBrush(new BitmapImage(new Uri($"pack://application:,,,/Resources/{piece.Simbol}{(piece.IsWhite ? "W" : "B")}.png")));
        }

        #endregion Public Constructors

        #region Public Properties

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                Rectangle.Stroke = _isSelected ? Brushes.SteelBlue : null;
            }
        }

        public Piece Piece { get; init; }

        #endregion Public Properties
    }
}