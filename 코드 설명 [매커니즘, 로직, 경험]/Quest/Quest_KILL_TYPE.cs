using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// ※ 특정 몬스터 타입 토벌 퀘스트 클래스
//    해당 퀘스트는 특정 몬스터 타입 토벌(사냥) 행위와 연관되어 있다. 몬스터 타입이란 평판 데이터(인간, 동물, 슬라임, 스켈레톤, 앤트, 마족, 용족, 어둠)와 관련이 있다.
//

public class Quest_KILL_TYPE : Quest // 기반이 되는 Quest 클래스 상속
{
    // 특정 몬스터 타입 관련 변수
    public E_MONSTER_KIND m_eMonsterType; // 특정 몬스터 타입
    public int m_nCount_Max;              // 특정 몬스터 타입 토벌 필요 수량
    public int m_nCount_Current;          // 특정 몬스터 타입 토벌 현재 수량

    // 특정 몬스터 타입 토벌 퀘스트를 생성하는 생성자
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

    // 특정 몬스터 타입 토벌 퀘스트 설정 함수
    public void SetCondition(E_MONSTER_KIND mk, int max, int c = 0) // mk : 몬스터 타입, max : 특정 몬스터 타입 토벌 필요 수량, c : 특정 몬스터 타입 토벌 현재 수량
    {
        m_eMonsterType = mk;
        m_nCount_Max = max;
        m_nCount_Current = c;
    }

    // 특정 몬스터 타입 토벌 퀘스트 갱신 함수
    // return true : 플레이어가 토벌한 몬스터 타입이 해당 특정 몬스터 타입 토벌 퀘스트와 관련 있음 / return false : 플레이어가 토벌한 몬스터 타입이 해당 특정 몬스터 타입 토벌 퀘스트와 관련 없음
    public bool Check_KILL_TYPE(E_MONSTER_KIND mk) // mk : 플레이어가 토벌한 몬스터 타입
    {
        bool m_bReturn = false;
        if (m_eMonsterType == mk) // 특정 몬스터 타입 == 플레이어가 토벌한 몬스터 타입
        {
            if (m_nCount_Max > m_nCount_Current) // 특정 몬스터 타입 토벌 필요 수량 > 특정 몬스터 타입 토벌 현재 수량
                m_nCount_Current++;

            m_bReturn = true;
        }

        Check_Condition(); // 퀘스트 완료 조건 달성 여부(플레이어가 현재 해당 퀘스트를 완료할 조건을 충족시켰는지?) 판단 함수

        if (m_bReturn == true)
            return true;
        else
            return false;
    }

    // 퀘스트 완료 조건 달성 여부(플레이어가 현재 해당 퀘스트를 완료할 조건을 충족시켰는지?) 판단 함수
    override public void Check_Condition()
    {
        if (m_nCount_Max == m_nCount_Current) // 특정 몬스터 타입 토벌 필요 수량 == 특정 몬스터 타입 토벌 현재 수량 : 퀘스트 완료 조건 충족
        {
            m_bCondition = true;
        }
        else if (m_nCount_Max > m_nCount_Current) // 특정 몬스터 타입 토벌 필요 수량 > 특정 몬스터 타입 토벌 현재 수량 : 퀘스트 완료 조건 미달
        {
            m_bCondition = false;
        }
    }
}
