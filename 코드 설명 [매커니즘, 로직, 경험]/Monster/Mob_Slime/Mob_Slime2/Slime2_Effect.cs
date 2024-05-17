using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime2_Effect : Monster_Effect
{
    void Start()
    {
        m_l_EffectList = new List<GameObject>();
        m_l_EffectList = new List<GameObject>();
        GameObject Effect = Resources.Load("Prefab/Effect/Effect_Yellow_3") as GameObject;
        m_l_EffectList.Add(Effect);
    }
}
