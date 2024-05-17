using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime2_Move : Monster_Move
{
    // Start is called before the first frame update
    void Awake()
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
                m_tTransform.position += (dir * speed * Time.deltaTime * 0.005f);
                //m_rRigdbody.MovePosition(this.transform.position + (dir * speed * Time.deltaTime * 0.01f));
    }

    override public void Chase(int speed, Vector3 dir)
    {
        SetDir(dir);

        if (m_bFix == false)
            m_tTransform.position += (dir * speed * Time.deltaTime * 0.005f);
            //m_rRigdbody.MovePosition(this.transform.position + (dir * speed * Time.deltaTime * 0.01f));
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
    }

    override public bool Attack(float attackspeed)
    {
        m_fAttackSpeed = attackspeed;
        if (m_bAttack == true)
        if (m_eMonsterState == E_MONSTER_MOVE_STATE.CHASE ||
            m_eMonsterState == E_MONSTER_MOVE_STATE.ATTACKED)
        {
            m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.ATTACK);
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
                        if (m_eMonsterState == E_MONSTER_MOVE_STATE.RUN || m_eMonsterState == E_MONSTER_MOVE_STATE.CHASE ||
                            m_eMonsterState == E_MONSTER_MOVE_STATE.ATTACK || m_eMonsterState == E_MONSTER_MOVE_STATE.DEATH ||
                            m_eMonsterState == E_MONSTER_MOVE_STATE.GOAWAY)
                            SetAnimationParameters("IDLE");
                    }
                }
                break;
            case E_MONSTER_MOVE_STATE.RUN:
                {
                    if (m_eMonsterState != ms)
                    {
                        if (m_eMonsterState == E_MONSTER_MOVE_STATE.IDLE)
                            SetAnimationParameters("RUN");
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

                        if (m_cProcessPeaceful == null)
                            m_cProcessPeaceful = StartCoroutine(ProcessPeaceful());
                        else
                        {
                            StopCoroutine(m_cProcessPeaceful);
                            m_cProcessPeaceful = StartCoroutine(ProcessPeaceful());
                        }
                    }
                }
                break;
            case E_MONSTER_MOVE_STATE.ATTACK:
                {
                    if (m_eMonsterState != ms)
                    {
                        if (m_eMonsterState == E_MONSTER_MOVE_STATE.CHASE)
                        {
                            if (m_bAttack == true)
                            {
                                SetAnimationParameters("ATTACK");
                                StartCoroutine(ProcessAttack());
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
            case E_MONSTER_MOVE_STATE.GOAWAY:
                {
                    if (m_eMonsterState != ms)
                    {
                        if (m_eMonsterState == E_MONSTER_MOVE_STATE.IDLE || m_eMonsterState == E_MONSTER_MOVE_STATE.RUN)
                        {
                            SetAnimationParameters("GOAWAY");
                            StartCoroutine(ProcessGoaway());
                        }
                    }
                }
                break;
            case E_MONSTER_MOVE_STATE.CHASE:
                {
                    if (m_eMonsterState != ms)
                    {
                        if (m_eMonsterState == E_MONSTER_MOVE_STATE.ATTACKED || m_eMonsterState == E_MONSTER_MOVE_STATE.ATTACK)
                            SetAnimationParameters("CHASE");
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
                    m_aAnimator.SetBool("RUN", false);
                    m_aAnimator.SetBool("ATTACKED", false);
                    m_aAnimator.SetBool("ATTACK", false);
                    m_aAnimator.SetBool("DEATH", false);
                    m_aAnimator.SetBool("GOAWAY", false);
                    m_aAnimator.SetBool("CHASE", false);
                }
                break;
            case "RUN":
                {
                    m_aAnimator.SetBool("IDLE", false);
                    m_aAnimator.SetBool("RUN", true);
                    m_aAnimator.SetBool("ATTACKED", false);
                    m_aAnimator.SetBool("ATTACK", false);
                    m_aAnimator.SetBool("DEATH", false);
                    m_aAnimator.SetBool("GOAWAY", false);
                    m_aAnimator.SetBool("CHASE", false);
                }
                break;
            case "ATTACKED":
                {
                    m_aAnimator.SetBool("IDLE", false);
                    m_aAnimator.SetBool("RUN", false);
                    m_aAnimator.SetBool("ATTACKED", true);
                    m_aAnimator.SetBool("ATTACK", false);
                    m_aAnimator.SetBool("DEATH", false);
                    m_aAnimator.SetBool("GOAWAY", false);
                    m_aAnimator.SetBool("CHASE", false);
                }
                break;
            case "ATTACK":
                {
                    m_aAnimator.SetBool("IDLE", false);
                    m_aAnimator.SetBool("RUN", false);
                    m_aAnimator.SetBool("ATTACKED", false);
                    m_aAnimator.SetBool("ATTACK", true);
                    m_aAnimator.SetBool("DEATH", false);
                    m_aAnimator.SetBool("GOAWAY", false);
                    m_aAnimator.SetBool("CHASE", false);
                }
                break;
            case "DEATH":
                {
                    m_aAnimator.SetBool("IDLE", false);
                    m_aAnimator.SetBool("RUN", false);
                    m_aAnimator.SetBool("ATTACKED", false);
                    m_aAnimator.SetBool("ATTACK", false);
                    m_aAnimator.SetBool("DEATH", true);
                    m_aAnimator.SetBool("GOAWAY", false);
                    m_aAnimator.SetBool("CHASE", false);
                }
                break;
            case "GOAWAY":
                {
                    m_aAnimator.SetBool("IDLE", false);
                    m_aAnimator.SetBool("RUN", false);
                    m_aAnimator.SetBool("ATTACKED", false);
                    m_aAnimator.SetBool("ATTACK", false);
                    m_aAnimator.SetBool("DEATH", false);
                    m_aAnimator.SetBool("GOAWAY", true);
                    m_aAnimator.SetBool("CHASE", false);
                }
                break;
            case "CHASE":
                {
                    m_aAnimator.SetBool("IDLE", false);
                    m_aAnimator.SetBool("RUN", false);
                    m_aAnimator.SetBool("ATTACKED", false);
                    m_aAnimator.SetBool("ATTACK", false);
                    m_aAnimator.SetBool("DEATH", false);
                    m_aAnimator.SetBool("GOAWAY", false);
                    m_aAnimator.SetBool("CHASE", true);
                }
                break;
        }
    }
}
