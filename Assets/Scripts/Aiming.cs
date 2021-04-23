using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    public enum AimType {
        Random,
        Player
    }
    public AimType aimType = AimType.Player;
    public float lastAimTime;
    float aimInterval;
    CharacterMotor motor;
    GunController gun;
    void Awake ()
    {
        motor = GetComponent<CharacterMotor>();
        gun = GetComponentInChildren<GunController>();
        lastAimTime = Time.time - Random.Range(0,aimInterval);
    }

    void Update()
    {
        switch(aimType)
        {
            case AimType.Player:
                UpdatePlayer();
                break;
            case AimType.Random:
                break;
            default:
                Debug.LogError("Shouldn't be here.");
                break;
        }
        FireAtPlayer();
    }

    void UpdatePlayer()
    {
        Vector3 diff = PlayerController.player.transform.position - transform.position;
                motor.lookDir = diff.normalized;
    }

    void UpdateAimRandom()
    {
        if (Time.time - lastAimTime > aimInterval)
        {
            lastAimTime = Time.time;
            Vector3 dir = Random.onUnitSphere;
            dir.y = 0;
            motor.lookDir = dir.normalized;
        }
    }
    void FireAtPlayer ()
    {
        RaycastHit hit;
        if (Physics.SphereCast(gun.transform.position, 0.2f, gun.transform.forward, out hit))
        {
            Debug.Log(hit.collider);
            TankController tank = hit.collider.GetComponentInParent<TankController>();
            gun.shouldFire = (tank != null);
        }
    }

    Vector3 Sweep ()
    {
        Vector3 dir = Vector3.right;
        Vector3 off = gun.transform.position - transform.position;
        return Vector3.zero;
    }
}
