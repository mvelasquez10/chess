namespace Chess.Domain.Game
{
    public struct CurrentPiece(Piece piece, IReadOnlyCollection<Position> positions)
    {
        #region Public Properties

        public Piece Piece { get; set; } = piece;

        public IReadOnlyCollection<Position> Positions { get; set; } = positions;

        #endregion Public Properties
    }
}