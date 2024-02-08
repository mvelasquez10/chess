using Chess.Domain.Pieces;

using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace Chess.Domain.Test
{
    public class KnightTest
    {
        #region Public Methods

        [Fact]
        public void Knight_DefaultColor_Black()
        {
            var knight = new Knight();

            Assert.False(knight.IsWhite);
        }

        [Fact]
        public void Knight_Movement_BlockSameColor()
        {
            var knight = new Knight { Position = new(4, 5) };

            var extraPieces = new List<Piece>
            {
                new Knight { Position = new(5,7)},
                new Knight { Position = new(3,7)},
                new Knight { Position = new(5,3)},
                new Knight { Position = new(3,3)},
                new Knight { Position = new(2,6)},
                new Knight { Position = new(2,4)},
                new Knight { Position = new(6,6)},
                new Knight { Position = new(6,4)},
            };

            Assert.Empty(knight.GetMoves(extraPieces));
        }

        [Fact]
        public void Knight_Movement_CaptureColor()
        {
            var knight = new Knight { Position = new(4, 5) };

            var extraPieces = new List<Piece>
            {
                new Knight { IsWhite = true, Position = new(5,7)},
                new Knight { IsWhite = true, Position = new(3,7)},
                new Knight { IsWhite = true, Position = new(5,3)},
                new Knight { IsWhite = true, Position = new(3,3)},
                new Knight { IsWhite = true, Position = new(2,6)},
                new Knight { IsWhite = true, Position = new(2,4)},
                new Knight { IsWhite = true, Position = new(6,6)},
                new Knight { IsWhite = true, Position = new(6,4)},
            };

            var expectedPositions = new List<Position>
            {
                new(2,4),
                new(2,6),
                new(3,3),
                new(3,7),
                new(5,3),
                new(5,7),
                new(6,4),
                new(6,6),
            };

            Assert.True(Enumerable.SequenceEqual(expectedPositions, knight.GetMoves(extraPieces)));
        }

        [Fact]
        public void Knight_Movement_LShape()
        {
            var knight = new Knight { Position = new(4, 5) };
            var expectedPositions = new List<Position>
            {
                new(2,4),
                new(2,6),
                new(3,3),
                new(3,7),
                new(5,3),
                new(5,7),
                new(6,4),
                new(6,6),
            };

            Assert.True(Enumerable.SequenceEqual(expectedPositions, knight.GetMoves()));
        }

        [Fact]
        public void Knight_Movement_NotAllValid()
        {
            var knight = new Knight { Position = new(1, 5) };
            var expectedPositions = new List<Position>
            {
                new(2,3),
                new(2,7),
                new(3,4),
                new(3,6),
            };

            Assert.True(Enumerable.SequenceEqual(expectedPositions, knight.GetMoves()));
        }

        [Fact]
        public void Knight_Simbol_K()
        {
            var knight = new Knight();

            Assert.Equal('N', knight.Simbol);
        }

        [Fact]
        public void Knight_ToString_Simbol()
        {
            var knight = new Knight();

            Assert.Equal("N", knight.ToString());
        }

        #endregion Public Methods
    }
}