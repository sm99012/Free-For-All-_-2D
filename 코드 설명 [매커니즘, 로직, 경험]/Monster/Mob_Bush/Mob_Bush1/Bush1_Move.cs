using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush1_Move : Monster_Move // 기반이 되는 Monster_Move 클래스 상속
{
    private void Awake()
    {
        m_sSpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        m_sSpriteRenderer_Shadow = this.gameObject.transform.Find("Bush Shadow").GetComponent<SpriteRenderer>();
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

    void Start()
    {
        Fadein(); // Fadein 효과 연출 함수
    }

    // 몬스터 이동 함수 - "수풀"은 이동하지 않는다.
    // override public void Move(int speed, Vector3 dir) { }

    // 몬스터 방향 설정 - "수풀"은 이동 방향 설정을 하지 않는다.
    // override public void SetDir(Vector3 dir) { }

    // 몬스터 추격 함수 - "수풀"은 추격하지 않는다.
    // override public void Chase(int speed, Vector3 dir) { }
    // 몬스터 추격 시간 계산 코루틴 - "수풀"은 추격하지 않는다.
    // override protected IEnumerator ProcessPeaceful() { }
    
    // 몬스터 공격 함수
    override public bool Attack(float attackspeed)
    { 
        return false;
    }
    // override public bool Attack(float attackspeed) { }
    // 몬스터 공격속도 계산 코루틴 - "수풀"은 공격하지 않는다.
    // override protected IEnumerator ProcessAttack(float attackspeed) { }
    // 몬스터 공격 종료 함수(가상 함수) - "수풀"은 공격하지 않는다.
    // override protected void EndAttack() { }

    // 몬스터 피격 함수
    override public void Attacked()
    {
        if (m_eMonsterState == E_MONSTER_MOVE_STATE.IDLE || m_eMonsterState == E_MONSTER_MOVE_STATE.RUN ||
            m_eMonsterState == E_MONSTER_MOVE_STATE.CHASE || m_eMonsterState == E_MONSTER_MOVE_STATE.ATTACKED)
            m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.ATTACKED);
    }
    // 몬스터 피격 시간 계산 코루틴1 - 부모 클래스인 Monster_Move의 ProcessAttacked1() 코루틴을 사용한다.
    // virtual protected IEnumerator ProcessAttacked1()
    // 몬스터 피격 시간 계산 코루틴2 - 부모 클래스인 Monster_Move의 ProcessAttacked2() 코루틴을 사용한다.
    // virtual protected IEnumerator ProcessAttacked2()

    // 몬스터 사망 함수. Fadeout 효과 관련
    override public void Death()
    {
        m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.DEATH);
        StartCoroutine(ProcessDeath());
    }
    // 몬스터 사망 시간 계산 코루틴. Fadeout 효과 관련 계산 - 부모 클래스인 Monster_Move의 ProcessDeath() 코루틴을 사용한다.
    // virtual public IEnumerator ProcessDeath() { }

    // 몬스터 놓아주기 함수 - "수풀"은 놓아주기가 불가능하다.
    // virtual public void Goaway() { }
    // 몬스터 놓아주기 시간 계산 코루틴. Fadeout 효과 관련 계산 - "수풀"은 놓아주기가 불가능하다.
    // virtual public IEnumerator ProcessGoaway()

    // 몬스터 리스폰 함수
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

    // 몬스터 동작 FSM 변경 함수(가상 함수) - 부모 클래스인 Monster_Move의 SetMonsterMoveState() 함수를 사용한다.
    // virtual public E_MONSTER_MOVE_STATE SetMonsterMoveState(E_MONSTER_MOVE_STATE ms, float attackspeed = 0) { }
}
