using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// ※ 기반이 되는 Quest 클래스를 구현한 후
//    Quest_KILL_MONSTER(특정 몬스터 토벌), Quest_KILL_TYPE(특정 몬스터 타입 토벌), Quest_GOAWAY_MONSTER(특정 몬스터 놓아주기), Quest_GOAWAY_TYPE(특정 몬스터 타입 놓아주기), 
//    Quest_COLLECT(수집), Quest_CONVERSATION(대화), Quest_ROLL(구르기), Quest_ELIMINATE_MONSER(특정 몬스터 제거(토벌 + 놓아주기)), Quest_ELIMINATE_TYPE(특정 몬스터 타입 제거(토벌 + 놓아주기))
//    클래스를 상속으로 구현했다.
//

// 퀘스트 타입 : { NULL, 이동, 공격, 기본 공격1, 기본 공격2, 기본 공격3, 피격, 특정 몬스터 토벌, 특정 몬스터 타입 토벌, 특정 몬스터 놓아주기, 특정 몬스터 타입 놓아주기, 수집, 대화, 구르기, 특정 몬스터 제거(토벌 + 놓아주기), 특정 몬스터 타입 제거(토벌 + 놓아주기)}
public enum E_QUEST_TYPE { NULL, MOVE, ATTACK, ATTACK1_1, ATTACK1_2, ATTACK1_3, ATTACKED, KILL_MONSTER, KILL_TYPE, GOAWAY_MONSTER, GOAWAY_TYPE, COLLECT, CONVERSATION, ROLL, ELIMINATE_MONSTER, ELIMINATE_TYPE }
// 퀘스트 등급(난이도) : { S1, S2, S3, S4, S5, S6, S7, S8, S0, S10 }
public enum E_QUEST_LEVEL { S1, S2, S3, S4, S5, S6, S7, S8, S9, S10 }
// 퀘스트 반복 여부 : { 단 한번 수행, 무제한 수행, 제한 수행 }
public enum E_QUEST_REPEAT { ONCE, FINITE, INFINITE }

//
// ※ Quest 클래스는 가장 공들여 설계한 클래스 중 하나이다. 상속과 가상함수(오버라이딩)을 이용해 다양한 퀘스트를 구현했다.
//    인터페이스를 이용한 효율적인 메모리 관리법을 생각하고 있다.
//

public class Quest : MonoBehaviour
{
    public string m_sQuest_Title;       // 퀘스트 제목
    
    public int m_nQuest_Code;           // 퀘스트 고유코드
                                        // ※ KILL_MONSTER(특정 몬스터 토벌) 고유코드 : 0 ~ 999
                                        //    KILL_TYPE(특정 몬스터 타입 토벌) 고유코드 : 1000 ~ 1999
                                        //    GOAWAY_MONSTER(특정 몬스터 놓아주기) 고유코드 : 2000 ~ 2999
                                        //    GOAWAY_TYPE(특정 몬스터 타입 놓아주기) 고유코드 : 3000 ~ 3999
                                        //    COLLECT(수집) 고유코드 : 4000 ~ 4999
                                        //    CONVERSATION(대화) 고유코드 : 5000 ~ 5999
                                        //    ROLL(구르기) 고유코드 : 6000 ~ 6999
                                        //    ELIMINATE_MONSTER(특정 몬스터 제거) 고유코드 : 7000 ~ 7999
                                        //    ELIMINATE_TYPE(특정 몬스터 타입 제거) 고유코드 : 8000 ~ 8999
                                      
    public int m_nQuestOrder;           // 퀘스트 진행 및 퀘스트 완료 순서(해당 순서에 따라 퀘스트GUI가 업데이트 된다.)
    public int m_nQuest_Loadmap_Code;   // 퀘스트 로드맵 순서(추천 퀘스트 출력 순서)

    public E_QUEST_TYPE m_eQuestType;   // 퀘스트 타입
    public E_QUEST_LEVEL m_eQuestLevel; // 퀘스트 등급(난이도)

    public int m_nNPC;       // 퀘스트 발행 NPC
    public int m_nNPC_Clear; // 퀘스트 완료 NPC

    // 퀘스트 대화 스크립트 관련 변수
    public List<string> m_sl_QuestDescription_Context; // 퀘스트 발행 시 대화 스크립트
    public List<string> m_sl_QuestDescription_Simple;  // 퀘스트 간단 설명
    public List<string> m_sl_QuestOk_Context;          // 퀘스트 수락 시 대화 스크립트
    public List<string> m_sl_QuestNo_Context;          // 퀘스트 거절 시 대화 스크립트
    public List<string> m_sl_QuestProgress_Context;    // 퀘스트 진행중일 시 대화 스크립트(퀘스트 완료 조건을 충족시키지 않은 경우 출력되는 대화 스크립트)
    public List<string> m_sl_QuestClear_Context;       // 퀘스트 완료 시 대화 스크립트

