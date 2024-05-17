using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public string m_sTriggerName;
    public int m_nTriggerCode;

    // 트리거 작동 대사 유무.
    public bool m_bTrigger_Progress_Context;
    public List<string> m_sl_Trigger_Progress_Context;

    // 트리거 작동 사전 조건.
    //해당 퀘스트 클리어 기록이 존재해야 트리거 작동.
    public List<Quest> m_ql_Quest_Necessity_Clear;
    // 해당 퀘스트 클리어 기록이 존재하면 트리거 미작동.
    public List<Quest> m_ql_Quest_Necessity_NonClear;
    // 해당 퀘스트를 진행중이면 트리거 작동.
    public List<Quest> m_ql_Quest_Necessity_Process;
    // 해당 퀘스트를 진행중이면 트리거 미작동.
    public List<Quest> m_ql_Quest_Necessity_NonProcess;
    // 능력치에 관련된 제약 조건.
    public STATUS m_sStatus_Necessity_Up;
    public STATUS m_sStatus_Necessity_Down;
    public SOC m_sSoc_Necessity_Up;
    public SOC m_sSoc_Necessity_Down;

    private void Start()
    {
        InitialSet();
    }

    protected virtual void InitialSet()
    {
        m_sTriggerName = "";
        m_nTriggerCode = -1;

        m_bTrigger_Progress_Context = false;
        m_sl_Trigger_Progress_Context = new List<string>();

        m_ql_Quest_Necessity_Clear = new List<Quest>();
        m_ql_Quest_Necessity_NonClear = new List<Quest>();
        m_ql_Quest_Necessity_Process = new List<Quest>();
        m_ql_Quest_Necessity_NonProcess = new List<Quest>();

        m_sStatus_Necessity_Up = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        m_sStatus_Necessity_Down = new STATUS(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        m_sSoc_Necessity_Up = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        m_sSoc_Necessity_Down = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);

        InitialSet_Etc();
    }

    // 트리거 작동 대사, 제약 조건 설정.
    protected virtual void InitialSet_Etc()
    {

    }

    // 트리거 작동 대사 스크립트 설정.
    public void AddTrigger_Progress_Context(string context)
    {
        m_bTrigger_Progress_Context = true;
        m_sl_Trigger_Progress_Context.Add(context);
    }

    // 트리거 작동 사전 조건 체크.
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

    // 트리거 작동 사전 조건 체크 _ 퀘스트
    virtual public bool Check_Condition_Connection()
    {
        // 이 트리거를 수행하기위해 사전 퀘스트가 클리어되야 할때 조건 체크
        for (int i = 0; i < m_ql_Quest_Necessity_Clear.Count; i++)
        {
            if (m_ql_Quest_Necessity_Clear[i].m_bClear == false)
                return false;
            else
                continue;
        }

        // 이 트리거를 수행하기위해 사전 퀘스트가 클리어되지 말아야 할때 조건 체크
        for (int i = 0; i < m_ql_Quest_Necessity_NonClear.Count; i++)
        {
            if (m_ql_Quest_Necessity_NonClear[i].m_bClear == true)
                return false;
            else
                continue;
        }

        // 이 트리거를 수행하기위해 특정 퀘스트가 진행중이어야할때
        for (int i = 0; i < m_ql_Quest_Necessity_Process.Count; i++)
        {
            if (m_ql_Quest_Necessity_Process[i].m_bProcess == false)
                return false;
            else
                continue;
        }

        // 이 트리거를 수행하기위해 특정 퀘스트가 진행중이지 않아야 할때
        for (int i = 0; i < m_ql_Quest_Necessity_NonProcess.Count; i++)
        {
            if (m_ql_Quest_Necessity_NonProcess[i].m_bProcess == true)
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
