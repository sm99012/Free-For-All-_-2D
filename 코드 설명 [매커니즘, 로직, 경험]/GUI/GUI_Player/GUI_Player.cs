using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUI_Player : MonoBehaviour
{
    [SerializeField] GameObject m_gPanel_PlayerStatus;
    [SerializeField] GameObject m_gPanel_PlayerStatus_HPBar;
    [SerializeField] GameObject m_gPanel_PlayerStatus_MPBar;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_DownBar;

    [Space(20)]
    TextMeshProUGUI m_TMP_LV;
    TextMeshProUGUI m_TMP_HP;
    TextMeshProUGUI m_TMP_MP;
    TextMeshProUGUI m_TMP_EXP;

    Image m_IMG_HP;
    Image m_IMG_MP;
    Image m_IMG_EXP;

    public void InitialSet()
    {
        InitialSet_Object();
    }

    private void Update()
    {
        if (Total_Manager.Instance.m_bStart == true)
            if (Player_Total.Instance != null)
                UpdatePlayerStatusUI();
    }


    // 초기 Object 불러오기.
    void InitialSet_Object()
    {
        m_gPanel_PlayerStatus = GameObject.Find("Panel_PlayerStatus");

        m_gPanel_PlayerStatus_HPBar = m_gPanel_PlayerStatus.transform.Find("Panel_PlayerStatus_HPBar").gameObject;
        m_IMG_HP = m_gPanel_PlayerStatus_HPBar.transform.Find("IMG_PlayerStatus_HPBar_Current").gameObject.GetComponent<Image>();
        m_TMP_HP = m_gPanel_PlayerStatus_HPBar.transform.Find("TMP_PlayerStatus_HPBar").gameObject.GetComponent<TextMeshProUGUI>();
        m_gPanel_PlayerStatus_MPBar = m_gPanel_PlayerStatus.transform.Find("Panel_PlayerStatus_MPBar").gameObject;
        m_IMG_MP = m_gPanel_PlayerStatus_MPBar.transform.Find("IMG_PlayerStatus_MPBar_Current").gameObject.GetComponent<Image>();
        m_TMP_MP = m_gPanel_PlayerStatus_MPBar.transform.Find("TMP_PlayerStatus_MPBar").gameObject.GetComponent<TextMeshProUGUI>();


        m_gPanel_DownBar = GameObject.Find("Panel_DownBar");

        m_IMG_EXP = m_gPanel_DownBar.transform.Find("IMG_DownBar_EXPBar_Current").gameObject.GetComponent<Image>();
        m_TMP_EXP = m_gPanel_DownBar.transform.Find("TMP_DownBar_EXPBar").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_LV = m_gPanel_DownBar.transform.Find("TMP_DownBar_LVBar").gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void UpdatePlayerStatusUI()
    {
        UpdateText();
        UpdateImage();
    }
    void UpdateText()
    {
        m_TMP_LV.text = "LV: " + Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_LV().ToString();
        m_TMP_HP.text = Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_HP_Current().ToString() + " / " + Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_HP_Max().ToString();
        m_TMP_MP.text = Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_MP_Current().ToString() + " / " + Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_MP_Max().ToString();
        m_TMP_EXP.text = "EXP: " + ((float)Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_EXP_Current()/ (float)Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_EXP_Max()) * 100 + "% (" + 
            Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_EXP_Current().ToString() + " / " + Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_EXP_Max().ToString() + ")";
    }
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
