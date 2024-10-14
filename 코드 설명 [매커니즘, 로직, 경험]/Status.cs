using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// Free For All 에서 중요한 기획 요소 중 하나인 능력치 시스템을 위한 데이터 타입
//

public struct STATUS
{
    int m_nLV; // 레벨
    
    int m_nEXP_Max;     // 최대경험치(레벨업을 위해 필요한 경험치)
    int m_nEXP_Current; // 현재경험치
    int m_nHP_Max;      // 최대체력
    int m_nHP_Current;  // 현재체력
    int m_nMP_Max;      // 최대마나
    int m_nMP_Current;  // 현재마나

    int m_nDamage_Total;    // 데미지(총데미지)
    int m_nDamage_Physical; // 물리데미지 ※ 미사용
    int m_nDamage_Magical;  // 마법데미지 ※ 미사용
    
    int m_nCriticalRate;   // 크리티컬 확률 ※ 미사용
    int m_nCriticalDamage; // 크리티컬 데미지 ※ 미사용

    int m_nDefence_Physical; // 방어력(물리방어력)
    int m_nDefence_Magical;  // 마법방어력 ※ 미사용
    int m_nEvasionRate;      // 회피율 ※ 미사용
    
    int m_nSpeed;         // 이동속도
    float m_fAttackSpeed; // 공격속도

    // 생성자 - 기본형
    public STATUS(int lv = 0, int exp_max = 0, int exp_cur = 0, int hp_max = 0, int hp_cur = 0,
                  int mp_max = 0, int mp_cur = 0, int dam_total = 0, int dam_physical = 0, int dam_magical = 0,
                  int criticalrate = 0, int criticaldamage = 0, int speed = 0,
                  int def_physical = 0, int def_magical = 0, int evasionrate = 0, float attackspeed = 0)
    {
        this.m_nLV = lv;
        this.m_nEXP_Max = exp_max;
        this.m_nEXP_Current = exp_cur;
        this.m_nHP_Max = hp_max;
        this.m_nHP_Current = hp_cur;
        this.m_nMP_Max = mp_max;
        this.m_nMP_Current = mp_cur;
        this.m_nDamage_Total = dam_total;
        this.m_nDamage_Physical = dam_physical;
        this.m_nDamage_Magical = dam_magical;
        this.m_nCriticalRate = criticalrate;
        this.m_nCriticalDamage = criticaldamage;
        this.m_nSpeed = speed;
        this.m_nDefence_Physical = def_physical;
        this.m_nDefence_Magical = def_magical;
        this.m_nEvasionRate = evasionrate;
        this.m_fAttackSpeed = attackspeed;
    }
    // 생성자 - 복사형
    public STATUS(STATUS status)
    {
        this.m_nLV = status.GetSTATUS_LV();
        this.m_nEXP_Max = status.GetSTATUS_EXP_Max();
        this.m_nEXP_Current = status.GetSTATUS_EXP_Current();
        this.m_nHP_Max = status.GetSTATUS_HP_Max();
        this.m_nHP_Current = status.GetSTATUS_HP_Current();
        this.m_nMP_Max = status.GetSTATUS_MP_Max();
        this.m_nMP_Current = status.GetSTATUS_MP_Current();
        this.m_nDamage_Total = status.GetSTATUS_Damage_Total();
        this.m_nDamage_Physical = status.GetSTATUS_Damage_Physical();
        this.m_nDamage_Magical = status.GetSTATUS_Damage_Magical();
        this.m_nCriticalRate = status.GetSTATUS_CriticalRate();
        this.m_nCriticalDamage = status.GetSTATUS_CriticalDamage();
        this.m_nSpeed = status.GetSTATUS_Speed();
        this.m_nDefence_Physical = status.GetSTATUS_Defence_Physical();
        this.m_nDefence_Magical = status.GetSTATUS_Defence_Magical();
        this.m_nEvasionRate = status.GetSTATUS_EvasionRate();
        this.m_fAttackSpeed = status.GetSTATUS_AttackSpeed();
    }
    // 생성자 - 조건형(최대ㆍ최소 능력치 설정)
    public STATUS(bool bmax)
    {
        if (bmax == true)
        {
            this.m_nLV = 10000;
            this.m_nEXP_Max = 10000;
            this.m_nEXP_Current = 10000;
            this.m_nHP_Max = 10000;
            this.m_nHP_Current = 10000;
            this.m_nMP_Max = 10000;
            this.m_nMP_Current = 10000;
            this.m_nDamage_Total = 10000;
            this.m_nDamage_Physical = 10000;
            this.m_nDamage_Magical = 10000;
            this.m_nCriticalRate = 10000;
            this.m_nCriticalDamage = 10000;
            this.m_nSpeed = 10000;
            this.m_nDefence_Physical = 10000;
            this.m_nDefence_Magical = 10000;
            this.m_nEvasionRate = 10000;
            this.m_fAttackSpeed = 10000;
        }
        else
        {
            this.m_nLV = -10000;
            this.m_nEXP_Max = -10000;
            this.m_nEXP_Current = -10000;
            this.m_nHP_Max = -10000;
            this.m_nHP_Current = -10000;
            this.m_nMP_Max = -10000;
            this.m_nMP_Current = -10000;
            this.m_nDamage_Total = -10000;
            this.m_nDamage_Physical = -10000;
            this.m_nDamage_Magical = -10000;
            this.m_nCriticalRate = -10000;
            this.m_nCriticalDamage = -10000;
            this.m_nSpeed = -10000;
            this.m_nDefence_Physical = -10000;
            this.m_nDefence_Magical = -10000;
            this.m_nEvasionRate = -10000;
            this.m_fAttackSpeed = -10000;
        }
    }

