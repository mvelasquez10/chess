using Chess.Domain.Pieces;

using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace Chess.Domain.Test
{
    public class BishopTest
    {
        #region Public Methods

        [Fact]
        public void Bishop_DefaultColor_Black()
        {
            var bishop = new Bishop();

            Assert.False(bishop.IsWhite);
        }

        [Fact]
        public void Bishop_Movement_BlockSameColor()
        {
            var bishop = new Bishop { Position = new(4, 5) };

            var extraPieces = new List<Piece>
            {
                new Bishop { Position = new(7,8)},
                new Bishop { Position = new(1,8)},
                new Bishop { Position = new(8,1)},
                new Bishop { Position = new(1,2)},
            };

            var expectedPositions = new List<Position>
            {
                new(2,3),
                new(2,7),
                new(3,4),
                new(3,6),
                new(5,4),
                new(5,6),
                new(6,3),
                new(6,7),
                new(7,2),
            };

            Assert.True(Enumerable.SequenceEqual(expectedPositions, bishop.GetMoves(extraPieces)));
        }

        [Fact]
        public void Bishop_Movement_CaptureColor()
        {
            var bishop = new Bishop { Position = new(4, 5) };

            var extraPieces = new List<Piece>
            {
                new Bishop { IsWhite = true, Position = new(4,8)},
                new Bishop { IsWhite = true, Position = new(4,1)},
                new Bishop { IsWhite = true, Position = new(8,5)},
                new Bishop { IsWhite = true, Position = new(1,5)},
            };

            var expectedPositions = new List<Position>
            {
                new(1,2),
                new(1,8),
                new(2,3),
                new(2,7),
                new(3,4),
                new(3,6),
                new(5,4),
                new(5,6),
                new(6,3),
                new(6,7),
                new(7,2),
                new(7,8),
                new(8,1),
            };

            Assert.True(Enumerable.SequenceEqual(expectedPositions, bishop.GetMoves(extraPieces)));
        }

        [Fact]
        public void Bishop_Movement_Diagonal()
        {
            var bishop = new Bishop { Position = new(4, 5) };
            var expectedPositions = new List<Position>
            {
                new(1,2),
                new(1,8),
                new(2,3),
                new(2,7),
                new(3,4),
                new(3,6),
                new(5,4),
                new(5,6),
                new(6,3),
                new(6,7),
                new(7,2),
                new(7,8),
                new(8,1),
            };

            Assert.True(Enumerable.SequenceEqual(expectedPositions, bishop.GetMoves()));
        }

        [Fact]
        public void Bishop_Simbol_B()
        {
            var bishop = new Bishop();

            Assert.Equal('B', bishop.Simbol);
        }

        [Fact]
        public void Bishop_ToString_Simbol()
        {
            var bishop = new Bishop();

            Assert.Equal("B", bishop.ToString());
        }

        #endregion Public Methods
    }
}