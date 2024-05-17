using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush1_Drop : Monster_Drop
{
    override public void InitialSet()
    {
        EssentialSet();

        m_vItempos = new Vector3(0, 0, 0);
        m_vItemoffset = new Vector3(0.001f, 0, 0);

        //obj = Resources.Load("Prefab/Item/Item_Etc/Item_Ent1") as GameObject;
        //m_List_DropItem.Add(obj);
        //m_List_DropPercent.Add(75);
        //obj = Resources.Load("Prefab/Item/Item_Etc/Item_Apple1") as GameObject;
        //m_List_DropItem.Add(obj);
        //m_List_DropPercent.Add(20);
        //obj = Resources.Load("Prefab/Item/Item_Equip/Item_Axe1") as GameObject;
        //m_List_DropItem.Add(obj);
        //m_List_DropPercent.Add(5);
        //obj = Resources.Load("Prefab/Item/Item_Equip/Item_Axe2") as GameObject;
        //m_List_DropItem.Add(obj);
        //m_List_DropPercent.Add(3);
        //obj = Resources.Load("Prefab/Item/Item_Equip/Item_Sword3") as GameObject;
        //m_List_DropItem.Add(obj);
        //m_List_DropPercent.Add(3);

        //m_List_DropItem_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0014]);
        //m_List_DropPercent_Etc.Add(3000);

        //m_List_DropItem_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8011]);
        //m_List_DropPercent_Use.Add(100);
        //m_List_DropItem_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8013]);
        //m_List_DropPercent_Use.Add(500);
        //m_List_DropItem_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9000]);
        //m_List_DropPercent_Use.Add(100);

        //m_List_DropItem_Gold_Min.Add(1);
        //m_List_DropItem_Gold_Max.Add(2);
        //m_List_DropPercent_Gold.Add(1000);
    }
}
