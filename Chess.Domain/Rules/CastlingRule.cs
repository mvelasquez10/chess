using Chess.Domain.Pieces;

namespace Chess.Domain.Rules
{
    public class CastlingRule : Rule<IReadOnlyCollection<Position>, MoveRuleArgument>
    {
        #region Public Methods

        public override IReadOnlyCollection<Position> Evaluate(MoveRuleArgument argument)
        {
            var result = new List<Position>();
            var piece = argument.Piece;
            var ocupied = argument.Ocupied;
            var rooks = ocupied.Where(p => p.IsWhite == piece.IsWhite && p is Rook);
            var enemiesPossibleMoves = ocupied.Where(p => p.IsWhite != piece.IsWhite && !p.IsCaptured).SelectMany(p => p.GetMoves(ocupied));

            if (piece is not King
                || piece.LastPosition is not null
                || !rooks.Any(p => p.LastPosition is null)
                || enemiesPossibleMoves.Any(p => p == piece.Position))
            {
                return result;
            }

            var availablePossitionLong = new List<Position>
            {
                new(2, (short)(piece.IsWhite ? 1 : 8)),
                new(3, (short)(piece.IsWhite ? 1 : 8)),
                new(4, (short)(piece.IsWhite ? 1 : 8)),
            };

            var availablePossitionShort = new List<Position>
            {
                new(6, (short)(piece.IsWhite ? 1 : 8)),
                new(7, (short)(piece.IsWhite ? 1 : 8)),
            };

            if (rooks.FirstOrDefault(p => p.Position == new Position(1, (short)(piece.IsWhite ? 1 : 8)) && p.LastPosition is null) is not null
                && !availablePossitionLong.Intersect(ocupied.Select(p => p.Position)).Any()
                && !availablePossitionLong.Skip(1).Intersect(enemiesPossibleMoves).Any())
            {
                result.Add(new(3, (short)(piece.IsWhite ? 1 : 8)));
            }

            if (rooks.FirstOrDefault(p => p.Position == new Position(8, (short)(piece.IsWhite ? 1 : 8)) && p.LastPosition is null) is not null
                && !availablePossitionShort.Intersect(ocupied.Select(p => p.Position)).Any()
                && !availablePossitionShort.Intersect(enemiesPossibleMoves).Any())
            {
                result.Add(new(7, (short)(piece.IsWhite ? 1 : 8)));
            }

            return result;
        }

        #endregion Public Methods
    }
}