using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    /// <summary>
    /// Responsible for loading and providing access to game assets.
    /// Uses the Singleton pattern to ensure only one instance of AssetLoader exists.
    /// </summary>

    public class AssetLoader : MonoBehaviour
    {
        private static AssetLoader instance = null;  // Singleton instance.

        // Asset variables.
        Sprite defaultPlayer;
        Dictionary<string, Sprite> characters;
        Sprite blankSprite;
        List<Sprite> numbers = new List<Sprite>();
        Dictionary<string, Sprite> highlightedSprites;
        GameObject container;

        /// <summary>
        /// Returns the single instance of AssetLoader.
        /// </summary>

        public static AssetLoader GetInstance()
        {
            return instance;
        }

        /// <summary>
        /// Properties to access assets.
        /// </summary>

        public Sprite DefaultPlayer
        {
            get
            {
                return defaultPlayer;
            }
        }

        public Dictionary<string, Sprite> Characters
        {
            get
            {
                return characters;
            }
        }

        public Sprite BlankSprite
        {
            get
            {
                return blankSprite;
            }
        }

        public GameObject Container
        {
            get
            {
                return container;
            }
        }

        void Awake()
        {
            // Singleton setup.
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject); // Keep active for entire game lifetime.
            }
            else if (instance != this)
            {
                Destroy(this.gameObject); // Destroy extra instances.
                return;
            }

            characters = new Dictionary<string, Sprite>();
            highlightedSprites = new Dictionary<string, Sprite>();

            // Load character sprites.
            LoadCharacterSprites();

            // Load blank sprite.
            blankSprite = Resources.Load<Sprite>("Sprites/BlankTileSprite");

            // Load number tile sprites.
            LoadNumberTileSprites();

            // Load highlighted tile sprites
            LoadHighlightedTileSprites();

            // Load container (placeholder for tile) prefab.
            // Used in implementation of memory game variation.
            container = Resources.Load<GameObject>("Prefabs/Grids/Container");
        }

        /// <summary>
        /// Loads character sprites.
        /// </summary>

        private void LoadCharacterSprites()
        {
            characters.Add("PB", Resources.Load<Sprite>("Sprites/PB"));
            defaultPlayer = characters["PB"];
            characters.Add("PG", Resources.Load<Sprite>("Sprites/PG"));
            characters.Add("PBSelected", Resources.Load<Sprite>("Sprites/PBSelected"));
            characters.Add("PGSelected", Resources.Load<Sprite>("Sprites/PGSelected"));
            characters.Add("PBCircle", Resources.Load<Sprite>("Sprites/PBCircle"));
            characters.Add("PGCircle", Resources.Load<Sprite>("Sprites/PGCircle"));
            characters.Add("OG", Resources.Load<Sprite>("Sprites/OG"));
            characters.Add("OB", Resources.Load<Sprite>("Sprites/OB"));
            characters.Add("OM", Resources.Load<Sprite>("Sprites/OM"));
            characters.Add("OD", Resources.Load<Sprite>("Sprites/OD"));
        }

        /// <summary>
        /// Loads number tile sprites.
        /// </summary>

        private void LoadNumberTileSprites()
        {
            numbers.Add(Resources.Load<Sprite>("Sprites/Tile1"));
            numbers.Add(Resources.Load<Sprite>("Sprites/Tile2"));
            numbers.Add(Resources.Load<Sprite>("Sprites/Tile3"));
            numbers.Add(Resources.Load<Sprite>("Sprites/Tile4"));
            numbers.Add(Resources.Load<Sprite>("Sprites/Tile5"));
            numbers.Add(Resources.Load<Sprite>("Sprites/Tile6"));
            numbers.Add(Resources.Load<Sprite>("Sprites/Tile7"));
            numbers.Add(Resources.Load<Sprite>("Sprites/Tile8"));
            numbers.Add(Resources.Load<Sprite>("Sprites/Tile9"));
        }

        /// <summary>
        /// Loads highlighted tile sprites.
        /// </summary>

        private void LoadHighlightedTileSprites()
        {
            highlightedSprites.Add("Tile1H", Resources.Load<Sprite>("Sprites/Tile1H"));
            highlightedSprites.Add("Tile2H", Resources.Load<Sprite>("Sprites/Tile2H"));
            highlightedSprites.Add("Tile3H", Resources.Load<Sprite>("Sprites/Tile3H"));
            highlightedSprites.Add("Tile4H", Resources.Load<Sprite>("Sprites/Tile4H"));
            highlightedSprites.Add("Tile5H", Resources.Load<Sprite>("Sprites/Tile5H"));
            highlightedSprites.Add("Tile6H", Resources.Load<Sprite>("Sprites/Tile6H"));
            highlightedSprites.Add("Tile7H", Resources.Load<Sprite>("Sprites/Tile7H"));
            highlightedSprites.Add("Tile8H", Resources.Load<Sprite>("Sprites/Tile8H"));
            highlightedSprites.Add("Tile9H", Resources.Load<Sprite>("Sprites/Tile9H"));
        }

        /// <summary>
        /// Retrieves a character sprite based on its name.
        /// <param name="name">The name of the sprite to load.</param>
        /// <returns>Character sprite</returns>
        /// </summary>

        public Sprite GetCharacterSpriteByName(string name)
        {
            return characters[name];
        }


        /// <summary>
        /// Retrieves a highlighted sprite based on its name.
        /// <param name="name">The name of the highlighted sprite to load.</param>
        /// <returns>Highlighted tile sprite</returns>
        /// </summary>

        public Sprite GetHighlightedSpriteByName(string name)
        {
            return highlightedSprites[name];
        }


        /// <summary>
        /// Retrieves a random number tile sprite and removes it from available choices.
        /// <returns>random number tile sprite</returns>
        /// </summary>

        public Sprite PickRandomSprite()
        {
            //Load all number tile sprites again if there are no available tiles.
            //Implemented here to deal with errors when not finding the appropriate sprite.
            if (numbers.Count == 0)
            {
                LoadNumberTileSprites();
            }

            int index = Random.Range(0, numbers.Count);
            Sprite s = numbers[index];
            numbers.Remove(s);
            return s;
        }
    }
}