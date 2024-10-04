using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Gold : Item
{
    public int m_nGold; // 골드(재화)

    // 빈 생성자
    public Item_Gold() { }
    // 골드(재화)를 생성하는 생성자. 필드에 골드(재화) 드랍 시 사용되는 생성자
    public Item_Gold(int gold, Vector3 itemposition) // gold : 획득할 골드(재화), itemposition : 골드(재화) 드랍 위치
    {
        GameObject itemobject = Instantiate(ItemManager.instance.m_gItem_Gold_Null); // 골드(재화) 사본(유니티 오브젝트) 생성

        // 골드(재화) 사본 객체 데이터 할당
        Item_Gold itemscript = itemobject.GetComponent<Item_Gold>();

        itemscript.m_bPossible_Get = false;

        itemscript.m_sItemName = gold.ToString() + " 골드";
        itemscript.m_sItemDescription = gold.ToString() + " 골드";
        itemscript.m_nItemCode = 0;
        itemscript.m_nItemNumber = 0; // 골드(재화)의 경우 아이템 생성코드 미할당(0 고정)
        // 획득할 골드(재화)의 양에 따라 아이템 스프라이트(이미지) 설정
        if (gold < 1000)
            itemscript.m_sp_Sprite = Resources.Load<Sprite>("Prefab/Item/Item_Gold/Gold Nugget");
        else
            itemscript.m_sp_Sprite = Resources.Load<Sprite>("Prefab/Item/Item_Gold/Golden Ingot");
        itemobject.GetComponent<SpriteRenderer>().sprite = itemscript.m_sp_Sprite;
        itemscript.m_spr_SpriteRenderer = itemobject.GetComponent<SpriteRenderer>();

        itemscript.m_eItemType = E_ITEM_TYPE.GOLD;
        itemscript.m_eItemGrade = E_ITEM_GRADE.NORMAL;

        itemscript.m_nPrice = gold;

        itemobject.transform.position = itemposition;

        itemobject.name = gold.ToString() + " 골드";

        itemscript.m_FadeinAlpa = 0;
        itemscript.Fadein(); // 아이템 생성 시 페이드인 효과 실행 함수
    }

    // 플레이어가 골드(재화) 사본을 획득할 시 사용되는 함수
    // 골드(재화) 사본(유니티 오브젝트)을 획득할 시 해당 골드(재화) 사본(유니티 오브젝트)은 삭제되고 해당 골드(재화) 사본(메모리 객체) 데이터(단순 int 타입 데이터)를 저장소에 저장
    public int GetGold(Item_Gold item)
    {
        int gold = item.m_nPrice;
        
        Destroy(this.gameObject);

        return gold;
    }
}
