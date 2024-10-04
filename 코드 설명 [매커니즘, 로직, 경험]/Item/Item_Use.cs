using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 소비아이템 타입 : { 회복포션, 일시적 버프포션, 영구적 버프포션, 강화서, 기프트 }
public enum E_ITEM_USE_TYPE { RECOVERPOTION, TEMPORARYBUFFPOTION, ETERNALBUFFPOTION, REINFORCEMENT, GIFT }
// 소비아이템(기프트) 타입 : { NULL, 기프트(아이템 확정 지급형), 기프트(랜덤 지급형 _ A 타입)(독립시행), 기프트(랜덤 지급형 _ B 타입)(종속시행), 기프트(혼합형) }
public enum E_ITEM_USE_GIFT_TYPE { NULL, FIXEDBOX, RANDOMBOX_INDEPENDENTTRIAL, RANDOMBOX_DEPENDENTTRIAL, FUSION }

public class Item_Use : Item // 기반이 되는 Item 클래스 상속
{
    public E_ITEM_USE_TYPE m_eItemUseType; // 소비아이템 타입

    public float m_fCoolTime; // 소비아이템 사용 쿨타임

    public float m_fDurationTime; // 버프포션 지속시간

    // 소비아이템(강화서) 관련 변수
    public Reinforcement m_Reinforcement_Effect;

    // 소비아이템(기프트) 관련 변수
    public E_ITEM_USE_GIFT_TYPE m_eItemUseGiftType; // 소비아이템(기프트) 타입
    public bool m_bDisplay_Gift_Item; // 소비아이템(기프트) 목록에 포함된 아이템 정보 출력 여부를 결정하는 변수
    // 소비아이템(기프트(랜덥 지급형 _ A, B 타입)) 관련 변수
    public int m_nRandomBox_PickCount_Max; // 획득 가능한 최대 아이템 개수
    public int m_nRandomBox_PickCount_Min; // 획득 가능한 최소 아이템 개수
    // 소비아이템(기프트) 사용 시 획득 가능한 장비아이템 목록
    public Dictionary<int, int> m_nDictionary_Gift_Item_Equip_Code;        // 장비아이템 고유코드 딕셔너리
    public Dictionary<int, int> m_nDictionary_Gift_Item_Equip_Count;       // 장비아이템 획득 개수 딕셔너리
    public Dictionary<int, int> m_nDictionary_Gift_Item_Equip_Probability; // 장비아이템 획득 확률 딕셔너리
    // 소비아이템(기프트) 사용 시 획득 가능한 소비아이템 목록
    public Dictionary<int, int> m_nDictionary_Gift_Item_Use_Code;        // 소비아이템 고유코드 딕셔너리
    public Dictionary<int, int> m_nDictionary_Gift_Item_Use_Count;       // 소비아이템 획득 개수 딕셔너리
    public Dictionary<int, int> m_nDictionary_Gift_Item_Use_Probability; // 소비아이템 획득 확률 딕셔너리
    // 소비아이템(기프트) 사용 시 획득 가능한 기타아이템 목록
    public Dictionary<int, int> m_nDictionary_Gift_Item_Etc_Code;        // 기타아이템 고유코드 딕셔너리
    public Dictionary<int, int> m_nDictionary_Gift_Item_Etc_Count;       // 기타아이템 획득 개수 딕셔너리
    public Dictionary<int, int> m_nDictionary_Gift_Item_Etc_Probability; // 기타아이템 획득 확률 딕셔너리

    //
    // ※ 아이템 고유코드, 획득 개수, 획득 확률 딕셔너리는 소비아이템(기프트) 사용 시 획득 가능한 아이템 관련 정보를 저장한다. 또 각각의 딕셔너리는 서로 연관되어 있다.
    //    1. m_nDictionary_Gift_Item_Equip_Code[n] 고유코드에 해당하는 장비아이템을
    //    2. 1 ~ m_nDictionary_Gift_Item_Equip_Count[n] 개 만큼
    //    3. m_nDictionary_Gift_Item_Equip_Probability[n] 의 확률로 획득 할 수 있다.
    //

