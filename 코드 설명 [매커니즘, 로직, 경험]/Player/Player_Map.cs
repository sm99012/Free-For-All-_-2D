using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Map : MonoBehaviour
{
    // 현재 Player 가 위치하고 있는 맵의 정보.
    public Map m_Map;
    public string m_sMapName;

    public void InitialSet()
    {
        m_Map = MapManager.Instance.m_Dictionary_Map_Tutorial[0001];
        m_sMapName = "튜토리얼맵";
    }

    public void ChangeMap(Map map)
    {
        m_Map = map;
        m_sMapName = map.GetMapName();

        //Debug.Log("Player 맵 이동: " + m_sMapName);
    }
}
