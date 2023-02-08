using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager
{
    public void LoadMap(int mapLevel)
    {
        DestoryMap();

        string mapName = "Map_" + mapLevel.ToString("000"); //map_001
        GameObject go = Managers.Resource.Instantiate($"Map/{mapName}");
        go.name = "Map";
    }

    public void DestoryMap()
    {
        GameObject map = GameObject.Find("Map");
        if (map != null)
        {
            GameObject.Destroy(map);
        }
    }

}
