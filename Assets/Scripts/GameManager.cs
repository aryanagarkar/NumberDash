using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameType type;
    public static Level level;
    void Awake()
    {
        LoadPlayerPrefValues();
    }

    public GameType Type {
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
        type = GameType.Original;
        ScreenManager.GetInstance().InstantiateScreen(CanvasName.LevelPanel, GameObject.FindWithTag("GameStartCanvas").transform);
    }
    
    public void LevelButtonClicked(Level levelName)
    {
        level = levelName;
        ScreenManager.GetInstance().InstantiateScreen(CanvasName.CharacterPanel, GameObject.FindWithTag("GameStartCanvas").transform);
    }

    public void MemoryPlayClicked()
    {
        type = GameType.Memory;
        ScreenManager.GetInstance().InstantiateScreen(CanvasName.CharacterPanel, GameObject.FindWithTag("GameStartCanvas").transform);
    }
}
