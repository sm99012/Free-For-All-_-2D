using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum ItemType { EQUIP, USE, ETC, GOLD }
public enum E_ITEM_GRADE { NORMAL, COMMON, RARE, RELIC, LEGEND, MYTH }
public enum E_ITEM_ADDITIONALOPTION_STATUS { S1, S2, S3, S4, S5, S6, S7, S8, S9, S10 }
public enum E_ITEM_ADDITIONALOPTION_SOC { S1, S2, S3, S4, S5, S6, S7, S8, S9, S10 }

public class Item : MonoBehaviour
{
    public string m_sItemName;
    public string m_sItemDescription;
    public int m_nItemCode;
    public int m_nItemNumber;
    public Sprite m_sp_Sprite;
    public SpriteRenderer m_spr_SpriteRenderer;

    public ItemType m_eItemtype;
    public E_ITEM_GRADE m_eItemGrade;

    // 제한(착용 제한, 사용 제한)
    public STATUS m_sStatus_Limit_Max;
    public STATUS m_sStatus_Limit_Min;
    public SOC m_sSoc_Limit_Max;
    public SOC m_sSoc_Limit_Min;
    // 효과(착용 효과, 사용 효과)
    public STATUS m_sStatus_Effect;
    public SOC m_sSoc_Effect;
    // 판매 금액.
    public int m_nPrice;

    // 아이템 페이드인.
    public bool m_bPossible_Get;
    protected float m_FadeinAlpa;

    //~Item()
    //{
    //    Debug.Log("아이템 원본 삭제: " + m_sItemName);
    //}

    public string GetItemDescription()
    {
        return m_sItemDescription;
    }    

    virtual public void ItemInformation()
    {
        Debug.Log(m_sItemName + ", " + m_nItemNumber);
    }

    // 필드에 아이템 드랍 후 일정 시간이 지나도록 Player 가 드랍하지 않을시 해당 아이템 삭제. 
    protected bool Delete_Item()
    {
        return true;
    }

    IEnumerator ProcessDeleteItem()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }

    protected void Fadein()
    {
        StartCoroutine(ProcessFadein());
    }

    protected IEnumerator ProcessFadein()
    {
        m_bPossible_Get = false;
        while (m_FadeinAlpa != 1)
        {
            m_spr_SpriteRenderer.color = new Color(m_spr_SpriteRenderer.color.r, m_spr_SpriteRenderer.color.g, m_spr_SpriteRenderer.color.b, m_FadeinAlpa);
            m_FadeinAlpa += 0.01f;

            if (m_FadeinAlpa > 1)
            {
                m_FadeinAlpa = 1;
                break;
            }
            yield return new WaitForSeconds(0.01f);
        }
        m_bPossible_Get = true;
    }
}
