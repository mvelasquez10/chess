namespace Chess.Domain.Rules
{
    public class MoveRuleArgument(Piece piece, IReadOnlyCollection<Piece>? ocupied = null)
    {
        #region Public Fields

        public readonly IReadOnlyCollection<Piece> Ocupied = ocupied ?? new List<Piece>();

        public readonly Piece Piece = piece;

        #endregion Public Fields
    }
}