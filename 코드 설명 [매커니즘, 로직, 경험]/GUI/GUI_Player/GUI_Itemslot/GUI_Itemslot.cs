using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public enum E_ITEMSLOT { EQUIP, USE, ETC }

public class GUI_Itemslot : MonoBehaviour
{
    // Itemslot UI.
    [SerializeField] GameObject m_gPanel_Itemslot;

    [SerializeField] GameObject m_gPanel_Itemslot_Bar;
    [SerializeField] GameObject m_gPanel_Itemslot_Exit;
    [SerializeField] Button m_BTN_Itemslot_Exit;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_Itemslot_Content;
    [SerializeField] GameObject m_gPanel_Itemslot_Content_Category_Equip;
    [SerializeField] GameObject m_gPanel_Itemslot_Content_Category_Use;
    [SerializeField] GameObject m_gPanel_Itemslot_Content_Category_Etc;
    [SerializeField] GameObject m_gPanel_Itemslot_Content_Slot;
    [SerializeField] GameObject m_gPanel_Itemslot_Content_Gold;
    [SerializeField] GameObject m_gScrollView_Itemslot_Content_Slot;
    [SerializeField] GameObject m_gViewport_Itemslot_Content_Slot;
    [SerializeField] GameObject m_gScrollbarVertical_Itemslot_Content_Slot;
    [SerializeField] RectTransform m_RectTransform_ScrollView;
    [Space(20)]
    [SerializeField] GameObject m_gContent_Itemslot_Content_Slot;
    [SerializeField] Button m_BTN_Itemslot_Content_Category_Equip;
    [SerializeField] Button m_BTN_Itemslot_Content_Category_Use;
    [SerializeField] Button m_BTN_Itemslot_Content_Category_Etc;
    [SerializeField] TextMeshProUGUI m_TMP_Itemslot_Content_Gold;

    public E_ITEMSLOT m_eItemslot = E_ITEMSLOT.EQUIP;

    public GameObject[] m_gary_Itemslot;

    public void InitialSet()
    {
        InitialSet_Object();
        InitialSet_Button();
        InitialSet_Itemslot();

        m_gPanel_Itemslot.SetActive(false);
    }

    //private void Update()
    //{
    //    if (Total_Manager.Instance.m_bStart == true)
    //    {
    //        //if (m_gPanel_Itemslot.activeSelf == true)
    //        //{
    //        //    m_TMP_Itemslot_Content_Gold.text = Player_Total.Instance.m_pi_Itemslot.m_nGold.ToString();
    //        //}
    //        //else
    //        //{
    //        //    m_gPanel_Itemslot_Equip_Information.SetActive(false);
    //        //    m_gPanel_Itemslot_Use_Information.SetActive(false);
    //        //    m_gPanel_Itemslot_Etc_Information.SetActive(false);
    //        //}

    //        if (GUIManager_Total.Instance.m_GUI_Reinforcement.m_gPanel_Reinforcement.activeSelf == true)
    //        {
    //            GUIManager_Total.Instance.m_GUI_Itemslot_Equip_Information.Init_Scrollbar();
    //            GUIManager_Total.Instance.m_GUI_Itemslot_Equip_Information.m_gPanel_Itemslot_Equip_Information.SetActive(false);
    //            GUIManager_Total.Instance.m_GUI_Itemslot_Use_Information.Init_Scrollbar();
    //            GUIManager_Total.Instance.m_GUI_Itemslot_Use_Information.m_gPanel_Itemslot_Use_Information.SetActive(false);
    //            GUIManager_Total.Instance.m_GUI_Itemslot_Etc_Information.Init_Scrollbar();
    //            GUIManager_Total.Instance.m_GUI_Itemslot_Etc_Information.m_gPanel_Itemslot_Etc_Information.SetActive(false);
    //        }
    //    }
    //}