    // 능력치 +- 연산
    public void P_OperatorSTATUS_LV(int value)
    {
        this.m_nLV += value;
    }
    public void P_OperatorSTATUS_EXP_Max(int value)
    {
        this.m_nEXP_Max += value;
    }
    public void P_OperatorSTATUS_EXP_Current(int value)
    {
        this.m_nEXP_Current += value;
    }
    public void P_OperatorSTATUS_HP_Max(int value)
    {
        this.m_nHP_Max += value;
    }
    public void P_OperatorSTATUS_HP_Current(int value)
    {
        this.m_nHP_Current += value;
    }
    public void P_OperatorSTATUS_MP_Max(int value)
    {
        this.m_nMP_Max += value;
    }
    public void P_OperatorSTATUS_MP_Current(int value)
    {
        this.m_nMP_Current += value;
    }
    public void P_OperatorSTATUS_Damage_Total(int value)
    {
        this.m_nDamage_Total += value;
    }
    public void P_OperatorSTATUS_Damage_Physical(int value)
    {
        this.m_nDamage_Physical += value;
    }
    public void P_OperatorSTATUS_Damage_Magical(int value)
    {
        this.m_nDamage_Magical += value;
    }
    public void P_OperatorSTATUS_CriticalRate(int value)
    {
        this.m_nCriticalRate += value;
    }
    public void P_OperatorSTATUS_CriticalDamage(int value)
    {
        this.m_nCriticalDamage += value;
    }
    public void P_OperatorSTATUS_Speed(int value)
    {
        this.m_nSpeed += value;
    }
    public void P_OperatorSTATUS_Defence_Physical(int value)
    {
        this.m_nDefence_Physical += value;
    }
    public void P_OperatorSTATUS_Defence_Magical(int value)
    {
        this.m_nDefence_Magical += value;
    }
    public void P_OperatorSTATUS_EvasionRate(int value)
    {
        this.m_nEvasionRate += value;
    }
    public void P_OperatorSTATUS_AttackSpeed(float value)
    {
        this.m_fAttackSpeed = ((float)Math.Round(this.m_fAttackSpeed + value, 2)); // 공격속도의 경우 소수점 2자리까지 연산
    }
    public void P_OperatorSTATUS(STATUS status)
    {
        this.m_nLV += status.m_nLV;
        this.m_nEXP_Max += status.m_nEXP_Max;
        this.m_nEXP_Current += status.m_nEXP_Current;
        this.m_nHP_Max += status.m_nHP_Max;
        this.m_nHP_Current += status.m_nHP_Current;
        this.m_nMP_Max += status.m_nMP_Max;
        this.m_nMP_Current += status.m_nMP_Current;
        this.m_nDamage_Total += status.m_nDamage_Total;
        this.m_nDamage_Physical += status.m_nDamage_Physical;
        this.m_nDamage_Magical += status.m_nDamage_Magical;
        this.m_nCriticalRate += status.m_nCriticalRate;
        this.m_nCriticalDamage += status.m_nCriticalDamage;
        this.m_nSpeed += status.m_nSpeed;
        this.m_nDefence_Physical += status.m_nDefence_Physical;
        this.m_nDefence_Magical += status.m_nDefence_Magical;
        this.m_nEvasionRate += status.m_nEvasionRate;
        this.m_fAttackSpeed = (float)Math.Round(this.m_fAttackSpeed + status.m_fAttackSpeed, 2);
    }

