using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager m_DataManager = null;
    public static DataManager instance
    {
        get
        {
            if (m_DataManager == null)
            {
                return null;
            }
            return m_DataManager;
        }
    }

    private void Awake()
    {
        if (m_DataManager == null)
        {
            m_DataManager = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        if (m_Coroutine_Save == null)
            m_Coroutine_Save = StartCoroutine(Process_Save(60));

        if (Input.GetKey(KeyCode.F5))
        {
            if (Input.GetKeyUp(KeyCode.Keypad9))
            {
                //SaveData();

                //Save_Player_Quest();
                //Player_Total.Instance.m_ps_Status.m_sStatus.P_OperatorSTATUS_LV(+1);

                //Save_Player_Quickslot();
            }
            if (Input.GetKeyUp(KeyCode.Keypad8))
            {
                //Load_Player_Dictionary_Monster();
                //Player_Total.Instance.m_ps_Status.m_sStatus.P_OperatorSTATUS_LV(-1);

                //Load_Player_Quickslot();
            }
        }

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    GUIManager_Total.Instance.m_GUI_Quest.InitialSort();
        //}
    }

    //string m_sDepot_C = "C:/Free For All/GameData/";
    string m_sDepot_C = "GameData/";
    string m_sDepot_Local = "GameData/";

    static Coroutine m_Coroutine_Save = null;
    IEnumerator Process_Save(float ftime)
    {
        yield return new WaitForSeconds(ftime);
        SaveData();
        m_Coroutine_Save = null;
    }
    public void GameStart()
    {
        Load_Player_Map();
        Total_Manager.Instance.GameStart(scenename, scenecode, playerposition, mapcode);
    }
    public void SaveData()
    {
        if (Total_Manager.Instance.m_bStart == true)
        {
            Debug.Log("1분 경과.");
            if (Player_Total.Instance.m_pm_Move.m_ePlayerMoveState != Player_Move.E_PLAYER_MOVE_STATE.DEATH && 
                BossManager.Instance.m_eBattle_Boss_State == E_BATTLE_BOSS_STATE.NULL)
            {
                Save_Player_Status();
                Save_Player_Itemslot();
                Save_Player_Equipslot();
                Save_Player_Quest();
                Save_Player_Map();
                Save_Player_Dictionary_Monster();
                Save_Player_Quickslot();
                GUIManager_Total.Instance.UpdateLog("자동 저장 완료.");
            }
        }
    }
    public void LoadData()
    {
        Load_Player_Status();
        Load_Player_Itemslot();
        Load_Player_Equipslot();
        Load_Player_Quest();
        Load_Player_Map();
        Load_Player_Dictionary_Monster();
        Load_Player_Quickslot();
        Load_Player_Status(1);


    }


    TextAsset textasset;
    string strtoken;
    string[] itemdata;
    // ----------------------------------------------------------------------------------------------------------------------------------------------------------
    // Player Item Data 저장,불러오기 관련.
    Item_Equip itemequip;
    Item_Use itemuse;
    Item_Etc itemetc;
    STATUS additionalstatus;
    STATUS reinforcementstatus;
    SOC additionalsoc;
    SOC reinforcementsoc;

    // Player_Itemslot Data Set. ---------------------------------------------------------------------------------------------------------------------------------
    // Player Item Data 불러오기.
    public bool Load_Player_Itemslot()
    {
        if (Load_ItemNumber() == false)
            return false;
        if (Load_Player_Itemslot_Item_Equip() == false)
            return false;
        if (Load_Player_Itemslot_Item_Use() == false)
            return false;
        if (Load_Player_Itemslot_Item_Etc() == false)
            return false;
        if (Load_Player_Itemslot_Gold() == false)
            return false;

        GUIManager_Total.Instance.Update_Itemslot();
        return true;
    }
    // (itemcode,itemnumber,itemcount,additionalstatus,additionalsoc,reinforcement_current,reinforcementstatus,reinforcementsoc)
    // status[17]: lv,exp_m,exp_c,hp_m,hp_c,mp_m,mp_c,damage_t,damage_p,damage_m,criticalrate,criticaldamage,spd,def_p,def_m,evasionrate,atkspd;
    // soc[9]: honer,human,animal,slime,skeleton,ent,devil,dragon,shadow
    bool Load_Player_Itemslot_Item_Equip()
    {
        //textasset = Resources.Load<TextAsset>("GameData/Player_Itemslot_Item_Equip");
        //StringReader sr = new StringReader(textasset.text);
        //StreamReader sr = new StreamReader("./GameData/Player_Itemslot_Item_Equip.csv");
        //StreamReader sr = new StreamReader("C:/Users/USER/AppData/LocalLow/Free For All/GameData/Player_Itemslot_Item_Equip.csv");
        StreamReader sr = new StreamReader(m_sDepot_C + "Player_Itemslot_Item_Equip.csv");

        strtoken = "";

        if (!sr.EndOfStream)
        {
            strtoken = sr.ReadLine();
            strtoken = sr.ReadLine();
            strtoken = sr.ReadLine();
            for (int i = 0; i < 60; i++)
            {
                strtoken = sr.ReadLine();
                itemdata = strtoken.Split(',');
                if (int.Parse(itemdata[2]) == 1)
                {
                    additionalstatus = new STATUS(int.Parse(itemdata[3]), int.Parse(itemdata[4]), int.Parse(itemdata[5]),
                        int.Parse(itemdata[6]), int.Parse(itemdata[7]), int.Parse(itemdata[8]), int.Parse(itemdata[9]),
                        int.Parse(itemdata[10]), int.Parse(itemdata[11]), int.Parse(itemdata[12]), int.Parse(itemdata[13]),
                        int.Parse(itemdata[14]), int.Parse(itemdata[15]), int.Parse(itemdata[16]), int.Parse(itemdata[17]),
                        int.Parse(itemdata[18]), float.Parse(itemdata[19]));
                    additionalsoc = new SOC(int.Parse(itemdata[20]), int.Parse(itemdata[21]), int.Parse(itemdata[22]),
                        int.Parse(itemdata[23]), int.Parse(itemdata[24]), int.Parse(itemdata[25]), int.Parse(itemdata[26]),
                        int.Parse(itemdata[27]), int.Parse(itemdata[28]));
                    reinforcementstatus = new STATUS(int.Parse(itemdata[30]), int.Parse(itemdata[31]), int.Parse(itemdata[32]),
                        int.Parse(itemdata[33]), int.Parse(itemdata[34]), int.Parse(itemdata[35]), int.Parse(itemdata[36]),
                        int.Parse(itemdata[37]), int.Parse(itemdata[38]), int.Parse(itemdata[39]), int.Parse(itemdata[40]),
                        int.Parse(itemdata[41]), int.Parse(itemdata[42]), int.Parse(itemdata[43]), int.Parse(itemdata[44]),
                        int.Parse(itemdata[45]), float.Parse(itemdata[46]));
                    reinforcementsoc = new SOC(int.Parse(itemdata[47]), int.Parse(itemdata[48]), int.Parse(itemdata[49]),
                        int.Parse(itemdata[50]), int.Parse(itemdata[51]), int.Parse(itemdata[52]), int.Parse(itemdata[53]),
                        int.Parse(itemdata[54]), int.Parse(itemdata[55]));
                    itemequip = ItemManager.instance.m_Dictionary_MonsterDrop_Equip[int.Parse(itemdata[0])].LoadItem(int.Parse(itemdata[0]), int.Parse(itemdata[1]),
                        additionalstatus, additionalsoc,
                        int.Parse(itemdata[29]), reinforcementstatus, reinforcementsoc);
                    Player_Itemslot.m_gary_Itemslot_Equip[i] = itemequip;
                    Player_Itemslot.m_nary_Itemslot_Equip_Count[i] = int.Parse(itemdata[2]);
                }
                else
                    continue;
            }
        }

        //Debug.Log("[플레이어가 소유한 장비 아이템 데이터 로드 완료.]");
        return true;
    }
    // (itemcode,itemcount)
    bool Load_Player_Itemslot_Item_Use()
    {
        //textasset = Resources.Load<TextAsset>("GameData/Player_Itemslot_Item_Use");
        //StringReader sr = new StringReader(textasset.text);
        //StreamReader sr = new StreamReader("./GameData/Player_Itemslot_Item_Use.csv");
        //StreamReader sr = new StreamReader("C:/Users/USER/AppData/LocalLow/Free For All/GameData/Player_Itemslot_Item_Use.csv");
        StreamReader sr = new StreamReader(m_sDepot_C + "Player_Itemslot_Item_Use.csv");

        strtoken = "";

        if (!sr.EndOfStream)
        {
            strtoken = sr.ReadLine();
            strtoken = sr.ReadLine();
            strtoken = sr.ReadLine();
            for (int i = 0; i < 60; i++)
            {
                strtoken = sr.ReadLine();
                itemdata = strtoken.Split(',');
                if (int.Parse(itemdata[1]) > 0)
                {
                    itemuse = ItemManager.instance.m_Dictionary_MonsterDrop_Use[int.Parse(itemdata[0])].LoadItem(int.Parse(itemdata[0]));
                    Player_Itemslot.m_gary_Itemslot_Use[i] = itemuse;
                    Player_Itemslot.m_nary_Itemslot_Use_Count[i] = int.Parse(itemdata[1]);
                }
                else
                    continue;
            }
        }

        //Debug.Log("[플레이어가 소유한 소비 아이템 데이터 로드 완료.]");
        return true;
    }
    // (itemcode,itemcount)
    bool Load_Player_Itemslot_Item_Etc()
    {
        //textasset = Resources.Load<TextAsset>("GameData/Player_Itemslot_Item_Etc");
        ///StringReader sr = new StringReader(textasset.text);
        //StreamReader sr = new StreamReader("./GameData/Player_Itemslot_Item_Etc.csv");
        //StreamReader sr = new StreamReader("C:/Users/USER/AppData/LocalLow/Free For All/GameData/Player_Itemslot_Item_Etc.csv");
        StreamReader sr = new StreamReader(m_sDepot_C + "Player_Itemslot_Item_Etc.csv");


        strtoken = "";

        if (!sr.EndOfStream)
        {
            strtoken = sr.ReadLine();
            strtoken = sr.ReadLine();
            strtoken = sr.ReadLine();
            for (int i = 0; i < 60; i++)
            {
                strtoken = sr.ReadLine();
                itemdata = strtoken.Split(',');
                if (int.Parse(itemdata[1]) > 0)
                {
                    itemetc = ItemManager.instance.m_Dictionary_MonsterDrop_Etc[int.Parse(itemdata[0])].LoadItem(int.Parse(itemdata[0]));
                    Player_Itemslot.m_gary_Itemslot_Etc[i] = itemetc;
                    Player_Itemslot.m_nary_Itemslot_Etc_Count[i] = int.Parse(itemdata[1]);
                }
                else
                    continue;
            }
        }

        //Debug.Log("[플레이어가 소유한 기타 아이템 데이터 로드 완료.]");
        return true;
    }
    // [gold]
    bool Load_Player_Itemslot_Gold()
    {
        //StreamReader sr = new StreamReader("C:/Users/USER/AppData/LocalLow/Free For All/GameData/Player_Itemslot_Gold.csv");
        StreamReader sr = new StreamReader(m_sDepot_C + "Player_Itemslot_Gold.csv");

        strtoken = "";

        if (!sr.EndOfStream)
        {
            strtoken = sr.ReadLine();
            strtoken = sr.ReadLine();
            strtoken = sr.ReadLine();

            strtoken = sr.ReadLine();
            itemdata = strtoken.Split(',');
            Player_Total.Instance.m_pi_Itemslot.m_nGold = int.Parse(itemdata[0]);
        }

        //Debug.Log("[플레이어가 소유한 골드 데이터 로드 완료.]");
        return true;
    }
    // (itemnumber)
    bool Load_ItemNumber()
    {
        //StreamReader sr = new StreamReader("C:/Users/USER/AppData/LocalLow/Free For All/GameData/Player_Itemslot_Gold.csv");
        StreamReader sr = new StreamReader(m_sDepot_C + "ItemNumber.csv");

        strtoken = "";

        if (!sr.EndOfStream)
        {
            strtoken = sr.ReadLine();
            strtoken = sr.ReadLine();

            strtoken = sr.ReadLine();
            itemdata = strtoken.Split(',');
            ItemManager.sm_nItemNumber = int.Parse(itemdata[0]);
        }

        //Debug.Log("[플레이어가 소유한 골드 데이터 로드 완료.]");
        return true;
    }

    // Player Item Data 저장.
    public bool Save_Player_Itemslot()
    {
        if (Save_ItemNumber() == false)
            return false;
        if (Save_Player_Itemslot_Item_Equip() == false)
            return false;
        if (Save_Player_Itemslot_Item_Use() == false)
            return false;
        if (Save_Player_Itemslot_Item_Etc() == false)
            return false;
        if (Save_Player_Itemslot_Gold() == false)
            return false;

        return true;
    }
    // (itemcode,itemnumber,itemcount,additionalstatus,additionalsoc,reinforcement_current,reinforcementstatus,reinforcementsoc)
    // status[17]: lv,exp_m,exp_c,hp_m,hp_c,mp_m,mp_c,damage_t,damage_p,damage_m,criticalrate,criticaldamage,spd,def_p,def_m,evasionrate,atkspd;
    // soc[9]: honer,human,animal,slime,skeleton,ent,devil,dragon,shadow
    bool Save_Player_Itemslot_Item_Equip()
    {
        //StreamWriter sw = new StreamWriter("./GameData/Player_Itemslot_Item_Equip.csv");
        //StreamWriter sw = new StreamWriter("C:/Users/USER/AppData/LocalLow/Free For All/GameData/Player_Itemslot_Item_Equip.csv");
        StreamWriter sw = new StreamWriter(m_sDepot_C + "Player_Itemslot_Item_Equip.csv");


        sw.WriteLine("[Player_Itemslot_Item_Equip]");
        sw.WriteLine("[itemcode][itemnumber][itemcount][additionalstatus][additionalsoc][reinforcement_current][reinforcementstatus][reinforcementsoc]");
        sw.WriteLine("0,1 ,2 ,3 ,4 ,5 ,6 ,7 ,8 ,9 ,10 ,11 ,12 ,13 ,14 ,15 ,16 ,17 ,18 ,19 ,20 ,21 ,22 ,23 ,24 ,25 ,26 ,27 ,28 ,29 ,30 ,31 ,32 ,33 ,34 ,35 ,36 ,37 ,38 ,39 ,40 ,41 ,42 ,43 ,44 ,45 ,46 ,47 ,48 ,49 ,50 ,51 ,52 ,53 ,54 ,55");

        strtoken = "";

        for (int i = 0; i < 60; i++)
        {
            if (Player_Itemslot.m_nary_Itemslot_Equip_Count[i] == 0)
            {
                strtoken = "0,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0";
            }
            else if (Player_Itemslot.m_nary_Itemslot_Equip_Count[i] > 0)
            {
                // [itemcode][itemnumber][itemcount]
                strtoken = Player_Itemslot.m_gary_Itemslot_Equip[i].m_nItemCode.ToString() + "," + Player_Itemslot.m_gary_Itemslot_Equip[i].m_nItemNumber.ToString() + "," + Player_Itemslot.m_nary_Itemslot_Equip_Count[i].ToString();
                // [additionalstatus][additionalsoc]
                strtoken += "," + Player_Itemslot.m_gary_Itemslot_Equip[i].m_STATUS_AdditionalOption.GetSTATUS_Data();
                strtoken += "," + Player_Itemslot.m_gary_Itemslot_Equip[i].m_SOC_AdditionalOption.GetSOC_Data();
                // [reinforcement_current][reinforcementstatus][reinforcementsoc]
                strtoken += "," + Player_Itemslot.m_gary_Itemslot_Equip[i].m_nReinforcementCount_Current.ToString();
                strtoken += "," + Player_Itemslot.m_gary_Itemslot_Equip[i].m_STATUS_ReinforcementValue.GetSTATUS_Data();
                strtoken += "," + Player_Itemslot.m_gary_Itemslot_Equip[i].m_SOC_ReinforcementValue.GetSOC_Data();
            }
            else
            {
                strtoken = "ERROR";
            }

            sw.WriteLine(strtoken);
        }
        sw.Flush();
        sw.Close();

        //Debug.Log("[플레이어가 소유한 장비 아이템 데이터 저장 완료.]");
        return true;
    }
    // (itemcode,itemcount)
    bool Save_Player_Itemslot_Item_Use()
    {
        //StreamWriter sw = new StreamWriter("C:/Users/USER/AppData/LocalLow/Free For All/GameData/Player_Itemslot_Item_Use.csv");
        StreamWriter sw = new StreamWriter(m_sDepot_C + "Player_Itemslot_Item_Use.csv");


        sw.WriteLine("[Player_Itemslot_Item_Use]");
        sw.WriteLine("[itemcode][itemcount]");
        sw.WriteLine("0,1");

        strtoken = "";

        for (int i = 0; i < 60; i++)
        {
            if (Player_Itemslot.m_nary_Itemslot_Use_Count[i] == 0)
            {
                strtoken = "0,0,0,";
            }
            else if (Player_Itemslot.m_nary_Itemslot_Use_Count[i] > 0)
            {
                strtoken = Player_Itemslot.m_gary_Itemslot_Use[i].m_nItemCode.ToString() + "," + Player_Itemslot.m_nary_Itemslot_Use_Count[i].ToString();
            }
            else
            {
                strtoken = "ERROR";
            }

            sw.WriteLine(strtoken);
        }
        sw.Flush();
        sw.Close();

        //Debug.Log("[플레이어가 소유한 소비 아이템 데이터 저장 완료.]");
        return true;
    }
    // (itemcode,itemcount)
    bool Save_Player_Itemslot_Item_Etc()
    {
        //StreamWriter sw = new StreamWriter("C:/Users/USER/AppData/LocalLow/Free For All/GameData/Player_Itemslot_Item_Etc.csv");
        StreamWriter sw = new StreamWriter(m_sDepot_C + "Player_Itemslot_Item_Etc.csv");


        sw.WriteLine("[Player_Itemslot_Item_Equip]");
        sw.WriteLine("[itemcode][itemcount]");
        sw.WriteLine("0,1");

        strtoken = "";

        for (int i = 0; i < 60; i++)
        {
            if (Player_Itemslot.m_nary_Itemslot_Etc_Count[i] == 0)
            {
                strtoken = "0,0";
            }
            else if (Player_Itemslot.m_nary_Itemslot_Etc_Count[i] > 0)
            {
                strtoken = Player_Itemslot.m_gary_Itemslot_Etc[i].m_nItemCode.ToString() + "," + Player_Itemslot.m_nary_Itemslot_Etc_Count[i].ToString();
            }
            else
            {
                strtoken = "ERROR";
            }

            sw.WriteLine(strtoken);
        }
        sw.Flush();
        sw.Close();

        //Debug.Log("[플레이어가 소유한 기타 아이템 데이터 저장 완료.]");
        return true;
    }
    // [gold]
    bool Save_Player_Itemslot_Gold()
    {
        //StreamWriter sw = new StreamWriter("C:/Users/USER/AppData/LocalLow/Free For All/GameData/Player_Itemslot_Gold.csv");
        StreamWriter sw = new StreamWriter(m_sDepot_C + "Player_Itemslot_Gold.csv");


        sw.WriteLine("[Player_Itemslot_Gold]");
        sw.WriteLine("[gold]");
        sw.WriteLine("0");

        strtoken = "";

        strtoken += Player_Total.Instance.m_pi_Itemslot.m_nGold.ToString();

        sw.WriteLine(strtoken);

        sw.Flush();
        sw.Close();

        //Debug.Log("[플레이어가 소유한 골드 데이터 저장 완료.]");
        return true;
    }
    // (itemnumber)
    bool Save_ItemNumber()
    {
        StreamWriter sw = new StreamWriter(m_sDepot_C + "ItemNumber.csv");


        sw.WriteLine("[ItemNumber]");
        sw.WriteLine("0");

        strtoken = "";

        strtoken += ItemManager.sm_nItemNumber.ToString();

        sw.WriteLine(strtoken);

        sw.Flush();
        sw.Close();

        //Debug.Log("[플레이어가 소유한 골드 데이터 저장 완료.]");
        return true;
    }
    // -----------------------------------------------------------------------------------------------------------------------------------------------------------



    // Player_Equipslot Data Set. --------------------------------------------------------------------------------------------------------------------------------
    // Player Item Data 불러오기.
    public bool Load_Player_Equipslot()
    {
        //textasset = Resources.Load<TextAsset>("GameData/Player_Equipslot_Item_Equip");
        //StringReader sr = new StringReader(textasset.text);
        //StreamReader sr = new StreamReader("./GameData/Player_Equipslot_Item_Equip.csv");
        //StreamReader sr = new StreamReader("C:/Users/USER/AppData/LocalLow/Free For All/GameData/Player_Equipslot_Item_Equip.csv");
        StreamReader sr = new StreamReader(m_sDepot_C + "Player_Equipslot_Item_Equip.csv");


        strtoken = "";

        if (!sr.EndOfStream)
        {
            strtoken = sr.ReadLine();
            strtoken = sr.ReadLine();
            strtoken = sr.ReadLine();

            // Player_Equipment.Hat
            strtoken = sr.ReadLine();
            itemdata = strtoken.Split(',');
            if (int.Parse(itemdata[2]) == 1)
            {
                additionalstatus = new STATUS(int.Parse(itemdata[3]), int.Parse(itemdata[4]), int.Parse(itemdata[5]),
                    int.Parse(itemdata[6]), int.Parse(itemdata[7]), int.Parse(itemdata[8]), int.Parse(itemdata[9]),
                    int.Parse(itemdata[10]), int.Parse(itemdata[11]), int.Parse(itemdata[12]), int.Parse(itemdata[13]),
                    int.Parse(itemdata[14]), int.Parse(itemdata[15]), int.Parse(itemdata[16]), int.Parse(itemdata[17]),
                    int.Parse(itemdata[18]), float.Parse(itemdata[19]));
                additionalsoc = new SOC(int.Parse(itemdata[20]), int.Parse(itemdata[21]), int.Parse(itemdata[22]),
                    int.Parse(itemdata[23]), int.Parse(itemdata[24]), int.Parse(itemdata[25]), int.Parse(itemdata[26]),
                    int.Parse(itemdata[27]), int.Parse(itemdata[28]));
                reinforcementstatus = new STATUS(int.Parse(itemdata[30]), int.Parse(itemdata[31]), int.Parse(itemdata[32]),
                    int.Parse(itemdata[33]), int.Parse(itemdata[34]), int.Parse(itemdata[35]), int.Parse(itemdata[36]),
                    int.Parse(itemdata[37]), int.Parse(itemdata[38]), int.Parse(itemdata[39]), int.Parse(itemdata[40]),
                    int.Parse(itemdata[41]), int.Parse(itemdata[42]), int.Parse(itemdata[43]), int.Parse(itemdata[44]),
                    int.Parse(itemdata[45]), float.Parse(itemdata[46]));
                reinforcementsoc = new SOC(int.Parse(itemdata[47]), int.Parse(itemdata[48]), int.Parse(itemdata[49]),
                    int.Parse(itemdata[50]), int.Parse(itemdata[51]), int.Parse(itemdata[52]), int.Parse(itemdata[53]),
                    int.Parse(itemdata[54]), int.Parse(itemdata[55]));
                itemequip = ItemManager.instance.m_Dictionary_MonsterDrop_Equip[int.Parse(itemdata[0])].LoadItem(int.Parse(itemdata[0]), int.Parse(itemdata[1]),
                    additionalstatus, additionalsoc,
                    int.Parse(itemdata[29]), reinforcementstatus, reinforcementsoc);
                Player_Equipment.m_gEquipment_Hat = itemequip;
                Player_Equipment.m_bEquipment_Hat = true;
            }

            // Player_Equipment.Top
            strtoken = sr.ReadLine();
            itemdata = strtoken.Split(',');
            if (int.Parse(itemdata[2]) == 1)
            {
                additionalstatus = new STATUS(int.Parse(itemdata[3]), int.Parse(itemdata[4]), int.Parse(itemdata[5]),
                    int.Parse(itemdata[6]), int.Parse(itemdata[7]), int.Parse(itemdata[8]), int.Parse(itemdata[9]),
                    int.Parse(itemdata[10]), int.Parse(itemdata[11]), int.Parse(itemdata[12]), int.Parse(itemdata[13]),
                    int.Parse(itemdata[14]), int.Parse(itemdata[15]), int.Parse(itemdata[16]), int.Parse(itemdata[17]),
                    int.Parse(itemdata[18]), float.Parse(itemdata[19]));
                additionalsoc = new SOC(int.Parse(itemdata[20]), int.Parse(itemdata[21]), int.Parse(itemdata[22]),
                    int.Parse(itemdata[23]), int.Parse(itemdata[24]), int.Parse(itemdata[25]), int.Parse(itemdata[26]),
                    int.Parse(itemdata[27]), int.Parse(itemdata[28]));
                reinforcementstatus = new STATUS(int.Parse(itemdata[30]), int.Parse(itemdata[31]), int.Parse(itemdata[32]),
                    int.Parse(itemdata[33]), int.Parse(itemdata[34]), int.Parse(itemdata[35]), int.Parse(itemdata[36]),
                    int.Parse(itemdata[37]), int.Parse(itemdata[38]), int.Parse(itemdata[39]), int.Parse(itemdata[40]),
                    int.Parse(itemdata[41]), int.Parse(itemdata[42]), int.Parse(itemdata[43]), int.Parse(itemdata[44]),
                    int.Parse(itemdata[45]), float.Parse(itemdata[46]));
                reinforcementsoc = new SOC(int.Parse(itemdata[47]), int.Parse(itemdata[48]), int.Parse(itemdata[49]),
                    int.Parse(itemdata[50]), int.Parse(itemdata[51]), int.Parse(itemdata[52]), int.Parse(itemdata[53]),
                    int.Parse(itemdata[54]), int.Parse(itemdata[55]));
                itemequip = ItemManager.instance.m_Dictionary_MonsterDrop_Equip[int.Parse(itemdata[0])].LoadItem(int.Parse(itemdata[0]), int.Parse(itemdata[1]),
                    additionalstatus, additionalsoc,
                    int.Parse(itemdata[29]), reinforcementstatus, reinforcementsoc);
                Player_Equipment.m_gEquipment_Top = itemequip;
                Player_Equipment.m_bEquipment_Top = true;
            }

            // Player_Equipment.Bottoms
            strtoken = sr.ReadLine();
            itemdata = strtoken.Split(',');
            if (int.Parse(itemdata[2]) == 1)
            {
                additionalstatus = new STATUS(int.Parse(itemdata[3]), int.Parse(itemdata[4]), int.Parse(itemdata[5]),
                    int.Parse(itemdata[6]), int.Parse(itemdata[7]), int.Parse(itemdata[8]), int.Parse(itemdata[9]),
                    int.Parse(itemdata[10]), int.Parse(itemdata[11]), int.Parse(itemdata[12]), int.Parse(itemdata[13]),
                    int.Parse(itemdata[14]), int.Parse(itemdata[15]), int.Parse(itemdata[16]), int.Parse(itemdata[17]),
                    int.Parse(itemdata[18]), float.Parse(itemdata[19]));
                additionalsoc = new SOC(int.Parse(itemdata[20]), int.Parse(itemdata[21]), int.Parse(itemdata[22]),
                    int.Parse(itemdata[23]), int.Parse(itemdata[24]), int.Parse(itemdata[25]), int.Parse(itemdata[26]),
                    int.Parse(itemdata[27]), int.Parse(itemdata[28]));
                reinforcementstatus = new STATUS(int.Parse(itemdata[30]), int.Parse(itemdata[31]), int.Parse(itemdata[32]),
                    int.Parse(itemdata[33]), int.Parse(itemdata[34]), int.Parse(itemdata[35]), int.Parse(itemdata[36]),
                    int.Parse(itemdata[37]), int.Parse(itemdata[38]), int.Parse(itemdata[39]), int.Parse(itemdata[40]),
                    int.Parse(itemdata[41]), int.Parse(itemdata[42]), int.Parse(itemdata[43]), int.Parse(itemdata[44]),
                    int.Parse(itemdata[45]), float.Parse(itemdata[46]));
                reinforcementsoc = new SOC(int.Parse(itemdata[47]), int.Parse(itemdata[48]), int.Parse(itemdata[49]),
                    int.Parse(itemdata[50]), int.Parse(itemdata[51]), int.Parse(itemdata[52]), int.Parse(itemdata[53]),
                    int.Parse(itemdata[54]), int.Parse(itemdata[55]));
                itemequip = ItemManager.instance.m_Dictionary_MonsterDrop_Equip[int.Parse(itemdata[0])].LoadItem(int.Parse(itemdata[0]), int.Parse(itemdata[1]),
                    additionalstatus, additionalsoc,
                    int.Parse(itemdata[29]), reinforcementstatus, reinforcementsoc);
                Player_Equipment.m_gEquipment_Bottoms = itemequip;
                Player_Equipment.m_bEquipment_Bottoms = true;
            }

            // Player_Equipment.Shose
            strtoken = sr.ReadLine();
            itemdata = strtoken.Split(',');
            if (int.Parse(itemdata[2]) == 1)
            {
                additionalstatus = new STATUS(int.Parse(itemdata[3]), int.Parse(itemdata[4]), int.Parse(itemdata[5]),
                    int.Parse(itemdata[6]), int.Parse(itemdata[7]), int.Parse(itemdata[8]), int.Parse(itemdata[9]),
                    int.Parse(itemdata[10]), int.Parse(itemdata[11]), int.Parse(itemdata[12]), int.Parse(itemdata[13]),
                    int.Parse(itemdata[14]), int.Parse(itemdata[15]), int.Parse(itemdata[16]), int.Parse(itemdata[17]),
                    int.Parse(itemdata[18]), float.Parse(itemdata[19]));
                additionalsoc = new SOC(int.Parse(itemdata[20]), int.Parse(itemdata[21]), int.Parse(itemdata[22]),
                    int.Parse(itemdata[23]), int.Parse(itemdata[24]), int.Parse(itemdata[25]), int.Parse(itemdata[26]),
                    int.Parse(itemdata[27]), int.Parse(itemdata[28]));
                reinforcementstatus = new STATUS(int.Parse(itemdata[30]), int.Parse(itemdata[31]), int.Parse(itemdata[32]),
                    int.Parse(itemdata[33]), int.Parse(itemdata[34]), int.Parse(itemdata[35]), int.Parse(itemdata[36]),
                    int.Parse(itemdata[37]), int.Parse(itemdata[38]), int.Parse(itemdata[39]), int.Parse(itemdata[40]),
                    int.Parse(itemdata[41]), int.Parse(itemdata[42]), int.Parse(itemdata[43]), int.Parse(itemdata[44]),
                    int.Parse(itemdata[45]), float.Parse(itemdata[46]));
                reinforcementsoc = new SOC(int.Parse(itemdata[47]), int.Parse(itemdata[48]), int.Parse(itemdata[49]),
                    int.Parse(itemdata[50]), int.Parse(itemdata[51]), int.Parse(itemdata[52]), int.Parse(itemdata[53]),
                    int.Parse(itemdata[54]), int.Parse(itemdata[55]));
                itemequip = ItemManager.instance.m_Dictionary_MonsterDrop_Equip[int.Parse(itemdata[0])].LoadItem(int.Parse(itemdata[0]), int.Parse(itemdata[1]),
                    additionalstatus, additionalsoc,
                    int.Parse(itemdata[29]), reinforcementstatus, reinforcementsoc);
                Player_Equipment.m_gEquipment_Shose = itemequip;
                Player_Equipment.m_bEquipment_Shose = true;
            }

            // Player_Equipment.Gloves
            strtoken = sr.ReadLine();
            itemdata = strtoken.Split(',');
            if (int.Parse(itemdata[2]) == 1)
            {
                additionalstatus = new STATUS(int.Parse(itemdata[3]), int.Parse(itemdata[4]), int.Parse(itemdata[5]),
                    int.Parse(itemdata[6]), int.Parse(itemdata[7]), int.Parse(itemdata[8]), int.Parse(itemdata[9]),
                    int.Parse(itemdata[10]), int.Parse(itemdata[11]), int.Parse(itemdata[12]), int.Parse(itemdata[13]),
                    int.Parse(itemdata[14]), int.Parse(itemdata[15]), int.Parse(itemdata[16]), int.Parse(itemdata[17]),
                    int.Parse(itemdata[18]), float.Parse(itemdata[19]));
                additionalsoc = new SOC(int.Parse(itemdata[20]), int.Parse(itemdata[21]), int.Parse(itemdata[22]),
                    int.Parse(itemdata[23]), int.Parse(itemdata[24]), int.Parse(itemdata[25]), int.Parse(itemdata[26]),
                    int.Parse(itemdata[27]), int.Parse(itemdata[28]));
                reinforcementstatus = new STATUS(int.Parse(itemdata[30]), int.Parse(itemdata[31]), int.Parse(itemdata[32]),
                    int.Parse(itemdata[33]), int.Parse(itemdata[34]), int.Parse(itemdata[35]), int.Parse(itemdata[36]),
                    int.Parse(itemdata[37]), int.Parse(itemdata[38]), int.Parse(itemdata[39]), int.Parse(itemdata[40]),
                    int.Parse(itemdata[41]), int.Parse(itemdata[42]), int.Parse(itemdata[43]), int.Parse(itemdata[44]),
                    int.Parse(itemdata[45]), float.Parse(itemdata[46]));
                reinforcementsoc = new SOC(int.Parse(itemdata[47]), int.Parse(itemdata[48]), int.Parse(itemdata[49]),
                    int.Parse(itemdata[50]), int.Parse(itemdata[51]), int.Parse(itemdata[52]), int.Parse(itemdata[53]),
                    int.Parse(itemdata[54]), int.Parse(itemdata[55]));
                itemequip = ItemManager.instance.m_Dictionary_MonsterDrop_Equip[int.Parse(itemdata[0])].LoadItem(int.Parse(itemdata[0]), int.Parse(itemdata[1]),
                    additionalstatus, additionalsoc,
                    int.Parse(itemdata[29]), reinforcementstatus, reinforcementsoc);
                Player_Equipment.m_gEquipment_Gloves = itemequip;
                Player_Equipment.m_bEquipment_Gloves = true;
            }

            // Player_Equipment.Mainweapon
            strtoken = sr.ReadLine();
            itemdata = strtoken.Split(',');
            if (int.Parse(itemdata[2]) == 1)
            {
                additionalstatus = new STATUS(int.Parse(itemdata[3]), int.Parse(itemdata[4]), int.Parse(itemdata[5]),
                    int.Parse(itemdata[6]), int.Parse(itemdata[7]), int.Parse(itemdata[8]), int.Parse(itemdata[9]),
                    int.Parse(itemdata[10]), int.Parse(itemdata[11]), int.Parse(itemdata[12]), int.Parse(itemdata[13]),
                    int.Parse(itemdata[14]), int.Parse(itemdata[15]), int.Parse(itemdata[16]), int.Parse(itemdata[17]),
                    int.Parse(itemdata[18]), float.Parse(itemdata[19]));
                additionalsoc = new SOC(int.Parse(itemdata[20]), int.Parse(itemdata[21]), int.Parse(itemdata[22]),
                    int.Parse(itemdata[23]), int.Parse(itemdata[24]), int.Parse(itemdata[25]), int.Parse(itemdata[26]),
                    int.Parse(itemdata[27]), int.Parse(itemdata[28]));
                reinforcementstatus = new STATUS(int.Parse(itemdata[30]), int.Parse(itemdata[31]), int.Parse(itemdata[32]),
                    int.Parse(itemdata[33]), int.Parse(itemdata[34]), int.Parse(itemdata[35]), int.Parse(itemdata[36]),
                    int.Parse(itemdata[37]), int.Parse(itemdata[38]), int.Parse(itemdata[39]), int.Parse(itemdata[40]),
                    int.Parse(itemdata[41]), int.Parse(itemdata[42]), int.Parse(itemdata[43]), int.Parse(itemdata[44]),
                    int.Parse(itemdata[45]), float.Parse(itemdata[46]));
                reinforcementsoc = new SOC(int.Parse(itemdata[47]), int.Parse(itemdata[48]), int.Parse(itemdata[49]),
                    int.Parse(itemdata[50]), int.Parse(itemdata[51]), int.Parse(itemdata[52]), int.Parse(itemdata[53]),
                    int.Parse(itemdata[54]), int.Parse(itemdata[55]));
                itemequip = ItemManager.instance.m_Dictionary_MonsterDrop_Equip[int.Parse(itemdata[0])].LoadItem(int.Parse(itemdata[0]), int.Parse(itemdata[1]),
                    additionalstatus, additionalsoc,
                    int.Parse(itemdata[29]), reinforcementstatus, reinforcementsoc);
                Player_Equipment.m_gEquipment_Mainweapon = itemequip;
                Player_Equipment.m_bEquipment_Mainweapon = true;

                if (int.Parse(itemdata[0]) >= 1000 && int.Parse(itemdata[0]) < 1300)
                    Player_Total.Instance.m_pm_Move.SetAnimation_Weapon(E_ITEM_EQUIP_MAINWEAPON_TYPE.SWORD);
                else if (int.Parse(itemdata[0]) >= 1300 && int.Parse(itemdata[0]) < 1600)
                    Player_Total.Instance.m_pm_Move.SetAnimation_Weapon(E_ITEM_EQUIP_MAINWEAPON_TYPE.KNIFE);
                else if (int.Parse(itemdata[0]) >= 1600 && int.Parse(itemdata[0]) < 1900)
                    Player_Total.Instance.m_pm_Move.SetAnimation_Weapon(E_ITEM_EQUIP_MAINWEAPON_TYPE.AXE);
                else
                    Player_Total.Instance.m_pm_Move.SetAnimation_Weapon(E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL);

                //Debug.Log(Player_Equipment.m_gEquipment_Mainweapon.m_sItemName);
            }


            // Player_Equipment.Subweapon
            strtoken = sr.ReadLine();
            itemdata = strtoken.Split(',');
            if (int.Parse(itemdata[2]) == 1)
            {
                additionalstatus = new STATUS(int.Parse(itemdata[3]), int.Parse(itemdata[4]), int.Parse(itemdata[5]),
                    int.Parse(itemdata[6]), int.Parse(itemdata[7]), int.Parse(itemdata[8]), int.Parse(itemdata[9]),
                    int.Parse(itemdata[10]), int.Parse(itemdata[11]), int.Parse(itemdata[12]), int.Parse(itemdata[13]),
                    int.Parse(itemdata[14]), int.Parse(itemdata[15]), int.Parse(itemdata[16]), int.Parse(itemdata[17]),
                    int.Parse(itemdata[18]), float.Parse(itemdata[19]));
                additionalsoc = new SOC(int.Parse(itemdata[20]), int.Parse(itemdata[21]), int.Parse(itemdata[22]),
                    int.Parse(itemdata[23]), int.Parse(itemdata[24]), int.Parse(itemdata[25]), int.Parse(itemdata[26]),
                    int.Parse(itemdata[27]), int.Parse(itemdata[28]));
                reinforcementstatus = new STATUS(int.Parse(itemdata[30]), int.Parse(itemdata[31]), int.Parse(itemdata[32]),
                    int.Parse(itemdata[33]), int.Parse(itemdata[34]), int.Parse(itemdata[35]), int.Parse(itemdata[36]),
                    int.Parse(itemdata[37]), int.Parse(itemdata[38]), int.Parse(itemdata[39]), int.Parse(itemdata[40]),
                    int.Parse(itemdata[41]), int.Parse(itemdata[42]), int.Parse(itemdata[43]), int.Parse(itemdata[44]),
                    int.Parse(itemdata[45]), float.Parse(itemdata[46]));
                reinforcementsoc = new SOC(int.Parse(itemdata[47]), int.Parse(itemdata[48]), int.Parse(itemdata[49]),
                    int.Parse(itemdata[50]), int.Parse(itemdata[51]), int.Parse(itemdata[52]), int.Parse(itemdata[53]),
                    int.Parse(itemdata[54]), int.Parse(itemdata[55]));
                itemequip = ItemManager.instance.m_Dictionary_MonsterDrop_Equip[int.Parse(itemdata[0])].LoadItem(int.Parse(itemdata[0]), int.Parse(itemdata[1]),
                    additionalstatus, additionalsoc,
                    int.Parse(itemdata[29]), reinforcementstatus, reinforcementsoc);
                Player_Equipment.m_gEquipment_Subweapon = itemequip;
                Player_Equipment.m_bEquipment_Subweapon = true;
            }
        }

        GUIManager_Total.Instance.Update_Equipslot();
        GUIManager_Total.Instance.Update_SS();

        //Debug.Log("[플레이어가 착용한 장비 아이템 데이터 로드 완료.]");
        return true;
    }

    // Player Item Data 저장.
    public bool Save_Player_Equipslot()
    {
        //StreamWriter sw = new StreamWriter("C:/Users/USER/AppData/LocalLow/Free For All/GameData/Player_Equipslot_Item_Equip.csv");
        StreamWriter sw = new StreamWriter(m_sDepot_C + "Player_Equipslot_Item_Equip.csv");

        sw.WriteLine("[Player_Itemslot_Item_Equip]");
        sw.WriteLine("[itemcode][itemnumber][itemcount][additionalstatus][additionalsoc][reinforcement_current][reinforcementstatus][reinforcementsoc]");
        sw.WriteLine("0,1 ,2 ,3 ,4 ,5 ,6 ,7 ,8 ,9 ,10 ,11 ,12 ,13 ,14 ,15 ,16 ,17 ,18 ,19 ,20 ,21 ,22 ,23 ,24 ,25 ,26 ,27 ,28 ,29 ,30 ,31 ,32 ,33 ,34 ,35 ,36 ,37 ,38 ,39 ,40 ,41 ,42 ,43 ,44 ,45 ,46 ,47 ,48 ,49 ,50 ,51 ,52 ,53 ,54 ,55");

        strtoken = "";

        if (Player_Equipment.m_bEquipment_Hat == true)
        {
            // [itemcode][itemnumber][itemcount]
            strtoken = Player_Equipment.m_gEquipment_Hat.m_nItemCode.ToString() + "," + Player_Equipment.m_gEquipment_Hat.m_nItemNumber.ToString() + "," + "1";
            // [additionalstatus][additionalsoc]
            strtoken += "," + Player_Equipment.m_gEquipment_Hat.m_STATUS_AdditionalOption.GetSTATUS_Data();
            strtoken += "," + Player_Equipment.m_gEquipment_Hat.m_SOC_AdditionalOption.GetSOC_Data();
            // [reinforcement_current][reinforcementstatus][reinforcementsoc]
            strtoken += "," + Player_Equipment.m_gEquipment_Hat.m_nReinforcementCount_Current.ToString();
            strtoken += "," + Player_Equipment.m_gEquipment_Hat.m_STATUS_ReinforcementValue.GetSTATUS_Data();
            strtoken += "," + Player_Equipment.m_gEquipment_Hat.m_SOC_ReinforcementValue.GetSOC_Data();
        }
        else
        {
            strtoken = "0,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0";
        }
        sw.WriteLine(strtoken);

        if (Player_Equipment.m_bEquipment_Top == true)
        {
            // [itemcode][itemnumber][itemcount]
            strtoken = Player_Equipment.m_gEquipment_Top.m_nItemCode.ToString() + "," + Player_Equipment.m_gEquipment_Top.m_nItemNumber.ToString() + "," + "1";
            // [additionalstatus][additionalsoc]
            strtoken += "," + Player_Equipment.m_gEquipment_Top.m_STATUS_AdditionalOption.GetSTATUS_Data();
            strtoken += "," + Player_Equipment.m_gEquipment_Top.m_SOC_AdditionalOption.GetSOC_Data();
            // [reinforcement_current][reinforcementstatus][reinforcementsoc]
            strtoken += "," + Player_Equipment.m_gEquipment_Top.m_nReinforcementCount_Current.ToString();
            strtoken += "," + Player_Equipment.m_gEquipment_Top.m_STATUS_ReinforcementValue.GetSTATUS_Data();
            strtoken += "," + Player_Equipment.m_gEquipment_Top.m_SOC_ReinforcementValue.GetSOC_Data();
        }
        else
        {
            strtoken = "0,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0";
        }
        sw.WriteLine(strtoken);

        if (Player_Equipment.m_bEquipment_Bottoms == true)
        {
            // [itemcode][itemnumber][itemcount]
            strtoken = Player_Equipment.m_gEquipment_Bottoms.m_nItemCode.ToString() + "," + Player_Equipment.m_gEquipment_Bottoms.m_nItemNumber.ToString() + "," + "1";
            // [additionalstatus][additionalsoc]
            strtoken += "," + Player_Equipment.m_gEquipment_Bottoms.m_STATUS_AdditionalOption.GetSTATUS_Data();
            strtoken += "," + Player_Equipment.m_gEquipment_Bottoms.m_SOC_AdditionalOption.GetSOC_Data();
            // [reinforcement_current][reinforcementstatus][reinforcementsoc]
            strtoken += "," + Player_Equipment.m_gEquipment_Bottoms.m_nReinforcementCount_Current.ToString();
            strtoken += "," + Player_Equipment.m_gEquipment_Bottoms.m_STATUS_ReinforcementValue.GetSTATUS_Data();
            strtoken += "," + Player_Equipment.m_gEquipment_Bottoms.m_SOC_ReinforcementValue.GetSOC_Data();
        }
        else
        {
            strtoken = "0,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0";
        }
        sw.WriteLine(strtoken);

        if (Player_Equipment.m_bEquipment_Shose == true)
        {
            // [itemcode][itemnumber][itemcount]
            strtoken = Player_Equipment.m_gEquipment_Shose.m_nItemCode.ToString() + "," + Player_Equipment.m_gEquipment_Shose.m_nItemNumber.ToString() + "," + "1";
            // [additionalstatus][additionalsoc]
            strtoken += "," + Player_Equipment.m_gEquipment_Shose.m_STATUS_AdditionalOption.GetSTATUS_Data();
            strtoken += "," + Player_Equipment.m_gEquipment_Shose.m_SOC_AdditionalOption.GetSOC_Data();
            // [reinforcement_current][reinforcementstatus][reinforcementsoc]
            strtoken += "," + Player_Equipment.m_gEquipment_Shose.m_nReinforcementCount_Current.ToString();
            strtoken += "," + Player_Equipment.m_gEquipment_Shose.m_STATUS_ReinforcementValue.GetSTATUS_Data();
            strtoken += "," + Player_Equipment.m_gEquipment_Shose.m_SOC_ReinforcementValue.GetSOC_Data();
        }
        else
        {
            strtoken = "0,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0";
        }
        sw.WriteLine(strtoken);

        if (Player_Equipment.m_bEquipment_Gloves == true)
        {
            // [itemcode][itemnumber][itemcount]
            strtoken = Player_Equipment.m_gEquipment_Gloves.m_nItemCode.ToString() + "," + Player_Equipment.m_gEquipment_Gloves.m_nItemNumber.ToString() + "," + "1";
            // [additionalstatus][additionalsoc]
            strtoken += "," + Player_Equipment.m_gEquipment_Gloves.m_STATUS_AdditionalOption.GetSTATUS_Data();
            strtoken += "," + Player_Equipment.m_gEquipment_Gloves.m_SOC_AdditionalOption.GetSOC_Data();
            // [reinforcement_current][reinforcementstatus][reinforcementsoc]
            strtoken += "," + Player_Equipment.m_gEquipment_Gloves.m_nReinforcementCount_Current.ToString();
            strtoken += "," + Player_Equipment.m_gEquipment_Gloves.m_STATUS_ReinforcementValue.GetSTATUS_Data();
            strtoken += "," + Player_Equipment.m_gEquipment_Gloves.m_SOC_ReinforcementValue.GetSOC_Data();
        }
        else
        {
            strtoken = "0,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0";
        }
        sw.WriteLine(strtoken);

        if (Player_Equipment.m_bEquipment_Mainweapon == true)
        {
            // [itemcode][itemnumber][itemcount]
            strtoken = Player_Equipment.m_gEquipment_Mainweapon.m_nItemCode.ToString() + "," + Player_Equipment.m_gEquipment_Mainweapon.m_nItemNumber.ToString() + "," + "1";
            // [additionalstatus][additionalsoc]
            strtoken += "," + Player_Equipment.m_gEquipment_Mainweapon.m_STATUS_AdditionalOption.GetSTATUS_Data();
            strtoken += "," + Player_Equipment.m_gEquipment_Mainweapon.m_SOC_AdditionalOption.GetSOC_Data();
            // [reinforcement_current][reinforcementstatus][reinforcementsoc]
            strtoken += "," + Player_Equipment.m_gEquipment_Mainweapon.m_nReinforcementCount_Current.ToString();
            strtoken += "," + Player_Equipment.m_gEquipment_Mainweapon.m_STATUS_ReinforcementValue.GetSTATUS_Data();
            strtoken += "," + Player_Equipment.m_gEquipment_Mainweapon.m_SOC_ReinforcementValue.GetSOC_Data();
        }
        else
        {
            strtoken = "0,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0";
        }
        sw.WriteLine(strtoken);

        if (Player_Equipment.m_bEquipment_Subweapon == true)
        {
            // [itemcode][itemnumber][itemcount]
            strtoken = Player_Equipment.m_gEquipment_Subweapon.m_nItemCode.ToString() + "," + Player_Equipment.m_gEquipment_Subweapon.m_nItemNumber.ToString() + "," + "1";
            // [additionalstatus][additionalsoc]
            strtoken += "," + Player_Equipment.m_gEquipment_Subweapon.m_STATUS_AdditionalOption.GetSTATUS_Data();
            strtoken += "," + Player_Equipment.m_gEquipment_Subweapon.m_SOC_AdditionalOption.GetSOC_Data();
            // [reinforcement_current][reinforcementstatus][reinforcementsoc]
            strtoken += "," + Player_Equipment.m_gEquipment_Subweapon.m_nReinforcementCount_Current.ToString();
            strtoken += "," + Player_Equipment.m_gEquipment_Subweapon.m_STATUS_ReinforcementValue.GetSTATUS_Data();
            strtoken += "," + Player_Equipment.m_gEquipment_Subweapon.m_SOC_ReinforcementValue.GetSOC_Data();
        }
        else
        {
            strtoken = "0,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0";
        }
        sw.WriteLine(strtoken);

        sw.Flush();
        sw.Close();

        //Debug.Log("[플레이어가 장착한 장비 아이템 데이터 저장 완료.]");
        return true;
    }
    // -----------------------------------------------------------------------------------------------------------------------------------------------------------



    // Player_Quest Data Set. ------------------------------------------------------------------------------------------------------------------------------------
    Quest_KILL_MONSTER qkm;
    Quest_KILL_TYPE qkt;
    Quest_GOAWAY_MONSTER qgm;
    Quest_GOAWAY_TYPE qgt;
    Quest_COLLECT qct;
    Quest_CONVERSATION qcn;
    Quest_ROLL qr;
    Quest_ELIMINATE_MONSTER qem;
    Quest_ELIMINATE_TYPE qet;
    // Player Quest Data 불러오기.
    // [Player_Quest]
    // QuestType: KILL_MONSTER,GOAWAY_MONSTER,ELIMINATE_MONSTER
    // [questorder][questcode][questtype][questprogress][questcondition][questclear][countcount][countcurrent. . . ]
    // QuestType: KILL_TYPE,GOAWAY_TYPE,ELIMINATE_TYPE,ROLL
    // [questorder][questcode][questtype][questprogress][questcondition][questclear][countcurrent]
    // QuestType: CONVERSATION,COLLECT
    // [questorder][questcode][questtype][questprogress][questcondition][questclear]
    // 0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,...
    public bool Load_Player_Quest()
    {
        int questprogressorder = 0, questcompleteorder = 0;

        StreamReader sr;

        // Player Quest
        //textasset = Resources.Load<TextAsset>("GameData/Player_Quest_Progress");
        //StringReader sr = new StringReader(textasset.text);
        //sr = new StreamReader("./GameData/Player_Quest_Progress.csv");
        //sr = new StreamReader("C:/Users/USER/AppData/LocalLow/Free For All/GameData/Player_Quest_Progress.csv");
        sr = new StreamReader(m_sDepot_C + "Player_Quest_Progress.csv");

        strtoken = "";

        if (!sr.EndOfStream)
        {
            strtoken = sr.ReadLine();
            strtoken = sr.ReadLine();
            strtoken = sr.ReadLine();
            strtoken = sr.ReadLine();
            strtoken = sr.ReadLine();
            strtoken = sr.ReadLine();
            strtoken = sr.ReadLine();
            strtoken = sr.ReadLine();

            //strtoken = sr.ReadLine();
            strtoken = "";

            while (!sr.EndOfStream)
            {
                strtoken = sr.ReadLine();
                itemdata = strtoken.Split(',');

                if (int.Parse(itemdata[0]) > questprogressorder)
                {
                    questprogressorder = int.Parse(itemdata[0]);
                }

                switch (itemdata[2])
                {
                    case "KILL_MONSTER":
                        {
                            qkm = QuestManager.Instance.GetQuest_KILL_MONSTER(int.Parse(itemdata[1]));
                            qkm.m_nQuestOrder = int.Parse(itemdata[0]);
                            if (itemdata[3] == "1")
                            {
                                qkm.m_bProcess = true;
                            }
                            else
                            {
                                qkm.m_bProcess = false;
                            }
                            if (itemdata[4] == "1")
                            {
                                qkm.m_bCondition = true;
                                GUIManager_Total.Instance.Display_GUI_QuestStateInfo(qkm);
                            }
                            else
                            {
                                qkm.m_bCondition = false;
                            }
                            if (itemdata[5] == "1")
                            {
                                qkm.m_bClear = true;
                            }
                            else
                            {
                                qkm.m_bClear = false;
                            }
                            for (int i = 0; i < int.Parse(itemdata[6]); i++)
                            {
                                qkm.m_nl_Count_Current[i] = int.Parse(itemdata[7 + i]);
                            }
                            //Player_Total.Instance.AddQuest(qkm);
                            Player_Quest.m_lQuestList_Progress_KILL_MONSTER.Add(qkm);
                            GUIManager_Total.Instance.Update_Quest(qkm, 0);
                        }
                        break;
                    case "KILL_TYPE":
                        {
                            qkt = QuestManager.Instance.GetQuest_KILL_TYPE(int.Parse(itemdata[1]));
                            qkt.m_nQuestOrder = int.Parse(itemdata[0]);
                            if (itemdata[3] == "1")
                            {
                                qkt.m_bProcess = true;
                            }
                            else
                            {
                                qkt.m_bProcess = false;
                            }
                            if (itemdata[4] == "1")
                            {
                                qkt.m_bCondition = true;
                                GUIManager_Total.Instance.Display_GUI_QuestStateInfo(qkt);
                            }
                            else
                            {
                                qkt.m_bCondition = false;
                            }
                            if (itemdata[5] == "1")
                            {
                                qkt.m_bClear = true;
                            }
                            else
                            {
                                qkt.m_bClear = false;
                            }
                            qkt.m_nCount_Current = int.Parse(itemdata[6]);
                            //Player_Total.Instance.AddQuest(qkt);
                            Player_Quest.m_lQuestList_Progress_KILL_TYPE.Add(qkt);
                            GUIManager_Total.Instance.Update_Quest(qkt, 0);
                        }
                        break;
                    case "GOAWAY_MONSTER":
                        {
                            qgm = QuestManager.Instance.GetQuest_GOAWAY_MONSTER(int.Parse(itemdata[1]));
                            qgm.m_nQuestOrder = int.Parse(itemdata[0]);
                            if (itemdata[3] == "1")
                            {
                                qgm.m_bProcess = true;
                            }
                            else
                            {
                                qgm.m_bProcess = false;
                            }
                            if (itemdata[4] == "1")
                            {
                                qgm.m_bCondition = true;
                                GUIManager_Total.Instance.Display_GUI_QuestStateInfo(qgm);
                            }
                            else
                            {
                                qgm.m_bCondition = false;
                            }
                            if (itemdata[5] == "1")
                            {
                                qgm.m_bClear = true;
                            }
                            else
                            {
                                qgm.m_bClear = false;
                            }
                            for (int i = 0; i < int.Parse(itemdata[6]); i++)
                            {
                                qgm.m_nl_Count_Current[i] = int.Parse(itemdata[7 + i]);
                            }
                            //Player_Total.Instance.AddQuest(qgm);
                            Player_Quest.m_lQuestList_Progress_GOAWAY_MONSTER.Add(qgm);
                            GUIManager_Total.Instance.Update_Quest(qgm, 0);
                        }
                        break;
                    case "GOAWAY_TYPE":
                        {
                            qgt = QuestManager.Instance.GetQuest_GOAWAY_TYPE(int.Parse(itemdata[1]));
                            qgt.m_nQuestOrder = int.Parse(itemdata[0]);
                            if (itemdata[3] == "1")
                            {
                                qgt.m_bProcess = true;
                            }
                            else
                            {
                                qgt.m_bProcess = false;
                            }
                            if (itemdata[4] == "1")
                            {
                                qgt.m_bCondition = true;
                                GUIManager_Total.Instance.Display_GUI_QuestStateInfo(qgt);
                            }
                            else
                            {
                                qgt.m_bCondition = false;
                            }
                            if (itemdata[5] == "1")
                            {
                                qgt.m_bClear = true;
                            }
                            else
                            {
                                qgt.m_bClear = false;
                            }
                            qgt.m_nCount_Current = int.Parse(itemdata[6]);
                            //Player_Total.Instance.AddQuest(qgt);
                            Player_Quest.m_lQuestList_Progress_GOAWAY_TYPE.Add(qgt);
                            GUIManager_Total.Instance.Update_Quest(qgt, 0);
                        }
                        break;
                    case "COLLECT":
                        {
                            qct = QuestManager.Instance.GetQuest_COLLECT(int.Parse(itemdata[1]));
                            qct.m_nQuestOrder = int.Parse(itemdata[0]);
                            if (itemdata[3] == "1")
                            {
                                qct.m_bProcess = true;
                            }
                            else
                            {
                                qct.m_bProcess = false;
                            }
                            if (itemdata[4] == "1")
                            {
                                qct.m_bCondition = true;
                                GUIManager_Total.Instance.Display_GUI_QuestStateInfo(qct);
                            }
                            else
                            {
                                qct.m_bCondition = false;
                            }
                            if (itemdata[5] == "1")
                            {
                                qct.m_bClear = true;
                            }
                            else
                            {
                                qct.m_bClear = false;
                            }
                            for (int i = 0; i < qct.m_nl_ItemCode.Count; i++)
                            {
                                qct.m_nl_ItemCount_Current[i] = qct.m_nl_ItemCount_Max[i];
                            }
                            //Player_Total.Instance.m_pq_Quest.AddQuest(qct);
                            Player_Quest.m_lQuestList_Progress_COLLECT.Add(qct);
                            GUIManager_Total.Instance.Update_Quest(qct, 0);
                        }
                        break;
                    case "ROLL":
                        {
                            qr = QuestManager.Instance.GetQuest_ROLL(int.Parse(itemdata[1]));
                            qr.m_nQuestOrder = int.Parse(itemdata[0]);
                            if (itemdata[3] == "1")
                            {
                                qr.m_bProcess = true;
                            }
                            else
                            {
                                qr.m_bProcess = false;
                            }
                            if (itemdata[4] == "1")
                            {
                                qr.m_bCondition = true;
                                GUIManager_Total.Instance.Display_GUI_QuestStateInfo(qr);
                            }
                            else
                            {
                                qr.m_bCondition = false;
                            }
                            if (itemdata[5] == "1")
                            {
                                qr.m_bClear = true;
                            }
                            else
                            {
                                qr.m_bClear = false;
                            }
                            qr.m_nCount_Current = int.Parse(itemdata[6]);
                            //Player_Total.Instance.AddQuest(qr);
                            Player_Quest.m_lQuestList_Progress_ROLL.Add(qr);
                            GUIManager_Total.Instance.Update_Quest(qr, 0);
                        }
                        break;
                    case "CONVERSATION":
                        {
                            qcn = QuestManager.Instance.GetQuest_CONVERSATION(int.Parse(itemdata[1]));
                            qcn.m_nQuestOrder = int.Parse(itemdata[0]);
                            if (itemdata[3] == "1")
                            {
                                qcn.m_bProcess = true;
                            }
                            else
                            {
                                qcn.m_bProcess = false;
                            }
                            if (itemdata[4] == "1")
                            {
                                qcn.m_bCondition = true;
                                //GUIManager_Total.Instance.Display_GUI_QuestStateInfo(qcn);
                            }
                            else
                            {
                                qcn.m_bCondition = false;
                            }
                            if (itemdata[5] == "1")
                            {
                                qcn.m_bClear = true;
                            }
                            else
                            {
                                qcn.m_bClear = false;
                            }
                            //Player_Total.Instance.AddQuest(qcn);
                            Player_Quest.m_lQuestList_Progress_CONVERSATION.Add(qcn);
                            GUIManager_Total.Instance.Update_Quest(qcn, 0);
                        }
                        break;
                    case "ELIMINATE_MONSTER":
                        {
                            qem = QuestManager.Instance.GetQuest_ELIMINATE_MONSTER(int.Parse(itemdata[1]));
                            qem.m_nQuestOrder = int.Parse(itemdata[0]);
                            if (itemdata[3] == "1")
                            {
                                qem.m_bProcess = true;
                            }
                            else
                            {
                                qem.m_bProcess = false;
                            }
                            if (itemdata[4] == "1")
                            {
                                qem.m_bCondition = true;
                                GUIManager_Total.Instance.Display_GUI_QuestStateInfo(qem);
                            }
                            else
                            {
                                qem.m_bCondition = false;
                            }
                            if (itemdata[5] == "1")
                            {
                                qem.m_bClear = true;
                            }
                            else
                            {
                                qem.m_bClear = false;
                            }
                            for (int i = 0; i < int.Parse(itemdata[6]); i++)
                            {
                                qem.m_nl_Count_Current[i] = int.Parse(itemdata[7 + i]);
                            }
                            //Player_Total.Instance.AddQuest(qem);
                            Player_Quest.m_lQuestList_Progress_ELIMINATE_MONSTER.Add(qem);
                            GUIManager_Total.Instance.Update_Quest(qem, 0);
                        }
                        break;
                    case "ELIMINATE_TYPE":
                        {
                            qet = QuestManager.Instance.GetQuest_ELIMINATE_TYPE(int.Parse(itemdata[1]));
                            qet.m_nQuestOrder = int.Parse(itemdata[0]);
                            if (itemdata[3] == "1")
                            {
                                qet.m_bProcess = true;
                            }
                            else
                            {
                                qet.m_bProcess = false;
                            }
                            if (itemdata[4] == "1")
                            {
                                qet.m_bCondition = true;
                                GUIManager_Total.Instance.Display_GUI_QuestStateInfo(qet);
                            }
                            else
                            {
                                qet.m_bCondition = false;
                            }
                            if (itemdata[5] == "1")
                            {
                                qet.m_bClear = true;
                            }
                            else
                            {
                                qet.m_bClear = false;
                            }
                            qet.m_nCount_Current = int.Parse(itemdata[6]);
                            //Player_Total.Instance.AddQuest(qet);
                            Player_Quest.m_lQuestList_Progress_ELIMINATE_TYPE.Add(qet);
                            GUIManager_Total.Instance.Update_Quest(qet, 0);
                        }
                        break;
                }
            }
            sr.Close();
        }
        // -----------------------------------------------------------------------------------------------------------------------------------------------------------
        //textasset = Resources.Load<TextAsset>("GameData/Player_Quest_Complete");
        //sr = new StringReader(textasset.text);
        //sr = new StreamReader("./GameData/Player_Quest_Complete.csv");
        //sr = new StreamReader("C:/Users/USER/AppData/LocalLow/Free For All/GameData/Player_Quest_Complete.csv");
        sr = new StreamReader(m_sDepot_C + "Player_Quest_Complete.csv");

        strtoken = "";

        if (!sr.EndOfStream)
        {
            strtoken = sr.ReadLine();
            strtoken = sr.ReadLine();
            strtoken = sr.ReadLine();
            strtoken = sr.ReadLine();

            //strtoken = sr.ReadLine();
            strtoken = "";

            while (!sr.EndOfStream)
            {
                strtoken = sr.ReadLine();
                itemdata = strtoken.Split(',');

                if (int.Parse(itemdata[0]) > questcompleteorder)
                {
                    questcompleteorder = int.Parse(itemdata[0]);
                }

                switch (itemdata[2])
                {
                    case "KILL_MONSTER":
                        {
                            qkm = QuestManager.Instance.GetQuest_KILL_MONSTER(int.Parse(itemdata[1]));
                            qkm.m_nQuestOrder = int.Parse(itemdata[0]);
                            if (itemdata[3] == "1")
                            {
                                qkm.m_bProcess = true;
                            }
                            else
                            {
                                qkm.m_bProcess = false;
                            }
                            if (itemdata[4] == "1")
                            {
                                qkm.m_bCondition = true;
                            }
                            else
                            {
                                qkm.m_bCondition = false;
                            }
                            if (itemdata[5] == "1")
                            {
                                qkm.m_bClear = true;
                            }
                            else
                            {
                                qkm.m_bClear = false;
                            }
                            for (int i = 0; i < qkm.m_nl_MonsterCode.Count; i++)
                            {
                                qkm.m_nl_Count_Current[i] = qkm.m_nl_Count_Max[i];
                            }
                            GUIManager_Total.Instance.Update_Quest(qkm, 0);
                            Player_Quest.m_lQuestList_Complete_KILL_MONSTER.Add(qkm);
                            GUIManager_Total.Instance.Update_Quest(qkm, 2);
                        }
                        break;
                    case "KILL_TYPE":
                        {
                            qkt = QuestManager.Instance.GetQuest_KILL_TYPE(int.Parse(itemdata[1]));
                            qkt.m_nQuestOrder = int.Parse(itemdata[0]);
                            if (itemdata[3] == "1")
                            {
                                qkt.m_bProcess = true;
                            }
                            else
                            {
                                qkt.m_bProcess = false;
                            }
                            if (itemdata[4] == "1")
                            {
                                qkt.m_bCondition = true;
                            }
                            else
                            {
                                qkt.m_bCondition = false;
                            }
                            if (itemdata[5] == "1")
                            {
                                qkt.m_bClear = true;
                            }
                            else
                            {
                                qkt.m_bClear = false;
                            }
                            qkt.m_nCount_Current = qkt.m_nCount_Max;
                            GUIManager_Total.Instance.Update_Quest(qkt, 0);
                            Player_Quest.m_lQuestList_Complete_KILL_TYPE.Add(qkt);
                            GUIManager_Total.Instance.Update_Quest(qkt, 2);
                        }
                        break;
                    case "GOAWAY_MONSTER":
                        {
                            qgm = QuestManager.Instance.GetQuest_GOAWAY_MONSTER(int.Parse(itemdata[1]));
                            qgm.m_nQuestOrder = int.Parse(itemdata[0]);
                            if (itemdata[3] == "1")
                            {
                                qgm.m_bProcess = true;
                            }
                            else
                            {
                                qgm.m_bProcess = false;
                            }
                            if (itemdata[4] == "1")
                            {
                                qgm.m_bCondition = true;
                            }
                            else
                            {
                                qgm.m_bCondition = false;
                            }
                            if (itemdata[5] == "1")
                            {
                                qgm.m_bClear = true;
                            }
                            else
                            {
                                qgm.m_bClear = false;
                            }
                            for (int i = 0; i < qgm.m_nl_MonsterCode.Count; i++)
                            {
                                qgm.m_nl_Count_Current[i] = qgm.m_nl_Count_Max[i];
                            }
                            GUIManager_Total.Instance.Update_Quest(qgm, 0);
                            Player_Quest.m_lQuestList_Complete_GOAWAY_MONSTER.Add(qgm);
                            GUIManager_Total.Instance.Update_Quest(qgm, 2);
                        }
                        break;
                    case "GOAWAY_TYPE":
                        {
                            qgt = QuestManager.Instance.GetQuest_GOAWAY_TYPE(int.Parse(itemdata[1]));
                            qgt.m_nQuestOrder = int.Parse(itemdata[0]);
                            if (itemdata[3] == "1")
                            {
                                qgt.m_bProcess = true;
                            }
                            else
                            {
                                qgt.m_bProcess = false;
                            }
                            if (itemdata[4] == "1")
                            {
                                qgt.m_bCondition = true;
                            }
                            else
                            {
                                qgt.m_bCondition = false;
                            }
                            if (itemdata[5] == "1")
                            {
                                qgt.m_bClear = true;
                            }
                            else
                            {
                                qgt.m_bClear = false;
                            }
                            qgt.m_nCount_Current = qgt.m_nCount_Max;
                            GUIManager_Total.Instance.Update_Quest(qgt, 0);
                            Player_Quest.m_lQuestList_Complete_GOAWAY_TYPE.Add(qgt);
                            GUIManager_Total.Instance.Update_Quest(qgt, 2);
                        }
                        break;
                    case "COLLECT":
                        {
                            qct = QuestManager.Instance.GetQuest_COLLECT(int.Parse(itemdata[1]));
                            qct.m_nQuestOrder = int.Parse(itemdata[0]);
                            if (itemdata[3] == "1")
                            {
                                qct.m_bProcess = true;
                            }
                            else
                            {
                                qct.m_bProcess = false;
                            }
                            if (itemdata[4] == "1")
                            {
                                qct.m_bCondition = true;
                            }
                            else
                            {
                                qct.m_bCondition = false;
                            }
                            if (itemdata[5] == "1")
                            {
                                qct.m_bClear = true;
                            }
                            else
                            {
                                qct.m_bClear = false;
                            }
                            for (int i = 0; i < qct.m_nl_ItemCode.Count; i++)
                            {
                                qct.m_nl_ItemCount_Current[i] = qct.m_nl_ItemCount_Max[i];
                            }
                            GUIManager_Total.Instance.Update_Quest(qct, 0);
                            Player_Quest.m_lQuestList_Complete_COLLECT.Add(qct);
                            GUIManager_Total.Instance.Update_Quest(qct, 2);
                        }
                        break;
                    case "ROLL":
                        {
                            qr = QuestManager.Instance.GetQuest_ROLL(int.Parse(itemdata[1]));
                            qr.m_nQuestOrder = int.Parse(itemdata[0]);
                            if (itemdata[3] == "1")
                            {
                                qr.m_bProcess = true;
                            }
                            else
                            {
                                qr.m_bProcess = false;
                            }
                            if (itemdata[4] == "1")
                            {
                                qr.m_bCondition = true;
                            }
                            else
                            {
                                qr.m_bCondition = false;
                            }
                            if (itemdata[5] == "1")
                            {
                                qr.m_bClear = true;
                            }
                            else
                            {
                                qr.m_bClear = false;
                            }
                            qr.m_nCount_Current = qr.m_nCount_Max;
                            GUIManager_Total.Instance.Update_Quest(qr, 0);
                            Player_Quest.m_lQuestList_Complete_ROLL.Add(qr);
                            GUIManager_Total.Instance.Update_Quest(qr, 2);
                        }
                        break;
                    case "CONVERSATION":
                        {
                            qcn = QuestManager.Instance.GetQuest_CONVERSATION(int.Parse(itemdata[1]));
                            qcn.m_nQuestOrder = int.Parse(itemdata[0]);
                            if (itemdata[3] == "1")
                            {
                                qcn.m_bProcess = true;
                            }
                            else
                            {
                                qcn.m_bProcess = false;
                            }
                            if (itemdata[4] == "1")
                            {
                                qcn.m_bCondition = true;
                            }
                            else
                            {
                                qcn.m_bCondition = false;
                            }
                            if (itemdata[5] == "1")
                            {
                                qcn.m_bClear = true;
                            }
                            else
                            {
                                qcn.m_bClear = false;
                            }
                            GUIManager_Total.Instance.Update_Quest(qcn, 0);
                            Player_Quest.m_lQuestList_Complete_CONVERSATION.Add(qcn);
                            GUIManager_Total.Instance.Update_Quest(qcn, 2);
                        }
                        break;
                    case "ELIMINATE_MONSTER":
                        {
                            qem = QuestManager.Instance.GetQuest_ELIMINATE_MONSTER(int.Parse(itemdata[1]));
                            qem.m_nQuestOrder = int.Parse(itemdata[0]);
                            if (itemdata[3] == "1")
                            {
                                qem.m_bProcess = true;
                            }
                            else
                            {
                                qem.m_bProcess = false;
                            }
                            if (itemdata[4] == "1")
                            {
                                qem.m_bCondition = true;
                            }
                            else
                            {
                                qem.m_bCondition = false;
                            }
                            if (itemdata[5] == "1")
                            {
                                qem.m_bClear = true;
                            }
                            else
                            {
                                qem.m_bClear = false;
                            }
                            for (int i = 0; i < qem.m_nl_MonsterCode.Count; i++)
                            {
                                qem.m_nl_Count_Current[i] = qem.m_nl_Count_Max[i];
                            }
                            GUIManager_Total.Instance.Update_Quest(qem, 0);
                            Player_Quest.m_lQuestList_Complete_ELIMINATE_MONSTER.Add(qem);
                            GUIManager_Total.Instance.Update_Quest(qem, 2);
                        }
                        break;
                    case "ELIMINATE_TYPE":
                        {
                            qet = QuestManager.Instance.GetQuest_ELIMINATE_TYPE(int.Parse(itemdata[1]));
                            qet.m_nQuestOrder = int.Parse(itemdata[0]);
                            if (itemdata[3] == "1")
                            {
                                qet.m_bProcess = true;
                            }
                            else
                            {
                                qet.m_bProcess = false;
                            }
                            if (itemdata[4] == "1")
                            {
                                qet.m_bCondition = true;
                            }
                            else
                            {
                                qet.m_bCondition = false;
                            }
                            if (itemdata[5] == "1")
                            {
                                qet.m_bClear = true;
                            }
                            else
                            {
                                qet.m_bClear = false;
                            }
                            qet.m_nCount_Current = qet.m_nCount_Max;
                            GUIManager_Total.Instance.Update_Quest(qet, 0);
                            Player_Quest.m_lQuestList_Complete_ELIMINATE_TYPE.Add(qet);
                            GUIManager_Total.Instance.Update_Quest(qet, 2);
                        }
                        break;
                }
            }
            sr.Close();
        }

        QuestManager.m_snQuest_ProcessOrder = questprogressorder + 1;
        QuestManager.m_snQuest_CompleteOrder = questcompleteorder + 1;

        NPCManager_Total.Instance.UpdateNPC();

        //Debug.Log("[플레이어가 진행중인 퀘스트 데이터 로드 완료.]");
        //Debug.Log("[플레이어가 완료한 퀘스트 데이터 로드 완료.]");

        GUIManager_Total.Instance.m_GUI_Quest.UpdateQuest_Init();

        GUIManager_Total.Instance.m_GUI_Quest.InitialSort();

        return true;
    }

    // Player Quest Data 저장.
    public bool Save_Player_Quest()
    {
        StreamWriter sw;

        // Player Quest
        //sw = new StreamWriter("C:/Users/USER/AppData/LocalLow/Free For All/GameData/Player_Quest_Progress.csv");
        sw = new StreamWriter(m_sDepot_C + "Player_Quest_Progress.csv");

        sw.WriteLine("[Player_Quest]");
        sw.WriteLine("QuestType: KILL_MONSTER,GOAWAY_MONSTER,ELIMINATE_MONSTER");
        sw.WriteLine("[questorder][questcode][questtype][questprogress = 1][questcondition][questclear = 0][countcount][countcurrent. . . ]");
        sw.WriteLine("QuestType: KILL_TYPE,GOAWAY_TYPE,ELIMINATE_TYPE,ROLL");
        sw.WriteLine("[questorder][questcode][questtype][questprogress = 1][questcondition][questclear = 0][countcurrent]");
        sw.WriteLine("QuestType: CONVERSATION,COLLECT");
        sw.WriteLine("[questorder][questcode][questtype][questprogress = 1][questcondition][questclear = 0]");
        sw.WriteLine("0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,...");

        strtoken = "";

        for (int i = 0; i < Player_Quest.m_lQuestList_Progress_KILL_MONSTER.Count; i++)
        {
            // [questorder][questcode][questtype]
            strtoken = Player_Quest.m_lQuestList_Progress_KILL_MONSTER[i].m_nQuestOrder.ToString() + "," + Player_Quest.m_lQuestList_Progress_KILL_MONSTER[i].m_nQuest_Code.ToString() + "," + Player_Quest.m_lQuestList_Progress_KILL_MONSTER[i].m_eQuestType.ToString();
            // [questprogress]
            strtoken += ",1";
            // [questcondition]
            if (Player_Quest.m_lQuestList_Progress_KILL_MONSTER[i].m_bCondition == true)
            {
                strtoken += ",1";
            }
            else
            {
                strtoken += ",0";
            }
            // [questclear]
            strtoken += ",0";
            // [countcount]
            strtoken += "," + Player_Quest.m_lQuestList_Progress_KILL_MONSTER[i].m_nl_MonsterCode.Count.ToString();
            // [countcurrent. . . ]
            for (int j = 0; j < Player_Quest.m_lQuestList_Progress_KILL_MONSTER[i].m_nl_MonsterCode.Count; j++)
            {
                strtoken += "," + Player_Quest.m_lQuestList_Progress_KILL_MONSTER[i].m_nl_Count_Current[j].ToString();
            }

            sw.WriteLine(strtoken);
        }
        for (int i = 0; i < Player_Quest.m_lQuestList_Progress_KILL_TYPE.Count; i++)
        {
            // [questcode][questtype]
            strtoken = Player_Quest.m_lQuestList_Progress_KILL_TYPE[i].m_nQuestOrder.ToString() + "," + Player_Quest.m_lQuestList_Progress_KILL_TYPE[i].m_nQuest_Code.ToString() + "," + Player_Quest.m_lQuestList_Progress_KILL_TYPE[i].m_eQuestType.ToString();
            // [questprogress]
            strtoken += ",1";
            // [questcondition]
            if (Player_Quest.m_lQuestList_Progress_KILL_TYPE[i].m_bCondition == true)
            {
                strtoken += ",1";
            }
            else
            {
                strtoken += ",0";
            }
            // [questclear]
            strtoken += ",0";
            // [countcurrent]
            strtoken += "," + Player_Quest.m_lQuestList_Progress_KILL_TYPE[i].m_nCount_Current.ToString();

            sw.WriteLine(strtoken);
        }
        for (int i = 0; i < Player_Quest.m_lQuestList_Progress_GOAWAY_MONSTER.Count; i++)
        {
            // [questcode][questtype]
            strtoken = Player_Quest.m_lQuestList_Progress_GOAWAY_MONSTER[i].m_nQuestOrder.ToString() + "," + Player_Quest.m_lQuestList_Progress_GOAWAY_MONSTER[i].m_nQuest_Code.ToString() + "," + Player_Quest.m_lQuestList_Progress_GOAWAY_MONSTER[i].m_eQuestType.ToString();
            // [questprogress]
            strtoken += ",1";
            // [questcondition]
            if (Player_Quest.m_lQuestList_Progress_GOAWAY_MONSTER[i].m_bCondition == true)
            {
                strtoken += ",1";
            }
            else
            {
                strtoken += ",0";
            }
            // [questclear]
            strtoken += ",0";
            // [countcount]
            strtoken += "," + Player_Quest.m_lQuestList_Progress_GOAWAY_MONSTER[i].m_nl_MonsterCode.Count.ToString();
            // [countcurrent. . . ]
            for (int j = 0; j < Player_Quest.m_lQuestList_Progress_GOAWAY_MONSTER[i].m_nl_MonsterCode.Count; j++)
            {
                strtoken += "," + Player_Quest.m_lQuestList_Progress_GOAWAY_MONSTER[i].m_nl_Count_Current[j].ToString();
            }

            sw.WriteLine(strtoken);
        }
        for (int i = 0; i < Player_Quest.m_lQuestList_Progress_GOAWAY_TYPE.Count; i++)
        {
            // [questcode][questtype]
            strtoken = Player_Quest.m_lQuestList_Progress_GOAWAY_TYPE[i].m_nQuestOrder.ToString() + "," + Player_Quest.m_lQuestList_Progress_GOAWAY_TYPE[i].m_nQuest_Code.ToString() + "," + Player_Quest.m_lQuestList_Progress_GOAWAY_TYPE[i].m_eQuestType.ToString();
            // [questprogress]
            strtoken += ",1";
            // [questcondition]
            if (Player_Quest.m_lQuestList_Progress_GOAWAY_TYPE[i].m_bCondition == true)
            {
                strtoken += ",1";
            }
            else
            {
                strtoken += ",0";
            }
            // [questclear]
            strtoken += ",0";
            // [countcurrent]
            strtoken += "," + Player_Quest.m_lQuestList_Progress_GOAWAY_TYPE[i].m_nCount_Current.ToString();

            sw.WriteLine(strtoken);
        }
        for (int i = 0; i < Player_Quest.m_lQuestList_Progress_COLLECT.Count; i++)
        {
            // [questcode][questtype]
            strtoken = Player_Quest.m_lQuestList_Progress_COLLECT[i].m_nQuestOrder.ToString() + "," + Player_Quest.m_lQuestList_Progress_COLLECT[i].m_nQuest_Code.ToString() + "," + Player_Quest.m_lQuestList_Progress_COLLECT[i].m_eQuestType.ToString();
            // [questprogress]
            strtoken += ",1";
            // [questcondition]
            if (Player_Quest.m_lQuestList_Progress_COLLECT[i].m_bCondition == true)
            {
                strtoken += ",1";
            }
            else
            {
                strtoken += ",0";
            }
            // [questclear]
            strtoken += ",0";
            // [countcurrent]
            // ㄴ 아이템 수집 현황은 Player_Itemslot 과 연관되어있기에 
            // ㄴ Player_Quest:QuestUpdate_Collect() 를 사용해 Loading 시 Quest 조건을 체크한다.

            sw.WriteLine(strtoken);
        }
        for (int i = 0; i < Player_Quest.m_lQuestList_Progress_ROLL.Count; i++)
        {
            // [questcode][questtype]
            strtoken = Player_Quest.m_lQuestList_Progress_ROLL[i].m_nQuestOrder.ToString() + "," + Player_Quest.m_lQuestList_Progress_ROLL[i].m_nQuest_Code.ToString() + "," + Player_Quest.m_lQuestList_Progress_ROLL[i].m_eQuestType.ToString();
            // [questprogress]
            strtoken += ",1";
            // [questcondition]
            if (Player_Quest.m_lQuestList_Progress_ROLL[i].m_bCondition == true)
            {
                strtoken += ",1";
            }
            else
            {
                strtoken += ",0";
            }
            // [questclear]
            strtoken += ",0";
            // [countcurrent]
            strtoken += "," + Player_Quest.m_lQuestList_Progress_ROLL[i].m_nCount_Current.ToString();

            sw.WriteLine(strtoken);
        }
        for (int i = 0; i < Player_Quest.m_lQuestList_Progress_CONVERSATION.Count; i++)
        {
            // [questcode][questtype]
            strtoken = Player_Quest.m_lQuestList_Progress_CONVERSATION[i].m_nQuestOrder.ToString() + "," + Player_Quest.m_lQuestList_Progress_CONVERSATION[i].m_nQuest_Code.ToString() + "," + Player_Quest.m_lQuestList_Progress_CONVERSATION[i].m_eQuestType.ToString();
            // [questprogress]
            strtoken += ",1";
            // [questcondition]
            if (Player_Quest.m_lQuestList_Progress_CONVERSATION[i].m_bCondition == true)
            {
                strtoken += ",1";
            }
            else
            {
                strtoken += ",0";
            }
            // [questclear]
            strtoken += ",0";

            sw.WriteLine(strtoken);
        }
        for (int i = 0; i < Player_Quest.m_lQuestList_Progress_ELIMINATE_MONSTER.Count; i++)
        {
            // [questcode][questtype]
            strtoken = Player_Quest.m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_nQuestOrder.ToString() + "," + Player_Quest.m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_nQuest_Code.ToString() + "," + Player_Quest.m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_eQuestType.ToString();
            // [questprogress]
            strtoken += ",1";
            // [questcondition]
            if (Player_Quest.m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_bCondition == true)
            {
                strtoken += ",1";
            }
            else
            {
                strtoken += ",0";
            }
            // [questclear]
            strtoken += ",0";
            // [countcount]
            strtoken += "," + Player_Quest.m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_nl_MonsterCode.Count.ToString();
            // [countcurrent. . . ]
            for (int j = 0; j < Player_Quest.m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_nl_MonsterCode.Count; j++)
            {
                strtoken += "," + Player_Quest.m_lQuestList_Progress_ELIMINATE_MONSTER[i].m_nl_Count_Current[j].ToString();
            }

            sw.WriteLine(strtoken);
        }
        for (int i = 0; i < Player_Quest.m_lQuestList_Progress_ELIMINATE_TYPE.Count; i++)
        {
            // [questcode][questtype]
            strtoken = Player_Quest.m_lQuestList_Progress_ELIMINATE_TYPE[i].m_nQuestOrder.ToString() + "," + Player_Quest.m_lQuestList_Progress_ELIMINATE_TYPE[i].m_nQuest_Code.ToString() + "," + Player_Quest.m_lQuestList_Progress_ELIMINATE_TYPE[i].m_eQuestType.ToString();
            // [questprogress]
            strtoken += ",1";
            // [questcondition]
            if (Player_Quest.m_lQuestList_Progress_ELIMINATE_TYPE[i].m_bCondition == true)
            {
                strtoken += ",1";
            }
            else
            {
                strtoken += ",0";
            }
            // [questclear]
            strtoken += ",0";
            // [countcurrent]
            strtoken += "," + Player_Quest.m_lQuestList_Progress_ELIMINATE_TYPE[i].m_nCount_Current.ToString();

            sw.WriteLine(strtoken);
        }

        sw.Flush();
        sw.Close();
        // -----------------------------------------------------------------------------------------------------------------------------------------------------------
        //sw = new StreamWriter("C:/Users/USER/AppData/LocalLow/Free For All/GameData/Player_Quest_Complete.csv");
        sw = new StreamWriter(m_sDepot_C + "Player_Quest_Complete.csv");

        sw.WriteLine("[Player_Quest]");
        sw.WriteLine("QuestType: KILL_MONSTER,KILL_TYPE,GOAWAY_MONSTER,GOAWAY_TYPE,COLLECT,CONVERSATION,ROLL,ELIMINATE_MONSTER, ELIMINATE_TYPE");
        sw.WriteLine("[questorder][questcode][questtype][questprogress = 0][questcondition][questclear = 1]");
        sw.WriteLine("0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,...");

        strtoken = "";

        for (int i = 0; i < Player_Quest.m_lQuestList_Complete_KILL_MONSTER.Count; i++)
        {
            // [questcode][questtype]
            strtoken = Player_Quest.m_lQuestList_Complete_KILL_MONSTER[i].m_nQuestOrder.ToString() + "," + Player_Quest.m_lQuestList_Complete_KILL_MONSTER[i].m_nQuest_Code.ToString() + "," + Player_Quest.m_lQuestList_Complete_KILL_MONSTER[i].m_eQuestType.ToString();
            // [questprogress]
            strtoken += ",0";
            // [questcondition]
            if (Player_Quest.m_lQuestList_Complete_KILL_MONSTER[i].m_bCondition == true)
            {
                strtoken += ",1";
            }
            else
            {
                strtoken += ",0";
            }
            // [questclear]
            strtoken += ",1";

            sw.WriteLine(strtoken);
        }
        for (int i = 0; i < Player_Quest.m_lQuestList_Complete_KILL_TYPE.Count; i++)
        {
            // [questcode][questtype]
            strtoken = Player_Quest.m_lQuestList_Complete_KILL_TYPE[i].m_nQuestOrder.ToString() + "," + Player_Quest.m_lQuestList_Complete_KILL_TYPE[i].m_nQuest_Code.ToString() + "," + Player_Quest.m_lQuestList_Complete_KILL_TYPE[i].m_eQuestType.ToString();
            // [questprogress]
            strtoken += ",0";
            // [questcondition]
            if (Player_Quest.m_lQuestList_Complete_KILL_TYPE[i].m_bCondition == true)
            {
                strtoken += ",1";
            }
            else
            {
                strtoken += ",0";
            }
            // [questclear]
            strtoken += ",1";

            sw.WriteLine(strtoken);
        }
        for (int i = 0; i < Player_Quest.m_lQuestList_Complete_GOAWAY_MONSTER.Count; i++)
        {
            // [questcode][questtype]
            strtoken = Player_Quest.m_lQuestList_Complete_GOAWAY_MONSTER[i].m_nQuestOrder.ToString() + "," + Player_Quest.m_lQuestList_Complete_GOAWAY_MONSTER[i].m_nQuest_Code.ToString() + "," + Player_Quest.m_lQuestList_Complete_GOAWAY_MONSTER[i].m_eQuestType.ToString();
            // [questprogress]
            strtoken += ",0";
            // [questcondition]
            if (Player_Quest.m_lQuestList_Complete_GOAWAY_MONSTER[i].m_bCondition == true)
            {
                strtoken += ",1";
            }
            else
            {
                strtoken += ",0";
            }
            // [questclear]
            strtoken += ",1";

            sw.WriteLine(strtoken);
        }
        for (int i = 0; i < Player_Quest.m_lQuestList_Complete_GOAWAY_TYPE.Count; i++)
        {
            // [questcode][questtype]
            strtoken = Player_Quest.m_lQuestList_Complete_GOAWAY_TYPE[i].m_nQuestOrder.ToString() + "," + Player_Quest.m_lQuestList_Complete_GOAWAY_TYPE[i].m_nQuest_Code.ToString() + "," + Player_Quest.m_lQuestList_Complete_GOAWAY_TYPE[i].m_eQuestType.ToString();
            // [questprogress]
            strtoken += ",0";
            // [questcondition]
            if (Player_Quest.m_lQuestList_Complete_GOAWAY_TYPE[i].m_bCondition == true)
            {
                strtoken += ",1";
            }
            else
            {
                strtoken += ",0";
            }
            // [questclear]
            strtoken += ",1";

            sw.WriteLine(strtoken);
        }
        for (int i = 0; i < Player_Quest.m_lQuestList_Complete_COLLECT.Count; i++)
        {
            // [questcode][questtype]
            strtoken = Player_Quest.m_lQuestList_Complete_COLLECT[i].m_nQuestOrder.ToString() + "," + Player_Quest.m_lQuestList_Complete_COLLECT[i].m_nQuest_Code.ToString() + "," + Player_Quest.m_lQuestList_Complete_COLLECT[i].m_eQuestType.ToString();
            // [questprogress]
            strtoken += ",0";
            // [questcondition]
            if (Player_Quest.m_lQuestList_Complete_COLLECT[i].m_bCondition == true)
            {
                strtoken += ",1";
            }
            else
            {
                strtoken += ",0";
            }
            // [questclear]
            strtoken += ",1";

            sw.WriteLine(strtoken);
        }
        for (int i = 0; i < Player_Quest.m_lQuestList_Complete_ROLL.Count; i++)
        {
            // [questcode][questtype]
            strtoken = Player_Quest.m_lQuestList_Complete_ROLL[i].m_nQuestOrder.ToString() + "," + Player_Quest.m_lQuestList_Complete_ROLL[i].m_nQuest_Code.ToString() + "," + Player_Quest.m_lQuestList_Complete_ROLL[i].m_eQuestType.ToString();
            // [questprogress]
            strtoken += ",0";
            // [questcondition]
            if (Player_Quest.m_lQuestList_Complete_ROLL[i].m_bCondition == true)
            {
                strtoken += ",1";
            }
            else
            {
                strtoken += ",0";
            }
            // [questclear]
            strtoken += ",1";

            sw.WriteLine(strtoken);
        }
        for (int i = 0; i < Player_Quest.m_lQuestList_Complete_CONVERSATION.Count; i++)
        {
            // [questcode][questtype]
            strtoken = Player_Quest.m_lQuestList_Complete_CONVERSATION[i].m_nQuestOrder.ToString() + "," + Player_Quest.m_lQuestList_Complete_CONVERSATION[i].m_nQuest_Code.ToString() + "," + Player_Quest.m_lQuestList_Complete_CONVERSATION[i].m_eQuestType.ToString();
            // [questprogress]
            strtoken += ",0";
            // [questcondition]
            if (Player_Quest.m_lQuestList_Complete_CONVERSATION[i].m_bCondition == true)
            {
                strtoken += ",1";
            }
            else
            {
                strtoken += ",0";
            }
            // [questclear]
            strtoken += ",1";

            sw.WriteLine(strtoken);
        }
        for (int i = 0; i < Player_Quest.m_lQuestList_Complete_ELIMINATE_MONSTER.Count; i++)
        {
            // [questcode][questtype]
            strtoken = Player_Quest.m_lQuestList_Complete_ELIMINATE_MONSTER[i].m_nQuestOrder.ToString() + "," + Player_Quest.m_lQuestList_Complete_ELIMINATE_MONSTER[i].m_nQuest_Code.ToString() + "," + Player_Quest.m_lQuestList_Complete_ELIMINATE_MONSTER[i].m_eQuestType.ToString();
            // [questprogress]
            strtoken += ",0";
            // [questcondition]
            if (Player_Quest.m_lQuestList_Complete_ELIMINATE_MONSTER[i].m_bCondition == true)
            {
                strtoken += ",1";
            }
            else
            {
                strtoken += ",0";
            }
            // [questclear]
            strtoken += ",1";

            sw.WriteLine(strtoken);
        }
        for (int i = 0; i < Player_Quest.m_lQuestList_Complete_ELIMINATE_TYPE.Count; i++)
        {
            // [questcode][questtype]
            strtoken = Player_Quest.m_lQuestList_Complete_ELIMINATE_TYPE[i].m_nQuestOrder.ToString() + "," + Player_Quest.m_lQuestList_Complete_ELIMINATE_TYPE[i].m_nQuest_Code.ToString() + "," + Player_Quest.m_lQuestList_Complete_ELIMINATE_TYPE[i].m_eQuestType.ToString();
            // [questprogress]
            strtoken += ",0";
            // [questcondition]
            if (Player_Quest.m_lQuestList_Complete_ELIMINATE_TYPE[i].m_bCondition == true)
            {
                strtoken += ",1";
            }
            else
            {
                strtoken += ",0";
            }
            // [questclear]
            strtoken += ",1";

            sw.WriteLine(strtoken);
        }

        sw.Flush();
        sw.Close();

        //Debug.Log("[플레이어가 진행중인 퀘스트 데이터 저장 완료.]");
        //Debug.Log("[플레이어가 완료한 퀘스트 데이터 저장 완료.]");
        return true;
    }
    // -----------------------------------------------------------------------------------------------------------------------------------------------------------

    // Player Status Data 불러오기.--------------------------------------------------------------------------------------------------------------------------------
    STATUS pstatusorigin, pstatus;
    SOC psocorigin, psoc;
    int exp, hp, mp;
    public bool Load_Player_Status(int num = 0)
    {
        if (num == 0)
        {
            if (Load_Player_Status_Use_Item_Use() == false)
                return false;
        }
        if (Load_Player_Status_SS() == false)
            return false;

        return true;
    }
    // Player Status 사용중인 아이템(Item_Use) 정보 저장.
    bool Load_Player_Status_Use_Item_Use()
    {
        //textasset = Resources.Load<TextAsset>("GameData/Player_Status_Use_Item_Use");
        //StringReader sr = new StringReader(textasset.text);
        //StreamReader sr = new StreamReader("./GameData/Player_Status_Use_Item_Use.csv");
        //StreamReader sr = new StreamReader("C:/Users/USER/AppData/LocalLow/Free For All/GameData/Player_Status_Use_Item_Use.csv");
        StreamReader sr = new StreamReader(m_sDepot_C + "Player_Status_Use_Item_Use.csv");

        sr.ReadLine();
        sr.ReadLine();
        sr.ReadLine();

        if (!sr.EndOfStream)
        {
            //strtoken = sr.ReadLine();
            strtoken = "";
            while (!sr.EndOfStream)
            {
                strtoken = sr.ReadLine();
                itemdata = strtoken.Split(',');
                Player_Total.Instance.m_ps_Status.Load_ApplyPotion(ItemManager.instance.m_Dictionary_MonsterDrop_Use[int.Parse(itemdata[0])], float.Parse(itemdata[1]) + 11f, float.Parse(itemdata[2]) + 11f);

            }
        }

        //Debug.Log("[플레이어에게 적용중인 버프 아이템 로드 완료.]");
        return true;
    }
    // Player Status 능력치 불러오기.
    bool Load_Player_Status_SS()
    {
        StreamReader sr;

        //textasset = Resources.Load<TextAsset>("GameData/Player_Status_SS");
        //StringReader sr = new StringReader(textasset.text);
        //sr = new StreamReader("./GameData/Player_Status_SS.csv");
        //sr = new StreamReader("C:/Users/USER/AppData/LocalLow/Free For All/GameData/Player_Status_SS.csv");
        sr = new StreamReader(m_sDepot_C + "Player_Status_SS.csv");

        if (!sr.EndOfStream)
        {
            strtoken = sr.ReadLine();
            strtoken = sr.ReadLine();
            strtoken = sr.ReadLine();
            strtoken = sr.ReadLine();
            strtoken = sr.ReadLine();
            strtoken = sr.ReadLine();
            strtoken = sr.ReadLine();

            strtoken = sr.ReadLine();
            itemdata = strtoken.Split(',');
            pstatus = new STATUS(int.Parse(itemdata[0]), int.Parse(itemdata[1]), int.Parse(itemdata[2]),
                int.Parse(itemdata[3]), int.Parse(itemdata[4]), int.Parse(itemdata[5]), int.Parse(itemdata[6]),
                int.Parse(itemdata[7]), int.Parse(itemdata[8]), int.Parse(itemdata[9]), int.Parse(itemdata[10]),
                int.Parse(itemdata[11]), int.Parse(itemdata[12]), int.Parse(itemdata[13]), int.Parse(itemdata[14]),
                int.Parse(itemdata[15]), float.Parse(itemdata[16]));
            exp = int.Parse(itemdata[2]); hp = int.Parse(itemdata[4]); mp = int.Parse(itemdata[6]);

            strtoken = sr.ReadLine();
            itemdata = strtoken.Split(',');
            psoc = new SOC(int.Parse(itemdata[0]), int.Parse(itemdata[1]), int.Parse(itemdata[2]),
                int.Parse(itemdata[3]), int.Parse(itemdata[4]), int.Parse(itemdata[5]), int.Parse(itemdata[6]),
                int.Parse(itemdata[7]), int.Parse(itemdata[8]));

            strtoken = sr.ReadLine();
            itemdata = strtoken.Split(',');
            pstatusorigin = new STATUS(int.Parse(itemdata[0]), int.Parse(itemdata[1]), int.Parse(itemdata[2]),
                    int.Parse(itemdata[3]), int.Parse(itemdata[4]), int.Parse(itemdata[5]), int.Parse(itemdata[6]),
                    int.Parse(itemdata[7]), int.Parse(itemdata[8]), int.Parse(itemdata[9]), int.Parse(itemdata[10]),
                    int.Parse(itemdata[11]), int.Parse(itemdata[12]), int.Parse(itemdata[13]), int.Parse(itemdata[14]),
                    int.Parse(itemdata[15]), float.Parse(itemdata[16]));
            Player_Total.Instance.m_ps_Status.m_sStatus_Origin.SetSTATUS(pstatusorigin);

            strtoken = sr.ReadLine();
            itemdata = strtoken.Split(',');
            psocorigin = new SOC(int.Parse(itemdata[0]), int.Parse(itemdata[1]), int.Parse(itemdata[2]),
                    int.Parse(itemdata[3]), int.Parse(itemdata[4]), int.Parse(itemdata[5]), int.Parse(itemdata[6]),
                    int.Parse(itemdata[7]), int.Parse(itemdata[8]));
            Player_Total.Instance.m_ps_Status.m_sSoc_Origin.SetSOC(psocorigin);

            Player_Total.Instance.m_ps_Status.Update_Loading(exp, hp, mp, pstatus, psoc);
        }

        //Debug.Log("[플레이어 능력치 데이터 로드 완료.]");
        return true;
    }

    // Player Status Data 저장.
    public bool Save_Player_Status()
    {
        if (Save_Player_Status_Use_Item_Use() == false)
            return false;
        if (Save_Player_Status_SS() == false)
            return false;

        return true;
    }
    // Player Status 사용중인 아이템(Item_Use) 정보 저장.
    // [itemcode][remainingitemdurationtime][remainingitemcooltime]
    bool Save_Player_Status_Use_Item_Use()
    {
        //StreamWriter sw = new StreamWriter("C:/Users/USER/AppData/LocalLow/Free For All/GameData/Player_Status_Use_Item_Use.csv");
        StreamWriter sw = new StreamWriter(m_sDepot_C + "Player_Status_Use_Item_Use.csv"); 


        sw.WriteLine("[Player_Status_UseItem]");
        sw.WriteLine("[itemcode][remainingitemduration][remainingcooltime]");
        sw.WriteLine("0,1,2");

        //Debug.Log("현재 적용중인 버프 아이템 수: " + Player_Status.m_Dictionary_Item_Use_Buff.Count);
        foreach(KeyValuePair<int, Item_Use> ditem in Player_Status.m_Dictionary_Item_Use_Buff)
        {
            strtoken = ditem.Value.m_nItemCode.ToString();
            if (Player_Status.m_Dictionary_Item_Use_Buff_RemainingTime.ContainsKey(ditem.Value.m_nItemCode) == true)
                strtoken += "," + Player_Status.m_Dictionary_Item_Use_Buff_RemainingTime[ditem.Value.m_nItemCode].ToString();
            else
                strtoken += ",0";
            if (Player_Status.m_Dictionary_Item_Use_CoolTime_RemainingTime.ContainsKey(ditem.Value.m_nItemCode) == true)
                strtoken += "," + Player_Status.m_Dictionary_Item_Use_CoolTime_RemainingTime[ditem.Value.m_nItemCode].ToString();
            else
                strtoken += ",0";

            sw.WriteLine(strtoken);
        }
        sw.Flush();
        sw.Close();

        //Debug.Log("[플레이어에게 적용중인 버프 아이템 저장 완료.]");
        return true;
    }
    // Player Status 능력치 저장.
    // [Player_STATUS]
    // [Player_SOC]
    // [Player_STATUS_Original]
    // [Player_SOC_Original]
    bool Save_Player_Status_SS()
    {
        //StreamWriter sw = new StreamWriter("C:/Users/USER/AppData/LocalLow/Free For All/GameData/Player_Status_SS.csv");
        StreamWriter sw = new StreamWriter(m_sDepot_C + "Player_Status_SS.csv");

        sw.WriteLine("[Player_Status_SS]");
        sw.WriteLine("[Player_STATUS]");
        sw.WriteLine("[Player_SOC]");
        sw.WriteLine("[Player_STATUS_Original]");
        sw.WriteLine("[Player_SOC_Original]");
        sw.WriteLine("0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16");
        sw.WriteLine("0,1,2,3,4,5,6,7,8");

        strtoken = "";

        // [Player_Status]
        strtoken = Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_Data();
        sw.WriteLine(strtoken);
        // [Player_Soc]
        strtoken = Player_Total.Instance.m_ps_Status.m_sSoc.GetSOC_Data();
        sw.WriteLine(strtoken);
        // [Player_Status_Original]
        strtoken = Player_Total.Instance.m_ps_Status.m_sStatus_Origin.GetSTATUS_Data();
        sw.WriteLine(strtoken);
        // [Player_Soc_Original]
        strtoken = Player_Total.Instance.m_ps_Status.m_sSoc_Origin.GetSOC_Data();
        sw.WriteLine(strtoken);

        sw.Flush();
        sw.Close();

        //Debug.Log("[플레이어 능력치 데이터 저장 완료.]");
        return true;
    }
    // -----------------------------------------------------------------------------------------------------------------------------------------------------------

    // Player Map Data 불러오기.-----------------------------------------------------------------------------------------------------------------------------------
    // [mapcode][scenecode][transform.position[x,y,z]]
    string scenename;
    int scenecode, mapcode;
    // (-7, 0.5f, 0)
    Vector3 playerposition;
    public bool Load_Player_Map()
    {
        //textasset = Resources.Load<TextAsset>("GameData/Player_Map");
        //StringReader sr = new StringReader(textasset.text);
        //StreamReader sr = new StreamReader("./GameData/Player_Map.csv");
        //StreamReader sr = new StreamReader("C:/Users/USER/AppData/LocalLow/Free For All/GameData/Player_Map.csv");
        StreamReader sr = new StreamReader(m_sDepot_C + "Player_Map.csv");

        if (!sr.EndOfStream)
        {
            sr.ReadLine();
            sr.ReadLine();
            sr.ReadLine();

            strtoken = sr.ReadLine();
            itemdata = strtoken.Split(',');

            switch (scenecode = int.Parse(itemdata[1]))
            {
                case 1:
                    {
                        scenename = "Scenes_Tutorial";
                    } break;
                case 2:
                    {
                        scenename = "Scenes_Chapter1";
                    } break;
            }
            Total_Manager.Instance.m_nSceneNumber = scenecode - 1;
            mapcode = int.Parse(itemdata[0]);
            playerposition = new Vector3(float.Parse(itemdata[2]), float.Parse(itemdata[3]), float.Parse(itemdata[4]));
        }

        //Debug.Log("[플레이어 위치 데이터 로드 완료.]");
        return true;
    }

    public bool Save_Player_Map()
    {
        //StreamWriter sw = new StreamWriter("C:/Users/USER/AppData/LocalLow/Free For All/GameData/Player_Map.csv");
        StreamWriter sw = new StreamWriter(m_sDepot_C + "Player_Map.csv");

        sw.WriteLine("[Player_Map]");
        sw.WriteLine("[mapcode][scenecode][transform.position[x.y.z]]");
        sw.WriteLine("0,1,2,3,4");

        Map map = Player_Total.Instance.m_pm_Map.m_Map;

        strtoken = "";
        strtoken += map.GetMapCode().ToString();
        strtoken += ',' + map.GetSceneCode().ToString();
        strtoken += ',' + Player_Total.Instance.m_pm_Move.m_tTransform.position.x.ToString();
        strtoken += ',' + Player_Total.Instance.m_pm_Move.m_tTransform.position.y.ToString();
        strtoken += ',' + Player_Total.Instance.m_pm_Move.m_tTransform.position.z.ToString();

        sw.WriteLine(strtoken);

        sw.Flush();
        sw.Close();

        //Debug.Log("[플레이어 위치 데이터 저장 완료.]");
        return true;
    }

    // Monster Dictionary 불러오기.--------------------------------------------------------------------------------------------------------------------------------
    // 순서 주의!
    public bool Load_Player_Dictionary_Monster()
    {
        //StreamReader sr = new StreamReader("C:/Users/USER/AppData/LocalLow/Free For All/GameData/Player_Dictionary_Monster.csv");
        StreamReader sr = new StreamReader(m_sDepot_C + "Player_Dictionary_Monster.csv");

        if (!sr.EndOfStream)
        {
            sr.ReadLine();
            sr.ReadLine();
            sr.ReadLine();

            while (!sr.EndOfStream)
            {
                strtoken = sr.ReadLine();
                itemdata = strtoken.Split(',');

                if (MonsterManager.m_Dictionary_Monster.ContainsKey(int.Parse(itemdata[1])) == true)
                {
                    MonsterManager.m_Dictionary_Monster[int.Parse(itemdata[1])].m_nMonster_Dictionary_Solve_Current = int.Parse(itemdata[2]);
                }
                else
                {
                    //Debug.Log("[플레이어 몬스터 사전 데이터 로드 오류.]");
                    return false;
                }
            }
        }

        MonsterManager.Instance.Update_Monster_Dictionary_Solve_Rate();

        //Debug.Log("[플레이어 몬스터 사전 데이터 로드 완료.]");
        return true;
    }
    // Monster Dictionary 저장.
    // 순서 주의!
    public bool Save_Player_Dictionary_Monster()
    {
        //StreamWriter sw = new StreamWriter("C:/Users/USER/AppData/LocalLow/Free For All/GameData/Player_Dictionary_Monster.csv");
        StreamWriter sw = new StreamWriter(m_sDepot_C + "Player_Dictionary_Monster.csv");

        sw.WriteLine("[Player_Dictionary_Monster]");
        sw.WriteLine("[monstername][monstercode][monstersolvecurrent]");
        sw.WriteLine("0,1,2");

        strtoken = "";
        foreach (KeyValuePair<int, MonsterDictionary> dictionary in MonsterManager.m_Dictionary_Monster)
        {
            strtoken = dictionary.Value.m_sMonster_Name;
            strtoken += "," + dictionary.Value.m_nMonster_Code.ToString();
            strtoken += "," + dictionary.Value.m_nMonster_Dictionary_Solve_Current;
            sw.WriteLine(strtoken);
        }
        sw.Flush();
        sw.Close();

        MonsterManager.Instance.Update_Monster_Dictionary_Solve_Rate();

        //Debug.Log("[플레이어 몬스터 사전 데이터 저장 완료.]");
        return true;
    }
    // -----------------------------------------------------------------------------------------------------------------------------------------------------------

    // Player Quickslot Data 불러오기.-----------------------------------------------------------------------------------------------------------------------------
    // [quickslotcode][quickslotsign][itemcategory][itemcode]
    // [quickslotcode]: 아이템이 등록된 퀵슬롯 번호.
    // [quickslotsign]: 퀵슬롯에 아이템이 등록되었는지 여부. quickslotsign == 0: 등록 X, quickslotsign == 1: 등록 O.
    // [itemcategory]: 퀵슬롯에 등록된 아이템 분류.
    // [itemcode]: 퀵슬롯에 등록된 아이템 코드. 장비 아이템: 아이템 슬롯의 해당 장비 아이템 배열 넘버, 소비, 기타 아이템: 해당 아이템 코드.
    public bool Load_Player_Quickslot()
    {
        StreamReader sr = new StreamReader(m_sDepot_C + "Player_Quickslot.csv");

        if (!sr.EndOfStream)
        {
            sr.ReadLine();
            sr.ReadLine();
            sr.ReadLine();

            while (!sr.EndOfStream)
            {
                strtoken = sr.ReadLine();
                itemdata = strtoken.Split(',');

                if (itemdata[2] == "NULL")
                {
                    
                }
                else if (itemdata[2] == "EQUIP")
                {
                    GUIManager_Total.Instance.Set_Quickslot(int.Parse(itemdata[0]), E_QUICKSLOT_CATEGORY.EQUIP, int.Parse(itemdata[3]));
                }
                else if (itemdata[2] == "USE")
                {
                    GUIManager_Total.Instance.Set_Quickslot(int.Parse(itemdata[0]), E_QUICKSLOT_CATEGORY.USE, int.Parse(itemdata[3]));
                }
                else if (itemdata[2] == "ETC")
                {
                    GUIManager_Total.Instance.Set_Quickslot(int.Parse(itemdata[0]), E_QUICKSLOT_CATEGORY.ETC, int.Parse(itemdata[3]));
                }
            }
        }

        MonsterManager.Instance.Update_Monster_Dictionary_Solve_Rate();

        //Debug.Log("[플레이어 몬스터 사전 데이터 로드 완료.]");
        return true;
    }

    public bool Save_Player_Quickslot()
    {
        StreamWriter sw = new StreamWriter(m_sDepot_C + "Player_Quickslot.csv");

        sw.WriteLine("[Player_Quickslot]");
        sw.WriteLine("[quickslotcode][quickslotsign][itemcategory][itemcode]");
        sw.WriteLine("0, 1, 2, 3");

        for (int i = 0; i < GUIManager_Total.Instance.m_GUI_Quickslot.m_gList_Quickslot.Count; i++)
        {
            strtoken = GUIManager_Total.Instance.m_GUI_Quickslot.m_qsList_Quickslot[i].m_nQuickslot_Number.ToString();

            if (GUIManager_Total.Instance.m_GUI_Quickslot.m_qsList_Quickslot[i].m_eQuickslot_Category == E_QUICKSLOT_CATEGORY.NULL)
                strtoken += ",0," + GUIManager_Total.Instance.m_GUI_Quickslot.m_qsList_Quickslot[i].m_eQuickslot_Category + ",-1";
            else if (GUIManager_Total.Instance.m_GUI_Quickslot.m_qsList_Quickslot[i].m_eQuickslot_Category == E_QUICKSLOT_CATEGORY.EQUIP)
            {
                strtoken += ",1," + GUIManager_Total.Instance.m_GUI_Quickslot.m_qsList_Quickslot[i].m_eQuickslot_Category + "," + GUIManager_Total.Instance.m_GUI_Quickslot.m_qsList_Quickslot[i].m_nItem_Equip_AryNumber.ToString();
            }
            else if (GUIManager_Total.Instance.m_GUI_Quickslot.m_qsList_Quickslot[i].m_eQuickslot_Category == E_QUICKSLOT_CATEGORY.USE)
            {
                strtoken += ",1," + GUIManager_Total.Instance.m_GUI_Quickslot.m_qsList_Quickslot[i].m_eQuickslot_Category + "," + GUIManager_Total.Instance.m_GUI_Quickslot.m_qsList_Quickslot[i].m_nItem_Use_Code.ToString();
            }
            else if (GUIManager_Total.Instance.m_GUI_Quickslot.m_qsList_Quickslot[i].m_eQuickslot_Category == E_QUICKSLOT_CATEGORY.ETC)
            {
                strtoken += ",1," + GUIManager_Total.Instance.m_GUI_Quickslot.m_qsList_Quickslot[i].m_eQuickslot_Category + ","  + GUIManager_Total.Instance.m_GUI_Quickslot.m_qsList_Quickslot[i].m_nItem_Etc_Code.ToString();
            }

            sw.WriteLine(strtoken);
        }

        sw.Flush();
        sw.Close();

        //Debug.Log("[플레이어 위치 데이터 저장 완료.]");
        return true;
    }
}
