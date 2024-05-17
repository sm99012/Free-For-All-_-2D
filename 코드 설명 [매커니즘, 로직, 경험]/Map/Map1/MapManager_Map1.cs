using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager_Map1 : MonoBehaviour
{
    public Tilemap m_Tilemap_CollectionPos;
    
    List<Vector3> m_vList_CollectionPos;

    int m_nCollectionCount;
    int m_nRandomNumber;

    private void Start()
    {
        InitialSet();
    }

    void InitialSet()
    {
        m_vList_CollectionPos = new List<Vector3>();

        m_nCollectionCount = 5;

        BoundsInt bi = m_Tilemap_CollectionPos.cellBounds;
        Vector3 min = new Vector3(bi.min.x, bi.min.y, 0);
        Vector3 max = new Vector3(bi.max.x, bi.max.y, 0);

        Vector3Int pos;

        for (int y = (int)min.y; y < (int)max.y; y++)
        {
            for (int x = (int)min.x; x < (int)max.x; x++)
            {
                pos = new Vector3Int(x, y, 0);
                if (m_Tilemap_CollectionPos.HasTile(pos) == true)
                {
                    m_vList_CollectionPos.Add(m_Tilemap_CollectionPos.CellToWorld(pos));
                }
            }
        }

        // 생성할 Collection.
        GameObject copyobj = Resources.Load<GameObject>("Prefab/MapObject/Collection_Plants_1");
        // 랜덤 생성 Collection 위치.

        for (int i = 0; i < m_nCollectionCount; i++)
        {
            m_nRandomNumber = Random.Range(0, m_vList_CollectionPos.Count);
            GameObject obj = Instantiate(copyobj);
            obj.GetComponent<Collection>().SetCollectionPos(m_vList_CollectionPos);
            obj.transform.position = m_vList_CollectionPos[m_nRandomNumber];
        }
    }
}
