using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Total_Manager : MonoBehaviour
{
    private static Total_Manager instance = null;
    public static Total_Manager Instance
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

    public int m_nSceneNumber;
    public Image m_IMG_ProgressBar;
    public TextMeshProUGUI m_TMP_ProgressInformation;
    public TextMeshProUGUI m_TMP_LoadingRate;

    public bool m_bStart;

    public static bool m_bPlay = false;

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

        // 첫번째 씬을 튜토리얼맵으로 고정.
        m_nSceneNumber = 0;

        m_bStart = false;

        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Process_Start());
    }
    static bool m_bProcessStart_Map = false;
    static bool m_bProcessStart_Player = false;
    static bool m_bProcessStart_Skill = false;
    static bool m_bProcessStart_Item = false;
    static bool m_bProcessStart_ItemSetEffect = false;
    static bool m_bProcessStart_Quest = false;
    static bool m_bProcessStart_Conversation = false;
    static bool m_bProcessStart_Collection = false;
    static bool m_bProcessStart_NPC = false;
    static bool m_bProcessStart_GUI = false;
    
    public void GameStart(string scenename, int scenecode, Vector3 pos, int mapcode)
    {
        StartCoroutine(Process_Start(scenename, scenecode, pos, mapcode));
    }
    IEnumerator Process_Start(string scenename, int scenecode, Vector3 pos, int mapcode)
    {
        GUI_Canvas_GUI.Instance.gameObject.SetActive(true);
        GUI_Canvas_Loading.Instance.gameObject.SetActive(true);

        yield return null;

        m_TMP_ProgressInformation.text = "맵 정보를 불러오는중. . .";
        MapManager.Instance.InitialSet(); m_bProcessStart_Map = true;
        yield return new WaitForSeconds(0.5f);
        m_TMP_ProgressInformation.text = "플레이어 정보를 불러오는중. . .";
        Player_Total.Instance.InitialSet(); m_bProcessStart_Player = true;
        yield return new WaitForSeconds(0.5f);
        m_TMP_ProgressInformation.text = "스킬 정보를 불러오는중. . .";
        SkillManager.Instance.InitialSet(); m_bProcessStart_Skill = true;
        yield return new WaitForSeconds(0.5f);
        m_TMP_ProgressInformation.text = "아이템 정보를 불러오는중. . .";
        ItemManager.instance.InitialSet(); m_bProcessStart_Item = true;
        m_TMP_ProgressInformation.text = "아이템 세트 효과 정보를 불러오는중. . .";
        ItemSetEffectManager.instance.InitialSet(); m_bProcessStart_ItemSetEffect = true;
        yield return new WaitForSeconds(0.5f);
        m_TMP_ProgressInformation.text = "퀘스트 정보를 불러오는중. . .";
        QuestManager.Instance.InitialSet(); m_bProcessStart_Quest = true;
        yield return new WaitForSeconds(0.5f);
        m_TMP_ProgressInformation.text = "대화 정보를 불러오는중. . .";
        ConversationManager.Instance.InitialSet(); m_bProcessStart_Conversation = true;
        yield return new WaitForSeconds(0.5f);
        m_TMP_ProgressInformation.text = "채집물 정보를 불러오는중. . .";
        CollectionManager.Instance.InitialSet(); m_bProcessStart_Collection = true;
        yield return new WaitForSeconds(0.5f);
        m_TMP_ProgressInformation.text = "NPC 정보를 불러오는중. . .";
        NPCManager_Total.Instance.InitialSet(); m_bProcessStart_NPC = true;
        yield return new WaitForSeconds(0.5f);
        m_TMP_ProgressInformation.text = "UI 정보를 불러오는중. . .";
        GUIManager_Total.Instance.InitialSet(); m_bProcessStart_GUI = true;
        yield return new WaitForSeconds(0.5f);
        m_TMP_ProgressInformation.text = "";

        DataManager.instance.LoadData();

        //Initial_LoadScene("Scenes_Tutorial", 0, new Vector3(0, 0, 0), 0);
        Initial_LoadScene(scenename, scenecode, pos, mapcode);

        // Load Data 스크립트.

        //GUIManager_Total.Instance.m_GUI_Quest.UpdateQuest_Init();

        m_bStart = true;

        //LoadScene("Scenes_Tutorial", 0);
        //StartCoroutine(Process_LoadScene("Scenes_Tutorial", 0, true));
        //MapManager.Instance.ChangeScene(0);
    }

    // Loading
    public void LoadScene(string scenename, int scenecode)
    {
        SceneManager.LoadScene("Scenes_Loading");

        StartCoroutine(Process_LoadScene(scenename, scenecode));

        GUIManager_Total.Instance.m_nDisplay_GUI_Quest_Number = 0;
    }
    // Initial Loading
    public void Initial_LoadScene(string scenename, int scenecode, Vector3 pos, int mapcode)
    {
        SceneManager.LoadScene("Scenes_Loading");

        StartCoroutine(Process_LoadScene_Tutorial(scenename, scenecode, pos, mapcode));

        GUIManager_Total.Instance.m_nDisplay_GUI_Quest_Number = 0;
    }
    // Loading
    public void LoadSavedScene(string scenename, int scenecode, Vector3 pos, int mapcode)
    {
        SceneManager.LoadScene("Scenes_Loading");

        StartCoroutine(Process_Initial_LoadScene(scenename, scenecode, pos, mapcode));

        GUIManager_Total.Instance.m_nDisplay_GUI_Quest_Number = 0;
    }
    IEnumerator Process_LoadScene(string scenename, int scenecode, bool bstart = false)
    {
        while (m_bProcessStart_GUI == false)
            yield return null;

        if (m_bProcessStart_GUI == true)
        {
            yield return null;

            GUIManager_Total.Instance.Display_GUI_Scene(scenecode);

            m_IMG_ProgressBar.fillAmount = 0;
            GUI_Canvas_Loading.Instance.gameObject.SetActive(true);
            yield return null;

            AsyncOperation asyncloading = SceneManager.LoadSceneAsync(scenename);
            asyncloading.allowSceneActivation = false;

            bool bLoadingSucces = false;
            float fstime = 0;
            float frate = 0;

            while (bLoadingSucces == false)
            {
                yield return null;
                fstime += Time.deltaTime;

                if (fstime <= 2.9f)
                {
                    m_TMP_LoadingRate.text = (Mathf.Round((fstime / 2.9f) * 100)).ToString() + " %";
                    // 부동소수점 문제 때문에 Time.deltaTime(0.016666) * 180 값에 가까운 2.9f 를 가정함.
                    m_IMG_ProgressBar.fillAmount = Mathf.Lerp(m_IMG_ProgressBar.fillAmount, fstime / 2.9f, fstime);
                }
                else
                {
                    if (asyncloading.progress >= 0.9f)
                    {
                        bLoadingSucces = true;
                        m_TMP_LoadingRate.text = "100 %";
                    }
                    else
                    {
                        m_TMP_LoadingRate.text = "99 %";
                    }
                }

                if (bLoadingSucces == true)
                {
                    //m_IMG_ProgressBar.fillAmount = Mathf.Lerp(m_IMG_ProgressBar.fillAmount, 1f, fstime);
                    //while (m_bStart == false)
                    //{
                    //    yield return null;
                    //}
                    yield return new WaitForSeconds(0.5f);

                    asyncloading.allowSceneActivation = true;

                    //GUIManager_Total.Instance.Update_ChangeMap();

                    yield return new WaitForSeconds(0.5f);

                    GUIManager_Total.Instance.Update_ChangeMap();

                    yield return new WaitForSeconds(1f);

                    GUI_Canvas_Loading.Instance.gameObject.SetActive(false);

                    MapManager.Instance.ChangeScene(scenecode);
                    MapManager.Instance.Update_Map_Object(Player_Total.Instance.m_pm_Map.m_Map);

                    NPCManager_Total.Instance.UpdateNPC();

                    //GUIManager_Total.Instance.m_nDisplay_GUI_Quest_Number = 0;

                    //GUIManager_Total.Instance.Display_GUI_Quest();
                    GUIManager_Total.Instance.m_GUI_Quest.UpdateQuest_SceneChange();

                    yield break;
                }
            }
        }
    }
    IEnumerator Process_Initial_LoadScene(string scenename, int scenecode, Vector3 pos, int mapcode = 0, bool bstart = false)
    {
        // Load
        while (m_bProcessStart_GUI == false)
            yield return null;

        if (m_bProcessStart_GUI == true)
        {
            yield return null;

            GUIManager_Total.Instance.Display_GUI_Scene(scenecode);

            m_IMG_ProgressBar.fillAmount = 0;
            GUI_Canvas_Loading.Instance.gameObject.SetActive(true);
            yield return null;

            AsyncOperation asyncloading = SceneManager.LoadSceneAsync(scenename);
            asyncloading.allowSceneActivation = false;

            bool bLoadingSucces = false;
            float fstime = 0;
            float frate = 0;

            while (bLoadingSucces == false)
            {
                yield return null;
                fstime += Time.deltaTime;

                if (fstime <= 2.9f)
                {
                    m_TMP_LoadingRate.text = (Mathf.Round((fstime / 2.9f) * 100)).ToString() + " %";
                    // 부동소수점 문제 때문에 Time.deltaTime(0.016666) * 180 값에 가까운 2.9f 를 가정함.
                    m_IMG_ProgressBar.fillAmount = Mathf.Lerp(m_IMG_ProgressBar.fillAmount, fstime / 2.9f, fstime);
                }
                else
                {
                    if (asyncloading.progress >= 0.9f)
                    {
                        bLoadingSucces = true;
                        m_TMP_LoadingRate.text = "100 %";
                    }
                    else
                    {
                        m_TMP_LoadingRate.text = "99 %";
                    }
                }

                if (bLoadingSucces == true)
                {
                    //m_IMG_ProgressBar.fillAmount = Mathf.Lerp(m_IMG_ProgressBar.fillAmount, 1f, fstime);
                    //while (m_bStart == false)
                    //{
                    //    yield return null;
                    //}
                    yield return new WaitForSeconds(0.5f);

                    asyncloading.allowSceneActivation = true;

                    //GUIManager_Total.Instance.Update_ChangeMap();

                    //yield return new WaitForSeconds(0.5f);

                    //yield return new WaitForSeconds(1f);

                    yield return null;

                    MapManager.Instance.ChangeScene(scenecode);

                    if (MapManager.Instance.m_Dictionary_Map_Tutorial.ContainsKey(mapcode) == true)
                    {
                        Player_Total.Instance.ChangeMap(true, MapManager.Instance.m_Dictionary_Map_Tutorial[mapcode], pos);
                    }
                    else if (MapManager.Instance.m_Dictionary_Map_Chapter1.ContainsKey(mapcode) == true)
                    {
                        Player_Total.Instance.ChangeMap(true, MapManager.Instance.m_Dictionary_Map_Chapter1[mapcode], pos);
                    }
                    GUIManager_Total.Instance.Update_ChangeMap();

                    yield return new WaitForSeconds(1f);

                    GUI_Canvas_Loading.Instance.gameObject.SetActive(false);

                    if (m_bPlay == false)
                    {
                        m_bPlay = true;
                        MapManager.Instance.Update_Map_Object(Player_Total.Instance.m_pm_Map.m_Map);
                    }

                    //MapManager.Instance.ChangeScene(scenecode);
                    //MapManager.Instance.Update_Map_Object(Player_Total.Instance.m_pm_Map.m_Map);

                    Player_Total.Instance.m_pq_Quest.QuestUpdate_Collect_NoDisplay();

                    //GUIManager_Total.Instance.m_nDisplay_GUI_Quest_Number = 0;

                    //GUIManager_Total.Instance.Display_GUI_Quest();
                    GUIManager_Total.Instance.m_GUI_Quest.UpdateQuest_SceneChange();

                    yield break;
                }
            }
        }
    }
    IEnumerator Process_LoadScene_Tutorial(string scenename, int scenecode, Vector3 pos, int mapcode = 0, bool bstart = false)
    {
        while (m_bProcessStart_GUI == false)
            yield return null;

        if (m_bProcessStart_GUI == true)
        {
            yield return null;

            //GUIManager_Total.Instance.Display_GUI_Scene(1);

            m_IMG_ProgressBar.fillAmount = 0;
           // GUI_Canvas_Loading.Instance.gameObject.SetActive(true);
            yield return null;

            AsyncOperation asyncloading = SceneManager.LoadSceneAsync("Scenes_Tutorial");
            asyncloading.allowSceneActivation = false;

            bool bLoadingSucces = false;
            float fstime = 0;
            float frate = 0;

            while (bLoadingSucces == false)
            {
                yield return null;

                if (asyncloading.progress >= 0.9f)
                {
                    bLoadingSucces = true;
                    //m_TMP_LoadingRate.text = "100 %";
                }
                else
                {
                    //m_TMP_LoadingRate.text = "99 %";
                }

                if (bLoadingSucces == true)
                {
                    //m_IMG_ProgressBar.fillAmount = Mathf.Lerp(m_IMG_ProgressBar.fillAmount, 1f, fstime);
                    //while (m_bStart == false)
                    //{
                    //    yield return null;
                    //}
                    yield return new WaitForSeconds(0.5f);

                    asyncloading.allowSceneActivation = true;

                    //GUIManager_Total.Instance.Update_ChangeMap();

                    yield return new WaitForSeconds(0.5f);

                    //GUIManager_Total.Instance.Update_ChangeMap();

                    yield return new WaitForSeconds(1f);

                    //GUI_Canvas_Loading.Instance.gameObject.SetActive(false);

                    //MapManager.Instance.ChangeScene(1);
                    //MapManager.Instance.Update_Map_Object(Player_Total.Instance.m_pm_Map.m_Map);

                    //NPCManager_Total.Instance.UpdateNPC();

                    SceneManager.LoadScene("Scenes_Loading");

                    StartCoroutine(Process_LoadScene_Chapter1(scenename, scenecode, pos, mapcode));

                    //GUIManager_Total.Instance.m_nDisplay_GUI_Quest_Number = 0;

                    //GUIManager_Total.Instance.Display_GUI_Quest();
                    GUIManager_Total.Instance.m_GUI_Quest.UpdateQuest_SceneChange();
                    yield break;
                }
            }
        }
    }
    IEnumerator Process_LoadScene_Chapter1(string scenename, int scenecode, Vector3 pos, int mapcode = 0, bool bstart = false)
    {
        while (m_bProcessStart_GUI == false)
            yield return null;

        if (m_bProcessStart_GUI == true)
        {
            yield return null;

            //GUIManager_Total.Instance.Display_GUI_Scene(2);

            m_IMG_ProgressBar.fillAmount = 0;
            //GUI_Canvas_Loading.Instance.gameObject.SetActive(true);
            yield return null;

            AsyncOperation asyncloading = SceneManager.LoadSceneAsync("Scenes_Chapter1");
            asyncloading.allowSceneActivation = false;

            bool bLoadingSucces = false;
            float fstime = 0;
            float frate = 0;

            while (bLoadingSucces == false)
            {
                yield return null;

                if (asyncloading.progress >= 0.9f)
                {
                    bLoadingSucces = true;
                    //m_TMP_LoadingRate.text = "100 %";
                }
                else
                {
                    //m_TMP_LoadingRate.text = "99 %";
                }

                if (bLoadingSucces == true)
                {
                    //m_IMG_ProgressBar.fillAmount = Mathf.Lerp(m_IMG_ProgressBar.fillAmount, 1f, fstime);
                    //while (m_bStart == false)
                    //{
                    //    yield return null;
                    //}
                    yield return new WaitForSeconds(0.5f);

                    asyncloading.allowSceneActivation = true;

                    //GUIManager_Total.Instance.Update_ChangeMap();

                    yield return new WaitForSeconds(0.5f);

                    //GUIManager_Total.Instance.Update_ChangeMap();

                    yield return new WaitForSeconds(1f);

                    //GUI_Canvas_Loading.Instance.gameObject.SetActive(false);

                    //MapManager.Instance.ChangeScene(2);
                    //MapManager.Instance.Update_Map_Object(Player_Total.Instance.m_pm_Map.m_Map);

                    //NPCManager_Total.Instance.UpdateNPC();


                    SceneManager.LoadScene("Scenes_Loading");

                    StartCoroutine(Process_Initial_LoadScene(scenename, scenecode, pos, mapcode));

                    //GUIManager_Total.Instance.m_nDisplay_GUI_Quest_Number = 0;

                    //GUIManager_Total.Instance.Display_GUI_Quest();
                    GUIManager_Total.Instance.m_GUI_Quest.UpdateQuest_SceneChange();
                    yield break;
                }
            }
        }
    }
    // Check framerate.

    //public int fontSize = 30;
    //public float width, height;
    //float time = 0;
    //private void OnGUI()
    //{
    //    Rect position = new Rect(width, height, Screen.width, Screen.height);

    //    float fps = 1.0f / Time.deltaTime;
    //    float ms = Time.deltaTime * 1000.0f;
    //    string text = string.Format("{0:N1} FPS ({1:N1}ms)", fps, ms);

    //    GUIStyle style = new GUIStyle();

    //    style.fontSize = fontSize;
    //    style.normal.textColor = Color.white;

    //    GUI.Label(position, text, style);

    //    time += Time.deltaTime;
    //    //Debug.Log(time);

    //}
}
