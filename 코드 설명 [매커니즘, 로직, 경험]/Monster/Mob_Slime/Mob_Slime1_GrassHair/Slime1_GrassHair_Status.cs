using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime1_GrassHair_Status : Monster_Status // 기반이 되는 Monster_Status 클래스 상속
{
    void Start()
    {
        m_sMonsterName = "잔디 머리 초원 슬라임";
        
        m_nMonsterCode = 2001;
        
        m_eObject_State = E_OBJECT_STATE.MONSTER;
        m_eMonster_Kind = E_MONSTER_KIND.SLIME;

        InitialSet_SOC(); // 몬스터 스탯(평판) 초기 설정
        InitialSet_STATUS(); // 몬스터 스탯(능력치) 초기 설정
    }

    // 몬스터 스탯(평판) 초기 설정
    override public void InitialSet_SOC()
    {
        m_sSoc_null = new SOC();
        m_sSoc_Goaway = new SOC(0, 0, 0, 0, 0, 0, 0, 0, 0);
        m_sSoc_Death = new SOC(0, 0, 0, 0, 0, 0, 0, 0, 0);
    }
    // 몬스터 스탯(능력치) 초기 설정
    override public void InitialSet_STATUS()
    {
        m_sStatus = new STATUS(6, 0, 0, 50, 50, 3, 3, 4, 4, 4, 0, 0, 30, 3, 3, 0, 2f); // 레벨 : 6
                                                                                       // 최대체력 : 50
                                                                                       // 최대마나 : 3
                                                                                       // 데미지(총데미지) : 4
                                                                                       // 이동속도 : 30
                                                                                       // 방어력(물리방어력) : 3
                                                                                       // 공격속도 : 2
        m_sStatus_Origin.SetSTATUS(m_sStatus);
        m_sStatus_Goaway = new STATUS();
        m_sStatus_Death = new STATUS(0, 0, 5); // 몬스터 토벌 시 획득 가능한 스탯(능력치)
                                               // 현재경험치 + 5
    }

    // 몬스터 피격 시 스탯(능력치) 변동 함수 - 부모 클래스인 Monster_Status의 Attacked() 함수를 사용한다.
    // virtual public bool Attacked(int dm, float dmrate) {ㆍㆍㆍ}

    // 몬스터 피격 시 스탯(능력치(몬스터 현재체력)) 계산 함수 - 부모 클래스인 Monster_Status의 Operator_HP() 함수를 사용한다.
    // bool Operator_HP(int dm) {ㆍㆍㆍ}

    // 몬스터 놓아주기 함수
    override public void Goaway()
    {
        m_bPower = true;
    }

    // 몬스터 리스폰 함수 - 부모 클래스인 Monster_Status의 Respone() 함수를 사용한다.
    // virtual public void Respone() {ㆍㆍㆍ}
}
