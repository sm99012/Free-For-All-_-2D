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
    
    public int m_nMonsterCode;             //몬스터 고유코드
    
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
    public SOC m_sSoc_null;                // null값을 가지는 평판. 놓아주기 실패 시 사용된다.
    public SOC m_sSoc_Goaway;              // 몬스터 놓아주기 시 획득 가능한 스탯(평판)

    public bool m_bDestroy = true;         // 몬스터 현재체력이 0이 되어도 죽지않는 특수 조건 
    public bool m_bDeath = false;          // 몬스터 사망 여부

    // 데미지 출력 오프셋
    protected Vector3 m_vDamageOffSet;

    void Start()
    {
        m_sStatus = new STATUS();
        m_eObject_State = E_OBJECT_STATE.MONSTER;

        InitialSet_SOC();
        InitialSet_STATUS();

        m_vDamageOffSet = new Vector3(0, 0.05f, 0);
    }

    
    void Update()
    {
        
    }

    virtual public void Respone()
    {
        m_sStatus.SetSTATUS(m_sStatus_Origin);
    }

    virtual public void InitialSet_SOC()
    {
        m_sSoc_null = new SOC();
        m_sSoc_Goaway = new SOC();
        m_sSoc_Death = new SOC();
    }

    virtual public void InitialSet_STATUS()
    {
        m_sStatus = new STATUS();
        m_sStatus_Goaway = new STATUS();
        m_sStatus_Death = new STATUS();

        m_sStatus_Origin.SetSTATUS(m_sStatus);
    }

    int m_nDamage;
    float m_fDamage;
    virtual public bool Attacked(int dm, float dmrate)
    {
        if (m_eObject_State == E_OBJECT_STATE.MONSTER)
        {
            // Defence_Physical
            m_fDamage = (int)((float)dm * ((float)m_sStatus.GetSTATUS_Defence_Physical() / ((float)(10) + (float)m_sStatus.GetSTATUS_Defence_Physical())));
            m_nDamage = dm - (int)Mathf.Round(m_fDamage);
            //m_nDamage = dm - m_sStatus.GetSTATUS_Defence_Physical();
            if (m_nDamage <= 0)
                m_nDamage = -1;
            else
                m_nDamage = (int)(-m_nDamage * dmrate);
            m_bDeath = Operator_HP(m_nDamage);
        }
        else if (m_eObject_State == E_OBJECT_STATE.HURDLE)
        {
            m_nDamage = -1;
            m_bDeath = Operator_HP(m_nDamage);
        }
        //GUIManager_Total.Instance.UpdateLog(m_sMonsterName + "이(가) " + Damage.ToString() + " 의 데미지를 입었다.");
        GameObject obj = Resources.Load("Prefab/GUI/TextMesh_Damage") as GameObject;
        GameObject copyobj = Instantiate(obj);
        copyobj.GetComponent<TextMesh_Damage>().InitialSet(this.transform.position + m_vDamageOffSet, -m_nDamage);
            
        if (m_bDeath == true)
            return true;
        else
            return false;
    }

    bool Operator_HP(int dm)
    {
        if (m_sStatus.GetSTATUS_HP_Current() + dm <= 0)
        {
            m_sStatus.SetSTATUS_HP_Current(0);
            if (m_bDestroy == true)
                return true;
            else
                return false;
        }
        else
        {
            m_sStatus.P_OperatorSTATUS_HP_Current(dm);
            //Debug.Log(m_nStatus.GetSTATUS_HP_Current());
            return false;
        }
    }

    virtual public void Goaway()
    {

    }
}
