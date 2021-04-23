using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int maxBounces = 1;
    public float lifeTime = 5;
    int bounces = 0;

    void OnEnable()
    {
        StartCoroutine(LifeCoroutine());
    }

    void OnCollisionEnter(Collision c)
    {
        bounces ++;

        TankController tank = c.gameObject.GetComponent<TankController>();
        BulletController bullet = c.gameObject.GetComponent<BulletController>();
        ExplosionController explosion = c.gameObject.gameObject.GetComponent<ExplosionController>();

        PlayerController p = c.gameObject.GetComponent<PlayerController>();
        if (bounces > maxBounces || tank != null || bullet != null || explosion != null)
        {
            gameObject.SetActive(false);
            StopAllCoroutines();
            //ParticalMAnager.Play("Explosion", transform.position);
        }
        else
        {
            //AudioManager.PlayVaried('bounce');
        }
    }
    IEnumerator LifeCoroutine()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }
}
