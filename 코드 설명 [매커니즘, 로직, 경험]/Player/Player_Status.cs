using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Status : MonoBehaviour
{
    // 평판 관련.
    public SOC m_sSoc;
    public SOC m_sSoc_Origin;
    public SOC m_sSoc_Extra_Equip_Hat;
    public SOC m_sSoc_Extra_Equip_Top;
    public SOC m_sSoc_Extra_Equip_Bottoms;
    public SOC m_sSoc_Extra_Equip_Shose;
    public SOC m_sSoc_Extra_Equip_Gloves;
    public SOC m_sSoc_Extra_Equip_Mainweapon;
    public SOC m_sSoc_Extra_Equip_Subweapon;
    public SOC m_sSoc_Item_Use_Buff;
    public SOC m_sSoc_Extra_ItemSetEffect;
    public SOC m_sSoc_Null;

    // 스탯 관련.
    public STATUS m_sStatus; // Total Status / m_sStatus_Origin + m_sStatus_Extra
    public STATUS m_sStatus_Origin;
    public STATUS m_sStatus_Extra_Equip_Hat;
    public STATUS m_sStatus_Extra_Equip_Top;
    public STATUS m_sStatus_Extra_Equip_Bottoms;
    public STATUS m_sStatus_Extra_Equip_Shose;
    public STATUS m_sStatus_Extra_Equip_Gloves;
    public STATUS m_sStatus_Extra_Equip_Mainweapon;
    public STATUS m_sStatus_Extra_Equip_Subweapon;
    public STATUS m_sStatus_Item_Use_Buff;
    public STATUS m_sStatus_Extra_ItemSetEffect;
    public STATUS m_sStatus_Null;

    // Player 상태.
    public static Condition m_cCondition;
    // Player 에게 적용되고있는 스킬.
    public static List<Skill> m_List_Skill;
    // Player 에게 적용되고있는 소비 아이템 효과(버프).
    public static Dictionary <int, Item_Use> m_Dictionary_Item_Use_Buff;
    public static Dictionary <int, Item_Use> m_Dictionary_Item_Use_CoolTime;
    public static Dictionary <int, Coroutine> m_Dictionary_Coroutine_Item_Use_Buff;
    public static Dictionary <int, Coroutine> m_Dictionary_Coroutine_Item_Use_CoolTime;
    ////// 게임 종료 시 남은 잔여 아이템 지속시간, 쿨타임을 저장함.
    public static Dictionary <int, float> m_Dictionary_Item_Use_Buff_RemainingTime;
    public static Dictionary <int, float> m_Dictionary_Item_Use_CoolTime_RemainingTime;

    // Condition 표현.
    GameObject m_gCondition;
    public GameObject m_gCondition_Bind;
    public GameObject m_gCondition_Shock;
    public GameObject m_gCondition_Dark;
    public GameObject m_gCondition_Slow;
    public GameObject m_gCondition_Confuse;

    // 데미지 출력 오프셋
    protected Vector3 m_vDamageOffSet;

    public void InitialSet()
    {
        InitialSet_Status();
        InitialSet_SOC();
        InitialSet_Condition();
        InitialSet_Skill();
        InitialSet_Item_Use();

        m_vDamageOffSet = new Vector3(0, 0.2f, 0);
    }

    public void InitialSet_Status()
    {
        // 기본 스탯(공속 0.1)
        // 공속의 경우 ATTACK1_1(0.5f), ATTACK1_2(0.5f), ATTACK1_3(1.0f) 의 연계 조건시간이 필요.
        // 현 공속이 만약 1이라면 ATTACK1_1 에서 ATTACK1_2로의 공격 연계는 불가능.
        m_sStatus_Origin = new STATUS(1, 10, 0, 10, 10, 0, 0, 1, 1, 1, 0, 100, 100, 1, 1, 0, 0.1f);
        //m_sStatus_Origin = new STATUS(1, 10, 0, 300, 300, 0, 0, 25, 1, 1, 0, 100, 100, 0, 0, 0, 0.1f);
        m_sStatus_Item_Use_Buff = new STATUS();
        m_sStatus_Extra_ItemSetEffect = new STATUS();
        m_sStatus_Null = new STATUS();
        m_sStatus = new STATUS();
        m_sStatus.SetSTATUS(m_sStatus_Origin);

    }
    public void InitialSet_SOC()
    {
        m_sSoc = new SOC();
        m_sSoc_Item_Use_Buff = new SOC();
        m_sSoc_Extra_ItemSetEffect = new SOC();
        m_sSoc_Null = new SOC();
        m_sSoc_Origin = new SOC();
        m_sSoc_Origin.SetSOC(m_sSoc);
    }
    public void InitialSet_Condition()
    {
        m_gCondition = transform.Find("Player_Condition").gameObject;
        m_gCondition_Bind = m_gCondition.transform.Find("Animation_Bind").gameObject;
        m_gCondition_Shock = m_gCondition.transform.Find("Animation_Shock").gameObject;
        m_gCondition_Dark = m_gCondition.transform.Find("Animation_Dark").gameObject;
        m_gCondition_Slow = m_gCondition.transform.Find("Animation_Slow").gameObject;
        m_gCondition_Confuse = m_gCondition.transform.Find("Animation_Confuse").gameObject;

        m_cCondition = new Condition();
    }
    public void InitialSet_Skill()
    {
        m_List_Skill = new List<Skill>();
    }
    public void InitialSet_Item_Use()
    {
        m_Dictionary_Item_Use_Buff = new Dictionary<int, Item_Use>();
        m_Dictionary_Item_Use_CoolTime = new Dictionary<int, Item_Use>();
        m_Dictionary_Coroutine_Item_Use_Buff = new Dictionary<int, Coroutine>();
        m_Dictionary_Coroutine_Item_Use_CoolTime = new Dictionary<int, Coroutine>();
        m_Dictionary_Item_Use_Buff_RemainingTime = new Dictionary<int, float>();
        m_Dictionary_Item_Use_CoolTime_RemainingTime = new Dictionary<int, float>();
    }

    int m_nTotalDamage;
    int m_fTotalDamage;
    public void Attacked(int damage, Vector3 dir)
    {
        m_fTotalDamage = (int)((float)damage * ((float)m_sStatus.GetSTATUS_Defence_Physical() / ((float)(10) + (float)m_sStatus.GetSTATUS_Defence_Physical())));
        m_nTotalDamage = damage - (int)Mathf.Round(m_fTotalDamage);

        //m_nTotalDamage = damage - m_sStatus.GetSTATUS_Defence_Physical();

        if (m_nTotalDamage <= 0)
            m_nTotalDamage = 1;

        if (m_sStatus.GetSTATUS_HP_Current() - m_nTotalDamage > 0)
            m_sStatus.P_OperatorSTATUS_HP_Current(-m_nTotalDamage);
        else
            m_sStatus.SetSTATUS_HP_Current(0);

        GameObject obj = Resources.Load("Prefab/GUI/TextMesh_Damage") as GameObject;
        GameObject copyobj = Instantiate(obj);
        copyobj.GetComponent<TextMesh_Damage>().InitialSet(this.transform.position + m_vDamageOffSet, -m_nTotalDamage);
        //GUIManager_Total.Instance.UpdateLog("HP: " + (-damage).ToString());
    }

    public void Goaway(SOC soc)
    {
        m_sSoc_Origin.P_OperatorSOC(soc);
        UpdateSOC();
    }

    int stexp;
    void CarculateEXP(STATUS status)
    {
        // 획득 경험치
        stexp = status.GetSTATUS_EXP_Current();
        stexp += m_nEXP_Current;
        if (stexp < m_sStatus.GetSTATUS_EXP_Max())
        {
            m_sStatus.SetSTATUS_EXP_Current(stexp);
        }
        else
        {
            while (stexp >= m_sStatus.GetSTATUS_EXP_Max())
            {
                stexp = stexp - m_sStatus.GetSTATUS_EXP_Max();
                CarculateLV();
            }

            m_sStatus.SetSTATUS_EXP_Current(stexp);
        }
        m_nEXP_Current = 0;
    }
    // LV 업
    void CarculateLV()
    {
        m_sStatus_Origin.P_OperatorSTATUS_LV(1);
        if (m_sStatus_Origin.GetSTATUS_LV() % 5 == 0)
        {
            m_sStatus_Origin.P_OperatorSTATUS_Damage_Total(1);
            m_sStatus_Origin.P_OperatorSTATUS_Damage_Physical(1);
            m_sStatus_Origin.P_OperatorSTATUS_Damage_Magical(1);
            m_sStatus_Origin.P_OperatorSTATUS_Defence_Physical(1);
            m_sStatus_Origin.P_OperatorSTATUS_Defence_Magical(1);
        }
        // 경험치통은 1.3배로 늘어난다???
        m_sStatus_Origin.M_OperatorSTATUS_EXP_Max(1.3f);
        m_sStatus_Origin.SetSTATUS_EXP_Current(0);

        m_sStatus_Origin.P_OperatorSTATUS_HP_Max(1);
        //m_sStatus_Origin.SetSTATUS_HP_Current(m_sStatus.GetSTATUS_HP_Max());
        m_sStatus_Origin.P_OperatorSTATUS_MP_Max(1);
        //m_sStatus_Origin.SetSTATUS_MP_Current(m_sStatus.GetSTATUS_MP_Max());

        UpdateStatus_LVup();

        m_sStatus.SetSTATUS_HP_Current(m_sStatus.GetSTATUS_HP_Max());
        m_sStatus.SetSTATUS_MP_Current(m_sStatus.GetSTATUS_MP_Max());

        m_nEXP_Current = m_sStatus.GetSTATUS_EXP_Current();
        m_nHP_Current = m_sStatus.GetSTATUS_HP_Current();
        m_nMP_Current = m_sStatus.GetSTATUS_MP_Current();
    }
    STATUS m_sStatusBefore;
    int m_nEXP_Current;
    int m_nHP_Current;
    int m_nMP_Current;

    // 소비 아이템 사용으로 인한 스탯 변경.
    // 회복포션.
    // Status: HP_Current
    // Status: MP_Current
    public void UpdateStatus_Item_Use_Recover(STATUS status)
    {
        m_sStatus.P_OperatorSTATUS_HP_Current(status.GetSTATUS_HP_Current());
        m_sStatus.P_OperatorSTATUS_MP_Current(status.GetSTATUS_MP_Current());

        CheckLogic();

        GUIManager_Total.Instance.Update_SS();
        GUIManager_Total.Instance.Update_Itemslot();
        GUIManager_Total.Instance.Update_Equipslot();
    }
    // 영구적 버프포션.
    public void UpdateStatus_Item_Use_EternalBuff(STATUS status, SOC soc)
    {
        m_sStatus_Origin.P_OperatorSTATUS(status);
        m_sSoc_Origin.P_OperatorSOC(soc);

        UpdateStatus_Equip();
        UpdateSOC();

        GUIManager_Total.Instance.Update_SS();
        GUIManager_Total.Instance.Update_Itemslot();
        GUIManager_Total.Instance.Update_Equipslot();
    }
    // 일시적 버프포션.
    public void UpdateStatus_Item_Use_TemporaryBuff()
    {
        m_sSoc.SetSOC_Zero();

        // Status
        m_nEXP_Current = m_sStatus.GetSTATUS_EXP_Current();
        m_nHP_Current = m_sStatus.GetSTATUS_HP_Current();
        m_nMP_Current = m_sStatus.GetSTATUS_MP_Current();

        m_sStatus.SetSTATUS_Zero();
        m_sStatus.P_OperatorSTATUS(m_sStatus_Origin);

        m_sStatus_Item_Use_Buff.SetSTATUS_Zero();
        m_sSoc_Item_Use_Buff.SetSOC_Zero();
        //for (int i = 0; i < m_List_Item_Use_Buff.Count; i++)
        //{
        //    m_sStatus_Item_Use_Buff.P_OperatorSTATUS(m_List_Item_Use_Buff[i].m_sStatus_Effect);
        //    m_sSoc_Item_Use_Buff.P_OperatorSOC(m_List_Item_Use_Buff[i].m_sSoc_Effect);
        //}
        foreach (KeyValuePair<int, Item_Use> ditem in m_Dictionary_Item_Use_Buff)
        {
            m_sStatus_Item_Use_Buff.P_OperatorSTATUS(ditem.Value.m_sStatus_Effect);
            m_sSoc_Item_Use_Buff.P_OperatorSOC(ditem.Value.m_sSoc_Effect);
        }
        m_sStatus.P_OperatorSTATUS(m_sStatus_Item_Use_Buff);

        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Hat);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Top);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Bottoms);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Shose);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Gloves);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Mainweapon);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Subweapon);

        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_ItemSetEffect);

        m_sStatus.SetSTATUS_EXP_Current(m_nEXP_Current);
        m_sStatus.SetSTATUS_HP_Current(m_nHP_Current);
        m_sStatus.SetSTATUS_MP_Current(m_nMP_Current);

        CheckLogic();

        UpdateSOC();

        GUIManager_Total.Instance.Update_SS();
        GUIManager_Total.Instance.Update_Itemslot();
        GUIManager_Total.Instance.Update_Equipslot();
    }

    // 장비 착용으로인한 스탯 변경
    public void UpdateStatus_Equip()
    {
        // Status
        m_nEXP_Current = m_sStatus.GetSTATUS_EXP_Current();
        m_nHP_Current = m_sStatus.GetSTATUS_HP_Current();
        m_nMP_Current = m_sStatus.GetSTATUS_MP_Current();

        m_sStatus.SetSTATUS_Zero();
        m_sStatus.P_OperatorSTATUS(m_sStatus_Origin);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Item_Use_Buff);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Hat);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Top);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Bottoms);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Shose);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Gloves);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Mainweapon);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Subweapon);

        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_ItemSetEffect);

        m_sStatus.SetSTATUS_EXP_Current(m_nEXP_Current);
        m_sStatus.SetSTATUS_HP_Current(m_nHP_Current);
        m_sStatus.SetSTATUS_MP_Current(m_nMP_Current);

        //CheckLogic();

        // Soc
        m_sSoc.SetSOC_Zero();
    }
    
    // 레벨업 으로인한 스탯 변경
    public void UpdateStatus_LVup()
    {
        m_sStatus.SetSTATUS_Zero();
        m_sStatus.P_OperatorSTATUS(m_sStatus_Origin);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Item_Use_Buff);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Hat);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Top);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Bottoms);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Shose);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Gloves);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Mainweapon);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Subweapon);

        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_ItemSetEffect);

        m_sStatus.SetSTATUS_EXP_Current(m_nEXP_Current);
        m_sStatus.SetSTATUS_HP_Current(m_nHP_Current);
        m_sStatus.SetSTATUS_MP_Current(m_nMP_Current);

        CheckLogic();
    }

    // 퀘스트 클리어로 인한 스탯 변경.
    public void UpdateStatus_QuestClear(STATUS extrastatus)
    {
        m_sStatus_Origin.P_OperatorSTATUS_HP_Max(extrastatus.GetSTATUS_HP_Max());
        m_sStatus_Origin.P_OperatorSTATUS_MP_Max(extrastatus.GetSTATUS_MP_Max());
        m_sStatus_Origin.P_OperatorSTATUS_Damage_Total(extrastatus.GetSTATUS_Damage_Total());
        m_sStatus_Origin.P_OperatorSTATUS_CriticalRate(extrastatus.GetSTATUS_CriticalRate());
        m_sStatus_Origin.P_OperatorSTATUS_CriticalDamage(extrastatus.GetSTATUS_CriticalDamage());
        m_sStatus_Origin.P_OperatorSTATUS_Defence_Physical(extrastatus.GetSTATUS_Defence_Physical());
        m_sStatus_Origin.P_OperatorSTATUS_Defence_Magical(extrastatus.GetSTATUS_Defence_Magical());
        m_sStatus_Origin.P_OperatorSTATUS_Speed(extrastatus.GetSTATUS_Speed());
        m_sStatus_Origin.P_OperatorSTATUS_AttackSpeed(extrastatus.GetSTATUS_AttackSpeed());
        m_sStatus_Origin.P_OperatorSTATUS_EvasionRate(extrastatus.GetSTATUS_EvasionRate());

        m_nHP_Current = m_sStatus.GetSTATUS_HP_Current();
        m_nMP_Current = m_sStatus.GetSTATUS_MP_Current();

        m_sStatus.SetSTATUS_Zero();
        m_sStatus.P_OperatorSTATUS(m_sStatus_Origin);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Item_Use_Buff);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Hat);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Top);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Bottoms);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Shose);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Gloves);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Mainweapon);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Subweapon);

        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_ItemSetEffect);

        m_sStatus.SetSTATUS_HP_Current(m_nHP_Current);
        m_sStatus.SetSTATUS_MP_Current(m_nMP_Current);

        //m_sStatus.SetSTATUS_EXP_Current(m_nEXP_Current);
        //m_sStatus.SetSTATUS_HP_Current(m_nHP_Current);
        //m_sStatus.SetSTATUS_MP_Current(m_nMP_Current);

        CheckLogic();
    }

    // 스킬 적용으로인한 스탯 변경(일시적)
    public void UpdateStatus_ApplySkill()
    {
        // Status
        m_nEXP_Current = m_sStatus.GetSTATUS_EXP_Current();
        m_nHP_Current = m_sStatus.GetSTATUS_HP_Current();
        m_nMP_Current = m_sStatus.GetSTATUS_MP_Current();

        m_sStatus.SetSTATUS_Zero();
        m_sStatus.P_OperatorSTATUS(m_sStatus_Origin);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Item_Use_Buff);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Hat);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Top);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Bottoms);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Shose);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Gloves);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Mainweapon);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Subweapon);

        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_ItemSetEffect);

        for (int i = 0; i < m_List_Skill.Count; i++)
        {
            m_sStatus.P_OperatorSTATUS(m_List_Skill[i].m_seSkillEffect.m_sStatus_Effect_Temporary);
        }

        m_sStatus.SetSTATUS_EXP_Current(m_nEXP_Current);
        m_sStatus.SetSTATUS_HP_Current(m_nHP_Current);
        m_sStatus.SetSTATUS_MP_Current(m_nMP_Current);

        CheckLogic();
    }

    // 스킬 적용으로인한 평판 변경(일시적)
    public void UpdateSoc_ApplySkill()
    {
        m_sSoc.SetSOC_Zero();
        m_sSoc.P_OperatorSOC(m_sSoc_Origin);
        m_sSoc.P_OperatorSOC(m_sSoc_Item_Use_Buff);
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Hat);
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Top);
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Bottoms);
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Shose);
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Gloves);
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Mainweapon);
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Subweapon);

        for (int i = 0; i < m_List_Skill.Count; i++)
        {
            m_sSoc.P_OperatorSOC(m_List_Skill[i].m_seSkillEffect.m_sSoc_Effect_Temporary);
        }
    }

    // Monster Kill, Goaway 로 인한 SOC(평판) 변경
    public void UpdateSOC()
    {
        m_sSoc.SetSOC_Zero();
        m_sSoc.P_OperatorSOC(m_sSoc_Origin);
        m_sSoc.P_OperatorSOC(m_sSoc_Item_Use_Buff);
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Hat);
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Top);
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Bottoms);
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Shose);
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Gloves);
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Mainweapon);
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Subweapon);

        m_sSoc.P_OperatorSOC(m_sSoc_Extra_ItemSetEffect);
    }

    public void MobDeath(SOC soc, STATUS status)
    {
        m_sSoc_Origin.P_OperatorSOC(soc);
        UpdateSOC();

        m_nEXP_Current = m_sStatus.GetSTATUS_EXP_Current();

        CarculateEXP(status);
        if (status.GetSTATUS_EXP_Current() != 0)
            GUIManager_Total.Instance.UpdateLog("+EXP: " + status.GetSTATUS_EXP_Current());
    }

    // 로딩 시 스탯 계산.
    public void UpdateStatus_Loading(int exp, int hp, int mp)
    {
        m_sStatus.SetSTATUS_Zero();
        m_sStatus.P_OperatorSTATUS(m_sStatus_Origin);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Item_Use_Buff);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Hat);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Top);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Bottoms);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Shose);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Gloves);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Mainweapon);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Subweapon);

        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_ItemSetEffect);

        for (int i = 0; i < m_List_Skill.Count; i++)
        {
            m_sStatus.P_OperatorSTATUS(m_List_Skill[i].m_seSkillEffect.m_sStatus_Effect_Temporary);
        }

        m_sStatus.SetSTATUS_EXP_Current(exp);
        m_sStatus.SetSTATUS_HP_Current(hp);
        m_sStatus.SetSTATUS_MP_Current(mp);

        CheckLogic();

        Player_Total.Instance.m_pm_Move.SetAttackSpeed(Return_AttackSpeed());
    }

    // 로딩 시 스탯 계산.
    public void UpdateSoc_Loading()
    {
        m_sSoc.SetSOC_Zero();
        m_sSoc.P_OperatorSOC(m_sSoc_Origin);
        m_sSoc.P_OperatorSOC(m_sSoc_Item_Use_Buff);
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Hat);
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Top);
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Bottoms);
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Shose);
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Gloves);
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Mainweapon);
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Subweapon);

        m_sSoc.P_OperatorSOC(m_sSoc_Extra_ItemSetEffect);

        for (int i = 0; i < m_List_Skill.Count; i++)
        {
            m_sSoc.P_OperatorSOC(m_List_Skill[i].m_seSkillEffect.m_sSoc_Effect_Temporary);
        }
    }

    // 로딩 시 소비 아이템 쿨타임, 지속시간.
    public void Item_Use_Loading()
    {
        
    }

    // Player Stauts 업데이트.
    public void Update_Loading(int exp, int hp, int mp, STATUS status, SOC soc)
    {
        UpdateStatus_Loading(exp, hp, mp);
        UpdateSoc_Loading();

        // Load 마지막 부분에 추가 예정.
        //if (m_sStatus.CheckIdentity(status) == true)
        //{
        //    GUIManager_Total.Instance.UpdateLog("Player_Status_STATUS 동기화 성공.");
        //}
        //else
        //{
        //    GUIManager_Total.Instance.UpdateLog("Player_Status_STATUS 동기화 실패.");
        //}

        //if (m_sSoc.CheckIdentity(soc) == true)
        //{
        //    GUIManager_Total.Instance.UpdateLog("Player_Status_SOC 동기화 성공.");
        //}
        //else
        //{
        //    GUIManager_Total.Instance.UpdateLog("Player_Status_SOC 동기화 실패.");
        //}

        Player_Total.Instance.m_pm_Move.SetAttackSpeed(Return_AttackSpeed());
    }

    public void GetQuestReward(Quest quest)
    {
        m_sSoc_Origin.P_OperatorSOC(quest.m_sRewardSOC);
        UpdateSOC();

        m_nEXP_Current = m_sStatus.GetSTATUS_EXP_Current();
        UpdateStatus_QuestClear(quest.m_sRewardSTATUS);

        CarculateEXP(quest.m_sRewardSTATUS);
        GUIManager_Total.Instance.UpdateLog("+EXP: " + quest.m_sRewardSTATUS.GetSTATUS_EXP_Current());
        // + 퀘스트 보상 스탯.
    }

    // 논리(조건) 체크
    public void CheckLogic()
    {
        if (m_sStatus.GetSTATUS_HP_Current() > m_sStatus.GetSTATUS_HP_Max())
        {
            m_sStatus.SetSTATUS_HP_Current(m_sStatus.GetSTATUS_HP_Max());
        }

        if (m_sStatus.GetSTATUS_MP_Current() > m_sStatus.GetSTATUS_MP_Max())
        {
            m_sStatus.SetSTATUS_MP_Current(m_sStatus.GetSTATUS_MP_Max());
        }
        //Debug.Log(m_sStatus.GetSTATUS_HP_Current() + " / " +  m_sStatus.GetSTATUS_HP_Max() + " / " + m_sStatus_Extra_ItemSetEffect.GetSTATUS_HP_Max());
    }
    
    // 적용중인 버프, 디버프, 스킬 모두 해제.
    void UpdateStatus_ReTry()
    {
        m_nEXP_Current = m_sStatus.GetSTATUS_EXP_Current();
        m_nHP_Current = m_sStatus.GetSTATUS_HP_Current();
        m_nMP_Current = m_sStatus.GetSTATUS_MP_Current();

        m_sStatus.SetSTATUS_Zero();
        m_sStatus.P_OperatorSTATUS(m_sStatus_Origin);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Hat);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Top);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Bottoms);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Shose);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Gloves);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Mainweapon);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Subweapon);
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_ItemSetEffect);

        m_sStatus_Item_Use_Buff.SetSTATUS_Zero();
        m_sStatus.P_OperatorSTATUS(m_sStatus_Item_Use_Buff);

        m_sStatus.SetSTATUS_EXP_Current(m_nEXP_Current);
        m_sStatus.SetSTATUS_HP_Current(m_sStatus.GetSTATUS_HP_Max());
        m_sStatus.SetSTATUS_MP_Current(m_sStatus.GetSTATUS_MP_Max());

        CheckLogic();
    }
    void UpdateSoc_ReTry()
    {
        m_sSoc.SetSOC_Zero();
        m_sSoc.P_OperatorSOC(m_sSoc_Origin);
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Hat);
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Top);
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Bottoms);
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Shose);
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Gloves);
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Mainweapon);
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Subweapon);
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_ItemSetEffect);

        m_sSoc_Item_Use_Buff.SetSOC_Zero();
        m_sSoc.P_OperatorSOC(m_sSoc_Item_Use_Buff);
    }
    public void ReTry_Initializing()
    {
        m_Dictionary_Item_Use_Buff.Clear();
        m_Dictionary_Item_Use_CoolTime.Clear();

        foreach(KeyValuePair<int, Coroutine> item in m_Dictionary_Coroutine_Item_Use_Buff)
        {
            StopCoroutine(item.Value);
        }
        foreach (KeyValuePair<int, Coroutine> item in m_Dictionary_Coroutine_Item_Use_CoolTime)
        {
            StopCoroutine(item.Value);
        }
        m_Dictionary_Coroutine_Item_Use_Buff.Clear();
        m_Dictionary_Coroutine_Item_Use_CoolTime.Clear();

        m_Dictionary_Item_Use_Buff_RemainingTime.Clear();
        m_Dictionary_Item_Use_CoolTime_RemainingTime.Clear();

        if (m_cProcessBind != null)
            StopCoroutine(m_cProcessBind);
        m_gCondition_Bind.SetActive(false);
        if (m_cProcessConfuse != null)
            StopCoroutine(m_cProcessConfuse);
        m_gCondition_Confuse.SetActive(false);
        if (m_cProcessDark != null)
            StopCoroutine(m_cProcessDark);
        m_gCondition_Dark.SetActive(false);
        if (m_cProcessShock != null)
            StopCoroutine(m_cProcessShock);
        m_gCondition_Shock.SetActive(false);
        if (m_cProcessSlow != null)
            StopCoroutine(m_cProcessSlow);
        m_gCondition_Slow.SetActive(false);

        m_cCondition.Initializing();

        m_List_Skill.Clear();

        UpdateStatus_ReTry();
        UpdateSoc_ReTry();
    }

    public void ReTry()
    {
        ReTry_Initializing();
    }

    // 장비 세트 효과
    public void CheckSetItemEffect(Dictionary<int, int> setitemdictionary)
    {
        m_sStatus_Extra_ItemSetEffect.SetSTATUS_Zero();
        m_sSoc_Extra_ItemSetEffect.SetSOC_Zero();
        foreach(KeyValuePair<int, int> dictionary in setitemdictionary)
        {
            if (dictionary.Key != 0)
            {
                for (int i = 1; i < dictionary.Value + 1; i++)
                {
                    m_sStatus_Extra_ItemSetEffect.P_OperatorSTATUS(ItemSetEffectManager.instance.Return_SetItemEffect_STATUS(dictionary.Key, i));
                    m_sSoc_Extra_ItemSetEffect.P_OperatorSOC(ItemSetEffectManager.instance.Return_SetItemEffect_SOC(dictionary.Key, i));
                }
                    
                //GUIManager_Total.Instance.UpdateLog(ItemSetEffectManager.m_Dictionary_ItemSetEffect[dictionary.Key].m_sItemSetEffect_Name + " / " + dictionary.Value);
            }
        }

        UpdateStatus_Equip();
        UpdateSOC();
    }

    // 장비 착용 조건 체크 + 장비 착용
    public bool CheckCondition_Item_Equip(Item_Equip item, STATUS playerstatus, SOC playersoc)
    {
        if (playerstatus.CheckCondition_Max(item.m_sStatus_Limit_Max) == false)
        {
            Debug.Log(item.m_sItemName + ": Status 착용 최대 조건 불만족");
            return false;
        }
        if (playerstatus.CheckCondition_Min(item.m_sStatus_Limit_Min) == false)
        {
            Debug.Log(item.m_sItemName + ": Status 착용 최소 조건 불만족");
            return false;
        }
        if (playersoc.CheckCondition_Max(item.m_sSoc_Limit_Max) == false)
        {
            Debug.Log(item.m_sItemName + ": Soc 착용 최대 조건 불만족");
            return false;
        }
        if (playersoc.CheckCondition_Min(item.m_sSoc_Limit_Min) == false)
        {
            Debug.Log(item.m_sItemName + ": Soc 착용 최소 조건 불만족");
            return false;
        }

        switch (item.m_eItemEquipType)
        {
            case E_ITEM_EQUIP_TYPE.HAT:
                {
                    m_sStatus_Extra_Equip_Hat = item.m_sStatus_Effect;
                    m_sSoc_Extra_Equip_Hat = item.m_sSoc_Effect;
                }
                break;
            case E_ITEM_EQUIP_TYPE.TOP:
                {
                    m_sStatus_Extra_Equip_Top = item.m_sStatus_Effect;
                    m_sSoc_Extra_Equip_Top = item.m_sSoc_Effect;
                }
                break;
            case E_ITEM_EQUIP_TYPE.BOTTOMS:
                {
                    m_sStatus_Extra_Equip_Bottoms = item.m_sStatus_Effect;
                    m_sSoc_Extra_Equip_Bottoms = item.m_sSoc_Effect;
                }
                break;
            case E_ITEM_EQUIP_TYPE.SHOSE:
                {
                    m_sStatus_Extra_Equip_Shose = item.m_sStatus_Effect;
                    m_sSoc_Extra_Equip_Shose = item.m_sSoc_Effect;
                }
                break;
            case E_ITEM_EQUIP_TYPE.GLOVES:
                {
                    m_sStatus_Extra_Equip_Gloves = item.m_sStatus_Effect;
                    m_sSoc_Extra_Equip_Gloves = item.m_sSoc_Effect;
                }
                break;
            case E_ITEM_EQUIP_TYPE.MAINWEAPON:
                {
                    m_sStatus_Extra_Equip_Mainweapon = item.m_sStatus_Effect;
                    m_sSoc_Extra_Equip_Mainweapon = item.m_sSoc_Effect;
                }
                break;
            case E_ITEM_EQUIP_TYPE.SUBWEAPON:
                {
                    m_sStatus_Extra_Equip_Subweapon = item.m_sStatus_Effect;
                    m_sSoc_Extra_Equip_Subweapon = item.m_sSoc_Effect;
                }
                break;
        }

        UpdateStatus_Equip();
        UpdateSOC();

        return true;
    }

    // 장비 해제
    public void Remove_Item_Equip(Item_Equip item)
    {
        m_sStatus_Extra_ItemSetEffect.SetSTATUS_Zero();
        m_sSoc_Extra_ItemSetEffect.SetSOC_Zero();
        switch (item.m_eItemEquipType)
        {
            case E_ITEM_EQUIP_TYPE.HAT:
                {
                    m_sStatus_Extra_Equip_Hat.SetSTATUS(m_sStatus_Null);
                    m_sSoc_Extra_Equip_Hat.SetSOC(m_sSoc_Null);
                }
                break;
            case E_ITEM_EQUIP_TYPE.TOP:
                {
                    m_sStatus_Extra_Equip_Top.SetSTATUS(m_sStatus_Null);
                    m_sSoc_Extra_Equip_Top.SetSOC(m_sSoc_Null);
                }
                break;
            case E_ITEM_EQUIP_TYPE.BOTTOMS:
                {
                    m_sStatus_Extra_Equip_Bottoms.SetSTATUS(m_sStatus_Null);
                    m_sSoc_Extra_Equip_Bottoms.SetSOC(m_sSoc_Null);
                }
                break;
            case E_ITEM_EQUIP_TYPE.SHOSE:
                {
                    m_sStatus_Extra_Equip_Shose.SetSTATUS(m_sStatus_Null);
                    m_sSoc_Extra_Equip_Shose.SetSOC(m_sSoc_Null);
                }
                break;
            case E_ITEM_EQUIP_TYPE.GLOVES:
                {
                    m_sStatus_Extra_Equip_Gloves.SetSTATUS(m_sStatus_Null);
                    m_sSoc_Extra_Equip_Gloves.SetSOC(m_sSoc_Null);
                }
                break;
            case E_ITEM_EQUIP_TYPE.MAINWEAPON:
                {
                    m_sStatus_Extra_Equip_Mainweapon.SetSTATUS(m_sStatus_Null);
                    m_sSoc_Extra_Equip_Mainweapon.SetSOC(m_sSoc_Null);
                }
                break;
            case E_ITEM_EQUIP_TYPE.SUBWEAPON:
                {
                    m_sStatus_Extra_Equip_Subweapon.SetSTATUS(m_sStatus_Null);
                    m_sSoc_Extra_Equip_Subweapon.SetSOC(m_sSoc_Null);
                }
                break;
        }

        UpdateStatus_Equip();
        UpdateSOC();
    }

    // 소비 아이템 사용 조건 체크
    // return 0: 아이템 사용.
    // return 1: 아이템 사용 조건 불만족. - STATUS, SOC
    // 
    public int CheckCondition_Item_Use(Item_Use item)
    {
        if (m_sStatus.CheckCondition_Max(item.m_sStatus_Limit_Max) == false)
        {
            Debug.Log(item.m_sItemName + ": Status 최대 조건 불만족");
            return 1;
        }
        if (m_sStatus.CheckCondition_Min(item.m_sStatus_Limit_Min) == false)
        {
            Debug.Log(item.m_sItemName + ": Status 최소 조건 불만족");
            return 1;
        }
        if (m_sSoc.CheckCondition_Max(item.m_sSoc_Limit_Max) == false)
        {
            Debug.Log(item.m_sItemName + ": Soc 최대 조건 불만족");
            return 1;
        }
        if (m_sSoc.CheckCondition_Min(item.m_sSoc_Limit_Min) == false)
        {
            Debug.Log(item.m_sItemName + ": Soc 최소 조건 불만족");
            return 1;
        }

        return 0;
    }

    public float Return_AttackSpeed()
    {
        return m_sStatus.GetSTATUS_AttackSpeed();
    }


    // 소비 아이템 사용에 관한 버프 적용
    // ApplyPotion == 0: 적용.
    // ApplyPotion == 1 ~ 9: 회복 포션.
    // ApplyPotion == 1: 쿨타임 적용중이라 적용 안됨.
    // ApplyPotion == 2: 회복 포션 적용.
    //
    // ApplyPotion == 10 ~ 19: 일시적 버프 포션.
    // ApplyPotion == 10: 버프 포션 쿨타임 적용중이라 적용 안됨.
    // ApplyPotion == 11: 이미 버프 포션 적용 중일때 사용.
    // ApplyPotion == 12: 버프 포션 적용.
    // 
    // ApplyPotion == 20 ~ 29: 영구적 스탯 변화 포션.
    //
    //
    //
    public int ApplyPotion(Item_Use item)
    {
        Coroutine coroutine_buff;
        Coroutine coroutine_cooltime;

        if (item.m_eItemUseType == E_ITEM_USE_TYPE.RECOVERPOTION)
        {
            //for (int i = 0; i < m_List_Coroutine_Item_Use_CoolTime.Count; i++)
            //{
            //    // 아이템 쿨타임일때.
            //    if (m_List_Item_Use_CoolTime[i].m_nItemCode == item.m_nItemCode)
            //    {
            //        GUIManager_Total.Instance.UpdateLog("[소비아이템][" + item.m_sItemName + "] 사용 불가.(쿨타임)");
            //        return 1;
            //    }
            //}
            //foreach (KeyValuePair<int, Item_Use> ditem in m_Dictionary_Item_Use_CoolTime)
            //{
            //    // Item_Use: CoolTime!
            //    if (ditem.Value.m_nItemCode == item.m_nItemCode)
            //    {
            //        GUIManager_Total.Instance.UpdateLog("[소비아이템][" + item.m_sItemName + "] 사용 불가.(쿨타임)");
            //        return 1;
            //    }
            //}
            if (m_Dictionary_Item_Use_CoolTime.ContainsKey(item.m_nItemCode) == true)
            {
                GUIManager_Total.Instance.UpdateLog("[소비아이템][" + item.m_sItemName + "] 사용 불가.(쿨타임)");
                return 1;
            }

            UpdateStatus_Item_Use_Recover(item.m_sStatus_Effect);

            //if (item.m_fCoolTime > 0)
            //{
            //    m_List_Item_Use_CoolTime.Add(item);
            //    coroutine_cooltime = StartCoroutine(Process_Item_Use_CoolTime(item));
            //    m_List_Coroutine_Item_Use_CoolTime.Add(coroutine_cooltime);
            //}
            if (item.m_fCoolTime > 0)
            {
                m_Dictionary_Item_Use_CoolTime.Add(item.m_nItemCode, item);
                coroutine_cooltime = StartCoroutine(Process_Item_Use_CoolTime(item));
                m_Dictionary_Coroutine_Item_Use_CoolTime.Add(item.m_nItemCode, coroutine_cooltime);
            }

            GUIManager_Total.Instance.UpdateLog("[소비아이템][" + item.m_sItemName + "] 회복 효과 적용.");
            return 0;
        }

        if (item.m_eItemUseType == E_ITEM_USE_TYPE.ETERNALBUFFPOTION)
        {
            //for (int i = 0; i < m_List_Coroutine_Item_Use_CoolTime.Count; i++)
            //{
            //    // 아이템 쿨타임일때.
            //    if (m_List_Item_Use_CoolTime[i].m_nItemCode == item.m_nItemCode)
            //    {
            //        GUIManager_Total.Instance.UpdateLog("[소비아이템][" + item.m_sItemName + "] 사용 불가.(쿨타임)");
            //        return 1;
            //    }
            //}
            //foreach (KeyValuePair<int, Item_Use> ditem in m_Dictionary_Item_Use_CoolTime)
            //{
            //    if (ditem.Value.m_nItemCode == item.m_nItemCode)
            //    {
            //        GUIManager_Total.Instance.UpdateLog("[소비아이템][" + item.m_sItemName + "] 사용 불가.(쿨타임)");
            //        return 1;
            //    }
            //}
            if (m_Dictionary_Item_Use_CoolTime.ContainsKey(item.m_nItemCode) == true)
            {
                GUIManager_Total.Instance.UpdateLog("[소비아이템][" + item.m_sItemName + "] 사용 불가.(쿨타임)");
                return 1;
            }

            UpdateStatus_Item_Use_EternalBuff(item.m_sStatus_Effect, item.m_sSoc_Effect);

            //if (item.m_fCoolTime > 0)
            //{
            //    m_List_Item_Use_CoolTime.Add(item);
            //    coroutine_cooltime = StartCoroutine(Process_Item_Use_CoolTime(item));
            //    m_List_Coroutine_Item_Use_CoolTime.Add(coroutine_cooltime);
            //}
            if (item.m_fCoolTime > 0)
            {
                m_Dictionary_Item_Use_CoolTime.Add(item.m_nItemCode, item);
                coroutine_cooltime = StartCoroutine(Process_Item_Use_CoolTime(item));
                m_Dictionary_Coroutine_Item_Use_CoolTime.Add(item.m_nItemCode, coroutine_cooltime);
            }

            GUIManager_Total.Instance.UpdateLog("[소비아이템][" + item.m_sItemName + "] 영구 스탯 변화 적용.");
            return 0;
        }

        if (item.m_eItemUseType == E_ITEM_USE_TYPE.TEMPORARYBUFFPOTION)
        {
            //for (int i = 0; i < m_List_Coroutine_Item_Use_CoolTime.Count; i++)
            //{
            //    // 아이템 쿨타임일때.
            //    if (m_List_Item_Use_CoolTime[i].m_nItemCode == item.m_nItemCode)
            //    {
            //        GUIManager_Total.Instance.UpdateLog("[소비아이템][" + item.m_sItemName + "] 사용 불가.(쿨타임)");
            //        return 1;
            //    }
            //}
            if (m_Dictionary_Item_Use_CoolTime.ContainsKey(item.m_nItemCode) == true)
            {
                GUIManager_Total.Instance.UpdateLog("[소비아이템][" + item.m_sItemName + "] 사용 불가.(쿨타임)");
                return 1;
            }
            //for (int i = 0; i < m_List_Item_Use_Buff.Count; i++)
            //{
            //    // 같은 아이템 코드를 가진 버프 포션은 중복해서 적용이 불가.
            //    // 단순 지속시간 초기화는 가능.
            //    if (m_List_Item_Use_Buff[i].m_nItemCode == item.m_nItemCode)
            //    {
            //        m_List_Item_Use_Buff.RemoveAt(i);
            //        //m_fList_Item_Use_Buff_RemainingTime.RemoveAt(i);
            //        StopCoroutine(m_List_Coroutine_Item_Use_Buff[i]);
            //        m_List_Coroutine_Item_Use_Buff.RemoveAt(i);

            //        m_List_Item_Use_Buff.Add(item);
            //        //m_fList_Item_Use_Buff_RemainingTime.Add(item.m_fDurationTime);
            //        coroutine_buff = StartCoroutine(Process_Item_Use_BuffTime(item));
            //        m_List_Coroutine_Item_Use_Buff.Add(coroutine_buff);
            //        if (item.m_fCoolTime > 0)
            //        {
            //            m_List_Item_Use_CoolTime.Add(item);
            //            coroutine_cooltime = StartCoroutine(Process_Item_Use_CoolTime(item));
            //            m_List_Coroutine_Item_Use_CoolTime.Add(coroutine_cooltime);
            //        }

            //        GUIManager_Total.Instance.UpdateLog("[소비아이템][" + item.m_sItemName + "] 버프 효과 적용. (지속시간 초기화)");
            //        return 0;
            //    }
            //}
            //foreach (KeyValuePair<int, Item_Use> ditem in m_Dictionary_Item_Use_Buff)
            //{
            //    // 같은 아이템 코드를 가진 버프 포션은 중복해서 적용이 불가.
            //    // 단순 지속시간 초기화는 가능.
            //    if (ditem.Value.m_nItemCode == item.m_nItemCode)
            //    {
            //        m_Dictionary_Item_Use_Buff.Remove(ditem.Key); Debug.Log("!!!");
            //        StopCoroutine(m_Dictionary_Coroutine_Item_Use_Buff[ditem.Key]);
            //        m_Dictionary_Coroutine_Item_Use_Buff.Remove(ditem.Key);

            //        m_Dictionary_Item_Use_Buff.Add(item.m_nItemCode, item);
            //        coroutine_buff = StartCoroutine(Process_Item_Use_BuffTime(item));
            //        //m_Dictionary_Coroutine_Ite
            //        //    m_Use_Buff.Add(item.m_nItemCode, coroutine_buff);
            //        if (item.m_fCoolTime > 0)
            //        {
            //            m_Dictionary_Item_Use_CoolTime.Add(item.m_nItemCode, item);
            //            coroutine_cooltime = StartCoroutine(Process_Item_Use_CoolTime(item));
            //            m_Dictionary_Coroutine_Item_Use_CoolTime.Add(item.m_nItemCode, coroutine_cooltime);
            //        }

            //        GUIManager_Total.Instance.UpdateLog("[소비아이템][" + item.m_sItemName + "] 버프 효과 적용. (지속시간 초기화)");
            //        return 0;
            //    }
            //}
            if (m_Dictionary_Item_Use_Buff.ContainsKey(item.m_nItemCode) == true)
            {
                m_Dictionary_Item_Use_Buff.Remove(item.m_nItemCode);
                StopCoroutine(m_Dictionary_Coroutine_Item_Use_Buff[item.m_nItemCode]);
                m_Dictionary_Coroutine_Item_Use_Buff.Remove(item.m_nItemCode);
                m_Dictionary_Item_Use_Buff_RemainingTime.Remove(item.m_nItemCode);

                m_Dictionary_Item_Use_Buff.Add(item.m_nItemCode, item);
                coroutine_buff = StartCoroutine(Process_Item_Use_BuffTime(item));
                m_Dictionary_Coroutine_Item_Use_Buff.Add(item.m_nItemCode, coroutine_buff);

                if (item.m_fCoolTime > 0)
                {
                    m_Dictionary_Item_Use_CoolTime.Add(item.m_nItemCode, item);
                    coroutine_cooltime = StartCoroutine(Process_Item_Use_CoolTime(item));
                    m_Dictionary_Coroutine_Item_Use_CoolTime.Add(item.m_nItemCode, coroutine_cooltime);
                }

                GUIManager_Total.Instance.UpdateLog("[소비아이템][" + item.m_sItemName + "] 버프 효과 적용. (지속시간 초기화)");
                return 0;
            }

            m_Dictionary_Item_Use_Buff.Add(item.m_nItemCode, item);
            coroutine_buff = StartCoroutine(Process_Item_Use_BuffTime(item));
            m_Dictionary_Coroutine_Item_Use_Buff.Add(item.m_nItemCode, coroutine_buff);
            m_Dictionary_Item_Use_Buff_RemainingTime.Remove(item.m_nItemCode);
            if (item.m_fCoolTime > 0)
            {
                m_Dictionary_Item_Use_CoolTime.Add(item.m_nItemCode, item);
                coroutine_cooltime = StartCoroutine(Process_Item_Use_CoolTime(item));
                m_Dictionary_Coroutine_Item_Use_CoolTime.Add(item.m_nItemCode, coroutine_cooltime);
            }

            GUIManager_Total.Instance.UpdateLog("[소비아이템][" + item.m_sItemName + "] 버프 효과 적용.");
            return 0;
        }

        return 0;
    }
    public int Load_ApplyPotion(Item_Use item, float duration, float cool)
    {
        Coroutine coroutine_buff;
        Coroutine coroutine_cooltime;

        if (item.m_eItemUseType == E_ITEM_USE_TYPE.RECOVERPOTION)
        {
            //if (m_Dictionary_Item_Use_CoolTime.ContainsKey(item.m_nItemCode) == true)
            //{
            //    GUIManager_Total.Instance.UpdateLog("[소비아이템][" + item.m_sItemName + "] 사용 불가.(쿨타임)");
            //    return 1;
            //}

            //UpdateStatus_Item_Use_Recover(item.m_sStatus_Effect);

            if (cool > 0)
            {
                m_Dictionary_Item_Use_CoolTime.Add(item.m_nItemCode, item);
                coroutine_cooltime = StartCoroutine(Process_Item_Use_CoolTime(item, cool));
                m_Dictionary_Coroutine_Item_Use_CoolTime.Add(item.m_nItemCode, coroutine_cooltime);
            }

            //GUIManager_Total.Instance.UpdateLog("[소비아이템][" + item.m_sItemName + "] 회복 효과 적용.");
            GUIManager_Total.Instance.Update_SS();
            GUIManager_Total.Instance.Update_Itemslot();
            GUIManager_Total.Instance.Update_Equipslot();
            return 0;
        }

        if (item.m_eItemUseType == E_ITEM_USE_TYPE.ETERNALBUFFPOTION)
        {
            //if (m_Dictionary_Item_Use_CoolTime.ContainsKey(item.m_nItemCode) == true)
            //{
            //    GUIManager_Total.Instance.UpdateLog("[소비아이템][" + item.m_sItemName + "] 사용 불가.(쿨타임)");
            //    return 1;
            //}

            //UpdateStatus_Item_Use_EternalBuff(item.m_sStatus_Effect, item.m_sSoc_Effect);

            if (cool > 0)
            {
                m_Dictionary_Item_Use_CoolTime.Add(item.m_nItemCode, item);
                coroutine_cooltime = StartCoroutine(Process_Item_Use_CoolTime(item, cool));
                m_Dictionary_Coroutine_Item_Use_CoolTime.Add(item.m_nItemCode, coroutine_cooltime);
            }

            //GUIManager_Total.Instance.UpdateLog("[소비아이템][" + item.m_sItemName + "] 영구 스탯 변화 적용.");
            GUIManager_Total.Instance.Update_SS();
            GUIManager_Total.Instance.Update_Itemslot();
            GUIManager_Total.Instance.Update_Equipslot();
            return 0;
        }

        if (item.m_eItemUseType == E_ITEM_USE_TYPE.TEMPORARYBUFFPOTION)
        {
            //if (m_Dictionary_Item_Use_CoolTime.ContainsKey(item.m_nItemCode) == true)
            //{
            //    GUIManager_Total.Instance.UpdateLog("[소비아이템][" + item.m_sItemName + "] 사용 불가.(쿨타임)");
            //    return 1;
            //}

            //if (m_Dictionary_Item_Use_Buff.ContainsKey(item.m_nItemCode) == true)
            //{
            //    m_Dictionary_Item_Use_Buff.Remove(item.m_nItemCode);
            //    StopCoroutine(m_Dictionary_Coroutine_Item_Use_Buff[item.m_nItemCode]);
            //    m_Dictionary_Coroutine_Item_Use_Buff.Remove(item.m_nItemCode);
            //    m_Dictionary_Item_Use_Buff_RemainingTime.Remove(item.m_nItemCode);

            //    m_Dictionary_Item_Use_Buff.Add(item.m_nItemCode, item);
            //    coroutine_buff = StartCoroutine(Process_Item_Use_BuffTime(item));
            //    m_Dictionary_Coroutine_Item_Use_Buff.Add(item.m_nItemCode, coroutine_buff);

            //    if (item.m_fCoolTime > 0)
            //    {
            //        m_Dictionary_Item_Use_CoolTime.Add(item.m_nItemCode, item);
            //        coroutine_cooltime = StartCoroutine(Process_Item_Use_CoolTime(item));
            //        m_Dictionary_Coroutine_Item_Use_CoolTime.Add(item.m_nItemCode, coroutine_cooltime);
            //    }

            //    GUIManager_Total.Instance.UpdateLog("[소비아이템][" + item.m_sItemName + "] 버프 효과 적용. (지속시간 초기화)");
            //    return 0;
            //}

            m_Dictionary_Item_Use_Buff.Add(item.m_nItemCode, item);
            coroutine_buff = StartCoroutine(Process_Item_Use_BuffTime(item, duration));
            m_Dictionary_Coroutine_Item_Use_Buff.Add(item.m_nItemCode, coroutine_buff);
            m_Dictionary_Item_Use_Buff_RemainingTime.Remove(item.m_nItemCode);
            if (cool > 0)
            {
                m_Dictionary_Item_Use_CoolTime.Add(item.m_nItemCode, item);
                coroutine_cooltime = StartCoroutine(Process_Item_Use_CoolTime(item, cool));
                m_Dictionary_Coroutine_Item_Use_CoolTime.Add(item.m_nItemCode, coroutine_cooltime);
            }

            GUIManager_Total.Instance.Update_SS();
            GUIManager_Total.Instance.Update_Itemslot();
            GUIManager_Total.Instance.Update_Equipslot();
            GUIManager_Total.Instance.UpdateLog("[소비아이템][" + item.m_sItemName + "] 버프 효과 적용.");
            return 0;
        }

        return 0;
    }

    IEnumerator Process_Item_Use_BuffTime(Item_Use item)
    {
        UpdateStatus_Item_Use_TemporaryBuff();
        GUIManager_Total.Instance.Update_SS();

        float durationtime = item.m_fDurationTime;

        m_Dictionary_Item_Use_Buff_RemainingTime.Add(item.m_nItemCode, durationtime);
        while (durationtime > 0)
        {
            yield return new WaitForSeconds(0.016f);
            durationtime -= 0.016f;
            m_Dictionary_Item_Use_Buff_RemainingTime[item.m_nItemCode] = durationtime;
        }

        //yield return new WaitForSeconds(item.m_fDurationTime);

        //for (int i = 0; i < m_List_Item_Use_Buff.Count; i++)
        //{
        //    if (item.m_nItemCode == m_List_Item_Use_Buff[i].m_nItemCode)
        //    {
        //        m_List_Item_Use_Buff.RemoveAt(i);
        //        m_List_Coroutine_Item_Use_Buff.RemoveAt(i);
        //        //m_fList_Item_Use_Buff_RemainingTime.RemoveAt(i);

        //        break;
        //    }
        //}
        m_Dictionary_Item_Use_Buff.Remove(item.m_nItemCode);
        m_Dictionary_Coroutine_Item_Use_Buff.Remove(item.m_nItemCode);
        m_Dictionary_Item_Use_Buff_RemainingTime.Remove(item.m_nItemCode);

        UpdateStatus_Item_Use_TemporaryBuff();
        GUIManager_Total.Instance.Update_SS();
    }
    IEnumerator Process_Item_Use_BuffTime(Item_Use item, float duration)
    {
        UpdateStatus_Item_Use_TemporaryBuff();
        GUIManager_Total.Instance.Update_SS();

        float durationtime = duration;

        m_Dictionary_Item_Use_Buff_RemainingTime.Add(item.m_nItemCode, durationtime);
        while (durationtime > 0)
        {
            yield return new WaitForSeconds(0.1f);
            durationtime -= 0.1f;
            m_Dictionary_Item_Use_Buff_RemainingTime[item.m_nItemCode] = durationtime;
        }

        //yield return new WaitForSeconds(item.m_fDurationTime);

        //for (int i = 0; i < m_List_Item_Use_Buff.Count; i++)
        //{
        //    if (item.m_nItemCode == m_List_Item_Use_Buff[i].m_nItemCode)
        //    {
        //        m_List_Item_Use_Buff.RemoveAt(i);
        //        m_List_Coroutine_Item_Use_Buff.RemoveAt(i);
        //        //m_fList_Item_Use_Buff_RemainingTime.RemoveAt(i);

        //        break;
        //    }
        //}
        m_Dictionary_Item_Use_Buff.Remove(item.m_nItemCode);
        m_Dictionary_Coroutine_Item_Use_Buff.Remove(item.m_nItemCode);
        m_Dictionary_Item_Use_Buff_RemainingTime.Remove(item.m_nItemCode);

        UpdateStatus_Item_Use_TemporaryBuff();
        GUIManager_Total.Instance.Update_SS();
    }

    IEnumerator Process_Item_Use_CoolTime(Item_Use item)
    {
        float cooltime = item.m_fCoolTime;

        m_Dictionary_Item_Use_CoolTime_RemainingTime.Add(item.m_nItemCode, cooltime);
        while (cooltime > 0)
        {
            yield return new WaitForSeconds(0.1f);
            cooltime -= 0.1f;
            m_Dictionary_Item_Use_CoolTime_RemainingTime[item.m_nItemCode] = cooltime;
        }

        //yield return new WaitForSeconds(item.m_fCoolTime);

        //for (int i = 0; i < m_List_Item_Use_CoolTime.Count; i++)
        //{
        //    if (item.m_nItemCode == m_List_Item_Use_CoolTime[i].m_nItemCode)
        //    {
        //        m_List_Item_Use_CoolTime.RemoveAt(i);
        //        m_List_Coroutine_Item_Use_CoolTime.RemoveAt(i);

        //        break;
        //    }
        //}
        m_Dictionary_Item_Use_CoolTime.Remove(item.m_nItemCode);
        m_Dictionary_Coroutine_Item_Use_CoolTime.Remove(item.m_nItemCode);
        m_Dictionary_Item_Use_CoolTime_RemainingTime.Remove(item.m_nItemCode);
    }
    IEnumerator Process_Item_Use_CoolTime(Item_Use item, float cool)
    {
        float cooltime = cool;

        m_Dictionary_Item_Use_CoolTime_RemainingTime.Add(item.m_nItemCode, cooltime);
        while (cooltime > 0)
        {
            yield return new WaitForSeconds(0.016f);
            cooltime -= 0.016f;
            m_Dictionary_Item_Use_CoolTime_RemainingTime[item.m_nItemCode] = cooltime;
        }

        //yield return new WaitForSeconds(item.m_fCoolTime);

        //for (int i = 0; i < m_List_Item_Use_CoolTime.Count; i++)
        //{
        //    if (item.m_nItemCode == m_List_Item_Use_CoolTime[i].m_nItemCode)
        //    {
        //        m_List_Item_Use_CoolTime.RemoveAt(i);
        //        m_List_Coroutine_Item_Use_CoolTime.RemoveAt(i);

        //        break;
        //    }
        //}
        m_Dictionary_Item_Use_CoolTime.Remove(item.m_nItemCode);
        m_Dictionary_Coroutine_Item_Use_CoolTime.Remove(item.m_nItemCode);
        m_Dictionary_Item_Use_CoolTime_RemainingTime.Remove(item.m_nItemCode);
    }


    // 리팩토링 필요.
    // SkillEffect(버프, 디버프, 상태이상) 적용.
    public bool ApplySkill(Skill skill)
    {
        m_List_Skill.Add(skill);
        m_sStatus.P_OperatorSTATUS(skill.m_seSkillEffect.m_sStatus_Effect_Eternal);
        m_sSoc_Origin.P_OperatorSOC(skill.m_seSkillEffect.m_sSoc_Effect_Eternal);
        UpdateSOC();

        bool Check = false;
        // 적용 Skill 에 Bind 효과가 있는지 없는지 검사.
        if (skill.m_seSkillEffect.m_cCondition.ConditionCheck_Bind() == true)
        {
            m_cCondition.AddBindTime(skill.m_seSkillEffect.m_cCondition.GetBindTime());
            ApplySkillEffect_Bind(skill);
            Check = true;
        }
        if (skill.m_seSkillEffect.m_cCondition.ConditionCheck_Shock() == true)
        {
            m_cCondition.AddShockTime(skill.m_seSkillEffect.m_cCondition.GetShockTime());
            ApplySkillEffect_Shock(skill);
            Check = true;
        }
        if (skill.m_seSkillEffect.m_cCondition.ConditionCheck_Dark() == true)
        {
            if (m_cCondition.GetDarkRatio() == skill.m_seSkillEffect.m_cCondition.GetDarkRatio())
            {
                ApplySkillEffect_Dark(skill);
                m_cCondition.AddDarkTime(skill.m_seSkillEffect.m_cCondition.GetDarkTime());
                Check = true;
            }
            else if (m_cCondition.GetDarkRatio() > skill.m_seSkillEffect.m_cCondition.GetDarkRatio())
            {
                Check = false;
            }
            else
            {
                m_cCondition.SetDarkTime(skill.m_seSkillEffect.m_cCondition.GetDarkTime());
                m_cCondition.SetDarkRatio(skill.m_seSkillEffect.m_cCondition.GetDarkRatio());
                ApplySkillEffect_Dark(skill);
                Check = true;
            }
        }
        if (skill.m_seSkillEffect.m_cCondition.ConditionCheck_Slow() == true)
        {
            if (m_cCondition.GetSlowRatio() == skill.m_seSkillEffect.m_cCondition.GetSlowRatio())
            {
                m_cCondition.AddSlowTime(skill.m_seSkillEffect.m_cCondition.GetSlowTime());
                ApplySkillEffect_Slow(skill);
                Check = true;
            }
            else if (m_cCondition.GetSlowRatio() > skill.m_seSkillEffect.m_cCondition.GetSlowRatio())
            {
                Check = false;
            }
            else
            {
                m_cCondition.SetSlowTime(skill.m_seSkillEffect.m_cCondition.GetSlowTime());
                m_cCondition.SetSlowRatio(skill.m_seSkillEffect.m_cCondition.GetSlowRatio());
                ApplySkillEffect_Slow(skill);
                Check = true;
            }
        }
        if (skill.m_seSkillEffect.m_cCondition.ConditionCheck_Confuse() == true)
        {
            m_cCondition.AddConfuseTime(skill.m_seSkillEffect.m_cCondition.GetConfuseTime());
            ApplySkillEffect_Confuse(skill);
            Check = true;
        }

        UpdateStatus_ApplySkill();
        UpdateSoc_ApplySkill();

        // SkillEffect 적용 시 true, 미 적용 시 false 반환.
        if (Check == true)
            return true;
        else
            return false;
    }

    // 해제된 스킬을 List에서 제거.
    void RemoveSkill(Skill skill)
    {
        for (int i = 0; i < m_List_Skill.Count; i++)
        {
            if (m_List_Skill[i].m_nSkillCode == skill.m_nSkillCode)
            {
                m_List_Skill.RemoveAt(i);
                break;
            }
        }
    }

    // Bind(속박)
    public void ApplySkillEffect_Bind(Skill skill)
    {
        if (m_cCondition.GetBindTime() > 0)
        {
            if (m_cProcessBind == null)
            {
                m_cProcessBind = StartCoroutine(ProcessBind(skill));
            }
            else
            {
                StopCoroutine(m_cProcessBind);
                m_cProcessBind = StartCoroutine(ProcessBind(skill));
            }
        }
    }
    Coroutine m_cProcessBind;
    IEnumerator ProcessBind(Skill skill)
    {
        m_sStatus.SetSTATUS_Speed(0);
        GUIManager_Total.Instance.Update_SS();
        m_cCondition.SetBindCondition(true);
        m_gCondition_Bind.SetActive(true);
        while (m_cCondition.GetBindTime() > 0)
        {
            m_cCondition.AddBindTime(-Time.deltaTime);
            //Debug.Log(m_cCondition.GetBindTime());
            yield return null;
        }
        m_cCondition.SetBindCondition(false);
        m_gCondition_Bind.SetActive(false);
        RemoveSkill(skill);
        UpdateStatus_ApplySkill();
        GUIManager_Total.Instance.Update_SS();
        UpdateSoc_ApplySkill();
        m_cCondition.SetBindTime(0);
        m_cProcessBind = null;
    }

    // Shock(기절)
    public void ApplySkillEffect_Shock(Skill skill)
    {
        if (m_cCondition.GetShockTime() > 0)
        {
            if (m_cProcessShock == null)
            {
                m_cProcessShock = StartCoroutine(ProcessShock(skill));
            }
            else
            {
                StopCoroutine(m_cProcessShock);
                m_cProcessShock = StartCoroutine(ProcessShock(skill));
            }
        }
    }
    Coroutine m_cProcessShock;
    IEnumerator ProcessShock(Skill skill)
    {
        GUIManager_Total.Instance.Update_SS();
        m_cCondition.SetShockCondition(true);
        m_gCondition_Shock.SetActive(true);
        while (m_cCondition.GetShockTime() > 0)
        {
            m_cCondition.AddShockTime(-Time.deltaTime);
            //Debug.Log(m_cCondition.GetDarkTime());
            yield return null;
        }
        m_cCondition.SetShockCondition(false);
        m_gCondition_Shock.SetActive(false);
        RemoveSkill(skill);
        UpdateStatus_ApplySkill();
        UpdateSoc_ApplySkill();
        GUIManager_Total.Instance.Update_SS();
        m_cCondition.SetShockTime(0);
        m_cProcessShock = null;
    }

    // Dark(암흑)
    public void ApplySkillEffect_Dark(Skill skill)
    {
        if (m_cCondition.GetDarkTime() > 0)
        {
            if (m_cProcessDark == null)
            {
                m_cProcessDark = StartCoroutine(ProcessDark(skill));
            }
            else
            {

            }
        }
    }
    Coroutine m_cProcessDark;
    IEnumerator ProcessDark(Skill skill)
    {
        GUIManager_Total.Instance.Update_SS();
        m_cCondition.SetDarkCondition(true);
        m_gCondition_Dark.SetActive(true);
        while (m_cCondition.GetDarkTime() > 0)
        {
            m_cCondition.AddDarkTime(-Time.deltaTime);
            //Debug.Log(m_cCondition.GetDarkTime());
            yield return null;
        }
        m_cCondition.SetDarkCondition(false);
        m_gCondition_Dark.SetActive(false);
        RemoveSkill(skill);
        UpdateStatus_ApplySkill();
        UpdateSoc_ApplySkill();
        GUIManager_Total.Instance.Update_SS();
        m_cCondition.SetDarkTime(0);
        m_cCondition.SetDarkRatio(0);
        m_cProcessDark = null;
    }

    // Slow(둔화)
    public void ApplySkillEffect_Slow(Skill skill)
    {
        if (m_cCondition.GetSlowTime() > 0)
        {
            if (m_cProcessSlow == null)
            {
                m_cProcessSlow = StartCoroutine(ProcessSlow(skill));
            }
            else
            {

            }
        }
    }
    Coroutine m_cProcessSlow;
    IEnumerator ProcessSlow(Skill skill)
    {
        m_sStatus.SetSTATUS_Speed((int)((float)m_sStatus.GetSTATUS_Speed() * ((100 - skill.m_seSkillEffect.m_cCondition.GetSlowRatio()) / 100)));
        GUIManager_Total.Instance.Update_SS();
        m_cCondition.SetSlowCondition(true);
        m_gCondition_Slow.SetActive(true);
        while (m_cCondition.GetSlowTime() > 0)
        {
            m_cCondition.AddSlowTime(-Time.deltaTime);
            //Debug.Log(m_cCondition.GetDarkTime());
            yield return null;
        }
        m_cCondition.SetSlowCondition(false);
        m_gCondition_Slow.SetActive(false);
        RemoveSkill(skill);
        UpdateStatus_ApplySkill();
        UpdateSoc_ApplySkill();
        GUIManager_Total.Instance.Update_SS();
        m_cCondition.SetSlowTime(0);
        m_cCondition.SetSlowRatio(0);
        m_cProcessSlow = null;
    }

    // Confuse(혼돈)
    public void ApplySkillEffect_Confuse(Skill skill)
    {
        if (m_cCondition.GetConfuseTime() > 0)
        {
            if (m_cProcessConfuse == null)
            {
                m_cProcessConfuse = StartCoroutine(ProcessConfuse(skill));
            }
            else
            {

            }
        }
    }
    Coroutine m_cProcessConfuse;
    IEnumerator ProcessConfuse(Skill skill)
    {
        GUIManager_Total.Instance.Update_SS();
        m_cCondition.SetConfuseCondition(true);
        m_gCondition_Confuse.SetActive(true);
        while (m_cCondition.GetConfuseTime() > 0)
        {
            m_cCondition.AddConfuseTime(-Time.deltaTime);
            //Debug.Log(m_cCondition.GetDarkTime());
            yield return null;
        }
        m_cCondition.SetConfuseCondition(false);
        m_gCondition_Confuse.SetActive(false);
        RemoveSkill(skill);
        UpdateStatus_ApplySkill();
        UpdateSoc_ApplySkill();
        GUIManager_Total.Instance.Update_SS();
        m_cCondition.SetConfuseTime(0);
        m_cProcessConfuse = null;
    }
}

