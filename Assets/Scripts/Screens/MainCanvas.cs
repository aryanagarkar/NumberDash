using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    GameObject numberGrid;
    Timer memoryTimer;

    void Awake()
    {
        numberGrid = GameObject.FindWithTag("NumberGrid");
        memoryTimer = gameObject.AddComponent<Timer>();
        memoryTimer.Duration = 5;
        GameObject.FindWithTag("GameBoard").GetComponent<TurnManager>().GameSetUp();
        if(GameObject.FindWithTag("PersistentObject").GetComponent<GameManager>().Type == GameType.Original){
            GameObject.FindWithTag("GameBoard").GetComponent<TurnManager>().StartGame();
        }
        else if(GameObject.FindWithTag("PersistentObject").GetComponent<GameManager>().Type == GameType.Memory){
            launchMemoryVersionDelay();
        }
    }

    void Update(){
        if(memoryTimer.Finished){
            memoryTimer.Stop();
            GameObject.FindWithTag("NumberGrid").GetComponent<NumberGrid>().flipTilesFaceDown();
            GameObject.FindWithTag("GameBoard").GetComponent<TurnManager>().StartGame();
        }
    }

    public void launchMemoryVersionDelay(){
        GameObject.FindWithTag("NumberGrid").GetComponent<NumberGrid>().flipTilesFaceUp();
        memoryTimer.Run();
    }

    public GameObject GetTileToPlay()
    {
        return numberGrid.GetComponent<NumberGrid>().GetTile();
    }
}
