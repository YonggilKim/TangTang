using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
public class ReflectionObject : MonoBehaviour
{
    public SkillType skillType = SkillType.Ball;
    Rigidbody2D rb;
    Vector3 LastVeclocity;
    bool CheckPos = true;
    Vector3[] dirList = new Vector3[] {
    new Vector3(1,1,0), new Vector3(1,-1,0), new Vector3(-1,1,0), new Vector3(-1,-1,0)
    };
    float objectSpeed = 35;
    float rotateValue = 5;

    void Start()
    {
        Vector3 ranDir = dirList[Random.Range(0, 4)];
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        transform.position = Managers.Game.Player.transform.position + Vector3.up;

        if (gameObject.name.Contains("Ball"))
        {
            skillType = SkillType.Ball;
            objectSpeed = 35;
        }
        else
        {
            skillType = SkillType.Tree;
            objectSpeed = 180;
            rotateValue = 1;
        }
        StartCoroutine(StopBall());
        rb.AddForce(ranDir * objectSpeed);
    }

    void Update()
    {
        LastVeclocity = rb.velocity;
        if (CheckPos == true)
        {
            //CheckPos = false;
        }
        transform.Rotate(0, 0, rotateValue);

        float randomValue = 1;
        // 카메라를 벗어나지 않도록 범위 제한
        Vector3 position = Camera.main.WorldToViewportPoint(transform.position);
        if (position.x < 0f)
        {
            position.x = 0f;
            var direction = Vector3.Reflect(LastVeclocity.normalized, new Vector3(randomValue, 0, 0));
            rb.velocity = direction * Mathf.Max(LastVeclocity.magnitude, 0f);
        }
        if (position.y < 0f)
        {
            position.y = 0f;
            var direction = Vector3.Reflect(LastVeclocity.normalized, new Vector3(0, randomValue, 0));
            rb.velocity = direction * Mathf.Max(LastVeclocity.magnitude, 0f);
        }
        if (position.x > 1f)
        {
            position.x = 1f;
            var direction = Vector3.Reflect(LastVeclocity.normalized, new Vector3(randomValue * -1, 0, 0) );
            rb.velocity = direction * Mathf.Max(LastVeclocity.magnitude, 0f);
        }
        if (position.y > 0.9f)
        {
            position.y = 0.9f;
            var direction = Vector3.Reflect(LastVeclocity.normalized, new Vector3(0, randomValue * -1, 0));
            rb.velocity = direction * Mathf.Max(LastVeclocity.magnitude, 0f);
        }
        transform.position = Camera.main.ViewportToWorldPoint(position);
    }

    IEnumerator StopBall()
    {
        yield return new WaitForSeconds(0.5f);
        // CheckPos = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            return;
        if (skillType == SkillType.Ball)
        {
            collision.gameObject.GetComponent<MonsterController>()?.OnHitted(this.gameObject);
            var speed = LastVeclocity.magnitude;
            var direction = Vector3.Reflect(LastVeclocity.normalized, collision.contacts[0].normal);
            rb.velocity = direction * Mathf.Max(LastVeclocity.magnitude, 0f);
        }
    }
}
