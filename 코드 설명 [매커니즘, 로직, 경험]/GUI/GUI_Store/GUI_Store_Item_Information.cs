using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GUI_Store_Item_Information : MonoBehaviour
{
    [SerializeField] GameObject m_gPanel_Store;

    [SerializeField] GameObject m_gPanel_Store_Item_Information;

    [SerializeField] GameObject m_gPanel_Store_Item_Information_Content;

    [SerializeField] GameObject m_gPanel_Store_Item_Information_C_UpBar;
    [SerializeField] TextMeshProUGUI m_TMP_Store_Item_Information_C_UpBar_Name;

    [SerializeField] GameObject m_gPanel_Store_Item_Information_UpBar;
    [SerializeField] TextMeshProUGUI m_TMP_Store_Item_Information_UpBar_Name;

    [SerializeField] GameObject m_gPanel_Store_Item_Information_Image;
    [SerializeField] Image m_IMG_Store_Item_Information_Image_ItemSprite;

    [SerializeField] GameObject m_gPanel_Store_Item_Information_ItemInformation;
    [SerializeField] TextMeshProUGUI m_TMP_Store_Item_Information_ItemInformation;

    [SerializeField] GameObject m_gPanel_Store_Item_Information_Effect;
    [SerializeField] GameObject m_gPanel_Store_Item_Information_Effect_Name;
    [SerializeField] TextMeshProUGUI m_TMP_Store_Item_Information_Effect_Name;
    [SerializeField] GameObject m_gPanel_Store_Item_Information_Effect_Status;
    [SerializeField] TextMeshProUGUI m_TMP_Store_Item_Information_Effect_Status_L;
    [SerializeField] TextMeshProUGUI m_TMP_Store_Item_Information_Effect_Status_R;
    [SerializeField] GameObject m_gPanel_Store_Item_Information_Effect_Soc;
    [SerializeField] TextMeshProUGUI m_TMP_Store_Item_Information_Effect_Soc_L;
    [SerializeField] TextMeshProUGUI m_TMP_Store_Item_Information_Effect_Soc_R;

    [SerializeField] GameObject m_gPanel_Store_Item_Information_Condition;
    [SerializeField] GameObject m_gPanel_Store_Item_Information_Condition_Name;
    [SerializeField] TextMeshProUGUI m_TMP_Store_Item_Information_Condition_Name;
    [SerializeField] GameObject m_gPanel_Store_Item_Information_Condition_Status;
    [SerializeField] TextMeshProUGUI m_TMP_Store_Item_Information_Condition_Status_L;
    [SerializeField] TextMeshProUGUI m_TMP_Store_Item_Information_Condition_Status_R;
    [SerializeField] GameObject m_gPanel_Store_Item_Information_Condition_Soc;
    [SerializeField] TextMeshProUGUI m_TMP_Store_Item_Information_Condition_Soc_L;
    [SerializeField] TextMeshProUGUI m_TMP_Store_Item_Information_Condition_Soc_R;

    [SerializeField] GameObject m_gPanel_Store_Item_Information_ItemDescription;

    [SerializeField] GameObject m_gPanel_Store_Item_Information_ItemDescription_Content;
    [SerializeField] GameObject m_gSV_Store_Item_Information_ItemDescription_Content;
    [SerializeField] GameObject m_gViewport_Store_Item_Equip_Information_ItemDescription_Content;
    [SerializeField] TextMeshProUGUI m_TMP_Store_Item_Information_ItemDescription_Content;
    [SerializeField] Scrollbar m_Scrollbar_Store_Item_Information_ItemDescription_Content;

    [SerializeField] Button m_BTN_Store_Item_Information_ChangeInformation_R;
    [SerializeField] Button m_BTN_Store_Item_Information_ChangeInformation_L;

    [SerializeField] GameObject m_gPanel_Store_Information_CanUse;
    [SerializeField] TextMeshProUGUI m_TMP_Store_Information_CanUse;

    [SerializeField] GameObject m_gPanel_SetItemEffect;
    [SerializeField] GameObject m_gPanel_SetItemEffect_Name;
    [SerializeField] TextMeshProUGUI m_TMP_SetItemEffect_Name;
    [Space(5)]
    [SerializeField] GameObject m_gPanel_SetItemEffect_Content;
    [Space(5)]
    [SerializeField] GameObject m_gPanel_SetItemEffect_Content_UpBar;
    [SerializeField] TextMeshProUGUI m_TMP_SetItemEffect_Content_UpBar_Name;
    [SerializeField] GameObject m_gBTN_SetItemEffect_Content_UpBar_Left;
    [SerializeField] Button m_BTN_SetItemEffect_Content_UpBar_Left;
    [SerializeField] GameObject m_gBTN_SetItemEffect_Content_UpBar_Right;
    [SerializeField] Button m_BTN_SetItemEffect_Content_UpBar_Right;
    [Space(5)]
    [SerializeField] GameObject m_gPanel_SetItemEffect_Content_SS;
    [SerializeField] GameObject m_gPanel_SetItemEffect_Content_SS_Description;
    [SerializeField] GameObject m_gSV_SetItemEffect_Content_SS_Description;
    [SerializeField] GameObject m_gViewport_SetItemEffect_Content_SS_Description;
    [SerializeField] TextMeshProUGUI m_TMP_SetItemEffect_Content_SS_Description;
    [SerializeField] Scrollbar m_Scrollbar_SetItemEffect_Content_SS_Description;

    [SerializeField] GameObject m_gPanel_SetItemEffect_Content_SS_Status;
    [SerializeField] TextMeshProUGUI m_TMP_SetItemEffect_Content_SS_Status_L;
    [SerializeField] TextMeshProUGUI m_TMP_SetItemEffect_Content_SS_Status_R;
    [SerializeField] GameObject m_gPanel_SetItemEffect_Content_SS_Soc;
    [SerializeField] TextMeshProUGUI m_TMP_SetItemEffect_Content_SS_Soc_L;
    [SerializeField] TextMeshProUGUI m_TMP_SetItemEffect_Content_SS_Soc_R;

    [SerializeField] GameObject m_gPanel_NoItemInfo;
    [SerializeField] TextMeshProUGUI m_TMP_NoItemInfo;

    int m_nSetItemEffect_Count;
    int m_nSetItemEffect_Current;
    [SerializeField] List<int> m_nList_SetItemEffect_Code;
    int m_nSetItemEffect_List_Index;
    int m_nPlayerEquipment_SetItemEffect_Current;

    ItemSetEffect m_ISE;

    // 기프트 관련.

    [SerializeField] GameObject m_gPanel_Gift_Info;

    [SerializeField] GameObject m_gPanel_Gift_Info_Content;

    [SerializeField] GameObject m_gPanel_Gift_Info_Content_Type;
    [SerializeField] TextMeshProUGUI m_TMP_Gift_Info_Content_Type_Name;

    [SerializeField] GameObject m_gPanel_Gift_Info_Content_Description;

    [SerializeField] GameObject m_gPanel_Gift_Info_Content_Description_Up;
    [SerializeField] TextMeshProUGUI m_TMP_Gift_Info_Content_Description_Up;

    [SerializeField] GameObject m_gPanel_Gift_Info_Content_Description_Down;
    [SerializeField] GameObject m_gSV_Gift_Info_Content_Description_Down;
    [SerializeField] GameObject m_gViewport_Gift_Info_Content_Description_Down;
    [SerializeField] GameObject m_gContent_Gift_Info_Content_Description_Down;
    [SerializeField] Scrollbar m_Scrollbar_Gift_Info_Content_Description_Down;

    [SerializeField] GameObject m_gPanel_Gift_Info_Content_Description_X;

    [SerializeField] GameObject m_gPanel_ItemInfo;
    List<GameObject> m_gList_Panel_ItemInfo;

    // 기능.
    public enum E_STORE_ITEM_INFORMATION { EFFECT, CONDITION, DESCRIPTION }
    public E_STORE_ITEM_INFORMATION m_e_Store_Item_Information;

    // Text 임시 저장.
    string m_sBuffer;
    // Color.
    string m_sColor_White = "<color=#ffffff>";
    string m_sColor_WhiteGray = "<color=#808080>";
    string m_sColor_Red = "<color=#FF0000>";
    string m_sColor_Brown = "<color=#915446>";
    string m_sColor_End = "</color>";

    // Text 정제 관련 변수.
    bool m_bRefine_Condition_Check;
    // 장비 착용 가능 여부.
    public bool m_bEU_Condition_Check;

    // 상점 갱신 시간 관련 변수.
    int m_nMinute;
    int m_nSecond;

    Player_Status player;

    public void InitialSet()
    {
        InitialSet_Object();
        InitialSet_Button();

        m_e_Store_Item_Information = E_STORE_ITEM_INFORMATION.DESCRIPTION;
    }

    void InitialSet_Object()
    {
        m_gPanel_Store = GameObject.Find("Canvas_GUI").transform.Find("Panel_Store").gameObject;

        m_gPanel_Store_Item_Information = m_gPanel_Store.transform.Find("Panel_Store_Item_Information").gameObject;

        m_gPanel_Store_Item_Information_Content = m_gPanel_Store_Item_Information.transform.Find("Panel_Store_Item_Information_Content").gameObject;

        m_gPanel_Store_Item_Information_C_UpBar = m_gPanel_Store_Item_Information.transform.Find("Panel_Store_Item_Information_UpBar").gameObject;
        m_TMP_Store_Item_Information_C_UpBar_Name = m_gPanel_Store_Item_Information_C_UpBar.transform.Find("TMP_Store_Item_Information_UpBar_Timer").gameObject.GetComponent<TextMeshProUGUI>();

        m_gPanel_Store_Item_Information_UpBar = m_gPanel_Store_Item_Information_Content.transform.Find("Panel_Store_Item_Information_UpBar").gameObject;
        m_TMP_Store_Item_Information_UpBar_Name = m_gPanel_Store_Item_Information_UpBar.transform.Find("TMP_Store_Item_Information_UpBar_Name").gameObject.GetComponent<TextMeshProUGUI>();

        m_gPanel_Store_Item_Information_Image = m_gPanel_Store_Item_Information_Content.transform.Find("Panel_Store_Item_Information_Image").gameObject;
        m_IMG_Store_Item_Information_Image_ItemSprite = m_gPanel_Store_Item_Information_Image.transform.Find("Panel_Store_Item_Information_Image_ItemSprite").gameObject.GetComponent<Image>();

        m_gPanel_Store_Item_Information_ItemInformation = m_gPanel_Store_Item_Information_Content.transform.Find("Panel_Store_Item_Information_ItemInformation").gameObject;
        m_TMP_Store_Item_Information_ItemInformation = m_gPanel_Store_Item_Information_ItemInformation.transform.Find("TMP_Store_Item_Information_ItemInformation").gameObject.GetComponent<TextMeshProUGUI>();

        m_gPanel_Store_Item_Information_Effect = m_gPanel_Store_Item_Information_Content.transform.Find("Panel_Store_Item_Information_Effect").gameObject;
        m_gPanel_Store_Item_Information_Effect_Name = m_gPanel_Store_Item_Information_Effect.transform.Find("Panel_Store_Item_Information_Effect_Name").gameObject;
        m_TMP_Store_Item_Information_Effect_Name = m_gPanel_Store_Item_Information_Effect_Name.transform.Find("TMP_Store_Item_Information_Effect_Name").gameObject.GetComponent<TextMeshProUGUI>();
        m_gPanel_Store_Item_Information_Effect_Status = m_gPanel_Store_Item_Information_Effect.transform.Find("Panel_Store_Item_Information_Effect_Status").gameObject;
        m_TMP_Store_Item_Information_Effect_Status_L = m_gPanel_Store_Item_Information_Effect_Status.transform.Find("TMP_Store_Item_Information_Effect_Status_L").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_Store_Item_Information_Effect_Status_R = m_gPanel_Store_Item_Information_Effect_Status.transform.Find("TMP_Store_Item_Information_Effect_Status_R").gameObject.GetComponent<TextMeshProUGUI>();
        m_gPanel_Store_Item_Information_Effect_Soc = m_gPanel_Store_Item_Information_Effect.transform.Find("Panel_Store_Item_Information_Effect_Soc").gameObject;
        m_TMP_Store_Item_Information_Effect_Soc_L = m_gPanel_Store_Item_Information_Effect_Soc.transform.Find("TMP_Store_Item_Information_Effect_Soc_L").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_Store_Item_Information_Effect_Soc_R = m_gPanel_Store_Item_Information_Effect_Soc.transform.Find("TMP_Store_Item_Information_Effect_Soc_R").gameObject.GetComponent<TextMeshProUGUI>();

        m_gPanel_Store_Item_Information_Condition = m_gPanel_Store_Item_Information_Content.transform.Find("Panel_Store_Item_Information_Condition").gameObject;
        m_gPanel_Store_Item_Information_Condition_Name = m_gPanel_Store_Item_Information_Condition.transform.Find("Panel_Store_Item_Information_Condition_Name").gameObject;
        m_TMP_Store_Item_Information_Condition_Name = m_gPanel_Store_Item_Information_Condition_Name.transform.Find("TMP_Store_Item_Information_Condition_Name").gameObject.GetComponent<TextMeshProUGUI>();
        m_gPanel_Store_Item_Information_Condition_Status = m_gPanel_Store_Item_Information_Condition.transform.Find("Panel_Store_Item_Information_Condition_Status").gameObject;
        m_TMP_Store_Item_Information_Condition_Status_L = m_gPanel_Store_Item_Information_Condition_Status.transform.Find("TMP_Store_Item_Information_Condition_Status_L").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_Store_Item_Information_Condition_Status_R = m_gPanel_Store_Item_Information_Condition_Status.transform.Find("TMP_Store_Item_Information_Condition_Status_R").gameObject.GetComponent<TextMeshProUGUI>();
        m_gPanel_Store_Item_Information_Condition_Soc = m_gPanel_Store_Item_Information_Condition.transform.Find("Panel_Store_Item_Information_Condition_Soc").gameObject;
        m_TMP_Store_Item_Information_Condition_Soc_L = m_gPanel_Store_Item_Information_Condition_Soc.transform.Find("TMP_Store_Item_Information_Condition_Soc_L").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_Store_Item_Information_Condition_Soc_R = m_gPanel_Store_Item_Information_Condition_Soc.transform.Find("TMP_Store_Item_Information_Condition_Soc_R").gameObject.GetComponent<TextMeshProUGUI>();

        m_gPanel_Store_Item_Information_ItemDescription = m_gPanel_Store_Item_Information_Content.transform.Find("Panel_Store_Item_Information_ItemDescription").gameObject;
        m_gPanel_Store_Item_Information_ItemDescription_Content = m_gPanel_Store_Item_Information_ItemDescription.transform.Find("Panel_Store_Item_Information_ItemDescription_Content").gameObject;
        m_gSV_Store_Item_Information_ItemDescription_Content = m_gPanel_Store_Item_Information_ItemDescription_Content.transform.Find("SV_Store_Item_Information_ItemDescription_Content").gameObject;
        m_gViewport_Store_Item_Equip_Information_ItemDescription_Content = m_gSV_Store_Item_Information_ItemDescription_Content.transform.Find("Viewport_Store_Item_Information_ItemDescription_Content").gameObject;
        m_TMP_Store_Item_Information_ItemDescription_Content = m_gViewport_Store_Item_Equip_Information_ItemDescription_Content.transform.Find("TMP_Store_Item_Information_ItemDescription_Content").gameObject.GetComponent<TextMeshProUGUI>();
        m_Scrollbar_Store_Item_Information_ItemDescription_Content = m_gSV_Store_Item_Information_ItemDescription_Content.transform.Find("Scrollbar_Store_Item_Information_ItemDescription_Content").gameObject.GetComponent<Scrollbar>();

        m_BTN_Store_Item_Information_ChangeInformation_R = m_gPanel_Store_Item_Information_Content.transform.Find("BTN_Store_Item_Information_ChangeInformation_R").gameObject.GetComponent<Button>();
        m_BTN_Store_Item_Information_ChangeInformation_L = m_gPanel_Store_Item_Information_Content.transform.Find("BTN_Store_Item_Information_ChangeInformation_L").gameObject.GetComponent<Button>();

        m_gPanel_Store_Information_CanUse = m_gPanel_Store_Item_Information_Content.transform.Find("Panel_Store_Item_Information_CanUse").gameObject;
        m_TMP_Store_Information_CanUse = m_gPanel_Store_Information_CanUse.transform.Find("TMP_Store_Item_Information_CanUse").gameObject.GetComponent<TextMeshProUGUI>();



        m_gPanel_NoItemInfo = m_gPanel_Store_Item_Information.transform.Find("Panel_NoItem").gameObject;
        m_TMP_NoItemInfo = m_gPanel_NoItemInfo.transform.Find("TMP_NoItem").gameObject.GetComponent<TextMeshProUGUI>();



        m_gPanel_SetItemEffect = m_gPanel_Store_Item_Information.transform.Find("Panel_SetItemEffect").gameObject;
        m_gPanel_SetItemEffect_Name = m_gPanel_SetItemEffect.transform.Find("Panel_SetItemEffect_Name").gameObject;
        m_TMP_SetItemEffect_Name = m_gPanel_SetItemEffect_Name.transform.Find("TMP_SetItemEffect_Name").gameObject.GetComponent<TextMeshProUGUI>();

        m_gPanel_SetItemEffect_Content = m_gPanel_SetItemEffect.transform.Find("Panel_SetItemEffect_Content").gameObject;

        m_gPanel_SetItemEffect_Content_UpBar = m_gPanel_SetItemEffect_Content.transform.Find("Panel_SetItemEffect_Content_UpBar").gameObject;
        m_TMP_SetItemEffect_Content_UpBar_Name = m_gPanel_SetItemEffect_Content_UpBar.transform.Find("TMP_SetItemEffect_Content_UpBar_Name").gameObject.GetComponent<TextMeshProUGUI>();
        m_gBTN_SetItemEffect_Content_UpBar_Left = m_gPanel_SetItemEffect_Content_UpBar.transform.Find("BTN_SetItemEffect_Content_UpBar_Left").gameObject;
        m_BTN_SetItemEffect_Content_UpBar_Left = m_gBTN_SetItemEffect_Content_UpBar_Left.GetComponent<Button>();
        m_gBTN_SetItemEffect_Content_UpBar_Right = m_gPanel_SetItemEffect_Content_UpBar.transform.Find("BTN_SetItemEffect_Content_UpBar_Right").gameObject;
        m_BTN_SetItemEffect_Content_UpBar_Right = m_gBTN_SetItemEffect_Content_UpBar_Right.GetComponent<Button>();

        m_gPanel_SetItemEffect_Content_SS = m_gPanel_SetItemEffect_Content.transform.Find("Panel_SetItemEffect_Content_SS").gameObject;
        m_gPanel_SetItemEffect_Content_SS_Description = m_gPanel_SetItemEffect_Content_SS.transform.Find("Panel_SetItemEffect_Content_SS_Description").gameObject;
        m_gSV_SetItemEffect_Content_SS_Description = m_gPanel_SetItemEffect_Content_SS_Description.transform.Find("SV_SetItemEffect_Content_SS_Description").gameObject;
        m_gViewport_SetItemEffect_Content_SS_Description = m_gSV_SetItemEffect_Content_SS_Description.transform.Find("Viewport_SetItemEffect_Content_SS_Description").gameObject;
        m_TMP_SetItemEffect_Content_SS_Description = m_gViewport_SetItemEffect_Content_SS_Description.transform.Find("TMP_SetItemEffect_Content_SS_Description").gameObject.GetComponent<TextMeshProUGUI>();
        m_Scrollbar_SetItemEffect_Content_SS_Description = m_gSV_SetItemEffect_Content_SS_Description.transform.Find("Scrollbar_SetItemEffect_Content_SS_Description").gameObject.GetComponent<Scrollbar>();

        m_gPanel_SetItemEffect_Content_SS_Status = m_gPanel_SetItemEffect_Content_SS.transform.Find("Panel_SetItemEffect_Content_SS_Status").gameObject;
        m_TMP_SetItemEffect_Content_SS_Status_L = m_gPanel_SetItemEffect_Content_SS_Status.transform.Find("TMP_SetItemEffect_Content_SS_Status_L").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_SetItemEffect_Content_SS_Status_R = m_gPanel_SetItemEffect_Content_SS_Status.transform.Find("TMP_SetItemEffect_Content_SS_Status_R").gameObject.GetComponent<TextMeshProUGUI>();
        m_gPanel_SetItemEffect_Content_SS_Soc = m_gPanel_SetItemEffect_Content_SS.transform.Find("Panel_SetItemEffect_Content_SS_Soc").gameObject;
        m_TMP_SetItemEffect_Content_SS_Soc_L = m_gPanel_SetItemEffect_Content_SS_Soc.transform.Find("TMP_SetItemEffect_Content_SS_Soc_L").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_SetItemEffect_Content_SS_Soc_R = m_gPanel_SetItemEffect_Content_SS_Soc.transform.Find("TMP_SetItemEffect_Content_SS_Soc_R").gameObject.GetComponent<TextMeshProUGUI>();



        m_nList_SetItemEffect_Code = new List<int>();



        m_gPanel_Gift_Info = m_gPanel_Store_Item_Information.transform.Find("Panel_Gift_Info").gameObject;

        m_gPanel_Gift_Info_Content = m_gPanel_Gift_Info.transform.Find("Panel_Gift_Info_Content").gameObject;

        m_gPanel_Gift_Info_Content_Type = m_gPanel_Gift_Info_Content.transform.Find("Panel_Gift_Info_Content_Type").gameObject;
        m_TMP_Gift_Info_Content_Type_Name = m_gPanel_Gift_Info_Content_Type.transform.Find("TMP_Gift_Info_Content_Type_Name").gameObject.GetComponent<TextMeshProUGUI>();

        m_gPanel_Gift_Info_Content_Description = m_gPanel_Gift_Info_Content.transform.Find("Panel_Gift_Info_Content_Description").gameObject;

        m_gPanel_Gift_Info_Content_Description_Up = m_gPanel_Gift_Info_Content_Description.transform.Find("Panel_Gift_Info_Content_Description_Up").gameObject;
        m_TMP_Gift_Info_Content_Description_Up = m_gPanel_Gift_Info_Content_Description_Up.transform.Find("TMP_Gift_Info_Content_Description_Up").gameObject.GetComponent<TextMeshProUGUI>();

        m_gPanel_Gift_Info_Content_Description_Down = m_gPanel_Gift_Info_Content_Description.transform.Find("Panel_Gift_Info_Content_Description_Down").gameObject;

        m_gPanel_Gift_Info_Content_Description_X = m_gPanel_Gift_Info_Content_Description_Down.transform.Find("Panel_Gift_Info_Content_Description_X").gameObject;

        m_gSV_Gift_Info_Content_Description_Down = m_gPanel_Gift_Info_Content_Description_Down.transform.Find("SV_Gift_Info_Content_Description_Down").gameObject;
        m_gViewport_Gift_Info_Content_Description_Down = m_gSV_Gift_Info_Content_Description_Down.transform.Find("Viewport_Gift_Info_Content_Description_Down").gameObject;
        m_gContent_Gift_Info_Content_Description_Down = m_gViewport_Gift_Info_Content_Description_Down.transform.Find("Content_Gift_Info_Content_Description_Down").gameObject;
        m_Scrollbar_Gift_Info_Content_Description_Down = m_gSV_Gift_Info_Content_Description_Down.transform.Find("Scrollbar_Gift_Info_Content_Description_Down").gameObject.GetComponent<Scrollbar>();



        m_gPanel_ItemInfo = Resources.Load("Prefab/GUI/Panel_ItemInfo") as GameObject;
        m_gList_Panel_ItemInfo = new List<GameObject>();
    }

    void InitialSet_Button()

    {

    }
    void Press_Btn_R(E_ITEM_USE_TYPE eiut)
    {
        if (eiut == E_ITEM_USE_TYPE.GIFT)
        {
            switch (m_e_Store_Item_Information)
            {
                case E_STORE_ITEM_INFORMATION.CONDITION:
                    {
                        m_e_Store_Item_Information = E_STORE_ITEM_INFORMATION.DESCRIPTION;
                        m_gPanel_Store_Item_Information_Effect.SetActive(false);
                        m_gPanel_Store_Item_Information_Condition.SetActive(false);
                        m_gPanel_Store_Item_Information_ItemDescription.SetActive(true);
                        m_Scrollbar_Store_Item_Information_ItemDescription_Content.value = 1;
                        m_Scrollbar_SetItemEffect_Content_SS_Description.value = 1;
                    }
                    break;
                case E_STORE_ITEM_INFORMATION.DESCRIPTION:
                    {
                        m_e_Store_Item_Information = E_STORE_ITEM_INFORMATION.CONDITION;
                        m_gPanel_Store_Item_Information_Effect.SetActive(false);
                        m_gPanel_Store_Item_Information_Condition.SetActive(true);
                        m_gPanel_Store_Item_Information_ItemDescription.SetActive(false);
                    }
                    break;
            }
        }
        else
        {
            switch (m_e_Store_Item_Information)
            {
                case E_STORE_ITEM_INFORMATION.EFFECT:
                    {
                        m_e_Store_Item_Information = E_STORE_ITEM_INFORMATION.CONDITION;
                        m_gPanel_Store_Item_Information_Effect.SetActive(false);
                        m_gPanel_Store_Item_Information_Condition.SetActive(true);
                        m_gPanel_Store_Item_Information_ItemDescription.SetActive(false);
                    }
                    break;
                case E_STORE_ITEM_INFORMATION.CONDITION:
                    {
                        m_e_Store_Item_Information = E_STORE_ITEM_INFORMATION.DESCRIPTION;
                        m_gPanel_Store_Item_Information_Effect.SetActive(false);
                        m_gPanel_Store_Item_Information_Condition.SetActive(false);
                        m_gPanel_Store_Item_Information_ItemDescription.SetActive(true);
                        m_Scrollbar_Store_Item_Information_ItemDescription_Content.value = 1;
                        m_Scrollbar_SetItemEffect_Content_SS_Description.value = 1;
                    }
                    break;
                case E_STORE_ITEM_INFORMATION.DESCRIPTION:
                    {
                        m_e_Store_Item_Information = E_STORE_ITEM_INFORMATION.EFFECT;
                        m_gPanel_Store_Item_Information_Effect.SetActive(true);
                        m_gPanel_Store_Item_Information_Condition.SetActive(false);
                        m_gPanel_Store_Item_Information_ItemDescription.SetActive(false);
                    }
                    break;
            }
        }
    }
    void Press_Btn_L(E_ITEM_USE_TYPE eiut)
    {
        if (eiut == E_ITEM_USE_TYPE.GIFT)
        {
            switch (m_e_Store_Item_Information)
            {
                case E_STORE_ITEM_INFORMATION.CONDITION:
                    {
                        m_e_Store_Item_Information = E_STORE_ITEM_INFORMATION.DESCRIPTION;
                        m_gPanel_Store_Item_Information_Effect.SetActive(false);
                        m_gPanel_Store_Item_Information_Condition.SetActive(false);
                        m_gPanel_Store_Item_Information_ItemDescription.SetActive(true);
                        m_Scrollbar_Store_Item_Information_ItemDescription_Content.value = 1;
                        m_Scrollbar_SetItemEffect_Content_SS_Description.value = 1;
                    }
                    break;
                case E_STORE_ITEM_INFORMATION.DESCRIPTION:
                    {
                        m_e_Store_Item_Information = E_STORE_ITEM_INFORMATION.CONDITION;
                        m_gPanel_Store_Item_Information_Effect.SetActive(false);
                        m_gPanel_Store_Item_Information_Condition.SetActive(true);
                        m_gPanel_Store_Item_Information_ItemDescription.SetActive(false);
                    }
                    break;
            }
        }
        else
        {
            switch (m_e_Store_Item_Information)
            {
                case E_STORE_ITEM_INFORMATION.EFFECT:
                    {
                        m_e_Store_Item_Information = E_STORE_ITEM_INFORMATION.DESCRIPTION;
                        m_gPanel_Store_Item_Information_Effect.SetActive(false);
                        m_gPanel_Store_Item_Information_Condition.SetActive(false);
                        m_gPanel_Store_Item_Information_ItemDescription.SetActive(true);
                        m_Scrollbar_Store_Item_Information_ItemDescription_Content.value = 1;
                        m_Scrollbar_SetItemEffect_Content_SS_Description.value = 1;
                    }
                    break;
                case E_STORE_ITEM_INFORMATION.CONDITION:
                    {
                        m_e_Store_Item_Information = E_STORE_ITEM_INFORMATION.EFFECT;
                        m_gPanel_Store_Item_Information_Effect.SetActive(true);
                        m_gPanel_Store_Item_Information_Condition.SetActive(false);
                        m_gPanel_Store_Item_Information_ItemDescription.SetActive(false);
                    }
                    break;
                case E_STORE_ITEM_INFORMATION.DESCRIPTION:
                    {
                        m_e_Store_Item_Information = E_STORE_ITEM_INFORMATION.CONDITION;
                        m_gPanel_Store_Item_Information_Effect.SetActive(false);
                        m_gPanel_Store_Item_Information_Condition.SetActive(true);
                        m_gPanel_Store_Item_Information_ItemDescription.SetActive(false);
                    }
                    break;
            }
        }
    }
    void Press_Btn_R()
    {
            switch (m_e_Store_Item_Information)
            {
                case E_STORE_ITEM_INFORMATION.EFFECT:
                    {
                        m_e_Store_Item_Information = E_STORE_ITEM_INFORMATION.CONDITION;
                        m_gPanel_Store_Item_Information_Effect.SetActive(false);
                        m_gPanel_Store_Item_Information_Condition.SetActive(true);
                        m_gPanel_Store_Item_Information_ItemDescription.SetActive(false);
                    }
                    break;
                case E_STORE_ITEM_INFORMATION.CONDITION:
                    {
                        m_e_Store_Item_Information = E_STORE_ITEM_INFORMATION.DESCRIPTION;
                        m_gPanel_Store_Item_Information_Effect.SetActive(false);
                        m_gPanel_Store_Item_Information_Condition.SetActive(false);
                        m_gPanel_Store_Item_Information_ItemDescription.SetActive(true);
                        m_Scrollbar_Store_Item_Information_ItemDescription_Content.value = 1;
                        m_Scrollbar_SetItemEffect_Content_SS_Description.value = 1;
                    }
                    break;
                case E_STORE_ITEM_INFORMATION.DESCRIPTION:
                    {
                        m_e_Store_Item_Information = E_STORE_ITEM_INFORMATION.EFFECT;
                        m_gPanel_Store_Item_Information_Effect.SetActive(true);
                        m_gPanel_Store_Item_Information_Condition.SetActive(false);
                        m_gPanel_Store_Item_Information_ItemDescription.SetActive(false);
                    }
                    break;
            }
    }
    void Press_Btn_L()
    {
        switch (m_e_Store_Item_Information)
        {
            case E_STORE_ITEM_INFORMATION.EFFECT:
                {
                    m_e_Store_Item_Information = E_STORE_ITEM_INFORMATION.DESCRIPTION;
                    m_gPanel_Store_Item_Information_Effect.SetActive(false);
                    m_gPanel_Store_Item_Information_Condition.SetActive(false);
                    m_gPanel_Store_Item_Information_ItemDescription.SetActive(true);
                    m_Scrollbar_Store_Item_Information_ItemDescription_Content.value = 1;
                    m_Scrollbar_SetItemEffect_Content_SS_Description.value = 1;
                }
                break;
            case E_STORE_ITEM_INFORMATION.CONDITION:
                {
                    m_e_Store_Item_Information = E_STORE_ITEM_INFORMATION.EFFECT;
                    m_gPanel_Store_Item_Information_Effect.SetActive(true);
                    m_gPanel_Store_Item_Information_Condition.SetActive(false);
                    m_gPanel_Store_Item_Information_ItemDescription.SetActive(false);
                }
                break;
            case E_STORE_ITEM_INFORMATION.DESCRIPTION:
                {
                    m_e_Store_Item_Information = E_STORE_ITEM_INFORMATION.CONDITION;
                    m_gPanel_Store_Item_Information_Effect.SetActive(false);
                    m_gPanel_Store_Item_Information_Condition.SetActive(true);
                    m_gPanel_Store_Item_Information_ItemDescription.SetActive(false);
                }
                break;
        }
    }
    // 세트 아이템 효과 버튼 설정.
    void Set_BTN_SetItemEffect_UpBar_Left()
    {
        m_nSetItemEffect_List_Index -= 1;
        m_nSetItemEffect_Current = m_nList_SetItemEffect_Code[m_nSetItemEffect_List_Index];

        UpdateItemEquipInformation_SetItemEffect_Content();
        UpdateItemEquipInformation_SetItemEffect_UpBar();
    }
    void Set_BTN_SetItemEffect_UpBar_Right()
    {
        m_nSetItemEffect_List_Index += 1;
        m_nSetItemEffect_Current = m_nList_SetItemEffect_Code[m_nSetItemEffect_List_Index];

        UpdateItemEquipInformation_SetItemEffect_Content();
        UpdateItemEquipInformation_SetItemEffect_UpBar();
    }

    public void Display_GUI_Store_Item_Information(Item_Equip item)
    {
        m_TMP_NoItemInfo.text = "선택된 아이템이 없습니다.";

        m_Scrollbar_Store_Item_Information_ItemDescription_Content.value = 1;
        m_Scrollbar_SetItemEffect_Content_SS_Description.value = 1;
        m_gPanel_Store_Item_Information.SetActive(true);

        player = Player_Total.Instance.m_ps_Status;

        UpdateStoreItemInformation(item);
        UpdateItemUseInformation_Button();

        m_BTN_Store_Item_Information_ChangeInformation_R.gameObject.SetActive(true);
        m_BTN_Store_Item_Information_ChangeInformation_L.gameObject.SetActive(true);

        m_gPanel_Store_Item_Information_Content.transform.SetAsLastSibling();
        m_gPanel_Store_Item_Information_Content.SetActive(true);

        m_gPanel_Gift_Info.SetActive(false);
    }
    public void Display_GUI_Store_Item_Information(Item_Use item)
    {
        m_TMP_NoItemInfo.text = "선택된 아이템이 없습니다.";

        m_Scrollbar_Store_Item_Information_ItemDescription_Content.value = 1;

        m_gPanel_Store_Item_Information.SetActive(true);

        player = Player_Total.Instance.m_ps_Status;

        UpdateStoreItemInformation(item);
        UpdateItemUseInformation_Button(item.m_eItemUseType);

        m_BTN_Store_Item_Information_ChangeInformation_R.gameObject.SetActive(true);
        m_BTN_Store_Item_Information_ChangeInformation_L.gameObject.SetActive(true);

        m_gPanel_Store_Item_Information_Content.transform.SetAsLastSibling();
        m_gPanel_Store_Item_Information_Content.SetActive(true);

        m_gPanel_SetItemEffect.SetActive(false);
    }
    public void Display_GUI_Store_Item_Information(Item_Etc item)
    {
        m_TMP_NoItemInfo.text = "선택된 아이템이 없습니다.";

        m_Scrollbar_Store_Item_Information_ItemDescription_Content.value = 1;

        m_gPanel_Store_Item_Information.SetActive(true);

        player = Player_Total.Instance.m_ps_Status;

        UpdateStoreItemInformation(item);
        UpdateItemUseInformation_Button();

        m_BTN_Store_Item_Information_ChangeInformation_R.gameObject.SetActive(false);
        m_BTN_Store_Item_Information_ChangeInformation_L.gameObject.SetActive(false);

        m_gPanel_Store_Item_Information_Content.transform.SetAsLastSibling();
        m_gPanel_Store_Item_Information_Content.SetActive(true);

        m_gPanel_SetItemEffect.SetActive(false);
        m_gPanel_Gift_Info.SetActive(false);
    }

    public void UpdateStoreItemInformation(Item_Equip item)
    {
        m_bEU_Condition_Check = true;

        m_TMP_Store_Item_Information_Effect_Name.text = "착용효과";
        m_TMP_Store_Item_Information_Condition_Name.text = "착용조건";

        //m_e_Store_Item_Information = E_STORE_ITEM_INFORMATION.DESCRIPTION;
        //m_gPanel_Store_Item_Information_Condition.SetActive(false);
        //m_gPanel_Store_Item_Information_Effect.SetActive(false);
        //m_gPanel_Store_Item_Information_ItemDescription.SetActive(true);

        m_TMP_Store_Item_Information_UpBar_Name.text = item.m_sItemName;
        if (item.m_nReinforcementCount_Current > 0)
            m_TMP_Store_Item_Information_UpBar_Name.text += " + " + item.m_nReinforcementCount_Current;

        // 장비 아이템 이미지.
        m_IMG_Store_Item_Information_Image_ItemSprite.sprite = item.m_sp_Sprite;
        m_IMG_Store_Item_Information_Image_ItemSprite.color = new Color(1, 1, 1, 0.75f);

        // 장비 아이템 분류, 등급, 추가 옵션 등급, 강화 상태.
        // 분류.
        m_TMP_Store_Item_Information_ItemInformation.text = "";
        if (item.m_eItemEquipType == E_ITEM_EQUIP_TYPE.MAINWEAPON)
        {
            switch (item.m_eItemEquipMainWeaponType)
            {
                case E_ITEM_EQUIP_MAINWEAPON_TYPE.SWORD:
                    {
                        m_sBuffer = "검";
                    }
                    break;
                case E_ITEM_EQUIP_MAINWEAPON_TYPE.KNIFE:
                    {
                        m_sBuffer = "단검";
                    }
                    break;
                case E_ITEM_EQUIP_MAINWEAPON_TYPE.AXE:
                    {
                        m_sBuffer = "도끼";
                    }
                    break;
            }
        }
        else
        {
            switch (item.m_eItemEquipType)
            {
                case E_ITEM_EQUIP_TYPE.HAT:
                    {
                        m_sBuffer = "모자";
                    }
                    break;
                case E_ITEM_EQUIP_TYPE.TOP:
                    {
                        m_sBuffer = "상의";
                    }
                    break;
                case E_ITEM_EQUIP_TYPE.BOTTOMS:
                    {
                        m_sBuffer = "하의";
                    }
                    break;
                case E_ITEM_EQUIP_TYPE.SHOSE:
                    {
                        m_sBuffer = "신발";
                    }
                    break;
                case E_ITEM_EQUIP_TYPE.GLOVES:
                    {
                        m_sBuffer = "장갑";
                    }
                    break;
                case E_ITEM_EQUIP_TYPE.MAINWEAPON:
                    {
                        m_sBuffer = "무기";
                    }
                    break;
                case E_ITEM_EQUIP_TYPE.SUBWEAPON:
                    {
                        m_sBuffer = "보조무기";
                    }
                    break;
            }
        }
        m_TMP_Store_Item_Information_ItemInformation.text += "분류: " + m_sBuffer + "\n";
        // 등급.
        switch (item.m_eItemGrade)
        {
            case E_ITEM_GRADE.NORMAL:
                {
                    m_sBuffer = "노멀";
                }
                break;
            case E_ITEM_GRADE.COMMON:
                {
                    m_sBuffer = "일반";
                }
                break;
            case E_ITEM_GRADE.RARE:
                {
                    m_sBuffer = "레어";
                }
                break;
            case E_ITEM_GRADE.RELIC:
                {
                    m_sBuffer = "유물";
                }
                break;
            case E_ITEM_GRADE.LEGEND:
                {
                    m_sBuffer = "전설";
                }
                break;
            case E_ITEM_GRADE.MYTH:
                {
                    m_sBuffer = "신화";
                }
                break;
        }
        m_TMP_Store_Item_Information_ItemInformation.text += "등급: " + m_sBuffer + "\n";
        // 추가 옵션 등급.
        m_TMP_Store_Item_Information_ItemInformation.text += "추가 옵션 등급: " + item.m_eItemEquip_SpecialRatio_STATUS + " / " + item.m_eItemEquip_SpecialRatio_SOC + "\n";
        // 강화 상태.
        m_TMP_Store_Item_Information_ItemInformation.text += "강화 상태: " + item.m_nReinforcementCount_Current + " / " + item.m_nReinforcementCount_Max;

        // 장비 아이템 효과.
        // 장비 아이템 효과 Status.
        m_TMP_Store_Item_Information_Effect_Status_L.text = "";
        m_TMP_Store_Item_Information_Effect_Status_L.text += Refine_Condition("레        벨:", item.m_sStatus_Effect.GetSTATUS_LV(), item.m_STATUS_AdditionalOption.GetSTATUS_LV(), item.m_STATUS_ReinforcementValue.GetSTATUS_LV());
        m_TMP_Store_Item_Information_Effect_Status_L.text += Refine_Condition("\n체        력:", item.m_sStatus_Effect.GetSTATUS_HP_Max(), item.m_STATUS_AdditionalOption.GetSTATUS_HP_Max(), item.m_STATUS_ReinforcementValue.GetSTATUS_HP_Max());
        m_TMP_Store_Item_Information_Effect_Status_L.text += Refine_Condition("\n데  미  지:", item.m_sStatus_Effect.GetSTATUS_Damage_Total(), item.m_STATUS_AdditionalOption.GetSTATUS_Damage_Total(), item.m_STATUS_ReinforcementValue.GetSTATUS_Damage_Total());
        m_TMP_Store_Item_Information_Effect_Status_L.text += Refine_Condition("\n이동속도:", item.m_sStatus_Effect.GetSTATUS_Speed(), item.m_STATUS_AdditionalOption.GetSTATUS_Speed(), item.m_STATUS_ReinforcementValue.GetSTATUS_Speed());
        m_TMP_Store_Item_Information_Effect_Status_R.text = "";
        m_TMP_Store_Item_Information_Effect_Status_R.text += "\n";
        m_TMP_Store_Item_Information_Effect_Status_R.text += Refine_Condition("마        나:", item.m_sStatus_Effect.GetSTATUS_MP_Max(), item.m_STATUS_AdditionalOption.GetSTATUS_MP_Max(), item.m_STATUS_ReinforcementValue.GetSTATUS_MP_Max());
        m_TMP_Store_Item_Information_Effect_Status_R.text += Refine_Condition("\n방  어  력:", item.m_sStatus_Effect.GetSTATUS_Defence_Physical(), item.m_STATUS_AdditionalOption.GetSTATUS_Defence_Physical(), item.m_STATUS_ReinforcementValue.GetSTATUS_Defence_Physical());
        m_TMP_Store_Item_Information_Effect_Status_R.text += Refine_Condition("\n공격속도:", item.m_sStatus_Effect.GetSTATUS_AttackSpeed(), item.m_STATUS_AdditionalOption.GetSTATUS_AttackSpeed(), item.m_STATUS_ReinforcementValue.GetSTATUS_AttackSpeed());
        // 장비 아이템 효과 Soc.
        m_TMP_Store_Item_Information_Effect_Soc_L.text = "";
        m_TMP_Store_Item_Information_Effect_Soc_L.text += Refine_Condition("명        예:", item.m_sSoc_Effect.GetSOC_Honor(), item.m_SOC_AdditionalOption.GetSOC_Honor(), item.m_SOC_ReinforcementValue.GetSOC_Honor());
        m_TMP_Store_Item_Information_Effect_Soc_L.text += Refine_Condition("\n인        간:", item.m_sSoc_Effect.GetSOC_Human(), item.m_SOC_AdditionalOption.GetSOC_Human(), item.m_SOC_ReinforcementValue.GetSOC_Human());
        m_TMP_Store_Item_Information_Effect_Soc_L.text += Refine_Condition("\n동        물:", item.m_sSoc_Effect.GetSOC_Animal(), item.m_SOC_AdditionalOption.GetSOC_Animal(), item.m_SOC_ReinforcementValue.GetSOC_Animal());
        m_TMP_Store_Item_Information_Effect_Soc_L.text += Refine_Condition("\n슬  라  임:", item.m_sSoc_Effect.GetSOC_Slime(), item.m_SOC_AdditionalOption.GetSOC_Slime(), item.m_SOC_ReinforcementValue.GetSOC_Slime());
        m_TMP_Store_Item_Information_Effect_Soc_L.text += Refine_Condition("\n스켈레톤:", item.m_sSoc_Effect.GetSOC_Skeleton(), item.m_SOC_AdditionalOption.GetSOC_Skeleton(), item.m_SOC_ReinforcementValue.GetSOC_Skeleton());
        m_TMP_Store_Item_Information_Effect_Soc_R.text = "";
        m_TMP_Store_Item_Information_Effect_Soc_R.text += "\n";
        m_TMP_Store_Item_Information_Effect_Soc_R.text += Refine_Condition("앤        트:", item.m_sSoc_Effect.GetSOC_Ents(), item.m_SOC_AdditionalOption.GetSOC_Ents(), item.m_SOC_ReinforcementValue.GetSOC_Ents());
        m_TMP_Store_Item_Information_Effect_Soc_R.text += Refine_Condition("\n마        족:", item.m_sSoc_Effect.GetSOC_Devil(), item.m_SOC_AdditionalOption.GetSOC_Devil(), item.m_SOC_ReinforcementValue.GetSOC_Devil());
        m_TMP_Store_Item_Information_Effect_Soc_R.text += Refine_Condition("\n용        족:", item.m_sSoc_Effect.GetSOC_Dragon(), item.m_SOC_AdditionalOption.GetSOC_Dragon(), item.m_SOC_ReinforcementValue.GetSOC_Dragon());
        m_TMP_Store_Item_Information_Effect_Soc_R.text += Refine_Condition("\n어        둠:", item.m_sSoc_Effect.GetSOC_Shadow(), item.m_SOC_AdditionalOption.GetSOC_Shadow(), item.m_SOC_ReinforcementValue.GetSOC_Shadow());

        // 장비 아이템 착용 조건.
        // 장비 아이템 착용 조건 Status.
        m_TMP_Store_Item_Information_Condition_Status_L.text = "";
        //m_TMP_Reinforcement_Equip_Information_Content_Condition_Status_L.text += "레        벨: " + Refine_Condition(item.m_sStatus_Limit_Min.GetSTATUS_LV(), item.m_sStatus_Limit_Max.GetSTATUS_LV()) + "\n";
        m_TMP_Store_Item_Information_Condition_Status_L.text += Refine_Condition("레        벨: ", "\n", item.m_sStatus_Limit_Min.GetSTATUS_LV(), item.m_sStatus_Limit_Max.GetSTATUS_LV(), player.m_sStatus.GetSTATUS_LV());
        //m_TMP_Reinforcement_Equip_Information_Content_Condition_Status_L.text += "체        력: " + Refine_Condition(item.m_sStatus_Limit_Min.GetSTATUS_HP_Max(), item.m_sStatus_Limit_Max.GetSTATUS_HP_Max()) + "\n";
        m_TMP_Store_Item_Information_Condition_Status_L.text += Refine_Condition("체        력: ", "\n", item.m_sStatus_Limit_Min.GetSTATUS_HP_Max(), item.m_sStatus_Limit_Max.GetSTATUS_HP_Max(), player.m_sStatus.GetSTATUS_HP_Max());
        //m_TMP_Reinforcement_Equip_Information_Content_Condition_Status_L.text += "데  미  지: " + Refine_Condition(item.m_sStatus_Limit_Min.GetSTATUS_Damage_Total(), item.m_sStatus_Limit_Max.GetSTATUS_Damage_Total()) + "\n";
        m_TMP_Store_Item_Information_Condition_Status_L.text += Refine_Condition("데  미  지: ", "\n", item.m_sStatus_Limit_Min.GetSTATUS_Damage_Total(), item.m_sStatus_Limit_Max.GetSTATUS_Damage_Total(), player.m_sStatus.GetSTATUS_Damage_Total());
        //m_TMP_Reinforcement_Equip_Information_Content_Condition_Status_L.text += "이동속도: " + Refine_Condition(item.m_sStatus_Limit_Min.GetSTATUS_Speed(), item.m_sStatus_Limit_Max.GetSTATUS_Speed());
        m_TMP_Store_Item_Information_Condition_Status_L.text += Refine_Condition("이동속도: ", "", item.m_sStatus_Limit_Min.GetSTATUS_Speed(), item.m_sStatus_Limit_Max.GetSTATUS_Speed(), player.m_sStatus.GetSTATUS_Speed());
        m_TMP_Store_Item_Information_Condition_Status_R.text = "";
        m_TMP_Store_Item_Information_Condition_Status_R.text += "\n";
        //m_TMP_Reinforcement_Equip_Information_Content_Condition_Status_R.text += "마        나: " + Refine_Condition(item.m_sStatus_Limit_Min.GetSTATUS_MP_Max(), item.m_sStatus_Limit_Max.GetSTATUS_MP_Max()) + "\n";
        m_TMP_Store_Item_Information_Condition_Status_R.text += Refine_Condition("마        나: ", "\n", item.m_sStatus_Limit_Min.GetSTATUS_MP_Max(), item.m_sStatus_Limit_Max.GetSTATUS_MP_Max(), player.m_sStatus.GetSTATUS_MP_Max());
        //m_TMP_Reinforcement_Equip_Information_Content_Condition_Status_R.text += "방  어  력: " + Refine_Condition(item.m_sStatus_Limit_Min.GetSTATUS_Defence_Physical(), item.m_sStatus_Limit_Max.GetSTATUS_Defence_Physical()) + "\n";
        m_TMP_Store_Item_Information_Condition_Status_R.text += Refine_Condition("방  어  력: ", "\n", item.m_sStatus_Limit_Min.GetSTATUS_Defence_Physical(), item.m_sStatus_Limit_Max.GetSTATUS_Defence_Physical(), player.m_sStatus.GetSTATUS_Defence_Physical());
        //m_TMP_Reinforcement_Equip_Information_Content_Condition_Status_R.text += "공격속도: " + Refine_Condition(item.m_sStatus_Limit_Min.GetSTATUS_AttackSpeed(), item.m_sStatus_Limit_Max.GetSTATUS_AttackSpeed());
        m_TMP_Store_Item_Information_Condition_Status_R.text += Refine_Condition("공격속도: ", "", item.m_sStatus_Limit_Min.GetSTATUS_AttackSpeed(), item.m_sStatus_Limit_Max.GetSTATUS_AttackSpeed(), player.m_sStatus.GetSTATUS_AttackSpeed());

        // 장비 아이템 조건 Soc.
        m_TMP_Store_Item_Information_Condition_Soc_L.text = "";
        //m_TMP_Reinforcement_Equip_Information_Content_Condition_Soc_L.text += "평        판: " + Refine_Condition(item.m_sSoc_Limit_Min.GetSOC_Honor(), item.m_sSoc_Limit_Max.GetSOC_Honor()) + "\n";
        m_TMP_Store_Item_Information_Condition_Soc_L.text += Refine_Condition("명        예: ", "\n", item.m_sSoc_Limit_Min.GetSOC_Honor(), item.m_sSoc_Limit_Max.GetSOC_Honor(), player.m_sSoc.GetSOC_Honor());
        //m_TMP_Reinforcement_Equip_Information_Content_Condition_Soc_L.text += "인        간: " + Refine_Condition(item.m_sSoc_Limit_Min.GetSOC_Human(), item.m_sSoc_Limit_Max.GetSOC_Human()) + "\n";
        m_TMP_Store_Item_Information_Condition_Soc_L.text += Refine_Condition("인        간: ", "\n", item.m_sSoc_Limit_Min.GetSOC_Human(), item.m_sSoc_Limit_Max.GetSOC_Human(), player.m_sSoc.GetSOC_Human());
        //m_TMP_Reinforcement_Equip_Information_Content_Condition_Soc_L.text += "동        물: " + Refine_Condition(item.m_sSoc_Limit_Min.GetSOC_Animal(), item.m_sSoc_Limit_Max.GetSOC_Animal()) + "\n";
        m_TMP_Store_Item_Information_Condition_Soc_L.text += Refine_Condition("동        물: ", "\n", item.m_sSoc_Limit_Min.GetSOC_Animal(), item.m_sSoc_Limit_Max.GetSOC_Animal(), player.m_sSoc.GetSOC_Animal());
        //m_TMP_Reinforcement_Equip_Information_Content_Condition_Soc_L.text += "슬  라  임: " + Refine_Condition(item.m_sSoc_Limit_Min.GetSOC_Slime(), item.m_sSoc_Limit_Max.GetSOC_Slime()) + "\n";
        m_TMP_Store_Item_Information_Condition_Soc_L.text += Refine_Condition("슬  라  임: ", "\n", item.m_sSoc_Limit_Min.GetSOC_Slime(), item.m_sSoc_Limit_Max.GetSOC_Slime(), player.m_sSoc.GetSOC_Slime());
        //m_TMP_Reinforcement_Equip_Information_Content_Condition_Soc_L.text += "스켈레톤: " + Refine_Condition(item.m_sSoc_Limit_Min.GetSOC_Skeleton(), item.m_sSoc_Limit_Max.GetSOC_Skeleton());
        m_TMP_Store_Item_Information_Condition_Soc_L.text += Refine_Condition("스켈레톤: ", "", item.m_sSoc_Limit_Min.GetSOC_Skeleton(), item.m_sSoc_Limit_Max.GetSOC_Skeleton(), player.m_sSoc.GetSOC_Skeleton());
        m_TMP_Store_Item_Information_Condition_Soc_R.text = "";
        m_TMP_Store_Item_Information_Condition_Soc_R.text += "\n";
        //m_TMP_Reinforcement_Equip_Information_Content_Condition_Soc_R.text += "앤        트: " + Refine_Condition(item.m_sSoc_Limit_Min.GetSOC_Ents(), item.m_sSoc_Limit_Max.GetSOC_Ents()) + "\n";
        m_TMP_Store_Item_Information_Condition_Soc_R.text += Refine_Condition("앤        트: ", "\n", item.m_sSoc_Limit_Min.GetSOC_Ents(), item.m_sSoc_Limit_Max.GetSOC_Ents(), player.m_sSoc.GetSOC_Ents());
        //m_TMP_Reinforcement_Equip_Information_Content_Condition_Soc_R.text += "마        족: " + Refine_Condition(item.m_sSoc_Limit_Min.GetSOC_Devil(), item.m_sSoc_Limit_Max.GetSOC_Devil()) + "\n";
        m_TMP_Store_Item_Information_Condition_Soc_R.text += Refine_Condition("마        족: ", "\n", item.m_sSoc_Limit_Min.GetSOC_Devil(), item.m_sSoc_Limit_Max.GetSOC_Devil(), player.m_sSoc.GetSOC_Devil());
        //m_TMP_Reinforcement_Equip_Information_Content_Condition_Soc_R.text += "용        족: " + Refine_Condition(item.m_sSoc_Limit_Min.GetSOC_Dragon(), item.m_sSoc_Limit_Max.GetSOC_Dragon()) + "\n";
        m_TMP_Store_Item_Information_Condition_Soc_R.text += Refine_Condition("용        족: ", "\n", item.m_sSoc_Limit_Min.GetSOC_Dragon(), item.m_sSoc_Limit_Max.GetSOC_Dragon(), player.m_sSoc.GetSOC_Dragon());
        //m_TMP_Reinforcement_Equip_Information_Content_Condition_Soc_R.text += "어        둠: " + Refine_Condition(item.m_sSoc_Limit_Min.GetSOC_Shadow(), item.m_sSoc_Limit_Max.GetSOC_Shadow());
        m_TMP_Store_Item_Information_Condition_Soc_R.text += Refine_Condition("어        듬: ", "", item.m_sSoc_Limit_Min.GetSOC_Shadow(), item.m_sSoc_Limit_Max.GetSOC_Shadow(), player.m_sSoc.GetSOC_Shadow());

        // 장비 아이템 설명.
        m_TMP_Store_Item_Information_ItemDescription_Content.text = "";
        m_TMP_Store_Item_Information_ItemDescription_Content.text += item.GetItemDescription();

        // 착용, 사용 가능 여부.
        m_gPanel_Store_Information_CanUse.SetActive(true);
        if (m_bEU_Condition_Check == true)
            m_TMP_Store_Information_CanUse.text = m_sColor_White + "착용가능" + m_sColor_End;
        else
            m_TMP_Store_Information_CanUse.text = m_sColor_Red + "착용불가능" + m_sColor_End;

        UpdateItemEquipInformation_SetItemEffect(item);
    }
    public void UpdateStoreItemInformation(Item_Use item)
    {
        m_bEU_Condition_Check = true;

        m_TMP_Store_Item_Information_Effect_Name.text = "사용효과";
        m_TMP_Store_Item_Information_Condition_Name.text = "사용조건";

        //m_e_Store_Item_Information = E_STORE_ITEM_INFORMATION.DESCRIPTION;
        //m_gPanel_Store_Item_Information_Condition.SetActive(false);
        //m_gPanel_Store_Item_Information_Effect.SetActive(false);
        //m_gPanel_Store_Item_Information_ItemDescription.SetActive(true);

        m_TMP_Store_Item_Information_UpBar_Name.text = item.m_sItemName;

        // 소비 아이템 이미지.
        m_IMG_Store_Item_Information_Image_ItemSprite.sprite = item.m_sp_Sprite;
        m_IMG_Store_Item_Information_Image_ItemSprite.color = new Color(1, 1, 1, 0.75f);

        // 분류.
        m_TMP_Store_Item_Information_ItemInformation.text = "";
        switch (item.m_eItemUseType)
        {
            case E_ITEM_USE_TYPE.RECOVERPOTION:
                {
                    m_sBuffer = "회복 포션";
                }
                break;
            case E_ITEM_USE_TYPE.TEMPORARYBUFFPOTION:
                {
                    m_sBuffer = "일시적 버프 비약";
                }
                break;
            case E_ITEM_USE_TYPE.ETERNALBUFFPOTION:
                {
                    m_sBuffer = "영구적 버프 비약";
                }
                break;
            case E_ITEM_USE_TYPE.REINFORCEMENT:
                {
                    m_sBuffer = "강화 주문서";
                }
                break;
            case E_ITEM_USE_TYPE.GIFT:
                {
                    m_sBuffer = "기프트 박스";
                }
                break;
        }
        m_TMP_Store_Item_Information_ItemInformation.text += "분류: " + m_sBuffer + "\n";
        // 등급.
        switch (item.m_eItemGrade)
        {
            case E_ITEM_GRADE.NORMAL:
                {
                    m_sBuffer = "노멀";
                }
                break;
            case E_ITEM_GRADE.COMMON:
                {
                    m_sBuffer = "일반";
                }
                break;
            case E_ITEM_GRADE.RARE:
                {
                    m_sBuffer = "레어";
                }
                break;
            case E_ITEM_GRADE.RELIC:
                {
                    m_sBuffer = "유물";
                }
                break;
            case E_ITEM_GRADE.LEGEND:
                {
                    m_sBuffer = "전설";
                }
                break;
            case E_ITEM_GRADE.MYTH:
                {
                    m_sBuffer = "신화";
                }
                break;
        }
        m_TMP_Store_Item_Information_ItemInformation.text += "등급: " + m_sBuffer + "\n";

        // 소비 아이템 효과 Status.
        switch (item.m_eItemUseType)
        {
            case E_ITEM_USE_TYPE.RECOVERPOTION:
                {
                    m_TMP_Store_Item_Information_Effect_Status_L.text = "";
                    m_TMP_Store_Item_Information_Effect_Status_L.text += Refine_Condition_Item_Use("레        벨:", item.m_sStatus_Effect.GetSTATUS_LV());
                    m_TMP_Store_Item_Information_Effect_Status_L.text += Refine_Condition_Item_Use("\n체        력:", item.m_sStatus_Effect.GetSTATUS_HP_Current());
                    m_TMP_Store_Item_Information_Effect_Status_R.text = "";
                    m_TMP_Store_Item_Information_Effect_Status_R.text += "\n";
                    m_TMP_Store_Item_Information_Effect_Status_R.text += Refine_Condition_Item_Use("마        나:", item.m_sStatus_Effect.GetSTATUS_MP_Current());
                }
                break;
            case E_ITEM_USE_TYPE.TEMPORARYBUFFPOTION:
            case E_ITEM_USE_TYPE.ETERNALBUFFPOTION:
                {
                    m_TMP_Store_Item_Information_Effect_Status_L.text = "";
                    m_TMP_Store_Item_Information_Effect_Status_L.text += Refine_Condition_Item_Use("레        벨:", item.m_sStatus_Effect.GetSTATUS_LV());
                    m_TMP_Store_Item_Information_Effect_Status_L.text += Refine_Condition_Item_Use("\n체        력:", item.m_sStatus_Effect.GetSTATUS_HP_Max());
                    m_TMP_Store_Item_Information_Effect_Status_L.text += Refine_Condition_Item_Use("\n데  미  지:", item.m_sStatus_Effect.GetSTATUS_Damage_Total());
                    m_TMP_Store_Item_Information_Effect_Status_L.text += Refine_Condition_Item_Use("\n이동속도:", item.m_sStatus_Effect.GetSTATUS_Speed());
                    m_TMP_Store_Item_Information_Effect_Status_R.text = "";
                    m_TMP_Store_Item_Information_Effect_Status_R.text += "\n";
                    m_TMP_Store_Item_Information_Effect_Status_R.text += Refine_Condition_Item_Use("마        나:", item.m_sStatus_Effect.GetSTATUS_MP_Max());
                    m_TMP_Store_Item_Information_Effect_Status_R.text += Refine_Condition_Item_Use("\n방  어  력:", item.m_sStatus_Effect.GetSTATUS_Defence_Physical());
                    m_TMP_Store_Item_Information_Effect_Status_R.text += Refine_Condition_Item_Use("\n공격속도:", item.m_sStatus_Effect.GetSTATUS_AttackSpeed());
                }
                break;
            case E_ITEM_USE_TYPE.REINFORCEMENT:
                {
                    m_TMP_Store_Item_Information_Effect_Status_L.text = "";
                    m_TMP_Store_Item_Information_Effect_Status_L.text += Refine_Condition_Item_Use("레        벨:", item.m_Reinforcement_Effect.GetReinforcementSTATUS().GetSTATUS_LV());
                    m_TMP_Store_Item_Information_Effect_Status_L.text += Refine_Condition_Item_Use("\n체        력:", item.m_Reinforcement_Effect.GetReinforcementSTATUS().GetSTATUS_HP_Max());
                    m_TMP_Store_Item_Information_Effect_Status_L.text += Refine_Condition_Item_Use("\n데  미  지:", item.m_Reinforcement_Effect.GetReinforcementSTATUS().GetSTATUS_Damage_Total());
                    m_TMP_Store_Item_Information_Effect_Status_L.text += Refine_Condition_Item_Use("\n이동속도:", item.m_Reinforcement_Effect.GetReinforcementSTATUS().GetSTATUS_Speed());
                    m_TMP_Store_Item_Information_Effect_Status_R.text = "";
                    m_TMP_Store_Item_Information_Effect_Status_R.text += "\n";
                    m_TMP_Store_Item_Information_Effect_Status_R.text += Refine_Condition_Item_Use("마        나:", item.m_Reinforcement_Effect.GetReinforcementSTATUS().GetSTATUS_MP_Max());
                    m_TMP_Store_Item_Information_Effect_Status_R.text += Refine_Condition_Item_Use("\n방  어  력:", item.m_Reinforcement_Effect.GetReinforcementSTATUS().GetSTATUS_Defence_Physical());
                    m_TMP_Store_Item_Information_Effect_Status_R.text += Refine_Condition_Item_Use("\n공격속도:", item.m_Reinforcement_Effect.GetReinforcementSTATUS().GetSTATUS_AttackSpeed());
                }
                break;
            case E_ITEM_USE_TYPE.GIFT:
                {
                    m_TMP_Store_Item_Information_Effect_Status_L.text = "";
                    m_TMP_Store_Item_Information_Effect_Status_R.text = "";
                }
                break;
        }
        // 소비 아이템 효과 Soc.
        if (item.m_eItemUseType == E_ITEM_USE_TYPE.REINFORCEMENT)
        {
            m_TMP_Store_Item_Information_Effect_Soc_L.text = "";
            m_TMP_Store_Item_Information_Effect_Soc_L.text += Refine_Condition_Item_Use("명        예:", item.m_Reinforcement_Effect.GetReinforcementSOC().GetSOC_Honor());
            m_TMP_Store_Item_Information_Effect_Soc_L.text += Refine_Condition_Item_Use("\n인        간:", item.m_Reinforcement_Effect.GetReinforcementSOC().GetSOC_Human());
            m_TMP_Store_Item_Information_Effect_Soc_L.text += Refine_Condition_Item_Use("\n동        물:", item.m_Reinforcement_Effect.GetReinforcementSOC().GetSOC_Animal());
            m_TMP_Store_Item_Information_Effect_Soc_L.text += Refine_Condition_Item_Use("\n슬  라  임:", item.m_Reinforcement_Effect.GetReinforcementSOC().GetSOC_Slime());
            m_TMP_Store_Item_Information_Effect_Soc_L.text += Refine_Condition_Item_Use("\n스켈레톤:", item.m_Reinforcement_Effect.GetReinforcementSOC().GetSOC_Skeleton());
            m_TMP_Store_Item_Information_Effect_Soc_R.text = "";
            m_TMP_Store_Item_Information_Effect_Soc_R.text += "\n";
            m_TMP_Store_Item_Information_Effect_Soc_R.text += Refine_Condition_Item_Use("앤        트:", item.m_Reinforcement_Effect.GetReinforcementSOC().GetSOC_Ents());
            m_TMP_Store_Item_Information_Effect_Soc_R.text += Refine_Condition_Item_Use("\n마        족:", item.m_Reinforcement_Effect.GetReinforcementSOC().GetSOC_Devil());
            m_TMP_Store_Item_Information_Effect_Soc_R.text += Refine_Condition_Item_Use("\n용        족:", item.m_Reinforcement_Effect.GetReinforcementSOC().GetSOC_Dragon());
            m_TMP_Store_Item_Information_Effect_Soc_R.text += Refine_Condition_Item_Use("\n어        둠:", item.m_Reinforcement_Effect.GetReinforcementSOC().GetSOC_Shadow());
        }
        else if (item.m_eItemUseType == E_ITEM_USE_TYPE.GIFT)
        {
            m_TMP_Store_Item_Information_Effect_Soc_L.text = "";
            m_TMP_Store_Item_Information_Effect_Soc_R.text = "";
        }
        else
        {
            m_TMP_Store_Item_Information_Effect_Soc_L.text = "";
            m_TMP_Store_Item_Information_Effect_Soc_L.text += Refine_Condition_Item_Use("명        예:", item.m_sSoc_Effect.GetSOC_Honor());
            m_TMP_Store_Item_Information_Effect_Soc_L.text += Refine_Condition_Item_Use("\n인        간:", item.m_sSoc_Effect.GetSOC_Human());
            m_TMP_Store_Item_Information_Effect_Soc_L.text += Refine_Condition_Item_Use("\n동        물:", item.m_sSoc_Effect.GetSOC_Animal());
            m_TMP_Store_Item_Information_Effect_Soc_L.text += Refine_Condition_Item_Use("\n슬  라  임:", item.m_sSoc_Effect.GetSOC_Slime());
            m_TMP_Store_Item_Information_Effect_Soc_L.text += Refine_Condition_Item_Use("\n스켈레톤:", item.m_sSoc_Effect.GetSOC_Skeleton());
            m_TMP_Store_Item_Information_Effect_Soc_R.text = "";
            m_TMP_Store_Item_Information_Effect_Soc_R.text += "\n";
            m_TMP_Store_Item_Information_Effect_Soc_R.text += Refine_Condition_Item_Use("앤        트:", item.m_sSoc_Effect.GetSOC_Ents());
            m_TMP_Store_Item_Information_Effect_Soc_R.text += Refine_Condition_Item_Use("\n마        족:", item.m_sSoc_Effect.GetSOC_Devil());
            m_TMP_Store_Item_Information_Effect_Soc_R.text += Refine_Condition_Item_Use("\n용        족:", item.m_sSoc_Effect.GetSOC_Dragon());
            m_TMP_Store_Item_Information_Effect_Soc_R.text += Refine_Condition_Item_Use("\n어        둠:", item.m_sSoc_Effect.GetSOC_Shadow());
        }

        // 소비 아이템 사용 조건.
        // 소비 아이템 사용 조건 Status.
        m_TMP_Store_Item_Information_Condition_Status_L.text = "";
        //m_TMP_Itemslot_Use_Information_Content_Condition_Status_L.text += "레        벨: " + Refine_Condition(item.m_sStatus_Limit_Min.GetSTATUS_LV(), item.m_sStatus_Limit_Max.GetSTATUS_LV()) + "\n";
        m_TMP_Store_Item_Information_Condition_Status_L.text += Refine_Condition("레        벨: ", "\n", item.m_sStatus_Limit_Min.GetSTATUS_LV(), item.m_sStatus_Limit_Max.GetSTATUS_LV(), player.m_sStatus.GetSTATUS_LV());
        //m_TMP_Itemslot_Use_Information_Content_Condition_Status_L.text += "체        력: " + Refine_Condition(item.m_sStatus_Limit_Min.GetSTATUS_HP_Max(), item.m_sStatus_Limit_Max.GetSTATUS_HP_Max()) + "\n";
        m_TMP_Store_Item_Information_Condition_Status_L.text += Refine_Condition("체        력: ", "\n", item.m_sStatus_Limit_Min.GetSTATUS_HP_Max(), item.m_sStatus_Limit_Max.GetSTATUS_HP_Max(), player.m_sStatus.GetSTATUS_HP_Max());
        //m_TMP_Itemslot_Use_Information_Content_Condition_Status_L.text += "데  미  지: " + Refine_Condition(item.m_sStatus_Limit_Min.GetSTATUS_Damage_Total(), item.m_sStatus_Limit_Max.GetSTATUS_Damage_Total()) + "\n";
        m_TMP_Store_Item_Information_Condition_Status_L.text += Refine_Condition("데  미  지: ", "\n", item.m_sStatus_Limit_Min.GetSTATUS_Damage_Total(), item.m_sStatus_Limit_Max.GetSTATUS_Damage_Total(), player.m_sStatus.GetSTATUS_Damage_Total());
        //m_TMP_Itemslot_Use_Information_Content_Condition_Status_L.text += "이동속도: " + Refine_Condition(item.m_sStatus_Limit_Min.GetSTATUS_Speed(), item.m_sStatus_Limit_Max.GetSTATUS_Speed());
        m_TMP_Store_Item_Information_Condition_Status_L.text += Refine_Condition("이동속도: ", "", item.m_sStatus_Limit_Min.GetSTATUS_Speed(), item.m_sStatus_Limit_Max.GetSTATUS_Speed(), player.m_sStatus.GetSTATUS_Speed());
        m_TMP_Store_Item_Information_Condition_Status_R.text = "";
        m_TMP_Store_Item_Information_Condition_Status_R.text += "\n";
        //m_TMP_Itemslot_Use_Information_Content_Condition_Status_R.text += "마        나: " + Refine_Condition(item.m_sStatus_Limit_Min.GetSTATUS_MP_Max(), item.m_sStatus_Limit_Max.GetSTATUS_MP_Max()) + "\n";
        m_TMP_Store_Item_Information_Condition_Status_R.text += Refine_Condition("마        나: ", "\n", item.m_sStatus_Limit_Min.GetSTATUS_MP_Max(), item.m_sStatus_Limit_Max.GetSTATUS_MP_Max(), player.m_sStatus.GetSTATUS_MP_Max());
        //m_TMP_Itemslot_Use_Information_Content_Condition_Status_R.text += "방  어  력: " + Refine_Condition(item.m_sStatus_Limit_Min.GetSTATUS_Defence_Physical(), item.m_sStatus_Limit_Max.GetSTATUS_Defence_Physical()) + "\n";
        m_TMP_Store_Item_Information_Condition_Status_R.text += Refine_Condition("방  어  력: ", "\n", item.m_sStatus_Limit_Min.GetSTATUS_Defence_Physical(), item.m_sStatus_Limit_Max.GetSTATUS_Defence_Physical(), player.m_sStatus.GetSTATUS_Defence_Physical());
        //m_TMP_Itemslot_Use_Information_Content_Condition_Status_R.text += "공격속도: " + Refine_Condition(item.m_sStatus_Limit_Min.GetSTATUS_AttackSpeed(), item.m_sStatus_Limit_Max.GetSTATUS_AttackSpeed());
        m_TMP_Store_Item_Information_Condition_Status_R.text += Refine_Condition("공격속도: ", "", item.m_sStatus_Limit_Min.GetSTATUS_AttackSpeed(), item.m_sStatus_Limit_Max.GetSTATUS_AttackSpeed(), player.m_sStatus.GetSTATUS_AttackSpeed());

        // 소비 아이템 사용 조건 Soc.
        m_TMP_Store_Item_Information_Condition_Soc_L.text = "";
        //m_TMP_Itemslot_Use_Information_Content_Condition_Soc_L.text += "평        판: " + Refine_Condition(item.m_sSoc_Limit_Min.GetSOC_Honor(), item.m_sSoc_Limit_Max.GetSOC_Honor()) + "\n";
        m_TMP_Store_Item_Information_Condition_Soc_L.text += Refine_Condition("명        예: ", "\n", item.m_sSoc_Limit_Min.GetSOC_Honor(), item.m_sSoc_Limit_Max.GetSOC_Honor(), player.m_sSoc.GetSOC_Honor());
        //m_TMP_Itemslot_Use_Information_Content_Condition_Soc_L.text += "인        간: " + Refine_Condition(item.m_sSoc_Limit_Min.GetSOC_Human(), item.m_sSoc_Limit_Max.GetSOC_Human()) + "\n";
        m_TMP_Store_Item_Information_Condition_Soc_L.text += Refine_Condition("인        간: ", "\n", item.m_sSoc_Limit_Min.GetSOC_Human(), item.m_sSoc_Limit_Max.GetSOC_Human(), player.m_sSoc.GetSOC_Human());
        //m_TMP_Itemslot_Use_Information_Content_Condition_Soc_L.text += "동        물: " + Refine_Condition(item.m_sSoc_Limit_Min.GetSOC_Animal(), item.m_sSoc_Limit_Max.GetSOC_Animal()) + "\n";
        m_TMP_Store_Item_Information_Condition_Soc_L.text += Refine_Condition("동        물: ", "\n", item.m_sSoc_Limit_Min.GetSOC_Animal(), item.m_sSoc_Limit_Max.GetSOC_Animal(), player.m_sSoc.GetSOC_Animal());
        //m_TMP_Itemslot_Use_Information_Content_Condition_Soc_L.text += "슬  라  임: " + Refine_Condition(item.m_sSoc_Limit_Min.GetSOC_Slime(), item.m_sSoc_Limit_Max.GetSOC_Slime()) + "\n";
        m_TMP_Store_Item_Information_Condition_Soc_L.text += Refine_Condition("슬  라  임: ", "\n", item.m_sSoc_Limit_Min.GetSOC_Slime(), item.m_sSoc_Limit_Max.GetSOC_Slime(), player.m_sSoc.GetSOC_Slime());
        //m_TMP_Itemslot_Use_Information_Content_Condition_Soc_L.text += "스켈레톤: " + Refine_Condition(item.m_sSoc_Limit_Min.GetSOC_Skeleton(), item.m_sSoc_Limit_Max.GetSOC_Skeleton());
        m_TMP_Store_Item_Information_Condition_Soc_L.text += Refine_Condition("스켈레톤: ", "", item.m_sSoc_Limit_Min.GetSOC_Skeleton(), item.m_sSoc_Limit_Max.GetSOC_Skeleton(), player.m_sSoc.GetSOC_Skeleton());
        m_TMP_Store_Item_Information_Condition_Soc_R.text = "";
        m_TMP_Store_Item_Information_Condition_Soc_R.text += "\n";
        //m_TMP_Itemslot_Use_Information_Content_Condition_Soc_R.text += "앤        트: " + Refine_Condition(item.m_sSoc_Limit_Min.GetSOC_Ents(), item.m_sSoc_Limit_Max.GetSOC_Ents()) + "\n";
        m_TMP_Store_Item_Information_Condition_Soc_R.text += Refine_Condition("앤        트: ", "\n", item.m_sSoc_Limit_Min.GetSOC_Ents(), item.m_sSoc_Limit_Max.GetSOC_Ents(), player.m_sSoc.GetSOC_Ents());
        //m_TMP_Itemslot_Use_Information_Content_Condition_Soc_R.text += "마        족: " + Refine_Condition(item.m_sSoc_Limit_Min.GetSOC_Devil(), item.m_sSoc_Limit_Max.GetSOC_Devil()) + "\n";
        m_TMP_Store_Item_Information_Condition_Soc_R.text += Refine_Condition("마        족: ", "\n", item.m_sSoc_Limit_Min.GetSOC_Devil(), item.m_sSoc_Limit_Max.GetSOC_Devil(), player.m_sSoc.GetSOC_Devil());
        //m_TMP_Itemslot_Use_Information_Content_Condition_Soc_R.text += "용        족: " + Refine_Condition(item.m_sSoc_Limit_Min.GetSOC_Dragon(), item.m_sSoc_Limit_Max.GetSOC_Dragon()) + "\n";
        m_TMP_Store_Item_Information_Condition_Soc_R.text += Refine_Condition("용        족: ", "\n", item.m_sSoc_Limit_Min.GetSOC_Dragon(), item.m_sSoc_Limit_Max.GetSOC_Dragon(), player.m_sSoc.GetSOC_Dragon());
        //m_TMP_Itemslot_Use_Information_Content_Condition_Soc_R.text += "어        둠: " + Refine_Condition(item.m_sSoc_Limit_Min.GetSOC_Shadow(), item.m_sSoc_Limit_Max.GetSOC_Shadow());
        m_TMP_Store_Item_Information_Condition_Soc_R.text += Refine_Condition("어        듬: ", "", item.m_sSoc_Limit_Min.GetSOC_Shadow(), item.m_sSoc_Limit_Max.GetSOC_Shadow(), player.m_sSoc.GetSOC_Shadow());

        // 소비 아이템 설명.
        m_TMP_Store_Item_Information_ItemDescription_Content.text = "";
        m_TMP_Store_Item_Information_ItemDescription_Content.text += item.GetItemDescription();

        //if (item.m_eItemUseType == E_ITEM_USE_TYPE.GIFT)
        //    m_TMP_Store_Item_Information_ItemDescription_Content.text += "\n\n[획득할 수 있는 아이템 정보]\n" + item.Return_Gift_List();

        // 착용, 사용 가능 여부.
        m_gPanel_Store_Information_CanUse.SetActive(true);
        if (m_bEU_Condition_Check == true)
            m_TMP_Store_Information_CanUse.text = m_sColor_White + "사용가능" + m_sColor_End;
        else
            m_TMP_Store_Information_CanUse.text = m_sColor_Red + "사용불가능" + m_sColor_End;

        if (m_e_Store_Item_Information == E_STORE_ITEM_INFORMATION.EFFECT)
        {
            m_e_Store_Item_Information = E_STORE_ITEM_INFORMATION.DESCRIPTION;
            m_gPanel_Store_Item_Information_Effect.SetActive(false);
            m_gPanel_Store_Item_Information_Condition.SetActive(false);
            m_gPanel_Store_Item_Information_ItemDescription.SetActive(true);
            m_Scrollbar_Store_Item_Information_ItemDescription_Content.value = 1;
            m_Scrollbar_SetItemEffect_Content_SS_Description.value = 1;
        }

        m_gPanel_SetItemEffect.SetActive(false);

        UpdateItemUse_GiftInformation(item);
    }
    public void UpdateStoreItemInformation(Item_Etc item)
    {
        m_e_Store_Item_Information = E_STORE_ITEM_INFORMATION.DESCRIPTION;
        m_gPanel_Store_Item_Information_Condition.SetActive(false);
        m_gPanel_Store_Item_Information_Effect.SetActive(false);
        m_gPanel_Store_Item_Information_ItemDescription.SetActive(true);

        m_TMP_Store_Item_Information_UpBar_Name.text = item.m_sItemName;

        // 기타 아이템 이미지.
        m_IMG_Store_Item_Information_Image_ItemSprite.sprite = item.m_sp_Sprite;
        m_IMG_Store_Item_Information_Image_ItemSprite.color = new Color(1, 1, 1, 0.75f);

        // 분류.
        m_TMP_Store_Item_Information_ItemInformation.text = "";
        switch (item.m_eItemEtcType)
        {
            case E_ITEM_ETC_TYPE.JUNK:
                {
                    m_sBuffer = "전리품";
                    break;
                }
            case E_ITEM_ETC_TYPE.QUEST:
                {
                    m_sBuffer = "퀘스트 아이템";
                }
                break;
            case E_ITEM_ETC_TYPE.MATERIAL:
                {
                    m_sBuffer = "재료 아이템";
                }
                break;
            case E_ITEM_ETC_TYPE.MULTIPLE:
                {
                    m_sBuffer = "다용도 아이템";
                }
                break;
            case E_ITEM_ETC_TYPE.NULL:
                {
                    m_sBuffer = "???";
                }
                break;
        }
        m_TMP_Store_Item_Information_ItemInformation.text += "분류: " + m_sBuffer + "\n";
        // 등급.
        switch (item.m_eItemGrade)
        {
            case E_ITEM_GRADE.NORMAL:
                {
                    m_sBuffer = "노멀";
                }
                break;
            case E_ITEM_GRADE.COMMON:
                {
                    m_sBuffer = "일반";
                }
                break;
            case E_ITEM_GRADE.RARE:
                {
                    m_sBuffer = "레어";
                }
                break;
            case E_ITEM_GRADE.RELIC:
                {
                    m_sBuffer = "유물";
                }
                break;
            case E_ITEM_GRADE.LEGEND:
                {
                    m_sBuffer = "전설";
                }
                break;
            case E_ITEM_GRADE.MYTH:
                {
                    m_sBuffer = "신화";
                }
                break;
        }
        m_TMP_Store_Item_Information_ItemInformation.text += "등급: " + m_sBuffer + "\n";

        // 기타 아이템 설명.
        m_TMP_Store_Item_Information_ItemDescription_Content.text = "";
        m_TMP_Store_Item_Information_ItemDescription_Content.text += item.GetItemDescription();

        m_gPanel_Store_Information_CanUse.SetActive(false);

        m_gPanel_SetItemEffect.SetActive(false);
    }

    void UpdateItemEquipInformation_SetItemEffect(Item_Equip item)
    {
        m_nList_SetItemEffect_Code.Clear();
        int setitemeffectnumber = ItemSetEffectManager.instance.Return_SetItemEffect(item.m_nItemCode);
        if (setitemeffectnumber == 0)
        {
            m_gPanel_SetItemEffect.SetActive(false);
            m_gPanel_NoItemInfo.SetActive(true);
            m_TMP_NoItemInfo.text = "선택된 아이템이 없습니다.";

            m_ISE = null;
        }
        else
        {
            m_gPanel_SetItemEffect.SetActive(true);
            //m_gPanel_NoItemInfo.SetActive(false);

            m_ISE = ItemSetEffectManager.m_Dictionary_ItemSetEffect[setitemeffectnumber];

            m_TMP_SetItemEffect_Name.text = m_ISE.m_sItemSetEffect_Name;

            m_nSetItemEffect_Count = m_ISE.m_Dictionary_Item_Equip_Code.Count;
            for (int i = 0; i < m_nSetItemEffect_Count; i++)
            {
                if (m_ISE.m_Dictionary_STATUS_Effect[i + 1].CheckIdentity(new STATUS(0)) == false ||
                    m_ISE.m_Dictionary_SOC_Effect[i + 1].CheckIdentity(new SOC(0)) == false)
                {
                    m_nList_SetItemEffect_Code.Add(i + 1);
                }
            }
            m_nSetItemEffect_Current = m_nList_SetItemEffect_Code[0];
            m_nSetItemEffect_List_Index = 0;
            m_TMP_SetItemEffect_Content_UpBar_Name.text = m_nList_SetItemEffect_Code[m_nSetItemEffect_List_Index].ToString() + "개 아이템 세트 효과";

            UpdateItemEquipInformation_SetItemEffect_Content();

            m_BTN_SetItemEffect_Content_UpBar_Left.onClick.RemoveAllListeners();
            m_BTN_SetItemEffect_Content_UpBar_Left.onClick.AddListener(delegate { Set_BTN_SetItemEffect_UpBar_Left(); });
            m_BTN_SetItemEffect_Content_UpBar_Right.onClick.RemoveAllListeners();
            m_BTN_SetItemEffect_Content_UpBar_Right.onClick.AddListener(delegate { Set_BTN_SetItemEffect_UpBar_Right(); });
            UpdateItemEquipInformation_SetItemEffect_UpBar();
        }
    }
    void UpdateItemEquipInformation_SetItemEffect_UpBar()
    {
        if (m_nSetItemEffect_Current == m_nList_SetItemEffect_Code[0])
        {
            m_gBTN_SetItemEffect_Content_UpBar_Left.SetActive(false);
        }
        else
        {
            m_gBTN_SetItemEffect_Content_UpBar_Left.SetActive(true);
        }
        if (m_nSetItemEffect_Current == m_nList_SetItemEffect_Code[m_nList_SetItemEffect_Code.Count - 1])
        {
            m_gBTN_SetItemEffect_Content_UpBar_Right.SetActive(false);
        }
        else
        {
            m_gBTN_SetItemEffect_Content_UpBar_Right.SetActive(true);
        }
    }
    void UpdateItemEquipInformation_SetItemEffect_Content()
    {
        m_TMP_SetItemEffect_Content_UpBar_Name.text = Refine_Condition(m_nList_SetItemEffect_Code[m_nSetItemEffect_List_Index].ToString() + "개 아이템 세트 효과");

        m_TMP_SetItemEffect_Content_SS_Description.text = Refine_Condition(m_ISE.m_Dictionary_Description[m_nList_SetItemEffect_Code[m_nSetItemEffect_List_Index]], false);

        m_TMP_SetItemEffect_Content_SS_Status_L.text = Refine_Condition("레        벨:", m_ISE.m_Dictionary_STATUS_Effect[m_nList_SetItemEffect_Code[m_nSetItemEffect_List_Index]].GetSTATUS_LV());
        m_TMP_SetItemEffect_Content_SS_Status_L.text += Refine_Condition("\n체        력:", m_ISE.m_Dictionary_STATUS_Effect[m_nList_SetItemEffect_Code[m_nSetItemEffect_List_Index]].GetSTATUS_HP_Max());
        m_TMP_SetItemEffect_Content_SS_Status_L.text += Refine_Condition("\n데  미  지:", m_ISE.m_Dictionary_STATUS_Effect[m_nList_SetItemEffect_Code[m_nSetItemEffect_List_Index]].GetSTATUS_Damage_Total());
        m_TMP_SetItemEffect_Content_SS_Status_L.text += Refine_Condition("\n이동속도:", m_ISE.m_Dictionary_STATUS_Effect[m_nList_SetItemEffect_Code[m_nSetItemEffect_List_Index]].GetSTATUS_Speed());
        m_TMP_SetItemEffect_Content_SS_Status_R.text = "";
        m_TMP_SetItemEffect_Content_SS_Status_R.text += Refine_Condition("\n마        나:", m_ISE.m_Dictionary_STATUS_Effect[m_nList_SetItemEffect_Code[m_nSetItemEffect_List_Index]].GetSTATUS_MP_Max());
        m_TMP_SetItemEffect_Content_SS_Status_R.text += Refine_Condition("\n방  어  력:", m_ISE.m_Dictionary_STATUS_Effect[m_nList_SetItemEffect_Code[m_nSetItemEffect_List_Index]].GetSTATUS_Defence_Physical());
        m_TMP_SetItemEffect_Content_SS_Status_R.text += Refine_Condition("\n공격속도:", m_ISE.m_Dictionary_STATUS_Effect[m_nList_SetItemEffect_Code[m_nSetItemEffect_List_Index]].GetSTATUS_AttackSpeed());

        m_TMP_SetItemEffect_Content_SS_Soc_L.text = "";
        m_TMP_SetItemEffect_Content_SS_Soc_L.text += Refine_Condition("명        예:", m_ISE.m_Dictionary_SOC_Effect[m_nList_SetItemEffect_Code[m_nSetItemEffect_List_Index]].GetSOC_Honor());
        m_TMP_SetItemEffect_Content_SS_Soc_L.text += Refine_Condition("\n인        간:", m_ISE.m_Dictionary_SOC_Effect[m_nList_SetItemEffect_Code[m_nSetItemEffect_List_Index]].GetSOC_Human());
        m_TMP_SetItemEffect_Content_SS_Soc_L.text += Refine_Condition("\n동        물:", m_ISE.m_Dictionary_SOC_Effect[m_nList_SetItemEffect_Code[m_nSetItemEffect_List_Index]].GetSOC_Animal());
        m_TMP_SetItemEffect_Content_SS_Soc_L.text += Refine_Condition("\n슬  라  임:", m_ISE.m_Dictionary_SOC_Effect[m_nList_SetItemEffect_Code[m_nSetItemEffect_List_Index]].GetSOC_Slime());
        m_TMP_SetItemEffect_Content_SS_Soc_L.text += Refine_Condition("\n스켈레톤:", m_ISE.m_Dictionary_SOC_Effect[m_nList_SetItemEffect_Code[m_nSetItemEffect_List_Index]].GetSOC_Skeleton());
        m_TMP_SetItemEffect_Content_SS_Soc_R.text = "";
        m_TMP_SetItemEffect_Content_SS_Soc_R.text += "";
        m_TMP_SetItemEffect_Content_SS_Soc_R.text += Refine_Condition("\n앤        트:", m_ISE.m_Dictionary_SOC_Effect[m_nList_SetItemEffect_Code[m_nSetItemEffect_List_Index]].GetSOC_Ents());
        m_TMP_SetItemEffect_Content_SS_Soc_R.text += Refine_Condition("\n마        족:", m_ISE.m_Dictionary_SOC_Effect[m_nList_SetItemEffect_Code[m_nSetItemEffect_List_Index]].GetSOC_Devil());
        m_TMP_SetItemEffect_Content_SS_Soc_R.text += Refine_Condition("\n용        족:", m_ISE.m_Dictionary_SOC_Effect[m_nList_SetItemEffect_Code[m_nSetItemEffect_List_Index]].GetSOC_Dragon());
        m_TMP_SetItemEffect_Content_SS_Soc_R.text += Refine_Condition("\n어        듬:", m_ISE.m_Dictionary_SOC_Effect[m_nList_SetItemEffect_Code[m_nSetItemEffect_List_Index]].GetSOC_Shadow());
    }

    void UpdateItemUse_GiftInformation(Item_Use item)
    {
        m_Scrollbar_Gift_Info_Content_Description_Down.value = 1;

        if (item.m_eItemUseType != E_ITEM_USE_TYPE.GIFT)
        {
            m_gPanel_Gift_Info.SetActive(false);
            m_gPanel_NoItemInfo.SetActive(true);
            m_TMP_NoItemInfo.text = "선택된 아이템이 없습니다.";
        }
        else
        {
            m_gPanel_Gift_Info.SetActive(true);

            // 기프트 분류, 설명
            switch (item.m_eItemUseGiftType)
            {
                case E_ITEM_USE_GIFT_TYPE.FIXEDBOX:
                    {
                        if (item.m_bDisplay_Gift_Item == true)
                        {
                            m_TMP_Gift_Info_Content_Type_Name.text = "전체 확정 지급형 기프트 박스";
                            m_TMP_Gift_Info_Content_Description_Up.text = "아래 아이템을 모두 획득합니다.";
                        }
                        else
                        {
                            m_TMP_Gift_Info_Content_Description_Up.text = "기프트 박스의 내용물을 확인할 수 없습니다.";
                        }
                    }
                    break;
                case E_ITEM_USE_GIFT_TYPE.RANDOMBOX_INDEPENDENTTRIAL:
                    {
                        if (item.m_bDisplay_Gift_Item == true)
                        {
                            m_TMP_Gift_Info_Content_Type_Name.text = "랜덤 지급형 기프트 박스 _ A 타입";
                            m_TMP_Gift_Info_Content_Description_Up.text = "아래 아이템을 랜덤으로 획득합니다. ( " + item.m_nRandomBox_PickCount_Min.ToString() + " 개 ~ " + item.m_nRandomBox_PickCount_Max.ToString() + " 개 )\n";
                            m_TMP_Gift_Info_Content_Description_Up.text += "이때 아이템의 중복 획득이 가능합니다.";
                        }
                        else
                        {
                            m_TMP_Gift_Info_Content_Description_Up.text = "기프트 박스의 내용물을 확인할 수 없습니다.";
                        }
                    }
                    break;
                case E_ITEM_USE_GIFT_TYPE.RANDOMBOX_DEPENDENTTRIAL:
                    {
                        if (item.m_bDisplay_Gift_Item == true)
                        {
                            m_TMP_Gift_Info_Content_Type_Name.text = "랜덤 지급형 기프트 박스 _ B 타입";
                            m_TMP_Gift_Info_Content_Description_Up.text = "아래 아이템을 랜덤으로 획득합니다. ( " + item.m_nRandomBox_PickCount_Min.ToString() + " 개 ~ " + item.m_nRandomBox_PickCount_Max.ToString() + " 개 )\n";
                            m_TMP_Gift_Info_Content_Description_Up.text += "이때 아이템의 중복 획득이 불가능합니다.";
                        }
                        else
                        {
                            m_TMP_Gift_Info_Content_Description_Up.text = "기프트 박스의 내용물을 확인할 수 없습니다.";
                        }
                    }
                    break;
            }

            for (int i = 0; i < m_gList_Panel_ItemInfo.Count; i++)
            {
                Destroy(m_gList_Panel_ItemInfo[i]);
            }
            m_gList_Panel_ItemInfo.Clear();

            // 획득할 수 있는 아이템 목록.
            if (item.m_bDisplay_Gift_Item == false)
            {
                m_gSV_Gift_Info_Content_Description_Down.SetActive(false);
                m_gPanel_Gift_Info_Content_Description_X.SetActive(true);
            }
            else
            {
                m_gSV_Gift_Info_Content_Description_Down.SetActive(true);
                m_gPanel_Gift_Info_Content_Description_X.SetActive(false);

                GameObject copypanel;
                RectTransform contentpos;

                switch (item.m_eItemUseGiftType)
                {
                    case E_ITEM_USE_GIFT_TYPE.FIXEDBOX:
                        {
                            for (int i = 0; i < item.m_nDictionary_Gift_Item_Equip_Code.Count; i++)
                            {
                                copypanel = Instantiate(m_gPanel_ItemInfo);
                                copypanel.GetComponent<Iteminfo>().Display(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[item.m_nDictionary_Gift_Item_Equip_Code[i]].m_sp_Sprite,
                                    ItemManager.instance.m_Dictionary_MonsterDrop_Equip[item.m_nDictionary_Gift_Item_Equip_Code[i]].m_sItemName + "\n" + item.m_nDictionary_Gift_Item_Equip_Count[i] + " 개");

                                contentpos = copypanel.GetComponent<RectTransform>();
                                contentpos.SetParent(m_gContent_Gift_Info_Content_Description_Down.transform);
                                contentpos.transform.localScale = new Vector3(1, 1, 1);
                                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                                m_gList_Panel_ItemInfo.Add(copypanel);
                            }
                            for (int i = 0; i < item.m_nDictionary_Gift_Item_Use_Code.Count; i++)
                            {
                                copypanel = Instantiate(m_gPanel_ItemInfo);
                                copypanel.GetComponent<Iteminfo>().Display(ItemManager.instance.m_Dictionary_MonsterDrop_Use[item.m_nDictionary_Gift_Item_Use_Code[i]].m_sp_Sprite,
                                    ItemManager.instance.m_Dictionary_MonsterDrop_Use[item.m_nDictionary_Gift_Item_Use_Code[i]].m_sItemName + "\n" + item.m_nDictionary_Gift_Item_Use_Count[i] + " 개");

                                contentpos = copypanel.GetComponent<RectTransform>();
                                contentpos.SetParent(m_gContent_Gift_Info_Content_Description_Down.transform);
                                contentpos.transform.localScale = new Vector3(1, 1, 1);
                                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                                m_gList_Panel_ItemInfo.Add(copypanel);
                            }
                            for (int i = 0; i < item.m_nDictionary_Gift_Item_Etc_Code.Count; i++)
                            {
                                copypanel = Instantiate(m_gPanel_ItemInfo);
                                copypanel.GetComponent<Iteminfo>().Display(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[item.m_nDictionary_Gift_Item_Etc_Code[i]].m_sp_Sprite,
                                    ItemManager.instance.m_Dictionary_MonsterDrop_Etc[item.m_nDictionary_Gift_Item_Etc_Code[i]].m_sItemName + "\n" + item.m_nDictionary_Gift_Item_Etc_Count[i] + " 개");

                                contentpos = copypanel.GetComponent<RectTransform>();
                                contentpos.SetParent(m_gContent_Gift_Info_Content_Description_Down.transform);
                                contentpos.transform.localScale = new Vector3(1, 1, 1);
                                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                                m_gList_Panel_ItemInfo.Add(copypanel);
                            }
                        }
                        break;
                    case E_ITEM_USE_GIFT_TYPE.RANDOMBOX_INDEPENDENTTRIAL:
                    case E_ITEM_USE_GIFT_TYPE.RANDOMBOX_DEPENDENTTRIAL:
                        {
                            for (int i = 0; i < item.m_nDictionary_Gift_Item_Equip_Code.Count; i++)
                            {
                                copypanel = Instantiate(m_gPanel_ItemInfo);
                                copypanel.GetComponent<Iteminfo>().Display(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[item.m_nDictionary_Gift_Item_Equip_Code[i]].m_sp_Sprite,
                                    ItemManager.instance.m_Dictionary_MonsterDrop_Equip[item.m_nDictionary_Gift_Item_Equip_Code[i]].m_sItemName + "\n" + item.m_nDictionary_Gift_Item_Equip_Count[i] + " 개 " +
                                    Mathf.Round(((float)(item.m_nDictionary_Gift_Item_Equip_Probability[i] / (float)10000) * 100)) + " %\n");

                                contentpos = copypanel.GetComponent<RectTransform>();
                                contentpos.SetParent(m_gContent_Gift_Info_Content_Description_Down.transform);
                                contentpos.transform.localScale = new Vector3(1, 1, 1);
                                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                                m_gList_Panel_ItemInfo.Add(copypanel);
                            }
                            for (int i = 0; i < item.m_nDictionary_Gift_Item_Use_Code.Count; i++)
                            {
                                copypanel = Instantiate(m_gPanel_ItemInfo);
                                copypanel.GetComponent<Iteminfo>().Display(ItemManager.instance.m_Dictionary_MonsterDrop_Use[item.m_nDictionary_Gift_Item_Use_Code[i]].m_sp_Sprite,
                                    ItemManager.instance.m_Dictionary_MonsterDrop_Use[item.m_nDictionary_Gift_Item_Use_Code[i]].m_sItemName + "\n" + item.m_nDictionary_Gift_Item_Use_Count[i] + " 개 " +
                                    Mathf.Round(((float)(item.m_nDictionary_Gift_Item_Use_Probability[i] / (float)10000) * 100)) + " %\n");

                                contentpos = copypanel.GetComponent<RectTransform>();
                                contentpos.SetParent(m_gContent_Gift_Info_Content_Description_Down.transform);
                                contentpos.transform.localScale = new Vector3(1, 1, 1);
                                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                                m_gList_Panel_ItemInfo.Add(copypanel);
                            }
                            for (int i = 0; i < item.m_nDictionary_Gift_Item_Etc_Code.Count; i++)
                            {
                                copypanel = Instantiate(m_gPanel_ItemInfo);
                                copypanel.GetComponent<Iteminfo>().Display(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[item.m_nDictionary_Gift_Item_Etc_Code[i]].m_sp_Sprite,
                                    ItemManager.instance.m_Dictionary_MonsterDrop_Etc[item.m_nDictionary_Gift_Item_Etc_Code[i]].m_sItemName + "\n" + item.m_nDictionary_Gift_Item_Etc_Count[i] + " 개 " +
                                    Mathf.Round(((float)(item.m_nDictionary_Gift_Item_Etc_Probability[i] / (float)10000) * 100)) + " %\n");

                                contentpos = copypanel.GetComponent<RectTransform>();
                                contentpos.SetParent(m_gContent_Gift_Info_Content_Description_Down.transform);
                                contentpos.transform.localScale = new Vector3(1, 1, 1);
                                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                                m_gList_Panel_ItemInfo.Add(copypanel);
                            }
                        }
                        break;
                }
            }
        }
    }

    void UpdateItemUseInformation_Button(E_ITEM_USE_TYPE eiut)
    {
        m_BTN_Store_Item_Information_ChangeInformation_R.onClick.RemoveAllListeners();
        m_BTN_Store_Item_Information_ChangeInformation_R.onClick.AddListener(delegate { Press_Btn_R(eiut); });
        m_BTN_Store_Item_Information_ChangeInformation_L.onClick.RemoveAllListeners();
        m_BTN_Store_Item_Information_ChangeInformation_L.onClick.AddListener(delegate { Press_Btn_L(eiut); });
    }
    void UpdateItemUseInformation_Button()
    {
        m_BTN_Store_Item_Information_ChangeInformation_R.onClick.RemoveAllListeners();
        m_BTN_Store_Item_Information_ChangeInformation_R.onClick.AddListener(delegate { Press_Btn_R(); });
        m_BTN_Store_Item_Information_ChangeInformation_L.onClick.RemoveAllListeners();
        m_BTN_Store_Item_Information_ChangeInformation_L.onClick.AddListener(delegate { Press_Btn_L(); });
    }

    public void UpdateStoreTime(float ftime)
    {
        m_nMinute = 5 - (int)ftime / 60;
        m_nSecond = (300 - (int)ftime) % 60;
        if (m_nSecond > 0) m_nMinute -= 1;
        else if (m_nSecond == 60) m_nSecond = 0;

        if (m_nMinute > 0)
            m_TMP_Store_Item_Information_C_UpBar_Name.text = "상점 갱신까지 " + m_nMinute.ToString() + "분 " + m_nSecond.ToString() + "초";
        else
            m_TMP_Store_Item_Information_C_UpBar_Name.text = "상점 갱신까지 " + m_nSecond.ToString() + "초";
    }

    public void Exit()
    {
        m_gPanel_Store_Item_Information_Content.SetActive(false);
        m_gPanel_SetItemEffect.SetActive(false);
        m_gPanel_Gift_Info.SetActive(false);
        Initializing();
    }

    public void Initializing()
    {
        
    }



    // 착용조건에 대한 Text 문장 정제 함수.
    // ex)
    // 장비 아이템 A 의 착용조건에 LV 제한 '10 <= LV <= 19' 이 있을때 -> 레벨: 10 ~ 19 라고 Text 정제.
    // 장비 아이템 A 의 착용조건에 LV 제한 '-10000 <= LV <= 19' 이 있을때 -> 레벨: ~ 19 라고 Text 정제.
    // 장비 아이템 A 의 착용조건에 LV 제한 '10 <= LV <= 10000' 이 있을때 -> 레벨: 10 ~ 라고 Text 정제.
    // -10000, 10000 은 최소, 최대 제한이 없다는것을 의미.
    string Refine_Condition(float min, float max)
    {
        m_bRefine_Condition_Check = false;

        if (min == -10000 && max == 10000)
            m_bRefine_Condition_Check = false;
        else
            m_bRefine_Condition_Check = true;

        m_sBuffer = "";
        if (min != -10000)
            m_sBuffer += min;
        if (m_bRefine_Condition_Check == true)
            m_sBuffer += " ~ ";
        else
            m_sBuffer = "X";
        if (max != 10000)
            m_sBuffer += max;

        return m_sBuffer;
    }
    string Refine_Condition(string beforesentence, string aftersentence, float condition_min, float condition_max, float current_condition)
    {
        m_bRefine_Condition_Check = false;

        if (condition_min == -10000 && condition_max == 10000)
            m_bRefine_Condition_Check = false;
        else
            m_bRefine_Condition_Check = true;

        m_sBuffer = "";
        if (condition_min != -10000)
            m_sBuffer += condition_min;
        if (m_bRefine_Condition_Check == true)
            m_sBuffer += " ~ ";
        else
            m_sBuffer = "";
        if (condition_max != 10000)
            m_sBuffer += condition_max;

        m_sBuffer = beforesentence + m_sBuffer;

        if (m_bRefine_Condition_Check == true)
        {
            if (condition_min <= current_condition && current_condition <= condition_max)
            {
                m_sBuffer = m_sColor_White + m_sBuffer + m_sColor_End + aftersentence;
            }
            else
            {
                m_sBuffer = m_sColor_Red + m_sBuffer + m_sColor_End + aftersentence;
            }
        }
        else
        {
            m_sBuffer = m_sColor_Brown + m_sBuffer + m_sColor_End + aftersentence;
        }

        if (condition_min <= current_condition && current_condition <= condition_max)
        {
            if (m_bEU_Condition_Check == true)
                m_bEU_Condition_Check = true;
        }
        else
        {
            m_bEU_Condition_Check = false;
        }

        return m_sBuffer;
    }
    // 아이템 세트 효과 개수.
    string Refine_Condition(string sentence, bool isname = true)
    {
        m_nPlayerEquipment_SetItemEffect_Current = 0;
        if (Player_Equipment.m_bEquipment_Hat == true)
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Hat.m_nItemCode) == m_ISE.m_nItemSetEffect_Code)
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_Hat() == true)
                    m_nPlayerEquipment_SetItemEffect_Current += 1;
            }
        }
        if (Player_Equipment.m_bEquipment_Top == true)
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Top.m_nItemCode) == m_ISE.m_nItemSetEffect_Code)
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_Top() == true)
                    m_nPlayerEquipment_SetItemEffect_Current += 1;
            }
        }
        if (Player_Equipment.m_bEquipment_Bottoms == true)
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Bottoms.m_nItemCode) == m_ISE.m_nItemSetEffect_Code)
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_Bottoms() == true)
                    m_nPlayerEquipment_SetItemEffect_Current += 1;
            }
        }
        if (Player_Equipment.m_bEquipment_Shose == true)
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Shose.m_nItemCode) == m_ISE.m_nItemSetEffect_Code)
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_Shose() == true)
                    m_nPlayerEquipment_SetItemEffect_Current += 1;
            }
        }
        if (Player_Equipment.m_bEquipment_Gloves == true)
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Gloves.m_nItemCode) == m_ISE.m_nItemSetEffect_Code)
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_Gloves() == true)
                    m_nPlayerEquipment_SetItemEffect_Current += 1;
            }
        }
        if (Player_Equipment.m_bEquipment_Mainweapon == true)
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Mainweapon.m_nItemCode) == m_ISE.m_nItemSetEffect_Code)
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_MainWeapon() == true)
                    m_nPlayerEquipment_SetItemEffect_Current += 1;
            }
        }
        if (Player_Equipment.m_bEquipment_Subweapon == true)
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Subweapon.m_nItemCode) == m_ISE.m_nItemSetEffect_Code)
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_SubWeapon() == true)
                    m_nPlayerEquipment_SetItemEffect_Current += 1;
            }
        }

        if (m_nList_SetItemEffect_Code[m_nSetItemEffect_List_Index] <= m_nPlayerEquipment_SetItemEffect_Current)
        {
            if (isname == true)
                return m_sColor_White + sentence + " 적용" + m_sColor_End;
            else
                return m_sColor_White + sentence + m_sColor_End;
        }
        else
        {
            if (isname == true)
                return m_sColor_Brown + sentence + " 미적용" + m_sColor_End;
            else
                return m_sColor_Brown + sentence + m_sColor_End;
        }
    }
    // 각각의 옵션.(고정된 아이템 세트 효과)
    string Refine_Condition(string sentence, int number)
    {
        m_nPlayerEquipment_SetItemEffect_Current = 0;
        if (Player_Equipment.m_bEquipment_Hat == true)
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Hat.m_nItemCode) == m_ISE.m_nItemSetEffect_Code)
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_Hat() == true)
                    m_nPlayerEquipment_SetItemEffect_Current += 1;
            }
        }
        if (Player_Equipment.m_bEquipment_Top == true)
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Top.m_nItemCode) == m_ISE.m_nItemSetEffect_Code)
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_Top() == true)
                    m_nPlayerEquipment_SetItemEffect_Current += 1;
            }
        }
        if (Player_Equipment.m_bEquipment_Bottoms == true)
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Bottoms.m_nItemCode) == m_ISE.m_nItemSetEffect_Code)
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_Bottoms() == true)
                    m_nPlayerEquipment_SetItemEffect_Current += 1;
            }
        }
        if (Player_Equipment.m_bEquipment_Shose == true)
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Shose.m_nItemCode) == m_ISE.m_nItemSetEffect_Code)
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_Shose() == true)
                    m_nPlayerEquipment_SetItemEffect_Current += 1;
            }
        }
        if (Player_Equipment.m_bEquipment_Gloves == true)
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Gloves.m_nItemCode) == m_ISE.m_nItemSetEffect_Code)
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_Gloves() == true)
                    m_nPlayerEquipment_SetItemEffect_Current += 1;
            }
        }
        if (Player_Equipment.m_bEquipment_Mainweapon == true)
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Mainweapon.m_nItemCode) == m_ISE.m_nItemSetEffect_Code)
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_MainWeapon() == true)
                    m_nPlayerEquipment_SetItemEffect_Current += 1;
            }
        }
        if (Player_Equipment.m_bEquipment_Subweapon == true)
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Subweapon.m_nItemCode) == m_ISE.m_nItemSetEffect_Code)
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_SubWeapon() == true)
                    m_nPlayerEquipment_SetItemEffect_Current += 1;
            }
        }

        //if (m_nList_SetItemEffect_Code[m_nSetItemEffect_List_Index] <= m_nPlayerEquipment_SetItemEffect_Current)
        //{
        if (number > 0)
            return m_sColor_White + sentence + " " + number.ToString() + m_sColor_End;
        else if (number < 0)
            return m_sColor_WhiteGray + sentence + " " + number.ToString() + m_sColor_End;
        else
            return m_sColor_Brown + sentence + " " + number.ToString() + m_sColor_End;
        //}
        //else
        //{
        //    return m_sColor_Brown + sentence + " " + number.ToString() + m_sColor_End;
        //}
    }
    string Refine_Condition(string sentence, float number)
    {
        m_nPlayerEquipment_SetItemEffect_Current = 0;
        if (Player_Equipment.m_bEquipment_Hat == true)
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Hat.m_nItemCode) == m_ISE.m_nItemSetEffect_Code)
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_Hat() == true)
                    m_nPlayerEquipment_SetItemEffect_Current += 1;
            }
        }
        if (Player_Equipment.m_bEquipment_Top == true)
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Top.m_nItemCode) == m_ISE.m_nItemSetEffect_Code)
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_Top() == true)
                    m_nPlayerEquipment_SetItemEffect_Current += 1;
            }
        }
        if (Player_Equipment.m_bEquipment_Bottoms == true)
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Bottoms.m_nItemCode) == m_ISE.m_nItemSetEffect_Code)
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_Bottoms() == true)
                    m_nPlayerEquipment_SetItemEffect_Current += 1;
            }
        }
        if (Player_Equipment.m_bEquipment_Shose == true)
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Shose.m_nItemCode) == m_ISE.m_nItemSetEffect_Code)
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_Shose() == true)
                    m_nPlayerEquipment_SetItemEffect_Current += 1;
            }
        }
        if (Player_Equipment.m_bEquipment_Gloves == true)
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Gloves.m_nItemCode) == m_ISE.m_nItemSetEffect_Code)
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_Gloves() == true)
                    m_nPlayerEquipment_SetItemEffect_Current += 1;
            }
        }
        if (Player_Equipment.m_bEquipment_Mainweapon == true)
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Mainweapon.m_nItemCode) == m_ISE.m_nItemSetEffect_Code)
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_MainWeapon() == true)
                    m_nPlayerEquipment_SetItemEffect_Current += 1;
            }
        }
        if (Player_Equipment.m_bEquipment_Subweapon == true)
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Subweapon.m_nItemCode) == m_ISE.m_nItemSetEffect_Code)
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_SubWeapon() == true)
                    m_nPlayerEquipment_SetItemEffect_Current += 1;
            }
        }

        //if (m_nList_SetItemEffect_Code[m_nSetItemEffect_List_Index] <= m_nPlayerEquipment_SetItemEffect_Current)
        //{
        if (number > 0)
            return m_sColor_WhiteGray + sentence + " " + number.ToString() + m_sColor_End;
        else if (number < 0)
            return m_sColor_White + sentence + " " + number.ToString() + m_sColor_End;
        else
            return m_sColor_Brown + sentence + " " + number.ToString() + m_sColor_End;
        //}
        //else
        //{
        //    return m_sColor_Brown + sentence + " " + number.ToString() + m_sColor_End;
        //}
    }
    // 각각의 옵션.(유동적인 아이템 효과)
    string Refine_Condition(string sentence, int option, int option_additional, int option_reinforcement)
    {
        if (option != 0)
        {
            if (option_additional + option_reinforcement != 0)
            {
                if (option > 0)
                    return m_sColor_White + sentence + " " + option.ToString() + "(" + (option_additional + option_reinforcement).ToString() + ")" + m_sColor_End;
                else
                    return m_sColor_WhiteGray + sentence + " " + option.ToString() + "(" + (option_additional + option_reinforcement).ToString() + ")" + m_sColor_End;
            }
            else
            {
                if (option > 0)
                    return m_sColor_White + sentence + " " + option.ToString() + "(" + (option_additional + option_reinforcement).ToString() + ")" + m_sColor_End;
                else
                    return m_sColor_WhiteGray + sentence + " " + option.ToString() + "(" + (option_additional + option_reinforcement).ToString() + ")" + m_sColor_End;
            }
        }
        else
        {
            if (option_additional + option_reinforcement != 0)
            {
                return m_sColor_Brown + sentence + " " + option.ToString() + "(" + (option_additional + option_reinforcement).ToString() + ")" + m_sColor_End;
            }
            else
            {
                if (option_additional != 0 || option_reinforcement != 0)
                    return m_sColor_Brown + sentence + " " + option.ToString() + "(" + (option_additional + option_reinforcement).ToString() + ")" + m_sColor_End;
                else
                    return m_sColor_Brown + sentence + " " + option.ToString() + m_sColor_End;
            }
        }
    }
    string Refine_Condition(string sentence, float option, float option_additional, float option_reinforcement)
    {
        if (option != 0)
        {
            if (option_additional + option_reinforcement != 0)
            {
                if (option < 0)
                    return m_sColor_White + sentence + " " + option.ToString() + "(" + (option_additional + option_reinforcement).ToString() + ")" + m_sColor_End;
                else
                    return m_sColor_WhiteGray + sentence + " " + option.ToString() + "(" + (option_additional + option_reinforcement).ToString() + ")" + m_sColor_End;
            }
            else
            {
                if (option < 0)
                    return m_sColor_White + sentence + " " + option.ToString() + "(" + (option_additional + option_reinforcement).ToString() + ")" + m_sColor_End;
                else
                    return m_sColor_WhiteGray + sentence + " " + option.ToString() + "(" + (option_additional + option_reinforcement).ToString() + ")" + m_sColor_End;
            }
        }
        else
        {
            if (option_additional + option_reinforcement != 0)
            {
                return m_sColor_Brown + sentence + " " + option.ToString() + "(" + (option_additional + option_reinforcement).ToString() + ")" + m_sColor_End;
            }
            else
            {
                if (option_additional != 0 || option_reinforcement != 0)
                    return m_sColor_Brown + sentence + " " + option.ToString() + "(" + (option_additional + option_reinforcement).ToString() + ")" + m_sColor_End;
                else
                    return m_sColor_Brown + sentence + " " + option.ToString() + m_sColor_End;
            }
        }
    }
    // 각각의 옵션.(유동적인 아이템 효과)
    string Refine_Condition_Item_Use(string sentence, int option)
    {
        if (option != 0)
        {
            if (option > 0)
            {
                return m_sColor_White + sentence + " " + option.ToString() + m_sColor_End;
            }
            else
            {
                return m_sColor_WhiteGray + sentence + " " + option.ToString() + m_sColor_End;
            }
        }
        else
        {
            return m_sColor_Brown + sentence + " " + option.ToString() + m_sColor_End;
        }
    }
    string Refine_Condition_Item_Use(string sentence, float option)
    {
        if (option != 0)
        {
            if (option < 0)
            {
                return m_sColor_White + sentence + " " + option.ToString() + m_sColor_End;
            }
            else
            {
                return m_sColor_WhiteGray + sentence + " " + option.ToString() + m_sColor_End;
            }
        }
        else
        {
            return m_sColor_Brown + sentence + " " + option.ToString() + m_sColor_End;
        }
    }
}