    // 능력치 */ 연산
    public void M_OperatorSTATUS_LV(float value)
    {
        this.m_nLV = (int)((float)this.m_nLV * value);
    }
    public void M_OperatorSTATUS_EXP_Max(float value)
    {
        this.m_nEXP_Max = (int)((float)this.m_nEXP_Max * value);
    }
    public void M_OperatorSTATUS_EXP_Current(float value)
    {
        this.m_nEXP_Current = (int)((float)this.m_nEXP_Current * value);
    }
    public void M_OperatorSTATUS_HP_Max(float value)
    {
        this.m_nHP_Max = (int)((float)this.m_nHP_Max * value);
    }
    public void M_OperatorSTATUS_HP_Current(float value)
    {
        this.m_nHP_Current = (int)((float)this.m_nHP_Current * value);
    }
    public void M_OperatorSTATUS_MP_Max(float value)
    {
        this.m_nMP_Max = (int)((float)this.m_nMP_Max * value);
    }
    public void M_OperatorSTATUS_MP_Current(float value)
    {
        this.m_nMP_Current = (int)((float)this.m_nMP_Current * value);
    }
    public void M_OperatorSTATUS_Damage_Total(float value)
    {
        this.m_nDamage_Total = (int)((float)this.m_nDamage_Total * value);
    }
    public void M_OperatorSTATUS_Damage_Physical(float value)
    {
        this.m_nDamage_Physical = (int)((float)this.m_nDamage_Physical * value);
    }
    public void M_OperatorSTATUS_Damage_Magical(float value)
    {
        this.m_nDamage_Magical = (int)((float)this.m_nDamage_Magical * value);
    }
    public void M_OperatorSTATUS_CriticalRate(float value)
    {
        this.m_nCriticalRate = (int)((float)this.m_nCriticalRate * value);
    }
    public void M_OperatorSTATUS_CriticalDamage(float value)
    {
        this.m_nCriticalDamage = (int)((float)this.m_nCriticalDamage * value);
    }
    public void M_OperatorSTATUS_Speed(float value)
    {
        this.m_nSpeed = (int)((float)this.m_nSpeed * value);
    }
    public void M_OperatorSTATUS_Defence_Physical(float value)
    {
        this.m_nDefence_Physical = (int)((float)this.m_nDefence_Physical * value);
    }
    public void M_OperatorSTATUS_Defence_Magical(float value)
    {
        this.m_nDefence_Magical = (int)((float)this.m_nDefence_Magical * value);
    }
    public void M_OperatorSTATUS_EvasionRate(float value)
    {
        this.m_nEvasionRate = (int)((float)this.m_nEvasionRate * value);
    }
    public void M_OperatorSTATUS_AttackSpeed(float value)
    {
        this.m_fAttackSpeed = (float)((float)this.m_fAttackSpeed * value);
    }
    public void M_OperatorSTATUS(STATUS status)
    {
        this.m_nLV *= status.m_nLV;
        this.m_nEXP_Max *= status.m_nEXP_Max;
        this.m_nEXP_Current *= status.m_nEXP_Current;
        this.m_nHP_Max *= status.m_nHP_Max;
        this.m_nHP_Current *= status.m_nHP_Current;
        this.m_nMP_Max *= status.m_nMP_Max;
        this.m_nMP_Current *= status.m_nMP_Current;
        this.m_nDamage_Total *= status.m_nDamage_Total;
        this.m_nDamage_Physical *= status.m_nDamage_Physical;
        this.m_nDamage_Magical *= status.m_nDamage_Magical;
        this.m_nCriticalRate *= status.m_nCriticalRate;
        this.m_nCriticalDamage *= status.m_nCriticalDamage;
        this.m_nSpeed *= status.m_nSpeed;
        this.m_nDefence_Physical *= status.m_nDefence_Physical;
        this.m_nDefence_Magical *= status.m_nDefence_Magical;
        this.m_nEvasionRate *= status.m_nEvasionRate;
        this.m_fAttackSpeed *= status.m_fAttackSpeed;
    }

