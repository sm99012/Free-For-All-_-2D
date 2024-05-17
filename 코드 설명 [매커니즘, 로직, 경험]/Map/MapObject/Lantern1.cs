using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Lantern1 : MonoBehaviour
{
    [SerializeField]
    Light2D m_Light_LaternLight;

    public bool m_bUse;
    bool m_bUp;

    public float m_fStartValue;
    float m_fRevisionValue;

    void Start()
    {
        m_Light_LaternLight = transform.Find("Parametric Light 2D_Point").GetComponent<Light2D>();

        m_Light_LaternLight.intensity = m_fStartValue;
        m_fRevisionValue = 0.001f;

        m_bUse = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLight();
    }

    // 랜턴 빛 밝기 조절.
    void UpdateLight()
    {
        if (m_bUse == true)
        {
            if (m_Light_LaternLight.intensity <= 0.05f)
            {
                m_bUp = true;
            }
            if (m_Light_LaternLight.intensity >= 0.3f)
            {
                m_bUp = false;
            }

            if (m_bUp == true)
            {
                m_Light_LaternLight.intensity += m_fRevisionValue;
            }
            else
            {
                m_Light_LaternLight.intensity -= m_fRevisionValue;
            }
        }
    }
}
