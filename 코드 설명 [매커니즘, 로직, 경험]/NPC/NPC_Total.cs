using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_NPC_QC_STATE { QUEST_ACCEPT_POSSIBLE, QUEST_CLEAR_POSSIBLE, QUEST_ACCEPT_N_CLEAR_POSSIBLE, QUEST_PROCESS, QUEST_PROCESS_, NULL }
public class NPC_Total : MonoBehaviour
{
    public string m_sNPCName;
    public int m_nNPCCode;
    public Sprite m_Sprite_NPC;

    public List <NPC_Store> m_List_NPC_Store;
    public List<int> m_List_NPC_Store_Code;
    public bool m_bNPC_Store = false;

    public List <Conversation> m_cl_Conversation;

    public List<Quest_KILL_MONSTER> m_ql_QuestList_KILL_MONSTER;
    public List<Quest_KILL_TYPE> m_ql_QuestList_KILL_TYPE;
    public List<Quest_GOAWAY_MONSTER> m_ql_QuestList_GOAWAY_MONSTER;
    public List<Quest_GOAWAY_TYPE> m_ql_QuestList_GOAWAY_TYPE;
    public List<Quest_COLLECT> m_ql_QuestList_COLLECT;
    public List<Quest_CONVERSATION> m_ql_QuestList_CONVERSATION;
    public List<Quest_ROLL> m_ql_QuestList_ROLL;
    public List<Quest_ELIMINATE_MONSTER> m_ql_QuestList_ELIMINATE_MONSTER;
    public List<Quest_ELIMINATE_TYPE> m_ql_QuestList_ELIMINATE_TYPE;


    public GameObject m_gNPC_Icon;

    // Quest 수락 가능. Icon: ?
    public GameObject m_gIcon_Quest_Accept_Possible;
    // Quest 클리어 가능. Icon: !
    public GameObject m_gIcon_Quest_Clear_Possible;
    // Quest 수락 가능 + Quest 클리어 가능. Icon: ?!
    public GameObject m_gIcon_Quest_Accept_N_Clear_Possible;
    // Quest 진행중. Icon: . . .
    public GameObject m_gIcon_Quest_Process;
    public GameObject m_gIcon_Quest_Process_;
    // Store.
    public GameObject m_gIcon_Store;

    [SerializeField] protected List<int> m_nList_Accept;
    [SerializeField] protected List<int> m_nList_Clear;
    [SerializeField] protected List<int> m_nList_Process;


    [Space(20)]
    [SerializeField] protected E_NPC_QC_STATE m_eNPC_QC_STATE;

    virtual public void InitialSet()
    {
        InitialSet_Icon();
    }

    virtual public void InitialSet_Store()
    {

    }

    public void Update_Store()
    {
        for (int i = 0; i < m_List_NPC_Store.Count; i++)
        {
            m_List_NPC_Store[i].Initialization();
        }
    }

    // Quest, Conversation 현황 알려주는 Icon.
    // 필수.
    protected void InitialSet_Icon()
    {
        m_gIcon_Quest_Accept_Possible = m_gNPC_Icon.transform.Find("QC_P").gameObject;
        m_gIcon_Quest_Clear_Possible = m_gNPC_Icon.transform.Find("Q_C").gameObject;
        m_gIcon_Quest_Accept_N_Clear_Possible = m_gNPC_Icon.transform.Find("QC_PC").gameObject;
        m_gIcon_Quest_Process = m_gNPC_Icon.transform.Find("Q_P").gameObject;
        m_gIcon_Quest_Process_ = m_gNPC_Icon.transform.Find("Q_P_").gameObject;
        m_gIcon_Store = m_gNPC_Icon.transform.Find("STORE").gameObject;

        m_eNPC_QC_STATE = E_NPC_QC_STATE.NULL;

        m_gIcon_Quest_Accept_Possible.SetActive(false);
        m_gIcon_Quest_Clear_Possible.SetActive(false);
        m_gIcon_Quest_Accept_N_Clear_Possible.SetActive(false);
        m_gIcon_Quest_Process.SetActive(false);
        m_gIcon_Quest_Process_.SetActive(false);
        m_gIcon_Store.SetActive(false);

        m_nList_Accept = new List<int>();
        m_nList_Clear = new List<int>();
        m_nList_Process = new List<int>();

        StartCoroutine(ProcessStart());
    }

