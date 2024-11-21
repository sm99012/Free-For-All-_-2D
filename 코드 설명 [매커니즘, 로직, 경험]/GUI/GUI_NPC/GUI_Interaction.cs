using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum E_INTERACTION_STATE { ON, OFF }

public class GUI_Interaction : MonoBehaviour
{
    [SerializeField] public GameObject m_gPanel_ChatBox;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_ChatBox_BTN;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_ChatBox_Exit_Flow;
    [SerializeField] Button m_BTN_ChatBox_Exit_Flow;
    [SerializeField] GameObject m_gPanel_ChatBox_Next;
    [SerializeField] Button m_BTN_ChatBox_Next;
    [SerializeField] GameObject m_gPanel_ChatBox_Before;
    [SerializeField] Button m_BTN_ChatBox_Before;
    [SerializeField] GameObject m_gPanel_ChatBox_Ok;
    [SerializeField] Button m_BTN_ChatBox_Ok;
    [SerializeField] GameObject m_gPanel_ChatBox_No;
    [SerializeField] Button m_BTN_ChatBox_No;
    [SerializeField] GameObject m_gPanel_ChatBox_Reward;
    [SerializeField] Button m_BTN_ChatBox_Reward;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_ChatBox_Exit_Free;
    [SerializeField] Button m_BTN_ChatBox_Exit_Free;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_ChatBox_Content;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_ChatBox_Content_ConversationBox;
    [SerializeField] TextMeshProUGUI m_TMP_ChatBox_Content_ConversationBox;
    [SerializeField] GameObject m_gPanel_ChatBox_Content_ConversationName;
    [SerializeField] TextMeshProUGUI m_TMP_ChatBox_Content_ConversationName;
    [SerializeField] GameObject m_gPanel_ChatBox_Content_NPCName;
    [SerializeField] TextMeshProUGUI m_TMP_ChatBox_Content_NPCName;
    [SerializeField] GameObject m_gPanel_ChatBox_Content_SelectBox;
    [SerializeField] GameObject m_gPanel_ChatBox_Content_SelectBox_Content;
    [SerializeField] GameObject m_gSV_ChatBox_Content_SelectBox_Content;
    [SerializeField] Scrollbar m_Scrollbar_Content_SelectBox;
    [SerializeField] GameObject m_gContent_ChatBox_Content_SelectBox_Content;

    [SerializeField] E_INTERACTION_STATE m_eInteraction_State = E_INTERACTION_STATE.OFF;

    public List<GameObject> m_gl_ButtonList;

    // 선택된 버튼 번호
    [SerializeField] int m_nChatbox_Selected_Number;
    // 선택된 버튼에 관한 스크롤 관련
    [SerializeField] int m_nChatbox_Selected_ScrollNumber;
    // 출력될 버튼 선택지 3개
    [SerializeField] List<int> m_nList_Chatbox_Selected_Display;
    // 버튼 선택지
    // true: BTN_Next, BTN_OK, BTN_Exit_Flow
    // false: BTN_Before, BTN_NO
    [SerializeField] bool m_bChatbox_Btn_State;

    public NPC_Total m_nt_NPC;

    public bool m_bConversation;
    public bool m_bQuest;

    bool m_bQuestClear;
    public E_QUEST_TYPE m_eQuestType;

    // 선택된 NPC 상호작용(대화, 퀘스트) 번호
    public int m_nInteractionNumber;
    // 대화 List 번호(순서)
    public int m_nInteractionProgressNumber;

    // 대화 스크립트 버퍼
    string m_sCQBuffer;

    public void InitialSet()
    {
        InitialSet_Object();
    }

    // 초기 Object 불러오기.
    void InitialSet_Object()
    {
        m_gPanel_ChatBox = GameObject.Find("Canvas_GUI").gameObject.transform.Find("Panel_ChatBox").gameObject;

        m_gPanel_ChatBox_BTN = m_gPanel_ChatBox.transform.Find("Panel_ChatBox_BTN").gameObject;

        m_gPanel_ChatBox_Exit_Flow = m_gPanel_ChatBox_BTN.transform.Find("Panel_ChatBox_Exit_Flow").gameObject;
        m_BTN_ChatBox_Exit_Flow = m_gPanel_ChatBox_Exit_Flow.transform.Find("BTN_ChatBox_Exit_Flow").gameObject.GetComponent<Button>();
        m_gPanel_ChatBox_Next = m_gPanel_ChatBox_BTN.transform.Find("Panel_ChatBox_Next").gameObject;
        m_BTN_ChatBox_Next = m_gPanel_ChatBox_Next.transform.Find("BTN_ChatBox_Next").gameObject.GetComponent<Button>();
        m_gPanel_ChatBox_Before = m_gPanel_ChatBox_BTN.transform.Find("Panel_ChatBox_Before").gameObject;
        m_BTN_ChatBox_Before = m_gPanel_ChatBox_Before.transform.Find("BTN_ChatBox_Before").gameObject.GetComponent<Button>();
        m_gPanel_ChatBox_Ok = m_gPanel_ChatBox_BTN.transform.Find("Panel_ChatBox_Ok").gameObject;
        m_BTN_ChatBox_Ok = m_gPanel_ChatBox_Ok.transform.Find("BTN_ChatBox_Ok").gameObject.GetComponent<Button>();
        m_gPanel_ChatBox_No = m_gPanel_ChatBox_BTN.transform.Find("Panel_ChatBox_No").gameObject;
        m_BTN_ChatBox_No = m_gPanel_ChatBox_No.transform.Find("BTN_ChatBox_No").gameObject.GetComponent<Button>();
        m_gPanel_ChatBox_Reward = m_gPanel_ChatBox_BTN.transform.Find("Panel_ChatBox_Reward").gameObject;
        m_BTN_ChatBox_Reward = m_gPanel_ChatBox_Reward.transform.Find("BTN_ChatBox_Reward").gameObject.GetComponent<Button>();

        m_gPanel_ChatBox_Exit_Free = m_gPanel_ChatBox.transform.Find("Panel_ChatBox_Exit_Free").gameObject;
        m_BTN_ChatBox_Exit_Free = m_gPanel_ChatBox_Exit_Free.transform.Find("BTN_ChatBox_Exit_Free").gameObject.GetComponent<Button>();

        m_gPanel_ChatBox_Content = m_gPanel_ChatBox.transform.Find("Panel_ChatBox_Content").gameObject;

        m_gPanel_ChatBox_Content_ConversationBox = m_gPanel_ChatBox_Content.transform.Find("Panel_ChatBox_Content_ConversationBox").gameObject;
        m_TMP_ChatBox_Content_ConversationBox = m_gPanel_ChatBox_Content_ConversationBox.transform.Find("TMP_ChatBox_Content_ConversationBox").gameObject.GetComponent<TextMeshProUGUI>();
        m_gPanel_ChatBox_Content_ConversationName = m_gPanel_ChatBox_Content.transform.Find("Panel_ChatBox_Content_ConversationName").gameObject;
        m_TMP_ChatBox_Content_ConversationName = m_gPanel_ChatBox_Content_ConversationName.transform.Find("TMP_ChatBox_Content_ConversationName").gameObject.GetComponent<TextMeshProUGUI>();
        m_gPanel_ChatBox_Content_NPCName = m_gPanel_ChatBox_Content.transform.Find("Panel_ChatBox_Content_NPCName").gameObject;
        m_TMP_ChatBox_Content_NPCName = m_gPanel_ChatBox_Content_NPCName.transform.Find("TMP_ChatBox_Content_NPCName").gameObject.GetComponent<TextMeshProUGUI>();
        m_gPanel_ChatBox_Content_SelectBox = m_gPanel_ChatBox_Content.transform.Find("Panel_ChatBox_Content_SelectBox").gameObject;
        m_gPanel_ChatBox_Content_SelectBox_Content = m_gPanel_ChatBox_Content_SelectBox.transform.Find("Panel_ChatBox_Content_SelectBox_Content").gameObject;
        m_gSV_ChatBox_Content_SelectBox_Content = m_gPanel_ChatBox_Content_SelectBox_Content.transform.Find("SV_ChatBox_Content_SelectBox_Content").gameObject;
        m_Scrollbar_Content_SelectBox = m_gSV_ChatBox_Content_SelectBox_Content.transform.Find("Scrollbar").gameObject.GetComponent<Scrollbar>();
        m_gContent_ChatBox_Content_SelectBox_Content = m_gSV_ChatBox_Content_SelectBox_Content.transform.Find("Viewport").gameObject.transform.Find("Content_ChatBox_Content_SelectBox_Content").gameObject;

        m_nList_Chatbox_Selected_Display = new List<int>();
    }

