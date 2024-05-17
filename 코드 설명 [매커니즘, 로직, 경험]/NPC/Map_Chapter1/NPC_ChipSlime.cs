using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_ChipSlime : NPC_Total
{
    private void Awake()
    {
        m_sNPCName = "[앤트 앤 칩스]\n칩스을라임";
        m_nNPCCode = 25;

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

        store = new NPC_Store("[[앤트 앤 칩스 가공식품점]]", 25001, m_Sprite_NPC, E_STORE_LEVEL.S2);
        if (m_List_NPC_Store_Code.Contains(store.m_nStore_Code) == false)
        {
            store.m_sDescription = "어서와요^^ 행복을 드리는 '앤트 앤 칩스' 가공식품점입니다^^\n저희는 품질 좋은 가공식품을 판매합니다^^ 소세지, 베이컨, 젬 등등 없는게 없다구요^^\n신선식품의 경우 바로 옆의 '앤트 앤 칩스' 신선식품점 을 이용해 주세요^^";
            store.m_fBuy_Item_Equip_Value = 0.1f;
            store.m_fBuy_Item_Use_Value = 0.9f;
            store.m_fBuy_Item_Etc_Value = 0.1f;

            // Item_Use
            {
                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8014]);
                store.m_List_Sale_Item_Use_Probability.Add(7500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8014].m_nPrice + 1);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8014].m_nPrice + 7);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8014]);
                store.m_List_Buy_Item_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8014].m_nPrice - 7);
                store.m_List_Buy_Item_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8014].m_nPrice);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8028]);
                store.m_List_Sale_Item_Use_Probability.Add(7500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8028].m_nPrice + 1);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8028].m_nPrice + 12);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8028]);
                store.m_List_Buy_Item_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8028].m_nPrice - 12);
                store.m_List_Buy_Item_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8028].m_nPrice);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8017]);
                store.m_List_Sale_Item_Use_Probability.Add(3300);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(5);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8017].m_nPrice + 10);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8017].m_nPrice + 31);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8017]);
                store.m_List_Buy_Item_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8017].m_nPrice - 31);
                store.m_List_Buy_Item_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8017].m_nPrice + 9);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8018]);
                store.m_List_Sale_Item_Use_Probability.Add(7500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(20);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8018].m_nPrice + 1);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8018].m_nPrice + 7);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8018]);
                store.m_List_Buy_Item_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8018].m_nPrice - 7);
                store.m_List_Buy_Item_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8018].m_nPrice);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8020]);
                store.m_List_Sale_Item_Use_Probability.Add(7500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(20);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8020].m_nPrice + 1);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8020].m_nPrice + 3);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8020]);
                store.m_List_Buy_Item_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8020].m_nPrice - 3);
                store.m_List_Buy_Item_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8020].m_nPrice);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8026]);
                store.m_List_Sale_Item_Use_Probability.Add(7500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8026].m_nPrice + 1);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8026].m_nPrice + 8);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8026]);
                store.m_List_Buy_Item_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8026].m_nPrice - 8);
                store.m_List_Buy_Item_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8026].m_nPrice);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8027]);
                store.m_List_Sale_Item_Use_Probability.Add(7500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8027].m_nPrice + 1);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8027].m_nPrice + 6);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8027]);
                store.m_List_Buy_Item_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8027].m_nPrice - 6);
                store.m_List_Buy_Item_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8027].m_nPrice);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9001]);
                store.m_List_Sale_Item_Use_Probability.Add(7500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(5);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9001].m_nPrice + 1);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9001].m_nPrice + 30);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9001]);
                store.m_List_Buy_Item_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9001].m_nPrice - 30);
                store.m_List_Buy_Item_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9001].m_nPrice);
            }


            m_List_NPC_Store.Add(store);
            m_List_NPC_Store_Code.Add(store.m_nStore_Code);
        }

        Update_Store();
    }
}
