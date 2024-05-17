using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy Object = null;
    public static DontDestroy _Object
    {
        get
        {
            if (Object == null)
            {
                return null;
            }
            return Object;
        }
    }

    private void Awake()
    {
        if (Object == null)
        {
            Object = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
