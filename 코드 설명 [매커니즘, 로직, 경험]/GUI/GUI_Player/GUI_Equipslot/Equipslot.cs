using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

//
// ※ 장비창 슬롯 GUI
//    해당 GUI는 장비창GUI에 포함된 각각의 장비창 슬롯에 해당한다.
//    착용중인 장비아이템의 외형이 해당 GUI를 통해 표시되며, 마우스 이벤트 함수를 이용해 해당 GUI 클릭 시 장비아이템의 세부 정보를 확인할 수 있는 GUI가 활성화되는 기능을 구현했다.
//

public class Equipslot : MonoBehaviour, IPointerClickHandler
{
    // GUI 오브젝트
    [SerializeField] Image m_IMG_ItemSprite;       // (이미지) 장비아이템 외형
    [SerializeField] Image m_IMG_BackgroundSprite; // (이미지) 장비창 슬롯 GUI 배경
    [SerializeField] Image m_IMG_RedMask;          // (이미지) 장비아이템 착용 조건 불충족 시 활성화되는 마스크
    
    public int m_nAryNumber; // 장비창 슬롯 고유코드. 해당 고유코드는 장비아이템 타입을 의미하며, 중복된 값을 가질 수 없다.
                             // 0 : 모자
                             // 1 : 상의
                             // 2 : 하의
                             // 3 : 신발
                             // 4 : 장갑
                             // 5 : 주무기
                             // 6 : 보조무기

    // GUI 초기 설정
    public void Awake()
    {
        InitialSet_Object(); // GUI 오브젝트 초기 설정
    }

     // GUI 오브젝트 초기 설정
    void InitialSet_Object()
    {
        m_IMG_ItemSprite = this.gameObject.transform.Find("Panel_ItemSprite").GetComponent<Image>();
        m_IMG_BackgroundSprite = this.gameObject.transform.Find("Panel_BackgroundSprite").GetComponent<Image>();
        m_IMG_RedMask = this.gameObject.transform.Find("Panel_RedMask").GetComponent<Image>();
    }

    // 장비창 슬롯 GUI 장비아이템 외형 표시
    public void SetItem_Equip(Item_Equip item) // item : 장비아이템
    {
        m_IMG_ItemSprite.sprite = item.m_sp_Sprite;
        m_IMG_ItemSprite.color = new Color(1, 1, 1, 1);
        m_IMG_RedMask.color = new Color(1, 0, 0, 0);
    }
    // 착용 조건을 불충족한 장비아이템 마스크 적용(활성화)
    public void SetItem_Equip_NotApply()
    {
        m_IMG_RedMask.color = new Color(1, 0, 0, 0.33f);
    }
    // 장비창 슬롯 GUI 초기화
    public void SetNull()
    {
        m_IMG_ItemSprite.color = new Color(1, 1, 1, 0);
        m_IMG_ItemSprite.sprite = null;
        m_IMG_RedMask.color = new Color(1, 0, 0, 0);
    }

