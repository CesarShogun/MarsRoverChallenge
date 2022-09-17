using NUnit.Framework;
using FluentAssertions;
using MarsRoverControl.Models;

namespace MarsRoverTests
{
    public class Tests
    {
        private const string PLATEAU = "Endeavour";

        public void CreateRoverAndLand(string roverName)
        {
            MissionControl.CreateMarsRover(roverName);
            MissionControl.GetRover(roverName).LandOnPlateau(PLATEAU, new Coords(3, 0));
        }
        
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Testing_Letters_Only_In_Names()
        {
            var ex = Assert.Throws<Exception>(() => MissionControl.CreateMarsRover("Mars100%Rover"));
            Assert.That(ex.Message, Is.EqualTo("Rover name not allowed. A name can only contain letters."));
        }

        [Test]
        public void Testing_Correct_Plateau_Name()
        {
            MissionControl.CreateMarsRover("Plateau");
            var ex = Assert.Throws<Exception>(() => MissionControl.GetRover("Plateau").LandOnPlateau("Incorrect", new Coords(1, 1)));
            Assert.That(ex.Message, Is.EqualTo("ERROR: No plateau was found under the name \"Incorrect\"."));
        }

        [Test]
        public void Testing_Correct_Coords()
        {
            string roverName = "Positive";
            MissionControl.CreateMarsRover(roverName);

            var ex = Assert.Throws<ArgumentException>(() => MissionControl.GetRover(roverName).LandOnPlateau(PLATEAU, new Coords(1, -1)));
            Assert.That(ex.Message, Is.EqualTo("COORD ERROR: No coord can be lower than zero."));

            var exx = Assert.Throws<Exception>(() => MissionControl.GetRover(roverName).LandOnPlateau(PLATEAU, new Coords(1, 21)));
            Assert.That(exx.Message, Is.EqualTo("COORD ERROR: The coords are out of bounds of the specified plateau dimensions."));
        }

        [Test]
        public void Testing_For_Launching_Of_Launched_Rover()
        {
            string roverName = "Launchtest";
            CreateRoverAndLand(roverName);
            var ex = Assert.Throws<Exception>(() => MissionControl.GetRover(roverName).LandOnPlateau(PLATEAU, new Coords(1, 1)));
            Assert.That(ex.Message, Is.EqualTo($"ERROR: The {roverName} rover is already on Mars."));
        }

        [Test]
        public void Testing_Correct_Movement_Instructions()
        {
            string roverName = "Instructions";
            MissionControl.CreateMarsRover(roverName);

            var ex = Assert.Throws<Exception>(() => MissionControl.GetRover(roverName).MovementInstructions("LML"));
            Assert.That(ex.Message, Is.EqualTo("ERROR: The rover is not on Mars."));

            MissionControl.GetRover(roverName).LandOnPlateau(PLATEAU, new Coords(1, 1));

            ex = Assert.Throws<ArgumentException>(() => MissionControl.GetRover(roverName).MovementInstructions("LMLX"));
            Assert.That(ex.Message, Is.EqualTo($"INSTRUCTION ERROR: The instructions for the movement of the rover can only be \'{MarsRover.LEFT_TURN}\', \'{MarsRover.RIGHT_TURN}\' and \'{MarsRover.MOVE}\'."));
        }

        [Test]
        public void Testing_Correct_Coordinates()
        {
            string roverName = "Coords";
            CreateRoverAndLand(roverName);

            MissionControl.GetRover(roverName).Coordinates().Should().Be(new Coords(3, 0));
        }

        [Test]
        public void Testing_Turns()
        {
            string roverName = "Turns";
            CreateRoverAndLand(roverName);

            MissionControl.GetRover(roverName).MovementInstructions("LLL");
            MissionControl.GetRover(roverName).Direction.Should().Be('E');

            MissionControl.GetRover(roverName).MovementInstructions("LL");
            MissionControl.GetRover(roverName).Direction.Should().Be('W');

            MissionControl.GetRover(roverName).MovementInstructions("R");
            MissionControl.GetRover(roverName).Direction.Should().Be('N');
        }

        [Test]
        public void Testing_Movement()
        {
            string roverName = "Movement";
            CreateRoverAndLand(roverName);

            MissionControl.GetRover(roverName).MovementInstructions("MMRM").Should().Be(new MarsEvent() { Event = Event.NoEvent, Coords = new Coords(4, 2) });
            MissionControl.GetRover(roverName).Direction.Should().Be('E');

            MissionControl.GetRover(roverName).MovementInstructions("LLMLMMRR").Should().Be(new MarsEvent() { Event = Event.NoEvent, Coords = new Coords(3, 0) });
            MissionControl.GetRover(roverName).Direction.Should().Be('N');
        }

        [Test]
        public void Testing_Correct_Edge_Of_Plateau_Detection()
        {
            string roverName = "Edge";
            CreateRoverAndLand(roverName);

            MissionControl.GetRover(roverName).MovementInstructions("LMLMM").Should().Be(new MarsEvent() { Event = Event.Edge, Coords = new Coords(2, 0) });
            MissionControl.GetRover(roverName).Direction.Should().Be('S');

            MissionControl.GetRover(roverName).MovementInstructions("RMMMMR").Should().Be(new MarsEvent() { Event = Event.Edge, Coords = new Coords(0, 0) });
            MissionControl.GetRover(roverName).Direction.Should().Be('W');

            MissionControl.GetRover(roverName).MovementInstructions("RMMRMLMMMMM").Should().Be(new MarsEvent() { Event = Event.Edge, Coords = new Coords(1, 5) });
            MissionControl.GetRover(roverName).Direction.Should().Be('N');

            MissionControl.GetRover(roverName).MovementInstructions("RRMMLMMMMMMLM").Should().Be(new MarsEvent() { Event = Event.Edge, Coords = new Coords(5, 3) });
            MissionControl.GetRover(roverName).Direction.Should().Be('E');

            MissionControl.GetRover(roverName).MovementInstructions("RMMMRMMR").Should().Be(new MarsEvent() { Event = Event.NoEvent, Coords = new Coords(3, 0) });
            MissionControl.GetRover(roverName).Direction.Should().Be('N');

        }
    }
}