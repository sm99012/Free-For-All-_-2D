using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Itemslot : MonoBehaviour
{
    // 인벤토리
    public static Item_Equip[] m_gary_Itemslot_Equip; // 장비아이템 데이터
    public static int[] m_nary_Itemslot_Equip_Count; // 장비아이템 개수. 장비아이템의 경우 인벤토리 한칸당 1개만 
    // 장비 아이템 귀속 여부 판단에 이용되는 변수
    // ㄴ 퀵슬롯 등록시 판매행위 불가.
    // ㄴ 퀵슬롯 등록시 퀘스트 클리어 불가능.
    public static bool[] m_bary_Itemslot_Equip_Belong;
    public static Item_Use[] m_gary_Itemslot_Use;
    public static int[] m_nary_Itemslot_Use_Count;
    public static Item_Etc[] m_gary_Itemslot_Etc;
    public static int[] m_nary_Itemslot_Etc_Count;
    // 아이템 보유 최대치
    int m_nMaxCount = 10;

    // 보유 재화.
    public int m_nGold;


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
            m_nary_Itemslot_Etc_Count[i] = 0;
        }

        m_nGold = 0;
    }

    // return 0 ~ 59: 아이템을 획득하여 저장된 배열 넘버.
    // return -1: 아이템 획득을 하지 못함.
    public int Get_Item_Equip(Item_Equip item)
    {
        for (int i = 0; i < 60; i++)
        {
            if (m_nary_Itemslot_Equip_Count[i] == 0)
            {
                m_gary_Itemslot_Equip[i] = item;//GetCloneItem(item);
                m_nary_Itemslot_Equip_Count[i] = 1;

                GUIManager_Total.Instance.Update_Quickslot();
                
                return i;
            }
        }

        GUIManager_Total.Instance.Update_Quickslot();

        return -1;
    }
    public int Get_Item_Use(Item_Use item)
    {
        bool Have = false;
        int arynum_have = -1;
        int arynum_null = -1;
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
        else
        {
            m_nary_Itemslot_Use_Count[arynum_have] += 1;

            GUIManager_Total.Instance.Update_Quickslot();

            return arynum_have;
        }
    }
    public int Get_Item_Etc(Item_Etc item)
    {
        bool Have = false;
        int arynum_have = -1;
        int arynum_null = -1;
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
        else
        {
            m_nary_Itemslot_Etc_Count[arynum_have] += 1;

            GUIManager_Total.Instance.Update_Quickslot();

            return arynum_have;
        }
    }

    public void Get_Gold(int gold)
    {
        m_nGold += gold;
    }

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

    public void GetQuestReward_Item_Use(Quest quest)
    {
        for (int i = 0; i < quest.m_lRewardList_Item_Use.Count; i++)
        {
            Item_Use item = quest.m_lRewardList_Item_Use[i].CreateItem(quest.m_lRewardList_Item_Use[i]);
            Destroy(item);
            Get_Item_Use(item);
        }
    }

    public void GetQuestReward_Item_Etc(Quest quest)
    {
        for (int i = 0; i < quest.m_lRewardList_Item_Etc.Count; i++)
        {
            Item_Etc item = quest.m_lRewardList_Item_Etc[i].CreateItem(quest.m_lRewardList_Item_Etc[i]);
            Destroy(item);
            Get_Item_Etc(item);
        }
    }

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

    // 아이템 퀵슬롯 등록 관련.
    // 퀵슬롯에 등록된 아이템의 총 수량 및 사용.
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
    // 퀵슬롯에 등록된 아이템 사용.
    public void Use_Quickslot_Item(E_QUICKSLOT_CATEGORY eqc, int itemcode)
    {
        if (eqc == E_QUICKSLOT_CATEGORY.EQUIP)
        {
            // 사용한다고 사라지는게 아님.
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
            // 사용 불가.
        }
    }

    // 리트라이 시 지불할 골드.
    public void ReTry_Pay_Gold(int gold)
    {
        m_nGold -= gold;
    }
    // 리트라이 시 사라지는 골드.
    public bool ReTry_Lost_Gold(int gold)
    {


        return true;
    }
    // 리트라이 시 사라지는 아이템.
    public bool ReTry_Lost_Item_Equip(Dictionary<int, int> dictionary)
    {
        foreach(KeyValuePair<int, int> item in dictionary)
        {
            Delete_Item_Equip(item.Key);
            Player_Total.Instance.m_pq_Quest.QuestUpdate_Collect(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[item.Key]);
        }

        return true;
    }
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
    public bool ReTry_Lost_Item_Use(Dictionary<int, int> dictionary)
    {
        foreach (KeyValuePair<int, int> item in dictionary)
        {
            Delete_Item_Use(item.Key, item.Value);
            Player_Total.Instance.m_pq_Quest.QuestUpdate_Collect(ItemManager.instance.m_Dictionary_MonsterDrop_Use[item.Key]);
        }

        return true;
    }
    void Delete_Item_Use(int itemcode, int itemcount)
    {
        int ic = itemcount;
        //for (int i = 0; i < m_gary_Itemslot_Use.Length;)
        //{
        //    if (m_nary_Itemslot_Use_Count[i] > 0)
        //    {
        //        if (m_gary_Itemslot_Use[i].m_nItemCode == itemcode)
        //        {
        //            itemcount -= 1;
        //            m_nary_Itemslot_Use_Count[i] -= 1;

        //            if (m_nary_Itemslot_Use_Count[i] < 1)
        //                i++;

        //            if (itemcount < 1)
        //            {
        //                break;
        //            }
        //        }
        //        else
        //            i++;
        //    }
        //    else
        //        i++;
        //}
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
    public bool ReTry_Lost_Item_Etc(Dictionary<int, int> dictionary)
    {
        foreach (KeyValuePair<int, int> item in dictionary)
        {
            Delete_Item_Etc(item.Key, item.Value);
            Player_Total.Instance.m_pq_Quest.QuestUpdate_Collect(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[item.Key]);
        }

        return true;
    }
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
    public int ReTry_Lost_Gold(int min = 1, int max = 10)
    {
        int lostrate = Random.Range(min, max + 1);
        int lostgold = (int)((float)m_nGold * ((float)lostrate / (float)100));

        m_nGold -= lostgold;

        return lostgold;
    }

    // 기프트 사용 관련 함수들.
    // 장비 아이템 슬롯의 잔여 슬롯 확인.
    // itemcount 만큼의 장비 아이템을 획득할 여분의 장비 아이템 슬롯이 남아 있는지 판단.
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
    // 소비 아이템 슬롯의 잔여 슬롯 확인.
    // itemcount 만큼의 소비 아이템을 획득할 여분의 소비 아이템 슬롯이 남아 있는지 판단.
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
    // 기타 아이템 슬롯의 잔여 슬롯 확인.
    // itemcount 만큼의 기타 아이템을 획득할 여분의 기타 아이템 슬롯이 남아 있는지 판단.
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
