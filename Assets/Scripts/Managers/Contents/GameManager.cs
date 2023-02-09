using Data;
using DG.Tweening.Core.Easing;
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
    public float _playTime = 0;
    public float _maxGameTime = 1 * 60f;
    private int _monsterSpawnLevel = 1;
    public int MonsteerSpawnLevel
    {
        get 
        {
            return _monsterSpawnLevel;
        }
        set
        {
            if (value < 1)
                _monsterSpawnLevel = 1;
            if (value > 3)
                _monsterSpawnLevel = 3;

            _monsterSpawnLevel = value;
        }
    }

    public void Init()
    {
        Debug.Log("@>> GameManager Init()");
        _playTime = 0;
        _maxGameTime = 1 * 60f;
        Dictionary<int, Monster> dict = Managers.Data.MonsterDic;
        dict.TryGetValue(_monsterSpawnLevel, out _monster);
    }

    //�����ϴ� ���� ����
    public void SetMonsterLevel()
    {
        Dictionary<int, Monster> dict = Managers.Data.MonsterDic;
        dict.TryGetValue(_monsterSpawnLevel, out _monster);

        _monsterSpawnLevel = Mathf.FloorToInt(_playTime / 10f);
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
}
