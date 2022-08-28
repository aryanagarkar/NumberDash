using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour, IPointerClickHandler
{
    static Sprite playerCharacter;

    public static Sprite PlayerCharacter {
        get {
            if (playerCharacter == null) {
                LoadPlayerCharacter();
            }
            return playerCharacter;
        }
    }
  
    private static void LoadPlayerCharacter() {
        if(!PlayerPrefs.HasKey("Avatar")){
            PlayerPrefs.SetString("Avatar", "Mummy");
        }
        playerCharacter = AssetLoader.GetInstance().GetSpriteByName(PlayerPrefs.GetString("Avatar"));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        playerCharacter = GetComponent<Image>().sprite;
        SoundManager.PlayClipByName(AudioClipName.ButtonClick);
        GameObject.FindWithTag("Settings").GetComponent<SettingsPage>().PlayerChosen(PlayerCharacter);
    }
}
