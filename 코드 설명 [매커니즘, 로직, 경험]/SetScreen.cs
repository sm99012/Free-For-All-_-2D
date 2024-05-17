using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SetScreen : MonoBehaviour
{
    int m_nWidth = 1600;
    int m_nHeight = 900;

    [SerializeField] float m_fScreenRatio;

    [SerializeField] float m_fScreenRatio_Set;


    float m_fTime = 0;

    private static Dictionary<int, ScreenSize> m_sDictionary_ScreenSize;

    void Start()
    {
        Screen.SetResolution(m_nWidth, m_nHeight, false);
        m_fScreenRatio = (float)Math.Round(((float)m_nWidth / (float)m_nHeight), 3);

        m_sDictionary_ScreenSize = new Dictionary<int, ScreenSize>();
        m_sDictionary_ScreenSize.Add(1600, new ScreenSize(1600, 900));
        m_sDictionary_ScreenSize.Add(1440, new ScreenSize(1440, 810));
        m_sDictionary_ScreenSize.Add(1280, new ScreenSize(1280, 720));
        m_sDictionary_ScreenSize.Add(1120, new ScreenSize(1120, 630));
        m_sDictionary_ScreenSize.Add(960, new ScreenSize(960, 540));
        m_sDictionary_ScreenSize.Add(800, new ScreenSize(800, 450));
    }

    int number = 0;

    internal static Dictionary<int, ScreenSize> SDictionary_ScreenSize { get => m_sDictionary_ScreenSize; set => m_sDictionary_ScreenSize = value; }

    private void Update()
    {
        // Screen.SetResolution(m_nWidth, m_nHeight, false);
        //m_fTime += Time.deltaTime;

        //if (m_fTime >= 1f)
        //{
        //    //m_fTime = 0;
        //    //GUIManager_Total.Instance.UpdateLog("Screen Size: " + Screen.width + " / " + Screen.height);
        //    m_fScreenRatio = (float)Math.Round(((float)Screen.width / (float)Screen.height), 3);
        //    //Debug.Log("Screen Size: " + Screen.width + " / " + Screen.height);

        //    if (m_fScreenRatio > 1.77)
        //    {
        //        // 가로비율 > 세로비율
        //        m_fScreenRatio_Set = (float)Screen.height / (float)m_nHeight;
        //    }
        //    else if (m_fScreenRatio <= 1.77)
        //    {
        //        // 가로비율 <= 세로비율
        //        m_fScreenRatio_Set = (float)Screen.width / (float)m_nWidth;
        //    }

        //    Screen.SetResolution((int)((float)m_nWidth * m_fScreenRatio_Set), (int)((float)m_nHeight * m_fScreenRatio_Set), false);
        //    GUIManager_Total.Instance.UpdateLog((float)m_nWidth * m_fScreenRatio_Set + " / " + (float)m_nHeight * m_fScreenRatio_Set);
        //}

        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    number += 1;
        //    if (number > m_sl_ScreenSize.Count)
        //    {
        //        number = 0;
        //    }
        //    Screen.SetResolution(m_sl_ScreenSize[number].m_nHorizontalValue, m_sl_ScreenSize[number].m_nVerticalValue, false);
        //}
    }


}

class ScreenSize
{
    public int m_nHorizontalValue;
    public int m_nVerticalValue;

    public ScreenSize(int hv, int vv)
    {
        this.m_nHorizontalValue = hv;
        this.m_nVerticalValue = vv;
    }
}