using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

//
// ※ 장비아이템 세부 정보 GUI
//    해당 GUI를 활성화 하여 플레이어가 현재 착용중인 장비아이템의 세부 정보를 확인할 수 있다.
//

public class GUI_Equipslot_Equip_Information : MonoBehaviour
{
    // GUI 오브젝트
    public GameObject m_gPanel_Equipslot_Equip_Information;
    
    // GUI 오브젝트 - 장비아이템 세부 정보 GUI
    [SerializeField] GameObject m_gPanel_Equipslot_Equip_Information_UpBar;
    [SerializeField] TextMeshProUGUI m_TMP_Equipslot_Equip_Information_UpBar; // (텍스트) 장비아이템 이름, 강화 상태
    [SerializeField] Button m_BTN_Equipslot_Equip_Information_UpBar_Exit;     // (버튼) 장비아이템 세부정보 GUI 비활성화
    
    [SerializeField] GameObject m_gPanel_Equipslot_Equip_Information_Content;

    // 1. 장비아이템 이미지
    [SerializeField] GameObject m_gPanel_Equipslot_Equip_Information_Content_Image;
    [SerializeField] GameObject m_gPanel_Equipslot_Equip_Information_Content_Image_ItemSprite;
    [SerializeField] Image m_IMG_Equipslot_Equip_Information_Content_Image_ItemSprite; // (이미지) 장비아이템 이미지

    // 2. 장비아이템 분류, 등급, 추가 옵션 등급, 강화 상태
    [SerializeField] GameObject m_gPanel_Equipslot_Equip_Information_Content_ItemInformation;
    [SerializeField] TextMeshProUGUI m_TMP_Equipslot_Equip_Information_Content_ItemInformation; // (텍스트) 장비아이템 분류, 등급, 추가 옵션 등급, 강화 상태

    [SerializeField] Button m_BTN_Equipslot_Equip_Information_Content_ChangeInformation_L; // (버튼) 장비아이템 정보 변경(L)
    [SerializeField] Button m_BTN_Equipslot_Equip_Information_Content_ChangeInformation_R; // (버튼) 장비아이템 정보 변경(R)

    // 3. 장비아이템 정보 - 장비아이템 설명
    [SerializeField] GameObject m_gPanel_Equipslot_Equip_Information_Content_ItemDescription;
    [SerializeField] GameObject m_gPanel_Equipslot_Equip_Information_Content_ItemDescription_Name;
    [SerializeField] TextMeshProUGUI m_TMP_Equipslot_Equip_Information_Content_ItemDescription_Name;     // (텍스트) "아이템 설명"
    [SerializeField] GameObject m_gPanel_Equipslot_Equip_Information_Content_ItemDescription_Content;
    [SerializeField] GameObject m_gSV_Equipslot_Equip_Information_Content_ItemDescription_Content;
    [SerializeField] GameObject m_gViewport_Equipslot_Equip_Information_Content_ItemDescription_Content;
    [SerializeField] TextMeshProUGUI m_TMP_Equipslot_Equip_Information_Content_ItemDescription_Content;  // (텍스트) 장비아이템 설명
    [SerializeField] Scrollbar m_Scrollbar_Equipslot_Equip_Information_Content_ItemDescription_Content;  // (스크롤바) 장비아이템 설명
    
    // 4. 장비아이템 정보 - 장비아이템 착용효과
    [SerializeField] GameObject m_gPanel_Equipslot_Equip_Information_Content_Effect;
    [SerializeField] GameObject m_gPanel_Equipslot_Equip_Information_Content_Effect_Name;
    [SerializeField] TextMeshProUGUI m_TMP_Equipslot_Equip_Information_Content_Effect_Name;     // (텍스트) "착용효과"
    [SerializeField] GameObject m_gPanel_Equipslot_Equip_Information_Content_Effect_Status;
    [SerializeField] TextMeshProUGUI m_TMP_Equipslot_Equip_Information_Content_Effect_Status_L; // (텍스트) 장비아이템 착용효과(스탯(능력치))_L
    [SerializeField] TextMeshProUGUI m_TMP_Equipslot_Equip_Information_Content_Effect_Status_R; // (텍스트) 장비아이템 착용효과(스탯(능력치))_R
    [SerializeField] GameObject m_gPanel_Equipslot_Equip_Information_Content_Effect_Soc;
    [SerializeField] TextMeshProUGUI m_TMP_Equipslot_Equip_Information_Content_Effect_Soc_L;    // (텍스트) 장비아이템 착용효과(스탯(평판))_L
    [SerializeField] TextMeshProUGUI m_TMP_Equipslot_Equip_Information_Content_Effect_Soc_R;    // (텍스트) 장비아이템 착용효과(스탯(평판))_R

    // 5. 장비아이템 정보 - 장비아이템 착용조건
    [SerializeField] GameObject m_gPanel_Equipslot_Equip_Information_Content_Condition;
    [SerializeField] GameObject m_gPanel_Equipslot_Equip_Information_Content_Condition_Name;
    [SerializeField] TextMeshProUGUI m_TMP_Equipslot_Equip_Information_Content_Condition_Name;     // (텍스트) "착용조건"
    [SerializeField] GameObject m_gPanel_Equipslot_Equip_Information_Content_Condition_Status;
    [SerializeField] TextMeshProUGUI m_TMP_Equipslot_Equip_Information_Content_Condition_Status_L; // (텍스트) 장비아이템 착용조건(스탯(능력치))_L
    [SerializeField] TextMeshProUGUI m_TMP_Equipslot_Equip_Information_Content_Condition_Status_R; // (텍스트) 장비아이템 착용조건(스탯(능력치))_R
    [SerializeField] GameObject m_gPanel_Equipslot_Equip_Information_Content_Condition_Soc;
    [SerializeField] TextMeshProUGUI m_TMP_Equipslot_Equip_Information_Content_Condition_Soc_L;    // (텍스트) 장비아이템 착용조건(스탯(평판))_L
    [SerializeField] TextMeshProUGUI m_TMP_Equipslot_Equip_Information_Content_Condition_Soc_R;    // (텍스트) 장비아이템 착용조건(스탯(평판))_R

    [SerializeField] GameObject m_gPanel_Equipslot_Equip_Information_EquipPossibility;
    [SerializeField] Button m_BTN_Equipslot_Equip_Information_EquipPossibility;          // (버튼) 장비아이템 착용 해제
    [SerializeField] TextMeshProUGUI m_TMP_Equipslot_Equip_Information_EquipPossibility; // (텍스트) "장비해제"



    // GUI 오브젝트 - 아이템 세트효과 세부 정보 GUI
    [SerializeField] GameObject m_gPanel_SetItemEffect;
    [SerializeField] GameObject m_gPanel_SetItemEffect_Name;
    [SerializeField] TextMeshProUGUI m_TMP_SetItemEffect_Name; // (텍스트) 아이템 세트효과 이름

    [SerializeField] GameObject m_gPanel_SetItemEffect_Content;

    [SerializeField] GameObject m_gPanel_SetItemEffect_Content_UpBar;
    [SerializeField] TextMeshProUGUI m_TMP_SetItemEffect_Content_UpBar_Name; // (텍스트) 아이템 세트효과 단계
    [SerializeField] GameObject m_gBTN_SetItemEffect_Content_UpBar_Left;
    [SerializeField] Button m_BTN_SetItemEffect_Content_UpBar_Left;          // (버튼) 아이템 세트효과 단계별 정보 변경(L)
    [SerializeField] GameObject m_gBTN_SetItemEffect_Content_UpBar_Right;
    [SerializeField] Button m_BTN_SetItemEffect_Content_UpBar_Right;         // (버튼) 아이템 세트효과 단계별 정보 변경(R)

    [SerializeField] GameObject m_gPanel_SetItemEffect_Content_SS;
    
    [SerializeField] GameObject m_gPanel_SetItemEffect_Content_SS_Description;
    [SerializeField] GameObject m_gSV_SetItemEffect_Content_SS_Description;
    [SerializeField] GameObject m_gViewport_SetItemEffect_Content_SS_Description;
    [SerializeField] TextMeshProUGUI m_TMP_SetItemEffect_Content_SS_Description; // (텍스트) 아이템 세트효과 설명
    [SerializeField] Scrollbar m_Scrollbar_SetItemEffect_Content_SS_Description; // (스크롤바) 아이템 세트효과 설명

    [SerializeField] GameObject m_gPanel_SetItemEffect_Content_SS_Status;
    [SerializeField] TextMeshProUGUI m_TMP_SetItemEffect_Content_SS_Status_L; // (텍스트) 아이템 세트효과(스탯(능력치))_L
    [SerializeField] TextMeshProUGUI m_TMP_SetItemEffect_Content_SS_Status_R; // (텍스트) 아이템 세트효과(스탯(능력치))_R
    
    [SerializeField] GameObject m_gPanel_SetItemEffect_Content_SS_Soc;
    [SerializeField] TextMeshProUGUI m_TMP_SetItemEffect_Content_SS_Soc_L;    // (텍스트) 아이템 세트효과(스탯(평판))_L
    [SerializeField] TextMeshProUGUI m_TMP_SetItemEffect_Content_SS_Soc_R;    // (텍스트) 아이템 세트효과(스탯(평판))_R

    ItemSetEffect m_ISE; // 장비아이템의 아이템 세트효과

    [SerializeField] List<int> m_nList_SetItemEffect_Code; // 아이템 세트효과 순서 목록
    // 아이템 세트효과 정보
    int m_nSetItemEffect_List_Index;                       // 아이템 세트효과 정보 순번. 아이템 세트효과 존재 판단에 사용되는 변수
    int m_nSetItemEffect_Current;                          // 아이템 세트효과 정보 순번. 현재 유저가 확인중인 아이템 세트효과 (Ex) [초보 모험가 세트효과] 3세트 정보 확인중)
    
    // 아이템 세트효과 적용 여부 판단 관련 변수
    int m_nPlayerEquipment_SetItemEffect_Current;          // 플레이어에게 적용중인 착용한 장비아이템과 동일한 아이템 세트효과 개수 (Ex) [초보 모험가 세트효과] 2세트 적용중)
    int m_nSetItemEffect_Count;                            // 아이템 세트효과 풀 세트 개수



    // 아이템 정보 타입 : { 효과, 조건, 설명 }
    public enum E_Equipslot_Equip_Information_PageNumber { EFFECT, CONDITION, DESCRIPTION }
    public E_Equipslot_Equip_Information_PageNumber m_eInformationPage; // 아이템 정보 타입

    // 장비아이템 착용 조건 + 아이템 정보 문자열 색 표시 관련 변수 
    public bool m_bEquip_Condition_Check; // 장비아이템 착용 가능 여부
                                          // m_bEquip_Condition_Check == true : 장비아이템 착용 불가능(장비아이템 착용 해제)
                                          // m_bEquip_Condition_Check == false : 장비아이템 착용 가능. 빨간색으로 표시
    bool m_bRefine_Condition_Check;       // 문자열 정제 관련 변수. 장비아이템 착용효과, 착용조건, 아이템 세트효과 등의 정보를 효율적으로 표시(강조)하기 위해 존재한다.
                                          // m_bRefine_Condition_Check == true : 문자열 정제 필요
                                          // m_bRefine_Condition_Check == false : 문자열 정제 불필요
                                          
