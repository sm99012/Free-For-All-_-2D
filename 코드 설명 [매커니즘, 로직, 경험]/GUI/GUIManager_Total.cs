using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager_Total : MonoBehaviour
{
    private static GUIManager_Total instance = null;
    public static GUIManager_Total Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    public List<int> m_nList_UI_Priority;

    public GUI_Player m_GUI_Player;
    public GUI_Log m_GUI_Log;
    public GUI_Interaction m_GUI_Interaction;
    public GUI_Quest m_GUI_Quest;
    //public GUI_Quest_Information m_GUI_Quest_Information;
    public GUI_QuestStateInfo m_GUI_QuestStateInfo;
    public GUI_QuestReward m_GUI_QuestReward;
    public GUI_Itemslot m_GUI_Itemslot;
    public GUI_Itemslot_Equip_Information m_GUI_Itemslot_Equip_Information;
    public GUI_Itemslot_Use_Information m_GUI_Itemslot_Use_Information;
    public GUI_Itemslot_Etc_Information m_GUI_Itemslot_Etc_Information;
    public GUI_Equipslot m_GUI_Equipslot;
    public GUI_Equipslot_Equip_Information m_GUI_Equipslot_Equip_Information;
    public GUI_Equipslot_Remove_Information m_GUI_Equipslot_Remove_Information;
    public GUI_Status m_GUI_Status;
    public GUI_Soc m_GUI_Soc;
    public GUI_UpBar m_GUI_UpBar;
    public GUI_ChangeMap m_GUI_ChangeMap;
    public GUI_Reinforcement m_GUI_Reinforcement;
    public GUI_Reinforcement_Equip_Information m_GUI_Reinforcement_Equip_Information;
    public GUI_Reinforcement_Check m_GUI_Reinforcement_Check;
    public GUI_Store m_GUI_Store;
    public GUI_Store_Simple_Item_Information m_GUI_Store_Simple_Item_Information;
    public GUI_Store_Item_Information m_GUI_Store_Item_Information;
    public GUI_Store_Exit_Information m_GUI_Store_Exit_Information;
    public GUI_Info m_GUI_Info;
    public GUI_Scene m_GUI_Scene;
    public GUI_MonsterDictionary m_GUI_MonsterDictionary;
    public GUI_Quickslot m_GUI_Quickslot;
    public GUI_Quickslot_Signup m_GUI_Quickslot_Signup;
    public GUI_Quickslot_Signdown m_GUI_Quickslot_Signdown;
    public GUI_Quickslot_Information m_GUI_Quickslot_Information;
    public GUI_ReTry m_GUI_ReTry;
    public GUI_ReTry_Information m_GUI_ReTry_Information;
    public GUI_Gift_GetItem_Info m_GUI_Gift_GetItem_Info;
    public GUI_Option m_GUI_Option;
    public GUI_ItemManager m_GUI_ItemManager;
    public GUI_BossInformation m_GUI_BossInformation;
    public GUI_BossBattleInformation m_GUI_BossBattleInformation;

    public int m_nGUISize_Width = 682;
    public int m_nGUISize_Height = 512;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public bool InitialSet()
    {
        m_GUI_Player = this.gameObject.GetComponent<GUI_Player>();
        m_GUI_Log = this.gameObject.GetComponent<GUI_Log>();
        m_GUI_Interaction = this.gameObject.GetComponent<GUI_Interaction>();
        m_GUI_Quest = this.gameObject.GetComponent<GUI_Quest>();
        //m_GUI_Quest_Information = this.gameObject.GetComponent<GUI_Quest_Information>();
        m_GUI_QuestStateInfo = this.gameObject.GetComponent<GUI_QuestStateInfo>();
        m_GUI_Itemslot = this.gameObject.GetComponent<GUI_Itemslot>();
        m_GUI_Itemslot_Equip_Information = this.gameObject.GetComponent<GUI_Itemslot_Equip_Information>();
        m_GUI_Itemslot_Use_Information = this.gameObject.GetComponent<GUI_Itemslot_Use_Information>();
        m_GUI_Itemslot_Etc_Information = this.gameObject.GetComponent<GUI_Itemslot_Etc_Information>();
        m_GUI_Equipslot = this.gameObject.GetComponent<GUI_Equipslot>();
        m_GUI_Equipslot_Equip_Information = this.gameObject.GetComponent<GUI_Equipslot_Equip_Information>();
        m_GUI_Equipslot_Remove_Information = this.gameObject.GetComponent<GUI_Equipslot_Remove_Information>();
        m_GUI_Status = this.gameObject.GetComponent<GUI_Status>();
        m_GUI_Soc = this.gameObject.GetComponent<GUI_Soc>();
        m_GUI_UpBar = this.gameObject.GetComponent<GUI_UpBar>();
        m_GUI_ChangeMap = this.gameObject.GetComponent<GUI_ChangeMap>();
        m_GUI_Reinforcement = this.gameObject.GetComponent<GUI_Reinforcement>();
        m_GUI_Reinforcement_Equip_Information = this.gameObject.GetComponent<GUI_Reinforcement_Equip_Information>();
        m_GUI_Reinforcement_Check = this.gameObject.GetComponent<GUI_Reinforcement_Check>();
        m_GUI_Store = this.gameObject.GetComponent<GUI_Store>();
        m_GUI_Store_Simple_Item_Information = this.gameObject.GetComponent<GUI_Store_Simple_Item_Information>();
        m_GUI_Store_Item_Information = this.gameObject.GetComponent<GUI_Store_Item_Information>();
        m_GUI_Store_Exit_Information = this.gameObject.GetComponent<GUI_Store_Exit_Information>();
        m_GUI_QuestReward = this.gameObject.GetComponent<GUI_QuestReward>();
        m_GUI_Info = this.gameObject.GetComponent<GUI_Info>();
        m_GUI_Scene = this.gameObject.GetComponent<GUI_Scene>();
        m_GUI_MonsterDictionary = this.gameObject.GetComponent<GUI_MonsterDictionary>();
        m_GUI_Quickslot = this.gameObject.GetComponent<GUI_Quickslot>();
        m_GUI_Quickslot_Signup = this.gameObject.GetComponent<GUI_Quickslot_Signup>();
        m_GUI_Quickslot_Signdown = this.gameObject.GetComponent<GUI_Quickslot_Signdown>();
        m_GUI_Quickslot_Information = this.gameObject.GetComponent<GUI_Quickslot_Information>();
        m_GUI_ReTry = this.gameObject.GetComponent<GUI_ReTry>();
        m_GUI_ReTry_Information = this.gameObject.GetComponent<GUI_ReTry_Information>();
        m_GUI_Gift_GetItem_Info = this.gameObject.GetComponent<GUI_Gift_GetItem_Info>();
        m_GUI_Option = this.gameObject.GetComponent<GUI_Option>();
        m_GUI_ItemManager = this.gameObject.GetComponent<GUI_ItemManager>();
        m_GUI_BossInformation = this.gameObject.GetComponent<GUI_BossInformation>();
        m_GUI_BossBattleInformation = this.gameObject.GetComponent<GUI_BossBattleInformation>();

        m_GUI_Player.InitialSet();
        m_GUI_Log.InitialSet();
        m_GUI_Interaction.InitialSet();
        m_GUI_Quest.InitialSet();
        //m_GUI_Quest_Information.InitialSet();
        m_GUI_QuestStateInfo.InitialSet();
        m_GUI_Itemslot.InitialSet();
        m_GUI_Itemslot_Equip_Information.InitialSet();
        m_GUI_Itemslot_Use_Information.InitialSet();
        m_GUI_Itemslot_Etc_Information.InitialSet();
        m_GUI_Equipslot.InitialSet();
        m_GUI_Equipslot_Equip_Information.InitialSet();
        m_GUI_Equipslot_Remove_Information.InitialSet();
        m_GUI_Status.InitialSet();
        m_GUI_Soc.InitialSet();
        m_GUI_UpBar.InitialSet();
        m_GUI_ChangeMap.InitialSet();
        m_GUI_Reinforcement.InitialSet();
        m_GUI_Reinforcement_Equip_Information.InitialSet();
        m_GUI_Reinforcement_Check.InitialSet();
        m_GUI_Store.InitialSet();
        m_GUI_Store_Simple_Item_Information.InitialSet();
        m_GUI_Store_Item_Information.InitialSet();
        m_GUI_Store_Exit_Information.InitialSet();
        m_GUI_QuestReward.InitialSet();
        m_GUI_Info.InitialSet();
        m_GUI_Scene.InitialSet();
        m_GUI_MonsterDictionary.InitialSet();
        m_GUI_Quickslot.InitialSet();
        m_GUI_Quickslot_Signup.InitialSet();
        m_GUI_Quickslot_Signdown.InitialSet();
        m_GUI_Quickslot_Information.InitialSet();
        m_GUI_ReTry.InitialSet();
        m_GUI_ReTry_Information.InitialSet();
        m_GUI_Gift_GetItem_Info.InitialSet();
        m_GUI_Option.InitialSet();
        m_GUI_ItemManager.InitialSet();
        m_GUI_BossInformation.InitialSet();
        m_GUI_BossBattleInformation.InitialSet();

        m_nList_UI_Priority = new List<int>();

        return true;
    }

    //----------------------------------------------------------------------------------------

    public void UpdateLog(string str)
    {
        m_GUI_Log.UpdateLog(str);
    }

    public void Interaction(NPC_Total npc)
    {
        m_GUI_Interaction.Interaction(npc);
        Fix_GUI_Priority(3);
    }

    public void ControlGUI_Interaction_Exit()
    {
        m_GUI_Interaction.Exit();    
    }

    public int m_nDisplay_GUI_Quest_Number;
    // GUI_Quest 관련 함수
    public void Display_GUI_Quest()
    {
        if (m_GUI_Quest.Display_GUI_Quest() == true)
            Fix_GUI_Priority(4);
        else
            Delete_GUI_Priority(4);

        if (m_nDisplay_GUI_Quest_Number == 0)
        {
            m_GUI_Quest.UpdateQuest_SceneChange();
        }
        m_nDisplay_GUI_Quest_Number++;
    }

    // GUI_Itemslot 관련 함수
    public void Display_GUI_Itemslot()
    {
        if (m_GUI_Itemslot.Display_GUI_Itemslot() == true)
            Fix_GUI_Priority(8);
        else
            Delete_GUI_Priority(8);
    }

    // Not Use
    public void Display_GUI_Equipslot()
    {
        m_GUI_Equipslot.Display_GUI_Equipslot();
    }
    // Not Use
    public void Display_GUI_Status()
    {
        m_GUI_Status.Display_GUI_Status();
    }
    // Use
    public void Display_GUI_ES()
    {
        if (m_GUI_Equipslot.Display_GUI_Equipslot() == true)
            Fix_GUI_Priority(16);
        else
            Delete_GUI_Priority(16);

        m_GUI_Status.Display_GUI_Status();
    }

    public void Display_GUI_Reinforcement(int arynumber = -1, Item_Use item_reinforcement = null)
    {
        if (m_GUI_Reinforcement.Display_GUI_Reinforcement(arynumber, item_reinforcement) == true)
            Fix_GUI_Priority(20);
        else
            Delete_GUI_Priority(20);
    }

    public void Display_GUI_Reinforcement_Check(bool bl)
    {
        m_GUI_Reinforcement_Check.Display_GUI_Reinforcement_Check(bl);
    }

    public void Display_GUI_Store(NPC_Store store)
    {
        m_GUI_Store.Display_GUI_Store(store);
        Fix_GUI_Priority(23);
    }

    public void Display_GUI_Store_Simple_Item_Information(E_STORESLOT_EYPE ese, E_ITEMSLOT ei, int arynum, Vector3 pos)
    {
        m_GUI_Store_Simple_Item_Information.Display_GUI_Store_Simple_Item_Information(ese, ei, arynum, pos);
    }

    public void Display_GUI_Store_Item_Information(Item_Equip item)
    {
        m_GUI_Store_Item_Information.Display_GUI_Store_Item_Information(item);
    }
    public void Display_GUI_Store_Item_Information(Item_Use item)
    {
        m_GUI_Store_Item_Information.Display_GUI_Store_Item_Information(item);
    }
    public void Display_GUI_Store_Item_Information(Item_Etc item)
    {
        m_GUI_Store_Item_Information.Display_GUI_Store_Item_Information(item);
    }

    public void Display_GUI_Store_Exit_Information()
    {
        m_GUI_Store_Exit_Information.Display();
        Delete_GUI_Priority(23);
    }

    public void Display_GUI_QuestReward(Quest quest, int n = 0)
    {
        m_GUI_QuestReward.Display_QuestReward(quest, n);
    }

    public void Display_GUI_Info()
    {
        if (m_GUI_Info.Display_GUI_Info() == true)
            Fix_GUI_Priority(27);
        else
            Delete_GUI_Priority(27);
    }

    public void Display_GUI_ItemManager()
    {
        m_GUI_ItemManager.Display_GUI_ItemManager();
    }

    public void Display_GUI_Scene(int scenenumber)
    {
        m_GUI_Scene.Display_Scene(scenenumber);
    }

    public void Display_GUI_QuestStateInfo(Quest quest)
    {
        string str;
        var strary = quest.m_sQuest_Title.Split('\n');
        str = strary[0] + strary[1] + "\n완료 가능!";

        m_GUI_QuestStateInfo.Disaply_GUI_QuestStateInfo(str);
    }
    public void UnDisplay_GUI_QuestStateInfo(Quest quest)
    {
        string str;
        var strary = quest.m_sQuest_Title.Split('\n');
        str = strary[0] + strary[1] + "\n완료 가능!";

        m_GUI_QuestStateInfo.UnDisaply_GUI_QuestStateInfo(str);
    }

    public void Display_GUI_Dictionary()
    {
        if (m_GUI_MonsterDictionary.Display_GUI_MonsterDictionary() == true)
            Fix_GUI_Priority(29);
        else
            Delete_GUI_Priority(29);
    }

    public void Display_GUI_Quickslot_Signup(E_QUICKSLOT_CATEGORY eqc, int number)
    {
        m_GUI_Quickslot_Signup.Display_GUI_Quickslot_Signup(eqc, number);
    }

    public void Display_GUI_Quickslot_Signdown(int number)
    {
        m_GUI_Quickslot_Signdown.Display_GUI_Quickslot_Signdown(number);
    }

    public void Display_GUI_Quickslot_Information(string str)
    {
        m_GUI_Quickslot_Information.Display_GUI_Quickslot_Information(str);
    }

    public void Display_GUI_ReTry(string deathname)
    {
        m_GUI_ReTry.Display_GUI_ReTry(deathname);
    }

    public void Display_GUI_ReTry_Information(Dictionary<int, int> dictionary_item_equip, Dictionary<int, int> dictionary_item_use, Dictionary<int, int> dictionary_item_etc, int lostgold)
    {
        StartCoroutine(Process_Update_ChangeMap_ReTry(dictionary_item_equip, dictionary_item_use, dictionary_item_etc, lostgold));
        Update_ChangeMap_ReTry();
    }
    IEnumerator Process_Update_ChangeMap_ReTry(Dictionary<int, int> dictionary_item_equip, Dictionary<int, int> dictionary_item_use, Dictionary<int, int> dictionary_item_etc, int lostgold)
    {
        yield return new WaitForSeconds(1.5f);

        Player_Total.Instance.ReTry_Success();

        Update_MapName(Player_Total.Instance.m_pm_Map.m_Map);
        Update_SS();
        Update_Itemslot();

        m_GUI_ReTry_Information.Display_GUI_ReTry_Information(dictionary_item_equip, dictionary_item_use, dictionary_item_etc, lostgold);
    }

    public void Display_GUI_Gift_GetItem_Info(Dictionary<int,int> dictionary_equip, Dictionary<int, int> dictionary_equip_count,
                                                Dictionary<int, int> dictionary_use, Dictionary<int, int> dictionary_use_count, 
                                                Dictionary<int, int> dictionary_etc, Dictionary<int, int> dictionary_etc_count)
    {
        m_GUI_Gift_GetItem_Info.Display_GUI_Gift_GetItem_Info(dictionary_equip, dictionary_equip_count,
                                                                dictionary_use, dictionary_use_count,
                                                                dictionary_etc, dictionary_etc_count);
    }

    public void Display_GUI_Option()
    {
        if (m_GUI_Option.Display_GUI_Option() == true)
            Fix_GUI_Priority(37);
        else
            Delete_GUI_Priority(37);
    }

    public void Display_GUI_BossInformation(string bossname, int bosslv)
    {
        m_GUI_BossInformation.Display_GUI_BossInformation(bossname, bosslv);
        //Fix_GUI_Priority(39);
    }
    public void UnDisplay_GUI_BossInformation()
    {
        m_GUI_BossInformation.UnDisplay_GUI_BossInformation();
        //Delete_GUI_Priority(39);
    }

    public void Display_GUI_BossBattleInformation(BossBattleDictionary bbd, Vector2 pos)
    {
        m_GUI_BossBattleInformation.Display_GUI_BossBattleInformation(bbd, pos);
        Fix_GUI_Priority(39);
    }

    //----------------------------------------------------------------------------------------

    // GUI_UpBar 설정.
    public void Update_MapName(Map map)
    {
        m_GUI_UpBar.Set_MapName(map.GetMapName());
    }

    // GUI_Status, GUI_Soc 설정.
    public void Update_SS()
    {
        m_GUI_Status.UpdateStatus();
        m_GUI_Soc.UpdateSoc();
    }

    // GUI_Itemslot 업데이트.
    public void Update_Itemslot()
    {
        m_GUI_Itemslot.UpdateItemslot();
        m_GUI_Itemslot.UpdateItemslot_Gold();
    }

    // GUI_Itemslot_Equip_Information 업데이트.
    public void Update_Itemslot_Equip_Information(Item_Equip item, int arynumber)
    {
        m_GUI_Itemslot_Equip_Information.UpdateItemEquipInformation(item, arynumber);
    }

    // GUI_Itemslot_Use_Information 업데이트.
    public void Update_Itemslot_Use_Information(Item_Use item, int arynumber)
    {
        m_GUI_Itemslot_Use_Information.UpdateItemUseInformation(item, arynumber);
    }

    // GUI_Itemslot_Etc_Information 업데이트.
    public void Update_Itemslot_Etc_Information(Item_Etc item, int arynumber)
    {
        m_GUI_Itemslot_Etc_Information.UpdateItemEtcInformation(item, arynumber);
    }

    // GUI_Equip 업데이트.
    public void Update_Equipslot()
    {
        m_GUI_Equipslot.UpdateEquipslot();
    }

    // GUI_Equipslot_Equip_Information 업데이트.
    public void Update_Equipslot_Equip_Information(Item_Equip item, int arynumber)
    {
        m_GUI_Equipslot_Equip_Information.UpdateItemEquipInformation(item, arynumber);
    }

    // GUI_Quest 업데이트.
    public void Update_Quest(Quest_KILL_MONSTER quest, int number)
    {
        m_GUI_Quest.UpdateQuest(quest, number);
    }
    public void Update_Quest(Quest_KILL_TYPE quest, int number)
    {
        m_GUI_Quest.UpdateQuest(quest, number);
    }
    public void Update_Quest(Quest_GOAWAY_MONSTER quest, int number)
    {
        m_GUI_Quest.UpdateQuest(quest, number);
    }
    public void Update_Quest(Quest_GOAWAY_TYPE quest, int number)
    {
        m_GUI_Quest.UpdateQuest(quest, number);
    }
    public void Update_Quest(Quest_COLLECT quest, int number)
    {
        m_GUI_Quest.UpdateQuest(quest, number);
    }
    public void Update_Quest(Quest_CONVERSATION quest, int number)
    {
        m_GUI_Quest.UpdateQuest(quest, number);
    }
    public void Update_Quest(Quest_ROLL quest, int number)
    {
        m_GUI_Quest.UpdateQuest(quest, number);
    }
    public void Update_Quest(Quest_ELIMINATE_MONSTER quest, int number)
    {
        m_GUI_Quest.UpdateQuest(quest, number);
    }
    public void Update_Quest(Quest_ELIMINATE_TYPE quest, int number)
    {
        m_GUI_Quest.UpdateQuest(quest, number);
    }

    // GUI_Quest_Information 업데이트.
    public void Update_Quest_Information(Quest_KILL_MONSTER quest, int number)
    {
        m_GUI_Quest.UpdateQuestInformation(quest, number);
    }
    public void Update_Quest_Information(Quest_KILL_TYPE quest, int number)
    {
        m_GUI_Quest.UpdateQuestInformation(quest, number);
    }
    public void Update_Quest_Information(Quest_GOAWAY_MONSTER quest, int number)
    {
        m_GUI_Quest.UpdateQuestInformation(quest, number);
    }
    public void Update_Quest_Information(Quest_GOAWAY_TYPE quest, int number)
    {
        m_GUI_Quest.UpdateQuestInformation(quest, number);
    }
    public void Update_Quest_Information(Quest_COLLECT quest, int number)
    {
        m_GUI_Quest.UpdateQuestInformation(quest, number);
    }
    public void Update_Quest_Information(Quest_CONVERSATION quest, int number)
    {
        m_GUI_Quest.UpdateQuestInformation(quest, number);
    }
    public void Update_Quest_Information(Quest_ROLL quest, int number)
    {
        m_GUI_Quest.UpdateQuestInformation(quest, number);
    }
    public void Update_Quest_Information(Quest_ELIMINATE_MONSTER quest, int number)
    {
        m_GUI_Quest.UpdateQuestInformation(quest, number);
    }
    public void Update_Quest_Information(Quest_ELIMINATE_TYPE quest, int number)
    {
        m_GUI_Quest.UpdateQuestInformation(quest, number);
    }

    // GUI_ChangeMap 업데이트.
    // 맵 이동시 Fadein, Fadeout 효과.
    public void Update_ChangeMap()
    {
        m_GUI_ChangeMap.Fade();

        Update_MapName(Player_Total.Instance.m_pm_Map.m_Map);
        Update_SS();
        Update_Itemslot();
    }

    // 리트라이시 Fadein, Fadeout 효과.
    public void Update_ChangeMap_ReTry()
    {
        m_GUI_ChangeMap.Fade();
    }

    // GUI_Reinforcement_Equip_Information 업데이트.
    public void Update_Reinforcement_Equip_Information(Item_Equip item, int arynumber_equip, int arynumber_use)
    {
        m_GUI_Reinforcement_Equip_Information.UpdateItemReinforcementInformation(item, arynumber_equip, arynumber_use);
    }

    // GUI_Store 업데이트.
    public void Update_Store(NPC_Store store)
    {
        m_GUI_Store.UpdateStore(store);
    }

    // GUI_MonsterDictionary 업데이트.
    public void Update_MonsterDictionary_Info(int monstercode)
    {
        m_GUI_MonsterDictionary.Update_MonsterDictionary_Info(monstercode);
    }

    // GUI_Quickslot 업데이트.
    public void Update_Quickslot()
    {
        m_GUI_Quickslot.Update_Quickslot();
    }
    // GUI_Quickslot 업데이트. - 아무 장비도 장착하지 않을때 퀵슬롯에 등록된 장비를 장착할 시 퀵슬롯 삭제. 

    public void Update_Quickslot_Equip(int arynumber)
    {
        m_GUI_Quickslot.Update_Quickslot_Equip(arynumber);
    }
    // GUI_BossInformation 업데이트. - 체력 상태 표시.
    public void Update_BossInformation(int bosshp_max, int bosshp_min)
    {
        m_GUI_BossInformation.Update_BossInformation(bosshp_max, bosshp_min);
    }
    // GUI_Store_Item_Information 업데이트. - 상점 갱신까지의 시간 표시.
    public void Update_StoreTime(float ftime)
    {
        m_GUI_Store_Item_Information.UpdateStoreTime(ftime);
    }

    // -------------------------------------------------------------------------------------------

    public void Sparkle_GUI_UpBar_Dictionary_ON(E_MONSTER_KIND emk, int monstercode, int releaseratio)
    {
        m_GUI_UpBar.Sparkle_GUI_Dictionary_ON();
        m_GUI_MonsterDictionary.Add_MonsterDictionary_Sparkle(emk, monstercode, releaseratio);
    }
    public void Sparkle_GUI_UpBar_Dictionary_OFF()
    {
        m_GUI_UpBar.Sparkle_GUI_Dictionary_OFF();
    }

    // -------------------------------------------------------------------------------------------

    // 퀵슬롯에 아이템 등록.
    public void Set_Quickslot(int quickslotarynumber, E_QUICKSLOT_CATEGORY eqc, int number)
    {
        m_GUI_Quickslot.Set_Quickslot(quickslotarynumber, eqc, number);
    }

    // -------------------------------------------------------------------------------------------

    // ESC 키입력을 이용한 GUI 닫기.
    int m_nPriorityNumber_High = -1;
    public bool Exit_GUI_Priority()
    {
        if (m_nList_UI_Priority.Count > 0)
        {
            switch(m_nPriorityNumber_High = m_nList_UI_Priority[0])
            {
                case 3:
                    {
                        // GUI_Interaction
                        ControlGUI_Interaction_Exit();
                    } break;
                case 4:
                    {
                        // GUI_Quest
                        Display_GUI_Quest();
                    }
                    break;
                case 8:
                    {
                        // GUI_Itemslot
                        Display_GUI_Itemslot();
                    }
                    break;
                //case 9:
                //    {
                //        // GUI_Itemslot_Equip_Information

                //    }
                //    break;
                //case 10:
                //    {
                //        // GUI_Itemslot_Use_Information

                //    }
                //    break;
                //case 11:
                //    {
                //        // GUI_Itemslot_Etc_Information

                //    }
                //    break;
                //case 13:
                //    {
                //        // GUI_Equipslot_Equip_Information

                //    }
                //    break;
                case 16:
                    {
                        // GUI_ES
                        Display_GUI_ES();
                    }
                    break;
                case 20:
                    {
                        // GUI_Reinforcement
                        Display_GUI_Reinforcement();
                    }
                    break;
                case 23:
                    {
                        // GUI_Store
                        m_GUI_Store.Press_Btn_Exit();
                        Delete_GUI_Priority(23);
                    }
                    break;
                case 27:
                    {
                        // GUI_Info
                        Display_GUI_Info();
                    }
                    break;
                case 29:
                    {
                        // GUI_MonsterDictionary
                        Display_GUI_Dictionary();
                    }
                    break;
                case 37:
                    {
                        // GUI_Option
                        Display_GUI_Option();
                    }
                    break;
                case 39:
                    {
                        // GUI_BossBattleInformation
                        m_GUI_BossBattleInformation.Press_Btn_Exit();
                    }
                    break;
            }
            //m_nList_UI_Priority.RemoveAt(0);

            return true;
        }
        else
        {
            return false;
        }
    }
    // GUI 우선순위 추가 및 변경.
    public void Fix_GUI_Priority(int guinumber)
    {
        if (m_nList_UI_Priority.Contains(guinumber) == true)
        {
            m_nList_UI_Priority.RemoveAt(m_nList_UI_Priority.IndexOf(guinumber));
        }
        else { }

        m_nList_UI_Priority.Insert(0, guinumber);
    }
    // GUI 우선순위 삭제.
    public void Delete_GUI_Priority(int guinumber)
    {
        if (m_nList_UI_Priority.Contains(guinumber) == true)
        {
            m_nList_UI_Priority.RemoveAt(m_nList_UI_Priority.IndexOf(guinumber));
        }
    }

    // -------------------------------------------------------------------------------------------

    // NPC와 상호작용 중일때 키입력 동작.
    public int Interaction_In_SSD(KeyCode keycode)
    {
        return m_GUI_Interaction.Interaction_In_SSD(keycode);
    }
}
