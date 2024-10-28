using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime3_Sad_Status : Monster_Status // 기반이 되는 Monster_Status 클래스 상속
{
    void Start()
    {
        m_sMonsterName = "슬픈 꼬마 초원 슬라임";
        
        m_nMonsterCode = 2202;
        
        m_eObject_State = E_OBJECT_STATE.MONSTER;
        m_eMonster_Kind = E_MONSTER_KIND.SLIME;

        InitialSet_SOC(); // 몬스터 스탯(평판) 초기 설정
        InitialSet_STATUS(); // 몬스터 스탯(능력치) 초기 설정
    }

    // 몬스터 스탯(평판) 초기 설정
    override public void InitialSet_SOC()
    {
        m_sSoc_null = new SOC();
        m_sSoc_Goaway = new SOC(0, 0, 0, 1, 0, 0, 0, 0, 0); // 몬스터 놓아주기 시 획득 가능한 스탯(평판)
                                                            // 슬라임 종족 평판 + 1
        m_sSoc_Death = new SOC(0, 0, 0, -1, 0, 0, 0, 0, 0); // 몬스터 토벌 시 획득 가능한 스탯(평판)
                                                            // 슬라임 종족 평판 - 1
    }
    // 몬스터 스탯(능력치) 초기 설정
    override public void InitialSet_STATUS()
    {
        m_sStatus = new STATUS(1, 0, 0, 3, 3, 0, 0, 1, 1, 1, 0, 0, 40, 0, 0, 0, 1); // 레벨 : 1
                                                                                    // 최대체력 : 3
                                                                                    // 데미지(총데미지) : 1
                                                                                    // 이동속도 : 40
                                                                                    // 공격속도 : 1
        m_sStatus_Origin.SetSTATUS(m_sStatus);
        m_sStatus_Goaway = new STATUS(0);
        m_sStatus_Death = new STATUS(0, 0, 1); // 몬스터 토벌 시 획득 가능한 스탯(능력치)
                                               // 현재경험치 + 1
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
