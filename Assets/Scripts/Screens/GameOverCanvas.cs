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
        /*, (success) => {
            Debug.Log("Callback called");
            transform.Find("YesPlayAgain").gameObject.GetComponent<Button>().enabled = true;
            transform.Find("NoToPlayAgain").gameObject.GetComponent<Button>().enabled = true;
        });*/
    }

    public void playAgain(){
        SoundManager.Stop();
        GameObject.Find("PersistentObject").GetComponent<InterstitialAds>().ShowAd((isSuccess) => {
            //Debug.Log("Callback called from ad finished");
            Camera.main.GetComponent<MainGame>().playAgain();
        }); 
    }

    public void Menu(){
        SoundManager.Stop();
        Camera.main.GetComponent<MainGame>().GoToStartScreen();
    }
}
