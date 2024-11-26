using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUI_Itemslot_Etc_Information : MonoBehaviour
{
    // Itemslot_Etc_Information 에서 확인 할 수 있는 장비 아이템 정보 UI.
    [SerializeField] public GameObject m_gPanel_Itemslot_Etc_Information;
    [Space(20)]

    [SerializeField] GameObject m_gPanel_Itemslot_Etc_Information_UpBar;
    [SerializeField] TextMeshProUGUI m_TMP_Itemslot_Etc_Information_UpBar;
    [SerializeField] Button m_BTN_Itemslot_Etc_Information_UpBar_Exit;
    [Space(20)]

    [SerializeField] GameObject m_gPanel_Itemslot_Etc_Information_Content;
    [Space(10)]

    [SerializeField] GameObject m_gPanel_Itemslot_Etc_Information_Content_Image;
    [SerializeField] GameObject m_gPanel_Itemslot_Etc_Information_Content_Image_ItemSprite;
    [SerializeField] Image m_IMG_Itemslot_Etc_Information_Content_Image_ItemSprite;
    [Space(10)]

    [SerializeField] GameObject m_gPanel_Itemslot_Etc_Information_Content_ItemInformation;
    [SerializeField] TextMeshProUGUI m_TMP_Itemslot_Etc_Information_Content_ItemInformation;
    [Space(10)]

    [SerializeField] GameObject m_gPanel_Itemslot_Etc_Information_Content_ItemDescription;

    [SerializeField] GameObject m_gPanel_Itemslot_Etc_Information_Content_ItemDescription_Name;
    [SerializeField] TextMeshProUGUI m_TMP_Itemslot_Etc_Information_Content_ItemDescription_Name;
    [Space(5)]
    [SerializeField] GameObject m_gPanel_Itemslot_Etc_Information_Content_ItemDescription_Content;
    [SerializeField] GameObject m_gSV_Itemslot_Etc_Information_Content_ItemDescription_Content;
    [SerializeField] GameObject m_gViewport_Itemslot_Etc_Information_Content_ItemDescription_Content;
    [SerializeField] TextMeshProUGUI m_TMP_Itemslot_Etc_Information_Content_ItemDescription_Content;
    [SerializeField] Scrollbar m_Scrollbar_Itemslot_Etc_Information_Content_ItemDescription_Content;


    // Text 임시 저장소.
    string m_sBuffer;
   
    public void InitialSet()
    {
        InitialSet_Object();
        InitialSet_Button();
    }

    // 초기 Object 불러오기.
    void InitialSet_Object()
    {
        m_gPanel_Itemslot_Etc_Information = GameObject.Find("Canvas_GUI").transform.Find("Panel_Itemslot_Etc_Information").gameObject;


        m_gPanel_Itemslot_Etc_Information_UpBar = m_gPanel_Itemslot_Etc_Information.transform.Find("Panel_Itemslot_Etc_Information_UpBar").gameObject;
        m_TMP_Itemslot_Etc_Information_UpBar = m_gPanel_Itemslot_Etc_Information_UpBar.transform.Find("TMP_Itemslot_Etc_Information_UpBar").gameObject.GetComponent<TextMeshProUGUI>();
        m_BTN_Itemslot_Etc_Information_UpBar_Exit = m_gPanel_Itemslot_Etc_Information_UpBar.transform.Find("BTN_Itemslot_Etc_Information_UpBar_Exit").gameObject.GetComponent<Button>();


        m_gPanel_Itemslot_Etc_Information_Content = m_gPanel_Itemslot_Etc_Information.transform.Find("Panel_Itemslot_Etc_Information_Content").gameObject;


        m_gPanel_Itemslot_Etc_Information_Content_Image = m_gPanel_Itemslot_Etc_Information_Content.transform.Find("Panel_Itemslot_Etc_Information_Content_Image").gameObject;
        m_gPanel_Itemslot_Etc_Information_Content_Image_ItemSprite = m_gPanel_Itemslot_Etc_Information_Content_Image.transform.Find("Panel_Itemslot_Etc_Information_Content_Image_ItemSprite").gameObject;
        m_IMG_Itemslot_Etc_Information_Content_Image_ItemSprite = m_gPanel_Itemslot_Etc_Information_Content_Image_ItemSprite.GetComponent<Image>();


        m_gPanel_Itemslot_Etc_Information_Content_ItemInformation = m_gPanel_Itemslot_Etc_Information_Content.transform.Find("Panel_Itemslot_Etc_Information_Content_ItemInformation").gameObject;
        m_TMP_Itemslot_Etc_Information_Content_ItemInformation = m_gPanel_Itemslot_Etc_Information_Content_ItemInformation.transform.Find("TMP_Itemslot_Etc_Information_Content_ItemInformation").gameObject.GetComponent<TextMeshProUGUI>();


        m_gPanel_Itemslot_Etc_Information_Content_ItemDescription = m_gPanel_Itemslot_Etc_Information_Content.transform.Find("Panel_Itemslot_Etc_Information_Content_ItemDescription").gameObject;

        m_gPanel_Itemslot_Etc_Information_Content_ItemDescription_Name = m_gPanel_Itemslot_Etc_Information_Content_ItemDescription.transform.Find("Panel_Itemslot_Etc_Information_Content_ItemDescription_Name").gameObject;
        m_TMP_Itemslot_Etc_Information_Content_ItemDescription_Name = m_gPanel_Itemslot_Etc_Information_Content_ItemDescription_Name.transform.Find("TMP_Itemslot_Etc_Information_Content_ItemDescription_Name").gameObject.GetComponent<TextMeshProUGUI>();

        m_gPanel_Itemslot_Etc_Information_Content_ItemDescription_Content = m_gPanel_Itemslot_Etc_Information_Content_ItemDescription.transform.Find("Panel_Itemslot_Etc_Information_Content_ItemDescription_Content").gameObject;
        m_gSV_Itemslot_Etc_Information_Content_ItemDescription_Content = m_gPanel_Itemslot_Etc_Information_Content_ItemDescription_Content.transform.Find("SV_Itemslot_Etc_Information_Content_ItemDescription_Content").gameObject;
        m_gViewport_Itemslot_Etc_Information_Content_ItemDescription_Content = m_gSV_Itemslot_Etc_Information_Content_ItemDescription_Content.transform.Find("Viewport_Itemslot_Etc_Information_Content_ItemDescription_Content").gameObject;
        m_TMP_Itemslot_Etc_Information_Content_ItemDescription_Content = m_gViewport_Itemslot_Etc_Information_Content_ItemDescription_Content.transform.Find("TMP_Itemslot_Etc_Information_Content_ItemDescription_Content").gameObject.GetComponent<TextMeshProUGUI>();
        m_Scrollbar_Itemslot_Etc_Information_Content_ItemDescription_Content = m_gSV_Itemslot_Etc_Information_Content_ItemDescription_Content.transform.Find("Scrollbar_Itemslot_Etc_Information_Content_ItemDescription_Content").gameObject.GetComponent<Scrollbar>();

    }

    // 초기 Button 이벤트 설정.
    void InitialSet_Button()
    {
        m_BTN_Itemslot_Etc_Information_UpBar_Exit.onClick.RemoveAllListeners();
        m_BTN_Itemslot_Etc_Information_UpBar_Exit.onClick.AddListener(delegate { Set_BTN_Exit(); });
    }

    // 기타 아이템 설명 창 페이지 변경.
    void Set_BTN_Exit()
    {
        m_gPanel_Itemslot_Etc_Information.SetActive(false);
    }

    public void Display_GUI_Itemslot_Etc_Information(float fcoordination_x, float fcoordination_y)
    {
        m_gPanel_Itemslot_Etc_Information.SetActive(true);
        m_gPanel_Itemslot_Etc_Information.transform.SetAsLastSibling();
        m_gPanel_Itemslot_Etc_Information.transform.position = new Vector2(fcoordination_x, fcoordination_y);
    }
    public void UnDisplay_GUI_Itemslot_Etc_Information()
    {
        m_gPanel_Itemslot_Etc_Information.SetActive(false);
        m_Scrollbar_Itemslot_Etc_Information_Content_ItemDescription_Content.value = 1;
    }

    // 기타 아이템 설명 창 세부 설정.
    public void UpdateItemEtcInformation(Item_Etc item, int arynumber)
    {
        m_Scrollbar_Itemslot_Etc_Information_Content_ItemDescription_Content.value = 1;
        
        UpdateItemEtcInformation_UpBar(item);
        UpdateItemEtcInformation_Content(item);
    }
    void UpdateItemEtcInformation_UpBar(Item_Etc item)
    {
        // 기타 아이템 이름.
        m_TMP_Itemslot_Etc_Information_UpBar.text = item.m_sItemName;
    }
    void UpdateItemEtcInformation_Content(Item_Etc item)
    {
        // 기타 아이템 이미지.
        m_IMG_Itemslot_Etc_Information_Content_Image_ItemSprite.sprite = item.m_sp_Sprite;
        m_IMG_Itemslot_Etc_Information_Content_Image_ItemSprite.color = new Color(1, 1, 1, 0.75f);

        // 기타 아이템 분류, 등급.
        // 분류.
        m_TMP_Itemslot_Etc_Information_Content_ItemInformation.text = "";
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

        m_TMP_Itemslot_Etc_Information_Content_ItemInformation.text += "분류: " + m_sBuffer + "\n";
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
        m_TMP_Itemslot_Etc_Information_Content_ItemInformation.text += "등급: " + m_sBuffer + "\n";

        // 기타 아이템 설명.
        m_TMP_Itemslot_Etc_Information_Content_ItemDescription_Content.text = "";
        m_TMP_Itemslot_Etc_Information_Content_ItemDescription_Content.text += item.GetItemDescription();
    }
}
