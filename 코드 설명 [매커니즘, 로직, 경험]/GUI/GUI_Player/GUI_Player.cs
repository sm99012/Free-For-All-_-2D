using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//
// ※ 플레이어 스탯(능력치(체력, 마나, 경험치)) GUI
//    해당 GUI는 항상 활성화된 상태로 실시간으로 변화하는 플레이어의 스탯(능력치(체력, 마나, 경험치))을 확인할 수 있다.
//

public class GUI_Player : MonoBehaviour
{
    // GUI 오브젝트
    [SerializeField] GameObject m_gPanel_PlayerStatus;
    
    [SerializeField] GameObject m_gPanel_PlayerStatus_HPBar; 
    TextMeshProUGUI m_TMP_HP;                                // (텍스트) 현재체력 / 최대체력
    Image m_IMG_HP;                                          // (이미지) 체력 비율(현재체력 / 최대체력)
    
    [SerializeField] GameObject m_gPanel_PlayerStatus_MPBar;
    TextMeshProUGUI m_TMP_MP;                                // (텍스트) 현재마나 / 최대마나
    Image m_IMG_MP;                                          // (이미지) 마나 비율(현재마나 / 최대마나)

    [SerializeField] GameObject m_gPanel_DownBar;
    TextMeshProUGUI m_TMP_LV;                     // (텍스트) 레벨
    TextMeshProUGUI m_TMP_EXP;                    // (텍스트) 현재경험치 / 최대경험치
    Image m_IMG_EXP;                              // (이미지) 경험치 비율(현재경험치 / 최대경험치)

    // GUI 초기 설정
    public void InitialSet()
    {
        InitialSet_Object(); // GUI 오브젝트 초기 설정
    }
    // GUI 오브젝트 초기 설정
    void InitialSet_Object()
    {
        m_gPanel_PlayerStatus = GameObject.Find("Panel_PlayerStatus");

        m_gPanel_PlayerStatus_HPBar = m_gPanel_PlayerStatus.transform.Find("Panel_PlayerStatus_HPBar").gameObject;
        m_TMP_HP = m_gPanel_PlayerStatus_HPBar.transform.Find("TMP_PlayerStatus_HPBar").gameObject.GetComponent<TextMeshProUGUI>();
        m_IMG_HP = m_gPanel_PlayerStatus_HPBar.transform.Find("IMG_PlayerStatus_HPBar_Current").gameObject.GetComponent<Image>();
        
        m_gPanel_PlayerStatus_MPBar = m_gPanel_PlayerStatus.transform.Find("Panel_PlayerStatus_MPBar").gameObject;
        m_TMP_MP = m_gPanel_PlayerStatus_MPBar.transform.Find("TMP_PlayerStatus_MPBar").gameObject.GetComponent<TextMeshProUGUI>();
        m_IMG_MP = m_gPanel_PlayerStatus_MPBar.transform.Find("IMG_PlayerStatus_MPBar_Current").gameObject.GetComponent<Image>();
        
        m_gPanel_DownBar = GameObject.Find("Panel_DownBar");
        m_TMP_LV = m_gPanel_DownBar.transform.Find("TMP_DownBar_LVBar").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_EXP = m_gPanel_DownBar.transform.Find("TMP_DownBar_EXPBar").gameObject.GetComponent<TextMeshProUGUI>();
        m_IMG_EXP = m_gPanel_DownBar.transform.Find("IMG_DownBar_EXPBar_Current").gameObject.GetComponent<Image>();
    }

    private void Update()
    {
        if (Total_Manager.Instance.m_bStart == true)
            if (Player_Total.Instance != null)
                UpdatePlayerStatusUI(); // 플레이어 스탯(능력치(체력, 마나, 경험치)) GUI 업데이트
    }

    // 플레이어 스탯(능력치(체력, 마나, 경험치)) GUI 업데이트
    public void UpdatePlayerStatusUI()
    {
        UpdateText();  // 플레이어 스탯(능력치(체력, 마나, 경험치)) 텍스트 업데이트
        UpdateImage(); // 플레이어 스탯(능력치(체력, 마나, 경험치)) 이미지 업데이트
    }
    // 플레이어 스탯(능력치(체력, 마나, 경험치)) 텍스트 업데이트
    void UpdateText()
    {
        m_TMP_LV.text = "LV: " + Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_LV().ToString();
        m_TMP_HP.text = Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_HP_Current().ToString() + " / " + Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_HP_Max().ToString();
        m_TMP_MP.text = Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_MP_Current().ToString() + " / " + Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_MP_Max().ToString();
        m_TMP_EXP.text = "EXP: " + ((float)Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_EXP_Current() / (float)Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_EXP_Max()) * 100 + "% (" + 
            Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_EXP_Current().ToString() + " / " + Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_EXP_Max().ToString() + ")";
    }
    // 플레이어 스탯(능력치(체력, 마나, 경험치)) 이미지 업데이트
    // 선형보간 함수(Mathf.Lerp())를 사용해 플레이어 스탯(능력치(체력, 마나, 경험치)) 이미지의 부드러운 변화를 구현했다.
    void UpdateImage()
    {
        if (Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_HP_Max() != 0)
            m_IMG_HP.fillAmount = Mathf.Lerp(m_IMG_HP.fillAmount, (float)Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_HP_Current() / (float)Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_HP_Max(), Time.deltaTime);
        else
            m_IMG_HP.fillAmount = 0;
        if (Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_MP_Max() != 0)
            m_IMG_MP.fillAmount = Mathf.Lerp(m_IMG_MP.fillAmount, (float)Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_MP_Current() / (float)Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_MP_Max(), Time.deltaTime);
        else
            m_IMG_MP.fillAmount = 0;
        if (Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_EXP_Max() != 0)
            m_IMG_EXP.fillAmount = Mathf.Lerp(m_IMG_EXP.fillAmount, (float)Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_EXP_Current() / (float)Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_EXP_Max(), Time.deltaTime);
        else
            m_IMG_EXP.fillAmount = 0;
    }
}
