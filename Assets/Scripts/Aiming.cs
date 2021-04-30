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
    public float aimInterval = 3;
    float lastAimTime;
    public int sweepIterations = 36;
    
    CharacterMotor motor;
    GunController gun;
    TankController tank;
    void Awake ()
    {
        tank = GetComponent<TankController>();
        motor = GetComponent<CharacterMotor>();
        gun = GetComponentInChildren<GunController>();
        lastAimTime = Time.time - Random.Range(0,aimInterval);
    }

    void Update()
    {
        FireAtPlayer();
        switch(aimType)
        {
            case AimType.Player:
                UpdateAimAtPlayer();
                break;
            case AimType.Random:
                UpdateAimRandom();
                break;
            default:
                Debug.LogError("Shouldn't be here.");
                break;
        }
    }

    void UpdateAimAtPlayer()
    {
        if(Sweep(out Vector3 dir)) 
        {
            motor.lookDir = dir;
        }
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
        gun.shouldFire = CanHitPlayer(gun.transform.position, gun.transform.forward);
    }

    bool Sweep (out Vector3 dir)
    {   
        float dA = 360f / (float) sweepIterations;
        Quaternion rot = Quaternion.AngleAxis(Random.Range(0,dA), Vector3.up);
        dir = rot * gun.transform.forward;
        Vector3 off = rot * (gun.transform.position - transform.position);
        rot = Quaternion.AngleAxis(dA, Vector3.up);
        for (int i = 0; i < sweepIterations; i++)
        {
            if (CanHitPlayer(transform.position + off, dir)) return true;
            dir = rot * dir;
            off = rot * off;
            dir.y = 0;
        }
        return false;
    }

    bool CanHitPlayer (Vector3 pos, Vector3 dir)
    {
        //for (int bounces = gun.bulletPrefab.maxBounces; bounces >= 0; bounces--)
        //{
            if (Physics.SphereCast(pos, 0.2f, dir, out RaycastHit hit))
            {
                TankController tank = hit.collider.GetComponentInParent<TankController>();
                if (tank != null) 
                {
                    //Debug.Log(bounces + " - " + tank);
                    return true;
                }
                //pos = hit.point;
                //dir = Vector3.Reflect(dir, hit.normal);
            }
        //}
        return false;
    }
    //void OnDrawGizmos ()
    //{
    //    Vector3 dir = gun.transform.forward;
    //    Vector3 pos = gun.transform.position;
    //    if (Physics.SphereCast(pos,0.2f,dir,out RaycastHit hit))
    //    {
    //        Gizmos.DrawLine(pos,hit.point);
    //    }
    //}
}
