using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// 장비아이템 타입 : { 모자, 상의, 하의, 신발, 장갑, 주무기, 보조무기 }
public enum E_ITEM_EQUIP_TYPE { HAT, TOP, BOTTOMS, SHOSE, GLOVES, MAINWEAPON, SUBWEAPON }
// 장비아이템(주무기) 타입 : { NULL, 검, 도끼, 단검, 창 }. 주무기가 아닌 장비아이템은 NULL값 할당. 창 타입의 주무기는 미구현
public enum E_ITEM_EQUIP_MAINWEAPON_TYPE { NULL, SWORD, AXE, KNIFE, SPEAR }
// 장비아이템 추가 스탯(능력치) 등급. 해당 등급에 따라 장비아이템에 추가 또는 감소 스탯(능력치) 적용
public enum E_ITEM_ADDITIONALOPTION_STATUS { S1, S2, S3, S4, S5, S6, S7, S8, S9, S10 }
// 장비아이템 추가 스탯(평판) 등급. 해당 등급에 따라 장비아이템에 추가 또는 감소 스탯(평판) 적용
public enum E_ITEM_ADDITIONALOPTION_SOC { S1, S2, S3, S4, S5, S6, S7, S8, S9, S10 }

//
// ※ 장비아이템 추가 스탯(능력치, 평판) 등급 관련 정보는 아이템 추가옵션
//

public class Item_Equip : Item // 기반이 되는 Item 클래스 상속
{
    public E_ITEM_EQUIP_TYPE m_eItemEquipType;                              // 장비아이템 타입
    public E_ITEM_EQUIP_MAINWEAPON_TYPE m_eItemEquipMainWeaponType;         // 장비아이템(주무기) 타입
    public E_ITEM_ADDITIONALOPTION_STATUS m_eItemEquip_SpecialRatio_STATUS; // 장비아이템 추가 스탯(능력치) 등급
    public STATUS m_STATUS_AdditionalOption;                                // 장비아이템 추가 스탯(능력치)
    public E_ITEM_ADDITIONALOPTION_SOC m_eItemEquip_SpecialRatio_SOC;       // 장비아이템 추가 스탯(평판) 등급
    public SOC m_SOC_AdditionalOption;                                      // 장비아이템 추가 스탯(평판)

    public STATUS m_STATUS_ReinforcementValue;
    public SOC m_SOC_ReinforcementValue;

    public int m_nReinforcementCount_Max;
    public int m_nReinforcementCount_Current;

    // 세트 아이템 코드번호.
    // 0: 세트효과 X
    public int m_nItemSetCode;

    public Item_Equip() { }
    // Item 원본.
    public Item_Equip(string name, int code, string path_sprite,
        E_ITEM_GRADE eig, E_ITEM_EQUIP_TYPE iet,
        E_ITEM_EQUIP_MAINWEAPON_TYPE iemt = E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
        E_ITEM_ADDITIONALOPTION_STATUS eiastatus = E_ITEM_ADDITIONALOPTION_STATUS.S1,
        E_ITEM_ADDITIONALOPTION_SOC eiasoc = E_ITEM_ADDITIONALOPTION_SOC.S1,
        int rfcmax = 0, int rfccur = 0, int price = 0, int setitemcode = 0)
    {
        this.m_sItemName = name;
        this.m_nItemCode = code;
        //this.m_sp_Sprite = AssetDatabase.LoadAssetAtPath(path_sprite, typeof(Sprite)) as Sprite;
        this.m_sp_Sprite = Resources.Load<Sprite>(path_sprite);

        this.m_eItemType = E_ITEM_TYPE.EQUIP;
        this.m_eItemGrade = eig;
        this.m_eItemEquipType = iet;
        this.m_eItemEquipMainWeaponType = iemt;
        this.m_eItemEquip_SpecialRatio_STATUS = eiastatus;
        this.m_eItemEquip_SpecialRatio_SOC = eiasoc;

        this.m_sStatus_Limit_Min = new STATUS(1, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        this.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        this.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        this.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);

        m_nReinforcementCount_Max = rfcmax;
        m_nReinforcementCount_Current = rfccur;
        m_nPrice = price;
        m_nItemSetCode = setitemcode;

        m_STATUS_ReinforcementValue = new STATUS();
        m_SOC_ReinforcementValue = new SOC();
    }

