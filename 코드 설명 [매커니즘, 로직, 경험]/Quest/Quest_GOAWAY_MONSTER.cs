using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_GOAWAY_MONSTER : Quest
{
    public List<int> m_nl_MonsterCode;
    public List<int> m_nl_Count_Max;
    public List<int> m_nl_Count_Current;

    public Quest_GOAWAY_MONSTER(string questtitle, int questcode, E_QUEST_LEVEL ql, int givenpccode, int clearnpccode, int questloadmapcode)
    {
        m_sQuest_Title = questtitle;
        m_nQuest_Code = questcode;
        m_nQuest_Loadmap_Code = questloadmapcode;
        m_eQuestLevel = ql;
        m_eQuestType = E_QUEST_TYPE.GOAWAY_MONSTER;
        m_nNPC = givenpccode;
        m_nNPC_Clear = clearnpccode;

        InitialSet();

        m_nl_MonsterCode = new List<int>();
        m_nl_Count_Max = new List<int>();
        m_nl_Count_Current = new List<int>();
    }

    // Quest 조건에 Monster, MaxCount, CurrentCount 추가.
    public void AddCondition(int monstercode, int max, int c = 0)
    {
        m_nl_MonsterCode.Add(monstercode);
        m_nl_Count_Max.Add(max);
        m_nl_Count_Current.Add(c);
    }

    // 특정 몬스터 Goaway 에 관련한 퀘스트 (토벌 퀘스트)
    public bool Check_GOAWAY_MONSTER(int monstercode)
    {
        bool m_bReturn = false;
        for (int i = 0; i < m_nl_MonsterCode.Count; i++)
        {
            if (m_nl_MonsterCode[i] == monstercode)
            {
                if (m_nl_Count_Max[i] > m_nl_Count_Current[i])
                    m_nl_Count_Current[i]++;

                m_bReturn = true;
                break;
            }
        }

        Check_Condition();

        if (m_bReturn == true)
            return true;
        else
            return false;
    }

    void Check_Condition()
    {
        for (int i = 0; i < m_nl_MonsterCode.Count; i++)
        {
            if (m_nl_Count_Max[i] == m_nl_Count_Current[i])
            {
                m_bCondition = true;
                continue;
            }
            else if (m_nl_Count_Max[i] > m_nl_Count_Current[i])
            {
                m_bCondition = false;
                break;
            }
        }
    }
}
