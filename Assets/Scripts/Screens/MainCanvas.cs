using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    List<Sprite> numbers = new List<Sprite>();
    List<Sprite> usedNumbers = new List<Sprite>();
    Dictionary<string, Sprite> highlightedSprites;
    GameObject numberGrid;
    Sprite blankSprite;

    void Start()
    { 
        numberGrid = GameObject.FindWithTag("NumberGrid");
        blankSprite = Resources.Load<Sprite>("Sprites/BlankTileSprite");
        numbers.Add(Resources.Load<Sprite>("Sprites/Tile1"));
        numbers.Add(Resources.Load<Sprite>("Sprites/Tile2"));
        numbers.Add(Resources.Load<Sprite>("Sprites/Tile3"));
        numbers.Add(Resources.Load<Sprite>("Sprites/Tile4"));
        numbers.Add(Resources.Load<Sprite>("Sprites/Tile5"));
        numbers.Add(Resources.Load<Sprite>("Sprites/Tile6"));   
        numbers.Add(Resources.Load<Sprite>("Sprites/Tile7"));
        numbers.Add(Resources.Load<Sprite>("Sprites/Tile8"));
        numbers.Add(Resources.Load<Sprite>("Sprites/Tile9"));
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
    }

    public GameObject GetTileToPlay() {
        return numberGrid.GetComponent<NumberGrid>().GetTile();
    }

    public Sprite PickRandomSprite(){
        int index = Random.Range(0, numbers.Count);
        Sprite s = numbers[index];
        numbers.Remove(s);  
        return s;
    }

    public Sprite GetHighlightedSpriteByName(string name) {
        return highlightedSprites[name];
    }

     public Sprite ResetSprite(Sprite sprite){
        numbers.Add(sprite);  
        return blankSprite;
    }
}
