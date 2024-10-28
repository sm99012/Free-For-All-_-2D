using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime1_RedHat1_Move : Slime1_Move // 기반이 되는 Slime1_Move 클래스 상속
{
    // 부모 클래스인 Slime1_Move의 Awake() 함수를 사용한다.
    // private void Awake() {ㆍㆍㆍ}

    // 부모 클래스인 Slime1_Move의 Start() 함수를 사용한다.
    // void Start() {ㆍㆍㆍ}

    // 몬스터 이동 함수 - 부모 클래스인 Slime1_Move의 Move() 함수를 사용한다.
    // override public void Move(int speed, Vector3 dir) {ㆍㆍㆍ}

    // 몬스터 추격 함수 - 부모 클래스인 Slime1_Move의 Chase() 함수를 사용한다.
    // override public void Chase(int speed, Vector3 dir) {ㆍㆍㆍ}
    // 몬스터 추격 시간 계산 코루틴. 몬스터 동작 FSM : 추격(CHASE) -> 평화(IDLE) - 부모 클래스인 Monster_Move의 ProcessPeaceful() 코루틴을 사용한다.
    // virtual protected IEnumerator ProcessPeaceful() {ㆍㆍㆍ}

    // 몬스터 방향 설정 함수 - 부모 클래스인 Slime1_Move의 SetDir() 함수를 사용한다.
    // override public void SetDir(Vector3 dir) {ㆍㆍㆍ}

    // 몬스터 공격 함수 - 부모 클래스인 Slime1_Move의 Attack() 함수를 사용한다.
    // override public bool Attack(float attackspeed) {ㆍㆍㆍ}
    // 몬스터 공격속도 계산 코루틴. 몬스터의 공격속도에 따라 다음 공격까지 기다려야하는 시간을 계산한다. - 부모 클래스인 Monster_Move의 ProcessAttack() 코루틴을 사용한다.
    // virtual protected IEnumerator ProcessAttack(float attackspeed) {ㆍㆍㆍ}
    // 몬스터 공격 종료 함수 - 몬스터 공격 애니메이션의 특정 프레임에서 호출된다.
    // 몬스터 공격 종료 함수(가상 함수). 몬스터 공격 애니메이션의 특정 프레임에서 호출된다. - 부모 클래스인 Monster_Move의 EndAttack() 함수를 사용한다.
    // virtual protected void EndAttack()

    // 몬스터 피격 함수 - 부모 클래스인 Slime1_Move의 Attacked() 함수를 사용한다.
    // override public void Attacked() {ㆍㆍㆍ}
    // 몬스터 피격 시간 계산 코루틴1 - 부모 클래스인 Monster_Move의 ProcessAttacked1() 코루틴을 사용한다.
    // virtual protected IEnumerator ProcessAttacked1() {ㆍㆍㆍ}
    // 몬스터 피격 시간 계산 코루틴2 - 부모 클래스인 Monster_Move의 ProcessAttacked2() 코루틴을 사용한다.
    // virtual protected IEnumerator ProcessAttacked2() {ㆍㆍㆍ}

    // 몬스터 사망 함수. Fadeout 효과 관련 - 부모 클래스인 Slime1_Move의 Death() 함수를 사용한다.
    // override public void Death() {ㆍㆍㆍ}
    // 몬스터 사망 시간 계산 코루틴. Fadeout 효과 관련 계산 - 부모 클래스인 Monster_Move의 ProcessDeath() 코루틴을 사용한다.
    // virtual public IEnumerator ProcessDeath() {ㆍㆍㆍ}

    // 몬스터 놓아주기 함수 - 부모 클래스인 Slime1_Move의 Goaway() 함수를 사용한다.
    // override public void Goaway() {ㆍㆍㆍ}
    // 몬스터 놓아주기 시간 계산 코루틴. Fadeout 효과 관련 계산 - 부모 클래스인 Monster_Move의 ProcessGoaway() 코루틴을 사용한다.
    // virtual public IEnumerator ProcessGoaway() {ㆍㆍㆍ}

    // 몬스터 리스폰 함수 - 부모 클래스인 Monster_Move의 Respone() 함수를 사용한다.
    // virtual public void Respone() {ㆍㆍㆍ}

    // Fadein 효과 연출 함수(가상 함수) - 부모 클래스인 Monster_Move의 Fadein() 함수를 사용한다.
    // virtual public void Fadein() {ㆍㆍㆍ}
    // Fadein 효과 계산 코루틴 - 부모 클래스인 Monster_Move의 ProcessFadein() 코루틴을 사용한다.
    // IEnumerator ProcessFadein() {ㆍㆍㆍ}

    // 몬스터 동작 FSM 변경 함수 - 부모 클래스인 Slime1_Move의 SetMonsterMoveState() 함수를 사용한다.
    // override public E_MONSTER_MOVE_STATE SetMonsterMoveState(E_MONSTER_MOVE_STATE ms, float attackspeed = 0) {ㆍㆍㆍ}

    // 애니메이션 관리 - 부모 클래스인 Slime1_Move의 SetAnimationParameters() 함수를 사용한다.
    // override public void SetAnimationParameters(string str)
}
