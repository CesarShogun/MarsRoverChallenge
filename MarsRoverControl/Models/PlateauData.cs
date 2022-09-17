using System;

namespace MarsRoverControl.Models
{
    public class PlateauData
    {
        public readonly string NAME;
        public readonly int WIDTH;
        public readonly int HEIGHT;
        public readonly List<Coords> OBSTACLES = new();


        public PlateauData(string name, int width, int height, List<Coords> obstacles)
        {
            NAME = name;
            WIDTH = width;
            HEIGHT = height;
            OBSTACLES = obstacles;
        }
    }
}
