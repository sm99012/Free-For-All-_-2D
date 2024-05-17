using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeSlime_Drop : Monster_Drop
{
    override public void Start()
    {
        InitialSet();
    }

    override public void InitialSet()
    {
        EssentialSet(); ;

        m_vItempos = new Vector3(0, 0, 0);
        m_vItemoffset = new Vector3(0.001f, 0, 0);
    }
}
