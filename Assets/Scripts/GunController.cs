using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public BulletController bulletPrefab;
    public int maxShots = 6;
    public string fireSound = "launch";
    public float fireDelay = 0.1f;
    public float bulletSpeed = 40;
    float lastFire;

    public bool shouldFire = false;

    BulletController[] bullets;
    void Start()
    {
        bullets = new BulletController[maxShots];
        for (int i = 0; i < maxShots; i++)
        {
            bullets[i] = Instantiate<BulletController>(bulletPrefab);
            bullets[i].gameObject.SetActive(false);
        }
    }

    BulletController GetBullet()
    {
        for (int i = 0; i < maxShots; i++)
        {
            if (!bullets[i].gameObject.activeSelf) return bullets[i];
        }
        return null;
    }

    void Update()
    {
        if (shouldFire && Time.time - lastFire > fireDelay)
        {
            BulletController bullet = GetBullet();
            if (bullet == null) return;
            Rigidbody body = bullet.GetComponent<Rigidbody>();
            bullet.transform.position = transform.position;
            bullet.gameObject.SetActive(true);
            body.velocity = transform.forward * bulletSpeed;
            //AudioManager.PlayVaried(fireSound);
            lastFire = Time.time;
            
        }
    }
}
