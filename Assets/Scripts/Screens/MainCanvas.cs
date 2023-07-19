using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    GameObject numberGrid;

    void Awake()
    {
        numberGrid = GameObject.FindWithTag("NumberGrid");
    }

    public GameObject GetTileToPlay()
    {
        return numberGrid.GetComponent<NumberGrid>().GetTile();
    }
}
