using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// ※ "화가 잔뜩난 꼬마 초원 슬라임"은 "초원 슬라임"의 어린 개체이다. 이유는 모르겠으나 화가 나있다.
//    "화가 잔뜩난 꼬마 초원 슬라임"은 이동형 몬스터로 설계했다. 평범한 속도로 이동하며 매우 약하다. 공격을 받지 않아도 먼저 공격하는 선공 몬스터이다.
//    "화가 잔뜩난 꼬마 초원 슬라임"은 공격 시 몸을 뻗어 오브젝트(플레이어)와 충돌하는 공격을 한다.
//    "초원 슬라임"과 비교해 스탯(능력치, 평판), 크기, 생김새에 차이가 존재한다.
//

public class Slime3_Anger_Total : Monster_Total // 기반이 되는 Monster_Total 클래스 상속
{
    // 몬스터 탐지 관련 변수
    Collider2D[] co2_1; // 몬스터 탐지 콜라이더
    
    // 몬스터 공격 관련 변수
    protected Collider2D[] co2_3;                                  // 몬스터 공격 콜라이더
    protected Vector2 m_vSize2 = new Vector2(0.11f, 0.15f);        // 몬스터 공격 범위
    protected Vector3 m_vOffset1 = new Vector3(0.04f, 0.035f, 0);  // 몬스터 공격 범위 오프셋(오른쪽 방향)
    protected Vector3 m_vOffset2 = new Vector3(-0.04f, 0.035f, 0); // 몬스터 공격 범위 오프셋(왼쪽 방향)
    
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
                    if (m_gTarget == null) // 몬스터 추격 대상이 지정되지 않은 경우 - 무작위 방향 이동
                    {
                        SetDir(); // 몬스터 이동 방향 설정 함수
                        Move(); // 몬스터 이동 함수
                    }

                    Detect(); // 몬스터 탐지 함수 - 선공 몬스터이기 때문에 항상 탐지한다.
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

    // 몬스터 이동 함수 - "화가 잔뜩난 꼬마 초원 슬라임"은 평범한 속도로 이동한다.
    override public void Move()
    {
        m_mm_Move.Move(m_ms_Status.m_sStatus.GetSTATUS_Speed(), m_vDir); // 몬스터 이동 함수
    }

    // 몬스터 추격 함수 - "화가 잔뜩난 꼬마 초원 슬라임"은 평범한 속도로 추격한다.
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
            m_nRandomNumber = Random.Range(-7, 8); // 몬스터 이동 방향 설정 관련 변수 : -7 ~ 8 (16)
                                                   // 8 / 16 (50%) 확률로 몬스터 이동
                                                   // 8 / 16 (50%) 확률로 몬스터 이동하지 않음
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
                        m_vDir = Vector3.Normalize(new Vector3(0, 0, 0)); // 몬스터 이동하지 않음
                    }
                    break;
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
            }
        }
    }
    // 몬스터 이동 시간 설정 관련 코루틴
    IEnumerator ProcessSetTime()
    {
        m_bSetTime = false; // 몬스터 이동 방향 설정 불가능
        // 1 ~ 5초간 방향 설정 불가능. 1 ~ 5초간 지정된 방향으로 몬스터 이동
        m_fTime = Random.Range(1, 5);
        yield return new WaitForSeconds(m_fTime);
        m_bSetTime = true; // 몬스터 이동 방향 설정 가능
    }

    // 몬스터 탐지 함수 - "화가 잔뜩난 꼬마 초원 슬라임"은 짧은 거리의 오브젝트(플레이어)를 탐지해 공격으로 이어간다.
    override public void Detect()
    {
        co2_1 = Physics2D.OverlapCircleAll(this.transform.position, 0.1f, nLayer1); // 오버랩 써클

        if (co2_1.Length > 0)
        {
            for (int i = 0; i < co2_1.Length; i++)
            {
                Attack(m_ms_Status.m_sStatus.GetSTATUS_AttackSpeed()); // 몬스터 공격 함수
                break;
            }
        }
    }
    
    // 몬스터 공격 함수 - 부모 클래스인 Monster_Total의 Attack() 함수를 사용한다.
    // virtual public bool Attack(float attackspeed) {ㆍㆍㆍ}

    // 몬스터 공격 판정 함수 - 몬스터 공격 애니메이션의 특정 프레임에서 호출된다.
    override public void Attack_Check()
    {
        if (m_vDir.x >= 0)
            co2_3 = Physics2D.OverlapBoxAll(this.transform.position + m_vOffset1, m_vSize2, 0, nLayer1); // 오버랩 박스
        else
            co2_3 = Physics2D.OverlapBoxAll(this.transform.position + m_vOffset2, m_vSize2, 0, nLayer1); // 오버랩 박스

        if (co2_3.Length > 0)
        {
            for (int i = 0; i < co2_3.Length; i++)
            {
                m_vKnockBackDir = Vector3.Normalize(co2_3[i].gameObject.transform.position - this.transform.position); // 피격 대상 오브젝트(플레이어) 넉백 방향 설정
                co2_3[i].gameObject.GetComponent<Player_Total>().Attacked((int)((float)m_ms_Status.m_sStatus.GetSTATUS_Damage_Total()), m_vKnockBackDir, 0.3f, m_ms_Status.m_sMonsterName); // 플레이어 피격 함수
            }
        }
    }
    
    // 몬스터 접촉 시 오브젝트(플레이어) 피격 판정 함수(몸박뎀 판정) - "화가 잔뜩난 꼬마 초원 슬라임"은 오브젝트(플레이어) 접촉 판정이 없다.(몸박뎀이 존재하지 않는다.)
    override public void BodyDamage() { }

    // 몬스터 피격 함수
    override public bool Attacked(int dm, float dmrate, GameObject gm) // dm : 피격 데미지, dmrate : 피격 데미지 계수, gm : 몬스터 타격 대상(플레이어)
    {
        if (m_mm_Move.m_bPower == false)
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
                    m_mm_Move.Attacked(); // 몬스터 피격 함수

                return true;
            }
        }

        return false;
    }

    // 몬스터 사망 함수 + 리스폰 함수 - 부모 클래스인 Monster_Total의 Death() 함수를 사용한다.
    // virtual public void Death(float time) {ㆍㆍㆍ}

    // 몬스터 놓아주기 판정 함수
    override public SOC Goaway_Check()
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

                StartCoroutine(ProcessRespone(10)); // 몬스터 리스폰 코루틴(리스폰까지 필요한 대기시간 : 10초)

                return m_ms_Status.m_sSoc_Goaway;
            }
        }

        return m_ms_Status.m_sSoc_null;
    }

    // 몬스터 리스폰 코루틴 - 부모 클래스인 Monster_Total의 ProcessRespone() 코루틴을 사용한다.
    // virtual public IEnumerator ProcessRespone(float time) {ㆍㆍㆍ}

    // 몬스터 리스폰 함수 - 부모 클래스인 Monster_Total의 Respone() 함수를 사용한다.
    // virtual public void Respone() {ㆍㆍㆍ}

    // Fadein 효과 연출 함수 - 부모 클래스인 Monster_Total의 Fadein() 함수를 사용한다.
    // virtual public void Fadein() {ㆍㆍㆍ}
}
