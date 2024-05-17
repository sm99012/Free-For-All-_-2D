using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush2_Total : Bush1_Total
{
    Vector2 m_vOffSet = new Vector2(0, 0.1f);
    void Update()
    {
        if (m_bPlay == true)
        {
            if (m_bRelation == true && m_bWait == false)
            {
                BodyDamage(1.0f, 0.1f, m_vOffSet, 0.3f);
            }
        }

        //AnimationTest();
    }
}