    public void Interaction(NPC_Total npc)
    {
        m_bQuestClear = false;
        m_nt_NPC = npc;

        m_bConversation = false;
        m_bQuest = false;

        m_gPanel_ChatBox_Content_SelectBox.SetActive(true);
        m_gPanel_ChatBox_BTN.SetActive(false);
        m_gPanel_ChatBox_Next.SetActive(false);
        m_gPanel_ChatBox_Before.SetActive(false);
        m_gPanel_ChatBox_Ok.SetActive(false);
        m_gPanel_ChatBox_No.SetActive(false);
        m_gPanel_ChatBox_Exit_Flow.SetActive(false);
        m_gPanel_ChatBox_Exit_Free.SetActive(true);
        m_gPanel_ChatBox_Reward.SetActive(false);

        m_gl_ButtonList = new List<GameObject>();

        m_gPanel_ChatBox.transform.SetAsLastSibling();
        m_gPanel_ChatBox.SetActive(true);
        m_TMP_ChatBox_Content_NPCName.text = npc.m_sNPCName;
        //m_tm_ChatName.text = npc.m_cl_Conversation[0].m_sConversation_Name;
        m_TMP_ChatBox_Content_ConversationName.text = "";

        m_nChatbox_Selected_Number = 0;

        m_eInteraction_State = E_INTERACTION_STATE.ON;

        Set_Btn_Exit();

        // 상점 목록 생성. 여러개.
        if (npc.m_bNPC_Store == true)
        {
            for (int i = 0; i < npc.m_List_NPC_Store.Count; i++)
            {
                if (npc.m_List_NPC_Store[i].Check_Condition_Store() == true)
                {
                    GameObject S_obj = Resources.Load("Prefab/GUI/Button_Chat_Store") as GameObject;
                    GameObject copyobj = Instantiate(S_obj);
                    copyobj.transform.position = m_gContent_ChatBox_Content_SelectBox_Content.transform.position;
                    copyobj.GetComponent<CreateButton>().SetButtonName("[거래]\n" + npc.m_List_NPC_Store[i].m_sStore_Name);
                    RectTransform btnpos = copyobj.GetComponent<RectTransform>();
                    btnpos.SetParent(m_gContent_ChatBox_Content_SelectBox_Content.transform);
                    btnpos.transform.localScale = new Vector3(1, 1, 1);
                    // 델리게이트
                    int jkj = i;
                    copyobj.GetComponent<Button>().onClick.RemoveAllListeners();
                    copyobj.GetComponent<Button>().onClick.AddListener(delegate { Store_Start(npc.m_List_NPC_Store[jkj]); });
                    m_gl_ButtonList.Add(copyobj);
                }
            }
        }

        // 대화 목록 생성
        GameObject C_obj = Resources.Load("Prefab/GUI/Button_Chat_Conversation") as GameObject;
        for (int i = 0; i < npc.m_cl_Conversation.Count; i++)
        {
            if (npc.m_cl_Conversation[i].Check_Condition_Total() == true)
            {
                int n = i;
                GameObject copyobj = Instantiate(C_obj);
                copyobj.transform.position = m_gContent_ChatBox_Content_SelectBox_Content.transform.position;
                copyobj.GetComponent<CreateButton>().SetButtonName(npc.m_cl_Conversation[i].m_sConversation_Title);
                RectTransform btnpos = copyobj.GetComponent<RectTransform>();
                btnpos.SetParent(m_gContent_ChatBox_Content_SelectBox_Content.transform);
                btnpos.transform.localScale = new Vector3(1, 1, 1);
                // 델리게이트
                copyobj.GetComponent<Button>().onClick.AddListener(delegate { Conversation_Start(n); });
                m_gl_ButtonList.Add(copyobj);
            }
        }

        // 퀘스트 목록 생성
        GameObject Q_obj = Resources.Load("Prefab/GUI/Button_Chat_Quest") as GameObject;
        for (int i = 0; i < npc.m_ql_QuestList_KILL_MONSTER.Count; i++)
        {
            Interaction_Create_QuestButton(npc, npc.m_ql_QuestList_KILL_MONSTER[i], Q_obj, i);
        }
        for (int i = 0; i < npc.m_ql_QuestList_KILL_TYPE.Count; i++)
        {
            Interaction_Create_QuestButton(npc, npc.m_ql_QuestList_KILL_TYPE[i], Q_obj, i);
        }
        for (int i = 0; i < npc.m_ql_QuestList_GOAWAY_MONSTER.Count; i++)
        {
            Interaction_Create_QuestButton(npc, npc.m_ql_QuestList_GOAWAY_MONSTER[i], Q_obj, i);
        }
        for (int i = 0; i < npc.m_ql_QuestList_GOAWAY_TYPE.Count; i++)
        {
            Interaction_Create_QuestButton(npc, npc.m_ql_QuestList_GOAWAY_TYPE[i], Q_obj, i);
        }
        for (int i = 0; i < npc.m_ql_QuestList_COLLECT.Count; i++)
        {
            Interaction_Create_QuestButton(npc, npc.m_ql_QuestList_COLLECT[i], Q_obj, i);
        }
        for (int i = 0; i < npc.m_ql_QuestList_CONVERSATION.Count; i++)
        {
            Interaction_Create_QuestButton(npc, npc.m_ql_QuestList_CONVERSATION[i], Q_obj, i);
        }
        for (int i = 0; i < npc.m_ql_QuestList_ROLL.Count; i++)
        {
            Interaction_Create_QuestButton(npc, npc.m_ql_QuestList_ROLL[i], Q_obj, i);
        }
        for (int i = 0; i < npc.m_ql_QuestList_ELIMINATE_MONSTER.Count; i++)
        {
            Interaction_Create_QuestButton(npc, npc.m_ql_QuestList_ELIMINATE_MONSTER[i], Q_obj, i);
        }
        for (int i = 0; i < npc.m_ql_QuestList_ELIMINATE_TYPE.Count; i++)
        {
            Interaction_Create_QuestButton(npc, npc.m_ql_QuestList_ELIMINATE_TYPE[i], Q_obj, i);
        }

        InitialSet_Select_CQS_Button();
    }
    private void Interaction_Create_QuestButton(NPC_Total npc, Quest quest, GameObject gquestbutton, int nindex)
    {
        if (quest.Check_Condition_Total() == true && quest.m_bClear == false)
        {
            if (quest.m_nNPC == npc.m_nNPCCode)
            {
                GameObject copyobj = Instantiate(gquestbutton);
                copyobj.transform.position = m_gContent_ChatBox_Content_SelectBox_Content.transform.position;
                copyobj.GetComponent<CreateButton>().SetButtonName(quest.m_sQuest_Title);
                copyobj.GetComponent<Button>().onClick.AddListener(delegate { Quest_Start(quest.m_eQuestType, nindex); });

                RectTransform btnpos = copyobj.GetComponent<RectTransform>();
                btnpos.SetParent(m_gContent_ChatBox_Content_SelectBox_Content.transform);
                btnpos.transform.localScale = new Vector3(1, 1, 1);

                m_gl_ButtonList.Add(copyobj);
            }
            else
            {
                if (quest.m_bProcess == true)
                {
                    GameObject copyobj = Instantiate(gquestbutton);
                    copyobj.transform.position = m_gContent_ChatBox_Content_SelectBox_Content.transform.position;
                    copyobj.GetComponent<CreateButton>().SetButtonName(quest.m_sQuest_Title);
                    copyobj.GetComponent<Button>().onClick.AddListener(delegate { Quest_Start(quest.m_eQuestType, nindex); });

                    RectTransform btnpos = copyobj.GetComponent<RectTransform>();
                    btnpos.SetParent(m_gContent_ChatBox_Content_SelectBox_Content.transform);
                    btnpos.transform.localScale = new Vector3(1, 1, 1);

                    m_gl_ButtonList.Add(copyobj);
                }
            }
        }
    }


