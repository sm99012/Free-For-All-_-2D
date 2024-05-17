using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush2_Move : Bush1_Move
{
    private void Awake()
    {
        m_sSpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        //m_sSpriteRenderer_Shadow = this.gameObject.transform.Find("Bush Shadow").GetComponent<SpriteRenderer>();
        m_tTransform = this.gameObject.GetComponent<Transform>();
        m_rRigdbody = this.gameObject.GetComponent<Rigidbody2D>();

        m_vRightPos = new Vector3(1, 1, 1);
        m_vLeftPos = new Vector3(-1, 1, 1);

        m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.IDLE);

        m_bFix = false;

        if (m_sSpriteRenderer != null)
        {
            m_fAlpa = m_sSpriteRenderer.color.a;
        }
        m_FadeinAlpa = 0;

        m_fPeacefulTime = 10f;

        m_bAttack = true;
    }
}
