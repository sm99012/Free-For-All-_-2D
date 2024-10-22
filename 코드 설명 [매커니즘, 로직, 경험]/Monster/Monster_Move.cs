using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // 이동
    public Vector3 m_vRightPos;    // m_vRightPos = new Vector3(1, 1, 1);
    public Vector3 m_vLeftPos;     // m_vLeftPos = new Vector3(-1, 1, 1);
    public bool m_bFix;            // 몬스터 고정 여부 (m_bFix == true : 고정형 몬스터(이동 불가) / m_bFix == false : 이동형 몬스터(이동 가능))

    // 공격
    public bool m_bAttack;          // 몬스터 공격 가능 여부 (m_bAttack == true : 몬스터 공격 가능 / m_bAttack == false : 몬스터 공격 불가능)
    public float m_fPeacefulTime;   // CHASE 상태에서 IDLE 상태로 전환되는 시간
    
    // 피격
    protected Coroutine m_cProcessAttacked; // 몬스터 피격 계산 코루틴
    public bool m_bPower;                          // 몬스터 피격 가능 여부 (m_bPower == true : 몬스터 피격 블가능 / m_bPower == false : 몬스터 피격 가능)

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
    virtual public void SetDir(Vector3 dir)
    {

    }
    
    // 몬스터 추격 함수(가상 함수)
    virtual public void Chase(int speed, Vector3 dir) // speed : 몬스터 이동속도, dir : 몬스터 이동방향
    {

    }
    // Animator AddEvent
    protected Coroutine m_cProcessPeaceful;
    virtual protected void EndAttack()
    {
        m_bAttack = false;
        if (m_cProcessPeaceful != null)
            m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.CHASE);
        else 
            m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.IDLE);
    }
    // CHASE 일정시간이 지난 이후 평화로움 상태로 변경
    virtual protected IEnumerator ProcessPeaceful()
    {
        yield return new WaitForSeconds(m_fPeacefulTime);
        m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.IDLE);
        m_cProcessPeaceful = null;
    }

    // 몬스터 공격 함수(가상 함수)
    virtual public bool Attack(float attackspeed)
    {
        return false;
    }
    virtual protected IEnumerator ProcessAttack(float attackspeed)
    {
        yield return new WaitForSeconds(attackspeed);
        m_bAttack = true;
    }
    
    // 몬스터 피격 함수(가상 함수)
    virtual public void Attacked()
    {

    }
    // ATTACKED 모션 실행
    virtual protected IEnumerator ProcessAttacked1()
    {
        SetAnimationParameters("ATTACKED");
        m_bFix = true;
        yield return new WaitForSeconds(0.6f); // 경직시간
        m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.CHASE);
        m_bFix = false;
        m_cProcessAttacked = null;
    }
    // ATTACKED 모션 중에 또맞으면 ATTACKED모션 재실행
    virtual protected IEnumerator ProcessAttacked2()
    {
        SetAnimationParameters("CHASE");
        yield return new WaitForSeconds(0.03f);
        SetAnimationParameters("ATTACKED");
        m_bFix = true;
        yield return new WaitForSeconds(0.6f); // 경직시간
        m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.CHASE);
        m_bFix = false;
        m_cProcessAttacked = null;
    }

    // 몬스터 사망 함수
    virtual public void Death()
    {
        StartCoroutine(ProcessDeath());
    }
    // Death
    virtual public IEnumerator ProcessDeath()
    {
        m_fAlpa = 1;
        m_fAlpa_Shadow = 0.6f;
        m_bFix = true;
        m_rRigdbody.bodyType = RigidbodyType2D.Static;
        this.gameObject.layer = LayerMask.NameToLayer("Default");
        while (m_fAlpa > 0)
        {
            m_sSpriteRenderer.color = new Color(m_Color_OriginalSprite.r, m_Color_OriginalSprite.g, m_Color_OriginalSprite.b, m_fAlpa);
            if (m_fAlpa_Shadow >= 0)
                if (m_sSpriteRenderer_Shadow != null)
                    m_sSpriteRenderer_Shadow.color = new Color(m_Color_OriginalSprite_Shadow.r, m_Color_OriginalSprite_Shadow.g, m_Color_OriginalSprite_Shadow.b, m_fAlpa_Shadow);

            m_fAlpa -= 0.006f;
            m_fAlpa_Shadow -= 0.006f;
            yield return new WaitForSeconds(0.01f);
        }
        m_sSpriteRenderer.color = new Color(m_Color_OriginalSprite.r, m_Color_OriginalSprite.g, m_Color_OriginalSprite.b, 0);
        if (m_sSpriteRenderer_Shadow != null)
            m_sSpriteRenderer_Shadow.color = new Color(m_Color_OriginalSprite_Shadow.r, m_Color_OriginalSprite_Shadow.g, m_Color_OriginalSprite_Shadow.b, 0);
        m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.IDLE);
    }

    // 몬스터 놓아주기 함수(가상 함수)
    virtual public void Goaway()
    {

    }
    virtual public IEnumerator ProcessGoaway()
    {
        m_fAlpa = 1;
        m_fAlpa_Shadow = 0.6f;
        m_bFix = true;
        m_rRigdbody.bodyType = RigidbodyType2D.Static;
        this.gameObject.layer = LayerMask.NameToLayer("Default");
        while (m_fAlpa > 0)
        {
            m_sSpriteRenderer.color = new Color(m_Color_OriginalSprite.r, m_Color_OriginalSprite.g, m_Color_OriginalSprite.b, m_fAlpa);
            if (m_fAlpa_Shadow >= 0)
                if (m_sSpriteRenderer_Shadow != null)
                    m_sSpriteRenderer_Shadow.color = new Color(m_Color_OriginalSprite_Shadow.r, m_Color_OriginalSprite_Shadow.g, m_Color_OriginalSprite_Shadow.b, m_fAlpa_Shadow);

            m_fAlpa -= 0.006f;
            m_fAlpa_Shadow -= 0.006f;
            yield return new WaitForSeconds(0.01f);
        }
        m_sSpriteRenderer.color = new Color(m_Color_OriginalSprite.r, m_Color_OriginalSprite.g, m_Color_OriginalSprite.b, 0);
        if (m_sSpriteRenderer_Shadow != null)
            m_sSpriteRenderer_Shadow.color = new Color(m_Color_OriginalSprite_Shadow.r, m_Color_OriginalSprite_Shadow.g, m_Color_OriginalSprite_Shadow.b, 0);
        m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.IDLE);
    }

    virtual public void Respone()
    {
        m_eMonsterState = SetMonsterMoveState(E_MONSTER_MOVE_STATE.IDLE);
        m_rRigdbody.bodyType = RigidbodyType2D.Dynamic;
        m_sSpriteRenderer.color = m_Color_OriginalSprite;
        m_FadeinAlpa = 0;
        m_bFix = false;
        this.gameObject.layer = LayerMask.NameToLayer("Monster");
        Fadein();   
    }

    // Fadein 효과 관련 함수(가상 함수)
    // 플레이어의 몬스터 제거(토벌 + 놓아주기)로 인해 제거된 몬스터의 리스폰 시 연출되는 Fadein 효과
    virtual public void Fadein()
    {
        StartCoroutine(ProcessFadein()); // Fadein 효과 계산 코루틴
    }
    // Fadein 효과 계산 코루틴
    IEnumerator ProcessFadein()
    {
        m_bPower = true;
        while (m_FadeinAlpa != 1)
        {
            m_sSpriteRenderer.color = new Color(m_sSpriteRenderer.color.r, m_sSpriteRenderer.color.g, m_sSpriteRenderer.color.b, m_FadeinAlpa);
            m_FadeinAlpa += 0.006f;
            if (m_FadeinAlpa < 0.6f)
                if (m_sSpriteRenderer_Shadow != null)
                    m_sSpriteRenderer_Shadow.color = new Color(m_sSpriteRenderer_Shadow.color.r, m_sSpriteRenderer_Shadow.color.g, m_sSpriteRenderer_Shadow.color.b, m_FadeinAlpa);

            if (m_FadeinAlpa > 1)
                m_FadeinAlpa = 1;
            yield return new WaitForSeconds(0.01f);
        }
        m_bAttack = true;
        m_bPower = false;

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
