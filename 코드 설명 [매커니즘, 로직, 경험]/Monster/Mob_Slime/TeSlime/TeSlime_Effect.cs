using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeSlime_Effect : MonoBehaviour
{
    public List<GameObject> m_List_EffectList; 

    public void InitialSet()
    {
        m_List_EffectList = new List<GameObject>();
        GameObject effect;

        effect = Resources.Load<GameObject>("Prefab/Effect/TeSlime_SwordEffect1");
        m_List_EffectList.Add(effect);
        effect = Resources.Load<GameObject>("Prefab/Effect/TeSlime_SwordEffect2");
        m_List_EffectList.Add(effect);
    }

    public void InitialSet_Effect1_Damage(float damage)
    {
        m_List_EffectList[0].GetComponent<TeSlime_SwordEffect1>().InitialSet_Damage(damage);
    }

    public void InitialSet_Effect2_Damage(float damage)
    {
        m_List_EffectList[1].GetComponent<TeSlime_SwordEffect2>().InitialSet_Damage(damage);
    }

    public void Create_Effect1(Vector3 dir)
    {
        GameObject obj; float angle;
        
        obj = Instantiate(m_List_EffectList[0]);
        obj.transform.position = this.transform.position;
        obj.GetComponent<TeSlime_SwordEffect1>().Set_Dir(dir.normalized);
        obj.GetComponent<TeSlime_SwordEffect1>().InitialSet_Damage(m_List_EffectList[0].GetComponent<TeSlime_SwordEffect1>().m_fDamage);
        obj.GetComponent<TeSlime_SwordEffect1>().Set_Speed(300);
        angle = Mathf.Atan2(dir.normalized.y, dir.normalized.x) * Mathf.Rad2Deg;
        obj.transform.rotation = Quaternion.AngleAxis( - angle, Vector3.back);
    }

    public void Create_Effect2()
    {
        GameObject obj; float angle;

        obj = Instantiate(m_List_EffectList[0]);
        obj.transform.position = this.transform.position;
        obj.GetComponent<TeSlime_SwordEffect1>().Set_Dir(Vector3.up);
        obj.GetComponent<TeSlime_SwordEffect1>().InitialSet_Damage(m_List_EffectList[0].GetComponent<TeSlime_SwordEffect1>().m_fDamage);
        obj.GetComponent<TeSlime_SwordEffect1>().Set_Speed(300);
        angle = 90;
        obj.transform.rotation = Quaternion.AngleAxis( - angle, Vector3.back);

        obj = Instantiate(m_List_EffectList[0]);
        obj.transform.position = this.transform.position;
        obj.GetComponent<TeSlime_SwordEffect1>().Set_Dir((Vector3.up + Vector3.left).normalized);
        obj.GetComponent<TeSlime_SwordEffect1>().InitialSet_Damage(m_List_EffectList[0].GetComponent<TeSlime_SwordEffect1>().m_fDamage);
        obj.GetComponent<TeSlime_SwordEffect1>().Set_Speed(300);
        angle = 135;
        obj.transform.rotation = Quaternion.AngleAxis( - angle, Vector3.back);

        obj = Instantiate(m_List_EffectList[0]);
        obj.transform.position = this.transform.position;
        obj.GetComponent<TeSlime_SwordEffect1>().Set_Dir((Vector3.left).normalized);
        obj.GetComponent<TeSlime_SwordEffect1>().InitialSet_Damage(m_List_EffectList[0].GetComponent<TeSlime_SwordEffect1>().m_fDamage);
        obj.GetComponent<TeSlime_SwordEffect1>().Set_Speed(300);
        angle = 180;
        obj.transform.rotation = Quaternion.AngleAxis( - angle, Vector3.back);

        obj = Instantiate(m_List_EffectList[0]);
        obj.transform.position = this.transform.position;
        obj.GetComponent<TeSlime_SwordEffect1>().Set_Dir((Vector3.down + Vector3.left).normalized);
        obj.GetComponent<TeSlime_SwordEffect1>().InitialSet_Damage(m_List_EffectList[0].GetComponent<TeSlime_SwordEffect1>().m_fDamage);
        obj.GetComponent<TeSlime_SwordEffect1>().Set_Speed(300);
        angle = 225;
        obj.transform.rotation = Quaternion.AngleAxis( - angle, Vector3.back);

        obj = Instantiate(m_List_EffectList[0]);
        obj.transform.position = this.transform.position;
        obj.GetComponent<TeSlime_SwordEffect1>().Set_Dir((Vector3.down).normalized);
        obj.GetComponent<TeSlime_SwordEffect1>().InitialSet_Damage(m_List_EffectList[0].GetComponent<TeSlime_SwordEffect1>().m_fDamage);
        obj.GetComponent<TeSlime_SwordEffect1>().Set_Speed(300);
        angle = 270;
        obj.transform.rotation = Quaternion.AngleAxis( - angle, Vector3.back);

        obj = Instantiate(m_List_EffectList[0]);
        obj.transform.position = this.transform.position;
        obj.GetComponent<TeSlime_SwordEffect1>().Set_Dir((Vector3.down + Vector3.right).normalized);
        obj.GetComponent<TeSlime_SwordEffect1>().InitialSet_Damage(m_List_EffectList[0].GetComponent<TeSlime_SwordEffect1>().m_fDamage);
        obj.GetComponent<TeSlime_SwordEffect1>().Set_Speed(300);
        angle = 315;
        obj.transform.rotation = Quaternion.AngleAxis( - angle, Vector3.back);

        obj = Instantiate(m_List_EffectList[0]);
        obj.transform.position = this.transform.position;
        obj.GetComponent<TeSlime_SwordEffect1>().Set_Dir((Vector3.right).normalized);
        obj.GetComponent<TeSlime_SwordEffect1>().InitialSet_Damage(m_List_EffectList[0].GetComponent<TeSlime_SwordEffect1>().m_fDamage);
        obj.GetComponent<TeSlime_SwordEffect1>().Set_Speed(300);
        angle = 360;
        obj.transform.rotation = Quaternion.AngleAxis( - angle, Vector3.back);

        obj = Instantiate(m_List_EffectList[0]);
        obj.transform.position = this.transform.position;
        obj.GetComponent<TeSlime_SwordEffect1>().Set_Dir((Vector3.up + Vector3.right).normalized);
        obj.GetComponent<TeSlime_SwordEffect1>().InitialSet_Damage(m_List_EffectList[0].GetComponent<TeSlime_SwordEffect1>().m_fDamage);
        obj.GetComponent<TeSlime_SwordEffect1>().Set_Speed(300);
        angle = 45;
        obj.transform.rotation = Quaternion.AngleAxis( - angle, Vector3.back);
    }

    public void Create_Effect3(Vector3 dir)
    {
        GameObject obj; float angle;

        obj = Instantiate(m_List_EffectList[1]);
        obj.transform.position = this.transform.position;
        obj.GetComponent<TeSlime_SwordEffect2>().Set_Dir(dir.normalized);
        obj.GetComponent<TeSlime_SwordEffect2>().InitialSet_Damage(m_List_EffectList[1].GetComponent<TeSlime_SwordEffect2>().m_fDamage);
        obj.GetComponent<TeSlime_SwordEffect2>().Set_Speed(300);
        angle = Mathf.Atan2(dir.normalized.y, dir.normalized.x) * Mathf.Rad2Deg;
        obj.transform.rotation = Quaternion.AngleAxis( - angle, Vector3.back);
    }
}
