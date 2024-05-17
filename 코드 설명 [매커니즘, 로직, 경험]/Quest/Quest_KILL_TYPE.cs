using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_KILL_TYPE : Quest
{
    public E_MONSTER_KIND m_eMonsterType;
    public int m_nCount_Max;
    public int m_nCount_Current;

    public Quest_KILL_TYPE(string questtitle, int questcode, E_QUEST_LEVEL ql, int givenpccode, int clearnpccode, int questloadmapcode)
    {
        m_sQuest_Title = questtitle;
        m_nQuest_Code = questcode;
        m_nQuest_Loadmap_Code = questloadmapcode;
        m_eQuestLevel = ql;
        m_eQuestType = E_QUEST_TYPE.KILL_TYPE;
        m_nNPC = givenpccode;
        m_nNPC_Clear = clearnpccode;

        InitialSet();
    }

    // Quest 조건에 Monster, MaxCount, CurrentCount 추가.
    public void SetCondition(E_MONSTER_KIND mk, int max, int c = 0)
    {
        m_eMonsterType = mk;
        m_nCount_Max = max;
        m_nCount_Current = c;
    }

    // Type 몬스터 KILL 에 관련한 퀘스트 (토벌 퀘스트)
    public bool Check_KILL_TYPE(E_MONSTER_KIND mk)
    {
        bool m_bReturn = false;
        if (m_eMonsterType == mk)
        {
            if (m_nCount_Max > m_nCount_Current)
                m_nCount_Current++;

            m_bReturn = true;
        }

        Check_Condition();

        if (m_bReturn == true)
            return true;
        else
            return false;
    }

    void Check_Condition()
    {
        if (m_nCount_Max == m_nCount_Current)
        {
            m_bCondition = true;
        }
        else if (m_nCount_Max > m_nCount_Current)
        {
            m_bCondition = false;
        }
    }
}
