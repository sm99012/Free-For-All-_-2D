using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Effect : MonoBehaviour
{
    public List<GameObject> m_l_EffectList; // 플레이어에게 적용 가능한 이펙트 리스트

    public static Dictionary<int, Skill_Effect> m_sDictionary_Skill_Effect_Apply; // 플레이어에게 적용중인 스킬 이펙트 딕셔너리

    GameObject m_gCondition; // 플레이어 스킬(상태이상) 오브젝트
    public GameObject m_gCondition_Bind;    // 속박 이펙트(애니메이션) 오브젝트
    public GameObject m_gCondition_Shock;   // 기절 이펙트(애니메이션) 오브젝트
    public GameObject m_gCondition_Dark;    // 암흑 이펙트(애니메이션) 오브젝트
    public GameObject m_gCondition_Slow;    // 둔화 이펙트(애니메이션) 오브젝트
    public GameObject m_gCondition_Confuse; // 혼란 이펙트(애니메이션) 오브젝트

    public void InitialSet()
    {
        m_l_EffectList = new List<GameObject>();
        GameObject Effect = Resources.Load("Prefab/Effect/Effect1") as GameObject;
        m_l_EffectList.Add(Effect);
        Effect = Resources.Load("Prefab/Effect/Effect_Yellow_1") as GameObject;
        m_l_EffectList.Add(Effect);
        Effect = Resources.Load("Prefab/Effect/Effect_Yellow_2") as GameObject;
        m_l_EffectList.Add(Effect);
        Effect = Resources.Load("Prefab/Effect/Effect_Yellow_3") as GameObject;
        m_l_EffectList.Add(Effect);
        Effect = Resources.Load("Prefab/Effect/Effect_Yellow_4") as GameObject;
        m_l_EffectList.Add(Effect);

        m_sDictionary_Skill_Effect_Apply = new Dictionary<int, Skill_Effect>();

        InitialSet_Condition();
    }
    public void InitialSet_Condition()
    {
        m_gCondition = transform.Find("Player_Condition").gameObject;
        m_gCondition_Bind = m_gCondition.transform.Find("Animation_Bind").gameObject;
        m_gCondition_Shock = m_gCondition.transform.Find("Animation_Shock").gameObject;
        m_gCondition_Dark = m_gCondition.transform.Find("Animation_Dark").gameObject;
        m_gCondition_Slow = m_gCondition.transform.Find("Animation_Slow").gameObject;
        m_gCondition_Confuse = m_gCondition.transform.Find("Animation_Confuse").gameObject;
    }

    // 이펙트 함수
    // 타격 이펙트 연출
    public void Effect1(Vector3 pos) // pos : 이펙트 위치
    {
        StartCoroutine(ProcessEffect1(pos));
    }
    IEnumerator ProcessEffect1(Vector3 pos) // pos : 이펙트 위치
    {
        yield return new WaitForSeconds(0.1f);
        if (m_l_EffectList[0] != null)
        {
            GameObject efc = Instantiate(m_l_EffectList[0]);
            efc.transform.position = pos;
        }
    }
    // 
    public void Effect2(Vector3 pos) // pos : 이펙트 위치
    {
        StartCoroutine(ProcessEffect2(pos));
    }
    IEnumerator ProcessEffect2(Vector3 pos) // pos : 이펙트 위치
    {
        yield return new WaitForSeconds(0.1f);
        if (m_l_EffectList[1] != null)
        {
            GameObject efc = Instantiate(m_l_EffectList[1]);
            efc.transform.position = pos;
        }
    }
    // 
    public void Effect3(Vector3 pos) // pos : 이펙트 위치
    {
        StartCoroutine(ProcessEffect3(pos));
    }
    IEnumerator ProcessEffect3(Vector3 pos) // pos : 이펙트 위치
    {
        yield return new WaitForSeconds(0.1f);
        if (m_l_EffectList[1] != null)
        {
            GameObject efc = Instantiate(m_l_EffectList[1]);
            efc.transform.position = pos;
        }
    }
    // 
    public void Effect4(Vector3 pos) // pos : 이펙트 위치
    {
        StartCoroutine(ProcessEffect4(pos));
    }
    IEnumerator ProcessEffect4(Vector3 pos) // pos : 이펙트 위치
    {
        yield return new WaitForSeconds(0.1f);
        if (m_l_EffectList[3] != null)
        {
            GameObject efc = Instantiate(m_l_EffectList[3]);
            efc.transform.position = pos;
        }
    }
    // 
    public void Effect5(Vector3 pos) // pos : 이펙트 위치
    {
        StartCoroutine(ProcessEffect5(pos));
    }
    IEnumerator ProcessEffect5(Vector3 pos) // pos : 이펙트 위치
    {
        yield return new WaitForSeconds(0.1f);
        if (m_l_EffectList[4] != null)
        {
            GameObject efc = Instantiate(m_l_EffectList[4]);
            efc.transform.position = pos;
        }
    }

    // 스킬(버프ㆍ디버프, 상태이상) 적용 시 이펙트 연출
    // return true : 이펙트 연출 / return false : 이펙트 미연출
    public bool ApplySkill(int ncode, Skill_Effect ske) // ncode : 적용할 스킬코드, skse : 스킬 이펙트
    {
        if (m_sDictionary_Skill_Effect_Apply.ContainsKey(ncode) == false)
            m_sDictionary_Skill_Effect_Apply.Add(ncode, ske); // 플레이어에게 적용중인 스킬 이펙트 딕셔너리에 해당 스킬 이펙트 추가

        ApplySkill_Condition_Effect(); // 스킬(상태이상) 적용 시 이펙트 연출

        if (ske.m_gSkill_Effect != null)
            return true;
        else
            return false;
    }
    // 스킬(상태이상) 적용 시 이펙트 연출
    public void ApplySkill_Condition_Effect()
    {
        if (Player_Skill.m_Skill_Condition.ConditionCheck_Bind() == true)
            m_gCondition_Bind.SetActive(true);    // 상태이상(속박) 이펙트 활성화
        if (Player_Skill.m_Skill_Condition.ConditionCheck_Shock() == true)
            m_gCondition_Shock.SetActive(true);   // 상태이상(기절) 이펙트 활성화
        if (Player_Skill.m_Skill_Condition.ConditionCheck_Dark() == true)
            m_gCondition_Dark.SetActive(true);    // 상태이상(암흑) 이펙트 활성화
        if (Player_Skill.m_Skill_Condition.ConditionCheck_Slow() == true)
            m_gCondition_Slow.SetActive(true);    // 상태이상(둔화) 이펙트 활성화
        if (Player_Skill.m_Skill_Condition.ConditionCheck_Confuse() == true)
            m_gCondition_Confuse.SetActive(true); // 상태이상(혼란) 이펙트 활성화
    }

    // 스킬(버프ㆍ디버프) 해제 시 이펙트 제거
    public void UnApplySkill(int ncode) // ncode : 해제할 스킬(버프ㆍ디버프)코드
    {
        m_sDictionary_Skill_Effect_Apply.Remove(ncode);
    }
    // 스킬(상태이상) 해제 시 이펙트 비활성화
    public void UnApplySkill_Condition_Effect(int ncode) // ncode : 해제할 스킬(상태이상)코드
    {
        if (ncode / 1000 == 10)
            m_gCondition_Bind.SetActive(false);    // 스킬(상태이상[속박]) 이펙트 비활성화
        if (ncode / 1000 == 11)
            m_gCondition_Shock.SetActive(false);   // 스킬(상태이상[기절]) 이펙트 비활성화
        if (ncode / 1000 == 12)
            m_gCondition_Dark.SetActive(false);    // 스킬(상태이상[암흑]) 이펙트 비활성화
        if (ncode / 1000 == 13)
            m_gCondition_Slow.SetActive(false);    // 스킬(상태이상[둔화]) 이펙트 비활성화
        if (ncode / 1000 == 14)
            m_gCondition_Confuse.SetActive(false); // 스킬(상태이상[혼란]) 이펙트 비활성화
    }

    // 리트라이(부활) 시 플레이어에게 적용중인 스킬(상태이상) 이펙트 초기화
    public void ReTry()
    {
        m_gCondition_Bind.SetActive(false);    // 스킬(상태이상[속박]) 이펙트 비활성화
        m_gCondition_Shock.SetActive(false);   // 스킬(상태이상[기절]) 이펙트 비활성화
        m_gCondition_Dark.SetActive(false);    // 스킬(상태이상[암흑]) 이펙트 비활성화
        m_gCondition_Slow.SetActive(false);    // 스킬(상태이상[둔화] 이펙트 비활성화
        m_gCondition_Confuse.SetActive(false); // 스킬(상태이상[혼란]) 이펙트 비활성화
    }
}
