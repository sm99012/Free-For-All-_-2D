using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reinforcement
{
    int m_nProbabilityMax;
    int m_nProbability;

    STATUS m_STATUS_Reinforcement;
    SOC m_SOC_Reinforcement;

    public Reinforcement(int probability, STATUS status, SOC soc)
    {
        this.m_nProbabilityMax = 10000;
        this.m_nProbability = probability;

        m_STATUS_Reinforcement = status;
        m_SOC_Reinforcement = soc;
    }

    public bool Reinforce_Item_Equip()
    {
        int randomvalue = Random.Range(0, m_nProbabilityMax + 1);

        if (randomvalue <= m_nProbability)
            return true;
        else
            return false;
    }

    public STATUS GetReinforcementSTATUS()
    {
        return m_STATUS_Reinforcement;
    }
    public SOC GetReinforcementSOC()
    {
        return m_SOC_Reinforcement;
    }
}
