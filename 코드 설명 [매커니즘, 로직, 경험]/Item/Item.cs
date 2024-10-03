using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//
// ※ 기반이 되는 Item 클래스를 구현한 후 Item_Equip(장비아이템), Item_Use(소비아이템), Item_Etc(기타아이템) 클래스를 상속으로 구현했다.
//

// 아이템 타입 : { 장비아이템, 소비아이템, 기타아이템, 골드(재화) }
public enum E_ITEM_TYPE { EQUIP, USE, ETC, GOLD }
// 아이템 등급 : { 일반, 흔함, 희귀, 유물, 전설, 신화 }
public enum E_ITEM_GRADE { NORMAL, COMMON, RARE, RELIC, LEGEND, MYTH }

public class Item : MonoBehaviour
{
    public string m_sItemName;                  // 아이템 이름
    public string m_sItemDescription;           // 아이템 정보(설명)
    public int m_nItemCode;                     // 아이템 고유코드
                                                // 
                                                // ※ 장비아이템(주무기) 고유코드 : 1000 ~ 1999
                                                //    장비아이템(보조무기) 고유코드 : 2000 ~ 2999
                                                //    장비아이템(모자) 고유코드 : 3000 ~ 3999
                                                //    장비아이템(상의) 고유코드 : 4000 ~ 4999
                                                //    장비아이템(하의) 고유코드 : 5000 ~ 5999
                                                //    장비아이템(신발) 고유코드 : 6000 ~ 6999
                                                //    장비아이템(장갑) 고유코드 : 7000 ~ 7999
                                                //
                                                // ※ 소비아이템(회복포션) 고유코드 : 8000 ~ 8999
                                                //    소비아이템(일시적 버프포션) 고유코드 : 9000 ~ 9999
                                                //    소비아이템(영구적 버프포션) 고유코드 : 10000 ~ 10999
                                                //    소비아이템(강화서) 고유코드 : 11000 ~ 11999
                                                //    소비아이템(기프트(아이템 확정 지급형)) 고유코드 : 12000 ~ 12299
                                                //    소비아이템(기프트(랜덤 지급형 _ A 타입)) 고유코드 : 12300 ~ 12599
                                                //    소비아이템(기프트(랜덤 지급형 _ B 타입)) 고유코드 : 12600 ~ 12899
                                                //    소비아이템(기프트(혼합형)) 고유코드 : 12900 ~ 12999
                                                //
                                                // ※ 기타아이템 고유코드 : 0 ~ 999 (기타아이템 고유코드의 경우 20000번대로 수정 예정)
                                                //
    public int m_nItemNumber;                   // 아이템 생성코드(아이템 생성 순서)
    public Sprite m_sp_Sprite;                  // 아이템 스프라이트(이미지)
    public SpriteRenderer m_spr_SpriteRenderer; // 아이템 스프라이트 랜더러(이미지 + 색상 정보 등)

    public E_ITEM_TYPE m_eItemType;   // 아이템 타입
    public E_ITEM_GRADE m_eItemGrade; // 아이템 등급

    // 아이템 착용 및 사용 조건(상한ㆍ하한) 스탯(능력치)
    public STATUS m_sStatus_Limit_Max; // 아이템 착용 및 사용 조건 : 능력치 상한(플레이어의 능력치 합계가 아이템 착용 및 사용 조건(능력치 상한)을 초과한 경우 제한)
    public STATUS m_sStatus_Limit_Min; // 아이템 착용 및 사용 조건 : 능력치 하한(플레이어의 능력치 합계가 아이템 착용 및 사용 조건(능력치 하한)에 미달한 경우 제한)
    // 아이템 착용 및 사용 조건(상한ㆍ하한) 스탯(평판)
    public SOC m_sSoc_Limit_Max; // 아이템 착용 및 사용 조건 : 평판 상한(플레이어의 평판 합계가 아이템 착용 및 사용 조건(평판 상한)을 초과한 경우 제한)
    public SOC m_sSoc_Limit_Min; // 아이템 착용 및 사용 조건 : 평판 하한(플레이어의 평판 합계가 아이템 착용 및 사용 조건(평판 하한)에 미달한 경우 제한)
    // 아이템 착용 및 사용 효과 스탯(능력치)
    public STATUS m_sStatus_Effect;
    // 아이템 착용 및 사용 효과 스탯(평판)
    public SOC m_sSoc_Effect;
    
    // 아이템 가격(가치)
    public int m_nPrice;

    // 아이템 획득 관련 변수
    public bool m_bPossible_Get;  // (m_bPossible_Get == true : 아이템 획득 가능 / m_bPossible_Get == false : 아이템 획득 불가능)
    protected float m_FadeinAlpa; // 아이템 스프라이트 랜더러(이미지) 투명도

    // 소멸자를 이용해 아이템 객체가 정상적으로 제거되는지 확인하는 함수. 테스트용
    //~Item()
    //{
    //    Debug.Log("아이템 원본 삭제: " + m_sItemName);
    //}

    // 아이템 정보(설명) 반환 함수
    public string GetItemDescription()
    {
        return m_sItemDescription;
    }    

    // 아이템 정보(아이템 이름, 아이템 생성코드) 출력 함수(가상 함수). 테스트용
    virtual public void ItemInformation()
    {
        Debug.Log(m_sItemName + ", " + m_nItemNumber);
    }

    // 아이템 생성 시 페이드인 효과 실행 함수
    protected void Fadein()
    {
        StartCoroutine(ProcessFadein());
    }
    // 페이드인 효과 코루틴
    protected IEnumerator ProcessFadein()
    {
        m_bPossible_Get = false;
        while (m_FadeinAlpa != 1)
        {
            m_spr_SpriteRenderer.color = new Color(m_spr_SpriteRenderer.color.r, m_spr_SpriteRenderer.color.g, m_spr_SpriteRenderer.color.b, m_FadeinAlpa);
            m_FadeinAlpa += 0.01f;

            if (m_FadeinAlpa > 1)
            {
                m_FadeinAlpa = 1;
                break;
            }
            yield return new WaitForSeconds(0.01f);
        }
        m_bPossible_Get = true;
    }
}
