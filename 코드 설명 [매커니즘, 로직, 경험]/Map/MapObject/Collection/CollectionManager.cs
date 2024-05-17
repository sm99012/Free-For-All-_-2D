using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    private static CollectionManager instance = null;
    public static CollectionManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    public Dictionary<int, Collection> m_Dictionary_Collection;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        m_Dictionary_Collection = new Dictionary<int, Collection>();
    }

    public void InitialSet()
    {
        foreach (KeyValuePair<int, Collection> collection in m_Dictionary_Collection)
        {
            collection.Value.InitialSet();
        }
    }
}
