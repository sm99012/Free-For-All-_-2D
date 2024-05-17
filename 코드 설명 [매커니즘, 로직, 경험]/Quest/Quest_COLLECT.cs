using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_COLLECT : Quest
{
    public List<int> m_nl_ItemCode;
    public List<int> m_nl_ItemCount_Max;
    public List<int> m_nl_ItemCount_Current;

    public Quest_COLLECT(string questtitle, int questcode, E_QUEST_LEVEL ql, int givenpccode,int clearnpccode, int questloadmapcode)
    {
        m_sQuest_Title = questtitle;
        m_nQuest_Code = questcode;
        m_nQuest_Loadmap_Code = questloadmapcode;
        m_eQuestLevel = ql;
        m_eQuestType = E_QUEST_TYPE.COLLECT;
        m_nNPC = givenpccode;
        m_nNPC_Clear = clearnpccode;

        InitialSet();

        m_nl_ItemCode = new List<int>();
        m_nl_ItemCount_Max = new List<int>();
        m_nl_ItemCount_Current = new List<int>();
    }
    // Quest 조건에 ItemCode, MaxCount, CurrentCount 추가.
    public void AddCondition(int itemcode, int max, int c = 0)
    {
        m_nl_ItemCode.Add(itemcode);
        m_nl_ItemCount_Max.Add(max);
        m_nl_ItemCount_Current.Add(c);
    }

    // 특정 아이템 수집 퀘스트
    public bool Check_COLLECT()
    {
        bool returntype = false;
        for (int i = 0; i < m_nl_ItemCode.Count; i++)
        {
            m_nl_ItemCount_Current[i] = 0;
            for (int a = 0; a < 60; a++)
            {
                if (Player_Itemslot.m_nary_Itemslot_Equip_Count[a] != 0)
                {
                    if (Player_Itemslot.m_gary_Itemslot_Equip[a].m_nItemCode == m_nl_ItemCode[i])
                    {
                        m_nl_ItemCount_Current[i] += Player_Itemslot.m_nary_Itemslot_Equip_Count[a];
                        returntype = true;
                    }
                }
                if (Player_Itemslot.m_nary_Itemslot_Use_Count[a] != 0)
                {
                    if (Player_Itemslot.m_gary_Itemslot_Use[a].m_nItemCode == m_nl_ItemCode[i])
                    {
                        m_nl_ItemCount_Current[i] += Player_Itemslot.m_nary_Itemslot_Use_Count[a];
                        returntype = true;
                    }
                }
                if (Player_Itemslot.m_nary_Itemslot_Etc_Count[a] != 0)
                {
                    if (Player_Itemslot.m_gary_Itemslot_Etc[a].m_nItemCode == m_nl_ItemCode[i])
                    {
                        m_nl_ItemCount_Current[i] += Player_Itemslot.m_nary_Itemslot_Etc_Count[a];
                        returntype = true;
                    }
                }
            }
        }
        Check_Condition();

        if (returntype == false)
            return false;
        else
            return true;
    }
    
    // Quest 클리어 조건 체크.
    public bool Check_Condition()
    {
        for (int i = 0; i < m_nl_ItemCode.Count; i++)
        {
            if (m_nl_ItemCount_Max[i] <= m_nl_ItemCount_Current[i])
            {
                continue;
            }
            else
            {
                m_bCondition = false;
                GUIManager_Total.Instance.UnDisplay_GUI_QuestStateInfo(this);
                return false;
            }
        }
        m_bCondition = true;
        return true;
    }
}
