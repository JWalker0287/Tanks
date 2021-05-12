using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Steering : MonoBehaviour
{
    NavMeshPath path;
    int index;
    CharacterMotor motor;
    //SteeringBehavior[] behaviors;
    IEnumerator Start()
    {
        //behaviors = GetComponentsInChildren<SteeringBehavior>();
        motor = GetComponent<CharacterMotor>();
        path = new NavMeshPath();
        yield return new WaitForSeconds(Random.Range(0,3));
        while (enabled)
        {
            Vector3 p = PlayerController.player.transform.position;
            NavMesh.CalculatePath(transform.position, p, NavMesh.AllAreas, path);
            index = 0;
            yield return new WaitForSeconds(1);
        }
    }

    void Update()
    {
        if (path.status != NavMeshPathStatus.PathComplete || index >= path.corners.Length)
        {
            motor.moveDir = Vector3.zero;
            return;
        }
        Vector3 diff = path.corners[index] - transform.position;
        diff.y = 0;
        motor.moveDir = diff.normalized;

        if (diff.sqrMagnitude < 0.1f) index ++;
    }

    Vector3 PathSteer()
    {
        if (path.status != NavMeshPathStatus.PathComplete || index >= path.corners.Length) return Vector3.zero;

        Vector3 diff = path.corners[index] - transform.position;
        diff.y = 0;
        if (diff.sqrMagnitude < 0.1f) index ++;

        return diff.normalized;
    }

    Vector3 AvoidSteer()
    {
        Vector3 avoidSteer = Vector3.zero;
        return avoidSteer;
        
    }
}
