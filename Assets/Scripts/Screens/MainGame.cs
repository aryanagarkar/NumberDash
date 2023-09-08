using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The MainGame class handles the primary gameplay logic.
/// </summary>

public class MainGame : MonoBehaviour
{
    //References and variables for game over UI elements.
    string gameOverText;
    AudioClipName gameOverClipName;
    GameStatus gameOverStatus;
    Button reloadButton;

    void Awake() {
        //Set up reload button reference.
        reloadButton = GameObject.FindWithTag("MainCanvas").transform.Find("ButtonPanel").Find("ReloadIcon").gameObject.GetComponent<Button>();
    }

    /// <summary>
    /// Resets the game and reloads the game scene to play again.
    /// </summary>

    public void playAgain(){
        //Set up the scoreboard reference
         GameObject scoreboard = GameObject.FindWithTag("MainCanvas").transform.Find("ScoreBoard").gameObject;

         //If the game is still active, i.e, you pressed the reaload button while a game was in progress, the computer automatically wins.
         if(GameObject.FindWithTag("GameBoard").GetComponent<GameBoard>().ActiveGame){
            scoreboard.GetComponent<Scoreboard>().ComputerWon();
         }

         //Reload the game scene to restart the game.
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// When the help button is clicked, display the instructions screen.
    /// </summary>

    public void help(){
        ScreenManager.GetInstance().InstantiateScreen(UIElementName.InstructionsPage, GameObject.FindWithTag("MainCanvas").transform);
    }

    /// <summary>
    /// When the menu button is clicked, go to the start screen and reset the scores.
    /// </summary>

    public void GoToStartScreen(){
       ScreenManager.GetInstance().GoToScene(SceneName.GameStartScene);
       Scoreboard.ResetScores();
    }

    /// <summary>
    /// When the quit button is clicked, exit the application.
    /// </summary>

    public void quit(){

       Application.Quit();
    }


    /// <summary>
    /// Handles the logic for the game over scenarios.
    /// </summary>
    /// <param name="lastPlayer">The last player to play before game over.</param>
    /// <param name="gameStatus">The final status (win, lose, or tie) of the game.</param>
    /// <param name="oppponentName">Name of the opponent.</param>
    /// <param name="losingNumber">The losing number tile game object.</param>
    /// <param name="neighbor">The neighboring number tile to the losing tile</param>

    public void gameOver(PlayerStatus lastPlayer, GameStatus gameStatus, string oppponentName, GameObject losingNumber, GameObject neighbor){
        //Deactivate the reload button while game over phase is running.
        reloadButton.interactable = false;

        gameOverStatus = gameStatus;

        //Execute different actions based on whether the game was a tie, the player lost, or the player won.
        if (gameStatus == GameStatus.Tied) {
            gameOverClipName = AudioClipName.Tied;
            gameOverText = "It was a tie!";
            Invoke("UpdateGameOverSettings", 1);
        } else if(lastPlayer == PlayerStatus.You) {
            gameOverClipName = AudioClipName.PlayerLost;
            gameOverText = PlayerStatus.You.ToString() + " lost! " + losingNumber.GetComponent<Tile>().Number + " can't be placed next to " + neighbor.GetComponent<Tile>().Number + ".";      
            Invoke("UpdateGameOverSettings", 2.2f);
        } else {
            gameOverClipName = AudioClipName.PlayerWon;
            gameOverText = PlayerStatus.You.ToString() + " won! " + Opponent.CharacterName + " played " + losingNumber.GetComponent<Tile>().Number + " next to " + neighbor.GetComponent<Tile>().Number + ".";
            Invoke("UpdateGameOverSettings", 2.2f);
            gameOverStatus = GameStatus.Won;
        }
    }

    /// <summary>
    /// Displays the score message for the game session.
    /// </summary>

    public void DisplayScoreMessage(){
        ScreenManager.GetInstance().InstantiateScreen(UIElementName.ScoreMessage, GameObject.FindWithTag("MainCanvas").transform);
        GameObject.FindWithTag("MainCanvas").transform.Find("ScoreBoard").gameObject.GetComponent<Scoreboard>().SetScoreMessage();
    }

    /// <summary>
    /// Updates the game over settings, messages, and scores.
    /// </summary>
    
    private void UpdateGameOverSettings(){
        //Set up the scoreboard reference.
        GameObject scoreboard = GameObject.FindWithTag("MainCanvas").transform.Find("ScoreBoard").gameObject;

        //Go to game over display.
        ScreenManager.GetInstance().InstantiateScreen(UIElementName.GameOver, GameObject.FindWithTag("MainCanvas").transform);

        GameObject.FindWithTag("GameOverCanvas").GetComponent<GameOverCanvas>().updateTextandPlaySound(gameOverText, gameOverClipName);

        //Based on the game status (win, lose, or tie), call the scoreboard to update its score accordingly.
        switch(gameOverStatus){
            case GameStatus.Tied:
                scoreboard.GetComponent<Scoreboard>().GameTied();
                break;
            case GameStatus.Lost:
                scoreboard.GetComponent<Scoreboard>().ComputerWon();
                break;
            case GameStatus.Won:
                scoreboard.GetComponent<Scoreboard>().PlayerWon();
                break;
        }
    }
}

