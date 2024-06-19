using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using HelperEnums;

namespace Managers
{
    /// <summary>
    /// Manages general game settings and navigations between different scenes and UI elements.
    /// </summary>

    public class GameManager : MonoBehaviour
    {
        public static GameVariation type;
        public static Level level;

        void Awake()
        {
            // Load previously saved values.
            LoadPlayerPrefValues();
        }

        /// <summary>
        /// Game type property.
        /// Gets the game variation type.
        /// </summary>

        public GameVariation Type
        {
            get { return type; }
        }

        /// <summary>
        /// Loads saved volume values from player preferences and sets the game volume.
        /// Sets the volume to default if no saved preference is found.
        /// </summary>

        private void LoadPlayerPrefValues()
        {
            if (PlayerPrefs.HasKey("VolumeValue"))
            {
                float volumeValue = PlayerPrefs.GetFloat("VolumeValue");
                AudioListener.volume = volumeValue;
            }
            else
            {
                AudioListener.volume = 1;
            }
        }

        /// <summary>
        /// Transitions to the main game scene.
        /// </summary>

        public void goToMainGame()
        {
            ScreenManager.GetInstance().GoToScene(SceneName.MainGameBoard);
        }

        /// <summary>
        /// Sets the game variation to "Original" and opens the level display.
        /// </summary>

        public void OriginalPlayClicked()
        {
            type = GameVariation.Original;
            ScreenManager.GetInstance().InstantiateScreen(UIElementName.LevelPanel, GameObject.FindWithTag("GameStartCanvas").transform);
        }

        /// <summary>
        /// Sets the game variation type to "Memory" and opens the character display.
        /// </summary>
        public void MemoryPlayClicked()
        {
            type = GameVariation.Memory;
            ScreenManager.GetInstance().InstantiateScreen(UIElementName.CharacterPanel, GameObject.FindWithTag("GameStartCanvas").transform);
        }

        /// <summary>
        /// Sets the game level and opens the character display.
        /// </summary>
        /// <param name="levelName">The selected game level.</param>

        public void LevelButtonClicked(Level levelName)
        {
            level = levelName;
            ScreenManager.GetInstance().InstantiateScreen(UIElementName.CharacterPanel, GameObject.FindWithTag("GameStartCanvas").transform);
        }
    }
}