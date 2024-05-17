using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Popup_GUI : MonoBehaviour, IPointerClickHandler
{
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        this.gameObject.transform.SetAsLastSibling();

        // 상점UI 우선순위 관련.
        switch (this.gameObject.name)
        {
            case "Panel_Interaction":
                {
                    // GUI_Interaction
                    GUIManager_Total.Instance.Fix_GUI_Priority(3);
                }
                break;
            case "Panel_Quest":
                {
                    // GUI_Quest
                    GUIManager_Total.Instance.Fix_GUI_Priority(4);
                }
                break;
            case "Panel_Itemslot":
                {
                    // GUI_Itemslot
                    GUIManager_Total.Instance.Fix_GUI_Priority(8);
                }
                break;
            //case 9:
            //    {
            //        // GUI_Itemslot_Equip_Information
            //        GUIManager_Total.Instance.m_nList_UI_Priority.Add(9);
            //    }
            //    break;
            //case 10:
            //    {
            //        // GUI_Itemslot_Use_Information
            //        GUIManager_Total.Instance.m_nList_UI_Priority.Add(10);
            //    }
            //    break;
            //case 11:
            //    {
            //        // GUI_Itemslot_Etc_Information
            //        GUIManager_Total.Instance.m_nList_UI_Priority.Add(11);
            //    }
            //    break;
            //case "Panel_Equipslot_Equip_Information":
            //    {
            //        // GUI_Equipslot_Equip_Information
            //        GUIManager_Total.Instance.m_nList_UI_Priority.Add(13);
            //    }
            //    break;
            case "Panel_ES":
                {
                    // GUI_ES
                    GUIManager_Total.Instance.Fix_GUI_Priority(16);
                }
                break;
            case "Panel_Reinforcement":
                {
                    // GUI_Reinforcement
                    GUIManager_Total.Instance.Fix_GUI_Priority(20);
                }
                break;
            case "Panel_Store":
                {
                    // GUI_Store
                    if (GUIManager_Total.Instance.m_GUI_Store_Simple_Item_Information.m_gPanel_Store_Simple_Item_Information.activeSelf == true)
                    {
                        GUIManager_Total.Instance.m_GUI_Store_Simple_Item_Information.m_gPanel_Store_Simple_Item_Information.transform.SetAsLastSibling();
                        GUIManager_Total.Instance.Fix_GUI_Priority(23);
                    }
                }
                break;
            case "Panel_Info":
                {
                    // GUI_Info
                    GUIManager_Total.Instance.Fix_GUI_Priority(27);
                }
                break;
            case "Panel_MonsterDictionary":
                {
                    // GUI_MonsterDictionary
                    GUIManager_Total.Instance.Fix_GUI_Priority(29);
                }
                break;
            case "Panel_Option":
                {
                    // GUI_Option
                    GUIManager_Total.Instance.Fix_GUI_Priority(37);
                }
                break;
            case "Panel_BossBattleInformation":
                {
                    // GUI_BossBattleInformation
                    GUIManager_Total.Instance.Fix_GUI_Priority(39);
                }
                break;
        }
    }
}
