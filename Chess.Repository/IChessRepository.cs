using Chess.Domain.Game;

namespace Chess.Repository
{
    public interface IChessRepository
    {
        #region Public Methods

        Task<IGame?> LoadGame(Guid gameId);

        Task SaveEvent(IGame game, ChessAction action);

        Task SaveGame(IGame game);

        #endregion Public Methods
    }
}