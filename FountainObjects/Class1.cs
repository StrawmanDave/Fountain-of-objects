namespace FountainObjects;

public class Maze
{
    //4by4 grid of rooms room (Row=0, Column=0) is the start and the end.
    //room(Row=0,Column=2) is the fountain room
    public Room[,] rowColumn = new Room[,]{};
    public Room Current;
    public int x = 0;
    public int y = 0;
    public Maze()
    {
        rowColumn = new Room[4,4];
        setArray();
        rowColumn[0,0].sense = "You see light coming from the cavern entrance.";
        rowColumn[0,0].item = 'D';
        setFountainLocation();
        setPitLocation();
        Current = rowColumn[y,x];
    }
    public Maze(int size)
    {
        rowColumn = new Room[size,size];
        setArray();
        rowColumn[0,0].sense = "You see light coming from the cavern entrance.";
        rowColumn[0,0].item = 'D';
        setFountainLocation();
        Current = rowColumn[y,x];
    }
    public Maze(int row, int column)
    {
        rowColumn = new Room[row,column];
        setArray();
        rowColumn[0,0].sense = "You see light coming from the cavern entrance.";
        rowColumn[0,0].item = 'D';
        setFountainLocation();
        Current = rowColumn[y,x];
    }

    public void setArray()
    {
        for(int row = 0; row< rowColumn.GetLength(0); row++)
        {
            for(int column = 0; column<rowColumn.GetLength(1); column++)
            {
                rowColumn[row,column] =  new Room();
            }
        }
    }

    public void DisplayMaze()
    {
        for(int row = 0; row< rowColumn.GetLength(0); row++)
        {
            for(int column = 0; column<rowColumn.GetLength(1); column++)
            {
                Console.Write(rowColumn[row,column].item);
            }
            Console.WriteLine();
        }
    }

    public void setFountainLocation()
    {
        Random rand = new Random();
        int randomRow = 0;
        int randomColumn = 0;
        while(randomRow == 0 && randomColumn == 0)
        {
            randomRow = rand.Next(0,4);
            randomColumn = rand.Next(0,4);
        }
        Room containsFountain = new Room();
        containsFountain.sense = "You hear water dripping in this room. The Fountain of Objects is here!";
        containsFountain.item = 'F';
        rowColumn[randomRow,randomColumn] = containsFountain;
    }

    public void setPitLocation()
    {
        Room pit = new Room();
        pit.item = 'P';
        Random rand = new Random();
        int newRandom = rand.Next(0,4);
        if(rowColumn[newRandom,newRandom].item == 'F' || newRandom == 0 || rowColumn[newRandom,newRandom].item == 'P')
        {
            newRandom = rand.Next(0,4);
            rowColumn[newRandom,newRandom] = pit;
        }else
        {
            rowColumn[newRandom,newRandom] = pit;
        }

        //Then set the surrounding Rooms sense to display a pit in a nearby room
        if(canMove(newRandom -1, newRandom) == true) // one south of the pit
        {
            rowColumn[newRandom -1,newRandom].sense = "You feel a draft. There is a pit in a nearby room.";
        }

        if(canMove(newRandom +1, newRandom) == true) // one North of the pit
        {
            rowColumn[newRandom +1,newRandom].sense = "You feel a draft. There is a pit in a nearby room.";
        }
    
        if(canMove(newRandom, newRandom +1) == true) // one east of the pit
        {
            rowColumn[newRandom,newRandom +1].sense = "You feel a draft. There is a pit in a nearby room.";
        }

        if(canMove(newRandom, newRandom -1) == true) // one west of the pit
        {
            rowColumn[newRandom,newRandom -1].sense = "You feel a draft. There is a pit in a nearby room.";
        }

        if(canMove(newRandom -1, newRandom -1) == true) // one south west of the pit
        {
            rowColumn[newRandom -1,newRandom -1].sense = "You feel a draft. There is a pit in a nearby room.";
        }

        if(canMove(newRandom -1, newRandom +1) == true) // one south east of the pit
        {
            rowColumn[newRandom -1,newRandom +1].sense = "You feel a draft. There is a pit in a nearby room.";
        }

        if(canMove(newRandom +1, newRandom -1) == true) // one north west of the pit
        {
            rowColumn[newRandom +1 ,newRandom -1].sense = "You feel a draft. There is a pit in a nearby room.";
        }

        if(canMove(newRandom +1, newRandom +1) == true) // one north east of the pit
        {
            rowColumn[newRandom +1,newRandom +1].sense = "You feel a draft. There is a pit in a nearby room.";
        }
    }

    public bool canMove(int row, int column)
    {
        if(row > rowColumn.GetLength(0) -1 || row < 0)
        {
            return false;
        }

        if(column > rowColumn.GetLength(1) -1 || column < 0)
        {
            return false;
        }
        return true;
    }

    public void moveNorth()
    {
        if(canMove(y-1, x) == true) //current.Row current.column
        {
            y--;
            Current = rowColumn[y,x];
        }
    }
    public void moveSouth()
    {
        if(canMove(y+1, x) == true) //current.Row current.column
        {
            y++;
            Current = rowColumn[y,x];
        }        
    }

    public void moveWest()
    {
        if(canMove(y, x-1) == true) //current.Row current.column
        {
            x--;
            Current = rowColumn[y,x];
        }
    }

    public void moveEast()
    {
        if(canMove(y, x+1) == true) //current.Row current.column
        {
            x++;
            Current = rowColumn[y,x];
        }
    }
}

public class Room
{
    //What defines a room?
    //There is walls and there is what is inside it
    //You can sense what is in the room so we will have string sense that will later print out depending on what room you are in.
    public string? sense = null;
    public char item;
    public Room()
    {
        sense = " ";
        item = ' ';
    }
}