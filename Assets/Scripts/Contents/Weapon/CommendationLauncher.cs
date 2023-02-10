using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CommendationLauncher : MonoBehaviour 
{
    float _timer;
    float _attackSpeed;

    private void Start()
    {
        _attackSpeed = 1f;
    }

    private void FixedUpdate()
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
        if (Managers.Game.Player == null)
            return;

        if (Managers.Game.Player.GetComponent<Scanner>().NearestTarget == null)
            return;

        Vector3 targetPos = Managers.Game.Player.GetComponent<Scanner>().NearestTarget.position;
        Vector3 dir = targetPos - Managers.Game.Player.transform.position;
        dir = dir.normalized;

        GameObject com = Managers.Resource.Instantiate("Skill/Commendation", transform.parent);
        com.transform.position = transform.position;
        com.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        com.GetComponent<Commendation>().Init(dir);
    }
}
