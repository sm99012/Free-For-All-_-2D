using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Panel_MonsterDictionary_SS : MonoBehaviour
{
    public Image m_IMG_MonsterDictionary_Sprite;
    public TextMeshProUGUI m_TMP_MonsterDictionary_Left;
    public TextMeshProUGUI m_TMP_MonsterDictionary_Right;

    // Panel_SS
    public void Set_STATUS(STATUS status, int number)
    {
        m_IMG_MonsterDictionary_Sprite.sprite = Resources.Load<Sprite>("Prefab/GUI_Sprite/FFA_V1_SS");

        switch (number)
        {
            case 1:
                {
                    m_TMP_MonsterDictionary_Left.text = "레        벨: " + status.GetSTATUS_LV(); m_TMP_MonsterDictionary_Right.text = "";
                } break;
            case 2:
                {
                    m_TMP_MonsterDictionary_Left.text = "체        력: " + status.GetSTATUS_HP_Max(); m_TMP_MonsterDictionary_Right.text = "마        나: " + status.GetSTATUS_MP_Max() + "\n";
                } break;
            case 3:
                {
                    m_TMP_MonsterDictionary_Left.text = "데  미  지: " + status.GetSTATUS_Damage_Total(); m_TMP_MonsterDictionary_Right.text = "방  어  력: " + status.GetSTATUS_Defence_Physical() + "\n";
                } break;
            case 4:
                {
                    m_TMP_MonsterDictionary_Left.text = "이동속도: " + status.GetSTATUS_Speed(); m_TMP_MonsterDictionary_Right.text = "공격속도: " + status.GetSTATUS_AttackSpeed() + "\n";
                } break;
        }
    }

    // Panel_Reward_SS
    public void Set_Reward_STATUS(STATUS status)
    {
        m_IMG_MonsterDictionary_Sprite.sprite = Resources.Load<Sprite>("Prefab/GUI_Sprite/FFA_V1_SS");

        m_TMP_MonsterDictionary_Left.text = "경  험  치: " + status.GetSTATUS_EXP_Current(); m_TMP_MonsterDictionary_Right.text = "";
    }

    public void Set_Reward_SOC(string leftstr, int leftn, string rightstr = "", int rightn = 0)
    {
        m_IMG_MonsterDictionary_Sprite.sprite = Resources.Load<Sprite>("Prefab/GUI_Sprite/FFA_V1_Honor");

        string srightn = "";
        if (rightn != 0)
            srightn = rightn.ToString();
        m_TMP_MonsterDictionary_Left.text = leftstr + leftn; m_TMP_MonsterDictionary_Right.text = rightstr + srightn;

    }
}
