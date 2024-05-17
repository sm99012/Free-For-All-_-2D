using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy1_Move : Monster_Move
{
    private void Awake()
    {
        m_sSpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
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
        SetDir(dir);

        if (dir.x == 0 && dir.y == 0)
            m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.IDLE);
        else
            m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.RUN);

        if (m_eMonsterState == E_MONSTER_MOVE_STATE.RUN)
            if (m_bFix == false)
                this.transform.position += dir * speed * Time.deltaTime * 0.005f;
    }

    override public void Chase(int speed, Vector3 dir)
    {
        SetDir(dir);

        if (m_bFix == false)
            this.transform.position += dir * speed * Time.deltaTime * 0.005f;
    }

    override public void SetDir(Vector3 dir)
    {
        if (dir.x >= 0)
            m_tTransform.localScale = m_vRightPos;
        else
            m_tTransform.localScale = m_vLeftPos;
    }

    override public void Attacked()
    {
        if (m_eMonsterState == E_MONSTER_MOVE_STATE.IDLE || m_eMonsterState == E_MONSTER_MOVE_STATE.RUN ||
            m_eMonsterState == E_MONSTER_MOVE_STATE.CHASE || m_eMonsterState == E_MONSTER_MOVE_STATE.ATTACKED)
            m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.ATTACKED);
    }

    override public void Goaway()
    {
        if (m_eMonsterState == E_MONSTER_MOVE_STATE.IDLE || m_eMonsterState == E_MONSTER_MOVE_STATE.RUN)
            m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.GOAWAY);
    }

    override public void Death()
    {
        m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.DEATH);
        StartCoroutine(ProcessDeath());
    }

    override public bool Attack(float attackspeed)
    {
        m_fAttackSpeed = attackspeed;
        if (m_eMonsterState == E_MONSTER_MOVE_STATE.CHASE && m_bAttack == true)
        {
            m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.ATTACK);
            return true;
        }
        return false;
    }

    override public E_MONSTER_MOVE_STATE SetMonsterMoveState(E_MONSTER_MOVE_STATE ms)
    {
        return ms;
    }

    // DEATH
    override public IEnumerator ProcessDeath()
    {
        m_fAlpa = 1;
        m_bFix = true;
        m_rRigdbody.bodyType = RigidbodyType2D.Static;
        this.gameObject.layer = LayerMask.NameToLayer("Default");
        while (m_fAlpa > 0)
        {
            m_sSpriteRenderer.color = new Color(1, 1, 1, m_fAlpa);
            m_fAlpa -= 0.03f;
            yield return new WaitForSeconds(0.01f);
        }
        m_sSpriteRenderer.color = new Color(0, 0, 0, 0);
        m_eMonsterState = E_MONSTER_MOVE_STATE.IDLE;
    }
    Coroutine m_cProcessPeaceful;

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

}
