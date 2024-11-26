using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUI_Quickslot_Signdown : MonoBehaviour
{
    [SerializeField] public GameObject m_gPanel_Quickslot_Signdown;
    [SerializeField] GameObject m_gPanel_Quickslot_Signdown_UpBar;
    [SerializeField] Button m_BTN_Quickslot_Signdown_Exit;
    [SerializeField] Button m_BTN_Quickslot_Signdown_Signdown;

    public void InitialSet()
    {
        InitialSet_Object();
        InitialSet_Button();
    }

    void InitialSet_Object()
    {
        m_gPanel_Quickslot_Signdown = GameObject.Find("Canvas_GUI").gameObject.transform.Find("Panel_Quickslot_Signdown").gameObject;

        m_gPanel_Quickslot_Signdown_UpBar = m_gPanel_Quickslot_Signdown.transform.Find("Panel_Quickslot_Signdown_UpBar").gameObject;
        m_BTN_Quickslot_Signdown_Exit = m_gPanel_Quickslot_Signdown_UpBar.transform.Find("BTN_Quickslot_Signdown_UpBar_Exit").gameObject.GetComponent<Button>();
        m_BTN_Quickslot_Signdown_Signdown = m_gPanel_Quickslot_Signdown_UpBar.transform.Find("BTN_Quickslot_Signdown_UpBar_Signdown").gameObject.GetComponent<Button>();
    }
    void InitialSet_Button()
    {
        m_BTN_Quickslot_Signdown_Exit.onClick.RemoveAllListeners();
        m_BTN_Quickslot_Signdown_Exit.onClick.AddListener(delegate { Press_Btn_Exit(); });
        //m_BTN_Quickslot_Signdown_Signdown.onClick.RemoveAllListeners();
        //m_BTN_Quickslot_Signdown_Signdown.onClick.AddListener(delegate { Press_Btn_Signdown(); });
    }

    void Press_Btn_Exit()
    {
        m_gPanel_Quickslot_Signdown.SetActive(false);
    }
    void Press_Btn_Signdown(int quickslotnumber)
    {
        GUIManager_Total.Instance.m_GUI_Quickslot.m_qsList_Quickslot[quickslotnumber].Set_Quickslot_Null();
    }

    public void Display_GUI_Quickslot_Signdown(int quickslotnumber)
    {
        m_gPanel_Quickslot_Signdown.SetActive(true);
        m_gPanel_Quickslot_Signdown.transform.SetAsLastSibling();

        int n = quickslotnumber;
        m_BTN_Quickslot_Signdown_Signdown.onClick.RemoveAllListeners();
        m_BTN_Quickslot_Signdown_Signdown.onClick.AddListener(delegate { Press_Btn_Signdown(n); });
    }
    public void UnDisplay_GUI_Quickslot_Signdown()
    {
        m_gPanel_Quickslot_Signdown.SetActive(false);
    }
}
