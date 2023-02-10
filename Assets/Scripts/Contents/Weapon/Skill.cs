using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static Define;

//TODO �������� �ʿ� ��������� ã��
public class Skill : MonoBehaviour
{
    public SkillType CurrentSkillType { get; set; }
    public float Damage { get; set; }
    public int Penetration { get; set; } //����
    public Vector3 Dir;

    GameManager _game;
    #region ȭ����
    public GameObject _firebomb;
    public GameObject _player;
    public GameObject _target;
    #endregion

    public void Awake()
    {
        _game = Managers.Game;
    }

    //TODO DataManager�� ��ų�� ��������
    public void SetSpiner()
    {
        GameObject spinner = Managers.Resource.Instantiate("Skill/Spinner", gameObject.transform);
        spinner.GetComponent<Spinner>().Init();
    }

    public void SetCommendation()
    {
        GameObject launcher = Managers.Resource.Instantiate("Skill/BulletLauncher", gameObject.transform);
    }

    public void SetFireBomb()
    {
        _player = _game.Player.gameObject;
        _target = _game.Player.FollowPoint;

        StartCoroutine(GenerateFirebomb());
    }

    public void SetDrone()
    {
        GameObject dron = Managers.Resource.Instantiate("Skill/Drone", gameObject.transform);
    }

    public void SetReflectionWeapon(SkillType type)
    {
        switch (type)
        {
            case SkillType.Ball:
                StartCoroutine(GenerateReflectionWeapon(type));
                break;
            case SkillType.Tree:
                StartCoroutine(GenerateReflectionWeapon(type));
                break;
        }

    }

    //ȭ���� ��ġ ��� �� ����(���)
    IEnumerator GenerateFirebomb()
    {
        yield return new WaitForSeconds(2f);
        _firebomb = Managers.Resource.Instantiate("Skill/FireBomb");

        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();//�������� ����� �Ȱ�ģ�� ��?
        _firebomb = Managers.Resource.Instantiate("Skill/FireBomb");

        yield return new WaitForEndOfFrame();
        _firebomb = Managers.Resource.Instantiate("Skill/FireBomb");

        yield return new WaitForEndOfFrame(); 
        _firebomb = Managers.Resource.Instantiate("Skill/FireBomb");
        yield return new WaitForEndOfFrame();

        _firebomb = Managers.Resource.Instantiate("Skill/FireBomb");
        yield return new WaitForEndOfFrame();

        _firebomb = Managers.Resource.Instantiate("Skill/FireBomb");
        yield return new WaitForEndOfFrame();

        _firebomb = Managers.Resource.Instantiate("Skill/FireBomb");
        yield return new WaitForSeconds(4f);
        StartCoroutine(GenerateFirebomb());
    }

    IEnumerator GenerateReflectionWeapon(SkillType type)
    {
        int count = type == SkillType.Tree ? 1 : 3;
        for(int i =0; i < count; i++)
        {
            yield return new WaitForSeconds(1f);
            Managers.Resource.Instantiate($"Skill/{type}");
        }
    }
}
