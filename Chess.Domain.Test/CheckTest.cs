using Chess.Domain.Pieces;
using Chess.Domain.Rules;

using System.Collections.Generic;

using Xunit;

namespace Chess.Domain.Test
{
    public class CheckTest
    {
        #region Public Methods

        [Fact]
        public void Check_Enemy_CanCapture()
        {
            var king = new King { IsWhite = true, Position = new(1, 1) };
            var pawn = new Pawn { IsWhite = true, Position = new(8, 1) };
            var queenEnemy = new Queen { Position = new(1, 8) };
            var checkRule = new CheckRule();

            Assert.True(checkRule.Evaluate(new(king, pawn, new List<Piece> { queenEnemy })));
        }

        [Fact]
        public void Check_Enemy_CanNotCapture()
        {
            var king = new King { IsWhite = true, Position = new(1, 1) };
            var pawn = new Pawn { IsWhite = true, Position = new(8, 1) };
            var queenEnemy = new Queen { Position = new(2, 8) };
            var checkRule = new CheckRule();

            Assert.False(checkRule.Evaluate(new(king, pawn, new List<Piece> { queenEnemy })));
        }

        #endregion Public Methods
    }
}