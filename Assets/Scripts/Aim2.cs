using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim2 : MonoBehaviour
{
    CharacterMotor motor;
    GunController gun;

    void Awake()
    {
        motor = GetComponent<CharacterMotor>();
        gun = GetComponentInChildren<GunController>();
    }

    void Update()
    {
        Vector3 diff = PlayerController.player.transform.position - transform.position;
        motor.lookDir = diff.normalized;

        if (Physics.SphereCast(gun.transform.position, 0.2f, gun.transform.forward, out RaycastHit hit))
        {
            Debug.Log(hit.collider);
            TankController tank = hit.collider.GetComponentInParent<TankController>();
            gun.shouldFire = (tank != null && tank != this);
        }
    }
}
