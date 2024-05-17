using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUI_Soc : MonoBehaviour
{
    // SOC UI 활성화 / 비활성화
    // 활성화 시: 종족별 평판 확인 가능.
    // 비활성화 시: 종족별 평판 확인 불가능.
    [SerializeField] GameObject m_gPanel_SOC;
    [SerializeField] GameObject m_gPanel_SOC_Bar;
    [SerializeField] GameObject m_gPanel_SOC_Total;

    // 평판 상태창 펼치기 / 접기
    // m_bDisplaySOC = false: 접기.
    // m_bDisplaySOC = true: 펼치기.
    [SerializeField] Button m_BTN_SOC_UD;
    [SerializeField] bool m_bDisplaySOC;

    [Space(20)]
    public TextMeshProUGUI m_TMP_BTN_SOC_UD;

    public TextMeshProUGUI m_TMP_Soc_Honor;
    public TextMeshProUGUI m_TMP_Soc_Human;
    public TextMeshProUGUI m_TMP_Soc_Animal;
    public TextMeshProUGUI m_TMP_Soc_Slime;
    public TextMeshProUGUI m_TMP_Soc_Skeleton;
    public TextMeshProUGUI m_TMP_Soc_Ents;
    public TextMeshProUGUI m_TMP_Soc_Devil;
    public TextMeshProUGUI m_TMP_Soc_Dragon;
    public TextMeshProUGUI m_TMP_Soc_Shadow;

    public void InitialSet()
    {
        InitialSet_Object();
        InitialSet_Button();

        m_bDisplaySOC = false;

        m_TMP_BTN_SOC_UD.text = "-";

        m_gPanel_SOC_Total.SetActive(true);
        m_gPanel_SOC.transform.SetAsLastSibling();
    }

    // 초기 Object 불러오기.
    void InitialSet_Object()
    {
        m_gPanel_SOC = GameObject.Find("Panel_SOC");

        m_gPanel_SOC_Bar = m_gPanel_SOC.transform.Find("Panel_SOC_Bar").gameObject;
        m_BTN_SOC_UD = m_gPanel_SOC_Bar.transform.Find("BTN_SOC_UD").gameObject.GetComponent<Button>();
        m_TMP_BTN_SOC_UD = m_BTN_SOC_UD.transform.Find("TMP_BTN_SOC_UD").gameObject.GetComponent<TextMeshProUGUI>();

        m_gPanel_SOC_Total = m_gPanel_SOC.transform.Find("Panel_SOC_Total").gameObject;
        m_TMP_Soc_Honor = m_gPanel_SOC_Total.transform.Find("Panel_SOC_Honor").gameObject.transform.Find("TMP_SOC_Honor").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_Soc_Human = m_gPanel_SOC_Total.transform.Find("Panel_SOC_Human").gameObject.transform.Find("TMP_SOC_Human").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_Soc_Animal = m_gPanel_SOC_Total.transform.Find("Panel_SOC_Animal").gameObject.transform.Find("TMP_SOC_Animal").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_Soc_Slime = m_gPanel_SOC_Total.transform.Find("Panel_SOC_Slime").gameObject.transform.Find("TMP_SOC_Slime").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_Soc_Skeleton = m_gPanel_SOC_Total.transform.Find("Panel_SOC_Skeleton").gameObject.transform.Find("TMP_SOC_Skeleton").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_Soc_Ents = m_gPanel_SOC_Total.transform.Find("Panel_SOC_Ents").gameObject.transform.Find("TMP_SOC_Ents").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_Soc_Devil = m_gPanel_SOC_Total.transform.Find("Panel_SOC_Devil").gameObject.transform.Find("TMP_SOC_Devil").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_Soc_Dragon = m_gPanel_SOC_Total.transform.Find("Panel_SOC_Dragon").gameObject.transform.Find("TMP_SOC_Dragon").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_Soc_Shadow = m_gPanel_SOC_Total.transform.Find("Panel_SOC_Shadow").gameObject.transform.Find("TMP_SOC_Shadow").gameObject.GetComponent<TextMeshProUGUI>();

    }
    // 초기 Button 이벤트 설정.
    void InitialSet_Button()
    {
        m_bDisplaySOC = true;

        m_BTN_SOC_UD.onClick.RemoveAllListeners();
        m_BTN_SOC_UD.onClick.AddListener(delegate { Btn_Press_UD(); });
    }

    // Button 에 이벤트 추가.
    void Btn_Press_UD()
    {
        if (m_bDisplaySOC == false)
        {
            m_bDisplaySOC = true;
            m_TMP_BTN_SOC_UD.text = "+";

            m_gPanel_SOC_Total.SetActive(false);
            m_gPanel_SOC.transform.SetAsLastSibling();
        }
        else
        {
            m_bDisplaySOC = false;
            m_TMP_BTN_SOC_UD.text = "-";

            m_gPanel_SOC_Total.SetActive(true);
            m_gPanel_SOC.transform.SetAsLastSibling();
        }
    }

    // 평판 상태창 갱신.
    public void UpdateSoc()
    {
        m_TMP_Soc_Honor.text = "명        예: " + Player_Total.Instance.m_ps_Status.m_sSoc.GetSOC_Honor();
        m_TMP_Soc_Human.text = "인        간: " + Player_Total.Instance.m_ps_Status.m_sSoc.GetSOC_Human();
        m_TMP_Soc_Animal.text = "동        물: " + Player_Total.Instance.m_ps_Status.m_sSoc.GetSOC_Animal();
        m_TMP_Soc_Slime.text = "슬  라  임: " + Player_Total.Instance.m_ps_Status.m_sSoc.GetSOC_Slime();
        m_TMP_Soc_Skeleton.text = "스켈레톤: " + Player_Total.Instance.m_ps_Status.m_sSoc.GetSOC_Skeleton();
        m_TMP_Soc_Ents.text = "앤        트: " + Player_Total.Instance.m_ps_Status.m_sSoc.GetSOC_Ents();
        m_TMP_Soc_Devil.text = "마        족: " + Player_Total.Instance.m_ps_Status.m_sSoc.GetSOC_Devil();
        m_TMP_Soc_Dragon.text = "용        족: " + Player_Total.Instance.m_ps_Status.m_sSoc.GetSOC_Dragon();
        m_TMP_Soc_Shadow.text = "어        둠: " + Player_Total.Instance.m_ps_Status.m_sSoc.GetSOC_Shadow();
    }
}
