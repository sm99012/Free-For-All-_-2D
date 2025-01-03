﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Equipment : MonoBehaviour
{
    // 플레이어가 착용중인 장비아이템
    public static Item_Equip m_gEquipment_Hat;        // 착용중인 장비아이템(모자)
    public static Item_Equip m_gEquipment_Top;        // 착용중인 장비아이템(상의)
    public static Item_Equip m_gEquipment_Bottoms;    // 착용중인 장비아이템(하의)
    public static Item_Equip m_gEquipment_Shose;      // 착용중인 장비아이템(신발)
    public static Item_Equip m_gEquipment_Gloves;     // 착용중인 장비아이템(장갑)
    public static Item_Equip m_gEquipment_Mainweapon; // 착용중인 장비아이템(주무기)
    public static Item_Equip m_gEquipment_Subweapon;  // 착용중인 장비아이템(보조무기)

    // 플레이어의 장비아이템 착용 현황
    public static bool m_bEquipment_Hat;        // 모자
    public static bool m_bEquipment_Top;        // 상의
    public static bool m_bEquipment_Bottoms;    // 하의
    public static bool m_bEquipment_Shose;      // 신발
    public static bool m_bEquipment_Gloves;     // 장갑
    public static bool m_bEquipment_Mainweapon; // 주무기
    public static bool m_bEquipment_Subweapon;  // 보조무기
    
    //
    // ※ Item_Equip 클래스를 만들어 장비아이템을 구현했다. 그 과정에서 Unity의 null 기능을 사용할 수 없었기에(Item_Equip데이터가 할당되어 있어도 Unity엔진이 null로 인식) 추가로 bool변수를 이용했다.
    //

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
    }
    
    // 장비아이템 착용
    public void Equip(Item_Equip item) // 착용할 장비아이템
    {
        switch (item.m_eItemEquipType) // 착용할 장비아이템의 분류별 장비아이템 착용
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

    // 장비아이템 해제
    public void Remove_Equip(E_ITEM_EQUIP_TYPE miet) // 해제할 장비아이템 분류
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

    // 착용중인 주무기의 타입 { SWORD, AXE, KNIFE } 반환
    public E_ITEM_EQUIP_MAINWEAPON_TYPE Get_MainWeaponType()
    {
        return m_gEquipment_Mainweapon.m_eItemEquipMainWeaponType;
    }

    // 착용중인 장비아이템의 아이템 세트효과 코드 반환
    public int CheckSetItemEffect(E_ITEM_EQUIP_TYPE eiet) // 착용중인 장비아이템 분류
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

    // 리트라이(부활) 시 장비아이템 소실에 관한 함수(장비아이템 소실 + 장비아이템 해제)
    public bool ReTry_Lost_Item_Equip(Dictionary<int, int> dictionary) // 매개변수 Dictionary에 잃어버리는 아이템 정보가 존재
    {
        foreach (KeyValuePair<int, int> item in dictionary)
        {
            Delete_Item_Equip(item.Key); // 장비아이템 삭제
        }

        return true;
    }
    // 착용중인 장비아이템 삭제 함수
    void Delete_Item_Equip(int itemnumber) // 아이템 고유 코드(아이템 생성 순서)
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
