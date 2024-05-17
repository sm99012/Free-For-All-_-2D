using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Slot_QuestReward_Item : MonoBehaviour
{
    public Image m_IMG_QuestReward_Item_Sprite;
    public TextMeshProUGUI m_TMP_QuestReward_Item_Name;

    public void Set_Null()
    {
        m_IMG_QuestReward_Item_Sprite = null;
        m_IMG_QuestReward_Item_Sprite.color = new Color(1, 1, 1, 0);
        m_TMP_QuestReward_Item_Name.text = "";
    }

    public void Set_Item(Item item)
    {
        m_IMG_QuestReward_Item_Sprite.sprite = item.m_sp_Sprite;
        m_IMG_QuestReward_Item_Sprite.color = new Color(1, 1, 1, 1);
        m_TMP_QuestReward_Item_Name.text = item.m_sItemName;
    }

    public void Set_Gold(int gold)
    {
        if (gold < 1000)
            m_IMG_QuestReward_Item_Sprite.sprite = Resources.Load<Sprite>("Prefab/Item/Item_Gold/Gold Nugget");
        else
            m_IMG_QuestReward_Item_Sprite.sprite = Resources.Load<Sprite>("Prefab/Item/Item_Gold/Golden Ingot");

        m_IMG_QuestReward_Item_Sprite.color = new Color(1, 1, 1, 1);
        m_TMP_QuestReward_Item_Name.text = gold.ToString() + " 골드";
    }

    public void Set_Status_int(string sname, int value)
    {
        m_IMG_QuestReward_Item_Sprite.sprite = Resources.Load<Sprite>("Prefab/GUI_Sprite/FFA_V1_SS");
        m_IMG_QuestReward_Item_Sprite.color = new Color(1, 1, 1, 1);
        if (value > 0)
            m_TMP_QuestReward_Item_Name.text = sname + ": +" + value.ToString();
        else
            m_TMP_QuestReward_Item_Name.text = sname + ": -" + value.ToString();
    }
    public void Set_Status_float(string sname, float value)
    {
        m_IMG_QuestReward_Item_Sprite.sprite = Resources.Load<Sprite>("Prefab/GUI_Sprite/FFA_V1_SS");
        m_IMG_QuestReward_Item_Sprite.color = new Color(1, 1, 1, 1);
        if (value > 0)
            m_TMP_QuestReward_Item_Name.text = sname + ": +" + value.ToString();
        else
            m_TMP_QuestReward_Item_Name.text = sname + ": -" + value.ToString();
    }

    public void Set_Soc_int(string sname, int value)
    {
        m_IMG_QuestReward_Item_Sprite.sprite = Resources.Load<Sprite>("Prefab/GUI_Sprite/FFA_V1_Honor");
        m_IMG_QuestReward_Item_Sprite.color = new Color(1, 1, 1, 1);
        if (value > 0)
            m_TMP_QuestReward_Item_Name.text = sname + ": +" + value.ToString();
        else
            m_TMP_QuestReward_Item_Name.text = sname + ": -" + value.ToString();
    }
}
