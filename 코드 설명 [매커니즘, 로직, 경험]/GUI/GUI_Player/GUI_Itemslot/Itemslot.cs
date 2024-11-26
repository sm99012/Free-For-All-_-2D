using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Itemslot : MonoBehaviour, IPointerClickHandler
{
    // Itemslot 에서 아이템 확인 UI.
    [SerializeField] Image m_IMG_ItemSprite;
    [SerializeField] Image m_IMG_BackgroundSprite;

    [SerializeField] TextMeshProUGUI m_TMP_ItemCount;

    float m_fPanelCoordination_x;
    float m_fPanelCoordination_y;

    public int m_nAryNumber;

    public void Awake()
    {
        InitialSet_Object();
    }

    // 초기 Object 불러오기.
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

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //if (GUIManager_Total.Instance != null && m_gPanel_Itemslot_Equip_Information != null)
            if (GUIManager_Total.Instance != null)
            {
                if (GUIManager_Total.Instance.m_GUI_Itemslot.m_eItemslot == E_ITEMSLOT.EQUIP)
                {
                    if (Player_Itemslot.m_nary_Itemslot_Equip_Count[m_nAryNumber] != 0)
                    {
                        GUIManager_Total.Instance.Update_Itemslot_Equip_Information(Player_Itemslot.m_gary_Itemslot_Equip[m_nAryNumber], m_nAryNumber);
                        GUIManager_Total.Instance.Display_GUI_Itemslot_Equip_Information(Mathf.Round(this.gameObject.transform.position.x), Mathf.Round(this.gameObject.transform.position.y));

                        GUIManager_Total.Instance.UnDisplay_GUI_Quickslot_Signup();
                    }
                    else
                    {
                        GUIManager_Total.Instance.UnDisplay_GUI_Itemslot_Equip_Information();
                    }
                }
                else if (GUIManager_Total.Instance.m_GUI_Itemslot.m_eItemslot == E_ITEMSLOT.USE)
                {
                    if (Player_Itemslot.m_nary_Itemslot_Use_Count[m_nAryNumber] != 0)
                    {
                        GUIManager_Total.Instance.Update_Itemslot_Use_Information(Player_Itemslot.m_gary_Itemslot_Use[m_nAryNumber], m_nAryNumber);
                        GUIManager_Total.Instance.Display_GUI_Itemslot_Use_Information(Mathf.Round(this.gameObject.transform.position.x), Mathf.Round(this.gameObject.transform.position.y));

                        GUIManager_Total.Instance.UnDisplay_GUI_Quickslot_Signup();
                    }
                    else
                    {
                        GUIManager_Total.Instance.UnDisplay_GUI_Itemslot_Use_Information();
                    }
                }
                else if (GUIManager_Total.Instance.m_GUI_Itemslot.m_eItemslot == E_ITEMSLOT.ETC)
                {
                    if (Player_Itemslot.m_nary_Itemslot_Etc_Count[m_nAryNumber] != 0)
                    {
                        GUIManager_Total.Instance.Update_Itemslot_Etc_Information(Player_Itemslot.m_gary_Itemslot_Etc[m_nAryNumber], m_nAryNumber);

                        GUIManager_Total.Instance.Display_GUI_Itemslot_Etc_Information(Mathf.Round(this.gameObject.transform.position.x), Mathf.Round(this.gameObject.transform.position.y));

                        GUIManager_Total.Instance.UnDisplay_GUI_Quickslot_Signup();
                    }
                    else
                    {
                        GUIManager_Total.Instance.UnDisplay_GUI_Itemslot_Etc_Information();
                    }
                }
            }
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (GUIManager_Total.Instance.m_GUI_Itemslot.m_eItemslot == E_ITEMSLOT.EQUIP)
                if (Player_Itemslot.m_nary_Itemslot_Equip_Count[m_nAryNumber] != 0)
                    if (GUIManager_Total.Instance != null && GUIManager_Total.Instance.m_GUI_Quickslot_Signup != null)
                    {
                        if (Player_Itemslot.m_nary_Itemslot_Equip_Count[m_nAryNumber] != 0)
                        {
                            GUIManager_Total.Instance.Display_GUI_Quickslot_Signup(E_QUICKSLOT_CATEGORY.EQUIP, m_nAryNumber, Mathf.Round(this.transform.position.x), Mathf.Round(this.transform.position.y));

                            GUIManager_Total.Instance.UnDisplay_GUI_Itemslot_Equip_Information();
                            GUIManager_Total.Instance.UnDisplay_GUI_Itemslot_Use_Information();
                            GUIManager_Total.Instance.UnDisplay_GUI_Itemslot_Etc_Information();
                        }
                        //else
                        //{
                        //    GUIManager_Total.Instance.UnDisplay_GUI_Quickslot_Signup();
                        //}
                    }
            if (GUIManager_Total.Instance.m_GUI_Itemslot.m_eItemslot == E_ITEMSLOT.USE)
                if (Player_Itemslot.m_nary_Itemslot_Use_Count[m_nAryNumber] != 0)
                    if (GUIManager_Total.Instance != null && GUIManager_Total.Instance.m_GUI_Quickslot_Signup != null)
                    {
                        if (Player_Itemslot.m_nary_Itemslot_Use_Count[m_nAryNumber] != 0)
                        {
                            GUIManager_Total.Instance.Display_GUI_Quickslot_Signup(E_QUICKSLOT_CATEGORY.USE, Player_Itemslot.m_gary_Itemslot_Use[m_nAryNumber].m_nItemCode, Mathf.Round(this.transform.position.x), Mathf.Round(this.transform.position.y));

                            GUIManager_Total.Instance.UnDisplay_GUI_Itemslot_Equip_Information();
                            GUIManager_Total.Instance.UnDisplay_GUI_Itemslot_Use_Information();
                            GUIManager_Total.Instance.UnDisplay_GUI_Itemslot_Etc_Information();
                        }
                        //else
                        //{
                        //    GUIManager_Total.Instance.UnDisplay_GUI_Quickslot_Signup();
                        //}
                    }
            if (GUIManager_Total.Instance.m_GUI_Itemslot.m_eItemslot == E_ITEMSLOT.ETC)
                if (Player_Itemslot.m_nary_Itemslot_Etc_Count[m_nAryNumber] != 0)
                    if (GUIManager_Total.Instance != null && GUIManager_Total.Instance.m_GUI_Quickslot_Signup != null)
                    {
                        if (Player_Itemslot.m_nary_Itemslot_Etc_Count[m_nAryNumber] != 0)
                        {
                            GUIManager_Total.Instance.Display_GUI_Quickslot_Signup(E_QUICKSLOT_CATEGORY.ETC, Player_Itemslot.m_gary_Itemslot_Etc[m_nAryNumber].m_nItemCode, Mathf.Round(this.transform.position.x), Mathf.Round(this.transform.position.y));

                            GUIManager_Total.Instance.UnDisplay_GUI_Itemslot_Equip_Information();
                            GUIManager_Total.Instance.UnDisplay_GUI_Itemslot_Use_Information();
                            GUIManager_Total.Instance.UnDisplay_GUI_Itemslot_Etc_Information();
                        }
                        //else
                        //{
                        //    GUIManager_Total.Instance.UnDisplay_GUI_Quickslot_Signup();
                        //}
                    }
        }
    }
}
