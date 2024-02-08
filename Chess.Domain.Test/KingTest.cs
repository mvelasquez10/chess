using Chess.Domain.Pieces;

using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace Chess.Domain.Test
{
    public class KingTest
    {
        #region Public Methods

        [Fact]
        public void King_DefaultColor_Black()
        {
            var king = new King();

            Assert.False(king.IsWhite);
        }

        [Fact]
        public void King_Movement_BlockSameColor()
        {
            var king = new King { Position = new(4, 5) };

            var extraPieces = new List<Piece>
            {
                new King { Position = new(4,6)},
                new King { Position = new(4,4)},
                new King { Position = new(5,5)},
                new King { Position = new(3,5)},
                new King { Position = new(5,6)},
                new King { Position = new(3,6)},
                new King { Position = new(5,4)},
                new King { Position = new(3,4)},
            };

            Assert.Empty(king.GetMoves(extraPieces));
        }

        [Fact]
        public void King_Movement_CaptureColor()
        {
            var king = new King { Position = new(4, 5) };

            var extraPieces = new List<Piece>
            {
                new King { IsWhite = true, Position = new(3,4)},
                new King { IsWhite = true, Position = new(3,5)},
                new King { IsWhite = true, Position = new(3,6)},
                new King { IsWhite = true, Position = new(5,4)},
                new King { IsWhite = true, Position = new(5,5)},
                new King { IsWhite = true, Position = new(5,6)},
                new King { IsWhite = true, Position = new(4,4)},
                new King { IsWhite = true, Position = new(4,6)},
            };

            var expectedPositions = new List<Position>
            {
                new(3,4),
                new(3,5),
                new(3,6),
                new(4,4),
                new(4,6),
                new(5,4),
                new(5,5),
                new(5,6),
            };

            Assert.True(Enumerable.SequenceEqual(expectedPositions, king.GetMoves(extraPieces)));
        }

        [Fact]
        public void King_Movement_CrossAndDiagonal()
        {
            var king = new King { Position = new(4, 5) };
            var expectedPositions = new List<Position>
            {
                new(3,4),
                new(3,5),
                new(3,6),
                new(4,4),
                new(4,6),
                new(5,4),
                new(5,5),
                new(5,6),
            };

            Assert.True(Enumerable.SequenceEqual(expectedPositions, king.GetMoves()));
        }

        [Fact]
        public void King_Simbol_Q()
        {
            var king = new King();

            Assert.Equal('K', king.Simbol);
        }

        [Fact]
        public void King_ToString_Simbol()
        {
            var king = new King();

            Assert.Equal("K", king.ToString());
        }

        #endregion Public Methods
    }
}