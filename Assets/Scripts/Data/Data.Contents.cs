using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
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