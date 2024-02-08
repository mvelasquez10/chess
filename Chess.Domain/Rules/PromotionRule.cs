using Chess.Domain.Pieces;

namespace Chess.Domain.Rules
{
    public class PromotionRule : Rule<bool, Piece>
    {
        #region Public Methods

        public override bool Evaluate(Piece argument)
        {
            if (argument is not Pawn)
            {
                return false;
            }

            return
                argument.IsWhite ?
                argument.Position.Y == 8 :
                argument.Position.Y == 1;
        }

        #endregion Public Methods
    }
}