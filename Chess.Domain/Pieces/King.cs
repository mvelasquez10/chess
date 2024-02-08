using Chess.Domain.Rules;

namespace Chess.Domain.Pieces
{
    public record King : Piece
    {
        public override char Simbol => 'K';

        protected override IMoveRule MoveRule { get; init; } = new KingMoveRule();

        public override string ToString()
        {
            return base.ToString();
        }
    }
}