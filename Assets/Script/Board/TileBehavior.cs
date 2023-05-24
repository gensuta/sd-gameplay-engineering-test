using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Contains all functionality for the Tile GameObject. 
/// Each tile Behavior is connected to a Tile in the Board.
/// </summary>
public class TileBehavior : MonoBehaviour
{
    private Image image; // image the tile has
    private Button button; // button component of the tile

    public Tile tile; // tile connected to this TileBehavior

    [SerializeField]
    Sprite[] tileIcons; // the limited tileIcons that corresponds with each tileType. 0 is None, 1 is X, 2 is O

    Player currentPlayer // added this so it's easy to access the player's current tileType 
    { 
        get 
        { 
            return TurnManager.currentPlayer; 
        } 
    }

    public static event Action<Tile> OnTileChosen; // fires off everytime a tile is clicked on. Currently not used.


    void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        image.sprite = tileIcons[0]; // setting our tile to a blank image
    }

    /// <summary>
    /// Sets the given Tile to a specific TileType based on the currentPlayer's TileType and makes this Tile uninteractable for the rest of the round.
    /// Also asks the TurnManager to check for a winner so that the round can end or keep going.
    /// </summary>
    public void SetTile()
    {
        int tileTypeNum = (int)currentPlayer.tileType; // TileTypes already have numbers assigned to each to make this easier
        image.sprite = tileIcons[tileTypeNum];

        tile.tileType = currentPlayer.tileType;
        button.interactable = false;

        OnTileChosen?.Invoke(tile);
        TurnManager.Instance.CheckForWinner();
    }

    /// <summary>
    /// Resets everything in the TileBehavior so that it's icon and Tile is set to None and it's interactable again.
    /// </summary>
    public void ResetTileBehavior()
    {
        image.sprite = tileIcons[0];
        button.interactable = true;
        tile.tileType = TileType.None;
    }

    /// <summary>
    /// Updates this Tile to a new TileType due to the save data that's been loaded
    /// </summary>
    /// <param name="tileType">the new TileType for this Tile</param>
    public void UpdateTile(TileType tileType)
    {
        int tileTypeNum = (int)tileType;
        image.sprite = tileIcons[tileTypeNum];

        tile.tileType = tileType;

        if (tileType != TileType.None)
        {
            button.interactable = false; // makes sure any X or O Tiles aren't interactable
        }
    }
}
