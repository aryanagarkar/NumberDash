using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameVariations type;
    public static Level level;
    void Awake()
    {
        LoadPlayerPrefValues();
    }

    public GameVariations Type {
        get { return type; }
    }

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

    public void goToMainGame()
    {
        ScreenManager.GetInstance().GoToScene(SceneName.MainGameBoard);
    }

    public void OriginalPlayClicked(){
        type = GameVariations.Original;
        ScreenManager.GetInstance().InstantiateScreen(UIElementName.LevelPanel, GameObject.FindWithTag("GameStartCanvas").transform);
    }
    
    public void LevelButtonClicked(Level levelName)
    {
        level = levelName;
        ScreenManager.GetInstance().InstantiateScreen(UIElementName.CharacterPanel, GameObject.FindWithTag("GameStartCanvas").transform);
    }

    public void MemoryPlayClicked()
    {
        type = GameVariations.Memory;
        ScreenManager.GetInstance().InstantiateScreen(UIElementName.CharacterPanel, GameObject.FindWithTag("GameStartCanvas").transform);
    }
}
