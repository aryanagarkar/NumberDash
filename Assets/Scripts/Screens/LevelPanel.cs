using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPanel : MonoBehaviour
{
    public void EasyLevelSelected(){
        Destroy(gameObject);
        GameObject.FindWithTag("PersistentObject").GetComponent<GameManager>().LevelButtonClicked(Level.Easy);
    }

    public void MediumLevelSelected(){
        Destroy(gameObject);
        GameObject.FindWithTag("PersistentObject").GetComponent<GameManager>().LevelButtonClicked(Level.Medium);
    }
}
