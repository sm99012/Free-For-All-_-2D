using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_OldWeakSlime : NPC_Total
{
    private void Awake()
    {
        m_sNPCName = "늙고 병든 슬라임";
        m_nNPCCode = 1;

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

        m_ql_QuestList_KILL_MONSTER.Add(QuestManager.Instance.GetQuest_KILL_MONSTER(0000));
        m_ql_QuestList_GOAWAY_MONSTER.Add(QuestManager.Instance.GetQuest_GOAWAY_MONSTER(2000));
        m_ql_QuestList_COLLECT.Add(QuestManager.Instance.GetQuest_COLLECT(4000));
        m_ql_QuestList_ROLL.Add(QuestManager.Instance.GetQuest_ROLL(6000));
        //m_ql_QuestList_ELIMINATE_MONSTER.Add(QuestManager.Instance.GetQuest_ELIMINATE_MONSTER(7000));
        m_ql_QuestList_CONVERSATION.Add(QuestManager.Instance.GetQuest_CONVERSATION(5005));
        m_ql_QuestList_CONVERSATION.Add(QuestManager.Instance.GetQuest_CONVERSATION(5009));
        m_ql_QuestList_COLLECT.Add(QuestManager.Instance.GetQuest_COLLECT(4008));

        m_cl_Conversation.Add(ConversationManager.Instance.m_Dictionary_ConversationList[1]);
        m_cl_Conversation.Add(ConversationManager.Instance.m_Dictionary_ConversationList[3]);
        m_cl_Conversation.Add(ConversationManager.Instance.m_Dictionary_ConversationList[7]);
    }

    override public void InitialSet_Store()
    {
        m_bNPC_Store = true;

        m_List_NPC_Store = new List<NPC_Store>();
        m_List_NPC_Store_Code = new List<int>();

        NPC_Store store;

        store = new NPC_Store("['늙고 병든 슬라임' 과의 거래]", 1001, m_Sprite_NPC, E_STORE_LEVEL.S1);
        if (m_List_NPC_Store_Code.Contains(store.m_nStore_Code) == false)
        {
            store.m_sDescription = "'깊디깊은숲' 에선 따스한 햇살 말고는 부족한게 없다네.";
            store.m_fBuy_Item_Equip_Value = 0.50f;
            store.m_fBuy_Item_Use_Value = 0.50f;
            store.m_fBuy_Item_Etc_Value = 0.50f;
            // Item_Equip
            {
                store.m_List_Sale_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1001]);
                store.m_List_Sale_Item_Equip_Probability.Add(10000);
                store.m_List_Sale_Item_Equip_Count_Min.Add(1);
                store.m_List_Sale_Item_Equip_Count_Max.Add(2);
                store.m_List_Sale_Item_Equip_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1001].m_nPrice);
                store.m_List_Sale_Item_Equip_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1001].m_nPrice);

                store.m_List_Sale_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[2001]);
                store.m_List_Sale_Item_Equip_Probability.Add(10000);
                store.m_List_Sale_Item_Equip_Count_Min.Add(1);
                store.m_List_Sale_Item_Equip_Count_Max.Add(5);
                store.m_List_Sale_Item_Equip_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[2001].m_nPrice);
                store.m_List_Sale_Item_Equip_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[2001].m_nPrice);

                store.m_List_Sale_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[3001]);
                store.m_List_Sale_Item_Equip_Probability.Add(10000);
                store.m_List_Sale_Item_Equip_Count_Min.Add(1);
                store.m_List_Sale_Item_Equip_Count_Max.Add(3);
                store.m_List_Sale_Item_Equip_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[3001].m_nPrice);
                store.m_List_Sale_Item_Equip_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[3001].m_nPrice);
            }

            // Item_Use
            {
                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8009]);
                store.m_List_Sale_Item_Use_Probability.Add(10000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(3);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8009].m_nPrice);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8009].m_nPrice);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8014]);
                store.m_List_Sale_Item_Use_Probability.Add(10000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(4);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8014].m_nPrice);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8014].m_nPrice);
            }

            store.m_ql_Quest_Necessity_Clear.Add(QuestManager.Instance.GetQuest_COLLECT(4000));

            m_List_NPC_Store.Add(store);
            m_List_NPC_Store_Code.Add(store.m_nStore_Code);
        }

        Update_Store();
    }
}
