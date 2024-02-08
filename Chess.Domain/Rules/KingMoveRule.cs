namespace Chess.Domain.Rules
{
    public class KingMoveRule : MoveRule
    {
        #region Public Methods

        public override IReadOnlyCollection<Position> Evaluate(MoveRuleArgument argument)
        {
            return GetMoves(Direction.Cross | Direction.Diagonal, argument.Piece, argument.Ocupied, true);
        }

        #endregion Public Methods
    }
}