    // Monster 가 Item 을 떨어트릴때 사용. 
    // Item 사본.
    public Item_Equip(Item_Equip item, Vector3 itemposition)
    {
        GameObject itemobject = Instantiate(ItemManager.instance.m_gItem_Equip_Null);
        Item_Equip itemscript = itemobject.GetComponent<Item_Equip>();

        itemscript.m_bPossible_Get = false;

        itemscript.m_sItemName = item.m_sItemName;
        itemscript.m_sItemDescription = item.m_sItemDescription;
        itemscript.m_nItemCode = item.m_nItemCode;
        itemscript.m_nItemNumber = ++ItemManager.sm_nItemNumber;
        itemscript.m_sp_Sprite = item.m_sp_Sprite;
        itemscript.m_spr_SpriteRenderer = itemobject.GetComponent<SpriteRenderer>();

        itemscript.m_eItemType = E_ITEM_TYPE.EQUIP;
        itemscript.m_eItemGrade = item.m_eItemGrade;
        itemscript.m_eItemEquipType = item.m_eItemEquipType;
        itemscript.m_eItemEquipMainWeaponType = item.m_eItemEquipMainWeaponType;
        itemscript.m_eItemEquip_SpecialRatio_STATUS = item.m_eItemEquip_SpecialRatio_STATUS;
        itemscript.m_STATUS_AdditionalOption = Set_Item_Equip_AdditionalOption_STATUS(item, item.m_eItemEquip_SpecialRatio_STATUS);
        itemscript.m_eItemEquip_SpecialRatio_SOC = item.m_eItemEquip_SpecialRatio_SOC;
        itemscript.m_SOC_AdditionalOption = Set_Item_Equip_AdditionalOption_SOC(item, item.m_eItemEquip_SpecialRatio_SOC);

        itemscript.m_sStatus_Effect = item.m_sStatus_Effect;
        itemscript.m_sStatus_Effect.P_OperatorSTATUS(itemscript.m_STATUS_AdditionalOption);
        itemscript.m_sStatus_Effect.SetSTATUS_AttackSpeed((float)Math.Round(itemscript.m_sStatus_Effect.GetSTATUS_AttackSpeed(), 2));
        itemscript.m_sStatus_Limit_Min = item.m_sStatus_Limit_Min;
        itemscript.m_sStatus_Limit_Max = item.m_sStatus_Limit_Max;
        itemscript.m_sSoc_Effect = item.m_sSoc_Effect;
        itemscript.m_sSoc_Effect.P_OperatorSOC(itemscript.m_SOC_AdditionalOption);
        itemscript.m_sSoc_Limit_Min = item.m_sSoc_Limit_Min;
        itemscript.m_sSoc_Limit_Max = item.m_sSoc_Limit_Max;

        itemscript.m_nReinforcementCount_Max = item.m_nReinforcementCount_Max;
        itemscript.m_nReinforcementCount_Current = item.m_nReinforcementCount_Current;
        itemscript.m_nPrice = item.m_nPrice;
        itemscript.m_nItemSetCode = item.m_nItemSetCode;

        itemobject.transform.position = itemposition;

        itemobject.GetComponent<SpriteRenderer>().sprite = item.m_sp_Sprite;
        itemobject.name = item.m_sItemName;

        Debug.Log("ItemName: " + itemscript.m_sItemName + ", ItemNumber: " + itemscript.m_nItemNumber);

        itemscript.m_FadeinAlpa = 0;
        itemscript.Fadein();
    }