    // STATUS 연산자 오버로딩. 현재 최적화로 인해 사용하지 않는다.
    public static STATUS operator +(STATUS status_origin, STATUS status_add)
    {
        return new STATUS(status_origin.GetSTATUS_LV() + status_add.GetSTATUS_LV(), status_origin.GetSTATUS_EXP_Max() + status_add.GetSTATUS_EXP_Max(), status_origin.GetSTATUS_EXP_Current() + status_add.GetSTATUS_EXP_Current(), status_origin.GetSTATUS_HP_Max() + status_add.GetSTATUS_HP_Max(), status_origin.GetSTATUS_HP_Current() + status_add.GetSTATUS_HP_Current(),
            status_origin.GetSTATUS_MP_Max() + status_add.GetSTATUS_MP_Max(), status_origin.GetSTATUS_MP_Current() + status_add.GetSTATUS_MP_Current(), status_origin.GetSTATUS_Damage_Total() + status_add.GetSTATUS_Damage_Total(), status_origin.GetSTATUS_Damage_Physical() + status_add.GetSTATUS_Damage_Physical(), status_origin.GetSTATUS_Damage_Magical() + status_add.GetSTATUS_Damage_Magical(),
            status_origin.GetSTATUS_CriticalRate() + status_add.GetSTATUS_CriticalRate(), status_origin.GetSTATUS_CriticalDamage() + status_add.GetSTATUS_CriticalDamage(), status_origin.GetSTATUS_Speed() + status_add.GetSTATUS_Speed(),
            status_origin.GetSTATUS_Defence_Physical() + status_add.GetSTATUS_Defence_Physical(), status_origin.GetSTATUS_Defence_Magical() + status_add.GetSTATUS_Defence_Magical(), status_origin.GetSTATUS_EvasionRate() + status_add.GetSTATUS_EvasionRate(), status_origin.GetSTATUS_AttackSpeed() + status_add.GetSTATUS_AttackSpeed());
    }
    public static STATUS operator -(STATUS status_origin, STATUS status_add)
    {
        return new STATUS(status_origin.GetSTATUS_LV() - status_add.GetSTATUS_LV(), status_origin.GetSTATUS_EXP_Max() - status_add.GetSTATUS_EXP_Max(), status_origin.GetSTATUS_EXP_Current() - status_add.GetSTATUS_EXP_Current(), status_origin.GetSTATUS_HP_Max() - status_add.GetSTATUS_HP_Max(), status_origin.GetSTATUS_HP_Current() - status_add.GetSTATUS_HP_Current(),
            status_origin.GetSTATUS_MP_Max() - status_add.GetSTATUS_MP_Max(), status_origin.GetSTATUS_MP_Current() - status_add.GetSTATUS_MP_Current(), status_origin.GetSTATUS_Damage_Total() - status_add.GetSTATUS_Damage_Total(), status_origin.GetSTATUS_Damage_Physical() - status_add.GetSTATUS_Damage_Physical(), status_origin.GetSTATUS_Damage_Magical() - status_add.GetSTATUS_Damage_Magical(),
            status_origin.GetSTATUS_CriticalRate() - status_add.GetSTATUS_CriticalRate(), status_origin.GetSTATUS_CriticalDamage() - status_add.GetSTATUS_CriticalDamage(), status_origin.GetSTATUS_Speed() - status_add.GetSTATUS_Speed(),
            status_origin.GetSTATUS_Defence_Physical() - status_add.GetSTATUS_Defence_Physical(), status_origin.GetSTATUS_Defence_Magical() - status_add.GetSTATUS_Defence_Magical(), status_origin.GetSTATUS_EvasionRate() - status_add.GetSTATUS_EvasionRate(), status_origin.GetSTATUS_AttackSpeed() - status_add.GetSTATUS_AttackSpeed());
    }
    public static STATUS operator *(STATUS status_origin, STATUS status_add)
    {
        return new STATUS(status_origin.GetSTATUS_LV() * status_add.GetSTATUS_LV(), status_origin.GetSTATUS_EXP_Max() * status_add.GetSTATUS_EXP_Max(), status_origin.GetSTATUS_EXP_Current() * status_add.GetSTATUS_EXP_Current(), status_origin.GetSTATUS_HP_Max() * status_add.GetSTATUS_HP_Max(), status_origin.GetSTATUS_HP_Current() * status_add.GetSTATUS_HP_Current(),
            status_origin.GetSTATUS_MP_Max() * status_add.GetSTATUS_MP_Max(), status_origin.GetSTATUS_MP_Current() * status_add.GetSTATUS_MP_Current(), status_origin.GetSTATUS_Damage_Total() * status_add.GetSTATUS_Damage_Total(), status_origin.GetSTATUS_Damage_Physical() * status_add.GetSTATUS_Damage_Physical(), status_origin.GetSTATUS_Damage_Magical() * status_add.GetSTATUS_Damage_Magical(),
            status_origin.GetSTATUS_CriticalRate() * status_add.GetSTATUS_CriticalRate(), status_origin.GetSTATUS_CriticalDamage() * status_add.GetSTATUS_CriticalDamage(), status_origin.GetSTATUS_Speed() * status_add.GetSTATUS_Speed(),
            status_origin.GetSTATUS_Defence_Physical() * status_add.GetSTATUS_Defence_Physical(), status_origin.GetSTATUS_Defence_Magical() * status_add.GetSTATUS_Defence_Magical(), status_origin.GetSTATUS_EvasionRate() * status_add.GetSTATUS_EvasionRate(), status_origin.GetSTATUS_AttackSpeed() * status_add.GetSTATUS_AttackSpeed());
    }
    public static STATUS operator /(STATUS status_origin, STATUS status_add)
    {
        return new STATUS(status_origin.GetSTATUS_LV() / status_add.GetSTATUS_LV(), status_origin.GetSTATUS_EXP_Max() / status_add.GetSTATUS_EXP_Max(), status_origin.GetSTATUS_EXP_Current() / status_add.GetSTATUS_EXP_Current(), status_origin.GetSTATUS_HP_Max() / status_add.GetSTATUS_HP_Max(), status_origin.GetSTATUS_HP_Current() / status_add.GetSTATUS_HP_Current(),
            status_origin.GetSTATUS_MP_Max() / status_add.GetSTATUS_MP_Max(), status_origin.GetSTATUS_MP_Current() / status_add.GetSTATUS_MP_Current(), status_origin.GetSTATUS_Damage_Total() / status_add.GetSTATUS_Damage_Total(), status_origin.GetSTATUS_Damage_Physical() / status_add.GetSTATUS_Damage_Physical(), status_origin.GetSTATUS_Damage_Magical() / status_add.GetSTATUS_Damage_Magical(),
            status_origin.GetSTATUS_CriticalRate() / status_add.GetSTATUS_CriticalRate(), status_origin.GetSTATUS_CriticalDamage() / status_add.GetSTATUS_CriticalDamage(), status_origin.GetSTATUS_Speed() / status_add.GetSTATUS_Speed(),
            status_origin.GetSTATUS_Defence_Physical() / status_add.GetSTATUS_Defence_Physical(), status_origin.GetSTATUS_Defence_Magical() / status_add.GetSTATUS_Defence_Magical(), status_origin.GetSTATUS_EvasionRate() / status_add.GetSTATUS_EvasionRate(), status_origin.GetSTATUS_AttackSpeed() / status_add.GetSTATUS_AttackSpeed());
    }
    
