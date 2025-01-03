﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// ※ NPC_Total 클래스는 NPC에 관한 변수, 함수를 총괄한다. NPC가 보유한 대화, 퀘스트, 거래와 관련된 데이터를 가지고 있다.
//    NPC_Total 클래스를 기반으로 여러 클래스(NPC 종류)를 상속으로 구현했다. NPC 종류에 따라 각각 다른 데이터를 가상 함수를 이용해 구현했다.
//

public class NPC_Total : MonoBehaviour
{
    public string m_sNPCName;   // NPC 이름
    
    public int m_nNPCCode;      // NPC 고유코드
    
    public Sprite m_Sprite_NPC; // NPC 스프라이트(이미지 정보)

    // NPC가 보유한 대화 관련 변수
    public List <Conversation> m_cl_Conversation; // 대화 데이터

    // NPC가 보유한 퀘스트 관련 변수
    public List<Quest_KILL_MONSTER> m_ql_QuestList_KILL_MONSTER;           // 특정 몬스터 토벌 퀘스트 데이터
    public List<Quest_KILL_TYPE> m_ql_QuestList_KILL_TYPE;                 // 특정 몬스터 타입 토벌 퀘스트 데이터
    public List<Quest_GOAWAY_MONSTER> m_ql_QuestList_GOAWAY_MONSTER;       // 특정 몬스터 놓아주기 퀘스트 데이터
    public List<Quest_GOAWAY_TYPE> m_ql_QuestList_GOAWAY_TYPE;             // 특정 몬스터 타입 놓아주기 퀘스트 데이터
    public List<Quest_COLLECT> m_ql_QuestList_COLLECT;                     // 수집 퀘스트 데이터
    public List<Quest_CONVERSATION> m_ql_QuestList_CONVERSATION;           // 대화 퀘스트 데이터
    public List<Quest_ROLL> m_ql_QuestList_ROLL;                           // 구르기 퀘스트 데이터
    public List<Quest_ELIMINATE_MONSTER> m_ql_QuestList_ELIMINATE_MONSTER; // 특정 몬스터 제거(토벌 + 놓아주기) 퀘스트 데이터
    public List<Quest_ELIMINATE_TYPE> m_ql_QuestList_ELIMINATE_TYPE;       // 특정 몬스터 타입 제거(토벌 + 놓아주기) 퀘스트 데이터

    // NPC가 보유한 거래 관련 변수
    public List<NPC_Store> m_List_NPC_Store; // 거래 데이터
    public List<int> m_List_NPC_Store_Code;  // 거래 고유코드
    public bool m_bNPC_Store = false;        // 거래 존재 유무

    // NPC가 보유한 퀘스트, 거래 상태를 알려주기 위한 아이콘. 플레이어에게 퀘스트 진행 상태(수락 가능, 완료 가능, 수락 가능 + 완료 가능, 진행중), 거래 존재 유무에 따라 관련 아이콘이 활성화 또는 비활성화된다.
    public GameObject m_gNPC_Icon;
    public GameObject m_gIcon_Quest_Accept_Possible;         // 수락 가능한 퀘스트가 존재하는 경우 활성화되는 아이콘 : ?
    public GameObject m_gIcon_Quest_Clear_Possible;          // 완료 가능한 퀘스트가 존재하는 경우 활성화되는 아이콘 : !
    public GameObject m_gIcon_Quest_Accept_N_Clear_Possible; // 수락 및 완료 가능한 퀘스트가 존재하는 경우 활성화되는 아이콘 : ?!
    public GameObject m_gIcon_Quest_Process;                 // 진행중인 퀘스트가 존재하는 경우 활성화되는 아이콘 : ㆍㆍㆍ
    public GameObject m_gIcon_Quest_Process_;                // 진행중인 퀘스트가 존재하는 경우 활성화되는 아이콘 : ㆍㆍㆍ (진행중인 퀘스트만 존재하는 경우 해당 아이콘이 활성화된다.)
    public GameObject m_gIcon_Store;                         // 이용 가능한 거래가 존재하는 경우 활성화되는 아이콘 : STORE
    