    // 초기 Object 불러오기.
    void InitialSet_Object()
    {
        m_gPanel_Itemslot = GameObject.Find("Canvas_GUI").gameObject.transform.Find("Panel_Itemslot").gameObject;

        m_gPanel_Itemslot_Bar = m_gPanel_Itemslot.transform.Find("Panel_Itemslot_Bar").gameObject;
        m_gPanel_Itemslot_Exit = m_gPanel_Itemslot.transform.Find("Panel_Itemslot_Exit").gameObject;
        m_BTN_Itemslot_Exit = m_gPanel_Itemslot_Exit.transform.Find("BTN_Itemslot_Exit").gameObject.GetComponent<Button>();


        m_gPanel_Itemslot_Content = m_gPanel_Itemslot.transform.Find("Panel_Itemslot_Content").gameObject;
        m_gPanel_Itemslot_Content_Category_Equip = m_gPanel_Itemslot_Content.transform.Find("Panel_Itemslot_Content_Category_Equip").gameObject;
        m_BTN_Itemslot_Content_Category_Equip = m_gPanel_Itemslot_Content_Category_Equip.transform.Find("BTN_Itemslot_Content_Category_Equip").gameObject.GetComponent<Button>();

        m_gPanel_Itemslot_Content_Category_Use = m_gPanel_Itemslot_Content.transform.Find("Panel_Itemslot_Content_Category_Use").gameObject;
        m_BTN_Itemslot_Content_Category_Use = m_gPanel_Itemslot_Content_Category_Use.transform.Find("BTN_Itemslot_Content_Category_Use").gameObject.GetComponent<Button>();

        m_gPanel_Itemslot_Content_Category_Etc = m_gPanel_Itemslot_Content.transform.Find("Panel_Itemslot_Content_Category_Etc").gameObject;
        m_BTN_Itemslot_Content_Category_Etc = m_gPanel_Itemslot_Content_Category_Etc.transform.Find("BTN_Itemslot_Content_Category_Etc").gameObject.GetComponent<Button>();

        m_gPanel_Itemslot_Content_Slot = m_gPanel_Itemslot_Content.transform.Find("Panel_Itemslot_Content_Slot").gameObject;
        m_gScrollView_Itemslot_Content_Slot = m_gPanel_Itemslot_Content_Slot.transform.Find("ScrollView_Itemslot_Content_Slot").gameObject;
        m_gViewport_Itemslot_Content_Slot = m_gScrollView_Itemslot_Content_Slot.transform.Find("Viewport_Itemslot_Content_Slot").gameObject;
        m_gContent_Itemslot_Content_Slot = m_gViewport_Itemslot_Content_Slot.transform.Find("Content_Itemslot_Content_Slot").gameObject;
        m_RectTransform_ScrollView = m_gContent_Itemslot_Content_Slot.GetComponent<RectTransform>();

        m_gPanel_Itemslot_Content_Gold = m_gPanel_Itemslot_Content.transform.Find("Panel_Itemslot_Content_Gold").gameObject;
        m_TMP_Itemslot_Content_Gold = m_gPanel_Itemslot_Content_Gold.transform.Find("TMP_Itemslot_Content_Gold").gameObject.GetComponent<TextMeshProUGUI>();

        m_gScrollbarVertical_Itemslot_Content_Slot = m_gScrollView_Itemslot_Content_Slot.transform.Find("ScrollbarVertical_Itemslot_Content_Slot").gameObject;

    }
    // 초기 Button 이벤트 설정.
    void InitialSet_Button()
    {
        m_BTN_Itemslot_Exit.GetComponent<Button>().onClick.AddListener(delegate { Btn_Press_Exit(); });
        m_BTN_Itemslot_Content_Category_Equip.GetComponent<Button>().onClick.AddListener(delegate { Btn_Press_Itemslot_Equip(); });
        m_BTN_Itemslot_Content_Category_Use.GetComponent<Button>().onClick.AddListener(delegate { Btn_Press_Itemslot_Use(); });
        m_BTN_Itemslot_Content_Category_Etc.GetComponent<Button>().onClick.AddListener(delegate { Btn_Press_Itemslot_Etc(); });
    }
    // Itemslot 초기 설정
    public void InitialSet_Itemslot()
    {
        m_gary_Itemslot = new GameObject[60];
        for (int i = 0; i < 60; i++)
        {
            m_gary_Itemslot[i] = m_gContent_Itemslot_Content_Slot.transform.GetChild(i).gameObject;
            m_gary_Itemslot[i].GetComponent<Itemslot>().m_nAryNumber = i;
        }
    }

    public bool Display_GUI_Itemslot()
    {
        if (m_gPanel_Itemslot.activeSelf == true)
        {
            m_gPanel_Itemslot.SetActive(false);
        
            GUIManager_Total.Instance.UnDisplay_GUI_Itemslot_Equip_Information();
            GUIManager_Total.Instance.UnDisplay_GUI_Itemslot_Use_Information();
            GUIManager_Total.Instance.UnDisplay_GUI_Itemslot_Etc_Information();

            GUIManager_Total.Instance.UnDisplay_GUI_Quickslot_Signdown();

            m_TMP_Itemslot_Content_Gold.text = Player_Total.Instance.m_pi_Itemslot.m_nGold.ToString();

            return false;
        }
        else
        {
            m_BTN_Itemslot_Exit.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            Itemslot_Reset();
            m_gPanel_Itemslot.SetActive(true);
            m_gPanel_Itemslot.transform.SetAsLastSibling();

            return true;
        }
    }

