using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Opponent : MonoBehaviour, IPointerClickHandler
{
    public static Sprite CharacterToPlayWith;
    public static string CharacterName;

    public void OnPointerClick(PointerEventData eventData)
    {
        CharacterToPlayWith = GetComponent<Image>().sprite;
        CharacterName = transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        SoundManager.PlayClipByName(AudioClipName.ButtonClick);
        GameObject.FindWithTag("PersistentObject").GetComponent<GameManager>().goToMainGame();
    }
}
