using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commendation : MonoBehaviour
{
    Rigidbody2D _rigid;
    private int _penetration = 3;
    
    public void Init(Vector3 dir)
    {
        _rigid = GetComponent<Rigidbody2D>();
        _rigid.velocity = dir * 10f;
        _penetration = Managers.Game.Skill.CommPenCount;
        StartCoroutine(CheckMissing());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Monster") || _penetration == -1)
            return;

        _penetration--;

        if (_penetration == -1)
        { 
            _rigid.velocity = Vector3.zero;
            Managers.Resource.Destroy(gameObject);
        }
    }

    //ǥâ�� �������� ��� �ڵ����� �ı�
    IEnumerator CheckMissing()
    {
        yield return new WaitForSeconds(3f);
        Managers.Resource.Destroy(gameObject);
    }
}
