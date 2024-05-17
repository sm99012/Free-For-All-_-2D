using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreatePanel_Loop : MonoBehaviour
{
    public TextMeshProUGUI m_tm_Day;
    public TextMeshProUGUI m_tm_Soc;

    public GameObject m_g_Btn_Loop;

    // 평판: 100 | 사람: 100 | 슬라임: 100 | 스켈레톤: 100 | 엔트: 100 | 마족: 100 | 용: 100 | 그림자: 100
    public void SetInfo(int n)
    {
        int dn = n;
        m_tm_Day.text = "Day " + n.ToString();
        m_tm_Soc.text = "평판: " + Loop.Instance.m_ll_LoopDataList[n].m_sLoopSoc.GetSOC_Honor() + " | " +
                        "사람: " + Loop.Instance.m_ll_LoopDataList[n].m_sLoopSoc.GetSOC_Human() + " | " +
                        "슬라임: " + Loop.Instance.m_ll_LoopDataList[n].m_sLoopSoc.GetSOC_Slime() + " | " +
                        "스켈레톤: " + Loop.Instance.m_ll_LoopDataList[n].m_sLoopSoc.GetSOC_Skeleton() + " | " +
                        "엔트: " + Loop.Instance.m_ll_LoopDataList[n].m_sLoopSoc.GetSOC_Ents() + " | " +
                        "마족: " + Loop.Instance.m_ll_LoopDataList[n].m_sLoopSoc.GetSOC_Devil() + " | " +
                        "용: " + Loop.Instance.m_ll_LoopDataList[n].m_sLoopSoc.GetSOC_Dragon() + " | " +
                        "그림자: " + Loop.Instance.m_ll_LoopDataList[n].m_sLoopSoc.GetSOC_Shadow();

        m_g_Btn_Loop.GetComponent<Button>().onClick.AddListener(delegate { Set_Btn(dn); });
    }

    public void Set_Btn(int n)
    {
        Loop.Instance.Loop_Before(n);
    }
}
