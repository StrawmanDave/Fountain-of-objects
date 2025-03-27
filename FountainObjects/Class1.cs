namespace FountainObjects;

public class Maze
{
    //4by4 grid of rooms room (Row=0, Column=0) is the start and the end.
    public Room[,] rowColumn = new Room[,]{};
    public Room Current;
    public int x = 0;
    public int y = 0;
    public int Arrows = 5;
    public Maze()
    {
        rowColumn = new Room[4,4];
        setArray();
        rowColumn[0,0].sense = "You see light coming from the cavern entrance.";
        rowColumn[0,0].item = 'D';
        setFountainLocation();
        addPitLocation();
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

    public void setFountainLocation()
    {
        Random rand = new Random();
        int randomRow = 0;
        int randomColumn = 0;
        while(randomRow == 0 && randomColumn == 0)
        {
            randomRow = rand.Next(0,rowColumn.GetLength(0));
            randomColumn = rand.Next(0,rowColumn.GetLength(1));
        }
        Room containsFountain = new Room();
        containsFountain.sense = "You hear water dripping in this room. The Fountain of Objects is here!";
        containsFountain.item = 'F';
        rowColumn[randomRow,randomColumn] = containsFountain;
    }

    public void resetMaze()
    {
        for(int row = 0; row<rowColumn.GetLength(0); row ++)
        {
            for(int column = 0; column<rowColumn.GetLength(1); column++)
            {
                rowColumn[row,column].sense = " ";
                if(rowColumn[row,column].item == 'F')
                {
                    rowColumn[row,column].sense = "You hear water dripping in this room. The Fountain of Objects is here!";
                }else if(rowColumn[row,column].item == 'P')
                {
                    setSurroundings(row,column,"You feel a draft. There is a pit in a nearby room");
                }else if(rowColumn[row,column].item == 'M')
                {
                    setSurroundings(row,column, "You hear the growling and groaning of a maelstrom nearby.");
                }else if(rowColumn[row,column].item == 'A')
                {
                    setSurroundings(row,column,"You can smeel the rotten stench of an amarok in a nearby room");
                }else if(rowColumn[row,column].item == 'D')
                {
                    rowColumn[row,column].sense = "You see light coming from the cavern entrance.";
                }
            }
        }
    }

    public void shootArrow(int verticle, int horizontal)
    {
        if(Arrows > 0)
        {
            if(canMove(verticle,horizontal))
            {
                if(rowColumn[verticle,horizontal].item == 'M' || rowColumn[verticle,horizontal].item == 'A')
                {
                    rowColumn[verticle,horizontal].item = ' ';
                    clearSurroundings(verticle,horizontal);
                    resetMaze();
                }
            }
        }
        Arrows --;
    }

    public void shootNorth()
    {
        shootArrow(y-1,x);
    }

    public void shootSouth()
    {
        shootArrow(y+1,x);
    }

    public void shootEast()
    {
        shootArrow(y,x+1);
    }

    public void shootWest()
    {
        shootArrow(y,x+1);
    }

    public void addPitLocation()
    {
        Room pit = new Room();
        pit.item = 'P';
        Random rand = new Random();
        int randomRow;
        int randomColumn;
        do
        {
            //removeAllRooms(pit);
            randomRow = rand.Next(0,rowColumn.GetLength(0));
            randomColumn = rand.Next(0,rowColumn.GetLength(1));
        }while(canBePlaced(randomRow,randomColumn) == false);

        rowColumn[randomRow,randomColumn] = pit;
        setSurroundings(randomRow,randomColumn, "You feel a draft. There is a pit in a nearby room.");
    }

    public void addMaelstroms()
    {
        Room Maelstrom = new Room();
        Maelstrom.item = 'M';

        Random rand = new Random();
        int randomRow;;
        int randmColumn;
        do
        {
            randomRow = rand.Next(0,rowColumn.GetLength(0));
            randmColumn = rand.Next(0,rowColumn.GetLength(1));
        }while(canBePlaced(randomRow,randmColumn) == false);
        rowColumn[randomRow,randmColumn] = Maelstrom;
        setSurroundings(randomRow, randmColumn, "You hear the growling and groaning of a malestrom nearby.");
    }

    public void addAmaroks()
    {
        Room Amarok = new Room();
        Amarok.item = 'A';

        Random rand = new Random();
        int randomRow;
        int randomColumn;
        do
        {
            randomRow = rand.Next(0, rowColumn.GetLength(0));
            randomColumn = rand.Next(0, rowColumn.GetLength(1));
        }while(canBePlaced(randomRow,randomColumn) == false);

        rowColumn[randomRow,randomColumn] = Amarok;
        setSurroundings(randomRow,randomColumn, "You can smell the rotten stench of amarok in a nearby room.");
    }

    public Room Maelstrom()
    {
        Room Maelstrom = new Room();
        Maelstrom.item = 'M';
        return Maelstrom;
    }

    public void malestromEffect()
    {
        if(Current.item == 'M')
        {
            clearSurroundings(y,x);
            rowColumn[y,x].item = ' ';

            if(canMove(y+1, x-2) == true)
            {
                rowColumn[y+1, x-2] = Maelstrom();
                //setSurroundings(y+1,x-2, "You hear the growling and groaning of a malestrom nearby.");
            }else if(canMove(y+1,x -1) == true)
            {
                rowColumn[y+1, x-1] = Maelstrom();
                //setSurroundings(y+1,x-1, "You hear the growling and groaning of a malestrom nearby.");
            }else if(canMove(y+1, x))
            {
                rowColumn[y+1, x] = Maelstrom();
                //setSurroundings(y+1,x, "You hear the growling and groaning of a malestrom nearby.");
            }else if(canMove(y, x-2))
            {
                rowColumn[y, x-2] = Maelstrom();
                //setSurroundings(y,x-2, "You hear the growling and groaning of a malestrom nearby.");
            }else if(canMove(y,x-1))
            {
                rowColumn[y, x-1] = Maelstrom();
                //setSurroundings(y,x-1, "You hear the growling and groaning of a malestrom nearby.");
            }else
            {

            }
            resetMaze();

            moveNorth();
            moveEast();
            moveEast();
        }
    }

    public void clearSurroundings(int row, int column)
    {
        if(canMove(row -1, column) == true) // one south of the pit
        {
            rowColumn[row -1, column].sense = " ";
        }

        if(canMove(row +1, column) == true) // one North of the pit
        {
            rowColumn[row +1, column].sense = " ";
        }
    
        if(canMove(row, column +1) == true) // one east of the pit
        {
            rowColumn[row, column +1].sense = " ";
        }

        if(canMove(row, column -1) == true) // one west of the pit
        {
            rowColumn[row, column -1].sense = " ";
        }

        if(canMove(row -1, column -1) == true) // one south west of the pit
        {
            rowColumn[row -1, column -1].sense = " ";
        }

        if(canMove(row -1, column +1) == true) // one south east of the pit
        {
            rowColumn[row -1, column +1].sense = " ";
        }

        if(canMove(row +1, column -1) == true) // one north west of the pit
        {
            rowColumn[row +1, column -1].sense = " ";
        }

        if(canMove(row +1, column +1) == true) // one north east of the pit
        {
            rowColumn[row +1, column +1].sense = " ";
        }
    }
    public void setSurroundings(int row, int column, string words)
    {
        
        if(canMove(row -1, column) == true) // one south of the pit
        {
            if(rowColumn[row -1, column].sense == " ")
            {
                rowColumn[row -1, column].sense = words;
            }else
            {
                rowColumn[row -1, column].sense = rowColumn[row -1, column].sense + "\n" + words;
            }
        }

        if(canMove(row +1, column) == true) // one North of the pit
        {
            if(rowColumn[row +1, column].sense == " ")
            {
                rowColumn[row +1, column].sense = words;
            }else
            {
                rowColumn[row +1, column].sense = rowColumn[row +1, column].sense + "\n" + words;
            }
        }
    
        if(canMove(row, column +1) == true) // one east of the pit
        {
            if(rowColumn[row, column +1].sense == " ")
            {
                rowColumn[row, column +1].sense = words;
            }else
            {
                rowColumn[row, column +1].sense = rowColumn[row, column +1].sense + "\n" + words;
            }
        }

        if(canMove(row, column -1) == true) // one west of the pit
        {
            if(rowColumn[row, column -1].sense == " ")
            {
                rowColumn[row, column -1].sense = words;
            }else
            {
                rowColumn[row, column -1].sense = rowColumn[row, column -1].sense  + "\n" + words;
            }
        }

        if(canMove(row -1, column -1) == true) // one south west of the pit
        {
            if(rowColumn[row -1, column -1].sense == " ")
            {
                rowColumn[row -1, column -1].sense = words;
            }else
            {
                rowColumn[row -1, column -1].sense = rowColumn[row -1, column -1].sense + "\n" + words;
            }
        }

        if(canMove(row -1, column +1) == true) // one south east of the pit
        {
            if(rowColumn[row -1, column +1].sense == " ")
            {
                rowColumn[row -1, column +1].sense = words;
            }else
            {
                rowColumn[row -1, column +1].sense = rowColumn[row -1, column +1].sense + "\n" + words;
            }
        }

        if(canMove(row +1, column -1) == true) // one north west of the pit
        {
            if(rowColumn[row +1, column -1].sense == " ")
            {
                rowColumn[row +1, column -1].sense = words;
            }else
            {
                rowColumn[row +1, column -1].sense = rowColumn[row +1, column -1].sense + "\n" + words;
            }
        }

        if(canMove(row +1, column +1) == true) // one north east of the pit
        {
            if(rowColumn[row +1, column +1].sense == " ")
            {
                rowColumn[row +1, column +1].sense = words;
            }else
            {
                rowColumn[row +1, column +1].sense = rowColumn[row +1, column +1].sense + "\n" + words;
            }
        }
    }

    public void removeAllRoomTypes(char contains)
    {
        for(int row = 0; row< rowColumn.GetLength(0); row++)
        {
            for(int column = 0; column<rowColumn.GetLength(1); column++)
            {
                if(rowColumn[row,column].item == contains)
                {
                    rowColumn[row,column] = new Room();
                }
            }
        }
    }

    public bool canBePlaced(int row, int column)
    {
        if(rowColumn[row,column].item == 'F')
        {
            return false;
        }else if(rowColumn[row,column].item == 'D')
        {
            return false;
        }else if(rowColumn[row,column].item == 'P')
        {
            return false;
        }else if(rowColumn[row,column].item == 'M')
        {
            return false;
        }else if(rowColumn[row,column].item == 'A')
        {
            return false;
        }else
        {
            return true;
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

public class RandomTip
{
    Dictionary<int,string> listOfTips = new Dictionary<int,string>();

    public RandomTip()
    {
        listOfTips.Add(1,"You can enable the fountain by typing the command 'enable fountain'.");
        listOfTips.Add(2,"You can destroy maelstroms and amaroks with the command any of the 'shoot north,south,west,east' commands.");
        listOfTips.Add(3,"When the arrow count is 0 you can no longer shoot any arrows.");
        listOfTips.Add(4,"Watch out for pits.");
        listOfTips.Add(5,"Stay astray from amaroks");
    }

    public int getRandomtip()
    {
        Random rand = new Random();
        int tipNumber = rand.Next(1,5);
        return tipNumber;
    }

    public void displayTip()
    {
        Console.WriteLine(listOfTips[1]);
    }
}