using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the game selection panel that lets players choose between the "Original" and "Memory" game variations.
/// </summary>

public class TypeOfGamePanel : MonoBehaviour
{
    /// <summary>
    /// Handles the action when the "Original" game mode is selected.
    /// Calls GameManager to start the original game mode.
    /// Destroys this display.
    /// </summary>

    public void OriginalSelected()
    {
        Destroy(gameObject);
        GameObject.FindWithTag("PersistentObject").GetComponent<GameManager>().OriginalPlayClicked();
    }

    /// <summary>
    /// Handles the action when the "Memory" game mode is selected.
    /// Calls GameManager to start the memory game mode.
    /// Destroys this display.
    /// </summary>

    public void MemorySelected()
    {
        Destroy(gameObject);
        GameObject.FindWithTag("PersistentObject").GetComponent<GameManager>().MemoryPlayClicked();
    }
}