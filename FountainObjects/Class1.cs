﻿namespace FountainObjects;

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
        rowColumn[0,2].sense = "You hear water dripping in this room. The Fountain of Objects is here!";
        rowColumn[0,2].item = 'F';
        Current = rowColumn[y,x];
    }
    public Maze(int size)
    {
        rowColumn = new Room[size,size];
        setArray();
        rowColumn[0,0].sense = "You see light coming from the cavern entrance.";
        rowColumn[0,0].item = 'D';
        rowColumn[0,2].sense = "You hear water dripping in this room. The Fountain of Objects is here!";
        rowColumn[0,2].item = 'F';
        Current = rowColumn[y,x];
    }
    public Maze(int row, int column)
    {
        rowColumn = new Room[row,column];
        setArray();
        rowColumn[0,0].sense = "You see light coming from the cavern entrance.";
        rowColumn[0,0].item = 'D';
        rowColumn[0,2].sense = "You hear water dripping in this room. The Fountain of Objects is here!";
        rowColumn[0,2].item = 'F';
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

    public bool canMove(int row, int column)
    {
        if(row > rowColumn.GetLength(0) || row < 0)
        {
            return false;
        }

        if(column > rowColumn.GetLength(1) || column < 0)
        {
            return false;
        }
        return true;
    }

    public void moveNorth()
    {
        if(canMove(y+1, x) == true) //current.Row current.column
        {
            y++;
            Current = rowColumn[y,x];
        }
    }
    public void moveSouth()
    {
        if(canMove(y-1, x) == true) //current.Row current.column
        {
            y--;
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