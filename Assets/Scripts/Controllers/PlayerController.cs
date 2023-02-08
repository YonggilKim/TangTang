using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Define;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D RigidBody { get; private set; }
    public Animator anim;
    public UI_Joystick joystickMovement;

    private float _playerSpeed;
    public float PlayerSpeed 
    {
        get { return _playerSpeed; }
        set 
        { 
            _playerSpeed = value; 
        }
    }

    private void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Managers.UI.ShowSceneUI<UI_Joystick>();
        //TODO 조이스틱 값을 좀더 효율적으로 받는방법?
        joystickMovement = GameObject.Find("UI_Joystick").GetComponent<UI_Joystick>();
        PlayerSpeed = 3;
        Managers.Game.Player = this;
    }

    private void Update()
    {
        //transform.eulerAngles
        //rb.velocity.normalized
        //Debug.Log($"{rb.velocity.normalized}");
        //Debug.Log($"{transform.TransformDirection}");
        if (RigidBody.velocity.magnitude > 0)
        {
            anim.Play("Player_Walk");
        }
        else
        {
            anim.Play("0");
        }
        
        UpdatePlayerDirection();

        if (joystickMovement.JoystickVec.y != 0)
        {
            RigidBody.velocity = new Vector2(joystickMovement.JoystickVec.x * _playerSpeed, joystickMovement.JoystickVec.y * _playerSpeed);
        }
        else
        {
            RigidBody.velocity = Vector2.zero;
        }
    }

    void UpdatePlayerDirection()
    {
        if (joystickMovement.JoystickVec.x < 0)
        {
            //Debug.Log(" Going Left");
            this.gameObject.transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            //if (Gun.activeSelf == true)
            //{
            //    Gun.transform.localScale = new Vector3(-0.2446888f, 0.2446888f, 0.2446888f);
            //}
        }

        if (joystickMovement.JoystickVec.x > 0)
        {
            //Debug.Log("Going Right");
            this.gameObject.transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            //if (Gun.activeSelf == true)
            //{
            //    Gun.transform.localScale = new Vector3(0.2446888f, 0.2446888f, 0.2446888f);
            //}
        }
    }
}
