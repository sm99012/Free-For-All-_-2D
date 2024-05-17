using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_SKILL_TYPE { PASSIVE, ACTIVE }
public enum E_SKILL_LEVEL { S1, S2, S3, S4, S5, S6, S8, S9, S10 }

public class Skill : MonoBehaviour
{
    public string m_sSkillName;
    public string m_sSkillDdscrption;

    public int m_nSkillCode;

    public E_SKILL_TYPE m_eSkillType;
    public E_SKILL_LEVEL m_eSkillLevel;

    // 제한사항
    public STATUS m_sStatus_Limit_Max;
    public STATUS m_sStatus_Limit_Min;
    public SOC m_sSoc_Limit_Max;
    public SOC m_sSoc_Limit_Min;

    // 효과
    public SkillEffect m_seSkillEffect;

    // 스킬 발동시 소모되는 자원.
    public STATUS m_sStatus_Consume;
    public SOC m_sSoc_Consume;

    public Skill(string name, string des, int code, E_SKILL_TYPE st, E_SKILL_LEVEL sl,
        STATUS stmax, STATUS stmin, SOC smax, SOC smin, SkillEffect se, STATUS stc, SOC sc)
    {
        this.m_sSkillName = name;
        this.m_sSkillDdscrption = des;
        this.m_nSkillCode = code;
        this.m_eSkillType = st;
        this.m_eSkillLevel = sl;
        this.m_sStatus_Limit_Max = stmax;
        this.m_sStatus_Limit_Min = stmin;
        this.m_sSoc_Limit_Max = smax;
        this.m_sSoc_Limit_Min = smin;
        this.m_seSkillEffect = se;
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
