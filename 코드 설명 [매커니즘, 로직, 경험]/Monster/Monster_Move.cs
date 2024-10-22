using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// ※ 몬스터의 동작을 관리하는 Monster_Move 기반 클래스를 구현한 후 각종 몬스터의 ㆍㆍㆍ_Move 클래스를 상속으로 구현했다.
//    가상 함수를 이용해 몬스터의 특징에 따른 적절한 동작을 구현했다.
//

public class Monster_Move : MonoBehaviour
{
    public SpriteRenderer m_sSpriteRenderer;        // 몬스터 스프라이트 랜더러(이미지 + 색상 정보 등)
    public SpriteRenderer m_sSpriteRenderer_Shadow; // 몬스터 그림자 스프라이트 랜더러(이미지 + 색상 정보 등)
    protected Color m_Color_OriginalSprite;         // 몬스터 스프라이트 원본 색상(변동X)
    protected Color m_Color_OriginalSprite_Shadow;  // 몬스터 그림자 스프라이트 원본 색상(변동X)
    public Transform m_tTransform;
    public Animator m_aAnimator;
    public Rigidbody2D m_rRigdbody;

    // 몬스터 동작 FSM
    public enum E_MONSTER_MOVE_STATE { IDLE, RUN, ATTACK, ATTACKED, CHASE, DEATH, GOAWAY, ATTACK1, ATTACK2 }
    public E_MONSTER_MOVE_STATE m_eMonsterState; // 몬스터 동작 FSM 변수

    // 몬스터 이동 관련 변수
    public Vector3 m_vRightPos;    // m_vRightPos = new Vector3(1, 1, 1);
    public Vector3 m_vLeftPos;     // m_vLeftPos = new Vector3(-1, 1, 1);
    public bool m_bFix;            // 몬스터 고정 여부 (m_bFix == true : 몬스터 이동 불가 / m_bFix == false : 몬스터 이동 가능)

    // 몬스터 추격 관련 변수
    protected Coroutine m_cProcessPeaceful; // 몬스터 추격(CHASE) -> 평화(IDEL) 상태 변화 계산 코루틴
    
    // 몬스터 공격 관련 변수
    public bool m_bAttack;          // 몬스터 공격 가능 여부 (m_bAttack == true : 몬스터 공격 가능 / m_bAttack == false : 몬스터 공격 불가능)
    public float m_fPeacefulTime;   // CHASE 상태에서 IDLE 상태로 전환되는 시간
    
    // 몬스터 피격 관련 변수
    protected Coroutine m_cProcessAttacked; // 몬스터 피격 계산 코루틴
    public bool m_bPower;                   // 몬스터 피격 가능 여부 (m_bPower == true : 몬스터 피격 블가능 / m_bPower == false : 몬스터 피격 가능)

    // Fadein 효과 관련 변수
    public float m_FadeinAlpa;   // 몬스터 스프라이트 랜더러(이미지) 투명도
    // Fadeout 효과 관련 변수
    public float m_fAlpa;        // 몬스터 스프라이트 랜더러(이미지) 투명도
    public float m_fAlpa_Shadow; // 몬스터 그림자 스프라이트 랜더러(이미지) 투명도

    // 몬스터 이동 함수(가상 함수)
    virtual public void Move(int speed, Vector3 dir) // speed : 몬스터 이동속도, dir : 몬스터 이동방향
    {

    }
    
    // 몬스터 방향 설정(가상 함수)
    virtual public void SetDir(Vector3 dir) // dir : 방향
    {

    }
    
