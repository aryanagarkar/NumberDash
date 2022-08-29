using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour, IPointerClickHandler
{
    static Sprite playerCharacter;
    bool selected = false;

    public static Sprite PlayerCharacter {
        get {
            return playerCharacter;
        }
    }

    void Awake(){
        playerCharacter = LoadPlayerCharacter();
    }
  
    private static Sprite LoadPlayerCharacter() {
        if(!PlayerPrefs.HasKey("Avatar")){
            PlayerPrefs.SetString("Avatar", "PB");
            return AssetLoader.GetInstance().DefaultPlayer;
        }
        return AssetLoader.GetInstance().GetSpriteByName(PlayerPrefs.GetString("Avatar"));
    }

    public bool isSelected() {
        return selected;
    }

    public void ChangeSelection(bool select) {
        this.selected = select;
        SetImage();
    }

    private void SetImage() {
        string imageName = name;
        if(selected) {
            imageName = name + "Selected";
            PlayerPrefs.SetString("Avatar", name);
            playerCharacter = AssetLoader.GetInstance().GetSpriteByName(PlayerPrefs.GetString("Avatar"));
            GameObject.FindWithTag("Settings").GetComponent<SettingsPage>().PlayerAvatarSelected(name);
        } else {
            imageName = name + "Circle";
        }
        GetComponent<Image>().sprite = AssetLoader.GetInstance().GetSpriteByName(imageName);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (selected) {
            return;
        }
        SoundManager.PlayClipByName(AudioClipName.ButtonClick);
        ChangeSelection(true);
    }
}