    // 생성자 오버로딩을 이용한 소비아이템 생성 함수(경우에 따라 사용하는 생성자가 다르다.)
    // 빈 생성자
    public Item_Use() { }
    // 소비아이템 원본을 생성하는 생성자. 게임 시작 시 최초 1회만 사용되는 생성자
    public Item_Use(string name, int code, string path_sprite, E_ITEM_USE_TYPE iut, E_ITEM_GRADE ig,
        float durationtime, float cooltime, int price)
    {
        this.m_sItemName = name;
        this.m_nItemCode = code;
        this.m_sp_Sprite = Resources.Load<Sprite>(path_sprite);
        
        this.m_eItemType = E_ITEM_TYPE.USE;
        this.m_eItemGrade = ig;
        this.m_eItemUseType = iut;

        // 소비아이템 사용 효과 및 조건(상한ㆍ하한) 스탯(능력치, 평판) 초기화
        this.m_sStatus_Effect = new STATUS(0);
        this.m_sStatus_Limit_Min = new STATUS(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        this.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        this.m_sSoc_Effect = new SOC(0);
        this.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        this.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);

        if (iut == E_ITEM_USE_TYPE.GIFT) // 소비아이템(기프트)의 경우 관련 딕셔너리 초기화
        {
            m_bDisplay_Gift_Item = true;
            m_nDictionary_Gift_Item_Equip_Code = new Dictionary<int, int>();
            m_nDictionary_Gift_Item_Equip_Count = new Dictionary<int, int>();
            m_nDictionary_Gift_Item_Equip_Probability = new Dictionary<int, int>();
            m_nDictionary_Gift_Item_Use_Code = new Dictionary<int, int>();
            m_nDictionary_Gift_Item_Use_Count = new Dictionary<int, int>();
            m_nDictionary_Gift_Item_Use_Probability = new Dictionary<int, int>();
            m_nDictionary_Gift_Item_Etc_Code = new Dictionary<int, int>();
            m_nDictionary_Gift_Item_Etc_Count = new Dictionary<int, int>();
            m_nDictionary_Gift_Item_Etc_Probability = new Dictionary<int, int>();
        }
        else
            m_eItemUseGiftType = E_ITEM_USE_GIFT_TYPE.NULL;

        this.m_fDurationTime = durationtime;
        this.m_fCoolTime = cooltime;
        
        this.m_nPrice = price;
    }
    // Item 사본.
    public Item_Use(Item_Use item, Vector3 itemposition)
    {
        GameObject itemobject = Instantiate(ItemManager.instance.m_gItem_Use_Null);
        Item_Use itemscript = itemobject.GetComponent<Item_Use>();

        itemscript.m_bPossible_Get = false;

        itemscript.m_sItemName = item.m_sItemName;
        itemscript.m_sItemDescription = item.m_sItemDescription;
        itemscript.m_nItemCode = item.m_nItemCode;
        //itemscript.m_nItemNumber = ++ItemManager.sm_nItemNumber;
        itemscript.m_nItemNumber = 0;
        itemscript.m_sp_Sprite = item.m_sp_Sprite;
        itemscript.m_spr_SpriteRenderer = itemobject.GetComponent<SpriteRenderer>();

        itemscript.m_eItemType = E_ITEM_TYPE.USE;
        itemscript.m_eItemGrade = item.m_eItemGrade;
        itemscript.m_eItemUseType = item.m_eItemUseType;

        itemscript.m_fDurationTime = item.m_fDurationTime;
        itemscript.m_fCoolTime = item.m_fCoolTime;
        itemscript.m_nPrice = item.m_nPrice;

        itemscript.m_Reinforcement_Effect = item.m_Reinforcement_Effect;

        itemscript.m_bDisplay_Gift_Item = item.m_bDisplay_Gift_Item;
        itemscript.m_eItemUseGiftType = item.m_eItemUseGiftType;
        itemscript.m_nRandomBox_PickCount_Min = item.m_nRandomBox_PickCount_Min;
        itemscript.m_nRandomBox_PickCount_Max = item.m_nRandomBox_PickCount_Max;
        itemscript.m_nDictionary_Gift_Item_Equip_Code = item.m_nDictionary_Gift_Item_Equip_Code;
        itemscript.m_nDictionary_Gift_Item_Equip_Count = item.m_nDictionary_Gift_Item_Equip_Count;
        itemscript.m_nDictionary_Gift_Item_Equip_Probability = item.m_nDictionary_Gift_Item_Equip_Probability;
        itemscript.m_nDictionary_Gift_Item_Use_Code = item.m_nDictionary_Gift_Item_Use_Code;
        itemscript.m_nDictionary_Gift_Item_Use_Count = item.m_nDictionary_Gift_Item_Use_Count;
        itemscript.m_nDictionary_Gift_Item_Use_Probability = item.m_nDictionary_Gift_Item_Use_Probability;
        itemscript.m_nDictionary_Gift_Item_Etc_Code = item.m_nDictionary_Gift_Item_Etc_Code;
        itemscript.m_nDictionary_Gift_Item_Etc_Count = item.m_nDictionary_Gift_Item_Etc_Count;
        itemscript.m_nDictionary_Gift_Item_Etc_Probability = item.m_nDictionary_Gift_Item_Etc_Probability;

        itemscript.m_sStatus_Effect = item.m_sStatus_Effect;
        itemscript.m_sStatus_Limit_Min = item.m_sStatus_Limit_Min;
        itemscript.m_sStatus_Limit_Max = item.m_sStatus_Limit_Max;
        itemscript.m_sSoc_Effect = item.m_sSoc_Effect;
        itemscript.m_sSoc_Limit_Min = item.m_sSoc_Limit_Min;
        itemscript.m_sSoc_Limit_Max = item.m_sSoc_Limit_Max;

        itemobject.transform.position = itemposition;

        itemobject.GetComponent<SpriteRenderer>().sprite = item.m_sp_Sprite;
        itemobject.name = item.m_sItemName;

        Debug.Log("ItemName: " + itemscript.m_sItemName + ", ItemUseType: " + itemscript.m_eItemUseType + ", ItemNumber: " + itemscript.m_nItemNumber);
        //if (itemscript.m_eItemUseType == E_ITEM_USE_TYPE.GIFT)
        //{
        //    Debug.Log(itemscript.m_nDictionary_Gift_Item_Equip_Code.Count + " / " + itemscript.m_nDictionary_Gift_Item_Use_Code.Count + " / " + itemscript.m_nDictionary_Gift_Item_Etc_Code.Count);
        //    Debug.Log(itemscript.Return_Gift_List());
        //}

        itemscript.m_FadeinAlpa = 0;
        itemscript.Fadein();
    }

