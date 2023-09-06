using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// Handles button actions for the game start canvas.
/// </summary>

public class GameStartCanvas : MonoBehaviour
{
    /// <summary>
    /// Navigates to the display where you can choose the variation of the game that you want to play.
    /// </summary>

    public void play()
    {
        ScreenManager.GetInstance().InstantiateScreen(UIElementName.TypeOfGamePanel, transform);
    }

    /// <summary>
    /// Navigates to the settings menu.
    /// </summary>

    public void goToOptionsMenu()
    {
        ScreenManager.GetInstance().InstantiateScreen(UIElementName.Settings, transform);
    }

    /// <summary>
    /// Navigates to the instructions page.
    /// </summary>

    public void goTohowToPage()
    {
        ScreenManager.GetInstance().InstantiateScreen(UIElementName.InstructionsPage, transform);
    }

    /// <summary>
    /// Exits the application.
    /// </summary>

    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Opens the specified URL in the default web browser.
    /// </summary>
    /// <param name="url">The URL to be opened.</param>

    public void goToUrl(string url)
    {
        Application.OpenURL(url);
    }
}