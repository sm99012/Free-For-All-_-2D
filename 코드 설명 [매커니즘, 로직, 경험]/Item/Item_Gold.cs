using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Gold : Item
{
    public int m_nGold;

    public Item_Gold() { }

    // Item 생성.(골드)
    public Item_Gold(int gold, Vector3 itemposition)
    {
        GameObject itemobject = Instantiate(ItemManager.instance.m_gItem_Gold_Null);
        Item_Gold itemscript = itemobject.GetComponent<Item_Gold>();

        itemscript.m_bPossible_Get = false;

        itemscript.m_sItemName = gold.ToString() + " 골드";
        itemscript.m_sItemDescription = gold.ToString() + " 골드";
        itemscript.m_nItemCode = 0;
        //itemscript.m_nItemNumber = ++ItemManager.sm_nItemNumber;
        itemscript.m_nItemNumber = 0;
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

        Debug.Log("ItemName: " + itemscript.m_sItemName + ", ItemNumber: " + itemscript.m_nItemNumber);

        itemscript.m_FadeinAlpa = 0;
        itemscript.Fadein();
    }

    // Item 획득.(골드)
    public int GetGold(Item_Gold item)
    {
        int gold = item.m_nPrice;
        Destroy(this.gameObject);

        return gold;
        //return item.m_nGold;
    }
}
