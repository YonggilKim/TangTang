using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BulletLauncher : Skill 
{
    float _timer;
    float _attackSpeed;
    PlayerController _player;

    private void Awake()
    {
        _player = GetComponentInParent<PlayerController>();
        _attackSpeed = 1f;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _attackSpeed)
        {
            Fire();
            _timer = 0f;
        }
    }

    void Fire()
    {
        if (_player == null)
            return;

        if (_player.GetComponent<Scanner>().nearestTarget == null)
            return;

        Vector3 targetPos = _player.GetComponent<Scanner>().nearestTarget.position;
        Vector3 dir = targetPos - _player.transform.position;
        dir = dir.normalized;

q
        GameObject com = Managers.Resource.Instantiate("Skill/Commendation", transform.parent);
        com.transform.position = transform.position;
        com.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        com.GetComponent<Commendation>().Init(dir);
    }
}
