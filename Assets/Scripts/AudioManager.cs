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

    void Awake()
    {
        manager = this;
        WarmPool();
    }
    void WarmPool()
    {
        pool = new AudioSource[poolSize];
        for (int i = 0; i < poolSize; i ++)
        {
            GameObject g = new GameObject("SFX." + i);
            AudioSource s = g.AddComponent<AudioSource>();
            s.playOnAwake = false;
            pool[i] = s;
            g.transform.SetParent(transform);
        }
    }

    public static void  Play (string name)
    {
        AudioClip clip = null;
        for (int i = 0; i < manager.clips.Length;i ++)
        {
            if (manager.clips[i].name == name)
            {
                clip = manager.clips[i];
                break;
            }
        }
        if (clip  == null) return;

        AudioSource s = manager.pool[manager.currentSource];
        s.clip = clip;
        s.Play();
        manager.currentSource = (manager.currentSource+1) % manager.poolSize;
    }
}
