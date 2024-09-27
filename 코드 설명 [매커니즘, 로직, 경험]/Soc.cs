using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 
// Free For All 에서 중요한 기획 요소 중 하나인 평판 시스템을 위한 데이터 타입
//

public struct SOC
{
    int m_nHonor;       // 명예
    int m_nHumanSoc;    // 인간 종족 평판
    int m_nAnimalSoc;   // 동물 종족 평판
    int m_nSlimeSoc;    // 슬라임 종족 평판
    int m_nSkeletonSoc; // 스켈레톤 종족 평판
    int m_nEntsSoc;     // 앤트 종족 평판
    int m_nDevilSoc;    // 마족 평판
    int m_nDragonSoc;   // 용족 평판
    int m_nShadowSoc;   // 어둠 평판

    // 생성자 - 기본형
    public SOC(int honor = 0, int human = 0, int animal = 0, int slime = 0, int skeleton = 0, int ents = 0, int devil = 0, int dragon = 0, int shadow = 0)
    {
        this.m_nHonor = honor;
        this.m_nHumanSoc = human;
        this.m_nAnimalSoc = animal;
        this.m_nSlimeSoc = slime;
        this.m_nSkeletonSoc = skeleton;
        this.m_nEntsSoc = ents;
        this.m_nDevilSoc = devil;
        this.m_nDragonSoc = dragon;
        this.m_nShadowSoc = shadow;
    }
    // 생성자 - 복사형
    public SOC(SOC soc) // soc : 복사할 평판 데이터
    {
        this.m_nHonor = soc.GetSOC_Honor();
        this.m_nHumanSoc = soc.GetSOC_Human();
        this.m_nAnimalSoc = soc.GetSOC_Animal();
        this.m_nSlimeSoc = soc.GetSOC_Slime();
        this.m_nSkeletonSoc = soc.GetSOC_Skeleton();
        this.m_nEntsSoc = soc.GetSOC_Ents();
        this.m_nDevilSoc = soc.GetSOC_Devil();
        this.m_nDragonSoc = soc.GetSOC_Dragon();
        this.m_nShadowSoc = soc.GetSOC_Shadow();
    }

    // 평판 +- 연산
    public void P_OperatorSOC_Honor(int value)
    {
        this.m_nHonor += value;
    }
    public void P_OperatorSOC_Human(int value)
    {
        this.m_nHumanSoc += value;
    }
    public void P_OperatorSOC_Animal(int value)
    {
        this.m_nAnimalSoc += value;
    }
    public void P_OperatorSOC_Slime(int value)
    {
        this.m_nSlimeSoc += value;
    }
    public void P_OperatorSOC_Skeleton(int value)
    {
        this.m_nSkeletonSoc += value;
    }
    public void P_OperatorSOC_Ents(int value)
    {
        this.m_nEntsSoc += value;
    }
    public void P_OperatorSOC_Devil(int value)
    {
        this.m_nDevilSoc += value;
    }
    public void P_OperatorSOC_Dragon(int value)
    {
        this.m_nDragonSoc += value;
    }
    public void P_OperatorSOC_Shadow(int value)
    {
        this.m_nShadowSoc += value;
    }
    public void P_OperatorSOC(SOC soc)
    {
        this.m_nHonor += soc.m_nHonor;
        this.m_nHumanSoc += soc.m_nHumanSoc;
        this.m_nAnimalSoc += soc.m_nAnimalSoc;
        this.m_nSlimeSoc += soc.m_nSlimeSoc;
        this.m_nSkeletonSoc += soc.m_nSkeletonSoc;
        this.m_nEntsSoc += soc.m_nEntsSoc;
        this.m_nDevilSoc += soc.m_nDevilSoc;
        this.m_nDragonSoc += soc.m_nDragonSoc;
        this.m_nShadowSoc += soc.m_nShadowSoc;
    }

