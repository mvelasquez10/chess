using Chess.Domain.Rules;

namespace Chess.Domain.Pieces
{
    public record Knight : Piece
    {
        public override char Simbol => 'N';

        protected override IMoveRule MoveRule { get; init; } = new KnightMoveRule();

        public override string ToString()
        {
            return base.ToString();
        }
    }
}