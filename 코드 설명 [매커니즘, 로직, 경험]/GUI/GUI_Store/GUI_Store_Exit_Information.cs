using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUI_Store_Exit_Information : MonoBehaviour
{
    [SerializeField] GameObject m_gPanel_Store_Exit_Information;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_Store_Exit_Information_Content;
    [SerializeField] Button m_BTN_Store_Exit_Information_Content_OK;

    public void InitialSet()
    {
        InitialSet_Object();
        InitialSet_Button();
    }

    void InitialSet_Object()
    {
        m_gPanel_Store_Exit_Information = GameObject.Find("Canvas_GUI").transform.Find("Panel_Store_Exit_Information").gameObject;

        m_gPanel_Store_Exit_Information_Content = m_gPanel_Store_Exit_Information.transform.Find("Panel_Store_Exit_Information_Content").gameObject;
        m_BTN_Store_Exit_Information_Content_OK = m_gPanel_Store_Exit_Information_Content.transform.Find("BTN_Store_Exit_Information_Content_OK").gameObject.GetComponent<Button>();
    }

    void InitialSet_Button()
    {
        m_BTN_Store_Exit_Information_Content_OK.onClick.RemoveAllListeners();
        m_BTN_Store_Exit_Information_Content_OK.onClick.AddListener(delegate { Set_BTN_Exit(); });
    }

    void Set_BTN_Exit()
    {
        m_gPanel_Store_Exit_Information.SetActive(false);
    }

    public void Display()
    {
        m_gPanel_Store_Exit_Information.SetActive(true);
        m_gPanel_Store_Exit_Information.transform.SetAsLastSibling();
    }
}
