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
        _monster = data; //얕은복사 괜찮은지?
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

        //TODO 구조잘못짬 데미지 처리하는 구조 생각하기
        int damage = Managers.Game.GetNameTODamage(other.name);
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
        //TODO 애니메이터 종료 콜백 함수로 변경하기
        _anim.Play($"{gameObject.name}_Die");
        yield return new WaitForSeconds(0.6f);
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
