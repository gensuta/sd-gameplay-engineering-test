using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles all board generation. 
/// Spawns prefabs for each Tile and places said Tiles in the tileHolder
/// </summary>
public class BoardGenerator : MonoBehaviour
{
    private Board board; // the board we generate or load in with save data

    [SerializeField]
    private Transform tileHolder; // holds all tile GameObjects

    [SerializeField]
    public GameObject tilePrefab; // the GameObject that has the TileBehvaior component


    public static event Action<Board> OnBoardGenerated; // fires off when the Board is generated
    public static event Action<TileBehavior,int,int> OnTileGenerated; // fires off when a Tile is generated

    /// <summary>
    /// Generates a SquareBoard depending on a given size
    /// </summary>
    /// <param name="size">the size of our SquareBoard</param>
    public void GenerateSquareBoard(int size = 3)
    {
        Debug.Log(string.Format("Generating a board with the size of {0}!",size));

        board = new SquareBoard(size);

        OnBoardGenerated?.Invoke(board);

        // generating the SquareBoard with y first to make sure it's instantiating the Tiles in the appropriate order
        // instead of 0,0 0,1 0,2 it'll be 0,0 1,0 2,0 which is the correct way for this Board
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                SpawnTilePrefab(x,y);
            }
        }

    }

    /// <summary>
    /// Generates a rectangular Board depending on a given width and height
    /// </summary>
    /// <param name="width">the width of our Board</param>
    /// <param name="height">the height of our Board</param>
    public void GenerateBoard(int width, int height)
    {
        Debug.Log(string.Format("Generating a board with a width of {0} and a height of {1}!", width, height));

        board = new Board(width, height);

        // generating the SquareBoard with y first to make sure it's instantiating the Tiles in the appropriate order
        // instead of 0,0 0,1 0,2 it'll be 0,0 1,0 2,0 which is the correct way for this Board
        OnBoardGenerated?.Invoke(board);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                SpawnTilePrefab(x, y);
            }
        }

    }

    /// <summary>
    /// Instantiates a tile to put in the tileHolder and gives it the appropriate data for it's TileBehavior
    /// </summary>
    /// <param name="x">The x location of the Tile to be attached to TileBehavior</param>
    /// <param name="y">The y location of the Tile to be attached to TileBehavior</param>
    private void SpawnTilePrefab(int x, int y)
    {
        GameObject tileObj = Instantiate(tilePrefab, tileHolder);

        TileBehavior tileBehavior = tileObj.GetComponent<TileBehavior>();

        if (tileBehavior != null) 
        { 
            tileBehavior.tile = board.GetTile(x, y);
            OnTileGenerated?.Invoke(tileBehavior,x,y);
        }
    }
}
