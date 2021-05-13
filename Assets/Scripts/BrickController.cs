using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    void OnTriggerEnter(Collider c)
    {
        MineController mine = c.gameObject.GetComponentInParent<MineController>();
        if (mine == null) return;
        gameObject.SetActive(false);
    }
}
