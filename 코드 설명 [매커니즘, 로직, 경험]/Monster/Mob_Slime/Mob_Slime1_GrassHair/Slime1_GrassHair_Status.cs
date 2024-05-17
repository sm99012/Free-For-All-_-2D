using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime1_GrassHair_Status : Monster_Status
{
    void Start()
    {
        m_eObject_State = E_OBJECT_STATE.MONSTER;
        m_eMonster_Kind = E_MONSTER_KIND.SLIME;

        InitialSet_SOC();
        InitialSet_STATUS();

        m_sMonsterName = "잔디 머리 초원 슬라임";
        m_nMonsterCode = 2001;
    }

    override public void InitialSet_SOC()
    {
        m_sSoc_null = new SOC();
        m_sSoc_Goaway = new SOC(0, 0, 0, 0, 0, 0, 0, 0, 0);
        m_sSoc_Death = new SOC(0, 0, 0, 0, 0, 0, 0, 0, 0);
    }

    override public void InitialSet_STATUS()
    {
        //m_sStatus = new STATUS(2, 0, 0, 12, 12, 1, 1, 2, 2, 2, 0, 0, 30, 0, 0, 0, 3f);
        m_sStatus = new STATUS(6, 0, 0, 50, 50, 3, 3, 4, 4, 4, 0, 0, 30, 3, 3, 0, 2f);
        m_sStatus_Origin.SetSTATUS(m_sStatus);
        m_sStatus_Goaway = new STATUS();
        m_sStatus_Death = new STATUS(0, 0, 5);
    }

    override public void Goaway()
    {
        m_bPower = true;
    }
}
