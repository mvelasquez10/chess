namespace Chess.Domain.Rules
{
    public class QueenMoveRule : MoveRule
    {
        #region Public Methods

        public override IReadOnlyCollection<Position> Evaluate(MoveRuleArgument argument)
        {
            return GetMoves(Direction.Cross | Direction.Diagonal, argument.Piece, argument.Ocupied);
        }

        #endregion Public Methods
    }
}