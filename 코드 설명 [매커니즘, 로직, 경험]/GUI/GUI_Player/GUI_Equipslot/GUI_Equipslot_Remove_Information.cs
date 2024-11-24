using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

//
// ※ 장비아이템 해제 알림 GUI
//    해당 GUI는 플레이어의 스탯(능력치, 평판) 변동으로, 현재 착용중인 장비아이템의 착용 조건 불충족 시 활성화된다.
//

public class GUI_Equipslot_Remove_Information : MonoBehaviour
{
    // GUI 오브젝트
    [SerializeField] GameObject m_gPanel_Equipslot_Remove_Information;
    [SerializeField] GameObject m_gPanel_Equipslot_Remove_Information_Content;
    [SerializeField] Button m_BTN_Equipslot_Remove_Information_Content_Ok; // (버튼) GUI 비활성화

    // GUI 초기 설정
    public void InitialSet()
    {
        InitialSet_Object(); // GUI 오브젝트 초기 설정
        InitialSet_Button(); // GUI 버튼 설정
    }
    // GUI 오브젝트 초기 설정
    void InitialSet_Object()
    {
        m_gPanel_Equipslot_Remove_Information = GameObject.Find("Canvas_GUI").transform.Find("Panel_Equipslot_Remove_Information").gameObject;

        m_gPanel_Equipslot_Remove_Information_Content = m_gPanel_Equipslot_Remove_Information.transform.Find("Panel_Equipslot_Remove_Information_Content").gameObject;
        m_BTN_Equipslot_Remove_Information_Content_Ok = m_gPanel_Equipslot_Remove_Information_Content.transform.Find("BTN_Equipslot_Remove_Information_Content_Ok").gameObject.GetComponent<Button>();
    }
    // GUI 버튼 설정
    void InitialSet_Button()
    {
        // (버튼) GUI 비활성화 클릭 이벤트 함수 설정
        m_BTN_Equipslot_Remove_Information_Content_Ok.onClick.RemoveAllListeners();
        m_BTN_Equipslot_Remove_Information_Content_Ok.onClick.AddListener(delegate { Btn_Press_Ok(); });
    }

    // (버튼) GUI 비활성화 클릭 이벤트 함수 - 장비아이템 착용 해제 알림 GUI를 비활성화 한다.
    void Btn_Press_Ok()
    {
        m_gPanel_Equipslot_Remove_Information.SetActive(false);
    }

    // 장비아이템 해제 알림 GUI 활성화
    public void Display_GUI_Equipslot_Remove_Information()
    {
        m_gPanel_Equipslot_Remove_Information.transform.SetAsLastSibling();
        m_gPanel_Equipslot_Remove_Information.SetActive(true);
    }
}
