using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush1_Move : Monster_Move
{
    private void Awake()
    {
        m_sSpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        m_sSpriteRenderer_Shadow = this.gameObject.transform.Find("Bush Shadow").GetComponent<SpriteRenderer>();
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

    void Start()
    {
        Fadein();
    }

    override public void Move(int speed, Vector3 dir)
    {

    }

    override public void Chase(int speed, Vector3 dir)
    {

    }

    override public void SetDir(Vector3 dir)
    {

    }

    override public void Attacked()
    {
        if (m_eMonsterState == E_MONSTER_MOVE_STATE.IDLE || m_eMonsterState == E_MONSTER_MOVE_STATE.RUN ||
            m_eMonsterState == E_MONSTER_MOVE_STATE.CHASE || m_eMonsterState == E_MONSTER_MOVE_STATE.ATTACKED)
            m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.ATTACKED);
    }

    override public void Goaway()
    {

    }

    override public void Death()
    {
        m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.DEATH);
        StartCoroutine(ProcessDeath());
    }

    override public void Respone()
    {
        m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.IDLE);
        m_rRigdbody.bodyType = RigidbodyType2D.Static;
        m_sSpriteRenderer.color = m_Color_OriginalSprite;
        m_FadeinAlpa = 0;
        m_bFix = false;
        this.gameObject.layer = LayerMask.NameToLayer("Monster");
        Fadein();
    }

    override public bool Attack(float attackspeed)
    { 
        return false;
    }

    override public E_MONSTER_MOVE_STATE SetMonsterMoveState(E_MONSTER_MOVE_STATE ms, float attackspeed = 0)
    {
        return ms;
    }
}
