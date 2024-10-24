using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// ※ "짙은 앤트"는 이동형 몬스터로 설계했다. 매우 느린 속도로 이동하며 단단하다. 또 묵직한 한방을 자랑한다.
//    "짙은 앤트"는 공격 시 지면 아래로 뿌리를 뻗어 일정 시간 경과 후 오브젝트(플레이어)의 위치로 뿌리를 돌출 시키는 공격을 한다. 꽤나 멀리까지 공격이 가능하다.
//

public class Ents1_Total : Monster_Total // 기반이 되는 Monster_Total 클래스 상속
{
    // 몬스터 탐지 관련 변수
    Collider2D[] co2_1;                              // 몬스터 탐지 콜라이더
    Vector2 m_vDetectSize = new Vector2(1.5f, 1.5f); // 몬스터 탐지 범위
    Vector3 m_vOffset = new Vector3(0, 0.2f, 0);     // 몬스터 탐지 범위 오프셋
    Vector3 m_vTargetPos;                            // 공격 대상 오브젝트(플레이어) 위치
    
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

    // 몬스터 이동 방향 설정 함수
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

    // 몬스터 탐지 함수 - "짙은 앤트"는 비교적 멀리있는 오브젝트(플레이어)를 탐지해 공격으로 이어간다.
    override public void Detect()
    {
        co2_1 = Physics2D.OverlapBoxAll(this.transform.position + m_vOffset, m_vDetectSize, 0, nLayer1); // 오버랩 박스

        if (co2_1.Length > 0)
        {
            for (int i = 0; i < co2_1.Length; i++)
            {
                m_vTargetPos = m_gTarget.transform.position;
                Attack(m_ms_Status.m_sStatus.GetSTATUS_AttackSpeed()); // 몬스터 공격 함수
                break;
            }
        }
    }
    
    // 몬스터 공격 함수 - 부모 클래스인 Monster_Total의 Attack() 함수를 사용한다.
    // virtual public bool Attack(float attackspeed) {ㆍㆍㆍ}

    // 몬스터 공격 판정 함수 - 몬스터 공격 애니메이션의 특정 프레임에서 호출된다. "짙은 앤트"는 특별한 공격 이펙트를 연출한다.
    override public void Attack_Check()
    {
        m_vTargetPos = m_gTarget.transform.position; // 공격 대상 오브젝트(플레이어) 위치 설정
        m_me_Effect.Effect1(m_vTargetPos, m_ms_Status.m_sStatus.GetSTATUS_Damage_Total(), m_ms_Status.m_sMonsterName); // "짙은 앤트"의 공격 이펙트 연출 함수
    }

    // 몬스터 접촉 시 오브젝트(플레이어) 피격 판정 함수(몸박뎀 판정) - "짙은 앤트"는 오브젝트(플레이어) 접촉 판정이 없다.(몸박뎀이 존재하지 않는다.)
    override public void BodyDamage() { }

    // 몬스터 피격 함수
    override public bool Attacked(int dm,  float dmrate, GameObject gm) // dm : 피격 데미지, dmrate : 피격 데미지 계수, gm : 몬스터 타격 대상(플레이어)
    {
        if (m_mm_Move.m_bPower == false) // 몬스터 피격 가능할 경우
        {
            if (m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.IDLE ||
                m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.RUN ||
                m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.CHASE ||
                m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.ATTACK ||
                m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.ATTACKED) // 몬스터 동작 FSM 상태 판단
            {
                m_gTarget = gm;

                if (m_ms_Status.Attacked(dm, dmrate) == true) // 몬스터 피격 시 스탯(능력치) 변동 함수
                {
                    Death(10); // 몬스터 사망 함수 + 리스폰 함수(리스폰까지 필요한 대기시간 : 10초)
                }
                else
                {
                    m_mm_Move.Attacked(); // 몬스터 피격 함수
                }


                return true;
            }
        }

        return false;
    }

    // 몬스터 사망 함수 + 리스폰 함수 - 부모 클래스인 Monster_Total의 Death() 함수를 사용한다.
    // virtual public void Death(float time) {ㆍㆍㆍ}

    // 몬스터 놓아주기 판정 함수
    override public SOC Goaway()
    {
        if (m_bWait == false) // 다른 오브젝트와 상호작용 가능
        {
            if (m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.IDLE || m_mm_Move.m_eMonsterState == Monster_Move.E_MONSTER_MOVE_STATE.RUN) // 몬스터 동작 FSM 상태 판단
            {
                m_bWait = true; // 다른 오브젝트와 상호작용 불가능
                m_ms_Status.Goaway(); // 몬스터 놓아주기 판정 함수
                m_mm_Move.Goaway(); // 몬스터 놓아주기 판정 함수
                m_md_Drop.DropItem_Goaway(m_ms_Status.m_nMonsterCode, this.gameObject.transform.position); // 몬스터 놓아주기로 인한 아이템 드롭(아이템 필드 생성)
                m_me_Effect.Effect_Goaway(this.transform.position); // 몬스터 놓아주기 이펙트 연출 함수

                StartCoroutine(ProcessRespone(15f)); // 몬스터 사망 코루틴

                return m_ms_Status.m_sSoc_Goaway;
            }
        }

        return m_ms_Status.m_sSoc_null;
    }

    // 몬스터 사망 코루틴 - 부모 클래스인 Monster_Total의 ProcessRespone() 코루틴을 사용한다.
    // virtual public IEnumerator ProcessRespone(float time) {ㆍㆍㆍ}

    // 몬스터 리스폰 함수 - 부모 클래스인 Monster_Total의 Respone() 함수를 사용한다.
    // virtual public void Respone() {ㆍㆍㆍ}

    // Fadein 효과 연출 함수 - 부모 클래스인 Monster_Total의 Fadein() 함수를 사용한다.
    // virtual public void Fadein() {ㆍㆍㆍ}
}
