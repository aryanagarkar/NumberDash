using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberGrid : MonoBehaviour
{
    public GameObject GetTile() {
        return transform.GetChild(0).gameObject;
    }
}
