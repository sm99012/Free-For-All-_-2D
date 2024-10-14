using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// ※ 대화 퀘스트 클래스
//    해당 퀘스트는 NPC와 상호작용(대화) 행위와 연관되어 있다.
//

public class Quest_CONVERSATION : Quest // 기반이 되는 Quest 클래스 상속
{
    // 대화 퀘스트를 생성하는 생성자
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

    // 대화 퀘스트 갱신 함수. 특정 NPC와 상호작용(대화)을 통해 퀘스트 완료 조건을 달성한다.
    public bool Check_CONVERSATION(NPC_Total npc) // npc : NPC와 상호작용(대화)를 보유한 NPC
    {
        if (npc.m_nNPCCode == m_nNPC_Clear) // NPC와 상호작용(대화)를 보유한 NPC 고유코드 == 퀘스트 완료 NPC
        {
            m_bCondition = true;
            return true;
        }
        return false;
    }
}
