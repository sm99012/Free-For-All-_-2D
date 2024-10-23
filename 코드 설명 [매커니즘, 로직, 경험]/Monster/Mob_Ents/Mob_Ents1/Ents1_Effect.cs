using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ents1_Effect : Monster_Effect // 기반이 되는 Monster_Effect 클래스 상속
{
    void Start()
    {
        m_l_EffectList = new List<GameObject>();
        
        GameObject Effect = Resources.Load("Prefab/Effect/Effect_Yellow_3") as GameObject; // 몬스터 놓아주기 이펙트
        m_l_EffectList.Add(Effect);
        
        Effect = Resources.Load("Prefab/Effect/Monster_Effect_Ents1") as GameObject; // 몬스터 공격 이펙트
        m_l_EffectList.Add(Effect);
    }

    // 몬스터 놓아주기 이펙트 연출 함수 - 부모 클래스인 Monster_Effect의 Effect_Goaway() 함수를 사용한다.
    // virtual public void Effect_Goaway(Vector3 pos) {ㆍㆍㆍ}
    // 몬스터 놓아주기 이펙트 연출 코루틴 - 부모 클래스인 Monster_Effect의 ProcessEffect_Goaway() 코루틴 사용한다.
    // virtual protected IEnumerator ProcessEffect_Goaway(Vector3 pos) {ㆍㆍㆍ}

    //
    // ※ "짙은 앤트"는 공격 시 지면 아래로 뿌리를 뻗어 일정 시간 경과 후 오브젝트(플레이어)의 위치로 뿌리를 돌출 시키는 공격을 한다.
    //    이때 돌출되는 뿌리는 이펙트로 연출된다.
    //
    
    // "짙은 앤트"의 공격 이펙트 연출 함수
    override public void Effect1(Vector3 pos, int damage, string attackname) // pos : 이펙트 위치, damage : 이펙트 데미지(충돌 시 피격), attackname : 피격 정보(이름)
    {
        StartCoroutine(ProcessEffect1(pos, damage, attackname)); // "짙은 앤트"의 공격 이펙트 연출 코루틴
    }

    // "짙은 앤트"의 공격 이펙트 연출 코루틴
    IEnumerator ProcessEffect1(Vector3 pos, int damage, string attackname) // pos : 이펙트 위치, damage : 이펙트 데미지(충돌 시 피격), attackname : 피격 정보(이름)
    {
        yield return new WaitForSeconds(0.1f);
        if (m_l_EffectList[1] != null)
        {
            GameObject efc = Instantiate(m_l_EffectList[1]); // "짙은 앤트"의 공격 이펙트 생성
            efc.transform.position = pos;
            efc.GetComponent<Mob_Effect_Ent1>().InitialSet(pos, damage, attackname); // "짙은 앤트"의 공격 이펙트 설정
        }
    }
}
