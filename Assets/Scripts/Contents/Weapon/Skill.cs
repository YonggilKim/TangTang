using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static Define;

//TODO �������� �ʿ� ��������� ã��
public class Skill : MonoBehaviour
{
    public SkillType _skillType;
    public float Damage { get; set; }
    public int Penetration { get; set; } //����
    public Vector3 Dir;

    //TODO DataManager�� ��ų�� ��������
    public void SetSpiner()
    {
        GameObject spinner = Managers.Resource.Instantiate("Skill/Spinner",gameObject.transform);
        spinner.GetComponent<Spinner>().Init();
    }

    public void SetCommendation()
    {
        GameObject launcher = Managers.Resource.Instantiate("Skill/BulletLauncher", gameObject.transform);
    }

}