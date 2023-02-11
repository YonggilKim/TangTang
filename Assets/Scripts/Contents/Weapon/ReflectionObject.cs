using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
public class ReflectionObject : MonoBehaviour
{
    public SkillType _skillType = SkillType.Ball;
    Rigidbody2D _rb;
    Vector3 _lastVeclocity;
    Vector3[] dirList = new Vector3[] {
    new Vector3(1,1,0), new Vector3(1,-1,0), new Vector3(-1,1,0), new Vector3(-1,-1,0)
    };
    float objectSpeed = 35;
    float rotateValue = 5;

    void Start()
    {
        ShootObject();
        StartCoroutine(CheckStopBall());
    }

    void FixedUpdate()
    {
        _lastVeclocity = _rb.velocity;

        transform.Rotate(0, 0, rotateValue);

        float randomValue = 1;
        // 카메라를 벗어나지 않도록 범위 제한
        Vector3 position = Camera.main.WorldToViewportPoint(transform.position);
        if (position.x < 0f)
        {
            position.x = 0f;
            var direction = Vector3.Reflect(_lastVeclocity.normalized, new Vector3(randomValue, 0, 0));
            _rb.velocity = direction * Mathf.Max(_lastVeclocity.magnitude, 0f);
        }
        if (position.y < 0f)
        {
            position.y = 0f;
            var direction = Vector3.Reflect(_lastVeclocity.normalized, new Vector3(0, randomValue, 0));
            _rb.velocity = direction * Mathf.Max(_lastVeclocity.magnitude, 0f);
        }
        if (position.x > 1f)
        {
            position.x = 1f;
            var direction = Vector3.Reflect(_lastVeclocity.normalized, new Vector3(randomValue * -1, 0, 0) );
            _rb.velocity = direction * Mathf.Max(_lastVeclocity.magnitude, 0f);
        }
        if (position.y > 0.9f)
        {
            position.y = 0.9f;
            var direction = Vector3.Reflect(_lastVeclocity.normalized, new Vector3(0, randomValue * -1, 0));
            _rb.velocity = direction * Mathf.Max(_lastVeclocity.magnitude, 0f);
        }
        transform.position = Camera.main.ViewportToWorldPoint(position);
    }

    void ShootObject()
    {
        Vector3 ranDir = dirList[Random.Range(0, 4)];
        _rb = this.gameObject.GetComponent<Rigidbody2D>();
        transform.position = Managers.Game.Player.transform.position + Vector3.up;

        if (gameObject.name.Contains("Ball"))
        {
            _skillType = SkillType.Ball;
            objectSpeed = 35;
        }
        else
        {
            _skillType = SkillType.Tree;
            objectSpeed = 180;
            rotateValue = 2;
        }
        _rb.AddForce(ranDir * objectSpeed);
    }

    IEnumerator CheckStopBall()
    {
        if(_rb.velocity.magnitude < 0.1f) 
        {
            ShootObject();
        }
        yield return new WaitForSeconds(1f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            return;
        if (_skillType == SkillType.Ball)
        {
            collision.gameObject.GetComponent<MonsterController>()?.OnHitted(this.gameObject);
            var speed = _lastVeclocity.magnitude;
            var direction = Vector3.Reflect(_lastVeclocity.normalized, collision.contacts[0].normal);
            _rb.velocity = direction * Mathf.Max(_lastVeclocity.magnitude, 0f);
        }
    }
}
