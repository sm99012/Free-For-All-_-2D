using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUI_UpBar : MonoBehaviour
{
    // GUI UpBar 는 항상 활성화 상태.
    [SerializeField] GameObject m_gPanel_UpBar;
    [Space(20)]

    [SerializeField] GameObject m_gPanel_Stauts;
    [SerializeField] Button m_BTN_Status;

    [SerializeField] GameObject m_gPanel_Skill;
    [SerializeField] Button m_BTN_Skill;

    [SerializeField] GameObject m_gPanel_Equipslot;
    [SerializeField] Button m_BTN_Equipslot;

    [SerializeField] GameObject m_gPanel_Itemslot;
    [SerializeField] Button m_BTN_Itemslot;

    [SerializeField] GameObject m_gPanel_MapName;
    public TextMeshProUGUI m_TMP_UpBar_MapName;

    [SerializeField] GameObject m_gPanel_MapInformation;
    [SerializeField] Button m_BTN_MapInformation;

    [SerializeField] GameObject m_gPanel_Quest;
    [SerializeField] Button m_BTN_Quest;

    [SerializeField] GameObject m_gPanel_Dictionary;
    [SerializeField] Button m_BTN_Dictionary;
    [SerializeField] Image m_IMG_UpBar_MonsterDictionary_Sparkle;
    [SerializeField] float m_fMonsterDictionary_Sparkle;
    static Coroutine m_Coroutine_Dictionary_Alarm = null;

    [SerializeField] GameObject m_gPanel_Option;
    [SerializeField] Button m_BTN_Option;

    [SerializeField] GameObject m_gPanel_Info;
    [SerializeField] Button m_BTN_Info;

    [SerializeField] GameObject m_gPanel_ItemManager;
    [SerializeField] Button m_BTN_ItemManager;

    //[Space(20)]

    public void InitialSet()
    {
        InitialSet_Object();
        InitialSet_Button();
    }

    // 초기 Object 불러오기.
    void InitialSet_Object()
    {
        m_gPanel_UpBar = GameObject.Find("Panel_UpBar");

        m_gPanel_Stauts = m_gPanel_UpBar.transform.Find("Panel_UpBar_Status").gameObject;
        m_BTN_Status = m_gPanel_Stauts.transform.Find("BTN_UpBar_Status").gameObject.GetComponent<Button>();
        m_gPanel_Skill = m_gPanel_UpBar.transform.Find("Panel_UpBar_Skill").gameObject;
        m_BTN_Skill = m_gPanel_Skill.transform.Find("BTN_UpBar_Skill").gameObject.GetComponent<Button>();
        m_gPanel_Equipslot = m_gPanel_UpBar.transform.Find("Panel_UpBar_Equipslot").gameObject;
        m_BTN_Equipslot = m_gPanel_Equipslot.transform.Find("BTN_UpBar_Equipslot").gameObject.GetComponent<Button>();
        m_gPanel_Itemslot = m_gPanel_UpBar.transform.Find("Panel_UpBar_Itemslot").gameObject;
        m_BTN_Itemslot = m_gPanel_Itemslot.transform.Find("BTN_UpBar_Itemslot").gameObject.GetComponent<Button>();
        m_gPanel_MapName = m_gPanel_UpBar.transform.Find("Panel_UpBar_MapName").gameObject;
        m_TMP_UpBar_MapName = m_gPanel_MapName.transform.Find("TMP_UpBar_MapName").gameObject.GetComponent<TextMeshProUGUI>();
        m_gPanel_MapInformation = m_gPanel_UpBar.transform.Find("Panel_UpBar_MapInformation").gameObject;
        m_BTN_MapInformation = m_gPanel_MapInformation.transform.Find("BTN_UpBar_MapInformation").gameObject.GetComponent<Button>();
        m_gPanel_Quest = m_gPanel_UpBar.transform.Find("Panel_UpBar_Quest").gameObject;
        m_BTN_Quest = m_gPanel_Quest.transform.Find("BTN_UpBar_Quest").gameObject.GetComponent<Button>();
        m_gPanel_Dictionary = m_gPanel_UpBar.transform.Find("Panel_UpBar_Dictionary").gameObject;
        m_BTN_Dictionary = m_gPanel_Dictionary.transform.Find("BTN_UpBar_Dictionary").gameObject.GetComponent<Button>();
        m_IMG_UpBar_MonsterDictionary_Sparkle = m_gPanel_Dictionary.transform.Find("Panel_UpBar_Monsterdictionary_Sparkle").gameObject.GetComponent<Image>();
        m_gPanel_Option = m_gPanel_UpBar.transform.Find("Panel_UpBar_Option").gameObject;
        m_BTN_Option = m_gPanel_Option.transform.Find("BTN_UpBar_Option").gameObject.GetComponent<Button>();
        m_gPanel_Info = m_gPanel_UpBar.transform.Find("Panel_UpBar_Info").gameObject;
        m_BTN_Info = m_gPanel_Info.transform.Find("BTN_UpBar_Info").gameObject.GetComponent<Button>();
        m_gPanel_ItemManager = m_gPanel_UpBar.transform.Find("Panel_UpBar_ItemManager").gameObject;
        m_BTN_ItemManager = m_gPanel_ItemManager.transform.Find("BTN_UpBar_ItemManager").gameObject.GetComponent<Button>();
    }
    // 초기 Button 이벤트 추가.
    void InitialSet_Button()
    {
        //m_BTN_Status.onClick.RemoveAllListeners();
        //m_BTN_Status.onClick.AddListener(delegate { Set_BTN_Press_Status(); });
        m_BTN_Skill.onClick.RemoveAllListeners();
        m_BTN_Skill.onClick.AddListener(delegate { Set_BTN_Press_Skill(); });
        //m_BTN_Equipslot.onClick.RemoveAllListeners();
        //m_BTN_Equipslot.onClick.AddListener(delegate { Set_BTN_Press_Equipslot(); });
        m_BTN_Equipslot.onClick.RemoveAllListeners();
        m_BTN_Equipslot.onClick.AddListener(delegate { Set_BTN_Press_ES(); });
        m_BTN_Itemslot.onClick.RemoveAllListeners();
        m_BTN_Itemslot.onClick.AddListener(delegate { Set_BTN_Press_Itemslot(); });
        m_BTN_MapInformation.onClick.RemoveAllListeners();
        m_BTN_MapInformation.onClick.AddListener(delegate { Set_BTN_Press_MapInformation(); });
        m_BTN_Quest.onClick.RemoveAllListeners();
        m_BTN_Quest.onClick.AddListener(delegate { Set_BTN_Press_Quest(); });
        m_BTN_Dictionary.onClick.RemoveAllListeners();
        m_BTN_Dictionary.onClick.AddListener(delegate { Set_BTN_Press_Dictionary(); });
        m_BTN_Option.onClick.RemoveAllListeners();
        m_BTN_Option.onClick.AddListener(delegate { Set_BTN_Press_Option(); });
        m_BTN_Info.onClick.RemoveAllListeners();
        m_BTN_Info.onClick.AddListener(delegate { Set_BTN_Press_Info(); });
        m_BTN_ItemManager.onClick.RemoveAllListeners();
        m_BTN_ItemManager.onClick.AddListener(delegate { Set_BTN_Press_ItemManager(); });
    }

    // NotUse -> Set_BTN_Press_ES()
    void Set_BTN_Press_Equipslot()
    {
        GUIManager_Total.Instance.Display_GUI_Equipslot();
    }
    // NotUse -> Set_BTN_Press_ES()
    void Set_BTN_Press_Status()
    {
        GUIManager_Total.Instance.Display_GUI_Status();
    }
    void Set_BTN_Press_ES()
    {
        GUIManager_Total.Instance.Display_GUI_ES();
    }
    void Set_BTN_Press_Skill()
    {
        //GUIManager_Total.Instance.Display_GUI_Skill();
    }
    void Set_BTN_Press_Itemslot()
    {
        GUIManager_Total.Instance.Display_GUI_Itemslot();
    }
    void Set_BTN_Press_MapInformation()
    {
        //GUIManager_Total.Instance.Display_GUI_MapInformation();
    }
    void Set_BTN_Press_Quest()
    {
        GUIManager_Total.Instance.Display_GUI_Quest();
    }
    void Set_BTN_Press_Dictionary()
    {
        GUIManager_Total.Instance.Display_GUI_Dictionary();
    }
    void Set_BTN_Press_Option()
    {
        GUIManager_Total.Instance.Display_GUI_Option();
    }
    void Set_BTN_Press_Info()
    {
        GUIManager_Total.Instance.Display_GUI_Info();
    }
    void Set_BTN_Press_ItemManager()
    {
        GUIManager_Total.Instance.Display_GUI_ItemManager();
    }
    public void Set_MapName(string mapname)
    {
        m_TMP_UpBar_MapName.text = mapname;
    }

    // --------------------------------------------------------------------------------------------------------------

    public void Sparkle_GUI_Dictionary_ON()
    {
        m_fMonsterDictionary_Sparkle = 0;
        m_IMG_UpBar_MonsterDictionary_Sparkle.color = new Color(m_IMG_UpBar_MonsterDictionary_Sparkle.color.r, m_IMG_UpBar_MonsterDictionary_Sparkle.color.g, m_IMG_UpBar_MonsterDictionary_Sparkle.color.b, m_fMonsterDictionary_Sparkle);
        if (m_Coroutine_Dictionary_Alarm == null)
            m_Coroutine_Dictionary_Alarm = StartCoroutine(Process_Sparkle_Dictionary_Alarm());
    }
    public void Sparkle_GUI_Dictionary_OFF()
    {
        if (m_Coroutine_Dictionary_Alarm != null)
        {
            m_fMonsterDictionary_Sparkle = 0;
            m_IMG_UpBar_MonsterDictionary_Sparkle.color = new Color(m_IMG_UpBar_MonsterDictionary_Sparkle.color.r, m_IMG_UpBar_MonsterDictionary_Sparkle.color.g, m_IMG_UpBar_MonsterDictionary_Sparkle.color.b, m_fMonsterDictionary_Sparkle);
            StopCoroutine(m_Coroutine_Dictionary_Alarm);
            m_Coroutine_Dictionary_Alarm = null;
        }    
    }
    IEnumerator Process_Sparkle_Dictionary_Alarm()
    {
        bool bSparkle = true;
        while (true)
        {
            if (m_fMonsterDictionary_Sparkle <= 0)
                bSparkle = true;
            else if (m_fMonsterDictionary_Sparkle >= 0.5f)
                bSparkle = false;

            if (bSparkle == true)
                m_fMonsterDictionary_Sparkle += 0.05f;
            else
                m_fMonsterDictionary_Sparkle -= 0.05f;

            m_IMG_UpBar_MonsterDictionary_Sparkle.color = new Color(m_IMG_UpBar_MonsterDictionary_Sparkle.color.r, m_IMG_UpBar_MonsterDictionary_Sparkle.color.g, m_IMG_UpBar_MonsterDictionary_Sparkle.color.b, m_fMonsterDictionary_Sparkle);

            yield return new WaitForSeconds(0.05f);
        }
    }
}
