using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// ※ "초원 슬라임(2)"은 일반적은 "초원 슬라임"과는 달리 "큰 초원 슬라임" 토벌 시에만 등장하는 몬스터이다.("큰 초원 슬라임" 토벌 시 여러 마리의 "초원 슬라임(2)"으로 분열한다.)
//    해당 몬스터는 "큰 초원 슬라임"을 토벌한 대상 오브젝트(플레이어)를 끊임없이(100초) 추격하며 공격한다.
//    "초원 슬라임"과 비교해 스탯(능력치, 평판), 크기, 생김새에 차이가 존재하지 않는다.
//

public class Slime1_2_Total : Slime1_Total // 기반이 되는 Slime1_Total 클래스 상속
{
    // 부모 클래스인 Slime1_Total의 Awake() 함수를 사용한다.
    // private void Awake() {ㆍㆍㆍ}

    // Fadein 효과 연출과 함께 몬스터 스폰. 이후 대상 오브젝트(플레이어) 추격
    private void Start()
    {
        Fadein();
        m_mm_Move.m_eMonsterState = m_mm_Move.SetMonsterMoveState(Monster_Move.E_MONSTER_MOVE_STATE.CHASE);
        m_mm_Move.SetAnimationParameters("CHASE");
    }

    // 부모 클래스인 Slime1_Total의 Update() 함수를 사용한다.
    // void Update() {ㆍㆍㆍ}

    // 몬스터 이동 함수 - 부모 클래스인 Slime1_Total의 Move() 함수를 사용한다.
    // override public void Move() {ㆍㆍㆍ}

    // 몬스터 추격 함수 - 부모 클래스인 Slime1_Total의 Chase() 함수를 사용한다.
    // override public  void Chase() {ㆍㆍㆍ}

    // 몬스터 이동 방향 설정 함수 - 부모 클래스인 Slime1_Total의 SetDir() 함수를 사용한다.
    // override public void SetDir() {ㆍㆍㆍ}
    // 몬스터 이동 시간 설정 관련 코루틴 - 부모 클래스인 Slime1_Total의 ProcessSetTime() 코루틴을 사용한다.
    // IEnumerator ProcessSetTime() {ㆍㆍㆍ}

    // 몬스터 탐지 함수 - 부모 클래스인 Slime1_Total의 Detect() 함수를 사용한다.
    // override public void Detect() {ㆍㆍㆍ}

    // 몬스터 공격 함수 - 부모 클래스인 Monster_Total의 Attack() 함수를 사용한다.
    // virtual public bool Attack(float attackspeed) {ㆍㆍㆍ}

    // 몬스터 공격 판정 함수 - 부모 클래스인 Slime1_Total의 Attack_Check() 함수를 사용한다.
    // override public void Attack_Check() {ㆍㆍㆍ}

    // 몬스터 접촉 시 오브젝트(플레이어) 피격 판정 함수(몸박뎀 판정) - 부모 클래스인 Slime1_Total의 BodyDamage() 함수를 사용한다.
    // override public void BodyDamage() { } {ㆍㆍㆍ}

    // 몬스터 피격 함수 - 부모 클래스인 Slime1_Total의 Attacked() 함수를 사용한다.
    // override public bool Attacked() {ㆍㆍㆍ}
    
    // 몬스터 사망 함수. 리스폰 하지 않는다.
    override public void Death(float time)
    {
        Death(); // 몬스터 사망 함수 _ 일회용 오브젝트 사망(제거). 튜토리얼 전용, 특수 전용
    }

    // 몬스터 놓아주기 판정 함수
    override public SOC Goaway()
    {
        if (m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.IDLE || m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.RUN) // 몬스터 동작 FSM 상태 판단
        {
            m_bWait = true; // 다른 오브젝트와 상호작용 불가능
            m_ms_Status.Goaway(); // 몬스터 놓아주기 판정 함수
            m_mm_Move.Goaway(); // 몬스터 놓아주기 판정 함수
            // m_md_Drop.DropItem_Goaway(m_ms_Status.m_nMonsterCode, this.gameObject.transform.position); // 몬스터 놓아주기로 인한 아이템 드롭 제한
            m_me_Effect.Effect_Goaway(this.transform.position); // 몬스터 놓아주기 이펙트 연출 함수

            // StartCoroutine(ProcessRespone(10)); // 몬스터 리스폰 제한
            
            return m_ms_Status.m_sSoc_Goaway;
        }

        return m_ms_Status.m_sSoc_null;
    }

    // 몬스터 리스폰 코루틴 - 부모 클래스인 Monster_Total의 ProcessRespone() 코루틴을 사용한다.
    // virtual public IEnumerator ProcessRespone(float time) {ㆍㆍㆍ}

    // 몬스터 리스폰 함수 - 부모 클래스인 Monster_Total의 Respone() 함수를 사용한다.
    // virtual public void Respone() {ㆍㆍㆍ}

    // Fadein 효과 연출 함수 - 부모 클래스인 Monster_Total의 Fadein() 함수를 사용한다.
    // virtual public void Fadein() {ㆍㆍㆍ}
}
