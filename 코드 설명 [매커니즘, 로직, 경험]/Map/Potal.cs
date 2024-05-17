using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potal : MonoBehaviour
{
    public Vector3 m_vPotalPos;

    public GameObject m_gDestinationPotal;
    public Map m_Map_Destination;
    public Vector3 m_vDestinationPos;
    public int m_nMapCode_Destination;

    public int m_nMapCode;
    public string m_sMapName;
    public Map m_Map = null;

    public bool m_bUP = false;
    public bool m_bRIGHT = false;
    public bool m_bDOWN = false;
    public bool m_bLEFT = false;

    public bool m_bNORMAL = false;
    public bool m_bFULL = false;
    public bool m_bZOOMIN = false;
    public bool m_bSPECIAL = false;

    // 씬 이동용.
    public bool m_bChangeScene;
    public int m_nChangeSceneNumber;

    public Vector3 m_vCenterPos;
    Vector3 m_vRevision;

    private void Awake()
    {
        m_vPotalPos = this.gameObject.transform.position;

        if (m_gDestinationPotal != null)
            m_vDestinationPos = m_gDestinationPotal.transform.position;
        else { }

        if (m_bUP == true)
        {
            m_vRevision = Vector3.up * 0.2f;
        }
        if (m_bRIGHT == true)
        {
            m_vRevision = Vector3.right * 0.2f + Vector3.down * 0.1f;
        }
        if (m_bDOWN == true)
        {
            m_vRevision = Vector3.down * 0.2f;
        }
        if (m_bLEFT == true)
        {
            m_vRevision = Vector3.left * 0.2f + Vector3.down * 0.1f;
        }
    }

    private void Start()
    {
        StartCoroutine(ProcessLinkMap());

        if (m_nMapCode != 0)
        {
            if (MapManager.Instance.m_Dictionary_Map_Tutorial.ContainsKey(m_nMapCode))
            {
                m_Map = MapManager.Instance.m_Dictionary_Map_Tutorial[m_nMapCode];
                m_sMapName = m_Map.GetMapName();
            }
            else if (MapManager.Instance.m_Dictionary_Map_Chapter1.ContainsKey(m_nMapCode))
            {
                m_Map = MapManager.Instance.m_Dictionary_Map_Chapter1[m_nMapCode];
                m_sMapName = m_Map.GetMapName();
            }
        }
    }

    // 임의 조정 필요.
    IEnumerator ProcessLinkMap()
    {
        yield return null;
        if (m_gDestinationPotal != null)
        {
            m_Map_Destination = m_gDestinationPotal.GetComponent<Potal>().m_Map;
        }
        else
        {
            switch (m_nMapCode_Destination)
            {
                case 0003:
                    {
                        m_Map_Destination = MapManager.Instance.m_Dictionary_Map_Tutorial[0003];
                    }
                    break;
                case 0005:
                    {
                        m_Map_Destination = MapManager.Instance.m_Dictionary_Map_Chapter1[0005];
                    }
                    break;
            }
        }
    }
        
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Player")
        {
            if (m_bChangeScene == false)
            {
                if (m_cProcess_ChangeMap == null)
                {
                    m_cProcess_ChangeMap = StartCoroutine(Process_ChangeMap(trigger));
                }
            }
            else
            {
                if (m_cProcess_ChangeScene == null)
                {
                    m_cProcess_ChangeScene = StartCoroutine(Process_ChangeScene(trigger));
                    //MapManager.Instance.ChangeScene(m_nChangeSceneNumber); 
                }
            }
        }
    }
    static Coroutine m_cProcess_ChangeMap;
    IEnumerator Process_ChangeMap(Collider2D trigger)
    {
        GUIManager_Total.Instance.Update_ChangeMap();

        yield return new WaitForSeconds(1.5f);

        trigger.gameObject.transform.position = m_vDestinationPos + m_vRevision;
        trigger.gameObject.GetComponent<Player_Total>().ChangeMap(true, m_Map_Destination);

        if (m_bNORMAL == true)
        {
            Player_Total.Instance.m_pc_Camera.SetCamera_NORMAL();
        }
        else if (m_bFULL == true)
        {
            Player_Total.Instance.m_pc_Camera.SetCamera_FULL(m_vCenterPos);
        }
        else if (m_bZOOMIN == true)
        {

        }
        else
        {

        }

        MapManager.Instance.Update_Map_Object(m_Map, m_Map_Destination);

        m_cProcess_ChangeMap = null;
    }
    static Coroutine m_cProcess_ChangeScene;
    IEnumerator Process_ChangeScene(Collider2D trigger)
    {
        GUIManager_Total.Instance.Update_ChangeMap();

        yield return new WaitForSeconds(1.5f);

        switch (m_nChangeSceneNumber)
        {
            case 0:
                {
                    Total_Manager.Instance.LoadScene("Scenes_Tutorial", 0);
                    Total_Manager.Instance.m_nSceneNumber = 0;
                } break;
            case 1:
                {
                    Total_Manager.Instance.LoadScene("Scenes_Tutorial", 1);
                    Total_Manager.Instance.m_nSceneNumber = 0;
                } break;
            case 2:
                {
                    Total_Manager.Instance.LoadScene("Scenes_Chapter1", 2);
                    Total_Manager.Instance.m_nSceneNumber = 1;
                } break;
        }

        //trigger.gameObject.transform.position = m_vDestinationPos + m_vRevision;

        //if (m_bNORMAL == true)
        //{
        //    Player_Total.Instance.m_pc_Camera.SetCamera_NORMAL();
        //}
        //else if (m_bFULL == true)
        //{
        //    Player_Total.Instance.m_pc_Camera.SetCamera_FULL(m_vCenterPos);
        //}
        //else if (m_bZOOMIN == true)
        //{

        //}
        //else
        //{

        //}

        m_cProcess_ChangeScene = null;
    }
}