    string m_sBuffer; // 문자열 임시 저장소. 표시할 정보가 많기에 임시 저장소를 사용한다.
    // 색 문자열 정보
    string m_sColor_White = "<color=#ffffff>";     // 흰색 - 이로운 효과, 부합한 조건
    string m_sColor_WhiteGray = "<color=#808080>"; // 회색 - 해로운 효과
    string m_sColor_Red = "<color=#FF0000>";       // 빨간색 - 부적합한 조건
    string m_sColor_Brown = "<color=#915446>";     // 갈색 - 존재하지 않는 효과, 조건
    string m_sColor_End = "</color>";              // 색상 문자열 끝

    Player_Status player; // 플레이어 스탯(능력치, 평판) 변수

    // GUI 초기 설정
    public void InitialSet()
    {
        InitialSet_Object(); // GUI 오브젝트 초기 설정
        InitialSet_Button(); // GUI 버튼 설정
        
        m_eInformationPage = E_Equipslot_Equip_Information_PageNumber.DESCRIPTION;
        
        player = Player_Total.Instance.m_ps_Status; // 플레이어 스탯(능력치, 평판) 설정
    }
    // GUI 오브젝트 초기 설정
    void InitialSet_Object()
    {
        m_gPanel_Equipslot_Equip_Information = GameObject.Find("Canvas_GUI").transform.Find("Panel_Equipslot_Equip_Information").gameObject;

        m_gPanel_Equipslot_Equip_Information_UpBar = m_gPanel_Equipslot_Equip_Information.transform.Find("Panel_Equipslot_Equip_Information_UpBar").gameObject;
        m_TMP_Equipslot_Equip_Information_UpBar = m_gPanel_Equipslot_Equip_Information_UpBar.transform.Find("TMP_Equipslot_Equip_Information_UpBar").gameObject.GetComponent<TextMeshProUGUI>();
        m_BTN_Equipslot_Equip_Information_UpBar_Exit = m_gPanel_Equipslot_Equip_Information_UpBar.transform.Find("BTN_Equipslot_Equip_Information_UpBar_Exit").gameObject.GetComponent<Button>();

        m_gPanel_Equipslot_Equip_Information_Content = m_gPanel_Equipslot_Equip_Information.transform.Find("Panel_Equipslot_Equip_Information_Content").gameObject;

        m_gPanel_Equipslot_Equip_Information_Content_Image = m_gPanel_Equipslot_Equip_Information_Content.transform.Find("Panel_Equipslot_Equip_Information_Content_Image").gameObject;
        m_gPanel_Equipslot_Equip_Information_Content_Image_ItemSprite = m_gPanel_Equipslot_Equip_Information_Content_Image.transform.Find("Panel_Equipslot_Equip_Information_Content_Image_ItemSprite").gameObject;
        m_IMG_Equipslot_Equip_Information_Content_Image_ItemSprite = m_gPanel_Equipslot_Equip_Information_Content_Image_ItemSprite.GetComponent<Image>();

        m_gPanel_Equipslot_Equip_Information_Content_ItemInformation = m_gPanel_Equipslot_Equip_Information_Content.transform.Find("Panel_Equipslot_Equip_Information_Content_ItemInformation").gameObject;
        m_TMP_Equipslot_Equip_Information_Content_ItemInformation = m_gPanel_Equipslot_Equip_Information_Content_ItemInformation.transform.Find("TMP_Equipslot_Equip_Information_Content_ItemInformation").gameObject.GetComponent<TextMeshProUGUI>();

        m_BTN_Equipslot_Equip_Information_Content_ChangeInformation_L = m_gPanel_Equipslot_Equip_Information_Content.transform.Find("BTN_Equipslot_Equip_Information_Content_ChangeInformation_L").gameObject.GetComponent<Button>();
        m_BTN_Equipslot_Equip_Information_Content_ChangeInformation_R = m_gPanel_Equipslot_Equip_Information_Content.transform.Find("BTN_Equipslot_Equip_Information_Content_ChangeInformation_R").gameObject.GetComponent<Button>();

        m_gPanel_Equipslot_Equip_Information_Content_ItemDescription = m_gPanel_Equipslot_Equip_Information_Content.transform.Find("Panel_Equipslot_Equip_Information_Content_ItemDescription").gameObject;
        m_gPanel_Equipslot_Equip_Information_Content_ItemDescription_Name = m_gPanel_Equipslot_Equip_Information_Content_ItemDescription.transform.Find("Panel_Equipslot_Equip_Information_Content_ItemDescription_Name").gameObject;
        m_TMP_Equipslot_Equip_Information_Content_ItemDescription_Name = m_gPanel_Equipslot_Equip_Information_Content_ItemDescription_Name.transform.Find("TMP_Equipslot_Equip_Information_Content_ItemDescription_Name").gameObject.GetComponent<TextMeshProUGUI>();
        m_gPanel_Equipslot_Equip_Information_Content_ItemDescription_Content = m_gPanel_Equipslot_Equip_Information_Content_ItemDescription.transform.Find("Panel_Equipslot_Equip_Information_Content_ItemDescription_Content").gameObject;
        m_gSV_Equipslot_Equip_Information_Content_ItemDescription_Content = m_gPanel_Equipslot_Equip_Information_Content_ItemDescription_Content.transform.Find("SV_Equipslot_Equip_Information_Content_ItemDescription_Content").gameObject;
        m_gViewport_Equipslot_Equip_Information_Content_ItemDescription_Content = m_gSV_Equipslot_Equip_Information_Content_ItemDescription_Content.transform.Find("Viewport_Equipslot_Equip_Information_Content_ItemDescription_Content").gameObject;
        m_TMP_Equipslot_Equip_Information_Content_ItemDescription_Content = m_gViewport_Equipslot_Equip_Information_Content_ItemDescription_Content.transform.Find("TMP_Equipslot_Equip_Information_Content_ItemDescription_Content").gameObject.GetComponent<TextMeshProUGUI>();
        m_Scrollbar_Equipslot_Equip_Information_Content_ItemDescription_Content = m_gSV_Equipslot_Equip_Information_Content_ItemDescription_Content.transform.Find("Scrollbar_Equipslot_Equip_Information_Content_ItemDescription_Content").gameObject.GetComponent<Scrollbar>();

        m_gPanel_Equipslot_Equip_Information_Content_Effect = m_gPanel_Equipslot_Equip_Information_Content.transform.Find("Panel_Equipslot_Equip_Information_Content_Effect").gameObject;
        m_gPanel_Equipslot_Equip_Information_Content_Effect_Name = m_gPanel_Equipslot_Equip_Information_Content_Effect.transform.Find("Panel_Equipslot_Equip_Information_Content_Effect_Name").gameObject;
        m_TMP_Equipslot_Equip_Information_Content_Effect_Name = m_gPanel_Equipslot_Equip_Information_Content_Effect_Name.transform.Find("TMP_Equipslot_Equip_Information_Content_Effect_Name").gameObject.GetComponent<TextMeshProUGUI>();
        m_gPanel_Equipslot_Equip_Information_Content_Effect_Status = m_gPanel_Equipslot_Equip_Information_Content_Effect.transform.Find("Panel_Equipslot_Equip_Information_Content_Effect_Status").gameObject;
        m_TMP_Equipslot_Equip_Information_Content_Effect_Status_L = m_gPanel_Equipslot_Equip_Information_Content_Effect_Status.transform.Find("TMP_Equipslot_Equip_Information_Content_Effect_Status_L").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_Equipslot_Equip_Information_Content_Effect_Status_R = m_gPanel_Equipslot_Equip_Information_Content_Effect_Status.transform.Find("TMP_Equipslot_Equip_Information_Content_Effect_Status_R").gameObject.GetComponent<TextMeshProUGUI>();
        m_gPanel_Equipslot_Equip_Information_Content_Effect_Soc = m_gPanel_Equipslot_Equip_Information_Content_Effect.transform.Find("Panel_Equipslot_Equip_Information_Content_Effect_Soc").gameObject;
        m_TMP_Equipslot_Equip_Information_Content_Effect_Soc_L = m_gPanel_Equipslot_Equip_Information_Content_Effect_Soc.transform.Find("TMP_Equipslot_Equip_Information_Content_Effect_Soc_L").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_Equipslot_Equip_Information_Content_Effect_Soc_R = m_gPanel_Equipslot_Equip_Information_Content_Effect_Soc.transform.Find("TMP_Equipslot_Equip_Information_Content_Effect_Soc_R").gameObject.GetComponent<TextMeshProUGUI>();

        m_gPanel_Equipslot_Equip_Information_Content_Condition = m_gPanel_Equipslot_Equip_Information_Content.transform.Find("Panel_Equipslot_Equip_Information_Content_Condition").gameObject;
        m_gPanel_Equipslot_Equip_Information_Content_Condition_Name = m_gPanel_Equipslot_Equip_Information_Content_Condition.transform.Find("Panel_Equipslot_Equip_Information_Content_Condition_Name").gameObject;
        m_TMP_Equipslot_Equip_Information_Content_Condition_Name = m_gPanel_Equipslot_Equip_Information_Content_Condition_Name.transform.Find("TMP_Equipslot_Equip_Information_Content_Condition_Name").gameObject.GetComponent<TextMeshProUGUI>();
        m_gPanel_Equipslot_Equip_Information_Content_Condition_Status = m_gPanel_Equipslot_Equip_Information_Content_Condition.transform.Find("Panel_Equipslot_Equip_Information_Content_Condition_Status").gameObject;
        m_TMP_Equipslot_Equip_Information_Content_Condition_Status_L = m_gPanel_Equipslot_Equip_Information_Content_Condition_Status.transform.Find("TMP_Equipslot_Equip_Information_Content_Condition_Status_L").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_Equipslot_Equip_Information_Content_Condition_Status_R = m_gPanel_Equipslot_Equip_Information_Content_Condition_Status.transform.Find("TMP_Equipslot_Equip_Information_Content_Condition_Status_R").gameObject.GetComponent<TextMeshProUGUI>();
        m_gPanel_Equipslot_Equip_Information_Content_Condition_Soc = m_gPanel_Equipslot_Equip_Information_Content_Condition.transform.Find("Panel_Equipslot_Equip_Information_Content_Condition_Soc").gameObject;
        m_TMP_Equipslot_Equip_Information_Content_Condition_Soc_L = m_gPanel_Equipslot_Equip_Information_Content_Condition_Soc.transform.Find("TMP_Equipslot_Equip_Information_Content_Condition_Soc_L").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_Equipslot_Equip_Information_Content_Condition_Soc_R = m_gPanel_Equipslot_Equip_Information_Content_Condition_Soc.transform.Find("TMP_Equipslot_Equip_Information_Content_Condition_Soc_R").gameObject.GetComponent<TextMeshProUGUI>();

        m_gPanel_Equipslot_Equip_Information_EquipPossibility = m_gPanel_Equipslot_Equip_Information.transform.Find("Panel_Equipslot_Equip_Information_EquipPossibility").gameObject;
        m_BTN_Equipslot_Equip_Information_EquipPossibility = m_gPanel_Equipslot_Equip_Information_EquipPossibility.transform.Find("BTN_Equipslot_Equip_Information_EquipPossibility").gameObject.GetComponent<Button>();
        m_TMP_Equipslot_Equip_Information_EquipPossibility = m_BTN_Equipslot_Equip_Information_EquipPossibility.transform.Find("TMP_Equipslot_Equip_Information_EquipPossibility").gameObject.GetComponent<TextMeshProUGUI>();



        m_gPanel_SetItemEffect = m_gPanel_Equipslot_Equip_Information.transform.Find("Panel_SetItemEffect").gameObject;
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
    // GUI 버튼 설정
    void InitialSet_Button()
    {
        // (버튼) 장비아이템 세부정보 GUI 비활성화 클릭 이벤트 함수 설정
        m_BTN_Equipslot_Equip_Information_UpBar_Exit.onClick.RemoveAllListeners();
        m_BTN_Equipslot_Equip_Information_UpBar_Exit.onClick.AddListener(delegate { Set_BTN_Exit(); });
        // (버튼) 장비아이템 정보 변경(L) 클릭 이벤트 함수 설정
        m_BTN_Equipslot_Equip_Information_Content_ChangeInformation_L.onClick.RemoveAllListeners();
        m_BTN_Equipslot_Equip_Information_Content_ChangeInformation_L.onClick.AddListener(delegate { Set_BTN_ChangeInformationPageNumber_L(); });
        // (버튼) 장비아이템 정보 변경(R) 클릭 이벤트 함수 설정
        m_BTN_Equipslot_Equip_Information_Content_ChangeInformation_R.onClick.RemoveAllListeners();
        m_BTN_Equipslot_Equip_Information_Content_ChangeInformation_R.onClick.AddListener(delegate { Set_BTN_ChangeInformationPageNumber_R(); });
    }
    
    // (버튼) 장비아이템 세부정보 GUI 비활성화 클릭 이벤트 함수 - 장비아이템 세부 정보 GUI를 비활성화한다.
    void Set_BTN_Exit()
    {
        m_gPanel_Equipslot_Equip_Information.SetActive(false);
    }
    
    // (버튼) 장비아이템 정보 변경(L) 클릭 이벤트 함수
    void Set_BTN_ChangeInformationPageNumber_L()
    {
        switch (m_eInformationPage)
        {
            case E_Equipslot_Equip_Information_PageNumber.EFFECT: // 장비아이템 착용효과 -> 장비아이템 설명
                {
                    m_eInformationPage = E_Equipslot_Equip_Information_PageNumber.DESCRIPTION;         // 아이템 정보 타입 = 설명
                    m_gPanel_Equipslot_Equip_Information_Content_Effect.SetActive(false);              // 장비아이템 착용효과 정보 비활성화
                    m_gPanel_Equipslot_Equip_Information_Content_Condition.SetActive(false);           // 장비아이템 착용조건 정보 비활성화
                    m_gPanel_Equipslot_Equip_Information_Content_ItemDescription.SetActive(true);      // 장비아이템 설명 정보 활성화
                    m_Scrollbar_Equipslot_Equip_Information_Content_ItemDescription_Content.value = 1; // (스크롤바) 장비아이템 설명 정보 초기화
                    m_Scrollbar_SetItemEffect_Content_SS_Description.value = 1;                        // (스크롤바) 아이템 세트효과 정보 초기화
                }
                break;
            case E_Equipslot_Equip_Information_PageNumber.CONDITION: // 장비아이템 착용조건 -> 장비아이템 착용효과
                {
                    m_eInformationPage = E_Equipslot_Equip_Information_PageNumber.EFFECT;          // 아이템 정보 타입 = 효과
                    m_gPanel_Equipslot_Equip_Information_Content_Effect.SetActive(true);           // 장비아이템 착용효과 정보 활성화
                    m_gPanel_Equipslot_Equip_Information_Content_Condition.SetActive(false);       // 장비아이템 착용효과 정보 비활성화
                    m_gPanel_Equipslot_Equip_Information_Content_ItemDescription.SetActive(false); // 장비아이템 설명 정보 비활성화
                }
                break;
            case E_Equipslot_Equip_Information_PageNumber.DESCRIPTION: // 장비아이템 설명 -> 장비아이템 착용조건
                {
                    m_eInformationPage = E_Equipslot_Equip_Information_PageNumber.CONDITION;       // 아이템 정보 타입 = 조건
                    m_gPanel_Equipslot_Equip_Information_Content_Effect.SetActive(false);          // 장비아이템 착용효과 정보 비활성화
                    m_gPanel_Equipslot_Equip_Information_Content_Condition.SetActive(true);        // 장비아이템 착용효과 정보 활성화
                    m_gPanel_Equipslot_Equip_Information_Content_ItemDescription.SetActive(false); // 장비아이템 설명 정보 비활성화
                }
                break;
        }
    }
    // (버튼) 장비아이템 정보 변경(R) 클릭 이벤트 함수
    void Set_BTN_ChangeInformationPageNumber_R()
    {
        switch (m_eInformationPage)
        {
            case E_Equipslot_Equip_Information_PageNumber.EFFECT: // 장비아이템 착용효과 -> 장비아이템 착용조건
                {
                    m_eInformationPage = E_Equipslot_Equip_Information_PageNumber.CONDITION;       // 아이템 정보 타입 = 조건
                    m_gPanel_Equipslot_Equip_Information_Content_Effect.SetActive(false);          // 장비아이템 착용효과 정보 비활성화
                    m_gPanel_Equipslot_Equip_Information_Content_Condition.SetActive(true);        // 장비아이템 착용효과 정보 활성화
                    m_gPanel_Equipslot_Equip_Information_Content_ItemDescription.SetActive(false); // 장비아이템 설명 정보 비활성화
                }
                break;
            case E_Equipslot_Equip_Information_PageNumber.CONDITION: // 장비아이템 착용조건 -> 장비아이템 설명
                {
                    m_eInformationPage = E_Equipslot_Equip_Information_PageNumber.DESCRIPTION;         // 아이템 정보 타입 = 설명
                    m_gPanel_Equipslot_Equip_Information_Content_Effect.SetActive(false);              // 장비아이템 착용효과 정보 비활성화
                    m_gPanel_Equipslot_Equip_Information_Content_Condition.SetActive(false);           // 장비아이템 착용효과 정보 비활성화
                    m_gPanel_Equipslot_Equip_Information_Content_ItemDescription.SetActive(true);      // 장비아이템 설명 정보 활성화
                    m_Scrollbar_Equipslot_Equip_Information_Content_ItemDescription_Content.value = 1; // (스크롤바) 장비아이템 설명 정보 초기화
                    m_Scrollbar_SetItemEffect_Content_SS_Description.value = 1;                        // (스크롤바) 아이템 세트효과 정보 초기화
                }
                break;
            case E_Equipslot_Equip_Information_PageNumber.DESCRIPTION: // 장비아이템 설명 -> 장비아이템 착용효과
                {
                    m_eInformationPage = E_Equipslot_Equip_Information_PageNumber.EFFECT;          // 아이템 정보 타입 = 효과
                    m_gPanel_Equipslot_Equip_Information_Content_Effect.SetActive(true);           // 장비아이템 착용효과 정보 활성화
                    m_gPanel_Equipslot_Equip_Information_Content_Condition.SetActive(false);       // 장비아이템 착용효과 정보 비활성화
                    m_gPanel_Equipslot_Equip_Information_Content_ItemDescription.SetActive(false); // 장비아이템 설명 정보 비활성화
                }
                break;
        }
    }
    
    // (버튼) 장비아이템 착용 해제 클릭 이벤트 함수
    void Set_BTN_Equip_Remove(int arynumber) // arynumber : 장비창 슬롯 고유코드
    {
        switch(arynumber)
        {
            case 0:
                {
                    Player_Total.Instance.Remove_Item_Equip(Player_Equipment.m_gEquipment_Hat); // 장비아이템(모자) 착용 해제 관련 함수
                } break;
            case 1:
                {
                    Player_Total.Instance.Remove_Item_Equip(Player_Equipment.m_gEquipment_Top); // 장비아이템(상의) 착용 해제 관련 함수
                }
                break;
            case 2:
                {
                    Player_Total.Instance.Remove_Item_Equip(Player_Equipment.m_gEquipment_Bottoms); // 장비아이템(하의) 착용 해제 관련 함수
                }
                break;
            case 3:
                {
                    Player_Total.Instance.Remove_Item_Equip(Player_Equipment.m_gEquipment_Shose); // 장비아이템(신발) 착용 해제 관련 함수
                }
                break;
            case 4:
                {
                    Player_Total.Instance.Remove_Item_Equip(Player_Equipment.m_gEquipment_Gloves); // 장비아이템(장갑) 착용 해제 관련 함수
                }
                break;
            case 5:
                {
                    Player_Total.Instance.Remove_Item_Equip(Player_Equipment.m_gEquipment_Mainweapon); // 장비아이템(주무기) 착용 해제 관련 함수
                }
                break;
            case 6:
                {
                    Player_Total.Instance.Remove_Item_Equip(Player_Equipment.m_gEquipment_Subweapon); // 장비아이템(보조무기) 착용 해제 관련 함수
                }
                break;
        }

        m_gPanel_Equipslot_Equip_Information.SetActive(false); // 장비아이템 세부 정보 GUI 비활성화
    }
    
    // (버튼) 아이템 세트효과 단계별 정보 변경(L) 클릭 이벤트 함수
    void Set_BTN_SetItemEffect_UpBar_Left()
    {
        m_nSetItemEffect_List_Index -= 1;
        m_nSetItemEffect_Current = m_nList_SetItemEffect_Code[m_nSetItemEffect_List_Index];

        UpdateItemEquipInformation_SetItemEffect_UpBar();   // 아이템 세트효과 세부 정보 GUI 업데이트 - (버튼) 아이템 세트효과 단계별 정보 변경(L, R) 활성화 / 비활성화 여부 판단
        UpdateItemEquipInformation_SetItemEffect_Content(); // 아이템 세트효과 세부 정보 GUI 업데이트 - 아이템 세트효과 정보
    }
    // (버튼) 아이템 세트효과 단계별 정보 변경(R) 클릭 이벤트 함수
    void Set_BTN_SetItemEffect_UpBar_Right()
    {
        m_nSetItemEffect_List_Index += 1;
        m_nSetItemEffect_Current = m_nList_SetItemEffect_Code[m_nSetItemEffect_List_Index];

        UpdateItemEquipInformation_SetItemEffect_UpBar();   // 아이템 세트효과 세부 정보 GUI 업데이트 - (버튼) 아이템 세트효과 단계별 정보 변경(L, R) 활성화 / 비활성화 여부 판단
        UpdateItemEquipInformation_SetItemEffect_Content(); // 아이템 세트효과 세부 정보 GUI 업데이트 - 아이템 세트효과 정보
    }

    // 장비아이템 세부 정보 GUI 활성화 함수
    public void Display_GUI_Equipslot_Equip_Information(float fcoordination_x, float fcoordination_y) // fcoordination_x, y : 장비아이템 세부 정보 GUI 좌표(x, y)
    {
        m_gPanel_Equipslot_Equip_Information.SetActive(true);
        m_gPanel_Equipslot_Equip_Information.transform.SetAsLastSibling();
        m_gPanel_Equipslot_Equip_Information.transform.position = new Vector2(fcoordination_x, fcoordination_y); // 장비아이템 세부 정보 GUI 좌표 설정
    }
    // 장비아이템 세부 정보 GUI 비활성화 함수
    public void UnDisplay_GUI_Equipslot_Equip_Information()
    {
        m_gPanel_Equipslot_Equip_Information.SetActive(false);
    }

    // 장비아이템 세부 정보 GUI 업데이트
    public void UpdateItemEquipInformation(Item_Equip item, int arynumber) // item : 장비아이템, arynumber : 장비창 슬롯 고유코드
    {
        GUIManager_Total.Instance.m_GUI_Equipslot_Equip_Information.m_bEquip_Condition_Check = true; // 장비아이템 착용 가능
        
        m_Scrollbar_Equipslot_Equip_Information_Content_ItemDescription_Content.value = 1; // (스크롤바) 장비아이템 설명 정보 초기화
        m_Scrollbar_SetItemEffect_Content_SS_Description.value = 1;                        // (스크롤바) 아이템 세트효과 정보 초기화
        
        UpdateItemEquipInformation_UpBar(item);                  // 장비아이템 세부 정보 GUI 업데이트 - 장비아이템 이름, 강화 상태
        UpdateItemEquipInformation_Content(item);                // 장비아이템 세부 정보 GUI 업데이트 - 장비아이템 정보
        UpdateItemEquipInformation_EquipRemove(item, arynumber); // 장비아이템 세부 정보 GUI 업데이트 - 장비아이템 착용 해제
        UpdateItemEquipInformation_SetItemEffect(item);          // 장비아이템 세부 정보 GUI 업데이트 - 아이템 세트효과 정보
    }
    // 장비아이템 세부 정보 GUI 업데이트 - 장비아이템 이름, 강화 상태
    void UpdateItemEquipInformation_UpBar(Item_Equip item) // item : 장비아이템
    {
        m_TMP_Equipslot_Equip_Information_UpBar.text = item.m_sItemName;
        
        if (item.m_nReinforcementCount_Current > 0) // 장비아이템이 강화된 경우(장비아이템 현재 강화 횟수 > 0)
            m_TMP_Equipslot_Equip_Information_UpBar.text += " + " + item.m_nReinforcementCount_Current;
    }
    // 장비아이템 세부 정보 GUI 업데이트 - 장비아이템 정보
    void UpdateItemEquipInformation_Content(Item_Equip item) // item : 장비아이템
    {
        // 장비아이템 이미지 설정
        m_IMG_Equipslot_Equip_Information_Content_Image_ItemSprite.sprite = item.m_sp_Sprite;
        m_IMG_Equipslot_Equip_Information_Content_Image_ItemSprite.color = new Color(1, 1, 1, 0.75f);

        m_TMP_Equipslot_Equip_Information_Content_ItemInformation.text = ""; // 초기화
        
        // 장비아이템 분류 설정
        if (item.m_eItemEquipType == E_ITEM_EQUIP_TYPE.MAINWEAPON) // 장비아이템 타입이 주무기인 경우
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
        else // 장비아이템 타입이 주무기가 아닌 경우
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
        m_TMP_Equipslot_Equip_Information_Content_ItemInformation.text += "분류: " + m_sBuffer + "\n";
        
        // 장비아이템 등급 설정
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
        m_TMP_Equipslot_Equip_Information_Content_ItemInformation.text += "등급: " + m_sBuffer + "\n";
        
        // 장비아이템 추가 옵션 등급 설정
        m_TMP_Equipslot_Equip_Information_Content_ItemInformation.text += "추가 옵션 등급: " + item.m_eItemEquip_SpecialRatio_STATUS + " / " + item.m_eItemEquip_SpecialRatio_SOC + "\n";
        
        // 장비아이템 강화 상태 설정
        m_TMP_Equipslot_Equip_Information_Content_ItemInformation.text += "강화 상태: " + item.m_nReinforcementCount_Current + " / " + item.m_nReinforcementCount_Max;

        // 장비아이템 설명 설정
        m_TMP_Equipslot_Equip_Information_Content_ItemDescription_Content.text = "";
        m_TMP_Equipslot_Equip_Information_Content_ItemDescription_Content.text += item.GetItemDescription();

        // 장비아이템 착용효과(스탯(능력치)) 설정
        m_TMP_Equipslot_Equip_Information_Content_Effect_Status_L.text = "";
        m_TMP_Equipslot_Equip_Information_Content_Effect_Status_L.text += Refine_Condition("레        벨:", item.m_sStatus_Effect.GetSTATUS_LV(), item.m_STATUS_AdditionalOption.GetSTATUS_LV(), item.m_STATUS_ReinforcementValue.GetSTATUS_LV());
        m_TMP_Equipslot_Equip_Information_Content_Effect_Status_L.text += Refine_Condition("\n체        력:", item.m_sStatus_Effect.GetSTATUS_HP_Max(), item.m_STATUS_AdditionalOption.GetSTATUS_HP_Max(), item.m_STATUS_ReinforcementValue.GetSTATUS_HP_Max());
        m_TMP_Equipslot_Equip_Information_Content_Effect_Status_L.text += Refine_Condition("\n데  미  지:", item.m_sStatus_Effect.GetSTATUS_Damage_Total(), item.m_STATUS_AdditionalOption.GetSTATUS_Damage_Total(), item.m_STATUS_ReinforcementValue.GetSTATUS_Damage_Total());
        m_TMP_Equipslot_Equip_Information_Content_Effect_Status_L.text += Refine_Condition("\n이동속도:", item.m_sStatus_Effect.GetSTATUS_Speed(), item.m_STATUS_AdditionalOption.GetSTATUS_Speed(), item.m_STATUS_ReinforcementValue.GetSTATUS_Speed());
        m_TMP_Equipslot_Equip_Information_Content_Effect_Status_R.text = "";
        m_TMP_Equipslot_Equip_Information_Content_Effect_Status_R.text += "\n";
        m_TMP_Equipslot_Equip_Information_Content_Effect_Status_R.text += Refine_Condition("마        나:", item.m_sStatus_Effect.GetSTATUS_MP_Max(), item.m_STATUS_AdditionalOption.GetSTATUS_MP_Max(), item.m_STATUS_ReinforcementValue.GetSTATUS_MP_Max());
        m_TMP_Equipslot_Equip_Information_Content_Effect_Status_R.text += Refine_Condition("\n방  어  력:", item.m_sStatus_Effect.GetSTATUS_Defence_Physical(), item.m_STATUS_AdditionalOption.GetSTATUS_Defence_Physical(), item.m_STATUS_ReinforcementValue.GetSTATUS_Defence_Physical());
        m_TMP_Equipslot_Equip_Information_Content_Effect_Status_R.text += Refine_Condition("\n공격속도:", item.m_sStatus_Effect.GetSTATUS_AttackSpeed(), item.m_STATUS_AdditionalOption.GetSTATUS_AttackSpeed(), item.m_STATUS_ReinforcementValue.GetSTATUS_AttackSpeed());
        
        // 장비아이템 착용효과(스탯(평판)) 설정
        m_TMP_Equipslot_Equip_Information_Content_Effect_Soc_L.text = "";
        m_TMP_Equipslot_Equip_Information_Content_Effect_Soc_L.text += Refine_Condition("명        예:", item.m_sSoc_Effect.GetSOC_Honor(), item.m_SOC_AdditionalOption.GetSOC_Honor(), item.m_SOC_ReinforcementValue.GetSOC_Honor());
        m_TMP_Equipslot_Equip_Information_Content_Effect_Soc_L.text += Refine_Condition("\n인        간:", item.m_sSoc_Effect.GetSOC_Human(), item.m_SOC_AdditionalOption.GetSOC_Human(), item.m_SOC_ReinforcementValue.GetSOC_Human());
        m_TMP_Equipslot_Equip_Information_Content_Effect_Soc_L.text += Refine_Condition("\n동        물:", item.m_sSoc_Effect.GetSOC_Animal(), item.m_SOC_AdditionalOption.GetSOC_Animal(), item.m_SOC_ReinforcementValue.GetSOC_Animal());
        m_TMP_Equipslot_Equip_Information_Content_Effect_Soc_L.text += Refine_Condition("\n슬  라  임:", item.m_sSoc_Effect.GetSOC_Slime(), item.m_SOC_AdditionalOption.GetSOC_Slime(), item.m_SOC_ReinforcementValue.GetSOC_Slime());
        m_TMP_Equipslot_Equip_Information_Content_Effect_Soc_L.text += Refine_Condition("\n스켈레톤:", item.m_sSoc_Effect.GetSOC_Skeleton(), item.m_SOC_AdditionalOption.GetSOC_Skeleton(), item.m_SOC_ReinforcementValue.GetSOC_Skeleton());
        m_TMP_Equipslot_Equip_Information_Content_Effect_Soc_R.text = "";
        m_TMP_Equipslot_Equip_Information_Content_Effect_Soc_R.text += "\n";
        m_TMP_Equipslot_Equip_Information_Content_Effect_Soc_R.text += Refine_Condition("앤        트:", item.m_sSoc_Effect.GetSOC_Ents(), item.m_SOC_AdditionalOption.GetSOC_Ents(), item.m_SOC_ReinforcementValue.GetSOC_Ents());
        m_TMP_Equipslot_Equip_Information_Content_Effect_Soc_R.text += Refine_Condition("\n마        족:", item.m_sSoc_Effect.GetSOC_Devil(), item.m_SOC_AdditionalOption.GetSOC_Devil(), item.m_SOC_ReinforcementValue.GetSOC_Devil());
        m_TMP_Equipslot_Equip_Information_Content_Effect_Soc_R.text += Refine_Condition("\n용        족:", item.m_sSoc_Effect.GetSOC_Dragon(), item.m_SOC_AdditionalOption.GetSOC_Dragon(), item.m_SOC_ReinforcementValue.GetSOC_Dragon());
        m_TMP_Equipslot_Equip_Information_Content_Effect_Soc_R.text += Refine_Condition("\n어        둠:", item.m_sSoc_Effect.GetSOC_Shadow(), item.m_SOC_AdditionalOption.GetSOC_Shadow(), item.m_SOC_ReinforcementValue.GetSOC_Shadow());

        // 장비아이템 착용조건(스탯(능력치)) 설정
        m_TMP_Equipslot_Equip_Information_Content_Condition_Status_L.text = "";
        m_TMP_Equipslot_Equip_Information_Content_Condition_Status_L.text += Refine_Condition("레        벨: ", "\n", item.m_sStatus_Limit_Min.GetSTATUS_LV(), item.m_sStatus_Limit_Max.GetSTATUS_LV(), player.m_sStatus.GetSTATUS_LV());
        m_TMP_Equipslot_Equip_Information_Content_Condition_Status_L.text += Refine_Condition("체        력: ", "\n", item.m_sStatus_Limit_Min.GetSTATUS_HP_Max(), item.m_sStatus_Limit_Max.GetSTATUS_HP_Max(), player.m_sStatus.GetSTATUS_HP_Max());
        m_TMP_Equipslot_Equip_Information_Content_Condition_Status_L.text += Refine_Condition("데  미  지: ", "\n", item.m_sStatus_Limit_Min.GetSTATUS_Damage_Total(), item.m_sStatus_Limit_Max.GetSTATUS_Damage_Total(), player.m_sStatus.GetSTATUS_Damage_Total());
        m_TMP_Equipslot_Equip_Information_Content_Condition_Status_L.text += Refine_Condition("이동속도: ", "", item.m_sStatus_Limit_Min.GetSTATUS_Speed(), item.m_sStatus_Limit_Max.GetSTATUS_Speed(), player.m_sStatus.GetSTATUS_Speed());
        m_TMP_Equipslot_Equip_Information_Content_Condition_Status_R.text = "";
        m_TMP_Equipslot_Equip_Information_Content_Condition_Status_R.text += "\n";
        m_TMP_Equipslot_Equip_Information_Content_Condition_Status_R.text += Refine_Condition("마        나: ", "\n", item.m_sStatus_Limit_Min.GetSTATUS_MP_Max(), item.m_sStatus_Limit_Max.GetSTATUS_MP_Max(), player.m_sStatus.GetSTATUS_MP_Max());
        m_TMP_Equipslot_Equip_Information_Content_Condition_Status_R.text += Refine_Condition("방  어  력: ", "\n", item.m_sStatus_Limit_Min.GetSTATUS_Defence_Physical(), item.m_sStatus_Limit_Max.GetSTATUS_Defence_Physical(), player.m_sStatus.GetSTATUS_Defence_Physical());
        m_TMP_Equipslot_Equip_Information_Content_Condition_Status_R.text += Refine_Condition("공격속도: ", "", item.m_sStatus_Limit_Min.GetSTATUS_AttackSpeed(), item.m_sStatus_Limit_Max.GetSTATUS_AttackSpeed(), player.m_sStatus.GetSTATUS_AttackSpeed());

        // 장비아이템 착용조건(스탯(평판)) 설정
        m_TMP_Equipslot_Equip_Information_Content_Condition_Soc_L.text = "";
        m_TMP_Equipslot_Equip_Information_Content_Condition_Soc_L.text += Refine_Condition("명        예: ", "\n", item.m_sSoc_Limit_Min.GetSOC_Honor(), item.m_sSoc_Limit_Max.GetSOC_Honor(), player.m_sSoc.GetSOC_Honor());
        m_TMP_Equipslot_Equip_Information_Content_Condition_Soc_L.text += Refine_Condition("인        간: ", "\n", item.m_sSoc_Limit_Min.GetSOC_Human(), item.m_sSoc_Limit_Max.GetSOC_Human(), player.m_sSoc.GetSOC_Human());
        m_TMP_Equipslot_Equip_Information_Content_Condition_Soc_L.text += Refine_Condition("동        물: ", "\n", item.m_sSoc_Limit_Min.GetSOC_Animal(), item.m_sSoc_Limit_Max.GetSOC_Animal(), player.m_sSoc.GetSOC_Animal());
        m_TMP_Equipslot_Equip_Information_Content_Condition_Soc_L.text += Refine_Condition("슬  라  임: ", "\n", item.m_sSoc_Limit_Min.GetSOC_Slime(), item.m_sSoc_Limit_Max.GetSOC_Slime(), player.m_sSoc.GetSOC_Slime());
        m_TMP_Equipslot_Equip_Information_Content_Condition_Soc_L.text += Refine_Condition("스켈레톤: ", "", item.m_sSoc_Limit_Min.GetSOC_Skeleton(), item.m_sSoc_Limit_Max.GetSOC_Skeleton(), player.m_sSoc.GetSOC_Skeleton());
        m_TMP_Equipslot_Equip_Information_Content_Condition_Soc_R.text = "";
        m_TMP_Equipslot_Equip_Information_Content_Condition_Soc_R.text += "\n";
        m_TMP_Equipslot_Equip_Information_Content_Condition_Soc_R.text += Refine_Condition("앤        트: ", "\n", item.m_sSoc_Limit_Min.GetSOC_Ents(), item.m_sSoc_Limit_Max.GetSOC_Ents(), player.m_sSoc.GetSOC_Ents());
        m_TMP_Equipslot_Equip_Information_Content_Condition_Soc_R.text += Refine_Condition("마        족: ", "\n", item.m_sSoc_Limit_Min.GetSOC_Devil(), item.m_sSoc_Limit_Max.GetSOC_Devil(), player.m_sSoc.GetSOC_Devil());
        m_TMP_Equipslot_Equip_Information_Content_Condition_Soc_R.text += Refine_Condition("용        족: ", "\n", item.m_sSoc_Limit_Min.GetSOC_Dragon(), item.m_sSoc_Limit_Max.GetSOC_Dragon(), player.m_sSoc.GetSOC_Dragon());
        m_TMP_Equipslot_Equip_Information_Content_Condition_Soc_R.text += Refine_Condition("어        듬: ", "", item.m_sSoc_Limit_Min.GetSOC_Shadow(), item.m_sSoc_Limit_Max.GetSOC_Shadow(), player.m_sSoc.GetSOC_Shadow());
    }
    // 장비아이템 세부 정보 GUI 업데이트 - 장비아이템 착용 해제
    void UpdateItemEquipInformation_EquipRemove(Item_Equip item, int arynumber) // item : 장비아이템, arynumber : 장비창 슬롯 고유코드
    {
        m_TMP_Equipslot_Equip_Information_EquipPossibility.text = m_sColor_White + "장비해제" + m_sColor_End;
        // (버튼) 장비아이템 착용 해제 클릭 이벤트 함수 설정
        m_BTN_Equipslot_Equip_Information_EquipPossibility.onClick.RemoveAllListeners();
        m_BTN_Equipslot_Equip_Information_EquipPossibility.onClick.AddListener(delegate { Set_BTN_Equip_Remove(arynumber); });
    }
    // 장비아이템 세부 정보 GUI 업데이트 - 아이템 세트효과 정보
    void UpdateItemEquipInformation_SetItemEffect(Item_Equip item) // item : 장비아이템
    {
        m_nList_SetItemEffect_Code.Clear(); // 아이템 세트효과 순서 목록 초기화
        int setitemeffectnumber = ItemSetEffectManager.instance.Return_SetItemEffect(item.m_nItemCode); // 아이템 세트효과 고유코드
        if (setitemeffectnumber == 0) // 장비아이템의 아이템 세트효과가 존재하지 않는 경우
        {
            m_gPanel_SetItemEffect.SetActive(false); // 아이템 세트효과 세부 정보 GUI 비활성화

            m_ISE = null;
        }
        else // 장비아이템의 아이템 세트효과가 존재하는 경우
        {
            m_gPanel_SetItemEffect.SetActive(true);

            m_ISE = ItemSetEffectManager.m_Dictionary_ItemSetEffect[setitemeffectnumber]; // 장비아이템의 아이템 세트효과 설정

            m_TMP_SetItemEffect_Name.text = m_ISE.m_sItemSetEffect_Name;

            m_nSetItemEffect_Count = m_ISE.m_Dictionary_Item_Equip_Code.Count; // 아이템 세트효과 풀 세트 개수 설정
            for (int i = 0; i < m_nSetItemEffect_Count; i++)
            {
                if (m_ISE.m_Dictionary_STATUS_Effect[i + 1].CheckIdentity(new STATUS(0)) == false ||
                    m_ISE.m_Dictionary_SOC_Effect[i + 1].CheckIdentity(new SOC(0)) == false) // 아이템 세트효과가 존재하는 경우
                {
                    m_nList_SetItemEffect_Code.Add(i + 1); // 아이템 세트효과 순서 목록 추가(설정)
                }
            }

            // 아이템 세트효과 세부 정보 GUI 초기 설정
            m_nSetItemEffect_Current = m_nList_SetItemEffect_Code[0];
            m_nSetItemEffect_List_Index = 0;
            m_TMP_SetItemEffect_Content_UpBar_Name.text = m_nList_SetItemEffect_Code[m_nSetItemEffect_List_Index].ToString() + "개 아이템 세트 효과";
            
            UpdateItemEquipInformation_SetItemEffect_UpBar();   // 아이템 세트효과 세부 정보 GUI 업데이트 - (버튼) 아이템 세트효과 단계별 정보 변경(L, R) 활성화 / 비활성화 여부 판단
            UpdateItemEquipInformation_SetItemEffect_Content(); // 아이템 세트효과 세부 정보 GUI 업데이트 - 아이템 세트효과 정보

            // (버튼) 아이템 세트효과 단계별 정보 변경(L) 클릭 이벤트 함수 설정
            m_BTN_SetItemEffect_Content_UpBar_Left.onClick.RemoveAllListeners();
            m_BTN_SetItemEffect_Content_UpBar_Left.onClick.AddListener(delegate { Set_BTN_SetItemEffect_UpBar_Left(); });
            // (버튼) 아이템 세트효과 단계별 정보 변경(R) 클릭 이벤트 함수 설정
            m_BTN_SetItemEffect_Content_UpBar_Right.onClick.RemoveAllListeners();
            m_BTN_SetItemEffect_Content_UpBar_Right.onClick.AddListener(delegate { Set_BTN_SetItemEffect_UpBar_Right(); });
        }
    }
    // 아이템 세트효과 세부 정보 GUI 업데이트 - (버튼) 아이템 세트효과 단계별 정보 변경(L, R) 활성화 / 비활성화 여부 판단
    void UpdateItemEquipInformation_SetItemEffect_UpBar()
    {
        if (m_nSetItemEffect_Current == m_nList_SetItemEffect_Code[0]) // 이전 단계의 아이템 세트효과가 존재하지 않는 경우
        {
            m_gBTN_SetItemEffect_Content_UpBar_Left.SetActive(false); // (버튼) 아이템 세트효과 단계별 정보 변경(L) 비활성화
        }
        else // 이전 단계의 아이템 세트효과가 존재하는 경우
        {
            m_gBTN_SetItemEffect_Content_UpBar_Left.SetActive(true); // (버튼) 아이템 세트효과 단계별 정보 변경(L) 활성화
        }
        if (m_nSetItemEffect_Current == m_nList_SetItemEffect_Code[m_nList_SetItemEffect_Code.Count - 1]) // 다음 단계의 아이템 세트효과가 존재하지 않는 경우
        {
            m_gBTN_SetItemEffect_Content_UpBar_Right.SetActive(false); // (버튼) 아이템 세트효과 단계별 정보 변경(R) 비활성화
        }
        else // 다음 단계의 아이템 세트효과가 존재하는 경우
        {
            m_gBTN_SetItemEffect_Content_UpBar_Right.SetActive(true); // (버튼) 아이템 세트효과 단계별 정보 변경(R) 활성화
        }
    }
    // 아이템 세트효과 세부 정보 GUI 업데이트 - 아이템 세트효과 정보
    void UpdateItemEquipInformation_SetItemEffect_Content()
    {
        // 아이템 세트효과 이름 설정
        m_TMP_SetItemEffect_Content_UpBar_Name.text = Refine_Condition(m_nList_SetItemEffect_Code[m_nSetItemEffect_List_Index].ToString() + "개 아이템 세트 효과");

        // 아이템 세트효과 설명 설정
        m_TMP_SetItemEffect_Content_SS_Description.text = Refine_Condition(m_ISE.m_Dictionary_Description[m_nList_SetItemEffect_Code[m_nSetItemEffect_List_Index]], false);

        // 아이템 세트효과 효과(스탯(능력치)) 설정
        m_TMP_SetItemEffect_Content_SS_Status_L.text = Refine_Condition("레        벨:", m_ISE.m_Dictionary_STATUS_Effect[m_nList_SetItemEffect_Code[m_nSetItemEffect_List_Index]].GetSTATUS_LV());
        m_TMP_SetItemEffect_Content_SS_Status_L.text += Refine_Condition("\n체        력:", m_ISE.m_Dictionary_STATUS_Effect[m_nList_SetItemEffect_Code[m_nSetItemEffect_List_Index]].GetSTATUS_HP_Max());
        m_TMP_SetItemEffect_Content_SS_Status_L.text += Refine_Condition("\n데  미  지:", m_ISE.m_Dictionary_STATUS_Effect[m_nList_SetItemEffect_Code[m_nSetItemEffect_List_Index]].GetSTATUS_Damage_Total());
        m_TMP_SetItemEffect_Content_SS_Status_L.text += Refine_Condition("\n이동속도:", m_ISE.m_Dictionary_STATUS_Effect[m_nList_SetItemEffect_Code[m_nSetItemEffect_List_Index]].GetSTATUS_Speed());
        m_TMP_SetItemEffect_Content_SS_Status_R.text = "";
        m_TMP_SetItemEffect_Content_SS_Status_R.text += Refine_Condition("\n마        나:", m_ISE.m_Dictionary_STATUS_Effect[m_nList_SetItemEffect_Code[m_nSetItemEffect_List_Index]].GetSTATUS_MP_Max());
        m_TMP_SetItemEffect_Content_SS_Status_R.text += Refine_Condition("\n방  어  력:", m_ISE.m_Dictionary_STATUS_Effect[m_nList_SetItemEffect_Code[m_nSetItemEffect_List_Index]].GetSTATUS_Defence_Physical());
        m_TMP_SetItemEffect_Content_SS_Status_R.text += Refine_Condition("\n공격속도:", m_ISE.m_Dictionary_STATUS_Effect[m_nList_SetItemEffect_Code[m_nSetItemEffect_List_Index]].GetSTATUS_AttackSpeed());
        
        // 아이템 세트효과 효과(스탯(평판)) 설정
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

    // 텍스트(문자열) 정제 함수. 장비아이템 착용효과, 착용조건, 아이템 세트효과 등의 정보를 효율적으로 표시(강조)한다.
    // ~(물결표)를 이용해 장비아이템의 착용조건을 표시한다. 스탯(능력치, 평판) 최대 상한(+10000)ㆍ최소 하한(-10000)은 표시하지 않는다.
    
    // 텍스트(문자열) 정제 함수 - 장비아이템 착용조건
    string Refine_Condition(string beforesentence, string aftersentence, float condition_min, float condition_max, float current_condition) // beforesentence : 이전 문장, aftersentence : 이후 문장,
                                                                                                                                            // condition_min : 장비아이템 착용조건 - 스탯(능력치, 평판) 하한,
                                                                                                                                            // condition_max : 장비아이템 착용조건 - 스탯(능력치, 평판) 상한,
                                                                                                                                            // current_condition : 플레이어 스탯(능력치, 평판)
    {
        if (condition_min == -10000 && condition_max == 10000) // 별도의 스탯(능력치, 평판) 효과, 조건 등이 존재하지 않는 경우
            m_bRefine_Condition_Check = false; // 문자열 정제 불필요
        else // 별도의 스탯(능력치, 평판) 효과, 조건 등이 존재하는 경우
            m_bRefine_Condition_Check = true; // 문자열 정제 필요

        m_sBuffer = ""; // 문자열 임시 저장소 초기화
        
        if (condition_min != -10000) // 장비아이템 착용 조건 - 스탯(능력치, 평판) 하한이 존재하는 경우(!= 최소 하한)
            m_sBuffer += condition_min;
            
        if (m_bRefine_Condition_Check == true) // 문자열 정제가 필요한 경우
            m_sBuffer += " ~ ";
        else
            m_sBuffer = "";
            
        if (condition_max != 10000) // 장비아이템 착용 조건 - 스탯(능력치, 평판) 상한이 존재하는 경우(!= 최대 상한)
            m_sBuffer += condition_max;

        m_sBuffer = beforesentence + m_sBuffer;

        if (m_bRefine_Condition_Check == true) // 문자열 정제가 필요한 경우
        {
            if (condition_min <= current_condition && current_condition <= condition_max) // 플레이어 스탯(능력치, 평판)이 장비아이템 착용조건 - 스탯(능력치, 평판)에 부합하는 경우
            {
                m_sBuffer = m_sColor_White + m_sBuffer + m_sColor_End + aftersentence; // 문자열 흰색 설정
            }
            else // 플레이어 스탯(능력치, 평판)이 장비아이템 착용조건 - 스탯(능력치, 평판)에 미달하거나 초과하는 경우
            {
                m_sBuffer = m_sColor_Red + m_sBuffer + m_sColor_End + aftersentence; // 문자열 빨간색 설정
                m_bEquip_Condition_Check = false; // 장비아이템 착용 불가능(장비아이템 착용 해제)
            }
        }
        else // 문자열 정제가 불필요한 경우
        {
            m_sBuffer = m_sColor_Brown + m_sBuffer + m_sColor_End + aftersentence; // 문자열 갈색 설정
        }

        return m_sBuffer; // 문자열 임시 저장소 반환
    }
    // 텍스트(문자열) 정제 함수 - 아이템 세트효과 이름, 설명
    string Refine_Condition(string sentence, bool isname = true) // sentence : 문장, isname : 아이템 세트효과 이름인지 판단
    {
        m_nPlayerEquipment_SetItemEffect_Current = 0; // 플레이어에게 적용중인 착용한 장비아이템과 동일한 아이템 세트효과 개수

        // 플레이어에게 적용중인 착용한 장비아이템과 동일한 아이템 세트효과 개수 판단
        if (Player_Equipment.m_bEquipment_Hat == true) // 착용중인 장비아이템(모자)이 존재하는 경우
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Hat.m_nItemCode) == m_ISE.m_nItemSetEffect_Code) // 장비아이템(모자)가 동일한 아이템 세트효과를 가지는 경우
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_Hat() == true) // 착용중인 장비아이템(모자) 착용 조건 판단 == ture
                    m_nPlayerEquipment_SetItemEffect_Current += 1; // 플레이어에게 적용중인 착용한 장비아이템과 동일한 아이템 세트효과 개수 += 1
            }
        }
        if (Player_Equipment.m_bEquipment_Top == true) // 착용중인 장비아이템(상의)이 존재하는 경우
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Top.m_nItemCode) == m_ISE.m_nItemSetEffect_Code) // 장비아이템(상의)가 동일한 아이템 세트효과를 가지는 경우
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_Top() == true) // 착용중인 장비아이템(상의) 착용 조건 판단 == ture
                    m_nPlayerEquipment_SetItemEffect_Current += 1; // 플레이어에게 적용중인 착용한 장비아이템과 동일한 아이템 세트효과 개수 += 1
            }
        }
        if (Player_Equipment.m_bEquipment_Bottoms == true) // 착용중인 장비아이템(하의)이 존재하는 경우
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Bottoms.m_nItemCode) == m_ISE.m_nItemSetEffect_Code) // 장비아이템(하의)가 동일한 아이템 세트효과를 가지는 경우
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_Bottoms() == true) // 착용중인 장비아이템(하의) 착용 조건 판단 == ture
                    m_nPlayerEquipment_SetItemEffect_Current += 1; // 플레이어에게 적용중인 착용한 장비아이템과 동일한 아이템 세트효과 개수 += 1
            }
        }
        if (Player_Equipment.m_bEquipment_Shose == true) // 착용중인 장비아이템(신발)이 존재하는 경우
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Shose.m_nItemCode) == m_ISE.m_nItemSetEffect_Code) // 장비아이템(신발)가 동일한 아이템 세트효과를 가지는 경우
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_Shose() == true) // 착용중인 장비아이템(신발) 착용 조건 판단 == ture
                    m_nPlayerEquipment_SetItemEffect_Current += 1; // 플레이어에게 적용중인 착용한 장비아이템과 동일한 아이템 세트효과 개수 += 1
            }
        }
        if (Player_Equipment.m_bEquipment_Gloves == true) // 착용중인 장비아이템(장갑)이 존재하는 경우
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Gloves.m_nItemCode) == m_ISE.m_nItemSetEffect_Code) // 장비아이템(장갑)가 동일한 아이템 세트효과를 가지는 경우
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_Gloves() == true) // 착용중인 장비아이템(장갑) 착용 조건 판단 == ture
                    m_nPlayerEquipment_SetItemEffect_Current += 1; // 플레이어에게 적용중인 착용한 장비아이템과 동일한 아이템 세트효과 개수 += 1
            }
        }
        if (Player_Equipment.m_bEquipment_Mainweapon == true) // 착용중인 장비아이템(주무기)이 존재하는 경우
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Mainweapon.m_nItemCode) == m_ISE.m_nItemSetEffect_Code) // 장비아이템(주무기)가 동일한 아이템 세트효과를 가지는 경우
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_MainWeapon() == true) // 착용중인 장비아이템(주무기) 착용 조건 판단 == ture
                    m_nPlayerEquipment_SetItemEffect_Current += 1; // 플레이어에게 적용중인 착용한 장비아이템과 동일한 아이템 세트효과 개수 += 1
            }
        }
        if (Player_Equipment.m_bEquipment_Subweapon == true) // 착용중인 장비아이템(보조무기)이 존재하는 경우
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Subweapon.m_nItemCode) == m_ISE.m_nItemSetEffect_Code) // 장비아이템(보조무기)가 동일한 아이템 세트효과를 가지는 경우
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_SubWeapon() == true) // 착용중인 장비아이템(보조무기) 착용 조건 판단 == ture
                    m_nPlayerEquipment_SetItemEffect_Current += 1; // 플레이어에게 적용중인 착용한 장비아이템과 동일한 아이템 세트효과 개수 += 1
            }
        }

        if (m_nList_SetItemEffect_Code[m_nSetItemEffect_List_Index] <= m_nPlayerEquipment_SetItemEffect_Current) // 해당 아이템 세트효과가 적용중인 경우
        {
            if (isname == true) // 아이템 세트효과 이름인 경우
                return m_sColor_White + sentence + " 적용" + m_sColor_End; // 문자열 흰색 설정 + "적용"
            else // 아이템 세트효과 이름이 아닌 경우(아이템 세트효과 설명인 경우)
                return m_sColor_White + sentence + m_sColor_End; // 문자열 흰색 설정
        }
        else // 해당 아이템 세트효과가 적용중이지 않을 경우
        {
            if (isname == true) // 아이템 세트효과 이름인 경우
                return m_sColor_Brown + sentence + " 미적용" + m_sColor_End; // 문자열 갈색 설정 + "적미용"
            else // 아이템 세트효과 이름이 아닌 경우(아이템 세트효과 설명인 경우)
                return m_sColor_Brown + sentence + m_sColor_End; // 문자열 갈색 설정
        }
    }
    // 텍스트(문자열) 정제 함수 - 아이템 세트효과(int)
    string Refine_Condition(string sentence, int number) // sentence : 문장, number : 아이템 세트효과 스탯(능력치, 평판)
    {
        m_nPlayerEquipment_SetItemEffect_Current = 0; // 플레이어에게 적용중인 착용한 장비아이템과 동일한 아이템 세트효과 개수

        // 플레이어에게 적용중인 착용한 장비아이템과 동일한 아이템 세트효과 개수 판단
        if (Player_Equipment.m_bEquipment_Hat == true) // 착용중인 장비아이템(모자)이 존재하는 경우
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Hat.m_nItemCode) == m_ISE.m_nItemSetEffect_Code) // 장비아이템(모자)가 동일한 아이템 세트효과를 가지는 경우
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_Hat() == true) // 착용중인 장비아이템(모자) 착용 조건 판단 == ture
                    m_nPlayerEquipment_SetItemEffect_Current += 1; // 플레이어에게 적용중인 착용한 장비아이템과 동일한 아이템 세트효과 개수 += 1
            }
        }
        if (Player_Equipment.m_bEquipment_Top == true) // 착용중인 장비아이템(상의)이 존재하는 경우
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Top.m_nItemCode) == m_ISE.m_nItemSetEffect_Code) // 장비아이템(상의)가 동일한 아이템 세트효과를 가지는 경우
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_Top() == true) // 착용중인 장비아이템(상의) 착용 조건 판단 == ture
                    m_nPlayerEquipment_SetItemEffect_Current += 1; // 플레이어에게 적용중인 착용한 장비아이템과 동일한 아이템 세트효과 개수 += 1
            }
        }
        if (Player_Equipment.m_bEquipment_Bottoms == true) // 착용중인 장비아이템(하의)이 존재하는 경우
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Bottoms.m_nItemCode) == m_ISE.m_nItemSetEffect_Code) // 장비아이템(하의)가 동일한 아이템 세트효과를 가지는 경우
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_Bottoms() == true) // 착용중인 장비아이템(하의) 착용 조건 판단 == ture
                    m_nPlayerEquipment_SetItemEffect_Current += 1; // 플레이어에게 적용중인 착용한 장비아이템과 동일한 아이템 세트효과 개수 += 1
            }
        }
        if (Player_Equipment.m_bEquipment_Shose == true) // 착용중인 장비아이템(신발)이 존재하는 경우
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Shose.m_nItemCode) == m_ISE.m_nItemSetEffect_Code) // 장비아이템(신발)가 동일한 아이템 세트효과를 가지는 경우
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_Shose() == true) // 착용중인 장비아이템(신발) 착용 조건 판단 == ture
                    m_nPlayerEquipment_SetItemEffect_Current += 1; // 플레이어에게 적용중인 착용한 장비아이템과 동일한 아이템 세트효과 개수 += 1
            }
        }
        if (Player_Equipment.m_bEquipment_Gloves == true) // 착용중인 장비아이템(장갑)이 존재하는 경우
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Gloves.m_nItemCode) == m_ISE.m_nItemSetEffect_Code) // 장비아이템(장갑)가 동일한 아이템 세트효과를 가지는 경우
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_Gloves() == true) // 착용중인 장비아이템(장갑) 착용 조건 판단 == ture
                    m_nPlayerEquipment_SetItemEffect_Current += 1; // 플레이어에게 적용중인 착용한 장비아이템과 동일한 아이템 세트효과 개수 += 1
            }
        }
        if (Player_Equipment.m_bEquipment_Mainweapon == true) // 착용중인 장비아이템(주무기)이 존재하는 경우
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Mainweapon.m_nItemCode) == m_ISE.m_nItemSetEffect_Code) // 장비아이템(주무기)가 동일한 아이템 세트효과를 가지는 경우
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_MainWeapon() == true) // 착용중인 장비아이템(주무기) 착용 조건 판단 == ture
                    m_nPlayerEquipment_SetItemEffect_Current += 1; // 플레이어에게 적용중인 착용한 장비아이템과 동일한 아이템 세트효과 개수 += 1
            }
        }
        if (Player_Equipment.m_bEquipment_Subweapon == true) // 착용중인 장비아이템(보조무기)이 존재하는 경우
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Subweapon.m_nItemCode) == m_ISE.m_nItemSetEffect_Code) // 장비아이템(보조무기)가 동일한 아이템 세트효과를 가지는 경우
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_SubWeapon() == true) // 착용중인 장비아이템(보조무기) 착용 조건 판단 == ture
                    m_nPlayerEquipment_SetItemEffect_Current += 1; // 플레이어에게 적용중인 착용한 장비아이템과 동일한 아이템 세트효과 개수 += 1
            }
        }

        if (number > 0) // 이로운 효과(아이템 세트효과 스탯(능력치, 평판) > 0)
            return m_sColor_White + sentence + " " + number.ToString() + m_sColor_End; // 문자열 흰색 + 아이템 세트효과 스탯(능력치, 평판)
        else if (number < 0) // 해로운 효과(아이템 세트효과 스탯(능력치, 평판) < 0)
            return m_sColor_WhiteGray + sentence + " " + number.ToString() + m_sColor_End; // 문자열 회색 + 아이템 세트효과 스탯(능력치, 평판)
        else // 존재하지 않는 효과
            return m_sColor_Brown + sentence + " " + number.ToString() + m_sColor_End; // 문자열 갈색 + 아이템 세트효과 스탯(능력치, 평판)
    }
    // 텍스트(문자열) 정제 함수 - 아이템 세트효과(float). 스탯(능력치(공격속도)) 전용
    string Refine_Condition(string sentence, float number) // sentence : 문장, number : 아이템 세트효과 스탯(능력치, 평판)
    {
        m_nPlayerEquipment_SetItemEffect_Current = 0; // 플레이어에게 적용중인 착용한 장비아이템과 동일한 아이템 세트효과 개수
        
        // 플레이어에게 적용중인 착용한 장비아이템과 동일한 아이템 세트효과 개수 판단
        if (Player_Equipment.m_bEquipment_Hat == true) // 착용중인 장비아이템(모자)이 존재하는 경우
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Hat.m_nItemCode) == m_ISE.m_nItemSetEffect_Code) // 장비아이템(모자)가 동일한 아이템 세트효과를 가지는 경우
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_Hat() == true) // 착용중인 장비아이템(모자) 착용 조건 판단 == ture
                    m_nPlayerEquipment_SetItemEffect_Current += 1; // 플레이어에게 적용중인 착용한 장비아이템과 동일한 아이템 세트효과 개수 += 1
            }
        }
        if (Player_Equipment.m_bEquipment_Top == true) // 착용중인 장비아이템(상의)이 존재하는 경우
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Top.m_nItemCode) == m_ISE.m_nItemSetEffect_Code) // 장비아이템(상의)가 동일한 아이템 세트효과를 가지는 경우
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_Top() == true) // 착용중인 장비아이템(상의) 착용 조건 판단 == ture
                    m_nPlayerEquipment_SetItemEffect_Current += 1; // 플레이어에게 적용중인 착용한 장비아이템과 동일한 아이템 세트효과 개수 += 1
            }
        }
        if (Player_Equipment.m_bEquipment_Bottoms == true) // 착용중인 장비아이템(하의)이 존재하는 경우
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Bottoms.m_nItemCode) == m_ISE.m_nItemSetEffect_Code) // 장비아이템(하의)가 동일한 아이템 세트효과를 가지는 경우
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_Bottoms() == true) // 착용중인 장비아이템(하의) 착용 조건 판단 == ture
                    m_nPlayerEquipment_SetItemEffect_Current += 1; // 플레이어에게 적용중인 착용한 장비아이템과 동일한 아이템 세트효과 개수 += 1
            }
        }
        if (Player_Equipment.m_bEquipment_Shose == true) // 착용중인 장비아이템(신발)이 존재하는 경우
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Shose.m_nItemCode) == m_ISE.m_nItemSetEffect_Code) // 장비아이템(신발)가 동일한 아이템 세트효과를 가지는 경우
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_Shose() == true) // 착용중인 장비아이템(신발) 착용 조건 판단 == ture
                    m_nPlayerEquipment_SetItemEffect_Current += 1; // 플레이어에게 적용중인 착용한 장비아이템과 동일한 아이템 세트효과 개수 += 1
            }
        }
        if (Player_Equipment.m_bEquipment_Gloves == true) // 착용중인 장비아이템(장갑)이 존재하는 경우
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Gloves.m_nItemCode) == m_ISE.m_nItemSetEffect_Code) // 장비아이템(장갑)가 동일한 아이템 세트효과를 가지는 경우
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_Gloves() == true) // 착용중인 장비아이템(장갑) 착용 조건 판단 == ture
                    m_nPlayerEquipment_SetItemEffect_Current += 1; // 플레이어에게 적용중인 착용한 장비아이템과 동일한 아이템 세트효과 개수 += 1
            }
        }
        if (Player_Equipment.m_bEquipment_Mainweapon == true) // 착용중인 장비아이템(주무기)이 존재하는 경우
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Mainweapon.m_nItemCode) == m_ISE.m_nItemSetEffect_Code) // 장비아이템(주무기)가 동일한 아이템 세트효과를 가지는 경우
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_MainWeapon() == true) // 착용중인 장비아이템(주무기) 착용 조건 판단 == ture
                    m_nPlayerEquipment_SetItemEffect_Current += 1; // 플레이어에게 적용중인 착용한 장비아이템과 동일한 아이템 세트효과 개수 += 1
            }
        }
        if (Player_Equipment.m_bEquipment_Subweapon == true) // 착용중인 장비아이템(보조무기)이 존재하는 경우
        {
            if (ItemSetEffectManager.instance.Return_SetItemEffect(Player_Equipment.m_gEquipment_Subweapon.m_nItemCode) == m_ISE.m_nItemSetEffect_Code) // 장비아이템(보조무기)가 동일한 아이템 세트효과를 가지는 경우
            {
                if (Player_Total.Instance.CheckCondition_Item_Equip_SubWeapon() == true) // 착용중인 장비아이템(보조무기) 착용 조건 판단 == ture
                    m_nPlayerEquipment_SetItemEffect_Current += 1; // 플레이어에게 적용중인 착용한 장비아이템과 동일한 아이템 세트효과 개수 += 1
            }
        }

        if (number > 0) // 해로운 효과(아이템 세트효과 스탯(능력치, 평판) > 0)
            return m_sColor_White + sentence + " " + number.ToString() + m_sColor_End; // 문자열 흰색 + 아이템 세트효과 스탯(능력치, 평판)
        else if (number < 0) // 이로운 효과(아이템 세트효과 스탯(능력치, 평판) < 0)
            return m_sColor_WhiteGray + sentence + " " + number.ToString() + m_sColor_End; // 문자열 회색 + 아이템 세트효과 스탯(능력치, 평판)
        else // 존재하지 않는 효과
            return m_sColor_Brown + sentence + " " + number.ToString() + m_sColor_End; // 문자열 갈색 + 아이템 세트효과 스탯(능력치, 평판)
    }
    // 텍스트(문자열) 정제 함수 - 장비아이템 착용효과(int)
    string Refine_Condition(string sentence, int option, int option_additional, int option_reinforcement) // sentence : 문장,
                                                                                                          // option : 장비아이템 스탯(능력치, 평판), 
                                                                                                          // option_additional : 장비아이템 추가 옵셥 스탯(능력치, 평판), 
                                                                                                          // option_reinforcement : 장비아이템 강화 스탯(능력치, 평판)
    {
        if (option != 0) // 장비아이템 스탯(능력치, 평판) != 0
        {
            if (option_additional + option_reinforcement != 0) // 장비아이템 추가 옵셥 스탯(능력치, 평판) + 장비아이템 강화 스탯(능력치, 평판) != 0
            {
                if (option > 0) // 장비아이템 스탯(능력치, 평판) > 0
                    return m_sColor_White + sentence + " " + option.ToString() + "(" + (option_additional + option_reinforcement).ToString() + ")" + m_sColor_End; // 문자열 흰색 설정 +
                                                                                                                                                                   // 장비아이템 스탯(능력치, 평판) +
                                                                                                                                                                   // 장비아이템 추가 옵셥 스탯(능력치, 평판) +
                                                                                                                                                                   // 장비아이템 강화 스탯(능력치, 평판)
                else // 장비아이템 스탯(능력치, 평판) <= 0
                    return m_sColor_WhiteGray + sentence + " " + option.ToString() + "(" + (option_additional + option_reinforcement).ToString() + ")" + m_sColor_End; // 문자열 회색 설정 + 
                                                                                                                                                                       // 장비아이템 스탯(능력치, 평판) +
                                                                                                                                                                       // 장비아이템 추가 옵셥 스탯(능력치, 평판) +
                                                                                                                                                                       // 장비아이템 강화 스탯(능력치, 평판)
            }
            else // 장비아이템 추가 옵셥 스탯(능력치, 평판) + 장비아이템 강화 스탯(능력치, 평판) == 0
            {
                if (option > 0) // 장비아이템 스탯(능력치, 평판) > 0
                    return m_sColor_White + sentence + " " + option.ToString() + "(" + (option_additional + option_reinforcement).ToString() + ")" + m_sColor_End; // 문자열 흰색 설정 + 
                                                                                                                                                                   // 장비아이템 스탯(능력치, 평판) +
                                                                                                                                                                   // 장비아이템 추가 옵셥 스탯(능력치, 평판) +
                                                                                                                                                                   // 장비아이템 강화 스탯(능력치, 평판)
                else // 장비아이템 스탯(능력치, 평판) <= 0
                    return m_sColor_WhiteGray + sentence + " " + option.ToString() + "(" + (option_additional + option_reinforcement).ToString() + ")" + m_sColor_End; // 문자열 회색 설정 + 
                                                                                                                                                                       // 장비아이템 스탯(능력치, 평판) +
                                                                                                                                                                       // 장비아이템 추가 옵셥 스탯(능력치, 평판) +
                                                                                                                                                                       // 장비아이템 강화 스탯(능력치, 평판)
            }
        }
        else // 장비아이템 스탯(능력치, 평판) == 0
        {
            if (option_additional + option_reinforcement != 0) // 장비아이템 추가 옵셥 스탯(능력치, 평판) + 장비아이템 강화 스탯(능력치, 평판) != 0
            {
                return m_sColor_Brown + sentence + " " + option.ToString() + "(" + (option_additional + option_reinforcement).ToString() + ")" + m_sColor_End; // 문자열 갈색 설정 + 
                                                                                                                                                               // 장비아이템 스탯(능력치, 평판) +
                                                                                                                                                               // 장비아이템 추가 옵셥 스탯(능력치, 평판) +
                                                                                                                                                               // 장비아이템 강화 스탯(능력치, 평판)
            }
            else // 장비아이템 추가 옵셥 스탯(능력치, 평판) + 장비아이템 강화 스탯(능력치, 평판) == 0
            {
                if (option_additional != 0 || option_reinforcement != 0) // 장비아이템 추가 옵셥 스탯(능력치, 평판) != 0 || 장비아이템 강화 스탯(능력치, 평판) != 0
                    return m_sColor_Brown + sentence + " " + option.ToString() + "(" + (option_additional + option_reinforcement).ToString() + ")" + m_sColor_End; // 문자열 갈색 설정 + 
                                                                                                                                                                   // 장비아이템 스탯(능력치, 평판) +
                                                                                                                                                                   // 장비아이템 추가 옵셥 스탯(능력치, 평판) +
                                                                                                                                                                   // 장비아이템 강화 스탯(능력치, 평판)
                else // 장비아이템 추가 옵셥 스탯(능력치, 평판) == 0 && 장비아이템 강화 스탯(능력치, 평판) == 0
                    return m_sColor_Brown + sentence + " " + option.ToString() + m_sColor_End; // 문자열 갈색 설정 + 장비아이템 스탯(능력치, 평판)
            }
        }
    }
    // 텍스트(문자열) 정제 함수 - 장비아이템 착용효과(float). 스탯(능력치(공격속도)) 전용
    string Refine_Condition(string sentence, float option, float option_additional, float option_reinforcement) // sentence : 문장,
                                                                                                                // option : 장비아이템 스탯(능력치, 평판), 
                                                                                                                // option_additional : 장비아이템 추가 옵셥 스탯(능력치, 평판), 
                                                                                                                // option_reinforcement : 장비아이템 강화 스탯(능력치, 평판)
    {
        if (option != 0) // 장비아이템 스탯(능력치, 평판) != 0
        {
            if (option_additional + option_reinforcement != 0) // 장비아이템 추가 옵셥 스탯(능력치, 평판) + 장비아이템 강화 스탯(능력치, 평판) != 0
            {
                if (option < 0) // 장비아이템 스탯(능력치, 평판) < 0
                    return m_sColor_White + sentence + " " + option.ToString() + "(" + (option_additional + option_reinforcement).ToString() + ")" + m_sColor_End; // 문자열 흰색 설정 +
                                                                                                                                                                   // 장비아이템 스탯(능력치, 평판) +
                                                                                                                                                                   // 장비아이템 추가 옵셥 스탯(능력치, 평판) +
                                                                                                                                                                   // 장비아이템 강화 스탯(능력치, 평판)
                else // 장비아이템 스탯(능력치, 평판) >= 0
                    return m_sColor_WhiteGray + sentence + " " + option.ToString() + "(" + (option_additional + option_reinforcement).ToString() + ")" + m_sColor_End; // 문자열 회색 설정 + 
                                                                                                                                                                       // 장비아이템 스탯(능력치, 평판) +
                                                                                                                                                                       // 장비아이템 추가 옵셥 스탯(능력치, 평판) +
                                                                                                                                                                       // 장비아이템 강화 스탯(능력치, 평판)
            }
            else // 장비아이템 추가 옵셥 스탯(능력치, 평판) + 장비아이템 강화 스탯(능력치, 평판) == 0
            {
                if (option < 0) // 장비아이템 스탯(능력치, 평판) < 0
                    return m_sColor_White + sentence + " " + option.ToString() + "(" + (option_additional + option_reinforcement).ToString() + ")" + m_sColor_End; // 문자열 흰색 설정 + 
                                                                                                                                                                   // 장비아이템 스탯(능력치, 평판) +
                                                                                                                                                                   // 장비아이템 추가 옵셥 스탯(능력치, 평판) +
                                                                                                                                                                   // 장비아이템 강화 스탯(능력치, 평판)
                else // 장비아이템 스탯(능력치, 평판) >= 0
                    return m_sColor_WhiteGray + sentence + " " + option.ToString() + "(" + (option_additional + option_reinforcement).ToString() + ")" + m_sColor_End; // 문자열 회색 설정 + 
                                                                                                                                                                       // 장비아이템 스탯(능력치, 평판) +
                                                                                                                                                                       // 장비아이템 추가 옵셥 스탯(능력치, 평판) +
                                                                                                                                                                       // 장비아이템 강화 스탯(능력치, 평판)
            }
        }
        else // 장비아이템 스탯(능력치, 평판) == 0
        {
            if (option_additional + option_reinforcement != 0) // 장비아이템 추가 옵셥 스탯(능력치, 평판) + 장비아이템 강화 스탯(능력치, 평판) != 0
            {
                return m_sColor_Brown + sentence + " " + option.ToString() + "(" + (option_additional + option_reinforcement).ToString() + ")" + m_sColor_End; // 문자열 갈색 설정 + 
                                                                                                                                                               // 장비아이템 스탯(능력치, 평판) +
                                                                                                                                                               // 장비아이템 추가 옵셥 스탯(능력치, 평판) +
                                                                                                                                                               // 장비아이템 강화 스탯(능력치, 평판)
            }
            else // 장비아이템 추가 옵셥 스탯(능력치, 평판) + 장비아이템 강화 스탯(능력치, 평판) == 0
            {
                if (option_additional != 0 || option_reinforcement != 0) // 장비아이템 추가 옵셥 스탯(능력치, 평판) != 0 || 장비아이템 강화 스탯(능력치, 평판) != 0
                    return m_sColor_Brown + sentence + " " + option.ToString() + "(" + (option_additional + option_reinforcement).ToString() + ")" + m_sColor_End; // 문자열 갈색 설정 + 
                                                                                                                                                                   // 장비아이템 스탯(능력치, 평판) +
                                                                                                                                                                   // 장비아이템 추가 옵셥 스탯(능력치, 평판) +
                                                                                                                                                                   // 장비아이템 강화 스탯(능력치, 평판)
                else // 장비아이템 추가 옵셥 스탯(능력치, 평판) == 0 && 장비아이템 강화 스탯(능력치, 평판) == 0
                    return m_sColor_Brown + sentence + " " + option.ToString() + m_sColor_End; // 문자열 갈색 설정 + 장비아이템 스탯(능력치, 평판)
            }
        }
    }
}
