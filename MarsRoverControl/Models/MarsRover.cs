using MarsRoverControl.MarsModels;
using System;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace MarsRoverControl.Models
{
    public class MarsRover
    {
        public const char LEFT_TURN = 'L';
        public const char RIGHT_TURN = 'R';
        public const char MOVE = 'M';

        public readonly string Name;
        private Plateau? plateau = null;
        private readonly RoverDirection direction = new();

        public MarsRover(string name)
        {
            if (Regex.IsMatch(name, @"^[a-zA-Z]+$"))
                Name = name;
            else
                throw new Exception("Rover name not allowed. A name can only contain letters.");
        }

        public void LandOnPlateau(string plateauName, Coords coords)
        {
            if (plateau != null)
                throw new Exception($"ERROR: The {Name} rover is already on Mars.");

            Mars.LandRoverOn(this, plateauName, coords);
        }

        public Coords Coordinates()
        {
            if (plateau == null)
                throw new Exception("ERROR: The rover is not on Mars.");
            
            return plateau.GetMyCoordinates(this);
        }

        public char Direction
        {
            get { return direction.Direction; }
        }

        public MarsEvent MovementInstructions(string instructions)
        {
            if (plateau == null)
                throw new Exception("ERROR: The rover is not on Mars.");

            foreach (char c in instructions)
            {
                if (c != LEFT_TURN && c != RIGHT_TURN && c != MOVE)
                    throw new ArgumentException($"INSTRUCTION ERROR: The instructions for the movement of the rover can only be \'{LEFT_TURN}\', \'{RIGHT_TURN}\' and \'{MOVE}\'.");
            }

            foreach (char c in instructions)
            {
                switch (c)
                {
                    case LEFT_TURN:
                        direction.TurnLeft();
                        break;
                    case RIGHT_TURN:
                        direction.TurnRight();
                        break;
                    case MOVE:
                        var marsEvent = plateau.MoveThrough(this);
                        if (marsEvent.Event != Event.NoEvent)
                            return marsEvent;
                        break;
                    default:
                        break;
                }
            }

            return new MarsEvent() { Event = Event.NoEvent, Coords = Coordinates() };
        }


        public void SetPlateau(Plateau plateau)
        {
            this.plateau = plateau;
        }

        public string GetPlateau()
        {
            if (plateau == null)
                throw new Exception("ERROR: The rover is not on Mars.");

            return plateau.NAME;
        }

        public void SayHi()
        {
            if (plateau != null)
                Console.WriteLine($"Hi, my name is {Name} and I am in Mars, in a place named {plateau.NAME}.");
        }

        //------ PRIVATE CLASS -----------
        private class RoverDirection
        {
            //direction can only have 4 values, from 0 to 3. 0 is North, 1 is East, 2 is South, 3 is West.
            private int direction = 0;

            public void TurnRight()
            {
                direction++;
                if (direction == 4)
                    direction = 0;
            }

            public void TurnLeft()
            {
                direction--;
                if (direction == -1)
                    direction = 3;
            }

            public char Direction
            {
                get
                {
                    switch (direction)
                    {
                        case 0:
                            return 'N';
                        case 1:
                            return 'E';
                        case 2:
                            return 'S';
                        case 3:
                            return 'W';
                        default:
                            break;
                    }

                    throw new NotImplementedException();
                }
            }
        }
    }
}
