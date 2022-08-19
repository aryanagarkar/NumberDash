using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverCanvas : MonoBehaviour
{
    TextMeshProUGUI textComponent;

    void Awake()
    {
        textComponent = transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>(); 
    }


    public void updateTextandPlaySound(string text, AudioClipName name){
        textComponent.text = text;
        SoundManager.PlayClipByName(name);
    }

    public void playAgain(){
        Camera.main.GetComponent<MainGame>().playAgain();
    }

    public void Menu(){
          Camera.main.GetComponent<MainGame>().GoToStartScreen();
    }
}
