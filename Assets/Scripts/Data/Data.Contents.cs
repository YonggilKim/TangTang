using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    #region SkilStat
    [Serializable]
    public class SkillStat
    {
        public int level;
        public float commAttackSpeed;
        public int commPenCount;//관통가능
        public int spinDuration;// 유지시간
        public int spinDelay;// 유지시간
        public int ballCount;//
        public float droneAttackSpeed;
        public int firebombCount;
        public int treeCount;
    }
    [Serializable]
    public class SkillStatData : ILoader<int, SkillStat>
    {
        public List<SkillStat> stats = new List<SkillStat>();
        public Dictionary<int, SkillStat> MakeDict()
        {
            Dictionary<int, SkillStat> dict = new Dictionary<int, SkillStat>();
            foreach (SkillStat stat in stats)
                dict.Add(stat.level, stat);
            return dict;
        }
    }
    #endregion

    #region PlayerData
    [Serializable]
    public class Player
    {
        public int level;
        public int maxHp;
        public int attack;
        public int totalExp;
        public float monsterSpawnTime;
    }
    [Serializable]
    public class PlayerData : ILoader<int, Player>
    {
        public List<Player> stats = new List<Player>();
        public Dictionary<int, Player> MakeDict()
        {
            Dictionary<int, Player> dict = new Dictionary<int, Player>();
            foreach (Player stat in stats)
                dict.Add(stat.level, stat);
            return dict;
        }
    }
    #endregion

    #region SpawnData
    [Serializable]
    public class Monster
    {
        public int monsterLevel;
        public float spawnTime;
        public int spriteType;
        public int maxHealth;
        public int health;
        public float speed;
        public int attack;
        public int exp;
    }

    [Serializable]
    public class MonsterData : ILoader<int, Monster>
    {
        public List<Monster> stats = new List<Monster>();
        public Dictionary<int, Monster> MakeDict()
        {
            Dictionary<int, Monster> dict = new Dictionary<int, Monster>();
            foreach (Monster stat in stats)
                dict.Add(stat.monsterLevel, stat);
            return dict;
        }
    }
    #endregion
}