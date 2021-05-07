using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public int team = 1;
    public float treadSpeed = 10;
    //Material leftTread;
    //Material rightTread;
    CharacterMotor motor;
    public Renderer treads;
    TankController tank;
    GunController gun;

    public float trackDist = 0.25f;
    public float trackRot = 15;
    Vector3 lastTrackPos;
    Vector3 lastTtrackDir;

    void Awake()
    {
        tank = GetComponent<TankController>();
        motor = GetComponent<CharacterMotor>();
        gun = GetComponentInChildren<GunController>();
        //leftTread = treads.materials[1];
        //rightTread = treads.materials[2];
        lastTrackPos = motor.moveRoot.position;
        lastTtrackDir = motor.moveRoot.forward;
    }

    void Update()
    {
        UpdateTracks();
        UpdateTreads();
    }

    void UpdateTracks()
    {
        float sqDist = (motor.moveRoot.position - lastTrackPos).sqrMagnitude;
        float angle = Vector3.Angle(motor.moveRoot.forward, lastTtrackDir);
        if (sqDist > trackDist  * trackDist || angle > trackRot)
        {
            Spawner.Spawn("Tracks", motor.moveRoot.position, motor.moveRoot.rotation);
            lastTrackPos = motor.moveRoot.position;
            lastTtrackDir = motor.moveRoot.forward;
        }
    }

    void UpdateTreads()
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
        Spawner.Spawn("DeathX", transform.position);
        AudioManager.PlayVaried("Explosion");
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
        GameManager.TankDestroyed(tank);
    }
}
