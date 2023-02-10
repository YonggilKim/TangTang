using Data;
using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking.Types;
using static Define;
using static UnityEngine.UI.Image;
using static Util;

public class GameManager
{
    Transform _root;

    private PlayerController _player;
    public PlayerController Player
    {
        get
        {
            return _player;
        }
        set
        {
            _player = value;
        }
    }
    public Skill Skill { get; set; }
    Monster _monster;
    //public float _playTime = 0;
    //public float _maxGameTime = 1 * 60f;
    public float SpawnInterval{get; set;}
    public float PlayTime { get; set; }
    public float MaxGameTime { get; set; } = 1 * 60f;

    private int _playerLevel = 0;
    public int PlayerLevel 
    {
        get { return _playerLevel; }
        set 
        {
            _playerLevel = value;
            RefreshPlayerData();
        }
    }

    private int _playerExp = 0;
    public int PlayerExp
    {
        get { return _playerExp; }
        set
        {
            _playerExp = value;
            RefreshPlayerData();
        }
    }

    public List<SkillType> currentSkills = new List<SkillType>();
    public Action OnPlayerDataUpdated;

    public void Init()
    {
        Debug.Log("@>> GameManager Init()");

        _root = new GameObject().transform;
        _root.name = $"Monster_Root";

        PlayerLevel = 1;
        Dictionary<int, Monster> dict = Managers.Data.MonsterDic;
        dict.TryGetValue(PlayerLevel, out _monster);
    }

    public void GameStart()
    {
        //1. �ʻ���
        Managers.Map.LoadMap();
        //2. �÷��̾� ����
        GeneratePlayer();

        // temp -> ���� ����
        //TODO ���͸� ������Ʈ����Ʈ�� ��Ƽ� �ѹ��� ������ �� �ְ� ������

        //Game UI TODO
    }

    // TODO �÷��̾���Ʈ�ѿ��� ��ų�� �߰��ϴ°� �³� gm�����ϴ°� �³�
    public void AddSkill(SkillType type)
    {
        currentSkills.Add(type);
        switch (type)
        {
            case SkillType.Spinner:
                Skill.SetSpiner();
                break;
            case SkillType.Commendation:
                Skill.SetCommendation();
                break;
            case SkillType.Firebomb:
                Skill.SetFireBomb();
                break;
            case SkillType.Ball:
                Skill.SetReflectionWeapon(type);
                break;
            case SkillType.Tree:
                Skill.SetReflectionWeapon(type);
                break;
            case SkillType.Drone:
                Skill.SetDrone();
                break;
            default:
                break;
        }
    }

    void GeneratePlayer()
    {
        GameObject p = Managers.Resource.Instantiate("Creature/Player");
        p.name = "Player";
        Player = p.GetComponent<PlayerController>();
        Skill = Player.GetComponentInChildren<Skill>();
    }

    public void GenerateMonster()
    {
        Managers.Data.MonsterDic.TryGetValue(PlayerLevel, out _monster);
        GameObject monster = Managers.Resource.Instantiate($"Creature/Monster_00{_monster.spriteType}",_root);
        monster.GetOrAddComponent<MonsterController>();
        monster.tag = "Monster";
        monster.name = $"Monster_00{_monster.spriteType}";

        Vector2 randCirclePos = Util.RandomPointInAnnulus((Vector2)Player.transform.position);
        monster.transform.position = randCirclePos;
        monster.GetComponent<MonsterController>().Init(_monster);
    }

    public float GetMonsterSpawnInterval()
    {
        if (_monster == null)
            return -1;
        return _monster.spawnTime;
    }

    public void RefreshPlayerData()
    {
        OnPlayerDataUpdated?.Invoke();
    }

    public void RefreshMonsterData()
    {
        OnPlayerDataUpdated?.Invoke();
    }

    public int GetNameTODamage(string objname)
    {
        int res = 0;

        if (objname.Contains("Commendation"))
            res = GetSkillDamage(SkillType.Commendation);
        else if (objname.Contains("FireEffect"))
            res = GetSkillDamage(SkillType.Firebomb);
        else if (objname.Contains("Spinner"))
            res = GetSkillDamage(SkillType.Spinner);
        else if (objname.Contains("Ball"))
            res = GetSkillDamage(SkillType.Ball);
        else if (objname.Contains("Tree"))
            res = GetSkillDamage(SkillType.Tree);
        else if (objname.Contains("RocketEffect"))
            res = GetSkillDamage(SkillType.Drone);
        return res;
    }

    //TODO Json����Ÿ�� �ޱ�
    public int GetSkillDamage(SkillType type, int playerLevel = 1)
    {
        int res = 0;
        switch (type)
        {
            case SkillType.Spinner:
                res =  50;
                break;
            case SkillType.Commendation:
                res =  100;
                break;
            case SkillType.Ball:
                res = 100;
                break;
            case SkillType.Firebomb:
                res =  30;
                break;
            case SkillType.Tree:
                res = 80;
                break;
            case SkillType.Drone:
                res = 100;
                break;
            default:
                res =  0;
                break;
        }
        return res;
    }

    public Vector3 GetPlayerPosition()
    {
        return Player.gameObject.transform.position;
    }
}
