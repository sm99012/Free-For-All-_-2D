using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// ※ 플레이어는 NPC와의 거래를 통해 아이템을 매매할 수 있다.
//    NPC마다 선호, 비선호 아이템이 다르기에 매매 가격 또한 다르다. 선호, 비선호 하지 않는 평범한 아이템의 경우 매매 상수를 적용한 가격으로 매매한다.
//

// 거래 등급 : { S1, S2, S3, S4, S5, S6, S7, S8, S9, S10 }
public enum E_STORE_LEVEL { S1, S2, S3, S4, S5, S6, S7, S8, S9, S10 }

public class NPC_Store : MonoBehaviour
{
    public string m_sStore_Name;        // 거래 제목
    public string m_sDescription;       // 거래 내용(설명)

    public int m_nStore_Code;           // 거래 고유코드
    
    public Sprite m_Sprite_NPC;         // 거래 주관 NPC 스프라이트(이미지)
    
    public E_STORE_LEVEL m_eStoreLevel; // 거래 등급

    // 거래 이용 사전 조건 관련 변수
    // 거래 이용 사전 조건 - 스탯(능력치) 상한ㆍ하한
    public STATUS m_sStatus_Necessity_Up;   // 스탯(능력치) 상한(플레이어의 스탯(능력치) 합계가 거래 이용 사전 조건(해당 조건)을 초과한 경우 제한)
    public STATUS m_sStatus_Necessity_Down; // 스탯(능력치) 하한(플레이어의 스탯(능력치) 합계가 거래 이용 사전 조건(해당 조건)에 미달한 경우 제한)
    // 거래 이용 사전 조건 - 스탯(평판) 상한ㆍ하한
    public SOC m_sSoc_Necessity_Up;         // 스탯(평판) 상한(플레이어의 스탯(평판) 합계가 거래 이용 사전 조건(해당 조건)을 초과한 경우 제한)
    public SOC m_sSoc_Necessity_Down;       // 스탯(평판) 하한(플레이어의 스탯(평판) 합계가 거래 이용 사전 조건(해당 조건)에 미달한 경우 제한)
    // 거래 이용 사전 조건 - 퀘스트 연관
    public List<Quest> m_ql_Quest_Necessity_Clear;      // 필수 완료 퀘스트(해당 리스트에 포함된 퀘스트가 완료되지 않은 경우 제한)
    public List<Quest> m_ql_Quest_Necessity_NonClear;   // 필수 미완료 퀘스트(해당 리스트에 포함된 퀘스트가 완료된 경우 제한)
    public List<Quest> m_ql_Quest_Necessity_Process;    // 필수 진행 퀘스트(해당 리스트에 포함된 퀘스트가 진행 중이지 않은 경우 제한)
    public List<Quest> m_ql_Quest_Necessity_NonProcess; // 필수 미진행 퀘스트(해당 리스트에 포함된 퀘스트가 진행 중인 경우 제한)

    // 거래 관련 변수
    // 판매(NPC가 판매) 품목 관련 변수
    public List<Item_Equip> m_List_Sale_Item_Equip;
    // 구매 확률. 0 ~ 10000
    public List<int> m_List_Sale_Item_Equip_Probability; // 
    // 최소, 최대 구매 수량.
    public List<int> m_List_Sale_Item_Equip_Count_Min;
    public List<int> m_List_Sale_Item_Equip_Count_Max;
    // 실 구매 목록.
    public List<Item_Equip> m_List_Sale_Item_Equip_Current;
    // 실 구매 수량.
    public List<int> m_List_Sale_Item_Equip_Count;
    // 구매 최소, 최대 가격.
    public List<int> m_List_Sale_Item_Equip_Price_Min;
    public List<int> m_List_Sale_Item_Equip_Price_Max;
    // 실 구매 가격.
    public List<int> m_List_Sale_Item_Equip_Price;


