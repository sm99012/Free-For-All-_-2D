using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUI_Quickslot : MonoBehaviour
{
    [SerializeField] GameObject m_gPanel_Quickslot;
    [SerializeField] GameObject m_gPanel_Quickslot_Content;

    [SerializeField] public List<GameObject> m_gList_Quickslot;
    [SerializeField] public List<Quickslot> m_qsList_Quickslot;
    [SerializeField] public List<KeyCode> m_KeyCodeList_Quickslot;

    public void InitialSet()
    {
        InitialSet_Object();
        InitialSet_Button();
    }

    private void Update()
    {
        if (m_KeyCodeList_Quickslot != null)
        {
            for (int i = 0; i < m_KeyCodeList_Quickslot.Count; i++)
            {
                if (Input.GetKeyUp(m_KeyCodeList_Quickslot[i]))
                {
                    m_qsList_Quickslot[i].Use_Quickslot();
                }

                Update_Quickslot_CoolTime(i);
            }
        }
    }

    // 초기 Object 불러오기.
    void InitialSet_Object()
    {
        m_gPanel_Quickslot = GameObject.Find("Canvas_GUI").gameObject.transform.Find("Panel_Quickslot").gameObject;
        m_gPanel_Quickslot_Content = m_gPanel_Quickslot.transform.Find("Panel_Quickslot_Content").gameObject;

        m_gList_Quickslot = new List<GameObject>();
        m_qsList_Quickslot = new List<Quickslot>();
        for (int i = 0; i < m_gPanel_Quickslot_Content.transform.childCount; i++)
        {
            m_gList_Quickslot.Add(m_gPanel_Quickslot_Content.transform.GetChild(i).gameObject);
            m_qsList_Quickslot.Add(m_gList_Quickslot[i].GetComponent<Quickslot>());
        }

        m_KeyCodeList_Quickslot = new List<KeyCode>();
        m_KeyCodeList_Quickslot.Add(KeyCode.Delete);
        m_KeyCodeList_Quickslot.Add(KeyCode.End);
        m_KeyCodeList_Quickslot.Add(KeyCode.PageDown);
        m_KeyCodeList_Quickslot.Add(KeyCode.Insert);
        m_KeyCodeList_Quickslot.Add(KeyCode.Home);
        m_KeyCodeList_Quickslot.Add(KeyCode.PageUp);

    }
    // 초기 Button 이벤트 설정.
    void InitialSet_Button()
    {

    }

    // 퀵슬롯에 아이템 등록.
    public void Set_Quickslot(int quickslotarynumber, E_QUICKSLOT_CATEGORY eqc, int number)
    {
        for (int i = 0; i < m_gList_Quickslot.Count; i++)
        {
            if (eqc == E_QUICKSLOT_CATEGORY.EQUIP)
            {
                if (m_qsList_Quickslot[i].m_nQuickslot_Number != quickslotarynumber)
                {
                    if (m_qsList_Quickslot[i].m_nItem_Equip_AryNumber == number)
                    {
                        m_qsList_Quickslot[i].Set_Quickslot_Null();
                    }
                }
            }
            else if (eqc == E_QUICKSLOT_CATEGORY.USE)
            {
                if (m_qsList_Quickslot[i].m_nQuickslot_Number != quickslotarynumber)
                {
                    if (m_qsList_Quickslot[i].m_nItem_Use_Code == number)
                    {
                        m_qsList_Quickslot[i].Set_Quickslot_Null();
                    }
                }
            }
            else if (eqc == E_QUICKSLOT_CATEGORY.ETC)
            {
                if (m_qsList_Quickslot[i].m_nQuickslot_Number != quickslotarynumber)
                {
                    if (m_qsList_Quickslot[i].m_nItem_Etc_Code == number)
                    {
                        m_qsList_Quickslot[i].Set_Quickslot_Null();
                    }
                }
            }
        }
        m_qsList_Quickslot[quickslotarynumber].Set_Quickslot(eqc, number);
    }

    // 퀵슬롯에 등록된 아이템 개수 업데이트.
    public void Update_Quickslot()
    {
        for (int i = 0; i < m_gList_Quickslot.Count; i++)
        {
            switch (m_qsList_Quickslot[i].m_eQuickslot_Category)
            {
                case E_QUICKSLOT_CATEGORY.EQUIP:
                    {
                        if (Player_Itemslot.m_nary_Itemslot_Equip_Count[m_qsList_Quickslot[i].m_nItem_Equip_AryNumber] == 0)
                        {
                            m_qsList_Quickslot[i].Set_Quickslot_Null();
}
                        else
                        {

                        }
                    } break;
                case E_QUICKSLOT_CATEGORY.USE:
                    {
                        m_qsList_Quickslot[i].Set_Item_Count(Player_Total.Instance.Return_Quickslot_Item_Count(E_QUICKSLOT_CATEGORY.USE, m_qsList_Quickslot[i].m_nItem_Use_Code));
                    } break;
                case E_QUICKSLOT_CATEGORY.ETC:
                    {
                        m_qsList_Quickslot[i].Set_Item_Count(Player_Total.Instance.Return_Quickslot_Item_Count(E_QUICKSLOT_CATEGORY.ETC, m_qsList_Quickslot[i].m_nItem_Etc_Code));
                    } break;
            }
        }
    }
    // GUI_Quickslot 업데이트. - 아무 장비도 장착하지 않을때 퀵슬롯에 등록된 장비를 장착할 시 퀵슬롯 삭제. 
    public void Update_Quickslot_Equip(int arynumber)
    {
        for (int i = 0; i < m_gList_Quickslot.Count; i++)
        {
            if (m_qsList_Quickslot[i].m_eQuickslot_Category == E_QUICKSLOT_CATEGORY.EQUIP)
            {
                if (m_qsList_Quickslot[i].m_nItem_Equip_AryNumber == arynumber)
                {
                    m_qsList_Quickslot[i].Set_Quickslot_Null();
                }
            }
        }
    }

    // 퀵슬롯 쿨타임.
    public void Update_Quickslot_CoolTime(int arynumber)
    {
        m_qsList_Quickslot[arynumber].Update_Quickslot_CoolTime();
    }
}
