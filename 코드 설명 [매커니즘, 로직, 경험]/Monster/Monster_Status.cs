using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//
// ※ 몬스터의 스탯(능력치, 평판)을 관리하는 Monster_Status 기반 클래스를 구현한 후 각종 몬스터의 ㆍㆍㆍ_Status 클래스를 상속으로 구현했다.
//    가상 함수를 이용해 몬스터의 특징에 따른 적절한 동작을 구현했다.
//

// 오브젝트 타입 : { 몬스터, 오브젝트(장애물(모든 피격 데미지 1 고정)) }
public enum E_OBJECT_STATE { MONSTER, HURDLE }
// 몬스터 타입 : { 오브젝트, 인간, 동물, 슬라임, 스켈레톤, 앤트, 마족, 용족, 어둠 }
public enum E_MONSTER_KIND { OBJECT, HUMAN, ANIMAL, SLIME, SKELETON, ENTS, DEVIL, DRAGON, SHADOW }

public class Monster_Status : MonoBehaviour
{ 
    public string m_sMonsterName;          // 몬스터 이름
    
    public int m_nMonsterCode;             // 몬스터 고유코드
    
    public E_OBJECT_STATE m_eObject_State; // 오브젝트 타입
    public E_MONSTER_KIND m_eMonster_Kind; // 몬스터 타입

    // 몬스터 스탯(능력치) 관련 변수
    public STATUS m_sStatus_Origin;        // 기본 몬스터 스탯(능력치)
    public STATUS m_sStatus;               // 합계 몬스터 스탯(능력치)

    // 몬스터 토벌 시 획득 가능한 스탯(능력치, 평판)
    public STATUS m_sStatus_Death;         // 몬스터 토벌 시 획득 가능한 스탯(능력치(경험치 + @))
    public SOC m_sSoc_Death;               // 몬스터 토벌 시 획득 가능한 스탯(평판)
    // 몬스터 놓아주기 시 획득 가능한 스탯(능력치, 평판)
    public STATUS m_sStatus_Goaway;        // 몬스터 놓아주기 시 획득 가능한 스탯(능력치(경험치 + @))
    public SOC m_sSoc_null;                // null값을 가지는 스탯(평판). 놓아주기 실패 시 사용된다.
    public SOC m_sSoc_Goaway;              // 몬스터 놓아주기 시 획득 가능한 스탯(평판)

    public bool m_bDestroy = true;         // 몬스터 현재체력이 0 이 되어도 죽지않는 특수 조건 
    public bool m_bDeath = false;          // 몬스터 사망 여부

    protected Vector3 m_vDamageOffSet;     // 몬스터 별 데미지 출력 오프셋
    int m_nDamage;                         // 몬스터 피격 데미지(출력 전용. 소숫점 반올림)
    float m_fDamage;                       // 몬스터 피격 데미지(소숫점 계산 가능)

    void Start()
    {
        m_eObject_State = E_OBJECT_STATE.MONSTER;
        
        m_sStatus = new STATUS();

        InitialSet_SOC(); // 몬스터 스탯(평판) 초기 설정
        InitialSet_STATUS(); // 몬스터 스탯(능력치) 초기 설정

        m_vDamageOffSet = new Vector3(0, 0.05f, 0); // 몬스터 피격 데미지 출력 오프셋 설정
    }

    // 몬스터 스탯(평판) 초기 설정
    virtual public void InitialSet_SOC()
    {
        m_sSoc_null = new SOC();
        m_sSoc_Goaway = new SOC(); // 몬스터 놓아주기 시 획득 가능한 스탯(평판)
        m_sSoc_Death = new SOC();  // 몬스터 토벌 시 획득 가능한 스탯(평판)
    }
    // 몬스터 스탯(능력치) 초기 설정
    virtual public void InitialSet_STATUS()
    {
        m_sStatus = new STATUS();
        m_sStatus_Goaway = new STATUS(); // 몬스터 놓아주기 시 획득 가능한 스탯(능력치)
        m_sStatus_Death = new STATUS();  // 몬스터 토벌 시 획득 가능한 스탯(능력치)

        m_sStatus_Origin.SetSTATUS(m_sStatus);
    }

    // 몬스터 피격 시 스탯(능력치) 변동 함수
    // return true : 몬스터 사망 / return false : 몬스터 생존
    virtual public bool Attacked(int dm, float dmrate) // dm : 피격 데미지, dmrate : 피격 데미지 계수
    {
        // 오브젝트 타입 == 몬스터 : 피격 데미지 계산
        if (m_eObject_State == E_OBJECT_STATE.MONSTER)
        {
            // 몬스터 피격 시 피격 데미지 계산
            // 데미지, 방어력 계산 공식 : 데미지 * (물리방어력 / (10(방어력 계수) + 물리방어력)). ※ 데미지, 방어력 계산 공식은 추후 변경될 수 있다.(단순 데미지 - 방어력 -> 방어력 계수 적용)
            m_fDamage = (int)((float)dm * ((float)m_sStatus.GetSTATUS_Defence_Physical() / ((float)(10) + (float)m_sStatus.GetSTATUS_Defence_Physical())));
            m_nDamage = dm - (int)Mathf.Round(m_fDamage); // 데미지의 소숫점 제거(반올림)
            if (m_nDamage <= 0)
                m_nDamage = -1; // 최소 피격 데미지 = -1
            else
                m_nDamage = (int)(-m_nDamage * dmrate); // 피격 데미지 계수 적용(플레이어 착용 무기 분류별 데미지 계수 적용)
            m_bDeath = Operator_HP(m_nDamage); //
        }
        // 오브젝트 타입 == 오브젝트(장애물) : 모든 피격 데미지 1 고정
        else if (m_eObject_State == E_OBJECT_STATE.HURDLE)
        {
            m_nDamage = -1;
            m_bDeath = Operator_HP(m_nDamage);
        }
        // 몬스터 피격 데미지 출력
        GameObject obj = Resources.Load("Prefab/GUI/TextMesh_Damage") as GameObject;
        GameObject copyobj = Instantiate(obj);
        copyobj.GetComponent<TextMesh_Damage>().InitialSet(this.transform.position + m_vDamageOffSet, -m_nDamage);
            
        if (m_bDeath == true)
            return true;
        else
            return false;
    }

    // 몬스터 피격 시 스탯(능력치(몬스터 현재체력)) 계산 함수
    // return true : 몬스터 사망 / return false : 몬스터 생존
    bool Operator_HP(int dm) // dm : 피격 데미지
    {
        if (m_sStatus.GetSTATUS_HP_Current() + dm <= 0) // 몬스터 사망
        {
            m_sStatus.SetSTATUS_HP_Current(0);
            if (m_bDestroy == true)
                return true;
            else
                return false;
        }
        else // 몬스터 생존
        {
            m_sStatus.P_OperatorSTATUS_HP_Current(dm);
            return false;
        }
    }

    // 몬스터 놓아주기 관련 함수
    virtual public void Goaway()
    {
        
    }

    // 몬스터 리스폰 함수
    virtual public void Respone()
    {
        m_sStatus.SetSTATUS(m_sStatus_Origin);
    }
}
