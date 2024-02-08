namespace Chess.Domain.Game.GameEventArgs
{
    public class PieceEventArgs(Piece piece) : EventArgs
    {
        #region Public Properties

        public Piece Piece { get; init; } = piece;

        #endregion Public Properties
    }
}