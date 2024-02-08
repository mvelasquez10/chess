using Chess.Domain.Rules;

namespace Chess.Domain.Pieces
{
    public record Queen : Piece
    {
        public override char Simbol => 'Q';

        protected override IMoveRule MoveRule { get; init; } = new QueenMoveRule();

        public override string ToString()
        {
            return base.ToString();
        }
    }
}