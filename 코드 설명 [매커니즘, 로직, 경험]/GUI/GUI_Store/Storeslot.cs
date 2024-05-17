using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public enum E_STORESLOT_EYPE { SALE, BUY }

public class Storeslot : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    // Storeslot 에서 판매, 구매 할 아이템 확인 UI.
    [SerializeField] Image m_IMG_ItemSprite;
    [SerializeField] Image m_IMG_BackgroundSprite;

    [SerializeField] TextMeshProUGUI m_TMP_ItemCount;

    [SerializeField] GameObject m_gPanel_Item_Equip_Information;
    [SerializeField] GameObject m_gPanel_Item_Use_Information;
    [SerializeField] GameObject m_gPanel_Item_Etc_Information;

    public E_STORESLOT_EYPE m_e_StoreSlot_Type;

    public int m_nAryNumber;

    private void Awake()
    {
        InitialSet_Object();
    }

    void InitialSet_Object()
    {

        m_IMG_ItemSprite = this.gameObject.transform.Find("Panel_ItemSprite").GetComponent<Image>();
        m_IMG_BackgroundSprite = this.gameObject.transform.Find("Panel_BackgroundSprite").GetComponent<Image>();
        m_TMP_ItemCount = this.gameObject.transform.Find("Text_Count").GetComponent<TextMeshProUGUI>();
    }

    public void SetItem_Equip(Item_Equip item, int count)
    {
        m_IMG_ItemSprite.sprite = item.m_sp_Sprite;
        m_IMG_ItemSprite.color = new Color(1, 1, 1, 1);
        m_TMP_ItemCount.text = count.ToString();
    }
    public void SetItem_Use(Item_Use item, int count)
    {
        m_IMG_ItemSprite.sprite = item.m_sp_Sprite;
        m_IMG_ItemSprite.color = new Color(1, 1, 1, 1);
        m_TMP_ItemCount.text = count.ToString();
    }
    public void SetItem_Etc(Item_Etc item, int count)
    {
        m_IMG_ItemSprite.sprite = item.m_sp_Sprite;
        m_IMG_ItemSprite.color = new Color(1, 1, 1, 1);
        m_TMP_ItemCount.text = count.ToString();
    }
    public void SetNull()
    {
        m_IMG_ItemSprite.color = new Color(1, 1, 1, 0);
        m_IMG_ItemSprite.sprite = null;
        m_TMP_ItemCount.text = null;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {

    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (m_e_StoreSlot_Type == E_STORESLOT_EYPE.SALE)
        {
            if (GUIManager_Total.Instance.m_GUI_Store.m_bNPC_Store == true)
            {
                if (GUIManager_Total.Instance.m_GUI_Store.m_e_Sale_Type == E_ITEMSLOT.EQUIP)
                {
                    if (GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Sale_Item_Equip_Current.Count > m_nAryNumber)
                    {
                        GUIManager_Total.Instance.Display_GUI_Store_Simple_Item_Information(E_STORESLOT_EYPE.SALE, E_ITEMSLOT.EQUIP, m_nAryNumber, this.transform.position);
                        GUIManager_Total.Instance.Display_GUI_Store_Item_Information(GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Sale_Item_Equip_Current[m_nAryNumber]);
                    }
                    else
                    {
                        GUIManager_Total.Instance.m_GUI_Store_Simple_Item_Information.Exit();
                        GUIManager_Total.Instance.m_GUI_Store_Item_Information.Exit();
                    }
                }
                else if (GUIManager_Total.Instance.m_GUI_Store.m_e_Sale_Type == E_ITEMSLOT.USE)
                {
                    if (GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Sale_Item_Use_Current.Count > m_nAryNumber)
                    {
                        GUIManager_Total.Instance.Display_GUI_Store_Simple_Item_Information(E_STORESLOT_EYPE.SALE, E_ITEMSLOT.USE, m_nAryNumber, this.transform.position);
                        GUIManager_Total.Instance.Display_GUI_Store_Item_Information(GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Sale_Item_Use_Current[m_nAryNumber]);
                    }
                    else
                    {
                        GUIManager_Total.Instance.m_GUI_Store_Simple_Item_Information.Exit();
                        GUIManager_Total.Instance.m_GUI_Store_Item_Information.Exit();
                    }
                }
                else if (GUIManager_Total.Instance.m_GUI_Store.m_e_Sale_Type == E_ITEMSLOT.ETC)
                {
                    if (GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Sale_Item_Etc_Current.Count > m_nAryNumber)
                    {
                        GUIManager_Total.Instance.Display_GUI_Store_Simple_Item_Information(E_STORESLOT_EYPE.SALE, E_ITEMSLOT.ETC, m_nAryNumber, this.transform.position);
                        GUIManager_Total.Instance.Display_GUI_Store_Item_Information(GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Sale_Item_Etc_Current[m_nAryNumber]);
                    }
                    else
                    {
                        GUIManager_Total.Instance.m_GUI_Store_Simple_Item_Information.Exit();
                        GUIManager_Total.Instance.m_GUI_Store_Item_Information.Exit();
                    }
                }
            }
        }
        else if (m_e_StoreSlot_Type == E_STORESLOT_EYPE.BUY)
        {
            if (GUIManager_Total.Instance.m_GUI_Store.m_bNPC_Store == true)
            {
                if (GUIManager_Total.Instance.m_GUI_Store.m_e_Buy_Type == E_ITEMSLOT.EQUIP)
                {
                    if (Player_Itemslot.m_nary_Itemslot_Equip_Count[m_nAryNumber] > 0)
                    {
                        GUIManager_Total.Instance.Display_GUI_Store_Simple_Item_Information(E_STORESLOT_EYPE.BUY, E_ITEMSLOT.EQUIP, m_nAryNumber, this.transform.position);
                        GUIManager_Total.Instance.Display_GUI_Store_Item_Information(Player_Itemslot.m_gary_Itemslot_Equip[m_nAryNumber]);
                    }
                    else
                    {
                        GUIManager_Total.Instance.m_GUI_Store_Simple_Item_Information.Exit();
                        GUIManager_Total.Instance.m_GUI_Store_Item_Information.Exit();
                    }
                }
                else if (GUIManager_Total.Instance.m_GUI_Store.m_e_Buy_Type == E_ITEMSLOT.USE)
                {
                    if (Player_Itemslot.m_nary_Itemslot_Use_Count[m_nAryNumber] > 0)
                    {
                        GUIManager_Total.Instance.Display_GUI_Store_Simple_Item_Information(E_STORESLOT_EYPE.BUY, E_ITEMSLOT.USE, m_nAryNumber, this.transform.position);
                        GUIManager_Total.Instance.Display_GUI_Store_Item_Information(Player_Itemslot.m_gary_Itemslot_Use[m_nAryNumber]);
                    }
                    else
                    {
                        GUIManager_Total.Instance.m_GUI_Store_Simple_Item_Information.Exit();
                        GUIManager_Total.Instance.m_GUI_Store_Item_Information.Exit();
                    }
                }
                else if (GUIManager_Total.Instance.m_GUI_Store.m_e_Buy_Type == E_ITEMSLOT.ETC)
                {
                    if (Player_Itemslot.m_nary_Itemslot_Etc_Count[m_nAryNumber] > 0)
                    {
                        GUIManager_Total.Instance.Display_GUI_Store_Simple_Item_Information(E_STORESLOT_EYPE.BUY, E_ITEMSLOT.ETC, m_nAryNumber, this.transform.position);
                        GUIManager_Total.Instance.Display_GUI_Store_Item_Information(Player_Itemslot.m_gary_Itemslot_Etc[m_nAryNumber]);
                    }
                    else
                    {
                        GUIManager_Total.Instance.m_GUI_Store_Simple_Item_Information.Exit();
                        GUIManager_Total.Instance.m_GUI_Store_Item_Information.Exit();
                    }
                }
            }
        }
    }
}
