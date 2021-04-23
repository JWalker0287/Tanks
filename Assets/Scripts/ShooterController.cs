using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    public static ProjectileSpawner bullets;
    public string spawnAnimName = "ShooterSpawn";
    public float shotDist = 28;
    public float chaseDist = 10;
    float chaseDistSq;
    CharacterMotor motor;
    public GunController gun;
    Animator anim;

    void Start()
    {
        chaseDistSq = chaseDist * chaseDist;
        motor = GetComponent<CharacterMotor>();
    }

    void OnEnable()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Animator>().Play(spawnAnimName);
    }

    void Update()
    {
        Vector3 diff = PlayerController.player.transform.position - transform.position;
        float distSq = diff.sqrMagnitude;
        if (distSq > chaseDistSq)
        {
            motor.moveDir = diff.normalized;
        }
        else
        {
            motor.moveDir *= 0.9f;
        }
        gun.shouldFire = distSq < shotDist * shotDist;
        motor.lookDir = diff;
    }

}
