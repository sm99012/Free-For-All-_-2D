using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUI_ReTry_Information : MonoBehaviour
{
    [SerializeField] GameObject m_gPanel_ReTry_Information;

    [SerializeField] GameObject m_gPanel_ReTry_Information_UpBar;
    [SerializeField] Button m_BTN_ReTry_Information_Exit;

    [SerializeField] GameObject m_gPanel_ReTry_Information_Content;

    [SerializeField] GameObject m_gPanel_ReTry_Information_Content_BTNList;
    [SerializeField] GameObject m_gBTN_ReTry_Information_Content_BTNList_Item_Equip;
    [SerializeField] Button m_BTN_ReTry_Information_Content_BTNList_Item_Equip;
    [SerializeField] GameObject m_gBTN_ReTry_Information_Content_BTNList_Item_Use;
    [SerializeField] Button m_BTN_ReTry_Information_Content_BTNList_Item_Use;
    [SerializeField] GameObject m_gBTN_ReTry_Information_Content_BTNList_Item_Etc;
    [SerializeField] Button m_BTN_ReTry_Information_Content_BTNList_Item_Etc;

    [SerializeField] GameObject m_gSV_ReTry_Information_Content;
    [SerializeField] GameObject m_gViewport_ReTry_Information_Content;
    [SerializeField] GameObject m_gContent_ReTry_Information_Content;

    [SerializeField] GameObject m_gPanel_ReTry_Information_Content_LostGold;
    [SerializeField] TextMeshProUGUI m_TMP_ReTry_Information_Content_LostGold;

    public List<GameObject> m_gList_ReTryslot;

    GameObject m_gPanel_ReTryslot;

    Dictionary<int, int> m_Dictionary_Temp_Item_Equip, m_Dictionary_Temp_Item_Use, m_Dictionary_Temp_Item_Etc;

    public void InitialSet()
    {
        InitialSet_Object();
        InitialSet_Button();
    }

    void InitialSet_Object()
    {
        m_gPanel_ReTry_Information = GameObject.Find("Canvas_GUI").gameObject.transform.Find("Panel_ReTry_Information").gameObject;

        m_gPanel_ReTry_Information_UpBar = m_gPanel_ReTry_Information.transform.Find("Panel_ReTry_Information_UpBar").gameObject;
        m_BTN_ReTry_Information_Exit = m_gPanel_ReTry_Information_UpBar.transform.Find("BTN_ReTry_Information_Exit").gameObject.GetComponent<Button>();

        m_gPanel_ReTry_Information_Content = m_gPanel_ReTry_Information.transform.Find("Panel_ReTry_Information_Content").gameObject;

        m_gPanel_ReTry_Information_Content_BTNList = m_gPanel_ReTry_Information_Content.transform.Find("Panel_ReTry_Information_Content_BTNList").gameObject;
        m_gBTN_ReTry_Information_Content_BTNList_Item_Equip = m_gPanel_ReTry_Information_Content_BTNList.transform.Find("BTN_ReTry_Information_Content_BTNList_Item_Equip").gameObject;
        m_BTN_ReTry_Information_Content_BTNList_Item_Equip = m_gBTN_ReTry_Information_Content_BTNList_Item_Equip.GetComponent<Button>();
        m_gBTN_ReTry_Information_Content_BTNList_Item_Use = m_gPanel_ReTry_Information_Content_BTNList.transform.Find("BTN_ReTry_Information_Content_BTNList_Item_Use").gameObject;
        m_BTN_ReTry_Information_Content_BTNList_Item_Use = m_gBTN_ReTry_Information_Content_BTNList_Item_Use.GetComponent<Button>();
        m_gBTN_ReTry_Information_Content_BTNList_Item_Etc = m_gPanel_ReTry_Information_Content_BTNList.transform.Find("BTN_ReTry_Information_Content_BTNList_Item_Etc").gameObject;
        m_BTN_ReTry_Information_Content_BTNList_Item_Etc = m_gBTN_ReTry_Information_Content_BTNList_Item_Etc.GetComponent<Button>();

        m_gSV_ReTry_Information_Content = m_gPanel_ReTry_Information_Content.transform.Find("SV_ReTry_Information_Content").gameObject;
        m_gViewport_ReTry_Information_Content = m_gSV_ReTry_Information_Content.transform.Find("Viewport_ReTry_Information_Content").gameObject;
        m_gContent_ReTry_Information_Content = m_gViewport_ReTry_Information_Content.transform.Find("Content_ReTry_Information_Content").gameObject;

        m_gPanel_ReTry_Information_Content_LostGold = m_gPanel_ReTry_Information_Content.transform.Find("Panel_ReTry_Information_Content_LostGold").gameObject;
        m_TMP_ReTry_Information_Content_LostGold = m_gPanel_ReTry_Information_Content_LostGold.transform.Find("TMP_ReTry_Information_Content_LostGold").gameObject.GetComponent<TextMeshProUGUI>();

        m_gPanel_ReTryslot = Resources.Load("Prefab/GUI/Panel_ReTryslot") as GameObject;

        m_gList_ReTryslot = new List<GameObject>();

        m_Dictionary_Temp_Item_Equip = new Dictionary<int, int>();
        m_Dictionary_Temp_Item_Use = new Dictionary<int, int>();
        m_Dictionary_Temp_Item_Etc = new Dictionary<int, int>();
    }
    void InitialSet_Button()
    {
        m_BTN_ReTry_Information_Exit.onClick.RemoveAllListeners();
        m_BTN_ReTry_Information_Exit.onClick.AddListener(delegate { Btn_Press_Exit(); });
    }

    void Btn_Press_Exit()
    {
        m_gBTN_ReTry_Information_Content_BTNList_Item_Equip.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_gBTN_ReTry_Information_Content_BTNList_Item_Use.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_gBTN_ReTry_Information_Content_BTNList_Item_Etc.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_gPanel_ReTry_Information.SetActive(false);

        for (int i = 0; i < m_gList_ReTryslot.Count; i++)
        {
            Destroy(m_gList_ReTryslot[i]);
        }
        m_gList_ReTryslot.Clear();
    }
    void Btn_Press_Item_Equip()
    {
        m_gBTN_ReTry_Information_Content_BTNList_Item_Equip.GetComponent<Image>().color = new Color(0.75f, 0.75f, 0.75f, 1);
        m_gBTN_ReTry_Information_Content_BTNList_Item_Use.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_gBTN_ReTry_Information_Content_BTNList_Item_Etc.GetComponent<Image>().color = new Color(1, 1, 1, 1);

        m_gSV_ReTry_Information_Content.transform.SetAsLastSibling();
        m_gSV_ReTry_Information_Content.SetActive(true);

        for (int i = 0; i < m_gList_ReTryslot.Count; i++)
        {
            Destroy(m_gList_ReTryslot[i]);
        }
        m_gList_ReTryslot.Clear();

        foreach (KeyValuePair<int, int> item in m_Dictionary_Temp_Item_Equip)
        {
            //Debug.Log(item.Value);
            GameObject copypanel = Instantiate(m_gPanel_ReTryslot);
            copypanel.GetComponent<ReTryslot>().Set_Item(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[item.Value]);
            RectTransform contentpos = copypanel.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_ReTry_Information_Content.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
            m_gList_ReTryslot.Add(copypanel);
        }
    }
    void Btn_Press_Item_Use()
    {
        m_gBTN_ReTry_Information_Content_BTNList_Item_Equip.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_gBTN_ReTry_Information_Content_BTNList_Item_Use.GetComponent<Image>().color = new Color(0.75f, 0.75f, 0.75f, 1);
        m_gBTN_ReTry_Information_Content_BTNList_Item_Etc.GetComponent<Image>().color = new Color(1, 1, 1, 1);

        m_gSV_ReTry_Information_Content.transform.SetAsLastSibling();
        m_gSV_ReTry_Information_Content.SetActive(true);

        for (int i = 0; i < m_gList_ReTryslot.Count; i++)
        {
            Destroy(m_gList_ReTryslot[i]);
        }
        m_gList_ReTryslot.Clear();

        foreach (KeyValuePair<int, int> item in m_Dictionary_Temp_Item_Use)
        {
            //Debug.Log(item.Value);
            GameObject copypanel = Instantiate(m_gPanel_ReTryslot);
            copypanel.GetComponent<ReTryslot>().Set_Item(ItemManager.instance.m_Dictionary_MonsterDrop_Use[item.Key], item.Value);
            RectTransform contentpos = copypanel.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_ReTry_Information_Content.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
            m_gList_ReTryslot.Add(copypanel);
        }
    }
    void Btn_Press_Item_Etc()
    {
        m_gBTN_ReTry_Information_Content_BTNList_Item_Equip.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_gBTN_ReTry_Information_Content_BTNList_Item_Use.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_gBTN_ReTry_Information_Content_BTNList_Item_Etc.GetComponent<Image>().color = new Color(0.75f, 0.75f, 0.75f, 1);

        m_gSV_ReTry_Information_Content.transform.SetAsLastSibling();
        m_gSV_ReTry_Information_Content.SetActive(true);

        for (int i = 0; i < m_gList_ReTryslot.Count; i++)
        {
            Destroy(m_gList_ReTryslot[i]);
        }
        m_gList_ReTryslot.Clear();

        foreach (KeyValuePair<int, int> item in m_Dictionary_Temp_Item_Etc)
        {
            GameObject copypanel = Instantiate(m_gPanel_ReTryslot);
            copypanel.GetComponent<ReTryslot>().Set_Item(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[item.Key], item.Value);
            RectTransform contentpos = copypanel.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_ReTry_Information_Content.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);
            m_gList_ReTryslot.Add(copypanel);
        }
    }

    public void Display_GUI_ReTry_Information(Dictionary<int, int> dictionary_item_equip, Dictionary<int, int> dictionary_item_use, Dictionary<int, int> dictionary_item_etc, int lostgold)
    {
        m_gPanel_ReTry_Information.SetActive(true);
        m_gSV_ReTry_Information_Content.SetActive(false);

        m_BTN_ReTry_Information_Content_BTNList_Item_Equip.onClick.RemoveAllListeners();
        m_BTN_ReTry_Information_Content_BTNList_Item_Equip.onClick.AddListener(delegate { Btn_Press_Item_Equip(); });
        m_BTN_ReTry_Information_Content_BTNList_Item_Use.onClick.RemoveAllListeners();
        m_BTN_ReTry_Information_Content_BTNList_Item_Use.onClick.AddListener(delegate { Btn_Press_Item_Use(); });
        m_BTN_ReTry_Information_Content_BTNList_Item_Etc.onClick.RemoveAllListeners();
        m_BTN_ReTry_Information_Content_BTNList_Item_Etc.onClick.AddListener(delegate { Btn_Press_Item_Etc(); });

        m_Dictionary_Temp_Item_Equip = dictionary_item_equip;
        m_Dictionary_Temp_Item_Use = dictionary_item_use;
        m_Dictionary_Temp_Item_Etc = dictionary_item_etc;

        m_TMP_ReTry_Information_Content_LostGold.text = lostgold.ToString() + " 골드";
    }
}
