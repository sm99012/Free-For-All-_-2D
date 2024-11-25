using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Status : MonoBehaviour
{
    // SOC, STATUS 구조체 클래스를 참조해 주세요.
    // 플레이어 평판 관련 스탯
    public SOC m_sSoc;                        // 평판 합계
    public SOC m_sSoc_Origin;                 // 고유 평판(성장 평판 + 영구버프아이템, 퀘스트 완료 보상 등 추가로 획득한 평판)
    public SOC m_sSoc_Extra_Equip_Hat;        // 착용중인 장비아이템(모자) 평판
    public SOC m_sSoc_Extra_Equip_Top;        // 착용중인 장비아이템(상의) 평판
    public SOC m_sSoc_Extra_Equip_Bottoms;    // 착용중인 장비아이템(하의) 평판
    public SOC m_sSoc_Extra_Equip_Shose;      // 착용중인 장비아이템(신발) 평판
    public SOC m_sSoc_Extra_Equip_Gloves;     // 착용중인 장비아이템(장갑) 평판
    public SOC m_sSoc_Extra_Equip_Mainweapon; // 착용중인 장비아이템(주무기) 평판
    public SOC m_sSoc_Extra_Equip_Subweapon;  // 착용중인 장비아이템(보조무기) 평판
    public SOC m_sSoc_Item_Use_Buff;          // 적용중인 소비아이템(일시적 버프포션) 평판 합계
    public SOC m_sSoc_Extra_ItemSetEffect;    // 적용중인 아이템 세트효과 평판
    public SOC m_sSoc_Null;                   // 각종 평판 계산에 사용되는 초기화 평판

    // 플레이어 능력치 관련 스탯
    public STATUS m_sStatus;                        // 능력치 합계
    public STATUS m_sStatus_Origin;                 // 고유 능력치(성장 능력치 + 영구적 버프포션, 놓아주기, 퀘스트 완료 보상 등 추가로 획득한 능력치)
    public STATUS m_sStatus_Extra_Equip_Hat;        // 착용중인 장비아이템(모자) 능력치
    public STATUS m_sStatus_Extra_Equip_Top;        // 착용중인 장비아이템(상의) 능력치
    public STATUS m_sStatus_Extra_Equip_Bottoms;    // 착용중인 장비아이템(하의) 능력치
    public STATUS m_sStatus_Extra_Equip_Shose;      // 착용중인 장비아이템(신발) 능력치
    public STATUS m_sStatus_Extra_Equip_Gloves;     // 착용중인 장비아이템(장갑) 능력치
    public STATUS m_sStatus_Extra_Equip_Mainweapon; // 착용중인 장비아이템(주무기) 능력치
    public STATUS m_sStatus_Extra_Equip_Subweapon;  // 착용중인 장비아이템(보조무기) 능력치
    public STATUS m_sStatus_Item_Use_Buff;          // 적용중인 소비아이템(일시적 버프포션) 능력치 합계
    public STATUS m_sStatus_Extra_ItemSetEffect;    // 적용중인 아이템 세트효과 능력치
    public STATUS m_sStatus_Null;                   // 각종 능력치 계산에 사용되는 초기화 능력치
    
    // 플레이어에게 적용되고있는 소비아이템 정보
    public static Dictionary <int, Item_Use> m_Dictionary_Item_Use_Buff;                // 소비아이템 효과 딕셔너리(버프 / 디버프). Dictionary <Key : 아이템코드 , Value : 소비아이템 정보>
    public static Dictionary <int, Item_Use> m_Dictionary_Item_Use_CoolTime;            // 소비아이템 쿨타임 딕셔너리. Dictionary <Key : 아이템코드 , Value : 소비아이템 정보>
    public static Dictionary <int, Coroutine> m_Dictionary_Coroutine_Item_Use_Buff;     // 소비아이템 효과(버프 / 디버프) 지속시간(코루틴) 딕셔너리. Dictionary <Key : 아이템코드 , Value : 소비아이템 효과 지속시간 코루틴>
    public static Dictionary <int, Coroutine> m_Dictionary_Coroutine_Item_Use_CoolTime; // 소비아이템 쿨타입(코루틴) 딕셔너리. Dictionary <Key : 아이템코드 , Value : 소비아이템 쿨타임 지속시간 코루틴>
    // 플레이어에게 적용되고있는 소비아이템 효과(버프 / 디버프)의 지속시간, 소비아이템 쿨타임을 저장한다. 유니티에는 특정 코루틴의 잔여 시간을 알 수 있는 방법이 없기에 고안했다.
    public static Dictionary <int, float> m_Dictionary_Item_Use_Buff_RemainingTime;     // 소비아이템 효과(버프 / 디버프) 지속시간(float) 딕셔너리. Dictionary <Key : 아이템코드 , Value : 지속시간>
    public static Dictionary <int, float> m_Dictionary_Item_Use_CoolTime_RemainingTime; // 소비아이템 쿨타임(float) 딕셔너리. Dictionary <Key : 아이템코드 , Value : 쿨타임>
    
    public static Dictionary<int, Skill_SSEffect> m_sDictionary_Skill_SSEffect_Apply;   // 플레이어에게 적용중인 스킬(버프ㆍ디버프) 효과 딕셔너리

    protected Vector3 m_vDamageOffSet = new Vector3(0, 0.2f, 0); // 데미지 출력 오프셋

    public void InitialSet()
    {
        InitialSet_Status();
        InitialSet_SOC();
        InitialSet_Item_Use();

        m_sDictionary_Skill_SSEffect_Apply = new Dictionary<int, Skill_SSEffect>();
    }

    public void InitialSet_Status()
    {
        // 기본 능력치
        m_sStatus_Origin = new STATUS(1, 10, 0, 10, 10, 0, 0, 1, 1, 1, 0, 100, 100, 1, 1, 0, 0.1f);
        // 레벨 : 1, 레벨업 경험치 요구량 : 10, 현재경험치 : 0, 
        // 최대체력 : 10, 현재체력 : 10, 최대마나 : 0, 현재마나 : 0, 
        // 총데미지 : 1, 물리데미지 : 1, 마법데미지 : 1, 
        // 크리티컬 확률 : 0, 크리티컬 데미지 : 100%, 
        // 이동속도 : 100, 
        // 물리방어력 : 1, 마법방어력 : 1,
        // 회피율 : 0,
        // 공격속도 : 0.1f(초)
        m_sStatus_Item_Use_Buff = new STATUS();
        m_sStatus_Extra_ItemSetEffect = new STATUS();
        m_sStatus_Null = new STATUS();
        m_sStatus = new STATUS();
        m_sStatus.SetSTATUS(m_sStatus_Origin);

    }
    public void InitialSet_SOC()
    {
        // 기본 평판
        m_sSoc = new SOC();
        // 명    예 : 0,
        // 인    간 : 0,
        // 동    물 : 0,
        // 슬 라 임 : 0,
        // 스켈레톤 : 0,
        // 앤    트 : 0,
        // 마    족 : 0,
        // 용    족 : 0,
        // 어    둠 : 0
        m_sSoc_Item_Use_Buff = new SOC();
        m_sSoc_Extra_ItemSetEffect = new SOC();
        m_sSoc_Null = new SOC();
        m_sSoc_Origin = new SOC();
        m_sSoc_Origin.SetSOC(m_sSoc);
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

    int m_nTotalDamage; // 피격 데미지
    int m_fTotalDamage; // 피격 데미지
    // 플레이어 피격 시 피격 데미지 계산 및 출력
    // 데미지, 방어력 계산 공식 : 데미지 * (물리방어력 / (10(방어력 계수) + 물리방어력)). ※ 데미지, 방어력 계산 공식은 추후 변경될 수 있다.(단순 데미지 - 방어력 -> 방어력 계수 적용)
    public void Attacked(int damage, Vector3 dir) // damage : 데미지, dir : 넉백 방향
    {
        m_fTotalDamage = (int)((float)damage * ((float)m_sStatus.GetSTATUS_Defence_Physical() / ((float)(10) + (float)m_sStatus.GetSTATUS_Defence_Physical()))); // 피격 데미지 계산(float)
        m_nTotalDamage = damage - (int)Mathf.Round(m_fTotalDamage); // 자료형 변환(float -> int)

        if (m_nTotalDamage <= 0) // 피격 데미지는 1 이상의 값
            m_nTotalDamage = 1;

        // 피격 데미지 적용
        if (m_sStatus.GetSTATUS_HP_Current() - m_nTotalDamage > 0)
            m_sStatus.P_OperatorSTATUS_HP_Current(-m_nTotalDamage);
        else
            m_sStatus.SetSTATUS_HP_Current(0); // 플레이어의 현재체력은 0 보다 작은 값을 가질 수 없다.

        //
        // ※ 오브젝트 풀링 적용 예정
        //
        GameObject obj = Resources.Load("Prefab/GUI/TextMesh_Damage") as GameObject;
        GameObject copyobj = Instantiate(obj);
        copyobj.GetComponent<TextMesh_Damage>().InitialSet(this.transform.position + m_vDamageOffSet, -m_nTotalDamage); // 피격 데미지 출력
    }

    // 플레이어가 놓아주기를 성공 시켰을때 평판 변경
    public void Goaway(SOC soc) // soc : 변경될 평판 정보
    {
        m_sSoc_Origin.P_OperatorSOC(soc); // 평판 변경
        UpdateSOC(); // 평판 업데이트
    }

    int stexp; // 변경될 능력치(경험치)
    int m_nEXP_Current; // 현재경험치
    int m_nHP_Current;  // 현재체력
    int m_nMP_Current;  // 현재마나
    // 경험치 계산 함수
    void CarculateEXP(STATUS status) // status : 변경될 능력치(경험치) 정보
    {
        stexp = status.GetSTATUS_EXP_Current(); // stexp = 추가될 경험치
        stexp += m_nEXP_Current; // stexp = 추가될 경험치 + 현재경험치
        // stexp가 레벨업 경험치 요구량보다 적을때
        if (stexp < m_sStatus.GetSTATUS_EXP_Max())
        {
            m_sStatus.SetSTATUS_EXP_Current(stexp); // 현재경험치를 stexp로 설정
        }
        // stexp가 레벨업 경험치 요구량보다 많을때(중첩 레벨업 가능)
        else
        {
            while (stexp >= m_sStatus.GetSTATUS_EXP_Max()) // 중첩 레벨업
            {
                stexp = stexp - m_sStatus.GetSTATUS_EXP_Max();
                CarculateLV(); // 레벨업
            }
            m_sStatus.SetSTATUS_EXP_Current(stexp); // 현재경험치를 stexp로 설정
        }
        m_nEXP_Current = 0;
    }
    // 레벨업 함수
    void CarculateLV()
    {
        m_sStatus_Origin.P_OperatorSTATUS_LV(1); // 레벨 += 1
        
        // 플레이어의 레벨이 1 오를때 마다 특정 능력치 성장
        m_sStatus_Origin.M_OperatorSTATUS_EXP_Max(1.3f); // 레벨업 경험치 요구량 *= 1.3f
        m_sStatus_Origin.SetSTATUS_EXP_Current(0);
        m_sStatus_Origin.P_OperatorSTATUS_HP_Max(1);     // 최대체력 += 1
        m_sStatus_Origin.P_OperatorSTATUS_MP_Max(1);     // 최대마나 += 1
        // 플레이어의 레벨이 5의 배수를 도달할때 마다 특정 능력치 성장
        if (m_sStatus_Origin.GetSTATUS_LV() % 5 == 0)
        {
            m_sStatus_Origin.P_OperatorSTATUS_Damage_Total(1);     // 총데미지 += 1
            m_sStatus_Origin.P_OperatorSTATUS_Damage_Physical(1);  // 물리데미지 += 1
            m_sStatus_Origin.P_OperatorSTATUS_Damage_Magical(1);   // 마법데미지 += 1
            m_sStatus_Origin.P_OperatorSTATUS_Defence_Physical(1); // 물리방여력 += 1
            m_sStatus_Origin.P_OperatorSTATUS_Defence_Magical(1);  // 마법방어력 += 1
        }

        UpdateStatus_LVup(); // 능력치 업데이트(레벨업)

        m_sStatus.SetSTATUS_HP_Current(m_sStatus.GetSTATUS_HP_Max()); // 현재체력 회복(현재체력 = 최대체력)
        m_sStatus.SetSTATUS_MP_Current(m_sStatus.GetSTATUS_MP_Max()); // 현재마나 회복(현재마나 = 최대마나)

        m_nEXP_Current = m_sStatus.GetSTATUS_EXP_Current();
        m_nHP_Current = m_sStatus.GetSTATUS_HP_Current();
        m_nMP_Current = m_sStatus.GetSTATUS_MP_Current();
    }

    // 스탯(능력치, 평판) 업데이트 함수
    // 레벨업 으로 인한 스탯(능력치, 평판) 변경
    public void UpdateStatus_LVup()
    {
        m_sStatus.SetSTATUS_Zero(); // 능력치 합계 초기화
        
        // 1. 플레이어 고유 능력치 업데이트
        m_sStatus.P_OperatorSTATUS(m_sStatus_Origin);                 // 능력치 합계 += 고유 능력치(성장 능력치 + 영구적 버프포션, 놓아주기, 퀘스트 완료 보상 등 추가로 획득한 능력치)
        // 2. 플레이어에게 적용중인 소비아이템(일시적 버프포션)의 능력치 업데이트
        m_sStatus.P_OperatorSTATUS(m_sStatus_Item_Use_Buff);          // 능력치 합계 += 플레이어에게 적용중인 소비아이템(일시적 버프포션)의 능력치
        // 3. 플레이어가 착용중인 장비아이템의 능력치 업데이트
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Hat);        // 능력치 합계 += 착용중인 장비아이템(모자) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Top);        // 능력치 합계 += 착용중인 장비아이템(상의) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Bottoms);    // 능력치 합계 += 착용중인 장비아이템(하의) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Shose);      // 능력치 합계 += 착용중인 장비아이템(신발) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Gloves);     // 능력치 합계 += 착용중인 장비아이템(장갑) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Mainweapon); // 능력치 합계 += 착용중인 장비아이템(주무기) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Subweapon);  // 능력치 합계 += 착용중인 장비아이템(보조무기) 능력치

        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_ItemSetEffect);    // 능력치 합계 += 적용중인 아이템 세트효과 능력치

        // 스킬(버프ㆍ디버프) 적용으로 인한 일시적 스탯(능력치) 업데이트
        foreach (KeyValuePair<int, Skill_SSEffect> set in m_sDictionary_Skill_SSEffect_Apply) // 플레이어에게 적용중인 모든 스킬(버프ㆍ디버프) 효과 조사
        {
            m_sStatus.P_OperatorSTATUS(set.Value.m_sStatus_Effect_Temporary); // 능력치 합계 += 적용중인 스킬 능력치
	    m_sSoc.P_OperatorSOC(set.Value.m_sSoc_Effect_Temporary);          // 평판 합계 += 적용중인 스킬 평판
        }

        CheckSkill_Condition(); // 스킬(상태이상[속박, 둔화]) 적용으로 인한 스탯(능력치) 업데이트

        CheckLogic(); // 능력치 논리 판단(현재체력, 현재마나)
    }
    
    // 소비아이템(회복포션) 사용으로 인한 스탯(능력치, 평판) 업데이트
    public void UpdateStatus_Item_Use_Recover(STATUS status) // status : 변경될 능력치(현재체력, 현재마나) 정보
    {
        m_sStatus.P_OperatorSTATUS_HP_Current(status.GetSTATUS_HP_Current()); // 현재체력 변경
        m_sStatus.P_OperatorSTATUS_MP_Current(status.GetSTATUS_MP_Current()); // 현재마나 변경

        CheckLogic(); // 능력치 논리 판단(현재체력, 현재마나)
    }
    // 소비아이템(영구적 버프포션) 사용으로 인한 스탯(능력치, 평판) 업데이트
    public void UpdateStatus_Item_Use_EternalBuff(STATUS status, SOC soc) // status : 변경될 능력치 정보, soc : 변경될 평판 정보
    {
        m_sStatus_Origin.P_OperatorSTATUS(status); // 능력치 변경
        m_sSoc_Origin.P_OperatorSOC(soc); // 평판 변경

        UpdateStatus_Equip(); // 능력치 업데이트(착용중인 장비아이템 변경, 소비아이템(영구적 버프포션) 사용)
        UpdateSOC(); // 평판 업데이트
    }
    // 소비아이템(일시적 버프포션) 사용으로 인한 스탯(능력치, 평판) 업데이트
    public void UpdateStatus_Item_Use_TemporaryBuff()
    {
        m_nEXP_Current = m_sStatus.GetSTATUS_EXP_Current(); // 현재경험치 임시 저장
        m_nHP_Current = m_sStatus.GetSTATUS_HP_Current();   // 현재체력 임시 저장
        m_nMP_Current = m_sStatus.GetSTATUS_MP_Current();   // 현재마나 임시 저장

        m_sStatus.SetSTATUS_Zero(); // 능력치 합계 초기화
        m_sSoc.SetSOC_Zero(); // 평판 합계 초기화

        // 1. 플레이어의 고유 능력치 업데이트
        m_sStatus.P_OperatorSTATUS(m_sStatus_Origin); // 능력치 합계 += 고유 능력치(성장 능력치 + 영구적 버프포션, 놓아주기, 퀘스트 완료 보상 등 추가로 획득한 능력치)
        // 2. 플레이어에게 적용중인 소비아이템(일시적 버프포션)의 능력치, 평판 업데이트
        m_sStatus_Item_Use_Buff.SetSTATUS_Zero(); // 적용중인 소비아이템(일시적 버프포션) 능력치 합계 초기화
        m_sSoc_Item_Use_Buff.SetSOC_Zero(); // 적용중인 소비아이템(일시적 버프포션) 평판 합계 초기화
        foreach (KeyValuePair<int, Item_Use> ditem in m_Dictionary_Item_Use_Buff)
        {
            m_sStatus_Item_Use_Buff.P_OperatorSTATUS(ditem.Value.m_sStatus_Effect);
            m_sSoc_Item_Use_Buff.P_OperatorSOC(ditem.Value.m_sSoc_Effect);
        }
        m_sStatus.P_OperatorSTATUS(m_sStatus_Item_Use_Buff); // 능력치 합계 += 플레이어에게 적용중인 소비아이템(일시적 버프포션)의 능력치
        // 3. 플레이어가 착용중인 장비아이템의 능력치 업데이트
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Hat);        // 능력치 합계 += 착용중인 장비아이템(모자) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Top);        // 능력치 합계 += 착용중인 장비아이템(상의) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Bottoms);    // 능력치 합계 += 착용중인 장비아이템(하의) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Shose);      // 능력치 합계 += 착용중인 장비아이템(신발) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Gloves);     // 능력치 합계 += 착용중인 장비아이템(장갑) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Mainweapon); // 능력치 합계 += 착용중인 장비아이템(주무기) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Subweapon);  // 능력치 합계 += 착용중인 장비아이템(보조무기) 능력치

        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_ItemSetEffect);    // 능력치 합계 += 적용중인 아이템 세트효과 능력치

        // 스킬(버프ㆍ디버프) 적용으로 인한 일시적 스탯(능력치) 업데이트
        foreach (KeyValuePair<int, Skill_SSEffect> set in m_sDictionary_Skill_SSEffect_Apply) // 플레이어에게 적용중인 모든 스킬(버프ㆍ디버프) 효과 조사
        {
            m_sStatus.P_OperatorSTATUS(set.Value.m_sStatus_Effect_Temporary); // 능력치 합계 += 적용중인 스킬 능력치
	    m_sSoc.P_OperatorSOC(set.Value.m_sSoc_Effect_Temporary);          // 평판 합계 += 적용중인 스킬 평판
        }

        CheckSkill_Condition(); // 스킬(상태이상[속박, 둔화]) 적용으로 인한 스탯(능력치) 업데이트

        m_sStatus.SetSTATUS_EXP_Current(m_nEXP_Current); // 현재경험치 설정
        m_sStatus.SetSTATUS_HP_Current(m_nHP_Current);   // 현재체력 설정
        m_sStatus.SetSTATUS_MP_Current(m_nMP_Current);   // 현재마나 설정

        CheckLogic(); // 능력치 논리 판단(현재체력, 현재마나)

        UpdateSOC(); // 평판 업데이트
    }

    // 장비아이템 착용ㆍ해제 로인한 능력치 업데이트. 소비아이템(영구적 버프포션) 사용시에도 사용
    public void UpdateStatus_Equip()
    {
        m_nEXP_Current = m_sStatus.GetSTATUS_EXP_Current(); // 현재경험치 임시 저장
        m_nHP_Current = m_sStatus.GetSTATUS_HP_Current();   // 현재체력 임시 저장
        m_nMP_Current = m_sStatus.GetSTATUS_MP_Current();   // 현재마나 임시 저장

        m_sStatus.SetSTATUS_Zero(); // 능력치 합계 초기화

        // 1. 플레이어 고유 능력치 업데이트
        m_sStatus.P_OperatorSTATUS(m_sStatus_Origin);                 // 능력치 합계 += 고유 능력치(성장 능력치 + 영구적 버프포션, 놓아주기, 퀘스트 완료 보상 등 추가로 획득한 능력치)
        // 2. 플레이어에게 적용중인 소비아이템(일시적 버프포션)의 능력치 업데이트
        m_sStatus.P_OperatorSTATUS(m_sStatus_Item_Use_Buff);          // 능력치 합계 += 플레이어에게 적용중인 소비아이템(일시적 버프포션)의 능력치
        // 3. 플레이어가 착용중인 장비아이템의 능력치 업데이트
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Hat);        // 능력치 합계 += 착용중인 장비아이템(모자) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Top);        // 능력치 합계 += 착용중인 장비아이템(상의) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Bottoms);    // 능력치 합계 += 착용중인 장비아이템(하의) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Shose);      // 능력치 합계 += 착용중인 장비아이템(신발) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Gloves);     // 능력치 합계 += 착용중인 장비아이템(장갑) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Mainweapon); // 능력치 합계 += 착용중인 장비아이템(주무기) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Subweapon);  // 능력치 합계 += 착용중인 장비아이템(보조무기) 능력치

        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_ItemSetEffect);    // 능력치 합계 += 적용중인 아이템 세트효과 능력치

        // 스킬(버프ㆍ디버프) 적용으로 인한 일시적 스탯(능력치) 업데이트
        foreach (KeyValuePair<int, Skill_SSEffect> set in m_sDictionary_Skill_SSEffect_Apply) // 플레이어에게 적용중인 모든 스킬(버프ㆍ디버프) 효과 조사
        {
            m_sStatus.P_OperatorSTATUS(set.Value.m_sStatus_Effect_Temporary); // 능력치 합계 += 적용중인 스킬 능력치
	    m_sSoc.P_OperatorSOC(set.Value.m_sSoc_Effect_Temporary);          // 평판 합계 += 적용중인 스킬 평판
        }

        CheckSkill_Condition(); // 스킬(상태이상[속박, 둔화]) 적용으로 인한 스탯(능력치) 업데이트

        m_sStatus.SetSTATUS_EXP_Current(m_nEXP_Current); // 현재경험치 설정
        m_sStatus.SetSTATUS_HP_Current(m_nHP_Current);   // 현재체력 설정
        m_sStatus.SetSTATUS_MP_Current(m_nMP_Current);   // 현재마나 설정
    }

    // 퀘스트 완료로인한 능력치 업데이트
    public void UpdateStatus_QuestClear(STATUS extrastatus) // extrastatus : 변경될 능력치 정보
    {
        // 특정 능력치만 적용
        m_sStatus_Origin.P_OperatorSTATUS_HP_Max(extrastatus.GetSTATUS_HP_Max());                     // 고유 능력치(최대체력) += 퀘스트 완료 보상 능력치(최대체력)
        m_sStatus_Origin.P_OperatorSTATUS_MP_Max(extrastatus.GetSTATUS_MP_Max());                     // 고유 능력치(최대마나) += 퀘스트 완료 보상 능력치(최대마나)
        m_sStatus_Origin.P_OperatorSTATUS_Damage_Total(extrastatus.GetSTATUS_Damage_Total());         // 고유 능력치(총데미지) += 퀘스트 완료 보상 능력치(총데미지)
        m_sStatus_Origin.P_OperatorSTATUS_CriticalRate(extrastatus.GetSTATUS_CriticalRate());         // 고유 능력치(크리티컬 확률) += 퀘스트 완료 보상 능력치(크리티컬 확률)
        m_sStatus_Origin.P_OperatorSTATUS_CriticalDamage(extrastatus.GetSTATUS_CriticalDamage());     // 고유 능력치(크리티컬 데미지) += 퀘스트 완료 보상 능력치(크리티컬 데미지)
        m_sStatus_Origin.P_OperatorSTATUS_Defence_Physical(extrastatus.GetSTATUS_Defence_Physical()); // 고유 능력치(물리방어력) += 퀘스트 완료 보상 능력치(물리방어력)
        m_sStatus_Origin.P_OperatorSTATUS_Defence_Magical(extrastatus.GetSTATUS_Defence_Magical());   // 고유 능력치(마법방어력) += 퀘스트 완료 보상 능력치(마법방어력)
        m_sStatus_Origin.P_OperatorSTATUS_Speed(extrastatus.GetSTATUS_Speed());                       // 고유 능력치(이동속도) += 퀘스트 완료 보상 능력치(이동속도)
        m_sStatus_Origin.P_OperatorSTATUS_AttackSpeed(extrastatus.GetSTATUS_AttackSpeed());           // 고유 능력치(공격속도) += 퀘스트 완료 보상 능력치(공격속도)
        m_sStatus_Origin.P_OperatorSTATUS_EvasionRate(extrastatus.GetSTATUS_EvasionRate());           // 고유 능력치(회피율) += 퀘스트 완료 보상 능력치(회피율)

        m_nHP_Current = m_sStatus.GetSTATUS_HP_Current();   // 현재체력 임시 저장
        m_nMP_Current = m_sStatus.GetSTATUS_MP_Current();   // 현재마나 임시 저장

        m_sStatus.SetSTATUS_Zero(); // 능력치 합계 초기화

        // 1. 플레이어 고유 능력치 업데이트
        m_sStatus.P_OperatorSTATUS(m_sStatus_Origin);                 // 능력치 합계 += 고유 능력치(성장 능력치 + 영구적 버프포션, 놓아주기, 퀘스트 완료 보상 등 추가로 획득한 능력치)
        // 2. 플레이어에게 적용중인 소비아이템(일시적 버프포션)의 능력치 업데이트
        m_sStatus.P_OperatorSTATUS(m_sStatus_Item_Use_Buff);          // 능력치 합계 += 플레이어에게 적용중인 소비아이템(일시적 버프포션)의 능력치
        // 플레이어가 착용중인 장비아이템의 능력치 업데이트
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Hat);        // 능력치 합계 += 착용중인 장비아이템(모자) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Top);        // 능력치 합계 += 착용중인 장비아이템(상의) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Bottoms);    // 능력치 합계 += 착용중인 장비아이템(하의) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Shose);      // 능력치 합계 += 착용중인 장비아이템(신발) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Gloves);     // 능력치 합계 += 착용중인 장비아이템(장갑) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Mainweapon); // 능력치 합계 += 착용중인 장비아이템(주무기) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Subweapon);  // 능력치 합계 += 착용중인 장비아이템(보조무기) 능력치

        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_ItemSetEffect);    // 능력치 합계 += 적용중인 아이템 세트효과 능력치

        // 스킬(버프ㆍ디버프) 적용으로 인한 일시적 스탯(능력치) 업데이트
        foreach (KeyValuePair<int, Skill_SSEffect> set in m_sDictionary_Skill_SSEffect_Apply) // 플레이어에게 적용중인 모든 스킬(버프ㆍ디버프) 효과 조사
        {
            m_sStatus.P_OperatorSTATUS(set.Value.m_sStatus_Effect_Temporary); // 능력치 합계 += 적용중인 스킬 능력치
	    m_sSoc.P_OperatorSOC(set.Value.m_sSoc_Effect_Temporary);          // 평판 합계 += 적용중인 스킬 평판
        }

        CheckSkill_Condition(); // 스킬(상태이상[속박, 둔화]) 적용으로 인한 스탯(능력치) 업데이트

        m_sStatus.SetSTATUS_HP_Current(m_nHP_Current);   // 현재체력 설정
        m_sStatus.SetSTATUS_MP_Current(m_nMP_Current);   // 현재마나 설정

        CheckLogic(); // 능력치 논리 판단(현재체력, 현재마나)
    }

    // 스킬(버프ㆍ디버프) 적용으로 인한 능력치 업데이트
    public void UpdateStatus_ApplySkill()
    {
        m_nEXP_Current = m_sStatus.GetSTATUS_EXP_Current(); // 현재경험치 임시 저장
        m_nHP_Current = m_sStatus.GetSTATUS_HP_Current();   // 현재체력 임시 저장
        m_nMP_Current = m_sStatus.GetSTATUS_MP_Current();   // 현재마나 임시 저장

        m_sStatus.SetSTATUS_Zero(); // 능력치 합계 초기화

        // 1. 플레이어 고유 능력치 업데이트
        m_sStatus.P_OperatorSTATUS(m_sStatus_Origin);                 // 능력치 합계 += 고유 능력치(성장 능력치 + 영구적 버프포션, 놓아주기, 퀘스트 완료 보상 등 추가로 획득한 능력치)
        // 2. 플레이어에게 적용중인 소비아이템(일시적 버프포션)의 능력치 업데이트
        m_sStatus.P_OperatorSTATUS(m_sStatus_Item_Use_Buff);          // 능력치 합계 += 플레이어에게 적용중인 소비아이템(일시적 버프포션)의 능력치
        // 3. 플레이어가 착용중인 장비아이템의 능력치 업데이트
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Hat);        // 능력치 합계 += 착용중인 장비아이템(모자) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Top);        // 능력치 합계 += 착용중인 장비아이템(상의) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Bottoms);    // 능력치 합계 += 착용중인 장비아이템(하의) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Shose);      // 능력치 합계 += 착용중인 장비아이템(신발) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Gloves);     // 능력치 합계 += 착용중인 장비아이템(장갑) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Mainweapon); // 능력치 합계 += 착용중인 장비아이템(주무기) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Subweapon);  // 능력치 합계 += 착용중인 장비아이템(보조무기) 능력치

        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_ItemSetEffect);    // 능력치 합계 += 적용중인 아이템 세트효과 능력치

        // 스킬(버프ㆍ디버프) 적용으로 인한 일시적 스탯(능력치) 업데이트
        foreach (KeyValuePair<int, Skill_SSEffect> set in m_sDictionary_Skill_SSEffect_Apply) // 플레이어에게 적용중인 모든 스킬(버프ㆍ디버프) 효과 조사
        {
            m_sStatus.P_OperatorSTATUS(set.Value.m_sStatus_Effect_Temporary); // 능력치 합계 += 적용중인 스킬 능력치
	    m_sSoc.P_OperatorSOC(set.Value.m_sSoc_Effect_Temporary);          // 평판 합계 += 적용중인 스킬 평판
        }

        CheckSkill_Condition(); // 스킬(상태이상[속박, 둔화]) 적용으로 인한 스탯(능력치) 업데이트

        m_sStatus.SetSTATUS_EXP_Current(m_nEXP_Current); // 현재경험치 설정
        m_sStatus.SetSTATUS_HP_Current(m_nHP_Current);   // 현재체력 설정
        m_sStatus.SetSTATUS_MP_Current(m_nMP_Current);   // 현재마나 설정

        CheckLogic(); // 능력치 논리 판단(현재체력, 현재마나)
    }

    // 스킬(버프ㆍ디버프) 적용으로 인한 평판 업데이트
    public void UpdateSoc_ApplySkill()
    {
        m_sSoc.SetSOC_Zero(); // 평판 합계 초기화

        // 1. 플레이어 고유 평판 업데이트
        m_sSoc.P_OperatorSOC(m_sSoc_Origin);                 // 평판 합계 += 고유 평판(성장 평판 + 영구적 버프포션, 놓아주기, 퀘스트 완료 보상 등 추가로 획득한 평판)
        // 2. 플레이어에게 적용중인 소비아이템(일시적 버프포션)의 평판 업데이트
        m_sSoc.P_OperatorSOC(m_sSoc_Item_Use_Buff);          // 평판 합계 += 플레이어에게 적용중인 소비아이템(일시적 버프포션)의 평판
        // 3. 플레이어가 착용중인 장비아이템의 평판 업데이트
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Hat);        // 평판 합계 += 착용중인 장비아이템(모자) 평판
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Top);        // 평판 합계 += 착용중인 장비아이템(상의) 평판
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Bottoms);    // 평판 합계 += 착용중인 장비아이템(하의) 평판
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Shose);      // 평판 합계 += 착용중인 장비아이템(신발) 평판
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Gloves);     // 평판 합계 += 착용중인 장비아이템(장갑) 평판
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Mainweapon); // 평판 합계 += 착용중인 장비아이템(주무기) 평판
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Subweapon);  // 평판 합계 += 착용중인 장비아이템(보조무기) 평판

        m_sSoc.P_OperatorSOC(m_sSoc_Extra_ItemSetEffect);    // 평판 합계 += 적용중인 아이템 세트효과 평판

        // 스킬 적용으로 인한 일시적 스탯(평판) 변화 업데이트
        foreach (KeyValuePair<int, Skill_SSEffect> set in m_sDictionary_Skill_SSEffect_Apply) // 플레이어에게 적용중인 모든 스킬 조사
        {
            m_sSoc.P_OperatorSOC(set.Value.m_sSoc_Effect_Temporary); // 평판 합계 += 적용중인 스킬 평판
        }
    }

    // 평판 업데이트. 대부분의 스탯(능력치, 평판) 업데이트 함수에서 사용된다.
    public void UpdateSOC()
    {
        m_sSoc.SetSOC_Zero(); // 평판 합계 초기화

        // 1. 플레이어 고유 평판 업데이트
        m_sSoc.P_OperatorSOC(m_sSoc_Origin);                 // 평판 합계 += 고유 평판(성장 평판 + 영구적 버프포션, 놓아주기, 퀘스트 완료 보상 등 추가로 획득한 평판)
        // 2. 플레이어에게 적용중인 소비아이템(일시적 버프포션)의 평판 업데이트
        m_sSoc.P_OperatorSOC(m_sSoc_Item_Use_Buff);          // 평판 합계 += 플레이어에게 적용중인 소비아이템(일시적 버프포션)의 평판
        // 3. 플레이어가 착용중인 장비아이템의 평판 업데이트
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Hat);        // 평판 합계 += 착용중인 장비아이템(모자) 평판
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Top);        // 평판 합계 += 착용중인 장비아이템(상의) 평판
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Bottoms);    // 평판 합계 += 착용중인 장비아이템(하의) 평판
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Shose);      // 평판 합계 += 착용중인 장비아이템(신발) 평판
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Gloves);     // 평판 합계 += 착용중인 장비아이템(장갑) 평판
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Mainweapon); // 평판 합계 += 착용중인 장비아이템(주무기) 평판
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Subweapon);  // 평판 합계 += 착용중인 장비아이템(보조무기) 평판

        m_sSoc.P_OperatorSOC(m_sSoc_Extra_ItemSetEffect);    // 평판 합계 += 적용중인 아이템 세트효과 평판

         // 스킬 적용으로 인한 일시적 스탯(평판) 변화 업데이트
        foreach (KeyValuePair<int, Skill_SSEffect> set in m_sDictionary_Skill_SSEffect_Apply) // 플레이어에게 적용중인 모든 스킬 조사
        {
            m_sSoc.P_OperatorSOC(set.Value.m_sSoc_Effect_Temporary); // 평판 합계 += 적용중인 스킬 평판
        }
    }

    // 몬스터 토벌 시 변경되는 스탯(능력치(주로 경험치), 평판) 업데이트
    public void MobDeath(STATUS status, SOC soc) // soc : 변경될 평판 정보, status : 변경될 능력치(주로 경험치) 정보
    {
        // 플레이어 고유 평판 업데이트
        m_sSoc_Origin.P_OperatorSOC(soc);
        UpdateSOC(); // 평판 업데이트

        m_nEXP_Current = m_sStatus.GetSTATUS_EXP_Current(); // 현재경험치 임시 저장

        CarculateEXP(status); // 경험치 계산
        // 로그GUI에 획득한 경험치 정보 출력
        if (status.GetSTATUS_EXP_Current() != 0)
            GUIManager_Total.Instance.UpdateLog("+EXP: " + status.GetSTATUS_EXP_Current());
    }

    // 로딩 관련 함수
    // 게임 시작 시 플레이어 스탯(능력치, 평판) 로딩
    public void Update_Loading(int exp, int hp, int mp, STATUS status, SOC soc)
    {
        UpdateStatus_Loading(exp, hp, mp); // 플레이어 능력치 로딩
        UpdateSoc_Loading(); // 플레이어 평판 로딩

        // 로딩 마지막 부분에 추가할 예정인 코드
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
    }
    // 플레이어 능력치 로딩
    public void UpdateStatus_Loading(int exp, int hp, int mp) // exp : 현재경험치, hp : 현재체력, mp : 현재마나
    {
        m_sStatus.SetSTATUS_Zero(); // 능력치 합계 초기화

        // 1. 플레이어 고유 능력치 업데이트
        m_sStatus.P_OperatorSTATUS(m_sStatus_Origin);                 // 능력치 합계 += 고유 능력치(성장 능력치 + 영구적 버프포션, 놓아주기, 퀘스트 완료 보상 등 추가로 획득한 능력치)
        // 2. 플레이어에게 적용중인 소비아이템(일시적 버프포션)의 능력치 업데이트
        m_sStatus.P_OperatorSTATUS(m_sStatus_Item_Use_Buff);          // 능력치 합계 += 플레이어에게 적용중인 소비아이템(일시적 버프포션)의 능력치
        // 3. 플레이어가 착용중인 장비아이템의 능력치 업데이트
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Hat);        // 능력치 합계 += 착용중인 장비아이템(모자) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Top);        // 능력치 합계 += 착용중인 장비아이템(상의) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Bottoms);    // 능력치 합계 += 착용중인 장비아이템(하의) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Shose);      // 능력치 합계 += 착용중인 장비아이템(신발) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Gloves);     // 능력치 합계 += 착용중인 장비아이템(장갑) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Mainweapon); // 능력치 합계 += 착용중인 장비아이템(주무기) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Subweapon);  // 능력치 합계 += 착용중인 장비아이템(보조무기) 능력치

        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_ItemSetEffect);    // 능력치 합계 += 적용중인 아이템 세트효과 능력치

        // 스킬(버프ㆍ디버프) 적용으로 인한 일시적 스탯(능력치) 업데이트
        foreach (KeyValuePair<int, Skill_SSEffect> set in m_sDictionary_Skill_SSEffect_Apply) // 플레이어에게 적용중인 모든 스킬(버프ㆍ디버프) 효과 조사
        {
            m_sStatus.P_OperatorSTATUS(set.Value.m_sStatus_Effect_Temporary); // 능력치 합계 += 적용중인 스킬 능력치
	    m_sSoc.P_OperatorSOC(set.Value.m_sSoc_Effect_Temporary);          // 평판 합계 += 적용중인 스킬 평판
        }

        CheckSkill_Condition(); // 스킬(상태이상[속박, 둔화]) 적용으로 인한 스탯(능력치) 업데이트

        m_sStatus.SetSTATUS_EXP_Current(exp); // 현재경험치 설정
        m_sStatus.SetSTATUS_HP_Current(hp);   // 현재체력 설정
        m_sStatus.SetSTATUS_MP_Current(mp);   // 현재마나 설정

        CheckLogic(); // 능력치 논리 판단(현재체력, 현재마나)

        Player_Total.Instance.m_pm_Move.SetAttackSpeed(Return_AttackSpeed()); // 플레이어의 행동을 관리하는 Player_Move.cs에 플레이어의 공격속도를 제공
                                                                              // 
                                                                              // ※ Player_Total.cs에 포함되는 Player_Status.cs에서 다시 Player_Total.cs에 접근하는것은 계층구조상 바람직하지 않다.
                                                                              //    따라서 해당 부분을 Player_Status.cs 외부로 옮기는 방향으로 수정할 예정이다.
                                                                              //
    }
    // 플레이어 평판 로딩
    public void UpdateSoc_Loading()
    {
        m_sSoc.SetSOC_Zero(); // 평판 합계 초기화

        // 1. 플레이어 고유 평판 업데이트
        m_sSoc.P_OperatorSOC(m_sSoc_Origin);                 // 평판 합계 += 고유 평판(성장 평판 + 영구적 버프포션, 놓아주기, 퀘스트 완료 보상 등 추가로 획득한 평판)
        // 2. 플레이어에게 적용중인 소비아이템(일시적 버프포션)의 평판 업데이트
        m_sSoc.P_OperatorSOC(m_sSoc_Item_Use_Buff);          // 평판 합계 += 플레이어에게 적용중인 소비아이템(일시적 버프포션)의 평판
        // 3. 플레이어가 착용중인 장비아이템의 평판 업데이트
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Hat);        // 평판 합계 += 착용중인 장비아이템(모자) 평판
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Top);        // 평판 합계 += 착용중인 장비아이템(상의) 평판
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Bottoms);    // 평판 합계 += 착용중인 장비아이템(하의) 평판
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Shose);      // 평판 합계 += 착용중인 장비아이템(신발) 평판
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Gloves);     // 평판 합계 += 착용중인 장비아이템(장갑) 평판
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Mainweapon); // 평판 합계 += 착용중인 장비아이템(주무기) 평판
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Subweapon);  // 평판 합계 += 착용중인 장비아이템(보조무기) 평판

        m_sSoc.P_OperatorSOC(m_sSoc_Extra_ItemSetEffect);    // 평판 합계 += 적용중인 아이템 세트효과 평판

        // 스킬 적용으로 인한 일시적 스탯(평판) 변화 업데이트
        foreach (KeyValuePair<int, Skill_SSEffect> set in m_sDictionary_Skill_SSEffect_Apply) // 플레이어에게 적용중인 모든 스킬 조사
        {
            m_sSoc.P_OperatorSOC(set.Value.m_sSoc_Effect_Temporary); // 평판 합계 += 적용중인 스킬 평판
        }
    }

    // 사용중인 소비아이템의 지속시간, 쿨타임 로딩
    public void Item_Use_Loading()
    {
        
    }

    // 퀘스트 완료 보상 수령으로 인한 스탯(능력치, 평판) 업데이트
    public void GetQuestReward(Quest quest) // queset : 퀘스트 정보(퀘스트 완료 보상(스탯(능력치, 평판)))
    {
        // 플레이어 고유 평판 업데이트
        m_sSoc_Origin.P_OperatorSOC(quest.m_sRewardSOC);
        UpdateSOC(); // 평판 업데이트

        m_nEXP_Current = m_sStatus.GetSTATUS_EXP_Current(); // 현재경험치 임시 저장
        UpdateStatus_QuestClear(quest.m_sRewardSTATUS); // 퀘스트 완료로인한 능력치 업데이트

        CarculateEXP(quest.m_sRewardSTATUS); // 경험치 계산
        GUIManager_Total.Instance.UpdateLog("+EXP: " + quest.m_sRewardSTATUS.GetSTATUS_EXP_Current()); // 로그GUI에 퀘스트 완료 보상(추가 경험치) 정보 출력
    }

    // 능력치 논리(조건) 판단
    public void CheckLogic()
    {
        // 플레이어의 현재체력은 최대체력을 초과할 수 없다.
        if (m_sStatus.GetSTATUS_HP_Current() > m_sStatus.GetSTATUS_HP_Max())
        {
            m_sStatus.SetSTATUS_HP_Current(m_sStatus.GetSTATUS_HP_Max());
        }
        // 플레이어의 현재마나는 최대마나를 초과할 수 없다.
        if (m_sStatus.GetSTATUS_MP_Current() > m_sStatus.GetSTATUS_MP_Max())
        {
            m_sStatus.SetSTATUS_MP_Current(m_sStatus.GetSTATUS_MP_Max());
        }
    }

    // 스킬(상태이상[속박, 둔화]) 적용으로 인한 능력치 업데이트
    public void CheckSkill_Condition()
    {
        if (Player_Skill.m_Skill_Condition.ConditionCheck_Bind() == true)
        {
            m_sStatus.SetSTATUS_Speed(0); // 플레이어 이동속도 0 고정
        }
        if (Player_Skill.m_Skill_Condition.ConditionCheck_Slow() == true)
        {
            m_sStatus.SetSTATUS_Speed((int)((float)m_sStatus.GetSTATUS_Speed() * ((100 - Player_Skill.m_Skill_Condition.GetSlowRatio()) / 100))); // 스킬(상태이상[둔화]) 비율에 따라 플레이어 이동속도 감소
        }
    }

    // 리트라이(부활) 관련 함수
    public void ReTry()
    {
        ReTry_Initializing();
    }
    // 리트라이(부활) 시 플레이어 스탯(능력치, 평판) 초기화(플레이어에게 적용중인 소비아이템 효과(버프ㆍ디버프), 스킬, 상태이상 해제)
    public void ReTry_Initializing()
    {
        // 소비아이템 목록 초기화
        m_Dictionary_Item_Use_Buff.Clear(); // 플레이어에게 적용중인 소비아이템 목록 초기화
        m_Dictionary_Item_Use_CoolTime.Clear(); // 소비아이템 쿨타임 목록 초기화
        // 플레이어에게 적용중인 소비아이템의 지속시간을 계산하는 코루틴 종료
        foreach(KeyValuePair<int, Coroutine> item in m_Dictionary_Coroutine_Item_Use_Buff)
        {
            StopCoroutine(item.Value);
        }
        // 소비아이템의 쿨타임을 계산하는 코루틴 종료
        foreach (KeyValuePair<int, Coroutine> item in m_Dictionary_Coroutine_Item_Use_CoolTime)
        {
            StopCoroutine(item.Value);
        }
        // 코루틴 초기화
        m_Dictionary_Coroutine_Item_Use_Buff.Clear(); // 플레이어에게 적용중인 소비아이템의 지속시간을 계산하는 코루틴 목록 초기화
        m_Dictionary_Coroutine_Item_Use_CoolTime.Clear(); // 소비아이템의 쿨타임을 계산하는 코루틴 목록 초기화
        // 시간(float) 초기화
        m_Dictionary_Item_Use_Buff_RemainingTime.Clear(); // 플레이어에게 적용중인 소비아이템의 지속시간 목록 초기화
        m_Dictionary_Item_Use_CoolTime_RemainingTime.Clear(); // 소비아이템의 쿨타임 목록 초기화

        UpdateStatus_ReTry(); // 능력치 업데이트
        UpdateSoc_ReTry(); // 평판 업데이트

 	GUIManager_Total.Instance.Update_SS(); // 스탯GUI 업데이트
    }
    // 리트라이(부활) 시 스탯(능력치, 평판) 업데이트 관련 함수.
    // 리트라이(부활) 시 능력치 업데이트
    void UpdateStatus_ReTry()
    {
        m_nEXP_Current = m_sStatus.GetSTATUS_EXP_Current(); // 현재경험치 임시 저장
        m_nHP_Current = m_sStatus.GetSTATUS_HP_Current();   // 현재체력 임시 저장
        m_nMP_Current = m_sStatus.GetSTATUS_MP_Current();   // 현재마나 임시 저장

        m_sStatus.SetSTATUS_Zero(); // 능력치 합계 초기화
        
        // 1. 플레이어 고유 능력치 업데이트
        m_sStatus.P_OperatorSTATUS(m_sStatus_Origin);                 // 능력치 합계 += 고유 능력치(성장 능력치 + 영구적 버프포션, 놓아주기, 퀘스트 완료 보상 등 추가로 획득한 능력치)
        // 2. 플레이어에게 적용중인 소비아이템(일시적 버프포션)의 능력치 업데이트
        m_sStatus_Item_Use_Buff.SetSTATUS_Zero();
        m_sStatus.P_OperatorSTATUS(m_sStatus_Item_Use_Buff);          // 능력치 합계 += 플레이어에게 적용중인 소비아이템(일시적 버프포션)의 능력치
        // 3. 플레이어가 착용중인 장비아이템의 능력치 업데이트
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Hat);        // 능력치 합계 += 착용중인 장비아이템(모자) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Top);        // 능력치 합계 += 착용중인 장비아이템(상의) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Bottoms);    // 능력치 합계 += 착용중인 장비아이템(하의) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Shose);      // 능력치 합계 += 착용중인 장비아이템(신발) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Gloves);     // 능력치 합계 += 착용중인 장비아이템(장갑) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Mainweapon); // 능력치 합계 += 착용중인 장비아이템(주무기) 능력치
        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_Equip_Subweapon);  // 능력치 합계 += 착용중인 장비아이템(보조무기) 능력치

        m_sStatus.P_OperatorSTATUS(m_sStatus_Extra_ItemSetEffect);    // 능력치 합계 += 적용중인 아이템 세트효과 능력치

        m_sStatus.SetSTATUS_EXP_Current(m_nEXP_Current);              // 현재경험치 설정
        m_sStatus.SetSTATUS_HP_Current(m_sStatus.GetSTATUS_HP_Max()); // 현재체력 설정(현재체력 = 최대체력)
        m_sStatus.SetSTATUS_MP_Current(m_sStatus.GetSTATUS_MP_Max()); // 현재마나 설정(현재마나 = 최대마나)

        CheckLogic(); // 능력치 논리 판단(현재체력, 현재마나)
    }
    // 리트라이(부활) 시 평판 업데이트
    void UpdateSoc_ReTry()
    {
        m_sSoc.SetSOC_Zero(); // 평판 합계 초기화

        // 1. 플레이어 고유 평판 업데이트
        m_sSoc.P_OperatorSOC(m_sSoc_Origin);                 // 평판 합계 += 고유 평판(성장 평판 + 영구적 버프포션, 놓아주기, 퀘스트 완료 보상 등 추가로 획득한 평판)
        // 2. 플레이어에게 적용중인 소비아이템(일시적 버프포션)의 평판 업데이트
        m_sSoc_Item_Use_Buff.SetSOC_Zero();
        m_sSoc.P_OperatorSOC(m_sSoc_Item_Use_Buff);          // 평판 합계 += 플레이어에게 적용중인 소비아이템(일시적 버프포션)의 평판
        // 3. 플레이어가 착용중인 장비아이템의 평판 업데이트
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Hat);        // 평판 합계 += 착용중인 장비아이템(모자) 평판
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Top);        // 평판 합계 += 착용중인 장비아이템(상의) 평판
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Bottoms);    // 평판 합계 += 착용중인 장비아이템(하의) 평판
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Shose);      // 평판 합계 += 착용중인 장비아이템(신발) 평판
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Gloves);     // 평판 합계 += 착용중인 장비아이템(장갑) 평판
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Mainweapon); // 평판 합계 += 착용중인 장비아이템(주무기) 평판
        m_sSoc.P_OperatorSOC(m_sSoc_Extra_Equip_Subweapon);  // 평판 합계 += 착용중인 장비아이템(보조무기) 평판

        m_sSoc.P_OperatorSOC(m_sSoc_Extra_ItemSetEffect);    // 평판 합계 += 적용중인 아이템 세트효과 평판
    }

    // 
    // ※ 스탯(능력치, 평판) 업데이트 함수에 코드가 중복되는 부분이 많다. 이는 추후 중복되는 코드를 함수화 하여 코드 재사용성을 높이는 방향으로 변경할 예정이다.
    // 

    // 장비아이템 착용 조건 판단, 장비아이템 착용 시 스탯(능력치, 평판) 업데이트
    public bool CheckCondition_Item_Equip(Item_Equip item, STATUS playerstatus, SOC playersoc) // item : 착용할 장비아이템 정보(착용 조건(스탯(능력치, 평판) 최소ㆍ최대)), playerstatus : 플레이어 능력치 합계, playersoc : 플레이어 평판 합계
    {
        if (playerstatus.CheckCondition_Max(item.m_sStatus_Limit_Max) == false) // 장비아이템 착용 조건 : 능력치 상한(플레이어의 능력치 합계가 장비아이템 착용 조건(능력치 상한)을 초과한 경우 제한)
        {
            //Debug.Log(item.m_sItemName + ": Status 착용 최대 조건 불만족");
            return false;
        }
        if (playerstatus.CheckCondition_Min(item.m_sStatus_Limit_Min) == false) // 장비아이템 착용 조건 : 능력치 하한(플레이어의 능력치 합계가 장비아이템 착용 조건(능력치 하한)에 미달한 경우 제한)
        {
            //Debug.Log(item.m_sItemName + ": Status 착용 최소 조건 불만족");
            return false;
        }
        if (playersoc.CheckCondition_Max(item.m_sSoc_Limit_Max) == false) // 장비아이템 착용 조건 : 평판 상한(플레이어의 평판 합계가 장비아이템 착용 조건(평판 상한)을 초과한 경우 제한)
        {
            //Debug.Log(item.m_sItemName + ": Soc 착용 최대 조건 불만족");
            return false;
        }
        if (playersoc.CheckCondition_Min(item.m_sSoc_Limit_Min) == false) // 장비아이템 착용 조건 : 평판 하한(플레이어의 평판 합계가 장비아이템 착용 조건(평판 하한)에 미달한 경우 제한)
        {
            //Debug.Log(item.m_sItemName + ": Soc 착용 최소 조건 불만족");
            return false;
        }

        // 착용할 장비아이템의 분류에 따라 스탯(능력치, 평판) 할당
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

        UpdateStatus_Equip(); // 능력치 업데이트(착용중인 장비아이템 변경, 소비아이템(영구적 버프포션) 사용)
        UpdateSOC(); // 평판 업데이트

        return true;
    }

    // 착용중인 장비아이템 해제
    public void Remove_Item_Equip(Item_Equip item) // item : 해제할 장비아이템 정보
    {
        m_sStatus_Extra_ItemSetEffect.SetSTATUS_Zero(); // 적용중인 아이템 세트효과 능력치 초기화
        m_sSoc_Extra_ItemSetEffect.SetSOC_Zero(); // 적용중인 아이템 세트효과 평판 초기화

        // 해제할 장비아이템의 분류에 따라 스탯(능력치, 평판) 초기화
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

        UpdateStatus_Equip(); // 능력치 업데이트(착용중인 장비아이템 변경, 소비아이템(영구적 버프포션) 사용)
        UpdateSOC(); // 평판 업데이트
    }

    // 장비아이템 세트효과 적용 함수
    public void CheckSetItemEffect(Dictionary<int, int> setitemdictionary) // setitemdictionary : 플레이어에게 적용할 장비아이템 세트효과 정보.
                                                                           // Dictionary <Key : 아이템 세트효과 코드 , Value : Key값(아이템 세트효과 코드)을 가지는 장비아이템 개수>
    {
        m_sStatus_Extra_ItemSetEffect.SetSTATUS_Zero(); // 적용중인 아이템 세트효과 능력치 초기화
        m_sSoc_Extra_ItemSetEffect.SetSOC_Zero(); // 적용중인 아이템 세트효과 평판 초기화

        // 아이템 세트효과 스탯(능력치, 평판) 업데이트
        foreach(KeyValuePair<int, int> dictionary in setitemdictionary)
        {
            if (dictionary.Key != 0)
            {
                // 
                // ※ 아이템 세트효과를 가지는 장비아이템의 경우 해당하는 아이템 세트효과 코드를 가진다. 
                //    아이템 세트효과 코드를 가진 장비아이템을 장착하는 개수에 따라 상이한 세트효과가 적용되며, 이는 중첩 적용이 가능하다.
                //
                // 아이템 세트효과 적용    
                for (int i = 1; i < dictionary.Value + 1; i++)
                {
                    m_sStatus_Extra_ItemSetEffect.P_OperatorSTATUS(ItemSetEffectManager.instance.Return_SetItemEffect_STATUS(dictionary.Key, i)); // 아이템 세트효과(능력치) 업데이트
                    m_sSoc_Extra_ItemSetEffect.P_OperatorSOC(ItemSetEffectManager.instance.Return_SetItemEffect_SOC(dictionary.Key, i)); // 아이템 세트효과(평판) 업데이트
                }

                // 로그GUI에 적용중인 아이템 세트효과 정보를 출력
                //GUIManager_Total.Instance.UpdateLog(ItemSetEffectManager.m_Dictionary_ItemSetEffect[dictionary.Key].m_sItemSetEffect_Name + " / " + dictionary.Value);
            }
        }

        UpdateStatus_Equip(); // 능력치 업데이트(착용중인 장비아이템 변경, 소비아이템(영구적 버프포션) 사용)
        UpdateSOC(); // 평판 업데이트
    }

    // 소비아이템 사용 조건 판단
    // return 0 : 사용 가능 / return 1 : 사용 불가능
    public int CheckCondition_Item_Use(Item_Use item) // item : 사용할 소비아이템 정보
    {
        if (m_sStatus.CheckCondition_Max(item.m_sStatus_Limit_Max) == false) // 소비아이템 사용 조건 : 능력치 상한(플레이어의 능력치 합계가 소비아이템 사용 조건(능력치 상한)을 초과한 경우 제한)
        {
            Debug.Log(item.m_sItemName + ": Status 최대 조건 불만족");
            return 1;
        }
        if (m_sStatus.CheckCondition_Min(item.m_sStatus_Limit_Min) == false) // 소비아이템 사용 조건 : 능력치 하한(플레이어의 능력치 합계가 소비아이템 사용 조건(능력치 하한)에 미달한 경우 제한)
        {
            Debug.Log(item.m_sItemName + ": Status 최소 조건 불만족");
            return 1;
        }
        if (m_sSoc.CheckCondition_Max(item.m_sSoc_Limit_Max) == false) // 소비아이템 사용 조건 : 평판 상한(플레이어의 평판 합계가 소비아이템 사용 조건(평판 상한)을 초과한 경우 제한)
        {
            Debug.Log(item.m_sItemName + ": Soc 최대 조건 불만족");
            return 1;
        }
        if (m_sSoc.CheckCondition_Min(item.m_sSoc_Limit_Min) == false) // 소비아이템 사용 조건 : 평판 하한(플레이어의 평판 합계가 소비아이템 사용 조건(평판 하한)에 미달한 경우 제한)
        {
            Debug.Log(item.m_sItemName + ": Soc 최소 조건 불만족");
            return 1;
        }

        return 0;
    }

    // 플레이어의 합계 공격속도 반환
    public float Return_AttackSpeed()
    {
        return m_sStatus.GetSTATUS_AttackSpeed();
    }

    // 소비아이템 사용 시 스탯(능력치, 평판), 버프ㆍ디버프 업데이트
    // return 0 : 소비아이템 사용 성공 / return 1 : 소비아이템 사용 실패
    public int ApplyPotion(Item_Use item) // item : 사용할 소비아이템
    {
        Coroutine coroutine_buff;
        Coroutine coroutine_cooltime;

        // 소비아이템(회복포션) 사용
        if (item.m_eItemUseType == E_ITEM_USE_TYPE.RECOVERPOTION)
        {
             // 사용할 소비아이템의 쿨타임 확인(해당 소비아이템이 사용 가능한 상태인지 판단)
            if (m_Dictionary_Item_Use_CoolTime.ContainsKey(item.m_nItemCode) == true)
            {
                GUIManager_Total.Instance.UpdateLog("[소비아이템][" + item.m_sItemName + "] 사용 불가.(쿨타임)"); // 로그GUI에 사용 불가능한 소비아이템 정보 출력
                return 1;
            }
            Debug.Log(m_sStatus.GetSTATUS_HP_Current() + " / " + m_sStatus.GetSTATUS_HP_Max());
            UpdateStatus_Item_Use_Recover(item.m_sStatus_Effect); // 소비아이템(회복포션) 사용으로 인한 능력치 업데이트
            Debug.Log(m_sStatus.GetSTATUS_HP_Current() + " / " + m_sStatus.GetSTATUS_HP_Max());
            // 사용한 소비아이템의 쿨타임 계산
            if (item.m_fCoolTime > 0) // 해당 소비아이템의 쿨타임이 존재하는 경우
            {
                m_Dictionary_Item_Use_CoolTime.Add(item.m_nItemCode, item); // 적용중인 소비아이템 쿨타임 목록에서 해당 소비아이템 추가. <소비아이템 쿨타임 딕셔너리>
                coroutine_cooltime = StartCoroutine(Process_Item_Use_CoolTime(item)); // 소비아이템 쿨타임을 계산하는 코루틴 시작 및 추가. <소비아이템 쿨타임(코루틴) 딕셔너리>
                m_Dictionary_Coroutine_Item_Use_CoolTime.Add(item.m_nItemCode, coroutine_cooltime);
            }

            GUIManager_Total.Instance.UpdateLog("[소비아이템][" + item.m_sItemName + "] 회복 효과 적용."); // 로그GUI에 사용한 소비아이템 정보 출력
            return 0;
        }
        // 소비아이템(영구적 버프포션) 사용
        if (item.m_eItemUseType == E_ITEM_USE_TYPE.ETERNALBUFFPOTION)
        {
            // 사용할 소비아이템의 쿨타임 확인(해당 소비아이템이 사용 가능한 상태인지 판단)
            if (m_Dictionary_Item_Use_CoolTime.ContainsKey(item.m_nItemCode) == true)
            {
                GUIManager_Total.Instance.UpdateLog("[소비아이템][" + item.m_sItemName + "] 사용 불가.(쿨타임)"); // 로그GUI에 사용 불가능한 소비아이템 정보 출력
                return 1;
            }

            UpdateStatus_Item_Use_EternalBuff(item.m_sStatus_Effect, item.m_sSoc_Effect); // 소비아이템(회복포션) 사용으로 인한 능력치 업데이트

            // 사용한 소비아이템의 쿨타임 계산
            if (item.m_fCoolTime > 0) // 해당 소비아이템의 쿨타임이 존재하는 경우
            {
                m_Dictionary_Item_Use_CoolTime.Add(item.m_nItemCode, item); // 적용중인 소비아이템 쿨타임 목록에서 해당 소비아이템 추가. <소비아이템 쿨타임 딕셔너리>
                coroutine_cooltime = StartCoroutine(Process_Item_Use_CoolTime(item)); // 소비아이템 쿨타임을 계산하는 코루틴 시작 및 추가. <소비아이템 쿨타임(코루틴) 딕셔너리>
                m_Dictionary_Coroutine_Item_Use_CoolTime.Add(item.m_nItemCode, coroutine_cooltime);
            }

            GUIManager_Total.Instance.UpdateLog("[소비아이템][" + item.m_sItemName + "] 영구 스탯 변화 적용."); // 로그GUI에 사용한 소비아이템 정보 출력
            return 0;
        }
        // 소비아이템(일시적 버프포션) 사용
        if (item.m_eItemUseType == E_ITEM_USE_TYPE.TEMPORARYBUFFPOTION)
        {
            // 사용할 소비아이템의 쿨타임 확인(해당 소비아이템이 사용 가능한 상태인지 판단)
            if (m_Dictionary_Item_Use_CoolTime.ContainsKey(item.m_nItemCode) == true)
            {
                GUIManager_Total.Instance.UpdateLog("[소비아이템][" + item.m_sItemName + "] 사용 불가.(쿨타임)"); // 로그GUI에 사용 불가능한 소비아이템 정보 출력
                return 1;
            }

            // 사용한 소비아이템(일시적 버프포션) 효과 적용(지속시간 중첩 불가능(초기화)), 지속시간 및 쿨타임 계산
            if (m_Dictionary_Item_Use_Buff.ContainsKey(item.m_nItemCode) == true) // 해당 소비아이템 효과가 이미 적용중인 경우(소비아이템 효과 지속시간 갱신)
            {
                // 각종 딕셔너리에서 소비아이템 제거(효과, 지속시간)
                m_Dictionary_Item_Use_Buff.Remove(item.m_nItemCode); // 적용중인 소비아이템 목록에서 해당 소비아이템 제거. <소비아이템 효과 딕셔너리>
                StopCoroutine(m_Dictionary_Coroutine_Item_Use_Buff[item.m_nItemCode]); // 소비아이템의 지속시간을 계산하는 코루틴 중단 및 제거. <소비아이템 효과 지속시간(코루틴) 딕셔너리>
                m_Dictionary_Coroutine_Item_Use_Buff.Remove(item.m_nItemCode);
                m_Dictionary_Item_Use_Buff_RemainingTime.Remove(item.m_nItemCode); // 소비아이템의 지속시간 딕셔너리에서 해당 소비아이템 제거. <소비아이템 효과 지속시간(float) 딕셔너리>
                
                // 각종 딕셔너리에 소비아이템 추가(효과, 지속시간, 쿨타임)
                m_Dictionary_Item_Use_Buff.Add(item.m_nItemCode, item); // 적용중인 소비아이템 목록에 해당 소비아이템 추가. <소비아이템 효과 딕셔너리>
                coroutine_buff = StartCoroutine(Process_Item_Use_BuffTime(item)); // 소비아이템의 지속시간을 계산하는 코루틴 시작 및 추가. <소비아이템 효과 지속시간(코루틴) 딕셔너리>
                                                                                  // 해당 코루틴이 시작되면 소비아이템의 지속시간 딕셔너리가 갱신(추가, 업데이트). <소비아이템 효과 지속시간(float) 딕셔너리>
                m_Dictionary_Coroutine_Item_Use_Buff.Add(item.m_nItemCode, coroutine_buff);
                if (item.m_fCoolTime > 0) // 해당 소비아이템의 쿨타임이 존재하는 경우
                {
                    m_Dictionary_Item_Use_CoolTime.Add(item.m_nItemCode, item); // 적용중인 소비아이템 쿨타임 목록에서 해당 소비아이템 추가. <소비아이템 쿨타임 딕셔너리>
                    coroutine_cooltime = StartCoroutine(Process_Item_Use_CoolTime(item)); // 소비아이템 쿨타임을 계산하는 코루틴 시작 및 추가. <소비아이템 쿨타임(코루틴) 딕셔너리>
                    m_Dictionary_Coroutine_Item_Use_CoolTime.Add(item.m_nItemCode, coroutine_cooltime);
                }

                GUIManager_Total.Instance.UpdateLog("[소비아이템][" + item.m_sItemName + "] 버프 효과 적용. (지속시간 초기화)"); // 로그GUI에 사용한 소비아이템 정보 출력
            }
            else // 해당 소비아이템 효과가 이미 적용중이지 않을 경우(소비아이템 효과 추가)
            {
                // 각종 딕셔너리에 소비아이템 추가(효과, 지속시간, 쿨타임)
                m_Dictionary_Item_Use_Buff.Add(item.m_nItemCode, item); // 적용중인 소비아이템 목록에 해당 소비아이템 추가. <소비아이템 효과 딕셔너리>
                coroutine_buff = StartCoroutine(Process_Item_Use_BuffTime(item)); // 소비아이템의 지속시간을 계산하는 코루틴 시작 및 추가. <소비아이템 효과 지속시간(코루틴) 딕셔너리>
                                                                                  // 해당 코루틴이 시작되면 소비아이템의 지속시간 딕셔너리가 갱신(추가, 업데이트). <소비아이템 효과 지속시간(float) 딕셔너리>
                m_Dictionary_Coroutine_Item_Use_Buff.Add(item.m_nItemCode, coroutine_buff);
                if (item.m_fCoolTime > 0) // 해당 소비아이템의 쿨타임이 존재하는 경우
                {
                    m_Dictionary_Item_Use_CoolTime.Add(item.m_nItemCode, item); // 적용중인 소비아이템 쿨타임 목록에서 해당 소비아이템 추가. <소비아이템 쿨타임 딕셔너리>
                    coroutine_cooltime = StartCoroutine(Process_Item_Use_CoolTime(item)); // 소비아이템 쿨타임을 계산하는 코루틴 시작 및 추가. <소비아이템 쿨타임(코루틴) 딕셔너리>
                    m_Dictionary_Coroutine_Item_Use_CoolTime.Add(item.m_nItemCode, coroutine_cooltime);
                }
    
                GUIManager_Total.Instance.UpdateLog("[소비아이템][" + item.m_sItemName + "] 버프 효과 적용."); // 로그GUI에 사용한 소비아이템 정보 출력
            }
            return 0;
        }

        return 0;
    }
    // 게임 시작 시 플레이어에게 적용중인 소비아이템(효과, 지속시간, 쿨타임) 로딩
    // return 0 : 소비아이템 로딩 성공
    public int Load_ApplyPotion(Item_Use item, float duration, float cool) // item : 로딩할 소비아이템, duration : 소비아이템 잔여 지속시간, cool : 소비아이템 잔여 쿨타임
    {
        Coroutine coroutine_buff;
        Coroutine coroutine_cooltime;

        // 소비아이템(회복포션) 로딩(쿨타임)
        if (item.m_eItemUseType == E_ITEM_USE_TYPE.RECOVERPOTION)
        {
            if (cool > 0) // 해당 소비아이템의 잔여 쿨타임이 존재하는 경우
            {
                m_Dictionary_Item_Use_CoolTime.Add(item.m_nItemCode, item); // 적용중인 소비아이템 쿨타임 목록에서 해당 소비아이템 추가. <소비아이템 쿨타임 딕셔너리>
                coroutine_cooltime = StartCoroutine(Process_Item_Use_CoolTime(item, cool)); // 소비아이템 쿨타임을 계산하는 코루틴 시작 및 추가. <소비아이템 쿨타임(코루틴) 딕셔너리>
                m_Dictionary_Coroutine_Item_Use_CoolTime.Add(item.m_nItemCode, coroutine_cooltime);
            }

            return 0;
        }
        // 소비아이템(영구적 버프포션) 로딩(쿨타임)
        if (item.m_eItemUseType == E_ITEM_USE_TYPE.ETERNALBUFFPOTION)
        {
            if (cool > 0) // 해당 소비아이템의 잔여 쿨타임이 존재하는 경우
            {
                m_Dictionary_Item_Use_CoolTime.Add(item.m_nItemCode, item); // 적용중인 소비아이템 쿨타임 목록에서 해당 소비아이템 추가. <소비아이템 쿨타임 딕셔너리>
                coroutine_cooltime = StartCoroutine(Process_Item_Use_CoolTime(item, cool)); // 소비아이템 쿨타임을 계산하는 코루틴 시작 및 추가. <소비아이템 쿨타임(코루틴) 딕셔너리>
                m_Dictionary_Coroutine_Item_Use_CoolTime.Add(item.m_nItemCode, coroutine_cooltime);
            }

            return 0;
        }
        // 소비아이템(일시적 버프포션) 로딩(효과, 지속시간, 쿨타임)
        if (item.m_eItemUseType == E_ITEM_USE_TYPE.TEMPORARYBUFFPOTION)
        {
            m_Dictionary_Item_Use_Buff.Add(item.m_nItemCode, item); // 적용중인 소비아이템 목록에 해당 소비아이템 추가. <소비아이템 효과 딕셔너리>
            coroutine_buff = StartCoroutine(Process_Item_Use_BuffTime(item, duration)); // 소비아이템의 지속시간을 계산하는 코루틴 시작 및 추가. <소비아이템 효과 지속시간(코루틴) 딕셔너리>
																			            // 해당 코루틴이 시작되면 소비아이템의 지속시간 딕셔너리가 갱신(추가, 업데이트). <소비아이템 효과 지속시간(float) 딕셔너리>
            m_Dictionary_Coroutine_Item_Use_Buff.Add(item.m_nItemCode, coroutine_buff);
            if (cool > 0) // 해당 소비아이템의 잔여 쿨타임이 존재하는 경우
            {
                m_Dictionary_Item_Use_CoolTime.Add(item.m_nItemCode, item); // 적용중인 소비아이템 쿨타임 목록에서 해당 소비아이템 추가. <소비아이템 쿨타임 딕셔너리>
                coroutine_cooltime = StartCoroutine(Process_Item_Use_CoolTime(item, cool)); // 소비아이템 쿨타임을 계산하는 코루틴 시작 및 추가. <소비아이템 쿨타임(코루틴) 딕셔너리>
                m_Dictionary_Coroutine_Item_Use_CoolTime.Add(item.m_nItemCode, coroutine_cooltime);
            }

            GUIManager_Total.Instance.UpdateLog("[소비아이템][" + item.m_sItemName + "] 버프 효과 적용."); // 로그GUI에 사용한 소비아이템 정보 출력
            return 0;
        }

        return 0;
    }

    // 소비아이템 사용 시 지속시간을 계산하는 코루틴. <소비아이템 효과 지속시간(코루틴) 딕셔너리> m_Dictionary_Coroutine_Item_Use_Buff 할당
    IEnumerator Process_Item_Use_BuffTime(Item_Use item) // item : 사용한 소비아이템 정보(아이템 코드, 지속시간)
    {
        UpdateStatus_Item_Use_TemporaryBuff(); // 소비아이템(일시적 버프포션) 사용으로 인한 스탯(능력치, 평판) 업데이트
        GUIManager_Total.Instance.Update_SS(); // 스탯GUI 업데이트

        float durationtime = item.m_fDurationTime;

	// 코루틴 실행 시점
        m_Dictionary_Item_Use_Buff_RemainingTime.Add(item.m_nItemCode, durationtime); // 소비아이템의 지속시간 딕셔너리에 해당 소비아이템 추가. <소비아이템 효과 지속시간(float) 딕셔너리>
        while (durationtime > 0) // 해당 소비아이템의 잔여 지속시간이 존재하는 경우
        {
            yield return new WaitForSeconds(0.016f);
            durationtime -= 0.016f;
            m_Dictionary_Item_Use_Buff_RemainingTime[item.m_nItemCode] = durationtime; // 해당 소비아이템의 잔여 지속시간 업데이트. <소비아이템 효과 지속시간(float) 딕셔너리>
        }

 	// 코루틴 종료 시점
        m_Dictionary_Item_Use_Buff.Remove(item.m_nItemCode); // 적용중인 소비아이템 목록에서 해당 소비아이템 제거. <소비아이템 효과 딕셔너리>
        m_Dictionary_Coroutine_Item_Use_Buff.Remove(item.m_nItemCode); // 소비아이템의 지속시간을 계산하는 코루틴 제거. <소비아이템 효과 지속시간(코루틴) 딕셔너리>
        m_Dictionary_Item_Use_Buff_RemainingTime.Remove(item.m_nItemCode); // 소비아이템의 지속시간 딕셔너리에 해당 소비아이템 제거. <소비아이템 효과 지속시간(float) 딕셔너리>

        UpdateStatus_Item_Use_TemporaryBuff(); // 소비아이템(일시적 버프포션) 사용으로 인한 스탯(능력치, 평판) 업데이트
        GUIManager_Total.Instance.Update_SS(); // 스탯GUI 업데이트
    }
    // 로딩 시(게임 시작 시) 소비아이템(효과, 지속시간) 지속시간을 계산하는 코루틴. <소비아이템 효과 지속시간(코루틴) 딕셔너리> m_Dictionary_Coroutine_Item_Use_Buff 할당
    IEnumerator Process_Item_Use_BuffTime(Item_Use item, float duration) // item : 사용한 소비아이템 정보(아이템 코드), duration : 사용한 소비아이템 잔여 지속시간
    {
        UpdateStatus_Item_Use_TemporaryBuff(); // 소비아이템(일시적 버프포션) 사용으로 인한 스탯(능력치, 평판) 업데이트
        GUIManager_Total.Instance.Update_SS(); // 스탯GUI 업데이트

        float durationtime = duration;

        // 코루틴 실행 시점
        m_Dictionary_Item_Use_Buff_RemainingTime.Add(item.m_nItemCode, durationtime); // 소비아이템의 지속시간 딕셔너리에 해당 소비아이템 추가. <소비아이템 효과 지속시간(float) 딕셔너리>
        while (durationtime > 0) // 해당 소비아이템의 잔여 지속시간이 존재하는 경우
        {
            yield return new WaitForSeconds(0.1f);
            durationtime -= 0.1f;
            m_Dictionary_Item_Use_Buff_RemainingTime[item.m_nItemCode] = durationtime; // 해당 소비아이템의 잔여 지속시간 업데이트. <소비아이템 효과 지속시간(float) 딕셔너리>
        }
	
	// 코루틴 종료 시점
        m_Dictionary_Item_Use_Buff.Remove(item.m_nItemCode); // 적용중인 소비아이템 목록에서 해당 소비아이템 제거. <소비아이템 효과 딕셔너리>
        m_Dictionary_Coroutine_Item_Use_Buff.Remove(item.m_nItemCode); // 소비아이템의 지속시간을 계산하는 코루틴 제거. <소비아이템 효과 지속시간(코루틴) 딕셔너리>
        m_Dictionary_Item_Use_Buff_RemainingTime.Remove(item.m_nItemCode); // 소비아이템의 지속시간 딕셔너리에 해당 소비아이템 제거. <소비아이템 효과 지속시간(float) 딕셔너리>

        UpdateStatus_Item_Use_TemporaryBuff(); // 소비아이템(일시적 버프포션) 사용으로 인한 스탯(능력치, 평판) 업데이트
        GUIManager_Total.Instance.Update_SS(); // 스탯GUI 업데이트
    }
    // 소비아이템 사용 시 쿨타임 계산하는 코루틴. <소비아이템 효과 쿨타임(코루틴) 딕셔너리> m_Dictionary_Coroutine_Item_Use_CoolTime 할당
    IEnumerator Process_Item_Use_CoolTime(Item_Use item) // item : 사용한 소비아이템 정보(아이템 코드, 쿨타임)
    {
        float cooltime = item.m_fCoolTime;

	// 코루틴 시작 시점
        m_Dictionary_Item_Use_CoolTime_RemainingTime.Add(item.m_nItemCode, cooltime); // 소비아이템의 쿨타임 딕셔너리에 해당 소비아이템 추가. <소비아이템 효과 쿨타임(float) 딕셔너리>
        while (cooltime > 0) // 해당 소비아이템의 잔여 쿨타임이 존재하는 경우
        {
            yield return new WaitForSeconds(0.1f);
            cooltime -= 0.1f;
            m_Dictionary_Item_Use_CoolTime_RemainingTime[item.m_nItemCode] = cooltime; // 해당 소비아이템의 잔여 쿨타임 업데이트. <소비아이템 효과 쿨타임(float) 딕셔너리>
        }

	// 코루틴 종료 시점
        m_Dictionary_Item_Use_CoolTime.Remove(item.m_nItemCode); // 적용중인 소비아이템 목록에서 해당 소비아이템 제거. <소비아이템 쿨타임 딕셔너리>
        m_Dictionary_Coroutine_Item_Use_CoolTime.Remove(item.m_nItemCode); // 소비아이템의 쿨타임을 계산하는 코루틴 제거. <소비아이템 효과 쿨타임(코루틴) 딕셔너리>
        m_Dictionary_Item_Use_CoolTime_RemainingTime.Remove(item.m_nItemCode); // 소비아이템의 쿨타임 딕셔너리에 해당 소비아이템 제거. <소비아이템 효과 쿨타임(float) 딕셔너리>
    }
    // 로딩 시(게임 시작 시) 소비아이템(효과, 쿨타임) 지속시간을 계산하는 코루틴. <소비아이템 효과 쿨타임(코루틴) 딕셔너리> m_Dictionary_Coroutine_Item_Use_CoolTime 할당
    IEnumerator Process_Item_Use_CoolTime(Item_Use item, float cool)  // item : 사용한 소비아이템 정보(아이템 코드), cool : 사용한 소비아이템 잔여 쿨타임
    {
        float cooltime = cool;

	// 코루틴 시작 시점
        m_Dictionary_Item_Use_CoolTime_RemainingTime.Add(item.m_nItemCode, cooltime); // 소비아이템의 쿨타임 딕셔너리에 해당 소비아이템 추가. <소비아이템 효과 쿨타임(float) 딕셔너리>
        while (cooltime > 0) // 해당 소비아이템의 잔여 쿨타임이 존재하는 경우
        {
            yield return new WaitForSeconds(0.016f);
            cooltime -= 0.016f;
            m_Dictionary_Item_Use_CoolTime_RemainingTime[item.m_nItemCode] = cooltime; // 해당 소비아이템의 잔여 쿨타임 업데이트. <소비아이템 효과 쿨타임(float) 딕셔너리>
        }

	// 코루틴 종료 시점
        m_Dictionary_Item_Use_CoolTime.Remove(item.m_nItemCode); // 적용중인 소비아이템 목록에서 해당 소비아이템 제거. <소비아이템 쿨타임 딕셔너리>
        m_Dictionary_Coroutine_Item_Use_CoolTime.Remove(item.m_nItemCode); // 소비아이템의 쿨타임을 계산하는 코루틴 제거. <소비아이템 효과 쿨타임(코루틴) 딕셔너리>
        m_Dictionary_Item_Use_CoolTime_RemainingTime.Remove(item.m_nItemCode); // 소비아이템의 쿨타임 딕셔너리에 해당 소비아이템 제거. <소비아이템 효과 쿨타임(float) 딕셔너리>
    }

    // 스킬(버프ㆍ디버프, 상태이상) 적용 가능 여부 판단(스탯(능력치, 평판) 상한ㆍ하한, 소모 자원)
    // return 0 : 스킬 적용 가능 / return 1 : 스킬 적용 불가능
    public int CheckCondition_ApplySkill(STATUS stmax, STATUS stmin, SOC somax, SOC somin, STATUS stconsume, SOC soconsume) // stmax : 능력치 상한, stmin : 능력치 하한, somax : 평판 상한, somin : 평판 하한
                                                                                                                            // stconsume : 소모 능력치, soconsume : 소모 평판
    {
        if (m_sStatus.CheckCondition_Max(stmax) == false) // 스킬 적용 조건 : 능력치 상한(플레이어의 능력치 합계가 스킬 적용 조건(능력치 상한)을 초과한 경우 제한)
        {
            Debug.Log("1");
            return 1;
        }
        if (m_sStatus.CheckCondition_Min(stmin) == false) // 스킬 적용 조건 : 능력치 하한(플레이어의 능력치 합계가 스킬 적용 조건(능력치 하한)에 미달한 경우 제한)
        {
            Debug.Log("2");
            return 1;
        }
        if (m_sSoc.CheckCondition_Max(somax) == false) // 스킬 적용 조건 : 평판 상한(플레이어의 평판 합계가 스킬 적용 조건(평판 상한)을 초과한 경우 제한)
        {
            Debug.Log("3");
            return 1;
        }
        if (m_sSoc.CheckCondition_Min(somin) == false) // 스킬 적용 조건 : 평판 하한(플레이어의 평판 합계가 스킬 적용 조건(평판 하한)에 미달한 경우 제한)
        {
            Debug.Log("4");
            return 1;
        }

        if (m_sStatus.CheckCondition_Min(stconsume) == false) // 스킬 적용 조건 : 스킬 적용 시 소모되는 능력치를 플레이어가 가지고 있는지 판단
        {
            Debug.Log("5");
            return 1;
        }
        //if (m_sSoc.CheckCondition_Min(soconsume) == false) // 스킬 적용 조건 : 스킬 적용 시 소모되는 평판을 플레이어가 가지고 있는지 판단
        //{
        //    Debug.Log("6");
        //    return 1;
        //}

        return 0;
    }
    // 스킬(버프ㆍ디버프, 상태이상) 적용 시 스탯(능력치, 평판), 상태이상 업데이트
    public void ApplySkill(int ncode, Skill_SSEffect skse) // ncode : 적용할 스킬 코드, skse : 스킬 효과(스탯(능력치, 평판))
    {
        if (m_sDictionary_Skill_SSEffect_Apply.ContainsKey(ncode) == false)
            m_sDictionary_Skill_SSEffect_Apply.Add(ncode, skse); // 플레이어에게 적용중인 스킬 효과 딕셔너리에 해당 스킬 효과 추가

        UpdateStatus_ApplySkill(); // 스킬 적용으로 인한 능력치 업데이트
        UpdateSoc_ApplySkill(); // 스킬 적용으로 인한 평판 업데이트
    }
    // 스킬(버프ㆍ디버프) 해제 시 스탯(능력치, 평판) 업데이트
    public void UnApplySkill(int ncode) // ncode : 사용 중단할 스킬코드
    {
        m_sDictionary_Skill_SSEffect_Apply.Remove(ncode); // 플레이어에게 적용중인 스킬 효과 딕셔너리에서 해당 스킬 제거

        UpdateStatus_ApplySkill(); // 스킬 적용으로 인한 능력치 업데이트
        UpdateSoc_ApplySkill(); // 스킬 적용으로 인한 평판 업데이트
    }
    // 스킬(상태이상) 해제 시 스탯(능력치, 평판) 업데이트
    public void UnApplySkill_Condition_Effect(int ncode) // ncode : 해제할 스킬(상태이상)코드
    {
        UpdateStatus_ApplySkill(); // 스킬 적용으로 인한 능력치 업데이트
        UpdateSoc_ApplySkill(); // 스킬 적용으로 인한 평판 업데이트
    }
}

