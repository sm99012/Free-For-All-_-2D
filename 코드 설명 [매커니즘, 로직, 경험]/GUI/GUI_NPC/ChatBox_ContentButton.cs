using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatBox_ContentButton : MonoBehaviour
{
    [SerializeField] GameObject m_gPanel_Chat_Selected;

    public void Display_Chat_Selected()
    {
        m_gPanel_Chat_Selected.SetActive(true);
    }
    public void UnDisplay_Chat_Selected()
    {
        m_gPanel_Chat_Selected.SetActive(false);
    }
}
