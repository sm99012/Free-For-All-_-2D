using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush2_Move : Bush1_Move // 기반이 되는 Bush1_Move 클래스 상속
{
    private void Awake()
    {
        m_sSpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        //m_sSpriteRenderer_Shadow = this.gameObject.transform.Find("Bush Shadow").GetComponent<SpriteRenderer>(); // 몬스터 그림자 스프라이트 랜더러(이미지 + 색상 정보 등) 존재하지 않음
        m_tTransform = this.gameObject.GetComponent<Transform>();
        m_rRigdbody = this.gameObject.GetComponent<Rigidbody2D>();

        m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.IDLE);
        
        m_vRightPos = new Vector3(1, 1, 1);
        m_vLeftPos = new Vector3(-1, 1, 1);
        m_bFix = false;

        m_bAttack = true;
        m_fPeacefulTime = 10f;
        
        m_FadeinAlpa = 0;
        if (m_sSpriteRenderer != null)
        {
            m_fAlpa = m_sSpriteRenderer.color.a;
        }
    }
}
