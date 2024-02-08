namespace Chess.Domain.Rules
{
    public class RookMoveRule : MoveRule
    {
        #region Public Methods

        public override IReadOnlyCollection<Position> Evaluate(MoveRuleArgument argument)
        {
            return GetMoves(Direction.Cross, argument.Piece, argument.Ocupied);
        }

        #endregion Public Methods
    }
}