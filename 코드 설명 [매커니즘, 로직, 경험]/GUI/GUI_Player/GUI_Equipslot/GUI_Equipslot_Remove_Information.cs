using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


public class GUI_Equipslot_Remove_Information : MonoBehaviour
{
    // Equipslot_Remove_Information 에서 재확인 하는 장비 아이템 착용 해제 관련 UI.
    [SerializeField] GameObject m_gPanel_Equipslot_Remove_Information;
    [Space(20)]

    [SerializeField] GameObject m_gPanel_Equipslot_Remove_Information_Content;
    [SerializeField] Button m_BTN_Equipslot_Remove_Information_Content_Ok;

    public void InitialSet()
    {
        InitialSet_Object();
        InitialSet_Button();
    }

    // 초기 Object 불러오기.
    void InitialSet_Object()
    {
        m_gPanel_Equipslot_Remove_Information = GameObject.Find("Canvas_GUI").transform.Find("Panel_Equipslot_Remove_Information").gameObject;

        m_gPanel_Equipslot_Remove_Information_Content = m_gPanel_Equipslot_Remove_Information.transform.Find("Panel_Equipslot_Remove_Information_Content").gameObject;
        m_BTN_Equipslot_Remove_Information_Content_Ok = m_gPanel_Equipslot_Remove_Information_Content.transform.Find("BTN_Equipslot_Remove_Information_Content_Ok").gameObject.GetComponent<Button>();
    }
    // 초기 Button 이벤트 설정.
    void InitialSet_Button()
    {
        m_BTN_Equipslot_Remove_Information_Content_Ok.onClick.RemoveAllListeners();
        m_BTN_Equipslot_Remove_Information_Content_Ok.onClick.AddListener(delegate { Btn_Press_Ok(); });
    }

    // 버튼 이벤트 처리.
    void Btn_Press_Ok()
    {
        m_gPanel_Equipslot_Remove_Information.SetActive(false);
    }
}
