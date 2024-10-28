using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// ※ "화가 잔뜩난 꼬마 초원 슬라임"은 "초원 슬라임"의 어린 개체이다. 이유는 모르겠으나 화가 나있다.
//    "화가 잔뜩난 꼬마 초원 슬라임"은 이동형 몬스터로 설계했다. 느린 속도로 이동하며 매우 약하다. 공격을 받지 않아도 먼저 공격하는 선공 몬스터이다.
//    "화가 잔뜩난 꼬마 초원 슬라임"은 공격 시 몸을 뻗어 오브젝트(플레이어)와 충돌하는 공격을 한다.
//    "초원 슬라임"과 비교해 스탯(능력치, 평판)에 차이가 존재한다.
//

public class Slime3_Anger_Total : Monster_Total
{
    private void Awake()
    {
        m_mm_Move = this.gameObject.GetComponent<Monster_Move>();
        m_ms_Status = this.gameObject.GetComponent<Monster_Status>();
        m_md_Drop = this.gameObject.GetComponent<Monster_Drop>();
        m_me_Effect = this.gameObject.GetComponent<Monster_Effect>();

        m_bSetTime = true;
        m_bRelation = false;

        nLayer1 = 1 << LayerMask.NameToLayer("Player");

        m_bWait = false;
    }

    void Start()
    {
        Fadein();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bPlay == true)
        {
            if (m_bWait == false)
            {
                if (m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.IDLE ||
                    m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.RUN)
                {
                    if (m_gTarget == null)
                    {
                        SetDir();
                        Move();
                    }

                    Detect();
                }
                if (m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.CHASE ||
                    m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.ATTACKED)
                {
                    Chase();
                    Detect();
                }
            }

            if (m_bRelation == true && m_bWait == false)
            {
                BodyDamage(0.1f, 0.05f, Vector3.zero);
            }
        }