    public Item_Use DeleteItem(Item_Use item)
    {
        Item_Use itemscript = new Item_Use();

        itemscript.m_sItemName = item.m_sItemName;
        itemscript.m_sItemDescription = item.m_sItemDescription;
        itemscript.m_nItemCode = item.m_nItemCode;
        //itemscript.m_nItemNumber = item.m_nItemNumber;
        itemscript.m_nItemNumber = 0;
        itemscript.m_sp_Sprite = item.m_sp_Sprite;

        itemscript.m_eItemType = E_ITEM_TYPE.USE;
        itemscript.m_eItemGrade = item.m_eItemGrade;
        itemscript.m_eItemUseType = item.m_eItemUseType;

        itemscript.m_fDurationTime = item.m_fDurationTime;
        itemscript.m_fCoolTime = item.m_fCoolTime;
        itemscript.m_nPrice = item.m_nPrice;

        itemscript.m_Reinforcement_Effect = item.m_Reinforcement_Effect;

        itemscript.m_bDisplay_Gift_Item = item.m_bDisplay_Gift_Item;
        itemscript.m_eItemUseGiftType = item.m_eItemUseGiftType;
        itemscript.m_nRandomBox_PickCount_Min = item.m_nRandomBox_PickCount_Min;
        itemscript.m_nRandomBox_PickCount_Max = item.m_nRandomBox_PickCount_Max;
        itemscript.m_nDictionary_Gift_Item_Equip_Code = item.m_nDictionary_Gift_Item_Equip_Code;
        itemscript.m_nDictionary_Gift_Item_Equip_Count = item.m_nDictionary_Gift_Item_Equip_Count;
        itemscript.m_nDictionary_Gift_Item_Equip_Probability = item.m_nDictionary_Gift_Item_Equip_Probability;
        itemscript.m_nDictionary_Gift_Item_Use_Code = item.m_nDictionary_Gift_Item_Use_Code;
        itemscript.m_nDictionary_Gift_Item_Use_Count = item.m_nDictionary_Gift_Item_Use_Count;
        itemscript.m_nDictionary_Gift_Item_Use_Probability = item.m_nDictionary_Gift_Item_Use_Probability;
        itemscript.m_nDictionary_Gift_Item_Etc_Code = item.m_nDictionary_Gift_Item_Etc_Code;
        itemscript.m_nDictionary_Gift_Item_Etc_Count = item.m_nDictionary_Gift_Item_Etc_Count;
        itemscript.m_nDictionary_Gift_Item_Etc_Probability = item.m_nDictionary_Gift_Item_Etc_Probability;

        itemscript.m_sStatus_Effect = item.m_sStatus_Effect;
        itemscript.m_sStatus_Limit_Min = item.m_sStatus_Limit_Min;
        itemscript.m_sStatus_Limit_Max = item.m_sStatus_Limit_Max;
        itemscript.m_sSoc_Effect = item.m_sSoc_Effect;
        itemscript.m_sSoc_Limit_Min = item.m_sSoc_Limit_Min;
        itemscript.m_sSoc_Limit_Max = item.m_sSoc_Limit_Max;

        Destroy(this.gameObject);

        return itemscript;
    }

