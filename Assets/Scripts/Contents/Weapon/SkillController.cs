using Data;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static Define;

//TODO �������� �ʿ� ��������� ã��
public class SkillController : MonoBehaviour
{
    protected int Level;
    protected float CommAttackSpeed = 1;
    protected int CommPenCount = 3;//���밡��
    protected int SpinDuration = 4;// �����ð�
    protected int SpinDelay = 10;// �����ð�
    protected int BallCount = 1;//
    protected float DroneAttackSpeed = 6;
    protected int FirebombCount = 1;
    protected int TreeCount = 1;

    GameManager _game;
    #region ȭ����
    private GameObject _firebomb;
    private GameObject _player;
    private GameObject _target;
    #endregion

    public void Awake()
    {
        _game = Managers.Game;
    }

    public void OnLevelUP()
    {
        SkillStat skill = new SkillStat();
        Managers.Data.SkillDic.TryGetValue(Managers.Game.PlayerLevel, out skill);

        Level = skill.level;
        CommAttackSpeed = skill.commAttackSpeed;
        CommPenCount = skill.commPenCount;
        SpinDuration = skill.spinDuration;
        SpinDelay = skill.spinDelay;
        BallCount = skill.ballCount;
        DroneAttackSpeed = skill.droneAttackSpeed;
        FirebombCount = skill.firebombCount;
        TreeCount = skill.treeCount;
    }

    //TODO DataManager�� ��ų�� ��������
    public void SetSpiner()
    {
        GameObject spinner = Managers.Resource.Instantiate("Skill/Spinner", gameObject.transform);
        spinner.GetComponent<Spinner>().Init();
    }

    public void SetCommendation()
    {
        GameObject launcher = Managers.Resource.Instantiate("Skill/CommendationLauncher", gameObject.transform);
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
        int count = FirebombCount;

        yield return new WaitForSeconds(2f);
        for (int i = 0; i < count; i++)
        {
            _firebomb = Managers.Resource.Instantiate("Skill/FireBomb");
            if (i == 0)
            {
                yield return new WaitForEndOfFrame();//�������� ����� �Ȱ�ģ�� ��?
            }
            yield return new WaitForEndOfFrame();
            _firebomb = Managers.Resource.Instantiate("Skill/FireBomb");

        }
        yield return new WaitForSeconds(4f);
        StartCoroutine(GenerateFirebomb());
    }

    IEnumerator GenerateReflectionWeapon(SkillType type)
    {
        int count = 1;

        if (type == SkillType.Ball)
        {
            count = BallCount;
        }
        else
        {
            count = TreeCount;
        }

        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(1f);
            Managers.Resource.Instantiate($"Skill/{type}");
        }
    }
}
