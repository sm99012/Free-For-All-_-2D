using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// ※ "짙은 앤트"는 이동형 몬스터로 설계했다. 매우 느린 속도로 이동하며 단단하다. 또 묵직한 한방을 자랑한다.
//    "짙은 앤트"는 공격 시 지면 아래로 뿌리를 뻗어 일정 시간 경과 후 오브젝트(플레이어)의 위치로 뿌리를 돌출 시키는 공격을 한다. 꽤나 멀리까지 공격이 가능하다.
//

public class Ents1_Total : Monster_Total // 기반이 되는 Monster_Total 클래스 상속
{
    private void Awake()
    {
        m_mm_Move = this.gameObject.GetComponent<Monster_Move>();
        m_ms_Status = this.gameObject.GetComponent<Monster_Status>();
        m_md_Drop = this.gameObject.GetComponent<Monster_Drop>();
        m_me_Effect = this.gameObject.GetComponent<Monster_Effect>();

        m_bRelation = false; // 몬스터 접촉 시 오브젝트(플레이어) 피격 불가능(몬스터 몸박뎀 존재하지 않음)

        m_bWait = false; // 다른 오브젝트와 상호작용 가능
        m_bSetTime = true; // 몬스터 이동 방향 설정 가능
        
        // 레이어 설정
        nLayer1 = 1 << LayerMask.NameToLayer("Player"); // 몬스터와 충돌 가능한 오브젝트(플레이어) 레이어
    }

    // Fadein 효과 연출과 함께 몬스터 리스폰 - 부모 클래스인 Monster_Total의 Start() 함수를 사용한다.
    // void Start()  {ㆍㆍㆍ}

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
        }
    }

    // 몬스터 이동 함수 - "짙은 앤트"는 매우 느린 속도로 이동한다.
    override public void Move()
    {
        m_mm_Move.Move(m_ms_Status.m_sStatus.GetSTATUS_Speed(), m_vDir); // 몬스터 이동 함수
    }

    // 몬스터 추격 함수 - "짙은 앤트"는 매우 느린 속도로 추격한다.
    override public void Chase()
    {
        m_vDir = Vector3.Normalize(m_gTarget.transform.position - this.transform.position); // 몬스터 추격 방향 설정
        m_mm_Move.Chase(m_ms_Status.m_sStatus.GetSTATUS_Speed(), m_vDir); // 몬스터 추격 함수
    }

    // 몬스터 이동 방향 설정 함수 - "짙은 앤트"는 이동 방향 설정을 하지 않는다.
    override public void SetDir()
    {
        if (m_bSetTime == true) // 몬스터 이동 방향 설정 가능
        {
            StartCoroutine(ProcessSetTime()); // 몬스터 이동 시간 설정 관련 코루틴
            m_nRandomNumber = Random.Range(-23, 9); // 몬스터 이동 방향 설정 관련 변수 : -23 ~ 8 (32)
                                                    //  8 / 32 (25%) 확률로 몬스터 이동
                                                    // 24 / 32 (75%) 확률로 몬스터 이동하지 않음
            switch (m_nRandomNumber)
            {
                case 1:
                    {
                        m_vDir = Vector3.Normalize(new Vector3(0, 1, 0)); // 몬스터 ↑ 방향 이동
                    }
                    break;
                case 2:
                    {
                        m_vDir = Vector3.Normalize(new Vector3(0, -1, 0)); // 몬스터 ↓ 방향 이동
                    }
                    break;
                case 3:
                    {
                        m_vDir = Vector3.Normalize(new Vector3(1, 0, 0)); // 몬스터 → 방향 이동
                    }
                    break;
                case 4:
                    {
                        m_vDir = Vector3.Normalize(new Vector3(1, 1, 0)); // 몬스터 ↗ 방향 이동
                    }
                    break;
                case 5:
                    {
                        m_vDir = Vector3.Normalize(new Vector3(1, -1, 0)); // 몬스터 ↘ 방향 이동
                    }
                    break;
                case 6:
                    {
                        m_vDir = Vector3.Normalize(new Vector3(-1, 0, 0)); // 몬스터 ← 방향 이동
                    }
                    break;
                case 7:
                    {
                        m_vDir = Vector3.Normalize(new Vector3(-1, 1, 0)); // 몬스터 ↖ 방향 이동
                    }
                    break;
                case 8:
                    {
                        m_vDir = Vector3.Normalize(new Vector3(-1, -1, 0)); // 몬스터 ↙ 방향 이동
                    }
                    break;
                default:
                {
                    m_vDir = Vector3.Normalize(new Vector3(0, 0, 0)); // 몬스터 이동하지 않음
                } break;
            }
        }
    }
    // 몬스터 이동 시간 설정 관련 코루틴
    IEnumerator ProcessSetTime()
    {
        m_bSetTime = false; // 몬스터 이동 방향 설정 불가능
        // 1 ~ 5초간 방향 설정 불가능. 1 ~ 5초간 지정된 방향으로 몬스터 이동
        m_fTime = Random.Range(1, 6);
        yield return new WaitForSeconds(m_fTime);
        m_bSetTime = true; // 몬스터 이동 방향 설정 가능
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
