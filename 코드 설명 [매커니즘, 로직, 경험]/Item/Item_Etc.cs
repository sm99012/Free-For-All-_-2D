using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 기타아이템 타입 : { 재료, 퀘스트 전용, 쓰레기, 다용도, 기타 }
public enum E_ITEM_ETC_TYPE { MATERIAL, QUEST, JUNK, MULTIPLE, NULL }

public class Item_Etc : Item // 기반이 되는 Item 클래스 상속
{
    public E_ITEM_ETC_TYPE m_eItemEtcType; // 기타아이템 타입

    // 생성자 오버로딩을 이용한 기타아이템 생성 함수(경우에 따라 사용하는 생성자가 다르다.)
    // 빈 생성자
    public Item_Etc() { }
    // 기타아이템 원본을 생성하는 생성자. 게임 시작 시 최초 1회만 사용되는 생성자
    public Item_Etc(string name, int code, string path_sprite, 
        E_ITEM_GRADE eig, E_ITEM_ETC_TYPE iet, int price)
    {
        this.m_sItemName = name;
        this.m_nItemCode = code;
        this.m_sp_Sprite = Resources.Load<Sprite>(path_sprite);
        
        this.m_eItemType = E_ITEM_TYPE.ETC;
        this.m_eItemGrade = eig;
        this.m_eItemEtcType = iet;

        this.m_nPrice = price;
    }
    // 기타아이템 사본을 생성하는 생성자. 필드에 기타아이템 드랍 시 사용되는 생성자
    public Item_Etc(Item_Etc item, Vector3 itemposition) // item : 기타아이템 원본 객체, itemposition : 기타아이템 드랍 위치
    {
        GameObject itemobject = Instantiate(ItemManager.instance.m_gItem_Etc_Null); // 기타아이템 사본(유니티 오브젝트) 생성

        // 기타아이템 사본 객체 데이터 할당
        Item_Etc itemscript = itemobject.GetComponent<Item_Etc>();

        itemscript.m_bPossible_Get = false;

        itemscript.m_sItemName = item.m_sItemName;
        itemscript.m_sItemDescription = item.m_sItemDescription;
        itemscript.m_nItemCode = item.m_nItemCode;
        itemscript.m_nItemNumber = 0; // 소비아이템의 경우 아이템 생성코드 미할당(0 고정)
        itemscript.m_sp_Sprite = item.m_sp_Sprite;
        itemscript.m_spr_SpriteRenderer = itemobject.GetComponent<SpriteRenderer>();

        itemscript.m_eItemType = E_ITEM_TYPE.ETC;
        itemscript.m_eItemEtcType = item.m_eItemEtcType;
        itemscript.m_eItemGrade = item.m_eItemGrade;

        itemscript.m_sStatus_Limit_Max = item.m_sStatus_Limit_Max;
        itemscript.m_sStatus_Limit_Min = item.m_sStatus_Limit_Min;
        itemscript.m_sStatus_Effect = item.m_sStatus_Effect;
        itemscript.m_sSoc_Limit_Max = item.m_sSoc_Limit_Max;
        itemscript.m_sSoc_Limit_Min = item.m_sSoc_Limit_Min;
        itemscript.m_sSoc_Effect = item.m_sSoc_Effect;

        itemscript.m_nPrice = item.m_nPrice;

        itemobject.transform.position = itemposition;

        itemobject.GetComponent<SpriteRenderer>().sprite = item.m_sp_Sprite;
        itemobject.name = item.m_sItemName;

        itemscript.m_FadeinAlpa = 0;
        itemscript.Fadein(); // 아이템 생성 시 페이드인 효과 실행 함수
    }

