using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection_Plants1 : Collection
{
    private void Awake()
    {
        m_gParticleSystem = transform.Find("Particle System").gameObject.GetComponent<ParticleSystem>();
        m_Color_Start_ParticleSystemColor = m_gParticleSystem.startColor;

        m_bPossible = true;

        m_vItemPos = new Vector3(0, 0, 0);
        m_vItemOffset = new Vector3(0.001f, 0, 0);

        m_sCollectionName = "'늙고 병든 슬라임' 의 약초 더미";
        m_nCollectionCode = 1;

        if (CollectionManager.Instance.m_Dictionary_Collection.ContainsKey(m_nCollectionCode) == false)
            CollectionManager.Instance.m_Dictionary_Collection.Add(m_nCollectionCode, this);
        InitialSet();
    }

    override public void InitialSet()
    {
        m_List_DropCollection_Item_Etc = new List<DATA_Collection_Item_Etc>();
        m_List_DropCollection_Item_Use = new List<DATA_Collection_Item_Use>();
        m_List_DropCollection_Item_Equip = new List<DATA_Collection_Item_Equip>();

        m_List_SpriteRenderer = new List<SpriteRenderer>();
        m_List_Color_Start_SpriteRendererColor = new List<Color>();
        for (int i = 0; i < m_gObject.transform.childCount; i++)
        {
            m_List_SpriteRenderer.Add(m_gObject.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>());
            m_List_Color_Start_SpriteRendererColor.Add(m_gObject.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().color);
        }

        m_List_DropCollection_Item_Etc.Add(new DATA_Collection_Item_Etc(ItemManager.instance.m_Dictionary_Collection_Etc[0017], 9000));
        m_List_DropCollection_Item_Etc.Add(new DATA_Collection_Item_Etc(ItemManager.instance.m_Dictionary_Collection_Etc[0016], 2000));
        m_List_DropCollection_Item_Etc.Add(new DATA_Collection_Item_Etc(ItemManager.instance.m_Dictionary_Collection_Etc[0013], 2000));

        m_List_DropCollection_Item_Use.Add(new DATA_Collection_Item_Use(ItemManager.instance.m_Dictionary_Collection_Use[9000], 100));
        m_List_DropCollection_Item_Use.Add(new DATA_Collection_Item_Use(ItemManager.instance.m_Dictionary_Collection_Use[10000], 1));

        m_nResponeTime = 5;
    }

}
