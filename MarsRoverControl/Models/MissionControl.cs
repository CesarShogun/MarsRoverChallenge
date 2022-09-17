namespace MarsRoverControl.Models
{
    public static class MissionControl
    {
        private static List<MarsRover> marsRovers = new();

        public static void CreateMarsRover(string name)
        {
            try
            {
                GetRover(name);
            }
            catch
            {
                marsRovers.Add(new MarsRover(name));
                return;
            }

            throw new Exception($"ERROR: Already exists a rover by the name \"{name}\". Every Rover has to have a distinct name.");
        }

        public static MarsRover GetRover(string name)
        {
            foreach (MarsRover mr in marsRovers)
            {
                if (mr.Name == name)
                    return mr;
            }

            throw new Exception($"ERROR: No rover found with the name \"{name}\".");
        }

        public static List<MarsRover> GetAllRovers()
        {
            return marsRovers;
        }
    }
}
