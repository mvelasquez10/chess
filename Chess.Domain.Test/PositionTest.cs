using Xunit;

namespace Chess.Domain.Test
{
    public class PositionTest
    {
        #region Public Methods

        [Fact]
        public void Position_CompareToNull_0()
        {
            var position = new Position(1, 1);

            Assert.Equal(0, position.CompareTo(null));
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(9, 8)]
        [InlineData(8, 9)]
        public void Position_MustBe_Valid(short x, short y)
        {
            var position1 = new Position(1, 1);
            var position2 = position1 with { X = x, Y = y };

            Assert.False(position2.IsValid());
        }

        [Theory]
        [InlineData(1, 1, "a1")]
        [InlineData(8, 8, "h8")]
        [InlineData(5, 5, "e5")]
        public void ToString_right_format(short x, short y, string expected)
        {
            var position = new Position(x, y);

            Assert.Equal(expected, position.ToString());
        }

        #endregion Public Methods
    }
}