using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush1_Total : Monster_Total
{
    private void Awake()
    {
        m_mm_Move = this.gameObject.GetComponent<Monster_Move>();
        m_ms_Status = this.gameObject.GetComponent<Monster_Status>();
        m_md_Drop = this.gameObject.GetComponent<Monster_Drop>();
        m_me_Effect = this.gameObject.GetComponent<Monster_Effect>();

        m_bSetTime = true;
        m_bRelation = true;

        nLayer1 = 1 << LayerMask.NameToLayer("Player");

        m_bWait = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bPlay == true)
        {
            if (m_bRelation == true && m_bWait == false)
            {
                BodyDamage();
            }
        }

        //AnimationTest();
    }

    override public void Move()
    {

    }

    public override void Chase()
    {

    }

    override public void SetDir()
    {
        if (m_bSetTime == true)
        {
            m_vDir = Vector3.Normalize(new Vector3(0, 0, 0));
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
                    Death(30);
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

                //m_md_Drop.DropItem_Goaway(m_ms_Status.m_nMonsterCode, this.gameObject.transform.position);

                StartCoroutine(ProcessRespone(15f));

                return m_ms_Status.m_sSoc_Goaway;
            }
        }

        return m_ms_Status.m_sSoc_null;
    }


    override public void Detect()
    {

    }

    // 평시 몸박뎀
    Vector2 m_vSize1 = new Vector2(0.1f, 0.07f);
    Vector2 m_vOffset = new Vector2(0, -0.043f);
    Vector2 m_vPos;
    virtual public void BodyDamage()
    {
        m_vPos = new Vector2(this.transform.position.x, this.transform.position.y) + m_vOffset;
        co2_2 = Physics2D.OverlapBoxAll(m_vPos, m_vSize1, nLayer1);
        if (co2_2.Length > 0)
        {
            for (int i = 0; i < co2_2.Length; i++)
            {
                //Debug.Log(co2_2[i].gameObject.name);
                if (co2_2[i].gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    m_vKnockBackDir = Vector3.Normalize(co2_2[i].gameObject.transform.position - this.transform.position);
                    co2_2[i].GetComponent<Player_Total>().Attacked((int)((float)m_ms_Status.m_sStatus.GetSTATUS_Damage_Total()), m_vKnockBackDir, 0.3f, m_ms_Status.m_sMonsterName);
                }
            }
        }
    }


    override public bool Attack(float attackspeed)
    {
        return false;
    }
}
