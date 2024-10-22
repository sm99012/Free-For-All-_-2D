using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush2_Status : Bush1_Status
{
    // Start is called before the first frame update
    void Start()
    {
        m_eObject_State = E_OBJECT_STATE.HURDLE;
        m_eMonster_Kind = E_MONSTER_KIND.OBJECT;

        InitialSet_SOC();
        InitialSet_STATUS();

        m_sMonsterName = "가시덤불";
        m_nMonsterCode = 10;
    }

    override public void InitialSet_SOC()
    {
        m_sSoc_null = new SOC();
        m_sSoc_Goaway = new SOC(0, 0, 0, 0, 0, 0, 0, 0);
        m_sSoc_Death = new SOC(0, 0, 0, 0, 0, 0, 0, 0);
    }

    override public void InitialSet_STATUS()
    {
        m_sStatus = new STATUS(12, 0, 0, 20, 20, 0, 0, 5, 5, 5, 0, 0, 0, 0, 0, 0, 0);
        m_sStatus_Origin.SetSTATUS(m_sStatus);
        m_sStatus_Goaway = new STATUS();
        m_sStatus_Death = new STATUS(0, 0, 0);
    }

    override public void Goaway()
    {
        m_bPower = true;
    }
}
