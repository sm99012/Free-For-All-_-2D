using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class questpanel : MonoBehaviour
{
    // Quest 창에서 확인할 수 있는 Quest 정보.
    // 진행중인 퀘스트에는 퀘스트 요약 및 정보.
    // 완료한 퀘스트에는 퀘스트 요약이 제공된다.
    // 인스펙터 상에서 지정해줄것.
    [SerializeField] TextMeshProUGUI m_TMP_QuestTitle;
    [SerializeField] Button m_BTN_QuestInformation;

    [SerializeField] GameObject m_gPanel_Effect_Flesh;
    [SerializeField] Image m_IMG_Effect_Flesh;
    [SerializeField] GameObject m_gPanel_Quest_Content_Selected;

    // Quest 완료 가능 여부.
    bool m_bQuest_Clear_Possible = false;

    // Image 반짝임 효과.
    bool m_bAlpha_Up = false;

    [SerializeField] GameObject m_gPanel_QuestInformation;

    // 해당 Quest 패널의 Quest 정보.
    public E_QUEST_TYPE m_QuestType;
    public Quest_KILL_MONSTER m_Quest_KILL_MONSTER;
    public Quest_KILL_TYPE m_Quest_KILL_TYPE;
    public Quest_GOAWAY_MONSTER m_Quest_GOAWAY_MONSTER;
    public Quest_GOAWAY_TYPE m_Quest_GOAWAY_TYPE;
    public Quest_COLLECT m_Quest_COLLECT;
    public Quest_CONVERSATION m_Quest_CONVERSATION;
    public Quest_ROLL m_Quest_ROLL;
    public Quest_ELIMINATE_MONSTER m_Quest_ELIMINATE_MONSTER;
    public Quest_ELIMINATE_TYPE m_Quest_ELIMINATE_TYPE;

    public void Btn_Press_QuestInformation()
    {
        //if (m_Quest.m_bClear == true)
        //    GUIManager_Total.Instance.Update_Quest_Information(m_Quest, 2);
        //else
        //    GUIManager_Total.Instance.Update_Quest_Information(m_Quest, 1);

        //m_gPanel_QuestInformation.SetActive(true);

        // 이
        // 게
        // 최
        // 선
        // 인
        // 가
        // ?
        switch (m_QuestType)
        {
            case E_QUEST_TYPE.KILL_MONSTER:
                {
                    if (m_Quest_KILL_MONSTER.m_bClear == true)
                        GUIManager_Total.Instance.Update_Quest_Information(m_Quest_KILL_MONSTER, 2);
                    else
                    {
                        if (m_Quest_KILL_MONSTER.m_bProcess == true)
                            GUIManager_Total.Instance.Update_Quest_Information(m_Quest_KILL_MONSTER, 1);
                        else
                            GUIManager_Total.Instance.Update_Quest_Information(m_Quest_KILL_MONSTER, 0);
                    }
                } break;
            case E_QUEST_TYPE.KILL_TYPE:
                {
                    if (m_Quest_KILL_TYPE.m_bClear == true)
                        GUIManager_Total.Instance.Update_Quest_Information(m_Quest_KILL_TYPE, 2);
                    else
                    {
                        if (m_Quest_KILL_TYPE.m_bProcess == true)
                            GUIManager_Total.Instance.Update_Quest_Information(m_Quest_KILL_TYPE, 1);
                        else
                            GUIManager_Total.Instance.Update_Quest_Information(m_Quest_KILL_TYPE, 0);
                    }
                } break;
            case E_QUEST_TYPE.GOAWAY_MONSTER:
                {
                    if (m_Quest_GOAWAY_MONSTER.m_bClear == true)
                        GUIManager_Total.Instance.Update_Quest_Information(m_Quest_GOAWAY_MONSTER, 2);
                    else
                    {
                        if (m_Quest_GOAWAY_MONSTER.m_bProcess == true)
                            GUIManager_Total.Instance.Update_Quest_Information(m_Quest_GOAWAY_MONSTER, 1);
                        else
                            GUIManager_Total.Instance.Update_Quest_Information(m_Quest_GOAWAY_MONSTER, 0);
                    }
                } break;
            case E_QUEST_TYPE.GOAWAY_TYPE:
                {
                    if (m_Quest_GOAWAY_TYPE.m_bClear == true)
                        GUIManager_Total.Instance.Update_Quest_Information(m_Quest_GOAWAY_TYPE, 2);
                    else
                    {
                        if (m_Quest_GOAWAY_TYPE.m_bProcess == true)
                            GUIManager_Total.Instance.Update_Quest_Information(m_Quest_GOAWAY_TYPE, 1);
                        else
                            GUIManager_Total.Instance.Update_Quest_Information(m_Quest_GOAWAY_TYPE, 0);
                    }
                } break;
            case E_QUEST_TYPE.COLLECT:
                {
                    if (m_Quest_COLLECT.m_bClear == true)
                        GUIManager_Total.Instance.Update_Quest_Information(m_Quest_COLLECT, 2);
                    else
                    {
                        if (m_Quest_COLLECT.m_bProcess == true)
                            GUIManager_Total.Instance.Update_Quest_Information(m_Quest_COLLECT, 1);
                        else
                            GUIManager_Total.Instance.Update_Quest_Information(m_Quest_COLLECT, 0);
                    }
                } break;
            case E_QUEST_TYPE.CONVERSATION:
                {
                    if (m_Quest_CONVERSATION.m_bClear == true)
                        GUIManager_Total.Instance.Update_Quest_Information(m_Quest_CONVERSATION, 2);
                    else
                    {
                        if (m_Quest_CONVERSATION.m_bProcess == true)
                            GUIManager_Total.Instance.Update_Quest_Information(m_Quest_CONVERSATION, 1);
                        else
                            GUIManager_Total.Instance.Update_Quest_Information(m_Quest_CONVERSATION, 0);
                    }
                } break;
            case E_QUEST_TYPE.ROLL:
                {
                    if (m_Quest_ROLL.m_bClear == true)
                        GUIManager_Total.Instance.Update_Quest_Information(m_Quest_ROLL, 2);
                    else
                    {
                        if (m_Quest_ROLL.m_bProcess == true)
                            GUIManager_Total.Instance.Update_Quest_Information(m_Quest_ROLL, 1);
                        else
                            GUIManager_Total.Instance.Update_Quest_Information(m_Quest_ROLL, 0);
                    }
                } break;
            case E_QUEST_TYPE.ELIMINATE_MONSTER:
                {
                    if (m_Quest_ELIMINATE_MONSTER.m_bClear == true)
                        GUIManager_Total.Instance.Update_Quest_Information(m_Quest_ELIMINATE_MONSTER, 2);
                    else
                    {
                        if (m_Quest_ELIMINATE_MONSTER.m_bProcess == true)
                            GUIManager_Total.Instance.Update_Quest_Information(m_Quest_ELIMINATE_MONSTER, 1);
                        else
                            GUIManager_Total.Instance.Update_Quest_Information(m_Quest_ELIMINATE_MONSTER, 0);
                    }
                }
                break;
            case E_QUEST_TYPE.ELIMINATE_TYPE:
                {
                    if (m_Quest_ELIMINATE_TYPE.m_bClear == true)
                        GUIManager_Total.Instance.Update_Quest_Information(m_Quest_ELIMINATE_TYPE, 2);
                    else
                    {
                        if (m_Quest_ELIMINATE_TYPE.m_bProcess == true)
                            GUIManager_Total.Instance.Update_Quest_Information(m_Quest_ELIMINATE_TYPE, 1);
                        else
                            GUIManager_Total.Instance.Update_Quest_Information(m_Quest_ELIMINATE_TYPE, 0);
                    }
                }
                break;
        }
    }

    public void SetQuestInformation(Quest_KILL_MONSTER quest)
    {
        m_Quest_KILL_MONSTER = quest;
         m_QuestType = E_QUEST_TYPE.KILL_MONSTER;

        m_TMP_QuestTitle.text = m_Quest_KILL_MONSTER.m_sQuest_Title;

        this.gameObject.name = "QuestContent_" + quest.m_sQuest_Title;
    }
    public void SetQuestInformation(Quest_KILL_TYPE quest)
    {
        m_Quest_KILL_TYPE = quest;
        m_QuestType = E_QUEST_TYPE.KILL_TYPE;

        m_TMP_QuestTitle.text = m_Quest_KILL_TYPE.m_sQuest_Title;

        this.gameObject.name = "QuestContent_" + quest.m_sQuest_Title;
    }
    public void SetQuestInformation(Quest_GOAWAY_MONSTER quest)
    {
        m_Quest_GOAWAY_MONSTER = quest;
        m_QuestType = E_QUEST_TYPE.GOAWAY_MONSTER;

        m_TMP_QuestTitle.text = m_Quest_GOAWAY_MONSTER.m_sQuest_Title;

        this.gameObject.name = "QuestContent_" + quest.m_sQuest_Title;
    }
    public void SetQuestInformation(Quest_GOAWAY_TYPE quest)
    {
        m_Quest_GOAWAY_TYPE = quest;
        m_QuestType = E_QUEST_TYPE.GOAWAY_TYPE;

        m_TMP_QuestTitle.text = m_Quest_GOAWAY_TYPE.m_sQuest_Title;

        this.gameObject.name = "QuestContent_" + quest.m_sQuest_Title;
    }
    public void SetQuestInformation(Quest_COLLECT quest)
    {
        m_Quest_COLLECT = quest;
        m_QuestType = E_QUEST_TYPE.COLLECT;

        m_TMP_QuestTitle.text = m_Quest_COLLECT.m_sQuest_Title;

        this.gameObject.name = "QuestContent_" + quest.m_sQuest_Title;
    }
    public void SetQuestInformation(Quest_CONVERSATION quest)
    {
        m_Quest_CONVERSATION = quest;
        m_QuestType = E_QUEST_TYPE.CONVERSATION;

        m_TMP_QuestTitle.text = m_Quest_CONVERSATION.m_sQuest_Title;

        this.gameObject.name = "QuestContent_" + quest.m_sQuest_Title;
    }
    public void SetQuestInformation(Quest_ROLL quest)
    {
        m_Quest_ROLL = quest;
        m_QuestType = E_QUEST_TYPE.ROLL;

        m_TMP_QuestTitle.text = m_Quest_ROLL.m_sQuest_Title;

        this.gameObject.name = "QuestContent_" + quest.m_sQuest_Title;
    }
    public void SetQuestInformation(Quest_ELIMINATE_MONSTER quest)
    {
        m_Quest_ELIMINATE_MONSTER = quest;
        m_QuestType = E_QUEST_TYPE.ELIMINATE_MONSTER;

        m_TMP_QuestTitle.text = m_Quest_ELIMINATE_MONSTER.m_sQuest_Title;

        this.gameObject.name = "QuestContent_" + quest.m_sQuest_Title;
    }
    public void SetQuestInformation(Quest_ELIMINATE_TYPE quest)
    {
        m_Quest_ELIMINATE_TYPE = quest;
        m_QuestType = E_QUEST_TYPE.ELIMINATE_TYPE;

        m_TMP_QuestTitle.text = m_Quest_ELIMINATE_TYPE.m_sQuest_Title;

        this.gameObject.name = "QuestContent_" + quest.m_sQuest_Title;
    }

    public void Questpanel_Selected(bool logic)
    {
        m_gPanel_Quest_Content_Selected.SetActive(logic);
    }

    // 완료 가능한 Quest 반짝임 효과.
    public void Effect_Flesh()
    {
        switch(m_QuestType)
        {
            case E_QUEST_TYPE.KILL_MONSTER:
                {
                    if (m_Quest_KILL_MONSTER != null)
                    {
                        if (m_Quest_KILL_MONSTER.m_bCondition == true)
                            m_bQuest_Clear_Possible = true;
                        else
                            m_bQuest_Clear_Possible = false;
                    }
                } break;
            case E_QUEST_TYPE.KILL_TYPE:
                {
                    if (m_Quest_KILL_TYPE != null)
                    {
                        if (m_Quest_KILL_TYPE.m_bCondition == true)
                            m_bQuest_Clear_Possible = true;
                        else
                            m_bQuest_Clear_Possible = false;
                    }
                }
                break;
            case E_QUEST_TYPE.GOAWAY_MONSTER:
                {
                    if (m_Quest_GOAWAY_MONSTER != null)
                    {
                        if (m_Quest_GOAWAY_MONSTER.m_bCondition == true)
                            m_bQuest_Clear_Possible = true;
                        else
                            m_bQuest_Clear_Possible = false;
                    }
                }
                break;
            case E_QUEST_TYPE.GOAWAY_TYPE:
                {
                    if (m_Quest_GOAWAY_TYPE != null)
                    {
                        if (m_Quest_GOAWAY_TYPE.m_bCondition == true)
                            m_bQuest_Clear_Possible = true;
                        else
                            m_bQuest_Clear_Possible = false;
                    }
                }
                break;
            case E_QUEST_TYPE.COLLECT:
                {
                    if (m_Quest_COLLECT != null)
                    {
                        if (m_Quest_COLLECT.m_bCondition == true)
                            m_bQuest_Clear_Possible = true;
                        else
                            m_bQuest_Clear_Possible = false;
                    }
                }
                break;
            case E_QUEST_TYPE.CONVERSATION:
                {
                    if (m_Quest_CONVERSATION != null)
                    {
                        if (m_Quest_CONVERSATION.m_bCondition == true)
                            m_bQuest_Clear_Possible = true;
                        else
                            m_bQuest_Clear_Possible = false;
                    }
                }
                break;
            case E_QUEST_TYPE.ROLL:
                {
                    if (m_Quest_ROLL != null)
                    {
                        if (m_Quest_ROLL.m_bCondition == true)
                            m_bQuest_Clear_Possible = true;
                        else
                            m_bQuest_Clear_Possible = false;
                    }
                }
                break;
            case E_QUEST_TYPE.ELIMINATE_MONSTER:
                {
                    if (m_Quest_ELIMINATE_MONSTER != null)
                    {
                        if (m_Quest_ELIMINATE_MONSTER.m_bCondition == true)
                            m_bQuest_Clear_Possible = true;
                        else
                            m_bQuest_Clear_Possible = false;
                    }
                }
                break;
            case E_QUEST_TYPE.ELIMINATE_TYPE:
                {
                    if (m_Quest_ELIMINATE_TYPE != null)
                    {
                        if (m_Quest_ELIMINATE_TYPE.m_bCondition == true)
                            m_bQuest_Clear_Possible = true;
                        else
                            m_bQuest_Clear_Possible = false;
                    }
                }
                break;
        }

        if (m_gPanel_Effect_Flesh != null)
        {
            if (m_bQuest_Clear_Possible == true)
            {
                m_gPanel_Effect_Flesh.SetActive(true);

                if (m_IMG_Effect_Flesh.color.a > 0.3f && m_bAlpha_Up == true)
                {
                    m_bAlpha_Up = false;
                }
                else if (m_IMG_Effect_Flesh.color.a < 0f && m_bAlpha_Up == false)
                {
                    m_bAlpha_Up = true;
                }

                if (m_bAlpha_Up == false)
                {
                    m_IMG_Effect_Flesh.color = new Color(m_IMG_Effect_Flesh.color.r, m_IMG_Effect_Flesh.color.g, m_IMG_Effect_Flesh.color.b, m_IMG_Effect_Flesh.color.a - 0.01f);
                }
                else
                {
                    m_IMG_Effect_Flesh.color = new Color(m_IMG_Effect_Flesh.color.r, m_IMG_Effect_Flesh.color.g, m_IMG_Effect_Flesh.color.b, m_IMG_Effect_Flesh.color.a + 0.01f);
                }
            }
            else
            {
                m_gPanel_Effect_Flesh.SetActive(false);
            }
        }
    }

    // 반짝임 효과 off.
    public void UnEffect_Flesh()
    {
        m_gPanel_Effect_Flesh.SetActive(false);
    }
}
