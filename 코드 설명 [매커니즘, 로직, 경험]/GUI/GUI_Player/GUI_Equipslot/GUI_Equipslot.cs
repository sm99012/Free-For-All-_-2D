using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//
// ※ 장비창 GUI
//    해당 GUI를 활성화 하여 플레이어가 현재 착용중인 장비아이템을 확인할 수 있다. 해당 GUI는 상태창(능력치창 + 장비창 + 아이템 세트효과창)GUI로 통합 되었다.
//

public class GUI_Equipslot : MonoBehaviour
{
    // GUI 오브젝트 - 상태창 GUI
    [SerializeField] GameObject m_gPanel_ES;

    // GUI 오브젝트 - 장비창 GUI
    [SerializeField] GameObject m_gPanel_Equipslot;

    [SerializeField] GameObject m_gPanel_Equipslot_Exit;
    [SerializeField] Button m_BTN_Equipslot_Exit;

    [SerializeField] GameObject m_gPanel_Equipslot_Content;
    [SerializeField] GameObject m_gPanel_Equipslot_Content_Hat;
    [SerializeField] GameObject m_gPanel_Equipslot_Content_Top;
    [SerializeField] GameObject m_gPanel_Equipslot_Content_Bottoms;
    [SerializeField] GameObject m_gPanel_Equipslot_Content_Shose;
    [SerializeField] GameObject m_gPanel_Equipslot_Content_Gloves;
    [SerializeField] GameObject m_gPanel_Equipslot_Content_MainWeapon;
    [SerializeField] GameObject m_gPanel_Equipslot_Content_SubWeapon;

    public GameObject[] m_gary_Equipslot; // 장비창 슬롯 GUI 배열. 각각의 장비창 슬롯 GUI를 배열로 저장한다.
                                          // 0: 모자
                                          // 1: 상의
                                          // 2: 하의
                                          // 3: 신발
                                          // 4: 장갑
                                          // 5: 주무기
                                          // 6: 보조무기

    // GUI 초기 설정
    public void InitialSet()
    {
        InitialSet_Object();    // GUI 오브젝트 초기 설정
        InitialSet_Button();    // GUI 버튼 설정
        InitialSet_Equipslot(); // 장비창 슬롯 GUI 초기 설정
        
        m_gPanel_ES.SetActive(false);
        m_gPanel_Equipslot.SetActive(false);
    }
    // GUI 오브젝트 초기 설정
    void InitialSet_Object()
    {
        m_gPanel_ES = GameObject.Find("Canvas_GUI").gameObject.transform.Find("Panel_ES").gameObject;

        m_gPanel_Equipslot = GameObject.Find("Canvas_GUI").gameObject.transform.Find("Panel_ES").gameObject.transform.Find("Panel_Equipslot").gameObject;

        m_gPanel_Equipslot_Exit = m_gPanel_Equipslot.transform.Find("Panel_Equipslot_Exit").gameObject;
        m_BTN_Equipslot_Exit = m_gPanel_Equipslot_Exit.transform.Find("BTN_Equipslot_Exit").gameObject.GetComponent<Button>();

        m_gPanel_Equipslot_Content = m_gPanel_Equipslot.transform.Find("Panel_Equipslot_Content").gameObject;
        m_gPanel_Equipslot_Content_Hat = m_gPanel_Equipslot_Content.transform.Find("Panel_Equipslot_Content_Hat").gameObject;
        m_gPanel_Equipslot_Content_Top = m_gPanel_Equipslot_Content.transform.Find("Panel_Equipslot_Content_Top").gameObject;
        m_gPanel_Equipslot_Content_Bottoms = m_gPanel_Equipslot_Content.transform.Find("Panel_Equipslot_Content_Bottoms").gameObject;
        m_gPanel_Equipslot_Content_Shose = m_gPanel_Equipslot_Content.transform.Find("Panel_Equipslot_Content_Shose").gameObject;
        m_gPanel_Equipslot_Content_Gloves = m_gPanel_Equipslot_Content.transform.Find("Panel_Equipslot_Content_Gloves").gameObject;
        m_gPanel_Equipslot_Content_MainWeapon = m_gPanel_Equipslot_Content.transform.Find("Panel_Equipslot_Content_MainWeapon").gameObject;
        m_gPanel_Equipslot_Content_SubWeapon = m_gPanel_Equipslot_Content.transform.Find("Panel_Equipslot_Content_SubWeapon").gameObject;
    }
    // GUI 버튼 설정
    void InitialSet_Button()
    {
        // (버튼) 장비창 GUI 비활성화 클릭 이벤트 함수 설정
        m_BTN_Equipslot_Exit.onClick.RemoveAllListeners();
        m_BTN_Equipslot_Exit.onClick.AddListener(delegate { Btn_Press_Exit(); });
    }
    // 장비창 슬롯 GUI 초기 설정
    public void InitialSet_Equipslot()
    {
        m_gary_Equipslot = new GameObject[7];
        for (int i = 0; i < 7; i++)
        {
            m_gary_Equipslot[i] = m_gPanel_Equipslot_Content.transform.GetChild(i).gameObject;
            m_gary_Equipslot[i].GetComponent<Equipslot>().m_nAryNumber = i;
        }
    }

