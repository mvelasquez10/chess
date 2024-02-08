namespace Chess.Domain.Rules
{
    public class BishopMoveRule : MoveRule
    {
        #region Public Methods

        public override IReadOnlyCollection<Position> Evaluate(MoveRuleArgument argument)
        {
            return GetMoves(Direction.Diagonal, argument.Piece, argument.Ocupied);
        }

        #endregion Public Methods
    }
}