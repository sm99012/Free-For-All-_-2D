using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// ※ "짙은 앤트"는 공격 시 공격 이펙트를 연출한다. 해당 이펙트를 통해 공격 판정 함수를 실행한다.
//

public class Mob_Effect_Ent1 : MonoBehaviour
{
    string m_sAttackName;         // 공격 이펙트 이름

    int m_nDamage;                // 공격 이펙트의 데미지(총데미지)
    int nLayer1;                  // 공격 이펙트와 충돌 가능한 오브젝트(플레이어) 레이어

    Vector3 m_vPos;               // 공격 이펙트 연출 위치

    Animator m_aAnimator;         // 공격 이펙트 애니메이터

    // 공격 관련 변수
    Collider2D[] co2_3;           // 공격 콜라이더
    float m_fAttackRadius = 0.1f; // 공격 범위
    
    void Start()
    {
        m_aAnimator = this.GetComponent<Animator>();
        StartCoroutine(ProcessEffect()); // 공격 이펙트 연출 코루틴(애니메이션 실행)

        // 레이어 설정
        nLayer1 = 1 << LayerMask.NameToLayer("Player"); // 몬스터와 충돌 가능한 오브젝트(플레이어) 레이어
    }

    // 공격 이펙트 초기 설정
    public void InitialSet(Vector3 pos, int damage, string attackname) // pos : 공격 이펙트 연출 위치, damage : 공격 이펙트의 데미지(총데미지), attackname : 공격 이펙트 이름
    {
        m_vPos = pos;
        m_nDamage = damage;
        m_sAttackName = attackname;
    }

    // 공격 이펙트 연출 코루틴
    IEnumerator ProcessEffect()
    {
        // Debug.Log("뿌리공격");
        m_aAnimator.SetBool("ON", true); // 애니메이션 실행
        yield return new WaitForSeconds(1f);
        m_aAnimator.SetBool("ON", false); // 애니메이션 종료
        Destroy(this.gameObject); // 공격 이펙트 오브젝트 삭제
    }

    // 공격 이펙트 공격 판정 함수 - 공격 이펙트 애니메이션의 특정 프레임에서 호출된다.
    public void Attack_Check()
    {
        co2_3 = Physics2D.OverlapCircleAll(this.transform.position, m_fAttackRadius, nLayer1); // 오버랩 써클
        if (co2_3.Length > 0)
        {
            for (int i = 0; i < co2_3.Length; i++)
            {
                Vector3 m_vKnockBackDir = Vector3.Normalize(co2_3[i].gameObject.transform.position - this.transform.position); // 피격 대상 오브젝트(플레이어) 넉백 방향 설정
                SetDir(this.transform.position, co2_3[i].gameObject.transform.position); // 공격 이펙트 방향 설정 함수
                co2_3[i].gameObject.GetComponent<Player_Total>().Attacked((int)(m_nDamage * 1.2f), m_vKnockBackDir, 0.3f, m_sAttackName); // 플레이어 피격 함수
                
                break;
            }
        }
    }

    // 공격 이펙트 방향 설정 함수
    void SetDir(Vector3 startpos, Vector3 endpos) // 
    {
        if (startpos.x >= endpos.x)
            this.transform.localScale = new Vector3(1, 1, 1);
        else
            this.transform.localScale = new Vector3(-1, 1, 1);
    }
}
