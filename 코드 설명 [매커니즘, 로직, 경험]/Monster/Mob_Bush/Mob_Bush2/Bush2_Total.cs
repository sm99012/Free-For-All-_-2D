using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// ※ "가시덤불"은 고정형 오브젝트(장애물)로 설계했다. "수풀"을 기반으로 설계 및 구현 되었다.
//    해당 클래스는 Bush1_Total 클래스를 상속해 스탯(능력치), 몬스터 접촉 시 오브젝트(플레이어) 피격 판정을 다르게 조정한 클래스이다. 따라서 다른 기능은 Bush1_Total 클래스와 동일하다.
//

public class Bush2_Total : Bush1_Total // 기반이 되는 Bush1_Total 클래스 상속
{
    Vector3 m_vSize_HitBody_Offset = new Vector3(0, 0.1f, 0); // 몬스터 접촉 범위 오프셋

    void Update()
    {
        if (m_bPlay == true) // 몬스터 동작 가능할 경우
        {
            if (m_bRelation == true && m_bWait == false) // 몬스터 접촉 시 오브젝트 피격 가능 && 다른 오브젝트와 상호작용 가능
            {
                BodyDamage(1.0f, 0.1f, m_vSize_HitBody_Offset, 0.3f); // 몬스터 접촉 시 오브젝트(플레이어) 피격 판정 함수(몸박뎀 판정)
            }
        }
    }

    // 몬스터 이동 함수 - "수풀"은 이동하지 않는다.
    // override public void Move() { }

    // 몬스터 이동 방향 설정 함수 - "수풀"은 이동 방향 설정을 하지 않는다.
    // override public void SetDir() { }
    
    // 몬스터 추격 함수 - "수풀"은 추격하지 않는다.
    // override public void Chase() { }

    // 몬스터 탐지 함수 - "수풀"은 탐지하지 않는다.
    // override public void Detect() { }

    // 몬스터 공격 함수 - "수풀"은 공격하지 않는다.
    // override public bool Attack(float attackspeed) { }

    // 몬스터 공격 판정 함수 - "수풀"은 공격하지 않기에 공격 판정이 필요 없다.
    // override public void Attack_Check() { }

    // 몬스터 접촉 시 오브젝트(플레이어) 피격 판정 함수(몸박뎀 판정) - 부모 클래스인 Bush1_Total의 BodyDamage() 코루틴을 사용한다.
    // override public void BodyDamage(float percent, float radius, Vector3 offset, float knockbacktime = 0.3F) {ㆍㆍㆍ}

    // 몬스터 피격 함수 - 부모 클래스인 Monster_Total의 Attacked() 코루틴을 사용한다.
    // override public bool Attacked(int dm,  float dmrate, GameObject gm) // dm : 피격 데미지, dmrate : 피격 데미지 계수, gm : 몬스터 타격 대상(플레이어)

    // 몬스터 사망 함수 + 리스폰 함수 - 부모 클래스인 Monster_Total의 Death() 함수를 사용한다.
    // virtual public void Death(float time) {ㆍㆍㆍ}

    // 몬스터 놓아주기 판정 함수 - "수풀"은 놓아주기가 불가능하다.
    // override public SOC Goaway_Check() { }

    // 몬스터 사망 코루틴 - 부모 클래스인 Monster_Total의 ProcessRespone() 코루틴을 사용한다.
    // virtual public IEnumerator ProcessRespone(float time) {ㆍㆍㆍ}

    // 몬스터 리스폰 함수 - 부모 클래스인 Monster_Total의 Respone() 함수를 사용한다.
    // virtual public void Respone() {ㆍㆍㆍ}

    // Fadein 효과 연출 함수 - 부모 클래스인 Monster_Total의 Fadein() 함수를 사용한다.
    // virtual public void Fadein() {ㆍㆍㆍ}
}
