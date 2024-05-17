using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime1_2_Total : Slime1_Total
{
    private void Start()
    {
        Fadein();
        m_mm_Move.m_eMonsterState = m_mm_Move.SetMonsterMoveState(Monster_Move.E_MONSTER_MOVE_STATE.CHASE);
        m_mm_Move.SetAnimationParameters("CHASE");
    }

    // 리스폰하지 않는 슬라임1
    override public void Death(float time)
    {
        Death();
    }
    override public SOC Goaway()
    {
        if (m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.IDLE || m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.RUN)
        {
            m_bWait = true;
            m_ms_Status.Goaway();
            m_mm_Move.Goaway();
            //m_md_Drop.DropItem(this.gameObject.transform.position);
            //m_md_Drop.DropItem_Goaway(m_ms_Status.m_nMonsterCode, this.gameObject.transform.position);
            m_me_Effect.Effect_Goaway(this.transform.position);

            return m_ms_Status.m_sSoc_Goaway;
        }

        return m_ms_Status.m_sSoc_null;
    }
}
