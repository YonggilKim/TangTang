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
    public int _spawnlevel = 1;

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

        _spawnlevel = Mathf.FloorToInt(_playTime / 10f);

        if (_playTime > _maxGameTime)
        {
            _playTime = _maxGameTime;
            //게임종료
            return;
        }

        if (_spawnInterval > GetSpawnTime())
        {
            GenerateMonster(_spawnlevel);
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
        GameObject monster = Managers.Resource.Instantiate($"Creature/Monster_00{GetMonsterId()}");
        monster.GetOrAddComponent<MonsterController>();
        monster.tag = "Monster";
        monster.name = $"Monster_00{GetMonsterId()}";

        Vector2 randCirclePos = Util.RandomPointInAnnulus((Vector2)Player.transform.position);
        monster.transform.position = randCirclePos;
    }

    int GetMonsterId()
    {
        if (_spawnlevel < 2)
            return 1;
        else
            return 2;
    }

    float GetSpawnTime()
    {
        if (_spawnlevel < 2)
            return 0.8f;
        if (_spawnlevel < 5)
            return 0.5f;
        else
            return 0.2f;
    }



}
