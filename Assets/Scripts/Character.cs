using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Character : MonoBehaviour, IPointerClickHandler
{
    public static Sprite CharacterToPlayWith;

    public void OnPointerClick(PointerEventData eventData)
    {
        CharacterToPlayWith = GetComponent<Image>().sprite;
        GameObject.FindWithTag("GameStartCanvas").GetComponent<GameStartCanvas>().goToMainGame();
    }
}
