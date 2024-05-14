using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Skill : MonoBehaviour
{
    public List<Skill> m_List_ApplySkill;

    public void InitialSet()
    {
        m_List_ApplySkill = new List<Skill>();
    }
}
