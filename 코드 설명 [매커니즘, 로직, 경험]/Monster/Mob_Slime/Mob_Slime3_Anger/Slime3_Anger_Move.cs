using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime3_Anger_Move : Slime1_Move
{
    override public bool Attack(float attackspeed)
    {
        if (m_bAttack == true)
            if (m_eMonsterState == E_MONSTER_MOVE_STATE.CHASE ||
                m_eMonsterState == E_MONSTER_MOVE_STATE.ATTACKED || 
                m_eMonsterState == E_MONSTER_MOVE_STATE.IDLE || 
                m_eMonsterState == E_MONSTER_MOVE_STATE.RUN)
            {
                m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.ATTACK, attackspeed);
                return true;
            }
        return false;
    }

    override public E_MONSTER_MOVE_STATE SetMonsterMoveState(E_MONSTER_MOVE_STATE ms, float attackspeed = 0)
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
                        if (m_eMonsterState == E_MONSTER_MOVE_STATE.CHASE ||
                            m_eMonsterState == E_MONSTER_MOVE_STATE.ATTACKED ||
                            m_eMonsterState == E_MONSTER_MOVE_STATE.IDLE ||
                            m_eMonsterState == E_MONSTER_MOVE_STATE.RUN)
                        {
                            if (m_bAttack == true)
                            {
                                SetAnimationParameters("ATTACK");
                                StartCoroutine(ProcessAttack(attackspeed));
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
                        if (m_eMonsterState == E_MONSTER_MOVE_STATE.ATTACKED || m_eMonsterState == E_MONSTER_MOVE_STATE.ATTACK ||
                            m_eMonsterState == E_MONSTER_MOVE_STATE.IDLE || m_eMonsterState == E_MONSTER_MOVE_STATE.RUN)
                            SetAnimationParameters("CHASE");
                    }
                }
                break;
        }

        return ms;
    }
}
