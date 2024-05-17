using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUI_Canvas_Game : MonoBehaviour
{
    [SerializeField] GameObject m_gPanel_Game;
    [SerializeField] Button m_BTN_Game_Start;
    [SerializeField] Button m_BTN_Game_End;

    private void Awake()
    {
        InitialSet();
    }

    void InitialSet()
    {
        InitialSet_Object();
        InitialSet_Button();
    }

    void InitialSet_Object()
    {
        m_gPanel_Game = this.gameObject.transform.Find("Panel_Game").gameObject;
        m_BTN_Game_Start = m_gPanel_Game.transform.Find("BTN_Game_Start").gameObject.GetComponent<Button>();
        m_BTN_Game_End = m_gPanel_Game.transform.Find("BTN_Game_End").gameObject.GetComponent<Button>();
    }

    void InitialSet_Button()
    {
        m_BTN_Game_Start.onClick.RemoveAllListeners();
        m_BTN_Game_Start.onClick.AddListener(delegate { Set_BTN_Press_Game_Start(); });
        m_BTN_Game_End.onClick.RemoveAllListeners();
        m_BTN_Game_End.onClick.AddListener(delegate { Set_BTN_Press_Game_End(); });
    }
    void Set_BTN_Press_Game_Start()
    {
        // InitialSet
        //Total_Manager.Instance.GameStart("Scenes_Tutorial", 1, new Vector3(-7, 0.5f, 0), 1);
        DataManager.instance.GameStart();
    }
    void Set_BTN_Press_Game_End()
    {
        Application.Quit();
    }
}
