using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UI.Extensions;
using Managers;
using Sound;
using HelperEnums;

namespace Grids
{
    /// <summary>
    /// Represents a slot in a game grid where tiles can be placed.
    /// Also checks for losing tiles on every turn.
    /// </summary>

    public class Slot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        //Neighboring slots to this one.
        GameObject left;
        GameObject right;
        GameObject top;
        GameObject bottom;

        string slotName;

        Color losingSpotColor = Color.red;
        Color consecutiveTileColor = Color.blue;

        void Start()
        {
            slotName = gameObject.name;

            // Retrieve the neighboring slots based on this slot's position.
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

        /// <summary>
        /// Retrieves the tile that is currently in this slot. Returns null if the slot is empty.
        /// </summary>

        public GameObject tile
        {
            get
            {
                if (transform.childCount > 0)
                {
                    return transform.GetChild(0).gameObject;
                }
                return null;
            }
        }

        /// <summary>
        /// Drops a specified tile into this slot.
        /// </summary>

        public void DropTile(GameObject tile)
        {
            int childIndex = tile.GetComponent<Tile>().SiblingIndex;

            // Set parent and adjust position/scale of the tile to drop.
            tile.transform.SetParent(transform, false);
            tile.transform.localPosition = Vector3.zero;
            tile.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);

            // Update tile status and invoke related game events.
            tile.GetComponent<Tile>().PlacedInSlot = true;
            SoundManager.PlayClipByName(AudioClipName.TilePlaced);
            NumberGrid.tilePlaced();
            transform.parent.GetComponent<GameBoard>().SendPlayerEvent(gameObject);
            GameObject.FindWithTag("GameBoard").GetComponent<TurnManager>().TileDropped();
        }

        /// <summary>
        /// Handle the tile drop event.
        /// </summary>

        public void OnDrop(PointerEventData eventData)
        {
            if (!tile)
            {
                GameObject itemDragged = Tile.itemBeingDragged;
                if (itemDragged != null)
                {
                    //Drop the tile and play sound if tile is valid and the slot is empty.
                    DropTile(itemDragged);
                    SoundManager.PlayClipByName(AudioClipName.TilePlaced);
                }

                //Turn of the highlight on the slot.
                GetComponent<NicerOutline>().enabled = false;
            }
        }

        /// <summary>
        /// Handle mouse pointer entering the slot.
        /// </summary>

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!tile)
            {
                //Highlight the slot if there isin't a tile there.
                GetComponent<NicerOutline>().enabled = true;
            }
        }

        /// <summary>
        /// Handle mouse pointer exiting the slot.
        /// </summary>

        public void OnPointerExit(PointerEventData eventData)
        {
            //Turn of the highlight on the slot.
            GetComponent<NicerOutline>().enabled = false;
        }

        /// <summary>
        /// Checks if placing a tile in this slot results in a loss.
        /// </summary>
        /// <returns>True if game is lost, otherwise false.</returns>

        public bool IsGameLost(out GameObject currentNumber, out GameObject neighbor)
        {
            currentNumber = tile;
            return checkForAllConsecutiveSlots(currentNumber, out neighbor);
        }

        private bool checkForAllConsecutiveSlots(GameObject number, out GameObject neighbor)
        {
            // Check all neighboring slots for consecutive numbers
            return IsConsecutive(left, number, out neighbor)
                    || IsConsecutive(right, number, out neighbor)
                    || IsConsecutive(top, number, out neighbor)
                    || IsConsecutive(bottom, number, out neighbor);
        }

        public bool checkForAllConsecutiveSlots(GameObject number)
        {
            GameObject neighbor;
            return checkForAllConsecutiveSlots(number, out neighbor);
        }

        /// <summary>
        /// Determines if a number in the provided neighboring slot is consecutive to the current number.
        /// </summary>
        /// <returns>True if the tiles are consecutive, otherwise false.</returns>

        private bool IsConsecutive(GameObject neighboringSlot, GameObject currentNumber, out GameObject neighbor)
        {
            if (neighboringSlot != null && neighboringSlot.GetComponent<Slot>().tile != null)
            {
                neighbor = neighboringSlot.GetComponent<Slot>().tile;
                if (Math.Abs(currentNumber.GetComponent<Tile>().Number - neighbor.GetComponent<Tile>().Number) == 1)
                {
                    return true;
                }
            }
            neighbor = null;
            return false;
        }
    }
}