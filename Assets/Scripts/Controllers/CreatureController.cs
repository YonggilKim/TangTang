using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class CreatureController : MonoBehaviour
{
    public float _speed = 5.0f;

    public Vector3Int CellPos { get; set; } = Vector3Int.zero;
    protected Animator _animator;
    protected SpriteRenderer _sprite;

    CreatureState _state = CreatureState.Idle;
    public CreatureState State
    {
        get { return _state; }
        set
        {
            if (_state == value)
                return;

            _state = value;
            UpdateAnimation();
        }
    }

    MoveDir _lastDir = MoveDir.Down;
    MoveDir _dir = MoveDir.Down;
    public MoveDir Dir
    {
        get { return _dir; }
        set
        {
            if (_dir == value)
                return;

            _dir = value;
            if (value != MoveDir.None)
            {
                _lastDir = value;
            }
            UpdateAnimation();
        }
    }

    #region LifeCycle
    void Start()
    {
        Init();

    }

    void Update()
    {
        UpdateController();
    }

    #endregion

    protected virtual void Init()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        //Vector3 pos = Managers.Map.CurrentGrid.CellToWorld(CellPos) + new Vector3(0.5f, 0.64f);
        //transform.position = pos;
    }

    protected virtual void UpdateController()
    {
        UpdateIsMoving();
        UpdatePosition();
    }

    protected virtual void UpdateAnimation()
    {
        if (_state == CreatureState.Idle)
        {
            switch (_lastDir)
            {
                case MoveDir.Up:
                    _animator.Play("IDLE_BACK");
                    _sprite.flipX = false;
                    break;
                case MoveDir.Down:
                    _animator.Play("IDLE_FRONT");
                    _sprite.flipX = false;
                    break;
                case MoveDir.Left:
                    _animator.Play("IDLE_RIGHT");
                    _sprite.flipX = true;
                    break;
                case MoveDir.Right:
                    _animator.Play("IDLE_RIGHT");
                    _sprite.flipX = false;
                    break;
            }
        }
        else if (_state == CreatureState.Moving)
        {
            switch (_dir)
            {
                case MoveDir.Up:
                    _animator.Play("WALK_BACK");
                    _sprite.flipX = false;

                    break;
                case MoveDir.Down:
                    _animator.Play("WALK_FRONT");
                    _sprite.flipX = false;

                    break;
                case MoveDir.Left:
                    _animator.Play("WALK_RIGHT");
                    _sprite.flipX = true;
                    break;
                case MoveDir.Right:
                    _animator.Play("WALK_RIGHT");
                    _sprite.flipX = false;

                    break;
            }
        }
        else if (_state == CreatureState.Skill)
        {
            //TODO
        }
        else
        {

        }
    }

    #region Player Move Functions
    //셀단위로 스르르움직이는거 구현
    void UpdatePosition()
    {
        //if (State != CreatureState.Moving) return;

        //Vector3 destPos = Managers.Map.CurrentGrid.CellToWorld(CellPos) + new Vector3(0.5f, 0.64f);
        //Vector3 moveDir = destPos - transform.position;

        ////도착 여부 체크
        //float dist = moveDir.magnitude;
        //if (dist < _speed * Time.deltaTime)
        //{
        //    transform.position = destPos;
        //    State = CreatureState.Idle;
        //    if (_dir == MoveDir.None)
        //        UpdateAnimation();
        //}
        //else
        //{
        //    transform.position += moveDir.normalized * _speed * Time.deltaTime;// 스르르움직임
        //    State = CreatureState.Moving;
        //}

    }
    //이동가능 상태일때 실제좌표로 이동
    void UpdateIsMoving()
    {
        if (State == CreatureState.Idle && _dir != MoveDir.None)
        {
            Vector3Int destPos = CellPos;
            switch (Dir)
            {
                case MoveDir.Up:
                    destPos += Vector3Int.up;
                    break;
                case MoveDir.Down:
                    destPos += Vector3Int.down;
                    break;
                case MoveDir.Left:
                    destPos += Vector3Int.left;
                    break;
                case MoveDir.Right:
                    destPos += Vector3Int.right;
                    break;
            }
            //if (Managers.Map.CanGo(destPos) == true)
            //{
            //    if (Managers.Object.Find(destPos) == null)
            //    {
            //        State = CreatureState.Moving;
            //        CellPos = destPos;
            //    }
            //}
        }
    }
    #endregion
}