    // NPC와 상호작용 중 키입력으로 다양한 기능을 하는 함수.
    // 0: ERROR
    // 1: 대화 스크립트 스킵
    // 2: 선택지 선택 ↑
    // 3: 선택지 선택 ↓
    // 4: 선택지 결정
    public int Interaction_In_SSD(KeyCode keycode)
    {
        if (m_gPanel_ChatBox.activeSelf == true)
        {
            switch (keycode)
            {
                case KeyCode.Space:
                    {
                        if (m_c_Conversation_ProcessPrint_Text != null || m_c_Quest_ProcessPrint_Text != null)
                        {
                            Skip_CQ_Script();

                            return 1;
                        }
                        else
                        {
                            Press_Button();

                            return 4;
                        }
                    }
                case KeyCode.UpArrow:
                    {
                        if (m_gPanel_ChatBox_Content_SelectBox.activeSelf == true)
                            Select_CQS_Button(-1);
                        else
                        {
                            if (m_c_Conversation_ProcessPrint_Text == null && m_c_Quest_ProcessPrint_Text == null)
                                Select_Button(false);
                        }
                    }
                    return 2;
                case KeyCode.DownArrow:
                    {
                        if (m_gPanel_ChatBox_Content_SelectBox.activeSelf == true)
                            Select_CQS_Button(+1);
                        else
                        {
                            if (m_c_Conversation_ProcessPrint_Text == null && m_c_Quest_ProcessPrint_Text == null)
                                Select_Button(true);
                        }
                    }
                    return 3;
                default:
                    return 0;
            }
        }
        return 0;
    }
    // 대화 스크립트 스킵
    public void Skip_CQ_Script()
    {
        if (m_gPanel_ChatBox_Content_SelectBox.activeSelf == false && m_sCQBuffer != string.Empty)
        {
            m_TMP_ChatBox_Content_ConversationBox.text = m_sCQBuffer;

            if (m_c_Conversation_ProcessPrint_Text != null)
            {
                StopCoroutine(m_c_Conversation_ProcessPrint_Text);
                m_c_Conversation_ProcessPrint_Text = null;

                Display_Select_Button();
            }
            if (m_c_Quest_ProcessPrint_Text != null)
            {
                StopCoroutine(m_c_Quest_ProcessPrint_Text);
                m_c_Quest_ProcessPrint_Text = null;

                Display_Select_Button();
            }

            m_sCQBuffer = string.Empty;
        }
    }
    // 선택지 선택 ↑ / ↓
    // 초기화(시작)
    void InitialSet_Select_CQS_Button()
    {
        m_nList_Chatbox_Selected_Display.Clear();
        m_nList_Chatbox_Selected_Display.Add(0);
        m_nList_Chatbox_Selected_Display.Add(1);
        m_nList_Chatbox_Selected_Display.Add(2);

        if (m_gl_ButtonList.Count > 0)
            Select_CQS_Button();
    }
    // 순차적 선택지 변경
    public void Select_CQS_Button(int changevalue = 0)
    {
        if (changevalue != 0)
        {
            if (m_gl_ButtonList.Count > 3)
            {
                if (m_nList_Chatbox_Selected_Display.Contains(m_nChatbox_Selected_Number) == true)
                {
                    if (m_nList_Chatbox_Selected_Display.IndexOf(m_nChatbox_Selected_Number) == 0)
                    {
                        if (m_nChatbox_Selected_Number + changevalue >= 0)
                        {
                            if (m_nList_Chatbox_Selected_Display.Contains(m_nChatbox_Selected_Number + changevalue) == false)
                            {
                                //Debug.Log("스크롤 변경 ↑: " + (m_nChatbox_Selected_Number));
                                for (int i = 0; i < m_nList_Chatbox_Selected_Display.Count; i++)
                                {
                                    m_nList_Chatbox_Selected_Display[i] -= 1;
                                }
                                m_Scrollbar_Content_SelectBox.value = m_Scrollbar_Content_SelectBox.value + ((float)1 / (float)(m_gl_ButtonList.Count - 3));
                            }
                        }
                    }
                    else if (m_nList_Chatbox_Selected_Display.IndexOf(m_nChatbox_Selected_Number) == 2)
                    {
                        if (m_nChatbox_Selected_Number + changevalue < m_gl_ButtonList.Count)
                        {
                            if (m_nList_Chatbox_Selected_Display.Contains(m_nChatbox_Selected_Number + changevalue) == false)
                            {
                                //Debug.Log("스크롤 변경 ↓: " + (m_nChatbox_Selected_Number));
                                for (int i = 0; i < m_nList_Chatbox_Selected_Display.Count; i++)
                                {
                                    m_nList_Chatbox_Selected_Display[i] += 1;
                                }
                                m_Scrollbar_Content_SelectBox.value = m_Scrollbar_Content_SelectBox.value - ((float)1 / (float)(m_gl_ButtonList.Count - 3));
                            }
                        }
                    }
                }
            }
        }

        m_gl_ButtonList[m_nChatbox_Selected_Number].GetComponent<ChatBox_ContentButton>().UnDisplay_Chat_Selected();

        if (m_nChatbox_Selected_Number + changevalue < m_gl_ButtonList.Count && m_nChatbox_Selected_Number + changevalue >= 0)
        {
            m_gl_ButtonList[m_nChatbox_Selected_Number += changevalue].GetComponent<ChatBox_ContentButton>().Display_Chat_Selected();
        }
        else
        {
            m_gl_ButtonList[m_nChatbox_Selected_Number].GetComponent<ChatBox_ContentButton>().Display_Chat_Selected();
        }
    }
    // 버튼 선택 ↑ / ↓
    // true: BTN_Next, BTN_OK, BTN_Exit_Flow
    // false: BTN_Before, BTN_NO
    void Display_Select_Button()
    {
        if (m_gPanel_ChatBox_Next.activeSelf == true && m_gPanel_ChatBox_Before.activeSelf == true)
        {
            m_gPanel_ChatBox_Next.GetComponent<ChatBox_ContentButton>().Display_Chat_Selected();
            m_gPanel_ChatBox_Before.GetComponent<ChatBox_ContentButton>().UnDisplay_Chat_Selected();
            m_bChatbox_Btn_State = true;
        }
        else if (m_gPanel_ChatBox_Next.activeSelf == true && m_gPanel_ChatBox_Before.activeSelf == false)
        {
            m_gPanel_ChatBox_Next.GetComponent<ChatBox_ContentButton>().Display_Chat_Selected();
            m_gPanel_ChatBox_Before.GetComponent<ChatBox_ContentButton>().UnDisplay_Chat_Selected();
            m_bChatbox_Btn_State = true;
        }
        else if (m_gPanel_ChatBox_Next.activeSelf == false && m_gPanel_ChatBox_Before.activeSelf == true)
        {
            m_gPanel_ChatBox_Next.GetComponent<ChatBox_ContentButton>().UnDisplay_Chat_Selected();
            m_gPanel_ChatBox_Before.GetComponent<ChatBox_ContentButton>().Display_Chat_Selected();
            m_bChatbox_Btn_State = false;
        }

        if (m_gPanel_ChatBox_Ok.activeSelf == true && m_gPanel_ChatBox_No.activeSelf == true)
        {
            m_gPanel_ChatBox_Ok.GetComponent<ChatBox_ContentButton>().Display_Chat_Selected();
            m_gPanel_ChatBox_No.GetComponent<ChatBox_ContentButton>().UnDisplay_Chat_Selected();
            m_bChatbox_Btn_State = true;
        }

        if (m_gPanel_ChatBox_Exit_Flow.activeSelf == true && m_gPanel_ChatBox_Before.activeSelf == true)
        {
            m_gPanel_ChatBox_Exit_Flow.GetComponent<ChatBox_ContentButton>().Display_Chat_Selected();
            m_gPanel_ChatBox_Before.GetComponent<ChatBox_ContentButton>().UnDisplay_Chat_Selected();
            m_bChatbox_Btn_State = true;
        }
        if (m_gPanel_ChatBox_Exit_Flow.activeSelf == true && m_gPanel_ChatBox_Before.activeSelf == false)
        {
            m_gPanel_ChatBox_Exit_Flow.GetComponent<ChatBox_ContentButton>().Display_Chat_Selected();
            m_bChatbox_Btn_State = true;
        }
    }
    void Select_Button(bool logic)
    {
        m_bChatbox_Btn_State = logic;
        if (m_bChatbox_Btn_State == true)
        {
            if (m_gPanel_ChatBox_Next.activeSelf == true && m_gPanel_ChatBox_Before.activeSelf == true)
            {
                m_gPanel_ChatBox_Next.GetComponent<ChatBox_ContentButton>().Display_Chat_Selected();
                m_gPanel_ChatBox_Before.GetComponent<ChatBox_ContentButton>().UnDisplay_Chat_Selected();
            }
            else if (m_gPanel_ChatBox_Next.activeSelf == true && m_gPanel_ChatBox_Before.activeSelf == false)
            {
                m_gPanel_ChatBox_Next.GetComponent<ChatBox_ContentButton>().Display_Chat_Selected();
                m_gPanel_ChatBox_Before.GetComponent<ChatBox_ContentButton>().UnDisplay_Chat_Selected();
            }
            else if (m_gPanel_ChatBox_Next.activeSelf == false && m_gPanel_ChatBox_Before.activeSelf == true)
            {
                m_gPanel_ChatBox_Next.GetComponent<ChatBox_ContentButton>().UnDisplay_Chat_Selected();
                m_gPanel_ChatBox_Before.GetComponent<ChatBox_ContentButton>().Display_Chat_Selected();
            }

            if (m_gPanel_ChatBox_Ok.activeSelf == true && m_gPanel_ChatBox_No.activeSelf == true)
            {
                m_gPanel_ChatBox_Ok.GetComponent<ChatBox_ContentButton>().Display_Chat_Selected();
                m_gPanel_ChatBox_No.GetComponent<ChatBox_ContentButton>().UnDisplay_Chat_Selected();
            }


            if (m_gPanel_ChatBox_Next.activeSelf == false && m_gPanel_ChatBox_Exit_Flow.activeSelf == true && m_gPanel_ChatBox_Before.activeSelf == true)
            {
                m_gPanel_ChatBox_Exit_Flow.GetComponent<ChatBox_ContentButton>().Display_Chat_Selected();
                m_gPanel_ChatBox_Before.GetComponent<ChatBox_ContentButton>().UnDisplay_Chat_Selected();
            }
            if (m_gPanel_ChatBox_Exit_Flow.activeSelf == true && m_gPanel_ChatBox_Before.activeSelf == false)
            {
                m_gPanel_ChatBox_Exit_Flow.GetComponent<ChatBox_ContentButton>().Display_Chat_Selected();
            }
        }
        else
        {
            if (m_gPanel_ChatBox_Next.activeSelf == true && m_gPanel_ChatBox_Before.activeSelf == true)
            {
                m_gPanel_ChatBox_Next.GetComponent<ChatBox_ContentButton>().UnDisplay_Chat_Selected();
                m_gPanel_ChatBox_Before.GetComponent<ChatBox_ContentButton>().Display_Chat_Selected();
            }
            else if (m_gPanel_ChatBox_Next.activeSelf == true && m_gPanel_ChatBox_Before.activeSelf == false)
            {
                m_gPanel_ChatBox_Next.GetComponent<ChatBox_ContentButton>().Display_Chat_Selected();
                m_gPanel_ChatBox_Before.GetComponent<ChatBox_ContentButton>().UnDisplay_Chat_Selected();
            }
            else if (m_gPanel_ChatBox_Next.activeSelf == false && m_gPanel_ChatBox_Before.activeSelf == true)
            {
                m_gPanel_ChatBox_Next.GetComponent<ChatBox_ContentButton>().UnDisplay_Chat_Selected();
                m_gPanel_ChatBox_Before.GetComponent<ChatBox_ContentButton>().Display_Chat_Selected();
            }

            if (m_gPanel_ChatBox_Ok.activeSelf == true && m_gPanel_ChatBox_No.activeSelf == true)
            {
                m_gPanel_ChatBox_Ok.GetComponent<ChatBox_ContentButton>().UnDisplay_Chat_Selected();
                m_gPanel_ChatBox_No.GetComponent<ChatBox_ContentButton>().Display_Chat_Selected();
            }

            if (m_gPanel_ChatBox_Next.activeSelf == false && m_gPanel_ChatBox_Exit_Flow.activeSelf == true && m_gPanel_ChatBox_Before.activeSelf == true)
            {
                m_gPanel_ChatBox_Exit_Flow.GetComponent<ChatBox_ContentButton>().UnDisplay_Chat_Selected();
                m_gPanel_ChatBox_Before.GetComponent<ChatBox_ContentButton>().Display_Chat_Selected();
            }
            if (m_gPanel_ChatBox_Exit_Flow.activeSelf == true && m_gPanel_ChatBox_Before.activeSelf == false)
            {
                m_gPanel_ChatBox_Exit_Flow.GetComponent<ChatBox_ContentButton>().Display_Chat_Selected();
            }
        }
    }
    public void Press_Button()
    {
        if (m_gPanel_ChatBox_Content_SelectBox.activeSelf == false)
        {
            if (m_bChatbox_Btn_State == true)
            {
                if (m_gPanel_ChatBox_Exit_Flow.activeSelf == false)
                {
                    if (m_gPanel_ChatBox_Next.activeSelf == true && m_gPanel_ChatBox_Before.activeSelf == true)
                    {
                        if (m_bConversation == true && m_bQuest == false)
                            Conversation_Next();
                        else if (m_bConversation == false && m_bQuest == true)
                            Quest_Next(m_eQuestType);
                        return;
                    }
                    else if (m_gPanel_ChatBox_Next.activeSelf == true && m_gPanel_ChatBox_Before.activeSelf == false)
                    {
                        if (m_bConversation == true && m_bQuest == false)
                            Conversation_Next();
                        else if (m_bConversation == false && m_bQuest == true)
                            Quest_Next(m_eQuestType);
                        return;
                    }
                    else if (m_gPanel_ChatBox_Next.activeSelf == false && m_gPanel_ChatBox_Before.activeSelf == true)
                    {
                        if (m_bConversation == true && m_bQuest == false)
                            Conversation_Before();
                        else if (m_bConversation == false && m_bQuest == true)
                            Quest_Before(m_eQuestType);
                        return;
                    }

                    if (m_gPanel_ChatBox_Ok.activeSelf == true && m_gPanel_ChatBox_No.activeSelf == true)
                    {
                        Quest_Set_Btn_Ok(m_eQuestType);
                        return;
                    }
                }

                if (m_gPanel_ChatBox_Next.activeSelf == false && m_gPanel_ChatBox_Exit_Flow.activeSelf == true && m_gPanel_ChatBox_Before.activeSelf == true)
                {
                    Exit();
                    return;
                }
                if (m_gPanel_ChatBox_Next.activeSelf == false && m_gPanel_ChatBox_Exit_Flow.activeSelf == true && m_gPanel_ChatBox_Before.activeSelf == false)
                {
                    Exit(m_eQuestType);
                    return;
                }
            }
            else
            {
                if (m_gPanel_ChatBox_Exit_Flow.activeSelf == false)
                {
                    if (m_gPanel_ChatBox_Next.activeSelf == true && m_gPanel_ChatBox_Before.activeSelf == true)
                    {
                        if (m_bConversation == true && m_bQuest == false)
                            Conversation_Before();
                        else if (m_bConversation == false && m_bQuest == true)
                            Quest_Before(m_eQuestType);
                        return;
                    }
                    else if (m_gPanel_ChatBox_Next.activeSelf == true && m_gPanel_ChatBox_Before.activeSelf == false)
                    {
                        if (m_bConversation == true && m_bQuest == false)
                            Conversation_Next();
                        else if (m_bConversation == false && m_bQuest == true)
                            Quest_Next(m_eQuestType);
                        return;
                    }
                    else if (m_gPanel_ChatBox_Next.activeSelf == false && m_gPanel_ChatBox_Before.activeSelf == true)
                    {
                        if (m_bConversation == true && m_bQuest == false)
                            Conversation_Before();
                        else if (m_bConversation == false && m_bQuest == true)
                            Quest_Before(m_eQuestType);
                        return;
                    }

                    if (m_gPanel_ChatBox_Ok.activeSelf == true && m_gPanel_ChatBox_No.activeSelf == true)
                    {
                        Quest_Set_Btn_No(m_eQuestType);
                        return;
                    }
                }

                if (m_gPanel_ChatBox_Next.activeSelf == false && m_gPanel_ChatBox_Exit_Flow.activeSelf == true && m_gPanel_ChatBox_Before.activeSelf == true)
                {
                    Conversation_Before();
                    return;
                }
                if (m_gPanel_ChatBox_Next.activeSelf == false && m_gPanel_ChatBox_Exit_Flow.activeSelf == true && m_gPanel_ChatBox_Before.activeSelf == false)
                {
                    Exit(m_eQuestType);
                    return;
                }
            }
        }
        else
        {
            int num = m_nChatbox_Selected_Number;
            // 상점
            for (int i = 0; i < m_nt_NPC.m_List_NPC_Store.Count; i++)
            {
                if (num != 0)
                {
                    if (m_nt_NPC.m_List_NPC_Store[i].Check_Condition_Store() == true)
                        num -= 1;
                }
                else if (num == 0)
                {
                    if (m_nt_NPC.m_List_NPC_Store[i].Check_Condition_Store() == true)
                    {
                        Store_Start(m_nt_NPC.m_List_NPC_Store[i]);
                        return;
                    }
                }
            }
            // 대화
            for (int i = 0; i < m_nt_NPC.m_cl_Conversation.Count; i++)
            {
                if (num != 0)
                {
                    if (m_nt_NPC.m_cl_Conversation[i].Check_Condition_Total() == true)
                        num -= 1;
                }
                else if (num == 0)
                {
                    if (m_nt_NPC.m_cl_Conversation[i].Check_Condition_Total() == true)
                    {
                        Conversation_Start(i);
                        return;
                    }
                }
            }
            // 퀘스트
            for (int i = 0; i < m_nt_NPC.m_ql_QuestList_KILL_MONSTER.Count; i++)
            {
                if ((num = Press_Button_C(num, m_nt_NPC.m_ql_QuestList_KILL_MONSTER[i], i)) == 0) return;
            }
            for (int i = 0; i < m_nt_NPC.m_ql_QuestList_KILL_TYPE.Count; i++)
            {
                if ((num = Press_Button_C(num, m_nt_NPC.m_ql_QuestList_KILL_TYPE[i], i)) == 0) return;
            }
            for (int i = 0; i < m_nt_NPC.m_ql_QuestList_GOAWAY_MONSTER.Count; i++)
            {
                if ((num = Press_Button_C(num, m_nt_NPC.m_ql_QuestList_GOAWAY_MONSTER[i], i)) == 0) return;
            }
            for (int i = 0; i < m_nt_NPC.m_ql_QuestList_GOAWAY_TYPE.Count; i++)
            {
                if ((num = Press_Button_C(num, m_nt_NPC.m_ql_QuestList_GOAWAY_TYPE[i], i)) == 0) return;
            }
            for (int i = 0; i < m_nt_NPC.m_ql_QuestList_COLLECT.Count; i++)
            {
                if ((num = Press_Button_C(num, m_nt_NPC.m_ql_QuestList_COLLECT[i], i)) == 0) return;
            }
            for (int i = 0; i < m_nt_NPC.m_ql_QuestList_CONVERSATION.Count; i++)
            {
                if ((num = Press_Button_C(num, m_nt_NPC.m_ql_QuestList_CONVERSATION[i], i)) == 0) return;
            }
            for (int i = 0; i < m_nt_NPC.m_ql_QuestList_ROLL.Count; i++)
            {
                if ((num = Press_Button_C(num, m_nt_NPC.m_ql_QuestList_ROLL[i], i)) == 0) return;
            }
            for (int i = 0; i < m_nt_NPC.m_ql_QuestList_ELIMINATE_MONSTER.Count; i++)
            {
                if ((num = Press_Button_C(num, m_nt_NPC.m_ql_QuestList_ELIMINATE_MONSTER[i], i)) == 0) return;
            }
            for (int i = 0; i < m_nt_NPC.m_ql_QuestList_ELIMINATE_TYPE.Count; i++)
            {
                if ((num = Press_Button_C(num, m_nt_NPC.m_ql_QuestList_ELIMINATE_TYPE[i], i)) == 0) return;
            }
        }
    }
    private int Press_Button_C(int num, Quest quest, int questindex)
    {
        if (num != 0)
        {
            if (quest.Check_Condition_Total() == true && quest.m_bClear == false)
                num -= 1;
        }
        else if (num == 0)
        {
            if (quest.Check_Condition_Total() == true && quest.m_bClear == false)
                Quest_Start(quest.m_eQuestType, questindex);
        }
        return num;
    }


