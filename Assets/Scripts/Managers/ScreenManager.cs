using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    private static ScreenManager instance = null;

    GameObject levelPanel;
    GameObject settings;
    GameObject instructionsPage;
    GameObject characterPanel;
    GameObject gameOver;
    GameObject scoreMessage;

    public static ScreenManager GetInstance() {
        if (instance == null) {
            instance = new ScreenManager();
        }
        return instance;
    } 

    void Awake()
    {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else if(instance != this){
            Destroy(this.gameObject);
            return;
        }

        levelPanel = Resources.Load<GameObject>("Prefabs/Canvases/LevelPanel"); 
        settings = Resources.Load<GameObject>("Prefabs/Canvases/Settings"); 
        instructionsPage = Resources.Load<GameObject>("Prefabs/Canvases/InstructionsPage"); 
        characterPanel = Resources.Load<GameObject>("Prefabs/Canvases/CharacterPanel"); 
        gameOver = Resources.Load<GameObject>("Prefabs/Canvases/GameOver"); 
        scoreMessage = Resources.Load<GameObject>("Prefabs/Canvases/ScoreMessage"); 
    }

    public void GoToScene(SceneName name){
        SceneManager.LoadScene(name.ToString());
    }

    public void InstantiateScreen(CanvasName name, Transform parent){
        Debug.Log(name);
        Debug.Log(parent);
        switch(name){
            case CanvasName.LevelPanel:
            Debug.Log(levelPanel);
                 Instantiate(levelPanel, parent);
                 break;
            case CanvasName.Settings:
            Debug.Log(settings);
                 Instantiate(settings, parent);
                 break;
            case CanvasName.InstructionsPage:
            Debug.Log(instructionsPage);
                 Instantiate(instructionsPage, parent);
                 break;
            case CanvasName.CharacterPanel:
            Debug.Log(characterPanel);
                 Instantiate(characterPanel, parent);
                 break;
            case CanvasName.GameOver:
            Debug.Log(gameOver);
                 Instantiate(gameOver, parent);
                 break;
            case CanvasName.ScoreMessage:
            Debug.Log(scoreMessage);
                 Instantiate(scoreMessage, parent);
                 break;
        }
    }
}
