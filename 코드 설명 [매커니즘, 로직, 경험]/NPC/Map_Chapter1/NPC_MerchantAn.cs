using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_MerchantAn : NPC_Total
{
    private void Awake()
    {
        m_sNPCName = "[뜨내기 상인]\n앤";
        m_nNPCCode = 21;

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

        m_cl_Conversation.Add(ConversationManager.Instance.m_Dictionary_ConversationList[30]);

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

        store = new NPC_Store("[[뜨내기의 잡화상점]]", 21001, m_Sprite_NPC, E_STORE_LEVEL.S1);
        if (m_List_NPC_Store_Code.Contains(store.m_nStore_Code) == false)
        {
            store.m_sDescription = "어서와!\n이곳에서는 '드넓은 초원' 에서 얻은 식재료를 주로 판매하고 있어! 드물게 주변의 모험가로부터 구해온 전리품도 있으니 구경해봐!\n아참! 그리고 내가 아직 시세를 잘 몰라서 싸거나 비싸게 팔수도 있어ㅠ";
            store.m_fBuy_Item_Equip_Value = 0.70f;
            store.m_fBuy_Item_Use_Value = 0.70f;
            store.m_fBuy_Item_Etc_Value = 0.70f;
            // Item_Equip
            {
                store.m_List_Sale_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[4003]);
                store.m_List_Sale_Item_Equip_Probability.Add(500);
                store.m_List_Sale_Item_Equip_Count_Min.Add(1);
                store.m_List_Sale_Item_Equip_Count_Max.Add(1);
                store.m_List_Sale_Item_Equip_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[4003].m_nPrice - 50);
                store.m_List_Sale_Item_Equip_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[4003].m_nPrice + 50);

                store.m_List_Sale_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[4012]);
                store.m_List_Sale_Item_Equip_Probability.Add(500);
                store.m_List_Sale_Item_Equip_Count_Min.Add(1);
                store.m_List_Sale_Item_Equip_Count_Max.Add(1);
                store.m_List_Sale_Item_Equip_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[4012].m_nPrice - 50);
                store.m_List_Sale_Item_Equip_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[4012].m_nPrice + 50);
            }
            // Item_Use
            {
                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8000]);
                store.m_List_Sale_Item_Use_Probability.Add(3000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(2);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8000].m_nPrice + 10);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8000].m_nPrice + 20);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8001]);
                store.m_List_Sale_Item_Use_Probability.Add(3000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(2);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8001].m_nPrice + 10);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8001].m_nPrice + 20);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8002]);
                store.m_List_Sale_Item_Use_Probability.Add(3000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(2);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8002].m_nPrice + 10);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8002].m_nPrice + 20);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8013]);
                store.m_List_Sale_Item_Use_Probability.Add(5000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8013].m_nPrice - 3);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8013].m_nPrice + 3);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8014]);
                store.m_List_Sale_Item_Use_Probability.Add(7500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8014].m_nPrice - 5);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8014].m_nPrice + 5);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8009]);
                store.m_List_Sale_Item_Use_Probability.Add(500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(3);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8009].m_nPrice - 5);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8009].m_nPrice + 5);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8011]);
                store.m_List_Sale_Item_Use_Probability.Add(1000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(1);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8011].m_nPrice - 20);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8011].m_nPrice + 20);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8012]);
                store.m_List_Sale_Item_Use_Probability.Add(3000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(5);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8012].m_nPrice - 5);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8012].m_nPrice + 5);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8015]);
                store.m_List_Sale_Item_Use_Probability.Add(4000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8015].m_nPrice - 5);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8015].m_nPrice + 5);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8019]);
                store.m_List_Sale_Item_Use_Probability.Add(7500);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8019].m_nPrice - 1);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8019].m_nPrice + 1);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8022]);
                store.m_List_Sale_Item_Use_Probability.Add(4000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8022].m_nPrice - 5);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8022].m_nPrice + 5);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8024]);
                store.m_List_Sale_Item_Use_Probability.Add(2000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8024].m_nPrice + 5);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8024].m_nPrice + 10);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11000]);
                store.m_List_Sale_Item_Use_Probability.Add(1000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(1);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11000].m_nPrice + 50);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11000].m_nPrice + 100);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11001]);
                store.m_List_Sale_Item_Use_Probability.Add(1000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(1);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11001].m_nPrice + 50);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11001].m_nPrice + 100);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11002]);
                store.m_List_Sale_Item_Use_Probability.Add(1000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(1);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11002].m_nPrice + 50);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11002].m_nPrice + 100);

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

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12600]);
                store.m_List_Sale_Item_Use_Probability.Add(3000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(3);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12600].m_nPrice - 400);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12600].m_nPrice + 400);
            }
            // Item_Etc
            {
                store.m_List_Sale_Item_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0002]);
                store.m_List_Sale_Item_Etc_Probability.Add(100);
                store.m_List_Sale_Item_Etc_Count_Min.Add(1);
                store.m_List_Sale_Item_Etc_Count_Max.Add(3);
                store.m_List_Sale_Item_Etc_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0002].m_nPrice - 10);
                store.m_List_Sale_Item_Etc_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0002].m_nPrice + 20);

                store.m_List_Sale_Item_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0003]);
                store.m_List_Sale_Item_Etc_Probability.Add(100);
                store.m_List_Sale_Item_Etc_Count_Min.Add(1);
                store.m_List_Sale_Item_Etc_Count_Max.Add(5);
                store.m_List_Sale_Item_Etc_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0003].m_nPrice - 10);
                store.m_List_Sale_Item_Etc_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0003].m_nPrice + 30);

                store.m_List_Sale_Item_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0016]);
                store.m_List_Sale_Item_Etc_Probability.Add(500);
                store.m_List_Sale_Item_Etc_Count_Min.Add(1);
                store.m_List_Sale_Item_Etc_Count_Max.Add(1);
                store.m_List_Sale_Item_Etc_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0016].m_nPrice + 200);
                store.m_List_Sale_Item_Etc_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0016].m_nPrice + 300);
            }

            m_List_NPC_Store.Add(store);
            m_List_NPC_Store_Code.Add(store.m_nStore_Code);
        }

        Update_Store();
    }
}
