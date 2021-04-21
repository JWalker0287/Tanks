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
        
        motor.dir = (PlayerController.player.transform.position - transform.position).normalized;
      
    }

    void OnEnable()
    {
        GetComponent<Animator>().Play("EnemySpawn");
    }

    void OnCollisionEnter(Collision c)
    {
        PlayerController p = c.gameObject.GetComponent<PlayerController>();
        if (p == null) return;

        PlayerController.player.Death();

    }
}
    