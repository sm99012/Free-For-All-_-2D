using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// ※ 구르기 퀘스트 클래스
//    해당 퀘스트는 구르기 행위와 연관되어 있다.
//

public class Quest_ROLL : Quest // 기반이 되는 Quest 클래스 상속
{
    // 구르기 관련 변수
    public int m_nCount_Max;     // 구르기 필요 수량
    public int m_nCount_Current; // 구르기 현재 수량

    // 구르기 퀘스트를 생성하는 생성자
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

    // 구르기 퀘스트 설정 함수
    public void SetCondition(int max, int c = 0) // max : 구르기 필요 수량, c : 구르기 현재 수량
    {
        m_nCount_Max = max;
        m_nCount_Current = c;
    }

    // 구르기 퀘스트 갱신 함수. 퀘스트 완료 조건 달성 여부(플레이어가 현재 해당 퀘스트를 완료할 조건을 충족시켰는지?) 판단 함수
    public bool Check_ROLL()
    {
        if (m_nCount_Max > m_nCount_Current) // 구르기 필요 수량 > 구르기 현재 수량
            m_nCount_Current += 1;

        if (m_nCount_Max == m_nCount_Current) // 구르기 필요 수량 == 구르기 현재 수량
        {
            m_bCondition = true;
        }

        return true;
    }
}
