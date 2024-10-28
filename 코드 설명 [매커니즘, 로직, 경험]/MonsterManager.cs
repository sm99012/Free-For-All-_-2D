using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// ※ 싱글톤패턴을 적용한 MonsterManager 클래스를 이용해 모든 몬스터 관련 정보(몬스터 설명, 출몰 지역, 리스폰 시간, 드랍 아이템, 스탯(능력치, 평판))를 관리한다.
//    해당 MonsterManager 클래스를 이용해 몬스터 도감 기능을 구현했다. 또한 각각의 몬스터가 보유한 드랍 아이템 저장소를 MonsterManager 클래스로 통합 관리함으로 메모리 최적화를 추구했다.
//    최적화 관련 정보는 아래 링크를 참조해 주세요.
//
//

public class MonsterManager : MonoBehaviour
{
    private static MonsterManager instance = null;
    public static MonsterManager Instance
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

        InitialSet(); // 변수 초기화 및 몬스터 도감(정보) 데이터 로딩
    }

    public static Dictionary<int, MonsterDictionary> m_Dictionary_Monster; // 몬스터 도감 딕셔너리. Dictionary <Key : 몬스터 고유코드, Value : 몬스터 도감(정보)>

    // 몬스터 도감 해금 비율 계산 관련 변수
    float m_fMonster_Dictionary_Solve_Rate_Before; // 이전 몬스터 도감 해금 비율
    float m_fMonster_Dictionary_Solve_Rate_After;  // 이후 몬스터 도감 해금 비율

    // 변수 초기화 및 몬스터 도감(정보) 데이터 로딩
    public void InitialSet()
    {
        m_Dictionary_Monster = new Dictionary<int, MonsterDictionary>();
        MonsterDictionary md;

        if (m_Dictionary_Monster.ContainsKey(3) == false)
        {
            md = new MonsterDictionary("짙은 앤트", 3, "Prefab/Monster_Sprite/Ents1_Sprite", 100, 0, E_MONSTER_KIND.ENTS, E_MONSTER_GRADE.S3);
            md.MonsterDictionary_Add_25P("주로 '드넓은 초원' 에 서식하는 '짙은 앤트' 이다.\n'짙은 앤트' 는 매우 천천히 살아가는 존재로 매우 온화하고 강직하다. 그 성격에 걸맞게 매우 질긴 가죽을 가지고 있다.");
            md.MonsterDictionary_Add_50P("짙은 갈색을 띠고 있으며 자연환경과 잘 어우러져있다.\n매우 천천히 이동하는것이 특징이다.\n'짙은 앤트' 는 강력하기 때문에 사냥에 많은 준비가 필요할 것 같다.", "[드넓은 초원] 빛이 드는 숲 1", "[드넓은 초원] 빛이 드는 숲 2", "[드넓은 초원] 고양이가 노래하는 곳", "[드넓은 초원] 청량한 달빛 마을", "[드넓은 초원] 드넓은 초원 1");
            md.MonsterDictionary_Add_75P("'짙은 앤트' 는 10초 뒤 다시 스폰된다.\n'짙은 앤트' 의 옹이구멍에서는 다양한 것들이 발견된다. 또한 질기지만 가벼운 '짙은 앤트의 나무가죽' 을 얻을 수 있다.", new STATUS(12, 0, 7, 80, 80, 0, 0, 8, 8, 0, 0, 0, 15, 15, 15, 0, 5));
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip(1011, 1613, 1012, 1617, 1015, 1610, 2003, 4016, 4017, 4018);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip_DropRate(50, 50, 12, 12, 5, 1, 1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip_Count(1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use(8029, 8030, 12600);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use_DropRate(50, 50, 50);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use_Count(1, 1, 1);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc(0003, 0014);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc_DropRate(5000, 7500);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc_Count(1, 3);
            md.MonsterDictionary_Add_100P_Death_Reward_Gold(2, 2, 20, 3000);
            md.MonsterDictionary_Add_100P_SS_Death(new STATUS(0, 0, 13), new SOC(0));

            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip(1011, 1613, 1012, 1617);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip_DropRate(25, 25, 5, 5);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip_Count(1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use_DropRate();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use_Count();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc(0003, 0014);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc_DropRate(2500, 5000);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc_Count(1, 3);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Gold(2, 2, 20, 3000);
            md.MonsterDictionary_Add_100P_SS_Goaway(new STATUS(0), new SOC(0));
            m_Dictionary_Monster.Add(3, md);
        }
        if (m_Dictionary_Monster.ContainsKey(7) == false)
        {
            md = new MonsterDictionary("훈련용 허수아비", 7, "Prefab/Monster_Sprite/Puppet1_Sprite", 100, 0, E_MONSTER_KIND.OBJECT, E_MONSTER_GRADE.S1);
            md.MonsterDictionary_Add_25P("새를 쫓아내기 위한 용도, 혹은 체력 단련을 위해 만들어진 '훈련용 허수아비' 이다.\n끈질긴 생명력을 자랑하는 '수풀' 과 나뭇가지, 헝겊을 덧대어 만들었기에 내구도는 그리 좋지 못한 편이다.");
            md.MonsterDictionary_Add_50P("나뭇가지와 헝겊으로 '수풀' 을 감싸고 있기에 '수풀' 의 가시에 찔릴 염려는 없다. 그저 편하게 사용하면 될것같다.", "[깊디깊은숲] 깊은숲 어딘가", "[드넓은 초원] 훈련장", "[드넓은 초원] 청량한 달빛 마을");
            md.MonsterDictionary_Add_75P("망가진 '훈련용 허수아비' 는 10초 뒤 그 자리에서 재생하여 다시 쓸 수 있게 된다.\n'훈련용 허수아비' 에서 얻을 수 있는것은 딱히 없다.", new STATUS(1, 0, 0, 20, 20));
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip();
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip_DropRate();
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip_Count();
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use();
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use_DropRate();
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use_Count();
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc();
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc_DropRate();
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc_Count();
            md.MonsterDictionary_Add_100P_Death_Reward_Gold();
            md.MonsterDictionary_Add_100P_SS_Death(new STATUS(0), new SOC(0));

            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip_DropRate();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip_Count();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use_DropRate();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use_Count();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc_DropRate();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc_Count();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Gold();
            md.MonsterDictionary_Add_100P_SS_Goaway(new STATUS(0), new SOC(0));
            m_Dictionary_Monster.Add(7, md);
        }
        if (m_Dictionary_Monster.ContainsKey(9) == false)
        {
            md = new MonsterDictionary("전투용 허수아비", 9, "Prefab/Monster_Sprite/Puppet2_Sprite", 100, 0, E_MONSTER_KIND.OBJECT, E_MONSTER_GRADE.S1);
            md.MonsterDictionary_Add_25P("전투 훈련을 위해 만들어진 '전투용 허수아비' 이다.\n끈질긴 생명력을 자랑하는 '수풀' 과 나뭇가지, 헝겊 그리고 질기디 질긴 '짙은 앤트의 나무가죽' 을 덧대어 만들었기에 내구도와 방어력이 제법 괜찮은 편이다.\n");
            md.MonsterDictionary_Add_50P("나뭇가지와 헝겊으로 '수풀' 을 감싸고 있기에 '수풀' 의 가시에 찔릴 염려는 없다. 전투 훈련용으로 딱이다.", "[드넓은 초원] 청량한 달빛 마을");
            md.MonsterDictionary_Add_75P("망가진 '전투용 허수아비' 는 10초 뒤 그 자리에서 재생하여 다시 쓸 수 있게 된다.\n'전투용 허수아비' 에서 얻을 수 있는것은 딱히 없다.", new STATUS(1, 0, 0, 100, 100));
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip();
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip_DropRate();
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip_Count();
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use();
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use_DropRate();
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use_Count();
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc();
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc_DropRate();
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc_Count();
            md.MonsterDictionary_Add_100P_Death_Reward_Gold();
            md.MonsterDictionary_Add_100P_SS_Death(new STATUS(0), new SOC(0));

            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip_DropRate();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip_Count();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use_DropRate();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use_Count();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc_DropRate();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc_Count();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Gold();
            md.MonsterDictionary_Add_100P_SS_Goaway(new STATUS(0), new SOC(0));
            m_Dictionary_Monster.Add(9, md);
        }
        if (m_Dictionary_Monster.ContainsKey(8) == false)
        {
            md = new MonsterDictionary("수풀", 8, "Prefab/Monster_Sprite/Bush1_Sprite", 100, 0, E_MONSTER_KIND.OBJECT, E_MONSTER_GRADE.S1);
            md.MonsterDictionary_Add_25P("이 세계 전역에 퍼져있는 '수풀' 이다.\n주변 땅의 양분을 빨아먹는다고 알려져 있어 속히 제거해야한다.\n또한 생명력이 끈질겨 '수풀' 이 제거된곳에 다시 나는 경우가 많다.");
            md.MonsterDictionary_Add_50P("불그스름한 빛깔을 띠고 있어 위화감을 조성한다.\n스스로 움직일 수 없는 식물 덩어리지만, 가시가 있어 근처에 가면 찔릴 수도 있다.", "[깊디깊은숲] 전역", "[드넓은 초원] 전역");
            md.MonsterDictionary_Add_75P("'수풀' 이 제거되고 30초 뒤 또다른 '수풀' 이 그자리에서 자라난다.\n'수풀' 속에서는 다양한 것들이 발견된다. 운이 좋다면 제법 값어치 나가는것도 얻을 수 있다.\n드물게 좀 더 질긴 '수풀' 이 자라나는 경우도 있다.", new STATUS(1, 0, 0, 5, 5));
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip();
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip_DropRate();
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip_Count();
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use(8013, 8011, 9000);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use_DropRate(1000, 500, 500);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use_Count(1, 1, 1);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc(0014);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc_DropRate(3000);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc_Count(1);
            md.MonsterDictionary_Add_100P_Death_Reward_Gold(1, 1, 2, 1000);
            md.MonsterDictionary_Add_100P_SS_Death(new STATUS(0), new SOC(0));

            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip_DropRate();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip_Count();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use_DropRate();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use_Count();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc_DropRate();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc_Count();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Gold();
            md.MonsterDictionary_Add_100P_SS_Goaway(new STATUS(0), new SOC(0));
            m_Dictionary_Monster.Add(8, md);
        }
        if (m_Dictionary_Monster.ContainsKey(10) == false)
        {
            md = new MonsterDictionary("가시덤불", 10, "Prefab/Monster_Sprite/Bush2_Sprite", 100, 0, E_MONSTER_KIND.OBJECT, E_MONSTER_GRADE.S2);
            md.MonsterDictionary_Add_25P("황량한 '구양 사막' 의 억센 '가시덤불' 이다.\n주변 땅의 양분 뿐만 아니라 작은 생명체까지 잡아 먹는다고 알려져 있다.\n'가시덤불' 은 지금도 '구양 사막' 에서 이 세계 전역으로 퍼져나가고 있다.");
            md.MonsterDictionary_Add_50P("색만 봐도 알 수 있듯이 굉장히 매말라 있다.\n스스로 움직일 수 없는 식물 덩어리지만, 가시가 있어 근처에 가면 잡아 먹힐 수도 있다.", "[드넓은 초원] 드넓은 초원 1");
            md.MonsterDictionary_Add_75P("'가시덤불' 이 제거되고 30초 뒤 또다른 '가시덤불' 이 나타난다.\n'가시덤불' 속에서는 다양한 것들이 발견된다. 미처 생각치도 못한 것들을 얻게 될 수도 있다.", new STATUS(12, 0, 0, 20, 20, 0, 0, 5, 5, 5));
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip();
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip_DropRate();
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip_Count();
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use(8013, 8014, 8025, 8003, 8004, 8005);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use_DropRate(500, 500, 100, 50, 50, 50);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use_Count(2, 2, 2, 2, 2, 2);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc(0014);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc_DropRate(3000);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc_Count(1);
            md.MonsterDictionary_Add_100P_Death_Reward_Gold(1, 1, 2, 1000);
            md.MonsterDictionary_Add_100P_SS_Death(new STATUS(0), new SOC(0));

            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip_DropRate();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip_Count();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use_DropRate();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use_Count();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc_DropRate();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc_Count();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Gold();
            md.MonsterDictionary_Add_100P_SS_Goaway(new STATUS(0), new SOC(0));
            m_Dictionary_Monster.Add(10, md);
        }
        if (m_Dictionary_Monster.ContainsKey(2000) == false)
        {
            md = new MonsterDictionary("초원 슬라임", 2000, "Prefab/Monster_Sprite/Slime1_Sprite", 100, 0, E_MONSTER_KIND.SLIME, E_MONSTER_GRADE.S1);
            md.MonsterDictionary_Add_25P("주로 '드넓은 초원' 에 서식한다.\n어떠한 환경이라도 잘 적응하고 변화하는 슬라임 종족답게 '초원 슬라임' 은 숲과 초원의 뛰어난 재생능력을 가지고 있다.\n개체마다 다양한 성격과 신념을 가지고 있다.");
            md.MonsterDictionary_Add_50P("주변 환경에 잘 적응하여 대체로 '드넓은 초원' 의 색인 초록을 띤다.\n공격력과 방어력은 보잘것 없기에 손쉽게 사냥할 수 있다.", "[드넓은 초원] 전역");
            md.MonsterDictionary_Add_75P("'초원 슬라임' 은 10초 뒤 다시 스폰된다.\n'초원 슬라임' 에게서는 뛰어난 재생효과를 지닌 '초원 슬라임의 덩어리' 를 얻을 수 있다. 그 밖에 얻을 수 있는것은 미처 소화시키지 못한 여러 잡다한것들 뿐이다.", new STATUS(4, 0, 0, 30, 30, 1, 1, 3, 3, 3, 0, 0, 30, 2, 2, 0, 2f));
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip(1000, 1001, 1300, 1301, 1600, 1601, 2001, 3001, 4001, 4003, 4005, 4008, 4012, 6001);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip_DropRate(50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip_Count(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use(8000, 8001, 8002, 8012, 8019, 8013, 8015);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use_DropRate(120, 120, 120, 500, 500, 250, 250);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use_Count(1, 1, 1, 1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc(0001, 0002, 0011, 0012);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc_DropRate(3000, 500, 100, 1000);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc_Count(1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Death_Reward_Gold(1, 1, 3, 3000);
            md.MonsterDictionary_Add_100P_SS_Death(new STATUS(0, 0, 3), new SOC(0));

            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip(1000, 1001, 1300, 1301, 1600, 1601, 2001, 3001, 4001, 4003, 4005, 4008, 4012, 6001);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip_DropRate(25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip_Count(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use(8000, 8001, 8002, 8012, 8019, 8013, 8015);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use_DropRate(50, 50, 50, 250, 250, 120, 120);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use_Count(1, 1, 1, 1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc(0001, 0002, 0011, 0012);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc_DropRate(1000, 100, 100, 1000);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc_Count(1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Gold(1, 1, 3, 3000);
            md.MonsterDictionary_Add_100P_SS_Goaway(new STATUS(0), new SOC(0));
            m_Dictionary_Monster.Add(2000, md);
        }
        if (m_Dictionary_Monster.ContainsKey(2001) == false)
        {
            md = new MonsterDictionary("잔디 머리 초원 슬라임", 2001, "Prefab/Monster_Sprite/Slime1_GrassHair_Sprite", 100, 0, E_MONSTER_KIND.SLIME, E_MONSTER_GRADE.S3);
            md.MonsterDictionary_Add_25P("주로 '드넓은 초원' 에 서식한다.\n'초원 슬라임' 에서 진화한 개체로 상당히 개성있는 모습을 보여준다.\n엉뚱하지만 불의를 보면 참지 못하는 성격이기에 '드넓은 초원'의 다른 누군가와의 마찰이 잦은 편이다.");
            md.MonsterDictionary_Add_50P("주변 환경에 잘 적응하여 대체로 '드넓은 초원' 의 색인 초록을 띠고 있으며 특이한 헤어스타일을 가지고 있다.\n'잔디 머리 초원 슬라임' 의 성격 탓에 그는 항상 누군가와 싸워왔다. 따라서 다른 '초원 슬라임' 들 보다는 강력한 편이다.", "[드넓은 초원] 청량한 달빛 마을", "[드넓은 초원] 드넓은 초원 1");
            md.MonsterDictionary_Add_75P("'잔디 머리 초원 슬라임' 은 10초 뒤 다시 스폰된다.\n'잔디 머리 초원 슬라임' 에게서는 뛰어난 재생효과를 지닌 '초원 슬라임의 덩어리' 를 얻을 수 있다. 그 밖에 얻을 수 있는것은 미처 소화시키지 못한 여러 잡다한것들 뿐이다.", new STATUS(6, 0, 0, 50, 50, 3, 3, 4, 4, 4, 0, 0, 30, 3, 3, 0, 2f));
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip(1002, 1003, 1302, 1303, 1602, 1603, 2002, 3002, 3010, 4002, 4007, 6002, 4004);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip_DropRate(50, 50, 50, 50, 50, 50, 50, 12, 12, 12, 12, 12, 1);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip_Count(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use(8009, 8014, 8020, 8024, 8018, 8021, 8022);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use_DropRate(500, 500, 500, 500, 120, 250, 250);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use_Count(1, 1, 1, 1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc(0001, 0002, 0011, 0012);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc_DropRate(3000, 500, 100, 1000);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc_Count(1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Death_Reward_Gold(1, 2, 5, 3000);
            md.MonsterDictionary_Add_100P_SS_Death(new STATUS(0, 0, 5), new SOC(0));

            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip(1002, 1003, 1302, 1303, 1602, 1603, 2002, 3002, 3010, 4002, 4007, 6002);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip_DropRate(25, 25, 25, 25, 25, 25, 25, 5, 5, 5, 5, 5);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip_Count(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use(8009, 8014, 8020, 8024, 8018, 8021, 8022);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use_DropRate(250, 250, 250, 250, 50, 120, 120);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use_Count(1, 1, 1, 1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc(0001, 0002, 0011, 0012);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc_DropRate(1000, 100, 100, 1000);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc_Count(1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Gold(1, 2, 5, 3000);
            md.MonsterDictionary_Add_100P_SS_Goaway(new STATUS(0), new SOC(0));
            m_Dictionary_Monster.Add(2001, md);
        }
        if (m_Dictionary_Monster.ContainsKey(2002) == false)
        {
            md = new MonsterDictionary("웃고있는 초원 슬라임", 2002, "Prefab/Monster_Sprite/Slime1_Smile_Sprite", 100, 0, E_MONSTER_KIND.SLIME, E_MONSTER_GRADE.S2);
            md.MonsterDictionary_Add_25P("주로 '드넓은 초원' 에 서식한다.\n'초원 슬라임' 에서 진화한 개체로 그냥 웃고있다.\n그저 삶을 즐기고 싶어하는 편이다.");
            md.MonsterDictionary_Add_50P("주변 환경에 잘 적응하여 대체로 '드넓은 초원' 의 색인 초록을 띠고 있으며 항상 웃고있다.\n'웃고있는 초원 슬라임' 은 강력하지는 않지만 매우 빠르다.", "[드넓은 초원] 고양이가 노래하는 곳");
            md.MonsterDictionary_Add_75P("'웃고있는 초원 슬라임' 은 10초 뒤 다시 스폰된다.\n'웃고있는 초원 슬라임' 에게서는 뛰어난 재생효과를 지닌 '초원 슬라임의 덩어리' 를 얻을 수 있다. 그 밖에 얻을 수 있는것은 미처 소화시키지 못한 여러 잡다한것들 뿐이다.", new STATUS(8, 0, 0, 30, 30, 1, 1, 2, 2, 2, 0, 0, 50, 2, 2, 0, 2));
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip(1004, 1007, 1008, 1010, 1304, 3012, 1305, 1605, 2004, 3011, 1614, 1009, 1606, 1607, 1608, 1609);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip_DropRate(50, 50, 50, 50, 50, 50, 12, 12, 12, 12, 12, 1, 1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip_Count(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use(8026, 8027, 8028, 8025, 9001);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use_DropRate(250, 250, 250, 10, 120);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use_Count(1, 1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc(0001, 0002, 0011, 0012);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc_DropRate(3000, 500, 100, 1000);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc_Count(1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Death_Reward_Gold(1, 1, 3, 3000);
            md.MonsterDictionary_Add_100P_SS_Death(new STATUS(0, 0, 8), new SOC(0));

            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip(1004, 1007, 1008, 1010, 1304, 3012, 1305, 1605, 2004, 3011, 1614);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip_DropRate(25, 25, 25, 25, 25, 25, 5, 5, 5, 5, 5);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip_Count(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use(8026, 8027, 8028, 8025, 9001);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use_DropRate(120, 120, 120, 10, 50);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use_Count(1, 1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc(0001, 0002, 0011, 0012);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc_DropRate(1000, 100, 100, 1000);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc_Count(1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Gold(1, 1, 3, 3000);
            md.MonsterDictionary_Add_100P_SS_Goaway(new STATUS(0), new SOC(0));
            m_Dictionary_Monster.Add(2002, md);
        }
        if (m_Dictionary_Monster.ContainsKey(2003) == false)
        {
            md = new MonsterDictionary("상인단 경비 초원 슬라임", 2003, "Prefab/Monster_Sprite/Slime1_RedHat1_Sprite", 100, 0, E_MONSTER_KIND.SLIME, E_MONSTER_GRADE.S2);
            md.MonsterDictionary_Add_25P("주로 '드넓은 초원' 에 서식한다.\n'초원 슬라임' 에서 진화한 개체로 끊임없는 수련을 통해 그 능력을 인정받아 '주식회사 더 슬라' 에 입사하였다.\n시키는건 묻지도 따지지도 않고 다 하는편이다. 오로지 그의 임무에 충실하게 임할 뿐.");
            md.MonsterDictionary_Add_50P("주변 환경에 잘 적응하여 대체로 '드넓은 초원' 의 색인 초록을 띠고 있으며 '주식회사 더 슬라' 의 빨간 모자를 쓰고 있다.\n'상인단 경비 초원 슬라임' 은 끊임없는 수련 덕에 다른 '초원 슬라임' 들 보다는 강력한 편이다.", "[드넓은 초원] 빛이 드는 숲 2", "[드넓은 초원] 주식회사 더 슬라 드넓은 초원 지부");
            md.MonsterDictionary_Add_75P("'상인단 경비 초원 슬라임' 은 10초 뒤 다시 스폰된다.\n'상인단 경비 초원 슬라임' 에게서는 뛰어난 재생효과를 지닌 '초원 슬라임의 덩어리' 와 '주식회사 더 슬라' 의 일원임을 알려주는 '주식회사 더 슬라 사원증' 을 얻을 수 있다. 그 밖에 얻을 수 있는것은 미처 소화시키지 못한 여러 잡다한것들 뿐이다.", new STATUS(6, 0, 0, 60, 60, 1, 1, 5, 5, 5, 0, 0, 30, 7, 7, 0, 2f));
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip(1002, 1003, 1302, 1303, 1602, 1603, 2002, 3002, 3010, 4002, 4007, 6002, 4004);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip_DropRate(50, 50, 50, 50, 50, 50, 50, 12, 12, 12, 12, 12, 1);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip_Count(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use(8009, 8014, 8020, 8024, 8018, 8021, 8022);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use_DropRate(500, 500, 500, 500, 120, 250, 250);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use_Count(1, 1, 1, 1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc(0001, 0002, 0005, 0011, 0012);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc_DropRate(3000, 500, 7500, 100, 1000);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc_Count(1, 1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Death_Reward_Gold(2, 5, 10, 3000);
            md.MonsterDictionary_Add_100P_SS_Death(new STATUS(0, 0, 6), new SOC(0));

            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip(1002, 1003, 1302, 1303, 1602, 1603, 2002, 3002, 3010, 4002, 4007, 6002);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip_DropRate(25, 25, 25, 25, 25, 25, 25, 5, 5, 5, 5, 5);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip_Count(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use(8009, 8014, 8020, 8024, 8018, 8021, 8022);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use_DropRate(250, 250, 250, 250, 50, 120, 120);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use_Count(1, 1, 1, 1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc(0001, 0002, 0011, 0012);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc_DropRate(1000, 100, 100, 1000);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc_Count(4, 4, 1, 1);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Gold(1, 5, 10, 3000);
            md.MonsterDictionary_Add_100P_SS_Goaway(new STATUS(0), new SOC(0));
            m_Dictionary_Monster.Add(2003, md);
        }
        if (m_Dictionary_Monster.ContainsKey(2100) == false)
        {
            md = new MonsterDictionary("큰 초원 슬라임", 2100, "Prefab/Monster_Sprite/Slime2_Sprite", 100, 0, E_MONSTER_KIND.SLIME, E_MONSTER_GRADE.S3);
            md.MonsterDictionary_Add_25P("주로 '드넓은 초원' 에 서식한다.\n'초원 슬라임' 에서 진화한 개체로 매우 강력해 졌다. 그러나 왜, 어떻게 진화 했는지는 아무도 모른다. 그냥 단순히 남들보다 많이 먹었기에 진화를 했다는게 학계의 정설이다.");
            md.MonsterDictionary_Add_50P("주변 환경에 잘 적응하여 대체로 '드넓은 초원' 의 색인 초록을 띠고 있으며 매우 크다.\n큰 덩치에 걸맞게 '큰 초원 슬라임' 의 무게를 이용한 공격은 기절할만큼 강력하다. 또 건들리기만 해도 상당히 아프다.\n죽으면 1 ~ 6 마리의 '초원 슬라임' 으로 분열하여 공격한다.", "[드넓은 초원] 고양이가 노래하는 곳", "[드넓은 초원] 드넓은 초원 1");
            md.MonsterDictionary_Add_75P("'큰 초원 슬라임' 은 20초 뒤 다시 스폰된다.\n'큰 초원 슬라임' 에게서는 뛰어난 재생효과를 지닌 '초원 슬라임의 덩어리' 를 얻을 수 있다. 그 밖에 얻을 수 있는것은 미처 소화시키지 못한 여러 잡다한것들 뿐이다.", new STATUS(11, 0, 0, 220, 220, 10, 10, 6, 6, 6, 0, 0, 15, 10, 10, 0, 4));
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip(1014, 1611, 1612, 3013, 1011, 1613, 1013, 2005, 1012, 1617, 1015, 2006, 1610, 2003);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip_DropRate(50, 50, 50, 50, 50, 50, 12, 12, 12, 12, 12, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip_Count(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use(8003, 8004, 8005, 12600);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use_DropRate(120, 120, 120, 50);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use_Count(2, 2, 2, 1);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc(0001, 0002, 0011, 0012);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc_DropRate(3000, 500, 5000, 1000);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc_Count(4, 4, 2, 3);
            md.MonsterDictionary_Add_100P_Death_Reward_Gold(3, 1, 10, 3000);
            md.MonsterDictionary_Add_100P_SS_Death(new STATUS(0, 0, 8), new SOC(0));

            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip(1014, 1611, 1612, 3013, 1011, 1613, 1013, 2005, 1012, 1617, 1015);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip_DropRate(25, 25, 25, 25, 25, 25, 5, 5, 5, 5, 5);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip_Count(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use(8003, 8004, 8005);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use_DropRate(50, 50, 50);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use_Count(1, 1, 1);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc(0001, 0002, 0011, 0012);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc_DropRate(1000, 100, 2500, 1000);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc_Count(4, 4, 1, 1);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Gold(1, 1, 10, 3000);
            md.MonsterDictionary_Add_100P_SS_Goaway(new STATUS(0), new SOC(0));
            m_Dictionary_Monster.Add(2100, md);
        }
        if (m_Dictionary_Monster.ContainsKey(2200) == false)
        {
            md = new MonsterDictionary("꼬마 초원 슬라임", 2200, "Prefab/Monster_Sprite/Slime3_Sprite", 100, 0, E_MONSTER_KIND.SLIME, E_MONSTER_GRADE.S1);
            md.MonsterDictionary_Add_25P("주로 '드넓은 초원' 에 서식하지만, 모종의 이유로 '깊디깊은숲' 에서도 발견된다.\n'초원 슬라임' 의 어린 개체이다.");
            md.MonsterDictionary_Add_50P("주변 환경에 잘 적응하여 대체로 '드넓은 초원' 의 색인 초록을 띤다.\n공격력과 방어력은 보잘것 없기에 손쉽게 사냥할 수 있다.", "[깊디깊은숲] 빛조차 들지않는 깊은 숲 1", "[깊디깊은숲] 빛조차 들지않는 깊은 숲 2", "[깊디깊은숲] 누군가의 안식처", "[드넓은 초원] 전역");
            md.MonsterDictionary_Add_75P("'꼬마 초원 슬라임' 은 10초 뒤 다시 스폰된다.\n'꼬마 초원 슬라임' 에게서는 '초원 슬라임의 찌꺼기' 를 얻을 수 있다. 그 밖에 얻을 수 있는것은 미처 소화시키지 못한 여러 잡다한것들 뿐이다.", new STATUS(1, 0, 0, 6, 6, 0, 0, 1, 1, 1, 0, 0, 20, 0, 0, 0, 3));
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip(1000, 1001, 1300, 1301, 1600, 1601, 2001, 3001, 4001, 4003, 4005, 4008, 4012, 6001);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip_DropRate(50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip_Count(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use(8000, 8001, 8002, 8012, 8019, 8013, 8015);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use_DropRate(120, 120, 120, 500, 500, 250, 250);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use_Count(1, 1, 1, 1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc(0001, 0012);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc_DropRate(2000, 500);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc_Count(1, 1);
            md.MonsterDictionary_Add_100P_Death_Reward_Gold(1, 1, 2, 3000);
            md.MonsterDictionary_Add_100P_SS_Death(new STATUS(0, 0, 1), new SOC(0));

            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip(1000, 1001, 1300, 1301, 1600, 1601, 2001, 3001, 4001, 4003, 4005, 4008, 4012, 6001);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip_DropRate(25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip_Count(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use(8000, 8001, 8002, 8012, 8019, 8013, 8015);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use_DropRate(50, 50, 50, 250, 250, 120, 120);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use_Count(1, 1, 1, 1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc(0001, 0012);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc_DropRate(1000, 500);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc_Count(1, 1);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Gold(1, 1, 2, 3000);
            md.MonsterDictionary_Add_100P_SS_Goaway(new STATUS(0), new SOC(0));
            m_Dictionary_Monster.Add(2200, md);
        }
        if (m_Dictionary_Monster.ContainsKey(2201) == false)
        {
            md = new MonsterDictionary("화가 잔뜩난 꼬마 초원 슬라임", 2201, "Prefab/Monster_Sprite/Slime3_Anger_Sprite", 100, 0, E_MONSTER_KIND.SLIME, E_MONSTER_GRADE.S1);
            md.MonsterDictionary_Add_25P("주로 '드넓은 초원' 에 서식한다.\n화가 잔뜩나 닥치는대로 공격하는 '초원 슬라임' 의 어린 개체이다.");
            md.MonsterDictionary_Add_50P("너무 화가난 나머지 붉은색을 띤다.\n공격력과 방어력은 보잘것 없기에 손쉽게 사냥할 수 있다.", "[드넓은 초원] 드넓은 초원 1");
            md.MonsterDictionary_Add_75P("'화가 잔뜩난 꼬마 초원 슬라임' 은 10초 뒤 다시 스폰된다.\n'화가 잔뜩난 꼬마 초원 슬라임' 에게서는 '초원 슬라임의 찌꺼기' 를 얻을 수 있다. 그 밖에 얻을 수 있는것은 미처 소화시키지 못한 여러 잡다한것들 뿐이다.", new STATUS(1, 0, 0, 9, 9, 0, 0, 1, 1, 1, 0, 0, 30, 1, 1, 0, 3));
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip(1000, 1001, 1300, 1301, 1600, 1601, 2001, 3001, 4001, 4003, 4005, 4008, 4012, 6001);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip_DropRate(50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip_Count(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use(8000, 8001, 8002, 8012, 8019, 8013, 8015);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use_DropRate(120, 120, 120, 500, 500, 250, 250);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use_Count(1, 1, 1, 1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc(0001, 0012);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc_DropRate(2000, 500);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc_Count(1, 1);
            md.MonsterDictionary_Add_100P_Death_Reward_Gold(1, 1, 3, 3000);
            md.MonsterDictionary_Add_100P_SS_Death(new STATUS(0, 0, 1), new SOC(0));

            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip(1000, 1001, 1300, 1301, 1600, 1601, 2001, 3001, 4001, 4003, 4005, 4008, 4012, 6001);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip_DropRate(25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip_Count(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use(8000, 8001, 8002, 8012, 8019, 8013, 8015);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use_DropRate(50, 50, 50, 250, 250, 120, 120);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use_Count(1, 1, 1, 1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc(0001, 0012);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc_DropRate(1000, 500);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc_Count(1, 1);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Gold(1, 1, 3, 3000);
            md.MonsterDictionary_Add_100P_SS_Goaway(new STATUS(0), new SOC(0));
            m_Dictionary_Monster.Add(2201, md);
        }
        if (m_Dictionary_Monster.ContainsKey(2202) == false)
        {
            md = new MonsterDictionary("슬픈 꼬마 초원 슬라임", 2202, "Prefab/Monster_Sprite/Slime3_Sad_Sprite", 100, 0, E_MONSTER_KIND.SLIME, E_MONSTER_GRADE.S1);
            md.MonsterDictionary_Add_25P("주로 '드넓은 초원' 에 서식하지만, 모종의 이유로 '깊디깊은숲' 에서도 발견된다.\n너무 슬퍼서 울고있는 '초원 슬라임' 의 어린 개체이다. 뭐가 이렇게 슬픈걸까?");
            md.MonsterDictionary_Add_50P("너무 울어서 색이 조금 투명해 졌다.\n공격력과 방어력은 보잘것 없기에 손쉽게 사냥할 수 있다.", "[깊디깊은숲] 빛조차 들지않는 깊은 숲 1", "[깊디깊은숲] 빛조차 들지않는 깊은 숲 2", "[깊디깊은숲] 누군가의 안식처", "[드넓은 초원] 전역");
            md.MonsterDictionary_Add_75P("'슬픈 꼬마 초원 슬라임' 은 10초 뒤 다시 스폰된다.\n'슬픈 꼬마 초원 슬라임' 에게서는 '초원 슬라임의 찌꺼기' 를 얻을 수 있다. 그 밖에 얻을 수 있는것은 미처 소화시키지 못한 여러 잡다한것들 뿐이다.", new STATUS(1, 0, 0, 3, 3, 0, 0, 1, 1, 1, 0, 0, 40, 0, 0, 0, 1));
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip(1000, 1001, 1300, 1301, 1600, 1601, 2001, 3001, 4001, 4003, 4005, 4008, 4012, 6001);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip_DropRate(50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip_Count(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use(8000, 8001, 8002, 8012, 8019, 8013, 8015);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use_DropRate(120, 120, 120, 500, 500, 250, 250);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use_Count(1, 1, 1, 1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc(0001, 0012);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc_DropRate(2000, 500);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc_Count(1, 1);
            md.MonsterDictionary_Add_100P_Death_Reward_Gold(1, 1, 1, 3000);
            md.MonsterDictionary_Add_100P_SS_Death(new STATUS(0, 0, 1), new SOC(0, 0, 0, -1));

            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip(1000, 1001, 1300, 1301, 1600, 1601, 2001, 3001, 4001, 4003, 4005, 4008, 4012, 6001);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip_DropRate(25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip_Count(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use(8000, 8001, 8002, 8012, 8019, 8013, 8015);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use_DropRate(50, 50, 50, 250, 250, 120, 120);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use_Count(1, 1, 1, 1, 1, 1, 1);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc(0001, 0012);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc_DropRate(1000, 500);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc_Count(1, 1);
            md.MonsterDictionary_Add_100P_Goaway_Reward_Gold(1, 1, 1, 3000);
            md.MonsterDictionary_Add_100P_SS_Goaway(new STATUS(0), new SOC(0, 0, 0, 1));
            m_Dictionary_Monster.Add(2202, md);
        }
        if (m_Dictionary_Monster.ContainsKey(10001) == false)
        {
            md = new MonsterDictionary("테 슬라임", 10001, "Prefab/Monster_Sprite/TeSlime_Sprite", 4, 0, E_MONSTER_KIND.SLIME, E_MONSTER_GRADE.S5);
            md.MonsterDictionary_Add_25P("'테 슬라임' 은 '슬라임 주식회사 더 슬라' 의 낙천적인 경비대장이다.\n'드넓은 초원' 에서 그의 압도적인 무력을 감당할 수 있는 자는 아무도 없다. 아니 어쩌면 이 세계 전역에서도 그의 호적수를 찾기는 힘들 것이다.\n'용병단' 의 중급 용병 '엑슬라임' 이 자기멋대로 '테슬라임' 을 라이벌이라 생각한다.\n그는 세상 만물에 관심이 없다.\n왜 그가 '슬라임 주식회사 더 슬라' 의 경비대장을 하고있는지도 의문이다.\n종종 '그래스 슬라임' 의 제자였다는 소문이 들려오고는 한다. 소문을 뒷받침해주는 근거로는 '그래스 슬라임' 의 수제자인 '슬라임 협객' 과 친분이 있으면서도 만나기만 하면 서로 못 잡아먹어서 난리다.");
            md.MonsterDictionary_Add_50P("검을 이용한 다양한 공격을 한다. 검기를 날리거나 직접 베거나 혹은 빠르게 일직선상의 모든 적을 베어버리는 돌진 공격을 한다.\n꽤나 자주 잔다. 충분한 수면이 최고의 휴식인듯하다.\n수면 중 잠에 취해 무의식적으로 대부분의 공격을 흘려내어 피해를 감소시킨다.\n수면 후 컨디션이 좋아진 '테 슬라임' 은 체력 회복과 함께 더욱더 강력해진다.", "[깊디깊은숲] ???");
            md.MonsterDictionary_Add_75P("스승인 '그래스 슬라임' 을 지키기 위해, '드넓은 초원' 의 다수를 위해 '주식회사 더 슬라' 의 경비대장이 되었다. 그 과정에서 '슬라임 협객' 과 사이가 틀어지게 되었다.\n'구양 사막' 의 잊혀진 고대 제국의 유물인 '금강검' 을 사용하여 방해되는 모든 것들을 베어낸다.", new STATUS(21, 0, 400, 2000, 2000, 200, 200, 17, 17, 17, 20, 10, 40, 15, 15, 0, 5f));
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip();
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip_DropRate();
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Equip_Count();
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use(12600, 12601, 12302, 12303);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use_DropRate(5000, 5000, 1000, 100);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Use_Count(3, 3, 1, 1);
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc();
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc_DropRate();
            md.MonsterDictionary_Add_100P_Death_Reward_Item_Etc_Count();
            md.MonsterDictionary_Add_100P_Death_Reward_Gold();
            md.MonsterDictionary_Add_100P_SS_Death(new STATUS(0, 0, 400), new SOC(10, 0, 0, 10, 0, 10, 0, 0, -1));

            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip_DropRate();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip_Count();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use_DropRate();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Use_Count();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc_DropRate();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc_Count();
            md.MonsterDictionary_Add_100P_Goaway_Reward_Gold();
            md.MonsterDictionary_Add_100P_SS_Goaway(new STATUS(0), new SOC(0));
            m_Dictionary_Monster.Add(10001, md);
        }
    }

    // 몬스터 도감 갱신(업데이트) 함수
    public void Update_Monster_Dictionary(E_MONSTER_KIND emk, int monstercode) // emk : 몬스터 타입, monstercode : 몬스터 고유코드
    {
        if (m_Dictionary_Monster.ContainsKey(monstercode) == true) // 해당 몬스터 고유코드를 가진 몬스터가 존재하는지 판단
        {
            if (m_Dictionary_Monster[monstercode].m_nMonster_Dictionary_Solve_Max > m_Dictionary_Monster[monstercode].m_nMonster_Dictionary_Solve_Current) // 몬스터 도감 갱신 최대 마리수 > 몬스터 도감 갱신 현재 마리수
            {
                m_fMonster_Dictionary_Solve_Rate_Before = m_Dictionary_Monster[monstercode].m_fMonster_Dictionary_Solve_Rate; // 이전 몬스터 도감 해금 비율 설정

                m_Dictionary_Monster[monstercode].m_nMonster_Dictionary_Solve_Current += 1;
                m_Dictionary_Monster[monstercode].m_fMonster_Dictionary_Solve_Rate = Mathf.Round(((float)m_Dictionary_Monster[monstercode].m_nMonster_Dictionary_Solve_Current / (float)m_Dictionary_Monster[monstercode].m_nMonster_Dictionary_Solve_Max) * 100); // 몬스터 도감 해금 비율 계산

                m_fMonster_Dictionary_Solve_Rate_After = m_Dictionary_Monster[monstercode].m_fMonster_Dictionary_Solve_Rate; // 이후 몬스터 도감 해금 비율 설정

                // 몬스터 도감 해금 비율에 따른 알림 업데이트
                if (m_fMonster_Dictionary_Solve_Rate_Before == 0)
                {
                    if (m_fMonster_Dictionary_Solve_Rate_After > 0)
                        GUIManager_Total.Instance.Sparkle_GUI_UpBar_Dictionary_ON(emk, monstercode, (int)m_fMonster_Dictionary_Solve_Rate_After); // 몬스터 도감 알림 업데이트 함수
                         //Debug.Log("[" + m_Dictionary_Monster[monstercode].m_sMonster_Name + "] 등록.");
                    else if (m_fMonster_Dictionary_Solve_Rate_After >= 25)
                         GUIManager_Total.Instance.Sparkle_GUI_UpBar_Dictionary_ON(emk, monstercode, (int)m_fMonster_Dictionary_Solve_Rate_After); // 몬스터 도감 알림 업데이트 함수
                         //Debug.Log("[" + m_Dictionary_Monster[monstercode].m_sMonster_Name + "] 해금 25% 달성!");
                    else if (m_fMonster_Dictionary_Solve_Rate_After >= 50)
                         GUIManager_Total.Instance.Sparkle_GUI_UpBar_Dictionary_ON(emk, monstercode, (int)m_fMonster_Dictionary_Solve_Rate_After); // 몬스터 도감 알림 업데이트 함수
                         //Debug.Log("[" + m_Dictionary_Monster[monstercode].m_sMonster_Name + "] 해금 50% 달성!!");
                    else if (m_fMonster_Dictionary_Solve_Rate_After >= 75)
                         GUIManager_Total.Instance.Sparkle_GUI_UpBar_Dictionary_ON(emk, monstercode, (int)m_fMonster_Dictionary_Solve_Rate_After); // 몬스터 도감 알림 업데이트 함수
                         //Debug.Log("[" + m_Dictionary_Monster[monstercode].m_sMonster_Name + "] 해금 75% 달성!!!");
                    else if (m_fMonster_Dictionary_Solve_Rate_After >= 100)
                         GUIManager_Total.Instance.Sparkle_GUI_UpBar_Dictionary_ON(emk, monstercode, (int)m_fMonster_Dictionary_Solve_Rate_After); // 몬스터 도감 알림 업데이트 함수
                         //Debug.Log("[" + m_Dictionary_Monster[monstercode].m_sMonster_Name + "] 해금 100% 달성!!!!");
                }
                else if (m_fMonster_Dictionary_Solve_Rate_Before < 25)
                {
                    if (m_fMonster_Dictionary_Solve_Rate_After >= 25)
                         GUIManager_Total.Instance.Sparkle_GUI_UpBar_Dictionary_ON(emk, monstercode, (int)m_fMonster_Dictionary_Solve_Rate_After); // 몬스터 도감 알림 업데이트 함수
                         //Debug.Log("[" + m_Dictionary_Monster[monstercode].m_sMonster_Name + "] 해금 25% 달성!");
                    else if (m_fMonster_Dictionary_Solve_Rate_After >= 50)
                         GUIManager_Total.Instance.Sparkle_GUI_UpBar_Dictionary_ON(emk, monstercode, (int)m_fMonster_Dictionary_Solve_Rate_After); // 몬스터 도감 알림 업데이트 함수
                         //Debug.Log("[" + m_Dictionary_Monster[monstercode].m_sMonster_Name + "] 해금 50% 달성!!");
                    else if (m_fMonster_Dictionary_Solve_Rate_After >= 75)
                         GUIManager_Total.Instance.Sparkle_GUI_UpBar_Dictionary_ON(emk, monstercode, (int)m_fMonster_Dictionary_Solve_Rate_After); // 몬스터 도감 알림 업데이트 함수
                         //Debug.Log("[" + m_Dictionary_Monster[monstercode].m_sMonster_Name + "] 해금 75% 달성!!!");
                    else if (m_fMonster_Dictionary_Solve_Rate_After >= 100)
                         GUIManager_Total.Instance.Sparkle_GUI_UpBar_Dictionary_ON(emk, monstercode, (int)m_fMonster_Dictionary_Solve_Rate_After); // 몬스터 도감 알림 업데이트 함수
                         //Debug.Log("[" + m_Dictionary_Monster[monstercode].m_sMonster_Name + "] 해금 100% 달성!!!!");
                }
                else if (m_fMonster_Dictionary_Solve_Rate_Before < 50 )
                {
                    if (m_fMonster_Dictionary_Solve_Rate_After >= 50)
                         GUIManager_Total.Instance.Sparkle_GUI_UpBar_Dictionary_ON(emk, monstercode, (int)m_fMonster_Dictionary_Solve_Rate_After); // 몬스터 도감 알림 업데이트 함수
                         //Debug.Log("[" + m_Dictionary_Monster[monstercode].m_sMonster_Name + "] 해금 50% 달성!!");
                    else if (m_fMonster_Dictionary_Solve_Rate_After >= 75)
                         GUIManager_Total.Instance.Sparkle_GUI_UpBar_Dictionary_ON(emk, monstercode, (int)m_fMonster_Dictionary_Solve_Rate_After); // 몬스터 도감 알림 업데이트 함수
                         //Debug.Log("[" + m_Dictionary_Monster[monstercode].m_sMonster_Name + "] 해금 75% 달성!!!");
                    else if (m_fMonster_Dictionary_Solve_Rate_After >= 100)
                         GUIManager_Total.Instance.Sparkle_GUI_UpBar_Dictionary_ON(emk, monstercode, (int)m_fMonster_Dictionary_Solve_Rate_After); // 몬스터 도감 알림 업데이트 함수
                         //Debug.Log("[" + m_Dictionary_Monster[monstercode].m_sMonster_Name + "] 해금 100% 달성!!!!");
                }
                else if (m_fMonster_Dictionary_Solve_Rate_Before < 75)
                {
                    if (m_fMonster_Dictionary_Solve_Rate_After >= 75)
                         GUIManager_Total.Instance.Sparkle_GUI_UpBar_Dictionary_ON(emk, monstercode, (int)m_fMonster_Dictionary_Solve_Rate_After); // 몬스터 도감 알림 업데이트 함수
                         //Debug.Log("[" + m_Dictionary_Monster[monstercode].m_sMonster_Name + "] 해금 75% 달성!!!");
                    else if (m_fMonster_Dictionary_Solve_Rate_After >= 100)
                         GUIManager_Total.Instance.Sparkle_GUI_UpBar_Dictionary_ON(emk, monstercode, (int)m_fMonster_Dictionary_Solve_Rate_After); // 몬스터 도감 알림 업데이트 함수
                         //Debug.Log("[" + m_Dictionary_Monster[monstercode].m_sMonster_Name + "] 해금 100% 달성!!!!");
                }
                else if (m_fMonster_Dictionary_Solve_Rate_Before < 100)
                {
                    if (m_fMonster_Dictionary_Solve_Rate_After >= 100)
                         GUIManager_Total.Instance.Sparkle_GUI_UpBar_Dictionary_ON(emk, monstercode, (int)m_fMonster_Dictionary_Solve_Rate_After); // 몬스터 도감 알림 업데이트 함수
                         //Debug.Log("[" + m_Dictionary_Monster[monstercode].m_sMonster_Name + "] 해금 100% 달성!!!!");
                }
            }
        }
    }

    // 몬스터 도감 딕셔너리의 몬스터 도감 해금 비율 설정(저장)
    public void Update_Monster_Dictionary_Solve_Rate()
    {
        foreach(KeyValuePair<int, MonsterDictionary> dictionary in m_Dictionary_Monster)
        {
            dictionary.Value.m_fMonster_Dictionary_Solve_Rate = Mathf.Round(((float)dictionary.Value.m_nMonster_Dictionary_Solve_Current / (float)dictionary.Value.m_nMonster_Dictionary_Solve_Max) * 100); // 몬스터 도감 해금 비율 계산
        }
    }
}

// 몬스터 등급 : { S1, S2, S3, S4, S5, S6, S7, S8, S9 }
public enum E_MONSTER_GRADE { S1, S2, S3, S4, S5, S6, S7, S8, S9 }

public class MonsterDictionary // 몬스터 도감 데이터
{
    public string m_sMonster_Name;                  // 몬스터 이름
    
    public int m_nMonster_Code;                     // 몬스터 고유코드
    
    public Sprite m_spMonster_Sprite;               // 몬스터 스프라이트(이미지)
    
    public E_MONSTER_KIND m_eMonster_Kind;          // 몬스터 타입
    public E_MONSTER_GRADE m_eMonster_Grade;        // 몬스터 등급

    public int m_nMonster_Dictionary_Solve_Max;     // 몬스터 도감 데이터 갱신 최대 마리수
    public int m_nMonster_Dictionary_Solve_Current; // 몬스터 도감 데이터 갱신 현재 마리수
    public float m_fMonster_Dictionary_Solve_Rate;  // 몬스터 도감 데이터 해금 비율 : 0 ~ 100(%)

    // 몬스터 도감 데이터 해금 비율별 몬스터 정보
    // 몬스터 도감 데이터 해금 비율 0% : 몬스터 이름
    // 몬스터 도감 데이터 해금 비율 25% : + 몬스터 이미지, 몬스터 설명1(몬스터 컨셉)
    // 몬스터 도감 데이터 해금 비율 50% : + 몬스터 설명2(몬스터 특징1), 몬스터 출몰 지역
    // 몬스터 도감 데이터 해금 비율 75% : + 몬스터 설명3(몬스터 특징2), 몬스터 리스폰 시간, 몬스터 스탯(능력치)
    // 몬스터 도감 데이터 해금 비율 100% : + 몬스터 제거(토벌 + 놓아주기) 보상(아이템(획득 확률), 스탯(능력치, 평판))

    public string m_sMonster_Dictionary_Description_25P; // 몬스터 설명1(몬스터 도감 데이터 해금 비율 : 25%)
    public string m_sMonster_Dictionary_Description_50P; // 몬스터 설명2(몬스터 도감 데이터 해금 비율 : 50%)
    public string m_sMonster_Dictionary_Description_75P; // 몬스터 설명3(몬스터 도감 데이터 해금 비율 : 75%)

    public List<string> m_slMonster_Dictionary_AppearArea_50P; // 몬스터 출몰 지역 리스트(몬스터 도감 데이터 해금 비율 : 50%)
    
    public STATUS m_SMonster_Dictionary_STATUS; // 몬스터 스탯(능력치)(몬스터 도감 데이터 해금 비율 : 75%)
    
    // 몬스터 제거(토벌) 보상 관련 변수
    public List<int> m_nlMonster_Dictionary_Death_Reward_Item_Equip;           // 몬스터 제거(토벌) 보상 장비아이템 고유코드
    public List<int> m_nlMonster_Dictionary_Death_Reward_Item_Equip_DropRate;  // 몬스터 제거(토벌) 보상 장비아이템 획득 확률
    public List<int> m_nlMonster_Dictionary_Death_Reward_Item_Equip_Count;     // 몬스터 제거(토벌) 보상 장비아이템 개수
    public List<int> m_nlMonster_Dictionary_Death_Reward_Item_Use;             // 몬스터 제거(토벌) 보상 소비아이템 고유코드
    public List<int> m_nlMonster_Dictionary_Death_Reward_Item_Use_DropRate;    // 몬스터 제거(토벌) 보상 소비아이템 획득 확률
    public List<int> m_nlMonster_Dictionary_Death_Reward_Item_Use_Count;       // 몬스터 제거(토벌) 보상 소비아이템 개수
    public List<int> m_nlMonster_Dictionary_Death_Reward_Item_Etc;             // 몬스터 제거(토벌) 보상 기타아이템 고유코드
    public List<int> m_nlMonster_Dictionary_Death_Reward_Item_Etc_DropRate;    // 몬스터 제거(토벌) 보상 기타아이템 획득 확률
    public List<int> m_nlMonster_Dictionary_Death_Reward_Item_Etc_Count;       // 몬스터 제거(토벌) 보상 기타아이템 개수
    public int m_nMonster_Death_Reward_Gold_Min;                               // 몬스터 제거(토벌) 보상 골드(재화) 최소
    public int m_nMonster_Death_Reward_Gold_Max;                               // 몬스터 제거(토벌) 보상 골드(재화) 최대
    public int m_nMonster_Death_Reward_Gold_Count;                             // 몬스터 제거(토벌) 보상 골드(재화) 개수
    public int m_nMonster_Death_Reward_Gold_DropRate;                          // 몬스터 제거(토벌) 보상 골드(재화) 획득 확률
    public STATUS m_SMonster_Death_Reward_STATUS;                              // 몬스터 제거(토벌) 보상 스탯(능력치)
    public SOC m_SMonster_Death_Reward_SOC;                                    // 몬스터 제거(토벌) 보상 스탯(평판)
    // 몬스터 제거(놓아주기) 보상 관련 변수
    public List<int> m_nlMonster_Dictionary_Goaway_Reward_Item_Equip;          // 몬스터 제거(토벌) 보상 장비아이템 고유코드
    public List<int> m_nlMonster_Dictionary_Goaway_Reward_Item_Equip_DropRate; // 몬스터 제거(토벌) 보상 장비아이템 획득 확률
    public List<int> m_nlMonster_Dictionary_Goaway_Reward_Item_Equip_Count;    // 몬스터 제거(토벌) 보상 장비아이템 개수
    public List<int> m_nlMonster_Dictionary_Goaway_Reward_Item_Use;            // 몬스터 제거(토벌) 보상 소비아이템 고유코드
    public List<int> m_nlMonster_Dictionary_Goaway_Reward_Item_Use_DropRate;   // 몬스터 제거(토벌) 보상 소비아이템 획득 확률
    public List<int> m_nlMonster_Dictionary_Goaway_Reward_Item_Use_Count;      // 몬스터 제거(토벌) 보상 소비아이템 개수
    public List<int> m_nlMonster_Dictionary_Goaway_Reward_Item_Etc;            // 몬스터 제거(토벌) 보상 기타아이템 고유코드
    public List<int> m_nlMonster_Dictionary_Goaway_Reward_Item_Etc_DropRate;   // 몬스터 제거(토벌) 보상 기타아이템 획득 확률
    public List<int> m_nlMonster_Dictionary_Goaway_Reward_Item_Etc_Count;      // 몬스터 제거(토벌) 보상 기타아이템 개수
    public int m_nMonster_Goaway_Reward_Gold_Min;                              // 몬스터 제거(토벌) 보상 골드(재화) 최소
    public int m_nMonster_Goaway_Reward_Gold_Max;                              // 몬스터 제거(토벌) 보상 골드(재화) 최대
    public int m_nMonster_Goaway_Reward_Gold_Count;                            // 몬스터 제거(토벌) 보상 골드(재화) 개수
    public int m_nMonster_Goaway_Reward_Gold_DropRate;                         // 몬스터 제거(토벌) 보상 골드(재화) 획득 확률
    public STATUS m_SMonster_Goaway_Reward_STATUS;                             // 몬스터 제거(토벌) 보상 스탯(능력치)
    public SOC m_SMonster_Goaway_Reward_SOC;                                   // 몬스터 제거(토벌) 보상 스탯(평판)

    // 몬스터 도감 데이터 생성자
    public MonsterDictionary(string monstername, int monstercode, string path_sprite, int solvemax, int solvecurrent = 0, E_MONSTER_KIND emk = E_MONSTER_KIND.SLIME, E_MONSTER_GRADE emg = E_MONSTER_GRADE.S1)
    {
        this.m_sMonster_Name = monstername;
        this.m_nMonster_Code = monstercode;
        this.m_spMonster_Sprite = Resources.Load<Sprite>(path_sprite);
        this.m_nMonster_Dictionary_Solve_Max = solvemax;
        this.m_nMonster_Dictionary_Solve_Current = solvecurrent;
        this.m_fMonster_Dictionary_Solve_Rate = Mathf.Round((float)m_nMonster_Dictionary_Solve_Current / (float)m_nMonster_Dictionary_Solve_Max) * 100;
        this.m_eMonster_Kind = emk;
        this.m_eMonster_Grade = emg;

        m_slMonster_Dictionary_AppearArea_50P = new List<string>();
        m_nlMonster_Dictionary_Death_Reward_Item_Equip = new List<int>();
        m_nlMonster_Dictionary_Death_Reward_Item_Equip_DropRate = new List<int>();
        m_nlMonster_Dictionary_Death_Reward_Item_Equip_Count = new List<int>();
        m_nlMonster_Dictionary_Death_Reward_Item_Use = new List<int>();
        m_nlMonster_Dictionary_Death_Reward_Item_Use_DropRate = new List<int>();
        m_nlMonster_Dictionary_Death_Reward_Item_Use_Count = new List<int>();
        m_nlMonster_Dictionary_Death_Reward_Item_Etc = new List<int>();
        m_nlMonster_Dictionary_Death_Reward_Item_Etc_DropRate = new List<int>();
        m_nlMonster_Dictionary_Death_Reward_Item_Etc_Count = new List<int>();

        m_nlMonster_Dictionary_Goaway_Reward_Item_Equip = new List<int>();
        m_nlMonster_Dictionary_Goaway_Reward_Item_Equip_DropRate = new List<int>();
        m_nlMonster_Dictionary_Goaway_Reward_Item_Equip_Count = new List<int>();
        m_nlMonster_Dictionary_Goaway_Reward_Item_Use = new List<int>();
        m_nlMonster_Dictionary_Goaway_Reward_Item_Use_DropRate = new List<int>();
        m_nlMonster_Dictionary_Goaway_Reward_Item_Use_Count = new List<int>();
        m_nlMonster_Dictionary_Goaway_Reward_Item_Etc = new List<int>();
        m_nlMonster_Dictionary_Goaway_Reward_Item_Etc_DropRate = new List<int>();
        m_nlMonster_Dictionary_Goaway_Reward_Item_Etc_Count = new List<int>();

        this.m_nMonster_Death_Reward_Gold_Min = 0;
        this.m_nMonster_Death_Reward_Gold_Max = 0;
        this.m_nMonster_Death_Reward_Gold_Count = 0;
        this.m_nMonster_Death_Reward_Gold_DropRate = 0;
    }
    // 몬스터 도감 데이터 해금 비율 25% 설정 함수
    public void MonsterDictionary_Add_25P(string description) // description : 몬스터 설명1
    {
        this.m_sMonster_Dictionary_Description_25P = description;
    }
    // 몬스터 도감 데이터 해금 비율 50% 설정 함수
    public void MonsterDictionary_Add_50P(string description, params string[] appeararea) // description : 몬스터 설명2, appeararea(가변인자) : 몬스터 출몰 지역
    {
        this.m_sMonster_Dictionary_Description_50P = description;
        for (int i = 0; i < appeararea.Length; i++)
        {
            this.m_slMonster_Dictionary_AppearArea_50P.Add(appeararea[i]);
        }
    }
    // 몬스터 도감 데이터 해금 비율 75% 설정 함수
    public void MonsterDictionary_Add_75P(string description, STATUS monsterstatus) // description : 몬스터 설명3, monsterstatus : 몬스터 스탯(능력치)
    {
        this.m_sMonster_Dictionary_Description_75P = description;
        this.m_SMonster_Dictionary_STATUS = monsterstatus;
    }
    // 몬스터 도감 데이터 해금 비율 100% 설정 함수 - 몬스터 제거(토벌) 보상 장비아이템 고유코드
    public void MonsterDictionary_Add_100P_Death_Reward_Item_Equip(params int[] item) // item(가변인자) : 몬스터 제거(토벌) 보상 장비아이템 고유코드
    {
        for (int i = 0; i < item.Length; i++)
        {
            m_nlMonster_Dictionary_Death_Reward_Item_Equip.Add(item[i]);
        }
    }
    // 몬스터 도감 데이터 해금 비율 100% 설정 함수 - 몬스터 제거(토벌) 보상 장비아이템 획득 확률
    public void MonsterDictionary_Add_100P_Death_Reward_Item_Equip_DropRate(params int[] item) // item(가변인자) : 몬스터 제거(토벌) 보상 장비아이템 획득 확률
    {
        for (int i = 0; i < item.Length; i++)
        {
            m_nlMonster_Dictionary_Death_Reward_Item_Equip_DropRate.Add(item[i]);
        }
    }
    // 몬스터 도감 데이터 해금 비율 100% 설정 함수 - 몬스터 제거(토벌) 보상 장비아이템 개수
    public void MonsterDictionary_Add_100P_Death_Reward_Item_Equip_Count(params int [] item) // item(가변인자) : 몬스터 제거(토벌) 보상 장비아이템 개수
    {
        for (int i = 0; i < item.Length; i++)
        {
            m_nlMonster_Dictionary_Death_Reward_Item_Equip_Count.Add(item[i]);
        }
    }
    // 몬스터 도감 데이터 해금 비율 100% 설정 함수 - 몬스터 제거(토벌) 보상 소비아이템 고유코드
    public void MonsterDictionary_Add_100P_Death_Reward_Item_Use(params int[] item) // item(가변인자) : 몬스터 제거(토벌) 보상 소비아이템 고유코드
    {
        for (int i = 0; i < item.Length; i++)
        {
            m_nlMonster_Dictionary_Death_Reward_Item_Use.Add(item[i]);
        }
    }
    // 몬스터 도감 데이터 해금 비율 100% 설정 함수 - 몬스터 제거(토벌) 보상 소비아이템 획득 확률
    public void MonsterDictionary_Add_100P_Death_Reward_Item_Use_DropRate(params int[] item) // item(가변인자) : 몬스터 제거(토벌) 보상 소비아이템 획득 확률
    {
        for (int i = 0; i < item.Length; i++)
        {
            m_nlMonster_Dictionary_Death_Reward_Item_Use_DropRate.Add(item[i]);
        }
    }
    // 몬스터 도감 데이터 해금 비율 100% 설정 함수 - 몬스터 제거(토벌) 보상 소비아이템 개수
    public void MonsterDictionary_Add_100P_Death_Reward_Item_Use_Count(params int[] item) // item(가변인자) : 몬스터 제거(토벌) 보상 소비아이템 개수
    {
        for (int i = 0; i < item.Length; i++)
        {
            m_nlMonster_Dictionary_Death_Reward_Item_Use_Count.Add(item[i]);
        }
    }
    // 몬스터 도감 데이터 해금 비율 100% 설정 함수 - 몬스터 제거(토벌) 보상 기타아이템 고유코드
    public void MonsterDictionary_Add_100P_Death_Reward_Item_Etc(params int[] item) // item(가변인자) : 몬스터 제거(토벌) 보상 기타아이템 고유코드
    {
        for (int i = 0; i < item.Length; i++)
        {
            m_nlMonster_Dictionary_Death_Reward_Item_Etc.Add(item[i]);
        }
    }
    // 몬스터 도감 데이터 해금 비율 100% 설정 함수 - 몬스터 제거(토벌) 보상 기타아이템 획득 확률
    public void MonsterDictionary_Add_100P_Death_Reward_Item_Etc_DropRate(params int[] item) // item(가변인자) : 몬스터 제거(토벌) 보상 기타아이템 획득 확률
    {
        for (int i = 0; i < item.Length; i++)
        {
            m_nlMonster_Dictionary_Death_Reward_Item_Etc_DropRate.Add(item[i]);
        }
    }
    // 몬스터 도감 데이터 해금 비율 100% 설정 함수 - 몬스터 제거(토벌) 보상 기타아이템 개수
    public void MonsterDictionary_Add_100P_Death_Reward_Item_Etc_Count(params int[] item) // item(가변인자) : 몬스터 제거(토벌) 보상 기타아이템 개수
    {
        for (int i = 0; i < item.Length; i++)
        {
            m_nlMonster_Dictionary_Death_Reward_Item_Etc_Count.Add(item[i]);
        }
    }
    // 몬스터 도감 데이터 해금 비율 100% 설정 함수 - 몬스터 제거(토벌) 보상 골드(재화)
    public void MonsterDictionary_Add_100P_Death_Reward_Gold(int count = 0, int mingold = 0, int maxgold = 0, int rate = 0) // count : 골드(재화) 개수, mingold : 골드(재화) 최소, maxgold : 골드(재화) 최대, rate : 골드(재화) 획득 확률
    {
        this.m_nMonster_Death_Reward_Gold_Count = count;
        this.m_nMonster_Death_Reward_Gold_Max = maxgold;
        this.m_nMonster_Death_Reward_Gold_Min = mingold;
        this.m_nMonster_Death_Reward_Gold_DropRate = rate;
    }
    // 몬스터 도감 데이터 해금 비율 100% 설정 함수 - 몬스터 제거(토벌) 보상 스탯(능력치, 평판)
    public void MonsterDictionary_Add_100P_SS_Death(STATUS rewardstatus, SOC rewardsoc) // rewardstatus : ,
    {
        this.m_SMonster_Death_Reward_STATUS = rewardstatus;
        this.m_SMonster_Death_Reward_SOC = rewardsoc;
    }
    // 몬스터 도감 데이터 해금 비율 100% 설정 함수 - 
    public void MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip(params int[] item) // item(가변인자) : 
    {
        for (int i = 0; i < item.Length; i++)
        {
            m_nlMonster_Dictionary_Goaway_Reward_Item_Equip.Add(item[i]);
        }
    }
    // 몬스터 도감 데이터 해금 비율 100% 설정 함수 - 
    public void MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip_DropRate(params int[] item) // item(가변인자) : 
    {
        for (int i = 0; i < item.Length; i++)
        {
            m_nlMonster_Dictionary_Goaway_Reward_Item_Equip_DropRate.Add(item[i]);
        }
    }
    // 몬스터 도감 데이터 해금 비율 100% 설정 함수 - 
    public void MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip_Count(params int[] item) // item(가변인자) : 
    {
        for (int i = 0; i < item.Length; i++)
        {
            m_nlMonster_Dictionary_Goaway_Reward_Item_Equip_Count.Add(item[i]);
        }
    }
    // 몬스터 도감 데이터 해금 비율 100% 설정 함수 - 
    public void MonsterDictionary_Add_100P_Goaway_Reward_Item_Use(params int[] item) // item(가변인자) : 
    {
        for (int i = 0; i < item.Length; i++)
        {
            m_nlMonster_Dictionary_Goaway_Reward_Item_Use.Add(item[i]);
        }
    }
    // 몬스터 도감 데이터 해금 비율 100% 설정 함수 - 
    public void MonsterDictionary_Add_100P_Goaway_Reward_Item_Use_DropRate(params int[] item) // item(가변인자) : 
    {
        for (int i = 0; i < item.Length; i++)
        {
            m_nlMonster_Dictionary_Goaway_Reward_Item_Use_DropRate.Add(item[i]);
        }
    }
    // 몬스터 도감 데이터 해금 비율 100% 설정 함수 - 
    public void MonsterDictionary_Add_100P_Goaway_Reward_Item_Use_Count(params int[] item) // item(가변인자) : 
    {
        for (int i = 0; i < item.Length; i++)
        {
            m_nlMonster_Dictionary_Goaway_Reward_Item_Use_Count.Add(item[i]);
        }
    }
    // 몬스터 도감 데이터 해금 비율 100% 설정 함수 - 
    public void MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc(params int[] item) // item(가변인자) : 
    {
        for (int i = 0; i < item.Length; i++)
        {
            m_nlMonster_Dictionary_Goaway_Reward_Item_Etc.Add(item[i]);
        }
    }
    // 몬스터 도감 데이터 해금 비율 100% 설정 함수 - 
    public void MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc_DropRate(params int[] item) // item(가변인자) : 
    {
        for (int i = 0; i < item.Length; i++)
        {
            m_nlMonster_Dictionary_Goaway_Reward_Item_Etc_DropRate.Add(item[i]);
        }
    }
    // 몬스터 도감 데이터 해금 비율 100% 설정 함수 - 
    public void MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc_Count(params int[] item) // item(가변인자) : 
    {
        for (int i = 0; i < item.Length; i++)
        {
            m_nlMonster_Dictionary_Goaway_Reward_Item_Etc_Count.Add(item[i]);
        }
    }
    // 몬스터 도감 데이터 해금 비율 100% 설정 함수 - 
    public void MonsterDictionary_Add_100P_Goaway_Reward_Gold(int count = 0, int mingold = 0, int maxgold = 0, int rate = 0)
    {
        this.m_nMonster_Goaway_Reward_Gold_Count = count;
        this.m_nMonster_Goaway_Reward_Gold_Max = maxgold;
        this.m_nMonster_Goaway_Reward_Gold_Min = mingold;
        this.m_nMonster_Goaway_Reward_Gold_DropRate = rate;
    }
    // 몬스터 도감 데이터 해금 비율 100% 설정 함수 - 
    public void MonsterDictionary_Add_100P_SS_Goaway(STATUS rewardstatus, SOC rewardsoc)
    {
        this.m_SMonster_Goaway_Reward_STATUS = rewardstatus;
        this.m_SMonster_Goaway_Reward_SOC = rewardsoc;
    }
}
