using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using HelperEnums;

namespace UIElements
{
    /// <summary>
    /// Manages the level selection panel and lets players choose a difficulty level for the game.
    /// </summary>

    public class LevelPanel : MonoBehaviour
    {
        /// <summary>
        /// Handles the action when the "Easy" level is selected.
        /// Calls the GameManager to start the Easy level game.
        /// Destroys this display.
        /// </summary>

        public void EasyLevelSelected()
        {
            Destroy(gameObject);
            GameObject.FindWithTag("PersistentObject").GetComponent<GameManager>().LevelButtonClicked(Level.Easy);
        }

        /// <summary>
        /// Handles the action when the "Medium" level is selected.
        /// Calls the GameManager to start the Medium level game.
        /// Destroys this display.
        /// </summary>

        public void MediumLevelSelected()
        {
            Destroy(gameObject);
            GameObject.FindWithTag("PersistentObject").GetComponent<GameManager>().LevelButtonClicked(Level.Medium);
        }
    }
}