using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    GameObject left;
    GameObject right;
    GameObject top;
    GameObject bottom;
    string slotName;

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
        tile.transform.SetParent(transform, false);
        tile.transform.localPosition = Vector3.zero;
        tile.GetComponent<Tile>().PlacedInSlot = true;
        transform.parent.GetComponent<GameBoard>().SendPlayerEvent(gameObject);
    }

    public void OnDrop(PointerEventData eventData){
        if(!tile){
            GameObject itemDragged = Tile.itemBeingDragged;
            if(itemDragged != null) {
                DropTile(itemDragged);
                SoundManager.PlayClipByName(AudioClipName.TilePlaced);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!tile) {
            GetComponent<Outline>().enabled = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Outline>().enabled = false;
    }

    public bool IsGameLost(){
        int currentNumber = tile.GetComponent<Tile>().Number;
        return IsConsecutive(left, currentNumber) 
                || IsConsecutive(right, currentNumber) 
                || IsConsecutive(top, currentNumber) 
                || IsConsecutive(bottom, currentNumber);
    }

     public bool checkForAllConsecutiveSlots(int number){
        return IsConsecutive(left, number) 
                || IsConsecutive(right, number) 
                || IsConsecutive(top, number) 
                || IsConsecutive(bottom, number);
    }


    public bool IsConsecutive(GameObject neighboringSlot, int currentNumber) {
        if(neighboringSlot != null && neighboringSlot.GetComponent<Slot>().tile != null){
            int neighboringSlotNumber = neighboringSlot.GetComponent<Slot>().tile.GetComponent<Tile>().Number;
            if(Math.Abs(currentNumber - neighboringSlotNumber) == 1){
                return true;
            }
        }
        return false;
    }


}
