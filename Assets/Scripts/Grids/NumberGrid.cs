using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberGrid : MonoBehaviour
{
    static int numberOfTilesNotPlacedYet;

    void Start(){
        numberOfTilesNotPlacedYet = transform.childCount;
    }   

    public GameObject GetTile() {
        int randomTileIndex = Random.Range(0, transform.childCount - 1);
        return transform.GetChild(randomTileIndex).gameObject;
    }

    public static int GetRemainingTilesCount() {
        return numberOfTilesNotPlacedYet;
    }

    public static void tilePlaced(){
        numberOfTilesNotPlacedYet -= 1;
    }
}
