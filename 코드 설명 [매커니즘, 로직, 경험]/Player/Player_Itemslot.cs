using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Itemslot : MonoBehaviour
{
    // 인벤토리
    public static Item_Equip[] m_gary_Itemslot_Equip;  // 장비아이템 데이터. 60칸 제공
    public static int[] m_nary_Itemslot_Equip_Count;   // 장비아이템 개수. 최대 1개 아이템 소지 가능
    public static bool[] m_bary_Itemslot_Equip_Belong; // 퀵슬롯에 등록된 장비아이템의 각종 제한 판단
                                                       // 1. 해당 배열의 장비아이템을 퀵슬롯에 등록했다면 NPC와의 거래 불가
                                                       // 2. 해당 배열의 장비아이템을 퀵슬롯에 등록했다면 해당 배열의 장비아이템을 필요로 하는 퀘스트 클리어 불가(※ 단 퀵슬롯에 등록되지 않은 장비아이템이 존재하는 경우 퀘스트 클리어 가능)
    public static Item_Use[] m_gary_Itemslot_Use;      // 소비아이템 데이터. 60칸 제공
    public static int[] m_nary_Itemslot_Use_Count;     // 소비아이템 개수. 최대 10개 아이템 소지 가능
    public static Item_Etc[] m_gary_Itemslot_Etc;      // 기타아이템 데이터. 60칸 제공
    public static int[] m_nary_Itemslot_Etc_Count;     // 기타아이템 개수. 최대 10개 아이템 소지 가능

    int m_nMaxCount = 10; // 아이템 보유 최대치

    public int m_nGold; // 보유 골드(재화)


    public void InitialSet()
    {
        m_gary_Itemslot_Equip = new Item_Equip[60];
        m_nary_Itemslot_Equip_Count = new int[60];
        m_bary_Itemslot_Equip_Belong = new bool[60];
        m_gary_Itemslot_Use = new Item_Use[60];
        m_nary_Itemslot_Use_Count = new int[60];
        m_gary_Itemslot_Etc = new Item_Etc[60];
        m_nary_Itemslot_Etc_Count = new int[60];
        for (int i = 0; i < 60; i++)
        {
            m_bary_Itemslot_Equip_Belong[i] = false;
        }

        m_nGold = 0;
    }

    // 장비아이템 획득 함수
    // return 0 ~ 59 : 장비아이템을 획득하여 저장하고 저장된 배열 넘버를 반환한다.
    // return -1 : 인벤토리에 빈칸이 없어 아이템을 획득하지 못함.
    public int Get_Item_Equip(Item_Equip item)
    {
        for (int i = 0; i < 60; i++)
        {
            if (m_nary_Itemslot_Equip_Count[i] == 0)
            {
                m_gary_Itemslot_Equip[i] = item;
                m_nary_Itemslot_Equip_Count[i] = 1;

                GUIManager_Total.Instance.Update_Quickslot();
                
                return i;
            }
        }

        GUIManager_Total.Instance.Update_Quickslot();

        return -1;
    }
    // 소비아이템 획득 함수
    // return 0 ~ 59 : 소비아이템을 획득하여 저장하고 저장된 배열 넘버를 반환한다.
    // return -1 : 인벤토리에 빈칸이 없어 아이템을 획득하지 못함.
    // 소비아이템 획득 매커니즘 : 중복 아이템 획득
    // 1. 획득할 소비아이템과 동일한 소비아이템이 이미 인벤토리에 존재 하는지 판단한다.
    // 2. 아이템 보유 최대치(m_nMaxCount)보다 적게 가지고 있는지 판단한다. (Have = true / Have = false)
    // Have == true : 인벤토리에 이미 존재하는 소비아이템의 배열 넘버의 개수에 +1을 해준다.
    // Have == false : 인벤토리의 빈칸에 소비아이템을 할당한다.
    public int Get_Item_Use(Item_Use item)
    {
        bool Have = false;
        int arynum_have = -1;
        int arynum_null = -1;
        // 1. 획득할 소비아이템과 동일한 소비아이템이 이미 인벤토리에 존재 하는지 판단한다.
        for (int i = 0; i < 60; i++)
        {
            if (m_nary_Itemslot_Use_Count[i] == 0)
            {
                if (arynum_null == -1)
                {
                    arynum_null = i;
                }
            }
            else
            {
                // 2. 아이템 보유 최대치(m_nMaxCount)보다 적게 가지고 있는지 판단한다. (Have = true / false)
                if (m_gary_Itemslot_Use[i].m_sItemName == item.m_sItemName && m_nary_Itemslot_Use_Count[i] < m_nMaxCount)
                {
                    Have = true;
                    arynum_have = i;
                    break;
                }
                else
                    continue;
            }
        }
        // 소바아이템을 저장하고 저장된 배열 넘버를 반환한다.
        // Have == false : 인벤토리의 빈칸에 소비아이템을 할당한다.
        if (Have == false)
        {
            if (m_nary_Itemslot_Use_Count[arynum_null] == 0)
            {
                m_gary_Itemslot_Use[arynum_null] = item;
                m_nary_Itemslot_Use_Count[arynum_null] += 1;

                GUIManager_Total.Instance.Update_Quickslot();

                return arynum_null;
            }
            else
            {
                GUIManager_Total.Instance.Update_Quickslot();

                return -1;
            }
        }
        // Have == true : 인벤토리에 이미 존재하는 소비아이템의 배열 넘버의 개수에 +1을 해준다.
        else
        {
            m_nary_Itemslot_Use_Count[arynum_have] += 1;

            GUIManager_Total.Instance.Update_Quickslot();

            return arynum_have;
        }
    }
    // 기타아이템 획득 함수
    // return 0 ~ 59 : 기타아이템을 획득하여 저장하고 저장된 배열 넘버를 반환한다.
    // return -1 : 인벤토리에 빈칸이 없어 아이템을 획득하지 못함.
    // 기타아이템 획득 매커니즘 : 중복 아이템 획득
    // 1. 획득할 기타아이템과 동일한 기타아이템이 이미 인벤토리에 존재 하는지 판단한다.
    // 2. 아이템 보유 최대치(m_nMaxCount)보다 적게 가지고 있는지 판단한다. (Have = true / false)
    // Have == true : 인벤토리에 이미 존재하는 기타아이템의 배열 넘버의 개수에 +1을 해준다.
    // Have == false : 인벤토리의 빈칸에 기타아이템을 할당한다.
    public int Get_Item_Etc(Item_Etc item)
    {
        bool Have = false;
        int arynum_have = -1;
        int arynum_null = -1;
        // 1. 획득할 기타아이템과 동일한 기타아이템이 이미 인벤토리에 존재 하는지 판단한다.
        for (int i = 0; i < 60; i++)
        {
            if (m_nary_Itemslot_Etc_Count[i] == 0)
            {
                if (arynum_null == -1)
                {
                    arynum_null = i;
                }
            }
            else
            {
                // 2. 아이템 보유 최대치(m_nMaxCount)보다 적게 가지고 있는지 판단한다. (Have = true / false)
                if (m_gary_Itemslot_Etc[i].m_sItemName == item.m_sItemName && m_nary_Itemslot_Etc_Count[i] < m_nMaxCount)
                {
                    Have = true;
                    arynum_have = i;
                    break;
                }
                else
                    continue;
            }
        }
        // Have == false : 인벤토리의 빈칸에 기타아이템을 할당한다.
        if (Have == false)
        {
            if (m_nary_Itemslot_Etc_Count[arynum_null] == 0)
            {
                m_gary_Itemslot_Etc[arynum_null] = item;
                m_nary_Itemslot_Etc_Count[arynum_null] += 1;

                GUIManager_Total.Instance.Update_Quickslot();

                return arynum_null;
            }
            else
            {
                GUIManager_Total.Instance.Update_Quickslot();

                return -1;
            }
        }
        // Have == true : 인벤토리에 이미 존재하는 기타아이템의 배열 넘버의 개수에 +1을 해준다.
        else
        {
            m_nary_Itemslot_Etc_Count[arynum_have] += 1;

            GUIManager_Total.Instance.Update_Quickslot();

            return arynum_have;
        }
    }

    // 골드(재화) 획득 함수
    public void Get_Gold(int gold)
    {
        m_nGold += gold;
    }

    // 퀘스트 보상으로 장비아이템을 획득하는 함수
    public bool GetQuestReward_Item_Equip(Quest quest)
    {
        for (int i = 0; i < quest.m_lRewardList_Item_Equip.Count; i++)
        {
            Item_Equip item = quest.m_lRewardList_Item_Equip[i].CreateItem(quest.m_lRewardList_Item_Equip[i]);
            Destroy(item);
            Get_Item_Equip(item);

            return true;
        }

        return false;
    }
    // 퀘스트 보상으로 소비아이템을 획득하는 함수
    public void GetQuestReward_Item_Use(Quest quest)
    {
        for (int i = 0; i < quest.m_lRewardList_Item_Use.Count; i++)
        {
            Item_Use item = quest.m_lRewardList_Item_Use[i].CreateItem(quest.m_lRewardList_Item_Use[i]);
            Destroy(item);
            Get_Item_Use(item);
        }
    }
    // 퀘스트 보상으로 기타아이템을 획득하는 함수
    public void GetQuestReward_Item_Etc(Quest quest)
    {
        for (int i = 0; i < quest.m_lRewardList_Item_Etc.Count; i++)
        {
            Item_Etc item = quest.m_lRewardList_Item_Etc[i].CreateItem(quest.m_lRewardList_Item_Etc[i]);
            Destroy(item);
            Get_Item_Etc(item);
        }
    }

    // 수집 타입의 퀘스트 클리어에 필요한 아이템을 인벤토리에서 삭제하는 함수
    public void DeleteCollectItem(Quest_COLLECT quest)
    {
        if (quest.m_eQuestType == E_QUEST_TYPE.COLLECT)
        {
            for (int i = 0; i < quest.m_nl_ItemCode.Count; i++)
            {
                for (int a = 0; a < 60; a++)
                {
                    if (m_nary_Itemslot_Equip_Count[a] != 0)
                    {
                        if (m_gary_Itemslot_Equip[a].m_nItemCode == quest.m_nl_ItemCode[i])
                        {
                            // 장비아이템이 퀵슬롯에 등록된 경우 수집 타입의 퀘스트 클리어에 필요한 아이템이라 인정되지 않음.
                            if (m_bary_Itemslot_Equip_Belong[a] == false)
                            {
                                m_nary_Itemslot_Equip_Count[a] -= quest.m_nl_ItemCount_Max[i];
                                break;
                            }
                        }
                    }
                    if (m_nary_Itemslot_Use_Count[a] != 0)
                    {
                        if (m_gary_Itemslot_Use[a].m_nItemCode == quest.m_nl_ItemCode[i])
                        {
                            m_nary_Itemslot_Use_Count[a] -= quest.m_nl_ItemCount_Max[i];
                            break;
                        }
                    }
                    if (m_nary_Itemslot_Etc_Count[a] != 0 && m_nary_Itemslot_Etc_Count[a] >= quest.m_nl_ItemCount_Max[i])
                    {
                        if (m_gary_Itemslot_Etc[a].m_nItemCode == quest.m_nl_ItemCode[i])
                        {
                            m_nary_Itemslot_Etc_Count[a] -= quest.m_nl_ItemCount_Max[i];
                            break;
                        }
                    }
                }
            }

            GUIManager_Total.Instance.Update_Quickslot();
        }
    }

    // 아이템의 원본 데이터를 반환
    public GameObject GetCloneItem(GameObject item)
    {
        GameObject ReturnITem;
        if (item.GetComponent<Item>().m_eItemtype == ItemType.ETC)
        {
            ReturnITem = Resources.Load("Prefab/Item/Item_Etc/" + item.name) as GameObject;
        }
        else if (item.GetComponent<Item>().m_eItemtype == ItemType.USE)
        {
            ReturnITem = Resources.Load("Prefab/Item/Item_Use/" + item.name) as GameObject;
        }
        else
        {
            ReturnITem = Resources.Load("Prefab/Item/Item_Equip/" + item.name) as GameObject;
        }

        return ReturnITem;
    }

    // 퀵슬롯에 등록된 아이템의 수량 현황 확인
    public int Check_Quickslot_Item(E_QUICKSLOT_CATEGORY eqc, int itemcode)
    {
        int itemcount = 0;

        if (eqc == E_QUICKSLOT_CATEGORY.EQUIP)
        {
            itemcount = 1;
        }
        else if (eqc == E_QUICKSLOT_CATEGORY.USE)
        {
            for (int i = 0; i < 60; i++)
            {
                if (m_nary_Itemslot_Use_Count[i] != 0)
                {
                    if (m_gary_Itemslot_Use[i].m_nItemCode == itemcode)
                    {
                        itemcount += m_nary_Itemslot_Use_Count[i];
                    }
                }
            }
        }
        else if (eqc == E_QUICKSLOT_CATEGORY.ETC)
        {
            for (int i = 0; i < 60; i++)
            {
                if (m_nary_Itemslot_Etc_Count[i] != 0)
                {
                    if (m_gary_Itemslot_Etc[i].m_nItemCode == itemcode)
                    {
                        itemcount += m_nary_Itemslot_Etc_Count[i];
                    }
                }
            }
        }

        return itemcount;
    }
    // 퀵슬롯에 등록된 아이템을 사용하는 함수. 아이템의 수량 현황을 갱신하는 용도
    public void Use_Quickslot_Item(E_QUICKSLOT_CATEGORY eqc, int itemcode)
    {
        if (eqc == E_QUICKSLOT_CATEGORY.EQUIP)
        {
            // 인벤토리와 퀵슬롯의 장비아이템은 수량이 1로 고정되어있다.
        }
        else if (eqc == E_QUICKSLOT_CATEGORY.USE)
        {
            // 사용 시 -1.
            for (int i = 0; i < 60; i++)
            {
                if (m_nary_Itemslot_Use_Count[i] != 0)
                {
                    if (m_gary_Itemslot_Use[i].m_nItemCode == itemcode)
                    {
                        m_nary_Itemslot_Use_Count[i] -= 1;
                        if (m_nary_Itemslot_Use_Count[i] == 0)
                            m_gary_Itemslot_Use[i] = null;

                        break;
                    }
                }
            }
        }
        else if (eqc == E_QUICKSLOT_CATEGORY.ETC)
        {
            // 기타아이템은 사용이 불가능하다.
        }
    }

    // 리트라이(부활) 시 지불할 골드(재화)
    public void ReTry_Pay_Gold(int gold)
    {
        m_nGold -= gold;
    }

    // 리트라이(부활) 시 잃어버리는 장비아이템에 관한 함수
    public bool ReTry_Lost_Item_Equip(Dictionary<int, int> dictionary)
    {
        foreach(KeyValuePair<int, int> item in dictionary)
        {
            Delete_Item_Equip(item.Key);
            Player_Total.Instance.m_pq_Quest.QuestUpdate_Collect(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[item.Key]);
        }

        return true;
    }
    // 장비아이템을 삭제하는 함수. itemnumber는 아이템이 가지는 고유 코드(아이템 생성 순서)
    void Delete_Item_Equip(int itemnumber)
    {
        for (int i = 0; i < m_gary_Itemslot_Equip.Length; i++)
        {
            if (m_nary_Itemslot_Equip_Count[i] > 0)
            {
                if (m_gary_Itemslot_Equip[i].m_nItemNumber == itemnumber)
                {
                    m_nary_Itemslot_Equip_Count[i] = 0;
                    m_gary_Itemslot_Equip[i] = null;

                    break;
                }
            }
        }
    }
    // 리트라이(부활) 시 잃어버리는 소비아이템에 관한 함수
    public bool ReTry_Lost_Item_Use(Dictionary<int, int> dictionary)
    {
        foreach (KeyValuePair<int, int> item in dictionary)
        {
            Delete_Item_Use(item.Key, item.Value);
            Player_Total.Instance.m_pq_Quest.QuestUpdate_Collect(ItemManager.instance.m_Dictionary_MonsterDrop_Use[item.Key]);
        }

        return true;
    }
    // 소비아이템을 삭제하는 함수. itemcode가 동일한 소비아이템을 Itemcount 만큼 삭제한다.
    void Delete_Item_Use(int itemcode, int itemcount)
    {
        int ic = itemcount;
        
        for (int i = m_gary_Itemslot_Use.Length - 1; i >= 0;)
        {
            if (m_nary_Itemslot_Use_Count[i] > 0)
            {
                if (m_gary_Itemslot_Use[i].m_nItemCode == itemcode)
                {
                    itemcount -= 1;
                    m_nary_Itemslot_Use_Count[i] -= 1;

                    if (m_nary_Itemslot_Use_Count[i] < 1)
                    {
                        m_gary_Itemslot_Use[i] = null;
                        i--;
                    }

                    if (itemcount < 1)
                    {
                        break;
                    }
                }
                else
                    i--;
            }
            else
                i--;
        }
    }
    // 리트라이(부활) 시 잃어버리는 기타아이템에 관한 함수
    public bool ReTry_Lost_Item_Etc(Dictionary<int, int> dictionary)
    {
        foreach (KeyValuePair<int, int> item in dictionary)
        {
            Delete_Item_Etc(item.Key, item.Value);
            Player_Total.Instance.m_pq_Quest.QuestUpdate_Collect(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[item.Key]);
        }

        return true;
    }
    // 기타아이템을 삭제하는 함수. itemcode가 동일한 기타아이템을 Itemcount 만큼 삭제한다.
    void Delete_Item_Etc(int itemcode, int itemcount)
    {
        int ic = itemcount;
        for (int i = m_gary_Itemslot_Etc.Length - 1; i >= 0;)
        {
            if (m_nary_Itemslot_Etc_Count[i] > 0)
            {
                if (m_gary_Itemslot_Etc[i].m_nItemCode == itemcode)
                {
                    itemcount -= 1;
                    m_nary_Itemslot_Etc_Count[i] -= 1;

                    if (m_nary_Itemslot_Etc_Count[i] < 1)
                    {
                        m_gary_Itemslot_Etc[i] = null;
                        i--;
                    }

                    if (itemcount < 1)
                    {
                        break;
                    }
                }
                else
                    i--;
            }
            else
                i--;
        }
    }

    // 리트라이(부활) 시 잃어버리는 골드(재화). 리트라이 단계에 따라 잃어버리는 골드(재화)의 비율이 결정된다. 
    public int ReTry_Lost_Gold(int min = 1, int max = 10)
    {
        int lostrate = Random.Range(min, max + 1);
        int lostgold = (int)((float)m_nGold * ((float)lostrate / (float)100));

        m_nGold -= lostgold;

        return lostgold;
    }

    // 소비아이템_기프트(랜덤박스, 선물상자 등) 사용 관련 함수
    // 인벤토리(장비아이템)의 남은(여유) 공간 확인. itemcount 만큼의 장비아이템을 획득할 여분의 인벤토리 공간이 남아 있는지 판단
    public bool Check_Get_Item_Itemslot_Equip(int itemcount)
    {
        int value = 0;
        for (int i = 0; i < m_nary_Itemslot_Equip_Count.Length; i++)
        {
            if (m_nary_Itemslot_Equip_Count[i] == 0)
                value += 1;
        }

        if (value >= itemcount)
            return true;
        else
            return false;
    }
    // 인벤토리(소비아이템)의 남은(여유) 공간 확인. itemcount 만큼의 소비아이템을 획득할 여분의 인벤토리 공간이 남아 있는지 판단
    public bool Check_Get_Item_Itemslot_Use(int itemcount)
    {
        int value = 0;
        for (int i = 0; i < m_nary_Itemslot_Use_Count.Length; i++)
        {
            if (m_nary_Itemslot_Use_Count[i] == 0)
                value += 1;
        }

        if (value >= itemcount)
            return true;
        else
            return false;
    }
    public bool Check_Get_Item_Itemslot_Use(Dictionary<int, int> dictionary_itemcode, Dictionary<int, int> dictionary_itemcount)
    {
        List<int> List_Exit = new List<int>();

        for (int i = 0; i < dictionary_itemcode.Count; i++)
        {
            int value = dictionary_itemcount[i];
            
            for (int j = 0; j < m_gary_Itemslot_Use.Length; j++)
            {
                if (m_nary_Itemslot_Use_Count[j] != 0)
                {
                    if (m_gary_Itemslot_Use[j].m_nItemCode == dictionary_itemcode[i])
                    {
                        value -= (10 - m_nary_Itemslot_Use_Count[j]);
                    }
                }
                else
                {
                    if (List_Exit.Contains(j) == false)
                    {
                        value -= 10;
                        List_Exit.Add(j);
                    }
                }

                if (value <= 0)
                    break;
            }

            if (value > 0)
                return false;
        }

        return true;
    }
    // 인벤토리(기타아이템)의 남은(여유) 공간 확인. itemcount 만큼의 기타아이템을 획득할 여분의 인벤토리 공간이 남아 있는지 판단
    public bool Check_Get_Item_Itemslot_Etc(int itemcount)
    {
        int value = 0;
        for (int i = 0; i < m_nary_Itemslot_Etc_Count.Length; i++)
        {
            if (m_nary_Itemslot_Etc_Count[i] == 0)
                value += 1;
        }

        if (value >= itemcount)
            return true;
        else
            return false;
    }
    public bool Check_Get_Item_Itemslot_Etc(Dictionary<int, int> dictionary_itemcode, Dictionary<int, int> dictionary_itemcount)
    {
        List<int> List_Exit = new List<int>();

        for (int i = 0; i < dictionary_itemcode.Count; i++)
        {
            int value = dictionary_itemcount[i];

            for (int j = 0; j < m_gary_Itemslot_Etc.Length; j++)
            {
                if (m_nary_Itemslot_Etc_Count[j] != 0)
                {
                    if (m_gary_Itemslot_Etc[j].m_nItemCode == dictionary_itemcode[i])
                    {
                        value -= (10 - m_nary_Itemslot_Etc_Count[j]);
                    }
                }
                else
                {
                    if (List_Exit.Contains(j) == false)
                    {
                        value -= 10;
                        List_Exit.Add(j);
                    }
                }

                if (value <= 0)
                    break;
            }

            if (value > 0)
                return false;
        }

        return true;
    }
}
