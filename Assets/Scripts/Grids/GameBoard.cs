using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using Managers;
using Sound;
using Utils;
using UIElements;
using HelperEnums;

namespace Grids
{
    /// <summary>
    /// Represents and manages the state and behavior of the game board.
    /// </summary>

    public class GameBoard : MonoBehaviour
    {
        // Represents the board slots that are currently empty.
        private Dictionary<string, GameObject> emptySlots;

        // Represents board slots that are occupied.
        private Dictionary<string, GameObject> occupiedSlots;

        // Reference to the last tile that was placed on the board.
        private GameObject lastDroppedTile;

        // Tile that the computer intends to place on its turn.
        private GameObject computersTileToPlace;

        //Computer timer for dropping tile.
        Timer dropTimer;

        // The selected game difficulty level.
        private Level selectedLevel;

        // Flag indicating whether the game is currently active.
        private bool activeGame = false;

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
        }

        /// <summary>
        /// Gets the value indicating whether the game is active.
        /// </summary>

        public bool ActiveGame
        {
            get
            {
                return activeGame;
            }
        }

        /// <summary>
        /// Handles the outcome after a player makes a move.
        /// </summary>
        /// <param name="slot">The slot where the player placed their tile.</param>

        public void SendPlayerEvent(GameObject slot)
        {
            highlightLatestPlayedTile(slot.transform.GetChild(0).gameObject);

            moveEmptySlotToOccupied(slot.name);
            GameObject currentNumber;
            GameObject neighbor;
            bool isLost = slot.GetComponent<Slot>().IsGameLost(out currentNumber, out neighbor);
            PlayerStatus currentPlayer = TurnManager.currentPlayer;

            if (isLost)
            {
                SoundManager.PlayAudioXTimes(AudioClipName.Alarm, 1);
                PlayLostAnimation(currentNumber, neighbor);
                Camera.main.GetComponent<MainGame>().gameOver(currentPlayer, GameStatus.Lost, "Opponent", currentNumber, neighbor);
                activeGame = false;
                GetComponent<TurnManager>().EndGame();
            }
            else if (emptySlots.Count == 0)
            {
                Camera.main.GetComponent<MainGame>().gameOver(currentPlayer, GameStatus.Tied, "Opponent", null, null);
                activeGame = false;
                GetComponent<TurnManager>().EndGame();
            }
            else
            {
                GetComponent<TurnManager>().ChangeTurn();
            }
        }

        /// <summary>
        /// Plays the lost animation for the tiles causing the loss.
        /// </summary>
        /// <param name="current">The losing tile.</param>
        /// <param name="neighbor">The neighboring tile to the losing one.</param>

        private void PlayLostAnimation(GameObject current, GameObject neighbor)
        {
            current.GetComponent<Animator>().enabled = true;
            current.GetComponent<Animator>().Play("TileInLosingSlot", 0, 0f);
            neighbor.GetComponent<Animator>().enabled = true;
            neighbor.GetComponent<Animator>().Play("TileInLosingSlot", 0, 0f);
        }

        /// <summary>
        /// Executes the computer's turn logic.
        /// </summary>

        public void PlayComputersTurn()
        {
            computersTileToPlace = transform.parent.GetComponent<MainCanvas>().GetTileToPlay();
            Sprite s = GameObject.FindWithTag("NumberGrid").GetComponent<NumberGrid>().GetSpriteForTile(computersTileToPlace);
            computersTileToPlace.GetComponent<Tile>().SetSprite(s);
            computersTileToPlace.GetComponent<Animator>().enabled = true;
            computersTileToPlace.GetComponent<Animator>().Play("TileFlip", 0, 0f);
            SoundManager.PlayClipByName(AudioClipName.Swoosh);
            int lower = 9 % (NumberGrid.GetRemainingTilesCount());
            int duration = Random.Range(lower + 1, lower + 2);
            Invoke("PlaceTile", duration);
        }

        /// <summary>
        /// Place tile in a valid slot for the computer's turn.
        /// For the easy level, if there are fewer than four tiles left, pick a random slot.
        /// </summary>
        public void PlaceTile()
        {
            GameObject validSlotToPlay = null;
            switch (selectedLevel)
            {
                case Level.Medium:
                    validSlotToPlay = GetValidSlotForTile(computersTileToPlace);
                    break;

                case Level.Easy:
                    if (NumberGrid.GetRemainingTilesCount() >= 4)
                    {
                        validSlotToPlay = GetValidSlotForTile(computersTileToPlace);
                    }
                    else
                    {
                        validSlotToPlay = GetRandomSlot(computersTileToPlace);
                    }
                    break;
            }
            validSlotToPlay.GetComponent<Slot>().DropTile(computersTileToPlace);

        }

        /// <summary>
        /// Get a valid slot for the tile.
        /// </summary>

        private GameObject GetValidSlotForTile(GameObject tile)
        {
            List<GameObject> validSlots = new List<GameObject>();
            for (int i = 0; i < emptySlots.Count; i++)
            {
                KeyValuePair<string, GameObject> emptySlot = emptySlots.ElementAt(i);
                GameObject slotObject = emptySlot.Value;
                bool invalid = slotObject.GetComponent<Slot>().checkForAllConsecutiveSlots(tile);
                if (invalid == false)
                {
                    validSlots.Add(slotObject);
                }
            }

            if (validSlots.Count == 0)
            {
                int index = Random.Range(0, emptySlots.Count);
                return emptySlots.ElementAt(index).Value;
            }
            else
            {
                int index = Random.Range(0, validSlots.Count);
                return validSlots[index];
            }

        }

        /// <summary>
        /// Get a random slot for the tile.
        /// </summary>

        private GameObject GetRandomSlot(GameObject tile)
        {
            int index = Random.Range(0, emptySlots.Count - 1);
            return emptySlots.ElementAt(index).Value;
        }

        /// <summary>
        /// Update lists for empty slots and occupied slots.
        /// </summary>

        private void moveEmptySlotToOccupied(string slotKey)
        {
            GameObject removedObject;
            emptySlots.Remove(slotKey, out removedObject);
            occupiedSlots.Add(removedObject.name, removedObject);
        }

        private void highlightLatestPlayedTile(GameObject latestTile)
        {
            // Change the Tile Image Sprite to Highlighted One
            string highlightedSpriteName = latestTile.GetComponent<Image>().sprite.name + "H";
            if (GameObject.FindWithTag("PersistentObject").GetComponent<AssetLoader>().GetHighlightedSpriteByName(highlightedSpriteName) != null)
            {
                latestTile.GetComponent<Image>().sprite = GameObject.FindWithTag("PersistentObject").GetComponent<AssetLoader>().GetHighlightedSpriteByName(highlightedSpriteName);
                // Reset the sprite for the last dropped tile back to normal
                if (lastDroppedTile != null)
                {
                    string spritename = lastDroppedTile.GetComponent<Image>().sprite.name.Replace("H", "");
                    lastDroppedTile.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + spritename);
                }

                // Update the lastDroppedTile 
                lastDroppedTile = latestTile;
            }
        }
    }
}
