using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This Board is a square so it only has a size rather than a width/height
/// </summary>
public class SquareBoard : Board
{
    private int size; // the size of the board
    public int Size 
    { 
        get 
        { 
            return size; 
        } 
    }

    /// <summary>
    /// Constructor for a square board. It has a default size of 3.
    /// It uses the base constructor of board but sets width and height to size since they're the same.
    /// </summary>
    /// <param name="size">The given size for our square shaped board</param>
    public SquareBoard(int size = 3) : base(size,size)
    {
        this.size = size;
    }

    /// <summary>
    /// Constructor for the square board when there's already data to be loaded.
    /// Needs a size and tiles to load in
    /// </summary>
    /// <param name="tiles">The 2D array full of data loaded from the SaveManager</param>
    /// <param name="size">The size of the board</param>
    public SquareBoard(Tile[,] tiles, int size = 3) : base(tiles,size, size)
    {
        this.size = size;
    }

   
}
