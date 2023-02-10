using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CommendationLauncher : Skill 
{
    float _timer;
    float _attackSpeed;

    private void Start()
    {
        _attackSpeed = 1f;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _attackSpeed)
        {
            FireComm();
            _timer = 0f;
        }
    }

    void FireComm()
    {
        if (_player == null)
            return;

        if (_player.GetComponent<Scanner>().NearestTarget == null)
            return;

        Vector3 targetPos = _player.GetComponent<Scanner>().NearestTarget.position;
        Vector3 dir = targetPos - _player.transform.position;
        dir = dir.normalized;

        GameObject com = Managers.Resource.Instantiate("Skill/Commendation", transform.parent);
        com.transform.position = transform.position;
        com.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        com.GetComponent<Commendation>().Init(dir);
    }
}
