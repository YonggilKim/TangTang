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
    //public Dictionary<int, Data.Stat> StatDict { get; private set; } = new Dictionary<int, Data.Stat>();
    //public Dictionary<int, Data.Stat2> StatDict2 { get; private set; } = new Dictionary<int, Data.Stat2>();
    public Dictionary<int, Data.Monster> MonsterDic { get; private set; } = new Dictionary<int, Data.Monster>();

    public void Init()
    {
        //StatDict = LoadJson<Data.StatData, int, Data.Stat>("StatData").MakeDict();
        //StatDict2 = LoadJson<Data.StatData2, int, Data.Stat2>("StatData").MakeDict();
        MonsterDic = LoadJson<Data.MonsterData, int, Data.Monster>("MonsterData").MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        Loader loader = JsonUtility.FromJson<Loader>(textAsset.text);
        return loader;
    }
}