    protected IEnumerator ProcessStart()
    {
        yield return new WaitForSeconds(1f);
        UpdateIcon();
    }

    // NPC Quest 현황 Icon 갱신.
    virtual public int UpdateIcon()
    {
        if (this != null)
        {
            // m_bIcon_Quest_Accept_Possible 체크.
            // m_bIcon_Quest_Clear_Possible 체크.
            // m_bIcon_Quest_Process 체크.
            m_nList_Accept.Clear();
            m_nList_Clear.Clear();
            m_nList_Process.Clear();

            for (int i = 0; i < m_ql_QuestList_KILL_MONSTER.Count; i++)
            {
                if (m_ql_QuestList_KILL_MONSTER[i].Check_Condition_Total() == true)
                {
                    if (m_ql_QuestList_KILL_MONSTER[i].m_bClear == false)
                    {
                        // Quest 수락 가능.
                        if (m_ql_QuestList_KILL_MONSTER[i].m_bProcess == false && m_ql_QuestList_KILL_MONSTER[i].m_bCondition == false)
                        {
                            if (QuestManager.Instance.GetQuest_KILL_MONSTER(m_ql_QuestList_KILL_MONSTER[i].m_nQuest_Code).m_nNPC == m_nNPCCode)
                            {
                                m_nList_Accept.Add(m_ql_QuestList_KILL_MONSTER[i].m_nQuest_Code);
                            }
                        }
                        // Quest 클리어 가능.
                        if (m_ql_QuestList_KILL_MONSTER[i].m_bCondition == true)
                        {
                            if (QuestManager.Instance.GetQuest_KILL_MONSTER(m_ql_QuestList_KILL_MONSTER[i].m_nQuest_Code).m_nNPC_Clear == m_nNPCCode)
                            {
                                m_nList_Clear.Add(m_ql_QuestList_KILL_MONSTER[i].m_nQuest_Code);
                            }
                            else if (QuestManager.Instance.GetQuest_KILL_MONSTER(m_ql_QuestList_KILL_MONSTER[i].m_nQuest_Code).m_nNPC == m_nNPCCode)
                            {
                                m_nList_Process.Add(m_ql_QuestList_KILL_MONSTER[i].m_nQuest_Code);
                            }
                        }
                        // Quest 진행중.
                        if (m_ql_QuestList_KILL_MONSTER[i].m_bCondition == false && m_ql_QuestList_KILL_MONSTER[i].m_bProcess == true)
                        {
                            m_nList_Process.Add(m_ql_QuestList_KILL_MONSTER[i].m_nQuest_Code);
                        }
                    }
                }
            }
            for (int i = 0; i < m_ql_QuestList_KILL_TYPE.Count; i++)
            {
                if (m_ql_QuestList_KILL_TYPE[i].Check_Condition_Total() == true)
                {
                    if (m_ql_QuestList_KILL_TYPE[i].m_bClear == false)
                    {
                        // Quest 수락 가능.
                        if (m_ql_QuestList_KILL_TYPE[i].m_bProcess == false && m_ql_QuestList_KILL_TYPE[i].m_bCondition == false)
                        {
                            if (QuestManager.Instance.GetQuest_KILL_TYPE(m_ql_QuestList_KILL_TYPE[i].m_nQuest_Code).m_nNPC == m_nNPCCode)
                            {
                                m_nList_Accept.Add(m_ql_QuestList_KILL_TYPE[i].m_nQuest_Code);
                            }
                        }
                        // Quest 클리어 가능.
                        if (m_ql_QuestList_KILL_TYPE[i].m_bCondition == true)
                        {
                            if (QuestManager.Instance.GetQuest_KILL_TYPE(m_ql_QuestList_KILL_TYPE[i].m_nQuest_Code).m_nNPC_Clear == m_nNPCCode)
                            {
                                m_nList_Clear.Add(m_ql_QuestList_KILL_TYPE[i].m_nQuest_Code);
                            }
                            else if (QuestManager.Instance.GetQuest_KILL_TYPE(m_ql_QuestList_KILL_TYPE[i].m_nQuest_Code).m_nNPC == m_nNPCCode)
                            {
                                m_nList_Process.Add(m_ql_QuestList_KILL_TYPE[i].m_nQuest_Code);
                            }
                        }
                        // Quest 진행중.
                        if (m_ql_QuestList_KILL_TYPE[i].m_bCondition == false && m_ql_QuestList_KILL_TYPE[i].m_bProcess == true)
                        {
                            m_nList_Process.Add(m_ql_QuestList_KILL_TYPE[i].m_nQuest_Code);
                        }
                    }
                }
            }
            for (int i = 0; i < m_ql_QuestList_GOAWAY_MONSTER.Count; i++)
            {
                if (m_ql_QuestList_GOAWAY_MONSTER[i].Check_Condition_Total() == true)
                {
                    if (m_ql_QuestList_GOAWAY_MONSTER[i].m_bClear == false)
                    {
                        // Quest 수락 가능.
                        if (m_ql_QuestList_GOAWAY_MONSTER[i].m_bProcess == false && m_ql_QuestList_GOAWAY_MONSTER[i].m_bCondition == false)
                        {
                            if (QuestManager.Instance.GetQuest_GOAWAY_MONSTER(m_ql_QuestList_GOAWAY_MONSTER[i].m_nQuest_Code).m_nNPC == m_nNPCCode)
                            {
                                m_nList_Accept.Add(m_ql_QuestList_GOAWAY_MONSTER[i].m_nQuest_Code);
                            }
                        }
                        // Quest 클리어 가능.
                        if (m_ql_QuestList_GOAWAY_MONSTER[i].m_bCondition == true)
                        {
                            if (QuestManager.Instance.GetQuest_GOAWAY_MONSTER(m_ql_QuestList_GOAWAY_MONSTER[i].m_nQuest_Code).m_nNPC_Clear == m_nNPCCode)
                            {
                                m_nList_Clear.Add(m_ql_QuestList_GOAWAY_MONSTER[i].m_nQuest_Code);
                            }
                            else if (QuestManager.Instance.GetQuest_GOAWAY_MONSTER(m_ql_QuestList_GOAWAY_MONSTER[i].m_nQuest_Code).m_nNPC == m_nNPCCode)
                            {
                                m_nList_Process.Add(m_ql_QuestList_GOAWAY_MONSTER[i].m_nQuest_Code);
                            }
                        }
                        // Quest 진행중.
                        if (m_ql_QuestList_GOAWAY_MONSTER[i].m_bCondition == false && m_ql_QuestList_GOAWAY_MONSTER[i].m_bProcess == true)
                        {
                            m_nList_Process.Add(m_ql_QuestList_GOAWAY_MONSTER[i].m_nQuest_Code);
                        }
                    }
                }
            }
            for (int i = 0; i < m_ql_QuestList_GOAWAY_TYPE.Count; i++)
            {
                if (m_ql_QuestList_GOAWAY_TYPE[i].Check_Condition_Total() == true)
                {
                    if (m_ql_QuestList_GOAWAY_TYPE[i].m_bClear == false)
                    {
                        // Quest 수락 가능.
                        if (m_ql_QuestList_GOAWAY_TYPE[i].m_bProcess == false && m_ql_QuestList_GOAWAY_TYPE[i].m_bCondition == false)
                        {
                            if (QuestManager.Instance.GetQuest_GOAWAY_TYPE(m_ql_QuestList_GOAWAY_TYPE[i].m_nQuest_Code).m_nNPC == m_nNPCCode)
                            {
                                m_nList_Accept.Add(m_ql_QuestList_GOAWAY_TYPE[i].m_nQuest_Code);
                            }
                        }
                        // Quest 클리어 가능.
                        if (m_ql_QuestList_GOAWAY_TYPE[i].m_bCondition == true)
                        {
                            if (QuestManager.Instance.GetQuest_GOAWAY_TYPE(m_ql_QuestList_GOAWAY_TYPE[i].m_nQuest_Code).m_nNPC_Clear == m_nNPCCode)
                            {
                                m_nList_Clear.Add(m_ql_QuestList_GOAWAY_TYPE[i].m_nQuest_Code);
                            }
                            else if (QuestManager.Instance.GetQuest_GOAWAY_TYPE(m_ql_QuestList_GOAWAY_TYPE[i].m_nQuest_Code).m_nNPC == m_nNPCCode)
                            {
                                m_nList_Process.Add(m_ql_QuestList_GOAWAY_TYPE[i].m_nQuest_Code);
                            }
                        }
                        // Quest 진행중.
                        if (m_ql_QuestList_GOAWAY_TYPE[i].m_bCondition == false && m_ql_QuestList_GOAWAY_TYPE[i].m_bProcess == true)
                        {
                            m_nList_Process.Add(m_ql_QuestList_GOAWAY_TYPE[i].m_nQuest_Code);
                        }
                    }
                }
            }
            for (int i = 0; i < m_ql_QuestList_COLLECT.Count; i++)
            {
                if (m_ql_QuestList_COLLECT[i].Check_Condition_Total() == true)
                {
                    if (m_ql_QuestList_COLLECT[i].m_bClear == false)
                    {
                        // Quest 수락 가능.
                        if (m_ql_QuestList_COLLECT[i].m_bProcess == false && m_ql_QuestList_COLLECT[i].m_bCondition == false)
                        {
                            if (QuestManager.Instance.GetQuest_COLLECT(m_ql_QuestList_COLLECT[i].m_nQuest_Code).m_nNPC == m_nNPCCode)
                            {
                                m_nList_Accept.Add(m_ql_QuestList_COLLECT[i].m_nQuest_Code);
                            }
                        }
                        // Quest 클리어 가능.
                        if (m_ql_QuestList_COLLECT[i].m_bCondition == true)
                        {
                            if (QuestManager.Instance.GetQuest_COLLECT(m_ql_QuestList_COLLECT[i].m_nQuest_Code).m_nNPC_Clear == m_nNPCCode)
                            {
                                m_nList_Clear.Add(m_ql_QuestList_COLLECT[i].m_nQuest_Code);
                            }
                            else if (QuestManager.Instance.GetQuest_COLLECT(m_ql_QuestList_COLLECT[i].m_nQuest_Code).m_nNPC == m_nNPCCode)
                            {
                                m_nList_Process.Add(m_ql_QuestList_COLLECT[i].m_nQuest_Code);
                            }
                        }
                        // Quest 진행중.
                        if (m_ql_QuestList_COLLECT[i].m_bProcess == true && m_ql_QuestList_COLLECT[i].m_bCondition == false)
                        {
                            m_nList_Process.Add(m_ql_QuestList_COLLECT[i].m_nQuest_Code);
                        }
                    }
                }
            }
            for (int i = 0; i < m_ql_QuestList_CONVERSATION.Count; i++)
            {
                if (m_ql_QuestList_CONVERSATION[i].Check_Condition_Total() == true)
                {
                    if (m_ql_QuestList_CONVERSATION[i].m_bClear == false)
                    {
                        // Quest 수락 가능.
                        if (m_ql_QuestList_CONVERSATION[i].m_bProcess == false && m_ql_QuestList_CONVERSATION[i].m_bCondition == false)
                        {
                            if (QuestManager.Instance.GetQuest_CONVERSATION(m_ql_QuestList_CONVERSATION[i].m_nQuest_Code).m_nNPC == m_nNPCCode)
                            {
                                m_nList_Accept.Add(m_ql_QuestList_CONVERSATION[i].m_nQuest_Code);
                            }
                        }
                        // Quest 클리어 가능.
                        if (m_ql_QuestList_CONVERSATION[i].m_bCondition == true)
                        {
                            if (QuestManager.Instance.GetQuest_CONVERSATION(m_ql_QuestList_CONVERSATION[i].m_nQuest_Code).m_nNPC_Clear == m_nNPCCode)
                            {
                                m_nList_Clear.Add(m_ql_QuestList_CONVERSATION[i].m_nQuest_Code);
                            }
                            else if (QuestManager.Instance.GetQuest_CONVERSATION(m_ql_QuestList_CONVERSATION[i].m_nQuest_Code).m_nNPC == m_nNPCCode)
                            {
                                m_nList_Process.Add(m_ql_QuestList_CONVERSATION[i].m_nQuest_Code);
                            }
                        }
                        // Quest 진행중.
                        if (m_ql_QuestList_CONVERSATION[i].m_bProcess == true)
                        {
                            if (QuestManager.Instance.GetQuest_CONVERSATION(m_ql_QuestList_CONVERSATION[i].m_nQuest_Code).m_nNPC == m_nNPCCode)
                            {
                                m_nList_Process.Add(m_ql_QuestList_CONVERSATION[i].m_nQuest_Code);
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < m_ql_QuestList_ROLL.Count; i++)
            {
                if (m_ql_QuestList_ROLL[i].Check_Condition_Total() == true)
                {
                    if (m_ql_QuestList_ROLL[i].m_bClear == false)
                    {
                        // Quest 수락 가능.
                        if (m_ql_QuestList_ROLL[i].m_bProcess == false && m_ql_QuestList_ROLL[i].m_bCondition == false)
                        {
                            if (QuestManager.Instance.GetQuest_ROLL(m_ql_QuestList_ROLL[i].m_nQuest_Code).m_nNPC == m_nNPCCode)
                            {
                                m_nList_Accept.Add(m_ql_QuestList_ROLL[i].m_nQuest_Code);
                            }
                        }
                        // Quest 클리어 가능.
                        if (m_ql_QuestList_ROLL[i].m_bCondition == true)
                        {
                            if (QuestManager.Instance.GetQuest_ROLL(m_ql_QuestList_ROLL[i].m_nQuest_Code).m_nNPC_Clear == m_nNPCCode)
                            {
                                m_nList_Clear.Add(m_ql_QuestList_ROLL[i].m_nQuest_Code);
                            }
                            else if (QuestManager.Instance.GetQuest_ROLL(m_ql_QuestList_ROLL[i].m_nQuest_Code).m_nNPC == m_nNPCCode)
                            {
                                m_nList_Process.Add(m_ql_QuestList_ROLL[i].m_nQuest_Code);
                            }
                        }
                        // Quest 진행중.
                        if (m_ql_QuestList_ROLL[i].m_bCondition == false && m_ql_QuestList_ROLL[i].m_bProcess == true)
                        {
                            m_nList_Process.Add(m_ql_QuestList_ROLL[i].m_nQuest_Code);
                        }
                    }
                }
            }
            for (int i = 0; i < m_ql_QuestList_ELIMINATE_MONSTER.Count; i++)
            {
                if (m_ql_QuestList_ELIMINATE_MONSTER[i].Check_Condition_Total() == true)
                {
                    if (m_ql_QuestList_ELIMINATE_MONSTER[i].m_bClear == false)
                    {
                        // Quest 수락 가능.
                        if (m_ql_QuestList_ELIMINATE_MONSTER[i].m_bProcess == false && m_ql_QuestList_ELIMINATE_MONSTER[i].m_bCondition == false)
                        {
                            if (QuestManager.Instance.GetQuest_ELIMINATE_MONSTER(m_ql_QuestList_ELIMINATE_MONSTER[i].m_nQuest_Code).m_nNPC == m_nNPCCode)
                            {
                                m_nList_Accept.Add(m_ql_QuestList_ELIMINATE_MONSTER[i].m_nQuest_Code);
                            }
                        }
                        // Quest 클리어 가능.
                        if (m_ql_QuestList_ELIMINATE_MONSTER[i].m_bCondition == true)
                        {
                            if (QuestManager.Instance.GetQuest_ELIMINATE_MONSTER(m_ql_QuestList_ELIMINATE_MONSTER[i].m_nQuest_Code).m_nNPC_Clear == m_nNPCCode)
                            {
                                m_nList_Clear.Add(m_ql_QuestList_ELIMINATE_MONSTER[i].m_nQuest_Code);
                            }
                            else if (QuestManager.Instance.GetQuest_ELIMINATE_MONSTER(m_ql_QuestList_ELIMINATE_MONSTER[i].m_nQuest_Code).m_nNPC == m_nNPCCode)
                            {
                                m_nList_Process.Add(m_ql_QuestList_ELIMINATE_MONSTER[i].m_nQuest_Code);
                            }
                        }
                        // Quest 진행중.
                        if (m_ql_QuestList_ELIMINATE_MONSTER[i].m_bCondition == false && m_ql_QuestList_ELIMINATE_MONSTER[i].m_bProcess == true)
                        {
                            m_nList_Process.Add(m_ql_QuestList_ELIMINATE_MONSTER[i].m_nQuest_Code);
                        }
                    }
                }
            }
            for (int i = 0; i < m_ql_QuestList_ELIMINATE_TYPE.Count; i++)
            {
                if (m_ql_QuestList_ELIMINATE_TYPE[i].Check_Condition_Total() == true)
                {
                    if (m_ql_QuestList_ELIMINATE_TYPE[i].m_bClear == false)
                    {
                        // Quest 수락 가능.
                        if (m_ql_QuestList_ELIMINATE_TYPE[i].m_bProcess == false && m_ql_QuestList_ELIMINATE_TYPE[i].m_bCondition == false)
                        {
                            if (QuestManager.Instance.GetQuest_ELIMINATE_TYPE(m_ql_QuestList_ELIMINATE_TYPE[i].m_nQuest_Code).m_nNPC == m_nNPCCode)
                            {
                                m_nList_Accept.Add(m_ql_QuestList_ELIMINATE_TYPE[i].m_nQuest_Code);
                            }
                        }
                        // Quest 클리어 가능.
                        if (m_ql_QuestList_ELIMINATE_TYPE[i].m_bCondition == true)
                        {
                            if (QuestManager.Instance.GetQuest_ELIMINATE_TYPE(m_ql_QuestList_ELIMINATE_TYPE[i].m_nQuest_Code).m_nNPC_Clear == m_nNPCCode)
                            {
                                m_nList_Clear.Add(m_ql_QuestList_ELIMINATE_TYPE[i].m_nQuest_Code);
                            }
                            else if (QuestManager.Instance.GetQuest_ELIMINATE_TYPE(m_ql_QuestList_ELIMINATE_TYPE[i].m_nQuest_Code).m_nNPC == m_nNPCCode)
                            {
                                m_nList_Process.Add(m_ql_QuestList_ELIMINATE_TYPE[i].m_nQuest_Code);
                            }
                        }
                        // Quest 진행중.
                        if (m_ql_QuestList_ELIMINATE_TYPE[i].m_bCondition == false && m_ql_QuestList_ELIMINATE_TYPE[i].m_bProcess == true)
                        {
                            m_nList_Process.Add(m_ql_QuestList_ELIMINATE_TYPE[i].m_nQuest_Code);
                        }
                    }
                }
            }

            // Icon Display
            if (m_nList_Process.Count > 0)
            {
                if (m_nList_Accept.Count <= 0 && m_nList_Clear.Count <= 0)
                {
                    m_gIcon_Quest_Accept_Possible.SetActive(false);
                    m_gIcon_Quest_Clear_Possible.SetActive(false);
                    m_gIcon_Quest_Accept_N_Clear_Possible.SetActive(false);
                    m_gIcon_Quest_Process_.SetActive(true);
                    m_gIcon_Quest_Process.SetActive(false);
                }
                if (m_nList_Accept.Count > 0 && m_nList_Clear.Count <= 0)
                {
                    m_gIcon_Quest_Accept_Possible.SetActive(true);
                    m_gIcon_Quest_Clear_Possible.SetActive(false);
                    m_gIcon_Quest_Accept_N_Clear_Possible.SetActive(false);
                    m_gIcon_Quest_Process_.SetActive(false);
                    m_gIcon_Quest_Process.SetActive(true);
                }
                if (m_nList_Accept.Count <= 0 && m_nList_Clear.Count > 0)
                {
                    m_gIcon_Quest_Accept_Possible.SetActive(false);
                    m_gIcon_Quest_Clear_Possible.SetActive(true);
                    m_gIcon_Quest_Accept_N_Clear_Possible.SetActive(false);
                    m_gIcon_Quest_Process_.SetActive(false);
                    m_gIcon_Quest_Process.SetActive(true);
                }
                if (m_nList_Accept.Count > 0 && m_nList_Clear.Count > 0)
                {
                    m_gIcon_Quest_Accept_Possible.SetActive(false);
                    m_gIcon_Quest_Clear_Possible.SetActive(false);
                    m_gIcon_Quest_Accept_N_Clear_Possible.SetActive(true);
                    m_gIcon_Quest_Process_.SetActive(false);
                    m_gIcon_Quest_Process.SetActive(true);
                }
            }
            else
            {
                if (m_nList_Accept.Count <= 0 && m_nList_Clear.Count <= 0)
                {
                    m_gIcon_Quest_Accept_Possible.SetActive(false);
                    m_gIcon_Quest_Clear_Possible.SetActive(false);
                    m_gIcon_Quest_Accept_N_Clear_Possible.SetActive(false);
                    m_gIcon_Quest_Process_.SetActive(false);
                    m_gIcon_Quest_Process.SetActive(false);
                }
                if (m_nList_Accept.Count > 0 && m_nList_Clear.Count <= 0)
                {
                    m_gIcon_Quest_Accept_Possible.SetActive(true);
                    m_gIcon_Quest_Clear_Possible.SetActive(false);
                    m_gIcon_Quest_Accept_N_Clear_Possible.SetActive(false);
                    m_gIcon_Quest_Process_.SetActive(false);
                    m_gIcon_Quest_Process.SetActive(false);
                }
                if (m_nList_Accept.Count <= 0 && m_nList_Clear.Count > 0)
                {
                    m_gIcon_Quest_Accept_Possible.SetActive(false);
                    m_gIcon_Quest_Clear_Possible.SetActive(true);
                    m_gIcon_Quest_Accept_N_Clear_Possible.SetActive(false);
                    m_gIcon_Quest_Process_.SetActive(false);
                    m_gIcon_Quest_Process.SetActive(false);
                }
                if (m_nList_Accept.Count > 0 && m_nList_Clear.Count > 0)
                {
                    m_gIcon_Quest_Accept_Possible.SetActive(false);
                    m_gIcon_Quest_Clear_Possible.SetActive(false);
                    m_gIcon_Quest_Accept_N_Clear_Possible.SetActive(true);
                    m_gIcon_Quest_Process_.SetActive(false);
                    m_gIcon_Quest_Process.SetActive(false);
                }
            }

            if (m_bNPC_Store == true)
            {
                for (int i = 0; i < m_List_NPC_Store.Count; i++)
                {
                    if (m_List_NPC_Store[i].Check_Condition_Store() == true)
                    {
                        m_gIcon_Store.SetActive(true);
                        return 0;
                    }
                }
                m_gIcon_Store.SetActive(false);
            }

        }

        return 0;
    }
}
