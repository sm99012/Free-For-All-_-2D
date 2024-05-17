using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public SpriteRenderer m_sSpriteRenderer;
    public Transform m_tTransform;
    public Animator m_aAnimator;
    public Rigidbody2D m_rRigdbody;

    Vector3 m_vScale;    // 플레이어 이동 방향
    Vector3 m_vRightPos = new Vector3(1, 1, 1);
    Vector3 m_vLeftPos = new Vector3(-1, 1, 1);
    Vector3 m_vInputDir; // 키보드로부터 입력된 수평ㆍ수직 이동에따른 방향 벡터

    public bool m_bMove; // (m_bMove == true : 플레이어 이동 가능 / m_bMove == false : 플레이어 이동 불가능)
    float m_fMoveRate;   // 플레이어 이동 계수 (달리기 : m_fMoveRate = 1.0f / 구르기 : m_fMoveRate = 1.5f)
    
    // 플레이어가 착용한 무기분류에 따라 상이한 플레이어 애니메이션을 적용하기 위한 FSM
    public enum E_PLAYER_WEAPON_STATE { SWORD(검), AXE(도끼), KNIFE(단검) }
    public E_PLAYER_WEAPON_STATE m_ePlayerWeaponState;
    // 플레이어 동작 FSM
    public enum E_PLAYER_MOVE_STATE { IDLE(가만히 있기), RUN(달리기), ATTACK1_1(기본 공격1), ATTACK1_2(기본 공격2), ATTACK1_3(기본 공격3), ATTACKED(피격), DEATH(사망), ROLL(구르기), GOAWAY(놓아주기), CONVERSATION(상호작용), NULL(ETC) }
    public E_PLAYER_MOVE_STATE m_ePlayerMoveState = E_PLAYER_MOVE_STATE.IDLE;

    // 기본 공격(연계 공격)
    public bool m_bAttack;    // (m_bAttack == true : 플레이어 공격 가능 / m_bAttack == false : 플레이어 공격 불가능)
    public bool m_bAttack1_1; // (m_bAttack1_1 == true : '기본 공격1' 가능)
    public bool m_bAttack1_2; // (m_bAttack1_2 == true : '기본 공격2' 가능)
    public bool m_bAttack1_3; // (m_bAttack1_3 == true : '기본 공격3' 가능)
    // 연계 공격 허용 지속시간
    Coroutine m_cProcess_Attack_Duration = null;       // 연계 공격 가능 시간 계산 코루틴
    Coroutine m_cProcess_AttackToIdle_Duration = null; // 공격 후 딜레이 계산 코루틴
    Coroutine m_cProcess_AttackDelay_Duration = null;  // 플레이어 공격 속도 계산 코루틴(다음 공격까지 기다려야하는 시간 계산)
    float m_fAttack_DurationTime;                      // 연계 공격 가능 시간 계산 변수
    float m_fAttackDelay_DurationTime;                 // 플레이어 가변 공격 속도
    float m_fAttack1_1DurationTime = 0.6f;      // '기본 공격1' 이후 '기본 공격2' 동작이 가능한 시간
    float m_fAttack1_2DurationTime = 0.4f;      // '기본 공격2' 이후 '기본 공격3' 동작이 가능한 시간
    float m_fAttack1_3DurationTime = 1f;        // '기본 공격3' 이후의 공격은 '기본 공격1'로 되돌아간다. 이후 추가될 '기본 공격4' 등을 위해 설정해둔 임의의 값

    // 구르기
    public bool m_bRoll;                       // 구르기 가능 여부 (m_bRoll == true : 구르기 가능 / m_bRoll == false : 구르기 불가능)    
    Coroutine m_cProcess_Roll_Cooltime = null; // 구르기 쿨타임 계산 코루틴
    Coroutine m_cProcess_RollToIdle = null;    // 구르기 지속시간 계산 코루틴
    float m_fCooltime_Roll = 3;                // 구르기 쿨타임
    
    // 피격
    Coroutine m_cProcess_Attacked;      // 플레이어 피격 계산 코루틴
    Coroutine m_cProcess_KnockBack;     // 플레이어 넉백 계산 코루틴
    float m_fAttackedToIdleTime = 0.3f; // 플레이어 피격 후 딜레이
    Coroutine m_cProcess_Power = null;  // 플레이어 피격 가능 시간 계산 코루틴
    public bool m_bPower;               // 플레이어 피격 가능 여부 (m_bPower == true : 플레이어 피격 불가능 / m_bPower == false : 플레이어 피격 가능)
                                        // m_bPower 변수는 '피격 후 피격 불가 상태 계산', '구르기 지속 중 피격 불가 상태 계산'에 사용된다.

    // 놓아주기
    Coroutine m_cProcess_Goaway_Cooltime = null; // 놓아주기 쿨타임 계산 코루틴
    Coroutine m_cProcess_Goaway_Duration = null; // 놓아주기 시전 시간 계산 코루틴
    public bool m_bGoaway;                       // 놓아주기 가능 여부 (m_bGoaway == true : 놓아주기 가능 / m_bGoaway == false : 놓아주기 불가능)
    public bool m_bGoaway_Success;               // 놓아주기 성공 여부 (m_bGoaway_Success == true : 놓아주기 성공 / m_bGoaway_Success == false : 놓아주기 실패)
    public float m_fGoaway_Cooltime = 10f;       // 놓아주기 쿨타임
    public float m_fGoaway_Durationtime = 3f;    // 놓아주기 시전 시간
    
    public void InitialSet()
    {
        m_sSpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        m_tTransform = this.gameObject.GetComponent<Transform>();
        m_aAnimator = this.gameObject.GetComponent<Animator>();
        m_rRigdbody = this.gameObject.GetComponent<Rigidbody2D>();
        
        m_vScale = m_vRightPos;

        m_bAttack = true;

        m_bAttack1_1 = true;
        m_bAttack1_2 = false;
        m_bAttack1_3 = false;

        m_bMove = true;

        m_bRoll = true;

        m_bPower = false;

        m_bGoaway = true;
        m_bGoaway_Success = false;
    }

    // 플레이어 이동 관련 함수
    // Player_Total.cs에서 키입력을 통해 이 함수가 실행된다. 플레이어의 달리기 동작을 수행한다.
    // 플레이어 동작 FSM 정보를 반환
    public E_PLAYER_MOVE_STATE Move(int h, int v, int fspeed) // h(수평 이동 값), v(수직 이동 값), fspeed(플레이어 이동속도)
    {
        if (m_bMove == true)
        {
            if (m_ePlayerMoveState == E_PLAYER_MOVE_STATE.IDLE || m_ePlayerMoveState == E_PLAYER_MOVE_STATE.RUN ||
               m_ePlayerMoveState == E_PLAYER_MOVE_STATE.ROLL || m_ePlayerMoveState == E_PLAYER_MOVE_STATE.GOAWAY)
            {
                SetScale(h, v);

                if (h == 0 && v == 0)
                    m_vInputDir = Vector3.zero;
                else if (h != 0 && v != 0)
                    m_vInputDir = new Vector3(h / 1.4f, v / 1.4f); // 플레이어의 대각선 이동 시 추가 보정값을 적용
                else
                    m_vInputDir = new Vector3(h, v);
                    
                if (m_ePlayerMoveState == E_PLAYER_MOVE_STATE.IDLE || m_ePlayerMoveState == E_PLAYER_MOVE_STATE.RUN) // 달리기 상태
                {
                    m_fMoveRate = 1f;   // 기본 이동 계수
                    m_rRigdbody.MovePosition(this.gameObject.transform.position + (m_vInputDir * fspeed * 0.016f * 0.01f * m_fMoveRate));
                }
                if (m_ePlayerMoveState == E_PLAYER_MOVE_STATE.ROLL) // 구르기 상태
                {
                    m_fMoveRate = 1.5f; // 달리기보다 빠른 이동이 가능한 구르기
                    m_rRigdbody.MovePosition(this.gameObject.transform.position + m_vInputDir * fspeed * 0.016f * 0.01f * m_fMoveRate);
                }
            }
            return m_ePlayerMoveState; // 플레이어 동작 FSM 정보를 반환 { IDEL(가만히 있기), RUN(달리기), ROLL(구르기) }
        }
        else
        {
            return E_PLAYER_MOVE_STATE.NULL; // 플레이어 동작 FSM 정보를 반환 { NULL(플레이어 이동 불가 상태(기절, 속박 등의 상태이상)) }
        }
    }
    public Vector3 Get_MoveDir()
    {
        return m_vInputDir;
    }

    // 플레이어 방향 설정, UML 적용
    void SetScale(int h, int v)
    {
        if (m_ePlayerMoveState == E_PLAYER_MOVE_STATE.IDLE || m_ePlayerMoveState == E_PLAYER_MOVE_STATE.RUN)
        {
            if (h == 1)
            {
                m_vScale = m_vRightPos;
                m_ePlayerMoveState = SetPlayerMoveState(E_PLAYER_MOVE_STATE.RUN);
            }
            else if (h == -1)
            {
                m_vScale = m_vLeftPos;
                m_ePlayerMoveState = SetPlayerMoveState(E_PLAYER_MOVE_STATE.RUN);
            }

            if (v == 1)
            {
                m_ePlayerMoveState = SetPlayerMoveState(E_PLAYER_MOVE_STATE.RUN);
            }
            else if (v == -1)
            {
                m_ePlayerMoveState = SetPlayerMoveState(E_PLAYER_MOVE_STATE.RUN);
            }

            if (h == 0 && v == 0)
                m_ePlayerMoveState = SetPlayerMoveState(E_PLAYER_MOVE_STATE.IDLE);
        }
        if (m_ePlayerMoveState == E_PLAYER_MOVE_STATE.ROLL)
        {
            if (h == 1)
            {
                m_vScale = m_vRightPos;
            }
            else if (h == -1)
            {
                m_vScale = m_vLeftPos;
            }
        }

        m_tTransform.localScale = m_vScale;
    }

    // 플레이어 위치 설정.
    public void ChangeMap(Vector3 pos)
    {
        m_tTransform.position = pos;
    }

    public int Attack()
    {
        if (m_ePlayerMoveState == E_PLAYER_MOVE_STATE.IDLE || m_ePlayerMoveState == E_PLAYER_MOVE_STATE.RUN)
        {
            if (m_bAttack == true)
            {
                if (m_bAttack1_1 == true)
                {
                    m_ePlayerMoveState = SetPlayerMoveState(E_PLAYER_MOVE_STATE.ATTACK1_1);
                    //Debug.Log("Player Attack1_1");
                    return 1;
                }
                else if (m_bAttack1_2 == true)
                {
                    m_ePlayerMoveState = SetPlayerMoveState(E_PLAYER_MOVE_STATE.ATTACK1_2);
                    //Debug.Log("Player Attack1_2");
                    return 2;
                }
                else if (m_bAttack1_3 == true)
                {
                    m_ePlayerMoveState = SetPlayerMoveState(E_PLAYER_MOVE_STATE.ATTACK1_3);
                    //Debug.Log("Player Attack1_3");
                    return 3;
                }
            }
        }

        return 0;
    }

    public void SetAttackSpeed(float atkspd)
    {
          m_fAttackDelay_DurationTime
    }

    IEnumerator ProcessAttackDelay()
    {
        m_bAttack = false;
        while (m_fAttackDelay_DurationTime > 0)
        {
            //m_fAttackDelay_DurationTime -= Time.deltaTime;
            //yield return null;
            m_fAttackDelay_DurationTime -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        m_bAttack = true;
        if (m_cProcess_AttackDelay_Duration != null)
            m_cProcess_AttackDelay_Duration = null;
    }
    IEnumerator ProcessAttack1_1()
    {
        m_fAttack_DurationTime = m_fAttack1_1DurationTime; //Debug.Log(m_fAttack_DurationTime);
        m_bAttack1_1 = false;
        m_bAttack1_2 = true;
        m_bAttack1_3 = false;
        while (m_fAttack_DurationTime > 0)
        {
            //m_fAttack_DurationTime -= Time.deltaTime;
            //yield return null;
            m_fAttack_DurationTime -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        m_cProcess_Attack_Duration = null;
        m_bAttack1_1 = true;
        m_bAttack1_2 = false;
        m_bAttack1_3 = false;
    }
    IEnumerator ProcessAttack1_2()
    {
        m_fAttack_DurationTime = m_fAttack1_2DurationTime; //Debug.Log(m_fAttack_DurationTime);
        m_bAttack1_1 = false;
        m_bAttack1_2 = false;
        m_bAttack1_3 = true;
        while (m_fAttack_DurationTime > 0)
        {
            //m_fAttack_DurationTime -= Time.deltaTime;
            //yield return null;
            m_fAttack_DurationTime -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        m_cProcess_Attack_Duration = null;
        m_bAttack1_1 = true;
        m_bAttack1_2 = false;
        m_bAttack1_3 = false;
    }
    IEnumerator ProcessAttack1_3()
    {
        m_fAttack_DurationTime = m_fAttack1_3DurationTime; //Debug.Log(m_fAttack_DurationTime);
        m_bAttack1_1 = true;
        m_bAttack1_2 = false;
        m_bAttack1_3 = false;
        while (m_fAttack_DurationTime > 0)
        {
            //m_fAttack_DurationTime -= Time.deltaTime;
            //yield return null;
            m_fAttack_DurationTime -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        m_cProcess_Attack_Duration = null;
        m_bAttack1_1 = true;
        m_bAttack1_2 = false;
        m_bAttack1_3 = false;
    }
    // 공격 후 딜레이타임 설정
    IEnumerator ProcessAttackToIdle(float ftime)
    {
        m_bMove = false;
        yield return new WaitForSeconds(ftime);
        m_ePlayerMoveState = SetPlayerMoveState(E_PLAYER_MOVE_STATE.IDLE);
        if (m_cProcess_KnockBack == null && m_cProcess_Attacked == null)
            m_bMove = true;
        m_cProcess_AttackToIdle_Duration = null;
    }
    void Attack1_1()
    {
        m_cProcess_Attack_Duration = null;
        m_cProcess_Attack_Duration = StartCoroutine(ProcessAttack1_1());
        m_cProcess_AttackToIdle_Duration = StartCoroutine(ProcessAttackToIdle(0.4f));//0.3f
    }
    void Attack1_2()
    {
        m_cProcess_Attack_Duration = null;
        m_cProcess_Attack_Duration = StartCoroutine(ProcessAttack1_2());
        m_cProcess_AttackToIdle_Duration = StartCoroutine(ProcessAttackToIdle(0.4f));//0.3f
    }
    void Attack1_3()
    {
        m_cProcess_Attack_Duration = null;
        m_cProcess_Attack_Duration = StartCoroutine(ProcessAttack1_3());
        m_cProcess_AttackToIdle_Duration = StartCoroutine(ProcessAttackToIdle(0.6f));//0.6f
    }
    
    public bool Attacked(float time, float speed, Vector3 dir)
    {
        if (m_bPower == false)
        {
            m_bMove = false;
            // GOAWAY 기능 취소
            if (m_cProcess_Goaway_Duration != null)
            {
                StopCoroutine(m_cProcess_Goaway_Duration);
                //m_bMove = true;
            }
            m_cProcess_Power = StartCoroutine(ProcessPower());
            if (m_ePlayerMoveState == E_PLAYER_MOVE_STATE.IDLE || m_ePlayerMoveState == E_PLAYER_MOVE_STATE.RUN ||
                m_ePlayerMoveState == E_PLAYER_MOVE_STATE.GOAWAY)
            {
                m_cProcess_Attacked = StartCoroutine(ProcessAttackedToIdle());
                m_ePlayerMoveState = SetPlayerMoveState(E_PLAYER_MOVE_STATE.ATTACKED);
            }
            KnockBack(time, speed, dir);

            return true;
        }
        return false;
    }

    IEnumerator ProcessAttackedToIdle()
    {
        m_bMove = false;
        yield return new WaitForSeconds(m_fAttackedToIdleTime);
        m_ePlayerMoveState = SetPlayerMoveState(E_PLAYER_MOVE_STATE.IDLE);
        if (m_cProcess_KnockBack == null && m_cProcess_AttackToIdle_Duration == null)
            m_bMove = true;
        m_cProcess_Attacked = null;
    }
    IEnumerator ProcessPower()
    {
        m_bPower = true;
        yield return new WaitForSeconds(1f);
        m_bPower = false;
        m_cProcess_Power = null;
    }

    // 넉백
    public void KnockBack(float time, float fspeed, Vector3 dir)
    {
        if (m_cProcess_KnockBack == null)
            m_cProcess_KnockBack = StartCoroutine(ProcessKnockBack(time, fspeed * 0.3f, dir));
    }
    IEnumerator ProcessKnockBack(float time, float fspeed, Vector3 dir)
    {
        float ftime = time;
        m_bMove = false;
        while (ftime > 0)
        {
            ftime -= 0.016f;
            yield return new WaitForSeconds(0.016f);
            m_rRigdbody.MovePosition(this.gameObject.transform.position + dir * fspeed * 0.016f * 0.01f * 1.5f);
            Player_Total.Instance.CameraMove(this.gameObject.transform.position + dir * fspeed * 0.05f * 0.01f);
        }
        // if (m_cProcess_AttackToIdle_Duration == null && m_cProcess_Attacked == null)
        //if (m_cProcess_AttackToIdle_Duration == null)
        m_bMove = true;
        m_cProcess_KnockBack = null;
    }

    // 플레이어 사망  - FSM 내부로 옮길 필요.
    public void Death()
    {
        if (m_cProcess_KnockBack != null)
        {
            StopCoroutine(m_cProcess_KnockBack);
            m_cProcess_KnockBack = null;
        }
        if (m_cProcess_AttackDelay_Duration != null)
        {
            StopCoroutine(m_cProcess_AttackDelay_Duration);
            m_cProcess_AttackDelay_Duration = null;
        }
        if (m_cProcess_AttackToIdle_Duration != null)
        {
            StopCoroutine(m_cProcess_AttackToIdle_Duration);
            m_cProcess_AttackToIdle_Duration = null;
        }
        if (m_cProcess_Attacked != null)
        {
            StopCoroutine(m_cProcess_Attacked);
            m_cProcess_Attacked = null;
        }
        if (m_cProcess_Attack_Duration != null)
        {
            StopCoroutine(m_cProcess_Attack_Duration);
            m_cProcess_Attack_Duration = null;
        }
        if (m_cProcess_Goaway_Cooltime != null)
        {
            StopCoroutine(m_cProcess_Goaway_Cooltime);
            m_cProcess_Goaway_Cooltime = null;
        }
        if (m_cProcess_Goaway_Duration != null)
        {
            StopCoroutine(m_cProcess_Goaway_Duration);
            m_cProcess_Goaway_Duration = null;
        }
        if (m_cProcess_Roll_Cooltime != null)
        {
            StopCoroutine(m_cProcess_Roll_Cooltime);
            m_cProcess_Roll_Cooltime = null;
        }
        if (m_cProcess_RollToIdle != null)
        {
            StopCoroutine(m_cProcess_RollToIdle);
            m_cProcess_RollToIdle = null;
        }
        if (m_cProcess_Power != null)
        {
            StopCoroutine(m_cProcess_Power);
            m_cProcess_Power = null;
        }

        m_bMove = false;
        m_bPower = true;
        m_ePlayerMoveState = SetPlayerMoveState(E_PLAYER_MOVE_STATE.DEATH);
        //StopAllCoroutines();
    } 

    public void ReTry()
    {
        m_bAttack = true;
        m_bAttack1_1 = true;
        m_bAttack1_2 = false;
        m_bAttack1_3 = false;
        m_bMove = true;
        m_bRoll = true;
        m_bPower = false;
        m_bGoaway = true;
        m_bGoaway_Success = false;

        StartCoroutine(Process_ReTry());

        m_ePlayerMoveState = SetPlayerMoveState(E_PLAYER_MOVE_STATE.IDLE);
    }

    IEnumerator Process_ReTry()
    {
        m_bPower = true;
        yield return new WaitForSeconds(3f);
        m_bPower = false;
    }

    // 플레이어 구르기  - FSM 내부로 옮길 필요.
    public bool Roll()
    {
        if (m_ePlayerMoveState == E_PLAYER_MOVE_STATE.IDLE || m_ePlayerMoveState == E_PLAYER_MOVE_STATE.RUN || 
            m_ePlayerMoveState == E_PLAYER_MOVE_STATE.ATTACK1_1 || m_ePlayerMoveState == E_PLAYER_MOVE_STATE.ATTACK1_2 ||
            m_ePlayerMoveState == E_PLAYER_MOVE_STATE.ATTACK1_3 || m_ePlayerMoveState == E_PLAYER_MOVE_STATE.GOAWAY)
        {
            if (m_bRoll == true)
            {
                // 공격 모션(후딜) 캔슬
                if (m_cProcess_AttackToIdle_Duration != null)
                {
                    StopCoroutine(m_cProcess_AttackToIdle_Duration);
                    m_cProcess_AttackToIdle_Duration = null;
                }
                // GOAWAY 기능 취소
                if (m_cProcess_Goaway_Duration != null)
                {
                    StopCoroutine(m_cProcess_Goaway_Duration);
                    m_cProcess_Goaway_Duration = null;
                    m_bGoaway_Success = false;
                    //m_bMove = true;
                }
                // ATTACKED 캔슬
                //if (m_cProcess_Attacked != null)
                //    StopCoroutine(m_cProcess_Attacked);
                if (m_cProcess_KnockBack != null)
                {
                    StopCoroutine(m_cProcess_KnockBack);
                    m_cProcess_KnockBack = null;
                    m_bMove = true;
                    m_cProcess_KnockBack = null;
                }

                m_ePlayerMoveState = SetPlayerMoveState(E_PLAYER_MOVE_STATE.ROLL);

                if (m_cProcess_Power != null)
                {
                    StopCoroutine(m_cProcess_Power);
                    m_cProcess_Power = null;
                    m_bPower = false;
                }

                m_bMove = true;

                m_cProcess_Roll_Cooltime = StartCoroutine(ProcessRoll_Cooltime());
                m_cProcess_RollToIdle = StartCoroutine(ProcessRollToIdle());

                return true;
            }
            return false;
        }
        return false;
    }
    IEnumerator ProcessRoll_Cooltime()
    {
        //m_bMove = true;
        m_bRoll = false;
        yield return new WaitForSeconds(m_fCooltime_Roll);
        if (m_cProcess_Roll_Cooltime != null)
            m_cProcess_Roll_Cooltime = null;
        m_bRoll = true;
    }
    IEnumerator ProcessRollToIdle()
    {
        m_bPower = true;
        yield return new WaitForSeconds(0.7f);
        if (m_cProcess_RollToIdle != null)
            m_cProcess_RollToIdle = null;
        m_bPower = false;
        m_ePlayerMoveState = SetPlayerMoveState(E_PLAYER_MOVE_STATE.IDLE);
    }

    // 플레이어 놓아주기  - FSM 내부로 옮길 필요.
    public void Goaway()
    {
        if (m_ePlayerMoveState == E_PLAYER_MOVE_STATE.IDLE || m_ePlayerMoveState == E_PLAYER_MOVE_STATE.RUN)
        {
            if (m_bGoaway == true)
            {
                m_bGoaway_Success = false;
                m_ePlayerMoveState = SetPlayerMoveState(E_PLAYER_MOVE_STATE.GOAWAY);
                m_cProcess_Goaway_Cooltime = StartCoroutine(ProcessGoaway_Cooltime());
                m_cProcess_Goaway_Duration = StartCoroutine(ProcessGoawayToIdle());
            }
        }
    }
    IEnumerator ProcessGoaway_Cooltime()
    {
        m_bGoaway = false;
        yield return new WaitForSeconds(m_fGoaway_Cooltime);
        if (m_cProcess_Goaway_Cooltime != null)
            m_cProcess_Goaway_Cooltime = null;
        m_bGoaway = true;
    }

    IEnumerator ProcessGoawayToIdle()
    {
        m_bMove = false;
        yield return new WaitForSeconds(m_fGoaway_Durationtime);
        m_bMove = true;
        m_bGoaway_Success = true;
        if (m_cProcess_Goaway_Duration != null)
            m_cProcess_Goaway_Duration = null;
        m_ePlayerMoveState = SetPlayerMoveState(E_PLAYER_MOVE_STATE.IDLE);
        yield return null;
        m_bGoaway_Success = false;
    }
    // Goaway 키다운 지속시간
    public void Cancel_Goaway()
    {
        // GOAWAY 기능 취소
        if (m_cProcess_Goaway_Duration != null)
        {
            StopCoroutine(m_cProcess_Goaway_Duration);
            m_ePlayerMoveState = SetPlayerMoveState(E_PLAYER_MOVE_STATE.IDLE);
            m_bMove = true;
        }
    }

    // Conversation
    public bool Conversation()
    {
        if (m_ePlayerMoveState == E_PLAYER_MOVE_STATE.IDLE || m_ePlayerMoveState == E_PLAYER_MOVE_STATE.RUN)
        {
            m_ePlayerMoveState = SetPlayerMoveState(E_PLAYER_MOVE_STATE.CONVERSATION);
            return true;
        }
        else
            return false;
    }

    // 무기 타입별 Animation 적용
    public void SetAnimation_Weapon(E_ITEM_EQUIP_MAINWEAPON_TYPE iemt)
    {
        switch (iemt)
        {
            case E_ITEM_EQUIP_MAINWEAPON_TYPE.SWORD:
                {
                    m_ePlayerWeaponState = SetPlayerWeaponState(E_PLAYER_WEAPON_STATE.SWORD);
                } break;
            case E_ITEM_EQUIP_MAINWEAPON_TYPE.AXE:
                {
                    m_ePlayerWeaponState = SetPlayerWeaponState(E_PLAYER_WEAPON_STATE.AXE);
                } break;
            case E_ITEM_EQUIP_MAINWEAPON_TYPE.KNIFE:
                {
                    m_ePlayerWeaponState = SetPlayerWeaponState(E_PLAYER_WEAPON_STATE.KNIFE);
                } break;
            default:
                {
                    m_ePlayerWeaponState = SetPlayerWeaponState(E_PLAYER_WEAPON_STATE.SWORD);
                } break;
        }
    }

    // 장비 변경 시 연계 공격 초기화.
    public void Equip()
    {
        if (m_cProcess_Attack_Duration != null)
        {
            StopCoroutine(m_cProcess_Attack_Duration);
            m_cProcess_Attack_Duration = null;
        }

        m_bAttack1_1 = true;
        m_bAttack1_2 = false;
        m_bAttack1_3 = false;
    }

    // FSM 관리
    // PlayerWeaponState
    public E_PLAYER_WEAPON_STATE SetPlayerWeaponState(E_PLAYER_WEAPON_STATE pws)
    {
        switch(pws)
        {
            case E_PLAYER_WEAPON_STATE.SWORD:
                {
                    m_aAnimator.SetBool("Equip_Sword", true);
                    m_aAnimator.SetBool("Equip_Axe", false);
                    m_aAnimator.SetBool("Equip_Knife", false);
                } break;
            case E_PLAYER_WEAPON_STATE.AXE:
                {
                    m_aAnimator.SetBool("Equip_Sword", false);
                    m_aAnimator.SetBool("Equip_Axe", true);
                    m_aAnimator.SetBool("Equip_Knife", false);
                } break;
            case E_PLAYER_WEAPON_STATE.KNIFE:
                {
                    m_aAnimator.SetBool("Equip_Sword", false);
                    m_aAnimator.SetBool("Equip_Axe", false);
                    m_aAnimator.SetBool("Equip_Knife", true);
                } break;
            default:
                {
                    m_aAnimator.SetBool("Equip_Sword", true);
                    m_aAnimator.SetBool("Equip_Axe", false);
                    m_aAnimator.SetBool("Equip_Knife", false);
                } break;
        }

        if (m_ePlayerMoveState == E_PLAYER_MOVE_STATE.RUN)
        {
            //Debug.Log(m_aAnimator.GetParameter(12).name);
            //Debug.Log(m_aAnimator.GetParameter(12).defaultFloat);

            //RuntimeAnimatorController rac = m_aAnimator.runtimeAnimatorController;
            //Debug.Log(rac.animationClips.Length);
            //for (int i = 0; i < rac.animationClips.Length; i++)
            //{
            //    Debug.Log(i + " / " + rac.animationClips[i].name + " / " + rac.animationClips[i].length);
            //}

            //Debug.Log(m_aAnimator.GetCurrentAnimatorStateInfo(1 << LayerMask.NameToLayer("Player Base")).length);
            //Debug.Log(m_aAnimator.GetCurrentAnimatorStateInfo(0).length);

            float startingtime;
            RuntimeAnimatorController rac = m_aAnimator.runtimeAnimatorController;

            if (m_aAnimator.GetBool("Equip_Sword") == true)
            {
                if (m_aAnimator.GetCurrentAnimatorStateInfo(0).IsName("Knife_Run") || m_aAnimator.GetCurrentAnimatorStateInfo(0).IsName("Axe_Run"))
                {
                    startingtime = m_aAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                    startingtime = startingtime % 1f;
                    m_aAnimator.Play("Sword_Run", 0, startingtime);
                    //Debug.Log(m_aAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime + " / " + startingtime);
                }
            }
            if (m_aAnimator.GetBool("Equip_Knife") == true)
            {
                if (m_aAnimator.GetCurrentAnimatorStateInfo(0).IsName("Sword_Run") || m_aAnimator.GetCurrentAnimatorStateInfo(0).IsName("Axe_Run"))
                {
                    startingtime = m_aAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                    startingtime = startingtime % 1f;
                    m_aAnimator.Play("Knife_Run", 0, startingtime);
                    //Debug.Log(m_aAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime + " / " + startingtime);
                }
            }
            if (m_aAnimator.GetBool("Equip_Axe") == true)
            {
                if (m_aAnimator.GetCurrentAnimatorStateInfo(0).IsName("Sword_Run") || m_aAnimator.GetCurrentAnimatorStateInfo(0).IsName("Knife_Run"))
                {
                    startingtime = m_aAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                    startingtime = startingtime % 1f;
                    m_aAnimator.Play("Axe_Run", 0, startingtime);
                    //Debug.Log(m_aAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime + " / " + startingtime);
                }
            }
            //if (m_aAnimator.GetBool("Equip_Knife") && m_aAnimator.GetCurrentAnimatorStateInfo(0).IsName("Knife_Run"))
            //{
            //    startingtime = m_aAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            //    startingtime = startingtime % 1.3f;
            //    m_aAnimator.Play("Knife_Run", 0, startingtime);
            //}
            //if (m_aAnimator.GetBool("Axe_Knife") && m_aAnimator.GetCurrentAnimatorStateInfo(0).IsName("Axe_Run"))
            //{
            //    startingtime = m_aAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            //    startingtime = startingtime % 1.3f;
            //    m_aAnimator.Play("Axe_Run", 0, startingtime);
            //}
        }

        return pws;
    }

    // PlayerMoveState
    public E_PLAYER_MOVE_STATE SetPlayerMoveState(E_PLAYER_MOVE_STATE pms)
    {
        switch (pms)
        {
            case E_PLAYER_MOVE_STATE.IDLE:
                {
                    if (m_ePlayerMoveState != pms)
                    {
                        SetAnimatorParameters("Idle");
                    }
                }
                break;
            case E_PLAYER_MOVE_STATE.RUN:
                {
                    if (m_ePlayerMoveState != pms)
                    {
                        SetAnimatorParameters("Run");
                    }
                }
                break;
            case E_PLAYER_MOVE_STATE.ATTACK1_1:
                {
                    if (m_ePlayerMoveState != pms)
                    {
                        if (m_cProcess_AttackDelay_Duration != null)
                        {
                            StopCoroutine(m_cProcess_AttackDelay_Duration);
                        }
                        m_cProcess_AttackDelay_Duration = StartCoroutine(ProcessAttackDelay());
                        if (m_cProcess_Attack_Duration != null)
                            StopCoroutine(m_cProcess_Attack_Duration);
                        SetAnimatorParameters("Attack1_1");
                        Attack1_1();
                    }
                }
                break;
            case E_PLAYER_MOVE_STATE.ATTACK1_2:
                {
                    if (m_ePlayerMoveState != pms)
                    {
                        if (m_cProcess_AttackDelay_Duration != null)
                        {
                            StopCoroutine(m_cProcess_AttackDelay_Duration);
                        }
                        m_cProcess_AttackDelay_Duration = StartCoroutine(ProcessAttackDelay());
                        if (m_cProcess_Attack_Duration != null)
                            StopCoroutine(m_cProcess_Attack_Duration);
                        SetAnimatorParameters("Attack1_2");
                        Attack1_2();
                    }
                }
                break;
            case E_PLAYER_MOVE_STATE.ATTACK1_3:
                {
                    if (m_ePlayerMoveState != pms)
                    {
                        if (m_cProcess_AttackDelay_Duration != null)
                        {
                            StopCoroutine(m_cProcess_AttackDelay_Duration);
                        }
                        m_cProcess_AttackDelay_Duration = StartCoroutine(ProcessAttackDelay());
                        if (m_cProcess_Attack_Duration != null)
                            StopCoroutine(m_cProcess_Attack_Duration);
                        SetAnimatorParameters("Attack1_3");
                        Attack1_3();
                    }
                }
                break;
            case E_PLAYER_MOVE_STATE.ATTACKED:
                {
                    if (m_ePlayerMoveState != pms)
                    {
                        //if (m_cProcess_Attack_Duration != null)
                        //    StopCoroutine(m_cProcess_Attack_Duration);
                        SetAnimatorParameters("Attacked");
                    }
                }
                break;
            case E_PLAYER_MOVE_STATE.DEATH:
                {
                    if (m_ePlayerMoveState != pms)
                    {
                        SetAnimatorParameters("Death");
                        m_bMove = false;
                    }
                }
                break;
            case E_PLAYER_MOVE_STATE.ROLL:
                {
                    if (m_ePlayerMoveState != pms)
                    {
                        SetAnimatorParameters("Roll");
                    }
                }
                break;
            case E_PLAYER_MOVE_STATE.GOAWAY:
                {
                    if (m_ePlayerMoveState != pms)
                    {
                        SetAnimatorParameters("Goaway");
                    }
                }
                break;
            case E_PLAYER_MOVE_STATE.CONVERSATION:
                {
                    if (m_ePlayerMoveState != pms)
                    {
                        SetAnimatorParameters("Conversation");
                    }
                }
                break;
        }

        return pms;
    }
    // Animation 관리
    public void SetAnimatorParameters(string str)
    {
        switch (str)
        {
            case "Idle":
                {
                    m_aAnimator.SetBool("Idle", true);
                    m_aAnimator.SetBool("Run", false);
                    m_aAnimator.SetBool("Attack1_1", false);
                    m_aAnimator.SetBool("Attack1_2", false);
                    m_aAnimator.SetBool("Attack1_3", false);
                    m_aAnimator.SetBool("Attacked", false);
                    m_aAnimator.SetBool("Death", false);
                    m_aAnimator.SetBool("Roll", false);
                    m_aAnimator.SetBool("Goaway", false);
                    m_aAnimator.SetBool("Conversation", false);
                }
                break;
            case "Run":
                {
                    m_aAnimator.SetBool("Idle", false);
                    m_aAnimator.SetBool("Run", true);
                    m_aAnimator.SetBool("Attack1_1", false);
                    m_aAnimator.SetBool("Attack1_2", false);
                    m_aAnimator.SetBool("Attack1_3", false);
                    m_aAnimator.SetBool("Attacked", false);
                    m_aAnimator.SetBool("Death", false);
                    m_aAnimator.SetBool("Roll", false);
                    m_aAnimator.SetBool("Goaway", false);
                    m_aAnimator.SetBool("Conversation", false);
                }
                break;
            case "Attack1_1":
                {
                    m_aAnimator.SetBool("Idle", false);
                    m_aAnimator.SetBool("Run", false);
                    m_aAnimator.SetBool("Attack1_1", true);
                    m_aAnimator.SetBool("Attack1_2", false);
                    m_aAnimator.SetBool("Attack1_3", false);
                    m_aAnimator.SetBool("Attacked", false);
                    m_aAnimator.SetBool("Death", false);
                    m_aAnimator.SetBool("Roll", false);
                    m_aAnimator.SetBool("Goaway", false);
                    m_aAnimator.SetBool("Conversation", false);
                }
                break;
            case "Attack1_2":
                {
                    m_aAnimator.SetBool("Idle", false);
                    m_aAnimator.SetBool("Run", false);
                    m_aAnimator.SetBool("Attack1_1", false);
                    m_aAnimator.SetBool("Attack1_2", true);
                    m_aAnimator.SetBool("Attack1_3", false);
                    m_aAnimator.SetBool("Attacked", false);
                    m_aAnimator.SetBool("Death", false);
                    m_aAnimator.SetBool("Roll", false);
                    m_aAnimator.SetBool("Goaway", false);
                    m_aAnimator.SetBool("Conversation", false);
                }
                break;
            case "Attack1_3":
                {
                    m_aAnimator.SetBool("Idle", false);
                    m_aAnimator.SetBool("Run", false);
                    m_aAnimator.SetBool("Attack1_1", false);
                    m_aAnimator.SetBool("Attack1_2", false);
                    m_aAnimator.SetBool("Attack1_3", true);
                    m_aAnimator.SetBool("Attacked", false);
                    m_aAnimator.SetBool("Death", false);
                    m_aAnimator.SetBool("Roll", false);
                    m_aAnimator.SetBool("Goaway", false);
                    m_aAnimator.SetBool("Conversation", false);
                }
                break;
            case "Attacked":
                {
                    m_aAnimator.SetBool("Idle", false);
                    m_aAnimator.SetBool("Run", false);
                    m_aAnimator.SetBool("Attack1_1", false);
                    m_aAnimator.SetBool("Attack1_2", false);
                    m_aAnimator.SetBool("Attack1_3", false);
                    m_aAnimator.SetBool("Attacked", true);
                    m_aAnimator.SetBool("Death", false);
                    m_aAnimator.SetBool("Roll", false);
                    m_aAnimator.SetBool("Goaway", false);
                    m_aAnimator.SetBool("Conversation", false);
                }
                break;
            case "Death":
                {
                    m_aAnimator.SetBool("Idle", false);
                    m_aAnimator.SetBool("Run", false);
                    m_aAnimator.SetBool("Attack1_1", false);
                    m_aAnimator.SetBool("Attack1_2", false);
                    m_aAnimator.SetBool("Attack1_3", false);
                    m_aAnimator.SetBool("Attacked", false);
                    m_aAnimator.SetBool("Death", true);
                    m_aAnimator.SetBool("Roll", false);
                    m_aAnimator.SetBool("Goaway", false);
                    m_aAnimator.SetBool("Conversation", false);
                }
                break;
            case "Roll":
                {
                    m_aAnimator.SetBool("Idle", false);
                    m_aAnimator.SetBool("Run", false);
                    m_aAnimator.SetBool("Attack1_1", false);
                    m_aAnimator.SetBool("Attack1_2", false);
                    m_aAnimator.SetBool("Attack1_3", false);
                    m_aAnimator.SetBool("Attacked", false);
                    m_aAnimator.SetBool("Death", false);
                    m_aAnimator.SetBool("Roll", true);
                    m_aAnimator.SetBool("Goaway", false);
                    m_aAnimator.SetBool("Conversation", false);
                }
                break;
            case "Goaway":
                {
                    m_aAnimator.SetBool("Idle", false);
                    m_aAnimator.SetBool("Run", false);
                    m_aAnimator.SetBool("Attack1_1", false);
                    m_aAnimator.SetBool("Attack1_2", false);
                    m_aAnimator.SetBool("Attack1_3", false);
                    m_aAnimator.SetBool("Attacked", false);
                    m_aAnimator.SetBool("Death", false);
                    m_aAnimator.SetBool("Roll", false);
                    m_aAnimator.SetBool("Goaway", true);
                    m_aAnimator.SetBool("Conversation", false);
                }
                break;
            case "Conversation":
                {
                    m_aAnimator.SetBool("Idle", false);
                    m_aAnimator.SetBool("Run", false);
                    m_aAnimator.SetBool("Attack1_1", false);
                    m_aAnimator.SetBool("Attack1_2", false);
                    m_aAnimator.SetBool("Attack1_3", false);
                    m_aAnimator.SetBool("Attacked", false);
                    m_aAnimator.SetBool("Death", false);
                    m_aAnimator.SetBool("Roll", false);
                    m_aAnimator.SetBool("Goaway", false);
                    m_aAnimator.SetBool("Conversation", true);
                }
                break;
        }
    }
}
