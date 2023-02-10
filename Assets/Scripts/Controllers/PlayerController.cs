using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Define;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D RigidBody { get; private set; }
    public Animator anim;
    private UI_Joystick joystickMovement;
    private SpriteRenderer _sprite;
    [NonSerialized]
    public Skill Skill;
    public GameObject FollowPoint;

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
        _sprite = GetComponent<SpriteRenderer>();
        RigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Skill = GetComponentInChildren<Skill>();
        Managers.UI.ShowSceneUI<UI_Joystick>();
        //TODO 조이스틱 값을 좀더 효율적으로 받는방법?
        joystickMovement = GameObject.Find("UI_Joystick").GetComponent<UI_Joystick>();
        PlayerSpeed = 3;
        Managers.Game.Player = this;
    }

    private void Update()
    {
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

    private void LateUpdate()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    void UpdatePlayerDirection()
    {
        if (joystickMovement.JoystickVec.x < 0)
        {
            _sprite.flipX = true;
        }

        if (joystickMovement.JoystickVec.x > 0)
        {
            //Debug.Log("Going Right");
            _sprite.flipX = false;
        }
    }

}
