using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_SKILL_EFFECT_LEVEL { S1, S2, S3, S4, S5 }

public class SkillEffect : MonoBehaviour
{
    public string m_sSkillEffectName;
    public string m_sSkillEffectDescription;

    public int m_nSkillEffectCode;

    public E_SKILL_EFFECT_LEVEL m_eSkillEffectLevel;

    // 일시적 효과
    public STATUS m_sStatus_Effect_Temporary;
    public SOC m_sSoc_Effect_Temporary;
    // 영구적 효과
    public STATUS m_sStatus_Effect_Eternal;
    public SOC m_sSoc_Effect_Eternal;

    // 상태
    public Condition m_cCondition;

    public SkillEffect(string name, string description, int code, E_SKILL_EFFECT_LEVEL sel, STATUS status_tem, STATUS status_ete, SOC soc_tem, SOC soc_ete,  Condition condition)
    {
        m_sSkillEffectName = name;
        m_sSkillEffectDescription = description;
        m_nSkillEffectCode = code;
        m_eSkillEffectLevel = sel;
        m_sStatus_Effect_Temporary = status_tem;
        m_sStatus_Effect_Eternal = status_ete;
        m_sSoc_Effect_Temporary = soc_tem;
        m_sSoc_Effect_Eternal = soc_ete;
        m_cCondition = condition;
    }

    public void SkillEffect_SetDescription(string description)
    {
        m_sSkillEffectDescription = description;
    }



    // 스킬 효과 적용.
    // 스탯, 평판 적용.
    public void StatusAndSocApply(GameObject obj)
    {

    }

    // 상태 적용.
    public void ConditionApply_Bind(GameObject obj)
    {
        if (obj.layer == LayerMask.NameToLayer("Player"))
        {
            if (m_cCondition.ConditionCheck_Bind() == true)
                Player_Status.m_cCondition.AddBindTime(m_cCondition.GetBindTime());
        }
    }
    public void ConditionApply_Shock(GameObject obj)
    {
        if (obj.layer == LayerMask.NameToLayer("Player"))
        {
            if (m_cCondition.ConditionCheck_Shock() == true)
                Player_Status.m_cCondition.AddShockTime(m_cCondition.GetShockTime());
        }
    }
    public void ConditionApply_Dark(GameObject obj)
    {
        if (obj.layer == LayerMask.NameToLayer("Player"))
        {
            if (m_cCondition.ConditionCheck_Dark() == true)
                Player_Status.m_cCondition.AddDarkTime(m_cCondition.GetDarkTime());
        }
    }
    public void ConditionApply_Slow(GameObject obj)
    {
        if (obj.layer == LayerMask.NameToLayer("Player"))
        {
            if (m_cCondition.ConditionCheck_Slow() == true)
                Player_Status.m_cCondition.AddSlowTime(m_cCondition.GetSlowTime());
        }
    }
    public void ConditionApply_Confuse(GameObject obj)
    {
        if (obj.layer == LayerMask.NameToLayer("Player"))
        {
            if (m_cCondition.ConditionCheck_Confuse() == true)
                Player_Status.m_cCondition.AddConfuseTime(m_cCondition.GetConfuseTime());
        }
    }
}

public class Condition
{
    // 속박
    bool m_bBind;
    float m_fBindTime;
    // 기절
    bool m_bShock;
    float m_fShockTime;
    // 암흑
    bool m_bDark;
    float m_fDarkTime;
    // 암흑 비율. 0 ~ 100 % 비율에 따라 데미지 감소.
    float m_fDarkRatio;
    // 둔화
    bool m_bSlow;
    float m_fSlowTime;
    // 둔화 비율. 0 ~ 100 % 비율에 따라 이동속도 감소.
    float m_fSlowRatio;
    // 혼란
    bool m_bConfuse;
    float m_fConfuseTime;

    public Condition()
    {
        m_bBind = false;
        m_fBindTime = 0;
        m_bShock = false;
        m_fShockTime = 0;
        m_bDark = false;
        m_fDarkTime = 0;
        m_fDarkRatio = 0;
        m_bSlow = false;
        m_fSlowTime = 0;
        m_fSlowRatio = 0;
        m_bConfuse = false;
        m_fConfuseTime = 0;
    }

