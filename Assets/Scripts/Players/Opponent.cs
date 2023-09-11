using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Managers;
using Sound;
using HelperEnums;

namespace Players
{
    /// <summary>
    /// Represents the opponent (the computer) in the game. 
    /// Allows selection of the character for this player for gameplay.
    /// </summary>

    public class Opponent : MonoBehaviour, IPointerClickHandler
    {
        public static Sprite CharacterToPlayWith;   // The sprite representing the opponent's character selected for gameplay.
        public static string CharacterName;         // The name of the character above.

        /// <summary>
        /// Detects when a character is chosen for the opponent.
        /// Updates the opponent character's sprite and name. 
        /// Transitions to the main game.
        /// </summary>
        /// <param name="eventData">Data associated with the pointer event.</param>

        public void OnPointerClick(PointerEventData eventData)
        {
            // Retrieve the sprite and name of the selected character and set the variables.
            CharacterToPlayWith = GetComponent<Image>().sprite;
            CharacterName = transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;

            SoundManager.PlayClipByName(AudioClipName.ButtonClick);

            GameObject.FindWithTag("PersistentObject").GetComponent<GameManager>().goToMainGame();
        }
    }
}