    // NPC와 상호작용(대화창)이 종료되면 실행되는 함수.
    // Quest 종료.
    public void Exit(E_QUEST_TYPE qt)
    {
        if (m_c_Conversation_ProcessPrint_Text == null && m_c_Quest_ProcessPrint_Text == null)
        {
            m_Scrollbar_Content_SelectBox.value = 1;

            if (m_c_Conversation_ProcessPrint_Text != null)
            {
                StopCoroutine(m_c_Conversation_ProcessPrint_Text);
            }
            if (m_c_Quest_ProcessPrint_Text != null)
            {
                StopCoroutine(m_c_Quest_ProcessPrint_Text);
            }

            switch (qt)
            {
                case E_QUEST_TYPE.KILL_MONSTER:
                    {
                        if (m_bQuestClear == true)
                            Player_Total.Instance.GetQuestReward(m_nt_NPC.m_ql_QuestList_KILL_MONSTER[m_nInteractionNumber]);
                    }
                    break;
                case E_QUEST_TYPE.KILL_TYPE:
                    {
                        if (m_bQuestClear == true)
                            Player_Total.Instance.GetQuestReward(m_nt_NPC.m_ql_QuestList_KILL_TYPE[m_nInteractionNumber]);
                    }
                    break;
                case E_QUEST_TYPE.GOAWAY_MONSTER:
                    {
                        if (m_bQuestClear == true)
                            Player_Total.Instance.GetQuestReward(m_nt_NPC.m_ql_QuestList_GOAWAY_MONSTER[m_nInteractionNumber]);
                    }
                    break;
                case E_QUEST_TYPE.GOAWAY_TYPE:
                    {
                        if (m_bQuestClear == true)
                            Player_Total.Instance.GetQuestReward(m_nt_NPC.m_ql_QuestList_GOAWAY_TYPE[m_nInteractionNumber]);
                    }
                    break;
                case E_QUEST_TYPE.COLLECT:
                    {
                        if (m_bQuestClear == true)
                            Player_Total.Instance.GetQuestReward(m_nt_NPC.m_ql_QuestList_COLLECT[m_nInteractionNumber]);
                    }
                    break;
                case E_QUEST_TYPE.CONVERSATION:
                    {
                        if (m_bQuestClear == true)
                            Player_Total.Instance.GetQuestReward(m_nt_NPC.m_ql_QuestList_CONVERSATION[m_nInteractionNumber]);
                    }
                    break;
                case E_QUEST_TYPE.ROLL:
                    {
                        if (m_bQuestClear == true)
                            Player_Total.Instance.GetQuestReward(m_nt_NPC.m_ql_QuestList_ROLL[m_nInteractionNumber]);
                    }
                    break;
                case E_QUEST_TYPE.ELIMINATE_MONSTER:
                    {
                        if (m_bQuestClear == true)
                            Player_Total.Instance.GetQuestReward(m_nt_NPC.m_ql_QuestList_ELIMINATE_MONSTER[m_nInteractionNumber]);
                    }
                    break;
                case E_QUEST_TYPE.ELIMINATE_TYPE:
                    {
                        if (m_bQuestClear == true)
                            Player_Total.Instance.GetQuestReward(m_nt_NPC.m_ql_QuestList_ELIMINATE_TYPE[m_nInteractionNumber]);
                    }
                    break;
            }

            m_BTN_ChatBox_Next.GetComponent<Button>().onClick.RemoveAllListeners();
            m_BTN_ChatBox_Before.GetComponent<Button>().onClick.RemoveAllListeners();
            m_BTN_ChatBox_Ok.GetComponent<Button>().onClick.RemoveAllListeners();
            m_BTN_ChatBox_No.GetComponent<Button>().onClick.RemoveAllListeners();
            m_BTN_ChatBox_Exit_Flow.GetComponent<Button>().onClick.RemoveAllListeners();
            m_BTN_ChatBox_Exit_Free.GetComponent<Button>().onClick.RemoveAllListeners();
            m_BTN_ChatBox_Reward.GetComponent<Button>().onClick.RemoveAllListeners();

            m_gPanel_ChatBox.SetActive(false);
            m_gPanel_ChatBox_Exit_Flow.SetActive(false);
            m_gPanel_ChatBox_Exit_Free.SetActive(false);
            m_nt_NPC = null;
            m_TMP_ChatBox_Content_ConversationBox.text = "";
            End_Interaction();
            m_nInteractionNumber = 0;

            NPCManager_Total.Instance.UpdateNPC();
        }
        else
        {
            if (m_c_Conversation_ProcessPrint_Text != null)
            {
                Skip_CQ_Script();
            }
            if (m_c_Quest_ProcessPrint_Text != null)
            {
                Skip_CQ_Script();
            }
        }
    }
    // Conversation 종료
    public void Exit()
    {
        if (m_c_Conversation_ProcessPrint_Text == null && m_c_Quest_ProcessPrint_Text == null)
        {
            m_Scrollbar_Content_SelectBox.value = 1;

            m_BTN_ChatBox_Next.GetComponent<Button>().onClick.RemoveAllListeners();
            m_BTN_ChatBox_Before.GetComponent<Button>().onClick.RemoveAllListeners();
            m_BTN_ChatBox_Ok.GetComponent<Button>().onClick.RemoveAllListeners();
            m_BTN_ChatBox_No.GetComponent<Button>().onClick.RemoveAllListeners();
            m_BTN_ChatBox_Exit_Flow.GetComponent<Button>().onClick.RemoveAllListeners();
            m_BTN_ChatBox_Exit_Free.GetComponent<Button>().onClick.RemoveAllListeners();
            m_BTN_ChatBox_Reward.GetComponent<Button>().onClick.RemoveAllListeners();

            m_gPanel_ChatBox.SetActive(false);
            m_gPanel_ChatBox_Exit_Flow.SetActive(false);
            m_gPanel_ChatBox_Exit_Free.SetActive(false);
            m_gPanel_ChatBox_Reward.SetActive(false);
            m_nt_NPC = null;
            m_TMP_ChatBox_Content_ConversationBox.text = "";
            End_Interaction();
            m_nInteractionNumber = 0;
        }
        else
        {
            if (m_c_Conversation_ProcessPrint_Text != null)
            {
                Skip_CQ_Script();
            }
            if (m_c_Quest_ProcessPrint_Text != null)
            {
                Skip_CQ_Script();
            }
        }
    }
    // 버튼 삭제, UI 닫기.
    public void End_Interaction()
    {
        m_nChatbox_Selected_Number = 0;
        m_nChatbox_Selected_ScrollNumber = 0;

        m_gPanel_ChatBox_Next.GetComponent<ChatBox_ContentButton>().UnDisplay_Chat_Selected();
        m_gPanel_ChatBox_Before.GetComponent<ChatBox_ContentButton>().UnDisplay_Chat_Selected();
        m_gPanel_ChatBox_Exit_Flow.GetComponent<ChatBox_ContentButton>().UnDisplay_Chat_Selected();
        m_gPanel_ChatBox_Ok.GetComponent<ChatBox_ContentButton>().UnDisplay_Chat_Selected();
        m_gPanel_ChatBox_No.GetComponent<ChatBox_ContentButton>().UnDisplay_Chat_Selected();

        for (int i = 0; i < m_gl_ButtonList.Count; i++)
        {
            m_gl_ButtonList[i].gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
            Destroy(m_gl_ButtonList[i].gameObject);
        }
        m_gl_ButtonList.Clear();
        Player_Total.Instance.m_pc_Camera.ZoomOut();
        Player_Total.Instance.ExitConversation();

        m_eInteraction_State = E_INTERACTION_STATE.OFF;

        if (GUIManager_Total.Instance.m_GUI_QuestReward.m_gPanel_QuestReward.activeSelf == true)
            GUIManager_Total.Instance.m_GUI_QuestReward.Press_Btn_Exit();

        GUIManager_Total.Instance.Delete_GUI_Priority(3);
    }

