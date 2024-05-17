using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

public class GUI_Itemslot_Equip_Information : MonoBehaviour
{
    // Itemslot_Equip_Information 에서 확인 할 수 있는 장비 아이템 정보 UI.
    [SerializeField] public GameObject m_gPanel_Itemslot_Equip_Information;
    [Space(20)]

    [SerializeField] GameObject m_gPanel_Itemslot_Equip_Information_UpBar;
    [SerializeField] TextMeshProUGUI m_TMP_Itemslot_Equip_Information_UpBar;
    [SerializeField] Button m_BTN_Itemslot_Equip_Information_UpBar_Exit;
    [Space(20)]

    [SerializeField] GameObject m_gPanel_Itemslot_Equip_Information_Content;
    [Space(10)]

    [SerializeField] GameObject m_gPanel_Itemslot_Equip_Information_Content_Image;
    [SerializeField] GameObject m_gPanel_Itemslot_Equip_Information_Content_Image_ItemSprite;
    [SerializeField] Image m_IMG_Itemslot_Equip_Information_Content_Image_ItemSprite;
    [Space(10)]

    [SerializeField] GameObject m_gPanel_Itemslot_Equip_Information_Content_ItemInformation;
    [SerializeField] TextMeshProUGUI m_TMP_Itemslot_Equip_Information_Content_ItemInformation;
    [Space(10)]

    [SerializeField] GameObject m_gPanel_Itemslot_Equip_Information_Content_Effect;

    [SerializeField] GameObject m_gPanel_Itemslot_Equip_Information_Content_Effect_Name;
    [SerializeField] TextMeshProUGUI m_TMP_Itemslot_Equip_Information_Content_Effect_Name;
    [Space(5)]
    [SerializeField] GameObject m_gPanel_Itemslot_Equip_Information_Content_Effect_Status;
    [SerializeField] TextMeshProUGUI m_TMP_Itemslot_Equip_Information_Content_Effect_Status_L;
    [SerializeField] TextMeshProUGUI m_TMP_Itemslot_Equip_Information_Content_Effect_Status_R;
    [Space(5)]
    [SerializeField] GameObject m_gPanel_Itemslot_Equip_Information_Content_Effect_Soc;
    [SerializeField] TextMeshProUGUI m_TMP_Itemslot_Equip_Information_Content_Effect_Soc_L;
    [SerializeField] TextMeshProUGUI m_TMP_Itemslot_Equip_Information_Content_Effect_Soc_R;
    [Space(10)]

    [SerializeField] GameObject m_gPanel_Itemslot_Equip_Information_Content_Condition;

    [SerializeField] GameObject m_gPanel_Itemslot_Equip_Information_Content_Condition_Name;
    [SerializeField] TextMeshProUGUI m_TMP_Itemslot_Equip_Information_Content_Condition_Name;
    [Space(5)]
    [SerializeField] GameObject m_gPanel_Itemslot_Equip_Information_Content_Condition_Status;
    [SerializeField] TextMeshProUGUI m_TMP_Itemslot_Equip_Information_Content_Condition_Status_L;
    [SerializeField] TextMeshProUGUI m_TMP_Itemslot_Equip_Information_Content_Condition_Status_R;
    [Space(5)]
    [SerializeField] GameObject m_gPanel_Itemslot_Equip_Information_Content_Condition_Soc;
    [SerializeField] TextMeshProUGUI m_TMP_Itemslot_Equip_Information_Content_Condition_Soc_L;
    [SerializeField] TextMeshProUGUI m_TMP_Itemslot_Equip_Information_Content_Condition_Soc_R;
    [Space(10)]

    [SerializeField] GameObject m_gPanel_Itemslot_Equip_Information_Content_ItemDescription;

    [SerializeField] GameObject m_gPanel_Itemslot_Equip_Information_Content_ItemDescription_Name;
    [SerializeField] TextMeshProUGUI m_TMP_Itemslot_Equip_Information_Content_ItemDescription_Name;
    [Space(5)]
    [SerializeField] GameObject m_gPanel_Itemslot_Equip_Information_Content_ItemDescription_Content;
    [SerializeField] GameObject m_gSV_Itemslot_Equip_Information_Content_ItemDescription_Content;
    [SerializeField] GameObject m_gViewport_Itemslot_Equip_Information_Content_ItemDescription_Content;
    [SerializeField] TextMeshProUGUI m_TMP_Itemslot_Equip_Information_Content_ItemDescription_Content;
    [SerializeField] Scrollbar m_Scrollbar_Itemslot_Equip_Information_Content_ItemDescription_Content;
    [Space(10)]

    [SerializeField] Button m_BTN_Itemslot_Equip_Information_Content_ChangeInformation_R;
    [SerializeField] Button m_BTN_Itemslot_Equip_Information_Content_ChangeInformation_L;
    [Space(20)]

    [SerializeField] GameObject m_gPanel_Itemslot_Equip_Information_EquipPossibility;
    [SerializeField] Button m_BTN_Itemslot_Equip_Information_EquipPossibility;
    [SerializeField] TextMeshProUGUI m_TMP_Itemslot_Equip_Information_EquipPossibility;
    [Space(20)]

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
    [Space(20)]

    int m_nSetItemEffect_Count;
    int m_nSetItemEffect_Current;
    [SerializeField] List<int> m_nList_SetItemEffect_Code;
    int m_nSetItemEffect_List_Index;
    int m_nPlayerEquipment_SetItemEffect_Current;

    ItemSetEffect m_ISE;

    // 기능.
    public enum E_Itemslot_Equip_Information_PageNumber { EFFECT, CONDITION, DESCRIPTION }
    public E_Itemslot_Equip_Information_PageNumber m_eInformationPage;

    // Text 임시 저장소.
    string m_sBuffer;
    // Color.
    string m_sColor_White = "<color=#ffffff>";
    string m_sColor_WhiteGray = "<color=#808080>";
    //string m_sColor_Brown = "<color=#663333>";
    //string m_sColor_Brown = "<color=#753838>";
    //string m_sColor_Brown = "<color=#9c4b4b>";
    string m_sColor_Brown = "<color=#915446>";
    string m_sColor_Red = "<color=#FF0000>";
    string m_sColor_End = "</color>";

