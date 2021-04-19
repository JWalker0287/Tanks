using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMotor : MonoBehaviour
{
    public Vector3 dir;
    public Vector3 lookDir;
    public float speed = 10;
    public float turnSpeed = 20;
    public float maxVelocityChange = 1;
    Rigidbody body;

    void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (lookDir.magnitude > 0.25f)
        {
            transform.forward = Vector3.Lerp(
                transform.forward, 
                lookDir, 
                Time.deltaTime * turnSpeed);
        }
        else if (body.velocity.sqrMagnitude > 0.1f)
        {
            transform.forward = Vector3.Lerp(
                transform.forward, 
                dir, 
                Time.deltaTime * turnSpeed
            );
        }
    }
    
    void FixedUpdate ()
    {
        Vector3 velocityChange = dir * speed - body.velocity;
        velocityChange.y = 0;
        if (velocityChange.magnitude > maxVelocityChange)
        {
            velocityChange = velocityChange.normalized * maxVelocityChange;
        }
        body.AddForce(velocityChange, ForceMode.VelocityChange);
    }
}
