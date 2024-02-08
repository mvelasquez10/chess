using Chess.Domain.Rules;

namespace Chess.Domain.Pieces
{
    public record Pawn : Piece
    {
        public override char Simbol => 'P';

        protected override IMoveRule MoveRule { get; init; } = new PawnMoveRule();

        public override string ToString() => string.Empty;
    }
}