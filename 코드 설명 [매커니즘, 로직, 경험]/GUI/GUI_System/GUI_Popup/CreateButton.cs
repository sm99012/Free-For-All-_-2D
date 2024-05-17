using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateButton : MonoBehaviour
{
    public TextMeshProUGUI m_tm_ButtonName;

    public void SetButtonName(string str)
    {
        m_tm_ButtonName.text = str;
    }
}
