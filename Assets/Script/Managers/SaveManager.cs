using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Handles all saving and loading for this game
/// </summary>
public class SaveManager : Singleton<SaveManager>
{
    public bool isAutoSaveOn = true; // can be changed, but is set to true by default

    public static event Action OnGameSaved; // fires off every time the game is saved
    public static event Action<SaveData> OnGameLoaded; // fires off with the given SaveData every time the game is loaded

    [SerializeField]
    private string saveFileName = "saveData.txt"; // our save file name to be combined with the persistent data path

    private void OnEnable()
    {
        TurnManager.OnTurnSaved += SaveGame;
    }

    private void OnDisable()
    {
        TurnManager.OnTurnSaved -= SaveGame;
    }

    /// <summary>
    /// Checks if our save file exists and returns the outcome
    /// </summary>
    /// <returns>bool determining if we can save or not based on if our save file exists</returns>
    public bool CanLoadGame()
    {
        return File.Exists(GetSavePath());
    }

    /// <summary>
    /// Saves the game given the SaveData we send.
    /// Creates a new save file if one doesn't already exists
    /// </summary>
    /// <param name="data">the save data sent containing the game's current info</param>
    public void SaveGame(SaveData data)
    {
        string path = GetSavePath();

        string json = JsonUtility.ToJson(data); // turning our save file into a json string

        if(!File.Exists(path)) // creating a new save file if it doesn't already exist
        {
            StreamWriter streamWriter = File.CreateText(path);
            streamWriter.Close();
        }

        File.WriteAllText(path, json); // writing our json string to the file

        OnGameSaved?.Invoke();
    }

    /// <summary>
    /// Saves the game given the Board we send
    /// Creates a new save file if one doesn't already exists
    /// </summary>
    /// <param name="board">the current Board data in the game</param>
    public void SaveGame(Board board)
    {
        // creates new SaveData based on the currentPlayer's PlayerType and the given Board
        SaveData data = new SaveData(board, TurnManager.currentPlayer.playerType); 

        string path = GetSavePath();

        string json = JsonUtility.ToJson(data); // turning our save file into a json string

        if (!File.Exists(path)) // creating a new save file if it doesn't already exist
        {
            StreamWriter streamWriter = File.CreateText(path);
            streamWriter.Close();
        }

        File.WriteAllText(path, json); // writing our json string to the file

        OnGameSaved?.Invoke();
    }

    /// <summary>
    /// Loads the json file and updates the currentPlayer and Board from the given data
    /// </summary>
    public void LoadGame()
    {
        string json = File.ReadAllText(GetSavePath()); // getting our json data from our save file

        SaveData data = JsonUtility.FromJson<SaveData>(json); // converting our json string back into SaveData

        TurnManager.Instance.SwapPlayer(data.playerType); // making sure the correct player's turn is chosen

        data.board = new Board(data.ConvertTilesToMultiArray(),data.width,data.height); // turning our list of Tiles back into a 2D array of Tiles

        OnGameLoaded?.Invoke(data);
        Debug.Log("loaded save data.");
    }

    /// <summary>
    /// Gets the combined path of the save file name and the persistentDataPath
    /// </summary>
    /// <returns>Returns the full save path</returns>
    private string GetSavePath()
    {
        return Path.Combine(Application.persistentDataPath,saveFileName);
    }
}

