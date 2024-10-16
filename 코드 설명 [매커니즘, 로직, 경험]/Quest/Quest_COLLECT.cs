using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// ※ 수집 퀘스트 클래스
//    해당 퀘스트는 특정 아이템 수집 행위와 연관되어 있다.
//

public class Quest_COLLECT : Quest // 기반이 되는 Quest 클래스 상속
{
    // 특정 아이템 관련 변수
    public List<int> m_nl_ItemCode;          // 특정 아이템 고유코드 리스트
    public List<int> m_nl_ItemCount_Max;     // 특정 아이템 수집 필요 수량 리스트
    public List<int> m_nl_ItemCount_Current; // 특정 아이템 수집 현재 수량 리스트

    //
    // ※ m_nl_ItemCode(특정 아이템 고유코드 리스트), m_nl_ItemCount_Max(특정 아이템 수집 필요 수량 리스트), m_nl_ItemCount_Current(특정 아이템 수집 현재 수량 리스트)는 리스트 순번에 따라 짝을 이룬다.
    //    m_nl_ItemCode[n]에 해당하는 고유코드를 가진 아이템을 m_nl_ItemCount_Max[n]만큼 수집해야 한다. 이때 m_nl_ItemCount_Current[n]을 통해 수집 현황을 확인할 수 있다.
    //

    // 수집 퀘스트를 생성하는 생성자
    public Quest_COLLECT(string questtitle, int questcode, E_QUEST_LEVEL ql, int givenpccode,int clearnpccode, int questloadmapcode)
    {
        m_sQuest_Title = questtitle;
        m_nQuest_Code = questcode;
        m_nQuest_Loadmap_Code = questloadmapcode;
        m_eQuestLevel = ql;
        m_eQuestType = E_QUEST_TYPE.COLLECT;
        m_nNPC = givenpccode;
        m_nNPC_Clear = clearnpccode;

        InitialSet();

        m_nl_ItemCode = new List<int>();
        m_nl_ItemCount_Max = new List<int>();
        m_nl_ItemCount_Current = new List<int>();
    }
    
    // 수집 퀘스트 설정(추가) 함수
    public void AddCondition(int itemcode, int max, int c = 0) // itencode : 특정 아이템 고유코드, max : 특정 아이템 수집 필요 수량, c : 특정 아이템 수집 현재 수량
    {
        m_nl_ItemCode.Add(itemcode);
        m_nl_ItemCount_Max.Add(max);
        m_nl_ItemCount_Current.Add(c);
    }

    // 수집 퀘스트 갱신 함수
    // return true : 플레이어가 보유한 아이템이 해당 수집 퀘스트와 관련 있음 / return false : 플레이어가 보유한 아이템이 해당 수집 퀘스트와 관련 없음
    public bool Check_COLLECT()
    {
        bool returntype = false;
        for (int i = 0; i < m_nl_ItemCode.Count; i++) // 특정 아이템 수량만큼 반복
        {
            m_nl_ItemCount_Current[i] = 0;
            for (int a = 0; a < 60; a++) // 인벤토리 크기만큼 반복
            {
                if (Player_Itemslot.m_nary_Itemslot_Equip_Count[a] != 0) // 인벤토리(장비아이템)에 장비아이템이 존재하는 경우
                {
                    if (Player_Itemslot.m_gary_Itemslot_Equip[a].m_nItemCode == m_nl_ItemCode[i]) // 보유한 장비아이템 고유코드 == 특정 아이템 고유코드
                    {
                        m_nl_ItemCount_Current[i] += Player_Itemslot.m_nary_Itemslot_Equip_Count[a]; // 특정 아이템 수집 현재 수량 += 보유한 장비아이템 수량(1)
                        returntype = true;
                    }
                }
                if (Player_Itemslot.m_nary_Itemslot_Use_Count[a] != 0) // 인벤토리(소비아이템)에 소비아이템이 존재하는 경우
                {
                    if (Player_Itemslot.m_gary_Itemslot_Use[a].m_nItemCode == m_nl_ItemCode[i]) // 보유한 소비아이템 고유코드 == 특정 아이템 고유코드
                    {
                        m_nl_ItemCount_Current[i] += Player_Itemslot.m_nary_Itemslot_Use_Count[a]; // 특정 아이템 수집 현재 수량 += 보유한 소비아이템 수량
                        returntype = true;
                    }
                }
                if (Player_Itemslot.m_nary_Itemslot_Etc_Count[a] != 0) // 인벤토리(기타아이템)에 기타아이템이 존재하는 경우
                {
                    if (Player_Itemslot.m_gary_Itemslot_Etc[a].m_nItemCode == m_nl_ItemCode[i]) // 보유한 기타아이템 고유코드 == 특정 아이템 고유코드
                    {
                        m_nl_ItemCount_Current[i] += Player_Itemslot.m_nary_Itemslot_Etc_Count[a]; // 특정 아이템 수집 현재 수량 += 보유한 기타아이템 수량
                        returntype = true;
                    }
                }
            }
        }
        
        Check_Condition(); // 퀘스트 완료 조건 달성 여부(플레이어가 현재 해당 퀘스트를 완료할 조건을 충족시켰는지?) 판단 함수

        if (returntype == false)
            return false;
        else
            return true;
    }
    
    // 퀘스트 완료 조건(플레이어가 현재 해당 퀘스트를 완료할 조건을 충족시켰는지?) 판단 함수
    public bool Check_Condition()
    {
        for (int i = 0; i < m_nl_ItemCode.Count; i++) // 특정 아이템 수량만큼 반복
        {
            if (m_nl_ItemCount_Max[i] <= m_nl_ItemCount_Current[i]) // 특정 아이템 수집 필요 수량 <= 특정 아이템 수집 현재 수량
            {
                continue;
            }
            else
            {
                m_bCondition = false;
                GUIManager_Total.Instance.UnDisplay_GUI_QuestStateInfo(this);
                return false;
            }
        }
        m_bCondition = true;
        return true;
    }
}
