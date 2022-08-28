using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tile : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public static GameObject itemBeingDragged;
    public static GameObject openTile;

    Vector3 startPosition;
    Transform startParent;
    int siblingIndex;

    // A sprite is assigned to a tile when it is clicked and this flag is set to true
    // If the dragged tile is not dropped in a valid slot, this flag is reset.
    bool hasSprite = false;

    // This flag is set to false when a tile is placed in a slot
    bool placedInSlot = false;
    
    // This flag indicates that a player started to drag this tile. It is needed to ensure that events like
    // OnDrag and OnEndDrag do not take an action unless the drag was started.
    bool dragStarted = false;
    int number = 0;

    public int Number {
        get{
            return number;
        }
    }

    public bool PlacedInSlot {
        get{
            return placedInSlot;
        }
        set{
            placedInSlot = value;
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if(!IsMovable()) {
            return;
        }
        if(!hasSprite) {
            Sprite s = GameObject.FindWithTag("MainCanvas").GetComponent<MainCanvas>().PickRandomSprite();
            SetSprite(s);
            Tile.openTile = gameObject;
        }
    }

    public void OnBeginDrag(PointerEventData eventData){
        if(!IsMovable()) {
            return;
           }
        dragStarted = true;
        if(!hasSprite) {
            Sprite s = GameObject.FindWithTag("MainCanvas").GetComponent<MainCanvas>().PickRandomSprite();
            SetSprite(s);
            Tile.openTile = gameObject;
        }
        siblingIndex = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
    
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        itemBeingDragged = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void SetSprite (Sprite s) {
        GetComponent<Image>().sprite = s;
        string name = s.name;
        name = name.Replace("Tile", "");
        number = int.Parse(name);
        hasSprite = true;
    }

    public void OnDrag(PointerEventData eventData){
        if(!dragStarted) {
            return;
        }
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData){
        if(!dragStarted) {
            return;
        }
        transform.localScale = new Vector3(1, 1, 1);
        itemBeingDragged = null;
        if(transform.parent == startParent) {
            transform.SetSiblingIndex(siblingIndex);
            transform.position = startPosition;
        } else {
            placedInSlot = true;
            Tile.openTile = null;
        }

        dragStarted = false;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    // This method checks if a tile can be moved, which checks
    // 1. Is there an already opened tile that hasn't been placed in a slot, if yes, is it this tile?
    // 2. Has it been already placed in a slot?
    // 3. If not, is it the players turn? If it's computers turn, then the player is not allowed to move tiles.
    private bool IsMovable() {
        // Check if an active game in progress, else don't allow to move
        if (!GameObject.FindWithTag("GameBoard").GetComponent<GameBoard>().ActiveGame) {
            return false;
        }
        // Check if it's the user playing
        if (GameObject.FindWithTag("GameBoard").GetComponent<TurnManager>().CurrentPlayer == PlayerStatus.You) {
            // Is the tile already in a slot - If yes, don't allow to move
            if (!placedInSlot) {
                // If already opened, allow player to move it
                if (hasSprite) {
                    return true;
                } else {
                    // Not in slot yet and is also not opened. Check if there is another opened tile that must be played first
                    return !isThereAlreadyAnOpenTile();
                }
            }
        } 
        return false;
    }

    private bool isThereAlreadyAnOpenTile() {
        return Tile.openTile != null;
    }
}