    // 마우스 클릭 이벤트 함수
    // 장비창 슬롯 GUI 클릭 시 장비아이템의 세부 정보를 확인할 수 있는 GUI 활성화
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        switch (m_nAryNumber) // 장비창 슬롯 고유코드
        {
            case 0:
                {
                    if (Player_Equipment.m_bEquipment_Hat == true) // 착용중인 장비아이템(모자)이 존재하는 경우
                    {
                        GUIManager_Total.Instance.m_GUI_Equipslot_Equip_Information.m_bEquip_Condition_Check = true;
                        GUIManager_Total.Instance.Update_Equipslot_Equip_Information(Player_Equipment.m_gEquipment_Hat, m_nAryNumber); // 착용중인 장비아이템(모자) 세부 정보 GUI 업데이트
                        GUIManager_Total.Instance.Display_GUI_Equipslot_Equip_Information(Mathf.Round(this.gameObject.transform.position.x), Mathf.Round(this.gameObject.transform.position.y)); // 장비아이템 세부 정보 GUI 활성화
                    }
                    else
                        GUIManager_Total.Instance.UnDisplay_GUI_Equipslot_Equip_Information(); // 장비아이템 세부 정보 GUI 비활성화
                }
                break;
            case 1:
                {
                    if (Player_Equipment.m_bEquipment_Top == true) // 착용중인 장비아이템(상의)이 존재하는 경우
                    {
                        GUIManager_Total.Instance.m_GUI_Equipslot_Equip_Information.m_bEquip_Condition_Check = true;
                        GUIManager_Total.Instance.Update_Equipslot_Equip_Information(Player_Equipment.m_gEquipment_Top, m_nAryNumber); // 착용중인 장비아이템(상의) 세부 정보 GUI 업데이트
                        GUIManager_Total.Instance.Display_GUI_Equipslot_Equip_Information(Mathf.Round(this.gameObject.transform.position.x), Mathf.Round(this.gameObject.transform.position.y)); // 장비아이템 세부 정보 GUI 활성화
                    }
                    else
                        GUIManager_Total.Instance.UnDisplay_GUI_Equipslot_Equip_Information(); // 장비아이템 세부 정보 GUI 비활성화
                }
                break;
            case 2:
                {
                    if (Player_Equipment.m_bEquipment_Bottoms == true) // 착용중인 장비아이템(하의)이 존재하는 경우
                    {
                        GUIManager_Total.Instance.m_GUI_Equipslot_Equip_Information.m_bEquip_Condition_Check = true;
                        GUIManager_Total.Instance.Update_Equipslot_Equip_Information(Player_Equipment.m_gEquipment_Bottoms, m_nAryNumber); // 착용중인 장비아이템(하의) 세부 정보 GUI 업데이트
                        GUIManager_Total.Instance.Display_GUI_Equipslot_Equip_Information(Mathf.Round(this.gameObject.transform.position.x), Mathf.Round(this.gameObject.transform.position.y)); // 장비아이템 세부 정보 GUI 활성화
                    }
                    else
                        GUIManager_Total.Instance.UnDisplay_GUI_Equipslot_Equip_Information(); // 장비아이템 세부 정보 GUI 비활성화
                }
                break;
            case 3:
                {
                    if (Player_Equipment.m_bEquipment_Shoes == true) // 착용중인 장비아이템(신발)이 존재하는 경우
                    {
                        GUIManager_Total.Instance.m_GUI_Equipslot_Equip_Information.m_bEquip_Condition_Check = true;
                        GUIManager_Total.Instance.Update_Equipslot_Equip_Information(Player_Equipment.m_gEquipment_Shoes, m_nAryNumber); // 착용중인 장비아이템(신발) 세부 정보 GUI 업데이트
                        GUIManager_Total.Instance.Display_GUI_Equipslot_Equip_Information(Mathf.Round(this.gameObject.transform.position.x), Mathf.Round(this.gameObject.transform.position.y)); // 장비아이템 세부 정보 GUI 활성화
                    }
                    else
                        GUIManager_Total.Instance.UnDisplay_GUI_Equipslot_Equip_Information(); // 장비아이템 세부 정보 GUI 비활성화
                }
                break;
            case 4:
                {
                    if (Player_Equipment.m_bEquipment_Gloves == true) // 착용중인 장비아이템(장갑)이 존재하는 경우
                    {
                        GUIManager_Total.Instance.m_GUI_Equipslot_Equip_Information.m_bEquip_Condition_Check = true;
                        GUIManager_Total.Instance.Update_Equipslot_Equip_Information(Player_Equipment.m_gEquipment_Gloves, m_nAryNumber); // 착용중인 장비아이템(장갑) 세부 정보 GUI 업데이트
                        GUIManager_Total.Instance.Display_GUI_Equipslot_Equip_Information(Mathf.Round(this.gameObject.transform.position.x), Mathf.Round(this.gameObject.transform.position.y)); // 장비아이템 세부 정보 GUI 활성화
                    }
                    else
                        GUIManager_Total.Instance.UnDisplay_GUI_Equipslot_Equip_Information(); // 장비아이템 세부 정보 GUI 비활성화
                }
                break;
            case 5:
                {
                    if (Player_Equipment.m_bEquipment_Mainweapon == true) // 착용중인 장비아이템(주무기)이 존재하는 경우
                    {
                        GUIManager_Total.Instance.m_GUI_Equipslot_Equip_Information.m_bEquip_Condition_Check = true;
                        GUIManager_Total.Instance.Update_Equipslot_Equip_Information(Player_Equipment.m_gEquipment_Mainweapon, m_nAryNumber); // 착용중인 장비아이템(주무기) 세부 정보 GUI 업데이트
                        GUIManager_Total.Instance.Display_GUI_Equipslot_Equip_Information(Mathf.Round(this.gameObject.transform.position.x), Mathf.Round(this.gameObject.transform.position.y)); // 장비아이템 세부 정보 GUI 활성화
                    }
                    else
                        GUIManager_Total.Instance.UnDisplay_GUI_Equipslot_Equip_Information(); // 장비아이템 세부 정보 GUI 비활성화
                }
                break;
            case 6:
                {
                    if (Player_Equipment.m_bEquipment_Subweapon == true) // 착용중인 장비아이템(보조무기)이 존재하는 경우
                    {
                        GUIManager_Total.Instance.m_GUI_Equipslot_Equip_Information.m_bEquip_Condition_Check = true;
                        GUIManager_Total.Instance.Update_Equipslot_Equip_Information(Player_Equipment.m_gEquipment_Subweapon, m_nAryNumber); // 착용중인 장비아이템(보조무기) 세부 정보 GUI 업데이트
                        GUIManager_Total.Instance.Display_GUI_Equipslot_Equip_Information(Mathf.Round(this.gameObject.transform.position.x), Mathf.Round(this.gameObject.transform.position.y)); // 장비아이템 세부 정보 GUI 활성화
                    }
                    else
                        GUIManager_Total.Instance.UnDisplay_GUI_Equipslot_Equip_Information(); // 장비아이템 세부 정보 GUI 비활성화
            }
                break;
        }
    }
}
