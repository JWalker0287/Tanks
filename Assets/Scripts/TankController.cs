using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public int team = 1;
    public float speed = 10;
    //Material leftTread;
    //Material rightTread;
    CharacterMotor motor;
    public Renderer treads;
    TankController tank;

    void Awake()
    {
        tank = GetComponent<TankController>();
        motor = GetComponent<CharacterMotor>();
        //leftTread = treads.materials[1];
        //rightTread = treads.materials[2];
    }

    void Update()
    {
        Vector3 forward = motor.moveRoot.forward;
        Vector3 target = motor.moveDir;

        float dot = Vector3.Dot(forward, target);
        float cross = Vector3.Cross(forward,target).y;

        float left = dot;
        float right = dot;
        if (dot < 0.707f && dot > -0.707f)
        {
            left = cross;
            right = -cross;
        }

        //leftTread.mainTextureOffset += Vector2.right * left * Time.deltaTime * speed;
        //rightTread.mainTextureOffset += Vector2.right * right * Time.deltaTime * speed;
    }
    void OnCollisionEnter(Collision c)
    {
        BulletController bullet = c.gameObject.GetComponentInChildren<BulletController>();
        if (bullet == null) return;
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        ParticleManager.Play("TankExplode", transform.position);
        AudioManager.PlayVaried("Explosion");
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
        GameManager.TankDestroyed(tank);
    }
}