    // Player 퀘스트 보상 획득 시 사용.
    public Item_Use CreateItem(Item_Use item)
    {
        Item_Use itemscript = new Item_Use();

        itemscript.m_sItemName = item.m_sItemName;
        itemscript.m_sItemDescription = item.m_sItemDescription;
        itemscript.m_nItemCode = item.m_nItemCode;
        //itemscript.m_nItemNumber = item.m_nItemNumber;
        itemscript.m_nItemNumber = 0;
        itemscript.m_sp_Sprite = item.m_sp_Sprite;

        itemscript.m_eItemType = E_ITEM_TYPE.USE;
        itemscript.m_eItemGrade = item.m_eItemGrade;
        itemscript.m_eItemUseType = item.m_eItemUseType;

        itemscript.m_fDurationTime = item.m_fDurationTime;
        itemscript.m_fCoolTime = item.m_fCoolTime;
        itemscript.m_nPrice = item.m_nPrice;

        itemscript.m_Reinforcement_Effect = item.m_Reinforcement_Effect;

        itemscript.m_bDisplay_Gift_Item = item.m_bDisplay_Gift_Item;
        itemscript.m_eItemUseGiftType = item.m_eItemUseGiftType;
        itemscript.m_nRandomBox_PickCount_Min = item.m_nRandomBox_PickCount_Min;
        itemscript.m_nRandomBox_PickCount_Max = item.m_nRandomBox_PickCount_Max;
        itemscript.m_nDictionary_Gift_Item_Equip_Code = item.m_nDictionary_Gift_Item_Equip_Code;
        itemscript.m_nDictionary_Gift_Item_Equip_Count = item.m_nDictionary_Gift_Item_Equip_Count;
        itemscript.m_nDictionary_Gift_Item_Equip_Probability = item.m_nDictionary_Gift_Item_Equip_Probability;
        itemscript.m_nDictionary_Gift_Item_Use_Code = item.m_nDictionary_Gift_Item_Use_Code;
        itemscript.m_nDictionary_Gift_Item_Use_Count = item.m_nDictionary_Gift_Item_Use_Count;
        itemscript.m_nDictionary_Gift_Item_Use_Probability = item.m_nDictionary_Gift_Item_Use_Probability;
        itemscript.m_nDictionary_Gift_Item_Etc_Code = item.m_nDictionary_Gift_Item_Etc_Code;
        itemscript.m_nDictionary_Gift_Item_Etc_Count = item.m_nDictionary_Gift_Item_Etc_Count;
        itemscript.m_nDictionary_Gift_Item_Etc_Probability = item.m_nDictionary_Gift_Item_Etc_Probability;

        itemscript.m_sStatus_Effect = item.m_sStatus_Effect;
        itemscript.m_sStatus_Limit_Min = item.m_sStatus_Limit_Min;
        itemscript.m_sStatus_Limit_Max = item.m_sStatus_Limit_Max;
        itemscript.m_sSoc_Effect = item.m_sSoc_Effect;
        itemscript.m_sSoc_Limit_Min = item.m_sSoc_Limit_Min;
        itemscript.m_sSoc_Limit_Max = item.m_sSoc_Limit_Max;

        return itemscript;
    }

