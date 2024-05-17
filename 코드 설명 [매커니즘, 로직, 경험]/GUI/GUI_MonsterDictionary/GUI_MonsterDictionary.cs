using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUI_MonsterDictionary : MonoBehaviour
{
    [SerializeField] GameObject m_gPanel_MonsterDictionary;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_MonsterDictionary_UpBar;
    [SerializeField] TextMeshProUGUI m_TMP_MonsterDictionary_UpBar_Name;
    [SerializeField] Button m_BTN_MonsterDictionary_UpBar_Exit;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_MonsterDictionary_SimpleInfo;
    [SerializeField] GameObject m_gPanel_MonsterDictionary_SimpleInfo_Image;
    [SerializeField] GameObject m_gPanel_MonsterDictionary_SimpleInfo_Background;
    [SerializeField] GameObject m_gPanel_MonsterDictionary_SimpleInfo_MonsterImage;
    [SerializeField] Image m_IMG_MonsterDictionary_SimpleInfo_MonsterImage;

    [SerializeField] GameObject m_gPanel_MonsterDictionary_SimpleInfo_Info;
    [SerializeField] TextMeshProUGUI m_TMP_MonsterDictionary_SimpleInfo_Info;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_MonsterDictionary_DetailInfo;
    [SerializeField] GameObject m_gPanel_MonsterDictionary_DetailInfo_UpBar;
    [SerializeField] GameObject m_gPanel_MonsterDictionary_DetailInfo_UpBar_Description;
    [SerializeField] Button m_BTN_MonsterDictionary_DetailInfo_UpBar_Description;
    [SerializeField] GameObject m_gPanel_Description_TMP;
    [SerializeField] GameObject m_gPanel_Description_Rock;
    [SerializeField] GameObject m_gPanel_MonsterDictionary_DetailInfo_UpBar_AppearArea;
    [SerializeField] Button m_BTN_MonsterDictionary_DetailInfo_UpBar_AppearArea;
    [SerializeField] GameObject m_gPanel_AppearArea_TMP;
    [SerializeField] GameObject m_gPanel_AppearArea_Rock;
    [SerializeField] GameObject m_gPanel_MonsterDictionary_DetailInfo_UpBar_SS;
    [SerializeField] Button m_BTN_MonsterDictionary_DetailInfo_UpBar_SS;
    [SerializeField] GameObject m_gPanel_SS_TMP;
    [SerializeField] GameObject m_gPanel_SS_Rock;
    [SerializeField] GameObject m_gPanel_MonsterDictionary_DetailInfo_UpBar_Reward;
    [SerializeField] Button m_BTN_MonsterDictionary_DetailInfo_UpBar_Reward;
    [SerializeField] GameObject m_gPanel_Reward_TMP;
    [SerializeField] GameObject m_gPanel_Reward_Rock;
    [Space(20)]
    [SerializeField] GameObject m_gSV_MonsterDictionary_DetailInfo_Description;
    [SerializeField] GameObject m_gViewport_MonsterDictionary_DetailInfo_Description;
    [SerializeField] TextMeshProUGUI m_TMP_MonsterDictionary_DetailInfo_Description;
    [SerializeField] Scrollbar m_ScrollBar_MonsterDictionary_DetailInfo_Description;
    [Space(20)]
    [SerializeField] GameObject m_gSV_MonsterDictionary_DetailInfo_Panel;
    [SerializeField] GameObject m_gViewport_MonsterDictionary_DetailInfo_Panel;
    [SerializeField] GameObject m_gContent_MonsterDictionary_DetailInfo_Panel;
    [SerializeField] Scrollbar m_ScrollBar_MonsterDictionary_DetailInfo_Panel;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_MonsterDictionary_DetailInfo_Reward;
    [SerializeField] GameObject m_gBTN_MonsterDictionary_DetailInfo_Reward_Death;
    [SerializeField] Button m_BTN_MonsterDictionary_DetailInfo_Reward_Death;
    [SerializeField] GameObject m_gBTN_MonsterDictionary_DetailInfo_Reward_Goaway;
    [SerializeField] Button m_BTN_MonsterDictionary_DetailInfo_Reward_Goaway;
    [SerializeField] GameObject m_gBTN_MonsterDictionary_DetailInfo_Reward_Item_Equip;
    [SerializeField] Button m_BTN_MonsterDictionary_DetailInfo_Reward_Item_Equip;
    [SerializeField] GameObject m_gBTN_MonsterDictionary_DetailInfo_Reward_Item_Use;
    [SerializeField] Button m_BTN_MonsterDictionary_DetailInfo_Reward_Item_Use;
    [SerializeField] GameObject m_gBTN_MonsterDictionary_DetailInfo_Reward_Item_Etc;
    [SerializeField] Button m_BTN_MonsterDictionary_DetailInfo_Reward_Item_Etc;
    [SerializeField] GameObject m_gBTN_MonsterDictionary_DetailInfo_Reward_SS;
    [SerializeField] Button m_BTN_MonsterDictionary_DetailInfo_Reward_SS;
    [SerializeField] GameObject m_gSV_MonsterDictionary_DetailInfo_Reward;
    [SerializeField] GameObject m_gViewport_MonsterDictionary_DetailInfo_Reward;
    [SerializeField] GameObject m_gContent_MonsterDictionary_DetailInfo_Reward;
    [SerializeField] Scrollbar m_ScrollBar_MonsterDictionary_DetailInfo_Reward;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_MonsterDictionary_LeftBar;
    [SerializeField] GameObject m_gPanel_MonsterDictionary_LeftBar_BTNBox;
    [SerializeField] Button m_BTN_Human;
    [SerializeField] Button m_BTN_Animal;
    [SerializeField] Button m_BTN_Slime;
    [SerializeField] Button m_BTN_Skeleton;
    [SerializeField] Button m_BTN_Ents;
    [SerializeField] Button m_BTN_Devil;
    [SerializeField] Button m_BTN_Dragon;
    [SerializeField] Button m_BTN_Shadow;
    [SerializeField] Button m_BTN_Object;
    [SerializeField] Image[] m_IMGAry_Sparkle = new Image[9];
    [SerializeField] float m_fMonsterDictionary_Sparkle;
    static Coroutine m_Coroutine_Dictionary_Alarm = null;
    [SerializeField] bool[] m_bAry_Sparkle = new bool[9] { false, false, false, false, false, false, false, false, false };
    [Space(20)]
    [SerializeField] GameObject m_gSV_MonsterDictionary_LeftBar_Content;
    [SerializeField] GameObject m_gViewport_MonsterDictionary_LeftBar_Content;
    [SerializeField] GameObject m_gContent_MonsterDictionary_LeftBar_Content;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_MonsterDictionary_NoneSelect;

    // MonsterDictionary 선택중인 몬스터 분류(카테고리).
    public E_MONSTER_KIND m_eMonster_Kind_Selected;

    // MonsterDictionary 생성된 몬스터 목록 버튼.
    [SerializeField] GameObject m_gBTN_MonsterDictionary_Monster;
    public List<GameObject> m_gList_MonsterDictionary_Monster;
    // monstercode, Image
    public Dictionary<int, Image> m_iIDictionary_MonsterDictionary_Monster_Sparkle;

    // MonsterDictionary 생성된 Panel 목록.
    [SerializeField] GameObject m_gPanel_MonsterDictionary_DetailInfo_Panel;
    [SerializeField] GameObject m_gPanel_MonsterDictionary_DetailInfo_Panel_SS;
    public List<GameObject> m_gList_MonsterDictionary_DetailInfo_Panel;

    // MonsterDictionary 생성된 Reward 목록.
    [SerializeField] GameObject m_gPanel_MonsterDictionary_DetailInfo_RewardPanel;
    public List<GameObject> m_gList_MonsterDictionary_DetailInfo_Reward;

    // MonsterDictionary 갱신된 몬스터 목록 표시.
    // [0]: Monster Kind
    // [1]: Monster Code
    // [2]: MonsterDictionary Release Ratio {1, 25, 50, 75, 100}
    // [3]: User Confirm? { 0, 1 }
    public static List<int[]> m_nList_MonsterDictionary_Sparkle = new List<int[]>();

    public enum E_CATEGORY_REWARD { KILL, GOAWAY }
    public E_CATEGORY_REWARD m_eCategory_Reward;

    bool m_bSelected;

    public void InitialSet()
    {
        InitialSet_Object();
        InitialSet_Button();

        m_gList_MonsterDictionary_Monster = new List<GameObject>();
        m_iIDictionary_MonsterDictionary_Monster_Sparkle = new Dictionary<int, Image>();
        m_gList_MonsterDictionary_DetailInfo_Panel = new List<GameObject>();
        m_gList_MonsterDictionary_DetailInfo_Reward = new List<GameObject>();

        m_bSelected = false;

        Display_GUI_MonsterDictionary();
        //m_gPanel_MonsterDictionary.SetActive(false);
    }

    void InitialSet_Object()
    {
        m_gPanel_MonsterDictionary = GameObject.Find("Canvas_GUI").transform.Find("Panel_MonsterDictionary").gameObject;

        m_gPanel_MonsterDictionary_UpBar = m_gPanel_MonsterDictionary.transform.Find("Panel_MonsterDictionary_UpBar").gameObject;
        m_TMP_MonsterDictionary_UpBar_Name = m_gPanel_MonsterDictionary_UpBar.transform.Find("TMP_MonsterDictionary_UpBar_Name").gameObject.GetComponent<TextMeshProUGUI>();
        m_BTN_MonsterDictionary_UpBar_Exit = m_gPanel_MonsterDictionary_UpBar.transform.Find("BTN_MonsterDictionary_UpBar_Exit").gameObject.GetComponent<Button>();

        m_gPanel_MonsterDictionary_SimpleInfo = m_gPanel_MonsterDictionary.transform.Find("Panel_MonsterDictionary_SimpleInfo").gameObject;
        m_gPanel_MonsterDictionary_SimpleInfo_Image = m_gPanel_MonsterDictionary_SimpleInfo.transform.Find("Panel_MonsterDictionary_SimpleInfo_Image").gameObject;
        m_gPanel_MonsterDictionary_SimpleInfo_Background = m_gPanel_MonsterDictionary_SimpleInfo_Image.transform.Find("Panel_MonsterDictionary_SimpleInfo_Background").gameObject;
        m_gPanel_MonsterDictionary_SimpleInfo_MonsterImage = m_gPanel_MonsterDictionary_SimpleInfo_Image.transform.Find("Panel_MonsterDictionary_SimpleInfo_MonsterImage").gameObject;
        m_IMG_MonsterDictionary_SimpleInfo_MonsterImage = m_gPanel_MonsterDictionary_SimpleInfo_MonsterImage.GetComponent<Image>();

        m_gPanel_MonsterDictionary_SimpleInfo_Info = m_gPanel_MonsterDictionary_SimpleInfo.transform.Find("Panel_MonsterDictionary_SimpleInfo_Info").gameObject;
        m_TMP_MonsterDictionary_SimpleInfo_Info = m_gPanel_MonsterDictionary_SimpleInfo_Info.transform.Find("TMP_MonsterDictionary_SimpleInfo_Info").gameObject.GetComponent<TextMeshProUGUI>();

        m_gPanel_MonsterDictionary_DetailInfo = m_gPanel_MonsterDictionary.transform.Find("Panel_MonsterDictionary_DetailInfo").gameObject;
        m_gPanel_MonsterDictionary_DetailInfo_UpBar = m_gPanel_MonsterDictionary_DetailInfo.transform.Find("Panel_MonsterDictionary_DetailInfo_UpBar").gameObject;
        m_gPanel_MonsterDictionary_DetailInfo_UpBar_Description = m_gPanel_MonsterDictionary_DetailInfo_UpBar.transform.Find("BTN_MonsterDictionary_DetailInfo_UpBar_Description").gameObject;
        m_BTN_MonsterDictionary_DetailInfo_UpBar_Description = m_gPanel_MonsterDictionary_DetailInfo_UpBar_Description.GetComponent<Button>();
        m_gPanel_Description_TMP = m_BTN_MonsterDictionary_DetailInfo_UpBar_Description.transform.Find("TMP").gameObject;
        m_gPanel_Description_Rock = m_BTN_MonsterDictionary_DetailInfo_UpBar_Description.transform.Find("Rock").gameObject;

        m_gPanel_MonsterDictionary_DetailInfo_UpBar_AppearArea = m_gPanel_MonsterDictionary_DetailInfo_UpBar.transform.Find("BTN_MonsterDictionary_DetailInfo_UpBar_AppearArea").gameObject;
        m_BTN_MonsterDictionary_DetailInfo_UpBar_AppearArea = m_gPanel_MonsterDictionary_DetailInfo_UpBar_AppearArea.GetComponent<Button>();
        m_gPanel_AppearArea_TMP = m_BTN_MonsterDictionary_DetailInfo_UpBar_AppearArea.transform.Find("TMP").gameObject;
        m_gPanel_AppearArea_Rock = m_BTN_MonsterDictionary_DetailInfo_UpBar_AppearArea.transform.Find("Rock").gameObject;

        m_gPanel_MonsterDictionary_DetailInfo_UpBar_SS = m_gPanel_MonsterDictionary_DetailInfo_UpBar.transform.Find("BTN_MonsterDictionary_DetailInfo_UpBar_SS").gameObject;
        m_BTN_MonsterDictionary_DetailInfo_UpBar_SS = m_gPanel_MonsterDictionary_DetailInfo_UpBar_SS.GetComponent<Button>();
        m_gPanel_SS_TMP = m_BTN_MonsterDictionary_DetailInfo_UpBar_SS.transform.Find("TMP").gameObject;
        m_gPanel_SS_Rock = m_BTN_MonsterDictionary_DetailInfo_UpBar_SS.transform.Find("Rock").gameObject;

        m_gPanel_MonsterDictionary_DetailInfo_UpBar_Reward = m_gPanel_MonsterDictionary_DetailInfo_UpBar.transform.Find("BTN_MonsterDictionary_DetailInfo_UpBar_Reward").gameObject;
        m_BTN_MonsterDictionary_DetailInfo_UpBar_Reward = m_gPanel_MonsterDictionary_DetailInfo_UpBar_Reward.GetComponent<Button>();
        m_gPanel_Reward_TMP = m_BTN_MonsterDictionary_DetailInfo_UpBar_Reward.transform.Find("TMP").gameObject;
        m_gPanel_Reward_Rock = m_BTN_MonsterDictionary_DetailInfo_UpBar_Reward.transform.Find("Rock").gameObject;

        m_gSV_MonsterDictionary_DetailInfo_Description = m_gPanel_MonsterDictionary_DetailInfo.transform.Find("SV_MonsterDictionary_DetailInfo_Description").gameObject;
        m_gViewport_MonsterDictionary_DetailInfo_Description = m_gSV_MonsterDictionary_DetailInfo_Description.transform.Find("Viewport_MonsterDictionary_DetailInfo_Description").gameObject;
        m_TMP_MonsterDictionary_DetailInfo_Description = m_gViewport_MonsterDictionary_DetailInfo_Description.transform.Find("TMP_MonsterDictionary_DetailInfo_Description").gameObject.GetComponent<TextMeshProUGUI>();
        m_ScrollBar_MonsterDictionary_DetailInfo_Description = m_gSV_MonsterDictionary_DetailInfo_Description.transform.Find("Scrollbar_MonsterDictionary_DetailInfo_Description").gameObject.GetComponent<Scrollbar>();

        m_gSV_MonsterDictionary_DetailInfo_Panel = m_gPanel_MonsterDictionary_DetailInfo.transform.Find("SV_MonsterDictionary_DetailInfo_Panel").gameObject;
        m_gViewport_MonsterDictionary_DetailInfo_Panel = m_gSV_MonsterDictionary_DetailInfo_Panel.transform.Find("Viewport_MonsterDictionary_DetailInfo_Panel").gameObject;
        m_gContent_MonsterDictionary_DetailInfo_Panel = m_gViewport_MonsterDictionary_DetailInfo_Panel.transform.Find("Content_MonsterDictionary_DetailInfo_Panel").gameObject;
        m_ScrollBar_MonsterDictionary_DetailInfo_Panel = m_gSV_MonsterDictionary_DetailInfo_Panel.transform.Find("Scrollbar_MonsterDictionary_DetailInfo_Panel").gameObject.GetComponent<Scrollbar>();

        m_gPanel_MonsterDictionary_DetailInfo_Reward = m_gPanel_MonsterDictionary_DetailInfo.transform.Find("Panel_MonsterDictionary_DetailInfo_Reward").gameObject;
        m_gBTN_MonsterDictionary_DetailInfo_Reward_Death = m_gPanel_MonsterDictionary_DetailInfo_Reward.transform.Find("BTN_MonsterDictionary_DetailInfo_Reward_Death").gameObject;
        m_BTN_MonsterDictionary_DetailInfo_Reward_Death = m_gBTN_MonsterDictionary_DetailInfo_Reward_Death.GetComponent<Button>();
        m_gBTN_MonsterDictionary_DetailInfo_Reward_Goaway = m_gPanel_MonsterDictionary_DetailInfo_Reward.transform.Find("BTN_MonsterDictionary_DetailInfo_Reward_Goaway").gameObject;
        m_BTN_MonsterDictionary_DetailInfo_Reward_Goaway = m_gBTN_MonsterDictionary_DetailInfo_Reward_Goaway.GetComponent<Button>();
        m_gBTN_MonsterDictionary_DetailInfo_Reward_Item_Equip = m_gPanel_MonsterDictionary_DetailInfo_Reward.transform.Find("BTN_MonsterDictionary_DetailInfo_Reward_Item_Equip").gameObject;
        m_BTN_MonsterDictionary_DetailInfo_Reward_Item_Equip = m_gBTN_MonsterDictionary_DetailInfo_Reward_Item_Equip.GetComponent<Button>();
        m_gBTN_MonsterDictionary_DetailInfo_Reward_Item_Use = m_gPanel_MonsterDictionary_DetailInfo_Reward.transform.Find("BTN_MonsterDictionary_DetailInfo_Reward_Item_Use").gameObject;
        m_BTN_MonsterDictionary_DetailInfo_Reward_Item_Use = m_gBTN_MonsterDictionary_DetailInfo_Reward_Item_Use.GetComponent<Button>();
        m_gBTN_MonsterDictionary_DetailInfo_Reward_Item_Etc = m_gPanel_MonsterDictionary_DetailInfo_Reward.transform.Find("BTN_MonsterDictionary_DetailInfo_Reward_Item_Etc").gameObject;
        m_BTN_MonsterDictionary_DetailInfo_Reward_Item_Etc = m_gBTN_MonsterDictionary_DetailInfo_Reward_Item_Etc.GetComponent<Button>();
        m_gBTN_MonsterDictionary_DetailInfo_Reward_SS = m_gPanel_MonsterDictionary_DetailInfo_Reward.transform.Find("BTN_MonsterDictionary_DetailInfo_Reward_SS").gameObject;
        m_BTN_MonsterDictionary_DetailInfo_Reward_SS = m_gBTN_MonsterDictionary_DetailInfo_Reward_SS.GetComponent<Button>();
        m_gSV_MonsterDictionary_DetailInfo_Reward = m_gPanel_MonsterDictionary_DetailInfo_Reward.transform.Find("SV_MonsterDictionary_DetailInfo_Reward").gameObject;
        m_gViewport_MonsterDictionary_DetailInfo_Reward = m_gSV_MonsterDictionary_DetailInfo_Reward.transform.Find("Viewport_MonsterDictionary_DetailInfo_Reward").gameObject;
        m_gContent_MonsterDictionary_DetailInfo_Reward = m_gViewport_MonsterDictionary_DetailInfo_Reward.transform.Find("Content_MonsterDictionary_DetailInfo_Reward").gameObject;
        m_ScrollBar_MonsterDictionary_DetailInfo_Reward = m_gSV_MonsterDictionary_DetailInfo_Reward.transform.Find("Scrollbar_MonsterDictionary_DetailInfo_Reward").gameObject.GetComponent<Scrollbar>();

        m_gPanel_MonsterDictionary_LeftBar = m_gPanel_MonsterDictionary.transform.Find("Panel_MonsterDictionary_LeftBar").gameObject;
        m_gPanel_MonsterDictionary_LeftBar_BTNBox = m_gPanel_MonsterDictionary_LeftBar.transform.Find("Panel_MonsterDictionary_LeftBar_BTNBox").gameObject;
        m_BTN_Human = m_gPanel_MonsterDictionary_LeftBar_BTNBox.transform.Find("BTN_MonsterDictionary_LeftBar_Human").gameObject.GetComponent<Button>();
        m_IMGAry_Sparkle[0] = m_gPanel_MonsterDictionary_LeftBar_BTNBox.transform.Find("BTN_MonsterDictionary_LeftBar_Human").transform.Find("Panel_MonsterDictionary_LeftBar_Sparkle").gameObject.GetComponent<Image>();
        m_BTN_Animal = m_gPanel_MonsterDictionary_LeftBar_BTNBox.transform.Find("BTN_MonsterDictionary_LeftBar_Animal").gameObject.GetComponent<Button>();
        m_IMGAry_Sparkle[1] = m_gPanel_MonsterDictionary_LeftBar_BTNBox.transform.Find("BTN_MonsterDictionary_LeftBar_Animal").transform.Find("Panel_MonsterDictionary_LeftBar_Sparkle").gameObject.GetComponent<Image>();
        m_BTN_Slime = m_gPanel_MonsterDictionary_LeftBar_BTNBox.transform.Find("BTN_MonsterDictionary_LeftBar_Slime").gameObject.GetComponent<Button>();
        m_IMGAry_Sparkle[2] = m_gPanel_MonsterDictionary_LeftBar_BTNBox.transform.Find("BTN_MonsterDictionary_LeftBar_Slime").transform.Find("Panel_MonsterDictionary_LeftBar_Sparkle").gameObject.GetComponent<Image>();
        m_BTN_Skeleton = m_gPanel_MonsterDictionary_LeftBar_BTNBox.transform.Find("BTN_MonsterDictionary_LeftBar_Skeleton").gameObject.GetComponent<Button>();
        m_IMGAry_Sparkle[3] = m_gPanel_MonsterDictionary_LeftBar_BTNBox.transform.Find("BTN_MonsterDictionary_LeftBar_Skeleton").transform.Find("Panel_MonsterDictionary_LeftBar_Sparkle").gameObject.GetComponent<Image>();
        m_BTN_Ents = m_gPanel_MonsterDictionary_LeftBar_BTNBox.transform.Find("BTN_MonsterDictionary_LeftBar_Ents").gameObject.GetComponent<Button>();
        m_IMGAry_Sparkle[4] = m_gPanel_MonsterDictionary_LeftBar_BTNBox.transform.Find("BTN_MonsterDictionary_LeftBar_Ents").transform.Find("Panel_MonsterDictionary_LeftBar_Sparkle").gameObject.GetComponent<Image>();
        m_BTN_Devil = m_gPanel_MonsterDictionary_LeftBar_BTNBox.transform.Find("BTN_MonsterDictionary_LeftBar_Devil").gameObject.GetComponent<Button>();
        m_IMGAry_Sparkle[5] = m_gPanel_MonsterDictionary_LeftBar_BTNBox.transform.Find("BTN_MonsterDictionary_LeftBar_Devil").transform.Find("Panel_MonsterDictionary_LeftBar_Sparkle").gameObject.GetComponent<Image>();
        m_BTN_Dragon = m_gPanel_MonsterDictionary_LeftBar_BTNBox.transform.Find("BTN_MonsterDictionary_LeftBar_Dragon").gameObject.GetComponent<Button>();
        m_IMGAry_Sparkle[6] = m_gPanel_MonsterDictionary_LeftBar_BTNBox.transform.Find("BTN_MonsterDictionary_LeftBar_Dragon").transform.Find("Panel_MonsterDictionary_LeftBar_Sparkle").gameObject.GetComponent<Image>();
        m_BTN_Shadow = m_gPanel_MonsterDictionary_LeftBar_BTNBox.transform.Find("BTN_MonsterDictionary_LeftBar_Shadow").gameObject.GetComponent<Button>();
        m_IMGAry_Sparkle[7] = m_gPanel_MonsterDictionary_LeftBar_BTNBox.transform.Find("BTN_MonsterDictionary_LeftBar_Shadow").transform.Find("Panel_MonsterDictionary_LeftBar_Sparkle").gameObject.GetComponent<Image>();
        m_BTN_Object = m_gPanel_MonsterDictionary_LeftBar_BTNBox.transform.Find("BTN_MonsterDictionary_LeftBar_Object").gameObject.GetComponent<Button>();
        m_IMGAry_Sparkle[8] = m_gPanel_MonsterDictionary_LeftBar_BTNBox.transform.Find("BTN_MonsterDictionary_LeftBar_Object").transform.Find("Panel_MonsterDictionary_LeftBar_Sparkle").gameObject.GetComponent<Image>();

        m_gSV_MonsterDictionary_LeftBar_Content = m_gPanel_MonsterDictionary_LeftBar.transform.Find("SV_MonsterDictionary_LeftBar_Content").gameObject;
        m_gViewport_MonsterDictionary_LeftBar_Content = m_gSV_MonsterDictionary_LeftBar_Content.transform.Find("Viewport_MonsterDictionary_LeftBar_Content").gameObject;
        m_gContent_MonsterDictionary_LeftBar_Content = m_gViewport_MonsterDictionary_LeftBar_Content.transform.Find("Content_MonsterDictionary_LeftBar_Content").gameObject;

        m_gPanel_MonsterDictionary_NoneSelect = m_gPanel_MonsterDictionary.transform.Find("Panel_MonsterDictionary_NoneSelect").gameObject;



        m_gBTN_MonsterDictionary_Monster = Resources.Load("Prefab/GUI/GUI_MonsterDictionary/BTN_MonsterDictionary_Monster") as GameObject;
        m_gPanel_MonsterDictionary_DetailInfo_Panel = Resources.Load("Prefab/GUI/GUI_MonsterDictionary/Panel_MonsterDictionary_DetailInfo_Panel") as GameObject;
        m_gPanel_MonsterDictionary_DetailInfo_Panel_SS = Resources.Load("Prefab/GUI/GUI_MonsterDictionary/Panel_MonsterDictionary_DetailInfo_Panel_SS") as GameObject;

    }
    void InitialSet_Button()
    {
        m_BTN_MonsterDictionary_UpBar_Exit.onClick.RemoveAllListeners();
        m_BTN_MonsterDictionary_UpBar_Exit.onClick.AddListener(delegate { Press_BTN_Exit(); });

        m_BTN_Human.onClick.RemoveAllListeners();
        m_BTN_Human.onClick.AddListener(delegate { Press_BTN_LeftBar_Human(); });
        m_BTN_Animal.onClick.RemoveAllListeners();
        m_BTN_Animal.onClick.AddListener(delegate { Press_BTN_LeftBar_Animal(); });
        m_BTN_Slime.onClick.RemoveAllListeners();
        m_BTN_Slime.onClick.AddListener(delegate { Press_BTN_LeftBar_Slime(); });
        m_BTN_Skeleton.onClick.RemoveAllListeners();
        m_BTN_Skeleton.onClick.AddListener(delegate { Press_BTN_LeftBar_Skeleton(); });
        m_BTN_Ents.onClick.RemoveAllListeners();
        m_BTN_Ents.onClick.AddListener(delegate { Press_BTN_LeftBar_Ents(); });
        m_BTN_Devil.onClick.RemoveAllListeners();
        m_BTN_Devil.onClick.AddListener(delegate { Press_BTN_LeftBar_Devil(); });
        m_BTN_Dragon.onClick.RemoveAllListeners();
        m_BTN_Dragon.onClick.AddListener(delegate { Press_BTN_LeftBar_Dragon(); });
        m_BTN_Shadow.onClick.RemoveAllListeners();
        m_BTN_Shadow.onClick.AddListener(delegate { Press_BTN_LeftBar_Shadow(); });
        m_BTN_Object.onClick.RemoveAllListeners();
        m_BTN_Object.onClick.AddListener(delegate { Press_BTN_LeftBar_Object(); });
    }

    void Press_BTN_Exit()
    {
        Display_GUI_MonsterDictionary();
        GUIManager_Total.Instance.Delete_GUI_Priority(29);
    }

    void Press_BTN_LeftBar_Human()
    {
        m_eMonster_Kind_Selected = E_MONSTER_KIND.HUMAN;
        Update_MonsterDictionary_LeftBar();
        m_BTN_Human.GetComponent<Image>().color = new Color(.75f, .75f, .75f, 1);
        m_BTN_Animal.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Slime.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Skeleton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Ents.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Devil.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Dragon.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Shadow.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Object.GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }
    void Press_BTN_LeftBar_Animal()
    {
        m_eMonster_Kind_Selected = E_MONSTER_KIND.ANIMAL;
        Update_MonsterDictionary_LeftBar();
        m_BTN_Animal.GetComponent<Image>().color = new Color(.75f, .75f, .75f, 1);
        m_BTN_Human.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Slime.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Skeleton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Ents.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Devil.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Dragon.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Shadow.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Object.GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }
    void Press_BTN_LeftBar_Slime()
    {
        m_eMonster_Kind_Selected = E_MONSTER_KIND.SLIME;
        Update_MonsterDictionary_LeftBar();
        m_BTN_Slime.GetComponent<Image>().color = new Color(.75f, .75f, .75f, 1);
        m_BTN_Human.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Animal.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Skeleton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Ents.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Devil.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Dragon.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Shadow.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Object.GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }
    void Press_BTN_LeftBar_Skeleton()
    {
        m_eMonster_Kind_Selected = E_MONSTER_KIND.SKELETON;
        Update_MonsterDictionary_LeftBar();
        m_BTN_Skeleton.GetComponent<Image>().color = new Color(.75f, .75f, .75f, 1);
        m_BTN_Human.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Animal.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Slime.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Ents.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Devil.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Dragon.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Shadow.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Object.GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }
    void Press_BTN_LeftBar_Ents()
    {
        m_eMonster_Kind_Selected = E_MONSTER_KIND.ENTS;
        Update_MonsterDictionary_LeftBar();
        m_BTN_Ents.GetComponent<Image>().color = new Color(.75f, .75f, .75f, 1);
        m_BTN_Human.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Animal.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Slime.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Skeleton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Devil.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Dragon.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Shadow.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Object.GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }
    void Press_BTN_LeftBar_Devil()
    {
        m_eMonster_Kind_Selected = E_MONSTER_KIND.DEVIL;
        Update_MonsterDictionary_LeftBar();
        m_BTN_Devil.GetComponent<Image>().color = new Color(.75f, .75f, .75f, 1);
        m_BTN_Human.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Animal.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Slime.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Skeleton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Ents.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Dragon.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Shadow.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Object.GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }
    void Press_BTN_LeftBar_Dragon()
    {
        m_eMonster_Kind_Selected = E_MONSTER_KIND.DRAGON;
        Update_MonsterDictionary_LeftBar();
        m_BTN_Dragon.GetComponent<Image>().color = new Color(.75f, .75f, .75f, 1);
        m_BTN_Human.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Animal.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Slime.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Skeleton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Ents.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Devil.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Shadow.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Object.GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }
    void Press_BTN_LeftBar_Shadow()
    {
        m_eMonster_Kind_Selected = E_MONSTER_KIND.SHADOW;
        Update_MonsterDictionary_LeftBar();
        m_BTN_Shadow.GetComponent<Image>().color = new Color(.75f, .75f, .75f, 1);
        m_BTN_Human.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Animal.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Slime.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Skeleton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Ents.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Devil.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Dragon.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Object.GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }
    void Press_BTN_LeftBar_Object()
    {
        m_eMonster_Kind_Selected = E_MONSTER_KIND.OBJECT;
        Update_MonsterDictionary_LeftBar();
        m_BTN_Object.GetComponent<Image>().color = new Color(.75f, .75f, .75f, 1);
        m_BTN_Human.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Animal.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Slime.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Skeleton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Ents.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Devil.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Dragon.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_Shadow.GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }

    public bool Display_GUI_MonsterDictionary()
    {
        if (m_gPanel_MonsterDictionary.activeSelf == true)
        {
            m_gPanel_MonsterDictionary.SetActive(false);

            m_bSelected = false;

            m_BTN_Human.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            m_BTN_Animal.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            m_BTN_Slime.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            m_BTN_Skeleton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            m_BTN_Ents.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            m_BTN_Devil.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            m_BTN_Dragon.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            m_BTN_Shadow.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            m_BTN_Object.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            //for (int i = 0; i < m_IMGAry_Sparkle.Length; i++)
            //{
            //    m_IMGAry_Sparkle[i].color = new Color(m_IMGAry_Sparkle[i].color.r, m_IMGAry_Sparkle[i].color.g, m_IMGAry_Sparkle[i].color.b, 0);
            //}

            if (m_Coroutine_Dictionary_Alarm != null)
            {
                m_fMonsterDictionary_Sparkle = 0;

                for (int i = 0; i < m_bAry_Sparkle.Length; i++)
                {
                    m_IMGAry_Sparkle[i].color = new Color(m_IMGAry_Sparkle[i].color.r, m_IMGAry_Sparkle[i].color.g, m_IMGAry_Sparkle[i].color.b, 0);
                }
                StopCoroutine(m_Coroutine_Dictionary_Alarm);
                m_Coroutine_Dictionary_Alarm = null;
            }

            return false;
        }
        else
        {
            for (int i = 0; i < m_nList_MonsterDictionary_Sparkle.Count; i++)
            {
                m_bAry_Sparkle[m_nList_MonsterDictionary_Sparkle[i][0]] = true;
                //Debug.Log("Monster Kind: " + m_nList_MonsterDictionary_Sparkle[i][0]);
                //Debug.Log("Monster Code: " + m_nList_MonsterDictionary_Sparkle[i][1]);
                //Debug.Log("MonsterDictionary Release Ratio: " + m_nList_MonsterDictionary_Sparkle[i][2]);
                //Debug.Log("User Confirm?(Maybe 0): " + m_nList_MonsterDictionary_Sparkle[i][3]);
            }
            
            Update_MonsterDictionary_Init();

            for (int i = 0; i < m_gList_MonsterDictionary_Monster.Count; i++)
            {
                m_gList_MonsterDictionary_Monster[i].GetComponent<Button>().onClick.RemoveAllListeners();
                Destroy(m_gList_MonsterDictionary_Monster[i].gameObject);
            }
            m_gList_MonsterDictionary_Monster.Clear();

            m_gPanel_MonsterDictionary.transform.SetAsLastSibling();
            m_gPanel_MonsterDictionary.SetActive(true);
            if (m_bSelected == false)
            {
                m_gPanel_MonsterDictionary_NoneSelect.transform.SetAsLastSibling();
                m_gPanel_MonsterDictionary_NoneSelect.SetActive(true);
            }
            else
            {
                m_gPanel_MonsterDictionary_NoneSelect.SetActive(false);
                Update_MonsterDictionary_LeftBar();
            }

            //for (int i = 0; i < m_IMGList_MonsterDictionary_Monster_Sparkle.Count; i++)
            //{
            //    m_IMGList_MonsterDictionary_Monster_Sparkle[i].color = new Color(m_IMGList_MonsterDictionary_Monster_Sparkle[i].color.r, m_IMGList_MonsterDictionary_Monster_Sparkle[i].color.g, m_IMGList_MonsterDictionary_Monster_Sparkle[i].color.b, 0);
            //}
            //m_IMGList_MonsterDictionary_Monster_Sparkle.Clear();
            foreach (KeyValuePair<int, Image> items in m_iIDictionary_MonsterDictionary_Monster_Sparkle)
            {
                items.Value.color = new Color(items.Value.color.r, items.Value.color.g, items.Value.color.b, 0);
            }
            m_iIDictionary_MonsterDictionary_Monster_Sparkle.Clear();

            return true;
        }
    }

    // m_nList_MonsterDictionary_Sparkle 를 사용자가 확인 했는지, 안했는지 여부 판단.
    public bool Check_MonsterDictionary_Sparkling(int monstercode)
    {
        bool isit = false;
        for (int i = 0; i < m_nList_MonsterDictionary_Sparkle.Count; i++)
        {
            if (m_nList_MonsterDictionary_Sparkle[i][1] == monstercode)
            {
                m_nList_MonsterDictionary_Sparkle[i][3] = 1;
                m_nList_MonsterDictionary_Sparkle.RemoveAt(i);
                if (m_iIDictionary_MonsterDictionary_Monster_Sparkle.ContainsKey(monstercode) == true)
                {
                    m_iIDictionary_MonsterDictionary_Monster_Sparkle[monstercode].color = new Color(m_iIDictionary_MonsterDictionary_Monster_Sparkle[monstercode].color.r, m_iIDictionary_MonsterDictionary_Monster_Sparkle[monstercode].color.g, m_iIDictionary_MonsterDictionary_Monster_Sparkle[monstercode].color.b, 0);
                    m_iIDictionary_MonsterDictionary_Monster_Sparkle.Remove(monstercode);
                }
                i--;

                isit = true;

            }
        }

        return isit;
    }


    void Update_MonsterDictionary_Init()
    {
        m_bAry_Sparkle = new bool[9] { false, false, false, false, false, false, false, false, false };
        for (int i = 0; i < m_nList_MonsterDictionary_Sparkle.Count; i++)
        {
            m_bAry_Sparkle[m_nList_MonsterDictionary_Sparkle[i][0]] = true;
        }

        for (int i = 0; i < m_bAry_Sparkle.Length; i++)
        {
            m_IMGAry_Sparkle[i].color = new Color(m_IMGAry_Sparkle[i].color.r, m_IMGAry_Sparkle[i].color.g, m_IMGAry_Sparkle[i].color.b, 0);
        }
        if (m_Coroutine_Dictionary_Alarm == null)
            m_Coroutine_Dictionary_Alarm = StartCoroutine(Process_Sparkle_Dictionary_Alarm());
    }
    IEnumerator Process_Sparkle_Dictionary_Alarm()
    {
        bool bSparkle = true;
        while (true)
        {
            if (m_fMonsterDictionary_Sparkle <= 0)
                bSparkle = true;
            else if (m_fMonsterDictionary_Sparkle >= 0.5f)
                bSparkle = false;

            if (bSparkle == true)
                m_fMonsterDictionary_Sparkle += 0.05f;
            else
                m_fMonsterDictionary_Sparkle -= 0.05f;

            for (int i = 0; i < m_bAry_Sparkle.Length; i++)
            {
                if (m_bAry_Sparkle[i] == true)
                {
                    m_IMGAry_Sparkle[i].color = new Color(m_IMGAry_Sparkle[i].color.r, m_IMGAry_Sparkle[i].color.g, m_IMGAry_Sparkle[i].color.b, m_fMonsterDictionary_Sparkle);
                }
                else
                {
                    m_IMGAry_Sparkle[i].color = new Color(m_IMGAry_Sparkle[i].color.r, m_IMGAry_Sparkle[i].color.g, m_IMGAry_Sparkle[i].color.b, 0);
                }
            }
            foreach (KeyValuePair<int, Image> items in m_iIDictionary_MonsterDictionary_Monster_Sparkle)
            {
                items.Value.color = new Color(items.Value.color.r, items.Value.color.g, items.Value.color.b, m_fMonsterDictionary_Sparkle);
            }

            yield return new WaitForSeconds(0.05f);
        }
    }

    public void Update_MonsterDictionary_LeftBar()
    {
        for (int i = 0; i < m_gList_MonsterDictionary_Monster.Count; i++)
        {
            m_gList_MonsterDictionary_Monster[i].GetComponent<Button>().onClick.RemoveAllListeners();
            Destroy(m_gList_MonsterDictionary_Monster[i].gameObject);
        }
        m_gList_MonsterDictionary_Monster.Clear();
        foreach (KeyValuePair<int, Image> items in m_iIDictionary_MonsterDictionary_Monster_Sparkle)
        {
            items.Value.color = new Color(items.Value.color.r, items.Value.color.g, items.Value.color.b, 0);
        }
        m_iIDictionary_MonsterDictionary_Monster_Sparkle.Clear();

        foreach (KeyValuePair<int, MonsterDictionary> dictionary in MonsterManager.m_Dictionary_Monster)
        {
            if (dictionary.Value.m_eMonster_Kind == m_eMonster_Kind_Selected)
            {
                GameObject copybtn = Instantiate(m_gBTN_MonsterDictionary_Monster);

                RectTransform contentpos = copybtn.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_MonsterDictionary_LeftBar_Content.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_MonsterDictionary_Monster.Add(copybtn);
                for (int i = 0; i < m_nList_MonsterDictionary_Sparkle.Count; i++)
                {
                    if (m_nList_MonsterDictionary_Sparkle[i][1] == dictionary.Value.m_nMonster_Code)
                    {
                        m_iIDictionary_MonsterDictionary_Monster_Sparkle.Add(dictionary.Value.m_nMonster_Code, copybtn.GetComponent<BTN_MonsterDictionary_Monster>().m_IMG_MonsterDictionary_Monster_Sparkle);
                        break;
                    }
                }

                copybtn.GetComponent<BTN_MonsterDictionary_Monster>().Update_Btn(dictionary.Value.m_spMonster_Sprite, dictionary.Value.m_nMonster_Code);
            }
        }
    }
    public void Update_MonsterDictionary_Info(int monstercode)
    {
        if (MonsterManager.m_Dictionary_Monster.ContainsKey(monstercode) == true)
        {
            m_gPanel_MonsterDictionary_DetailInfo_UpBar_Description.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            m_gPanel_MonsterDictionary_DetailInfo_UpBar_AppearArea.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            m_gPanel_MonsterDictionary_DetailInfo_UpBar_SS.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            m_gPanel_MonsterDictionary_DetailInfo_UpBar_Reward.GetComponent<Image>().color = new Color(1, 1, 1, 1);

            m_ScrollBar_MonsterDictionary_DetailInfo_Description.value = 1;
            m_ScrollBar_MonsterDictionary_DetailInfo_Panel.value = 1;
            m_ScrollBar_MonsterDictionary_DetailInfo_Reward.value = 1;

            m_gSV_MonsterDictionary_DetailInfo_Description.SetActive(false);
            m_gSV_MonsterDictionary_DetailInfo_Panel.SetActive(false);
            m_gPanel_MonsterDictionary_DetailInfo_Reward.SetActive(false);

            m_bSelected = true;
            m_gPanel_MonsterDictionary_NoneSelect.SetActive(false);

            //Update_MonsterDictionary_Init();
            //m_bAry_Sparkle = new bool[9] { false, false, false, false, false, false, false, false, false };
            //Debug.Log(m_nList_MonsterDictionary_Sparkle.Count);
            //for (int i = 0; i < m_nList_MonsterDictionary_Sparkle.Count; i++)
            //{
            //    m_bAry_Sparkle[m_nList_MonsterDictionary_Sparkle[i][0]] = true;
            //}
            //for (int i = 0; i < m_bAry_Sparkle.Length; i++)
            //{
            //    m_IMGAry_Sparkle[i].color = new Color(m_IMGAry_Sparkle[i].color.r, m_IMGAry_Sparkle[i].color.g, m_IMGAry_Sparkle[i].color.b, 0);
            //}

            md = MonsterManager.m_Dictionary_Monster[monstercode];
            Update_MonsterDictionary_SimpleInfo(monstercode);
            Update_MonsterDictionary_DetailInfo(monstercode);

            Check_MonsterDictionary_Sparkling(monstercode);
            m_bAry_Sparkle = new bool[9] { false, false, false, false, false, false, false, false, false };
            for (int i = 0; i < m_nList_MonsterDictionary_Sparkle.Count; i++)
            {
                m_bAry_Sparkle[m_nList_MonsterDictionary_Sparkle[i][0]] = true;
            }

            for (int i = 0; i < m_bAry_Sparkle.Length; i++)
            {
                m_IMGAry_Sparkle[i].color = new Color(m_IMGAry_Sparkle[i].color.r, m_IMGAry_Sparkle[i].color.g, m_IMGAry_Sparkle[i].color.b, 0);
            }
            if (m_nList_MonsterDictionary_Sparkle.Count == 0)
                GUIManager_Total.Instance.Sparkle_GUI_UpBar_Dictionary_OFF();
        }
    }

    static MonsterDictionary md;
    string str;
    void Update_MonsterDictionary_SimpleInfo(int monstercode)
    {
        m_IMG_MonsterDictionary_SimpleInfo_MonsterImage.sprite = md.m_spMonster_Sprite;

        if (MonsterManager.m_Dictionary_Monster[monstercode].m_nMonster_Dictionary_Solve_Current == 0)
        {
            m_IMG_MonsterDictionary_SimpleInfo_MonsterImage.color = new Color(0, 0, 0, 1);
        }
        else
        {
            if (m_IMG_MonsterDictionary_SimpleInfo_MonsterImage.color.a != 0)
                m_IMG_MonsterDictionary_SimpleInfo_MonsterImage.color = new Color(1, 1, 1, 1);
        }

        m_TMP_MonsterDictionary_SimpleInfo_Info.text = "몬스터 이름: " + md.m_sMonster_Name + "\n";
        switch (md.m_eMonster_Kind)
        {
            case E_MONSTER_KIND.HUMAN:
                {
                    str = "인간";
                } break;
            case E_MONSTER_KIND.ANIMAL:
                {
                    str = "동물";
                } break;
            case E_MONSTER_KIND.SLIME:
                {
                    str = "슬라임";
                } break;
            case E_MONSTER_KIND.SKELETON:
                {
                    str = "스켈레톤";
                } break;
            case E_MONSTER_KIND.ENTS:
                {
                    str = "앤트";
                } break;
            case E_MONSTER_KIND.DEVIL:
                {
                    str = "마족";
                } break;
            case E_MONSTER_KIND.DRAGON:
                {
                    str = "용족";
                } break;
            case E_MONSTER_KIND.SHADOW:
                {
                    str = "어둠";
                } break;
            case E_MONSTER_KIND.OBJECT:
                {
                    str = "기타";
                } break;
            default:
                {
                    str = "???";
                } break;
        }
        m_TMP_MonsterDictionary_SimpleInfo_Info.text += "몬스터 타입: " + str + "\n";
        m_TMP_MonsterDictionary_SimpleInfo_Info.text += "몬스터 등급: " + md.m_eMonster_Grade + "\n";
        m_TMP_MonsterDictionary_SimpleInfo_Info.text += "몬스터 해금: " + md.m_nMonster_Dictionary_Solve_Current.ToString() + " / " + md.m_nMonster_Dictionary_Solve_Max.ToString() + "(" + md.m_fMonster_Dictionary_Solve_Rate.ToString() + " %)\n";
        if (md.m_fMonster_Dictionary_Solve_Rate < 0.25f)
        {
            str = "'" + md.m_sMonster_Name + "' 을(를) 잘 알지 못합니다.";
        }
        else if (md.m_fMonster_Dictionary_Solve_Rate < 0.5f)
        {
            str = "'" + md.m_sMonster_Name + "' 을(를) 어느정도 압니다.";
        }
        else if (md.m_fMonster_Dictionary_Solve_Rate < 0.75f)
        {
            str = "'" + md.m_sMonster_Name + "' 을(를) 많이 압니다.";
        }
        else
        {
            str = "'" + md.m_sMonster_Name + "' 을(를) 완벽히 압니다.";
        }
        //m_TMP_MonsterDictionary_SimpleInfo_Info.text += "[" + str + "]";
    }
    void Update_MonsterDictionary_DetailInfo(int monstercode)
    {
        if (md.m_fMonster_Dictionary_Solve_Rate < 25.0f)
        {
            m_gPanel_Description_TMP.SetActive(false);
            m_gPanel_Description_Rock.SetActive(true);
            m_gPanel_AppearArea_TMP.SetActive(false);
            m_gPanel_AppearArea_Rock.SetActive(true);
            m_gPanel_SS_TMP.SetActive(false);
            m_gPanel_SS_Rock.SetActive(true);
            m_gPanel_Reward_TMP.SetActive(false);
            m_gPanel_Reward_Rock.SetActive(true);
        }
        else if (md.m_fMonster_Dictionary_Solve_Rate < 50.0f)
        {
            m_gPanel_Description_TMP.SetActive(true);
            m_gPanel_Description_Rock.SetActive(false);
            m_gPanel_AppearArea_TMP.SetActive(false);
            m_gPanel_AppearArea_Rock.SetActive(true);
            m_gPanel_SS_TMP.SetActive(false);
            m_gPanel_SS_Rock.SetActive(true);
            m_gPanel_Reward_TMP.SetActive(false);
            m_gPanel_Reward_Rock.SetActive(true);
        }
        else if (md.m_fMonster_Dictionary_Solve_Rate < 75.0f)
        {
            m_gPanel_Description_TMP.SetActive(true);
            m_gPanel_Description_Rock.SetActive(false);
            m_gPanel_AppearArea_TMP.SetActive(true);
            m_gPanel_AppearArea_Rock.SetActive(false);
            m_gPanel_SS_TMP.SetActive(false);
            m_gPanel_SS_Rock.SetActive(true);
            m_gPanel_Reward_TMP.SetActive(false);
            m_gPanel_Reward_Rock.SetActive(true);
        }
        else if (md.m_fMonster_Dictionary_Solve_Rate < 100.0f)
        {
            m_gPanel_Description_TMP.SetActive(true);
            m_gPanel_Description_Rock.SetActive(false);
            m_gPanel_AppearArea_TMP.SetActive(true);
            m_gPanel_AppearArea_Rock.SetActive(false);
            m_gPanel_SS_TMP.SetActive(true);
            m_gPanel_SS_Rock.SetActive(false);
            m_gPanel_Reward_TMP.SetActive(false);
            m_gPanel_Reward_Rock.SetActive(true);
        }
        else
        {
            m_gPanel_Description_TMP.SetActive(true);
            m_gPanel_Description_Rock.SetActive(false);
            m_gPanel_AppearArea_TMP.SetActive(true);
            m_gPanel_AppearArea_Rock.SetActive(false);
            m_gPanel_SS_TMP.SetActive(true);
            m_gPanel_SS_Rock.SetActive(false);
            m_gPanel_Reward_TMP.SetActive(true);
            m_gPanel_Reward_Rock.SetActive(false);
        }

        Set_BTN_DetailInfo(md.m_fMonster_Dictionary_Solve_Rate);
    }
    void Set_BTN_DetailInfo(float solverate)
    {
        m_BTN_MonsterDictionary_DetailInfo_UpBar_Description.onClick.RemoveAllListeners();
        m_BTN_MonsterDictionary_DetailInfo_UpBar_AppearArea.onClick.RemoveAllListeners();
        m_BTN_MonsterDictionary_DetailInfo_UpBar_SS.onClick.RemoveAllListeners();
        m_BTN_MonsterDictionary_DetailInfo_UpBar_Reward.onClick.RemoveAllListeners();
        m_BTN_MonsterDictionary_DetailInfo_Reward_Death.onClick.RemoveAllListeners();
        m_BTN_MonsterDictionary_DetailInfo_Reward_Goaway.onClick.RemoveAllListeners();
        m_BTN_MonsterDictionary_DetailInfo_Reward_Item_Equip.onClick.RemoveAllListeners();
        m_BTN_MonsterDictionary_DetailInfo_Reward_Item_Use.onClick.RemoveAllListeners();
        m_BTN_MonsterDictionary_DetailInfo_Reward_Item_Etc.onClick.RemoveAllListeners();
        m_BTN_MonsterDictionary_DetailInfo_Reward_SS.onClick.RemoveAllListeners();

        float f = solverate;

        if (solverate < 25.0f)
        {

        }
        else if (solverate < 50.0f)
        {
            m_BTN_MonsterDictionary_DetailInfo_UpBar_Description.onClick.AddListener(delegate { Press_BTN_MonsterDictionary_Description(f); });
        }
        else if (solverate < 75.0f)
        {
            m_BTN_MonsterDictionary_DetailInfo_UpBar_Description.onClick.AddListener(delegate { Press_BTN_MonsterDictionary_Description(f); });
            m_BTN_MonsterDictionary_DetailInfo_UpBar_AppearArea.onClick.AddListener(delegate { Press_BTN_MonsterDictionary_AppearArea(); });
        }
        else if (solverate < 100.0f)
        {
            m_BTN_MonsterDictionary_DetailInfo_UpBar_Description.onClick.AddListener(delegate { Press_BTN_MonsterDictionary_Description(f); });
            m_BTN_MonsterDictionary_DetailInfo_UpBar_AppearArea.onClick.AddListener(delegate { Press_BTN_MonsterDictionary_AppearArea(); });
            m_BTN_MonsterDictionary_DetailInfo_UpBar_SS.onClick.AddListener(delegate { Press_BTN_MonsterDictionary_SS(); });
        }
        else
        {
            m_BTN_MonsterDictionary_DetailInfo_UpBar_Description.onClick.AddListener(delegate { Press_BTN_MonsterDictionary_Description(f); });
            m_BTN_MonsterDictionary_DetailInfo_UpBar_AppearArea.onClick.AddListener(delegate { Press_BTN_MonsterDictionary_AppearArea(); });
            m_BTN_MonsterDictionary_DetailInfo_UpBar_SS.onClick.AddListener(delegate { Press_BTN_MonsterDictionary_SS(); });
            m_BTN_MonsterDictionary_DetailInfo_UpBar_Reward.onClick.AddListener(delegate { Press_BTN_MonsterDictionary_Reward(); });
            m_BTN_MonsterDictionary_DetailInfo_Reward_Death.onClick.AddListener(delegate { Press_BTN_MonsterDictionary_Reward_Death(); });
            m_BTN_MonsterDictionary_DetailInfo_Reward_Goaway.onClick.AddListener(delegate { Press_BTN_MonsterDictionary_Reward_Goaway(); });

            m_BTN_MonsterDictionary_DetailInfo_Reward_Item_Equip.onClick.AddListener(delegate { Press_BTN_MonsterDictionary_Reward_Item_Equip(); });
            m_BTN_MonsterDictionary_DetailInfo_Reward_Item_Use.onClick.AddListener(delegate { Press_BTN_MonsterDictionary_Reward_Item_Use(); });
            m_BTN_MonsterDictionary_DetailInfo_Reward_Item_Etc.onClick.AddListener(delegate { Press_BTN_MonsterDictionary_Reward_Item_Etc(); });
            m_BTN_MonsterDictionary_DetailInfo_Reward_SS.onClick.AddListener(delegate { Press_BTN_MonsterDictionary_Reward_SS(); });
        }
    }
    void Press_BTN_MonsterDictionary_Description(float solverate)
    {
        m_gPanel_MonsterDictionary_DetailInfo_UpBar_Description.GetComponent<Image>().color = new Color(0.75f, 0.75f, 0.75f, 1);
        m_gPanel_MonsterDictionary_DetailInfo_UpBar_AppearArea.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_gPanel_MonsterDictionary_DetailInfo_UpBar_SS.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_gPanel_MonsterDictionary_DetailInfo_UpBar_Reward.GetComponent<Image>().color = new Color(1, 1, 1, 1);

        m_gSV_MonsterDictionary_DetailInfo_Description.SetActive(true);
        m_gSV_MonsterDictionary_DetailInfo_Panel.SetActive(false);
        m_gPanel_MonsterDictionary_DetailInfo_Reward.SetActive(false);

        m_gBTN_MonsterDictionary_DetailInfo_Reward_Item_Equip.SetActive(false);
        m_gBTN_MonsterDictionary_DetailInfo_Reward_Item_Use.SetActive(false);
        m_gBTN_MonsterDictionary_DetailInfo_Reward_Item_Etc.SetActive(false);
        m_gBTN_MonsterDictionary_DetailInfo_Reward_SS.SetActive(false);

        if (solverate < 25.0f)
        {
            m_TMP_MonsterDictionary_DetailInfo_Description.text = "";
        }
        else if (solverate < 50.0f)
        {
            m_TMP_MonsterDictionary_DetailInfo_Description.text = "[25%]\n";
            m_TMP_MonsterDictionary_DetailInfo_Description.text += md.m_sMonster_Dictionary_Description_25P;
        }
        else if (solverate < 75.0f)
        {
            m_TMP_MonsterDictionary_DetailInfo_Description.text = "[25%]\n";
            m_TMP_MonsterDictionary_DetailInfo_Description.text += md.m_sMonster_Dictionary_Description_25P;
            m_TMP_MonsterDictionary_DetailInfo_Description.text += "\n\n";
            m_TMP_MonsterDictionary_DetailInfo_Description.text += "[50%]\n";
            m_TMP_MonsterDictionary_DetailInfo_Description.text += md.m_sMonster_Dictionary_Description_50P;
        }
        else
        {
            m_TMP_MonsterDictionary_DetailInfo_Description.text = "[25%]\n";
            m_TMP_MonsterDictionary_DetailInfo_Description.text += md.m_sMonster_Dictionary_Description_25P;
            m_TMP_MonsterDictionary_DetailInfo_Description.text += "\n\n";
            m_TMP_MonsterDictionary_DetailInfo_Description.text += "[50%]\n";
            m_TMP_MonsterDictionary_DetailInfo_Description.text += md.m_sMonster_Dictionary_Description_50P;
            m_TMP_MonsterDictionary_DetailInfo_Description.text += "\n\n";
            m_TMP_MonsterDictionary_DetailInfo_Description.text += "[75%]\n";
            m_TMP_MonsterDictionary_DetailInfo_Description.text += md.m_sMonster_Dictionary_Description_75P;
        }
    }
    void Press_BTN_MonsterDictionary_AppearArea()
    {
        m_gPanel_MonsterDictionary_DetailInfo_UpBar_Description.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_gPanel_MonsterDictionary_DetailInfo_UpBar_AppearArea.GetComponent<Image>().color = new Color(0.75f, 0.75f, 0.75f, 1);
        m_gPanel_MonsterDictionary_DetailInfo_UpBar_SS.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_gPanel_MonsterDictionary_DetailInfo_UpBar_Reward.GetComponent<Image>().color = new Color(1, 1, 1, 1);

        m_gSV_MonsterDictionary_DetailInfo_Description.SetActive(false);
        m_gSV_MonsterDictionary_DetailInfo_Panel.SetActive(true);
        m_gPanel_MonsterDictionary_DetailInfo_Reward.SetActive(false);

        m_gBTN_MonsterDictionary_DetailInfo_Reward_Item_Equip.SetActive(false);
        m_gBTN_MonsterDictionary_DetailInfo_Reward_Item_Use.SetActive(false);
        m_gBTN_MonsterDictionary_DetailInfo_Reward_Item_Etc.SetActive(false);
        m_gBTN_MonsterDictionary_DetailInfo_Reward_SS.SetActive(false);

        for (int i = 0; i < m_gList_MonsterDictionary_DetailInfo_Panel.Count; i++)
        {
            Destroy(m_gList_MonsterDictionary_DetailInfo_Panel[i].gameObject);
        }
        m_gList_MonsterDictionary_DetailInfo_Panel.Clear();

        for (int i = 0; i < md.m_slMonster_Dictionary_AppearArea_50P.Count; i++)
        {
            GameObject copybtn = Instantiate(m_gPanel_MonsterDictionary_DetailInfo_Panel);
            copybtn.GetComponent<Panel_MonsterDictionary>().Set_AppearArea(md.m_slMonster_Dictionary_AppearArea_50P[i]);

            RectTransform contentpos = copybtn.GetComponent<RectTransform>();
            contentpos.SetParent(m_gContent_MonsterDictionary_DetailInfo_Panel.transform);
            contentpos.transform.localScale = new Vector3(1, 1, 1);
            contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

            m_gList_MonsterDictionary_DetailInfo_Panel.Add(copybtn);
        }
    }
    void Press_BTN_MonsterDictionary_SS()
    {
        m_gPanel_MonsterDictionary_DetailInfo_UpBar_Description.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_gPanel_MonsterDictionary_DetailInfo_UpBar_AppearArea.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_gPanel_MonsterDictionary_DetailInfo_UpBar_SS.GetComponent<Image>().color = new Color(0.75f, 0.75f, 0.75f, 1);
        m_gPanel_MonsterDictionary_DetailInfo_UpBar_Reward.GetComponent<Image>().color = new Color(1, 1, 1, 1);

        m_gSV_MonsterDictionary_DetailInfo_Description.SetActive(false);
        m_gSV_MonsterDictionary_DetailInfo_Panel.SetActive(true);
        m_gPanel_MonsterDictionary_DetailInfo_Reward.SetActive(false);

        m_gBTN_MonsterDictionary_DetailInfo_Reward_Item_Equip.SetActive(false);
        m_gBTN_MonsterDictionary_DetailInfo_Reward_Item_Use.SetActive(false);
        m_gBTN_MonsterDictionary_DetailInfo_Reward_Item_Etc.SetActive(false);
        m_gBTN_MonsterDictionary_DetailInfo_Reward_SS.SetActive(false);

        for (int i = 0; i < m_gList_MonsterDictionary_DetailInfo_Panel.Count; i++)
        {
            Destroy(m_gList_MonsterDictionary_DetailInfo_Panel[i].gameObject);
        }
        m_gList_MonsterDictionary_DetailInfo_Panel.Clear();


        GameObject copybtn = Instantiate(m_gPanel_MonsterDictionary_DetailInfo_Panel_SS);
        copybtn.GetComponent<Panel_MonsterDictionary_SS>().Set_STATUS(md.m_SMonster_Dictionary_STATUS, 1);

        RectTransform contentpos = copybtn.GetComponent<RectTransform>();
        contentpos.SetParent(m_gContent_MonsterDictionary_DetailInfo_Panel.transform);
        contentpos.transform.localScale = new Vector3(1, 1, 1);
        contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

        m_gList_MonsterDictionary_DetailInfo_Panel.Add(copybtn);


        copybtn = Instantiate(m_gPanel_MonsterDictionary_DetailInfo_Panel_SS);
        copybtn.GetComponent<Panel_MonsterDictionary_SS>().Set_STATUS(md.m_SMonster_Dictionary_STATUS, 2);

        contentpos = copybtn.GetComponent<RectTransform>();
        contentpos.SetParent(m_gContent_MonsterDictionary_DetailInfo_Panel.transform);
        contentpos.transform.localScale = new Vector3(1, 1, 1);
        contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

        m_gList_MonsterDictionary_DetailInfo_Panel.Add(copybtn);


        copybtn = Instantiate(m_gPanel_MonsterDictionary_DetailInfo_Panel_SS);
        copybtn.GetComponent<Panel_MonsterDictionary_SS>().Set_STATUS(md.m_SMonster_Dictionary_STATUS, 3);

        contentpos = copybtn.GetComponent<RectTransform>();
        contentpos.SetParent(m_gContent_MonsterDictionary_DetailInfo_Panel.transform);
        contentpos.transform.localScale = new Vector3(1, 1, 1);
        contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

        m_gList_MonsterDictionary_DetailInfo_Panel.Add(copybtn);


        copybtn = Instantiate(m_gPanel_MonsterDictionary_DetailInfo_Panel_SS);
        copybtn.GetComponent<Panel_MonsterDictionary_SS>().Set_STATUS(md.m_SMonster_Dictionary_STATUS, 4);

        contentpos = copybtn.GetComponent<RectTransform>();
        contentpos.SetParent(m_gContent_MonsterDictionary_DetailInfo_Panel.transform);
        contentpos.transform.localScale = new Vector3(1, 1, 1);
        contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

        m_gList_MonsterDictionary_DetailInfo_Panel.Add(copybtn);
    }
    void Press_BTN_MonsterDictionary_Reward()
    {
        m_gPanel_MonsterDictionary_DetailInfo_UpBar_Description.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_gPanel_MonsterDictionary_DetailInfo_UpBar_AppearArea.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_gPanel_MonsterDictionary_DetailInfo_UpBar_SS.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        m_gPanel_MonsterDictionary_DetailInfo_UpBar_Reward.GetComponent<Image>().color = new Color(0.75f, 0.75f, 0.75f, 1);

        m_gSV_MonsterDictionary_DetailInfo_Description.SetActive(false);
        m_gSV_MonsterDictionary_DetailInfo_Panel.SetActive(false);
        m_gPanel_MonsterDictionary_DetailInfo_Reward.SetActive(true);
        m_gSV_MonsterDictionary_DetailInfo_Reward.SetActive(false);

        m_gBTN_MonsterDictionary_DetailInfo_Reward_Item_Equip.SetActive(false);
        m_gBTN_MonsterDictionary_DetailInfo_Reward_Item_Use.SetActive(false);
        m_gBTN_MonsterDictionary_DetailInfo_Reward_Item_Etc.SetActive(false);
        m_gBTN_MonsterDictionary_DetailInfo_Reward_SS.SetActive(false);

        m_BTN_MonsterDictionary_DetailInfo_Reward_Death.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_MonsterDictionary_DetailInfo_Reward_Goaway.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);

        m_BTN_MonsterDictionary_DetailInfo_Reward_Item_Equip.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_MonsterDictionary_DetailInfo_Reward_Item_Use.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_MonsterDictionary_DetailInfo_Reward_Item_Etc.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_MonsterDictionary_DetailInfo_Reward_SS.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
    }
    void Press_BTN_MonsterDictionary_Reward_Death()
    {
        m_eCategory_Reward = E_CATEGORY_REWARD.KILL;

        m_BTN_MonsterDictionary_DetailInfo_Reward_Death.GetComponentInChildren<Image>().color = new Color(0.75f, 0.75f, 0.75f, 1);
        m_BTN_MonsterDictionary_DetailInfo_Reward_Goaway.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);

        m_gBTN_MonsterDictionary_DetailInfo_Reward_Item_Equip.SetActive(true);
        m_BTN_MonsterDictionary_DetailInfo_Reward_Item_Equip.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
        m_gBTN_MonsterDictionary_DetailInfo_Reward_Item_Use.SetActive(true);
        m_BTN_MonsterDictionary_DetailInfo_Reward_Item_Use.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
        m_gBTN_MonsterDictionary_DetailInfo_Reward_Item_Etc.SetActive(true);
        m_BTN_MonsterDictionary_DetailInfo_Reward_Item_Etc.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
        m_gBTN_MonsterDictionary_DetailInfo_Reward_SS.SetActive(true);
        m_BTN_MonsterDictionary_DetailInfo_Reward_SS.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);

        m_gSV_MonsterDictionary_DetailInfo_Reward.SetActive(false);
    }
    void Press_BTN_MonsterDictionary_Reward_Goaway()
    {
        m_eCategory_Reward = E_CATEGORY_REWARD.GOAWAY;

        m_BTN_MonsterDictionary_DetailInfo_Reward_Death.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_MonsterDictionary_DetailInfo_Reward_Goaway.GetComponentInChildren<Image>().color = new Color(0.75f, 0.75f, 0.75f, 1);

        m_gBTN_MonsterDictionary_DetailInfo_Reward_Item_Equip.SetActive(true);
        m_BTN_MonsterDictionary_DetailInfo_Reward_Item_Equip.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
        m_gBTN_MonsterDictionary_DetailInfo_Reward_Item_Use.SetActive(true);
        m_BTN_MonsterDictionary_DetailInfo_Reward_Item_Use.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
        m_gBTN_MonsterDictionary_DetailInfo_Reward_Item_Etc.SetActive(true);
        m_BTN_MonsterDictionary_DetailInfo_Reward_Item_Etc.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
        m_gBTN_MonsterDictionary_DetailInfo_Reward_SS.SetActive(true);
        m_BTN_MonsterDictionary_DetailInfo_Reward_SS.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);

        m_gSV_MonsterDictionary_DetailInfo_Reward.SetActive(false);
    }

    void Press_BTN_MonsterDictionary_Reward_Item_Equip()
    {
        m_BTN_MonsterDictionary_DetailInfo_Reward_Item_Equip.GetComponentInChildren<Image>().color = new Color(0.75f, 0.75f, 0.75f, 1);
        m_BTN_MonsterDictionary_DetailInfo_Reward_Item_Use.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_MonsterDictionary_DetailInfo_Reward_Item_Etc.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_MonsterDictionary_DetailInfo_Reward_SS.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);

        if (m_eCategory_Reward == E_CATEGORY_REWARD.KILL)
        {
            m_gSV_MonsterDictionary_DetailInfo_Reward.SetActive(true);
            m_ScrollBar_MonsterDictionary_DetailInfo_Reward.value = 1;

            for (int i = 0; i < m_gList_MonsterDictionary_DetailInfo_Reward.Count; i++)
            {
                Destroy(m_gList_MonsterDictionary_DetailInfo_Reward[i].gameObject);
            }
            m_gList_MonsterDictionary_DetailInfo_Reward.Clear();

            GameObject copybtn; RectTransform contentpos;

            if (md.m_nMonster_Death_Reward_Gold_Max != 0)
            {
                copybtn = Instantiate(m_gPanel_MonsterDictionary_DetailInfo_Panel);
                copybtn.GetComponent<Panel_MonsterDictionary>().Set_Gold(md.m_nMonster_Death_Reward_Gold_Min, md.m_nMonster_Death_Reward_Gold_Max, md.m_nMonster_Death_Reward_Gold_Count, md.m_nMonster_Death_Reward_Gold_DropRate);

                contentpos = copybtn.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_MonsterDictionary_DetailInfo_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_MonsterDictionary_DetailInfo_Reward.Add(copybtn);
            }

            for (int i = 0; i < md.m_nlMonster_Dictionary_Death_Reward_Item_Equip.Count; i++)
            {
                copybtn = Instantiate(m_gPanel_MonsterDictionary_DetailInfo_Panel);
                copybtn.GetComponent<Panel_MonsterDictionary>().Set_Item(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[md.m_nlMonster_Dictionary_Death_Reward_Item_Equip[i]], md.m_nlMonster_Dictionary_Death_Reward_Item_Equip_Count[i], md.m_nlMonster_Dictionary_Death_Reward_Item_Equip_DropRate[i]);

                contentpos = copybtn.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_MonsterDictionary_DetailInfo_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_MonsterDictionary_DetailInfo_Reward.Add(copybtn);
            }
        }
        else
        {
            m_gSV_MonsterDictionary_DetailInfo_Reward.SetActive(true);
            m_ScrollBar_MonsterDictionary_DetailInfo_Reward.value = 1;

            for (int i = 0; i < m_gList_MonsterDictionary_DetailInfo_Reward.Count; i++)
            {
                Destroy(m_gList_MonsterDictionary_DetailInfo_Reward[i].gameObject);
            }
            m_gList_MonsterDictionary_DetailInfo_Reward.Clear();

            GameObject copybtn; RectTransform contentpos;

            if (md.m_nMonster_Goaway_Reward_Gold_Max != 0)
            {
                copybtn = Instantiate(m_gPanel_MonsterDictionary_DetailInfo_Panel);
                copybtn.GetComponent<Panel_MonsterDictionary>().Set_Gold(md.m_nMonster_Goaway_Reward_Gold_Min, md.m_nMonster_Goaway_Reward_Gold_Max, md.m_nMonster_Goaway_Reward_Gold_Count, md.m_nMonster_Goaway_Reward_Gold_DropRate);

                contentpos = copybtn.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_MonsterDictionary_DetailInfo_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_MonsterDictionary_DetailInfo_Reward.Add(copybtn);
            }

            for (int i = 0; i < md.m_nlMonster_Dictionary_Goaway_Reward_Item_Equip.Count; i++)
            {
                copybtn = Instantiate(m_gPanel_MonsterDictionary_DetailInfo_Panel);
                copybtn.GetComponent<Panel_MonsterDictionary>().Set_Item(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[md.m_nlMonster_Dictionary_Goaway_Reward_Item_Equip[i]], md.m_nlMonster_Dictionary_Goaway_Reward_Item_Equip_Count[i], md.m_nlMonster_Dictionary_Goaway_Reward_Item_Equip_DropRate[i]);

                contentpos = copybtn.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_MonsterDictionary_DetailInfo_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_MonsterDictionary_DetailInfo_Reward.Add(copybtn);
            }
        }
    }
    void Press_BTN_MonsterDictionary_Reward_Item_Use()
    {
        m_BTN_MonsterDictionary_DetailInfo_Reward_Item_Equip.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_MonsterDictionary_DetailInfo_Reward_Item_Use.GetComponentInChildren<Image>().color = new Color(0.75f, 0.75f, 0.75f, 1);
        m_BTN_MonsterDictionary_DetailInfo_Reward_Item_Etc.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_MonsterDictionary_DetailInfo_Reward_SS.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);

        if (m_eCategory_Reward == E_CATEGORY_REWARD.KILL)
        {
            m_gSV_MonsterDictionary_DetailInfo_Reward.SetActive(true);
            m_ScrollBar_MonsterDictionary_DetailInfo_Reward.value = 1;

            for (int i = 0; i < m_gList_MonsterDictionary_DetailInfo_Reward.Count; i++)
            {
                Destroy(m_gList_MonsterDictionary_DetailInfo_Reward[i].gameObject);
            }
            m_gList_MonsterDictionary_DetailInfo_Reward.Clear();

            GameObject copybtn; RectTransform contentpos;

            if (md.m_nMonster_Death_Reward_Gold_Max != 0)
            {
                copybtn = Instantiate(m_gPanel_MonsterDictionary_DetailInfo_Panel);
                copybtn.GetComponent<Panel_MonsterDictionary>().Set_Gold(md.m_nMonster_Death_Reward_Gold_Min, md.m_nMonster_Death_Reward_Gold_Max, md.m_nMonster_Death_Reward_Gold_Count, md.m_nMonster_Death_Reward_Gold_DropRate);

                contentpos = copybtn.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_MonsterDictionary_DetailInfo_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_MonsterDictionary_DetailInfo_Reward.Add(copybtn);
            }

            for (int i = 0; i < md.m_nlMonster_Dictionary_Death_Reward_Item_Use.Count; i++)
            {
                copybtn = Instantiate(m_gPanel_MonsterDictionary_DetailInfo_Panel);
                copybtn.GetComponent<Panel_MonsterDictionary>().Set_Item(ItemManager.instance.m_Dictionary_MonsterDrop_Use[md.m_nlMonster_Dictionary_Death_Reward_Item_Use[i]], md.m_nlMonster_Dictionary_Death_Reward_Item_Use_Count[i], md.m_nlMonster_Dictionary_Death_Reward_Item_Use_DropRate[i]);

                contentpos = copybtn.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_MonsterDictionary_DetailInfo_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_MonsterDictionary_DetailInfo_Reward.Add(copybtn);
            }
        }
        else
        {
            m_gSV_MonsterDictionary_DetailInfo_Reward.SetActive(true);
            m_ScrollBar_MonsterDictionary_DetailInfo_Reward.value = 1;

            for (int i = 0; i < m_gList_MonsterDictionary_DetailInfo_Reward.Count; i++)
            {
                Destroy(m_gList_MonsterDictionary_DetailInfo_Reward[i].gameObject);
            }
            m_gList_MonsterDictionary_DetailInfo_Reward.Clear();

            GameObject copybtn; RectTransform contentpos;

            if (md.m_nMonster_Goaway_Reward_Gold_Max != 0)
            {
                copybtn = Instantiate(m_gPanel_MonsterDictionary_DetailInfo_Panel);
                copybtn.GetComponent<Panel_MonsterDictionary>().Set_Gold(md.m_nMonster_Goaway_Reward_Gold_Min, md.m_nMonster_Goaway_Reward_Gold_Max, md.m_nMonster_Goaway_Reward_Gold_Count, md.m_nMonster_Goaway_Reward_Gold_DropRate);

                contentpos = copybtn.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_MonsterDictionary_DetailInfo_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_MonsterDictionary_DetailInfo_Reward.Add(copybtn);
            }

            for (int i = 0; i < md.m_nlMonster_Dictionary_Goaway_Reward_Item_Use.Count; i++)
            {
                copybtn = Instantiate(m_gPanel_MonsterDictionary_DetailInfo_Panel);
                copybtn.GetComponent<Panel_MonsterDictionary>().Set_Item(ItemManager.instance.m_Dictionary_MonsterDrop_Use[md.m_nlMonster_Dictionary_Goaway_Reward_Item_Use[i]], md.m_nlMonster_Dictionary_Goaway_Reward_Item_Use_Count[i], md.m_nlMonster_Dictionary_Goaway_Reward_Item_Use_DropRate[i]);

                contentpos = copybtn.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_MonsterDictionary_DetailInfo_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_MonsterDictionary_DetailInfo_Reward.Add(copybtn);
            }
        }
    }
    void Press_BTN_MonsterDictionary_Reward_Item_Etc()
    {
        m_BTN_MonsterDictionary_DetailInfo_Reward_Item_Equip.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_MonsterDictionary_DetailInfo_Reward_Item_Use.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_MonsterDictionary_DetailInfo_Reward_Item_Etc.GetComponentInChildren<Image>().color = new Color(0.75f, 0.75f, 0.75f, 1);
        m_BTN_MonsterDictionary_DetailInfo_Reward_SS.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);

        if (m_eCategory_Reward == E_CATEGORY_REWARD.KILL)
        {
            m_gSV_MonsterDictionary_DetailInfo_Reward.SetActive(true);
            m_ScrollBar_MonsterDictionary_DetailInfo_Reward.value = 1;

            for (int i = 0; i < m_gList_MonsterDictionary_DetailInfo_Reward.Count; i++)
            {
                Destroy(m_gList_MonsterDictionary_DetailInfo_Reward[i].gameObject);
            }
            m_gList_MonsterDictionary_DetailInfo_Reward.Clear();

            GameObject copybtn; RectTransform contentpos;

            if (md.m_nMonster_Death_Reward_Gold_Max != 0)
            {
                copybtn = Instantiate(m_gPanel_MonsterDictionary_DetailInfo_Panel);
                copybtn.GetComponent<Panel_MonsterDictionary>().Set_Gold(md.m_nMonster_Death_Reward_Gold_Min, md.m_nMonster_Death_Reward_Gold_Max, md.m_nMonster_Death_Reward_Gold_Count, md.m_nMonster_Death_Reward_Gold_DropRate);

                contentpos = copybtn.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_MonsterDictionary_DetailInfo_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_MonsterDictionary_DetailInfo_Reward.Add(copybtn);
            }

            for (int i = 0; i < md.m_nlMonster_Dictionary_Death_Reward_Item_Etc.Count; i++)
            {
                copybtn = Instantiate(m_gPanel_MonsterDictionary_DetailInfo_Panel);
                copybtn.GetComponent<Panel_MonsterDictionary>().Set_Item(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[md.m_nlMonster_Dictionary_Death_Reward_Item_Etc[i]], md.m_nlMonster_Dictionary_Death_Reward_Item_Etc_Count[i], md.m_nlMonster_Dictionary_Death_Reward_Item_Etc_DropRate[i]);

                contentpos = copybtn.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_MonsterDictionary_DetailInfo_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_MonsterDictionary_DetailInfo_Reward.Add(copybtn);
            }
        }
        else
        {
            m_gSV_MonsterDictionary_DetailInfo_Reward.SetActive(true);
            m_ScrollBar_MonsterDictionary_DetailInfo_Reward.value = 1;

            for (int i = 0; i < m_gList_MonsterDictionary_DetailInfo_Reward.Count; i++)
            {
                Destroy(m_gList_MonsterDictionary_DetailInfo_Reward[i].gameObject);
            }
            m_gList_MonsterDictionary_DetailInfo_Reward.Clear();

            GameObject copybtn; RectTransform contentpos;

            if (md.m_nMonster_Goaway_Reward_Gold_Max != 0)
            {
                copybtn = Instantiate(m_gPanel_MonsterDictionary_DetailInfo_Panel);
                copybtn.GetComponent<Panel_MonsterDictionary>().Set_Gold(md.m_nMonster_Goaway_Reward_Gold_Min, md.m_nMonster_Goaway_Reward_Gold_Max, md.m_nMonster_Goaway_Reward_Gold_Count, md.m_nMonster_Goaway_Reward_Gold_DropRate);

                contentpos = copybtn.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_MonsterDictionary_DetailInfo_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_MonsterDictionary_DetailInfo_Reward.Add(copybtn);
            }

            for (int i = 0; i < md.m_nlMonster_Dictionary_Goaway_Reward_Item_Etc.Count; i++)
            {
                copybtn = Instantiate(m_gPanel_MonsterDictionary_DetailInfo_Panel);
                copybtn.GetComponent<Panel_MonsterDictionary>().Set_Item(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[md.m_nlMonster_Dictionary_Goaway_Reward_Item_Etc[i]], md.m_nlMonster_Dictionary_Goaway_Reward_Item_Etc_Count[i], md.m_nlMonster_Dictionary_Goaway_Reward_Item_Etc_DropRate[i]);

                contentpos = copybtn.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_MonsterDictionary_DetailInfo_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_MonsterDictionary_DetailInfo_Reward.Add(copybtn);
            }
        }
    }
    void Press_BTN_MonsterDictionary_Reward_SS()
    {
        m_BTN_MonsterDictionary_DetailInfo_Reward_Item_Equip.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_MonsterDictionary_DetailInfo_Reward_Item_Use.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_MonsterDictionary_DetailInfo_Reward_Item_Etc.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
        m_BTN_MonsterDictionary_DetailInfo_Reward_SS.GetComponentInChildren<Image>().color = new Color(0.75f, 0.75f, 0.75f, 1);

        if (m_eCategory_Reward == E_CATEGORY_REWARD.KILL)
        {
            m_gSV_MonsterDictionary_DetailInfo_Reward.SetActive(true);
            m_ScrollBar_MonsterDictionary_DetailInfo_Reward.value = 1;

            for (int i = 0; i < m_gList_MonsterDictionary_DetailInfo_Reward.Count; i++)
            {
                Destroy(m_gList_MonsterDictionary_DetailInfo_Reward[i].gameObject);
            }
            m_gList_MonsterDictionary_DetailInfo_Reward.Clear();

            GameObject copybtn; RectTransform contentpos;

            if (md.m_SMonster_Death_Reward_STATUS.GetSTATUS_EXP_Current() != 0)
            {
                copybtn = Instantiate(m_gPanel_MonsterDictionary_DetailInfo_Panel_SS);
                copybtn.GetComponent<Panel_MonsterDictionary_SS>().Set_Reward_STATUS(md.m_SMonster_Death_Reward_STATUS);

                contentpos = copybtn.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_MonsterDictionary_DetailInfo_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_MonsterDictionary_DetailInfo_Reward.Add(copybtn);
            }

            if (md.m_SMonster_Death_Reward_SOC.GetSOC_Honor() != 0)
            {
                copybtn = Instantiate(m_gPanel_MonsterDictionary_DetailInfo_Panel_SS);
                copybtn.GetComponent<Panel_MonsterDictionary_SS>().Set_Reward_SOC("명        예: ", md.m_SMonster_Death_Reward_SOC.GetSOC_Honor());

                contentpos = copybtn.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_MonsterDictionary_DetailInfo_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_MonsterDictionary_DetailInfo_Reward.Add(copybtn);
            }
            if (md.m_SMonster_Death_Reward_SOC.GetSOC_Human() != 0)
            {
                copybtn = Instantiate(m_gPanel_MonsterDictionary_DetailInfo_Panel_SS);
                copybtn.GetComponent<Panel_MonsterDictionary_SS>().Set_Reward_SOC("인        간: ", md.m_SMonster_Death_Reward_SOC.GetSOC_Human());

                contentpos = copybtn.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_MonsterDictionary_DetailInfo_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_MonsterDictionary_DetailInfo_Reward.Add(copybtn);
            }
            if (md.m_SMonster_Death_Reward_SOC.GetSOC_Animal() != 0)
            {
                copybtn = Instantiate(m_gPanel_MonsterDictionary_DetailInfo_Panel_SS);
                copybtn.GetComponent<Panel_MonsterDictionary_SS>().Set_Reward_SOC("동        물: ", md.m_SMonster_Death_Reward_SOC.GetSOC_Animal());

                contentpos = copybtn.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_MonsterDictionary_DetailInfo_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_MonsterDictionary_DetailInfo_Reward.Add(copybtn);
            }
            if (md.m_SMonster_Death_Reward_SOC.GetSOC_Slime() != 0)
            {
                copybtn = Instantiate(m_gPanel_MonsterDictionary_DetailInfo_Panel_SS);
                copybtn.GetComponent<Panel_MonsterDictionary_SS>().Set_Reward_SOC("슬  라  임: ", md.m_SMonster_Death_Reward_SOC.GetSOC_Slime());

                contentpos = copybtn.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_MonsterDictionary_DetailInfo_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_MonsterDictionary_DetailInfo_Reward.Add(copybtn);
            }
            if (md.m_SMonster_Death_Reward_SOC.GetSOC_Skeleton() != 0)
            {
                copybtn = Instantiate(m_gPanel_MonsterDictionary_DetailInfo_Panel_SS);
                copybtn.GetComponent<Panel_MonsterDictionary_SS>().Set_Reward_SOC("스켈레톤: ", md.m_SMonster_Death_Reward_SOC.GetSOC_Skeleton());

                contentpos = copybtn.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_MonsterDictionary_DetailInfo_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_MonsterDictionary_DetailInfo_Reward.Add(copybtn);
            }
            if (md.m_SMonster_Death_Reward_SOC.GetSOC_Ents() != 0)
            {
                copybtn = Instantiate(m_gPanel_MonsterDictionary_DetailInfo_Panel_SS);
                copybtn.GetComponent<Panel_MonsterDictionary_SS>().Set_Reward_SOC("앤        트: ", md.m_SMonster_Death_Reward_SOC.GetSOC_Ents());

                contentpos = copybtn.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_MonsterDictionary_DetailInfo_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_MonsterDictionary_DetailInfo_Reward.Add(copybtn);
            }
            if (md.m_SMonster_Death_Reward_SOC.GetSOC_Devil() != 0)
            {
                copybtn = Instantiate(m_gPanel_MonsterDictionary_DetailInfo_Panel_SS);
                copybtn.GetComponent<Panel_MonsterDictionary_SS>().Set_Reward_SOC("마        족: ", md.m_SMonster_Death_Reward_SOC.GetSOC_Devil());

                contentpos = copybtn.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_MonsterDictionary_DetailInfo_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_MonsterDictionary_DetailInfo_Reward.Add(copybtn);
            }
            if (md.m_SMonster_Death_Reward_SOC.GetSOC_Dragon() != 0)
            {
                copybtn = Instantiate(m_gPanel_MonsterDictionary_DetailInfo_Panel_SS);
                copybtn.GetComponent<Panel_MonsterDictionary_SS>().Set_Reward_SOC("용        족: ", md.m_SMonster_Death_Reward_SOC.GetSOC_Dragon());

                contentpos = copybtn.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_MonsterDictionary_DetailInfo_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_MonsterDictionary_DetailInfo_Reward.Add(copybtn);
            }
            if (md.m_SMonster_Death_Reward_SOC.GetSOC_Shadow() != 0)
            {
                copybtn = Instantiate(m_gPanel_MonsterDictionary_DetailInfo_Panel_SS);
                copybtn.GetComponent<Panel_MonsterDictionary_SS>().Set_Reward_SOC("어        둠: ", md.m_SMonster_Death_Reward_SOC.GetSOC_Shadow());

                contentpos = copybtn.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_MonsterDictionary_DetailInfo_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_MonsterDictionary_DetailInfo_Reward.Add(copybtn);
            }
        }
        else
        {
            m_gSV_MonsterDictionary_DetailInfo_Reward.SetActive(true);
            m_ScrollBar_MonsterDictionary_DetailInfo_Reward.value = 1;

            for (int i = 0; i < m_gList_MonsterDictionary_DetailInfo_Reward.Count; i++)
            {
                Destroy(m_gList_MonsterDictionary_DetailInfo_Reward[i].gameObject);
            }
            m_gList_MonsterDictionary_DetailInfo_Reward.Clear();

            GameObject copybtn; RectTransform contentpos;

            if (md.m_SMonster_Goaway_Reward_STATUS.GetSTATUS_EXP_Current() != 0)
            {
                copybtn = Instantiate(m_gPanel_MonsterDictionary_DetailInfo_Panel_SS);
                copybtn.GetComponent<Panel_MonsterDictionary_SS>().Set_Reward_STATUS(md.m_SMonster_Goaway_Reward_STATUS);

                contentpos = copybtn.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_MonsterDictionary_DetailInfo_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_MonsterDictionary_DetailInfo_Reward.Add(copybtn);
            }

            if (md.m_SMonster_Goaway_Reward_SOC.GetSOC_Honor() != 0)
            {
                copybtn = Instantiate(m_gPanel_MonsterDictionary_DetailInfo_Panel_SS);
                copybtn.GetComponent<Panel_MonsterDictionary_SS>().Set_Reward_SOC("명        예: ", md.m_SMonster_Goaway_Reward_SOC.GetSOC_Honor());

                contentpos = copybtn.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_MonsterDictionary_DetailInfo_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_MonsterDictionary_DetailInfo_Reward.Add(copybtn);
            }
            if (md.m_SMonster_Goaway_Reward_SOC.GetSOC_Human() != 0)
            {
                copybtn = Instantiate(m_gPanel_MonsterDictionary_DetailInfo_Panel_SS);
                copybtn.GetComponent<Panel_MonsterDictionary_SS>().Set_Reward_SOC("인        간: ", md.m_SMonster_Goaway_Reward_SOC.GetSOC_Human());

                contentpos = copybtn.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_MonsterDictionary_DetailInfo_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_MonsterDictionary_DetailInfo_Reward.Add(copybtn);
            }
            if (md.m_SMonster_Goaway_Reward_SOC.GetSOC_Animal() != 0)
            {
                copybtn = Instantiate(m_gPanel_MonsterDictionary_DetailInfo_Panel_SS);
                copybtn.GetComponent<Panel_MonsterDictionary_SS>().Set_Reward_SOC("동        물: ", md.m_SMonster_Goaway_Reward_SOC.GetSOC_Animal());

                contentpos = copybtn.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_MonsterDictionary_DetailInfo_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_MonsterDictionary_DetailInfo_Reward.Add(copybtn);
            }
            if (md.m_SMonster_Goaway_Reward_SOC.GetSOC_Slime() != 0)
            {
                copybtn = Instantiate(m_gPanel_MonsterDictionary_DetailInfo_Panel_SS);
                copybtn.GetComponent<Panel_MonsterDictionary_SS>().Set_Reward_SOC("슬  라  임: ", md.m_SMonster_Goaway_Reward_SOC.GetSOC_Slime());

                contentpos = copybtn.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_MonsterDictionary_DetailInfo_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_MonsterDictionary_DetailInfo_Reward.Add(copybtn);
            }
            if (md.m_SMonster_Goaway_Reward_SOC.GetSOC_Skeleton() != 0)
            {
                copybtn = Instantiate(m_gPanel_MonsterDictionary_DetailInfo_Panel_SS);
                copybtn.GetComponent<Panel_MonsterDictionary_SS>().Set_Reward_SOC("스켈레톤: ", md.m_SMonster_Goaway_Reward_SOC.GetSOC_Skeleton());

                contentpos = copybtn.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_MonsterDictionary_DetailInfo_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_MonsterDictionary_DetailInfo_Reward.Add(copybtn);
            }
            if (md.m_SMonster_Goaway_Reward_SOC.GetSOC_Ents() != 0)
            {
                copybtn = Instantiate(m_gPanel_MonsterDictionary_DetailInfo_Panel_SS);
                copybtn.GetComponent<Panel_MonsterDictionary_SS>().Set_Reward_SOC("앤        트: ", md.m_SMonster_Goaway_Reward_SOC.GetSOC_Ents());

                contentpos = copybtn.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_MonsterDictionary_DetailInfo_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_MonsterDictionary_DetailInfo_Reward.Add(copybtn);
            }
            if (md.m_SMonster_Goaway_Reward_SOC.GetSOC_Devil() != 0)
            {
                copybtn = Instantiate(m_gPanel_MonsterDictionary_DetailInfo_Panel_SS);
                copybtn.GetComponent<Panel_MonsterDictionary_SS>().Set_Reward_SOC("마        족: ", md.m_SMonster_Goaway_Reward_SOC.GetSOC_Devil());

                contentpos = copybtn.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_MonsterDictionary_DetailInfo_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_MonsterDictionary_DetailInfo_Reward.Add(copybtn);
            }
            if (md.m_SMonster_Goaway_Reward_SOC.GetSOC_Dragon() != 0)
            {
                copybtn = Instantiate(m_gPanel_MonsterDictionary_DetailInfo_Panel_SS);
                copybtn.GetComponent<Panel_MonsterDictionary_SS>().Set_Reward_SOC("용        족: ", md.m_SMonster_Goaway_Reward_SOC.GetSOC_Dragon());

                contentpos = copybtn.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_MonsterDictionary_DetailInfo_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_MonsterDictionary_DetailInfo_Reward.Add(copybtn);
            }
            if (md.m_SMonster_Goaway_Reward_SOC.GetSOC_Shadow() != 0)
            {
                copybtn = Instantiate(m_gPanel_MonsterDictionary_DetailInfo_Panel_SS);
                copybtn.GetComponent<Panel_MonsterDictionary_SS>().Set_Reward_SOC("어        둠: ", md.m_SMonster_Goaway_Reward_SOC.GetSOC_Shadow());

                contentpos = copybtn.GetComponent<RectTransform>();
                contentpos.SetParent(m_gContent_MonsterDictionary_DetailInfo_Reward.transform);
                contentpos.transform.localScale = new Vector3(1, 1, 1);
                contentpos.localPosition = new Vector3(contentpos.localPosition.x, contentpos.localPosition.y, 0);

                m_gList_MonsterDictionary_DetailInfo_Reward.Add(copybtn);
            }
        }
    }

    // MonsterDictionary 갱신된 몬스터 목록.
    public void Add_MonsterDictionary_Sparkle(E_MONSTER_KIND emk, int monstercode, int releaseratio)
    {
        int arynum;
        if (emk == E_MONSTER_KIND.OBJECT)
            arynum = 8;
        else
            arynum = (int)emk - 1;
       
        m_nList_MonsterDictionary_Sparkle.Add(new int[4] { arynum, monstercode, releaseratio, 0 });
    }
}
