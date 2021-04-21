using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public Rigidbody prefab;
    List<Rigidbody> pool = new List<Rigidbody>();
    public int poolSize = 100;

    void Awake ()
    {
        FillPool();
    }

    void FillPool()
    {
        for(int i = 0; i < poolSize; i ++)
        {
            Rigidbody bullet = Instantiate<Rigidbody>(prefab);
            pool.Add(bullet);
            bullet.gameObject.SetActive(false);
            bullet.transform.SetParent(transform);
        }
    }

    public void Fire (Vector3 pos, Vector3 velocity)
    {
        Rigidbody projectile = pool.Find((r) => !r.gameObject.activeSelf);
        
        if (projectile == null) 
        {
            projectile = Instantiate<Rigidbody>(prefab);
            pool.Add(projectile);
            projectile.transform.SetParent(transform);
        }

        projectile.transform.position = pos;
        projectile.gameObject.SetActive(true);
        projectile.velocity = velocity;
    }
}