    // (버튼) 장비창 GUI 비활성화 클릭 이벤트 함수 - 장비창 GUI를 비활성화한다.
    public void Btn_Press_Exit()
    {
        m_BTN_Equipslot_Exit.GetComponent<Image>().color = new Color(.75f, .75f, .75f, 1);
        GUIManager_Total.Instance.Display_GUI_ES(); // 상태창 GUI 비활성화
        GUIManager_Total.Instance.UnDisplay_GUI_Equipslot_Equip_Information(); // 장비아이템 세부 정보 GUI 비활성화
        GUIManager_Total.Instance.Delete_GUI_Priority(16); // GUI 우선순위 제거
    }

    // 장비창 GUI 업데이트
    public void UpdateEquipslot()
    {
        // 장비창 GUI 업데이트 - 장비아이템(모자)
        if (Player_Equipment.m_bEquipment_Hat == false) // 착용중인 장비아이템(모자)이 존재하지 않는 경우
        {
            m_gary_Equipslot[0].GetComponent<Equipslot>().SetNull(); // 장비아이템(모자)에 해당하는 장비창 슬롯 GUI 초기화
        }
        else
        {
            m_gary_Equipslot[0].GetComponent<Equipslot>().SetItem_Equip(Player_Equipment.m_gEquipment_Hat); // 장비아이템(모자)에 해당하는 장비창 슬롯 GUI 장비아이템 외형 표시
            if (Player_Total.Instance.CheckCondition_Item_Equip_Hat() == true) { } // 착용중인 장비아이템(모자) 착용 조건 판단
            else
            {
                m_gary_Equipslot[0].GetComponent<Equipslot>().SetItem_Equip_NotApply(); // 착용 조건을 불충족한 장비아이템 마스크 적용(활성화)
                GUIManager_Total.Instance.Display_GUI_Equipslot_Remove_Information(); // 장비아이템 해제 알림 GUI 활성화
            }
        }
        // 장비창 GUI 업데이트 - 장비아이템(상의)
        if (Player_Equipment.m_bEquipment_Top == false) // 착용중인 장비아이템(상의)이 존재하지 않는 경우
        {
            m_gary_Equipslot[1].GetComponent<Equipslot>().SetNull(); // 장비아이템(상의)에 해당하는 장비창 슬롯 GUI 초기화
        }
        else
        {
            m_gary_Equipslot[1].GetComponent<Equipslot>().SetItem_Equip(Player_Equipment.m_gEquipment_Top); // 장비아이템(상의)에 해당하는 장비창 슬롯 GUI 장비아이템 외형 표시
            if (Player_Total.Instance.CheckCondition_Item_Equip_Top() == true) { } // 착용중인 장비아이템(상의) 착용 조건 판단
            else
            {
                m_gary_Equipslot[1].GetComponent<Equipslot>().SetItem_Equip_NotApply(); // 착용 조건을 불충족한 장비아이템 마스크 적용(활성화)
                GUIManager_Total.Instance.Display_GUI_Equipslot_Remove_Information(); // 장비아이템 해제 알림 GUI 활성화
            }
        }
        // 장비창 GUI 업데이트 - 장비아이템(하의)
        if (Player_Equipment.m_bEquipment_Bottoms == false) // 착용중인 장비아이템(하의)이 존재하지 않는 경우
        {
            m_gary_Equipslot[2].GetComponent<Equipslot>().SetNull(); // 장비아이템(하의)에 해당하는 장비창 슬롯 GUI 초기화
        }
        else
        {
            m_gary_Equipslot[2].GetComponent<Equipslot>().SetItem_Equip(Player_Equipment.m_gEquipment_Bottoms); // 장비아이템(하의)에 해당하는 장비창 슬롯 GUI 장비아이템 외형 표시
            if (Player_Total.Instance.CheckCondition_Item_Equip_Bottoms() == true) { } // 착용중인 장비아이템(하의) 착용 조건 판단
            else
            {
                m_gary_Equipslot[2].GetComponent<Equipslot>().SetItem_Equip_NotApply(); // 착용 조건을 불충족한 장비아이템 마스크 적용(활성화)
                GUIManager_Total.Instance.Display_GUI_Equipslot_Remove_Information(); // 장비아이템 해제 알림 GUI 활성화
            }
        }
        // 장비창 GUI 업데이트 - 장비아이템(신발)
        if (Player_Equipment.m_bEquipment_Shose == false) // 착용중인 장비아이템(신발)이 존재하지 않는 경우
        {
            m_gary_Equipslot[3].GetComponent<Equipslot>().SetNull(); // 장비아이템(신발)에 해당하는 장비창 슬롯 GUI 초기화
        }
        else
        {
            m_gary_Equipslot[3].GetComponent<Equipslot>().SetItem_Equip(Player_Equipment.m_gEquipment_Shose); // 장비아이템(신발)에 해당하는 장비창 슬롯 GUI 장비아이템 외형 표시
            if (Player_Total.Instance.CheckCondition_Item_Equip_Shose() == true) { } // 착용중인 장비아이템(신발) 착용 조건 판단
            else
            {
                m_gary_Equipslot[3].GetComponent<Equipslot>().SetItem_Equip_NotApply(); // 착용 조건을 불충족한 장비아이템 마스크 적용(활성화)
                GUIManager_Total.Instance.Display_GUI_Equipslot_Remove_Information(); // 장비아이템 해제 알림 GUI 활성화
            }
        }
        // 장비창 GUI 업데이트 - 장비아이템(장갑)
        if (Player_Equipment.m_bEquipment_Gloves == false) // 착용중인 장비아이템(장갑)이 존재하지 않는 경우
        {
            m_gary_Equipslot[4].GetComponent<Equipslot>().SetNull(); // 장비아이템(장갑)에 해당하는 장비창 슬롯 GUI 초기화
        }
        else
        {
            m_gary_Equipslot[4].GetComponent<Equipslot>().SetItem_Equip(Player_Equipment.m_gEquipment_Gloves); // 장비아이템(장갑)에 해당하는 장비창 슬롯 GUI 장비아이템 외형 표시
            if (Player_Total.Instance.CheckCondition_Item_Equip_Gloves() == true) { } // 착용중인 장비아이템(장갑) 착용 조건 판단
            else
            {
                m_gary_Equipslot[4].GetComponent<Equipslot>().SetItem_Equip_NotApply(); // 착용 조건을 불충족한 장비아이템 마스크 적용(활성화)
                GUIManager_Total.Instance.Display_GUI_Equipslot_Remove_Information(); // 장비아이템 해제 알림 GUI 활성화
            }
        }
        // 장비창 GUI 업데이트 - 장비아이템(주무기)
        if (Player_Equipment.m_bEquipment_Mainweapon == false) // 착용중인 장비아이템(주무기)이 존재하지 않는 경우
        {
            m_gary_Equipslot[5].GetComponent<Equipslot>().SetNull(); // 장비아이템(주무기)에 해당하는 장비창 슬롯 GUI 초기화
        }
        else
        {
            m_gary_Equipslot[5].GetComponent<Equipslot>().SetItem_Equip(Player_Equipment.m_gEquipment_Mainweapon); // 장비아이템(주무기)에 해당하는 장비창 슬롯 GUI 장비아이템 외형 표시
            if (Player_Total.Instance.CheckCondition_Item_Equip_MainWeapon() == true) { } // 착용중인 장비아이템(주무기) 착용 조건 판단
            else
            {
                m_gary_Equipslot[5].GetComponent<Equipslot>().SetItem_Equip_NotApply(); // 착용 조건을 불충족한 장비아이템 마스크 적용(활성화)
                GUIManager_Total.Instance.Display_GUI_Equipslot_Remove_Information(); // 장비아이템 해제 알림 GUI 활성화
            }
        }
        // 장비창 GUI 업데이트 - 장비아이템(보조무기)
        if (Player_Equipment.m_bEquipment_Subweapon == false) // 착용중인 장비아이템(보조무기)이 존재하지 않는 경우
        {
            m_gary_Equipslot[6].GetComponent<Equipslot>().SetNull(); // 장비아이템(보조무기)에 해당하는 장비창 슬롯 GUI 초기화
        }
        else
        {
            m_gary_Equipslot[6].GetComponent<Equipslot>().SetItem_Equip(Player_Equipment.m_gEquipment_Subweapon); // 장비아이템(보조무기)에 해당하는 장비창 슬롯 GUI 장비아이템 외형 표시
            if (Player_Total.Instance.CheckCondition_Item_Equip_SubWeapon() == true) { } // 착용중인 장비아이템(보조무기) 착용 조건 판단
            else
            {
                m_gary_Equipslot[6].GetComponent<Equipslot>().SetItem_Equip_NotApply(); // 착용 조건을 불충족한 장비아이템 마스크 적용(활성화)
                GUIManager_Total.Instance.Display_GUI_Equipslot_Remove_Information(); // 장비아이템 해제 알림 GUI 활성화
            }
        }
    }

    // 장비창(상태창) GUI 활성화 / 비활성화 함수
    // return true : 장비창(상태창) GUI 활성화 / return false : 장비창(상태창) GUI 비활성화
    public bool Display_GUI_Equipslot()
    {
        if (m_gPanel_Equipslot.activeSelf == true)
        {
            m_gPanel_ES.SetActive(false);
            m_gPanel_Equipslot.SetActive(false);

            return false;
        }
        else
        {
            m_BTN_Equipslot_Exit.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            m_gPanel_ES.SetActive(true);
            m_gPanel_Equipslot.SetActive(true);

            m_gPanel_ES.transform.SetAsLastSibling();
            m_gPanel_Equipslot.transform.SetAsLastSibling();

            return true;
        }
    }
}
