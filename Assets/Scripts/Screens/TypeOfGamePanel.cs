using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeOfGamePanel : MonoBehaviour
{
    public void OriginalSelected()
    {
        Destroy(gameObject);
        GameObject.FindWithTag("PersistentObject").GetComponent<GameManager>().OriginalPlayClicked();
    }

    public void MemorySelected()
    {
        Destroy(gameObject);
        GameObject.FindWithTag("PersistentObject").GetComponent<GameManager>().MemoryPlayClicked();
    }
}
