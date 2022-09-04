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
        switch(name){
            case CanvasName.LevelPanel:
                 Instantiate(levelPanel, parent);
                 break;
            case CanvasName.Settings:
                 Instantiate(settings, parent);
                 break;
            case CanvasName.InstructionsPage:
                 Instantiate(instructionsPage, parent);
                 break;
            case CanvasName.CharacterPanel:
                 Instantiate(characterPanel, parent);
                 break;
            case CanvasName.GameOver:
                 Instantiate(gameOver, parent);
                 break;
            case CanvasName.ScoreMessage:
                 Instantiate(scoreMessage, parent);
                 break;
        }
    }
}
