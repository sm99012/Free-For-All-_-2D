using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_Canvas_GUI : MonoBehaviour
{
    private static GUI_Canvas_GUI instance = null;

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

        this.gameObject.SetActive(false);
    }
    public static GUI_Canvas_GUI Instance
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
}