    // NPC가 보유한 퀘스트 진행 상태 판단에 사용되는 변수
    [SerializeField] protected List<int> m_nList_Accept;  // 수락 가능한 퀘스트 고유코드 목록
    [SerializeField] protected List<int> m_nList_Clear;   // 완료 가능한 퀘스트 고유코드 목록
    [SerializeField] protected List<int> m_nList_Process; // 진행중인 퀘스트 고유코드 목록

    // NPC 초기 설정(대화, 퀘스트 설정 함수)(가상 함수)
    virtual public void InitialSet()
    {
        InitialSet_Icon(); // NPC가 보유한 퀘스트, 거래 상태를 알려주기 위한 아이콘 초기 설정
        InitialSet_Store(); // 거래 설정 함수
    }

    // NPC가 보유한 퀘스트, 거래 상태를 알려주기 위한 아이콘 초기 설정 함수
    protected void InitialSet_Icon()
    {
        m_gIcon_Quest_Accept_Possible = m_gNPC_Icon.transform.Find("QC_P").gameObject;
        m_gIcon_Quest_Clear_Possible = m_gNPC_Icon.transform.Find("Q_C").gameObject;
        m_gIcon_Quest_Accept_N_Clear_Possible = m_gNPC_Icon.transform.Find("QC_PC").gameObject;
        m_gIcon_Quest_Process = m_gNPC_Icon.transform.Find("Q_P").gameObject;
        m_gIcon_Quest_Process_ = m_gNPC_Icon.transform.Find("Q_P_").gameObject;
        m_gIcon_Store = m_gNPC_Icon.transform.Find("STORE").gameObject;

        m_gIcon_Quest_Accept_Possible.SetActive(false);
        m_gIcon_Quest_Clear_Possible.SetActive(false);
        m_gIcon_Quest_Accept_N_Clear_Possible.SetActive(false);
        m_gIcon_Quest_Process.SetActive(false);
        m_gIcon_Quest_Process_.SetActive(false);
        m_gIcon_Store.SetActive(false);

        m_nList_Accept = new List<int>();
        m_nList_Clear = new List<int>();
        m_nList_Process = new List<int>();

        StartCoroutine(ProcessStart()); // NPC가 보유한 퀘스트, 거래 상태를 알려주기 위한 아이콘 초기 설정 코루틴
                                        // 다른 데이터의 로딩을 마치고 실행하기 위해 1초의 대기시간을 가진다.(수정이 필요하다.)
    }

    // NPC가 보유한 퀘스트, 거래 상태를 알려주기 위한 아이콘 초기 설정 코루틴. 게임 로딩 이후 퀘스트, 거래 상태를 알려주기 위함으로 최초 1회만 실행된다.
    protected IEnumerator ProcessStart()
    {
        yield return new WaitForSeconds(1f); // 1초 대기
        UpdateIcon(); // NPC가 보유한 퀘스트, 거래 상태를 알려주기 위한 아이콘 갱신 함수
    }

    // NPC가 보유한 거래 설정 함수(가상 함수)
    virtual public void InitialSet_Store()
    {

    }

    // NPC가 보유한 거래 갱신 함수. NPC가 보유한 거래가 초기화(갱신) 된다. 판매 아이템 종류, 수량, 매매 가격 등이 변경된다.
    public void Update_Store()
    {
        for (int i = 0; i < m_List_NPC_Store.Count; i++)
        {
            m_List_NPC_Store[i].Initialization(); // 거래 갱신 함수. 5분마다 거래가 초기화(갱신) 된다. 판매 아이템 종류, 수량, 매매 가격 등이 변경된다.
        }
    }

