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
    public Dictionary<string, SkillEffect> m_Dictionary_SkillEffect;

    public void InitialSet()
    {
        m_Dictionary_Skill = new Dictionary<string, Skill>();
        m_Dictionary_SkillEffect = new Dictionary<string, SkillEffect>();

        Skill sk;
        SkillEffect se;
        Condition co;

        // 패시브 스킬_디버프(상태 이상, 스탯/평판 감소).
        //
        //co = new Condition();
        //co.SetCondition(true);
        //co.SetConditionTime(2);
        //se = new SkillEffect("Bind1", "Lv1 바인드 효과.",10001 , E_SKILL_EFFECT_LEVEL.S1, new STATUS(), new STATUS(), new SOC(), new SOC(), co);
        //m_Dictionary_SkillEffect.Add(se.m_sSkillEffectName, se);
        //sk = new Skill("Skill_Bind1", "Lv1 바인드 스킬", 10001, E_SKILL_TYPE.ACTIVE, E_SKILL_LEVEL.S2,
        //    new STATUS(), new STATUS(), new SOC(), new SOC(), se, new STATUS(), new SOC());
        //m_Dictionary_Skill.Add(sk.m_sSkillName, sk);

        //co = new Condition();
        //co.SetCondition(false, true);
        //co.SetConditionTime(0, 2);
        //se = new SkillEffect("Shock1", "Lv1 기절 효과.", 11001, E_SKILL_EFFECT_LEVEL.S1, new STATUS(), new STATUS(), new SOC(), new SOC(), co);
        //m_Dictionary_SkillEffect.Add(se.m_sSkillEffectName, se);
        //sk = new Skill("Skill_Shock1", "Lv1 기절 스킬", 11001, E_SKILL_TYPE.ACTIVE, E_SKILL_LEVEL.S3,
        //    new STATUS(), new STATUS(), new SOC(), new SOC(), se, new STATUS(), new SOC());
        //m_Dictionary_Skill.Add(sk.m_sSkillName, sk);

        //co = new Condition();
        //co.SetCondition(false, false, true);
        //co.SetConditionTime(0, 0, 10);
        //co.SetDarkRatio(30);
        //se = new SkillEffect("Dark1", "Lv1 암흑 효과.", 12001, E_SKILL_EFFECT_LEVEL.S1, new STATUS(), new STATUS(), new SOC(), new SOC(), co);
        //m_Dictionary_SkillEffect.Add(se.m_sSkillEffectName, se);
        //sk = new Skill("Skill_Dark1", "Lv1 암흑 스킬", 12001, E_SKILL_TYPE.ACTIVE, E_SKILL_LEVEL.S2,
        //    new STATUS(), new STATUS(), new SOC(), new SOC(), se, new STATUS(), new SOC());
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
        //co.SetSlowRatio(30);
        //se = new SkillEffect("Slow1", "Lv1 둔화 효과.", 13001, E_SKILL_EFFECT_LEVEL.S1, new STATUS(), new STATUS(), new SOC(), new SOC(), co);
        //m_Dictionary_SkillEffect.Add(se.m_sSkillEffectName, se);
        //sk = new Skill("Skill_Slow1", "Lv1 둔화 스킬", 13001, E_SKILL_TYPE.ACTIVE, E_SKILL_LEVEL.S2,
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
        SkillEffect se;
        Condition co;

        co = new Condition();
        co.SetCondition(false, false, false, true);
        co.SetConditionTime(0, 0, 0, 3);
        co.SetSlowRatio(10);
        se = new SkillEffect("TeSlime_Slow1", "'테 슬라임' 의 1단계 둔화 효과.", 13003, E_SKILL_EFFECT_LEVEL.S3, new STATUS(), new STATUS(), new SOC(), new SOC(), co);
        m_Dictionary_SkillEffect.Add(se.m_sSkillEffectName, se);
        sk = new Skill("TeSlime_Slow1", "'테 슬라임' 의 1단계 둔화 스킬.", 13003, E_SKILL_TYPE.PASSIVE, E_SKILL_LEVEL.S3,
            new STATUS(), new STATUS(), new SOC(), new SOC(), se, new STATUS(), new SOC());
        m_Dictionary_Skill.Add(sk.m_sSkillName, sk);
    }

    void InitialSet_TeSlime()
    {
        Skill sk;
        SkillEffect se;
        Condition co;

        co = new Condition();
        co.SetCondition(false ,true, false, true, false);
        co.SetConditionTime(0, 1, 0, 1, 0);
        co.SetSlowRatio(10);
        se = new SkillEffect("Slime2_AttackEffect1", "'큰 초원 슬라임' 의 1단계 피격 디버프 효과.", 15000, E_SKILL_EFFECT_LEVEL.S2, new STATUS(), new STATUS(), new SOC(), new SOC(), co);
        m_Dictionary_SkillEffect.Add(se.m_sSkillEffectName, se);
        sk = new Skill("Slime2_AttackEffect1", "'큰 초원 슬라임' 의 1단계 피격 디버프 효과.", 15000, E_SKILL_TYPE.PASSIVE, E_SKILL_LEVEL.S2,
            new STATUS(), new STATUS(), new SOC(), new SOC(), se, new STATUS(), new SOC());
        m_Dictionary_Skill.Add(sk.m_sSkillName, sk);

        //co = new Condition();
        //co.SetCondition(false, false, false, false);
        //se = new SkillEffect("TeSlime_Attack1_2", "'테 슬라임' 의 돌진 효과.", 20001, E_SKILL_EFFECT_LEVEL.S1, new STATUS(), new STATUS(), new SOC(), new SOC(), co);
        //m_Dictionary_SkillEffect.Add(se.m_sSkillEffectName, se);
        //sk = new Skill_Active("TeSlime_Slow1", "'테 슬라임' 의 돌진 스킬.", 20001, E_SKILL_TYPE.ACTIVE, E_SKILL_LEVEL.S2,
        //    new STATUS(), new STATUS(), new SOC(), new SOC(), se, new STATUS(), new SOC());
        //m_Dictionary_Skill.Add(sk.m_sSkillName, sk);

    }
}