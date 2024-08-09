using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스킬(버프) : 단순 스탯(능력치, 평판) 상승 
// 스킬(디버프) : 단순 스탯(능력치, 평판) 하락
// 스킬(상태이상) : 복합적, 치명적인 방해 { 속박(이동불가), 기절(조작불가), 암흑(데미지 제한), 둔화(이동속도 일정 비율(%) 감소), 혼란(게임 플레이 방해) }

public class Player_Skill : MonoBehaviour
{
    public static Dictionary<int, Skill> m_sDictionary_Skill_Apply;              // 플레이어에게 적용중인 스킬 딕셔너리
    public static Dictionary<int, float> m_sDictionary_Skill_Apply_DurationTime; // 플레이어에게 적용중인 스킬 지속시간 딕셔너리
    public static Dictionary<int, float> m_sDictionary_Skill_CoolTime;           // 플레이어에게 적용중인 스킬 쿨타임 딕셔너리

    public static Skill_Condition m_Skill_Condition; // 플레이어에게 적용중인 상태이상(속박, 기절, 암흑, 둔화, 혼란) 정보

    public void InitialSet()
    {
        m_sDictionary_Skill_Apply = new Dictionary<int, Skill>();
        m_sDictionary_Skill_Apply_DurationTime = new Dictionary<int, float>();
        m_sDictionary_Skill_CoolTime = new Dictionary<int, float>();

        m_Skill_Condition = new Skill_Condition();
    }

    // 스킬 적용 가능 여부 판단(스킬 쿨타임)
    // return true : 스킬 적용 가능 / return false : 스킬 적용 불가능
    public bool CheckCondition_ApplySkill(int ncode)
    {
        if (m_sDictionary_Skill_Apply.ContainsKey(ncode) == true)
        {
            if (m_sDictionary_Skill_CoolTime.ContainsKey(ncode) == true)
            {
                Debug.Log("스킬 쿨타임");
                return false;
            }
            else
                return true;
        }
        else
            return true;
    }

