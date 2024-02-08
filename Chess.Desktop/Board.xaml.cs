using Chess.Domain;

using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Chess.Desktop
{
    /// <summary>
    /// Interaction logic for Board.xaml
    /// </summary>
    public partial class Board : UserControl
    {
        #region Public Constructors

        public Board()
        {
            InitializeComponent();

            var background = new Rectangle
            {
                Width = 400,
                Height = 400,
                Fill = Brushes.White,
                Stroke = Brushes.Gray,
            };

            Canvas.SetLeft(background, 20);
            Canvas.SetTop(background, 20);

            BoardCanvas.Children.Add(background);

            for (short i = 0; i < 8; i++)
            {
                for (short j = 0; j < 8; j++)
                {
                    var square = new Rectangle
                    {
                        Width = 50,
                        Height = 50,
                        Fill = ((i + j) % 2 == 0) ? Brushes.White : Brushes.Bisque,
                        Stroke = Brushes.Gray,
                        Tag = new Position((short)(j + 1), (short)(8 - i)),
                        Name = (Tag as Position)?.ToString() ?? "",
                    };

                    square.MouseUp += (s, e) =>
                    {
                        OnSquareClick?.Invoke(s, (s as Shape)?.Tag as Position ?? new Position(0, 0));
                    };

                    Canvas.SetLeft(square, 20 + (50 * j));
                    Canvas.SetTop(square, 20 + (50 * i));

                    BoardCanvas.Children.Add(square);
                    BoardRects.Add(square);
                }
            }
        }

        #endregion Public Constructors

        #region Public Delegates

        public delegate void SquareClick(object sender, Position position);

        #endregion Public Delegates

        #region Public Events

        public event SquareClick? OnSquareClick;

        #endregion Public Events

        #region Public Properties

        public ICollection<Rectangle> BoardRects { get; internal set; } = new HashSet<Rectangle>();

        #endregion Public Properties
    }
}