    // Player 줍기 시 사용.
    // Item 사본을 획득하면 Item 사본은 삭제, Item 사본 데이터를 저장.
    public Item_Equip DeleteItem(Item_Equip item)
    {
        Item_Equip itemscript = new Item_Equip();

        itemscript.m_sItemName = item.m_sItemName;
        itemscript.m_sItemDescription = item.m_sItemDescription;
        itemscript.m_nItemCode = item.m_nItemCode;
        itemscript.m_nItemNumber = item.m_nItemNumber;
        itemscript.m_sp_Sprite = item.m_sp_Sprite;

        itemscript.m_eItemType = E_ITEM_TYPE.EQUIP;
        itemscript.m_eItemGrade = item.m_eItemGrade;
        itemscript.m_eItemEquipType = item.m_eItemEquipType;
        itemscript.m_eItemEquipMainWeaponType = item.m_eItemEquipMainWeaponType;
        itemscript.m_eItemEquip_SpecialRatio_STATUS = item.m_eItemEquip_SpecialRatio_STATUS;
        itemscript.m_STATUS_AdditionalOption = item.m_STATUS_AdditionalOption;
        itemscript.m_STATUS_ReinforcementValue = item.m_STATUS_ReinforcementValue;
        itemscript.m_eItemEquip_SpecialRatio_SOC = item.m_eItemEquip_SpecialRatio_SOC;
        itemscript.m_SOC_AdditionalOption = item.m_SOC_AdditionalOption;
        itemscript.m_SOC_ReinforcementValue = item.m_SOC_ReinforcementValue;

        itemscript.m_sStatus_Effect = item.m_sStatus_Effect;
        itemscript.m_eItemEquip_SpecialRatio_STATUS = item.m_eItemEquip_SpecialRatio_STATUS;
        itemscript.m_sStatus_Limit_Min = item.m_sStatus_Limit_Min;
        itemscript.m_sStatus_Limit_Max = item.m_sStatus_Limit_Max;

        itemscript.m_sSoc_Effect = item.m_sSoc_Effect;
        itemscript.m_eItemEquip_SpecialRatio_SOC = item.m_eItemEquip_SpecialRatio_SOC;
        itemscript.m_sSoc_Limit_Min = item.m_sSoc_Limit_Min;
        itemscript.m_sSoc_Limit_Max = item.m_sSoc_Limit_Max;
        itemscript.m_nPrice = item.m_nPrice;
        itemscript.m_nItemSetCode = item.m_nItemSetCode;

        itemscript.m_nReinforcementCount_Max = item.m_nReinforcementCount_Max;
        itemscript.m_nReinforcementCount_Current = item.m_nReinforcementCount_Current;

        Destroy(this.gameObject);

        return itemscript;
    }

    // Player 퀘스트 보상 획득 시 사용.
    public Item_Equip CreateItem(Item_Equip item)
    {
        Item_Equip itemscript = new Item_Equip();

        itemscript.m_sItemName = item.m_sItemName;
        itemscript.m_sItemDescription = item.m_sItemDescription;
        itemscript.m_nItemCode = item.m_nItemCode;
        itemscript.m_nItemNumber = ++ItemManager.sm_nItemNumber;
        itemscript.m_sp_Sprite = item.m_sp_Sprite;

        itemscript.m_eItemType = E_ITEM_TYPE.EQUIP;
        itemscript.m_eItemGrade = item.m_eItemGrade;
        itemscript.m_eItemEquipType = item.m_eItemEquipType;
        itemscript.m_eItemEquipMainWeaponType = item.m_eItemEquipMainWeaponType;
        itemscript.m_eItemEquip_SpecialRatio_STATUS = item.m_eItemEquip_SpecialRatio_STATUS;
        itemscript.m_eItemEquip_SpecialRatio_SOC = item.m_eItemEquip_SpecialRatio_SOC;

        itemscript.m_sStatus_Effect = item.m_sStatus_Effect;
        itemscript.m_STATUS_AdditionalOption = Set_Item_Equip_AdditionalOption_STATUS(item, item.m_eItemEquip_SpecialRatio_STATUS);
        itemscript.m_STATUS_ReinforcementValue = item.m_STATUS_ReinforcementValue;
        itemscript.m_sStatus_Effect.P_OperatorSTATUS(itemscript.m_STATUS_AdditionalOption);
        itemscript.m_sStatus_Effect.SetSTATUS_AttackSpeed((float)Math.Round(itemscript.m_sStatus_Effect.GetSTATUS_AttackSpeed(), 2));
        itemscript.m_sStatus_Limit_Min = item.m_sStatus_Limit_Min;
        itemscript.m_sStatus_Limit_Max = item.m_sStatus_Limit_Max;

        itemscript.m_sSoc_Effect = item.m_sSoc_Effect;
        itemscript.m_SOC_AdditionalOption = Set_Item_Equip_AdditionalOption_SOC(item, item.m_eItemEquip_SpecialRatio_SOC);
        itemscript.m_SOC_ReinforcementValue = item.m_SOC_ReinforcementValue;
        itemscript.m_sSoc_Effect.P_OperatorSOC(itemscript.m_SOC_AdditionalOption);
        itemscript.m_sSoc_Limit_Min = item.m_sSoc_Limit_Min;
        itemscript.m_sSoc_Limit_Max = item.m_sSoc_Limit_Max;

        itemscript.m_sSoc_Limit_Min = item.m_sSoc_Limit_Min;
        itemscript.m_sSoc_Limit_Max = item.m_sSoc_Limit_Max;
        itemscript.m_nPrice = item.m_nPrice;
        itemscript.m_nItemSetCode = item.m_nItemSetCode;

        itemscript.m_nReinforcementCount_Max = item.m_nReinforcementCount_Max;
        itemscript.m_nReinforcementCount_Current = item.m_nReinforcementCount_Current;

        return itemscript;
    }