    // 스킬(버프ㆍ디버프, 상태이상) 관련 정보 저장 및 적용. 스킬(상태이상) { 속박, 기절, 암흑, 둔화,  혼란 }
    // return true : 스킬(상태이상) 적용 / return false : 스킬(상태이상) 미적용
    public bool ApplySkill(Skill skill) // skill : 플레이어에게 적용할 스킬(버프ㆍ디버프, 상태이상)
    {
        if (m_sDictionary_Skill_Apply.ContainsKey(skill.m_nSkillCode) == true) // 스킬이 이미 적용중인 경우(스킬 지속시간 초기화)
        {
            m_sDictionary_Skill_Apply_DurationTime[skill.m_nSkillCode] = skill.m_fSkill_DurationTime; // 플레이어에게 적용중인 스킬 지속시간 딕셔너리에 해당 스킬 지속시간 초기화(재설정)
        }
        else
        {
            m_sDictionary_Skill_Apply.Add(skill.m_nSkillCode, skill); // 플레이어에게 적용중인 스킬 딕셔너리에 해당 스킬 추가
            m_sDictionary_Skill_Apply_DurationTime.Add(skill.m_nSkillCode, skill.m_fSkill_DurationTime); // 플레이어에게 적용중인 스킬 지속시간 딕셔너리에 해당 스킬 지속시간 추가
        }

        m_sDictionary_Skill_CoolTime.Add(skill.m_nSkillCode, skill.m_fSkill_CoolTime); // 플레이어에게 적용중인 스킬 쿨타임 딕셔너리에 해당 스킬 쿨타임 추가

        bool Check = false; // 스킬(상태이상) 적용 여부

        // 스킬(상태이상) 적용
        if (skill.m_Skill_Condition.ConditionCheck_Bind() == true) // 스킬(상태이상[속박]) 적용
        {
            m_Skill_Condition.SetBindTime(skill.m_Skill_Condition.GetBindTime()); // 플레이어에게 적용할 스킬(상태이상[속박]) 지속시간 초기화(재설정)
            m_Skill_Condition.SetBindCondition(true); // 플레이어에게 적용중인 스킬(상태이상[속박]) 업데이트 : 스킬(상태이상[속박]) true
            Check = true;
        }
        if (skill.m_Skill_Condition.ConditionCheck_Shock() == true) // 스킬(상태이상[기절]) 적용
        {
            m_Skill_Condition.SetShockTime(skill.m_Skill_Condition.GetShockTime()); // 플레이어에게 적용할 스킬(상태이상[기절]) 지속시간 초기화(재설정)
            m_Skill_Condition.SetShockCondition(true); // 플레이어에게 적용중인 스킬(상태이상[기절]) 업데이트 : 스킬(상태이상[기절]) true
            Check = true;
        }
        if (skill.m_Skill_Condition.ConditionCheck_Dark() == true) // 스킬(상태이상[암흑]) 적용
        {
            // 이미 적용중인 스킬(상태이상[암흑])과 새로 적용할 스킬(상태이상[암흑])을 비교하여 더 강력한 스킬(상태이상[암흑]) 적용
            // 스킬(상태이상[암흑]) 비율이 높은 스킬(상태이상[암흑]) 우선 적용
            if (m_Skill_Condition.GetDarkRatio() == skill.m_Skill_Condition.GetDarkRatio()) // 이미 적용중인 스킬(상태이상[암흑]) 비율 == 새로 적용할 스킬(상태이상[암흑]) 비율 : 단순 스킬(상태이상[암흑]) 지속시간 초기화(재설정)
            {
                m_Skill_Condition.SetDarkTime(skill.m_Skill_Condition.GetDarkTime()); // 플레이어에게 적용할 스킬(상태이상[암흑]) 지속시간 초기화(재설정)
                m_Skill_Condition.SetDarkCondition(true); // 플레이어에게 적용중인 스킬(상태이상[암흑]) 업데이트 : 스킬(상태이상[암흑]) true
                Check = true;
            }
            else if (m_Skill_Condition.GetDarkRatio() > skill.m_Skill_Condition.GetDarkRatio()) // 이미 적용중인 스킬(상태이상[암흑]) 비율 > 새로 적용할 스킬(상태이상[암흑]) 비율 : 새로운 스킬(상태이상[암흑]) 미적용
            {
                Check = false;
            }
            else // 이미 적용중인 스킬(상태이상[암흑]) < 새로 적용할 스킬(상태이상[암흑]) : 새로운 스킬(상태이상[암흑]) 적용
            {
                m_Skill_Condition.SetDarkTime(skill.m_Skill_Condition.GetDarkTime()); // 플레이어에게 적용할 스킬(상태이상[암흑]) 지속시간 초기화(재설정)
                m_Skill_Condition.SetDarkCondition(true); // 플레이어에게 적용중인 스킬(상태이상[암흑]) 업데이트 : 상태이상[암흑] true
                m_Skill_Condition.SetDarkRatio(skill.m_Skill_Condition.GetDarkRatio()); // 플레이어에게 적용할 스킬(상태이상[암흑]) 비율 재설정
                Check = true;
            }
        }
        if (skill.m_Skill_Condition.ConditionCheck_Slow() == true) // 스킬(상태이상[둔화]) 적용
        {
            // 이미 적용중인 스킬(상태이상[둔화])과 새로 적용할 스킬(상태이상[둔화])을 비교하여 더 강력한 스킬(상태이상[둔화]) 적용
            // 스킬(상태이상[둔화]) 비율이 높은 스킬(상태이상[둔화]) 우선 적용
            if (m_Skill_Condition.GetSlowRatio() == skill.m_Skill_Condition.GetSlowRatio()) // 이미 적용중인 스킬(상태이상[둔화]) 비율 == 새로 적용할 스킬(상태이상[둔화]) 비율 : 스킬(상태이상[둔화]) 지속시간 초기화(재설정)
            {
                m_Skill_Condition.SetSlowTime(skill.m_Skill_Condition.GetSlowTime()); // 플레이어에게 적용할 스킬(상태이상[둔화]) 지속시간 초기화(재설정)
                m_Skill_Condition.SetSlowCondition(true); // 플레이어에게 적용중인 스킬(상태이상[둔화]) 업데이트 : 스킬(상태이상[둔화]) true
                Check = true;
            }
            else if (m_Skill_Condition.GetSlowRatio() > skill.m_Skill_Condition.GetSlowRatio()) // 이미 적용중인 스킬(상태이상[둔화]) > 새로 적용할 스킬(상태이상[둔화]) : 새로운 스킬(상태이상[둔화]) 미적용
            {
                Check = false;
            }
            else // 이미 적용중인 스킬(상태이상[둔화]) < 새로 적용할 스킬(상태이상[둔화]) : 새로운 스킬(상태이상[둔화]) 적용
            {
                m_Skill_Condition.SetSlowTime(skill.m_Skill_Condition.GetSlowTime()); // 플레이어에게 적용할 스킬(상태이상[둔화]) 지속시간 초기화(재설정)
                m_Skill_Condition.SetSlowCondition(true); // 플레이어에게 적용중인 스킬(상태이상[둔화]) 업데이트 : 스킬(상태이상[둔화]) true
                m_Skill_Condition.SetSlowRatio(skill.m_Skill_Condition.GetSlowRatio()); // 플레이어에게 적용할 스킬(상태이상[둔화]) 비율 재설정
                Check = true;
            }
        }
        if (skill.m_Skill_Condition.ConditionCheck_Confuse() == true) // 스킬(상태이상[혼란]) 적용
        {
            m_Skill_Condition.SetConfuseTime(skill.m_Skill_Condition.GetConfuseTime()); // 플레이어에게 적용할 스킬(상태이상[혼란]) 지속시간 초기화(재설정)
            m_Skill_Condition.SetConfuseCondition(true); // 플레이어에게 적용중인 스킬(상태이상[혼란]) 업데이트 : 스킬(상태이상[혼란]) true
            Check = true;
        }

        if (Check == true)
            return true;
        else
            return false;
    }

