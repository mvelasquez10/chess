using Chess.Domain;
using Chess.Domain.Game;
using Chess.Domain.Pieces;

using System.Collections.Immutable;

namespace Chess.Engine
{
    public record Game : IGame
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public int CurrentTurn { get; set; } = 1;

        public GameStatus Status { get; set; } = GameStatus.Playing;
        public DateTime Started { get; init; } = DateTime.Now;

        public DateTime Finished { get; set; }

        public Game()
        {
        }

        public Game(IReadOnlyCollection<Piece> pieces)
        {
            if (pieces.Count(p => p is King) < 2)
            {
                throw new ArgumentException("A game must have at least two kings");
            }

            Pieces = pieces;
        }

        public IReadOnlyCollection<Piece> Pieces { get; internal set; } = GeneratePieces();

        private static ImmutableList<Piece> GeneratePieces()
        {
            var pieces = new HashSet<Piece>();

            for (short i = 1; i <= 8; i++)
            {
                pieces.Add(new Pawn { Id = (short)(pieces.Count + 1), Position = new(i, 2), IsWhite = true });
                pieces.Add(new Pawn { Id = (short)(pieces.Count + 1), Position = new(i, 7) });
            }

            for (short i = 1; i <= 8; i += 7)
            {
                pieces.Add(new Rook { Id = (short)(pieces.Count + 1), Position = new(i, 1), IsWhite = true });
                pieces.Add(new Rook { Id = (short)(pieces.Count + 1), Position = new(i, 8) });
            }

            for (short i = 2; i <= 7; i += 5)
            {
                pieces.Add(new Knight { Id = (short)(pieces.Count + 1), Position = new(i, 1), IsWhite = true });
                pieces.Add(new Knight { Id = (short)(pieces.Count + 1), Position = new(i, 8) });
            }

            for (short i = 3; i <= 6; i += 3)
            {
                pieces.Add(new Bishop { Id = (short)(pieces.Count + 1), Position = new(i, 1), IsWhite = true });
                pieces.Add(new Bishop { Id = (short)(pieces.Count + 1), Position = new(i, 8) });
            }

            pieces.Add(new Queen { Id = (short)(pieces.Count + 1), Position = new(4, 1), IsWhite = true });
            pieces.Add(new Queen { Id = (short)(pieces.Count + 1), Position = new(4, 8) });

            pieces.Add(new King { Id = (short)(pieces.Count + 1), Position = new(5, 1), IsWhite = true });
            pieces.Add(new King { Id = (short)(pieces.Count + 1), Position = new(5, 8) });

            return ImmutableList.CreateRange(pieces);
        }

        public void UpdatePiece(Piece oldPiece, Piece newPiece)
        {
            Pieces = ImmutableList.CreateRange(Pieces.Where(p => p != oldPiece).Append(newPiece));
        }

        public void Finish(Turn? currentTurn)
        {
            Status = currentTurn.HasValue ? (currentTurn.Value == Turn.White ? GameStatus.WhiteWins : GameStatus.BlackWins) : GameStatus.Draw;
            Finished = DateTime.Now;
        }
    }
}