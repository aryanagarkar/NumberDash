using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance = null;
    static Dictionary<AudioClipName, AudioClip> clips;
    static AudioSource source;

    void Awake()
    {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else if(instance != this){
            Destroy(this.gameObject);
            return;
        }
        source = GetComponent<AudioSource>();
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
