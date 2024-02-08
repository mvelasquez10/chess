namespace Chess.Domain.Rules
{
    public interface IMoveRule : IRule<IReadOnlyCollection<Position>, MoveRuleArgument>
    {
    }
}