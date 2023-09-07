using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages general game settings and navigations between different scenes and UI panels.
/// </summary>

public class GameManager : MonoBehaviour
{
    public static GameVariations type; 
    public static Level level;

    void Awake()
    {
        //Load previously saved values.
        LoadPlayerPrefValues();
    }

    /// <summary>
    /// Gets the game variation type.
    /// </summary>

    public GameVariations Type
    {
        get { return type; }
    }

    /// <summary>
    /// Loads saved volume values from player preferences and sets it.
    /// sets the volume to default if no saved preference is found.
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
        type = GameVariations.Original;
        ScreenManager.GetInstance().InstantiateScreen(UIElementName.LevelPanel, GameObject.FindWithTag("GameStartCanvas").transform);
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

    /// <summary>
    /// Sets the game variation type to "Memory" and opens the character display.
    /// </summary>

    public void MemoryPlayClicked()
    {
        type = GameVariations.Memory;
        ScreenManager.GetInstance().InstantiateScreen(UIElementName.CharacterPanel, GameObject.FindWithTag("GameStartCanvas").transform);
    }
}


