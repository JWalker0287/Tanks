using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMotor))]
public class EnemyController : MonoBehaviour
{
    CharacterMotor motor;
    void Awake()
    {
        motor = GetComponent<CharacterMotor>();
    }
    void Update()
    {
        
        motor.moveDir = (PlayerController.player.transform.position - transform.position).normalized;
      
    }

    void OnCollisionEnter(Collision c)
    {
        PlayerController p = c.gameObject.GetComponent<PlayerController>();
        if (p == null) return;
    }
}
    