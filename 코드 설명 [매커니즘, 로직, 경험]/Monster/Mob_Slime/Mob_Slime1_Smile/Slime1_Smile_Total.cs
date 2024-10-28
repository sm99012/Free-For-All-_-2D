using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// ※ "웃고있는 초원 슬라임"은 굉장히 특이한 개체이다. 그들은 "드넓은 초원"의 주민들을 약올리는데 특화되어있다.
//    "웃고있는 초원 슬라임"은 이동형 몬스터로 설계했다. 제법 빠른 속도로 이동하며 약하다.
//    "웃고있는 초원 슬라임"은 공격 시 몸을 뻗어 오브젝트(플레이어)와 충돌하는 공격을 한다.
//    "초원 슬라임"과 비교해 스탯(능력치, 평판)에 차이가 존재한다. 그러나 별 차이는 없다.
//

public class Slime1_Smile_Total : Slime1_Total // 기반이 되는 Slime1_Total 클래스 상속
{
    // 부모 클래스인 Slime1_Total의 Awake() 함수를 사용한다.
    // private void Awake() {ㆍㆍㆍ}
  
    // Fadein 효과 연출과 함께 몬스터 리스폰 - 부모 클래스인 Monster_Total의 Start() 함수를 사용한다.
    // void Start() {ㆍㆍㆍ}

    // 부모 클래스인 Slime1_Total의 Update() 함수를 사용한다.
    // void Update() {ㆍㆍㆍ}

    // 몬스터 이동 함수 - 부모 클래스인 Slime1_Total의 Move() 함수를 사용한다.
    // override public void Move() {ㆍㆍㆍ}

    // 몬스터 추격 함수 - 부모 클래스인 Slime1_Total의 Chase() 함수를 사용한다.
    // override public  void Chase() {ㆍㆍㆍ}

    // 몬스터 이동 방향 설정 함수 - 부모 클래스인 Slime1_Total의 SetDir() 함수를 사용한다.
    // override public void SetDir() {ㆍㆍㆍ}
    // 몬스터 이동 시간 설정 관련 코루틴 - 부모 클래스인 Slime1_Total의 ProcessSetTime() 코루틴을 사용한다.
    // IEnumerator ProcessSetTime() {ㆍㆍㆍ}

    // 몬스터 탐지 함수 - 부모 클래스인 Slime1_Total의 Detect() 함수를 사용한다.
    // override public void Detect() {ㆍㆍㆍ}

    // 몬스터 공격 함수 - 부모 클래스인 Monster_Total의 Attack() 함수를 사용한다.
    // virtual public bool Attack(float attackspeed) {ㆍㆍㆍ}

    // 몬스터 공격 판정 함수 - 부모 클래스인 Slime1_Total의 Attack_Check() 함수를 사용한다.
    // override public void Attack_Check() {ㆍㆍㆍ}

    // 몬스터 접촉 시 오브젝트(플레이어) 피격 판정 함수(몸박뎀 판정) - 부모 클래스인 Slime1_Total의 BodyDamage() 함수를 사용한다.
    // override public void BodyDamage() { } {ㆍㆍㆍ}

    // 몬스터 피격 함수 - 부모 클래스인 Slime1_Total의 Attacked() 함수를 사용한다.
    // override public bool Attacked() {ㆍㆍㆍ}
    
    // 몬스터 사망 함수 + 리스폰 함수 - 부모 클래스인 Monster_Total의 Death() 함수를 사용한다.
    // virtual public void Death(float time) {ㆍㆍㆍ}

    // 몬스터 놓아주기 판정 함수 - 부모 클래스인 Slime1_Total의 Goaway() 함수를 사용한다.
    // override public SOC Goaway() {ㆍㆍㆍ}

    // 몬스터 리스폰 코루틴 - 부모 클래스인 Monster_Total의 ProcessRespone() 코루틴을 사용한다.
    // virtual public IEnumerator ProcessRespone(float time) {ㆍㆍㆍ}

    // 몬스터 리스폰 함수 - 부모 클래스인 Monster_Total의 Respone() 함수를 사용한다.
    // virtual public void Respone() {ㆍㆍㆍ}

    // Fadein 효과 연출 함수 - 부모 클래스인 Monster_Total의 Fadein() 함수를 사용한다.
    // virtual public void Fadein() {ㆍㆍㆍ}
}
