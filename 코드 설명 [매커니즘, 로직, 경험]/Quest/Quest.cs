using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_QUEST_LEVEL { S1, S2, S3, S4, S5, S6, S7, S8, S9, S10 }
public enum E_QUEST_REPEAT { ONCE, FINITE, INFINITE }
public enum E_QUEST_TYPE { NULL, MOVE, ATTACK, ATTACK1_1, ATTACK1_2, ATTACK1_3, ATTACKED, GOAWAY_MONSTER, GOAWAY_TYPE, KILL_MONSTER, KILL_TYPE, COLLECT, CONVERSATION, ROLL, ELIMINATE_MONSTER, ELIMINATE_TYPE }
// Quest_Type 개발 현황.
// KILL_MONSTER 0 ~ 999
// KILL_TYPE 1000 ~ 1999
// GOAWAY_MONSTER 2000 ~ 2999
// GOAWAY_TYPE 3000 ~ 3999
// COLLECT 4000 ~ 4999
// CONVERSATION 5000 ~ 5999
// ROLL 6000 ~ 6999
// ELIMINATE_MONSTER 7000 ~ 7999
// ELIMINATE_TYPE 8000 ~ 8999

public class Quest : MonoBehaviour
{
    //// KILL, GOAWAY 타입 퀘스트
    //public List<int> m_nl_MonsterCode;
    //public List<int> m_nl_Count_Max;
    //public List<int> m_nl_Count_Current;
    // m_nNPC 은 퀘스트 발행인
    public int m_nNPC;
    // 퀘스트 클리어 가능 NPC
    public int m_nNPC_Clear;
    //// COLLECT 타입 퀘스트
    //public List<int> m_nl_ItemCode;
    //public List<int> m_nl_ItemCount_Max;
    //public List<int> m_nl_ItemCount_Current;

    public int m_nQuest_Code;

    // 퀘스트 로드맵 순서(추천 퀘스트 로드맵 출력 순서)
    public int m_nQuest_Loadmap_Code;

    public string m_sQuest_Title;
    // 퀘스트 부여 대사
    public List<string> m_sl_QuestDescription_Context;
    // 퀘스트 간단 정보
    public List<string> m_sl_QuestDescription_Simple;
    // 퀘스트 수락시 대사
    public List<string> m_sl_QuestOk_Context;
    // 퀘스트 거절시 대사
    public List<string> m_sl_QuestNo_Context;
    // 퀘스트 도중 대사
    public List<string> m_sl_QuestProgress_Context;
    // 퀘스트 클리어 대사
    public List<string> m_sl_QuestClear_Context;

    public E_QUEST_LEVEL m_eQuestLevel;

    // 퀘스트 타입
    // KILL_TYPE: m_nl_Count_Max, m_nl_Count_Current 의 크기는 1로 한정.
    // KILL_MONSTER: 조건에 맞게 m_nl_Count_Max, m_nl_Count_Current 크기 리스트로 지정. 
    public E_QUEST_TYPE m_eQuestType;

    // 퀘스트 진행, 클리어 순서.
    public int m_nQuestOrder;

    // 플레이어가 퀘스트를 진행중인지?
    public bool m_bProcess;
    // 플레이어가 퀘스트 클리어를 위한 조건을 모두 달성했는지?
    public bool m_bCondition;
    // 플레이어가 퀘스트를 클리어 했는지?
    public bool m_bClear;

    // 연계 퀘스트 사전 조건 체크
    // 사전 필수 퀘스트 리스트
    public List<Quest> m_ql_Quest_Necessity_Clear;
    // 해당 퀘스트 클리어 기록이 존재하면 퀘스트X
    public List<Quest> m_ql_Quest_Necessity_NonClear;
    // 해당 퀘스트를 진행중이면 퀘스트O
    public List<Quest> m_ql_Quest_Necessity_Process;
    // 해당 퀘스트를 진행중이면 퀘스트X
    public List<Quest> m_ql_Quest_Necessity_NonProcess;
    public STATUS m_sStatus_Necessity_Up;
    public STATUS m_sStatus_Necessity_Down;
    public SOC m_sSoc_Necessity_Up;
    public SOC m_sSoc_Necessity_Down;

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