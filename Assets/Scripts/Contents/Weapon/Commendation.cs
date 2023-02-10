using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commendation : CommendationLauncher
{
    Rigidbody2D rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    //TODO ǥâ ���߷��� ������ �����̻��ϰ��� �������� �ٲٱ�
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
