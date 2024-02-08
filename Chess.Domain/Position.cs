namespace Chess.Domain
{
    public record Position : IComparable<Position>
    {
        public Position(short x, short y)
        {
            X = x;
            Y = y;
        }

        public short X { get; init; }

        public short Y { get; init; }

        public override string ToString() => $"{(char)(96 + X)}{Y}";

        public bool IsValid() => X >= 1 && Y >= 1 && X <= 8 && Y <= 8;

        public int CompareTo(Position? other)
        {
            if (other is null)
            {
                return 0;
            }

            if (X.CompareTo(other.X) == 0)
            {
                return Y.CompareTo(other.Y);
            }

            return X.CompareTo(other.X);
        }
    }
}