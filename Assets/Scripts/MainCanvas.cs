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
        blankSprite = Resources.Load<Sprite>("BlankTileSprite");
        numbers.Add(Resources.Load<Sprite>("Tile1"));
        numbers.Add(Resources.Load<Sprite>("Tile2"));
        numbers.Add(Resources.Load<Sprite>("Tile3"));
        numbers.Add(Resources.Load<Sprite>("Tile4"));
        numbers.Add(Resources.Load<Sprite>("Tile5"));
        numbers.Add(Resources.Load<Sprite>("Tile6"));   
        numbers.Add(Resources.Load<Sprite>("Tile7"));
        numbers.Add(Resources.Load<Sprite>("Tile8"));
        numbers.Add(Resources.Load<Sprite>("Tile9"));
        highlightedSprites = new Dictionary<string, Sprite>();
        highlightedSprites.Add("Tile1H", Resources.Load<Sprite>("Tile1H"));
        highlightedSprites.Add("Tile2H", Resources.Load<Sprite>("Tile2H"));
        highlightedSprites.Add("Tile3H", Resources.Load<Sprite>("Tile3H"));
        highlightedSprites.Add("Tile4H", Resources.Load<Sprite>("Tile4H"));
        highlightedSprites.Add("Tile5H", Resources.Load<Sprite>("Tile5H"));
        highlightedSprites.Add("Tile6H", Resources.Load<Sprite>("Tile6H"));
        highlightedSprites.Add("Tile7H", Resources.Load<Sprite>("Tile7H"));
        highlightedSprites.Add("Tile8H", Resources.Load<Sprite>("Tile8H"));
        highlightedSprites.Add("Tile9H", Resources.Load<Sprite>("Tile9H"));
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
