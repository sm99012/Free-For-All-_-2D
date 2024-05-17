using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection : MonoBehaviour
{
    // 채집물 이름.
    public string m_sCollectionName;

    public int m_nCollectionCode;

    protected ParticleSystem m_gParticleSystem;
    protected Color m_Color_Start_ParticleSystemColor;

    protected List<SpriteRenderer> m_List_SpriteRenderer;
    protected List<Color> m_List_Color_Start_SpriteRendererColor;

    // 채집물 Sprite 가 저장된 Object.
    public GameObject m_gObject;

    // 드랍 채집물 정보.
    protected List<DATA_Collection_Item_Equip> m_List_DropCollection_Item_Equip;
    protected List<DATA_Collection_Item_Use> m_List_DropCollection_Item_Use;
    protected List<DATA_Collection_Item_Etc> m_List_DropCollection_Item_Etc;

    protected Vector3 m_vItemPos;
    protected Vector3 m_vItemOffset;

    // Collection 생성 위치정보.
    protected List<Vector3> m_vList_CollectionPos;

    protected int m_nRandomNum;

    protected float m_fAlpha;

    protected bool m_bPossible;

    protected int m_nResponeTime;

    virtual public void InitialSet()
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

        m_nResponeTime = 5;
    }

    public void DropItem(Vector3 pos)
    {
        if (m_bPossible == true)
        {
            m_bPossible = false;
            for (int i = 0; i < m_List_DropCollection_Item_Equip.Count; i++)
            {
                m_nRandomNum = Random.Range(1, 10001);
                if (m_nRandomNum <= m_List_DropCollection_Item_Equip[i].GetDropPercent())
                {
                    Item item = new Item_Equip(m_List_DropCollection_Item_Equip[i].GetCollectionItem(), this.transform.position + m_vItemPos);
                    Destroy(item);
                    m_vItemPos += m_vItemOffset;
                }
            }
            for (int i = 0; i < m_List_DropCollection_Item_Use.Count; i++)
            {
                m_nRandomNum = Random.Range(1, 10001);
                if (m_nRandomNum <= m_List_DropCollection_Item_Use[i].GetDropPercent())
                {
                    Item item = new Item_Use(m_List_DropCollection_Item_Use[i].GetCollectionItem(), this.transform.position + m_vItemPos);
                    Destroy(item);
                    m_vItemPos += m_vItemOffset;
                }
            }
            for (int i = 0; i < m_List_DropCollection_Item_Etc.Count; i++)
            {
                m_nRandomNum = Random.Range(1, 10001);
                if (m_nRandomNum <= m_List_DropCollection_Item_Etc[i].GetDropPercent())
                {
                    Item item = new Item_Etc(m_List_DropCollection_Item_Etc[i].GetCollectionItem(), this.transform.position + m_vItemPos);
                    Destroy(item);
                    m_vItemPos += m_vItemOffset;
                }
            }
            StartCoroutine(ProcessDestroy(m_nResponeTime));
        }
    }

    public IEnumerator ProcessDestroy(float time)
    {
        // Destroy
        m_fAlpha = 1;
        while (m_fAlpha > 0)
        {
            for (int i = 0; i < m_List_SpriteRenderer.Count; i++)
            {
                m_List_SpriteRenderer[i].color = new Color(m_List_Color_Start_SpriteRendererColor[i].r, m_List_Color_Start_SpriteRendererColor[i].g, m_List_Color_Start_SpriteRendererColor[i].b, m_fAlpha);
            }
            m_gParticleSystem.startColor = new Color(m_Color_Start_ParticleSystemColor.r, m_Color_Start_ParticleSystemColor.g, m_Color_Start_ParticleSystemColor.b, m_fAlpha);
            m_fAlpha -= Time.deltaTime;
            yield return null;
        }
        //this.gameObject.SetActive(false);
        m_bPossible = false;
        m_gParticleSystem.Stop();
        //this.gameObject.SetActive(false);

        //Debug.Log("Collection Respone - X");
        yield return new WaitForSeconds(time);
        //Debug.Log("Collection Respone - O");

        // Resopne
        m_fAlpha = 0;
        Respone();
        while (m_fAlpha < 1)
        {
            for (int i = 0; i < m_List_SpriteRenderer.Count; i++)
            {
                m_List_SpriteRenderer[i].color = new Color(m_List_Color_Start_SpriteRendererColor[i].r, m_List_Color_Start_SpriteRendererColor[i].g, m_List_Color_Start_SpriteRendererColor[i].b, m_fAlpha);
            }
            m_gParticleSystem.startColor = new Color(m_Color_Start_ParticleSystemColor.r, m_Color_Start_ParticleSystemColor.g, m_Color_Start_ParticleSystemColor.b, m_fAlpha);
            m_fAlpha += Time.deltaTime;
            yield return null;
        }
        m_gParticleSystem.startColor = new Color(m_Color_Start_ParticleSystemColor.r, m_Color_Start_ParticleSystemColor.g, m_Color_Start_ParticleSystemColor.b, 1);
        //this.gameObject.SetActive(true);
        m_bPossible = true;
        Debug.Log("Collection Respone - OO");
    }

    public void Respone()
    {
        m_nRandomNum = Random.Range(0, m_vList_CollectionPos.Count);
        this.transform.position = m_vList_CollectionPos[m_nRandomNum];
        this.gameObject.SetActive(true);
        m_gParticleSystem.Play();
    }

    public void SetCollectionPos(List<Vector3> listv3)
    {
        m_vList_CollectionPos = new List<Vector3>();

        for (int i = 0; i < listv3.Count; i++)
        {
            m_vList_CollectionPos.Add(listv3[i]);
        }
    }
}

public class DATA_Collection_Item_Equip
{
    Item_Equip m_Collection_Item;
    // 0 .. 10000
    int m_nDropPercent;

    public DATA_Collection_Item_Equip(Item_Equip item, int droppercent)
    {
        this.m_Collection_Item = item;
        this.m_nDropPercent = droppercent;
    }

    public Item_Equip GetCollectionItem()
    {
        return m_Collection_Item;
    }
    public int GetDropPercent()
    {
        return m_nDropPercent;
    }
}

public class DATA_Collection_Item_Use
{
    Item_Use m_Collection_Item;
    // 0 .. 10000
    int m_nDropPercent;

    public DATA_Collection_Item_Use(Item_Use item, int droppercent)
    {
        this.m_Collection_Item = item;
        this.m_nDropPercent = droppercent;
    }

    public Item_Use GetCollectionItem()
    {
        return m_Collection_Item;
    }
    public int GetDropPercent()
    {
        return m_nDropPercent;
    }
}

public class DATA_Collection_Item_Etc
{
    Item_Etc m_Collection_Item;
    // 0 .. 10000
    int m_nDropPercent;

    public DATA_Collection_Item_Etc(Item_Etc item, int droppercent)
    {
        this.m_Collection_Item = item;
        this.m_nDropPercent = droppercent;
    }

    public Item_Etc GetCollectionItem()
    {
        return m_Collection_Item;
    }
    public int GetDropPercent()
    {
        return m_nDropPercent;
    }
}