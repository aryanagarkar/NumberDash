using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static Dictionary<AudioClipName, AudioClip> clips;
    static AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
        clips = new Dictionary<AudioClipName, AudioClip>();
        clips.Add(AudioClipName.PlayerLost, Resources.Load<AudioClip>("Audio/Lose"));
        clips.Add(AudioClipName.PlayerWon, Resources.Load<AudioClip>("Audio/Win"));
        clips.Add(AudioClipName.Tied, Resources.Load<AudioClip>("Audio/Tied"));
        clips.Add(AudioClipName.TilePlaced, Resources.Load<AudioClip>("Audio/TilePlaced"));
        clips.Add(AudioClipName.ButtonClick, Resources.Load<AudioClip>("Audio/ButtonClick"));
    }

    public static void PlayClipByName(AudioClipName name){
        source.PlayOneShot(clips[name]);
    }
}
