using Chess.Domain;
using Chess.Domain.Game;
using Chess.Domain.Game.GameEventArgs;
using Chess.Domain.Pieces;

using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace Chess.Engine.Test
{
    public class GameEngineTest
    {
        [Fact]
        public void GameEngine_Check_IsNotified()
        {
            var engine = new GameEngine();

            engine.NewGame(new Game(new List<Piece>
            {
                new Queen { Position = new(1,7), IsWhite = true },
                new King { Position = new(8,1), IsWhite = true },
                new King { Position = new(8,8)},
            }));

            engine.SelectCurrentPiece(new(1, 7));

            var eventAssert = Assert.Raises<CheckEventArgs>(
                e => engine.OnCheck += e,
                e => engine.OnCheck -= e,
                () => engine.MoveCurrentPiece(new(1, 8)));

            Assert.Equal(Turn.Black, engine.CurrentTurn);
        }

        [Fact]
        public void GameEngine_Checkmate_IsNotified()
        {
            var engine = new GameEngine();

            engine.NewGame(new Game(new List<Piece>
            {
                new Queen { Position = new(1,7), IsWhite = true },
                new Rook { Position = new(2,7), IsWhite = true },
                new King { Position = new(8,1), IsWhite = true },
                new King { Position = new(8,8)},
            }));

            engine.SelectCurrentPiece(new(1, 7));

            var eventAssert = Assert.Raises<GameOverEventArgs>(
                e => engine.OnGameOver += e,
                e => engine.OnGameOver -= e,
                () => engine.MoveCurrentPiece(new(1, 8)));

            Assert.Equal(GameStatus.WhiteWins, eventAssert.Arguments.Status);
        }

        [Fact]
        public void GameEngine_Draw_IsNotified()
        {
            var engine = new GameEngine();

            engine.NewGame(new Game(new List<Piece>
            {
                new Queen { Position = new(1,6), IsWhite = true },
                new Rook { Position = new(7,1), IsWhite = true },
                new King { Position = new(8,1), IsWhite = true },
                new King { Position = new(8,8)},
            }));

            engine.SelectCurrentPiece(new(1, 6));

            var eventAssert = Assert.Raises<GameOverEventArgs>(
                e => engine.OnGameOver += e,
                e => engine.OnGameOver -= e,
                () => engine.MoveCurrentPiece(new(1, 7)));

            Assert.Equal(GameStatus.Draw, engine.CurrentGame.Status);
        }

        [Fact]
        public void GameEngine_Finish_Draw()
        {
            var engine = new GameEngine();
            engine.NewGame();

            Assert.Equal(GameStatus.Playing, engine.CurrentGame.Status);

            var eventAssert = Assert.Raises<GameOverEventArgs>(
                e => engine.OnGameOver += e,
                e => engine.OnGameOver -= e,
                () => engine.FinishInDraw());

            Assert.Equal(GameStatus.Draw, eventAssert.Arguments.Status);
        }

        [Fact]
        public void GameEngine_Load_Game()
        {
            var engine = new GameEngine();
            engine.NewGame(new Game(new List<Piece>
            {
                new King { Position = new(3,1), IsWhite = true },
                new King { Position = new(4,3)},
            }));

            Assert.Equal(2, engine.Pieces.Count);
        }

        [Fact]
        public void GameEngine_MovePiece_Capture()
        {
            var engine = new GameEngine();
            engine.NewGame(new Game(new List<Piece>
            {
                new Queen { Id = 1, Position = new(1,1), IsWhite = true },
                new King { Position = new(1,8), IsWhite = true },
                new Queen { Id = 2,Position = new(1,2)},
                new King { Position = new(8,8)},
            }));

            Assert.Equal(Turn.White, engine.CurrentTurn);

            engine.SelectCurrentPiece(new(1, 1));
            engine.MoveCurrentPiece(new(1, 2));

            Assert.Equal(new(1, 2), engine.CurrentGame.Pieces.First(p => p.Id == 1).Position);
            Assert.True(engine.CurrentGame.Pieces.First(p => p.Id == 2).IsCaptured);
        }

        [Fact]
        public void GameEngine_MovePiece_FinishTurn()
        {
            var engine = new GameEngine();
            engine.NewGame();

            Assert.Equal(Turn.White, engine.CurrentTurn);

            engine.SelectCurrentPiece(new(1, 2));

            var eventAssert = Assert.Raises<CurrentTurnEventArgs>(
                e => engine.OnTurnChanged += e,
                e => engine.OnTurnChanged -= e,
                () => engine.MoveCurrentPiece(new(1, 3)));

            Assert.Equal(Turn.Black, engine.CurrentTurn);
            Assert.Equal(Turn.Black, eventAssert.Arguments.CurrentTurn);
        }

        [Fact]
        public void GameEngine_MovePiece_GameOver_False()
        {
            var engine = new GameEngine();
            engine.NewGame();
            engine.FinishInDraw();
            engine.SelectCurrentPiece(new(2, 1));

            Assert.False(engine.MoveCurrentPiece(new(3, 1)));
        }

        [Fact]
        public void GameEngine_MovePiece_IsNotified()
        {
            var engine = new GameEngine();
            engine.NewGame();

            Assert.Equal(Turn.White, engine.CurrentTurn);

            engine.SelectCurrentPiece(new(1, 2));

            var eventAssert = Assert.Raises<PieceMoveEventArgs>(
                e => engine.OnPieceMoved += e,
                e => engine.OnPieceMoved -= e,
                () => engine.MoveCurrentPiece(new(1, 3)));

            Assert.NotNull(eventAssert.Arguments.PiecesMoved);
        }

        [Fact]
        public void GameEngine_MovePiece_NotSelected_False()
        {
            var engine = new GameEngine();
            engine.NewGame();

            Assert.False(engine.MoveCurrentPiece(new(3, 1)));
        }

        [Fact]
        public void GameEngine_MovePiece_NotValidMove_False()
        {
            var engine = new GameEngine();
            engine.NewGame();
            engine.SelectCurrentPiece(new(2, 1));

            Assert.False(engine.MoveCurrentPiece(new(3, 1)));
        }

        [Fact]
        public void GameEngine_New_Game()
        {
            var engine = new GameEngine();
            engine.NewGame();

            Assert.Equal(32, engine.Pieces.Count);
            Assert.Equal(Turn.White, engine.CurrentTurn);
        }

        [Fact]
        public void GameEngine_PerformCastle_Valid()
        {
            var engine = new GameEngine();

            engine.NewGame(new Game(new List<Piece>
            {
                new Rook { Position = new(1,1), IsWhite = true },
                new King { Position = new(5, 1), IsWhite = true },
                new King { Position = new(8,8)},
            }));

            engine.SelectCurrentPiece(new(5, 1));

            var eventAssert = Assert.Raises<PieceMoveEventArgs>(
                e => engine.OnPieceMoved += e,
                e => engine.OnPieceMoved -= e,
                () => engine.MoveCurrentPiece(new(3, 1)));

            Assert.Equal(2, eventAssert.Arguments.PiecesMoved.Count);
        }

        [Fact]
        public void GameEngine_PerformPromotion_IsNotified()
        {
            var engine = new GameEngine();

            engine.NewGame(new Game(new List<Piece>
            {
                new Pawn { Position = new(1,7), IsWhite = true },
                new King { Position = new(8,1), IsWhite = true },
                new King { Position = new(8,8)},
            }));

            engine.SelectCurrentPiece(new(1, 7));

            var eventAssert = Assert.Raises<PieceEventArgs>(
                e => engine.OnPromotion += e,
                e => engine.OnPromotion -= e,
                () => engine.MoveCurrentPiece(new(1, 8)));

            Assert.True(eventAssert.Arguments.Piece is Pawn);
        }

        [Theory]
        [InlineData(Promote.Queen)]
        [InlineData(Promote.Rook)]
        [InlineData(Promote.Knight)]
        [InlineData(Promote.Bishop)]
        public void GameEngine_PerformPromotion_Pïeces(Promote promotion)
        {
            var engine = new GameEngine();

            engine.NewGame(new Game(new List<Piece>
            {
                new Pawn { Position = new(1,7), IsWhite = true },
                new King { Position = new(8,1), IsWhite = true },
                new King { Position = new(8,8)},
            }));

            engine.SelectCurrentPiece(new(1, 7));

            var eventRequest = Assert.Raises<PieceEventArgs>(
                e => engine.OnPromotion += e,
                e => engine.OnPromotion -= e,
                () => engine.MoveCurrentPiece(new(1, 8)));

            var pawn = (Pawn)eventRequest.Arguments.Piece;

            var eventAssert = Assert.Raises<PieceMoveEventArgs>(
                e => engine.OnPieceMoved += e,
                e => engine.OnPieceMoved -= e,
                () => engine.PerformPromotion(pawn, promotion));

            Assert.Equal(promotion.ToString(), eventAssert.Arguments.PiecesMoved.First().NewPiece.GetType().Name);
            Assert.Equal(typeof(Pawn).Name, eventAssert.Arguments.PiecesMoved.First().OldPiece.GetType().Name);
        }

        [Fact]
        public void GameEngine_Promotion_IsNotified()
        {
            var engine = new GameEngine();

            engine.NewGame(new Game(new List<Piece>
            {
                new Pawn { Id = 1, Position = new(1,7), IsWhite = true },
                new King { Position = new(8,1), IsWhite = true },
                new King { Position = new(8,7)},
            }));

            engine.SelectCurrentPiece(new(1, 7));

            var eventAssert = Assert.Raises<PieceEventArgs>(
                e => engine.OnPromotion += e,
                e => engine.OnPromotion -= e,
                () => engine.MoveCurrentPiece(new(1, 8)));

            Assert.Equal(1, eventAssert.Arguments.Piece.Id);
        }

        [Fact]
        public void GameEngine_SelectPiece_EmptySpace()
        {
            var engine = new GameEngine();
            engine.NewGame(new Game(new List<Piece>
            {
                new Queen { Id = 1, Position = new(1,1), IsWhite = true },
                new King { Position = new(1,8), IsWhite = true },
                new Queen { Id = 2,Position = new(1,2)},
                new King { Position = new(8,8)},
            }));

            Assert.Equal(Turn.White, engine.CurrentTurn);

            engine.SelectCurrentPiece(new(1, 5));

            Assert.Null(engine.CurrentPiece);
        }

        [Fact]
        public void GameEngine_SelectPiece_PosibleMoves()
        {
            var engine = new GameEngine();
            var position = new Position(2, 1);

            engine.NewGame();

            var eventAssert = Assert.Raises<CurrentPieceEventArgs>(
                e => engine.OnCurrentPieceChanged += e,
                e => engine.OnCurrentPieceChanged -= e,
                () => engine.SelectCurrentPiece(new(2, 1)));

            Assert.Equal(2, eventAssert.Arguments.CurrentPiece?.Positions.Count);
        }
    }
}