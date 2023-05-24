using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Contains it's location and tileType for other classes to access.
/// </summary>
[Serializable]
public class Tile 
{
    public int x; // x position of this Tile
    public int y; // y position of this Tile
    public TileType tileType; // TileType of this Tile

    /// <summary>
    /// Sets this Tile to a specific x and y location
    /// </summary>
    /// <param name="x">The x location of this Tile in a Board</param>
    /// <param name="y">The y location of this Tile in a Board</param>
    public void SetLocation(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

}

/// <summary>
/// Determines if a Tile contains an X, O, or no symbol in a Board
/// </summary>
public enum TileType
{
    None = 0,
    X = 1,
    O = 2
}
