using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy1_Total : Monster_Total
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
        if (m_bRelation == true && m_bWait == false)
        {
            BodyDamage();
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

                StartCoroutine(ProcessRespone(15f));

                return m_ms_Status.m_sSoc_Goaway;
            }
        }

        return m_ms_Status.m_sSoc_null;
    }


    override public void Detect()
    {

    }

    virtual public void BodyDamage()
    {

    }


    override public bool Attack(float attackspeed)
    {
        return false;
    }
}
