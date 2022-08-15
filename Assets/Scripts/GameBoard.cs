using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameBoard : MonoBehaviour
{
    Dictionary<string, GameObject> emptySlots;
    Dictionary<string, GameObject> occupiedSlots;
    GameObject lastDroppedTile;
    GameObject computersTileToPlace;
    Timer dropTimer;


    void Start()
    {
        emptySlots = new Dictionary<string, GameObject>();
        occupiedSlots = new Dictionary<string, GameObject>();
        Transform[] trans = gameObject.GetComponentsInChildren<Transform>();
        Transform[] children = trans.Skip(1).ToArray();
        foreach (Transform child in children)
        {
            emptySlots.Add(child.gameObject.name, child.gameObject);
        }

    dropTimer = gameObject.AddComponent<Timer>();
    dropTimer.Duration = 3;
    }

    public void Update(){
         if (dropTimer.Finished)
        {
            placeTile(computersTileToPlace);
            dropTimer.stop();
        }
    }

    public void SendPlayerEvent(GameObject slot) {
        highlightLatestPlayedTile(slot.transform.GetChild(0).gameObject);

        moveEmptySlotToOccupied(slot.name);

        bool lost = slot.GetComponent<Slot>().IsGameLost();
        Player currentPlayer = TurnManager.currentPlayer;

        if (lost) {
             Camera.main.GetComponent<MainGame>().gameOver(currentPlayer, GameStatus.lost);
              GameObject.FindWithTag("TurnText").GetComponent<TurnManager>().finishGame();
        } else if(emptySlots.Count == 0) {
            Camera.main.GetComponent<MainGame>().gameOver(currentPlayer, GameStatus.tied);
             GameObject.FindWithTag("TurnText").GetComponent<TurnManager>().finishGame();
        } else { 
            GameObject.FindWithTag("TurnText").GetComponent<TurnManager>().ChangeTurn();
        } 
    }

    public void PlayComputersTurn() {
        Sprite s = transform.parent.GetComponent<MainCanvas>().PickRandomSprite();
        computersTileToPlace = transform.parent.GetComponent<MainCanvas>().GetTileToPlay();
        computersTileToPlace.GetComponent<Tile>().SetSprite(s);
        dropTimer.Run();
    }

    public void placeTile(GameObject tile){
        KeyValuePair<string, GameObject> validSlotToPlay = GetValidSlotForTile(tile);
        validSlotToPlay.Value.GetComponent<Slot>().DropTile(tile);
    }

    private KeyValuePair<string, GameObject> GetValidSlotForTile(GameObject tile){
        for(int i = 0; i < emptySlots.Count; i++){
            KeyValuePair<string, GameObject> emptySlot = emptySlots.ElementAt(i);
            GameObject slotObject = emptySlot.Value;
            bool invalid = slotObject.GetComponent<Slot>().checkForAllConsecutiveSlots(tile.GetComponent<Tile>().Number);
            if(invalid == false){
                return emptySlot;
            }
        }
        int index = Random.Range(0, emptySlots.Count);
        return emptySlots.ElementAt(index);
    }
    
    private void moveEmptySlotToOccupied(string slotKey) {
        GameObject removedObject;
        emptySlots.Remove(slotKey, out removedObject);
        occupiedSlots.Add(removedObject.name, removedObject);
    }

    private void highlightLatestPlayedTile(GameObject latestTile) {
        // Change the Tile Image Sprite to Highlighted One
        string highlightedSpriteName = latestTile.GetComponent<Image>().sprite.name + "H";
        if (transform.parent.GetComponent<MainCanvas>().GetHighlightedSpriteByName(highlightedSpriteName) != null) {
            latestTile.GetComponent<Image>().sprite = transform.parent.GetComponent<MainCanvas>().GetHighlightedSpriteByName(highlightedSpriteName);

            // Reset the sprite for the last dropped tile back to normal
            if(lastDroppedTile != null) {
                string spritename = lastDroppedTile.GetComponent<Image>().sprite.name.Replace("H", "");
                lastDroppedTile.GetComponent<Image>().sprite = Resources.Load<Sprite>(spritename);
            }

            // Update the lastDroppedTile 
            lastDroppedTile = latestTile;
        }
    }
}
