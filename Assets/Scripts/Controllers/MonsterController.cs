using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    float _speed = 1.7f;
    public float _health;
    public float _maxHealth;

    Rigidbody2D _target;

    bool _isLive = true;

    Rigidbody2D _rigid;
    SpriteRenderer _spriter;
    Animator _anim;
    public RuntimeAnimatorController[] _runtimeAnimatorController;


    private void OnEnable()
    {
        _target = Managers.Game.Player.GetComponent<Rigidbody2D>();
        _isLive = true;
        _health = _maxHealth = 0;
    }
    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _spriter = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (!_isLive)
            return;
        Vector2 dirVec = _target.position - _rigid.position;
        Vector2 nextVec = dirVec.normalized * _speed * Time.fixedDeltaTime;
        _rigid.MovePosition(_rigid.position + nextVec);
        _rigid.velocity = Vector2.zero;

        float dist = Vector2.Distance(_target.position, _rigid.position);
        if (dist > 15)
            MonsterReposition();

    }

    public void Init(Monster data)
    {
        _speed = data.speed;
        _maxHealth = data.maxHealth;
        _health = data.health;
    }

    public void MonsterReposition()
    {
        _rigid.MovePosition(Util.RandomPointInAnnulus((Vector2)_target.position));
    }

    private void LateUpdate()
    {
        _spriter.flipX = _target.position.x < _rigid.position.x;
    }

}
