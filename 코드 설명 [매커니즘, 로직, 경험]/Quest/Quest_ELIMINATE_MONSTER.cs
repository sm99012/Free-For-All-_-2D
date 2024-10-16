using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// ※ 특정 몬스터 제거(토벌 + 놓아주기) 퀘스트 클래스
//    해당 퀘스트는 특정 몬스터 제거(토벌 + 놓아주기)(사냥) 행위와 연관되어 있다.
//

public class Quest_ELIMINATE_MONSTER : Quest // 기반이 되는 Quest 클래스 상속
{
    // 특정 몬스터 관련 변수
    public List<int> m_nl_MonsterCode;   // 특정 몬스터 고유코드 리스트
    public List<int> m_nl_Count_Max;     // 특정 몬스터 제거(토벌 + 놓아주기) 필요 수량 리스트
    public List<int> m_nl_Count_Current; // 특정 몬스터 제거(토벌 + 놓아주기) 현재 수량 리스트

    //
    // ※ m_nl_MonsterCode(특정 몬스터 고유코드 리스트), m_nl_Count_Max(특정 몬스터 제거(토벌 + 놓아주기) 필요 수량 리스트), m_nl_Count_Current(특정 몬스터 제거(토벌 + 놓아주기) 현재 수량 리스트)는 리스트 순번에 따라 짝을 이룬다.
    //    m_nl_MonsterCode[n]에 해당하는 고유코드를 가진 몬스터를 m_nl_Count_Max[n]만큼 제거(토벌 + 놓아주기)해야 한다. 이때 m_nl_Count_Current[n]을 통해 제거(토벌 + 놓아주기) 현황을 확인할 수 있다.
    //

    // 특정 몬스터 제거(토벌 + 놓아주기) 퀘스트를 생성하는 생성자
    public Quest_ELIMINATE_MONSTER(string questtitle, int questcode, E_QUEST_LEVEL ql, int givenpccode, int clearnpccode, int questloadmapcode)
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

    // 특정 몬스터 제거(토벌 + 놓아주기) 퀘스트 설정(추가) 함수
    public void AddCondition(int monstercode, int max, int c = 0) // monstercode : 특정 몬스터 고유코드, max : 특정 몬스터 제거(토벌 + 놓아주기) 필요 수량, c : 특정 몬스터 제거(토벌 + 놓아주기) 현재 수량
    {
        m_nl_MonsterCode.Add(monstercode);
        m_nl_Count_Max.Add(max);
        m_nl_Count_Current.Add(c);
    }

    // 특정 몬스터 제거(토벌 + 놓아주기) 퀘스트 갱신 함수
    // return true : 플레이어가 제거(토벌 + 놓아주기)한 몬스터가 해당 특정 몬스터 제거(토벌 + 놓아주기) 퀘스트와 관련 있음 / return false : 플레이어가 제거(토벌 + 놓아주기)한 몬스터가 해당 특정 몬스터 제거(토벌 + 놓아주기) 퀘스트와 관련 없음
    public bool Check_ELIMINATE_MONSTER(int monstercode) // monstercode : 플레이어가 제거(토벌 + 놓아주기)한 몬스터 고유코드
    {
        bool m_bReturn = false;
        for (int i = 0; i < m_nl_MonsterCode.Count; i++) // 특정 몬스터 수량만큼 반복
        {
            if (m_nl_MonsterCode[i] == monstercode) // 특정 몬스터 고유코드 == 플레이어가 제거(토벌 + 놓아주기)한 몬스터 고유코드
            {
                if (m_nl_Count_Max[i] > m_nl_Count_Current[i]) // 특정 몬스터 제거(토벌 + 놓아주기) 필요 수량 > 특정 몬스터 제거(토벌 + 놓아주기) 현재 수량
                    m_nl_Count_Current[i]++;

                m_bReturn = true;
                break;
            }
        }

        Check_Condition(); // 퀘스트 완료 조건 달성 여부(플레이어가 현재 해당 퀘스트를 완료할 조건을 충족시켰는지?) 판단 함수

        if (m_bReturn == true)
            return true;
        else
            return false;
    }

    // 퀘스트 완료 조건(플레이어가 현재 해당 퀘스트를 완료할 조건을 충족시켰는지?) 판단 함수
    override public void Check_Condition()
    {
        for (int i = 0; i < m_nl_MonsterCode.Count; i++) // 특정 몬스터 수량만큼 반복
        {
            if (m_nl_Count_Max[i] == m_nl_Count_Current[i]) // 특정 몬스터 제거(토벌 + 놓아주기) 필요 수량 == 특정 몬스터 제거(토벌 + 놓아주기) 현재 수량 : 퀘스트 완료 조건 충족
            {
                m_bCondition = true;
                continue;
            }
            else if (m_nl_Count_Max[i] > m_nl_Count_Current[i]) // 특정 몬스터 제거(토벌 + 놓아주기) 필요 수량 > 특정 몬스터 제거(토벌 + 놓아주기) 현재 수량 : 퀘스트 완료 조건 미달
            {
                m_bCondition = false;
                break;
            }
        }
    }
}
