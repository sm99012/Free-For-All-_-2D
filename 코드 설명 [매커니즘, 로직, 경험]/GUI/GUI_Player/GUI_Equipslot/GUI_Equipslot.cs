using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//
// ※ 장비창 GUI
//    해당 GUI를 활성화 하여 플레이어가 현재 착용중인 장비아이템을 확인할 수 있다. 해당 GUI는 상태창(능력치창 + 장비창 + 아이템 세트효과창)GUI로 통합 되었다.
//

public class GUI_Equipslot : MonoBehaviour
{
    [SerializeField] GameObject m_gPanel_ES;
    // Equipslot UI.
    [SerializeField] GameObject m_gPanel_Equipslot;

    [SerializeField] GameObject m_gPanel_Equipslot_Exit;
    [SerializeField] GameObject m_gPanel_Equipslot_Content;
    [Space(20)]
    [SerializeField] Button m_BTN_Equipslot_Exit;

    [Space(20)]
    [SerializeField] GameObject m_gPanel_Equipslot_Content_Hat;
    [SerializeField] GameObject m_gPanel_Equipslot_Content_Top;
    [SerializeField] GameObject m_gPanel_Equipslot_Content_Bottoms;
    [SerializeField] GameObject m_gPanel_Equipslot_Content_Shose;
    [SerializeField] GameObject m_gPanel_Equipslot_Content_Gloves;
    [SerializeField] GameObject m_gPanel_Equipslot_Content_MainWeapon;
    [SerializeField] GameObject m_gPanel_Equipslot_Content_SubWeapon;
    [Space(20)]
    // 장비아이템 세부설명 UI.
    [SerializeField] GameObject m_gPanel_Equipslot_Equip_Information;


    public GameObject[] m_gary_Equipslot;

    // 각각의 장비슬롯의 해당하는 장비 분류를 배열로 저장.
    // m_gary_Equipslot: 착용중인 분류의 장비 아이템.
    // m_bary_Equipslot: 착용중이면 true, 착용중이 아니면 false.
    // 0: Hat 
    // 1: Top
    // 2: Bottoms
    // 3: Shose
    // 4: Gloves
    // 5: MainWeapon
    // 6: SubWeapon

    public void InitialSet()
    {
        InitialSet_Object();
        InitialSet_Button();
        InitialSet_Itemslot();
        m_gPanel_ES.SetActive(false);
        m_gPanel_Equipslot.SetActive(false);
    }

    // 수정필요
    private void Update()
    {
        if (Total_Manager.Instance.m_bStart == true)
        {
            if (m_gPanel_Equipslot.activeSelf == true)
            {

            }
            else
            {
                m_gPanel_Equipslot_Equip_Information.SetActive(false);

            }
        }
    }

    // 초기 Object 불러오기.
    void InitialSet_Object()
    {
        m_gPanel_ES = GameObject.Find("Canvas_GUI").gameObject.transform.Find("Panel_ES").gameObject;

        m_gPanel_Equipslot = GameObject.Find("Canvas_GUI").gameObject.transform.Find("Panel_ES").gameObject.transform.Find("Panel_Equipslot").gameObject;
        m_gPanel_Equipslot_Equip_Information = GameObject.Find("Canvas_GUI").gameObject.transform.Find("Panel_Equipslot_Equip_Information").gameObject;

        m_gPanel_Equipslot_Exit = m_gPanel_Equipslot.transform.Find("Panel_Equipslot_Exit").gameObject;
        m_gPanel_Equipslot_Content = m_gPanel_Equipslot.transform.Find("Panel_Equipslot_Content").gameObject;

        m_BTN_Equipslot_Exit = m_gPanel_Equipslot_Exit.transform.Find("BTN_Equipslot_Exit").gameObject.GetComponent<Button>();

        m_gPanel_Equipslot_Content_Hat = m_gPanel_Equipslot_Content.transform.Find("Panel_Equipslot_Content_Hat").gameObject;
        m_gPanel_Equipslot_Content_Top = m_gPanel_Equipslot_Content.transform.Find("Panel_Equipslot_Content_Top").gameObject;
        m_gPanel_Equipslot_Content_Bottoms = m_gPanel_Equipslot_Content.transform.Find("Panel_Equipslot_Content_Bottoms").gameObject;
        m_gPanel_Equipslot_Content_Shose = m_gPanel_Equipslot_Content.transform.Find("Panel_Equipslot_Content_Shose").gameObject;
        m_gPanel_Equipslot_Content_Gloves = m_gPanel_Equipslot_Content.transform.Find("Panel_Equipslot_Content_Gloves").gameObject;
        m_gPanel_Equipslot_Content_MainWeapon = m_gPanel_Equipslot_Content.transform.Find("Panel_Equipslot_Content_MainWeapon").gameObject;
        m_gPanel_Equipslot_Content_SubWeapon = m_gPanel_Equipslot_Content.transform.Find("Panel_Equipslot_Content_SubWeapon").gameObject;
    }
    // 초기 Button 이벤트 설정.
    void InitialSet_Button()
    {
        m_BTN_Equipslot_Exit.GetComponent<Button>().onClick.AddListener(delegate { Btn_Press_Exit(); });
    }

    // Equipslot 초기 설정
    public void InitialSet_Itemslot()
    {
        m_gary_Equipslot = new GameObject[7];
        for (int i = 0; i < 7; i++)
        {
            m_gary_Equipslot[i] = m_gPanel_Equipslot_Content.transform.GetChild(i).gameObject;
            m_gary_Equipslot[i].GetComponent<Equipslot>().m_nAryNumber = i;
        }
    }

