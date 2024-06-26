﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Equipslot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // Equipslot 에서 아이템 확인 UI.
    [SerializeField] Image m_IMG_ItemSprite;
    [SerializeField] Image m_IMG_BackgroundSprite;
    [SerializeField] Image m_IMG_RedMask;
    // 0: Hat 
    // 1: Top
    // 2: Bottoms
    // 3: Shose
    // 4: Gloves
    // 5: MainWeapon
    // 6: SubWeapon
    public int m_nAryNumber;

    float m_fPanelCoordination_x;
    float m_fPanelCoordination_y;

    // Equipslot_Equip_Information 에서 확인 할 수 있는 장비 아이템 정보 UI.
    [SerializeField] GameObject m_gPanel_Equipslot_Equip_Information;

    public void Awake()
    {
        InitialSet_Object();
    }

    // 초기 Object 불러오기.
    void InitialSet_Object()
    {
        m_IMG_ItemSprite = this.gameObject.transform.Find("Panel_ItemSprite").GetComponent<Image>();
        m_IMG_BackgroundSprite = this.gameObject.transform.Find("Panel_BackgroundSprite").GetComponent<Image>();
        m_IMG_RedMask = this.gameObject.transform.Find("Panel_RedMask").GetComponent<Image>();

        m_gPanel_Equipslot_Equip_Information = GameObject.Find("Canvas_GUI").transform.Find("Panel_Equipslot_Equip_Information").gameObject;
    }

    public void SetItem_Equip(Item_Equip item)
    {
        m_IMG_ItemSprite.sprite = item.m_sp_Sprite;
        m_IMG_ItemSprite.color = new Color(1, 1, 1, 1);
        m_IMG_RedMask.color = new Color(1, 0, 0, 0);
    }
    public void SetItem_Equip_NotApply()
    {
        m_IMG_RedMask.color = new Color(1, 0, 0, 0.33f);
    }
    public void SetNull()
    {
        m_IMG_ItemSprite.color = new Color(1, 1, 1, 0);
        m_IMG_ItemSprite.sprite = null;
        m_IMG_RedMask.color = new Color(1, 0, 0, 0);
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {

    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        switch (m_nAryNumber)
        {
            case 0:
                {
                    if (Player_Equipment.m_bEquipment_Hat == true)
                    {
                        GUIManager_Total.Instance.m_GUI_Equipslot_Equip_Information.m_bEquip_Condition_Check = true;
                        GUIManager_Total.Instance.Update_Equipslot_Equip_Information(Player_Equipment.m_gEquipment_Hat, m_nAryNumber);

                        m_gPanel_Equipslot_Equip_Information.transform.SetAsLastSibling();
                        m_gPanel_Equipslot_Equip_Information.SetActive(true);
                        //m_gPanel_Equipslot_Equip_Information.transform.position = this.gameObject.transform.position;
                        m_fPanelCoordination_x = Mathf.Round(this.gameObject.transform.position.x);
                        m_fPanelCoordination_y = Mathf.Round(this.gameObject.transform.position.y);
                        m_gPanel_Equipslot_Equip_Information.transform.position = new Vector2(m_fPanelCoordination_x, m_fPanelCoordination_y);
                    }
                    else
                        m_gPanel_Equipslot_Equip_Information.SetActive(false);
                }
                break;
            case 1:
                {
                    if (Player_Equipment.m_bEquipment_Top == true)
                    {
                        GUIManager_Total.Instance.m_GUI_Equipslot_Equip_Information.m_bEquip_Condition_Check = true;
                        GUIManager_Total.Instance.Update_Equipslot_Equip_Information(Player_Equipment.m_gEquipment_Top, m_nAryNumber);

                        m_gPanel_Equipslot_Equip_Information.transform.SetAsLastSibling();
                        m_gPanel_Equipslot_Equip_Information.SetActive(true);
                        //m_gPanel_Equipslot_Equip_Information.transform.position = this.gameObject.transform.position;
                        m_fPanelCoordination_x = Mathf.Round(this.gameObject.transform.position.x);
                        m_fPanelCoordination_y = Mathf.Round(this.gameObject.transform.position.y);
                        m_gPanel_Equipslot_Equip_Information.transform.position = new Vector2(m_fPanelCoordination_x, m_fPanelCoordination_y);
                    }
                    else
                        m_gPanel_Equipslot_Equip_Information.SetActive(false);
                }
                break;
            case 2:
                {
                    if (Player_Equipment.m_bEquipment_Bottoms == true)
                    {
                        GUIManager_Total.Instance.m_GUI_Equipslot_Equip_Information.m_bEquip_Condition_Check = true;
                        GUIManager_Total.Instance.Update_Equipslot_Equip_Information(Player_Equipment.m_gEquipment_Bottoms, m_nAryNumber);

                        m_gPanel_Equipslot_Equip_Information.transform.SetAsLastSibling();
                        m_gPanel_Equipslot_Equip_Information.SetActive(true);
                        //m_gPanel_Equipslot_Equip_Information.transform.position = this.gameObject.transform.position;
                        m_fPanelCoordination_x = Mathf.Round(this.gameObject.transform.position.x);
                        m_fPanelCoordination_y = Mathf.Round(this.gameObject.transform.position.y);
                        //m_gPanel_Equipslot_Equip_Information.transform.position = new Vector2(m_fPanelCoordination_x, m_fPanelCoordination_y);
                        m_fPanelCoordination_x = Mathf.Round(this.gameObject.transform.position.x);
                        m_fPanelCoordination_y = Mathf.Round(this.gameObject.transform.position.y);
                        m_gPanel_Equipslot_Equip_Information.transform.position = new Vector2(m_fPanelCoordination_x, m_fPanelCoordination_y);
                    }
                    else
                        m_gPanel_Equipslot_Equip_Information.SetActive(false);
                }
                break;
            case 3:
                {
                    if (Player_Equipment.m_bEquipment_Shose == true)
                    {
                        GUIManager_Total.Instance.m_GUI_Equipslot_Equip_Information.m_bEquip_Condition_Check = true;
                        GUIManager_Total.Instance.Update_Equipslot_Equip_Information(Player_Equipment.m_gEquipment_Shose, m_nAryNumber);

                        m_gPanel_Equipslot_Equip_Information.transform.SetAsLastSibling();
                        m_gPanel_Equipslot_Equip_Information.SetActive(true);
                        //m_gPanel_Equipslot_Equip_Information.transform.position = this.gameObject.transform.position;
                        m_fPanelCoordination_x = Mathf.Round(this.gameObject.transform.position.x);
                        m_fPanelCoordination_y = Mathf.Round(this.gameObject.transform.position.y);
                        m_gPanel_Equipslot_Equip_Information.transform.position = new Vector2(m_fPanelCoordination_x, m_fPanelCoordination_y);
                    }
                    else
                        m_gPanel_Equipslot_Equip_Information.SetActive(false);
                }
                break;
            case 4:
                {
                    if (Player_Equipment.m_bEquipment_Gloves == true)
                    {
                        GUIManager_Total.Instance.m_GUI_Equipslot_Equip_Information.m_bEquip_Condition_Check = true;
                        GUIManager_Total.Instance.Update_Equipslot_Equip_Information(Player_Equipment.m_gEquipment_Gloves, m_nAryNumber);

                        m_gPanel_Equipslot_Equip_Information.transform.SetAsLastSibling();
                        m_gPanel_Equipslot_Equip_Information.SetActive(true);
                        //m_gPanel_Equipslot_Equip_Information.transform.position = this.gameObject.transform.position;
                        m_fPanelCoordination_x = Mathf.Round(this.gameObject.transform.position.x);
                        m_fPanelCoordination_y = Mathf.Round(this.gameObject.transform.position.y);
                        m_gPanel_Equipslot_Equip_Information.transform.position = new Vector2(m_fPanelCoordination_x, m_fPanelCoordination_y);
                    }
                    else
                        m_gPanel_Equipslot_Equip_Information.SetActive(false);
                }
                break;
            case 5:
                {
                    if (Player_Equipment.m_bEquipment_Mainweapon == true)
                    {
                        GUIManager_Total.Instance.m_GUI_Equipslot_Equip_Information.m_bEquip_Condition_Check = true;
                        GUIManager_Total.Instance.Update_Equipslot_Equip_Information(Player_Equipment.m_gEquipment_Mainweapon, m_nAryNumber);

                        m_gPanel_Equipslot_Equip_Information.transform.SetAsLastSibling();
                        m_gPanel_Equipslot_Equip_Information.SetActive(true);
                        //m_gPanel_Equipslot_Equip_Information.transform.position = this.gameObject.transform.position;
                        m_fPanelCoordination_x = Mathf.Round(this.gameObject.transform.position.x);
                        m_fPanelCoordination_y = Mathf.Round(this.gameObject.transform.position.y);
                        m_gPanel_Equipslot_Equip_Information.transform.position = new Vector2(m_fPanelCoordination_x, m_fPanelCoordination_y);
                    }
                    else
                        m_gPanel_Equipslot_Equip_Information.SetActive(false);
                }
                break;
            case 6:
                {
                    if (Player_Equipment.m_bEquipment_Subweapon == true)
                    {
                        GUIManager_Total.Instance.m_GUI_Equipslot_Equip_Information.m_bEquip_Condition_Check = true;
                        GUIManager_Total.Instance.Update_Equipslot_Equip_Information(Player_Equipment.m_gEquipment_Subweapon, m_nAryNumber);

                        m_gPanel_Equipslot_Equip_Information.transform.SetAsLastSibling();
                        m_gPanel_Equipslot_Equip_Information.SetActive(true);
                        //m_gPanel_Equipslot_Equip_Information.transform.position = this.gameObject.transform.position;
                        m_fPanelCoordination_x = Mathf.Round(this.gameObject.transform.position.x);
                        m_fPanelCoordination_y = Mathf.Round(this.gameObject.transform.position.y);
                        m_gPanel_Equipslot_Equip_Information.transform.position = new Vector2(m_fPanelCoordination_x, m_fPanelCoordination_y);
                    }
                    else
                        m_gPanel_Equipslot_Equip_Information.SetActive(false);
                }
                break;
        }
    }
}
