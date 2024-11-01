using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private static SkillManager instance = null;
    public static SkillManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public Dictionary<string, Skill> m_Dictionary_Skill;

    public void InitialSet()
    {
        m_Dictionary_Skill = new Dictionary<string, Skill>();

        Skill sk;
        Skill_Condition co;
        Skill_Effect se;
        Skill_SSEffect sse;

        // 패시브 스킬_디버프(상태 이상, 스탯/평판 감소).
        co = new Skill_Condition();
        co.SetCondition(true);
        co.SetConditionTime(3);
        se = new Skill_Effect("Lv1 Bind", 10001);
        sse = new Skill_SSEffect(new STATUS(), new SOC(), new STATUS(), new SOC());
        sk = new Skill("Skill Lv1 Bind", "Lv1 상태이상 스킬[속박]", 10001, E_SKILL_TYPE.ACTIVE, E_SKILL_LEVEL.S1, E_SKILL_TYPE_BD.DEBUFF,
            new STATUS(true), new STATUS(false), new SOC(true), new SOC(false),
            co, se, sse,
            new STATUS(), new SOC());
        m_Dictionary_Skill.Add(sk.m_sSkillName, sk);

        co = new Skill_Condition();
        co.SetCondition(false, true);
        co.SetConditionTime(0, 3);
        se = new Skill_Effect("Lv1 Shock", 11001);
        sse = new Skill_SSEffect(new STATUS(), new SOC(), new STATUS(), new SOC());
        sk = new Skill("Skill Lv1 Shock", "Lv1 상태이상 스킬[기절]", 11001, E_SKILL_TYPE.ACTIVE, E_SKILL_LEVEL.S2, E_SKILL_TYPE_BD.DEBUFF,
            new STATUS(true), new STATUS(false), new SOC(true), new SOC(false),
            co, se, sse,
            new STATUS(), new SOC());
        m_Dictionary_Skill.Add(sk.m_sSkillName, sk);

        co = new Skill_Condition();
        co.SetCondition(false, false, true);
        co.SetConditionTime(0, 0, 3);
        co.SetDarkRatio(30);
        se = new Skill_Effect("Lv1 Dark", 12001);
        sse = new Skill_SSEffect(new STATUS(), new SOC(), new STATUS(), new SOC());
        sk = new Skill("Skill Lv1 Dark", "Lv1 상태이상 스킬[암흑]", 12001, E_SKILL_TYPE.ACTIVE, E_SKILL_LEVEL.S1, E_SKILL_TYPE_BD.DEBUFF,
            new STATUS(true), new STATUS(false), new SOC(true), new SOC(false),
            co, se, sse,
            new STATUS(), new SOC());
        m_Dictionary_Skill.Add(sk.m_sSkillName, sk);

        co = new Skill_Condition();
        co.SetCondition(false, false, false, true);
        co.SetConditionTime(0, 0, 0, 3);
        co.SetSlowRatio(30);
        se = new Skill_Effect("Lv1 Slow", 13001);
        sse = new Skill_SSEffect(new STATUS(), new SOC(), new STATUS(), new SOC());
        sk = new Skill("Skill Lv1 Slow", "Lv1 상태이상 스킬[둔화]", 13001, E_SKILL_TYPE.ACTIVE, E_SKILL_LEVEL.S1, E_SKILL_TYPE_BD.DEBUFF,
            new STATUS(true), new STATUS(false), new SOC(true), new SOC(false),
            co, se, sse,
            new STATUS(), new SOC());
        m_Dictionary_Skill.Add(sk.m_sSkillName, sk);

        //co = new Skill_Condition();
        //co.SetCondition(false, true, false, true);
        //co.SetConditionTime(0, 0, 0, 30);
        //co.SetSlowRatio(30);
        //se = new Skill_Effect("Test", 15000);
        //sse = new Skill_SSEffect(new STATUS(), new SOC(), new STATUS(), new SOC());
        //sk = new Skill("Skill Test", "Lv1 Test", 13001, E_SKILL_TYPE.ACTIVE, E_SKILL_LEVEL.S1, E_SKILL_TYPE_BD.DEBUFF,
        //    new STATUS(true), new STATUS(false), new SOC(true), new SOC(false),
        //    co, se, sse,
        //    new STATUS(), new SOC());
        //sk.m_Skill_SSEffect.m_sStatus_Effect_Temporary.SetSTATUS_HP_Max(-5);
        //sk.m_fSkill_DurationTime = 10;
        //sk.m_fSkill_CoolTime = 0;
        //sk.m_sStatus_Consume.SetSTATUS_HP_Current(1);
        //m_Dictionary_Skill.Add(sk.m_sSkillName, sk);

        //co = new Condition();
        //co.SetCondition(false, false, true);
        //co.SetConditionTime(0, 0, 10);
        //co.SetDarkRatio(50);
        //se = new SkillEffect("Dark2", "Lv2 암흑 효과.", 12002, E_SKILL_EFFECT_LEVEL.S1, new STATUS(), new STATUS(), new SOC(), new SOC(), co);
        //m_Dictionary_SkillEffect.Add(se.m_sSkillEffectName, se);
        //sk = new Skill("Skill_Dark2", "Lv2 암흑 스킬", 12002, E_SKILL_TYPE.ACTIVE, E_SKILL_LEVEL.S2,
        //    new STATUS(), new STATUS(), new SOC(), new SOC(), se, new STATUS(), new SOC());
        //m_Dictionary_Skill.Add(sk.m_sSkillName, sk);

        //co = new Condition();
        //co.SetCondition(false, false, true);
        //co.SetConditionTime(0, 0, 10);
        //co.SetDarkRatio(100);
        //se = new SkillEffect("Dark3", "Lv3 암흑 효과.", 12003, E_SKILL_EFFECT_LEVEL.S1, new STATUS(), new STATUS(), new SOC(), new SOC(), co);
        //m_Dictionary_SkillEffect.Add(se.m_sSkillEffectName, se);
        //sk = new Skill("Skill_Dark3", "Lv3 암흑 스킬", 12003, E_SKILL_TYPE.ACTIVE, E_SKILL_LEVEL.S2,
        //    new STATUS(), new STATUS(), new SOC(), new SOC(), se, new STATUS(), new SOC());
        //m_Dictionary_Skill.Add(sk.m_sSkillName, sk);

        //co = new Condition();
        //co.SetCondition(false, false, false, true);
        //co.SetConditionTime(0, 0, 0, 10);
        //co.SetSlowRatio(90);
        //se = new SkillEffect("Slow2", "Lv2 둔화 효과.", 13002, E_SKILL_EFFECT_LEVEL.S1, new STATUS(), new STATUS(), new SOC(), new SOC(), co);
        //m_Dictionary_SkillEffect.Add(se.m_sSkillEffectName, se);
        //sk = new Skill("Skill_Slow2", "Lv2 둔화 스킬", 13002, E_SKILL_TYPE.ACTIVE, E_SKILL_LEVEL.S2,
        //    new STATUS(), new STATUS(), new SOC(), new SOC(), se, new STATUS(), new SOC());
        //m_Dictionary_Skill.Add(sk.m_sSkillName, sk);


        // 액티브스킬_이동.
        // UP
        //co = new Condition();
        //se = new SkillEffect("Move1_UP", "Lv1 이동 효과.", 20001, E_SKILL_EFFECT_LEVEL.S1, new STATUS(), new STATUS(), new SOC(), new SOC(), co);
        //m_Dictionary_SkillEffect.Add(se.m_sSkillEffectName, se);
        //sk = new Skill_Active("Skill_Active_Move1_UP", "Lv1 이동 스킬.", 20001, E_SKILL_TYPE.ACTIVE, E_SKILL_LEVEL.S1,
        //    new STATUS(), new STATUS(), new SOC(), new SOC(), se, new STATUS(), new SOC(), E_SKILL_ACTIVE_TYPE.MOVE);
        //sk.Set_MOVE(new Vector2(0, 0.5f), 2f);
        //m_Dictionary_Skill.Add(sk.m_sSkillName, sk);
        //// DOWN
        //co = new Condition();
        //se = new SkillEffect("Move1_DOWN", "Lv1 이동 효과.", 20002, E_SKILL_EFFECT_LEVEL.S1, new STATUS(), new STATUS(), new SOC(), new SOC(), co);
        //m_Dictionary_SkillEffect.Add(se.m_sSkillEffectName, se);
        //sk = new Skill_Active("Skill_Active_Move1_DOWN", "Lv1 이동 스킬.", 20001, E_SKILL_TYPE.ACTIVE, E_SKILL_LEVEL.S1,
        //    new STATUS(), new STATUS(), new SOC(), new SOC(), se, new STATUS(), new SOC(), E_SKILL_ACTIVE_TYPE.MOVE);
        //sk.Set_MOVE(new Vector2(0, -0.5f), 2f);
        //m_Dictionary_Skill.Add(sk.m_sSkillName, sk);
        //// RIGHT
        //co = new Condition();
        //se = new SkillEffect("Move1_RIGHT", "Lv1 이동 효과.", 20002, E_SKILL_EFFECT_LEVEL.S1, new STATUS(), new STATUS(), new SOC(), new SOC(), co);
        //m_Dictionary_SkillEffect.Add(se.m_sSkillEffectName, se);
        //sk = new Skill_Active("Skill_Active_Move1_RIGHT", "Lv1 이동 스킬.", 20001, E_SKILL_TYPE.ACTIVE, E_SKILL_LEVEL.S1,
        //    new STATUS(), new STATUS(), new SOC(), new SOC(), se, new STATUS(), new SOC(), E_SKILL_ACTIVE_TYPE.MOVE);
        //sk.Set_MOVE(new Vector2(0.5f, 0), 2f);
        //m_Dictionary_Skill.Add(sk.m_sSkillName, sk);
        //// LEFT
        //co = new Condition();
        //se = new SkillEffect("Move1_LEFT", "Lv1 이동 효과.", 20002, E_SKILL_EFFECT_LEVEL.S1, new STATUS(), new STATUS(), new SOC(), new SOC(), co);
        //m_Dictionary_SkillEffect.Add(se.m_sSkillEffectName, se);
        //sk = new Skill_Active("Skill_Active_Move1_LEFT", "Lv1 이동 스킬.", 20001, E_SKILL_TYPE.ACTIVE, E_SKILL_LEVEL.S1,
        //    new STATUS(), new STATUS(), new SOC(), new SOC(), se, new STATUS(), new SOC(), E_SKILL_ACTIVE_TYPE.MOVE);
        //sk.Set_MOVE(new Vector2(-0.5f, 0), 2f);
        //m_Dictionary_Skill.Add(sk.m_sSkillName, sk);
        //// UP_RIGHT
        //co = new Condition();
        //se = new SkillEffect("Move1_UP_RIGHT", "Lv1 이동 효과.", 20002, E_SKILL_EFFECT_LEVEL.S1, new STATUS(), new STATUS(), new SOC(), new SOC(), co);
        //m_Dictionary_SkillEffect.Add(se.m_sSkillEffectName, se);
        //sk = new Skill_Active("Skill_Active_Move1_UP_RIGHT", "Lv1 이동 스킬.", 20001, E_SKILL_TYPE.ACTIVE, E_SKILL_LEVEL.S1,
        //    new STATUS(), new STATUS(), new SOC(), new SOC(), se, new STATUS(), new SOC(), E_SKILL_ACTIVE_TYPE.MOVE);
        //sk.Set_MOVE(new Vector2(0.5f, 0.5f), 2f);
        //m_Dictionary_Skill.Add(sk.m_sSkillName, sk);

        InitialSet_Slime2();
        InitialSet_TeSlime();
    }

    void InitialSet_Slime2()
    {
        Skill sk;
        Skill_Condition co;
        Skill_Effect se;
        Skill_SSEffect sse;

        co = new Skill_Condition();
        co.SetCondition(false, true, false, true, false);
        co.SetConditionTime(0, 1, 0, 2, 0);
        co.SetSlowRatio(30);
        se = new Skill_Effect("Lv1 Slime2_Shock/Slow", 13002);
        sse = new Skill_SSEffect(new STATUS(), new SOC(), new STATUS(), new SOC());
        sk = new Skill("Skill Lv1 Slime2_Shock/Slow", "'큰 초원 슬라임' 의 Lv1 상태이상 스킬[기절, 둔화]", 13002, E_SKILL_TYPE.ACTIVE, E_SKILL_LEVEL.S2, E_SKILL_TYPE_BD.DEBUFF,
            new STATUS(true), new STATUS(false), new SOC(true), new SOC(false),
            co, se, sse,
            new STATUS(), new SOC());
        sk.m_sStatus_Limit_Max.SetSTATUS_LV(20); // 스킬 적용 대상의 레벨이 20 이상인 경우 해당 스킬 적용 불가
        m_Dictionary_Skill.Add(sk.m_sSkillName, sk);

        //co = new Condition();
        //co.SetCondition(false, false, false, false);
        //se = new SkillEffect("TeSlime_Attack1_2", "'테 슬라임' 의 돌진 효과.", 20001, E_SKILL_EFFECT_LEVEL.S1, new STATUS(), new STATUS(), new SOC(), new SOC(), co);
        //m_Dictionary_SkillEffect.Add(se.m_sSkillEffectName, se);
        //sk = new Skill_Active("TeSlime_Slow1", "'테 슬라임' 의 돌진 스킬.", 20001, E_SKILL_TYPE.ACTIVE, E_SKILL_LEVEL.S2,
        //    new STATUS(), new STATUS(), new SOC(), new SOC(), se, new STATUS(), new SOC());
        //m_Dictionary_Skill.Add(sk.m_sSkillName, sk);

    }
    void InitialSet_TeSlime()
    {
        Skill sk;
        Skill_Condition co;
        Skill_Effect se;
        Skill_SSEffect sse;

        co = new Skill_Condition();
        co.SetCondition(false, false, false, true);
        co.SetConditionTime(0, 0, 0, 3);
        co.SetSlowRatio(30);
        se = new Skill_Effect("Lv1 TeSlime_Slow", 13003);
        sse = new Skill_SSEffect(new STATUS(), new SOC(), new STATUS(), new SOC());
        sk = new Skill("Skill Lv1 TeSlime_Slow", "'테슬라임' 의 Lv1 상태이상 스킬[둔화]", 13003, E_SKILL_TYPE.ACTIVE, E_SKILL_LEVEL.S3, E_SKILL_TYPE_BD.DEBUFF,
            new STATUS(true), new STATUS(false), new SOC(true), new SOC(false),
            co, se, sse,
            new STATUS(), new SOC());
        sk.m_sStatus_Limit_Max.SetSTATUS_LV(20); // 스킬 적용 대상의 레벨이 20 이상인 경우 해당 스킬 적용 불가
        m_Dictionary_Skill.Add(sk.m_sSkillName, sk);
    }
}
