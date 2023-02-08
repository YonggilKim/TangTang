using Data;
using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking.Types;
using static Util;

public class GameManager : MonoBehaviour
{
    Transform _root;

    //public SpawnData[] _spawnData;
    private PlayerController _player;
    public PlayerController Player
    {
        get
        {
            if(_player == null)
                return FindObjectOfType<PlayerController>();
            return _player;
        }
        set 
        {
            _player = value; 
        }
    }

    public float _spawnInterval = 0;
    public float _playTime = 0;
    public float _maxGameTime = 1 * 60f;
    public int _monsterlevel = 1;

    Monster _monster;
    public void Init()
    {
        if (_root == null)
        {
            GameObject root = GameObject.Find("@GameManager");
            if (root == null)
            {
                root = new GameObject { name = "@GameManager" };
                root.gameObject.AddComponent<GameManager>();   
                Object.DontDestroyOnLoad(root);
            }
        }
    }

    private void Update()
    {
        _spawnInterval += Time.deltaTime;
        _playTime += Time.deltaTime;

        _monsterlevel = Mathf.FloorToInt(_playTime / 10f);
        if (_monsterlevel < 1)
            _monsterlevel= 1;
        

        if (_playTime > _maxGameTime)
        {
            _playTime = _maxGameTime;
            //게임종료
            return;
        }
        Dictionary<int, Monster> dict = Managers.Data.MonsterDic;
        dict.TryGetValue(_monsterlevel,out _monster);
        if (_spawnInterval > _monster.spawnTime)
        //if (_spawnInterval > GetSpawnTime())
        {
            GenerateMonster(_monsterlevel);
            _spawnInterval = 0;
        }
    }

    public void GameStart(int mapLevel)
    {
        //1. 맵생성
        Managers.Map.LoadMap(mapLevel);
        //2. 플레이어 생성
        GameObject player = Managers.Resource.Instantiate("Creature/Player");
        player.name = "Player";
        Player = player.GetComponent<PlayerController>();

        // temp -> 몬스터 생성
        //TODO 몬스터를 오브젝트리스트에 담아서 한번에 다죽일 수 있게 만들자

        //Game UI TODO

    }

    public void GenerateMonster(int spawnlevel)
    {
        GameObject monster = Managers.Resource.Instantiate($"Creature/Monster_00{_monster.monsterLevel}");
        monster.GetOrAddComponent<MonsterController>();
        monster.tag = "Monster";
        monster.name = $"Monster_00{GetMonsterId()}";

        Vector2 randCirclePos = Util.RandomPointInAnnulus((Vector2)Player.transform.position);
        monster.transform.position = randCirclePos;
        monster.GetComponent<MonsterController>().Init(_monster);
    }

    int GetMonsterId()
    {
        if (_monsterlevel < 2)
            return 1;
        else
            return 2;
    }

    float GetSpawnTime()
    {
        if (_monsterlevel < 2)
            return 0.8f;
        if (_monsterlevel < 5)
            return 0.5f;
        else
            return 0.2f;
    }



}

//public class SpawnData
//{
//    public int level;
//    public float spawnTime;
//    public int spriteType;
//    public int health;
//    public float speed;
//}