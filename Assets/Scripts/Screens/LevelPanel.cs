using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPanel : MonoBehaviour
{
    public void EasyLevelSelected(){
        transform.parent.GetComponent<GameStartCanvas>().EasyButtonClicked();
    }

    public void MediumLevelSelected(){
        transform.parent.GetComponent<GameStartCanvas>().MediumButtonClicked();
    }
}
