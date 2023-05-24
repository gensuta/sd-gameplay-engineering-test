using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains all the data that needs to be saved and loaded
/// </summary>
[Serializable]
public class SaveData 
{
    public Board board; // our current Board data

    public int width; // the width of the Board
    public int height; // the height of the Board

    public PlayerType playerType; // our currentPlayer's PlayerType
    public List<Tile> serializedTiles; // a list version of all our Tiles since 2D arrays can't be serialized

    /// <summary>
    /// Constructor for SaveData. Needs a Board and PlayerType
    /// </summary>
    /// <param name="board">the current game's Board</param>
    /// <param name="playerType">the PlayerType of the current Player</param>
    public SaveData(Board board, PlayerType playerType)
    {
        this.board = board;
        this.playerType = playerType;
        serializedTiles = new List<Tile>();

        // storing for when we need to convert our serializedTiles back to a 2D array
        width = board.Width; 
        height = board.Height;

        for (int x = 0; x < board.Width; x++)
        {
            for (int y = 0; y < board.Height; y++)
            {
                serializedTiles.Add(board.GetTile(x, y)); // adding all our Board's tiles to the serializedTiles list
            }
        }
    }

    /// <summary>
    /// Turns our list of tiles back into a 2D array and returns it
    /// </summary>
    /// <returns>the multi array of Tiles put in the appropriate order</returns>
    public Tile[,] ConvertTilesToMultiArray()
    {
        Tile[,] tiles = new Tile[width, height]; // creating new 2D array with the given width and height we saved

       foreach (Tile tile in serializedTiles)
        {
            tiles[tile.x, tile.y] = tile; // making sure we're putting each tile in the correct location!
        }

        return tiles;
    }
}
