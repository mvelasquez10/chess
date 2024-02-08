namespace Chess.Domain.Game
{
    public class MoveStatus(Piece oldPiece, Piece newPiece, bool isCastling = false)
    {
        #region Public Properties

        public bool IsCastling { get; init; } = isCastling;

        public Piece NewPiece { get; init; } = newPiece;

        public Piece OldPiece { get; init; } = oldPiece;

        #endregion Public Properties
    }
}