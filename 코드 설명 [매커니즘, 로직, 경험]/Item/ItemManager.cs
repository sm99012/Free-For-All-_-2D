using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    //
    // ※ 싱글톤패턴을 적용한 ItemManager 클래스를 이용해 모든 아이템을 관리한다. 원본 아이템 데이터의 저장 공간이다.(게임 시작 시 모든 아이템 데이터를 저장한다.)
    //    추후 최적화를 위해 플레이어가 현재 착용 및 적용중이거나, 보유한 아이템 데이터만을 로드해 메모리 성능을 높일 예정이다.
    //    최적화 관련 정보는 아래 링크를 참조해 주세요.
    //    
    //    
    
    public static ItemManager m_ItemManager = null;
    private void Awake()
    {
        if (m_ItemManager == null)
        {
            m_ItemManager = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static ItemManager instance
    {
        get
        {
            if (m_ItemManager == null)
            {
                return null;
            }
            return m_ItemManager;
        }
    }

    public static int sm_nItemNumber = 0; // 아이템 생성코드(아이템 생성 순서) 계산을 위한 스태틱 변수이다. 아이템 생성코드는 장비아이템에만 할당된다.

    public GameObject m_gItem_Equip_Null; // 장비아이템 사본(유니티 오브젝트) 생성을 위한 기반(유니티 오브젝트)
    public GameObject m_gItem_Use_Null;   // 소비아이템 사본(유니티 오브젝트) 생성을 위한 기반(유니티 오브젝트)
    public GameObject m_gItem_Etc_Null;   // 기타아이템 사본(유니티 오브젝트) 생성을 위한 기반(유니티 오브젝트)
    public GameObject m_gItem_Gold_Null;  // 골드(재화) 사본(유니티 오브젝트) 생성을 위한 기반(유니티 오브젝트)

    public Dictionary <int, Item_Equip> m_Dictionary_MonsterDrop_Equip; // 몬스터 제거(토벌 + 놓아주기) 를 통해 획득 가능한 장비아이템 딕셔너리. Dictionary <Key : 장비아이템 고유코드, Value : 장비아이템 데이터>
    public Dictionary <int, Item_Use> m_Dictionary_MonsterDrop_Use;     // 몬스터 제거(토벌 + 놓아주기) 를 통해 획득 가능한 소비아이템 딕셔너리. Dictionary <Key : 소비아이템 고유코드, Value : 소비아이템 데이터>
    public Dictionary <int, Item_Etc> m_Dictionary_MonsterDrop_Etc;     // 몬스터 제거(토벌 + 놓아주기) 를 통해 획득 가능한 기타아이템 딕셔너리. Dictionary <Key : 기타아이템 고유코드, Value : 기타아이템 데이터>

    public Dictionary<int, Item_Equip> m_Dictionary_Collection_Equip; // 채집물 채집을 통해 획득 가능한 장비아이템 딕셔너리. Dictionary <Key : 장비아이템 고유코드, Value : 장비아이템 데이터>
    public Dictionary<int, Item_Use> m_Dictionary_Collection_Use;     // 채집물 채집을 통해 획득 가능한 소비아이템 딕셔너리. Dictionary <Key : 소비아이템 고유코드, Value : 소비아이템 데이터>
    public Dictionary<int, Item_Etc> m_Dictionary_Collection_Etc;     // 채집물 채집을 통해 획득 가능한 기타아이템 딕셔너리. Dictionary <Key : 기타아이템 고유코드, Value : 기타아이템 데이터>

    public Dictionary<int, Item_Equip> m_Dictionary_QuestReward_Equip; // 퀘스트 완료 보상으로 획득 가능한 장비아이템 딕셔너리. Dictionary <Key : 장비아이템 고유코드, Value : 장비아이템 데이터>
    public Dictionary<int, Item_Use> m_Dictionary_QuestReward_Use;     // 퀘스트 완료 보상으로 획득 가능한 소비아이템 딕셔너리. Dictionary <Key : 소비아이템 고유코드, Value : 소비아이템 데이터>
    public Dictionary<int, Item_Etc> m_Dictionary_QuestReward_Etc;     // 퀘스트 완료 보상으로 획득 가능한 기타아이템 딕셔너리. Dictionary <Key : 기타아이템 고유코드, Value : 기타아이템 데이터>

    // 변수 초기화 및 아이템 데이터 로딩
    public void InitialSet()
    {
        m_Dictionary_MonsterDrop_Equip = new Dictionary<int, Item_Equip>();
        m_Dictionary_MonsterDrop_Use = new Dictionary<int, Item_Use>();
        m_Dictionary_MonsterDrop_Etc = new Dictionary<int, Item_Etc>();

        m_Dictionary_Collection_Equip = new Dictionary<int, Item_Equip>();
        m_Dictionary_Collection_Use = new Dictionary<int, Item_Use>();
        m_Dictionary_Collection_Etc = new Dictionary<int, Item_Etc>();

        m_Dictionary_QuestReward_Equip = new Dictionary<int, Item_Equip>();
        m_Dictionary_QuestReward_Use = new Dictionary<int, Item_Use>();
        m_Dictionary_QuestReward_Etc = new Dictionary<int, Item_Etc>();

        m_gItem_Equip_Null = Resources.Load("Prefab/GameObject/Item_Equip_Null") as GameObject;
        m_gItem_Use_Null = Resources.Load("Prefab/GameObject/Item_Use_Null") as GameObject;
        m_gItem_Etc_Null = Resources.Load("Prefab/GameObject/Item_Etc_Null") as GameObject;
        m_gItem_Gold_Null = Resources.Load("Prefab/GameObject/Item_Gold_Null") as GameObject;

        Load_Item(); // 게임 시작 시 모든 아이템 데이터 로딩
    }

    // 로딩 관련 함수
    // 게임 시작 시 모든 아이템 데이터 로딩
    void Load_Item()
    {
        Load_Item_Equip();    // 모든 장비아이템 데이터 로딩
        Load_Item_Use();      // 소비아이템(기프트)을 제외한 모든 소비아이템 데이터 로딩
        Load_Item_Etc();      // 모든 기타아이템 데이터 로딩
        
        Load_Item_Use_Gift(); // 모든 소비아이템(기프트) 데이터 로딩
    }
    // 모든 장비아이템 데이터 로딩
    void Load_Item_Equip()
    {
        Load_Item_Equip_MainWeapon(); // 모든 장비아이템(주무기) 데이터 로딩
        Load_Item_Equip_SubWeapon();  // 모든 장비아이템(보조무기) 데이터 로딩
        Load_Item_Equip_Hat();        // 모든 장비아이템(모자) 데이터 로딩
        Load_Item_Equip_Top();        // 모든 장비아이템(상의) 데이터 로딩
        Load_Item_Equip_Bottoms();    // 모든 장비아이템(하의) 데이터 로딩
        Load_Item_Equip_Shose();      // 모든 장비아이템(신발) 데이터 로딩
    }
    // 모든 장비아이템(주무기) 데이터 로딩
    void Load_Item_Equip_MainWeapon()
    {
        Load_Item_Equip_MainWaepon_Sword(); // 모든 장비아이템(주무기(검)) 데이터 로딩
        Load_Item_Equip_MainWeapon_Knife(); // 모든 장비아이템(주무기(단검)) 데이터 로딩
        Load_Item_Equip_MainWeapon_Axe();   // 모든 장비아이템(주무기(도끼)) 데이터 로딩
    }
    // 모든 장비아이템(주무기(검)) 데이터 로딩
    void Load_Item_Equip_MainWaepon_Sword()
    {
        Item_Equip item;

        item = new Item_Equip("목검", 1000, "Prefab/Item/Item_Equip/Weapons/Sword/001",
            E_ITEM_GRADE.NORMAL, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.SWORD,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S1,
            0, 0, 450);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0.25f);
        item.m_sStatus_Limit_Min = new STATUS(1, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(1);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "평범한 나무를 다듬어 만든 목검이다.\n금방이라도 부서질것만 같다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("양산형 글라디우스", 1001, "Prefab/Item/Item_Equip/Weapons/Sword/003",
            E_ITEM_GRADE.NORMAL, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.SWORD,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S1,
            1, 0, 1500);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, -10, 1, 0, 0, 0.35f);
        item.m_sStatus_Limit_Min = new STATUS(1, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(1);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "흔히 볼 수 있는 양산형 글라디우스이다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("비대칭 글라디우스", 1002, "Prefab/Item/Item_Equip/Weapons/Sword/012",
            E_ITEM_GRADE.NORMAL, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.SWORD,
            E_ITEM_ADDITIONALOPTION_STATUS.S2, E_ITEM_ADDITIONALOPTION_SOC.S2,
            1, 0, 1650);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, -5, 0, 0, 0, 0.3f);
        item.m_sStatus_Limit_Min = new STATUS(5, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(1);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "비대칭적인 구조를 가진 글라디우스이다. 비대칭이기에 우아하다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("브로드소드", 1003, "Prefab/Item/Item_Equip/Weapons/Sword/004",
            E_ITEM_GRADE.NORMAL, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.SWORD,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S1,
            1, 0, 2400);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, -10, 0, 0, 0, 0.4f);
        item.m_sStatus_Limit_Min = new STATUS(5, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(1);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "손잡이가 달린 브로드소드이다.\n제법 크고 묵직하기에 사용하기 힘들다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("양산형 롱소드", 1004, "Prefab/Item/Item_Equip/Weapons/Sword/007",
            E_ITEM_GRADE.NORMAL, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.SWORD,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S1,
            1, 0, 3800);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, -5, 0, 0, 0, 0.4f);
        item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(2);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "이세계에서 가장 보편화된 무기인 양산형 롱소드이다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("달빛 대도[50%]", 1005, "Prefab/Item/Item_Equip/Weapons/Sword/014",
            E_ITEM_GRADE.RARE, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.SWORD,
            E_ITEM_ADDITIONALOPTION_STATUS.S3, E_ITEM_ADDITIONALOPTION_SOC.S1,
            1, 0, 4200);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 5, 0, 0, 0, 0.25f);
        item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(2);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "달빛에 은은하게 빛나는 대도이다.\n달빛의 신성한 힘을 50% 응축시켜 신속하게 움직일 수 있으나 공격력이 낮다.\n기능이 제한되어있다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("달빛 대도[99%]", 1006, "Prefab/Item/Item_Equip/Weapons/Sword/015",
            E_ITEM_GRADE.RARE, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.SWORD,
            E_ITEM_ADDITIONALOPTION_STATUS.S3, E_ITEM_ADDITIONALOPTION_SOC.S1,
            3, 0, 9200);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 5, 0, 0, 0, 0.2f);
        item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(4);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "달빛에 은은하게 빛나는 대도이다.\n달빛의 신성한 힘을 99% 까지 응축시켜 신속하게 움직이고 날카로운 일격을 가할 수 있다.\n달은 항상 어둠속에 찾아온다. 그렇기에 달빛의 신성한 힘은 99% 까지밖에 응축할 수 없다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("볼룹 소드", 1007, "Prefab/Item/Item_Equip/Weapons/Sword/010",
            E_ITEM_GRADE.NORMAL, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.SWORD,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S1,
            2, 0, 4100);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 5, 5, 5, 0, 0, -10, 1, 1, 0, 0.4f);
        item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(2);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "검날의 시작 부분이 튀어나온 형태가 특이한 볼룹 소드이다.\n한번 볼룹 소드에 찔린 상대는 무사하기 힘들 것이다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("레이피어", 1008, "Prefab/Item/Item_Equip/Weapons/Sword/023",
            E_ITEM_GRADE.COMMON, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.SWORD,
            E_ITEM_ADDITIONALOPTION_STATUS.S3, E_ITEM_ADDITIONALOPTION_SOC.S1,
            3, 0, 3800);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 5, 0, 0, 0, 0.2f);
        item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(3);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "신속히 상대를 찌르는 용도의 레이피어이다.\n상대를 단 한 번의 일격에 무찌르는 것을 가장 중요하게 여긴다.\n기사단이 주로 사용하는 무기이다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("래피드 레이피어", 1009, "Prefab/Item/Item_Equip/Weapons/Sword/025",
            E_ITEM_GRADE.RARE, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.SWORD,
            E_ITEM_ADDITIONALOPTION_STATUS.S3, E_ITEM_ADDITIONALOPTION_SOC.S1,
            5, 0, 6400);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, 10, 0, 0, 0, 0.15f);
        item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(5);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "빛의 속도로 강력하게 찌르는 레이피어이다. 마치 말벌의 벌침과 같다.\n기사단에서는 주기적으로 레이피어 사용자들 중 실력이 출중한 몇 명을 추려 최고급 소재로 만든 래피드 레이피어를 제공하고 있다.\n대단히 긍지높은 기사의 무기라 자부할 수 있다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("무거운 대검", 1010, "Prefab/Item/Item_Equip/Weapons/Sword/078",
            E_ITEM_GRADE.COMMON, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.SWORD,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S1,
            3, 0, 4200);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 10, 0, 0, 0, 0, -20, 3, 3, 0, 0.6f);
        item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, 20, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(2);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "아무나 휘두를 수 없는 무거운 대검이다.\n단순히 대검의 무게를 최대한 늘리기 위해 주변에서 쉽게 구할 수 있는 무거운 재료를 혼합했다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("태도", 1011, "Prefab/Item/Item_Equip/Weapons/Sword/084",
            E_ITEM_GRADE.NORMAL, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.SWORD,
            E_ITEM_ADDITIONALOPTION_STATUS.S2, E_ITEM_ADDITIONALOPTION_SOC.S2,
            2, 0, 5700);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 5, 5, 5, 0, 0, 5, 1, 1, 0, 0.3f);
        item.m_sStatus_Limit_Min = new STATUS(12, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(2);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "저 멀리 바다를 건너온 무기인 태도이다.\n방어력을 제외하고는 준수한 성능덕에 많이들 사용한다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("불꽃 태도", 1012, "Prefab/Item/Item_Equip/Weapons/Sword/087",
            E_ITEM_GRADE.COMMON, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.SWORD,
            E_ITEM_ADDITIONALOPTION_STATUS.S3, E_ITEM_ADDITIONALOPTION_SOC.S3,
            4, 0, 6200);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, -5, 0, 5, 0, 7, 7, 7, 0, 0, 10, -3, -3, 0, 0.3f);
        item.m_sStatus_Limit_Min = new STATUS(12, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(3);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "저 멀리 바다를 건너온 무기인 태도이다.\n모든 것을 집어삼키는 불꽃처럼 붉은색을 띠고 있다.\n사용자 또한 불꽃에 삼켜질 우려가 있으니 조심해야한다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("낡은 환도", 1013, "Prefab/Item/Item_Equip/Weapons/Sword/088",
            E_ITEM_GRADE.COMMON, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.SWORD,
            E_ITEM_ADDITIONALOPTION_STATUS.S3, E_ITEM_ADDITIONALOPTION_SOC.S5,
            2, 0, 3800);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 4, 4, 4, 0, 0, -5, 0, 0, 0, 0.3f);
        item.m_sStatus_Limit_Min = new STATUS(10, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(2);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "이리 치이고 저리 치인 낡은 환도이다.\n구양 사막 지대의 전통 무기이다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("환도", 1014, "Prefab/Item/Item_Equip/Weapons/Sword/089",
            E_ITEM_GRADE.NORMAL, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.SWORD,
            E_ITEM_ADDITIONALOPTION_STATUS.S3, E_ITEM_ADDITIONALOPTION_SOC.S2,
            3, 0, 4400);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 6, 6, 6, 0, 0, -5, 1, 1, 0, 0.3f);
        item.m_sStatus_Limit_Min = new STATUS(10, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(2);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "구양 사막 지대의 전통 무기로 흔히 볼 수 있는 환도이다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("지휘관의 환도", 1015, "Prefab/Item/Item_Equip/Weapons/Sword/091",
            E_ITEM_GRADE.COMMON, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.SWORD,
            E_ITEM_ADDITIONALOPTION_STATUS.S3, E_ITEM_ADDITIONALOPTION_SOC.S5,
            4, 0, 6000);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 3, 0, 3, 0, 6, 6, 6, 0, 0, -5, 1, 1, 0, 0.3f);
        item.m_sStatus_Limit_Min = new STATUS(12, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(4, 0, 0, 0, 1, 1, 0, 0, 0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "구양 사막 지대의 일부 지역을 다스리는 영주에게 지급되는 지휘관의 환도이다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("금강검", 1016, "Prefab/Item/Item_Equip/Weapons/Sword/094",
            E_ITEM_GRADE.RELIC, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.SWORD,
            E_ITEM_ADDITIONALOPTION_STATUS.S4, E_ITEM_ADDITIONALOPTION_SOC.S7,
            7, 0, 11000);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 5, 0, 5, 0, 8, 8, 8, 0, 0, -5, 2, 2, 0, 0.3f);
        item.m_sStatus_Limit_Min = new STATUS(15, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(7, 0, 0, 0, 3, 3, 0, 0, 0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "구양 사막 지대에 위치했던 잊혀진 고대 제국의 유물인 금강검이다.\n억겁의 시간이 흘러도, 거센 모래폭풍이 들이닥쳐도 금강검은 변함없다.\n소문에 의하면 금강검을 얻는자가 잊혀진 고대 제국의 비밀을 파헤친다고 한다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("사인검", 1017, "Prefab/Item/Item_Equip/Weapons/Sword/095",
            E_ITEM_GRADE.RARE, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.SWORD,
            E_ITEM_ADDITIONALOPTION_STATUS.S3, E_ITEM_ADDITIONALOPTION_SOC.S6,
            5, 0, 9000);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 5, 0, 5, 0, 7, 7, 7, 0, 0, -5, 2, 2, 0, 0.2f);
        item.m_sStatus_Limit_Min = new STATUS(15, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(7, 0, 0, 0, 3, 3, 0, 0, 0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "구양 사막 지대에 위치하고 있는 컨트 공국의 장군들에게 제공되는 사인검이다.\n사인검은 명예, 지위, 부 등 모든 것을 의미하는 컨트 공국의 최고 명검이다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("알트제 반달검", 1018, "Prefab/Item/Item_Equip/Weapons/Sword/081",
            E_ITEM_GRADE.RARE, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.SWORD,
            E_ITEM_ADDITIONALOPTION_STATUS.S3, E_ITEM_ADDITIONALOPTION_SOC.S6,
            5, 0, 9000);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 5, 0, 5, 0, 6, 6, 6, 0, 0, 5, -2, -2, 0, 0.3f);
        item.m_sStatus_Limit_Min = new STATUS(15, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(7, 0, 0, 0, 3, 3, 0, 0, 0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "구양 사막 지대에 위치하고 있는 알트 왕국의 공신들에게 제공되는 사막의 모래폭풍이라 불리는 알트제 반달검이다.\n'알트제 반달검' 은 알트 왕국 그 자체를 의미할 정도로 깊은 역사를 자랑한다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);
    }
    // 모든 장비아이템(주무기(단검)) 데이터 로딩
    void Load_Item_Equip_MainWeapon_Knife()
    {
        Item_Equip item;

        item = new Item_Equip("나무단검", 1300, "Prefab/Item/Item_Equip/Weapons/Knife/016",
            E_ITEM_GRADE.NORMAL, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.KNIFE,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S1,
            0, 0, 400);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0.15f);
        item.m_sStatus_Limit_Min = new STATUS(1, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(1);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "평범한 나무를 다듬어 만든 단검이다.\n금방이라도 부서질것만 같다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("양산형 대거", 1301, "Prefab/Item/Item_Equip/Weapons/Knife/017",
            E_ITEM_GRADE.NORMAL, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.KNIFE,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S1,
            1, 0, 1350);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 5, 0, 0, 0, 0.1f);
        item.m_sStatus_Limit_Min = new STATUS(1, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(2);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "흔히 볼 수 있는 양산형 대거이다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("비대칭 대거", 1302, "Prefab/Item/Item_Equip/Weapons/Knife/097",
            E_ITEM_GRADE.NORMAL, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.KNIFE,
            E_ITEM_ADDITIONALOPTION_STATUS.S2, E_ITEM_ADDITIONALOPTION_SOC.S2,
            1, 0, 1400);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 5, 0, 0, 0, 0.05f);
        item.m_sStatus_Limit_Min = new STATUS(5, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(2);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "비대칭적인 구조를 가진 대거이다. 비대칭이기에 우아하다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("브로드대거", 1303, "Prefab/Item/Item_Equip/Weapons/Knife/018",
            E_ITEM_GRADE.NORMAL, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.KNIFE,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S1,
            1, 0, 2100);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, -5, 0, 0, 0, 0.15f);
        item.m_sStatus_Limit_Min = new STATUS(5, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(2);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "손잡이가 달린 브로드대거이다.\n사용감이 좋다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("양산형 나이프", 1304, "Prefab/Item/Item_Equip/Weapons/Knife/020",
            E_ITEM_GRADE.NORMAL, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.KNIFE,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S1,
            1, 0, 1800);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0.15f);
        item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(2);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "이세계에서 가장 보편화된 무기인 양산형 나이프이다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("별빛 커틀러스", 1305, "Prefab/Item/Item_Equip/Weapons/Knife/113",
            E_ITEM_GRADE.COMMON, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.KNIFE,
            E_ITEM_ADDITIONALOPTION_STATUS.S2, E_ITEM_ADDITIONALOPTION_SOC.S2,
            2, 0, 2400);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 10, 0, 0, 0, 0.05f);
        item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(2);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "별빛에 은은하게 빛나는 커틀러스이다.\n해적들의 무기라 알려져있지만 준수한 성능덕에 많이들 사용한다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);
    }
    // 모든 장비아이템(주무기(도끼)) 데이터 로딩
    void Load_Item_Equip_MainWeapon_Axe()
    {
        Item_Equip item;

        item = new Item_Equip("나무도끼", 1600, "Prefab/Item/Item_Equip/Weapons/Axe/051",
            E_ITEM_GRADE.NORMAL, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.AXE,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S1,
            0, 0, 600);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0.6f);
        item.m_sStatus_Limit_Min = new STATUS(1, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(1);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "평범한 나무를 다듬어 만든 도끼이다.\n금방이라도 부서질것만 같다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("철제도끼", 1601, "Prefab/Item/Item_Equip/Weapons/Axe/073",
            E_ITEM_GRADE.NORMAL, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.AXE,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S1,
            1, 0, 2000);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, -10, 1, 0, 0, 0.5f);
        item.m_sStatus_Limit_Min = new STATUS(1, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(2);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "흔히 볼 수 있는 양산형 철제도끼이다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("다용도 철제도끼", 1602, "Prefab/Item/Item_Equip/Weapons/Axe/072",
            E_ITEM_GRADE.NORMAL, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.AXE,
            E_ITEM_ADDITIONALOPTION_STATUS.S2, E_ITEM_ADDITIONALOPTION_SOC.S2,
            1, 0, 2200);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, -10, 2, 0, 0, 0.55f);
        item.m_sStatus_Limit_Min = new STATUS(5, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(2);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "특이한 구조를 가진 철제도끼이다.\n사용처가 많다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("브로드엑스", 1603, "Prefab/Item/Item_Equip/Weapons/Axe/035",
            E_ITEM_GRADE.NORMAL, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.AXE,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S1,
            1, 0, 2800);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 8, 0, 0, 0, 0, -15, 1, 0, 0, 0.5f);
        item.m_sStatus_Limit_Min = new STATUS(5, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(2);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "손잡이가 달린 브로드엑스이다.\n제법 크고 묵직하기에 사용하기 힘들다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("다마스커스엑스", 1604, "Prefab/Item/Item_Equip/Weapons/Axe/074",
            E_ITEM_GRADE.NORMAL, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.AXE,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S1,
            1, 0, 2400);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, -20, 1, 0, 0, 0.7f);
        item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(2);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "무늬가 인상깊은 다마스커스엑스이다.\n다마스커스를 만드는 과정은 매우 번거로우며 힘들다. 그러나 그만큼 아름답고 예리하다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("커틀러스 엑스", 1605, "Prefab/Item/Item_Equip/Weapons/Axe/031",
            E_ITEM_GRADE.COMMON, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.AXE,
            E_ITEM_ADDITIONALOPTION_STATUS.S2, E_ITEM_ADDITIONALOPTION_SOC.S2,
            2, 0, 3100);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 8, 0, 0, 0,0, -15, 1, 0, 0, 0.6f);
        item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(2);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "생김새가 특이한 커틀러스 엑스이다.\n해적들의 무기라 알려져있지만 준수한 성능덕에 많이들 사용한다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("구리빛 뿔도끼", 1606, "Prefab/Item/Item_Equip/Weapons/Axe/055",
            E_ITEM_GRADE.NORMAL, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.AXE,
            E_ITEM_ADDITIONALOPTION_STATUS.S4, E_ITEM_ADDITIONALOPTION_SOC.S2,
            3, 0, 3600);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, -10, 1, 1, 0, 0.3f);
        item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(2, 1);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "인류가 발견한 첫 번째 금속인 '구리'로 만들어진 뿔도 끼이다.\n'구리' 로 만들어져 무기로서의 파괴력은 조금 떨어지지만 가벼워 널리 사용된다.\n소문에 의하면 용병단의 누군가가 사용한다고 한다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("야만 전사의 양날도끼", 1607, "Prefab/Item/Item_Equip/Weapons/Axe/033",
            E_ITEM_GRADE.RARE, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.AXE,
            E_ITEM_ADDITIONALOPTION_STATUS.S2, E_ITEM_ADDITIONALOPTION_SOC.S2,
            5, 0, 8000);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, -5, 0, 12, 0, 0, 0, 0, -30, 5, 5, 0, 1f);
        item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(-4, -1, 0, -1, 1, -1, -1, -1, 1);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "역사상 가장 강력했다고 알려진 야만 전사의 양날도끼이다.\n야만 전사들은 강인하디 강인한 북방민족마저도 침략하고 무릎을 꿇릴 정도로 매우 강력하고 악랄했다. 그런 야만 전사들의 장비에는 그들의 인생이 담겨있는듯 투박하다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("사자의 은빛 낫", 1608, "Prefab/Item/Item_Equip/Weapons/Axe/027",
            E_ITEM_GRADE.RARE, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.AXE,
            E_ITEM_ADDITIONALOPTION_STATUS.S3, E_ITEM_ADDITIONALOPTION_SOC.S3,
            5, 0, 7200);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, -5, 0, 0, 0, 0.3f);
        item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(-5, -5, -5, -5, 5, -5, -5, -5, 1);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "죽은 자를 의미하는 사자의 은빛 낫이다.\n살아있는 모든 것을 베어버린다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("풍요의 금빛 낫", 1609, "Prefab/Item/Item_Equip/Weapons/Axe/029",
            E_ITEM_GRADE.RARE, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.AXE,
            E_ITEM_ADDITIONALOPTION_STATUS.S3, E_ITEM_ADDITIONALOPTION_SOC.S3,
            5, 0, 7200);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 20, 0, 0, 0, 0, 0, 0, 0, 0, -5, 0, 0, 0, 0.3f);
        item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(5, 5, 5, 5, -5, 5, 5, 5, -1);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "평화와 안정을 불러오는 풍요의 금빛 낫이다.\n한번 휘두름으로 죽어가는 생명체를 되살린다.\n그러나 이미 죽은 생명체를 살릴 수는 없다...";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("야만 전사 우요의 오른손도끼", 1610, "Prefab/Item/Item_Equip/Weapons/Axe/032",
            E_ITEM_GRADE.RELIC, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.AXE,
            E_ITEM_ADDITIONALOPTION_STATUS.S4, E_ITEM_ADDITIONALOPTION_SOC.S4,
            5, 0, 12000);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 16, 0, 0, 0, 0, -10, 5, 5, 0, 0.5f);
        item.m_sStatus_Limit_Min = new STATUS(12, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(-4, -1, 0, -1, 1, -1, -1, -1, 1);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "역사상 가장 강력했다고 알려진 야만 전사 일족에는 하나의 전통이 있다. 바로 남들보다 더 강력하고 더 악랄한 야만 전사에게는 이름이 주어진다는 것.\n이 도끼는 야만 전사 우요의 오른손도끼이다.\n'야만 전사 우요의 왼손해머' 와 함께 착용할 시 '야만 전사 우요의 세트 효과' 를 얻을 수 있다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("어썸 메이스", 1611, "Prefab/Item/Item_Equip/Weapons/Axe/045",
            E_ITEM_GRADE.NORMAL, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.AXE,
            E_ITEM_ADDITIONALOPTION_STATUS.S2, E_ITEM_ADDITIONALOPTION_SOC.S2,
            1, 0, 5200);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 10, 0, 0, 0, 0, -10, 0, 0, 0, 0.5f);
        item.m_sStatus_Limit_Min = new STATUS(10, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(2);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "맞으면 엌! 이라는 소리가 절로 나오는 놀라운 메이스이다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("오래된 철퇴", 1612, "Prefab/Item/Item_Equip/Weapons/Axe/048",
            E_ITEM_GRADE.NORMAL, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.AXE,
            E_ITEM_ADDITIONALOPTION_STATUS.S2, E_ITEM_ADDITIONALOPTION_SOC.S2,
            1, 0, 5400);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 13, 0, 0, 0, 0, -20, 1, 1, 0, 0.7f);
        item.m_sStatus_Limit_Min = new STATUS(10, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(2);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "오래전부터 쓰인 것으로 추정되는 철퇴이다.\n무언가를 부수는데 특화되어있다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("데인엑스", 1613, "Prefab/Item/Item_Equip/Weapons/Axe/053",
            E_ITEM_GRADE.NORMAL, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.AXE,
            E_ITEM_ADDITIONALOPTION_STATUS.S2, E_ITEM_ADDITIONALOPTION_SOC.S2,
            2, 0, 6000);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 1, 0, 0, 0, 12, 12, 12, 0, 0, -10, 1, 1, 0, 0.6f);
        item.m_sStatus_Limit_Min = new STATUS(12, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(2);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "북방 민족이 사용하던 평범한 데인엑스이다.\n북방 민족은 데인엑스로 의, 식, 주 모든 것을 해결해왔다.\n그들은 아직도 데인 엑스를 머리맡에 두고 잔다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("플랜지드 메이스", 1614, "Prefab/Item/Item_Equip/Weapons/Axe/047",
            E_ITEM_GRADE.COMMON, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.AXE,
            E_ITEM_ADDITIONALOPTION_STATUS.S3, E_ITEM_ADDITIONALOPTION_SOC.S2,
            2, 0, 4200);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 4, 4, 4, 0, 0, -5, 0, 0, 0, 0.3f);
        item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(1);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "단순해 보이지만 절대 쉽게 만들 수 없는 플랜지드 메이스이다.\n한 손에 들 수 있게끔 가볍게 제작되었다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("의식용 메이스", 1615, "Prefab/Item/Item_Equip/Weapons/Axe/049",
            E_ITEM_GRADE.RELIC, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.AXE,
            E_ITEM_ADDITIONALOPTION_STATUS.S4, E_ITEM_ADDITIONALOPTION_SOC.S2,
            0, 0, 13000);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, -5, 0, -5, 0, -3, -3, -3, 0, 0, -10, -3, -3, 0, 0.5f);
        item.m_sStatus_Limit_Min = new STATUS(15, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(3);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "먼 과거 어떠한 의식을 위해 사용되던 의식용 메이스이다.\n장비 착용자의 스탯을 갉아먹는다고 알려져 있다.\n무언가 숨겨진 비밀이 존재하는 것 같다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("리치본 해머", 1616, "Prefab/Item/Item_Equip/Weapons/Axe/064",
            E_ITEM_GRADE.COMMON, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.AXE,
            E_ITEM_ADDITIONALOPTION_STATUS.S2, E_ITEM_ADDITIONALOPTION_SOC.S2,
            2, 0, 9000);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 12, 12, 12, 0, 0, -40, 3, 3, 0, 1.2f);
        item.m_sStatus_Limit_Min = new STATUS(1, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(2);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "돈이 있는 곳에는 항상 리치본 해머가 존재했다.\n화려한 외형에 보석 장식으로 마무리된 고급 해머지만 성능은 장담할 수 없다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("포테이토 해머", 1617, "Prefab/Item/Item_Equip/Weapons/Axe/060",
            E_ITEM_GRADE.COMMON, E_ITEM_EQUIP_TYPE.MAINWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.AXE,
            E_ITEM_ADDITIONALOPTION_STATUS.S2, E_ITEM_ADDITIONALOPTION_SOC.S2,
            2, 0, 7200);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 5, 0, 0, 0, 9, 9, 9, 0, 0, -10, 3, 3, 0, 0.5f);
        item.m_sStatus_Limit_Min = new STATUS(12, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(2);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "갓 캐낸 감자 뭉텅이를 닮아있어 포테이토 해머라고 불린다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);
    }
    // 모든 장비아이템(보조무기) 데이터 로딩
    void Load_Item_Equip_SubWeapon()
    {
        Item_Equip item;

        item = new Item_Equip("낡은 원형 방패", 2001, "Prefab/Item/Item_Equip/SubWeapon/037",
            E_ITEM_GRADE.NORMAL, E_ITEM_EQUIP_TYPE.SUBWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S1,
            1, 0, 700);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, -5, 2, 0, 0, 0.1f);
        item.m_sStatus_Limit_Min = new STATUS(1, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(1);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "착용하는 것이 좋을지 의문이 들 정도로 낡은 방패.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("나무 방패", 2002, "Prefab/Item/Item_Equip/SubWeapon/036",
            E_ITEM_GRADE.NORMAL , E_ITEM_EQUIP_TYPE.SUBWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S1,
            1, 0, 800);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, -5, 3, 0, 0, 0.2f);
        item.m_sStatus_Limit_Min = new STATUS(5, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(1);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "평범한 나무 방패.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("야만 전사 우요의 왼손해머", 2003, "Prefab/Item/Item_Equip/Weapons/Axe/062",
            E_ITEM_GRADE.RELIC, E_ITEM_EQUIP_TYPE.SUBWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S4, E_ITEM_ADDITIONALOPTION_SOC.S4,
            5, 0, 9000);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 5, 5, 5, 0, 0, -5, 1, 1, 0, 0.2f);
        item.m_sStatus_Limit_Min = new STATUS(12, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(-4, -1, 0, -1, 1, -1, -1, -1, 1);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "역사상 가장 강력했다고 알려진 야만 전사 일족에는 하나의 전통이 있다. 바로 남들보다 더 강력하고 더 악랄한 야만 전사에게는 이름이 주어진다는 것.\n이 해머는 야만 전사 우요의 왼손해머이다.\n'야만 전사 우요의 오른손도끼' 와 함께 착용할 시 '야만 전사 우요의 세트 효과' 를 얻을 수 있다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("스틸 실드", 2004, "Prefab/Item/Item_Equip/SubWeapon/038",
            E_ITEM_GRADE.COMMON, E_ITEM_EQUIP_TYPE.SUBWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S2, E_ITEM_ADDITIONALOPTION_SOC.S1,
            2, 0, 2800);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 1, 0, 0, 0, 1, 1, 1, 0, 0, -5, 2, 2, 0, 0.2f);
        item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(1);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "강철로 만들어진 스틸 실드이다.\n적은 부위를 보호할 수 있다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("카이트 실드", 2005, "Prefab/Item/Item_Equip/SubWeapon/039",
            E_ITEM_GRADE.COMMON, E_ITEM_EQUIP_TYPE.SUBWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S2, E_ITEM_ADDITIONALOPTION_SOC.S3,
            2, 0, 3200);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 1, 0, 0, 0, 1, 1, 1, 0, 0, -20, 5, 5, 0, 0.3f);
        item.m_sStatus_Limit_Min = new STATUS(10, -10000, -10000, 20, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(1);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "강철로 만들어진 카이트 실드이다.\n많은 부위를 보호할 수 있으나 매우 무겁다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("골든 카이트 실드", 2006, "Prefab/Item/Item_Equip/SubWeapon/040",
            E_ITEM_GRADE.RARE, E_ITEM_EQUIP_TYPE.SUBWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S1,
            4, 0, 5000);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 3, 0, 0, 0, 1, 1, 1, 0, 0, -10, 7, 7, 0, 0.25f);
        item.m_sStatus_Limit_Min = new STATUS(10, -10000, -10000, 20, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(3);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "강철과 금을 적절히 섞어 만든 골든 카이트 실드이다.\n많은 부위를 보호할 수 있고 가볍기까지 하다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("호신용 단검", 2007, "Prefab/Item/Item_Equip/SubWeapon/sw_01",
            E_ITEM_GRADE.RARE, E_ITEM_EQUIP_TYPE.SUBWEAPON, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S3, E_ITEM_ADDITIONALOPTION_SOC.S1,
            3, 0, 3200);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 3, 3, 3, 0, 0, 5, 0, 0, 0, 0.05f);
        item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, 110, -10000, -10000, -10000, 0.4f);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(2, 1);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "대장장이 '블랙' 이 만든 최소한의 몸을 지키기 위한 용도의 단검이다.\n그래서 그런지 주무기로는 사용하기 힘들다.\n'드넓은 초원' 의 핍박받는 약자들을 위해 만들어졌으나 단검의 능력치가 출중하여 구하려 해도 못 구하는 귀한 단검이다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);
    }
    // 모든 장비아이템(모자) 데이터 로딩
    void Load_Item_Equip_Hat()
    {
        Item_Equip item;

        item = new Item_Equip("초보 모험가의 투구", 3001, "Prefab/Item/Item_Equip/Hat/Novice Adventurers Hat",
            E_ITEM_GRADE.NORMAL, E_ITEM_EQUIP_TYPE.HAT, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S1,
            0, 0, 500);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0);
        item.m_sStatus_Limit_Min = new STATUS(1, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(1);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "모험을 이제 막 시작한 초보 모험가를 위한 투구.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("허름한 기사의 투구", 3002, "Prefab/Item/Item_Equip/Hat/Shabby Knights Hat",
            E_ITEM_GRADE.COMMON , E_ITEM_EQUIP_TYPE.HAT, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S2, E_ITEM_ADDITIONALOPTION_SOC.S2,
            1, 0, 3000);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -5, 3, 3, 0, 0.1f);
        item.m_sStatus_Limit_Min = new STATUS(5, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(3);
        item.m_sSoc_Limit_Min = new SOC(10, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "어느 누군가의 기사, 어떤 가정의 아버지가 착용했던 허름하지만 위대한 투구.\n묵직하다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("마을 경비대의 갑옷[투구]", 3003, "Prefab/Item/Item_Equip/Hat/Village Guards Hat",
            E_ITEM_GRADE.COMMON, E_ITEM_EQUIP_TYPE.HAT, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S1,
            3, 0, 2100, 1);
        item.m_sStatus_Limit_Min = new STATUS(5, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Min = new SOC(20, -20, -20, -20, -20, -20, -20, -20, -20);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, -5, 1, 1, 0, 0.05f);
        item.m_sSoc_Effect = new SOC(3, 1, 2, 2, 1, 2, 1, 1, 0);
        item.m_sItemDescription = "마을 사람들의 신뢰가 쌓여야 착용할 수 있는 마을 경비대의 갑옷[투구]이다.\n착용 조건은 까다롭지만 그만큼 안정적인 착용 효과가 있는 것이 장점이다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("초급 용병의 갑옷[투구]", 3004, "Prefab/Item/Item_Equip/Hat/Low Mercenary Soldiers Hat",
            E_ITEM_GRADE.COMMON, E_ITEM_EQUIP_TYPE.HAT, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S3, E_ITEM_ADDITIONALOPTION_SOC.S3,
            3, 0, 2400, 2);
        item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, 20, -10000, -10000, -10000, 3, -10000, -10000, -10000, -10000, -10000, 3, -10000, -10000, -10000);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0);
        item.m_sSoc_Effect = new SOC(-5, 0, 0, -1, -1, -1, 0, 0, 1);
        item.m_sItemDescription = "초급 용병에게 보급되는 갑옷[투구]이다.\n신속하고 정확한 작전 수행을 위해 특수 제작되어 능력치가 상당히 좋다. 그러나 주변으로부터의 평판은 장담할 수 없다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("중급 용병의 갑옷[투구]", 3005, "Prefab/Item/Item_Equip/Hat/Middle Mercenary Soldiers Hat",
            E_ITEM_GRADE.COMMON, E_ITEM_EQUIP_TYPE.HAT, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S4, E_ITEM_ADDITIONALOPTION_SOC.S3,
            5, 0, 4400);
        item.m_sStatus_Limit_Min = new STATUS(10, -10000, -10000, 30, -10000, -10000, -10000, 5, -10000, -10000, -10000, -10000, -10000, 5, -10000, -10000, -10000);
        item.m_sStatus_Effect = new STATUS(0, 0, 0,5, 0, 0, 0, 1, 1, 1, 0, 0, 0, 2, 2, 0, 0);
        item.m_sSoc_Effect = new SOC(-10, -1, -1, -2, -2, -2, -1, -1, 2);
        item.m_sItemDescription = "용병계의 베테랑이라고 불리는 중급 용병에게 보급되는 갑옷[투구]이다.\n신속하고 정확한 작전 수행을 위해 특수 제작되어 능력치가 상당히 좋다. 그러나 주변으로부터의 평판은 장담할 수 없다.\n소문으로 이 갑옷은 '주식회사 더 슬라' 에서 제작된다고...";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("상급 용병의 갑옷[투구]", 3006, "Prefab/Item/Item_Equip/Hat/Upper Mercenary Soldiers Hat",
            E_ITEM_GRADE.RARE, E_ITEM_EQUIP_TYPE.HAT, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S5, E_ITEM_ADDITIONALOPTION_SOC.S3,
            7, 0, 7500);
        item.m_sStatus_Limit_Min = new STATUS(15, -10000, -10000, 40, -10000, -10000, -10000, 10, -10000, -10000, -10000, -10000, -10000, 5, -10000, -10000, -10000);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 10, 0, 0, 0, 2, 2, 2, 0, 0, 0, 3, 3, 0, 0);
        item.m_sSoc_Effect = new SOC(-20, -2, -2, -4, -4, -4, -2, -2, 3);
        item.m_sItemDescription = "노력 만으로는 절대 도달할 수 없는 상급 용병에게 보급되는 갑옷[투구]이다.\n능력치는 말할 것도 없다. 평판 또한.\n이 장비 덕에 상급 용병의 작전 수행률은 99% 에 수렴한다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("초급 기사의 갑옷[투구]", 3007, "Prefab/Item/Item_Equip/Hat/Low Knights Hat",
            E_ITEM_GRADE.COMMON, E_ITEM_EQUIP_TYPE.HAT, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S1,
            3, 0, 2400);
        item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, 15, -10000, -10000, -10000, 2, -10000, -10000, -10000, -10000, -10000, 2, -10000, -10000, -10000);
        item.m_sSoc_Limit_Min = new SOC(30, 0, 0, 0, 0, 0, 0, 0, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, -5, 2, 2, 0, 0);
        item.m_sSoc_Effect = new SOC(5, 1, 1, 1, 1, 1, 1, 1, -1);
        item.m_sItemDescription = "초급 기사에게 보급되는 갑옷[투구]이다.\n견뎌내고 지켜내는 임무를 수행하기 위해 특수 제작되어 능력치가 상당히 좋다. 또한 초급 기사라도 어엿한 기사인법. 그렇기에 주변으로부터의 평판도 좋다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("중급 기사의 갑옷[투구]", 3008, "Prefab/Item/Item_Equip/Hat/Middle Knights Hat",
            E_ITEM_GRADE.COMMON, E_ITEM_EQUIP_TYPE.HAT, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S1,
            5, 0, 4400);
        item.m_sStatus_Limit_Min = new STATUS(10, -10000, -10000, 25, -10000, -10000, -10000, 4, -10000, -10000, -10000, -10000, -10000, 4, -10000, -10000, -10000);
        item.m_sSoc_Limit_Min = new SOC(50, 0, 0, 0, 0, 0, 0, 0, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 5);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 10, 0, 0, 0, 1, 1, 1, 0, 0, -5, 3, 3, 0, 0);
        item.m_sSoc_Effect = new SOC(7, 2, 2, 2, 2, 2, 2, 2, -2);
        item.m_sItemDescription = "기사단의 베테랑이라고 불리는 중급 기사에게 보급되는 갑옷[투구]이다.\n여러가지 다양한 임무 수행을 위해 특수 제작되어 능력치가 상당히 좋다. 또한 주변으로부터의 평판도 좋다.\n정의감에 불타는자를 위한 장비.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("상급 기사의 갑옷[투구]", 3009, "Prefab/Item/Item_Equip/Hat/Upper Knights Hat",
            E_ITEM_GRADE.RARE, E_ITEM_EQUIP_TYPE.HAT, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S1,
            7, 0, 7500);
        item.m_sStatus_Limit_Min = new STATUS(15, -10000, -10000, 50, -10000, -10000, -10000, 5, -10000, -10000, -10000, -10000, -10000, 5, -10000, -10000, -10000);
        item.m_sSoc_Limit_Min = new SOC(100, 0, 0, 0, 0, 0, 0, 0, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 0);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 20, 0, 0, 0, 2, 2, 2, 0, 0, -5, 5, 5, 0, 0);
        item.m_sSoc_Effect = new SOC(30, 8, 8, 8, 8, 8, 8, 8, -3);
        item.m_sItemDescription = "상위 1% 의 능력을 인정받은 상급 기사에게 보급되는 갑옷[투구]이다.\n능력치는 말할 것도 없다. 평판 또한.\n이 장비 덕에 상급 기사는 임무에는 실패해도 절대 죽지는 않는다고 한다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("작업모", 3010, "Prefab/Item/Item_Equip/Hat/Working Hat",
            E_ITEM_GRADE.COMMON, E_ITEM_EQUIP_TYPE.HAT, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S2, E_ITEM_ADDITIONALOPTION_SOC.S5,
            3, 0, 1900);
        item.m_sStatus_Limit_Min = new STATUS(5, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Min = new SOC(0, 0, 0, 0, 0, 0, 0, 0, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 5, 1, 1, 0, -0.05f);
        item.m_sSoc_Effect = new SOC(1, 1, 1, 1, 1, 1, 1, 1, -1);
        item.m_sItemDescription = "다양한 일을 할 때 착용하는 작업모이다. 모험, 채집, 제작 등의 일을 함에 쓰인다.\n가벼운 소재로 만들어졌기에 잘 찢어질 것 같다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("베달부의 모자", 3011, "Prefab/Item/Item_Equip/Hat/Delivery Mans Hat",
            E_ITEM_GRADE.COMMON, E_ITEM_EQUIP_TYPE.HAT, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S2, E_ITEM_ADDITIONALOPTION_SOC.S2,
            3, 0, 2800);
        item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, 100, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Min = new SOC(0, 0, 0, 0, 0, 0, 0, 0, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, -3, 0, 0, 0, 0, 0, 0, 0, 0, 20, -3, -3, 0, -0.1f);
        item.m_sSoc_Effect = new SOC(1, 1, 1, 1, 1, 1, 1, 1, -1);
        item.m_sItemDescription = "요즘 시대에는 배달부라는 직업이 인기이다. 그런 배달부의 모자이다.\n신속함을 최우선으로 생각한다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("부실한 친환경 투구", 3012, "Prefab/Item/Item_Equip/Hat/Poor Eco Friendly Hat",
            E_ITEM_GRADE.NORMAL, E_ITEM_EQUIP_TYPE.HAT, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S2, E_ITEM_ADDITIONALOPTION_SOC.S2,
            1, 0, 3200);
        item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Min = new SOC(0, 0, 0, 0, 0, 0, 0, 0, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, -5, 2, 2, 0, 0.05f);
        item.m_sSoc_Effect = new SOC(1, 0, 1);
        item.m_sItemDescription = "나뭇가지와 풀때기를 엮어 대충 만든 부실한 친환경 투구이다.\n성능은 장담할 수 없다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("튼튼한 친환경 투구", 3013, "Prefab/Item/Item_Equip/Hat/Strong Eco Friendly Hat",
            E_ITEM_GRADE.NORMAL, E_ITEM_EQUIP_TYPE.HAT, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S2, E_ITEM_ADDITIONALOPTION_SOC.S2,
            1, 0, 4000);
        item.m_sStatus_Limit_Min = new STATUS(10, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Min = new SOC(0, 0, 0, 0, 0, 0, 0, 0, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, -5, 3, 3, 0, 0.05f);
        item.m_sSoc_Effect = new SOC(1, 0, 1);
        item.m_sItemDescription = "나뭇가지와 풀때기를 엮어 조금은 튼튼하게 만든 부실한 친환경 투구이다.\n성능은 장담할 수 없다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);


    }
    // 모든 장비아이템(상의) 데이터 로딩
    void Load_Item_Equip_Top()
    {
        Item_Equip item;

        item = new Item_Equip("초보 모험가의 상의", 4001, "Prefab/Item/Item_Equip/Top/Novice Adventurers Armor",
            E_ITEM_GRADE.NORMAL, E_ITEM_EQUIP_TYPE.TOP, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S1,
            0, 0, 800);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, -5, 1, 1, 0, 0);
        item.m_sSoc_Effect = new SOC(1);
        item.m_sItemDescription = "모험을 이제 막 시작한 초보 모험가를 위한 투구.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("허름한 기사의 갑옷", 4002, "Prefab/Item/Item_Equip/Top/Shabby Knights Armor",
            E_ITEM_GRADE.COMMON, E_ITEM_EQUIP_TYPE.TOP, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S2, E_ITEM_ADDITIONALOPTION_SOC.S2,
            1, 0, 4500);
        item.m_sStatus_Limit_Min = new STATUS(5, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, -10, 5, 5, 0, 0.1f);
        item.m_sSoc_Limit_Min = new SOC(10, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Effect = new SOC(3);
        item.m_sItemDescription = "어느 누군가의 기사, 어떤 가정의 아버지가 착용했던 허름하지만 위대한 갑옷.\n묵직하다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("사냥꾼의 상의", 4003, "Prefab/Item/Item_Equip/Top/Hunters Armor",
            E_ITEM_GRADE.NORMAL, E_ITEM_EQUIP_TYPE.TOP, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S2,
            1, 0, 2500);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3, 0, 0);
        item.m_sSoc_Effect = new SOC(3, 1, -1, -1, -1, -1, -1, -1, -1);
        item.m_sItemDescription = "사냥꾼의 상의이다.\n자신보다 약한 사냥감을 사냥하기 위해 만들어졌기에 기동성이 뛰어나지만 방어력 자체는 보잘것없다.\n사냥감들의 원한이 서려 있는듯 하다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("방랑자의 겉옷", 4004, "Prefab/Item/Item_Equip/Top/Wanderers Armor",
            E_ITEM_GRADE.RARE, E_ITEM_EQUIP_TYPE.TOP, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S3, E_ITEM_ADDITIONALOPTION_SOC.S3,
            3, 0, 6000);
        item.m_sStatus_Limit_Min = new STATUS(5, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Min = new SOC(5, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 1, 0, 1, 0, 1, 1, 1, 0, 0, 0, 1, 1, 0, 0);
        item.m_sSoc_Effect = new SOC(3, 1, 0, 0, 1, 0, 0, 0, 0);
        item.m_sItemDescription = "이 세계를 떠돌던 한 이름 없는 방랑자의 겉옷.\n그의 이름은 기억되지 않았지만 그의 물건은 세계 곳곳에서 발견되고 있다. 그는 왜 세계 곳곳을 누비고 다닌 걸까?";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("나무꾼의 겉옷", 4005, "Prefab/Item/Item_Equip/Top/Woodmans Armor",
            E_ITEM_GRADE.NORMAL, E_ITEM_EQUIP_TYPE.TOP, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S1,
            1, 0, 1800);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, -10, 1, 1, 0, 0);
        item.m_sSoc_Effect = new SOC(1, 1, 0, 0, 0, -5, 0, 0, 1);
        item.m_sItemDescription = "보통 나무꾼이 나무하러 갈 때 입는 겉옷이다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("마을 경비대의 갑옷[상의]", 4006, "Prefab/Item/Item_Equip/Top/Village Guards Armor",
            E_ITEM_GRADE.COMMON, E_ITEM_EQUIP_TYPE.TOP, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S1,
            3, 0, 4200, 1);
        item.m_sStatus_Limit_Min = new STATUS(5, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Min = new SOC(20, -20, -20, -20, -20, -20, -20, -20, -20);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, -5, 3, 3, 0, 0.05f);
        item.m_sSoc_Effect = new SOC(5, 1, 2, 2, 1, 2, 1, 1, 0);
        item.m_sItemDescription = "마을 사람들의 신뢰가 쌓여야 착용할 수 있는 마을 경비대의 갑옷[상의]이다.\n착용 조건은 까다롭지만 그만큼 안정적인 착용 효과가 있는 것이 장점이다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("따뜻한 양털 상의", 4007, "Prefab/Item/Item_Equip/Top/Warm Woolen Top",
            E_ITEM_GRADE.COMMON, E_ITEM_EQUIP_TYPE.TOP, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S2, E_ITEM_ADDITIONALOPTION_SOC.S1,
            1, 0, 4000);
        item.m_sStatus_Limit_Min = new STATUS(5, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 1, 0, 1, 1, 1, 0, 0, 5, -1, -1, 0, -0.1f);
        item.m_sSoc_Effect = new SOC(2, 0, -5, 0, 0, 0, 0, 0, 1);
        item.m_sItemDescription = "포근한 양털로 만들어진 양털 상의.\n매우 가볍지만 방어에 큰 효과는 없는듯하다.\n그러나 값비싼 양털로 만들어졌기에 값이 꾀 나가는 편이다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("대장장이의 작업복", 4008, "Prefab/Item/Item_Equip/Top/Blacksmiths Work Clothes",
            E_ITEM_GRADE.NORMAL, E_ITEM_EQUIP_TYPE.TOP, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S2,
            1, 0, 3300);
        item.m_sStatus_Limit_Min = new STATUS(1, -10000, -10000, 25, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 20, 0, 0, 0, 0, 0, 0, 0, 0, -30, 5, 5, 0, 0.1f);
        item.m_sSoc_Effect = new SOC(2, 1, 1, 1, 1, 1, 1, 1, 0);
        item.m_sItemDescription = "이 세계에 존재하는 다양한 도구를 만들어내는 대장장이의 작업복이다.\n대장장이 일은 매우 고되기에 체력이 어느 정도 뒷받침되지 않으면 착용할 수 없다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("초급 용병의 갑옷[상의]", 4009, "Prefab/Item/Item_Equip/Top/Low Mercenary Soldiers Armor",
            E_ITEM_GRADE.COMMON, E_ITEM_EQUIP_TYPE.TOP, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S3, E_ITEM_ADDITIONALOPTION_SOC.S3,
            3, 0, 4900, 2);
        item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, 20, -10000, -10000, -10000, 3, -10000, -10000, -10000, -10000, -10000, 3, -10000, -10000, -10000);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 5, 0, 0, 0, 1, 1, 1, 0, 0, -5, 3, 3, 0, 0.05f);
        item.m_sSoc_Effect = new SOC(-10, -1, -1, -2, -2, -2, -1, -1, 1);
        item.m_sItemDescription = "초급 용병에게 보급되는 갑옷[상의]이다.\n신속하고 정확한 작전 수행을 위해 특수 제작되어 능력치가 상당히 좋다. 그러나 주변으로부터의 평판은 장담할 수 없다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("중급 용병의 갑옷[상의]", 4010, "Prefab/Item/Item_Equip/Top/Middle Mercenary Soldiers Armor",
            E_ITEM_GRADE.COMMON, E_ITEM_EQUIP_TYPE.TOP, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S4, E_ITEM_ADDITIONALOPTION_SOC.S3,
            5, 0, 8900);
        item.m_sStatus_Limit_Min = new STATUS(10, -10000, -10000, 30, -10000, -10000, -10000, 5, -10000, -10000, -10000, -10000, -10000, 5, -10000, -10000, -10000);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 10, 0, 0, 0, 2, 2, 2, 0, 0, -5, 5, 5, 0, 0.05f);
        item.m_sSoc_Effect = new SOC(-20, -2, -2, -4, -4, -4, -2, -2, 2);
        item.m_sItemDescription = "용병계의 베테랑이라고 불리는 중급 용병에게 보급되는 갑옷[상의]이다.\n신속하고 정확한 작전 수행을 위해 특수 제작되어 능력치가 상당히 좋다. 그러나 주변으로부터의 평판은 장담할 수 없다.\n소문으로 이 갑옷은 '주식회사 더 슬라' 에서 제작된다고...";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("상급 용병의 갑옷[상의]", 4011, "Prefab/Item/Item_Equip/Top/Upper Mercenary Soldiers Armor",
            E_ITEM_GRADE.RARE, E_ITEM_EQUIP_TYPE.TOP, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S5, E_ITEM_ADDITIONALOPTION_SOC.S3,
            7, 0, 15000);
        item.m_sStatus_Limit_Min = new STATUS(15, -10000, -10000, 40, -10000, -10000, -10000, 10, -10000, -10000, -10000, -10000, -10000, 5, -10000, -10000, -10000);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 20, 0, 0, 0, 3, 3, 3, 0, 0, -5, 7, 7, 0, 0.05f);
        item.m_sSoc_Effect = new SOC(-40, -4, -4, -8, -8, -8, -4, -4, 5);
        item.m_sItemDescription = "노력 만으로는 절대 도달할 수 없는 상급 용병에게 보급되는 갑옷[상의]이다.\n능력치는 말할 것도 없다. 평판 또한.\n이 장비 덕에 상급 용병의 작전 수행률은 99% 에 수렴한다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("노상강도의 상의", 4012, "Prefab/Item/Item_Equip/Top/Bandits Armor",
            E_ITEM_GRADE.NORMAL, E_ITEM_EQUIP_TYPE.TOP, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S1,
            2, 0, 2100);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, -5, 1, 1, 0, -0.05f);
        item.m_sSoc_Effect = new SOC(-5, -1, -1, -1, -1, -1, -1, -1, 3);
        item.m_sItemDescription = "보잘것없는 노상강도의 상의.\n정말 보잘것없다.\n착용 시 몸은 가벼워 진것 같으나 평판이 감소한다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("초급 기사의 갑옷[상의]", 4013, "Prefab/Item/Item_Equip/Top/Low Knights Armor",
            E_ITEM_GRADE.COMMON, E_ITEM_EQUIP_TYPE.TOP, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S1,
            3, 0, 4900);
        item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, 15, -10000, -10000, -10000, 2, -10000, -10000, -10000, -10000, -10000, 2, -10000, -10000, -10000);
        item.m_sSoc_Limit_Min = new SOC(30, 0, 0, 0, 0, 0, 0, 0, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 10, 0, 0, 0, 1, 1, 1, 0, 0, -10, 4, 4, 0, 0.05f);
        item.m_sSoc_Effect = new SOC(10, 2, 2, 2, 2, 2, 2, 2, -1);
        item.m_sItemDescription = "초급 기사에게 보급되는 갑옷[상의]이다.\n견뎌내고 지켜내는 임무를 수행하기 위해 특수 제작되어 능력치가 상당히 좋다. 또한 초급 기사라도 어엿한 기사인법. 그렇기에 주변으로부터의 평판도 좋다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("중급 기사의 갑옷[상의]", 4014, "Prefab/Item/Item_Equip/Top/Middle Knights Armor",
            E_ITEM_GRADE.COMMON, E_ITEM_EQUIP_TYPE.TOP, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S1,
            5, 0, 8900);
        item.m_sStatus_Limit_Min = new STATUS(10, -10000, -10000, 25, -10000, -10000, -10000, 4, -10000, -10000, -10000, -10000, -10000, 4, -10000, -10000, -10000);
        item.m_sSoc_Limit_Min = new SOC(50, 0, 0, 0, 0, 0, 0, 0, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 5);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 20, 0, 0, 0, 2, 2, 2, 0, 0, -10, 6, 6, 0, 0.05f);
        item.m_sSoc_Effect = new SOC(15, 4, 4, 4, 4, 4, 4, 4, -2);
        item.m_sItemDescription = "기사단의 베테랑이라고 불리는 중급 기사에게 보급되는 갑옷[상의]이다.\n여러가지 다양한 임무 수행을 위해 특수 제작되어 능력치가 상당히 좋다. 또한 주변으로부터의 평판도 좋다.\n정의감에 불타는자를 위한 장비.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("상급 기사의 갑옷[상의]", 4015, "Prefab/Item/Item_Equip/Top/Upper Knights Armor",
            E_ITEM_GRADE.RARE, E_ITEM_EQUIP_TYPE.TOP, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S1,
            7, 0, 15000);
        item.m_sStatus_Limit_Min = new STATUS(15, -10000, -10000, 50, -10000, -10000, -10000, 5, -10000, -10000, -10000, -10000, -10000, 5, -10000, -10000, -10000);
        item.m_sSoc_Limit_Min = new SOC(100, 0, 0, 0, 0, 0, 0, 0, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 0);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 40, 0, 0, 0, 4, 4, 4, 0, 0, -10, 8, 8, 0, 0.05f);
        item.m_sSoc_Effect = new SOC(30, 8, 8, 8, 8, 8, 8, 8, -3);
        item.m_sItemDescription = "상위 1% 의 능력을 인정받은 상급 기사에게 보급되는 갑옷[상의]이다.\n능력치는 말할 것도 없다. 평판 또한.\n이 장비 덕에 상급 기사는 임무에는 실패해도 절대 죽지는 않는다고 한다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("반짝이는 구릿빛 갑옷", 4016, "Prefab/Item/Item_Equip/Top/Sparkling Copper Armor",
            E_ITEM_GRADE.RARE, E_ITEM_EQUIP_TYPE.TOP, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S3, E_ITEM_ADDITIONALOPTION_SOC.S3,
            7, 0, 11000);
        item.m_sStatus_Limit_Min = new STATUS(12, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 15, 0, 0, 0, 2, 2, 2, 0, 0, 10, 3, 3, 0, -0.1f);
        item.m_sSoc_Effect = new SOC(3, 0, 0, 0, 0, 0, 0, 0, 0);
        item.m_sItemDescription = "반짝이는 구릿빛 갑옷이다.\n구리로 만들어 가볍지만 내구성은 떨어진다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("빛나는 은빛 갑옷", 4017, "Prefab/Item/Item_Equip/Top/Shining Sliver Armor",
            E_ITEM_GRADE.RARE, E_ITEM_EQUIP_TYPE.TOP, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S4, E_ITEM_ADDITIONALOPTION_SOC.S4,
            7, 0, 13000);
        item.m_sStatus_Limit_Min = new STATUS(12, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 25, 0, 0, 0, 3, 3, 3, 0, 0, -5, 5, 5, 0, 0.05f);
        item.m_sSoc_Effect = new SOC(4, 0, 0, 0, 0, 0, 0, 0, 0);
        item.m_sItemDescription = "빛나는 은빛 갑옷이다.\n은으로 만들어 은은한 고급미와 세련미가 돋보인다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("화려한 금빛 갑옷", 4018, "Prefab/Item/Item_Equip/Top/Fancy Gold Armor",
            E_ITEM_GRADE.RARE, E_ITEM_EQUIP_TYPE.TOP, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S5, E_ITEM_ADDITIONALOPTION_SOC.S5,
            7, 0, 15000);
        item.m_sStatus_Limit_Min = new STATUS(12, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 40, 0, 0, 0, 4, 4, 4, 0, 0, -10, 7, 7, 0, 0.05f);
        item.m_sSoc_Effect = new SOC(5, 0, 0, 0, 0, 0, 0, 0, 0);
        item.m_sItemDescription = "화려한 금빛 갑옷이다.\n금과 여러 사치스러운 재료로 만들어 매우 화려하다.\n좋은 재료는 다 들어있기에 성능은 확실하다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);
    }
    // 모든 장비아이템(하의) 데이터 로딩
    void Load_Item_Equip_Bottoms()
    {
        Item_Equip item;

        item = new Item_Equip("마을 경비대의 갑옷[하의]", 5001, "Prefab/Item/Item_Equip/Bottoms/Village Guards Pants",
            E_ITEM_GRADE.COMMON, E_ITEM_EQUIP_TYPE.BOTTOMS, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S1,
            3, 0, 2100, 1);
        item.m_sStatus_Limit_Min = new STATUS(5, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Min = new SOC(20, -20, -20, -20, -20, -20, -20, -20, -20);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, -5, 1, 1, 0, 0.05f);
        item.m_sSoc_Effect = new SOC(3, 1, 2, 2, 1, 2, 1, 1, 0);
        item.m_sItemDescription = "마을 사람들의 신뢰가 쌓여야 착용할 수 있는 마을 경비대의 갑옷[하의]이다.\n착용 조건은 까다롭지만 그만큼 안정적인 착용 효과가 있는 것이 장점이다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("초급 용병의 갑옷[하의]", 5002, "Prefab/Item/Item_Equip/Bottoms/Low Mercenary Soldiers Pants",
            E_ITEM_GRADE.COMMON, E_ITEM_EQUIP_TYPE.BOTTOMS, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S3, E_ITEM_ADDITIONALOPTION_SOC.S3,
            3, 0, 2400, 2);
        item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, 20, -10000, -10000, -10000, 3, -10000, -10000, -10000, -10000, -10000, 3, -10000, -10000, -10000);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0);
        item.m_sSoc_Effect = new SOC(-5, 0, 0, -1, -1, -1, 0, 0, 1);
        item.m_sItemDescription = "초급 용병에게 보급되는 갑옷[하의]이다.\n신속하고 정확한 작전 수행을 위해 특수 제작되어 능력치가 상당히 좋다. 그러나 주변으로부터의 평판은 장담할 수 없다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("중급 용병의 갑옷[하의]", 5003, "Prefab/Item/Item_Equip/Bottoms/Middle Mercenary Soldiers Pants",
            E_ITEM_GRADE.COMMON, E_ITEM_EQUIP_TYPE.BOTTOMS, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S4, E_ITEM_ADDITIONALOPTION_SOC.S3,
            5, 0, 4400);
        item.m_sStatus_Limit_Min = new STATUS(10, -10000, -10000, 30, -10000, -10000, -10000, 5, -10000, -10000, -10000, -10000, -10000, 5, -10000, -10000, -10000);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 5, 0, 0, 0, 1, 1, 1, 0, 0, 0, 2, 2, 0, 0);
        item.m_sSoc_Effect = new SOC(-10, -1, -1, -2, -2, -2, -1, -1, 2);
        item.m_sItemDescription = "용병계의 베테랑이라고 불리는 중급 용병에게 보급되는 갑옷[하의]이다.\n신속하고 정확한 작전 수행을 위해 특수 제작되어 능력치가 상당히 좋다. 그러나 주변으로부터의 평판은 장담할 수 없다.\n소문으로 이 갑옷은 '주식회사 더 슬라' 에서 제작된다고...";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("상급 용병의 갑옷[하의]", 5004, "Prefab/Item/Item_Equip/Bottoms/Upper Mercenary Soldiers Pants",
            E_ITEM_GRADE.RARE, E_ITEM_EQUIP_TYPE.BOTTOMS, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S5, E_ITEM_ADDITIONALOPTION_SOC.S3,
            7, 0, 7500);
        item.m_sStatus_Limit_Min = new STATUS(15, -10000, -10000, 40, -10000, -10000, -10000, 10, -10000, -10000, -10000, -10000, -10000, 5, -10000, -10000, -10000);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 10, 0, 0, 0, 2, 2, 2, 0, 0, 0, 3, 3, 0, 0);
        item.m_sSoc_Effect = new SOC(-20, -2, -2, -4, -4, -4, -2, -2, 3);
        item.m_sItemDescription = "노력 만으로는 절대 도달할 수 없는 상급 용병에게 보급되는 갑옷[하의]이다.\n능력치는 말할 것도 없다. 평판 또한.\n이 장비 덕에 상급 용병의 작전 수행률은 99% 에 수렴한다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("초급 기사의 갑옷[하의]", 5005, "Prefab/Item/Item_Equip/Bottoms/Low Knights Pants",
            E_ITEM_GRADE.COMMON, E_ITEM_EQUIP_TYPE.BOTTOMS, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S1,
            3, 0, 2400);
        item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, 15, -10000, -10000, -10000, 2, -10000, -10000, -10000, -10000, -10000, 2, -10000, -10000, -10000);
        item.m_sSoc_Limit_Min = new SOC(30, 0, 0, 0, 0, 0, 0, 0, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, -5, 2, 2, 0, 0);
        item.m_sSoc_Effect = new SOC(5, 1, 1, 1, 1, 1, 1, 1, -1);
        item.m_sItemDescription = "초급 기사에게 보급되는 갑옷[하의]이다.\n견뎌내고 지켜내는 임무를 수행하기 위해 특수 제작되어 능력치가 상당히 좋다. 또한 초급 기사라도 어엿한 기사인법. 그렇기에 주변으로부터의 평판도 좋다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("중급 기사의 갑옷[하의]", 5006, "Prefab/Item/Item_Equip/Bottoms/Middle Knights Pants",
            E_ITEM_GRADE.COMMON, E_ITEM_EQUIP_TYPE.BOTTOMS, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S1,
            5, 0, 4400);
        item.m_sStatus_Limit_Min = new STATUS(10, -10000, -10000, 25, -10000, -10000, -10000, 4, -10000, -10000, -10000, -10000, -10000, 4, -10000, -10000, -10000);
        item.m_sSoc_Limit_Min = new SOC(50, 0, 0, 0, 0, 0, 0, 0, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 5);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 10, 0, 0, 0, 1, 1, 1, 0, 0, -5, 3, 3, 0, 0);
        item.m_sSoc_Effect = new SOC(7, 2, 2, 2, 2, 2, 2, 2, -2);
        item.m_sItemDescription = "기사단의 베테랑이라고 불리는 중급 기사에게 보급되는 갑옷[하의]이다.\n여러가지 다양한 임무 수행을 위해 특수 제작되어 능력치가 상당히 좋다. 또한 주변으로부터의 평판도 좋다.\n정의감에 불타는자를 위한 장비.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("상급 기사의 갑옷[하의]", 5007, "Prefab/Item/Item_Equip/Bottoms/Upper Knights Pants",
            E_ITEM_GRADE.RARE, E_ITEM_EQUIP_TYPE.BOTTOMS, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S1,
            7, 0, 7500);
        item.m_sStatus_Limit_Min = new STATUS(15, -10000, -10000, 50, -10000, -10000, -10000, 5, -10000, -10000, -10000, -10000, -10000, 5, -10000, -10000, -10000);
        item.m_sSoc_Limit_Min = new SOC(100, 0, 0, 0, 0, 0, 0, 0, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 0);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 20, 0, 0, 0, 2, 2, 2, 0, 0, -5, 5, 5, 0, 0);
        item.m_sSoc_Effect = new SOC(30, 8, 8, 8, 8, 8, 8, 8, -3);
        item.m_sItemDescription = "상위 1% 의 능력을 인정받은 상급 기사에게 보급되는 갑옷[하의]이다.\n능력치는 말할 것도 없다. 평판 또한.\n이 장비 덕에 상급 기사는 임무에는 실패해도 절대 죽지는 않는다고 한다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("방랑자의 겉옷", 5008, "Prefab/Item/Item_Equip/Bottoms/Wanderers Pants",
            E_ITEM_GRADE.RARE, E_ITEM_EQUIP_TYPE.BOTTOMS, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S3, E_ITEM_ADDITIONALOPTION_SOC.S3,
            3, 0, 4000);
        item.m_sStatus_Limit_Min = new STATUS(5, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Min = new SOC(5, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 1, 0, 1, 0, 1, 1, 1, 0, 0, 0, 1, 1, 0, 0);
        item.m_sSoc_Effect = new SOC(3, 1, 0, 0, 1, 0, 0, 0, 0);
        item.m_sItemDescription = "이 세계를 떠돌던 한 이름 없는 방랑자의 하의.\n그의 이름은 기억되지 않았지만 그의 물건은 세계 곳곳에서 발견되고 있다. 그는 왜 세계 곳곳을 누비고 다닌 걸까?";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);
    }
    // 모든 장비아이템(신발) 데이터 로딩
    void Load_Item_Equip_Shose()
    {
        Item_Equip item;

        item = new Item_Equip("초보 모험가의 신발", 6001, "Prefab/Item/Item_Equip/Shoes/Novice Adventurers Shoes",
            E_ITEM_GRADE.NORMAL , E_ITEM_EQUIP_TYPE.SHOSE, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S1,
            0, 0, 500);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 1, 1, 0, 0);
        item.m_sStatus_Limit_Min = new STATUS(1, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(1);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "모험을 이제 막 시작한 초보 모험가를 위한 신발.\n어디에서나 쉽게 구할 수 있다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("허름한 기사의 장화", 6002, "Prefab/Item/Item_Equip/Shoes/Shabby Knights Shoes",
            E_ITEM_GRADE.COMMON, E_ITEM_EQUIP_TYPE.SHOSE, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S2, E_ITEM_ADDITIONALOPTION_SOC.S2,
            1, 0, 3000);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 2, 2, 0, 0.1f);
        item.m_sStatus_Limit_Min = new STATUS(5, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(3);
        item.m_sSoc_Limit_Min = new SOC(10, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "어느 누군가의 기사, 어떤 가정의 아버지가 착용했던 허름하지만 위대한 장화.\n묵직하다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("마을 경비대의 갑옷[장화]", 6003, "Prefab/Item/Item_Equip/Shoes/Village Guards Shoes",
            E_ITEM_GRADE.COMMON, E_ITEM_EQUIP_TYPE.SHOSE, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S1,
            3, 0, 2100, 1);
        item.m_sStatus_Limit_Min = new STATUS(5, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Min = new SOC(20, -20, -20, -20, -20, -20, -20, -20, -20);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 5, 1, 1, 0, 0);
        item.m_sSoc_Effect = new SOC(3, 1, 2, 2, 1, 2, 1, 1, 0);
        item.m_sItemDescription = "마을 사람들의 신뢰가 쌓여야 착용할 수 있는 마을 경비대의 갑옷[장화]이다.\n착용 조건은 까다롭지만 그만큼 안정적인 착용 효과가 있는 것이 장점이다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("초급 용병의 갑옷[장화]", 6004, "Prefab/Item/Item_Equip/Shoes/Low Mercenary Soldiers Shoes",
            E_ITEM_GRADE.COMMON, E_ITEM_EQUIP_TYPE.SHOSE, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S3, E_ITEM_ADDITIONALOPTION_SOC.S3,
            3, 0, 2400, 2);
        item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, 20, -10000, -10000, -10000, 3, -10000, -10000, -10000, -10000, -10000, 3, -10000, -10000, -10000);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 5, 1, 1, 0, 0);
        item.m_sSoc_Effect = new SOC(-5, 0, 0, -1, -1, -1, 0, 0, 1);
        item.m_sItemDescription = "초급 용병에게 보급되는 갑옷[장화]이다.\n신속하고 정확한 작전 수행을 위해 특수 제작되어 능력치가 상당히 좋다. 그러나 주변으로부터의 평판은 장담할 수 없다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("중급 용병의 갑옷[장화]", 6005, "Prefab/Item/Item_Equip/Shoes/Middle Mercenary Soldiers Shoes",
            E_ITEM_GRADE.COMMON, E_ITEM_EQUIP_TYPE.SHOSE, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S4, E_ITEM_ADDITIONALOPTION_SOC.S3,
            5, 0, 4400);
        item.m_sStatus_Limit_Min = new STATUS(10, -10000, -10000, 30, -10000, -10000, -10000, 5, -10000, -10000, -10000, -10000, -10000, 5, -10000, -10000, -10000);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 5, 0, 0, 0, 1, 1, 1, 0, 0, 5, 2, 2, 0, 0);
        item.m_sSoc_Effect = new SOC(-10, -1, -1, -2, -2, -2, -1, -1, 2);
        item.m_sItemDescription = "용병계의 베테랑이라고 불리는 중급 용병에게 보급되는 갑옷[장화]이다.\n신속하고 정확한 작전 수행을 위해 특수 제작되어 능력치가 상당히 좋다. 그러나 주변으로부터의 평판은 장담할 수 없다.\n소문으로 이 갑옷은 '주식회사 더 슬라' 에서 제작된다고...";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("상급 용병의 갑옷[장화]", 6006, "Prefab/Item/Item_Equip/Shoes/Upper Mercenary Soldiers Shoes",
            E_ITEM_GRADE.RARE, E_ITEM_EQUIP_TYPE.SHOSE, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S5, E_ITEM_ADDITIONALOPTION_SOC.S3,
            7, 0, 7500);
        item.m_sStatus_Limit_Min = new STATUS(15, -10000, -10000, 40, -10000, -10000, -10000, 10, -10000, -10000, -10000, -10000, -10000, 5, -10000, -10000, -10000);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 10, 0, 0, 0, 2, 2, 2, 0, 0, 10, 3, 3, 0, 0);
        item.m_sSoc_Effect = new SOC(-20, -2, -2, -4, -4, -4, -2, -2, 3);
        item.m_sItemDescription = "노력 만으로는 절대 도달할 수 없는 상급 용병에게 보급되는 갑옷[장화]이다.\n능력치는 말할 것도 없다. 평판 또한.\n이 장비 덕에 상급 용병의 작전 수행률은 99% 에 수렴한다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("초급 기사의 갑옷[장화]", 6007, "Prefab/Item/Item_Equip/Shoes/Low Knights Shoes",
            E_ITEM_GRADE.COMMON, E_ITEM_EQUIP_TYPE.SHOSE, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S1,
            3, 0, 2400);
        item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, 15, -10000, -10000, -10000, 2, -10000, -10000, -10000, -10000, -10000, 2, -10000, -10000, -10000);
        item.m_sSoc_Limit_Min = new SOC(30, 0, 0, 0, 0, 0, 0, 0, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0);
        item.m_sSoc_Effect = new SOC(5, 1, 1, 1, 1, 1, 1, 1, -1);
        item.m_sItemDescription = "초급 기사에게 보급되는 갑옷[장화]이다.\n견뎌내고 지켜내는 임무를 수행하기 위해 특수 제작되어 능력치가 상당히 좋다. 또한 초급 기사라도 어엿한 기사인법. 그렇기에 주변으로부터의 평판도 좋다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);

        item = new Item_Equip("중급 기사의 갑옷[장화]", 6008, "Prefab/Item/Item_Equip/Shoes/Middle Knights Shoes",
            E_ITEM_GRADE.COMMON, E_ITEM_EQUIP_TYPE.SHOSE, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S1,
            5, 0, 4400);
        item.m_sStatus_Limit_Min = new STATUS(10, -10000, -10000, 25, -10000, -10000, -10000, 4, -10000, -10000, -10000, -10000, -10000, 4, -10000, -10000, -10000);
        item.m_sSoc_Limit_Min = new SOC(50, 0, 0, 0, 0, 0, 0, 0, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 5);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 10, 0, 0, 0, 1, 1, 1, 0, 0, 0, 3, 3, 0, 0);
        item.m_sSoc_Effect = new SOC(7, 2, 2, 2, 2, 2, 2, 2, -2);
        item.m_sItemDescription = "기사단의 베테랑이라고 불리는 중급 기사에게 보급되는 갑옷[장화]이다.\n여러가지 다양한 임무 수행을 위해 특수 제작되어 능력치가 상당히 좋다. 또한 주변으로부터의 평판도 좋다.\n정의감에 불타는자를 위한 장비.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);


        item = new Item_Equip("상급 기사의 갑옷[장화]", 6009, "Prefab/Item/Item_Equip/Shoes/Upper Knights Shoes",
            E_ITEM_GRADE.RARE, E_ITEM_EQUIP_TYPE.SHOSE, E_ITEM_EQUIP_MAINWEAPON_TYPE.NULL,
            E_ITEM_ADDITIONALOPTION_STATUS.S1, E_ITEM_ADDITIONALOPTION_SOC.S1,
            7, 0, 7500);
        item.m_sStatus_Limit_Min = new STATUS(15, -10000, -10000, 50, -10000, -10000, -10000, 5, -10000, -10000, -10000, -10000, -10000, 5, -10000, -10000, -10000);
        item.m_sSoc_Limit_Min = new SOC(100, 0, 0, 0, 0, 0, 0, 0, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 0);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 20, 0, 0, 0, 2, 2, 2, 0, 0, 5, 5, 5, 0, 0);
        item.m_sSoc_Effect = new SOC(30, 8, 8, 8, 8, 8, 8, 8, -3);
        item.m_sItemDescription = "상위 1% 의 능력을 인정받은 상급 기사에게 보급되는 갑옷[장화]이다.\n능력치는 말할 것도 없다. 평판 또한.\n이 장비 덕에 상급 기사는 임무에는 실패해도 절대 죽지는 않는다고 한다.";
        m_Dictionary_MonsterDrop_Equip.Add(item.m_nItemCode, item);
    }
    // 소비아이템(기프트)을 제외한 모든 소비아이템 데이터 로딩
    void Load_Item_Use()
    {
        Load_Item_Use_RecoverPotion();       // 모든 소비아이템(회복포션) 데이터 로딩
        Load_Item_Use_TemporaryBuffPotion(); // 모든 소비아이템(일시적 버프포션) 데이터 로딩
        Load_Item_Use_EternalBuffPotion();   // 모든 소비아이템(영구적 버프포션) 데이터 로딩
        Load_Item_Use_Reinforcement();       // 모든 소비아이템(강화서) 데이터 로딩
    }
    // 모든 소비아이템(회복포션) 데이터 로딩
    void Load_Item_Use_RecoverPotion()
    {
        Item_Use item;

        item = new Item_Use("최하급 체력 회복 포션", 8000, "Prefab/Item/Item_Use/Red Potion", E_ITEM_USE_TYPE.RECOVERPOTION, E_ITEM_GRADE.NORMAL,
            0, 0, 75);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 5);
        item.m_sStatus_Limit_Min = new STATUS(1, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "체력을 5 회복 시켜준다.";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

        item = new Item_Use("최하급 마나 회복 포션", 8001, "Prefab/Item/Item_Use/Blue Potion", E_ITEM_USE_TYPE.RECOVERPOTION, E_ITEM_GRADE.NORMAL,
            0, 0, 75);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 5);
        item.m_sStatus_Limit_Min = new STATUS(1, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "마나를 5 회복 시켜준다.";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

        item = new Item_Use("최하급 회복 포션", 8002, "Prefab/Item/Item_Etc/Green Potion", E_ITEM_USE_TYPE.RECOVERPOTION, E_ITEM_GRADE.NORMAL,
            0, 0, 100);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 5, 0, 5);
        item.m_sStatus_Limit_Min = new STATUS(1, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "체력과 마나를 5 회복 시켜준다.";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

        item = new Item_Use("하급 체력 회복 포션", 8003, "Prefab/Item/Item_Use/Red Potion 2", E_ITEM_USE_TYPE.RECOVERPOTION, E_ITEM_GRADE.NORMAL,
            0, 0, 200);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 20);
        item.m_sStatus_Limit_Min = new STATUS(10, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "체력을 20 회복 시켜준다.";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

        item = new Item_Use("하급 마나 회복 포션", 8004, "Prefab/Item/Item_Use/Blue Potion 2", E_ITEM_USE_TYPE.RECOVERPOTION, E_ITEM_GRADE.NORMAL,
            0, 0, 200);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 20);
        item.m_sStatus_Limit_Min = new STATUS(10, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "마나를 20 회복 시켜준다.";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

        item = new Item_Use("하급 회복 포션", 8005, "Prefab/Item/Item_Use/Green Potion 2", E_ITEM_USE_TYPE.RECOVERPOTION, E_ITEM_GRADE.NORMAL,
            0, 0, 300);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 20, 0, 20);
        item.m_sStatus_Limit_Min = new STATUS(10, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "체력과 마나를 20 회복 시켜준다.";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

        item = new Item_Use("중급 체력 회복 포션", 8006, "Prefab/Item/Item_Use/Red Potion", E_ITEM_USE_TYPE.RECOVERPOTION, E_ITEM_GRADE.NORMAL,
            0, 0, 400);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 50);
        item.m_sStatus_Limit_Min = new STATUS(15, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "체력을 50 회복 시켜준다.";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

        item = new Item_Use("중급 마나 회복 포션", 8007, "Prefab/Item/Item_Use/Blue Potion", E_ITEM_USE_TYPE.RECOVERPOTION, E_ITEM_GRADE.NORMAL,
            0, 0, 400);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 50);
        item.m_sStatus_Limit_Min = new STATUS(15, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "마나를 50 회복 시켜준다.";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

        item = new Item_Use("중급 회복 포션", 8008, "Prefab/Item/Item_Etc/Green Potion", E_ITEM_USE_TYPE.RECOVERPOTION, E_ITEM_GRADE.NORMAL,
            0, 0, 600);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 50, 0, 50);
        item.m_sStatus_Limit_Min = new STATUS(15, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "체력과 마나를 50 회복 시켜준다.";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

        item = new Item_Use("맛좋은 거대 사과", 8009, "Prefab/Item/Item_Use/Apple", E_ITEM_USE_TYPE.RECOVERPOTION, E_ITEM_GRADE.NORMAL,
            0, 0, 40);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 10, 0, 1);
        item.m_sStatus_Limit_Min = new STATUS(5, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "'드넓은 초원' 의 영양분을 듬뿍 먹고 자란 거대한 사과다.\n크기에 비해 맛은 그닥이지만 아삭아삭한 식감이 일품이다.\n체력을 10, 마나를 1 회복 시켜준다. ";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

        item = new Item_Use("신선한 아보카도", 8010, "Prefab/Item/Item_Use/Avocado", E_ITEM_USE_TYPE.RECOVERPOTION, E_ITEM_GRADE.COMMON,
            0, 0, 100);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 3);
        item.m_sStatus_Limit_Min = new STATUS(1, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "저~ 멀리 바다 건너왔다는 귀한 식재료인 아보카도.\n보통 아보카도를 다른 식재료와 곁들여 먹는다.\n바다를 건너왔기에 값이 비싼편이다.\n체력을 3 회복 시켜준다. ";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

        item = new Item_Use("달짝지근한 꿀", 8011, "Prefab/Item/Item_Use/Honey", E_ITEM_USE_TYPE.RECOVERPOTION, E_ITEM_GRADE.RARE,
            0, 10, 200);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 20, 0, 20);
        item.m_sStatus_Limit_Min = new STATUS(1, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "설명이 필요한가? 미네랄 등 각종 영양소가 풍부한 '드넓은 초원' 에서난 꿀이다.\n엄청 달고 귀한 식재료다. 값은 비싼편.\n체력과 마나를 20 회복 시켜준다.\n너무 달아서 다시 먹으려면 10초를 기다려야한다.";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

        item = new Item_Use("올리브", 8012, "Prefab/Item/Item_Use/Olive", E_ITEM_USE_TYPE.RECOVERPOTION, E_ITEM_GRADE.NORMAL,
            0, 0, 60);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 1, 0, 3);
        item.m_sStatus_Limit_Min = new STATUS(1, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "그냥 곁들여먹기 좋은 정도인 올리브 열매이다.\n맛은 별로 없다.\n체력을 1, 마나를 3 회복 시켜준다. ";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

        item = new Item_Use("초 고단백 굼뱅이", 8013, "Prefab/Item/Item_Use/Bug", E_ITEM_USE_TYPE.RECOVERPOTION, E_ITEM_GRADE.COMMON,
            0, 10, 20);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 10, 0, 0);
        item.m_sStatus_Limit_Min = new STATUS(1, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "'드넓은 초원' 에 널리고 널린 굼뱅이다.\n먹으려면 상당한 용기가 필요하다. 또 끔찍한 맛을 자랑한다...\n체력을 10 회복 시켜준다. 정말 필요할때만 먹도록 하자.\n끔찍한 굼뱅이의 생김새 때문에 다시 먹으려면 10초를 기다려야한다.";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

        item = new Item_Use("오래된 빵", 8014, "Prefab/Item/Item_Use/Bread", E_ITEM_USE_TYPE.RECOVERPOTION, E_ITEM_GRADE.NORMAL,
            0, 0, 70);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 15);
        item.m_sStatus_Limit_Min = new STATUS(5, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "누군가에게 잊혀진 오래된 빵이다. 오래되어 딱딱하고 맛은 없지만 먹을만하다. 체력을 15 회복시켜준다.";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

        item = new Item_Use("시큼한 체리", 8015, "Prefab/Item/Item_Use/Cherry", E_ITEM_USE_TYPE.RECOVERPOTION, E_ITEM_GRADE.COMMON,
            0, 0, 20);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 2);
        item.m_sStatus_Limit_Min = new STATUS(1, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "입에 넣는 순간 표정이 찡그려질 정도로 시큼한 체리다. 보통은 바로 먹지 않고 젬이나 음료로 만들어 먹는다.\n체력을 2 회복시켜준다.";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

        item = new Item_Use("달달~ 한 체리 꿀 젬", 8017, "Prefab/Item/Item_Use/61_jam", E_ITEM_USE_TYPE.RECOVERPOTION, E_ITEM_GRADE.RARE,
            0, 0, 310);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 30, 0, 20, 0);
        item.m_sStatus_Limit_Min = new STATUS(5, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "'달짝지근한 꿀' 과 으깬 '시큼한 체리' 를 약불에 졸여서 만든 딱 먹기 좋은 정도의 잼이다.\n맛이 매우 좋은 편이고 값이 꽤나 나간다.\n체력을 30, 마나를 20 회복시켜준다.";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

        item = new Item_Use("체리주스", 8018, "Prefab/Item/Item_Use/Moonshine", E_ITEM_USE_TYPE.RECOVERPOTION, E_ITEM_GRADE.COMMON,
            0, 5, 70);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 7, 0, 2, 0);
        item.m_sStatus_Limit_Min = new STATUS(5, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "'시큼한 체리' 를 발효시켜 만든 체리주스이다.\n달콤하지만 약간 시큼한 맛에 남녀노소가 모두 즐긴다. 그러나 발효 음료의 특성상 너무 많이 마시게 된다면 조금은 취할 수 있다.\n체력을 7, 마나를 2 회복시켜준다.\n다시 마시려면 5초를 기다려야 한다.";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

        item = new Item_Use("퍽퍽한 감자", 8019, "Prefab/Item/Item_Use/Potato", E_ITEM_USE_TYPE.RECOVERPOTION, E_ITEM_GRADE.NORMAL,
            0, 0, 20);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 1, 0, 0, 0);
        item.m_sStatus_Limit_Min = new STATUS(1, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "척박한 환경에서 제멋대로 막 기른 퍽퍽한 감자이다.\n너무 퍽퍽해서 물 없이 먹었다가는 질식사할 수도 있으니 조심하자. 그럼에도 다양한 음식으로 요리할 수 있어 가장 보편화된 식재료이다.\n체력을 1 회복시켜준다.";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

        item = new Item_Use("퍽퍽한 감자 쿠키", 8020, "Prefab/Item/Item_Use/Cookie", E_ITEM_USE_TYPE.RECOVERPOTION, E_ITEM_GRADE.NORMAL,
            0, 0, 30);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 5, 0, 0, 0);
        item.m_sStatus_Limit_Min = new STATUS(5, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "'퍽퍽한 감자' 로 만든 쿠키다. 퍽퍽함은 그대로지만 나름 먹을만하다.\n체력을 5 회복시켜준다.";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

        item = new Item_Use("프리미엄 감자", 8021, "Prefab/Item/Item_Use/PotatoRed", E_ITEM_USE_TYPE.RECOVERPOTION, E_ITEM_GRADE.COMMON,
            0, 0, 50);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 3, 0, 0, 0);
        item.m_sStatus_Limit_Min = new STATUS(5, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "엄선된 환경에서 제대로 기른 붉은빛을 띄는 프리미엄 감자이다.\n적당한 정도의 퍽퍽함과 단맛이 일품이다. 또한 다양한 고급 음식으로 요리할 수 있어 '퍽퍽한 감자' 에 비해 가격이 2.5배 정도 비싸다.\n체력을 3 회복시켜준다.";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

        item = new Item_Use("짭짤이 토마토", 8022, "Prefab/Item/Item_Use/Tomato", E_ITEM_USE_TYPE.RECOVERPOTION, E_ITEM_GRADE.COMMON,
            0, 0, 30);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 2, 0, 2, 0);
        item.m_sStatus_Limit_Min = new STATUS(5, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "볕이 좋은 곳에서 땅의 영양분을 듬뿍 먹고 자란 짭짤이 토마토이다.\n토마토 특유의 향과 짭짤함이 베여있어 중독성 있는 맛을 자랑한다.\n체력을 2, 마나를 2 회복시켜준다.";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

        item = new Item_Use("네가지 맛 감자 롤", 8023, "Prefab/Item/Item_Use/Roll", E_ITEM_USE_TYPE.RECOVERPOTION, E_ITEM_GRADE.RARE,
            0, 0, 110);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 30, 0, 0, 0);
        item.m_sStatus_Limit_Min = new STATUS(5, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "'슬라임 할머니' 가 아들에게 자주 만들어주던 요리이다.\n'프리미엄 감자' 와 '짭짤이 토마토' 로 만들었기에 네가지 맛이 난다.\n입에 넣으면 단맛, 짠맛, 토마토향, 퍽퍽함 이 조화롭게 춤춘다.\n체력을 30 회복시켜준다.";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

        item = new Item_Use("평범한 계란", 8024, "Prefab/Item/Item_Use/Egg", E_ITEM_USE_TYPE.RECOVERPOTION, E_ITEM_GRADE.NORMAL,
            0, 0, 15);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 1, 0, 0, 0);
        item.m_sStatus_Limit_Min = new STATUS(5, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "이 세계에서 쉽게 구할 수 있는 평범한 계란이다. 주로 다른 식재료를 곁들여 요리해 먹는다.\n체력을 1회복시켜준다.";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

        item = new Item_Use("오묘한 청란", 8025, "Prefab/Item/Item_Use/Monster Egg", E_ITEM_USE_TYPE.RECOVERPOTION, E_ITEM_GRADE.RARE,
            0, 0, 250);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0);
        item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "매우 희귀하게 발견되는 오묘하게 생긴 청란이다.\n푸른빛 바탕의 무늬가 인상적이기에 장식품으로도 사용되고 다양한 고급 음식으로 요리할 수 있어 값이 비싼 편이다.\n섭취 시 아무런 효과도 얻을 수 없다. 조리가 필요하다.";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

        item = new Item_Use("맛있는 소세지", 8026, "Prefab/Item/Item_Use/Sausages", E_ITEM_USE_TYPE.RECOVERPOTION, E_ITEM_GRADE.COMMON,
            0, 0, 80);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 7);
        item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "잡다한 고기로 만든 맛있는 소세지다.\n무슨 고기로 만들었는지 몰라서 조금은 찝찝하지만 맛은 좋다.\n체력을 7 회복시켜준다.";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

        item = new Item_Use("바삭 베이컨", 8027, "Prefab/Item/Item_Use/13_bacon", E_ITEM_USE_TYPE.RECOVERPOTION, E_ITEM_GRADE.COMMON,
            0, 0, 60);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 5);
        item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "바삭바삭 거리는 식감이 일품인 바삭 베이컨이다. 체력을 5 회복시켜준다.";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

        item = new Item_Use("따끈다끈한 빵", 8028, "Prefab/Item/Item_Use/07_bread", E_ITEM_USE_TYPE.RECOVERPOTION, E_ITEM_GRADE.COMMON,
            0, 0, 120);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 20);
        item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "갓 구워낸 따끈따끈한 빵이다.\n매우 많은 활용처가 있지만 그냥 먹어도 맛있다.\n이 세계 전역에서 가장 보편화된 음식이다.\n체력을 20 회복시켜준다.";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

        item = new Item_Use("평범한 핫도그", 8029, "Prefab/Item/Item_Use/54_hotdog", E_ITEM_USE_TYPE.RECOVERPOTION, E_ITEM_GRADE.COMMON,
            0, 0, 210);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 30);
        item.m_sStatus_Limit_Min = new STATUS(12, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "핫도그. 말 그대로 핫도그. 핫도그를 모르는 사람은 없을 것이다. 그냥 평범하게 맛있는 핫도그이다.\n체력을 30 회복시켜준다.";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

        item = new Item_Use("소스가 뿌려진 핫도그", 8030, "Prefab/Item/Item_Use/55_hotdog_sauce", E_ITEM_USE_TYPE.RECOVERPOTION, E_ITEM_GRADE.COMMON,
            0, 0, 230);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 35, 0, 5);
        item.m_sStatus_Limit_Min = new STATUS(12, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "소스가 뿌려진 핫도그. 말 그대로 소스가 뿌려진 핫도그. 핫도그를 모르는 사람은 없을 것이다. 그냥 소스가 뿌려져 더 맛있는 핫도그이다.\n체력을 35, 마나를 5 회복시켜준다.";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);
    }
    // 모든 소비아이템(일시적 버프포션) 데이터 로딩
    void Load_Item_Use_TemporaryBuffPotion()
    {
        Item_Use item;

        item = new Item_Use("맛없어 보이는 버섯", 9000, "Prefab/Item/Item_Use/Mushroom", E_ITEM_USE_TYPE.TEMPORARYBUFFPOTION, E_ITEM_GRADE.COMMON,
            30, 20, 400);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 5, 0, 0);
        item.m_sStatus_Limit_Min = new STATUS(5, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "맛없어 보이는 버섯이다. 어떻게 이게 음식일까?\n그러나 참고 먹는다면 버섯이 소화되는 30초 동안은 몸에 좋을것같다...\n버섯의 독성 때문에 다시 먹으려면 20초를 기다려야한다.";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);
        m_Dictionary_Collection_Use.Add(item.m_nItemCode, item);

        item = new Item_Use("악취 나는 치즈", 9001, "Prefab/Item/Item_Use/Cheese", E_ITEM_USE_TYPE.TEMPORARYBUFFPOTION, E_ITEM_GRADE.COMMON,
            60, 30, 300);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 5, 0, 0, 0, 1, 1, 1, 0, 0, -10, 0, 0, 0, 0.1f);
        item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "이 치즈의 악취는 매우 끔찍하다. 때문에 많은 용기가 필요한 치즈이다.\n이 치즈를 먹었을때 1분동안 이로운 효과를 얻을 수 있다. 그러나 악취 때문에 행동이 둔해진다.\n치즈의 악취 때문에 다시 먹으려면 30초는 기다려야 할것같다.";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);
        m_Dictionary_Collection_Use.Add(item.m_nItemCode, item);

        item = new Item_Use("용과", 9002, "Prefab/Item/Item_Use/DragonFruit", E_ITEM_USE_TYPE.TEMPORARYBUFFPOTION, E_ITEM_GRADE.RARE,
            30, 0, 1000);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(10, 0, 0, 0, 0, 0, 0, 5, 0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "용의 간식이라 불리는 용과다.\n용의 둥지에 드물게 자라는 용과나무에서 얻을 수 있다.\n섭취 시 30초 동안 최대 마나량과 관련 평판이 대폭 상승한다.";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);
        m_Dictionary_Collection_Use.Add(item.m_nItemCode, item);

        item = new Item_Use("황금빛 감자 오믈렛", 9003, "Prefab/Item/Item_Use/74_omlet_dish", E_ITEM_USE_TYPE.TEMPORARYBUFFPOTION, E_ITEM_GRADE.RELIC,
            180, 0, 700);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 30, 0, 20, 0, 3, 3, 3, 0, 0, 0, 3, 3, 0, 0.05f);
        item.m_sStatus_Limit_Min = new STATUS(12, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(10, 3, 3, 3, 3, 3, 3, 3, -3);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "'프리미엄 감자', '짭짤이 토마토', '오묘한 청란' 등으로 요리한 황금빛 감자 오믈렛이다.\n여러 고급 식재료를 숙련된 요리사인 '슬라임 할머니' 의 손맛을 더해 요리한 결과물이다.\n부드럽고 단짠단짠의 조화가 훌륭하며 감자 알갱이의 식감이 뛰어나다. 그래서인지 먹어도 먹어도 자꾸 들어간다.\n섭취 시 3분 동안 능력치와 평판이 대폭 상승한다. 그러나 몸이 약간 무거워질지도...";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

        item = new Item_Use("퍼석퍼석한 샌드위치", 9004, "Prefab/Item/Item_Use/93_sandwich_dish", E_ITEM_USE_TYPE.TEMPORARYBUFFPOTION, E_ITEM_GRADE.COMMON,
            60, 0, 150);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 10, 0);
        item.m_sStatus_Limit_Min = new STATUS(10, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0, 0, 0, 0, 0, 0, 0, 0, 0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "'오래된 빵', '퍽퍽한 감자', '짭짤이 토마토', '맛있는 소세지' 등으로 만든 퍼석퍼석 한 샌드위치이다.\n여러 그냥저냥 한 식재료를 숙련된 요리사인 '슬라임 할머니'의 손맛을 더해 만든 결과물이다.\n퍼석퍼석 한 식감조차 맛으로 느껴질 정도로 맛있다. 간편하게 들고 다니며 먹기 편하다.\n섭취 시 1분 동안 최대 체력을 10 상승시킨다.";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

        item = new Item_Use("찐득찐득 사과파이", 9005, "Prefab/Item/Item_Use/06_apple_pie_dish", E_ITEM_USE_TYPE.TEMPORARYBUFFPOTION, E_ITEM_GRADE.COMMON,
            20, 60, 200);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -50, 10, 10, 0, 0.1f);
        item.m_sStatus_Limit_Min = new STATUS(10, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0, 0, 0, -5, 0, 0, 0, 0, 0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "'초원 슬라임의 덩어리', '맛좋은 거대 사과' 등으로 만든 매우 찐득찐득한 사과파이다.\n'초원 슬라임의 덩어리' 의 찐득찐득함이 아주 잘 느껴진다. 맛은 없다.\n섭취 시 20초 동안 방어력이 크게 증가하지만 이동속도와 공격속도가 많이 느려진다.";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

        item = new Item_Use("균형잡힌 햄버거", 9006, "Prefab/Item/Item_Use/17_burger_napkin", E_ITEM_USE_TYPE.TEMPORARYBUFFPOTION, E_ITEM_GRADE.COMMON,
            120, 0, 250);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 3, 0, 3, 0, 1, 1, 1, 0, 0, 0, 3, 3, 0, 0.05f);
        item.m_sStatus_Limit_Min = new STATUS(12, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0, 0, 0, 0, 0, 0, 0, 0, 0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "여러 잡다한 식재료로 만든 영양 밸런스가 뛰어난 햄버거이다.\n맛은 그저 그렇지만 보편화된 음식이다.\n섭취 시 2분 동안 능력치가 상승하지만 공격속도는 느려진다.";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

        item = new Item_Use("오묘한 푸딩", 9007, "Prefab/Item/Item_Use/76_pudding_dish", E_ITEM_USE_TYPE.TEMPORARYBUFFPOTION, E_ITEM_GRADE.RARE,
            180, 0, 500);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 7, 0, -0.1f);
        item.m_sStatus_Limit_Min = new STATUS(10, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0, 0, 0, 0, 0, 0, 0, 0, 0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "'오묘한 청란' 등으로 만든 푸딩이다. 푸딩의 탱글탱글함 덕인지 섭취 시 3분 동안 방어력이 7 상승하고 공격속도가 빨라진다.";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

        item = new Item_Use("이상한 초콜릿", 9008, "Prefab/Item/Item_Use/27_chocolate_dish", E_ITEM_USE_TYPE.TEMPORARYBUFFPOTION, E_ITEM_GRADE.COMMON,
            30, 0, 100);
        item.m_sStatus_Effect = new STATUS(0);
        item.m_sStatus_Limit_Min = new STATUS(1, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(-10, 0, 0, 0, 0, 0, 0, 0, 10);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "이상한 초콜릿이다.\n무엇으로 만들어졌는지, 누가 만들었는지, 어디서 왔는지 아무것도 모른다.\n섭취 시 주변 꼬맹이들로부터 '엄마 쟤 똥 먹어!' 라는 말을 들을지도 모른다.\n섭취 시 30초 동안 평판이 크게 감소한다.";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);
    }
    // 모든 소비아이템(영구적 버프포션) 데이터 로딩
    void Load_Item_Use_EternalBuffPotion()
    {
        Item_Use item;

        item = new Item_Use("더럽게 맛없는 버섯", 10000, "Prefab/Item/Item_Use/Mushroom", E_ITEM_USE_TYPE.ETERNALBUFFPOTION, E_ITEM_GRADE.RARE,
            0, 0, 2000);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 5, 0, 5, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0);
        item.m_sStatus_Limit_Min = new STATUS(5, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(3);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "더럽게 맛없는 버섯이다. 진짜 보기만해도 엄청 맛없다는것을 알 수 있다...\n그러나 원래 몸에좋은것은 맛이 없다. 영양분이 듬뿍 담겨있어 먹는다면 영구적인 효과를 얻는다.";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);
        m_Dictionary_Collection_Use.Add(item.m_nItemCode, item);

        item = new Item_Use("소설: 나의 슬라임 오렌지 나무", 10001, "Prefab/Item/Item_Use/Book 2", E_ITEM_USE_TYPE.ETERNALBUFFPOTION, E_ITEM_GRADE.MYTH,
            0, 0, 8000);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 10, 0, 10, 0, 0, 0, 0, 0, 0, -5, 0, 0, 0, -0.1f);
        item.m_sStatus_Limit_Min = new STATUS(15, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(20, 5, 5, 20, 5, 20, 5, 5, 0);
        item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
        item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sItemDescription = "세계적으로 유명한 소설.\n왠지 모르게 앤트들과 슬라임들에게 인기가 좋다.\n대부분의 이야기는 구전되는 경우가 많아 이렇게 책으로 존재하는것은 매우 귀하다.\n직접 읽으려면 상당한 교양과 경험이 쌓여야 한다.";
        m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);
        m_Dictionary_QuestReward_Use.Add(item.m_nItemCode, item);
    }
    // 모든 소비아이템(강화서) 데이터 로딩
    void Load_Item_Use_Reinforcement()
    {
        Item_Use item;
        STATUS status; SOC soc;
        Reinforcement reinforcement1;
        // 소비아이템(강화서(데미지)). 공격의 강화서
        {
            item = new Item_Use("데미지 강화서[S1]", 11000, "Prefab/Item/Item_Use/Scroll", E_ITEM_USE_TYPE.REINFORCEMENT, E_ITEM_GRADE.COMMON,
                0, 0, 500);
            item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sSoc_Effect = new SOC(0);
            status = new STATUS(0, 0, 0, 0, 0, 0, 0, 1); soc = new SOC(1, 0, 0, 0, 0, 0, 0, 0, 0);
            reinforcement1 = new Reinforcement(9000, status, soc);
            item.m_Reinforcement_Effect = reinforcement1;
            item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sItemDescription = "90% 확률로 영구적으로 장비의 데미지을 1, 명예를 1 증가시킬 수 있는 강화서이다.\n장비의 강화 가능 횟수가 남아있을때 사용 가능하다.";
            m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

            item = new Item_Use("데미지 강화서[S2]", 11001, "Prefab/Item/Item_Use/Scroll", E_ITEM_USE_TYPE.REINFORCEMENT, E_ITEM_GRADE.COMMON,
                0, 0, 1000);
            item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sSoc_Effect = new SOC(0);
            status = new STATUS(0, 0, 0, 0, 0, 0, 0, 2); soc = new SOC(1, 0, 0, 0, 0, 0, 0, 0, 0);
            reinforcement1 = new Reinforcement(8000, status, soc);
            item.m_Reinforcement_Effect = reinforcement1;
            item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sItemDescription = "80% 확률로 영구적으로 장비의 데미지을 2, 명예를 1 증가시킬 수 있는 강화서이다.\n장비의 강화 가능 횟수가 남아있을때 사용 가능하다.";
            m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

            item = new Item_Use("데미지 강화서[S3]", 11002, "Prefab/Item/Item_Use/Scroll", E_ITEM_USE_TYPE.REINFORCEMENT, E_ITEM_GRADE.COMMON,
                0, 0, 1500);
            item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sSoc_Effect = new SOC(0);
            status = new STATUS(0, 0, 0, 0, 0, 0, 0, 3); soc = new SOC(1, 0, 0, 0, 0, 0, 0, 0, 0);
            reinforcement1 = new Reinforcement(7000, status, soc);
            item.m_Reinforcement_Effect = reinforcement1;
            item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sItemDescription = "70% 확률로 영구적으로 장비의 데미지을 3, 명예를 1 증가시킬 수 있는 강화서이다.\n장비의 강화 가능 횟수가 남아있을때 사용 가능하다.";
            m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

            item = new Item_Use("데미지 강화서[S4]", 11003, "Prefab/Item/Item_Use/Scroll", E_ITEM_USE_TYPE.REINFORCEMENT, E_ITEM_GRADE.COMMON,
                0, 0, 2000);
            item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sSoc_Effect = new SOC(0);
            status = new STATUS(0, 0, 0, 0, 0, 0, 0, 4); soc = new SOC(1, 0, 0, 0, 0, 0, 0, 0, 0);
            reinforcement1 = new Reinforcement(6000, status, soc);
            item.m_Reinforcement_Effect = reinforcement1;
            item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sItemDescription = "60% 확률로 영구적으로 장비의 데미지을 4, 명예를 1 증가시킬 수 있는 강화서이다.\n장비의 강화 가능 횟수가 남아있을때 사용 가능하다.";
            m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

            item = new Item_Use("데미지 강화서[S5]", 11004, "Prefab/Item/Item_Use/Scroll", E_ITEM_USE_TYPE.REINFORCEMENT, E_ITEM_GRADE.RARE,
                0, 0, 5000);
            item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            item.m_sStatus_Limit_Min = new STATUS(12, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sSoc_Effect = new SOC(0);
            status = new STATUS(0, 0, 0, 0, 0, 0, 0, 10); soc = new SOC(3, 0, 0, 0, 0, 0, 0, 0, 0);
            reinforcement1 = new Reinforcement(5000, status, soc);
            item.m_Reinforcement_Effect = reinforcement1;
            item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sItemDescription = "50% 확률로 영구적으로 장비의 데미지을 10, 명예를 3 증가시킬 수 있는 강화서이다.\n장비의 강화 가능 횟수가 남아있을때 사용 가능하다.";
            m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

            item = new Item_Use("데미지 강화서[S6]", 11005, "Prefab/Item/Item_Use/Scroll", E_ITEM_USE_TYPE.REINFORCEMENT, E_ITEM_GRADE.RARE,
                0, 0, 6000);
            item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            item.m_sStatus_Limit_Min = new STATUS(12, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sSoc_Effect = new SOC(0);
            status = new STATUS(0, 0, 0, 0, 0, 0, 0, 12); soc = new SOC(4, 0, 0, 0, 0, 0, 0, 0, 0);
            reinforcement1 = new Reinforcement(4000, status, soc);
            item.m_Reinforcement_Effect = reinforcement1;
            item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sItemDescription = "40% 확률로 영구적으로 장비의 데미지을 12, 명예를 4 증가시킬 수 있는 강화서이다.\n장비의 강화 가능 횟수가 남아있을때 사용 가능하다.";
            m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

            item = new Item_Use("데미지 강화서[S7]", 11006, "Prefab/Item/Item_Use/Scroll", E_ITEM_USE_TYPE.REINFORCEMENT, E_ITEM_GRADE.RARE,
                0, 0, 10500);
            item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            item.m_sStatus_Limit_Min = new STATUS(15, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sSoc_Effect = new SOC(0);
            status = new STATUS(0, 0, 0, 0, 0, 0, 0, 21); soc = new SOC(7, 0, 0, 0, 0, 0, 0, 0, 0);
            reinforcement1 = new Reinforcement(3000, status, soc);
            item.m_Reinforcement_Effect = reinforcement1;
            item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sItemDescription = "30% 확률로 영구적으로 장비의 데미지을 21, 명예를 7 증가시킬 수 있는 강화서이다.\n장비의 강화 가능 횟수가 남아있을때 사용 가능하다.";
            m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

            item = new Item_Use("데미지 강화서[S8]", 11007, "Prefab/Item/Item_Use/Scroll", E_ITEM_USE_TYPE.REINFORCEMENT, E_ITEM_GRADE.RARE,
                0, 0, 12000);
            item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            item.m_sStatus_Limit_Min = new STATUS(15, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sSoc_Effect = new SOC(0);
            status = new STATUS(0, 0, 0, 0, 0, 0, 0, 24); soc = new SOC(8, 0, 0, 0, 0, 0, 0, 0, 0);
            reinforcement1 = new Reinforcement(2000, status, soc);
            item.m_Reinforcement_Effect = reinforcement1;
            item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sItemDescription = "20% 확률로 영구적으로 장비의 데미지을 24, 명예를 8 증가시킬 수 있는 강화서이다.\n장비의 강화 가능 횟수가 남아있을때 사용 가능하다.";
            m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

            item = new Item_Use("데미지 강화서[S9]", 11008, "Prefab/Item/Item_Use/Scroll", E_ITEM_USE_TYPE.REINFORCEMENT, E_ITEM_GRADE.RARE,
                0, 0, 18000);
            item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            item.m_sStatus_Limit_Min = new STATUS(20, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sSoc_Effect = new SOC(0);
            status = new STATUS(0, 0, 0, 0, 0, 0, 0, 36); soc = new SOC(12, 0, 0, 0, 0, 0, 0, 0, 0);
            reinforcement1 = new Reinforcement(1000, status, soc);
            item.m_Reinforcement_Effect = reinforcement1;
            item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sItemDescription = "10% 확률로 영구적으로 장비의 데미지을 36, 명예를 12 증가시킬 수 있는 강화서이다.\n장비의 강화 가능 횟수가 남아있을때 사용 가능하다.";
            m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

            item = new Item_Use("데미지 강화서[S10]", 11009, "Prefab/Item/Item_Use/Scroll", E_ITEM_USE_TYPE.REINFORCEMENT, E_ITEM_GRADE.RELIC,
                0, 0, 25000);
            item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            item.m_sStatus_Limit_Min = new STATUS(25, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sSoc_Effect = new SOC(0);
            status = new STATUS(0, 0, 0, 0, 0, 0, 0, 50); soc = new SOC(16, 0, 0, 0, 0, 0, 0, 0, 0);
            reinforcement1 = new Reinforcement(100, status, soc);
            item.m_Reinforcement_Effect = reinforcement1;
            item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sItemDescription = "1% 확률로 영구적으로 장비의 데미지을 50, 명예를 16 증가시킬 수 있는 강화서이다.\n장비의 강화 가능 횟수가 남아있을때 사용 가능하다.";
            m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);
        }
        // 소비아이템(강화서(방어력)). 방어의 강화서
        {
            item = new Item_Use("방어력 강화서[S1]", 11010, "Prefab/Item/Item_Use/Scroll", E_ITEM_USE_TYPE.REINFORCEMENT, E_ITEM_GRADE.COMMON,
                0, 0, 500);
            item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sSoc_Effect = new SOC(0);
            status = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0); soc = new SOC(1, 0, 0, 0, 0, 0, 0, 0, 0);
            reinforcement1 = new Reinforcement(9000, status, soc);
            item.m_Reinforcement_Effect = reinforcement1;
            item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sItemDescription = "90% 확률로 영구적으로 장비의 방어력을 1, 명예를 1 증가시킬 수 있는 강화서이다.\n장비의 강화 가능 횟수가 남아있을때 사용 가능하다.";
            m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

            item = new Item_Use("방어력 강화서[S2]", 11011, "Prefab/Item/Item_Use/Scroll", E_ITEM_USE_TYPE.REINFORCEMENT, E_ITEM_GRADE.COMMON,
                0, 0, 1000);
            item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sSoc_Effect = new SOC(0);
            status = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0); soc = new SOC(1, 0, 0, 0, 0, 0, 0, 0, 0);
            reinforcement1 = new Reinforcement(8000, status, soc);
            item.m_Reinforcement_Effect = reinforcement1;
            item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sItemDescription = "80% 확률로 영구적으로 장비의 방어력을 2, 명예를 1 증가시킬 수 있는 강화서이다.\n장비의 강화 가능 횟수가 남아있을때 사용 가능하다.";
            m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

            item = new Item_Use("방어력 강화서[S3]", 11012, "Prefab/Item/Item_Use/Scroll", E_ITEM_USE_TYPE.REINFORCEMENT, E_ITEM_GRADE.COMMON,
                0, 0, 1500);
            item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sSoc_Effect = new SOC(0);
            status = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3, 0, 0); soc = new SOC(1, 0, 0, 0, 0, 0, 0, 0, 0);
            reinforcement1 = new Reinforcement(7000, status, soc);
            item.m_Reinforcement_Effect = reinforcement1;
            item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sItemDescription = "70% 확률로 영구적으로 장비의 방어력을 3, 명예를 1 증가시킬 수 있는 강화서이다.\n장비의 강화 가능 횟수가 남아있을때 사용 가능하다.";
            m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

            item = new Item_Use("방어력 강화서[S4]", 11013, "Prefab/Item/Item_Use/Scroll", E_ITEM_USE_TYPE.REINFORCEMENT, E_ITEM_GRADE.COMMON,
                0, 0, 2000);
            item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            item.m_sStatus_Limit_Min = new STATUS(7, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sSoc_Effect = new SOC(0);
            status = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 0, 0); soc = new SOC(1, 0, 0, 0, 0, 0, 0, 0, 0);
            reinforcement1 = new Reinforcement(6000, status, soc);
            item.m_Reinforcement_Effect = reinforcement1;
            item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sItemDescription = "60% 확률로 영구적으로 장비의 방어력을 4, 명예를 1 증가시킬 수 있는 강화서이다.\n장비의 강화 가능 횟수가 남아있을때 사용 가능하다.";
            m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

            item = new Item_Use("방어력 강화서[S5]", 11014, "Prefab/Item/Item_Use/Scroll", E_ITEM_USE_TYPE.REINFORCEMENT, E_ITEM_GRADE.RARE,
                0, 0, 5000);
            item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            item.m_sStatus_Limit_Min = new STATUS(12, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sSoc_Effect = new SOC(0);
            status = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 10, 0, 0); soc = new SOC(3, 0, 0, 0, 0, 0, 0, 0, 0);
            reinforcement1 = new Reinforcement(5000, status, soc);
            item.m_Reinforcement_Effect = reinforcement1;
            item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sItemDescription = "50% 확률로 영구적으로 장비의 방어력을 10, 명예를 3 증가시킬 수 있는 강화서이다.\n장비의 강화 가능 횟수가 남아있을때 사용 가능하다.";
            m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

            item = new Item_Use("방어력 강화서[S6]", 11015, "Prefab/Item/Item_Use/Scroll", E_ITEM_USE_TYPE.REINFORCEMENT, E_ITEM_GRADE.RARE,
                0, 0, 6000);
            item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            item.m_sStatus_Limit_Min = new STATUS(12, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sSoc_Effect = new SOC(0);
            status = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 12, 12, 0, 0); soc = new SOC(4, 0, 0, 0, 0, 0, 0, 0, 0);
            reinforcement1 = new Reinforcement(4000, status, soc);
            item.m_Reinforcement_Effect = reinforcement1;
            item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sItemDescription = "40% 확률로 영구적으로 장비의 방어력을 12, 명예를 4 증가시킬 수 있는 강화서이다.\n장비의 강화 가능 횟수가 남아있을때 사용 가능하다.";
            m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

            item = new Item_Use("방어력 강화서[S7]", 11016, "Prefab/Item/Item_Use/Scroll", E_ITEM_USE_TYPE.REINFORCEMENT, E_ITEM_GRADE.RARE,
                0, 0, 10500);
            item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            item.m_sStatus_Limit_Min = new STATUS(15, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sSoc_Effect = new SOC(0);
            status = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 21, 21, 0, 0); soc = new SOC(7, 0, 0, 0, 0, 0, 0, 0, 0);
            reinforcement1 = new Reinforcement(3000, status, soc);
            item.m_Reinforcement_Effect = reinforcement1;
            item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sItemDescription = "30% 확률로 영구적으로 장비의 방어력을 21, 명예를 7 증가시킬 수 있는 강화서이다.\n장비의 강화 가능 횟수가 남아있을때 사용 가능하다.";
            m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

            item = new Item_Use("방어력 강화서[S8]", 11017, "Prefab/Item/Item_Use/Scroll", E_ITEM_USE_TYPE.REINFORCEMENT, E_ITEM_GRADE.RARE,
                0, 0, 12000);
            item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            item.m_sStatus_Limit_Min = new STATUS(15, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sSoc_Effect = new SOC(0);
            status = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 24, 24, 0, 0); soc = new SOC(8, 0, 0, 0, 0, 0, 0, 0, 0);
            reinforcement1 = new Reinforcement(2000, status, soc);
            item.m_Reinforcement_Effect = reinforcement1;
            item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sItemDescription = "20% 확률로 영구적으로 장비의 방어력을 24, 명예를 8 증가시킬 수 있는 강화서이다.\n장비의 강화 가능 횟수가 남아있을때 사용 가능하다.";
            m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

            item = new Item_Use("방어력 강화서[S9]", 11018, "Prefab/Item/Item_Use/Scroll", E_ITEM_USE_TYPE.REINFORCEMENT, E_ITEM_GRADE.RARE,
                0, 0, 18000);
            item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            item.m_sStatus_Limit_Min = new STATUS(20, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sSoc_Effect = new SOC(0);
            status = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 36, 36, 0, 0); soc = new SOC(12, 0, 0, 0, 0, 0, 0, 0, 0);
            reinforcement1 = new Reinforcement(1000, status, soc);
            item.m_Reinforcement_Effect = reinforcement1;
            item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sItemDescription = "10% 확률로 영구적으로 장비의 방어력을 36, 명예를 12 증가시킬 수 있는 강화서이다.\n장비의 강화 가능 횟수가 남아있을때 사용 가능하다.";
            m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);

            item = new Item_Use("방어력 강화서[S10]", 11019, "Prefab/Item/Item_Use/Scroll", E_ITEM_USE_TYPE.REINFORCEMENT, E_ITEM_GRADE.RELIC,
                0, 0, 25000);
            item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            item.m_sStatus_Limit_Min = new STATUS(25, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sSoc_Effect = new SOC(0);
            status = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 50, 50, 0, 0); soc = new SOC(16, 0, 0, 0, 0, 0, 0, 0, 0);
            reinforcement1 = new Reinforcement(100, status, soc);
            item.m_Reinforcement_Effect = reinforcement1;
            item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sItemDescription = "1% 확률로 영구적으로 장비의 방어력을 50, 명예를 16 증가시킬 수 있는 강화서이다.\n장비의 강화 가능 횟수가 남아있을때 사용 가능하다.";
            m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);
        }
        // 소비아이템(강화서(공격속도, 이동속도)). 민첩의 강화서
        {
            item = new Item_Use("민첩의 강화서[S1]", 11020, "Prefab/Item/Item_Use/Scroll", E_ITEM_USE_TYPE.REINFORCEMENT, E_ITEM_GRADE.RARE,
                0, 0, 5000);
            item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            item.m_sStatus_Limit_Min = new STATUS(12, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sSoc_Effect = new SOC(0);
            status = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, -0.05f); soc = new SOC(3, 0, 0, 0, 0, 0, 0, 0, 0);
            reinforcement1 = new Reinforcement(5000, status, soc);
            item.m_Reinforcement_Effect = reinforcement1;
            item.m_sSoc_Limit_Min = new SOC(-10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000, -10000);
            item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
            item.m_sItemDescription = "50% 확률로 영구적으로 장비의 이동속도를 5 증가시키고, 공격속도를 0.05 감소시킬 수 있는 강화서이다.\n장비의 강화 가능 횟수가 남아있을때 사용 가능하다.";
            m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);
        }
    }
    // 모든 기타아이템 데이터 로딩
    void Load_Item_Etc()
    {
        Item_Etc item;

        item = new Item_Etc("초원 슬라임의 찌꺼기", 0001, "Prefab/Item/Item_Etc/Item_Slime_2", E_ITEM_GRADE.NORMAL, E_ITEM_ETC_TYPE.JUNK, 12);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        item.m_sStatus_Limit_Min = new STATUS(-10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC();
        item.m_sSoc_Limit_Max = new SOC();
        item.m_sItemDescription = "'초원 슬라임' 에게서 얻은 찌꺼기이다.\n딱히 쓸모는 없어 보인다.";
        m_Dictionary_MonsterDrop_Etc.Add(item.m_nItemCode, item);

        item = new Item_Etc("초원 슬라임의 덩어리", 0002, "Prefab/Item/Item_Etc/Item_Slime_1", E_ITEM_GRADE.RARE, E_ITEM_ETC_TYPE.MULTIPLE, 150);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        item.m_sStatus_Limit_Min = new STATUS(-10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC();
        item.m_sSoc_Limit_Max = new SOC();
        item.m_sItemDescription = "'초원 슬라임의 찌꺼기' 가 오랜 시간동안 농축되어 만들어졌다.\n재생과 해독에 탁월하여 각종 포션의 원료로 사용된다. 그래서 꽤나 값이 나가는 편이다";
        m_Dictionary_MonsterDrop_Etc.Add(item.m_nItemCode, item);

        item = new Item_Etc("짙은 앤트의 나무가죽", 0003, "Prefab/Item/Item_Etc/Item_Ent1", E_ITEM_GRADE.RARE, E_ITEM_ETC_TYPE.MULTIPLE, 140);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        item.m_sStatus_Limit_Min = new STATUS(-10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC();
        item.m_sSoc_Limit_Max = new SOC();
        item.m_sItemDescription = "'짙은 앤트' 에게서 얻은 나무가죽이다.\n질기지만 가벼워 '드넓은 초원' 에서 널리 사용되고있다.\n'짙은 앤트의 나무가죽' 으로 만든 방어구는 성능이 뛰어나다고 한다.";
        m_Dictionary_MonsterDrop_Etc.Add(item.m_nItemCode, item);

        item = new Item_Etc("주식회사 더 슬라 사원증", 0005, "Prefab/Item/Item_Etc/Leather", E_ITEM_GRADE.NORMAL, E_ITEM_ETC_TYPE.JUNK, 20);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        item.m_sStatus_Limit_Min = new STATUS(-10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC();
        item.m_sSoc_Limit_Max = new SOC();
        item.m_sItemDescription = "'주식회사 더 슬라' 의 일원임을 알려주는 사원증이다.";
        m_Dictionary_MonsterDrop_Etc.Add(item.m_nItemCode, item);

        item = new Item_Etc("알보찰", 0006, "Prefab/Item/Item_Etc/Green Potion", E_ITEM_GRADE.RARE, E_ITEM_ETC_TYPE.QUEST, 1500);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        item.m_sStatus_Limit_Min = new STATUS(-10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC();
        item.m_sSoc_Limit_Max = new SOC();
        item.m_sItemDescription = "'드넓은 초원' 에서 만들어지는 세계 최고 수준의 재생포션이다.\n현재 '주식회사 더 슬라' 가 독점 판매 및 유통 하고 있다.\n좋은 약임에 분명하지만 '알보찰' 을 만들기 위해 핍박받는 '초원 슬라임' 의 고충은 아무도 알아주지 않는다.";
        m_Dictionary_MonsterDrop_Etc.Add(item.m_nItemCode, item);

        item = new Item_Etc("초원 슬라임의 정수", 0007, "Prefab/Item/Item_Etc/Item_Etc_GrassSlimeCore", E_ITEM_GRADE.RELIC, E_ITEM_ETC_TYPE.MULTIPLE, 4000);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        item.m_sStatus_Limit_Min = new STATUS(-10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC();
        item.m_sSoc_Limit_Max = new SOC();
        item.m_sItemDescription = "'초원 슬라임' 중 강력한 개체들을 통해 아주 드물게 얻을 수 있다.\n오랜시간의 인고와 노력을 통해 생성된다.\n'초원 슬라임' 들에게 '초원 슬라임의 정수'란 그 자체로 종족의 자존심이자 존경의 대상이다.\n" +
            "재생과 해독에 탁월해 각종 포션의 원료로 사용된다.\n그 밖에도 사용처가 많다.";
        m_Dictionary_MonsterDrop_Etc.Add(item.m_nItemCode, item);

        item = new Item_Etc("찢어진 헝겊 조각", 0008, "Prefab/Item/Item_Etc/Item_Etc_TornPieceOfCloth", E_ITEM_GRADE.NORMAL, E_ITEM_ETC_TYPE.JUNK, 10);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        item.m_sStatus_Limit_Min = new STATUS(-10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC();
        item.m_sSoc_Limit_Max = new SOC();
        item.m_sItemDescription = "다시 사용 할 수 없을 정도로 찢어진 헝겊의 조각이다.";
        m_Dictionary_MonsterDrop_Etc.Add(item.m_nItemCode, item);

        item = new Item_Etc("부식된 철 조각", 0009, "Prefab/Item/Item_Etc/Silver Nugget", E_ITEM_GRADE.NORMAL, E_ITEM_ETC_TYPE.JUNK, 12);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        item.m_sStatus_Limit_Min = new STATUS(-10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC();
        item.m_sSoc_Limit_Max = new SOC();
        item.m_sItemDescription = "다시 사용 할 수 없을 정도로 부식된 철의 조각이다.";
        m_Dictionary_MonsterDrop_Etc.Add(item.m_nItemCode, item);

        item = new Item_Etc("감", 0010, "Prefab/Item/Item_Etc/Item_Use_RedFruitOfWood", E_ITEM_GRADE.NORMAL, E_ITEM_ETC_TYPE.QUEST, 30);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        item.m_sStatus_Limit_Min = new STATUS(-10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC();
        item.m_sSoc_Limit_Max = new SOC();
        item.m_sItemDescription = "어디서나 흔히 볼 수 있는 맛있는 감이다.";
        m_Dictionary_MonsterDrop_Etc.Add(item.m_nItemCode, item);

        item = new Item_Etc("부식된 뼈", 0011, "Prefab/Item/Item_Etc/Item_Etc_Bone1", E_ITEM_GRADE.NORMAL, E_ITEM_ETC_TYPE.JUNK, 10);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        item.m_sStatus_Limit_Min = new STATUS(-10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC();
        item.m_sSoc_Limit_Max = new SOC();
        item.m_sItemDescription = "누구의것인지 알 수 없을정도로 부식된 뼈이다.";
        m_Dictionary_MonsterDrop_Etc.Add(item.m_nItemCode, item);

        item = new Item_Etc("부식된 부러진 뼈", 0012, "Prefab/Item/Item_Etc/Item_Etc_BrokenBone1", E_ITEM_GRADE.NORMAL, E_ITEM_ETC_TYPE.JUNK, 4);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        item.m_sStatus_Limit_Min = new STATUS(-10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC();
        item.m_sSoc_Limit_Max = new SOC();
        item.m_sItemDescription = "누구의것인지 알 수 없을정도로 부식되고 부러진 뼈이다.";
        m_Dictionary_MonsterDrop_Etc.Add(item.m_nItemCode, item);

        item = new Item_Etc("단단한 뼈", 0013, "Prefab/Item/Item_Etc/Bone", E_ITEM_GRADE.COMMON, E_ITEM_ETC_TYPE.MULTIPLE, 14);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        item.m_sStatus_Limit_Min = new STATUS(-10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC();
        item.m_sSoc_Limit_Max = new SOC();
        item.m_sItemDescription = "누군가의 단단한 뼈이다.\n그 어떤것에도 손상되지 않았다.\n누군가의 애완동물에게 가져다주면 좋아할 것 같다.";
        m_Dictionary_MonsterDrop_Etc.Add(item.m_nItemCode, item);
        m_Dictionary_Collection_Etc.Add(item.m_nItemCode, item);

        item = new Item_Etc("나뭇가지", 0014, "Prefab/Item/Item_Etc/Item_Etc_Brunch", E_ITEM_GRADE.NORMAL, E_ITEM_ETC_TYPE.JUNK, 12);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        item.m_sStatus_Limit_Min = new STATUS(-10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC();
        item.m_sSoc_Limit_Max = new SOC();
        item.m_sItemDescription = "어디서나 흔히 볼 수 있는 나뭇가지이다.";
        m_Dictionary_MonsterDrop_Etc.Add(item.m_nItemCode, item);

        item = new Item_Etc("호박 원석", 0015, "Prefab/Item/Item_Etc/Item_Etc_Amber", E_ITEM_GRADE.RARE, E_ITEM_ETC_TYPE.MULTIPLE, 500);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        item.m_sStatus_Limit_Min = new STATUS(-10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC();
        item.m_sSoc_Limit_Max = new SOC();
        item.m_sItemDescription = "'드넓은 초원' 의 거대한 나무들의 좁은 틈에서 만들어진 호박의 원석이다.\n원석 자체의 신비로움 때문인지 고가로 거래된다.";
        m_Dictionary_MonsterDrop_Etc.Add(item.m_nItemCode, item);

        item = new Item_Etc("100년 묵은 도라지", 0016, "Prefab/Item/Item_Etc/Item_Etc_DorageLiving100", E_ITEM_GRADE.RARE, E_ITEM_ETC_TYPE.MULTIPLE, 300);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        item.m_sStatus_Limit_Min = new STATUS(-10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC();
        item.m_sSoc_Limit_Max = new SOC();
        item.m_sItemDescription = "'늙고 병든 슬라임' 이 100년전 심어두고 까먹은 도라지이다.\n관절에 좋다고 한다. 그래서 늙은이들에게 인기가 좋다.";
        m_Dictionary_MonsterDrop_Etc.Add(item.m_nItemCode, item);
        m_Dictionary_Collection_Etc.Add(item.m_nItemCode, item);

        item = new Item_Etc("잔 나뭇가지", 0017, "Prefab/Item/Item_Etc/Item_Etc_Brunch", E_ITEM_GRADE.NORMAL, E_ITEM_ETC_TYPE.JUNK, 7);
        item.m_sStatus_Effect = new STATUS(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        item.m_sStatus_Limit_Min = new STATUS(-10000);
        item.m_sStatus_Limit_Max = new STATUS(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000);
        item.m_sSoc_Effect = new SOC(0);
        item.m_sSoc_Limit_Min = new SOC();
        item.m_sSoc_Limit_Max = new SOC();
        item.m_sItemDescription = "어디서나 흔히 볼 수 있는 잔 나뭇가지이다.";
        m_Dictionary_MonsterDrop_Etc.Add(item.m_nItemCode, item);
        m_Dictionary_Collection_Etc.Add(item.m_nItemCode, item);
    }
    // 모든 소비아이템(기프트) 데이터 로딩
    void Load_Item_Use_Gift()
    {
        Load_Item_Use_Gift_FixedBox();                   // 모든 소비아이템(기프트(아이템 확정 지급형)) 데이터 로딩
        Load_Item_Use_Gift_RandomBox_IndependentTrial(); // 모든 소비아이템(기프트(랜덤 지급형 _ A 타입)) 데이터 로딩
        Load_Item_Use_Gift_RandomBox_dependentTrial();   // 모든 소비아이템(기프트(랜덤 지급형 _ B 타입)) 데이터 로딩
    }
    // 모든 소비아이템(기프트(아이템 확정 지급형)) 데이터 로딩
    void Load_Item_Use_Gift_FixedBox()
    {
        Item_Use item;
        // 초보 모험가를 위한 선물 상자
        {
            item = new Item_Use("초보 모험가를 위한 선물 상자", 12000, "Prefab/Item/Item_Use/Gift/Box_Wood_1", E_ITEM_USE_TYPE.GIFT, E_ITEM_GRADE.NORMAL,
                0, 0, 1000);
            item.Set_Item_Use_Gift(E_ITEM_USE_GIFT_TYPE.FIXEDBOX);
            item.m_sStatus_Limit_Max.SetSTATUS_LV(10);
            item.m_sItemDescription = "초보 모험가의 편의를 위해 제공되는 선물상자이다.\n무기와 회복포션이 들어있다.";
            item.Add_Gift_Item_Equip(1000, 1);
            item.Add_Gift_Item_Equip(1300, 1);
            item.Add_Gift_Item_Equip(1600, 1);
            item.Add_Gift_Item_Equip(3001, 1);
            item.Add_Gift_Item_Equip(4001, 1);
            item.Add_Gift_Item_Equip(6001, 1);
            item.Add_Gift_Item_Use(8000, 20);
            item.Add_Gift_Item_Use(8001, 20);
            m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);
        }
        // 용병단의 비보 Lv1
        {
            item = new Item_Use("용병단의 비보 Lv1", 12001, "Prefab/Item/Item_Use/Gift/Box_Gold_Silver_1", E_ITEM_USE_TYPE.GIFT, E_ITEM_GRADE.RARE,
                0, 0, 13000);
            item.Set_Item_Use_Gift(E_ITEM_USE_GIFT_TYPE.FIXEDBOX);
            item.m_sStatus_Limit_Min.SetSTATUS_LV(7);
            item.m_sItemDescription = "역사상 최강의 용병 조직인 용병단의 비보이다.\n오직 신속하고 정확한 임무수행만을 위해 조직에 헌신하는 초급 용병에게 주어진다.\n앞으로 수행할 고난도의 임무 수행에 대비해 여러 아이템이 들어있다. 용병단의 재력은 상당한 수준이고 이 세계의 금기따위에 얽매이지 않기 때문에 획득하기 힘든 아이템을 여럿 획득할 수 있다.";
            item.Add_Gift_Item_Equip(3004, 1);
            item.Add_Gift_Item_Equip(4009, 1);
            item.Add_Gift_Item_Equip(5002, 1);
            item.Add_Gift_Item_Equip(6004, 1);
            item.Add_Gift_Item_Use(8003, 20);
            item.Add_Gift_Item_Use(8004, 20);
            item.Add_Gift_Item_Use(11001, 5);
            item.Add_Gift_Item_Use(11011, 3);
            m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);
        }
        // 기사단의 비보 Lv1
        {
            item = new Item_Use("기사단의 비보 Lv1", 12002, "Prefab/Item/Item_Use/Gift/Box_Gold_Ruby_1", E_ITEM_USE_TYPE.GIFT, E_ITEM_GRADE.RARE,
                0, 0, 13000);
            item.Set_Item_Use_Gift(E_ITEM_USE_GIFT_TYPE.FIXEDBOX);
            item.m_sSoc_Limit_Min = new SOC(30, 0, 0, 0, 0, 0, 0, 0, -10000);
            item.m_sSoc_Limit_Max = new SOC(10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10);
            item.m_sStatus_Limit_Min.SetSTATUS_LV(7);
            item.m_sItemDescription = "오랜 전통과 역사를 자랑하는 기사단의 비보이다.\n끝없는 역경과 고난을 헤쳐내고 인내하며 남을 위해 헌신하는 정의로운 초급 기사에게 주어진다.\n앞으로 수행할 고난도의 임무 수행에 대비해 여러 아이템이 들어있다.";
            item.Add_Gift_Item_Equip(3007, 1);
            item.Add_Gift_Item_Equip(4013, 1);
            item.Add_Gift_Item_Equip(5005, 1);
            item.Add_Gift_Item_Equip(6007, 1);
            item.Add_Gift_Item_Use(8003, 10);
            item.Add_Gift_Item_Use(8004, 10);
            m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);
        }
    }
    // 모든 소비아이템(기프트(랜덤 지급형 _ A 타입)) 데이터 로딩
    // 소비아이템(기프트(랜덤 지급형 _ A 타입)) 목록의 아이템 획득 확률의 합은 10000(100%)
    void Load_Item_Use_Gift_RandomBox_IndependentTrial()
    {
        Item_Use item;
        // 대장장이 '블랙' 의 무기 상자
        {
            item = new Item_Use("대장장이 '블랙' 의 무기 상자", 12300, "Prefab/Item/Item_Use/Gift/Box_Iron_1", E_ITEM_USE_TYPE.GIFT, E_ITEM_GRADE.COMMON,
                0, 0, 2500);
            item.Set_Item_Use_Gift(E_ITEM_USE_GIFT_TYPE.RANDOMBOX_INDEPENDENTTRIAL, 1, 1);
            item.m_sItemDescription = "'드넓은 초원' 의 대장장이 '블랙' 이 만든 무기가 들어있다.\n어떤 무기가 들어있는지는 알 수 없으나 대장장이 '블랙' 이 직접 만든 무기가 단 한개만 들어있다.\n운이 좋다면 좋은 무기를 얻을 수 있다.";
            item.m_sStatus_Limit_Min.SetSTATUS_LV(5);
            item.Add_Gift_Item_Equip(1001, 1, 1000);
            item.Add_Gift_Item_Equip(1003, 1, 1000);
            item.Add_Gift_Item_Equip(1004, 1, 1000);
            item.Add_Gift_Item_Equip(1301, 1, 1000);
            item.Add_Gift_Item_Equip(1303, 1, 1000);
            item.Add_Gift_Item_Equip(1304, 1, 1000);
            item.Add_Gift_Item_Equip(1601, 1, 1000);
            item.Add_Gift_Item_Equip(1602, 1, 1000);
            item.Add_Gift_Item_Equip(1603, 1, 1000);
            item.Add_Gift_Item_Equip(2007, 1, 1000);
            m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);
        }
        // 대장장이 '블랙 스미슬라임' 의 방어구 상자
        {
            item = new Item_Use("대장장이 '블랙스미슬라임' 의 방어구 상자", 12301, "Prefab/Item/Item_Use/Gift/Box_Iron_1", E_ITEM_USE_TYPE.GIFT, E_ITEM_GRADE.COMMON,
                0, 0, 2500);
            item.Set_Item_Use_Gift(E_ITEM_USE_GIFT_TYPE.RANDOMBOX_INDEPENDENTTRIAL, 1, 1);
            item.m_sItemDescription = "'드넓은 초원' 의 대장장이 '블랙스미슬라임' 이 만든 방어구가 들어있다.\n어떤 방어구가 들어있는지는 알 수 없으나 대장장이 '블랙스미슬라임' 이 직접 만든 방어구가 단 한개만 들어있다.\n운이 좋다면 좋은 방어구를 얻을 수 있다.";
            item.m_sStatus_Limit_Min.SetSTATUS_LV(5);
            item.Add_Gift_Item_Equip(3003, 1, 1250);
            item.Add_Gift_Item_Equip(3010, 1, 1250);
            item.Add_Gift_Item_Equip(4003, 1, 1250);
            item.Add_Gift_Item_Equip(4005, 1, 1250);
            item.Add_Gift_Item_Equip(4006, 1, 1250);
            item.Add_Gift_Item_Equip(4008, 1, 1250);
            item.Add_Gift_Item_Equip(5001, 1, 1250);
            item.Add_Gift_Item_Equip(6003, 1, 1250);
            m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);
        }
        // 은빛 보물 상자 Lv1
        {
            item = new Item_Use("은빛 보물 상자 Lv1", 12302, "Prefab/Item/Item_Use/Gift/Box_SilverPlating_1", E_ITEM_USE_TYPE.GIFT, E_ITEM_GRADE.RARE,
                0, 0, 80000);
            item.Set_Item_Use_Gift(E_ITEM_USE_GIFT_TYPE.RANDOMBOX_INDEPENDENTTRIAL, 1, 1);
            item.m_sItemDescription = "귀하디 귀한 보물 상자이다.\n등급은 낮지만 그래도 꼴에 보물 상자라고 레어보다 낮은 등급의 아이템은 상자에 들어있지 않다.";
            item.Add_Gift_Item_Equip(1005, 1, 250);
            item.Add_Gift_Item_Equip(1006, 1, 250);
            item.Add_Gift_Item_Equip(1009, 1, 250);
            item.Add_Gift_Item_Equip(1017, 1, 250);
            item.Add_Gift_Item_Equip(1018, 1, 250);
            item.Add_Gift_Item_Equip(1607, 1, 250);
            item.Add_Gift_Item_Equip(1608, 1, 250);
            item.Add_Gift_Item_Equip(1609, 1, 250);
            item.Add_Gift_Item_Equip(2006, 1, 250);
            item.Add_Gift_Item_Equip(2007, 1, 250);
            item.Add_Gift_Item_Equip(4004, 1, 250);
            item.Add_Gift_Item_Equip(4016, 1, 250);
            item.Add_Gift_Item_Equip(4017, 1, 250);
            item.Add_Gift_Item_Equip(4018, 1, 250);
            item.Add_Gift_Item_Equip(5008, 1, 250);
            item.Add_Gift_Item_Equip(1016, 1, 60);
            item.Add_Gift_Item_Equip(1610, 1, 60);
            item.Add_Gift_Item_Equip(1615, 1, 60);
            item.Add_Gift_Item_Equip(2003, 1, 60);

            item.Add_Gift_Item_Use(8011, 1, 250);
            item.Add_Gift_Item_Use(8017, 1, 250);
            item.Add_Gift_Item_Use(8023, 1, 250);
            item.Add_Gift_Item_Use(8025, 1, 250);
            item.Add_Gift_Item_Use(9002, 1, 250);
            item.Add_Gift_Item_Use(9007, 1, 250);
            item.Add_Gift_Item_Use(9003, 1, 60);
            item.Add_Gift_Item_Use(10000, 1, 250);
            item.Add_Gift_Item_Use(10001, 1, 20);

            item.Add_Gift_Item_Use(11004, 1, 250);
            item.Add_Gift_Item_Use(11005, 1, 250);
            item.Add_Gift_Item_Use(11006, 1, 250);
            item.Add_Gift_Item_Use(11007, 1, 250);
            item.Add_Gift_Item_Use(11008, 1, 250);
            item.Add_Gift_Item_Use(11014, 1, 250);
            item.Add_Gift_Item_Use(11015, 1, 250);
            item.Add_Gift_Item_Use(11016, 1, 250);
            item.Add_Gift_Item_Use(11017, 1, 250);
            item.Add_Gift_Item_Use(11018, 1, 250);
            item.Add_Gift_Item_Use(11020, 1, 250);
            item.Add_Gift_Item_Use(11009, 1, 60);
            item.Add_Gift_Item_Use(11019, 1, 60);

            item.Add_Gift_Item_Etc(0002, 1, 250);
            item.Add_Gift_Item_Etc(0003, 1, 250);
            item.Add_Gift_Item_Etc(0006, 1, 250);
            item.Add_Gift_Item_Etc(0015, 1, 250);
            item.Add_Gift_Item_Etc(0016, 1, 250);
            item.Add_Gift_Item_Etc(0007, 1, 60);
            m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);
        }
        // 은빛 보물 상자 Lv2
        {
            item = new Item_Use("은빛 보물 상자 Lv2", 12303, "Prefab/Item/Item_Use/Gift/Box_SilverPlating_2", E_ITEM_USE_TYPE.GIFT, E_ITEM_GRADE.RARE,
                0, 0, 120000);
            item.Set_Item_Use_Gift(E_ITEM_USE_GIFT_TYPE.RANDOMBOX_INDEPENDENTTRIAL, 1, 3);
            item.m_sItemDescription = "귀하디 귀한 보물 상자이다.\n등급은 낮지만 그래도 꼴에 보물 상자라고 레어보다 낮은 등급의 아이템은 상자에 들어있지 않다.\n'은빛 보물 상자 Lv1' 보다 더 많은 아이템을 획득할 수 있다.";
            item.Add_Gift_Item_Equip(1005, 1, 250);
            item.Add_Gift_Item_Equip(1006, 1, 250);
            item.Add_Gift_Item_Equip(1009, 1, 250);
            item.Add_Gift_Item_Equip(1017, 1, 250);
            item.Add_Gift_Item_Equip(1018, 1, 250);
            item.Add_Gift_Item_Equip(1607, 1, 250);
            item.Add_Gift_Item_Equip(1608, 1, 250);
            item.Add_Gift_Item_Equip(1609, 1, 250);
            item.Add_Gift_Item_Equip(2006, 1, 250);
            item.Add_Gift_Item_Equip(2007, 1, 250);
            item.Add_Gift_Item_Equip(4004, 1, 250);
            item.Add_Gift_Item_Equip(4016, 1, 250);
            item.Add_Gift_Item_Equip(4017, 1, 250);
            item.Add_Gift_Item_Equip(4018, 1, 250);
            item.Add_Gift_Item_Equip(5008, 1, 250);
            item.Add_Gift_Item_Equip(1016, 1, 60);
            item.Add_Gift_Item_Equip(1610, 1, 60);
            item.Add_Gift_Item_Equip(1615, 1, 60);
            item.Add_Gift_Item_Equip(2003, 1, 60);

            item.Add_Gift_Item_Use(8011, 1, 250);
            item.Add_Gift_Item_Use(8017, 1, 250);
            item.Add_Gift_Item_Use(8023, 1, 250);
            item.Add_Gift_Item_Use(8025, 1, 250);
            item.Add_Gift_Item_Use(9002, 1, 250);
            item.Add_Gift_Item_Use(9007, 1, 250);
            item.Add_Gift_Item_Use(9003, 1, 60);
            item.Add_Gift_Item_Use(10000, 1, 250);
            item.Add_Gift_Item_Use(10001, 1, 20);

            item.Add_Gift_Item_Use(11004, 1, 250);
            item.Add_Gift_Item_Use(11005, 1, 250);
            item.Add_Gift_Item_Use(11006, 1, 250);
            item.Add_Gift_Item_Use(11007, 1, 250);
            item.Add_Gift_Item_Use(11008, 1, 250);
            item.Add_Gift_Item_Use(11014, 1, 250);
            item.Add_Gift_Item_Use(11015, 1, 250);
            item.Add_Gift_Item_Use(11016, 1, 250);
            item.Add_Gift_Item_Use(11017, 1, 250);
            item.Add_Gift_Item_Use(11018, 1, 250);
            item.Add_Gift_Item_Use(11020, 1, 250);
            item.Add_Gift_Item_Use(11009, 1, 60);
            item.Add_Gift_Item_Use(11019, 1, 60);

            item.Add_Gift_Item_Etc(0002, 1, 250);
            item.Add_Gift_Item_Etc(0003, 1, 250);
            item.Add_Gift_Item_Etc(0006, 1, 250);
            item.Add_Gift_Item_Etc(0015, 1, 250);
            item.Add_Gift_Item_Etc(0016, 1, 250);
            item.Add_Gift_Item_Etc(0007, 1, 60);
            m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);
        }
    }
    // 모든 소비아이템(기프트(랜덤 지급형 _ B 타입)) 데이터 로딩
    // 소비아이템(기프트(랜덤 지급형 _ B 타입)) 목록의 아이템 획득 확률의 합은 10000(100%)
    void Load_Item_Use_Gift_RandomBox_dependentTrial()
    {
        Item_Use item;
        // 초급 의문의 상자
        {
            item = new Item_Use("초급 의문의 상자", 12600, "Prefab/Item/Item_Use/Gift/Box_Wood_2", E_ITEM_USE_TYPE.GIFT, E_ITEM_GRADE.NORMAL,
                0, 0, 2000);
            item.Set_Item_Use_Gift(E_ITEM_USE_GIFT_TYPE.RANDOMBOX_DEPENDENTTRIAL, 1, 5);
            item.m_sItemDescription = "무엇이 들어있는지 알 수 없는 상자이다.";
            item.m_bDisplay_Gift_Item = true;
            item.m_sStatus_Limit_Min.SetSTATUS_LV(1);
            item.Add_Gift_Item_Equip(1002, 1, 200);
            item.Add_Gift_Item_Equip(1005, 1, 200);
            item.Add_Gift_Item_Equip(1302, 1, 200);
            item.Add_Gift_Item_Equip(1305, 1, 200);
            item.Add_Gift_Item_Equip(1602, 1, 200);
            item.Add_Gift_Item_Equip(1604, 1, 200);
            item.Add_Gift_Item_Equip(2001, 1, 150);
            item.Add_Gift_Item_Equip(2002, 1, 150);
            item.Add_Gift_Item_Equip(2004, 1, 150);
            item.Add_Gift_Item_Equip(3002, 1, 150);
            item.Add_Gift_Item_Equip(4002, 1, 150);
            item.Add_Gift_Item_Equip(4003, 1, 150);
            item.Add_Gift_Item_Equip(4005, 1, 200);
            item.Add_Gift_Item_Equip(6002, 1, 200);

            item.Add_Gift_Item_Use(8000, 5, 500);
            item.Add_Gift_Item_Use(8001, 5, 500);
            item.Add_Gift_Item_Use(8002, 5, 300);
            item.Add_Gift_Item_Use(9000, 1, 300);
            item.Add_Gift_Item_Use(9001, 1, 300);
            item.Add_Gift_Item_Use(10000, 1, 100);
            item.Add_Gift_Item_Use(11000, 1, 300);
            item.Add_Gift_Item_Use(11010, 1, 300);

            item.Add_Gift_Item_Etc(0001, 1, 800);
            item.Add_Gift_Item_Etc(0002, 1, 300);
            item.Add_Gift_Item_Etc(0003, 1, 300);
            item.Add_Gift_Item_Etc(0008, 1, 800);
            item.Add_Gift_Item_Etc(0009, 1, 800);
            item.Add_Gift_Item_Etc(0011, 1, 800);
            item.Add_Gift_Item_Etc(0014, 1, 800);
            item.Add_Gift_Item_Etc(0015, 1, 300);
            m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);
        }
        // 중급 의문의 상자
        {
            item = new Item_Use("중급 의문의 상자", 12601, "Prefab/Item/Item_Use/Gift/Box_Iron_2", E_ITEM_USE_TYPE.GIFT, E_ITEM_GRADE.NORMAL,
                0, 0, 5000);
            item.Set_Item_Use_Gift(E_ITEM_USE_GIFT_TYPE.RANDOMBOX_DEPENDENTTRIAL, 1, 5);
            item.m_sItemDescription = "무엇이 들어있는지 알 수 없는 상자이다.";
            item.m_bDisplay_Gift_Item = true;
            item.m_sStatus_Limit_Min.SetSTATUS_LV(10);
            item.Add_Gift_Item_Equip(1013, 1, 300);
            item.Add_Gift_Item_Equip(1012, 1, 100); 
            item.Add_Gift_Item_Equip(1304, 1, 300);
            item.Add_Gift_Item_Equip(1305, 1, 100);
            item.Add_Gift_Item_Equip(1611, 1, 300);
            item.Add_Gift_Item_Equip(1613, 1, 100);
            item.Add_Gift_Item_Equip(2005, 1, 300);
            item.Add_Gift_Item_Equip(2006, 1, 100);
            item.Add_Gift_Item_Equip(3012, 1, 300);
            item.Add_Gift_Item_Equip(3013, 1, 100);
            item.Add_Gift_Item_Equip(4016, 1, 200);
            item.Add_Gift_Item_Equip(4017, 1, 125);
            item.Add_Gift_Item_Equip(4018, 1, 75);

            item.Add_Gift_Item_Use(8003, 5, 200);
            item.Add_Gift_Item_Use(8004, 5, 200);
            item.Add_Gift_Item_Use(8005, 5, 200);
            item.Add_Gift_Item_Use(8009, 5, 300);
            item.Add_Gift_Item_Use(8011, 5, 300);
            item.Add_Gift_Item_Use(8017, 5, 300);
            item.Add_Gift_Item_Use(8020, 5, 300);
            item.Add_Gift_Item_Use(8029, 5, 300);
            item.Add_Gift_Item_Use(8030, 5, 200);
            item.Add_Gift_Item_Use(9008, 1, 240);
            item.Add_Gift_Item_Use(9002, 1, 240);
            item.Add_Gift_Item_Use(9004, 1, 240);
            item.Add_Gift_Item_Use(9005, 1, 240);
            item.Add_Gift_Item_Use(9003, 1, 240);
            item.Add_Gift_Item_Use(11000, 1, 300);
            item.Add_Gift_Item_Use(11001, 1, 200);
            item.Add_Gift_Item_Use(11002, 1, 100);
            item.Add_Gift_Item_Use(11000, 1, 300);
            item.Add_Gift_Item_Use(11001, 1, 200);
            item.Add_Gift_Item_Use(11012, 1, 100);

            item.Add_Gift_Item_Etc(0002, 1, 300);
            item.Add_Gift_Item_Etc(0003, 1, 300);
            item.Add_Gift_Item_Etc(0005, 1, 800);
            item.Add_Gift_Item_Etc(0006, 1, 100);
            item.Add_Gift_Item_Etc(0013, 1, 800);
            item.Add_Gift_Item_Etc(0015, 1, 300);
            item.Add_Gift_Item_Etc(0016, 1, 300);
            m_Dictionary_MonsterDrop_Use.Add(item.m_nItemCode, item);
        }
    }
    
    // 아이템 데이터 반환 함수
    public Item Get_Item_Information(int itemcode) // itemcode : 반환할 아이템 고유코드
    {
        if (m_Dictionary_MonsterDrop_Equip.ContainsKey(itemcode) == true)
        {
            return m_Dictionary_MonsterDrop_Equip[itemcode];
        }
        if (m_Dictionary_MonsterDrop_Use.ContainsKey(itemcode) == true)
        {
            return m_Dictionary_MonsterDrop_Use[itemcode];
        }
        if (m_Dictionary_MonsterDrop_Etc.ContainsKey(itemcode) == true)
        {
            return m_Dictionary_MonsterDrop_Etc[itemcode];
        }

        return null;
    }
}
