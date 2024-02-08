namespace Chess.Domain.Game.GameEventArgs
{
    public class CurrentTurnEventArgs(Turn currentTurn) : EventArgs
    {
        #region Public Properties

        public Turn CurrentTurn { get; init; } = currentTurn;

        #endregion Public Properties
    }
}