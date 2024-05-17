using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime1_GrassHair_Drop : Monster_Drop
{
    override public void Start()
    {
        InitialSet();
    }

    override public void InitialSet()
    {
        EssentialSet();

        m_vItempos = new Vector3(0, 0, 0);
        m_vItemoffset = new Vector3(0.001f, 0, 0);

        //m_List_DropItem_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1002]);
        //m_List_DropPercent_Equip.Add(50);
        //m_List_DropItem_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1005]);
        //m_List_DropPercent_Equip.Add(50);
        //m_List_DropItem_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1302]);
        //m_List_DropPercent_Equip.Add(50);
        //m_List_DropItem_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1305]);
        //m_List_DropPercent_Equip.Add(50);
        //m_List_DropItem_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1602]);
        //m_List_DropPercent_Equip.Add(50);
        //m_List_DropItem_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1605]);
        //m_List_DropPercent_Equip.Add(50);

        //m_List_DropItem_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[2001]);
        //m_List_DropPercent_Equip.Add(50);

        //m_List_DropItem_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0001]);
        //m_List_DropPercent_Etc.Add(3000);
        //m_List_DropItem_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0002]);
        //m_List_DropPercent_Etc.Add(500);
        //m_List_DropItem_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0011]);
        //m_List_DropPercent_Etc.Add(1000);
        //m_List_DropItem_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0012]);
        //m_List_DropPercent_Etc.Add(100);

        //m_List_DropItem_Gold_Min.Add(1);
        //m_List_DropItem_Gold_Max.Add(2);
        //m_List_DropPercent_Gold.Add(3000);
    }
}
