using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelperEnums;

namespace Sound
{
    /// <summary>
    /// Manages sound-related functions, including playing and stopping audio clips.
    /// Uses the Singleton pattern to ensure only one instance of SoundManager exists.
    /// </summary>

    public class SoundManager : MonoBehaviour
    {
        private static SoundManager instance = null; // Singleton instance.
        static Dictionary<AudioClipName, AudioClip> clips; // Stores all audio clips by name.
        static AudioSource source; // The source component for playing audio.

        /// <summary>
        /// Retrieves the singleton instance of SoundManager. 
        /// If it doesn't exist, a new instance is created.
        /// <returns>Current soundManager instance</returns>
        /// </summary>

        public static SoundManager GetInstance()
        {
            if (instance == null)
            {
                instance = new SoundManager();
            }
            return instance;
        }

        /// <summary>
        /// Initializes the SoundManager on game start and ensures only one instance exists.
        /// Loads audio source and all audio clips.
        /// </summary>

        void Awake()
        {
            // Singleton setup.
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject); // Keep active for entire game lifetime.
            }
            else if (instance != this)
            {
                Destroy(this.gameObject); // Destroy extra instances.
                return;
            }

            // Audio setup.
            source = GetComponent<AudioSource>();
            clips = new Dictionary<AudioClipName, AudioClip>();

            // Load all game audio clips.
            clips.Add(AudioClipName.PlayerLost, Resources.Load<AudioClip>("Audio/Lose"));
            clips.Add(AudioClipName.PlayerWon, Resources.Load<AudioClip>("Audio/Win"));
            clips.Add(AudioClipName.Tied, Resources.Load<AudioClip>("Audio/Tied"));
            clips.Add(AudioClipName.TilePlaced, Resources.Load<AudioClip>("Audio/TilePlaced"));
            clips.Add(AudioClipName.ButtonClick, Resources.Load<AudioClip>("Audio/ButtonClick"));
            clips.Add(AudioClipName.Swoosh, Resources.Load<AudioClip>("Audio/Swoosh"));
            clips.Add(AudioClipName.Alarm, Resources.Load<AudioClip>("Audio/Alarm"));
        }

        /// <summary>
        /// Plays the specified audio clip once.
        /// </summary>
        /// <param name="name">The name of the audio clip to play.</param>

        public static void PlayClipByName(AudioClipName name)
        {
            source.PlayOneShot(clips[name]);
        }

        /// <summary>
        /// Stops any currently playing audio clip.
        /// </summary>

        public static void StopClip()
        {
            if (source.isPlaying)
            {
                source.Stop();
            }
        }

        /// <summary>
        /// Plays the specified audio clip for a given number of times.
        /// </summary>
        /// <param name="name">The name of the audio clip to play.</param>
        /// <param name="timesToLoop">The number of times to loop the audio clip.</param>

        public static void PlayAudioXTimes(AudioClipName name, int timesToLoop)
        {
            for (int i = 0; i < timesToLoop; i++)
            {
                source.PlayOneShot(clips[name]);
            }
        }

        /// <summary>
        /// Checks if any sound is currently being played.
        /// </summary>
        /// <returns>True if no sound is playing, false otherwise.</returns>

        public static bool isSoundOver()
        {
            return !source.isPlaying;
        }
    }
}