        //AnimationTest();
    }

    override public void Move()
    {
        m_mm_Move.Move(m_ms_Status.m_sStatus.GetSTATUS_Speed(), m_vDir);
    }

    public override void Chase()
    {
        m_vDir = Vector3.Normalize(m_gTarget.transform.position - this.transform.position);
        m_mm_Move.Chase(m_ms_Status.m_sStatus.GetSTATUS_Speed(), m_vDir);
    }

    override public void SetDir()
    {
        if (m_bSetTime == true)
        {
            StartCoroutine(ProcessSetTime());
            m_nRandomNumber = Random.Range(-7, 9);
            switch (m_nRandomNumber)
            {
                case -7:
                case -6:
                case -5:
                case -4:
                case -3:
                case -2:
                case -1:
                case 0:
                    {
                        m_vDir = Vector3.Normalize(new Vector3(0, 0, 0));
                    }
                    break;
                case 1:
                    {
                        m_vDir = Vector3.Normalize(new Vector3(0, 1, 0));
                    }
                    break;
                case 2:
                    {
                        m_vDir = Vector3.Normalize(new Vector3(0, -1, 0));
                    }
                    break;
                case 3:
                    {
                        m_vDir = Vector3.Normalize(new Vector3(1, 0, 0));
                    }
                    break;
                case 4:
                    {
                        m_vDir = Vector3.Normalize(new Vector3(1, 1, 0));
                    }
                    break;
                case 5:
                    {
                        m_vDir = Vector3.Normalize(new Vector3(1, -1, 0));
                    }
                    break;
                case 6:
                    {
                        m_vDir = Vector3.Normalize(new Vector3(-1, 0, 0));
                    }
                    break;
                case 7:
                    {
                        m_vDir = Vector3.Normalize(new Vector3(-1, 1, 0));
                    }
                    break;
                case 8:
                    {
                        m_vDir = Vector3.Normalize(new Vector3(-1, -1, 0));
                    }
                    break;
            }
        }
    }
    IEnumerator ProcessSetTime()
    {
        m_bSetTime = false;
        m_fTime = Random.Range(1, 6);
        yield return new WaitForSeconds(m_fTime);
        m_bSetTime = true;
    }

    override public bool Attacked(int dm, float dmrate, GameObject gm)
    {
        if (m_mm_Move.m_bPower == false)
        {
            if (m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.IDLE ||
                m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.RUN ||
                m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.CHASE ||
                m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.ATTACK ||
                m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.ATTACKED)
            {
                m_gTarget = gm;

                if (m_ms_Status.Attacked(dm, dmrate) == true)
                {
                    Death(10);
                }
                else
                    m_mm_Move.Attacked();


                return true;
            }
        }

        return false;
    }

    override public SOC Goaway()
    {
        if (m_bWait == false)
        {
            if (m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.IDLE || m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.RUN)
            {
                m_bWait = true;
                m_ms_Status.Goaway();
                m_mm_Move.Goaway();
                //m_md_Drop.DropItem(this.gameObject.transform.position);
                m_md_Drop.DropItem_Goaway(m_ms_Status.m_nMonsterCode, this.gameObject.transform.position);
                m_me_Effect.Effect_Goaway(this.transform.position);

                StartCoroutine(ProcessRespone(10));

                return m_ms_Status.m_sSoc_Goaway;
            }
        }

        return m_ms_Status.m_sSoc_null;
    }

    // ATTACK 상태에서의 공격
    protected Collider2D[] co2_1;
    override public void Detect()
    {
        co2_1 = Physics2D.OverlapCircleAll(this.transform.position, .1f, nLayer1);

        if (co2_1.Length > 0)
        {
            for (int i = 0; i < co2_1.Length; i++)
            {
                Attack(m_ms_Status.m_sStatus.GetSTATUS_AttackSpeed());
                break;
            }
        }
    }

    protected Collider2D[] co2_3;
    protected Vector2 m_vSize2 = new Vector2(0.11f, 0.15f);
    protected Vector3 m_vOffset1 = new Vector3(0.04f, 0.035f, 0);
    protected Vector3 m_vOffset2 = new Vector3(-0.04f, 0.035f, 0);
    override public void Attack_Check()
    {
        if (m_vDir.x >= 0)
            co2_3 = Physics2D.OverlapBoxAll(this.transform.position + m_vOffset1, m_vSize2, 0, nLayer1);
        else
            co2_3 = Physics2D.OverlapBoxAll(this.transform.position + m_vOffset2, m_vSize2, 0, nLayer1);

        if (co2_3.Length > 0)
        {
            for (int i = 0; i < co2_3.Length; i++)
            {
                m_vKnockBackDir = Vector3.Normalize(co2_3[i].gameObject.transform.position - this.transform.position);
                co2_3[i].gameObject.GetComponent<Player_Total>().Attacked((int)((float)m_ms_Status.m_sStatus.GetSTATUS_Damage_Total()), m_vKnockBackDir, 0.3f, m_ms_Status.m_sMonsterName);
            }
        }
    }

    // Animation Test
    void AnimationTest()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            Debug.Log("Test: Monster: IDLE");
            m_mm_Move.m_aAnimator.SetBool("IDLE", true);
            m_mm_Move.m_aAnimator.SetBool("RUN", false);
            m_mm_Move.m_aAnimator.SetBool("ATTACKED", false);
            m_mm_Move.m_aAnimator.SetBool("ATTACK", false);
            m_mm_Move.m_aAnimator.SetBool("DEATH", false);
            m_mm_Move.m_aAnimator.SetBool("GOAWAY", false);
            m_mm_Move.m_aAnimator.SetBool("CHASE", false);
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            Debug.Log("Test: Monster: RUN");
            m_mm_Move.m_aAnimator.SetBool("IDLE", false);
            m_mm_Move.m_aAnimator.SetBool("RUN", true);
            m_mm_Move.m_aAnimator.SetBool("ATTACKED", false);
            m_mm_Move.m_aAnimator.SetBool("ATTACK", false);
            m_mm_Move.m_aAnimator.SetBool("DEATH", false);
            m_mm_Move.m_aAnimator.SetBool("GOAWAY", false);
            m_mm_Move.m_aAnimator.SetBool("CHASE", false);
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            Debug.Log("Test: Monster: ATTACKED");
            m_mm_Move.m_aAnimator.SetBool("IDLE", false);
            m_mm_Move.m_aAnimator.SetBool("RUN", false);
            m_mm_Move.m_aAnimator.SetBool("ATTACKED", true);
            m_mm_Move.m_aAnimator.SetBool("ATTACK", false);
            m_mm_Move.m_aAnimator.SetBool("DEATH", false);
            m_mm_Move.m_aAnimator.SetBool("GOAWAY", false);
            m_mm_Move.m_aAnimator.SetBool("CHASE", false);
        }
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            Debug.Log("Test: Monster: ATTACK");
            m_mm_Move.m_aAnimator.SetBool("IDLE", false);
            m_mm_Move.m_aAnimator.SetBool("RUN", false);
            m_mm_Move.m_aAnimator.SetBool("ATTACKED", false);
            m_mm_Move.m_aAnimator.SetBool("ATTACK", true);
            m_mm_Move.m_aAnimator.SetBool("DEATH", false);
            m_mm_Move.m_aAnimator.SetBool("GOAWAY", false);
            m_mm_Move.m_aAnimator.SetBool("CHASE", false);
        }
        if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            Debug.Log("Test: Monster: DEATH");
            m_mm_Move.m_aAnimator.SetBool("IDLE", false);
            m_mm_Move.m_aAnimator.SetBool("RUN", false);
            m_mm_Move.m_aAnimator.SetBool("ATTACKED", false);
            m_mm_Move.m_aAnimator.SetBool("ATTACK", false);
            m_mm_Move.m_aAnimator.SetBool("DEATH", true);
            m_mm_Move.m_aAnimator.SetBool("GOAWAY", false);
            m_mm_Move.m_aAnimator.SetBool("CHASE", false);
        }
        if (Input.GetKeyUp(KeyCode.Alpha6))
        {
            Debug.Log("Test: Monster: GOAWAY");
            m_mm_Move.m_aAnimator.SetBool("IDLE", false);
            m_mm_Move.m_aAnimator.SetBool("RUN", false);
            m_mm_Move.m_aAnimator.SetBool("ATTACKED", false);
            m_mm_Move.m_aAnimator.SetBool("ATTACK", false);
            m_mm_Move.m_aAnimator.SetBool("DEATH", false);
            m_mm_Move.m_aAnimator.SetBool("GOAWAY", true);
            m_mm_Move.m_aAnimator.SetBool("CHASE", false);
        }
        if (Input.GetKeyUp(KeyCode.Alpha7))
        {
            Debug.Log("Test: Monster: CHASE");
            m_mm_Move.m_aAnimator.SetBool("IDLE", false);
            m_mm_Move.m_aAnimator.SetBool("RUN", false);
            m_mm_Move.m_aAnimator.SetBool("ATTACKED", false);
            m_mm_Move.m_aAnimator.SetBool("ATTACK", false);
            m_mm_Move.m_aAnimator.SetBool("DEATH", false);
            m_mm_Move.m_aAnimator.SetBool("GOAWAY", false);
            m_mm_Move.m_aAnimator.SetBool("CHASE", true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position + new Vector3(0, 0.035f, 0), .1f);
        if (m_vDir.x >= 0)
            Gizmos.DrawWireCube(this.transform.position + m_vOffset1, m_vSize2);
        else
            Gizmos.DrawWireCube(this.transform.position + m_vOffset2, m_vSize2);
    }
}
