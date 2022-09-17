using MarsRoverControl.MarsModels;

namespace MarsRoverControl.Models
{
    public static class MarsRoverConsole
    {
        public static void InitConsole()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            printLogo();
            string? line = "";
            while (line != "exit")
            {
                Console.Write(">");
                line = Console.ReadLine();
                commandListener(line);
            }
        }

        private static void commandListener(string? line)
        {
            if (line == null || line == "")
                return;

            string[] commands = line.Split(' ');

            if (commands.Length > 0)
            {
                switch (commands[0])
                {
                    case "plateaus":
                        plateausCommand();
                        break;
                    case "survey":
                        surveyCommand(commands);
                        break;
                    case "rovers":
                        roversCommand();
                        break;
                    case "construct":
                        constructCommand(commands);
                        break;
                    case "land":
                        landCommand(commands);
                        break;
                    case "coords":
                        coordsCommand(commands);
                        break;
                    case "move":
                        moveCommand(commands);
                        break;
                    case "help":
                        helpCommand();
                        break;
                    case "clear":
                        clearCommand();
                        break;
                    case "exit":
                        break;
                    default:
                        Console.WriteLine($"\"{commands[0]}\" is not a valid command. See \"help\".");
                        break;
                }
            }
        }

        private static void printLogo()
        {
            Console.WriteLine(
                "                            \r\n" +
                "    _   _    _    ____    _    \r\n" +
                "   | \\ | |  / \\  / ___|  / \\   \r\n" +
                "   |  \\| | / _ \\ \\___ \\ / _ \\  \r\n" +
                "   | |\\  |/ ___ \\ ___) / ___ \\ \r\n" +
                "   |_| \\_/_/   \\_|____/_/   \\_\\\r\n");

            Console.WriteLine("__________________________________\n");
            Console.WriteLine("Nasa Mars Rover Shell v4.2");
            Console.WriteLine("Copyright (c) Rob-Co Corp.");
            Console.WriteLine("__________________________________\n");
            Console.WriteLine("Welcome to the MARS ROVER CONTROL SHELL.\n" +
            "Type \"exit\" if you want to exit the program. Type \"clear\" to clean the screen. Type \"help\" if you want to see the list of possible commands.\n");
        }

        private static void plateausCommand()
        {
            var plateaus = Mars.GetPlateaus();
            Console.WriteLine("The plateaus found in Mars are named: \r");
            foreach (var p in plateaus)
            {
                Console.WriteLine($"\t{p.NAME}");
            }
        }