    // 구매 목록.
    public List<Item_Use> m_List_Sale_Item_Use;
    // 구매 확률. 0 ~ 10000
    public List<int> m_List_Sale_Item_Use_Probability;
    // 최소, 최대 구매 수량.
    public List<int> m_List_Sale_Item_Use_Count_Min;
    public List<int> m_List_Sale_Item_Use_Count_Max;
    // 실 구매 목록.
    public List<Item_Use> m_List_Sale_Item_Use_Current;
    // 실 구매 수량.
    public List<int> m_List_Sale_Item_Use_Count;
    // 최소, 최대 가격.
    public List<int> m_List_Sale_Item_Use_Price_Min;
    public List<int> m_List_Sale_Item_Use_Price_Max;
    // 실 구매 가격.
    public List<int> m_List_Sale_Item_Use_Price;


    // 구매 목록.
    public List<Item_Etc> m_List_Sale_Item_Etc;
    // 구매 확률. 0 ~ 10000
    public List<int> m_List_Sale_Item_Etc_Probability;
    // 최소, 최대 구매 수량.
    public List<int> m_List_Sale_Item_Etc_Count_Min;
    public List<int> m_List_Sale_Item_Etc_Count_Max;
    // 실 구매 목록.
    public List<Item_Etc> m_List_Sale_Item_Etc_Current;
    // 실 구매 수량.
    public List<int> m_List_Sale_Item_Etc_Count;
    // 최소, 최대 가격.
    public List<int> m_List_Sale_Item_Etc_Price_Min;
    public List<int> m_List_Sale_Item_Etc_Price_Max;
    // 실 구매 가격.
    public List<int> m_List_Sale_Item_Etc_Price;


    // 특별 판매 목록.
    public List<Item> m_List_Buy_Item;
    // 특별 판매 최소, 최대 가격.
    public List<int> m_List_Buy_Item_Price_Min;
    public List<int> m_List_Buy_Item_Price_Max;
    // 실 특별 판매 가격.
    public List<int> m_List_Buy_Item_Price;
    // 일반 판매 상수.
    // ㄴ 특별 판매 목록에 없는 아이템일 경우 '일반 판매 상수' 를 곱한 가격에 판매.
    // ㄴ 0 ~ 1
    // ㄴ 기본값: 1
    public float m_fBuy_Item_Equip_Value;
    public float m_fBuy_Item_Use_Value;
    public float m_fBuy_Item_Etc_Value;

    public NPC_Store(string name, int code, Sprite sprite, E_STORE_LEVEL sl)
    {
        this.m_sStore_Name = name;
        this.m_Sprite_NPC = sprite;
        this.m_eStoreLevel = sl;
        this.m_nStore_Code = code;

        m_sStatus_Necessity_Up = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        m_sStatus_Necessity_Down = new STATUS(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        m_sSoc_Necessity_Up = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        m_sSoc_Necessity_Down = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);

        m_ql_Quest_Necessity_Clear = new List<Quest>();
        m_ql_Quest_Necessity_NonClear = new List<Quest>();
        m_ql_Quest_Necessity_Process = new List<Quest>();
        m_ql_Quest_Necessity_NonProcess = new List<Quest>();

        m_List_Sale_Item_Equip = new List<Item_Equip>();
        m_List_Sale_Item_Equip_Probability = new List<int>();
        m_List_Sale_Item_Equip_Count_Min = new List<int>();
        m_List_Sale_Item_Equip_Count_Max = new List<int>();
        m_List_Sale_Item_Equip_Current = new List<Item_Equip>();
        m_List_Sale_Item_Equip_Count = new List<int>();
        m_List_Sale_Item_Equip_Price_Min = new List<int>();
        m_List_Sale_Item_Equip_Price_Max = new List<int>();
        m_List_Sale_Item_Equip_Price = new List<int>();

        m_List_Sale_Item_Use = new List<Item_Use>();
        m_List_Sale_Item_Use_Probability = new List<int>();
        m_List_Sale_Item_Use_Count_Min = new List<int>();
        m_List_Sale_Item_Use_Count_Max = new List<int>();
        m_List_Sale_Item_Use_Current = new List<Item_Use>();
        m_List_Sale_Item_Use_Count = new List<int>();
        m_List_Sale_Item_Use_Price_Min = new List<int>();
        m_List_Sale_Item_Use_Price_Max = new List<int>();
        m_List_Sale_Item_Use_Price = new List<int>();

        m_List_Sale_Item_Etc = new List<Item_Etc>();
        m_List_Sale_Item_Etc_Probability = new List<int>();
        m_List_Sale_Item_Etc_Count_Min = new List<int>();
        m_List_Sale_Item_Etc_Count_Max = new List<int>();
        m_List_Sale_Item_Etc_Current = new List<Item_Etc>();
        m_List_Sale_Item_Etc_Count = new List<int>();
        m_List_Sale_Item_Etc_Price_Min = new List<int>();
        m_List_Sale_Item_Etc_Price_Max = new List<int>();
        m_List_Sale_Item_Etc_Price = new List<int>();

        m_List_Buy_Item = new List<Item>();
        m_List_Buy_Item_Price_Min = new List<int>();
        m_List_Buy_Item_Price_Max = new List<int>();
        m_List_Buy_Item_Price = new List<int>();

        m_fBuy_Item_Equip_Value = 1.0f;
        m_fBuy_Item_Use_Value = 1.0f;
        m_fBuy_Item_Etc_Value = 1.0f;

        //Debug.Log(name);
    }

