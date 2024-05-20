using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Skill : MonoBehaviour
{
    public List<Skill> m_List_ApplySkill; // 플레이어가 적용중인 스킬 정보

    public void InitialSet()
    {
        m_List_ApplySkill = new List<Skill>();
    }
}
