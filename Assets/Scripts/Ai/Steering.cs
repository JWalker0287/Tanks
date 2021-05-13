using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Steering : MonoBehaviour
{
    public float avoidWeight = 1;
    public float pathWeight = 1;
    public float updateInterval = 1;
    NavMeshPath path;
    int index;
    CharacterMotor motor;
    //SteeringBehavior[] behaviors;
    IEnumerator Start()
    {
        //behaviors = GetComponentsInChildren<SteeringBehavior>();
        motor = GetComponent<CharacterMotor>();
        path = new NavMeshPath();
        yield return new WaitForSeconds(Random.Range(0,updateInterval));
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

        motor.moveDir = (PathSteer() + AvoidSteer()).normalized;
    }

    Vector3 PathSteer()
    {
        if (path.status != NavMeshPathStatus.PathComplete || index >= path.corners.Length) return Vector3.zero;

        Vector3 diff = path.corners[index] - transform.position;
        diff.y = 0;
        if (diff.sqrMagnitude < 0.1f) index ++;

        return diff.normalized * pathWeight;
    }

    Vector3 AvoidSteer()
    {
        Vector3 avoidSteer = Vector3.zero;
        foreach (BulletController bullet in BulletController.bullets)
        {
            Vector3 diff = transform.position - bullet.transform.position;
            float dist = diff.magnitude;
            Vector3 dir = diff/dist;
            Vector3 velo = bullet.GetComponent<Rigidbody>().velocity;
            float speed = velo.magnitude;
            float dot = Vector3.Dot(dir,velo/speed);
            if (dot <= 0) continue;

            float t = dist/speed;
            Vector3 dest = bullet.transform.position + velo * t;
            dest.y = 0;
            avoidSteer += transform.position - dest;
        }
        return avoidSteer.normalized * avoidWeight;
        
    }
}
