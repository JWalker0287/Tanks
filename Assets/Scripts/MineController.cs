using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : MonoBehaviour
{
    public GameObject explosion;
    public GameObject mineMesh1;
    public GameObject mineMesh2;
    bool isExploding = false;
    Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        StartCoroutine(Explode());
    }
    void OnDisable()
    {
        explosion.SetActive(false);
        mineMesh1.SetActive(true);
        mineMesh2.SetActive(true);
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(5);
        anim.Play("CountDown");
        yield return new WaitForSeconds(1);
        isExploding = true;
        AudioManager.PlayVaried("synthetic_explosion_1");
        anim.Play("Explosion");
        yield return new WaitForSeconds(0.4f);
        PlayerController.player.mines --;
        gameObject.SetActive(false);
        isExploding = false;
    }

    IEnumerator JustExplode()
    {
        isExploding = true;
        AudioManager.PlayVaried("synthetic_explosion_1");
        anim.Play("Explosion");
        mineMesh1.SetActive(false);
        mineMesh2.SetActive(false);
        yield return new WaitForSeconds(0.4f);
        PlayerController.player.mines --;
        gameObject.SetActive(false);
        isExploding = false;
    }

    void OnCollisionEnter(Collision c)
    {
        BulletController bullet = c.gameObject.GetComponent<BulletController>();
        if (bullet == null || isExploding) return;
        StopAllCoroutines();
        StartCoroutine(JustExplode());
    }
    void OnTriggerEnter(Collider c)
    {
        MineController mine = c.gameObject.GetComponentInParent<MineController>();
        if (mine == null || isExploding) return;
        StopAllCoroutines();
        StartCoroutine(JustExplode());
    }
}
