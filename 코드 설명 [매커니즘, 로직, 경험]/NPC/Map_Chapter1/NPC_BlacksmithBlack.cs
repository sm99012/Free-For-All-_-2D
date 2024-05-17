using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_BlacksmithBlack : NPC_Total
{
    private void Awake()
    {
        m_sNPCName = "[대장장이]\n블랙";
        m_nNPCCode = 22;

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

        m_ql_QuestList_CONVERSATION.Add(QuestManager.Instance.GetQuest_CONVERSATION(5011));
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

        store = new NPC_Store("[[블랙의 무기 상점]]", 22001, m_Sprite_NPC, E_STORE_LEVEL.S3);
        if (m_List_NPC_Store_Code.Contains(store.m_nStore_Code) == false)
        {
            store.m_sDescription = "어서오세요~\n주로 양산형 무기를 만들어 판매합니다~ 제가 만들지 못하는 장비는 좀 더 비싸게 사주겠습니다! 저도 발전해야 하거든요.\n가격은 비싸더라도 구하기 힘든 강화서도 판매를 하고 있답니다.\n다만 환불은 없습니다~";
            store.m_fBuy_Item_Equip_Value = 1.1f;
            store.m_fBuy_Item_Use_Value = 0.75f;
            store.m_fBuy_Item_Etc_Value = 0.3f;

            // Item_Equip
            {
                store.m_List_Sale_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1001]);
                store.m_List_Sale_Item_Equip_Probability.Add(10000);
                store.m_List_Sale_Item_Equip_Count_Min.Add(1);
                store.m_List_Sale_Item_Equip_Count_Max.Add(1);
                store.m_List_Sale_Item_Equip_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1001].m_nPrice - 50);
                store.m_List_Sale_Item_Equip_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1001].m_nPrice + 50);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1001]);
                store.m_List_Buy_Item_Price_Min.Add(0);
                store.m_List_Buy_Item_Price_Max.Add(0);

                store.m_List_Sale_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1003]);
                store.m_List_Sale_Item_Equip_Probability.Add(5000);
                store.m_List_Sale_Item_Equip_Count_Min.Add(1);
                store.m_List_Sale_Item_Equip_Count_Max.Add(1);
                store.m_List_Sale_Item_Equip_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1003].m_nPrice - 50);
                store.m_List_Sale_Item_Equip_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1003].m_nPrice + 50);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1003]);
                store.m_List_Buy_Item_Price_Min.Add(0);
                store.m_List_Buy_Item_Price_Max.Add(0);

                store.m_List_Sale_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1004]);
                store.m_List_Sale_Item_Equip_Probability.Add(10000);
                store.m_List_Sale_Item_Equip_Count_Min.Add(1);
                store.m_List_Sale_Item_Equip_Count_Max.Add(1);
                store.m_List_Sale_Item_Equip_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1004].m_nPrice - 50);
                store.m_List_Sale_Item_Equip_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1004].m_nPrice + 50);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1004]);
                store.m_List_Buy_Item_Price_Min.Add(0);
                store.m_List_Buy_Item_Price_Max.Add(0);

                store.m_List_Sale_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1301]);
                store.m_List_Sale_Item_Equip_Probability.Add(10000);
                store.m_List_Sale_Item_Equip_Count_Min.Add(1);
                store.m_List_Sale_Item_Equip_Count_Max.Add(1);
                store.m_List_Sale_Item_Equip_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1301].m_nPrice - 50);
                store.m_List_Sale_Item_Equip_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1301].m_nPrice + 50);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1301]);
                store.m_List_Buy_Item_Price_Min.Add(0);
                store.m_List_Buy_Item_Price_Max.Add(0);

                store.m_List_Sale_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1303]);
                store.m_List_Sale_Item_Equip_Probability.Add(5000);
                store.m_List_Sale_Item_Equip_Count_Min.Add(1);
                store.m_List_Sale_Item_Equip_Count_Max.Add(1);
                store.m_List_Sale_Item_Equip_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1303].m_nPrice - 50);
                store.m_List_Sale_Item_Equip_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1303].m_nPrice + 50);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1303]);
                store.m_List_Buy_Item_Price_Min.Add(0);
                store.m_List_Buy_Item_Price_Max.Add(0);

                store.m_List_Sale_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1304]);
                store.m_List_Sale_Item_Equip_Probability.Add(10000);
                store.m_List_Sale_Item_Equip_Count_Min.Add(1);
                store.m_List_Sale_Item_Equip_Count_Max.Add(1);
                store.m_List_Sale_Item_Equip_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1304].m_nPrice - 50);
                store.m_List_Sale_Item_Equip_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1304].m_nPrice + 50);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1304]);
                store.m_List_Buy_Item_Price_Min.Add(0);
                store.m_List_Buy_Item_Price_Max.Add(0);

                store.m_List_Sale_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1601]);
                store.m_List_Sale_Item_Equip_Probability.Add(10000);
                store.m_List_Sale_Item_Equip_Count_Min.Add(1);
                store.m_List_Sale_Item_Equip_Count_Max.Add(1);
                store.m_List_Sale_Item_Equip_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1601].m_nPrice - 50);
                store.m_List_Sale_Item_Equip_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1601].m_nPrice + 50);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1601]);
                store.m_List_Buy_Item_Price_Min.Add(0);
                store.m_List_Buy_Item_Price_Max.Add(0);

                store.m_List_Sale_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1602]);
                store.m_List_Sale_Item_Equip_Probability.Add(7000);
                store.m_List_Sale_Item_Equip_Count_Min.Add(1);
                store.m_List_Sale_Item_Equip_Count_Max.Add(1);
                store.m_List_Sale_Item_Equip_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1602].m_nPrice - 50);
                store.m_List_Sale_Item_Equip_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1602].m_nPrice + 50);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1602]);
                store.m_List_Buy_Item_Price_Min.Add(0);
                store.m_List_Buy_Item_Price_Max.Add(0);

                store.m_List_Sale_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1603]);
                store.m_List_Sale_Item_Equip_Probability.Add(3000);
                store.m_List_Sale_Item_Equip_Count_Min.Add(1);
                store.m_List_Sale_Item_Equip_Count_Max.Add(1);
                store.m_List_Sale_Item_Equip_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1603].m_nPrice - 50);
                store.m_List_Sale_Item_Equip_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1603].m_nPrice + 50);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1603]);
                store.m_List_Buy_Item_Price_Min.Add(0);
                store.m_List_Buy_Item_Price_Max.Add(0);

                store.m_List_Sale_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[2007]);
                store.m_List_Sale_Item_Equip_Probability.Add(1000);
                store.m_List_Sale_Item_Equip_Count_Min.Add(1);
                store.m_List_Sale_Item_Equip_Count_Max.Add(1);
                store.m_List_Sale_Item_Equip_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[2007].m_nPrice - 50);
                store.m_List_Sale_Item_Equip_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[2007].m_nPrice + 50);
                store.m_List_Buy_Item.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[2007]);
                store.m_List_Buy_Item_Price_Min.Add(0);
                store.m_List_Buy_Item_Price_Max.Add(0);
            }
            // Item_Use
            {
                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11000]);
                store.m_List_Sale_Item_Use_Probability.Add(5000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(5);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11000].m_nPrice + 50);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11000].m_nPrice + 100);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11001]);
                store.m_List_Sale_Item_Use_Probability.Add(4000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(4);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11001].m_nPrice + 100);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11001].m_nPrice + 200);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11002]);
                store.m_List_Sale_Item_Use_Probability.Add(3000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(3);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11002].m_nPrice + 150);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11002].m_nPrice + 300);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11003]);
                store.m_List_Sale_Item_Use_Probability.Add(2000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(2);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11003].m_nPrice + 200);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11003].m_nPrice + 400);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11004]);
                store.m_List_Sale_Item_Use_Probability.Add(1000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(1);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11004].m_nPrice + 1000);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11004].m_nPrice + 2000);



                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11010]);
                store.m_List_Sale_Item_Use_Probability.Add(5000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(5);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11010].m_nPrice + 50);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11010].m_nPrice + 100);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11011]);
                store.m_List_Sale_Item_Use_Probability.Add(4000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(4);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11011].m_nPrice + 100);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11011].m_nPrice + 200);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11012]);
                store.m_List_Sale_Item_Use_Probability.Add(3000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(3);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11012].m_nPrice + 150);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11012].m_nPrice + 300);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11013]);
                store.m_List_Sale_Item_Use_Probability.Add(2000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(2);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11013].m_nPrice + 200);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11013].m_nPrice + 400);

                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11014]);
                store.m_List_Sale_Item_Use_Probability.Add(1000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(1);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11014].m_nPrice + 1000);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11014].m_nPrice + 2000);



                store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11020]);
                store.m_List_Sale_Item_Use_Probability.Add(10000);
                store.m_List_Sale_Item_Use_Count_Min.Add(1);
                store.m_List_Sale_Item_Use_Count_Max.Add(10);
                store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11020].m_nPrice + 1000);
                store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11020].m_nPrice + 2000);
            }

            m_List_NPC_Store.Add(store);
            m_List_NPC_Store_Code.Add(store.m_nStore_Code);
        }

        Update_Store();
    }
}