    public void UpdateItemslot()
    {
        //if (m_g_ItemslotBox.activeSelf == true)
        {
            switch (m_eItemslot)
            {
                case E_ITEMSLOT.EQUIP:
                    {
                        for (int i = 0; i < 60; i++)
                        {
                            if (Player_Itemslot.m_nary_Itemslot_Equip_Count[i] != 0)
                            {
                                m_gary_Itemslot[i].GetComponent<Itemslot>().SetItem_Equip(Player_Itemslot.m_gary_Itemslot_Equip[i], Player_Itemslot.m_nary_Itemslot_Equip_Count[i]);
                                if (Player_Itemslot.m_nary_Itemslot_Equip_Count[i] == 0)
                                {
                                    Player_Itemslot.m_gary_Itemslot_Equip[i] = null;
                                }
                            }
                            else
                                m_gary_Itemslot[i].GetComponent<Itemslot>().SetNull();
                        }
                    }
                    break;
                case E_ITEMSLOT.USE:
                    {
                        for (int i = 0; i < 60; i++)
                        {
                            if (Player_Itemslot.m_nary_Itemslot_Use_Count[i] != 0)
                            {
                                m_gary_Itemslot[i].GetComponent<Itemslot>().SetItem_Use(Player_Itemslot.m_gary_Itemslot_Use[i], Player_Itemslot.m_nary_Itemslot_Use_Count[i]);
                                if (Player_Itemslot.m_nary_Itemslot_Use_Count[i] == 0)
                                {
                                    Player_Itemslot.m_gary_Itemslot_Use[i] = null;
                                }
                            }
                            else
                                m_gary_Itemslot[i].GetComponent<Itemslot>().SetNull();
                        }
                    }
                    break;
                case E_ITEMSLOT.ETC:
                    {
                        for (int i = 0; i < 60; i++)
                        {
                            if (Player_Itemslot.m_nary_Itemslot_Etc_Count[i] != 0)
                            {
                                m_gary_Itemslot[i].GetComponent<Itemslot>().SetItem_Etc(Player_Itemslot.m_gary_Itemslot_Etc[i], Player_Itemslot.m_nary_Itemslot_Etc_Count[i]);
                                if (Player_Itemslot.m_nary_Itemslot_Etc_Count[i] == 0)
                                {
                                    Player_Itemslot.m_gary_Itemslot_Etc[i] = null;
                                }
                                //Debug.Log("[" + i + "] != null");
                            }
                            else
                            {
                                m_gary_Itemslot[i].GetComponent<Itemslot>().SetNull();
                                //Debug.Log("[" + i + "] == null");
                            }
                        }
                    }
                    break;
            }
        }
    }
    public void UpdateItemslot_Gold()
    {
        if (m_gPanel_Itemslot.activeSelf == true)
        {
            m_TMP_Itemslot_Content_Gold.text = Player_Total.Instance.m_pi_Itemslot.m_nGold.ToString();
        }
    }

    // 아이템 슬롯 초기화.
    void Itemslot_Reset()
    {
        m_gScrollbarVertical_Itemslot_Content_Slot.GetComponent<Scrollbar>().value = 1;
        GUIManager_Total.Instance.m_GUI_Itemslot_Equip_Information.Init_Scrollbar();
        GUIManager_Total.Instance.m_GUI_Itemslot_Equip_Information.m_gPanel_Itemslot_Equip_Information.SetActive(false);
        GUIManager_Total.Instance.m_GUI_Itemslot_Use_Information.Init_Scrollbar();
        GUIManager_Total.Instance.m_GUI_Itemslot_Use_Information.m_gPanel_Itemslot_Use_Information.SetActive(false);
        GUIManager_Total.Instance.m_GUI_Itemslot_Etc_Information.Init_Scrollbar();
        GUIManager_Total.Instance.m_GUI_Itemslot_Etc_Information.m_gPanel_Itemslot_Etc_Information.SetActive(false);
    }

    // 버튼 이벤트 처리
    public void Btn_Press_Exit()
    {
        m_BTN_Itemslot_Exit.GetComponent<Image>().color = new Color(.75f, .75f, .75f, 1);
        //m_gPanel_Itemslot_Equip_Information.SetActive(false);

        Display_GUI_Itemslot();
        GUIManager_Total.Instance.Delete_GUI_Priority(8);
    }
    public void Btn_Press_Itemslot_Equip()
    {
        m_eItemslot = E_ITEMSLOT.EQUIP;

        UpdateItemslot();
        Itemslot_Reset();

        m_BTN_Itemslot_Content_Category_Equip.GetComponent<Image>().color = new Color(.75f, .75f, .75f, 1);
        m_BTN_Itemslot_Content_Category_Use.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Itemslot_Content_Category_Etc.GetComponent<Image>().color = new Color(1, 1, 1, 1);

        m_gPanel_Itemslot.transform.SetAsLastSibling();
    }
    public void Btn_Press_Itemslot_Use()
    {
        m_eItemslot = E_ITEMSLOT.USE;

        UpdateItemslot();
        Itemslot_Reset();

        m_BTN_Itemslot_Content_Category_Equip.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Itemslot_Content_Category_Use.GetComponent<Image>().color = new Color(.75f, .75f, .75f, 1);
        m_BTN_Itemslot_Content_Category_Etc.GetComponent<Image>().color = new Color(1, 1, 1, 1);

        m_gPanel_Itemslot.transform.SetAsLastSibling();
    }
    public void Btn_Press_Itemslot_Etc()
    {
        m_eItemslot = E_ITEMSLOT.ETC;

        UpdateItemslot();
        Itemslot_Reset();

        m_BTN_Itemslot_Content_Category_Equip.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Itemslot_Content_Category_Use.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Itemslot_Content_Category_Etc.GetComponent<Image>().color = new Color(.75f, .75f, .75f, 1);

        m_gPanel_Itemslot.transform.SetAsLastSibling();
    }
}
