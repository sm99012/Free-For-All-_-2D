using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree1 : Tree
{
    int m_nLayer = 1;

    private void Start()
    {
        m_sp_Tree = this.gameObject.GetComponent<SpriteRenderer>();

        m_Color_Tree = m_sp_Tree.color;

        m_vSize = new Vector2(0.66f, 0.55f);
        m_vOffset = new Vector2(0, .3f);

        m_nLayer = 1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Item");

        m_vPos = new Vector2(this.transform.position.x + m_vOffset.x, this.transform.position.y + m_vOffset.y);

        m_fDetectTime = 0.1f;
        m_fDetectTime_Current = 0;

        m_bPlay = false;
    }

    private void Update()
    {
        if (m_bPlay == true)
        {
            m_fDetectTime_Current += Time.deltaTime;
            if (m_fDetectTime_Current > m_fDetectTime)
            {
                m_co2 = Physics2D.OverlapBoxAll(m_vPos, m_vSize, 0, m_nLayer);

                if (m_co2.Length > 0)
                {
                    m_sp_Tree.color = new Color(m_Color_Tree.r, m_Color_Tree.g, m_Color_Tree.b, 0.3f);
                }
                else
                {
                    m_sp_Tree.color = new Color(m_Color_Tree.r, m_Color_Tree.g, m_Color_Tree.b, 1f);
                }
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireCube(m_vPos + m_vOffset, m_vSize);
    //}
}
