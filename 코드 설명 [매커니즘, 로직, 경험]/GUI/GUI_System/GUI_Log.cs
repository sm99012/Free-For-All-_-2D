using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUI_Log : MonoBehaviour
{
    public TextMeshProUGUI m_tm_Log;
    public ScrollRect m_sr_Scroll;
    public Scrollbar m_sb_ScrollBar;
    [SerializeField] Button m_BTN_Log_Clean;
    // Log를 기록할 배열.
    // 가로: 40, 세로: 20
    // 모든 로그 기록은 40자 이내로 처리하도록 한다.
    public List <string> m_List_LogList;
    //public string m_sLog;

    public void InitialSet()
    {
        InitialSet_Object();
        InitialSet_Button();

        m_List_LogList = new List<string>();
        Press_Btn_Log_Clean();
    }
    void InitialSet_Object()
    {
        GameObject canvas = GameObject.Find("Canvas_GUI").gameObject;
        GameObject m_gPanel_LogBox = canvas.transform.Find("Panel_LogBox").gameObject;
        m_tm_Log = m_gPanel_LogBox.transform.Find("Panel_Log").Find("Scroll View").Find("Viewport").Find("Content").Find("Text_Log").gameObject.GetComponent<TextMeshProUGUI>();
        m_sb_ScrollBar = m_gPanel_LogBox.transform.Find("Panel_Log").Find("Scroll View").Find("Scrollbar Vertical").gameObject.GetComponent<Scrollbar>();
        m_BTN_Log_Clean = m_gPanel_LogBox.transform.Find("BTN_Log_Clean").gameObject.GetComponent<Button>();
    }
    void InitialSet_Button()
    {
        m_BTN_Log_Clean.onClick.RemoveAllListeners();
        m_BTN_Log_Clean.onClick.AddListener(delegate { Press_Btn_Log_Clean(); });
    }

    void Press_Btn_Log_Clean()
    {
        m_List_LogList.Clear();
        m_sb_ScrollBar.value = 1;
        m_tm_Log.text = "";
    }

    //int ina = 0;
    //private void Update()
    //{
    //    if (Input.GetKeyUp(KeyCode.M))
    //        UpdateLog((++ina).ToString() + " - \n");
    //}

    public void UpdateLog(string str)
    {
        //Debug.Log("[F: UpdateLog / parameter str] " + str);
        //Debug.Log("[F: UpdateLog / m_List_LogList.Count] " + m_List_LogList.Count);
        m_tm_Log.text = "";
        if (m_List_LogList.Count == 20)
        {
            m_List_LogList.RemoveAt(0);
        }
        m_List_LogList.Add(str);
        
        for (int i = m_List_LogList.Count - 1; i >= 0; i--)
        {
            m_tm_Log.text += m_List_LogList[i] + "\n";
            //Debug.Log("LogUi: " + m_List_LogList[i]);
        }  
    }

}
