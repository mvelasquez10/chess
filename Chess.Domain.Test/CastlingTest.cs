using Chess.Domain.Pieces;
using Chess.Domain.Rules;

using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace Chess.Domain.Test
{
    public class CastlingTest
    {
        #region Public Methods

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Castling_King_IsCheck(bool isWhite)
        {
            var king = new King { IsWhite = isWhite, Position = new(5, (short)(isWhite ? 1 : 8)) };
            var rookLeft = new Rook { IsWhite = isWhite, Position = new(8, (short)(isWhite ? 1 : 8)) };
            var rookRigth = new Rook { IsWhite = isWhite, Position = new(8, (short)(isWhite ? 1 : 8)) };
            var rookEnemy = new Rook { IsWhite = !isWhite, Position = new(5, 5) };
            var castlingRule = new CastlingRule();
            var moveRuleArgument = new MoveRuleArgument(king, new List<Piece> { rookLeft, rookRigth, rookEnemy });

            Assert.Empty(castlingRule.Evaluate(moveRuleArgument));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Castling_King_Moved(bool isWhite)
        {
            var king = new King { IsWhite = isWhite, Position = new(5, (short)(isWhite ? 1 : 8)), LastPosition = new(5, 5) };
            var rookLeft = new Rook { IsWhite = isWhite, Position = new(8, (short)(isWhite ? 1 : 8)) };
            var rookRigth = new Rook { IsWhite = isWhite, Position = new(8, (short)(isWhite ? 1 : 8)) };
            var castlingRule = new CastlingRule();
            var moveRuleArgument = new MoveRuleArgument(king, new List<Piece> { rookLeft, rookRigth });

            Assert.Empty(castlingRule.Evaluate(moveRuleArgument));
        }

        [Fact]
        public void Castling_Only_King()
        {
            var piece = new Rook();
            var castlingRule = new CastlingRule();
            var moveRuleArgument = new MoveRuleArgument(piece);

            Assert.Empty(castlingRule.Evaluate(moveRuleArgument));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Castling_PathLong_BlockedEnemy(bool isWhite)
        {
            var king = new King { IsWhite = isWhite, Position = new(5, (short)(isWhite ? 1 : 8)) };
            var rookLeft = new Rook { IsWhite = isWhite, Position = new(1, (short)(isWhite ? 1 : 8)) };
            var rookEnemy = new Rook { IsWhite = !isWhite, Position = new(4, (short)(isWhite ? 8 : 1)) };
            var castlingRule = new CastlingRule();
            var moveRuleArgument = new MoveRuleArgument(king, new List<Piece> { rookLeft, rookEnemy });

            Assert.Empty(castlingRule.Evaluate(moveRuleArgument));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Castling_PathLong_BlockedFriendly(bool isWhite)
        {
            var king = new King { IsWhite = isWhite, Position = new(5, (short)(isWhite ? 1 : 8)) };
            var rookLeft = new Rook { IsWhite = isWhite, Position = new(1, (short)(isWhite ? 1 : 8)) };
            var queen = new Queen { IsWhite = isWhite, Position = new(2, (short)(isWhite ? 1 : 8)) };
            var castlingRule = new CastlingRule();
            var moveRuleArgument = new MoveRuleArgument(king, new List<Piece> { rookLeft, queen });

            Assert.Empty(castlingRule.Evaluate(moveRuleArgument));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Castling_Paths_Available(bool isWhite)
        {
            var king = new King { IsWhite = isWhite, Position = new(5, (short)(isWhite ? 1 : 8)) };
            var rookRigth = new Rook { IsWhite = isWhite, Position = new(8, (short)(isWhite ? 1 : 8)) };
            var rookLeft = new Rook { IsWhite = isWhite, Position = new(1, (short)(isWhite ? 1 : 8)) };
            var castlingRule = new CastlingRule();
            var moveRuleArgument = new MoveRuleArgument(king, new List<Piece> { rookRigth, rookLeft });
            var expectedMoves = new List<Position>
            {
                new(3, (short)(isWhite ? 1 : 8)),
                new(7, (short)(isWhite ? 1 : 8))
            };

            Assert.True(Enumerable.SequenceEqual(expectedMoves, castlingRule.Evaluate(moveRuleArgument)));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Castling_PathShort_BlockedEnemy(bool isWhite)
        {
            var king = new King { IsWhite = isWhite, Position = new(5, (short)(isWhite ? 1 : 8)) };
            var rookRigth = new Rook { IsWhite = isWhite, Position = new(8, (short)(isWhite ? 1 : 8)) };
            var rookEnemy = new Rook { IsWhite = !isWhite, Position = new(6, (short)(isWhite ? 8 : 1)) };
            var castlingRule = new CastlingRule();
            var moveRuleArgument = new MoveRuleArgument(king, new List<Piece> { rookRigth, rookEnemy });

            Assert.Empty(castlingRule.Evaluate(moveRuleArgument));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Castling_PathShort_BlockedFriendly(bool isWhite)
        {
            var king = new King { IsWhite = isWhite, Position = new(5, (short)(isWhite ? 1 : 8)) };
            var rookRigth = new Rook { IsWhite = isWhite, Position = new(8, (short)(isWhite ? 1 : 8)) };
            var queen = new Queen { IsWhite = isWhite, Position = new(6, (short)(isWhite ? 1 : 8)) };
            var castlingRule = new CastlingRule();
            var moveRuleArgument = new MoveRuleArgument(king, new List<Piece> { rookRigth, queen });

            Assert.Empty(castlingRule.Evaluate(moveRuleArgument));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Castling_Rooks_Moved(bool isWhite)
        {
            var king = new King { IsWhite = isWhite, Position = new(5, (short)(isWhite ? 1 : 8)) };
            var rookLeft = new Rook { IsWhite = isWhite, Position = new(8, (short)(isWhite ? 1 : 8)), LastPosition = new(5, 5) };
            var rookRigth = new Rook { IsWhite = isWhite, Position = new(8, (short)(isWhite ? 1 : 8)), LastPosition = new(5, 5) };
            var castlingRule = new CastlingRule();
            var moveRuleArgument = new MoveRuleArgument(king, new List<Piece> { rookLeft, rookRigth });

            Assert.Empty(castlingRule.Evaluate(moveRuleArgument));
        }

        #endregion Public Methods
    }
}