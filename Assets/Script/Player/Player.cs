using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds the information for each Player
/// </summary>
public class Player : MonoBehaviour
{
    public TileType tileType; // Does this player use X or O?
    public PlayerType playerType; // Are we the first or second player?
}
public enum PlayerType
{
    Computer, // in case we wanted to add AI
    FirstPlayer,
    SecondPlayer
}
