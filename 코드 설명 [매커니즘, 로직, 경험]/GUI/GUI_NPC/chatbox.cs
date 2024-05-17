using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class chatbox : MonoBehaviour, IPointerClickHandler
{
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (GUIManager_Total.Instance.m_GUI_Interaction.m_gPanel_ChatBox.activeSelf == true)
        {
            GUIManager_Total.Instance.m_GUI_Interaction.Skip_CQ_Script();
        }
    }
}
