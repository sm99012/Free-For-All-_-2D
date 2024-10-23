using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ents1_Effect : Monster_Effect // 기반이 되는 Monster_Effect 클래스 상속
{
    void Start()
    {
        m_l_EffectList = new List<GameObject>();
        
        GameObject Effect = Resources.Load("Prefab/Effect/Effect_Yellow_3") as GameObject; // 몬스터 놓아주기 이펙트
        m_l_EffectList.Add(Effect);
        
        Effect = Resources.Load("Prefab/Effect/Monster_Effect_Ents1") as GameObject; // 몬스터 공격 이펙트
        m_l_EffectList.Add(Effect);
    }

    override public void Effect_Goaway(Vector3 pos)
    {
        StartCoroutine(ProcessEffect_Goaway(pos));
    }

    override public void Effect1(Vector3 pos, int damage, string attackname)
    {
        StartCoroutine(ProcessEffect1(pos, damage, attackname));
    }

    IEnumerator ProcessEffect1(Vector3 pos, int damage, string attackname)
    {
        yield return new WaitForSeconds(0.1f);
        if (m_l_EffectList[1] != null)
        {
            GameObject efc = Instantiate(m_l_EffectList[1]);
            efc.transform.position = pos;
            efc.GetComponent<Mob_Effect_Ent1>().InitialSet(pos, damage, attackname);
        }
    }
}
