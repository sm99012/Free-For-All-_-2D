using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// ※ 유저는 NPC와의 거래를 통해 아이템을 매매할 수 있다.
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
    // 판매(NPC가 판매) 품목 관련 변수 - 장비아이템
    public List<Item_Equip> m_List_Sale_Item_Equip;         // 장비아이템 판매 목록
    public List<int> m_List_Sale_Item_Equip_Probability;    // 장비아이템 판매 확률(1 ~ 10000)
    public List<int> m_List_Sale_Item_Equip_Count_Min;      // 장비아이템 판매 최소 수량
    public List<int> m_List_Sale_Item_Equip_Count_Max;      // 장비아이템 판매 최대 수량
    public List<int> m_List_Sale_Item_Equip_Price_Min;      // 장비아이템 판매 하한(최소 가격)
    public List<int> m_List_Sale_Item_Equip_Price_Max;      // 장비아이템 판매 상한(최대 가격)
    
    public List<Item_Equip> m_List_Sale_Item_Equip_Current; // 장비아이템 실제 판매 목록
    public List<int> m_List_Sale_Item_Equip_Count;          // 장비아이템 실제 판매 수량
    public List<int> m_List_Sale_Item_Equip_Price;          // 장비아이템 실제 판매 가격

    // 판매(NPC가 판매) 품목 관련 변수 - 소비아이템
    public List<Item_Use> m_List_Sale_Item_Use;         // 소비아이템 판매 목록
    public List<int> m_List_Sale_Item_Use_Probability;  // 소비아이템 판매 확률(1 ~ 10000)
    public List<int> m_List_Sale_Item_Use_Count_Min;    // 소비아이템 판매 최소 수량
    public List<int> m_List_Sale_Item_Use_Count_Max;    // 소비아이템 판매 최대 수량
    public List<int> m_List_Sale_Item_Use_Price_Min;    // 소비아이템 판매 하한(최소 가격)
    public List<int> m_List_Sale_Item_Use_Price_Max;    // 소비아이템 판매 상한(최대 가격)
    
    public List<Item_Use> m_List_Sale_Item_Use_Current; // 소비아이템 실제 판매 목록
    public List<int> m_List_Sale_Item_Use_Count;        // 소비아이템 실제 판매 수량
    public List<int> m_List_Sale_Item_Use_Price;        // 소비아이템 실제 판매 가격

    // 판매(NPC가 판매, 유저가 구매) 품목 관련 변수 - 기타아이템
    public List<Item_Etc> m_List_Sale_Item_Etc;         // 기타아이템 판매 목록
    public List<int> m_List_Sale_Item_Etc_Probability;  // 기타아이템 판매 확률(1 ~ 10000)
    public List<int> m_List_Sale_Item_Etc_Count_Min;    // 기타아이템 판매 최소 수량
    public List<int> m_List_Sale_Item_Etc_Count_Max;    // 기타아이템 판매 최대 수량
    public List<int> m_List_Sale_Item_Etc_Price_Min;    // 기타아이템 판매 하한(최소 가격)
    public List<int> m_List_Sale_Item_Etc_Price_Max;    // 기타아이템 판매 상한(최대 가격)

    public List<Item_Etc> m_List_Sale_Item_Etc_Current; // 기타아이템 실제 판매 목록
    public List<int> m_List_Sale_Item_Etc_Count;        // 기타아이템 실제 판매 수량
    public List<int> m_List_Sale_Item_Etc_Price;        // 기타아이템 실제 판매 가격

    // 구매(NPC가 구매, 유저가 판매) 품목 관련 변수 - 별도의 아이템 구매가 지정
    public List<Item> m_List_Buy_Item;          // 아이템 구매 목록
    public List<int> m_List_Buy_Item_Price_Min; // 아이템 구매 하한(최소 가격)
    public List<int> m_List_Buy_Item_Price_Max; // 아이템 구매 상한(최대 가격)

    public List<int> m_List_Buy_Item_Price;     // 아이템 실제 구매 가격

    // 아이템 구매 상수 : 별도의 아이템 구매 목록에 없는 아이템의 경우 아이템 원가격에 '아이템 구매 상수'를 곱한 가격에 구매한다.
    public float m_fBuy_Item_Equip_Value; // 장비아이템 구매 상수(0.0 ~ 1.0)
    public float m_fBuy_Item_Use_Value;   // 소비아이템 구매 상수(0.0 ~ 1.0)
    public float m_fBuy_Item_Etc_Value;   // 기타아이템 구매 상수(0.0 ~ 1.0)
    
    // 거래 데이터를 생성하는 생성자
    public NPC_Store(string name, int code, Sprite sprite, E_STORE_LEVEL sl)
    {
        this.m_sStore_Name = name;
        this.m_nStore_Code = code;
        this.m_Sprite_NPC = sprite;
        this.m_eStoreLevel = sl;
    
        m_sStatus_Necessity_Up = new STATUS(true);
        m_sStatus_Necessity_Down = new STATUS(false);
        m_sSoc_Necessity_Up = new SOC(true);
        m_sSoc_Necessity_Down = new SOC(false);
        
        m_ql_Quest_Necessity_Clear = new List<Quest>();
        m_ql_Quest_Necessity_NonClear = new List<Quest>();
        m_ql_Quest_Necessity_Process = new List<Quest>();
        m_ql_Quest_Necessity_NonProcess = new List<Quest>();

        m_List_Sale_Item_Equip = new List<Item_Equip>();
        m_List_Sale_Item_Equip_Probability = new List<int>();
        m_List_Sale_Item_Equip_Count_Min = new List<int>();
        m_List_Sale_Item_Equip_Count_Max = new List<int>();
        m_List_Sale_Item_Equip_Price_Min = new List<int>();
        m_List_Sale_Item_Equip_Price_Max = new List<int>();

        m_List_Sale_Item_Equip_Current = new List<Item_Equip>();
        m_List_Sale_Item_Equip_Count = new List<int>();
        m_List_Sale_Item_Equip_Price = new List<int>();

        m_List_Sale_Item_Use = new List<Item_Use>();
        m_List_Sale_Item_Use_Probability = new List<int>();
        m_List_Sale_Item_Use_Count_Min = new List<int>();
        m_List_Sale_Item_Use_Count_Max = new List<int>();
        m_List_Sale_Item_Use_Price_Min = new List<int>();
        m_List_Sale_Item_Use_Price_Max = new List<int>();
        
        m_List_Sale_Item_Use_Current = new List<Item_Use>();
        m_List_Sale_Item_Use_Count = new List<int>();
        m_List_Sale_Item_Use_Price = new List<int>();

        m_List_Sale_Item_Etc = new List<Item_Etc>();
        m_List_Sale_Item_Etc_Probability = new List<int>();
        m_List_Sale_Item_Etc_Count_Min = new List<int>();
        m_List_Sale_Item_Etc_Count_Max = new List<int>();
        m_List_Sale_Item_Etc_Price_Min = new List<int>();
        m_List_Sale_Item_Etc_Price_Max = new List<int>();
        
        m_List_Sale_Item_Etc_Current = new List<Item_Etc>();
        m_List_Sale_Item_Etc_Count = new List<int>();
        m_List_Sale_Item_Etc_Price = new List<int>();

        m_List_Buy_Item = new List<Item>();
        m_List_Buy_Item_Price_Min = new List<int>();
        m_List_Buy_Item_Price_Max = new List<int>();
        
        m_List_Buy_Item_Price = new List<int>();

        m_fBuy_Item_Equip_Value = 1.0f;
        m_fBuy_Item_Use_Value = 1.0f;
        m_fBuy_Item_Etc_Value = 1.0f;
    }

    // 아이템 구매 상수 설정 함수
    public void Set_Buy_Item_Value(float fitemequip, float fitemuse, float fitemetc)
    {
        this.m_fBuy_Item_Equip_Value = fitemequip; // 장비아이템 구매 상수 설정
        this.m_fBuy_Item_Use_Value = fitemuse;     // 소비아이템 구매 상수 설정
        this.m_fBuy_Item_Etc_Value = fitemetc;     // 기타아이템 구매 상수 설정
    }

    // 아이템 실제 판매 데이터(아이템 실제 판매 목록, 아이템 실제 판매 수량, 아이템 실제 판매 가격) 삭제
    public void Remove_Sale_Item(E_ITEMSLOT ei, int index) // ei : 아이템 타입, index : 삭제할 아이템 순번
    {
        switch(ei)
        {
            case E_ITEMSLOT.EQUIP:
                {
                    m_List_Sale_Item_Equip_Current.RemoveAt(index); // 장비아이템 실제 판매 목록에서 해당 순번 삭제
                    m_List_Sale_Item_Equip_Count.RemoveAt(index);   // 장비아이템 실제 판매 수량에서 해당 순번 삭제
                    m_List_Sale_Item_Equip_Price.RemoveAt(index);   // 장비아이템 실제 판매 가격에서 해당 순번 삭제

                } break;
            case E_ITEMSLOT.USE:
                {
                    m_List_Sale_Item_Use_Current.RemoveAt(index); // 소비아이템 실제 판매 목록에서 해당 순번 삭제
                    m_List_Sale_Item_Use_Count.RemoveAt(index);   // 소비아이템 실제 판매 수량에서 해당 순번 삭제
                    m_List_Sale_Item_Use_Price.RemoveAt(index);   // 소비아이템 실제 판매 가격에서 해당 순번 삭제
                } break;
            case E_ITEMSLOT.ETC:
                {
                    m_List_Sale_Item_Etc_Current.RemoveAt(index); // 기타아이템 실제 판매 목록에서 해당 순번 삭제
                    m_List_Sale_Item_Etc_Count.RemoveAt(index);   // 기타아이템 실제 판매 수량에서 해당 순번 삭제
                    m_List_Sale_Item_Etc_Price.RemoveAt(index);   // 기타아이템 실제 판매 가격에서 해당 순번 삭제
                } break;
        }
    }

    // 거래 이용 사전 조건 판단 함수
    // return true : 거래 이용 가능 / return false : 거래 이용 불가능
    public bool Check_Condition_Store()
    {
        if (Check_Condition_Store_Quest() == false) // 거래 이용 사전 조건 판단 함수 - 퀘스트 연관
            return false;
        if (Check_Condition_Store_STATUS() == false) // 거래 이용 사전 조건 판단 함수 - 스탯(능력치) 상한ㆍ하한
            return false;
        if (Check_Condition_Store_SOC() == false) // 거래 이용 사전 조건 판단 함수 - 스탯(평판) 상한ㆍ하한
            return false;

        return true;
    }
    // 거래 이용 사전 조건 판단 함수 - 퀘스트 연관
    // return true : 조건 충족 / return false : 조건 미흡
    bool Check_Condition_Store_Quest()
    {
        // 거래 이용 사전 조건 판단 - 필수 완료 퀘스트(해당 리스트에 포함된 퀘스트가 완료되지 않은 경우 제한)
        for (int i = 0; i < m_ql_Quest_Necessity_Clear.Count; i++)
        {
            if (m_ql_Quest_Necessity_Clear[i].m_bClear == false)
                return false;
            else
                continue;
        }
        // 거래 이용 사전 조건 판단 - 필수 미완료 퀘스트(해당 리스트에 포함된 퀘스트가 완료된 경우 제한)
        for (int i = 0; i < m_ql_Quest_Necessity_NonClear.Count; i++)
        {
            if (m_ql_Quest_Necessity_NonClear[i].m_bClear == true)
                return false;
            else
                continue;
        }
        // 거래 이용 사전 조건 판단 - 필수 진행 퀘스트(해당 리스트에 포함된 퀘스트가 진행 중이지 않은 경우 제한)
        for (int i = 0; i < m_ql_Quest_Necessity_Process.Count; i++)
        {
            if (m_ql_Quest_Necessity_Process[i].m_bProcess == false)
                return false;
            else
                continue;
        }
        // 거래 이용 사전 조건 판단 - 필수 미진행 퀘스트(해당 리스트에 포함된 퀘스트가 진행 중인 경우 제한)
        for (int i = 0; i < m_ql_Quest_Necessity_NonProcess.Count; i++)
        {
            if (m_ql_Quest_Necessity_NonProcess[i].m_bProcess == true)
                return false;
            else
                continue;
        }

        return true;
    }
    // 거래 이용 사전 조건 판단 함수 - 스탯(능력치) 상한ㆍ하한
    // return true : 조건 충족 / return false : 조건 미흡
    bool Check_Condition_Store_STATUS()
    {
        return (Player_Total.Instance.m_ps_Status.m_sStatus.CheckCondition_Min(m_sStatus_Necessity_Down) == true && 
                Player_Total.Instance.m_ps_Status.m_sStatus.CheckCondition_Max(m_sStatus_Necessity_Up) == true);
    }
    // 거래 이용 사전 조건 판단 함수 - 스탯(평판) 상한ㆍ하한
    // return true : 조건 충족 / return false : 조건 미흡
    bool Check_Condition_Store_SOC()
    {
        return (Player_Total.Instance.m_ps_Status.m_sSoc.CheckCondition_Min(m_sSoc_Necessity_Down) == true && 
                Player_Total.Instance.m_ps_Status.m_sSoc.CheckCondition_Max(m_sSoc_Necessity_Up) == true);
    }

    // 거래 갱신(초기화) 함수. 판매 아이템 종류, 수량, 매매 가격 등이 변경된다.
    public void Initialization()
    {
        // 아이템 실제 판매 데이터 초기화 - 장비아이템
        m_List_Sale_Item_Equip_Current.Clear();
        m_List_Sale_Item_Equip_Count.Clear();
        m_List_Sale_Item_Equip_Price.Clear();
        // 아이템 실제 판매 데이터 초기화 - 소비아이템
        m_List_Sale_Item_Use_Current.Clear();
        m_List_Sale_Item_Use_Count.Clear();
        m_List_Sale_Item_Use_Price.Clear();
        // 아이템 실제 판매 데이터 초기화 - 기타아이템
        m_List_Sale_Item_Etc_Current.Clear();
        m_List_Sale_Item_Etc_Count.Clear();
        m_List_Sale_Item_Etc_Price.Clear();

        // 아이템 실제 구매 가격 초기화 - 장비아이템, 소비아이템, 기타아이템
        m_List_Buy_Item_Price.Clear();

        // 아이템 실제 판매 데이터 설정(추가) - 장비아이템
        for (int i = 0; i < m_List_Sale_Item_Equip.Count; i++)
        {
            if (Random.Range(0, 10000) < m_List_Sale_Item_Equip_Probability[i]) // 장비아이템 판매 여부 판단
            {
                m_List_Sale_Item_Equip_Current.Add(m_List_Sale_Item_Equip[i]);
                m_List_Sale_Item_Equip_Count.Add(Random.Range(m_List_Sale_Item_Equip_Count_Min[i], m_List_Sale_Item_Equip_Count_Max[i])); // 장비아이템 실제 판매 수량 설정
                m_List_Sale_Item_Equip_Price.Add(Random.Range(m_List_Sale_Item_Equip_Price_Min[i], m_List_Sale_Item_Equip_Price_Max[i])); // 장비아이템 실제 판매 가격 설정
            }
        }
        // 아이템 실제 판매 데이터 설정(추가) - 소비아이템
        for (int i = 0; i < m_List_Sale_Item_Use.Count; i++)
        {
            if (Random.Range(0, 10000) < m_List_Sale_Item_Use_Probability[i]) // 소비아이템 판매 여부 판단
            {
                m_List_Sale_Item_Use_Current.Add(m_List_Sale_Item_Use[i]);
                m_List_Sale_Item_Use_Count.Add(Random.Range(m_List_Sale_Item_Use_Count_Min[i], m_List_Sale_Item_Use_Count_Max[i])); // 소비아이템 실제 판매 수량 설정
                m_List_Sale_Item_Use_Price.Add(Random.Range(m_List_Sale_Item_Use_Price_Min[i], m_List_Sale_Item_Use_Price_Max[i])); // 소비아이템 실제 판매 가격 설정
            }
        }
        // 아이템 실제 판매 데이터 설정(추가) - 기타아이템
        for (int i = 0; i < m_List_Sale_Item_Etc.Count; i++)
        {
            if (Random.Range(0, 10000) < m_List_Sale_Item_Etc_Probability[i]) // 기타아이템 판매 여부 판단
            {
                m_List_Sale_Item_Etc_Current.Add(m_List_Sale_Item_Etc[i]);
                m_List_Sale_Item_Etc_Count.Add(Random.Range(m_List_Sale_Item_Etc_Count_Min[i], m_List_Sale_Item_Etc_Count_Max[i])); // 기타아이템 실제 판매 수량 설정
                m_List_Sale_Item_Etc_Price.Add(Random.Range(m_List_Sale_Item_Etc_Price_Min[i], m_List_Sale_Item_Etc_Price_Max[i])); // 기타아이템 실제 판매 가격 설정
            }
        }
        
        // 아이템 실제 구매 가격 설정(추가) - 장비아이템, 소비아이템, 기타아이템
        for (int i = 0; i < m_List_Buy_Item.Count; i++)
        {
            m_List_Buy_Item_Price.Add(Random.Range(m_List_Buy_Item_Price_Min[i], m_List_Buy_Item_Price_Max[i])); // 아이템 실제 구매 가격 설정
        }
    }
}
