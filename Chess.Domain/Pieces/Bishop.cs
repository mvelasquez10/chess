using Chess.Domain.Rules;

namespace Chess.Domain.Pieces
{
    public record Bishop : Piece
    {
        public override char Simbol => 'B';

        protected override IMoveRule MoveRule { get; init; } = new BishopMoveRule();

        public override string ToString()
        {
            return base.ToString();
        }
    }
}