using Chess.Domain.Rules;

namespace Chess.Domain.Pieces
{
    public record Rook : Piece
    {
        public override char Simbol => 'R';

        protected override IMoveRule MoveRule { get; init; } = new RookMoveRule();

        public override string ToString()
        {
            return base.ToString();
        }
    }
}