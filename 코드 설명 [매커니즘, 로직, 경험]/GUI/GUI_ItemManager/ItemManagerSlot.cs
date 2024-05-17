using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ItemManagerSlot : MonoBehaviour, IPointerClickHandler
{
    public Image m_IMG_ItemManager_Slot_Sprite;

    [SerializeField] ItemType m_eItemtype;
    [SerializeField] int m_nItemCode;

    public void SetItemInformation(Item item)
    {
        m_IMG_ItemManager_Slot_Sprite.color = new Color(1, 1, 1, 1);
        m_IMG_ItemManager_Slot_Sprite.sprite = item.m_sp_Sprite;
        m_eItemtype = item.m_eItemtype;
        m_nItemCode = item.m_nItemCode;
    }

    public void OnPointerClick(PointerEventData eventData)
    { 
        switch(m_eItemtype)
        {
            case ItemType.EQUIP:
                {
                    Item item = new Item_Equip(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[m_nItemCode], Player_Total.Instance.gameObject.transform.position);
                    Destroy(item);
                } break;
            case ItemType.USE:
                {
                    Item item = new Item_Use(ItemManager.instance.m_Dictionary_MonsterDrop_Use[m_nItemCode], Player_Total.Instance.gameObject.transform.position);
                    Destroy(item);
                } break;
            case ItemType.ETC:
                {
                    Item item = new Item_Etc(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[m_nItemCode], Player_Total.Instance.gameObject.transform.position);
                    Destroy(item);
                } break;
        }
    }
}
