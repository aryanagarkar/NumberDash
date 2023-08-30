using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberGrid : MonoBehaviour
{
    static int numberOfTilesNotPlacedYet;

    Dictionary<GameObject, Sprite> tileNumbers;

    void Awake()
    {
        numberOfTilesNotPlacedYet = transform.childCount;
        //Pick sprite for each tile
        tileNumbers = new Dictionary<GameObject, Sprite>();
        for(int i = 0; i < transform.childCount; i++){
            tileNumbers.Add(transform.GetChild(i).gameObject, GameObject.FindWithTag("PersistentObject").GetComponent<AssetLoader>().PickRandomSprite());
        }
    }

    public Sprite GetSpriteForTile(GameObject tile){
        return tileNumbers[tile];
    }

    public GameObject GetTile()
    {
        int randomTileIndex = Random.Range(0, transform.childCount - 1);
        return transform.GetChild(randomTileIndex).gameObject;
    }

    public static int GetRemainingTilesCount()
    {
        return numberOfTilesNotPlacedYet;
    }

    public static void tilePlaced()
    {
        numberOfTilesNotPlacedYet -= 1;
    }

    public void flipTilesFaceUp(){
        for(int i = 0; i < transform.childCount; i++){
            GameObject tile = transform.GetChild(i).gameObject;
            tile.GetComponent<Tile>().SetSprite(tileNumbers[transform.GetChild(i).gameObject]);
            tile.GetComponent<Animator>().enabled = true;
            tile.GetComponent<Animator>().Play("TileFlip", 0, 0f);
            SoundManager.PlayClipByName(AudioClipName.Swoosh);
        }
    }

    public void flipTilesFaceDown(){
        for(int i = 0; i < transform.childCount; i++){
            GameObject tile = transform.GetChild(i).gameObject;
            tile.GetComponent<Tile>().ResetSpriteToBlank();
            tile.GetComponent<Animator>().enabled = true;
            tile.GetComponent<Animator>().Play("TileFlip", 0, 0f);
            SoundManager.PlayClipByName(AudioClipName.Swoosh);
        }
    }
}
