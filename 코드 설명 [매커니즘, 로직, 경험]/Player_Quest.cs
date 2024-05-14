﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class Player_Quest : MonoBehaviour
{
    // 진행중인 퀘스트
    public static List<Quest_KILL_MONSTER> m_lQuestList_Progress_KILL_MONSTER;
    public static List<Quest_KILL_TYPE> m_lQuestList_Progress_KILL_TYPE;
    public static List<Quest_GOAWAY_MONSTER> m_lQuestList_Progress_GOAWAY_MONSTER;
    public static List<Quest_GOAWAY_TYPE> m_lQuestList_Progress_GOAWAY_TYPE;
    public static List<Quest_COLLECT> m_lQuestList_Progress_COLLECT;
    public static List<Quest_CONVERSATION> m_lQuestList_Progress_CONVERSATION;
    public static List<Quest_ROLL> m_lQuestList_Progress_ROLL;
    public static List<Quest_ELIMINATE_MONSTER> m_lQuestList_Progress_ELIMINATE_MONSTER;
    public static List<Quest_ELIMINATE_TYPE> m_lQuestList_Progress_ELIMINATE_TYPE;
    // 완료한 퀘스트
    public static List<Quest_KILL_MONSTER> m_lQuestList_Complete_KILL_MONSTER;
    public static List<Quest_KILL_TYPE> m_lQuestList_Complete_KILL_TYPE;
    public static List<Quest_GOAWAY_MONSTER> m_lQuestList_Complete_GOAWAY_MONSTER;
    public static List<Quest_GOAWAY_TYPE> m_lQuestList_Complete_GOAWAY_TYPE;
    public static List<Quest_COLLECT> m_lQuestList_Complete_COLLECT;
    public static List<Quest_CONVERSATION> m_lQuestList_Complete_CONVERSATION;
    public static List<Quest_ROLL> m_lQuestList_Complete_ROLL;
    public static List<Quest_ELIMINATE_MONSTER> m_lQuestList_Complete_ELIMINATE_MONSTER;
    public static List<Quest_ELIMINATE_TYPE> m_lQuestList_Complete_ELIMINATE_TYPE;

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

    // 토벌 퀘스트
    public void QuestUpdate_Kill(E_MONSTER_KIND mk, int code)
    {
        for (int i = 0; i < m_lQuestList_Progress_KILL_MONSTER.Count; i++)
        {
            var item = m_lQuestList_Progress_KILL_MONSTER[i].m_sQuest_Title.Split('\n');
            if (m_lQuestList_Progress_KILL_MONSTER[i].m_bCondition == false)
            {
                if (m_lQuestList_Progress_KILL_MONSTER[i].Check_KILL_MONSTER(code) == true)
                {
                    for (int j = 0; j < m_lQuestList_Progress_KILL_MONSTER[i].m_nl_Count_Current.Count; j++)
                    {
                        if (m_lQuestList_Progress_KILL_MONSTER[i].m_nl_MonsterCode[j] == code)
                            if (m_lQuestList_Progress_KILL_MONSTER[i].m_nl_Count_Max[j] >= m_lQuestList_Progress_KILL_MONSTER[i].m_nl_Count_Current[j])
                            {
                                //GUIManager_Total.Instance.UpdateLog("[" + m_lQuestList_Progress_KILL_MONSTER[i].m_sQuest_Title + "][" + MonsterManager.m_Dictionary_Monster[code].m_sMonster_Name + "] " + m_lQuestList_Progress_KILL_MONSTER[i].m_nl_Count_Current[j] + " / " + m_lQuestList_Progress_KILL_MONSTER[i].m_nl_Count_Max[j]);
                                GUIManager_Total.Instance.UpdateLog("[" + item[1] + "][" + MonsterManager.m_Dictionary_Monster[code].m_sMonster_Name + "] " + m_lQuestList_Progress_KILL_MONSTER[i].m_nl_Count_Current[j] + " / " + m_lQuestList_Progress_KILL_MONSTER[i].m_nl_Count_Max[j]);
                            }
                    }
                }
                if (m_lQuestList_Progress_KILL_MONSTER[i].m_bCondition == true)
                {
                    //GUIManager_Total.Instance.UpdateLog("[" + m_lQuestList_Progress_KILL_MONSTER[i].m_sQuest_Title + "] 완료");
                    GUIManager_Total.Instance.UpdateLog("[" + item[1] + "] 완료");
                    GUIManager_Total.Instance.Display_GUI_QuestStateInfo(m_lQuestList_Progress_KILL_MONSTER[i]);
                }
            }
            if (NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_KILL_MONSTER[i].m_nNPC] != null)
            {
                if (m_lQuestList_Progress_KILL_MONSTER[i].m_nNPC == m_lQuestList_Progress_KILL_MONSTER[i].m_nNPC_Clear)
                {
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_KILL_MONSTER[i].m_nNPC].UpdateIcon();
                }
                else
                {
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_KILL_MONSTER[i].m_nNPC].UpdateIcon();
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_KILL_MONSTER[i].m_nNPC_Clear].UpdateIcon();
                }
            }

            GUIManager_Total.Instance.Update_Quest_Information(m_lQuestList_Progress_KILL_MONSTER[i], 1);
        }
        for (int i = 0; i < m_lQuestList_Progress_KILL_TYPE.Count; i++)
        {
            var item = m_lQuestList_Progress_KILL_TYPE[i].m_sQuest_Title.Split('\n');
            if (m_lQuestList_Progress_KILL_TYPE[i].m_bCondition == false)
            {
                if (m_lQuestList_Progress_KILL_TYPE[i].Check_KILL_TYPE(mk) == true)
                {
                    if (m_lQuestList_Progress_KILL_TYPE[i].m_nCount_Max != m_lQuestList_Progress_KILL_TYPE[i].m_nCount_Current)
                    {
                        //GUIManager_Total.Instance.UpdateLog("[" + m_lQuestList_Progress_KILL_TYPE[i].m_sQuest_Title + "] " + m_lQuestList_Progress_KILL_TYPE[i].m_nCount_Current + " / " + m_lQuestList_Progress_KILL_TYPE[i].m_nCount_Max);
                        GUIManager_Total.Instance.UpdateLog("[" + item[1] + "] " + "[" + m_lQuestList_Progress_KILL_TYPE[i].m_eMonsterType + "] "+ m_lQuestList_Progress_KILL_TYPE[i].m_nCount_Current + " / " + m_lQuestList_Progress_KILL_TYPE[i].m_nCount_Max);

                    }
                }
                if (m_lQuestList_Progress_KILL_TYPE[i].m_bCondition == true)
                {
                    //GUIManager_Total.Instance.UpdateLog("[" + m_lQuestList_Progress_KILL_TYPE[i].m_sQuest_Title + "] 완료");
                    GUIManager_Total.Instance.UpdateLog("[" + item[1] + "] 완료");
                    GUIManager_Total.Instance.Display_GUI_QuestStateInfo(m_lQuestList_Progress_KILL_TYPE[i]);
                }
            }

            if (NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_KILL_TYPE[i].m_nNPC] != null)
            {
                if (m_lQuestList_Progress_KILL_TYPE[i].m_nNPC == m_lQuestList_Progress_KILL_TYPE[i].m_nNPC_Clear)
                {
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_KILL_TYPE[i].m_nNPC].UpdateIcon();
                }
                else
                {
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_KILL_TYPE[i].m_nNPC].UpdateIcon();
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_KILL_TYPE[i].m_nNPC_Clear].UpdateIcon();
                }
            }


            GUIManager_Total.Instance.Update_Quest_Information(m_lQuestList_Progress_KILL_TYPE[i], 1);
        }
    }

    // GOAWAY 퀘스트
    public void QuestUpdate_Goaway(E_MONSTER_KIND mk, int code)
    {
        for (int i = 0; i < m_lQuestList_Progress_GOAWAY_MONSTER.Count; i++)
        {
            var item = m_lQuestList_Progress_GOAWAY_MONSTER[i].m_sQuest_Title.Split('\n');
            if (m_lQuestList_Progress_GOAWAY_MONSTER[i].m_bCondition == false)
            {
                if (m_lQuestList_Progress_GOAWAY_MONSTER[i].Check_GOAWAY_MONSTER(code) == true)
                {
                    for (int j = 0; j < m_lQuestList_Progress_GOAWAY_MONSTER[i].m_nl_Count_Current.Count; j++)
                    {
                        if (m_lQuestList_Progress_GOAWAY_MONSTER[i].m_nl_MonsterCode[j] == code)
                            if (m_lQuestList_Progress_GOAWAY_MONSTER[i].m_nl_Count_Max[j] >= m_lQuestList_Progress_GOAWAY_MONSTER[i].m_nl_Count_Current[j])
                            {
                                //GUIManager_Total.Instance.UpdateLog("[" + m_lQuestList_Progress_GOAWAY_MONSTER[i].m_sQuest_Title + "][" + MonsterManager.m_Dictionary_Monster[code].m_sMonster_Name + "] " + m_lQuestList_Progress_GOAWAY_MONSTER[i].m_nl_Count_Current[j] + " / " + m_lQuestList_Progress_GOAWAY_MONSTER[i].m_nl_Count_Max[j]);
                                GUIManager_Total.Instance.UpdateLog("[" + item[1] + "][" + MonsterManager.m_Dictionary_Monster[code].m_sMonster_Name + "] " + m_lQuestList_Progress_GOAWAY_MONSTER[i].m_nl_Count_Current[j] + " / " + m_lQuestList_Progress_GOAWAY_MONSTER[i].m_nl_Count_Max[j]);

                            }
                    }
                }
                if (m_lQuestList_Progress_GOAWAY_MONSTER[i].m_bCondition == true)
                {
                    //GUIManager_Total.Instance.UpdateLog("[" + m_lQuestList_Progress_GOAWAY_MONSTER[i].m_sQuest_Title + "] 완료");
                    GUIManager_Total.Instance.UpdateLog("[" + item[1] + "] 완료");
                    GUIManager_Total.Instance.Display_GUI_QuestStateInfo(m_lQuestList_Progress_GOAWAY_MONSTER[i]);

                }
            }

            if (NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_GOAWAY_MONSTER[i].m_nNPC] != null)
            {
                if (m_lQuestList_Progress_GOAWAY_MONSTER[i].m_nNPC == m_lQuestList_Progress_GOAWAY_MONSTER[i].m_nNPC_Clear)
                {
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_GOAWAY_MONSTER[i].m_nNPC].UpdateIcon();
                }
                else
                {
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_GOAWAY_MONSTER[i].m_nNPC].UpdateIcon();
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_GOAWAY_MONSTER[i].m_nNPC_Clear].UpdateIcon();
                }
            }

            GUIManager_Total.Instance.Update_Quest_Information(m_lQuestList_Progress_GOAWAY_MONSTER[i], 1);
        }
        for (int i = 0; i < m_lQuestList_Progress_GOAWAY_TYPE.Count; i++)
        {
            var item = m_lQuestList_Progress_GOAWAY_TYPE[i].m_sQuest_Title.Split('\n');
            if (m_lQuestList_Progress_GOAWAY_TYPE[i].m_bCondition == false)
            {
                if (m_lQuestList_Progress_GOAWAY_TYPE[i].Check_GOAWAY_TYPE(mk) == true)
                {
                    if (m_lQuestList_Progress_GOAWAY_TYPE[i].m_nCount_Max != m_lQuestList_Progress_GOAWAY_TYPE[i].m_nCount_Current)
                    {
                        //GUIManager_Total.Instance.UpdateLog("[" + m_lQuestList_Progress_GOAWAY_TYPE[i].m_sQuest_Title + "] " + m_lQuestList_Progress_GOAWAY_TYPE[i].m_nCount_Current + " / " + m_lQuestList_Progress_GOAWAY_TYPE[i].m_nCount_Max);
                        GUIManager_Total.Instance.UpdateLog("[" + item[1] + "] " + "[" + m_lQuestList_Progress_GOAWAY_TYPE[i].m_eMonsterType + "] "+ m_lQuestList_Progress_GOAWAY_TYPE[i].m_nCount_Current + " / " + m_lQuestList_Progress_GOAWAY_TYPE[i].m_nCount_Max);
                    }
                }
                if (m_lQuestList_Progress_GOAWAY_TYPE[i].m_bCondition == true)
                {
                    //GUIManager_Total.Instance.UpdateLog("[" + m_lQuestList_Progress_GOAWAY_TYPE[i].m_sQuest_Title + "] 완료");
                    GUIManager_Total.Instance.UpdateLog("[" + item[1] + "] 완료");
                    GUIManager_Total.Instance.Display_GUI_QuestStateInfo(m_lQuestList_Progress_GOAWAY_TYPE[i]);
                }
            }

            if (NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_GOAWAY_TYPE[i].m_nNPC] != null)
            {
                if (m_lQuestList_Progress_GOAWAY_TYPE[i].m_nNPC == m_lQuestList_Progress_GOAWAY_TYPE[i].m_nNPC_Clear)
                {
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_GOAWAY_TYPE[i].m_nNPC].UpdateIcon();
                }
                else
                {
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_GOAWAY_TYPE[i].m_nNPC].UpdateIcon();
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_GOAWAY_TYPE[i].m_nNPC_Clear].UpdateIcon();
                }
            }

            GUIManager_Total.Instance.Update_Quest_Information(m_lQuestList_Progress_GOAWAY_TYPE[i], 1);
        }
    }

    // COLLECT 퀘스트
    public void QuestUpdate_Collect_NoDisplay()
    {
        for (int i = 0; i < m_lQuestList_Progress_COLLECT.Count; i++)
        {
            // Collect 퀘스트_타입
            if (m_lQuestList_Progress_COLLECT[i].m_eQuestType == E_QUEST_TYPE.COLLECT)
            {
                if (m_lQuestList_Progress_COLLECT[i].m_bCondition == false)
                {
                    if (m_lQuestList_Progress_COLLECT[i].Check_COLLECT() == true)
                    {
                        for (int j = 0; j < m_lQuestList_Progress_COLLECT[i].m_nl_ItemCode.Count; j++)
                        {
                        }
                    }
                    if (m_lQuestList_Progress_COLLECT[i].m_bCondition == true)
                    {
                        GUIManager_Total.Instance.Display_GUI_QuestStateInfo(m_lQuestList_Progress_COLLECT[i]);
                    }
                }
                else
                {
                    m_lQuestList_Progress_COLLECT[i].Check_COLLECT();
                }
            }

            if (NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_COLLECT[i].m_nNPC] != null)
            {
                if (m_lQuestList_Progress_COLLECT[i].m_nNPC == m_lQuestList_Progress_COLLECT[i].m_nNPC_Clear)
                {
                    if (NPCManager_Total.m_Dictionary_NPC.ContainsKey(m_lQuestList_Progress_COLLECT[i].m_nNPC) == true)
                    {
                        NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_COLLECT[i].m_nNPC].UpdateIcon();
                    }
                }
                else
                {
                    if (NPCManager_Total.m_Dictionary_NPC.ContainsKey(m_lQuestList_Progress_COLLECT[i].m_nNPC) == true)
                    {
                        NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_COLLECT[i].m_nNPC].UpdateIcon();
                        //if (NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_COLLECT[i].m_nNPC_Clear] != null)
                        NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_COLLECT[i].m_nNPC_Clear].UpdateIcon();
                    }
                }
            }

            GUIManager_Total.Instance.Update_Quest_Information(m_lQuestList_Progress_COLLECT[i], 1);
        }
    }
    // COLLECT 퀘스트
    public void QuestUpdate_Collect()
    {
        for (int i = 0; i < m_lQuestList_Progress_COLLECT.Count; i++)
        {
            var item = m_lQuestList_Progress_COLLECT[i].m_sQuest_Title.Split('\n');
            // Collect 퀘스트_타입
            if (m_lQuestList_Progress_COLLECT[i].m_eQuestType == E_QUEST_TYPE.COLLECT)
            {
                if (m_lQuestList_Progress_COLLECT[i].m_bCondition == false)
                {
                    if (m_lQuestList_Progress_COLLECT[i].Check_COLLECT() == true)
                    {
                        for (int j = 0; j < m_lQuestList_Progress_COLLECT[i].m_nl_ItemCode.Count; j++)
                        {
                            UnityEngine.Debug.Log("QuestUpdate_Collect !");
                            //GUIManager_Total.Instance.UpdateLog("[" + m_lQuestList_Progress_COLLECT[i].m_sQuest_Title + "] " + m_lQuestList_Progress_COLLECT[i].m_nl_ItemCount_Current[j] + " / " + m_lQuestList_Progress_COLLECT[i].m_nl_ItemCount_Max[j]);
                            GUIManager_Total.Instance.UpdateLog("[" + item[1] + "] " + m_lQuestList_Progress_COLLECT[i].m_nl_ItemCount_Current[j] + " / " + m_lQuestList_Progress_COLLECT[i].m_nl_ItemCount_Max[j]);
                        }
                    }
                    if (m_lQuestList_Progress_COLLECT[i].m_bCondition == true)
                    {
                        UnityEngine.Debug.Log("QuestUpdate_Collect !");
                        //GUIManager_Total.Instance.UpdateLog("[" + m_lQuestList_Progress_COLLECT[i].m_sQuest_Title + "] 완료");
                        GUIManager_Total.Instance.UpdateLog("[" + item[1] + "] 완료");
                        GUIManager_Total.Instance.Display_GUI_QuestStateInfo(m_lQuestList_Progress_COLLECT[i]);
                    }
                }
                else
                {
                    m_lQuestList_Progress_COLLECT[i].Check_COLLECT();
                }
            }

            if (NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_COLLECT[i].m_nNPC] != null)
            {
                if (m_lQuestList_Progress_COLLECT[i].m_nNPC == m_lQuestList_Progress_COLLECT[i].m_nNPC_Clear)
                {
                    if (NPCManager_Total.m_Dictionary_NPC.ContainsKey(m_lQuestList_Progress_COLLECT[i].m_nNPC) == true)
                    {
                        NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_COLLECT[i].m_nNPC].UpdateIcon();
                    }
                }
                else
                {
                    if (NPCManager_Total.m_Dictionary_NPC.ContainsKey(m_lQuestList_Progress_COLLECT[i].m_nNPC) == true)
                    {
                        NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_COLLECT[i].m_nNPC].UpdateIcon();
                        //if (NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_COLLECT[i].m_nNPC_Clear] != null)
                        NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_COLLECT[i].m_nNPC_Clear].UpdateIcon();
                    }
                }
            }

            GUIManager_Total.Instance.Update_Quest_Information(m_lQuestList_Progress_COLLECT[i], 1);
        }
    }
    public void QuestUpdate_Collect(Item item)
    {
        for (int i = 0; i < m_lQuestList_Progress_COLLECT.Count; i++)
        {
            var items = m_lQuestList_Progress_COLLECT[i].m_sQuest_Title.Split('\n');
            // Collect 퀘스트_타입
            if (m_lQuestList_Progress_COLLECT[i].m_eQuestType == E_QUEST_TYPE.COLLECT)
            {
                if (m_lQuestList_Progress_COLLECT[i].m_bCondition == false)
                {
                    //UnityEngine.Debug.Log(m_lQuestList_Progress_COLLECT[i].m_sQuest_Title + " / Y");
                    //if (m_lQuestList_Progress_COLLECT[i].Check_COLLECT() == true)
                    m_lQuestList_Progress_COLLECT[i].Check_COLLECT();
                    {
                        for (int j = 0; j < m_lQuestList_Progress_COLLECT[i].m_nl_ItemCode.Count; j++)
                            if (m_lQuestList_Progress_COLLECT[i].m_nl_ItemCode[j] == item.m_nItemCode)
                            {
                                //UnityEngine.Debug.Log(m_lQuestList_Progress_COLLECT[i].m_nl_ItemCount_Max[j] + " / " + m_lQuestList_Progress_COLLECT[i].m_nl_ItemCount_Current[j]);
                                //if (m_lQuestList_Progress_COLLECT[i].m_nl_ItemCount_Max[j] >= m_lQuestList_Progress_COLLECT[i].m_nl_ItemCount_Current[j])
                                {
                                    //GUIManager_Total.Instance.UpdateLog("[" + m_lQuestList_Progress_COLLECT[i].m_sQuest_Title + "][" + ItemManager.instance.Get_Item_Information(item.m_nItemCode).m_sItemName + "] " + m_lQuestList_Progress_COLLECT[i].m_nl_ItemCount_Current[j] + " / " + m_lQuestList_Progress_COLLECT[i].m_nl_ItemCount_Max[j]);
                                    GUIManager_Total.Instance.UpdateLog("[" + items[1] + "][" + ItemManager.instance.Get_Item_Information(item.m_nItemCode).m_sItemName + "] " + m_lQuestList_Progress_COLLECT[i].m_nl_ItemCount_Current[j] + " / " + m_lQuestList_Progress_COLLECT[i].m_nl_ItemCount_Max[j]);
                                    //UnityEngine.Debug.Log("O");
                                    //UnityEngine.Debug.Log("[" + items[1] + "][" + ItemManager.instance.Get_Item_Information(item.m_nItemCode).m_sItemName + "] " + m_lQuestList_Progress_COLLECT[i].m_nl_ItemCount_Current[j] + " / " + m_lQuestList_Progress_COLLECT[i].m_nl_ItemCount_Max[j]);
                                }
                            }    
                    }
                    if (m_lQuestList_Progress_COLLECT[i].m_bCondition == true)
                    {
                        //GUIManager_Total.Instance.UpdateLog("[" + m_lQuestList_Progress_COLLECT[i].m_sQuest_Title + "] 완료");
                        GUIManager_Total.Instance.UpdateLog("[" + items[1] + "] 완료");
                        GUIManager_Total.Instance.Display_GUI_QuestStateInfo(m_lQuestList_Progress_COLLECT[i]);
                    }
                }
                else
                {
                    //UnityEngine.Debug.Log(m_lQuestList_Progress_COLLECT[i].m_sQuest_Title + " / N");
                    // m_lQuestList_Progress_COLLECT[i].Check_COLLECT();

                    //m_lQuestList_Progress_COLLECT[i].Check_COLLECT();
                    //if (m_lQuestList_Progress_COLLECT[i].Check_COLLECT() == true)
                    m_lQuestList_Progress_COLLECT[i].Check_COLLECT();
                    {
                        for (int j = 0; j < m_lQuestList_Progress_COLLECT[i].m_nl_ItemCode.Count; j++)
                            if (m_lQuestList_Progress_COLLECT[i].m_nl_ItemCode[j] == item.m_nItemCode)
                            {
                                UnityEngine.Debug.Log(m_lQuestList_Progress_COLLECT[i].m_nl_ItemCount_Max[j] + " / " + m_lQuestList_Progress_COLLECT[i].m_nl_ItemCount_Current[j]);
                                //UnityEngine.Debug.Log(m_lQuestList_Progress_COLLECT[i].m_nl_ItemCount_Max[j] + " / " + m_lQuestList_Progress_COLLECT[i].m_nl_ItemCount_Current[j]);
                                if (m_lQuestList_Progress_COLLECT[i].m_nl_ItemCount_Max[j] >= m_lQuestList_Progress_COLLECT[i].m_nl_ItemCount_Current[j])
                                {
                                    //GUIManager_Total.Instance.UpdateLog("[" + m_lQuestList_Progress_COLLECT[i].m_sQuest_Title + "][" + ItemManager.instance.Get_Item_Information(item.m_nItemCode).m_sItemName + "] " + m_lQuestList_Progress_COLLECT[i].m_nl_ItemCount_Current[j] + " / " + m_lQuestList_Progress_COLLECT[i].m_nl_ItemCount_Max[j]);
                                    GUIManager_Total.Instance.UpdateLog("[" + items[1] + "][" + ItemManager.instance.Get_Item_Information(item.m_nItemCode).m_sItemName + "] " + m_lQuestList_Progress_COLLECT[i].m_nl_ItemCount_Current[j] + " / " + m_lQuestList_Progress_COLLECT[i].m_nl_ItemCount_Max[j]);
                                    //UnityEngine.Debug.Log("O");
                                    //UnityEngine.Debug.Log("[" + items[1] + "][" + ItemManager.instance.Get_Item_Information(item.m_nItemCode).m_sItemName + "] " + m_lQuestList_Progress_COLLECT[i].m_nl_ItemCount_Current[j] + " / " + m_lQuestList_Progress_COLLECT[i].m_nl_ItemCount_Max[j]);
                                }
                            }
                    }
                }
            }

            if (NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_COLLECT[i].m_nNPC] != null)
            {
                if (m_lQuestList_Progress_COLLECT[i].m_nNPC == m_lQuestList_Progress_COLLECT[i].m_nNPC_Clear)
                {
                    if (NPCManager_Total.m_Dictionary_NPC.ContainsKey(m_lQuestList_Progress_COLLECT[i].m_nNPC) == true)
                    {
                        NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_COLLECT[i].m_nNPC].UpdateIcon();
                    }
                }
                else
                {
                    if (NPCManager_Total.m_Dictionary_NPC.ContainsKey(m_lQuestList_Progress_COLLECT[i].m_nNPC) == true)
                    {
                        NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_COLLECT[i].m_nNPC].UpdateIcon();
                        NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_COLLECT[i].m_nNPC_Clear].UpdateIcon();
                    }
                }
            }

            GUIManager_Total.Instance.Update_Quest_Information(m_lQuestList_Progress_COLLECT[i], 1);
        }
    }

    // ROLL 퀘스트(구르기 한 횟수)
    public void QuestUpdate_Roll()
    {
        for (int i = 0; i < m_lQuestList_Progress_ROLL.Count; i++)
        {
            var item = m_lQuestList_Progress_ROLL[i].m_sQuest_Title.Split('\n');
            if (m_lQuestList_Progress_ROLL[i].m_eQuestType == E_QUEST_TYPE.ROLL)
            {
                if (m_lQuestList_Progress_ROLL[i].m_bCondition == false)
                {
                    if (m_lQuestList_Progress_ROLL[i].Check_ROLL() == true)
                    {
                        //GUIManager_Total.Instance.UpdateLog("[" + m_lQuestList_Progress_ROLL[i].m_sQuest_Title + "] " + m_lQuestList_Progress_ROLL[i].m_nCount_Current + " / " + m_lQuestList_Progress_ROLL[i].m_nCount_Max);
                        GUIManager_Total.Instance.UpdateLog("[" + item[1] + "] " + m_lQuestList_Progress_ROLL[i].m_nCount_Current + " / " + m_lQuestList_Progress_ROLL[i].m_nCount_Max);
                    }
                    if (m_lQuestList_Progress_ROLL[i].m_bCondition == true)
                    {
                        //GUIManager_Total.Instance.UpdateLog("[" + m_lQuestList_Progress_ROLL[i].m_sQuest_Title + "] 완료");
                        GUIManager_Total.Instance.UpdateLog("[" + item[1] + "] 완료");
                        GUIManager_Total.Instance.Display_GUI_QuestStateInfo(m_lQuestList_Progress_ROLL[i]);
                    }
                }
            }

            if (NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_ROLL[i].m_nNPC] != null)
            {
                if (m_lQuestList_Progress_ROLL[i].m_nNPC == m_lQuestList_Progress_ROLL[i].m_nNPC_Clear)
                {
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_ROLL[i].m_nNPC].UpdateIcon();
                }
                else
                {
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_ROLL[i].m_nNPC].UpdateIcon();
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_ROLL[i].m_nNPC_Clear].UpdateIcon();
                }
            }

            GUIManager_Total.Instance.Update_Quest_Information(m_lQuestList_Progress_ROLL[i], 1);
        }
    }

    // ELIMINATE 퀘스트
    public void QuestUpdate_Eliminate(E_MONSTER_KIND mk, int code)
    {
        for (int i = 0; i < m_lQuestList_Progress_ELIMINATE_MONSTER.Count; i++)
        {
            var item = m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_sQuest_Title.Split('\n');
            if (m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_bCondition == false)
            {
                if (m_lQuestList_Progress_ELIMINATE_MONSTER[i].Check_ELIMINATE_MONSTER(code) == true)
                {
                    for (int j = 0; j < m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_nl_Count_Current.Count; j++)
                    {
                        if (m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_nl_MonsterCode[j] == code)
                            if (m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_nl_Count_Max[j] >= m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_nl_Count_Current[j])
                            {
                                //GUIManager_Total.Instance.UpdateLog("[" + m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_sQuest_Title + "][" + MonsterManager.m_Dictionary_Monster[code].m_sMonster_Name + "] " + m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_nl_Count_Current[j] + " / " + m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_nl_Count_Max[j]);
                                GUIManager_Total.Instance.UpdateLog("[" + item[1] + "][" + MonsterManager.m_Dictionary_Monster[code].m_sMonster_Name + "] " + m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_nl_Count_Current[j] + " / " + m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_nl_Count_Max[j]);
                            }
                    }
                }
                if (m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_bCondition == true)
                {
                    //GUIManager_Total.Instance.UpdateLog("[" + m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_sQuest_Title + "] 완료");
                    GUIManager_Total.Instance.UpdateLog("[" + item[1] + "] 완료");
                    GUIManager_Total.Instance.Display_GUI_QuestStateInfo(m_lQuestList_Progress_ELIMINATE_MONSTER[i]);
                }
            }

            if (NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_nNPC] != null)
            {
                if (m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_nNPC == m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_nNPC_Clear)
                {
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_nNPC].UpdateIcon();
                }
                else
                {
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_nNPC].UpdateIcon();
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_nNPC_Clear].UpdateIcon();
                }
            }

            GUIManager_Total.Instance.Update_Quest_Information(m_lQuestList_Progress_ELIMINATE_MONSTER[i], 1);
        }
        for (int i = 0; i < m_lQuestList_Progress_ELIMINATE_TYPE.Count; i++)
        {
            var item = m_lQuestList_Progress_ELIMINATE_TYPE[i].m_sQuest_Title.Split('\n');
            if (m_lQuestList_Progress_ELIMINATE_TYPE[i].m_bCondition == false)
            {
                if (m_lQuestList_Progress_ELIMINATE_TYPE[i].Check_ELIMINATE_TYPE(mk) == true)
                {
                    if (m_lQuestList_Progress_ELIMINATE_TYPE[i].m_nCount_Max != m_lQuestList_Progress_ELIMINATE_TYPE[i].m_nCount_Current)
                    {
                        //GUIManager_Total.Instance.UpdateLog("[" + m_lQuestList_Progress_ELIMINATE_TYPE[i].m_sQuest_Title + "] " + m_lQuestList_Progress_ELIMINATE_TYPE[i].m_nCount_Current + " / " + m_lQuestList_Progress_ELIMINATE_TYPE[i].m_nCount_Max);
                        GUIManager_Total.Instance.UpdateLog("[" + item[1] + "] " + "[" + m_lQuestList_Progress_ELIMINATE_TYPE[i].m_eMonsterType + "] " + m_lQuestList_Progress_ELIMINATE_TYPE[i].m_nCount_Current + " / " + m_lQuestList_Progress_ELIMINATE_TYPE[i].m_nCount_Max);
                    }
                }
                if (m_lQuestList_Progress_ELIMINATE_TYPE[i].m_bCondition == true)
                {
                    //GUIManager_Total.Instance.UpdateLog("[" + m_lQuestList_Progress_ELIMINATE_TYPE[i].m_sQuest_Title + "] 완료");
                    GUIManager_Total.Instance.UpdateLog("[" + item[1] + "] 완료");
                    GUIManager_Total.Instance.Display_GUI_QuestStateInfo(m_lQuestList_Progress_ELIMINATE_TYPE[i]);
                }
            }

            if (NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_ELIMINATE_TYPE[i].m_nNPC] != null)
            {
                if (m_lQuestList_Progress_ELIMINATE_TYPE[i].m_nNPC == m_lQuestList_Progress_ELIMINATE_TYPE[i].m_nNPC_Clear)
                {
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_ELIMINATE_TYPE[i].m_nNPC].UpdateIcon();
                }
                else
                {
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_ELIMINATE_TYPE[i].m_nNPC].UpdateIcon();
                    NPCManager_Total.m_Dictionary_NPC[m_lQuestList_Progress_ELIMINATE_TYPE[i].m_nNPC_Clear].UpdateIcon();
                }
            }

            GUIManager_Total.Instance.Update_Quest_Information(m_lQuestList_Progress_ELIMINATE_TYPE[i], 1);
        }
    }

    // 퀘스트 추가
    public void AddQuest(Quest_KILL_MONSTER quest)
    {
        quest.m_nQuestOrder = QuestManager.m_snQuest_ProcessOrder++;
        m_lQuestList_Progress_KILL_MONSTER.Add(quest);
        GUIManager_Total.Instance.Update_Quest(quest, 1);
        quest.m_bProcess = true;
        quest.m_bClear = false;
    }
    public void AddQuest(Quest_KILL_TYPE quest)
    {
        quest.m_nQuestOrder = QuestManager.m_snQuest_ProcessOrder++;
        m_lQuestList_Progress_KILL_TYPE.Add(quest);
        GUIManager_Total.Instance.Update_Quest(quest, 1);
        quest.m_bProcess = true;
        quest.m_bClear = false;
    }
    public void AddQuest(Quest_GOAWAY_MONSTER quest)
    {
        quest.m_nQuestOrder = QuestManager.m_snQuest_ProcessOrder++;
        m_lQuestList_Progress_GOAWAY_MONSTER.Add(quest);
        GUIManager_Total.Instance.Update_Quest(quest, 1);
        quest.m_bProcess = true;
        quest.m_bClear = false;
    }
    public void AddQuest(Quest_GOAWAY_TYPE quest)
    {
        quest.m_nQuestOrder = QuestManager.m_snQuest_ProcessOrder++;
        m_lQuestList_Progress_GOAWAY_TYPE.Add(quest);
        GUIManager_Total.Instance.Update_Quest(quest, 1);
        quest.m_bProcess = true;
        quest.m_bClear = false;
    }
    public void AddQuest(Quest_COLLECT quest)
    {
        quest.m_nQuestOrder = QuestManager.m_snQuest_ProcessOrder++;
        m_lQuestList_Progress_COLLECT.Add(quest);
        GUIManager_Total.Instance.Update_Quest(quest, 1);
        quest.m_bProcess = true;
        quest.m_bClear = false;
    }
    public void AddQuest(Quest_CONVERSATION quest)
    {
        quest.m_nQuestOrder = QuestManager.m_snQuest_ProcessOrder++;
        m_lQuestList_Progress_CONVERSATION.Add(quest);
        GUIManager_Total.Instance.Update_Quest(quest, 1);
        quest.m_bProcess = true;
        quest.m_bClear = false;
    }
    public void AddQuest(Quest_ROLL quest)
    {
        quest.m_nQuestOrder = QuestManager.m_snQuest_ProcessOrder++;
        m_lQuestList_Progress_ROLL.Add(quest);
        GUIManager_Total.Instance.Update_Quest(quest, 1);
        quest.m_bProcess = true;
        quest.m_bClear = false;
    }
    public void AddQuest(Quest_ELIMINATE_MONSTER quest)
    {
        quest.m_nQuestOrder = QuestManager.m_snQuest_ProcessOrder++;
        m_lQuestList_Progress_ELIMINATE_MONSTER.Add(quest);
        GUIManager_Total.Instance.Update_Quest(quest, 1);
        quest.m_bProcess = true;
        quest.m_bClear = false;
    }
    public void AddQuest(Quest_ELIMINATE_TYPE quest)
    {
        quest.m_nQuestOrder = QuestManager.m_snQuest_ProcessOrder++;
        m_lQuestList_Progress_ELIMINATE_TYPE.Add(quest);
        GUIManager_Total.Instance.Update_Quest(quest, 1);
        quest.m_bProcess = true;
        quest.m_bClear = false;
    }

    //public void RemoveQuest(Quest quest)
    //{
    //    for (int i = 0; i < m_lQuestList_Progress.Count; i++)
    //    {
    //        if (m_lQuestList_Progress[i] == quest)
    //        {
    //            m_lQuestList_Progress.RemoveAt(i);
    //            quest.m_bProcess = false;
    //            quest.m_bClear = true;
    //            break;
    //        }
    //    }
    //}
    //public void InitQuestAll()
    //{
    //    m_lQuestList_Progress.Clear();
    //    m_lQuestList_Complete.Clear();
    //}

    public void GetQuestReward(Quest_KILL_MONSTER quest)
    {
        for (int i = 0; i < m_lQuestList_Progress_KILL_MONSTER.Count; i++)
        {
            if (m_lQuestList_Progress_KILL_MONSTER[i].m_nQuest_Code == quest.m_nQuest_Code)
            {
                quest.m_nQuestOrder = QuestManager.m_snQuest_CompleteOrder++;
                m_lQuestList_Complete_KILL_MONSTER.Add(quest);
                m_lQuestList_Progress_KILL_MONSTER.RemoveAt(i);
                GUIManager_Total.Instance.UpdateLog(quest.m_sQuest_Title + "클리어");
                GUIManager_Total.Instance.Update_Quest(quest, 2);
                GUIManager_Total.Instance.UnDisplay_GUI_QuestStateInfo(quest);
                break;
            }
        }
    }
    public void GetQuestReward(Quest_KILL_TYPE quest)
    {
        for (int i = 0; i < m_lQuestList_Progress_KILL_TYPE.Count; i++)
        {
            if (m_lQuestList_Progress_KILL_TYPE[i].m_nQuest_Code == quest.m_nQuest_Code)
            {
                quest.m_nQuestOrder = QuestManager.m_snQuest_CompleteOrder++;
                m_lQuestList_Complete_KILL_TYPE.Add(quest);
                m_lQuestList_Progress_KILL_TYPE.RemoveAt(i);
                GUIManager_Total.Instance.UpdateLog(quest.m_sQuest_Title + "클리어");
                GUIManager_Total.Instance.Update_Quest(quest, 2);
                GUIManager_Total.Instance.UnDisplay_GUI_QuestStateInfo(quest);
                break;
            }
        }
    }
    public void GetQuestReward(Quest_GOAWAY_MONSTER quest)
    {
        for (int i = 0; i < m_lQuestList_Progress_GOAWAY_MONSTER.Count; i++)
        {
            if (m_lQuestList_Progress_GOAWAY_MONSTER[i].m_nQuest_Code == quest.m_nQuest_Code)
            {
                quest.m_nQuestOrder = QuestManager.m_snQuest_CompleteOrder++;
                m_lQuestList_Complete_GOAWAY_MONSTER.Add(quest);
                m_lQuestList_Progress_GOAWAY_MONSTER.RemoveAt(i);
                GUIManager_Total.Instance.UpdateLog(quest.m_sQuest_Title + "클리어");
                GUIManager_Total.Instance.Update_Quest(quest, 2);
                GUIManager_Total.Instance.UnDisplay_GUI_QuestStateInfo(quest);
                break;
            }
        }
    }
    public void GetQuestReward(Quest_GOAWAY_TYPE quest)
    {
        for (int i = 0; i < m_lQuestList_Progress_GOAWAY_TYPE.Count; i++)
        {
            if (m_lQuestList_Progress_GOAWAY_TYPE[i].m_nQuest_Code == quest.m_nQuest_Code)
            {
                quest.m_nQuestOrder = QuestManager.m_snQuest_CompleteOrder++;
                m_lQuestList_Complete_GOAWAY_TYPE.Add(quest);
                m_lQuestList_Progress_GOAWAY_TYPE.RemoveAt(i);
                GUIManager_Total.Instance.UpdateLog(quest.m_sQuest_Title + "클리어");
                GUIManager_Total.Instance.Update_Quest(quest, 2);
                GUIManager_Total.Instance.UnDisplay_GUI_QuestStateInfo(quest);
                break;
            }
        }
    }
    public void GetQuestReward(Quest_COLLECT quest)
    {
        for (int i = 0; i < m_lQuestList_Progress_COLLECT.Count; i++)
        {
            if (m_lQuestList_Progress_COLLECT[i].m_nQuest_Code == quest.m_nQuest_Code)
            {
                for (int j = 0; j < m_lQuestList_Progress_COLLECT[i].m_nl_ItemCode.Count; j++)
                {
                    m_lQuestList_Progress_COLLECT[i].m_nl_ItemCount_Current[j] = m_lQuestList_Progress_COLLECT[i].m_nl_ItemCount_Max[j];
                }
                quest.m_nQuestOrder = QuestManager.m_snQuest_CompleteOrder++;
                m_lQuestList_Complete_COLLECT.Add(quest);
                m_lQuestList_Progress_COLLECT.RemoveAt(i);
                GUIManager_Total.Instance.UpdateLog(quest.m_sQuest_Title + "클리어");
                GUIManager_Total.Instance.Update_Quest(quest, 2);
                GUIManager_Total.Instance.UnDisplay_GUI_QuestStateInfo(quest);
                break;
            }
        }
    }
    public void GetQuestReward(Quest_CONVERSATION quest)
    {
        for (int i = 0; i < m_lQuestList_Progress_CONVERSATION.Count; i++)
        {
            if (m_lQuestList_Progress_CONVERSATION[i].m_nQuest_Code == quest.m_nQuest_Code)
            {
                quest.m_nQuestOrder = QuestManager.m_snQuest_CompleteOrder++;
                m_lQuestList_Complete_CONVERSATION.Add(quest);
                m_lQuestList_Progress_CONVERSATION.RemoveAt(i);
                GUIManager_Total.Instance.UpdateLog(quest.m_sQuest_Title + "클리어");
                GUIManager_Total.Instance.Update_Quest(quest, 2);
                GUIManager_Total.Instance.UnDisplay_GUI_QuestStateInfo(quest);
                break;
            }
        }
    }
    public void GetQuestReward(Quest_ROLL quest)
    {
        for (int i = 0; i < m_lQuestList_Progress_ROLL.Count; i++)
        {
            if (m_lQuestList_Progress_ROLL[i].m_nQuest_Code == quest.m_nQuest_Code)
            {
                quest.m_nQuestOrder = QuestManager.m_snQuest_CompleteOrder++;
                m_lQuestList_Complete_ROLL.Add(quest);
                m_lQuestList_Progress_ROLL.RemoveAt(i);
                GUIManager_Total.Instance.UpdateLog(quest.m_sQuest_Title + "클리어");
                GUIManager_Total.Instance.Update_Quest(quest, 2);
                GUIManager_Total.Instance.UnDisplay_GUI_QuestStateInfo(quest);
                break;
            }
        }
    }
    public void GetQuestReward(Quest_ELIMINATE_MONSTER quest)
    {
        for (int i = 0; i < m_lQuestList_Progress_ELIMINATE_MONSTER.Count; i++)
        {
            if (m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_nQuest_Code == quest.m_nQuest_Code)
            {
                quest.m_nQuestOrder = QuestManager.m_snQuest_CompleteOrder++;
                m_lQuestList_Complete_ELIMINATE_MONSTER.Add(quest);
                m_lQuestList_Progress_ELIMINATE_MONSTER.RemoveAt(i);
                GUIManager_Total.Instance.UpdateLog(quest.m_sQuest_Title + "클리어");
                GUIManager_Total.Instance.Update_Quest(quest, 2);
                GUIManager_Total.Instance.UnDisplay_GUI_QuestStateInfo(quest);
                break;
            }
        }
    }
    public void GetQuestReward(Quest_ELIMINATE_TYPE quest)
    {
        for (int i = 0; i < m_lQuestList_Progress_ELIMINATE_TYPE.Count; i++)
        {
            if (m_lQuestList_Progress_ELIMINATE_TYPE[i].m_nQuest_Code == quest.m_nQuest_Code)
            {
                quest.m_nQuestOrder = QuestManager.m_snQuest_CompleteOrder++;
                m_lQuestList_Complete_ELIMINATE_TYPE.Add(quest);
                m_lQuestList_Progress_ELIMINATE_TYPE.RemoveAt(i);
                GUIManager_Total.Instance.UpdateLog(quest.m_sQuest_Title + "클리어");
                GUIManager_Total.Instance.Update_Quest(quest, 2);
                GUIManager_Total.Instance.UnDisplay_GUI_QuestStateInfo(quest);
                break;
            }
        }
    }
}