    // 능력치 설정
    public void SetSTATUS_LV(int value)
    {
        this.m_nLV = value;
    }
    public void SetSTATUS_EXP_Max(int value)
    {
        this.m_nEXP_Max = value;
    }
    public void SetSTATUS_EXP_Current(int value)
    {
        this.m_nEXP_Current = value;
    }
    public void SetSTATUS_HP_Max(int value)
    {
        this.m_nHP_Max = value;
    }
    public void SetSTATUS_HP_Current(int value)
    {
        this.m_nHP_Current = value;
    }
    public void SetSTATUS_MP_Max(int value)
    {
        this.m_nMP_Max = value;
    }
    public void SetSTATUS_MP_Current(int value)
    {
        this.m_nMP_Current = value;
    }
    public void SetSTATUS_Damage_Total(int value)
    {
        this.m_nDamage_Total = value;
    }
    public void SetSTATUS_Damage_Physical(int value)
    {
        this.m_nDamage_Physical = value;
    }
    public void SetSTATUS_Damage_Magical(int value)
    {
        this.m_nDamage_Magical = value;
    }
    public void SetSTATUS_CriticalRate(int value)
    {
        this.m_nCriticalRate = value;
    }
    public void SetSTATUS_CriticalDamage(int value)
    {
        this.m_nCriticalDamage = value;
    }
    public void SetSTATUS_Speed(int value)
    {
        this.m_nSpeed = value;
    }
    public void SetSTATUS_Defence_Physical(int value)
    {
        this.m_nDefence_Physical = value;
    }
    public void SetSTATUS_Defence_Magical(int value)
    {
        this.m_nDefence_Magical = value;
    }
    public void SetSTATUS_EvasionRate(int value)
    {
        this.m_nEvasionRate = value;
    }
    public void SetSTATUS_AttackSpeed(float value)
    {
        this.m_fAttackSpeed = value;
    }
    public void SetSTATUS(STATUS status)
    {
        this.m_nLV = status.m_nLV;
        this.m_nEXP_Max = status.m_nEXP_Max;
        this.m_nEXP_Current = status.m_nEXP_Current;
        this.m_nHP_Max = status.m_nHP_Max;
        this.m_nHP_Current = status.m_nHP_Current;
        this.m_nMP_Max = status.m_nMP_Max;
        this.m_nMP_Current = status.m_nMP_Current;
        this.m_nDamage_Total = status.m_nDamage_Total;
        this.m_nDamage_Physical = status.m_nDamage_Physical;
        this.m_nDamage_Magical = status.m_nDamage_Magical;
        this.m_nCriticalRate = status.m_nCriticalRate;
        this.m_nCriticalDamage = status.m_nCriticalDamage;
        this.m_nSpeed = status.m_nSpeed;
        this.m_nDefence_Physical = status.m_nDefence_Physical;
        this.m_nDefence_Magical = status.m_nDefence_Magical;
        this.m_nEvasionRate = status.m_nEvasionRate;
        this.m_fAttackSpeed = status.m_fAttackSpeed;
    }
    public void SetSTATUS_Zero()
    {
        this.m_nLV = 0;
        this.m_nEXP_Max = 0;
        this.m_nEXP_Current = 0;
        this.m_nHP_Max = 0;
        this.m_nHP_Current = 0;
        this.m_nMP_Max = 0;
        this.m_nMP_Current = 0;
        this.m_nDamage_Total = 0;
        this.m_nDamage_Physical = 0;
        this.m_nDamage_Magical = 0;
        this.m_nCriticalRate = 0;
        this.m_nCriticalDamage = 0;
        this.m_nSpeed = 0;
        this.m_nDefence_Physical = 0;
        this.m_nDefence_Magical = 0;
        this.m_nEvasionRate = 0;
        this.m_fAttackSpeed = 0;
    }