    // Text 정제 관련 변수.
    bool m_bRefine_Condition_Check;
    // 장비 착용 가능 여부.
    public bool m_bEquip_Condition_Check;

    // Player 정보.
    Player_Status player;

    public void InitialSet()
    {
        InitialSet_Object();
        InitialSet_Button();
        m_eInformationPage = E_Itemslot_Equip_Information_PageNumber.DESCRIPTION;
        player = Player_Total.Instance.m_ps_Status;
    }

    // 초기 Object 불러오기.
    void InitialSet_Object()
    {
        m_gPanel_Itemslot_Equip_Information = GameObject.Find("Canvas_GUI").transform.Find("Panel_Itemslot_Equip_Information").gameObject;


        m_gPanel_Itemslot_Equip_Information_UpBar = m_gPanel_Itemslot_Equip_Information.transform.Find("Panel_Itemslot_Equip_Information_UpBar").gameObject;
        m_TMP_Itemslot_Equip_Information_UpBar = m_gPanel_Itemslot_Equip_Information_UpBar.transform.Find("TMP_Itemslot_Equip_Information_UpBar").gameObject.GetComponent<TextMeshProUGUI>();
        m_BTN_Itemslot_Equip_Information_UpBar_Exit = m_gPanel_Itemslot_Equip_Information_UpBar.transform.Find("BTN_Itemslot_Equip_Information_UpBar_Exit").gameObject.GetComponent<Button>();


        m_gPanel_Itemslot_Equip_Information_Content = m_gPanel_Itemslot_Equip_Information.transform.Find("Panel_Itemslot_Equip_Information_Content").gameObject;


        m_gPanel_Itemslot_Equip_Information_Content_Image = m_gPanel_Itemslot_Equip_Information_Content.transform.Find("Panel_Itemslot_Equip_Information_Content_Image").gameObject;
        m_gPanel_Itemslot_Equip_Information_Content_Image_ItemSprite = m_gPanel_Itemslot_Equip_Information_Content_Image.transform.Find("Panel_Itemslot_Equip_Information_Content_Image_ItemSprite").gameObject;
        m_IMG_Itemslot_Equip_Information_Content_Image_ItemSprite = m_gPanel_Itemslot_Equip_Information_Content_Image_ItemSprite.GetComponent<Image>();


        m_gPanel_Itemslot_Equip_Information_Content_ItemInformation = m_gPanel_Itemslot_Equip_Information_Content.transform.Find("Panel_Itemslot_Equip_Information_Content_ItemInformation").gameObject;
        m_TMP_Itemslot_Equip_Information_Content_ItemInformation = m_gPanel_Itemslot_Equip_Information_Content_ItemInformation.transform.Find("TMP_Itemslot_Equip_Information_Content_ItemInformation").gameObject.GetComponent<TextMeshProUGUI>();


        m_gPanel_Itemslot_Equip_Information_Content_Effect = m_gPanel_Itemslot_Equip_Information_Content.transform.Find("Panel_Itemslot_Equip_Information_Content_Effect").gameObject;

        m_gPanel_Itemslot_Equip_Information_Content_Effect_Name = m_gPanel_Itemslot_Equip_Information_Content_Effect.transform.Find("Panel_Itemslot_Equip_Information_Content_Effect_Name").gameObject;
        m_TMP_Itemslot_Equip_Information_Content_Effect_Name = m_gPanel_Itemslot_Equip_Information_Content_Effect_Name.transform.Find("TMP_Itemslot_Equip_Information_Content_Effect_Name").gameObject.GetComponent<TextMeshProUGUI>();

        m_gPanel_Itemslot_Equip_Information_Content_Effect_Status = m_gPanel_Itemslot_Equip_Information_Content_Effect.transform.Find("Panel_Itemslot_Equip_Information_Content_Effect_Status").gameObject;
        m_TMP_Itemslot_Equip_Information_Content_Effect_Status_L = m_gPanel_Itemslot_Equip_Information_Content_Effect_Status.transform.Find("TMP_Itemslot_Equip_Information_Content_Effect_Status_L").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_Itemslot_Equip_Information_Content_Effect_Status_R = m_gPanel_Itemslot_Equip_Information_Content_Effect_Status.transform.Find("TMP_Itemslot_Equip_Information_Content_Effect_Status_R").gameObject.GetComponent<TextMeshProUGUI>();

        m_gPanel_Itemslot_Equip_Information_Content_Effect_Soc = m_gPanel_Itemslot_Equip_Information_Content_Effect.transform.Find("Panel_Itemslot_Equip_Information_Content_Effect_Soc").gameObject;
        m_TMP_Itemslot_Equip_Information_Content_Effect_Soc_L = m_gPanel_Itemslot_Equip_Information_Content_Effect_Soc.transform.Find("TMP_Itemslot_Equip_Information_Content_Effect_Soc_L").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_Itemslot_Equip_Information_Content_Effect_Soc_R = m_gPanel_Itemslot_Equip_Information_Content_Effect_Soc.transform.Find("TMP_Itemslot_Equip_Information_Content_Effect_Soc_R").gameObject.GetComponent<TextMeshProUGUI>();


        m_gPanel_Itemslot_Equip_Information_Content_Condition = m_gPanel_Itemslot_Equip_Information_Content.transform.Find("Panel_Itemslot_Equip_Information_Content_Condition").gameObject;

        m_gPanel_Itemslot_Equip_Information_Content_Condition_Name = m_gPanel_Itemslot_Equip_Information_Content_Condition.transform.Find("Panel_Itemslot_Equip_Information_Content_Condition_Name").gameObject;
        m_TMP_Itemslot_Equip_Information_Content_Condition_Name = m_gPanel_Itemslot_Equip_Information_Content_Condition_Name.transform.Find("TMP_Itemslot_Equip_Information_Content_Condition_Name").gameObject.GetComponent<TextMeshProUGUI>();

        m_gPanel_Itemslot_Equip_Information_Content_Condition_Status = m_gPanel_Itemslot_Equip_Information_Content_Condition.transform.Find("Panel_Itemslot_Equip_Information_Content_Condition_Status").gameObject;
        m_TMP_Itemslot_Equip_Information_Content_Condition_Status_L = m_gPanel_Itemslot_Equip_Information_Content_Condition_Status.transform.Find("TMP_Itemslot_Equip_Information_Content_Condition_Status_L").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_Itemslot_Equip_Information_Content_Condition_Status_R = m_gPanel_Itemslot_Equip_Information_Content_Condition_Status.transform.Find("TMP_Itemslot_Equip_Information_Content_Condition_Status_R").gameObject.GetComponent<TextMeshProUGUI>();

        m_gPanel_Itemslot_Equip_Information_Content_Condition_Soc = m_gPanel_Itemslot_Equip_Information_Content_Condition.transform.Find("Panel_Itemslot_Equip_Information_Content_Condition_Soc").gameObject;
        m_TMP_Itemslot_Equip_Information_Content_Condition_Soc_L = m_gPanel_Itemslot_Equip_Information_Content_Condition_Soc.transform.Find("TMP_Itemslot_Equip_Information_Content_Condition_Soc_L").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_Itemslot_Equip_Information_Content_Condition_Soc_R = m_gPanel_Itemslot_Equip_Information_Content_Condition_Soc.transform.Find("TMP_Itemslot_Equip_Information_Content_Condition_Soc_R").gameObject.GetComponent<TextMeshProUGUI>();


        m_gPanel_Itemslot_Equip_Information_Content_ItemDescription = m_gPanel_Itemslot_Equip_Information_Content.transform.Find("Panel_Itemslot_Equip_Information_Content_ItemDescription").gameObject;

        m_gPanel_Itemslot_Equip_Information_Content_ItemDescription_Name = m_gPanel_Itemslot_Equip_Information_Content_ItemDescription.transform.Find("Panel_Itemslot_Equip_Information_Content_ItemDescription_Name").gameObject;
        m_TMP_Itemslot_Equip_Information_Content_ItemDescription_Name = m_gPanel_Itemslot_Equip_Information_Content_ItemDescription_Name.transform.Find("TMP_Itemslot_Equip_Information_Content_ItemDescription_Name").gameObject.GetComponent<TextMeshProUGUI>();

        m_gPanel_Itemslot_Equip_Information_Content_ItemDescription_Content = m_gPanel_Itemslot_Equip_Information_Content_ItemDescription.transform.Find("Panel_Itemslot_Equip_Information_Content_ItemDescription_Content").gameObject;
        m_gSV_Itemslot_Equip_Information_Content_ItemDescription_Content = m_gPanel_Itemslot_Equip_Information_Content_ItemDescription_Content.transform.Find("SV_Itemslot_Equip_Information_Content_ItemDescription_Content").gameObject;
        m_gViewport_Itemslot_Equip_Information_Content_ItemDescription_Content = m_gSV_Itemslot_Equip_Information_Content_ItemDescription_Content.transform.Find("Viewport_Itemslot_Equip_Information_Content_ItemDescription_Content").gameObject;
        m_TMP_Itemslot_Equip_Information_Content_ItemDescription_Content = m_gViewport_Itemslot_Equip_Information_Content_ItemDescription_Content.transform.Find("TMP_Itemslot_Equip_Information_Content_ItemDescription_Content").gameObject.GetComponent<TextMeshProUGUI>();
        m_Scrollbar_Itemslot_Equip_Information_Content_ItemDescription_Content = m_gSV_Itemslot_Equip_Information_Content_ItemDescription_Content.transform.Find("Scrollbar_Itemslot_Equip_Information_Content_ItemDescription_Content").gameObject.GetComponent<Scrollbar>();

        m_BTN_Itemslot_Equip_Information_Content_ChangeInformation_R = m_gPanel_Itemslot_Equip_Information_Content.transform.Find("BTN_Itemslot_Equip_Information_Content_ChangeInformation_R").gameObject.GetComponent<Button>();
        m_BTN_Itemslot_Equip_Information_Content_ChangeInformation_L = m_gPanel_Itemslot_Equip_Information_Content.transform.Find("BTN_Itemslot_Equip_Information_Content_ChangeInformation_L").gameObject.GetComponent<Button>();


        m_gPanel_Itemslot_Equip_Information_EquipPossibility = m_gPanel_Itemslot_Equip_Information.transform.Find("Panel_Itemslot_Equip_Information_EquipPossibility").gameObject;
        m_BTN_Itemslot_Equip_Information_EquipPossibility = m_gPanel_Itemslot_Equip_Information_EquipPossibility.transform.Find("BTN_Itemslot_Equip_Information_EquipPossibility").gameObject.GetComponent<Button>();
        m_TMP_Itemslot_Equip_Information_EquipPossibility = m_BTN_Itemslot_Equip_Information_EquipPossibility.transform.Find("TMP_Itemslot_Equip_Information_EquipPossibility").gameObject.GetComponent<TextMeshProUGUI>();



        m_gPanel_SetItemEffect = m_gPanel_Itemslot_Equip_Information.transform.Find("Panel_SetItemEffect").gameObject;
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
    }
    // 초기 Button 이벤트 설정.
    void InitialSet_Button()
    {
        m_BTN_Itemslot_Equip_Information_UpBar_Exit.onClick.RemoveAllListeners();
        m_BTN_Itemslot_Equip_Information_UpBar_Exit.onClick.AddListener(delegate { Set_BTN_Exit(); });
        m_BTN_Itemslot_Equip_Information_Content_ChangeInformation_R.onClick.RemoveAllListeners();
        m_BTN_Itemslot_Equip_Information_Content_ChangeInformation_R.onClick.AddListener(delegate { Set_BTN_ChangeInformationPageNumber_R(); });
        m_BTN_Itemslot_Equip_Information_Content_ChangeInformation_L.onClick.RemoveAllListeners();
        m_BTN_Itemslot_Equip_Information_Content_ChangeInformation_L.onClick.AddListener(delegate { Set_BTN_ChangeInformationPageNumber_L(); });
    }

