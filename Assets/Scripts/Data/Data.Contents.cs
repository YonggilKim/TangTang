using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    #region Stat
    [Serializable]
    public class Stat
    {
        public int level;
        public int maxHp;
        public int attack;
        public int totalExp;
    }
    [Serializable]
    public class StatData : ILoader<int, Stat>
    {
        public List<Stat> stats = new List<Stat>();
        public Dictionary<int, Stat> MakeDict()
        {
            Dictionary<int, Stat> dict = new Dictionary<int, Stat>();
            foreach (Stat stat in stats)
                dict.Add(stat.level, stat);
            return dict;
        }
    }

    [Serializable]
    public class Stat2
    {
        public int level;
        public float maxHp;
        public int attack;
        public float totalExp;
    }
    [Serializable]
    public class StatData2 : ILoader<int, Stat2>
    {
        public List<Stat2> stats = new List<Stat2>();
        public Dictionary<int, Stat2> MakeDict()
        {
            Dictionary<int, Stat2> dict = new Dictionary<int, Stat2>();
            foreach (Stat2 stat in stats)
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