    public void Set_Btn_Exit()
    {
        m_BTN_ChatBox_Exit_Free.GetComponent<Button>().onClick.RemoveAllListeners();
        m_BTN_ChatBox_Exit_Free.GetComponent<Button>().onClick.AddListener(delegate { Exit(); });
    }

    public void Store_Start(NPC_Store store)
    {
        m_bConversation = false;
        m_bQuest = false;

        GUIManager_Total.Instance.Display_GUI_Store(store);
        //Debug.Log(store.m_sDescription);

        m_gPanel_ChatBox.SetActive(false);
        //Exit();
    }

    // 대화 관련 상호작용
    //
    // 다음으로, 이전으로 버튼 기능 추가하기.
    public void Conversation_Set_Btn_Next()
    {
        m_BTN_ChatBox_Next.GetComponent<Button>().onClick.AddListener(delegate { Conversation_Next(); });
    }
    public void Conversation_Set_Btn_Before()
    {
        m_BTN_ChatBox_Before.GetComponent<Button>().onClick.AddListener(delegate { Conversation_Before(); });
    }
    public void Conversation_Set_Btn_Exit()
    {
        m_BTN_ChatBox_Exit_Flow.GetComponent<Button>().onClick.RemoveAllListeners();
        m_BTN_ChatBox_Exit_Flow.GetComponent<Button>().onClick.AddListener(delegate { Exit(); });
    }

    public void Conversation_Start(int n)
    {
        m_bConversation = true;
        m_bQuest = false;

        m_nInteractionNumber = 0;
        m_nInteractionProgressNumber = 0;

        m_gPanel_ChatBox_Content_SelectBox.SetActive(false);
        m_gPanel_ChatBox_BTN.SetActive(true);
        m_gPanel_ChatBox_Next.SetActive(true);
        m_gPanel_ChatBox_Before.SetActive(true);
        m_gPanel_ChatBox_Exit_Flow.SetActive(false);
        m_gPanel_ChatBox_Exit_Free.SetActive(true);
        m_gPanel_ChatBox_Ok.SetActive(false);
        m_gPanel_ChatBox_No.SetActive(false);

        m_nInteractionNumber = n;
        m_TMP_ChatBox_Content_ConversationName.text = m_nt_NPC.m_cl_Conversation[n].m_sConversation_Title;
        //GUIManager_Total.Instance.m_GUI_Log.UpdateLog(m_nt_NPC.m_cl_Conversation[n].m_sConversation_Title);

        //m_tm_ChatBox.text = m_nt_NPC.m_cl_Conversation[m_nInteractionNumber].m_sl_Conversation_Content[m_nInteractionProgressNumber];
        Conversation_Print_Text(m_nt_NPC.m_cl_Conversation[m_nInteractionNumber].m_sl_Conversation_Context[m_nInteractionProgressNumber]);

        Conversation_Set_Btn_Next();
        Conversation_Set_Btn_Before();
        Conversation_Set_Btn_Exit();
        Conversation_Check();

        m_gPanel_ChatBox.transform.SetAsLastSibling();
    }
    // Next, Before 버튼 Display 설정 - Fix 해야함
    public void Conversation_Check()
    {
        // 대사 스크립트 나오고있을때는 넘기기 불가.
        //if (m_c_Quest_ProcessPrint_Text == null)
        m_gPanel_ChatBox.transform.SetAsLastSibling();

        {
            m_gPanel_ChatBox_Next.GetComponent<ChatBox_ContentButton>().UnDisplay_Chat_Selected();
            m_gPanel_ChatBox_Before.GetComponent<ChatBox_ContentButton>().UnDisplay_Chat_Selected();
            m_gPanel_ChatBox_Exit_Flow.GetComponent<ChatBox_ContentButton>().UnDisplay_Chat_Selected();
            m_bChatbox_Btn_State = true;
        }

        if (m_nt_NPC.m_cl_Conversation[m_nInteractionNumber].m_sl_Conversation_Context.Count - 1 > m_nInteractionProgressNumber)
        {
            m_gPanel_ChatBox_Next.SetActive(true);
        }
        else
            m_gPanel_ChatBox_Next.SetActive(false);

        if (0 < m_nInteractionProgressNumber)
        {
            m_gPanel_ChatBox_Before.SetActive(true);
        }
        else
            m_gPanel_ChatBox_Before.SetActive(false);

        if (m_nt_NPC.m_cl_Conversation[m_nInteractionNumber].m_sl_Conversation_Context.Count - 1 == m_nInteractionProgressNumber)
        {
            m_gPanel_ChatBox_Exit_Flow.SetActive(true);
        }
        else
            m_gPanel_ChatBox_Exit_Flow.SetActive(false);
    }
    // 대화창 다음으로, 이전으로 기능
    public void Conversation_Next()
    {
        m_gPanel_ChatBox.transform.SetAsLastSibling();

        if (m_c_Conversation_ProcessPrint_Text == null)
        {
            m_nInteractionProgressNumber++;
            //m_tm_ChatBox.text = m_nt_NPC.m_cl_Conversation[m_nInteractionNumber].m_sl_Conversation_Content[m_nInteractionProgressNumber];
            Conversation_Print_Text(m_nt_NPC.m_cl_Conversation[m_nInteractionNumber].m_sl_Conversation_Context[m_nInteractionProgressNumber]);
            Conversation_Check();
        }
        else
        {
            Skip_CQ_Script();
        }
    }
    public void Conversation_Before()
    {
        m_gPanel_ChatBox.transform.SetAsLastSibling();

        if (m_c_Conversation_ProcessPrint_Text == null)
        {
            m_nInteractionProgressNumber--;
            //m_tm_ChatBox.text = m_nt_NPC.m_cl_Conversation[m_nInteractionNumber].m_sl_Conversation_Content[m_nInteractionProgressNumber];
            Conversation_Print_Text(m_nt_NPC.m_cl_Conversation[m_nInteractionNumber].m_sl_Conversation_Context[m_nInteractionProgressNumber]);
            Conversation_Check();
        }
        else
        {
            Skip_CQ_Script();
        }
    }

