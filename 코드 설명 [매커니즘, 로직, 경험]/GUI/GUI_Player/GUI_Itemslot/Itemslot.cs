using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

//
// ※ 인벤토리 슬롯 GUI
//    해당 GUI는 인벤토리GUI에 포함된 각각의 인벤토리 슬롯에 해당한다.
//    보유한 장비아이템, 소비아이템, 기타아이템의 외형이 해당 GUI를 통해 표시되며, 마우스 이벤트 함수를 이용해 해당 GUI 클릭 시 아이템의 세부 정보를 확인할 수 있는 GUI가 활성화되는 기능을 구현했다.
//

public class Itemslot : MonoBehaviour, IPointerClickHandler
{
    // GUI 오브젝트
    [SerializeField] Image m_IMG_ItemSprite;          // (이미지) 아이템 외형
    [SerializeField] Image m_IMG_BackgroundSprite;    // (이미지) 인벤토리 슬롯 GUI 배경
    [SerializeField] TextMeshProUGUI m_TMP_ItemCount; // (텍스트) 아이템 보유 수량

    public int m_nAryNumber; // 인벤토리 슬롯 고유코드. 해당 고유코드는 인벤토리 슬롯(배열) 번호를 의미하며, 중복된 값을 가질 수 없다.
                             // 0 ~ 59 사이의 값을 가진다.

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
        m_TMP_ItemCount = this.gameObject.transform.Find("Text_Count").GetComponent<TextMeshProUGUI>();
    }

    // 인벤토리 슬롯 GUI 장비아이템 외형 표시
    public void SetItem_Equip(Item_Equip item, int count) // item : 장비아이템, count : 장비아이템 개수(1고정)
    {
        m_IMG_ItemSprite.sprite = item.m_sp_Sprite;
        m_IMG_ItemSprite.color = new Color(1, 1, 1, 1);
        m_TMP_ItemCount.text = count.ToString();
    }
    // 인벤토리 슬롯 GUI 소비아이템 외형 표시
    public void SetItem_Use(Item_Use item, int count) // item : 소비아이템, count : 소비아이템 개수
    {
        m_IMG_ItemSprite.sprite = item.m_sp_Sprite;
        m_IMG_ItemSprite.color = new Color(1, 1, 1, 1);
        m_TMP_ItemCount.text = count.ToString();
    }
    // 인벤토리 슬롯 GUI 기타아이템 외형 표시
    public void SetItem_Etc(Item_Etc item, int count) // item : 기타아이템, count : 기타아이템 개수
    {
        m_IMG_ItemSprite.sprite = item.m_sp_Sprite;
        m_IMG_ItemSprite.color = new Color(1, 1, 1, 1);
        m_TMP_ItemCount.text = count.ToString();
    }
    // 인벤토리 슬롯 GUI 초기화
    public void SetNull()
    {
        m_IMG_ItemSprite.color = new Color(1, 1, 1, 0);
        m_IMG_ItemSprite.sprite = null;
        m_TMP_ItemCount.text = null;
    }

    // 마우스 클릭 이벤트 함수
    // 마우스 좌클릭 : 인벤토리 슬롯 GUI 클릭 시 아이템(장비아이템, 소비아이템, 기타아이템) 세부 정보 GUI 활성화
    // 마우스 우클릭 : 퀵슬롯 등록 GUI 활성화
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData) // eventData : 마우스 우클릭, 좌클릭 구분에 사용
    {
        if (eventData.button == PointerEventData.InputButton.Left) // 마우스 좌클릭
        {
            if (GUIManager_Total.Instance != null)
            {
                if (GUIManager_Total.Instance.m_GUI_Itemslot.m_eItemslot == E_ITEMSLOT.EQUIP) // 인벤토리 타입이 장비아이템인 경우
                {
                    if (Player_Itemslot.m_nary_Itemslot_Equip_Count[m_nAryNumber] != 0) // 인벤토리 슬롯에 장비아이템이 존재하는 경우
                    {
                        GUIManager_Total.Instance.Update_Itemslot_Equip_Information(Player_Itemslot.m_gary_Itemslot_Equip[m_nAryNumber], m_nAryNumber); // 인벤토리 장비아이템 세부 정보 GUI 업데이트
                        GUIManager_Total.Instance.Display_GUI_Itemslot_Equip_Information(Mathf.Round(this.gameObject.transform.position.x), Mathf.Round(this.gameObject.transform.position.y)); // 인벤토리 장비아이템 세부 정보 GUI 활성화

                        GUIManager_Total.Instance.UnDisplay_GUI_Quickslot_Signup(); // 퀵슬롯 등록 GUI 비활성화
                    }
                    else
                    {
                        GUIManager_Total.Instance.UnDisplay_GUI_Itemslot_Equip_Information(); // 인벤토리 장비아이템 세부 정보 GUI 비활성화
                    }
                }
                else if (GUIManager_Total.Instance.m_GUI_Itemslot.m_eItemslot == E_ITEMSLOT.USE) // 인벤토리 타입이 소비아이템인 경우
                {
                    if (Player_Itemslot.m_nary_Itemslot_Use_Count[m_nAryNumber] != 0) // 인벤토리 슬롯에 소비아이템이 존재하는 경우
                    {
                        GUIManager_Total.Instance.Update_Itemslot_Use_Information(Player_Itemslot.m_gary_Itemslot_Use[m_nAryNumber], m_nAryNumber); // 인벤토리 소비아이템 세부 정보 GUI 업데이트
                        GUIManager_Total.Instance.Display_GUI_Itemslot_Use_Information(Mathf.Round(this.gameObject.transform.position.x), Mathf.Round(this.gameObject.transform.position.y)); // 인벤토리 소비아이템 세부 정보 GUI 활성화

                        GUIManager_Total.Instance.UnDisplay_GUI_Quickslot_Signup(); // 퀵슬롯 등록 GUI 비활성화
                    }
                    else
                    {
                        GUIManager_Total.Instance.UnDisplay_GUI_Itemslot_Use_Information(); // 인벤토리 소비아이템 세부 정보 GUI 비활성화
                    }
                }
                else if (GUIManager_Total.Instance.m_GUI_Itemslot.m_eItemslot == E_ITEMSLOT.ETC) // 인벤토리 타입이 기타아이템인 경우
                {
                    if (Player_Itemslot.m_nary_Itemslot_Etc_Count[m_nAryNumber] != 0) // 인벤토리 슬롯에 기타아이템이 존재하는 경우
                    {
                        GUIManager_Total.Instance.Update_Itemslot_Etc_Information(Player_Itemslot.m_gary_Itemslot_Etc[m_nAryNumber], m_nAryNumber); // 인벤토리 기타아이템 세부 정보 GUI 업데이트
                        GUIManager_Total.Instance.Display_GUI_Itemslot_Etc_Information(Mathf.Round(this.gameObject.transform.position.x), Mathf.Round(this.gameObject.transform.position.y)); // 인벤토리 기타아이템 세부 정보 GUI 활성화

                        GUIManager_Total.Instance.UnDisplay_GUI_Quickslot_Signup(); // 퀵슬롯 등록 GUI 비활성화
                    }
                    else
                    {
                        GUIManager_Total.Instance.UnDisplay_GUI_Itemslot_Etc_Information(); // 인벤토리 기타아이템 세부 정보 GUI 비활성화
                    }
                }
            }
        }
        if (eventData.button == PointerEventData.InputButton.Right) // 마우스 우클릭
        {
            if (GUIManager_Total.Instance != null)
            {
                if (GUIManager_Total.Instance.m_GUI_Itemslot.m_eItemslot == E_ITEMSLOT.EQUIP) // 인벤토리 타입이 장비아이템인 경우
                {
                    if (Player_Itemslot.m_nary_Itemslot_Equip_Count[m_nAryNumber] != 0) // 인벤토리 슬롯에 장비아이템이 존재하는 경우
                    {
                        if (Player_Itemslot.m_nary_Itemslot_Equip_Count[m_nAryNumber] != 0) // 인벤토리 슬롯에 장비아이템이 존재하는 경우
                        {
                            GUIManager_Total.Instance.Display_GUI_Quickslot_Signup(E_QUICKSLOT_CATEGORY.EQUIP, m_nAryNumber, Mathf.Round(this.transform.position.x), Mathf.Round(this.transform.position.y)); // 퀵슬롯 등록 GUI 활성화
                            
                            GUIManager_Total.Instance.UnDisplay_GUI_Itemslot_Equip_Information(); // 인벤토리 장비아이템 세부 정보 GUI 비활성화
                            GUIManager_Total.Instance.UnDisplay_GUI_Itemslot_Use_Information();   // 인벤토리 소비아이템 세부 정보 GUI 비활성화
                            GUIManager_Total.Instance.UnDisplay_GUI_Itemslot_Etc_Information();   // 인벤토리 기타아이템 세부 정보 GUI 비활성화
                        }
                    }
                }
                if (GUIManager_Total.Instance.m_GUI_Itemslot.m_eItemslot == E_ITEMSLOT.USE) // 인벤토리 타입이 소비아이템인 경우
                {
                    if (Player_Itemslot.m_nary_Itemslot_Use_Count[m_nAryNumber] != 0) // 인벤토리 슬롯에 소비아이템이 존재하는 경우
                    {
                        if (Player_Itemslot.m_nary_Itemslot_Use_Count[m_nAryNumber] != 0) // 인벤토리 슬롯에 소비아이템이 존재하는 경우
                        {
                            GUIManager_Total.Instance.Display_GUI_Quickslot_Signup(E_QUICKSLOT_CATEGORY.USE, Player_Itemslot.m_gary_Itemslot_Use[m_nAryNumber].m_nItemCode, Mathf.Round(this.transform.position.x), Mathf.Round(this.transform.position.y)); // 퀵슬롯 등록 GUI 활성화

                            GUIManager_Total.Instance.UnDisplay_GUI_Itemslot_Equip_Information(); // 인벤토리 장비아이템 세부 정보 GUI 비활성화
                            GUIManager_Total.Instance.UnDisplay_GUI_Itemslot_Use_Information();   // 인벤토리 소비아이템 세부 정보 GUI 비활성화
                            GUIManager_Total.Instance.UnDisplay_GUI_Itemslot_Etc_Information();   // 인벤토리 기타아이템 세부 정보 GUI 비활성화
                        }
                    }
                }
                if (GUIManager_Total.Instance.m_GUI_Itemslot.m_eItemslot == E_ITEMSLOT.ETC) // 인벤토리 타입이 기타아이템인 경우
                {
                    if (Player_Itemslot.m_nary_Itemslot_Etc_Count[m_nAryNumber] != 0) // 인벤토리 슬롯에 기타아이템이 존재하는 경우
                    {
                        if (Player_Itemslot.m_nary_Itemslot_Etc_Count[m_nAryNumber] != 0) // 인벤토리 슬롯에 기타아이템이 존재하는 경우
                        {
                            GUIManager_Total.Instance.Display_GUI_Quickslot_Signup(E_QUICKSLOT_CATEGORY.ETC, Player_Itemslot.m_gary_Itemslot_Etc[m_nAryNumber].m_nItemCode, Mathf.Round(this.transform.position.x), Mathf.Round(this.transform.position.y)); // 퀵슬롯 등록 GUI 활성화

                            GUIManager_Total.Instance.UnDisplay_GUI_Itemslot_Equip_Information(); // 인벤토리 장비아이템 세부 정보 GUI 비활성화
                            GUIManager_Total.Instance.UnDisplay_GUI_Itemslot_Use_Information();   // 인벤토리 소비아이템 세부 정보 GUI 비활성화
                            GUIManager_Total.Instance.UnDisplay_GUI_Itemslot_Etc_Information();   // 인벤토리 기타아이템 세부 정보 GUI 비활성화
                        }
                    }
                }
            }
        }
    }
}
