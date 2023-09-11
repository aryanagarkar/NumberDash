using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using HelperEnums;

namespace Sound
{
    /// <summary>
    /// Handles button click operations.
    /// </summary>

    public class ButtonClick : MonoBehaviour
    {
        /// <summary>
        /// Plays the button click sound effect when a button is clicked.
        /// </summary>

        public void ButtonClicked()
        {
            SoundManager.PlayClipByName(AudioClipName.ButtonClick);
        }
    }
}