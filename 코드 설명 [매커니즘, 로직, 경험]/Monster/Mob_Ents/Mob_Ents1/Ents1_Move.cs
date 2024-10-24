using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ents1_Move : Monster_Move // 기반이 되는 Monster_Move 클래스 상속
{
    private void Awake()
    {
        m_sSpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        m_sSpriteRenderer_Shadow = this.gameObject.transform.Find("Ents1_Shadow").GetComponent<SpriteRenderer>();
        m_tTransform = this.gameObject.GetComponent<Transform>();
        m_rRigdbody = this.gameObject.GetComponent<Rigidbody2D>();
        m_aAnimator = this.gameObject.GetComponent<Animator>();

        m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.IDLE);
        
        m_vRightPos = new Vector3(1, 1, 1);
        m_vLeftPos = new Vector3(-1, 1, 1);
        m_bFix = false;

        m_bAttack = true;
        m_fPeacefulTime = 10f; // CHASE 상태에서 IDLE 상태로 전환되는 시간 : 10초
        
        m_FadeinAlpa = 0;
        if (m_sSpriteRenderer != null)
        {
            m_fAlpa = m_sSpriteRenderer.color.a;
        } 
    }

    void Start()
    {
        Fadein(); // Fadein 효과 연출 함수
    }

    // 몬스터 이동 함수 - "짙은 앤트"는 매우 느린 속도로 이동한다.
    override public void Move(int speed, Vector3 dir) // speed : 몬스터 이동속도, dir : 몬스터 이동방향
    {
        SetDir(dir); // 몬스터 방향 설정 함수

        if (dir.x == 0 && dir.y == 0)
            m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.IDLE); // 몬스터 동작 FSM 변경 함수
        else
            m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.RUN); // 몬스터 동작 FSM 변경 함수

        if (m_eMonsterState == E_MONSTER_MOVE_STATE.RUN) // 몬스터 동작 FSM 상태 판단
            if (m_bFix == false)
                m_tTransform.position += (dir * speed * Time.deltaTime * 0.005f);
    }

    // 몬스터 추격 함수 - "짙은 앤트"는 매우 느린 속도로 오브젝트(플레이어)를 추격한다.
    override public void Chase(int speed, Vector3 dir) // speed : 몬스터 이동속도, dir : 몬스터 이동방향
    {
        SetDir(dir); // 몬스터 방향 설정 함수

        if (m_bFix == false)
            m_tTransform.position += (dir * speed * Time.deltaTime * 0.005f);
    }
    // 몬스터 추격 시간 계산 코루틴. 몬스터 동작 FSM : 추격(CHASE) -> 평화(IDLE) - 부모 클래스인 Monster_Move의 ProcessPeaceful() 코루틴을 사용한다.
    // virtual protected IEnumerator ProcessPeaceful() {ㆍㆍㆍ}

    // 몬스터 방향 설정 함수
    override public void SetDir(Vector3 dir) // dir : 몬스터 이동방향
    {
        if (dir.x >= 0)
            m_tTransform.localScale = m_vRightPos; // 몬스터가 바라보는 방향 오른쪽 전환
        else
            m_tTransform.localScale = m_vLeftPos; // 몬스터가 바라보는 방향 왼쪽 전환
    }

    // 몬스터 공격 함수 - "짙은 앤트"는 공격 시 지면 아래로 뿌리를 뻗어 일정 시간 경과 후 오브젝트(플레이어)의 위치로 뿌리를 돌출 시키는 공격을 한다.
    // return true : 몬스터 공격 성공 / return false : 몬스터 공격 실패
    override public bool Attack(float attackspeed) // attakcspeed : 몬스터 공격속도
    {
        if (m_bAttack == true) // 몬스터 공격 가능
            if (m_eMonsterState == E_MONSTER_MOVE_STATE.CHASE ||
                m_eMonsterState == E_MONSTER_MOVE_STATE.ATTACKED) // 몬스터 동작 FSM 상태 판단
            {
                m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.ATTACK, attackspeed); // 몬스터 동작 FSM 변경 함수
                return true;
            }
        return false;
    }
    // 몬스터 공격속도 계산 코루틴. 몬스터의 공격속도에 따라 다음 공격까지 기다려야하는 시간을 계산한다. - 부모 클래스인 Monster_Move의 ProcessAttack() 코루틴을 사용한다.
    // virtual protected IEnumerator ProcessAttack(float attackspeed) {ㆍㆍㆍ}
    // 몬스터 공격 종료 함수(가상 함수). 몬스터 공격 애니메이션의 특정 프레임에서 호출된다. - 부모 클래스인 Monster_Move의 EndAttack() 함수를 사용한다.
    // virtual protected void EndAttack()

    // 몬스터 피격 함수
    override public void Attacked()
    {
        if (m_eMonsterState == E_MONSTER_MOVE_STATE.IDLE || m_eMonsterState == E_MONSTER_MOVE_STATE.RUN ||
            m_eMonsterState == E_MONSTER_MOVE_STATE.CHASE || m_eMonsterState == E_MONSTER_MOVE_STATE.ATTACKED) // 몬스터 동작 FSM 상태 판단
            m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.ATTACKED); // 몬스터 동작 FSM 변경 함수
    }
    // 몬스터 피격 시간 계산 코루틴1 - 부모 클래스인 Monster_Move의 ProcessAttacked1() 코루틴을 사용한다.
    virtual protected IEnumerator ProcessAttacked1()
    // 몬스터 피격 시간 계산 코루틴2 - 부모 클래스인 Monster_Move의 ProcessAttacked2() 코루틴을 사용한다.
    virtual protected IEnumerator ProcessAttacked2()

    // 몬스터 사망 함수. Fadeout 효과 관련
    override public void Death()
    {
        m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.DEATH); // 몬스터 동작 FSM 변경 함수
    }
    // 몬스터 사망 시간 계산 코루틴. Fadeout 효과 관련 계산 - 부모 클래스인 Monster_Move의 ProcessDeath() 코루틴을 사용한다.
    // virtual public IEnumerator ProcessDeath() {ㆍㆍㆍ}

    // 몬스터 놓아주기 함수
    override public void Goaway()
    {
        if (m_eMonsterState == E_MONSTER_MOVE_STATE.IDLE || m_eMonsterState == E_MONSTER_MOVE_STATE.RUN) // 몬스터 동작 FSM 상태 판단
            m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.GOAWAY); // 몬스터 동작 FSM 변경 함수
    }
    // 몬스터 놓아주기 시간 계산 코루틴. Fadeout 효과 관련 계산 - 부모 클래스인 Monster_Move의 ProcessGoaway() 코루틴을 사용한다.
    // virtual public IEnumerator ProcessGoaway() {ㆍㆍㆍ}

    // 몬스터 리스폰 함수 - 부모 클래스인 Monster_Move의 Respone() 함수를 사용한다.
    // virtual public void Respone() {ㆍㆍㆍ}

    // Fadein 효과 연출 함수(가상 함수) - 부모 클래스인 Monster_Move의 Fadein() 함수를 사용한다.
    // virtual public void Fadein() {ㆍㆍㆍ}
    // Fadein 효과 계산 코루틴 - 부모 클래스인 Monster_Move의 ProcessFadein() 코루틴을 사용한다.
    // IEnumerator ProcessFadein() {ㆍㆍㆍ}

    // 몬스터 동작 FSM 변경 함수
    // 몬스터 동작 FSM의 상태는 무조건 아래 FSM 상태 변경 함수를 통해서 변경된다. 상태 변경에 따른 적절한 조치(함수 실행, 애니메이션 병경)가 이루어 진다.
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
                            m_eMonsterState == E_MONSTER_MOVE_STATE.GOAWAY) // 몬스터 동작 FSM 상태 판단
                            SetAnimationParameters("IDLE"); // 애니메이션 변경 : IDLE
                    }
                }
                break;
            case E_MONSTER_MOVE_STATE.RUN:
                {
                    if (m_eMonsterState != ms)
                    {
                        if (m_eMonsterState == E_MONSTER_MOVE_STATE.IDLE) // 몬스터 동작 FSM 상태 판단
                            SetAnimationParameters("RUN"); // 애니메이션 변경 : RUN
                    }
                }
                break;
            case E_MONSTER_MOVE_STATE.ATTACKED:
                {
                    if (m_eMonsterState == E_MONSTER_MOVE_STATE.IDLE || m_eMonsterState == E_MONSTER_MOVE_STATE.RUN ||
                        m_eMonsterState == E_MONSTER_MOVE_STATE.CHASE || m_eMonsterState == E_MONSTER_MOVE_STATE.ATTACK ||
                        m_eMonsterState == E_MONSTER_MOVE_STATE.ATTACKED) // 몬스터 동작 FSM 상태 판단
                    {
                        if (m_cProcessAttacked == null)
                            m_cProcessAttacked = StartCoroutine(ProcessAttacked1()); // 몬스터 피격 시간 계산 코루틴1
                        else // 몬스터가 이미 피격 중일 경우
                        {
                            StopCoroutine(m_cProcessAttacked);
                            m_cProcessAttacked = StartCoroutine(ProcessAttacked2()); // 몬스터 피격 시간 계산 코루틴2
                        }

                        if (m_cProcessPeaceful == null)
                            m_cProcessPeaceful = StartCoroutine(ProcessPeaceful()); // 몬스터 추격 시간 계산 코루틴
                        else // 몬스터가 이미 오브젝트(플레이어)를 추격하는 중일 경우
                        {
                            StopCoroutine(m_cProcessPeaceful);
                            m_cProcessPeaceful = StartCoroutine(ProcessPeaceful()); // 몬스터 추격 시간 계산 코루틴
                        }
                    }
                }
                break;
            case E_MONSTER_MOVE_STATE.ATTACK:
                {
                    if (m_eMonsterState != ms)
                    {
                        if (m_eMonsterState == E_MONSTER_MOVE_STATE.CHASE || m_eMonsterState == E_MONSTER_MOVE_STATE.ATTACKED) // 몬스터 동작 FSM 상태 판단
                        {
                            if (m_bAttack == true) // 몬스터 공격 가능
                            {
                                SetAnimationParameters("ATTACK"); // 애니메이션 변경 : ATTACK
                                StartCoroutine(ProcessAttack(attackspeed)); // 몬스터 공격속도 계산 코루틴
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
                            m_eMonsterState == E_MONSTER_MOVE_STATE.CHASE) // 몬스터 동작 FSM 상태 판단
                        {
                            SetAnimationParameters("DEATH"); // 애니메이션 변경 : DEATH
                            if (m_cProcessAttacked != null)
                                StopCoroutine(m_cProcessAttacked); // 몬스터 피격 시간 계산 코루틴 종료
                            StartCoroutine(ProcessDeath()); // 몬스터 사망 시간 계산 코루틴
                        }
                    }
                }
                break;
            case E_MONSTER_MOVE_STATE.GOAWAY:
                {
                    if (m_eMonsterState != ms)
                    {
                        if (m_eMonsterState == E_MONSTER_MOVE_STATE.IDLE || m_eMonsterState == E_MONSTER_MOVE_STATE.RUN) // 몬스터 동작 FSM 상태 판단
                        {
                            SetAnimationParameters("GOAWAY"); // 애니메이션 변경 : GOAWAY
                            StartCoroutine(ProcessGoaway()); // 몬스터 놓아주기 시간 계산 코루틴
                        }
                    }
                }
                break;
            case E_MONSTER_MOVE_STATE.CHASE:
                {
                    if (m_eMonsterState != ms)
                    {
                        if (m_eMonsterState == E_MONSTER_MOVE_STATE.ATTACKED || m_eMonsterState == E_MONSTER_MOVE_STATE.ATTACK) // 몬스터 동작 FSM 상태 판단
                            SetAnimationParameters("CHASE"); // 애니메이션 변경 : CHASE
                    }
                }
                break;
        }

        return ms;
    }
    
    // 애니메이션 관리
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
