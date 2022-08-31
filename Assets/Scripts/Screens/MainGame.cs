using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    GameObject gameOverCanvas;
    GameObject mainCanvas;
    GameObject instructionPage;
    Timer gameOverDelay;

    string gameOverText;
    AudioClipName gameOverClipName;

    void Start()
    {
        gameOverCanvas = Resources.Load<GameObject>("Prefabs/Canvases/GameOver");
        mainCanvas = Resources.Load<GameObject>("Prefabs/MainCanvas");
        instructionPage = Resources.Load<GameObject>("Prefabs/Canvases/InstructionsPage");
        gameOverDelay = gameObject.AddComponent<Timer>();
    }

    public void Update(){
         if (gameOverDelay.Finished)
        {
            Instantiate(gameOverCanvas, GameObject.FindWithTag("MainCanvas").transform);
            GameObject.FindWithTag("GameOverCanvas").GetComponent<GameOverCanvas>().updateTextandPlaySound(gameOverText, gameOverClipName);
            gameOverDelay.Stop();
        }
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

    public void gameOver(PlayerStatus lastPlayer, GameStatus gameStatus, string oppponentName, GameObject losingNumber, GameObject neighbor){
        if (gameStatus == GameStatus.tied) {
            gameOverClipName = AudioClipName.Tied;
            gameOverText = "It was a tie!";
            gameOverDelay.Duration = 1;
        } else if(lastPlayer == PlayerStatus.You) {
            gameOverClipName = AudioClipName.PlayerLost;
            gameOverText = lastPlayer.ToString() + " lost! " + losingNumber.GetComponent<Tile>().Number + " can't be placed next to " + neighbor.GetComponent<Tile>().Number + ".";
            gameOverDelay.Duration = 2.2f;
        } else {
            gameOverClipName = AudioClipName.PlayerWon;
            gameOverText = PlayerStatus.You.ToString() + " won! " + oppponentName + " played " + losingNumber.GetComponent<Tile>().Number + " next to " + neighbor.GetComponent<Tile>().Number + ".";
            gameOverDelay.Duration = 2.2f;
        }
        gameOverDelay.Run();
    }
}

