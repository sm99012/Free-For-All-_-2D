using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUI_Quickslot_Signup : MonoBehaviour
{
    [SerializeField] public GameObject m_gPanel_Quickslot_Signup;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_Quickslot_Signup_UpBar;
    [SerializeField] Button m_BTN_Quickslot_Signup_Upbar_Exit;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_Quickslot_Signup_Content;
    [SerializeField] public List<GameObject> m_gBTNList_Quickslot_Signup_Content;
    [SerializeField] public List<Button> m_BTNList_Quickslot_Signup_Content;
    [SerializeField] public List<TextMeshProUGUI> m_TMPList_Quickslot_Signup_Content;

    public int m_nQuickslot_Number;
    public E_QUICKSLOT_CATEGORY m_eQuickslot_Category;
    // 착용할 장비 아이템의 아이템 슬롯 배열 번호.
    public int m_nItem_Equip_AryNumber;
    // 사용할 소비 아이템의 코드 번호.
    public int m_nItem_Use_Code;
    // 확인할 기타 아이템의 코드 번호.
    public int m_nItem_Etc_Code;

    public void InitialSet()
    {
        InitialSet_Object();
        InitialSet_Button();
    }

    void InitialSet_Object()
    {
        m_gPanel_Quickslot_Signup = GameObject.Find("Canvas_GUI").gameObject.transform.Find("Panel_Quickslot_Signup").gameObject;

        m_gPanel_Quickslot_Signup_UpBar = m_gPanel_Quickslot_Signup.transform.Find("Panel_Quickslot_Signup_UpBar").gameObject;
        m_BTN_Quickslot_Signup_Upbar_Exit = m_gPanel_Quickslot_Signup_UpBar.transform.Find("BTN_Quickslot_Signup_UpBar_Exit").gameObject.GetComponent<Button>();

        m_gPanel_Quickslot_Signup_Content = m_gPanel_Quickslot_Signup.transform.Find("Panel_Quickslot_Signup_Content").gameObject;
        m_gBTNList_Quickslot_Signup_Content = new List<GameObject>();
        m_BTNList_Quickslot_Signup_Content = new List<Button>();
        m_TMPList_Quickslot_Signup_Content = new List<TextMeshProUGUI>();
        for (int i = 0; i < m_gPanel_Quickslot_Signup_Content.transform.childCount; i++)
        {
            m_gBTNList_Quickslot_Signup_Content.Add(m_gPanel_Quickslot_Signup_Content.transform.GetChild(i).gameObject);
            m_BTNList_Quickslot_Signup_Content.Add(m_gBTNList_Quickslot_Signup_Content[i].GetComponent<Button>());
            m_TMPList_Quickslot_Signup_Content.Add(m_gBTNList_Quickslot_Signup_Content[i].transform.Find("TMP_Quickslot_Signup").gameObject.GetComponent<TextMeshProUGUI>());
        }
    }
    void InitialSet_Button()
    {
        m_BTN_Quickslot_Signup_Upbar_Exit.onClick.RemoveAllListeners();
        m_BTN_Quickslot_Signup_Upbar_Exit.onClick.AddListener(delegate { Press_Btn_Exit(); });
    }

    public void Set_Button()
    {
        for (int i = 0; i < m_gBTNList_Quickslot_Signup_Content.Count; i++)
        {
            int j = i;
            m_BTNList_Quickslot_Signup_Content[j].onClick.RemoveAllListeners();
            if (m_eQuickslot_Category == E_QUICKSLOT_CATEGORY.EQUIP)
                m_BTNList_Quickslot_Signup_Content[j].onClick.AddListener(delegate { Press_Btn_Signup(j, m_eQuickslot_Category, m_nItem_Equip_AryNumber); });
            else if (m_eQuickslot_Category == E_QUICKSLOT_CATEGORY.USE)
                m_BTNList_Quickslot_Signup_Content[j].onClick.AddListener(delegate { Press_Btn_Signup(j, m_eQuickslot_Category, m_nItem_Use_Code); });
            else if (m_eQuickslot_Category == E_QUICKSLOT_CATEGORY.ETC)
                m_BTNList_Quickslot_Signup_Content[j].onClick.AddListener(delegate { Press_Btn_Signup(j, m_eQuickslot_Category, m_nItem_Etc_Code); });
        }
    }

    void Press_Btn_Exit()
    {
        m_gPanel_Quickslot_Signup.SetActive(false);
    }
    void Press_Btn_Signup(int quickslotarynumber, E_QUICKSLOT_CATEGORY eqc, int number)
    {
        GUIManager_Total.Instance.Set_Quickslot(quickslotarynumber, eqc, number);

        m_gPanel_Quickslot_Signup.SetActive(false);
    }

    public void Display_GUI_Quickslot_Signup(E_QUICKSLOT_CATEGORY eqc, int number, float fcoordination_x, float fcoordination_y)
    {
        m_gPanel_Quickslot_Signup.gameObject.transform.SetAsLastSibling();
        m_gPanel_Quickslot_Signup.SetActive(true);
        m_gPanel_Quickslot_Signup.transform.position = new Vector2(fcoordination_x, fcoordination_y);

        m_eQuickslot_Category = eqc;
        if (m_eQuickslot_Category == E_QUICKSLOT_CATEGORY.EQUIP)
        {
            m_nItem_Equip_AryNumber = number;
            m_nItem_Use_Code = -1;
            m_nItem_Etc_Code = -1;
        }
        else if (m_eQuickslot_Category == E_QUICKSLOT_CATEGORY.USE)
        {
            m_nItem_Equip_AryNumber = -1;
            m_nItem_Use_Code = number;
            m_nItem_Etc_Code = -1;
        }
        else if (m_eQuickslot_Category == E_QUICKSLOT_CATEGORY.ETC)
        {
            m_nItem_Equip_AryNumber = -1;
            m_nItem_Use_Code = -1;
            m_nItem_Etc_Code = number;
        }

        Set_Button();
    }
    public void UnDisplay_GUI_Quickslot_Signup()
    {
        m_gPanel_Quickslot_Signup.SetActive(false);
    }
}
