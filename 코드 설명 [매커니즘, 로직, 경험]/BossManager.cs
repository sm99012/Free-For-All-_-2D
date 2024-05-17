using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_BATTLE_BOSS_INFO { NULL, TESLIME }
public enum E_BATTLE_BOSS_STATE { NULL, BATTLE, END }

public class BossManager : MonoBehaviour
{
    private static BossManager instance = null;
    public static BossManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    public E_BATTLE_BOSS_INFO m_eBattle_Boss_Info;
    public E_BATTLE_BOSS_STATE m_eBattle_Boss_State;

    [SerializeField] List<GameObject> m_gl_Spawn_Boss;
    [SerializeField] List<GameObject> m_gl_Disappear_Object;

    public static Dictionary<int, BossBattleDictionary> m_Dictionary_BossBattle;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        m_eBattle_Boss_Info = E_BATTLE_BOSS_INFO.NULL;
        m_eBattle_Boss_State = E_BATTLE_BOSS_STATE.NULL;

        m_gl_Spawn_Boss = new List<GameObject>();
        m_gl_Disappear_Object = new List<GameObject>();

        InitialSet();
    }

    void InitialSet()
    {
        m_Dictionary_BossBattle = new Dictionary<int, BossBattleDictionary>();
        BossBattleDictionary bd;

        // 보스전투: '드넓은 초원' 의 진정한 평화를 위한 길
        if (m_Dictionary_BossBattle.ContainsKey(1) == false)
        {
            bd = new BossBattleDictionary("보스전투[S5]: '드넓은 초원' 의 진정한 평화", 1, E_BOSSBATTLE_GRADE.S5);
            bd.BossBattleDictionary_Add("테 슬라임", 10001, 
                "'테 슬라임' 은 '주식회사 더 슬라' 의 최고 실력자입니다.\n특히나 그의 검술과 정신력은 이 세계 최고 수준입니다.\n이런 그를 상대하기 위해서는 만반의 준비를 갖추어야 합니다.",
                "[ 권장 능력치, 아이템 / 주의사항 ]\n1. 데미지 30 이상. / 방어력 40 이상.\n2. 각종 회복포션, 버프포션 필요.\n3. [ 1 ], [ 2 ], [ 3 ] 페이즈로 구분되는 보스 몬스터 패턴.\n※ 보스전투는 한번 시작하면 맵 이동이 불가능해 집니다.");

            m_Dictionary_BossBattle.Add(1, bd);
        }
    }

    int m_nBoss_DurationTime_Teslime = 600;
    public Coroutine m_cProcess_Boss_DurationTime_TeSlime;
    IEnumerator Process_Boss_DurationTime_TeSlime()
    {
        m_nBoss_DurationTime_Teslime = 600;

        while (m_nBoss_DurationTime_Teslime > 0)
        {
            GUIManager_Total.Instance.m_GUI_BossInformation.Update_BossInformation_Time(m_nBoss_DurationTime_Teslime);
            yield return new WaitForSeconds(1);
            m_nBoss_DurationTime_Teslime -= 1;
        }

        End_Battle_Boss_Fail();
        m_Dictionary_BossBattle[1].m_bSpawn_Possible = true;
        m_cProcess_Boss_DurationTime_TeSlime = null;
    }
    public void Start_Battle_Boss_TeSlime(Vector2 spawnposition)
    {
        m_Dictionary_BossBattle[1].m_bSpawn_Possible = false;
        Start_Battle_Boss(10001, 0, spawnposition);
        m_cProcess_Boss_DurationTime_TeSlime = StartCoroutine(Process_Boss_DurationTime_TeSlime());

        m_gl_Disappear_Object.Add(GameObject.Find("Potal").transform.Find("Potal 10001_10").gameObject);

        if (m_gl_Disappear_Object.Count != 0)
        {
            for (int i = 0; i < m_gl_Disappear_Object.Count; i++)
            {
                m_gl_Disappear_Object[i].SetActive(false);
            }
        }    
    }
    public int Get_Boss_DurationTime_TeSlime()
    {
        return m_nBoss_DurationTime_Teslime;
    }


    public void Start_Battle_Boss(int bosscode, int mapcode, Vector2 spawnposition)
    {
        switch (bosscode)
        {
            case 10001: // 테 슬라임.
                {
                    m_eBattle_Boss_Info = E_BATTLE_BOSS_INFO.TESLIME;
                    m_eBattle_Boss_State = E_BATTLE_BOSS_STATE.BATTLE;

                    GameObject bossmonster = Instantiate(Resources.Load("Prefab/Monster/TeSlime") as GameObject);
                    bossmonster.transform.position = spawnposition;

                    m_gl_Spawn_Boss.Add(bossmonster);

                    //GUIManager_Total.Instance.Display_GUI_BossInformation("테 슬라임", 21);
                    //Debug.Log(bossmonster.GetComponent<TeSlime_Total>().m_ts_Status.m_sStatus.GetSTATUS_HP_Current() + " / " + bossmonster.GetComponent<TeSlime_Total>().m_ts_Status.m_sStatus.GetSTATUS_HP_Max());
                    //GUIManager_Total.Instance.Update_BossInformation(bossmonster.GetComponent<TeSlime_Total>().m_ts_Status.m_sStatus.GetSTATUS_HP_Max(), bossmonster.GetComponent<TeSlime_Total>().m_ts_Status.m_sStatus.GetSTATUS_HP_Current());
                } break;
        }
    }

    public void End_Battle_Boss()
    {
        StartCoroutine(Process_End_Battle_Boss());

        //GUIManager_Total.Instance.UnDisplay_GUI_BossInformation();
    }
    IEnumerator Process_End_Battle_Boss()
    {
        yield return new WaitForSeconds(5f);

        if (m_gl_Spawn_Boss.Count != 0)
        {
            for (int i = 0; i < m_gl_Spawn_Boss.Count; i++)
            {
                Destroy(m_gl_Spawn_Boss[i]);
            }
        }
        m_gl_Spawn_Boss.Clear();

        if (m_gl_Disappear_Object.Count != 0)
        {
            for (int i = 0; i < m_gl_Disappear_Object.Count; i++)
            {
                m_gl_Disappear_Object[i].SetActive(true);
            }
        }
        m_gl_Spawn_Boss.Clear();

        if (m_cProcess_Boss_DurationTime_TeSlime != null)
        {
            StopCoroutine(m_cProcess_Boss_DurationTime_TeSlime);
            m_cProcess_Boss_DurationTime_TeSlime = null;
        }
    }


    public void End_Battle_Boss_Succes()
    {
        Disappear_Boss();
        End_Battle_Boss();

        GUIManager_Total.Instance.UnDisplay_GUI_BossInformation();
        BossManager.Instance.m_eBattle_Boss_State = E_BATTLE_BOSS_STATE.NULL;

        // 보스전투 승리 UI 출력.
    }
    public void End_Battle_Boss_Fail()
    {
        Disappear_Boss();
        End_Battle_Boss();

        // 보스전투 패배 UI 출력.
    }

    public void Disappear_Boss()
    {
        switch(m_eBattle_Boss_Info)
        {
            case E_BATTLE_BOSS_INFO.TESLIME:
            {
                    for (int i = 0; i < m_gl_Spawn_Boss.Count; i++)
                    {
                        if (m_gl_Spawn_Boss[i].name == "TeSlime")
                        {
                            m_gl_Spawn_Boss[i].GetComponent<TeSlime_Total>().m_tm_Move.Fadeout();
                        }
                    }
            } break;

        }

        // GUIManager_Total.Instance.UnDisplay_GUI_BossInformation();

        m_eBattle_Boss_Info = E_BATTLE_BOSS_INFO.NULL;
        m_eBattle_Boss_State = E_BATTLE_BOSS_STATE.END;
    }
}

public enum E_BOSSBATTLE_GRADE { S1, S2, S3, S4, S5, S6, S7, S8, S9 }

public class BossBattleDictionary
{
    public string m_sBossBattle_Name;
    public int m_nBossBattle_Code;
    public E_BOSSBATTLE_GRADE m_eBossBattle_Grade;
    public bool m_bSpawn_Possible;

    public List<int> m_nl_Object_Code;
    public List<string> m_sl_Object_Description;
    public List<string> m_sl_Object_Guide;

    public BossBattleDictionary(string name, int code, E_BOSSBATTLE_GRADE ebg)
    {
        this.m_sBossBattle_Name = name;
        this.m_nBossBattle_Code = code;
        this.m_eBossBattle_Grade = ebg;
        this.m_bSpawn_Possible = true;

        m_nl_Object_Code = new List<int>();
        m_sl_Object_Description = new List<string>();
        m_sl_Object_Guide = new List<string>();
    }

    public void BossBattleDictionary_Add(string objectname, int objectcode, string des, string gui)
    {
        m_nl_Object_Code.Add(objectcode);
        m_sl_Object_Description.Add(des);
        m_sl_Object_Guide.Add(gui);
    }
}