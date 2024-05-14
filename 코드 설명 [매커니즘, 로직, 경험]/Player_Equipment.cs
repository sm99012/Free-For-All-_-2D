using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Equipment : MonoBehaviour
{
    public static Item_Equip m_gEquipment_Hat;
    public static Item_Equip m_gEquipment_Top;
    public static Item_Equip m_gEquipment_Bottoms;
    public static Item_Equip m_gEquipment_Shose;
    public static Item_Equip m_gEquipment_Gloves;
    public static Item_Equip m_gEquipment_Mainweapon;
    public static Item_Equip m_gEquipment_Subweapon;

    public static bool m_bEquipment_Hat;
    public static bool m_bEquipment_Top;
    public static bool m_bEquipment_Bottoms;
    public static bool m_bEquipment_Shose;
    public static bool m_bEquipment_Gloves;
    public static bool m_bEquipment_Mainweapon;
    public static bool m_bEquipment_Subweapon;


    STATUS m_sStatus_Effect;
    SOC m_sSoc_Effect;


    public void InitialSet()
    {
        m_gEquipment_Hat = null;
        m_gEquipment_Top = null;
        m_gEquipment_Bottoms = null;
        m_gEquipment_Shose = null;
        m_gEquipment_Gloves = null;
        m_gEquipment_Mainweapon = null;
        m_gEquipment_Subweapon = null;

        m_bEquipment_Hat = false;
        m_bEquipment_Top = false;
        m_bEquipment_Bottoms = false;
        m_bEquipment_Shose = false;
        m_bEquipment_Gloves = false;
        m_bEquipment_Mainweapon = false;
        m_bEquipment_Subweapon = false;

        m_sStatus_Effect = new STATUS();
        m_sSoc_Effect = new SOC();
    }

    //public STATUS UpdateEquipmentStatus()
    //{
    //    InitStatus();
    //    if (m_bEquipment_Hat == true)
    //    {
    //        m_sStatus_Effect.P_OperatorSTATUS(m_gEquipment_Hat.m_sStatus_Effect);
    //    }
    //    if (m_bEquipment_Top == true)
    //    {
    //        m_sStatus_Effect.P_OperatorSTATUS(m_gEquipment_Top.m_sStatus_Effect);
    //    }
    //    if (m_bEquipment_Bottoms == true)
    //    {
    //        m_sStatus_Effect.P_OperatorSTATUS(m_gEquipment_Bottoms.m_sStatus_Effect);
    //    }
    //    if (m_bEquipment_Shose == true)
    //    {
    //        m_sStatus_Effect.P_OperatorSTATUS(m_gEquipment_Shose.m_sStatus_Effect);
    //    }
    //    if (m_bEquipment_Gloves == true)
    //    {
    //        m_sStatus_Effect.P_OperatorSTATUS(m_gEquipment_Gloves.m_sStatus_Effect);
    //    }
    //    if (m_bEquipment_Mainweapon == true)
    //    {
    //        m_sStatus_Effect.P_OperatorSTATUS(m_gEquipment_Mainweapon.m_sStatus_Effect);
    //    }
    //    if (m_bEquipment_Subweapon == true)
    //    {
    //        m_sStatus_Effect.P_OperatorSTATUS(m_gEquipment_Subweapon.m_sStatus_Effect);
    //    }

    //    return m_sStatus_Effect;
    //}
    //void InitStatus()
    //{
    //    m_sStatus_Effect.SetSTATUS_Zero();
    //}

    //public SOC UpdateEquipmentSoc()
    //{
    //    InitSoc();
    //    if (m_bEquipment_Hat == true)
    //    {
    //        m_sSoc_Effect.P_OperatorSOC(m_gEquipment_Hat.m_sSoc_Effect);
    //    }
    //    if (m_bEquipment_Top == true)
    //    {
    //        m_sSoc_Effect.P_OperatorSOC(m_gEquipment_Top.m_sSoc_Effect);
    //    }
    //    if (m_bEquipment_Bottoms == true)
    //    {
    //        m_sSoc_Effect.P_OperatorSOC(m_gEquipment_Bottoms.m_sSoc_Effect);
    //    }
    //    if (m_bEquipment_Shose == true)
    //    {
    //        m_sSoc_Effect.P_OperatorSOC(m_gEquipment_Shose.m_sSoc_Effect);
    //    }
    //    if (m_bEquipment_Gloves == true)
    //    {
    //        m_sSoc_Effect.P_OperatorSOC(m_gEquipment_Gloves.m_sSoc_Effect);
    //    }
    //    if (m_bEquipment_Mainweapon == true)
    //    {
    //        m_sSoc_Effect.P_OperatorSOC(m_gEquipment_Mainweapon.m_sSoc_Effect);
    //    }
    //    if (m_bEquipment_Subweapon == true)
    //    {
    //        m_sSoc_Effect.P_OperatorSOC(m_gEquipment_Subweapon.m_sSoc_Effect);
    //    }

    //    return m_sSoc_Effect;
    //}
    //void InitSoc()
    //{
    //    m_sSoc_Effect.SetSOC_Zero();
    //}

    // 장비 착용

    //
    public void Equip(Item_Equip item)
    {
        switch (item.m_eItemEquipType)
        {
            case E_ITEM_EQUIP_TYPE.HAT:
                {
                    m_gEquipment_Hat = item;
                    m_bEquipment_Hat = true;
                }
                break;
            case E_ITEM_EQUIP_TYPE.TOP:
                {
                    m_gEquipment_Top = item;
                    m_bEquipment_Top = true;
                }
                break;
            case E_ITEM_EQUIP_TYPE.BOTTOMS:
                {
                    m_gEquipment_Bottoms = item;
                    m_bEquipment_Bottoms = true;
                }
                break;
            case E_ITEM_EQUIP_TYPE.SHOSE:
                {
                    m_gEquipment_Shose = item;
                    m_bEquipment_Shose = true;
                }
                break;
            case E_ITEM_EQUIP_TYPE.GLOVES:
                {
                    m_gEquipment_Gloves = item;
                    m_bEquipment_Gloves = true;
                }
                break;
            case E_ITEM_EQUIP_TYPE.MAINWEAPON:
                {
                    m_gEquipment_Mainweapon = item;
                    m_bEquipment_Mainweapon = true;
                }
                break;
            case E_ITEM_EQUIP_TYPE.SUBWEAPON:
                {
                    m_gEquipment_Subweapon = item;
                    m_bEquipment_Subweapon = true;
                }
                break;
        }
    }

    // 장비착용 해제
    public void Remove_Equip(E_ITEM_EQUIP_TYPE miet)
    {
        switch(miet)
        {
            case E_ITEM_EQUIP_TYPE.HAT:
                {
                    m_gEquipment_Hat = null;
                    m_bEquipment_Hat = false;
                } 
                break;
            case E_ITEM_EQUIP_TYPE.TOP:
                {
                    m_gEquipment_Top = null;
                    m_bEquipment_Top = false;
                }
                break;
            case E_ITEM_EQUIP_TYPE.BOTTOMS:
                {
                    m_gEquipment_Bottoms = null;
                    m_bEquipment_Bottoms = false;
                }
                break;
            case E_ITEM_EQUIP_TYPE.SHOSE:
                {
                    m_gEquipment_Shose = null;
                    m_bEquipment_Shose = false;
                }
                break;
            case E_ITEM_EQUIP_TYPE.GLOVES:
                {
                    m_gEquipment_Gloves = null;
                    m_bEquipment_Gloves = false;

                }
                break;
            case E_ITEM_EQUIP_TYPE.MAINWEAPON:
                {
                    m_gEquipment_Mainweapon = null;
                    m_bEquipment_Mainweapon = false;
                }
                break;
            case E_ITEM_EQUIP_TYPE.SUBWEAPON:
                {
                    m_gEquipment_Subweapon = null;
                    m_bEquipment_Subweapon = false;
                }
                break;
        }
    }

    // 착용 무기의 타입 반환
    public E_ITEM_EQUIP_MAINWEAPON_TYPE Get_MainWeaponType()
    {
        return m_gEquipment_Mainweapon.m_eItemEquipMainWeaponType;
    }

    //// 장비 착용 조건 체크
    //public bool CheckCondition_Equip(Item_Equip item, STATUS playerstatus, SOC playersoc)
    //{
    //    if (playerstatus.CheckCondition_Max(item.m_sStatus_Limit_Max) == false)
    //    {
    //        Debug.Log("Status_Max");
    //        return false;
    //    }
    //    if (playerstatus.CheckCondition_Min(item.m_sStatus_Limit_Min) == false)
    //    {
    //        Debug.Log("Status_Min");
    //        return false;
    //    }
    //    if (playersoc.CheckCondition_Max(item.m_sSoc_Limit_Max) == false)
    //    {
    //        Debug.Log("Soc_Max");
    //        return false;
    //    }
    //    if (playersoc.CheckCondition_Min(item.m_sSoc_Limit_Min) == false)
    //    {
    //        Debug.Log("Soc_Min");
    //        return false;
    //    }

    //    return true;
    //}

    public int CheckSetItemEffect(E_ITEM_EQUIP_TYPE eiet)
    {
        switch (eiet)
        {
            case E_ITEM_EQUIP_TYPE.HAT:
                {
                    if (m_bEquipment_Hat == true)
                    {
                        return ItemSetEffectManager.instance.Return_SetItemEffect(m_gEquipment_Hat.m_nItemCode);
                    }
                    else
                        return 0;
                }
            case E_ITEM_EQUIP_TYPE.TOP:
                {
                    if (m_bEquipment_Top == true)
                    {
                        return ItemSetEffectManager.instance.Return_SetItemEffect(m_gEquipment_Top.m_nItemCode);
                    }
                    else
                        return 0;
                }
            case E_ITEM_EQUIP_TYPE.BOTTOMS:
                {
                    if (m_bEquipment_Bottoms == true)
                    {
                        return ItemSetEffectManager.instance.Return_SetItemEffect(m_gEquipment_Bottoms.m_nItemCode);
                    }
                    else
                        return 0;
                }
            case E_ITEM_EQUIP_TYPE.SHOSE:
                {
                    if (m_bEquipment_Shose == true)
                    {
                        return ItemSetEffectManager.instance.Return_SetItemEffect(m_gEquipment_Shose.m_nItemCode);
                    }
                    else
                        return 0;
                }
            case E_ITEM_EQUIP_TYPE.GLOVES:
                {
                    if (m_bEquipment_Gloves == true)
                    {
                        return ItemSetEffectManager.instance.Return_SetItemEffect(m_gEquipment_Gloves.m_nItemCode);
                    }
                    else
                        return 0;
                }
            case E_ITEM_EQUIP_TYPE.MAINWEAPON:
                {
                    if (m_bEquipment_Mainweapon == true)
                    {
                        return ItemSetEffectManager.instance.Return_SetItemEffect(m_gEquipment_Mainweapon.m_nItemCode);
                    }
                    else
                        return 0;
                }
            case E_ITEM_EQUIP_TYPE.SUBWEAPON:
                {
                    if (m_bEquipment_Subweapon == true)
                    {
                        return ItemSetEffectManager.instance.Return_SetItemEffect(m_gEquipment_Subweapon.m_nItemCode);
                    }
                    else
                        return 0;
                }
        }

        return 0;
    }

    // 리트라이 시 사라지는 아이템.
    public bool ReTry_Lost_Item_Equip(Dictionary<int, int> dictionary)
    {
        foreach (KeyValuePair<int, int> item in dictionary)
        {
            Delete_Item_Equip(item.Key);
        }

        return true;
    }
    void Delete_Item_Equip(int itemnumber)
    {
        if (m_bEquipment_Hat == true)
        {
            if (m_gEquipment_Hat.m_nItemNumber == itemnumber)
            {
                m_bEquipment_Hat = false;
                m_gEquipment_Hat = null;

                return;
            }
        }
        if (m_bEquipment_Top == true)
        {
            if (m_gEquipment_Top.m_nItemNumber == itemnumber)
            {
                m_bEquipment_Top = false;
                m_gEquipment_Top = null;

                return;
            }
        }
        if (m_bEquipment_Bottoms == true)
        {
            if (m_gEquipment_Bottoms.m_nItemNumber == itemnumber)
            {
                m_bEquipment_Bottoms = false;
                m_gEquipment_Bottoms = null;

                return;
            }
        }
        if (m_bEquipment_Gloves == true)
        {
            if (m_gEquipment_Gloves.m_nItemNumber == itemnumber)
            {
                m_bEquipment_Gloves = false;
                m_gEquipment_Gloves = null;

                return;
            }
        }
        if (m_bEquipment_Shose == true)
        {
            if (m_gEquipment_Shose.m_nItemNumber == itemnumber)
            {
                m_bEquipment_Shose = false;
                m_gEquipment_Shose = null;

                return;
            }
        }
        if (m_bEquipment_Mainweapon == true)
        {
            if (m_gEquipment_Mainweapon.m_nItemNumber == itemnumber)
            {
                m_bEquipment_Mainweapon = false;
                m_gEquipment_Mainweapon = null;

                return;
            }
        }
        if (m_bEquipment_Subweapon == true)
        {
            if (m_gEquipment_Subweapon.m_nItemNumber == itemnumber)
            {
                m_bEquipment_Subweapon = false;
                m_gEquipment_Subweapon = null;

                return;
            }
        }
    }
}
