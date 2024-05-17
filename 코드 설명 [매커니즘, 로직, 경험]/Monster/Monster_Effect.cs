using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Effect : MonoBehaviour
{
    public List<GameObject> m_l_EffectList;
    void Start()
    {
        m_l_EffectList = new List<GameObject>();
        m_l_EffectList = new List<GameObject>();
        GameObject Effect = Resources.Load("Effect/Effect_Yellow_3") as GameObject;
        m_l_EffectList.Add(Effect);
    }

    virtual public void Effect_Goaway(Vector3 pos)
    {
        StartCoroutine(ProcessEffect_Goaway(pos));
    }
    virtual protected IEnumerator ProcessEffect_Goaway(Vector3 pos)
    {
        yield return new WaitForSeconds(0.1f);
        if (m_l_EffectList[0] != null)
        {
            GameObject efc = Instantiate(m_l_EffectList[0]);
            efc.transform.position = pos;
        }
    }

    virtual public void Effect1(Vector3 pos)
    {

    }
    virtual public void Effect1(Vector3 pos, int damage, string attackname)
    {

    }
}
