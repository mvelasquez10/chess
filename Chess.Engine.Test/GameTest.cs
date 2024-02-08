using Chess.Domain;
using Chess.Domain.Game;
using Chess.Domain.Pieces;

using System;
using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace Chess.Engine.Test
{
    public class GameTest
    {
        #region Public Methods

        [Fact]
        public void Game_Finish_BlackWins()
        {
            var game = new Game();

            game.Finish(Turn.Black);

            Assert.Equal(GameStatus.BlackWins, game.Status);
        }

        [Fact]
        public void Game_Finish_Draw()
        {
            var game = new Game();

            game.Finish(null);

            Assert.Equal(GameStatus.Draw, game.Status);
        }

        [Fact]
        public void Game_Finish_WhiteWins()
        {
            var game = new Game();

            game.Finish(Turn.White);

            Assert.Equal(GameStatus.WhiteWins, game.Status);
        }

        [Fact]
        public void Game_New_Id_1()
        {
            var game = new Game();

            Assert.IsType<Guid>(game.Id);
        }

        [Fact]
        public void Game_New_MustHaveKings()
        {
            Assert.Throws<ArgumentException>(() =>
            new Game(new List<Piece>
            {
                new Queen { IsWhite = true },
                new Queen {},
            }));
        }

        [Fact]
        public void Game_New_Pieces()
        {
            var game = new Game(new List<Piece>
            {
                new King { IsWhite = true },
                new King {},
            });

            Assert.Equal(2, game.Pieces.Count);
        }

        [Fact]
        public void Game_New_Pieces_1KingBlack()
        {
            var game = new Game();

            var expectedPositions = new List<Position>
            {
                new(5,8)
            };

            Assert.True(Enumerable.SequenceEqual(expectedPositions, game.Pieces.Where(p => p.IsWhite == false && p is King).Select(p => p.Position)));
        }

        [Fact]
        public void Game_New_Pieces_1KingWhite()
        {
            var game = new Game();

            var expectedPositions = new List<Position>
            {
                new(5,1)
            };

            Assert.True(Enumerable.SequenceEqual(expectedPositions, game.Pieces.Where(p => p.IsWhite == true && p is King).Select(p => p.Position)));
        }

        [Fact]
        public void Game_New_Pieces_1QueenBlack()
        {
            var game = new Game();

            var expectedPositions = new List<Position>
            {
                new(4,8)
            };

            Assert.True(Enumerable.SequenceEqual(expectedPositions, game.Pieces.Where(p => p.IsWhite == false && p is Queen).Select(p => p.Position)));
        }

        [Fact]
        public void Game_New_Pieces_1QueenWhite()
        {
            var game = new Game();

            var expectedPositions = new List<Position>
            {
                new(4,1)
            };

            Assert.True(Enumerable.SequenceEqual(expectedPositions, game.Pieces.Where(p => p.IsWhite == true && p is Queen).Select(p => p.Position)));
        }

        [Fact]
        public void Game_New_Pieces_2BishopsBlack()
        {
            var game = new Game();

            var expectedPositions = new List<Position>
            {
                new(3,8),
                new(6,8),
            };

            Assert.True(Enumerable.SequenceEqual(expectedPositions, game.Pieces.Where(p => !p.IsWhite && p is Bishop).Select(p => p.Position)));
        }

        [Fact]
        public void Game_New_Pieces_2BishopsWhite()
        {
            var game = new Game();

            var expectedPositions = new List<Position>
            {
                new(3,1),
                new(6,1),
            };

            Assert.True(Enumerable.SequenceEqual(expectedPositions, game.Pieces.Where(p => p.IsWhite && p is Bishop).Select(p => p.Position)));
        }

        [Fact]
        public void Game_New_Pieces_2KnightsBlack()
        {
            var game = new Game();

            var expectedPositions = new List<Position>
            {
                new(2,8),
                new(7,8),
            };

            Assert.True(Enumerable.SequenceEqual(expectedPositions, game.Pieces.Where(p => p.IsWhite == false && p is Knight).Select(p => p.Position)));
        }

        [Fact]
        public void Game_New_Pieces_2KnightsWhite()
        {
            var game = new Game();

            var expectedPositions = new List<Position>
            {
                new(2,1),
                new(7,1),
            };

            Assert.True(Enumerable.SequenceEqual(expectedPositions, game.Pieces.Where(p => p.IsWhite == true && p is Knight).Select(p => p.Position)));
        }

        [Fact]
        public void Game_New_Pieces_2RooksBlack()
        {
            var game = new Game();

            var expectedPositions = new List<Position>
            {
                new(1,8),
                new(8,8),
            };

            Assert.True(Enumerable.SequenceEqual(expectedPositions, game.Pieces.Where(p => p.IsWhite == false && p is Rook).Select(p => p.Position)));
        }

        [Fact]
        public void Game_New_Pieces_2RooksWhite()
        {
            var game = new Game();

            var expectedPositions = new List<Position>
            {
                new(1,1),
                new(8,1),
            };

            Assert.True(Enumerable.SequenceEqual(expectedPositions, game.Pieces.Where(p => p.IsWhite == true && p is Rook).Select(p => p.Position)));
        }

        [Fact]
        public void Game_New_Pieces_8PawnsBlack()
        {
            var game = new Game();

            var expectedPositions = new List<Position>
            {
                new(1,7),
                new(2,7),
                new(3,7),
                new(4,7),
                new(5,7),
                new(6,7),
                new(7,7),
                new(8,7),
            };

            Assert.True(Enumerable.SequenceEqual(expectedPositions, game.Pieces.Where(p => p.IsWhite == false && p is Pawn).Select(p => p.Position)));
        }

        [Fact]
        public void Game_New_Pieces_8PawnsWhite()
        {
            var game = new Game();

            var expectedPositions = new List<Position>
            {
                new(1,2),
                new(2,2),
                new(3,2),
                new(4,2),
                new(5,2),
                new(6,2),
                new(7,2),
                new(8,2),
            };

            Assert.True(Enumerable.SequenceEqual(expectedPositions, game.Pieces.Where(p => p.IsWhite == true && p is Pawn).Select(p => p.Position)));
        }

        [Fact]
        public void Game_New_Status_Playing()
        {
            var game = new Game();

            Assert.Equal(GameStatus.Playing, game.Status);
        }

        [Fact]
        public void Game_New_Turn_1()
        {
            var game = new Game();

            Assert.Equal(1, game.CurrentTurn);
        }

        [Fact]
        public void Game_Update_Piece()
        {
            var game = new Game();
            var piece = new Queen();

            game.UpdatePiece(game.Pieces.Last(), piece);

            Assert.Equal(game.Pieces.Last(), piece);
        }

        #endregion Public Methods
    }
}