using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect1 : MonoBehaviour
{
    Animator m_aAnimator;

    void Start()
    {
        m_aAnimator = this.GetComponent<Animator>();
        StartCoroutine(ProcessEffect());
    }

    IEnumerator ProcessEffect()
    {
        m_aAnimator.SetBool("ON", true);
        yield return new WaitForSeconds(0.417f);
        m_aAnimator.SetBool("ON", false);
        Destroy(this.gameObject);
    }
}