    public void UpdateEquipslot()
    {
        if (Player_Equipment.m_bEquipment_Hat == false)
        {
            m_gary_Equipslot[0].GetComponent<Equipslot>().SetNull();
        }
        else
        {
            m_gary_Equipslot[0].GetComponent<Equipslot>().SetItem_Equip(Player_Equipment.m_gEquipment_Hat);
            if (Player_Total.Instance.CheckCondition_Item_Equip_Hat() == true) { }
            else
            {
                m_gary_Equipslot[0].GetComponent<Equipslot>().SetItem_Equip_NotApply();
                GUIManager_Total.Instance.Display_GUI_Equipslot_Remove_Information();
            }
        }
        if (Player_Equipment.m_bEquipment_Top == false)
        {
            m_gary_Equipslot[1].GetComponent<Equipslot>().SetNull();
        }
        else
        {
            m_gary_Equipslot[1].GetComponent<Equipslot>().SetItem_Equip(Player_Equipment.m_gEquipment_Top);
            if (Player_Total.Instance.CheckCondition_Item_Equip_Top() == true) { }
            else
            {
                m_gary_Equipslot[1].GetComponent<Equipslot>().SetItem_Equip_NotApply();
                GUIManager_Total.Instance.Display_GUI_Equipslot_Remove_Information();
            }
        }
        if (Player_Equipment.m_bEquipment_Bottoms == false)
        {
            m_gary_Equipslot[2].GetComponent<Equipslot>().SetNull();
        }
        else
        {
            m_gary_Equipslot[2].GetComponent<Equipslot>().SetItem_Equip(Player_Equipment.m_gEquipment_Bottoms);
            if (Player_Total.Instance.CheckCondition_Item_Equip_Bottoms() == true) { }
            else
            {
                m_gary_Equipslot[2].GetComponent<Equipslot>().SetItem_Equip_NotApply();
                GUIManager_Total.Instance.Display_GUI_Equipslot_Remove_Information();
            }
        }
        if (Player_Equipment.m_bEquipment_Shose == false)
        {
            m_gary_Equipslot[3].GetComponent<Equipslot>().SetNull();
        }
        else
        {
            m_gary_Equipslot[3].GetComponent<Equipslot>().SetItem_Equip(Player_Equipment.m_gEquipment_Shose);
            if (Player_Total.Instance.CheckCondition_Item_Equip_Shose() == true) { }
            else
            {
                m_gary_Equipslot[3].GetComponent<Equipslot>().SetItem_Equip_NotApply();
                GUIManager_Total.Instance.Display_GUI_Equipslot_Remove_Information();
            }
        }
        if (Player_Equipment.m_bEquipment_Gloves == false)
        {
            m_gary_Equipslot[4].GetComponent<Equipslot>().SetNull();
        }
        else
        {
            m_gary_Equipslot[4].GetComponent<Equipslot>().SetItem_Equip(Player_Equipment.m_gEquipment_Gloves);
            if (Player_Total.Instance.CheckCondition_Item_Equip_Gloves() == true) { }
            else
            {
                m_gary_Equipslot[4].GetComponent<Equipslot>().SetItem_Equip_NotApply();
                GUIManager_Total.Instance.Display_GUI_Equipslot_Remove_Information();
            }
        }
        if (Player_Equipment.m_bEquipment_Mainweapon == false)
        {
            m_gary_Equipslot[5].GetComponent<Equipslot>().SetNull();
        }
        else
        {
            m_gary_Equipslot[5].GetComponent<Equipslot>().SetItem_Equip(Player_Equipment.m_gEquipment_Mainweapon);
            if (Player_Total.Instance.CheckCondition_Item_Equip_MainWeapon() == true) { }
            else
            {
                m_gary_Equipslot[5].GetComponent<Equipslot>().SetItem_Equip_NotApply();
                GUIManager_Total.Instance.Display_GUI_Equipslot_Remove_Information();
            }
        }
        if (Player_Equipment.m_bEquipment_Subweapon == false)
        {
            m_gary_Equipslot[6].GetComponent<Equipslot>().SetNull();
        }
        else
        {
            m_gary_Equipslot[6].GetComponent<Equipslot>().SetItem_Equip(Player_Equipment.m_gEquipment_Subweapon);
            if (Player_Total.Instance.CheckCondition_Item_Equip_SubWeapon() == true) { }
            else
            {
                m_gary_Equipslot[6].GetComponent<Equipslot>().SetItem_Equip_NotApply();
                GUIManager_Total.Instance.Display_GUI_Equipslot_Remove_Information();
            }
        }
    }

    public bool Display_GUI_Equipslot()
    {
        if (m_gPanel_Equipslot.activeSelf == true)
        {
            m_gPanel_ES.SetActive(false);
            m_gPanel_Equipslot.SetActive(false);

            return false;
        }
        else
        {
            m_BTN_Equipslot_Exit.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            m_gPanel_ES.SetActive(true);
            m_gPanel_Equipslot.SetActive(true);
            m_gPanel_Equipslot.transform.SetAsLastSibling();
            m_gPanel_ES.transform.SetAsLastSibling();

            return true;
        }
    }

    // 버튼 이벤트 처리
    public void Btn_Press_Exit()
    {
        m_BTN_Equipslot_Exit.GetComponent<Image>().color = new Color(.75f, .75f, .75f, 1);
        //m_gPanel_Equipslot.SetActive(false);
        GUIManager_Total.Instance.Display_GUI_ES();
        m_gPanel_ES.SetActive(false);
        m_gPanel_Equipslot_Equip_Information.SetActive(false);

        GUIManager_Total.Instance.Delete_GUI_Priority(16);
    }
}
