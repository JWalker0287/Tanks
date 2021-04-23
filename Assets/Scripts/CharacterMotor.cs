using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMotor : MonoBehaviour
{
    public Vector3 moveDir;
    public Vector3 lookDir;
    public float speed = 10;
    public float turnSpeed = 10;
    public float maxVelocityChange = 1;
    public Transform moveRoot;
    public Transform lookRoot;
    Rigidbody body;
    void Awake ()
    {
        body = GetComponent<Rigidbody>();
        if (lookRoot == null) lookRoot = transform;
        if (moveRoot == null) moveRoot = transform;
    }

    void Update ()
    {
        if (lookDir.sqrMagnitude > 0.1f)
        {   
            lookRoot.forward = Vector3.Lerp(
                lookRoot.forward,
                lookDir,
                Time.deltaTime * turnSpeed
            );
        }
        if (body.velocity.sqrMagnitude > 0.1f)
        {
            moveRoot.forward = Vector3.Lerp(
                moveRoot.forward,
                moveDir,
                Time.deltaTime * turnSpeed
            );
        }
    }

    void FixedUpdate ()
    {
        Vector3 velocityChange = moveDir * speed - body.velocity;
        velocityChange.y = 0;
        if (velocityChange.magnitude > maxVelocityChange)
        {
            velocityChange = velocityChange.normalized * maxVelocityChange;
        }
        body.AddForce(velocityChange, ForceMode.VelocityChange);
    }
}
