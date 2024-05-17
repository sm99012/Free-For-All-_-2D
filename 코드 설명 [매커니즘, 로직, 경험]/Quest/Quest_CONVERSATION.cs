using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_CONVERSATION : Quest
{
    public Quest_CONVERSATION(string questtitle, int questcode, E_QUEST_LEVEL ql, int givenpccode, int clearnpccode, int questloadmapcode)
    {
        m_sQuest_Title = questtitle;
        m_nQuest_Code = questcode;
        m_nQuest_Loadmap_Code = questloadmapcode;
        m_eQuestLevel = ql;
        m_eQuestType = E_QUEST_TYPE.CONVERSATION;
        m_nNPC = givenpccode;
        m_nNPC_Clear = clearnpccode;

        InitialSet();
    }

    public bool Check_CONVERSATION(NPC_Total npc)
    {
        if (npc.m_nNPCCode == m_nNPC_Clear)
        {
            m_bCondition = true;
            return true;
        }
        return false;
    }
}
