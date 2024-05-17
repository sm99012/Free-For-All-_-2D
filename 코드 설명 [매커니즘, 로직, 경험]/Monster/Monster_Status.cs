using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum E_OBJECT_STATE { MONSTER, HURDLE }
public enum E_MONSTER_KIND { OBJECT, HUMAN, ANIMAL, SLIME, SKELETON, ENTS, DEVIL, DRAGON, SHADOW }

public class Monster_Status : MonoBehaviour
{ 
    public E_OBJECT_STATE m_eObject_State;

    public E_MONSTER_KIND m_eMonster_Kind;

    public SOC m_sSoc_null;
    public SOC m_sSoc_Goaway;
    public SOC m_sSoc_Death;

    public int m_nMonsterCode;
    public string m_sMonsterName;

    // 공격속도 > 0.5f
    // 공격속도 최소치 = 0.5f
    public STATUS m_sStatus_Origin;
    public STATUS m_sStatus;
    public STATUS m_sStatus_Goaway;
    public STATUS m_sStatus_Death;

    // 체력이 0 이 되어도 죽지않는 조건 추가 (좀비몹) 
    public bool m_bDestroy = true;
    // 죽음?
    public bool m_bDeath = false;
    // 무적상태
    public bool m_bPower = false;

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
        m_bPower = false;
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
        if (m_bPower == false)
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
        }
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