using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainGame : MonoBehaviour
{

    string gameOverText;
    AudioClipName gameOverClipName;
    GameStatus gameOverStatus;
    Button reloadButton;

    void Awake() {
        reloadButton = GameObject.FindWithTag("MainCanvas").transform.Find("ButtonPanel").Find("ReloadIcon").gameObject.GetComponent<Button>();
    }
    public void playAgain(){
         GameObject scoreboard = GameObject.FindWithTag("MainCanvas").transform.Find("ScoreBoard").gameObject;
         if(GameObject.FindWithTag("GameBoard").GetComponent<GameBoard>().ActiveGame){
            scoreboard.GetComponent<Scoreboard>().ComputerWon();
         }
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void help(){
        ScreenManager.GetInstance().InstantiateScreen(CanvasName.InstructionsPage, GameObject.FindWithTag("MainCanvas").transform);
    }

    public void GoToStartScreen(){
       ScreenManager.GetInstance().GoToScene(SceneName.GameStartScene);
       Scoreboard.ResetScores();
    }

     public void quit(){

       Application.Quit();
    }

    public void gameOver(PlayerStatus lastPlayer, GameStatus gameStatus, string oppponentName, GameObject losingNumber, GameObject neighbor){
        reloadButton.interactable = false;
        gameOverStatus = gameStatus;
        if (gameStatus == GameStatus.tied) {
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
            //gameOverDelay.Duration = 2.2f;
            gameOverStatus = GameStatus.won;
        }
    }

    private void UpdateGameOverSettings(){
        GameObject scoreboard = GameObject.FindWithTag("MainCanvas").transform.Find("ScoreBoard").gameObject;
        ScreenManager.GetInstance().InstantiateScreen(CanvasName.GameOver, GameObject.FindWithTag("MainCanvas").transform);
        GameObject.FindWithTag("GameOverCanvas").GetComponent<GameOverCanvas>().updateTextandPlaySound(gameOverText, gameOverClipName);
        switch(gameOverStatus){
            case GameStatus.tied:
                scoreboard.GetComponent<Scoreboard>().GameTied();
                break;
            case GameStatus.lost:
                scoreboard.GetComponent<Scoreboard>().ComputerWon();
                break;
            case GameStatus.won:
                scoreboard.GetComponent<Scoreboard>().PlayerWon();
                break;
        }
    }

    public void DisplayScoreMessage(){
        ScreenManager.GetInstance().InstantiateScreen(CanvasName.ScoreMessage, GameObject.FindWithTag("MainCanvas").transform);
        GameObject.FindWithTag("MainCanvas").transform.Find("ScoreBoard").gameObject.GetComponent<Scoreboard>().SetScoreMessage();
    }
}

