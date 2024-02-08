namespace Chess.Domain.Rules
{
    public abstract class Rule<TResult, TArgument> : IRule<TResult, TArgument>
    {
        #region Public Methods

        public abstract TResult Evaluate(TArgument argument);

        #endregion Public Methods
    }
}