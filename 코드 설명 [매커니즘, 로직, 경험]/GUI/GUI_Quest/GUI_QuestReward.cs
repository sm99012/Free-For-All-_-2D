using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUI_QuestReward : MonoBehaviour
{
    [SerializeField] public GameObject m_gPanel_QuestReward;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_QuestReward_UpBar;
    [SerializeField] Button m_BTN_QuestReward_UpBar_Exit;
    [SerializeField] Button m_BTN_QuestReward_UpBar_Next;
    [SerializeField] Button m_BTN_QuestReward_UpBar_Before;
    [SerializeField] TextMeshProUGUI m_TMP_QuestReward_UpBar_Name;
    [SerializeField] TextMeshProUGUI m_TMP_QuestReward_UpBar_Information;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_QuestReward_Content;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_QuestReward_Content_SOC;
    [SerializeField] TextMeshProUGUI m_TMP_QuestReward_Content_SOC_L;
    [SerializeField] TextMeshProUGUI m_TMP_QuestReward_Content_SOC_R;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_QuestReward_Content_STATUS;
    [SerializeField] TextMeshProUGUI m_TMP_QuestReward_Content_STATUS_L;
    [SerializeField] TextMeshProUGUI m_TMP_QuestReward_Content_STATUS_R;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_QuestReward_Content_Item;
    [SerializeField] GameObject m_SVPanel_QuestReward_Content_Item;
    [SerializeField] GameObject m_Viewport_QuestReward_Content_Item;
    [SerializeField] GameObject m_gContent_QuestReward_Content_Item;

    [SerializeField] GameObject m_gPanel_QuestReward_Hidden;

    public enum E_QUEST_REWARD_CATEGORY { REWARD_STATUS, REWARD_SOC, REWARD_ITEM }
    public E_QUEST_REWARD_CATEGORY m_eQuestRewardCategory;

    [SerializeField] List<GameObject> m_List_Content_Item;

    public void InitialSet()
    {
        InitialSet_Object();
        InitialSet_Button();
    }

    void InitialSet_Object()
    {
        m_gPanel_QuestReward = GameObject.Find("Canvas_GUI").transform.Find("Panel_QuestReward").gameObject;

        m_gPanel_QuestReward_UpBar = m_gPanel_QuestReward.transform.Find("Panel_QuestReward_UpBar").gameObject;

        m_BTN_QuestReward_UpBar_Exit = m_gPanel_QuestReward_UpBar.transform.Find("BTN_QuestReward_UpBar_Exit").gameObject.GetComponent<Button>();
        m_BTN_QuestReward_UpBar_Next = m_gPanel_QuestReward_UpBar.transform.Find("BTN_QuestReward_UpBar_Next").gameObject.GetComponent<Button>();
        m_BTN_QuestReward_UpBar_Before = m_gPanel_QuestReward_UpBar.transform.Find("BTN_QuestReward_UpBar_Before").gameObject.GetComponent<Button>();
        m_TMP_QuestReward_UpBar_Name = m_gPanel_QuestReward_UpBar.transform.Find("TMP_QuestReward_UpBar_Name").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_QuestReward_UpBar_Information = m_gPanel_QuestReward_UpBar.transform.Find("TMP_QuestReward_UpBar_Information").gameObject.GetComponent<TextMeshProUGUI>();

        m_gPanel_QuestReward_Content = m_gPanel_QuestReward.transform.Find("Panel_QuestReward_Content").gameObject;

        m_gPanel_QuestReward_Content_SOC = m_gPanel_QuestReward_Content.transform.Find("Panel_QuestReward_Content_SOC").gameObject;
        m_TMP_QuestReward_Content_SOC_L = m_gPanel_QuestReward_Content_SOC.transform.Find("TMP_QuestReward_Content_SOC_L").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_QuestReward_Content_SOC_R = m_gPanel_QuestReward_Content_SOC.transform.Find("TMP_QuestReward_Content_SOC_R").gameObject.GetComponent<TextMeshProUGUI>();

        m_gPanel_QuestReward_Content_STATUS = m_gPanel_QuestReward_Content.transform.Find("Panel_QuestReward_Content_STATUS").gameObject;
        m_TMP_QuestReward_Content_STATUS_L = m_gPanel_QuestReward_Content_STATUS.transform.Find("TMP_QuestReward_Content_STATUS_L").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_QuestReward_Content_STATUS_R = m_gPanel_QuestReward_Content_STATUS.transform.Find("TMP_QuestReward_Content_STATUS_R").gameObject.GetComponent<TextMeshProUGUI>();

        m_gPanel_QuestReward_Content_Item = m_gPanel_QuestReward_Content.transform.Find("Panel_QuestReward_Content_Item").gameObject;
        m_SVPanel_QuestReward_Content_Item = m_gPanel_QuestReward_Content_Item.transform.Find("SV_QuestReward_Content_Item").gameObject;
        m_Viewport_QuestReward_Content_Item = m_SVPanel_QuestReward_Content_Item.transform.Find("Viewport_QuestReward_Content_Item").gameObject;
        m_gContent_QuestReward_Content_Item = m_Viewport_QuestReward_Content_Item.transform.Find("Content_QuestReward_Content_Item").gameObject;

        m_gPanel_QuestReward_Hidden = m_gPanel_QuestReward.transform.Find("Panel_QuestReward_Hidden").gameObject;

        m_eQuestRewardCategory = E_QUEST_REWARD_CATEGORY.REWARD_STATUS;
        m_List_Content_Item = new List<GameObject>();
    }

    void InitialSet_Button()
    {
        m_BTN_QuestReward_UpBar_Exit.onClick.RemoveAllListeners();
        m_BTN_QuestReward_UpBar_Exit.onClick.AddListener(delegate { Press_Btn_Exit(); });
        m_BTN_QuestReward_UpBar_Next.onClick.RemoveAllListeners();
        m_BTN_QuestReward_UpBar_Next.onClick.AddListener(delegate { Press_Btn_Next(); });
        m_BTN_QuestReward_UpBar_Before.onClick.RemoveAllListeners();
        m_BTN_QuestReward_UpBar_Before.onClick.AddListener(delegate { Press_Btn_Before(); });
    }
    public void Press_Btn_Exit()
    {
        m_gPanel_QuestReward.SetActive(false);

        for (int i = 0; i < m_List_Content_Item.Count; i++)
        {
            Destroy(m_List_Content_Item[i]);
        }
        m_List_Content_Item.Clear();
    }
    void Press_Btn_Next()
    {
        Change_QuestRewardCategory(1);
    }
    void Press_Btn_Before()
    {
        Change_QuestRewardCategory(-1);
    }

    // number == +1: Press_Btn_Next()
    // number == -1: Press_Btn_Before()
    void Change_QuestRewardCategory(int number)
    {
        if (number == 1)
        {
            switch (m_eQuestRewardCategory)
            {
                case E_QUEST_REWARD_CATEGORY.REWARD_STATUS:
                    {
                        m_gPanel_QuestReward_Content_STATUS.SetActive(false);
                        m_gPanel_QuestReward_Content_SOC.SetActive(true);
                        m_gPanel_QuestReward_Content_Item.SetActive(false);
                        m_eQuestRewardCategory = E_QUEST_REWARD_CATEGORY.REWARD_SOC;
                        m_TMP_QuestReward_UpBar_Information.text = "평판 보상";
                    }
                    break;
                case E_QUEST_REWARD_CATEGORY.REWARD_SOC:
                    {
                        m_gPanel_QuestReward_Content_STATUS.SetActive(false);
                        m_gPanel_QuestReward_Content_SOC.SetActive(false);
                        m_gPanel_QuestReward_Content_Item.SetActive(true);
                        m_eQuestRewardCategory = E_QUEST_REWARD_CATEGORY.REWARD_ITEM;
                        m_TMP_QuestReward_UpBar_Information.text = "아이템 보상";
                    }
                    break;
                case E_QUEST_REWARD_CATEGORY.REWARD_ITEM:
                    {
                        m_gPanel_QuestReward_Content_STATUS.SetActive(true);
                        m_gPanel_QuestReward_Content_SOC.SetActive(false);
                        m_gPanel_QuestReward_Content_Item.SetActive(false);
                        m_eQuestRewardCategory = E_QUEST_REWARD_CATEGORY.REWARD_STATUS;
                        m_TMP_QuestReward_UpBar_Information.text = "스탯 보상";
                    }
                    break;
            }
        }
        else if (number == -1)
        {
            switch (m_eQuestRewardCategory)
            {
                case E_QUEST_REWARD_CATEGORY.REWARD_STATUS:
                    {
                        m_gPanel_QuestReward_Content_STATUS.SetActive(false);
                        m_gPanel_QuestReward_Content_SOC.SetActive(false);
                        m_gPanel_QuestReward_Content_Item.SetActive(true);
                        m_eQuestRewardCategory = E_QUEST_REWARD_CATEGORY.REWARD_ITEM;
                        m_TMP_QuestReward_UpBar_Information.text = "아이템 보상";
                    }
                    break;
                case E_QUEST_REWARD_CATEGORY.REWARD_SOC:
                    {
                        m_gPanel_QuestReward_Content_STATUS.SetActive(true);
                        m_gPanel_QuestReward_Content_SOC.SetActive(false);
                        m_gPanel_QuestReward_Content_Item.SetActive(false);
                        m_eQuestRewardCategory = E_QUEST_REWARD_CATEGORY.REWARD_STATUS;
                        m_TMP_QuestReward_UpBar_Information.text = "스탯 보상";
                    }
                    break;
                case E_QUEST_REWARD_CATEGORY.REWARD_ITEM:
                    {
                        m_gPanel_QuestReward_Content_STATUS.SetActive(false);
                        m_gPanel_QuestReward_Content_SOC.SetActive(true);
                        m_gPanel_QuestReward_Content_Item.SetActive(false);
                        m_eQuestRewardCategory = E_QUEST_REWARD_CATEGORY.REWARD_SOC;
                        m_TMP_QuestReward_UpBar_Information.text = "평판 보상";
                    }
                    break;
            }
        }
    }

    // n == 0: 서브, 히든, 스토리 퀘스트 관계없이 보상 공개.
    // n == 1:
    // n == 2: 보상 미공개.
    public void Display_QuestReward(Quest quest, int n = 0)
    {
        m_gPanel_QuestReward.SetActive(true);
        m_gPanel_QuestReward.transform.SetAsLastSibling();

        if (n == 0)
        {
            //if (m_gPanel_QuestReward.activeSelf == false)
            {
                Update_QuestReward(quest);
                m_gPanel_QuestReward_Hidden.SetActive(false);
                m_gPanel_QuestReward_UpBar.SetActive(true);
                m_gPanel_QuestReward_Content.SetActive(true);

            }
        }
        else if (n == 1)
        {
            //if (m_gPanel_QuestReward.activeSelf == false)
            {
                if (quest.m_bCondition == true)
                {
                    Update_QuestReward(quest);
                    m_gPanel_QuestReward_Hidden.SetActive(false);
                    m_gPanel_QuestReward_UpBar.SetActive(true);
                    m_gPanel_QuestReward_Content.SetActive(true);
                }
                else
                {
                    m_gPanel_QuestReward_Hidden.SetActive(true);
                    m_gPanel_QuestReward_UpBar.SetActive(false);
                    m_gPanel_QuestReward_Content.SetActive(false);
                }
            }
        }
        else if (n == 2)
        {
            //if (m_gPanel_QuestReward.activeSelf == false)
            {
                m_gPanel_QuestReward_Hidden.SetActive(true);
                m_gPanel_QuestReward_UpBar.SetActive(false);
                m_gPanel_QuestReward_Content.SetActive(false);
            }
        }
    }

    public void Update_QuestReward(Quest quest)
    {
        //if (m_gPanel_QuestReward.activeSelf == false)
        {
            m_TMP_QuestReward_UpBar_Name.text = quest.m_sQuest_Title.Substring(quest.m_sQuest_Title.IndexOf('\n') + 1);

            m_TMP_QuestReward_Content_STATUS_L.text = "";
            m_TMP_QuestReward_Content_STATUS_L.text += "경  험  치: " + quest.m_sRewardSTATUS.GetSTATUS_EXP_Current() + "\n";
            m_TMP_QuestReward_Content_STATUS_L.text += "체        력: " + quest.m_sRewardSTATUS.GetSTATUS_HP_Max() + "\n";
            m_TMP_QuestReward_Content_STATUS_L.text += "데  미  지: " + quest.m_sRewardSTATUS.GetSTATUS_Damage_Total() + "\n";
            m_TMP_QuestReward_Content_STATUS_L.text += "이동속도: " + quest.m_sRewardSTATUS.GetSTATUS_Speed();
            m_TMP_QuestReward_Content_STATUS_R.text = "";
            m_TMP_QuestReward_Content_STATUS_R.text += "\n";
            m_TMP_QuestReward_Content_STATUS_R.text += "마        나: " + quest.m_sRewardSTATUS.GetSTATUS_MP_Max() + "\n";
            m_TMP_QuestReward_Content_STATUS_R.text += "방  어  력: " + quest.m_sRewardSTATUS.GetSTATUS_Defence_Physical() + "\n";
            m_TMP_QuestReward_Content_STATUS_R.text += "공격속도: " + quest.m_sRewardSTATUS.GetSTATUS_AttackSpeed();

            m_TMP_QuestReward_Content_SOC_L.text = "";
            m_TMP_QuestReward_Content_SOC_L.text += "명        예: " + quest.m_sRewardSOC.GetSOC_Honor() + "\n";
            m_TMP_QuestReward_Content_SOC_L.text += "인        간: " + quest.m_sRewardSOC.GetSOC_Human() + "\n";
            m_TMP_QuestReward_Content_SOC_L.text += "동        물: " + quest.m_sRewardSOC.GetSOC_Animal() + "\n";
            m_TMP_QuestReward_Content_SOC_L.text += "슬  라  임: " + quest.m_sRewardSOC.GetSOC_Slime() + "\n";
            m_TMP_QuestReward_Content_SOC_L.text += "스켈레톤: " + quest.m_sRewardSOC.GetSOC_Skeleton();
            m_TMP_QuestReward_Content_SOC_R.text = "";
            m_TMP_QuestReward_Content_SOC_R.text += "\n";
            m_TMP_QuestReward_Content_SOC_R.text += "앤        트: " + quest.m_sRewardSOC.GetSOC_Ents() + "\n";
            m_TMP_QuestReward_Content_SOC_R.text += "마        족: " + quest.m_sRewardSOC.GetSOC_Devil() + "\n";
            m_TMP_QuestReward_Content_SOC_R.text += "용        족: " + quest.m_sRewardSOC.GetSOC_Dragon() + "\n";
            m_TMP_QuestReward_Content_SOC_R.text += "어        둠: " + quest.m_sRewardSOC.GetSOC_Shadow();

            GameObject copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestReward_Item") as GameObject);
            RectTransform contentpos = copyitemcontent.GetComponent<RectTransform>();

            if (quest.m_nRewardGold != 0)
            {
                copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestReward_Item") as GameObject);
                copyitemcontent.GetComponent<Slot_QuestReward_Item>().Set_Gold(quest.m_nRewardGold);
                contentpos = copyitemcontent.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_QuestReward_Content_Item.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                m_List_Content_Item.Add(copyitemcontent);
            }

            for (int i = 0; i < quest.m_lRewardList_Item_Equip.Count; i++)
            {
                copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestReward_Item") as GameObject);
                copyitemcontent.GetComponent<Slot_QuestReward_Item>().Set_Item(quest.m_lRewardList_Item_Equip[i]);
                contentpos = copyitemcontent.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_QuestReward_Content_Item.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                m_List_Content_Item.Add(copyitemcontent);
            }
            for (int i = 0; i < quest.m_lRewardList_Item_Use.Count; i++)
            {
                copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestReward_Item") as GameObject);
                copyitemcontent.GetComponent<Slot_QuestReward_Item>().Set_Item(quest.m_lRewardList_Item_Use[i]);
                contentpos = copyitemcontent.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_QuestReward_Content_Item.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                m_List_Content_Item.Add(copyitemcontent);
            }
            for (int i = 0; i < quest.m_lRewardList_Item_Etc.Count; i++)
            {
                copyitemcontent = Instantiate(Resources.Load("Prefab/GUI/Panel_QuestReward_Item") as GameObject);
                copyitemcontent.GetComponent<Slot_QuestReward_Item>().Set_Item(quest.m_lRewardList_Item_Etc[i]);
                contentpos = copyitemcontent.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_QuestReward_Content_Item.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
                m_List_Content_Item.Add(copyitemcontent);
            }
        }
    }
}
