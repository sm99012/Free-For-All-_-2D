using System.Collections;
using System.Collections.Generic;

public class ItemSetEffect
{
    public string m_sItemSetEffect_Name;
    public int m_nItemSetEffect_Code;

    // m_Dictionary_STATUS_Effect[0]: 세트효과 없음.
    // m_Dictionary_STATUS_Effect[1]: 세트 아이템 1개 장착 효과.
    // m_Dictionary_STATUS_Effect[2]: 세트 아이템 2개 장착 효과.
    // m_Dictionary_STATUS_Effect[3]: 세트 아이템 3개 장착 효과.
    // . . .
    public Dictionary<int, STATUS> m_Dictionary_STATUS_Effect;
    public Dictionary<int, SOC> m_Dictionary_SOC_Effect;
    public Dictionary<int, string> m_Dictionary_Description;

    // 세트 아이템 리스트. - 아이템 코드로 확인.
    public Dictionary<int, int> m_Dictionary_Item_Equip_Code;

    public ItemSetEffect(string name, int code)
    {
        this.m_sItemSetEffect_Name = name;
        this.m_nItemSetEffect_Code = code;

        m_Dictionary_STATUS_Effect = new Dictionary<int, STATUS>();
        m_Dictionary_SOC_Effect = new Dictionary<int, SOC>();
        m_Dictionary_Description = new Dictionary<int, string>();

        m_Dictionary_Item_Equip_Code = new Dictionary<int, int>();
    }

    public bool AddItemSetEffect(int itemcount, STATUS statuseffect, SOC soceffect, string description = "")
    {
        if (AddItemSetEffect_STATUS(itemcount, statuseffect) == false)
        {
            return false;
        }
        if (AddItemSetEffect_SOC(itemcount, soceffect) == false)
        {
            return false;
        }
        if (AddItemSetEffect_Description(itemcount, description) == false)
        {
            return false;
        }
        return true;
    }
    bool AddItemSetEffect_STATUS(int itemcount, STATUS statuseffect)
    {
        if (m_Dictionary_STATUS_Effect.ContainsKey(itemcount) == false)
        {
            m_Dictionary_STATUS_Effect.Add(itemcount, statuseffect);

            return true;
        }
        return false;
    }
    bool AddItemSetEffect_SOC(int itemcount, SOC soceffect)
    {
        if (m_Dictionary_SOC_Effect.ContainsKey(itemcount) == false)
        {
            m_Dictionary_SOC_Effect.Add(itemcount, soceffect);

            return true;
        }
        return false;
    }
    bool AddItemSetEffect_Description(int itemcount, string description)
    {
        if (m_Dictionary_Description.ContainsKey(itemcount) == false)
        {
            m_Dictionary_Description.Add(itemcount, description);

            return true;
        }
        return false;
    }

    public bool AddItem(int itemcode, string itemname = "")
    {
        if (m_Dictionary_Item_Equip_Code.ContainsKey(itemcode) == false)
        {
            m_Dictionary_Item_Equip_Code.Add(itemcode, itemcode);

            return true;
        }
        return false;
    }

    // 세트 아이템 효과 정보 출력.
    public void Information_SetItemEffect()
    {
        //Debug.Log(m_sItemSetEffect_Name + " / " + m_nItemSetEffect_Code);
        //Debug.Log(m_Dictionary_STATUS_Effect[1].GetSTATUS_Data());
        //Debug.Log(m_Dictionary_SOC_Effect[1].GetSOC_Data());
    }
}
