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
    public int Level;
    public float CommAttackSpeed = 1;
    public int CommPenCount = 3;//���밡��
    public int SpinDuration = 4;// �����ð�
    public int SpinDelay = 10;// �����ð�
    public int BallCount = 1;//
    public float DroneAttackSpeed = 6;
    public int FirebombCount = 1;
    public int TreeCount = 1;

    private int _prevBallCount = 0;
    private int _prevtreeCount = 0;
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
        DroneAttackSpeed = skill.droneAttackSpeed;
        FirebombCount = skill.firebombCount;

        _prevBallCount = BallCount;
        _prevtreeCount = TreeCount;
        BallCount = skill.ballCount;
        TreeCount = skill.treeCount;
       
        if(BallCount - _prevBallCount > 0 && IsHaveSkill(SkillType.Ball)) 
        { 
            Managers.Resource.Instantiate($"Skill/Ball");
        }
        if (TreeCount - _prevtreeCount > 0 && IsHaveSkill(SkillType.Tree))
        {
            Managers.Resource.Instantiate($"Skill/Tree");
        }

    }

    bool IsHaveSkill(SkillType type)
    {
        for (int i = 0; i < _game.CurrentSkills.Count; i++)
        {
            if (type == _game.CurrentSkills[i])
                return true;
        }
        return false;
    }

    public void SetSpiner()
    {
        GameObject spinner = Managers.Resource.Instantiate("Skill/Spinner", gameObject.transform);
        spinner.GetComponent<Spinner>().Init();
    }

    public void SetCommendation()
    {
        GameObject launcher = Managers.Resource.Instantiate("Skill/CommendationLauncher", gameObject.transform);
    }

    public void SetDrone()
    {
        GameObject dron = Managers.Resource.Instantiate("Skill/Drone", gameObject.transform);
    }
   
    public void SetFireBomb()
    {
        _player = _game.Player.gameObject;
        _target = _game.Player.FollowPoint;

        StartCoroutine(GenerateFirebomb());
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

    IEnumerator GenerateReflectionWeapon(SkillType type )
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
