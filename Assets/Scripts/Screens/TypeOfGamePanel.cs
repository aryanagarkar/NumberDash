using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeOfGamePanel : MonoBehaviour
{
    public void OriginalSelected()
    {
        Destroy(gameObject);
        ScreenManager.GetInstance().InstantiateScreen(CanvasName.LevelPanel, transform.parent);
    }

    public void MemorySelected()
    {
        Destroy(gameObject);
        transform.parent.GetComponent<GameStartCanvas>().MemoryPlayClicked();
    }
}