    // 능력치 반환
    public int GetSTATUS_LV()
    {
        return m_nLV;
    }
    public int GetSTATUS_EXP_Max()
    {
        return m_nEXP_Max;
    }
    public int GetSTATUS_EXP_Current()
    {
        return m_nEXP_Current;
    }
    public int GetSTATUS_HP_Max()
    {
        return m_nHP_Max;
    }
    public int GetSTATUS_HP_Current()
    {
        return m_nHP_Current;
    }
    public int GetSTATUS_MP_Max()
    {
        return m_nMP_Max;
    }
    public int GetSTATUS_MP_Current()
    {
        return m_nMP_Current;
    }
    public int GetSTATUS_Damage_Total()
    {
        return m_nDamage_Total;
    }
    public int GetSTATUS_Damage_Physical()
    {
        return m_nDamage_Physical;
    }
    public int GetSTATUS_Damage_Magical()
    {
        return m_nDamage_Magical;
    }
    public int GetSTATUS_CriticalRate()
    {
        return m_nCriticalRate;
    }
    public int GetSTATUS_CriticalDamage()
    {
        return m_nCriticalDamage;
    }
    public int GetSTATUS_Speed()
    {
        return m_nSpeed;
    }
    public int GetSTATUS_Defence_Physical()
    {
        return m_nDefence_Physical;
    }
    public int GetSTATUS_Defence_Magical()
    {
        return m_nDefence_Magical;
    }
    public int GetSTATUS_EvasionRate()
    {
        return m_nEvasionRate;
    }
    public float GetSTATUS_AttackSpeed()
    {
        return m_fAttackSpeed;
    }
    public STATUS GetSTATUS()
    {
        return this;
    }

