﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//
// ※ 소비아이템 세부 정보 GUI(인벤토리)
//    해당 GUI를 활성화 하여 플레이어가 현재 보유중인 소비아이템의 세부 정보를 확인할 수 있다.
//

public class GUI_Itemslot_Use_Information : MonoBehaviour
{
    // GUI 오브젝트
    public GameObject m_gPanel_Itemslot_Use_Information;

    [SerializeField] GameObject m_gPanel_Itemslot_Use_Information_UpBar;
    [SerializeField] TextMeshProUGUI m_TMP_Itemslot_Use_Information_UpBar; // (텍스트) 소비아이템 이름, 강화 상태
    [SerializeField] Button m_BTN_Itemslot_Use_Information_UpBar_Exit;     // (버튼) 소비아이템 세부 정보 GUI 비활성화

    [SerializeField] GameObject m_gPanel_Itemslot_Use_Information_Content;

    // 1. 소비아이템 이미지
    [SerializeField] GameObject m_gPanel_Itemslot_Use_Information_Content_Image;
    [SerializeField] GameObject m_gPanel_Itemslot_Use_Information_Content_Image_ItemSprite;
    [SerializeField] Image m_IMG_Itemslot_Use_Information_Content_Image_ItemSprite; // (이미지) 소비아이템 이미지

    // 2. 소비아이템 분류, 등급, 추가 옵션 등급, 강화 상태
    [SerializeField] GameObject m_gPanel_Itemslot_Use_Information_Content_ItemInformation;
    [SerializeField] TextMeshProUGUI m_TMP_Itemslot_Use_Information_Content_ItemInformation; // (텍스트) 소비아이템 분류, 등급, 추가 옵션 등급, 강화 상태

    [SerializeField] Button m_BTN_Itemslot_Use_Information_Content_ChangeInformation_L; // (버튼) 소비아이템 정보 변경(L)
    [SerializeField] Button m_BTN_Itemslot_Use_Information_Content_ChangeInformation_R; // (버튼) 소비아이템 정보 변경(R)

    // 3. 소비아이템 정보 - 소비아이템 설명
    [SerializeField] GameObject m_gPanel_Itemslot_Use_Information_Content_ItemDescription;
    [SerializeField] GameObject m_gPanel_Itemslot_Use_Information_Content_ItemDescription_Name;
    [SerializeField] TextMeshProUGUI m_TMP_Itemslot_Use_Information_Content_ItemDescription_Name;     // (텍스트) "아이템 설명"
    [SerializeField] GameObject m_gPanel_Itemslot_Use_Information_Content_ItemDescription_Content;
    [SerializeField] GameObject m_gSV_Itemslot_Use_Information_Content_ItemDescription_Content;
    [SerializeField] GameObject m_gViewport_Itemslot_Use_Information_Content_ItemDescription_Content;
    [SerializeField] TextMeshProUGUI m_TMP_Itemslot_Use_Information_Content_ItemDescription_Content;  // (텍스트) 소비아이템 설명
    [SerializeField] Scrollbar m_Scrollbar_Itemslot_Use_Information_Content_ItemDescription_Content;  // (스크롤바) 소비아이템 설명

    // 4. 소비아이템 정보 - 소비아이템 사용효과
    [SerializeField] GameObject m_gPanel_Itemslot_Use_Information_Content_Effect;
    [SerializeField] GameObject m_gPanel_Itemslot_Use_Information_Content_Effect_Name;
    [SerializeField] TextMeshProUGUI m_TMP_Itemslot_Use_Information_Content_Effect_Name;     // (텍스트) "사용효과"
    [SerializeField] GameObject m_gPanel_Itemslot_Use_Information_Content_Effect_Status;
    [SerializeField] TextMeshProUGUI m_TMP_Itemslot_Use_Information_Content_Effect_Status_L; // (텍스트) 소비아이템 사용효과(스탯(능력치))_L
    [SerializeField] TextMeshProUGUI m_TMP_Itemslot_Use_Information_Content_Effect_Status_R; // (텍스트) 소비아이템 사용효과(스탯(능력치))_R
    [SerializeField] GameObject m_gPanel_Itemslot_Use_Information_Content_Effect_Soc;
    [SerializeField] TextMeshProUGUI m_TMP_Itemslot_Use_Information_Content_Effect_Soc_L;    // (텍스트) 소비아이템 사용효과(스탯(평판))_L
    [SerializeField] TextMeshProUGUI m_TMP_Itemslot_Use_Information_Content_Effect_Soc_R;    // (텍스트) 소비아이템 사용효과(스탯(평판))_R

    // 5. 소비아이템 정보 - 소비아이템 사용조건
    [SerializeField] GameObject m_gPanel_Itemslot_Use_Information_Content_Condition;
    [SerializeField] GameObject m_gPanel_Itemslot_Use_Information_Content_Condition_Name;
    [SerializeField] TextMeshProUGUI m_TMP_Itemslot_Use_Information_Content_Condition_Name;     // (텍스트) "사용조건"
    [SerializeField] GameObject m_gPanel_Itemslot_Use_Information_Content_Condition_Status;
    [SerializeField] TextMeshProUGUI m_TMP_Itemslot_Use_Information_Content_Condition_Status_L; // (텍스트) 사용조건 착용조건(스탯(능력치))_L
    [SerializeField] TextMeshProUGUI m_TMP_Itemslot_Use_Information_Content_Condition_Status_R; // (텍스트) 사용조건 착용조건(스탯(능력치))_R
    [SerializeField] GameObject m_gPanel_Itemslot_Use_Information_Content_Condition_Soc;
    [SerializeField] TextMeshProUGUI m_TMP_Itemslot_Use_Information_Content_Condition_Soc_L;
    [SerializeField] TextMeshProUGUI m_TMP_Itemslot_Use_Information_Content_Condition_Soc_R;

    [SerializeField] GameObject m_gPanel_Itemslot_Use_Information_UsePossibility;
    [SerializeField] Button m_BTN_Itemslot_Use_Information_UsePossibility;          // (버튼) 소비아이템 사용
    [SerializeField] TextMeshProUGUI m_TMP_Itemslot_Use_Information_UsePossibility; // (텍스트) "사용가능 / 사용불가능"

    // 소비아이템(기프트) 세부 정보 GUI
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
    public enum E_Itemslot_Use_Information_PageNumber { EFFECT, CONDITION, DESCRIPTION }
    public E_Itemslot_Use_Information_PageNumber m_eInformationPage;

    // Text 임시 저장소.
    string m_sBuffer;
    // Color.
    string m_sColor_White = "<color=#ffffff>";
    string m_sColor_WhiteGray = "<color=#808080>";
    string m_sColor_Red = "<color=#FF0000>";
    string m_sColor_Brown = "<color=#915446>";
    string m_sColor_End = "</color>";

    // Text 정제 관련 변수.
    bool m_bRefine_Condition_Check;
    // 아이템 사용 가능 여부.
    public bool m_bUse_Condition_Check;

    // Player 정보.
    Player_Status player;

    public void InitialSet()
    {
        InitialSet_Object();
        InitialSet_Button();
        m_eInformationPage = E_Itemslot_Use_Information_PageNumber.DESCRIPTION;
        player = Player_Total.Instance.m_ps_Status;
    }

