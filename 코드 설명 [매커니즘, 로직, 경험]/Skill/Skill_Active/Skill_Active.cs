using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_SKILL_ACTIVE_TYPE { MOVE, ATTACK }

public class Skill_Active : Skill
{
    [SerializeField]
    GameObject m_gSkillObject;
    [SerializeField]
    Vector2 m_vMoveValue;
    float m_fMoveDistance;
    RaycastHit2D Ray_Move;

    int nLayer1;

    public E_SKILL_ACTIVE_TYPE m_eSkillActiveType;

    public Skill_Active(string name, string des, int code, E_SKILL_TYPE st, E_SKILL_LEVEL sl,
        STATUS stmax, STATUS stmin, SOC smax, SOC smin, SkillEffect se, STATUS stc, SOC sc, E_SKILL_ACTIVE_TYPE sat)
        : base(name, des, code, st, sl, stmax, stmin, smax, smin, se, stc, sc) 
    {
        m_eSkillType = E_SKILL_TYPE.ACTIVE;
        m_eSkillActiveType = sat;

        nLayer1 = 1 << LayerMask.NameToLayer("Hurdle") | 1 << LayerMask.NameToLayer("CameraWall");
    }

    override public void Set_MOVE(Vector2 pos, float distance)
    {
        m_vMoveValue = Vector3.Normalize(pos);
        m_fMoveDistance = distance;
    }


    Vector3 m_vRayPoint = new Vector3();
    public override bool UseSkill(GameObject mainobj)
    {
        Debug.DrawRay(mainobj.transform.position, m_vMoveValue * m_fMoveDistance, Color.blue, 5);
        Ray_Move = Physics2D.Raycast(mainobj.transform.position, m_vMoveValue, m_fMoveDistance, nLayer1);

        if (Ray_Move.collider != null)
        {
            m_vRayPoint = Ray_Move.point;

            if (mainobj.transform.position.y < m_vRayPoint.y)
                mainobj.transform.position = m_vRayPoint - new Vector3(0, 0.1f, 0);
            else if (mainobj.transform.position.y > m_vRayPoint.y)
                mainobj.transform.position = m_vRayPoint + new Vector3(0, 0.1f, 0);

            if (mainobj.transform.position.x < m_vRayPoint.x)
                mainobj.transform.position = m_vRayPoint - new Vector3(0.1f, 0, 0);
            else if(mainobj.transform.position.x > m_vRayPoint.x)
                mainobj.transform.position = m_vRayPoint + new Vector3(0.1f, 0, 0);
        }
        else
        {
            mainobj.transform.position += (Vector3)m_vMoveValue * m_fMoveDistance;
        }
        
        // 스킬 사용 주체가 Player.
        if (mainobj.layer == LayerMask.NameToLayer("Player"))
        {

        }
        // 스킬 사용 주체가 Monster.
        else if (mainobj.layer == LayerMask.NameToLayer("Monster"))
        {
            // 스킬 사용 주체가 Monster_Boss.
            if (mainobj.tag == "Boss")
            {
                // 스킬 사용 주체가 Monster_Boss_TeSlime.
                if (mainobj.name == "TeSlime")
                {

                }
            }
        }

        return true;
    }
}
