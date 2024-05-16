using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSlime1_Move : Monster_Move
{
    private void Awake()
    {
        m_sSpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        m_tTransform = this.gameObject.GetComponent<Transform>();
        m_rRigdbody = this.gameObject.GetComponent<Rigidbody2D>();
        m_aAnimator = this.gameObject.GetComponent<Animator>();

        m_vRightPos = new Vector3(1, 1, 1);
        m_vLeftPos = new Vector3(-1, 1, 1);

        m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.IDLE);

        m_bFix = false;

        if (m_sSpriteRenderer != null)
        {
            m_fAlpa = m_sSpriteRenderer.color.a;
        }
        m_FadeinAlpa = 0;

        m_fPeacefulTime = 100f;

        m_bAttack = true;
    }

    void Start()
    {
        Fadein();
    }

    override public void Chase(int speed, Vector3 dir)
    {
        SetDir(dir);
        if (m_bFix == false)
        {
            this.transform.position += dir * speed * Time.deltaTime * 0.005f;
            Debug.Log(this.transform.position);
        }
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
        if (m_eMonsterState == E_MONSTER_MOVE_STATE.IDLE || m_eMonsterState == E_MONSTER_MOVE_STATE.CHASE)
        {
            m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.ATTACKED);
        }
    }

    override public void Death()
    {
        m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.DEATH);
    }

    protected float m_fAttackSpeed;
    override public bool Attack(float attackspeed)
    {
        m_fAttackSpeed = attackspeed;
        if (m_eMonsterState == E_MONSTER_MOVE_STATE.CHASE && m_bAttack == true)
        {
            m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.ATTACK1);
            return true;
        }
        return false;
    }

    override public E_MONSTER_MOVE_STATE SetMonsterMoveState(E_MONSTER_MOVE_STATE ms)
    {
        switch (ms)
        {
            case E_MONSTER_MOVE_STATE.IDLE:
                {
                    if (m_eMonsterState != ms)
                    {
                        if (m_eMonsterState == E_MONSTER_MOVE_STATE.ATTACKED || m_eMonsterState == E_MONSTER_MOVE_STATE.ATTACK2)
                            SetAnimationParameters("IDLE");
                    }
                }
                break;
            case E_MONSTER_MOVE_STATE.CHASE:
                {
                    if (m_eMonsterState != ms)
                    {
                        if (m_eMonsterState == E_MONSTER_MOVE_STATE.IDLE || m_eMonsterState == E_MONSTER_MOVE_STATE.ATTACK1 ||
                            m_eMonsterState == E_MONSTER_MOVE_STATE.ATTACKED)
                            SetAnimationParameters("CHASE");
                    }
                }
                break;
            case E_MONSTER_MOVE_STATE.ATTACKED:
                {
                    if (m_eMonsterState == E_MONSTER_MOVE_STATE.IDLE || m_eMonsterState == E_MONSTER_MOVE_STATE.RUN ||
                        m_eMonsterState == E_MONSTER_MOVE_STATE.CHASE || m_eMonsterState == E_MONSTER_MOVE_STATE.ATTACK ||
                        m_eMonsterState == E_MONSTER_MOVE_STATE.ATTACKED)
                    {
                        if (m_cProcessAttacked == null)
                            m_cProcessAttacked = StartCoroutine(ProcessAttacked1());
                        else
                        {
                            StopCoroutine(m_cProcessAttacked);
                            m_cProcessAttacked = StartCoroutine(ProcessAttacked2());
                        }
                    }
                }
                break;
            case E_MONSTER_MOVE_STATE.ATTACK1:
                {
                    if (m_eMonsterState != ms)
                    {
                        if (m_eMonsterState == E_MONSTER_MOVE_STATE.CHASE)
                        {
                            if (m_bAttack == true)
                            {
                                SetAnimationParameters("ATTACK1");
                                StartCoroutine(ProcessAttack1());
                            }
                        }
                    }
                }
                break;
            case E_MONSTER_MOVE_STATE.ATTACK2:
                {
                    if (m_eMonsterState != ms)
                    {
                        if (m_eMonsterState == E_MONSTER_MOVE_STATE.CHASE)
                        {
                            if (m_bAttack == true)
                            {
                                SetAnimationParameters("ATTACK2");
                                StartCoroutine(ProcessAttack2());
                            }
                        }
                    }
                }
                break;
            case E_MONSTER_MOVE_STATE.DEATH:
                {
                    if (m_eMonsterState != ms)
                    {
                        if (m_eMonsterState == E_MONSTER_MOVE_STATE.IDLE || m_eMonsterState == E_MONSTER_MOVE_STATE.RUN ||
                            m_eMonsterState == E_MONSTER_MOVE_STATE.ATTACKED || m_eMonsterState == E_MONSTER_MOVE_STATE.ATTACK ||
                            m_eMonsterState == E_MONSTER_MOVE_STATE.CHASE)
                        {
                            SetAnimationParameters("DEATH");
                            if (m_cProcessAttacked != null)
                                StopCoroutine(m_cProcessAttacked);
                            StartCoroutine(ProcessDeath());
                        }
                    }
                }
                break;
        }

        return ms;
    }

    override public void SetAnimationParameters(string str)
    {
        switch (str)
        {
            case "IDLE":
                {
                    m_aAnimator.SetBool("IDLE", true);
                    m_aAnimator.SetBool("CHASE", false);
                    m_aAnimator.SetBool("ATTACK1", false);
                    m_aAnimator.SetBool("ATTACK2", false);
                    m_aAnimator.SetBool("ATTACKED", false);
                    m_aAnimator.SetBool("DEATH", false);
                }
                break;
            case "CHASE":
                {
                    m_aAnimator.SetBool("IDLE", false);
                    m_aAnimator.SetBool("CHASE", true);
                    m_aAnimator.SetBool("ATTACK1", false);
                    m_aAnimator.SetBool("ATTACK2", false);
                    m_aAnimator.SetBool("ATTACKED", false);
                    m_aAnimator.SetBool("DEATH", false);
                }
                break;
            case "ATTACK1":
                {
                    m_aAnimator.SetBool("IDLE", false);
                    m_aAnimator.SetBool("CHASE", false);
                    m_aAnimator.SetBool("ATTACK1", true);
                    m_aAnimator.SetBool("ATTACK2", false);
                    m_aAnimator.SetBool("ATTACKED", false);
                    m_aAnimator.SetBool("DEATH", false);
                }
                break;
            case "ATTACK2":
                {
                    m_aAnimator.SetBool("IDLE", false);
                    m_aAnimator.SetBool("CHASE", false);
                    m_aAnimator.SetBool("ATTACK1", false);
                    m_aAnimator.SetBool("ATTACK2", true);
                    m_aAnimator.SetBool("ATTACKED", false);
                    m_aAnimator.SetBool("DEATH", false);
                }
                break;
            case "ATTACKED":
                {
                    m_aAnimator.SetBool("IDLE", false);
                    m_aAnimator.SetBool("CHASE", false);
                    m_aAnimator.SetBool("ATTACK1", false);
                    m_aAnimator.SetBool("ATTACK2", false);
                    m_aAnimator.SetBool("ATTACKED", true);
                    m_aAnimator.SetBool("DEATH", false);
                }
                break;

            case "DEATH":
                {
                    m_aAnimator.SetBool("IDLE", false);
                    m_aAnimator.SetBool("CHASE", false);
                    m_aAnimator.SetBool("ATTACK1", false);
                    m_aAnimator.SetBool("ATTACK2", false);
                    m_aAnimator.SetBool("ATTACKED", false);
                    m_aAnimator.SetBool("DEATH", true);
                }
                break;
        }
    }

    Coroutine m_cProcessAttacked = null;
    // ATTACKED 모션 실행
    IEnumerator ProcessAttacked1()
    {
        SetAnimationParameters("ATTACKED");
        m_bFix = true;
        yield return new WaitForSeconds(0.5f); // 경직시간
        m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.CHASE);
        m_bFix = false;
        m_cProcessAttacked = null;
    }
    // ATTACKED 모션 중에 또맞으면 ATTACKED모션 재실행
    IEnumerator ProcessAttacked2()
    {
        m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.CHASE);
        yield return new WaitForSeconds(0.01f);
        SetAnimationParameters("ATTACKED");
        m_bFix = true;
        yield return new WaitForSeconds(0.5f); // 경직시간
        m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.CHASE);
        m_bFix = false;
        m_cProcessAttacked = null;
    }

    // DEATH
    override public IEnumerator ProcessDeath()
    {
        m_bFix = true;
        m_rRigdbody.bodyType = RigidbodyType2D.Static;
        this.gameObject.layer = LayerMask.NameToLayer("Default");
        yield return new WaitForSeconds(1f);
        m_sSpriteRenderer.color = new Color(0, 0, 0, 0);
        m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.IDLE);
    }

    // ATTACK1
    IEnumerator ProcessAttack1()
    {
        m_bAttack = false;
        yield return new WaitForSeconds(0.5f);
        // 공격중일때 플레이어 공격 지속시간이 끝날경우 IDLE, 그렇지 않을경우 CHASE
        m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.CHASE);
        yield return new WaitForSeconds(m_fAttackSpeed - 0.5f);
        m_bAttack = true;
    }

    // ATTACK2
    IEnumerator ProcessAttack2()
    {
        m_bAttack = false;
        yield return new WaitForSeconds(0.5f);
        // 공격중일때 플레이어 공격 지속시간이 끝날경우 IDLE, 그렇지 않을경우 CHASE
        m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.CHASE);
        yield return new WaitForSeconds(m_fAttackSpeed - 0.5f);
        m_bAttack = true;
    }
}