    // 불러오기.
    public Item_Equip LoadItem(int itemcode, int itemnumber, STATUS additionalstatus, SOC additionalsoc, int reinforcementcount_current, STATUS reinforcementstatus, SOC reinforcementsoc)
    {
        Item_Equip item = ItemManager.instance.m_Dictionary_MonsterDrop_Equip[itemcode];
        Item_Equip itemscript = new Item_Equip();

        itemscript.m_sItemName = item.m_sItemName;
        itemscript.m_sItemDescription = item.m_sItemDescription;
        itemscript.m_nItemCode = item.m_nItemCode;
        itemscript.m_nItemNumber = itemnumber;
        itemscript.m_sp_Sprite = item.m_sp_Sprite;

        itemscript.m_eItemType = E_ITEM_TYPE.EQUIP;
        itemscript.m_eItemGrade = item.m_eItemGrade;
        itemscript.m_eItemEquipType = item.m_eItemEquipType;
        itemscript.m_eItemEquipMainWeaponType = item.m_eItemEquipMainWeaponType;
        itemscript.m_eItemEquip_SpecialRatio_STATUS = item.m_eItemEquip_SpecialRatio_STATUS;
        itemscript.m_STATUS_AdditionalOption = additionalstatus;
        itemscript.m_STATUS_ReinforcementValue = reinforcementstatus;
        itemscript.m_eItemEquip_SpecialRatio_SOC = item.m_eItemEquip_SpecialRatio_SOC;
        itemscript.m_SOC_AdditionalOption = additionalsoc;
        itemscript.m_SOC_ReinforcementValue = reinforcementsoc;

        itemscript.m_sStatus_Effect = item.m_sStatus_Effect;
        itemscript.m_sStatus_Effect.P_OperatorSTATUS(itemscript.m_STATUS_AdditionalOption);
        itemscript.m_sStatus_Effect.P_OperatorSTATUS(itemscript.m_STATUS_ReinforcementValue);
        itemscript.m_sStatus_Effect.SetSTATUS_AttackSpeed((float)Math.Round(itemscript.m_sStatus_Effect.GetSTATUS_AttackSpeed(), 2));
        itemscript.m_sStatus_Limit_Min = item.m_sStatus_Limit_Min;
        itemscript.m_sStatus_Limit_Max = item.m_sStatus_Limit_Max;
        itemscript.m_sSoc_Effect = item.m_sSoc_Effect;
        itemscript.m_sSoc_Effect.P_OperatorSOC(itemscript.m_SOC_AdditionalOption);
        itemscript.m_sSoc_Effect.P_OperatorSOC(itemscript.m_SOC_ReinforcementValue);
        itemscript.m_sSoc_Limit_Min = item.m_sSoc_Limit_Min;
        itemscript.m_sSoc_Limit_Max = item.m_sSoc_Limit_Max;

        itemscript.m_sSoc_Limit_Min = item.m_sSoc_Limit_Min;
        itemscript.m_sSoc_Limit_Max = item.m_sSoc_Limit_Max;
        itemscript.m_nPrice = item.m_nPrice;
        itemscript.m_nItemSetCode = item.m_nItemSetCode;

        itemscript.m_nReinforcementCount_Max = item.m_nReinforcementCount_Max;
        itemscript.m_nReinforcementCount_Current = reinforcementcount_current;

        //if (itemscript.m_nReinforcementCount_Current != 0)
        //    itemscript.m_sItemName += " + " + itemscript.m_nReinforcementCount_Current.ToString();

        return itemscript;
    }

