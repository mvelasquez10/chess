using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Chess.Desktop
{
    /// <summary>
    /// Interaction logic for Player.xaml
    /// </summary>
    public partial class Player : UserControl
    {
        #region Public Constructors

        public Player(bool isWhite)
        {
            IsWhite = isWhite;
            InitializeComponent();

            PLayerLogo.Source = new BitmapImage(new Uri($"pack://application:,,,/Resources/K{(IsWhite ? "W" : "B")}.png"));

            PlayerDraw.Checked += PlayerDraw_Checked;
            PlayerDraw.Unchecked += PlayerDraw_Checked;

            Reset();
        }

        #endregion Public Constructors

        #region Public Events

        public Action? OnDrawChanged;

        #endregion Public Events

        #region Public Enums

        public enum PlayerText
        {
            TURN,

            NONE,

            WINS,

            DRAW
        }

        #endregion Public Enums

        #region Public Properties

        public bool IsCheck { get => PlayerCheck.Visibility == Visibility.Visible; }
        public bool IsDrawing { get => PlayerDraw.IsChecked ?? false; }
        public bool IsWhite { get; init; }

        #endregion Public Properties

        #region Public Methods

        public void Reset()
        {
            PlayerStatus.Text = IsWhite ? PlayerText.TURN.ToString() : string.Empty;
            PLayerCaptures.Children.Clear();
            PlayerDraw.IsChecked = false;
            IsEnabled = true;
            PlayerCheck.Visibility = Visibility.Hidden;
        }

        public void UpdateCapture(BoardPiece piece)
        {
            piece.Margin = new(2);
            PLayerCaptures.Children.Add(piece);
        }

        public void UpdateCheck(bool isCheck)
        {
            PlayerCheck.Visibility = isCheck ? Visibility.Visible : Visibility.Hidden;
        }

        public void UpdateText(PlayerText newText)
        {
            PlayerStatus.Text = newText == PlayerText.NONE ? string.Empty : newText.ToString();
        }

        #endregion Public Methods

        #region Private Methods

        internal void ResetDraw()
        {
            PlayerDraw.IsChecked = false;
        }

        private void PlayerDraw_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            OnDrawChanged?.Invoke();
        }

        #endregion Private Methods
    }
}