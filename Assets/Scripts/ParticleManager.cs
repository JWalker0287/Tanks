using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager manager;
    public ParticleSystem[] particles;

    ParticleSystem[][] pools;
    public int poolSize = 10;
    int[] currentParticles;

    void Awake () 
    {
        manager = this;
        WarmPool();
    }

    void WarmPool ()
    {
        currentParticles = new int[particles.Length];
        pools = new ParticleSystem[particles.Length][];
        for (int j = 0; j < particles.Length; j++)
        {
            ParticleSystem[] pool = new ParticleSystem[poolSize];
            pools[j] = pool;
            for (int i = 0; i < poolSize; i++)
            {
                ParticleSystem s = Instantiate<ParticleSystem>(particles[j]);
                pool[i] = s;
                s.transform.SetParent(transform);
            }
        }
    }

    public ParticleSystem Play(string name)
    {
        int index = -1;
        for (int i = 0; i < particles.Length; i++)
        {
            if (particles[i].name == name)
            {
                index = i;
                break;
            }
        }
        if (index == -1) return null;

        ParticleSystem[] pool = pools[index];
        int currentParticle = currentParticles[index];
        ParticleSystem p = pool[currentParticle];
        currentParticle = (currentParticle + 1) % poolSize;
        return p;
    }

    public static void Play(string name, Vector3 pos)
    {
        ParticleSystem p = manager.Play(name);
        if (p == null) return;

        p.transform.position = pos;
        p.Play();
    }
}