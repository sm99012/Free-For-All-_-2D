using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_ChangeMap : MonoBehaviour
{
    [SerializeField] public GameObject m_gPanel_ChangeMap;
    [SerializeField] Image m_IMG_ChangeMap;
    float m_fAlpha;

    public void InitialSet()
    {
        InitialSet_Object();
    }

    // 초기 Object 불러오기.
    void InitialSet_Object()
    {
        m_gPanel_ChangeMap = GameObject.Find("Canvas_GUI").gameObject.transform.Find("Panel_ChangeMap").gameObject;
        m_IMG_ChangeMap = m_gPanel_ChangeMap.GetComponent<Image>();
    }

    public void Fade(float startingwatetime = 0)
    {
        StartCoroutine(Process_Fade(startingwatetime));
    }
    IEnumerator Process_Fade(float startingwatetime)
    {
        yield return new WaitForSeconds(startingwatetime);

        m_gPanel_ChangeMap.transform.SetAsLastSibling();
        m_gPanel_ChangeMap.SetActive(true);
        m_fAlpha = 0;
        while (m_fAlpha < 1)
        {
            m_fAlpha += Time.deltaTime;
            m_IMG_ChangeMap.color = new Color(m_IMG_ChangeMap.color.r, m_IMG_ChangeMap.color.g, m_IMG_ChangeMap.color.b, m_fAlpha);
            yield return null;
        }
        m_fAlpha = 1;
        m_IMG_ChangeMap.color = new Color(m_IMG_ChangeMap.color.r, m_IMG_ChangeMap.color.g, m_IMG_ChangeMap.color.b, m_fAlpha);
        yield return new WaitForSeconds(Time.deltaTime * 120);
        while (m_fAlpha > 0)
        {
            m_fAlpha -= Time.deltaTime;
            m_IMG_ChangeMap.color = new Color(m_IMG_ChangeMap.color.r, m_IMG_ChangeMap.color.g, m_IMG_ChangeMap.color.b, m_fAlpha);
            yield return null;
        }
        m_gPanel_ChangeMap.SetActive(false);
    }
}
