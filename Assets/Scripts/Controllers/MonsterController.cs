using Data;
using DG.Tweening;
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
    bool _isKnockback = false;
    Rigidbody2D _rigid;
    Collider2D _collider;
    SpriteRenderer _spriter;
    Animator _anim;
    Monster _monster;

    private void OnEnable()
    {
        _target = Managers.Game.Player.GetComponent<Rigidbody2D>();
        _isLive = true;
        _collider.enabled = true;
        _rigid.simulated = true;
        _anim.Play($"{gameObject.name}_Walk");
    }

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _spriter = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (!_isLive || _isKnockback == true)
            return;
        Vector2 dirVec = _target.position - _rigid.position;
        Vector2 nextVec = dirVec.normalized * _speed * Time.fixedDeltaTime;
        _rigid.MovePosition(_rigid.position + nextVec);
        _rigid.velocity = Vector2.zero;
        //Debug.DrawRay(_rigid.position, dirVec, Color.green);
        Debug.DrawLine(_rigid.position, _target.position);

        float dist = Vector2.Distance(_target.position, _rigid.position);
        if (dist > 15)
            MonsterReposition();
    }

    public void Init(Monster data)
    {
        _speed = data.speed;
        _maxHealth = data.maxHealth;
        _health = data.health;
        _monster = data; //???????? ?????????
    }

    public void MonsterReposition()
    {
        _rigid.MovePosition(Util.RandomPointInAnnulus((Vector2)_target.position));
    }

    private void LateUpdate()
    {
        _spriter.flipX = _target.position.x < _rigid.position.x;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Weapon"))
            return;

        //TODO ?????߸?« ?????? ó???ϴ? ???? ?????ϱ?
        OnHitted(other.gameObject);
    }

    public void OnHitted(GameObject obj)
    {
        int damage = Managers.Game.GetNameTODamage(obj.name);
        _health -= damage;

        if (_health > 0)
        {
            StartCoroutine("KnockbBack");
            _anim.Play($"{gameObject.name}_Hit");
        }
        else
        {
            StartCoroutine("Dead");
        }
    }
    IEnumerator Dead()
    {
        _isLive= false;
        _collider.enabled = false;
        _rigid.simulated = false;
        //TODO ?ִϸ????? ???? ?ݹ? ?Լ??? ?????ϱ?
        _anim.Play($"{gameObject.name}_Die");
        Managers.Game.NumDeadMonsters++;
        yield return new WaitForSeconds(0.6f);
        // ?????? ??????
        int rand = Random.Range(0, 10);
        if (rand < 6)
        { 
            GameObject gem = Managers.Resource.Instantiate("Exp/Exp", Managers.Game.Exproot);
            gem.transform.position = transform.position;
        }

        Managers.Resource.Destroy(this.gameObject);

    }

    IEnumerator KnockbBack()
    {
        yield return new WaitForFixedUpdate();
        _isKnockback = true;
        Vector3 playerPos = Managers.Game.Player.transform.position;
        Vector3 dir = transform.position - playerPos;
        _rigid.AddForce(dir.normalized * 2, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);
        _isKnockback = false;
    }
}
