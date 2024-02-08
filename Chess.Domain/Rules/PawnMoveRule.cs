using System.Collections.Immutable;

namespace Chess.Domain.Rules
{
    internal class PawnMoveRule : MoveRule
    {
        #region Public Methods

        public override IReadOnlyCollection<Position> Evaluate(MoveRuleArgument argument)
        {
            var piece = argument.Piece;
            var ocupiedNotCaptured = argument.Ocupied.Where(x => !x.IsCaptured).ToList();
            Position possibleMovement;

            var possibleMovements = new List<Position>();

            // If front is available it can move 1
            possibleMovement = argument.Piece.Position with { Y = (short)(argument.Piece.Position.Y + (piece.IsWhite ? 1 : -1)) };
            if (!ocupiedNotCaptured.Any(p => p.Position == possibleMovement))
            {
                possibleMovements.Add(possibleMovement);
            }

            // Start position allows a second square, if available
            if (piece.Position.Y == (piece.IsWhite ? 2 : 7))
            {
                possibleMovement = argument.Piece.Position with { Y = (short)(argument.Piece.Position.Y + (piece.IsWhite ? 2 : -2)) };
                if (!ocupiedNotCaptured.Any(p => p.Position == possibleMovement))
                {
                    possibleMovements.Add(possibleMovement);
                }
            }

            // Capture available if front sides has enemy
            var enemy = ocupiedNotCaptured.Where(p => p.IsWhite != piece.IsWhite).ToList();

            var possitionLeft = piece.Position with { Y = (short)(piece.Position.Y + (piece.IsWhite ? 1 : -1)), X = (short)(piece.Position.X - 1) };
            if (enemy.Any(p => p.Position == possitionLeft))
            {
                possibleMovements.Add(enemy.First(p => p.Position == possitionLeft).Position);
            }

            var possitionRight = piece.Position with { Y = (short)(piece.Position.Y + (piece.IsWhite ? 1 : -1)), X = (short)(piece.Position.X + 1) };
            if (enemy.Any(p => p.Position == possitionRight))
            {
                possibleMovements.Add(enemy.First(p => p.Position == possitionRight).Position);
            }

            //En Passant
            // TODO

            possibleMovements.Sort();
            return ImmutableList.CreateRange(possibleMovements);
        }

        #endregion Public Methods
    }
}