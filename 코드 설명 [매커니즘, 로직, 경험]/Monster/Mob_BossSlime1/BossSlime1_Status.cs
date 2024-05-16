using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSlime1_Status : Monster_Status
{
    // Start is called before the first frame update
    void Start()
    {
        m_eObject_State = E_OBJECT_STATE.MONSTER;
        m_eMonster_Kind = E_MONSTER_KIND.SLIME;

        InitialSet_SOC();
        InitialSet_STATUS();

        m_sMonsterName = "테 슬라임";
        m_nMonsterCode = 1001;
    }

    override public void InitialSet_SOC()
    {
        m_sSoc_null = new SOC();
        m_sSoc_Goaway = new SOC(0, 0, 0, 0, 0, 0, 0, 0);
        m_sSoc_Death = new SOC(20, -5, -5, 0, 20, 0, 5, -5);
    }

    override public void InitialSet_STATUS()
    {
        m_sStatus = new STATUS(1, 0, 1, 50, 50, 1, 1, 1, 1, 1, 0, 0, 30, 0, 0, 0, 2);
        m_sStatus_Origin.SetSTATUS(m_sStatus);
        m_sStatus_Goaway = new STATUS();
        m_sStatus_Death = new STATUS(0, 0, 1);
    }

    override public void Goaway()
    {
        m_bPower = true;
    }
}
