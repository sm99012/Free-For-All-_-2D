using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_GrandMotherSlime : NPC_Total
{
    private void Awake()
    {
        m_sNPCName = "슬라임 할머니";
        m_nNPCCode = 20;

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
            InitialSet_Store();
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

        m_cl_Conversation.Add(ConversationManager.Instance.m_Dictionary_ConversationList[29]);
        //m_cl_Conversation.Add(ConversationManager.Instance.m_Dictionary_ConversationList[25]);
        //m_cl_Conversation.Add(ConversationManager.Instance.m_Dictionary_ConversationList[20]);

        //m_ql_QuestList_COLLECT.Add(QuestManager.Instance.GetQuest_COLLECT(4005));
        m_ql_QuestList_CONVERSATION.Add(QuestManager.Instance.GetQuest_CONVERSATION(5009));
        m_ql_QuestList_COLLECT.Add(QuestManager.Instance.GetQuest_COLLECT(4008));

        m_ql_QuestList_CONVERSATION.Add(QuestManager.Instance.GetQuest_CONVERSATION(5010));
        m_ql_QuestList_COLLECT.Add(QuestManager.Instance.GetQuest_COLLECT(4009));
        m_ql_QuestList_COLLECT.Add(QuestManager.Instance.GetQuest_COLLECT(4010));
        m_ql_QuestList_KILL_MONSTER.Add(QuestManager.Instance.GetQuest_KILL_MONSTER(0007));
    }

    override public void InitialSet_Store()
    {
        m_bNPC_Store = true;

        m_List_NPC_Store = new List<NPC_Store>();
        m_List_NPC_Store_Code = new List<int>();

        NPC_Store store;

        store = new NPC_Store("[[슬라임 할머니의 식당]]", 20001, m_Sprite_NPC, E_STORE_LEVEL.S4);
        if (m_List_NPC_Store_Code.Contains(store.m_nStore_Code) == false)
        {
            store.m_sDescription = "니 또왔나?!?! 어여 처묵고 가그라! 다른눔들도 무야되니껜!!\n메뉴는 이 할미 맘대로니까 불만 가지지 말그라?!\n내 음식값은 재료값만 받으꾸마!!";
            store.m_ql_Quest_Necessity_Clear.Add(QuestManager.Instance.GetQuest_KILL_MONSTER(0007));
            store.m_fBuy_Item_Equip_Value = 0.10f;
            store.m_fBuy_Item_Use_Value = 0.10f;
            store.m_fBuy_Item_Etc_Value = 0.10f;
            // Item_Use
            {
                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8017]);
                store.m_List_Sale_Item_Use_Probability.Add(7500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8017].m_nPrice);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8017].m_nPrice);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8018]);
                store.m_List_Sale_Item_Use_Probability.Add(7500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8018].m_nPrice);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8018].m_nPrice);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8020]);
                store.m_List_Sale_Item_Use_Probability.Add(7500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8020].m_nPrice);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8020].m_nPrice);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8023]);
                store.m_List_Sale_Item_Use_Probability.Add(7500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(20);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8023].m_nPrice);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8023].m_nPrice);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8028]);
                store.m_List_Sale_Item_Use_Probability.Add(10000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8028].m_nPrice);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8028].m_nPrice);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9003]);
                store.m_List_Sale_Item_Use_Probability.Add(1000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(3);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9003].m_nPrice);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9003].m_nPrice);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9004]);
                store.m_List_Sale_Item_Use_Probability.Add(7500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9004].m_nPrice);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9004].m_nPrice);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9005]);
                store.m_List_Sale_Item_Use_Probability.Add(7500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9005].m_nPrice);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9005].m_nPrice);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9006]);
                store.m_List_Sale_Item_Use_Probability.Add(7500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9006].m_nPrice);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9006].m_nPrice);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9007]);
                store.m_List_Sale_Item_Use_Probability.Add(1000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(3);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9007].m_nPrice);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9007].m_nPrice);
            }

            m_List_NPC_Store.Add(store);
            m_List_NPC_Store_Code.Add(store.m_nStore_Code);
        }

        Update_Store();
    }
}
