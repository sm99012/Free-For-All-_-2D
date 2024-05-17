using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ReTryslot : MonoBehaviour, IPointerClickHandler
{
    public int m_nItemCode;
    public Image m_IMG_ItemSprite;
    public TextMeshProUGUI m_TMP_ItemInformation;

    public void Set_Item(Item item, int count = 1)
    {
        this.m_nItemCode = item.m_nItemCode;
        this.m_IMG_ItemSprite.sprite = item.m_sp_Sprite;
        this.m_TMP_ItemInformation.text = item.m_sItemName;

        if (count > 1)
            this.m_TMP_ItemInformation.text += " " + count + " 개";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }
}
