using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    Animator anim;
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    void OnEnable()
    {
        StartCoroutine(Explode());
    }
    IEnumerator Explode()
    {
        anim.SetTrigger("Explosion");
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }
}
