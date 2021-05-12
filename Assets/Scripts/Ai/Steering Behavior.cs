using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehavior : MonoBehaviour
{
    public Transform target;
    [Range(0,1)]public float weight = 1;

    public virtual Vector3 GetSteering()
    {
        return (target.position - transform.position).normalized * weight;
    }
}
