using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Total : MonoBehaviour
{
    public GameObject m_gTarget;
    public Monster_Status m_ms_Status;
    public Monster_Move m_mm_Move;
    public Monster_Drop m_md_Drop;
    public Monster_Effect m_me_Effect;

    // m_bRelation == true : 몹에게 데이면 데미지 들어감
    // m_bRelation == false : 몹에게 데여도 데미지 X
    public bool m_bRelation;

    // m_bWait == true : 아무 상호작용도 할수 없다. 태어나기전.
    // m_bWait == false : 모든 상호작용 가능.
    public bool m_bWait;

    // 최적화를 위해 고안.
    // m_bPlay == true: Player 가 해당 맵에 위치하여 Monster 가 상호작용 가능.
    // m_bPlay == false: Player 가 해당 맵에서 벗어나 Monster 가 상호작용 하지 않음.
    public bool m_bPlay;

    public int nLayer1;

    // random 방향 정하기
    public Vector3 m_vDir = Vector3.right;
    public int m_nRandomNumber;
    public bool m_bSetTime;
    public float m_fTime;

    private void Awake()
    {
        m_ms_Status = this.gameObject.GetComponent<Monster_Status>();
        m_mm_Move = this.gameObject.GetComponent<Monster_Move>();
        m_md_Drop = this.gameObject.GetComponent<Monster_Drop>();
        m_me_Effect = this.gameObject.GetComponent<Monster_Effect>();
        m_bRelation = false;

        nLayer1 = 1 << LayerMask.NameToLayer("Player");
        m_bWait = false;
        m_bPlay = false;
    }

    void Start()
    {
        Fadein();
    }

    virtual public void Move()
    {

    }

    virtual public void Chase()
    {

    }

    virtual public void Death(float time)
    {
        m_gTarget.GetComponent<Player_Total>().MobDeath(this);

        StartCoroutine(ProcessRespone(time));

        m_bWait = true;
        m_mm_Move.Death();
        m_md_Drop.DropItem_Death(m_ms_Status.m_nMonsterCode, this.gameObject.transform.position);
    }
    // 일회용 오브젝트 사망.
    virtual public void Death()
    {
        m_gTarget.GetComponent<Player_Total>().MobDeath(this);

        StartCoroutine(ProcessDeath());

        m_bWait = true;
        m_mm_Move.Death();
        //m_md_Drop.DropItem(this.gameObject.transform.position);
        m_md_Drop.DropItem_Death(m_ms_Status.m_nMonsterCode, this.gameObject.transform.position);
    }

    virtual public void Respone()
    {
        m_bWait = false;
        m_mm_Move.Respone();
        m_ms_Status.Respone();
    }

    virtual public IEnumerator ProcessRespone(float time)
    {
        yield return new WaitForSeconds(time);
        //ebug.Log("3 Seconds Later");
        Respone();
    }

    virtual public IEnumerator ProcessDeath()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }

    virtual public void SetDir()
    {

    }

    virtual public bool Attacked(int dm, float dmrate, GameObject gm)
    {
        if (m_mm_Move.m_bPower == false)
        {
            m_gTarget = gm;

            if (m_ms_Status.Attacked(dm, dmrate) == true)
            {
                Death(0);
            }
            else
                m_mm_Move.Attacked();

            return true;
        }

        return false;
    }

    virtual public SOC Goaway()
    {
        m_bWait = true;

        return m_ms_Status.m_sSoc_Goaway;
    }

    virtual public void Detect()
    {

    }

    protected Collider2D[] co2_2;
    protected Vector2 m_vSize_HitBody;
    protected Vector3 m_vKnockBackDir;
    virtual public void BodyDamage(float percent, float radius, Vector3 offset, float knockbacktime = 0.3f)
    {
        co2_2 = Physics2D.OverlapCircleAll(this.transform.position + offset, radius, nLayer1);
        if (co2_2.Length > 0)
        {
            for (int i = 0; i < co2_2.Length; i++)
            {
                if (co2_2[i].gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    m_vKnockBackDir = Vector3.Normalize(co2_2[i].gameObject.transform.position - this.transform.position);
                    co2_2[i].GetComponent<Player_Total>().Attacked((int)((float)m_ms_Status.m_sStatus.GetSTATUS_Damage_Total() * percent), m_vKnockBackDir, knockbacktime, m_ms_Status.m_sMonsterName);
                }
            }
        }
    }

    virtual public bool Attack(float attackspeed)
    {
        if (m_mm_Move.Attack(attackspeed) == true)
            return true;
        else
            return false;
    }

    virtual public void Attack_Check()
    {
        
    }

    virtual public void Fadein()
    {
        m_mm_Move.Fadein();
    }
}
