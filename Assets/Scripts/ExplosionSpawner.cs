using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSpawner : MonoBehaviour
{
    public static ExplosionSpawner spawner;
    public ExplosionController prefab;
    ExplosionController[] pool;
    public int poolSize = 10;
    void Awake()
    {
        if (spawner == null) spawner = this;
        FillPool();
    }
    void FillPool ()
    {
        pool = new ExplosionController[poolSize];
        for (int i = 0; i < poolSize; i ++)
        {
            ExplosionController explosion = Instantiate<ExplosionController>(prefab);
            pool[i] = explosion;
            explosion.gameObject.SetActive(false);
            explosion.transform.SetParent(transform);
        }
    }
    ExplosionController SpawnExplosion ()
    {
        for (int i = 0;i < poolSize;i ++)
        {
            ExplosionController explosion = pool[i];
            if (!explosion.gameObject.activeSelf)
            {
                return explosion;
            }
        }
        return null;
    }
    public static void SpawnExplosion (Vector3 pos)
    {
        ExplosionController explosion = spawner.SpawnExplosion();
        if (explosion == null) return;

        explosion.transform.position = pos;
        explosion.gameObject.SetActive(true);
    }
}