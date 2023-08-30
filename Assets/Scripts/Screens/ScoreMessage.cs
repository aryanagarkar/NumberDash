using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMessage : MonoBehaviour
{
    public void GoToMainMenu(){
        SoundManager.Stop();
        Camera.main.GetComponent<MainGame>().GoToStartScreen();
    }

    public void PlayAgain(){
        SoundManager.Stop();
        Camera.main.GetComponent<MainGame>().playAgain(); 
    }
}
