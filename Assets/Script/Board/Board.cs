using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

/// <summary>
/// The Board is a class made up of Tile objects.
/// The Board can initialize the tiles, give tiles to other scripts, and reset the tiles (thus resetting the Board)
/// </summary>
public class Board
{
    // width and height shouldn't be edited by any other scripts during the game
    protected int width;
    protected int height;

    public int Width
    {
        get 
        { 
            return width; 
        }
    }

    public int Height
    { 
        get 
        { 
            return height; 
        } 
    }

    protected Tile[,] tiles;

    /// <summary>
    /// Returns a tile based on the given x and y values
    /// </summary>
    /// <param name="x">The x location of the tile</param>
    /// <param name="y">The y location of the tile</param>
    /// <returns>The tile at the given location</returns>
    public virtual Tile GetTile(int x, int y)
    {
        return tiles[x, y];
    }

    /// <summary>
    /// Returns the entire 2D array of tiles
    /// </summary>
    /// <returns>returns the tiles in the board</returns>
    public virtual Tile[,] GetTiles()
    {
        return tiles;
    }

    /// <summary>
    /// Constructor for the board. 
    /// Needs a width and height so that it can initialize all the tiles
    /// </summary>
    /// <param name="width">The width of the board</param>
    /// <param name="height">The height of the board</param>
    public Board(int width, int height) 
    {
        this.width = width;
        this.height = height;

        tiles = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[x, y] = new Tile();
                tiles[x, y].SetLocation(x, y);
            }
        }
    }

    /// <summary>
    /// Constructor for the board when there's already data to be loaded.
    /// Needs width, height, and the set of tiles to load in.
    /// </summary>
    /// <param name="tiles">The 2D array full of data loaded from the SaveManager</param>
    /// <param name="width">The width of the board</param>
    /// <param name="height">The height of the board</param>
    public Board(Tile[,] tiles, int width, int height)
    {
        this.tiles = tiles;

        this.width = width;
        this.height = height;   
    }

    /// <summary>
    /// Resets the board so that all tiles go back to having a TileType of None
    /// </summary>
    public virtual void ResetBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[x, y].tileType = TileType.None;
            }
        }
    }
}
