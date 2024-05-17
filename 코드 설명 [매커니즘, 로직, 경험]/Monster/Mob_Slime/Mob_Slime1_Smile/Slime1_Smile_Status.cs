using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime1_Smile_Status : Monster_Status
{
    void Start()
    {
        m_eObject_State = E_OBJECT_STATE.MONSTER;
        m_eMonster_Kind = E_MONSTER_KIND.SLIME;

        InitialSet_SOC();
        InitialSet_STATUS();

        m_sMonsterName = "웃고있는 초원 슬라임";
        m_nMonsterCode = 2002;
    }

    override public void InitialSet_SOC()
    {
        m_sSoc_null = new SOC();
        m_sSoc_Goaway = new SOC(0, 0, 0, 0, 0, 0, 0, 0, 0);
        m_sSoc_Death = new SOC(0, 0, 0, 0, 0, 0, 0, 0, 0);
    }

    override public void InitialSet_STATUS()
    {
        //m_sStatus = new STATUS(4, 0, 0, 15, 15, 1, 1, 2, 2, 2, 0, 0, 30, 2, 2, 0, 2);
        //m_sStatus = new STATUS(2, 0, 0, 10, 10, 1, 1, 1, 1, 1, 0, 0, 40, 0, 0, 0, 1.5f);
        m_sStatus = new STATUS(8, 0, 0, 30, 30, 1, 1, 2, 2, 2, 0, 0, 50, 2, 2, 0, 2);
        m_sStatus_Origin.SetSTATUS(m_sStatus);
        m_sStatus_Goaway = new STATUS();
        m_sStatus_Death = new STATUS(0, 0, 8);
    }

    override public void Goaway()
    {
        m_bPower = true;
    }
}
