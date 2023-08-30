using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UI.Extensions;

public class Slot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    GameObject left;
    GameObject right;
    GameObject top;
    GameObject bottom;
    string slotName;

    Color losingSpotColor = Color.red;
    Color consecutiveTileColor = Color.blue;

    void Start(){
        slotName = gameObject.name;
        string[] indices = slotName.Split(",");
        int leftColumn = int.Parse(indices[1]) - 1;
        int rightColumn = int.Parse(indices[1]) + 1;
        int aboveRow = int.Parse(indices[0]) - 1;
        int belowRow = int.Parse(indices[0]) + 1;

        left = GameObject.Find(indices[0] + "," + leftColumn.ToString());
        right = GameObject.Find(indices[0] + "," + rightColumn.ToString());
        top = GameObject.Find(aboveRow.ToString() + "," + indices[1]);
        bottom = GameObject.Find(belowRow.ToString() + "," + indices[1]);
    }


    public GameObject tile {
        get{
            if(transform.childCount > 0){
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    public void DropTile(GameObject tile) {
        int childIndex = tile.GetComponent<Tile>().SiblingIndex;

        tile.transform.SetParent(transform, false);
        tile.transform.localPosition = Vector3.zero;
        tile.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);

        if(GameObject.FindWithTag("PersistentObject").GetComponent<GameManager>().Type == GameType.Memory){
            GameObject.FindWithTag("NumberGrid").GetComponent<NumberGrid>().ReplaceTileToMaintainGridPosition(tile, childIndex);
        }

        tile.GetComponent<Tile>().PlacedInSlot = true;
        SoundManager.PlayClipByName(AudioClipName.TilePlaced);
        NumberGrid.tilePlaced();
        transform.parent.GetComponent<GameBoard>().SendPlayerEvent(gameObject);
        GameObject.FindWithTag("GameBoard").GetComponent<TurnManager>().TileDropped();
    }

    public void OnDrop(PointerEventData eventData){
        if(!tile){
            GameObject itemDragged = Tile.itemBeingDragged;
            if(itemDragged != null) {
                DropTile(itemDragged);
                SoundManager.PlayClipByName(AudioClipName.TilePlaced);
            }
            GetComponent<NicerOutline>().enabled = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!tile) {
            GetComponent<NicerOutline>().enabled = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<NicerOutline>().enabled = false;
    }

    public bool IsGameLost(out GameObject currentNumber, out GameObject neighbor){
        currentNumber = tile;
        return checkForAllConsecutiveSlots(currentNumber, out neighbor);
    }

    private bool checkForAllConsecutiveSlots(GameObject number, out GameObject neighbor){
        return IsConsecutive(left, number, out neighbor) 
                || IsConsecutive(right, number, out neighbor) 
                || IsConsecutive(top, number, out neighbor) 
                || IsConsecutive(bottom, number, out neighbor);
    }

    public bool checkForAllConsecutiveSlots(GameObject number){
        GameObject neighbor;
        return checkForAllConsecutiveSlots(number, out neighbor);
    }

    private bool IsConsecutive(GameObject neighboringSlot, GameObject currentNumber, out GameObject neighbor) {
        if(neighboringSlot != null && neighboringSlot.GetComponent<Slot>().tile != null){
            neighbor = neighboringSlot.GetComponent<Slot>().tile;
            if(Math.Abs(currentNumber.GetComponent<Tile>().Number - neighbor.GetComponent<Tile>().Number) == 1){
                return true;
            }
        }
        neighbor = null;
        return false;
    }
}