    // 적용중인 스킬() 해제
    public void UnApplySkill(int ncode) // ncode : 해제할 스킬 코드
    {
        m_sDictionary_Skill_Apply.Remove(ncode); // 플레이어에게 적용중인 스킬 딕셔너리에서 해당 스킬 제거
                                                 // 스킬(상태이상)은 해당 함수에서 해제 불가능
    }
    // 스킬(상태이상) 해제
    public void UnApplySkill_Condition_Effect(int ncode) // ncode : 해제할 스킬(상태이상)코드
    {
        if (ncode / 1000 == 10)
            m_Skill_Condition.SetBindCondition(false);    // 플레이어에게 적용중인 스킬(상태이상[속박]) 업데이트 : 스킬(상태이상[속박]) false
        if (ncode / 1000 == 11)
            m_Skill_Condition.SetShockCondition(false);   // 플레이어에게 적용중인 스킬(상태이상[기절]) 업데이트 : 스킬(상태이상[기절]) false
        if (ncode / 1000 == 12)
            m_Skill_Condition.SetDarkCondition(false);    // 플레이어에게 적용중인 스킬(상태이상[암흑]) 업데이트 : 스킬(상태이상[암흑]) false
        if (ncode / 1000 == 13)
            m_Skill_Condition.SetSlowCondition(false);    // 플레이어에게 적용중인 스킬(상태이상[둔화]) 업데이트 : 스킬(상태이상[둔화]) false
        if (ncode / 1000 == 14)
            m_Skill_Condition.SetConfuseCondition(false); // 플레이어에게 적용중인 스킬(상태이상[혼란]) 업데이트 : 스킬(상태이상[혼란]) false
    }
    public void UnApplySkill_Condition_Effect()
    {
        if (m_Skill_Condition.ConditionCheck_Bind() == false)
            m_Skill_Condition.SetBindCondition(false);    // 플레이어에게 적용중인 스킬(상태이상[속박]) 업데이트 : 스킬(상태이상[속박]) false
        if (m_Skill_Condition.ConditionCheck_Shock() == false)
            m_Skill_Condition.SetShockCondition(false);   // 플레이어에게 적용중인 스킬(상태이상[기절]) 업데이트 : 스킬(상태이상[기절]) false
        if (m_Skill_Condition.ConditionCheck_Dark() == false)
            m_Skill_Condition.SetDarkCondition(false);    // 플레이어에게 적용중인 스킬(상태이상[암흑]) 업데이트 : 스킬(상태이상[암흑]) false
        if (m_Skill_Condition.ConditionCheck_Slow() == false)
            m_Skill_Condition.SetSlowCondition(false);    // 플레이어에게 적용중인 스킬(상태이상[둔화]) 업데이트 : 스킬(상태이상[둔화]) false
        if (m_Skill_Condition.ConditionCheck_Confuse() == false)
            m_Skill_Condition.SetConfuseCondition(false); // 플레이어에게 적용중인 스킬(상태이상[혼란]) 업데이트 : 스킬(상태이상[혼란]) false
    }
    // 리트라이(부활) 관련 함수
    public void ReTry()
    {
        m_sDictionary_Skill_Apply.Clear(); // 플레이어에게 적용중인 스킬(버프ㆍ디버프) 해제(초기화)

        // 플레이어에게 적용중인 스킬(상태이상) 해제(초기화)
        m_Skill_Condition.Initializing();
        UnApplySkill_Condition_Effect();
    }
}