    // 불러오기.
    public Item_Use LoadItem(int itemcode)
    {
        Item_Use item = ItemManager.instance.m_Dictionary_MonsterDrop_Use[itemcode];
        Item_Use itemscript = new Item_Use();

        itemscript.m_sItemName = item.m_sItemName;
        itemscript.m_sItemDescription = item.m_sItemDescription;
        itemscript.m_nItemCode = item.m_nItemCode;
        //itemscript.m_nItemNumber = item.m_nItemNumber;
        itemscript.m_nItemNumber = 0;
        itemscript.m_sp_Sprite = item.m_sp_Sprite;

        itemscript.m_eItemType = E_ITEM_TYPE.USE;
        itemscript.m_eItemGrade = item.m_eItemGrade;
        itemscript.m_eItemUseType = item.m_eItemUseType;

        itemscript.m_fDurationTime = item.m_fDurationTime;
        itemscript.m_fCoolTime = item.m_fCoolTime;
        itemscript.m_nPrice = item.m_nPrice;

        itemscript.m_Reinforcement_Effect = item.m_Reinforcement_Effect;

        itemscript.m_bDisplay_Gift_Item = item.m_bDisplay_Gift_Item;
        itemscript.m_eItemUseGiftType = item.m_eItemUseGiftType;
        itemscript.m_nRandomBox_PickCount_Min = item.m_nRandomBox_PickCount_Min;
        itemscript.m_nRandomBox_PickCount_Max = item.m_nRandomBox_PickCount_Max;
        itemscript.m_nDictionary_Gift_Item_Equip_Code = item.m_nDictionary_Gift_Item_Equip_Code;
        itemscript.m_nDictionary_Gift_Item_Equip_Count = item.m_nDictionary_Gift_Item_Equip_Count;
        itemscript.m_nDictionary_Gift_Item_Equip_Probability = item.m_nDictionary_Gift_Item_Equip_Probability;
        itemscript.m_nDictionary_Gift_Item_Use_Code = item.m_nDictionary_Gift_Item_Use_Code;
        itemscript.m_nDictionary_Gift_Item_Use_Count = item.m_nDictionary_Gift_Item_Use_Count;
        itemscript.m_nDictionary_Gift_Item_Use_Probability = item.m_nDictionary_Gift_Item_Use_Probability;
        itemscript.m_nDictionary_Gift_Item_Etc_Code = item.m_nDictionary_Gift_Item_Etc_Code;
        itemscript.m_nDictionary_Gift_Item_Etc_Count = item.m_nDictionary_Gift_Item_Etc_Count;
        itemscript.m_nDictionary_Gift_Item_Etc_Probability = item.m_nDictionary_Gift_Item_Etc_Probability;

        itemscript.m_sStatus_Effect = item.m_sStatus_Effect;
        itemscript.m_sStatus_Limit_Min = item.m_sStatus_Limit_Min;
        itemscript.m_sStatus_Limit_Max = item.m_sStatus_Limit_Max;
        itemscript.m_sSoc_Effect = item.m_sSoc_Effect;
        itemscript.m_sSoc_Limit_Min = item.m_sSoc_Limit_Min;
        itemscript.m_sSoc_Limit_Max = item.m_sSoc_Limit_Max;

        return itemscript;

    }

