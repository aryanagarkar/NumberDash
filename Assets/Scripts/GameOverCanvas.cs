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


    public void updateGameOverText(string text){
        textComponent.text = text;
    }

    public void playAgain(){
        Camera.main.GetComponent<MainGame>().playAgain();
    }

    public void quit(){
          Camera.main.GetComponent<MainGame>().quit();
    }
}
