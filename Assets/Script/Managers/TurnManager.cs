using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Handles all Turn Management.Checks when a turn should be ended and if a round should be ended.
/// Also determines when the player should be switched
/// </summary>
public class TurnManager : Singleton<TurnManager>
{
    [SerializeField]
    private Player firstPlayer; // our first player

    [SerializeField]
    private Player secondPlayer; // our second player

    public static Player currentPlayer; // the current player who can take an action

    public static event Action OnTurnEnd; // fires off when a turn ends
    public static event Action<SaveData> OnTurnSaved; // fires off when a turn's SaveData is put together

    public static event Action OnRoundEnd; // fires off when the game ( a round ) ends

    private Board board; // our generated board

    [SerializeField]
    private GameObject roundEndObject; // the object that displays at the end of the game before everything is reset
    [SerializeField]
    private TextMeshProUGUI roundEndText; // the text that's displayed at the end of the game before everything is reset

    private float timer; // the timer that counts upwards to the displayTime
    [SerializeField]
    private float displayTime = 2f; // how long should we display the roundEndObject before resetting the game?

    private bool shouldDisplay; // should we be displaying the roundEndObject?

    private void OnEnable()
    {
        BoardGenerator.OnBoardGenerated += StoreBoard;
    }

    private void OnDisable()
    {
        BoardGenerator.OnBoardGenerated -= StoreBoard;
    }

    private void Update()
    {
        if(shouldDisplay)
        {
            timer += Time.deltaTime;
            if(timer  > displayTime)
            {
                OnRoundEnd?.Invoke();
                roundEndObject.SetActive(false);
                shouldDisplay = false;
            }
        }
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
    /// Checks to see if we have a winner, if we have a tie, or if we should continue playing the game.
    /// </summary>
    public void CheckForWinner()
    {
        bool hasWinner = ScoreManager.Instance.CheckForWinner();

        bool isTie = ScoreManager.Instance.CheckForTie();

        if (hasWinner) // game ends if we have a winner!
        {
            Debug.Log(string.Format("The {0} wins!!", currentPlayer.playerType));
            DisplayEndInfo(isTie);
        }
        else if (isTie) // game ends if we have a tie!
        {
            Debug.Log("Tie!");
            DisplayEndInfo(isTie);
        }
        else // if our game isn't over, start a new turn
        {
            StartNewTurn();
        }
    }

    /// <summary>
    /// Displays an image with text stating if it's a tie or if there's a winner
    /// </summary>
    /// <param name="isTie">do we have a tie or a winner?</param>
    public void DisplayEndInfo(bool isTie)
    {
        roundEndObject.SetActive(true);
        roundEndText.text = (isTie) ? "Tie" : string.Format("{0} wins!!", currentPlayer.tileType);

        timer = 0;
        shouldDisplay = true;
    }

    /// <summary>
    /// Gets a random number and if 0 is chosen, the first Player goes, and if it's 1 the second Player goes.
    /// </summary>
    public void ChooseRandomPlayer()
    {
        int randNum = UnityEngine.Random.Range(0, 2);
        currentPlayer = (randNum == 0) ? firstPlayer : secondPlayer;
        Debug.Log("Starting as " + currentPlayer.playerType);
    }

    /// <summary>
    /// Starts a new turn after the last turn ends
    /// </summary>
    public void StartNewTurn()
    {
        GoToNextPlayer(); // switches who the current player is

        if (SaveManager.Instance.isAutoSaveOn) // saves the game if autosave is on
        {
            SaveData currentSave = new SaveData(board, currentPlayer.playerType); // getting what the board looks like and who's turn it is
            OnTurnSaved?.Invoke(currentSave);
        }

        OnTurnEnd?.Invoke();
    }

    /// <summary>
    /// Determines who's the next currentPlayer
    /// If the firstPlayer went, go to the secondPlayer and vice versa.
    /// </summary>
    public void GoToNextPlayer()
    {
        switch (currentPlayer.playerType)
        {
            case PlayerType.FirstPlayer:
                currentPlayer = secondPlayer;
                break;
            case PlayerType.SecondPlayer:
                currentPlayer = firstPlayer;
                break;
        }
    }

    /// <summary>
    /// Changes who the currentPlayer is based on the given PlayerType
    /// </summary>
    /// <param name="playerType">the given PlayerType determining who's turn it is</param>
    public void SwapPlayer(PlayerType playerType)
    {
        switch (playerType)
        {
            case PlayerType.FirstPlayer:
                currentPlayer = firstPlayer;
                break;
            case PlayerType.SecondPlayer:
                currentPlayer = secondPlayer;
                break;
        }
    }
}
