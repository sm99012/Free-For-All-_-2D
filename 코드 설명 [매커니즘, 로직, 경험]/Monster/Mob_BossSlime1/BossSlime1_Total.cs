using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSlime1_Total : Monster_Total
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
        if (m_bWait == false)
        {
            if (m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.CHASE)
            {
                Chase();
                Detect();
            }
        }

        //AnimationTest();
    }

    public override void Chase()
    {
        m_vDir = Vector3.Normalize(m_gTarget.transform.position - this.transform.position);
        m_mm_Move.Chase(m_ms_Status.m_sStatus.GetSTATUS_Speed(), m_vDir);
    }

    override public bool Attacked(int dm,  float dmrate, GameObject gm)
    {
        if (m_mm_Move.m_bPower == false)
        {
            if (m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.IDLE ||
                m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.CHASE ||
                m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.ATTACK1 ||
                m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.ATTACK2 ||
                m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.ATTACKED)
            {
                m_gTarget = gm;

                if (m_ms_Status.Attacked(dm, dmrate) == true)
                {
                    Death(3); // 리스폰타임 3
                }
                else
                    m_mm_Move.Attacked();

                return true;
            }
        }

        return false;
    }

    // ATTACK 상태에서의 공격
    Collider2D[] co2_1;
    override public void Detect()
    {
        co2_1 = Physics2D.OverlapCircleAll(this.transform.position, 0.05f, nLayer1);

        if (co2_1.Length > 0)
        {
            for (int i = 0; i < co2_1.Length; i++)
            {
                if (co2_1[i].gameObject == m_gTarget)
                {
                    StartCoroutine(ProcessAttack()); //
                    break;
                }
            }
        }
    }

    // 평시 몸박뎀
    Collider2D[] co2_2;
    Vector2 m_vSize1 = new Vector2(0.15f, 0.1f);
    public void BodyDamage()
    {
        co2_2 = Physics2D.OverlapCircleAll(this.transform.position, 0.05f, nLayer1);
        if (co2_2.Length > 0)
        {
            for (int i = 0; i < co2_2.Length; i++)
            {
                if (co2_2[i].gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    m_vKnockBackDir = Vector3.Normalize(co2_2[i].gameObject.transform.position - this.transform.position);
                    co2_2[i].GetComponent<Player_Total>().Attacked((int)((float)m_ms_Status.m_sStatus.GetSTATUS_Damage_Total() * 0.75f), m_vKnockBackDir);
                }
            }
        }
    }

    Collider2D[] co2_3;
    Vector2 m_vSize2 = new Vector2(0.1f, 0.15f);
    Vector3 m_vOffset = new Vector3(0.05f, 0, 0);
    override public void Attack_Check()
    {
        if (m_vDir.x >= 0)
            co2_3 = Physics2D.OverlapBoxAll(this.transform.position + m_vOffset, m_vSize2, 0, nLayer1);
        else
            co2_3 = Physics2D.OverlapBoxAll(this.transform.position - m_vOffset, m_vSize2, 0, nLayer1);

        if (co2_3.Length > 0)
        {
            for (int i = 0; i < co2_3.Length; i++)
            {
                m_vKnockBackDir = Vector3.Normalize(co2_2[i].gameObject.transform.position - this.transform.position);
                co2_3[i].gameObject.GetComponent<Player_Total>().Attacked((int)((float)m_ms_Status.m_sStatus.GetSTATUS_Damage_Total() * 1.2f), m_vKnockBackDir);
            }
        }
    }
    IEnumerator ProcessAttack()
    {
        if (m_mm_Move.Attack(m_ms_Status.m_sStatus.GetSTATUS_AttackSpeed()) == true)
        {
            yield return new WaitForSeconds(0.5f);
            Attack(m_ms_Status.m_sStatus.GetSTATUS_AttackSpeed());
        }
        else
            yield return new WaitForSeconds(0.5f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, 0.05f);
        if (m_vDir.x >= 0)
            Gizmos.DrawWireCube(this.transform.position + m_vOffset, m_vSize2);
        else
            Gizmos.DrawWireCube(this.transform.position - m_vOffset, m_vSize2);
    }
}
