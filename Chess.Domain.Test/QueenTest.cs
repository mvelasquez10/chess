using Chess.Domain.Pieces;

using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace Chess.Domain.Test
{
    public class QueenTest
    {
        #region Public Methods

        [Fact]
        public void Queen_DefaultColor_Black()
        {
            var queen = new Queen();

            Assert.False(queen.IsWhite);
        }

        [Fact]
        public void Queen_Movement_BlockSameColor()
        {
            var queen = new Queen { Position = new(4, 5) };

            var extraPieces = new List<Piece>
            {
                new Queen { Position = new(4,8)},
                new Queen { Position = new(4,1)},
                new Queen { Position = new(8,5)},
                new Queen { Position = new(1,5)},
                new Queen { Position = new(7,8)},
                new Queen { Position = new(1,8)},
                new Queen { Position = new(8,1)},
                new Queen { Position = new(1,2)},
            };

            var expectedPositions = new List<Position>
            {
                new(2,3),
                new(2,5),
                new(2,7),
                new(3,4),
                new(3,5),
                new(3,6),
                new(4,2),
                new(4,3),
                new(4,4),
                new(4,6),
                new(4,7),
                new(5,4),
                new(5,5),
                new(5,6),
                new(6,3),
                new(6,5),
                new(6,7),
                new(7,2),
                new(7,5),
            };

            Assert.True(Enumerable.SequenceEqual(expectedPositions, queen.GetMoves(extraPieces)));
        }

        [Fact]
        public void Queen_Movement_CaptureColor()
        {
            var queen = new Queen { Position = new(4, 5) };

            var extraPieces = new List<Piece>
            {
                new Queen { IsWhite = true, Position = new(4,8)},
                new Queen { IsWhite = true, Position = new(4,1)},
                new Queen { IsWhite = true, Position = new(8,5)},
                new Queen { IsWhite = true, Position = new(1,5)},
                new Queen { IsWhite = true, Position = new(4,8)},
                new Queen { IsWhite = true, Position = new(4,1)},
                new Queen { IsWhite = true, Position = new(8,5)},
                new Queen { IsWhite = true, Position = new(1,5)},
            };

            var expectedPositions = new List<Position>
            {
                new(1,2),
                new(1,5),
                new(1,8),
                new(2,3),
                new(2,5),
                new(2,7),
                new(3,4),
                new(3,5),
                new(3,6),
                new(4,1),
                new(4,2),
                new(4,3),
                new(4,4),
                new(4,6),
                new(4,7),
                new(4,8),
                new(5,4),
                new(5,5),
                new(5,6),
                new(6,3),
                new(6,5),
                new(6,7),
                new(7,2),
                new(7,5),
                new(7,8),
                new(8,1),
                new(8,5),
            };

            Assert.True(Enumerable.SequenceEqual(expectedPositions, queen.GetMoves(extraPieces)));
        }

        [Fact]
        public void Queen_Movement_CrossAndDiagonal()
        {
            var queen = new Queen { Position = new(4, 5) };
            var expectedPositions = new List<Position>
            {
                new(1,2),
                new(1,5),
                new(1,8),
                new(2,3),
                new(2,5),
                new(2,7),
                new(3,4),
                new(3,5),
                new(3,6),
                new(4,1),
                new(4,2),
                new(4,3),
                new(4,4),
                new(4,6),
                new(4,7),
                new(4,8),
                new(5,4),
                new(5,5),
                new(5,6),
                new(6,3),
                new(6,5),
                new(6,7),
                new(7,2),
                new(7,5),
                new(7,8),
                new(8,1),
                new(8,5),
            };

            Assert.True(Enumerable.SequenceEqual(expectedPositions, queen.GetMoves()));
        }

        [Fact]
        public void Queen_Simbol_Q()
        {
            var queen = new Queen();

            Assert.Equal('Q', queen.Simbol);
        }

        [Fact]
        public void Queen_ToString_Simbol()
        {
            var queen = new Queen();

            Assert.Equal("Q", queen.ToString());
        }

        #endregion Public Methods
    }
}