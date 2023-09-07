using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Manages and updates the scoreboard display.
/// </summary>

public class Scoreboard : MonoBehaviour
{
    // User's score UI elements and variables.
    TextMeshProUGUI you;
    TextMeshProUGUI yourScoreText;
    static int yourScore = 0; 

    // Opponent's score UI elements and variables
    TextMeshProUGUI computer;
    TextMeshProUGUI computerScoreText;
    static int computerScore = 0; 

    /// <summary>
    /// Initializes the scoreboard.
    /// Sets up references and displays starting scores of zero.
    /// </summary>

    void Awake()
    {
        // Setup user's score UI references.
        you = transform.Find("You").gameObject.GetComponent<TextMeshProUGUI>();
        you.text = "You:";
        yourScoreText = transform.Find("You").GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();

        // Setup opponent's score UI references.
        computer = transform.Find("Computer").gameObject.GetComponent<TextMeshProUGUI>();
        computer.text = Opponent.CharacterName + ":";
        computerScoreText = transform.Find("Computer").GetChild(0).gameObject.GetComponent<TextMeshProUGUI>(); 

        // Set initial scoreboard text values.
        yourScoreText.text = yourScore.ToString();
        computerScoreText.text = computerScore.ToString();
    }

    /// <summary>
    /// Updates the scoreboard when the opponent wins.
    /// </summary>

    public void ComputerWon()
    {
        computerScore += 1;
        computerScoreText.text = computerScore.ToString();
    }

    /// <summary>
    /// Updates the scoreboard when the user wins.
    /// </summary>
    public void PlayerWon()
    {
        yourScore += 1;
        yourScoreText.text = yourScore.ToString();
    }

    /// <summary>
    /// Updates the scoreboard when the game is tied.
    /// </summary>

    public void GameTied()
    {
        yourScore += 1;
        computerScore += 1;
        yourScoreText.text = yourScore.ToString();
        computerScoreText.text = computerScore.ToString();
    }

    /// <summary>
    /// Sets the ScoreMessage at the end of the game based on the final score. 
    /// </summary>

    public void SetScoreMessage()
    {
        //Get ScoreMessage references.
        GameObject message =  GameObject.FindWithTag("ScoreMessage");
        TextMeshProUGUI messageText = message.transform.Find("Message").gameObject.GetComponent<TextMeshProUGUI>();
        
        // Determine the message based on the final score
        if (computerScore > yourScore)
        {
            messageText.text = Opponent.CharacterName + " is winning!\nAre you sure you want to quit?";
        }
        else if (yourScore > computerScore)
        {
            messageText.text = "You are winning!\nAre you sure you want to quit?";
        }
        else
        {
            messageText.text = "You are tied!\nAre you sure you want to quit?";
        }
    }

    /// <summary>
    /// Resets both user and opponent scores to zero.
    /// </summary>

    public static void ResetScores()
    {
        yourScore = 0;
        computerScore = 0;
    }
}