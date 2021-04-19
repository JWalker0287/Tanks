using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(CharacterMotor))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController player;
    public string xAxisLeft = "Horizontal";
    public string yAxisLeft = "Vertical";
    public string xAxisRight = "Horizontal";
    public string yAxisRight = "Vertical";
    public Transform gun;
    CharacterMotor motor;
    public ProjectileSpawner bullets;
    public float fireDelay = 0.5f;
    public float bulletSpeed = 40;
    float lastFire;
    public ParticleSystem deathExplosion;
    public GameObject playerVisuals;

    void Awake()
    {
        player = this;
        motor = GetComponent<CharacterMotor>();
        //GetComponent<Animator>().Play("PlayerStart");
    }
    void Update()
    {
        float x = Input.GetAxisRaw(xAxisLeft);
        float z = Input.GetAxisRaw(yAxisLeft);
        motor.dir = new Vector3(x,0,z).normalized;
        if (xAxisLeft != xAxisRight) UpdateRightStick();
    }

    void UpdateRightStick()
    {
        float x = Input.GetAxisRaw(xAxisRight);
        float z = Input.GetAxisRaw(yAxisRight);
        motor.lookDir = new Vector3(x,0,z);

        float dot = Vector3.Dot(motor.lookDir, transform.forward);

        if (dot > 0.9f && Time.time - lastFire > fireDelay && motor.lookDir.sqrMagnitude> 0.25f)
        { 
            bullets.Fire(gun.position, gun.forward * bulletSpeed);
            lastFire = Time.time;
        }
    }

    public void Death()
    {
        motor.enabled = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<SphereCollider>().enabled = false;
        deathExplosion.Play();
        //GameManager.Death();
    }
}
