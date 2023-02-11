using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public Dictionary<int, Data.SkillStat> SkillDic { get; private set; } = new Dictionary<int, Data.SkillStat>();
    public Dictionary<int, Data.Player> PlayerDic { get; private set; } = new Dictionary<int, Data.Player>();
    public Dictionary<int, Data.Monster> MonsterDic { get; private set; } = new Dictionary<int, Data.Monster>();

    public void Init()
    {
        SkillDic = LoadJson<Data.SkillStatData, int, Data.SkillStat>("SkillData").MakeDict();
        PlayerDic = LoadJson<Data.PlayerData, int, Data.Player>("PlayerData").MakeDict();
        MonsterDic = LoadJson<Data.MonsterData, int, Data.Monster>("MonsterData").MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        Loader loader = JsonUtility.FromJson<Loader>(textAsset.text);
        return loader;
    }
}
