using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//
// ※ 플레이어 스탯(평판) GUI
//    실시간으로 변화하는 플레이어의 스탯(평판)을 확인할 수 있다. GUI 펼치기/접기 기능을 구현해 유저에 편의를 제공했다.
//

public class GUI_Soc : MonoBehaviour
{
    // GUI 오브젝트
    [SerializeField] GameObject m_gPanel_SOC;
    
    [SerializeField] GameObject m_gPanel_SOC_Bar;
    [SerializeField] Button m_BTN_SOC_UD;    // (버튼) GUI 펼치기/접기
    public TextMeshProUGUI m_TMP_BTN_SOC_UD; // (텍스트) GUI 펼치기/접기 ("+" / "-")
    bool m_bDisplaySOC;                      // GUI 펼치기/접기 상태 (m_bDisplaySOC == true : GUI 펼치기 상태 / m_bDisplaySOC == false : GUI 접기 상태)
    
    [SerializeField] GameObject m_gPanel_SOC_Total;
    public TextMeshProUGUI m_TMP_Soc_Honor;    // (텍스트) 명예
    public TextMeshProUGUI m_TMP_Soc_Human;    // (텍스트) 인간 종족 평판
    public TextMeshProUGUI m_TMP_Soc_Animal;   // (텍스트) 동물 종족 평판
    public TextMeshProUGUI m_TMP_Soc_Slime;    // (텍스트) 슬라임 종족 평판
    public TextMeshProUGUI m_TMP_Soc_Skeleton; // (텍스트) 스켈레톤 종족 평판
    public TextMeshProUGUI m_TMP_Soc_Ents;     // (텍스트) 앤트 종족 평판
    public TextMeshProUGUI m_TMP_Soc_Devil;    // (텍스트) 마족 평판
    public TextMeshProUGUI m_TMP_Soc_Dragon;   // (텍스트) 용족 평판
    public TextMeshProUGUI m_TMP_Soc_Shadow;   // (텍스트) 어둠 평판

    // GUI 초기 설정
    public void InitialSet()
    {
        InitialSet_Object(); // GUI 오브젝트 초기 설정
        InitialSet_Button(); // GUI 버튼 설정

        m_bDisplaySOC = false;

        m_TMP_BTN_SOC_UD.text = "-";

        m_gPanel_SOC_Total.SetActive(true);
        m_gPanel_SOC.transform.SetAsLastSibling();
    }
    // GUI 오브젝트 초기 설정
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
    // GUI 버튼 설정
    void InitialSet_Button()
    {
        m_bDisplaySOC = true;
        
        // (버튼) GUI 펼치기/접기 클릭 이벤트 함수 설정
        m_BTN_SOC_UD.onClick.RemoveAllListeners();
        m_BTN_SOC_UD.onClick.AddListener(delegate { Btn_Press_UD(); });
    }

    // (버튼) GUI 펼치기/접기 클릭 이벤트 함수
    void Btn_Press_UD()
    {
        if (m_bDisplaySOC == false)
        {
            m_bDisplaySOC = true;
            m_TMP_BTN_SOC_UD.text = "+"; // (텍스트) GUI 펼치기/접기 = "+"

            m_gPanel_SOC_Total.SetActive(false); // 관련 GUI 비활성화
            m_gPanel_SOC.transform.SetAsLastSibling();
        }
        else
        {
            m_bDisplaySOC = false;
            m_TMP_BTN_SOC_UD.text = "-"; // (텍스트) GUI 펼치기/접기 = "-"

            m_gPanel_SOC_Total.SetActive(true); // 관련 GUI 활성화
            m_gPanel_SOC.transform.SetAsLastSibling();
        }
    }

    // 플레이어 스탯(평판) GUI 업데이트
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
