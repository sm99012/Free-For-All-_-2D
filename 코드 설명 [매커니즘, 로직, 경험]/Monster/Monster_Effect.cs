using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// ※ 몬스터의 이펙트를 관리하는 Monster_Effect 기반 클래스를 구현한 후 각종 몬스터의 ㆍㆍㆍ_Effect 클래스를 상속으로 구현했다.
//    가상 함수를 이용해 몬스터의 특징에 따른 적절한 동작을 구현했다.
//

public class Monster_Effect : MonoBehaviour
{
    public List<GameObject> m_l_EffectList; // 몬스터에게 적용 가능한 이펙트 리스트
    
    void Start()
    {
        m_l_EffectList = new List<GameObject>();

        GameObject Effect = Resources.Load("Effect/Effect_Yellow_3") as GameObject; // 몬스터 놓아주기 이펙트
        m_l_EffectList.Add(Effect);
    }

    // 몬스터 놓아주기 이펙트 연출 함수(가상 함수)
    virtual public void Effect_Goaway(Vector3 pos)
    {
        StartCoroutine(ProcessEffect_Goaway(pos));
    }
    // 몬스터 놓아주기 이펙트 연출 코루틴
    virtual protected IEnumerator ProcessEffect_Goaway(Vector3 pos)
    {
        yield return new WaitForSeconds(0.1f);
        if (m_l_EffectList[0] != null)
        {
            GameObject efc = Instantiate(m_l_EffectList[0]);
            efc.transform.position = pos;
        }
    }

    // 몬스터 ㆍㆍㆍ 이펙트 연출 함수(가상 함수)
    virtual public void Effect1(Vector3 pos)
    {

    }
     // 몬스터 ㆍㆍㆍ 이펙트 연출 코루틴
    virtual public void Effect1(Vector3 pos, int damage, string attackname)
    {

    }
}
