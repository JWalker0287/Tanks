using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraController : MonoBehaviour
{
    public float speed = 10;
    public Transform target;
    public Vector3 offset = Vector3.up * 10;

#if UNITY_EDITOR
    void Update ()
    {
        if (!Application.isPlaying) transform.position = target.position + offset;
    }
    #endif
    void FixedUpdate()
    {
        if (target == null) return;
        Vector3 targetPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.fixedDeltaTime * speed);
    }
}