        private static void surveyCommand(string[] commands)
        {
            if (commands.Length > 1)
            {
                int plateauIndex;
                try
                {
                    plateauIndex = Mars.PlateauIndexOf(MissionControl.GetRover(commands[1]).GetPlateau());
                }
                catch
                {
                    try
                    {
                        plateauIndex = Mars.PlateauIndexOf(commands[1]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return;
                    }
                }
                if (plateauIndex != -1)
                {
                    Console.WriteLine($"The {Mars.PLATEAUS[plateauIndex].NAME} plateau has {Mars.PLATEAUS[plateauIndex].WIDTH} units to the East and {Mars.PLATEAUS[plateauIndex].HEIGHT} to the North from the specified zero point.");
                }
                else
                {
                    Console.WriteLine("ERROR: The plateau was not found.");
                }
            }
            else
                Console.WriteLine($"No name was especified.");
        }

        private static void roversCommand()
        {
            var rovers = MissionControl.GetAllRovers();
            Console.WriteLine("The following rovers are found in our database: \r");
            foreach (var p in rovers)
            {
                Console.WriteLine($"\t{p.Name}");
            }
        }

        private static void constructCommand(string[] commands)
        {
            if (commands.Length > 1)
            {
                try
                {
                    MissionControl.CreateMarsRover(commands[1]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }

                Console.WriteLine("THE CONSTRUCTION OF A NEW ROVER HAS INITIATED:");
                doneDelay("Gathering the materials for the construction... ", 2000);
                doneDelay("Issuing the templates to the assembly bots... ", 1500);
                doneDelay("Assembling the rover... ", 4000);
                doneDelay("Testing all systems... ", 2500);
                Console.WriteLine($"ALL TESTS PASEED: A new Mars Rover named {commands[1]} was constructed.");
            }
            else
                Console.WriteLine($"No name was especified. No rover was constructed.");
        }

        private static void landCommand(string[] commands)
        {
            if (commands.Length > 4)
            {
                try
                {
                    MissionControl.GetRover(commands[1]).LandOnPlateau(commands[2], new Coords(Int32.Parse(commands[3]), Int32.Parse(commands[4])));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message} The rover was not launched.");
                    return;
                }
                Console.WriteLine("INITIATING THE MARS ROVER LANDING PROTOCOL:");
                doneDelay("Testing launching pad systems... ", 2500);
                doneDelay("Filling up the fuel... ", 2500);
                doneDelay("Loading the rover in the launch pod... ", 5000);
                doneDelay("Calculating re-enter angle... ", 2500);
                doneDelay("Launching 5 seconds countdown... ", 5000);
                doneDelay("Launch! ", 1000);
                doneDelay("Waiting for communications from the rover... ", 5000);
                doneDelay("Receiving Communications... ", 3000);
                doneDelay("Testing mars rover systems... ", 2000);
                Console.WriteLine($"ALL TESTS PASSED: The Mars Rover has landed successfully!");
            }
            else
            {
                Console.WriteLine("Not all parameters for the launch where specified. No rover was launched.");
            }
        }

        private static void coordsCommand(string[] commands)
        {
            if (commands.Length > 1)
            {
                Coords coords;
                char direction = ' ';
                try
                {
                    coords = MissionControl.GetRover(commands[1]).Coordinates();
                    if (commands.Length > 2 && commands[2] == "-d")
                        direction = MissionControl.GetRover(commands[1]).Direction;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
                Console.WriteLine("COMMUNICATION LINE OPENED:");
                doneDelay("Establishing link... ", 1500);
                doneDelay("Receiving data... ", 2500);

                string output = $"VALID DATA RECEIVED: {commands[1]} rover's coordinates in units from the established point zero:\n" +
                    $"\t{coords.X} units East\n" +
                    $"\t{coords.Y} units North\n";
                if (direction != ' ')
                {
                    switch (direction)
                    {
                        case 'N':
                            output += "\tFacing North";
                            break;
                        case 'E':
                            output += "\tFacing East";
                            break;
                        case 'S':
                            output += "\tFacing South";
                            break;
                        case 'W':
                            output += "\tFacing West";
                            break;
                    }
                }
                Console.WriteLine(output);

            }
            else
                Console.WriteLine("No rover was especified.");
        }

        private static void moveCommand(string[] commands)
        {
            if (commands.Length > 2)
            {
                MarsEvent marsEvent;
                try
                {
                    marsEvent = MissionControl.GetRover(commands[1]).MovementInstructions(commands[2]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }

                Console.WriteLine("INITIATING AUTOMATIC MOVEMENT:");
                doneDelay("Communicating with mars rover... ", 3000);
                doneDelay("Receiving verification of instructions... ", 1500);
                doneDelay("Moving through Mars... ", 5000);
                doneDelay("Receiving movement mission results... ", 1500);

                if (marsEvent.Event == Event.NoEvent)
                    Console.WriteLine("RESULTS RECEIVED: The movement was carried out successfully. No unexpected events occurred.");
                else if (marsEvent.Event == Event.Edge)
                    Console.WriteLine("RESULTS RECEIVED: DANGER, the rover was instructed to run into the edge of the specified area. The rover has cancelled further movment instructions.");
            }
            else
            {
                Console.WriteLine("Not all parameters for the movement of the rover where specified. The rover will not move.");
            }
        }

        private static void clearCommand()
        {
            Console.Clear();
            printLogo();
        }

        private static void helpCommand()
        {
            Console.WriteLine("COMMAND LIST:");
            Console.WriteLine("\tclear: Clears the screen for redability.\n");
            Console.WriteLine("\tconstruct <name>: Construct a rover with the given name. No duplicates are allowed.\n");
            Console.WriteLine("\tcoords <rover> [-d]: Prints the coords of the rover. -d also prints the direction it's facing.\n");
            Console.WriteLine("\texit: Exits the console.");
            Console.WriteLine("\tland <rover> <plateau> <x coord> <y coord>: Lands a rover on a plateau, on the specified coordinates.\n");
            Console.WriteLine("\tplateaus: Prints a list of the plateau names that are discovered.\n");
            Console.WriteLine("\tmove <rover> <instructions>: Gives a set of instructions to the rover to complete, no spaces between. \'L\' turn left, \'R\' to turn right, \'M\' to move fordward.\n");
            Console.WriteLine("\trovers: Prints a list of the constructed rovers.\n");
            Console.WriteLine("\tsurvey <rover or plateau>: Gives the size of an existing plateau. <name> can be the name of a plateau or a rover.\n");
        }

        private static void doneDelay(string task, int milliseconds)
        {
            Console.Write($"\t> {task}");
            System.Threading.Thread.Sleep(milliseconds);
            Console.Write("[DONE]\n");
        }
    }
}
