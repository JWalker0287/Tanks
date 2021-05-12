using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public static List<BulletController> bullets = new List<BulletController>();
    public int maxBounces = 1;
    public float lifeTime = 5;
    int bounces = 0;
    Rigidbody body;
    void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        bullets.Add(this);
        StartCoroutine(LifeCoroutine());
    }
    void OnDisable()
    {
        bullets.Remove(this);
    }

    void OnCollisionEnter(Collision c)
    {
        bounces ++;
        transform.forward = body.velocity.normalized;

        TankController tank = c.gameObject.GetComponent<TankController>();
        BulletController bullet = c.gameObject.GetComponent<BulletController>();
        MineController mine = c.gameObject.gameObject.GetComponentInParent<MineController>();

        PlayerController p = c.gameObject.GetComponent<PlayerController>();
        if (bounces > maxBounces || tank != null || bullet != null || mine != null)
        {
            bounces = 0;
            gameObject.SetActive(false);
            StopAllCoroutines();
            ParticleManager.Play("BulletExplode", transform.position);
            AudioManager.PlayVaried("explosion_dull");
        }
        else
        {
            AudioManager.PlayVaried("thud2");
        }
    }
    IEnumerator LifeCoroutine()
    {
        yield return new WaitForSeconds(lifeTime);
        bounces = 0;
        gameObject.SetActive(false);
    }
}