    // SOC 연산자 오버로딩. 현재 최적화로 인해 사용하지 않는다.
    public static SOC operator+(SOC soc_origin, SOC soc_add)
    {
        return new SOC(soc_origin.GetSOC_Honor() + soc_add.GetSOC_Honor(),
                       soc_origin.GetSOC_Human() + soc_add.GetSOC_Human(),
                       soc_origin.GetSOC_Animal() + soc_add.GetSOC_Animal(),
                       soc_origin.GetSOC_Slime() + soc_add.GetSOC_Slime(),
                       soc_origin.GetSOC_Skeleton() + soc_add.GetSOC_Skeleton(),
                       soc_origin.GetSOC_Ents() + soc_add.GetSOC_Ents(),
                       soc_origin.GetSOC_Devil() + soc_add.GetSOC_Devil(),
                       soc_origin.GetSOC_Dragon() + soc_add.GetSOC_Dragon(),
                       soc_origin.GetSOC_Shadow() + soc_add.GetSOC_Shadow());
    }
    public static SOC operator -(SOC soc_origin, SOC soc_add)
    {
        return new SOC(soc_origin.GetSOC_Honor() - soc_add.GetSOC_Honor(),
                       soc_origin.GetSOC_Human() - soc_add.GetSOC_Human(),
                       soc_origin.GetSOC_Animal() - soc_add.GetSOC_Animal(),
                       soc_origin.GetSOC_Slime() - soc_add.GetSOC_Slime(),
                       soc_origin.GetSOC_Skeleton() - soc_add.GetSOC_Skeleton(),
                       soc_origin.GetSOC_Ents() - soc_add.GetSOC_Ents(),
                       soc_origin.GetSOC_Devil() - soc_add.GetSOC_Devil(),
                       soc_origin.GetSOC_Dragon() - soc_add.GetSOC_Dragon(),
                       soc_origin.GetSOC_Shadow() - soc_add.GetSOC_Shadow());
    }
    public static SOC operator *(SOC soc_origin, SOC soc_add)
    {
        return new SOC(soc_origin.GetSOC_Honor() * soc_add.GetSOC_Honor(),
                       soc_origin.GetSOC_Human() * soc_add.GetSOC_Human(),
                       soc_origin.GetSOC_Animal() * soc_add.GetSOC_Animal(),
                       soc_origin.GetSOC_Slime() * soc_add.GetSOC_Slime(),
                       soc_origin.GetSOC_Skeleton() * soc_add.GetSOC_Skeleton(),
                       soc_origin.GetSOC_Ents() * soc_add.GetSOC_Ents(),
                       soc_origin.GetSOC_Devil() * soc_add.GetSOC_Devil(),
                       soc_origin.GetSOC_Dragon() * soc_add.GetSOC_Dragon(),
                       soc_origin.GetSOC_Shadow() * soc_add.GetSOC_Shadow());
    }
    public static SOC operator /(SOC soc_origin, SOC soc_add)
    {
        return new SOC(soc_origin.GetSOC_Honor() / soc_add.GetSOC_Honor(),
                       soc_origin.GetSOC_Human() / soc_add.GetSOC_Human(),
                       soc_origin.GetSOC_Animal() / soc_add.GetSOC_Animal(),
                       soc_origin.GetSOC_Slime() / soc_add.GetSOC_Slime(),
                       soc_origin.GetSOC_Skeleton() / soc_add.GetSOC_Skeleton(),
                       soc_origin.GetSOC_Ents() / soc_add.GetSOC_Ents(),
                       soc_origin.GetSOC_Devil() / soc_add.GetSOC_Devil(),
                       soc_origin.GetSOC_Dragon() / soc_add.GetSOC_Dragon(),
                       soc_origin.GetSOC_Shadow() / soc_add.GetSOC_Shadow());
    }
    
