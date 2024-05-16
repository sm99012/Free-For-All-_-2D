using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSlime1_Drop : Monster_Drop
{
    public GameObject obj;
    override public void Start()
    {
        InitialSet();
    }

    override public void InitialSet()
    {
        m_vItempos = new Vector3(0, 0, 0);
        m_vItemoffset = new Vector3(0.001f, 0, 0);

        //obj = Resources.Load("Prefab/Item/Item_Etc/Item_Medicien1") as GameObject;
        ////Debug.Log(obj.name);
        //m_List_DropItem.Add(obj);
        //m_List_DropPercent.Add(100);

        //m_List_DropItem_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0006]);
        //m_List_DropPercent_Etc.Add(10000);
    }

}