    // Item 능력치 초기화 및 계산.
    void CarculateSTATUS()
    {
        
    }

    // 장비 아이템의 추가 능력치.
    // Drop, 퀘스트 보상 으로 인해 획득할 수 있는 장비에 부여된 추가 능력치.
    // STATUS
    STATUS Set_Item_Equip_AdditionalOption_STATUS(Item_Equip item, E_ITEM_ADDITIONALOPTION_STATUS eiastatus)
    {
        STATUS additionalstatus = new STATUS(item.m_sStatus_Effect);
        int AdditionalOptionRatio = ((int)eiastatus + 1);
        float AdditionalOptionRatio_Min, AdditionalOptionRatio_Max, AdditionalOptionRatio_AttackSpeed;

        //Debug.Log("추가 능력치 계수: " + AdditionalOptionRatio);

        //AdditionalOptionRatio_Max = (float)AdditionalOptionRatio * 0.05f;
        AdditionalOptionRatio_Max = (float)AdditionalOptionRatio * 0.1f;

        //switch (AdditionalOptionRatio)
        //{
        //    case 1:
        //        {
        //            AdditionalOptionRatio_Min = 0;
        //            AdditionalOptionRatio_AttackSpeed = 0.05f;
        //        }
        //        break;
        //    case 2:
        //        {
        //            AdditionalOptionRatio_Min = 0;
        //            AdditionalOptionRatio_AttackSpeed = 0.05f;
        //        }
        //        break;
        //    case 3:
        //        {
        //            AdditionalOptionRatio_Min = 0;
        //            AdditionalOptionRatio_AttackSpeed = 0.1f;
        //        }
        //        break;
        //    case 4:
        //        {
        //            AdditionalOptionRatio_Min = 0;
        //            AdditionalOptionRatio_AttackSpeed = 0.1f;
        //        }
        //        break;
        //    case 5:
        //        {
        //            AdditionalOptionRatio_Min = -0.05f;
        //            AdditionalOptionRatio_AttackSpeed = 0.15f;
        //        }
        //        break;
        //    case 6:
        //        {
        //            AdditionalOptionRatio_Min = -0.05f;
        //            AdditionalOptionRatio_AttackSpeed = 0.15f;
        //        }
        //        break;
        //    case 7:
        //        {
        //            AdditionalOptionRatio_Min = -0.1f;
        //            AdditionalOptionRatio_AttackSpeed = 0.2f;
        //        }
        //        break;
        //    case 8:
        //        {
        //            AdditionalOptionRatio_Min = -0.1f;
        //            AdditionalOptionRatio_AttackSpeed = 0.2f;
        //        }
        //        break;
        //    case 9:
        //        {
        //            AdditionalOptionRatio_Min = -0.15f;
        //            AdditionalOptionRatio_AttackSpeed = 0.25f;
        //        }
        //        break;
        //    case 10:
        //        {
        //            AdditionalOptionRatio_Min = -0.15f;
        //            AdditionalOptionRatio_AttackSpeed = 0.25f;
        //        }
        //        break;
        //    default:
        //        {
        //            AdditionalOptionRatio_Min = 0;
        //            AdditionalOptionRatio_AttackSpeed = 0;
        //        }
        //        break;
        //}
        switch (AdditionalOptionRatio)
        {
            case 1:
                {
                    AdditionalOptionRatio_Min = 0.05f;
                    AdditionalOptionRatio_AttackSpeed = 0.05f;
                }
                break;
            case 2:
                {
                    AdditionalOptionRatio_Min = 0.1f;
                    AdditionalOptionRatio_AttackSpeed = 0.05f;
                }
                break;
            case 3:
                {
                    AdditionalOptionRatio_Min = 0.15f;
                    AdditionalOptionRatio_AttackSpeed = 0.1f;
                }
                break;
            case 4:
                {
                    AdditionalOptionRatio_Min = 0.2f;
                    AdditionalOptionRatio_AttackSpeed = 0.1f;
                }
                break;
            case 5:
                {
                    AdditionalOptionRatio_Min = 0.25f;
                    AdditionalOptionRatio_AttackSpeed = 0.15f;
                }
                break;
            case 6:
                {
                    AdditionalOptionRatio_Min = 0.3f;
                    AdditionalOptionRatio_AttackSpeed = 0.15f;
                }
                break;
            case 7:
                {
                    AdditionalOptionRatio_Min = 0.35f;
                    AdditionalOptionRatio_AttackSpeed = 0.2f;
                }
                break;
            case 8:
                {
                    AdditionalOptionRatio_Min = 0.4f;
                    AdditionalOptionRatio_AttackSpeed = 0.2f;
                }
                break;
            case 9:
                {
                    AdditionalOptionRatio_Min = 0.45f;
                    AdditionalOptionRatio_AttackSpeed = 0.25f;
                }
                break;
            case 10:
                {
                    AdditionalOptionRatio_Min = 0.5f;
                    AdditionalOptionRatio_AttackSpeed = 0.25f;
                }
                break;
            default:
                {
                    AdditionalOptionRatio_Min = 0;
                    AdditionalOptionRatio_AttackSpeed = 0;
                }
                break;
        }

        //if (additionalstatus.GetSTATUS_HP_Max() != 0)
        //    additionalstatus.SetSTATUS_HP_Max((int)UnityEngine.Random.Range((float)(-additionalstatus.GetSTATUS_HP_Max() * AdditionalOptionRatio_Min), (float)(additionalstatus.GetSTATUS_HP_Max() * AdditionalOptionRatio_Max) + 0.1f));
        //if (additionalstatus.GetSTATUS_MP_Max() != 0)
        //    additionalstatus.SetSTATUS_MP_Max((int)UnityEngine.Random.Range((float)(-additionalstatus.GetSTATUS_MP_Max() * AdditionalOptionRatio_Min), (float)(additionalstatus.GetSTATUS_MP_Max() * AdditionalOptionRatio_Max) + 0.1f));
        //if (additionalstatus.GetSTATUS_Damage_Total() != 0)
        //    additionalstatus.SetSTATUS_Damage_Total((int)UnityEngine.Random.Range((float)(-additionalstatus.GetSTATUS_Damage_Total() * AdditionalOptionRatio_Min), (float)(additionalstatus.GetSTATUS_Damage_Total() * AdditionalOptionRatio_Max) + 0.1f));
        //if (additionalstatus.GetSTATUS_CriticalRate() != 0)
        //    additionalstatus.SetSTATUS_CriticalRate((int)UnityEngine.Random.Range((float)(-additionalstatus.GetSTATUS_CriticalRate() * AdditionalOptionRatio_Min), (float)(additionalstatus.GetSTATUS_CriticalRate() * AdditionalOptionRatio_Max) + 0.1f));
        //if (additionalstatus.GetSTATUS_CriticalDamage() != 0)
        //    additionalstatus.SetSTATUS_CriticalDamage((int)UnityEngine.Random.Range((float)(-additionalstatus.GetSTATUS_CriticalDamage() * AdditionalOptionRatio_Min), (float)(additionalstatus.GetSTATUS_CriticalDamage() * AdditionalOptionRatio_Max) + 0.1f));
        //if (additionalstatus.GetSTATUS_Defence_Physical() != 0)
        //    additionalstatus.SetSTATUS_Defence_Physical((int)UnityEngine.Random.Range((float)(-additionalstatus.GetSTATUS_Defence_Physical() * AdditionalOptionRatio_Min), (float)(additionalstatus.GetSTATUS_Defence_Physical() * AdditionalOptionRatio_Max) + 0.1f));
        //if (additionalstatus.GetSTATUS_Defence_Magical() != 0)
        //    additionalstatus.SetSTATUS_Defence_Magical((int)UnityEngine.Random.Range((float)(-additionalstatus.GetSTATUS_Defence_Magical() * AdditionalOptionRatio_Min), (float)(additionalstatus.GetSTATUS_Defence_Magical() * AdditionalOptionRatio_Max) + 0.1f));
        //if (additionalstatus.GetSTATUS_Speed() != 0)
        //    additionalstatus.SetSTATUS_Speed((int)UnityEngine.Random.Range((float)(-additionalstatus.GetSTATUS_Speed() * AdditionalOptionRatio_Min), (float)(additionalstatus.GetSTATUS_Speed() * AdditionalOptionRatio_Max) + 0.1f));
        //if (additionalstatus.GetSTATUS_EvasionRate() != 0)
        //    additionalstatus.SetSTATUS_EvasionRate((int)UnityEngine.Random.Range((float)(-additionalstatus.GetSTATUS_EvasionRate() * AdditionalOptionRatio_Min), (float)(additionalstatus.GetSTATUS_EvasionRate() * AdditionalOptionRatio_Max) + 0.1f));
        if (additionalstatus.GetSTATUS_HP_Max() != 0)
            additionalstatus.SetSTATUS_HP_Max((int)UnityEngine.Random.Range(Mathf.Round((float)(-additionalstatus.GetSTATUS_HP_Max() * AdditionalOptionRatio_Min)), Mathf.Round((float)(additionalstatus.GetSTATUS_HP_Max() * AdditionalOptionRatio_Max) + 0.1f)));
        if (additionalstatus.GetSTATUS_MP_Max() != 0)
            additionalstatus.SetSTATUS_MP_Max((int)UnityEngine.Random.Range(Mathf.Round((float)(-additionalstatus.GetSTATUS_MP_Max() * AdditionalOptionRatio_Min)), Mathf.Round((float)(additionalstatus.GetSTATUS_MP_Max() * AdditionalOptionRatio_Max) + 0.1f)));
        if (additionalstatus.GetSTATUS_Damage_Total() != 0)
            additionalstatus.SetSTATUS_Damage_Total((int)UnityEngine.Random.Range(Mathf.Round((float)(-additionalstatus.GetSTATUS_Damage_Total() * AdditionalOptionRatio_Min)), Mathf.Round((float)(additionalstatus.GetSTATUS_Damage_Total() * AdditionalOptionRatio_Max) + 0.1f)));
        if (additionalstatus.GetSTATUS_CriticalRate() != 0)
            additionalstatus.SetSTATUS_CriticalRate((int)UnityEngine.Random.Range(Mathf.Round((float)(-additionalstatus.GetSTATUS_CriticalRate() * AdditionalOptionRatio_Min)), Mathf.Round((float)(additionalstatus.GetSTATUS_CriticalRate() * AdditionalOptionRatio_Max) + 0.1f)));
        if (additionalstatus.GetSTATUS_CriticalDamage() != 0)
            additionalstatus.SetSTATUS_CriticalDamage((int)UnityEngine.Random.Range(Mathf.Round((float)(-additionalstatus.GetSTATUS_CriticalDamage() * AdditionalOptionRatio_Min)), Mathf.Round((float)(additionalstatus.GetSTATUS_CriticalDamage() * AdditionalOptionRatio_Max) + 0.1f)));
        if (additionalstatus.GetSTATUS_Defence_Physical() != 0)
            additionalstatus.SetSTATUS_Defence_Physical((int)UnityEngine.Random.Range(Mathf.Round((float)(-additionalstatus.GetSTATUS_Defence_Physical() * AdditionalOptionRatio_Min)), Mathf.Round((float)(additionalstatus.GetSTATUS_Defence_Physical() * AdditionalOptionRatio_Max) + 0.1f)));
        if (additionalstatus.GetSTATUS_Defence_Magical() != 0)
            additionalstatus.SetSTATUS_Defence_Magical((int)UnityEngine.Random.Range(Mathf.Round((float)(-additionalstatus.GetSTATUS_Defence_Magical() * AdditionalOptionRatio_Min)), Mathf.Round((float)(additionalstatus.GetSTATUS_Defence_Magical() * AdditionalOptionRatio_Max) + 0.1f)));
        if (additionalstatus.GetSTATUS_Speed() != 0)
            additionalstatus.SetSTATUS_Speed((int)UnityEngine.Random.Range(Mathf.Round((float)(-additionalstatus.GetSTATUS_Speed() * AdditionalOptionRatio_Min)), Mathf.Round((float)(additionalstatus.GetSTATUS_Speed() * AdditionalOptionRatio_Max) + 0.1f)));
        if (additionalstatus.GetSTATUS_EvasionRate() != 0)
            additionalstatus.SetSTATUS_EvasionRate((int)UnityEngine.Random.Range(Mathf.Round((float)(-additionalstatus.GetSTATUS_EvasionRate() * AdditionalOptionRatio_Min)), Mathf.Round((float)(additionalstatus.GetSTATUS_EvasionRate() * AdditionalOptionRatio_Max) + 0.1f)));


        if (additionalstatus.GetSTATUS_AttackSpeed() != 0)
            additionalstatus.SetSTATUS_AttackSpeed((float)Math.Round(UnityEngine.Random.Range(-AdditionalOptionRatio_AttackSpeed, AdditionalOptionRatio_AttackSpeed), 2));

        //Debug.Log(additionalstatus.GetSTATUS_Data());

        return additionalstatus;
    }
    // SOC
    SOC Set_Item_Equip_AdditionalOption_SOC(Item_Equip item, E_ITEM_ADDITIONALOPTION_SOC eiasoc)
    {
        SOC additionalsoc = new SOC(item.m_sSoc_Effect);
        //int AdditionalOptionRatio = item.m_sStatus_Limit_Min.GetSTATUS_LV() * ((int)eiasoc + 1);
        int AdditionalOptionRatio = (int)eiasoc + 1;

        if (additionalsoc.GetSOC_Honor() != 0)
            additionalsoc.SetSOC_Honor(UnityEngine.Random.Range(-AdditionalOptionRatio, AdditionalOptionRatio + 1));
        if (additionalsoc.GetSOC_Human() != 0)
            additionalsoc.SetSOC_Human(UnityEngine.Random.Range(-AdditionalOptionRatio, AdditionalOptionRatio + 1));
        if (additionalsoc.GetSOC_Animal() != 0)
            additionalsoc.SetSOC_Animal(UnityEngine.Random.Range(-AdditionalOptionRatio, AdditionalOptionRatio + 1));
        if (additionalsoc.GetSOC_Slime() != 0)
            additionalsoc.SetSOC_Slime(UnityEngine.Random.Range(-AdditionalOptionRatio, AdditionalOptionRatio + 1));
        if (additionalsoc.GetSOC_Skeleton() != 0)
            additionalsoc.SetSOC_Skeleton(UnityEngine.Random.Range(-AdditionalOptionRatio, AdditionalOptionRatio + 1));
        if (additionalsoc.GetSOC_Ents() != 0)
            additionalsoc.SetSOC_Ents(UnityEngine.Random.Range(-AdditionalOptionRatio, AdditionalOptionRatio + 1));
        if (additionalsoc.GetSOC_Devil() != 0)
            additionalsoc.SetSOC_Devil(UnityEngine.Random.Range(-AdditionalOptionRatio, AdditionalOptionRatio + 1));
        if (additionalsoc.GetSOC_Dragon() != 0)
            additionalsoc.SetSOC_Dragon(UnityEngine.Random.Range(-AdditionalOptionRatio, AdditionalOptionRatio + 1));
        if (additionalsoc.GetSOC_Shadow() != 0)
            additionalsoc.SetSOC_Shadow(UnityEngine.Random.Range(-AdditionalOptionRatio, AdditionalOptionRatio + 1));

        return additionalsoc;
    }

