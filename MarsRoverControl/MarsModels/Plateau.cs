using MarsRoverControl.Models;

namespace MarsRoverControl.MarsModels
{
    public class Plateau : PlateauData
    {
        private List<RoverInMars> rovers = new();
        
        public Plateau(string name, Coords limits) : base(name, limits)
        {
        }

        public Coords GetMyCoordinates(MarsRover rover)
        {
            return getRover(rover).Coords;
        }

        public MarsEvent MoveThrough(MarsRover rover)
        {
            var roverIndex = indexOfRover(rover);
            MarsEvent marsEvent = new();
            marsEvent.Event = Event.NoEvent;

            switch (rovers[roverIndex].Rover.Direction)
            {
                case 'N':
                    if (rovers[roverIndex].Coords.Y + 1 > HEIGHT)
                        marsEvent.Event = Event.Edge;
                    else
                        rovers[roverIndex].Coords.Y++;
                    break;
                case 'E':
                    if (rovers[roverIndex].Coords.X + 1 > HEIGHT)
                        marsEvent.Event = Event.Edge;
                    else
                        rovers[roverIndex].Coords.X++;
                    break;
                case 'S':
                    if (rovers[roverIndex].Coords.Y - 1 < 0)
                        marsEvent.Event = Event.Edge;
                    else
                        rovers[roverIndex].Coords.Y--;
                    break;
                case 'W':
                    if (rovers[roverIndex].Coords.X - 1 < 0)
                        marsEvent.Event = Event.Edge;
                    else
                        rovers[roverIndex].Coords.X--;
                    break;
            }

            marsEvent.Coords = rovers[roverIndex].Coords;

            return marsEvent;
        }

        public void AddRoverOn(MarsRover rover, Coords coords)
        {
            if (coords.FurtherThan(LIMITS))
                throw new Exception("COORD ERROR: The coords are out of bounds of the specified plateau dimensions.");
            
            rover.SetPlateau(this);
            rovers.Add(new RoverInMars(rover, coords));
        }

        //------- PRIVATES --------
        private RoverInMars getRover(MarsRover rover)
        {
            foreach (var r in rovers)
            {
                if (r.Rover.Name == rover.Name)
                    return r;
            }

            throw new NotImplementedException();
        }

        private int indexOfRover(MarsRover rover)
        {
            for (var i = 0; i < rovers.Count; i++)
            {
                if (rovers[i].Rover.Name == rover.Name)
                    return i;
            }

            return -1;
        }

        private class RoverInMars
        {
            public MarsRover Rover { get; }
            public Coords Coords;

            public RoverInMars(MarsRover rover, Coords coords)
            {
                Rover = rover;
                Coords = coords;
            }
        }
    }
}
