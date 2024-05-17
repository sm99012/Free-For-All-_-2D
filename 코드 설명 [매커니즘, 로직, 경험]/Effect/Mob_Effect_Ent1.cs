using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_Effect_Ent1 : MonoBehaviour
{
    int nLayer1;
    int m_nDamage;

    string m_sAttackName;

    Vector3 m_vPos;

    Animator m_aAnimator;

    void Start()
    {
        m_aAnimator = this.GetComponent<Animator>();
        StartCoroutine(ProcessEffect());

        nLayer1 = 1 << LayerMask.NameToLayer("Player");
    }

    public void InitialSet(Vector3 pos, int damage, string attackname)
    {
        m_vPos = pos;
        m_nDamage = damage;
        m_sAttackName = attackname;
    }

    IEnumerator ProcessEffect()
    {
        Debug.Log("뿌리공격");
        m_aAnimator.SetBool("ON", true);
        yield return new WaitForSeconds(1f);
        m_aAnimator.SetBool("ON", false);
        Destroy(this.gameObject);
    }

    Collider2D[] co2_3;
    float m_fAttackRadius = 0.1f;
    public void Attack_Check()
    {
        co2_3 = Physics2D.OverlapCircleAll(this.transform.position, m_fAttackRadius, nLayer1);
        if (co2_3.Length > 0)
        {
            for (int i = 0; i < co2_3.Length; i++)
            {
                Vector3 m_vKnockBackDir = Vector3.Normalize(co2_3[i].gameObject.transform.position - this.transform.position); ;
                SetDir(this.transform.position, co2_3[i].gameObject.transform.position);
                co2_3[i].gameObject.GetComponent<Player_Total>().Attacked((int)(m_nDamage * 1.2f), m_vKnockBackDir, 0.3f, m_sAttackName);
                
                break;
            }
        }
    }

    void SetDir(Vector3 startpos, Vector3 endpos)
    {
        if (startpos.x >= endpos.x)
            this.transform.localScale = new Vector3(1, 1, 1);
        else
            this.transform.localScale = new Vector3(-1, 1, 1);
    }
}