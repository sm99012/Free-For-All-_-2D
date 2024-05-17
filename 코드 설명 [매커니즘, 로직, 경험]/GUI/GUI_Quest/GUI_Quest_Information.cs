using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUI_Quest_Information : MonoBehaviour
{
    // Quest 의 진행 현황, 진행 도중 설명, 완료 후 설명 관련 UI.
    [SerializeField] GameObject m_gPanel_Quest_Information;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_Quest_Information_Content;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_Quest_Information_Content_Name;
    [SerializeField] TextMeshProUGUI m_TMP_Quest_Information_Content_Name;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_Quest_Information_Content_NPC;
    [SerializeField] Image m_IMG_Quest_Information_Content_NPC;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_Quest_Information_Content_Description_1;
    [SerializeField] TextMeshProUGUI m_TMP_Quest_Information_Content_Description_1;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_Quest_Information_Content_Description_2;
    [SerializeField] TextMeshProUGUI m_TMP_Quest_Information_Content_Description_2;
    [Space(20)]
    [SerializeField] Button m_BTN_Quest_Information_Exit;

    public void InitialSet()
    {
        InitialSet_Object();
        InitialSet_Button();
    }

    // 초기 Object 불러오기.
    void InitialSet_Object()
    {
        m_gPanel_Quest_Information = GameObject.Find("Canvas_GUI").gameObject.transform.Find("Panel_Quest_Information").gameObject;

        m_gPanel_Quest_Information_Content = m_gPanel_Quest_Information.transform.Find("Panel_Quest_Information_Content").gameObject;

        m_gPanel_Quest_Information_Content_Name = m_gPanel_Quest_Information_Content.transform.Find("Panel_Quest_Information_Content_Name").gameObject;
        m_TMP_Quest_Information_Content_Name = m_gPanel_Quest_Information_Content_Name.transform.Find("TMP_Quest_Information_Content_Name").gameObject.GetComponent<TextMeshProUGUI>();

        m_gPanel_Quest_Information_Content_NPC = m_gPanel_Quest_Information_Content.transform.Find("Panel_Quest_Information_Content_NPC").gameObject;
        m_IMG_Quest_Information_Content_NPC = m_gPanel_Quest_Information_Content_NPC.transform.Find("IMG_Quest_Information_Content_NPC").gameObject.GetComponent<Image>();

        m_gPanel_Quest_Information_Content_Description_1 = m_gPanel_Quest_Information_Content.transform.Find("Panel_Quest_Information_Content_Description_1").gameObject;
        m_TMP_Quest_Information_Content_Description_1 = m_gPanel_Quest_Information_Content_Description_1.transform.Find("Scroll View").transform.Find("Viewport").transform.Find
            ("TMP_Quest_Information_Content_Description_1").gameObject.GetComponent<TextMeshProUGUI>();

        m_gPanel_Quest_Information_Content_Description_2 = m_gPanel_Quest_Information_Content.transform.Find("Panel_Quest_Information_Content_Description_2").gameObject;
        m_TMP_Quest_Information_Content_Description_2 = m_gPanel_Quest_Information_Content_Description_2.transform.Find("Scroll View").transform.Find("Viewport").transform.Find
            ("TMP_Quest_Information_Content_Description_2").gameObject.GetComponent<TextMeshProUGUI>();



        m_BTN_Quest_Information_Exit = m_gPanel_Quest_Information.transform.Find("BTN_Quest_Information_Exit").gameObject.GetComponent<Button>();

    }
    // 초기 Button 이벤트 설정.
    void InitialSet_Button()
    {
        m_BTN_Quest_Information_Exit.onClick.RemoveAllListeners();
        m_BTN_Quest_Information_Exit.onClick.AddListener(delegate { Btn_Press_Exit(); });
    }
    // 버튼 이벤트 처리.
    public void Btn_Press_Exit()
    {
        m_gPanel_Quest_Information.SetActive(false);
    }

    //public void Set_NPC_Null()
    //{
    //    m_IMG_Quest_Information_Content_NPC.color = new Color(1, 1, 1, 0);
    //    m_IMG_Quest_Information_Content_NPC.sprite = null;
    //}
    //public void Set_NPC(NPC_Total npc)
    //{
    //    m_IMG_Quest_Information_Content_NPC.sprite = npc.m_Sprite_NPC;
    //    m_IMG_Quest_Information_Content_NPC.color = new Color(1, 1, 1, 1);
    //}

    //// Quest_Information UI 업데이트.
    //// num == 1: 진행중인 Quest.
    //// num == 2: 완료한 Quest.
    //// 어떤 몹을 몇마리 잡았는지까지 세부적으로 표시해주자. - ADD!!
    //public void UpdateQuestInformation(Quest_KILL_MONSTER quest, int num)
    //{
    //    m_TMP_Quest_Information_Content_Name.text = quest.m_sQuest_Title;

    //    if (num == 1)
    //    {
    //        if (quest.m_bCondition == false)
    //            m_TMP_Quest_Information_Content_Description_1.text = quest.m_sQuest_Information_Process;
    //        else
    //            m_TMP_Quest_Information_Content_Description_1.text = quest.m_sQuest_Information_Condition;

    //        if (quest.m_bQuest_Information_Process_Hide == false)
    //        {
    //            m_TMP_Quest_Information_Content_Description_2.text = "";
    //            m_TMP_Quest_Information_Content_Description_2.text += "토벌 현황: \n";
    //            for (int i = 0; i < quest.m_nl_MonsterCode.Count; i++)
    //            {
    //                if (i != 0)
    //                    m_TMP_Quest_Information_Content_Description_2.text += ", \n";
    //                m_TMP_Quest_Information_Content_Description_2.text += "[" + MonsterManager.m_Dictionary_Monster[quest.m_nl_MonsterCode[i]].m_sMonster_Name + "]" + quest.m_nl_Count_Current[i] + " / " + quest.m_nl_Count_Max[i];
    //            }
    //        }
    //        else
    //        {
    //            if (quest.m_bCondition == true)
    //            {
    //                m_TMP_Quest_Information_Content_Description_2.text = "";
    //                m_TMP_Quest_Information_Content_Description_2.text += "토벌 현황: \n";
    //                for (int i = 0; i < quest.m_nl_MonsterCode.Count; i++)
    //                {
    //                    if (i != 0)
    //                        m_TMP_Quest_Information_Content_Description_2.text += ", \n";
    //                    m_TMP_Quest_Information_Content_Description_2.text += "[" + MonsterManager.m_Dictionary_Monster[quest.m_nl_MonsterCode[i]].m_sMonster_Name + "]" + quest.m_nl_Count_Current[i] + " / " + quest.m_nl_Count_Max[i];
    //                }
    //            }
    //            else
    //                m_TMP_Quest_Information_Content_Description_2.text = "???";
    //        }
    //    }
    //    else
    //    {
    //        m_TMP_Quest_Information_Content_Description_1.text = quest.m_sQuest_Information_Clear;

    //        m_TMP_Quest_Information_Content_Description_2.text = "";
    //        m_TMP_Quest_Information_Content_Description_2.text += "토벌 현황: \n";
    //        for (int i = 0; i < quest.m_nl_MonsterCode.Count; i++)
    //        {
    //            if (i != 0)
    //                m_TMP_Quest_Information_Content_Description_2.text += ", \n";
    //            m_TMP_Quest_Information_Content_Description_2.text += "[" + MonsterManager.m_Dictionary_Monster[quest.m_nl_MonsterCode[i]].m_sMonster_Name + "]" + quest.m_nl_Count_Current[i] + " / " + quest.m_nl_Count_Max[i];
    //        }
    //    }

    //    m_IMG_Quest_Information_Content_NPC.sprite = NPCManager_Total.m_Dictionary_NPC[quest.m_nNPC_Clear].m_Sprite_NPC;

    //}
    //public void UpdateQuestInformation(Quest_KILL_TYPE quest, int num)
    //{
    //    m_TMP_Quest_Information_Content_Name.text = quest.m_sQuest_Title;

    //    if (num == 1)
    //    {
    //        if (quest.m_bCondition == false)
    //            m_TMP_Quest_Information_Content_Description_1.text = quest.m_sQuest_Information_Process;
    //        else
    //            m_TMP_Quest_Information_Content_Description_1.text = quest.m_sQuest_Information_Condition;

    //        if (quest.m_bQuest_Information_Process_Hide == false)
    //        {
    //            m_TMP_Quest_Information_Content_Description_2.text = "";
    //            m_TMP_Quest_Information_Content_Description_2.text += "토벌 현황: ";
    //            m_TMP_Quest_Information_Content_Description_2.text += "[" + quest.m_eMonsterType + "]" + quest.m_nCount_Current + " / " + quest.m_nCount_Max;
    //        }
    //        else
    //        {
    //            if (quest.m_bCondition == true)
    //            {
    //                m_TMP_Quest_Information_Content_Description_2.text = "";
    //                m_TMP_Quest_Information_Content_Description_2.text += "토벌 현황: ";
    //                m_TMP_Quest_Information_Content_Description_2.text += "[" + quest.m_eMonsterType + "]" + quest.m_nCount_Current + " / " + quest.m_nCount_Max;
    //            }
    //            else
    //                m_TMP_Quest_Information_Content_Description_2.text = "???";
    //        }
    //    }
    //    else
    //    {
    //        m_TMP_Quest_Information_Content_Description_1.text = quest.m_sQuest_Information_Clear;

    //        m_TMP_Quest_Information_Content_Description_2.text = "";
    //        m_TMP_Quest_Information_Content_Description_2.text += "토벌 현황: ";
    //        m_TMP_Quest_Information_Content_Description_2.text += "[" + quest.m_eMonsterType + "]" + quest.m_nCount_Current + " / " + quest.m_nCount_Max;
    //    }

    //    m_IMG_Quest_Information_Content_NPC.sprite = NPCManager_Total.m_Dictionary_NPC[quest.m_nNPC].m_Sprite_NPC;
    //}
    //public void UpdateQuestInformation(Quest_GOAWAY_MONSTER quest, int num)
    //{
    //    m_TMP_Quest_Information_Content_Name.text = quest.m_sQuest_Title;

    //    if (num == 1)
    //    {
    //        if (quest.m_bCondition == false)
    //            m_TMP_Quest_Information_Content_Description_1.text = quest.m_sQuest_Information_Process;
    //        else
    //            m_TMP_Quest_Information_Content_Description_1.text = quest.m_sQuest_Information_Condition;

    //        if (quest.m_bQuest_Information_Process_Hide == false)
    //        {
    //            m_TMP_Quest_Information_Content_Description_2.text = "";
    //            m_TMP_Quest_Information_Content_Description_2.text += "놓아주기 현황: \n";
    //            for (int i = 0; i < quest.m_nl_MonsterCode.Count; i++)
    //            {
    //                if (i != 0)
    //                    m_TMP_Quest_Information_Content_Description_2.text += ", \n";
    //                m_TMP_Quest_Information_Content_Description_2.text += "[" + MonsterManager.m_Dictionary_Monster[quest.m_nl_MonsterCode[i]].m_sMonster_Name + "]" + quest.m_nl_Count_Current[i] + " / " + quest.m_nl_Count_Max[i];
    //            }
    //        }
    //        else
    //        {
    //            if (quest.m_bCondition == true)
    //            {
    //                m_TMP_Quest_Information_Content_Description_2.text = "";
    //                m_TMP_Quest_Information_Content_Description_2.text += "놓아주기 현황: \n";
    //                for (int i = 0; i < quest.m_nl_MonsterCode.Count; i++)
    //                {
    //                    if (i != 0)
    //                        m_TMP_Quest_Information_Content_Description_2.text += ", \n";
    //                    m_TMP_Quest_Information_Content_Description_2.text += "[" + MonsterManager.m_Dictionary_Monster[quest.m_nl_MonsterCode[i]].m_sMonster_Name + "]" + quest.m_nl_Count_Current[i] + " / " + quest.m_nl_Count_Max[i];
    //                }
    //            }
    //            else
    //                m_TMP_Quest_Information_Content_Description_2.text = "???";
    //        }
    //    }
    //    else
    //    {
    //        m_TMP_Quest_Information_Content_Description_1.text = quest.m_sQuest_Information_Clear;

    //        m_TMP_Quest_Information_Content_Description_2.text = "";
    //        m_TMP_Quest_Information_Content_Description_2.text += "놓아주기 현황: \n";
    //        for (int i = 0; i < quest.m_nl_MonsterCode.Count; i++)
    //        {
    //            if (i != 0)
    //                m_TMP_Quest_Information_Content_Description_2.text += ", \n";
    //            m_TMP_Quest_Information_Content_Description_2.text += "[" + MonsterManager.m_Dictionary_Monster[quest.m_nl_MonsterCode[i]].m_sMonster_Name + "]" + quest.m_nl_Count_Current[i] + " / " + quest.m_nl_Count_Max[i];
    //        }
    //    }

    //    m_IMG_Quest_Information_Content_NPC.sprite = NPCManager_Total.m_Dictionary_NPC[quest.m_nNPC].m_Sprite_NPC;
    //}
    //public void UpdateQuestInformation(Quest_GOAWAY_TYPE quest, int num)
    //{
    //    m_TMP_Quest_Information_Content_Name.text = quest.m_sQuest_Title;

    //    if (num == 1)
    //    {
    //        if (quest.m_bCondition == false)
    //            m_TMP_Quest_Information_Content_Description_1.text = quest.m_sQuest_Information_Process;
    //        else
    //            m_TMP_Quest_Information_Content_Description_1.text = quest.m_sQuest_Information_Condition;

    //        if (quest.m_bQuest_Information_Process_Hide == false)
    //        {
    //            m_TMP_Quest_Information_Content_Description_2.text = "";
    //            m_TMP_Quest_Information_Content_Description_2.text += "놓아주기 현황: ";
    //            m_TMP_Quest_Information_Content_Description_2.text += "[" + quest.m_eMonsterType + "]" + quest.m_nCount_Current + " / " + quest.m_nCount_Max;
    //        }
    //        else
    //        {
    //            if (quest.m_bCondition == true)
    //            {
    //                m_TMP_Quest_Information_Content_Description_2.text = "";
    //                m_TMP_Quest_Information_Content_Description_2.text += "놓아주기 현황: ";
    //                m_TMP_Quest_Information_Content_Description_2.text += "[" + quest.m_eMonsterType + "]" + quest.m_nCount_Current + " / " + quest.m_nCount_Max;
    //            }
    //            else
    //                m_TMP_Quest_Information_Content_Description_2.text = "???";
    //        }
    //    }
    //    else
    //    {
    //        m_TMP_Quest_Information_Content_Description_1.text = quest.m_sQuest_Information_Clear;

    //        m_TMP_Quest_Information_Content_Description_2.text = "";
    //        m_TMP_Quest_Information_Content_Description_2.text += "놓아주기 현황: ";
    //        m_TMP_Quest_Information_Content_Description_2.text += "[" + quest.m_eMonsterType + "]" + quest.m_nCount_Current + " / " + quest.m_nCount_Max;
    //    }

    //    m_IMG_Quest_Information_Content_NPC.sprite = NPCManager_Total.m_Dictionary_NPC[quest.m_nNPC].m_Sprite_NPC;
    //}
    //public void UpdateQuestInformation(Quest_COLLECT quest, int num)
    //{
    //    m_TMP_Quest_Information_Content_Name.text = quest.m_sQuest_Title;

    //    if (num == 1)
    //    {
    //        if (quest.m_bCondition == false)
    //            m_TMP_Quest_Information_Content_Description_1.text = quest.m_sQuest_Information_Process;
    //        else
    //            m_TMP_Quest_Information_Content_Description_1.text = quest.m_sQuest_Information_Condition;

    //        if (quest.m_bQuest_Information_Process_Hide == false)
    //        {
    //            m_TMP_Quest_Information_Content_Description_2.text = "";
    //            m_TMP_Quest_Information_Content_Description_2.text += "수집 현황: \n";
    //            for (int i = 0; i < quest.m_nl_ItemCode.Count; i++)
    //            {
    //                if (i != 0)
    //                    m_TMP_Quest_Information_Content_Description_2.text += ", \n";
    //                // m_TMP_Quest_Information_Content_Description_2.text += "[" + quest.m_nl_ItemCode[i] + "]" + quest.m_nl_ItemCount_Current[0] + " / " + quest.m_nl_ItemCount_Max[0];
    //                m_TMP_Quest_Information_Content_Description_2.text += "[";
    //                if (quest.m_nl_ItemCode[i] < 1000)
    //                {
    //                    if (ItemManager.instance.m_Dictionary_MonsterDrop_Etc.ContainsKey(quest.m_nl_ItemCode[i]) == true)
    //                    {
    //                        m_TMP_Quest_Information_Content_Description_2.text += ItemManager.instance.m_Dictionary_MonsterDrop_Etc[quest.m_nl_ItemCode[i]].m_sItemName;
    //                    }
    //                    else if (ItemManager.instance.m_Dictionary_Collection_Etc.ContainsKey(quest.m_nl_ItemCode[i]) == true)
    //                    {
    //                        m_TMP_Quest_Information_Content_Description_2.text += ItemManager.instance.m_Dictionary_Collection_Etc[quest.m_nl_ItemCode[i]].m_sItemName;
    //                    }
    //                    else if (ItemManager.instance.m_Dictionary_QuestReward_Etc.ContainsKey(quest.m_nl_ItemCode[i]) == true)
    //                    {
    //                        m_TMP_Quest_Information_Content_Description_2.text += ItemManager.instance.m_Dictionary_QuestReward_Etc[quest.m_nl_ItemCode[i]].m_sItemName;
    //                    }
    //                }
    //                else if (quest.m_nl_ItemCode[i] < 7000)
    //                {
    //                    if (ItemManager.instance.m_Dictionary_MonsterDrop_Equip.ContainsKey(quest.m_nl_ItemCode[i]) == true)
    //                    {
    //                        m_TMP_Quest_Information_Content_Description_2.text += ItemManager.instance.m_Dictionary_MonsterDrop_Equip[quest.m_nl_ItemCode[i]].m_sItemName;
    //                    }
    //                    else if (ItemManager.instance.m_Dictionary_Collection_Equip.ContainsKey(quest.m_nl_ItemCode[i]) == true)
    //                    {
    //                        m_TMP_Quest_Information_Content_Description_2.text += ItemManager.instance.m_Dictionary_Collection_Equip[quest.m_nl_ItemCode[i]].m_sItemName;
    //                    }
    //                    else if (ItemManager.instance.m_Dictionary_QuestReward_Equip.ContainsKey(quest.m_nl_ItemCode[i]) == true)
    //                    {
    //                        m_TMP_Quest_Information_Content_Description_2.text += ItemManager.instance.m_Dictionary_QuestReward_Equip[quest.m_nl_ItemCode[i]].m_sItemName;
    //                    }
    //                }
    //                else
    //                {
    //                    if (ItemManager.instance.m_Dictionary_MonsterDrop_Use.ContainsKey(quest.m_nl_ItemCode[i]) == true)
    //                    {
    //                        m_TMP_Quest_Information_Content_Description_2.text += ItemManager.instance.m_Dictionary_MonsterDrop_Use[quest.m_nl_ItemCode[i]].m_sItemName;
    //                    }
    //                    else if (ItemManager.instance.m_Dictionary_Collection_Use.ContainsKey(quest.m_nl_ItemCode[i]) == true)
    //                    {
    //                        m_TMP_Quest_Information_Content_Description_2.text += ItemManager.instance.m_Dictionary_Collection_Use[quest.m_nl_ItemCode[i]].m_sItemName;
    //                    }
    //                    else if (ItemManager.instance.m_Dictionary_QuestReward_Use.ContainsKey(quest.m_nl_ItemCode[i]) == true)
    //                    {
    //                        m_TMP_Quest_Information_Content_Description_2.text += ItemManager.instance.m_Dictionary_QuestReward_Use[quest.m_nl_ItemCode[i]].m_sItemName;
    //                    }
    //                }
    //                m_TMP_Quest_Information_Content_Description_2.text += "]" + quest.m_nl_ItemCount_Current[i] + " / " + quest.m_nl_ItemCount_Max[i];
    //            }
    //        }
    //        else
    //        {
    //            if (quest.m_bCondition == true)
    //            {
    //                m_TMP_Quest_Information_Content_Description_2.text = "";
    //                m_TMP_Quest_Information_Content_Description_2.text += "수집 현황: \n";
    //                for (int i = 0; i < quest.m_nl_ItemCode.Count; i++)
    //                {
    //                    if (i != 0)
    //                        m_TMP_Quest_Information_Content_Description_2.text += ", \n";
    //                    // m_TMP_Quest_Information_Content_Description_2.text += "[" + quest.m_nl_ItemCode[i] + "]" + quest.m_nl_ItemCount_Current[0] + " / " + quest.m_nl_ItemCount_Max[0];
    //                    m_TMP_Quest_Information_Content_Description_2.text += "[";
    //                    if (quest.m_nl_ItemCode[i] < 1000)
    //                    {
    //                        if (ItemManager.instance.m_Dictionary_MonsterDrop_Etc.ContainsKey(quest.m_nl_ItemCode[i]) == true)
    //                        {
    //                            m_TMP_Quest_Information_Content_Description_2.text += ItemManager.instance.m_Dictionary_MonsterDrop_Etc[quest.m_nl_ItemCode[i]].m_sItemName;
    //                        }
    //                        else if (ItemManager.instance.m_Dictionary_Collection_Etc.ContainsKey(quest.m_nl_ItemCode[i]) == true)
    //                        {
    //                            m_TMP_Quest_Information_Content_Description_2.text += ItemManager.instance.m_Dictionary_Collection_Etc[quest.m_nl_ItemCode[i]].m_sItemName;
    //                        }
    //                        else if (ItemManager.instance.m_Dictionary_QuestReward_Etc.ContainsKey(quest.m_nl_ItemCode[i]) == true)
    //                        {
    //                            m_TMP_Quest_Information_Content_Description_2.text += ItemManager.instance.m_Dictionary_QuestReward_Etc[quest.m_nl_ItemCode[i]].m_sItemName;
    //                        }
    //                    }
    //                    else if (quest.m_nl_ItemCode[i] < 7000)
    //                    {
    //                        if (ItemManager.instance.m_Dictionary_MonsterDrop_Equip.ContainsKey(quest.m_nl_ItemCode[i]) == true)
    //                        {
    //                            m_TMP_Quest_Information_Content_Description_2.text += ItemManager.instance.m_Dictionary_MonsterDrop_Equip[quest.m_nl_ItemCode[i]].m_sItemName;
    //                        }
    //                        else if (ItemManager.instance.m_Dictionary_Collection_Equip.ContainsKey(quest.m_nl_ItemCode[i]) == true)
    //                        {
    //                            m_TMP_Quest_Information_Content_Description_2.text += ItemManager.instance.m_Dictionary_Collection_Equip[quest.m_nl_ItemCode[i]].m_sItemName;
    //                        }
    //                        else if (ItemManager.instance.m_Dictionary_QuestReward_Equip.ContainsKey(quest.m_nl_ItemCode[i]) == true)
    //                        {
    //                            m_TMP_Quest_Information_Content_Description_2.text += ItemManager.instance.m_Dictionary_QuestReward_Equip[quest.m_nl_ItemCode[i]].m_sItemName;
    //                        }
    //                    }
    //                    else
    //                    {
    //                        if (ItemManager.instance.m_Dictionary_MonsterDrop_Use.ContainsKey(quest.m_nl_ItemCode[i]) == true)
    //                        {
    //                            m_TMP_Quest_Information_Content_Description_2.text += ItemManager.instance.m_Dictionary_MonsterDrop_Use[quest.m_nl_ItemCode[i]].m_sItemName;
    //                        }
    //                        else if (ItemManager.instance.m_Dictionary_Collection_Use.ContainsKey(quest.m_nl_ItemCode[i]) == true)
    //                        {
    //                            m_TMP_Quest_Information_Content_Description_2.text += ItemManager.instance.m_Dictionary_Collection_Use[quest.m_nl_ItemCode[i]].m_sItemName;
    //                        }
    //                        else if (ItemManager.instance.m_Dictionary_QuestReward_Use.ContainsKey(quest.m_nl_ItemCode[i]) == true)
    //                        {
    //                            m_TMP_Quest_Information_Content_Description_2.text += ItemManager.instance.m_Dictionary_QuestReward_Use[quest.m_nl_ItemCode[i]].m_sItemName;
    //                        }
    //                    }
    //                    m_TMP_Quest_Information_Content_Description_2.text += "]" + quest.m_nl_ItemCount_Current[i] + " / " + quest.m_nl_ItemCount_Max[i];
    //                }
    //            }
    //            else
    //                m_TMP_Quest_Information_Content_Description_2.text = "???";
    //        }
    //    }
    //    else
    //    {
    //        m_TMP_Quest_Information_Content_Description_1.text = quest.m_sQuest_Information_Clear;

    //        m_TMP_Quest_Information_Content_Description_2.text = "";
    //        m_TMP_Quest_Information_Content_Description_2.text += "수집 현황: \n";
    //        for (int i = 0; i < quest.m_nl_ItemCode.Count; i++)
    //        {
    //            if (i != 0)
    //                m_TMP_Quest_Information_Content_Description_2.text += ", \n";
    //            m_TMP_Quest_Information_Content_Description_2.text += "[";
    //            if (quest.m_nl_ItemCode[i] < 1000)
    //            {
    //                if (ItemManager.instance.m_Dictionary_MonsterDrop_Etc.ContainsKey(quest.m_nl_ItemCode[i]) == true)
    //                {
    //                    m_TMP_Quest_Information_Content_Description_2.text += ItemManager.instance.m_Dictionary_MonsterDrop_Etc[quest.m_nl_ItemCode[i]].m_sItemName;
    //                }
    //                else if (ItemManager.instance.m_Dictionary_Collection_Etc.ContainsKey(quest.m_nl_ItemCode[i]) == true)
    //                {
    //                    m_TMP_Quest_Information_Content_Description_2.text += ItemManager.instance.m_Dictionary_Collection_Etc[quest.m_nl_ItemCode[i]].m_sItemName;
    //                }
    //                else if (ItemManager.instance.m_Dictionary_QuestReward_Etc.ContainsKey(quest.m_nl_ItemCode[i]) == true)
    //                {
    //                    m_TMP_Quest_Information_Content_Description_2.text += ItemManager.instance.m_Dictionary_QuestReward_Etc[quest.m_nl_ItemCode[i]].m_sItemName;
    //                }
    //            }
    //            else if (quest.m_nl_ItemCode[i] < 7000)
    //            {
    //                if (ItemManager.instance.m_Dictionary_MonsterDrop_Equip.ContainsKey(quest.m_nl_ItemCode[i]) == true)
    //                {
    //                    m_TMP_Quest_Information_Content_Description_2.text += ItemManager.instance.m_Dictionary_MonsterDrop_Equip[quest.m_nl_ItemCode[i]].m_sItemName;
    //                }
    //                else if (ItemManager.instance.m_Dictionary_Collection_Equip.ContainsKey(quest.m_nl_ItemCode[i]) == true)
    //                {
    //                    m_TMP_Quest_Information_Content_Description_2.text += ItemManager.instance.m_Dictionary_Collection_Equip[quest.m_nl_ItemCode[i]].m_sItemName;
    //                }
    //                else if (ItemManager.instance.m_Dictionary_QuestReward_Equip.ContainsKey(quest.m_nl_ItemCode[i]) == true)
    //                {
    //                    m_TMP_Quest_Information_Content_Description_2.text += ItemManager.instance.m_Dictionary_QuestReward_Equip[quest.m_nl_ItemCode[i]].m_sItemName;
    //                }
    //            }
    //            else
    //            {
    //                if (ItemManager.instance.m_Dictionary_MonsterDrop_Use.ContainsKey(quest.m_nl_ItemCode[i]) == true)
    //                {
    //                    m_TMP_Quest_Information_Content_Description_2.text += ItemManager.instance.m_Dictionary_MonsterDrop_Use[quest.m_nl_ItemCode[i]].m_sItemName;
    //                }
    //                else if (ItemManager.instance.m_Dictionary_Collection_Use.ContainsKey(quest.m_nl_ItemCode[i]) == true)
    //                {
    //                    m_TMP_Quest_Information_Content_Description_2.text += ItemManager.instance.m_Dictionary_Collection_Use[quest.m_nl_ItemCode[i]].m_sItemName;
    //                }
    //                else if (ItemManager.instance.m_Dictionary_QuestReward_Use.ContainsKey(quest.m_nl_ItemCode[i]) == true)
    //                {
    //                    m_TMP_Quest_Information_Content_Description_2.text += ItemManager.instance.m_Dictionary_QuestReward_Use[quest.m_nl_ItemCode[i]].m_sItemName;
    //                }
    //            }
    //            m_TMP_Quest_Information_Content_Description_2.text += "]" + quest.m_nl_ItemCount_Current[i] + " / " + quest.m_nl_ItemCount_Max[i];
    //        }
    //    }



    //    m_IMG_Quest_Information_Content_NPC.sprite = NPCManager_Total.m_Dictionary_NPC[quest.m_nNPC].m_Sprite_NPC;
    //}
    //public void UpdateQuestInformation(Quest_CONVERSATION quest, int num)
    //{
    //    m_TMP_Quest_Information_Content_Name.text = quest.m_sQuest_Title;

    //    if (num == 1)
    //    {
    //        if (quest.m_bCondition == false)
    //            m_TMP_Quest_Information_Content_Description_1.text = quest.m_sQuest_Information_Process;
    //        else
    //            m_TMP_Quest_Information_Content_Description_1.text = quest.m_sQuest_Information_Condition;

    //        if (quest.m_bQuest_Information_Process_Hide == false)
    //        {
    //            m_TMP_Quest_Information_Content_Description_2.text = "";
    //            m_TMP_Quest_Information_Content_Description_2.text += "퀘스트 진행중";
    //        }
    //        else
    //        {
    //            if (quest.m_bCondition == true)
    //            {
    //                m_TMP_Quest_Information_Content_Description_2.text = "";
    //                m_TMP_Quest_Information_Content_Description_2.text += "퀘스트 진행중";
    //            }
    //            else
    //                m_TMP_Quest_Information_Content_Description_2.text = "???";
    //        }
    //    }
    //    else
    //    {
    //        m_TMP_Quest_Information_Content_Description_1.text = quest.m_sQuest_Information_Clear;
    //        m_TMP_Quest_Information_Content_Description_2.text = "";
    //        m_TMP_Quest_Information_Content_Description_2.text += "퀘스트 완료";
    //    }

    //    m_IMG_Quest_Information_Content_NPC.sprite = NPCManager_Total.m_Dictionary_NPC[quest.m_nNPC].m_Sprite_NPC;
    //}
    //public void UpdateQuestInformation(Quest_ROLL quest, int num)
    //{
    //    m_TMP_Quest_Information_Content_Name.text = quest.m_sQuest_Title;

    //    if (num == 1)
    //    {
    //        if (quest.m_bCondition == false)
    //            m_TMP_Quest_Information_Content_Description_1.text = quest.m_sQuest_Information_Process;
    //        else
    //            m_TMP_Quest_Information_Content_Description_1.text = quest.m_sQuest_Information_Condition;

    //        if (quest.m_bQuest_Information_Process_Hide == false)
    //        {
    //            m_TMP_Quest_Information_Content_Description_2.text = "";
    //            m_TMP_Quest_Information_Content_Description_2.text += "구르기 횟수: ";
    //            m_TMP_Quest_Information_Content_Description_2.text += quest.m_nCount_Current + " / " + quest.m_nCount_Max;
    //        }
    //        else
    //        {
    //            if (quest.m_bCondition == true)
    //            {
    //                m_TMP_Quest_Information_Content_Description_2.text = "";
    //                m_TMP_Quest_Information_Content_Description_2.text += "구르기 횟수: ";
    //                m_TMP_Quest_Information_Content_Description_2.text += quest.m_nCount_Current + " / " + quest.m_nCount_Max;
    //            }
    //            else
    //                m_TMP_Quest_Information_Content_Description_2.text = "???";
    //        }
    //    }
    //    else
    //    {
    //        m_TMP_Quest_Information_Content_Description_1.text = quest.m_sQuest_Information_Clear;

    //        m_TMP_Quest_Information_Content_Description_2.text = "";
    //        m_TMP_Quest_Information_Content_Description_2.text += "구르기 횟수: ";
    //        m_TMP_Quest_Information_Content_Description_2.text += quest.m_nCount_Current + " / " + quest.m_nCount_Max;
    //    }

    //    m_IMG_Quest_Information_Content_NPC.sprite = NPCManager_Total.m_Dictionary_NPC[quest.m_nNPC].m_Sprite_NPC;
    //}
    //public void UpdateQuestInformation(Quest_ELIMINATE_MONSTER quest, int num)
    //{
    //    m_TMP_Quest_Information_Content_Name.text = quest.m_sQuest_Title;

    //    if (num == 1)
    //    {
    //        if (quest.m_bCondition == false)
    //            m_TMP_Quest_Information_Content_Description_1.text = quest.m_sQuest_Information_Process;
    //        else
    //            m_TMP_Quest_Information_Content_Description_1.text = quest.m_sQuest_Information_Condition;

    //        if (quest.m_bQuest_Information_Process_Hide == false)
    //        {
    //            m_TMP_Quest_Information_Content_Description_2.text = "";
    //            m_TMP_Quest_Information_Content_Description_2.text += "제거 현황: \n";
    //            for (int i = 0; i < quest.m_nl_MonsterCode.Count; i++)
    //            {
    //                if (i != 0)
    //                    m_TMP_Quest_Information_Content_Description_2.text += ", \n";
    //                m_TMP_Quest_Information_Content_Description_2.text += "[" + MonsterManager.m_Dictionary_Monster[quest.m_nl_MonsterCode[i]].m_sMonster_Name + "]" + quest.m_nl_Count_Current[i] + " / " + quest.m_nl_Count_Max[i];
    //            }
    //        }
    //        else
    //        {
    //            if (quest.m_bCondition == true)
    //            {
    //                m_TMP_Quest_Information_Content_Description_2.text = "";
    //                m_TMP_Quest_Information_Content_Description_2.text += "제거 현황: \n";
    //                for (int i = 0; i < quest.m_nl_MonsterCode.Count; i++)
    //                {
    //                    if (i != 0)
    //                        m_TMP_Quest_Information_Content_Description_2.text += ", \n";
    //                    m_TMP_Quest_Information_Content_Description_2.text += "[" + MonsterManager.m_Dictionary_Monster[quest.m_nl_MonsterCode[i]].m_sMonster_Name + "]" + quest.m_nl_Count_Current[i] + " / " + quest.m_nl_Count_Max[i];
    //                }
    //            }
    //            else
    //                m_TMP_Quest_Information_Content_Description_2.text = "???";
    //        }
    //    }
    //    else
    //    {
    //        m_TMP_Quest_Information_Content_Description_1.text = quest.m_sQuest_Information_Clear;

    //        m_TMP_Quest_Information_Content_Description_2.text = "";
    //        m_TMP_Quest_Information_Content_Description_2.text += "제거 현황: \n";
    //        for (int i = 0; i < quest.m_nl_MonsterCode.Count; i++)
    //        {
    //            if (i != 0)
    //                m_TMP_Quest_Information_Content_Description_2.text += ", \n";
    //            m_TMP_Quest_Information_Content_Description_2.text += "[" + MonsterManager.m_Dictionary_Monster[quest.m_nl_MonsterCode[i]].m_sMonster_Name + "]" + quest.m_nl_Count_Current[i] + " / " + quest.m_nl_Count_Max[i];
    //        }
    //    }

    //    m_IMG_Quest_Information_Content_NPC.sprite = NPCManager_Total.m_Dictionary_NPC[quest.m_nNPC_Clear].m_Sprite_NPC;

    //}
    //public void UpdateQuestInformation(Quest_ELIMINATE_TYPE quest, int num)
    //{
    //    m_TMP_Quest_Information_Content_Name.text = quest.m_sQuest_Title;

    //    if (num == 1)
    //    {
    //        if (quest.m_bCondition == false)
    //            m_TMP_Quest_Information_Content_Description_1.text = quest.m_sQuest_Information_Process;
    //        else
    //            m_TMP_Quest_Information_Content_Description_1.text = quest.m_sQuest_Information_Condition;

    //        if (quest.m_bQuest_Information_Process_Hide == false)
    //        {
    //            m_TMP_Quest_Information_Content_Description_2.text = "";
    //            m_TMP_Quest_Information_Content_Description_2.text += "제거 현황: ";
    //            m_TMP_Quest_Information_Content_Description_2.text += "[" + quest.m_eMonsterType + "]" + quest.m_nCount_Current + " / " + quest.m_nCount_Max;
    //        }
    //        else
    //        {
    //            if (quest.m_bCondition == true)
    //            {
    //                m_TMP_Quest_Information_Content_Description_2.text = "";
    //                m_TMP_Quest_Information_Content_Description_2.text += "제거 현황: ";
    //                m_TMP_Quest_Information_Content_Description_2.text += "[" + quest.m_eMonsterType + "]" + quest.m_nCount_Current + " / " + quest.m_nCount_Max;
    //            }
    //            else
    //                m_TMP_Quest_Information_Content_Description_2.text = "???";
    //        }
    //    }
    //    else
    //    {
    //        m_TMP_Quest_Information_Content_Description_1.text = quest.m_sQuest_Information_Clear;

    //        m_TMP_Quest_Information_Content_Description_2.text = "";
    //        m_TMP_Quest_Information_Content_Description_2.text += "제거 현황: ";
    //        m_TMP_Quest_Information_Content_Description_2.text += "[" + quest.m_eMonsterType + "]" + quest.m_nCount_Current + " / " + quest.m_nCount_Max;
    //    }

    //    m_IMG_Quest_Information_Content_NPC.sprite = NPCManager_Total.m_Dictionary_NPC[quest.m_nNPC].m_Sprite_NPC;
    //}
}
