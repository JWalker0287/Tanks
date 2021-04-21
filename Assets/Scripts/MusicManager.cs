using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager manager;
    public AudioClip music;
    public float crossFadeDuration = 1;
    AudioSource srcA;
    AudioSource srcB;

    void Awake()
    {
        if (manager == null)
        {
            manager = this;
            DontDestroyOnLoad(gameObject);
            srcA = gameObject.AddComponent<AudioSource>();
            srcA.playOnAwake = false;
            srcA.loop = true;
            srcB = gameObject.AddComponent<AudioSource>();
            srcB.playOnAwake = false;
            srcA.loop = true;
            srcA.clip = music;
            srcA.Play();
        }
        else
        {
            Play(music);
            Destroy(gameObject);
        }
    }

    public static void Play(AudioClip clip)
    {
        manager.StartCoroutine(manager.CrossFade(clip));
    }

    IEnumerator CrossFade(AudioClip clip)
    {
        srcB.volume = 0;
        srcB.clip = clip;
        srcB.Play();
        for (float t = 0; t <= crossFadeDuration; t += Time.unscaledDeltaTime)
        {
            float frac = t/crossFadeDuration;
            srcA.volume = 1-frac;
            srcB.volume = frac;
            yield return new WaitForEndOfFrame();
        }
        AudioSource temp = srcA;
        srcA = srcB;
        srcB = temp;
        srcB.Stop();
        srcA.volume = 1;
        srcA.loop = true;
        srcB.loop = true;
    }
}
