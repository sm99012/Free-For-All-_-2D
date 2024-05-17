using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime1_RedHat1_Status : Slime1_Status
{
    // Start is called before the first frame update
    void Start()
    {
        m_eObject_State = E_OBJECT_STATE.MONSTER;
        m_eMonster_Kind = E_MONSTER_KIND.SLIME;

        InitialSet_SOC();
        InitialSet_STATUS();

        m_sMonsterName = "상인단 경비 초원 슬라임";
        m_nMonsterCode = 2003;
    }

    override public void InitialSet_SOC()
    {
        m_sSoc_null = new SOC();
        m_sSoc_Goaway = new SOC(0, 0, 0, 0, 0, 0, 0, 0, 0);
        m_sSoc_Death = new SOC(0, 0, 0, 0, 0, 0, 0, 0, 0);
    }

    override public void InitialSet_STATUS()
    {
        //m_sStatus = new STATUS(4, 0, 0, 22, 22, 3, 3, 3, 5, 5, 0, 0, 30, 5, 5, 0, 2f);
        m_sStatus = new STATUS(6, 0, 0, 60, 60, 1, 1, 5, 5, 5, 0, 0, 30, 7, 7, 0, 2f);
        m_sStatus_Origin.SetSTATUS(m_sStatus);
        m_sStatus_Goaway = new STATUS();
        m_sStatus_Death = new STATUS(0, 0, 6);
    }
}
