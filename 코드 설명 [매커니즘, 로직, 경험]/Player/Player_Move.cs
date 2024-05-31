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
    
    // 플레이어 착용 무기별 애니메이션 FSM. 플레이어가 착용한 무기분류에 따라 상이한 플레이어 애니메이션을 적용하기 위한 FSM
    public enum E_PLAYER_WEAPON_STATE { SWORD(검), AXE(도끼), KNIFE(단검) }
    public E_PLAYER_WEAPON_STATE m_ePlayerWeaponState;
    // 플레이어 동작 FSM
    public enum E_PLAYER_MOVE_STATE { IDLE(가만히 있기), RUN(달리기), ATTACK1_1(기본 공격1), ATTACK1_2(기본 공격2), ATTACK1_3(기본 공격3), ATTACKED(피격), DEATH(사망), ROLL(구르기), GOAWAY(놓아주기), CONVERSATION(상호작용), NULL(ETC) }
    public E_PLAYER_MOVE_STATE m_ePlayerMoveState = E_PLAYER_MOVE_STATE.IDLE;

    // 이동
    Vector3 m_vScale;    // 플레이어가 바라보는 방향(2D 탑다운 게임이지만 플레이어는 우측과 좌측만 바라보고 게임을 진행한다.)
    Vector3 m_vRightPos = new Vector3(1, 1, 1);
    Vector3 m_vLeftPos = new Vector3(-1, 1, 1);
    Vector3 m_vInputDir; // 키보드로부터 입력된 수평ㆍ수직 이동에따른 방향 벡터
    public bool m_bMove; // (m_bMove == true : 플레이어 이동 가능 / m_bMove == false : 플레이어 이동 불가능)
    float m_fMoveRate;   // 플레이어 이동 계수 (달리기 : m_fMoveRate = 1.0f / 구르기 : m_fMoveRate = 1.5f)

    // 기본 공격(연계 공격)
    public bool m_bAttack;    // (m_bAttack == true : 플레이어 공격 가능 / m_bAttack == false : 플레이어 공격 불가능)
    public bool m_bAttack1_1; // (m_bAttack1_1 == true : '기본 공격1' 가능)
    public bool m_bAttack1_2; // (m_bAttack1_2 == true : '기본 공격2' 가능)
    public bool m_bAttack1_3; // (m_bAttack1_3 == true : '기본 공격3' 가능)
    // 연계 공격 허용 지속 시간
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
    Coroutine m_cProcess_RollToIdle = null;    // 구르기 지속 시간 계산 코루틴
    float m_fCooltime_Roll = 3;                // 구르기 쿨타임
    
    // 피격
    Coroutine m_cProcess_Attacked;      // 플레이어 피격 계산 코루틴
    Coroutine m_cProcess_KnockBack;     // 플레이어 넉백 계산 코루틴
    float m_fAttackedToIdleTime = 0.3f; // 플레이어 피격 후 딜레이
    Coroutine m_cProcess_Power = null;  // 플레이어 피격 가능 시간 계산 코루틴
    public bool m_bPower;               // 플레이어 피격 가능 여부 (m_bPower == true : 플레이어 피격 불가능 / m_bPower == false : 플레이어 피격 가능)
                                        // m_bPower 변수는 '피격 후 피격 불가 상태 계산', '구르기 지속 중 피격 불가 상태 계산'에 사용된다.
                                        // 무적 상태가 되는것이 아니다. 스킬 및 디버프에 의한 해로운 효과는 계속해서 적용된다.

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
        m_bMove = true;

        m_bAttack = true;

        m_bAttack1_1 = true;
        m_bAttack1_2 = false;
        m_bAttack1_3 = false;

        m_bRoll = true;

        m_bPower = false;

        m_bGoaway = true;
        m_bGoaway_Success = false;
    }

    // 플레이어 이동 함수
    // Player_Total.cs에서 키입력(↑, ↓, ←, →)을 통해 함수 실행. 플레이어의 달리기 동작 수행
    // 플레이어 동작 FSM 정보를 반환
    public E_PLAYER_MOVE_STATE Move(int h, int v, int fspeed) // h : 수평 이동 값, v : 수직 이동 값, fspeed : 플레이어 이동 속도
    {
        if (m_bMove == true)
        {
            if (m_ePlayerMoveState == E_PLAYER_MOVE_STATE.IDLE || m_ePlayerMoveState == E_PLAYER_MOVE_STATE.RUN ||
               m_ePlayerMoveState == E_PLAYER_MOVE_STATE.ROLL || m_ePlayerMoveState == E_PLAYER_MOVE_STATE.GOAWAY)
            {
                SetScale(h, v); // 플레이어 동작 FSM 변경

                if (h == 0 && v == 0)
                    m_vInputDir = Vector3.zero;
                else if (h != 0 && v != 0)
                    m_vInputDir = new Vector3(h / 1.4f, v / 1.4f); // 플레이어의 대각선 이동 시 추가 보정값을 적용
                else
                    m_vInputDir = new Vector3(h, v);
                    
                if (m_ePlayerMoveState == E_PLAYER_MOVE_STATE.IDLE || m_ePlayerMoveState == E_PLAYER_MOVE_STATE.RUN) // 달리기 상태
                {
                    m_fMoveRate = 1f; // 기본 이동 계수
                    m_rRigdbody.MovePosition(this.gameObject.transform.position + (m_vInputDir * fspeed * 0.016f * 0.01f * m_fMoveRate));
                }
                if (m_ePlayerMoveState == E_PLAYER_MOVE_STATE.ROLL) // 구르기 상태
                {
                    m_fMoveRate = 1.5f; // 달리기보다 빠른 이동이 가능한 구르기 이동 계수
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
    // 플레이어의 이동 방향 반환
    public Vector3 Get_MoveDir()
    {
        return m_vInputDir;
    }
    // 플레이어 방향 설정, 플레이어 동작 FSM 변경 함수
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

        m_tTransform.localScale = m_vScale; // 플레이어가 바라보는 방향 설정(좌 / 우)
    }

    // 맵 변경 후 플레이어의 위치 설정(포탈로 이동 시)
    public void ChangeMap(Vector3 pos)
    {
        m_tTransform.position = pos;
    }

    // 기본 공격 함수
    // Player_Total.cs에서 키입력(A)을 통해 함수 실행. 플레이어의 공격 동작 수행
    // return 1: '기본 공격1' / return 2 : '기본 공격2' / return 3 : '기본 공격3' / return 0 : 공격 안됨
    public int Attack()
    {
        if (m_ePlayerMoveState == E_PLAYER_MOVE_STATE.IDLE || m_ePlayerMoveState == E_PLAYER_MOVE_STATE.RUN)
        {
            if (m_bAttack == true)
            {
                if (m_bAttack1_1 == true) // 플레이어 동작 FSM을 통해 '기본 공격1' 동작 수행
                {
                    m_ePlayerMoveState = SetPlayerMoveState(E_PLAYER_MOVE_STATE.ATTACK1_1);
                    return 1;
                }
                else if (m_bAttack1_2 == true) // 플레이어 동작 FSM을 통해 '기본 공격2' 동작 수행
                {
                    m_ePlayerMoveState = SetPlayerMoveState(E_PLAYER_MOVE_STATE.ATTACK1_2);
                    return 2;
                }
                else if (m_bAttack1_3 == true) // 플레이어 동작 FSM을 통해 '기본 공격3' 동작 수행
                {
                    m_ePlayerMoveState = SetPlayerMoveState(E_PLAYER_MOVE_STATE.ATTACK1_3);
                    return 3;
                }
            }
        }

        return 0;
    }

    // Player_Total.cs에서 호출되는 플레이어의 공격 속도를 알려주는 함수. 플레이어 공격 속도 계산에 사용된다.
    // ※ 추후 플레이어 공격 속도 계산을 매개변수를 이용한 방식으로 변경한다면 사용되지 않을 예정
    public void SetAttackSpeed(float atkspd) // atkspd : 플레이어 공격 속도
    {
          m_fAttackDelay_DurationTime = atkspd;
    }
    
    // '기본 공격1' 이후 다음 공격이 '기본 공격2' 로 연계될 수 있는지 계산하는 함수
    IEnumerator ProcessAttack1_1()
    {
        m_fAttack_DurationTime = m_fAttack1_1DurationTime; // 0.6초 동안 '기본 공격2' 가능
        m_bAttack1_1 = false;
        m_bAttack1_2 = true;
        m_bAttack1_3 = false;
        while (m_fAttack_DurationTime > 0)
        {
            m_fAttack_DurationTime -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        m_cProcess_Attack_Duration = null; // 0.6초 이후 다시 '기본 공격1' 가능
        m_bAttack1_1 = true;
        m_bAttack1_2 = false;
        m_bAttack1_3 = false;
    }
    // '기본 공격2' 이후 다음 공격이 '기본 공격3' 으로 연계될 수 있는지 계산하는 함수
    IEnumerator ProcessAttack1_2()
    {
        m_fAttack_DurationTime = m_fAttack1_2DurationTime; // 0.4초 동안 '기본 공격3' 가능
        m_bAttack1_1 = false;
        m_bAttack1_2 = false;
        m_bAttack1_3 = true;
        while (m_fAttack_DurationTime > 0)
        {
            m_fAttack_DurationTime -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        m_cProcess_Attack_Duration = null; // 0.4초 이후 다시 '기본 공격1' 가능
        m_bAttack1_1 = true;
        m_bAttack1_2 = false;
        m_bAttack1_3 = false;
    }
    // '기본 공격3' 이후 다음 공격은 '기본 공격1'. 현재 '기본 공격3' 다음의 연계 공격이 없기에 해당 부분이 주석처리 되어있다.
    IEnumerator ProcessAttack1_3()
    {
        //m_fAttack_DurationTime = m_fAttack1_3DurationTime;
        m_bAttack1_1 = true;
        m_bAttack1_2 = false;
        m_bAttack1_3 = false;
        //while (m_fAttack_DurationTime > 0)
        //{
        //    m_fAttack_DurationTime -= 0.1f;
        //    yield return new WaitForSeconds(0.1f);
        //}
        m_cProcess_Attack_Duration = null;
        m_bAttack1_1 = true;
        m_bAttack1_2 = false;
        m_bAttack1_3 = false;
    }
    
    // 공격 후 딜레이 계산 함수.
    // 기본 공격의 종류에 따라 각각 다른 딜레이를 가진다.('기본 공격1' : 0.4초 / '기본 공격2' : 0.4초 / '기본 공격3' : 0.6초)
    // 해당 딜레이 동안 플레이어는 구르기를 제외한 모든 능동적 동작을 수행할 수 없다.
    // 해당 딜레이 동안 수행 가능한 동작 : { ATTACKED(피격), DEATH(사망), ROLL(구르기) }
    // 해당 딜레이 동안 수행 불가능한 동작 : { IDLE(가만히 있기), RUN(달리기), ATTACK1_1(기본 공격1), ATTACK1_2(기본 공격2), ATTACK1_3(기본 공격3), GOAWAY(놓아주기), CONVERSATION(상호작용) }
    IEnumerator ProcessAttackToIdle(float ftime) // ftime : 공격 후 딜레이
    {
        m_bMove = false; 
        yield return new WaitForSeconds(ftime);
        m_ePlayerMoveState = SetPlayerMoveState(E_PLAYER_MOVE_STATE.IDLE);
        if (m_cProcess_KnockBack == null && m_cProcess_Attacked == null) // 만약 공격 후 딜레이가 종료될 시점에 플레이어가 넉백 및 피격된 상태일때는 해당하는 상태의 IEnumerator 함수에서 플레이어 움직임 관련 변수(m_bMove)가 처리된다.
            m_bMove = true;
        m_cProcess_AttackToIdle_Duration = null;
    }

    // 플레이어 공격 속도 계산 함수. 플레이어의 공격 속도에 따라 다음 기본 공격까지 기다려야하는 시간을 계산한다.
    IEnumerator ProcessAttackDelay()
    {
        m_bAttack = false; // 기본 공격 불가능
        float ftemp = m_fAttackDelay_DurationTime;
        while (ftemp > 0)
        {
            ftemp -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        m_bAttack = true; // 기본 공격 가능
        if (m_cProcess_AttackDelay_Duration != null)
            m_cProcess_AttackDelay_Duration = null;
    }

    // '기본 공격1' 동작 수행. 플레이어 동작 FSM에서 호출
    void Attack1_1()
    {
        m_cProcess_Attack_Duration = null;
        m_cProcess_Attack_Duration = StartCoroutine(ProcessAttack1_1()); // 연계 공격 가능 시간(0.6초) 계산
        m_cProcess_AttackToIdle_Duration = StartCoroutine(ProcessAttackToIdle(0.4f)); // 공격 후 딜레이(0.4초) 설정
    }
    // '기본 공격2' 동작 수행. 플레이어 동작 FSM에서 호출
    void Attack1_2()
    {
        m_cProcess_Attack_Duration = null;
        m_cProcess_Attack_Duration = StartCoroutine(ProcessAttack1_2()); // 연계 공격 가능 시간(0.4초) 계산
        m_cProcess_AttackToIdle_Duration = StartCoroutine(ProcessAttackToIdle(0.4f)); // 공격 후 딜레이(0.4초) 설정
    }
    // '기본 공격3' 동작 수행. 플레이어 동작 FSM에서 호출
    void Attack1_3()
    {
        m_cProcess_Attack_Duration = null;
        m_cProcess_Attack_Duration = StartCoroutine(ProcessAttack1_3()); // 연계 공격 가능 시간(0초) 계산
        m_cProcess_AttackToIdle_Duration = StartCoroutine(ProcessAttackToIdle(0.6f)); // 공격 후 딜레이(0.6초) 설정
    }

    // 플레이어 피격 함수
    // Monster_Total.cs에서 Player_Total.cs의 함수를 호출해 실행. 플레이어의 피격 동작 수행
    // return true : 피격됨 / return false : 피격안됨
    public bool Attacked(float time, float speed, Vector3 dir) // time : 넉백 지속 시간, speed : 플레이어 이동 속도, dir : 넉백 방향
    {
        if (m_bPower == false) // 플레이어 피격 가능할 때
        {
            m_bMove = false; // 플레이어 이동 불가

            if (m_cProcess_Goaway_Duration != null)
            {
                StopCoroutine(m_cProcess_Goaway_Duration); // 놓아주기 기능 취소
            }
            
            m_cProcess_Power = StartCoroutine(ProcessAttackedPower()); // 플레이어 피격 가능 시간 계산

            // 피격 전 플레이어 동작 FSM의 상태에 따라 피격 후 딜레이와 플레이어 피격 애니메이션이 실행된다.
            // 피격 애니메이션 실행 가능 상태 : { IDLE(가만히 있기), RUN(달리기), GOAWAY(놓아주기), CONVERSATION(상호작용) }
            // 피격 애니메이션 실행 불가능 상태 : { ATTACK1_1(기본 공격1), ATTACK1_2(기본 공격2), ATTACK1_3(기본 공격3), ATTACKED(피격), DEATH(사망), ROLL(구르기) }
            if (m_ePlayerMoveState == E_PLAYER_MOVE_STATE.IDLE || m_ePlayerMoveState == E_PLAYER_MOVE_STATE.RUN ||
                m_ePlayerMoveState == E_PLAYER_MOVE_STATE.GOAWAY || m_ePlayerMoveState == E_PLAYER_MOVE_STATE.CONVERSATION)
            {
                m_cProcess_Attacked = StartCoroutine(ProcessAttackedToIdle()); // 플레이어 피격 후 딜레이
                m_ePlayerMoveState = SetPlayerMoveState(E_PLAYER_MOVE_STATE.ATTACKED); // 애니메이션 설정
            }
            KnockBack(time, speed, dir); // 넉백

            return true;
        }
        return false;
    }
    // 플레이어 피격 가능 시간 계산 함수. 플레이어 피격 후 1초간 플레이어 피격 불가 상태가 지속된다.
    IEnumerator ProcessAttackedPower()
    {
        m_bPower = true;
        yield return new WaitForSeconds(1f);
        m_bPower = false;
        m_cProcess_Power = null;
    }
    // 플레이어 피격 후 딜레이 계산 함수
    IEnumerator ProcessAttackedToIdle()
    {
        m_bMove = false;
        yield return new WaitForSeconds(m_fAttackedToIdleTime); // 넉백이 지속되는 0.3초 동안 특정 동작을 제외하고 플레이어는 행동할 수 없다.
        m_ePlayerMoveState = SetPlayerMoveState(E_PLAYER_MOVE_STATE.IDLE);
        if (m_cProcess_KnockBack == null && m_cProcess_AttackToIdle_Duration == null) // 만약 피격 후 딜레이가 종료될 시점에 플레이어가 여전히 넉백된 상태일때는 해당하는 상태의 IEnumerator 함수에서 플레이어 움직임 관련 변수(m_bMove)가 처리된다.
            m_bMove = true;
        m_cProcess_Attacked = null;
    }
    
    // 넉백 함수. 플레이어 동작 FSM의 상태에 관계 없이 넉백 실행
    public void KnockBack(float time, float fspeed, Vector3 dir) // time : 넉백 지속 시간, speed : 플레이어 이동 속도, dir : 넉백 방향
    {
        if (m_cProcess_KnockBack == null) // 넉백 중첩 불가능
            m_cProcess_KnockBack = StartCoroutine(ProcessKnockBack(time, fspeed * 0.3f, dir));
    }
    // 넉백 계산 함수
    IEnumerator ProcessKnockBack(float time, float fspeed, Vector3 dir) // time : 넉백 지속 시간, speed : 플레이어 이동 속도, dir : 넉백 방향
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
        m_bMove = true; // 넉백 시간이 종료되면 공격 후 딜레이, 피격 후 딜레이는 취소되며 플레이어는 다른 동작을 수행할 수 있다.
        m_cProcess_KnockBack = null;
    }

    //
    // ※ 각종 딜레이의 종료 우선순위는 1. 구르기(딜레이 캔슬 기능) > 2. 넉백 중 딜레이 > 3. 피격 후 딜레이 < 4. 공격 후 딜레이 순이다. 우선순위가 높은 딜레이가 종료된다면 지속중인 하위 딜레이는 모두 취소된다.
    //
    
    // 사망 함수. 모든 코루틴 종료
    // Monster_Total.cs에서 Player_Total.cs의 함수를 호출해 실행. 플레이어의 사망 동작 수행
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
    } 

    // 리트라이(부활) 함수. 플레이어 상태 초기화
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

    // 플레이어 구르기
    // Player_Total.cs에서 키입력(S)을 통해 함수 실행. 플레이어의 구르기 동작 수행
    // 각종 딜레이 캔슬, 회피, 빠른 이동 기능
    // return true : 구르기 시전 성공 / return false : 구르기 시전 실패
    public bool Roll()
    {
        if (m_ePlayerMoveState == E_PLAYER_MOVE_STATE.IDLE || m_ePlayerMoveState == E_PLAYER_MOVE_STATE.RUN || 
            m_ePlayerMoveState == E_PLAYER_MOVE_STATE.ATTACK1_1 || m_ePlayerMoveState == E_PLAYER_MOVE_STATE.ATTACK1_2 ||
            m_ePlayerMoveState == E_PLAYER_MOVE_STATE.ATTACK1_3 || m_ePlayerMoveState == E_PLAYER_MOVE_STATE.GOAWAY)
        {
            if (m_bRoll == true)
            {
                // 공격 후 딜레이 캔슬
                if (m_cProcess_AttackToIdle_Duration != null)
                {
                    StopCoroutine(m_cProcess_AttackToIdle_Duration);
                    m_cProcess_AttackToIdle_Duration = null;
                }
                // 피격 후 딜레이 캔슬
                if (m_cProcess_Attacked != null)
                {
                    StopCoroutine(m_cProcess_Attacked);
                    m_cProcess_Attacked = null;
                }
                // 넉백 중 딜레이 캔슬
                if (m_cProcess_KnockBack != null)
                {
                    StopCoroutine(m_cProcess_KnockBack);
                    m_cProcess_KnockBack = null;
                }

                m_ePlayerMoveState = SetPlayerMoveState(E_PLAYER_MOVE_STATE.ROLL); // 플레이어 동작 FSM 변경

                if (m_cProcess_Power != null) // 구르기 이전에 이미 플레이어 피격 불가능 상태(m_bPower == true)라면 해당 코루틴은 종료하고 구르기로 인한 플레이어 피격 불가능 상태를 다시 적용한다. 
                {
                    StopCoroutine(m_cProcess_Power);
                    m_cProcess_Power = null;
                    m_bPower = false;
                }

                // 놓아주기 시전 취소
                if (m_cProcess_Goaway_Duration != null)
                {
                    StopCoroutine(m_cProcess_Goaway_Duration);
                    m_cProcess_Goaway_Duration = null;
                    m_bGoaway_Success = false;
                }

                m_bMove = true;

                m_cProcess_Roll_Cooltime = StartCoroutine(ProcessRoll_Cooltime()); // 구르기 쿨타임 계산
                m_cProcess_RollToIdle = StartCoroutine(ProcessRollToIdle());       // 구르기 지속 시간 계산

                return true;
            }
            return false;
        }
        return false;
    }
    IEnumerator ProcessRoll_Cooltime()
    {
        m_bRoll = false;
        yield return new WaitForSeconds(m_fCooltime_Roll); // 구르기 쿨타임 3초
        if (m_cProcess_Roll_Cooltime != null)
            m_cProcess_Roll_Cooltime = null;
        m_bRoll = true;
    }
    IEnumerator ProcessRollToIdle()
    {
        m_bPower = true;
        yield return new WaitForSeconds(0.7f); // 구르기 지속 시간 0.7초. 시전 중 플레이어 피격 불가능
        if (m_cProcess_RollToIdle != null)
            m_cProcess_RollToIdle = null;
        m_bPower = false;
        m_ePlayerMoveState = SetPlayerMoveState(E_PLAYER_MOVE_STATE.IDLE);
    }

    // 플레이어 놓아주기
    // Player_Total.cs에서 키입력(D)을 통해 함수 실행. 플레이어의 놓아주기 동작 수행
    // 놓아주기 시전 시간(3초) 동안 특정 동작을 제외한 동작 불가. 3초 이후 플레이어 주변의 몬스터 퇴치
    // 놓아주기 시전 시간 동안 수행 가능한 동작 : { ATTACKED(피격), DEATH(사망), ROLL(구르기) }
    // 놓아주기 시전 시간 동안 수행 불가능한 동작 : { IDLE(가만히 있기), RUN(달리기), ATTACK1_1(기본 공격1), ATTACK1_2(기본 공격2), ATTACK1_3(기본 공격3), GOAWAY(놓아주기), CONVERSATION(상호작용) }
    public void Goaway()
    {
        if (m_ePlayerMoveState == E_PLAYER_MOVE_STATE.IDLE || m_ePlayerMoveState == E_PLAYER_MOVE_STATE.RUN)
        {
            if (m_bGoaway == true)
            {
                m_bGoaway_Success = false;
                m_ePlayerMoveState = SetPlayerMoveState(E_PLAYER_MOVE_STATE.GOAWAY);
                m_cProcess_Goaway_Cooltime = StartCoroutine(ProcessGoaway_Cooltime()); // 놓아주기 쿨타임 계산
                m_cProcess_Goaway_Duration = StartCoroutine(ProcessGoawayToIdle());    // 놓아주기 시전 시간 계산
            }
        }
    }
    IEnumerator ProcessGoaway_Cooltime()
    {
        m_bGoaway = false;
        yield return new WaitForSeconds(m_fGoaway_Cooltime); // 놓아주기 쿨타임 10초
        if (m_cProcess_Goaway_Cooltime != null)
            m_cProcess_Goaway_Cooltime = null;
        m_bGoaway = true;
    }
    IEnumerator ProcessGoawayToIdle()
    {
        m_bMove = false;
        yield return new WaitForSeconds(m_fGoaway_Durationtime); // 놓아주기 시전 시간 3초.
        m_bMove = true;
        m_bGoaway_Success = true;
        if (m_cProcess_Goaway_Duration != null)
            m_cProcess_Goaway_Duration = null;
        m_ePlayerMoveState = SetPlayerMoveState(E_PLAYER_MOVE_STATE.IDLE);
        yield return null;
        m_bGoaway_Success = false;
    }
    // 놓아주기 취소
    public void Cancel_Goaway()
    {
        if (m_cProcess_Goaway_Duration != null)
        {
            StopCoroutine(m_cProcess_Goaway_Duration);
            m_ePlayerMoveState = SetPlayerMoveState(E_PLAYER_MOVE_STATE.IDLE);
            m_bMove = true;
        }
    }

    // 플레이어와 NPC간의 상호작용(대화, 퀘스트, 거래)
    // Player_Total.cs에서 키입력(SPACE)을 통해 함수 실행. 플레이어와 NPC간의 상호작용 동작 수행
    // return true : 상호작용 가능 / return false : 상호작용 불가능
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

    // 플레이어 장비아이템 변경 시 연계 공격 초기화 함수
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
    // 플레이어 착용 무기별 애니메이션 FSM 변경 함수
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
    // 애니메이션 관리
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

        // 플레이어 무기 변경 시 자연스러운 애니메이션 교체. 달리기를 하는 도중에 애니메이션이 바뀌면 오른발이 앞으로 나갈 차례인데 다시 왼발이 나가는등의 애니메이션이 부자연스러운 문제 해결
        if (m_ePlayerMoveState == E_PLAYER_MOVE_STATE.RUN)
        {
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
        }

        return pws;
    }

    // 플레이어 동작 FSM 변경 함수
    // 플레이어 동작 FSM의 상태는 무조건 아래 FSM 상태 변경 함수를 통해서 변경된다. 상태 변경에 따른 적절한 조치(함수 실행, 애니메이션 변경)가 이루어 진다.
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
                        // 플레이어 공격 속도 계산 초기화
                        if (m_cProcess_AttackDelay_Duration != null)
                            StopCoroutine(m_cProcess_AttackDelay_Duration);
                        // 플레이어 공격 속도 계산
                        m_cProcess_AttackDelay_Duration = StartCoroutine(ProcessAttackDelay());
                        // 연계 공격 가능 시간 초기화
                        if (m_cProcess_Attack_Duration != null)
                            StopCoroutine(m_cProcess_Attack_Duration);
                        SetAnimatorParameters("Attack1_1"); // 공격 판정에 관련된 처리는 해당 애니메이션의 특정 프레임에서 이벤트를 호출하여 처리
                        // 공격 후 딜레이 계산, 연계 공격 가능 시간 계산
                        Attack1_1();
                    }
                }
                break;
            case E_PLAYER_MOVE_STATE.ATTACK1_2:
                {
                    if (m_ePlayerMoveState != pms)
                    {
                        // 플레이어 공격 속도 계산 초기화
                        if (m_cProcess_AttackDelay_Duration != null)
                            StopCoroutine(m_cProcess_AttackDelay_Duration);
                        // 플레이어 공격 속도 계산
                        m_cProcess_AttackDelay_Duration = StartCoroutine(ProcessAttackDelay());
                        // 연계 공격 가능 시간 초기화
                        if (m_cProcess_Attack_Duration != null)
                            StopCoroutine(m_cProcess_Attack_Duration);
                        SetAnimatorParameters("Attack1_2"); // 공격 판정에 관련된 처리는 해당 애니메이션의 특정 프레임에서 이벤트를 호출하여 처리
                        // 공격 후 딜레이 계산, 연계 공격 가능 시간 계산
                        Attack1_2();
                    }
                }
                break;
            case E_PLAYER_MOVE_STATE.ATTACK1_3:
                {
                    if (m_ePlayerMoveState != pms)
                    {
                        // 플레이어 공격 속도 계산 초기화
                        if (m_cProcess_AttackDelay_Duration != null)
                            StopCoroutine(m_cProcess_AttackDelay_Duration);
                        // 플레이어 공격 속도 계산
                        m_cProcess_AttackDelay_Duration = StartCoroutine(ProcessAttackDelay());
                        // 연계 공격 가능 시간 초기화
                        if (m_cProcess_Attack_Duration != null)
                            StopCoroutine(m_cProcess_Attack_Duration);
                        SetAnimatorParameters("Attack1_3"); // 공격 판정에 관련된 처리는 해당 애니메이션의 특정 프레임에서 이벤트를 호출하여 처리
                        // 공격 후 딜레이 계산, 연계 공격 가능 시간 계산
                        Attack1_3();
                    }
                }
                break;
            case E_PLAYER_MOVE_STATE.ATTACKED:
                {
                    if (m_ePlayerMoveState != pms)
                    {
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
    // 애니메이션 관리
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
