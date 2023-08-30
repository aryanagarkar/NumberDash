using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetLoader : MonoBehaviour
{
    private static AssetLoader instance = null;

    //Sprites
    Sprite defaultPlayer;
    Dictionary<string, Sprite> characters;
    Sprite blankSprite;
    List<Sprite> numbers = new List<Sprite>();
    Dictionary<string, Sprite> highlightedSprites;
    GameObject container;

    public static AssetLoader GetInstance()
    {
        return instance;
    }

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

    public Sprite BlankSprite{
        get{
            return blankSprite;
        }
    }

    public Sprite GetCharacterSpriteByName(string name)
    {
        return characters[name];
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        characters = new Dictionary<string, Sprite>();
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

        blankSprite = Resources.Load<Sprite>("Sprites/BlankTileSprite");

        populateNumberSprites();

        highlightedSprites = new Dictionary<string, Sprite>();
        highlightedSprites.Add("Tile1H", Resources.Load<Sprite>("Sprites/Tile1H"));
        highlightedSprites.Add("Tile2H", Resources.Load<Sprite>("Sprites/Tile2H"));
        highlightedSprites.Add("Tile3H", Resources.Load<Sprite>("Sprites/Tile3H"));
        highlightedSprites.Add("Tile4H", Resources.Load<Sprite>("Sprites/Tile4H"));
        highlightedSprites.Add("Tile5H", Resources.Load<Sprite>("Sprites/Tile5H"));
        highlightedSprites.Add("Tile6H", Resources.Load<Sprite>("Sprites/Tile6H"));
        highlightedSprites.Add("Tile7H", Resources.Load<Sprite>("Sprites/Tile7H"));
        highlightedSprites.Add("Tile8H", Resources.Load<Sprite>("Sprites/Tile8H"));
        highlightedSprites.Add("Tile9H", Resources.Load<Sprite>("Sprites/Tile9H"));

        container = Resources.Load<GameObject>("Prefabs/Grids/Container");
    }

    private void populateNumberSprites(){
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

    public GameObject Container
    {
        get {
            return container;
        }
    }

    public Sprite GetHighlightedSpriteByName(string name)
    {
        return highlightedSprites[name];
    }


    public Sprite PickRandomSprite()
    {
        if(numbers.Count == 0){
            populateNumberSprites();
        }
        int index = Random.Range(0, numbers.Count);
        Sprite s = numbers[index];
        numbers.Remove(s);
        return s;
    }
}