    // 퀘스트 진행 상태 관련 변수
    public bool m_bProcess;   // 퀘스트 진행 여부(플레이어가 현재 해당 퀘스트를 진행 중인지?)
    public bool m_bCondition; // 퀘스트 완료 조건 달성 여부(플레이어가 현재 해당 퀘스트를 완료할 조건을 충족시켰는지?)
    public bool m_bClear;     // 퀘스트 완료 여부(플레이어가 현재 해당 퀘스트를 완료 했는지?)

    // 퀘스트 진행 사전 조건 관련 변수
    // 퀘스트 진행 사전 조건 - 스탯(능력치) 상한ㆍ하한
    public STATUS m_sStatus_Necessity_Up;   // 스탯(능력치) 상한(플레이어의 스탯(능력치) 합계가 퀘스트 진행 사전 조건(해당 조건)을 초과한 경우 제한)
    public STATUS m_sStatus_Necessity_Down; // 스탯(능력치) 하한(플레이어의 스탯(능력치) 합계가 퀘스트 진행 사전 조건(해당 조건)에 미달한 경우 제한)
    // 퀘스트 진행 사전 조건 - 스탯(평판) 상한ㆍ하한
    public SOC m_sSoc_Necessity_Up;         // 스탯(평판) 상한(플레이어의 스탯(평판) 합계가 퀘스트 진행 사전 조건(해당 조건)을 초과한 경우 제한)
    public SOC m_sSoc_Necessity_Down;       // 스탯(평판) 하한(플레이어의 스탯(평판) 합계가 퀘스트 진행 사전 조건(해당 조건)에 미달한 경우 제한)
    // 퀘스트 진행 사전 조건 - 연계 퀘스트
    public List<Quest> m_ql_Quest_Necessity_Clear;      // 사전 조건 - 완료 퀘스트(해당 리스트에 포함된 퀘스트의 완료 여부 판단)
    public List<Quest> m_ql_Quest_Necessity_NonClear;   // 사전 조건 - 미완료 퀘스트(해당 리스트에 포함된 퀘스트의 미완료 여부 판단)
    public List<Quest> m_ql_Quest_Necessity_Process;    // 사전 조건 - 진행 퀘스트(해당 리스트에 포함된 퀘스트의 진행 여부 판단)
    public List<Quest> m_ql_Quest_Necessity_NonProcess; // 사전 조건 - 미진행 퀘스트(해당 리스트에 포함된 퀘스트의 미진행 여부 판단)

    // 퀘스트 클리어 보상
    public List<Item_Equip> m_lRewardList_Item_Equip;
    public List<Item_Use> m_lRewardList_Item_Use;
    public List<Item_Etc> m_lRewardList_Item_Etc;
    public STATUS m_sRewardSTATUS;
    public SOC m_sRewardSOC;
    public int m_nRewardGold;

    // 퀘스트 클리어 시점
    public int m_nClearDay;

    // 히든 퀘스트의 정보 출력 여부.
    public bool m_bQuest_Information_Process_Hide;

    // 추천 퀘스트 정보.
    public string m_sQuest_Information_Recommend;
    // 퀘스트 진행도중 퀘스트 정보.
    public string m_sQuest_Information_Process;
    // 퀘스트 완료 가능시 퀘스트 정보.
    public string m_sQuest_Information_Condition;
    // 퀘스트 완료 후 퀘스트 정보.
    public string m_sQuest_Information_Clear;

    public Quest()
    {
        InitialSet();
        //m_nl_MonsterCode = new List<int>();
        //m_nl_Count_Max = new List<int>();
        //m_nl_Count_Current = new List<int>();
        //m_nl_ItemCode = new List<int>();
        //m_nl_ItemCount_Max = new List<int>();
        //m_nl_ItemCount_Current = new List<int>();
    }

