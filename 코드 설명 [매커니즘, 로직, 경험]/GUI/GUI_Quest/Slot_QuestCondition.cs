using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot_QuestCondition : MonoBehaviour
{
    public Image m_IMG_QuestCondition_Sprite;
    public TextMeshProUGUI m_TMP_QuestCondition_Info;
    public GameObject m_gPanel_QuestCondition_Dissatisfied;

    public void Set_QuestCondition_Status_Title()
    {
        m_IMG_QuestCondition_Sprite.sprite = Resources.Load<Sprite>("Prefab/GUI_Sprite/FFA_V1_SS");
        m_IMG_QuestCondition_Sprite.color = new Color(1, 1, 1, 1);
        m_TMP_QuestCondition_Info.text = "[필수 능  력  치]";
        m_gPanel_QuestCondition_Dissatisfied.SetActive(false);
    }

    public void Set_QuestCondition_Soc_Title()
    {
        m_IMG_QuestCondition_Sprite.sprite = Resources.Load<Sprite>("Prefab/GUI_Sprite/FFA_V1_Honor");
        m_IMG_QuestCondition_Sprite.color = new Color(1, 1, 1, 1);
        m_TMP_QuestCondition_Info.text = "[필수 평        판]";
        m_gPanel_QuestCondition_Dissatisfied.SetActive(false);
    }

    public void Set_QuestCondition_Status_int(string sname, int minvalue, int maxvalue, bool condition)
    {
        m_IMG_QuestCondition_Sprite.sprite = Resources.Load<Sprite>("Prefab/GUI_Sprite/FFA_V1_SS");
        m_IMG_QuestCondition_Sprite.color = new Color(1, 1, 1, 1);

        if (minvalue != -10000)
            m_TMP_QuestCondition_Info.text = sname + ": " + minvalue.ToString();
        else
            m_TMP_QuestCondition_Info.text = sname + ": ";

        if (maxvalue != 10000)
            m_TMP_QuestCondition_Info.text += " ~ +" + maxvalue.ToString();
        else
            m_TMP_QuestCondition_Info.text += " ~ ";

        if (condition == true)
            m_gPanel_QuestCondition_Dissatisfied.SetActive(false);
        else
            m_gPanel_QuestCondition_Dissatisfied.SetActive(true);
    }
    public void Set_QuestCondition_Status_float(string sname, float minvalue, float maxvalue, bool condition)
    {
        m_IMG_QuestCondition_Sprite.sprite = Resources.Load<Sprite>("Prefab/GUI_Sprite/FFA_V1_SS");
        m_IMG_QuestCondition_Sprite.color = new Color(1, 1, 1, 1);

        if (minvalue != -10000)
            m_TMP_QuestCondition_Info.text = sname + ": " + minvalue.ToString();
        else
            m_TMP_QuestCondition_Info.text = sname + ": ";

        if (maxvalue != 10000)
            m_TMP_QuestCondition_Info.text += " ~ +" + maxvalue.ToString();
        else
            m_TMP_QuestCondition_Info.text += " ~ ";

        if (condition == true)
            m_gPanel_QuestCondition_Dissatisfied.SetActive(false);
        else
            m_gPanel_QuestCondition_Dissatisfied.SetActive(true);
    }

    public void Set_QuestCondition_Soc_int(string sname, int minvalue, int maxvalue, bool condition)
    {
        m_IMG_QuestCondition_Sprite.sprite = Resources.Load<Sprite>("Prefab/GUI_Sprite/FFA_V1_Honor");
        m_IMG_QuestCondition_Sprite.color = new Color(1, 1, 1, 1);

        if (minvalue != -10000)
            m_TMP_QuestCondition_Info.text = sname + ": " + minvalue.ToString();
        else
            m_TMP_QuestCondition_Info.text = sname + ": ";

        if (maxvalue != 10000)
            m_TMP_QuestCondition_Info.text += " ~ +" + maxvalue.ToString();
        else
            m_TMP_QuestCondition_Info.text += " ~ ";

        if (condition == true)
            m_gPanel_QuestCondition_Dissatisfied.SetActive(false);
        else
            m_gPanel_QuestCondition_Dissatisfied.SetActive(true);
    }

    public void Set_QuestCondition_PreQuest(string questname, bool condition)
    {
        m_IMG_QuestCondition_Sprite.sprite = Resources.Load<Sprite>("Prefab/GUI_Sprite/FFA_V1_Quest");
        m_IMG_QuestCondition_Sprite.color = new Color(1, 1, 1, 1);
        m_TMP_QuestCondition_Info.text = questname;

        if (condition == true)
            m_gPanel_QuestCondition_Dissatisfied.SetActive(false);
        else
            m_gPanel_QuestCondition_Dissatisfied.SetActive(true);
    }
}
