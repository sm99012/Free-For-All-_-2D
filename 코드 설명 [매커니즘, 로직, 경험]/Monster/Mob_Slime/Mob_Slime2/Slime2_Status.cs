using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime2_Status : Monster_Status
{
    // Start is called before the first frame update
    void Start()
    {
        m_eObject_State = E_OBJECT_STATE.MONSTER;
        m_eMonster_Kind = E_MONSTER_KIND.SLIME;

        InitialSet_SOC();
        InitialSet_STATUS();

        m_sMonsterName = "큰 초원 슬라임";
        m_nMonsterCode = 2100;
    }

    override public void InitialSet_SOC()
    {
        m_sSoc_null = new SOC();
        m_sSoc_Goaway = new SOC(0, 0, 0, 0, 0, 0, 0, 0, 0);
        m_sSoc_Death = new SOC(0, 0, 0, 0, 0, 0, 0, 0, 0);
    }

    override public void InitialSet_STATUS()
    {
        m_sStatus = new STATUS(11, 0, 0, 220, 220, 10, 10, 6, 6, 6, 0, 0, 15, 10, 10, 0, 4);
        m_sStatus_Origin.SetSTATUS(m_sStatus);
        m_sStatus_Goaway = new STATUS();
        m_sStatus_Death = new STATUS(0, 0, 8);
    }

    override public void Goaway()
    {
        m_bPower = true;
    }
}
