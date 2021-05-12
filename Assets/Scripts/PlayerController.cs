using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(CharacterMotor))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController player;
    public int maxMines = 6;
    public int mines = 0;
    public string xAxis = "Horizontal";
    public string yAxis = "Vertical";
    CharacterMotor motor;
    GunController[] guns;
    public float lookInfluence = 2;
    Camera cam;

    void Awake()
    {
        player = this;
        motor = GetComponent<CharacterMotor>();
        guns = GetComponentsInChildren<GunController>();
        cam = Camera.main;
        //GetComponent<Animator>().Play("PlayerStart");
    }
    void Update()
    {
        float x = Input.GetAxisRaw(xAxis);
        float z = Input.GetAxisRaw(yAxis);
        motor.moveDir = new Vector3(x,0,z).normalized;
        UpdateMouse();
    }

    void UpdateMouse()
    {
        Vector3 pos = cam.WorldToScreenPoint(transform.position);
        Vector3 diff  = Input.mousePosition - pos;
        Vector3 dir = new Vector3(diff.x,0,diff.y).normalized;
        motor.lookDir = dir;
        bool shouldFire = Input.GetMouseButtonDown(0);
        if (Input.GetMouseButtonDown(1)) 
        {
            SetMine();
        }
        for (int i = 0;i < guns.Length; i ++)
        {
            guns[i].shouldFire = shouldFire;
        }
    }

    void SetMine()
    {
        if (mines < maxMines)
        {
            Spawner.Spawn("Mine", transform.position);
            AudioManager.PlayVaried("beep_01");
            mines ++;
        }
    }
}
