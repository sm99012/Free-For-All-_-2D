using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    protected SpriteRenderer m_sp_Tree;

    protected Color m_Color_Tree;

    protected Collider2D[] m_co2;

    public Vector2 m_vPos;
    public Vector2 m_vSize;
    public Vector2 m_vOffset;

    protected float m_fDetectTime = 0.1f;
    protected float m_fDetectTime_Current = 0;

    public bool m_bPlay;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(m_vPos + m_vOffset, m_vSize);
    }
}
