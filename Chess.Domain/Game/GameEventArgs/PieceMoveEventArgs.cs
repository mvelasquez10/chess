namespace Chess.Domain.Game.GameEventArgs
{
    public class PieceMoveEventArgs(Piece? pieceCaptured, ICollection<MoveStatus> piecesMoved) : EventArgs
    {
        #region Public Properties

        public Piece? PieceCaptured { get; init; } = pieceCaptured;

        public ICollection<MoveStatus> PiecesMoved { get; init; } = piecesMoved;

        #endregion Public Properties
    }
}