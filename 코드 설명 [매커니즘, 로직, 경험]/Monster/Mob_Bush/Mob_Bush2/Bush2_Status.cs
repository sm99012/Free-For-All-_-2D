using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush2_Status : Bush1_Status // 기반이 되는 Bush1_Status 클래스 상속
{
    void Start()
    {
        m_sMonsterName = "가시덤불";
        
        m_nMonsterCode = 10;
       
        m_eObject_State = E_OBJECT_STATE.HURDLE;
        m_eMonster_Kind = E_MONSTER_KIND.OBJECT;

        InitialSet_SOC(); // 몬스터 스탯(평판) 초기 설정
        InitialSet_STATUS(); // 몬스터 스탯(능력치) 초기 설정
    }
    
    // 몬스터 스탯(평판) 초기 설정    
    override public void InitialSet_SOC()
    {
        m_sSoc_null = new SOC();
        m_sSoc_Goaway = new SOC(0, 0, 0, 0, 0, 0, 0, 0);
        m_sSoc_Death = new SOC(0, 0, 0, 0, 0, 0, 0, 0);
    }
    
    // 몬스터 스탯(능력치) 초기 설정
    override public void InitialSet_STATUS()
    {
        m_sStatus = new STATUS(12, 0, 0, 20, 20, 0, 0, 5, 5, 5, 0, 0, 0, 0, 0, 0, 0); // 레벨 : 12
                                                                                      // 최대체력 : 20
                                                                                      // 데미지(총데미지) : 5
        m_sStatus_Origin.SetSTATUS(m_sStatus);
        m_sStatus_Goaway = new STATUS();
        m_sStatus_Death = new STATUS(0, 0, 0);
    }

    override public void Goaway()
    {
        m_bPower = true;
    }
}
