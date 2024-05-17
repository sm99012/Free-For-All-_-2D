using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeSlime_Skill : MonoBehaviour
{
    public Dictionary<string, Skill> m_Dictionary_Skill;

    private void Start()
    {
        //InitialSet();
    }

    public void InitialSet()
    {
        m_Dictionary_Skill = new Dictionary<string, Skill>();

        m_Dictionary_Skill.Add(SkillManager.Instance.m_Dictionary_Skill["TeSlime_Slow1"].m_sSkillName, SkillManager.Instance.m_Dictionary_Skill["TeSlime_Slow1"]);
    }
}
