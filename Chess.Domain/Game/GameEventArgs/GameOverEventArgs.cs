namespace Chess.Domain.Game.GameEventArgs
{
    public class GameOverEventArgs(GameStatus status) : EventArgs
    {
        #region Public Properties

        public GameStatus Status { get; set; } = status;

        #endregion Public Properties
    }
}