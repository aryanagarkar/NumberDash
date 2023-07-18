using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPanel : MonoBehaviour
{
    public void EasyLevelSelected(){
        Destroy(gameObject);
        Debug.Log(GameObject.FindWithTag("PersistentObject").GetComponent<GameManager>());
        GameObject.FindWithTag("PersistentObject").GetComponent<GameManager>().EasyButtonClicked();
    }

    public void MediumLevelSelected(){
        Destroy(gameObject);
        GameObject.FindWithTag("PersistentObject").GetComponent<GameManager>().MediumButtonClicked();
    }
}
