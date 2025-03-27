//The player is told they can sense in the dark(see, hear, smell).
using FountainObjects;

Console.Clear();
Console.WriteLine("Welcome to The Fountain of Objects game!");
Console.WriteLine("Would you like to play a small, medium, or large game?");
string? mazeSize = Console.ReadLine();
Console.WriteLine("This is maze with unnatrual darkness you are relying on your sense of smell and hearing to determine what room you are in.");
Console.WriteLine("To move around you can use any of the 'move north,south,east,west' commands");
RandomTip tip = new RandomTip();
tip.displayTip();
Console.WriteLine("Press any key on the keyboard to continue");
Console.ReadKey(true);


switch(mazeSize)
{
    case "small":
    Maze small = new Maze(4);
    small.addPitLocation();
    runGame(small);
    break;
    case "medium":
    Maze medium = new Maze(6);
    medium.addPitLocation();
    medium.addPitLocation();
    medium.addMaelstroms();
    medium.addAmaroks();
    medium.addAmaroks();
    runGame(medium);
    break;
    case "large":
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
        Console.Clear();
        if(chosenMaze.Current.sense == "The Fountain of Objects has been reactivated, and you have escaped with your life!") // detect wins
        {
            win = true;
            continue;
        }

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

        if(chosenMaze.Current.item == 'M')
        {
            chosenMaze.malestromEffect();
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
        }
    }
}