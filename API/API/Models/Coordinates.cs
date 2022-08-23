namespace API.Models
{
    public class Coordinates
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public Coordinates(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public static bool operator ==(Coordinates c1, Coordinates c2)
        {
            if (c1 is null)
                return c2 is null;

            return c1.Equals(c2);
        }

        public static bool operator !=(Coordinates c1, Coordinates c2)
        {
            return !(c1 == c2);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var c2 = (Coordinates)obj;
            return (Row == c2.Row && Col == c2.Col);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row.GetHashCode(), Col.GetHashCode());
        }
    }
}
