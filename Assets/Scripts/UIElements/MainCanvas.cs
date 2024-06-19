using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grids;
using Managers;
using Utils;
using HelperEnums;

namespace UIElements
{
    /// <summary>
    /// Controls the main canvas functionalities and manages the start of each game variation.
    /// </summary>

    public class MainCanvas : MonoBehaviour
    {
        // Reference to the NumberGrid gameobject
        GameObject numberGrid;

        // Timer(s) used for the memory version start delay where the tile are shown face-up for memorization.
        Timer startDelay;
        Timer memoryTimer;

        void Awake()
        {
            // Set up references
            numberGrid = GameObject.FindWithTag("NumberGrid");

            // Initialize and configure the timer(s) 
            startDelay = gameObject.AddComponent<Timer>();
            startDelay.Duration = 1;

            memoryTimer = gameObject.AddComponent<Timer>();
            memoryTimer.Duration = 3;

            // Set up the game board 
            GameObject.FindWithTag("GameBoard").GetComponent<TurnManager>().GameSetUp();

            //Refererence to game manager
            GameManager gameManager = GameObject.FindWithTag("PersistentObject").GetComponent<GameManager>();

            //Execute the start of the game differently based on it's variation.
            //If game type is "Original", start the game immediately.
            //If game type is "Memory", run the delay.
            if (gameManager.Type == GameVariation.Original)
            {
                GameObject.FindWithTag("GameBoard").GetComponent<TurnManager>().StartGame();
            }
            else if (gameManager.Type == GameVariation.Memory)
            {
                startDelay.Run();
            }
        }

        /// <summary>
        /// Called every frame. Checks the status of the memory timer.
        /// </summary>

        void Update()
        {
            if (startDelay.Finished)
            {
                startDelay.Stop();
                launchMemoryVersionDelay();
            }

            // If the memory timer has finished, stop the timer, flip the tiles face down again, and start the game.
            if (memoryTimer.Finished)
            {
                memoryTimer.Stop();
                GameObject.FindWithTag("NumberGrid").GetComponent<NumberGrid>().flipTilesFaceDown();
                GameObject.FindWithTag("GameBoard").GetComponent<TurnManager>().StartGame();
            }
        }

        /// <summary>
        /// Launches the start delay for the "Memory" version of the game.
        /// </summary>

        public void launchMemoryVersionDelay()
        {
            // Flip the tiles face up and start the delay timer.
            memoryTimer.Run();
            GameObject.FindWithTag("NumberGrid").GetComponent<NumberGrid>().flipTilesFaceUp();
        }

        /// <summary>
        /// Gets a valid tile from the NumberGrid to play.
        /// </summary>
        /// <returns>Returns the number tile object.</returns>

        public GameObject GetTileToPlay()
        {
            return numberGrid.GetComponent<NumberGrid>().GetTile();
        }
    }
}