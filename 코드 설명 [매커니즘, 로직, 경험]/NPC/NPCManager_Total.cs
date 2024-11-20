using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// ※ 싱글톤패턴을 적용한 NPCManager_Total 클래스를 이용해 현재 위치한 게임 씬 내부의 모든 NPC를 관리한다.
//    현재 위치한 게임 씬 내부 NPC의 퀘스트, 거래 상태 아이콘 업데이트, 거래 초기화 작업이 이루어진다.
//

public class NPCManager_Total : MonoBehaviour
{
    private static NPCManager_Total instance = null;
    public static NPCManager_Total Instance
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

        m_Dictionary_NPC = new Dictionary<int, NPC_Total>();
    }
    
    public static Dictionary<int, NPC_Total> m_Dictionary_NPC; // 현재 위치한 게임 씬 내부 NPC 딕셔너리. Dictionary <Key : NPC 고유코드, Value : NPC 객체>

    // NPC가 보유한 거래 초기화 작업을 위한 타이머 변수
    static float m_fUpdateTime = 300f;  // 300초(5분)
    static float m_fCurrentTime = 0.0f; // 현재 시간(초)
    
    private void Update()
    {
        // 5분마다 거래 초기화 작업 수행
        if (m_fCurrentTime < m_fUpdateTime)
        {
            m_fCurrentTime += Time.deltaTime;
        }
        else
        {
            m_fCurrentTime = 0.0f; // 현재 시간 초기화
            UpdateNPC_Fix(); // 
        }

        if (GUIManager_Total.Instance.m_GUI_Store != null)
        if (GUIManager_Total.Instance.m_GUI_Store.m_gPanel_Store.activeSelf == true)
        {
            GUIManager_Total.Instance.Update_StoreTime(m_fCurrentTime);
        }
    }

    // 주기적인 NPC 업데이트.
    // ㄴ 상점 업데이트, Quest 현황 업데이트.
    //    ㄴ 상점: 물건이 다시 들어오고, 돈이 생길때까지 시간이 필요.
    //    ㄴ Quest: Quest 를 클리어 했다는 소문이 퍼지기까지 시간이 필요.
    public void UpdateNPC()
    {
        foreach(KeyValuePair<int, NPC_Total> dictionary in m_Dictionary_NPC)
        {
            if (dictionary.Value != null)
            {
                dictionary.Value.UpdateIcon();
            }
        }
    }

    // 현재 위치한 게임 씬 내부 NPC의 거래 갱신 함수. NPC가 보유한 거래가 초기화(갱신) 된다. 판매 아이템 종류, 수량, 매매 가격 등이 변경된다.
    static void UpdateNPC_Fix()
    {
        foreach (KeyValuePair<int, NPC_Total> dictionary in m_Dictionary_NPC)
        {
            if (dictionary.Value != null)
            {
                dictionary.Value.UpdateIcon(); // NPC가 보유한 퀘스트, 거래 상태를 알려주기 위한 아이콘 갱신 함수(가상 함수)
                if (dictionary.Value.m_bNPC_Store == true)
                {
                    for (int i = 0; i < dictionary.Value.m_List_NPC_Store.Count; i++)
                    {
                        if (dictionary.Value.m_List_NPC_Store[i].Check_Condition_Store() == true) // 거래 이용 사전 조건 판단 함수 == true : 거래 이용 가능
                        {
                            dictionary.Value.Update_Store(); // NPC가 보유한 거래 갱신 함수
                            // Debug.Log("[" + dictionary.Value.m_sNPCName + "] 판매 목록 갱신.");
                        }
                    }
                }
            }
        }

        // 플레이어가 NPC와 거래를 진행중일때 거래 초기화가 일어날 경우 해당 거래 종료
        if (GUIManager_Total.Instance.m_GUI_Store.m_gPanel_Store.activeSelf == true) // 거래 GUI 상태 == true : 거래 GUI 활성화 상태
        {
            GUIManager_Total.Instance.m_GUI_Store.Press_Btn_Exit(); // 거래 종료(거래 GUI 비활성화)
            GUIManager_Total.Instance.Display_GUI_Store_Exit_Information(); // 거래 종료(거래 초기화 알림 GUI 활성화)
        }
    }
}
