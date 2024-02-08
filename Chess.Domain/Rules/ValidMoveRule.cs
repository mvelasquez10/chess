using Chess.Domain.Pieces;

namespace Chess.Domain.Rules
{
    public class ValidMoveRule : MoveRule
    {
        #region Public Methods

        public override IReadOnlyCollection<Position> Evaluate(MoveRuleArgument argument)
        {
            var resultMoves = new List<Position>();

            var possibleMoves = argument.Piece.GetMoves(argument.Ocupied).ToList();
            var newPossibleMove = argument.Ocupied.Where(p => p.Position != argument.Piece.Position && !p.IsCaptured).ToList();

            var checkRule = new CheckRule();
            var king = argument.Ocupied.FirstOrDefault(p => p is King && p.IsWhite == argument.Piece.IsWhite) as King;

            possibleMoves.ForEach(move =>
            {
                var tempMove = argument.Piece with { Position = move, LastPosition = argument.Piece.Position };
                var tempNewPossibleMove = new List<Piece>(newPossibleMove) { tempMove };
                var currentKing = argument.Piece == king ? tempMove as King : king;

                // King must be always present
#pragma warning disable CS8604 // Possible null reference argument.
                if (!checkRule.Evaluate(new(currentKing, tempMove, tempNewPossibleMove)))
                {
                    resultMoves.Add(move);
                }
#pragma warning restore CS8604 // Possible null reference argument.
            });

            return resultMoves;
        }

        #endregion Public Methods
    }
}