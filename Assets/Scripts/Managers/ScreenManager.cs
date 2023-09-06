using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Methods for scene transitions and instantiation of UI elements.
/// Uses the Singleton pattern to ensure only one instance of SoundManager exists.
/// </summary>

public class ScreenManager : MonoBehaviour
{
    private static ScreenManager instance = null; // Singleton instance
    
    // UI elements 
    GameObject levelPanel;
    GameObject typeOfGamePanel;
    GameObject settings;
    GameObject instructionsPage;
    GameObject characterPanel;
    GameObject gameOver;
    GameObject scoreMessage;

    /// <summary>
    /// Retrieves the singleton instance of SoundManager. 
    /// If it doesn't exist, a new instance is created.
    /// <returns>Current soundManager instance</returns>
    /// </summary>

    public static ScreenManager GetInstance()
    {
        if (instance == null)
        {
            instance = new ScreenManager();
        }
        return instance;
    }

    /// <summary>
    /// Initializes the ScreenManager on game start and ensures only one instance exists.
    /// Loads UI prefabs.
    /// </summary>
    void Awake()
    {
        // Singleton setup
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep active for entire game lifetime
        }
        else if (instance != this)
        {
            Destroy(this.gameObject); // Destroy extra instances
            return;
        }

        // Load UI prefabs from Resources
        levelPanel = Resources.Load<GameObject>("Prefabs/Canvases/LevelPanel");
        typeOfGamePanel = Resources.Load<GameObject>("Prefabs/Canvases/TypeOfGamePanel");
        settings = Resources.Load<GameObject>("Prefabs/Canvases/Settings");
        instructionsPage = Resources.Load<GameObject>("Prefabs/Canvases/InstructionsPage");
        characterPanel = Resources.Load<GameObject>("Prefabs/Canvases/CharacterPanel");
        gameOver = Resources.Load<GameObject>("Prefabs/Canvases/GameOver");
        scoreMessage = Resources.Load<GameObject>("Prefabs/Canvases/ScoreMessage");
    }

    /// <summary>
    /// Loads a new scene based on the given scene name.
    /// </summary>
    /// <param name="name">The name of the scene to load.</param>

    public void GoToScene(SceneName name)
    {
        SceneManager.LoadScene(name.ToString());
    }

    /// <summary>
    /// Instantiates the specified UI element, with the specified parent.
    /// </summary>
    /// <param name="name">The name of the UI element to instantiate.</param>
    /// <param name="parent">The transform to be set as the parent of the instantiated object.</param>

    public void InstantiateScreen(UIElementName name, Transform parent)
    {
        switch (name)
        {
            case UIElementName.LevelPanel:
                Instantiate(levelPanel, parent);
                break;
            case UIElementName.TypeOfGamePanel:
                Instantiate(typeOfGamePanel, parent);
                break;
            case UIElementName.Settings:
                Instantiate(settings, parent);
                break;
            case UIElementName.InstructionsPage:
                Instantiate(instructionsPage, parent);
                break;
            case UIElementName.CharacterPanel:
                Instantiate(characterPanel, parent);
                break;
            case UIElementName.GameOver:
                Instantiate(gameOver, parent);
                break;
            case UIElementName.ScoreMessage:
                Instantiate(scoreMessage, parent);
                break;
        }
    }
}
