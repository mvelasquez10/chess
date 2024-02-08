using Chess.Domain.Pieces;
using Chess.Domain.Rules;

using Xunit;

namespace Chess.Domain.Test
{
    public class PromotionTest
    {
        #region Public Methods

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Promote_LastRow_True(bool isWhite)
        {
            var piece = new Pawn { IsWhite = isWhite, Position = new(2, (short)(isWhite ? 8 : 1)) };
            var promotionRule = new PromotionRule();

            Assert.True(promotionRule.Evaluate(piece));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Promote_NotLastRow_False(bool isWhite)
        {
            var piece = new Pawn { IsWhite = isWhite, Position = new(2, (short)(isWhite ? 7 : 2)) };
            var promotionRule = new PromotionRule();

            Assert.False(promotionRule.Evaluate(piece));
        }

        [Fact]
        public void Promote_Only_Pawn()
        {
            var piece = new Rook { Position = new(1, 1) };
            var promotionRule = new PromotionRule();

            Assert.False(promotionRule.Evaluate(piece));
        }

        #endregion Public Methods
    }
}