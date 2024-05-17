using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_BoringSlime : NPC_Total
{
    private void Awake()
    {
        m_sNPCName = "[앤트 앤 칩스]\n무료한 슬라임";
        m_nNPCCode = 28;

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

        m_ql_QuestList_CONVERSATION.Add(QuestManager.Instance.GetQuest_CONVERSATION(5012));
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

        store = new NPC_Store("[[대충 만든 상점]]", 28001, m_Sprite_NPC, E_STORE_LEVEL.S2);
        if (m_List_NPC_Store_Code.Contains(store.m_nStore_Code) == false)
        {
            store.m_sDescription = "아__ 난 시세따윈 잘 몰라__ 살거면 사고 안살거면 가라__ -_-\n나한테 어떤 물건이든 판매할 생각은 안하는게 좋을걸__\n따분하네\n하 ~ .... . .  .  .   .     .       . ~ 품 -_-";
            store.m_fBuy_Item_Equip_Value = 0.1f;
            store.m_fBuy_Item_Use_Value = 0.1f;
            store.m_fBuy_Item_Etc_Value = 0.1f;
            // Item_Equip
            {
                store.m_List_Sale_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1005]);
                store.m_List_Sale_Item_Equip_Probability.Add(2500);
                store.m_List_Sale_Item_Equip_Count_Min.Add(1);
                store.m_List_Sale_Item_Equip_Count_Max.Add(1);
                store.m_List_Sale_Item_Equip_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1005].m_nPrice - 100);
                store.m_List_Sale_Item_Equip_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1005].m_nPrice + 100);

                store.m_List_Sale_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1305]);
                store.m_List_Sale_Item_Equip_Probability.Add(2500);
                store.m_List_Sale_Item_Equip_Count_Min.Add(1);
                store.m_List_Sale_Item_Equip_Count_Max.Add(1);
                store.m_List_Sale_Item_Equip_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1305].m_nPrice - 100);
                store.m_List_Sale_Item_Equip_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1305].m_nPrice + 100);

                store.m_List_Sale_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1605]);
                store.m_List_Sale_Item_Equip_Probability.Add(2500);
                store.m_List_Sale_Item_Equip_Count_Min.Add(1);
                store.m_List_Sale_Item_Equip_Count_Max.Add(1);
                store.m_List_Sale_Item_Equip_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1605].m_nPrice - 100);
                store.m_List_Sale_Item_Equip_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1605].m_nPrice + 100);

                store.m_List_Sale_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[4002]);
                store.m_List_Sale_Item_Equip_Probability.Add(2500);
                store.m_List_Sale_Item_Equip_Count_Min.Add(1);
                store.m_List_Sale_Item_Equip_Count_Max.Add(1);
                store.m_List_Sale_Item_Equip_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[4002].m_nPrice - 100);
                store.m_List_Sale_Item_Equip_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[4002].m_nPrice + 100);

                store.m_List_Sale_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[4012]);
                store.m_List_Sale_Item_Equip_Probability.Add(2500);
                store.m_List_Sale_Item_Equip_Count_Min.Add(1);
                store.m_List_Sale_Item_Equip_Count_Max.Add(1);
                store.m_List_Sale_Item_Equip_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[4012].m_nPrice - 100);
                store.m_List_Sale_Item_Equip_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[4012].m_nPrice + 100);

                store.m_List_Sale_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[3002]);
                store.m_List_Sale_Item_Equip_Probability.Add(2500);
                store.m_List_Sale_Item_Equip_Count_Min.Add(1);
                store.m_List_Sale_Item_Equip_Count_Max.Add(1);
                store.m_List_Sale_Item_Equip_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[3002].m_nPrice - 100);
                store.m_List_Sale_Item_Equip_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[3002].m_nPrice + 100);

                store.m_List_Sale_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[6002]);
                store.m_List_Sale_Item_Equip_Probability.Add(2500);
                store.m_List_Sale_Item_Equip_Count_Min.Add(1);
                store.m_List_Sale_Item_Equip_Count_Max.Add(1);
                store.m_List_Sale_Item_Equip_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[6002].m_nPrice - 100);
                store.m_List_Sale_Item_Equip_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[6002].m_nPrice + 100);
            }
            // Item_Use
            {
                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8002]);
                store.m_List_Sale_Item_Use_Probability.Add(2500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(3);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8002].m_nPrice - 30);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8002].m_nPrice + 30);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8005]);
                store.m_List_Sale_Item_Use_Probability.Add(1000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(3);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8005].m_nPrice - 100);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8005].m_nPrice + 100);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8008]);
                store.m_List_Sale_Item_Use_Probability.Add(100);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(3);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8008].m_nPrice - 300);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8008].m_nPrice + 300);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8014]);
                store.m_List_Sale_Item_Use_Probability.Add(5000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(5);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8014].m_nPrice - 20);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8014].m_nPrice + 20);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8020]);
                store.m_List_Sale_Item_Use_Probability.Add(5000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(5);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8020].m_nPrice - 20);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8020].m_nPrice + 20);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9008]);
                store.m_List_Sale_Item_Use_Probability.Add(7500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(3);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9008].m_nPrice - 40);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9008].m_nPrice + 40);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12300]);
                store.m_List_Sale_Item_Use_Probability.Add(3000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(3);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12300].m_nPrice - 500);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12300].m_nPrice + 500);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12301]);
                store.m_List_Sale_Item_Use_Probability.Add(3000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(3);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12301].m_nPrice - 500);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12301].m_nPrice + 500);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12302]);
                store.m_List_Sale_Item_Use_Probability.Add(100);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(1);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12302].m_nPrice - 80000);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12302].m_nPrice + 80000);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12600]);
                store.m_List_Sale_Item_Use_Probability.Add(3000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(3);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12600].m_nPrice - 400);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12600].m_nPrice + 400);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12601]);
                store.m_List_Sale_Item_Use_Probability.Add(1000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(3);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12601].m_nPrice - 1250);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12601].m_nPrice + 1250);
            }
            // Item_Etc
            {
                store.m_List_Sale_Item_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0002]);
                store.m_List_Sale_Item_Etc_Probability.Add(1000);
                store.m_List_Sale_Item_Etc_Count_Min.Add(1);
                store.m_List_Sale_Item_Etc_Count_Max.Add(3);
                store.m_List_Sale_Item_Etc_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0002].m_nPrice - 50);
                store.m_List_Sale_Item_Etc_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0002].m_nPrice + 50);

                //store.m_List_Sale_Item_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0003]);
                //store.m_List_Sale_Item_Etc_Probability.Add(1000);
                //store.m_List_Sale_Item_Etc_Count_Min.Add(1);
                //store.m_List_Sale_Item_Etc_Count_Max.Add(3);
                //store.m_List_Sale_Item_Etc_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0003].m_nPrice - 50);
                //store.m_List_Sale_Item_Etc_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0003].m_nPrice + 50);

                store.m_List_Sale_Item_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0003]);
                store.m_List_Sale_Item_Etc_Probability.Add(10000);
                store.m_List_Sale_Item_Etc_Count_Min.Add(1);
                store.m_List_Sale_Item_Etc_Count_Max.Add(3);
                store.m_List_Sale_Item_Etc_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0003].m_nPrice - 25);
                store.m_List_Sale_Item_Etc_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0003].m_nPrice + 25);

                store.m_List_Sale_Item_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0008]);
                store.m_List_Sale_Item_Etc_Probability.Add(9000);
                store.m_List_Sale_Item_Etc_Count_Min.Add(1);
                store.m_List_Sale_Item_Etc_Count_Max.Add(10);
                store.m_List_Sale_Item_Etc_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0008].m_nPrice - 4);
                store.m_List_Sale_Item_Etc_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0008].m_nPrice + 4);

                store.m_List_Sale_Item_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0009]);
                store.m_List_Sale_Item_Etc_Probability.Add(9000);
                store.m_List_Sale_Item_Etc_Count_Min.Add(1);
                store.m_List_Sale_Item_Etc_Count_Max.Add(3);
                store.m_List_Sale_Item_Etc_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0009].m_nPrice - 4);
                store.m_List_Sale_Item_Etc_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0009].m_nPrice + 4);

                store.m_List_Sale_Item_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0011]);
                store.m_List_Sale_Item_Etc_Probability.Add(1000);
                store.m_List_Sale_Item_Etc_Count_Min.Add(1);
                store.m_List_Sale_Item_Etc_Count_Max.Add(3);
                store.m_List_Sale_Item_Etc_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0011].m_nPrice - 4);
                store.m_List_Sale_Item_Etc_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0011].m_nPrice + 4);

                store.m_List_Sale_Item_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0012]);
                store.m_List_Sale_Item_Etc_Probability.Add(3000);
                store.m_List_Sale_Item_Etc_Count_Min.Add(1);
                store.m_List_Sale_Item_Etc_Count_Max.Add(3);
                store.m_List_Sale_Item_Etc_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0012].m_nPrice - 4);
                store.m_List_Sale_Item_Etc_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0012].m_nPrice + 4);

                store.m_List_Sale_Item_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0013]);
                store.m_List_Sale_Item_Etc_Probability.Add(5000);
                store.m_List_Sale_Item_Etc_Count_Min.Add(1);
                store.m_List_Sale_Item_Etc_Count_Max.Add(3);
                store.m_List_Sale_Item_Etc_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0013].m_nPrice - 5);
                store.m_List_Sale_Item_Etc_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0013].m_nPrice + 5);

                store.m_List_Sale_Item_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0014]);
                store.m_List_Sale_Item_Etc_Probability.Add(9000);
                store.m_List_Sale_Item_Etc_Count_Min.Add(1);
                store.m_List_Sale_Item_Etc_Count_Max.Add(3);
                store.m_List_Sale_Item_Etc_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0014].m_nPrice - 5);
                store.m_List_Sale_Item_Etc_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0014].m_nPrice + 5);
            }


            m_List_NPC_Store.Add(store);
            m_List_NPC_Store_Code.Add(store.m_nStore_Code);
        }

        Update_Store();
    }
}
