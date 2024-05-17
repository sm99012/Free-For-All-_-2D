using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRoute : MonoBehaviour
{
    public LineRenderer m_LineRenderer;

    public Vector2 m_vStartPos;
    public Vector2 m_vEndPos;

    private void Awake()
    {
        m_LineRenderer = this.gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        m_LineRenderer.startWidth = 0.2f;
        m_LineRenderer.startColor = new Color(1, 0, 0, 1);
        m_LineRenderer.endWidth = 0.2f;
        m_LineRenderer.endColor = new Color(1, 0, 0, 1);

        m_LineRenderer.SetPosition(0, m_vStartPos);
        m_LineRenderer.SetPosition(1, m_vEndPos);
    }
}
