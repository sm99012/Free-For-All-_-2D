using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum E_ITEM_ETC_TYPE { MATERIAL, QUEST, JUNK, MULTIPLE, NULL }

public class Item_Etc : Item
{
    public E_ITEM_ETC_TYPE m_eItemEtcType;

    public Item_Etc() { }
    // Item 원본.
    public Item_Etc(string name, int code, string path_sprite, E_ITEM_GRADE eig, E_ITEM_ETC_TYPE iet,
        int price)
    {
        this.m_sItemName = name;
        this.m_nItemCode = code;
        this.m_sp_Sprite = Resources.Load<Sprite>(path_sprite);
        
        this.m_eItemType = E_ITEM_TYPE.ETC;
        this.m_eItemGrade = eig;
        this.m_eItemEtcType = iet;

        this.m_nPrice = price;
    }

    // Item 사본.
    public Item_Etc(Item_Etc item, Vector3 itemposition)
    {
        GameObject itemobject = Instantiate(ItemManager.instance.m_gItem_Etc_Null);
        Item_Etc itemscript = itemobject.GetComponent<Item_Etc>();

        itemscript.m_bPossible_Get = false;

        itemscript.m_sItemName = item.m_sItemName;
        itemscript.m_sItemDescription = item.m_sItemDescription;
        itemscript.m_nItemCode = item.m_nItemCode;
        //itemscript.m_nItemNumber = ++ItemManager.sm_nItemNumber;
        itemscript.m_nItemNumber = 0;
        itemscript.m_sp_Sprite = item.m_sp_Sprite;
        itemscript.m_spr_SpriteRenderer = itemobject.GetComponent<SpriteRenderer>();

        itemscript.m_eItemType = E_ITEM_TYPE.ETC;
        itemscript.m_eItemEtcType = item.m_eItemEtcType;
        itemscript.m_eItemGrade = item.m_eItemGrade;

        itemscript.m_sStatus_Effect = item.m_sStatus_Effect;
        itemscript.m_sStatus_Limit_Min = item.m_sStatus_Limit_Min;
        itemscript.m_sStatus_Limit_Max = item.m_sStatus_Limit_Max;
        itemscript.m_sSoc_Effect = item.m_sSoc_Effect;
        itemscript.m_sSoc_Limit_Min = item.m_sSoc_Limit_Min;
        itemscript.m_sSoc_Limit_Max = item.m_sSoc_Limit_Max;

        itemscript.m_nPrice = item.m_nPrice;

        itemobject.transform.position = itemposition;

        itemobject.GetComponent<SpriteRenderer>().sprite = item.m_sp_Sprite;
        itemobject.name = item.m_sItemName;

        Debug.Log("ItemName: " + itemscript.m_sItemName + ", ItemNumber: " + itemscript.m_nItemNumber);

        itemscript.m_FadeinAlpa = 0;
        itemscript.Fadein();
    }

    public Item_Etc DeleteItem(Item_Etc item)
    {
        Item_Etc itemscript = new Item_Etc();

        itemscript.m_sItemName = item.m_sItemName;
        itemscript.m_sItemDescription = item.m_sItemDescription;
        itemscript.m_nItemCode = item.m_nItemCode;
        //itemscript.m_nItemNumber = item.m_nItemNumber;
        itemscript.m_nItemNumber = 0;
        itemscript.m_sp_Sprite = item.m_sp_Sprite;

        itemscript.m_eItemType = E_ITEM_TYPE.ETC;
        itemscript.m_eItemEtcType = item.m_eItemEtcType;
        itemscript.m_eItemGrade = item.m_eItemGrade;

        itemscript.m_sStatus_Effect = item.m_sStatus_Effect;
        itemscript.m_sStatus_Limit_Min = item.m_sStatus_Limit_Min;
        itemscript.m_sStatus_Limit_Max = item.m_sStatus_Limit_Max;
        itemscript.m_sSoc_Effect = item.m_sSoc_Effect;
        itemscript.m_sSoc_Limit_Min = item.m_sSoc_Limit_Min;
        itemscript.m_sSoc_Limit_Max = item.m_sSoc_Limit_Max;

        itemscript.m_nPrice = item.m_nPrice;

        Destroy(this.gameObject);

        return itemscript;
    }

    // Player 퀘스트 보상 획득 시 사용.
    public Item_Etc CreateItem(Item_Etc item)
    {
        Item_Etc itemscript = new Item_Etc();

        itemscript.m_sItemName = item.m_sItemName;
        itemscript.m_sItemDescription = item.m_sItemDescription;
        itemscript.m_nItemCode = item.m_nItemCode;
        //itemscript.m_nItemNumber = item.m_nItemNumber;
        itemscript.m_nItemNumber = 0;
        itemscript.m_sp_Sprite = item.m_sp_Sprite;

        itemscript.m_eItemType = E_ITEM_TYPE.ETC;
        itemscript.m_eItemEtcType = item.m_eItemEtcType;
        itemscript.m_eItemGrade = item.m_eItemGrade;

        itemscript.m_sStatus_Effect = item.m_sStatus_Effect;
        itemscript.m_sStatus_Limit_Min = item.m_sStatus_Limit_Min;
        itemscript.m_sStatus_Limit_Max = item.m_sStatus_Limit_Max;
        itemscript.m_sSoc_Effect = item.m_sSoc_Effect;
        itemscript.m_sSoc_Limit_Min = item.m_sSoc_Limit_Min;
        itemscript.m_sSoc_Limit_Max = item.m_sSoc_Limit_Max;

        itemscript.m_nPrice = item.m_nPrice;

        return itemscript;
    }

    // 불러오기.
    public Item_Etc LoadItem(int itemcode)
    {
        Item_Etc item = ItemManager.instance.m_Dictionary_MonsterDrop_Etc[itemcode];
        Item_Etc itemscript = new Item_Etc();

        itemscript.m_sItemName = item.m_sItemName;
        itemscript.m_sItemDescription = item.m_sItemDescription;
        itemscript.m_nItemCode = item.m_nItemCode;
        //itemscript.m_nItemNumber = item.m_nItemNumber;
        itemscript.m_nItemNumber = 0;
        itemscript.m_sp_Sprite = item.m_sp_Sprite;

        itemscript.m_eItemType = E_ITEM_TYPE.ETC;
        itemscript.m_eItemEtcType = item.m_eItemEtcType;
        itemscript.m_eItemGrade = item.m_eItemGrade;

        itemscript.m_sStatus_Effect = item.m_sStatus_Effect;
        itemscript.m_sStatus_Limit_Min = item.m_sStatus_Limit_Min;
        itemscript.m_sStatus_Limit_Max = item.m_sStatus_Limit_Max;
        itemscript.m_sSoc_Effect = item.m_sSoc_Effect;
        itemscript.m_sSoc_Limit_Min = item.m_sSoc_Limit_Min;
        itemscript.m_sSoc_Limit_Max = item.m_sSoc_Limit_Max;

        itemscript.m_nPrice = item.m_nPrice;

        return itemscript;

    }
}
