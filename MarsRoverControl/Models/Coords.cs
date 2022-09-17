namespace MarsRoverControl.Models
{
    public struct Coords
    {
        public int X;
        public int Y;

        public Coords(int x, int y)
        {
            if (x < 0 || y < 0)
                throw new ArgumentException("COORD ERROR: No coord can be lower than zero.");

            X = x;
            Y = y;
        }

        public bool FurtherThan(Coords coord)
        {
            return X > coord.X || Y > coord.Y;
        }
    }
}
