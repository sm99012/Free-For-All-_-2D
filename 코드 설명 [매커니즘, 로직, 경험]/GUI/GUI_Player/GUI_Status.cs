using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

//
// ※ 플레이어 스탯(능력치) GUI
//    해당 GUI를 활성화 하여 플레이어의 모든 스탯(능력치)을 확인할 수 있다. 해당 GUI는 상태창(능력치창 + 장비창 + 아이템 세트효과창)GUI로 통합 되었다.
//

public class GUI_Status : MonoBehaviour
{
    // GUI 오브젝트
    [SerializeField] public GameObject m_gPanel_DetailStatus;
    
    [SerializeField] GameObject m_gPanel_DetailStatus_Exit;
    [SerializeField] Button m_BTN_DetailStatus_Exit; // (버튼) GUI 비활성화. 현재 GUI 통합으로 사용하지 않는다.
    
    [SerializeField] GameObject m_gPanel_DetailStatus_Content;
    
    [SerializeField] TextMeshProUGUI m_TMP_DetailStatus_Content_LV;               // (텍스트) 레벨
    [SerializeField] TextMeshProUGUI m_TMP_DetailStatus_Content_EXP;              // (텍스트) 현재경험치 / 최대경험치
    [SerializeField] TextMeshProUGUI m_TMP_DetailStatus_Content_HP;               // (텍스트) 현재체력 / 최대체력
    [SerializeField] TextMeshProUGUI m_TMP_DetailStatus_Content_MP;               // (텍스트) 현재마나 / 최대마나
    [SerializeField] TextMeshProUGUI m_TMP_DetailStatus_Content_TotalDamage;      // (텍스트) 데미지(총데미지)
    [SerializeField] TextMeshProUGUI m_TMP_DetailStatus_Content_PhysicalDeffence; // (텍스트) 방어력(물리방어력)
    [SerializeField] TextMeshProUGUI m_TMP_DetailStatus_Content_Speed;            // (텍스트) 이동속도
    [SerializeField] TextMeshProUGUI m_TMP_DetailStatus_Content_AttackSpeed;      // (텍스트) 공격속도

    [SerializeField] GameObject m_gPanel_DetailStatus_SetItemEffect;
    [SerializeField] GameObject m_gSV_DetailStatus_SetItemEffect_Content;
    [SerializeField] GameObject m_gViewport_DetailStatus_SetItemEffect_Content;
    [SerializeField] GameObject m_gTMP_DetailStatus_SetItemEffect_Content;
    [SerializeField] TextMeshProUGUI m_TMP_DetailStatus_SetItemEffect_Content;  // (텍스트) 현재 적용중인 아이템 세트효과

    // GUI 초기 설정
    public void InitialSet()
    {
        InitialSet_Object(); // GUI 오브젝트 초기 설정
        InitialSet_Button(); // GUI 버튼 설정
        
        m_gPanel_DetailStatus.SetActive(false);
    }
    // GUI 오브젝트 초기 설정
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
        (버튼) GUI 비활성화 클릭 클릭 이벤트 함수 설정
        //m_BTN_DetailStatus_Exit.onClick.RemoveAllListeners();
        //m_BTN_DetailStatus_Exit.onClick.AddListener(delegate { Btn_Press_Exit(); });
    }

    // (버튼) GUI 비활성화 클릭 이벤트 함수 - 플레이어 스탯(능력치) GUI를 비활성화 한다. 현재 GUI 통합으로 사용하지 않는다.
    void Btn_Press_Exit()
    {
        m_gPanel_DetailStatus.SetActive(false);
    }

    // 플레이어 스탯(능력치) GUI 업데이트
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
    // 현재 적용중인 아이템 세트효과 업데이트
    public void UpdateStatus_SetItemEffect(Dictionary<int, int> setitemdictionary) // setitemdictionary : 현재 적용중인 아이템 세트효과 데이터. Dictionary <Key : 아이템 세트효과 고유코드, Value : 아이템 세트효과 고유코드를 보유한 착용중인 장비아이템 개수>
    {
        m_TMP_DetailStatus_SetItemEffect_Content.text = "";

        foreach (KeyValuePair<int, int> dictionary in setitemdictionary)
        {
            if (dictionary.Key != 0)
            {
                for (int i = 1, j = 1; i < dictionary.Value + 1; i++)
                {
                    if (ItemSetEffectManager.instance.Check_SetItemEffect(dictionary.Key, i) == true) // 아이템 세트효과의 단계 별 추가 스탯(능력치, 평판) 존재 유무를 판단하는 함수
                    {
                        m_TMP_DetailStatus_SetItemEffect_Content.text += ItemSetEffectManager.instance.Return_SetItemEffect_Name(dictionary.Key, j) + "\n";
                        j++;
                    }
                }
            }
        }
    }

    // 플레이어 스탯(능력치) GUI 활성화 / 비활성화 함수
    public void Display_GUI_Status()
    {
        if (m_gPanel_DetailStatus.activeSelf == true)
            m_gPanel_DetailStatus.SetActive(false); // GUI 오브젝트 비활성화
        else
        {
            UpdateStatus_SetItemEffect(Player_Total.Instance.CheckSetItemEffect_UI()); // 현재 적용중인 아이템 세트효과 업데이트
            m_gPanel_DetailStatus.SetActive(true); // GUI 오브젝트 활성화
            m_gPanel_DetailStatus.transform.SetAsLastSibling();
        }
    }
}
