using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// ※ 싱글톤패턴을 적용한 ItemSetEffectManager 클래스를 이용해 모든 아이템 세트효과를 관리한다.
//    추후 최적화를 위해 플레이어에게 적용중인 아이템 세트효과 데이터만을 로드해 메모리 성능을 높일 예정이다.
//    최적화 관련 정보는 아래 링크를 참조해 주세요.
//    
//
public class ItemSetEffectManager : MonoBehaviour
{
    public static ItemSetEffectManager m_ItemSetEffectManager = null;
    public static ItemSetEffectManager instance
    {
        get
        {
            if (m_ItemSetEffectManager == null)
            {
                return null;
            }
            return m_ItemSetEffectManager;
        }
    }
    private void Awake()
    {
        if (m_ItemSetEffectManager == null)
        {
            m_ItemSetEffectManager = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
    public static Dictionary<int, ItemSetEffect> m_Dictionary_ItemSetEffect; // 아이템 세트효과 딕셔너리. Dictionary <Key : 아이템 세트효과 고유코드, Value : 아이템 세트효과 데이터>

    // 변수 초기화 및 데이터 로딩
    public void InitialSet()
    {
        m_Dictionary_ItemSetEffect = new Dictionary<int, ItemSetEffect>();

        ItemSetEffect ise;

        ise = new ItemSetEffect("[마을 경비대 세트 효과]", 1);
        ise.AddItem(3003, "마을 경비대의 갑옷[투구]");
        ise.AddItem(4006, "마을 경비대의 갑옷[상의]");
        ise.AddItem(5001, "마을 경비대의 갑옷[하의]");
        ise.AddItem(6003, "마을 경비대의 갑옷[장화]");
        ise.AddItemSetEffect(1, new STATUS(0, 0, 0, 1, 0, 0, 0, 1, 1, 1, 0, 0, 0, 1, 1, 0, 0), new SOC(3, 1, 1, 1, 1, 1, 1, 1, -1), "마을 경비대?");
        ise.AddItemSetEffect(2, new STATUS(0, 0, 0, 2, 0, 0, 0, 1, 1, 1, 0, 0, 0, 1, 1, 0, 0), new SOC(3, 1, 1, 1, 1, 1, 1, 1, -1), "서투른 마을 경비대.");
        ise.AddItemSetEffect(3, new STATUS(0, 0, 0, 5, 0, 0, 0, 1, 1, 1, 0, 0, 5, 1, 1, 0, 0), new SOC(3, 1, 1, 1, 1, 1, 1, 1, -1), "평범한 마을 경비대.");
        ise.AddItemSetEffect(4, new STATUS(0, 0, 0, 10, 0, 0, 0, 2, 2, 2, 0, 0, 5, 2, 2, 0, 0), new SOC(3, 1, 1, 1, 1, 1, 1, 1, -1), "숙련된 마을 경비대.");
        m_Dictionary_ItemSetEffect.Add(ise.m_nItemSetEffect_Code, ise);

        ise = new ItemSetEffect("[초급 용병의 갑옷 세트 효과]", 2);
        ise.AddItem(3004, "초급 용병의 갑옷[투구]");
        ise.AddItem(4009, "초급 용병의 갑옷[상의]");
        ise.AddItem(5002, "초급 용병의 갑옷[하의]");
        ise.AddItem(6004, "초급 용병의 갑옷[장화]");
        ise.AddItemSetEffect(1, new STATUS(0, 0, 0, 1, 0, 0, 0, 1, 1, 1, 0, 0, 5, 1, 1, 0, 0), new SOC(-5, -1, -1, -1, -1, -1, -1, -1, -1), "용병?");
        ise.AddItemSetEffect(2, new STATUS(0, 0, 0, 2, 0, 0, 0, 1, 1, 1, 0, 0, 5, 1, 1, 0, 0), new SOC(-5, -1, -1, -1, -1, -1, -1, -1, -1), "신출내기 용병.");
        ise.AddItemSetEffect(3, new STATUS(0, 0, 0, 5, 0, 0, 0, 2, 2, 2, 0, 0, 5, 2, 2, 0, 0), new SOC(-5, -1, -1, -1, -1, -1, -1, -1, -1), "강인한 용병.");
        ise.AddItemSetEffect(4, new STATUS(0, 0, 0, 10, 0, 0, 0, 5, 5, 5, 0, 0, 5, 5, 5, 0, 0), new SOC(-5, -1, -1, -1, -1, -1, -1, -1, -1), "속전속결의 용병.");
        m_Dictionary_ItemSetEffect.Add(ise.m_nItemSetEffect_Code, ise);

        ise = new ItemSetEffect("[초급 기사의 갑옷 세트 효과]", 3);
        ise.AddItem(3007, "초급 기사의 갑옷[투구]");
        ise.AddItem(4013, "초급 기사의 갑옷[상의]");
        ise.AddItem(5005, "초급 기사의 갑옷[하의]");
        ise.AddItem(6007, "초급 기사의 갑옷[장화]");
        ise.AddItemSetEffect(1, new STATUS(0, 0, 0, 1, 0, 0, 0, 1, 1, 1, 0, 0, 5, 1, 1, 0, 0), new SOC(5, 1, 1, 1, 1, 1, 1, 1, 1), "기사?");
        ise.AddItemSetEffect(2, new STATUS(0, 0, 0, 2, 0, 0, 0, 1, 1, 1, 0, 0, 5, 1, 1, 0, 0), new SOC(5, 1, 1, 1, 1, 1, 1, 1, 1), "무언가 부족한 기사.");
        ise.AddItemSetEffect(3, new STATUS(0, 0, 0, 5, 0, 0, 0, 2, 2, 2, 0, 0, 5, 2, 2, 0, 0), new SOC(5, 1, 1, 1, 1, 1, 1, 1, 1), "강인한 기사.");
        ise.AddItemSetEffect(4, new STATUS(0, 0, 0, 10, 0, 0, 0, 5, 5, 5, 0, 0, 5, 5, 5, 0, 0), new SOC(5, 1, 1, 1, 1, 1, 1, 1, 1), "강철의 기사.");
        m_Dictionary_ItemSetEffect.Add(ise.m_nItemSetEffect_Code, ise);

        ise = new ItemSetEffect("[야만 전사 우요의 세트 효과]", 4);
        ise.AddItem(1610, "야만 전사 우요의 오른솓도끼");
        ise.AddItem(2003, "야만 전사 우요의 왼손해머");
        ise.AddItemSetEffect(1, new STATUS(0), new SOC(0), "");
        ise.AddItemSetEffect(2, new STATUS(0, 0, 0, 20, 0, 10, 0, 5, 5, 5, 0, 0, 15, 5, 5, 0, -0.2f), new SOC(-5, 0, -5, -5, -5, -5, -5, -5, 1), "야만 전사 우요: 내 오른손의 도끼와 왼손의 해머로 부실 수 없는 것은 없다!\n앞도적 무력 앞에 모두 무릎 꿇어라! 크하하!!");
        m_Dictionary_ItemSetEffect.Add(ise.m_nItemSetEffect_Code, ise);

        ise = new ItemSetEffect("[방랑자의 세트 효과]", 5);
        ise.AddItem(4004, "방랑자의 겉옷");
        ise.AddItem(5008, "방랑자의 겉옷");
        ise.AddItemSetEffect(1, new STATUS(0), new SOC(0), "");
        ise.AddItemSetEffect(2, new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 30, 0, 0, 0, -0.1f), new SOC(0), "???: 이 세계 전역을 떠돌려면 빠르게 움직여야하지..");
        m_Dictionary_ItemSetEffect.Add(ise.m_nItemSetEffect_Code, ise);

        ise = new ItemSetEffect("[초보 모험가 세트 효과]", 6);
        ise.AddItem(1000, "목검");
        ise.AddItem(1300, "나무단검");
        ise.AddItem(1600, "나무도끼");
        ise.AddItem(3001, "초보 모험가의 투구");
        ise.AddItem(4001, "초보 모험가의 상의");
        ise.AddItem(6001, "초보 모험가의 신발");
        ise.AddItemSetEffect(1, new STATUS(0), new SOC(1), "이게 뭐에요? 먹는건가요?");
        ise.AddItemSetEffect(2, new STATUS(0, 0, 0, 3, 0, 3, 0, 0, 0, 0, 0, 0, 0, 3, 3, 0, 0), new SOC(1), "엥? 초보 모험가?");
        ise.AddItemSetEffect(3, new STATUS(0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0), new SOC(1), "이런 효과가!");
        ise.AddItemSetEffect(4, new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 0, 0, 0, -0.1f), new SOC(1), "제법 쓸만한 초보 모험가!");
        ise.AddItemSetEffect(5, new STATUS(0), new SOC(0), "");
        ise.AddItemSetEffect(6, new STATUS(0), new SOC(0), "");
        m_Dictionary_ItemSetEffect.Add(ise.m_nItemSetEffect_Code, ise);

        ise = new ItemSetEffect("[허름한 기사 세트 효과]", 7);
        ise.AddItem(3002, "허름한 기사의 투구");
        ise.AddItem(4002, "허름한 기사의 갑옷");
        ise.AddItem(6002, "허름한 기사의 장화");
        ise.AddItemSetEffect(1, new STATUS(0), new SOC(1), "부를 위해..");
        ise.AddItemSetEffect(2, new STATUS(0), new SOC(2), "명예를 위해..");
        ise.AddItemSetEffect(3, new STATUS(0, 0, 0, 20, 0, 0, 0, 3, 3, 3, 0, 0, 10, 5, 5, 0, -0.2f), new SOC(5), "가족을 위해..");
        m_Dictionary_ItemSetEffect.Add(ise.m_nItemSetEffect_Code, ise);
    }

