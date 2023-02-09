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

    public Action OnPlayerDataUpdated;

    public void Init()
    {
        Debug.Log("@>> GameManager Init()");
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

    public void AddSkill(SkillType type)
    {
        Player.AddSkill(type);
    }

    void GeneratePlayer()
    {
        GameObject p = Managers.Resource.Instantiate("Creature/Player");
        p.name = "Player";
        Player = p.GetComponent<PlayerController>();
    }

    public void GenerateMonster()
    {
        Managers.Data.MonsterDic.TryGetValue(PlayerLevel, out _monster);
        GameObject monster = Managers.Resource.Instantiate($"Creature/Monster_00{_monster.spriteType}");
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
}