    public void SetCondition(bool Bind = false, bool shock = false, bool dark = false, bool slow = false, bool confuse = false)
    {
        m_bBind = Bind;
        m_bShock = shock;
        m_bDark = dark;
        m_bSlow = slow;
        m_bConfuse = confuse;
    }

    public void SetConditionTime(float Bind = 0, float shock = 0, float dark = 0, float slow = 0, float confuse = 0)
    {
        if (m_bBind == true)
            m_fBindTime = Bind;
        if (m_bShock == true)
            m_fShockTime = shock;
        if (m_bDark == true)
            m_fDarkTime = dark;
        if (m_bSlow == true)
            m_fSlowTime = slow;
        if (m_bConfuse == true)
            m_fConfuseTime = confuse;
    }


    public void SetBindCondition(bool Bind)
    {
        m_bBind = Bind;
    }
    public void SetShockCondition(bool shock)
    {
        m_bShock = shock;
    }
    public void SetDarkCondition(bool dark)
    {
        m_bDark = dark;
    }
    public void SetSlowCondition(bool slow)
    {
        m_bSlow = slow;
    }
    public void SetConfuseCondition(bool confuse)
    {
        m_bConfuse = confuse;
    }

    public bool ConditionCheck_Bind()
    {
        if (m_bBind == false)
            return false;
        else
            return true;
    }
    public bool ConditionCheck_Shock()
    {
        if (m_bShock == false)
            return false;
        else
            return true;
    }
    public bool ConditionCheck_Dark()
    {
        if (m_bDark == false)
            return false;
        else
            return true;
    }
    public bool ConditionCheck_Slow()
    {
        if (m_bSlow == false)
            return false;
        else
            return true;
    }
    public bool ConditionCheck_Confuse()
    {
        if (m_bConfuse == false)
            return false;
        else
            return true;
    }


    public void AddBindTime(float time)
    {
        m_fBindTime += time;
    }
    public void AddShockTime(float time)
    {
        m_fShockTime += time;
    }
    public void AddDarkTime(float time)
    {
        m_fDarkTime += time;
    }
    public void AddSlowTime(float time)
    {
        m_fSlowTime += time;
    }
    public void AddConfuseTime(float time)
    {
        m_fConfuseTime += time;
    }

    public float GetBindTime()
    {
        return m_fBindTime;
    }
    public float GetShockTime()
    {
        return m_fShockTime;
    }
    public float GetDarkTime()
    {
        return m_fDarkTime;
    }
    public float GetSlowTime()
    {
        return m_fSlowTime;
    }
    public float GetConfuseTime()
    {
        return m_fConfuseTime;
    }

    public float GetDarkRatio()
    {
        return m_fDarkRatio;
    }
    public float GetSlowRatio()
    {
        return m_fSlowRatio;
    }

    public void SetBindTime(float time)
    {
        m_fBindTime = time;
    }
    public void SetShockTime(float time)
    {
        m_fShockTime = time;
    }
    public void SetDarkTime(float time)
    {
        m_fDarkTime = time;
    }
    public void SetSlowTime(float time)
    {
        m_fSlowTime = time;
    }
    public void SetConfuseTime(float time)
    {
        m_fConfuseTime = time;
    }

    public void SetDarkRatio(float ratio)
    {
        m_fDarkRatio = ratio;
    }
    public void SetSlowRatio(float ratio)
    {
        m_fSlowRatio = ratio;
    }

    public void Initializing()
    {
        m_bBind = false;
        m_fBindTime = 0;
        m_bShock = false;
        m_fShockTime = 0;
        m_bDark = false;
        m_fDarkTime = 0;
        m_fDarkRatio = 0;
        m_bSlow = false;
        m_fSlowTime = 0;
        m_fSlowRatio = 0;
        m_bConfuse = false;
        m_fConfuseTime = 0;
    }
}