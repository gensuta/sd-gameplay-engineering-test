using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

/// <summary>
/// Initializes the game, checks if we can load our game, and controls the width and height of our Board. 
/// Also determines if the Board is square or rectangular.
/// </summary>
public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private BoardGenerator boardGenerator;

    [SerializeField]
    private int width; // the given width of our new Board. Can be edited in the inspector, but not by any other script
    private int height; // the given height of our new Board. Can be edited in the inspector, but not by any other script

    // Start is called before the first frame update
    void Start()
    {
        if (!SaveManager.Instance.CanLoadGame()) // checks if there's a save file to load from. If not, run Init()
        {
            Init();
        }
        else
        {
            SaveManager.Instance.LoadGame();
        }
    }

    /// <summary>
    /// Initializes our game by choosing who goes first and telling the BoardGenerator to generate a Board
    /// depending on our given width and height
    /// </summary>
    private void Init()
    {
        TurnManager.Instance.ChooseRandomPlayer();


        if (width == 0 && height == 0) // if we didn't put anything, create a 3x3 SquareBoard by default
        {
            boardGenerator.GenerateSquareBoard();
        }
        else
        {
            if (width == height) 
            {
                // if our width and height are the same, it's a SquareBoard
                boardGenerator.GenerateSquareBoard(width);
            }
            else 
            {
                // if our width and height aren't the same, it's a rectangular Board
                boardGenerator.GenerateBoard(width,height);
            }
        }
    }
}
