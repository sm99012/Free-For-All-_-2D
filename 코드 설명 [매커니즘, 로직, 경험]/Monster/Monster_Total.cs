using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// ※ 몬스터에 관한 모든것이 Monster_Total 클래스를 기반으로 작성된 각종 몬스터의 ㆍㆍㆍ_Total 클래스를 거친다.
//    Monster_Total 클래스에 Monster_Status, Moster_Move, Monster_Drop, Monster_Effect 클래스가 포함된다.(Has - a 관계)
//    가상 함수를 이용해 몬스터의 특징에 따른 적절한 동작을 구현했다.
//

public class Monster_Total : MonoBehaviour
{
    public Monster_Status m_ms_Status; // 몬스터 스탯(능력치, 평판)
    public Monster_Move m_mm_Move;     // 몬스터 움직임
    public Monster_Drop m_md_Drop;     // 몬스터 아이템 드랍
    public Monster_Effect m_me_Effect; // 몬스터 이펙트

    public GameObject m_gTarget; // 몬스터 추격 대상

    // 몬스터 접촉 시 오브젝트(플레이어) 피격 (몸박뎀 판정) 관련 변수
    public bool m_bRelation;           // 몬스터 접촉 시 오브젝트(플레이어) 피격 여부(몸박뎀 존재 여부) (m_bRelation == true : 몬스터 접촉 시 오브젝트 피격 가능 / m_bRelation == false : 몬스터 접촉 시 오브젝트 피격 불가능)
    protected Collider2D[] co2_2;      // 몬스터 접촉 콜라이더
    protected Vector2 m_vSize_HitBody; // 몬스터 접촉 범위
    protected Vector3 m_vKnockBackDir; // 몬스터 접촉 시 오브젝트(플레이어) 넉백 방향

    public bool m_bWait; // 몬스터 상호작용 가능 여부 (m_bWait == true : 다른 오브젝트와 상호작용 불가능 / m_bWait == false : 다른 오브젝트와 상호작용 가능)
                         // 몬스터 리스폰 관련 변수
                         
    public bool m_bPlay; // 몬스터 동작 가능 여부 (m_bPlay == true : 몬스터 동작 가능 / m_bPlay == false : 몬스터 동작 불가능)
                         // 최적화를 위해 고안한 방법으로, 플레이어가 위치한 맵 내의 몬스터만 동작이 가능하다.

    public int nLayer1; // 몬스터와 충돌 가능한 오브젝트(플레이어) 레이어

    // 몬스터 이동 방향 설정 관련 변수
    public Vector3 m_vDir = Vector3.right; // 몬스터 이동 방향
    public int m_nRandomNumber;            // 몬스터 이동 방향 설정 관련 변수
                                           // ↖  ↑  ↗
                                           // ←       →
                                           // ↙  ↓  ↘
    public bool m_bSetTime;                // 몬스터 이동 시간 설정 관련 변수 (m_bSetTime == true : 몬스터 이동 방향 설정 가능 / m_bSetTime == false : 몬스터 이동 방향 설정 불가능)
    public float m_fTime;                  // 몬스터 이동 시간

    private void Awake()
    {
        m_ms_Status = this.gameObject.GetComponent<Monster_Status>();
        m_mm_Move = this.gameObject.GetComponent<Monster_Move>();
        m_md_Drop = this.gameObject.GetComponent<Monster_Drop>();
        m_me_Effect = this.gameObject.GetComponent<Monster_Effect>();
        
        m_bRelation = false;
        m_bWait = false;
        m_bPlay = false;

        // 레이어 설정
        nLayer1 = 1 << LayerMask.NameToLayer("Player"); // 몬스터와 충돌 가능한 오브젝트(플레이어) 레이어
    }

    // Fadein 효과 연출과 함께 몬스터 리스폰
    void Start()
    {
        Fadein(); // Fadein 효과 연출 함수
    }

    // 몬스터 이동 함수(가상 함수)
    virtual public void Move()
    {

    }
    // 몬스터 이동 방향 설정 함수(가상 함수)
    virtual public void SetDir()
    {

    }

    // 몬스터 추격 함수(가상 함수)
    virtual public void Chase()
    {

    }    
    // 몬스터 탐지 함수(가상 함수). 몬스터 공격으로 이어지는 함수
    virtual public void Detect()
    {

    }

    // 몬스터 공격 함수(가상 함수)
    // return true : 몬스터 공격 성공 / return false : 몬스터 공격 실패(공격속도 영향)
    virtual public bool Attack(float attackspeed) // attackspeed : 몬스터 공격속도
    {
        if (m_mm_Move.Attack(attackspeed) == true) // 몬스터 공격 함수
            return true;
        else
            return false;
    }

    // 몬스터 공격 판정 함수(가상 함수)
    // 오버랩을 이용해 공격 범위내의 모든 오브젝트(플레이어)에 데미지를 가한다.
    virtual public void Attack_Check()
    {
        
    }

