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

    //
    // ※ Item(Item_Equip, Item_Use, Item_Etc)라는 클래스를 만들어 인벤토리를 구현했다. 그 과정에서 Unity의 null 기능을 사용할 수 없었기에(Item클래스가 할당되어 있어도 Unity엔진이 null로 인식) 추가로 int 배열을 이용했다.
    //

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
    // return 0 ~ 59 : 장비아이템을 획득하여 저장하고 저장된 배열 넘버를 반환한다. / return -1 : 인벤토리에 빈칸이 없어 아이템을 획득하지 못함
    public int Get_Item_Equip(Item_Equip item) // item : 획득할 장비아이템
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
    // return 0 ~ 59 : 소비아이템을 획득하여 저장하고 저장된 배열 넘버를 반환한다. / return -1 : 인벤토리에 빈칸이 없어 아이템을 획득하지 못함
    // 소비아이템 획득 매커니즘 : 중복 아이템 획득
    // 1. 획득할 소비아이템과 동일한 소비아이템이 이미 인벤토리에 존재 하는지 판단한다.
    // 2. 아이템 보유 최대치(m_nMaxCount)보다 적게 가지고 있는지 판단한다. (Have = true / Have = false)
    // Have == true : 인벤토리에 이미 존재하는 소비아이템의 배열 넘버의 개수에 +1을 해준다.
    // Have == false : 인벤토리의 빈칸에 소비아이템을 할당한다.
    public int Get_Item_Use(Item_Use item) // item : 획득할 소비아이템
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
    // return 0 ~ 59 : 기타아이템을 획득하여 저장하고 저장된 배열 넘버를 반환한다. / return -1 : 인벤토리에 빈칸이 없어 아이템을 획득하지 못함
    // 기타아이템 획득 매커니즘 : 중복 아이템 획득
    // 1. 획득할 기타아이템과 동일한 기타아이템이 이미 인벤토리에 존재 하는지 판단한다.
    // 2. 아이템 보유 최대치(m_nMaxCount)보다 적게 가지고 있는지 판단한다. (Have = true / false)
    // Have == true : 인벤토리에 이미 존재하는 기타아이템의 배열 넘버의 개수에 +1을 해준다.
    // Have == false : 인벤토리의 빈칸에 기타아이템을 할당한다.
    public int Get_Item_Etc(Item_Etc item) // item : 획득할 기타아이템
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

    // 아이템의 원본 데이터를 반환. Unity의 프리팹 기능 사용. 오브젝트 형태로 불러온다.
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
    public int Check_Quickslot_Item(E_QUICKSLOT_CATEGORY eqc, int itemcode) // eqc : 퀵슬롯에 등록된 아이템 분류(장비아이템, 소비아이템, 기타아이템), itemcode : 퀵슬롯에 등록된 아이템 코드
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
    // 퀵슬롯에 등록된 아이템의 수량 현황을 갱신하는 용도
    public void Use_Quickslot_Item(E_QUICKSLOT_CATEGORY eqc, int itemcode) // eqc : 퀵슬롯에 등록된 아이템 분류(장비아이템, 소비아이템, 기타아이템), itemcode : 퀵슬롯에 등록된 아이템 코드
    {
        if (eqc == E_QUICKSLOT_CATEGORY.EQUIP)
        {
            // 인벤토리와 퀵슬롯의 장비아이템은 수량이 1로 고정되어있다.
        }
        else if (eqc == E_QUICKSLOT_CATEGORY.USE)
        {
            // 사용 시 -1
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

    // 리트라이(부활) 시 골드(재화) 지불
    public void ReTry_Pay_Gold(int gold)
    {
        m_nGold -= gold;
    }

    // 리트라이(부활) 시 장비아이템 소실에 관한 함수
    public bool ReTry_Lost_Item_Equip(Dictionary<int, int> Dictionary) // Dictionary : 소실되는 장비아이템 목록. Dictionary <Key : 아이템 고유 코드(아이템 생성 순서), Value : 아이템 코드>
    {
        foreach(KeyValuePair<int, int> item in Dictionary)
        {
            Delete_Item_Equip(item.Key); // 장비아이템을 삭제하는 함수
            Player_Total.Instance.m_pq_Quest.QuestUpdate_Collect(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[item.Key]); // 진행중인 퀘스트 현황을 업데이트하는 함수(퀘스트 타입 : 수집)
        }

        return true;
    }
    // 장비아이템을 삭제하는 함수
    // 장비아이템 삭제 매커니즘 : 매개변수 Dictionary의 Key값(아이템 고유 코드(아이템 생성 순서))에 해당하는 아이템을 인벤토리(장비아이템)에서 삭제한다.
    void Delete_Item_Equip(int itemnumber) // itemnumber : 아이템 고유 코드(아이템 생성 순서)
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
    // 리트라이(부활) 시 소비아이템 소실에 관한 함수
    public bool ReTry_Lost_Item_Use(Dictionary<int, int> Dictionary) // Dictionary : 소실되는 소비아이템 목록. Dictionary <Key : 아이템 코드, Value : 아이템 개수>
    {
        foreach (KeyValuePair<int, int> item in Dictionary)
        {
            Delete_Item_Use(item.Key, item.Value); // 소비아이템을 삭제하는 함수
            Player_Total.Instance.m_pq_Quest.QuestUpdate_Collect(ItemManager.instance.m_Dictionary_MonsterDrop_Use[item.Key]); // 진행중인 퀘스트 현황을 업데이트하는 함수(퀘스트 타입 : 수집)
        }

        return true;
    }
    // 소비아이템을 삭제하는 함수
    // 소비아이템 삭제 매커니즘 : 인벤토리(소비아이템)에서 itemcode가 동일한 소비아이템을 Itemcount 만큼 삭제한다.
    void Delete_Item_Use(int itemcode, int itemcount) // itemcode : 아이템 코드, itemcount : 아이템 개수
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
    // 리트라이(부활) 시 기타아이템 소실에 관한 함수
    public bool ReTry_Lost_Item_Etc(Dictionary<int, int> Dictionary) // Dictionary : 소실되는 기타아이템 목록. Dictionary <Key : 아이템 코드, Value : 아이템 개수>
    {
        foreach (KeyValuePair<int, int> item in Dictionary)
        {
            Delete_Item_Etc(item.Key, item.Value);
            Player_Total.Instance.m_pq_Quest.QuestUpdate_Collect(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[item.Key]);
        }

        return true;
    }
    // 기타아이템을 삭제하는 함수
    // 기타아이템 삭제 매커니즘 : 인벤토리(기타아이템)에서 itemcode가 동일한 기타아이템을 Itemcount 만큼 삭제한다.
    void Delete_Item_Etc(int itemcode, int itemcount) // itemcode : 아이템 코드, itemcount : 아이템 개수
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

    // 리트라이(부활) 시 골드(재화) 소실에 관한 함수. 리트라이 단계에 따라 소실되는 골드(재화)의 비율이 결정된다. 
    public int ReTry_Lost_Gold(int min = 1, int max = 10)
    {
        int lostrate = Random.Range(min, max + 1);
        int lostgold = (int)((float)m_nGold * ((float)lostrate / (float)100));

        m_nGold -= lostgold;

        return lostgold;
    }

    // 소비아이템_기프트(랜덤박스, 선물상자 등) 사용 관련 함수
    // 인벤토리(장비아이템)의 남은(여유) 공간 확인. itemcount 만큼의 장비아이템을 획득할 여분의 인벤토리 공간이 남아 있는지 판단
    // return true : 인벤토리(장비아이템) 여유 공간 존재 / return false : 인벤토리(장비아이템) 여유 공간 미존재
    public bool Check_Get_Item_Itemslot_Equip(int itemcount) // itemcount : 아이템 개수
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
    // 아이템 중복 여부를 따지지 않은 단순 판단
    // return true : 인벤토리(소비아이템) 여유 공간 존재 / return false : 인벤토리(소비아이템) 여유 공간 미존재
    public bool Check_Get_Item_Itemslot_Use(int itemcount) // itemcount : 아이템 개수
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
    // 아이템 중복 여부를 따지는 복잡한 판단
    // return true : 인벤토리(소비아이템) 여유 공간 존재 / return false : 인벤토리(소비아이템) 여유 공간 미존재
    public bool Check_Get_Item_Itemslot_Use(Dictionary<int, int> Dictionary_itemcode, Dictionary<int, int> Dictionary_itemcount) // Dictionary_itemcode <Key : 순서, Value : 아이템 코드>
                                                                                                                                 // Dictionary_itemcount <Key : 순서, Value : 아이템 개수>
    {
        List<int> List_Exit = new List<int>(); // 추가로 저장될 인벤토리(소비아이템) 배열 번호 리스트

        for (int i = 0; i < Dictionary_itemcode.Count; i++)
        {
            int value = Dictionary_itemcount[i]; // value = 소비아이템 종류(아이템 코드)별 획득할 소비아이템 개수
            
            for (int j = 0; j < m_gary_Itemslot_Use.Length; j++)
            {
                if (m_nary_Itemslot_Use_Count[j] != 0) // 해당 인벤토리(소비아이템) 배열[j]에 아이템이 존재할 경우
                {
                    if (m_gary_Itemslot_Use[j].m_nItemCode == Dictionary_itemcode[i]) // 해당 인벤토리(소비아이템) 배열[j]의 소비아이템이 새로 획득할 소비아이템과 동일할 경우
                    {
                        value -= (10 - m_nary_Itemslot_Use_Count[j]); // 인벤토리(소비아이템) 1개의 공간 당 10개의 소비아이템을 저장할 수 있기에 10을 기준으로 계산
                                                                      // value 만큼의 인벤토리(소비아이템) 내 여유 공간이 필요
                    }
                }
                else // 해당 인벤토리(소비아이템) 배열[j]에 아이템이 존재하지 않을 경우
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

            // value(획득할 소비아이템 개수) 가 0보다 클 경우 인벤토리(소비아이템)에 여유가 없다고 판단
            if (value > 0)
                return false;
        }

        return true;
    }
    // 아이템 중복 여부를 따지지 않은 단순 판단
    // return true : 인벤토리(기타아이템) 여유 공간 존재 / return false : 인벤토리(기타아이템) 여유 공간 미존재
    public bool Check_Get_Item_Itemslot_Etc(int itemcount) // itemcount : 아이템 개수
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
    // 아이템 중복 여부를 따지는 복잡한 판단
    // return true : 인벤토리(기타아이템) 여유 공간 존재 / return false : 인벤토리(기타아이템) 여유 공간 미존재
    public bool Check_Get_Item_Itemslot_Etc(Dictionary<int, int> Dictionary_itemcode, Dictionary<int, int> Dictionary_itemcount) // Dictionary_itemcode <Key : 순서, Value : 아이템 코드>
                                                                                                                                 // Dictionary_itemcount <Key : 순서, Value : 아이템 개수>
    {
        List<int> List_Exit = new List<int>(); // 추가로 저장될 인벤토리(기타아이템) 배열 번호 리스트

        for (int i = 0; i < Dictionary_itemcode.Count; i++)
        {
            int value = Dictionary_itemcount[i]; // value = 기타아이템 종류(아이템 코드)별 획득할 기타아이템 개수

            for (int j = 0; j < m_gary_Itemslot_Etc.Length; j++)
            {
                if (m_nary_Itemslot_Etc_Count[j] != 0) // 해당 인벤토리(기타아이템) 배열[j]에 아이템이 존재할 경우
                {
                    if (m_gary_Itemslot_Etc[j].m_nItemCode == Dictionary_itemcode[i]) // 해당 인벤토리(기타아이템) 배열[j]의 기타아이템이 새로 획득할 기타아이템과 동일할 경우
                    {
                        value -= (10 - m_nary_Itemslot_Etc_Count[j]); // 인벤토리(기타아이템) 1개의 공간 당 10개의 기타아이템을 저장할 수 있기에 10을 기준으로 계산
                                                                      // value 만큼의 인벤토리(기타아이템) 내 여유 공간이 필요
                    }
                }
                else // 해당 인벤토리(기타아이템) 배열[j]에 아이템이 존재하지 않을 경우
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

            // value(획득할 기타아이템 개수) 가 0보다 클 경우 인벤토리(기타아이템)에 여유가 없다고 판단
            if (value > 0)
                return false;
        }

        return true;
    }
}
