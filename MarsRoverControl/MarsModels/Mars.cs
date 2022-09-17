using MarsRoverControl.Models;

namespace MarsRoverControl.MarsModels
{
    public static class Mars
    {
        public static readonly List<Plateau> PLATEAUS = new List<Plateau> { 
            new Plateau("Endeavour", new Coords(5, 5)),
            new Plateau("Gale", new Coords(7, 5)),
            new Plateau("Jezero", new Coords(4, 9))
        };

        public static List<PlateauData> GetPlateaus()
        {
            List<PlateauData> plateaus = new();
            PLATEAUS.ForEach(p => plateaus.Add(new PlateauData(p.NAME, p.LIMITS)));

            return plateaus;
        }

        public static void LandRoverOn(MarsRover rover, String plateau, Coords coords)
        {
            var index = PlateauIndexOf(plateau);
            if (index == -1)
                throw new Exception($"ERROR: No plateau was found under the name \"{plateau}\".");

            PLATEAUS[index].AddRoverOn(rover, coords);
        }

        public static int PlateauIndexOf(string name)
        {
            for (var i = 0; i < PLATEAUS.Count; i++)
            {
                if (PLATEAUS[i].NAME == name)
                    return i;
            }
            return -1;
        }
    }
}
