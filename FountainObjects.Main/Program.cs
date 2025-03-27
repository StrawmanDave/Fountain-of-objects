//The player is told they can sense in the dark(see, hear, smell).
using FountainObjects;

string description = @"
You enter the Cavern of Objects, a maze of rooms filled with dangerous pits in search of the Fountain of Objects.
Light is visible only in the entrance, and no other light is seen anywhere in the caverns.
You must navigate the Caverns with your other senses.
Find the Fountain of Objects, activate it, and return to the entrance.
Look out for pits. You will feel a breeze if a pit is in an adjacent room. If you enter a room with a pit, you will die.
Maelstroms are violent forces of sentient wind. Entering a room with one could transport you to any other location in the caverns.
You will be able to hear thier growling and groaning in nearby rooms.
Amaroks roam the caverns. Encountering one is certain death, but you can smell their rotten stench in nearby rooms.
You carry with you a bow and a quiver of arrows. You can use them to shoot monsters in the caverns but be warned: you have a limited supply.
Enter help if you need any help
";

string help = @"
List of commands:
move north(moves you one up)
move south(moves you one down)
move east(moves you one to the left)
move west(moves you one to the right)
enable fountain(enables the fountain only if you are in the fountain room)
shoot north(shoots an arrow up one)
shoot south(shoots an arrow down one)
shoot east(shoots an arrow to your left)
shoot west(shoots an arrow to your right)
";

Console.Clear();
Console.WriteLine("Welcome to The Fountain of Objects game!");
Console.WriteLine("Would you like to play a small, medium, or large game?");
string? mazeSize = Console.ReadLine();
Console.WriteLine(description);

Console.BackgroundColor = ConsoleColor.Green;
Console.WriteLine("Press Any key to continue");
Console.BackgroundColor = ConsoleColor.Black;
Console.ReadKey(true);

switch(mazeSize)
{
    case "small":
    Console.Clear();
    Maze small = new Maze(4);
    small.addPitLocation();
    runGame(small);
    break;
    case "medium":
    Console.Clear();
    Maze medium = new Maze(6);
    medium.addPitLocation();
    medium.addPitLocation();
    medium.addMaelstroms();
    medium.addAmaroks();
    medium.addAmaroks();
    runGame(medium);
    break;
    case "large":
    Console.Clear();
    Maze large = new Maze(6);
    large.addPitLocation();
    large.addPitLocation();
    large.addPitLocation();
    large.addPitLocation();
    large.addMaelstroms();
    large.addMaelstroms();
    large.addAmaroks();
    large.addAmaroks();
    large.addAmaroks();
    runGame(large);
    break;
}


void runGame(Maze chosenMaze)
{
    bool win = false;
    while(win == false)
    {
        chosenMaze.DisplayMaze();

        if(chosenMaze.Current.item == 'P')
        {
            Console.WriteLine("You fell in a pit and died.");
            break;
        }

        if(chosenMaze.Current.item == 'A')
        {
            Console.WriteLine("You ran into a amarok you instantly died");
            break;
        }
        Console.WriteLine($"You are in (Row={chosenMaze.y}, Column={chosenMaze.x}) Arrow count is {chosenMaze.Arrows}");
        Console.WriteLine(chosenMaze.Current.sense);
        
        if(chosenMaze.Current.sense == "The Fountain of Objects has been reactivated, and you have escaped with your life!") // detect wins
        {
            win = true;
            Console.WriteLine("You Win!");
            continue;
        }

        if(chosenMaze.Current.item == 'M')
        {
            chosenMaze.maelstromEffect();
            Console.WriteLine("You ran into a malestrom and have been moved");
            continue;
        }

        Console.Write("What do you want to do?");

        string? command = Console.ReadLine();
        switch(command)
        {
            case "move north":
            chosenMaze.moveNorth();
            break;
            case "move south":
            chosenMaze.moveSouth();
            break;
            case "move east":
            chosenMaze.moveEast();
            break;
            case "move west":
            chosenMaze.moveWest();
            break;
            case "enable fountain":
                if(chosenMaze.Current.item == 'F')
                {
                    chosenMaze.Current.sense = "You hear the rushing waters from the Fountain of Objects. It has been reactivated!";
                    chosenMaze.rowColumn[0,0].sense = "The Fountain of Objects has been reactivated, and you have escaped with your life!";
                }
            break;
            case"shoot north":
            chosenMaze.shootNorth();
            break;
            case"shoot south":
            chosenMaze.shootSouth();
            break;
            case"shoot east":
            chosenMaze.shootEast();
            break;
            case"shoot west":
            chosenMaze.shootWest();
            break;
            case "help":
            Console.WriteLine(help);
            break;
        }
    }
}