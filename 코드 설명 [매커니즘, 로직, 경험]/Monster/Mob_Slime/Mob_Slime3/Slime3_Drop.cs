using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime3_Drop : Monster_Drop
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

        //m_List_DropItem_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0001]);
        //m_List_DropPercent_Etc.Add(2000);
        //m_List_DropItem_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0011]);
        //m_List_DropPercent_Etc.Add(500);

        //m_List_DropItem_Gold_Min.Add(1);
        //m_List_DropItem_Gold_Max.Add(2);
        //m_List_DropPercent_Gold.Add(1500);
    }
}
