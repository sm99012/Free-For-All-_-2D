﻿using System.Collections;
using System.Collections.Generic;

public class ItemSetEffect // 아이템 세트효과 클래스
{
    public string m_sItemSetEffect_Name; // 아이템 세트효과 이름
    public int m_nItemSetEffect_Code;    // 아이템 세트효과 고유코드(n > 0)

    public Dictionary<int, STATUS> m_Dictionary_STATUS_Effect; // 아이템 세트효과 추가 스탯(능력치) 딕셔너리. Dictionary <Key : 장비아이템 개수, Value : 추가 스탯(능력치)>
    public Dictionary<int, SOC> m_Dictionary_SOC_Effect;       // 아이템 세트효과 추가 스탯(평판) 딕셔너리. Dictionary <Key : 장비아이템 개수, Value : 추가 스탯(평판)>
    public Dictionary<int, string> m_Dictionary_Description;   // 아이템 세트효과 설명 딕셔너리. Dictionary <Key : 장비아이템 개수, Value : 설명>

    public Dictionary<int, int> m_Dictionary_Item_Equip_Code; // 아이템 세트효과 고유코드를 보유한 장비아이템 딕셔너리. Dictionary <Key : 장비아이템 고유코드, Value : 장비아이템 고유코드>
                                                              //
                                                              // ※ 이렇게 딕셔너리를 이용해 구현할 필요가 없다. 추후 변경 예정
                                                              //

    // 아이템 세트효과 데이터를 생성하는 생성자
    public ItemSetEffect(string name, int code)
    {
        this.m_sItemSetEffect_Name = name;
        this.m_nItemSetEffect_Code = code;

        m_Dictionary_STATUS_Effect = new Dictionary<int, STATUS>();
        m_Dictionary_SOC_Effect = new Dictionary<int, SOC>();
        m_Dictionary_Description = new Dictionary<int, string>();

        m_Dictionary_Item_Equip_Code = new Dictionary<int, int>();
    }

    // 아이템 세트효과 데이터 설정 관련 함수. 아이템 세트효과 고유코드를 보유한 장비아이템 개수별 아이템 세트효과 설정
    //
    // Ex) '굉장히 엄청난 세트 _ 1' : 해당 아이템 세트효과 고유코드를 보유한 장비아이템 1개 착용
    //     '굉장히 엄청난 세트 _ 2' : 해당 아이템 세트효과 고유코드를 보유한 장비아이템 2개 착용(해당 아이템 세트효과에 추가 스탯(능력치, 평판)이 존재하지 않는 경우 해당 아이템 세트효과는 표시되지 않는다.)
    //     '굉장히 엄청난 세트 _ 3' : 해당 아이템 세트효과 고유코드를 보유한 장비아이템 3개 착용
    //     '굉장히 엄청난 세트 _ n' : 해당 아이템 세트효과 고유코드를 보유한 장비아이템 n개 착용
    //
    public bool AddItemSetEffect(int itemcount, STATUS statuseffect, SOC soceffect, string description = "") // itemcount : 아이템 세트효과 고유코드를 보유한 장비아이템 개수, statuseffect : 추가 스탯(능력치), soceffect : 추가 스탯(평판), description : 설명
    {
        // 아이템 세트효과 데이터(스탯(능력치)) 설정
        if (AddItemSetEffect_STATUS(itemcount, statuseffect) == false)
        {
            return false;
        }
        // 아이템 세트효과 데이터(스탯(평판)) 설정
        if (AddItemSetEffect_SOC(itemcount, soceffect) == false)
        {
            return false;
        }
        // 아이템 세트효과 데이터(설명) 설정
        if (AddItemSetEffect_Description(itemcount, description) == false)
        {
            return false;
        }
        return true;
    }
    // 아이템 세트효과 데이터(스탯(능력치)) 설정
    bool AddItemSetEffect_STATUS(int itemcount, STATUS statuseffect) // itemcount : 아이템 세트효과 고유코드를 보유한 장비아이템 개수, statuseffect : 추가 스탯(능력치)
    {
        if (m_Dictionary_STATUS_Effect.ContainsKey(itemcount) == false)
        {
            m_Dictionary_STATUS_Effect.Add(itemcount, statuseffect);

            return true;
        }
        return false;
    }
    // 아이템 세트효과 데이터(스탯(평판)) 설정
    bool AddItemSetEffect_SOC(int itemcount, SOC soceffect) // itemcount : 아이템 세트효과 고유코드를 보유한 장비아이템 개수, soceffect : 추가 스탯(평판)
    {
        if (m_Dictionary_SOC_Effect.ContainsKey(itemcount) == false)
        {
            m_Dictionary_SOC_Effect.Add(itemcount, soceffect);

            return true;
        }
        return false;
    }
    // 아이템 세트효과 데이터(설명) 설정
    bool AddItemSetEffect_Description(int itemcount, string description) // itemcount : 아이템 세트효과 고유코드를 보유한 장비아이템 개수, description : 설명
    {
        if (m_Dictionary_Description.ContainsKey(itemcount) == false)
        {
            m_Dictionary_Description.Add(itemcount, description);

            return true;
        }
        return false;
    }

    // 아이템 세트효과 고유코드를 보유한 장비아이템 추가
    // return true : 추가 성공 / return false : 추가 실패(이미 추가된 상태)
    public bool AddItem(int itemcode, string itemname = "") // itemcode : 장비아이템 고유코드, itemname : 장비아이템 이름(확인용)
    {
        if (m_Dictionary_Item_Equip_Code.ContainsKey(itemcode) == false)
        {
            m_Dictionary_Item_Equip_Code.Add(itemcode, itemcode);

            return true;
        }
        return false;
    }

    // 아이템 세트효과 정보 출력(테스트 전용)
    public void Information_SetItemEffect()
    {
        Debug.Log(m_sItemSetEffect_Name + " / " + m_nItemSetEffect_Code);
        Debug.Log(m_Dictionary_STATUS_Effect[1].GetSTATUS_Data());
        Debug.Log(m_Dictionary_SOC_Effect[1].GetSOC_Data());
    }
}
