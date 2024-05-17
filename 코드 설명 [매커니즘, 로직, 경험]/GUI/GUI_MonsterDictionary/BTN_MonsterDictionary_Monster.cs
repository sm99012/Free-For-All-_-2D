using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BTN_MonsterDictionary_Monster : MonoBehaviour
{
    public Button m_BTN_MonsterDictionary_Monster;
    public Image m_IMG_MonsterDictionary_Monster;
    public Image m_IMG_MonsterDictionary_Monster_Sparkle;
    public int m_nMonsterDictionary_MonsterCode;

    public void Update_Btn(Sprite sprite, int monstercode)
    {
        m_IMG_MonsterDictionary_Monster.sprite = sprite;
        m_nMonsterDictionary_MonsterCode = monstercode;
        int code = monstercode;

        m_BTN_MonsterDictionary_Monster.onClick.RemoveAllListeners();
        m_BTN_MonsterDictionary_Monster.onClick.AddListener(delegate { GUIManager_Total.Instance.Update_MonsterDictionary_Info(code); });

        if (MonsterManager.m_Dictionary_Monster[monstercode].m_nMonster_Dictionary_Solve_Current == 0)
        {
            m_IMG_MonsterDictionary_Monster.color = new Color(0, 0, 0, 1);
        }
        else
        {
            m_IMG_MonsterDictionary_Monster.color = new Color(1, 1, 1, 1);
        }
    }
}