    // 일반 판매 상수 설정.
    public void Set_Buy_Item_Value(float fitemequip, float fitemuse, float fitemetc)
    {
        this.m_fBuy_Item_Equip_Value = fitemequip;
        this.m_fBuy_Item_Use_Value = fitemuse;
        this.m_fBuy_Item_Etc_Value = fitemetc;
    }

    // 구매 목록 삭제.
    public void Remove_Sale_Item(E_ITEMSLOT ei, int index)
    {
        switch(ei)
        {
            case E_ITEMSLOT.EQUIP:
                {
                    m_List_Sale_Item_Equip_Current.RemoveAt(index);
                    m_List_Sale_Item_Equip_Count.RemoveAt(index);
                    m_List_Sale_Item_Equip_Price.RemoveAt(index);

                } break;
            case E_ITEMSLOT.USE:
                {
                    m_List_Sale_Item_Use_Current.RemoveAt(index);
                    m_List_Sale_Item_Use_Count.RemoveAt(index);
                    m_List_Sale_Item_Use_Price.RemoveAt(index);
                } break;
            case E_ITEMSLOT.ETC:
                {
                    m_List_Sale_Item_Etc_Current.RemoveAt(index);
                    m_List_Sale_Item_Etc_Count.RemoveAt(index);
                    m_List_Sale_Item_Etc_Price.RemoveAt(index);
                } break;
        }
    }

    // 상점 이용 여부 판단.
    public bool Check_Condition_Store()
    {
        if (Check_Condition_Store_Quest() == false)
            return false;

        if (Check_Condition_Store_SOC() == false)
            return false;

        if (Check_Condition_Store_STATUS() == false)
            return false;

        return true;
    }
    bool Check_Condition_Store_Quest()
    {
        for (int i = 0; i < m_ql_Quest_Necessity_Clear.Count; i++)
        {
            if (m_ql_Quest_Necessity_Clear[i].m_bClear == false)
                return false;
            else
                continue;
        }

        for (int i = 0; i < m_ql_Quest_Necessity_NonClear.Count; i++)
        {
            if (m_ql_Quest_Necessity_NonClear[i].m_bClear == true)
                return false;
            else
                continue;
        }

        for (int i = 0; i < m_ql_Quest_Necessity_Process.Count; i++)
        {
            if (m_ql_Quest_Necessity_Process[i].m_bProcess == false)
                return false;
            else
                continue;
        }

        for (int i = 0; i < m_ql_Quest_Necessity_NonProcess.Count; i++)
        {
            if (m_ql_Quest_Necessity_NonProcess[i].m_bProcess == true)
                return false;
            else
                continue;
        }

        return true;
    }
    bool Check_Condition_Store_SOC()
    {
        if (Player_Total.Instance.m_ps_Status.m_sSoc.CheckCondition_Min(m_sSoc_Necessity_Down) == true &&
            Player_Total.Instance.m_ps_Status.m_sSoc.CheckCondition_Max(m_sSoc_Necessity_Up) == true)
            return true;
        else
            return false;
    }
    bool Check_Condition_Store_STATUS()
    {
        if (Player_Total.Instance.m_ps_Status.m_sStatus.CheckCondition_Min(m_sStatus_Necessity_Down) == true &&
            Player_Total.Instance.m_ps_Status.m_sStatus.CheckCondition_Max(m_sStatus_Necessity_Up) == true)
            return true;
        else
            return false;
    }

