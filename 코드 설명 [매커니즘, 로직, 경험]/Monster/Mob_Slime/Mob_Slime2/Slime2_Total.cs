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
    // 몬스터 탐지 관련 변수
    Collider2D[] co2_1; // 몬스터 탐지 콜라이더

    // 몬스터 공격 관련 변수
    Collider2D[] co2_3;                                  // 몬스터 공격 콜라이더
    Vector2 m_vSize2 = new Vector2(0.75f, 0.75f);        // 몬스터 공격 범위
    Vector3 m_vOffset1 = new Vector3(0.2f, 0.2f, 0);     // 몬스터 공격 범위 오프셋(오른쪽 방향)
    Vector3 m_vOffset2 = new Vector3(-0.2f, 0.2f, 0);    // 몬스터 공격 범위 오프셋(왼쪽 방향)
    
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

    // 몬스터 추격 함수 - "큰 초원 슬라임"은 매우 느린 속도로 추격한다.
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
        m_fTime = Random.Range(1, 5);
        yield return new WaitForSeconds(m_fTime);
        m_bSetTime = true; // 몬스터 이동 방향 설정 가능
    }

    // 몬스터 탐지 함수 - "큰 초원 슬라임"은 짧은 거리의 오브젝트(플레이어)를 탐지해 공격으로 이어간다.
    override public void Detect()
    {
        co2_1 = Physics2D.OverlapCircleAll(this.transform.position, 0.5f, nLayer1); // 오버랩 써클

        if (co2_1.Length > 0)
        {
            for (int i = 0; i < co2_1.Length; i++)
            {
                if (co2_1[i].gameObject == m_gTarget)
                {
                    Attack(m_ms_Status.m_sStatus.GetSTATUS_AttackSpeed()); // 몬스터 공격 함수
                    break;
                }
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
                if (co2_3[i].gameObject.GetComponent<Player_Total>().Attacked((int)((float)m_ms_Status.m_sStatus.GetSTATUS_Damage_Total()), m_vKnockBackDir, 0.75f, m_ms_Status.m_sMonsterName) == true) // 플레이어 피격 함수
                    co2_3[i].gameObject.GetComponent<Player_Total>().ApplySkill(SkillManager.Instance.m_Dictionary_Skill["Skill Lv1 Slime2_Shock/Slow"]); // 플레이어 스킬(버프ㆍ디버프, 상태이상) 적용 함수
            }
        }
    }

    // 몬스터 접촉 시 오브젝트(플레이어) 피격 판정 함수(몸박뎀 판정) - 부모 클래스인 Monster_Total의 BodyDamage() 함수를 사용한다.
    // virtual public void BodyDamage() {ㆍㆍㆍ}

    // 몬스터 피격 함수
    // return true : 몬스터 피격 O / return false : 몬스터 피격 X
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
                    Death(20); // 몬스터 사망 함수 + 리스폰 함수(리스폰까지 필요한 대기시간 : 20초)
                }
                else
                    m_mm_Move.Attacked(); // 몬스터 피격 함수

                return true;
            }
        }

        return false;
    }

    // 몬스터 사망 함수 + 리스폰 함수 - 부모 클래스인 Monster_Total의 Death() 함수를 사용한다.
    override public void Death(float time)
    {
        StartCoroutine(ProcessDivision()); // "큰 초원 슬라임" 토벌 시 0 ~ 6마리(90% 확률)의 "초원 슬라임(2)"으로 분열 코루틴

        base.Death(time); // 몬스터 사망 함수 + 리스폰 함수
    }
    // "큰 초원 슬라임" 토벌 시 0 ~ 6마리(90% 확률)의 "초원 슬라임(2)"으로 분열 코루틴
    IEnumerator ProcessDivision()
    {
        yield return new WaitForSeconds(1f); // 1초 대기
        DivisionSlime1(); // "큰 초원 슬라임" 토벌 시 0 ~ 6마리(90% 확률)의 "초원 슬라임(2)"으로 분열 함수
    }
    // "큰 초원 슬라임" 토벌 시 0 ~ 6마리(90% 확률)의 "초원 슬라임(2)"으로 분열 함수
    void DivisionSlime1()
    {
        GameObject obj = Resources.Load("Prefab/Monster/Slime1_2") as GameObject;
        for (int i = 0; i < m_gChildPos.Count; i++) // 6
        {
            if (Random.Range(0, 9) < 9) // "초원 슬라임(2)" 분열 확률 : 0 ~ 9(10)
                                        // 1 / 10 (10%) 확률로 "초원 슬라임(2)" 생성
                                        // 9 / 10 (90%) 확률로 "초원 슬라임(2)" 미생성
            {
                GameObject dobj = Instantiate(obj); // "초원 슬라임(2)" 생성
                dobj.transform.position = m_gChildPos[i].transform.position; // "초원 슬라임(2)" 위치 조정
                dobj.GetComponent<Monster_Total>().m_gTarget = this.m_gTarget; // "초원 슬라임(2)"의 몬스터 추격 대상 설정
                dobj.GetComponent<Monster_Total>().m_bPlay = true; // "초원 슬라임(2)" 동작 가능
                dobj.name = obj.name;
            }
        }
    }

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

                StartCoroutine(ProcessRespone(20)); // 몬스터 리스폰 코루틴(리스폰까지 필요한 대기시간 : 20초)
                
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
