using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversation : MonoBehaviour
{
    public string m_sConversation_Title;
    public int m_nConversationCode;

    public List<string> m_sl_Conversation_Context;
    // 연계 대화 사전 조건 체크
    // 사전 필수 퀘스트 리스트
    public List<Quest> m_ql_Quest_Necessity_Clear;
    // 해당 퀘스트 클리어 기록이 존재하면 대화X
    public List<Quest> m_ql_Quest_Necessity_NonClear;
    public STATUS m_sStatus_Necessity_Up;
    public STATUS m_sStatus_Necessity_Down;
    public SOC m_sSoc_Necessity_Up;
    public SOC m_sSoc_Necessity_Down;

    public Conversation(string c_name, int c_code)
    {
        this.m_sConversation_Title = c_name;
        this.m_nConversationCode = c_code;
        m_sl_Conversation_Context = new List<string>();
        m_ql_Quest_Necessity_Clear = new List<Quest>();
        m_ql_Quest_Necessity_NonClear = new List<Quest>();

        m_sStatus_Necessity_Down = new STATUS(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        m_sStatus_Necessity_Up = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        m_sSoc_Necessity_Down = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        m_sSoc_Necessity_Up = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
    }

    public int Add_Conversation_Content(string str)
    {
        m_sl_Conversation_Context.Add(str);

        return m_sl_Conversation_Context.Count;
    }

    // 대화 사전 요구조건
    virtual public bool Check_Condition_Total()
    {
        if (Check_Condition_Connection() == true)
        {

        }
        else
            return false;
        if (Check_Condition_SOC() == true)
        {

        }
        else
            return false;
        if (Check_Condition_STATUS() == true)
        {

        }
        else
            return false;

        return true;
    }

    // 퀘스트 사전 요구조건_연계
    virtual public bool Check_Condition_Connection()
    {
        // 이 퀘스트를 수행하기위해 사전 퀘스트가 클리어되야 할때 조건 체크
        for (int i = 0; i < m_ql_Quest_Necessity_Clear.Count; i++)
        {
            if (m_ql_Quest_Necessity_Clear[i].m_bClear == false)
                return false;
            else
                continue;
        }

        // 이 퀘스트를 수행하기위해 사전 퀘스트가 클리어되지 말아야 할때 조건 체크
        for (int i = 0; i < m_ql_Quest_Necessity_NonClear.Count; i++)
        {
            if (m_ql_Quest_Necessity_NonClear[i].m_bClear == true)
                return false;
            else
                continue;
        }

        return true;
    }
    virtual public bool Check_Condition_SOC()
    {
        if (Player_Total.Instance.m_ps_Status.m_sSoc.CheckCondition_Min(m_sSoc_Necessity_Down) == true &&
            Player_Total.Instance.m_ps_Status.m_sSoc.CheckCondition_Max(m_sSoc_Necessity_Up) == true)
            return true;
        else
            return false;
    }
    virtual public bool Check_Condition_STATUS()
    {
        if (Player_Total.Instance.m_ps_Status.m_sStatus.CheckCondition_Min(m_sStatus_Necessity_Down) == true &&
            Player_Total.Instance.m_ps_Status.m_sStatus.CheckCondition_Max(m_sStatus_Necessity_Up) == true)
            return true;
        else
            return false;
    }
}