    // 상점 초기화.
    public void Initialization()
    {
        m_List_Sale_Item_Equip_Current.Clear();
        m_List_Sale_Item_Equip_Count.Clear();
        m_List_Sale_Item_Equip_Price.Clear();

        m_List_Sale_Item_Use_Current.Clear();
        m_List_Sale_Item_Use_Count.Clear();
        m_List_Sale_Item_Use_Price.Clear();

        m_List_Sale_Item_Etc_Current.Clear();
        m_List_Sale_Item_Etc_Count.Clear();
        m_List_Sale_Item_Etc_Price.Clear();

        m_List_Buy_Item_Price.Clear();

        for (int i = 0; i < m_List_Sale_Item_Equip.Count; i++)
        {
            if (Random.Range(0, 10000) < m_List_Sale_Item_Equip_Probability[i])
            {
                m_List_Sale_Item_Equip_Current.Add(m_List_Sale_Item_Equip[i]);
                m_List_Sale_Item_Equip_Count.Add(Random.Range(m_List_Sale_Item_Equip_Count_Min[i], m_List_Sale_Item_Equip_Count_Max[i]));
                m_List_Sale_Item_Equip_Price.Add(Random.Range(m_List_Sale_Item_Equip_Price_Min[i], m_List_Sale_Item_Equip_Price_Max[i]));
            }
        }

        for (int i = 0; i < m_List_Sale_Item_Use.Count; i++)
        {
            if (Random.Range(0, 10000) < m_List_Sale_Item_Use_Probability[i])
            {
                m_List_Sale_Item_Use_Current.Add(m_List_Sale_Item_Use[i]);
                m_List_Sale_Item_Use_Count.Add(Random.Range(m_List_Sale_Item_Use_Count_Min[i], m_List_Sale_Item_Use_Count_Max[i]));
                m_List_Sale_Item_Use_Price.Add(Random.Range(m_List_Sale_Item_Use_Price_Min[i], m_List_Sale_Item_Use_Price_Max[i]));
            }
        }

        for (int i = 0; i < m_List_Sale_Item_Etc.Count; i++)
        {
            if (Random.Range(0, 10000) < m_List_Sale_Item_Etc_Probability[i])
            {
                m_List_Sale_Item_Etc_Current.Add(m_List_Sale_Item_Etc[i]);
                m_List_Sale_Item_Etc_Count.Add(Random.Range(m_List_Sale_Item_Etc_Count_Min[i], m_List_Sale_Item_Etc_Count_Max[i]));
                m_List_Sale_Item_Etc_Price.Add(Random.Range(m_List_Sale_Item_Etc_Price_Min[i], m_List_Sale_Item_Etc_Price_Max[i]));
            }
        }

        for (int i = 0; i < m_List_Buy_Item.Count; i++)
        {
            m_List_Buy_Item_Price.Add(Random.Range(m_List_Buy_Item_Price_Min[i], m_List_Buy_Item_Price_Max[i]));
        }
    }
}
