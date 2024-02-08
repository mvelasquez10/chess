using Chess.Domain.Pieces;
using Chess.Domain.Rules;

using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace Chess.Domain.Test
{
    public class ValidMoveRuleTest
    {
        #region Public Methods

        [Fact]
        public void King_Can_Escape()
        {
            var king = new King { Position = new(3, 1) };
            var enemyKing = new King { IsWhite = true, Position = new(3, 8) };
            var enemyRook = new Rook { IsWhite = true, Position = new(3, 4) };
            var validMoveRule = new ValidMoveRule();
            var expectedMoves = new List<Position>
            {
                new(2,1),
                new(2,2),
                new(4,1),
                new(4,2),
            };

            Assert.True(Enumerable.SequenceEqual(expectedMoves, validMoveRule.Evaluate(new(king, new List<Piece> { king, enemyKing, enemyRook }))));
        }

        [Fact]
        public void King_MustBe_Protected()
        {
            var king = new King { Position = new(3, 1) };
            var rook = new Rook { Position = new(3, 2) };
            var enemyKing = new King { IsWhite = true, Position = new(3, 8) };
            var enemyRook = new Rook { IsWhite = true, Position = new(3, 4) };
            var validMoveRule = new ValidMoveRule();
            var expectedMoves = new List<Position>
            {
                new(3,3),
                new(3,4)
            };

            Assert.True(Enumerable.SequenceEqual(expectedMoves, validMoveRule.Evaluate(new(rook, new List<Piece> { king, rook, enemyKing, enemyRook }))));
        }

        #endregion Public Methods
    }
}