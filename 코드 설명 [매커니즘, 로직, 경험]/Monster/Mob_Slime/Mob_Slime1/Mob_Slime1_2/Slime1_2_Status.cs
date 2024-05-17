using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime1_2_Status : Slime1_Status
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
}
