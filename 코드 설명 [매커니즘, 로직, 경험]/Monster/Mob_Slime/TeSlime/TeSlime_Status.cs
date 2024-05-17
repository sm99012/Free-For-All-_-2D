using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeSlime_Status : MonoBehaviour
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

    public void InitialSet()
    {
        m_eObject_State = E_OBJECT_STATE.MONSTER;
        m_eMonster_Kind = E_MONSTER_KIND.SLIME;

        InitialSet_STATUS();
        InitialSet_SOC();

        m_sMonsterName = "테 슬라임";
        m_nMonsterCode = 10001;
    }

    void InitialSet_STATUS()
    {
        m_sStatus = new STATUS(21, 0, 400, 2000, 2000, 200, 200, 17, 17, 17, 20, 10, 40, 15, 15, 0, 5f);

        m_sStatus_Origin.SetSTATUS(m_sStatus);
        m_sStatus_Goaway = new STATUS();
        m_sStatus_Death = new STATUS(0, 0, 400);
    }

    void InitialSet_SOC()
    {
        m_sSoc_null = new SOC();
        m_sSoc_Goaway = new SOC();
        m_sSoc_Death = new SOC(10, 0, 0, 10, 0, 10, 0, 0, -1);
    }

    int m_nTotalDamage;
    int m_fTotalDamage;
    public bool Attacked(int dm, float dmrate)
    {
        if (m_bPower == false)
        {
            if (m_eObject_State == E_OBJECT_STATE.MONSTER)
            {
                // Defence_Physical
                //m_nDamage = dm - m_sStatus.GetSTATUS_Defence_Physical();
                m_fTotalDamage = (int)((float)dm * ((float)m_sStatus.GetSTATUS_Defence_Physical() / ((float)(10) + (float)m_sStatus.GetSTATUS_Defence_Physical())));
                m_nTotalDamage = dm - (int)Mathf.Round(m_fTotalDamage);

                if (m_nTotalDamage <= 0)
                    m_nTotalDamage = -1;
                else
                    m_nTotalDamage = (int)(-m_nTotalDamage * dmrate);
                m_bDeath = Operator_HP(m_nTotalDamage);
            }
            else if (m_eObject_State == E_OBJECT_STATE.HURDLE)
            {
                m_nTotalDamage = -1;
                m_bDeath = Operator_HP(m_nTotalDamage);
            }
            //GUIManager_Total.Instance.UpdateLog(m_sMonsterName + "이(가) " + Damage.ToString() + " 의 데미지를 입었다.");
            GameObject obj = Resources.Load("Prefab/GUI/TextMesh_Damage") as GameObject;
            GameObject copyobj = Instantiate(obj);
            copyobj.GetComponent<TextMesh_Damage>().InitialSet(this.transform.position + m_vDamageOffSet, -m_nTotalDamage);
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

    public void Set_Phase1()
    {
        m_sStatus.SetSTATUS_Speed(40);
        m_sStatus.SetSTATUS_AttackSpeed(5f);
    }
    public void Set_Phase2()
    {
        m_sStatus.SetSTATUS_Speed(40);
        m_sStatus.SetSTATUS_AttackSpeed(3f);
    }
    public void Set_Phase3()
    {
        m_sStatus.SetSTATUS_Speed(60);
        m_sStatus.SetSTATUS_AttackSpeed(1.5f);
    }

    public bool UseSkill(int usehp = 0, int usemp = 0)
    {
        if (m_sStatus.GetSTATUS_HP_Current() - usehp < 0)
            return false;
        else
            m_sStatus.P_OperatorSTATUS_HP_Current(-usehp);

        if (m_sStatus.GetSTATUS_MP_Current() - usemp < 0)
            return false;
        else
            m_sStatus.P_OperatorSTATUS_MP_Current(-usemp);

        return true;
    }
}
