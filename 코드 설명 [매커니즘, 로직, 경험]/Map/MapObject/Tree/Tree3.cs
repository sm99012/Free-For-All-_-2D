using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree3 : MonoBehaviour
{
    public GameObject m_gTree_Upper;
    public GameObject m_gTree_Lower;

    SpriteRenderer m_sp_Tree_Upper;
    SpriteRenderer m_sp_Tree_Lower;

    Color m_Color_Tree_Upper;
    Color m_Color_Tree_Lower;

    Collider2D[] m_co2;

    Vector2 m_vPos;
    Vector2 m_vSize;
    Vector2 m_vOffset;

    int m_nLayer = 1;

    private void Start()
    {
        m_sp_Tree_Upper = m_gTree_Upper.gameObject.GetComponent<SpriteRenderer>();
        m_sp_Tree_Lower = m_gTree_Lower.gameObject.GetComponent<SpriteRenderer>();

        m_Color_Tree_Upper = m_sp_Tree_Upper.color;
        m_Color_Tree_Lower = m_sp_Tree_Lower.color;

        m_vOffset = new Vector2(0, .27f);
        m_vSize = new Vector2(0.55f, 0.55f);

        m_nLayer = 1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Item");

        m_vPos = new Vector2(this.transform.position.x + m_vOffset.x, this.transform.position.y + m_vOffset.y);
    }

    private void Update()
    {
        m_co2 = Physics2D.OverlapBoxAll(m_vPos, m_vSize, 0, m_nLayer);

        if (m_co2.Length > 0)
        {
            //for (int i = 0; i < m_co2.Length; i++)
            //{
            //    Debug.Log(m_co2[i].gameObject.name);
            //}
            m_sp_Tree_Upper.color = new Color(m_Color_Tree_Upper.r, m_Color_Tree_Upper.g, m_Color_Tree_Upper.b, 0.5f);
            m_sp_Tree_Lower.color = new Color(m_Color_Tree_Lower.r, m_Color_Tree_Lower.g, m_Color_Tree_Lower.b, 0.5f);
        }
        else
        {
            m_sp_Tree_Upper.color = new Color(m_Color_Tree_Upper.r, m_Color_Tree_Upper.g, m_Color_Tree_Upper.b, 1);
            m_sp_Tree_Lower.color = new Color(m_Color_Tree_Lower.r, m_Color_Tree_Lower.g, m_Color_Tree_Lower.b, 1);
        }
    }
}
