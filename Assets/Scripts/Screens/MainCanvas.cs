using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    GameObject numberGrid;

    void Awake()
    {
        numberGrid = GameObject.FindWithTag("NumberGrid");
        if(GameObject.FindWithTag("PersistentObject").GetComponent<GameManager>().Type == GameType.Memory){
            launchMemoryVersion();
        }
    }

    public void launchMemoryVersion(){
        Debug.Log("Memory Play");
    }

    public GameObject GetTileToPlay()
    {
        return numberGrid.GetComponent<NumberGrid>().GetTile();
    }
}
