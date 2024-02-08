using System.Collections.Immutable;

namespace Chess.Domain.Rules
{
    public class KnightMoveRule : MoveRule
    {
        #region Public Methods

        public override IReadOnlyCollection<Position> Evaluate(MoveRuleArgument argument)
        {
            var piece = argument.Piece;

            var possibleMovements = new List<Position>();

            new List<Position>
            {
                new(1, 2),  // Up - Rigth
                new(-1, 2), // Up - Left
                new(1, -2), // Down - Right
                new(-1, -2),// Down - Left
                new(-2, 1), // Left - Up
                new(-2, -1),// Left - Down
                new(2, 1),  // Right - Up
                new(2, -1)  // Right - Down
            }.ForEach(position => CheckIsValid(argument.Piece, argument.Ocupied, possibleMovements, position.X, position.Y));

            possibleMovements.Sort();
            return ImmutableList.CreateRange(possibleMovements);
        }

        #endregion Public Methods

        #region Private Methods

        private static void CheckIsValid(Piece piece, IReadOnlyCollection<Piece> ocupied, List<Position> possibleMovements, short xValue, short yValue)
        {
            var ocupiedNotCaptured = ocupied.Where(p => !p.IsCaptured).ToList();
            var possibleMovement = piece.Position with { X = (short)(piece.Position.X + xValue), Y = (short)(piece.Position.Y + yValue) };
            if (possibleMovement.IsValid()
                && (!ocupiedNotCaptured.Any(p => p.Position == possibleMovement)
                    || ocupiedNotCaptured.Where(p => p.IsWhite != piece.IsWhite).Any(p => p.Position == possibleMovement)))
            {
                possibleMovements.Add(possibleMovement);
            }
        }

        #endregion Private Methods
    }
}