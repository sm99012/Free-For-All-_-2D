using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    private static MapManager instance = null;
    public static MapManager Instance
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
    public Dictionary<int, Map> m_Dictionary_Map_Tutorial;
    public Dictionary<int, Map> m_Dictionary_Map_Chapter1;

    public List <ReTryArea> m_List_ReTryArea_Tutorial;
    public List<ReTryArea> m_List_ReTryArea_Chapter1;

    GameObject MapObject;
    GameObject Map;
    GameObject Monster, Tree;

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

    }

    public bool InitialSet()
    {
        InitialSet_Map_Tutorial();
        InitialSet_Map_Chapter1();

        InitialSet_ReTryArea();

        return true;
    }

    //----------------------------------------------------------------------------------------

    public void InitialSet_Map_Tutorial()
    {
        m_Dictionary_Map_Tutorial = new Dictionary<int, Map>();

        Map map1 = new Map("[깊디깊은숲] 깊은숲 어딘가", 0001, 1);
        Map map2 = new Map("[깊디깊은숲] 빛조차 들지않는 깊은숲 1", 0002, 1);
        Map map3 = new Map("[깊디깊은숲] 빛조차 들지않는 깊은숲 2", 0003, 1);
        Map map4 = new Map("[깊디깊은숲] 누군가의 안식처", 0004, 1);

        map1.AddLinkedMap(map2);
        map2.AddLinkedMap(map1);
        map2.AddLinkedMap(map3);
        map2.AddLinkedMap(map4);
        map3.AddLinkedMap(map2);
        map4.AddLinkedMap(map2);

        m_Dictionary_Map_Tutorial.Add(0003, map3);
        m_Dictionary_Map_Tutorial.Add(0002, map2);
        m_Dictionary_Map_Tutorial.Add(0001, map1);
        m_Dictionary_Map_Tutorial.Add(0004, map4);
    }
    public void InitialSet_Map_Chapter1()
    {
        m_Dictionary_Map_Chapter1 = new Dictionary<int, Map>();

        Map map1 = new Map("[드넓은 초원] 빛이 드는 숲 1", 0005, 2);
        Map map2 = new Map("[드넓은 초원] 빛이 드는 숲 2", 0006, 2);
        Map map3 = new Map("[드넓은 초원] 주식회사 더 슬라 드넓은 초원 지부", 0007, 2);
        Map map4 = new Map("[드넓은 초원] 훈련장", 0008, 2);
        Map map5 = new Map("[드넓은 초원] 고양이가 노래하는 곳", 0009, 2);
        Map map6 = new Map("[드넓은 초원] 청량한 달빛 마을", 0010, 2);
        //Map map7 = new Map("[드넓은 초원] 달빛 마을 뒷골목", 0011, 2);
        //Map map8 = new Map("[드넓은 초원] 달빛 주점", 0012, 2);
        Map map9 = new Map("[드넓은 초원] 드넓은 초원 1", 0013, 2);
        //Map map10 = new Map("[드넓은 초원] 드넓은 초원 2", 0014, 2);
        //Map map11 = new Map("[드넓은 초원] 드넓은 초원 3", 0015, 2);
        //Map map12 = new Map("[드넓은 초원] 쉬어가는 길", 0016, 2);
        //Map map13 = new Map("[드넓은 초원] 협객 슬라임의 오두막", 0017, 2);
        //Map map14 = new Map("[드넓은 초원] 오목하게 가라앉은 곳", 0018, 2);
        Map map10001 = new Map("[드넓은 초원] 오목하게 가라앉은 곳", 10001, 2);

        m_Dictionary_Map_Chapter1.Add(0005, map1);
        m_Dictionary_Map_Chapter1.Add(0006, map2);
        m_Dictionary_Map_Chapter1.Add(0007, map3);
        m_Dictionary_Map_Chapter1.Add(0008, map4);
        m_Dictionary_Map_Chapter1.Add(0009, map5);
        m_Dictionary_Map_Chapter1.Add(0010, map6);
        //m_Dictionary_Map_Chapter1.Add(0011, map7);
        //m_Dictionary_Map_Chapter1.Add(0012, map8);
        m_Dictionary_Map_Chapter1.Add(0013, map9);
        //m_Dictionary_Map_Chapter1.Add(0014, map10);
        //m_Dictionary_Map_Chapter1.Add(0015, map11);
        //m_Dictionary_Map_Chapter1.Add(0016, map12);
        //m_Dictionary_Map_Chapter1.Add(0017, map13);
        //m_Dictionary_Map_Chapter1.Add(0018, map14);
        m_Dictionary_Map_Chapter1.Add(10001, map10001);
    }

    public void InitialSet_Map_Object_Tutorial()
    {
        MapObject = GameObject.Find("MapObject").transform.gameObject;

        // 0001
        Map = MapObject.transform.Find("Map_1").transform.gameObject;
        Monster = Map.transform.Find("Monster").transform.gameObject;
        Tree = Map.transform.Find("Object").transform.Find("Object_Tree").transform.gameObject;
        AddObject(0001);

        // 0002
        Map = MapObject.transform.Find("Map_2").transform.gameObject;
        Monster = Map.transform.Find("Monster").transform.gameObject;
        Tree = Map.transform.Find("Object").transform.Find("Object_Tree").transform.gameObject;
        AddObject(0002);

        // 0003
        Map = MapObject.transform.Find("Map_3").transform.gameObject;
        Monster = Map.transform.Find("Monster").transform.gameObject;
        Tree = Map.transform.Find("Object").transform.Find("Object_Tree").transform.gameObject;
        AddObject(0003);

        // 0004
        Map = MapObject.transform.Find("Map_4").transform.gameObject;
        Monster = Map.transform.Find("Monster").transform.gameObject;
        Tree = Map.transform.Find("Object").transform.Find("Object_Tree").transform.gameObject;
        AddObject(0004);
    }
    public void InitialSet_Map_Object_Chapter1()
    {
        MapObject = GameObject.Find("MapObject").transform.gameObject;

        // 0005
        Map = MapObject.transform.Find("Map_5").transform.gameObject;
        Monster = Map.transform.Find("Monster").transform.gameObject;
        Tree = Map.transform.Find("Object").transform.Find("Object_Tree").transform.gameObject;
        AddObject(0005);

        // 0006
        Map = MapObject.transform.Find("Map_6").transform.gameObject;
        Monster = Map.transform.Find("Monster").transform.gameObject;
        Tree = Map.transform.Find("Object").transform.Find("Object_Tree").transform.gameObject;
        AddObject(0006);

        // 0007
        Map = MapObject.transform.Find("Map_7").transform.gameObject;
        Monster = Map.transform.Find("Monster").transform.gameObject;
        Tree = Map.transform.Find("Object").transform.Find("Object_Tree").transform.gameObject;
        AddObject(0007);

        // 0008
        Map = MapObject.transform.Find("Map_8").transform.gameObject;
        Monster = Map.transform.Find("Monster").transform.gameObject;
        Tree = Map.transform.Find("Object").transform.Find("Object_Tree").transform.gameObject;
        AddObject(0008);

        // 0009
        Map = MapObject.transform.Find("Map_9").transform.gameObject;
        Monster = Map.transform.Find("Monster").transform.gameObject;
        Tree = Map.transform.Find("Object").transform.Find("Object_Tree").transform.gameObject;
        AddObject(0009);

        // 0010
        Map = MapObject.transform.Find("Map_10").transform.gameObject;
        Monster = Map.transform.Find("Monster").transform.gameObject;
        Tree = Map.transform.Find("Object").transform.Find("Object_Tree").transform.gameObject;
        AddObject(0010);

        // 0013
        Map = MapObject.transform.Find("Map_13").transform.gameObject;
        Monster = Map.transform.Find("Monster").transform.gameObject;
        Tree = Map.transform.Find("Object").transform.Find("Object_Tree").transform.gameObject;
        AddObject(0013);

        // 10001_Boss_TeSlime
        Map = MapObject.transform.Find("Map_Boss_10001").transform.gameObject;
        //Monster = Map.transform.Find("Monster").transform.gameObject;
        Tree = Map.transform.Find("Object").transform.Find("Object_Tree").transform.gameObject;
        AddObject(10001);
    }

    // Scene 이동.
    public bool ChangeScene(int scenecode)
    {
        switch(scenecode)
        {
            case 0:
                {
                    InitialSet_Map_Object_Tutorial();
                    Player_Total.Instance.ChangeMap(true, m_Dictionary_Map_Tutorial[0001]);
                    Player_Total.Instance.transform.position = new Vector3(-7f, 0.5f, 0);
                    //GUIManager_Total.Instance.Update_ChangeMap();
                } break;
            case 1:
                {
                    InitialSet_Map_Object_Tutorial();
                    Player_Total.Instance.ChangeMap(true, m_Dictionary_Map_Tutorial[0003]);
                    Player_Total.Instance.transform.position = new Vector3(18.5f, 0.66f, 0);
                    //GUIManager_Total.Instance.Update_ChangeMap();
                } break;
            case 2:
                {
                    InitialSet_Map_Object_Chapter1();
                    Player_Total.Instance.ChangeMap(true, m_Dictionary_Map_Chapter1[0005]);
                    Player_Total.Instance.transform.position = new Vector3(-0.76f, 5f, 0);
                    //GUIManager_Total.Instance.Update_ChangeMap();
                } break;
        }

        return true;
    }

    // 맵 이동시 오브젝트 활성화.
    // 오브젝트 활성화 변수를 변경.
    public void Update_Map_Object(Map offmap, Map onmap)
    {
        // 이전 맵 오브젝트 비활성화.
        for (int i = 0; i < offmap.m_List_Tree.Count; i++)
        {
            offmap.m_List_Tree[i].m_bPlay = false;
        }
        for (int i = 0; i < offmap.m_List_Monster.Count; i++)
        {
            offmap.m_List_Monster[i].m_bPlay = false;
        }
        // 이후 맵 오브젝트 활성화.
        for (int i = 0; i < onmap.m_List_Tree.Count; i++)
        {
            onmap.m_List_Tree[i].m_bPlay = true;
        }
        Debug.Log(onmap.m_List_Tree.Count);
        for (int i = 0; i < onmap.m_List_Monster.Count; i++)
        {
            onmap.m_List_Monster[i].m_bPlay = true;
        }
    }
    // 게임 시작시 1회 실행.
    public void Update_Map_Object(Map onmap)
    {
        // 이후 맵 오브젝트 활성화.
        for (int i = 0; i < onmap.m_List_Tree.Count; i++)
        {
            onmap.m_List_Tree[i].m_bPlay = true;
        }
        for (int i = 0; i < onmap.m_List_Monster.Count; i++)
        {
            onmap.m_List_Monster[i].m_bPlay = true;
        }
    }

    // 맵에 오브젝트(몬스터, 투명화 가능 나무 등) 추가.
    void AddObject(int mapcode)
    {
        if (mapcode < 0005)
        {
            m_Dictionary_Map_Tutorial[mapcode].m_List_Monster.Clear();
            for (int i = 0; i < Monster.transform.childCount; i++)
            {
                m_Dictionary_Map_Tutorial[mapcode].m_List_Monster.Add(Monster.transform.GetChild(i).GetComponent<Monster_Total>());
            }
            m_Dictionary_Map_Tutorial[mapcode].m_List_Tree.Clear();
            for (int i = 0; i < Tree.transform.childCount; i++)
            {
                if (Tree.transform.GetChild(i).name == "Tree 1" || Tree.transform.GetChild(i).name == "Tree 2")
                {
                    m_Dictionary_Map_Tutorial[mapcode].m_List_Tree.Add(Tree.transform.GetChild(i).GetComponent<Tree>());
                }
            }
        }
        else if (mapcode < 0014 || mapcode == 10001)
        {
            m_Dictionary_Map_Chapter1[mapcode].m_List_Monster.Clear();
            for (int i = 0; i < Monster.transform.childCount; i++)
            {
                m_Dictionary_Map_Chapter1[mapcode].m_List_Monster.Add(Monster.transform.GetChild(i).GetComponent<Monster_Total>());
            }
            m_Dictionary_Map_Chapter1[mapcode].m_List_Tree.Clear();
            for (int i = 0; i < Tree.transform.childCount; i++)
            {
                if (Tree.transform.GetChild(i).name == "Tree 1" || Tree.transform.GetChild(i).name == "Tree 2")
                {
                    m_Dictionary_Map_Chapter1[mapcode].m_List_Tree.Add(Tree.transform.GetChild(i).GetComponent<Tree>());
                }
            }
        }
    }

    // 리트라이 지역 설정.
    void InitialSet_ReTryArea()
    {
        m_List_ReTryArea_Tutorial = new List<ReTryArea>();
        m_List_ReTryArea_Chapter1 = new List<ReTryArea>();

        ReTryArea ra; Vector3 pos;


        // Tutorial Map
        pos = new Vector3(-5, -0.2f, 0);
        ra = new ReTryArea(m_Dictionary_Map_Tutorial[0001], pos);
        m_List_ReTryArea_Tutorial.Add(ra);


        // Chapter1
        pos = new Vector3(-10, -4.2f, 0);
        ra = new ReTryArea(m_Dictionary_Map_Chapter1[0010], pos);
        m_List_ReTryArea_Chapter1.Add(ra);

        pos = new Vector3(6.8f, -12.5f, 0);
        ra = new ReTryArea(m_Dictionary_Map_Chapter1[0013], pos);
        m_List_ReTryArea_Chapter1.Add(ra);

        pos = new Vector3(3, 3, 0);
        ra = new ReTryArea(m_Dictionary_Map_Chapter1[0005], pos);
        m_List_ReTryArea_Chapter1.Add(ra);
    }
}

public class ReTryArea
{
    public Map m_Map_ReTry;
    public Vector3 m_vReTryPos;

    public ReTryArea(Map map, Vector3 pos)
    {
        this.m_Map_ReTry = map;
        this.m_vReTryPos = pos;
    }
}
