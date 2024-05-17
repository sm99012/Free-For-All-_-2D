using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_CatOnTheDriedTree : NPC_Total
{
    private void Awake()
    {
        m_sNPCName = "위태로운 고양이";
        m_nNPCCode = 4;

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

        m_cl_Conversation.Add(ConversationManager.Instance.m_Dictionary_ConversationList[4]);
    }

    override public void InitialSet_Store()
    {
        m_bNPC_Store = true;

        m_List_NPC_Store = new List<NPC_Store>();
        m_List_NPC_Store_Code = new List<int>();

        NPC_Store store;

        store = new NPC_Store("['나무 위의 위태로운 고양이' 와의 거래]", 4001, m_Sprite_NPC, E_STORE_LEVEL.S1);
        if (m_List_NPC_Store_Code.Contains(store.m_nStore_Code) == false)
        {
            store.m_sDescription = "냐옹~\n\n[고양이가 직접 물어온 잡다한 것들을 팔고있습니다.]";
            store.m_fBuy_Item_Equip_Value = 0.50f;
            store.m_fBuy_Item_Use_Value = 0.50f;
            store.m_fBuy_Item_Etc_Value = 0.50f;

            store.m_List_Sale_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1004]);
            store.m_List_Sale_Item_Equip_Probability.Add(7000);
            store.m_List_Sale_Item_Equip_Count_Min.Add(1);
            store.m_List_Sale_Item_Equip_Count_Max.Add(1);
            store.m_List_Sale_Item_Equip_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1004].m_nPrice);
            store.m_List_Sale_Item_Equip_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1004].m_nPrice);

            store.m_List_Sale_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[2001]);
            store.m_List_Sale_Item_Equip_Probability.Add(7000);
            store.m_List_Sale_Item_Equip_Count_Min.Add(1);
            store.m_List_Sale_Item_Equip_Count_Max.Add(1);
            store.m_List_Sale_Item_Equip_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[2001].m_nPrice);
            store.m_List_Sale_Item_Equip_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[2001].m_nPrice);

            store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8000]);
            store.m_List_Sale_Item_Use_Probability.Add(7000);
            store.m_List_Sale_Item_Use_Count_Min.Add(1);
            store.m_List_Sale_Item_Use_Count_Max.Add(4);
            store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8000].m_nPrice);
            store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8000].m_nPrice);

            store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8001]);
            store.m_List_Sale_Item_Use_Probability.Add(7000);
            store.m_List_Sale_Item_Use_Count_Min.Add(1);
            store.m_List_Sale_Item_Use_Count_Max.Add(4);
            store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8001].m_nPrice);
            store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8001].m_nPrice);

            store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8002]);
            store.m_List_Sale_Item_Use_Probability.Add(7000);
            store.m_List_Sale_Item_Use_Count_Min.Add(1);
            store.m_List_Sale_Item_Use_Count_Max.Add(4);
            store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8002].m_nPrice);
            store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8002].m_nPrice);

            store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9000]);
            store.m_List_Sale_Item_Use_Probability.Add(7000);
            store.m_List_Sale_Item_Use_Count_Min.Add(1);
            store.m_List_Sale_Item_Use_Count_Max.Add(2);
            store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9000].m_nPrice);
            store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9000].m_nPrice);

            store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[10000]);
            store.m_List_Sale_Item_Use_Probability.Add(1000);
            store.m_List_Sale_Item_Use_Count_Min.Add(1);
            store.m_List_Sale_Item_Use_Count_Max.Add(1);
            store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[10000].m_nPrice);
            store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[10000].m_nPrice);

            store.m_List_Sale_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12600]);
            store.m_List_Sale_Item_Use_Probability.Add(5000);
            store.m_List_Sale_Item_Use_Count_Min.Add(1);
            store.m_List_Sale_Item_Use_Count_Max.Add(5);
            store.m_List_Sale_Item_Use_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12600].m_nPrice);
            store.m_List_Sale_Item_Use_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12600].m_nPrice);

            store.m_List_Sale_Item_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0013]);
            store.m_List_Sale_Item_Etc_Probability.Add(2000);
            store.m_List_Sale_Item_Etc_Count_Min.Add(1);
            store.m_List_Sale_Item_Etc_Count_Max.Add(3);
            store.m_List_Sale_Item_Etc_Price_Min.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0013].m_nPrice);
            store.m_List_Sale_Item_Etc_Price_Max.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0013].m_nPrice);

            m_List_NPC_Store.Add(store);
            m_List_NPC_Store_Code.Add(store.m_nStore_Code);
        }

        Update_Store();
    }
}
