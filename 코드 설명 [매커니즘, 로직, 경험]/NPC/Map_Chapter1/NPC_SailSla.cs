using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_SailSla : NPC_Total
{
    private void Awake()
    {
        m_sNPCName = "[주식회사 더 슬라]\n세일슬라";
        m_nNPCCode = 26;

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

        store = new NPC_Store("[[주식회사 더 슬라 드넓은 초원 지부]]", 26001, m_Sprite_NPC, E_STORE_LEVEL.S2);
        if (m_List_NPC_Store_Code.Contains(store.m_nStore_Code) == false)
        {
            store.m_sDescription = "안녕하십니까!\n'주식회사 더 슬라' 입니다!\n다양한 식재료와 전리품을 판매하고 있으며, 모든 물건을 80% 가격으로 매입 합니다@@@@";
            store.m_fBuy_Item_Equip_Value = 0.80f;
            store.m_fBuy_Item_Use_Value = 0.80f;
            store.m_fBuy_Item_Etc_Value = 0.80f;
            // Item_Use
            {
                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8009]);
                store.m_List_Sale_Item_Use_Probability.Add(7500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8009].m_nPrice - 2);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8009].m_nPrice + 8);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8010]);
                store.m_List_Sale_Item_Use_Probability.Add(7500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(5);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8010].m_nPrice - 15);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8010].m_nPrice + 60);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8012]);
                store.m_List_Sale_Item_Use_Probability.Add(7500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8012].m_nPrice - 3);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8012].m_nPrice + 12);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8015]);
                store.m_List_Sale_Item_Use_Probability.Add(7500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8015].m_nPrice - 1);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8015].m_nPrice + 4);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8019]);
                store.m_List_Sale_Item_Use_Probability.Add(7500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8019].m_nPrice - 1);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8019].m_nPrice + 4);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8021]);
                store.m_List_Sale_Item_Use_Probability.Add(3300);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(5);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8021].m_nPrice - 2);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8021].m_nPrice + 10);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8022]);
                store.m_List_Sale_Item_Use_Probability.Add(7500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8022].m_nPrice - 1);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8022].m_nPrice + 6);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8024]);
                store.m_List_Sale_Item_Use_Probability.Add(7500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8024].m_nPrice - 1);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8024].m_nPrice + 4);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8025]);
                store.m_List_Sale_Item_Use_Probability.Add(100);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(2);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8025].m_nPrice);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8025].m_nPrice + 200);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9002]);
                store.m_List_Sale_Item_Use_Probability.Add(100);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(2);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9002].m_nPrice);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9002].m_nPrice + 2000);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8014]);
                store.m_List_Sale_Item_Use_Probability.Add(7500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8014].m_nPrice - 3);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8014].m_nPrice + 14);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8028]);
                store.m_List_Sale_Item_Use_Probability.Add(7500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8028].m_nPrice - 6);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8028].m_nPrice + 24);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8017]);
                store.m_List_Sale_Item_Use_Probability.Add(3300);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(5);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8017].m_nPrice - 15);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8017].m_nPrice + 62);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8018]);
                store.m_List_Sale_Item_Use_Probability.Add(7500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(20);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8018].m_nPrice - 3);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8018].m_nPrice + 14);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8020]);
                store.m_List_Sale_Item_Use_Probability.Add(7500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(20);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8020].m_nPrice - 1);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8020].m_nPrice + 6);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8026]);
                store.m_List_Sale_Item_Use_Probability.Add(7500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8026].m_nPrice - 4);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8026].m_nPrice + 16);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8027]);
                store.m_List_Sale_Item_Use_Probability.Add(7500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8027].m_nPrice - 3);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8027].m_nPrice + 12);
            }

            // Item_Etc
            {
                store.m_List_Sale_Item_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0001]);
                store.m_List_Sale_Item_Etc_Probability.Add(7500);
                store.m_List_Sale_Item_Etc_Count_Min.Add(1);
                store.m_List_Sale_Item_Etc_Count_Max.Add(10);
                store.m_List_Sale_Item_Etc_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0001].m_nPrice + 1);
                store.m_List_Sale_Item_Etc_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0001].m_nPrice + 6);

                store.m_List_Sale_Item_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0013]);
                store.m_List_Sale_Item_Etc_Probability.Add(3300);
                store.m_List_Sale_Item_Etc_Count_Min.Add(1);
                store.m_List_Sale_Item_Etc_Count_Max.Add(10);
                store.m_List_Sale_Item_Etc_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0013].m_nPrice + 1);
                store.m_List_Sale_Item_Etc_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0013].m_nPrice + 7);

                store.m_List_Sale_Item_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0014]);
                store.m_List_Sale_Item_Etc_Probability.Add(3300);
                store.m_List_Sale_Item_Etc_Count_Min.Add(1);
                store.m_List_Sale_Item_Etc_Count_Max.Add(10);
                store.m_List_Sale_Item_Etc_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0014].m_nPrice + 1);
                store.m_List_Sale_Item_Etc_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0014].m_nPrice + 6);
            }

            m_List_NPC_Store.Add(store);
            m_List_NPC_Store_Code.Add(store.m_nStore_Code);
        }

        Update_Store();
    }
}
