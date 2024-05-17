using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ents1_Drop : Monster_Drop
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
    }
}
