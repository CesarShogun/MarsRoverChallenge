namespace MarsRoverControl.Models
{
    public class PlateauData
    {
        public readonly string NAME;
        public readonly Coords LIMITS;

        public PlateauData(string name, Coords limits)
        {
            NAME = name;
            LIMITS = limits;
        }

        public int WIDTH
        {
            get
            {
                return LIMITS.X;
            }
        }

        public int HEIGHT
        {
            get
            {
                return LIMITS.Y;
            }
        }
    }
}
