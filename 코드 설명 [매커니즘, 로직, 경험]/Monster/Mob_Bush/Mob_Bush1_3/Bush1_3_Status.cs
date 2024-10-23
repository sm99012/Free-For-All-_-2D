using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// ※ 해당 클래스는 Bush1_Status 클래스를 상속해 몬스터("수풀")의 스탯(능력치)만을 다르게 조정한 클래스이다. 따라서 스탯(능력치)을 제외한 다른 기능은 모두 Bush1_Status 클래스와 동일하다.
//    동일한 몬스터("수풀")임에도 각각 다른 스탯(능력치)을 보유하도록 몬스터("수풀")를 다양화 했다.
//

public class Bush1_3_Status : Bush1_Status // 기반이 되는 Bush1_Status 클래스 상속
{
    // 부모 클래스인 Bush1_Status의 Start() 함수를 사용한다.
    // void Start() {ㆍㆍㆍ}
    
    // 몬스터 스탯(평판) 초기 설정 - 부모 클래스인 Bush1_Status의 InitialSet_SOC() 함수를 사용한다.
    // override public void InitialSet_SOC() {ㆍㆍㆍ}
    
    // 몬스터 스탯(능력치) 초기 설정
    override public void InitialSet_STATUS()
    {
        m_sStatus = new STATUS(1, 0, 0, 12, 12);  // 레벨 : 1
                                                  // 최대체력 : 12
        m_sStatus_Origin.SetSTATUS(m_sStatus);
        m_sStatus_Goaway = new STATUS();
        m_sStatus_Death = new STATUS(0, 0, 0);
    }

    // 몬스터 피격 시 스탯(능력치) 변동 함수 - 부모 클래스인 Monster_Status의 Attacked() 함수를 사용한다.
    // virtual public bool Attacked(int dm, float dmrate) {ㆍㆍㆍ}

    // 몬스터 피격 시 스탯(능력치(몬스터 현재체력)) 계산 함수 - 부모 클래스인 Monster_Status의 Operator_HP() 함수를 사용한다.
    // bool Operator_HP(int dm) {ㆍㆍㆍ}

    // 몬스터 놓아주기 관련 함수 - 부모 클래스인 Bush1_Status의 Goaway() 함수를 사용한다.
    // override public void Goaway() {ㆍㆍㆍ}

    // 몬스터 리스폰 함수 - 부모 클래스인 Monster_Status의 Respone() 함수를 사용한다.
    // virtual public void Respone() {ㆍㆍㆍ}
}
