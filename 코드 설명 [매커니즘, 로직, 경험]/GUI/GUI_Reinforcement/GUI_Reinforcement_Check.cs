using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUI_Reinforcement_Check : MonoBehaviour
{
    // 강화 성공 / 실패 여부 확인 UI.
    [SerializeField] GameObject m_gPanel_Reinforcement_Check;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_Reinforcement_Check_Content;
    [SerializeField] TextMeshProUGUI m_TMP_Reinforcement_Check_Content;
    [Space(20)]
    [SerializeField] Button m_BTN_Reinforcement_Check_Content_Ok;

    public void InitialSet()
    {
        InitialSet_Object();
        InitialSet_Button();
    }

    void InitialSet_Object()
    {
        m_gPanel_Reinforcement_Check = GameObject.Find("Canvas_GUI").transform.Find("Panel_Reinforcement_Check").gameObject;

        m_gPanel_Reinforcement_Check_Content = m_gPanel_Reinforcement_Check.transform.Find("Panel_Reinforcement_Check_Content").gameObject;
        m_TMP_Reinforcement_Check_Content = m_gPanel_Reinforcement_Check_Content.transform.Find("TMP_Reinforcement_Check_Content").gameObject.GetComponent<TextMeshProUGUI>();
        m_BTN_Reinforcement_Check_Content_Ok = m_gPanel_Reinforcement_Check_Content.transform.Find("BTN_Reinforcement_Check_Content_Ok").gameObject.GetComponent<Button>();
    }

    void InitialSet_Button()
    {
        m_BTN_Reinforcement_Check_Content_Ok.onClick.RemoveAllListeners();
        m_BTN_Reinforcement_Check_Content_Ok.onClick.AddListener(delegate { Press_Btn_Exit(); });
    }

    void Press_Btn_Exit()
    {
        m_gPanel_Reinforcement_Check.SetActive(false);
    }

    public void Display_GUI_Reinforcement_Check(bool bl)
    {
        m_gPanel_Reinforcement_Check.transform.SetAsLastSibling();
        m_gPanel_Reinforcement_Check.SetActive(true);

        if (bl == true)
        {
            m_TMP_Reinforcement_Check_Content.text = "강화성공";
        }
        else
        {
            m_TMP_Reinforcement_Check_Content.text = "강화실패";
        }
    }
}
