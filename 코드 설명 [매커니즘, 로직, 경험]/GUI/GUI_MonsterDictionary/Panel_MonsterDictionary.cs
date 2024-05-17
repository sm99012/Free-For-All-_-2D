using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum E_MONSTERDICTIONARY_PANEL_KIND { APPEARAREA, REWARD }

public class Panel_MonsterDictionary : MonoBehaviour
{
    public Image m_IMG_MonsterDictionary_Sprite;
    public TextMeshProUGUI m_TMP_MonsterDictionary_Name;

    public E_MONSTERDICTIONARY_PANEL_KIND m_eMonsterDictionary_Panel_Kind;

    // Panel_Reward
    public void Set_Gold(int mingold, int maxgold, int count, int droprate)
    {
        float averagegold = (((float)mingold + (float)maxgold) * count) / 2;
        if (averagegold < 1000)
            m_IMG_MonsterDictionary_Sprite.sprite = Resources.Load<Sprite>("Prefab/Item/Item_Gold/Gold Nugget");
        else
            m_IMG_MonsterDictionary_Sprite.sprite = Resources.Load<Sprite>("Prefab/Item/Item_Gold/Golden Ingot");

        m_TMP_MonsterDictionary_Name.text = mingold.ToString() + " ~ " + maxgold.ToString() + " 골드 (" + ((float)droprate / 100).ToString() + "%) X (1 ~ " + count.ToString() + ")";
    }
    public void Set_Item(Item item, int count, int droprate)
    {
        m_IMG_MonsterDictionary_Sprite.sprite = item.m_sp_Sprite;
        m_TMP_MonsterDictionary_Name.text = item.m_sItemName + " (" + ((float)droprate / 100).ToString() + "%) X (1 ~ " + count.ToString() + ")";
    }

    // Panel_AppearArea
    public void Set_AppearArea(string areaname)
    {
        var itemdata = areaname.Split(']');
        switch (itemdata[0])
        {
            case "[깊디깊은숲":
                {
                    m_IMG_MonsterDictionary_Sprite.sprite = Resources.Load<Sprite>("Prefab/Background_Sprite/숲1");
                } break;
            case "[드넓은 초원":
                {
                    m_IMG_MonsterDictionary_Sprite.sprite = Resources.Load<Sprite>("Prefab/Background_Sprite/들판2");
                } break;
            default:
                {
                    m_IMG_MonsterDictionary_Sprite.sprite = Resources.Load<Sprite>("Prefab/Background_Sprite/바다1");
                } break;
        }

        m_TMP_MonsterDictionary_Name.text = areaname;
    }
}
