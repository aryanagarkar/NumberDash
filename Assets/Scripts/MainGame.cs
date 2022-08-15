using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    GameObject gameOverCanvas;
    GameObject mainCanvas;

    void Start()
    {
        gameOverCanvas = Resources.Load<GameObject>("GameOver");
        mainCanvas = Resources.Load<GameObject>("MainCanvas");
    }


    public void playAgain(){
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void reset(){
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

     public void quit(){
       Application.Quit();
    }

    public void gameOver(Player lastPlayer, GameStatus gameStatus){
        string gameOverText = "";
        if (gameStatus == GameStatus.tied) {
            gameOverText = "It was a tie!";
        } else if(lastPlayer == Player.You) {
            gameOverText = lastPlayer.ToString() + " lost! Better Luck Next Time.";
        } else {
            gameOverText = Player.You.ToString() + " won! Great Job.";
        }
        Instantiate(gameOverCanvas, GameObject.FindWithTag("MainCanvas").transform);
        GameObject.FindWithTag("GameOverCanvas").GetComponent<GameOverCanvas>().updateGameOverText(gameOverText);
    }
}

