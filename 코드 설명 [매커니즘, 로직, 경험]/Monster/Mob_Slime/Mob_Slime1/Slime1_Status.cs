using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime1_Status : Monster_Status
{
    // Start is called before the first frame update
    void Start()
    {
        m_eObject_State = E_OBJECT_STATE.MONSTER;
        m_eMonster_Kind = E_MONSTER_KIND.SLIME;

        InitialSet_SOC();
        InitialSet_STATUS();

        m_sMonsterName = "초원 슬라임";
        m_nMonsterCode = 2000;
    }

    override public void InitialSet_SOC()
    {
        m_sSoc_null = new SOC();
        m_sSoc_Goaway = new SOC(0, 0, 0, 0, 0, 0, 0, 0, 0);
        m_sSoc_Death = new SOC(0, 0, 0, 0, 0, 0, 0, 0, 0);
    }

    override public void InitialSet_STATUS()
    {
        //m_sStatus = new STATUS(1, 0, 1, 5, 5, 1, 1, 1, 1, 1, 0, 0, 30, 0, 0, 0, 2);
        //m_sStatus = new STATUS(4, 0, 0, 15, 15, 1, 1, 1, 1, 1, 0, 0, 30, 0, 0, 0, 2);
        m_sStatus = new STATUS(4, 0, 0, 30, 30, 1, 1, 3, 3, 3, 0, 0, 30, 2, 2, 0, 2);
        m_sStatus_Origin.SetSTATUS(m_sStatus);
        m_sStatus_Goaway = new STATUS();
        m_sStatus_Death = new STATUS(0, 0, 3);
    }

    override public void Goaway()
    {
        m_bPower = true;
    }
}
