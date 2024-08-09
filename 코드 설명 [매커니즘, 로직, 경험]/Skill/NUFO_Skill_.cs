using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Not Use For Object Skill_ㆍㆍㆍ

// 스킬 이펙트
public class Skill_Effect : MonoBehaviour
{
    public string m_sSkill_EffectName;

    public int m_nSkill_EffectCode;

    public GameObject m_gSkill_Effect;

    public Skill_Effect(string name, int code, GameObject gse = null)
    {
        m_sSkill_EffectName = name;
        m_nSkill_EffectCode = code;
        m_gSkill_Effect = gse;
    }

}

// 스킬 스탯(능력치, 평판) 효과
public class Skill_SSEffect
{
    // 일시적 효과
    public STATUS m_sStatus_Effect_Temporary;
    public SOC m_sSoc_Effect_Temporary;
    // 영구적 효과
    public STATUS m_sStatus_Effect_Eternal;
    public SOC m_sSoc_Effect_Eternal;

    public Skill_SSEffect(STATUS statuset, SOC socet, STATUS statusee, SOC socee)
    {
        m_sStatus_Effect_Temporary = statuset;
        m_sSoc_Effect_Temporary = socet;
        m_sStatus_Effect_Eternal = statusee;
        m_sSoc_Effect_Eternal = socee;
    }
}

// 스킬 상태이상
public class Skill_Condition
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

    public Skill_Condition()
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