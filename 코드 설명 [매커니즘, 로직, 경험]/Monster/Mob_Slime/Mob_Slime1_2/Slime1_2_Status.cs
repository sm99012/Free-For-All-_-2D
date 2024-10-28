using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime1_2_Status : Slime1_Status // 기반이 되는 Slime1_Status 클래스 상속
{
    void Start()
    {
        m_sMonsterName = "초원 슬라임";
        
        m_nMonsterCode = 2000;
        
        m_eObject_State = E_OBJECT_STATE.MONSTER;
        m_eMonster_Kind = E_MONSTER_KIND.SLIME;

        InitialSet_SOC(); // 몬스터 스탯(평판) 초기 설정
        InitialSet_STATUS(); // 몬스터 스탯(능력치) 초기 설정
    }

    // 몬스터 스탯(평판) 초기 설정 - 부모 클래스인 Slime1_Status의 Respone() 함수를 사용한다.
    // override public void InitialSet_SOC() {ㆍㆍㆍ}
    // 몬스터 스탯(능력치) 초기 설정 - 부모 클래스인 Slime1_Status의 Respone() 함수를 사용한다.
    // override public void InitialSet_STATUS() {ㆍㆍㆍ}

    // 몬스터 피격 시 스탯(능력치) 변동 함수 - 부모 클래스인 Monster_Status의 Attacked() 함수를 사용한다.
    // virtual public bool Attacked(int dm, float dmrate) {ㆍㆍㆍ}

    // 몬스터 피격 시 스탯(능력치(몬스터 현재체력)) 계산 함수 - 부모 클래스인 Monster_Status의 Operator_HP() 함수를 사용한다.
    // bool Operator_HP(int dm) {ㆍㆍㆍ}

    // 몬스터 놓아주기 함수 - 부모 클래스인 Slime1_Status의 Respone() 함수를 사용한다.
    // override public void Goaway() {ㆍㆍㆍ}

    // 몬스터 리스폰 함수 - 부모 클래스인 Monster_Status의 Respone() 함수를 사용한다.
    // virtual public void Respone() {ㆍㆍㆍ}
}
