using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{

    private Board board; // the board we're scoring

    private void OnEnable()
    {
        BoardGenerator.OnBoardGenerated += SetCurrentBoard;
    }

    private void OnDisable()
    {
        BoardGenerator.OnBoardGenerated -= SetCurrentBoard;
    }

    /// <summary>
    /// Stores the generated board after OnBoardGenerated is fired off
    /// </summary>
    /// <param name="board">the generated board</param>
    private void SetCurrentBoard(Board board)
    {
        this.board = board;
    }

    // A simple 3x3 diagram of what the Board looks like with just it's locations

    // 0,0 1,0 2,0
    // 0,1 1,1 2,1 
    // 0,2 1,2 2,2

    /// <summary>
    /// Checks to see if any tiles have a 3 in a row match whether it's vertical, horizontal, or diagonal
    /// </summary>
    /// <returns>Returns if we have a winner or if no one's won yet</returns>
    public bool CheckForWinner()
    {
        // creating these ints for simpler reference since it's going to be used multiple times in this function
        int height = board.Height;
        int width = board.Width;

        // HORIZONTAL check
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width-2; x++) // x needs to be 2 less than the width since we're checking for x+1 and x+2 
            {
                if (!DoTilesMatch(x, y, x + 1, y))
                {
                    continue; // if we don't have a match, we don't check the next if statement
                }

                if (!DoTilesMatch(x, y, x + 2, y)) // if our first two tiles match, check if the first and third tile match
                {
                    continue;
                }
                return true;
            }
        }

        // VERTICAL check
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height - 2; y++) // y needs to be 2 less than the height since we're checking for y+1 and y+2 
            {
                if (!DoTilesMatch(x, y, x, y + 1))
                {
                    continue; // if we don't have a match, we don't check the next if statement
                }

                if (!DoTilesMatch(x, y, x, y + 2)) // if our first two tiles match, check if the first and third tile match
                {
                    continue;
                }
                return true;
            }
        }

        // LEFT DIAGONAL check
        //needs both the width and height to be subtracted by 2 since we're checking for x+2 and y+2
        for (int x = 0; x < width -2; x++)
        {
            for (int y = 0; y < height -2; y++)
            {
                if (!DoTilesMatch(x, y, x+1, y + 1))
                {
                    continue; // if we don't have a match, we don't check the next if statement
                }

                if (!DoTilesMatch(x+1, y+1, x+2, y + 2)) // if our first two tiles match, check if the second and third tile match
                {
                    continue;
                }
                return true;
            }
        }


        // RIGHT DIAGONAL check
        //needs both the width and height to be subtracted by 2 since we're checking for x+2 and y+2
        for (int x = 0; x < width-2; x++)
        {
            for (int y = 0; y < height - 2; y++)
            {
                if (!DoTilesMatch(x+2, y, x + 1, y + 1))
                {
                    continue; // if we don't have a match, we don't check the next if statement
                }

                if (!DoTilesMatch(x + 1, y + 1, x, y + 2)) // if our first two tiles match, check if the second and third tile match
                {
                    continue;
                }
                return true;
            }
        }

        // returns false if none of the previous for loops hits the return true line. Thus there being no matches
        return false; 
    }

    /// <summary>
    /// Checking to see if the game is ending in a tie
    /// </summary>
    /// <returns>Returns if all the tiles are filled or not with no match 3</returns>
    public bool CheckForTie()
    {
        for (int x = 0; x < board.Width; x++)
        {
            for (int y = 0; y < board.Height; y++)
            {
               if( board.GetTile(x, y).tileType == TileType.None) // if there's a tile we can still fill, it's not a tie
                {
                    return false;
                }
            }
        }

        return true; // all tiles are filled with no 3 in a row
    }

    /// <summary>
    /// Checking if two Tiles given their locations have matching TileTypes
    /// </summary>
    /// <param name="x1">The first Tile's x location</param>
    /// <param name="y1">The first Tile's y location</param>
    /// <param name="x2">The second Tile's x location</param>
    /// <param name="y2">The second Tile's y location</param>
    /// <returns></returns>
    bool DoTilesMatch(int x1,int y1,int x2, int y2)
    {
        TileType firstTileType = board.GetTile(x1,y1).tileType; // getting the first Tile's TileType based on it's location
        TileType secondTileType = board.GetTile(x2, y2).tileType; // getting the second Tile's TileType based on it's location

        if (firstTileType == TileType.None || secondTileType == TileType.None) // if either of the Tile's haven't been filled, it's not a match
        {
            return false;
        }

        if (firstTileType == secondTileType) // if our TileType's are equal to each other, it's a match
        {
            return true;
        }
        else // if our TileType's aren't equal to each other, it's not a match
        {
            return false;
        }
    }

}
