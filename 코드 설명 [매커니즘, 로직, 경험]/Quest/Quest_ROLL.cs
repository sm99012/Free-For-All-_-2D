using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_ROLL : Quest
{
    public int m_nCount_Max;
    public int m_nCount_Current;

    public Quest_ROLL(string questtitle, int questcode, E_QUEST_LEVEL ql, int givenpccode, int clearnpccode, int questloadmapcode)
    {
        m_sQuest_Title = questtitle;
        m_nQuest_Code = questcode;
        m_nQuest_Loadmap_Code = questloadmapcode;
        m_eQuestLevel = ql;
        m_eQuestType = E_QUEST_TYPE.ROLL;
        m_nNPC = givenpccode;
        m_nNPC_Clear = clearnpccode;

        InitialSet();
    }

    // Quest 조건에 MaxCount, CurrentCount 추가.
    public void SetCondition(int max, int c = 0)
    {
        m_nCount_Max = max;
        m_nCount_Current = c;
    }

    // 구르기 횟수 Check
    public bool Check_ROLL()
    {
        //Debug.Log("Roll");
        if (m_nCount_Max > m_nCount_Current)
            m_nCount_Current += 1;

        if (m_nCount_Max == m_nCount_Current)
        {
            m_bCondition = true;
        }

        return true;
    }
}
