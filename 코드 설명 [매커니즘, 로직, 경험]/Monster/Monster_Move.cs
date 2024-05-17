using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Move : MonoBehaviour
{
    public SpriteRenderer m_sSpriteRenderer;
    public SpriteRenderer m_sSpriteRenderer_Shadow;
    protected Color m_Color_OriginalSprite;
    protected Color m_Color_OriginalSprite_Shadow;
    public Transform m_tTransform;
    public Vector3 m_vRightPos;
    public Vector3 m_vLeftPos;

    public Animator m_aAnimator;

    public Rigidbody2D m_rRigdbody;

    public bool m_bFix;
    public bool m_bAttack;

    public bool m_bPower;

    // 투명도
    public float m_fAlpa;
    public float m_fAlpa_Shadow;
    // CHASE 상태에서 IDLE 상태로 전환되는 시간
    public float m_fPeacefulTime;

    public float m_FadeinAlpa;

    protected float m_fAttackSpeed;

    public enum E_MONSTER_MOVE_STATE { IDLE, RUN, ATTACK, ATTACKED, CHASE, DEATH, GOAWAY,
                                        ATTACK1, ATTACK2 }
    public E_MONSTER_MOVE_STATE m_eMonsterState;

    void Start()
    {

    }

    
    void Update()
    {
        
    }

    virtual public void Move(int speed, Vector3 dir)
    {

    }

    virtual public void Chase(int speed, Vector3 dir)
    {

    }

    virtual public void SetDir(Vector3 dir)
    {

    }

    virtual public void Attacked()
    {

    }

    virtual public void Goaway()
    {

    }

    virtual public void Death()
    {
        StartCoroutine(ProcessDeath());
    }

    virtual public bool Attack(float attackspeed)
    {
        return false;
    }

    virtual public void Fadein()
    {
        StartCoroutine(ProcessFadein());
    }
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

    virtual public E_MONSTER_MOVE_STATE SetMonsterMoveState(E_MONSTER_MOVE_STATE ms)
    {
        return ms;
    }

    virtual public void SetAnimationParameters(string str)
    {

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

    // GOAWAY
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

    protected Coroutine m_cProcessAttacked = null;
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

    // ATTACK
    virtual protected IEnumerator ProcessAttack()
    {
        yield return new WaitForSeconds(m_fAttackSpeed);
        m_bAttack = true;
    }

    // Animator AddEvent
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
    protected Coroutine m_cProcessPeaceful;
}
