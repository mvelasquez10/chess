namespace Chess.Domain.Rules
{
    public interface IRule<TResult, TArgument>
    {
        #region Public Methods

        TResult Evaluate(TArgument argument);

        #endregion Public Methods
    }
}