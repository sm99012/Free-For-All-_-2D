using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime1_RedHat1_Drop : Monster_Drop
{
    public GameObject obj;
    override public void Start()
    {
        InitialSet();
    }

    override public void InitialSet()
    {
        EssentialSet();

        m_vItempos = new Vector3(0, 0, 0);
        m_vItemoffset = new Vector3(0.001f, 0, 0);


        //m_List_DropItem_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1001]);
        //m_List_DropPercent_Equip.Add(50);
        //m_List_DropItem_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1004]);
        //m_List_DropPercent_Equip.Add(50);
        //m_List_DropItem_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1301]);
        //m_List_DropPercent_Equip.Add(50);
        //m_List_DropItem_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1304]);
        //m_List_DropPercent_Equip.Add(50);
        //m_List_DropItem_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1601]);
        //m_List_DropPercent_Equip.Add(50);
        //m_List_DropItem_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1604]);
        //m_List_DropPercent_Equip.Add(50);

        //m_List_DropItem_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[3002]);
        //m_List_DropPercent_Equip.Add(50);
        //m_List_DropItem_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[4002]);
        //m_List_DropPercent_Equip.Add(50);
        //m_List_DropItem_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[6002]);
        //m_List_DropPercent_Equip.Add(50);

        //m_List_DropItem_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0001]);
        //m_List_DropPercent_Etc.Add(3000);
        //m_List_DropItem_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0002]);
        //m_List_DropPercent_Etc.Add(500);
        //m_List_DropItem_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0005]);
        //m_List_DropPercent_Etc.Add(7500);
        //m_List_DropItem_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0011]);
        //m_List_DropPercent_Etc.Add(1000);
        //m_List_DropItem_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0012]);
        //m_List_DropPercent_Etc.Add(100);

        //m_List_DropItem_Gold_Min.Add(4);
        //m_List_DropItem_Gold_Max.Add(8);
        //m_List_DropPercent_Gold.Add(5000);
    }
}
