using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_BlackSmiSlime : NPC_Total
{
    private void Awake()
    {
        m_sNPCName = "[대장장이]\n블랙스미슬라임";
        m_nNPCCode = 23;

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

        store = new NPC_Store("[[블랙스미슬라임의 방어구 상점]]", 23001, m_Sprite_NPC, E_STORE_LEVEL.S2);
        if (m_List_NPC_Store_Code.Contains(store.m_nStore_Code) == false)
        {
            store.m_sDescription = "방어구 판다.\n너 사라.";
            store.m_fBuy_Item_Equip_Value = 0.9f;
            store.m_fBuy_Item_Use_Value = 0.6f;
            store.m_fBuy_Item_Etc_Value = 0.30f;
            // Item_Equip
            {
                store.m_List_Sale_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[3003]);
                store.m_List_Sale_Item_Equip_Probability.Add(10000);
                store.m_List_Sale_Item_Equip_Count_Min.Add(1);
                store.m_List_Sale_Item_Equip_Count_Max.Add(1);
                store.m_List_Sale_Item_Equip_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[3003].m_nPrice);
                store.m_List_Sale_Item_Equip_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[3003].m_nPrice);

                store.m_List_Sale_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[4006]);
                store.m_List_Sale_Item_Equip_Probability.Add(10000);
                store.m_List_Sale_Item_Equip_Count_Min.Add(1);
                store.m_List_Sale_Item_Equip_Count_Max.Add(1);
                store.m_List_Sale_Item_Equip_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[4006].m_nPrice);
                store.m_List_Sale_Item_Equip_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[4006].m_nPrice);

                store.m_List_Sale_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[5001]);
                store.m_List_Sale_Item_Equip_Probability.Add(10000);
                store.m_List_Sale_Item_Equip_Count_Min.Add(1);
                store.m_List_Sale_Item_Equip_Count_Max.Add(1);
                store.m_List_Sale_Item_Equip_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[5001].m_nPrice);
                store.m_List_Sale_Item_Equip_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[5001].m_nPrice);

                store.m_List_Sale_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[6003]);
                store.m_List_Sale_Item_Equip_Probability.Add(10000);
                store.m_List_Sale_Item_Equip_Count_Min.Add(1);
                store.m_List_Sale_Item_Equip_Count_Max.Add(1);
                store.m_List_Sale_Item_Equip_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[6003].m_nPrice);
                store.m_List_Sale_Item_Equip_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[6003].m_nPrice);

                store.m_List_Sale_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[4003]);
                store.m_List_Sale_Item_Equip_Probability.Add(5000);
                store.m_List_Sale_Item_Equip_Count_Min.Add(1);
                store.m_List_Sale_Item_Equip_Count_Max.Add(1);
                store.m_List_Sale_Item_Equip_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[4003].m_nPrice);
                store.m_List_Sale_Item_Equip_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[4003].m_nPrice + 500);

                store.m_List_Sale_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[4005]);
                store.m_List_Sale_Item_Equip_Probability.Add(10000);
                store.m_List_Sale_Item_Equip_Count_Min.Add(1);
                store.m_List_Sale_Item_Equip_Count_Max.Add(1);
                store.m_List_Sale_Item_Equip_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[4005].m_nPrice);
                store.m_List_Sale_Item_Equip_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[4005].m_nPrice + 500);

                store.m_List_Sale_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[4008]);
                store.m_List_Sale_Item_Equip_Probability.Add(10000);
                store.m_List_Sale_Item_Equip_Count_Min.Add(1);
                store.m_List_Sale_Item_Equip_Count_Max.Add(1);
                store.m_List_Sale_Item_Equip_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[4008].m_nPrice);
                store.m_List_Sale_Item_Equip_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[4008].m_nPrice + 500);
            }

            m_List_NPC_Store.Add(store);
            m_List_NPC_Store_Code.Add(store.m_nStore_Code);
        }

        Update_Store();
    }
}
