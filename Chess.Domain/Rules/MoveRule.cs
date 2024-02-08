using System.Collections.Immutable;

namespace Chess.Domain.Rules
{
    public abstract class MoveRule : Rule<IReadOnlyCollection<Position>, MoveRuleArgument>, IMoveRule
    {
        #region Protected Enums

        [Flags]
        protected enum Direction : byte
        {
            Cross = 1,

            Diagonal = 2
        }

        #endregion Protected Enums

        #region Protected Methods

        protected static IReadOnlyCollection<Position> GetMoves(Direction direction, Piece piece, IReadOnlyCollection<Piece> ocupied, bool justOneStep = false)
        {
            var possibleMovements = new List<Position>();

            var ocupiedNotCaptured = ocupied.Where(p => !p.IsCaptured).ToList();

            switch (direction)
            {
                case Direction.Cross:
                    Cross(piece, ocupiedNotCaptured, possibleMovements, justOneStep);
                    break;

                case Direction.Diagonal:
                    Diagonal(piece, ocupiedNotCaptured, possibleMovements, justOneStep);
                    break;

                case Direction.Cross | Direction.Diagonal:
                    Cross(piece, ocupiedNotCaptured, possibleMovements, justOneStep);
                    Diagonal(piece, ocupiedNotCaptured, possibleMovements, justOneStep);
                    break;
            }

            possibleMovements.Sort();
            return ImmutableList.CreateRange(possibleMovements);
        }

        #endregion Protected Methods

        #region Private Methods

        private static void CheckIsValid(
            Piece piece,
            IReadOnlyCollection<Piece> ocupied,
            ICollection<Position> possibleMovements,
            short xValue,
            short yValue,
            bool justOneStep)
        {
            var currentPosition = piece.Position;
            var possiblePosition = currentPosition with { X = (short)(currentPosition.X + xValue), Y = (short)(currentPosition.Y + yValue) };

            while (possiblePosition.IsValid())
            {
                if (!ocupied.Any(p => p.Position == possiblePosition))
                {
                    possibleMovements.Add(possiblePosition);
                    currentPosition = possiblePosition;
                    possiblePosition = currentPosition with { X = (short)(currentPosition.X + xValue), Y = (short)(currentPosition.Y + yValue) };

                    if (justOneStep)
                    {
                        break;
                    }
                }
                else if (ocupied.Where(p => p.IsWhite != piece.IsWhite).Any(p => p.Position == possiblePosition))
                {
                    possibleMovements.Add(possiblePosition);
                    break;
                }
                else
                {
                    break;
                }
            }
        }

        private static void Cross(Piece piece, IReadOnlyCollection<Piece> ocupied, ICollection<Position> possibleMovement, bool justOneStep)
        {
            new List<Position>
                {
                    new(0, 1),  // Up
                    new(0, -1), // Down
                    new(1, 0),  // Right
                    new(-1, 0), // Left
                }.ForEach(position => CheckIsValid(piece, ocupied, possibleMovement, position.X, position.Y, justOneStep));
        }

        private static void Diagonal(Piece piece, IReadOnlyCollection<Piece> ocupied, ICollection<Position> possibleMovement, bool justOneStep)
        {
            new List<Position>
                {
                    new(1, 1),  // Up - Right
                    new(-1, 1), // Up - Left
                    new(1, -1), // Down - Right
                    new(-1, -1),// Down - Left
                }.ForEach(position => CheckIsValid(piece, ocupied, possibleMovement, position.X, position.Y, justOneStep));
        }

        #endregion Private Methods
    }
}