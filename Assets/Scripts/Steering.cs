using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Steering : MonoBehaviour
{
    NavMeshPath path;
    int index;
    CharacterMotor motor;
    IEnumerator Start()
    {
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
        motor.moveDir = diff.normalized;

        if (diff.sqrMagnitude < 0.1f) index ++;
    }
}
