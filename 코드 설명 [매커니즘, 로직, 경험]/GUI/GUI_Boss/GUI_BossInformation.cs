using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUI_BossInformation : MonoBehaviour
{
    [SerializeField] GameObject m_gPanel_BossInformation;

    [SerializeField] GameObject m_gPanel_BossInformation_UpBar;
    [SerializeField] TextMeshProUGUI m_TMP_BossInformation_UpBar_Name;
    [SerializeField] TextMeshProUGUI m_TMP_BossInformation_UpBar_Time;

    [SerializeField] GameObject m_gPanel_BossInformation_HpBar;
    [SerializeField] Image m_IMG_BossInformation_HpBar_Current;
    [SerializeField] TextMeshProUGUI m_TMP_BossInformation_HpBar_Info;

    int m_nMinute;
    int m_nSecond;

    public void InitialSet()
    {
        InitialSet_Object();
    }

    void InitialSet_Object()
    {
        m_gPanel_BossInformation = GameObject.Find("Canvas_GUI").gameObject.transform.Find("Panel_BossInformation").gameObject;

        m_gPanel_BossInformation_UpBar = m_gPanel_BossInformation.transform.Find("Panel_BossInformation_UpBar").gameObject;
        m_TMP_BossInformation_UpBar_Name = m_gPanel_BossInformation_UpBar.transform.Find("TMP_BossInformation_UpBar_Name").gameObject.GetComponent<TextMeshProUGUI>();
        m_TMP_BossInformation_UpBar_Time = m_gPanel_BossInformation_UpBar.transform.Find("TMP_BossInformation_UpBar_Time").gameObject.GetComponent<TextMeshProUGUI>();

        m_gPanel_BossInformation_HpBar = m_gPanel_BossInformation.transform.Find("Panel_BossInformation_HpBar").gameObject;
        m_IMG_BossInformation_HpBar_Current = m_gPanel_BossInformation_HpBar.transform.Find("Panel_BossInformation_HpBar_Current").gameObject.GetComponent<Image>();
        m_TMP_BossInformation_HpBar_Info = m_gPanel_BossInformation_HpBar.transform.Find("TMP_BossInformation_HpBar_Info").gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void Display_GUI_BossInformation(string bossname, int bosslv)
    {
        m_gPanel_BossInformation.transform.SetAsLastSibling();

        m_gPanel_BossInformation.SetActive(true);

        m_TMP_BossInformation_UpBar_Name.text = "Lv " + bosslv.ToString() + ". ";
        m_TMP_BossInformation_UpBar_Name.text += bossname;
    }
    public void UnDisplay_GUI_BossInformation()
    {
        m_gPanel_BossInformation.SetActive(false);
    }

    public void Update_BossInformation(int bosshp_max, int bosshp_current)
    {
        m_IMG_BossInformation_HpBar_Current.fillAmount = Mathf.Lerp(m_IMG_BossInformation_HpBar_Current.fillAmount, (float)bosshp_current / (float)bosshp_max, Time.deltaTime);
        m_TMP_BossInformation_HpBar_Info.text = bosshp_current.ToString() + " / " + bosshp_max.ToString();
    }
    public void Update_BossInformation_Time(int time)
    {
        m_nMinute = time / 60;
        m_nSecond = time % 60;

        m_TMP_BossInformation_UpBar_Time.text = "";

        if (m_nMinute != 0)
            m_TMP_BossInformation_UpBar_Time.text = m_nMinute.ToString() + " 분";

        m_TMP_BossInformation_UpBar_Time.text += m_nSecond.ToString() + " 초";
    }
}
