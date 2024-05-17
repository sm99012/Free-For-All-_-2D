using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ents1_Total : Monster_Total
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

    void Update()
    {
        if (m_bPlay == true)
        {
            if (m_bWait == false)
            {
                if (m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.IDLE ||
                m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.RUN)
                {
                    SetDir();
                    Move();
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
                BodyDamage();
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
            m_nRandomNumber = Random.Range(-23, 9);
            switch (m_nRandomNumber)
            {
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
                default:
                {
                    m_vDir = Vector3.Normalize(new Vector3(0, 0, 0));
                } break;
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

    override public bool Attacked(int dm,  float dmrate, GameObject gm)
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
                {
                    m_mm_Move.Attacked();
                }


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

                StartCoroutine(ProcessRespone(15f));

                return m_ms_Status.m_sSoc_Goaway;
            }
        }

        return m_ms_Status.m_sSoc_null;
    }

    // ATTACK 상태에서의 공격
    Collider2D[] co2_1;
    Vector3 m_vOffset = new Vector3(0, 0.2f, 0);
    Vector2 m_vDetectSize = new Vector2(1.5f, 1.5f);
    Vector3 m_vTargetPos;
    override public void Detect()
    {
        co2_1 = Physics2D.OverlapBoxAll(this.transform.position + m_vOffset, m_vDetectSize, 0, nLayer1);

        if (co2_1.Length > 0)
        {
            for (int i = 0; i < co2_1.Length; i++)
            {
                //if (co2_1[i].gameObject == m_gTarget)
                {
                    m_vTargetPos = m_gTarget.transform.position;
                    Attack(m_ms_Status.m_sStatus.GetSTATUS_AttackSpeed());
                    break;
                }
            }
        }
    }

    // 평시 몸박뎀
    public void BodyDamage()
    {
        co2_2 = Physics2D.OverlapBoxAll(this.transform.position + m_vOffset, m_vDetectSize, 0, nLayer1);
        if (co2_2.Length > 0)
        {
            for (int i = 0; i < co2_2.Length; i++)
            {
                if (co2_2[i].gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    m_vKnockBackDir = Vector3.Normalize(co2_2[i].gameObject.transform.position - this.transform.position);
                    co2_2[i].GetComponent<Player_Total>().Attacked((int)((float)m_ms_Status.m_sStatus.GetSTATUS_Damage_Total() * 0.8f), m_vKnockBackDir, 0.3f, m_ms_Status.m_sMonsterName);
                }
            }
        }
    }

    public override void Attack_Check()
    {
        m_vTargetPos = m_gTarget.transform.position;
        m_me_Effect.Effect1(m_vTargetPos, m_ms_Status.m_sStatus.GetSTATUS_Damage_Total(), m_ms_Status.m_sMonsterName);
    }

    //IEnumerator ProcessAttack()
    //{
    //    // m_mm_Move.Attack(공격속도) 함수 에서 몹의 공격을 관리함 -> 추후 Monster_Total로 코드 변경필요.
    //    if (m_mm_Move.Attack(m_ms_Status.m_sStatus.GetSTATUS_AttackSpeed()) == true)
    //    {
    //        yield return new WaitForSeconds(0.2f);
    //        m_vTargetPos = m_gTarget.transform.position;
    //        m_me_Effect.Effect1(m_vTargetPos);
    //        yield return new WaitForSeconds(0.6f);
    //        Attack(m_ms_Status.m_sStatus.GetSTATUS_AttackSpeed());
    //        yield return new WaitForSeconds(0.2f);

    //    }
    //    else
    //        yield return new WaitForSeconds(0);
    //}


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.transform.position + m_vOffset, m_vDetectSize);
    }
}
