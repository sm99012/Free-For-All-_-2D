using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ※ "훈련용 허수아비"는 고정형 몬스터로 설계했다.

public class Dummy1_Total : Monster_Total // 기반이 되는 Monster_Total 클래스 상속
{
    private void Awake()
    {
        m_mm_Move = this.gameObject.GetComponent<Monster_Move>();
        m_ms_Status = this.gameObject.GetComponent<Monster_Status>();
        m_md_Drop = this.gameObject.GetComponent<Monster_Drop>();
        m_me_Effect = this.gameObject.GetComponent<Monster_Effect>();

        m_bRelation = false; // 몬스터 접촉 시 오브젝트(플레이어) 피격 불가능(몬스터 몸박뎀 존재하지 않음)
        
        m_bWait = false; // 다른 오브젝트와 상호작용 가능
        m_bSetTime = true; // 몬스터 이동 방향 설정 가능

        // 레이어 설정
        nLayer1 = 1 << LayerMask.NameToLayer("Player"); // 몬스터와 충돌 가능한 오브젝트(플레이어) 레이어

    }

    // Fadein 효과 연출과 함께 몬스터 리스폰 - 부모 클래스인 Monster_Total의 Start() 함수를 사용한다.
    // void Start()  {ㆍㆍㆍ}

    void Update()
    {
        if (m_bRelation == true && m_bWait == false) // 몬스터 접촉 시 오브젝트 피격 가능 && 다른 오브젝트와 상호작용 가능
        {
            BodyDamage(); // 몬스터 접촉 시 오브젝트(플레이어) 피격 판정 함수(몸박뎀 판정)
        }

        //AnimationTest();
    }

    // 몬스터 이동 함수 - "훈련용 허수아비"는 이동하지 않는다.
    override public void Move() { }
    
    // 몬스터 추격 함수 - "훈련용 허수아비"는 추격하지 않는다.
    override public void Chase() { }

    // 몬스터 이동 방향 설정 함수 - "훈련용 허수아비"는 이동 방향 설정을 하지 않는다.
    override public void SetDir() { }

    // 몬스터 탐지 함수 - "훈련용 허수아비"는 탐지하지 않는다.
    override public void Detect() { }

    // 몬스터 공격 함수 - "훈련용 허수아비"는 공격하지 않는다.
    override public bool Attack(float attackspeed) { }

    // 몬스터 공격 함수 - "훈련용 허수아비"는 공격하지 않는다.
    override public bool Attack(float attackspeed) { }

    // 몬스터 공격 판정 함수 - "훈련용 허수아비"는 공격하지 않기에 공격 판정이 필요 없다.
    override public void Attack_Check() { }

    // 몬스터 접촉 시 오브젝트(플레이어) 피격 판정 함수(몸박뎀 판정) - "훈련용 허수아비"는 오브젝트(플레이어) 접촉 판정이 없다.(몸박뎀이 존재하지 않는다.)
    virtual public void BodyDamage() { }

    // 몬스터 피격 함수
    override public bool Attacked(int dm, float dmrate, GameObject gm) // dm : 피격 데미지, dmrate : 피격 데미지 계수, gm : 몬스터 타격 대상(플레이어)
    {
        if (m_mm_Move.m_bPower == false) // 몬스터 피격 가능할 경우
        {
            if (m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.IDLE ||
                m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.RUN ||
                m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.CHASE ||
                m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.ATTACK ||
                m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.ATTACKED) // 몬스터 동작 FSM 상태 판단
            {
                m_gTarget = gm;

                if (m_ms_Status.Attacked(dm, dmrate) == true) // 몬스터 피격 시 스탯(능력치) 변동 함수
                {
                    Death(10); // 몬스터 사망 함수 + 리스폰 함수(리스폰까지 필요한 대기시간 : 10초)
                }
                else
                    m_mm_Move.Attacked(); // 몬스터 피격 함수

                return true;
            }
        }

        return false;
    }

    // 몬스터 사망 함수 + 리스폰 함수 - 부모 클래스인 Monster_Total의 Death() 함수를 사용한다.
    // virtual public void Death(float time) {ㆍㆍㆍ}
    
    // 몬스터 놓아주기 판정 함수 - "훈련용 허수아비"는 놓아주기가 불가능하다.
    override public SOC Goaway_Check()
    {
        return m_ms_Status.m_sSoc_null;
    }

    // 몬스터 리스폰 코루틴 - 부모 클래스인 Monster_Total의 ProcessRespone() 코루틴을 사용한다.
    // virtual public IEnumerator ProcessRespone(float time) {ㆍㆍㆍ}

    // 몬스터 리스폰 함수 - 부모 클래스인 Monster_Total의 Respone() 함수를 사용한다.
    // virtual public void Respone() {ㆍㆍㆍ}

    // Fadein 효과 연출 함수 - 부모 클래스인 Monster_Total의 Fadein() 함수를 사용한다.
    // virtual public void Fadein() {ㆍㆍㆍ}
}