    // 기타아이템 사본을 생성하는 함수. 플레이어가 기타아이템 사본을 획득할 시 사용되는 함수
    // 기타아이템 사본(유니티 오브젝트)을 획득할 시 해당 기타아이템 사본(유니티 오브젝트)은 삭제되고 해당 기타아이템 사본(메모리 객체) 데이터를 저장소에 저장
    public Item_Etc DeleteItem(Item_Etc item)
    {
        Item_Etc itemscript = new Item_Etc();

        itemscript.m_sItemName = item.m_sItemName;
        itemscript.m_sItemDescription = item.m_sItemDescription;
        itemscript.m_nItemCode = item.m_nItemCode;
        itemscript.m_nItemNumber = 0; // 소비아이템의 경우 아이템 생성코드 미할당(0 고정)
        itemscript.m_sp_Sprite = item.m_sp_Sprite;

        itemscript.m_eItemType = E_ITEM_TYPE.ETC;
        itemscript.m_eItemEtcType = item.m_eItemEtcType;
        itemscript.m_eItemGrade = item.m_eItemGrade;

        itemscript.m_sStatus_Limit_Max = item.m_sStatus_Limit_Max;
        itemscript.m_sStatus_Limit_Min = item.m_sStatus_Limit_Min;
        itemscript.m_sStatus_Effect = item.m_sStatus_Effect;
        itemscript.m_sSoc_Limit_Max = item.m_sSoc_Limit_Max;
        itemscript.m_sSoc_Limit_Min = item.m_sSoc_Limit_Min;
        itemscript.m_sSoc_Effect = item.m_sSoc_Effect;

        itemscript.m_nPrice = item.m_nPrice;

        Destroy(this.gameObject);

        return itemscript;
    }

    // 기타아이템 사본을 생성하는 함수. 플레이어가 기타아이템 사본을 획득할 시 사용되는 함수
    // 퀘스트 완료 보상으로 획득하는 기타아이템의 경우 기타아이템 사본(유니티 오브젝트)이 필요하지 않아 바로 기타아이템 사본(메모리 객체) 데이터를 저장소에 저장
    public Item_Etc CreateItem(Item_Etc item)
    {
        Item_Etc itemscript = new Item_Etc();

        itemscript.m_sItemName = item.m_sItemName;
        itemscript.m_sItemDescription = item.m_sItemDescription;
        itemscript.m_nItemCode = item.m_nItemCode;
        itemscript.m_nItemNumber = 0; // 소비아이템의 경우 아이템 생성코드 미할당(0 고정)
        itemscript.m_sp_Sprite = item.m_sp_Sprite;

        itemscript.m_eItemType = E_ITEM_TYPE.ETC;
        itemscript.m_eItemEtcType = item.m_eItemEtcType;
        itemscript.m_eItemGrade = item.m_eItemGrade;

        itemscript.m_sStatus_Limit_Max = item.m_sStatus_Limit_Max;
        itemscript.m_sStatus_Limit_Min = item.m_sStatus_Limit_Min;
        itemscript.m_sStatus_Effect = item.m_sStatus_Effect;
        itemscript.m_sSoc_Limit_Max = item.m_sSoc_Limit_Max;
        itemscript.m_sSoc_Limit_Min = item.m_sSoc_Limit_Min;
        itemscript.m_sSoc_Effect = item.m_sSoc_Effect;

        itemscript.m_nPrice = item.m_nPrice;

        return itemscript;
    }

    // 로딩 관련 함수
    // 게임 시작 시 플레이어에게 적용중이거나 보유한 기타아이템 데이터 로딩
    public Item_Etc LoadItem(int itemcode)
    {
        Item_Etc item = ItemManager.instance.m_Dictionary_MonsterDrop_Etc[itemcode];
        Item_Etc itemscript = new Item_Etc();

        itemscript.m_sItemName = item.m_sItemName;
        itemscript.m_sItemDescription = item.m_sItemDescription;
        itemscript.m_nItemCode = item.m_nItemCode;
        itemscript.m_nItemNumber = 0; // 소비아이템의 경우 아이템 생성코드 미할당(0 고정)
        itemscript.m_sp_Sprite = item.m_sp_Sprite;

        itemscript.m_eItemType = E_ITEM_TYPE.ETC;
        itemscript.m_eItemEtcType = item.m_eItemEtcType;
        itemscript.m_eItemGrade = item.m_eItemGrade;

        itemscript.m_sStatus_Limit_Max = item.m_sStatus_Limit_Max;
        itemscript.m_sStatus_Limit_Min = item.m_sStatus_Limit_Min;
        itemscript.m_sStatus_Effect = item.m_sStatus_Effect;
        itemscript.m_sSoc_Limit_Max = item.m_sSoc_Limit_Max;
        itemscript.m_sSoc_Limit_Min = item.m_sSoc_Limit_Min;
        itemscript.m_sSoc_Effect = item.m_sSoc_Effect;

        itemscript.m_nPrice = item.m_nPrice;

        return itemscript;

    }
}
