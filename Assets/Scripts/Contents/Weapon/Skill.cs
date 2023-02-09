using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static Define;

//TODO 구조변경 필요 더좋은방법 찾기
public class Skill : MonoBehaviour
{
    public SkillType _skillType;
    public float Damage { get; set; }
    public int Penetration { get; set; } //관통
    public Vector3 Dir;

    //TODO DataManager로 스킬값 가져오기
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
