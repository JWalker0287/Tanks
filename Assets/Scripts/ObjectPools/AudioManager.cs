using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager manager;
    public AudioClip[] clips;

    AudioSource[] pool;
    public int poolSize = 10;
    int currentSource = 0;

    Dictionary<string, float> lastPlayed = new Dictionary<string, float>();

    void Awake () 
    {
        manager = this;
        WarmPool();

        for (int i = 0; i < clips.Length; i++)
        {
            string clipName = clips[i].name;
            lastPlayed[clipName] = -1;
        }
    }

    void WarmPool ()
    {
        pool = new AudioSource[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            GameObject g = new GameObject("SFX."+i);
            AudioSource s = g.AddComponent<AudioSource>();
            s.playOnAwake = false;
            pool[i] = s;
            g.transform.SetParent(transform);
        }
    }

    public static void Play (string name)
    {
        Play(name, 1, 1);
    }

    public static void PlayVaried (string name)
    {
        Play(name, Random.Range(0.9f, 1.1f), Random.Range(0.9f, 1.1f));
    }

    public static void Play (string name, float pitch, float volume)
    {
        if (!manager.lastPlayed.ContainsKey(name)) return;

        float lastPlayTime = manager.lastPlayed[name];
        if (Time.time - lastPlayTime < 0.05) return;

        AudioClip clip = null;
        for (int i = 0; i < manager.clips.Length; i++)
        {
            if (manager.clips[i].name == name)
            {
                clip = manager.clips[i];
                break;
            }
        }
        if (clip == null) return;

        AudioSource s = manager.pool[manager.currentSource];
        s.clip = clip;
        s.volume = volume;
        s.pitch = pitch;
        s.Play();
        manager.lastPlayed[name] = Time.time;
        manager.currentSource = (manager.currentSource+1) % manager.poolSize;
    }
}
