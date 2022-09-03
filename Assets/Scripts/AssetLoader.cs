using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetLoader : MonoBehaviour
{
     private static AssetLoader instance = null;

    //Sprites
    Sprite defaultPlayer;
    Dictionary<string, Sprite> characters;

    public static AssetLoader GetInstance() {
        return instance;
    }

    public Sprite DefaultPlayer{
        get{
            return defaultPlayer;
        }
    }

    public Sprite GetSpriteByName(string name) {
        return characters[name];
    }

    void Awake()
    {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else if(instance != this){
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
    }


    public Dictionary<string, Sprite> Characters{
        get{
            return characters;
        }
    }
}
