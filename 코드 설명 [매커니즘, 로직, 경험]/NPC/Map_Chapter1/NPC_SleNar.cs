using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_SleNar : NPC_Total
{
    private void Awake()
    {
        m_sNPCName = "[초보 모험가]\n슬레나르";
        m_nNPCCode = 16;

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
        {
            NPCManager_Total.m_Dictionary_NPC.Add(m_nNPCCode, this);
            //InitialSet_Store();
        }
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

        m_cl_Conversation.Add(ConversationManager.Instance.m_Dictionary_ConversationList[24]);
        m_cl_Conversation.Add(ConversationManager.Instance.m_Dictionary_ConversationList[25]);
        //m_cl_Conversation.Add(ConversationManager.Instance.m_Dictionary_ConversationList[20]);

        //m_ql_QuestList_COLLECT.Add(QuestManager.Instance.GetQuest_COLLECT(4005));
        m_ql_QuestList_CONVERSATION.Add(QuestManager.Instance.GetQuest_CONVERSATION(5007));
        m_ql_QuestList_COLLECT.Add(QuestManager.Instance.GetQuest_COLLECT(4006));
        m_ql_QuestList_KILL_MONSTER.Add(QuestManager.Instance.GetQuest_KILL_MONSTER(0006));
        m_ql_QuestList_CONVERSATION.Add(QuestManager.Instance.GetQuest_CONVERSATION(5008));
        m_ql_QuestList_COLLECT.Add(QuestManager.Instance.GetQuest_COLLECT(4007));
    }

    override public void InitialSet_Store()
    {

    }
}
