using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign_UI : MonoBehaviour
{
    [SerializeField] GameObject m_gSign_Context;

    Vector2 m_vDetectArea;

    int m_nLayer;

    Collider2D m_Collider2D_Detect;

    private void Awake()
    {
        m_gSign_Context = this.gameObject.transform.Find("Sign_Context").gameObject;

        m_vDetectArea = this.gameObject.GetComponent<BoxCollider2D>().size;

        m_nLayer = 1 << LayerMask.NameToLayer("Player");
    }

    private void Update()
    {
        m_Collider2D_Detect = Physics2D.OverlapBox(this.gameObject.transform.position, m_vDetectArea, 0, m_nLayer);
        if (m_Collider2D_Detect != null)
            m_gSign_Context.SetActive(true);
        else
            m_gSign_Context.SetActive(false);
    }
}
