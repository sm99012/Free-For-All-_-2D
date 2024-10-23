using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// ※ "수풀"은 고정형 오브젝트(장애물)로 설계했다.
//

public class Bush1_Total : Monster_Total // 기반이 되는 Monster_Total 클래스 상속
{
    Vector3 m_vSize_HitBody_Offset = new Vector3(0, -0.043f, 0); // 몬스터 접촉 범위 오프셋
    
    private void Awake()
    {
        m_mm_Move = this.gameObject.GetComponent<Monster_Move>();
        m_ms_Status = this.gameObject.GetComponent<Monster_Status>();
        m_md_Drop = this.gameObject.GetComponent<Monster_Drop>();
        m_me_Effect = this.gameObject.GetComponent<Monster_Effect>();

        m_vSize_HitBody = new Vector2(0.1f, 0.07f);
        m_vSize_HitBody_Offset = new Vector3(0, -0.043f, 0);

        m_bWait = false; // 다른 오브젝트와 상호작용 가능
        m_bSetTime = true; // 몬스터 이동 방향 설정 가능
        m_bRelation = true; // 몬스터 접촉 시 오브젝트(플레이어) 피격 가능(몬스터 몸박뎀 존재)

        // 레이어 설정
        nLayer1 = 1 << LayerMask.NameToLayer("Player"); // 몬스터와 충돌 가능한 오브젝트(플레이어) 레이어
    }

    void Update()
    {
        if (m_bPlay == true) // 몬스터 동작 가능할 경우
        {
            if (m_bRelation == true && m_bWait == false)
            {
                BodyDamage(1, 0, m_vSize_HitBody_Offset); // 몬스터 접촉 시 오브젝트(플레이어) 피격 판정 함수(몸박뎀 판정)
            }
        }
    }

    // 몬스터 이동 함수 - "수풀"은 이동하지 않는다.
    override public void Move()
    {

    }

    // 몬스터 이동 방향 설정 함수 - "수풀"은 이동 방향 설정을 하지 않는다.
    override public void SetDir()
    {
        if (m_bSetTime == true)
        {
            m_vDir = Vector3.Normalize(new Vector3(0, 0, 0));
        }
    }
    
    // 몬스터 추격 함수 - "수풀"은 추격하지 않는다.
    public override void Chase()
    {

    }

    // 몬스터 탐지 함수 - "수풀"은 탐지하지 않는다.
    override public void Detect()
    {

    }

    // 몬스터 공격 함수 - "수풀"은 공격하지 않는다.
    // return false : 몬스터 공격 실패(공격속도 영향)
    override public bool Attack(float attackspeed) // attakcspeed : 몬스터 공격속도
    {
        return false;
    }

    // 몬스터 접촉 시 오브젝트(플레이어) 피격 판정 함수(몸박뎀 판정).
    // 오버랩을 이용해 범위내의 모든 오브젝트(플레이어)에 특정 데미지 계수를 적용한 데미지를 가한다.
    public override void BodyDamage(float percent, float radius, Vector3 offset, float knockbacktime = 0.3F)
    {
        co2_2 = Physics2D.OverlapBoxAll(this.transform.position + offset, m_vSize_HitBody, 0, nLayer1);
        if (co2_2.Length > 0)
        {
            for (int i = 0; i < co2_2.Length; i++)
            {
                if (co2_2[i].gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    m_vKnockBackDir = Vector3.Normalize(co2_2[i].gameObject.transform.position - this.transform.position);
                    co2_2[i].GetComponent<Player_Total>().Attacked((int)((float)m_ms_Status.m_sStatus.GetSTATUS_Damage_Total() * percent), m_vKnockBackDir, knockbacktime, m_ms_Status.m_sMonsterName);
                }
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
}
