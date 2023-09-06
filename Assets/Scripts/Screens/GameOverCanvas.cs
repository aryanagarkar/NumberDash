using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Manages game over functions.
/// </summary>

public class GameOverCanvas : MonoBehaviour
{
    TextMeshProUGUI textComponent; // Reference to the game over text component displayed in the game over canvas

    /// <summary>
    /// Initializes the GameOverCanvas and retrieves the text component.
    /// </summary>

    void Awake()
    {
        textComponent = transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>(); 
    }

    /// <summary>
    /// Updates the game over text and plays the specified sound.
    /// </summary>
    /// <param name="text">The text to display on the game over canvas.</param>
    /// <param name="name">The name of the audio clip to play.</param>

    public void updateTextandPlaySound(string text, AudioClipName name)
    {
        textComponent.text = text; 
        SoundManager.PlayClipByName(name); 
    }

    /// <summary>
    /// Handles the action when the 'yes' button is clicked to play again.
    /// Main game board reloads and start a new game.
    /// Stops any playing sound.
    /// </summary>

    public void playAgain()
    {
        SoundManager.StopClip();

        Camera.main.GetComponent<MainGame>().playAgain();
    }

    /// <summary>
    /// Handles the action when the 'no' button is clicked. 
    /// Stops any playing sound 
    /// Calls MainGame to display the "are you sure?" screen and score message.
    /// Destroys this display.
    /// </summary>

    public void noButtonClicked()
    {
        SoundManager.StopClip(); 
        Camera.main.GetComponent<MainGame>().DisplayScoreMessage();
        Destroy(gameObject);
    }
}