    // UI 에 한글자씩 출력하기.(타이핑 효과?)
    public void Conversation_Print_Text(string str)
    {
        m_sCQBuffer = str;
        m_c_Conversation_ProcessPrint_Text = StartCoroutine(Conversation_ProcessPrint_Text(str));
        m_gPanel_ChatBox_Content_SelectBox.SetActive(false);
    }
    Coroutine m_c_Conversation_ProcessPrint_Text = null;
    IEnumerator Conversation_ProcessPrint_Text(string str)
    {
        int str_count = str.Length;

        for (int i = 0; i < str_count + 1; i++)
        {
            yield return new WaitForSeconds(0.01f);
            m_TMP_ChatBox_Content_ConversationBox.text = str.Substring(0, i);
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    m_TMP_ChatBox_Content_ConversationBox.text = str;
            //    break;
            //}
        }
        m_sCQBuffer = string.Empty;
        if (m_c_Conversation_ProcessPrint_Text != null)
            m_c_Conversation_ProcessPrint_Text = null;

        Display_Select_Button();
    }

    // 퀘스트 관련 상호작용
    //
    //
    public void Quest_Set_Btn_Next(E_QUEST_TYPE qt)
    {
        m_BTN_ChatBox_Next.GetComponent<Button>().onClick.AddListener(delegate { Quest_Next(qt); });
    }
    public void Quest_Set_Btn_Before(E_QUEST_TYPE qt)
    {
        m_BTN_ChatBox_Before.GetComponent<Button>().onClick.AddListener(delegate { Quest_Before(qt); });
    }
    public void Quest_Set_Btn_Ok(E_QUEST_TYPE qt)
    {
        if (m_c_Quest_ProcessPrint_Text == null)
        {
            switch (qt)
            {
                case E_QUEST_TYPE.KILL_MONSTER:
                    {
                        Player_Total.Instance.AddQuest(m_nt_NPC.m_ql_QuestList_KILL_MONSTER[m_nInteractionNumber]);
                        Quest_Print_Text(m_nt_NPC.m_ql_QuestList_KILL_MONSTER[m_nInteractionNumber].m_sl_QuestOk_Context[0]);
                    }
                    break;
                case E_QUEST_TYPE.KILL_TYPE:
                    {
                        Player_Total.Instance.AddQuest(m_nt_NPC.m_ql_QuestList_KILL_TYPE[m_nInteractionNumber]);
                        Quest_Print_Text(m_nt_NPC.m_ql_QuestList_KILL_TYPE[m_nInteractionNumber].m_sl_QuestOk_Context[0]);
                    }
                    break;
                case E_QUEST_TYPE.GOAWAY_MONSTER:
                    {
                        Player_Total.Instance.AddQuest(m_nt_NPC.m_ql_QuestList_GOAWAY_MONSTER[m_nInteractionNumber]);
                        Quest_Print_Text(m_nt_NPC.m_ql_QuestList_GOAWAY_MONSTER[m_nInteractionNumber].m_sl_QuestOk_Context[0]);
                    }
                    break;
                case E_QUEST_TYPE.GOAWAY_TYPE:
                    {
                        Player_Total.Instance.AddQuest(m_nt_NPC.m_ql_QuestList_GOAWAY_TYPE[m_nInteractionNumber]);
                        Quest_Print_Text(m_nt_NPC.m_ql_QuestList_GOAWAY_TYPE[m_nInteractionNumber].m_sl_QuestOk_Context[0]);
                    }
                    break;
                case E_QUEST_TYPE.COLLECT:
                    {
                        Player_Total.Instance.AddQuest(m_nt_NPC.m_ql_QuestList_COLLECT[m_nInteractionNumber]);
                        Quest_Print_Text(m_nt_NPC.m_ql_QuestList_COLLECT[m_nInteractionNumber].m_sl_QuestOk_Context[0]);
                    }
                    break;
                case E_QUEST_TYPE.CONVERSATION:
                    {
                        Player_Total.Instance.AddQuest(m_nt_NPC.m_ql_QuestList_CONVERSATION[m_nInteractionNumber]);
                        Quest_Print_Text(m_nt_NPC.m_ql_QuestList_CONVERSATION[m_nInteractionNumber].m_sl_QuestOk_Context[0]);
                        // 단순 대화로 퀘스트 클리어됨.
                        m_nt_NPC.m_ql_QuestList_CONVERSATION[m_nInteractionNumber].m_bCondition = true;
                        //GUIManager_Total.Instance.Display_GUI_QuestStateInfo(m_nt_NPC.m_ql_QuestList_CONVERSATION[m_nInteractionNumber]);
                    }
                    break;
                case E_QUEST_TYPE.ROLL:
                    {
                        Player_Total.Instance.AddQuest(m_nt_NPC.m_ql_QuestList_ROLL[m_nInteractionNumber]);
                        Quest_Print_Text(m_nt_NPC.m_ql_QuestList_ROLL[m_nInteractionNumber].m_sl_QuestOk_Context[0]);
                    }
                    break;
                case E_QUEST_TYPE.ELIMINATE_MONSTER:
                    {
                        Player_Total.Instance.AddQuest(m_nt_NPC.m_ql_QuestList_ELIMINATE_MONSTER[m_nInteractionNumber]);
                        Quest_Print_Text(m_nt_NPC.m_ql_QuestList_ELIMINATE_MONSTER[m_nInteractionNumber].m_sl_QuestOk_Context[0]);
                    }
                    break;
                case E_QUEST_TYPE.ELIMINATE_TYPE:
                    {
                        Player_Total.Instance.AddQuest(m_nt_NPC.m_ql_QuestList_ELIMINATE_TYPE[m_nInteractionNumber]);
                        Quest_Print_Text(m_nt_NPC.m_ql_QuestList_ELIMINATE_TYPE[m_nInteractionNumber].m_sl_QuestOk_Context[0]);
                    }
                    break;
            }

            m_gPanel_ChatBox_Ok.SetActive(false);
            m_gPanel_ChatBox_No.SetActive(false);
            m_gPanel_ChatBox_Exit_Flow.SetActive(true);
        }
        else
        {
            Skip_CQ_Script();
        }
    }
    public void Quest_Set_Btn_No(E_QUEST_TYPE qt)
    {
        if (m_c_Quest_ProcessPrint_Text == null)
        {
            //GUIManager_Total.Instance.UpdateLog("퀘스트 거절");
            if (m_c_Conversation_ProcessPrint_Text != null)
            {
                StopCoroutine(m_c_Conversation_ProcessPrint_Text);
            }
            if (m_c_Quest_ProcessPrint_Text != null)
            {
                StopCoroutine(m_c_Quest_ProcessPrint_Text);
            }

            switch (qt)
            {
                case E_QUEST_TYPE.KILL_MONSTER:
                    {
                        Quest_Print_Text(m_nt_NPC.m_ql_QuestList_KILL_MONSTER[m_nInteractionNumber].m_sl_QuestNo_Context[0]);
                    }
                    break;
                case E_QUEST_TYPE.KILL_TYPE:
                    {
                        Quest_Print_Text(m_nt_NPC.m_ql_QuestList_KILL_TYPE[m_nInteractionNumber].m_sl_QuestNo_Context[0]);
                    }
                    break;
                case E_QUEST_TYPE.GOAWAY_MONSTER:
                    {
                        Quest_Print_Text(m_nt_NPC.m_ql_QuestList_GOAWAY_MONSTER[m_nInteractionNumber].m_sl_QuestNo_Context[0]);
                    }
                    break;
                case E_QUEST_TYPE.GOAWAY_TYPE:
                    {
                        Quest_Print_Text(m_nt_NPC.m_ql_QuestList_GOAWAY_TYPE[m_nInteractionNumber].m_sl_QuestNo_Context[0]);
                    }
                    break;
                case E_QUEST_TYPE.COLLECT:
                    {
                        Quest_Print_Text(m_nt_NPC.m_ql_QuestList_COLLECT[m_nInteractionNumber].m_sl_QuestNo_Context[0]);
                    }
                    break;
                case E_QUEST_TYPE.CONVERSATION:
                    {
                        Quest_Print_Text(m_nt_NPC.m_ql_QuestList_CONVERSATION[m_nInteractionNumber].m_sl_QuestNo_Context[0]);
                    }
                    break;
                case E_QUEST_TYPE.ROLL:
                    {
                        Quest_Print_Text(m_nt_NPC.m_ql_QuestList_ROLL[m_nInteractionNumber].m_sl_QuestNo_Context[0]);
                    }
                    break;
                case E_QUEST_TYPE.ELIMINATE_MONSTER:
                    {
                        Quest_Print_Text(m_nt_NPC.m_ql_QuestList_ELIMINATE_MONSTER[m_nInteractionNumber].m_sl_QuestNo_Context[0]);
                    }
                    break;
                case E_QUEST_TYPE.ELIMINATE_TYPE:
                    {
                        Quest_Print_Text(m_nt_NPC.m_ql_QuestList_ELIMINATE_TYPE[m_nInteractionNumber].m_sl_QuestNo_Context[0]);
                    }
                    break;
            }

            m_gPanel_ChatBox_Ok.SetActive(false);
            m_gPanel_ChatBox_No.SetActive(false);
            m_gPanel_ChatBox_Exit_Flow.SetActive(true);
        }
        else
        {
            Skip_CQ_Script();
        }
    }
    public void Quest_Set_Btn_Exit(E_QUEST_TYPE qt)
    {
        m_BTN_ChatBox_Exit_Flow.GetComponent<Button>().onClick.RemoveAllListeners();
        m_BTN_ChatBox_Exit_Flow.GetComponent<Button>().onClick.AddListener(delegate { Exit(qt); });
    }
    public void Quest_Set_Btn_Reward(E_QUEST_TYPE qt)
    {
        m_BTN_ChatBox_Reward.onClick.RemoveAllListeners();
        m_BTN_ChatBox_Reward.onClick.AddListener(delegate { Quest_Reward(qt); });

        m_gPanel_ChatBox_Reward.SetActive(false);
    }

