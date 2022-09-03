using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance = null;
    static Dictionary<AudioClipName, AudioClip> clips;
    static AudioSource source;

    public static SoundManager GetInstance() {
        if (instance == null) {
            instance = new SoundManager();
        }
        return instance;
    } 

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
        clips.Add(AudioClipName.Swoosh, Resources.Load<AudioClip>("Audio/Swoosh"));
        clips.Add(AudioClipName.Alarm, Resources.Load<AudioClip>("Audio/Alarm"));
    }

    /*public static void PlayClipByName(AudioClipName name, System.Action<bool> callback){
        source.PlayOneShot(clips[name]);
        Debug.Log(source.isPlaying);
        while(!source.isPlaying) {
            Debug.Log("Source is not playing anymore");
            callback(true);
        }
    }*/

    public static void PlayClipByName(AudioClipName name){
        source.PlayOneShot(clips[name]);
    }

    public static void Stop() {
        if (source.isPlaying) {
            source.Stop();
        }
    }

    public static void PlayAudioXTimes(AudioClipName name, int timesToLoop){
        for(int i = 0; i < timesToLoop; i++){
            source.PlayOneShot(clips[name]);
        }
    }

    public static bool isSoundOver() {
        return !source.isPlaying;
    }
}
