using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sound;

namespace UIElements
{
    /// <summary>
    /// Manages the actions on the score message display.
    /// </summary>

    public class ScoreMessage : MonoBehaviour
    {
        /// <summary>
        /// Handles the "Yes" button click event.
        /// Stops any playing sound clip and goes back to the start screen.
        /// </summary>

        public void GoToMainMenu()
        {
            SoundManager.StopClip();
            Camera.main.GetComponent<MainGame>().GoToStartScreen();
        }

        /// <summary>
        /// Handles the "No" button click event.
        /// Stops any playing sound clip and restarts the game.
        /// </summary>

        public void PlayAgain()
        {
            SoundManager.StopClip();
            Camera.main.GetComponent<MainGame>().playAgain();
        }
    }
}