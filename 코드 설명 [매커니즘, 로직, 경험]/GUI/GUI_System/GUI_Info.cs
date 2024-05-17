using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUI_Info : MonoBehaviour
{
    [SerializeField] GameObject m_gPanel_Info;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_Info_Bar;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_Info_Bar_Left;
    [SerializeField] GameObject m_gPanel_Info_Bar_Left_Control;
    [SerializeField] Button m_BTN_Info_Bar_Left_Control;
    [SerializeField] GameObject m_gPanel_Info_Bar_Left_Function;
    [SerializeField] Button m_BTN_Info_Bar_Left_Function;
    [SerializeField] GameObject m_gPanel_Info_Bar_Left_PlayMethod;
    [SerializeField] Button m_BTN_Info_Bar_Left_PlayMethod;
    
    [SerializeField] GameObject m_gPanel_Info_Bar_Up;
    [SerializeField] TextMeshProUGUI m_TMP_Info_Bar_Up_ContentName;
    [SerializeField] Button m_BTN_Info_Bar_Up_R;
    [SerializeField] Button m_BTN_Info_Bar_Up_L;

    [Space(20)]
    [SerializeField] GameObject m_gPanel_Info_Content;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_Info_Content_Control;
    [SerializeField] List<GameObject> m_glPanel_Info_Content_Control;
    List<string> m_sl_Info_Content_Control_Name;
    [SerializeField] int m_nInfo_Content_Control_Number;

    [SerializeField] GameObject m_gPanel_Info_Content_Function;
    [SerializeField] List<GameObject> m_glPanel_Info_Content_Function;
    List<string> m_sl_Info_Content_Function_Name;
    [SerializeField] int m_nInfo_Content_Function_Number;

    [SerializeField] GameObject m_gPanel_Info_Content_PlayMethod;
    [SerializeField] List<GameObject> m_glPanel_Info_Content_PlayMethod;
    List<string> m_sl_Info_Content_PlayMethod_Name;
    [SerializeField] int m_nInfo_Content_PlayMethod_Number;

    enum E_INFORMATION_CATEGORY { CONTROL, FUNCTION, PLAYMETHOD }
    E_INFORMATION_CATEGORY m_eInformation_Category;

    Color m_Color_Original = new Color(1, 1, 1, 1);
    Color m_Color_Press = new Color(.75f, .75f, .75f, 1);

    public void InitialSet()
    {
        InitialSet_Object();
        InitialSet_Button();

        m_eInformation_Category = E_INFORMATION_CATEGORY.CONTROL;
        m_nInfo_Content_Control_Number = 0;
        m_nInfo_Content_Function_Number = 0;

        Press_BTN_Control();
        for (int i = 0; i < m_glPanel_Info_Content_Control.Count; i++)
        {
            m_glPanel_Info_Content_Control[i].SetActive(false);
        }
        m_glPanel_Info_Content_Control[m_nInfo_Content_Control_Number].SetActive(true);
        m_TMP_Info_Bar_Up_ContentName.text = m_sl_Info_Content_Control_Name[m_nInfo_Content_Control_Number];
    }

    void InitialSet_Object()
    {
        m_gPanel_Info = GameObject.Find("Canvas_GUI").transform.Find("Panel_Info").gameObject;

        m_gPanel_Info_Bar = m_gPanel_Info.transform.Find("Panel_Info_Bar").gameObject;

        m_gPanel_Info_Bar_Left = m_gPanel_Info_Bar.transform.Find("Panel_Info_Bar_Left").gameObject;
        m_gPanel_Info_Bar_Left_Control = m_gPanel_Info_Bar_Left.transform.Find("BTN_Info_Bar_Left_Control").gameObject;
        m_BTN_Info_Bar_Left_Control = m_gPanel_Info_Bar_Left.transform.Find("BTN_Info_Bar_Left_Control").gameObject.GetComponent<Button>();
        m_gPanel_Info_Bar_Left_Function = m_gPanel_Info_Bar_Left.transform.Find("BTN_Info_Bar_Left_Function").gameObject;
        m_BTN_Info_Bar_Left_Function = m_gPanel_Info_Bar_Left.transform.Find("BTN_Info_Bar_Left_Function").gameObject.GetComponent<Button>();
        m_gPanel_Info_Bar_Left_PlayMethod = m_gPanel_Info_Bar_Left.transform.Find("BTN_Info_Bar_Left_PlayMethod").gameObject;
        m_BTN_Info_Bar_Left_PlayMethod = m_gPanel_Info_Bar_Left.transform.Find("BTN_Info_Bar_Left_PlayMethod").gameObject.GetComponent<Button>();

        m_gPanel_Info_Bar_Up = m_gPanel_Info_Bar.transform.Find("Panel_Info_Bar_Up").gameObject;
        m_TMP_Info_Bar_Up_ContentName = m_gPanel_Info_Bar_Up.transform.Find("TMP_Info_Bar_Up_ContentName").gameObject.GetComponent<TextMeshProUGUI>();
        m_BTN_Info_Bar_Up_R = m_gPanel_Info_Bar_Up.transform.Find("BTN_Info_Bar_Up_R").gameObject.GetComponent<Button>();
        m_BTN_Info_Bar_Up_L = m_gPanel_Info_Bar_Up.transform.Find("BTN_Info_Bar_Up_L").gameObject.GetComponent<Button>();

        m_gPanel_Info_Content = m_gPanel_Info.transform.Find("Panel_Info_Content").gameObject;

        m_gPanel_Info_Content_Control = m_gPanel_Info_Content.transform.Find("Panel_Info_Content_Control").gameObject;
        m_glPanel_Info_Content_Control = new List<GameObject>();
        m_sl_Info_Content_Control_Name = new List<string>();
        for (int i = 0; i < m_gPanel_Info_Content_Control.transform.childCount; i++)
        {
            m_glPanel_Info_Content_Control.Add(m_gPanel_Info_Content_Control.transform.GetChild(i).gameObject);
        }
        m_sl_Info_Content_Control_Name.Add("[조작: 이동]");
        m_sl_Info_Content_Control_Name.Add("[조작: 공격]");
        m_sl_Info_Content_Control_Name.Add("[조작: 구르기]");
        m_sl_Info_Content_Control_Name.Add("[조작: 놓아주기]");
        m_sl_Info_Content_Control_Name.Add("[조작: NPC 상호작용]");
        m_sl_Info_Content_Control_Name.Add("[조작: 채집]");

        m_gPanel_Info_Content_Function = m_gPanel_Info_Content.transform.Find("Panel_Info_Content_Function").gameObject;
        m_glPanel_Info_Content_Function = new List<GameObject>();
        m_sl_Info_Content_Function_Name = new List<string>();
        for (int i = 0; i < m_gPanel_Info_Content_Function.transform.childCount; i++)
        {
            m_glPanel_Info_Content_Function.Add(m_gPanel_Info_Content_Function.transform.GetChild(i).gameObject);
        }
        m_sl_Info_Content_Function_Name.Add("[기능: 능력치]");
        m_sl_Info_Content_Function_Name.Add("[기능: 아이템]");
        m_sl_Info_Content_Function_Name.Add("[기능: 장비 아이템]");
        m_sl_Info_Content_Function_Name.Add("[기능: 소비 아이템]");
        m_sl_Info_Content_Function_Name.Add("[기능: 기타 아이템]");
        m_sl_Info_Content_Function_Name.Add("[기능: 대화, 퀘스트]");

        m_gPanel_Info_Content_PlayMethod = m_gPanel_Info_Content.transform.Find("Panel_Info_Content_PlayMethod").gameObject;
        m_glPanel_Info_Content_PlayMethod = new List<GameObject>();
        m_sl_Info_Content_PlayMethod_Name = new List<string>();
        for (int i = 0; i < m_gPanel_Info_Content_PlayMethod.transform.childCount; i++)
        {
            m_glPanel_Info_Content_PlayMethod.Add(m_gPanel_Info_Content_PlayMethod.transform.GetChild(i).gameObject);
        }
        m_sl_Info_Content_PlayMethod_Name.Add("[플레이 방식]");
    }

    void InitialSet_Button()
    {
        m_BTN_Info_Bar_Left_Control.onClick.RemoveAllListeners();
        m_BTN_Info_Bar_Left_Control.onClick.AddListener(delegate { Press_BTN_Control(); });
        m_BTN_Info_Bar_Left_Function.onClick.RemoveAllListeners();
        m_BTN_Info_Bar_Left_Function.onClick.AddListener(delegate { Press_BTN_Function(); });
        m_BTN_Info_Bar_Left_PlayMethod.onClick.RemoveAllListeners();
        m_BTN_Info_Bar_Left_PlayMethod.onClick.AddListener(delegate { Press_BTN_PlayMethod(); });
        m_BTN_Info_Bar_Up_R.onClick.RemoveAllListeners();
        m_BTN_Info_Bar_Up_R.onClick.AddListener(delegate { Press_BTN_R(); });
        m_BTN_Info_Bar_Up_L.onClick.RemoveAllListeners();
        m_BTN_Info_Bar_Up_L.onClick.AddListener(delegate { Press_BTN_L(); });
    }
    void Press_BTN_Control()
    {
        m_gPanel_Info_Bar_Left_Control.GetComponent<Image>().color = m_Color_Press;
        m_gPanel_Info_Bar_Left_Function.GetComponent<Image>().color = m_Color_Original;
        m_gPanel_Info_Bar_Left_PlayMethod.GetComponent<Image>().color = m_Color_Original;
        m_gPanel_Info_Content_Control.SetActive(true);
        m_gPanel_Info_Content_Function.SetActive(false);
        m_gPanel_Info_Content_PlayMethod.SetActive(false);
        m_TMP_Info_Bar_Up_ContentName.text = m_sl_Info_Content_Control_Name[m_nInfo_Content_Control_Number];
        m_eInformation_Category = E_INFORMATION_CATEGORY.CONTROL;
    }
    void Press_BTN_Function()
    {
        m_gPanel_Info_Bar_Left_Control.GetComponent<Image>().color = m_Color_Original;
        m_gPanel_Info_Bar_Left_Function.GetComponent<Image>().color = m_Color_Press;
        m_gPanel_Info_Bar_Left_PlayMethod.GetComponent<Image>().color = m_Color_Original;
        m_gPanel_Info_Content_Control.SetActive(false);
        m_gPanel_Info_Content_Function.SetActive(true);
        m_gPanel_Info_Content_PlayMethod.SetActive(false);
        m_TMP_Info_Bar_Up_ContentName.text = m_sl_Info_Content_Function_Name[m_nInfo_Content_Function_Number];
        m_eInformation_Category = E_INFORMATION_CATEGORY.FUNCTION;
    }
    void Press_BTN_PlayMethod()
    {
        m_gPanel_Info_Bar_Left_Control.GetComponent<Image>().color = m_Color_Original;
        m_gPanel_Info_Bar_Left_Function.GetComponent<Image>().color = m_Color_Original;
        m_gPanel_Info_Bar_Left_PlayMethod.GetComponent<Image>().color = m_Color_Press;
        m_gPanel_Info_Content_Control.SetActive(false);
        m_gPanel_Info_Content_Function.SetActive(false);
        m_gPanel_Info_Content_PlayMethod.SetActive(true);
        m_TMP_Info_Bar_Up_ContentName.text = m_sl_Info_Content_PlayMethod_Name[m_nInfo_Content_PlayMethod_Number];
        m_eInformation_Category = E_INFORMATION_CATEGORY.PLAYMETHOD;
    }
    void Press_BTN_R()
    {
        if (m_eInformation_Category == E_INFORMATION_CATEGORY.CONTROL)
        {
            if (m_nInfo_Content_Control_Number == m_glPanel_Info_Content_Control.Count - 1)
            {
                m_nInfo_Content_Control_Number = 0;
            }
            else
            {
                m_nInfo_Content_Control_Number += 1;
            }
            for (int i = 0; i < m_glPanel_Info_Content_Control.Count; i++)
            {
                m_glPanel_Info_Content_Control[i].SetActive(false);
            }
            m_glPanel_Info_Content_Control[m_nInfo_Content_Control_Number].SetActive(true);
            m_TMP_Info_Bar_Up_ContentName.text = m_sl_Info_Content_Control_Name[m_nInfo_Content_Control_Number];
        }
        else if (m_eInformation_Category == E_INFORMATION_CATEGORY.FUNCTION)
        {
            if (m_nInfo_Content_Function_Number == m_glPanel_Info_Content_Function.Count - 1)
            {
                m_nInfo_Content_Function_Number = 0;
            }
            else
            {
                m_nInfo_Content_Function_Number += 1;
            }
            for (int i = 0; i < m_glPanel_Info_Content_Function.Count; i++)
            {
                m_glPanel_Info_Content_Function[i].SetActive(false);
            }
            m_glPanel_Info_Content_Function[m_nInfo_Content_Function_Number].SetActive(true);
            m_TMP_Info_Bar_Up_ContentName.text = m_sl_Info_Content_Function_Name[m_nInfo_Content_Function_Number];
        }
        else
        {
            if (m_nInfo_Content_PlayMethod_Number == m_glPanel_Info_Content_PlayMethod.Count - 1)
            {
                m_nInfo_Content_PlayMethod_Number = 0;
            }
            else
            {
                m_nInfo_Content_PlayMethod_Number += 1;
            }
            for (int i = 0; i < m_glPanel_Info_Content_PlayMethod.Count; i++)
            {
                m_glPanel_Info_Content_PlayMethod[i].SetActive(false);
            }
            m_glPanel_Info_Content_PlayMethod[m_nInfo_Content_PlayMethod_Number].SetActive(true);
            m_TMP_Info_Bar_Up_ContentName.text = m_sl_Info_Content_PlayMethod_Name[m_nInfo_Content_PlayMethod_Number];
        }
    }
    void Press_BTN_L()
    {
        if (m_eInformation_Category == E_INFORMATION_CATEGORY.CONTROL)
        {
            if (m_nInfo_Content_Control_Number == 0)
            {
                m_nInfo_Content_Control_Number = m_glPanel_Info_Content_Control.Count - 1;
            }
            else
            {
                m_nInfo_Content_Control_Number -= 1;
            }

            for (int i = 0; i < m_glPanel_Info_Content_Control.Count; i++)
            {
                m_glPanel_Info_Content_Control[i].SetActive(false);
            }
            m_glPanel_Info_Content_Control[m_nInfo_Content_Control_Number].SetActive(true);
            m_TMP_Info_Bar_Up_ContentName.text = m_sl_Info_Content_Control_Name[m_nInfo_Content_Control_Number];
        }
        else if (m_eInformation_Category == E_INFORMATION_CATEGORY.FUNCTION)
        {
            if (m_nInfo_Content_Function_Number == 0)
            {
                m_nInfo_Content_Function_Number = m_glPanel_Info_Content_Function.Count - 1;
            }
            else
            {
                m_nInfo_Content_Function_Number -= 1;
            }

            for (int i = 0; i < m_glPanel_Info_Content_Function.Count; i++)
            {
                m_glPanel_Info_Content_Function[i].SetActive(false);
            }
            m_glPanel_Info_Content_Function[m_nInfo_Content_Function_Number].SetActive(true);
            m_TMP_Info_Bar_Up_ContentName.text = m_sl_Info_Content_Function_Name[m_nInfo_Content_Function_Number];
        }
        else
        {
            if (m_nInfo_Content_PlayMethod_Number == 0)
            {
                m_nInfo_Content_PlayMethod_Number = m_glPanel_Info_Content_PlayMethod.Count - 1;
            }
            else
            {
                m_nInfo_Content_PlayMethod_Number -= 1;
            }

            for (int i = 0; i < m_glPanel_Info_Content_PlayMethod.Count; i++)
            {
                m_glPanel_Info_Content_PlayMethod[i].SetActive(false);
            }
            m_glPanel_Info_Content_PlayMethod[m_nInfo_Content_PlayMethod_Number].SetActive(true);
            m_TMP_Info_Bar_Up_ContentName.text = m_sl_Info_Content_PlayMethod_Name[m_nInfo_Content_PlayMethod_Number];
        }
    }

    public void Update_GUI_Info()
    {

    }

    public bool Display_GUI_Info()
    {
        if (m_gPanel_Info.activeSelf == true)
        {
            m_gPanel_Info.SetActive(false);

            return false;
        }
        else
        {
            m_gPanel_Info.transform.SetAsLastSibling();
            m_gPanel_Info.SetActive(true);

            return true;
        }
    }
}
