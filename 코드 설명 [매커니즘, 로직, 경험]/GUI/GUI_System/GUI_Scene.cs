using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUI_Scene : MonoBehaviour
{
    [SerializeField] GameObject m_gPanel_Loading;
    [SerializeField] GameObject m_gPanel_LoadingInformation;

    [SerializeField] GameObject m_gPanel_Background_Tutorial;
    [SerializeField] GameObject m_gPanel_Background_Chapter1;

    public void InitialSet()
    {
        InitialSet_Object();
    }

    void InitialSet_Object()
    {
        m_gPanel_Loading = GameObject.Find("Canvas_Loading").transform.Find("Panel_Loading").gameObject;

        m_gPanel_LoadingInformation = m_gPanel_Loading.transform.Find("Panel_LoadingInformation").gameObject;

        m_gPanel_Background_Tutorial = m_gPanel_LoadingInformation.transform.Find("Panel_Background_Tutorial").gameObject;
        m_gPanel_Background_Chapter1 = m_gPanel_LoadingInformation.transform.Find("Panel_Background_Chapter1").gameObject;
    }

    public void Display_Scene(int scenenumber)
    {
        switch (scenenumber)
        {
            case 0: case 1:
                {
                    m_gPanel_Background_Tutorial.SetActive(true);
                    m_gPanel_Background_Chapter1.SetActive(false);
                } break;
            case 2:
                {
                    m_gPanel_Background_Tutorial.SetActive(false);
                    m_gPanel_Background_Chapter1.SetActive(true);
                } break;
        }
    }
}
