using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages a generated Board and it's TileBehaviors. Can reset and update the given Board
/// </summary>
public class BoardManager : MonoBehaviour
{
    private Board board; // the generated board
    private TileBehavior[,] tileBehaviors; // the tile behaviors spawned from the generated board

    [SerializeField]
    private BoardGenerator boardGenerator;

    private void OnEnable()
    {
        BoardGenerator.OnTileGenerated += AddNewTileBehavior;
        BoardGenerator.OnBoardGenerated += StoreBoard;
        TurnManager.OnRoundEnd += ResetBoard;
        SaveManager.OnGameLoaded += UpdateBoard;
    }

    private void OnDisable()
    {
        BoardGenerator.OnTileGenerated -= AddNewTileBehavior;
        BoardGenerator.OnBoardGenerated -= StoreBoard;
        TurnManager.OnRoundEnd -= ResetBoard;
        SaveManager.OnGameLoaded -= UpdateBoard;

    }

    /// <summary>
    /// Stores the generated board after OnBoardGenerated is fired off
    /// </summary>
    /// <param name="board">the generated board</param>
    private void StoreBoard(Board board)
    {
        this.board = board;
    }

    /// <summary>
    /// Adds a TileBehavior it's given location in the 2D array
    /// </summary>
    /// <param name="tileBehavior">the given TileBehavior that was just spawned</param>
    /// <param name="x">The x location for the new TileBehavior</param>
    /// <param name="y">The y location for the new TileBehavior</param>
    private void AddNewTileBehavior(TileBehavior tileBehavior, int x, int y)
    {
        if(tileBehaviors == null)
        {
            tileBehaviors = new TileBehavior[board.Width, board.Height];
        }

        tileBehaviors[x, y] = tileBehavior;
    }

    /// <summary>
    /// Resets all the TileBehaviors in the board so they're TileType and sprites are set back to "None"
    /// Also makes sure the game is saved with the now reset Board.
    /// </summary>
    public void ResetBoard()
    {
        board.ResetBoard();
        for (int x = 0; x < board.Width; x++)
        {
            for (int y = 0; y < board.Height; y++)
            {
                tileBehaviors[x, y].ResetTileBehavior();
            }
        }
        SaveManager.Instance.SaveGame(board);
    }

    public void UpdateBoard(SaveData data)
    {
        if (board == null)
        {
            boardGenerator.GenerateBoard(data.width,data.height); // generating a fresh board!
        }

        board = data.board;
        for (int x = 0; x < board.Width; x++)
        {
            for (int y = 0; y < board.Height; y++)
            {
                tileBehaviors[x, y].UpdateTile(board.GetTile(x, y).tileType);
            }
        }
    }
}
