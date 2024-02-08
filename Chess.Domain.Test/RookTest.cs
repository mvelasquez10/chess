using Chess.Domain.Pieces;

using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace Chess.Domain.Test
{
    public class RookTest
    {
        #region Public Methods

        [Fact]
        public void Rook_DefaultColor_Black()
        {
            var rook = new Rook();

            Assert.False(rook.IsWhite);
        }

        [Fact]
        public void Rook_Movement_BlockSameColor()
        {
            var rook = new Rook { Position = new(4, 5) };

            var extraPieces = new List<Piece>
            {
                new Rook { Position = new(4,8)},
                new Rook { Position = new(4,1)},
                new Rook { Position = new(8,5)},
                new Rook { Position = new(1,5)},
            };

            var expectedPositions = new List<Position>
            {
                new(2,5),
                new(3,5),
                new(4,2),
                new(4,3),
                new(4,4),
                new(4,6),
                new(4,7),
                new(5,5),
                new(6,5),
                new(7,5),
            };

            Assert.True(Enumerable.SequenceEqual(expectedPositions, rook.GetMoves(extraPieces)));
        }

        [Fact]
        public void Rook_Movement_CaptureColor()
        {
            var rook = new Rook { Position = new(4, 5) };

            var extraPieces = new List<Piece>
            {
                new Rook { IsWhite = true, Position = new(4,8)},
                new Rook { IsWhite = true, Position = new(4,1)},
                new Rook { IsWhite = true, Position = new(8,5)},
                new Rook { IsWhite = true, Position = new(1,5)},
            };

            var expectedPositions = new List<Position>
            {
                new(1,5),
                new(2,5),
                new(3,5),
                new(4,1),
                new(4,2),
                new(4,3),
                new(4,4),
                new(4,6),
                new(4,7),
                new(4,8),
                new(5,5),
                new(6,5),
                new(7,5),
                new(8,5),
            };

            Assert.True(Enumerable.SequenceEqual(expectedPositions, rook.GetMoves(extraPieces)));
        }

        [Fact]
        public void Rook_Movement_Cross()
        {
            var rook = new Rook { Position = new(4, 5) };
            var expectedPositions = new List<Position>
            {
                new(1,5),
                new(2,5),
                new(3,5),
                new(4,1),
                new(4,2),
                new(4,3),
                new(4,4),
                new(4,6),
                new(4,7),
                new(4,8),
                new(5,5),
                new(6,5),
                new(7,5),
                new(8,5),
            };

            Assert.True(Enumerable.SequenceEqual(expectedPositions, rook.GetMoves()));
        }

        [Fact]
        public void Rook_Simbol_R()
        {
            var rook = new Rook();

            Assert.Equal('R', rook.Simbol);
        }

        [Fact]
        public void Rook_ToString_Simbol()
        {
            var rook = new Rook();

            Assert.Equal("R", rook.ToString());
        }

        #endregion Public Methods
    }
}