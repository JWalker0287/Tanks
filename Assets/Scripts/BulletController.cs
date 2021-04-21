using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float lifeTime = 5;

    void OnEnable()
    {
        StartCoroutine(LifeCoroutine());
    }
    void OnTriggerEnter(Collider c)
    {
        gameObject.SetActive(false);
        PlayerController p = c.gameObject.GetComponent<PlayerController>();
        if (p != null)
        {
            PlayerController.player.Death();
        }
    }
    IEnumerator LifeCoroutine()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }
}
