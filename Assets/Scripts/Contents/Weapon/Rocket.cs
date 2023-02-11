using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [NonSerialized]
    public Transform Target;
    [NonSerialized]
    public Rigidbody2D rigidBody;
    private float _rotateAmount = 200;
    private float _movementSpeed = 5;
    float _timer = 0;

    private void Awake()
    {
        rigidBody= GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        _timer += Time.deltaTime;
        if (_timer > 5 || Target.gameObject == null)
        {
            Explosion();
            _timer = 0;
            return;
        }

        Vector2 direction = (Vector2)Target.position - rigidBody.position;
        direction.Normalize();
        float rotateSpeed = Vector3.Cross(direction, transform.up).z;
        rigidBody.angularVelocity = -_rotateAmount * rotateSpeed;
        rigidBody.velocity = transform.up * _movementSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Monster"))
            return;
        Explosion();
    }

    void Explosion()
    {
        GameObject obj = Managers.Resource.Instantiate("Skill/RocketEffect", gameObject.transform.parent);
        obj.transform.position = gameObject.transform.position;
        Destroy(gameObject);
    }
}
