using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUI_Reinforcement : MonoBehaviour
{
    // Reinforcement UI.
    [SerializeField] public GameObject m_gPanel_Reinforcement;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_Reinforcement_UpBar;
    [SerializeField] Button m_BTN_Reinforcement_UpBar_Exit;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_Reinforcement_Content;
    [SerializeField] GameObject m_gSV_Reinforcement_Content;
    [SerializeField] GameObject m_gViewport_Reinforcement_Content;
    [SerializeField] GameObject m_gContent_Reinforcement_Content;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_Reinforcement_Information;
    [SerializeField] GameObject m_gPanel_Reinforcement_Information_Image;
    [SerializeField] GameObject m_gPanel_Reinforcement_Information_Image_ItemSprite;
    [SerializeField] Image m_IMG_Reinforcement_Information_Image_ItemSprite;
    [SerializeField] GameObject m_gPanel_Reinforcement_Information_ItemInformation;
    [SerializeField] TextMeshProUGUI m_TMP_Reinforcement_Information_ItemInformation;
    [SerializeField] GameObject m_gPanel_Reinforcement_Information_Description;
    [SerializeField] GameObject m_gSV_Reinforcement_Information_Description;
    [SerializeField] GameObject m_gViewport_Reinforcement_Information_Description;
    [SerializeField] TextMeshProUGUI m_TMP_Reinforcement_Information_Description;


    [SerializeField] GameObject m_gPanel_Reinforcement_Equip_Information;

    public GameObject[] m_gary_Reinforcementslot;

    string m_sBuffer;

    public void InitialSet()
    {
        InitialSet_Object();
        InitialSet_Button();
        InitialSet_Reinforcementslot();
        m_gPanel_Reinforcement.SetActive(false);
    }

    // 초기 Object 설정.
    void InitialSet_Object()
    {
        m_gPanel_Reinforcement = GameObject.Find("Canvas_GUI").gameObject.transform.Find("Panel_Reinforcement").gameObject;
        m_gPanel_Reinforcement_Equip_Information = GameObject.Find("Canvas_GUI").gameObject.transform.Find("Panel_Reinforcement_Equip_Information").gameObject;

        m_gPanel_Reinforcement_UpBar = m_gPanel_Reinforcement.transform.Find("Panel_Reinforcement_UpBar").gameObject;
        m_BTN_Reinforcement_UpBar_Exit = m_gPanel_Reinforcement_UpBar.transform.Find("BTN_Reinforcement_UpBar_Exit").gameObject.GetComponent<Button>();

        m_gPanel_Reinforcement_Content = m_gPanel_Reinforcement.transform.Find("Panel_Reinforcement_Content").gameObject;
        m_gSV_Reinforcement_Content = m_gPanel_Reinforcement_Content.transform.Find("ScrollView_Reinforcement_Content").gameObject;
        m_gViewport_Reinforcement_Content = m_gSV_Reinforcement_Content.transform.Find("Viewport_Reinforcement_Content").gameObject;
        m_gContent_Reinforcement_Content = m_gViewport_Reinforcement_Content.transform.Find("Content_Reinforcement_Content").gameObject;

        m_gPanel_Reinforcement_Information = m_gPanel_Reinforcement.transform.Find("Panel_Reinforcement_Information").gameObject;
        m_gPanel_Reinforcement_Information_Image = m_gPanel_Reinforcement_Information.transform.Find("Panel_Reinforcement_Information_Image").gameObject;
        m_gPanel_Reinforcement_Information_Image_ItemSprite = m_gPanel_Reinforcement_Information_Image.transform.Find("Panel_Reinforcement_Information_Image_ItemSprite").gameObject;
        m_IMG_Reinforcement_Information_Image_ItemSprite = m_gPanel_Reinforcement_Information_Image_ItemSprite.GetComponent<Image>();
        m_gPanel_Reinforcement_Information_ItemInformation = m_gPanel_Reinforcement_Information.transform.Find("Panel_Reinforcement_Information_ItemInformation").gameObject;
        m_TMP_Reinforcement_Information_ItemInformation = m_gPanel_Reinforcement_Information_ItemInformation.transform.Find("TMP_Reinforcement_Information_ItemInformation").gameObject.GetComponent<TextMeshProUGUI>();
        m_gPanel_Reinforcement_Information_Description = m_gPanel_Reinforcement_Information.transform.Find("Panel_Reinforcement_Information_Description").gameObject;
        m_gSV_Reinforcement_Information_Description = m_gPanel_Reinforcement_Information_Description.transform.Find("SV_Reinforcement_Information_Description").gameObject;
        m_gViewport_Reinforcement_Information_Description = m_gSV_Reinforcement_Information_Description.transform.Find("Viewport_Reinforcement_Information_Description").gameObject;
        m_TMP_Reinforcement_Information_Description = m_gViewport_Reinforcement_Information_Description.transform.Find("TMP_Reinforcement_Information_Description").gameObject.GetComponent<TextMeshProUGUI>();


    }
    // 초기 Button 설정.
    void InitialSet_Button()
    {
        m_BTN_Reinforcement_UpBar_Exit.onClick.RemoveAllListeners();
        m_BTN_Reinforcement_UpBar_Exit.onClick.AddListener(delegate { Press_Btn_Exit(); });
    }
    // 초기 Reinforcementslot 설정.
    void InitialSet_Reinforcementslot()
    {
        m_gary_Reinforcementslot = new GameObject[60];
        for (int i = 0; i < 60; i++)
        {
            m_gary_Reinforcementslot[i] = m_gContent_Reinforcement_Content.transform.GetChild(i).gameObject;
            m_gary_Reinforcementslot[i].GetComponent<Reinforcementslot>().m_nAryNumber = i;
        }
    }

    void Press_Btn_Exit()
    {
        Display_GUI_Reinforcement();
        GUIManager_Total.Instance.Delete_GUI_Priority(20);
    }

    public bool Display_GUI_Reinforcement(int arynumber = -1, Item_Use item_reinforcement = null)
    {
        if (m_gPanel_Reinforcement.activeSelf == true)
        {
            m_gPanel_Reinforcement.SetActive(false);
            m_gPanel_Reinforcement_Equip_Information.SetActive(false);

            return false;
        }
        else
        {
            UpdateReinforcementslot(arynumber, item_reinforcement);
            m_gPanel_Reinforcement.SetActive(true);
            m_gPanel_Reinforcement.transform.SetAsLastSibling();
            GUIManager_Total.Instance.m_GUI_Itemslot_Equip_Information.Init_Scrollbar();
            GUIManager_Total.Instance.m_GUI_Itemslot_Equip_Information.m_gPanel_Itemslot_Equip_Information.SetActive(false);
            GUIManager_Total.Instance.m_GUI_Itemslot_Use_Information.Init_Scrollbar();
            GUIManager_Total.Instance.m_GUI_Itemslot_Use_Information.m_gPanel_Itemslot_Use_Information.SetActive(false);
            GUIManager_Total.Instance.m_GUI_Itemslot_Etc_Information.Init_Scrollbar();
            GUIManager_Total.Instance.m_GUI_Itemslot_Etc_Information.m_gPanel_Itemslot_Etc_Information.SetActive(false);

            return true;
        }
    }

    public void UpdateReinforcementslot(int arynumber, Item_Use item_reinforcement)
    {
        // 소비 아이템 이미지.
        m_IMG_Reinforcement_Information_Image_ItemSprite.sprite = item_reinforcement.m_sp_Sprite;
        m_IMG_Reinforcement_Information_Image_ItemSprite.color = new Color(1, 1, 1, 0.75f);

        // 소비 아이템 분류, 등급, 추가 옵션 등급, 강화 상태.
        // 분류.
        m_TMP_Reinforcement_Information_ItemInformation.text = "";

        switch (item_reinforcement.m_eItemUseType)
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
                    m_sBuffer = "전리품 상자";
                }
                break;
        }
        m_TMP_Reinforcement_Information_ItemInformation.text += "분류: " + m_sBuffer + "\n";
        // 등급.
        switch (item_reinforcement.m_eItemGrade)
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
        m_TMP_Reinforcement_Information_ItemInformation.text += "등급: " + m_sBuffer + "\n";

        m_TMP_Reinforcement_Information_Description.text = item_reinforcement.m_sItemDescription;

        for (int i = 0; i < 60; i++)
        {
            if (Player_Itemslot.m_nary_Itemslot_Equip_Count[i] > 0)
            {
                if (Player_Itemslot.m_gary_Itemslot_Equip[i].m_nReinforcementCount_Max >
                    Player_Itemslot.m_gary_Itemslot_Equip[i].m_nReinforcementCount_Current)
                {
                    m_gary_Reinforcementslot[i].gameObject.GetComponent<Reinforcementslot>().SetItem_Equip(Player_Itemslot.m_gary_Itemslot_Equip[i], i);
                    m_gary_Reinforcementslot[i].SetActive(true);
                }
                else
                {
                    m_gary_Reinforcementslot[i].gameObject.GetComponent<Reinforcementslot>().SetItem_Null();
                    m_gary_Reinforcementslot[i].SetActive(false);
                }

                m_gary_Reinforcementslot[i].gameObject.GetComponent<Reinforcementslot>().m_nReinforcement_AryNubmer = arynumber;
            }
            else
            {
                m_gary_Reinforcementslot[i].SetActive(false);
            }
        }
    }
}
