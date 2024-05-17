using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUI_Gift_GetItem_Info : MonoBehaviour
{
    [SerializeField] GameObject m_gPanel_Gift_GetItem_Info;

    [SerializeField] GameObject m_gPanel_Gift_GetItem_Info_Content;

    [SerializeField] GameObject m_gPanel_Gift_GetItem_Info_Content_BtnList;
    [SerializeField] GameObject m_gBTN_Item_Equip;
    [SerializeField] Button m_BTN_Item_Equip;
    [SerializeField] GameObject m_gBTN_Item_Use;
    [SerializeField] Button m_BTN_Item_Use;
    [SerializeField] GameObject m_gBTN_Item_Etc;
    [SerializeField] Button m_BTN_Item_Etc;

    [SerializeField] GameObject m_gSV_Gift_GetItem_Info_Content;
    [SerializeField] GameObject m_gViewport_Gift_GetItem_Info_Content;
    [SerializeField] GameObject m_gContent_Gift_GetItem_Info_Content;
    [SerializeField] Scrollbar m_Scrollbar_Gift_GetItem_Info_Content;
    [SerializeField] GameObject m_gPanel_Gift_GetItem_Info_Content_X;

    [SerializeField] GameObject m_gPanel_Gift_GetItem_Info_DownBar;
    [SerializeField] Button m_BTN_Gift_GetItem_Info_DownBar_Exit;

    [SerializeField] public GameObject m_gPanel_Standard_RectTransform;

    [SerializeField] GameObject m_gPanel_ItemInfo;
    List<GameObject> m_gList_Panel_ItemInfo;

    List<int> m_nList_GetItem_Equip;
    List<int> m_nList_GetItem_Equip_Count;
    List<int> m_nList_GetItem_Use;
    List<int> m_nList_GetItem_Use_Count;
    List<int> m_nList_GetItem_Etc;
    List<int> m_nList_GetItem_Etc_Count;

    public void InitialSet()
    {
        InitialSet_Object();
        InitialSet_Button();
    }

    void InitialSet_Object()
    {
        m_gPanel_Gift_GetItem_Info = GameObject.Find("Canvas_GUI").transform.Find("Panel_Gift_GetItem_Info").gameObject;

        m_gPanel_Gift_GetItem_Info_Content = m_gPanel_Gift_GetItem_Info.transform.Find("Panel_Gift_GetItem_Info_Content").gameObject;

        m_gPanel_Gift_GetItem_Info_Content_BtnList = m_gPanel_Gift_GetItem_Info_Content.transform.Find("Panel_Gift_GetItem_Info_Content_BtnList").gameObject;
        m_gBTN_Item_Equip = m_gPanel_Gift_GetItem_Info_Content_BtnList.transform.Find("BTN_Item_Equip").gameObject;
        m_BTN_Item_Equip = m_gBTN_Item_Equip.GetComponent<Button>();
        m_gBTN_Item_Use = m_gPanel_Gift_GetItem_Info_Content_BtnList.transform.Find("BTN_Item_Use").gameObject;
        m_BTN_Item_Use = m_gBTN_Item_Use.GetComponent<Button>();
        m_gBTN_Item_Etc = m_gPanel_Gift_GetItem_Info_Content_BtnList.transform.Find("BTN_Item_Etc").gameObject;
        m_BTN_Item_Etc = m_gBTN_Item_Etc.GetComponent<Button>();

        m_gSV_Gift_GetItem_Info_Content = m_gPanel_Gift_GetItem_Info_Content.transform.Find("SV_Gift_GetItem_Info_Content").gameObject;
        m_gViewport_Gift_GetItem_Info_Content = m_gSV_Gift_GetItem_Info_Content.transform.Find("Viewport_Gift_GetItem_Info_Content").gameObject;
        m_gContent_Gift_GetItem_Info_Content = m_gViewport_Gift_GetItem_Info_Content.transform.Find("Content_Gift_GetItem_Info_Content").gameObject;
        m_Scrollbar_Gift_GetItem_Info_Content = m_gSV_Gift_GetItem_Info_Content.transform.Find("Scrollbar_Gift_GetItem_Info_Content").gameObject.GetComponent<Scrollbar>();
        m_gPanel_Gift_GetItem_Info_Content_X = m_gSV_Gift_GetItem_Info_Content.transform.Find("Panel_Gift_GetItem_Info_Content_X").gameObject;

        m_gPanel_Gift_GetItem_Info_DownBar = m_gPanel_Gift_GetItem_Info.transform.Find("Panel_Gift_GetItem_Info_DownBar").gameObject;
        m_BTN_Gift_GetItem_Info_DownBar_Exit = m_gPanel_Gift_GetItem_Info_DownBar.transform.Find("BTN_Gift_GetItem_Info_DownBar_Exit").gameObject.GetComponent<Button>();

        m_gPanel_Standard_RectTransform = m_gPanel_Gift_GetItem_Info.transform.Find("Panel_Standard_RectTransform").gameObject;

        m_gPanel_ItemInfo = Resources.Load("Prefab/GUI/Panel_ItemInfo") as GameObject;
        m_gList_Panel_ItemInfo = new List<GameObject>();

        m_nList_GetItem_Equip = new List<int>();
        m_nList_GetItem_Equip_Count = new List<int>();
        m_nList_GetItem_Use = new List<int>();
        m_nList_GetItem_Use_Count = new List<int>();
        m_nList_GetItem_Etc = new List<int>();
        m_nList_GetItem_Etc_Count = new List<int>();
    }
    void InitialSet_Button()
    {
        m_BTN_Gift_GetItem_Info_DownBar_Exit.onClick.RemoveAllListeners();
        m_BTN_Gift_GetItem_Info_DownBar_Exit.onClick.AddListener(delegate { Press_Btn_Exit(); });
    }

    void Press_Btn_Exit()
    {
        Clear_List();

        m_gPanel_Gift_GetItem_Info.SetActive(false);

        GUIManager_Total.Instance.m_GUI_Itemslot_Equip_Information.m_gPanel_Itemslot_Equip_Information.SetActive(false);
        GUIManager_Total.Instance.m_GUI_Itemslot_Use_Information.m_gPanel_Itemslot_Use_Information.SetActive(false);
        GUIManager_Total.Instance.m_GUI_Itemslot_Etc_Information.m_gPanel_Itemslot_Etc_Information.SetActive(false);
    }

    void Set_Btn()
    {
        m_BTN_Item_Equip.onClick.RemoveAllListeners();
        m_BTN_Item_Equip.onClick.AddListener(delegate { Press_Btn_Item_Equip(); });
        m_BTN_Item_Use.onClick.RemoveAllListeners();
        m_BTN_Item_Use.onClick.AddListener(delegate { Press_Btn_Item_Use(); });
        m_BTN_Item_Etc.onClick.RemoveAllListeners();
        m_BTN_Item_Etc.onClick.AddListener(delegate { Press_Btn_Item_Etc(); });
    }

    void Press_Btn_Item_Equip()
    {
        m_gPanel_Gift_GetItem_Info_Content_X.SetActive(false);

        m_gBTN_Item_Equip.gameObject.GetComponent<Image>().color = new Color(.75f, .75f, .75f, 1);
        m_gBTN_Item_Use.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_gBTN_Item_Etc.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);

        Clear_List();

        GameObject copypanel;
        RectTransform contentpos;
        for (int i = 0; i < m_nList_GetItem_Equip.Count; i++)
        {
            copypanel = Instantiate(m_gPanel_ItemInfo);
            copypanel.GetComponent<Iteminfo>().Display(Player_Itemslot.m_gary_Itemslot_Equip[m_nList_GetItem_Equip[i]].m_sp_Sprite,
                Player_Itemslot.m_gary_Itemslot_Equip[m_nList_GetItem_Equip[i]].m_sItemName + "\n" + m_nList_GetItem_Equip_Count[i] + " 개",
                E_ITEMSLOT.EQUIP, m_nList_GetItem_Equip[i]);

            contentpos = copypanel.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_Gift_GetItem_Info_Content.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

            m_gList_Panel_ItemInfo.Add(copypanel);
        }
    }
    void Press_Btn_Item_Use()
    {
        m_gPanel_Gift_GetItem_Info_Content_X.SetActive(false);

        m_gBTN_Item_Equip.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_gBTN_Item_Use.gameObject.GetComponent<Image>().color = new Color(.75f, .75f, .75f, 1);
        m_gBTN_Item_Etc.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);

        Clear_List();

        GameObject copypanel;
        RectTransform contentpos;
        for (int i = 0; i < m_nList_GetItem_Use.Count; i++)
        {
            copypanel = Instantiate(m_gPanel_ItemInfo);
            copypanel.GetComponent<Iteminfo>().Display(Player_Itemslot.m_gary_Itemslot_Use[m_nList_GetItem_Use[i]].m_sp_Sprite,
                Player_Itemslot.m_gary_Itemslot_Use[m_nList_GetItem_Use[i]].m_sItemName + "\n" + m_nList_GetItem_Use_Count[i] + " 개",
                E_ITEMSLOT.USE, m_nList_GetItem_Use[i]);

            contentpos = copypanel.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_Gift_GetItem_Info_Content.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

            m_gList_Panel_ItemInfo.Add(copypanel);
        }
    }
    void Press_Btn_Item_Etc()
    {
        m_gPanel_Gift_GetItem_Info_Content_X.SetActive(false);

        m_gBTN_Item_Equip.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_gBTN_Item_Use.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_gBTN_Item_Etc.gameObject.GetComponent<Image>().color = new Color(.75f, .75f, .75f, 1);

        Clear_List();

        GameObject copypanel;
        RectTransform contentpos;
        for (int i = 0; i < m_nList_GetItem_Etc.Count; i++)
        {
            copypanel = Instantiate(m_gPanel_ItemInfo);
            copypanel.GetComponent<Iteminfo>().Display(Player_Itemslot.m_gary_Itemslot_Etc[m_nList_GetItem_Etc[i]].m_sp_Sprite,
                Player_Itemslot.m_gary_Itemslot_Etc[m_nList_GetItem_Etc[i]].m_sItemName + "\n" + m_nList_GetItem_Etc_Count[i] + " 개",
                E_ITEMSLOT.ETC, m_nList_GetItem_Etc[i]);

            contentpos = copypanel.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_Gift_GetItem_Info_Content.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

            m_gList_Panel_ItemInfo.Add(copypanel);
        }
    }

    void Clear_List()
    {
        for (int i = 0; i < m_gList_Panel_ItemInfo.Count; i++)
        {
            Destroy(m_gList_Panel_ItemInfo[i]);
        }
        m_gList_Panel_ItemInfo.Clear();
    }

    public void Display_GUI_Gift_GetItem_Info(Dictionary<int, int> dictionary_equip, Dictionary<int, int> dictionary_equip_count,
                                                Dictionary<int, int> dictionary_use, Dictionary<int, int> dictionary_use_count,
                                                Dictionary<int, int> dictionary_etc, Dictionary<int, int> dictionary_etc_count)
    {
        GUIManager_Total.Instance.m_GUI_Itemslot_Equip_Information.m_gPanel_Itemslot_Equip_Information.SetActive(false);
        GUIManager_Total.Instance.m_GUI_Itemslot_Use_Information.m_gPanel_Itemslot_Use_Information.SetActive(false);
        GUIManager_Total.Instance.m_GUI_Itemslot_Etc_Information.m_gPanel_Itemslot_Etc_Information.SetActive(false);

        Clear_List();

        m_gBTN_Item_Equip.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_gBTN_Item_Use.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_gBTN_Item_Etc.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);

        m_gPanel_Gift_GetItem_Info.SetActive(true);
        m_gPanel_Gift_GetItem_Info.transform.SetAsLastSibling();
        m_gPanel_Gift_GetItem_Info_Content_X.SetActive(true);

        m_nList_GetItem_Equip.Clear();
        m_nList_GetItem_Equip_Count.Clear();
        m_nList_GetItem_Use.Clear();
        m_nList_GetItem_Use_Count.Clear();
        m_nList_GetItem_Etc.Clear();
        m_nList_GetItem_Etc_Count.Clear();

        Set_Btn();

        for (int i = 0; i < dictionary_equip.Count; i++)
        {
            //Debug.Log(dictionary_equip[i] + ": " + Player_Itemslot.m_gary_Itemslot_Equip[i].m_sItemName);
            GUIManager_Total.Instance.UpdateLog("[장비 아이템]" + Player_Itemslot.m_gary_Itemslot_Equip[dictionary_equip[i]].m_sItemName + " 을(를) 획득 하였습니다.");
            m_nList_GetItem_Equip.Add(dictionary_equip[i]);
            m_nList_GetItem_Equip_Count.Add(dictionary_equip_count[i]);
        }
        for (int i = 0; i < dictionary_use.Count; i++)
        {
            //Debug.Log(dictionary_use[i] + ": " + Player_Itemslot.m_gary_Itemslot_Use[i].m_sItemName);
            GUIManager_Total.Instance.UpdateLog("[소비 아이템]" + Player_Itemslot.m_gary_Itemslot_Use[dictionary_use[i]].m_sItemName + " 을(를) 획득 하였습니다.");
            m_nList_GetItem_Use.Add(dictionary_use[i]);
            m_nList_GetItem_Use_Count.Add(dictionary_use_count[i]);
        }
        for (int i = 0; i < dictionary_etc.Count; i++)
        {
            //Debug.Log(dictionary_etc[i] + ": " + Player_Itemslot.m_gary_Itemslot_Etc[i].m_sItemName);
            GUIManager_Total.Instance.UpdateLog("[기타 아이템]" + Player_Itemslot.m_gary_Itemslot_Etc[dictionary_etc[i]].m_sItemName + " 을(를) 획득 하였습니다.");
            m_nList_GetItem_Etc.Add(dictionary_etc[i]);
            m_nList_GetItem_Etc_Count.Add(dictionary_etc_count[i]);
        }
    }
}