    // 능력치 조건 판단(하한)
    // return true : 능력치 조건 충족 / return false : 능력치 조건 미흡
    public bool CheckCondition_Min(STATUS status)
    {
        if (this.m_nLV < status.m_nLV) return false;
        if (this.m_nEXP_Max < status.m_nEXP_Max) return false;
        if (this.m_nEXP_Current < status.m_nEXP_Current) return false;
        if (this.m_nHP_Max < status.m_nHP_Max) return false;
        if (this.m_nHP_Current < status.m_nHP_Current) return false;
        if (this.m_nMP_Max < status.m_nMP_Max) return false;
        if (this.m_nMP_Current < status.m_nMP_Current) return false;
        if (this.m_nDamage_Total < status.m_nDamage_Total) return false;
        if (this.m_nDamage_Physical < status.m_nDamage_Physical) return false;
        if (this.m_nDamage_Magical < status.m_nDamage_Magical) return false;
        if (this.m_nCriticalRate < status.m_nCriticalRate) return false;
        if (this.m_nCriticalDamage < status.m_nCriticalDamage) return false;
        if (this.m_nSpeed < status.m_nSpeed) return false;
        if (this.m_nDefence_Physical < status.m_nDefence_Physical) return false;
        if (this.m_nDefence_Magical < status.m_nDefence_Magical) return false;
        if (this.m_nEvasionRate < status.m_nEvasionRate) return false;
        if (this.m_fAttackSpeed < status.m_fAttackSpeed) return false;

        return true;
    }    
    // 능력치 조건 판단(상한)
    // return true : 능력치 조건 충족 / return false : 능력치 조건 미흡
    public bool CheckCondition_Max(STATUS status)
    {
        if (this.m_nLV > status.m_nLV) return false;
        if (this.m_nEXP_Max > status.m_nEXP_Max) return false;
        if (this.m_nEXP_Current > status.m_nEXP_Current) return false;
        if (this.m_nHP_Max > status.m_nHP_Max) return false;
        if (this.m_nHP_Current > status.m_nHP_Current) return false;
        if (this.m_nMP_Max > status.m_nMP_Max) return false;
        if (this.m_nMP_Current > status.m_nMP_Current) return false;
        if (this.m_nDamage_Total > status.m_nDamage_Total) return false;
        if (this.m_nDamage_Physical > status.m_nDamage_Physical) return false;
        if (this.m_nDamage_Magical > status.m_nDamage_Magical) return false;
        if (this.m_nCriticalRate > status.m_nCriticalRate) return false;
        if (this.m_nCriticalDamage > status.m_nCriticalDamage) return false;
        if (this.m_nSpeed > status.m_nSpeed) return false;
        if (this.m_nDefence_Physical > status.m_nDefence_Physical) return false;
        if (this.m_nDefence_Magical > status.m_nDefence_Magical) return false;
        if (this.m_nEvasionRate > status.m_nEvasionRate) return false;
        if (this.m_fAttackSpeed > status.m_fAttackSpeed) return false;

        return true;
    }
    // 능력치 조건 판단(하한 + 상한)
    // return true : 능력치 조건 충족 / return false : 능력치 조건 미흡
    public bool CheckCondition_MM_LV(int minvalue, int maxvalue) // minvalue : 하한, maxvalue : 상한
    {
        if (this.m_nLV < minvalue) return false;
        else
        {
            if (this.m_nLV > maxvalue) return false;
            else return true;
        }
    }
    public bool CheckCondition_MM_HP_Max(int minvalue, int maxvalue)
    {
        if (this.m_nHP_Max < minvalue) return false;
        else
        {
            if (this.m_nHP_Max > maxvalue) return false;
            else return true;
        }
    }
    public bool CheckCondition_MM_MP_Max(int minvalue, int maxvalue)
    {
        if (this.m_nMP_Max < minvalue) return false;
        else
        {
            if (this.m_nMP_Max > maxvalue) return false;
            else return true;
        }
    }
    public bool CheckCondition_MM_Damage_Total(int minvalue, int maxvalue)
    {
        if (this.m_nDamage_Total < minvalue) return false;
        else
        {
            if (this.m_nDamage_Total > maxvalue) return false;
            else return true;
        }
    }
    public bool CheckCondition_MM_Defence_Phisical(int minvalue, int maxvalue)
    {
        if (this.m_nDefence_Physical < minvalue) return false;
        else
        {
            if (this.m_nDefence_Physical > maxvalue) return false;
            else return true;
        }
    }
    public bool CheckCondition_MM_Speed(int minvalue, int maxvalue)
    {
        if (this.m_nSpeed < minvalue) return false;
        else
        {
            if (this.m_nSpeed > maxvalue) return false;
            else return true;
        }
    }
    public bool CheckCondition_MM_AttackSpeed(float minvalue, float maxvalue)
    {
        if (this.m_fAttackSpeed < minvalue) return false;
        else
        {
            if (this.m_fAttackSpeed > maxvalue) return false;
            else return true;
        }
    }

