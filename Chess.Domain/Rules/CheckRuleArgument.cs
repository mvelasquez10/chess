using Chess.Domain.Pieces;

namespace Chess.Domain.Rules
{
    public readonly struct CheckRuleArgument(King king, Piece checkingPiece, IReadOnlyCollection<Piece> ocupied)
    {
        #region Public Fields

        public readonly King King = king;

        public readonly IReadOnlyCollection<Piece> Ocupied = ocupied;

        #endregion Public Fields

        #region Public Properties

        public Piece CheckingPiece { get; } = checkingPiece;

        #endregion Public Properties
    }
}