    // 초기 Object 불러오기.
    void InitialSet_Object()
    {
        m_gPanel_Itemslot_Use_Information = GameObject.Find("Canvas_GUI").transform.Find("Panel_Itemslot_Use_Information").gameObject;


        m_gPanel_Itemslot_Use_Information_UpBar = m_gPanel_Itemslot_Use_Information.transform.Find("Panel_Itemslot_Use_Information_UpBar").gameObject;
        m_TMP_Itemslot_Use_Information_UpBar = m_gPanel_Itemslot_Use_Information_UpBar.transform.Find("TMP_Itemslot_Use_Information_UpBar").gameObject.GetComponent<TextMeshProUGUI>();
        m_BTN_Itemslot_Use_Information_UpBar_Exit = m_gPanel_Itemslot_Use_Information_UpBar.transform.Find("BTN_Itemslot_Use_Information_UpBar_Exit").gameObject.GetComponent<Button>();


        m_gPanel_Itemslot_Use_Information_Content = m_gPanel_Itemslot_Use_Information.transform.Find("Panel_Itemslot_Use_Information_Content").gameObject;


        m_gPanel_Itemslot_Use_Information_Content_Image = m_gPanel_Itemslot_Use_Information_Content.transform.Find("Panel_Itemslot_Use_Information_Content_Image").gameObject;
        m_gPanel_Itemslot_Use_Information_Content_Image_ItemSprite = m_gPanel_Itemslot_Use_Information_Content_Image.transform.Find("Panel_Itemslot_Use_Information_Content_Image_ItemSprite").gameObject;
        m_IMG_Itemslot_Use_Information_Content_Image_ItemSprite = m_gPanel_Itemslot_Use_Information_Content_Image_ItemSprite.GetComponent<Image>();


        m_gPanel_Itemslot_Use_Information_Content_ItemInformation = m_gPanel_Itemslot_Use_Information_Content.transform.Find("Panel_Itemslot_Use_Information_Content_ItemInformation").gameObject;
        m_TMP_Itemslot_Use_Information_Content_ItemInformation = m_gPanel_Itemslot_Use_Information_Content_ItemInformation.transform.Find("TMP_Itemslot_Use_Information_Content_ItemInformation").gameObject.GetComponent<TextMeshProUGUI>();


        m_gPanel_Itemslot_Use_Information_Content_Effect = m_gPanel_Itemslot_Use_Information_Content.transform.Find("Panel_Itemslot_Use_Information_Content_Effect").gameObject;

        m_gPanel_Itemslot_Use_Information_Content_Effect_Name = m_gPanel_Itemslot_Use_Information_Content_Effect.transform.Find("Panel_Itemslot_Use_Information_Content_Effect_Name").gameObject;
        m_TMP_Itemslot_Use_Information_Content_Effect_Name = m_gPanel_Itemslot_Use_Information_Content_Effect_Name.transform.Find("TMP_Itemslot_Use_Information_Content_Effect_Name").gameObject.GetComponent<TextMeshProUGUI>();

        m_gPanel_Itemslot_Use_Information_Content_Effect_Status = m_gPanel_Itemslot_Use_Information_Content_Effect.transform.Find("Panel_Itemslot_Use_Information_Content_Effect_Status").gameObject;
        m_TMP_Itemslot_Use_Information_Content_Effect_Status_L = m_gPanel_Itemslot_Use_Information_Content_Effect_Status.transform.Find("TMP_Itemslot_Use_Information_Content_Effect_Status_L").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_Itemslot_Use_Information_Content_Effect_Status_R = m_gPanel_Itemslot_Use_Information_Content_Effect_Status.transform.Find("TMP_Itemslot_Use_Information_Content_Effect_Status_R").gameObject.GetComponent<TextMeshProUGUI>();

        m_gPanel_Itemslot_Use_Information_Content_Effect_Soc = m_gPanel_Itemslot_Use_Information_Content_Effect.transform.Find("Panel_Itemslot_Use_Information_Content_Effect_Soc").gameObject;
        m_TMP_Itemslot_Use_Information_Content_Effect_Soc_L = m_gPanel_Itemslot_Use_Information_Content_Effect_Soc.transform.Find("TMP_Itemslot_Use_Information_Content_Effect_Soc_L").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_Itemslot_Use_Information_Content_Effect_Soc_R = m_gPanel_Itemslot_Use_Information_Content_Effect_Soc.transform.Find("TMP_Itemslot_Use_Information_Content_Effect_Soc_R").gameObject.GetComponent<TextMeshProUGUI>();


        m_gPanel_Itemslot_Use_Information_Content_Condition = m_gPanel_Itemslot_Use_Information_Content.transform.Find("Panel_Itemslot_Use_Information_Content_Condition").gameObject;

        m_gPanel_Itemslot_Use_Information_Content_Condition_Name = m_gPanel_Itemslot_Use_Information_Content_Condition.transform.Find("Panel_Itemslot_Use_Information_Content_Condition_Name").gameObject;
        m_TMP_Itemslot_Use_Information_Content_Condition_Name = m_gPanel_Itemslot_Use_Information_Content_Condition_Name.transform.Find("TMP_Itemslot_Use_Information_Content_Condition_Name").gameObject.GetComponent<TextMeshProUGUI>();

        m_gPanel_Itemslot_Use_Information_Content_Condition_Status = m_gPanel_Itemslot_Use_Information_Content_Condition.transform.Find("Panel_Itemslot_Use_Information_Content_Condition_Status").gameObject;
        m_TMP_Itemslot_Use_Information_Content_Condition_Status_L = m_gPanel_Itemslot_Use_Information_Content_Condition_Status.transform.Find("TMP_Itemslot_Use_Information_Content_Condition_Status_L").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_Itemslot_Use_Information_Content_Condition_Status_R = m_gPanel_Itemslot_Use_Information_Content_Condition_Status.transform.Find("TMP_Itemslot_Use_Information_Content_Condition_Status_R").gameObject.GetComponent<TextMeshProUGUI>();

        m_gPanel_Itemslot_Use_Information_Content_Condition_Soc = m_gPanel_Itemslot_Use_Information_Content_Condition.transform.Find("Panel_Itemslot_Use_Information_Content_Condition_Soc").gameObject;
        m_TMP_Itemslot_Use_Information_Content_Condition_Soc_L = m_gPanel_Itemslot_Use_Information_Content_Condition_Soc.transform.Find("TMP_Itemslot_Use_Information_Content_Condition_Soc_L").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_Itemslot_Use_Information_Content_Condition_Soc_R = m_gPanel_Itemslot_Use_Information_Content_Condition_Soc.transform.Find("TMP_Itemslot_Use_Information_Content_Condition_Soc_R").gameObject.GetComponent<TextMeshProUGUI>();


        m_gPanel_Itemslot_Use_Information_Content_ItemDescription = m_gPanel_Itemslot_Use_Information_Content.transform.Find("Panel_Itemslot_Use_Information_Content_ItemDescription").gameObject;

        m_gPanel_Itemslot_Use_Information_Content_ItemDescription_Name = m_gPanel_Itemslot_Use_Information_Content_ItemDescription.transform.Find("Panel_Itemslot_Use_Information_Content_ItemDescription_Name").gameObject;
        m_TMP_Itemslot_Use_Information_Content_ItemDescription_Name = m_gPanel_Itemslot_Use_Information_Content_ItemDescription_Name.transform.Find("TMP_Itemslot_Use_Information_Content_ItemDescription_Name").gameObject.GetComponent<TextMeshProUGUI>();

        m_gPanel_Itemslot_Use_Information_Content_ItemDescription_Content = m_gPanel_Itemslot_Use_Information_Content_ItemDescription.transform.Find("Panel_Itemslot_Use_Information_Content_ItemDescription_Content").gameObject;
        m_gSV_Itemslot_Use_Information_Content_ItemDescription_Content = m_gPanel_Itemslot_Use_Information_Content_ItemDescription_Content.transform.Find("SV_Itemslot_Use_Information_Content_ItemDescription_Content").gameObject;
        m_gViewport_Itemslot_Use_Information_Content_ItemDescription_Content = m_gSV_Itemslot_Use_Information_Content_ItemDescription_Content.transform.Find("Viewport_Itemslot_Use_Information_Content_ItemDescription_Content").gameObject;
        m_TMP_Itemslot_Use_Information_Content_ItemDescription_Content = m_gViewport_Itemslot_Use_Information_Content_ItemDescription_Content.transform.Find("TMP_Itemslot_Use_Information_Content_ItemDescription_Content").gameObject.GetComponent<TextMeshProUGUI>();
        m_Scrollbar_Itemslot_Use_Information_Content_ItemDescription_Content = m_gSV_Itemslot_Use_Information_Content_ItemDescription_Content.transform.Find("Scrollbar_Itemslot_Use_Information_Content_ItemDescription_Content").gameObject.GetComponent<Scrollbar>();


        m_BTN_Itemslot_Use_Information_Content_ChangeInformation_R = m_gPanel_Itemslot_Use_Information_Content.transform.Find("BTN_Itemslot_Use_Information_Content_ChangeInformation_R").gameObject.GetComponent<Button>();
        m_BTN_Itemslot_Use_Information_Content_ChangeInformation_L = m_gPanel_Itemslot_Use_Information_Content.transform.Find("BTN_Itemslot_Use_Information_Content_ChangeInformation_L").gameObject.GetComponent<Button>();


        m_gPanel_Itemslot_Use_Information_UsePossibility = m_gPanel_Itemslot_Use_Information.transform.Find("Panel_Itemslot_Use_Information_UsePossibility").gameObject;
        m_BTN_Itemslot_Use_Information_UsePossibility = m_gPanel_Itemslot_Use_Information_UsePossibility.transform.Find("BTN_Itemslot_Use_Information_UsePossibility").gameObject.GetComponent<Button>();
        m_TMP_Itemslot_Use_Information_UsePossibility = m_BTN_Itemslot_Use_Information_UsePossibility.transform.Find("TMP_Itemslot_Use_Information_UsePossibility").gameObject.GetComponent<TextMeshProUGUI>();



        m_gPanel_Gift_Info = m_gPanel_Itemslot_Use_Information.transform.Find("Panel_Gift_Info").gameObject;

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
    // 초기 Button 이벤트 설정.
    void InitialSet_Button()
    {
        m_BTN_Itemslot_Use_Information_UpBar_Exit.onClick.RemoveAllListeners();
        m_BTN_Itemslot_Use_Information_UpBar_Exit.onClick.AddListener(delegate { Set_BTN_Exit(); });
    }

    // Use Button 이벤트 설정.
    // 소비 아이템 설명 창 페이지 변경.
    void Set_BTN_Exit()
    {
        m_gPanel_Itemslot_Use_Information.SetActive(false);
    }
    void Set_BTN_ChangeInformationPageNumber_R(E_ITEM_USE_TYPE eiut)
    {
        if (eiut == E_ITEM_USE_TYPE.GIFT)
        {
            switch (m_eInformationPage)
            {
                case E_Itemslot_Use_Information_PageNumber.CONDITION:
                    {
                        m_eInformationPage = E_Itemslot_Use_Information_PageNumber.DESCRIPTION;
                        m_gPanel_Itemslot_Use_Information_Content_Effect.SetActive(false);
                        m_gPanel_Itemslot_Use_Information_Content_Condition.SetActive(false);
                        m_gPanel_Itemslot_Use_Information_Content_ItemDescription.SetActive(true);
                        m_Scrollbar_Itemslot_Use_Information_Content_ItemDescription_Content.value = 1;
                    }
                    break;
                case E_Itemslot_Use_Information_PageNumber.DESCRIPTION:
                    {
                        m_eInformationPage = E_Itemslot_Use_Information_PageNumber.CONDITION;
                        m_gPanel_Itemslot_Use_Information_Content_Effect.SetActive(false);
                        m_gPanel_Itemslot_Use_Information_Content_Condition.SetActive(true);
                        m_gPanel_Itemslot_Use_Information_Content_ItemDescription.SetActive(false);
                    }
                    break;
            }
        }
        else
        {
            switch (m_eInformationPage)
            {
                case E_Itemslot_Use_Information_PageNumber.EFFECT:
                    {
                        m_eInformationPage = E_Itemslot_Use_Information_PageNumber.CONDITION;
                        m_gPanel_Itemslot_Use_Information_Content_Effect.SetActive(false);
                        m_gPanel_Itemslot_Use_Information_Content_Condition.SetActive(true);
                        m_gPanel_Itemslot_Use_Information_Content_ItemDescription.SetActive(false);
                    }
                    break;
                case E_Itemslot_Use_Information_PageNumber.CONDITION:
                    {
                        m_eInformationPage = E_Itemslot_Use_Information_PageNumber.DESCRIPTION;
                        m_gPanel_Itemslot_Use_Information_Content_Effect.SetActive(false);
                        m_gPanel_Itemslot_Use_Information_Content_Condition.SetActive(false);
                        m_gPanel_Itemslot_Use_Information_Content_ItemDescription.SetActive(true);
                        m_Scrollbar_Itemslot_Use_Information_Content_ItemDescription_Content.value = 1;
                    }
                    break;
                case E_Itemslot_Use_Information_PageNumber.DESCRIPTION:
                    {
                        m_eInformationPage = E_Itemslot_Use_Information_PageNumber.EFFECT;
                        m_gPanel_Itemslot_Use_Information_Content_Effect.SetActive(true);
                        m_gPanel_Itemslot_Use_Information_Content_Condition.SetActive(false);
                        m_gPanel_Itemslot_Use_Information_Content_ItemDescription.SetActive(false);
                    }
                    break;
            }
        }
    }
    void Set_BTN_ChangeInformationPageNumber_L(E_ITEM_USE_TYPE eiut)
    {
        if (eiut == E_ITEM_USE_TYPE.GIFT)
        {
            switch (m_eInformationPage)
            {
                case E_Itemslot_Use_Information_PageNumber.CONDITION:
                    {
                        m_eInformationPage = E_Itemslot_Use_Information_PageNumber.DESCRIPTION;
                        m_gPanel_Itemslot_Use_Information_Content_Effect.SetActive(false);
                        m_gPanel_Itemslot_Use_Information_Content_Condition.SetActive(false);
                        m_gPanel_Itemslot_Use_Information_Content_ItemDescription.SetActive(true);
                        m_Scrollbar_Itemslot_Use_Information_Content_ItemDescription_Content.value = 1;
                    }
                    break;
                case E_Itemslot_Use_Information_PageNumber.DESCRIPTION:
                    {
                        m_eInformationPage = E_Itemslot_Use_Information_PageNumber.CONDITION;
                        m_gPanel_Itemslot_Use_Information_Content_Effect.SetActive(false);
                        m_gPanel_Itemslot_Use_Information_Content_Condition.SetActive(true);
                        m_gPanel_Itemslot_Use_Information_Content_ItemDescription.SetActive(false);
                    }
                    break;
            }
        }
        else
        {
            switch (m_eInformationPage)
            {
                case E_Itemslot_Use_Information_PageNumber.EFFECT:
                    {
                        m_eInformationPage = E_Itemslot_Use_Information_PageNumber.DESCRIPTION;
                        m_gPanel_Itemslot_Use_Information_Content_Effect.SetActive(false);
                        m_gPanel_Itemslot_Use_Information_Content_Condition.SetActive(false);
                        m_gPanel_Itemslot_Use_Information_Content_ItemDescription.SetActive(true);
                        m_Scrollbar_Itemslot_Use_Information_Content_ItemDescription_Content.value = 1;
                    }
                    break;
                case E_Itemslot_Use_Information_PageNumber.CONDITION:
                    {
                        m_eInformationPage = E_Itemslot_Use_Information_PageNumber.EFFECT;
                        m_gPanel_Itemslot_Use_Information_Content_Effect.SetActive(true);
                        m_gPanel_Itemslot_Use_Information_Content_Condition.SetActive(false);
                        m_gPanel_Itemslot_Use_Information_Content_ItemDescription.SetActive(false);
                    }
                    break;
                case E_Itemslot_Use_Information_PageNumber.DESCRIPTION:
                    {
                        m_eInformationPage = E_Itemslot_Use_Information_PageNumber.CONDITION;
                        m_gPanel_Itemslot_Use_Information_Content_Effect.SetActive(false);
                        m_gPanel_Itemslot_Use_Information_Content_Condition.SetActive(true);
                        m_gPanel_Itemslot_Use_Information_Content_ItemDescription.SetActive(false);
                    }
                    break;
            }
        }
    }
    // 소비 아이템 사용가능.
    void Set_BTN_UsePossibility_Possible(int arynumber)
    {
        int itemcode = Player_Itemslot.m_gary_Itemslot_Use[arynumber].m_nItemCode;
        // 사용 조건 체크
        if (Player_Total.Instance.m_pm_Move.m_ePlayerMoveState != Player_Move.E_PLAYER_MOVE_STATE.DEATH)
        {
            if (Player_Itemslot.m_gary_Itemslot_Use[arynumber].m_eItemUseType == E_ITEM_USE_TYPE.RECOVERPOTION ||
            Player_Itemslot.m_gary_Itemslot_Use[arynumber].m_eItemUseType == E_ITEM_USE_TYPE.TEMPORARYBUFFPOTION ||
            Player_Itemslot.m_gary_Itemslot_Use[arynumber].m_eItemUseType == E_ITEM_USE_TYPE.ETERNALBUFFPOTION)
            {
                int num_1;
                num_1 = Player_Total.Instance.CheckCondition_Item_Use(Player_Itemslot.m_gary_Itemslot_Use[arynumber], arynumber);


                if (num_1 == 0)
                {
                    Player_Itemslot.m_nary_Itemslot_Use_Count[arynumber] -= 1;
                    //GUIManager_Total.Instance.UpdateLog("[소비아이템][" + Player_Itemslot.m_gary_Itemslot_Use[arynumber].m_sItemName + "] 사용.");
                }
                else if (num_1 == 1)
                {
                    GUIManager_Total.Instance.UpdateLog("[소비아이템][" + Player_Itemslot.m_gary_Itemslot_Use[arynumber].m_sItemName + "] 사용 불가능.");
                }

                if (Player_Itemslot.m_nary_Itemslot_Use_Count[arynumber] == 0)
                {
                    Player_Itemslot.m_gary_Itemslot_Use[arynumber] = null;
                    m_BTN_Itemslot_Use_Information_UsePossibility.GetComponent<Button>().onClick.RemoveAllListeners();
                    m_gPanel_Itemslot_Use_Information.SetActive(false);
                }
            }
            else if (Player_Itemslot.m_gary_Itemslot_Use[arynumber].m_eItemUseType == E_ITEM_USE_TYPE.REINFORCEMENT)
            {
                int num_1;
                num_1 = Player_Total.Instance.CheckCondition_Item_Use(Player_Itemslot.m_gary_Itemslot_Use[arynumber], arynumber);

                if (num_1 == 0)
                {
                    //GUIManager_Total.Instance.UpdateLog("[소비아이템][" + Player_Itemslot.m_gary_Itemslot_Use[arynumber].m_sItemName + "] 사용 가능.");
                }
                else if (num_1 == 1)
                {
                    GUIManager_Total.Instance.UpdateLog("[소비아이템][" + Player_Itemslot.m_gary_Itemslot_Use[arynumber].m_sItemName + "] 사용 불가능.");
                }
            }
            else if (Player_Itemslot.m_gary_Itemslot_Use[arynumber].m_eItemUseType == E_ITEM_USE_TYPE.GIFT)
            {
                int num_1;
                num_1 = Player_Total.Instance.CheckCondition_Item_Use(Player_Itemslot.m_gary_Itemslot_Use[arynumber], arynumber);

                if (num_1 == 0)
                {
                    Player_Itemslot.m_nary_Itemslot_Use_Count[arynumber] -= 1;
                    //GUIManager_Total.Instance.UpdateLog("[소비아이템][" + Player_Itemslot.m_gary_Itemslot_Use[arynumber].m_sItemName + "] 사용 가능.");

                    if (m_gPanel_Itemslot_Use_Information.activeSelf == true)
                        m_gPanel_Itemslot_Use_Information.SetActive(false);
                }
                else if (num_1 == 1)
                {
                    GUIManager_Total.Instance.UpdateLog("[소비아이템][" + Player_Itemslot.m_gary_Itemslot_Use[arynumber].m_sItemName + "] 사용 불가능.");
                }

                if (Player_Itemslot.m_nary_Itemslot_Use_Count[arynumber] == 0)
                {
                    Player_Itemslot.m_gary_Itemslot_Use[arynumber] = null;
                    m_BTN_Itemslot_Use_Information_UsePossibility.GetComponent<Button>().onClick.RemoveAllListeners();
                    m_gPanel_Itemslot_Use_Information.SetActive(false);
                }
            }

            GUIManager_Total.Instance.Update_Itemslot();
            GUIManager_Total.Instance.m_GUI_Quickslot.Update_Quickslot();
            Player_Total.Instance.m_pq_Quest.QuestUpdate_Collect(ItemManager.instance.m_Dictionary_MonsterDrop_Use[itemcode]);
        }
    }

    public void Display_GUI_Itemslot_Use_Information(float fcoordination_x, float fcoordination_y)
    {
        m_gPanel_Itemslot_Use_Information.SetActive(true);
        m_gPanel_Itemslot_Use_Information.transform.SetAsLastSibling();
        m_gPanel_Itemslot_Use_Information.transform.position = new Vector2(fcoordination_x, fcoordination_y);
    }
    public void UnDisplay_GUI_Itemslot_Use_Information()
    {
        m_gPanel_Itemslot_Use_Information.SetActive(false);
        m_Scrollbar_Itemslot_Use_Information_Content_ItemDescription_Content.value = 1;
    }

    // 소비 아이템 설명 창 세부 설정.
    public void UpdateItemUseInformation(Item_Use item, int arynumber)
    {
        m_bUse_Condition_Check = true;
        
        m_Scrollbar_Itemslot_Use_Information_Content_ItemDescription_Content.value = 1;
        
        UpdateItemUseInformation_UpBar(item);
        UpdateItemUseInformation_Content(item);
        UpdateItemUseInformation_UsePossibility(item, arynumber);
        UpdateItemUseInformation_Button(item.m_eItemUseType);
        UpdateItemUse_GiftInformation(item);
    }
    // 착용 및 사용 금지.
    public void UseBan()
    {
        m_BTN_Itemslot_Use_Information_UsePossibility.onClick.RemoveAllListeners();
    }
    void UpdateItemUseInformation_UpBar(Item_Use item)
    {
        // 소비 아이템 이름, 강화 상태.
        m_TMP_Itemslot_Use_Information_UpBar.text = item.m_sItemName;
    }
    void UpdateItemUseInformation_Content(Item_Use item)
    {
        // 소비 아이템 이미지.
        m_IMG_Itemslot_Use_Information_Content_Image_ItemSprite.sprite = item.m_sp_Sprite;
        m_IMG_Itemslot_Use_Information_Content_Image_ItemSprite.color = new Color(1, 1, 1, 0.75f);

        // 소비 아이템 분류, 등급, 추가 옵션 등급, 강화 상태.
        // 분류.
        m_TMP_Itemslot_Use_Information_Content_ItemInformation.text = "";

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
        m_TMP_Itemslot_Use_Information_Content_ItemInformation.text += "분류: " + m_sBuffer + "\n";
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
        m_TMP_Itemslot_Use_Information_Content_ItemInformation.text += "등급: " + m_sBuffer + "\n";

        // 소비 아이템 효과.
        // 소비 아이템 효과 Status.
        switch (item.m_eItemUseType)
        {
            case E_ITEM_USE_TYPE.RECOVERPOTION:
                {
                    m_TMP_Itemslot_Use_Information_Content_Effect_Status_L.text = "";
                    m_TMP_Itemslot_Use_Information_Content_Effect_Status_L.text += Refine_Condition("레        벨:", item.m_sStatus_Effect.GetSTATUS_LV());
                    m_TMP_Itemslot_Use_Information_Content_Effect_Status_L.text += Refine_Condition("\n체        력:", item.m_sStatus_Effect.GetSTATUS_HP_Current());
                    m_TMP_Itemslot_Use_Information_Content_Effect_Status_R.text = "";
                    m_TMP_Itemslot_Use_Information_Content_Effect_Status_R.text += "\n";
                    m_TMP_Itemslot_Use_Information_Content_Effect_Status_R.text += Refine_Condition("마        나:", item.m_sStatus_Effect.GetSTATUS_MP_Current());
                }
                break;
            case E_ITEM_USE_TYPE.TEMPORARYBUFFPOTION: 
            case E_ITEM_USE_TYPE.ETERNALBUFFPOTION:
                {
                    m_TMP_Itemslot_Use_Information_Content_Effect_Status_L.text = "";
                    m_TMP_Itemslot_Use_Information_Content_Effect_Status_L.text += Refine_Condition("레        벨:", item.m_sStatus_Effect.GetSTATUS_LV());
                    m_TMP_Itemslot_Use_Information_Content_Effect_Status_L.text += Refine_Condition("\n체        력:", item.m_sStatus_Effect.GetSTATUS_HP_Max());
                    m_TMP_Itemslot_Use_Information_Content_Effect_Status_L.text += Refine_Condition("\n데  미  지:", item.m_sStatus_Effect.GetSTATUS_Damage_Total());
                    m_TMP_Itemslot_Use_Information_Content_Effect_Status_L.text += Refine_Condition("\n이동속도:", item.m_sStatus_Effect.GetSTATUS_Speed());
                    m_TMP_Itemslot_Use_Information_Content_Effect_Status_R.text = "";
                    m_TMP_Itemslot_Use_Information_Content_Effect_Status_R.text += "\n";
                    m_TMP_Itemslot_Use_Information_Content_Effect_Status_R.text += Refine_Condition("마        나:", item.m_sStatus_Effect.GetSTATUS_MP_Max());
                    m_TMP_Itemslot_Use_Information_Content_Effect_Status_R.text += Refine_Condition("\n방  어  력:", item.m_sStatus_Effect.GetSTATUS_Defence_Physical());
                    m_TMP_Itemslot_Use_Information_Content_Effect_Status_R.text += Refine_Condition("\n공격속도:", item.m_sStatus_Effect.GetSTATUS_AttackSpeed());
                }
                break;
            case E_ITEM_USE_TYPE.REINFORCEMENT:
                {
                    m_TMP_Itemslot_Use_Information_Content_Effect_Status_L.text = "";
                    m_TMP_Itemslot_Use_Information_Content_Effect_Status_L.text += Refine_Condition("레        벨:", item.m_Reinforcement_Effect.GetReinforcementSTATUS().GetSTATUS_LV());
                    m_TMP_Itemslot_Use_Information_Content_Effect_Status_L.text += Refine_Condition("\n체        력:", item.m_Reinforcement_Effect.GetReinforcementSTATUS().GetSTATUS_HP_Max());
                    m_TMP_Itemslot_Use_Information_Content_Effect_Status_L.text += Refine_Condition("\n데  미  지:", item.m_Reinforcement_Effect.GetReinforcementSTATUS().GetSTATUS_Damage_Total());
                    m_TMP_Itemslot_Use_Information_Content_Effect_Status_L.text += Refine_Condition("\n이동속도:", item.m_Reinforcement_Effect.GetReinforcementSTATUS().GetSTATUS_Speed());
                    m_TMP_Itemslot_Use_Information_Content_Effect_Status_R.text = "";
                    m_TMP_Itemslot_Use_Information_Content_Effect_Status_R.text += "\n";
                    m_TMP_Itemslot_Use_Information_Content_Effect_Status_R.text += Refine_Condition("마        나:", item.m_Reinforcement_Effect.GetReinforcementSTATUS().GetSTATUS_MP_Max());
                    m_TMP_Itemslot_Use_Information_Content_Effect_Status_R.text += Refine_Condition("\n방  어  력:", item.m_Reinforcement_Effect.GetReinforcementSTATUS().GetSTATUS_Defence_Physical());
                    m_TMP_Itemslot_Use_Information_Content_Effect_Status_R.text += Refine_Condition("\n공격속도:", item.m_Reinforcement_Effect.GetReinforcementSTATUS().GetSTATUS_AttackSpeed());
                }
                break;
            case E_ITEM_USE_TYPE.GIFT:
                {
                    m_TMP_Itemslot_Use_Information_Content_Effect_Status_L.text = "";
                    m_TMP_Itemslot_Use_Information_Content_Effect_Status_R.text = "";
                }
                break;
        }
        // 소비 아이템 효과 Soc.
        if (item.m_eItemUseType == E_ITEM_USE_TYPE.REINFORCEMENT)
        {
            m_TMP_Itemslot_Use_Information_Content_Effect_Soc_L.text = "";
            m_TMP_Itemslot_Use_Information_Content_Effect_Soc_L.text += Refine_Condition("명        예:", item.m_Reinforcement_Effect.GetReinforcementSOC().GetSOC_Honor());
            m_TMP_Itemslot_Use_Information_Content_Effect_Soc_L.text += Refine_Condition("\n인        간:", item.m_Reinforcement_Effect.GetReinforcementSOC().GetSOC_Human());
            m_TMP_Itemslot_Use_Information_Content_Effect_Soc_L.text += Refine_Condition("\n동        물:", item.m_Reinforcement_Effect.GetReinforcementSOC().GetSOC_Animal());
            m_TMP_Itemslot_Use_Information_Content_Effect_Soc_L.text += Refine_Condition("\n슬  라  임:", item.m_Reinforcement_Effect.GetReinforcementSOC().GetSOC_Slime());
            m_TMP_Itemslot_Use_Information_Content_Effect_Soc_L.text += Refine_Condition("\n스켈레톤:", item.m_Reinforcement_Effect.GetReinforcementSOC().GetSOC_Skeleton());
            m_TMP_Itemslot_Use_Information_Content_Effect_Soc_R.text = "";
            m_TMP_Itemslot_Use_Information_Content_Effect_Soc_R.text += "\n";
            m_TMP_Itemslot_Use_Information_Content_Effect_Soc_R.text += Refine_Condition("앤        트:", item.m_Reinforcement_Effect.GetReinforcementSOC().GetSOC_Ents());
            m_TMP_Itemslot_Use_Information_Content_Effect_Soc_R.text += Refine_Condition("\n마        족:", item.m_Reinforcement_Effect.GetReinforcementSOC().GetSOC_Devil());
            m_TMP_Itemslot_Use_Information_Content_Effect_Soc_R.text += Refine_Condition("\n용        족:", item.m_Reinforcement_Effect.GetReinforcementSOC().GetSOC_Dragon());
            m_TMP_Itemslot_Use_Information_Content_Effect_Soc_R.text += Refine_Condition("\n어        둠:", item.m_Reinforcement_Effect.GetReinforcementSOC().GetSOC_Shadow());
        }
        else if (item.m_eItemUseType == E_ITEM_USE_TYPE.GIFT)
        {
            m_TMP_Itemslot_Use_Information_Content_Effect_Soc_L.text = "";
            m_TMP_Itemslot_Use_Information_Content_Effect_Soc_R.text = "";
        }
        else
        {
            m_TMP_Itemslot_Use_Information_Content_Effect_Soc_L.text = "";
            m_TMP_Itemslot_Use_Information_Content_Effect_Soc_L.text += Refine_Condition("명        예:", item.m_sSoc_Effect.GetSOC_Honor());
            m_TMP_Itemslot_Use_Information_Content_Effect_Soc_L.text += Refine_Condition("\n인        간:", item.m_sSoc_Effect.GetSOC_Human());
            m_TMP_Itemslot_Use_Information_Content_Effect_Soc_L.text += Refine_Condition("\n동        물:", item.m_sSoc_Effect.GetSOC_Animal());
            m_TMP_Itemslot_Use_Information_Content_Effect_Soc_L.text += Refine_Condition("\n슬  라  임:", item.m_sSoc_Effect.GetSOC_Slime());
            m_TMP_Itemslot_Use_Information_Content_Effect_Soc_L.text += Refine_Condition("\n스켈레톤:", item.m_sSoc_Effect.GetSOC_Skeleton());
            m_TMP_Itemslot_Use_Information_Content_Effect_Soc_R.text = "";
            m_TMP_Itemslot_Use_Information_Content_Effect_Soc_R.text += "\n";
            m_TMP_Itemslot_Use_Information_Content_Effect_Soc_R.text += Refine_Condition("앤        트:", item.m_sSoc_Effect.GetSOC_Ents());
            m_TMP_Itemslot_Use_Information_Content_Effect_Soc_R.text += Refine_Condition("\n마        족:", item.m_sSoc_Effect.GetSOC_Devil());
            m_TMP_Itemslot_Use_Information_Content_Effect_Soc_R.text += Refine_Condition("\n용        족:", item.m_sSoc_Effect.GetSOC_Dragon());
            m_TMP_Itemslot_Use_Information_Content_Effect_Soc_R.text += Refine_Condition("\n어        둠:", item.m_sSoc_Effect.GetSOC_Shadow());
        }

        // 소비 아이템 사용 조건.
        // 소비 아이템 사용 조건 Status.
        m_TMP_Itemslot_Use_Information_Content_Condition_Status_L.text = "";
        //m_TMP_Itemslot_Use_Information_Content_Condition_Status_L.text += "레        벨: " + Refine_Condition(item.m_sStatus_Limit_Min.GetSTATUS_LV(), item.m_sStatus_Limit_Max.GetSTATUS_LV()) + "\n";
        m_TMP_Itemslot_Use_Information_Content_Condition_Status_L.text += Refine_Condition("레        벨: ", "\n", item.m_sStatus_Limit_Min.GetSTATUS_LV(), item.m_sStatus_Limit_Max.GetSTATUS_LV(), player.m_sStatus.GetSTATUS_LV());
        //m_TMP_Itemslot_Use_Information_Content_Condition_Status_L.text += "체        력: " + Refine_Condition(item.m_sStatus_Limit_Min.GetSTATUS_HP_Max(), item.m_sStatus_Limit_Max.GetSTATUS_HP_Max()) + "\n";
        m_TMP_Itemslot_Use_Information_Content_Condition_Status_L.text += Refine_Condition("체        력: ", "\n", item.m_sStatus_Limit_Min.GetSTATUS_HP_Max(), item.m_sStatus_Limit_Max.GetSTATUS_HP_Max(), player.m_sStatus.GetSTATUS_HP_Max());
        //m_TMP_Itemslot_Use_Information_Content_Condition_Status_L.text += "데  미  지: " + Refine_Condition(item.m_sStatus_Limit_Min.GetSTATUS_Damage_Total(), item.m_sStatus_Limit_Max.GetSTATUS_Damage_Total()) + "\n";
        m_TMP_Itemslot_Use_Information_Content_Condition_Status_L.text += Refine_Condition("데  미  지: ", "\n", item.m_sStatus_Limit_Min.GetSTATUS_Damage_Total(), item.m_sStatus_Limit_Max.GetSTATUS_Damage_Total(), player.m_sStatus.GetSTATUS_Damage_Total());
        //m_TMP_Itemslot_Use_Information_Content_Condition_Status_L.text += "이동속도: " + Refine_Condition(item.m_sStatus_Limit_Min.GetSTATUS_Speed(), item.m_sStatus_Limit_Max.GetSTATUS_Speed());
        m_TMP_Itemslot_Use_Information_Content_Condition_Status_L.text += Refine_Condition("이동속도: ", "", item.m_sStatus_Limit_Min.GetSTATUS_Speed(), item.m_sStatus_Limit_Max.GetSTATUS_Speed(), player.m_sStatus.GetSTATUS_Speed());
        m_TMP_Itemslot_Use_Information_Content_Condition_Status_R.text = "";
        m_TMP_Itemslot_Use_Information_Content_Condition_Status_R.text += "\n";
        //m_TMP_Itemslot_Use_Information_Content_Condition_Status_R.text += "마        나: " + Refine_Condition(item.m_sStatus_Limit_Min.GetSTATUS_MP_Max(), item.m_sStatus_Limit_Max.GetSTATUS_MP_Max()) + "\n";
        m_TMP_Itemslot_Use_Information_Content_Condition_Status_R.text += Refine_Condition("마        나: ", "\n", item.m_sStatus_Limit_Min.GetSTATUS_MP_Max(), item.m_sStatus_Limit_Max.GetSTATUS_MP_Max(), player.m_sStatus.GetSTATUS_MP_Max());
        //m_TMP_Itemslot_Use_Information_Content_Condition_Status_R.text += "방  어  력: " + Refine_Condition(item.m_sStatus_Limit_Min.GetSTATUS_Defence_Physical(), item.m_sStatus_Limit_Max.GetSTATUS_Defence_Physical()) + "\n";
        m_TMP_Itemslot_Use_Information_Content_Condition_Status_R.text += Refine_Condition("방  어  력: ", "\n", item.m_sStatus_Limit_Min.GetSTATUS_Defence_Physical(), item.m_sStatus_Limit_Max.GetSTATUS_Defence_Physical(), player.m_sStatus.GetSTATUS_Defence_Physical());
        //m_TMP_Itemslot_Use_Information_Content_Condition_Status_R.text += "공격속도: " + Refine_Condition(item.m_sStatus_Limit_Min.GetSTATUS_AttackSpeed(), item.m_sStatus_Limit_Max.GetSTATUS_AttackSpeed());
        m_TMP_Itemslot_Use_Information_Content_Condition_Status_R.text += Refine_Condition("공격속도: ", "", item.m_sStatus_Limit_Min.GetSTATUS_AttackSpeed(), item.m_sStatus_Limit_Max.GetSTATUS_AttackSpeed(), player.m_sStatus.GetSTATUS_AttackSpeed());

        // 소비 아이템 사용 조건 Soc.
        m_TMP_Itemslot_Use_Information_Content_Condition_Soc_L.text = "";
        //m_TMP_Itemslot_Use_Information_Content_Condition_Soc_L.text += "평        판: " + Refine_Condition(item.m_sSoc_Limit_Min.GetSOC_Honor(), item.m_sSoc_Limit_Max.GetSOC_Honor()) + "\n";
        m_TMP_Itemslot_Use_Information_Content_Condition_Soc_L.text += Refine_Condition("명        예: ", "\n", item.m_sSoc_Limit_Min.GetSOC_Honor(), item.m_sSoc_Limit_Max.GetSOC_Honor(), player.m_sSoc.GetSOC_Honor());
        //m_TMP_Itemslot_Use_Information_Content_Condition_Soc_L.text += "인        간: " + Refine_Condition(item.m_sSoc_Limit_Min.GetSOC_Human(), item.m_sSoc_Limit_Max.GetSOC_Human()) + "\n";
        m_TMP_Itemslot_Use_Information_Content_Condition_Soc_L.text += Refine_Condition("인        간: ", "\n", item.m_sSoc_Limit_Min.GetSOC_Human(), item.m_sSoc_Limit_Max.GetSOC_Human(), player.m_sSoc.GetSOC_Human());
        //m_TMP_Itemslot_Use_Information_Content_Condition_Soc_L.text += "동        물: " + Refine_Condition(item.m_sSoc_Limit_Min.GetSOC_Animal(), item.m_sSoc_Limit_Max.GetSOC_Animal()) + "\n";
        m_TMP_Itemslot_Use_Information_Content_Condition_Soc_L.text += Refine_Condition("동        물: ", "\n", item.m_sSoc_Limit_Min.GetSOC_Animal(), item.m_sSoc_Limit_Max.GetSOC_Animal(), player.m_sSoc.GetSOC_Animal());
        //m_TMP_Itemslot_Use_Information_Content_Condition_Soc_L.text += "슬  라  임: " + Refine_Condition(item.m_sSoc_Limit_Min.GetSOC_Slime(), item.m_sSoc_Limit_Max.GetSOC_Slime()) + "\n";
        m_TMP_Itemslot_Use_Information_Content_Condition_Soc_L.text += Refine_Condition("슬  라  임: ", "\n", item.m_sSoc_Limit_Min.GetSOC_Slime(), item.m_sSoc_Limit_Max.GetSOC_Slime(), player.m_sSoc.GetSOC_Slime());
        //m_TMP_Itemslot_Use_Information_Content_Condition_Soc_L.text += "스켈레톤: " + Refine_Condition(item.m_sSoc_Limit_Min.GetSOC_Skeleton(), item.m_sSoc_Limit_Max.GetSOC_Skeleton());
        m_TMP_Itemslot_Use_Information_Content_Condition_Soc_L.text += Refine_Condition("스켈레톤: ", "", item.m_sSoc_Limit_Min.GetSOC_Skeleton(), item.m_sSoc_Limit_Max.GetSOC_Skeleton(), player.m_sSoc.GetSOC_Skeleton());
        m_TMP_Itemslot_Use_Information_Content_Condition_Soc_R.text = "";
        m_TMP_Itemslot_Use_Information_Content_Condition_Soc_R.text += "\n";
        //m_TMP_Itemslot_Use_Information_Content_Condition_Soc_R.text += "앤        트: " + Refine_Condition(item.m_sSoc_Limit_Min.GetSOC_Ents(), item.m_sSoc_Limit_Max.GetSOC_Ents()) + "\n";
        m_TMP_Itemslot_Use_Information_Content_Condition_Soc_R.text += Refine_Condition("앤        트: ", "\n", item.m_sSoc_Limit_Min.GetSOC_Ents(), item.m_sSoc_Limit_Max.GetSOC_Ents(), player.m_sSoc.GetSOC_Ents());
        //m_TMP_Itemslot_Use_Information_Content_Condition_Soc_R.text += "마        족: " + Refine_Condition(item.m_sSoc_Limit_Min.GetSOC_Devil(), item.m_sSoc_Limit_Max.GetSOC_Devil()) + "\n";
        m_TMP_Itemslot_Use_Information_Content_Condition_Soc_R.text += Refine_Condition("마        족: ", "\n", item.m_sSoc_Limit_Min.GetSOC_Devil(), item.m_sSoc_Limit_Max.GetSOC_Devil(), player.m_sSoc.GetSOC_Devil());
        //m_TMP_Itemslot_Use_Information_Content_Condition_Soc_R.text += "용        족: " + Refine_Condition(item.m_sSoc_Limit_Min.GetSOC_Dragon(), item.m_sSoc_Limit_Max.GetSOC_Dragon()) + "\n";
        m_TMP_Itemslot_Use_Information_Content_Condition_Soc_R.text += Refine_Condition("용        족: ", "\n", item.m_sSoc_Limit_Min.GetSOC_Dragon(), item.m_sSoc_Limit_Max.GetSOC_Dragon(), player.m_sSoc.GetSOC_Dragon());
        //m_TMP_Itemslot_Use_Information_Content_Condition_Soc_R.text += "어        둠: " + Refine_Condition(item.m_sSoc_Limit_Min.GetSOC_Shadow(), item.m_sSoc_Limit_Max.GetSOC_Shadow());
        m_TMP_Itemslot_Use_Information_Content_Condition_Soc_R.text += Refine_Condition("어        듬: ", "", item.m_sSoc_Limit_Min.GetSOC_Shadow(), item.m_sSoc_Limit_Max.GetSOC_Shadow(), player.m_sSoc.GetSOC_Shadow());

        // 소비 아이템 설명.
        m_TMP_Itemslot_Use_Information_Content_ItemDescription_Content.text = "";
        m_TMP_Itemslot_Use_Information_Content_ItemDescription_Content.text += item.GetItemDescription();

        if (item.m_eItemUseType == E_ITEM_USE_TYPE.GIFT)
        { 
            if (item.m_bDisplay_Gift_Item == true)
                m_TMP_Itemslot_Use_Information_Content_ItemDescription_Content.text += "\n\n[획득할 수 있는 아이템 정보]\n" + item.Return_Gift_List();

            if (m_eInformationPage != E_Itemslot_Use_Information_PageNumber.DESCRIPTION)
            {
                m_eInformationPage = E_Itemslot_Use_Information_PageNumber.DESCRIPTION;
                m_gPanel_Itemslot_Use_Information_Content_Effect.SetActive(false);
                m_gPanel_Itemslot_Use_Information_Content_Condition.SetActive(false);
                m_gPanel_Itemslot_Use_Information_Content_ItemDescription.SetActive(true);
                m_Scrollbar_Itemslot_Use_Information_Content_ItemDescription_Content.value = 1;
            }
        }
    }
    void UpdateItemUseInformation_UsePossibility(Item_Use item, int arynumber)
    {
        if (m_bUse_Condition_Check == true)
        {
            m_TMP_Itemslot_Use_Information_UsePossibility.text = m_sColor_White + "사용가능" + m_sColor_End;
            m_BTN_Itemslot_Use_Information_UsePossibility.onClick.RemoveAllListeners();
            m_BTN_Itemslot_Use_Information_UsePossibility.onClick.AddListener(delegate { Set_BTN_UsePossibility_Possible(arynumber); });
        }
        else
        {
            m_TMP_Itemslot_Use_Information_UsePossibility.text = m_sColor_Red + "사용불가능" + m_sColor_End;
            m_BTN_Itemslot_Use_Information_UsePossibility.onClick.RemoveAllListeners();
        }

        //if (item.m_eItemUseType == E_ITEM_USE_TYPE.GIFT)
        //{
        //    if (Player_Total.Instance.CheckCondition_Item_Use_Gift(Player_Itemslot.m_gary_Itemslot_Use[arynumber], arynumber) == false)
        //    {
        //        m_TMP_Itemslot_Use_Information_UsePossibility.text = m_sColor_Red + "사용불가능 (아이템 슬롯을 비워주세요.)" + m_sColor_End;
        //        m_BTN_Itemslot_Use_Information_UsePossibility.onClick.RemoveAllListeners();
        //    }
        //}
    }
    void UpdateItemUseInformation_Button(E_ITEM_USE_TYPE eiut)
    {
        m_BTN_Itemslot_Use_Information_Content_ChangeInformation_R.onClick.RemoveAllListeners();
        m_BTN_Itemslot_Use_Information_Content_ChangeInformation_R.onClick.AddListener(delegate { Set_BTN_ChangeInformationPageNumber_R(eiut); });
        m_BTN_Itemslot_Use_Information_Content_ChangeInformation_L.onClick.RemoveAllListeners();
        m_BTN_Itemslot_Use_Information_Content_ChangeInformation_L.onClick.AddListener(delegate { Set_BTN_ChangeInformationPageNumber_L(eiut); });
    }
    void UpdateItemUse_GiftInformation(Item_Use item)
    {
        m_Scrollbar_Gift_Info_Content_Description_Down.value = 1;

        if (item.m_eItemUseType != E_ITEM_USE_TYPE.GIFT)
            m_gPanel_Gift_Info.SetActive(false);
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
                    } break;
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
                    } break;
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
                    } break;
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
                        } break;
                    case E_ITEM_USE_GIFT_TYPE.RANDOMBOX_INDEPENDENTTRIAL:
                    case E_ITEM_USE_GIFT_TYPE.RANDOMBOX_DEPENDENTTRIAL:
                        {
                            for (int i = 0; i < item.m_nDictionary_Gift_Item_Equip_Code.Count; i++)
                            {
                                copypanel = Instantiate(m_gPanel_ItemInfo);
                                copypanel.GetComponent<Iteminfo>().Display(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[item.m_nDictionary_Gift_Item_Equip_Code[i]].m_sp_Sprite,
                                    ItemManager.instance.m_Dictionary_MonsterDrop_Equip[item.m_nDictionary_Gift_Item_Equip_Code[i]].m_sItemName + "\n" + item.m_nDictionary_Gift_Item_Equip_Count[i] + " 개 " +
                                    System.Math.Round(((float)(item.m_nDictionary_Gift_Item_Equip_Probability[i] / (float)10000) * 100), 2) + " %\n");

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
                                    System.Math.Round(((float)(item.m_nDictionary_Gift_Item_Use_Probability[i] / (float)10000) * 100), 2) + " %\n");

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
                                    System.Math.Round(((float)(item.m_nDictionary_Gift_Item_Etc_Probability[i] / (float)10000) * 100), 2) + " %\n");

                                contentpos = copypanel.GetComponent<RectTransform>();
                                contentpos.SetParent(m_gContent_Gift_Info_Content_Description_Down.transform);
                                contentpos.transform.localScale = new Vector3(1, 1, 1);
                                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                                m_gList_Panel_ItemInfo.Add(copypanel);
                            }
                        } break;
                }
            }
        }
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
                m_bUse_Condition_Check = false;
            }
        }
        else
        {
            m_sBuffer = m_sColor_Brown + m_sBuffer + m_sColor_End + aftersentence;
        }

        return m_sBuffer;
    }
    // 각각의 옵션.(유동적인 아이템 효과)
    string Refine_Condition(string sentence, int option)
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
    string Refine_Condition(string sentence, float option)
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
