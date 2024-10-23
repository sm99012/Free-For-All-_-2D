using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy1_Effect : Monster_Effect // 기반이 되는 Monster_Effect 클래스 상속
{
    // 몬스터 놓아주기 이펙트 연출 함수 - 부모 클래스인 Monster_Effect의 Effect_Goaway() 함수를 사용한다.
    // virtual public void Effect_Goaway(Vector3 pos) {ㆍㆍㆍ}
    // 몬스터 놓아주기 이펙트 연출 코루틴 - 부모 클래스인 Monster_Effect의 ProcessEffect_Goaway() 코루틴을 사용한다.
    // virtual protected IEnumerator ProcessEffect_Goaway(Vector3 pos) {ㆍㆍㆍ}
    
    // 몬스터 ㆍㆍㆍ 이펙트 연출 함수 - "훈련용 허수아비"는 별도의 추가 이펙트를 가지지 않는다.
    override public void Effect1(Vector3 pos) { }
    // 몬스터 ㆍㆍㆍ 이펙트 연출 코루틴 - "훈련용 허수아비"는 별도의 추가 이펙트를 가지지 않는다.
    override public void Effect1(Vector3 pos, int damage, string attackname) { }
}
