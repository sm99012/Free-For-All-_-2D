using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// ※ "큰 초원 슬라임"은 "초원 슬라임"이 모종의 이유로 거대해진 개체이다.
//    "큰 초원 슬라임"은 이동형 몬스터로 설계했다. 매우 느린 속도로 이동하며 제법 강하고 질기다.
//    "큰 초원 슬라임"은 공격 시 몸을 뻗어 오브젝트(플레이어)와 충돌하는 공격을 한다. 해당 공격에 피격 시 오브젝트(플레이어)에게 상태이상(기절, 둔화)이 적용된다.
//    "큰 초원 슬라임" 접촉 시 오브젝트(플레이어) 피격이 가능하다.(몸박뎀 존재)
//    "큰 초원 슬라임" 토벌 시 0 ~ 6마리(90% 확률)의 "초원 슬라임(2)"으로 분열하며 토벌한 오브젝트(플레이어)를 끊임없이(100초) 추격하며 공격한다.
//

public class Slime2_Total : Monster_Total // 기반이 되는 Monster_Total 클래스 상속
{
    public List<GameObject> m_gChildPos; // "큰 초원 슬라임" 토벌 시 분열하는 "초원 슬라임(2)"의 위치 정보 리스트

    private void Awake()
    {
        m_mm_Move = this.gameObject.GetComponent<Monster_Move>();
        m_ms_Status = this.gameObject.GetComponent<Monster_Status>();
        m_md_Drop = this.gameObject.GetComponent<Monster_Drop>();
        m_me_Effect = this.gameObject.GetComponent<Monster_Effect>();

        m_bRelation = true; // 몬스터 접촉 시 오브젝트(플레이어) 피격 가능(몬스터 몸박뎀 존재)

        m_bWait = false; // 다른 오브젝트와 상호작용 가능
        m_bSetTime = true; // 몬스터 이동 방향 설정 가능

        // 레이어 설정
        nLayer1 = 1 << LayerMask.NameToLayer("Player"); // 몬스터와 충돌 가능한 오브젝트(플레이어) 레이어

        m_gChildPos.Add(transform.Find("Pos1").gameObject);
        m_gChildPos.Add(transform.Find("Pos2").gameObject);
        m_gChildPos.Add(transform.Find("Pos3").gameObject);
        m_gChildPos.Add(transform.Find("Pos4").gameObject);
        m_gChildPos.Add(transform.Find("Pos5").gameObject);
        m_gChildPos.Add(transform.Find("Pos6").gameObject);
    }

    // Fadein 효과 연출과 함께 몬스터 리스폰 - 부모 클래스인 Monster_Total의 Start() 함수를 사용한다.
    // void Start() {ㆍㆍㆍ}

    void Update()
    {
        if (m_bPlay == true) // 몬스터 동작 가능
        {
            if (m_bWait == false) // 다른 오브젝트와 상호작용 가능
            {
                if (m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.IDLE ||
                m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.RUN) // 몬스터 동작 FSM 상태 판단
                {
                    SetDir(); // 몬스터 이동 방향 설정 함수
                    Move(); // 몬스터 이동 함수
                }
                if (m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.CHASE ||
                    m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.ATTACKED) // 몬스터 동작 FSM 상태 판단
                {
                    Chase(); // 몬스터 추격 함수
                    Detect(); // 몬스터 탐지 함수
                }
            }

            if (m_bRelation == true && m_bWait == false) // 몬스터 접촉 시 오브젝트 피격 가능 && 다른 오브젝트와 상호작용 가능
            {
                BodyDamage(0.3f, 0.05f, new Vector2(0, 0.15f), 0.75f); // 몬스터 접촉 시 오브젝트(플레이어) 피격 판정 함수(몸박뎀 판정)
            }
        }
    }

    // 몬스터 이동 함수 - "큰 초원 슬라임"은 매우 느린 속도로 이동한다.
    override public void Move()
    {
        m_mm_Move.Move(m_ms_Status.m_sStatus.GetSTATUS_Speed(), m_vDir); // 몬스터 이동 함수
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
                    Death(20);
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
                StartCoroutine(ProcessRespone(20));
                m_bWait = true;
                m_ms_Status.Goaway();
                m_mm_Move.Goaway();
                //m_md_Drop.DropItem(this.gameObject.transform.position);
                m_md_Drop.DropItem_Goaway(m_ms_Status.m_nMonsterCode, this.gameObject.transform.position);
                m_me_Effect.Effect_Goaway(this.transform.position);

                return m_ms_Status.m_sSoc_Goaway;
            }
        }

        return m_ms_Status.m_sSoc_null;
    }

    override public void Death(float time)
    {
        StartCoroutine(ProcessDivision());

        base.Death(time);
    }

    void DivisionSlime1()
    {
        GameObject obj = Resources.Load("Prefab/Monster/Slime1_2") as GameObject;
        for (int i = 0; i < m_gChildPos.Count; i++)
        {
            if (Random.Range(0, 9) < 9)
            {
                GameObject dobj = Instantiate(obj);
                dobj.transform.position = m_gChildPos[i].transform.position;
                dobj.GetComponent<Monster_Total>().m_gTarget = this.m_gTarget;
                dobj.GetComponent<Monster_Total>().m_bPlay = true;
                dobj.name = obj.name;
            }
        }
    }

    IEnumerator ProcessDivision()
    {
        yield return new WaitForSeconds(1f);
        DivisionSlime1();
    }

    // ATTACK 상태에서의 공격
    Collider2D[] co2_1;
    override public void Detect()
    {
        co2_1 = Physics2D.OverlapCircleAll(this.transform.position, 0.5f, nLayer1);

        if (co2_1.Length > 0)
        {
            for (int i = 0; i < co2_1.Length; i++)
            {
                if (co2_1[i].gameObject == m_gTarget)
                {
                    Attack(m_ms_Status.m_sStatus.GetSTATUS_AttackSpeed());
                    break;
                }
            }
        }
    }

    Collider2D[] co2_3;
    Vector2 m_vSize2 = new Vector2(0.75f, 0.75f);
    Vector3 m_vOffset1 = new Vector3(0.2f, 0.2f, 0);
    Vector3 m_vOffset2 = new Vector3(-0.2f, 0.2f, 0);
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
                if (co2_3[i].gameObject.GetComponent<Player_Total>().Attacked((int)((float)m_ms_Status.m_sStatus.GetSTATUS_Damage_Total()), m_vKnockBackDir, 0.75f, m_ms_Status.m_sMonsterName) == true)
                    co2_3[i].gameObject.GetComponent<Player_Total>().ApplySkill(SkillManager.Instance.m_Dictionary_Skill["Slime2_AttackEffect1"]);
            }
        }
    }
        
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position + new Vector3(0, 0.15f, 0), 0.5f);
        if (m_vDir.x >= 0)
            Gizmos.DrawWireCube(this.transform.position + m_vOffset1, m_vSize2);
        else
            Gizmos.DrawWireCube(this.transform.position + m_vOffset2, m_vSize2);
    }
}