    // Equip Button 이벤트 설정.
    // 장비 아이템 설명 창 페이지 변경.
    void Set_BTN_Exit()
    {
        m_gPanel_Itemslot_Equip_Information.SetActive(false);
    }
    void Set_BTN_ChangeInformationPageNumber_R()
    {
        switch (m_eInformationPage)
        {
            case E_Itemslot_Equip_Information_PageNumber.EFFECT:
                {
                    m_eInformationPage = E_Itemslot_Equip_Information_PageNumber.CONDITION;
                    m_gPanel_Itemslot_Equip_Information_Content_Effect.SetActive(false);
                    m_gPanel_Itemslot_Equip_Information_Content_Condition.SetActive(true);
                    m_gPanel_Itemslot_Equip_Information_Content_ItemDescription.SetActive(false);
                } break;
            case E_Itemslot_Equip_Information_PageNumber.CONDITION:
                {
                    m_eInformationPage = E_Itemslot_Equip_Information_PageNumber.DESCRIPTION;
                    m_gPanel_Itemslot_Equip_Information_Content_Effect.SetActive(false);
                    m_gPanel_Itemslot_Equip_Information_Content_Condition.SetActive(false);
                    m_gPanel_Itemslot_Equip_Information_Content_ItemDescription.SetActive(true);
                    m_Scrollbar_Itemslot_Equip_Information_Content_ItemDescription_Content.value = 1;
                    m_Scrollbar_SetItemEffect_Content_SS_Description.value = 1;
                }
                break;
            case E_Itemslot_Equip_Information_PageNumber.DESCRIPTION:
                {
                    m_eInformationPage = E_Itemslot_Equip_Information_PageNumber.EFFECT;
                    m_gPanel_Itemslot_Equip_Information_Content_Effect.SetActive(true);
                    m_gPanel_Itemslot_Equip_Information_Content_Condition.SetActive(false);
                    m_gPanel_Itemslot_Equip_Information_Content_ItemDescription.SetActive(false);
                }
                break;
        }
    }
    void Set_BTN_ChangeInformationPageNumber_L()
    {
        switch (m_eInformationPage)
        {
            case E_Itemslot_Equip_Information_PageNumber.EFFECT:
                {
                    m_eInformationPage = E_Itemslot_Equip_Information_PageNumber.DESCRIPTION;
                    m_gPanel_Itemslot_Equip_Information_Content_Effect.SetActive(false);
                    m_gPanel_Itemslot_Equip_Information_Content_Condition.SetActive(false);
                    m_gPanel_Itemslot_Equip_Information_Content_ItemDescription.SetActive(true);
                    m_Scrollbar_Itemslot_Equip_Information_Content_ItemDescription_Content.value = 1;
                    m_Scrollbar_SetItemEffect_Content_SS_Description.value = 1;
                }
                break;
            case E_Itemslot_Equip_Information_PageNumber.CONDITION:
                {
                    m_eInformationPage = E_Itemslot_Equip_Information_PageNumber.EFFECT;
                    m_gPanel_Itemslot_Equip_Information_Content_Effect.SetActive(true);
                    m_gPanel_Itemslot_Equip_Information_Content_Condition.SetActive(false);
                    m_gPanel_Itemslot_Equip_Information_Content_ItemDescription.SetActive(false);
                }
                break;
            case E_Itemslot_Equip_Information_PageNumber.DESCRIPTION:
                {
                    m_eInformationPage = E_Itemslot_Equip_Information_PageNumber.CONDITION;
                    m_gPanel_Itemslot_Equip_Information_Content_Effect.SetActive(false);
                    m_gPanel_Itemslot_Equip_Information_Content_Condition.SetActive(true);
                    m_gPanel_Itemslot_Equip_Information_Content_ItemDescription.SetActive(false);
                   
                }
                break;
        }
    }
    // 장비 착용가능.
    void Set_BTN_EquipPossibility_Possible(int arynumber, Item item)
    {
        if (Player_Total.Instance.m_pm_Move.m_ePlayerMoveState != Player_Move.E_PLAYER_MOVE_STATE.DEATH)
        {
            Item_Equip m_gEquipslotItem = new Item_Equip();
            bool m_bEquip = false;
            if (Player_Itemslot.m_gary_Itemslot_Equip[arynumber].m_eItemEquipType == E_ITEM_EQUIP_TYPE.HAT)
            {
                if (Player_Equipment.m_bEquipment_Hat == true)
                {
                    m_gEquipslotItem = Player_Equipment.m_gEquipment_Hat;
                    m_bEquip = true;
                }
            }
            if (Player_Itemslot.m_gary_Itemslot_Equip[arynumber].m_eItemEquipType == E_ITEM_EQUIP_TYPE.TOP)
            {
                if (Player_Equipment.m_bEquipment_Top == true)
                {
                    m_gEquipslotItem = Player_Equipment.m_gEquipment_Top;
                    m_bEquip = true;
                }
            }
            if (Player_Itemslot.m_gary_Itemslot_Equip[arynumber].m_eItemEquipType == E_ITEM_EQUIP_TYPE.BOTTOMS)
            {
                if (Player_Equipment.m_bEquipment_Bottoms == true)
                {
                    m_gEquipslotItem = Player_Equipment.m_gEquipment_Bottoms;
                    m_bEquip = true;
                }
            }
            if (Player_Itemslot.m_gary_Itemslot_Equip[arynumber].m_eItemEquipType == E_ITEM_EQUIP_TYPE.SHOSE)
            {
                if (Player_Equipment.m_bEquipment_Shose == true)
                {
                    m_gEquipslotItem = Player_Equipment.m_gEquipment_Shose;
                    m_bEquip = true;
                }
            }
            if (Player_Itemslot.m_gary_Itemslot_Equip[arynumber].m_eItemEquipType == E_ITEM_EQUIP_TYPE.GLOVES)
            {
                if (Player_Equipment.m_bEquipment_Gloves == true)
                {
                    m_gEquipslotItem = Player_Equipment.m_gEquipment_Gloves;
                    m_bEquip = true;
                }
            }
            if (Player_Itemslot.m_gary_Itemslot_Equip[arynumber].m_eItemEquipType == E_ITEM_EQUIP_TYPE.MAINWEAPON)
            {
                if (Player_Equipment.m_bEquipment_Mainweapon == true)
                {
                    m_gEquipslotItem = Player_Equipment.m_gEquipment_Mainweapon;
                    m_bEquip = true;
                }
            }
            if (Player_Itemslot.m_gary_Itemslot_Equip[arynumber].m_eItemEquipType == E_ITEM_EQUIP_TYPE.SUBWEAPON)
            {
                if (Player_Equipment.m_bEquipment_Subweapon == true)
                {
                    m_gEquipslotItem = Player_Equipment.m_gEquipment_Subweapon;
                    m_bEquip = true;
                }
            }

            //착용 조건 체크
            if (Player_Total.Instance.CheckCondition_Item_Equip(Player_Itemslot.m_gary_Itemslot_Equip[arynumber], Player_Total.Instance.m_ps_Status.m_sStatus, Player_Total.Instance.m_ps_Status.m_sSoc) == true)
            {
                Player_Itemslot.m_nary_Itemslot_Equip_Count[arynumber] = 0;
                Player_Itemslot.m_gary_Itemslot_Equip[arynumber] = null;

                if (m_bEquip == true)
                {
                    Player_Total.Instance.m_pi_Itemslot.Get_Item_Equip(m_gEquipslotItem);
                }
            }

            m_gPanel_Itemslot_Equip_Information.SetActive(false);
            m_BTN_Itemslot_Equip_Information_EquipPossibility.GetComponent<Button>().onClick.RemoveAllListeners();

            GUIManager_Total.Instance.Update_Itemslot();
            Player_Total.Instance.m_pq_Quest.QuestUpdate_Collect(item);
            GUIManager_Total.Instance.Update_Quickslot_Equip(arynumber);

            Player_Total.Instance.m_ps_Status.CheckLogic();
        }
    }
    // 장비 착용불가능.
    void Set_BTN_EquipPossibility_ImPossible()
    {

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

    // 장비 아이템 설명 창 세부 설정.
    public void UpdateItemEquipInformation(Item_Equip item, int arynumber)
    {
        m_Scrollbar_Itemslot_Equip_Information_Content_ItemDescription_Content.value = 1;
        m_Scrollbar_SetItemEffect_Content_SS_Description.value = 1;
        UpdateItemEquipInformation_UpBar(item);
        UpdateItemEquipInformation_Content(item);
        UpdateItemEquipInformation_EquipPossibility(item, arynumber);
        UpdateItemEquipInformation_SetItemEffect(item);
    }
    // 착용 및 사용 금지.
    public void UseBan()
    {
        m_BTN_Itemslot_Equip_Information_EquipPossibility.onClick.RemoveAllListeners();
    }
    // 스크롤바 초기화.
    public void Init_Scrollbar()
    {
        m_Scrollbar_Itemslot_Equip_Information_Content_ItemDescription_Content.value = 1;
    }
    void UpdateItemEquipInformation_UpBar(Item_Equip item)
    {
        // 장비 아이템 이름, 강화 상태.
        m_TMP_Itemslot_Equip_Information_UpBar.text = item.m_sItemName;
        if (item.m_nReinforcementCount_Current > 0)
            m_TMP_Itemslot_Equip_Information_UpBar.text += " + " + item.m_nReinforcementCount_Current;
    }
    void UpdateItemEquipInformation_Content(Item_Equip item)
    {
        // 장비 아이템 이미지.
        m_IMG_Itemslot_Equip_Information_Content_Image_ItemSprite.sprite = item.m_sp_Sprite;
        m_IMG_Itemslot_Equip_Information_Content_Image_ItemSprite.color = new Color(1, 1, 1, 0.75f);

        // 장비 아이템 분류, 등급, 추가 옵션 등급, 강화 상태.
        // 분류.
        m_TMP_Itemslot_Equip_Information_Content_ItemInformation.text = "";
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
        m_TMP_Itemslot_Equip_Information_Content_ItemInformation.text += "분류: " + m_sBuffer + "\n";
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
        m_TMP_Itemslot_Equip_Information_Content_ItemInformation.text += "등급: " + m_sBuffer + "\n";
        // 추가 옵션 등급.
        m_TMP_Itemslot_Equip_Information_Content_ItemInformation.text += "추가 옵션 등급: " + item.m_eItemEquip_SpecialRatio_STATUS + " / " + item.m_eItemEquip_SpecialRatio_SOC + "\n";
        // 강화 상태.
        m_TMP_Itemslot_Equip_Information_Content_ItemInformation.text += "강화 상태: " + item.m_nReinforcementCount_Current + " / " + item.m_nReinforcementCount_Max;

        // 장비 아이템 효과.
        // 장비 아이템 효과 Status.
        m_TMP_Itemslot_Equip_Information_Content_Effect_Status_L.text = "";
        m_TMP_Itemslot_Equip_Information_Content_Effect_Status_L.text += Refine_Condition("레        벨:", item.m_sStatus_Effect.GetSTATUS_LV(), item.m_STATUS_AdditionalOption.GetSTATUS_LV(), item.m_STATUS_ReinforcementValue.GetSTATUS_LV());
        m_TMP_Itemslot_Equip_Information_Content_Effect_Status_L.text += Refine_Condition("\n체        력:", item.m_sStatus_Effect.GetSTATUS_HP_Max(), item.m_STATUS_AdditionalOption.GetSTATUS_HP_Max(), item.m_STATUS_ReinforcementValue.GetSTATUS_HP_Max());
        m_TMP_Itemslot_Equip_Information_Content_Effect_Status_L.text += Refine_Condition("\n데  미  지:", item.m_sStatus_Effect.GetSTATUS_Damage_Total(), item.m_STATUS_AdditionalOption.GetSTATUS_Damage_Total(), item.m_STATUS_ReinforcementValue.GetSTATUS_Damage_Total());
        m_TMP_Itemslot_Equip_Information_Content_Effect_Status_L.text += Refine_Condition("\n이동속도:", item.m_sStatus_Effect.GetSTATUS_Speed(), item.m_STATUS_AdditionalOption.GetSTATUS_Speed(), item.m_STATUS_ReinforcementValue.GetSTATUS_Speed());
        m_TMP_Itemslot_Equip_Information_Content_Effect_Status_R.text = "";
        m_TMP_Itemslot_Equip_Information_Content_Effect_Status_R.text += "\n";
        m_TMP_Itemslot_Equip_Information_Content_Effect_Status_R.text += Refine_Condition("마        나:", item.m_sStatus_Effect.GetSTATUS_MP_Max(), item.m_STATUS_AdditionalOption.GetSTATUS_MP_Max(), item.m_STATUS_ReinforcementValue.GetSTATUS_MP_Max());
        m_TMP_Itemslot_Equip_Information_Content_Effect_Status_R.text += Refine_Condition("\n방  어  력:", item.m_sStatus_Effect.GetSTATUS_Defence_Physical(), item.m_STATUS_AdditionalOption.GetSTATUS_Defence_Physical(), item.m_STATUS_ReinforcementValue.GetSTATUS_Defence_Physical());
        m_TMP_Itemslot_Equip_Information_Content_Effect_Status_R.text += Refine_Condition("\n공격속도:", item.m_sStatus_Effect.GetSTATUS_AttackSpeed(), item.m_STATUS_AdditionalOption.GetSTATUS_AttackSpeed(), item.m_STATUS_ReinforcementValue.GetSTATUS_AttackSpeed());
        // 장비 아이템 효과 Soc.
        m_TMP_Itemslot_Equip_Information_Content_Effect_Soc_L.text = "";
        m_TMP_Itemslot_Equip_Information_Content_Effect_Soc_L.text += Refine_Condition("명        예:", item.m_sSoc_Effect.GetSOC_Honor(), item.m_SOC_AdditionalOption.GetSOC_Honor(), item.m_SOC_ReinforcementValue.GetSOC_Honor());
        m_TMP_Itemslot_Equip_Information_Content_Effect_Soc_L.text += Refine_Condition("\n인        간:", item.m_sSoc_Effect.GetSOC_Human(), item.m_SOC_AdditionalOption.GetSOC_Human(), item.m_SOC_ReinforcementValue.GetSOC_Human());
        m_TMP_Itemslot_Equip_Information_Content_Effect_Soc_L.text += Refine_Condition("\n동        물:", item.m_sSoc_Effect.GetSOC_Animal(), item.m_SOC_AdditionalOption.GetSOC_Animal(), item.m_SOC_ReinforcementValue.GetSOC_Animal());
        m_TMP_Itemslot_Equip_Information_Content_Effect_Soc_L.text += Refine_Condition("\n슬  라  임:", item.m_sSoc_Effect.GetSOC_Slime(), item.m_SOC_AdditionalOption.GetSOC_Slime(), item.m_SOC_ReinforcementValue.GetSOC_Slime());
        m_TMP_Itemslot_Equip_Information_Content_Effect_Soc_L.text += Refine_Condition("\n스켈레톤:", item.m_sSoc_Effect.GetSOC_Skeleton(), item.m_SOC_AdditionalOption.GetSOC_Skeleton(), item.m_SOC_ReinforcementValue.GetSOC_Skeleton());
        m_TMP_Itemslot_Equip_Information_Content_Effect_Soc_R.text = "";
        m_TMP_Itemslot_Equip_Information_Content_Effect_Soc_R.text += "\n";
        m_TMP_Itemslot_Equip_Information_Content_Effect_Soc_R.text += Refine_Condition("앤        트:", item.m_sSoc_Effect.GetSOC_Ents(), item.m_SOC_AdditionalOption.GetSOC_Ents(), item.m_SOC_ReinforcementValue.GetSOC_Ents());
        m_TMP_Itemslot_Equip_Information_Content_Effect_Soc_R.text += Refine_Condition("\n마        족:", item.m_sSoc_Effect.GetSOC_Devil(), item.m_SOC_AdditionalOption.GetSOC_Devil(), item.m_SOC_ReinforcementValue.GetSOC_Devil());
        m_TMP_Itemslot_Equip_Information_Content_Effect_Soc_R.text += Refine_Condition("\n용        족:", item.m_sSoc_Effect.GetSOC_Dragon(), item.m_SOC_AdditionalOption.GetSOC_Dragon(), item.m_SOC_ReinforcementValue.GetSOC_Dragon());
        m_TMP_Itemslot_Equip_Information_Content_Effect_Soc_R.text += Refine_Condition("\n어        둠:", item.m_sSoc_Effect.GetSOC_Shadow(), item.m_SOC_AdditionalOption.GetSOC_Shadow(), item.m_SOC_ReinforcementValue.GetSOC_Shadow());

        // 장비 아이템 착용 조건.
        // 장비 아이템 착용 조건 Status.
        m_TMP_Itemslot_Equip_Information_Content_Condition_Status_L.text = "";
        //m_TMP_Itemslot_Equip_Information_Content_Condition_Status_L.text += "레        벨: " + Refine_Condition(item.m_sStatus_Limit_Min.GetSTATUS_LV(), item.m_sStatus_Limit_Max.GetSTATUS_LV()) + "\n";
        m_TMP_Itemslot_Equip_Information_Content_Condition_Status_L.text += Refine_Condition("레        벨: ", "\n", item.m_sStatus_Limit_Min.GetSTATUS_LV(), item.m_sStatus_Limit_Max.GetSTATUS_LV(), player.m_sStatus.GetSTATUS_LV());
        //m_TMP_Itemslot_Equip_Information_Content_Condition_Status_L.text += "체        력: " + Refine_Condition(item.m_sStatus_Limit_Min.GetSTATUS_HP_Max(), item.m_sStatus_Limit_Max.GetSTATUS_HP_Max()) + "\n";
        m_TMP_Itemslot_Equip_Information_Content_Condition_Status_L.text += Refine_Condition("체        력: ", "\n", item.m_sStatus_Limit_Min.GetSTATUS_HP_Max(), item.m_sStatus_Limit_Max.GetSTATUS_HP_Max(), player.m_sStatus.GetSTATUS_HP_Max());
        //m_TMP_Itemslot_Equip_Information_Content_Condition_Status_L.text += "데  미  지: " + Refine_Condition(item.m_sStatus_Limit_Min.GetSTATUS_Damage_Total(), item.m_sStatus_Limit_Max.GetSTATUS_Damage_Total()) + "\n";
        m_TMP_Itemslot_Equip_Information_Content_Condition_Status_L.text += Refine_Condition("데  미  지: ", "\n", item.m_sStatus_Limit_Min.GetSTATUS_Damage_Total(), item.m_sStatus_Limit_Max.GetSTATUS_Damage_Total(), player.m_sStatus.GetSTATUS_Damage_Total());
        //m_TMP_Itemslot_Equip_Information_Content_Condition_Status_L.text += "이동속도: " + Refine_Condition(item.m_sStatus_Limit_Min.GetSTATUS_Speed(), item.m_sStatus_Limit_Max.GetSTATUS_Speed());
        m_TMP_Itemslot_Equip_Information_Content_Condition_Status_L.text += Refine_Condition("이동속도: ", "", item.m_sStatus_Limit_Min.GetSTATUS_Speed(), item.m_sStatus_Limit_Max.GetSTATUS_Speed(), player.m_sStatus.GetSTATUS_Speed());
        m_TMP_Itemslot_Equip_Information_Content_Condition_Status_R.text = "";
        m_TMP_Itemslot_Equip_Information_Content_Condition_Status_R.text += "\n";
        //m_TMP_Itemslot_Equip_Information_Content_Condition_Status_R.text += "마        나: " + Refine_Condition(item.m_sStatus_Limit_Min.GetSTATUS_MP_Max(), item.m_sStatus_Limit_Max.GetSTATUS_MP_Max()) + "\n";
        m_TMP_Itemslot_Equip_Information_Content_Condition_Status_R.text += Refine_Condition("마        나: ", "\n", item.m_sStatus_Limit_Min.GetSTATUS_MP_Max(), item.m_sStatus_Limit_Max.GetSTATUS_MP_Max(), player.m_sStatus.GetSTATUS_MP_Max());
        //m_TMP_Itemslot_Equip_Information_Content_Condition_Status_R.text += "방  어  력: " + Refine_Condition(item.m_sStatus_Limit_Min.GetSTATUS_Defence_Physical(), item.m_sStatus_Limit_Max.GetSTATUS_Defence_Physical()) + "\n";
        m_TMP_Itemslot_Equip_Information_Content_Condition_Status_R.text += Refine_Condition("방  어  력: ", "\n", item.m_sStatus_Limit_Min.GetSTATUS_Defence_Physical(), item.m_sStatus_Limit_Max.GetSTATUS_Defence_Physical(), player.m_sStatus.GetSTATUS_Defence_Physical());
        //m_TMP_Itemslot_Equip_Information_Content_Condition_Status_R.text += "공격속도: " + Refine_Condition(item.m_sStatus_Limit_Min.GetSTATUS_AttackSpeed(), item.m_sStatus_Limit_Max.GetSTATUS_AttackSpeed());
        m_TMP_Itemslot_Equip_Information_Content_Condition_Status_R.text += Refine_Condition("공격속도: ", "", item.m_sStatus_Limit_Min.GetSTATUS_AttackSpeed(), item.m_sStatus_Limit_Max.GetSTATUS_AttackSpeed(), player.m_sStatus.GetSTATUS_AttackSpeed());

        // 장비 아이템 조건 Soc.
        m_TMP_Itemslot_Equip_Information_Content_Condition_Soc_L.text = "";
        //m_TMP_Itemslot_Equip_Information_Content_Condition_Soc_L.text += "평        판: " + Refine_Condition(item.m_sSoc_Limit_Min.GetSOC_Honor(), item.m_sSoc_Limit_Max.GetSOC_Honor()) + "\n";
        m_TMP_Itemslot_Equip_Information_Content_Condition_Soc_L.text += Refine_Condition("명        예: ", "\n", item.m_sSoc_Limit_Min.GetSOC_Honor(), item.m_sSoc_Limit_Max.GetSOC_Honor(), player.m_sSoc.GetSOC_Honor());
        //m_TMP_Itemslot_Equip_Information_Content_Condition_Soc_L.text += "인        간: " + Refine_Condition(item.m_sSoc_Limit_Min.GetSOC_Human(), item.m_sSoc_Limit_Max.GetSOC_Human()) + "\n";
        m_TMP_Itemslot_Equip_Information_Content_Condition_Soc_L.text += Refine_Condition("인        간: ", "\n", item.m_sSoc_Limit_Min.GetSOC_Human(), item.m_sSoc_Limit_Max.GetSOC_Human(), player.m_sSoc.GetSOC_Human());
        //m_TMP_Itemslot_Equip_Information_Content_Condition_Soc_L.text += "동        물: " + Refine_Condition(item.m_sSoc_Limit_Min.GetSOC_Animal(), item.m_sSoc_Limit_Max.GetSOC_Animal()) + "\n";
        m_TMP_Itemslot_Equip_Information_Content_Condition_Soc_L.text += Refine_Condition("동        물: ", "\n", item.m_sSoc_Limit_Min.GetSOC_Animal(), item.m_sSoc_Limit_Max.GetSOC_Animal(), player.m_sSoc.GetSOC_Animal());
        //m_TMP_Itemslot_Equip_Information_Content_Condition_Soc_L.text += "슬  라  임: " + Refine_Condition(item.m_sSoc_Limit_Min.GetSOC_Slime(), item.m_sSoc_Limit_Max.GetSOC_Slime()) + "\n";
        m_TMP_Itemslot_Equip_Information_Content_Condition_Soc_L.text += Refine_Condition("슬  라  임: ", "\n", item.m_sSoc_Limit_Min.GetSOC_Slime(), item.m_sSoc_Limit_Max.GetSOC_Slime(), player.m_sSoc.GetSOC_Slime());
        //m_TMP_Itemslot_Equip_Information_Content_Condition_Soc_L.text += "스켈레톤: " + Refine_Condition(item.m_sSoc_Limit_Min.GetSOC_Skeleton(), item.m_sSoc_Limit_Max.GetSOC_Skeleton());
        m_TMP_Itemslot_Equip_Information_Content_Condition_Soc_L.text += Refine_Condition("스켈레톤: ", "", item.m_sSoc_Limit_Min.GetSOC_Skeleton(), item.m_sSoc_Limit_Max.GetSOC_Skeleton(), player.m_sSoc.GetSOC_Skeleton());
        m_TMP_Itemslot_Equip_Information_Content_Condition_Soc_R.text = "";
        m_TMP_Itemslot_Equip_Information_Content_Condition_Soc_R.text += "\n";
        //m_TMP_Itemslot_Equip_Information_Content_Condition_Soc_R.text += "앤        트: " + Refine_Condition(item.m_sSoc_Limit_Min.GetSOC_Ents(), item.m_sSoc_Limit_Max.GetSOC_Ents()) + "\n";
        m_TMP_Itemslot_Equip_Information_Content_Condition_Soc_R.text += Refine_Condition("앤        트: ", "\n", item.m_sSoc_Limit_Min.GetSOC_Ents(), item.m_sSoc_Limit_Max.GetSOC_Ents(), player.m_sSoc.GetSOC_Ents());
        //m_TMP_Itemslot_Equip_Information_Content_Condition_Soc_R.text += "마        족: " + Refine_Condition(item.m_sSoc_Limit_Min.GetSOC_Devil(), item.m_sSoc_Limit_Max.GetSOC_Devil()) + "\n";
        m_TMP_Itemslot_Equip_Information_Content_Condition_Soc_R.text += Refine_Condition("마        족: ", "\n", item.m_sSoc_Limit_Min.GetSOC_Devil(), item.m_sSoc_Limit_Max.GetSOC_Devil(), player.m_sSoc.GetSOC_Devil());
        //m_TMP_Itemslot_Equip_Information_Content_Condition_Soc_R.text += "용        족: " + Refine_Condition(item.m_sSoc_Limit_Min.GetSOC_Dragon(), item.m_sSoc_Limit_Max.GetSOC_Dragon()) + "\n";
        m_TMP_Itemslot_Equip_Information_Content_Condition_Soc_R.text += Refine_Condition("용        족: ", "\n", item.m_sSoc_Limit_Min.GetSOC_Dragon(), item.m_sSoc_Limit_Max.GetSOC_Dragon(), player.m_sSoc.GetSOC_Dragon());
        //m_TMP_Itemslot_Equip_Information_Content_Condition_Soc_R.text += "어        둠: " + Refine_Condition(item.m_sSoc_Limit_Min.GetSOC_Shadow(), item.m_sSoc_Limit_Max.GetSOC_Shadow());
        m_TMP_Itemslot_Equip_Information_Content_Condition_Soc_R.text += Refine_Condition("어        듬: ", "", item.m_sSoc_Limit_Min.GetSOC_Shadow(), item.m_sSoc_Limit_Max.GetSOC_Shadow(), player.m_sSoc.GetSOC_Shadow());

        // 장비 아이템 설명.
        m_TMP_Itemslot_Equip_Information_Content_ItemDescription_Content.text = "";
        m_TMP_Itemslot_Equip_Information_Content_ItemDescription_Content.text += item.GetItemDescription();
    }
    void UpdateItemEquipInformation_EquipPossibility(Item_Equip item, int arynumber)
    {
        if (m_bEquip_Condition_Check == true)
        {
            m_TMP_Itemslot_Equip_Information_EquipPossibility.text = m_sColor_White + "착용가능" + m_sColor_End;
            m_BTN_Itemslot_Equip_Information_EquipPossibility.onClick.RemoveAllListeners();
            m_BTN_Itemslot_Equip_Information_EquipPossibility.onClick.AddListener(delegate { Set_BTN_EquipPossibility_Possible(arynumber, item); });
        }
        else
        {
            m_TMP_Itemslot_Equip_Information_EquipPossibility.text = m_sColor_Red + "착용불가능" + m_sColor_End;
            m_BTN_Itemslot_Equip_Information_EquipPossibility.onClick.RemoveAllListeners();
            m_BTN_Itemslot_Equip_Information_EquipPossibility.onClick.AddListener(delegate { Set_BTN_EquipPossibility_ImPossible(); });
        }
    }
    void UpdateItemEquipInformation_SetItemEffect(Item_Equip item)
    {
        m_nList_SetItemEffect_Code.Clear();
        int setitemeffectnumber = ItemSetEffectManager.instance.Return_SetItemEffect(item.m_nItemCode);
        if (setitemeffectnumber == 0)
        {
            m_gPanel_SetItemEffect.SetActive(false);

            m_ISE = null;
        }
        else
        {
            m_gPanel_SetItemEffect.SetActive(true);

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
                m_bEquip_Condition_Check = false;
            }
        }
        else
        {
            m_sBuffer = m_sColor_Brown + m_sBuffer + m_sColor_End + aftersentence;
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
}
