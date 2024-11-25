using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Iteminfo : MonoBehaviour, IPointerClickHandler
{
    public Image m_IMG_ItemSprite;

    public TextMeshProUGUI m_TMP_ItemDescription;

    public E_ITEMSLOT m_eItemslot;

    public int m_nAryNumber;

    float m_fPanelCoordination_x;
    float m_fPanelCoordination_y;

    public void Display(Sprite sp, string str)
    {
        m_IMG_ItemSprite.sprite = sp;
        m_TMP_ItemDescription.text = str;
        
        m_nAryNumber = -1;
    }

    public void Display(Sprite sp, string str, E_ITEMSLOT eis, int arynumber)
    {
        m_IMG_ItemSprite.sprite = sp;
        m_TMP_ItemDescription.text = str;
        m_eItemslot = eis;
        m_nAryNumber = arynumber;
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (GUIManager_Total.Instance != null && GUIManager_Total.Instance.m_GUI_Itemslot_Equip_Information.m_gPanel_Itemslot_Equip_Information != null)
            {
                if (m_nAryNumber != -1)
                {
                    switch (m_eItemslot)
                    {
                        case E_ITEMSLOT.EQUIP:
                            {
                                if (Player_Itemslot.m_nary_Itemslot_Equip_Count[m_nAryNumber] != 0)
                                {
                                    GUIManager_Total.Instance.m_GUI_Itemslot_Equip_Information.m_bEquip_Condition_Check = true;
                                    GUIManager_Total.Instance.Update_Itemslot_Equip_Information(Player_Itemslot.m_gary_Itemslot_Equip[m_nAryNumber], m_nAryNumber);
                                    GUIManager_Total.Instance.m_GUI_Itemslot_Equip_Information.UseBan();

                                    GUIManager_Total.Instance.m_GUI_Itemslot_Equip_Information.m_gPanel_Itemslot_Equip_Information.transform.SetAsLastSibling();
                                    GUIManager_Total.Instance.m_GUI_Itemslot_Equip_Information.m_gPanel_Itemslot_Equip_Information.SetActive(true);

                                    m_fPanelCoordination_x = Mathf.Round(GUIManager_Total.Instance.m_GUI_Gift_GetItem_Info.m_gPanel_Standard_RectTransform.transform.position.x);
                                    m_fPanelCoordination_y = Mathf.Round(GUIManager_Total.Instance.m_GUI_Gift_GetItem_Info.m_gPanel_Standard_RectTransform.transform.position.y);
                                    GUIManager_Total.Instance.m_GUI_Itemslot_Equip_Information.m_gPanel_Itemslot_Equip_Information.transform.position = new Vector2(m_fPanelCoordination_x, m_fPanelCoordination_y);
                                }
                                if (GUIManager_Total.Instance.m_GUI_Itemslot_Use_Information.m_gPanel_Itemslot_Use_Information.activeSelf == true)
                                    GUIManager_Total.Instance.m_GUI_Itemslot_Use_Information.m_gPanel_Itemslot_Use_Information.SetActive(false);
                                if (GUIManager_Total.Instance.m_GUI_Itemslot_Etc_Information.m_gPanel_Itemslot_Etc_Information.activeSelf == true)
                                    GUIManager_Total.Instance.m_GUI_Itemslot_Etc_Information.m_gPanel_Itemslot_Etc_Information.SetActive(false);
                            } break;
                        case E_ITEMSLOT.USE:
                            {
                                if (Player_Itemslot.m_nary_Itemslot_Use_Count[m_nAryNumber] != 0)
                                {
                                    GUIManager_Total.Instance.m_GUI_Itemslot_Use_Information.m_bUse_Condition_Check = true;
                                    GUIManager_Total.Instance.Update_Itemslot_Use_Information(Player_Itemslot.m_gary_Itemslot_Use[m_nAryNumber], m_nAryNumber);
                                    GUIManager_Total.Instance.m_GUI_Itemslot_Use_Information.UseBan();

                                    GUIManager_Total.Instance.m_GUI_Itemslot_Use_Information.m_gPanel_Itemslot_Use_Information.transform.SetAsLastSibling();
                                    GUIManager_Total.Instance.m_GUI_Itemslot_Use_Information.m_gPanel_Itemslot_Use_Information.SetActive(true);

                                    m_fPanelCoordination_x = Mathf.Round(GUIManager_Total.Instance.m_GUI_Gift_GetItem_Info.m_gPanel_Standard_RectTransform.transform.position.x);
                                    m_fPanelCoordination_y = Mathf.Round(GUIManager_Total.Instance.m_GUI_Gift_GetItem_Info.m_gPanel_Standard_RectTransform.transform.position.y);
                                    GUIManager_Total.Instance.m_GUI_Itemslot_Use_Information.m_gPanel_Itemslot_Use_Information.transform.position = new Vector2(m_fPanelCoordination_x, m_fPanelCoordination_y);
                                }
                                if (GUIManager_Total.Instance.m_GUI_Itemslot_Equip_Information.m_gPanel_Itemslot_Equip_Information.activeSelf == true)
                                    GUIManager_Total.Instance.m_GUI_Itemslot_Equip_Information.m_gPanel_Itemslot_Equip_Information.SetActive(false);
                                if (GUIManager_Total.Instance.m_GUI_Itemslot_Etc_Information.m_gPanel_Itemslot_Etc_Information.activeSelf == true)
                                    GUIManager_Total.Instance.m_GUI_Itemslot_Etc_Information.m_gPanel_Itemslot_Etc_Information.SetActive(false);
                            } break;
                        case E_ITEMSLOT.ETC:
                            {
                                if (Player_Itemslot.m_nary_Itemslot_Etc_Count[m_nAryNumber] != 0)
                                {
                                    GUIManager_Total.Instance.Update_Itemslot_Etc_Information(Player_Itemslot.m_gary_Itemslot_Etc[m_nAryNumber], m_nAryNumber);

                                    GUIManager_Total.Instance.m_GUI_Itemslot_Etc_Information.m_gPanel_Itemslot_Etc_Information.transform.SetAsLastSibling();
                                    GUIManager_Total.Instance.m_GUI_Itemslot_Etc_Information.m_gPanel_Itemslot_Etc_Information.SetActive(true);

                                    m_fPanelCoordination_x = Mathf.Round(GUIManager_Total.Instance.m_GUI_Gift_GetItem_Info.m_gPanel_Standard_RectTransform.transform.position.x);
                                    m_fPanelCoordination_y = Mathf.Round(GUIManager_Total.Instance.m_GUI_Gift_GetItem_Info.m_gPanel_Standard_RectTransform.transform.position.y);
                                    GUIManager_Total.Instance.m_GUI_Itemslot_Etc_Information.m_gPanel_Itemslot_Etc_Information.transform.position = new Vector2(m_fPanelCoordination_x, m_fPanelCoordination_y);
                                }
                                if (GUIManager_Total.Instance.m_GUI_Itemslot_Equip_Information.m_gPanel_Itemslot_Equip_Information.activeSelf == true)
                                    GUIManager_Total.Instance.m_GUI_Itemslot_Equip_Information.m_gPanel_Itemslot_Equip_Information.SetActive(false);
                                if (GUIManager_Total.Instance.m_GUI_Itemslot_Use_Information.m_gPanel_Itemslot_Use_Information.activeSelf == true)
                                    GUIManager_Total.Instance.m_GUI_Itemslot_Use_Information.m_gPanel_Itemslot_Use_Information.SetActive(false);
                            } break;
                    }
                }
            }
        }
    }
}
