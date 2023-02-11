using Data;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
    private void Awake()
    {
        string spriteName;
        int rand = Random.Range(0, 3);
        if (rand == 0)
            spriteName = "EXP";
        else if (rand == 1)
            spriteName = "EXP_big";
        else
            spriteName = "Exp_mid";
        GetComponent<SpriteRenderer>().sprite = Managers.Resource.Load<Sprite>($"Art/other/{ spriteName}");
    }

    void FixedUpdate()
    {
        float dist = Vector3.Distance(gameObject.transform.position, Managers.Game.GetPlayerPosition());
        if (dist < 1.5)
        {
            transform.position = Vector3.MoveTowards(transform.position, Managers.Game.GetPlayerPosition(), Time.deltaTime * 5.0f);
            if (dist < 0.2f)
            {
                Monster monster = new Monster();
                Managers.Data.MonsterDic.TryGetValue(Managers.Game.PlayerLevel, out monster);
                Managers.Game.PlayerExp += monster.exp;
                Managers.Resource.Destroy(gameObject);
            }
        }
        
    }
}
