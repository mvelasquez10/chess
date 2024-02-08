using Chess.Domain.Pieces;

using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace Chess.Domain.Test
{
    public class PawnTest
    {
        #region Public Methods

        [Fact]
        public void Pawn_DefaultColor_Black()
        {
            var pawn = new Pawn();

            Assert.False(pawn.IsWhite);
        }

        [Theory]
        [MemberData(nameof(GetExpectedTwoCapture))]
        public void Pawn_Front_IsBlock_CanCapture(bool isWhite, ICollection<Position> expectedMoves)
        {
            var pawn = new Pawn { IsWhite = isWhite, Position = new Position(2, 4) };

            Assert.True(Enumerable.SequenceEqual(expectedMoves, pawn.GetMoves(new List<Piece>
            {
                new Pawn { IsWhite = !isWhite, Position = new Position(2, (short)(isWhite ? 5 : 3)) },
                new Pawn { IsWhite = !isWhite, Position = new Position(1, (short)(isWhite ? 5 : 3)) },
                new Pawn { IsWhite = !isWhite, Position = new Position(3, (short)(isWhite ? 5 : 3)) }
            })));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Pawn_Front_IsBlock_NoMovements(bool isWhite)
        {
            var pawn = new Pawn { IsWhite = isWhite, Position = new Position(2, 4) };

            Assert.Empty(pawn.GetMoves(new List<Piece> { new Pawn { Position = new Position(2, (short)(isWhite ? 5 : 3)) } }));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Pawn_FrontAndSides_IsBlock(bool isWhite)
        {
            var pawn = new Pawn { IsWhite = isWhite, Position = new Position(2, 4) };

            Assert.Empty(pawn.GetMoves(new List<Piece>
            {
                new Pawn { IsWhite = isWhite, Position = new Position(2, (short)(isWhite ? 5 : 3)) },
                new Pawn { IsWhite = isWhite, Position = new Position(1, (short)(isWhite ? 5 : 3)) },
                new Pawn { IsWhite = isWhite, Position = new Position(3, (short)(isWhite ? 5 : 3)) }
            }));
        }

        [Fact]
        public void Pawn_Simbol_P()
        {
            var pawn = new Pawn();

            Assert.Equal('P', pawn.Simbol);
        }

        [Theory]
        [MemberData(nameof(GetExpectedStartPositionsSecondBlocked))]
        public void Pawn_StartPosition_SecondBlocked_OneMovement(bool isWhite, ICollection<Position> expectedMoves)
        {
            var pawn = new Pawn { IsWhite = isWhite, Position = new Position(1, (short)(isWhite ? 2 : 7)) };

            Assert.True(Enumerable.SequenceEqual(expectedMoves, pawn.GetMoves(new List<Piece> { new Pawn { Position = new Position(1, (short)(isWhite ? 4 : 5)) } })));
        }

        [Theory]
        [MemberData(nameof(GetExpectedStartPositions))]
        public void Pawn_StartPosition_TwoMovements(bool isWhite, ICollection<Position> expectedMoves)
        {
            var pawn = new Pawn { IsWhite = isWhite, Position = new Position(1, (short)(isWhite ? 2 : 7)) };

            Assert.True(Enumerable.SequenceEqual(expectedMoves, pawn.GetMoves()));
        }

        [Fact]
        public void Pawn_ToString_Empty()
        {
            var pawn = new Pawn();

            Assert.Equal(string.Empty, pawn.ToString());
        }

        #endregion Public Methods

        #region Private Methods

        public static IEnumerable<object[]> GetExpectedStartPositions() =>
            new List<object[]>
            {
                new object[] { true, new List<Position> { new (1, 3), new (1, 4) } },
                new object[] { false, new List<Position> { new (1, 5), new (1, 6) } },
            };

        public static IEnumerable<object[]> GetExpectedStartPositionsSecondBlocked() =>
            new List<object[]>
            {
                new object[] { true, new List<Position> { new (1, 3) } },
                new object[] { false, new List<Position> { new (1, 6) } },
            };

        public static IEnumerable<object[]> GetExpectedTwoCapture() =>
            new List<object[]>
            {
                new object[] { true, new List<Position> { new (1, 5), new (3, 5) } },
                new object[] { false, new List<Position> { new (1, 3), new (3, 3) } },
            };

        #endregion Private Methods
    }
}