using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firebomb : MonoBehaviour
{
    Vector3 _vector;
    private void Start()
    {
        transform.position = Managers.Game.Player.transform.position;
        _vector = Managers.Game.Player.FollowPoint.transform.position;
    }

    void Update()
    {
        transform.Rotate(0, 0, +3f);
        transform.position = Vector2.MoveTowards(this.transform.position, _vector, Time.deltaTime * 3);
            
        if(transform.position == _vector)
        {
            GameObject fireEffect = Managers.Resource.Instantiate("Skill/FireEffect");
            fireEffect.transform.position = _vector;
            Managers.Sound.Play("tangtang/game/bullet/create/FireBombSound");
            Destroy(this.gameObject);
        }
    }
}
