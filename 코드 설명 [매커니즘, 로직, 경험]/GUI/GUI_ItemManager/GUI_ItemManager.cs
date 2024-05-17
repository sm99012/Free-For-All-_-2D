using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUI_ItemManager : MonoBehaviour
{
    [SerializeField] GameObject m_gPanel_ItemManager;

    [SerializeField] GameObject m_gPanel_ItemManager_UpBar;
    [SerializeField] Button m_BTN_ItemManager_Exit;

    [SerializeField] GameObject m_gPanel_ItemManager_Content;

    [SerializeField] GameObject m_gPanel_ItemManager_Content_Category;
    [SerializeField] Button m_BTN_ItemManager_Content_Category_Equip;
    [SerializeField] Button m_BTN_ItemManager_Content_Category_Use;
    [SerializeField] Button m_BTN_ItemManager_Content_Category_Etc;

    [SerializeField] GameObject m_gSV_ItemManager_Content;
    [SerializeField] GameObject m_gViewport_ItemManager_Content;
    [SerializeField] GameObject m_gContent_ItemManager_Content;

    [SerializeField] GameObject m_gPanel_ItemManager_Slot;

    [SerializeField] List<GameObject> m_gl_ItemManager_Slot;

    public void InitialSet()
    {
        InitialSet_Object();
        InitialSet_Button();
    }

    void InitialSet_Object()
    {
        m_gPanel_ItemManager = GameObject.Find("Canvas_GUI").gameObject.transform.Find("Panel_ItemManager").gameObject;

        m_gPanel_ItemManager_UpBar = m_gPanel_ItemManager.transform.Find("Panel_ItemManager_UpBar").gameObject;
        m_BTN_ItemManager_Exit = m_gPanel_ItemManager_UpBar.transform.Find("BTN_ItemManager_Exit").gameObject.GetComponent<Button>();

        m_gPanel_ItemManager_Content = m_gPanel_ItemManager.transform.Find("Panel_ItemManager_Content").gameObject;
        m_gPanel_ItemManager_Content_Category = m_gPanel_ItemManager_Content.transform.Find("Panel_ItemManager_Content_Category").gameObject;
        m_BTN_ItemManager_Content_Category_Equip = m_gPanel_ItemManager_Content_Category.transform.Find("BTN_ItemManager_Content_Category_Equip").gameObject.GetComponent<Button>();
        m_BTN_ItemManager_Content_Category_Use = m_gPanel_ItemManager_Content_Category.transform.Find("BTN_ItemManager_Content_Category_Use").gameObject.GetComponent<Button>();
        m_BTN_ItemManager_Content_Category_Etc = m_gPanel_ItemManager_Content_Category.transform.Find("BTN_ItemManager_Content_Category_Etc").gameObject.GetComponent<Button>();

        m_gSV_ItemManager_Content = m_gPanel_ItemManager_Content.transform.Find("SV_ItemManager_Content").gameObject;
        m_gViewport_ItemManager_Content = m_gSV_ItemManager_Content.transform.Find("Viewport_ItemManager_Content").gameObject;
        m_gContent_ItemManager_Content = m_gViewport_ItemManager_Content.transform.Find("Content_ItemManager_Content").gameObject;

        m_gPanel_ItemManager_Slot = Resources.Load("Prefab/GUI/Panel_ItemManager_Slot") as GameObject;

        m_gl_ItemManager_Slot = new List<GameObject>();
    }
    void InitialSet_Button()
    {
        m_BTN_ItemManager_Exit.onClick.RemoveAllListeners();
        m_BTN_ItemManager_Exit.onClick.AddListener(delegate { Press_Btn_Exit(); });
        m_BTN_ItemManager_Content_Category_Equip.onClick.RemoveAllListeners();
        m_BTN_ItemManager_Content_Category_Equip.onClick.AddListener(delegate { Press_Btn_Item_Equip(); });
        m_BTN_ItemManager_Content_Category_Use.onClick.RemoveAllListeners();
        m_BTN_ItemManager_Content_Category_Use.onClick.AddListener(delegate { Press_Btn_Item_Use(); });
        m_BTN_ItemManager_Content_Category_Etc.onClick.RemoveAllListeners();
        m_BTN_ItemManager_Content_Category_Etc.onClick.AddListener(delegate { Press_Btn_Item_Etc(); });
    }

    void Press_Btn_Exit()
    {
        Clear();

        m_gPanel_ItemManager.SetActive(false);
    }
    void Press_Btn_Item_Equip()
    {
        Clear(); ;

        foreach (KeyValuePair<int, Item_Equip> item in ItemManager.instance.m_Dictionary_MonsterDrop_Equip)
        {
            GameObject copycontent = Instantiate(m_gPanel_ItemManager_Slot);
            copycontent.GetComponent<ItemManagerSlot>().SetItemInformation(item.Value);

            RectTransform contentpos = copycontent.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_ItemManager_Content.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
            m_gl_ItemManager_Slot.Add(copycontent);
        }
    }
    void Press_Btn_Item_Use()
    {
        Clear(); ;

        foreach (KeyValuePair<int, Item_Use> item in ItemManager.instance.m_Dictionary_MonsterDrop_Use)
        {
            GameObject copycontent = Instantiate(m_gPanel_ItemManager_Slot);
            copycontent.GetComponent<ItemManagerSlot>().SetItemInformation(item.Value);

            RectTransform contentpos = copycontent.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_ItemManager_Content.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
            m_gl_ItemManager_Slot.Add(copycontent);
        }
    }
    void Press_Btn_Item_Etc()
    {
        Clear(); ;

        foreach (KeyValuePair<int, Item_Etc> item in ItemManager.instance.m_Dictionary_MonsterDrop_Etc)
        {
            GameObject copycontent = Instantiate(m_gPanel_ItemManager_Slot);
            copycontent.GetComponent<ItemManagerSlot>().SetItemInformation(item.Value);

            RectTransform contentpos = copycontent.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_ItemManager_Content.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
            m_gl_ItemManager_Slot.Add(copycontent);
        }
    }

    void Clear()
    {
        for (int i = 0; i < m_gl_ItemManager_Slot.Count; i++)
        {
            Destroy(m_gl_ItemManager_Slot[i].gameObject);
        }
        m_gl_ItemManager_Slot.Clear();
    }

    public void Display_GUI_ItemManager()
    {
        if (m_gPanel_ItemManager.activeSelf == true)
        {
            Clear();

            m_gPanel_ItemManager.SetActive(false);
        }
        else
        {
            m_gPanel_ItemManager.transform.SetAsLastSibling();

            m_gPanel_ItemManager.SetActive(true);
        }
    }
}
