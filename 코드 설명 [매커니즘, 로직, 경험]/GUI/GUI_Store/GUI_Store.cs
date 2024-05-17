using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUI_Store : MonoBehaviour
{
    // 상점 GUI.
    [SerializeField] public GameObject m_gPanel_Store;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_Store_UpBar;
    [SerializeField] TextMeshProUGUI m_TMP_Store_UpBar_Name;
    [SerializeField] Button m_BTN_Store_UpBar_Exit;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_Store_Information;
    [SerializeField] GameObject m_gPanel_Store_Information_NPC;
    [SerializeField] Image m_IMG_Store_Information_NPC;

    [SerializeField] GameObject m_gPanel_Store_Information_Description;
    [SerializeField] GameObject m_gSV_Store_Item_Information_ItemInformation;
    [SerializeField] GameObject m_gViewport_Store_Information_ItemInformation;
    [SerializeField] TextMeshProUGUI m_TMP_Store_Information_Description;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_Store_Sale;
    [SerializeField] GameObject m_gPanel_Store_Sale_LeftBar;
    [SerializeField] Button m_BTN_Store_Sale_LeftBar_Equip;
    [SerializeField] Button m_BTN_Store_Sale_LeftBar_Use;
    [SerializeField] Button m_BTN_Store_Sale_LeftBar_Etc;

    [SerializeField] GameObject m_gSV_Store_Sale;
    [SerializeField] GameObject m_gViewport_Store_Sale;
    [SerializeField] GameObject m_gContent_Store_Sale;
    public GameObject[] m_gary_Content_Store_Sale_Slot;
    public E_ITEMSLOT m_e_Sale_Type;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_Store_Buy;
    [SerializeField] GameObject m_gPanel_Store_Buy_LeftBar;
    [SerializeField] Button m_BTN_Store_Buy_LeftBar_Equip;
    [SerializeField] Button m_BTN_Store_Buy_LeftBar_Use;
    [SerializeField] Button m_BTN_Store_Buy_LeftBar_Etc;

    [SerializeField] GameObject m_gSV_Store_Buy;
    [SerializeField] GameObject m_gViewport_Store_Buy;
    [SerializeField] GameObject m_gContent_Store_Buy;
    public GameObject[] m_gary_Content_Store_Buy_Slot;
    public E_ITEMSLOT m_e_Buy_Type;

    [SerializeField] TextMeshProUGUI m_TMP_Gold;

    public NPC_Store m_NPC_Store;
    public bool m_bNPC_Store;

    public void InitialSet()
    {
        InitialSet_Object();
        InitialSet_Button();

        m_gPanel_Store.SetActive(false);
        m_NPC_Store = null;
        m_bNPC_Store = false;
    }

    void InitialSet_Object()
    {
        m_gPanel_Store = GameObject.Find("Canvas_GUI").gameObject.transform.Find("Panel_Store").gameObject;

        m_gPanel_Store_UpBar = m_gPanel_Store.transform.Find("Panel_Store_UpBar").gameObject;
        m_TMP_Store_UpBar_Name = m_gPanel_Store_UpBar.transform.Find("TMP_Store_UpBar_Name").gameObject.GetComponent<TextMeshProUGUI>();
        m_BTN_Store_UpBar_Exit = m_gPanel_Store_UpBar.transform.Find("BTN_Store_UpBar_Exit").gameObject.GetComponent<Button>();

        m_gPanel_Store_Information = m_gPanel_Store.transform.Find("Panel_Store_Information").gameObject;
        m_gPanel_Store_Information_NPC = m_gPanel_Store_Information.transform.Find("Panel_Store_Information_NPC").gameObject;
        m_IMG_Store_Information_NPC = m_gPanel_Store_Information_NPC.transform.Find("IMG_Store_Information_NPC").gameObject.GetComponent<Image>();

        m_gPanel_Store_Information_Description = m_gPanel_Store_Information.transform.Find("Panel_Store_Information_Description").gameObject;
        m_gSV_Store_Item_Information_ItemInformation = m_gPanel_Store_Information_Description.transform.Find("SV_Store_Information_Description").gameObject;
        m_gViewport_Store_Information_ItemInformation = m_gSV_Store_Item_Information_ItemInformation.transform.Find("Viewport_Store_Information_Description").gameObject;
        m_TMP_Store_Information_Description = m_gViewport_Store_Information_ItemInformation.transform.Find("TMP_Store_Information_Description").gameObject.GetComponent<TextMeshProUGUI>();

        m_gPanel_Store_Sale = m_gPanel_Store.transform.Find("Panel_Store_Sale").gameObject;
        m_gPanel_Store_Sale_LeftBar = m_gPanel_Store_Sale.transform.Find("Panel_Store_Sale_LeftBar").gameObject;
        m_BTN_Store_Sale_LeftBar_Equip = m_gPanel_Store_Sale_LeftBar.transform.Find("BTN_Store_Sale_LeftBar_Equip").gameObject.GetComponent<Button>();
        m_BTN_Store_Sale_LeftBar_Use = m_gPanel_Store_Sale_LeftBar.transform.Find("BTN_Store_Sale_LeftBar_Use").gameObject.GetComponent<Button>();
        m_BTN_Store_Sale_LeftBar_Etc = m_gPanel_Store_Sale_LeftBar.transform.Find("BTN_Store_Sale_LeftBar_Etc").gameObject.GetComponent<Button>();

        m_gSV_Store_Sale = m_gPanel_Store_Sale.transform.Find("SV_Store_Sale").gameObject;
        m_gViewport_Store_Sale = m_gSV_Store_Sale.transform.Find("Viewport_Store_Sale").gameObject;
        m_gContent_Store_Sale = m_gViewport_Store_Sale.transform.Find("Content_Store_Sale").gameObject;
        m_gary_Content_Store_Sale_Slot = new GameObject[60];
        for (int i = 0; i < 60; i++)
        {
            m_gary_Content_Store_Sale_Slot[i] = m_gContent_Store_Sale.transform.GetChild(i).gameObject;
            m_gary_Content_Store_Sale_Slot[i].GetComponent<Storeslot>().m_nAryNumber = i;
            m_gary_Content_Store_Sale_Slot[i].GetComponent<Storeslot>().m_e_StoreSlot_Type = E_STORESLOT_EYPE.SALE;
        }
        m_e_Sale_Type = E_ITEMSLOT.EQUIP;

        m_gPanel_Store_Buy = m_gPanel_Store.transform.Find("Panel_Store_Buy").gameObject;
        m_gPanel_Store_Buy_LeftBar = m_gPanel_Store_Buy.transform.Find("Panel_Store_Buy_LeftBar").gameObject;
        m_BTN_Store_Buy_LeftBar_Equip = m_gPanel_Store_Buy_LeftBar.transform.Find("BTN_Store_Buy_LeftBar_Equip").gameObject.GetComponent<Button>();
        m_BTN_Store_Buy_LeftBar_Use = m_gPanel_Store_Buy_LeftBar.transform.Find("BTN_Store_Buy_LeftBar_Use").gameObject.GetComponent<Button>();
        m_BTN_Store_Buy_LeftBar_Etc = m_gPanel_Store_Buy_LeftBar.transform.Find("BTN_Store_Buy_LeftBar_Etc").gameObject.GetComponent<Button>();

        m_gSV_Store_Buy = m_gPanel_Store_Buy.transform.Find("SV_Store_Buy").gameObject;
        m_gViewport_Store_Buy = m_gSV_Store_Buy.transform.Find("Viewport_Store_Buy").gameObject;
        m_gContent_Store_Buy = m_gViewport_Store_Buy.transform.Find("Content_Store_Buy").gameObject;
        m_gary_Content_Store_Buy_Slot = new GameObject[60];
        for (int i = 0; i < 60; i++)
        {
            m_gary_Content_Store_Buy_Slot[i] = m_gContent_Store_Buy.transform.GetChild(i).gameObject;
            m_gary_Content_Store_Buy_Slot[i].GetComponent<Storeslot>().m_nAryNumber = i;
            m_gary_Content_Store_Buy_Slot[i].GetComponent<Storeslot>().m_e_StoreSlot_Type = E_STORESLOT_EYPE.BUY;
        }
        m_e_Buy_Type = E_ITEMSLOT.EQUIP;

        m_TMP_Gold = m_gPanel_Store.transform.Find("Panel").Find("TMP").gameObject.GetComponent<TextMeshProUGUI>();
    }

    void InitialSet_Button()
    {
        m_BTN_Store_UpBar_Exit.onClick.RemoveAllListeners();
        m_BTN_Store_UpBar_Exit.onClick.AddListener(delegate { Press_Btn_Exit(); });
    }
    public void Press_Btn_Exit()
    {
        GUIManager_Total.Instance.m_GUI_Store_Simple_Item_Information.Exit();
        GUIManager_Total.Instance.m_GUI_Store_Item_Information.Exit();

        m_gPanel_Store.SetActive(false);
        m_NPC_Store = null;
        m_bNPC_Store = false;

        GUIManager_Total.Instance.ControlGUI_Interaction_Exit();
        GUIManager_Total.Instance.Delete_GUI_Priority(23);
    }

    void Set_Button(NPC_Store store)
    {
        m_BTN_Store_Sale_LeftBar_Equip.onClick.RemoveAllListeners();
        m_BTN_Store_Sale_LeftBar_Equip.onClick.AddListener(delegate { Press_Btn_Sale_Item_Equip(store); });
        m_BTN_Store_Sale_LeftBar_Use.onClick.RemoveAllListeners();
        m_BTN_Store_Sale_LeftBar_Use.onClick.AddListener(delegate { Press_Btn_Sale_Item_Use(store); });
        m_BTN_Store_Sale_LeftBar_Etc.onClick.RemoveAllListeners();
        m_BTN_Store_Sale_LeftBar_Etc.onClick.AddListener(delegate { Press_Btn_Sale_Item_Etc(store); });
        m_BTN_Store_Buy_LeftBar_Equip.onClick.RemoveAllListeners();
        m_BTN_Store_Buy_LeftBar_Equip.onClick.AddListener(delegate { Press_Btn_Buy_Item_Equip(store); });
        m_BTN_Store_Buy_LeftBar_Use.onClick.RemoveAllListeners();
        m_BTN_Store_Buy_LeftBar_Use.onClick.AddListener(delegate { Press_Btn_Buy_Item_Use(store); });
        m_BTN_Store_Buy_LeftBar_Etc.onClick.RemoveAllListeners();
        m_BTN_Store_Buy_LeftBar_Etc.onClick.AddListener(delegate { Press_Btn_Buy_Item_Etc(store); });
    }
    void Press_Btn_Sale_Item_Equip(NPC_Store store)
    {
        if (m_e_Sale_Type != E_ITEMSLOT.EQUIP)
        {
            m_e_Sale_Type = E_ITEMSLOT.EQUIP;
            UpdateStore(store);
        }
    }
    void Press_Btn_Sale_Item_Use(NPC_Store store)
    {
        if (m_e_Sale_Type != E_ITEMSLOT.USE)
        {
            m_e_Sale_Type = E_ITEMSLOT.USE;
            UpdateStore(store);
        }
    }
    void Press_Btn_Sale_Item_Etc(NPC_Store store)
    {
        if (m_e_Sale_Type != E_ITEMSLOT.ETC)
        {
            m_e_Sale_Type = E_ITEMSLOT.ETC;
            UpdateStore(store);
        }
    }
    void Press_Btn_Buy_Item_Equip(NPC_Store store)
    {
        if (m_e_Buy_Type != E_ITEMSLOT.EQUIP)
        {
            m_e_Buy_Type = E_ITEMSLOT.EQUIP;
            UpdateStore(store);
        }
    }
    void Press_Btn_Buy_Item_Use(NPC_Store store)
    {
        if (m_e_Buy_Type != E_ITEMSLOT.USE)
        {
            m_e_Buy_Type = E_ITEMSLOT.USE;
            UpdateStore(store);
        }
    }
    void Press_Btn_Buy_Item_Etc(NPC_Store store)
    {
        if (m_e_Buy_Type != E_ITEMSLOT.ETC)
        {
            m_e_Buy_Type = E_ITEMSLOT.ETC;
            UpdateStore(store);
        }
    }

    public void Display_GUI_Store(NPC_Store store)
    {
        if (GUIManager_Total.Instance.m_GUI_Itemslot_Equip_Information.m_gPanel_Itemslot_Equip_Information.activeSelf == true)
            GUIManager_Total.Instance.m_GUI_Itemslot_Equip_Information.m_gPanel_Itemslot_Equip_Information.SetActive(false);
        if (GUIManager_Total.Instance.m_GUI_Itemslot_Use_Information.m_gPanel_Itemslot_Use_Information.activeSelf == true)
            GUIManager_Total.Instance.m_GUI_Itemslot_Use_Information.m_gPanel_Itemslot_Use_Information.SetActive(false);
        if (GUIManager_Total.Instance.m_GUI_Itemslot_Etc_Information.m_gPanel_Itemslot_Etc_Information.activeSelf == true)
            GUIManager_Total.Instance.m_GUI_Itemslot_Etc_Information.m_gPanel_Itemslot_Etc_Information.SetActive(false);
        if (GUIManager_Total.Instance.m_GUI_Equipslot_Equip_Information.m_gPanel_Equipslot_Equip_Information.activeSelf == true)
            GUIManager_Total.Instance.m_GUI_Equipslot_Equip_Information.m_gPanel_Equipslot_Equip_Information.SetActive(false);
        if (GUIManager_Total.Instance.m_GUI_Reinforcement.m_gPanel_Reinforcement.activeSelf == true)
            GUIManager_Total.Instance.m_GUI_Reinforcement.m_gPanel_Reinforcement.SetActive(false);
        if (GUIManager_Total.Instance.m_GUI_Reinforcement_Equip_Information.m_gPanel_Reinforcement_Equip_Information.activeSelf == true)
            GUIManager_Total.Instance.m_GUI_Reinforcement_Equip_Information.m_gPanel_Reinforcement_Equip_Information.SetActive(false);

        m_e_Sale_Type = E_ITEMSLOT.EQUIP;
        m_e_Buy_Type = E_ITEMSLOT.EQUIP;

        Set_Button(store);

        UpdateStore(store);
        m_gPanel_Store.SetActive(true);
        m_gPanel_Store.transform.SetAsLastSibling();
        m_NPC_Store = store;
        m_bNPC_Store = true;
    }

    public void UpdateStore(NPC_Store store)
    {
        //m_gPanel_Store.transform.SetAsLastSibling();

        m_TMP_Store_UpBar_Name.text = store.m_sStore_Name;
        m_IMG_Store_Information_NPC.sprite = store.m_Sprite_NPC;
        m_TMP_Store_Information_Description.text = store.m_sDescription;

        for (int i = 0; i < 60; i ++)
        {
            m_gary_Content_Store_Sale_Slot[i].GetComponent<Storeslot>().SetNull();
            m_gary_Content_Store_Buy_Slot[i].GetComponent<Storeslot>().SetNull();
        }

        if (m_e_Sale_Type == E_ITEMSLOT.EQUIP)
        {
            for (int i = 0; i < store.m_List_Sale_Item_Equip_Current.Count; i++)
            {
                if (store.m_List_Sale_Item_Equip_Count[i] > 0)
                {
                    m_gary_Content_Store_Sale_Slot[i].GetComponent<Storeslot>().SetItem_Equip(store.m_List_Sale_Item_Equip_Current[i], store.m_List_Sale_Item_Equip_Count[i]);
                }
            }
        }
        else if (m_e_Sale_Type == E_ITEMSLOT.USE)
        {
            for (int i = 0; i < store.m_List_Sale_Item_Use_Current.Count; i++)
            {
                if (store.m_List_Sale_Item_Use_Count[i] > 0)
                {
                    m_gary_Content_Store_Sale_Slot[i].GetComponent<Storeslot>().SetItem_Use(store.m_List_Sale_Item_Use_Current[i], store.m_List_Sale_Item_Use_Count[i]);
                }
            }
        }
        else if (m_e_Sale_Type == E_ITEMSLOT.ETC)
        {
            for (int i = 0; i < store.m_List_Sale_Item_Etc_Current.Count; i++)
            {
                if (store.m_List_Sale_Item_Etc_Count[i] > 0)
                {
                    m_gary_Content_Store_Sale_Slot[i].GetComponent<Storeslot>().SetItem_Etc(store.m_List_Sale_Item_Etc_Current[i], store.m_List_Sale_Item_Etc_Count[i]);
                }
            }
        }

        if (m_e_Buy_Type == E_ITEMSLOT.EQUIP)
        {
            for (int i = 0; i < 60; i++)
            {
                if (Player_Itemslot.m_nary_Itemslot_Equip_Count[i] > 0)
                {
                    m_gary_Content_Store_Buy_Slot[i].GetComponent<Storeslot>().SetItem_Equip(Player_Itemslot.m_gary_Itemslot_Equip[i], 1);
                }
            }
        }
        else if (m_e_Buy_Type == E_ITEMSLOT.USE)
        {
            for (int i = 0; i < 60; i++)
            {
                if (Player_Itemslot.m_nary_Itemslot_Use_Count[i] > 0)
                {
                    m_gary_Content_Store_Buy_Slot[i].GetComponent<Storeslot>().SetItem_Use(Player_Itemslot.m_gary_Itemslot_Use[i], Player_Itemslot.m_nary_Itemslot_Use_Count[i]);
                }
            }
        }
        else if (m_e_Buy_Type == E_ITEMSLOT.ETC)
        {
            for (int i = 0; i < 60; i++)
            {
                if (Player_Itemslot.m_nary_Itemslot_Etc_Count[i] > 0)
                {
                    m_gary_Content_Store_Buy_Slot[i].GetComponent<Storeslot>().SetItem_Etc(Player_Itemslot.m_gary_Itemslot_Etc[i], Player_Itemslot.m_nary_Itemslot_Etc_Count[i]);
                }
            }
        }

        Update_Gold();

        GUIManager_Total.Instance.Update_Itemslot();
    }

    public void Update_Gold()
    {
        m_TMP_Gold.text = Player_Total.Instance.m_pi_Itemslot.m_nGold.ToString();
    }
}
