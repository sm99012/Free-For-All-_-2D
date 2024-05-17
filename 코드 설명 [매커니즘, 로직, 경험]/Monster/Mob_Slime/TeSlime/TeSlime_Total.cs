using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TeSlime_Total : MonoBehaviour
{
    // Player.
    public GameObject m_gTarget;

    // Has-a Relation.
    public TeSlime_Status m_ts_Status;
    public TeSlime_Move m_tm_Move;
    public TeSlime_Drop m_td_Drop;
    public TeSlime_Effect m_te_Effect;
    public TeSlime_Skill m_ts_Skill;

    // Attack1_1 Attack Area
    [SerializeField]
    BoxCollider2D m_Collider_Attack1_1;

    // Attack1_2 Attack Area
    [SerializeField]
    GameObject m_gTrailObject1_2;
    [SerializeField]
    LineRenderer m_LineRenderer1_2;
    [SerializeField]
    MeshCollider m_MeshCollider1_2;
    [SerializeField]
    Mesh m_Mesh1_2;

    // Focus Effect
    [SerializeField]
    GameObject m_gFocusEffect;
    [SerializeField]
    ParticleSystem m_ParticleSystem_FocusEffect;

    // Player Layer.
    int m_nLayer1;

    // Attack1_2 / Attack1_3 결정.
    int m_nRandom_Attack;

    float m_fDistance_Target;

    Vector3 m_vDirection_Attack1_3;
    
    enum E_TESLIME_PHASE { L1, L2, L3 }

    E_TESLIME_PHASE m_eTeSlime_Phase;

    private void Awake()
    {
        m_ts_Status = this.GetComponent<TeSlime_Status>();
        m_tm_Move = this.GetComponent<TeSlime_Move>();
        m_td_Drop = this.GetComponent<TeSlime_Drop>();
        m_te_Effect = this.GetComponent<TeSlime_Effect>();
        m_ts_Skill = this.GetComponent<TeSlime_Skill>();

        m_Collider_Attack1_1 = transform.Find("Area_Attack1_1").gameObject.GetComponent<BoxCollider2D>();
        m_gTrailObject1_2 = transform.Find("Area_Attack1_2").gameObject;
        m_LineRenderer1_2 = m_gTrailObject1_2.GetComponent<LineRenderer>();
        m_MeshCollider1_2 = m_gTrailObject1_2.GetComponent<MeshCollider>();
        m_Mesh1_2 = new Mesh();

        m_gFocusEffect = transform.Find("Effect_Focus").gameObject;
        m_gFocusEffect.SetActive(false);
        m_ParticleSystem_FocusEffect = m_gFocusEffect.GetComponent<ParticleSystem>();

        m_nLayer1 = 1 << LayerMask.NameToLayer("Player");

        SetTeSlimePhase();
    }

    private void Start()
    {
        m_gTarget = Player_Total.Instance.gameObject;

        m_ts_Status.InitialSet();
        m_tm_Move.InitialSet();
        m_te_Effect.InitialSet();
        m_te_Effect.InitialSet_Effect1_Damage(m_ts_Status.m_sStatus.GetSTATUS_Damage_Total());
        m_te_Effect.InitialSet_Effect2_Damage(m_ts_Status.m_sStatus.GetSTATUS_Damage_Total());
        m_ts_Skill.InitialSet();

        this.gameObject.name = "TeSlime";

        GUIManager_Total.Instance.Display_GUI_BossInformation("테 슬라임", 21);
        Debug.Log(m_ts_Status.m_sStatus.GetSTATUS_HP_Current() + " / " + m_ts_Status.m_sStatus.GetSTATUS_HP_Max());
        GUIManager_Total.Instance.Update_BossInformation(m_ts_Status.m_sStatus.GetSTATUS_HP_Max(), m_ts_Status.m_sStatus.GetSTATUS_HP_Current());

        m_tm_Move.Fadein();
    }

    private void Update()
    {
        if (m_tm_Move.m_eTeSlimeState == TeSlime_Move.E_TESLIME_MOVE_STATE.EYE || m_tm_Move.m_eTeSlimeState == TeSlime_Move.E_TESLIME_MOVE_STATE.CHASE)
        {
            if (Player_Total.Instance.m_pm_Move.m_ePlayerMoveState != Player_Move.E_PLAYER_MOVE_STATE.DEATH)
            {
                SetDir();
                Detect();
            }
        }

        GUIManager_Total.Instance.Update_BossInformation(m_ts_Status.m_sStatus.GetSTATUS_HP_Max(), m_ts_Status.m_sStatus.GetSTATUS_HP_Current());

        //if (Input.GetKeyUp(KeyCode.Alpha1))
        //{
        //    Focus();
        //}
    }

    // Function.
    void Eye()
    {
        m_tm_Move.Eye();
    }

    void Chase()
    {
        m_tm_Move.Chase(m_ts_Status.m_sStatus.GetSTATUS_Speed());
    }

    void Focus()
    {
        m_tm_Move.Focus();
    }
    void CheckFocus()
    {
        Debug.Log("TeSlime.Focus");
        //m_ts_Status.m_sStatus.SetSTATUS_HP_Current((int)(m_ts_Status.m_sStatus.GetSTATUS_HP_Max() * 0.05f));
        //m_ts_Status.m_sStatus.SetSTATUS_MP_Current((int)(m_ts_Status.m_sStatus.GetSTATUS_MP_Max() * 0.05f));
        m_ts_Status.m_sStatus.SetSTATUS_HP_Current(m_ts_Status.m_sStatus.GetSTATUS_HP_Current() + 10);
        if (m_ts_Status.m_sStatus.GetSTATUS_HP_Current() > m_ts_Status.m_sStatus.GetSTATUS_HP_Max())
            m_ts_Status.m_sStatus.SetSTATUS_HP_Current(m_ts_Status.m_sStatus.GetSTATUS_HP_Max());

        m_ts_Status.m_sStatus.SetSTATUS_MP_Current(m_ts_Status.m_sStatus.GetSTATUS_MP_Current() + 10);
        if (m_ts_Status.m_sStatus.GetSTATUS_MP_Current() > m_ts_Status.m_sStatus.GetSTATUS_MP_Max())
            m_ts_Status.m_sStatus.SetSTATUS_MP_Current(m_ts_Status.m_sStatus.GetSTATUS_MP_Max());

        SetTeSlimePhase();

        //GUIManager_Total.Instance.Update_BossInformation(m_ts_Status.m_sStatus.GetSTATUS_HP_Max(), m_ts_Status.m_sStatus.GetSTATUS_HP_Current());
    }

    Collider2D[] co2_d;
    // 공격 범위 감지.
    void Detect()
    {
        co2_d = Physics2D.OverlapBoxAll(this.transform.position, new Vector2(1.1f, 0.2f), 0, m_nLayer1);
        m_fDistance_Target = Vector3.Distance(m_gTarget.transform.position, this.transform.position);

        if (co2_d.Length > 0)
        {
            if (m_eTeSlime_Phase == E_TESLIME_PHASE.L3)
            {
                m_nRandom_Attack = Random.Range(1, 101);
                if (m_nRandom_Attack > 25)
                {
                    Attack1_1(m_ts_Status.m_sStatus.GetSTATUS_AttackSpeed());
                }
                else if (m_nRandom_Attack > 50)
                {
                    Focus();
                }
                else
                {
                    m_nRandom_Attack = Random.Range(1, 101);
                    if (m_nRandom_Attack > 50)
                        Pattern_Attack2(m_ts_Status.m_sStatus.GetSTATUS_AttackSpeed());
                    else
                        Attack1_3();
                }
            }
            else
            {
                Attack1_1(m_ts_Status.m_sStatus.GetSTATUS_AttackSpeed());
            }
        }
        else
        {
            if (m_eTeSlime_Phase == E_TESLIME_PHASE.L1)
            {
                Chase();
                if (m_fDistance_Target >= 0.6f)
                {
                    Attack1_2();

                    //case 3:
                    //    {
                    //        Attack1_3();
                    //    }
                    //    break;
                }
            }
            else if (m_eTeSlime_Phase == E_TESLIME_PHASE.L2)
            {
                Chase();
                if (m_fDistance_Target >= 0.6f)
                {
                    if ((m_nRandom_Attack = Random.Range(1, 101)) > 50)
                        Pattern_Attack2(m_ts_Status.m_sStatus.GetSTATUS_AttackSpeed());
                    else
                        Attack1_3();
                }
            }
            else if (m_eTeSlime_Phase == E_TESLIME_PHASE.L3)
            {
                m_nRandom_Attack = Random.Range(1, 101);
                if (m_nRandom_Attack > 70)
                {
                    Chase();
                }
                else
                {
                    Focus();
                }

                if (m_fDistance_Target >= 0.6f)
                {
                    m_nRandom_Attack = Random.Range(1, 101);
                    if (m_nRandom_Attack > 50)
                        Pattern_Attack2(m_ts_Status.m_sStatus.GetSTATUS_AttackSpeed());
                    else
                        Attack1_3();
                }
            }
        }
    }

    // 기본 공격.
    bool Attack1_1(float speed)
    {
        if (m_tm_Move.Attack1_1(speed) == true)
        {
            Debug.Log("TeSlime.Attack1_1");

            return true;
        }

        return false;
    }
    // 기본 공격 히트 판정.
    Collider2D[] co2_1;
    Vector3 m_vOffset_Attack1_1;
    // Animation Event 를 통해 관리.
    void Check_Attack1_1()
    {
        //Debug.Log("'테 슬라임'의 기본 공격.");

        if (m_tm_Move.m_vDir.x >= 0)
        {
            m_vOffset_Attack1_1 = new Vector3(this.transform.position.x + m_Collider_Attack1_1.offset.x, this.transform.position.y + m_Collider_Attack1_1.offset.y, 0);
            co2_1 = Physics2D.OverlapBoxAll(m_vOffset_Attack1_1, m_Collider_Attack1_1.size, 0, m_nLayer1);
        }
        else
        {
            m_vOffset_Attack1_1 = new Vector3(this.transform.position.x - m_Collider_Attack1_1.offset.x, this.transform.position.y + m_Collider_Attack1_1.offset.y, 0);
            co2_1 = Physics2D.OverlapBoxAll(m_vOffset_Attack1_1, m_Collider_Attack1_1.size, 0, m_nLayer1);
        }

        if (co2_1.Length > 0)
        {            
            for (int i = 0; i < co2_1.Length; i++)
            {
                Vector3 m_vKnockBackDir = Vector3.Normalize(co2_1[i].gameObject.transform.position - this.transform.position);
                if (co2_1[i].GetComponent<Player_Total>().Attacked((int)((float)m_ts_Status.m_sStatus.GetSTATUS_Damage_Total()), m_vKnockBackDir, 0.3f, m_ts_Status.m_sMonsterName) == true)
                    co2_1[i].GetComponent<Player_Total>().ApplySkill(m_ts_Skill.m_Dictionary_Skill["TeSlime_Slow1"]);
            }
        }
    }

    // 돌진 공격.
    bool Attack1_2()
    {
        if (m_tm_Move.Check_Attack1_2() == true)
        {
            if (m_tm_Move.Attack1_2() == true)
                if (m_ts_Status.UseSkill(0, 3) == true)
                {
                    Debug.Log("TeSlime.Attack1_2");
                    StartCoroutine(Process_Check_Attack1_2());

                    return true;
                }
        }
        return false;
    }
    // 돌진 공격 히트 판정.
    Collider2D[] co2_2;
    Vector3 m_vDirection_Attack1_2;
    Vector3 m_vEndPos;
    [SerializeField]
    float m_fDistance_Attack1_2;
    void Check_Attack1_2()
    {
        //float angle = m_Mesh1_2.bounds.size.y / m_Mesh1_2.bounds.size.x;
        float angle = Mathf.Atan2(m_vDirection_Attack1_2.y, m_vDirection_Attack1_2.x) * Mathf.Rad2Deg;
        //Debug.Log(angle + ", " + m_Mesh1_2.bounds.center + ", " + m_Mesh1_2.bounds.size);
        float x_distance = Mathf.Sqrt((Mathf.Pow(m_Mesh1_2.bounds.size.x, 2) + Mathf.Pow(m_Mesh1_2.bounds.size.y, 2)));
        co2_2 = Physics2D.OverlapBoxAll(m_Mesh1_2.bounds.center, new Vector3(x_distance, 0.2f), angle, m_nLayer1);

        if (co2_2.Length > 0)
        {
            for (int i = 0; i < co2_2.Length; i++)
            {
                Vector3 m_vKnockBackDir = Vector3.Normalize(co2_2[i].gameObject.transform.position - this.transform.position);
                co2_2[i].GetComponent<Player_Total>().Attacked((int)((float)m_ts_Status.m_sStatus.GetSTATUS_Damage_Total() * 2f), m_vKnockBackDir, 0.3f, m_ts_Status.m_sMonsterName);
            }
        }
    }
    // 돌진 공격의 범위를 알려주는 스크립트로 Coroutine 을 사용하여 따로 실행.
    IEnumerator Process_Check_Attack1_2()
    {
        m_gTrailObject1_2.SetActive(true);

        if (m_gTrailObject1_2 != null)
        {
            m_vDirection_Attack1_2 = Vector3.Normalize(m_gTarget.transform.position - m_gTrailObject1_2.transform.position);
            m_fDistance_Attack1_2 = Vector3.Distance(m_gTarget.transform.position, m_gTrailObject1_2.transform.position);
            m_vEndPos = m_gTarget.transform.position;

            float angle = Mathf.Atan2(m_vDirection_Attack1_2.x, m_vDirection_Attack1_2.y) * Mathf.Rad2Deg;

            m_gTrailObject1_2.transform.rotation = Quaternion.AngleAxis(90, Vector3.back);

            m_LineRenderer1_2.startWidth = 0.2f;
            m_LineRenderer1_2.startColor = new Color(1, 0, 0, 1);
            m_LineRenderer1_2.endWidth = 0.2f;
            m_LineRenderer1_2.endColor = new Color(1, 0, 0, 1);

            m_LineRenderer1_2.SetPosition(0, m_gTrailObject1_2.transform.position);
            m_LineRenderer1_2.SetPosition(1, m_gTarget.transform.position + Vector3.Normalize(m_gTarget.transform.position - m_gTrailObject1_2.transform.position) * 0.4f);

            m_LineRenderer1_2.BakeMesh(m_Mesh1_2, true);
            m_MeshCollider1_2.sharedMesh = m_Mesh1_2;

            float Alpha = 1;
            while (Alpha >= 0)
            {
                yield return new WaitForSeconds(0.01f);
                Alpha -= 0.03f;

                m_LineRenderer1_2.startColor = new Color(1, 0, 0, Alpha);
                m_LineRenderer1_2.endColor = new Color(1, 0, 0, Alpha);
            }
        }

        m_gTrailObject1_2.SetActive(false);
        if (m_gTrailObject1_2.transform.position.x <= m_gTarget.transform.position.x)
        {
            this.transform.position = m_vEndPos + new Vector3(0.1f, 0.1f, 0);
        }
        else
        {
            this.transform.position = m_vEndPos + new Vector3(-0.1f, 0.1f, 0);
        }
        //m_gTrailObject1_2.transform.position = this.transform.position;

        //yield return new WaitForSeconds(1f);
        //Eye();
    }

    // 참격.
    bool Attack1_3()
    {
        if (m_tm_Move.Check_Attack1_3() == true)
        {
            if (m_ts_Status.UseSkill(0, 2) == true)
            {
                if (m_tm_Move.Attack1_3() == true)
                {
                    Debug.Log("TeSlime.Attack1_3");
                    m_vDirection_Attack1_3 = m_gTarget.transform.position - this.transform.position;

                    return true;
                }
            }
        }
        return false;
    }
    void Create_Attack1_3()
    {
        if (m_ts_Status.UseSkill(0, 2) == true)
        {
            if (m_eTeSlime_Phase == E_TESLIME_PHASE.L2)
            {
                if ((m_nRandom_Attack = Random.Range(1, 101)) > 50)
                {
                    Create_Attack1_3_1();
                }
                else
                {
                    Create_Attack1_3_2();
                }
            }
            if (m_eTeSlime_Phase == E_TESLIME_PHASE.L3)
            {
                m_nRandom_Attack = Random.Range(1, 101);
                if (m_nRandom_Attack < 20)
                {
                    Create_Attack1_3_1();
                }
                else if (m_nRandom_Attack < 40)
                {
                    Create_Attack1_3_2();
                }
                else
                {
                    Create_Attack1_3_3();
                }
            }
        }
    }
    void Create_Attack1_3_1()
    {
        m_te_Effect.Create_Effect1(m_vDirection_Attack1_3);
    }
    void Create_Attack1_3_2()
    {
        StartCoroutine(ProcessCreateAttack1_3_2());
    }
    IEnumerator ProcessCreateAttack1_3_2()
    {
        m_te_Effect.Create_Effect3(m_gTarget.transform.position - this.transform.position);
        yield return new WaitForSeconds(0.5f);
        m_te_Effect.Create_Effect3(m_gTarget.transform.position - this.transform.position);
    }
    void Create_Attack1_3_3()
    {
        m_te_Effect.Create_Effect2();
    }

    public void SetDir()
    {
        if (m_gTarget)
        {
            m_tm_Move.SetDir(m_gTarget.transform.position);
        }

    }

    public bool Attacked(int dm, float dmrate, GameObject gm)
    {
        if (m_tm_Move.m_bPower == false)
        {
            m_gTarget = gm;
            if (m_tm_Move.m_eTeSlimeState != TeSlime_Move.E_TESLIME_MOVE_STATE.FOCUS)
            {
                if (m_ts_Status.Attacked(dm, dmrate) == true)
                {
                    Death();
                }
                else
                {
                    m_tm_Move.Attacked();
                    SetTeSlimePhase();
                }
            }
            else
            {
                if (m_ts_Status.Attacked((int)(dm * 0.5f), dmrate) == true)
                {
                    Death();
                }
                else
                {
                    SetTeSlimePhase();
                }
            }

            //GUIManager_Total.Instance.Update_BossInformation(m_ts_Status.m_sStatus.GetSTATUS_HP_Max(), m_ts_Status.m_sStatus.GetSTATUS_HP_Current());

            return true;
        }
        return false;
    }

    void SetTeSlimePhase()
    {
        if (m_ts_Status.m_sStatus.GetSTATUS_HP_Ratio() > 50)
        {
            m_eTeSlime_Phase = E_TESLIME_PHASE.L1;
            m_tm_Move.Set_Phase1();
        }
        else if (m_ts_Status.m_sStatus.GetSTATUS_HP_Ratio() > 20)
        {
            m_eTeSlime_Phase = E_TESLIME_PHASE.L2;
            m_tm_Move.Set_Phase2();
        }
        else if (m_ts_Status.m_sStatus.GetSTATUS_HP_Ratio() > 0)
        {
            m_eTeSlime_Phase = E_TESLIME_PHASE.L3;
            m_ts_Status.Set_Phase3();
            m_tm_Move.Set_Phase3();
        }
    }

    public void Death()
    {
        if (m_tm_Move.m_eTeSlimeState != TeSlime_Move.E_TESLIME_MOVE_STATE.DEATH)
        {
            m_gTarget.GetComponent<Player_Total>().MobDeath(this);
            m_tm_Move.Death();
            m_td_Drop.DropItem_Death(m_ts_Status.m_nMonsterCode, this.gameObject.transform.position);
            //BossManager.Instance.End_Battle_Boss_Succes();
        }
    }

    // Override Function.
    public void Death(float time)
    {
        m_gTarget.GetComponent<Player_Total>().MobDeath(this);

        m_tm_Move.Death();
        //m_tm_Drop.Death();
    }



    // Attack Pattenr.
    // 기본 공격.
    void Pattern_Attack1()
    {
        Attack1_1(m_ts_Status.m_sStatus.GetSTATUS_AttackSpeed());
    }

    // 돌진 공격 + 기본 공격.
    bool Pattern_Attack2(float speed)
    {
        if (m_tm_Move.Check_Attack1_2() == true)
        {
            if (m_ts_Status.UseSkill(0, 3) == true)
            {
                if (m_tm_Move.Attack1_2() == true)
                {
                    Debug.Log("TeSlime.Attack1_2");
                    StartCoroutine(Process_Check_Attack1_2());

                    return true;
                }
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireSphere(this.transform.position, 0.05f);
        if (m_Collider_Attack1_1 != null)
        {
            if (m_tm_Move.m_vDir.x >= 0)
                Gizmos.DrawWireCube(new Vector3(this.transform.position.x + m_Collider_Attack1_1.offset.x, this.transform.position.y + m_Collider_Attack1_1.offset.y, 0), m_Collider_Attack1_1.size);
            else
                Gizmos.DrawWireCube(new Vector3(this.transform.position.x - m_Collider_Attack1_1.offset.x, this.transform.position.y + m_Collider_Attack1_1.offset.y, 0), m_Collider_Attack1_1.size);
        }

        //if (m_Mesh1_2 != null)
        //{
        //    float x_distance = Mathf.Sqrt((Mathf.Pow(m_Mesh1_2.bounds.size.x, 2) + Mathf.Pow(m_Mesh1_2.bounds.size.y, 2)));
        //    Gizmos.DrawCube(m_Mesh1_2.bounds.center, new Vector2(x_distance, 0.1f));
        //}

        Gizmos.DrawWireCube(this.transform.position, new Vector2(1.1f, 0.4f));
    }

    //private void OnGUI()
    //{
    //    GUI.TextArea(new Rect(120, 0, 100, 40), "HP: " + m_ts_Status.m_sStatus.GetSTATUS_HP_Current() + "/" + m_ts_Status.m_sStatus.GetSTATUS_HP_Max() + "\n" +
    //        "MP: " + m_ts_Status.m_sStatus.GetSTATUS_MP_Current() + "/" + m_ts_Status.m_sStatus.GetSTATUS_MP_Max());

    //}
}