    // NPC가 보유한 퀘스트, 거래 상태를 알려주기 위한 아이콘 갱신 함수(가상 함수)
    // 1. NPC가 보유한 모든 퀘스트의 진행 상태를 판단한다.
    // 2. 퀘스트 진행 상태에 따라 퀘스트 아이콘(활성화 / 비활성화)을 갱신한다. 이때 별도의 규칙을 따른다.
    // 3. NPC가 보유한 거래의 존재 유무에 따라 거래 아이콘(활성화 / 비활성화)을 갱신한다.
    virtual public int UpdateIcon()
    {
        if (this != null)
        {
            m_nList_Accept.Clear();
            m_nList_Clear.Clear();
            m_nList_Process.Clear();

            // 1. NPC가 보유한 모든 퀘스트의 진행 상태를 판단한다.
            // 1_1. NPC가 보유한 퀘스트(특정 몬스터 토벌 퀘스트)의 진행 상태를 판단하는 함수
            for (int i = 0; i < m_ql_QuestList_KILL_MONSTER.Count; i++)
            {
                UpdateIcon_Quest(m_ql_QuestList_KILL_MONSTER[i]);
            }
            // 1_2. NPC가 보유한 퀘스트(특정 몬스터 타입 토벌 퀘스트)의 진행 상태를 판단하는 함수
            for (int i = 0; i < m_ql_QuestList_KILL_TYPE.Count; i++)
            {
                UpdateIcon_Quest(m_ql_QuestList_KILL_TYPE[i]); 
            }
            // 1_3. NPC가 보유한 퀘스트(특정 몬스터 놓아주기 퀘스트)의 진행 상태를 판단하는 함수
            for (int i = 0; i < m_ql_QuestList_GOAWAY_MONSTER.Count; i++)
            {
                UpdateIcon_Quest(m_ql_QuestList_GOAWAY_MONSTER[i]); 
            }
            // 1_4. NPC가 보유한 퀘스트(특정 몬스터 타입 놓아주기 퀘스트)의 진행 상태를 판단하는 함수
            for (int i = 0; i < m_ql_QuestList_GOAWAY_TYPE.Count; i++)
            {
                UpdateIcon_Quest(m_ql_QuestList_GOAWAY_TYPE[i]); 
            }
            // 1_5. NPC가 보유한 퀘스트(수집 퀘스트)의 진행 상태를 판단하는 함수
            for (int i = 0; i < m_ql_QuestList_COLLECT.Count; i++)
            {
                UpdateIcon_Quest(m_ql_QuestList_COLLECT[i]); 
            }
            // 1_6. NPC가 보유한 퀘스트(대화 퀘스트)의 진행 상태를 판단하는 함수
            for (int i = 0; i < m_ql_QuestList_CONVERSATION.Count; i++)
            {
                UpdateIcon_Quest(m_ql_QuestList_CONVERSATION[i]); 
            }
            // 1_7. NPC가 보유한 퀘스트(구르기 퀘스트)의 진행 상태를 판단하는 함수
            for (int i = 0; i < m_ql_QuestList_ROLL.Count; i++)
            {
                UpdateIcon_Quest(m_ql_QuestList_ROLL[i]); 
            }
            // 1_8. NPC가 보유한 퀘스트(특정 몬스터 제거(토벌 + 놓아주기) 퀘스트)의 진행 상태를 판단하는 함수
            for (int i = 0; i < m_ql_QuestList_ELIMINATE_MONSTER.Count; i++)
            {
                UpdateIcon_Quest(m_ql_QuestList_ELIMINATE_MONSTER[i]); 
            }
            // 1_9. NPC가 보유한 퀘스트(특정 몬스터 타입 제거(토벌 + 놓아주기) 퀘스트)의 진행 상태를 판단하는 함수
            for (int i = 0; i < m_ql_QuestList_ELIMINATE_TYPE.Count; i++)
            {
                UpdateIcon_Quest(m_ql_QuestList_ELIMINATE_TYPE[i]); 
            }

            // 2. 퀘스트 진행 상태에 따라 퀘스트 아이콘(활성화 / 비활성화)을 갱신한다. 이때 별도의 규칙을 따른다.
            // 2_1. 진행중인 퀘스트가 존재하는 경우
            if (m_nList_Process.Count > 0)
            {
                // 2_1_1. 수락 가능한 퀘스트, 완료 가능한 퀘스트가 모두 존재하지 않는 경우 : 퀘스트 진행중[ㆍㆍㆍ] 아이콘 활성화
                if (m_nList_Accept.Count <= 0 && m_nList_Clear.Count <= 0)
                {
                    m_gIcon_Quest_Accept_Possible.SetActive(false);
                    m_gIcon_Quest_Clear_Possible.SetActive(false);
                    m_gIcon_Quest_Accept_N_Clear_Possible.SetActive(false);
                    m_gIcon_Quest_Process_.SetActive(true);
                    m_gIcon_Quest_Process.SetActive(false);
                }
                // 2_1_2. 수락 가능한 퀘스트는 존재하고, 완료 가능한 퀘스트는 존재하지 않는 경우 : 퀘스트 수락 가능[?], 퀘스트 진행중[ㆍㆍㆍ] 아이콘 활성화
                if (m_nList_Accept.Count > 0 && m_nList_Clear.Count <= 0)
                {
                    m_gIcon_Quest_Accept_Possible.SetActive(true);
                    m_gIcon_Quest_Clear_Possible.SetActive(false);
                    m_gIcon_Quest_Accept_N_Clear_Possible.SetActive(false);
                    m_gIcon_Quest_Process_.SetActive(false);
                    m_gIcon_Quest_Process.SetActive(true);
                }
                // 2_1_3. 수락 가능한 퀘스트는 존재하지 않고, 완료 가능한 퀘스트는 존재하는 경우 : 퀘스트 완료 가능[!], 퀘스트 진행중[ㆍㆍㆍ] 아이콘 활성화
                if (m_nList_Accept.Count <= 0 && m_nList_Clear.Count > 0)
                {
                    m_gIcon_Quest_Accept_Possible.SetActive(false);
                    m_gIcon_Quest_Clear_Possible.SetActive(true);
                    m_gIcon_Quest_Accept_N_Clear_Possible.SetActive(false);
                    m_gIcon_Quest_Process_.SetActive(false);
                    m_gIcon_Quest_Process.SetActive(true);
                }
                // 2_1_4. 수락 가능한 퀘스트, 완료 가능한 퀘스트가 모두 존재하는 경우 : 퀘스트 수락/완료 가능[?!], 퀘스트 진행중[ㆍㆍㆍ] 아이콘 활성화
                if (m_nList_Accept.Count > 0 && m_nList_Clear.Count > 0)
                {
                    m_gIcon_Quest_Accept_Possible.SetActive(false);
                    m_gIcon_Quest_Clear_Possible.SetActive(false);
                    m_gIcon_Quest_Accept_N_Clear_Possible.SetActive(true);
                    m_gIcon_Quest_Process_.SetActive(false);
                    m_gIcon_Quest_Process.SetActive(true);
                }
            }
            // 2_2. 진행중인 퀘스트가 존재하지 않는 경우
            else
            {
                // 2_2_1. 수락 가능한 퀘스트, 완료 가능한 퀘스트가 모두 존재하지 않는 경우 : 모든 아이콘 비활성화
                if (m_nList_Accept.Count <= 0 && m_nList_Clear.Count <= 0)
                {
                    m_gIcon_Quest_Accept_Possible.SetActive(false);
                    m_gIcon_Quest_Clear_Possible.SetActive(false);
                    m_gIcon_Quest_Accept_N_Clear_Possible.SetActive(false);
                    m_gIcon_Quest_Process_.SetActive(false);
                    m_gIcon_Quest_Process.SetActive(false);
                }
                // 2_2_2. 수락 가능한 퀘스트는 존재하고, 완료 가능한 퀘스트는 존재하지 않는 경우 : 퀘스트 수락 가능[?] 아이콘 활성화
                if (m_nList_Accept.Count > 0 && m_nList_Clear.Count <= 0)
                {
                    m_gIcon_Quest_Accept_Possible.SetActive(true);
                    m_gIcon_Quest_Clear_Possible.SetActive(false);
                    m_gIcon_Quest_Accept_N_Clear_Possible.SetActive(false);
                    m_gIcon_Quest_Process_.SetActive(false);
                    m_gIcon_Quest_Process.SetActive(false);
                }
                // 2_2_3. 수락 가능한 퀘스트는 존재하지 않고, 완료 가능한 퀘스트는 존재하는 경우 : 퀘스트 완료 가능[!] 아이콘 활성화
                if (m_nList_Accept.Count <= 0 && m_nList_Clear.Count > 0)
                {
                    m_gIcon_Quest_Accept_Possible.SetActive(false);
                    m_gIcon_Quest_Clear_Possible.SetActive(true);
                    m_gIcon_Quest_Accept_N_Clear_Possible.SetActive(false);
                    m_gIcon_Quest_Process_.SetActive(false);
                    m_gIcon_Quest_Process.SetActive(false);
                }
                // 2_2_4. 수락 가능한 퀘스트, 완료 가능한 퀘스트가 모두 존재하는 경우 : 퀘스트 수락/완료 가능[?!] 아이콘 활성화
                if (m_nList_Accept.Count > 0 && m_nList_Clear.Count > 0)
                {
                    m_gIcon_Quest_Accept_Possible.SetActive(false);
                    m_gIcon_Quest_Clear_Possible.SetActive(false);
                    m_gIcon_Quest_Accept_N_Clear_Possible.SetActive(true);
                    m_gIcon_Quest_Process_.SetActive(false);
                    m_gIcon_Quest_Process.SetActive(false);
                }
            }

            // 3. NPC가 보유한 거래의 존재 유무에 따라 거래 아이콘(활성화 / 비활성화)을 갱신한다.
            if (m_bNPC_Store == true)
            {
                for (int i = 0; i < m_List_NPC_Store.Count; i++)
                {
                    if (m_List_NPC_Store[i].Check_Condition_Store() == true) // 거래 이용 사전 조건 판단 함수 == true : 거래 이용 가능
                    {
                        m_gIcon_Store.SetActive(true); // 거래 아이콘[STORE] 활성화
                        return 0;
                    }
                }
                
                m_gIcon_Store.SetActive(false); // 이용 가능한 거래가 존재하지 않는 경우 거래 아이콘[STORE] 비활성화
            }

        }

        return 0;
    }
    // NPC가 보유한 퀘스트의 진행 상태를 판단하는 함수. 진행 상태는 m_nList_Accept, m_nList_Clear, m_nList_Process 변수에 할당된다.
    private void UpdateIcon_Quest(Quest quest) // quest : 진행 상태를 판단할 퀘스트
    {
        if (quest.Check_Condition_Total() == true) // 퀘스트 진행 사전 조건 판단 함수 == true : 퀘스트 진행 가능
        {
            if (quest.m_bClear == false) // 퀘스트 완료 여부 == false : 퀘스트를 완료하지 않은 경우
            {
                // 1. 수락 가능한 퀘스트의 경우
                if (quest.m_bProcess == false && quest.m_bCondition == false)
                {
                    // 퀘스트 발행 NPC의 퀘스트 수락 가능 아이콘만 활성화된다. 퀘스트 완료 NPC의 경우 별도의 아이콘 갱신이 이루어지지 않는다.
                    if (quest.m_nNPC == m_nNPCCode) // 퀘스트 발행 NPC와 NPC 고유코드가 동일한지 판단
                    {
                        m_nList_Accept.Add(quest.m_nQuest_Code); // 수락 가능한 퀘스트 고유코드 목록 추가
                    }
                }
                // 2. 완료 가능한 퀘스트의 경우
                if (quest.m_bCondition == true)
                {
                    // 퀘스트 완료 NPC의 경우 퀘스트 완료 가능 아이콘이 활성화된다.
                    if (quest.m_nNPC_Clear == m_nNPCCode) // 퀘스트 완료 NPC와 NPC 고유코드가 동일한지 판단
                    {
                        m_nList_Clear.Add(quest.m_nQuest_Code); // 완료 가능한 퀘스트 고유코드 목록 추가
                    }
                    // 퀘스트 발행 NPC의 경우 퀘스트 진행중 아이콘이 활성화된다.
                    else if (quest.m_nNPC == m_nNPCCode) // 퀘스트 발행 NPC와 NPC 고유코드가 동일한지 판단
                    {
                        m_nList_Process.Add(quest.m_nQuest_Code); // 진행중인 퀘스트 고유코드 목록 추가
                    }
                }
                // 3. 진행중인 퀘스트의 경우
                if (quest.m_bCondition == false && quest.m_bProcess == true)
                {
                    // 퀘스트 발행 NPC, 퀘스트 완료 NPC의 퀘스트 진행중 아이콘이 활성화된다.
                    m_nList_Process.Add(quest.m_nQuest_Code); // 진행중인 퀘스트 고유코드 목록 추가
                }
            }
        }
    }
}
