using Chess.Domain.Rules;

namespace Chess.Domain
{
    public abstract record Piece : Entity
    {
        public abstract char Simbol { get; }

        public bool IsWhite { get; init; }

        public Position Position { get; init; } = default!;

        public Position LastPosition { get; init; } = default!;

        public bool IsCaptured { get; init; } = false;

        protected abstract IMoveRule MoveRule { get; init; }

        public IReadOnlyCollection<Position> GetMoves(IReadOnlyCollection<Piece> ocupied) => MoveRule.Evaluate(new(this, ocupied));

        public IReadOnlyCollection<Position> GetMoves() => GetMoves(new List<Piece>());

        public override string ToString() => $"{Simbol}";
    }
}