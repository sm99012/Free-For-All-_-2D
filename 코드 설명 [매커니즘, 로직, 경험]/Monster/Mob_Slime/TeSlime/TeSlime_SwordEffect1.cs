using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeSlime_SwordEffect1 : MonoBehaviour
{
    public float m_fDamage;

    float m_fDurationTime = 10;
    float m_fSpeed;

    Vector3 m_vDir;

    private void Update()
    {
        if (m_fDurationTime >= 0)
        {
            this.transform.position += m_vDir * m_fSpeed * Time.smoothDeltaTime * 0.005f;
            m_fDurationTime -= 0.05f;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void InitialSet_Damage(float damage)
    {
        m_fDamage = damage;
    }

    public void Set_Speed(float speed)
    {
        m_fSpeed = speed;
    }

    public void Set_Dir(Vector3 dir)
    {
        m_vDir = Vector3.Normalize(dir);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Vector3 m_vKnockBackDir = Vector3.Normalize(collision.gameObject.transform.position - this.transform.position);
            collision.GetComponent<Player_Total>().Attacked((int)(m_fDamage * 1.5f), m_vKnockBackDir, 0.3f, "테슬라임");
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Hurdle"))
        {
            Destroy(this.gameObject);
        }
        Debug.Log(collision.gameObject.name);
    }

}
