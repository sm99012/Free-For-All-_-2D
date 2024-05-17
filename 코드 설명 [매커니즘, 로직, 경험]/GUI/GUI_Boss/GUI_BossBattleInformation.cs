using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUI_BossBattleInformation : MonoBehaviour
{
    [SerializeField] GameObject m_gPanel_BossBattleInformation;

    [SerializeField] GameObject m_gPanel_BossBattleInformation_UpBar;
    [SerializeField] TextMeshProUGUI m_TMP_BossBattleInformation_UpBar_Name;
    [SerializeField] Button m_BTN_BossBattleInformation_UpBar_Exit;

    [SerializeField] GameObject m_gPanel_BossBattleInformation_Content;

    [SerializeField] GameObject m_gPanel_BossBattleInformation_Content_UpBar;
    [SerializeField] TextMeshProUGUI m_TMP_BossBattleInformation_Content_UpBar_Name;
    [SerializeField] GameObject m_gBTN_BossBattleInformation_Content_UpBar_R;
    [SerializeField] Button m_BTN_BossBattleInformation_Content_UpBar_R;
    [SerializeField] GameObject m_gBTN_BossBattleInformation_Content_UpBar_L;
    [SerializeField] Button m_BTN_BossBattleInformation_Content_UpBar_L;

    [SerializeField] GameObject m_gPanel_BossBattleInformation_Content_Image;
    [SerializeField] Image m_IMG_BossBattleInformation_Content_Image_ObjectImage;

    [SerializeField] GameObject m_gPanel_BossBattleInformation_Content_Description;
    [SerializeField] TextMeshProUGUI m_TMP_BossBattleInformation_Content_Description;

    [SerializeField] GameObject m_gPanel_BossBattleInformation_Content_Guide;
    [SerializeField] TextMeshProUGUI m_TMP_BossBattleInformation_Content_Guide;

    [SerializeField] Button m_BTN_BossBattleInformation_Start;

    [SerializeField] int m_nBossBattleInformation_Number;
    [SerializeField] int m_nBossBattleInformation_Number_Current;
    [SerializeField] BossBattleDictionary m_BossBattleDictionary;
    [SerializeField] Vector2 m_vSpawnPosition;

    public void InitialSet()
    {
        InitialSet_Object();
        InitialSet_Button();
    }

    void InitialSet_Object()
    {
        m_gPanel_BossBattleInformation = GameObject.Find("Canvas_GUI").gameObject.transform.Find("Panel_BossBattleInformation").gameObject;

        m_gPanel_BossBattleInformation_UpBar = m_gPanel_BossBattleInformation.transform.Find("Panel_BossBattleInformation_UpBar").gameObject;
        m_TMP_BossBattleInformation_UpBar_Name = m_gPanel_BossBattleInformation_UpBar.transform.Find("TMP_BossBattleInformation_UpBar_Name").gameObject.GetComponent<TextMeshProUGUI>();
        m_BTN_BossBattleInformation_UpBar_Exit = m_gPanel_BossBattleInformation_UpBar.transform.Find("BTN_BossBattleInformation_UpBar_Exit").gameObject.GetComponent<Button>();

        m_gPanel_BossBattleInformation_Content = m_gPanel_BossBattleInformation.transform.Find("Panel_BossBattleInformation_Content").gameObject;

        m_gPanel_BossBattleInformation_Content_UpBar = m_gPanel_BossBattleInformation_Content.transform.Find("Panel_BossBattleInformation_Content_UpBar").gameObject;
        m_TMP_BossBattleInformation_Content_UpBar_Name = m_gPanel_BossBattleInformation_Content_UpBar.transform.Find("TMP_BossBattleInformation_Content_UpBar_Name").gameObject.GetComponent<TextMeshProUGUI>();
        m_gBTN_BossBattleInformation_Content_UpBar_R = m_gPanel_BossBattleInformation_Content_UpBar.transform.Find("BTN_BossBattleInformation_Content_UpBar_R").gameObject;
        m_BTN_BossBattleInformation_Content_UpBar_R = m_gBTN_BossBattleInformation_Content_UpBar_R.gameObject.GetComponent<Button>();
        m_gBTN_BossBattleInformation_Content_UpBar_L = m_gPanel_BossBattleInformation_Content_UpBar.transform.Find("BTN_BossBattleInformation_Content_UpBar_L").gameObject;
        m_BTN_BossBattleInformation_Content_UpBar_L = m_gBTN_BossBattleInformation_Content_UpBar_L.gameObject.GetComponent<Button>();

        m_gPanel_BossBattleInformation_Content_Image = m_gPanel_BossBattleInformation_Content.transform.Find("Panel_BossBattleInformation_Content_Image").gameObject;
        m_IMG_BossBattleInformation_Content_Image_ObjectImage = m_gPanel_BossBattleInformation_Content_Image.transform.Find("IMG_BossBattleInformation_Content_Image_ObjectImage").gameObject.GetComponent<Image>();

        m_gPanel_BossBattleInformation_Content_Description = m_gPanel_BossBattleInformation_Content.transform.Find("Panel_BossBattleInformation_Content_Description").gameObject;
        m_TMP_BossBattleInformation_Content_Description = m_gPanel_BossBattleInformation_Content_Description.transform.Find("TMP_BossBattleInformation_Content_Description").gameObject.GetComponent<TextMeshProUGUI>();

        m_gPanel_BossBattleInformation_Content_Guide = m_gPanel_BossBattleInformation_Content.transform.Find("Panel_BossBattleInformation_Content_Guide").gameObject;
        m_TMP_BossBattleInformation_Content_Guide = m_gPanel_BossBattleInformation_Content_Guide.transform.Find("TMP_BossBattleInformation_Content_Guide").gameObject.GetComponent<TextMeshProUGUI>();

        m_BTN_BossBattleInformation_Start = m_gPanel_BossBattleInformation.transform.Find("BTN_BossBattleInformation_Start").gameObject.GetComponent<Button>();
    }
    void InitialSet_Button()
    {
        m_BTN_BossBattleInformation_UpBar_Exit.onClick.RemoveAllListeners();
        m_BTN_BossBattleInformation_UpBar_Exit.onClick.AddListener(delegate { Press_Btn_Exit(); });
        m_BTN_BossBattleInformation_Content_UpBar_R.onClick.RemoveAllListeners();
        m_BTN_BossBattleInformation_Content_UpBar_R.onClick.AddListener(delegate { Press_Btn_R(); });
        m_BTN_BossBattleInformation_Content_UpBar_L.onClick.RemoveAllListeners();
        m_BTN_BossBattleInformation_Content_UpBar_L.onClick.AddListener(delegate { Press_Btn_L(); });
        m_BTN_BossBattleInformation_Start.onClick.RemoveAllListeners();
        m_BTN_BossBattleInformation_Start.onClick.AddListener(delegate { Press_Btn_Start(); });
    }

    public void Press_Btn_Exit()
    {
        m_gPanel_BossBattleInformation.SetActive(false);
        GUIManager_Total.Instance.Delete_GUI_Priority(39);
    }
    void Press_Btn_R()
    {
        m_nBossBattleInformation_Number_Current += 1;

        Update_GUI_BossBattleInformation();

        if (m_nBossBattleInformation_Number_Current == m_nBossBattleInformation_Number - 1)
            m_gBTN_BossBattleInformation_Content_UpBar_R.SetActive(false);
        else
            m_gBTN_BossBattleInformation_Content_UpBar_R.SetActive(true);

        if (m_nBossBattleInformation_Number_Current == 0)
            m_gBTN_BossBattleInformation_Content_UpBar_L.SetActive(false);
        else
            m_gBTN_BossBattleInformation_Content_UpBar_L.SetActive(true);
    }
    void Press_Btn_L()
    {
        m_nBossBattleInformation_Number_Current -= 1;

        Update_GUI_BossBattleInformation();

        if (m_nBossBattleInformation_Number_Current == m_nBossBattleInformation_Number - 1)
            m_gBTN_BossBattleInformation_Content_UpBar_R.SetActive(false);
        else
            m_gBTN_BossBattleInformation_Content_UpBar_R.SetActive(true);

        if (m_nBossBattleInformation_Number_Current == 0)
            m_gBTN_BossBattleInformation_Content_UpBar_L.SetActive(false);
        else
            m_gBTN_BossBattleInformation_Content_UpBar_L.SetActive(true);
    }
    void Press_Btn_Start()
    {
        // 보스전투: '드넓은 초원' 의 진정한 평화를 위한 길
        if (m_BossBattleDictionary.m_nBossBattle_Code == 1)
        {
            BossManager.Instance.Start_Battle_Boss_TeSlime(m_vSpawnPosition);

            m_gPanel_BossBattleInformation.SetActive(false);
        }
    }

    public void Display_GUI_BossBattleInformation(BossBattleDictionary bbd, Vector2 pos)
    {
        m_gPanel_BossBattleInformation.transform.SetAsLastSibling();

        m_gPanel_BossBattleInformation.SetActive(true);

        m_BossBattleDictionary = bbd;

        m_vSpawnPosition = pos;

        m_nBossBattleInformation_Number = bbd.m_nl_Object_Code.Count;
        m_nBossBattleInformation_Number_Current = 0;

        Update_GUI_BossBattleInformation();
    }

    void Update_GUI_BossBattleInformation()
    {
        m_TMP_BossBattleInformation_UpBar_Name.text = m_BossBattleDictionary.m_sBossBattle_Name;

        m_TMP_BossBattleInformation_Content_UpBar_Name.text = MonsterManager.m_Dictionary_Monster[m_BossBattleDictionary.m_nl_Object_Code[m_nBossBattleInformation_Number_Current]].m_sMonster_Name;

        m_IMG_BossBattleInformation_Content_Image_ObjectImage.sprite = MonsterManager.m_Dictionary_Monster[m_BossBattleDictionary.m_nl_Object_Code[m_nBossBattleInformation_Number_Current]].m_spMonster_Sprite;

        m_TMP_BossBattleInformation_Content_Description.text = m_BossBattleDictionary.m_sl_Object_Description[m_nBossBattleInformation_Number_Current];

        m_TMP_BossBattleInformation_Content_Guide.text = m_BossBattleDictionary.m_sl_Object_Guide[m_nBossBattleInformation_Number_Current];

        Check_Btn(m_BossBattleDictionary);
    }

    void Check_Btn(BossBattleDictionary bbd)
    {
        if (m_nBossBattleInformation_Number == 1)
        {
            m_gBTN_BossBattleInformation_Content_UpBar_R.SetActive(false);
            m_gBTN_BossBattleInformation_Content_UpBar_L.SetActive(false);
        }
        else
        {
            m_gBTN_BossBattleInformation_Content_UpBar_R.SetActive(true);
            m_gBTN_BossBattleInformation_Content_UpBar_L.SetActive(false);
        }
    }
}
