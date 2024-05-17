using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestLoadmap_questpanel : MonoBehaviour
{
    [SerializeField] GameObject m_gBTN_Loadmap_Quest_Num;
    [SerializeField] Button m_BTN_Loadmap_Quest_Num;

    [SerializeField] GameObject m_gPanel_Loadmap_Quest_Possible;
    [SerializeField] GameObject m_gPanel_Loadmap_Quest_Progress;
    [SerializeField] GameObject m_gPanel_Loadmap_Quest_Clear;

    [SerializeField] GameObject m_gPanel_Loadmap_Quest_Except;

    [SerializeField] int m_nQuestCode;

    public void Update_QuestLoadmap_QuestPanel()
    {
        // 퀘스트 클리어 여부
        if (QuestManager.Instance.GetQuest_Info_CurrentCondition(m_nQuestCode) == true)
        {
            m_gPanel_Loadmap_Quest_Possible.SetActive(false);
            m_gPanel_Loadmap_Quest_Progress.SetActive(false);
            m_gPanel_Loadmap_Quest_Clear.SetActive(true);
            m_gPanel_Loadmap_Quest_Except.SetActive(false);
        }
        else
        {
            // 퀘스트 진행 여부
            if (QuestManager.Instance.GetQuest_Info_ProgressCondition(m_nQuestCode) == true)
            {
                m_gPanel_Loadmap_Quest_Possible.SetActive(false);
                m_gPanel_Loadmap_Quest_Progress.SetActive(true);
                m_gPanel_Loadmap_Quest_Clear.SetActive(false);
                m_gPanel_Loadmap_Quest_Except.SetActive(false);
            }
            else
            {
                // 퀘스트 사전 조건 판단
                if (QuestManager.Instance.GetQuest_Info_PreCondition(m_nQuestCode) == true)
                {
                    m_gPanel_Loadmap_Quest_Possible.SetActive(true);
                    m_gPanel_Loadmap_Quest_Progress.SetActive(false);
                    m_gPanel_Loadmap_Quest_Clear.SetActive(false);
                    m_gPanel_Loadmap_Quest_Except.SetActive(false);
                }
                else
                {
                    m_gPanel_Loadmap_Quest_Possible.SetActive(true);
                    m_gPanel_Loadmap_Quest_Progress.SetActive(false);
                    m_gPanel_Loadmap_Quest_Clear.SetActive(false);
                    m_gPanel_Loadmap_Quest_Except.SetActive(true);
                }
            }
        }
    }

    public void Press_Btn()
    {
        switch (QuestManager.Instance.GetQuestType(m_nQuestCode))
        {
            case E_QUEST_TYPE.KILL_MONSTER:
                {
                    if (QuestManager.Instance.GetQuest_KILL_MONSTER(m_nQuestCode).m_bClear == true)
                        GUIManager_Total.Instance.Update_Quest_Information(QuestManager.Instance.GetQuest_KILL_MONSTER(m_nQuestCode), 2);
                    else
                    {
                        if (QuestManager.Instance.GetQuest_KILL_MONSTER(m_nQuestCode).m_bProcess == true)
                            GUIManager_Total.Instance.Update_Quest_Information(QuestManager.Instance.GetQuest_KILL_MONSTER(m_nQuestCode), 1);
                        else
                            GUIManager_Total.Instance.Update_Quest_Information(QuestManager.Instance.GetQuest_KILL_MONSTER(m_nQuestCode), 0);
                    }
                }
                break;
            case E_QUEST_TYPE.KILL_TYPE:
                {
                    if (QuestManager.Instance.GetQuest_KILL_TYPE(m_nQuestCode).m_bClear == true)
                        GUIManager_Total.Instance.Update_Quest_Information(QuestManager.Instance.GetQuest_KILL_TYPE(m_nQuestCode), 2);
                    else
                    {
                        if (QuestManager.Instance.GetQuest_KILL_TYPE(m_nQuestCode).m_bProcess == true)
                            GUIManager_Total.Instance.Update_Quest_Information(QuestManager.Instance.GetQuest_KILL_TYPE(m_nQuestCode), 1);
                        else
                            GUIManager_Total.Instance.Update_Quest_Information(QuestManager.Instance.GetQuest_KILL_TYPE(m_nQuestCode), 0);
                    }
                }
                break;
            case E_QUEST_TYPE.GOAWAY_MONSTER:
                {
                    if (QuestManager.Instance.GetQuest_GOAWAY_MONSTER(m_nQuestCode).m_bClear == true)
                        GUIManager_Total.Instance.Update_Quest_Information(QuestManager.Instance.GetQuest_GOAWAY_MONSTER(m_nQuestCode), 2);
                    else
                    {
                        if (QuestManager.Instance.GetQuest_GOAWAY_MONSTER(m_nQuestCode).m_bProcess == true)
                            GUIManager_Total.Instance.Update_Quest_Information(QuestManager.Instance.GetQuest_GOAWAY_MONSTER(m_nQuestCode), 1);
                        else
                            GUIManager_Total.Instance.Update_Quest_Information(QuestManager.Instance.GetQuest_GOAWAY_MONSTER(m_nQuestCode), 0);
                    }
                }
                break;
            case E_QUEST_TYPE.GOAWAY_TYPE:
                {
                    if (QuestManager.Instance.GetQuest_GOAWAY_TYPE(m_nQuestCode).m_bClear == true)
                        GUIManager_Total.Instance.Update_Quest_Information(QuestManager.Instance.GetQuest_GOAWAY_TYPE(m_nQuestCode), 2);
                    else
                    {
                        if (QuestManager.Instance.GetQuest_GOAWAY_TYPE(m_nQuestCode).m_bProcess == true)
                            GUIManager_Total.Instance.Update_Quest_Information(QuestManager.Instance.GetQuest_GOAWAY_TYPE(m_nQuestCode), 1);
                        else
                            GUIManager_Total.Instance.Update_Quest_Information(QuestManager.Instance.GetQuest_GOAWAY_TYPE(m_nQuestCode), 0);
                    }
                }
                break;
            case E_QUEST_TYPE.COLLECT:
                {
                    if (QuestManager.Instance.GetQuest_COLLECT(m_nQuestCode).m_bClear == true)
                        GUIManager_Total.Instance.Update_Quest_Information(QuestManager.Instance.GetQuest_COLLECT(m_nQuestCode), 2);
                    else
                    {
                        if (QuestManager.Instance.GetQuest_COLLECT(m_nQuestCode).m_bProcess == true)
                            GUIManager_Total.Instance.Update_Quest_Information(QuestManager.Instance.GetQuest_COLLECT(m_nQuestCode), 1);
                        else
                            GUIManager_Total.Instance.Update_Quest_Information(QuestManager.Instance.GetQuest_COLLECT(m_nQuestCode), 0);
                    }
                }
                break;
            case E_QUEST_TYPE.CONVERSATION:
                {
                    if (QuestManager.Instance.GetQuest_CONVERSATION(m_nQuestCode).m_bClear == true)
                        GUIManager_Total.Instance.Update_Quest_Information(QuestManager.Instance.GetQuest_CONVERSATION(m_nQuestCode), 2);
                    else
                    {
                        if (QuestManager.Instance.GetQuest_CONVERSATION(m_nQuestCode).m_bProcess == true)
                            GUIManager_Total.Instance.Update_Quest_Information(QuestManager.Instance.GetQuest_CONVERSATION(m_nQuestCode), 1);
                        else
                            GUIManager_Total.Instance.Update_Quest_Information(QuestManager.Instance.GetQuest_CONVERSATION(m_nQuestCode), 0);
                    }
                }
                break;
            case E_QUEST_TYPE.ROLL:
                {
                    if (QuestManager.Instance.GetQuest_ROLL(m_nQuestCode).m_bClear == true)
                        GUIManager_Total.Instance.Update_Quest_Information(QuestManager.Instance.GetQuest_ROLL(m_nQuestCode), 2);
                    else
                    {
                        if (QuestManager.Instance.GetQuest_ROLL(m_nQuestCode).m_bProcess == true)
                            GUIManager_Total.Instance.Update_Quest_Information(QuestManager.Instance.GetQuest_ROLL(m_nQuestCode), 1);
                        else
                            GUIManager_Total.Instance.Update_Quest_Information(QuestManager.Instance.GetQuest_ROLL(m_nQuestCode), 0);
                    }
                }
                break;
            case E_QUEST_TYPE.ELIMINATE_MONSTER:
                {
                    if (QuestManager.Instance.GetQuest_ELIMINATE_MONSTER(m_nQuestCode).m_bClear == true)
                        GUIManager_Total.Instance.Update_Quest_Information(QuestManager.Instance.GetQuest_ELIMINATE_MONSTER(m_nQuestCode), 2);
                    else
                    {
                        if (QuestManager.Instance.GetQuest_ELIMINATE_MONSTER(m_nQuestCode).m_bProcess == true)
                            GUIManager_Total.Instance.Update_Quest_Information(QuestManager.Instance.GetQuest_ELIMINATE_MONSTER(m_nQuestCode), 1);
                        else
                            GUIManager_Total.Instance.Update_Quest_Information(QuestManager.Instance.GetQuest_ELIMINATE_MONSTER(m_nQuestCode), 0);
                    }
                }
                break;
            case E_QUEST_TYPE.ELIMINATE_TYPE:
                {
                    if (QuestManager.Instance.GetQuest_ELIMINATE_TYPE(m_nQuestCode).m_bClear == true)
                        GUIManager_Total.Instance.Update_Quest_Information(QuestManager.Instance.GetQuest_ELIMINATE_TYPE(m_nQuestCode), 2);
                    else
                    {
                        if (QuestManager.Instance.GetQuest_ELIMINATE_TYPE(m_nQuestCode).m_bProcess == true)
                            GUIManager_Total.Instance.Update_Quest_Information(QuestManager.Instance.GetQuest_ELIMINATE_TYPE(m_nQuestCode), 1);
                        else
                            GUIManager_Total.Instance.Update_Quest_Information(QuestManager.Instance.GetQuest_ELIMINATE_TYPE(m_nQuestCode), 0);
                    }
                }
                break;
        }
    }
}
