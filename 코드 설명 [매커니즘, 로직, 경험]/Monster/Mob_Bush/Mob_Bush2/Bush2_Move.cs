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

    // 부모 클래스인 Bush1_Move의 Start() 함수를 사용한다.
    // void Start() {ㆍㆍㆍ}

    // 몬스터 이동 함수 - "가시덤불"은 이동하지 않는다.
    override public void Move(int speed, Vector3 dir) { }

    // 몬스터 방향 설정 - "가시덤불"은 이동 방향 설정을 하지 않는다.
    override public void SetDir(Vector3 dir) { }

    // 몬스터 추격 함수 - "가시덤불"은 추격하지 않는다.
    override public void Chase(int speed, Vector3 dir) { }
    // 몬스터 추격 시간 계산 코루틴 - "가시덤불"은 추격하지 않는다.
    override protected IEnumerator ProcessPeaceful() { }
    
    // 몬스터 공격 함수 - "가시덤불"은 공격하지 않는다.
    override public bool Attack(float attackspeed)
    {
        return false;
    }
    // 몬스터 공격속도 계산 코루틴 - "가시덤불"은 공격하지 않는다.
    override protected IEnumerator ProcessAttack(float attackspeed) { }
    // 몬스터 공격 종료 함수(가상 함수) - "가시덤불"은 공격하지 않는다.
    override protected void EndAttack() { }

    // 몬스터 피격 함수 - 부모 클래스인 Bush1_Move의 Attacked() 함수를 사용한다.
    // override public void Attacked() {ㆍㆍㆍ}
    // 몬스터 피격 시간 계산 코루틴1 - 부모 클래스인 Monster_Move의 ProcessAttacked1() 코루틴을 사용한다.
    // virtual protected IEnumerator ProcessAttacked1() {ㆍㆍㆍ}
    // 몬스터 피격 시간 계산 코루틴2 - 부모 클래스인 Monster_Move의 ProcessAttacked2() 코루틴을 사용한다.
    // virtual protected IEnumerator ProcessAttacked2() {ㆍㆍㆍ}

    // 몬스터 사망 함수. Fadeout 효과 관련 - 부모 클래스인 Bush1_Move의 Death() 함수를 사용한다.
    // override public void Death() {ㆍㆍㆍ}
    // 몬스터 사망 시간 계산 코루틴. Fadeout 효과 관련 계산 - 부모 클래스인 Monster_Move의 ProcessDeath() 코루틴을 사용한다.
    // virtual public IEnumerator ProcessDeath() {ㆍㆍㆍ}

    // 몬스터 놓아주기 함수 - "가시덤불"은 놓아주기가 불가능하다.
    virtual public void Goaway() { }
    // 몬스터 놓아주기 시간 계산 코루틴. Fadeout 효과 관련 계산 - "가시덤불"은 놓아주기가 불가능하다.
    // virtual public IEnumerator ProcessGoaway()

    // 몬스터 리스폰 함수 - 부모 클래스인 Bush1_Move의 Fadein() 함수를 사용한다.
    // override public void Respone() {ㆍㆍㆍ}

    // Fadein 효과 연출 함수 - 부모 클래스인 Monster_Move의 Fadein() 함수를 사용한다.
    // virtual public void Fadein() {ㆍㆍㆍ}

    // 몬스터 동작 FSM 변경 함수(가상 함수) - "훈련용 허수아비"는 몬스터 동작 FSM이 변경될 필요가 없다.
    override public E_MONSTER_MOVE_STATE SetMonsterMoveState(E_MONSTER_MOVE_STATE ms, float attackspeed = 0)
    {
        return ms;
    }

    // 애니메이션 관리(가상 함수) - "가시덤불"은 애니메이션이 존재하지 않는다.
    override public void SetAnimationParameters(string str) { }
}
