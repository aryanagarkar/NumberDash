using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a grid of numbered tiles, with functionalities to manipulate tile properties.
/// </summary>

public class NumberGrid : MonoBehaviour
{
    // Tracks the number of tiles that haven't been placed
    static int numberOfTilesNotPlacedYet;

    // Dictionary to map tiles to their associated sprites.
    Dictionary<GameObject, Sprite> tileNumbers;

    void Awake()
    {
        //At the beginning of the game, no tiles are placed.
        numberOfTilesNotPlacedYet = transform.childCount;

        tileNumbers = new Dictionary<GameObject, Sprite>();

        // Load a random sprite for each tile and store the mapping.
        for(int i = 0; i < transform.childCount; i++){
            tileNumbers.Add(transform.GetChild(i).gameObject, GameObject.FindWithTag("PersistentObject").GetComponent<AssetLoader>().PickRandomSprite());
        }
    }

    /// <summary>
    /// Retrieves the sprite associated with the given tile.
    /// </summary>
    /// <param name="tile">The tile GameObject that the sprite is requested for.</param>
    /// <returns>Sprite associated with the tile.</returns>

    public Sprite GetSpriteForTile(GameObject tile){
        return tileNumbers[tile];
    }

    /// <summary>
    ///<returns>A random tile from the grid.</returns>
    /// </summary>

    public GameObject GetTile()
    {
        int randomTileIndex = Random.Range(0, transform.childCount - 1);
        return transform.GetChild(randomTileIndex).gameObject;
    }

    /// <summary>
    ///<returns>The number of tiles not placed yet.</returns>
    /// </summary>

    public static int GetRemainingTilesCount()
    {
        return numberOfTilesNotPlacedYet;
    }

    /// <summary>
    /// Called when a tile is placed.
    /// Decreases the count of tiles that haven't been placed.
    /// </summary>

    public static void tilePlaced()
    {
        numberOfTilesNotPlacedYet -= 1;
    }

    /// <summary>
    /// Flips all tiles face up (setting their sprites and playing a flip animation).
    /// </summary>

    public void flipTilesFaceUp(){
        for(int i = 0; i < transform.childCount; i++){
            GameObject tile = transform.GetChild(i).gameObject;
            tile.GetComponent<Tile>().SetSprite(tileNumbers[transform.GetChild(i).gameObject]);
            tile.GetComponent<Animator>().enabled = true;
            tile.GetComponent<Animator>().Play("TileFlip", 0, 0f);
            SoundManager.PlayClipByName(AudioClipName.Swoosh);
        }
    }

    /// <summary>
    /// Flips all tiles face up (resetting their sprites to blank and playing a flip animation).
    /// </summary>
    
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
