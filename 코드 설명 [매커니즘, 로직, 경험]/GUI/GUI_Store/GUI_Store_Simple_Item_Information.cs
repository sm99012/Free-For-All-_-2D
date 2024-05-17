using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUI_Store_Simple_Item_Information : MonoBehaviour
{
    // 상점에서 아이템 구매 / 판매 시 사용할 간단한 UI.
    [SerializeField] public GameObject m_gPanel_Store_Simple_Item_Information;
    [Space(20)]

    [SerializeField] GameObject m_gPanel_Store_Simple_Item_Information_Content;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_Store_Simple_Item_Information_Content_Content;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_store_Simple_Item_Information_Content_Content_Price;
    [SerializeField] TextMeshProUGUI m_TMP_store_Simple_Item_Information_Content_Content_Price;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_Store_Simple_Item_Information_Content_Content_Count;
    [SerializeField] TextMeshProUGUI m_TMP_Store_Simple_Item_Information_Content_Content_Count;
    [SerializeField] Button m_BTN_Store_Simple_Item_Information_Content_Content_Count_Up;
    [SerializeField] Button m_BTN_Store_Simple_Item_Information_Content_Content_Count_Down;
    [Space(20)]
    [SerializeField] Button m_BTN_Store_Simple_Item_Information_Content_Trade;
    [SerializeField] TextMeshProUGUI m_TMP_Store_Simple_Item_Information_Content_Trade;


    public void InitialSet()
    {
        InitialSet_Object();
        InitialSet_Button();

        m_bExit_BuyList = false;
    }

    void InitialSet_Object()
    {
        m_gPanel_Store_Simple_Item_Information = GameObject.Find("Canvas_GUI").gameObject.transform.Find("Panel_Store_Simple_Item_Information").gameObject;

        m_gPanel_Store_Simple_Item_Information_Content = m_gPanel_Store_Simple_Item_Information.transform.Find("Panel_Store_Simple_Item_Information_Content").gameObject;

        m_gPanel_Store_Simple_Item_Information_Content_Content = m_gPanel_Store_Simple_Item_Information_Content.transform.Find("Panel_Store_Simple_Item_Information_Content_Content").gameObject;

        m_gPanel_store_Simple_Item_Information_Content_Content_Price = m_gPanel_Store_Simple_Item_Information_Content_Content.transform.Find("Panel_Store_Simple_Item_Information_Content_Content_Price").gameObject;
        m_TMP_store_Simple_Item_Information_Content_Content_Price = m_gPanel_store_Simple_Item_Information_Content_Content_Price.transform.Find("TMP_Store_Simple_Item_Information_Content_Content_Price").gameObject.GetComponent<TextMeshProUGUI>();

        m_gPanel_Store_Simple_Item_Information_Content_Content_Count = m_gPanel_Store_Simple_Item_Information_Content_Content.transform.Find("Panel_Store_Simple_Item_Information_Content_Content_Count").gameObject;
        m_TMP_Store_Simple_Item_Information_Content_Content_Count = m_gPanel_Store_Simple_Item_Information_Content_Content_Count.transform.Find("TMP_Store_Simple_Item_Information_Content_Content_Count").gameObject.GetComponent<TextMeshProUGUI>();
        m_BTN_Store_Simple_Item_Information_Content_Content_Count_Up = m_gPanel_Store_Simple_Item_Information_Content_Content_Count.transform.Find("BTN_Store_Simple_Item_Information_Content_Content_Count_Up").gameObject.GetComponent<Button>();
        m_BTN_Store_Simple_Item_Information_Content_Content_Count_Down = m_gPanel_Store_Simple_Item_Information_Content_Content_Count.transform.Find("BTN_Store_Simple_Item_Information_Content_Content_Count_Down").gameObject.GetComponent<Button>();

        m_BTN_Store_Simple_Item_Information_Content_Trade = m_gPanel_Store_Simple_Item_Information_Content.transform.Find("BTN_Store_Simple_Item_Information_Content_Trade").gameObject.GetComponent<Button>();
        m_TMP_Store_Simple_Item_Information_Content_Trade = m_BTN_Store_Simple_Item_Information_Content_Trade.transform.Find("TMP_Store_Simple_Item_Information_Content_Trade").gameObject.GetComponent<TextMeshProUGUI>();
    }

    void InitialSet_Button()
    {

    }
    void Press_Btn_Exit()
    {
        m_gPanel_Store_Simple_Item_Information.SetActive(false);
    }

    void Set_Button(int arynum)
    {
        m_BTN_Store_Simple_Item_Information_Content_Content_Count_Up.onClick.RemoveAllListeners();
        m_BTN_Store_Simple_Item_Information_Content_Content_Count_Up.onClick.AddListener(delegate { Press_Btn_Count_Up(); });
        m_BTN_Store_Simple_Item_Information_Content_Content_Count_Down.onClick.RemoveAllListeners();
        m_BTN_Store_Simple_Item_Information_Content_Content_Count_Down.onClick.AddListener(delegate { Press_Btn_Count_Down(); });
    }
    // 1 < 판매 및 구매 수량 < 10 (한 슬롯 당 10개의 아이템만 존재할 수 있기 때문에.)
    int m_nItemCount_Min = 1;
    int m_nItemCount_Max = 10;
    int m_nItemCount_Min_Current;
    int m_nItemCount_Max_Current;
    int m_nItemCount_Current;
    int m_nItemPrice_Fixed;
    int m_nItemPrice;
    int m_nTotalItem_Price;
    int m_nItemCode;
    bool m_bExit_BuyList;
    void Press_Btn_Count_Up()
    {
        if (m_nItemCount_Max_Current > m_nItemCount_Current)
        {
            m_nItemCount_Current++;
            m_TMP_Store_Simple_Item_Information_Content_Content_Count.text = m_nItemCount_Current.ToString();
            m_nTotalItem_Price = m_nItemCount_Current * m_nItemPrice;
            m_TMP_store_Simple_Item_Information_Content_Content_Price.text = m_nTotalItem_Price.ToString() + "골드";
            m_nTotalItem_Price = m_nItemCount_Current * m_nItemPrice_Fixed;
            m_TMP_store_Simple_Item_Information_Content_Content_Price.text += " (정가: " + m_nTotalItem_Price.ToString() + "골드)";
        }
    }
    void Press_Btn_Count_Down()
    {
        if (m_nItemCount_Min_Current < m_nItemCount_Current)
        {
            m_nItemCount_Current--;
            m_TMP_Store_Simple_Item_Information_Content_Content_Count.text = m_nItemCount_Current.ToString();
            m_nTotalItem_Price = m_nItemCount_Current * m_nItemPrice;
            m_TMP_store_Simple_Item_Information_Content_Content_Price.text = m_nTotalItem_Price.ToString() + "골드";
            m_nTotalItem_Price = m_nItemCount_Current * m_nItemPrice_Fixed;
            m_TMP_store_Simple_Item_Information_Content_Content_Price.text += " (정가: " + m_nTotalItem_Price.ToString() + "골드)";
        }
    }
    void Press_Btn_Sale(E_ITEMSLOT ei, int arynum)
    {
        switch(ei)
        {
            case E_ITEMSLOT.EQUIP:
                {
                    if (Player_Total.Instance.m_pi_Itemslot.m_nGold >= m_nTotalItem_Price)
                    {
                        Player_Total.Instance.m_pi_Itemslot.m_nGold -= m_nTotalItem_Price;
                        Item_Equip item = GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Sale_Item_Equip_Current[arynum].CreateItem(GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Sale_Item_Equip_Current[arynum]);
                        for (int i = 0; i < m_nItemCount_Current; i++)
                        {                            
                            Destroy(item);
                            if (Player_Total.Instance.m_pi_Itemslot.Get_Item_Equip(item) != -1)
                            {
                                if ((GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Sale_Item_Equip_Count[arynum] -= 1) > 0){ }
                                else
                                {
                                    GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.Remove_Sale_Item(E_ITEMSLOT.EQUIP, arynum);
                                    //GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Sale_Item_Equip_Current.RemoveAt(arynum);
                                    //GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Sale_Item_Equip_Count.RemoveAt(arynum);
                                    Exit();
                                    GUIManager_Total.Instance.m_GUI_Store_Item_Information.Exit();

                                    m_gPanel_Store_Simple_Item_Information.SetActive(false);
                                    break;
                                }
                            }
                        }
                        GUIManager_Total.Instance.Update_Store(GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store);
                        Player_Total.Instance.m_pq_Quest.QuestUpdate_Collect(item);
                        NPCManager_Total.Instance.UpdateNPC();
                    }
                }
                break;
            case E_ITEMSLOT.USE:
                {
                    //Debug.Log(m_nTotalItem_Price);
                    if (Player_Total.Instance.m_pi_Itemslot.m_nGold >= m_nTotalItem_Price)
                    {
                        Player_Total.Instance.m_pi_Itemslot.m_nGold -= m_nTotalItem_Price;
                        Item_Use item = GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Sale_Item_Use_Current[arynum].CreateItem(GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Sale_Item_Use_Current[arynum]);
                        for (int i = 0; i < m_nItemCount_Current; i++)
                        {
                            Destroy(item);
                            if (Player_Total.Instance.m_pi_Itemslot.Get_Item_Use(item) != -1)
                            {
                                if ((GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Sale_Item_Use_Count[arynum] -= 1) > 0) { }
                                else
                                {
                                    GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.Remove_Sale_Item(E_ITEMSLOT.USE, arynum);
                                    //GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Sale_Item_Use_Current.RemoveAt(arynum);
                                    //GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Sale_Item_Use_Count.RemoveAt(arynum);
                                    Exit();
                                    GUIManager_Total.Instance.m_GUI_Store_Item_Information.Exit();

                                    m_gPanel_Store_Simple_Item_Information.SetActive(false);
                                    break;
                                }
                            }
                        }
                        GUIManager_Total.Instance.Update_Store(GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store);
                        Player_Total.Instance.m_pq_Quest.QuestUpdate_Collect(item);
                        NPCManager_Total.Instance.UpdateNPC();
                    }
                }
                break;
            case E_ITEMSLOT.ETC:
                {
                    if (Player_Total.Instance.m_pi_Itemslot.m_nGold >= m_nTotalItem_Price)
                    {
                        Player_Total.Instance.m_pi_Itemslot.m_nGold -= m_nTotalItem_Price;
                        Item_Etc item = GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Sale_Item_Etc_Current[arynum].CreateItem(GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Sale_Item_Etc_Current[arynum]);
                        for (int i = 0; i < m_nItemCount_Current; i++)
                        {
                            Destroy(item);
                            if (Player_Total.Instance.m_pi_Itemslot.Get_Item_Etc(item) != -1)
                            {
                                if ((GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Sale_Item_Etc_Count[arynum] -= 1) > 0) { }
                                else
                                {
                                    GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.Remove_Sale_Item(E_ITEMSLOT.ETC, arynum);
                                    //GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Sale_Item_Etc_Current.RemoveAt(arynum);
                                    //GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Sale_Item_Etc_Count.RemoveAt(arynum);
                                    Exit();
                                    GUIManager_Total.Instance.m_GUI_Store_Item_Information.Exit();

                                    m_gPanel_Store_Simple_Item_Information.SetActive(false);
                                    break;
                                }
                            }
                        }
                        GUIManager_Total.Instance.Update_Store(GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store);
                        Player_Total.Instance.m_pq_Quest.QuestUpdate_Collect(item);
                        NPCManager_Total.Instance.UpdateNPC();
                    }
                }
                break;
        }
    }
    void Press_Btn_Buy(E_ITEMSLOT ei, int arynum)
    {
        switch (ei)
        {
            case E_ITEMSLOT.EQUIP:
                {
                    Item_Equip item = ItemManager.instance.m_Dictionary_MonsterDrop_Equip[Player_Itemslot.m_gary_Itemslot_Equip[arynum].m_nItemCode];
                    //if (Player_Itemslot.m_bary_Itemslot_Equip_Belong[arynum] == false)
                    //{
                    //    Player_Itemslot.m_nary_Itemslot_Equip_Count[arynum] -= m_nItemCount_Current;
                    //    if (Player_Itemslot.m_nary_Itemslot_Equip_Count[arynum] == 0)
                    //    {
                    //        Player_Itemslot.m_gary_Itemslot_Equip[arynum] = null;
                    //        Exit();
                    //        GUIManager_Total.Instance.m_GUI_Store_Item_Information.Exit();
                    //    }

                    //    Player_Total.Instance.m_pi_Itemslot.m_nGold += m_nTotalItem_Price;
                    //}
                    //else
                    //{
                    //    GUIManager_Total.Instance.Display_GUI_Quickslot_Information("해당 아이템은 퀵슬롯에 등록중이라 판매가 불가능합니다.");
                    //}
                    Player_Itemslot.m_nary_Itemslot_Equip_Count[arynum] -= m_nItemCount_Current;
                    if (Player_Itemslot.m_nary_Itemslot_Equip_Count[arynum] == 0)
                    {
                        Player_Itemslot.m_gary_Itemslot_Equip[arynum] = null;
                        Exit();
                        GUIManager_Total.Instance.m_GUI_Store_Item_Information.Exit();
                    }

                    Player_Total.Instance.m_pi_Itemslot.m_nGold += m_nTotalItem_Price;
                    m_gPanel_Store_Simple_Item_Information.SetActive(false);
                    GUIManager_Total.Instance.Update_Store(GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store);
                    Player_Total.Instance.m_pq_Quest.QuestUpdate_Collect(item);
                    NPCManager_Total.Instance.UpdateNPC();
                }
                break;
            case E_ITEMSLOT.USE:
                {
                    Item_Use item = ItemManager.instance.m_Dictionary_MonsterDrop_Use[Player_Itemslot.m_gary_Itemslot_Use[arynum].m_nItemCode];
                    Player_Itemslot.m_nary_Itemslot_Use_Count[arynum] -= m_nItemCount_Current;
                    if (Player_Itemslot.m_nary_Itemslot_Use_Count[arynum] == 0)
                    {
                        Player_Itemslot.m_gary_Itemslot_Use[arynum] = null;
                        Exit();
                        GUIManager_Total.Instance.m_GUI_Store_Item_Information.Exit();
                    }

                    Player_Total.Instance.m_pi_Itemslot.m_nGold += m_nTotalItem_Price;
                    m_gPanel_Store_Simple_Item_Information.SetActive(false);
                    GUIManager_Total.Instance.Update_Store(GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store);
                    Player_Total.Instance.m_pq_Quest.QuestUpdate_Collect(item);
                    NPCManager_Total.Instance.UpdateNPC();
                }
                break;
            case E_ITEMSLOT.ETC:
                {
                    Item_Etc item = ItemManager.instance.m_Dictionary_MonsterDrop_Etc[Player_Itemslot.m_gary_Itemslot_Etc[arynum].m_nItemCode];
                    Player_Itemslot.m_nary_Itemslot_Etc_Count[arynum] -= m_nItemCount_Current;
                    if (Player_Itemslot.m_nary_Itemslot_Etc_Count[arynum] == 0)
                    {
                        Player_Itemslot.m_gary_Itemslot_Etc[arynum] = null;
                        Exit();
                        GUIManager_Total.Instance.m_GUI_Store_Item_Information.Exit();
                    }

                    Player_Total.Instance.m_pi_Itemslot.m_nGold += m_nTotalItem_Price;
                    m_gPanel_Store_Simple_Item_Information.SetActive(false);
                    GUIManager_Total.Instance.Update_Store(GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store);
                    Player_Total.Instance.m_pq_Quest.QuestUpdate_Collect(item);
                    NPCManager_Total.Instance.UpdateNPC();
                }
                break;
        }

        GUIManager_Total.Instance.Update_Quickslot();
    }

    public void Display_GUI_Store_Simple_Item_Information(E_STORESLOT_EYPE ese, E_ITEMSLOT ei, int arynum, Vector3 pos)
    {
        UpdateStoreSimpleItemInformation(ese, ei, arynum);

        m_gPanel_Store_Simple_Item_Information.transform.SetAsLastSibling();
        m_gPanel_Store_Simple_Item_Information.SetActive(true);
    }

    public void UpdateStoreSimpleItemInformation(E_STORESLOT_EYPE ese, E_ITEMSLOT ei, int arynum)
    {
        m_bExit_BuyList = false;

        if (ese == E_STORESLOT_EYPE.SALE)
        {
            m_TMP_Store_Simple_Item_Information_Content_Trade.text = "구매";

            switch (ei)
            {
                case E_ITEMSLOT.EQUIP:
                    {
                        m_nItemCount_Min_Current = m_nItemCount_Min;
                        m_nItemCount_Max_Current = GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Sale_Item_Equip_Count[arynum];
                        m_nItemPrice_Fixed = GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Sale_Item_Equip_Current[arynum].m_nPrice;
                        m_nItemPrice = GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Sale_Item_Equip_Price[arynum];
                        m_BTN_Store_Simple_Item_Information_Content_Trade.onClick.RemoveAllListeners();
                        m_BTN_Store_Simple_Item_Information_Content_Trade.onClick.AddListener(delegate { Press_Btn_Sale(E_ITEMSLOT.EQUIP, arynum); });
                    }
                    break;
                case E_ITEMSLOT.USE:
                    {
                        m_nItemCount_Min_Current = m_nItemCount_Min;
                        m_nItemCount_Max_Current = GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Sale_Item_Use_Count[arynum];
                        m_nItemPrice_Fixed = GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Sale_Item_Use_Current[arynum].m_nPrice;
                        m_nItemPrice = GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Sale_Item_Use_Price[arynum];
                        m_BTN_Store_Simple_Item_Information_Content_Trade.onClick.RemoveAllListeners();
                        m_BTN_Store_Simple_Item_Information_Content_Trade.onClick.AddListener(delegate { Press_Btn_Sale(E_ITEMSLOT.USE, arynum); });


                    }
                    break;
                case E_ITEMSLOT.ETC:
                    {
                        m_nItemCount_Min_Current = m_nItemCount_Min;
                        m_nItemCount_Max_Current = GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Sale_Item_Etc_Count[arynum];
                        m_nItemPrice_Fixed = GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Sale_Item_Etc_Current[arynum].m_nPrice;
                        m_nItemPrice = GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Sale_Item_Etc_Price[arynum];
                        m_BTN_Store_Simple_Item_Information_Content_Trade.onClick.RemoveAllListeners();
                        m_BTN_Store_Simple_Item_Information_Content_Trade.onClick.AddListener(delegate { Press_Btn_Sale(E_ITEMSLOT.ETC, arynum); });


                    }
                    break;
            }

            if (ei == E_ITEMSLOT.EQUIP)
            {
                m_nItemCount_Current = 1;
                m_TMP_Store_Simple_Item_Information_Content_Content_Count.text = m_nItemCount_Current.ToString();
                m_nTotalItem_Price = m_nItemCount_Current * m_nItemPrice;
                m_TMP_store_Simple_Item_Information_Content_Content_Price.text = m_nTotalItem_Price.ToString() + "골드";
                m_nTotalItem_Price = m_nItemCount_Current * m_nItemPrice_Fixed;
                m_TMP_store_Simple_Item_Information_Content_Content_Price.text += " (정가: " + m_nTotalItem_Price.ToString() + "골드)";
                m_nTotalItem_Price = m_nItemCount_Current * m_nItemPrice;
            }
            else
            {
                m_nItemCount_Current = m_nItemCount_Max_Current;
                m_TMP_Store_Simple_Item_Information_Content_Content_Count.text = m_nItemCount_Current.ToString();
                m_nTotalItem_Price = m_nItemCount_Current * m_nItemPrice;
                m_TMP_store_Simple_Item_Information_Content_Content_Price.text = m_nTotalItem_Price.ToString() + "골드";
                m_nTotalItem_Price = m_nItemCount_Current * m_nItemPrice_Fixed;
                m_TMP_store_Simple_Item_Information_Content_Content_Price.text += " (정가: " + m_nTotalItem_Price.ToString() + "골드)";
                m_nTotalItem_Price = m_nItemCount_Current * m_nItemPrice;
            }
        }
        else if (ese == E_STORESLOT_EYPE.BUY)
        {
            m_TMP_Store_Simple_Item_Information_Content_Trade.text = "판매";

            switch (ei)
            {
                case E_ITEMSLOT.EQUIP:
                    {
                        m_nItemCount_Min_Current = m_nItemCount_Min;
                        m_nItemCount_Max_Current = m_nItemCount_Min;
                        m_nItemCode = Player_Itemslot.m_gary_Itemslot_Equip[arynum].m_nItemCode;
                        m_nItemPrice_Fixed = ItemManager.instance.m_Dictionary_MonsterDrop_Equip[m_nItemCode].m_nPrice;
                        m_nItemPrice = m_nItemPrice_Fixed;
                        for (int i = 0; i < GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Buy_Item.Count; i++)
                        {
                            if (m_nItemCode == GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Buy_Item[i].m_nItemCode)
                            {
                                m_nItemPrice = GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Buy_Item_Price[i];
                                m_bExit_BuyList = true;
                                break;
                            }
                        }
                        if (m_bExit_BuyList == false)
                        {
                            m_nItemPrice = (int)((float)m_nItemPrice_Fixed * GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_fBuy_Item_Equip_Value);
                        }
                        m_BTN_Store_Simple_Item_Information_Content_Trade.onClick.RemoveAllListeners();
                        m_BTN_Store_Simple_Item_Information_Content_Trade.onClick.AddListener(delegate { Press_Btn_Buy(E_ITEMSLOT.EQUIP, arynum); });
                    }
                    break;
                case E_ITEMSLOT.USE:
                    {
                        m_nItemCount_Min_Current = m_nItemCount_Min;
                        m_nItemCount_Max_Current = Player_Itemslot.m_nary_Itemslot_Use_Count[arynum];
                        m_nItemCode = Player_Itemslot.m_gary_Itemslot_Use[arynum].m_nItemCode;
                        m_nItemPrice_Fixed = ItemManager.instance.m_Dictionary_MonsterDrop_Use[m_nItemCode].m_nPrice;
                        m_nItemPrice = m_nItemPrice_Fixed;
                        for (int i = 0; i < GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Buy_Item.Count; i++)
                        {
                            if (m_nItemCode == GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Buy_Item[i].m_nItemCode)
                            {
                                m_nItemPrice = GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Buy_Item_Price[i];
                                m_bExit_BuyList = true;
                                break;
                            }
                        }
                        if (m_bExit_BuyList == false)
                        {
                            m_nItemPrice = (int)((float)m_nItemPrice_Fixed * GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_fBuy_Item_Use_Value);
                        }
                        m_BTN_Store_Simple_Item_Information_Content_Trade.onClick.RemoveAllListeners();
                        m_BTN_Store_Simple_Item_Information_Content_Trade.onClick.AddListener(delegate { Press_Btn_Buy(E_ITEMSLOT.USE, arynum); });
                    }
                    break;
                case E_ITEMSLOT.ETC:
                    {
                        m_nItemCount_Min_Current = m_nItemCount_Min;
                        m_nItemCount_Max_Current = Player_Itemslot.m_nary_Itemslot_Etc_Count[arynum];
                        m_nItemCode = Player_Itemslot.m_gary_Itemslot_Etc[arynum].m_nItemCode;
                        m_nItemPrice_Fixed = ItemManager.instance.m_Dictionary_MonsterDrop_Etc[m_nItemCode].m_nPrice;
                        m_nItemPrice = m_nItemPrice_Fixed;
                        for (int i = 0; i < GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Buy_Item.Count; i++)
                        {
                            if (m_nItemCode == GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Buy_Item[i].m_nItemCode)
                            {
                                m_nItemPrice = GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_List_Buy_Item_Price[i];
                                m_bExit_BuyList = true;
                                break;
                            }
                        }
                        if (m_bExit_BuyList == false)
                        {
                            m_nItemPrice = (int)((float)m_nItemPrice_Fixed * GUIManager_Total.Instance.m_GUI_Store.m_NPC_Store.m_fBuy_Item_Etc_Value);
                        }
                        m_BTN_Store_Simple_Item_Information_Content_Trade.onClick.RemoveAllListeners();
                        m_BTN_Store_Simple_Item_Information_Content_Trade.onClick.AddListener(delegate { Press_Btn_Buy(E_ITEMSLOT.ETC, arynum); });
                    }
                    break;
            }
            m_nItemCount_Current = m_nItemCount_Max_Current;
            m_TMP_Store_Simple_Item_Information_Content_Content_Count.text = m_nItemCount_Current.ToString();
            m_nTotalItem_Price = m_nItemCount_Current * m_nItemPrice;
            m_TMP_store_Simple_Item_Information_Content_Content_Price.text = m_nTotalItem_Price.ToString() + "골드";
            m_nTotalItem_Price = m_nItemCount_Current * m_nItemPrice_Fixed;
            m_TMP_store_Simple_Item_Information_Content_Content_Price.text += " (정가: " + m_nTotalItem_Price.ToString() + "골드)";
            m_nTotalItem_Price = m_nItemCount_Current * m_nItemPrice;

            //m_nItemCount_Current = m_nItemCount_Max_Current;
            //m_TMP_Store_Simple_Item_Information_Content_Content_Count.text = m_nItemCount_Current.ToString();
            //m_nTotalItem_Price = m_nItemCount_Current * m_nItemPrice;
            //m_TMP_store_Simple_Item_Information_Content_Content_Price.text = m_nTotalItem_Price.ToString() + "골드";
            //m_nTotalItem_Price = m_nItemCount_Current * m_nItemPrice_Fixed;
            //m_TMP_store_Simple_Item_Information_Content_Content_Price.text += " (정가: " + m_nTotalItem_Price.ToString() + "골드)";
        }

        Set_Button(arynum);
    }

    public void Exit()
    {
        Initializing();
    }

    public void Initializing()
    {
        m_BTN_Store_Simple_Item_Information_Content_Content_Count_Up.onClick.RemoveAllListeners();
        m_BTN_Store_Simple_Item_Information_Content_Content_Count_Down.onClick.RemoveAllListeners();
        m_BTN_Store_Simple_Item_Information_Content_Trade.onClick.RemoveAllListeners();

        m_gPanel_Store_Simple_Item_Information.SetActive(false);
    }

    public void PositionSet(Vector3 pos)
    {
        //m_gPanel_Store_Simple_Item_Information.transform.position = pos;
    }
}
