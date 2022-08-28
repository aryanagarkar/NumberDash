using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPanel : MonoBehaviour
{
    public void EasyLevelSelected(){
        Destroy(gameObject);
        transform.parent.GetComponent<GameStartCanvas>().EasyButtonClicked();
    }

    public void MediumLevelSelected(){
        Destroy(gameObject);
        transform.parent.GetComponent<GameStartCanvas>().MediumButtonClicked();
    }
}
