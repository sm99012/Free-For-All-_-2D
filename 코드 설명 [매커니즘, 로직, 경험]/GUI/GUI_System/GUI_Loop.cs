using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_Loop : MonoBehaviour
{
    public GameObject m_g_LoopBox;
    public GameObject m_g_Loop_Content;

    public GameObject m_g_Btn_Exit;

    public List<GameObject> m_lg_PanelList;


    public void Display_GUI_Loop()
    {
        if (m_g_LoopBox.activeSelf == true)
        {
            for (int i = 0; i < m_lg_PanelList.Count; i++)
            {
                Destroy(m_lg_PanelList[i].gameObject);
            }
            m_lg_PanelList.Clear();

            m_g_LoopBox.SetActive(false);
        }
        else
        {
            InitialSet_GUI_Loop();
            m_g_LoopBox.SetActive(true);
        }
    }

    public void InitialSet_GUI_Loop()
    {
        Init();
        GameObject P_obj = Resources.Load("Prefab/GUI/Panel_Loop_Content") as GameObject;
        for (int i = 0; i < Loop.Instance.m_ll_LoopDataList.Count; i++)
        {
            GameObject copyobj = Instantiate(P_obj);
            copyobj.GetComponent<CreatePanel_Loop>().SetInfo(i);
            RectTransform panelpos = copyobj.GetComponent<RectTransform>();
            panelpos.SetParent(m_g_Loop_Content.transform);
            panelpos.transform.localScale = new Vector3(1, 1, 1);
            panelpos.localPosition = new Vector3(panelpos.localPosition.x, panelpos.localPosition.y, 0);
            m_lg_PanelList.Add(copyobj);
        }
    }

    public void Init()
    {
        for (int i = 0; i < m_lg_PanelList.Count; i++)
        {
            Destroy(m_lg_PanelList[i].gameObject);
        }
        m_lg_PanelList.Clear();
    }

    public void Btn_Exit()
    {
        for (int i = 0; i < m_lg_PanelList.Count; i++)
        {
            Destroy(m_lg_PanelList[i].gameObject);
        }
        m_lg_PanelList.Clear();

        m_g_LoopBox.SetActive(false);
    }
}