    // 현재체력 비율(%) 반환
    public int GetSTATUS_HP_Ratio()
    {
        return (int)(((float)m_nHP_Current / (float)m_nHP_Max) * 100);
    }
    // 현재마나 비율(%) 반환
    public int GetSTATUS_MP_Ratio()
    {
        return (int)(((float)m_nMP_Current / (float)m_nMP_Max) * 100);
    }

    // 능력치 동일성 판단
    // return true : 두 능력치가 동일한 수치를 가진다. / return false : 두 능력치가 다른 수치를 가진다.
    public bool CheckIdentity(STATUS status)
    {
        if (this.m_nLV != status.m_nLV)
            return false;
        if (this.m_nEXP_Max != status.m_nEXP_Max)
            return false;
        if (this.m_nEXP_Current != status.m_nEXP_Current)
            return false;
        if (this.m_nHP_Max != status.m_nHP_Max)
            return false;
        if (this.m_nHP_Current != status.m_nHP_Current)
            return false;
        if (this.m_nMP_Max != status.m_nMP_Max)
            return false;
        if (this.m_nMP_Current != status.m_nMP_Current)
            return false;
        if (this.m_nDamage_Total != status.m_nDamage_Total)
            return false;
        if (this.m_nDamage_Physical != status.m_nDamage_Physical)
            return false;
        if (this.m_nDamage_Magical != status.m_nDamage_Magical)
            return false;
        if (this.m_nCriticalRate != status.m_nCriticalRate)
            return false;
        if (this.m_nCriticalDamage != status.m_nCriticalDamage)
            return false;
        if (this.m_nSpeed != status.m_nSpeed)
            return false;
        if (this.m_nDefence_Physical != status.m_nDefence_Physical)
            return false;
        if (this.m_nDefence_Magical != status.m_nDefence_Magical)
            return false;
        if (this.m_nEvasionRate != status.m_nEvasionRate)
            return false;
        //if (this.m_fAttackSpeed != status.m_fAttackSpeed)
        //    return false;

        return true;
    }

    // 능력치 데이터 저장에 사용되는 함수
    public string GetSTATUS_Data()
    {
        string str = "";

        str = m_nLV.ToString() + ", " + m_nEXP_Max.ToString() + ", " + m_nEXP_Current.ToString() + ", " +
            m_nHP_Max.ToString() + ", " + m_nHP_Current.ToString() + ", " + m_nMP_Max.ToString() + ", " + m_nMP_Current.ToString() + ", " +
            m_nDamage_Total.ToString() + ", " + m_nDamage_Physical.ToString() + ", " + m_nDamage_Magical.ToString() + ", " +
            m_nCriticalRate.ToString() + ", " + m_nCriticalDamage.ToString() + ", " + m_nSpeed.ToString() + ", " +
            m_nDefence_Physical.ToString() + ", " + m_nDefence_Magical.ToString() + ", " + m_nEvasionRate.ToString() + ", " + m_fAttackSpeed.ToString();

        return str;
    }
}
