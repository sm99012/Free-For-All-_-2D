using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GUI_Status : MonoBehaviour
{
    // Player 세부 능력치 UI.
    [SerializeField] public GameObject m_gPanel_DetailStatus;
    [SerializeField] GameObject m_gPanel_DetailStatus_Exit;
    [SerializeField] GameObject m_gPanel_DetailStatus_Content;
    [Space(20)]
    [SerializeField] Button m_BTN_DetailStatus_Exit;
    [Space(20)]
    [SerializeField] TextMeshProUGUI m_TMP_DetailStatus_Content_LV;
    [SerializeField] TextMeshProUGUI m_TMP_DetailStatus_Content_EXP;
    [SerializeField] TextMeshProUGUI m_TMP_DetailStatus_Content_HP;
    [SerializeField] TextMeshProUGUI m_TMP_DetailStatus_Content_MP;
    [SerializeField] TextMeshProUGUI m_TMP_DetailStatus_Content_TotalDamage;
    [SerializeField] TextMeshProUGUI m_TMP_DetailStatus_Content_PhysicalDeffence;
    [SerializeField] TextMeshProUGUI m_TMP_DetailStatus_Content_Speed;
    [SerializeField] TextMeshProUGUI m_TMP_DetailStatus_Content_AttackSpeed;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_DetailStatus_SetItemEffect;
    [SerializeField] GameObject m_gSV_DetailStatus_SetItemEffect_Content;
    [SerializeField] GameObject m_gViewport_DetailStatus_SetItemEffect_Content;
    [SerializeField] GameObject m_gTMP_DetailStatus_SetItemEffect_Content;
    [SerializeField] TextMeshProUGUI m_TMP_DetailStatus_SetItemEffect_Content;

    public void InitialSet()
    {
        InitialSet_Object();
        InitialSet_Button();
        m_gPanel_DetailStatus.SetActive(false);
    }

    // 초기 Object 불러오기.
    void InitialSet_Object()
    {
        m_gPanel_DetailStatus = GameObject.Find("Canvas_GUI").gameObject.transform.Find("Panel_ES").gameObject.transform.Find("Panel_DetailStatus").gameObject;

        m_gPanel_DetailStatus_Exit = m_gPanel_DetailStatus.transform.Find("Panel_DetailStatus_Exit").gameObject;
        //m_BTN_DetailStatus_Exit = m_gPanel_DetailStatus_Exit.transform.Find("BTN_DetailStatus_Exit").gameObject.GetComponent<Button>();

        m_gPanel_DetailStatus_Content = m_gPanel_DetailStatus.transform.Find("Panel_DetailStatus_Content").gameObject;
        m_TMP_DetailStatus_Content_LV = m_gPanel_DetailStatus_Content.transform.Find("Panel_LV").gameObject.transform.Find("TMP_LV").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_DetailStatus_Content_EXP = m_gPanel_DetailStatus_Content.transform.Find("Panel_EXP").gameObject.transform.Find("TMP_EXP").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_DetailStatus_Content_HP = m_gPanel_DetailStatus_Content.transform.Find("Panel_HP").gameObject.transform.Find("TMP_HP").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_DetailStatus_Content_MP = m_gPanel_DetailStatus_Content.transform.Find("Panel_MP").gameObject.transform.Find("TMP_MP").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_DetailStatus_Content_TotalDamage = m_gPanel_DetailStatus_Content.transform.Find("Panel_TotalDamage").gameObject.transform.Find("TMP_TotalDamage").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_DetailStatus_Content_PhysicalDeffence = m_gPanel_DetailStatus_Content.transform.Find("Panel_PhysicalDeffence").gameObject.transform.Find("TMP_PhysicalDeffence").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_DetailStatus_Content_Speed = m_gPanel_DetailStatus_Content.transform.Find("Panel_Speed").gameObject.transform.Find("TMP_Speed").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_DetailStatus_Content_AttackSpeed = m_gPanel_DetailStatus_Content.transform.Find("Panel_AttackSpeed").gameObject.transform.Find("TMP_AttackSpeed").gameObject.GetComponent<TextMeshProUGUI>();

        m_gPanel_DetailStatus_SetItemEffect = m_gPanel_DetailStatus.transform.Find("Panel_DetailStatus_SetItemEffect").gameObject;
        m_gSV_DetailStatus_SetItemEffect_Content = m_gPanel_DetailStatus_SetItemEffect.transform.Find("SV_DetailStatus_SetItemEffect_Content").gameObject;
        m_gViewport_DetailStatus_SetItemEffect_Content = m_gSV_DetailStatus_SetItemEffect_Content.transform.Find("Viewport_DetailStatus_SetItemEffect_Content").gameObject;
        m_gTMP_DetailStatus_SetItemEffect_Content = m_gViewport_DetailStatus_SetItemEffect_Content.transform.Find("TMP_DetailStatus_SetItemEffect_Content").gameObject;
        m_TMP_DetailStatus_SetItemEffect_Content = m_gTMP_DetailStatus_SetItemEffect_Content.GetComponent<TextMeshProUGUI>();
    }
    // 초기 Button 이벤트 설정.
    void InitialSet_Button()
    {
        //m_BTN_DetailStatus_Exit.onClick.RemoveAllListeners();
        //m_BTN_DetailStatus_Exit.onClick.AddListener(delegate { Btn_Press_Exit(); });
    }

    // Button 에 이벤트 추가.
    void Btn_Press_Exit()
    {
        m_gPanel_DetailStatus.SetActive(false);
    }

    public void UpdateStatus()
    {
        m_TMP_DetailStatus_Content_LV.text = "레        벨: " + Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_LV();
        //m_TMP_DetailStatus_Content_EXP.text = "경  험  치: " + Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_EXP_Current() + " / " + Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_EXP_Max();
        m_TMP_DetailStatus_Content_HP.text = "체        력: " + Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_HP_Current() + " / " + Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_HP_Max();
        m_TMP_DetailStatus_Content_MP.text = "마        나: " + Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_MP_Current() + " / " + Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_MP_Max();
        m_TMP_DetailStatus_Content_TotalDamage.text = "데  미  지: " + Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_Damage_Total();
        m_TMP_DetailStatus_Content_PhysicalDeffence.text = "방  어  력: " + Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_Defence_Physical();
        m_TMP_DetailStatus_Content_Speed.text = "이동속도: " + Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_Speed();
        m_TMP_DetailStatus_Content_AttackSpeed.text = "공격속도: " + (float)Math.Round(Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_AttackSpeed(), 2);
    }
    // 아이템 세트 효과 표시.
    public void UpdateStatus_SetItemEffect(Dictionary<int, int> setitemdictionary)
    {
        m_TMP_DetailStatus_SetItemEffect_Content.text = "";

        foreach (KeyValuePair<int, int> dictionary in setitemdictionary)
        {
            if (dictionary.Key != 0)
            {
                for (int i = 1, j = 1; i < dictionary.Value + 1; i++)
                {
                    if (ItemSetEffectManager.instance.Check_SetItemEffect(dictionary.Key, i) == true)
                    {
                        m_TMP_DetailStatus_SetItemEffect_Content.text += ItemSetEffectManager.instance.Return_SetItemEffect_Name(dictionary.Key, j) + "\n";
                        j++;
                    }
                }
            }
        }
    }

    public void Display_GUI_Status()
    {
        if (m_gPanel_DetailStatus.activeSelf == true)
            m_gPanel_DetailStatus.SetActive(false);
        else
        {
            UpdateStatus_SetItemEffect(Player_Total.Instance.CheckSetItemEffect_UI());
            m_gPanel_DetailStatus.SetActive(true);
            m_gPanel_DetailStatus.transform.SetAsLastSibling();
        }
    }
}