    // 몬스터 추격 함수(가상 함수)
    virtual public void Chase(int speed, Vector3 dir) // speed : 몬스터 이동속도, dir : 몬스터 이동방향
    {

    }
    // 몬스터 추격 시간 계산 코루틴. 몬스터 동작 FSM : 추격(CHASE) -> 평화(IDLE)
    virtual protected IEnumerator ProcessPeaceful()
    {
        yield return new WaitForSeconds(m_fPeacefulTime); // m_fPeacefulTime 만큼 몬스터는 대상(플레이어)을 추격한다.
        m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.IDLE); // 몬스터 동작 FSM : 추격(CHASE) -> 평화(IDLE)
        m_cProcessPeaceful = null;
    }

    // 몬스터 공격 함수(가상 함수)
    virtual public bool Attack(float attackspeed)
    {
        return false;
    }
    // 몬스터 공격속도 계산 코루틴. 몬스터의 공격속도에 따라 다음 공격까지 기다려야하는 시간을 계산한다.
    virtual protected IEnumerator ProcessAttack(float attackspeed)
    {
        yield return new WaitForSeconds(attackspeed);
        m_bAttack = true;
    }
    // 몬스터 공격 종료 함수(가상 함수). 몬스터 공격 애니메이션의 특정 프레임에서 호출된다. 몬스터 동작 FSM : 공격(ATTACK) -> 추격(CHASE) / 평화(IDLE)
    virtual protected void EndAttack()
    {
        m_bAttack = false;
        if (m_cProcessPeaceful != null) // 몬스터 공격이 종료된 후에도 여전히 몬스터가 대상(플레이어)을 추격해야하는 상태일 경우
            m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.CHASE); // 몬스터 동작 FSM : 공격(ATTACK) -> 추격(CHASE)
        else // 몬스터 공격이 종료된 후 몬스터가 평화로운 상태(추격X)일 경우
            m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.IDLE);  // 몬스터 동작 FSM : 공격(ATTACK) -> 평화(IDLE)
    }
    
    // 몬스터 피격 함수(가상 함수)
    virtual public void Attacked()
    {

    }
    // 몬스터 피격 시간 계산 코루틴1. 몬스터 피격 시 0.6f초의 경직 적용
    virtual protected IEnumerator ProcessAttacked1()
    {
        SetAnimationParameters("ATTACKED"); // 몬스터 피격 애니메이션 설정
        m_bFix = true; // 몬스터 이동 불가
        yield return new WaitForSeconds(0.6f);
        m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.CHASE); // 몬스터 피격 후 몬스터 추격 실행. 몬스터 동작 FSM : 피격(ATTACKED) -> 추격(CHASE)
        m_bFix = false; // 몬스터 이동 가능
        m_cProcessAttacked = null;
    }
    // 몬스터 피격 시간 계산 코루틴2. 몬스터 피격 중 또다시 피격 시 피격 판정 재실행(몬스터 동작 FSM의 상태를 변화해주는 추가 작업 필요)
    virtual protected IEnumerator ProcessAttacked2()
    {
        // 몬스터 동작 FSM의 상태를 변화해주는 추가 작업
        SetAnimationParameters("CHASE");
        yield return new WaitForSeconds(0.03f);
        
        SetAnimationParameters("ATTACKED"); // 몬스터 피격 애니메이션 설정
        m_bFix = true; // 몬스터 이동 불가
        yield return new WaitForSeconds(0.6f);
        m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.CHASE); // 몬스터 피격 후 몬스터 추격 실행. 몬스터 동작 FSM : 피격(ATTACKED) -> 추격(CHASE)
        m_bFix = false; // 몬스터 이동 가능
        m_cProcessAttacked = null;
    }

    // 몬스터 사망 함수
    virtual public void Death()
    {
        StartCoroutine(ProcessDeath()); // 몬스터 사망 시간 계산 코루틴
    }
    // 몬스터 사망 시간 계산 코루틴. Fadeout 효과 관련 계산
    virtual public IEnumerator ProcessDeath()
    {
        // 몬스터 설정 변경
        m_bFix = true; // 몬스터 이동 불가
        m_rRigdbody.bodyType = RigidbodyType2D.Static; // Rigidbody(강체).bodyType : Dynamic(다른 오브젝트에 의한 물리 적용 가능) -> Static(다른 오브젝트에 의한 물리 적용 불가능)
                                                       // 몬스터에게 다른 오브젝트(몬스터)에 의한 물리법칙을 적용 불가능하게 변경(다른 오브젝트에 의해 밀리는 현상 불가능)
        this.gameObject.layer = LayerMask.NameToLayer("Default"); // 몬스터의 레이어 변경 : "Monster" -> "Default"
                                                                  // 몬스터가 대상(플레이어)과 충돌 불가능 하도록 레이어 변경
                                                                  
        // 몬스터 스프라이트 랜더러(이미지) 투명도 감소(Fadeout 효과)
        m_fAlpa = 1;
        m_fAlpa_Shadow = 0.6f;
        while (m_fAlpa > 0)
        {
            m_sSpriteRenderer.color = new Color(m_Color_OriginalSprite.r, m_Color_OriginalSprite.g, m_Color_OriginalSprite.b, m_fAlpa);
            if (m_fAlpa_Shadow >= 0)
                if (m_sSpriteRenderer_Shadow != null) // 몬스터 그림자 스프라이트 랜더러가 존재하는 경우
                    m_sSpriteRenderer_Shadow.color = new Color(m_Color_OriginalSprite_Shadow.r, m_Color_OriginalSprite_Shadow.g, m_Color_OriginalSprite_Shadow.b, m_fAlpa_Shadow);

            m_fAlpa -= 0.006f;
            m_fAlpa_Shadow -= 0.006f;
            yield return new WaitForSeconds(0.01f);
        }
        m_sSpriteRenderer.color = new Color(m_Color_OriginalSprite.r, m_Color_OriginalSprite.g, m_Color_OriginalSprite.b, 0);
        if (m_sSpriteRenderer_Shadow != null) // 몬스터 그림자 스프라이트 랜더러가 존재하는 경우
            m_sSpriteRenderer_Shadow.color = new Color(m_Color_OriginalSprite_Shadow.r, m_Color_OriginalSprite_Shadow.g, m_Color_OriginalSprite_Shadow.b, 0);
        
        m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.IDLE); // 몬스터 동작 FSM : 사망(DEATH) -> 평화(IDLE)
    }

    // 몬스터 놓아주기 함수(가상 함수)
    virtual public void Goaway()
    {

    }
    // 몬스터 놓아주기 시간 계산 코루틴. Fadeout 효과 관련 계산
    virtual public IEnumerator ProcessGoaway()
    {
        // 몬스터 설정 변경
        m_bFix = true; // 몬스터 이동 불가
        m_rRigdbody.bodyType = RigidbodyType2D.Static; // Rigidbody(강체).bodyType : Dynamic(다른 오브젝트에 의한 물리 적용 가능) -> Static(다른 오브젝트에 의한 물리 적용 불가능)
												       // 몬스터에게 다른 오브젝트(몬스터)에 의한 물리법칙을 적용 불가능하게 변경(다른 오브젝트에 의해 밀리는 현상 불가능)
        this.gameObject.layer = LayerMask.NameToLayer("Default"); // 몬스터의 레이어 변경 : "Monster" -> "Default"
															      // 몬스터가 대상(플레이어)과 충돌 불가능 하도록 레이어 변경
                     
        // 몬스터 스프라이트 랜더러(이미지) 투명도 감소(Fadeout 효과)
        m_fAlpa = 1;
        m_fAlpa_Shadow = 0.6f;
        while (m_fAlpa > 0)
        {
            m_sSpriteRenderer.color = new Color(m_Color_OriginalSprite.r, m_Color_OriginalSprite.g, m_Color_OriginalSprite.b, m_fAlpa);
            if (m_fAlpa_Shadow >= 0)
                if (m_sSpriteRenderer_Shadow != null) // 몬스터 그림자 스프라이트 랜더러가 존재하는 경우
                    m_sSpriteRenderer_Shadow.color = new Color(m_Color_OriginalSprite_Shadow.r, m_Color_OriginalSprite_Shadow.g, m_Color_OriginalSprite_Shadow.b, m_fAlpa_Shadow);

            m_fAlpa -= 0.006f;
            m_fAlpa_Shadow -= 0.006f;
            yield return new WaitForSeconds(0.01f);
        }
        m_sSpriteRenderer.color = new Color(m_Color_OriginalSprite.r, m_Color_OriginalSprite.g, m_Color_OriginalSprite.b, 0);
        if (m_sSpriteRenderer_Shadow != null) // 몬스터 그림자 스프라이트 랜더러가 존재하는 경우
            m_sSpriteRenderer_Shadow.color = new Color(m_Color_OriginalSprite_Shadow.r, m_Color_OriginalSprite_Shadow.g, m_Color_OriginalSprite_Shadow.b, 0);
            
        m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.IDLE); // 몬스터 동작 FSM : 놓아주기(GOAWAY) -> 평화(IDLE)
    }

    // 몬스터 리스폰 함수
    virtual public void Respone()
    {
    	// 몬스터 설정 변경(초기화)
     	m_bFix = false; // 몬스터 이동 가능
        m_rRigdbody.bodyType = RigidbodyType2D.Dynamic; // Rigidbody(강체).bodyType : Dynamic(다른 오브젝트에 의한 물리 적용 가능)
                                                        // 몬스터에게 다른 오브젝트(몬스터)에 의한 물리법칙을 적용 가능하도록 변경(다른 오브젝트에 의해 밀리는 현상 가능)
        this.gameObject.layer = LayerMask.NameToLayer("Monster"); // 몬스터의 레이어 변경 : "Monster" -> "Default"
                                                                  // 몬스터가 대상(플레이어)과 충돌 불가능 하도록 레이어 변경
        m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.IDLE); // 몬스터 동작 FSM : 평화(IDLE)

 	// 몬스터 스프라이트 랜더러(색상) 변경(초기화)
        m_sSpriteRenderer.color = m_Color_OriginalSprite;

 	// Fadein 효과 연출
        m_FadeinAlpa = 0;
        Fadein();   
    }

    // Fadein 효과 연출 함수(가상 함수)
    // 플레이어의 몬스터 제거(토벌 + 놓아주기)로 인해 제거된 몬스터의 리스폰 시 연출되는 Fadein 효과
    virtual public void Fadein()
    {
        StartCoroutine(ProcessFadein()); // Fadein 효과 계산 코루틴
    }
    // Fadein 효과 계산 코루틴
    IEnumerator ProcessFadein()
    {
        m_bPower = true; // 몬스터 피격 불가능
        while (m_FadeinAlpa != 1)
        {
            m_sSpriteRenderer.color = new Color(m_sSpriteRenderer.color.r, m_sSpriteRenderer.color.g, m_sSpriteRenderer.color.b, m_FadeinAlpa);
            m_FadeinAlpa += 0.006f;
            if (m_FadeinAlpa < 0.6f)
                if (m_sSpriteRenderer_Shadow != null) // 몬스터 그림자 스프라이트 랜더러가 존재하는 경우
                    m_sSpriteRenderer_Shadow.color = new Color(m_sSpriteRenderer_Shadow.color.r, m_sSpriteRenderer_Shadow.color.g, m_sSpriteRenderer_Shadow.color.b, m_FadeinAlpa);

            if (m_FadeinAlpa > 1)
                m_FadeinAlpa = 1;
            yield return new WaitForSeconds(0.01f);
        }
        m_bAttack = true; // 몬스터 공격 가능
        m_bPower = false; // 몬스터 피격 가능

        m_Color_OriginalSprite = m_sSpriteRenderer.color;
        if (m_sSpriteRenderer_Shadow != null)
            m_Color_OriginalSprite_Shadow = m_sSpriteRenderer_Shadow.color;
    }

    // 몬스터 동작 FSM 변경 함수(가상 함수)
    // 몬스터 동작 FSM의 상태는 무조건 아래 FSM 상태 변경 함수를 통해서 변경된다. 상태 변경에 따른 적절한 조치(함수 실행, 애니메이션 병경)가 이루어 진다.
    virtual public E_MONSTER_MOVE_STATE SetMonsterMoveState(E_MONSTER_MOVE_STATE ms, float attackspeed = 0)
    {
        return ms;
    }
    // 애니메이션 관리(가상 함수)
    virtual public void SetAnimationParameters(string str)
    {

    }
}