    // 평판 설정
    public void SetSOC_Honor(int value)
    {
        this.m_nHonor = value;
    }
    public void SetSOC_Human(int value)
    {
        this.m_nHumanSoc = value;
    }
    public void SetSOC_Animal(int value)
    {
        this.m_nAnimalSoc = value;
    }
    public void SetSOC_Slime(int value)
    {
        this.m_nSlimeSoc = value;
    }
    public void SetSOC_Skeleton(int value)
    {
        this.m_nSkeletonSoc = value;
    }
    public void SetSOC_Ents(int value)
    {
        this.m_nEntsSoc = value;
    }
    public void SetSOC_Devil(int value)
    {
        this.m_nDevilSoc = value;
    }
    public void SetSOC_Dragon(int value)
    {
        this.m_nDragonSoc = value;
    }
    public void SetSOC_Shadow(int value)
    {
        this.m_nShadowSoc = value;
    }
    public void SetSOC(SOC soc)
    {
        this.m_nHonor = soc.m_nHonor;
        this.m_nHumanSoc = soc.m_nHumanSoc;
        this.m_nAnimalSoc = soc.m_nAnimalSoc;
        this.m_nSlimeSoc = soc.m_nSlimeSoc;
        this.m_nSkeletonSoc = soc.m_nSkeletonSoc;
        this.m_nEntsSoc = soc.m_nEntsSoc;
        this.m_nDevilSoc = soc.m_nDevilSoc;
        this.m_nDragonSoc = soc.m_nDragonSoc;
        this.m_nShadowSoc = soc.m_nShadowSoc;
    }
    public void SetSOC_Zero()
    {
        this.m_nHonor = 0;
        this.m_nHumanSoc = 0;
        this.m_nAnimalSoc = 0;
        this.m_nSlimeSoc = 0;
        this.m_nSkeletonSoc = 0;
        this.m_nEntsSoc = 0;
        this.m_nDevilSoc = 0;
        this.m_nDragonSoc = 0;
        this.m_nShadowSoc = 0;
    }

    // 평판 반환
    public int GetSOC_Honor()
    {
        return m_nHonor;
    }
    public int GetSOC_Human()
    {
        return m_nHumanSoc;
    }
    public int GetSOC_Animal()
    {
        return m_nAnimalSoc;
    }
    public int GetSOC_Slime()
    {
        return m_nSlimeSoc;
    }
    public int GetSOC_Skeleton()
    {
        return m_nSkeletonSoc;
    }
    public int GetSOC_Ents()
    {
        return m_nEntsSoc;
    }
    public int GetSOC_Devil()
    {
        return m_nDevilSoc;
    }
    public int GetSOC_Dragon()
    {
        return m_nDragonSoc;
    }
    public int GetSOC_Shadow()
    {
        return m_nShadowSoc;
    }
    public SOC GetSOC()
    {
        return this;
    }

