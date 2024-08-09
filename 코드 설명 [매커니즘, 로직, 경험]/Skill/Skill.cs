using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_SKILL_TYPE { PASSIVE, ACTIVE }
public enum E_SKILL_TYPE_BD { BUFF, DEBUFF, FUSION }
public enum E_SKILL_LEVEL { S1, S2, S3, S4, S5, S6, S8, S9, S10 }

public class Skill : MonoBehaviour
{
    public string m_sSkillName;
    public string m_sSkillDdscrption;

    public int m_nSkillCode;

    public E_SKILL_TYPE m_eSkillType;
    public E_SKILL_TYPE_BD m_eSkillType_BD;
    public E_SKILL_LEVEL m_eSkillLevel;

    // 제한사항
    public STATUS m_sStatus_Limit_Max;
    public STATUS m_sStatus_Limit_Min;
    public SOC m_sSoc_Limit_Max;
    public SOC m_sSoc_Limit_Min;

    // 상태이상
    public Skill_Condition m_Skill_Condition;
    // 스킬 이펙트
    public Skill_Effect m_Skill_Effect;
    // 스킬 효과
    public Skill_SSEffect m_Skill_SSEffect;

    // 스킬 지속시간
    public float m_fSkill_DurationTime = 0f;
    // 스킬 쿨타임
    public float m_fSkill_CoolTime = 0f;

    // 스킬 발동시 소모되는 자원.
    public STATUS m_sStatus_Consume;
    public SOC m_sSoc_Consume;

    // 상태이상 적용.
    public void ConditionApply_Bind(GameObject obj)
    {
        if (obj.layer == LayerMask.NameToLayer("Player"))
        {
            if (m_Skill_Condition.ConditionCheck_Bind() == true)
                Player_Skill.m_Skill_Condition.AddBindTime(m_Skill_Condition.GetBindTime());
        }
    }
    public void ConditionApply_Shock(GameObject obj)
    {
        if (obj.layer == LayerMask.NameToLayer("Player"))
        {
            if (m_Skill_Condition.ConditionCheck_Shock() == true)
                Player_Skill.m_Skill_Condition.AddShockTime(m_Skill_Condition.GetShockTime());
        }
    }
    public void ConditionApply_Dark(GameObject obj)
    {
        if (obj.layer == LayerMask.NameToLayer("Player"))
        {
            if (m_Skill_Condition.ConditionCheck_Dark() == true)
                Player_Skill.m_Skill_Condition.AddDarkTime(m_Skill_Condition.GetDarkTime());
        }
    }
    public void ConditionApply_Slow(GameObject obj)
    {
        if (obj.layer == LayerMask.NameToLayer("Player"))
        {
            if (m_Skill_Condition.ConditionCheck_Slow() == true)
                Player_Skill.m_Skill_Condition.AddSlowTime(m_Skill_Condition.GetSlowTime());
        }
    }
    public void ConditionApply_Confuse(GameObject obj)
    {
        if (obj.layer == LayerMask.NameToLayer("Player"))
        {
            if (m_Skill_Condition.ConditionCheck_Confuse() == true)
                Player_Skill.m_Skill_Condition.AddConfuseTime(m_Skill_Condition.GetConfuseTime());
        }
    }

    public Skill(string name, string des, int code, E_SKILL_TYPE st, E_SKILL_LEVEL sl, E_SKILL_TYPE_BD stb,
        STATUS stmax, STATUS stmin, SOC smax, SOC smin, 
        Skill_Condition skc, Skill_Effect ske, Skill_SSEffect skse,
        STATUS stc, SOC sc)
    {
        this.m_sSkillName = name;
        this.m_sSkillDdscrption = des;
        this.m_nSkillCode = code;
        this.m_eSkillType = st;
        this.m_eSkillLevel = sl;
        this.m_eSkillType_BD = stb;
        this.m_sStatus_Limit_Max = stmax;
        this.m_sStatus_Limit_Min = stmin;
        this.m_sSoc_Limit_Max = smax;
        this.m_sSoc_Limit_Min = smin;
        this.m_Skill_Condition = skc;
        this.m_Skill_Effect = ske;
        this.m_Skill_SSEffect = skse;
        this.m_sStatus_Consume = stc;
        this.m_sSoc_Consume = sc;
    }

    virtual public void Set_MOVE(Vector2 pos, float distance) { }

    virtual public bool UseSkill(GameObject mainobj)
    {
        // 스킬 사용 주체가 Player.
        if (mainobj.layer == LayerMask.NameToLayer("Player"))
        {

        }
        // 스킬 사용 주체가 Monster.
        else if (mainobj.layer == LayerMask.NameToLayer("Monster"))
        {
            // 스킬 사용 주체가 Monster_Boss.
            if (mainobj.tag == "Boss")
            {
                // 스킬 사용 주체가 Monster_Boss_TeSlime.
                if (mainobj.name == "TeSlime")
                {

                }
            }
        }
        return true;
    }

}
