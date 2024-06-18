using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class Player_Quest : MonoBehaviour
{
    // 진행중인 퀘스트와 완료한 퀘스트의 경우 List를 사용하여 관리
    // 진행중인 퀘스트
    public static List<Quest_KILL_MONSTER> m_lQuestList_Progress_KILL_MONSTER;           // 퀘스트 타입 : 특정 몬스터 토벌
    public static List<Quest_KILL_TYPE> m_lQuestList_Progress_KILL_TYPE;                 // 퀘스트 타입 : 특정 타입의 몬스터 토벌
    public static List<Quest_GOAWAY_MONSTER> m_lQuestList_Progress_GOAWAY_MONSTER;       // 퀘스트 타입 : 특정 몬스터 놓아주기
    public static List<Quest_GOAWAY_TYPE> m_lQuestList_Progress_GOAWAY_TYPE;             // 퀘스트 타입 : 특정 타입의 몬스터 놓아주기
    public static List<Quest_COLLECT> m_lQuestList_Progress_COLLECT;                     // 퀘스트 타입 : 수집
    public static List<Quest_CONVERSATION> m_lQuestList_Progress_CONVERSATION;           // 퀘스트 타입 : 대화
    public static List<Quest_ROLL> m_lQuestList_Progress_ROLL;                           // 퀘스트 타입 : 구르기
    public static List<Quest_ELIMINATE_MONSTER> m_lQuestList_Progress_ELIMINATE_MONSTER; // 퀘스트 타입 : 특정 몬스터 제거
    public static List<Quest_ELIMINATE_TYPE> m_lQuestList_Progress_ELIMINATE_TYPE;       // 퀘스트 타입 : 특정 타입의 몬스터 제거
    // 완료한 퀘스트
    public static List<Quest_KILL_MONSTER> m_lQuestList_Complete_KILL_MONSTER;           // 퀘스트 타입 : 특정 몬스터 토벌
    public static List<Quest_KILL_TYPE> m_lQuestList_Complete_KILL_TYPE;                 // 퀘스트 타입 : 특정 타입의 몬스터 토벌
    public static List<Quest_GOAWAY_MONSTER> m_lQuestList_Complete_GOAWAY_MONSTER;       // 퀘스트 타입 : 특정 몬스터 놓아주기
    public static List<Quest_GOAWAY_TYPE> m_lQuestList_Complete_GOAWAY_TYPE;             // 퀘스트 타입 : 특정 타입의 몬스터 놓아주기
    public static List<Quest_COLLECT> m_lQuestList_Complete_COLLECT;                     // 퀘스트 타입 : 수집
    public static List<Quest_CONVERSATION> m_lQuestList_Complete_CONVERSATION;           // 퀘스트 타입 : 대화
    public static List<Quest_ROLL> m_lQuestList_Complete_ROLL;                           // 퀘스트 타입 : 구르기
    public static List<Quest_ELIMINATE_MONSTER> m_lQuestList_Complete_ELIMINATE_MONSTER; // 퀘스트 타입 : 특정 몬스터 제거
    public static List<Quest_ELIMINATE_TYPE> m_lQuestList_Complete_ELIMINATE_TYPE;       // 퀘스트 타입 : 특정 타입의 몬스터 제거

    public void InitialSet()
    {
        m_lQuestList_Progress_KILL_MONSTER = new List<Quest_KILL_MONSTER>();
        m_lQuestList_Complete_KILL_MONSTER = new List<Quest_KILL_MONSTER>();
        m_lQuestList_Progress_KILL_TYPE = new List<Quest_KILL_TYPE>();
        m_lQuestList_Complete_KILL_TYPE = new List<Quest_KILL_TYPE>();
        m_lQuestList_Progress_GOAWAY_MONSTER = new List<Quest_GOAWAY_MONSTER>();
        m_lQuestList_Complete_GOAWAY_MONSTER = new List<Quest_GOAWAY_MONSTER>();
        m_lQuestList_Progress_GOAWAY_TYPE = new List<Quest_GOAWAY_TYPE>();
        m_lQuestList_Complete_GOAWAY_TYPE = new List<Quest_GOAWAY_TYPE>();
        m_lQuestList_Progress_COLLECT = new List<Quest_COLLECT>();
        m_lQuestList_Complete_COLLECT = new List<Quest_COLLECT>();
        m_lQuestList_Progress_CONVERSATION = new List<Quest_CONVERSATION>();
        m_lQuestList_Complete_CONVERSATION = new List<Quest_CONVERSATION>();
        m_lQuestList_Progress_ROLL = new List<Quest_ROLL>();
        m_lQuestList_Complete_ROLL = new List<Quest_ROLL>();
        m_lQuestList_Progress_ELIMINATE_MONSTER = new List<Quest_ELIMINATE_MONSTER>();
        m_lQuestList_Complete_ELIMINATE_MONSTER = new List<Quest_ELIMINATE_MONSTER>();
        m_lQuestList_Progress_ELIMINATE_TYPE = new List<Quest_ELIMINATE_TYPE>();
        m_lQuestList_Complete_ELIMINATE_TYPE = new List<Quest_ELIMINATE_TYPE>();
    }

    // 진행중인 퀘스트 현황을 업데이트하는 함수
    // 퀘스트 타입 : 특정 몬스터 토벌, 특정 타입의 몬스터 토벌
    public void QuestUpdate_Kill(E_MONSTER_KIND mk, int code) // mk : 몬스터 타입, code : 몬스터 코드
    {
        // 특정 몬스터 토벌 퀘스트 현황 업데이트
        for (int i = 0; i < m_lQuestList_Progress_KILL_MONSTER.Count; i++) // 해당하는 퀘스트 타입의 진행중인 모든 퀘스트를 조사
        {
            var item = m_lQuestList_Progress_KILL_MONSTER[i].m_sQuest_Title.Split('\n'); // 퀘스트의 정보(분류, 제목, 난이도)
            if (m_lQuestList_Progress_KILL_MONSTER[i].m_bCondition == false) // 퀘스트가 완료 가능하지 않을때(퀘스트 완료 조건 미충족)
            {
                if (m_lQuestList_Progress_KILL_MONSTER[i].Check_KILL_MONSTER(code) == true) // 1. 몬스터 코드(code)가 퀘스트와 관련 있는지 판단(return true : 관련있음 / return false : 관련없음)
                                                                                            // 2. 퀘스트 완료 조건 판단
                {
                    // 로그GUI에 퀘스트 현황 업데이트 정보 출력
                    for (int j = 0; j < m_lQuestList_Progress_KILL_MONSTER[i].m_nl_Count_Current.Count; j++) // 모든 퀘스트 완료 조건 조사
                    {
                        if (m_lQuestList_Progress_KILL_MONSTER[i].m_nl_MonsterCode[j] == code) // 해당하는 퀘스트 완료 조건 현황만 출력한다. 다른 퀘스트 완료 조건의 현황은 출력하지 않는다.
                            if (m_lQuestList_Progress_KILL_MONSTER[i].m_nl_Count_Max[j] >= m_lQuestList_Progress_KILL_MONSTER[i].m_nl_Count_Current[j]) // 불필요한 정보 출력은 하지 않는다.
                            {
                                GUIManager_Total.Instance.UpdateLog("[" + item[1] + "][" + MonsterManager.m_Dictionary_Monster[code].m_sMonster_Name + "] " + m_lQuestList_Progress_KILL_MONSTER[i].m_nl_Count_Current[j] + " / " + m_lQuestList_Progress_KILL_MONSTER[i].m_nl_Count_Max[j]);
                            }
                    }
                }
                // 로그GUI에 퀘스트 완료 가능 정보 출력(퀘스트 완료 조건 충족)
                if (m_lQuestList_Progress_KILL_MONSTER[i].m_bCondition == true)
                {
                    GUIManager_Total.Instance.UpdateLog("[" + item[1] + "] 완료");
                    GUIManager_Total.Instance.Display_GUI_QuestStateInfo(m_lQuestList_Progress_KILL_MONSTER[i]); // 퀘스트 완료 가능 알림
                }
            }
            // 퀘스트와 관련된 NPC(퀘스트 발행 NPC, 퀘스트 완료 NPC)의 퀘스트 아이콘 업데이트
            if (NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_KILL_MONSTER[i].m_nNPC] != null)
            {
                // 퀘스트 발행 NPC == 퀘스트 완료 NPC
                if (m_lQuestList_Progress_KILL_MONSTER[i].m_nNPC == m_lQuestList_Progress_KILL_MONSTER[i].m_nNPC_Clear)
                {
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_KILL_MONSTER[i].m_nNPC].UpdateIcon(); // NPC의 퀘스트 아이콘 : 퀘스트 완료 가능
                }
                // 퀘스트 발행 NPC != 퀘스트 완료 NPC
                else
                {
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_KILL_MONSTER[i].m_nNPC].UpdateIcon(); // 퀘스트 발행 NPC의 퀘스트 아이콘 : 퀘스트 진행중(해당 NPC를 통해 퀘스트 완료 불가능)
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_KILL_MONSTER[i].m_nNPC_Clear].UpdateIcon(); // 퀘스트 완료 NPC의 퀘스트 아이콘 : 퀘스트 완료 가능
                }
            }

            GUIManager_Total.Instance.Update_Quest_Information(m_lQuestList_Progress_KILL_MONSTER[i], 1); // 퀘스트GUI 업데이트
        }
        // 특정 타입의 몬스터 토벌 퀘스트 현황 업데이트
        for (int i = 0; i < m_lQuestList_Progress_KILL_TYPE.Count; i++) // 해당하는 타입의 진행중인 모든 퀘스트를 조사
        {
            var item = m_lQuestList_Progress_KILL_TYPE[i].m_sQuest_Title.Split('\n'); // 퀘스트의 정보(분류, 제목, 난이도)
            if (m_lQuestList_Progress_KILL_TYPE[i].m_bCondition == false) // 퀘스트가 완료 가능하지 않을때(퀘스트 완료 조건 미충족)
            {
                if (m_lQuestList_Progress_KILL_TYPE[i].Check_KILL_TYPE(mk) == true) // 1. 몬스터 코드(code)가 퀘스트와 관련 있는지 판단(return true : 관련있음 / return false : 관련없음)
                                                                                    // 2. 퀘스트 완료 조건 판단
                {
                    // 로그GUI에 퀘스트 현황 업데이트 정보 출력
                    // 퀘스트 완료 조건이 한가지만 존재
                    if (m_lQuestList_Progress_KILL_TYPE[i].m_nCount_Max != m_lQuestList_Progress_KILL_TYPE[i].m_nCount_Current)
                    {
                        GUIManager_Total.Instance.UpdateLog("[" + item[1] + "] " + "[" + m_lQuestList_Progress_KILL_TYPE[i].m_eMonsterType + "] "+ m_lQuestList_Progress_KILL_TYPE[i].m_nCount_Current + " / " + m_lQuestList_Progress_KILL_TYPE[i].m_nCount_Max);
                    }
                }
                // 로그GUI에 퀘스트 완료 가능 정보 출력(퀘스트 완료 조건 충족)
                if (m_lQuestList_Progress_KILL_TYPE[i].m_bCondition == true)
                {
                    GUIManager_Total.Instance.UpdateLog("[" + item[1] + "] 완료");
                    GUIManager_Total.Instance.Display_GUI_QuestStateInfo(m_lQuestList_Progress_KILL_TYPE[i]); // 퀘스트 완료 가능 알림
                }
            }
            // 퀘스트와 관련된 NPC(퀘스트 발행 NPC, 퀘스트 완료 NPC)의 퀘스트 아이콘 업데이트
            if (NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_KILL_TYPE[i].m_nNPC] != null)
            {
                // 퀘스트 발행 NPC == 퀘스트 완료 NPC
                if (m_lQuestList_Progress_KILL_TYPE[i].m_nNPC == m_lQuestList_Progress_KILL_TYPE[i].m_nNPC_Clear)
                {
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_KILL_TYPE[i].m_nNPC].UpdateIcon(); // NPC의 퀘스트 아이콘 : 퀘스트 완료 가능
                }
                // 퀘스트 발행 NPC != 퀘스트 완료 NPC
                else
                {
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_KILL_TYPE[i].m_nNPC].UpdateIcon(); // 퀘스트 발행 NPC의 퀘스트 아이콘 : 퀘스트 진행중(해당 NPC를 통해 퀘스트 완료 불가능)
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_KILL_TYPE[i].m_nNPC_Clear].UpdateIcon(); // 퀘스트 완료 NPC의 퀘스트 아이콘 : 퀘스트 완료 가능
                }
            }

            GUIManager_Total.Instance.Update_Quest_Information(m_lQuestList_Progress_KILL_TYPE[i], 1); // 퀘스트GUI 업데이트
        }
    }
    
    // 퀘스트 타입 : 특정 몬스터 놓아주기, 특정 타입의 몬스터 놓아주기
    public void QuestUpdate_Goaway(E_MONSTER_KIND mk, int code) // mk : 몬스터 타입, code : 몬스터 코드
    {
        // 특정 몬스터 놓아주기 퀘스트 현황 업데이트
        for (int i = 0; i < m_lQuestList_Progress_GOAWAY_MONSTER.Count; i++) // 해당하는 퀘스트 타입의 진행중인 모든 퀘스트를 조사
        {
            var item = m_lQuestList_Progress_GOAWAY_MONSTER[i].m_sQuest_Title.Split('\n'); // 퀘스트의 정보(분류, 제목, 난이도)
            if (m_lQuestList_Progress_GOAWAY_MONSTER[i].m_bCondition == false) // 퀘스트가 완료 가능하지 않을때(퀘스트 완료 조건 미충족)
            {
                if (m_lQuestList_Progress_GOAWAY_MONSTER[i].Check_GOAWAY_MONSTER(code) == true) // 1. 몬스터 코드(code)가 퀘스트와 관련 있는지 판단(return true : 관련있음 / return false : 관련없음)
                                                                                                // 2. 퀘스트 완료 조건 판단
                {
                    // 로그GUI에 퀘스트 현황 업데이트 정보 출력
                    for (int j = 0; j < m_lQuestList_Progress_GOAWAY_MONSTER[i].m_nl_Count_Current.Count; j++) // 모든 퀘스트 완료 조건 조사
                    {
                        if (m_lQuestList_Progress_GOAWAY_MONSTER[i].m_nl_MonsterCode[j] == code) // 해당하는 퀘스트 완료 조건 현황만 출력한다. 다른 퀘스트 완료 조건의 현황은 출력하지 않는다.
                            if (m_lQuestList_Progress_GOAWAY_MONSTER[i].m_nl_Count_Max[j] >= m_lQuestList_Progress_GOAWAY_MONSTER[i].m_nl_Count_Current[j]) // 불필요한 정보 출력은 하지 않는다.
                            {
                                GUIManager_Total.Instance.UpdateLog("[" + item[1] + "][" + MonsterManager.m_Dictionary_Monster[code].m_sMonster_Name + "] " + m_lQuestList_Progress_GOAWAY_MONSTER[i].m_nl_Count_Current[j] + " / " + m_lQuestList_Progress_GOAWAY_MONSTER[i].m_nl_Count_Max[j]);
                            }
                    }
                }
                // 로그GUI에 퀘스트 완료 가능 정보 출력(퀘스트 완료 조건 충족)
                if (m_lQuestList_Progress_GOAWAY_MONSTER[i].m_bCondition == true)
                {
                    GUIManager_Total.Instance.UpdateLog("[" + item[1] + "] 완료");
                    GUIManager_Total.Instance.Display_GUI_QuestStateInfo(m_lQuestList_Progress_GOAWAY_MONSTER[i]); // 퀘스트 완료 가능 알림

                }
            }
            // 퀘스트와 관련된 NPC(퀘스트 발행 NPC, 퀘스트 완료 NPC)의 퀘스트 아이콘 업데이트
            if (NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_GOAWAY_MONSTER[i].m_nNPC] != null)
            {
                // 퀘스트 발행 NPC == 퀘스트 완료 NPC
                if (m_lQuestList_Progress_GOAWAY_MONSTER[i].m_nNPC == m_lQuestList_Progress_GOAWAY_MONSTER[i].m_nNPC_Clear)
                {
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_GOAWAY_MONSTER[i].m_nNPC].UpdateIcon(); // NPC의 퀘스트 아이콘 : 퀘스트 완료 가능
                }
                // 퀘스트 발행 NPC != 퀘스트 완료 NPC
                else
                {
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_GOAWAY_MONSTER[i].m_nNPC].UpdateIcon(); // 퀘스트 발행 NPC의 퀘스트 아이콘 : 퀘스트 진행중(해당 NPC를 통해 퀘스트 완료 불가능)
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_GOAWAY_MONSTER[i].m_nNPC_Clear].UpdateIcon(); // 퀘스트 완료 NPC의 퀘스트 아이콘 : 퀘스트 완료 가능
                }
            }

            GUIManager_Total.Instance.Update_Quest_Information(m_lQuestList_Progress_GOAWAY_MONSTER[i], 1); // 퀘스트GUI 업데이트
        }
        // 특정 타입의 몬스터 놓아주기 퀘스트 현황 업데이트
        for (int i = 0; i < m_lQuestList_Progress_GOAWAY_TYPE.Count; i++) // 해당하는 타입의 진행중인 모든 퀘스트를 조사
        {
            var item = m_lQuestList_Progress_GOAWAY_TYPE[i].m_sQuest_Title.Split('\n'); // 퀘스트의 정보(분류, 제목, 난이도)
            if (m_lQuestList_Progress_GOAWAY_TYPE[i].m_bCondition == false) // 퀘스트가 완료 가능하지 않을때(퀘스트 완료 조건 미충족)
            {
                if (m_lQuestList_Progress_GOAWAY_TYPE[i].Check_GOAWAY_TYPE(mk) == true) // 1. 몬스터 코드(code)가 퀘스트와 관련 있는지 판단(return true : 관련있음 / return false : 관련없음)
                                                                                        // 2. 퀘스트 완료 조건 판단
                {
                    // 로그GUI에 퀘스트 현황 업데이트 정보 출력
                    // 퀘스트 완료 조건이 한가지만 존재
                    if (m_lQuestList_Progress_GOAWAY_TYPE[i].m_nCount_Max != m_lQuestList_Progress_GOAWAY_TYPE[i].m_nCount_Current)
                    {
                        GUIManager_Total.Instance.UpdateLog("[" + item[1] + "] " + "[" + m_lQuestList_Progress_GOAWAY_TYPE[i].m_eMonsterType + "] "+ m_lQuestList_Progress_GOAWAY_TYPE[i].m_nCount_Current + " / " + m_lQuestList_Progress_GOAWAY_TYPE[i].m_nCount_Max);
                    }
                }
                // 로그GUI에 퀘스트 완료 가능 정보 출력(퀘스트 완료 조건 충족)
                if (m_lQuestList_Progress_GOAWAY_TYPE[i].m_bCondition == true)
                {
                    GUIManager_Total.Instance.UpdateLog("[" + item[1] + "] 완료");
                    GUIManager_Total.Instance.Display_GUI_QuestStateInfo(m_lQuestList_Progress_GOAWAY_TYPE[i]); // 퀘스트 완료 가능 알림
                }
            }
            // 퀘스트와 관련된 NPC(퀘스트 발행 NPC, 퀘스트 완료 NPC)의 퀘스트 아이콘 업데이트
            if (NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_GOAWAY_TYPE[i].m_nNPC] != null)
            {
                // 퀘스트 발행 NPC == 퀘스트 완료 NPC
                if (m_lQuestList_Progress_GOAWAY_TYPE[i].m_nNPC == m_lQuestList_Progress_GOAWAY_TYPE[i].m_nNPC_Clear)
                {
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_GOAWAY_TYPE[i].m_nNPC].UpdateIcon(); // NPC의 퀘스트 아이콘 : 퀘스트 완료 가능
                }
                // 퀘스트 발행 NPC != 퀘스트 완료 NPC
                else
                {
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_GOAWAY_TYPE[i].m_nNPC].UpdateIcon(); // 퀘스트 발행 NPC의 퀘스트 아이콘 : 퀘스트 진행중(해당 NPC를 통해 퀘스트 완료 불가능)
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_GOAWAY_TYPE[i].m_nNPC_Clear].UpdateIcon(); // 퀘스트 완료 NPC의 퀘스트 아이콘 : 퀘스트 완료 가능
                }
            }

            GUIManager_Total.Instance.Update_Quest_Information(m_lQuestList_Progress_GOAWAY_TYPE[i], 1); // 퀘스트GUI 업데이트
        }
    }

    // 퀘스트 타입 : 수집
    // Player_Total.cs의 void GetQuestReward(ㆍㆍㆍ) 함수에서 사용된다. 퀘스트 보상으로 얻는 아이템에 대해 진행중인 수집 타입의 퀘스트 현황을 업데이트한다. 로그GUI에 정보를 출력하지 않는다.
    public void QuestUpdate_Collect_NoDisplay()
    {
        for (int i = 0; i < m_lQuestList_Progress_COLLECT.Count; i++) // 해당하는 퀘스트 타입의 진행중인 모든 퀘스트를 조사
        {
            m_lQuestList_Progress_COLLECT[i].Check_COLLECT(); // 1. 플레이어가 보유한 모든 아이템에대한 퀘스트 관련성 조사(하나라도 퀘스트와 관련된 아이템이 존재하는지 판단)
                                                              // 1. (return true : 퀘스트와 관련된 아이템 존재 / return false : 모든 아이템이 해당 퀘스트와 관련없음)
                                                              // 2. 퀘스트 완료 조건 판단
            // 퀘스트 완료 가능 정보 출력(퀘스트 완료 조건 충족)
            if (m_lQuestList_Progress_COLLECT[i].m_bCondition == true)
            {
                GUIManager_Total.Instance.Display_GUI_QuestStateInfo(m_lQuestList_Progress_COLLECT[i]); // 퀘스트 완료 가능 알림
            }
            // 퀘스트와 관련된 NPC(퀘스트 발행 NPC, 퀘스트 완료 NPC)의 퀘스트 아이콘 업데이트
            if (NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_COLLECT[i].m_nNPC] != null)
            {
                // 퀘스트 발행 NPC == 퀘스트 완료 NPC
                if (m_lQuestList_Progress_COLLECT[i].m_nNPC == m_lQuestList_Progress_COLLECT[i].m_nNPC_Clear)
                {
                    if (NPCManager_Total.m_Dictionary_NPC.ContainsKey(m_lQuestList_Progress_COLLECT[i].m_nNPC) == true)
                    {
                        NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_COLLECT[i].m_nNPC].UpdateIcon(); // NPC의 퀘스트 아이콘 : 퀘스트 완료 가능
                    }
                }
                // 퀘스트 발행 NPC != 퀘스트 완료 NPC
                else
                {
                    if (NPCManager_Total.m_Dictionary_NPC.ContainsKey(m_lQuestList_Progress_COLLECT[i].m_nNPC) == true)
                    {
                        NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_COLLECT[i].m_nNPC].UpdateIcon(); // 퀘스트 발행 NPC의 퀘스트 아이콘 : 퀘스트 진행중(해당 NPC를 통해 퀘스트 완료 불가능)
                        NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_COLLECT[i].m_nNPC_Clear].UpdateIcon(); // 퀘스트 완료 NPC의 퀘스트 아이콘 : 퀘스트 완료 가능
                    }
                }
            }

            GUIManager_Total.Instance.Update_Quest_Information(m_lQuestList_Progress_COLLECT[i], 1); // 퀘스트GUI 업데이트
        }
    }
    // 퀘스트 타입 : 수집
    // Player_Total.cs의 void AddQuest(ㆍㆍㆍ) 함수에서 사용된다. 퀘스트 수락시 플레이어가 이미 가지고 있는 아이템에 대해 진행중인 수집타입의 퀘스트 현황을 업데이트한다. 로그GUI에 정보를 출력한다.
    public void QuestUpdate_Collect()
    {
        for (int i = 0; i < m_lQuestList_Progress_COLLECT.Count; i++) // 해당하는 퀘스트 타입의 진행중인 모든 퀘스트를 조사
        {
            var item = m_lQuestList_Progress_COLLECT[i].m_sQuest_Title.Split('\n'); // 퀘스트의 정보(분류, 제목, 난이도)

            if (m_lQuestList_Progress_COLLECT[i].m_bCondition == false) // 퀘스트가 완료 가능하지 않을때(퀘스트 완료 조건 미충족)
            {
                if (m_lQuestList_Progress_COLLECT[i].Check_COLLECT() == true) // 1. 플레이어가 보유한 모든 아이템에대한 퀘스트 관련성 조사(하나라도 퀘스트와 관련된 아이템이 존재하는지 판단)
                                                                              // 1. (return true : 퀘스트와 관련된 아이템 존재 / return false : 모든 아이템이 해당 퀘스트와 관련없음)
                                                                              // 2. 퀘스트 완료 조건 판단
                {
                    // 로그GUI에 퀘스트 현황 업데이트 정보 출력
                    for (int j = 0; j < m_lQuestList_Progress_COLLECT[i].m_nl_ItemCode.Count; j++)
                    {
                        GUIManager_Total.Instance.UpdateLog("[" + item[1] + "] " + m_lQuestList_Progress_COLLECT[i].m_nl_ItemCount_Current[j] + " / " + m_lQuestList_Progress_COLLECT[i].m_nl_ItemCount_Max[j]);
                    }
                }
            }
            else // 퀘스트가 완료 가능할때(퀘스트 완료 조건 충족)
                 // 수집 타입의 퀘스트의 경우 퀘스트 완료 가능 여부가 유동적이다.
                 // 퀘스트 완료에 필요한 아이템을 획득하여 퀘스트 완료가 가능 하더라도 착용, 사용, 거래, 다른 퀘스트 완료 등의 이유로 퀘스트 완료가 불가능해 질수도 있다.
                 // 따라서 다른 타입의 퀘스트와 달리 퀘스트 완료 조건 판단을 추가했다.
            {
                m_lQuestList_Progress_COLLECT[i].Check_COLLECT(); // 퀘스트 완료 조건 판단
            }
            // 로그GUI에 퀘스트 완료 가능 정보 출력(퀘스트 완료 조건 충족)
            if (m_lQuestList_Progress_COLLECT[i].m_bCondition == true)
            {
                GUIManager_Total.Instance.UpdateLog("[" + item[1] + "] 완료");
                GUIManager_Total.Instance.Display_GUI_QuestStateInfo(m_lQuestList_Progress_COLLECT[i]); // 퀘스트 완료 가능 알림
            }
            // 퀘스트와 관련된 NPC(퀘스트 발행 NPC, 퀘스트 완료 NPC)의 퀘스트 아이콘 업데이트
            if (NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_COLLECT[i].m_nNPC] != null)
            {
                // 퀘스트 발행 NPC == 퀘스트 완료 NPC
                if (m_lQuestList_Progress_COLLECT[i].m_nNPC == m_lQuestList_Progress_COLLECT[i].m_nNPC_Clear)
                {
                    if (NPCManager_Total.m_Dictionary_NPC.ContainsKey(m_lQuestList_Progress_COLLECT[i].m_nNPC) == true)
                    {
                        NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_COLLECT[i].m_nNPC].UpdateIcon(); // NPC의 퀘스트 아이콘 : 퀘스트 완료 가능
                    }
                }
                // 퀘스트 발행 NPC != 퀘스트 완료 NPC
                else
                {
                    if (NPCManager_Total.m_Dictionary_NPC.ContainsKey(m_lQuestList_Progress_COLLECT[i].m_nNPC) == true)
                    {
                        NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_COLLECT[i].m_nNPC].UpdateIcon(); // 퀘스트 발행 NPC의 퀘스트 아이콘 : 퀘스트 진행중(해당 NPC를 통해 퀘스트 완료 불가능)
                        NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_COLLECT[i].m_nNPC_Clear].UpdateIcon(); // 퀘스트 완료 NPC의 퀘스트 아이콘 : 퀘스트 완료 가능
                    }
                }
            }

            GUIManager_Total.Instance.Update_Quest_Information(m_lQuestList_Progress_COLLECT[i], 1); // 퀘스트GUI 업데이트
        }
    }
    // 퀘스트 타입 : 수집
    // 특정 목적을 가진 위의 void QuestUpdate_Collect_NoDisplay(), void QuestUpdate_Collect() 함수와는 달리 수집 타입의 퀘스트 현황 업데이트에 관련된 모든 함수에서 실행된다. 로그GUI에 정보를 출력한다.
    public void QuestUpdate_Collect(Item item) // item : 획득하거나 잃어버린 아이템
    {
        for (int i = 0; i < m_lQuestList_Progress_COLLECT.Count; i++) // 해당하는 퀘스트 타입의 진행중인 모든 퀘스트를 조사
        {
            var items = m_lQuestList_Progress_COLLECT[i].m_sQuest_Title.Split('\n'); // 퀘스트의 정보(분류, 제목, 난이도)
            if (m_lQuestList_Progress_COLLECT[i].m_bCondition == false) // 퀘스트가 완료 가능하지 않을때(퀘스트 완료 조건 미충족)
            {
                m_lQuestList_Progress_COLLECT[i].Check_COLLECT(); // 퀘스트 완료 조건 판단
                {
                    // 로그GUI에 퀘스트 현황 업데이트 정보 출력
                    for (int j = 0; j < m_lQuestList_Progress_COLLECT[i].m_nl_ItemCode.Count; j++)
                        if (m_lQuestList_Progress_COLLECT[i].m_nl_ItemCode[j] == item.m_nItemCode) // 해당하는 아이템의 퀘스트 완료 조건 현황만 출력한다. 다른 퀘스트 완료 조건의 아이템 현황은 출력하지 않는다.
                        {
                            GUIManager_Total.Instance.UpdateLog("[" + items[1] + "][" + ItemManager.instance.Get_Item_Information(item.m_nItemCode).m_sItemName + "] " + m_lQuestList_Progress_COLLECT[i].m_nl_ItemCount_Current[j] + " / " + m_lQuestList_Progress_COLLECT[i].m_nl_ItemCount_Max[j]);
                        }    
                }
                if (m_lQuestList_Progress_COLLECT[i].m_bCondition == true)
                {
                    GUIManager_Total.Instance.UpdateLog("[" + items[1] + "] 완료");
                    GUIManager_Total.Instance.Display_GUI_QuestStateInfo(m_lQuestList_Progress_COLLECT[i]); // 퀘스트 완료 가능 알림
                }
            }
            else // 퀘스트가 완료 가능할때(퀘스트 완료 조건 충족)
                 // 수집 타입의 퀘스트의 경우 퀘스트 완료 가능 여부가 유동적이다.
                 // 퀘스트 완료에 필요한 아이템을 획득하여 퀘스트 완료가 가능 하더라도 착용, 사용, 거래, 다른 퀘스트 완료 등의 이유로 퀘스트 완료가 불가능해 질수도 있다.
                 // 따라서 다른 타입의 퀘스트와 달리 퀘스트 완료 조건 판단을 추가했다.
            {
                m_lQuestList_Progress_COLLECT[i].Check_COLLECT(); // 퀘스트 완료 조건 판단
                {
                    // 로그GUI에 퀘스트 현황 업데이트 정보 출력
                    for (int j = 0; j < m_lQuestList_Progress_COLLECT[i].m_nl_ItemCode.Count; j++)
                        if (m_lQuestList_Progress_COLLECT[i].m_nl_ItemCode[j] == item.m_nItemCode) // 해당하는 아이템의 퀘스트 완료 조건 현황만 출력한다. 다른 퀘스트 완료 조건의 아이템 현황은 출력하지 않는다.
                        {
                            if (m_lQuestList_Progress_COLLECT[i].m_nl_ItemCount_Max[j] >= m_lQuestList_Progress_COLLECT[i].m_nl_ItemCount_Current[j]) // 퀘스트 완료 조건에 변동이 생겼을때만 아이템 현황을 출력한다.
                            {
                                GUIManager_Total.Instance.UpdateLog("[" + items[1] + "][" + ItemManager.instance.Get_Item_Information(item.m_nItemCode).m_sItemName + "] " + m_lQuestList_Progress_COLLECT[i].m_nl_ItemCount_Current[j] + " / " + m_lQuestList_Progress_COLLECT[i].m_nl_ItemCount_Max[j]);
                                // 별도의 퀘스트 완료 가능 알림은 없다.
                            }
                        }
                }
            }
            // 퀘스트와 관련된 NPC(퀘스트 발행 NPC, 퀘스트 완료 NPC)의 퀘스트 아이콘 업데이트
            if (NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_COLLECT[i].m_nNPC] != null)
            {
                // 퀘스트 발행 NPC == 퀘스트 완료 NPC
                if (m_lQuestList_Progress_COLLECT[i].m_nNPC == m_lQuestList_Progress_COLLECT[i].m_nNPC_Clear)
                {
                    if (NPCManager_Total.m_Dictionary_NPC.ContainsKey(m_lQuestList_Progress_COLLECT[i].m_nNPC) == true)
                    {
                        NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_COLLECT[i].m_nNPC].UpdateIcon(); // NPC의 퀘스트 아이콘 : 퀘스트 완료 가능
                    }
                }
                // 퀘스트 발행 NPC != 퀘스트 완료 NPC
                else
                {
                    if (NPCManager_Total.m_Dictionary_NPC.ContainsKey(m_lQuestList_Progress_COLLECT[i].m_nNPC) == true)
                    {
                        NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_COLLECT[i].m_nNPC].UpdateIcon(); // 퀘스트 발행 NPC의 퀘스트 아이콘 : 퀘스트 진행중(해당 NPC를 통해 퀘스트 완료 불가능)
                        NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_COLLECT[i].m_nNPC_Clear].UpdateIcon(); // 퀘스트 완료 NPC의 퀘스트 아이콘 : 퀘스트 완료 가능
                    }
                }
            }

            GUIManager_Total.Instance.Update_Quest_Information(m_lQuestList_Progress_COLLECT[i], 1); // 퀘스트GUI 업데이트
        }
    }

    //
    // ※ 수집 타입의 퀘스트 업데이트 함수는 현재 플레이어가 가지고 있는 모든 아이템을 조사하는 방식으로 실행된다. 비효율적이다. 따라서 초기 게임 로딩, 퀘스트 수락ㆍ완료 등의 경우를 제외하고는 더 효율적인 업데이트 방법을 추구해야 한다.
    //    1. 획득하거나 잃어버리는 아이템 정보와 아이템 개수를 가지고온다.
    //    2. 해당 아이템 수집을 퀘스트 완료 조건으로 가지는 모든 퀘스트의 현황을 업데이트한다.
    //    3. 이때 플레이어가 가지고 있는 모든 아이템을 조사하는게 아니라 해당하는 아이템 조건에 아이템 개수를 가감 해준다면 확실히 메모리 관리에 도움이 될것이다.
    //

    // 퀘스트 타입 : 구르기
    public void QuestUpdate_Roll()
    {
        for (int i = 0; i < m_lQuestList_Progress_ROLL.Count; i++) // 해당하는 퀘스트 타입의 진행중인 모든 퀘스트를 조사
        {
            var item = m_lQuestList_Progress_ROLL[i].m_sQuest_Title.Split('\n'); // 퀘스트의 정보(분류, 제목, 난이도)
            if (m_lQuestList_Progress_ROLL[i].m_bCondition == false) // 퀘스트가 완료 가능하지 않을때(퀘스트 완료 조건 미충족)
            {
                if (m_lQuestList_Progress_ROLL[i].Check_ROLL() == true)  // 퀘스트 완료 조건 판단
                {
                    // 로그GUI에 퀘스트 현황 업데이트 정보 출력
                    GUIManager_Total.Instance.UpdateLog("[" + item[1] + "] " + m_lQuestList_Progress_ROLL[i].m_nCount_Current + " / " + m_lQuestList_Progress_ROLL[i].m_nCount_Max);
                }
                // 로그GUI에 퀘스트 완료 가능 정보 출력(퀘스트 완료 조건 충족)
                if (m_lQuestList_Progress_ROLL[i].m_bCondition == true) // 퀘스트가 완료 가능할때
                {
                    GUIManager_Total.Instance.UpdateLog("[" + item[1] + "] 완료");
                    GUIManager_Total.Instance.Display_GUI_QuestStateInfo(m_lQuestList_Progress_ROLL[i]); // 퀘스트 완료 가능 알림
                }
            }
            // 퀘스트와 관련된 NPC(퀘스트 발행 NPC, 퀘스트 완료 NPC)의 퀘스트 아이콘 업데이트
            if (NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_ROLL[i].m_nNPC] != null)
            {
                // 퀘스트 발행 NPC == 퀘스트 완료 NPC
                if (m_lQuestList_Progress_ROLL[i].m_nNPC == m_lQuestList_Progress_ROLL[i].m_nNPC_Clear)
                {
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_ROLL[i].m_nNPC].UpdateIcon(); // NPC의 퀘스트 아이콘 : 퀘스트 완료 가능
                }
                // 퀘스트 발행 NPC != 퀘스트 완료 NPC
                else
                {
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_ROLL[i].m_nNPC].UpdateIcon(); // 퀘스트 발행 NPC의 퀘스트 아이콘 : 퀘스트 진행중(해당 NPC를 통해 퀘스트 완료 불가능)
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_ROLL[i].m_nNPC_Clear].UpdateIcon(); // 퀘스트 완료 NPC의 퀘스트 아이콘 : 퀘스트 완료 가능
                }
            }

            GUIManager_Total.Instance.Update_Quest_Information(m_lQuestList_Progress_ROLL[i], 1); // 퀘스트GUI 업데이트
        }
    }

    // 퀘스트 타입 : 특정 몬스터 제거, 특정 타입의 몬스터 제거
    // 제거 = 토벌 + 놓아주기
    public void QuestUpdate_Eliminate(E_MONSTER_KIND mk, int code) // mk : 몬스터 타입, code : 몬스터 코드
    {
        // 특정 몬스터 제거 퀘스트 현황 업데이트
        for (int i = 0; i < m_lQuestList_Progress_ELIMINATE_MONSTER.Count; i++) // 해당하는 퀘스트 타입의 진행중인 모든 퀘스트를 조사
        {
            var item = m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_sQuest_Title.Split('\n'); // 퀘스트의 정보(분류, 제목, 난이도)
            if (m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_bCondition == false) // 퀘스트가 완료 가능하지 않을때(퀘스트 완료 조건 미충족)
            {
                if (m_lQuestList_Progress_ELIMINATE_MONSTER[i].Check_ELIMINATE_MONSTER(code) == true) // 1. 몬스터 코드(code)가 퀘스트와 관련 있는지 판단(return true : 관련있음 / return false : 관련없음)
                                                                                                      // 2. 퀘스트 완료 조건 판단
                {
                    // 로그GUI에 퀘스트 현황 업데이트 정보 출력
                    for (int j = 0; j < m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_nl_Count_Current.Count; j++) // 모든 퀘스트 완료 조건 조사
                    {
                        if (m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_nl_MonsterCode[j] == code) // 해당하는 퀘스트 완료 조건 현황만 출력한다. 다른 퀘스트 완료 조건의 현황은 출력하지 않는다.
                            if (m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_nl_Count_Max[j] >= m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_nl_Count_Current[j]) // 불필요한 정보 출력은 하지 않는다.
                            {
                                GUIManager_Total.Instance.UpdateLog("[" + item[1] + "][" + MonsterManager.m_Dictionary_Monster[code].m_sMonster_Name + "] " + m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_nl_Count_Current[j] + " / " + m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_nl_Count_Max[j]);
                            }
                    }
                }
                // 로그GUI에 퀘스트 완료 가능 정보 출력(퀘스트 완료 조건 충족)
                if (m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_bCondition == true)
                {
                    GUIManager_Total.Instance.UpdateLog("[" + item[1] + "] 완료");
                    GUIManager_Total.Instance.Display_GUI_QuestStateInfo(m_lQuestList_Progress_ELIMINATE_MONSTER[i]); // 퀘스트 완료 가능 알림
                }
            }
            // 퀘스트와 관련된 NPC(퀘스트 발행 NPC, 퀘스트 완료 NPC)의 퀘스트 아이콘 업데이트
            if (NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_nNPC] != null)
            {
                if (m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_nNPC == m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_nNPC_Clear)
                {
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_nNPC].UpdateIcon(); // NPC의 퀘스트 아이콘 : 퀘스트 완료 가능
                }
                else
                {
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_nNPC].UpdateIcon(); // 퀘스트 발행 NPC의 퀘스트 아이콘 : 퀘스트 진행중(해당 NPC를 통해 퀘스트 완료 불가능)
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_nNPC_Clear].UpdateIcon(); // 퀘스트 완료 NPC의 퀘스트 아이콘 : 퀘스트 완료 가능
                }
            }

            GUIManager_Total.Instance.Update_Quest_Information(m_lQuestList_Progress_ELIMINATE_MONSTER[i], 1); // 퀘스트GUI 업데이트
        }
        // 특정 타입의 몬스터 제거 퀘스트 현황 업데이트
        for (int i = 0; i < m_lQuestList_Progress_ELIMINATE_TYPE.Count; i++) // 해당하는 타입의 진행중인 모든 퀘스트를 조사
        {
            var item = m_lQuestList_Progress_ELIMINATE_TYPE[i].m_sQuest_Title.Split('\n'); // 퀘스트의 정보(분류, 제목, 난이도)
            if (m_lQuestList_Progress_ELIMINATE_TYPE[i].m_bCondition == false) // 퀘스트가 완료 가능하지 않을때(퀘스트 완료 조건 미충족)
            {
                if (m_lQuestList_Progress_ELIMINATE_TYPE[i].Check_ELIMINATE_TYPE(mk) == true) // 1. 몬스터 코드(code)가 퀘스트와 관련 있는지 판단(return true : 관련있음 / return false : 관련없음)
                                                                                              // 2. 퀘스트 완료 조건 판단
                {
                    // 로그GUI에 퀘스트 현황 업데이트 정보 출력
                    // 퀘스트 완료 조건이 한가지만 존재
                    if (m_lQuestList_Progress_ELIMINATE_TYPE[i].m_nCount_Max != m_lQuestList_Progress_ELIMINATE_TYPE[i].m_nCount_Current)
                    {
                        GUIManager_Total.Instance.UpdateLog("[" + item[1] + "] " + "[" + m_lQuestList_Progress_ELIMINATE_TYPE[i].m_eMonsterType + "] " + m_lQuestList_Progress_ELIMINATE_TYPE[i].m_nCount_Current + " / " + m_lQuestList_Progress_ELIMINATE_TYPE[i].m_nCount_Max);
                    }
                }
                // 로그GUI에 퀘스트 완료 가능 정보 출력(퀘스트 완료 조건 충족)
                if (m_lQuestList_Progress_ELIMINATE_TYPE[i].m_bCondition == true)
                {
                    GUIManager_Total.Instance.UpdateLog("[" + item[1] + "] 완료");
                    GUIManager_Total.Instance.Display_GUI_QuestStateInfo(m_lQuestList_Progress_ELIMINATE_TYPE[i]); // 퀘스트 완료 가능 알림
                }
            }
            // 퀘스트와 관련된 NPC(퀘스트 발행 NPC, 퀘스트 완료 NPC)의 퀘스트 아이콘 업데이트
            if (NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_ELIMINATE_TYPE[i].m_nNPC] != null)
            {
                // 퀘스트 발행 NPC == 퀘스트 완료 NPC
                if (m_lQuestList_Progress_ELIMINATE_TYPE[i].m_nNPC == m_lQuestList_Progress_ELIMINATE_TYPE[i].m_nNPC_Clear)
                {
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_ELIMINATE_TYPE[i].m_nNPC].UpdateIcon(); // NPC의 퀘스트 아이콘 : 퀘스트 완료 가능
                }
                // 퀘스트 발행 NPC != 퀘스트 완료 NPC
                else
                {
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_ELIMINATE_TYPE[i].m_nNPC].UpdateIcon(); // 퀘스트 발행 NPC의 퀘스트 아이콘 : 퀘스트 진행중(해당 NPC를 통해 퀘스트 완료 불가능)
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_ELIMINATE_TYPE[i].m_nNPC_Clear].UpdateIcon(); // 퀘스트 완료 NPC의 퀘스트 아이콘 : 퀘스트 완료 가능
                }
            }

            GUIManager_Total.Instance.Update_Quest_Information(m_lQuestList_Progress_ELIMINATE_TYPE[i], 1); // 퀘스트GUI 업데이트
        }
    }

    // 퀘스트를 추가하는 함수(퀘스트 최초 수락 시 사용). 진행중인 퀘스트 목록에 추가. 함수 오버로딩을 이용
    // 퀘스트 타입 : 특정 몬스터 토벌
    public void AddQuest(Quest_KILL_MONSTER quest) // quest : 추가할 퀘스트 정보
    {
        quest.m_nQuestOrder = QuestManager.m_snQuest_ProcessOrder++; // 퀘스트 추가 순서
        m_lQuestList_Progress_KILL_MONSTER.Add(quest);
        GUIManager_Total.Instance.Update_Quest(quest, 1); // 퀘스트GUI 업데이트
        quest.m_bProcess = true;
        quest.m_bClear = false;
    }
    // 퀘스트 타입 : 특정 타입의 몬스터 토벌
    public void AddQuest(Quest_KILL_TYPE quest) // quest : 추가할 퀘스트 정보
    {
        quest.m_nQuestOrder = QuestManager.m_snQuest_ProcessOrder++; // 퀘스트 추가 순서
        m_lQuestList_Progress_KILL_TYPE.Add(quest);
        GUIManager_Total.Instance.Update_Quest(quest, 1); // 퀘스트GUI 업데이트
        quest.m_bProcess = true;
        quest.m_bClear = false;
    }
    // 퀘스트 타입 : 특정 몬스터 놓아주기
    public void AddQuest(Quest_GOAWAY_MONSTER quest) // quest : 추가할 퀘스트 정보
    {
        quest.m_nQuestOrder = QuestManager.m_snQuest_ProcessOrder++; // 퀘스트 추가 순서
        m_lQuestList_Progress_GOAWAY_MONSTER.Add(quest);
        GUIManager_Total.Instance.Update_Quest(quest, 1); // 퀘스트GUI 업데이트
        quest.m_bProcess = true;
        quest.m_bClear = false;
    }
    // 퀘스트 타입 : 특정 타입의 몬스터 놓아주기
    public void AddQuest(Quest_GOAWAY_TYPE quest) // quest : 추가할 퀘스트 정보
    {
        quest.m_nQuestOrder = QuestManager.m_snQuest_ProcessOrder++; // 퀘스트 추가 순서
        m_lQuestList_Progress_GOAWAY_TYPE.Add(quest);
        GUIManager_Total.Instance.Update_Quest(quest, 1); // 퀘스트GUI 업데이트
        quest.m_bProcess = true;
        quest.m_bClear = false;
    }
    // 퀘스트 타입 : 수집
    public void AddQuest(Quest_COLLECT quest) // quest : 추가할 퀘스트 정보
    {
        quest.m_nQuestOrder = QuestManager.m_snQuest_ProcessOrder++; // 퀘스트 추가 순서
        m_lQuestList_Progress_COLLECT.Add(quest);
        GUIManager_Total.Instance.Update_Quest(quest, 1); // 퀘스트GUI 업데이트
        quest.m_bProcess = true;
        quest.m_bClear = false;
    }
    // 퀘스트 타입 : 대화
    public void AddQuest(Quest_CONVERSATION quest) // quest : 추가할 퀘스트 정보
    {
        quest.m_nQuestOrder = QuestManager.m_snQuest_ProcessOrder++; // 퀘스트 추가 순서
        m_lQuestList_Progress_CONVERSATION.Add(quest);
        GUIManager_Total.Instance.Update_Quest(quest, 1); // 퀘스트GUI 업데이트
        quest.m_bProcess = true;
        quest.m_bClear = false;
    }
    // 퀘스트 타입 : 구르기
    public void AddQuest(Quest_ROLL quest) // quest : 추가할 퀘스트 정보
    {
        quest.m_nQuestOrder = QuestManager.m_snQuest_ProcessOrder++; // 퀘스트 추가 순서
        m_lQuestList_Progress_ROLL.Add(quest);
        GUIManager_Total.Instance.Update_Quest(quest, 1); // 퀘스트GUI 업데이트
        quest.m_bProcess = true;
        quest.m_bClear = false;
    }
    // 퀘스트 타입 : 특정 몬스터 제거
    public void AddQuest(Quest_ELIMINATE_MONSTER quest) // quest : 추가할 퀘스트 정보
    {
        quest.m_nQuestOrder = QuestManager.m_snQuest_ProcessOrder++; // 퀘스트 추가 순서
        m_lQuestList_Progress_ELIMINATE_MONSTER.Add(quest);
        GUIManager_Total.Instance.Update_Quest(quest, 1); // 퀘스트GUI 업데이트
        quest.m_bProcess = true;
        quest.m_bClear = false;
    }
    // 퀘스트 타입 : 특정 타입의 몬스터 제거
    public void AddQuest(Quest_ELIMINATE_TYPE quest) // quest : 추가할 퀘스트 정보
    {
        quest.m_nQuestOrder = QuestManager.m_snQuest_ProcessOrder++; // 퀘스트 추가 순서
        m_lQuestList_Progress_ELIMINATE_TYPE.Add(quest);
        GUIManager_Total.Instance.Update_Quest(quest, 1); // 퀘스트GUI 업데이트
        quest.m_bProcess = true;
        quest.m_bClear = false;
    }

    // 진행중인 퀘스트를 완료하는 함수. 함수 오버로딩을 이용
    // 퀘스트 타입 : 특정 몬스터 토벌
    public void GetQuestReward(Quest_KILL_MONSTER quest) // quest : 완료할 퀘스트 정보
    {
        for (int i = 0; i < m_lQuestList_Progress_KILL_MONSTER.Count; i++) // 완료할 퀘스트를 찾는 과정
        {
            if (m_lQuestList_Progress_KILL_MONSTER[i].m_nQuest_Code == quest.m_nQuest_Code)
            {
                quest.m_nQuestOrder = QuestManager.m_snQuest_CompleteOrder++; // 퀘스트 완료 순서
                m_lQuestList_Complete_KILL_MONSTER.Add(quest);
                m_lQuestList_Progress_KILL_MONSTER.RemoveAt(i);
                GUIManager_Total.Instance.UpdateLog(quest.m_sQuest_Title + "완료");
                GUIManager_Total.Instance.Update_Quest(quest, 2); // 퀘스트GUI 업데이트
                GUIManager_Total.Instance.UnDisplay_GUI_QuestStateInfo(quest); // 퀘스트 완료 가능 알림 삭제
                break;
            }
        }
    }
    // 퀘스트 타입 : 특정 타입의 몬스터 토벌
    public void GetQuestReward(Quest_KILL_TYPE quest) // quest : 완료할 퀘스트 정보
    {
        for (int i = 0; i < m_lQuestList_Progress_KILL_TYPE.Count; i++) // 완료할 퀘스트를 찾는 과정
        {
            if (m_lQuestList_Progress_KILL_TYPE[i].m_nQuest_Code == quest.m_nQuest_Code)
            {
                quest.m_nQuestOrder = QuestManager.m_snQuest_CompleteOrder++; // 퀘스트 완료 순서
                m_lQuestList_Complete_KILL_TYPE.Add(quest);
                m_lQuestList_Progress_KILL_TYPE.RemoveAt(i);
                GUIManager_Total.Instance.UpdateLog(quest.m_sQuest_Title + "완료");
                GUIManager_Total.Instance.Update_Quest(quest, 2); // 퀘스트GUI 업데이트
                GUIManager_Total.Instance.UnDisplay_GUI_QuestStateInfo(quest); // 퀘스트 완료 가능 알림 삭제
                break;
            }
        }
    }
    // 퀘스트 타입 : 특정 몬스터 놓아주기
    public void GetQuestReward(Quest_GOAWAY_MONSTER quest) // quest : 완료할 퀘스트 정보
    {
        for (int i = 0; i < m_lQuestList_Progress_GOAWAY_MONSTER.Count; i++) // 완료할 퀘스트를 찾는 과정
        {
            if (m_lQuestList_Progress_GOAWAY_MONSTER[i].m_nQuest_Code == quest.m_nQuest_Code)
            {
                quest.m_nQuestOrder = QuestManager.m_snQuest_CompleteOrder++; // 퀘스트 완료 순서
                m_lQuestList_Complete_GOAWAY_MONSTER.Add(quest);
                m_lQuestList_Progress_GOAWAY_MONSTER.RemoveAt(i);
                GUIManager_Total.Instance.UpdateLog(quest.m_sQuest_Title + "완료");
                GUIManager_Total.Instance.Update_Quest(quest, 2); // 퀘스트GUI 업데이트
                GUIManager_Total.Instance.UnDisplay_GUI_QuestStateInfo(quest); // 퀘스트 완료 가능 알림 삭제
                break;
            }
        }
    }
    // 퀘스트 타입 : 특정 타입의 몬스터 놓아주기
    public void GetQuestReward(Quest_GOAWAY_TYPE quest) // quest : 완료할 퀘스트 정보
    {
        for (int i = 0; i < m_lQuestList_Progress_GOAWAY_TYPE.Count; i++) // 완료할 퀘스트를 찾는 과정
        {
            if (m_lQuestList_Progress_GOAWAY_TYPE[i].m_nQuest_Code == quest.m_nQuest_Code)
            {
                quest.m_nQuestOrder = QuestManager.m_snQuest_CompleteOrder++; // 퀘스트 완료 순서
                m_lQuestList_Complete_GOAWAY_TYPE.Add(quest);
                m_lQuestList_Progress_GOAWAY_TYPE.RemoveAt(i);
                GUIManager_Total.Instance.UpdateLog(quest.m_sQuest_Title + "완료");
                GUIManager_Total.Instance.Update_Quest(quest, 2); // 퀘스트GUI 업데이트
                GUIManager_Total.Instance.UnDisplay_GUI_QuestStateInfo(quest); // 퀘스트 완료 가능 알림 삭제
                break;
            }
        }
    }
    // 퀘스트 타입 : 수집
    public void GetQuestReward(Quest_COLLECT quest) // quest : 완료할 퀘스트 정보
    {
        for (int i = 0; i < m_lQuestList_Progress_COLLECT.Count; i++) // 완료할 퀘스트를 찾는 과정
        {
            if (m_lQuestList_Progress_COLLECT[i].m_nQuest_Code == quest.m_nQuest_Code)
            {
                for (int j = 0; j < m_lQuestList_Progress_COLLECT[i].m_nl_ItemCode.Count; j++)
                {
                    m_lQuestList_Progress_COLLECT[i].m_nl_ItemCount_Current[j] = m_lQuestList_Progress_COLLECT[i].m_nl_ItemCount_Max[j];
                }
                quest.m_nQuestOrder = QuestManager.m_snQuest_CompleteOrder++; // 퀘스트 완료 순서
                m_lQuestList_Complete_COLLECT.Add(quest);
                m_lQuestList_Progress_COLLECT.RemoveAt(i);
                GUIManager_Total.Instance.UpdateLog(quest.m_sQuest_Title + "완료");
                GUIManager_Total.Instance.Update_Quest(quest, 2); // 퀘스트GUI 업데이트
                GUIManager_Total.Instance.UnDisplay_GUI_QuestStateInfo(quest); // 퀘스트 완료 가능 알림 삭제
                break;
            }
        }
    }
    // 퀘스트 타입 : 대화
    public void GetQuestReward(Quest_CONVERSATION quest) // quest : 완료할 퀘스트 정보
    {
        for (int i = 0; i < m_lQuestList_Progress_CONVERSATION.Count; i++) // 완료할 퀘스트를 찾는 과정
        {
            if (m_lQuestList_Progress_CONVERSATION[i].m_nQuest_Code == quest.m_nQuest_Code)
            {
                quest.m_nQuestOrder = QuestManager.m_snQuest_CompleteOrder++; // 퀘스트 완료 순서
                m_lQuestList_Complete_CONVERSATION.Add(quest);
                m_lQuestList_Progress_CONVERSATION.RemoveAt(i);
                GUIManager_Total.Instance.UpdateLog(quest.m_sQuest_Title + "완료");
                GUIManager_Total.Instance.Update_Quest(quest, 2); // 퀘스트GUI 업데이트
                GUIManager_Total.Instance.UnDisplay_GUI_QuestStateInfo(quest); // 퀘스트 완료 가능 알림 삭제
                break;
            }
        }
    }
    // 퀘스트 타입 : 구르기
    public void GetQuestReward(Quest_ROLL quest) // quest : 완료할 퀘스트 정보
    {
        for (int i = 0; i < m_lQuestList_Progress_ROLL.Count; i++) // 완료할 퀘스트를 찾는 과정
        {
            if (m_lQuestList_Progress_ROLL[i].m_nQuest_Code == quest.m_nQuest_Code)
            {
                quest.m_nQuestOrder = QuestManager.m_snQuest_CompleteOrder++; // 퀘스트 완료 순서
                m_lQuestList_Complete_ROLL.Add(quest);
                m_lQuestList_Progress_ROLL.RemoveAt(i);
                GUIManager_Total.Instance.UpdateLog(quest.m_sQuest_Title + "완료");
                GUIManager_Total.Instance.Update_Quest(quest, 2); // 퀘스트GUI 업데이트
                GUIManager_Total.Instance.UnDisplay_GUI_QuestStateInfo(quest); // 퀘스트 완료 가능 알림 삭제
                break;
            }
        }
    }
    // 퀘스트 타입 : 특정 몬스터 제거
    public void GetQuestReward(Quest_ELIMINATE_MONSTER quest) // quest : 완료할 퀘스트 정보
    {
        for (int i = 0; i < m_lQuestList_Progress_ELIMINATE_MONSTER.Count; i++) // 완료할 퀘스트를 찾는 과정
        {
            if (m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_nQuest_Code == quest.m_nQuest_Code)
            {
                quest.m_nQuestOrder = QuestManager.m_snQuest_CompleteOrder++; // 퀘스트 완료 순서
                m_lQuestList_Complete_ELIMINATE_MONSTER.Add(quest);
                m_lQuestList_Progress_ELIMINATE_MONSTER.RemoveAt(i);
                GUIManager_Total.Instance.UpdateLog(quest.m_sQuest_Title + "완료");
                GUIManager_Total.Instance.Update_Quest(quest, 2); // 퀘스트GUI 업데이트
                GUIManager_Total.Instance.UnDisplay_GUI_QuestStateInfo(quest); // 퀘스트 완료 가능 알림 삭제
                break;
            }
        }
    }
    // 퀘스트 타입 : 특정 타입의 몬스터 제거
    public void GetQuestReward(Quest_ELIMINATE_TYPE quest) // quest : 완료할 퀘스트 정보
    {
        for (int i = 0; i < m_lQuestList_Progress_ELIMINATE_TYPE.Count; i++) // 완료할 퀘스트를 찾는 과정
        {
            if (m_lQuestList_Progress_ELIMINATE_TYPE[i].m_nQuest_Code == quest.m_nQuest_Code)
            {
                quest.m_nQuestOrder = QuestManager.m_snQuest_CompleteOrder++; // 퀘스트 완료 순서
                m_lQuestList_Complete_ELIMINATE_TYPE.Add(quest);
                m_lQuestList_Progress_ELIMINATE_TYPE.RemoveAt(i);
                GUIManager_Total.Instance.UpdateLog(quest.m_sQuest_Title + "완료");
                GUIManager_Total.Instance.Update_Quest(quest, 2); // 퀘스트GUI 업데이트
                GUIManager_Total.Instance.UnDisplay_GUI_QuestStateInfo(quest); // 퀘스트 완료 가능 알림 삭제
                break;
            }
        }
    }
}
