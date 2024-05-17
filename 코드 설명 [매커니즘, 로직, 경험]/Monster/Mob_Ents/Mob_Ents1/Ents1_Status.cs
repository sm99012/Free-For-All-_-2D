using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ents1_Status : Monster_Status
{
    
    void Start()
    {
        m_eObject_State = E_OBJECT_STATE.MONSTER;
        m_eMonster_Kind = E_MONSTER_KIND.ENTS;

        InitialSet_SOC();
        InitialSet_STATUS();

        m_sMonsterName = "짙은 앤트";
        m_nMonsterCode = 3;
    }

    override public void InitialSet_SOC()
    {
        m_sSoc_null = new SOC();
        m_sSoc_Goaway = new SOC(0, 0, 0, 0, 0, 0, 0, 0, 0);
        m_sSoc_Death = new SOC(0, 0, 0, 0, 0, 0, 0, 0, 0);
    }

    override public void InitialSet_STATUS()
    {
        //m_sStatus = new STATUS(3, 0, 5, 40, 40, 0, 0, 5, 5, 0, 0, 0, 20, 7, 7, 0, 5);
        m_sStatus = new STATUS(12, 0, 7, 80, 80, 0, 0, 8, 8, 0, 0, 0, 15, 15, 15, 0, 5);
        m_sStatus_Origin.SetSTATUS(m_sStatus);
        m_sStatus_Goaway = new STATUS();
        //m_sStatus_Death = new STATUS(0, 0, 3);
        m_sStatus_Death = new STATUS(0, 0, 13);
    }

    override public void Goaway()
    {
        m_bPower = true;
    }
}
