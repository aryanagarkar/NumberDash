using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class GameBoard : MonoBehaviour
{
    Dictionary<string, GameObject> emptySlots;
    Dictionary<string, GameObject> occupiedSlots;
    GameObject lastDroppedTile;
    GameObject computersTileToPlace;
    Timer dropTimer;
    Level selectedLevel;
    bool activeGame = false;

    void Awake()
    {
        emptySlots = new Dictionary<string, GameObject>();
        occupiedSlots = new Dictionary<string, GameObject>();
        Transform[] trans = gameObject.GetComponentsInChildren<Transform>();
        Transform[] children = trans.Skip(1).ToArray();
        foreach (Transform child in children)
        {
            emptySlots.Add(child.gameObject.name, child.gameObject);
        }
        selectedLevel = GameManager.level;
        dropTimer = gameObject.AddComponent<Timer>();
        activeGame = true;
        GetComponent<TurnManager>().StartGame();
    }

    public bool ActiveGame {
        get {
            return activeGame;
        }
    }

    public void SendPlayerEvent(GameObject slot) {
        highlightLatestPlayedTile(slot.transform.GetChild(0).gameObject);

        moveEmptySlotToOccupied(slot.name);
        GameObject currentNumber;
        GameObject neighbor;
        bool isLost = slot.GetComponent<Slot>().IsGameLost(out currentNumber, out neighbor);
        PlayerStatus currentPlayer = TurnManager.currentPlayer;

        if (isLost) {
            SoundManager.PlayAudioXTimes(AudioClipName.Alarm, 1);
            PlayLostAnimation(currentNumber, neighbor);
            Camera.main.GetComponent<MainGame>().gameOver(currentPlayer, GameStatus.Lost, "Opponent", currentNumber, neighbor);
            activeGame = false;
            GetComponent<TurnManager>().EndGame();
        } else if(emptySlots.Count == 0) {
            Camera.main.GetComponent<MainGame>().gameOver(currentPlayer, GameStatus.Tied, "Opponent", null, null);
            activeGame = false;
            GetComponent<TurnManager>().EndGame();
        } else { 
            GetComponent<TurnManager>().ChangeTurn();
        } 
    }

    private void PlayLostAnimation(GameObject current, GameObject neighbor){
        current.GetComponent<Animator>().enabled = true;
        current.GetComponent<Animator>().Play("TileInLosingSlot", 0, 0f);
        neighbor.GetComponent<Animator>().enabled = true;
        neighbor.GetComponent<Animator>().Play("TileInLosingSlot", 0, 0f);
    }

    public void PlayComputersTurn() {
        Sprite s = transform.parent.GetComponent<MainCanvas>().PickRandomSprite();
        computersTileToPlace = transform.parent.GetComponent<MainCanvas>().GetTileToPlay();
        computersTileToPlace.GetComponent<Animator>().enabled = true;
        computersTileToPlace.GetComponent<Animator>().Play("TileFlip", 0, 0f);
        SoundManager.PlayClipByName(AudioClipName.Swoosh);
        computersTileToPlace.GetComponent<Tile>().SetSprite(s);
        int lower = 9 % (NumberGrid.GetRemainingTilesCount());
        int duration = Random.Range(lower+1, lower+2);
        Invoke("PlaceTile", duration);
    }

    public void PlaceTile(){
        GameObject validSlotToPlay = null;
        switch (selectedLevel)
        {
            case Level.Medium :
             validSlotToPlay = GetValidSlotForTile(computersTileToPlace);
             break;
            
            case Level.Easy :
            if(NumberGrid.GetRemainingTilesCount() >= 4){
                validSlotToPlay = GetValidSlotForTile(computersTileToPlace);
            }
            else{
                validSlotToPlay = GetRandomSlot(computersTileToPlace);
            }
             break;
        }
        validSlotToPlay.GetComponent<Slot>().DropTile(computersTileToPlace);
        
    }

    private GameObject GetValidSlotForTile(GameObject tile){
        List<GameObject> validSlots = new List<GameObject>();
        for(int i = 0; i < emptySlots.Count; i++){
            KeyValuePair<string, GameObject> emptySlot = emptySlots.ElementAt(i);
            GameObject slotObject = emptySlot.Value;
            bool invalid = slotObject.GetComponent<Slot>().checkForAllConsecutiveSlots(tile);
            if(invalid == false){
                validSlots.Add(slotObject);
            }
        }
        
        if(validSlots.Count == 0) {
            int index = Random.Range(0, emptySlots.Count);
            return emptySlots.ElementAt(index).Value;
        } else {
            int index = Random.Range(0, validSlots.Count);
            return validSlots[index];
        }
        
    }

     private GameObject GetRandomSlot(GameObject tile){
        int index = Random.Range(0, emptySlots.Count - 1);
        return emptySlots.ElementAt(index).Value;
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
            //latestTile.GetComponent<NicerOutline>().enabled = true;
            if(lastDroppedTile != null) {
                string spritename = lastDroppedTile.GetComponent<Image>().sprite.name.Replace("H", "");
                lastDroppedTile.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + spritename);
                //lastDroppedTile.GetComponent<NicerOutline>().enabled = false;
            }

            // Update the lastDroppedTile 
            lastDroppedTile = latestTile;
        }
    }
}
