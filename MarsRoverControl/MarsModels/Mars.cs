using MarsRoverControl.Models;

namespace MarsRoverControl.MarsModels
{
    public static class Mars
    {
        public static readonly List<Plateau> PLATEAUS = new List<Plateau> { 
            new Plateau("Endeavour", 5, 5, new List<Coords>()),
            new Plateau("Gale", 7, 7, new List<Coords>() {
                new Coords(0, 0), new Coords(0, 1), new Coords(1, 0), new Coords(3, 4) }),
            new Plateau("Jezero", 9, 9, new List<Coords> {
                new Coords(2, 3), new Coords(5, 4) })
        };

        public static List<PlateauData> GetPlateaus()
        {
            List<PlateauData> plateaus = new();
            PLATEAUS.ForEach(p => plateaus.Add(new PlateauData(p.NAME, p.WIDTH, p.HEIGHT, p.OBSTACLES)));

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
