//The player is told they can sense in the dark(see, hear, smell).
using System.Transactions;
using FountainObjects;

Maze fountainGame = new Maze();
bool win = false;
while(win == false)
{
    Console.WriteLine($"You are in (Row={fountainGame.y}, Column={fountainGame.x})");
    Console.WriteLine(fountainGame.Current.sense);
    if(fountainGame.Current.sense == "The Fountain of Objects has been reactivated, and you have escaped with your life!") // detect wins
    {
        win = true;
        continue;
    }

    Console.Write("What do you want to do?");



    string? command = Console.ReadLine();
    switch(command)
    {
        case "move north":
        fountainGame.moveNorth();
        break;
        case "move south":
        fountainGame.moveSouth();
        break;
        case "move east":
        fountainGame.moveEast();
        break;
        case "move west":
        fountainGame.moveWest();
        break;
        case "enable fountain":
            if(fountainGame.Current.item == 'F')
            {
                fountainGame.Current.sense = "You hear the rushing waters from the Fountain of Objects. It has been reactivated!";
                fountainGame.rowColumn[0,0].sense = "The Fountain of Objects has been reactivated, and you have escaped with your life!";
            }
        break;
    }
}