    // 장비 아이템 강화.
    public bool ReinforceItem(Reinforcement rf)
    {
        Debug.Log("REINFORCEMENT: " + m_nReinforcementCount_Current + " / " + m_nReinforcementCount_Max);
        if (m_nReinforcementCount_Max > m_nReinforcementCount_Current)
        {
            Debug.Log("업그레이드 가능.");
            if (rf.Reinforce_Item_Equip() == true)
            {
                Debug.Log("강화 성공");
                m_STATUS_ReinforcementValue.P_OperatorSTATUS(rf.GetReinforcementSTATUS());
                m_SOC_ReinforcementValue.P_OperatorSOC(rf.GetReinforcementSOC());
                m_sStatus_Effect.P_OperatorSTATUS(rf.GetReinforcementSTATUS());
                m_sSoc_Effect.P_OperatorSOC(rf.GetReinforcementSOC());
                //CarculateSTATUS();
                GUIManager_Total.Instance.Display_GUI_Reinforcement_Check(true);
            }
            else
            {
                Debug.Log("강화 실패");
                GUIManager_Total.Instance.Display_GUI_Reinforcement_Check(false);
            }

            m_nReinforcementCount_Current += 1;

            return true;
        }
        else
            Debug.Log("업그레이드 가능 횟수 초과.");
            return false;
    }
}