    // 몬스터 접촉 시 오브젝트(플레이어) 피격 판정 함수(몸박뎀 판정)
    // 오버랩을 이용해 범위내의 모든 오브젝트(플레이어)에 특정 데미지 계수를 적용한 데미지를 가한다.
    virtual public void BodyDamage(float percent, float radius, Vector3 offset, float knockbacktime = 0.3f) // percent : 데미지 계수, radius : 몬스터 접촉 범위, offset : 몬스터 접촉 범위 위치 오프셋, knockbacktime : 넉백 시간
    {
        co2_2 = Physics2D.OverlapCircleAll(this.transform.position + offset, radius, nLayer1); // 오버랩 서클
        if (co2_2.Length > 0)
        {
            for (int i = 0; i < co2_2.Length; i++)
            {
                if (co2_2[i].gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    m_vKnockBackDir = Vector3.Normalize(co2_2[i].gameObject.transform.position - this.transform.position);
                    co2_2[i].GetComponent<Player_Total>().Attacked((int)((float)m_ms_Status.m_sStatus.GetSTATUS_Damage_Total() * percent), m_vKnockBackDir, knockbacktime, m_ms_Status.m_sMonsterName); // 플레이어 피격 함수
                }
            }
        }
    }

    // 몬스터 피격 함수
    // return true : 몬스터 피격 O / return false : 몬스터 피격 X
    virtual public bool Attacked(int dm, float dmrate, GameObject gm) // dm : 피격 데미지, dmrate : 피격 데미지 계수, gm : 몬스터 타격 대상(플레이어)
    {
        if (m_mm_Move.m_bPower == false) // 몬스터 피격 가능할 경우
        {
            m_gTarget = gm;

            if (m_ms_Status.Attacked(dm, dmrate) == true) // 몬스터 피격 시 스탯(능력치) 변동 함수
            {
                Death(0); // 몬스터 사망 함수 + 리스폰 함수
            }
            else
                m_mm_Move.Attacked(); // 몬스터 피격 함수

            return true;
        }

        return false;
    }

    // 몬스터 사망 함수 + 리스폰 함수
    virtual public void Death(float time) // time : 리스폰까지 필요한 대기시간
    {
        m_bWait = true; // 몬스터 상호작용 불가능
        
        m_gTarget.GetComponent<Player_Total>().MobDeath(this); // 몬스터 토벌 시 플레이어 업데이트(스탯(능력치(경험치 + @), 평판), 퀘스트 현황 업데이트)

        StartCoroutine(ProcessRespone(time)); // 몬스터 사망 코루틴
        
        m_mm_Move.Death(); // 몬스터 사망 함수. Fadeout 효과 관련
        m_md_Drop.DropItem_Death(m_ms_Status.m_nMonsterCode, this.gameObject.transform.position); // 몬스터 토벌로 인한 아이템 드롭(아이템 필드 생성)
    }
    // 몬스터 사망 함수 _ 일회용 오브젝트 사망(제거). 튜토리얼 전용, 특수 전용
    virtual public void Death()
    {
        m_bWait = true; // 몬스터 상호작용 불가능
            
        m_gTarget.GetComponent<Player_Total>().MobDeath(this); // 몬스터 토벌 시 플레이어 업데이트(스탯(능력치(경험치 + @), 평판), 퀘스트 현황 업데이트)

        StartCoroutine(ProcessDeath()); // 몬스터 사망 코루틴
        
        m_mm_Move.Death(); // 몬스터 사망 함수. Fadeout 효과 관련
        m_md_Drop.DropItem_Death(m_ms_Status.m_nMonsterCode, this.gameObject.transform.position); // 몬스터 토벌로 인한 아이템 드롭(아이템 필드 생성)
    }
    // 몬스터 사망 코루틴 _ 일회용 오브젝트 사망(제거)
    virtual public IEnumerator ProcessDeath()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject); // 해당 오브젝트 삭제
    }
    
    // 몬스터 놓아주기 판정 함수(가상 함수)
    virtual public SOC Goaway_Check()
    {
        m_bWait = true; // 몬스터 상호작용 불가능

        return m_ms_Status.m_sSoc_Goaway; // null값 에 해당하는 스탯(평판) 반환
    }

    // 몬스터 사망 코루틴. 리스폰으로 이어진다.
    virtual public IEnumerator ProcessRespone(float time) // time : 리스폰까지 필요한 대기시간
    {
        yield return new WaitForSeconds(time);
        Respone(); // 몬스터 리스폰 함수
    }
    // 몬스터 리스폰 함수
    virtual public void Respone()
    {
        m_bWait = false; // 몬스터 상호작용 가능
        m_mm_Move.Respone(); // 몬스터 리스폰 함수
        m_ms_Status.Respone(); // 몬스터 리스폰 함수
    }

    // Fadein 효과 연출 함수
    virtual public void Fadein()
    {
        m_mm_Move.Fadein(); // Fadein 효과 연출 함수
    }
}
