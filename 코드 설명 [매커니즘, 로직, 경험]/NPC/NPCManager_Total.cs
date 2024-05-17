using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public static Dictionary<int, NPC_Total> m_Dictionary_NPC;
    public static List<Coroutine> m_CoroutineList_NPC_Store;

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
        m_CoroutineList_NPC_Store = new List<Coroutine>();
    }

    static float m_fUpdateTime = 300f;
    static float m_fCurrentTime = 0.0f;
    private void Update()
    {
        if (m_fCurrentTime < m_fUpdateTime)
        {
            m_fCurrentTime += Time.deltaTime;
        }
        else
        {
            m_fCurrentTime = 0.0f;
            UpdateNPC_Fix();
        }

        if (GUIManager_Total.Instance.m_GUI_Store != null)
        if (GUIManager_Total.Instance.m_GUI_Store.m_gPanel_Store.activeSelf == true)
        {
            GUIManager_Total.Instance.Update_StoreTime(m_fCurrentTime);
        }

        //if (Input.GetKeyDown(KeyCode.Insert))
        //{
        //    UpdateNPC_Fix();
        //}
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

    static void UpdateNPC_Fix()
    {
        foreach (KeyValuePair<int, NPC_Total> dictionary in m_Dictionary_NPC)
        {
            if (dictionary.Value != null)
            {
                dictionary.Value.UpdateIcon();
                if (dictionary.Value.m_bNPC_Store == true)
                {
                    for (int i = 0; i < dictionary.Value.m_List_NPC_Store.Count; i++)
                    {
                        if (dictionary.Value.m_List_NPC_Store[i].Check_Condition_Store() == true)
                        {
                            dictionary.Value.Update_Store();
                            Debug.Log("[" + dictionary.Value.m_sNPCName + "] 판매 목록 갱신.");
                        }
                    }
                }
            }
        }
        if (GUIManager_Total.Instance.m_GUI_Store.m_gPanel_Store.activeSelf == true)
        {
            GUIManager_Total.Instance.m_GUI_Store.Press_Btn_Exit();
            GUIManager_Total.Instance.Display_GUI_Store_Exit_Information();
        }
    }

    public void InitialSet()
    {
        //int num = 0;
        //foreach (KeyValuePair<int, NPC_Total> npc in m_Dictionary_NPC)
        //{
        //    npc.Value.InitialSet();
        //    Debug.Log(npc.Value.m_sNPCName);
        //    num += 1;
        //}
        //Debug.Log(num);
    }
}