    // 평판 조건 판단(하한)
    // return true : 평판 조건 충족 / return false : 평판 조건 미흡
    public bool CheckCondition_Min(SOC soc)
    {
        if (this.m_nHonor < soc.m_nHonor) return false;
        if (this.m_nHumanSoc < soc.m_nHumanSoc) return false;
        if (this.m_nAnimalSoc < soc.m_nAnimalSoc) return false;
        if (this.m_nSlimeSoc < soc.m_nSlimeSoc) return false;
        if (this.m_nSkeletonSoc < soc.m_nSkeletonSoc) return false;
        if (this.m_nEntsSoc < soc.m_nEntsSoc) return false;
        if (this.m_nDevilSoc < soc.m_nDevilSoc) return false;
        if (this.m_nDragonSoc < soc.m_nDragonSoc) return false;
        if (this.m_nShadowSoc < soc.m_nShadowSoc) return false;

        return true;
    }
    // 평판 조건 판단(상한)
    // return true : 평판 조건 충족 / return false : 평판 조건 미흡
    public bool CheckCondition_Max(SOC soc)
    {
        if (this.m_nHonor > soc.m_nHonor) return false;
        if (this.m_nHumanSoc > soc.m_nHumanSoc) return false;
        if (this.m_nAnimalSoc > soc.m_nAnimalSoc) return false;
        if (this.m_nSlimeSoc > soc.m_nSlimeSoc) return false;
        if (this.m_nSkeletonSoc > soc.m_nSkeletonSoc) return false;
        if (this.m_nEntsSoc > soc.m_nEntsSoc) return false;
        if (this.m_nDevilSoc > soc.m_nDevilSoc) return false;
        if (this.m_nDragonSoc > soc.m_nDragonSoc) return false;
        if (this.m_nShadowSoc > soc.m_nShadowSoc) return false;

        return true;
    }
    // 평판 조건 판단(하한 + 상한)
    // return true : 평판 조건 충족 / return false : 평판 조건 미흡
    public bool CheckCondition_MM_Honor(int minvalue, int maxvalue) // minvalue : 하한, maxvalue : 상한
    {
        if (this.m_nHonor < minvalue) return false;
        else
        {
            if (this.m_nHonor > maxvalue) return false;
            else return true;
        }
    }
    public bool CheckCondition_MM_Human(int minvalue, int maxvalue)
    {
        if (this.m_nHumanSoc < minvalue) return false;
        else
        {
            if (this.m_nHumanSoc > maxvalue) return false;
            else return true;
        }
    }
    public bool CheckCondition_MM_Animal(int minvalue, int maxvalue)
    {
        if (this.m_nAnimalSoc < minvalue) return false;
        else
        {
            if (this.m_nAnimalSoc > maxvalue) return false;
            else return true;
        }
    }
    public bool CheckCondition_MM_Slime(int minvalue, int maxvalue)
    {
        if (this.m_nSlimeSoc < minvalue) return false;
        else
        {
            if (this.m_nSlimeSoc > maxvalue) return false;
            else return true;
        }
    }
    public bool CheckCondition_MM_Skeleton(int minvalue, int maxvalue)
    {
        if (this.m_nSkeletonSoc < minvalue) return false;
        else
        {
            if (this.m_nSkeletonSoc > maxvalue) return false;
            else return true;
        }
    }
    public bool CheckCondition_MM_Ents(int minvalue, int maxvalue)
    {
        if (this.m_nEntsSoc < minvalue) return false;
        else
        {
            if (this.m_nEntsSoc > maxvalue) return false;
            else return true;
        }
    }
    public bool CheckCondition_MM_Devil(int minvalue, int maxvalue)
    {
        if (this.m_nDevilSoc < minvalue) return false;
        else
        {
            if (this.m_nDevilSoc > maxvalue) return false;
            else return true;
        }
    }
    public bool CheckCondition_MM_Dragon(int minvalue, int maxvalue)
    {
        if (this.m_nDragonSoc < minvalue) return false;
        else
        {
            if (this.m_nDragonSoc > maxvalue) return false;
            else return true;
        }
    }
    public bool CheckCondition_MM_Shadow(int minvalue, int maxvalue)
    {
        if (this.m_nShadowSoc < minvalue) return false;
        else
        {
            if (this.m_nShadowSoc > maxvalue) return false;
            else return true;
        }
    }

    // 평판 동일성 판단
    // return true : 두 평판이 동일한 수치를 가진다. / return false : 두 평판이 다른 수치를 가진다.
    public bool CheckIdentity(SOC soc)
    {
        if (this.m_nHonor != soc.m_nHonor)
            return false;
        if (this.m_nHumanSoc != soc.m_nHumanSoc)
            return false;
        if (this.m_nAnimalSoc != soc.m_nAnimalSoc)
            return false;
        if (this.m_nSlimeSoc != soc.m_nSlimeSoc)
            return false;
        if (this.m_nSkeletonSoc != soc.m_nSkeletonSoc)
            return false;
        if (this.m_nEntsSoc != soc.m_nEntsSoc)
            return false;
        if (this.m_nDevilSoc != soc.m_nDevilSoc)
            return false;
        if (this.m_nDragonSoc != soc.m_nDragonSoc)
            return false;
        if (this.m_nShadowSoc != soc.m_nShadowSoc)
            return false;

        return true;
    }

    // 평판 데이터 저장에 사용되는 함수
    public string GetSOC_Data()
    {
        string str = "";

        str = m_nHonor.ToString() + ", " + m_nHumanSoc.ToString() + ", " + m_nAnimalSoc.ToString() + ", " +
            m_nSlimeSoc.ToString() + ", " + m_nSkeletonSoc.ToString() + ", " + m_nEntsSoc.ToString() + ", " +
            m_nDevilSoc.ToString() + ", " + m_nDragonSoc.ToString() + ", " + m_nShadowSoc.ToString();

        return str;
    }
}
