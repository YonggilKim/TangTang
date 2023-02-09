using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commendation : BulletLauncher
{
    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    //TODO 표창 명중률이 떨어짐 유도미사일같은 로직으로 바꾸기
    public void Init(Vector3 dir)
    {
        rigid.velocity = dir * 10f;
        Damage = 100f;
        Penetration = 3;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Monster") || Penetration == -1)
            return;

        Penetration--;

        if (Penetration == -1)
        { 
            rigid.velocity = Vector3.zero;
            Managers.Resource.Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
            Managers.Resource.Destroy(gameObject);
    }

}
