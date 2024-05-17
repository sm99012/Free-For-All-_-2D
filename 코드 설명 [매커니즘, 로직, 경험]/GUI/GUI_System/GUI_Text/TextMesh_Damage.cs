using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMesh_Damage : MonoBehaviour
{
    TextMesh m_t_DamageText;

    Vector3 m_vPos;
    Vector3 m_vMovePos;

    int m_nDamage;

    float m_fDestroyTime;
    float m_fMoveTime;

    bool m_bStart;

    //private void Start()
    //{
    //    m_t_DamageText = this.GetComponent<TextMesh>();

    //    m_vMovePos = new Vector3(0, 1, 0) * Time.deltaTime;

    //    m_fDestroyTime = 2;
    //    m_fMoveTime = 2;

    //    m_bStart = false;
    //}

    private void Update()
    {
        if (m_bStart == true)
        {
            m_fDestroyTime -= Time.deltaTime;
            if (m_fDestroyTime <= 0)
            {
                Destroy(this.gameObject);
            }

            m_fMoveTime -= Time.deltaTime;
            if (m_fMoveTime > 0)
            {
                this.transform.position += m_vMovePos;
            }
        }
    }

    public void InitialSet(Vector3 pos, int damage)
    {
        m_t_DamageText = this.GetComponent<TextMesh>();

        m_vMovePos = new Vector3(0, 0.25f, 0) * Time.deltaTime;

        m_fDestroyTime = 1;
        m_fMoveTime = 1;

        m_bStart = true;
        m_vPos = pos;
        this.transform.position = m_vPos;
        m_nDamage = damage;
        m_t_DamageText.text = m_nDamage.ToString();
    }
}
