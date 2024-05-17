﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_GeneralOfTraningSlime : NPC_Total
{
    private void Awake()
    {
        m_sNPCName = "[주식회사 더 슬라]\n훈련대장 초원 슬라임";
        m_nNPCCode = 5;

        m_ql_QuestList_KILL_MONSTER = new List<Quest_KILL_MONSTER>();
        m_ql_QuestList_KILL_TYPE = new List<Quest_KILL_TYPE>();
        m_ql_QuestList_GOAWAY_MONSTER = new List<Quest_GOAWAY_MONSTER>();
        m_ql_QuestList_GOAWAY_TYPE = new List<Quest_GOAWAY_TYPE>();
        m_ql_QuestList_COLLECT = new List<Quest_COLLECT>();
        m_ql_QuestList_CONVERSATION = new List<Quest_CONVERSATION>();
        m_ql_QuestList_ROLL = new List<Quest_ROLL>();
        m_ql_QuestList_ELIMINATE_MONSTER = new List<Quest_ELIMINATE_MONSTER>();
        m_ql_QuestList_ELIMINATE_TYPE = new List<Quest_ELIMINATE_TYPE>();

        m_cl_Conversation = new List<Conversation>();

        m_Sprite_NPC = this.GetComponent<SpriteRenderer>().sprite;

        if (NPCManager_Total.m_Dictionary_NPC.ContainsKey(m_nNPCCode) == false)
            NPCManager_Total.m_Dictionary_NPC.Add(m_nNPCCode, this);
        else
        {
            NPCManager_Total.m_Dictionary_NPC.Remove(m_nNPCCode);
            NPCManager_Total.m_Dictionary_NPC.Add(m_nNPCCode, this);
        }
        InitialSet();
    }

    override public void InitialSet()
    {
        InitialSet_Icon();

        m_cl_Conversation.Add(ConversationManager.Instance.m_Dictionary_ConversationList[5]);
        m_cl_Conversation.Add(ConversationManager.Instance.m_Dictionary_ConversationList[6]);

        m_ql_QuestList_ROLL.Add(QuestManager.Instance.GetQuest_ROLL(6001));
        m_ql_QuestList_KILL_MONSTER.Add(QuestManager.Instance.GetQuest_KILL_MONSTER(0001));
        m_ql_QuestList_KILL_MONSTER.Add(QuestManager.Instance.GetQuest_KILL_MONSTER(0002));
    }
}
