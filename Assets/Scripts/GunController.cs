using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    //public string fireSound = "launch";
    public string bulletName = "bullet";
    [Range(1,100)] public int bulletsPerShot = 3;
    [Range(0,1)] public float accuracy = 1;
    [Range(0,180)] public float spray = 15;
    public float fireDelay = 0.1f;
    public float bulletSpeed = 40;
    float lastFire;

    public bool shouldFire = false;

    void Update()
    {
        if (shouldFire && Time.time - lastFire > fireDelay)
        {
            float dA = spray * 2f / (float)bulletsPerShot;
            for (int i = 0; i < bulletsPerShot; i ++)
            {
                float a = -spray + dA * i;
                a = Mathf.Lerp(Random.Range(-100,100),a,accuracy);
                Vector3 forward = Quaternion.AngleAxis(a,Vector3.up) * transform.forward;
                Rigidbody projectile = Spawner.Spawn(bulletName).GetComponent<Rigidbody>();
                projectile.transform.position = transform.position;
                projectile.gameObject.SetActive(true);
                projectile.velocity = forward.normalized * bulletSpeed;
                //AudioManager.PlayVaried(fireSound);
            }
            lastFire = Time.time;
        }
    }
}
