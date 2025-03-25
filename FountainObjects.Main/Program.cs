//The player is told they can sense in the dark(see, hear, smell).
using FountainObjects;


Console.WriteLine("Would you like to play a small, medium, or large game?");
string? mazeSize = Console.ReadLine();
switch(mazeSize)
{
    case "small":
    Maze small = new Maze();
    runGame(small);
    break;
    case "medium":
    Maze medium = new Maze(6);
    runGame(medium);
    break;
    case "large":
    Maze large = new Maze(6);
    runGame(large);
    break;
}


void runGame(Maze chosenMaze)
{
    bool win = false;
    while(win == false)
    {
        Console.WriteLine($"You are in (Row={chosenMaze.y}, Column={chosenMaze.x})");
        Console.WriteLine(chosenMaze.Current.sense);
        if(chosenMaze.Current.sense == "The Fountain of Objects has been reactivated, and you have escaped with your life!") // detect wins
        {
            win = true;
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
        }
    }
}

