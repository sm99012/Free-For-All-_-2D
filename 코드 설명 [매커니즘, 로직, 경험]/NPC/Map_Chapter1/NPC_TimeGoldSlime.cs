using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_TimeGoldSlime : NPC_Total
{
    private void Awake()
    {
        m_sNPCName = "[주식 회사 더슬라]\n골드 타임 슬라임";
        m_nNPCCode = 11;

        m_ql_QuestList_KILL_MONSTER = new List<Quest_KILL_MONSTER>();
        m_ql_QuestList_KILL_TYPE = new List<Quest_KILL_TYPE>();
        m_ql_QuestList_GOAWAY_MONSTER = new List<Quest_GOAWAY_MONSTER>();
        m_ql_QuestList_GOAWAY_TYPE = new List<Quest_GOAWAY_TYPE>();
        m_ql_QuestList_COLLECT = new List<Quest_COLLECT>();
        m_ql_QuestList_CONVERSATION = new List<Quest_CONVERSATION>();
        m_ql_QuestList_ROLL = new List<Quest_ROLL>();
        m_ql_QuestList_ELIMINATE_MONSTER = new List<Quest_ELIMINATE_MONSTER>();
        m_ql_QuestList_ELIMINATE_TYPE = new List<Quest_ELIMINATE_TYPE>();

        m_cl_Conversation = new List<Conversation>();

        m_Sprite_NPC = this.GetComponent<SpriteRenderer>().sprite;

        if (NPCManager_Total.m_Dictionary_NPC.ContainsKey(m_nNPCCode) == false)
        {
            NPCManager_Total.m_Dictionary_NPC.Add(m_nNPCCode, this);
        }
        else
        {
            NPCManager_Total.m_Dictionary_NPC.Remove(m_nNPCCode);
            NPCManager_Total.m_Dictionary_NPC.Add(m_nNPCCode, this);
        }
        InitialSet();
        InitialSet_Store();
    }

    override public void InitialSet()
    {
        InitialSet_Icon();

        m_cl_Conversation.Add(ConversationManager.Instance.m_Dictionary_ConversationList[15]);
        m_cl_Conversation.Add(ConversationManager.Instance.m_Dictionary_ConversationList[16]);

        m_ql_QuestList_COLLECT.Add(QuestManager.Instance.GetQuest_COLLECT(4003));
        m_ql_QuestList_COLLECT.Add(QuestManager.Instance.GetQuest_COLLECT(4004));
        m_ql_QuestList_KILL_MONSTER.Add(QuestManager.Instance.GetQuest_KILL_MONSTER(0003));
        m_ql_QuestList_KILL_MONSTER.Add(QuestManager.Instance.GetQuest_KILL_MONSTER(0004));
        m_ql_QuestList_COLLECT.Add(QuestManager.Instance.GetQuest_COLLECT(4007));
        m_ql_QuestList_COLLECT.Add(QuestManager.Instance.GetQuest_COLLECT(4014));
        m_ql_QuestList_CONVERSATION.Add(QuestManager.Instance.GetQuest_CONVERSATION(5013));
        m_ql_QuestList_CONVERSATION.Add(QuestManager.Instance.GetQuest_CONVERSATION(5015));
    }

    override public void InitialSet_Store()
    {
        m_bNPC_Store = true;

        m_List_NPC_Store = new List<NPC_Store>();
        m_List_NPC_Store_Code = new List<int>();

        NPC_Store store;

        store = new NPC_Store("[[주식회사 더 슬라 드넓은 초원 지부]]", 11001, m_Sprite_NPC, E_STORE_LEVEL.S4);
        if (m_List_NPC_Store_Code.Contains(store.m_nStore_Code) == false)
        {
            store.m_sDescription = "어서오십시오. '주식회사 더 슬라' 드넓은 초원의 지부장 '골드 타임 슬라임' 이라고 합니다.\n고객님이 원하시는 모든것을 판매합니다.\n지부장인 저는 싸구려는 취급하지 않습니다.ㅎ";
            store.m_fBuy_Item_Equip_Value = 0.80f;
            store.m_fBuy_Item_Use_Value = 0.80f;
            store.m_fBuy_Item_Etc_Value = 0.80f;
            // Item_Equip
            {
                store.m_List_Sale_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1001]);
                store.m_List_Sale_Item_Equip_Probability.Add(7500);
                store.m_List_Sale_Item_Equip_Count_Min.Add(3);
                store.m_List_Sale_Item_Equip_Count_Max.Add(1);
                store.m_List_Sale_Item_Equip_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1001].m_nPrice - 10);
                store.m_List_Sale_Item_Equip_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1001].m_nPrice + 10);

                store.m_List_Sale_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1004]);
                store.m_List_Sale_Item_Equip_Probability.Add(7500);
                store.m_List_Sale_Item_Equip_Count_Min.Add(3);
                store.m_List_Sale_Item_Equip_Count_Max.Add(1);
                store.m_List_Sale_Item_Equip_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1004].m_nPrice - 10);
                store.m_List_Sale_Item_Equip_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1004].m_nPrice + 10);

                store.m_List_Sale_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1301]);
                store.m_List_Sale_Item_Equip_Probability.Add(7500);
                store.m_List_Sale_Item_Equip_Count_Min.Add(3);
                store.m_List_Sale_Item_Equip_Count_Max.Add(1);
                store.m_List_Sale_Item_Equip_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1301].m_nPrice - 10);
                store.m_List_Sale_Item_Equip_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1301].m_nPrice + 10);

                store.m_List_Sale_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1304]);
                store.m_List_Sale_Item_Equip_Probability.Add(7500);
                store.m_List_Sale_Item_Equip_Count_Min.Add(3);
                store.m_List_Sale_Item_Equip_Count_Max.Add(1);
                store.m_List_Sale_Item_Equip_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1304].m_nPrice - 10);
                store.m_List_Sale_Item_Equip_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1304].m_nPrice + 10);

                store.m_List_Sale_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1601]);
                store.m_List_Sale_Item_Equip_Probability.Add(7500);
                store.m_List_Sale_Item_Equip_Count_Min.Add(3);
                store.m_List_Sale_Item_Equip_Count_Max.Add(1);
                store.m_List_Sale_Item_Equip_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1601].m_nPrice - 10);
                store.m_List_Sale_Item_Equip_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1601].m_nPrice + 10);

                store.m_List_Sale_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1602]);
                store.m_List_Sale_Item_Equip_Probability.Add(7500);
                store.m_List_Sale_Item_Equip_Count_Min.Add(3);
                store.m_List_Sale_Item_Equip_Count_Max.Add(1);
                store.m_List_Sale_Item_Equip_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1602].m_nPrice - 10);
                store.m_List_Sale_Item_Equip_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1602].m_nPrice + 10);
            }
            // Item_Use
            {
                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8000]);
                store.m_List_Sale_Item_Use_Probability.Add(10000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8000].m_nPrice - 5);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8000].m_nPrice);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8000]);
                store.m_List_Buy_Item_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8000].m_nPrice - 10);
                store.m_List_Buy_Item_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8000].m_nPrice - 5);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8001]);
                store.m_List_Sale_Item_Use_Probability.Add(10000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8001].m_nPrice - 5);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8001].m_nPrice);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8001]);
                store.m_List_Buy_Item_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8001].m_nPrice - 10);
                store.m_List_Buy_Item_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8001].m_nPrice - 5);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8002]);
                store.m_List_Sale_Item_Use_Probability.Add(10000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8002].m_nPrice - 5);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8002].m_nPrice);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8002]);
                store.m_List_Buy_Item_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8002].m_nPrice - 10);
                store.m_List_Buy_Item_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8002].m_nPrice - 5);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8003]);
                store.m_List_Sale_Item_Use_Probability.Add(10000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(7);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8003].m_nPrice - 20);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8003].m_nPrice);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8003]);
                store.m_List_Buy_Item_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8003].m_nPrice - 40);
                store.m_List_Buy_Item_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8003].m_nPrice - 20);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8004]);
                store.m_List_Sale_Item_Use_Probability.Add(10000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(7);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8004].m_nPrice - 20);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8004].m_nPrice);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8004]);
                store.m_List_Buy_Item_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8004].m_nPrice - 40);
                store.m_List_Buy_Item_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8004].m_nPrice - 20);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8005]);
                store.m_List_Sale_Item_Use_Probability.Add(10000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(7);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8005].m_nPrice - 20);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8005].m_nPrice);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8005]);
                store.m_List_Buy_Item_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8005].m_nPrice - 40);
                store.m_List_Buy_Item_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8005].m_nPrice - 20);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12600]);
                store.m_List_Sale_Item_Use_Probability.Add(8000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(3);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12600].m_nPrice);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12600].m_nPrice + 500);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12600]);
                store.m_List_Buy_Item_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12600].m_nPrice - 100);
                store.m_List_Buy_Item_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12600].m_nPrice - 50);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12601]);
                store.m_List_Sale_Item_Use_Probability.Add(8000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(3);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12601].m_nPrice);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12601].m_nPrice + 1250);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12601]);
                store.m_List_Buy_Item_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12601].m_nPrice - 500);
                store.m_List_Buy_Item_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12601].m_nPrice - 250);
            }
            // Item_Etc
            {
                store.m_List_Sale_Item_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0002]);
                store.m_List_Sale_Item_Etc_Probability.Add(300);
                store.m_List_Sale_Item_Etc_Count_Min.Add(1);
                store.m_List_Sale_Item_Etc_Count_Max.Add(3);
                store.m_List_Sale_Item_Etc_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0002].m_nPrice + 1);
                store.m_List_Sale_Item_Etc_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0002].m_nPrice + 75);

                store.m_List_Sale_Item_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0003]);
                store.m_List_Sale_Item_Etc_Probability.Add(300);
                store.m_List_Sale_Item_Etc_Count_Min.Add(1);
                store.m_List_Sale_Item_Etc_Count_Max.Add(3);
                store.m_List_Sale_Item_Etc_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0003].m_nPrice + 1);
                store.m_List_Sale_Item_Etc_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0003].m_nPrice + 70);

                store.m_List_Sale_Item_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0006]);
                store.m_List_Sale_Item_Etc_Probability.Add(10000);
                store.m_List_Sale_Item_Etc_Count_Min.Add(1);
                store.m_List_Sale_Item_Etc_Count_Max.Add(1);
                store.m_List_Sale_Item_Etc_Price_Min.Add(15000);
                store.m_List_Sale_Item_Etc_Price_Max.Add(15000);
            }

            m_List_NPC_Store.Add(store);
            m_List_NPC_Store_Code.Add(store.m_nStore_Code);
        }

        store = new NPC_Store("[[특별한 거래]]", 11002, m_Sprite_NPC, E_STORE_LEVEL.S4);
        if (m_List_NPC_Store_Code.Contains(store.m_nStore_Code) == false)
        {
            store.m_sDescription = "저희 '주식회사 더 슬라' 의 VIP 고객님을 위해 노력하겠습니다.";
            store.m_ql_Quest_Necessity_Clear.Add(QuestManager.Instance.GetQuest_KILL_MONSTER(0003));
            store.m_fBuy_Item_Equip_Value = 0.80f;
            store.m_fBuy_Item_Use_Value = 0.80f;
            store.m_fBuy_Item_Etc_Value = 0.80f;
            // Item_Use
            {
                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12600]);
                store.m_List_Sale_Item_Use_Probability.Add(10000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12600].m_nPrice - 100);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12600].m_nPrice);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12600]);
                store.m_List_Buy_Item_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12600].m_nPrice - 100);
                store.m_List_Buy_Item_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12600].m_nPrice - 50);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12601]);
                store.m_List_Sale_Item_Use_Probability.Add(10000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12601].m_nPrice - 500);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12601].m_nPrice);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12601]);
                store.m_List_Buy_Item_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12601].m_nPrice - 500);
                store.m_List_Buy_Item_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12601].m_nPrice - 250);
            }
            // Item_Etc
            {
                store.m_List_Sale_Item_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0002]);
                store.m_List_Sale_Item_Etc_Probability.Add(5000);
                store.m_List_Sale_Item_Etc_Count_Min.Add(1);
                store.m_List_Sale_Item_Etc_Count_Max.Add(3);
                store.m_List_Sale_Item_Etc_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0002].m_nPrice + 5);
                store.m_List_Sale_Item_Etc_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0002].m_nPrice + 20);

                store.m_List_Sale_Item_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0003]);
                store.m_List_Sale_Item_Etc_Probability.Add(5000);
                store.m_List_Sale_Item_Etc_Count_Min.Add(1);
                store.m_List_Sale_Item_Etc_Count_Max.Add(3);
                store.m_List_Sale_Item_Etc_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0003].m_nPrice + 5);
                store.m_List_Sale_Item_Etc_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0003].m_nPrice + 20);

                store.m_List_Sale_Item_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0005]);
                store.m_List_Sale_Item_Etc_Probability.Add(5000);
                store.m_List_Sale_Item_Etc_Count_Min.Add(1);
                store.m_List_Sale_Item_Etc_Count_Max.Add(3);
                store.m_List_Sale_Item_Etc_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0005].m_nPrice);
                store.m_List_Sale_Item_Etc_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0005].m_nPrice);

                store.m_List_Sale_Item_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0006]);
                store.m_List_Sale_Item_Etc_Probability.Add(10000);
                store.m_List_Sale_Item_Etc_Count_Min.Add(1);
                store.m_List_Sale_Item_Etc_Count_Max.Add(1);
                store.m_List_Sale_Item_Etc_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0006].m_nPrice);
                store.m_List_Sale_Item_Etc_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0006].m_nPrice);

                store.m_List_Sale_Item_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0007]);
                store.m_List_Sale_Item_Etc_Probability.Add(1000);
                store.m_List_Sale_Item_Etc_Count_Min.Add(1);
                store.m_List_Sale_Item_Etc_Count_Max.Add(3);
                store.m_List_Sale_Item_Etc_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0007].m_nPrice + 1000);
                store.m_List_Sale_Item_Etc_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0007].m_nPrice + 2000);
            }

            m_List_NPC_Store.Add(store);
            m_List_NPC_Store_Code.Add(store.m_nStore_Code);
        }

        store = new NPC_Store("[[특별한 거래]]", 11003, m_Sprite_NPC, E_STORE_LEVEL.S4);
        if (m_List_NPC_Store_Code.Contains(store.m_nStore_Code) == false)
        {
            store.m_sDescription = "저희 '주식회사 더 슬라' 의 VIP 고객님을 위해 노력하겠습니다.";
            store.m_ql_Quest_Necessity_Clear.Add(QuestManager.Instance.GetQuest_KILL_MONSTER(0004));
            // Item_Use
            {
                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12600]);
                store.m_List_Sale_Item_Use_Probability.Add(10000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12600].m_nPrice - 100);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12600].m_nPrice);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12600]);
                store.m_List_Buy_Item_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12600].m_nPrice - 100);
                store.m_List_Buy_Item_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12600].m_nPrice - 50);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12601]);
                store.m_List_Sale_Item_Use_Probability.Add(10000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12601].m_nPrice - 500);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12601].m_nPrice);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12601]);
                store.m_List_Buy_Item_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12601].m_nPrice - 500);
                store.m_List_Buy_Item_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12601].m_nPrice - 250);
            }
            // Item_Etc
            {
                store.m_List_Sale_Item_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0002]);
                store.m_List_Sale_Item_Etc_Probability.Add(5000);
                store.m_List_Sale_Item_Etc_Count_Min.Add(1);
                store.m_List_Sale_Item_Etc_Count_Max.Add(3);
                store.m_List_Sale_Item_Etc_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0002].m_nPrice + 5);
                store.m_List_Sale_Item_Etc_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0002].m_nPrice + 20);

                store.m_List_Sale_Item_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0003]);
                store.m_List_Sale_Item_Etc_Probability.Add(5000);
                store.m_List_Sale_Item_Etc_Count_Min.Add(1);
                store.m_List_Sale_Item_Etc_Count_Max.Add(3);
                store.m_List_Sale_Item_Etc_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0003].m_nPrice + 5);
                store.m_List_Sale_Item_Etc_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0003].m_nPrice + 20);

                store.m_List_Sale_Item_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0005]);
                store.m_List_Sale_Item_Etc_Probability.Add(5000);
                store.m_List_Sale_Item_Etc_Count_Min.Add(1);
                store.m_List_Sale_Item_Etc_Count_Max.Add(3);
                store.m_List_Sale_Item_Etc_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0005].m_nPrice);
                store.m_List_Sale_Item_Etc_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0005].m_nPrice);

                store.m_List_Sale_Item_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0006]);
                store.m_List_Sale_Item_Etc_Probability.Add(10000);
                store.m_List_Sale_Item_Etc_Count_Min.Add(1);
                store.m_List_Sale_Item_Etc_Count_Max.Add(1);
                store.m_List_Sale_Item_Etc_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0006].m_nPrice);
                store.m_List_Sale_Item_Etc_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0006].m_nPrice);

                store.m_List_Sale_Item_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0007]);
                store.m_List_Sale_Item_Etc_Probability.Add(1000);
                store.m_List_Sale_Item_Etc_Count_Min.Add(1);
                store.m_List_Sale_Item_Etc_Count_Max.Add(3);
                store.m_List_Sale_Item_Etc_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0007].m_nPrice + 1000);
                store.m_List_Sale_Item_Etc_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0007].m_nPrice + 2000);
            }

            m_List_NPC_Store.Add(store);
            m_List_NPC_Store_Code.Add(store.m_nStore_Code);
        }

        Update_Store();
    }
}
