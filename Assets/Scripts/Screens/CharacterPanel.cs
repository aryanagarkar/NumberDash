using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class CharacterPanel : MonoBehaviour
{
    Dictionary<string, Sprite> availableCharacters;
    string chosenAvatar;

    GameObject spritePanel;

    void Start()
    {
        availableCharacters = new Dictionary<string, Sprite>(AssetLoader.GetInstance().Characters);  
        chosenAvatar = PlayerPrefs.GetString("Avatar");
        availableCharacters.Remove(chosenAvatar);  
        spritePanel = transform.GetChild(1).gameObject;
        SetCharacters();  
    }

    private void SetCharacters(){
        int randomSpriteIndex;
        for(int i = 0; i < 4; i++){
            randomSpriteIndex = Random.Range(0, availableCharacters.Count - 1);
            GameObject character = spritePanel.transform.GetChild(i).gameObject;
            Image characterImage = character.GetComponent<Image>();
            TextMeshProUGUI name = character.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            characterImage.sprite = availableCharacters.ElementAt(randomSpriteIndex).Value;
            name.text = availableCharacters.ElementAt(randomSpriteIndex).Key;
            availableCharacters.Remove(availableCharacters.ElementAt(randomSpriteIndex).Key);
        }  
    }
}