    private void Quest_Start(E_QUEST_TYPE qt, int n)
    {
        m_bConversation = false;
        m_bQuest = true;

        m_bQuestClear = false;
        m_nInteractionNumber = 0;
        m_nInteractionProgressNumber = 0;

        m_gPanel_ChatBox_Content_SelectBox.SetActive(false);
        m_gPanel_ChatBox_BTN.SetActive(true);
        m_gPanel_ChatBox_Next.SetActive(true);
        m_gPanel_ChatBox_Before.SetActive(true);
        m_gPanel_ChatBox_Exit_Flow.SetActive(false);
        m_gPanel_ChatBox_Exit_Free.SetActive(true);
        m_gPanel_ChatBox_Ok.SetActive(false);
        m_gPanel_ChatBox_No.SetActive(false);
        m_gPanel_ChatBox_Reward.SetActive(false);

        m_BTN_ChatBox_No.GetComponent<Button>().onClick.AddListener(delegate { Quest_Set_Btn_No(qt); });
        m_BTN_ChatBox_Ok.GetComponent<Button>().onClick.AddListener(delegate { Quest_Set_Btn_Ok(qt); });
        Quest_Set_Btn_Exit(qt);

        m_eQuestType = qt;

        m_gPanel_ChatBox.transform.SetAsLastSibling();

        m_nInteractionNumber = n;

        switch (qt)
        {
            case E_QUEST_TYPE.KILL_MONSTER:
                {
                    Quest_Start_C(m_nt_NPC.m_ql_QuestList_KILL_MONSTER[m_nInteractionNumber]);
                } 
                break;
            case E_QUEST_TYPE.KILL_TYPE:
                {
                    Quest_Start_C(m_nt_NPC.m_ql_QuestList_KILL_TYPE[m_nInteractionNumber]);
                }
                break;
            case E_QUEST_TYPE.GOAWAY_MONSTER:
                {
                    Quest_Start_C(m_nt_NPC.m_ql_QuestList_GOAWAY_MONSTER[m_nInteractionNumber]);
                }
                break;
            case E_QUEST_TYPE.GOAWAY_TYPE:
                {
                    Quest_Start_C(m_nt_NPC.m_ql_QuestList_GOAWAY_TYPE[m_nInteractionNumber]);
                }
                break;
            case E_QUEST_TYPE.COLLECT:
                {
                    Quest_Start_C(m_nt_NPC.m_ql_QuestList_COLLECT[m_nInteractionNumber]);
                }
                break;
            case E_QUEST_TYPE.CONVERSATION:
                {
                    Quest_Start_C(m_nt_NPC.m_ql_QuestList_CONVERSATION[m_nInteractionNumber]);
                }
                break;
            case E_QUEST_TYPE.ROLL:
                {
                    Quest_Start_C(m_nt_NPC.m_ql_QuestList_ROLL[m_nInteractionNumber]);
                }
                break;
            case E_QUEST_TYPE.ELIMINATE_MONSTER:
                {
                    Quest_Start_C(m_nt_NPC.m_ql_QuestList_ELIMINATE_MONSTER[m_nInteractionNumber]);
                }
                break;
            case E_QUEST_TYPE.ELIMINATE_TYPE:
                {
                    Quest_Start_C(m_nt_NPC.m_ql_QuestList_ELIMINATE_TYPE[m_nInteractionNumber]);
                }
                break;
        }
    }
    private void Quest_Start_C(Quest quest)
    {
        m_TMP_ChatBox_Content_ConversationName.text = quest.m_sQuest_Title;

        if (quest.m_bProcess == false)
        {
            Quest_Print_Text(quest.m_sl_QuestDescription_Context[m_nInteractionProgressNumber]);

            Quest_Set_Btn_Next(quest.m_eQuestType);
            Quest_Set_Btn_Before(quest.m_eQuestType);
            Quest_Check(quest.m_eQuestType);
        }
        else
        {
            if (quest.m_nNPC_Clear == m_nt_NPC.m_nNPCCode)
            {
                if (quest.m_bCondition == false)
                {
                    Quest_Print_Text(quest.m_sl_QuestProgress_Context[m_nInteractionProgressNumber]);
                    m_gPanel_ChatBox_BTN.SetActive(true);
                    m_gPanel_ChatBox_Next.SetActive(false);
                    m_gPanel_ChatBox_Before.SetActive(false);
                    m_gPanel_ChatBox_Exit_Flow.SetActive(true);
                    m_gPanel_ChatBox_Exit_Free.SetActive(true);
                    m_gPanel_ChatBox_Ok.SetActive(false);
                    m_gPanel_ChatBox_No.SetActive(false);
                }
                else
                {
                    Quest_Print_Text(quest.m_sl_QuestClear_Context[m_nInteractionProgressNumber]);
                    quest.m_bClear = true;
                    quest.m_bProcess = false;
                    m_gPanel_ChatBox_BTN.SetActive(true);
                    m_gPanel_ChatBox_Next.SetActive(false);
                    m_gPanel_ChatBox_Before.SetActive(false);
                    m_gPanel_ChatBox_Exit_Flow.SetActive(true);
                    m_gPanel_ChatBox_Exit_Free.SetActive(false);
                    m_gPanel_ChatBox_Ok.SetActive(false);
                    m_gPanel_ChatBox_No.SetActive(false);

                    m_bQuestClear = true;
                }
            }
            else
            {
                Quest_Print_Text(quest.m_sl_QuestProgress_Context[m_nInteractionProgressNumber]);
                m_gPanel_ChatBox_BTN.SetActive(true);
                m_gPanel_ChatBox_Next.SetActive(false);
                m_gPanel_ChatBox_Before.SetActive(false);
                m_gPanel_ChatBox_Exit_Flow.SetActive(true);
                m_gPanel_ChatBox_Exit_Free.SetActive(true);
                m_gPanel_ChatBox_Ok.SetActive(false);
                m_gPanel_ChatBox_No.SetActive(false);
            }

            Quest_Reward(quest.m_eQuestType);
        }

        Quest_Set_Btn_Reward(quest.m_eQuestType);
    }
    // Next, Before 버튼 Display 설정
    public void Quest_Check(E_QUEST_TYPE qt)
    {
        //if (m_c_Quest_ProcessPrint_Text == null)
        {
            m_gPanel_ChatBox.transform.SetAsLastSibling();

            {
                m_gPanel_ChatBox_Next.GetComponent<ChatBox_ContentButton>().UnDisplay_Chat_Selected();
                m_gPanel_ChatBox_Before.GetComponent<ChatBox_ContentButton>().UnDisplay_Chat_Selected();
                m_gPanel_ChatBox_Exit_Flow.GetComponent<ChatBox_ContentButton>().UnDisplay_Chat_Selected();
                m_gPanel_ChatBox_Ok.GetComponent<ChatBox_ContentButton>().UnDisplay_Chat_Selected();
                m_gPanel_ChatBox_No.GetComponent<ChatBox_ContentButton>().UnDisplay_Chat_Selected();
                m_bChatbox_Btn_State = true;
            }

            switch (qt)
            {
                case E_QUEST_TYPE.KILL_MONSTER:
                    {
                        Quest_Check_C(m_nt_NPC.m_ql_QuestList_KILL_MONSTER[m_nInteractionNumber]);
                    }
                    break;
                case E_QUEST_TYPE.KILL_TYPE:
                    {
                        Quest_Check_C(m_nt_NPC.m_ql_QuestList_KILL_TYPE[m_nInteractionNumber]);
                    }
                    break;
                case E_QUEST_TYPE.GOAWAY_MONSTER:
                    {
                        Quest_Check_C(m_nt_NPC.m_ql_QuestList_GOAWAY_MONSTER[m_nInteractionNumber]);
                    }
                    break;
                case E_QUEST_TYPE.GOAWAY_TYPE:
                    {
                        Quest_Check_C(m_nt_NPC.m_ql_QuestList_GOAWAY_TYPE[m_nInteractionNumber]);
                    }
                    break;
                case E_QUEST_TYPE.COLLECT:
                    {
                        Quest_Check_C(m_nt_NPC.m_ql_QuestList_COLLECT[m_nInteractionNumber]);
                    }
                    break;
                case E_QUEST_TYPE.CONVERSATION:
                    {
                        Quest_Check_C(m_nt_NPC.m_ql_QuestList_CONVERSATION[m_nInteractionNumber]);
                    }
                    break;
                case E_QUEST_TYPE.ROLL:
                    {
                        Quest_Check_C(m_nt_NPC.m_ql_QuestList_ROLL[m_nInteractionNumber]);
                    }
                    break;
                case E_QUEST_TYPE.ELIMINATE_MONSTER:
                    {
                        Quest_Check_C(m_nt_NPC.m_ql_QuestList_ELIMINATE_MONSTER[m_nInteractionNumber]);
                    }
                    break;
                case E_QUEST_TYPE.ELIMINATE_TYPE:
                    {
                        Quest_Check_C(m_nt_NPC.m_ql_QuestList_ELIMINATE_TYPE[m_nInteractionNumber]);
                    }
                    break;
            }
        }
    }
    private void Quest_Check_C(Quest quest)
    {
        if (quest.m_sl_QuestDescription_Context.Count - 1 > m_nInteractionProgressNumber)
        {
            m_gPanel_ChatBox_Next.SetActive(true);
        }
        else
            m_gPanel_ChatBox_Next.SetActive(false);

        if (0 < m_nInteractionProgressNumber && quest.m_sl_QuestDescription_Context.Count - 1 != m_nInteractionProgressNumber)
        {
            m_gPanel_ChatBox_Before.SetActive(true);
        }
        else
            m_gPanel_ChatBox_Before.SetActive(false);

        if (quest.m_sl_QuestDescription_Context.Count - 1 == m_nInteractionProgressNumber)
        {
            m_gPanel_ChatBox_Ok.SetActive(true);
            m_gPanel_ChatBox_No.SetActive(true);

            Quest_Reward(quest.m_eQuestType);
        }
        else
        {
            m_gPanel_ChatBox_Ok.SetActive(false);
            m_gPanel_ChatBox_No.SetActive(false);
        }
    }
    // 대화창 다음으로, 이전으로 기능
    public void Quest_Next(E_QUEST_TYPE qt)
    {
        m_gPanel_ChatBox.transform.SetAsLastSibling();

        if (m_c_Quest_ProcessPrint_Text == null)
        {
            //Debug.Log("GUI NEXT");
            m_nInteractionProgressNumber++;
            Quest_Check(qt);
            switch (qt)
            {
                case E_QUEST_TYPE.KILL_MONSTER:
                    {
                        Quest_Print_Text(m_nt_NPC.m_ql_QuestList_KILL_MONSTER[m_nInteractionNumber].m_sl_QuestDescription_Context[m_nInteractionProgressNumber]);
                    }
                    break;
                case E_QUEST_TYPE.KILL_TYPE:
                    {
                        Quest_Print_Text(m_nt_NPC.m_ql_QuestList_KILL_TYPE[m_nInteractionNumber].m_sl_QuestDescription_Context[m_nInteractionProgressNumber]);
                    }
                    break;
                case E_QUEST_TYPE.GOAWAY_MONSTER:
                    {
                         Quest_Print_Text(m_nt_NPC.m_ql_QuestList_GOAWAY_MONSTER[m_nInteractionNumber].m_sl_QuestDescription_Context[m_nInteractionProgressNumber]);
                    }
                    break;
                case E_QUEST_TYPE.GOAWAY_TYPE:
                    {
                        Quest_Print_Text(m_nt_NPC.m_ql_QuestList_GOAWAY_TYPE[m_nInteractionNumber].m_sl_QuestDescription_Context[m_nInteractionProgressNumber]);
                    }
                    break;
                case E_QUEST_TYPE.COLLECT:
                    {
                        Quest_Print_Text(m_nt_NPC.m_ql_QuestList_COLLECT[m_nInteractionNumber].m_sl_QuestDescription_Context[m_nInteractionProgressNumber]);
                    }
                    break;
                case E_QUEST_TYPE.CONVERSATION:
                    {
                        Quest_Print_Text(m_nt_NPC.m_ql_QuestList_CONVERSATION[m_nInteractionNumber].m_sl_QuestDescription_Context[m_nInteractionProgressNumber]);
                    }
                    break;
                case E_QUEST_TYPE.ROLL:
                    {
                        Quest_Print_Text(m_nt_NPC.m_ql_QuestList_ROLL[m_nInteractionNumber].m_sl_QuestDescription_Context[m_nInteractionProgressNumber]);
                    }
                    break;
                case E_QUEST_TYPE.ELIMINATE_MONSTER:
                    {
                        Quest_Print_Text(m_nt_NPC.m_ql_QuestList_ELIMINATE_MONSTER[m_nInteractionNumber].m_sl_QuestDescription_Context[m_nInteractionProgressNumber]);
                    }
                    break;
                case E_QUEST_TYPE.ELIMINATE_TYPE:
                    {
                        Quest_Print_Text(m_nt_NPC.m_ql_QuestList_ELIMINATE_TYPE[m_nInteractionNumber].m_sl_QuestDescription_Context[m_nInteractionProgressNumber]);
                    }
                    break;
            }
        }
        else
        {
            Skip_CQ_Script();
        }
    }
    public void Quest_Before(E_QUEST_TYPE qt)
    {
        m_gPanel_ChatBox.transform.SetAsLastSibling();

        if (m_c_Quest_ProcessPrint_Text == null)
        {
            //Debug.Log("GUI BEFORE");
            m_nInteractionProgressNumber--;
            Quest_Check(qt);

            switch (qt)
            {
                case E_QUEST_TYPE.KILL_MONSTER:
                    {
                        Quest_Print_Text(m_nt_NPC.m_ql_QuestList_KILL_MONSTER[m_nInteractionNumber].m_sl_QuestDescription_Context[m_nInteractionProgressNumber]);
                    }
                    break;
                case E_QUEST_TYPE.KILL_TYPE:
                    {
                        Quest_Print_Text(m_nt_NPC.m_ql_QuestList_KILL_TYPE[m_nInteractionNumber].m_sl_QuestDescription_Context[m_nInteractionProgressNumber]);
                    }
                    break;
                case E_QUEST_TYPE.GOAWAY_MONSTER:
                    {
                        Quest_Print_Text(m_nt_NPC.m_ql_QuestList_GOAWAY_MONSTER[m_nInteractionNumber].m_sl_QuestDescription_Context[m_nInteractionProgressNumber]);
                    }
                    break;
                case E_QUEST_TYPE.GOAWAY_TYPE:
                    {
                        Quest_Print_Text(m_nt_NPC.m_ql_QuestList_GOAWAY_TYPE[m_nInteractionNumber].m_sl_QuestDescription_Context[m_nInteractionProgressNumber]);
                    }
                    break;
                case E_QUEST_TYPE.COLLECT:
                    {
                        Quest_Print_Text(m_nt_NPC.m_ql_QuestList_COLLECT[m_nInteractionNumber].m_sl_QuestDescription_Context[m_nInteractionProgressNumber]);
                    }
                    break;
                case E_QUEST_TYPE.CONVERSATION:
                    {
                        Quest_Print_Text(m_nt_NPC.m_ql_QuestList_CONVERSATION[m_nInteractionNumber].m_sl_QuestDescription_Context[m_nInteractionProgressNumber]);
                    }
                    break;
                case E_QUEST_TYPE.ROLL:
                    {
                        Quest_Print_Text(m_nt_NPC.m_ql_QuestList_ROLL[m_nInteractionNumber].m_sl_QuestDescription_Context[m_nInteractionProgressNumber]);
                    }
                    break;
                case E_QUEST_TYPE.ELIMINATE_MONSTER:
                    {
                        Quest_Print_Text(m_nt_NPC.m_ql_QuestList_ELIMINATE_MONSTER[m_nInteractionNumber].m_sl_QuestDescription_Context[m_nInteractionProgressNumber]);
                    }
                    break;
                case E_QUEST_TYPE.ELIMINATE_TYPE:
                    {
                        Quest_Print_Text(m_nt_NPC.m_ql_QuestList_ELIMINATE_TYPE[m_nInteractionNumber].m_sl_QuestDescription_Context[m_nInteractionProgressNumber]);
                    }
                    break;
            }
        }
        else
        {
            Skip_CQ_Script();
        }
    }
    // 퀘스트 보상 확인.
    public void Quest_Reward(E_QUEST_TYPE qt)
    {
        //m_gPanel_ChatBox.transform.SetAsLastSibling();
        if (GUIManager_Total.Instance.m_GUI_QuestReward.m_gPanel_QuestReward.activeSelf == false)
        {
            switch (qt)
            {
                case E_QUEST_TYPE.KILL_MONSTER:
                    {
                        Quest_Reward_C(m_nt_NPC.m_ql_QuestList_KILL_MONSTER[m_nInteractionNumber]);
                    }
                    break;
                case E_QUEST_TYPE.KILL_TYPE:
                    {
                        Quest_Reward_C(m_nt_NPC.m_ql_QuestList_KILL_TYPE[m_nInteractionNumber]);
                    }
                    break;
                case E_QUEST_TYPE.GOAWAY_MONSTER:
                    {
                        Quest_Reward_C(m_nt_NPC.m_ql_QuestList_GOAWAY_MONSTER[m_nInteractionNumber]);
                    }
                    break;
                case E_QUEST_TYPE.GOAWAY_TYPE:
                    {
                        Quest_Reward_C(m_nt_NPC.m_ql_QuestList_GOAWAY_TYPE[m_nInteractionNumber]);
                    }
                    break;
                case E_QUEST_TYPE.COLLECT:
                    {
                        Quest_Reward_C(m_nt_NPC.m_ql_QuestList_COLLECT[m_nInteractionNumber]);
                    }
                    break;
                case E_QUEST_TYPE.CONVERSATION:
                    {
                        Quest_Reward_C(m_nt_NPC.m_ql_QuestList_CONVERSATION[m_nInteractionNumber]);
                    }
                    break;
                case E_QUEST_TYPE.ROLL:
                    {
                        Quest_Reward_C(m_nt_NPC.m_ql_QuestList_ROLL[m_nInteractionNumber]);
                    }
                    break;
                case E_QUEST_TYPE.ELIMINATE_MONSTER:
                    {
                        Quest_Reward_C(m_nt_NPC.m_ql_QuestList_ELIMINATE_MONSTER[m_nInteractionNumber]);
                    }
                    break;
                case E_QUEST_TYPE.ELIMINATE_TYPE:
                    {
                        Quest_Reward_C(m_nt_NPC.m_ql_QuestList_ELIMINATE_TYPE[m_nInteractionNumber]);
                    }
                    break;
            }
        }
        else
        {
            GUIManager_Total.Instance.m_GUI_QuestReward.Press_Btn_Exit();
        }
    }
    private void Quest_Reward_C(Quest quest)
    {
        if (quest.m_bQuest_Information_Process_Hide == false)
            GUIManager_Total.Instance.Display_GUI_QuestReward(quest);
        else
        {
            if (quest.m_nNPC_Clear == m_nt_NPC.m_nNPCCode)
            {
                GUIManager_Total.Instance.Display_GUI_QuestReward(quest, 1);
            }
            else
            {
                GUIManager_Total.Instance.Display_GUI_QuestReward(quest, 2);
            }
        }
    }

    // UI 에 한글자씩 출력하기.(타이핑 효과?)
    public void Quest_Print_Text(string str)
    {
        m_sCQBuffer = str;
        m_c_Quest_ProcessPrint_Text = StartCoroutine(Quest_ProcessPrint_Text(str));
        m_gPanel_ChatBox_Content_SelectBox.SetActive(false);
    }
    Coroutine m_c_Quest_ProcessPrint_Text = null;
    IEnumerator Quest_ProcessPrint_Text(string str)
    {
        int str_count = str.Length;

        for (int i = 0; i < str_count + 1; i++)
        {
            yield return new WaitForSeconds(0.01f);
            m_TMP_ChatBox_Content_ConversationBox.text = str.Substring(0, i);
        }
        m_sCQBuffer = string.Empty;
        if (m_c_Quest_ProcessPrint_Text != null)
            m_c_Quest_ProcessPrint_Text = null;

        Display_Select_Button();
    }
}
