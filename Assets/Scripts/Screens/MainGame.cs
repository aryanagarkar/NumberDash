using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    GameObject gameOverCanvas;
    GameObject mainCanvas;
     GameObject instructionPage;

    void Start()
    {
        gameOverCanvas = Resources.Load<GameObject>("Prefabs/Canvases/GameOver");
        mainCanvas = Resources.Load<GameObject>("Prefabs/MainCanvas");
        instructionPage = Resources.Load<GameObject>("Prefabs/Canvases/InstructionsPage");
    }


    public void playAgain(){
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void help(){
        Instantiate(instructionPage, GameObject.FindWithTag("MainCanvas").transform);
    }

    public void GoToStartScreen(){
       SceneManager.LoadScene(0);
    }

     public void quit(){
       Application.Quit();
    }

    public void gameOver(Player lastPlayer, GameStatus gameStatus){
        string gameOverText = "";
        AudioClipName name = AudioClipName.None;
        if (gameStatus == GameStatus.tied) {
            name = AudioClipName.Tied;
            gameOverText = "It was a tie!";
        } else if(lastPlayer == Player.You) {
            name = AudioClipName.PlayerLost;
            gameOverText = lastPlayer.ToString() + " lost! Better Luck Next Time.";
        } else {
            name = AudioClipName.PlayerWon;
            gameOverText = Player.You.ToString() + " won! Great Job.";
        }
        Instantiate(gameOverCanvas, GameObject.FindWithTag("MainCanvas").transform);
        GameObject.FindWithTag("GameOverCanvas").GetComponent<GameOverCanvas>().updateTextandPlaySound(gameOverText, name);
    }
}

