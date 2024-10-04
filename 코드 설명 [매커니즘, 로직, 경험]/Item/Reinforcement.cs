using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reinforcement // 장비아이템 강화 관련 클래스
                           // 장비아이템에는 장비아이템 최대 강화 횟수가 존재하는데 해당 횟수 만큼 강화를 할 수 있다.
                           // 강화는 성공할때 까지 가능하다. 강화 실패의 단점이 없다.
{
    int m_nProbabilityMax; // 장비아이템 강화 확률 범위(10000 고정)
    int m_nProbability;    // 장비아이템 강화 성공 확률(0.01% ~ 100%)

    STATUS m_STATUS_Reinforcement; // 장비아이템 강화 스탯(능력치)
    SOC m_SOC_Reinforcement;       // 장비아이템 강화 스탯(평판)

    // 장비아이템 강화 데이터를 생성하는 생성자
    public Reinforcement(int probability, STATUS status, SOC soc) // probability : 장비아이템 강화 성공 확률, status : 장비아이템 강화 스탯(능력치), soc : 장비아이템 강화 스탯(평판)
    {
        this.m_nProbabilityMax = 10000;
        this.m_nProbability = probability;

        m_STATUS_Reinforcement = status;
        m_SOC_Reinforcement = soc;
    }

    // 장비아이템 강화 확률 계산
    // return true : 장비아이템 강화 성공 / return false : 장비아이템 강화 실패
    public bool Reinforce_Item_Equip()
    {
        int randomvalue = Random.Range(0, m_nProbabilityMax + 1);

        if (randomvalue <= m_nProbability)
            return true;
        else
            return false;
    }

    // 장비아이템 강화 스탯(능력치) 반환
    public STATUS GetReinforcementSTATUS()
    {
        return m_STATUS_Reinforcement;
    }
    // 장비아이템 강화 스탯(평판) 반환
    public SOC GetReinforcementSOC()
    {
        return m_SOC_Reinforcement;
    }
}
