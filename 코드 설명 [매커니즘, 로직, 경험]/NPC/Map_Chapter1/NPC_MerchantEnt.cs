using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_MerchantEnt : NPC_Total
{
    private void Awake()
    {
        m_sNPCName = "[앤트 앤 칩스]\n주렁주렁 앤트";
        m_nNPCCode = 24;

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
            //InitialSet_Store();
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

        //m_cl_Conversation.Add(ConversationManager.Instance.m_Dictionary_ConversationList[29]);

        //m_ql_QuestList_CONVERSATION.Add(QuestManager.Instance.GetQuest_CONVERSATION(5009));
        //m_ql_QuestList_COLLECT.Add(QuestManager.Instance.GetQuest_COLLECT(4008));

        //m_ql_QuestList_CONVERSATION.Add(QuestManager.Instance.GetQuest_CONVERSATION(5010));
        //m_ql_QuestList_COLLECT.Add(QuestManager.Instance.GetQuest_COLLECT(4009));
        //m_ql_QuestList_COLLECT.Add(QuestManager.Instance.GetQuest_COLLECT(4010));
        //m_ql_QuestList_KILL_MONSTER.Add(QuestManager.Instance.GetQuest_KILL_MONSTER(0007));
    }

    override public void InitialSet_Store()
    {
        m_bNPC_Store = true;

        m_List_NPC_Store = new List<NPC_Store>();
        m_List_NPC_Store_Code = new List<int>();

        NPC_Store store;

        store = new NPC_Store("[[앤트 앤 칩스 신선식품점]]", 24001, m_Sprite_NPC, E_STORE_LEVEL.S2);
        if (m_List_NPC_Store_Code.Contains(store.m_nStore_Code) == false)
        {
            store.m_sDescription = "어서와요^^ 행복을 드리는 '앤트 앤 칩스' 신선식품점입니다^^\n저희는 품질 좋은 신선한 식재료를 판매합니다^^ 아 물론 수입산도 있답니다^^\n가공식품의 경우 바로 옆의 '앤트 앤 칩스' 가공식품점 을 이용해 주세요^^";
            store.m_fBuy_Item_Equip_Value = 0.1f;
            store.m_fBuy_Item_Use_Value = 0.9f;
            store.m_fBuy_Item_Etc_Value = 0.1f;

            // Item_Use
            {
                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8009]);
                store.m_List_Sale_Item_Use_Probability.Add(7500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8009].m_nPrice + 1);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8009].m_nPrice + 4);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8009]);
                store.m_List_Buy_Item_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8009].m_nPrice - 4);
                store.m_List_Buy_Item_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8009].m_nPrice);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8010]);
                store.m_List_Sale_Item_Use_Probability.Add(7500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(5);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8010].m_nPrice + 10);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8010].m_nPrice + 30);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8010]);
                store.m_List_Buy_Item_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8010].m_nPrice - 30);
                store.m_List_Buy_Item_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8010].m_nPrice + 9);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8012]);
                store.m_List_Sale_Item_Use_Probability.Add(7500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8012].m_nPrice + 1);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8012].m_nPrice + 6);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8012]);
                store.m_List_Buy_Item_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8012].m_nPrice - 6);
                store.m_List_Buy_Item_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8012].m_nPrice);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8015]);
                store.m_List_Sale_Item_Use_Probability.Add(7500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8015].m_nPrice + 1);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8015].m_nPrice + 2);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8015]);
                store.m_List_Buy_Item_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8015].m_nPrice - 2);
                store.m_List_Buy_Item_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8015].m_nPrice);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8019]);
                store.m_List_Sale_Item_Use_Probability.Add(7500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8019].m_nPrice + 1);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8019].m_nPrice + 2);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8019]);
                store.m_List_Buy_Item_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8019].m_nPrice - 2);
                store.m_List_Buy_Item_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8019].m_nPrice);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8021]);
                store.m_List_Sale_Item_Use_Probability.Add(7500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(5);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8021].m_nPrice + 1);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8021].m_nPrice + 5);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8021]);
                store.m_List_Buy_Item_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8021].m_nPrice - 5);
                store.m_List_Buy_Item_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8021].m_nPrice);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8022]);
                store.m_List_Sale_Item_Use_Probability.Add(7500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8022].m_nPrice + 1);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8022].m_nPrice + 3);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8022]);
                store.m_List_Buy_Item_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8022].m_nPrice - 3);
                store.m_List_Buy_Item_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8022].m_nPrice);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8024]);
                store.m_List_Sale_Item_Use_Probability.Add(7500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8024].m_nPrice + 1);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8024].m_nPrice + 2);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8024]);
                store.m_List_Buy_Item_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8024].m_nPrice - 2);
                store.m_List_Buy_Item_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8024].m_nPrice);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8025]);
                store.m_List_Sale_Item_Use_Probability.Add(500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(2);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8025].m_nPrice + 50);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8025].m_nPrice + 250);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8025]);
                store.m_List_Buy_Item_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8025].m_nPrice + 50);
                store.m_List_Buy_Item_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8025].m_nPrice + 250);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9002]);
                store.m_List_Sale_Item_Use_Probability.Add(500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(2);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9002].m_nPrice + 100);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9002].m_nPrice + 1000);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9002]);
                store.m_List_Buy_Item_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9002].m_nPrice + 100);
                store.m_List_Buy_Item_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9002].m_nPrice + 1000);
            }

            m_List_NPC_Store.Add(store);
            m_List_NPC_Store_Code.Add(store.m_nStore_Code);
        }

        Update_Store();
    }
}
