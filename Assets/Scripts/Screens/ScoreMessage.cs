using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMessage : MonoBehaviour
{
    public void GoToMainMenu(){
        SoundManager.StopClip();
        Camera.main.GetComponent<MainGame>().GoToStartScreen();
    }

    public void PlayAgain(){
        SoundManager.StopClip();
        Camera.main.GetComponent<MainGame>().playAgain(); 
    }
}
