using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// ※ 해당 클래스는 Bush1_Status 클래스를 상속해 몬스터("수풀")의 스탯(능력치)만을 다르게 조정한 클래스이다. 따라서 스탯(능력치)을 제외한 다른 기능은 모두 Bush1_Status 클래스와 동일하다.
//    동일한 몬스터("수풀")임에도 각각 다른 스탯(능력치)을 보유하도록 몬스터("수풀")를 다양화 했다.
//

public class Bush1_2_Status : Bush1_Status // 기반이 되는 Bush1_Status 클래스 상속
{
    // 몬스터 스탯(능력치) 초기 설정
    override public void InitialSet_STATUS()
    {
        m_sStatus = new STATUS(1, 0, 0, 7, 7); // 최대체력 = 7
        m_sStatus_Origin.SetSTATUS(m_sStatus);
        m_sStatus_Goaway = new STATUS();
        m_sStatus_Death = new STATUS(0, 0, 0);
    }
}
