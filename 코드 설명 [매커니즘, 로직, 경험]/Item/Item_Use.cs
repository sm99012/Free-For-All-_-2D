﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum E_ITEM_USE_TYPE { RECOVERPOTION, TEMPORARYBUFFPOTION, ETERNALBUFFPOTION, REINFORCEMENT, GIFT }
public enum E_ITEM_USE_GIFT_TYPE { NULL, FIXEDBOX, RANDOMBOX_INDEPENDENTTRIAL, RANDOMBOX_DEPENDENTTRIAL, FUSION }

public class Item_Use : Item
{
    public E_ITEM_USE_TYPE m_eItemUseType;

    // 소비 아이템 쿨타임
    public float m_fCoolTime;

    // 버프포션 지속시간.
    public float m_fDurationTime;

    // 강화서.
    public Reinforcement m_Reinforcement_Effect;

    // 기프트 타입
    public E_ITEM_USE_GIFT_TYPE m_eItemUseGiftType;
    // 기프트 타입의 아이템에 포함된 아이템 품목 정보를 출력하는지 안하는지.
    public bool m_bDisplay_Gift_Item;
    // 기프트 타입이 랜덤박스일 경우 뽑을수 있는 최대 수량.
    public int m_nRandomBox_PickCount_Max;
    public int m_nRandomBox_PickCount_Min;
    // 보상 소비아이템(선물)
    public Dictionary<int, int> m_nDictionary_Gift_Item_Equip_Code;
    public Dictionary<int, int> m_nDictionary_Gift_Item_Equip_Count;
    public Dictionary<int, int> m_nDictionary_Gift_Item_Equip_Probability;
    // 보상 소비아이템(선물)
    public Dictionary<int, int> m_nDictionary_Gift_Item_Use_Code;
    public Dictionary<int, int> m_nDictionary_Gift_Item_Use_Count;
    public Dictionary<int, int> m_nDictionary_Gift_Item_Use_Probability;
    // 보상 소비아이템(선물)
    public Dictionary<int, int> m_nDictionary_Gift_Item_Etc_Code;
    public Dictionary<int, int> m_nDictionary_Gift_Item_Etc_Count;
    public Dictionary<int, int> m_nDictionary_Gift_Item_Etc_Probability;

    public Item_Use() { }
    // Item 원본.
    public Item_Use(string name, int code, string path_sprite, E_ITEM_USE_TYPE iut, E_ITEM_GRADE ig,
        float durationtime, float cooltime, int price)
    {
        this.m_sItemName = name;
        this.m_nItemCode = code;
        this.m_sp_Sprite = Resources.Load<Sprite>(path_sprite);
        
        this.m_eItemtype = ItemType.USE;
        this.m_eItemGrade = ig;
        this.m_eItemUseType = iut;

        this.m_sStatus_Effect = new STATUS(0);
        this.m_sStatus_Limit_Min = new STATUS(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        this.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        this.m_sSoc_Effect = new SOC(0);
        this.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        this.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);

        if (iut == E_ITEM_USE_TYPE.GIFT)
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

    // 기프트 타입 지정.
    public void Set_Item_Use_Gift(E_ITEM_USE_GIFT_TYPE eiugt, int pickcount_min = 0, int pickcount_max = 0)
    {
        m_eItemUseGiftType = eiugt;
        m_nRandomBox_PickCount_Min = pickcount_min;
        m_nRandomBox_PickCount_Max = pickcount_max;
    }
    // 기프트 품목 추가.
    public void Add_Gift_Item_Equip(int code, int count, int probability = 10000)
    {
        m_nDictionary_Gift_Item_Equip_Code.Add(m_nDictionary_Gift_Item_Equip_Probability.Count, code);
        m_nDictionary_Gift_Item_Equip_Count.Add(m_nDictionary_Gift_Item_Equip_Probability.Count, count);
        m_nDictionary_Gift_Item_Equip_Probability.Add(m_nDictionary_Gift_Item_Equip_Probability.Count, probability);
    }
    public void Add_Gift_Item_Use(int code, int count, int probability = 10000)
    {
        m_nDictionary_Gift_Item_Use_Code.Add(m_nDictionary_Gift_Item_Use_Probability.Count, code);
        m_nDictionary_Gift_Item_Use_Count.Add(m_nDictionary_Gift_Item_Use_Probability.Count, count);
        m_nDictionary_Gift_Item_Use_Probability.Add(m_nDictionary_Gift_Item_Use_Probability.Count, probability);
    }
    public void Add_Gift_Item_Etc(int code, int count, int probability = 10000)
    {
        m_nDictionary_Gift_Item_Etc_Code.Add(m_nDictionary_Gift_Item_Etc_Probability.Count, code);
        m_nDictionary_Gift_Item_Etc_Count.Add(m_nDictionary_Gift_Item_Etc_Probability.Count, count);
        m_nDictionary_Gift_Item_Etc_Probability.Add(m_nDictionary_Gift_Item_Etc_Probability.Count, probability);
    }

    // 아이템 정보 UI 용 기프트 품목명, 수량, 확률 반환.
    public string Return_Gift_List()
    {
        string strtoken = "";
        if (m_eItemUseGiftType == E_ITEM_USE_GIFT_TYPE.FIXEDBOX)
        {
            strtoken += "[사용 시 아래 아이템을 모두 획득합니다.]\n";
            for (int i = 0; i < m_nDictionary_Gift_Item_Equip_Code.Count; i++)
            {
                strtoken += ItemManager.instance.m_Dictionary_MonsterDrop_Equip[m_nDictionary_Gift_Item_Equip_Code[i]].m_sItemName + " " + m_nDictionary_Gift_Item_Equip_Count[i] + " 개\n";
                //strtoken += m_nDictionary_Gift_Item_Equip_Code[i] + " " + m_nDictionary_Gift_Item_Equip_Count[i] + " 개\n";

            }
            for (int i = 0; i < m_nDictionary_Gift_Item_Use_Code.Count; i++)
            {
                strtoken += ItemManager.instance.m_Dictionary_MonsterDrop_Use[m_nDictionary_Gift_Item_Use_Code[i]].m_sItemName + " " + m_nDictionary_Gift_Item_Use_Count[i] + " 개\n";
                //strtoken += m_nDictionary_Gift_Item_Use_Code[i] + " " + m_nDictionary_Gift_Item_Use_Count[i] + " 개\n";
            }
            for (int i = 0; i < m_nDictionary_Gift_Item_Etc_Code.Count; i++)
            {
                strtoken += ItemManager.instance.m_Dictionary_MonsterDrop_Etc[m_nDictionary_Gift_Item_Etc_Code[i]].m_sItemName + " " + m_nDictionary_Gift_Item_Etc_Count[i] + " 개\n";
                //strtoken += m_nDictionary_Gift_Item_Etc_Code[i] + " " + m_nDictionary_Gift_Item_Etc_Count[i] + " 개\n";

            }

            return strtoken;
        }
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

        itemscript.m_eItemtype = ItemType.USE;
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

        itemscript.m_eItemtype = ItemType.USE;
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

        itemscript.m_eItemtype = ItemType.USE;
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

        itemscript.m_eItemtype = ItemType.USE;
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
}
