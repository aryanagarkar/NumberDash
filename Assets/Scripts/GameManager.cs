using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameType type;
    public static Level level;
    void Awake()
    {
        LoadPlayerPrefValues();
    }

    void Update()
    {

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
        //play memory if memory is chosen
    }
    public void EasyButtonClicked()
    {
        type = GameType.Original;
        level = Level.Easy;
        ScreenManager.GetInstance().InstantiateScreen(CanvasName.CharacterPanel, GameObject.FindWithTag("GameStartCanvas").transform);
    }

    public void MediumButtonClicked()
    {
        type = GameType.Original;
        level = Level.Medium;
        ScreenManager.GetInstance().InstantiateScreen(CanvasName.CharacterPanel, GameObject.FindWithTag("GameStartCanvas").transform);
    }

    public void MemoryPlayClicked()
    {
        type = GameType.Memory;
        //ScreenManager.GetInstance().InstantiateScreen(CanvasName.CharacterPanel, GameObject.FindWithTag("GameStartCanvas").transform);
    }
}