    public void Information_SetItemEffect(int setitemcode)
    {
        if (m_Dictionary_ItemSetEffect.ContainsKey(setitemcode) == true)
        {
            m_Dictionary_ItemSetEffect[setitemcode].Information_SetItemEffect();
        }
        else
        {
            Debug.Log("NULL_SetItemEffect");
        }
    }

    // 해당 아이템이 세트효과를 가지고 있는지 판단.
    // return null: 세트효과 X.
    public int Return_SetItemEffect(int itemcode)
    {
        foreach (KeyValuePair<int, ItemSetEffect> dictionary in m_Dictionary_ItemSetEffect)
        {
            if (dictionary.Value.m_Dictionary_Item_Equip_Code.ContainsKey(itemcode) == true)
            {
                return dictionary.Key;
            }
        }

        return 0;
    }
    // 아이템 세트 효과 여부 판단.
    public bool Check_SetItemEffect(int setitemnumber, int count)
    {
        if (m_Dictionary_ItemSetEffect[setitemnumber].m_Dictionary_STATUS_Effect[count].CheckIdentity(new STATUS(0)) == true && 
            m_Dictionary_ItemSetEffect[setitemnumber].m_Dictionary_SOC_Effect[count].CheckIdentity(new SOC(0)) == true)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


    // 아이템 세트 효과 반환.
    public STATUS Return_SetItemEffect_STATUS(int setitemnumber, int count)
    {
        return new STATUS(m_Dictionary_ItemSetEffect[setitemnumber].m_Dictionary_STATUS_Effect[count]);
    }
    public SOC Return_SetItemEffect_SOC(int setitemnumber, int count)
    {
        return new SOC(m_Dictionary_ItemSetEffect[setitemnumber].m_Dictionary_SOC_Effect[count]);
    }
    public string Return_SetItemEffect_Name(int setitemnumber, int count)
    {
        return m_Dictionary_ItemSetEffect[setitemnumber].m_sItemSetEffect_Name + " " + count.ToString();
    }
}

//
// ※ 아이템 세트효과 클래스
//

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

