using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a timer.
/// </summary>

public class Timer : MonoBehaviour
{
    // Total duration of the timer in seconds
    float totalSeconds = 0;

    // Elapsed time since the timer started
    float elapsedSeconds = 0;

    // Indicates whether the timer is currently running
    bool running = false;

    // Indicates whether the timer has started for the current timer
    bool started = false;

   /// <summary>
   /// Timer Duration property.
   /// When the timer is not running, setting the total duration of the timer in seconds is allowed.
   /// </summary>

    public float Duration
    {
        set
        {
            if (!running)
            {
                totalSeconds = value;
            }
        }
    }

    /// <summary>
    /// Timer Finished property.
    /// Gets a true/false value of whether the timer has finished.
    /// </summary>

    public bool Finished
    {
        get { return started && !running; }
    }

    /// <summary>
    /// Timer Running property.
    /// Gets a true/false value of whether the timer is currently running.
    /// </summary>

    public bool Running
    {
        get { return running; }
    }

    /// <summary>
    /// Updates the timer's elapsed time every frame.
    /// If it has reached its total duration, set running to false.
    /// </summary>

    void Update()
    {
        if (running)
        {
            elapsedSeconds += Time.deltaTime;
            if (elapsedSeconds >= totalSeconds)
            {
                running = false;
            }
        }
    }

    /// <summary>
    /// Starts the timer if its total duration is greater than zero.
    /// </summary>

    public void Run()
    {
        if (totalSeconds > 0)
        {
            started = true;
            running = true;
            elapsedSeconds = 0;
        }
    }

    /// <summary>
    /// Stops the timer and resets its state.
    /// </summary>

    public void Stop()
    {
        running = false;
        started = false;
    }
}