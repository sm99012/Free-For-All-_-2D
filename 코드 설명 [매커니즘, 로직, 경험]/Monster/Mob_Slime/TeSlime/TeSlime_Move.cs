using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeSlime_Move : MonoBehaviour
{
    [SerializeField] SpriteRenderer m_SpriteRenderer;
    Transform m_Transform;
    protected Color m_Color_OriginalSprite;
    public float m_FadeinAlpa;

    Vector3 m_vRightPos;
    Vector3 m_vLeftPos;

    [SerializeField]
    Animator m_Animator;

    Rigidbody2D m_Rigidbody;

    // 이동 가능?
    public bool m_bFix;
    // 공격 가능?
    public bool m_bAttack;
    // 피격 애니메이션 가능?
    public bool m_bAnimationPossible_Attacked;
    // 무적?
    public bool m_bPower;

    bool m_bFocus;
    public bool m_bAttack1_1;
    public bool m_bAttack1_2;
    public bool m_bAttack1_3;

    // 투명도.
    float m_fAlpha;
    float m_fFadeInAlpha;

    // Animation 지속시간.
    float m_fAnimationDurationTime_Focus;
    float m_fAnimationDurationTime_Attack1_1;
    float m_fAnimationDurationTime_Attack1_2;
    float m_fAnimationDurationTime_Attack1_3;
    float m_fAnimationDurationTime_Attacked;
    // 쿨타임.
    float m_fCoolTime_Focus;
    float m_fCoolTime_Attack1_2;
    float m_fCoolTime_Attack1_3;

    // Player Direction.
    public Vector3 m_vDir;

    public enum E_TESLIME_MOVE_STATE { EYE, CHASE, FOCUS, ATTACK1_1, ATTACK1_2, ATTACK1_3, ATTACKED, DEATH }

    public E_TESLIME_MOVE_STATE m_eTeSlimeState;

    public void InitialSet()
    {
        m_bAttack = true;
        m_bAnimationPossible_Attacked = true;
        m_bFocus = true;
        m_bAttack1_1 = true;
        m_bAttack1_2 = true;
        m_bAttack1_3 = true;

        m_Rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        m_SpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        m_FadeinAlpa = 0;

        Set_Phase1();
    }
    public void Set_Phase1()
    {
        m_fAnimationDurationTime_Focus = 1.0f;
        m_fAnimationDurationTime_Attack1_1 = 1.0f;
        m_fAnimationDurationTime_Attack1_2 = 3f;
        m_fAnimationDurationTime_Attack1_3 = 1.0f;
        m_fAnimationDurationTime_Attacked = 0.7f;

        m_fCoolTime_Focus = 60f;
        m_fCoolTime_Attack1_2 = 10f;
        m_fCoolTime_Attack1_3 = 10f;
    }
    public void Set_Phase2()
    {
        m_fAnimationDurationTime_Focus = 1.0f;
        m_fAnimationDurationTime_Attack1_1 = 1.0f;
        m_fAnimationDurationTime_Attack1_2 = 2f;
        m_fAnimationDurationTime_Attack1_3 = 1.0f;
        m_fAnimationDurationTime_Attacked = 0.7f;

        m_fCoolTime_Focus = 60f;
        m_fCoolTime_Attack1_2 = 5f;
        m_fCoolTime_Attack1_3 = 5f;
    }
    public void Set_Phase3()
    {
        m_fAnimationDurationTime_Focus = 1.0f;
        m_fAnimationDurationTime_Attack1_1 = 1.0f;
        m_fAnimationDurationTime_Attack1_2 = 1.2f;
        m_fAnimationDurationTime_Attack1_3 = 1.0f;
        m_fAnimationDurationTime_Attacked = 0.7f;

        m_fCoolTime_Focus = 60f;
        m_fCoolTime_Attack1_2 = 2f;
        m_fCoolTime_Attack1_3 = 1.2f;
    }

    public void SetDir(Vector3 targetpos)
    {
        m_vDir = Vector3.Normalize(targetpos - this.transform.position);

        if (m_vDir.x >= 0)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void Awake()
    {
        m_Animator = this.GetComponent<Animator>();
    }

    public void Eye()
    {
        if (m_bFocus == true && m_bAttack1_1 == true && m_bAttack1_2 == true && m_bAttack1_3 == true)
            m_eTeSlimeState = SetTeSlimeMoveState(E_TESLIME_MOVE_STATE.EYE);
    }

    public void Chase(float speed)
    {
        if (m_bFocus == true && m_bAttack1_1 == true && m_bAttack1_2 == true && m_bAttack1_3 == true)
        {
            m_eTeSlimeState = SetTeSlimeMoveState(E_TESLIME_MOVE_STATE.CHASE);

            if (m_bFix == false)
                m_Rigidbody.MovePosition(this.transform.position + (m_vDir * speed * Time.deltaTime * 0.01f));
        }
    }

    public bool Focus()
    {
        if (m_cProcess_Focus == null)
        if (m_bFocus == true && m_bAttack1_1 == true && m_bAttack1_2 == true && m_bAttack1_3 == true)
        {
            m_bFocus = false;
            m_eTeSlimeState = SetTeSlimeMoveState(E_TESLIME_MOVE_STATE.FOCUS);
            m_cProcess_Focus = StartCoroutine(ProcessFocus(m_fCoolTime_Focus));

            return true;
        }
        return false;
    }
    Coroutine m_cProcess_Focus;
    float m_fFocus_RandomDurationTime;
    IEnumerator ProcessFocus(float time)
    {
        m_fFocus_RandomDurationTime = Random.Range(1, 20);
        Debug.Log(m_fFocus_RandomDurationTime);
        m_bAttack = false;
        m_bAnimationPossible_Attacked = false;
        if (time > m_fFocus_RandomDurationTime)
        {
            while (time >= 0)
            {
                yield return null;
                time -= Time.deltaTime;
                m_fFocus_RandomDurationTime -= Time.deltaTime;

                if (m_fFocus_RandomDurationTime < 0)
                {
                    m_bAttack = true;
                    m_bFocus = true;
                    Eye();
                }
            }
        }
        else
        {
            while (m_fFocus_RandomDurationTime >= 0)
            {
                yield return null;
                time -= Time.deltaTime;
                m_fFocus_RandomDurationTime -= Time.deltaTime;
            }
            m_bAttack = true;
            m_bFocus = true;
            Eye();
        }
        m_cProcess_Focus = null;
        m_bAnimationPossible_Attacked = true;
    }


    public bool Attack1_1(float speed)
    {
        if (m_cProcess_Attack1_1 == null)
        if (m_bFocus == true && m_bAttack1_1 == true && m_bAttack1_2 == true && m_bAttack1_3 == true)
        {
            m_bAttack = false;
            m_bAttack1_1 = false;
            m_eTeSlimeState = SetTeSlimeMoveState(E_TESLIME_MOVE_STATE.ATTACK1_1);
            m_cProcess_Attack1_1 = StartCoroutine(ProcessAttack1_1(speed, m_fAnimationDurationTime_Attack1_1));
            return true;
        }
        return false;
    }
    Coroutine m_cProcess_Attack1_1;
    IEnumerator ProcessAttack1_1(float speed, float durationtime)
    {
        bool m_bCheck_Animation = true;
        m_bAnimationPossible_Attacked = false;
        while (speed >= 0)
        {
            yield return null;
            speed -= Time.deltaTime;
            //Debug.Log(speed);
            if (durationtime > 0)
            {
                durationtime -= Time.deltaTime;
            }
            else
            {
                if (m_bCheck_Animation == true)
                {
                    m_bCheck_Animation = false;
                    m_bAttack1_1 = true;
                    if (m_bFocus == true && m_bAttack1_1 == true && m_bAttack1_2 == true && m_bAttack1_3 == true)
                        Eye();
                }
            }
        }
        m_cProcess_Attack1_1 = null;
        m_bAttack = true;
        m_bAnimationPossible_Attacked = true;
    }

    public bool Check_Attack1_2()
    {
        if (m_cProcess_Attack1_2 == null)
            if (m_bFocus == true && m_bAttack1_1 == true && m_bAttack1_2 == true && m_bAttack1_3 == true)
                return true;

        return false;
    }

    public bool Attack1_2()
    {
        if (m_cProcess_Attack1_2 == null)
        if (m_bFocus == true && m_bAttack1_1 == true && m_bAttack1_2 == true && m_bAttack1_3 == true)
        {
            m_bAttack = false;
            m_bAttack1_2 = false;
            m_eTeSlimeState = SetTeSlimeMoveState(E_TESLIME_MOVE_STATE.ATTACK1_2);
            m_cProcess_Attack1_2 = StartCoroutine(ProcessAttack1_2(m_fCoolTime_Attack1_2, m_fAnimationDurationTime_Attack1_2));
            return true;
        }
        return false;
    }
    Coroutine m_cProcess_Attack1_2;
    IEnumerator ProcessAttack1_2(float time, float durationtime)
    {
        bool m_bCheck_Animation = true;
        m_bAnimationPossible_Attacked = false;
        while (time >= 0)
        {
            yield return null;
            time -= Time.deltaTime;
            if (durationtime > 0)
            {
                durationtime -= Time.deltaTime;
            }
            else
            {
                if (m_bCheck_Animation == true)
                {
                    m_bCheck_Animation = false;
                    m_bAttack = true;
                    m_bAttack1_2 = true;
                    m_bAnimationPossible_Attacked = true;
                    if (m_bFocus == true && m_bAttack1_1 == true && m_bAttack1_2 == true && m_bAttack1_3 == true)
                        Eye();
                }
            }
        }
        m_cProcess_Attack1_2 = null;
    }

    public bool Check_Attack1_3()
    {
        if (m_cProcess_Attack1_3 == null)
            if (m_bFocus == true && m_bAttack1_1 == true && m_bAttack1_2 == true && m_bAttack1_3 == true)
                return true;

        return false;
    }

    public bool Attack1_3()
    {
        if (m_cProcess_Attack1_3 == null)
        if (m_bFocus == true && m_bAttack1_1 == true && m_bAttack1_2 == true && m_bAttack1_3 == true)
        {
            m_bAttack = false;
            m_bAttack1_3 = false;
            m_eTeSlimeState = SetTeSlimeMoveState(E_TESLIME_MOVE_STATE.ATTACK1_3);
            m_cProcess_Attack1_3 = StartCoroutine(ProcessAttack1_3(m_fCoolTime_Attack1_3, m_fAnimationDurationTime_Attack1_3));
            return true;
        }
        return false;
    }
    Coroutine m_cProcess_Attack1_3;
    IEnumerator ProcessAttack1_3(float time, float durationtime)
    {
        bool m_bCheck_Animation = true;
        m_bAnimationPossible_Attacked = false;
        while (time >= 0)
        {
            yield return null;
            time -= Time.deltaTime;
            if (durationtime > 0)
            {
                durationtime -= Time.deltaTime;
            }
            else
            {
                if (m_bCheck_Animation == true)
                {
                    m_bCheck_Animation = false;
                    m_bAttack = true;
                    m_bAttack1_3 = true;
                    m_bAnimationPossible_Attacked = true;
                    if (m_bFocus == true && m_bAttack1_1 == true && m_bAttack1_2 == true && m_bAttack1_3 == true)
                        Eye();
                }
            }
        }
        m_cProcess_Attack1_3 = null;
    }

    public void Attacked()
    {
        if (m_bFocus == false && m_bAnimationPossible_Attacked == true && m_cProcess_Attacked == null)
        {
            m_eTeSlimeState = SetTeSlimeMoveState(E_TESLIME_MOVE_STATE.ATTACKED);
            m_cProcess_Attacked = StartCoroutine(ProcessAttacked());
        }
    }
    Coroutine m_cProcess_Attacked;
    IEnumerator ProcessAttacked()
    {
        m_bFix = true;
        yield return new WaitForSeconds(m_fAnimationDurationTime_Attacked); // 경직시간
        Eye();
        m_bFix = false;
        m_cProcess_Attacked = null;
    }

    public void Death()
    {
        StopAllCoroutines();
        m_eTeSlimeState = SetTeSlimeMoveState(E_TESLIME_MOVE_STATE.DEATH);
    }

    public void Fadein()
    {
        StartCoroutine(ProcessFadein());
    }
    IEnumerator ProcessFadein()
    {
        m_eTeSlimeState = E_TESLIME_MOVE_STATE.FOCUS;
        m_bPower = true;
        while (m_FadeinAlpa < 1)
        {
            m_SpriteRenderer.color = new Color(m_SpriteRenderer.color.r, m_SpriteRenderer.color.g, m_SpriteRenderer.color.b, m_FadeinAlpa);
            m_FadeinAlpa += 0.006f;
            yield return new WaitForSeconds(0.01f);
        }
        m_SpriteRenderer.color = new Color(m_SpriteRenderer.color.r, m_SpriteRenderer.color.g, m_SpriteRenderer.color.b, 1);
        m_FadeinAlpa = 1;
        m_bAttack = true;
        m_bPower = false;

        m_Color_OriginalSprite = m_SpriteRenderer.color;
        m_eTeSlimeState = E_TESLIME_MOVE_STATE.EYE;
    }
    public void Fadeout()
    {
        StartCoroutine(ProcessFadeout());
    }
    IEnumerator ProcessFadeout()
    {
        m_eTeSlimeState = E_TESLIME_MOVE_STATE.FOCUS;
        m_bPower = true;
        while (m_FadeinAlpa > 0)
        {
            m_SpriteRenderer.color = new Color(m_SpriteRenderer.color.r, m_SpriteRenderer.color.g, m_SpriteRenderer.color.b, m_FadeinAlpa);
            m_FadeinAlpa -= 0.006f;
            yield return new WaitForSeconds(0.01f);
        }
        m_SpriteRenderer.color = new Color(m_SpriteRenderer.color.r, m_SpriteRenderer.color.g, m_SpriteRenderer.color.b, 0);
        m_FadeinAlpa = 0;
        m_bAttack = true;
        m_bPower = false;

        Destroy(this.gameObject);

        m_Color_OriginalSprite = m_SpriteRenderer.color;
        m_eTeSlimeState = E_TESLIME_MOVE_STATE.EYE;
    }

    // FSM.
    E_TESLIME_MOVE_STATE SetTeSlimeMoveState(E_TESLIME_MOVE_STATE ms)
    {
        switch(ms)
        {
            case E_TESLIME_MOVE_STATE.EYE:
                {
                        SetAnimationParameters("EYE");
                }
                break;
            case E_TESLIME_MOVE_STATE.CHASE:
                {
                        SetAnimationParameters("CHASE");
                }
                break;
            case E_TESLIME_MOVE_STATE.FOCUS:
                {
                        SetAnimationParameters("FOCUS");
                }
                break;
            case E_TESLIME_MOVE_STATE.ATTACK1_1:
                {
                        SetAnimationParameters("ATTACK1_1");
                }
                break;
            case E_TESLIME_MOVE_STATE.ATTACK1_2:
                {
                        SetAnimationParameters("ATTACK1_2");
                }
                break;
            case E_TESLIME_MOVE_STATE.ATTACK1_3:
                {
                        SetAnimationParameters("ATTACK1_3");
                }
                break;
            case E_TESLIME_MOVE_STATE.ATTACKED:
                {
                        SetAnimationParameters("ATTACKED");
                }
                break;
            case E_TESLIME_MOVE_STATE.DEATH:
                {
                        SetAnimationParameters("DEATH");
                }
                break;
        }

        return ms;
    }

    // Animation.
    void SetAnimationParameters(string str)
    {
        switch(str)
        {
            case "EYE":
                {
                    m_Animator.SetBool("EYE", true);
                    m_Animator.SetBool("CHASE", false);
                    m_Animator.SetBool("FOCUS", false);
                    m_Animator.SetBool("ATTACK1_1", false);
                    m_Animator.SetBool("ATTACK1_2", false);
                    m_Animator.SetBool("ATTACK1_3", false);
                    m_Animator.SetBool("ATTACKED", false);
                    m_Animator.SetBool("DEATH", false);
                }
                break;
            case "CHASE":
                {
                    m_Animator.SetBool("EYE", false);
                    m_Animator.SetBool("CHASE", true);
                    m_Animator.SetBool("FOCUS", false);
                    m_Animator.SetBool("ATTACK1_1", false);
                    m_Animator.SetBool("ATTACK1_2", false);
                    m_Animator.SetBool("ATTACK1_3", false);
                    m_Animator.SetBool("ATTACKED", false);
                    m_Animator.SetBool("DEATH", false);
                }
                break;
            case "FOCUS":
                {
                    m_Animator.SetBool("EYE", false);
                    m_Animator.SetBool("CHASE", false);
                    m_Animator.SetBool("FOCUS", true);
                    m_Animator.SetBool("ATTACK1_1", false);
                    m_Animator.SetBool("ATTACK1_2", false);
                    m_Animator.SetBool("ATTACK1_3", false);
                    m_Animator.SetBool("ATTACKED", false);
                    m_Animator.SetBool("DEATH", false);
                }
                break;
            case "ATTACK1_1":
                {
                    m_Animator.SetBool("EYE", false);
                    m_Animator.SetBool("CHASE", false);
                    m_Animator.SetBool("FOCUS", false);
                    m_Animator.SetBool("ATTACK1_1", true);
                    m_Animator.SetBool("ATTACK1_2", false);
                    m_Animator.SetBool("ATTACK1_3", false);
                    m_Animator.SetBool("ATTACKED", false);
                    m_Animator.SetBool("DEATH", false);
                }
                break;
            case "ATTACK1_2":
                {
                    m_Animator.SetBool("EYE", false);
                    m_Animator.SetBool("CHASE", false);
                    m_Animator.SetBool("FOCUS", false);
                    m_Animator.SetBool("ATTACK1_1", false);
                    m_Animator.SetBool("ATTACK1_2", true);
                    m_Animator.SetBool("ATTACK1_3", false);
                    m_Animator.SetBool("ATTACKED", false);
                    m_Animator.SetBool("DEATH", false);
                }
                break;
            case "ATTACK1_3":
                {
                    m_Animator.SetBool("EYE", false);
                    m_Animator.SetBool("CHASE", false);
                    m_Animator.SetBool("FOCUS", false);
                    m_Animator.SetBool("ATTACK1_1", false);
                    m_Animator.SetBool("ATTACK1_2", false);
                    m_Animator.SetBool("ATTACK1_3", true);
                    m_Animator.SetBool("ATTACKED", false);
                    m_Animator.SetBool("DEATH", false);
                }
                break;
            case "ATTACKED":
                {
                    m_Animator.SetBool("EYE", false);
                    m_Animator.SetBool("CHASE", false);
                    m_Animator.SetBool("FOCUS", false);
                    m_Animator.SetBool("ATTACK1_1", false);
                    m_Animator.SetBool("ATTACK1_2", false);
                    m_Animator.SetBool("ATTACK1_3", false);
                    m_Animator.SetBool("ATTACKED", true);
                    m_Animator.SetBool("DEATH", false);
                }
                break;
            case "DEATH":
                {
                    m_Animator.SetBool("EYE", false);
                    m_Animator.SetBool("CHASE", false);
                    m_Animator.SetBool("FOCUS", false);
                    m_Animator.SetBool("ATTACK1_1", false);
                    m_Animator.SetBool("ATTACK1_2", false);
                    m_Animator.SetBool("ATTACK1_3", false);
                    m_Animator.SetBool("ATTACKED", false);
                    m_Animator.SetBool("DEATH", true);
                }
                break;
        }
    }
}
