namespace Chess.Domain.Game
{
    public interface IGame
    {
        public int CurrentTurn { get; set; }
        public DateTime Finished { get; protected set; }
        public Guid Id { get; init; }
        IReadOnlyCollection<Piece> Pieces { get; }
        public DateTime Started { get; init; }
        public GameStatus Status { get; protected set; }

        void Finish(Turn? currentTurn);

        void UpdatePiece(Piece oldPiece, Piece newPiece);
    }
}