        // 소비아이템(기프트) 관련 설정 함수
    public void Set_Item_Use_Gift(E_ITEM_USE_GIFT_TYPE eiugt, int pickcount_min = 0, int pickcount_max = 0) // eiugt : 소비아이템(기프트) 타입
    {
        m_eItemUseGiftType = eiugt;
        m_nRandomBox_PickCount_Min = pickcount_min;
        m_nRandomBox_PickCount_Max = pickcount_max;
    }
    // 소비아이템(기프트) 목록 추가 함수
    public void Add_Gift_Item_Equip(int code, int count, int probability = 10000) // code : 추가할 장비아이템 고유코드, count : 아이템 획득 개수, probability : 아이템 획득 확률
    {
        m_nDictionary_Gift_Item_Equip_Code.Add(m_nDictionary_Gift_Item_Equip_Probability.Count, code);
        m_nDictionary_Gift_Item_Equip_Count.Add(m_nDictionary_Gift_Item_Equip_Probability.Count, count);
        m_nDictionary_Gift_Item_Equip_Probability.Add(m_nDictionary_Gift_Item_Equip_Probability.Count, probability);
    }
    public void Add_Gift_Item_Use(int code, int count, int probability = 10000) // code : 추가할 소비아이템 고유코드, count : 아이템 획득 개수, probability : 아이템 획득 확률
    {
        m_nDictionary_Gift_Item_Use_Code.Add(m_nDictionary_Gift_Item_Use_Probability.Count, code);
        m_nDictionary_Gift_Item_Use_Count.Add(m_nDictionary_Gift_Item_Use_Probability.Count, count);
        m_nDictionary_Gift_Item_Use_Probability.Add(m_nDictionary_Gift_Item_Use_Probability.Count, probability);
    }
    public void Add_Gift_Item_Etc(int code, int count, int probability = 10000) // code : 추가할 기타아이템 고유코드, count : 아이템 획득 개수, probability : 아이템 획득 확률
    {
        m_nDictionary_Gift_Item_Etc_Code.Add(m_nDictionary_Gift_Item_Etc_Probability.Count, code);
        m_nDictionary_Gift_Item_Etc_Count.Add(m_nDictionary_Gift_Item_Etc_Probability.Count, count);
        m_nDictionary_Gift_Item_Etc_Probability.Add(m_nDictionary_Gift_Item_Etc_Probability.Count, probability);
    }

