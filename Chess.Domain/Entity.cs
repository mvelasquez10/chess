namespace Chess.Domain
{
    public abstract record Entity
    {
        public short Id { get; init; }
    }
}