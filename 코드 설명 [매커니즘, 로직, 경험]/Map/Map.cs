using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : ScriptableObject
{
    string m_sMapName;

    int m_nMapCode;
    int m_nSceneCode;

    // 해당 맵에 위치한 오브젝트.
    public List <Monster_Total> m_List_Monster;
    public List <Tree> m_List_Tree;

    // 연결된 맵.
    List<Map> m_List_LinkedMap;

    public Map(string name, int mapcode, int scenecode)
    {
        this.m_sMapName = name;
        this.m_nMapCode = mapcode;
        this.m_nSceneCode = scenecode;

        m_List_Monster = new List<Monster_Total>();
        m_List_Tree = new List<Tree>();

        m_List_LinkedMap = new List<Map>();
    }

    public void AddLinkedMap(Map map)
    {
        m_List_LinkedMap.Add(map);
    }

    public string GetMapName()
    {
        return m_sMapName;
    }
    public int GetMapCode()
    {
        return m_nMapCode;
    }
    public int GetSceneCode()
    {
        return m_nSceneCode;
    }
}
