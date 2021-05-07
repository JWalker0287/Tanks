using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner spawner;
    public GameObject[] prefabs;
    List<GameObject>[] pools;
    public int poolSize = 100;

    void Awake()
    {
        if (spawner == null) spawner = this;
        FillPools();
    }
    void FillPools()
    {
        pools = new List<GameObject>[prefabs.Length];
        for (int i = 0; i < prefabs.Length; i ++)
        {
            List<GameObject> pool = new List<GameObject>();
            pools[i] = pool;
            FillPool(pool, prefabs[i]);
        }
    }
    void FillPool(List<GameObject> pool, GameObject prefab)
    {
        for (int i = 0; i < poolSize; i ++)
        {
            GameObject g = Instantiate<GameObject>(prefab);
            pool.Add(g);
            g.gameObject.SetActive(false);
            g.transform.SetParent(transform);
        }
    }
    public static void DisableAll()
    {
        for (int i = 0; i < spawner.prefabs.Length; i ++)
        {
            for (int j = 0; j < spawner.pools[i].Count; j ++)
            {
                GameObject g = spawner.pools[i][j];
                g.SetActive(false);
            }
        }
    }

    public static GameObject Spawn(string name)
    {
        int index = -1;
        for (int i = 0; i < spawner.prefabs.Length; i++)
        {
            if (spawner.prefabs[i].name == name)
            {
                index = i;
                break;
            }
        }
        if (index == -1) return null;

        List<GameObject> pool = spawner.pools[index];
        GameObject g = pool.Find(g => !g.activeSelf);
        if (g == null)
        {
            g = Instantiate(spawner.prefabs[index]);
            pool.Add(g);
            g.SetActive(false);
            g.transform.SetParent(spawner.transform);
        }
        return g;
    }
    
    public static GameObject Spawn(string name, Vector3 pos)
    {
        GameObject g = Spawn(name);
        if (g == null) return null;
        g.transform.position = pos;
        g.SetActive(true);
        return g;
    }

    public static GameObject Spawn(string name, Vector3 pos, Quaternion rot)
    {
        GameObject g = Spawn(name);
        if (g == null) return null;
        g.transform.position = pos;
        g.transform.rotation = rot;
        g.SetActive(true);
        return g;
    }
}