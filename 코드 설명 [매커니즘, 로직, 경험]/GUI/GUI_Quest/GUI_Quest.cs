using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUI_Quest : MonoBehaviour
{
    enum E_MAP { MAP_TUTORIAL, MAP_CHAPTER1 }
    enum E_QUEST_STATE { RECOMMEND, PROGRESS, COMPLETE }
    enum E_QUEST_LOADMAP { ON, OFF} 
    enum E_QUEST_INFO { CONDITION, CONTENT, REWARD }

    // Quest UI.
    [SerializeField] GameObject m_gPanel_Quest;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_Quest_Header;
    [SerializeField] Button m_BTN_Quest_Exit;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_Quest_Content_Left;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_Quest_Content_Left_UpBar;
    [SerializeField] GameObject m_gBTN_Quest_Content_Left_UpBar_Recommend;
    [SerializeField] Button m_BTN_Quest_Content_Left_UpBar_Recommend;
    [SerializeField] GameObject m_gBTN_Quest_Content_Left_UpBar_Progress;
    [SerializeField] Button m_BTN_Quest_Content_Left_UpBar_Progress;
    [SerializeField] GameObject m_gBTN_Quest_Content_Left_UpBar_Complete;
    [SerializeField] Button m_BTN_Quest_Content_Left_UpBar_Complete;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_Quest_Content_Left_Content;
    [SerializeField] GameObject m_gSV_Quest_Content_Left_Content_QuestList;
    [SerializeField] ScrollRect m_ScrollRect_Quest_Content_Left_Content_QuestList;
    [SerializeField] RectTransform m_RectTransform_Content_Left_Content_QuestList;
    [SerializeField] GameObject m_gViewport_Quest_Content_Left_Content_QuestList;
    [SerializeField] GameObject m_gContent_Quest_Content_Left_Content_QuestList_Recommend;
    [SerializeField] GameObject m_gContent_Quest_Content_Left_Content_QuestList_Progress;
    [SerializeField] GameObject m_gContent_Quest_Content_Left_Content_QuestList_Complete;
    [SerializeField] Scrollbar m_Scrollbar_Quest_Content_Left_Content_QuestList;
    [Space(20)]
    [SerializeField] GameObject m_gSV_Quest_Content_Left_Content_Loadmap;
    [SerializeField] ScrollRect m_ScrollRect_Quest_Content_Left_Content_Loadmap;
    [SerializeField] GameObject m_gViewport_Quest_Content_Left_Content_Loadmap;
    [SerializeField] GameObject m_gContent_Quest_Content_Left_Content_Loadmap_Map_Tutorial;
    [SerializeField] GameObject m_gContent_Quest_Content_Left_Content_Loadmap_Map_Chapter1;
    [SerializeField] GameObject m_gPanel_Quest_Content_Left_Content_Loadmap_Map_Blank;
    [SerializeField] Scrollbar m_Scrollbar_Quest_Content_Left_Content_Loadmap_Horizontal;
    [SerializeField] Scrollbar m_Scrollbar_Quest_Content_Left_Content_Loadmap_Vertical;
    public static List<QuestLoadmap_questpanel> m_csList_Loadmap;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_Quest_Content_Left_Content_Loadmap_DownBar;
    [SerializeField] GameObject m_gBTN_Quest_Content_Left_Content_Loadmap_DownBar_PM;
    [SerializeField] TextMeshProUGUI m_TMP_Quest_Content_Left_Content_Loadmap_DownBar;
    [SerializeField] Button m_BTN_Quest_Content_Left_Content_Loadmap_DownBar_PM;
    [SerializeField] TextMeshProUGUI m_TMP_Quest_Content_Left_Content_Loadmap_DownBar_PM;
    [SerializeField] GameObject m_gBTN_Quest_Content_Left_Content_Loadmap_DownBar_MR;
    [SerializeField] Button m_BTN_Quest_Content_Left_Content_Loadmap_DownBar_MR;
    [SerializeField] GameObject m_gBTN_Quest_Content_Left_Content_Loadmap_DownBar_ML;
    [SerializeField] Button m_BTN_Quest_Content_Left_Content_Loadmap_DownBar_ML;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_Quest_Content_Right;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_Quest_Content_Right_UpBar;
    [SerializeField] TextMeshProUGUI m_TMP_Quest_Content_Right_UpBar_Name;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_Quest_Content_Right_Content;
    [SerializeField] GameObject m_gPanel_Quest_Content_Right_Content_Blank;
    [SerializeField] GameObject m_gPanel_Quest_Content_Right_Content_QuestInfo;
    [SerializeField] GameObject m_gPanel_Quest_Content_Right_Content_QuestInfo_NPC;
    [SerializeField] GameObject m_gIMG_Quest_Content_Right_Content_QuestInfo_NPC;
    [SerializeField] Image m_IMG_Quest_Content_Right_Content_QuestInfo_NPC;
    [SerializeField] GameObject m_gPanel_Quest_Content_Right_Content_QuestInfo_Info;
    [SerializeField] TextMeshProUGUI m_TMP_Quest_Content_Right_Content_QuestInfo_Info;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_Quest_Content_Right_Content_QuestDescription;
    [SerializeField] GameObject m_gPanel_Quest_Content_Right_Content_QuestDescription_UpBar;
    [SerializeField] Button m_BTN_Quest_Content_Right_Content_QuestDescription_UpBar_Condition;
    [SerializeField] Button m_BTN_Quest_Content_Right_Content_QuestDescription_UpBar_Content;
    [SerializeField] Button m_BTN_Quest_Content_Right_Content_QuestDescription_UpBar_Reward;
    [Space(20)]
    [SerializeField] GameObject m_gSV_Quest_Content_Right_Content_QuestDescription_Condition;
    [SerializeField] ScrollRect m_ScrollRect_Quest_Content_Right_Content_QuestDescription_Condition;
    [SerializeField] GameObject m_gViewport_Quest_Content_Right_Content_QuestDescription_Condition;
    [SerializeField] GameObject m_gContent_Quest_Content_Right_Content_QuestDescription_Condition;
    [SerializeField] Scrollbar m_Scrollbar_Quest_Content_Right_Content_QuestDescription_Condition;
    [Space(20)]
    [SerializeField] GameObject m_gSV_Quest_Content_Right_Content_QuestDescription_Content;
    [SerializeField] ScrollRect m_ScrollRect_Quest_Content_Right_Content_QuestDescription_Content;
    [SerializeField] GameObject m_gViewport_Quest_Content_Right_Content_QuestDescription_Content;
    [SerializeField] TextMeshProUGUI m_TMP_Quest_Content_Right_Content_QuestDescription_Content;
    [SerializeField] Scrollbar m_Scrollbar_Quest_Content_Right_Content_QuestDescription_Content;
    [Space(20)]
    [SerializeField] GameObject m_gSV_Quest_Content_Right_Content_QuestDescription_Reward;
    [SerializeField] ScrollRect m_ScrollRect_Quest_Content_Right_Content_QuestDescription_Reward;
    [SerializeField] GameObject m_gViewport_Quest_Content_Right_Content_QuestDescription_Reward;
    [SerializeField] GameObject m_gContent_Quest_Content_Right_Content_QuestDescription_Reward;
    [SerializeField] Scrollbar m_Scrollbar_Quest_Content_Right_Content_QuestDescription_Reward;
    [SerializeField] GameObject m_gPanel_Quest_Content_Right_Content_QuestDescription_Reward_Blank;

    [SerializeField] E_MAP m_eMap = E_MAP.MAP_TUTORIAL;
    [SerializeField] E_QUEST_STATE m_eQuest_State = E_QUEST_STATE.RECOMMEND;
    [SerializeField] E_QUEST_LOADMAP m_eQuest_Loadmap = E_QUEST_LOADMAP.ON;
    [SerializeField] E_QUEST_INFO m_eQuest_Info = E_QUEST_INFO.CONTENT;

    [Space(20)]
    public static List<GameObject> m_gList_Quest_Recommend;
    public static List<int> m_nList_Quest_Recommend;
    public static List<GameObject> m_gList_Quest_Process;
    public static List<int> m_nList_Quest_Process;
    public static List<GameObject> m_gList_Quest_Complete;
    public static List<int> m_nList_Quest_Complete;

    public static List<GameObject> m_gList_Quest_Info_Condition;
    public static List<GameObject> m_gList_Quest_Info_Reward;

    [SerializeField] int m_nQuest_Selected_Number;

    GameObject m_gQuestContent;

    public void InitialSet()
    {
        InitialSet_Object();
        InitialSet_Button();
        Btn_Press_Quest_Recommend();
        //m_gPanel_Quest.SetActive(false);
    }

    private void Update()
    {
        if (m_gPanel_Quest != null)
        {
            if (m_gPanel_Quest.activeSelf == true)
            {
                for (int i = 0; i < m_gList_Quest_Process.Count; i++)
                {
                    m_gList_Quest_Process[i].GetComponent<questpanel>().Effect_Flesh();
                }
            }
        }
    }

    // 초기 Object 불러오기.
    void InitialSet_Object()
    {
        m_gPanel_Quest = GameObject.Find("Canvas_GUI").gameObject.transform.Find("Panel_Quest").gameObject;

        m_gPanel_Quest_Header = m_gPanel_Quest.transform.Find("Panel_Quest_Header").gameObject;
        m_BTN_Quest_Exit = m_gPanel_Quest_Header.transform.Find("BTN_Quest_Exit").GetComponent<Button>();

        m_gPanel_Quest_Content_Left = m_gPanel_Quest.transform.Find("Panel_Quest_Content_Left").gameObject;
        m_gPanel_Quest_Content_Left_UpBar = m_gPanel_Quest_Content_Left.transform.Find("Panel_Quest_Content_Left_UpBar").gameObject;
        m_gBTN_Quest_Content_Left_UpBar_Recommend = m_gPanel_Quest_Content_Left_UpBar.transform.Find("BTN_Quest_Content_Left_UpBar_Recommend").gameObject;
        m_BTN_Quest_Content_Left_UpBar_Recommend = m_gBTN_Quest_Content_Left_UpBar_Recommend.GetComponent<Button>();
        m_gBTN_Quest_Content_Left_UpBar_Progress = m_gPanel_Quest_Content_Left_UpBar.transform.Find("BTN_Quest_Content_Left_UpBar_Progress").gameObject;
        m_BTN_Quest_Content_Left_UpBar_Progress = m_gBTN_Quest_Content_Left_UpBar_Progress.GetComponent<Button>();
        m_gBTN_Quest_Content_Left_UpBar_Complete = m_gPanel_Quest_Content_Left_UpBar.transform.Find("BTN_Quest_Content_Left_UpBar_Complete").gameObject;
        m_BTN_Quest_Content_Left_UpBar_Complete = m_gBTN_Quest_Content_Left_UpBar_Complete.GetComponent<Button>();

        m_gPanel_Quest_Content_Left_Content = m_gPanel_Quest_Content_Left.transform.Find("Panel_Quest_Content_Left_Content").gameObject;
        m_gSV_Quest_Content_Left_Content_QuestList = m_gPanel_Quest_Content_Left_Content.transform.Find("SV_Quest_Content_Left_Content_QuestList").gameObject;
        m_ScrollRect_Quest_Content_Left_Content_QuestList = m_gSV_Quest_Content_Left_Content_QuestList.GetComponent<ScrollRect>();
        m_RectTransform_Content_Left_Content_QuestList = m_gSV_Quest_Content_Left_Content_QuestList.GetComponent<RectTransform>();
        m_gViewport_Quest_Content_Left_Content_QuestList = m_gSV_Quest_Content_Left_Content_QuestList.transform.Find("Viewport_Quest_Content_Left_Content_QuestList").gameObject;
        m_gContent_Quest_Content_Left_Content_QuestList_Recommend = m_gViewport_Quest_Content_Left_Content_QuestList.transform.Find("Content_Quest_Content_Left_Content_QuestList_Recommend").gameObject;
        m_gContent_Quest_Content_Left_Content_QuestList_Progress = m_gViewport_Quest_Content_Left_Content_QuestList.transform.Find("Content_Quest_Content_Left_Content_QuestList_Progress").gameObject;
        m_gContent_Quest_Content_Left_Content_QuestList_Complete = m_gViewport_Quest_Content_Left_Content_QuestList.transform.Find("Content_Quest_Content_Left_Content_QuestList_Complete").gameObject;
        m_Scrollbar_Quest_Content_Left_Content_QuestList = m_gSV_Quest_Content_Left_Content_QuestList.transform.Find("Scrollbar_Quest_Content_Left_Content_QuestList").gameObject.GetComponent<Scrollbar>();

        m_gSV_Quest_Content_Left_Content_Loadmap = m_gPanel_Quest_Content_Left_Content.transform.Find("SV_Quest_Content_Left_Content_Loadmap").gameObject;
        m_ScrollRect_Quest_Content_Left_Content_Loadmap = m_gSV_Quest_Content_Left_Content_Loadmap.GetComponent<ScrollRect>();
        m_gViewport_Quest_Content_Left_Content_Loadmap = m_gSV_Quest_Content_Left_Content_Loadmap.transform.Find("Viewport_Quest_Content_Left_Content_Loadmap").gameObject;
        m_gContent_Quest_Content_Left_Content_Loadmap_Map_Tutorial = m_gViewport_Quest_Content_Left_Content_Loadmap.transform.Find("Content_Quest_Content_Left_Content_Loadmap_Map_Tutorial").gameObject;
        m_gContent_Quest_Content_Left_Content_Loadmap_Map_Chapter1 = m_gViewport_Quest_Content_Left_Content_Loadmap.transform.Find("Content_Quest_Content_Left_Content_Loadmap_Map_Chapter1").gameObject;
        m_gPanel_Quest_Content_Left_Content_Loadmap_Map_Blank = m_gViewport_Quest_Content_Left_Content_Loadmap.transform.Find("Panel_Quest_Content_Left_Content_Loadmap_Map_Blank").gameObject;

        m_Scrollbar_Quest_Content_Left_Content_Loadmap_Horizontal = m_gSV_Quest_Content_Left_Content_Loadmap.transform.Find("Scrollbar_Horizontal").gameObject.GetComponent<Scrollbar>();
        m_Scrollbar_Quest_Content_Left_Content_Loadmap_Vertical = m_gSV_Quest_Content_Left_Content_Loadmap.transform.Find("Scrollbar_Vertical").gameObject.GetComponent<Scrollbar>();

        m_csList_Loadmap = new List<QuestLoadmap_questpanel>();
        for (int i = 0; i < m_gContent_Quest_Content_Left_Content_Loadmap_Map_Tutorial.transform.childCount; i++)
        {
            if (m_gContent_Quest_Content_Left_Content_Loadmap_Map_Tutorial.transform.GetChild(i).gameObject.name != "AR_List")
                m_csList_Loadmap.Add(m_gContent_Quest_Content_Left_Content_Loadmap_Map_Tutorial.transform.GetChild(i).gameObject.GetComponent<QuestLoadmap_questpanel>());
        }
        for (int i = 0; i < m_gContent_Quest_Content_Left_Content_Loadmap_Map_Chapter1.transform.childCount; i++)
        {
            if (m_gContent_Quest_Content_Left_Content_Loadmap_Map_Chapter1.transform.GetChild(i).gameObject.name != "AR_List")
                m_csList_Loadmap.Add(m_gContent_Quest_Content_Left_Content_Loadmap_Map_Chapter1.transform.GetChild(i).gameObject.GetComponent<QuestLoadmap_questpanel>());
        }

        m_gPanel_Quest_Content_Left_Content_Loadmap_DownBar = m_gPanel_Quest_Content_Left_Content.transform.Find("Panel_Quest_Content_Left_Content_Loadmap_DownBar").gameObject;
        m_TMP_Quest_Content_Left_Content_Loadmap_DownBar = m_gPanel_Quest_Content_Left_Content_Loadmap_DownBar.transform.Find("TMP_Quest_Content_Left_Content_Loadmap_DownBar").gameObject.GetComponent<TextMeshProUGUI>();
        m_gBTN_Quest_Content_Left_Content_Loadmap_DownBar_PM = m_gPanel_Quest_Content_Left_Content_Loadmap_DownBar.transform.Find("BTN_Quest_Content_Left_Content_Loadmap_DownBar_PM").gameObject;
        m_BTN_Quest_Content_Left_Content_Loadmap_DownBar_PM = m_gBTN_Quest_Content_Left_Content_Loadmap_DownBar_PM.GetComponent<Button>();
        m_TMP_Quest_Content_Left_Content_Loadmap_DownBar_PM = m_gBTN_Quest_Content_Left_Content_Loadmap_DownBar_PM.transform.Find("TMP_Quest_Content_Left_Content_Loadmap_DownBar_PM").gameObject.GetComponent<TextMeshProUGUI>();
        m_gBTN_Quest_Content_Left_Content_Loadmap_DownBar_MR = m_gPanel_Quest_Content_Left_Content_Loadmap_DownBar.transform.Find("BTN_Quest_Content_Left_Content_Loadmap_DownBar_MR").gameObject;
        m_BTN_Quest_Content_Left_Content_Loadmap_DownBar_MR = m_gBTN_Quest_Content_Left_Content_Loadmap_DownBar_MR.GetComponent<Button>();
        m_gBTN_Quest_Content_Left_Content_Loadmap_DownBar_ML = m_gPanel_Quest_Content_Left_Content_Loadmap_DownBar.transform.Find("BTN_Quest_Content_Left_Content_Loadmap_DownBar_ML").gameObject;
        m_BTN_Quest_Content_Left_Content_Loadmap_DownBar_ML = m_gBTN_Quest_Content_Left_Content_Loadmap_DownBar_ML.GetComponent<Button>();

        m_gPanel_Quest_Content_Right = m_gPanel_Quest.transform.Find("Panel_Quest_Content_Right").gameObject;
        m_gPanel_Quest_Content_Right_UpBar = m_gPanel_Quest_Content_Right.transform.Find("Panel_Quest_Content_Right_UpBar").gameObject;
        m_TMP_Quest_Content_Right_UpBar_Name = m_gPanel_Quest_Content_Right_UpBar.transform.Find("TMP_Quest_Content_Right_UpBar_Name").gameObject.GetComponent<TextMeshProUGUI>();

        m_gPanel_Quest_Content_Right_Content = m_gPanel_Quest_Content_Right.transform.Find("Panel_Quest_Content_Right_Content").gameObject;
        m_gPanel_Quest_Content_Right_Content_Blank = m_gPanel_Quest_Content_Right_Content.transform.Find("Panel_Quest_Content_Right_Content_Blank").gameObject;

        m_gPanel_Quest_Content_Right_Content_QuestInfo = m_gPanel_Quest_Content_Right_Content.transform.Find("Panel_Quest_Content_Right_Content_QuestInfo").gameObject;
        m_gPanel_Quest_Content_Right_Content_QuestInfo_NPC = m_gPanel_Quest_Content_Right_Content_QuestInfo.transform.Find("Panel_Quest_Content_Right_Content_QuestInfo_NPC").gameObject;
        m_gIMG_Quest_Content_Right_Content_QuestInfo_NPC = m_gPanel_Quest_Content_Right_Content_QuestInfo_NPC.transform.Find("IMG_Quest_Content_Right_Content_QuestInfo_NPC").gameObject;
        m_IMG_Quest_Content_Right_Content_QuestInfo_NPC = m_gIMG_Quest_Content_Right_Content_QuestInfo_NPC.GetComponent<Image>();
        m_gPanel_Quest_Content_Right_Content_QuestInfo_Info = m_gPanel_Quest_Content_Right_Content_QuestInfo.transform.Find("Panel_Quest_Content_Right_Content_QuestInfo_Info").gameObject;
        m_TMP_Quest_Content_Right_Content_QuestInfo_Info = m_gPanel_Quest_Content_Right_Content_QuestInfo_Info.transform.Find("TMP_Quest_Content_Right_Content_QuestInfo_Info").gameObject.GetComponent<TextMeshProUGUI>();

        m_gPanel_Quest_Content_Right_Content_QuestDescription = m_gPanel_Quest_Content_Right_Content.transform.Find("Panel_Quest_Content_Right_Content_QuestDescription").gameObject;
        m_gPanel_Quest_Content_Right_Content_QuestDescription_UpBar = m_gPanel_Quest_Content_Right_Content_QuestDescription.transform.Find("Panel_Quest_Content_Right_Content_QuestDescription_UpBar").gameObject;
        m_BTN_Quest_Content_Right_Content_QuestDescription_UpBar_Condition = m_gPanel_Quest_Content_Right_Content_QuestDescription_UpBar.transform.Find("BTN_Quest_Content_Right_Content_QuestDescription_UpBar_Condition").gameObject.GetComponent<Button>();
        m_BTN_Quest_Content_Right_Content_QuestDescription_UpBar_Content = m_gPanel_Quest_Content_Right_Content_QuestDescription_UpBar.transform.Find("BTN_Quest_Content_Right_Content_QuestDescription_UpBar_Content").gameObject.GetComponent<Button>();
        m_BTN_Quest_Content_Right_Content_QuestDescription_UpBar_Reward = m_gPanel_Quest_Content_Right_Content_QuestDescription_UpBar.transform.Find("BTN_Quest_Content_Right_Content_QuestDescription_UpBar_Reward").gameObject.GetComponent<Button>();

        m_gSV_Quest_Content_Right_Content_QuestDescription_Condition = m_gPanel_Quest_Content_Right_Content_QuestDescription.transform.Find("SV_Quest_Content_Right_Content_QuestDescription_Condition").gameObject;
        m_ScrollRect_Quest_Content_Right_Content_QuestDescription_Condition = m_gSV_Quest_Content_Right_Content_QuestDescription_Condition.GetComponent<ScrollRect>();
        m_gViewport_Quest_Content_Right_Content_QuestDescription_Condition = m_gSV_Quest_Content_Right_Content_QuestDescription_Condition.transform.Find("Viewport_Quest_Content_Right_Content_QuestDescription_Condition").gameObject;
        m_gContent_Quest_Content_Right_Content_QuestDescription_Condition = m_gViewport_Quest_Content_Right_Content_QuestDescription_Condition.transform.Find("Content_Quest_Content_Right_Content_QuestDescription_Condition").gameObject;
        m_Scrollbar_Quest_Content_Right_Content_QuestDescription_Condition = m_gSV_Quest_Content_Right_Content_QuestDescription_Condition.transform.Find("Scrollbar_Quest_Content_Right_Content_QuestDescription_Condition").gameObject.GetComponent<Scrollbar>();

        m_gSV_Quest_Content_Right_Content_QuestDescription_Content = m_gPanel_Quest_Content_Right_Content_QuestDescription.transform.Find("SV_Quest_Content_Right_Content_QuestDescription_Content").gameObject;
        m_ScrollRect_Quest_Content_Right_Content_QuestDescription_Content = m_gSV_Quest_Content_Right_Content_QuestDescription_Content.GetComponent<ScrollRect>();
        m_gViewport_Quest_Content_Right_Content_QuestDescription_Content = m_gSV_Quest_Content_Right_Content_QuestDescription_Content.transform.Find("Viewport_Quest_Content_Right_Content_QuestDescription_Content").gameObject;
        m_TMP_Quest_Content_Right_Content_QuestDescription_Content = m_gViewport_Quest_Content_Right_Content_QuestDescription_Content.transform.Find("TMP_Quest_Content_Right_Content_QuestDescription_Content").gameObject.GetComponent<TextMeshProUGUI>();
        m_Scrollbar_Quest_Content_Right_Content_QuestDescription_Content = m_gSV_Quest_Content_Right_Content_QuestDescription_Content.transform.Find("Scrollbar_Quest_Content_Right_Content_QuestDescription_Content").gameObject.GetComponent<Scrollbar>();

        m_gSV_Quest_Content_Right_Content_QuestDescription_Reward = m_gPanel_Quest_Content_Right_Content_QuestDescription.transform.Find("SV_Quest_Content_Right_Content_QuestDescription_Reward").gameObject;
        m_ScrollRect_Quest_Content_Right_Content_QuestDescription_Reward = m_gSV_Quest_Content_Right_Content_QuestDescription_Reward.GetComponent<ScrollRect>();
        m_gViewport_Quest_Content_Right_Content_QuestDescription_Reward = m_gSV_Quest_Content_Right_Content_QuestDescription_Reward.transform.Find("Viewport_Quest_Content_Right_Content_QuestDescription_Reward").gameObject;
        m_gContent_Quest_Content_Right_Content_QuestDescription_Reward = m_gViewport_Quest_Content_Right_Content_QuestDescription_Reward.transform.Find("Content_Quest_Content_Right_Content_QuestDescription_Reward").gameObject;
        m_Scrollbar_Quest_Content_Right_Content_QuestDescription_Reward = m_gSV_Quest_Content_Right_Content_QuestDescription_Reward.transform.Find("Scrollbar_Quest_Content_Right_Content_QuestDescription_Reward").gameObject.GetComponent<Scrollbar>();
        m_gPanel_Quest_Content_Right_Content_QuestDescription_Reward_Blank = m_gViewport_Quest_Content_Right_Content_QuestDescription_Reward.transform.Find("Panel_Quest_Content_Right_Content_QuestDescription_Reward_Blank").gameObject;

        m_gList_Quest_Recommend = new List<GameObject>();
        m_nList_Quest_Recommend = new List<int>();
        m_gList_Quest_Process = new List<GameObject>();
        m_nList_Quest_Process = new List<int>();
        m_gList_Quest_Complete = new List<GameObject>();
        m_nList_Quest_Complete = new List<int>();

        m_gList_Quest_Info_Condition = new List<GameObject>();
        m_gList_Quest_Info_Reward = new List<GameObject>();

        m_gQuestContent = Resources.Load("Prefab/GUI/Panel_Quest_Content") as GameObject;
    }
    // 초기 Button 이벤트 설정.
    void InitialSet_Button()
    {
        m_BTN_Quest_Content_Left_Content_Loadmap_DownBar_PM.onClick.RemoveAllListeners();
        m_BTN_Quest_Content_Left_Content_Loadmap_DownBar_PM.onClick.AddListener(delegate { Btn_Press_Quest_Loadmap_ONOFF(); });
        m_BTN_Quest_Content_Left_Content_Loadmap_DownBar_MR.onClick.RemoveAllListeners();
        m_BTN_Quest_Content_Left_Content_Loadmap_DownBar_MR.onClick.AddListener(delegate { Btn_Press_Quest_Loadmap_Right(); });
        m_BTN_Quest_Content_Left_Content_Loadmap_DownBar_ML.onClick.RemoveAllListeners();
        m_BTN_Quest_Content_Left_Content_Loadmap_DownBar_ML.onClick.AddListener(delegate { Btn_Press_Quest_Loadmap_Left(); });

        m_BTN_Quest_Content_Right_Content_QuestDescription_UpBar_Condition.onClick.RemoveAllListeners();
        m_BTN_Quest_Content_Right_Content_QuestDescription_UpBar_Condition.onClick.AddListener(delegate { Btn_Press_Quest_Info_Condition(); });
        m_BTN_Quest_Content_Right_Content_QuestDescription_UpBar_Content.onClick.RemoveAllListeners();
        m_BTN_Quest_Content_Right_Content_QuestDescription_UpBar_Content.onClick.AddListener(delegate { Btn_Press_Quest_Info_Content(); });
        m_BTN_Quest_Content_Right_Content_QuestDescription_UpBar_Reward.onClick.RemoveAllListeners();
        m_BTN_Quest_Content_Right_Content_QuestDescription_UpBar_Reward.onClick.AddListener(delegate { Btn_Press_Quest_Info_Reward(); });

        m_BTN_Quest_Content_Left_UpBar_Recommend.onClick.RemoveAllListeners();
        m_BTN_Quest_Content_Left_UpBar_Recommend.onClick.AddListener(delegate { Btn_Press_Quest_Recommend(); });
        m_BTN_Quest_Content_Left_UpBar_Progress.onClick.RemoveAllListeners();
        m_BTN_Quest_Content_Left_UpBar_Progress.onClick.AddListener(delegate { Btn_Press_Quest_Process(); });
        m_BTN_Quest_Content_Left_UpBar_Complete.onClick.RemoveAllListeners();
        m_BTN_Quest_Content_Left_UpBar_Complete.onClick.AddListener(delegate { Btn_Press_Quest_Complete(); });
        m_BTN_Quest_Exit.onClick.RemoveAllListeners();
        m_BTN_Quest_Exit.onClick.AddListener(delegate { Btn_Press_Quest_Exit(); });
    }

    // Button 이벤트 추가.
    public void Btn_Press_Quest_Loadmap_ONOFF()
    {
        if (m_gSV_Quest_Content_Left_Content_Loadmap.activeSelf == true)
        {
            m_RectTransform_Content_Left_Content_QuestList.sizeDelta = new Vector2(504, 457);

            m_gSV_Quest_Content_Left_Content_Loadmap.SetActive(false);

            m_TMP_Quest_Content_Left_Content_Loadmap_DownBar_PM.text = "+";

            m_eQuest_Loadmap = E_QUEST_LOADMAP.OFF;
        }
        else
        {
            m_RectTransform_Content_Left_Content_QuestList.sizeDelta = new Vector2(504, 235);

            m_gSV_Quest_Content_Left_Content_Loadmap.SetActive(true);

            m_TMP_Quest_Content_Left_Content_Loadmap_DownBar_PM.text = "-";

            m_eQuest_Loadmap = E_QUEST_LOADMAP.ON;
        }
    }
    public void Btn_Press_Quest_Loadmap_Right()
    {
        if (m_gPanel_Quest_Content_Left_Content_Loadmap_Map_Blank.activeSelf == true)
        {
            m_gPanel_Quest_Content_Left_Content_Loadmap_Map_Blank.SetActive(false);
        }
        else
        {
            if (m_eMap == E_MAP.MAP_TUTORIAL)
            {
                m_eMap = E_MAP.MAP_CHAPTER1;
            }
        }

        Check_Btn_Quest_Loadmap_RL();
    }
    public void Btn_Press_Quest_Loadmap_Left()
    {
        if (m_eMap == E_MAP.MAP_CHAPTER1)
        {
            m_eMap = E_MAP.MAP_TUTORIAL;
        }

        Check_Btn_Quest_Loadmap_RL();
    }
    // 퀘스트 로드맵의 맵 변경 시 관련 버튼의 상태 체크
    // 퀘스트 로드맵 변경
    void Check_Btn_Quest_Loadmap_RL()
    {
        if (m_eMap == E_MAP.MAP_TUTORIAL)
        {
            m_gBTN_Quest_Content_Left_Content_Loadmap_DownBar_ML.SetActive(false);
            m_gBTN_Quest_Content_Left_Content_Loadmap_DownBar_MR.SetActive(true);

            m_gContent_Quest_Content_Left_Content_Loadmap_Map_Tutorial.SetActive(true);
            m_gContent_Quest_Content_Left_Content_Loadmap_Map_Chapter1.SetActive(false);

            m_ScrollRect_Quest_Content_Left_Content_Loadmap.content = m_gContent_Quest_Content_Left_Content_Loadmap_Map_Tutorial.GetComponent<RectTransform>();

            m_TMP_Quest_Content_Left_Content_Loadmap_DownBar.text = "로드맵[깊디깊은숲]";
        }
        else if (m_eMap == E_MAP.MAP_CHAPTER1)
        {
            m_gBTN_Quest_Content_Left_Content_Loadmap_DownBar_ML.SetActive(true);
            m_gBTN_Quest_Content_Left_Content_Loadmap_DownBar_MR.SetActive(false);

            m_gContent_Quest_Content_Left_Content_Loadmap_Map_Tutorial.SetActive(false);
            m_gContent_Quest_Content_Left_Content_Loadmap_Map_Chapter1.SetActive(true);

            m_ScrollRect_Quest_Content_Left_Content_Loadmap.content = m_gContent_Quest_Content_Left_Content_Loadmap_Map_Chapter1.GetComponent<RectTransform>();

            m_TMP_Quest_Content_Left_Content_Loadmap_DownBar.text = "로드맵[드넓은 초원]";
        }
    }

    public void Btn_Press_Quest_Recommend()
    {
        m_BTN_Quest_Content_Left_UpBar_Recommend.GetComponent<Image>().color = new Color(0.75f, 0.75f, 0.75f);
        m_BTN_Quest_Content_Left_UpBar_Progress.GetComponent<Image>().color = new Color(1, 1, 1);
        m_BTN_Quest_Content_Left_UpBar_Complete.GetComponent<Image>().color = new Color(1, 1, 1);

        m_eQuest_State = E_QUEST_STATE.RECOMMEND;
        
        m_nQuest_Selected_Number = 0;

        Check_Btn_Quest_RPC();
    }
    public void Btn_Press_Quest_Process()
    {
        m_BTN_Quest_Content_Left_UpBar_Recommend.GetComponent<Image>().color = new Color(1, 1, 1);
        m_BTN_Quest_Content_Left_UpBar_Progress.GetComponent<Image>().color = new Color(0.75f, 0.75f, 0.75f);
        m_BTN_Quest_Content_Left_UpBar_Complete.GetComponent<Image>().color = new Color(1, 1, 1);

        m_eQuest_State = E_QUEST_STATE.PROGRESS;

        m_nQuest_Selected_Number = 0;

        Check_Btn_Quest_RPC();
    }
    public void Btn_Press_Quest_Complete()
    {
        m_BTN_Quest_Content_Left_UpBar_Recommend.GetComponent<Image>().color = new Color(1, 1, 1);
        m_BTN_Quest_Content_Left_UpBar_Progress.GetComponent<Image>().color = new Color(1, 1, 1);
        m_BTN_Quest_Content_Left_UpBar_Complete.GetComponent<Image>().color = new Color(0.75f, 0.75f, 0.75f);

        m_eQuest_State = E_QUEST_STATE.COMPLETE;

        m_nQuest_Selected_Number = 0;

        Check_Btn_Quest_RPC();
    }
    // 퀘스트 카테고리 변경 시 관련 상태 체크
    void Check_Btn_Quest_RPC()
    {
        if (m_eQuest_State == E_QUEST_STATE.RECOMMEND)
        {
            m_gContent_Quest_Content_Left_Content_QuestList_Recommend.SetActive(true);
            m_gContent_Quest_Content_Left_Content_QuestList_Progress.SetActive(false);
            m_gContent_Quest_Content_Left_Content_QuestList_Complete.SetActive(false);

            m_ScrollRect_Quest_Content_Left_Content_QuestList.content = m_gContent_Quest_Content_Left_Content_QuestList_Recommend.GetComponent<RectTransform>();
        }
        else if (m_eQuest_State == E_QUEST_STATE.PROGRESS)
        {
            m_gContent_Quest_Content_Left_Content_QuestList_Recommend.SetActive(false);
            m_gContent_Quest_Content_Left_Content_QuestList_Progress.SetActive(true);
            m_gContent_Quest_Content_Left_Content_QuestList_Complete.SetActive(false);

            m_ScrollRect_Quest_Content_Left_Content_QuestList.content = m_gContent_Quest_Content_Left_Content_QuestList_Progress.GetComponent<RectTransform>();
        }
        else if (m_eQuest_State == E_QUEST_STATE.COMPLETE)
        {
            m_gContent_Quest_Content_Left_Content_QuestList_Recommend.SetActive(false);
            m_gContent_Quest_Content_Left_Content_QuestList_Progress.SetActive(false);
            m_gContent_Quest_Content_Left_Content_QuestList_Complete.SetActive(true);

            m_ScrollRect_Quest_Content_Left_Content_QuestList.content = m_gContent_Quest_Content_Left_Content_QuestList_Complete.GetComponent<RectTransform>();
        }
    }

    public void Btn_Press_Quest_Info_Condition()
    {
        m_BTN_Quest_Content_Right_Content_QuestDescription_UpBar_Condition.GetComponent<Image>().color = new Color(0.75f, 0.75f, 0.75f);
        m_BTN_Quest_Content_Right_Content_QuestDescription_UpBar_Content.GetComponent<Image>().color = new Color(1, 1, 1);
        m_BTN_Quest_Content_Right_Content_QuestDescription_UpBar_Reward.GetComponent<Image>().color = new Color(1, 1, 1);

        m_eQuest_Info = E_QUEST_INFO.CONDITION;

        Check_Btn_Quest_Info();
    }
    public void Btn_Press_Quest_Info_Content()
    {
        m_BTN_Quest_Content_Right_Content_QuestDescription_UpBar_Condition.GetComponent<Image>().color = new Color(1, 1, 1);
        m_BTN_Quest_Content_Right_Content_QuestDescription_UpBar_Content.GetComponent<Image>().color = new Color(0.75f, 0.75f, 0.75f);
        m_BTN_Quest_Content_Right_Content_QuestDescription_UpBar_Reward.GetComponent<Image>().color = new Color(1, 1, 1);

        m_eQuest_Info = E_QUEST_INFO.CONTENT;

        Check_Btn_Quest_Info();
    }
    public void Btn_Press_Quest_Info_Reward()
    {
        m_BTN_Quest_Content_Right_Content_QuestDescription_UpBar_Condition.GetComponent<Image>().color = new Color(1, 1, 1);
        m_BTN_Quest_Content_Right_Content_QuestDescription_UpBar_Content.GetComponent<Image>().color = new Color(1, 1, 1);
        m_BTN_Quest_Content_Right_Content_QuestDescription_UpBar_Reward.GetComponent<Image>().color = new Color(0.75f, 0.75f, 0.75f);

        m_eQuest_Info = E_QUEST_INFO.REWARD;

        Check_Btn_Quest_Info();
    }
    // 퀘스트 내용 확인 관련 상태 체크
    void Check_Btn_Quest_Info()
    {
        if (m_eQuest_Info == E_QUEST_INFO.CONDITION)
        {
            m_gSV_Quest_Content_Right_Content_QuestDescription_Condition.SetActive(true);
            m_gSV_Quest_Content_Right_Content_QuestDescription_Content.SetActive(false);
            m_gSV_Quest_Content_Right_Content_QuestDescription_Reward.SetActive(false);
        }
        else if (m_eQuest_Info == E_QUEST_INFO.CONTENT)
        {
            m_gSV_Quest_Content_Right_Content_QuestDescription_Condition.SetActive(false);
            m_gSV_Quest_Content_Right_Content_QuestDescription_Content.SetActive(true);
            m_gSV_Quest_Content_Right_Content_QuestDescription_Reward.SetActive(false);
        }
        else if (m_eQuest_Info == E_QUEST_INFO.REWARD)
        {
            m_gSV_Quest_Content_Right_Content_QuestDescription_Condition.SetActive(false);
            m_gSV_Quest_Content_Right_Content_QuestDescription_Content.SetActive(false);
            m_gSV_Quest_Content_Right_Content_QuestDescription_Reward.SetActive(true);
        }

        m_gPanel_Quest_Content_Right_Content_Blank.SetActive(false);
    }

    public void Btn_Press_Quest_Exit()
    {
        Display_GUI_Quest();
        GUIManager_Total.Instance.Delete_GUI_Priority(4);
    }

    public bool Display_GUI_Quest()
    {
        if (m_gPanel_Quest.activeSelf == true)
        {
            m_gPanel_Quest.SetActive(false);

            return false;
        }
        else
        {
            m_gPanel_Quest.SetActive(true);
            m_gPanel_Quest.transform.SetAsLastSibling();
            Check_Btn_Quest_Loadmap_RL();
            Update_QuestLoadmap();

            return true;
        }
    }
    
    // Quest 현황 갱신.
    // 진행 중이던 Quest를 클리어하면 완료한 Quest로 이동.
    // num == 0: 새로운 Quest 추가.
    // num == 1: 시작 가능한 Quest 갱신(진행중).
    // num == 2: 진행 중이던 Quest 갱신(클리어).
    public void UpdateQuest(Quest_KILL_MONSTER quest, int num)
    {
        if (num == 0)
        {
            GameObject copyquestcontent = Instantiate(m_gQuestContent);
            copyquestcontent.GetComponent<questpanel>().SetQuestInformation(quest);

            RectTransform contentpos = copyquestcontent.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
            m_gList_Quest_Process.Add(copyquestcontent);
            m_nList_Quest_Process.Add(quest.m_nQuest_Code);
        }
        else if (num == 1)
        {
            for (int i = 0; i < m_gList_Quest_Recommend.Count; i++)
            {
                if (m_gList_Quest_Recommend[i].GetComponent<questpanel>().m_QuestType == E_QUEST_TYPE.KILL_MONSTER)
                {
                    if (quest.m_nQuest_Code == m_gList_Quest_Recommend[i].GetComponent<questpanel>().m_Quest_KILL_MONSTER.m_nQuest_Code)
                    {
                        for (int j = 0; j < m_gContent_Quest_Content_Left_Content_QuestList_Recommend.transform.childCount; j++)
                        {
                            if (m_gContent_Quest_Content_Left_Content_QuestList_Recommend.transform.GetChild(j).name == "QuestContent_" + quest.m_sQuest_Title)
                            {
                                Destroy(m_gContent_Quest_Content_Left_Content_QuestList_Recommend.transform.GetChild(j).gameObject);

                                break;
                            }
                        }
                        m_gList_Quest_Recommend.RemoveAt(i);
                        m_nList_Quest_Recommend.RemoveAt(i);

                        GameObject copyquestcontent = Instantiate(m_gQuestContent);
                        copyquestcontent.GetComponent<questpanel>().SetQuestInformation(quest);

                        RectTransform contentpos = copyquestcontent.GetComponent<RectTransform>();
                        contentpos.SetParent(m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform);
                        contentpos.transform.localScale = new Vector3(1, 1, 1);
                        contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                        m_gList_Quest_Process.Add(copyquestcontent);
                        m_nList_Quest_Process.Add(quest.m_nQuest_Code);

                        break;
                    }
                }
            }
        }
        else if (num == 2)
        {
            for (int i = 0; i < m_gList_Quest_Process.Count; i++)
            {
                if (m_gList_Quest_Process[i].GetComponent<questpanel>().m_QuestType == E_QUEST_TYPE.KILL_MONSTER)
                {
                    if (quest.m_nQuest_Code == m_gList_Quest_Process[i].GetComponent<questpanel>().m_Quest_KILL_MONSTER.m_nQuest_Code)
                    {
                        for (int j = 0; j < m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform.childCount; j++)
                        {
                            if (m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform.GetChild(j).name == "QuestContent_" + quest.m_sQuest_Title)
                            {
                                Destroy(m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform.GetChild(j).gameObject);

                                break;
                            }
                        }
                        m_gList_Quest_Process.RemoveAt(i);
                        m_nList_Quest_Process.RemoveAt(i);

                        GameObject copyquestcontent = Instantiate(m_gQuestContent);
                        copyquestcontent.GetComponent<questpanel>().SetQuestInformation(quest);
                        copyquestcontent.GetComponent<questpanel>().UnEffect_Flesh();

                        RectTransform contentpos = copyquestcontent.GetComponent<RectTransform>();
                        contentpos.SetParent(m_gContent_Quest_Content_Left_Content_QuestList_Complete.transform);
                        contentpos.transform.localScale = new Vector3(1, 1, 1);
                        contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                        m_gList_Quest_Complete.Add(copyquestcontent);
                        m_nList_Quest_Complete.Add(quest.m_nQuest_Code);

                        break;
                    }
                }
            }
        }
        //if (num == 1)
        //{
        //    GameObject copyquestcontent = Instantiate(m_gQuestContent);
        //    copyquestcontent.GetComponent<questpanel>().SetQuestInformation(quest);

        //    RectTransform contentpos = copyquestcontent.GetComponent<RectTransform>();
        //    contentpos.SetParent(m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform);
        //    contentpos.transform.localScale = new Vector3(1, 1, 1);
        //    contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
        //    m_gList_Quest_Process.Add(copyquestcontent);
        //    m_nList_Quest_Process.Add(quest.m_nQuest_Code);
        //}
        //else if (num == 2)
        //{
        //    for (int i = 0; i < m_gList_Quest_Process.Count; i++)
        //    {
        //        if (m_gList_Quest_Process[i].GetComponent<questpanel>().m_QuestType == E_QUEST_TYPE.KILL_MONSTER)
        //        {
        //            if (quest.m_nQuest_Code == m_gList_Quest_Process[i].GetComponent<questpanel>().m_Quest_KILL_MONSTER.m_nQuest_Code)
        //            {
        //                for (int j = 0; j < m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform.childCount; j++)
        //                {
        //                    if (m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform.GetChild(j).name == "QuestContent_" + quest.m_sQuest_Title)
        //                    {
        //                        Destroy(m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform.GetChild(j).gameObject);

        //                        break;
        //                    }
        //                }
        //                m_gList_Quest_Process.RemoveAt(i);
        //                m_nList_Quest_Process.RemoveAt(i);

        //                GameObject copyquestcontent = Instantiate(m_gQuestContent);
        //                copyquestcontent.GetComponent<questpanel>().SetQuestInformation(quest);
        //                copyquestcontent.GetComponent<questpanel>().UnEffect_Flesh();

        //                RectTransform contentpos = copyquestcontent.GetComponent<RectTransform>();
        //                contentpos.SetParent(m_gContent_Quest_Content_Left_Content_QuestList_Complete.transform);
        //                contentpos.transform.localScale = new Vector3(1, 1, 1);
        //                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
        //                m_gList_Quest_Complete.Add(copyquestcontent);
        //                m_nList_Quest_Complete.Add(quest.m_nQuest_Code);

        //                break;
        //            }
        //        }
        //    }
        //}
    }
    public void UpdateQuest(Quest_KILL_TYPE quest, int num)
    {
        if (num == 0)
        {
            GameObject copyquestcontent = Instantiate(m_gQuestContent);
            copyquestcontent.GetComponent<questpanel>().SetQuestInformation(quest);

            RectTransform contentpos = copyquestcontent.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
            m_gList_Quest_Process.Add(copyquestcontent);
            m_nList_Quest_Process.Add(quest.m_nQuest_Code);
        }
        else if (num == 1)
        {
            for (int i = 0; i < m_gList_Quest_Recommend.Count; i++)
            {
                if (m_gList_Quest_Recommend[i].GetComponent<questpanel>().m_QuestType == E_QUEST_TYPE.KILL_TYPE)
                {
                    if (quest.m_nQuest_Code == m_gList_Quest_Recommend[i].GetComponent<questpanel>().m_Quest_KILL_TYPE.m_nQuest_Code)
                    {
                        for (int j = 0; j < m_gContent_Quest_Content_Left_Content_QuestList_Recommend.transform.childCount; j++)
                        {
                            if (m_gContent_Quest_Content_Left_Content_QuestList_Recommend.transform.GetChild(j).name == "QuestContent_" + quest.m_sQuest_Title)
                            {
                                Destroy(m_gContent_Quest_Content_Left_Content_QuestList_Recommend.transform.GetChild(j).gameObject);

                                break;
                            }
                        }
                        m_gList_Quest_Recommend.RemoveAt(i);
                        m_nList_Quest_Recommend.RemoveAt(i);

                        GameObject copyquestcontent = Instantiate(m_gQuestContent);
                        copyquestcontent.GetComponent<questpanel>().SetQuestInformation(quest);

                        RectTransform contentpos = copyquestcontent.GetComponent<RectTransform>();
                        contentpos.SetParent(m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform);
                        contentpos.transform.localScale = new Vector3(1, 1, 1);
                        contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                        m_gList_Quest_Process.Add(copyquestcontent);
                        m_nList_Quest_Process.Add(quest.m_nQuest_Code);

                        break;
                    }
                }
            }
        }
        else if (num == 2)
        {
            for (int i = 0; i < m_gList_Quest_Process.Count; i++)
            {
                if (m_gList_Quest_Process[i].GetComponent<questpanel>().m_QuestType == E_QUEST_TYPE.KILL_TYPE)
                {
                    if (quest.m_nQuest_Code == m_gList_Quest_Process[i].GetComponent<questpanel>().m_Quest_KILL_TYPE.m_nQuest_Code)
                    {
                        for (int j = 0; j < m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform.childCount; j++)
                        {
                            if (m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform.GetChild(j).name == "QuestContent_" + quest.m_sQuest_Title)
                            {
                                Destroy(m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform.GetChild(j).gameObject);

                                break;
                            }
                        }
                        m_gList_Quest_Process.RemoveAt(i);
                        m_nList_Quest_Process.RemoveAt(i);

                        GameObject copyquestcontent = Instantiate(m_gQuestContent);
                        copyquestcontent.GetComponent<questpanel>().SetQuestInformation(quest);
                        copyquestcontent.GetComponent<questpanel>().UnEffect_Flesh();

                        RectTransform contentpos = copyquestcontent.GetComponent<RectTransform>();
                        contentpos.SetParent(m_gContent_Quest_Content_Left_Content_QuestList_Complete.transform);
                        contentpos.transform.localScale = new Vector3(1, 1, 1);
                        contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                        m_gList_Quest_Complete.Add(copyquestcontent);
                        m_nList_Quest_Complete.Add(quest.m_nQuest_Code);

                        break;
                    }
                }
            }
        }
    }
    public void UpdateQuest(Quest_GOAWAY_MONSTER quest, int num)
    {
        if (num == 0)
        {
            GameObject copyquestcontent = Instantiate(m_gQuestContent);
            copyquestcontent.GetComponent<questpanel>().SetQuestInformation(quest);

            RectTransform contentpos = copyquestcontent.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
            m_gList_Quest_Process.Add(copyquestcontent);
            m_nList_Quest_Process.Add(quest.m_nQuest_Code);
        }
        else if (num == 1)
        {
            for (int i = 0; i < m_gList_Quest_Recommend.Count; i++)
            {
                if (m_gList_Quest_Recommend[i].GetComponent<questpanel>().m_QuestType == E_QUEST_TYPE.GOAWAY_MONSTER)
                {
                    if (quest.m_nQuest_Code == m_gList_Quest_Recommend[i].GetComponent<questpanel>().m_Quest_GOAWAY_MONSTER.m_nQuest_Code)
                    {
                        for (int j = 0; j < m_gContent_Quest_Content_Left_Content_QuestList_Recommend.transform.childCount; j++)
                        {
                            if (m_gContent_Quest_Content_Left_Content_QuestList_Recommend.transform.GetChild(j).name == "QuestContent_" + quest.m_sQuest_Title)
                            {
                                Destroy(m_gContent_Quest_Content_Left_Content_QuestList_Recommend.transform.GetChild(j).gameObject);

                                break;
                            }
                        }
                        m_gList_Quest_Recommend.RemoveAt(i);
                        m_nList_Quest_Recommend.RemoveAt(i);

                        GameObject copyquestcontent = Instantiate(m_gQuestContent);
                        copyquestcontent.GetComponent<questpanel>().SetQuestInformation(quest);

                        RectTransform contentpos = copyquestcontent.GetComponent<RectTransform>();
                        contentpos.SetParent(m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform);
                        contentpos.transform.localScale = new Vector3(1, 1, 1);
                        contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                        m_gList_Quest_Process.Add(copyquestcontent);
                        m_nList_Quest_Process.Add(quest.m_nQuest_Code);

                        break;
                    }
                }
            }
        }
        else if (num == 2)
        {
            for (int i = 0; i < m_gList_Quest_Process.Count; i++)
            {
                if (m_gList_Quest_Process[i].GetComponent<questpanel>().m_QuestType == E_QUEST_TYPE.GOAWAY_MONSTER)
                {
                    if (quest.m_nQuest_Code == m_gList_Quest_Process[i].GetComponent<questpanel>().m_Quest_GOAWAY_MONSTER.m_nQuest_Code)
                    {
                        for (int j = 0; j < m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform.childCount; j++)
                        {
                            if (m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform.GetChild(j).name == "QuestContent_" + quest.m_sQuest_Title)
                            {
                                Destroy(m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform.GetChild(j).gameObject);

                                break;
                            }
                        }
                        m_gList_Quest_Process.RemoveAt(i);
                        m_nList_Quest_Process.RemoveAt(i);

                        GameObject copyquestcontent = Instantiate(m_gQuestContent);
                        copyquestcontent.GetComponent<questpanel>().SetQuestInformation(quest);
                        copyquestcontent.GetComponent<questpanel>().UnEffect_Flesh();

                        RectTransform contentpos = copyquestcontent.GetComponent<RectTransform>();
                        contentpos.SetParent(m_gContent_Quest_Content_Left_Content_QuestList_Complete.transform);
                        contentpos.transform.localScale = new Vector3(1, 1, 1);
                        contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                        m_gList_Quest_Complete.Add(copyquestcontent);
                        m_nList_Quest_Complete.Add(quest.m_nQuest_Code);

                        break;
                    }
                }
            }
        }
    }
    public void UpdateQuest(Quest_GOAWAY_TYPE quest, int num)
    {
        if (num == 0)
        {
            GameObject copyquestcontent = Instantiate(m_gQuestContent);
            copyquestcontent.GetComponent<questpanel>().SetQuestInformation(quest);

            RectTransform contentpos = copyquestcontent.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
            m_gList_Quest_Process.Add(copyquestcontent);
            m_nList_Quest_Process.Add(quest.m_nQuest_Code);
        }
        else if (num == 1)
        {
            for (int i = 0; i < m_gList_Quest_Recommend.Count; i++)
            {
                if (m_gList_Quest_Recommend[i].GetComponent<questpanel>().m_QuestType == E_QUEST_TYPE.GOAWAY_TYPE)
                {
                    if (quest.m_nQuest_Code == m_gList_Quest_Recommend[i].GetComponent<questpanel>().m_Quest_GOAWAY_TYPE.m_nQuest_Code)
                    {
                        for (int j = 0; j < m_gContent_Quest_Content_Left_Content_QuestList_Recommend.transform.childCount; j++)
                        {
                            if (m_gContent_Quest_Content_Left_Content_QuestList_Recommend.transform.GetChild(j).name == "QuestContent_" + quest.m_sQuest_Title)
                            {
                                Destroy(m_gContent_Quest_Content_Left_Content_QuestList_Recommend.transform.GetChild(j).gameObject);

                                break;
                            }
                        }
                        m_gList_Quest_Recommend.RemoveAt(i);
                        m_nList_Quest_Recommend.RemoveAt(i);

                        GameObject copyquestcontent = Instantiate(m_gQuestContent);
                        copyquestcontent.GetComponent<questpanel>().SetQuestInformation(quest);

                        RectTransform contentpos = copyquestcontent.GetComponent<RectTransform>();
                        contentpos.SetParent(m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform);
                        contentpos.transform.localScale = new Vector3(1, 1, 1);
                        contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                        m_gList_Quest_Process.Add(copyquestcontent);
                        m_nList_Quest_Process.Add(quest.m_nQuest_Code);

                        break;
                    }
                }
            }
        }
        else if (num == 2)
        {
            for (int i = 0; i < m_gList_Quest_Process.Count; i++)
            {
                if (m_gList_Quest_Process[i].GetComponent<questpanel>().m_QuestType == E_QUEST_TYPE.GOAWAY_TYPE)
                {
                    if (quest.m_nQuest_Code == m_gList_Quest_Process[i].GetComponent<questpanel>().m_Quest_GOAWAY_TYPE.m_nQuest_Code)
                    {
                        for (int j = 0; j < m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform.childCount; j++)
                        {
                            if (m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform.GetChild(j).name == "QuestContent_" + quest.m_sQuest_Title)
                            {
                                Destroy(m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform.GetChild(j).gameObject);

                                break;
                            }
                        }
                        m_gList_Quest_Process.RemoveAt(i);
                        m_nList_Quest_Process.RemoveAt(i);

                        GameObject copyquestcontent = Instantiate(m_gQuestContent);
                        copyquestcontent.GetComponent<questpanel>().SetQuestInformation(quest);
                        copyquestcontent.GetComponent<questpanel>().UnEffect_Flesh();

                        RectTransform contentpos = copyquestcontent.GetComponent<RectTransform>();
                        contentpos.SetParent(m_gContent_Quest_Content_Left_Content_QuestList_Complete.transform);
                        contentpos.transform.localScale = new Vector3(1, 1, 1);
                        contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                        m_gList_Quest_Complete.Add(copyquestcontent);
                        m_nList_Quest_Complete.Add(quest.m_nQuest_Code);

                        break;
                    }
                }
            }
        }
    }
    public void UpdateQuest(Quest_COLLECT quest, int num)
    {
        if (num == 0)
        {
            GameObject copyquestcontent = Instantiate(m_gQuestContent);
            copyquestcontent.GetComponent<questpanel>().SetQuestInformation(quest);

            RectTransform contentpos = copyquestcontent.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
            m_gList_Quest_Process.Add(copyquestcontent);
            m_nList_Quest_Process.Add(quest.m_nQuest_Code);
        }
        else if (num == 1)
        {
            for (int i = 0; i < m_gList_Quest_Recommend.Count; i++)
            {
                if (m_gList_Quest_Recommend[i].GetComponent<questpanel>().m_QuestType == E_QUEST_TYPE.COLLECT)
                {
                    if (quest.m_nQuest_Code == m_gList_Quest_Recommend[i].GetComponent<questpanel>().m_Quest_COLLECT.m_nQuest_Code)
                    {
                        for (int j = 0; j < m_gContent_Quest_Content_Left_Content_QuestList_Recommend.transform.childCount; j++)
                        {
                            if (m_gContent_Quest_Content_Left_Content_QuestList_Recommend.transform.GetChild(j).name == "QuestContent_" + quest.m_sQuest_Title)
                            {
                                Destroy(m_gContent_Quest_Content_Left_Content_QuestList_Recommend.transform.GetChild(j).gameObject);

                                break;
                            }
                        }
                        m_gList_Quest_Recommend.RemoveAt(i);
                        m_nList_Quest_Recommend.RemoveAt(i);

                        GameObject copyquestcontent = Instantiate(m_gQuestContent);
                        copyquestcontent.GetComponent<questpanel>().SetQuestInformation(quest);

                        RectTransform contentpos = copyquestcontent.GetComponent<RectTransform>();
                        contentpos.SetParent(m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform);
                        contentpos.transform.localScale = new Vector3(1, 1, 1);
                        contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                        m_gList_Quest_Process.Add(copyquestcontent);
                        m_nList_Quest_Process.Add(quest.m_nQuest_Code);

                        break;
                    }
                }
            }
        }
        else if (num == 2)
        {
            for (int i = 0; i < m_gList_Quest_Process.Count; i++)
            {
                //Debug.Log(quest.m_nQuest_Code);
                //Debug.Log(m_gList_Quest_Process[i].GetComponent<questpanel>().m_Quest_COLLECT.m_nQuest_Code);
                if (m_gList_Quest_Process[i].GetComponent<questpanel>().m_QuestType == E_QUEST_TYPE.COLLECT)
                {
                    if (quest.m_nQuest_Code == m_gList_Quest_Process[i].GetComponent<questpanel>().m_Quest_COLLECT.m_nQuest_Code)
                    {
                        for (int j = 0; j < m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform.childCount; j++)
                        {
                            if (m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform.GetChild(j).name == "QuestContent_" + quest.m_sQuest_Title)
                            {
                                Destroy(m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform.GetChild(j).gameObject);

                                break;
                            }
                        }
                        m_gList_Quest_Process.RemoveAt(i);
                        m_nList_Quest_Process.RemoveAt(i);

                        GameObject copyquestcontent = Instantiate(m_gQuestContent);
                        copyquestcontent.GetComponent<questpanel>().SetQuestInformation(quest);
                        copyquestcontent.GetComponent<questpanel>().UnEffect_Flesh();

                        RectTransform contentpos = copyquestcontent.GetComponent<RectTransform>();
                        contentpos.SetParent(m_gContent_Quest_Content_Left_Content_QuestList_Complete.transform);
                        contentpos.transform.localScale = new Vector3(1, 1, 1);
                        contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                        m_gList_Quest_Complete.Add(copyquestcontent);
                        m_nList_Quest_Complete.Add(quest.m_nQuest_Code);

                        break;
                    }
                }
            }
        }
    }
    public void UpdateQuest(Quest_CONVERSATION quest, int num)
    {
        if (num == 0)
        {
            GameObject copyquestcontent = Instantiate(m_gQuestContent);
            copyquestcontent.GetComponent<questpanel>().SetQuestInformation(quest);

            RectTransform contentpos = copyquestcontent.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
            m_gList_Quest_Process.Add(copyquestcontent);
            m_nList_Quest_Process.Add(quest.m_nQuest_Code);
        }
        else if (num == 1)
        {
            for (int i = 0; i < m_gList_Quest_Recommend.Count; i++)
            {
                if (m_gList_Quest_Recommend[i].GetComponent<questpanel>().m_QuestType == E_QUEST_TYPE.CONVERSATION)
                {
                    if (quest.m_nQuest_Code == m_gList_Quest_Recommend[i].GetComponent<questpanel>().m_Quest_CONVERSATION.m_nQuest_Code)
                    {
                        for (int j = 0; j < m_gContent_Quest_Content_Left_Content_QuestList_Recommend.transform.childCount; j++)
                        {
                            if (m_gContent_Quest_Content_Left_Content_QuestList_Recommend.transform.GetChild(j).name == "QuestContent_" + quest.m_sQuest_Title)
                            {
                                Destroy(m_gContent_Quest_Content_Left_Content_QuestList_Recommend.transform.GetChild(j).gameObject);

                                break;
                            }
                        }
                        m_gList_Quest_Recommend.RemoveAt(i);
                        m_nList_Quest_Recommend.RemoveAt(i);

                        GameObject copyquestcontent = Instantiate(m_gQuestContent);
                        copyquestcontent.GetComponent<questpanel>().SetQuestInformation(quest);

                        RectTransform contentpos = copyquestcontent.GetComponent<RectTransform>();
                        contentpos.SetParent(m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform);
                        contentpos.transform.localScale = new Vector3(1, 1, 1);
                        contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                        m_gList_Quest_Process.Add(copyquestcontent);
                        m_nList_Quest_Process.Add(quest.m_nQuest_Code);

                        break;
                    }
                }
            }
        }
        else if (num == 2)
        {
            for (int i = 0; i < m_gList_Quest_Process.Count; i++)
            {
                if (m_gList_Quest_Process[i].GetComponent<questpanel>().m_QuestType == E_QUEST_TYPE.CONVERSATION)
                {
                    if (quest.m_nQuest_Code == m_gList_Quest_Process[i].GetComponent<questpanel>().m_Quest_CONVERSATION.m_nQuest_Code)
                    {
                        for (int j = 0; j < m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform.childCount; j++)
                        {
                            if (m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform.GetChild(j).name == "QuestContent_" + quest.m_sQuest_Title)
                            {
                                Destroy(m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform.GetChild(j).gameObject);

                                break;
                            }
                        }
                        m_gList_Quest_Process.RemoveAt(i);
                        m_nList_Quest_Process.RemoveAt(i);

                        GameObject copyquestcontent = Instantiate(m_gQuestContent);
                        copyquestcontent.GetComponent<questpanel>().SetQuestInformation(quest);
                        copyquestcontent.GetComponent<questpanel>().UnEffect_Flesh();

                        RectTransform contentpos = copyquestcontent.GetComponent<RectTransform>();
                        contentpos.SetParent(m_gContent_Quest_Content_Left_Content_QuestList_Complete.transform);
                        contentpos.transform.localScale = new Vector3(1, 1, 1);
                        contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                        m_gList_Quest_Complete.Add(copyquestcontent);
                        m_nList_Quest_Complete.Add(quest.m_nQuest_Code);

                        break;
                    }
                }
            }
        }
    }
    public void UpdateQuest(Quest_ROLL quest, int num)
    {
        if (num == 0)
        {
            GameObject copyquestcontent = Instantiate(m_gQuestContent);
            copyquestcontent.GetComponent<questpanel>().SetQuestInformation(quest);

            RectTransform contentpos = copyquestcontent.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
            m_gList_Quest_Process.Add(copyquestcontent);
            m_nList_Quest_Process.Add(quest.m_nQuest_Code);
        }
        else if (num == 1)
        {
            for (int i = 0; i < m_gList_Quest_Recommend.Count; i++)
            {
                if (m_gList_Quest_Recommend[i].GetComponent<questpanel>().m_QuestType == E_QUEST_TYPE.ROLL)
                {
                    if (quest.m_nQuest_Code == m_gList_Quest_Recommend[i].GetComponent<questpanel>().m_Quest_ROLL.m_nQuest_Code)
                    {
                        for (int j = 0; j < m_gContent_Quest_Content_Left_Content_QuestList_Recommend.transform.childCount; j++)
                        {
                            if (m_gContent_Quest_Content_Left_Content_QuestList_Recommend.transform.GetChild(j).name == "QuestContent_" + quest.m_sQuest_Title)
                            {
                                Destroy(m_gContent_Quest_Content_Left_Content_QuestList_Recommend.transform.GetChild(j).gameObject);

                                break;
                            }
                        }
                        m_gList_Quest_Recommend.RemoveAt(i);
                        m_nList_Quest_Recommend.RemoveAt(i);

                        GameObject copyquestcontent = Instantiate(m_gQuestContent);
                        copyquestcontent.GetComponent<questpanel>().SetQuestInformation(quest);

                        RectTransform contentpos = copyquestcontent.GetComponent<RectTransform>();
                        contentpos.SetParent(m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform);
                        contentpos.transform.localScale = new Vector3(1, 1, 1);
                        contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                        m_gList_Quest_Process.Add(copyquestcontent);
                        m_nList_Quest_Process.Add(quest.m_nQuest_Code);

                        break;
                    }
                }
            }
        }
        else if (num == 2)
        {
            for (int i = 0; i < m_gList_Quest_Process.Count; i++)
            {
                if (m_gList_Quest_Process[i].GetComponent<questpanel>().m_QuestType == E_QUEST_TYPE.ROLL)
                {
                    if (quest.m_nQuest_Code == m_gList_Quest_Process[i].GetComponent<questpanel>().m_Quest_ROLL.m_nQuest_Code)
                    {
                        for (int j = 0; j < m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform.childCount; j++)
                        {
                            if (m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform.GetChild(j).name == "QuestContent_" + quest.m_sQuest_Title)
                            {
                                Destroy(m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform.GetChild(j).gameObject);

                                break;
                            }
                        }
                        m_gList_Quest_Process.RemoveAt(i);
                        m_nList_Quest_Process.RemoveAt(i);

                        GameObject copyquestcontent = Instantiate(m_gQuestContent);
                        copyquestcontent.GetComponent<questpanel>().SetQuestInformation(quest);
                        copyquestcontent.GetComponent<questpanel>().UnEffect_Flesh();

                        RectTransform contentpos = copyquestcontent.GetComponent<RectTransform>();
                        contentpos.SetParent(m_gContent_Quest_Content_Left_Content_QuestList_Complete.transform);
                        contentpos.transform.localScale = new Vector3(1, 1, 1);
                        contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                        m_gList_Quest_Complete.Add(copyquestcontent);
                        m_nList_Quest_Complete.Add(quest.m_nQuest_Code);

                        break;
                    }
                }
            }
        }
    }
    public void UpdateQuest(Quest_ELIMINATE_MONSTER quest, int num)
    {
        if (num == 0)
        {
            GameObject copyquestcontent = Instantiate(m_gQuestContent);
            copyquestcontent.GetComponent<questpanel>().SetQuestInformation(quest);

            RectTransform contentpos = copyquestcontent.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
            m_gList_Quest_Process.Add(copyquestcontent);
            m_nList_Quest_Process.Add(quest.m_nQuest_Code);
        }
        else if (num == 1)
        {
            for (int i = 0; i < m_gList_Quest_Recommend.Count; i++)
            {
                if (m_gList_Quest_Recommend[i].GetComponent<questpanel>().m_QuestType == E_QUEST_TYPE.ELIMINATE_MONSTER)
                {
                    if (quest.m_nQuest_Code == m_gList_Quest_Recommend[i].GetComponent<questpanel>().m_Quest_ELIMINATE_MONSTER.m_nQuest_Code)
                    {
                        for (int j = 0; j < m_gContent_Quest_Content_Left_Content_QuestList_Recommend.transform.childCount; j++)
                        {
                            if (m_gContent_Quest_Content_Left_Content_QuestList_Recommend.transform.GetChild(j).name == "QuestContent_" + quest.m_sQuest_Title)
                            {
                                Destroy(m_gContent_Quest_Content_Left_Content_QuestList_Recommend.transform.GetChild(j).gameObject);

                                break;
                            }
                        }
                        m_gList_Quest_Recommend.RemoveAt(i);
                        m_nList_Quest_Recommend.RemoveAt(i);

                        GameObject copyquestcontent = Instantiate(m_gQuestContent);
                        copyquestcontent.GetComponent<questpanel>().SetQuestInformation(quest);

                        RectTransform contentpos = copyquestcontent.GetComponent<RectTransform>();
                        contentpos.SetParent(m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform);
                        contentpos.transform.localScale = new Vector3(1, 1, 1);
                        contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                        m_gList_Quest_Process.Add(copyquestcontent);
                        m_nList_Quest_Process.Add(quest.m_nQuest_Code);

                        break;
                    }
                }
            }
        }
        else if (num == 2)
        {
            for (int i = 0; i < m_gList_Quest_Process.Count; i++)
            {
                if (m_gList_Quest_Process[i].GetComponent<questpanel>().m_QuestType == E_QUEST_TYPE.ELIMINATE_MONSTER)
                {
                    if (quest.m_nQuest_Code == m_gList_Quest_Process[i].GetComponent<questpanel>().m_Quest_ELIMINATE_MONSTER.m_nQuest_Code)
                    {
                        for (int j = 0; j < m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform.childCount; j++)
                        {
                            if (m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform.GetChild(j).name == "QuestContent_" + quest.m_sQuest_Title)
                            {
                                Destroy(m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform.GetChild(j).gameObject);

                                break;
                            }
                        }
                        m_gList_Quest_Process.RemoveAt(i);
                        m_nList_Quest_Process.RemoveAt(i);

                        GameObject copyquestcontent = Instantiate(m_gQuestContent);
                        copyquestcontent.GetComponent<questpanel>().SetQuestInformation(quest);
                        copyquestcontent.GetComponent<questpanel>().UnEffect_Flesh();

                        RectTransform contentpos = copyquestcontent.GetComponent<RectTransform>();
                        contentpos.SetParent(m_gContent_Quest_Content_Left_Content_QuestList_Complete.transform);
                        contentpos.transform.localScale = new Vector3(1, 1, 1);
                        contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                        m_gList_Quest_Complete.Add(copyquestcontent);
                        m_nList_Quest_Complete.Add(quest.m_nQuest_Code);

                        break;
                    }
                }
            }
        }
    }
    public void UpdateQuest(Quest_ELIMINATE_TYPE quest, int num)
    {
        if (num == 0)
        {
            GameObject copyquestcontent = Instantiate(m_gQuestContent);
            copyquestcontent.GetComponent<questpanel>().SetQuestInformation(quest);

            RectTransform contentpos = copyquestcontent.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
            m_gList_Quest_Process.Add(copyquestcontent);
            m_nList_Quest_Process.Add(quest.m_nQuest_Code);
        }
        else if (num == 1)
        {
            for (int i = 0; i < m_gList_Quest_Recommend.Count; i++)
            {
                if (m_gList_Quest_Recommend[i].GetComponent<questpanel>().m_QuestType == E_QUEST_TYPE.ELIMINATE_TYPE)
                {
                    if (quest.m_nQuest_Code == m_gList_Quest_Recommend[i].GetComponent<questpanel>().m_Quest_ELIMINATE_TYPE.m_nQuest_Code)
                    {
                        for (int j = 0; j < m_gContent_Quest_Content_Left_Content_QuestList_Recommend.transform.childCount; j++)
                        {
                            if (m_gContent_Quest_Content_Left_Content_QuestList_Recommend.transform.GetChild(j).name == "QuestContent_" + quest.m_sQuest_Title)
                            {
                                Destroy(m_gContent_Quest_Content_Left_Content_QuestList_Recommend.transform.GetChild(j).gameObject);

                                break;
                            }
                        }
                        m_gList_Quest_Recommend.RemoveAt(i);
                        m_nList_Quest_Recommend.RemoveAt(i);

                        GameObject copyquestcontent = Instantiate(m_gQuestContent);
                        copyquestcontent.GetComponent<questpanel>().SetQuestInformation(quest);

                        RectTransform contentpos = copyquestcontent.GetComponent<RectTransform>();
                        contentpos.SetParent(m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform);
                        contentpos.transform.localScale = new Vector3(1, 1, 1);
                        contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                        m_gList_Quest_Process.Add(copyquestcontent);
                        m_nList_Quest_Process.Add(quest.m_nQuest_Code);

                        break;
                    }
                }
            }
        }
        else if (num == 2)
        {
            for (int i = 0; i < m_gList_Quest_Process.Count; i++)
            {
                if (m_gList_Quest_Process[i].GetComponent<questpanel>().m_QuestType == E_QUEST_TYPE.ELIMINATE_TYPE)
                {
                    if (quest.m_nQuest_Code == m_gList_Quest_Process[i].GetComponent<questpanel>().m_Quest_ELIMINATE_TYPE.m_nQuest_Code)
                    {
                        for (int j = 0; j < m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform.childCount; j++)
                        {
                            if (m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform.GetChild(j).name == "QuestContent_" + quest.m_sQuest_Title)
                            {
                                Destroy(m_gContent_Quest_Content_Left_Content_QuestList_Progress.transform.GetChild(j).gameObject);

                                break;
                            }
                        }
                        m_gList_Quest_Process.RemoveAt(i);
                        m_nList_Quest_Process.RemoveAt(i);

                        GameObject copyquestcontent = Instantiate(m_gQuestContent);
                        copyquestcontent.GetComponent<questpanel>().SetQuestInformation(quest);
                        copyquestcontent.GetComponent<questpanel>().UnEffect_Flesh();

                        RectTransform contentpos = copyquestcontent.GetComponent<RectTransform>();
                        contentpos.SetParent(m_gContent_Quest_Content_Left_Content_QuestList_Complete.transform);
                        contentpos.transform.localScale = new Vector3(1, 1, 1);
                        contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                        m_gList_Quest_Complete.Add(copyquestcontent);
                        m_nList_Quest_Complete.Add(quest.m_nQuest_Code);

                        break;
                    }
                }
            }
        }
    }


    // 추천 퀘스트(시작 가능 퀘스트) 출력
    // 게임 시작시 초기화
    public void UpdateQuest_Init()
    {
        foreach (KeyValuePair<int, Quest_KILL_MONSTER> quest in QuestManager.Instance.m_Dictionary_QuestList_KILL_MONSTER)
        {
            if (quest.Value.m_bProcess == false && quest.Value.m_bClear == false)
            {
                GameObject copyquestcontent = Instantiate(m_gQuestContent);
                copyquestcontent.GetComponent<questpanel>().SetQuestInformation(quest.Value);
                copyquestcontent.GetComponent<questpanel>().UnEffect_Flesh();

                RectTransform contentpos = copyquestcontent.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_Quest_Content_Left_Content_QuestList_Recommend.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                m_gList_Quest_Recommend.Add(copyquestcontent);
                m_nList_Quest_Recommend.Add(quest.Value.m_nQuest_Code);
            }
        }
        foreach (KeyValuePair<int, Quest_KILL_TYPE> quest in QuestManager.Instance.m_Dictionary_QuestList_KILL_TYPE)
        {
            if (quest.Value.m_bProcess == false && quest.Value.m_bClear == false)
            {
                GameObject copyquestcontent = Instantiate(m_gQuestContent);
                copyquestcontent.GetComponent<questpanel>().SetQuestInformation(quest.Value);
                copyquestcontent.GetComponent<questpanel>().UnEffect_Flesh();

                RectTransform contentpos = copyquestcontent.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_Quest_Content_Left_Content_QuestList_Recommend.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                m_gList_Quest_Recommend.Add(copyquestcontent);
                m_nList_Quest_Recommend.Add(quest.Value.m_nQuest_Code);
            }
            else
                continue;
        }
        foreach (KeyValuePair<int, Quest_GOAWAY_MONSTER> quest in QuestManager.Instance.m_Dictionary_QuestList_GOAWAY_MONSTER)
        {
            if (quest.Value.m_bProcess == false && quest.Value.m_bClear == false)
            {
                GameObject copyquestcontent = Instantiate(m_gQuestContent);
                copyquestcontent.GetComponent<questpanel>().SetQuestInformation(quest.Value);
                copyquestcontent.GetComponent<questpanel>().UnEffect_Flesh();

                RectTransform contentpos = copyquestcontent.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_Quest_Content_Left_Content_QuestList_Recommend.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                m_gList_Quest_Recommend.Add(copyquestcontent);
                m_nList_Quest_Recommend.Add(quest.Value.m_nQuest_Code);
            }
            else
                continue;
        }
        foreach (KeyValuePair<int, Quest_GOAWAY_TYPE> quest in QuestManager.Instance.m_Dictionary_QuestList_GOAWAY_TYPE)
        {
            if (quest.Value.m_bProcess == false && quest.Value.m_bClear == false)
            {
                GameObject copyquestcontent = Instantiate(m_gQuestContent);
                copyquestcontent.GetComponent<questpanel>().SetQuestInformation(quest.Value);
                copyquestcontent.GetComponent<questpanel>().UnEffect_Flesh();

                RectTransform contentpos = copyquestcontent.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_Quest_Content_Left_Content_QuestList_Recommend.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                m_gList_Quest_Recommend.Add(copyquestcontent);
                m_nList_Quest_Recommend.Add(quest.Value.m_nQuest_Code);
            }
            else
                continue;
        }
        foreach (KeyValuePair<int, Quest_COLLECT> quest in QuestManager.Instance.m_Dictionary_QuestList_COLLECT)
        {
            if (quest.Value.m_bProcess == false && quest.Value.m_bClear == false)
            {
                GameObject copyquestcontent = Instantiate(m_gQuestContent);
                copyquestcontent.GetComponent<questpanel>().SetQuestInformation(quest.Value);
                copyquestcontent.GetComponent<questpanel>().UnEffect_Flesh();

                RectTransform contentpos = copyquestcontent.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_Quest_Content_Left_Content_QuestList_Recommend.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                m_gList_Quest_Recommend.Add(copyquestcontent);
                m_nList_Quest_Recommend.Add(quest.Value.m_nQuest_Code);
            }
            else
                continue;
        }
        foreach (KeyValuePair<int, Quest_CONVERSATION> quest in QuestManager.Instance.m_Dictionary_QuestList_CONVERSATION)
        {
            if (quest.Value.m_bProcess == false && quest.Value.m_bClear == false)
            {
                GameObject copyquestcontent = Instantiate(m_gQuestContent);
                copyquestcontent.GetComponent<questpanel>().SetQuestInformation(quest.Value);
                copyquestcontent.GetComponent<questpanel>().UnEffect_Flesh();

                RectTransform contentpos = copyquestcontent.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_Quest_Content_Left_Content_QuestList_Recommend.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                m_gList_Quest_Recommend.Add(copyquestcontent);
                m_nList_Quest_Recommend.Add(quest.Value.m_nQuest_Code);
            }
            else
                continue;
        }
        foreach (KeyValuePair<int, Quest_ROLL> quest in QuestManager.Instance.m_Dictionary_QuestList_ROLL)
        {
            if (quest.Value.m_bProcess == false && quest.Value.m_bClear == false)
            {
                GameObject copyquestcontent = Instantiate(m_gQuestContent);
                copyquestcontent.GetComponent<questpanel>().SetQuestInformation(quest.Value);
                copyquestcontent.GetComponent<questpanel>().UnEffect_Flesh();

                RectTransform contentpos = copyquestcontent.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_Quest_Content_Left_Content_QuestList_Recommend.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                m_gList_Quest_Recommend.Add(copyquestcontent);
                m_nList_Quest_Recommend.Add(quest.Value.m_nQuest_Code);
            }
            else
                continue;
        }
        foreach (KeyValuePair<int, Quest_ELIMINATE_MONSTER> quest in QuestManager.Instance.m_Dictionary_QuestList_ELIMINATE_MONSTER)
        {
            if (quest.Value.m_bProcess == false && quest.Value.m_bClear == false)
            {
                GameObject copyquestcontent = Instantiate(m_gQuestContent);
                copyquestcontent.GetComponent<questpanel>().SetQuestInformation(quest.Value);
                copyquestcontent.GetComponent<questpanel>().UnEffect_Flesh();

                RectTransform contentpos = copyquestcontent.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_Quest_Content_Left_Content_QuestList_Recommend.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                m_gList_Quest_Recommend.Add(copyquestcontent);
                m_nList_Quest_Recommend.Add(quest.Value.m_nQuest_Code);
            }
            else
                continue;
        }
        foreach (KeyValuePair<int, Quest_ELIMINATE_TYPE> quest in QuestManager.Instance.m_Dictionary_QuestList_ELIMINATE_TYPE)
        {
            if (quest.Value.m_bProcess == false && quest.Value.m_bClear == false)
            {
                GameObject copyquestcontent = Instantiate(m_gQuestContent);
                copyquestcontent.GetComponent<questpanel>().SetQuestInformation(quest.Value);
                copyquestcontent.GetComponent<questpanel>().UnEffect_Flesh();

                RectTransform contentpos = copyquestcontent.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_Quest_Content_Left_Content_QuestList_Recommend.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                m_gList_Quest_Recommend.Add(copyquestcontent);
                m_nList_Quest_Recommend.Add(quest.Value.m_nQuest_Code);
            }
            else
                continue;
        }
    }
    // 씬 전환시 Quest 정보 초기화.
    public void UpdateQuest_SceneChange()
    {
        for (int i = 0; i < m_gList_Quest_Recommend.Count; i++)
        {
            switch (m_gList_Quest_Recommend[i].GetComponent<questpanel>().m_QuestType)
            {
                case E_QUEST_TYPE.KILL_MONSTER:
                    {
                        m_gList_Quest_Recommend[i].GetComponent<questpanel>().m_Quest_KILL_MONSTER = QuestManager.Instance.GetQuest_KILL_MONSTER(m_nList_Quest_Recommend[i]);
                    }
                    break;
                case E_QUEST_TYPE.KILL_TYPE:
                    {
                        m_gList_Quest_Recommend[i].GetComponent<questpanel>().m_Quest_KILL_TYPE = QuestManager.Instance.GetQuest_KILL_TYPE(m_nList_Quest_Recommend[i]);
                    }
                    break;
                case E_QUEST_TYPE.GOAWAY_MONSTER:
                    {
                        m_gList_Quest_Recommend[i].GetComponent<questpanel>().m_Quest_GOAWAY_MONSTER = QuestManager.Instance.GetQuest_GOAWAY_MONSTER(m_nList_Quest_Recommend[i]);
                    }
                    break;
                case E_QUEST_TYPE.GOAWAY_TYPE:
                    {
                        m_gList_Quest_Recommend[i].GetComponent<questpanel>().m_Quest_GOAWAY_TYPE = QuestManager.Instance.GetQuest_GOAWAY_TYPE(m_nList_Quest_Recommend[i]);
                    }
                    break;
                case E_QUEST_TYPE.COLLECT:
                    {
                        m_gList_Quest_Recommend[i].GetComponent<questpanel>().m_Quest_COLLECT = QuestManager.Instance.GetQuest_COLLECT(m_nList_Quest_Recommend[i]);
                    }
                    break;
                case E_QUEST_TYPE.CONVERSATION:
                    {
                        m_gList_Quest_Recommend[i].GetComponent<questpanel>().m_Quest_CONVERSATION = QuestManager.Instance.GetQuest_CONVERSATION(m_nList_Quest_Recommend[i]);
                    }
                    break;
                case E_QUEST_TYPE.ROLL:
                    {
                        m_gList_Quest_Recommend[i].GetComponent<questpanel>().m_Quest_ROLL = QuestManager.Instance.GetQuest_ROLL(m_nList_Quest_Recommend[i]);
                    }
                    break;
                case E_QUEST_TYPE.ELIMINATE_MONSTER:
                    {
                        m_gList_Quest_Recommend[i].GetComponent<questpanel>().m_Quest_ELIMINATE_MONSTER = QuestManager.Instance.GetQuest_ELIMINATE_MONSTER(m_nList_Quest_Recommend[i]);
                    }
                    break;
                case E_QUEST_TYPE.ELIMINATE_TYPE:
                    {
                        m_gList_Quest_Recommend[i].GetComponent<questpanel>().m_Quest_ELIMINATE_TYPE = QuestManager.Instance.GetQuest_ELIMINATE_TYPE(m_nList_Quest_Recommend[i]);
                    }
                    break;
            }
        }
        for (int i = 0; i < m_gList_Quest_Process.Count; i++)
        {
            switch(m_gList_Quest_Process[i].GetComponent<questpanel>().m_QuestType)
            {
                case E_QUEST_TYPE.KILL_MONSTER:
                    {
                        m_gList_Quest_Process[i].GetComponent<questpanel>().m_Quest_KILL_MONSTER = QuestManager.Instance.GetQuest_KILL_MONSTER(m_nList_Quest_Process[i]);
                    }
                    break;
                case E_QUEST_TYPE.KILL_TYPE:
                    {
                        m_gList_Quest_Process[i].GetComponent<questpanel>().m_Quest_KILL_TYPE = QuestManager.Instance.GetQuest_KILL_TYPE(m_nList_Quest_Process[i]);
                    }
                    break;
                case E_QUEST_TYPE.GOAWAY_MONSTER:
                    {
                        m_gList_Quest_Process[i].GetComponent<questpanel>().m_Quest_GOAWAY_MONSTER = QuestManager.Instance.GetQuest_GOAWAY_MONSTER(m_nList_Quest_Process[i]);
                    }
                    break;
                case E_QUEST_TYPE.GOAWAY_TYPE:
                    {
                        m_gList_Quest_Process[i].GetComponent<questpanel>().m_Quest_GOAWAY_TYPE = QuestManager.Instance.GetQuest_GOAWAY_TYPE(m_nList_Quest_Process[i]);
                    }
                    break;
                case E_QUEST_TYPE.COLLECT:
                    {
                        m_gList_Quest_Process[i].GetComponent<questpanel>().m_Quest_COLLECT = QuestManager.Instance.GetQuest_COLLECT(m_nList_Quest_Process[i]);
                    }
                    break;
                case E_QUEST_TYPE.CONVERSATION:
                    {
                        m_gList_Quest_Process[i].GetComponent<questpanel>().m_Quest_CONVERSATION = QuestManager.Instance.GetQuest_CONVERSATION(m_nList_Quest_Process[i]);
                    }
                    break;
                case E_QUEST_TYPE.ROLL:
                    {
                        m_gList_Quest_Process[i].GetComponent<questpanel>().m_Quest_ROLL = QuestManager.Instance.GetQuest_ROLL(m_nList_Quest_Process[i]);
                    }
                    break;
                case E_QUEST_TYPE.ELIMINATE_MONSTER:
                    {
                        m_gList_Quest_Process[i].GetComponent<questpanel>().m_Quest_ELIMINATE_MONSTER = QuestManager.Instance.GetQuest_ELIMINATE_MONSTER(m_nList_Quest_Process[i]);
                    }
                    break;
                case E_QUEST_TYPE.ELIMINATE_TYPE:
                    {
                        m_gList_Quest_Process[i].GetComponent<questpanel>().m_Quest_ELIMINATE_TYPE = QuestManager.Instance.GetQuest_ELIMINATE_TYPE(m_nList_Quest_Process[i]);
                    }
                    break;
            }
        }
        for (int i = 0; i < m_gList_Quest_Complete.Count; i++)
        {
            switch (m_gList_Quest_Complete[i].GetComponent<questpanel>().m_QuestType)
            {
                case E_QUEST_TYPE.KILL_MONSTER:
                    {
                        m_gList_Quest_Complete[i].GetComponent<questpanel>().m_Quest_KILL_MONSTER = QuestManager.Instance.GetQuest_KILL_MONSTER(m_nList_Quest_Complete[i]);
                    }
                    break;
                case E_QUEST_TYPE.KILL_TYPE:
                    {
                        m_gList_Quest_Complete[i].GetComponent<questpanel>().m_Quest_KILL_TYPE = QuestManager.Instance.GetQuest_KILL_TYPE(m_nList_Quest_Complete[i]);
                    }
                    break;
                case E_QUEST_TYPE.GOAWAY_MONSTER:
                    {
                        m_gList_Quest_Complete[i].GetComponent<questpanel>().m_Quest_GOAWAY_MONSTER = QuestManager.Instance.GetQuest_GOAWAY_MONSTER(m_nList_Quest_Complete[i]);
                    }
                    break;
                case E_QUEST_TYPE.GOAWAY_TYPE:
                    {
                        m_gList_Quest_Complete[i].GetComponent<questpanel>().m_Quest_GOAWAY_TYPE = QuestManager.Instance.GetQuest_GOAWAY_TYPE(m_nList_Quest_Complete[i]);
                    }
                    break;
                case E_QUEST_TYPE.COLLECT:
                    {
                        m_gList_Quest_Complete[i].GetComponent<questpanel>().m_Quest_COLLECT = QuestManager.Instance.GetQuest_COLLECT(m_nList_Quest_Complete[i]);
                    }
                    break;
                case E_QUEST_TYPE.CONVERSATION:
                    {
                        m_gList_Quest_Complete[i].GetComponent<questpanel>().m_Quest_CONVERSATION = QuestManager.Instance.GetQuest_CONVERSATION(m_nList_Quest_Complete[i]);
                    }
                    break;
                case E_QUEST_TYPE.ROLL:
                    {
                        m_gList_Quest_Complete[i].GetComponent<questpanel>().m_Quest_ROLL = QuestManager.Instance.GetQuest_ROLL(m_nList_Quest_Complete[i]);
                    }
                    break;
                case E_QUEST_TYPE.ELIMINATE_MONSTER:
                    {
                        m_gList_Quest_Complete[i].GetComponent<questpanel>().m_Quest_ELIMINATE_MONSTER = QuestManager.Instance.GetQuest_ELIMINATE_MONSTER(m_nList_Quest_Complete[i]);
                    }
                    break;
                case E_QUEST_TYPE.ELIMINATE_TYPE:
                    {
                        m_gList_Quest_Complete[i].GetComponent<questpanel>().m_Quest_ELIMINATE_TYPE = QuestManager.Instance.GetQuest_ELIMINATE_TYPE(m_nList_Quest_Complete[i]);
                    }
                    break;
            }
        }
    }

    // Quest_Information UI 업데이트.
    // num == 0: 추천 Quest
    // num == 1: 진행중인 Quest
    // num == 2: 완료한 Quest
    // 어떤 몹을 몇마리 잡았는지까지 세부적으로 표시해주자. - ADD!!
    public void UpdateQuestInformation(Quest_KILL_MONSTER quest, int num)
    {
        UpdateQuestInformation_Info(quest);

        if (num == 0)
        {
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text = quest.m_sQuest_Information_Recommend;
        }
        else if (num == 1)
        {
            if (quest.m_bCondition == false)
            {
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text = string.Empty;
                for (int i = 0; i < quest.m_sl_QuestDescription_Context.Count; i++)
                {
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_sl_QuestDescription_Context[i];
                }
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_sl_QuestOk_Context[0];
            }
            else
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text = quest.m_sQuest_Information_Condition;

            if (quest.m_bQuest_Information_Process_Hide == false)
            {
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n\n----------------------------------------------\n\n";
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "토벌 현황: \n";
                for (int i = 0; i < quest.m_nl_MonsterCode.Count; i++)
                {
                    if (i != 0)
                        m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ", \n";
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "[" + MonsterManager.m_Dictionary_Monster[quest.m_nl_MonsterCode[i]].m_sMonster_Name + "]" + quest.m_nl_Count_Current[i] + " / " + quest.m_nl_Count_Max[i];
                }
            }
            else
            {
                if (quest.m_bCondition == true)
                {
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n\n----------------------------------------------\n\n";
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "토벌 현황: \n";
                    for (int i = 0; i < quest.m_nl_MonsterCode.Count; i++)
                    {
                        if (i != 0)
                            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ", \n";
                        m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "[" + MonsterManager.m_Dictionary_Monster[quest.m_nl_MonsterCode[i]].m_sMonster_Name + "]" + quest.m_nl_Count_Current[i] + " / " + quest.m_nl_Count_Max[i];
                    }
                }
                else
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n???";
            }
        }
        else
        {
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text = string.Empty;
            for (int i = 0; i < quest.m_sl_QuestDescription_Context.Count; i++)
            {
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_sl_QuestDescription_Context[i];
            }
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n\n----------------------------------------------\n\n";
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_sQuest_Information_Clear;

            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n\n----------------------------------------------\n\n";
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "토벌 현황: \n";
            for (int i = 0; i < quest.m_nl_MonsterCode.Count; i++)
            {
                if (i != 0)
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ", \n";
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "[" + MonsterManager.m_Dictionary_Monster[quest.m_nl_MonsterCode[i]].m_sMonster_Name + "]" + quest.m_nl_Count_Current[i] + " / " + quest.m_nl_Count_Max[i];
            }
        }

        m_IMG_Quest_Content_Right_Content_QuestInfo_NPC.sprite = NPCManager_Total.m_Dictionary_NPC[quest.m_nNPC_Clear].m_Sprite_NPC;

        UpdateQuestInformation_Condition(quest);

        UpdateQuestInformation_Reward(quest);

        Check_Btn_Quest_Info();
    }
    public void UpdateQuestInformation(Quest_KILL_TYPE quest, int num)
    {
        UpdateQuestInformation_Info(quest);

        if (num == 0)
        {
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text = quest.m_sQuest_Information_Recommend;
        }
        else if (num == 1)
        {
            if (quest.m_bCondition == false)
            {
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text = string.Empty;
                for (int i = 0; i < quest.m_sl_QuestDescription_Context.Count; i++)
                {
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_sl_QuestDescription_Context[i];
                }
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_sl_QuestOk_Context[0];
            }
            else
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text = quest.m_sQuest_Information_Condition;

            if (quest.m_bQuest_Information_Process_Hide == false)
            {
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n\n----------------------------------------------\n\n";
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "토벌 현황: ";
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "[" + quest.m_eMonsterType + "]" + quest.m_nCount_Current + " / " + quest.m_nCount_Max;
            }
            else
            {
                if (quest.m_bCondition == true)
                {
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n\n----------------------------------------------\n\n";
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "토벌 현황: ";
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "[" + quest.m_eMonsterType + "]" + quest.m_nCount_Current + " / " + quest.m_nCount_Max;
                }
                else
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n???";
            }
        }
        else
        {
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text = string.Empty;
            for (int i = 0; i < quest.m_sl_QuestDescription_Context.Count; i++)
            {
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_sl_QuestDescription_Context[i];
            }
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n\n----------------------------------------------\n\n";
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_sQuest_Information_Clear;

            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n\n----------------------------------------------\n\n";
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "토벌 현황: ";
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "[" + quest.m_eMonsterType + "]" + quest.m_nCount_Current + " / " + quest.m_nCount_Max;
        }

        m_IMG_Quest_Content_Right_Content_QuestInfo_NPC.sprite = NPCManager_Total.m_Dictionary_NPC[quest.m_nNPC].m_Sprite_NPC;

        UpdateQuestInformation_Condition(quest);

        UpdateQuestInformation_Reward(quest);

        Check_Btn_Quest_Info();
    }
    public void UpdateQuestInformation(Quest_GOAWAY_MONSTER quest, int num)
    {
        UpdateQuestInformation_Info(quest);

        if (num == 0)
        {
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text = quest.m_sQuest_Information_Recommend;
        }
        else if (num == 1)
        {
            if (quest.m_bCondition == false)
            {
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text = string.Empty;
                for (int i = 0; i < quest.m_sl_QuestDescription_Context.Count; i++)
                {
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_sl_QuestDescription_Context[i];
                }
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_sl_QuestOk_Context[0];
            }
            else
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text = quest.m_sQuest_Information_Condition;

            if (quest.m_bQuest_Information_Process_Hide == false)
            {
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n\n----------------------------------------------\n\n";
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "놓아주기 현황: \n";
                for (int i = 0; i < quest.m_nl_MonsterCode.Count; i++)
                {
                    if (i != 0)
                        m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ", \n";
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "[" + MonsterManager.m_Dictionary_Monster[quest.m_nl_MonsterCode[i]].m_sMonster_Name + "]" + quest.m_nl_Count_Current[i] + " / " + quest.m_nl_Count_Max[i];
                }
            }
            else
            {
                if (quest.m_bCondition == true)
                {
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n\n----------------------------------------------\n\n";
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "놓아주기 현황: \n";
                    for (int i = 0; i < quest.m_nl_MonsterCode.Count; i++)
                    {
                        if (i != 0)
                            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ", \n";
                        m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "[" + MonsterManager.m_Dictionary_Monster[quest.m_nl_MonsterCode[i]].m_sMonster_Name + "]" + quest.m_nl_Count_Current[i] + " / " + quest.m_nl_Count_Max[i];
                    }
                }
                else
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n???";
            }
        }
        else
        {
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text = string.Empty;
            for (int i = 0; i < quest.m_sl_QuestDescription_Context.Count; i++)
            {
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_sl_QuestDescription_Context[i];
            }
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n\n----------------------------------------------\n\n";
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_sQuest_Information_Clear;

            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n\n----------------------------------------------\n\n";
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "놓아주기 현황: \n";
            for (int i = 0; i < quest.m_nl_MonsterCode.Count; i++)
            {
                if (i != 0)
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ", \n";
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "[" + MonsterManager.m_Dictionary_Monster[quest.m_nl_MonsterCode[i]].m_sMonster_Name + "]" + quest.m_nl_Count_Current[i] + " / " + quest.m_nl_Count_Max[i];
            }
        }

        m_IMG_Quest_Content_Right_Content_QuestInfo_NPC.sprite = NPCManager_Total.m_Dictionary_NPC[quest.m_nNPC].m_Sprite_NPC;

        UpdateQuestInformation_Condition(quest);

        UpdateQuestInformation_Reward(quest);

        Check_Btn_Quest_Info();
    }
    public void UpdateQuestInformation(Quest_GOAWAY_TYPE quest, int num)
    {
        UpdateQuestInformation_Info(quest);

        if (num == 0)
        {
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text = quest.m_sQuest_Information_Recommend;
        }
        else if (num == 1)
        {
            if (quest.m_bCondition == false)
            {
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text = string.Empty;
                for (int i = 0; i < quest.m_sl_QuestDescription_Context.Count; i++)
                {
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_sl_QuestDescription_Context[i];
                }
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_sl_QuestOk_Context[0];
            }
            else
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text = quest.m_sQuest_Information_Condition;

            if (quest.m_bQuest_Information_Process_Hide == false)
            {
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n\n----------------------------------------------\n\n";
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "놓아주기 현황: ";
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "[" + quest.m_eMonsterType + "]" + quest.m_nCount_Current + " / " + quest.m_nCount_Max;
            }
            else
            {
                if (quest.m_bCondition == true)
                {
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n\n----------------------------------------------\n\n";
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "놓아주기 현황: ";
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "[" + quest.m_eMonsterType + "]" + quest.m_nCount_Current + " / " + quest.m_nCount_Max;
                }
                else
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n???";
            }
        }
        else
        {
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text = string.Empty;
            for (int i = 0; i < quest.m_sl_QuestDescription_Context.Count; i++)
            {
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_sl_QuestDescription_Context[i];
            }
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n\n----------------------------------------------\n\n";
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_sQuest_Information_Clear;

            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n\n----------------------------------------------\n\n";
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "놓아주기 현황: ";
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "[" + quest.m_eMonsterType + "]" + quest.m_nCount_Current + " / " + quest.m_nCount_Max;
        }

        m_IMG_Quest_Content_Right_Content_QuestInfo_NPC.sprite = NPCManager_Total.m_Dictionary_NPC[quest.m_nNPC].m_Sprite_NPC;

        UpdateQuestInformation_Condition(quest);

        UpdateQuestInformation_Reward(quest);

        Check_Btn_Quest_Info();
    }
    public void UpdateQuestInformation(Quest_COLLECT quest, int num)
    {
        UpdateQuestInformation_Info(quest);

        if (num == 0)
        {
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text = quest.m_sQuest_Information_Recommend;
        }
        else if (num == 1)
        {
            if (quest.m_bCondition == false)
            {
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text = string.Empty;
                for (int i = 0; i < quest.m_sl_QuestDescription_Context.Count; i++)
                {
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_sl_QuestDescription_Context[i];
                }
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_sl_QuestOk_Context[0];
            }
            else
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text = quest.m_sQuest_Information_Condition;

            if (quest.m_bQuest_Information_Process_Hide == false)
            {
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n\n----------------------------------------------\n\n";
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "수집 현황: \n";
                for (int i = 0; i < quest.m_nl_ItemCode.Count; i++)
                {
                    if (i != 0)
                        m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ", \n";
                    // m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "[" + quest.m_nl_ItemCode[i] + "]" + quest.m_nl_ItemCount_Current[0] + " / " + quest.m_nl_ItemCount_Max[0];
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "[";
                    if (quest.m_nl_ItemCode[i] < 1000)
                    {
                        if (ItemManager.instance.m_Dictionary_MonsterDrop_Etc.ContainsKey(quest.m_nl_ItemCode[i]) == true)
                        {
                            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ItemManager.instance.m_Dictionary_MonsterDrop_Etc[quest.m_nl_ItemCode[i]].m_sItemName;
                        }
                        else if (ItemManager.instance.m_Dictionary_Collection_Etc.ContainsKey(quest.m_nl_ItemCode[i]) == true)
                        {
                            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ItemManager.instance.m_Dictionary_Collection_Etc[quest.m_nl_ItemCode[i]].m_sItemName;
                        }
                        else if (ItemManager.instance.m_Dictionary_QuestReward_Etc.ContainsKey(quest.m_nl_ItemCode[i]) == true)
                        {
                            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ItemManager.instance.m_Dictionary_QuestReward_Etc[quest.m_nl_ItemCode[i]].m_sItemName;
                        }
                    }
                    else if (quest.m_nl_ItemCode[i] < 7000)
                    {
                        if (ItemManager.instance.m_Dictionary_MonsterDrop_Equip.ContainsKey(quest.m_nl_ItemCode[i]) == true)
                        {
                            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ItemManager.instance.m_Dictionary_MonsterDrop_Equip[quest.m_nl_ItemCode[i]].m_sItemName;
                        }
                        else if (ItemManager.instance.m_Dictionary_Collection_Equip.ContainsKey(quest.m_nl_ItemCode[i]) == true)
                        {
                            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ItemManager.instance.m_Dictionary_Collection_Equip[quest.m_nl_ItemCode[i]].m_sItemName;
                        }
                        else if (ItemManager.instance.m_Dictionary_QuestReward_Equip.ContainsKey(quest.m_nl_ItemCode[i]) == true)
                        {
                            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ItemManager.instance.m_Dictionary_QuestReward_Equip[quest.m_nl_ItemCode[i]].m_sItemName;
                        }
                    }
                    else
                    {
                        if (ItemManager.instance.m_Dictionary_MonsterDrop_Use.ContainsKey(quest.m_nl_ItemCode[i]) == true)
                        {
                            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ItemManager.instance.m_Dictionary_MonsterDrop_Use[quest.m_nl_ItemCode[i]].m_sItemName;
                        }
                        else if (ItemManager.instance.m_Dictionary_Collection_Use.ContainsKey(quest.m_nl_ItemCode[i]) == true)
                        {
                            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ItemManager.instance.m_Dictionary_Collection_Use[quest.m_nl_ItemCode[i]].m_sItemName;
                        }
                        else if (ItemManager.instance.m_Dictionary_QuestReward_Use.ContainsKey(quest.m_nl_ItemCode[i]) == true)
                        {
                            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ItemManager.instance.m_Dictionary_QuestReward_Use[quest.m_nl_ItemCode[i]].m_sItemName;
                        }
                    }
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "]" + quest.m_nl_ItemCount_Current[i] + " / " + quest.m_nl_ItemCount_Max[i];
                }
            }
            else
            {
                if (quest.m_bCondition == true)
                {
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n\n----------------------------------------------\n\n";
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "수집 현황: \n";
                    for (int i = 0; i < quest.m_nl_ItemCode.Count; i++)
                    {
                        if (i != 0)
                            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ", \n";
                        // m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "[" + quest.m_nl_ItemCode[i] + "]" + quest.m_nl_ItemCount_Current[0] + " / " + quest.m_nl_ItemCount_Max[0];
                        m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "[";
                        if (quest.m_nl_ItemCode[i] < 1000)
                        {
                            if (ItemManager.instance.m_Dictionary_MonsterDrop_Etc.ContainsKey(quest.m_nl_ItemCode[i]) == true)
                            {
                                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ItemManager.instance.m_Dictionary_MonsterDrop_Etc[quest.m_nl_ItemCode[i]].m_sItemName;
                            }
                            else if (ItemManager.instance.m_Dictionary_Collection_Etc.ContainsKey(quest.m_nl_ItemCode[i]) == true)
                            {
                                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ItemManager.instance.m_Dictionary_Collection_Etc[quest.m_nl_ItemCode[i]].m_sItemName;
                            }
                            else if (ItemManager.instance.m_Dictionary_QuestReward_Etc.ContainsKey(quest.m_nl_ItemCode[i]) == true)
                            {
                                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ItemManager.instance.m_Dictionary_QuestReward_Etc[quest.m_nl_ItemCode[i]].m_sItemName;
                            }
                        }
                        else if (quest.m_nl_ItemCode[i] < 7000)
                        {
                            if (ItemManager.instance.m_Dictionary_MonsterDrop_Equip.ContainsKey(quest.m_nl_ItemCode[i]) == true)
                            {
                                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ItemManager.instance.m_Dictionary_MonsterDrop_Equip[quest.m_nl_ItemCode[i]].m_sItemName;
                            }
                            else if (ItemManager.instance.m_Dictionary_Collection_Equip.ContainsKey(quest.m_nl_ItemCode[i]) == true)
                            {
                                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ItemManager.instance.m_Dictionary_Collection_Equip[quest.m_nl_ItemCode[i]].m_sItemName;
                            }
                            else if (ItemManager.instance.m_Dictionary_QuestReward_Equip.ContainsKey(quest.m_nl_ItemCode[i]) == true)
                            {
                                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ItemManager.instance.m_Dictionary_QuestReward_Equip[quest.m_nl_ItemCode[i]].m_sItemName;
                            }
                        }
                        else
                        {
                            if (ItemManager.instance.m_Dictionary_MonsterDrop_Use.ContainsKey(quest.m_nl_ItemCode[i]) == true)
                            {
                                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ItemManager.instance.m_Dictionary_MonsterDrop_Use[quest.m_nl_ItemCode[i]].m_sItemName;
                            }
                            else if (ItemManager.instance.m_Dictionary_Collection_Use.ContainsKey(quest.m_nl_ItemCode[i]) == true)
                            {
                                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ItemManager.instance.m_Dictionary_Collection_Use[quest.m_nl_ItemCode[i]].m_sItemName;
                            }
                            else if (ItemManager.instance.m_Dictionary_QuestReward_Use.ContainsKey(quest.m_nl_ItemCode[i]) == true)
                            {
                                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ItemManager.instance.m_Dictionary_QuestReward_Use[quest.m_nl_ItemCode[i]].m_sItemName;
                            }
                        }
                        m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "]" + quest.m_nl_ItemCount_Current[i] + " / " + quest.m_nl_ItemCount_Max[i];
                    }
                }
                else
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n???";
            }
        }
        else
        {
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text = string.Empty;
            for (int i = 0; i < quest.m_sl_QuestDescription_Context.Count; i++)
            {
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_sl_QuestDescription_Context[i];
            }
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n\n----------------------------------------------\n\n";
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_sQuest_Information_Clear;

            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n\n----------------------------------------------\n\n";
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "수집 현황: \n";
            for (int i = 0; i < quest.m_nl_ItemCode.Count; i++)
            {
                if (i != 0)
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ", \n";
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "[";
                if (quest.m_nl_ItemCode[i] < 1000)
                {
                    if (ItemManager.instance.m_Dictionary_MonsterDrop_Etc.ContainsKey(quest.m_nl_ItemCode[i]) == true)
                    {
                        m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ItemManager.instance.m_Dictionary_MonsterDrop_Etc[quest.m_nl_ItemCode[i]].m_sItemName;
                    }
                    else if (ItemManager.instance.m_Dictionary_Collection_Etc.ContainsKey(quest.m_nl_ItemCode[i]) == true)
                    {
                        m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ItemManager.instance.m_Dictionary_Collection_Etc[quest.m_nl_ItemCode[i]].m_sItemName;
                    }
                    else if (ItemManager.instance.m_Dictionary_QuestReward_Etc.ContainsKey(quest.m_nl_ItemCode[i]) == true)
                    {
                        m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ItemManager.instance.m_Dictionary_QuestReward_Etc[quest.m_nl_ItemCode[i]].m_sItemName;
                    }
                }
                else if (quest.m_nl_ItemCode[i] < 7000)
                {
                    if (ItemManager.instance.m_Dictionary_MonsterDrop_Equip.ContainsKey(quest.m_nl_ItemCode[i]) == true)
                    {
                        m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ItemManager.instance.m_Dictionary_MonsterDrop_Equip[quest.m_nl_ItemCode[i]].m_sItemName;
                    }
                    else if (ItemManager.instance.m_Dictionary_Collection_Equip.ContainsKey(quest.m_nl_ItemCode[i]) == true)
                    {
                        m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ItemManager.instance.m_Dictionary_Collection_Equip[quest.m_nl_ItemCode[i]].m_sItemName;
                    }
                    else if (ItemManager.instance.m_Dictionary_QuestReward_Equip.ContainsKey(quest.m_nl_ItemCode[i]) == true)
                    {
                        m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ItemManager.instance.m_Dictionary_QuestReward_Equip[quest.m_nl_ItemCode[i]].m_sItemName;
                    }
                }
                else
                {
                    if (ItemManager.instance.m_Dictionary_MonsterDrop_Use.ContainsKey(quest.m_nl_ItemCode[i]) == true)
                    {
                        m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ItemManager.instance.m_Dictionary_MonsterDrop_Use[quest.m_nl_ItemCode[i]].m_sItemName;
                    }
                    else if (ItemManager.instance.m_Dictionary_Collection_Use.ContainsKey(quest.m_nl_ItemCode[i]) == true)
                    {
                        m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ItemManager.instance.m_Dictionary_Collection_Use[quest.m_nl_ItemCode[i]].m_sItemName;
                    }
                    else if (ItemManager.instance.m_Dictionary_QuestReward_Use.ContainsKey(quest.m_nl_ItemCode[i]) == true)
                    {
                        m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ItemManager.instance.m_Dictionary_QuestReward_Use[quest.m_nl_ItemCode[i]].m_sItemName;
                    }
                }
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "]" + quest.m_nl_ItemCount_Current[i] + " / " + quest.m_nl_ItemCount_Max[i];
            }
        }

        m_IMG_Quest_Content_Right_Content_QuestInfo_NPC.sprite = NPCManager_Total.m_Dictionary_NPC[quest.m_nNPC].m_Sprite_NPC;

        UpdateQuestInformation_Condition(quest);

        UpdateQuestInformation_Reward(quest);

        Check_Btn_Quest_Info();
    }
    public void UpdateQuestInformation(Quest_CONVERSATION quest, int num)
    {
        UpdateQuestInformation_Info(quest);

        if (num == 0)
        {
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text = quest.m_sQuest_Information_Recommend;
        }
        else if (num == 1)
        {
            if (quest.m_bCondition == false)
            {
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text = string.Empty;
                for (int i = 0; i < quest.m_sl_QuestDescription_Context.Count; i++)
                {
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_sl_QuestDescription_Context[i];
                }
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_sl_QuestOk_Context[0];
            }
            else
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text = quest.m_sQuest_Information_Condition;

            if (quest.m_bQuest_Information_Process_Hide == false)
            {
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n\n----------------------------------------------\n\n";
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "퀘스트 진행중";
            }
            else
            {
                if (quest.m_bCondition == true)
                {
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n\n----------------------------------------------\n\n";
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "퀘스트 진행중";
                }
                else
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n???";
            }
        }
        else
        {
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text = string.Empty;
            for (int i = 0; i < quest.m_sl_QuestDescription_Context.Count; i++)
            {
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_sl_QuestDescription_Context[i];
            }
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n\n----------------------------------------------\n\n";
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_sQuest_Information_Clear;

            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n\n----------------------------------------------\n\n";
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "퀘스트 완료";
        }

        m_IMG_Quest_Content_Right_Content_QuestInfo_NPC.sprite = NPCManager_Total.m_Dictionary_NPC[quest.m_nNPC].m_Sprite_NPC;

        UpdateQuestInformation_Condition(quest);

        UpdateQuestInformation_Reward(quest);

        Check_Btn_Quest_Info();
    }
    public void UpdateQuestInformation(Quest_ROLL quest, int num)
    {
        UpdateQuestInformation_Info(quest);

        if (num == 0)
        {
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text = quest.m_sQuest_Information_Recommend;
        }
        else if (num == 1)
        {
            if (quest.m_bCondition == false)
            {
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text = string.Empty;
                for (int i = 0; i < quest.m_sl_QuestDescription_Context.Count; i++)
                {
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_sl_QuestDescription_Context[i];
                }
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_sl_QuestOk_Context[0];
            }
            else
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text = quest.m_sQuest_Information_Condition;

            if (quest.m_bQuest_Information_Process_Hide == false)
            {
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n\n----------------------------------------------\n\n";
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "구르기 횟수: ";
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_nCount_Current + " / " + quest.m_nCount_Max;
            }
            else
            {
                if (quest.m_bCondition == true)
                {
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n\n----------------------------------------------\n\n";
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "구르기 횟수: ";
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_nCount_Current + " / " + quest.m_nCount_Max;
                }
                else
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text = "???";
            }
        }
        else
        {
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text = string.Empty;
            for (int i = 0; i < quest.m_sl_QuestDescription_Context.Count; i++)
            {
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_sl_QuestDescription_Context[i];
            }
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n\n----------------------------------------------\n\n";
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_sQuest_Information_Clear;

            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n\n----------------------------------------------\n\n";
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "구르기 횟수: ";
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_nCount_Current + " / " + quest.m_nCount_Max;
        }

        m_IMG_Quest_Content_Right_Content_QuestInfo_NPC.sprite = NPCManager_Total.m_Dictionary_NPC[quest.m_nNPC].m_Sprite_NPC;

        UpdateQuestInformation_Condition(quest);

        UpdateQuestInformation_Reward(quest);

        Check_Btn_Quest_Info();
    }
    public void UpdateQuestInformation(Quest_ELIMINATE_MONSTER quest, int num)
    {
        UpdateQuestInformation_Info(quest);

        if (num == 0)
        {
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text = quest.m_sQuest_Information_Recommend;
        }
        else if (num == 1)
        {
            if (quest.m_bCondition == false)
            {
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text = string.Empty;
                for (int i = 0; i < quest.m_sl_QuestDescription_Context.Count; i++)
                {
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_sl_QuestDescription_Context[i];
                }
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_sl_QuestOk_Context[0];
            }
            else
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text = quest.m_sQuest_Information_Condition;

            if (quest.m_bQuest_Information_Process_Hide == false)
            {
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n\n----------------------------------------------\n\n";
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "제거 현황: \n";
                for (int i = 0; i < quest.m_nl_MonsterCode.Count; i++)
                {
                    if (i != 0)
                        m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ", \n";
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "[" + MonsterManager.m_Dictionary_Monster[quest.m_nl_MonsterCode[i]].m_sMonster_Name + "]" + quest.m_nl_Count_Current[i] + " / " + quest.m_nl_Count_Max[i];
                }
            }
            else
            {
                if (quest.m_bCondition == true)
                {
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n\n----------------------------------------------\n\n";
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "제거 현황: \n";
                    for (int i = 0; i < quest.m_nl_MonsterCode.Count; i++)
                    {
                        if (i != 0)
                            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ", \n";
                        m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "[" + MonsterManager.m_Dictionary_Monster[quest.m_nl_MonsterCode[i]].m_sMonster_Name + "]" + quest.m_nl_Count_Current[i] + " / " + quest.m_nl_Count_Max[i];
                    }
                }
                else
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n???";
            }
        }
        else
        {
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text = string.Empty;
            for (int i = 0; i < quest.m_sl_QuestDescription_Context.Count; i++)
            {
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_sl_QuestDescription_Context[i];
            }
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n\n----------------------------------------------\n\n";
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_sQuest_Information_Clear;

            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n\n----------------------------------------------\n\n";
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "제거 현황: \n";
            for (int i = 0; i < quest.m_nl_MonsterCode.Count; i++)
            {
                if (i != 0)
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += ", \n";
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "[" + MonsterManager.m_Dictionary_Monster[quest.m_nl_MonsterCode[i]].m_sMonster_Name + "]" + quest.m_nl_Count_Current[i] + " / " + quest.m_nl_Count_Max[i];
            }
        }

        m_IMG_Quest_Content_Right_Content_QuestInfo_NPC.sprite = NPCManager_Total.m_Dictionary_NPC[quest.m_nNPC_Clear].m_Sprite_NPC;

        UpdateQuestInformation_Condition(quest);

        UpdateQuestInformation_Reward(quest);

        Check_Btn_Quest_Info();
    }
    public void UpdateQuestInformation(Quest_ELIMINATE_TYPE quest, int num)
    {
        UpdateQuestInformation_Info(quest);

        if (num == 0)
        {
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text = quest.m_sQuest_Information_Recommend;
        }
        else if (num == 1)
        {
            if (quest.m_bCondition == false)
            {
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text = string.Empty;
                for (int i = 0; i < quest.m_sl_QuestDescription_Context.Count; i++)
                {
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_sl_QuestDescription_Context[i];
                }
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_sl_QuestOk_Context[0];
            }
            else
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text = quest.m_sQuest_Information_Condition;

            if (quest.m_bQuest_Information_Process_Hide == false)
            {
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n\n----------------------------------------------\n\n";
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "제거 현황: ";
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "[" + quest.m_eMonsterType + "]" + quest.m_nCount_Current + " / " + quest.m_nCount_Max;
            }
            else
            {
                if (quest.m_bCondition == true)
                {
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n\n----------------------------------------------\n\n";
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "제거 현황: ";
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "[" + quest.m_eMonsterType + "]" + quest.m_nCount_Current + " / " + quest.m_nCount_Max;
                }
                else
                    m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text = "???";
            }
        }
        else
        {
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text = string.Empty;
            for (int i = 0; i < quest.m_sl_QuestDescription_Context.Count; i++)
            {
                m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_sl_QuestDescription_Context[i];
            }
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n\n----------------------------------------------\n\n";
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += quest.m_sQuest_Information_Clear;

            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "\n\n----------------------------------------------\n\n";
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "제거 현황: ";
            m_TMP_Quest_Content_Right_Content_QuestDescription_Content.text += "[" + quest.m_eMonsterType + "]" + quest.m_nCount_Current + " / " + quest.m_nCount_Max;
        }

        m_IMG_Quest_Content_Right_Content_QuestInfo_NPC.sprite = NPCManager_Total.m_Dictionary_NPC[quest.m_nNPC].m_Sprite_NPC;

        UpdateQuestInformation_Condition(quest);

        UpdateQuestInformation_Reward(quest);

        Check_Btn_Quest_Info();
    }

    void UpdateQuestInformation_Condition(Quest quest)
    {
        for (int i = 0; i < m_gList_Quest_Info_Condition.Count; i++)
        {
            Destroy(m_gList_Quest_Info_Condition[i]);
        }
        m_gList_Quest_Info_Condition.Clear();

        GameObject copyitemcontent; RectTransform contentpos;

        // 능력치 조건
        bool blogic;
        blogic = false;
        if (quest.m_sStatus_Necessity_Down.GetSTATUS_LV() != -10000 || quest.m_sStatus_Necessity_Up.GetSTATUS_LV() != 10000)
        {
            copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestCondition") as GameObject);
            copyitemcontent.GetComponent<Slot_QuestCondition>().Set_QuestCondition_Status_int("[능  력  치][레        벨]", quest.m_sStatus_Necessity_Down.GetSTATUS_LV(), quest.m_sStatus_Necessity_Up.GetSTATUS_LV(),
                Player_Total.Instance.m_ps_Status.m_sStatus.CheckCondition_MM_LV(quest.m_sStatus_Necessity_Down.GetSTATUS_LV(), quest.m_sStatus_Necessity_Up.GetSTATUS_LV()));

            contentpos = copyitemcontent.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Condition.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

            m_gList_Quest_Info_Condition.Add(copyitemcontent); blogic = true;
        }
        if (quest.m_sStatus_Necessity_Down.GetSTATUS_HP_Max() != -10000 || quest.m_sStatus_Necessity_Up.GetSTATUS_HP_Max() != 10000)
        {
            copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestCondition") as GameObject);
            copyitemcontent.GetComponent<Slot_QuestCondition>().Set_QuestCondition_Status_int("[능  력  치][최대체력]", quest.m_sStatus_Necessity_Down.GetSTATUS_HP_Max(), quest.m_sStatus_Necessity_Up.GetSTATUS_HP_Max(),
                Player_Total.Instance.m_ps_Status.m_sStatus.CheckCondition_MM_HP_Max(quest.m_sStatus_Necessity_Down.GetSTATUS_HP_Max(), quest.m_sStatus_Necessity_Up.GetSTATUS_HP_Max()));

            contentpos = copyitemcontent.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Condition.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

            m_gList_Quest_Info_Condition.Add(copyitemcontent); blogic = true;
        }
        if (quest.m_sStatus_Necessity_Down.GetSTATUS_MP_Max() != -10000 || quest.m_sStatus_Necessity_Up.GetSTATUS_MP_Max() != 10000)
        {
            copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestCondition") as GameObject);
            copyitemcontent.GetComponent<Slot_QuestCondition>().Set_QuestCondition_Status_int("[능  력  치][최대마나]", quest.m_sStatus_Necessity_Down.GetSTATUS_MP_Max(), quest.m_sStatus_Necessity_Up.GetSTATUS_MP_Max(),
                Player_Total.Instance.m_ps_Status.m_sStatus.CheckCondition_MM_MP_Max(quest.m_sStatus_Necessity_Down.GetSTATUS_MP_Max(), quest.m_sStatus_Necessity_Up.GetSTATUS_MP_Max()));

            contentpos = copyitemcontent.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Condition.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

            m_gList_Quest_Info_Condition.Add(copyitemcontent); blogic = true;
        }
        if (quest.m_sStatus_Necessity_Down.GetSTATUS_Damage_Total() != -10000 || quest.m_sStatus_Necessity_Up.GetSTATUS_Damage_Total() != 10000)
        {
            copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestCondition") as GameObject);
            copyitemcontent.GetComponent<Slot_QuestCondition>().Set_QuestCondition_Status_int("[능  력  치][데  미  지]", quest.m_sStatus_Necessity_Down.GetSTATUS_Damage_Total(), quest.m_sStatus_Necessity_Up.GetSTATUS_Damage_Total(),
                Player_Total.Instance.m_ps_Status.m_sStatus.CheckCondition_MM_Damage_Total(quest.m_sStatus_Necessity_Down.GetSTATUS_Damage_Total(), quest.m_sStatus_Necessity_Up.GetSTATUS_Damage_Total()));

            contentpos = copyitemcontent.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Condition.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

            m_gList_Quest_Info_Condition.Add(copyitemcontent); blogic = true;
        }
        if (quest.m_sStatus_Necessity_Down.GetSTATUS_Defence_Physical() != -10000 || quest.m_sStatus_Necessity_Up.GetSTATUS_Defence_Physical() != 10000)
        {
            copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestCondition") as GameObject);
            copyitemcontent.GetComponent<Slot_QuestCondition>().Set_QuestCondition_Status_int("[능  력  치][방  어  력]", quest.m_sStatus_Necessity_Down.GetSTATUS_Defence_Physical(), quest.m_sStatus_Necessity_Up.GetSTATUS_Defence_Physical(),
                Player_Total.Instance.m_ps_Status.m_sStatus.CheckCondition_MM_Defence_Phisical(quest.m_sStatus_Necessity_Down.GetSTATUS_Defence_Physical(), quest.m_sStatus_Necessity_Up.GetSTATUS_Defence_Physical()));

            contentpos = copyitemcontent.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Condition.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

            m_gList_Quest_Info_Condition.Add(copyitemcontent); blogic = true;
        }
        if (quest.m_sStatus_Necessity_Down.GetSTATUS_Speed() != -10000 || quest.m_sStatus_Necessity_Up.GetSTATUS_Speed() != 10000)
        {
            copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestCondition") as GameObject);
            copyitemcontent.GetComponent<Slot_QuestCondition>().Set_QuestCondition_Status_int("[능  력  치][이동속도]", quest.m_sStatus_Necessity_Down.GetSTATUS_Speed(), quest.m_sStatus_Necessity_Up.GetSTATUS_Speed(),
                Player_Total.Instance.m_ps_Status.m_sStatus.CheckCondition_MM_Speed(quest.m_sStatus_Necessity_Down.GetSTATUS_Speed(), quest.m_sStatus_Necessity_Up.GetSTATUS_Speed()));

            contentpos = copyitemcontent.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Condition.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

            m_gList_Quest_Info_Condition.Add(copyitemcontent); blogic = true;
        }
        if (quest.m_sStatus_Necessity_Down.GetSTATUS_AttackSpeed() != -10000 || quest.m_sStatus_Necessity_Up.GetSTATUS_AttackSpeed() != 10000)
        {
            copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestCondition") as GameObject);
            copyitemcontent.GetComponent<Slot_QuestCondition>().Set_QuestCondition_Status_float("[능  력  치][공격속도]", quest.m_sStatus_Necessity_Down.GetSTATUS_AttackSpeed(), quest.m_sStatus_Necessity_Up.GetSTATUS_AttackSpeed(),
                Player_Total.Instance.m_ps_Status.m_sStatus.CheckCondition_MM_AttackSpeed(quest.m_sStatus_Necessity_Down.GetSTATUS_AttackSpeed(), quest.m_sStatus_Necessity_Up.GetSTATUS_AttackSpeed()));

            contentpos = copyitemcontent.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Condition.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

            m_gList_Quest_Info_Condition.Add(copyitemcontent); blogic = true;
        }
        if (blogic == true)
        {
            copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestCondition") as GameObject);
            copyitemcontent.GetComponent<Slot_QuestCondition>().Set_QuestCondition_Status_Title();

            contentpos = copyitemcontent.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Condition.transform);
            contentpos.SetAsFirstSibling();
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

            m_gList_Quest_Info_Condition.Add(copyitemcontent);

            blogic = false;
        }
        // 평판 조건
        if (quest.m_sSoc_Necessity_Down.GetSOC_Honor() != -10000 || quest.m_sSoc_Necessity_Up.GetSOC_Honor() != 10000)
        {
            copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestCondition") as GameObject);
            copyitemcontent.GetComponent<Slot_QuestCondition>().Set_QuestCondition_Soc_int("[평        판][명        예]", quest.m_sSoc_Necessity_Down.GetSOC_Honor(), quest.m_sSoc_Necessity_Up.GetSOC_Honor(),
                Player_Total.Instance.m_ps_Status.m_sSoc.CheckCondition_MM_Honor(quest.m_sSoc_Necessity_Down.GetSOC_Honor(), quest.m_sSoc_Necessity_Up.GetSOC_Honor()));

            contentpos = copyitemcontent.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Condition.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

            m_gList_Quest_Info_Condition.Add(copyitemcontent); blogic = true;
        }
        if (quest.m_sSoc_Necessity_Down.GetSOC_Human() != -10000 || quest.m_sSoc_Necessity_Up.GetSOC_Human() != 10000)
        {
            copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestCondition") as GameObject);
            copyitemcontent.GetComponent<Slot_QuestCondition>().Set_QuestCondition_Soc_int("[평        판][인        간]", quest.m_sSoc_Necessity_Down.GetSOC_Human(), quest.m_sSoc_Necessity_Up.GetSOC_Human(),
                Player_Total.Instance.m_ps_Status.m_sSoc.CheckCondition_MM_Human(quest.m_sSoc_Necessity_Down.GetSOC_Human(), quest.m_sSoc_Necessity_Up.GetSOC_Human()));

            contentpos = copyitemcontent.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Condition.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

            m_gList_Quest_Info_Condition.Add(copyitemcontent); blogic = true;
        }
        if (quest.m_sSoc_Necessity_Down.GetSOC_Animal() != -10000 || quest.m_sSoc_Necessity_Up.GetSOC_Animal() != 10000)
        {
            copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestCondition") as GameObject);
            copyitemcontent.GetComponent<Slot_QuestCondition>().Set_QuestCondition_Soc_int("[평        판][동        물]", quest.m_sSoc_Necessity_Down.GetSOC_Animal(), quest.m_sSoc_Necessity_Up.GetSOC_Animal(),
                Player_Total.Instance.m_ps_Status.m_sSoc.CheckCondition_MM_Animal(quest.m_sSoc_Necessity_Down.GetSOC_Animal(), quest.m_sSoc_Necessity_Up.GetSOC_Animal()));

            contentpos = copyitemcontent.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Condition.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

            m_gList_Quest_Info_Condition.Add(copyitemcontent); blogic = true;
        }
        if (quest.m_sSoc_Necessity_Down.GetSOC_Slime() != -10000 || quest.m_sSoc_Necessity_Up.GetSOC_Slime() != 10000)
        {
            copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestCondition") as GameObject);
            copyitemcontent.GetComponent<Slot_QuestCondition>().Set_QuestCondition_Soc_int("[평        판][슬  라  임]", quest.m_sSoc_Necessity_Down.GetSOC_Slime(), quest.m_sSoc_Necessity_Up.GetSOC_Slime(),
                Player_Total.Instance.m_ps_Status.m_sSoc.CheckCondition_MM_Slime(quest.m_sSoc_Necessity_Down.GetSOC_Slime(), quest.m_sSoc_Necessity_Up.GetSOC_Slime()));

            contentpos = copyitemcontent.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Condition.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

            m_gList_Quest_Info_Condition.Add(copyitemcontent); blogic = true;
        }
        if (quest.m_sSoc_Necessity_Down.GetSOC_Skeleton() != -10000 || quest.m_sSoc_Necessity_Up.GetSOC_Skeleton() != 10000)
        {
            copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestCondition") as GameObject);
            copyitemcontent.GetComponent<Slot_QuestCondition>().Set_QuestCondition_Soc_int("[평        판][스켈레톤]", quest.m_sSoc_Necessity_Down.GetSOC_Skeleton(), quest.m_sSoc_Necessity_Up.GetSOC_Skeleton(),
                Player_Total.Instance.m_ps_Status.m_sSoc.CheckCondition_MM_Skeleton(quest.m_sSoc_Necessity_Down.GetSOC_Skeleton(), quest.m_sSoc_Necessity_Up.GetSOC_Skeleton()));

            contentpos = copyitemcontent.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Condition.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

            m_gList_Quest_Info_Condition.Add(copyitemcontent); blogic = true;
        }
        if (quest.m_sSoc_Necessity_Down.GetSOC_Ents() != -10000 || quest.m_sSoc_Necessity_Up.GetSOC_Ents() != 10000)
        {
            copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestCondition") as GameObject);
            copyitemcontent.GetComponent<Slot_QuestCondition>().Set_QuestCondition_Soc_int("[평        판][앤        트]", quest.m_sSoc_Necessity_Down.GetSOC_Ents(), quest.m_sSoc_Necessity_Up.GetSOC_Ents(),
                Player_Total.Instance.m_ps_Status.m_sSoc.CheckCondition_MM_Ents(quest.m_sSoc_Necessity_Down.GetSOC_Ents(), quest.m_sSoc_Necessity_Up.GetSOC_Ents()));

            contentpos = copyitemcontent.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Condition.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

            m_gList_Quest_Info_Condition.Add(copyitemcontent); blogic = true;
        }
        if (quest.m_sSoc_Necessity_Down.GetSOC_Devil() != -10000 || quest.m_sSoc_Necessity_Up.GetSOC_Devil() != 10000)
        {
            copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestCondition") as GameObject);
            copyitemcontent.GetComponent<Slot_QuestCondition>().Set_QuestCondition_Soc_int("[평        판][마        족]", quest.m_sSoc_Necessity_Down.GetSOC_Devil(), quest.m_sSoc_Necessity_Up.GetSOC_Devil(),
                Player_Total.Instance.m_ps_Status.m_sSoc.CheckCondition_MM_Devil(quest.m_sSoc_Necessity_Down.GetSOC_Devil(), quest.m_sSoc_Necessity_Up.GetSOC_Devil()));

            contentpos = copyitemcontent.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Condition.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

            m_gList_Quest_Info_Condition.Add(copyitemcontent); blogic = true;
        }
        if (quest.m_sSoc_Necessity_Down.GetSOC_Dragon() != -10000 || quest.m_sSoc_Necessity_Up.GetSOC_Dragon() != 10000)
        {
            copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestCondition") as GameObject);
            copyitemcontent.GetComponent<Slot_QuestCondition>().Set_QuestCondition_Soc_int("[평        판][용        족]", quest.m_sSoc_Necessity_Down.GetSOC_Dragon(), quest.m_sSoc_Necessity_Up.GetSOC_Dragon(),
                Player_Total.Instance.m_ps_Status.m_sSoc.CheckCondition_MM_Dragon(quest.m_sSoc_Necessity_Down.GetSOC_Dragon(), quest.m_sSoc_Necessity_Up.GetSOC_Dragon()));

            contentpos = copyitemcontent.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Condition.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

            m_gList_Quest_Info_Condition.Add(copyitemcontent); blogic = true;
        }
        if (quest.m_sSoc_Necessity_Down.GetSOC_Shadow() != -10000 || quest.m_sSoc_Necessity_Up.GetSOC_Shadow() != 10000)
        {
            copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestCondition") as GameObject);
            copyitemcontent.GetComponent<Slot_QuestCondition>().Set_QuestCondition_Soc_int("[평        판][어        둠]", quest.m_sSoc_Necessity_Down.GetSOC_Shadow(), quest.m_sSoc_Necessity_Up.GetSOC_Shadow(),
                Player_Total.Instance.m_ps_Status.m_sSoc.CheckCondition_MM_Shadow(quest.m_sSoc_Necessity_Down.GetSOC_Shadow(), quest.m_sSoc_Necessity_Up.GetSOC_Shadow()));

            contentpos = copyitemcontent.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Condition.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

            m_gList_Quest_Info_Condition.Add(copyitemcontent); blogic = true;
        }
        if (blogic == true)
        {
            copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestCondition") as GameObject);
            copyitemcontent.GetComponent<Slot_QuestCondition>().Set_QuestCondition_Soc_Title();

            contentpos = copyitemcontent.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Condition.transform);
            contentpos.SetAsFirstSibling();
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

            m_gList_Quest_Info_Condition.Add(copyitemcontent);

            blogic = false;
        }

        // 선행 완료 퀘스트
        if (quest.m_ql_Quest_Necessity_Clear.Count > 0)
        {
            copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestCondition") as GameObject);
            copyitemcontent.GetComponent<Slot_QuestCondition>().Set_QuestCondition_PreQuest("[필수 선행 완료 퀘스트]", true);
            contentpos = copyitemcontent.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Condition.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
            m_gList_Quest_Info_Condition.Add(copyitemcontent);

            for (int i = 0; i < quest.m_ql_Quest_Necessity_Clear.Count; i++)
            {
                copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestCondition") as GameObject);
                if (quest.m_ql_Quest_Necessity_Clear[i].m_bClear == true)
                    copyitemcontent.GetComponent<Slot_QuestCondition>().Set_QuestCondition_PreQuest(quest.m_ql_Quest_Necessity_Clear[i].m_sQuest_Title, true);
                else
                    copyitemcontent.GetComponent<Slot_QuestCondition>().Set_QuestCondition_PreQuest(quest.m_ql_Quest_Necessity_Clear[i].m_sQuest_Title, false);
                contentpos = copyitemcontent.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Condition.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                m_gList_Quest_Info_Condition.Add(copyitemcontent);
            }
        }
        // 선행 진행 퀘스트
        if (quest.m_ql_Quest_Necessity_Process.Count > 0)
        {
            copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestCondition") as GameObject);
            copyitemcontent.GetComponent<Slot_QuestCondition>().Set_QuestCondition_PreQuest("[필수 선행 진행 퀘스트]", true);
            contentpos = copyitemcontent.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Condition.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
            m_gList_Quest_Info_Condition.Add(copyitemcontent);

            for (int i = 0; i < quest.m_ql_Quest_Necessity_Process.Count; i++)
            {
                copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestCondition") as GameObject);
                if (quest.m_ql_Quest_Necessity_Process[i].m_bProcess == true)
                    copyitemcontent.GetComponent<Slot_QuestCondition>().Set_QuestCondition_PreQuest(quest.m_ql_Quest_Necessity_Process[i].m_sQuest_Title, true);
                else
                    copyitemcontent.GetComponent<Slot_QuestCondition>().Set_QuestCondition_PreQuest(quest.m_ql_Quest_Necessity_Process[i].m_sQuest_Title, false);
                contentpos = copyitemcontent.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Condition.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                m_gList_Quest_Info_Condition.Add(copyitemcontent);
            }
        }
        // 선행 미완료 퀘스트
        if (quest.m_ql_Quest_Necessity_NonClear.Count > 0)
        {
            copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestCondition") as GameObject);
            copyitemcontent.GetComponent<Slot_QuestCondition>().Set_QuestCondition_PreQuest("[필수 선행 미완료 퀘스트]", true);
            contentpos = copyitemcontent.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Condition.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
            m_gList_Quest_Info_Condition.Add(copyitemcontent);

            for (int i = 0; i < quest.m_ql_Quest_Necessity_NonClear.Count; i++)
            {
                copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestCondition") as GameObject);
                if (quest.m_ql_Quest_Necessity_NonClear[i].m_bClear == false)
                    copyitemcontent.GetComponent<Slot_QuestCondition>().Set_QuestCondition_PreQuest(quest.m_ql_Quest_Necessity_NonClear[i].m_sQuest_Title, true);
                else
                    copyitemcontent.GetComponent<Slot_QuestCondition>().Set_QuestCondition_PreQuest(quest.m_ql_Quest_Necessity_NonClear[i].m_sQuest_Title, false);
                contentpos = copyitemcontent.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Condition.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                m_gList_Quest_Info_Condition.Add(copyitemcontent);
            }
        }
        // 선행 미진행 퀘스트
        if (quest.m_ql_Quest_Necessity_NonProcess.Count > 0)
        {
            copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestCondition") as GameObject);
            copyitemcontent.GetComponent<Slot_QuestCondition>().Set_QuestCondition_PreQuest("[필수 선행 미진행 퀘스트]", true);
            contentpos = copyitemcontent.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Condition.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
            m_gList_Quest_Info_Condition.Add(copyitemcontent);

            for (int i = 0; i < quest.m_ql_Quest_Necessity_NonProcess.Count; i++)
            {
                copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestCondition") as GameObject);
                if (quest.m_ql_Quest_Necessity_NonProcess[i].m_bProcess == false)
                    copyitemcontent.GetComponent<Slot_QuestCondition>().Set_QuestCondition_PreQuest(quest.m_ql_Quest_Necessity_NonProcess[i].m_sQuest_Title, true);
                else
                    copyitemcontent.GetComponent<Slot_QuestCondition>().Set_QuestCondition_PreQuest(quest.m_ql_Quest_Necessity_NonProcess[i].m_sQuest_Title, false);
                contentpos = copyitemcontent.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Condition.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                m_gList_Quest_Info_Condition.Add(copyitemcontent);
            }
        }
    }
    void UpdateQuestInformation_Info(Quest quest)
    {
        var ary1 = quest.m_sQuest_Title.Split('\n');
        m_TMP_Quest_Content_Right_UpBar_Name.text = ary1[1];

        var ary2 = ary1[0].Split(' ');
        m_TMP_Quest_Content_Right_Content_QuestInfo_Info.text = "퀘스트 종류: " + ary2[1] + " 퀘스트\n";

        if (quest.m_bQuest_Information_Process_Hide == false)
        {
            switch (quest.m_eQuestType)
            {
                case E_QUEST_TYPE.KILL_MONSTER:
                    {
                        m_TMP_Quest_Content_Right_Content_QuestInfo_Info.text += "퀘스트 타입: 몬스터 토벌\n";
                    }
                    break;
                case E_QUEST_TYPE.KILL_TYPE:
                    {
                        m_TMP_Quest_Content_Right_Content_QuestInfo_Info.text += "퀘스트 타입: 종족 토벌\n";
                    }
                    break;
                case E_QUEST_TYPE.GOAWAY_MONSTER:
                    {
                        m_TMP_Quest_Content_Right_Content_QuestInfo_Info.text += "퀘스트 타입: 몬스터 놓아주기\n";
                    }
                    break;
                case E_QUEST_TYPE.GOAWAY_TYPE:
                    {
                        m_TMP_Quest_Content_Right_Content_QuestInfo_Info.text += "퀘스트 타입: 종족 놓아주기\n";
                    }
                    break;
                case E_QUEST_TYPE.COLLECT:
                    {
                        m_TMP_Quest_Content_Right_Content_QuestInfo_Info.text += "퀘스트 타입: 수집\n";
                    }
                    break;
                case E_QUEST_TYPE.CONVERSATION:
                    {
                        m_TMP_Quest_Content_Right_Content_QuestInfo_Info.text += "퀘스트 타입: 대화\n";
                    }
                    break;
                case E_QUEST_TYPE.ROLL:
                    {
                        m_TMP_Quest_Content_Right_Content_QuestInfo_Info.text += "퀘스트 타입: 구르기\n";
                    }
                    break;
                case E_QUEST_TYPE.ELIMINATE_MONSTER:
                    {
                        m_TMP_Quest_Content_Right_Content_QuestInfo_Info.text += "퀘스트 타입: 몬스터 토벌ㆍ놓아주기\n";
                    }
                    break;
                case E_QUEST_TYPE.ELIMINATE_TYPE:
                    {
                        m_TMP_Quest_Content_Right_Content_QuestInfo_Info.text += "퀘스트 타입: 종족 토벌ㆍ놓아주기\n";
                    }
                    break;
            }
        }
        else
            m_TMP_Quest_Content_Right_Content_QuestInfo_Info.text += "퀘스트 타입: ???\n";

        m_TMP_Quest_Content_Right_Content_QuestInfo_Info.text += "퀘스트 난이도: " + quest.m_eQuestLevel + "\n";

        if (NPCManager_Total.m_Dictionary_NPC[quest.m_nNPC].m_sNPCName.Contains("\n") == true)
        {
            var ary3 = NPCManager_Total.m_Dictionary_NPC[quest.m_nNPC].m_sNPCName.Split('\n');
            m_TMP_Quest_Content_Right_Content_QuestInfo_Info.text += "NPC: " + ary3[0] + " " + ary3[1];
        }
        else
            m_TMP_Quest_Content_Right_Content_QuestInfo_Info.text += "NPC: " + NPCManager_Total.m_Dictionary_NPC[quest.m_nNPC].m_sNPCName;
    }
    void UpdateQuestInformation_Reward(Quest quest)
    {
        for (int i = 0; i < m_gList_Quest_Info_Reward.Count; i++)
        {
            Destroy(m_gList_Quest_Info_Reward[i]);
        }
        m_gList_Quest_Info_Reward.Clear();
        if (quest.m_bQuest_Information_Process_Hide == false)
        {
            GameObject copyitemcontent; RectTransform contentpos;
            // 능력치 보상
            if (quest.m_sRewardSTATUS.GetSTATUS_EXP_Current() != 0)
            {
                copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestReward_Item") as GameObject);
                copyitemcontent.GetComponent<Slot_QuestReward_Item>().Set_Status_int("[능  력  치][경  험  치]", quest.m_sRewardSTATUS.GetSTATUS_EXP_Current());

                contentpos = copyitemcontent.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_Quest_Info_Reward.Add(copyitemcontent);
            }
            if (quest.m_sRewardSTATUS.GetSTATUS_HP_Max() != 0)
            {
                copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestReward_Item") as GameObject);
                copyitemcontent.GetComponent<Slot_QuestReward_Item>().Set_Status_int("[능  력  치][최대체력]", quest.m_sRewardSTATUS.GetSTATUS_HP_Max());

                contentpos = copyitemcontent.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_Quest_Info_Reward.Add(copyitemcontent);
            }
            if (quest.m_sRewardSTATUS.GetSTATUS_MP_Max() != 0)
            {
                copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestReward_Item") as GameObject);
                copyitemcontent.GetComponent<Slot_QuestReward_Item>().Set_Status_int("[능  력  치][최대마나]", quest.m_sRewardSTATUS.GetSTATUS_MP_Max());

                contentpos = copyitemcontent.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_Quest_Info_Reward.Add(copyitemcontent);
            }
            if (quest.m_sRewardSTATUS.GetSTATUS_Damage_Total() != 0)
            {
                copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestReward_Item") as GameObject);
                copyitemcontent.GetComponent<Slot_QuestReward_Item>().Set_Status_int("[능  력  치][데  미  지]", quest.m_sRewardSTATUS.GetSTATUS_Damage_Total());

                contentpos = copyitemcontent.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_Quest_Info_Reward.Add(copyitemcontent);
            }
            if (quest.m_sRewardSTATUS.GetSTATUS_Defence_Physical() != 0)
            {
                copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestReward_Item") as GameObject);
                copyitemcontent.GetComponent<Slot_QuestReward_Item>().Set_Status_int("[능  력  치][방  어  력]", quest.m_sRewardSTATUS.GetSTATUS_Defence_Physical());

                contentpos = copyitemcontent.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_Quest_Info_Reward.Add(copyitemcontent);
            }
            if (quest.m_sRewardSTATUS.GetSTATUS_Speed() != 0)
            {
                copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestReward_Item") as GameObject);
                copyitemcontent.GetComponent<Slot_QuestReward_Item>().Set_Status_int("[능  력  치][이동속도]", quest.m_sRewardSTATUS.GetSTATUS_Speed());

                contentpos = copyitemcontent.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_Quest_Info_Reward.Add(copyitemcontent);
            }
            if (quest.m_sRewardSTATUS.GetSTATUS_AttackSpeed() != 0)
            {
                copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestReward_Item") as GameObject);
                copyitemcontent.GetComponent<Slot_QuestReward_Item>().Set_Status_float("[능  력  치][공격속도]", quest.m_sRewardSTATUS.GetSTATUS_AttackSpeed());

                contentpos = copyitemcontent.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_Quest_Info_Reward.Add(copyitemcontent);
            }
            // 평판 보상
            if (quest.m_sRewardSOC.GetSOC_Honor() != 0)
            {
                copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestReward_Item") as GameObject);
                copyitemcontent.GetComponent<Slot_QuestReward_Item>().Set_Soc_int("[평        판][명        예]", quest.m_sRewardSOC.GetSOC_Honor());

                contentpos = copyitemcontent.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_Quest_Info_Reward.Add(copyitemcontent);
            }
            if (quest.m_sRewardSOC.GetSOC_Human() != 0)
            {
                copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestReward_Item") as GameObject);
                copyitemcontent.GetComponent<Slot_QuestReward_Item>().Set_Soc_int("[평        판][인        간]", quest.m_sRewardSOC.GetSOC_Human());

                contentpos = copyitemcontent.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_Quest_Info_Reward.Add(copyitemcontent);
            }
            if (quest.m_sRewardSOC.GetSOC_Animal() != 0)
            {
                copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestReward_Item") as GameObject);
                copyitemcontent.GetComponent<Slot_QuestReward_Item>().Set_Soc_int("[평        판][동        물]", quest.m_sRewardSOC.GetSOC_Animal());

                contentpos = copyitemcontent.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_Quest_Info_Reward.Add(copyitemcontent);
            }
            if (quest.m_sRewardSOC.GetSOC_Slime() != 0)
            {
                copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestReward_Item") as GameObject);
                copyitemcontent.GetComponent<Slot_QuestReward_Item>().Set_Soc_int("[평        판][슬  라  임]", quest.m_sRewardSOC.GetSOC_Slime());

                contentpos = copyitemcontent.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_Quest_Info_Reward.Add(copyitemcontent);
            }
            if (quest.m_sRewardSOC.GetSOC_Skeleton() != 0)
            {
                copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestReward_Item") as GameObject);
                copyitemcontent.GetComponent<Slot_QuestReward_Item>().Set_Soc_int("[평        판][스켈레톤]", quest.m_sRewardSOC.GetSOC_Skeleton());

                contentpos = copyitemcontent.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_Quest_Info_Reward.Add(copyitemcontent);
            }
            if (quest.m_sRewardSOC.GetSOC_Ents() != 0)
            {
                copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestReward_Item") as GameObject);
                copyitemcontent.GetComponent<Slot_QuestReward_Item>().Set_Soc_int("[평        판][앤        트]", quest.m_sRewardSOC.GetSOC_Ents());

                contentpos = copyitemcontent.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_Quest_Info_Reward.Add(copyitemcontent);
            }
            if (quest.m_sRewardSOC.GetSOC_Devil() != 0)
            {
                copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestReward_Item") as GameObject);
                copyitemcontent.GetComponent<Slot_QuestReward_Item>().Set_Soc_int("[평        판][마        족]", quest.m_sRewardSOC.GetSOC_Devil());

                contentpos = copyitemcontent.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_Quest_Info_Reward.Add(copyitemcontent);
            }
            if (quest.m_sRewardSOC.GetSOC_Dragon() != 0)
            {
                copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestReward_Item") as GameObject);
                copyitemcontent.GetComponent<Slot_QuestReward_Item>().Set_Soc_int("[평        판][용        족]", quest.m_sRewardSOC.GetSOC_Dragon());

                contentpos = copyitemcontent.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_Quest_Info_Reward.Add(copyitemcontent);
            }
            if (quest.m_sRewardSOC.GetSOC_Shadow() != 0)
            {
                copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestReward_Item") as GameObject);
                copyitemcontent.GetComponent<Slot_QuestReward_Item>().Set_Soc_int("[평        판][어        둠]", quest.m_sRewardSOC.GetSOC_Shadow());

                contentpos = copyitemcontent.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_Quest_Info_Reward.Add(copyitemcontent);
            }

            // 아이템 보상
            if (quest.m_nRewardGold != 0)
            {
                copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestReward_Item") as GameObject);
                copyitemcontent.GetComponent<Slot_QuestReward_Item>().Set_Gold(quest.m_nRewardGold);
                contentpos = copyitemcontent.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                m_gList_Quest_Info_Reward.Add(copyitemcontent);
            }

            for (int i = 0; i < quest.m_lRewardList_Item_Equip.Count; i++)
            {
                copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestReward_Item") as GameObject);
                copyitemcontent.GetComponent<Slot_QuestReward_Item>().Set_Item(quest.m_lRewardList_Item_Equip[i]);
                contentpos = copyitemcontent.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                m_gList_Quest_Info_Reward.Add(copyitemcontent);
            }
            for (int i = 0; i < quest.m_lRewardList_Item_Use.Count; i++)
            {
                copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestReward_Item") as GameObject);
                copyitemcontent.GetComponent<Slot_QuestReward_Item>().Set_Item(quest.m_lRewardList_Item_Use[i]);
                contentpos = copyitemcontent.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                m_gList_Quest_Info_Reward.Add(copyitemcontent);
            }
            for (int i = 0; i < quest.m_lRewardList_Item_Etc.Count; i++)
            {
                copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestReward_Item") as GameObject);
                copyitemcontent.GetComponent<Slot_QuestReward_Item>().Set_Item(quest.m_lRewardList_Item_Etc[i]);
                contentpos = copyitemcontent.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_Quest_Content_Right_Content_QuestDescription_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                m_gList_Quest_Info_Reward.Add(copyitemcontent);
            }
            m_gPanel_Quest_Content_Right_Content_QuestDescription_Reward_Blank.SetActive(false);
        }
        else
        {
            m_gPanel_Quest_Content_Right_Content_QuestDescription_Reward_Blank.SetActive(true);
        }
    }

    // 초기 퀘스트 데이터 로딩 시 퀘스트 진행, 클리어 순서별 정렬.
    public void InitialSort()
    {
        InitialSort_Quest_Recommend();
        InitialSort_Quest_Process();
        InitialSort_Quest_Complete();

        m_BTN_Quest_Content_Right_Content_QuestDescription_UpBar_Condition.GetComponent<Image>().color = new Color(1, 1, 1);
        m_BTN_Quest_Content_Right_Content_QuestDescription_UpBar_Content.GetComponent<Image>().color = new Color(0.75f, 0.75f, 0.75f);
        m_BTN_Quest_Content_Right_Content_QuestDescription_UpBar_Reward.GetComponent<Image>().color = new Color(1, 1, 1);
    }
    void InitialSort_Quest_Recommend()
    {
        List<int> list_order = new List<int>();
        int temp_order;
        GameObject temp_gameobject;
        int temp_questcode;

        for (int i = 0; i < m_nList_Quest_Recommend.Count; i++)
        {
            list_order.Add(QuestManager.Instance.GetQuest_Order_Recommend(m_nList_Quest_Recommend[i]));
        }

        for (int i = 0; i < m_nList_Quest_Recommend.Count - 1;)
        {
            if (list_order[i] < list_order[i + 1])
            {
                temp_order = list_order[i];
                list_order[i] = list_order[i + 1];
                list_order[i + 1] = temp_order;

                temp_gameobject = m_gList_Quest_Recommend[i];
                m_gList_Quest_Recommend[i] = m_gList_Quest_Recommend[i + 1];
                m_gList_Quest_Recommend[i + 1] = temp_gameobject;

                temp_questcode = m_nList_Quest_Recommend[i];
                m_nList_Quest_Recommend[i] = m_nList_Quest_Recommend[i + 1];
                m_nList_Quest_Recommend[i + 1] = temp_questcode;

                i = 0;
            }
            else
                i++;
        }

        for (int i = 0; i < m_gList_Quest_Recommend.Count; i++)
        {
            m_gList_Quest_Recommend[i].gameObject.transform.SetAsFirstSibling();
            //Debug.Log(m_gList_Quest_Process[i].name);
        }
    }
    void InitialSort_Quest_Process()
    {
        List<int> list_order = new List<int>();
        int temp_order;
        GameObject temp_gameobject;
        int temp_questcode;

        for (int i = 0; i < m_nList_Quest_Process.Count; i++)
        {
            list_order.Add(QuestManager.Instance.GetQuest_Order(m_nList_Quest_Process[i]));
        }

        for (int i = 0; i < m_nList_Quest_Process.Count - 1; )
        {
            if (list_order[i] < list_order[i + 1])
            {
                temp_order = list_order[i];
                list_order[i] = list_order[i + 1];
                list_order[i + 1] = temp_order;

                temp_gameobject = m_gList_Quest_Process[i];
                m_gList_Quest_Process[i] = m_gList_Quest_Process[i + 1];
                m_gList_Quest_Process[i + 1] = temp_gameobject;

                temp_questcode = m_nList_Quest_Process[i];
                m_nList_Quest_Process[i] = m_nList_Quest_Process[i + 1];
                m_nList_Quest_Process[i + 1] = temp_questcode;

                i = 0;
            }
            else
                i++;
        }

        for (int i = 0; i < m_gList_Quest_Process.Count; i++)
        {
            m_gList_Quest_Process[i].gameObject.transform.SetAsFirstSibling();
            //Debug.Log(m_gList_Quest_Process[i].name);
        }
    }
    void InitialSort_Quest_Complete()
    {
        List<int> list_order = new List<int>();
        int temp_order;
        GameObject temp_gameobject;
        int temp_questcode;

        for (int i = 0; i < m_nList_Quest_Complete.Count; i++)
        {
            list_order.Add(QuestManager.Instance.GetQuest_Order(m_nList_Quest_Complete[i]));
        }

        for (int i = 0; i < m_nList_Quest_Complete.Count - 1;)
        {
            if (list_order[i] < list_order[i + 1])
            {
                temp_order = list_order[i];
                list_order[i] = list_order[i + 1];
                list_order[i + 1] = temp_order;

                temp_gameobject = m_gList_Quest_Complete[i];
                m_gList_Quest_Complete[i] = m_gList_Quest_Complete[i + 1];
                m_gList_Quest_Complete[i + 1] = temp_gameobject;

                temp_questcode = m_nList_Quest_Complete[i];
                m_nList_Quest_Complete[i] = m_nList_Quest_Complete[i + 1];
                m_nList_Quest_Complete[i + 1] = temp_questcode;

                i = 0;
            }
            else
                i++;
        }

        for (int i = 0; i < m_gList_Quest_Complete.Count; i++)
        {
            m_gList_Quest_Complete[i].gameObject.transform.SetAsFirstSibling();
            //Debug.Log(m_gList_Quest_Complete[i].name);
        }
    }

    // 퀘스트 로드맵 업데이트
    void Update_QuestLoadmap()
    {
        for (int i = 0; i < m_csList_Loadmap.Count; i++)
        {
            m_csList_Loadmap[i].Update_QuestLoadmap_QuestPanel();
        }
    }
}
