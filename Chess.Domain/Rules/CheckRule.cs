namespace Chess.Domain.Rules
{
    public class CheckRule : Rule<bool, CheckRuleArgument>
    {
        #region Public Methods

        public override bool Evaluate(CheckRuleArgument argument)
        {
            var king = argument.King;
            var ocupied = argument.Ocupied;
            var enemiesPossibleMoves = ocupied
                .Where(p => p.IsWhite != king.IsWhite && !p.IsCaptured && p.Position != argument.CheckingPiece.Position)
                .SelectMany(p => p.GetMoves(ocupied));

            return enemiesPossibleMoves.Any(p => p == king.Position);
        }

        #endregion Public Methods
    }
}