using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : SkillController
{
    float _timer = 0;
    List<Transform> _transformList = new List<Transform>();

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, Managers.Game.GetPlayerPosition() + new Vector3(0.5f, 0.5f, 0), 2.8f * Time.deltaTime);
        gameObject.GetComponent<SpriteRenderer>().flipX = Managers.Game.Player.GetIsFlip();

        _timer += Time.deltaTime;

        if (_timer > DroneAttackSpeed)
        {
            StartCoroutine(SpawenRocket());
            _timer = 0f;
        }
    }

    IEnumerator SpawenRocket()
    {
        _transformList = Managers.Game.Player.GetComponent<Scanner>().GetNearestTargets();
        yield return new WaitForEndOfFrame();

        int count = _transformList.Count;
        if (count > 30)
            count = 30;

        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPos = transform.position + new Vector3(Managers.Game.Player.GetIsFlip() == true ? -0.2f : 0.2f, 0, 0);
            GameObject obj = Managers.Resource.Instantiate("Skill/Rocket", transform.parent);
            obj.transform.position = spawnPos;
            obj.GetComponent<Rocket>().Target = _transformList[i];
            yield return new WaitForSeconds(0.15f);
        }
    }
}