using Data;
using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Networking.Types;
using static Define;
using static UnityEngine.UI.Image;
using static Util;

public class GameManager
{
    Transform _monsterroot;
    public Transform Exproot;
    #region Player
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
    private int _playerLevel = 1;
    public int PlayerLevel
    {
        get { return _playerLevel; }
        set
        {
            _playerLevel = value;
        }
    }
    private int _playerExp = 0;
    public int PlayerExp
    {
        get { return _playerExp; }
        set
        {
            _playerExp = value;
            // 레벨업 체크
            int level = PlayerLevel;
            while (true)
            {
                Player nextPlayer;
                if (Managers.Data.PlayerDic.TryGetValue(level + 1, out nextPlayer) == false) 
                    break;
                Player player;
                Managers.Data.PlayerDic.TryGetValue(level, out player);
                if (_playerExp < player.totalExp) // _exp가 현재 레벨에서의 totalExp 보다 적으면 이제 레벨 증가 필요 없으므로 그만!
                    break;
                level++;
            }

            if (level != PlayerLevel)
            {
                Debug.Log("Level Up!");
                PlayerLevel = level;
                SetPlayerStat(PlayerLevel);
            }
            RefreshPlayerData();
        }
    }
    public int PlayerTotalExp { get; set; }
    public int PlayerHp { get; set; }
    public int PlayerMaxHp { get; set; }
    public SkillController Skill { get; set; }
    public List<SkillType> CurrentSkills = new List<SkillType>(); // 가지고 있는 스킬
    public bool CanUpgradeSkill // 스킬이 업글가능
    {
        get 
        {
            if (CurrentSkills.Count == (int)SkillType.Count) return false;
            else return true;
        }
        set { } 
    }

    #endregion
    #region Monster
    Monster _monsterData;
    private int _numDeadMonsters = 0;
    public int NumDeadMonsters 
    {
        get { return _numDeadMonsters; }
        set { _numDeadMonsters = value; RefreshMonsterData();} 
    }
    #endregion
    #region Time
    public float SpawnInterval{get; set;}
    public float PlayTime { get; set; }
    public float MaxGameTime { get; set; } = 10 * 60f;
    #endregion
    #region Action
    public Action OnPlayerDataUpdated;
    public Action OnMonsterDataUpdated;
    public Action OnPlayerLevelUp;
    #endregion
    public void Init()
    {
        Debug.Log("@>> GameManager Init()");

        _monsterroot = new GameObject().transform;
        _monsterroot.name = $"Monster_Root";

        Exproot = new GameObject().transform;
        Exproot.name = $"ExpRoot";

        Dictionary<int, Monster> dict = Managers.Data.MonsterDic;
        dict.TryGetValue(PlayerLevel, out _monsterData);
        CanUpgradeSkill = true;
    }

    public void GameStart()
    {
        //1. 맵생성
        Managers.Map.LoadMap();
        //2. 플레이어 생성
        GeneratePlayer();
        SetPlayerStat();
        CurrentSkills.Clear();
        // temp -> 몬스터 생성
        //TODO 몬스터를 오브젝트리스트에 담아서 한번에 다죽일 수 있게 만들자

        //Game UI TODO
    }

    public void SetPlayerStat(int level = 1)
    {
        
        Dictionary<int, Data.Player> dict = Managers.Data.PlayerDic;
        Data.Player player = dict[level]; 

        PlayerLevel = player.level;
        PlayerMaxHp = player.maxHp;
        PlayerTotalExp = player.totalExp;
        PlayerHp = player.maxHp;

        if(PlayerLevel >1)
            LevelUp();

    }
    // TODO 플레이어컨트롤에서 스킬을 추가하는게 맞나 gm에서하는게 맞나
    public void AddSkill(SkillType type)
    {
        CurrentSkills.Add(type);
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
        Skill = Player.GetComponentInChildren<SkillController>();
    }

    public void GenerateMonster()
    {
        Managers.Data.MonsterDic.TryGetValue(PlayerLevel, out _monsterData);
        GameObject monster = Managers.Resource.Instantiate($"Creature/Monster_00{_monsterData.spriteType}",_monsterroot);
        monster.GetOrAddComponent<MonsterController>();
        monster.tag = "Monster";
        monster.name = $"Monster_00{_monsterData.spriteType}";

        Vector2 randCirclePos = Util.RandomPointInAnnulus((Vector2)Player.transform.position);
        monster.transform.position = randCirclePos;
        monster.GetComponent<MonsterController>().Init(_monsterData);
    }

    public float GetMonsterSpawnInterval()
    {
        Dictionary<int, Data.Player> dict = Managers.Data.PlayerDic;
        Data.Player player = dict[PlayerLevel];

        if (player == null)
            return -1;
        return player.monsterSpawnTime;
    }

    public void RefreshPlayerData()
    {
        OnPlayerDataUpdated?.Invoke();
    }
   
    public void LevelUp()
    {
       OnPlayerLevelUp?.Invoke();
       Skill.OnLevelUP();
    }
    
    public void RefreshMonsterData()
    {
        OnMonsterDataUpdated?.Invoke();
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
    
    //TODO Json데이타로 받기
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

    public List<SkillType> GetCanUpgradeSkills()
    {
        List<SkillType> res = new List<SkillType>();
        List<SkillType> temp = new List<SkillType>();

        int canSkillCount = (int)SkillType.Count - CurrentSkills.Count;
        if (canSkillCount > 3)
            canSkillCount = 3;

        temp.Clear();
        temp =  CurrentSkills.ToList();
        while (true)
        {
            SkillType ranType = RandomEnum<SkillType>();
            if (ranType == SkillType.Count) continue;
            bool isFInd = false;
            for(int i = 0; i < temp.Count; i++)
            {
                if(ranType == temp[i])
                    isFInd = true;
            }
            for (int i = 0; i < res.Count; i++)
            {
                if (ranType == res[i])
                    isFInd = true;
            }
            if (isFInd == false && res.Count <= canSkillCount)
            { 
                res.Add(ranType);
            }

            if (res.Count == canSkillCount)
                break;
        }
        return res;
    }
    
}
