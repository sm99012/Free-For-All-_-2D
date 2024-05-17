using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush1_2_Status : Bush1_Status
{
    override public void InitialSet_STATUS()
    {
        m_sStatus = new STATUS(1, 0, 0, 7, 7);
        m_sStatus_Origin.SetSTATUS(m_sStatus);
        m_sStatus_Goaway = new STATUS();
        m_sStatus_Death = new STATUS(0, 0, 0);
    }
}
