using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetLoader : MonoBehaviour
{
     private static AssetLoader instance = null;

    //Sprites
    Dictionary<string, Sprite> characters;

    public static AssetLoader GetInstance() {
        if (instance == null) {
            instance = new AssetLoader();
        }
        return instance;
    }

    public Sprite GetSpriteByName(string name) {
        return characters[name];
    }

    void Awake()
    {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else if(instance != this){
            Destroy(this.gameObject);
            return;
        }

        characters = new Dictionary<string, Sprite>();
        characters.Add("Mummy", Resources.Load<Sprite>("Sprites/Mummy"));
        characters.Add("Alien", Resources.Load<Sprite>("Sprites/Alien"));
        characters.Add("Werewolf", Resources.Load<Sprite>("Sprites/Werewolf"));
        characters.Add("Cyclops", Resources.Load<Sprite>("Sprites/Cyclops"));
        characters.Add("Fish", Resources.Load<Sprite>("Sprites/Fish"));
        characters.Add("Robot", Resources.Load<Sprite>("Sprites/Robot"));
    }


    public Dictionary<string, Sprite> Characters{
        get{
            return characters;
        }
    }
}
