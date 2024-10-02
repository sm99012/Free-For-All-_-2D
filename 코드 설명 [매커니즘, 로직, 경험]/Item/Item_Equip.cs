using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// 장비아이템 타입 : { 모자, 상의, 하의, 신발, 장갑, 주무기, 보조무기 }
public enum E_ITEM_EQUIP_TYPE { HAT, TOP, BOTTOMS, SHOSE, GLOVES, MAINWEAPON, SUBWEAPON }
// 장비아이템(주무기) 타입 : { NULL, 검, 도끼, 단검, 창 }. 주무기가 아닌 장비아이템은 NULL 할당. 창 타입의 주무기는 미구현
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

    public STATUS m_STATUS_ReinforcementValue; // 장비아이템 강화 스탯(능력치)
    public SOC m_SOC_ReinforcementValue;       // 장비아이템 강화 스탯(평판)

    public int m_nReinforcementCount_Max;     // 장비아이템 최대 강화 횟수
    public int m_nReinforcementCount_Current; // 장비아이템 현재 강화 횟수

    public int m_nItemSetCode; // 장비아이템 세트효과 코드번호. 세트효과가 존재하지 않을 경우 0 할당

    // 생성자 오버로딩을 이용한 장비아이템 생성 함수(경우에 따라 사용하는 생성자가 다르다.)
    // 빈 생성자
    public Item_Equip() { }
    // 장비아이템 원본을 생성하는 생성자. 게임 시작시 최초 1회만 실행되는 함수
    public Item_Equip(string name, int code, string path_sprite,
        E_ITEM_GRADE eig, E_ITEM_EQUIP_TYPE iet,
        E_ITEM_EQUIP_MAINWEAPON_TYPE iemt = E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
        E_ITEM_ADDITIONALOPTION_STATUS eiastatus = E_ITEM_ADDITIONALOPTION_STATUS.S1,
        E_ITEM_ADDITIONALOPTION_SOC eiasoc = E_ITEM_ADDITIONALOPTION_SOC.S1,
        int rfcmax = 0, int rfccur = 0, int price = 0, int setitemcode = 0)
    {
        this.m_sItemName = name;
        this.m_nItemCode = code;
        this.m_sp_Sprite = Resources.Load<Sprite>(path_sprite);

        this.m_eItemType = E_ITEM_TYPE.EQUIP;
        this.m_eItemGrade = eig;
        this.m_eItemEquipType = iet;
        this.m_eItemEquipMainWeaponType = iemt;
        this.m_eItemEquip_SpecialRatio_STATUS = eiastatus;
        this.m_eItemEquip_SpecialRatio_SOC = eiasoc;

        // 장비아이템 착용 조건(상한ㆍ하한) 스탯(능력치, 평판) 초기화
        this.m_sStatus_Limit_Min = new STATUS(1, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        this.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        this.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        this.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);

        m_STATUS_ReinforcementValue = new STATUS();
        m_SOC_ReinforcementValue = new SOC();
        
        m_nReinforcementCount_Max = rfcmax;
        m_nReinforcementCount_Current = rfccur;
        
        m_nPrice = price;
        m_nItemSetCode = setitemcode;
    }
    // 장비아이템 사본을 생성하는 생성자. 필드에 장비아이템 드랍 시 실행되는 함수
    public Item_Equip(Item_Equip item, Vector3 itemposition) // item : 장비아이템 원본 객체, itemposition : 장비아이템 드랍 위치
    {
        GameObject itemobject = Instantiate(ItemManager.instance.m_gItem_Equip_Null); // 장비아이템 사본(유니티 오브젝트) 생성

        // 장비아이템 사본 객체 정보 할당
        Item_Equip itemscript = itemobject.GetComponent<Item_Equip>();

        itemscript.m_bPossible_Get = false;

        itemscript.m_sItemName = item.m_sItemName;
        itemscript.m_sItemDescription = item.m_sItemDescription;
        itemscript.m_nItemCode = item.m_nItemCode;
        itemscript.m_nItemNumber = ++ItemManager.sm_nItemNumber; // 아이템 생성코드 할당
        itemscript.m_sp_Sprite = item.m_sp_Sprite;
        itemscript.m_spr_SpriteRenderer = itemobject.GetComponent<SpriteRenderer>();

        itemscript.m_eItemType = E_ITEM_TYPE.EQUIP;
        itemscript.m_eItemGrade = item.m_eItemGrade;
        itemscript.m_eItemEquipType = item.m_eItemEquipType;
        itemscript.m_eItemEquipMainWeaponType = item.m_eItemEquipMainWeaponType;
        
        itemscript.m_eItemEquip_SpecialRatio_STATUS = item.m_eItemEquip_SpecialRatio_STATUS;
        itemscript.m_STATUS_AdditionalOption = Set_Item_Equip_AdditionalOption_STATUS(item, item.m_eItemEquip_SpecialRatio_STATUS); // 장비아이템 추가 스탯(능력치)을 결정하는 함수
        itemscript.m_eItemEquip_SpecialRatio_SOC = item.m_eItemEquip_SpecialRatio_SOC;
        itemscript.m_SOC_AdditionalOption = Set_Item_Equip_AdditionalOption_SOC(item, item.m_eItemEquip_SpecialRatio_SOC); // 장비아이템 추가 스탯(평판)을 결정하는 함수

        itemscript.m_STATUS_ReinforcementValue = item.m_STATUS_ReinforcementValue;
        itemscript.m_SOC_ReinforcementValue = item.m_SOC_ReinforcementValue;
        
        itemscript.m_sStatus_Limit_Max = item.m_sStatus_Limit_Max;
        itemscript.m_sStatus_Limit_Min = item.m_sStatus_Limit_Min;
        itemscript.m_sStatus_Effect = item.m_sStatus_Effect;
        itemscript.m_sStatus_Effect.P_OperatorSTATUS(itemscript.m_STATUS_AdditionalOption); // 장비아이템 스탯(능력치) = 원본 스탯(능력치) + 추가 스탯(능력치)
        itemscript.m_sStatus_Effect.P_OperatorSTATUS(itemscript.m_STATUS_ReinforcementValue); // 장비아이템 스탯(능력치) : 원본 스탯(능력치) += 강화 스탯(능력치)
        itemscript.m_sStatus_Effect.SetSTATUS_AttackSpeed((float)Math.Round(itemscript.m_sStatus_Effect.GetSTATUS_AttackSpeed(), 2)); // 장비아이템 스탯(능력치(공격속도))는 소수점 2자리까지만 계산
        itemscript.m_sSoc_Limit_Max = item.m_sSoc_Limit_Max;
        itemscript.m_sSoc_Limit_Min = item.m_sSoc_Limit_Min;
        itemscript.m_sSoc_Effect = item.m_sSoc_Effect;
        itemscript.m_sSoc_Effect.P_OperatorSOC(itemscript.m_SOC_AdditionalOption); // 장비아이템 스탯(평판) = 원본 스탯(평판) + 추가 스탯(평판)
        itemscript.m_sSoc_Effect.P_OperatorSOC(itemscript.m_SOC_ReinforcementValue); // 장비아이템 스탯(평판) : 원본 스탯(평판) += 강화 스탯(평판)

        itemscript.m_nReinforcementCount_Max = item.m_nReinforcementCount_Max;
        itemscript.m_nReinforcementCount_Current = item.m_nReinforcementCount_Current;
        
        itemscript.m_nPrice = item.m_nPrice;
        itemscript.m_nItemSetCode = item.m_nItemSetCode;

        itemobject.transform.position = itemposition;

        itemobject.GetComponent<SpriteRenderer>().sprite = item.m_sp_Sprite;
        itemobject.name = item.m_sItemName;

        itemscript.m_FadeinAlpa = 0;
        itemscript.Fadein(); // 아이템 생성시 페이드인 효과 실행 함수
    }
    // 장비아이템 사본을 생성하는 생성자. 플레이어가 장비아이템 사본을 획득할 시 실행되는 함수
    // 장비아이템 사본(유니티 오브젝트)을 획득할 시 해당 장비아이템 사본(유니티 오브젝트)은 삭제되고 해당 장비아이템 사본(메모리 객체) 데이터를 저장소에 저장
    public Item_Equip DeleteItem(Item_Equip item) // item : 장비아이템 사본 객체
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
        itemscript.m_eItemEquip_SpecialRatio_SOC = item.m_eItemEquip_SpecialRatio_SOC;
        itemscript.m_SOC_AdditionalOption = item.m_SOC_AdditionalOption;

        itemscript.m_STATUS_ReinforcementValue = item.m_STATUS_ReinforcementValue;
        itemscript.m_SOC_ReinforcementValue = item.m_SOC_ReinforcementValue;

        itemscript.m_sStatus_Limit_Max = item.m_sStatus_Limit_Max;
        itemscript.m_sStatus_Limit_Min = item.m_sStatus_Limit_Min;
        itemscript.m_sStatus_Effect = item.m_sStatus_Effect;
        itemscript.m_sStatus_Effect.P_OperatorSTATUS(itemscript.m_STATUS_AdditionalOption); // 장비아이템 스탯(능력치) : 원본 스탯(능력치) += 추가 스탯(능력치)
        itemscript.m_sStatus_Effect.P_OperatorSTATUS(itemscript.m_STATUS_ReinforcementValue); // 장비아이템 스탯(능력치) : 원본 스탯(능력치) += 강화 스탯(능력치)
        itemscript.m_sStatus_Effect.SetSTATUS_AttackSpeed((float)Math.Round(itemscript.m_sStatus_Effect.GetSTATUS_AttackSpeed(), 2));
        itemscript.m_sSoc_Limit_Max = item.m_sSoc_Limit_Max;
        itemscript.m_sSoc_Limit_Min = item.m_sSoc_Limit_Min;
        itemscript.m_sSoc_Effect = item.m_sSoc_Effect;
        itemscript.m_sSoc_Effect.P_OperatorSOC(itemscript.m_SOC_AdditionalOption); // 장비아이템 스탯(평판) : 원본 스탯(평판) += 추가 스탯(평판)
        itemscript.m_sSoc_Effect.P_OperatorSOC(itemscript.m_SOC_ReinforcementValue); // 장비아이템 스탯(평판) : 원본 스탯(평판) += 강화 스탯(평판)

        itemscript.m_nReinforcementCount_Max = item.m_nReinforcementCount_Max;
        itemscript.m_nReinforcementCount_Current = item.m_nReinforcementCount_Current;
        
        itemscript.m_nPrice = item.m_nPrice;
        itemscript.m_nItemSetCode = item.m_nItemSetCode;

        Destroy(this.gameObject); // 장비아이템 사본(유니티 오브젝트) 삭제

        return itemscript; // 장비아이템 사본(메모리 객체) 데이터 반환
    }
    // 장비아이템 사본을 생성하는 생성자. 플레이어가 장비아이템 사본을 획득할 시 실행되는 함수
    // 퀘스트 완료 보상으로 획득하는 장비아이템의 경우 장비아이템 사본(유니티 오브젝트)이 필요하지 않아 바로 장비아이템 사본(메모리 객체) 데이터를 저장소에 저장
    public Item_Equip CreateItem(Item_Equip item)
    {
        Item_Equip itemscript = new Item_Equip();

        itemscript.m_sItemName = item.m_sItemName;
        itemscript.m_sItemDescription = item.m_sItemDescription;
        itemscript.m_nItemCode = item.m_nItemCode;
        itemscript.m_nItemNumber = ++ItemManager.sm_nItemNumber; // 아이템 생성코드 할당
        itemscript.m_sp_Sprite = item.m_sp_Sprite;

        itemscript.m_eItemType = E_ITEM_TYPE.EQUIP;
        itemscript.m_eItemGrade = item.m_eItemGrade;
        itemscript.m_eItemEquipType = item.m_eItemEquipType;
        itemscript.m_eItemEquipMainWeaponType = item.m_eItemEquipMainWeaponType;
        
        itemscript.m_eItemEquip_SpecialRatio_STATUS = item.m_eItemEquip_SpecialRatio_STATUS;
        itemscript.m_STATUS_AdditionalOption = Set_Item_Equip_AdditionalOption_STATUS(item, item.m_eItemEquip_SpecialRatio_STATUS);
        itemscript.m_eItemEquip_SpecialRatio_SOC = item.m_eItemEquip_SpecialRatio_SOC;
        itemscript.m_SOC_AdditionalOption = Set_Item_Equip_AdditionalOption_SOC(item, item.m_eItemEquip_SpecialRatio_SOC);

        itemscript.m_STATUS_ReinforcementValue = item.m_STATUS_ReinforcementValue;
        itemscript.m_SOC_ReinforcementValue = item.m_SOC_ReinforcementValue;
        
        itemscript.m_sStatus_Limit_Max = item.m_sStatus_Limit_Max;
        itemscript.m_sStatus_Limit_Min = item.m_sStatus_Limit_Min;
        itemscript.m_sStatus_Effect = item.m_sStatus_Effect;
        itemscript.m_sStatus_Effect.P_OperatorSTATUS(itemscript.m_STATUS_AdditionalOption); // 장비아이템 스탯(능력치) : 원본 스탯(능력치) += 추가 스탯(능력치)
        itemscript.m_sStatus_Effect.P_OperatorSTATUS(itemscript.m_STATUS_ReinforcementValue); // 장비아이템 스탯(능력치) : 원본 스탯(능력치) += 강화 스탯(능력치)
        itemscript.m_sStatus_Effect.SetSTATUS_AttackSpeed((float)Math.Round(itemscript.m_sStatus_Effect.GetSTATUS_AttackSpeed(), 2));
        itemscript.m_sSoc_Limit_Max = item.m_sSoc_Limit_Max;
        itemscript.m_sSoc_Limit_Min = item.m_sSoc_Limit_Min;
        itemscript.m_sSoc_Effect = item.m_sSoc_Effect;
        itemscript.m_sSoc_Effect.P_OperatorSOC(itemscript.m_SOC_AdditionalOption); // 장비아이템 스탯(평판) : 원본 스탯(평판) += 추가 스탯(평판)
        itemscript.m_sSoc_Effect.P_OperatorSOC(itemscript.m_SOC_ReinforcementValue); // 장비아이템 스탯(평판) : 원본 스탯(평판) += 강화 스탯(평판)

        itemscript.m_nReinforcementCount_Max = item.m_nReinforcementCount_Max;
        itemscript.m_nReinforcementCount_Current = item.m_nReinforcementCount_Current;

        itemscript.m_nPrice = item.m_nPrice;
        itemscript.m_nItemSetCode = item.m_nItemSetCode;

        return itemscript;
    }

    // 불러오기
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
        itemscript.m_eItemEquip_SpecialRatio_SOC = item.m_eItemEquip_SpecialRatio_SOC;
        itemscript.m_SOC_AdditionalOption = additionalsoc;
        
        itemscript.m_STATUS_ReinforcementValue = reinforcementstatus;
        itemscript.m_SOC_ReinforcementValue = reinforcementsoc;

        itemscript.m_sStatus_Limit_Max = item.m_sStatus_Limit_Max;
        itemscript.m_sStatus_Limit_Min = item.m_sStatus_Limit_Min;
        itemscript.m_sStatus_Effect = item.m_sStatus_Effect;
        itemscript.m_sStatus_Effect.P_OperatorSTATUS(itemscript.m_STATUS_AdditionalOption);
        itemscript.m_sStatus_Effect.P_OperatorSTATUS(itemscript.m_STATUS_ReinforcementValue);
        itemscript.m_sStatus_Effect.SetSTATUS_AttackSpeed((float)Math.Round(itemscript.m_sStatus_Effect.GetSTATUS_AttackSpeed(), 2));
        itemscript.m_sSoc_Limit_Max = item.m_sSoc_Limit_Max;
        itemscript.m_sSoc_Limit_Min = item.m_sSoc_Limit_Min;
        itemscript.m_sSoc_Effect = item.m_sSoc_Effect;
        itemscript.m_sSoc_Effect.P_OperatorSOC(itemscript.m_SOC_AdditionalOption);
        itemscript.m_sSoc_Effect.P_OperatorSOC(itemscript.m_SOC_ReinforcementValue);

        itemscript.m_nReinforcementCount_Max = item.m_nReinforcementCount_Max;
        itemscript.m_nReinforcementCount_Current = reinforcementcount_current;

        itemscript.m_nPrice = item.m_nPrice;
        itemscript.m_nItemSetCode = item.m_nItemSetCode;

        return itemscript;
    }

    // 장비아이템 스탯(능력치, 평판) 초기화 함수
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
