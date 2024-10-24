using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ※ "전투용 허수아비"는 고정형 몬스터로 설계했다.

public class Puppet2_Total : Dummy1_Total // 기반이 되는 Dummy1_Total 클래스 상속
{
    // 부모 클래스인 Dummy1_Total의 Awake() 함수를 사용한다.
    // private void Awake() {ㆍㆍㆍ}

    // Fadein 효과 연출과 함께 몬스터 리스폰 - 부모 클래스인 Monster_Total의 Start() 함수를 사용한다.
    // void Start()  {ㆍㆍㆍ}

    // 부모 클래스인 Dummy1_Total의 Update() 함수를 사용한다.
    // void Update() {ㆍㆍㆍ}

    // 몬스터 이동 함수 - "전투용 허수아비"는 이동하지 않는다.
    override public void Move() { }
    
    // 몬스터 추격 함수 - "전투용 허수아비"는 추격하지 않는다.
    override public void Chase() { }

    // 몬스터 이동 방향 설정 함수 - "전투용 허수아비"는 이동 방향 설정을 하지 않는다.
    override public void SetDir() { }

    // 몬스터 탐지 함수 - "전투용 허수아비"는 탐지하지 않는다.
    override public void Detect() { }

    // 몬스터 공격 함수 - "전투용 허수아비"는 공격하지 않는다.
    override public bool Attack(float attackspeed) { }

    // 몬스터 공격 함수 - "전투용 허수아비"는 공격하지 않는다.
    override public bool Attack(float attackspeed) { }

    // 몬스터 공격 판정 함수 - "전투용 허수아비"는 공격하지 않기에 공격 판정이 필요 없다.
    override public void Attack_Check() { }

    // 몬스터 접촉 시 오브젝트(플레이어) 피격 판정 함수(몸박뎀 판정) - "전투용 허수아비"는 오브젝트(플레이어) 접촉 판정이 없다.(몸박뎀이 존재하지 않는다.)
    virtual public void BodyDamage() { }

    // 몬스터 피격 함수 - 부모 클래스인 Dummy1_Total의 Attacked() 함수를 사용한다.
    override public bool Attacked(int dm, float dmrate, GameObject gm) {ㆍㆍㆍ}

    // 몬스터 사망 함수 + 리스폰 함수 - 부모 클래스인 Monster_Total의 Death() 함수를 사용한다.
    // virtual public void Death(float time) {ㆍㆍㆍ}
    
    // 몬스터 놓아주기 판정 함수 - "전투용 허수아비"는 놓아주기가 불가능하다.
    override public SOC Goaway_Check()
    {
        return m_ms_Status.m_sSoc_null;
    }

    // 몬스터 사망 코루틴 - 부모 클래스인 Monster_Total의 ProcessRespone() 코루틴을 사용한다.
    // virtual public IEnumerator ProcessRespone(float time) {ㆍㆍㆍ}

    // 몬스터 리스폰 함수 - 부모 클래스인 Monster_Total의 Respone() 함수를 사용한다.
    // virtual public void Respone() {ㆍㆍㆍ}

    // Fadein 효과 연출 함수 - 부모 클래스인 Monster_Total의 Fadein() 함수를 사용한다.
    // virtual public void Fadein() {ㆍㆍㆍ}
}
