namespace Chess.Domain.Game.GameEventArgs
{
    public class CurrentPieceEventArgs(CurrentPiece? currentPiece) : EventArgs
    {
        #region Public Properties

        public CurrentPiece? CurrentPiece { get; init; } = currentPiece;

        #endregion Public Properties
    }
}