    // Quest 초기 설정.
    protected void InitialSet()
    {
        m_bProcess = false;
        m_bCondition = false;
        m_bClear = false;

        m_sl_QuestDescription_Context = new List<string>();
        m_sl_QuestDescription_Simple = new List<string>();
        m_sl_QuestOk_Context = new List<string>();
        m_sl_QuestNo_Context = new List<string>();
        m_sl_QuestProgress_Context = new List<string>();
        m_sl_QuestClear_Context = new List<string>();
        m_ql_Quest_Necessity_Clear = new List<Quest>();
        m_ql_Quest_Necessity_NonClear = new List<Quest>();
        m_ql_Quest_Necessity_Process = new List<Quest>();
        m_ql_Quest_Necessity_NonProcess = new List<Quest>();
        m_lRewardList_Item_Equip = new List<Item_Equip>();
        m_lRewardList_Item_Use = new List<Item_Use>();
        m_lRewardList_Item_Etc = new List<Item_Etc>();

        m_sStatus_Necessity_Up = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        m_sStatus_Necessity_Down = new STATUS(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        m_sSoc_Necessity_Up = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        m_sSoc_Necessity_Down = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
    }

    // Quest 대화 스크립트 설정.
    // Quest 대화.
    public void AddQuestDescription_Context(string str)
    {
        m_sl_QuestDescription_Context.Add(str);
    }
    // Quest 수락.
    public void AddQuestOk_Context(string str)
    {
        m_sl_QuestOk_Context.Add(str);
    }
    // Quest 거절.
    public void AddQuestNo_Context(string str)
    {
        m_sl_QuestNo_Context.Add(str);
    }
    // Quest 진행중.
    public void AddQuestProgress_Context(string str)
    {
        m_sl_QuestProgress_Context.Add(str);
    }
    // Quest 완료.
    public void AddQuestClear_Context(string str)
    {
        m_sl_QuestClear_Context.Add(str);
    }

    // Quest 보상 Item 추가.
    public void AddQuestClearReward_Item(Item_Equip item)
    {
        m_lRewardList_Item_Equip.Add(item);
    }
    public void AddQuestClearReward_Item(Item_Use item)
    {
        m_lRewardList_Item_Use.Add(item);
    }
    public void AddQuestClearReward_Item(Item_Etc item)
    {
        m_lRewardList_Item_Etc.Add(item);
    }

    // Quest 리셋
    public void ResetQuest()
    {
        m_bClear = false;
        m_bProcess = false;
        m_bCondition = false;
    }
    
    // 공격 횟수에 관한 퀘스트
    virtual public void Check_ATTACK_Count()
    {

    }

    // 퀘스트 사전 요구조건
    virtual public bool Check_Condition_Total()
    {
        if (Check_Condition_Connection() == true)
        {

        }
        else
            return false;
        if (Check_Condition_SOC() == true)
        {

        }
        else
            return false;
        if (Check_Condition_STATUS() == true)
        {

        }
        else
            return false;

        return true;
    }

    // 퀘스트 사전 요구조건_연계
    virtual public bool Check_Condition_Connection()
    {
        // 이 퀘스트를 수행하기위해 사전 퀘스트가 클리어되야 할때 조건 체크
        for (int i = 0; i < m_ql_Quest_Necessity_Clear.Count; i++)
        {
            if (m_ql_Quest_Necessity_Clear[i].m_bClear == false)
                return false;
            else
                continue;
        }

        // 이 퀘스트를 수행하기위해 사전 퀘스트가 클리어되지 말아야 할때 조건 체크
        for (int i = 0; i < m_ql_Quest_Necessity_NonClear.Count; i++)
        {
            if (m_ql_Quest_Necessity_NonClear[i].m_bClear == true)
                return false;
            else
                continue;
        }

        // 이 퀘스트를 수행하기위해 특정 퀘스트가 진행중이어야할때
        for (int i = 0; i < m_ql_Quest_Necessity_Process.Count; i++)
        {
            if (m_ql_Quest_Necessity_Process[i].m_bProcess == false)
                return false;
            else
                continue;
        }

        for (int i = 0; i < m_ql_Quest_Necessity_NonProcess.Count; i++)
        {
            if (m_ql_Quest_Necessity_NonProcess[i].m_bProcess == true)
                return false;
            else
                continue;
        }

        return true;
    }
    virtual public bool Check_Condition_SOC()
    {
        if (Player_Total.Instance.m_ps_Status.m_sSoc.CheckCondition_Min(m_sSoc_Necessity_Down) == true &&
            Player_Total.Instance.m_ps_Status.m_sSoc.CheckCondition_Max(m_sSoc_Necessity_Up) == true)
            return true;
        else
            return false;
    }
    virtual public bool Check_Condition_STATUS()
    {
        if (Player_Total.Instance.m_ps_Status.m_sStatus.CheckCondition_Min(m_sStatus_Necessity_Down) == true &&
            Player_Total.Instance.m_ps_Status.m_sStatus.CheckCondition_Max(m_sStatus_Necessity_Up) == true)
            return true;
        else
            return false;
    }
}