    // 소비아이템(기프트) 사용 정보(아이템 목록, 아이템 획득 개수, 아이템 획득 확률) 반환 함수
    public string Return_Gift_List()
    {
        string strtoken = "";
        // 1. 소비아이템(기프트(아이템 확정 지급형))
        if (m_eItemUseGiftType == E_ITEM_USE_GIFT_TYPE.FIXEDBOX)
        {
            strtoken += "[사용 시 아래 아이템을 모두 획득합니다.]\n";
            for (int i = 0; i < m_nDictionary_Gift_Item_Equip_Code.Count; i++)
            {
                strtoken += ItemManager.instance.m_Dictionary_MonsterDrop_Equip[m_nDictionary_Gift_Item_Equip_Code[i]].m_sItemName + " " + m_nDictionary_Gift_Item_Equip_Count[i] + " 개\n";

            }
            for (int i = 0; i < m_nDictionary_Gift_Item_Use_Code.Count; i++)
            {
                strtoken += ItemManager.instance.m_Dictionary_MonsterDrop_Use[m_nDictionary_Gift_Item_Use_Code[i]].m_sItemName + " " + m_nDictionary_Gift_Item_Use_Count[i] + " 개\n";
            }
            for (int i = 0; i < m_nDictionary_Gift_Item_Etc_Code.Count; i++)
            {
                strtoken += ItemManager.instance.m_Dictionary_MonsterDrop_Etc[m_nDictionary_Gift_Item_Etc_Code[i]].m_sItemName + " " + m_nDictionary_Gift_Item_Etc_Count[i] + " 개\n";

            }

            return strtoken;
        }
        // 2. 소비아이템(기프트(랜덤 지급형 _ A 타입))
        else if (m_eItemUseGiftType == E_ITEM_USE_GIFT_TYPE.RANDOMBOX_INDEPENDENTTRIAL)
        {
            strtoken += "[사용 시 아래 아이템을 획득합니다.]\n";
            strtoken += "[획득할 수 있는 아이템 개수: " + m_nRandomBox_PickCount_Min.ToString() + " ~ " + m_nRandomBox_PickCount_Max.ToString() + " 개.]\n";
            strtoken += "[아이템 중복 획득이 가능합니다.]\n";
            for (int i = 0; i < m_nDictionary_Gift_Item_Equip_Code.Count; i++)
            {
                strtoken += ItemManager.instance.m_Dictionary_MonsterDrop_Equip[m_nDictionary_Gift_Item_Equip_Code[i]].m_sItemName + " " + m_nDictionary_Gift_Item_Equip_Count[i] + " 개 " + Mathf.Round(((float)(m_nDictionary_Gift_Item_Equip_Probability[i] / (float)10000) * 100)) + " %\n";
            }
            for (int i = 0; i < m_nDictionary_Gift_Item_Use_Code.Count; i++)
            {
                strtoken += ItemManager.instance.m_Dictionary_MonsterDrop_Use[m_nDictionary_Gift_Item_Use_Code[i]].m_sItemName + " " + m_nDictionary_Gift_Item_Use_Count[i] + " 개 " + Mathf.Round(((float)(m_nDictionary_Gift_Item_Use_Probability[i] / (float)10000) * 100)) + " %\n";
            }
            for (int i = 0; i < m_nDictionary_Gift_Item_Etc_Code.Count; i++)
            {
                strtoken += ItemManager.instance.m_Dictionary_MonsterDrop_Etc[m_nDictionary_Gift_Item_Etc_Code[i]].m_sItemName + " " + m_nDictionary_Gift_Item_Etc_Count[i] + " 개 " + Mathf.Round(((float)(m_nDictionary_Gift_Item_Etc_Probability[i] / (float)10000) * 100)) + " %\n";
            }

            return strtoken;
        }
        // 3. 소비아이템(기프트(랜덤 지급형 _ B 타입))
        else if (m_eItemUseGiftType == E_ITEM_USE_GIFT_TYPE.RANDOMBOX_DEPENDENTTRIAL)
        {
            strtoken += "[사용 시 아래 아이템을 획득합니다.]\n";
            strtoken += "[획득할 수 있는 아이템 개수: " + m_nRandomBox_PickCount_Min.ToString() + " ~ " + m_nRandomBox_PickCount_Max.ToString() + " 개.]\n";
            strtoken += "[아이템 중복 획득이 불가능합니다.]\n";
            for (int i = 0; i < m_nDictionary_Gift_Item_Equip_Code.Count; i++)
            {
                strtoken += ItemManager.instance.m_Dictionary_MonsterDrop_Equip[m_nDictionary_Gift_Item_Equip_Code[i]].m_sItemName + " " + m_nDictionary_Gift_Item_Equip_Count[i] + " 개 " + Mathf.Round(((float)(m_nDictionary_Gift_Item_Equip_Probability[i] / (float)10000) * 100)) + " %\n";
            }
            for (int i = 0; i < m_nDictionary_Gift_Item_Use_Code.Count; i++)
            {
                strtoken += ItemManager.instance.m_Dictionary_MonsterDrop_Use[m_nDictionary_Gift_Item_Use_Code[i]].m_sItemName + " " + m_nDictionary_Gift_Item_Use_Count[i] + " 개 " + Mathf.Round(((float)(m_nDictionary_Gift_Item_Use_Probability[i] / (float)10000) * 100)) + " %\n";
            }
            for (int i = 0; i < m_nDictionary_Gift_Item_Etc_Code.Count; i++)
            {
                strtoken += ItemManager.instance.m_Dictionary_MonsterDrop_Etc[m_nDictionary_Gift_Item_Etc_Code[i]].m_sItemName + " " + m_nDictionary_Gift_Item_Etc_Count[i] + " 개 " + Mathf.Round(((float)(m_nDictionary_Gift_Item_Etc_Probability[i] / (float)10000) * 100)) + " %\n";
            }

            return strtoken;
        }
        else
            return "";
    }
}
