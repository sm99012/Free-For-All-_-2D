using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUI_QuestStateInfo : MonoBehaviour
{
    [SerializeField] GameObject m_gPanel_QuestStateInfo;
    [SerializeField] TextMeshProUGUI m_TMP_QuestStateInfo;
    
    [SerializeField] GameObject m_gPanel_Effect_Flesh;
    [SerializeField] Image m_IMG_Effect_Flesh;

    bool m_bAlpha_Up = false;

    List<string> m_List_QuestClear_Title;

    public void InitialSet()
    {
        InitialSet_Object();
    }
    void InitialSet_Object()
    {
        m_gPanel_QuestStateInfo = GameObject.Find("Canvas_GUI").transform.Find("Panel_QuestStateInfo").gameObject;
        m_TMP_QuestStateInfo = m_gPanel_QuestStateInfo.transform.Find("TMP_QuestStateInfo").gameObject.GetComponent<TextMeshProUGUI>();

        m_gPanel_Effect_Flesh = m_gPanel_QuestStateInfo.transform.Find("Panel_Effect_Flesh").gameObject;
        m_IMG_Effect_Flesh = m_gPanel_Effect_Flesh.GetComponent<Image>();

        m_List_QuestClear_Title = new List<string>();
    }

    public void Disaply_GUI_QuestStateInfo(string str)
    {
        m_List_QuestClear_Title.Add(str);
        //m_gPanel_QuestStateInfo.transform.SetAsLastSibling();
    }

    public void UnDisaply_GUI_QuestStateInfo(string str)
    {
        for (int i = 0; i < m_List_QuestClear_Title.Count; i++)
        {
            if (m_List_QuestClear_Title[i].Equals(str) == true)
                m_List_QuestClear_Title.RemoveAt(i);
        }
    }

    private void Update()
    {
        if (m_gPanel_Effect_Flesh != null)
        {
            if (m_List_QuestClear_Title.Count > 0)
            {
                m_gPanel_QuestStateInfo.SetActive(true);
                m_TMP_QuestStateInfo.text = m_List_QuestClear_Title[0];

                if (m_IMG_Effect_Flesh.color.a > 0.3f && m_bAlpha_Up == true)
                {
                    m_bAlpha_Up = false;
                }
                else if (m_IMG_Effect_Flesh.color.a < 0f && m_bAlpha_Up == false)
                {
                    m_bAlpha_Up = true;
                }

                if (m_bAlpha_Up == false)
                {
                    m_IMG_Effect_Flesh.color = new Color(m_IMG_Effect_Flesh.color.r, m_IMG_Effect_Flesh.color.g, m_IMG_Effect_Flesh.color.b, m_IMG_Effect_Flesh.color.a - 0.01f);
                }
                else
                {
                    m_IMG_Effect_Flesh.color = new Color(m_IMG_Effect_Flesh.color.r, m_IMG_Effect_Flesh.color.g, m_IMG_Effect_Flesh.color.b, m_IMG_Effect_Flesh.color.a + 0.01f);
                }
            }
            else
            {
                m_gPanel_QuestStateInfo.SetActive(false);
            }
        }
    }
}
