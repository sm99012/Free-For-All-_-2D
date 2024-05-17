using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUI_Option : MonoBehaviour
{
    [SerializeField] GameObject m_gPanel_Option;

    [SerializeField] GameObject m_gPanel_Option_UpBar;
    [SerializeField] TextMeshProUGUI m_TMP_Option_UpBar_Name;
    [SerializeField] Button m_BTN_Option_UpBar_Exit;

    [SerializeField] GameObject m_gPanel_Option_Content;

    [SerializeField] GameObject m_gBTN_Option_Content_View;
    [SerializeField] Button m_BTN_Option_Content_View;
    [SerializeField] GameObject m_gBTN_Option_Content_Sound;
    [SerializeField] Button m_BTN_Option_Content_Sound;
    [SerializeField] GameObject m_gBTN_Option_Content_GameExit;
    [SerializeField] Button m_BTN_Option_Content_GameExit;

    // 해상도 변경 패널.
    [SerializeField] GameObject m_gPanel_Option_Content_View;
    
    [SerializeField] Button m_BTN_Option_Content_View_Exit;

    [SerializeField] GameObject m_gDd_Option_Content_View;
    [SerializeField] Button m_Dd_Option_Content_View;
    [SerializeField] TextMeshProUGUI m_TMP_Option_Content_View_Name;
    [SerializeField] GameObject m_gTemplate_Option_Content_View;
    [SerializeField] GameObject m_gViewport_Option_Content_View;
    [SerializeField] GameObject m_gContent_Option_Content_View;
    [SerializeField] List<GameObject> m_gList_Option_Content_View_Item;
    [SerializeField] List<Toggle> m_tl_Option_Content_View_Item;

    // 사운드 변경 패널.
    [SerializeField] GameObject m_gPanel_Option_Content_Sound;

    [SerializeField] Button m_BTN_Option_Content_Sound_Exit;

    [SerializeField] GameObject m_gPanel_Option_Content_Sound_Grid;

    [SerializeField] GameObject m_gPanel_Option_Content_Sound_Grid_Total;
    [SerializeField] Toggle m_TG_Option_Content_Sound_Grid_Total;
    [SerializeField] Scrollbar m_Scrollbar_Option_Content_Sound_Grid_Total;

    [SerializeField] GameObject m_gPanel_Option_Content_Sound_Grid_Background;
    [SerializeField] Toggle m_TG_Option_Content_Sound_Grid_Background;
    [SerializeField] Scrollbar m_Scrollbar_Option_Content_Sound_Grid_Background;

    [SerializeField] GameObject m_gPanel_Option_Content_Sound_Grid_Effect;
    [SerializeField] Toggle m_TG_Option_Content_Sound_Grid_Effect;
    [SerializeField] Scrollbar m_Scrollbar_Option_Content_Sound_Grid_Effect;

    public void InitialSet()
    {
        InitialSet_Object();
        InitialSet_Button();
    }

    void InitialSet_Object()
    {
        m_gPanel_Option = GameObject.Find("Canvas_GUI").transform.Find("Panel_Option").gameObject;

        m_gPanel_Option_UpBar = m_gPanel_Option.transform.Find("Panel_Option_UpBar").gameObject;
        m_TMP_Option_UpBar_Name = m_gPanel_Option_UpBar.transform.Find("TMP_Option_UpBar_Name").gameObject.GetComponent<TextMeshProUGUI>();
        m_BTN_Option_UpBar_Exit = m_gPanel_Option_UpBar.transform.Find("BTN_Option_Exit").gameObject.GetComponent<Button>();

        m_gPanel_Option_Content = m_gPanel_Option.transform.Find("Panel_Option_Content").gameObject;
        m_gBTN_Option_Content_View = m_gPanel_Option_Content.transform.Find("BTN_Option_Content_View").gameObject;
        m_BTN_Option_Content_View = m_gBTN_Option_Content_View.GetComponent<Button>();
        m_gBTN_Option_Content_Sound = m_gPanel_Option_Content.transform.Find("BTN_Option_Content_Sound").gameObject;
        m_BTN_Option_Content_Sound = m_gBTN_Option_Content_Sound.GetComponent<Button>();
        m_gBTN_Option_Content_GameExit = m_gPanel_Option_Content.transform.Find("BTN_Option_Content_GameExit").gameObject;
        m_BTN_Option_Content_GameExit = m_gBTN_Option_Content_GameExit.GetComponent<Button>();

        // 해상도 변경 패널.
        m_gPanel_Option_Content_View = m_gPanel_Option.transform.Find("Panel_Option_Content_View").gameObject;
        m_BTN_Option_Content_View_Exit = m_gPanel_Option_Content_View.transform.Find("BTN_Option_Content_View_Exit").gameObject.GetComponent<Button>();

        m_gDd_Option_Content_View = m_gPanel_Option_Content_View.transform.Find("Dd_Option_Content_View").gameObject;
        m_Dd_Option_Content_View = m_gDd_Option_Content_View.GetComponent<Button>();
        m_TMP_Option_Content_View_Name = m_gDd_Option_Content_View.transform.Find("TMP_Option_Content_View_Name").gameObject.GetComponent<TextMeshProUGUI>();
        m_gTemplate_Option_Content_View = m_gDd_Option_Content_View.transform.Find("Template_Option_Content_View").gameObject;
        m_gViewport_Option_Content_View = m_gTemplate_Option_Content_View.transform.Find("Viewport_Option_Content_View").gameObject;
        m_gContent_Option_Content_View = m_gViewport_Option_Content_View.transform.Find("Content_Option_Content_View").gameObject;

        m_gList_Option_Content_View_Item = new List<GameObject>();
        m_tl_Option_Content_View_Item = new List<Toggle>();
        for (int i = 0; i < m_gContent_Option_Content_View.transform.childCount; i++)
        {
            m_gList_Option_Content_View_Item.Add(m_gContent_Option_Content_View.transform.GetChild(i).gameObject);
            m_tl_Option_Content_View_Item.Add(m_gList_Option_Content_View_Item[i].GetComponent<Toggle>());
        }

        // 사운드 변경 패널.
        m_gPanel_Option_Content_Sound = m_gPanel_Option.transform.Find("Panel_Option_Content_Sound").gameObject;
        m_BTN_Option_Content_Sound_Exit = m_gPanel_Option_Content_Sound.transform.Find("BTN_Option_Content_Sound_Exit").gameObject.GetComponent<Button>();

        m_gPanel_Option_Content_Sound_Grid = m_gPanel_Option_Content_Sound.transform.Find("Panel_Option_Content_Sound_Grid").gameObject;

        m_gPanel_Option_Content_Sound_Grid_Total = m_gPanel_Option_Content_Sound_Grid.transform.Find("Panel_Option_Content_Sound_Grid_Total").gameObject;
        m_TG_Option_Content_Sound_Grid_Total = m_gPanel_Option_Content_Sound_Grid_Total.transform.Find("TG_Option_Content_Sound_Grid_Total").gameObject.GetComponent<Toggle>();
        m_Scrollbar_Option_Content_Sound_Grid_Total = m_gPanel_Option_Content_Sound_Grid_Total.transform.Find("Scrollbar_Option_Content_Sound_Grid_Total").gameObject.GetComponent<Scrollbar>();

        m_gPanel_Option_Content_Sound_Grid_Background = m_gPanel_Option_Content_Sound_Grid.transform.Find("Panel_Option_Content_Sound_Grid_Background").gameObject;
        m_TG_Option_Content_Sound_Grid_Background = m_gPanel_Option_Content_Sound_Grid_Background.transform.Find("TG_Option_Content_Sound_Grid_Background").gameObject.GetComponent<Toggle>();
        m_Scrollbar_Option_Content_Sound_Grid_Background = m_gPanel_Option_Content_Sound_Grid_Background.transform.Find("Scrollbar_Option_Content_Sound_Grid_Background").gameObject.GetComponent<Scrollbar>();

        m_gPanel_Option_Content_Sound_Grid_Effect = m_gPanel_Option_Content_Sound_Grid.transform.Find("Panel_Option_Content_Sound_Grid_Effect").gameObject;
        m_TG_Option_Content_Sound_Grid_Effect = m_gPanel_Option_Content_Sound_Grid_Effect.transform.Find("TG_Option_Content_Sound_Grid_Effect").gameObject.GetComponent<Toggle>();
        m_Scrollbar_Option_Content_Sound_Grid_Effect = m_gPanel_Option_Content_Sound_Grid_Effect.transform.Find("Scrollbar_Option_Content_Sound_Grid_Effect").gameObject.GetComponent<Scrollbar>();
    }

    void InitialSet_Button()
    {
        m_BTN_Option_UpBar_Exit.onClick.RemoveAllListeners();
        m_BTN_Option_UpBar_Exit.onClick.AddListener(delegate { Press_Btn_Exit(); });

        m_BTN_Option_Content_View.onClick.RemoveAllListeners();
        m_BTN_Option_Content_View.onClick.AddListener(delegate { Press_Btn_View(); });
        m_BTN_Option_Content_View_Exit.onClick.RemoveAllListeners();
        m_BTN_Option_Content_View_Exit.onClick.AddListener(delegate { Press_Btn_View_Exit(); });

        for (int i = 0; i < m_tl_Option_Content_View_Item.Count; i++)
        {
            int j = i;
            m_tl_Option_Content_View_Item[i].onValueChanged.RemoveAllListeners();
            m_tl_Option_Content_View_Item[i].onValueChanged.AddListener(delegate { Press_Toggle_View_Item(j); });
        }
        m_Dd_Option_Content_View.onClick.RemoveAllListeners();
        m_Dd_Option_Content_View.onClick.AddListener(delegate { Press_Dropdown_View(); });

        m_BTN_Option_Content_Sound.onClick.RemoveAllListeners();
        m_BTN_Option_Content_Sound.onClick.AddListener(delegate { Press_Btn_Sound(); });
        m_BTN_Option_Content_Sound_Exit.onClick.RemoveAllListeners();
        m_BTN_Option_Content_Sound_Exit.onClick.AddListener(delegate { Press_Btn_Sound_Exit(); });

        m_TG_Option_Content_Sound_Grid_Total.onValueChanged.RemoveAllListeners();
        m_TG_Option_Content_Sound_Grid_Total.onValueChanged.AddListener(delegate { Press_Toggle_Sound_Total(); });
        m_TG_Option_Content_Sound_Grid_Background.onValueChanged.RemoveAllListeners();
        m_TG_Option_Content_Sound_Grid_Background.onValueChanged.AddListener(delegate { Press_Toggle_Sound_Background(); });
        m_TG_Option_Content_Sound_Grid_Effect.onValueChanged.RemoveAllListeners();
        m_TG_Option_Content_Sound_Grid_Effect.onValueChanged.AddListener(delegate { Press_Toggle_Sound_Effect(); });

        m_Scrollbar_Option_Content_Sound_Grid_Total.onValueChanged.RemoveAllListeners();
        m_Scrollbar_Option_Content_Sound_Grid_Total.onValueChanged.AddListener(delegate { Press_Scrollbar_Sound_Total(); } );
        m_Scrollbar_Option_Content_Sound_Grid_Background.onValueChanged.RemoveAllListeners();
        m_Scrollbar_Option_Content_Sound_Grid_Background.onValueChanged.AddListener(delegate { Press_Scrollbar_Sound_Background(); });
        m_Scrollbar_Option_Content_Sound_Grid_Effect.onValueChanged.RemoveAllListeners();
        m_Scrollbar_Option_Content_Sound_Grid_Effect.onValueChanged.AddListener(delegate { Press_Scrollbar_Sound_Effect(); });

        m_BTN_Option_Content_GameExit.onClick.RemoveAllListeners();
        m_BTN_Option_Content_GameExit.onClick.AddListener(delegate { Press_Btn_GameExit(); });
    }

    void Press_Btn_Exit()
    {
        Display_GUI_Option();
        GUIManager_Total.Instance.Delete_GUI_Priority(37);

        m_TMP_Option_UpBar_Name.text = "일시중지 [ ESC ]";
    }
    // 해상도 버튼 설정.
    void Press_Btn_View()
    {
        m_gPanel_Option_Content_View.SetActive(true);
        m_TMP_Option_UpBar_Name.text = "해상도 설정 [ ESC ]";
    }
    void Press_Btn_View_Exit()
    {
        m_gPanel_Option_Content_View.SetActive(false);
        m_TMP_Option_UpBar_Name.text = "일시중지 [ ESC ]";
    }
    void Press_Dropdown_View()
    {
        m_gTemplate_Option_Content_View.SetActive(true);
    }
    void Press_Toggle_View_Item(int number)
    {
        switch (number)
        {
            case 0:
                {
                    m_TMP_Option_Content_View_Name.text = "현재 해상도: 1600 X 900";
                    Screen.SetResolution(SetScreen.SDictionary_ScreenSize[1600].m_nHorizontalValue, SetScreen.SDictionary_ScreenSize[1600].m_nVerticalValue, false);

                } break;
            case 1:
                {
                    m_TMP_Option_Content_View_Name.text = "현재 해상도: 1440 X 810";
                    Screen.SetResolution(SetScreen.SDictionary_ScreenSize[1440].m_nHorizontalValue, SetScreen.SDictionary_ScreenSize[1440].m_nVerticalValue, false);
                }
                break;
            case 2:
                {
                    m_TMP_Option_Content_View_Name.text = "현재 해상도: 1280 X 720";
                    Screen.SetResolution(SetScreen.SDictionary_ScreenSize[1280].m_nHorizontalValue, SetScreen.SDictionary_ScreenSize[1280].m_nVerticalValue, false);
                }
                break;
            case 3:
                {
                    m_TMP_Option_Content_View_Name.text = "현재 해상도: 1120 X 630";
                    Screen.SetResolution(SetScreen.SDictionary_ScreenSize[1120].m_nHorizontalValue, SetScreen.SDictionary_ScreenSize[1120].m_nVerticalValue, false);
                }
                break;
            case 4:
                {
                    m_TMP_Option_Content_View_Name.text = "현재 해상도: 960 X 540";
                    Screen.SetResolution(SetScreen.SDictionary_ScreenSize[960].m_nHorizontalValue, SetScreen.SDictionary_ScreenSize[960].m_nVerticalValue, false);
                }
                break;
            case 5:
                {
                    m_TMP_Option_Content_View_Name.text = "현재 해상도: 800 X 450";
                    Screen.SetResolution(SetScreen.SDictionary_ScreenSize[800].m_nHorizontalValue, SetScreen.SDictionary_ScreenSize[800].m_nVerticalValue, false);
                }
                break;
        }

        m_gTemplate_Option_Content_View.SetActive(false);
    }
    // 사운드 버튼 설정.
    void Press_Btn_Sound()
    {
        m_gPanel_Option_Content_Sound.SetActive(true);
        m_TMP_Option_UpBar_Name.text = "사운드 설정 [ ESC ]";
    }
    void Press_Btn_Sound_Exit()
    {
        m_gPanel_Option_Content_Sound.SetActive(false);
        m_TMP_Option_UpBar_Name.text = "일시중지 [ ESC ]";
    }
    void Press_Toggle_Sound_Total()
    {
        if (m_TG_Option_Content_Sound_Grid_Total.isOn == true)
        {
            Debug.Log("전체 음소거 해제.");
        }
        else
        {
            Debug.Log("전체 음소거.");
        }
    }
    void Press_Toggle_Sound_Background()
    {
        if (m_TG_Option_Content_Sound_Grid_Background.isOn == true)
        {
            Debug.Log("배경음악 음소거 해제.");
        }
        else
        {
            Debug.Log("배경음악 음소거.");
        }
    }
    void Press_Toggle_Sound_Effect()
    {
        if (m_TG_Option_Content_Sound_Grid_Effect.isOn == true)
        {
            Debug.Log("효과음 음소거 해제.");
        }
        else
        {
            Debug.Log("효과음 음소거.");
        }
    }
    void Press_Scrollbar_Sound_Total()
    {

    }
    void Press_Scrollbar_Sound_Background()
    {

    }

    void Press_Scrollbar_Sound_Effect()
    {

    }
    // 게임 종료.
    void Press_Btn_GameExit()
    {
        StopAllCoroutines();
        DataManager.instance.SaveData();
        Application.Quit();
    }


    public bool Display_GUI_Option()
    {
        if (m_gPanel_Option.activeSelf == true)
        {
            m_gPanel_Option.SetActive(false);
            m_gPanel_Option_Content_View.SetActive(false);
            m_gTemplate_Option_Content_View.SetActive(false);
            m_gPanel_Option_Content_Sound.SetActive(false);

            m_TMP_Option_UpBar_Name.text = "일시중지 [ ESC ]";

            Time.timeScale = 1;

            return false;
        }
        else
        {
            m_gPanel_Option.transform.SetAsLastSibling();
            m_gPanel_Option.SetActive(true);

            Time.timeScale = 0;
            if (m_cProcessStop != null)
            {
                StopCoroutine(m_cProcessStop);
                m_cProcessStop = null;
            }
            m_cProcessStop = StartCoroutine(ProcessStop());

            return true;
        }
    }
    Coroutine m_cProcessStop;
    IEnumerator ProcessStop()
    {
        while (Time.timeScale == 0)
        {
            yield return new WaitForSecondsRealtime(0.01f);
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                Display_GUI_Option();
                GUIManager_Total.Instance.Delete_GUI_Priority(37);
            }
        }
        if (m_cProcessStop != null)
        {
            StopCoroutine(m_cProcessStop);
            m_cProcessStop = null;
        }
    }
}
