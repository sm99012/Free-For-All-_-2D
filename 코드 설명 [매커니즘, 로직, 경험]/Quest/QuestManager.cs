    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private static QuestManager instance = null;

    public static int m_snQuest_ProcessOrder;
    public static int m_snQuest_CompleteOrder;

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
    public static QuestManager Instance
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

    public Dictionary <int, Quest_KILL_MONSTER> m_Dictionary_QuestList_KILL_MONSTER;
    public Dictionary <int, Quest_KILL_TYPE> m_Dictionary_QuestList_KILL_TYPE;
    public Dictionary <int, Quest_GOAWAY_MONSTER> m_Dictionary_QuestList_GOAWAY_MONSTER;
    public Dictionary <int, Quest_GOAWAY_TYPE> m_Dictionary_QuestList_GOAWAY_TYPE;
    public Dictionary <int, Quest_COLLECT> m_Dictionary_QuestList_COLLECT;
    public Dictionary <int, Quest_CONVERSATION> m_Dictionary_QuestList_CONVERSATION;
    public Dictionary <int, Quest_ROLL> m_Dictionary_QuestList_ROLL;
    public Dictionary <int, Quest_ELIMINATE_MONSTER> m_Dictionary_QuestList_ELIMINATE_MONSTER;
    public Dictionary <int, Quest_ELIMINATE_TYPE> m_Dictionary_QuestList_ELIMINATE_TYPE;

    public bool InitialSet()
    {
        m_Dictionary_QuestList_KILL_MONSTER = new Dictionary<int, Quest_KILL_MONSTER>();
        m_Dictionary_QuestList_KILL_TYPE = new Dictionary<int, Quest_KILL_TYPE>();
        m_Dictionary_QuestList_GOAWAY_MONSTER = new Dictionary<int, Quest_GOAWAY_MONSTER>();
        m_Dictionary_QuestList_GOAWAY_TYPE = new Dictionary<int, Quest_GOAWAY_TYPE>();
        m_Dictionary_QuestList_COLLECT = new Dictionary<int, Quest_COLLECT>();
        m_Dictionary_QuestList_CONVERSATION = new Dictionary<int, Quest_CONVERSATION>();
        m_Dictionary_QuestList_ROLL = new Dictionary<int, Quest_ROLL>();
        m_Dictionary_QuestList_ELIMINATE_MONSTER = new Dictionary<int, Quest_ELIMINATE_MONSTER>();
        m_Dictionary_QuestList_ELIMINATE_TYPE = new Dictionary<int, Quest_ELIMINATE_TYPE>();

        Quest_KILL_MONSTER Quest1_KILL_MONSTER1 = new Quest_KILL_MONSTER("[퀘스트[S1]: 튜토리얼 퀘스트]\n[약초밭을 위한 '수풀' 제거]", 0000, E_QUEST_LEVEL.S1, 1, 1, 1);
        Quest_ROLL Quest2_ROLL1 = new Quest_ROLL("[퀘스트[S1]: 튜토리얼 퀘스트]\n[굴러라 젊은이여]", 6000, E_QUEST_LEVEL.S1, 1, 1, 2);
        Quest_COLLECT Quest3_COLLECT1 = new Quest_COLLECT("[퀘스트[S2]: 튜토리얼 퀘스트]\n['100년 묵은 도라지' 원정대]", 4000, E_QUEST_LEVEL.S2, 1, 1, 4);
        Quest_GOAWAY_MONSTER Quest4_GOAWAY_MONSTER1 = new Quest_GOAWAY_MONSTER("[퀘스트[S1]: 튜토리얼 퀘스트]\n[집으로]", 2000, E_QUEST_LEVEL.S1, 1, 1, 6);
        Quest_CONVERSATION Quest24_CONVERSATION6 = new Quest_CONVERSATION("[퀘스트[S1]: 스토리 퀘스트]\n['드넓은 초원' 으로..]", 5005, E_QUEST_LEVEL.S1, 1, 1, 7);
        Quest_COLLECT Quest5_COLLECT2 = new Quest_COLLECT("[퀘스트[S3]: 히든 퀘스트]\n[으르르르르르!! 왈!]", 4001, E_QUEST_LEVEL.S3, 2, 2, 3);
        Quest_CONVERSATION Quest6_CONVERSATION1 = new Quest_CONVERSATION("[퀘스트[S1]: 스토리 퀘스트]\n[...]", 5000, E_QUEST_LEVEL.S1, 3, 3, 5);

        Quest_COLLECT Quest7_COLLECT3 = new Quest_COLLECT("[퀘스트[S1]: 서브 퀘스트]\n[배고프다..]", 4002, E_QUEST_LEVEL.S1, 6, 6, 25);
        Quest_ROLL Quest8_ROLL2 = new Quest_ROLL("[퀘스트[S1]: 서브 퀘스트]\n['훈련대장 초원 슬라임' 의 훈련 1]", 6001, E_QUEST_LEVEL.S1, 5, 5, 26);
        Quest_KILL_MONSTER Quest9_KILL_MONSTER2 = new Quest_KILL_MONSTER("[퀘스트[S3]: 서브 퀘스트]\n['훈련대장 초원 슬라임' 의 훈련 2]", 0001, E_QUEST_LEVEL.S3, 5, 5, 27);
        Quest_KILL_MONSTER Quest10_KILL_MONSTER3 = new Quest_KILL_MONSTER("[퀘스트[S2]: 서브 퀘스트]\n['훈련대장 초원 슬라임' 의 훈련 3]", 0002, E_QUEST_LEVEL.S2, 5, 5, 28);
        Quest_CONVERSATION Quest11_CONVERSATION2 = new Quest_CONVERSATION("[퀘스트[S1]: 서브 퀘스트]\n[짜증나는 말투 ㅋㅋ]", 5001, E_QUEST_LEVEL.S1, 7, 8, 20);
        Quest_CONVERSATION Quest12_CONVERSATION3 = new Quest_CONVERSATION("[퀘스트[S2]: 서브 퀘스트]\n[화나는 말투 ㅋ]", 5002, E_QUEST_LEVEL.S2, 8, 7, 21);
        Quest_CONVERSATION Quest13_CONVERSATION4 = new Quest_CONVERSATION("[퀘스트[S3]: 서브 퀘스트]\n[화병]", 5003, E_QUEST_LEVEL.S3, 7, 8, 22);
        Quest_GOAWAY_MONSTER Quest14_GOAWAY_MONSTER2 = new Quest_GOAWAY_MONSTER("[퀘스트[S4]: 히든 퀘스트]\n[화를 다스리는 법]", 2001, E_QUEST_LEVEL.S4, 9, 9, 23);
        Quest_CONVERSATION Quest15_CONVERSATION5 = new Quest_CONVERSATION("[퀘스트[S4]: 히든 퀘스트]\n[화내지않고 살아가기]", 5004, E_QUEST_LEVEL.S4, 9, 9, 24);
        Quest_GOAWAY_TYPE Quest16_GOAWAY_TYPE1 = new Quest_GOAWAY_TYPE("[퀘스트[S2]: 스토리 퀘스트]\n[우릴 좀 내버려둬]", 3000, E_QUEST_LEVEL.S2, 10, 10, 38);
        Quest_KILL_TYPE Quest17_KILL_TYPE1 = new Quest_KILL_TYPE("[퀘스트[S2]: 스토리 퀘스트]\n[잘못을 했으면 벌을 받는게 세상의 이치]", 1000, E_QUEST_LEVEL.S2, 10, 10, 39);
        Quest_COLLECT Quest18_COLLECT4 = new Quest_COLLECT("[퀘스트[S2]: 스토리 퀘스트]\n['골드 타임 슬라임' 의 의뢰 1]", 4003, E_QUEST_LEVEL.S2, 11, 11, 15);
        Quest_COLLECT Quest19_COLLECT5 = new Quest_COLLECT("[퀘스트[S3]: 스토리 퀘스트]\n['골드 타임 슬라임' 의 의뢰 2]", 4004, E_QUEST_LEVEL.S3, 11, 11, 16);
        Quest_KILL_MONSTER Quest20_KILL_MONSTER4 = new Quest_KILL_MONSTER("[퀘스트[S3]: 히든 퀘스트]\n[은밀한 의뢰]", 0003, E_QUEST_LEVEL.S3, 11, 11, 19);
        Quest_KILL_MONSTER Quest21_KILL_MONSTER5 = new Quest_KILL_MONSTER("[퀘스트[S3]: 히든 퀘스트]\n[은밀한 의뢰]", 0004, E_QUEST_LEVEL.S3, 11, 11, 18);
        Quest_KILL_MONSTER Quest22_KILL_MONSTER6 = new Quest_KILL_MONSTER("[퀘스트[S3]: 스토리 퀘스트]\n[무력시위]", 0005, E_QUEST_LEVEL.S3, 12, 12, 17);
        Quest_COLLECT Quest23_COLLECT6 = new Quest_COLLECT("[퀘스트[S3]: 히든 퀘스트]\n['작은 바위' 의 꿈]", 4005, E_QUEST_LEVEL.S3, 13, 13, 13);
        Quest_CONVERSATION Quest25_CONVERSATION7 = new Quest_CONVERSATION("[퀘스트[S1]: 히든 퀘스트]\n['작은 바위' 는 잘 있을까요?]", 5006, E_QUEST_LEVEL.S1, 14, 13, 14);
        Quest_CONVERSATION Quest26_CONVERSATION8 = new Quest_CONVERSATION("[퀘스트[S2]: 서브 퀘스트]\n[마지막 인사]", 5007, E_QUEST_LEVEL.S2, 15, 16, 30);
        Quest_COLLECT Quest27_COLLECT7 = new Quest_COLLECT("[퀘스트[S3]: 서브 퀘스트]\n[마지막 수리]", 4006, E_QUEST_LEVEL.S3, 16, 16, 31);
        Quest_KILL_MONSTER Quest28_KILL_MONSTER7 = new Quest_KILL_MONSTER("[퀘스트[S1]: 서브 퀘스트]\n['슬레나르' 와 함께 1]", 0006, E_QUEST_LEVEL.S1, 16, 16, 32);
        Quest_CONVERSATION Quest29_CONVERSATION9 = new Quest_CONVERSATION("[퀘스트[S1]: 서브 퀘스트]\n['슬레나르' 와 함께 2]", 5008, E_QUEST_LEVEL.S1, 16, 16, 33);
        Quest_COLLECT Quest30_COLLECT8 = new Quest_COLLECT("[퀘스트[S1]: 서브 퀘스트]\n[신속 정확한 배달]", 4007, E_QUEST_LEVEL.S1, 16, 11, 34);
        Quest_CONVERSATION Quest31_CONVERSATION10 = new Quest_CONVERSATION("[퀘스트[S3]: 히든퀘스트]\n[전설의 도라지를 찾아서 1]", 5009, E_QUEST_LEVEL.S3, 20, 1, 43);
        Quest_COLLECT Quest32_COLLECT9 = new Quest_COLLECT("[퀘스트[S3]: 히든 퀘스트]\n[전설의 도라지를 찾아서 2]", 4008, E_QUEST_LEVEL.S2, 1, 20, 44);
        Quest_CONVERSATION Quest33_CONVERSATION11 = new Quest_CONVERSATION("[퀘스트[S1]: 서브 퀘스트]\n[대신 장좀 봐돌라 안카나?!]", 5010, E_QUEST_LEVEL.S1, 20, 20, 40);
        Quest_COLLECT Quest34_COLLECT10 = new Quest_COLLECT("[퀘스트[S2]: 서브 퀘스트]\n[장보기]", 4009, E_QUEST_LEVEL.S2, 20, 20, 41);
        Quest_COLLECT Quest35_COLLECT11 = new Quest_COLLECT("[퀘스트[S2]: 서브 퀘스트]\n[소울 푸드]", 4010, E_QUEST_LEVEL.S2, 20, 19, 42);
        Quest_KILL_MONSTER Quest36_KILL_MONSTER8 = new Quest_KILL_MONSTER("[퀘스트[S3]: 히든 퀘스트]\n[가정식]", 0007, E_QUEST_LEVEL.S3, 20, 20, 45);
        Quest_COLLECT Quest37_COLLECT12 = new Quest_COLLECT("[퀘스트[S2]: 서브 퀘스트]\n[내 도끼 어디갔어!!]", 4011, E_QUEST_LEVEL.S2, 17, 17, 35);
        Quest_GOAWAY_TYPE Quest38_GOAWAY_TYPE2 = new Quest_GOAWAY_TYPE("[퀘스트[S2]: 서브 퀘스트]\n[이예~이~~~~우....]", 3001, E_QUEST_LEVEL.S2, 18, 18, 36);
        Quest_COLLECT Quest39_COLLECT13 = new Quest_COLLECT("[퀘스트[S3]: 서브 퀘스트]\n[으으으...]", 4012, E_QUEST_LEVEL.S3, 18, 18, 37);
        Quest_KILL_TYPE Quest40_KILL_TYPE2 = new Quest_KILL_TYPE("[퀘스트[S1]: 스토리 퀘스트]\n[초원의 평화를 위하여 1]", 1001, E_QUEST_LEVEL.S1, 29, 29, 8);
        Quest_KILL_MONSTER Quest41_KILL_MONSTER9 = new Quest_KILL_MONSTER("[퀘스트[S2]: 스토리 퀘스트]\n[초원의 평화를 위하여 2]", 0008, E_QUEST_LEVEL.S2, 29, 29, 9);
        Quest_GOAWAY_TYPE Quest42_GOAWAY_TYPE3 = new Quest_GOAWAY_TYPE("[퀘스트[S2]: 스토리 퀘스트]\n[초원의 진정한 평화를 위하여]", 3002, E_QUEST_LEVEL.S2, 29, 29, 10);
        Quest_COLLECT Quest43_COLLECT14 = new Quest_COLLECT("[퀘스트[S3]: 스토리 퀘스트]\n['드넓은 초원' 의 따스한 햇살]", 4013, E_QUEST_LEVEL.S3, 30, 30, 11);
        Quest_KILL_MONSTER Quest44_KILL_MONSTER10 = new Quest_KILL_MONSTER("[퀘스트[S2]: 스토리 퀘스트]\n['꼬마 앤트' 를 위해]", 0009, E_QUEST_LEVEL.S2, 29, 29, 12);
        Quest_COLLECT Quest45_COLLECT15 = new Quest_COLLECT("[퀘스트[S3]: 스토리 퀘스트]\n[재생 특효약 '알보찰']", 4014, E_QUEST_LEVEL.S3, 11, 11, 46);
        Quest_CONVERSATION Quest46_CONVERSATION12 = new Quest_CONVERSATION("[퀘스트[S1]: 서브 퀘스트]\n[장비 강화]", 5011, E_QUEST_LEVEL.S1, 22, 22, 29);
        Quest_CONVERSATION Quest47_CONVERSATION13 = new Quest_CONVERSATION("[퀘스트[S4]: 스토리 퀘스트]\n[선택]", 5012, E_QUEST_LEVEL.S4, 28, 28, 47);
        Quest_CONVERSATION Quest48_CONVERSATION14 = new Quest_CONVERSATION("[퀘스트[S4]: 스토리 퀘스트]\n[선택[자신과 '주식회사 더 슬라' 의 이권을 위해]]", 5013, E_QUEST_LEVEL.S4, 11, 11, 50);
        Quest_CONVERSATION Quest49_CONVERSATION15 = new Quest_CONVERSATION("[퀘스트[S4]: 스토리 퀘스트]\n[선택['드넓은 초원' 의 평화를 위해]]", 5014, E_QUEST_LEVEL.S4, 29, 29, 48);
        Quest_KILL_MONSTER Quest50_KILL_MONSTER11 = new Quest_KILL_MONSTER("[퀘스트[S5]: 스토리 퀘스트]\n[검증]", 0010, E_QUEST_LEVEL.S5, 27, 27, 49);
        Quest_CONVERSATION Quest51_CONVERSATION16 = new Quest_CONVERSATION("[퀘스트[S4]: 스토리 퀘스트]\n[협상]", 5015, E_QUEST_LEVEL.S4, 11, 27, 51);
        Quest_KILL_MONSTER Quest52_KILL_MONSTER12 = new Quest_KILL_MONSTER("[퀘스트[S5]: 스토리 퀘스트]\n[검증]", 0011, E_QUEST_LEVEL.S5, 27, 27, 52);


        // [퀘스트[S1]: 튜토리얼]\n[약초밭을 위한 '수풀' 제거]
        {
            Quest1_KILL_MONSTER1.AddQuestDescription_Context("...!");
            Quest1_KILL_MONSTER1.AddQuestDescription_Context("어서오게나 젊은이..\n참 오랜만에 보는 사람이구먼~");
            Quest1_KILL_MONSTER1.AddQuestDescription_Context("나는 '늙고 병든 슬라임' 이라고 하네~\n뭐 다들 이렇게 부르더군..");
            Quest1_KILL_MONSTER1.AddQuestDescription_Context("나는 약초를 키우며 살아가고있지..\n그런데말이야........");
            Quest1_KILL_MONSTER1.AddQuestDescription_Context("어제 그만 허리를 삐끗 해버렸지 뭔가.. 끌끌끌...\n허리가 아파서 '수풀' 조차 제거하기가 쉽지않네그려~..\n약초를 기르는데 가장 중요한것은 '수풀' 같은 잡초 제거인데 말이야...");
            Quest1_KILL_MONSTER1.AddQuestDescription_Context("내 자네에게 '수풀' 제거를 좀 부탁해도 되겠나?~\n이 늙은이 좀 도와주게~\n\n[A 키를 눌러 '기본 공격' 을 할 수 있습니다.]\n['기본 공격' 을 통해 '수풀' 을 5개 제거해 보세요.]\n['수풀' 은 맵 곳곳에 놓여있는 불그스름한 무언가입니다.]");
            Quest1_KILL_MONSTER1.AddQuestOk_Context("고맙네 젊은이... 끌끌끌... 잘 부탁하네^~^\n참고로 '수풀' 에는 가시가 있다네.. 부디 조심하기를..");
            Quest1_KILL_MONSTER1.AddQuestNo_Context("라떼는말이야...-_- 으이?!?!-_- 늙은이가 부탁하면...-_- 으이?!?!-_-\n...\n...\n...-_- 초코라떼가 최고라네...&_&(쭈굴..)");
            Quest1_KILL_MONSTER1.AddQuestClear_Context("이야~ 정말 고맙네그려~~\n덕분에 약초가 잘~ 자랄 수 있겠구먼~~^~^");
            Quest1_KILL_MONSTER1.AddQuestProgress_Context("잉? 아직 '수풀' 이 좀 많은거같은디..?\n'수풀' 은 색이 불그스름한 무언가라네^^");
            Quest1_KILL_MONSTER1.m_sQuest_Information_Recommend = ("내 부탁좀 들어주게 젊은이..\n허리를 삐끗한 나를 대신하여 약초밭 관리를 도와주게나..\n끌끌끌...");
            Quest1_KILL_MONSTER1.m_sQuest_Information_Process = ("내 부탁좀 들어주게 젊은이..\n허리를 삐끗한 나를 대신하여 약초밭 관리를 좀 해주게나..\n끌끌끌...");
            Quest1_KILL_MONSTER1.m_sQuest_Information_Condition = ("'늙고 병든 슬라임' 의 부탁을 받고 '수풀' 을 5개 제거했습니다.\n이제 다시 '늙고 병든 슬라임' 에게 가봅시다.");
            Quest1_KILL_MONSTER1.m_sQuest_Information_Clear = ("'늙고 병든 슬라임' 의 첫번째 부탁을 들어주었습니다.\n허리가 아픈 '늙고 병든 슬라임' 을 대신해 약초밭 근처 곳곳의 '수풀' 을 5개 제거했습니다.");
            Quest1_KILL_MONSTER1.AddCondition(0008, 5, 0); // 5, 0
            Quest1_KILL_MONSTER1.m_sRewardSOC = new SOC(1, 0, 0, 1, 0, 0, 0, 0, 0);
            Quest1_KILL_MONSTER1.m_sRewardSTATUS = new STATUS(0, 0, 10); // 10
            //Quest1_KILL_MONSTER1.AddQuestClearReward_Item(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1000]);
            Quest1_KILL_MONSTER1.m_nRewardGold = 500;
            m_Dictionary_QuestList_KILL_MONSTER.Add(Quest1_KILL_MONSTER1.m_nQuest_Code, Quest1_KILL_MONSTER1);
        }

        // [퀘스트[S1]: 튜토리얼]\n[굴러라 젊은이여]
        {
            Quest2_ROLL1.AddQuestDescription_Context("젊은이 내 염치없지만 하나 더 부탁해도 되겠나..?\n좀처럼 허리가 나아지질 않는구먼...ㅠㅠ");
            Quest2_ROLL1.AddQuestDescription_Context("이 늙은이의 허리를 낫게하기 위해서는 '100년 묵은 도라지' 를 구해야 하네..\n달여 먹으면 늙은이 관절에 그거만큼 좋은게 없거든~~");
            Quest2_ROLL1.AddQuestDescription_Context("하지만 '100년 묵은 도라지' 는 구하기가 힘들다네..\n'빛 조차 들지않는 깊은숲' 어딘가에 있다네..\n꽤나 험한 곳이지...");
            Quest2_ROLL1.AddQuestDescription_Context("그래서 사전 준비가 필요하네!!!\n");
            Quest2_ROLL1.AddQuestDescription_Context("어떤가? '100년 묵은 도라지' 원정을 위해 사전 준비를 해볼텐가?\n어려운건 아니고 좀만 구르면 된다네~\n\n[S 키를 눌러 '구르기' 를 할 수 있습니다.]\n['구르기' 를 5번 해보세요.]\n['구르기' 시전 중 플레이어는 조금 더 빨리 움직입니다.]\n['구르기' 시전 중 플레이어는 그 어떤것에도 방해받지 않습니다.(무적)]");
            Quest2_ROLL1.AddQuestOk_Context("다~~~ 자네를 위하고~~~ 이 늙은이를 위한 일이네~~~\n일석이조가 아닌가~~^~^");
            Quest2_ROLL1.AddQuestNo_Context("라떼는말이야...-_- 으이?!?!-_- 늙은이가 부탁하면 으이?!?!-_-\n...\n...\n...-_- 고구마라떼도 괜찮다네...&_&(쭈굴..)\n아이고 허리야..");
            Quest2_ROLL1.AddQuestClear_Context("끌끌끌... 자네 생각보다 유능하구만~~\n덕분에 빨리 나을 수 있겠구먼~~^~^");
            Quest2_ROLL1.AddQuestProgress_Context("잉? 아직 덜 구른것 같은데??\n좀 더 굴러보게나~");
            Quest2_ROLL1.m_sQuest_Information_Recommend = ("자네덕에 약초밭을 지켜내었어!\n하지만 내 허리는 여전히 엉망진창이구먼.. 한번만 더 도와줄 수 있겠는가?");
            Quest2_ROLL1.m_sQuest_Information_Process = ("철저한 준비를 해보자고 젊은이..\n'100년 묵은 도라지' 원정길은 꽤나 험하지...\n따라서 사전준비로 '구르기' 를 익혀보게나..\n끌끌끌...");
            Quest2_ROLL1.m_sQuest_Information_Condition = ("'늙고 병든 슬라임' 의 두번째 부탁을 받고 '구르기' 5회를 성곡적으로 수행했습니다.\n이제 다시 '늙고 병든 슬라임' 에게 가봅시다.");
            Quest2_ROLL1.m_sQuest_Information_Clear = ("'늙고 병든 슬라임' 의 두번째 부탁을 들어주었습니다.\n'100년 묵은 도라지' 원정을 위한 사전준비로 맵 곳곳을 굴러다녔습니다.");
            Quest2_ROLL1.SetCondition(5, 0); // 5
            Quest2_ROLL1.m_ql_Quest_Necessity_Clear.Add(Quest1_KILL_MONSTER1);
            Quest2_ROLL1.m_sRewardSOC = new SOC(1, 0, 0, 1, 0, 0, 0, 0, 0);
            Quest2_ROLL1.m_sRewardSTATUS = new STATUS(0, 0, 12);
            Quest2_ROLL1.m_nRewardGold = 500;
            m_Dictionary_QuestList_ROLL.Add(Quest2_ROLL1.m_nQuest_Code, Quest2_ROLL1);
        }

        // [퀘스트[S2]: 튜토리얼]\n['100년 묵은 도라지' 원정대]
        {
            Quest3_COLLECT1.AddQuestDescription_Context("자!! 준비는 끝난거 같군..!");
            Quest3_COLLECT1.AddQuestDescription_Context("본격적으로 '100년 묵은 도라지' 원정을 떠나보자고!!!");
            Quest3_COLLECT1.AddQuestDescription_Context("'빛조차 들지않는 깊은숲 1', '빛조차 들지않는 깊은숲 2' 어딘가에 분명 있을걸세..!!");
            Quest3_COLLECT1.AddQuestDescription_Context("사실.....!\n아닐세 다녀올랑가?\n'100년 묵은 도라지' 만 가져와 준다면 쓸만한 물건도 여럿 판매해주겠네. ㅎㅎ\n\n[SPACE 키를 눌러 '채집' 을 할 수 있습니다.]\n[반짝이는 채집물 근처에서 '채집' 을 해보세요.]\n['채집' 을 통해 '100년 묵은 도라지' 를 포함한 각종 아이템을 획득할 수 있습니다.]");
            Quest3_COLLECT1.AddQuestOk_Context("고마우이~~..\n기다리고 있겠네~~^~^\n상당히 험한곳이니 조심히 다녀오게나.\n끌끌끌...");
            Quest3_COLLECT1.AddQuestNo_Context("라떼는말이야...-_- 에휴..?!?!-_-\n...\n...\n...... %_%(쭈굴..)\n아이고 허리야..");
            Quest3_COLLECT1.AddQuestClear_Context("고맙네.. 끌끌..\n이걸로 이 늙은이의 허리는...... 세계 최강이될걸세...!!!!!!!!!!\n사실 '100년 묵은 도라지' 는 젊은시절 내가 심어놨다네..ㅎ^^. 그동안 까먹었고 있었다네^^...ㄱ-\n\n<color=red>[''늙고 병든 슬라임' 과의 거래' 가 가능해집니다.]</color>");
            Quest3_COLLECT1.AddQuestProgress_Context("잉? '100년 묵은 도라지' 는 아직인가..?");
            Quest3_COLLECT1.m_sQuest_Information_Recommend = ("드디어 결전의 날이 밝았구먼..\n이 지긋지긋한 관절병도 이젠 안녕일세!\n부탁하네!!");
            Quest3_COLLECT1.m_sQuest_Information_Process = ("드디어 결전의 날일세..\n'채집' 을 통해 '100년 묵은 도라지' 를 구해오게나!..");
            Quest3_COLLECT1.m_sQuest_Information_Condition = ("'늙고 병든 슬라임' 의 세번째 부탁을 받고 '채집' 을 통해 드디어 '100년 묵은 도라지' 를 1개 획득했습니다.\n이제 다시 '늙고 병든 슬라임' 에게 가봅시다.\n과연 '늙고 병든 슬라임' 의 허리는 나아질까요?");
            Quest3_COLLECT1.m_sQuest_Information_Clear = ("'늙고 병든 슬라임' 의 세번째 부탁을 들어주었습니다.\n'100년 묵은 도라지' 원정에 성공했습니다.\n'늙고 병든 슬라임' 의 허리는 자칭 최강이 되었다고 하네요.");
            Quest3_COLLECT1.AddCondition(0016, 1, 0); // 1, 0
            Quest3_COLLECT1.m_ql_Quest_Necessity_Clear.Add(Quest2_ROLL1);
            Quest3_COLLECT1.m_sRewardSOC = new SOC(1, 0, 0, 2, 0, 0, 0, 0, 0);
            Quest3_COLLECT1.m_sRewardSTATUS = new STATUS(0, 0, 14);
            Quest3_COLLECT1.m_nRewardGold = 500;
            m_Dictionary_QuestList_COLLECT.Add(Quest3_COLLECT1.m_nQuest_Code, Quest3_COLLECT1);
        }

        // [퀘스트[S1]: 튜토리얼]\n[집으로]
        {
            Quest4_GOAWAY_MONSTER1.AddQuestDescription_Context("오오... 자네 왔나?\n덕분에 허리가 많이 좋아졌다네~");
            Quest4_GOAWAY_MONSTER1.AddQuestDescription_Context("다름이 아니라 이 '깊디깊은숲' 에 언제부터인가 길잃은 '꼬마 초원 슬라임' 이 많이 보이기 시작했네..\n분명 자네도 본적이 있을걸세..");
            Quest4_GOAWAY_MONSTER1.AddQuestDescription_Context("'드넓은 초원' 에서 들어온 녀석들인것 같구먼.. 끌끌끌...");
            Quest4_GOAWAY_MONSTER1.AddQuestDescription_Context("'꼬마 초원 슬라임' 들은 아직 어리다네..\n그렇기에 이 '깊디깊은숲' 은 너무 위험하다네..!");
            Quest4_GOAWAY_MONSTER1.AddQuestDescription_Context("그들을 다시 돌려보내주게!! 부탁해도 되겠는가..? 나는 따로 알아볼것이 있다네.\n\n[D 키를 눌러 '놓아주기' 를 할 수 있습니다.]\n['놓아주기' 를 통해 '꼬마 초원 슬라임' 3마리를 집으로 돌려보내 주세요.]");
            Quest4_GOAWAY_MONSTER1.AddQuestOk_Context("고맙네!! 묘한 동질감이 드는구먼...\n자네라면 분명 할 수 있을걸세~");
            Quest4_GOAWAY_MONSTER1.AddQuestNo_Context("지금 이 순간에도 길잃은 '꼬마 초원 슬라임' 들은 분명 가족을 그리워하고 무서워할걸세..!!!!!");
            Quest4_GOAWAY_MONSTER1.AddQuestClear_Context("길잃은 '꼬마 초원 슬라임' 들을 집으로 잘 돌려보내 주었군^^");
            Quest4_GOAWAY_MONSTER1.AddQuestProgress_Context("혼자가 되는 그 기분... 자네가 아는가..?\n어서 빨리 길잃은 '꼬마 초원 슬라임' 들을 집으로 돌려보내 주게나!");
            Quest4_GOAWAY_MONSTER1.m_sQuest_Information_Recommend = ("가여운것들... 하루빨리 이녀석들이 집으로 돌아가야 할텐데...");
            Quest4_GOAWAY_MONSTER1.m_sQuest_Information_Process = ("어서 빨리 '놓아주기' 를 통해 길잃은 '꼬마 초원 슬라임' 들을 집으로 돌려보내 주게나..!");
            Quest4_GOAWAY_MONSTER1.m_sQuest_Information_Condition = ("'늙고 병든 슬라임' 의 마지막 부탁을 받고 '놓아주기' 를 통해 길잃은 '꼬마 초원 슬라임' 3마리를 가족 곁으로 돌려보내 주었습니다.\n이제 다시 '늙고 병든 슬라임' 에게 가봅시다.");
            Quest4_GOAWAY_MONSTER1.m_sQuest_Information_Clear = ("'늙고 병든 슬라임' 의 마지막 부탁을 들어주었습니다.\n길잃은 '꼬마 초원 슬라임' 들을 가족 곁으로 돌려보내 주었습니다.\n이제 그들은 안전합니다. 그러나 한가지 의문점이 있습니다. '드넓은 초원' 에서 살아가는 '초원 슬라임' 들이 위험한 '깊디깊은숲' 까지 흘러들어온 이유는 뭘까요?");
            Quest4_GOAWAY_MONSTER1.AddCondition(2200, 3, 0); // 3
            Quest4_GOAWAY_MONSTER1.m_ql_Quest_Necessity_Clear.Add(Quest3_COLLECT1);
            Quest4_GOAWAY_MONSTER1.m_sRewardSOC = new SOC(1, 0, 0, 1, 0, 0, 0, 0, 0);
            Quest4_GOAWAY_MONSTER1.m_sRewardSTATUS = new STATUS(0, 0, 16);
            Quest4_GOAWAY_MONSTER1.m_nRewardGold = 500;
            m_Dictionary_QuestList_GOAWAY_MONSTER.Add(Quest4_GOAWAY_MONSTER1.m_nQuest_Code, Quest4_GOAWAY_MONSTER1);
        }

        // [퀘스트[S3]: 히든퀘스트]\n[으르르르르르!! 왈!]
        // '늙고 병든 슬라임' 의 애완동물 '감쟈' 를 위한 뼈다구 수집.
        {
            Quest5_COLLECT2.AddQuestDescription_Context("왈! 왈! 으르르를!!\n\n[??? ㅎ..해석할 수 없습니다.]");
            Quest5_COLLECT2.AddQuestDescription_Context("왈!왈....으르르!..\n왈!!\n\n[... 아마도 '감쟈' 는 단단한 장난감을 찾고있는것 같습니다.]");
            Quest5_COLLECT2.AddQuestOk_Context("왈!왈!^~^\n\n['감쟈' 가 좋아합니다.]");
            Quest5_COLLECT2.AddQuestNo_Context("으르르르르르르르르르르르!!!!!!\n\n['감쟈' 가 싫어합니다.]");
            Quest5_COLLECT2.AddQuestClear_Context("왈!^~^\n\n['단단한 뼈' 1개를 '감쟈' 에게 건네주었습니다.]\n[당신은 '감쟈' 를 위해 노력했습니다. 최대 체력이 3 증가합니다.]");
            Quest5_COLLECT2.AddQuestProgress_Context("으르르르르르르르!!!!\n\n[???]");
            Quest5_COLLECT2.m_sQuest_Information_Recommend = ("왈! 왈! 으르르를!!\n\n'감쟈' 가 무언가 원하고 있습니다.");
            Quest5_COLLECT2.m_sQuest_Information_Process = ("왈! 왈! 으르르를!!\n\n'감쟈' 가 무언가 원하고 있습니다.");
            Quest5_COLLECT2.m_sQuest_Information_Condition = ("오 '감쟈' 가 원하는것을 얻어냈군요!!\n빨리 '감쟈' 에게 가져다 줍시다!");
            Quest5_COLLECT2.m_sQuest_Information_Clear = ("왈!왈!^~^\n\n'감쟈' 에게 '단단한 뼈' 를 가져다 주었습니다. 매우 좋아하는것 같습니다.\n당신은 '감쟈' 의 마음에 드는 물건을 찾기 위해 '깊디깊은숲' 전체를 샅샅히 뒤졌습니다.\n때로는 걷고, 뛰며, 구르는 노력의 과정 중에 체력이 3만큼 좋아졌습니다.");
            Quest5_COLLECT2.m_bQuest_Information_Process_Hide = true; // true
            Quest5_COLLECT2.m_ql_Quest_Necessity_Clear.Add(Quest2_ROLL1);
            Quest5_COLLECT2.AddCondition(0013, 1, 0);
            Quest5_COLLECT2.m_sRewardSOC = new SOC(1, 0, 1, 1, 0, 0, 0, 0, 0);
            Quest5_COLLECT2.m_sRewardSTATUS = new STATUS(0, 0, 10, 3);
            Quest5_COLLECT2.m_nRewardGold = 0;
            m_Dictionary_QuestList_COLLECT.Add(Quest5_COLLECT2.m_nQuest_Code, Quest5_COLLECT2);
        }

        // [퀘스트[S1]: 스토리퀘스트]\n[...]
        {
            Quest6_CONVERSATION1.AddQuestDescription_Context("[관리 되지 않은 비석에는 이렇게 쓰여있다.]");
            Quest6_CONVERSATION1.AddQuestDescription_Context("['드넓은 초원' 의 영원한 벗.]");
            Quest6_CONVERSATION1.AddQuestDescription_Context("[평화의 상징.]");
            Quest6_CONVERSATION1.AddQuestDescription_Context("[......]");
            Quest6_CONVERSATION1.AddQuestDescription_Context("[누구보다 강인했으며]");
            Quest6_CONVERSATION1.AddQuestDescription_Context("[햇살보다 따스했으며]");
            Quest6_CONVERSATION1.AddQuestDescription_Context("[어떤 보석보다 빛났으며]");
            Quest6_CONVERSATION1.AddQuestDescription_Context("[모두의 친구였다.]");
            Quest6_CONVERSATION1.AddQuestDescription_Context("[......]");
            Quest6_CONVERSATION1.AddQuestDescription_Context("['XXX X라X'. 이곳에 잠들다.]\n\n[누군가 의도적으로 무덤을 훼손시켜 누구의 무덤인지 알 수 없습니다.]");
            Quest6_CONVERSATION1.AddQuestDescription_Context("[무덤에 예의를 갖추시겠습니까?]");
            Quest6_CONVERSATION1.AddQuestOk_Context("...");
            Quest6_CONVERSATION1.AddQuestNo_Context("...");
            Quest6_CONVERSATION1.AddQuestClear_Context("'xxx x라x'...");
            Quest6_CONVERSATION1.m_sQuest_Information_Recommend = ("누군가의 무덤.");
            Quest6_CONVERSATION1.m_sQuest_Information_Process = ("누군가의 무덤에 예의를 갖추어야 합니다.");
            Quest6_CONVERSATION1.m_sQuest_Information_Condition = ("당신은 '드넓은 초원' 의 위대한 누군가를 기리는 중입니다. . .\n잠시후 다시 '누군가의 무덤' 에 말을 걸어봅시다.");
            Quest6_CONVERSATION1.m_sQuest_Information_Clear = ("평화의 상징인 'xxx x라x' 의 무덤에 예의를 갖추었습니다.\n과연 'xxx x라x' 은 누굴까요?\n왜 그의 무덤은 의도적으로 훼손되었을까요?");
            Quest6_CONVERSATION1.m_bQuest_Information_Process_Hide = true;
            Quest6_CONVERSATION1.m_sRewardSOC = new SOC(1, 0, 0, 1, 0, 1, 0, 0, 0);
            Quest6_CONVERSATION1.m_sRewardSTATUS = new STATUS(0, 0, 16);
            Quest6_CONVERSATION1.m_nRewardGold = 0;
            m_Dictionary_QuestList_CONVERSATION.Add(Quest6_CONVERSATION1.m_nQuest_Code, Quest6_CONVERSATION1);
        }

        // [퀘스트[S1]: 서브퀘스트]\n[배고프다..]
        {
            Quest7_COLLECT3.AddQuestDescription_Context("충성!!!!\n아 아니구나...\n\n\n엇..!!!! 마침 잘됬다!!\n(꼬로록)");
            Quest7_COLLECT3.AddQuestDescription_Context("저기..\n제가 경계근무중이라 움직일수가 없어요..\n그래서 그런데..\n(꼬로록)");
            Quest7_COLLECT3.AddQuestDescription_Context("혹시 '맛좋은 거대 사과' 1개만 구해와줄 수 있나요????\n배가 너무 고파요....\n(꼬로록)\n제가 아무리 보급대장이지만 경계근무 중에 자리를 비울 수는 없어서요...");
            Quest7_COLLECT3.AddQuestOk_Context("오 고맙습니다.!!!!\n당신은 제 생명의 은인이에요..!!!\n'맛좋은 거대 사과'는 '큰 초원 슬라임', '짙은 앤트' 들이 드랍한다고 해요..!\n(꼬로록)");
            Quest7_COLLECT3.AddQuestNo_Context("...\n(꼬로록)");
            Quest7_COLLECT3.AddQuestProgress_Context("'맛좋은 거대 사과'.... 빨리.. 구해주세요..\n(꼬로록)\n현기증 난단 말이에요!!!(꼬로로로록)");
            Quest7_COLLECT3.AddQuestClear_Context("(아그작)\n(아그작)\n(아그아그작작)\n(꺼억~)\n감사합니다!!\n덕분에 경계근무를 제대로 설 수 있겠네요.!\n(꼬로록...)아........");
            Quest7_COLLECT3.m_sQuest_Information_Recommend = ("꼬르르륵..... 꽈롸락..");
            Quest7_COLLECT3.m_sQuest_Information_Process = ("충성! 배가 너무 고픕니다... 힘빠져요..\n'맛 좋은 거대 사과' 를 1개 만 부탁드려도 될까요..?");
            Quest7_COLLECT3.m_sQuest_Information_Condition = ("배고픈 '보급대장 초원 슬라임' 을 위해 '맛 좋은 거대 사과' 1개를 구했습니다.\n'보급대장 초원 슬라임' 이 배고픔을 참지 못하고 보급품에 손대기 전에 빨리 가져다 줍시다.");
            Quest7_COLLECT3.m_sQuest_Information_Clear = ("감사합니다! 충성!\n경계근무는 항상 고되답니다.ㅎㅎ\n당신은 경계근무 중인 '보급대장 초원 슬라임' 에게 '맛좋은 거대 사과' 를 1개 건네주었습니다. 굉장히 좋아하는군요. 아주 세상을 다가진것 같습니다.");
            Quest7_COLLECT3.AddCondition(8009, 1, 0);
            Quest7_COLLECT3.m_sRewardSOC = new SOC(1, 0, 0, 1, 0, -1, 0, 0, 0);
            Quest7_COLLECT3.m_sRewardSTATUS = new STATUS(0, 0, 20);
            Quest7_COLLECT3.m_nRewardGold = 50;
            m_Dictionary_QuestList_COLLECT.Add(Quest7_COLLECT3.m_nQuest_Code, Quest7_COLLECT3);
        }

        // [퀘스트[S1]: 서브퀘스트]\n['훈련대장 초원 슬라임' 의 훈련 1]
        {
            Quest8_ROLL2.AddQuestDescription_Context("이봐 애송이..\n바쁜거 안보이나? 가라.");
            Quest8_ROLL2.AddQuestDescription_Context("뭐냐...\n혹시 내 훈련법에 관심있는건가?");
            Quest8_ROLL2.AddQuestDescription_Context("내 훈련법은 알고싶어도 알려줄 수 없다.\n거기엔 여러 이유가 있다.");
            Quest8_ROLL2.AddQuestDescription_Context("뭐? 그래도 알고싶다고?");
            Quest8_ROLL2.AddQuestDescription_Context("정말 꼭 알려달라고?");
            Quest8_ROLL2.AddQuestDescription_Context("...");
            Quest8_ROLL2.AddQuestDescription_Context("...\n그렇다면.. 우선 '구르기' 10회다.!\n'구르기' 는 신체 밸런스를 잡아주는 훌륭한 운동법이지.\n우리 슬라임들은 하루에도 몇번씩 굴러다닌다.");
            Quest8_ROLL2.AddQuestDescription_Context("가라.");
            Quest8_ROLL2.AddQuestOk_Context("두고 보겠다.");
            Quest8_ROLL2.AddQuestNo_Context("훗. 겨우 '구르기' 10회 조차 못하는거냐. 인간이란 참으로 나약하다.");
            Quest8_ROLL2.AddQuestClear_Context("'구르기' 10번 한걸로 우쭐대지마라. 다음 훈련으로 넘어가지.\n\n[당신은 당신도 모르게 구르며 체력이 좋아졌습니다. 최대 체력이 1 증가합니다.]");
            Quest8_ROLL2.AddQuestProgress_Context("...");
            Quest8_ROLL2.m_sQuest_Information_Recommend = ("이 세계 전역을 굴러 다니는 슬라임들의 특별한 훈련법이 있다고 하는데..?");
            Quest8_ROLL2.m_sQuest_Information_Process = ("'훈련대장 초원 슬라임' 의 첫번째 훈련법 대로 '구르기' 를 10회 해야합니다.\n이 세계 전역을 굴러 다니는 슬라임들의 특별 훈련법이라고 하는데 과연 의미가 있을까요?");
            Quest8_ROLL2.m_sQuest_Information_Condition = ("'훈련대장 초원 슬라임' 의 첫번째 훈련법 대로 '구르기' 10회를 성공적으로 수행했습니다.\n당장 느껴지지는 않지만 체력이 더 좋아진것같군요.\n이제 '훈련대장 초원 슬라임' 에게 가봅시다.");
            Quest8_ROLL2.m_sQuest_Information_Clear = ("'훈련대장 초원 슬라임' 의 첫번째 훈련을 완료했습니다.\n이 세계 전역을 굴러 다니는 슬라임들의 특별 훈련법인 '구르기' 를 10회 수행함으로써 당신은 체력이 1만큼 더 좋아졌습니다.");
            Quest8_ROLL2.SetCondition(10, 0); // 10
            Quest8_ROLL2.m_sStatus_Necessity_Down.SetSTATUS_LV(5);
            Quest8_ROLL2.m_sRewardSOC = new SOC(1, 0, 0, 1, 0, 0, 0, 0, 0);
            Quest8_ROLL2.m_sRewardSTATUS = new STATUS(0, 0, 12, 1);
            Quest8_ROLL2.m_nRewardGold = 0;
            m_Dictionary_QuestList_ROLL.Add(Quest8_ROLL2.m_nQuest_Code, Quest8_ROLL2);
        }

        // [퀘스트[S3]: 서브퀘스트]\n['훈련대장 초원 슬라임' 의 훈련 2]
        {
            Quest9_KILL_MONSTER2.AddQuestDescription_Context("자 다음 훈련이다. 바쁘니까 빠르게 설명하겠다.");
            Quest9_KILL_MONSTER2.AddQuestDescription_Context("이곳 '드넓은 초원' 에서 가장 단단한것이 뭐라 생각하나?");
            Quest9_KILL_MONSTER2.AddQuestDescription_Context("바로 '짙은 앤트' 다.\n오랜 세월을 살며 단단한 나무 가죽을 가지고 있지.");
            Quest9_KILL_MONSTER2.AddQuestDescription_Context("이쯤 말했으니 알것이다.\n다음 훈련은 '짙은 앤트' 3마리 사냥이다.\n'짙은 앤트' 의 단단한 나무 가죽을 과연 베어낼 수 있을까?");
            Quest9_KILL_MONSTER2.AddQuestDescription_Context("이 훈련은 우리 '주식회사 더 슬라' 의 훈련병들이 거치는 필수과정 중 하나지.\n(웬지 너라면 가능할것같군.)");
            Quest9_KILL_MONSTER2.AddQuestDescription_Context("가라.\n\n['짙은 앤트' 는 '드넓은 초원' 에서 살아가는 생명체 중 가장 단단한 생명체입니다. 조심하세요.]");
            Quest9_KILL_MONSTER2.AddQuestOk_Context("이번에도 두고 보겠다.");
            Quest9_KILL_MONSTER2.AddQuestNo_Context("훗. 어림도 없다.");
            Quest9_KILL_MONSTER2.AddQuestClear_Context("호오... 정말로 '짙은 앤트' 를 베어버리다니..\n\n[당신은 타격에 좀 더 능숙해 졌습니다. 데미지가 1 증가합니다.]");
            Quest9_KILL_MONSTER2.AddQuestProgress_Context("...");
            Quest9_KILL_MONSTER2.m_sQuest_Information_Recommend = ("'주식회사 더 슬라' 에 소속된 슬라임들의 특별한 훈련법이 있다고 하는데..?");
            Quest9_KILL_MONSTER2.m_sQuest_Information_Process = ("'훈련대장 초원 슬라임' 의 두번째 훈련법 대로 '짙은 앤트' 를 3마리 사냥해야합니다.\n'짙은 앤트' 는 '드넓은 초원' 에서 살아가는 생명체 중 가장 단단한 생명체입니다. 이건 좀 힘들것 같네요.");
            Quest9_KILL_MONSTER2.m_sQuest_Information_Condition = ("'훈련대장 초원 슬라임' 의 두번째 훈련법 대로 '짙은 앤트' 를 3마리 사냥했습니다.\n'짙은 앤트' 의 단단함을 베어냈습니다. 정말 대단하군요!\n이제 '훈련대장 초원 슬라임' 에게 가봅시다.");
            Quest9_KILL_MONSTER2.m_sQuest_Information_Clear = ("'훈련대장 초원 슬라임' 의 두번째 훈련을 완료했습니다.\n'드넓은 초원' 에서 살아가는 생명체 중 가장 단단한 생명체인 '짙은 앤트' 를 3마리 사냥함으로써 당신의 데미지가 1만큼 증가했습니다.");
            Quest9_KILL_MONSTER2.m_ql_Quest_Necessity_Clear.Add(Quest8_ROLL2);
            Quest9_KILL_MONSTER2.AddCondition(0003, 3, 0); // 5, 0
            Quest9_KILL_MONSTER2.m_sStatus_Necessity_Down.SetSTATUS_LV(5);
            Quest9_KILL_MONSTER2.m_sRewardSOC = new SOC(1, 0, 0, 1, 0, -1, 0, 0, 0);
            Quest9_KILL_MONSTER2.m_sRewardSTATUS = new STATUS(0, 0, 15, 0, 0, 0, 0, 1, 1);
            Quest9_KILL_MONSTER2.m_nRewardGold = 0;
            m_Dictionary_QuestList_KILL_MONSTER.Add(Quest9_KILL_MONSTER2.m_nQuest_Code, Quest9_KILL_MONSTER2);
        }

        // [퀘스트[S3]: 서브퀘스트]\n['훈련대장 초원 슬라임' 의 훈련 3]
        {
            Quest10_KILL_MONSTER3.AddQuestDescription_Context("자 마지막 훈련이다. 제법 쓸만해 졌군.");
            Quest10_KILL_MONSTER3.AddQuestDescription_Context("훈련에는 여러 방법이 있다.\n그러나 실전 경험을 쌓을 수 있는것에는 대련 만한게 없다.");
            Quest10_KILL_MONSTER3.AddQuestDescription_Context("마지막은 내가 훈련시켰던 '상인단 경비 초원 슬라임' 과의 대련이다.\n'상인단 경비 초원 슬라임' 10마리와 싸워 이겨라.\n'주식회사 더 슬라' 소속이라고, 나의 제자들이라고 봐주지 마라.\n대련은 대련일 뿐이다.");
            Quest10_KILL_MONSTER3.AddQuestDescription_Context("이긴다면 너의 강함은 증명될것이고, 진다면 우리 경비대의 위엄이 올라갈것이다.");
            Quest10_KILL_MONSTER3.AddQuestDescription_Context("이기든 지든 우리 '주식회사 더 슬라' 에 손해가 되지는 않는다. \n그렇기에 '일롱 더 슬라' 님께서 너의 훈련을 수락한것이다.");
            Quest10_KILL_MONSTER3.AddQuestDescription_Context("가라.");
            Quest10_KILL_MONSTER3.AddQuestOk_Context("패기롭군. 이번에도 두고 보겠다.");
            Quest10_KILL_MONSTER3.AddQuestNo_Context("훗. 아직 실력이 모자란건가.");
            Quest10_KILL_MONSTER3.AddQuestClear_Context("훌륭하군.\n'드넓은 초원' 에는 내가 신뢰하는 대장장이들이 많지.\n이걸 주도록 하겠다. 부디 잘 쓰도록.");
            Quest10_KILL_MONSTER3.AddQuestProgress_Context("...");
            Quest10_KILL_MONSTER3.m_sQuest_Information_Recommend = ("훈련중 가장 중요한것은 경험..?");
            Quest10_KILL_MONSTER3.m_sQuest_Information_Process = ("'훈련대장 초원 슬라임' 의 마지막 훈련법 대로 '상인단 경비 초원 슬라임' 10마리와 대련해 이겨야 합니다.");
            Quest10_KILL_MONSTER3.m_sQuest_Information_Condition = ("'훈련대장 초원 슬라임' 의 마지막 훈련법 대로 '상인단 경비 초원 슬라임' 10마리와 대련해 이겼습니다.\n이제 '훈련대장 초원 슬라임' 에게 가봅시다.");
            Quest10_KILL_MONSTER3.m_sQuest_Information_Clear = ("'훈련대장 초원 슬라임' 의 마지막 훈련을 완료했습니다.\n'상인단 경비 초원 슬라임' 10마리와 대련해 이겼습니다. 당신의 강함은 '주식회사 더 슬라' 를 통해 증명되었습니다.");
            Quest10_KILL_MONSTER3.m_ql_Quest_Necessity_Clear.Add(Quest9_KILL_MONSTER2);
            Quest10_KILL_MONSTER3.AddCondition(2003, 10, 0); // 10, 0
            Quest10_KILL_MONSTER3.m_sStatus_Necessity_Down.SetSTATUS_LV(5);
            Quest10_KILL_MONSTER3.m_sRewardSOC = new SOC(1, 0, 0, 1, 0, 0, 0, 0, 0);
            Quest10_KILL_MONSTER3.m_sRewardSTATUS = new STATUS(0, 0, 20);
            Quest10_KILL_MONSTER3.m_lRewardList_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12300]);
            Quest10_KILL_MONSTER3.m_lRewardList_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12301]);
            Quest10_KILL_MONSTER3.m_nRewardGold = 1000;
            m_Dictionary_QuestList_KILL_MONSTER.Add(Quest10_KILL_MONSTER3.m_nQuest_Code, Quest10_KILL_MONSTER3);
        }

        // [퀘스트[S1]: 서브퀘스트]\n[짜증나는 말투 ㅋㅋ]
        {
            Quest11_CONVERSATION2.AddQuestDescription_Context("야~~ㅋㅋ");
            Quest11_CONVERSATION2.AddQuestDescription_Context("뭐 할거없나~ 하고 이리저리 떠돌아다니는거같은데~~~ㅋㅋ");
            Quest11_CONVERSATION2.AddQuestDescription_Context("ㅋㅋ\nㅋㅋ");
            Quest11_CONVERSATION2.AddQuestDescription_Context("할거 만들어줄까?ㅋㅋ\n\n[당신은 '쪼개는 초원 슬라임' 의 말투 때문에 짜증이 나기 시작합니다.]");
            Quest11_CONVERSATION2.AddQuestDescription_Context("빌어봐ㅋㅋ\n싫음 말고ㅋㅋ\n\n[당신은 화가 나기 시작합니다.]");
            Quest11_CONVERSATION2.AddQuestDescription_Context("해볼래?ㅋㅋ\n뭐 간단한건데ㅋㅋ 내 형인 '싸가지없는 초원 슬라임' 을 찾아줘ㅋㅋ\n나랑 비슷하게 생겨서 쉬울걸ㅋㅋ\n\n[여기까지 들은 이상 당신은 퀘스트를 수락해야할것같습니다.]");
            Quest11_CONVERSATION2.AddQuestOk_Context("그럴줄 알았지ㅋㅋ 니가 퀘스트 안하고 배기겠냐고ㅋㅋ");
            Quest11_CONVERSATION2.AddQuestNo_Context("올ㅋㅋ 거절할줄도 알아?ㅋㅋ\n둘러보다와ㅋㅋ 넌 할 수 밖에 없을걸?ㅋㅋ\n\n[깊은 빡침이 몰려옵니다.]");
            Quest11_CONVERSATION2.AddQuestClear_Context("내 동생 '쪼개는 초원 슬라임' 이 날 찾았다고?ㅋ\n너 그런 잡심부름이나 하는 보잘것 없는 녀석이구나ㅋ\n\n[당신은 두 슬라임 형제 때문에 화가난 나머지 홧병이 생겨버렸습니다. 최대체력이 1 감소합니다.]");
            Quest11_CONVERSATION2.AddQuestProgress_Context("뭐해 빨리 가지않고ㅋㅋ");
            Quest11_CONVERSATION2.m_sQuest_Information_Recommend = ("ㅋㅋ");
            Quest11_CONVERSATION2.m_sQuest_Information_Process = ("'쪼개는 초원 슬라임' 의 형 '싸가지없는 초원 슬라임' 을 찾아 말을 걸어 봅시다.");
            Quest11_CONVERSATION2.m_sQuest_Information_Condition = ("'쪼개는 초원 슬라임' 의 형 '싸가지없는 초원 슬라임' 을 찾아 말을 걸어 봅시다.");
            Quest11_CONVERSATION2.m_sQuest_Information_Clear = ("'싸가지없는 초원 슬라임' 을 찾아 말을 걸었습니다.");
            Quest11_CONVERSATION2.m_sRewardSOC = new SOC(0, 0, 0, 1, 0, 0, 0, 0, 0);
            Quest11_CONVERSATION2.m_sRewardSTATUS = new STATUS(0, 0, 15, -1);
            Quest11_CONVERSATION2.m_nRewardGold = 10;
            m_Dictionary_QuestList_CONVERSATION.Add(Quest11_CONVERSATION2.m_nQuest_Code, Quest11_CONVERSATION2);
        }

        // [퀘스트[S2]: 서브퀘스트]\n[화나는 말투 ㅋ]
        {
            Quest12_CONVERSATION3.AddQuestDescription_Context("근데ㅋ");
            Quest12_CONVERSATION3.AddQuestDescription_Context("왜 날 찾아왔냐?ㅋ\n\n[당신은 '싸가지없는 초원 슬라임' 의 말투 때문에 짜증이 나기 시작합니다.]");
            Quest12_CONVERSATION3.AddQuestDescription_Context("ㅋ\nㅋ");
            Quest12_CONVERSATION3.AddQuestDescription_Context("날 찾아와서 뭐 어떻게하게ㅋ\n\n[사실 왜, 무엇 때문에 '쪼개는 초원 슬라임' 이 '싸가지없는 초원 슬라임' 을 찾으라고 한지 모릅니다.]");
            Quest12_CONVERSATION3.AddQuestDescription_Context("난 이유를 알거같은데?ㅋ\n무릎 꿇고 빌어봐ㅋ\n싫음 말고ㅋ\n\n[당신의 인내심에 큰 문제가 생기고 있습니다.]");
            Quest12_CONVERSATION3.AddQuestOk_Context("그럴줄 알았지ㅋ\n근데 알려주기 싫은데ㅋ\n정 궁금하면 '쪼개는 초원 슬라임' 에게 가서 물어보던가ㅋ\n\n[???????????당신은 진심으로 '싸가지없는 초원 슬라임' 을 죽이고 싶어합니다.]");
            Quest12_CONVERSATION3.AddQuestNo_Context("올ㅋ 거절할줄도 알아?ㅋ\n둘러보다와ㅋ 넌 할 수 밖에 없을걸?ㅋ\n왜냐면 퀘스트 진행이 안ㅋ되ㅋ거ㅋ든ㅋ\n\n[깊은 빡침이 몰려옵니다.]");
            Quest12_CONVERSATION3.AddQuestClear_Context("왜 다시왔어?ㅋㅋ\n\n[당신은 두 슬라임 형제 때문에 화가난 나머지 홧병이 더 심해졌습니다. 최대체력이 2 감소합니다.]");
            Quest12_CONVERSATION3.AddQuestProgress_Context("뭐해 빨리 가지않고ㅋ");
            Quest12_CONVERSATION3.m_sQuest_Information_Recommend = ("ㅋ");
            Quest12_CONVERSATION3.m_sQuest_Information_Process = ("다시 '쪼개는 초원 슬라임' 에게 돌아가 '싸가지없는 초원 슬라임' 을 찾으려는 이유를 알아야합니다.");
            Quest12_CONVERSATION3.m_sQuest_Information_Condition = ("다시 '쪼개는 초원 슬라임' 에게 돌아가 '싸가지없는 초원 슬라임' 을 찾으려는 이유를 알아야합니다.");
            Quest12_CONVERSATION3.m_sQuest_Information_Clear = ("'쪼개는 초원 슬라임' 에게 돌아가 다시 말을 걸었습니다.");
            Quest12_CONVERSATION3.m_ql_Quest_Necessity_Clear.Add(Quest11_CONVERSATION2);
            Quest12_CONVERSATION3.m_sRewardSOC = new SOC(0, 0, 0, 1, 0, 0, 0, 0, 0);
            Quest12_CONVERSATION3.m_sRewardSTATUS = new STATUS(0, 0, 15, -2);
            Quest12_CONVERSATION3.m_nRewardGold = 10;
            m_Dictionary_QuestList_CONVERSATION.Add(Quest12_CONVERSATION3.m_nQuest_Code, Quest12_CONVERSATION3);
        }

        // [퀘스트[S3]: 서브퀘스트]\n[홧병]
        {
            Quest13_CONVERSATION4.AddQuestDescription_Context("야~~ㅋㅋ 또왔네ㅋㅋ");
            Quest13_CONVERSATION4.AddQuestDescription_Context("왜 그냥 돌아왔니?ㅋㅋ 형은 못찾은거야?ㅋㅋ\n\n[당신은 '쪼개는 초원 슬라임' 의 말투 때문에 짜증이 나기 시작합니다.]");
            Quest13_CONVERSATION4.AddQuestDescription_Context("쉽다니깐ㅋㅋ 그것도 하나 못하냐ㅋㅋ\nㅋㅋ");
            Quest13_CONVERSATION4.AddQuestDescription_Context("형을 찾아야하는 이유?ㅋㅋ\n내가 왜 알려줘야되는데?ㅋㅋ 그냥 찾아와ㅋㅋ\n\n[당신은 굉장히 화가 많이 납니다. 속 깊은곳에서 무언가 끓어오름을 느낍니다.]");
            Quest13_CONVERSATION4.AddQuestDescription_Context("야 한대 치겠네ㅋㅋ 알았어 알려줄게ㅋㅋ\n밥먹으러 오라고ㅋㅋ 곧 점심시간이잖아ㅋㅋ\n싫음 말고ㅋㅋ\n\n[사실 두 형제는 당신을 가지고 놀고 있었습니다. 당신의 인내심에 큰 문제가 생기고 있습니다.]");
            Quest13_CONVERSATION4.AddQuestOk_Context("마지막으로 다녀오라고ㅋㅋ\n\n[당신은 진심으로 '쪼개는 초원 슬라임' 과 '싸가지없는 초원 슬라임' 을 죽이고 싶어합니다.]");
            Quest13_CONVERSATION4.AddQuestNo_Context("올ㅋㅋ 거절할줄도 알아?ㅋㅋ\n둘러보다와ㅋㅋ 넌 할 수 밖에 없을걸?ㅋㅋ\n왜냐면 퀘스트 진행이 안ㅋㅋ되ㅋㅋ거ㅋㅋ든ㅋㅋ\n\n[깊은 빡침이 몰려옵니다.]");
            Quest13_CONVERSATION4.AddQuestClear_Context("ㅋㅋ\n아 밥먹으러가야지~~ㅋㅋ 맛있겠다ㅋㅋ\n\n[당신은 두 슬라임 형제 때문에 화가난 나머지 홧병이 더 심해졌습니다. 최대체력이 3 감소합니다.]");
            Quest13_CONVERSATION4.AddQuestProgress_Context("빨리가ㅋㅋ\n형이 너때문에 밥때를 놓치면 안되잖아ㅋㅋ");
            Quest13_CONVERSATION4.m_sQuest_Information_Recommend = ("ㅂㄷㅂㄷ...");
            Quest13_CONVERSATION4.m_sQuest_Information_Process = ("다시 '싸가지없는 초원 슬라임' 에게 돌아가 밥먹으로 오라고 해야합니다.");
            Quest13_CONVERSATION4.m_sQuest_Information_Condition = ("다시 '싸가지없는 초원 슬라임' 에게 돌아가 밥먹으로 오라고 해야합니다.");
            Quest13_CONVERSATION4.m_sQuest_Information_Clear = ("'쪼개는 초원 슬라임' 과 '싸가지없는 초원 슬라임' 에게 끝까지 농락당하며 홧병을 얻었습니다.");
            Quest13_CONVERSATION4.m_ql_Quest_Necessity_Clear.Add(Quest12_CONVERSATION3);
            Quest13_CONVERSATION4.m_sRewardSOC = new SOC(0, 0, 0, 1, 0, 0, 0, 0, 0);
            Quest13_CONVERSATION4.m_sRewardSTATUS = new STATUS(0, 0, 15, -3);
            Quest13_CONVERSATION4.m_nRewardGold = 10;
            m_Dictionary_QuestList_CONVERSATION.Add(Quest13_CONVERSATION4.m_nQuest_Code, Quest13_CONVERSATION4);
        }

        // [퀘스트[S4]: 히든퀘스트]\n[화를 다스리는 법]
        {
            Quest14_GOAWAY_MONSTER2.AddQuestDescription_Context("안녕~");
            Quest14_GOAWAY_MONSTER2.AddQuestDescription_Context("난 하루하루를 즐기며 행복하게 살아가는 '행복한 짙은 앤트' 야~");
            Quest14_GOAWAY_MONSTER2.AddQuestDescription_Context("아무리 우리 앤트 종족이 지금 힘들더라도 나만큼은 웃음을 잃지 않으려 하고있어~");
            Quest14_GOAWAY_MONSTER2.AddQuestDescription_Context("너의 얼굴에는 화가 가득하구나~\n무슨일 있었니~?");
            Quest14_GOAWAY_MONSTER2.AddQuestDescription_Context("['쪼개는 초원 슬라임' 과 '싸가지없는 초원 슬라임' 형제에게 당한일을 말해줬습니다.]");
            Quest14_GOAWAY_MONSTER2.AddQuestDescription_Context("아... 그 두 슬라임 형제가 또 사고를 쳤구나~~ 안타까워라~");
            Quest14_GOAWAY_MONSTER2.AddQuestDescription_Context("흠~ 언제까지고 화를 낼 수는 없고 또 다~ 지난일이니 그들을 이제 용서하는게 어때~?");
            //Quest14_GOAWAY_MONSTER2.AddQuestDescription_Context("그들과 비슷하게 생긴 '웃고있는 초원 슬라임' 5마리를 '놓아주기' 를 통해 용서해 보도록 해봐~");
            Quest14_GOAWAY_MONSTER2.AddQuestOk_Context("잘생각했어~ 화만 내고있기엔 너무 시간이 아깝잖아~?\n행복해야할 시간도 부족한데말야~\n화를 어떻게 다스려야 할지는 스스로 생각해 보도록 해~");
            Quest14_GOAWAY_MONSTER2.AddQuestNo_Context("화를 다스릴 방법이 필요해 보여~");
            Quest14_GOAWAY_MONSTER2.AddQuestClear_Context("어때? 좀 화가 풀렸니?\n용서를 통해 넌 한발짝 자애로운 사람이 되었구나~");
            Quest14_GOAWAY_MONSTER2.AddQuestProgress_Context("그들을 아직 용서하지 못했구나~");
            //Quest14_GOAWAY_MONSTER2.m_sQuest_Information_Process = ("'드넓은 초원' 의 악동 '쪼개는 초원 슬라임' 과 '싸가지없는 초원 슬라임' 을 용서하기 위해 비슷하게생긴 '웃고있는 초원 슬라임' 5마리를 놓아주어야 합니다.");
            Quest14_GOAWAY_MONSTER2.m_sQuest_Information_Recommend = ("나는 절대로 화를 내지 않지~");
            Quest14_GOAWAY_MONSTER2.m_sQuest_Information_Process = ("'드넓은 초원' 의 악동 '쪼개는 초원 슬라임' 과 '싸가지없는 초원 슬라임' 을 용서합시다. 과연 어떻게 용서해야할까요?");
            Quest14_GOAWAY_MONSTER2.m_sQuest_Information_Condition = ("당신은 올라오는 화를 집어 삼키며 '쪼개는 초원 슬라임' 과 '싸가지없는 초원 슬라임' 과 닮은 '웃고있는 초원 슬라임' 5마리를 놓아주었습니다.");
            Quest14_GOAWAY_MONSTER2.m_sQuest_Information_Clear = ("'행복한 짙은 앤트' 의 말 대로 '쪼개는 초원 슬라임' 과 '싸가지없는 초원 슬라임' 을 닮은 '웃고있는 초원 슬라임' 5마리를 놓아주었습니다.\n당신은 '드넓은 초원' 의 악동 형제를 조금..이나마 용서한것 같군요?");
            Quest14_GOAWAY_MONSTER2.AddCondition(2002, 5, 0); // 5
            Quest14_GOAWAY_MONSTER2.m_ql_Quest_Necessity_Clear.Add(Quest13_CONVERSATION4);
            Quest14_GOAWAY_MONSTER2.m_bQuest_Information_Process_Hide = true;
            Quest14_GOAWAY_MONSTER2.m_sRewardSOC = new SOC(1, 0, 0, 5, 0, 5, 0, 0, 0);
            Quest14_GOAWAY_MONSTER2.m_sRewardSTATUS = new STATUS(0, 0, 20);
            Quest14_GOAWAY_MONSTER2.m_nRewardGold = 0;
            m_Dictionary_QuestList_GOAWAY_MONSTER.Add(Quest14_GOAWAY_MONSTER2.m_nQuest_Code, Quest14_GOAWAY_MONSTER2);
        }

        // [퀘스트[S5]: 히든퀘스트]\n[화내지않고 살아가기]
        {
            Quest15_CONVERSATION5.AddQuestDescription_Context("흠... 잠깐만~! '소설: 나의 슬라임 오렌지 나무' 라는 책을 줄게~ 한번 읽어봐~\n화를 다스리기 좋은 책이니까 부디 이 책을 읽고 화를 다스려 보도록 해~");
            Quest15_CONVERSATION5.AddQuestOk_Context("잠시만 기다려줘~ 옹이구멍에서 책을 찾아줄게~\n\n[다시 '행복한 짙은 앤트' 에게 말을 걸어봅시다.]");
            Quest15_CONVERSATION5.AddQuestNo_Context("한번 읽어보는게 좋을걸~?");
            Quest15_CONVERSATION5.AddQuestClear_Context("자 여기~ 이걸 읽고 앞으로는 화내지않고 살아가보도록해~");
            Quest15_CONVERSATION5.AddQuestProgress_Context("");
            Quest15_CONVERSATION5.m_sQuest_Information_Recommend = ("화를 다스리고 용서할 줄 안다는것은 인생의 크나큰 행복이란다~\n또한 많은것을 얻을 수있지~~~");
            Quest15_CONVERSATION5.m_sQuest_Information_Process = ("'행복한 짙은 앤트' 가 옹이구멍에서 '소설: 나의 슬라임 오렌지 나무' 를 찾고있습니다.\n잠시후 다시 '행복한 짙은 앤트' 에게 말을 걸어보세요.");
            Quest15_CONVERSATION5.m_sQuest_Information_Condition = ("'행복한 짙은 앤트' 가 옹이구멍에서 '소설: 나의 슬라임 오렌지 나무' 를 찾고있습니다.\n잠시후 다시 '행복한 짙은 앤트' 에게 말을 걸어보세요.");
            Quest15_CONVERSATION5.m_sQuest_Information_Clear = ("'행복한 짙은 앤트' 로 부터 화를 다스리는법을 배웠습니다. 또한 '소설: 나의 슬라임 오렌지 나무' 를 받았습니다.");
            Quest15_CONVERSATION5.m_ql_Quest_Necessity_Clear.Add(Quest14_GOAWAY_MONSTER2);
            Quest15_CONVERSATION5.m_bQuest_Information_Process_Hide = true;
            Quest15_CONVERSATION5.m_sRewardSOC = new SOC(1, 0, 0, 1, 0, 1, 0, 0, 0);
            Quest15_CONVERSATION5.m_sRewardSTATUS = new STATUS(0, 0, 10);
            Quest15_CONVERSATION5.m_lRewardList_Item_Use.Add(ItemManager.instance.m_Dictionary_QuestReward_Use[10001]);
            Quest15_CONVERSATION5.m_nRewardGold = 0;
            m_Dictionary_QuestList_CONVERSATION.Add(Quest15_CONVERSATION5.m_nQuest_Code, Quest15_CONVERSATION5);
        }

        // [퀘스트[S2]: 스토리퀘스트]\n[우릴 좀 내버려둬]
        {
            Quest16_GOAWAY_TYPE1.AddQuestDescription_Context("이보게 모함가양반... 내 부탁하나 해도 되겠나...?");
            Quest16_GOAWAY_TYPE1.AddQuestDescription_Context("보다시피 내 주변에 다수의 '짙은 앤트' 일족이 살아가고 있다네...\n이 근처에는 나의 영향력이 뻗치기에 평화롭게 살아갈 수 있기 때문이지....");
            Quest16_GOAWAY_TYPE1.AddQuestDescription_Context("그러나 내 영향력이 미치지 못하는곳에서는 아직 우리 '짙은 앤트' 일족이 두려움에 떨고 있지....");
            Quest16_GOAWAY_TYPE1.AddQuestDescription_Context("우리 일족을 위해 슬라임들을 '놓아주기' 를 통해 20마리 퇴치해 줄 수 있겠는가...?\n우리 '짙은 앤트' 일족을 건드리지 말라는 마지막 경고인 셈이지...!!!");
            Quest16_GOAWAY_TYPE1.AddQuestOk_Context("고맙네..!! 종류에 상관없이 슬라임들을 20마리 퇴치해 보게..!");
            Quest16_GOAWAY_TYPE1.AddQuestNo_Context("아쉽군... 그렇다면 내가 나서야하는가....... 그럼 일이 커지고 말걸세...\n다시 생각해 보게나...!");
            Quest16_GOAWAY_TYPE1.AddQuestProgress_Context("좀더 슬라임들을 퇴치 해주게나..!\n아직 슬라임들에게 우리 진심이 전해지지 않았을거야...");
            Quest16_GOAWAY_TYPE1.AddQuestClear_Context("정말 고맙네!!\n무리한 부탁이었는데 들어주어 고맙네..!\n이로써 슬라임들도 우릴 괴롭히지 않을걸세..^^!");
            Quest16_GOAWAY_TYPE1.m_sQuest_Information_Recommend = ("끄응...! 모두의 '드넓은 초원' 이 어째서...!!");
            Quest16_GOAWAY_TYPE1.m_sQuest_Information_Process = ("'드넓은 초원' 의 '짙은 앤트' 들을 괴롭히는 슬라임들을 종류에 상관없이 '놓아주기' 를 사용해서 20마리 퇴치해주게...\n아직은 평화롭게....");
            Quest16_GOAWAY_TYPE1.m_sQuest_Information_Condition = ("'짙은 앤트' 무리를 괴롭히는 '초원 슬라임' 들에게 경고를 했습니다. 슬라임 20마리정도를 놓아주었습니다.\n이제 '천년 묵은 짙은 앤트' 에게 가봅시다.");
            Quest16_GOAWAY_TYPE1.m_sQuest_Information_Clear = ("'천년 묵은 짙은 앤트' 의 부탁대로 슬라임 20마리를 놓아주었습니다.\n이번엔 평화롭게 '놓아주기' 를 통해 해결했지만.. 과연 이 평화가 오래갈까요?");
            Quest16_GOAWAY_TYPE1.SetCondition(E_MONSTER_KIND.SLIME, 20, 0);
            Quest16_GOAWAY_TYPE1.m_sRewardSOC = new SOC(2, 0, 0, 2, 0, 2, 0, 0, 0);
            Quest16_GOAWAY_TYPE1.m_sRewardSTATUS = new STATUS(0, 0, 30);
            Quest16_GOAWAY_TYPE1.m_nRewardGold = 500;
            m_Dictionary_QuestList_GOAWAY_TYPE.Add(Quest16_GOAWAY_TYPE1.m_nQuest_Code, Quest16_GOAWAY_TYPE1);
        }

        // [퀘스트[S2]: 스토리퀘스트]\n[잘못을 했으면 벌을 받는게 세상의 이치]
        {
            Quest17_KILL_TYPE1.AddQuestDescription_Context("자네왔나...");
            Quest17_KILL_TYPE1.AddQuestDescription_Context("그동안 많은 일이 있었지... 우리의 평화로운 경고는 철저히 무시당했다네!!\n슬라임들의 핍박과 괴롭힘은 끊이질 않고있고..! 우리 '짙은 앤트' 들은 하루하루 비명을 지르고 있다네!\n특히 '주식회사 더 슬라' 의 횡포는 정말이지...!!!");
            Quest17_KILL_TYPE1.AddQuestDescription_Context("이젠 달리 방법이 없어!\n우리 '짙은 앤트' 일족은 슬라임들과 전쟁을 치룰걸세! 어떤 대가를 치르더라도!");
            Quest17_KILL_TYPE1.AddQuestDescription_Context("잘못을 했으면 벌을 받는게 세상의 이치라더군!");
            Quest17_KILL_TYPE1.AddQuestDescription_Context("이제 우리가 벌을줄게! 달게 받아라 이 탐욕스러운 슬라임짜식들!!");
            Quest17_KILL_TYPE1.AddQuestDescription_Context("지금당장 슬라임 청소를 시작할걸세! 자네도 함께할텐가?!\n보이는 모든 슬라임들을 죽일걸세!");
            Quest17_KILL_TYPE1.AddQuestOk_Context("고맙네..!! 나도 내키지는 않지만... 말로해서 안들은 '초원 슬라임' 들 잘못일세..!\n이제 우리 '짙은 앤트' 일족도 참지 않겠다네...!!\n슬라임들을 종류에 상관없이 20마리 사냥해주게..!");
            Quest17_KILL_TYPE1.AddQuestNo_Context("아쉽군.. 자네와 함께라면 일이 더 수월하게 진행되었을텐데..!");
            Quest17_KILL_TYPE1.AddQuestProgress_Context("아직일세!!! 좀더 슬라임들을 사냥하게나..!");
            Quest17_KILL_TYPE1.AddQuestClear_Context("덕분에 당분간 슬라임들은 조용할테지.. 고맙네.....\n그러나 나 또한 탐욕스러운 슬라임들과 같은짓을 해버렸군..... 이젠 정말 돌이킬 수 없을지도..............");
            Quest17_KILL_TYPE1.m_sQuest_Information_Recommend = ("도저히 안되겠군!\n꼭 그래야만 했___냐~!~!\n지금부터 녀석들에게 벌을 줄테다..!");
            Quest17_KILL_TYPE1.m_sQuest_Information_Process = ("'드넓은 초원' 의 '짙은 앤트' 들을 괴롭히는 슬라임들을 종류에 상관없이 20마리 사냥해주게...");
            Quest17_KILL_TYPE1.m_sQuest_Information_Condition = ("'짙은 앤트' 무리를 괴롭히는 '초원 슬라임' 들을 무자비하게 20마리정도를 사냥했습니다.\n이제 '천년 묵은 짙은 앤트' 에게 가봅시다.");
            Quest17_KILL_TYPE1.m_sQuest_Information_Clear = ("'천년 묵은 짙은 앤트' 의 부탁대로 슬라임 20마리를 무자비하게 사냥했습니다.\n'짙은 앤트' 들은 폭력을 이용한 모순적인 잠깐의 평화를 얻었습니다. 과연..?");
            Quest17_KILL_TYPE1.SetCondition(E_MONSTER_KIND.SLIME, 20, 0);
            Quest17_KILL_TYPE1.m_ql_Quest_Necessity_Clear.Add(Quest16_GOAWAY_TYPE1);
            Quest17_KILL_TYPE1.m_ql_Quest_Necessity_Clear.Add(Quest18_COLLECT4);
            Quest17_KILL_TYPE1.m_sRewardSOC = new SOC(2, 0, 0, -2, 0, 2, 0, 0, 1);
            Quest17_KILL_TYPE1.m_sRewardSTATUS = new STATUS(0, 0, 40);
            Quest17_KILL_TYPE1.m_nRewardGold = 500;
            m_Dictionary_QuestList_KILL_TYPE.Add(Quest17_KILL_TYPE1.m_nQuest_Code, Quest17_KILL_TYPE1);
        }

        // [퀘스트[S3]: 스토리퀘스트]\n['골드 타임 슬라임' 의 의뢰 1]
        {
            Quest18_COLLECT4.AddQuestDescription_Context("이런 시간이 없군요...\n\n['골드 타임 슬라임' 은 굉장히 초조해 하고있습니다.]");
            Quest18_COLLECT4.AddQuestDescription_Context("앗! 마침 잘됬군요. 저희 부탁 하나만 들어주시죠?");
            Quest18_COLLECT4.AddQuestDescription_Context("구구절절한 설명은 하지 않겠습니다. 바로 설명하죠.");
            Quest18_COLLECT4.AddQuestDescription_Context("요즘 '짙은 앤트의 나무가죽'이 '드넓은 초원'에서 인기랍니다.\n실제로 '짙은 앤트의 나무가죽' 을 사용하고있는 모습을 심심치않게 찾아 볼 수 있죠.");
            Quest18_COLLECT4.AddQuestDescription_Context("하지만 주문은 밀려있는 상황에 '짙은 앤트' 들의 저항은 점점 심해지고 있죠.\n숨어버렸거나 강력하게 저항한다거나 한답니다.\n어떻습니까? 보상은 두둑 ~ 히 챙겨드리겠습니다.");
            Quest18_COLLECT4.AddQuestOk_Context("좋습니다.\n'짙은 앤트의 나무가죽' 을 5개 구해주세요.\n구하는 방식은 따지지 않습니다. 구해만 오세요. 급합니다.");
            Quest18_COLLECT4.AddQuestNo_Context("이런! 한시가 급한데 제 시간을 낭비했군요!");
            Quest18_COLLECT4.AddQuestProgress_Context("아직인가요..\n시간이 없습니다..!\n빨리 구해오도록 하세요.");
            Quest18_COLLECT4.AddQuestClear_Context("음 양질의 '짙은 앤트의 나무가죽' 이군요.\n덕분에 거래 시간에 늦지 않을것 같네요. 빚을 졌네요.^^");
            Quest18_COLLECT4.m_sQuest_Information_Recommend = ("이런.. 시간이 없는걸....");
            Quest18_COLLECT4.m_sQuest_Information_Process = ("요즘 '드넓은 초원' 에서 유행하는 최고로 핫한 '짙은 앤트의 나무가죽'을 5개 구해와주세요. 급합니다.");
            Quest18_COLLECT4.m_sQuest_Information_Condition = ("'골드 타임 슬라임' 의 첫번째 의뢰인 '짙은 앤트의 나무가죽' 5개를 성공적으로 구했습니다.\n이제 '골드 타임 슬라임' 에게 돌아가봅시다.");
            Quest18_COLLECT4.m_sQuest_Information_Clear = ("물량이 없어 초조해 하던 '골드 타임 슬라임' 의 첫번째 의뢰인 '짙은 앤트의 나무가죽' 5개를 구해줬습니다.\n'짙은 앤트의 나무가죽' 처럼 가볍지만 매우 튼튼하며 색감이 고급 진 재료는 항상 인기랍니다.");
            Quest18_COLLECT4.AddCondition(0003, 5, 0); // 5, 0
            Quest18_COLLECT4.m_sRewardSOC = new SOC(2, 0, 0, 2, 0, -2, 0, 0, 1);
            Quest18_COLLECT4.m_sRewardSTATUS = new STATUS(0, 0, 20);
            Quest18_COLLECT4.m_nRewardGold = 1000;
            Quest18_COLLECT4.m_lRewardList_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12600]);
            m_Dictionary_QuestList_COLLECT.Add(Quest18_COLLECT4.m_nQuest_Code, Quest18_COLLECT4);
        }

        // [퀘스트[S4]: 스토리퀘스트]\n['골드 타임 슬라임' 의 의뢰 2]
        {
            Quest19_COLLECT5.AddQuestDescription_Context("＃!!~~!!@@#@~$~!!~\n\n['골드 타임 슬라임' 이 그의 부하 직원에게 화를 내고 있습니다.]");
            Quest19_COLLECT5.AddQuestDescription_Context("!@#~~!!@!!!!\n아 당신이군요. 몰라뵈어 죄송합니다. 거래에 빵꾸가 났거든요..");
            Quest19_COLLECT5.AddQuestDescription_Context("저희는 많은 물품을 판매하고 있지만 그중에서도 단연 돋보이는건 재생 특효약 '알보찰' 이랍니다.\n이번에는 이 '알보찰' 공급에 문제가 생겨버렸네요.");
            Quest19_COLLECT5.AddQuestDescription_Context("긴말하지 않겠습니다.\n당신의 능력이라면 가능하다고 생각합니다.\n'알보찰' 의 원료인 '초원 슬라임의 덩어리' 를 3개 구해와 주세요.");
            Quest19_COLLECT5.AddQuestOk_Context("좋습니다. 최대한 빨리 구해와 주세요. 고객님이 까다로운 분이거든요.\n아시다시피 어떤 방법이던 구해오기만 하면 된답니다.");
            Quest19_COLLECT5.AddQuestNo_Context("이를어째..!");
            Quest19_COLLECT5.AddQuestProgress_Context("서둘러주세요.\n거래에는 신뢰가 제일 중요하니까요.");
            Quest19_COLLECT5.AddQuestClear_Context("오! '초원 슬라임의 덩어리' 3개 잘 받았습니다.\n제법이군요.\n또다시 빚을졌군요.^^");
            Quest19_COLLECT5.m_sQuest_Information_Recommend = ("＃!!~~!!@@#@~$~!!~\n어우 씨!\n이를 어째....");
            Quest19_COLLECT5.m_sQuest_Information_Process = ("이런! 말도안됩니다!!!\n직원 실수로 귀하디 귀한 '알보찰' 의 공급에 문제가 생겼습니다!!!\n'알보찰' 의 원료인 '초원 슬라임의 덩어리' 를 3개만 구해주세요.\n'골드 타임 슬라임' 은 굉장히 화나보입니다.");
            Quest19_COLLECT5.m_sQuest_Information_Condition = ("'골드 타임 슬라임' 의 두번째 의뢰인 '초원 슬라임의 덩어리' 3개를 성공적으로 구했습니다.\n이제 '골드 타임 슬라임' 에게 돌아가봅시다.");
            Quest19_COLLECT5.m_sQuest_Information_Clear = ("굉장히 엄청난 화를 내던 '골드 타임 슬라임' 의 두번째 의뢰인 '알보찰' 의 원활한 공급에 기여했습니다. '초원 슬라임의 덩어리' 3개를 구해줬습니다.\n'골드 타임 슬라임' 의 화가 조금은 풀어진 것 같습니다.\n'초원 슬라임의 덩어리' 로 만들어지는 '알보찰' 은 재생에 탁월한 비약입니다. 그러나 '초원 슬라임의 고통' 이라고도 부릅니다.");
            Quest19_COLLECT5.AddCondition(0002, 3, 0); // 3, 0
            Quest19_COLLECT5.m_ql_Quest_Necessity_Clear.Add(Quest18_COLLECT4);
            Quest19_COLLECT5.m_sRewardSOC = new SOC(3, 0, 0, -3, 0, 0, 0, 0, 1);
            Quest19_COLLECT5.m_sRewardSTATUS = new STATUS(0, 0, 20);
            Quest19_COLLECT5.m_nRewardGold = 1000;
            Quest19_COLLECT5.m_lRewardList_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12600]);
            m_Dictionary_QuestList_COLLECT.Add(Quest19_COLLECT5.m_nQuest_Code, Quest19_COLLECT5);
        }

        // [퀘스트[S4]: 히든퀘스트]\n[은밀한 의뢰]
        {
            Quest20_KILL_MONSTER4.AddQuestDescription_Context("어서오세요. 기다리고 있었습니다.\n아주 중요한 의뢰입니다.");
            Quest20_KILL_MONSTER4.AddQuestDescription_Context("요즘 '주식회사 더 슬라' 를 대놓고 방해하는 무리가 많아졌습니다.\n이제 뭔가 조치를 취해야 할 것 같습니다.");
            Quest20_KILL_MONSTER4.AddQuestDescription_Context("'드넓은 초원' 의 평화? 알.빠 입니까?\n저희는 돈만 벌면 되는겁니다.");
            Quest20_KILL_MONSTER4.AddQuestDescription_Context("돈. 두둑히 챙겨드리겠습니다.\n또한 당신을 '주식회사 더 슬라' 의 VIP 로 인정하겠습니다.");
            Quest20_KILL_MONSTER4.AddQuestDescription_Context("이번에도 의뢰를 수행 하시겠습니까?\n이번 의뢰를 성공적으로 마친다면 <color=red>특별한 거래</color> 또한 가능하게 될지도모르겠군요.");
            Quest20_KILL_MONSTER4.AddQuestDescription_Context("이번 의뢰는 '잔디 머리 초원 슬라임' 과 '짙은 앤트' 암살입니다.\n누가 아래이고 위인지 확실하게 각인시켜 주자구요.");
            Quest20_KILL_MONSTER4.AddQuestOk_Context("좋습니다.\n'잔디 머리 초원 슬라임' 과 '짙은 앤트' 를 5마리씩 죽여주세요. 본보기입니다.\n이번 의뢰 또한 믿고 있겠습니다.\n감히 '주식회사 더 슬라' 에 반기를 들다니.. 있을 수 없는 일입니다.");
            Quest20_KILL_MONSTER4.AddQuestNo_Context("천천히 다시 생각해보시죠. 분명 좋은 조건입니다.");
            Quest20_KILL_MONSTER4.AddQuestClear_Context("역시 당신도 돈이 가장 중요한것 같군요... 좋습니다.\n\n<color=red>['특별한 거래' 가 가능해집니다.]</color>");
            Quest20_KILL_MONSTER4.AddQuestProgress_Context("아직 본보기를 덜 보인것 같군요.");
            Quest20_KILL_MONSTER4.m_sQuest_Information_Recommend = ("좋은 물건.. 값싼 가격..\n모든것은 '주식회사 더슬라' 에 대한 충성과 이어지죠.");
            Quest20_KILL_MONSTER4.m_sQuest_Information_Process = ("'주식회사 더 슬라' 를 방해하는 무리를 토벌합시다. '슬라임 인권 협회' 의 '잔디 머리 초원 슬라임' 과 '짙은 앤트' 를 각각 5마리씩 토벌합시다.");
            Quest20_KILL_MONSTER4.m_sQuest_Information_Condition = ("'골드 타임 슬라임' 의 은밀한 의뢰인 '잔디 머리 초원 슬라임' 과 '짙은 앤트' 를 5마리씩 죽였습니다.\n이제 '골드 타임 슬라임' 에게 돌아가봅시다.");
            Quest20_KILL_MONSTER4.m_sQuest_Information_Clear = ("'골드 타임 슬라임' 의 은밀한 의뢰인 '주식회사 더 슬라' 를 방해하는 무리인 '슬라임 인권 협회' 의 '잔디 머리 초원 슬라임' 과 '짙은 앤트' 를 5마리씩 죽였습니다.\n그렇게 당신은 '주식회사 더 슬라' 의 VIP 가 되어 '은밀한 거래' 가 가능해 졌습니다.");
            Quest20_KILL_MONSTER4.AddCondition(0003, 5, 0); // 5, 0
            Quest20_KILL_MONSTER4.AddCondition(2001, 5, 0); // 5, 0
            Quest20_KILL_MONSTER4.m_ql_Quest_Necessity_Clear.Add(Quest19_COLLECT5);
            Quest20_KILL_MONSTER4.m_ql_Quest_Necessity_NonClear.Add(Quest21_KILL_MONSTER5);
            Quest20_KILL_MONSTER4.m_ql_Quest_Necessity_NonClear.Add(Quest22_KILL_MONSTER6);
            Quest20_KILL_MONSTER4.m_ql_Quest_Necessity_NonProcess.Add(Quest21_KILL_MONSTER5);
            Quest20_KILL_MONSTER4.m_ql_Quest_Necessity_NonProcess.Add(Quest22_KILL_MONSTER6);
            Quest20_KILL_MONSTER4.m_sRewardSOC = new SOC(1, 0, 0, -10, 0, -10, 0, 0, 1);
            Quest20_KILL_MONSTER4.m_sRewardSTATUS = new STATUS(0, 0, 30); // 10
            Quest20_KILL_MONSTER4.m_nRewardGold = 2000;
            Quest20_KILL_MONSTER4.m_lRewardList_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12601]);
            m_Dictionary_QuestList_KILL_MONSTER.Add(Quest20_KILL_MONSTER4.m_nQuest_Code, Quest20_KILL_MONSTER4);
        }

        // [퀘스트[S4]: 히든퀘스트]\n[은밀한 의뢰]
        {
            Quest21_KILL_MONSTER5.AddQuestDescription_Context("어서오세요. 기다리고 있었습니다.\n아주 중요한 의뢰입니다.");
            Quest21_KILL_MONSTER5.AddQuestDescription_Context("요즘 '주식회사 더 슬라' 를 대놓고 방해하는 무리가 많아졌습니다.\n이제 뭔가 조치를 취해야 할 것 같습니다.");
            Quest21_KILL_MONSTER5.AddQuestDescription_Context("'드넓은 초원' 의 평화? 알.빠 입니까?\n저희는 돈만 벌면 되는겁니다.");
            Quest21_KILL_MONSTER5.AddQuestDescription_Context("돈. 두둑히 챙겨드리겠습니다.\n또한 당신을 '주식회사 더 슬라' 의 VIP 로 인정하겠습니다.");
            Quest21_KILL_MONSTER5.AddQuestDescription_Context("이번에도 의뢰를 수행 하시겠습니까?\n<color=red>이번 의뢰를 성공적으로 마친다면 특별한 거래또한 가능하게 될지도모르겠군요.</color>");
            Quest21_KILL_MONSTER5.AddQuestDescription_Context("이번 의뢰는 '잔디 머리 초원 슬라임' 과 '짙은 앤트' 암살입니다.\n누가 아래이고 위인지 확실하게 각인시켜 주자구요.");
            Quest21_KILL_MONSTER5.AddQuestOk_Context("그러나 당신은 '슬라임 인권 협회' 를 위해 '무력시위' 를 했더군요..\n본인이 증명하십시오. 누구와 함께 할지..\n'잔디 머리 초원 슬라임' 과 '짙은 앤트' 를 20마리씩 죽여주세요. 본보기입니다.\n감히 '주식회사 더 슬라' 에 반기를 들다니.. 있을 수 없는 일입니다.");
            Quest21_KILL_MONSTER5.AddQuestNo_Context("저희 '주식회사 더 슬라' 와 함께할 마지막 기회인것같군요.");
            Quest21_KILL_MONSTER5.AddQuestClear_Context("역시 당신도 돈이 가장 중요한것 같군요... 좋습니다.\n\n<color=red>['특별한 거래' 가 가능해집니다.]</color>");
            Quest21_KILL_MONSTER5.AddQuestProgress_Context("아직 본보기를 덜 보인것 같군요.");
            Quest21_KILL_MONSTER5.m_sQuest_Information_Recommend = ("좋은 물건.. 값싼 가격..\n모든것은 '주식회사 더슬라' 에 대한 충성과 이어지죠.");
            Quest21_KILL_MONSTER5.m_sQuest_Information_Process = ("'주식회사 더 슬라' 를 방해하는 무리를 토벌합시다. '슬라임 인권 협회' 의 '잔디 머리 초원 슬라임' 과 '짙은 앤트' 를 각각 20마리씩 토벌합시다.");
            Quest21_KILL_MONSTER5.m_sQuest_Information_Condition = ("'골드 타임 슬라임' 의 은밀한 의뢰인 '잔디 머리 초원 슬라임' 과 '짙은 앤트' 를 5마리씩 죽였습니다.\n이제 '골드 타임 슬라임' 에게 돌아가봅시다.");
            Quest21_KILL_MONSTER5.m_sQuest_Information_Clear = ("'골드 타임 슬라임' 의 은밀한 의뢰인 '주식회사 더 슬라' 를 방해하는 무리인 '슬라임 인권 협회' 의 '잔디 머리 초원 슬라임' 과 '짙은 앤트' 를 20마리씩 죽였습니다.\n그렇게 당신은 '주식회사 더 슬라' 의 VIP 가 되어 '은밀한 거래' 가 가능해 졌습니다.");
            Quest21_KILL_MONSTER5.AddCondition(0003, 20, 0); // 5, 0
            Quest21_KILL_MONSTER5.AddCondition(2001, 20, 0); // 5, 0
            Quest21_KILL_MONSTER5.m_ql_Quest_Necessity_Clear.Add(Quest19_COLLECT5);
            Quest21_KILL_MONSTER5.m_ql_Quest_Necessity_Clear.Add(Quest22_KILL_MONSTER6);
            Quest21_KILL_MONSTER5.m_ql_Quest_Necessity_NonClear.Add(Quest20_KILL_MONSTER4);
            Quest21_KILL_MONSTER5.m_ql_Quest_Necessity_NonProcess.Add(Quest20_KILL_MONSTER4);
            Quest21_KILL_MONSTER5.m_sRewardSOC = new SOC(1, 0, 0, -10, 0, -10, 0, 0, 1);
            Quest21_KILL_MONSTER5.m_sRewardSTATUS = new STATUS(0, 0, 30); // 10
            Quest21_KILL_MONSTER5.m_nRewardGold = 1000;
            Quest21_KILL_MONSTER5.m_lRewardList_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12601]);
            m_Dictionary_QuestList_KILL_MONSTER.Add(Quest21_KILL_MONSTER5.m_nQuest_Code, Quest21_KILL_MONSTER5);
        }

        // [퀘스트[S3]: 스토리퀘스트]\n[무력시위]
        {
            Quest22_KILL_MONSTER6.AddQuestDescription_Context("'드넓은 초원' 의 평화를 해치는 '주식회사 더 슬라' 는 물러가라!!!!\n(물러가라!!!)\n(물러가라!!!)\n(물러가라!!!)");
            Quest22_KILL_MONSTER6.AddQuestDescription_Context("우리는!!!\n핍박받는 슬라임들을 더이상 두고볼 수 없다!!!!");
            Quest22_KILL_MONSTER6.AddQuestDescription_Context("'골드 타임 슬라임' !!!!!!! \n돈이면 다 되는줄 아냐!!!!!!!!!");
            Quest22_KILL_MONSTER6.AddQuestDescription_Context("너 또한 '초원 슬라임의 덩어리' 로 만들어지는 '알보찰' 을 잘 알테지!!");
            Quest22_KILL_MONSTER6.AddQuestDescription_Context("'알보찰' 하나를 만들기 위해 '초원 슬라임' 10여마리 정도가 희생되는것을 아느냐!!!!!!");
            Quest22_KILL_MONSTER6.AddQuestDescription_Context("우리는 싸울것이다!!\n'주식회사 더 슬라' 가 더이상 '알보찰' 을 만들지 않을때 까지!!!");
            Quest22_KILL_MONSTER6.AddQuestDescription_Context("너도 우리와 함께 싸워 슬라임들의 인권을 지켜주자!!!!!");
            Quest22_KILL_MONSTER6.AddQuestDescription_Context("우리는 끝까지 싸울것이다!!\n너 또한 '상인단 경비 초원 슬라임' 5마리를 죽이고 뜻을 보태라!!!!!!");
            Quest22_KILL_MONSTER6.AddQuestOk_Context("좋다!!! '상인단 경비 초원 슬라임' 은 상당히 강력하니 조심해라!!");
            Quest22_KILL_MONSTER6.AddQuestNo_Context("(물러가라!!)\n(물러가라!!!)\n니놈또한 '주식회사 더 슬라' 에 물든놈이었군...!!!\n 꺼져라!!");
            Quest22_KILL_MONSTER6.AddQuestClear_Context("(물러가라!!)\n(물러가라!)\n... 좋다..\n우리함께 끝까지 싸워보자!!!");
            Quest22_KILL_MONSTER6.AddQuestProgress_Context("아직이다!!! 증명해라! 니놈의 뜻을!!!");
            Quest22_KILL_MONSTER6.m_sQuest_Information_Recommend = ("물러가라!!!! 물러가라!!!!\n'드넓은 초원' 의 평화를 해치는 '주식회사 더 슬라' 는 물러가라!!!!\n더이상의 협상은 이제 없다!!!!");
            Quest22_KILL_MONSTER6.m_sQuest_Information_Process = ("'슬라임 인권 협회' 의 극단주의자인 '잔디머리 초원 슬라임' 과 뜻을 함께하기로 했습니다.\n'상인단 경비 초원 슬라임' 5마리를 죽여야합니다.");
            Quest22_KILL_MONSTER6.m_sQuest_Information_Condition = ("'상인단 경비 초원 슬라임' 5마리를 죽이고 '슬라임 인권 협회' 의 극단주의자인 '잔디머리 초원 슬라임' 의 뜻에 동참했습니다.\n이제 '잔디 머리 초원 슬라임' 에게 돌아가봅시다.");
            Quest22_KILL_MONSTER6.m_sQuest_Information_Clear = ("당신은 '슬라임 인권 협회'의 극단주의자인 '잔디머리 초원 슬라임' 과 뜻을 함께합니다.\n모든것은 슬라임의 인권을 위해!!\n더 나아가 '드넓은 초원' 의 평화를 위해!!");
            Quest22_KILL_MONSTER6.m_ql_Quest_Necessity_NonProcess.Add(Quest20_KILL_MONSTER4);
            Quest22_KILL_MONSTER6.AddCondition(2003, 5, 0); // 5, 0
            Quest22_KILL_MONSTER6.m_sRewardSOC = new SOC(3, 0, 0, 5, 0, 5, 0, 0, -1);
            Quest22_KILL_MONSTER6.m_sRewardSTATUS = new STATUS(0, 0, 30); // 10
            Quest22_KILL_MONSTER6.m_nRewardGold = 500;
            m_Dictionary_QuestList_KILL_MONSTER.Add(Quest22_KILL_MONSTER6.m_nQuest_Code, Quest22_KILL_MONSTER6);
        }

        // [퀘스트[S3]: 히든퀘스트]\n[작은 바위의 꿈]
        {
            Quest23_COLLECT6.AddQuestDescription_Context(". . . ");
            Quest23_COLLECT6.AddQuestDescription_Context(". . . ");
            Quest23_COLLECT6.AddQuestDescription_Context(". . . \n\n['작은 바위' 에 우거진 나뭇가지 사이로 빛이 내리쬐고 있습니다.]");
            Quest23_COLLECT6.AddQuestDescription_Context(". . . . . .\n\n[하지만 어딘가모르게 외로워 보이는군요.]");
            Quest23_COLLECT6.AddQuestDescription_Context(". . .\n\n['작은 바위' 곁에 함께할 무언가를 찾아봐야겠군요.]\n[어디서든 잘 자라는 균류가 좋아보입니다.]");
            Quest23_COLLECT6.AddQuestOk_Context(". . .");
            Quest23_COLLECT6.AddQuestNo_Context(". . .\n\n['작은 바위' 가 시무룩해 하는것 같습니다.]");
            Quest23_COLLECT6.AddQuestProgress_Context("외로워 보이는 '작은 바위' 를 꾸밀 무언가를 찾아야 합니다.");
            Quest23_COLLECT6.AddQuestClear_Context("더이상 '작은 바위' 는 외로워 하지 않습니다. '맛없어 보이는 버섯' 과 함께니까요.^^\n\n['맛없어 보이는 버섯' 을 '작은 바위' 곁에 뒀습니다.]\n[당신은 '작은 바위' 의 감수성을 느꼈습니다. 최대 마나가 2, 방어력이 1 증가합니다.]");
            Quest23_COLLECT6.m_sQuest_Information_Recommend = ("'작은 바위' 가 외로워 보입니다.");
            Quest23_COLLECT6.m_sQuest_Information_Process = ("외로워 보이는 '작은 바위' 를 꾸밀 무언가를 찾아야 합니다.");
            Quest23_COLLECT6.m_sQuest_Information_Condition = ("'작은 바위' 에 어울릴법한것을 찾았습니다.\n이제 '작은 바위' 에게 돌아가 외로워 보이지 않게 근처를 꾸며줍시다.");
            Quest23_COLLECT6.m_sQuest_Information_Clear = ("이제부터 '작은 바위' 곁에는 '맛없어 보이는 버섯' 이 함께합니다.\n더이상 '작은 바위' 는 외롭지 않습니다.\n'작은 바위' 의 감수성을 느껴 최대 마나가 2, 방어력이 1만큼 증가했습니다.");
            Quest23_COLLECT6.AddCondition(9000, 1, 0);
            Quest23_COLLECT6.m_bQuest_Information_Process_Hide = true;
            Quest23_COLLECT6.m_sRewardSOC = new SOC(1, 0, 0, 0, 0, 0, 0, 0, 0);
            Quest23_COLLECT6.m_sRewardSTATUS = new STATUS(0, 0, 30, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0);
            Quest23_COLLECT6.m_nRewardGold = 0;
            m_Dictionary_QuestList_COLLECT.Add(Quest23_COLLECT6.m_nQuest_Code, Quest23_COLLECT6);
        }

        // [퀘스트[S1]: 튜토리얼]\n['드넓은 초원' 으로..]
        {
            Quest24_CONVERSATION6.AddQuestDescription_Context("끌끌끌..\n\n['늙고 병든 슬라임' 이 당신을 흐뭇하게 쳐다봅니다. 그러나 한편으로는 근심어린 표정또한 보입니다.]");
            Quest24_CONVERSATION6.AddQuestDescription_Context("내 자네를 보아하니 그동안 내 부탁을 들어주며 많이 성장했더군..");
            Quest24_CONVERSATION6.AddQuestDescription_Context("자네 정도라면 이제 앞으로 나아갈 때가 되지 않았나 싶네.\n'깊디깊은숲' 을 벗어나 '드넓은 초원' 으로 나아가 자네의 꿈을 펼쳐보게나.");
            Quest24_CONVERSATION6.AddQuestDescription_Context("그러나 지금의 '드넓은 초원' 은 조금 위험한것 같더군...\n사실 자네가 수행한 의뢰 중 길잃은 '꼬마 초원 슬라임' 들을 집으로 돌려보내라는 의뢰 또한 '드넓은 초원' 의 현 상황을 보여주지..");
            Quest24_CONVERSATION6.AddQuestDescription_Context("이 늙은이가 조사한 바에 의하면 서로의 이해관계 때문에 크고 작은 소란이 일어나는 모양이야..\n나도 자세한건 잘 모르겠다네...");
            Quest24_CONVERSATION6.AddQuestDescription_Context("부디 조심하게나...!\n또 기회가 된다면 '드넓은 초원' 에 일어나고있는 일을 설명해주러 와주게나.");
            Quest24_CONVERSATION6.AddQuestDescription_Context("짜릿한 모험, 명예, 막대한 부 등 원하는 어떠한것이라도 자네는 얻을 수 있다네.\n자네가 생각하기에 가장 중요한 가치는 무엇인지 잘 생각해보게나..\n부디 이세계를 재미있게 즐겨주기를...");
            Quest24_CONVERSATION6.AddQuestOk_Context("좋네! 조금만 있다가 말을 걸어주게나!! 자네의 여행에 약소하지만 조금 보태고 싶군..");
            Quest24_CONVERSATION6.AddQuestNo_Context("잉? 아직 둥지 밖으로 나갈 준비가 안된건가? 자네?");
            Quest24_CONVERSATION6.AddQuestClear_Context("잘~ 가시게~~~ 부디 몸조심하게나~~.^^\n아 참 그리고 기회가 된다면 꼭 '드넓은 초원' 에 일어나고있는 일을 설명해주러 와주게나!!");
            Quest24_CONVERSATION6.AddQuestProgress_Context(". . . ");
            Quest24_CONVERSATION6.m_sQuest_Information_Recommend = ("'늙고 병든 슬라임' 이 당신의 여행에 보탬이 되기위한 무언가를 찾고 있습니다.\n잠시후 다시 말을 걸어 보세요.");
            Quest24_CONVERSATION6.m_sQuest_Information_Process = ("'늙고 병든 슬라임' 이 당신의 여행에 보탬이 되기위한 무언가를 찾고 있습니다.\n잠시후 다시 말을 걸어 보세요.");
            Quest24_CONVERSATION6.m_sQuest_Information_Condition = ("'늙고 병든 슬라임' 이 당신의 여행에 보탬이 되기위한 무언가를 찾고 있습니다.\n잠시후 다시 말을 걸어 보세요.");
            Quest24_CONVERSATION6.m_sQuest_Information_Clear = ("'늙고 병든 슬라임' 이 당신의 여행을 응원합니다.\n자! 떠납시다! '드넓은 초원' 으로!\n기회가 된다면 '늙고 병든 슬라임' 에게 다시 돌아오는것도 잊지 말자구요!");
            Quest24_CONVERSATION6.m_bQuest_Information_Process_Hide = false;
            Quest24_CONVERSATION6.m_ql_Quest_Necessity_Clear.Add(Quest4_GOAWAY_MONSTER1);
            Quest24_CONVERSATION6.m_sRewardSOC = new SOC(1, 0, 0, 1, 0, 0, 0, 0, 0);
            Quest24_CONVERSATION6.m_sRewardSTATUS = new STATUS(0, 0, 3);
            Quest24_CONVERSATION6.m_nRewardGold = 1000;
            Quest24_CONVERSATION6.m_lRewardList_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12000]);
            m_Dictionary_QuestList_CONVERSATION.Add(Quest24_CONVERSATION6.m_nQuest_Code, Quest24_CONVERSATION6);
        }

        // [퀘스트[S1]: 히든퀘스트]\n['작은 바위' 는 잘 있을까요?]
        {
            Quest25_CONVERSATION7.AddQuestDescription_Context(". . . ");
            Quest25_CONVERSATION7.AddQuestDescription_Context(". . . ");
            Quest25_CONVERSATION7.AddQuestDescription_Context(". . . \n\n['큰 바위' 주변에는 고양이도 있고, 돌맹이도 있어 시끌벅적합니다.]");
            Quest25_CONVERSATION7.AddQuestDescription_Context(". . . \n\n[문뜩 '작은 바위' 가 떠오릅니다.]\n[너무나 외로워 보였던 '작은 바위' 의 친구를 찾아줬던 일을 기억합니다.]");
            Quest25_CONVERSATION7.AddQuestDescription_Context(". . . \n\n['작은 바위' 가 잘 있나 확인해 봅시다.]");
            Quest25_CONVERSATION7.AddQuestOk_Context(". . . ");
            Quest25_CONVERSATION7.AddQuestNo_Context(". . . ");
            Quest25_CONVERSATION7.AddQuestClear_Context(". . . \n\n['작은 바위' 는 당신이 자신을 찾아준것에 감사합니다.]\n[그리고 잘 지내고 있다는 듯이 무언가를 건네주었습니다. 아마도 '작은 바위' 의 새로운 친구들이 많이 생겼나보군요.]");
            Quest25_CONVERSATION7.AddQuestProgress_Context(". . . ");
            Quest25_CONVERSATION7.m_sQuest_Information_Recommend = ("'작은 바위' 는 잘 있을까요?");
            Quest25_CONVERSATION7.m_sQuest_Information_Process = ("시끌벅적한 '큰 바위' 를 보고 '작은 바위' 가 잘 있나 궁금해 졌습니다.\n'작은 바위' 를 찾아가 봅시다.");
            Quest25_CONVERSATION7.m_sQuest_Information_Condition = ("시끌벅적한 '큰 바위' 를 보고 '작은 바위' 가 잘 있나 궁금해 졌습니다.\n'작은 바위' 를 찾아가 봅시다.");
            Quest25_CONVERSATION7.m_sQuest_Information_Clear = ("'작은 바위' 곁에는 '맛없어 보이는 버섯' 뿐만이 아니라 다른 생물들도 함께 합니다.\n더이상 '작은 바위' 가 외로울 겨를이 없네요.");
            Quest25_CONVERSATION7.m_lRewardList_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8013]);
            Quest25_CONVERSATION7.m_lRewardList_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8011]);
            Quest25_CONVERSATION7.m_bQuest_Information_Process_Hide = false;
            Quest25_CONVERSATION7.m_ql_Quest_Necessity_Clear.Add(Quest23_COLLECT6);
            Quest25_CONVERSATION7.m_sRewardSOC = new SOC(1, 0, 0, 0, 0, 0, 0, 0, 0);
            Quest25_CONVERSATION7.m_sRewardSTATUS = new STATUS(0, 0, 30);
            Quest25_CONVERSATION7.m_nRewardGold = 0;
            m_Dictionary_QuestList_CONVERSATION.Add(Quest25_CONVERSATION7.m_nQuest_Code, Quest25_CONVERSATION7);
        }

        // [퀘스트[S2]: 서브퀘스트]\n[마지막 인사]
        {
            Quest26_CONVERSATION8.AddQuestDescription_Context("[누군가에게 버림받은 낡은 목검이 있습니다.]");
            Quest26_CONVERSATION8.AddQuestDescription_Context("[특별함이라고는 전혀 찾아볼 수 없는 목검에는 '슬레나르' 라는 이름이 적혀있습니다.]\n[아마도 목검의 주인인듯하군요.]");
            Quest26_CONVERSATION8.AddQuestDescription_Context("['버림받은 목검' 은 '슬레나르' 와 함께했던 시절을 추억하며 자신의 마지막을 보내고 있습니다.]");
            Quest26_CONVERSATION8.AddQuestDescription_Context("['버림받은 목검' 은 '슬레나르' 에게 마지막 인사를 하고 싶어 하는듯 합니다.]");
            Quest26_CONVERSATION8.AddQuestDescription_Context("['슬레나르' 를 찾아가 봅시다.]");
            Quest26_CONVERSATION8.AddQuestOk_Context("['슬레나르' 는 '드넓은 초원 1' 에서 자신이 맡은 의뢰를 수행중입니다.]");
            Quest26_CONVERSATION8.AddQuestNo_Context("[. . .]");
            Quest26_CONVERSATION8.AddQuestClear_Context("아.아.. 기억나는군요..ㅠ\n오래전 제가 모험을 시작할 때 그 목검 이로군요.\n장비의 효율만 따지다보니 그렇게 된 것 같네요..ㅠ\n제가 '버림받은 목검' 을 찾아가 보겠습니다.");
            Quest26_CONVERSATION8.AddQuestProgress_Context(". . . ");
            Quest26_CONVERSATION8.m_sQuest_Information_Recommend = ("금방이라도 부스러질 것 같은 '버림받은 목검' 입니다.");
            Quest26_CONVERSATION8.m_sQuest_Information_Process = ("금방이라도 부스러질 것 같은 '버림받은 목검' 의 주인인 '슬레나르' 를 찾아가 봅시다.\n'버림받는 목검' 의 마지막을 위해...");
            Quest26_CONVERSATION8.m_sQuest_Information_Condition = ("금방이라도 부스러질 것 같은 '버림받은 목검' 의 주인인 '슬레나르' 를 찾아가 봅시다.\n'버림받는 목검' 의 마지막을 위해...");
            Quest26_CONVERSATION8.m_sQuest_Information_Clear = ("'버림받은 목검' 의 마지막을 위해 주인인 '슬레나르' 를 찾아갔습니다.\n'슬레나르' 는 '버림받은 목검' 을 추억하며 생각에 잠겼습니다.");
            Quest26_CONVERSATION8.m_bQuest_Information_Process_Hide = false;
            Quest26_CONVERSATION8.m_sStatus_Necessity_Down.SetSTATUS_LV(6);
            Quest26_CONVERSATION8.m_sRewardSOC = new SOC(1, 0, 0, 1, 0, 0, 0, 0, 0);
            Quest26_CONVERSATION8.m_sRewardSTATUS = new STATUS(0, 0, 30);
            Quest26_CONVERSATION8.m_nRewardGold = 0;
            m_Dictionary_QuestList_CONVERSATION.Add(Quest26_CONVERSATION8.m_nQuest_Code, Quest26_CONVERSATION8);
        }

        // [퀘스트[S4]: 서브퀘스트]\n[마지막 수리]
        {
            Quest27_COLLECT7.AddQuestDescription_Context("아 일전에 '버림받은 목검' 의 부탁을 들어주셔서 감사합니다.\n그 덕에 많은걸 다시 생각해 볼 수 있었습니다.");
            Quest27_COLLECT7.AddQuestDescription_Context("사실 저는 세계 이곳저곳을 떠도는 모험가랍니다.\n모험은 즐거운 일이었습니다. 하고 싶은 것도 하며 돈도 벌수 있었죠.");
            Quest27_COLLECT7.AddQuestDescription_Context("그러나 모험을 하면 할수록 제 한계는 명확히 드러나더군요..\n한계를 돌파하는것은 너무도 힘든 일이기에 이제 조금 쉬고싶어요.....");
            Quest27_COLLECT7.AddQuestDescription_Context("제가 버려두고 왔던 '버림받은 목검' 을 추억하며.... 자신을 다시 돌아볼겁니다!");
            Quest27_COLLECT7.AddQuestDescription_Context("저는 '버림받은 목검' 을 이대로 두고 싶지 않습니다.\n제 스스로를 한번 되돌아보게 만들어준 '버림받은 목검' 을 어떻게든 수리하고 싶습니다!");
            Quest27_COLLECT7.AddQuestDescription_Context("부탁드립니다! '나뭇가지' 5개, '짙은 앤트의 나무가죽' 3개, '부식된 뼈' 3개 를 구해주세요!\n제가 수리해볼테니 재료만 모아주세요!");
            Quest27_COLLECT7.AddQuestOk_Context("감사합니다. 매번 절 이렇게 도와주시다니..");
            Quest27_COLLECT7.AddQuestNo_Context("아 다른 바쁜일이 있으신겁니까?");
            Quest27_COLLECT7.AddQuestProgress_Context("아직 재료를 덜 모으신것 같군요.\n'나뭇가지' 와 '짙은 앤트의 나무가죽' 은 '버림받은 목검' 의 홈을 매꾸고, '부식된 뼈' 를 갈아서 바른 다음 '버림받은 목검' 을 더욱더 단단하게 만들겁니다.");
            Quest27_COLLECT7.AddQuestClear_Context("감사합니다. 덕분에 제 추억을 소중히 간직할 수 있게 되겠네요.^^");
            Quest27_COLLECT7.m_sQuest_Information_Recommend = ("'버림받은 목검' 을 추억하며...");
            Quest27_COLLECT7.m_sQuest_Information_Process = ("'버림받은 목검' 을 추억하며 수리하기 위해 '나뭇가지' 5개, '짙은 앤트의 나무가죽' 5개, '부식된 뼈' 3개 를 '슬레나르' 에게 가져다 주어야 합니다.");
            Quest27_COLLECT7.m_sQuest_Information_Condition = ("준비된 재료를 가지고 '슬레나르' 에게 가봅시다.");
            Quest27_COLLECT7.m_sQuest_Information_Clear = ("'슬레나르' 의 추억이 깃든 '버림받은 목검' 을 수리하는데 도움을 주었습니다.\n과연 수리가 성공했을까요?");
            Quest27_COLLECT7.AddCondition(0014, 5, 0);
            Quest27_COLLECT7.AddCondition(0003, 3, 0);
            Quest27_COLLECT7.AddCondition(0011, 3, 0);
            Quest27_COLLECT7.m_bQuest_Information_Process_Hide = false;
            Quest27_COLLECT7.m_ql_Quest_Necessity_Clear.Add(Quest26_CONVERSATION8);
            Quest27_COLLECT7.m_sRewardSOC = new SOC(1, 0, 0, 1, 0, 0, 0, 0, 0);
            Quest27_COLLECT7.m_sRewardSTATUS = new STATUS(0, 0, 40);
            Quest27_COLLECT7.m_nRewardGold = 300;
            m_Dictionary_QuestList_COLLECT.Add(Quest27_COLLECT7.m_nQuest_Code, Quest27_COLLECT7);
        }

        // [퀘스트[S1]: 서브퀘스트]\n['슬레나르' 와 함께 1]
        {
            Quest28_KILL_MONSTER7.AddQuestDescription_Context("오 안녕하세요 날씨가 좋네요.");
            Quest28_KILL_MONSTER7.AddQuestDescription_Context("저는 요즘 제 자신을 돌아보며 할 일을 찾고 있답니다.");
            Quest28_KILL_MONSTER7.AddQuestDescription_Context("아 참고로 '버림받은 목검' 의 수리는 실패했습니다.ㅠㅠ 너무 상태가 좋지 않더군요.\n그래서 '버림받은 목검' 의 일부만 떼어내어 소중히 간직하고 있답니다.\n항상 감사하게 생각하고 있다구요.");
            Quest28_KILL_MONSTER7.AddQuestDescription_Context("최근에는 그동안의 모험가 경력을 인정받아 '드넓은 초원' 전역의 '수풀' 제거 총괄직을 맡았답니다.\n거리가 깨끗해지니 마음이 편안하더군요. 제 천직을 찾은것 같아요. 이대로 '드넓은 초원' 에 정착해 농사나 지을까~ 하고 있습니다.ㅎㅎ");
            Quest28_KILL_MONSTER7.AddQuestDescription_Context("정말 괜찮은 일이라 그런데 같이 '수풀' 제거.. 하실래요..?\n보수도 나름 짭짤~ 하고 어떠세요?");
            Quest28_KILL_MONSTER7.AddQuestOk_Context("아 역시나 하실줄 알았다구요.\n'수풀' 20개만 제거해 주세요.ㅎㅎ\n'수풀' 은 꾸준한 개체수 관리가 필요하답니다.");
            Quest28_KILL_MONSTER7.AddQuestNo_Context("따로 할 일이 있으신건가요? 아쉽네요..");
            Quest28_KILL_MONSTER7.AddQuestClear_Context("오 이 많은걸 이렇게 빨리 해내시다니... 재능이 있으시군요?ㅎ");
            Quest28_KILL_MONSTER7.AddQuestProgress_Context("어.. 아직 '수풀' 20개를 전부다 제거하지 못한것 같아요.");
            Quest28_KILL_MONSTER7.m_sQuest_Information_Recommend = ("거리가 깨끗해지니 마음이 편안하더군요. 제 천직을 찾은것 같아요.\n어때요? 함께 하시겠습니까?");
            Quest28_KILL_MONSTER7.m_sQuest_Information_Process = ("'드넓은 초원' 전역의 '수풀' 제거 총괄직을 새로 맡은 '슬레나르' 의 부탁으로 '수풀' 20개를 제거해야합니다.");
            Quest28_KILL_MONSTER7.m_sQuest_Information_Condition = ("'수풀' 20개를 제거했습니다. '슬레나르' 에게 돌아갑시다.");
            Quest28_KILL_MONSTER7.m_sQuest_Information_Clear = ("'슬레나르' 의 새로운 도전을 위한 첫번째 부탁을 들어주었습니다.\n'수풀' 을 20개 제거했습니다.\n'슬레나르' 의 새로운 도전을 응원했습니다.");
            Quest28_KILL_MONSTER7.AddCondition(0008, 20, 0);
            Quest28_KILL_MONSTER7.m_bQuest_Information_Process_Hide = false;
            Quest28_KILL_MONSTER7.m_ql_Quest_Necessity_Clear.Add(Quest27_COLLECT7);
            Quest28_KILL_MONSTER7.m_sRewardSOC = new SOC(1, 0, 0, 1, 0, 0, 0, 0, 0);
            Quest28_KILL_MONSTER7.m_sRewardSTATUS = new STATUS(0, 0, 20); // 10
            Quest28_KILL_MONSTER7.m_nRewardGold = 300;
            m_Dictionary_QuestList_KILL_MONSTER.Add(Quest28_KILL_MONSTER7.m_nQuest_Code, Quest28_KILL_MONSTER7);
        }

        // [퀘스트[S1]: 서브퀘스트]\n['슬레나르' 와 함께 2]
        {
            Quest29_CONVERSATION9.AddQuestDescription_Context("또 오셨군요.");
            Quest29_CONVERSATION9.AddQuestDescription_Context("도와주신 덕분에 잘 지내고 있습니다.\n최근에는 '드넓은 초원' 전역의 '수풀' 제거 뿐만 아니라 다른 가벼운 의뢰도 맡아서 해보고 있어요. 그동안 너무 빡빡하게만 살아왔더군요. 이렇게 재미있는 의뢰도 많은데.ㅎㅎ");
            Quest29_CONVERSATION9.AddQuestDescription_Context("그런데 한가지 문제가 있어요.");
            Quest29_CONVERSATION9.AddQuestDescription_Context("제가 착각을 해서 까먹은게 있거든요... 오늘은 제 스승님의 기일이랍니다. 그래서 한번 찾아봬야 될 것 같아요.");
            Quest29_CONVERSATION9.AddQuestDescription_Context("하지만 제가 맡은 의뢰 중에 신선식재료 배달 의뢰가 있는데 오늘 중으로 배달해야한단 말이죠...");
            Quest29_CONVERSATION9.AddQuestDescription_Context("부탁드립니다! 제가 오늘중으로는 영~ 시간이 안날것 같아요.. 배달지까지는 멀지 않으니 저를 좀 도와주시겠습니까?\n의뢰 보상은 모두 드리겠습니다.");
            Quest29_CONVERSATION9.AddQuestOk_Context("감사합니다. 매번 절 이렇게 도와주시다니..\n이 '신선한 아보카도' 5개를 '드넓은 초원' 의 '청량한 달빛 마을' 에 있는 '골드 타임 슬라임' 에게 배달해 주시겠습니까?\n다시 말을 걸어 주시면 '신선한 아보카도' 를 5개 드리겠습니다.");
            Quest29_CONVERSATION9.AddQuestNo_Context("따로 할 일이 있으신건가요? 큰일이네요..");
            Quest29_CONVERSATION9.AddQuestClear_Context("여기 '신선한 아보카도' 5개 입니다. 잘부탁드려요.ㅎ");
            Quest29_CONVERSATION9.AddQuestProgress_Context(". . . ");
            Quest29_CONVERSATION9.m_sQuest_Information_Recommend = ("으으.. 깜빡했다..\n저 대신 제 의뢰를 수행해 주실 수 있으신가요? 까먹은 일이 있어서요..");
            Quest29_CONVERSATION9.m_sQuest_Information_Process = ("스승님의 무덤을 찾아가야 하는 '슬레나르' 대신 배달 의뢰를 수행합시다.\n'슬레나르' 에게 다시 말을 걸어 '신선한 아보카도' 5개를 받으세요.");
            Quest29_CONVERSATION9.m_sQuest_Information_Condition = ("스승님의 무덤을 찾아가야 하는 '슬레나르' 대신 배달 의뢰를 수행합시다.\n'슬레나르' 에게 다시 말을 걸어 '신선한 아보카도' 5개를 받으세요.");
            Quest29_CONVERSATION9.m_sQuest_Information_Clear = ("스승님의 무덤을 찾아가야 하는 '슬레나르' 대신 배달 의뢰를 수행하기로 했습니다.\n'신선한 아보카도' 5개를 받았습니다.");
            Quest29_CONVERSATION9.m_bQuest_Information_Process_Hide = false;
            Quest29_CONVERSATION9.m_ql_Quest_Necessity_Clear.Add(Quest28_KILL_MONSTER7);
            Quest29_CONVERSATION9.m_sRewardSOC = new SOC(1, 0, 0, 1, 0, 0, 0, 0, 0);
            Quest29_CONVERSATION9.m_sRewardSTATUS = new STATUS(0, 0, 1);
            Quest29_CONVERSATION9.m_nRewardGold = 0;
            Quest29_CONVERSATION9.m_lRewardList_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8010]);
            Quest29_CONVERSATION9.m_lRewardList_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8010]);
            Quest29_CONVERSATION9.m_lRewardList_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8010]);
            Quest29_CONVERSATION9.m_lRewardList_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8010]);
            Quest29_CONVERSATION9.m_lRewardList_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8010]);
            m_Dictionary_QuestList_CONVERSATION.Add(Quest29_CONVERSATION9.m_nQuest_Code, Quest29_CONVERSATION9);
        }

        // [퀘스트[S1]: 서브퀘스트]\n[신속 정확한 배달]
        {
            Quest30_COLLECT8.AddQuestDescription_Context("좋습니다! 배달 의뢰도 어찌저찌 해결했고 저도 이만 스승님의 무덤에 잘 다녀올 수 있겠군요!\n신속하고 정확한 배달! 부탁드립니다!");
            Quest30_COLLECT8.AddQuestOk_Context("정말 감사합니다!\n\n['드넓은 초원' 의 '청량한 달빛 마을' 에 있는 '골드 타임 슬라임' 에게 '신선한 아보카도' 5개를 배달합시다.]\n[배달을 하지 않고 '신선한 아보카도' 를 팔거나 먹어도 됩니다만, 그렇게 된다면 퀘스트를 클리어 할 수 없게됩니다.]\n['슬레나르' 가 책임지겠죠 뭐]");
            Quest30_COLLECT8.AddQuestNo_Context("이미 물건도 받아가셨으면서!!! 먹튀를?!");
            Quest30_COLLECT8.AddQuestProgress_Context("'신선한 아보카도'.......");
            Quest30_COLLECT8.AddQuestClear_Context("아~ '신선한 아보카도' 5개로군요. 신속하고 정확하군요. 훌륭합니다.\n'슬레나르' 씨에게는 제가 말해놓겠습니다. 여기 의뢰 보상입니다.");
            Quest30_COLLECT8.m_sQuest_Information_Recommend = ("감사합니다! 그럼 잘 다녀오겠습니다! 모험가님도 잘 다녀오세요~");
            Quest30_COLLECT8.m_sQuest_Information_Process = ("스승님의 무덤을 찾아가야 하는 '슬레나르' 대신 배달 의뢰를 수행합시다.\n'신선한 아보카도' 5개를 '드넓은 초원' 의 '청량한 달빛 마을' 에 있는 '골드 타임 슬라임' 에게 배달해야 합니다.\n어라 '신선한 아보카도' 가 어디갔지..? 뭐 알아서 하시길.");
            Quest30_COLLECT8.m_sQuest_Information_Condition = ("스승님의 무덤을 찾아가야 하는 '슬레나르' 대신 배달 의뢰를 수행합시다.\n'신선한 아보카도' 5개를 '드넓은 초원' 의 '청량한 달빛 마을' 에 있는 '골드 타임 슬라임' 에게 배달해야 합니다.");
            Quest30_COLLECT8.m_sQuest_Information_Clear = ("스승님의 무덤을 찾아가야 하는 '슬레나르' 대신 배달 의뢰를 수행했습니다.\n'신선한 아보카도' 5개를 '드넓은 초원' 의 '청량한 달빛 마을' 에 있는 '골드 타임 슬라임' 에게 배달했습니다.\n덕분에 '슬레나르' 는 스승님을 잘 뵙고 왔습니다.");
            Quest30_COLLECT8.AddCondition(8010, 5, 0);
            Quest30_COLLECT8.m_bQuest_Information_Process_Hide = false;
            Quest30_COLLECT8.m_ql_Quest_Necessity_Clear.Add(Quest29_CONVERSATION9);
            Quest30_COLLECT8.m_sRewardSOC = new SOC(1, 0, 0, 1, 0, 0, 0, 0, 0);
            Quest30_COLLECT8.m_sRewardSTATUS = new STATUS(0, 0, 10);
            Quest30_COLLECT8.m_nRewardGold = 300;
            m_Dictionary_QuestList_COLLECT.Add(Quest30_COLLECT8.m_nQuest_Code, Quest30_COLLECT8);
        }

        // [퀘스트[S3]: 히든퀘스트]\n[전설의 도라지를 찾아서 1]
        {
            Quest31_CONVERSATION10.AddQuestDescription_Context("에구에구 허리야~");
            Quest31_CONVERSATION10.AddQuestDescription_Context("비가올라카는강 온몸의 관절이 다아프나안카나!");
            Quest31_CONVERSATION10.AddQuestDescription_Context("소문에 의하면 '깊디깊은숲' 어딘가에는 어떤 관절병이든 쉽게 치료되는 전설의 도라지가 있다카든데~");
            Quest31_CONVERSATION10.AddQuestDescription_Context("전설의 도라지는 말 그대로 전설적인 도라지라 안카나?!\n마 그래가 구할 수 있는지 없는지는 모르겠는데 그래도 함 알아봐 줄 수 있나?!");
            Quest31_CONVERSATION10.AddQuestDescription_Context("그것 좀 함 알아봐도잉?\n내 별 기대는 안하지마는 그래도 혹시나 진짜 전설의 도라지가 있을수도 있다 아이가~\n부탁한데이~~~");
            Quest31_CONVERSATION10.AddQuestOk_Context("고맙데이~~\n\n[일단 전설의 도라지에 대한 행방을 아는 사람을 찾아봅시다.]");
            Quest31_CONVERSATION10.AddQuestNo_Context("마 니도 글체?\n전설의 도라지라니 장난하는것도 아이고!!");
            Quest31_CONVERSATION10.AddQuestClear_Context("이야~ 젊은이 어서오게나.^^ 오랜만에 보는구먼~~ 그간 잘 있었나?\n왜이리 늦게왔어~!\n안그래도 요즘 자네 소식이 좀 들리는것 같더군~~.ㅎ\n일단 오랜만이니 이것부터 좀 들게나.^^");
            Quest31_CONVERSATION10.AddQuestProgress_Context("니 아직도 안알아봤나!!! 퍼뜩 다녀오라 안카나!");
            Quest31_CONVERSATION10.m_sQuest_Information_Recommend = ("어떤 관절병이든 쉽게 치료되는 전설의 도라지가 있다카든데~ 전설의 도라지에대해 뭘 좀 알고있나!?");
            Quest31_CONVERSATION10.m_sQuest_Information_Process = ("어떤 관절병이든 쉽게 치료되는 전설의 도라지가 있다카든데~ 전설의 도라지에대해 뭘 좀 알고있는아한테 가가 함 물어봐라!");
            Quest31_CONVERSATION10.m_sQuest_Information_Condition = ("어떤 관절병이든 쉽게 치료되는 전설의 도라지가 있다카든데~ 전설의 도라지에대해 뭘 좀 알고있는아한테 가가 함 물어봐라!");
            Quest31_CONVERSATION10.m_sQuest_Information_Clear = ("'슬라임 할머니' 의 부탁을 받고 오랜만에 전설의 도라지를 아는 '늙고 병든 슬라임' 을 찾아갔습니다.\n'늙고 병든 슬라임' 이 당신을 따뜻하게 반겨주며 약간의 음식을 줬습니다.");
            Quest31_CONVERSATION10.m_bQuest_Information_Process_Hide = true;
            Quest31_CONVERSATION10.m_sStatus_Necessity_Down.SetSTATUS_LV(7);
            Quest31_CONVERSATION10.m_sRewardSOC = new SOC(1, 0, 0, 1, 0, 0, 0, 0, 0);
            Quest31_CONVERSATION10.m_sRewardSTATUS = new STATUS(0, 0, 30);
            Quest31_CONVERSATION10.m_nRewardGold = 0;
            Quest31_CONVERSATION10.m_lRewardList_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8014]);
            Quest31_CONVERSATION10.m_lRewardList_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9001]);
            Quest31_CONVERSATION10.m_lRewardList_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8002]);
            m_Dictionary_QuestList_CONVERSATION.Add(Quest31_CONVERSATION10.m_nQuest_Code, Quest31_CONVERSATION10);
        }

        // [퀘스트[S3]: 히든퀘스트]\n[전설의 도라지를 찾아서 2]
        {
            Quest32_COLLECT9.AddQuestDescription_Context("오랜만에 들렀으니 좀 쉬다 가게나.ㅎㅎ\n뭔가 할말이라도 더 있는겐가?");
            Quest32_COLLECT9.AddQuestDescription_Context("['늙고 병든 슬라임' 에게 '슬라임 할머니' 의 부탁을 받고 왔다고 말해줬습니다.]");
            Quest32_COLLECT9.AddQuestDescription_Context("아~ '슬라임 할머니' 와는 '달빛 노인정' 에서 몇번 만난적이 있다네~~");
            Quest32_COLLECT9.AddQuestDescription_Context("'슬라임 할머니' 도 하나있는 아들을 혼자 키우느라 고생 참 많이 했지~~ 이름이 '스페이슬라임' 이라고 했던가..?");
            Quest32_COLLECT9.AddQuestDescription_Context("아무튼 그자의 부탁으로 왔다면 나도 힘좀 써야지!!");
            Quest32_COLLECT9.AddQuestDescription_Context("전설의 도라지라....\n흠......\n음...\n\n[당신은 '늙고 병든 슬라임' 과 함께 전설의 도라지에 대해 생각하고 또 생각했습니다.]");
            Quest32_COLLECT9.AddQuestDescription_Context("내 '깊디깊은숲' 에서 산 지도 꽤나 많은 시간이 흘렀지.\n그럼에도 전설의 도라지 라는건 한번도 본 적이 없다네..");
            Quest32_COLLECT9.AddQuestDescription_Context("!!!");
            Quest32_COLLECT9.AddQuestDescription_Context("!!!!!!");
            Quest32_COLLECT9.AddQuestDescription_Context("!!!!!!!!!");
            Quest32_COLLECT9.AddQuestDescription_Context("혹시 전설의 도라지가 내 '100년 묵은 도라지' 를 말하는게 아닌가?!?!?!");
            Quest32_COLLECT9.AddQuestDescription_Context("[?!]");
            Quest32_COLLECT9.AddQuestDescription_Context("엇! 맞는것 같군.ㅎㅎ\n일전에 '달빛 노인정' 에서 내가 '100년 묵은 도라지' 를 달여먹고 허리가 좋아졌다고 말한적이 있다네~!\n설마 전설의 도라지가 '100년 묵은 도라지' 였을줄이야...");
            Quest32_COLLECT9.AddQuestDescription_Context("어서 '100년 묵은 도라지' 를 구해서 '슬라임 할머니' 에게 가보게나.ㅎㅎ\n일전에도 '100년 묵은 도라지' 를 구해 봤으니 방법은 알거라 믿네ㅎㅎ");
            Quest32_COLLECT9.AddQuestOk_Context("그려그려~ 자네 일이 쉽게 해결되어 다행이구먼~\n다음에 또 들리게나~~");
            Quest32_COLLECT9.AddQuestNo_Context("어 그래~ 자네 마음대로 좀 더 쉬고 가게나~~");
            Quest32_COLLECT9.AddQuestProgress_Context("...");
            Quest32_COLLECT9.AddQuestClear_Context("잉? 이게 뭐꼬? 이게 전설의 도라지가?!\n이야 참말로 고맙데이~~~ 내 함 달여먹어 보꾸마~~\n부탁 들어준기 고마워가 이것저것 챙기줄테니 단단히 챙겨가그라~ 알았제~~\n\n['슬라임 할머니' 가 이것저것 챙겨주셨습니다.]");
            Quest32_COLLECT9.m_sQuest_Information_Recommend = ("오랜만이군!!! 정말 오랜만이야!~~\n잘 지내고 있었는가?~\n나야 자네 덕분에 회춘했다네 ㅎㅎ");
            Quest32_COLLECT9.m_sQuest_Information_Process = ("'슬라임 할머니' 의 관절병을 치료하기 위해 전설의 도라지라고 알려진 '100년 묵은 도라지' 를 1개 구해야 합니다.");
            Quest32_COLLECT9.m_sQuest_Information_Condition = ("'100년 묵은 도라지' 1개를 구했습니다.\n이제 '슬라임 할머니' 에게 가져가야 합니다.");
            Quest32_COLLECT9.m_sQuest_Information_Clear = ("이야 니덕에 쪼까 나아진것같데이~ 참말로 고맙데이~~~\n'슬라임 할머니' 의 관절병을 치료하기 위해 전설의 도라지라고 알려진 '100년 묵은 도라지' 1개를 구해줬습니다.\n그 과정에서 오랜만에 '늙고 병든 슬라임' 을 만나 그간 못 나눴던 이야기를 하며 즐거운 시간을 보냈습니다.");
            Quest32_COLLECT9.AddCondition(0016, 1, 0);
            Quest32_COLLECT9.m_bQuest_Information_Process_Hide = true;
            Quest32_COLLECT9.m_ql_Quest_Necessity_Clear.Add(Quest31_CONVERSATION10);
            Quest32_COLLECT9.m_sRewardSOC = new SOC(1, 0, 0, 2, 0, 0, 0, 0, 0);
            Quest32_COLLECT9.m_sRewardSTATUS = new STATUS(0, 0, 30);
            Quest32_COLLECT9.m_nRewardGold = 500;
            Quest32_COLLECT9.m_lRewardList_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[4007]);
            Quest32_COLLECT9.m_lRewardList_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8008]);
            Quest32_COLLECT9.m_lRewardList_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8009]);
            Quest32_COLLECT9.m_lRewardList_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8014]);
            Quest32_COLLECT9.m_lRewardList_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9001]);
            m_Dictionary_QuestList_COLLECT.Add(Quest32_COLLECT9.m_nQuest_Code, Quest32_COLLECT9);
        }

        // [퀘스트[S1]: 서브퀘스트]\n[대신 장좀 봐돌라 안카나?!]
        {
            Quest33_CONVERSATION11.AddQuestDescription_Context("에구에구 관절이야~~ 비가올라 카는강?!");
            Quest33_CONVERSATION11.AddQuestDescription_Context("관절이 아파가 아무것도 못하겠다 카이!");
            Quest33_CONVERSATION11.AddQuestDescription_Context("장보러 마을에 가야되는데 못가게되뿟다!! 우짜면 좋을꼬!!");
            Quest33_CONVERSATION11.AddQuestDescription_Context("이봐래이 니 할거없으면 내 대신 장좀 봐오니라!");
            Quest33_CONVERSATION11.AddQuestOk_Context("그래 고맙데이~\n내 돈은 챙기주꾸마 쪼매만 있어봐래이\n\n[잠시후 다시 '슬라임 할머니' 에게 말을 걸어봅시다.]");
            Quest33_CONVERSATION11.AddQuestNo_Context("아 할끼 있다꼬? 그람 우짤수없제....... . . .  .   .    .     .");
            Quest33_CONVERSATION11.AddQuestClear_Context("자 여있다 아나! 300 골드!\n\n[잠시후 다시 '슬라임 할머니' 에게 말을 걸어봅시다.]");
            Quest33_CONVERSATION11.AddQuestProgress_Context(". . .");
            Quest33_CONVERSATION11.m_sQuest_Information_Recommend = ("에구에구 관절이야~~\n관절이 아파가 아무것도 못하겠다 카이!\n장봐야되는데 큰일나뿟따!!");
            Quest33_CONVERSATION11.m_sQuest_Information_Process = ("'슬라임 할머니' 가 장보기를 위한 돈을 챙기고 있습니다. 잠시후 다시 '슬라임 할머니' 에게 말을 걸어 돈을 받으세요.");
            Quest33_CONVERSATION11.m_sQuest_Information_Condition = ("'슬라임 할머니' 가 장보기를 위한 돈을 챙기고 있습니다. 잠시후 다시 '슬라임 할머니' 에게 말을 걸어 돈을 받으세요.");
            Quest33_CONVERSATION11.m_sQuest_Information_Clear = ("'슬라임 할머니' 대신 장을 보기 위해 돈을 받았습니다. 잠시후 다시 '슬라임 할머니' 에게 말을 걸어 사와야 할것을 확인하세요.");
            Quest33_CONVERSATION11.m_bQuest_Information_Process_Hide = false;
            Quest33_CONVERSATION11.m_sRewardSOC = new SOC(1, 0, 0, 1, 0, 0, 0, 0, 0);
            Quest33_CONVERSATION11.m_sRewardSTATUS = new STATUS(0, 0, 1);
            Quest33_CONVERSATION11.m_nRewardGold = 300;
            m_Dictionary_QuestList_CONVERSATION.Add(Quest33_CONVERSATION11.m_nQuest_Code, Quest33_CONVERSATION11);
        }

        // [퀘스트[S3]: 서브퀘스트]\n[장보기]
        {
            Quest34_COLLECT10.AddQuestDescription_Context("그래 돈은 단디 챙겨라잉?! 잃가도 다시 안준다 안카나!!");
            Quest34_COLLECT10.AddQuestDescription_Context("그라믄 저기저 '청량한 달빛 마을' 에 가가 '프리미엄 감자' 하고 '짭짤이 토마토' 3개씩 사온나!");
            Quest34_COLLECT10.AddQuestDescription_Context("단디 들었제? '프리미엄 감자' 하고 '짭잘이 토마토' 3개씩이다잉?!");
            Quest34_COLLECT10.AddQuestOk_Context("그래 그라믄 잘갔다 오니라~\n중간에 이상한데서 돈 쓰지말고 알긋제?!\n'청량한 달빛 마을' 이다잉?! 거 근처에 상인들이 많다 아이가!");
            Quest34_COLLECT10.AddQuestNo_Context("이자슥이! 돈받고 입 싸악 닫아뿟네!!!\n아이고 동네 사람들!!!");
            Quest34_COLLECT10.AddQuestProgress_Context("뭐하노 아직도 안갔다왔나?! 후딱 갔다 오니라!");
            Quest34_COLLECT10.AddQuestClear_Context("참말로 고맙데이~~~\n짜슥이 멍청~ 하이 생기가 제법이데이~~\n쪼끔만 기다려 보그라\n니도 맛은 봐야된다 아이겠나?!~~ 하나만 묵고 다른 하나는 꼭 남겨놔래이!!\n\n['슬라임 할머니' 가 그자리에서 말도 안되는 솜씨로 '네가지 맛 감자 롤' 을 요리하고 건네주었습니다.]\n[군침이 싸 ~ 악 도네요.]");
            Quest34_COLLECT10.m_sQuest_Information_Recommend = ("단디 하그라!\n맹~ 하이 굴면 안된데이!!");
            Quest34_COLLECT10.m_sQuest_Information_Process = ("'청량한 달빛 마을' 에 있는 상인들 한테서 '프리미엄 감자' 하고 '짭짤이 토마토' 3개씩 사오니라!!");
            Quest34_COLLECT10.m_sQuest_Information_Condition = ("식재료를 모두 구했으니 이제 '슬라임 할머니' 에게 돌아갑시다.");
            Quest34_COLLECT10.m_sQuest_Information_Clear = ("짜슥이 멍청~ 하이 생기가 제법이데이~~\n'청량한 달빛 마을' 의 여러 상인들에게서 구한 식재료인 '프리미엄 감자' 와 '짭짤이 토마토' 3개를 '슬라임 할머니' 에게 건네 주었습니다.\n'슬라임 할머니' 가 그자리에서 말도 안되는 솜씨로 '네가지 맛 감자 롤' 을 요리하고 건네주었습니다.\n군침이 싸 ~ 악 도네요.");
            Quest34_COLLECT10.AddCondition(8021, 3, 0);
            Quest34_COLLECT10.AddCondition(8022, 3, 0);
            Quest34_COLLECT10.m_bQuest_Information_Process_Hide = false;
            Quest34_COLLECT10.m_ql_Quest_Necessity_Clear.Add(Quest33_CONVERSATION11);
            Quest34_COLLECT10.m_sRewardSOC = new SOC(1, 1, 0, 2, 0, 1, 0, 0, 0);
            Quest34_COLLECT10.m_sRewardSTATUS = new STATUS(0, 0, 30);
            Quest34_COLLECT10.m_nRewardGold = 0;
            Quest34_COLLECT10.m_lRewardList_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8023]);
            Quest34_COLLECT10.m_lRewardList_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8023]);
            m_Dictionary_QuestList_COLLECT.Add(Quest34_COLLECT10.m_nQuest_Code, Quest34_COLLECT10);
        }

        // [퀘스트[S2]: 서브퀘스트]\n[소울 푸드]
        {
            Quest35_COLLECT11.AddQuestDescription_Context("그래! 내가 만든 '네가지 맛 감자 롤' 은 무봤나?!");
            Quest35_COLLECT11.AddQuestDescription_Context("맛있제?!");
            Quest35_COLLECT11.AddQuestDescription_Context("얻어 뭇으면 또 일해야안되나!");
            Quest35_COLLECT11.AddQuestDescription_Context("내 니한테 줬던 '네가지 맛 감자 롤' 하나 더 가지고 있제?!\n'드넓은 초원' 어딘가에 있는 울 아들내미한테 좀 전해도!");
            Quest35_COLLECT11.AddQuestDescription_Context("울 아들내미가 젤로 좋아하던 음식이니깐 부탁좀 한데이~");
            Quest35_COLLECT11.AddQuestDescription_Context("우리 아들내미 이름은 '스페이슬라임' 이데이 똑디 들었제?!");
            Quest35_COLLECT11.AddQuestOk_Context("오냐 '네가지 맛 감자 롤' 이 식기전에 다녀오니라!");
            Quest35_COLLECT11.AddQuestNo_Context("이자슥이! 먹을거 무뿌고 입 싸악 닫아뿟네!!!\n아이고 동네 사람들!!!!!!");
            Quest35_COLLECT11.AddQuestProgress_Context(". . . 후딱!!");
            Quest35_COLLECT11.AddQuestClear_Context("아니 이건 울 어머니의 '네가지 맛 감자 롤'!!!!\n진짜 고맙심더... 흙흙...\n역시 울 어머니 손맛이 최고네요~~^^\n\n['스페이슬라임' 이 매우 기뻐합니다.]");
            Quest35_COLLECT11.m_sQuest_Information_Recommend = ("울 아들내미가 젤로 좋아하던 음식..");
            Quest35_COLLECT11.m_sQuest_Information_Process = ("내가 만든 '네가지 맛 감자 롤' 을 '드넓은 초원' 어딘가에 있는 울 아들내미한테 좀 전해도! 아들 이름은 '스페이슬라임' 이데이~");
            Quest35_COLLECT11.m_sQuest_Information_Condition = ("내가 만든 '네가지 맛 감자 롤' 을 '드넓은 초원' 어딘가에 있는 울 아들내미한테 좀 전해도! 아들 이름은 '스페이슬라임' 이데이~");
            Quest35_COLLECT11.m_sQuest_Information_Clear = ("아니 이건 울 어머니의 '네가지 맛 감자 롤'!!!!\n진짜 고맙심더... 흙흙...\n역시 울 어머니 손맛이 최고네요~~^^\n'스페이슬라임' 이 매우 기뻐합니다.");
            Quest35_COLLECT11.AddCondition(8023, 1, 0);
            Quest35_COLLECT11.m_bQuest_Information_Process_Hide = false;
            Quest35_COLLECT11.m_ql_Quest_Necessity_Clear.Add(Quest34_COLLECT10);
            Quest35_COLLECT11.m_sRewardSOC = new SOC(1, 0, 0, 1, 0, 0, 0, 0, 0);
            Quest35_COLLECT11.m_sRewardSTATUS = new STATUS(0, 0, 30);
            Quest35_COLLECT11.m_nRewardGold = 100;
            m_Dictionary_QuestList_COLLECT.Add(Quest35_COLLECT11.m_nQuest_Code, Quest35_COLLECT11);
        }

        // [퀘스트[S3]: 히든퀘스트]\n[가정식]
        {
            Quest36_KILL_MONSTER8.AddQuestDescription_Context("야야~ 니가 전설의 도라지('100년 묵은 도라지')를 구해준 덕분에 관절병이 싸악~~~ 나았다 아이가~");
            Quest36_KILL_MONSTER8.AddQuestDescription_Context("그리고 울 아들내미한테 '네가지 맛 감자 롤' 해주는것도 도와줘가 참말로 고맙데이~~\n니도 맛있게 뭇다 아이가?!");
            Quest36_KILL_MONSTER8.AddQuestDescription_Context("내 부탁을 두개나 들어줘가 을매나 고마운지 모른다카이~~");
            Quest36_KILL_MONSTER8.AddQuestDescription_Context("따른기 아이고 내가 이번에 관절도 싸악~~~ 나아뿟고 내 요리를 맛있게 먹어주는 니를 보고 느낀게 있다안카나!!");
            Quest36_KILL_MONSTER8.AddQuestDescription_Context("고마 나도 식당이나 차릴라 칸다!");
            Quest36_KILL_MONSTER8.AddQuestDescription_Context("안그래도 '드넓은 초원' 에 모험가들이 많은데 임마 이것들 밥은 잘 묵고 댕기는지 모르겠다!!");
            Quest36_KILL_MONSTER8.AddQuestDescription_Context("그래가 내가 챙기주야겠다!!");
            Quest36_KILL_MONSTER8.AddQuestDescription_Context("근데 한가지 문제가 있데이..");
            Quest36_KILL_MONSTER8.AddQuestDescription_Context("인자 내가 '청량한 달빛 마을' 까지 가가 식재료 사는건 되겠는데 가는길이 험하다 아이가!!");
            Quest36_KILL_MONSTER8.AddQuestDescription_Context("마지막으로 '수풀' 하고 '가시덤불' 좀 없애도!\n죽겠데이!");
            Quest36_KILL_MONSTER8.AddQuestDescription_Context("내 이것까지만 해준다 카면 니한테 <color=red>특별한 요리들을 팔아주겠다 안하나!!!</color>");
            Quest36_KILL_MONSTER8.AddQuestOk_Context("그래 잘다녀오니라~\n따 ~ 끈 따 ~ 끈 한 요리가 니를 기다리고 있데이~!");
            Quest36_KILL_MONSTER8.AddQuestNo_Context("싫음 마는기라!");
            Quest36_KILL_MONSTER8.AddQuestClear_Context("그래 잘다녀왔나?!\n이제 내도 안심하고 나다닐수 잇겠데이~~~고맙데이!!!\n자 아나 이거 줄테이 맛 함 보그라~\n담부턴 사무래이~~~!\n\n<color=red>['슬라임 할머니의 식당' 이 이용 가능해 집니다.]</color>");
            Quest36_KILL_MONSTER8.AddQuestProgress_Context("다했나? 아인거같은데?!");
            Quest36_KILL_MONSTER8.m_sQuest_Information_Recommend = ("야야~ '청량한 달빛 마을' 까지 가는길이 와이래됬노!\n니가좀 치아바라!");
            Quest36_KILL_MONSTER8.m_sQuest_Information_Process = ("'슬라임 할머니' 가 혼자 '청량한 달빛 마을' 로 안전하게 가기 위해 장애물 제거를 부탁했습니다.");
            Quest36_KILL_MONSTER8.m_sQuest_Information_Condition = ("장애물을 모두 제거 했으니 '슬라임 할머니' 에게 돌아갑시다.");
            Quest36_KILL_MONSTER8.m_sQuest_Information_Clear = ("이제 내도 안심하고 나다닐수 잇겠데이~~~고맙데이!!!\n자 아나 이거 줄테이 맛 함 보그라~ 담부턴 사무래이~~~!\n'슬라임 할머니' 가 식재료를 사기 위해 '청량한 달빛 마을' 로 가는길을 청소 했습니다.\n'슬라임 할머니의 식당' 이 이용 가능해 집니다.");
            Quest36_KILL_MONSTER8.AddCondition(0008, 5, 0); // 5, 0
            Quest36_KILL_MONSTER8.AddCondition(0010, 5, 0); // 5, 0
            Quest36_KILL_MONSTER8.m_ql_Quest_Necessity_Clear.Add(Quest32_COLLECT9);
            Quest36_KILL_MONSTER8.m_ql_Quest_Necessity_Clear.Add(Quest35_COLLECT11);
            Quest36_KILL_MONSTER8.m_sRewardSOC = new SOC(3, 0, 0, 2, 0, 0, 0, 0, 0);
            Quest36_KILL_MONSTER8.m_sRewardSTATUS = new STATUS(0, 0, 30);
            Quest36_KILL_MONSTER8.m_nRewardGold = 500;
            Quest36_KILL_MONSTER8.m_lRewardList_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8029]);
            Quest36_KILL_MONSTER8.m_lRewardList_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9004]);
            Quest36_KILL_MONSTER8.m_lRewardList_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9005]);
            Quest36_KILL_MONSTER8.m_lRewardList_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[9006]);
            m_Dictionary_QuestList_KILL_MONSTER.Add(Quest36_KILL_MONSTER8.m_nQuest_Code, Quest36_KILL_MONSTER8);
        }

        // [퀘스트[S2]: 서브퀘스트]\n[내 도끼 어디갔어!!]
        {
            Quest37_COLLECT12.AddQuestDescription_Context("킁! 이봐");
            Quest37_COLLECT12.AddQuestDescription_Context("내 오른손 전용 도끼 어디서 못봤나? ㅠㅠ");
            Quest37_COLLECT12.AddQuestDescription_Context("아잇 진짜 큰일이네 이거 ㅠ\n중급 용병씩이나 되는 내가 자기 무기를 잃어버리다니...");
            Quest37_COLLECT12.AddQuestDescription_Context("이건 분명 누군가 나와 내 도끼들을 시기한게 분명하군!");
            Quest37_COLLECT12.AddQuestDescription_Context("흐흐흐... 짜식 누군진 몰라도 좋은건 알아가지고. 킁!");
            Quest37_COLLECT12.AddQuestDescription_Context("아무튼 너 내 도끼좀 찾아줄 수 있겠나?!");
            Quest37_COLLECT12.AddQuestDescription_Context("나도 체면이 있지... 내 부하들에게 내 무기를 잃어버렸다고 같이 찾아달라고는 못하겠거든..\n");
            Quest37_COLLECT12.AddQuestOk_Context("그래! 나도 찾아볼테니 부탁한다!\n아참참..! 내 도끼는 '구리빛 뿔도끼' 다! 탐난다고 가져간다면 내 왼손에 들린 '야만 전사의 양날도끼' 가 널 용서하지 않을테다..!");
            Quest37_COLLECT12.AddQuestNo_Context("킁!");
            Quest37_COLLECT12.AddQuestProgress_Context("끄응...! 아직 찾지 못한건가... 내 '구리빛 뿔도끼'...\n크흡...");
            Quest37_COLLECT12.AddQuestClear_Context("우하하하!!!!\n정말 고맙다!!\n내 이번 일은 꼭 기억해두지!!\n\n['구리빛 뿔도끼' 를 되찾은 '엑슬라임' 이 매우 기뻐하며 도끼를 휘두릅니다.]");
            Quest37_COLLECT12.m_sQuest_Information_Recommend = ("내 소중한 '구리빛 뿔도끼' 를 찾아줘!");
            Quest37_COLLECT12.m_sQuest_Information_Process = ("내 소중한 '구리빛 뿔도끼' 를 찾아줘!\n'엑슬라임' 이 잃어버린 '구리빛 뿔도끼' 를 찾아봅시다.");
            Quest37_COLLECT12.m_sQuest_Information_Condition = ("'구리빛 뿔도끼' 를 찾았으니 '엑슬라임' 에게 가져갑시다.\n아 물론 그냥 가져도 됩니다. 알아서 하세요.");
            Quest37_COLLECT12.m_sQuest_Information_Clear = ("우하하하!!!! 정말 고맙다!! 내 이번 일은 꼭 기억해두지!!\n'구리빛 뿔도끼' 를 되찾은 '엑슬라임' 이 매우 기뻐하며 도끼를 휘두릅니다.");
            Quest37_COLLECT12.AddCondition(1606, 1, 0);
            Quest37_COLLECT12.m_bQuest_Information_Process_Hide = false;
            Quest37_COLLECT12.m_sRewardSOC = new SOC(1, 0, 0, 1, 0, 0, 0, 0, 0);
            Quest37_COLLECT12.m_sRewardSTATUS = new STATUS(0, 0, 30);
            Quest37_COLLECT12.m_nRewardGold = 2000;
            m_Dictionary_QuestList_COLLECT.Add(Quest37_COLLECT12.m_nQuest_Code, Quest37_COLLECT12);
        }

        // [퀘스트[S2]: 서브퀘스트]\n[이예~이~~~~우....]
        {
            Quest38_GOAWAY_TYPE2.AddQuestDescription_Context("우~~!!@@@@아~~!@@");
            Quest38_GOAWAY_TYPE2.AddQuestDescription_Context("예이~~~!~!~~~ 아~~~~웅~~@@@@\n\n[뭐죠 이 주정뱅이는?]");
            Quest38_GOAWAY_TYPE2.AddQuestDescription_Context("내가~임마~!@@@@ 엉?~!!?\n\n['술무라임' 곁에는 '체리주스' 빈병과 '구리빛 뿔도끼' 가 어지럽게 놓여있습니다.]");
            Quest38_GOAWAY_TYPE2.AddQuestDescription_Context("'체리주스' 좀 더줘 히이이잉이잉@@!~~~ 딸꾹");
            Quest38_GOAWAY_TYPE2.AddQuestDescription_Context("아잇~~!!푸~~!@@ 아 임무도 해야되는데에에에엥잉@@@");
            Quest38_GOAWAY_TYPE2.AddQuestDescription_Context("내 임무좀 대신해줘어잉@!~~");
            Quest38_GOAWAY_TYPE2.AddQuestOk_Context("나는 평화를 쏴랑하는 용병!@~~ '술무라임' 님 이시다!@~~~\n앤트놈들좀 절로 가라해봐@@~~~ 짜식들이 봐준다는데 자꾸 앙?~!@@@?\n\n[종류에 상관없이 앤트종족을 '놓아주기' 를 통해 5마리 퇴치해 주세요.]");
            Quest38_GOAWAY_TYPE2.AddQuestNo_Context("후.... 인생 우웅웩!~~@~");
            Quest38_GOAWAY_TYPE2.AddQuestProgress_Context("임....무........@@..");
            Quest38_GOAWAY_TYPE2.AddQuestClear_Context("허하허으어@@!~~~ 고맙드우웩~~@@@@\nㄴ..너 좋은 녀셕이였쟎하~!@@\n\n['술무라임' 이 이것저것 챙겨주었습니다.]");
            Quest38_GOAWAY_TYPE2.m_sQuest_Information_Recommend = ("예이~~~!~!~~~ 아~~~~웅~~@@@@");
            Quest38_GOAWAY_TYPE2.m_sQuest_Information_Process = ("우~~!!@@@@아~~!@@\n...종류에 상관없이 앤트종족을 '놓아주기' 를 통해 5마리 퇴치해 주세요.");
            Quest38_GOAWAY_TYPE2.m_sQuest_Information_Condition = ("'놓아주기' 를 통해 앤트종족을 5마리 퇴치했습니다. 이제 '술무라임' 에게 돌아가 봅시다.");
            Quest38_GOAWAY_TYPE2.m_sQuest_Information_Clear = ("'체리주스' 에 취한 '술무라임' 을 대신하여 그의 임무인 앤트종족 퇴치를 했습니다. '술무라임' 이 정신없이 이것저것 챙겨주었습니다.\n'술무라임' 의 근처에는 '구리빛 뿔도끼' 가 놓여있습니다.");
            Quest38_GOAWAY_TYPE2.SetCondition(E_MONSTER_KIND.ENTS, 5, 0);
            Quest38_GOAWAY_TYPE2.m_sRewardSOC = new SOC(1, 0, 0, 1, 0, 1, 0, 0, 0);
            Quest38_GOAWAY_TYPE2.m_sRewardSTATUS = new STATUS(0, 0, 30);
            Quest38_GOAWAY_TYPE2.m_nRewardGold = 417;
            Quest38_GOAWAY_TYPE2.m_lRewardList_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8005]);
            Quest38_GOAWAY_TYPE2.m_lRewardList_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8013]);
            Quest38_GOAWAY_TYPE2.m_lRewardList_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8019]);
            Quest38_GOAWAY_TYPE2.m_lRewardList_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[8023]);
            m_Dictionary_QuestList_GOAWAY_TYPE.Add(Quest38_GOAWAY_TYPE2.m_nQuest_Code, Quest38_GOAWAY_TYPE2);
        }

        // [퀘스트[S3]: 서브퀘스트]\n[으으으...]
        {
            Quest39_COLLECT13.AddQuestDescription_Context("우....으으으... 난 이제 죽었다... 딸꾹!");
            Quest39_COLLECT13.AddQuestDescription_Context("저기 형씨 나 도와준 김에 한번만 더 도와줘... 딸꾹! ㅠㅠ\n\n[??? 염치도 없네요.]");
            Quest39_COLLECT13.AddQuestDescription_Context("... 임무를 나서던 길에..... 불의의 사고로 '체리주스' 에 취해버렸지 뭐야... 딸꾹!");
            Quest39_COLLECT13.AddQuestDescription_Context("'슬라임 할머니' 가 '네가지 맛 감자 롤' 을 맛보라고 주셔서.... '체리주스' 와 함께 마시다 보니... 딸꾹!");
            Quest39_COLLECT13.AddQuestDescription_Context("하... 임무 끝 보고도 해야하는데....\n심지어 내손에 왜 대장의 '구리빛 뿔도끼' 가 들려 있는지 모르겠어.... 틀림없이 날 죽일거야... 딸꾹!");
            Quest39_COLLECT13.AddQuestDescription_Context("부..부탁이야!!! 날 좀 도와줘...!! 대장에게 살해당한다고!!! 딸꾹!");
            Quest39_COLLECT13.AddQuestDescription_Context("ㅈ..재생..... 재생특효약!!! '알보찰' 만 있으면!!! 임무 끝 보고도 정상적으로 할 수 있어..! 딸꾹!");
            Quest39_COLLECT13.AddQuestOk_Context("고마워... 우선 '알보찰' 을 구해와줘..! 딸꾹!");
            Quest39_COLLECT13.AddQuestNo_Context("으악!!! 제발 도와줘!! 대장에게 살해당한다고!!!!!!!!!! ");
            Quest39_COLLECT13.AddQuestProgress_Context("'빨리..! 딸꾹!");
            Quest39_COLLECT13.AddQuestClear_Context("고마워!!!\n자 여기 대장의 '구리빛 뿔도끼' 를 맡아줘..\n어디 '수풀' 사이에서 주웠다고하고 대장에게 돌려주라... 제발.....\n하... 돈은 또 다 어디갔어...! 딸꾹!\n\n['술무라임' 이 초조해 합니다.]");
            Quest39_COLLECT13.m_sQuest_Information_Recommend = ("우....으으으... 난 이제 죽었다... 딸꾹!");
            Quest39_COLLECT13.m_sQuest_Information_Process = ("으으으... ㅈ..재생..... 재생특효약!!! '알보찰' 만 있으면!!! 임무 끝 보고도 정상적으로 할 수 있어..! 딸꾹!\n'술무라임' 의 임무 끝 보고 를 위해 우선 '알보찰' 을 구합시다.");
            Quest39_COLLECT13.m_sQuest_Information_Condition = ("어렵게 어렵게 재생 특효약인 '알보찰' 을 구했습니다.\n이제 '술무라임' 에게 가봅시다.");
            Quest39_COLLECT13.m_sQuest_Information_Clear = ("고마워!!! 자 여기 대장의 '구리빛 뿔도끼' 를 맡아줘.. 어디 '수풀' 사이에서 주웠다고하고 대장에게 돌려주라... 제발....\n이건 특별히 너 줄게..! 귀한거야!\n\n[재생 특효약인 '알보찰' 을 이용해 정신을 차린 '술무라임' 이 '구리빛 뿔도끼' 를 건네주었습니다.]");
            Quest39_COLLECT13.m_ql_Quest_Necessity_Clear.Add(Quest38_GOAWAY_TYPE2);
            Quest39_COLLECT13.AddCondition(0006, 1, 0);
            Quest39_COLLECT13.m_sRewardSOC = new SOC(1, 0, 0, -1, 0, 0, 0, 0, 0);
            Quest39_COLLECT13.m_sRewardSTATUS = new STATUS(0, 0, 30);
            Quest39_COLLECT13.m_nRewardGold = 0;
            Quest39_COLLECT13.m_lRewardList_Item_Equip.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[1606]);
            Quest39_COLLECT13.m_lRewardList_Item_Use.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Use[12601]);
            m_Dictionary_QuestList_COLLECT.Add(Quest39_COLLECT13.m_nQuest_Code, Quest39_COLLECT13);
        }

        // [퀘스트[S1]: 스토리퀘스트]\n[초원의 평화를 위하여 1]
        {
            Quest40_KILL_TYPE2.AddQuestDescription_Context("이봐 모험가 양반..!");
            Quest40_KILL_TYPE2.AddQuestDescription_Context("한가지 부탁을 해도 되겠는가?...!..!");
            Quest40_KILL_TYPE2.AddQuestDescription_Context("최근 슬라임들이 앤트 들을 괴롭힌다는 소리가 들리고있네..!\n꽤나 값비싼 '짙은 엔트의 나무가죽' 때문인것같더군...!");
            Quest40_KILL_TYPE2.AddQuestDescription_Context("나는 '드넓은 초원' 의 평화를 원하네.\n'드넓은 초원' 에서 약자를 괴롭힌다는것은 있을수 없는일.");
            Quest40_KILL_TYPE2.AddQuestDescription_Context("슬라임 3마리를 잡아줄수 있겠는가..?");
            Quest40_KILL_TYPE2.AddQuestOk_Context("고맙네.!.!.!\n좋은 결과 기대하지!!");
            Quest40_KILL_TYPE2.AddQuestNo_Context("내가 사람을 잘못봤군..");
            Quest40_KILL_TYPE2.AddQuestProgress_Context("아직인가..");
            Quest40_KILL_TYPE2.AddQuestClear_Context("흠... 제법 쓸만할지도..?");
            Quest40_KILL_TYPE2.m_sQuest_Information_Recommend = ("이봐 모험가 양반..!\n한가지 부탁을 해도 되겠는가?...!..!");
            Quest40_KILL_TYPE2.m_sQuest_Information_Process = ("'드넓은 초원' 의 앤트 들을 괴롭히는 슬라임들을 종류에 상관없이 3마리 잡아주게...");
            Quest40_KILL_TYPE2.m_sQuest_Information_Condition = ("앤트 무리를 괴롭히는 슬라임 들을 무자비하게 3마리 잡았습니다.\n이제 '슬라임 협객' 에게 돌아가봅시다.");
            Quest40_KILL_TYPE2.m_sQuest_Information_Clear = ("흠... 제법 쓸만할지도..?\n'드넓은 초원' 의 평화를 원하는 '슬라임 협객' 의 부탁대로 슬라임 3마리를 잡았습니다.");
            Quest40_KILL_TYPE2.SetCondition(E_MONSTER_KIND.SLIME, 3, 0);
            Quest40_KILL_TYPE2.m_sRewardSOC = new SOC(1, 0, 0, -1, 0, 1, 0, 0, 0);
            Quest40_KILL_TYPE2.m_sRewardSTATUS = new STATUS(0, 0, 15);
            Quest40_KILL_TYPE2.m_nRewardGold = 300;
            m_Dictionary_QuestList_KILL_TYPE.Add(Quest40_KILL_TYPE2.m_nQuest_Code, Quest40_KILL_TYPE2);
        }

        // [퀘스트[S2]: 스토리퀘스트]\n[초원의 평화를 위하여 2]
        {
            Quest41_KILL_MONSTER9.AddQuestDescription_Context("큰일이군.. 큰일이야..!");
            Quest41_KILL_MONSTER9.AddQuestDescription_Context("그대가 나를 도와준 덕분에 미미하지만 '드넓은 초원' 의 평화는 좀더..!!\n다시한번 고마움을 표하지..!!");
            Quest41_KILL_MONSTER9.AddQuestDescription_Context("하지만 여전히 '덩치 큰 초원 슬라임' 들은 '드넓은 초원' 의 평화를 깨부시고 있네...");
            Quest41_KILL_MONSTER9.AddQuestDescription_Context("이번에도 부탁해도 되겠나..?\n'덩치 큰 초원 슬라임' 1마리를 잡아줄수 있겠는가..?\n나는 사정이 있어 숨어다녀야 해서말이야....");
            Quest41_KILL_MONSTER9.AddQuestOk_Context("'덩치 큰 초원 슬라임' 은 제법 강력하고 특이한 패턴이 있으니 조심하게..!");
            Quest41_KILL_MONSTER9.AddQuestNo_Context("... 준비가 되면 돌아오게.");
            Quest41_KILL_MONSTER9.AddQuestClear_Context("오...!!! 자네 제법 강하군! 이정도일줄은 몰랐는데 말이야!");
            Quest41_KILL_MONSTER9.AddQuestProgress_Context("아직인가.. 천천히 하게.\n'덩치 큰 초원 슬라임' 은 제법 강한 상대이니 말이야.");
            Quest41_KILL_MONSTER9.m_sQuest_Information_Recommend = ("이봐 모험가 양반 이전엔 고마웠네..!\n다름이 아니라 다시한번 부탁을 해도 되겠는가?...!..!");
            Quest41_KILL_MONSTER9.m_sQuest_Information_Process = ("'덩치 큰 초원 슬라임' 1마리를 잡아줄수 있겠는가..?\n나는 사정이 있어 숨어다녀야 해서말이야....");
            Quest41_KILL_MONSTER9.m_sQuest_Information_Condition = ("'덩치 큰 초원 슬라임' 1마리를 잡았습니다. 이제 '슬라임 협객' 에게 돌아갑시다.");
            Quest41_KILL_MONSTER9.m_sQuest_Information_Clear = ("오...!!! 자네 제법 강하군! 이정도일줄은 몰랐는데 말이야!\n'드넓은 초원' 의 평화를 원하는 '슬라임 협객' 의 부탁대로 '덩치 큰 초원 슬라임' 1마리를 잡았습니다.");
            Quest41_KILL_MONSTER9.AddCondition(2100, 1, 0); 
            Quest41_KILL_MONSTER9.m_ql_Quest_Necessity_Clear.Add(Quest40_KILL_TYPE2);
            Quest41_KILL_MONSTER9.m_sRewardSOC = new SOC(1, 0, 0, -1, 0, 1, 0, 0, 0);
            Quest41_KILL_MONSTER9.m_sRewardSTATUS = new STATUS(0, 0, 30);
            Quest41_KILL_MONSTER9.m_nRewardGold = 400;
            m_Dictionary_QuestList_KILL_MONSTER.Add(Quest41_KILL_MONSTER9.m_nQuest_Code, Quest41_KILL_MONSTER9);
        }

        // [퀘스트[S2]: 스토리퀘스트]\n[초원의 진정한 평화를 위하여]
        {
            Quest42_GOAWAY_TYPE3.AddQuestDescription_Context("모험은 잘하고 있는가..?");
            Quest42_GOAWAY_TYPE3.AddQuestDescription_Context("내 그동안 동족인 슬라임들을 다소 공격적인 방법으로 퇴치해달라 하였지...\n동족인데도 불구하고 말이야...");
            Quest42_GOAWAY_TYPE3.AddQuestDescription_Context("그래서 다른 슬라임들이 자네를 바라보는 시선이 그리 좋지는 않아졌네...!");
            Quest42_GOAWAY_TYPE3.AddQuestDescription_Context("어떤가??\n이번에는 보다 평화롭게 슬라임들을 퇴치하여 줄텐가?\n죽이지않고 슬라임들을 20마리 퇴치해 주게..!");
            Quest42_GOAWAY_TYPE3.AddQuestOk_Context("슬라임들은 대체로 온순한 편이니 어렵진 않을걸세...!\n\n[종류에 상관없이 슬라임 종족을 '놓아주기' 를 통해 20마리 퇴치해 주세요.]");
            Quest42_GOAWAY_TYPE3.AddQuestNo_Context("아 자네는 슬라임들의 분노가 두렵지 않은가..?");
            Quest42_GOAWAY_TYPE3.AddQuestProgress_Context("아직 부족한것같군.");
            Quest42_GOAWAY_TYPE3.AddQuestClear_Context("어떤가..?^^ 평화롭게 퇴치하는것도 좋지않은가?^^\n이게 진정한 평화가 아닐까 하네.");
            Quest42_GOAWAY_TYPE3.m_sQuest_Information_Recommend = ("흠... 마지막 부탁일세!");
            Quest42_GOAWAY_TYPE3.m_sQuest_Information_Process = ("이번에는 보다 평화롭게 슬라임들을 퇴치하여 줄텐가?\n죽이지않고 슬라임들을 20마리 퇴치해 주게..!\n'놓아주기' 를 통해 슬라임들을 20마리 퇴치하세요.");
            Quest42_GOAWAY_TYPE3.m_sQuest_Information_Condition = ("'놓아주기' 를 통해 슬라임들을 20마리 퇴치했습니다. 이제 다시 '슬라임 협객' 에게 돌아갑시다.");
            Quest42_GOAWAY_TYPE3.m_sQuest_Information_Clear = ("어떤가..?^^ 평화롭게 퇴치하는것도 좋지않은가?^^ 이게 진정한 평화가 아닐까 하네.\n'드넓은 초원' 의 평화를 원하는 '슬라임 협객' 의 부탁대로 '놓아주기' 를 통해 슬라임들을 20마리 놓아주었습니다.");
            Quest42_GOAWAY_TYPE3.SetCondition(E_MONSTER_KIND.SLIME, 20, 0);
            Quest42_GOAWAY_TYPE3.m_ql_Quest_Necessity_Clear.Add(Quest41_KILL_MONSTER9);
            Quest42_GOAWAY_TYPE3.m_sRewardSOC = new SOC(1, 0, 0, 2, 0, 0, 0, 0, 0);
            Quest42_GOAWAY_TYPE3.m_sRewardSTATUS = new STATUS(0, 0, 30);
            Quest42_GOAWAY_TYPE3.m_nRewardGold = 500;
            m_Dictionary_QuestList_GOAWAY_TYPE.Add(Quest42_GOAWAY_TYPE3.m_nQuest_Code, Quest42_GOAWAY_TYPE3);
        }

        // [퀘스트[S3]: 스토리퀘스트]\n['드넓은 초원' 의 따스한 햇살]
        {
            Quest43_COLLECT14.AddQuestDescription_Context("안녕하세요!");
            Quest43_COLLECT14.AddQuestDescription_Context("저는 커서 '드넓은 초원' 에 내리쬐는 따스한 햇살 같은 존재가 되고싶어요!");
            Quest43_COLLECT14.AddQuestDescription_Context("하지만 저는 '짙은 앤트의 나무가죽' 을 노리는 자들에 의해 상처를 입었답니다..");
            Quest43_COLLECT14.AddQuestDescription_Context("어서 제 꿈을 위해 회복해야해요......... 저를 도와주세요!");
            Quest43_COLLECT14.AddQuestDescription_Context("약이 필요해요. 제 상처를 아물게 할 수 있는 약이..");
            Quest43_COLLECT14.AddQuestOk_Context("감사합니다!!\n분명 제 병을 치료할 약이 '드넓은 초원' 어딘가에 있을거에요!");
            Quest43_COLLECT14.AddQuestNo_Context("콜록!\n콜록!콜록!!!!");
            Quest43_COLLECT14.AddQuestProgress_Context("빨리 회복해서 제 꿈을 향해 나아갈 거에요!\n모험가님! 약을 구해주세요!!");
            Quest43_COLLECT14.AddQuestClear_Context("['꼬마 앤트' 의 상처부위에 '알보찰' 을 부었습니다.]\n(치이이이이이이이익)\n(치이이이이익)\n끄아아악!!!!\nㄱ..감ㅅ...사합니다.. ㄷ..더..덕분에... 좀 나아진것같은...\n\n.ㄱㅡ...");
            Quest43_COLLECT14.m_sQuest_Information_Recommend = ("'꼬마 앤트' 는 아파하고 있습니다.\n'꼬마 앤트' 를 위해 뭐라도 해야하지 않을까요?");
            Quest43_COLLECT14.m_sQuest_Information_Process = ("아픈 '꼬마 앤트' 의 상처를 치료할 수 있는 약을 구해야 합니다.\n'꼬마 앤트' 는 '짙은 앤트의 나무가죽' 을 노리는 자들에 의해 상처를 입었습니다.");
            Quest43_COLLECT14.m_sQuest_Information_Condition = ("재생 특효약 '알보찰' 이라면 '꼬마 앤트' 의 상처를 치료할 수 있을지도 모릅니다!\n'꼬마 앤트' 에게 빨리 돌아갑시다.");
            Quest43_COLLECT14.m_sQuest_Information_Clear = ("(치이이이이이이이익)\n(치이이이이익)\n끄아아악!!!!\nㄱ..감ㅅ...사합니다.. ㄷ..더..덕분에... 좀 나아진것같은...\n'짙은 앤트의 나무가죽' 을 노리는 자들에 의해 상처를 입은 '꼬마 앤트' 를 치료하기위해 '알보찰' 을 구해줬습니다.");
            Quest43_COLLECT14.AddCondition(0006, 1, 0);
            Quest43_COLLECT14.m_bQuest_Information_Process_Hide = true;
            Quest43_COLLECT14.m_sRewardSOC = new SOC(1, 0, 0, -1, 0, 1, 0, 0, 0);
            Quest43_COLLECT14.m_sRewardSTATUS = new STATUS(0, 0, 30);
            Quest43_COLLECT14.m_nRewardGold = 500;
            m_Dictionary_QuestList_COLLECT.Add(Quest43_COLLECT14.m_nQuest_Code, Quest43_COLLECT14);
        }

        // [퀘스트[S2]: 스토리퀘스트]\n['꼬마 앤트' 를 위해]
        {
            Quest44_KILL_MONSTER10.AddQuestDescription_Context("오 자네인가?\n오랜만이군..!");
            Quest44_KILL_MONSTER10.AddQuestDescription_Context("'꼬마 앤트' 를 치료하기 위해 약을 구하고다닌다는 소식이 들리더군.");
            Quest44_KILL_MONSTER10.AddQuestDescription_Context("'드넓은 초원' 의 '짙은 앤트의 나무가죽' 을 노리는 자들에 의해 상처를 입은 '꼬마 앤트' 를 위해...");
            Quest44_KILL_MONSTER10.AddQuestDescription_Context("'꼬마 앤트' 는 앤트 종족의 희망일세..!");
            Quest44_KILL_MONSTER10.AddQuestDescription_Context("'드넓은 초원' 의 앤트라면 누구나 알고있고 언제나 밝고 긍정적인 '꼬마 앤트' 를 보며 힘든 하루를 살아가지..!");
            Quest44_KILL_MONSTER10.AddQuestDescription_Context("그래서 더더욱!!! '꼬마 앤트' 의 치료가 필요하다네!");
            Quest44_KILL_MONSTER10.AddQuestDescription_Context("그래서 치료약 에 대해 조사를 하던 도중 어떤 정보를 입수했지..!");
            Quest44_KILL_MONSTER10.AddQuestDescription_Context("바로 '주식회사 더 슬라' 에서 취급하는 '알보찰' 이 '꼬마 앤트' 의 상처를 치료할 수 있을걸세!");
            Quest44_KILL_MONSTER10.AddQuestDescription_Context("그 약만 있다면 '꼬마 앤트' 가 상처를 잘 치료할 수 있을거라네!");
            Quest44_KILL_MONSTER10.AddQuestDescription_Context("그러나 한가지 문제가 있다네... '알보찰' 을 구할 방법이 없다네...");
            Quest44_KILL_MONSTER10.AddQuestDescription_Context("'알보찰' 을 구할 방법을 알아내기 위해 '상인단 경비 초원 슬라임' 5마리를 잡아주게나..!");
            Quest44_KILL_MONSTER10.AddQuestOk_Context("좋네! 기다리고 있겠네!!");
            Quest44_KILL_MONSTER10.AddQuestNo_Context("'꼬마 앤트' 는 '알보찰' 을 간절히 기다리고 있을텐데...");
            Quest44_KILL_MONSTER10.AddQuestClear_Context("!!! 그런것이었나..\n'알보찰' 은 쉽게 구할 수 없겠군....\n'주식회사 더 슬라' 의 VIP 가 아니라면 매우 비싸게 구할 수 밖에 없다니..");
            Quest44_KILL_MONSTER10.AddQuestProgress_Context("아직인건가..");
            Quest44_KILL_MONSTER10.m_sQuest_Information_Recommend = ("자네.. 최근에 '알보칠' 을 구하고다닌다는 소식이 들리더군.\n나와 함께하지! 내가 돕겠네!!!");
            Quest44_KILL_MONSTER10.m_sQuest_Information_Process = ("'상인단 경비 초원 슬라임' 5마리를 사냥하고 '알보찰' 을 구할 방법을 알아내주게나..!");
            Quest44_KILL_MONSTER10.m_sQuest_Information_Condition = ("'상인단 경비 초원 슬라임' 을 사냥하고 '알보찰' 을 구할 방법을 알아냈습니다.");
            Quest44_KILL_MONSTER10.m_sQuest_Information_Clear = ("'알보찰' 은 쉽게 구할 수 없겠군....\n'주식회사 더 슬라' 의 VIP 가 아니라면 매우 비싸게 구할 수 밖에 없다니..\n'상인단 경비 초원 슬라임' 을 사냥해 '알보찰' 을 구할 방법을 알아냈습니다.");
            Quest44_KILL_MONSTER10.AddCondition(2003, 5, 0);
            Quest44_KILL_MONSTER10.m_ql_Quest_Necessity_Clear.Add(Quest42_GOAWAY_TYPE3);
            Quest44_KILL_MONSTER10.m_ql_Quest_Necessity_Process.Add(Quest43_COLLECT14);
            Quest44_KILL_MONSTER10.m_sRewardSOC = new SOC(1, 0, 0, -1, 0, 1, 0, 0, 0);
            Quest44_KILL_MONSTER10.m_sRewardSTATUS = new STATUS(0, 0, 30);
            Quest44_KILL_MONSTER10.m_nRewardGold = 500;
            m_Dictionary_QuestList_KILL_MONSTER.Add(Quest44_KILL_MONSTER10.m_nQuest_Code, Quest44_KILL_MONSTER10);

         
            //q8_KILL_MONSTER.AddCondition(0006, 5, 0);
            //q8_KILL_MONSTER.m_sSoc_Necessity_Down = new SOC(-1000, -1000, -1000, -1000, -1000, -1000, -1000, -1000, -1000);
            //q8_KILL_MONSTER.m_sSoc_Necessity_Up = new SOC(1000, 1000, 1000, 1000, 1000, 1000, 1000, 1000, 1000);
            //q8_KILL_MONSTER.m_ql_Quest_Necessity_Process.Add(q7_COLLECT);
            //q8_KILL_MONSTER.m_sRewardSOC = new SOC(5, 0, 0, 0, 0, 5, 0, 0, 0);
            //q8_KILL_MONSTER.m_sRewardSTATUS = new STATUS(0, 0, 20);
            //q8_KILL_MONSTER.AddQuestClearReward_Item(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0002]);
            //m_ql_QuestList_KILL_MONSTER.Add(q8_KILL_MONSTER);
        }

        // [퀘스트[S3]: 스토리퀘스트]\n[재생 특효약 '알보찰']
        {
            Quest45_COLLECT15.AddQuestDescription_Context("이렇게 찾아올줄 알았습니다.");
            Quest45_COLLECT15.AddQuestDescription_Context("'알보찰' 이 필요하다 들었습니다.\n우리 상인단의 정보력은 세계 최강이거든요.^^");
            Quest45_COLLECT15.AddQuestDescription_Context("긴말하지 않겠습니다.\n세상에 공짜는 있을 수 없는법입니다.");
            Quest45_COLLECT15.AddQuestDescription_Context("'알보찰' 을 만드는 과정은 매우 까다롭습니다.\n'알보찰' 하나를 위해 '초원 슬라임의 덩어리' 10개 정도가 필요하죠. 그렇다면 초원 슬라임 100여 마리가 필요하답니다.^^");
            Quest45_COLLECT15.AddQuestDescription_Context("저희 '주식회사 더 슬라' 가 모험가님께 진 빚을 갚겠습니다.\n이번에만 특별히 '초원 슬라임의 덩어리' 3개를 구해 오신다면 '알보찰' 1개로 바꿔드리겠습니다.");
            Quest45_COLLECT15.AddQuestDescription_Context("어떻습니까? 제법 괜찮은 거래가 아닙니까?");
            Quest45_COLLECT15.AddQuestOk_Context("좋습니다.\n알다시피 어떻게든 구해오기만 하면 된답니다.^^");
            Quest45_COLLECT15.AddQuestNo_Context("흠 '알보찰' 이 필요 없다면 어쩔 수 없습니다.");
            Quest45_COLLECT15.AddQuestProgress_Context("'알보찰' 이 급하지 않습니까?");
            Quest45_COLLECT15.AddQuestClear_Context("'알보찰' 여기 있습니다. 그간의 빚은 확실히 갚은겁니다.");
            Quest45_COLLECT15.m_sQuest_Information_Recommend = ("이렇게 찾아올줄 알았습니다.\n이번에만 특별히 손해를 감수하도록 하죠.'");
            Quest45_COLLECT15.m_sQuest_Information_Process = ("이번에만 특별히 '초원 슬라임의 덩어리' 3개를 구해 오신다면 '알보찰' 1개로 바꿔드리겠습니다.\n원래는 10개가 필요하다구요.");
            Quest45_COLLECT15.m_sQuest_Information_Condition = ("'초원 슬라임의 덩어리' 3개를 구했습니다.\n이제 '골드 타임 슬라임' 에게 돌아가 '알보찰' 로 바꿉시다.");
            Quest45_COLLECT15.m_sQuest_Information_Clear = ("'알보찰' 여기 있습니다.");
            Quest45_COLLECT15.AddCondition(0002, 3, 0);
            Quest45_COLLECT15.m_ql_Quest_Necessity_Clear.Add(Quest44_KILL_MONSTER10);
            Quest45_COLLECT15.m_ql_Quest_Necessity_Clear.Add(Quest19_COLLECT5);
            Quest45_COLLECT15.m_bQuest_Information_Process_Hide = false;
            Quest45_COLLECT15.m_sRewardSOC = new SOC(1, 0, 0, -1, 0, 0, 0, 0, 0);
            Quest45_COLLECT15.m_sRewardSTATUS = new STATUS(0, 0, 30);
            Quest45_COLLECT15.m_lRewardList_Item_Etc.Add(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[0006]);
            m_Dictionary_QuestList_COLLECT.Add(Quest45_COLLECT15.m_nQuest_Code, Quest45_COLLECT15);
        }

        // [퀘스트[S1]: 서브퀘스트]\n[장비 강화]
        {
            Quest46_CONVERSATION12.AddQuestDescription_Context("이봐요 혹시 장비강화에 대해 관심 있나요?");
            Quest46_CONVERSATION12.AddQuestDescription_Context("장비 강화란 '강화서' 를 통해 이루어 진답니다.");
            Quest46_CONVERSATION12.AddQuestDescription_Context("'강화서' 를 이용해 일정 확률로 장비 아이템의 능력치를 변경시켜 주죠.\n추가 체력, 추가 마나, 공격력, 방어력, 공격속도, 이동속도 등 다양한 능력치를 변경시켜준답니다.\n대체로 좋은쪽으로 말이죠.");
            Quest46_CONVERSATION12.AddQuestDescription_Context("하지만 '강화서' 는 현재 모종의 이유로 이 세계 전역에서 금기시 되어 통제받는 물건이랍니다.\n구할래야 구할수도없죠.");
            Quest46_CONVERSATION12.AddQuestDescription_Context("그러나 제가 누굽니까!\n저는 대장장이 일도 좋지만 세계 곳곳의 '강화서' 수집에도 대단히 관심을 가지고 있죠.");
            Quest46_CONVERSATION12.AddQuestDescription_Context("어떻습니까? 성능은 보장합니다. 한번 써보시길..");
            Quest46_CONVERSATION12.AddQuestOk_Context("그럴줄 알았습니다. 역시 '드넓은 초원' 의 사고뭉치 답군요.\n\n[잠시후 다시 '블랙' 에게 말을 걸어봅시다.]");
            Quest46_CONVERSATION12.AddQuestNo_Context("... 방금 대화는 잊어주시죠.");
            Quest46_CONVERSATION12.AddQuestClear_Context("이 세계의 금기라니 얼마나 두근두근합니까?!\n은밀하게 사용해보도록 합시다!!!");
            Quest46_CONVERSATION12.AddQuestProgress_Context("...");
            Quest46_CONVERSATION12.m_sQuest_Information_Recommend = ("당신 이 세계의 금기인 장비강화에 대해 관심 있으신가요?");
            Quest46_CONVERSATION12.m_sQuest_Information_Process = ("당신은 이 세계의 금기를 어길것인가?");
            Quest46_CONVERSATION12.m_sQuest_Information_Condition = ("당신은 이 세계의 금기를 어길것인가?");
            Quest46_CONVERSATION12.m_sQuest_Information_Clear = ("당신은 이 세계의 금기를 어겼습니다. 그러나 때로는 그런것이 짜릿한 법이죠.\n위험부담 없이 발전은 없습니다.\n\n이 세계 전역에서 금기시 되어 통제받는 '강화서' 를 이용해 장비 아이템을 강화할 수 있다는것을 깨달았습니다.");
            Quest46_CONVERSATION12.m_bQuest_Information_Process_Hide = false;
            Quest46_CONVERSATION12.m_sStatus_Necessity_Down = new STATUS(15);
            Quest46_CONVERSATION12.m_sRewardSOC = new SOC(1, 1, 0, 0, 0, 0, 0, 0, 1);
            Quest46_CONVERSATION12.m_sRewardSTATUS = new STATUS(0, 0, 1);
            Quest46_CONVERSATION12.m_nRewardGold = 0;
            Quest46_CONVERSATION12.AddQuestClearReward_Item(ItemManager.instance.m_Dictionary_MonsterDrop_Use[11000]);
            m_Dictionary_QuestList_CONVERSATION.Add(Quest46_CONVERSATION12.m_nQuest_Code, Quest46_CONVERSATION12);
        }

        // [퀘스트[S4]: 스토리퀘스트]\n[선택]
        {
            Quest47_CONVERSATION13.AddQuestDescription_Context("어이__ '드넓은 초원' 의 사고뭉치녀석___ -_-");
            Quest47_CONVERSATION13.AddQuestDescription_Context("너가 설치고 다녀서 요 근래 '드넓은 초원' 의 혼란이 극대화 되버렸잖아. ㅡㅡ");
            Quest47_CONVERSATION13.AddQuestDescription_Context("니가 싼 똥은 니가 치우라고 -_-");
            Quest47_CONVERSATION13.AddQuestDescription_Context("오직 이득을 위해 '주식회사 더 슬라' 와 손잡을지, 아니면 '드넓은 초원' 의 진정한 평화를 위해 '슬라임 협객' 과 손잡을지 선택해라__");
            Quest47_CONVERSATION13.AddQuestDescription_Context("'주식회사 더 슬라' 와 손잡고 '드넓은 초원' 에 직면한 문제를 눈감는다면 앞으로 너의 모험에 부족함은 없겠지. 돈이면 돈, 명예면 명예__");
            Quest47_CONVERSATION13.AddQuestDescription_Context("'슬라임 협객' 과 손잡고 '드넓은 초원' 의 진정한 평화를 위해 노력한다면 당장은 힘들고 괴로울지라도 그 끝에는 든든한 '드넓은 초원' 이라는 조력자를 얻을 수 있게 되겠지__");
            Quest47_CONVERSATION13.AddQuestDescription_Context("나같이 타락해버린 슬라임들은 이제와서 예전처럼 '드넓은 초원' 의 진정한 평화를 바라지는 않는다고___\n그저 적응해서 살아갈 뿐이야...\n\n[잠시후 다시 '무료한 슬라임' 에게 말을 걸어 봅시다.]");
            Quest47_CONVERSATION13.AddQuestOk_Context("모든것에는 책임이 따른다고 했다___\n부디 선택을 통해 그동안의 일을 책임지고 앞으로 나아가라___");
            Quest47_CONVERSATION13.AddQuestNo_Context("모든것은 너의 책임이 따른다___\n책임을 회피하는거냐__");
            Quest47_CONVERSATION13.AddQuestClear_Context("사고뭉치녀석 우린 니가 어떤 선택을 하든 상관없어__ 하고픈대로 하도록 해라__\n\n['골드 타임 슬라임' 에게서 [퀘스트[S4]: 스토리퀘스트][선택[자신과 '주식회사 더 슬라' 의 이권을 위해]] 를 진행할 수 있습니다.]\n['슬라임 협객' 에게서 [퀘스트[S4]: 스토리퀘스트][선택['드넓은 초원' 의 평화를 위해]] 를 진행할 수 있습니다.]\n[※ 두가지 퀘스트를 동시에 수락할 수 없습니다.]");
            Quest47_CONVERSATION13.AddQuestProgress_Context("...");
            Quest47_CONVERSATION13.m_sQuest_Information_Recommend = ("모든것에는 책임이 따른다고 했다___\n부디 선택을 통해 그동안의 일을 책임지고 앞으로 나아가라___");
            Quest47_CONVERSATION13.m_sQuest_Information_Process = ("모든것에는 책임이 따른다고 했다___\n부디 선택을 통해 그동안의 일을 책임지고 앞으로 나아가라___");
            Quest47_CONVERSATION13.m_sQuest_Information_Condition = ("모든것에는 책임이 따른다고 했다___\n부디 선택을 통해 그동안의 일을 책임지고 앞으로 나아가라___");
            Quest47_CONVERSATION13.m_sQuest_Information_Clear = ("사고뭉치녀석 우린 니가 어떤 선택을 하든 상관없어__ 하고픈대로 하도록 해라__\n\n'골드 타임 슬라임' 에게서 [퀘스트[S4]: 스토리퀘스트][선택[자신과 '주식회사 더 슬라' 의 이권을 위해]] 를 진행할 수 있습니다.\n'슬라임 협객' 에게서 [퀘스트[S4]: 스토리퀘스트][선택['드넓은 초원' 의 평화를 위해]] 를 진행할 수 있습니다.\n※ 두가지 퀘스트를 동시에 수락할 수 없습니다.");
            Quest47_CONVERSATION13.m_bQuest_Information_Process_Hide = false;
            Quest47_CONVERSATION13.m_sRewardSOC = new SOC(1, 0, 0, 1, 0, 0, 0, 0, 0);
            Quest47_CONVERSATION13.m_sRewardSTATUS = new STATUS(0, 0, 1);
            Quest47_CONVERSATION13.m_nRewardGold = 0;
            Quest47_CONVERSATION13.m_sStatus_Necessity_Down.SetSTATUS_LV(12);
            Quest47_CONVERSATION13.m_ql_Quest_Necessity_Clear.Add(Quest45_COLLECT15);
            m_Dictionary_QuestList_CONVERSATION.Add(Quest47_CONVERSATION13.m_nQuest_Code, Quest47_CONVERSATION13);
        }

        // [퀘스트[S4]: 스토리퀘스트]\n[선택[자신과 '주식회사 더 슬라' 의 이권을 위해]]
        {
            Quest48_CONVERSATION14.AddQuestDescription_Context("어서오십시오. 잘 찾아 오셨습니다.");
            Quest48_CONVERSATION14.AddQuestDescription_Context("긴말 하지 않겠습니다.\n'슬라임 협객' 을 버리고 저희 '주식회사 더 슬라' 와 함께합시다.");
            Quest48_CONVERSATION14.AddQuestDescription_Context("대체 '드넓은 초원' 의 평화가 뭐냔 말입니까?");
            Quest48_CONVERSATION14.AddQuestDescription_Context("매번 말하지만 돈만 벌면 됩니다.\n살아남기 위해, 더 높은곳으로 가기 위해..");
            Quest48_CONVERSATION14.AddQuestDescription_Context("사소한건 신경쓰지 마세요.");
            Quest48_CONVERSATION14.AddQuestDescription_Context("저희 '주식회사 더 슬라' 와 함께합시다. 모험가님의 미래를 저희 회사에서 보장해드리도록 하겠습니다.\n\n[['슬라임 협객' 에게서 [퀘스트[S4]: 스토리퀘스트][선택['드넓은 초원' 의 평화를 위해]] 를 진행할 수 없습니다.]]");
            Quest48_CONVERSATION14.AddQuestOk_Context("한번 더 생각할 시간을 드리도록 하죠.\n\n[잠시후 다시 '골드 타임 슬라임' 에게 말을 걸어봅시다.]");
            Quest48_CONVERSATION14.AddQuestNo_Context("후회하실 텐데요...");
            Quest48_CONVERSATION14.AddQuestClear_Context("좋습니다. 앞으로도 쭉 함께 높은곳으로 가봅시다!");
            Quest48_CONVERSATION14.AddQuestProgress_Context("...");
            Quest48_CONVERSATION14.m_sQuest_Information_Recommend = ("대체 '드넓은 초원' 의 평화가 뭐냔 말입니까?\n매번 말하지만 돈만 벌면 됩니다. 살아남기 위해, 더 높은곳으로 가기 위해..\n저희 '주식회사 더 슬라' 와 함께합시다. 모험가님의 미래를 저희 회사에서 보장해드리도록 하겠습니다.");
            Quest48_CONVERSATION14.m_sQuest_Information_Process = ("대체 '드넓은 초원' 의 평화가 뭐냔 말입니까?\n매번 말하지만 돈만 벌면 됩니다. 살아남기 위해, 더 높은곳으로 가기 위해..\n저희 '주식회사 더 슬라' 와 함께합시다. 모험가님의 미래를 저희 회사에서 보장해드리도록 하겠습니다.");
            Quest48_CONVERSATION14.m_sQuest_Information_Condition = ("대체 '드넓은 초원' 의 평화가 뭐냔 말입니까?\n매번 말하지만 돈만 벌면 됩니다. 살아남기 위해, 더 높은곳으로 가기 위해..\n저희 '주식회사 더 슬라' 와 함께합시다. 모험가님의 미래를 저희 회사에서 보장해드리도록 하겠습니다.");
            Quest48_CONVERSATION14.m_sQuest_Information_Clear = ("좋습니다. 앞으로도 쭉 함께 높은곳으로 가봅시다!\n당신은 '드넓은 초원' 의 평화 보다는 자신과 '주식회사 더 슬라' 의 이권을 선택했습니다.\n실리주의자!");
            Quest48_CONVERSATION14.m_bQuest_Information_Process_Hide = false;
            Quest48_CONVERSATION14.m_sRewardSOC = new SOC(1, 0, 0, 1, 0, 0, 0, 0, 0);
            Quest48_CONVERSATION14.m_sRewardSTATUS = new STATUS(0, 0, 50);
            Quest48_CONVERSATION14.m_nRewardGold = 5000;
            Quest48_CONVERSATION14.m_sStatus_Necessity_Down.SetSTATUS_LV(12);
            Quest48_CONVERSATION14.m_ql_Quest_Necessity_Clear.Add(Quest47_CONVERSATION13);
            Quest48_CONVERSATION14.m_ql_Quest_Necessity_NonProcess.Add(Quest49_CONVERSATION15);
            Quest48_CONVERSATION14.m_ql_Quest_Necessity_NonClear.Add(Quest49_CONVERSATION15);
            m_Dictionary_QuestList_CONVERSATION.Add(Quest48_CONVERSATION14.m_nQuest_Code, Quest48_CONVERSATION14);
        }

        // [퀘스트[S4]: 스토리퀘스트]\n[선택['드넓은 초원' 의 평화를 위해]]
        {
            Quest49_CONVERSATION15.AddQuestDescription_Context("때가 되었군..!");
            Quest49_CONVERSATION15.AddQuestDescription_Context("이제 우리 '슬라임 인권 협회', 앤트들이 모두 뭉쳐 진정한 '드넓은 초원' 의 평화를 되찾을때가 되었네!");
            Quest49_CONVERSATION15.AddQuestDescription_Context("그동안 다소 과격한 방법으로 '드넓은 초원' 의 평화를 되찾기 위해 노력했었지...");
            Quest49_CONVERSATION15.AddQuestDescription_Context("그러나 내가 틀린것 같더군!!!");
            Quest49_CONVERSATION15.AddQuestDescription_Context("스승님인 '그래스 슬라임' 님이 사라지고난 이후 '주식회사 더 슬라' 가 등장하여 그에맞게 적응하며 살아가는 '드넓은 초원' 의 슬라임과 앤트들을 보니 우리의 과격 투쟁에 대해 다시한번 생각하게 되더군..");
            Quest49_CONVERSATION15.AddQuestDescription_Context("사실 우리는 어떤 척박한 환경에도 꺾이지 않고 잘 적응해 살아왔다네.");
            Quest49_CONVERSATION15.AddQuestDescription_Context("먼 과거 슬라임과 앤트들이 아무것도 없던 '드넓은 초원' 에 정착하여 삶의 터전을 이룰때도..");
            Quest49_CONVERSATION15.AddQuestDescription_Context("과거 '대전쟁' 이 일어나 이 세계 전역이 쑥대밭이 되었을때도.. 잘....");
            Quest49_CONVERSATION15.AddQuestDescription_Context("사실 슬라임과 앤트종족은 가장 약하고 핍박받는 종족이지.\n그렇기에 환경 적응력이 뛰어나다네.");
            Quest49_CONVERSATION15.AddQuestDescription_Context("이미 '주식회사 더 슬라' 의 성장으로 변화한 '드넓은 초원' 에 적응하며 살아가는 슬라임과 앤트들을 위해 우리가 과격하게 투쟁을 해야할까 싶다네..");
            Quest49_CONVERSATION15.AddQuestDescription_Context("사실 조금 지치기도 했고말이야. 괜히 나때문에 우리 연합조직도 피해를 보는듯 해서 심란하다네.");
            Quest49_CONVERSATION15.AddQuestDescription_Context("이런 나를 위해 '주식회사 더 슬라' 에 담판을 지으러 가세나.\n자네와 함께라면 든든할 것 같군. 우리 둘만 가자고. 연합조직원들이 피흘리는건 볼 수 없어.\n\n['골드 타임 슬라임' 에게서 [퀘스트[S4]: 스토리퀘스트][선택[자신과 '주식회사 더 슬라' 의 이권을 위해]] 를 진행할 수 없습니다.]");
            Quest49_CONVERSATION15.AddQuestOk_Context("고맙네! 역시 '드넓은 초원' 의 사고뭉치라니까 ㅎㅎ.\n자 가보자고!\n\n[잠시후 다시 '슬라임 협객' 에게 말을 걸어봅시다.]");
            Quest49_CONVERSATION15.AddQuestNo_Context("내가 생각을 바꾸기전에 다시 찾아와 주길..");
            Quest49_CONVERSATION15.AddQuestClear_Context("잘 생각했네!\n그럼 이제 '주식회사 더 슬라' 의 '골드 타임 슬라임' 을 찾아가 보세나!");
            Quest49_CONVERSATION15.AddQuestProgress_Context("...");
            Quest49_CONVERSATION15.m_sQuest_Information_Recommend = ("'주식회사 더 슬라' 에 담판을 지으러 가세나.\n자네와 함께라면 든든할 것 같군. 우리 둘만 가자고. 연합조직원들이 피흘리는건 볼 수 없어.");
            Quest49_CONVERSATION15.m_sQuest_Information_Process = ("'주식회사 더 슬라' 에 담판을 지으러 가세나.\n자네와 함께라면 든든할 것 같군. 우리 둘만 가자고. 연합조직원들이 피흘리는건 볼 수 없어.");
            Quest49_CONVERSATION15.m_sQuest_Information_Condition = ("'주식회사 더 슬라' 에 담판을 지으러 가세나.\n자네와 함께라면 든든할 것 같군. 우리 둘만 가자고. 연합조직원들이 피흘리는건 볼 수 없어.");
            Quest49_CONVERSATION15.m_sQuest_Information_Clear = ("자 드가자!\n당신은 '드넓은 초원' 의 평화를 선택했습니다.\n낭만파!");
            Quest49_CONVERSATION15.m_bQuest_Information_Process_Hide = false;
            Quest49_CONVERSATION15.m_sRewardSOC = new SOC(1, 0, 0, 1, 0, 0, 0, 0, 0);
            Quest49_CONVERSATION15.m_sRewardSTATUS = new STATUS(0, 0, 50);
            Quest49_CONVERSATION15.m_nRewardGold = 0;
            Quest49_CONVERSATION15.m_sStatus_Necessity_Down.SetSTATUS_LV(12);
            Quest49_CONVERSATION15.m_ql_Quest_Necessity_Clear.Add(Quest47_CONVERSATION13);
            Quest49_CONVERSATION15.m_ql_Quest_Necessity_NonProcess.Add(Quest48_CONVERSATION14);
            Quest49_CONVERSATION15.m_ql_Quest_Necessity_NonClear.Add(Quest48_CONVERSATION14);
            m_Dictionary_QuestList_CONVERSATION.Add(Quest49_CONVERSATION15.m_nQuest_Code, Quest49_CONVERSATION15);
        }

        // [퀘스트[S5]: 스토리퀘스트]\n[검증]
        {
            Quest50_KILL_MONSTER11.AddQuestDescription_Context("이야기는 들었다.");
            Quest50_KILL_MONSTER11.AddQuestDescription_Context("우리와.... 함께 하기로 했다고.");
            Quest50_KILL_MONSTER11.AddQuestDescription_Context("[끄덕]");
            Quest50_KILL_MONSTER11.AddQuestDescription_Context("어리석구나. 부와 명예에 눈이 멀어버렸군.");
            Quest50_KILL_MONSTER11.AddQuestDescription_Context("'드넓은 초원' 의 불평등따윈 신경쓰지 않는다는거냐.");
            Quest50_KILL_MONSTER11.AddQuestDescription_Context("너에게 연민과 다른이들의 고통은 중요하지 않나보군.");
            Quest50_KILL_MONSTER11.AddQuestDescription_Context("[당신또한 그렇지 않습니까? 듣기로는 '드넓은 초원' 의 영원한 벗 '그래스 슬라임' 님이 가장 아꼈다고 들었습니다!]");
            Quest50_KILL_MONSTER11.AddQuestDescription_Context(". . . 그를... 알고있나?");
            Quest50_KILL_MONSTER11.AddQuestDescription_Context(". . . 꽤나 많은걸 알고있군.");
            Quest50_KILL_MONSTER11.AddQuestDescription_Context("난 그저 다가올 미래를 대비하며 현재를 살아갈 뿐이다.\n어떤 멍청이('슬라임 협객') 처럼 의미없는짓을 하지 않을 뿐이다. 나에게 과거는 의미없는것.");
            Quest50_KILL_MONSTER11.AddQuestDescription_Context("잡설은 여기까지 하지. 앞으로 잘부탁한다.");
            Quest50_KILL_MONSTER11.AddQuestDescription_Context("그러나!");
            Quest50_KILL_MONSTER11.AddQuestDescription_Context("'골드 타임 슬라임' 은 널 인정했어도 난 아직 널 인정하지 않았다.");
            Quest50_KILL_MONSTER11.AddQuestDescription_Context("날 이겨보도록.\n진지하게 상대해 주겠다.");
            Quest50_KILL_MONSTER11.AddQuestOk_Context("미리 말하겠지만 준비를 많이 해야 할거다.\n'[드넓은 초원] 오목하게 가라앉은 곳' 으로 온다면 전투에 응하도록 하지.\n\n['[드넓은 초원] 오목하게 가라앉은 곳' 에서 '테 슬라임' 과의 보스전투의 진행이 가능해 집니다.]");
            Quest50_KILL_MONSTER11.AddQuestNo_Context("왜그러지? 자신없는건가?");
            Quest50_KILL_MONSTER11.AddQuestClear_Context("훗. 제법 강하군.\n정식으로 소개하지. 난 앞으로 함께 일 할 '주식회사 더 슬라' 의 '테 슬라임' 이라고 한다.\n잘부탁 한다.\n\n['주식회사 더 슬라' 의 최고 강자이자 이 세계에서 손꼽히는 강자인 '테 슬라임' 과의 보스전투에서 승리했기에 관련 스탯이 대폭 상승합니다.]");
            Quest50_KILL_MONSTER11.AddQuestProgress_Context("아직 넌 날 꺾지 못했다.");
            Quest50_KILL_MONSTER11.m_sQuest_Information_Recommend = ("아직 넌 날 꺾지 못했다.\n날 이겨보도록. 진지하게 상대해 주겠다.");
            Quest50_KILL_MONSTER11.m_sQuest_Information_Process = ("날 이겨보도록. 진지하게 상대해 주겠다.\n['테 슬라임' 과의 보스전투에서 이겨야합니다.]");
            Quest50_KILL_MONSTER11.m_sQuest_Information_Condition = ("... 너의 승리다.\n['테 슬라임' 과의 보스전투에서 이겼습니다.]\n['테 슬라임' 에게 돌아가 말을 걸어봅시다.]");
            Quest50_KILL_MONSTER11.m_sQuest_Information_Clear = ("훗. 제법 강하군.\n정식으로 소개하지. 난 앞으로 함께 일 할 '주식회사 더 슬라' 의 '테 슬라임' 이라고 한다.\n잘부탁 한다.\n['테 슬라임' 과의 보스전투에서 이겨 그에게 인정받았습니다.]");
            Quest50_KILL_MONSTER11.AddCondition(10001, 1, 0);
            Quest50_KILL_MONSTER11.m_sStatus_Necessity_Down.SetSTATUS_LV(12);
            Quest50_KILL_MONSTER11.m_ql_Quest_Necessity_Clear.Add(Quest48_CONVERSATION14);
            Quest50_KILL_MONSTER11.m_sRewardSOC = new SOC(10, 5, 5, 5, 5, 5, 5, 5, 0);
            Quest50_KILL_MONSTER11.m_sRewardSTATUS = new STATUS(0, 0, 100, 10, 0, 0, 0, 3, 3, 3, 0, 0, 0, 3, 3, 3, 0);
            Quest50_KILL_MONSTER11.m_nRewardGold = 5000;
            m_Dictionary_QuestList_KILL_MONSTER.Add(Quest50_KILL_MONSTER11.m_nQuest_Code, Quest50_KILL_MONSTER11);
        }

        // [퀘스트[S4]: 스토리퀘스트]\n[협상]
        {
            Quest51_CONVERSATION16.AddQuestDescription_Context("흥. 꽤나 달갑지 않은 얼굴들이군요.");
            Quest51_CONVERSATION16.AddQuestDescription_Context("할말 있으시면 제 비서인 '세일 슬라' 에게 말씀주시죠. 제가 워낙 바빠서요.");
            Quest51_CONVERSATION16.AddQuestDescription_Context("[담판을 지으러 왔습니다.]");
            Quest51_CONVERSATION16.AddQuestDescription_Context("호오. 담판이라? 그동안 저희 '주식회사 더 슬라' 의 일거수 일투족을 감시하며 방해하더니 이제와서 뻔뻔하기 그지없군요.");
            Quest51_CONVERSATION16.AddQuestDescription_Context("[. . .]");
            Quest51_CONVERSATION16.AddQuestDescription_Context("들어는 보겠습니다. 단 소중한 제 시간만큼의 값어치가 없을 경우 각오해야할겁니다.");
            Quest51_CONVERSATION16.AddQuestDescription_Context("말씀해 보시죠.");
            Quest51_CONVERSATION16.AddQuestDescription_Context("[슬라임 협객]: [그동안에 있었던 과격투쟁으로 인한 모든 피해를 보상하겠네. 또한 앞으로 과격투쟁은 일체 없을걸세!]");
            Quest51_CONVERSATION16.AddQuestDescription_Context("[슬라임 협객]: [그러나 자네들 '주식회사 더 슬라' 또한 '드넓은 초원' 의 모든 슬라임과 앤트들을 존중해 주게나! 공생하는 방법을 찾아가보세나!]");
            Quest51_CONVERSATION16.AddQuestDescription_Context(". . . 무언가 단단히 '착각' 하고 있으신것 같군요.");
            Quest51_CONVERSATION16.AddQuestDescription_Context("당신들의 과격투쟁에 저희 '주식회사 더 슬라' 가 큰 피해를 봤다고 생각하십니까?");
            Quest51_CONVERSATION16.AddQuestDescription_Context("또한 저희가 어째서 '드넓은 초원' 의 모든 슬라임과 앤트들을 존중해 줘야하죠? 미안하지만 그럴 의무는 없습니다.");
            Quest51_CONVERSATION16.AddQuestDescription_Context("[슬라임 협객]: [하지만 내겐 '드넓은 초원' 의 모두를 단합할 능력이 있지. 자네는 두렵지 않나?]");
            Quest51_CONVERSATION16.AddQuestDescription_Context(". . . 확실히 그건 무섭군요. 확실히 당신의 리더쉽은 강력하죠.");
            Quest51_CONVERSATION16.AddQuestDescription_Context("[슬라임 협객]: [난 더이상 피를 보고싶지는 않다네.]");
            Quest51_CONVERSATION16.AddQuestDescription_Context("저도 마찬가지입니다. 피차 좋은게 좋은거 아니겠습니까?");
            Quest51_CONVERSATION16.AddQuestDescription_Context(". . .");
            Quest51_CONVERSATION16.AddQuestDescription_Context("그렇다면 이건 어떻습니까?");
            Quest51_CONVERSATION16.AddQuestDescription_Context("저희 '주식회사 더 슬라' 는 최근들어 가파르게 성장하고있습니다. 그에따라 다양한 분야의 수많은 인재가 필요하구요.");
            Quest51_CONVERSATION16.AddQuestDescription_Context("저희가 정식으로 '드넓은 초원' 의 능력있는 슬라임과 앤트들을 채용하겠습니다.");
            Quest51_CONVERSATION16.AddQuestDescription_Context("또한 채용된 자들의 실적에 근거하여 '알보칠' 의 원료인 '초원 슬라임의 덩어리' 와 '짙은앤트의 나무가죽' 의 채집을 줄이겠습니다.");
            Quest51_CONVERSATION16.AddQuestDescription_Context("[슬라임 협객]: [. . . 괜찮은 방법 같구먼.]");
            Quest51_CONVERSATION16.AddQuestDescription_Context("하지만 '드넓은 초원' 의 슬라임과 앤트들이 능력이 있는지 먼저 따져보아야 하죠.");
            Quest51_CONVERSATION16.AddQuestDescription_Context("비록 슬라임과 앤트종족은 아니더라도 '드넓은 초원' 의 상황을 잘 파악하고있고, '드넓은 초원' 에서 성장해 영향력을 행사하고있는 당신.");
            Quest51_CONVERSATION16.AddQuestDescription_Context("저는 당신의 능력에대해 검증을 요구합니다.");
            Quest51_CONVERSATION16.AddQuestDescription_Context("우리 슬라임과 앤트들은 환경에 적응하는 능력이 아주 뛰어납니다.");
            Quest51_CONVERSATION16.AddQuestDescription_Context("따라서 똑같은 환경에서 성장한 당신의 능력이 검증된다면 '드넓은 초원' 의 슬라임과 앤트들을 믿고 채용할 수 있을것 같습니다.");
            Quest51_CONVERSATION16.AddQuestDescription_Context("어떻습니까?");
            Quest51_CONVERSATION16.AddQuestOk_Context("좋습니다. 검증은 간단합니다.\n'주식회사 더 슬라' 의 최고 강자이자 이 세계 에서 손꼽히는 강자인 '테 슬라임' 을 찾아가보십시오.\n그에게는 따로 말을 해 놓겠습니다.");
            Quest51_CONVERSATION16.AddQuestNo_Context("자신이 없는건가요?\n전 당신의 자신감 만큼은 높게 평가했습니다만...");
            Quest51_CONVERSATION16.AddQuestClear_Context("니녀석인가? '드넓은 초원' 의 희망이..");
            Quest51_CONVERSATION16.AddQuestProgress_Context("'테 슬라임' 을 찾아가 보시죠.");
            Quest51_CONVERSATION16.m_sQuest_Information_Recommend = ("비록 슬라임과 앤트종족은 아니더라도 '드넓은 초원' 의 상황을 잘 파악하고있고, '드넓은 초원' 에서 성장해 영향력을 행사하고있는 당신.\n" +
                "저는 당신의 능력에대해 검증을 요구합니다.\n" +
                "우리 슬라임과 앤트들은 환경에 적응하는 능력이 아주 뛰어납니다.\n" +
                "따라서 똑같은 환경에서 성장한 당신의 능력이 검증된다면 '드넓은 초원' 의 슬라임과 앤트들을 믿고 채용할 수 있을것 같습니다.\n" +
                "좋습니다. 검증은 간단합니다.\n'주식회사 더 슬라' 의 최고 강자이자 이 세계 에서 손꼽히는 강자인 '테 슬라임' 을 찾아가보십시오.");
            Quest51_CONVERSATION16.m_sQuest_Information_Process = ("비록 슬라임과 앤트종족은 아니더라도 '드넓은 초원' 의 상황을 잘 파악하고있고, '드넓은 초원' 에서 성장해 영향력을 행사하고있는 당신.\n" +
                "저는 당신의 능력에대해 검증을 요구합니다.\n" +
                "우리 슬라임과 앤트들은 환경에 적응하는 능력이 아주 뛰어납니다.\n" +
                "따라서 똑같은 환경에서 성장한 당신의 능력이 검증된다면 '드넓은 초원' 의 슬라임과 앤트들을 믿고 채용할 수 있을것 같습니다.\n" +
                "좋습니다. 검증은 간단합니다.\n'주식회사 더 슬라' 의 최고 강자이자 이 세계 에서 손꼽히는 강자인 '테 슬라임' 을 찾아가보십시오.");
            Quest51_CONVERSATION16.m_sQuest_Information_Condition = ("비록 슬라임과 앤트종족은 아니더라도 '드넓은 초원' 의 상황을 잘 파악하고있고, '드넓은 초원' 에서 성장해 영향력을 행사하고있는 당신.\n" +
                "저는 당신의 능력에대해 검증을 요구합니다.\n" +
                "우리 슬라임과 앤트들은 환경에 적응하는 능력이 아주 뛰어납니다.\n" +
                "따라서 똑같은 환경에서 성장한 당신의 능력이 검증된다면 '드넓은 초원' 의 슬라임과 앤트들을 믿고 채용할 수 있을것 같습니다.\n" +
                "좋습니다. 검증은 간단합니다.\n'주식회사 더 슬라' 의 최고 강자이자 이 세계 에서 손꼽히는 강자인 '테 슬라임' 을 찾아가보십시오.");
            Quest51_CONVERSATION16.m_sQuest_Information_Clear = ("비록 슬라임과 앤트종족은 아니더라도 '드넓은 초원' 의 상황을 잘 파악하고있고, '드넓은 초원' 에서 성장해 영향력을 행사하고있는 당신.\n" +
                "저는 당신의 능력에대해 검증을 요구합니다.\n" +
                "우리 슬라임과 앤트들은 환경에 적응하는 능력이 아주 뛰어납니다.\n" +
                "따라서 똑같은 환경에서 성장한 당신의 능력이 검증된다면 '드넓은 초원' 의 슬라임과 앤트들을 믿고 채용할 수 있을것 같습니다.\n" +
                "좋습니다. 검증은 간단합니다.\n'주식회사 더 슬라' 의 최고 강자이자 이 세계 에서 손꼽히는 강자인 '테 슬라임' 을 찾아갔습니다.");
            Quest51_CONVERSATION16.m_bQuest_Information_Process_Hide = false;
            Quest51_CONVERSATION16.m_sRewardSOC = new SOC(1, 0, 0, 1, 0, 0, 0, 0, 0);
            Quest51_CONVERSATION16.m_sRewardSTATUS = new STATUS(0, 0, 50);
            Quest51_CONVERSATION16.m_nRewardGold = 0;
            Quest51_CONVERSATION16.m_sStatus_Necessity_Down.SetSTATUS_LV(12);
            Quest51_CONVERSATION16.m_ql_Quest_Necessity_Clear.Add(Quest49_CONVERSATION15);
            Quest51_CONVERSATION16.m_ql_Quest_Necessity_NonProcess.Add(Quest48_CONVERSATION14);
            Quest51_CONVERSATION16.m_ql_Quest_Necessity_NonClear.Add(Quest48_CONVERSATION14);
            m_Dictionary_QuestList_CONVERSATION.Add(Quest51_CONVERSATION16.m_nQuest_Code, Quest51_CONVERSATION16);
        }

        // [퀘스트[S5]: 스토리퀘스트]\n[검증]
        {
            Quest52_KILL_MONSTER12.AddQuestDescription_Context("이야기는 들었다.");
            Quest52_KILL_MONSTER12.AddQuestDescription_Context("'드넓은 초원' 의 모두를 위한 방법을 찾았다고.");
            Quest52_KILL_MONSTER12.AddQuestDescription_Context("[끄덕]");
            Quest52_KILL_MONSTER12.AddQuestDescription_Context("훌륭하구나. 나조차 해내지 못한것을 해내다니.");
            Quest52_KILL_MONSTER12.AddQuestDescription_Context("[당신이 해내지 못한 것이라니...? 제가 듣기로 당신은 '드넓은 초원' 에, '그래스 슬라임' 님의 뜻에 등을 돌렸다 했습니다!]");
            Quest52_KILL_MONSTER12.AddQuestDescription_Context(". . . 그를... 알고있나?");
            Quest52_KILL_MONSTER12.AddQuestDescription_Context("[예! '슬라임 협객' 에게 전해들었습니다.]");
            Quest52_KILL_MONSTER12.AddQuestDescription_Context(". . . 꽤나 많은걸 알고있군.");
            Quest52_KILL_MONSTER12.AddQuestDescription_Context("난 그저 다가올 미래를 대비하며 현실에 타협했을 뿐이다.\n어떤 멍청이('슬라임 협객') 처럼 말도 안되는 싸움을 계속하지는 않았지...");
            Quest52_KILL_MONSTER12.AddQuestDescription_Context("하지만 내가 틀린것 같군. 너에겐 정의와 용기와 지혜가 가득하구나. 낭만 합격이다.");
            Quest52_KILL_MONSTER12.AddQuestDescription_Context("'슬라임 협객' 이라... 이름을 버리고 활동한 내 오랜 친구여.. 자네가 맞았군.....");
            Quest52_KILL_MONSTER12.AddQuestDescription_Context("잡설은 여기까지. 자네는 능력을 검증받으러 날 찾아온 것일테지.");
            Quest52_KILL_MONSTER12.AddQuestDescription_Context("어떤가? 날를 꺾을수 있겠는가?");
            Quest52_KILL_MONSTER12.AddQuestOk_Context("미리 말하겠지만 준비를 많이 해야 할거다.\n'[드넓은 초원] 오목하게 가라앉은 곳' 으로 온다면 전투에 응하도록 하지.\n\n['[드넓은 초원] 오목하게 가라앉은 곳' 에서 '테 슬라임' 과의 보스전투의 진행이 가능해 집니다.]");
            Quest52_KILL_MONSTER12.AddQuestNo_Context("왜그러지? 자신없는건가?");
            Quest52_KILL_MONSTER12.AddQuestClear_Context("훗. 제법 강하군.\n자네덕에 '드넓은 초원' 에 변화의 바람이 불겠군.\n고맙다.\n\n['주식회사 더 슬라' 의 최고 강자이자 이 세계에서 손꼽히는 강자인 '테 슬라임' 과의 보스전투에서 승리했기에 관련 스탯이 대폭 상승합니다.]");
            Quest52_KILL_MONSTER12.AddQuestProgress_Context("아직 넌 날 꺾지 못했다.");
            Quest52_KILL_MONSTER12.m_sQuest_Information_Recommend = ("아직 넌 날 꺾지 못했다.\n날 이겨보도록. 진지하게 상대해 주겠다.");
            Quest52_KILL_MONSTER12.m_sQuest_Information_Process = ("날 이겨보도록. 진지하게 상대해 주겠다.\n['테 슬라임' 과의 보스전투에서 이겨야합니다.]");
            Quest52_KILL_MONSTER12.m_sQuest_Information_Condition = ("... 너의 승리다.\n['테 슬라임' 과의 보스전투에서 이겼습니다.]\n['테 슬라임' 에게 돌아가 말을 걸어봅시다.]");
            Quest52_KILL_MONSTER12.m_sQuest_Information_Clear = ("훗. 제법 강하군.\n자네덕에 '드넓은 초원' 에 변화의 바람이 불겠군.\n고맙다.\n['테 슬라임' 과의 보스전투에서 이겨 그에게 인정받았습니다.]\n[또한 '드넓은 초원' 의 슬라임과 앤트들과 '주식회사 더 슬라' 가 공존할 수 있게끔 크나큰 기여를 했습니다.]");
            Quest52_KILL_MONSTER12.AddCondition(10001, 1, 0);
            Quest52_KILL_MONSTER12.m_sStatus_Necessity_Down.SetSTATUS_LV(12);
            Quest52_KILL_MONSTER12.m_ql_Quest_Necessity_Clear.Add(Quest51_CONVERSATION16);
            Quest52_KILL_MONSTER12.m_sRewardSOC = new SOC(10, 5, 5, 5, 5, 5, 5, 5, 0);
            Quest52_KILL_MONSTER12.m_sRewardSTATUS = new STATUS(0, 0, 100, 10, 0, 0, 0, 3, 3, 3, 0, 0, 0, 3, 3, 3, 0);
            Quest52_KILL_MONSTER12.m_nRewardGold = 5000;
            m_Dictionary_QuestList_KILL_MONSTER.Add(Quest52_KILL_MONSTER12.m_nQuest_Code, Quest52_KILL_MONSTER12);
        }

        return true;
    }

    // Find Quest
    public Quest_KILL_MONSTER GetQuest_KILL_MONSTER(int questcode)
    {
        if (m_Dictionary_QuestList_KILL_MONSTER.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_KILL_MONSTER[questcode];
        }

        return null;
    }
    public Quest_KILL_TYPE GetQuest_KILL_TYPE(int questcode)
    {
        if (m_Dictionary_QuestList_KILL_TYPE.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_KILL_TYPE[questcode];
        }

        return null;
    }
    public Quest_GOAWAY_MONSTER GetQuest_GOAWAY_MONSTER(int questcode)
    {
        if (m_Dictionary_QuestList_GOAWAY_MONSTER.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_GOAWAY_MONSTER[questcode];
        }

        return null;
    }
    public Quest_GOAWAY_TYPE GetQuest_GOAWAY_TYPE(int questcode)
    {
        if (m_Dictionary_QuestList_GOAWAY_TYPE.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_GOAWAY_TYPE[questcode];
        }

        return null;
    }
    public Quest_COLLECT GetQuest_COLLECT(int questcode)
    {
        if (m_Dictionary_QuestList_COLLECT.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_COLLECT[questcode];
        }

        return null;
    }
    public Quest_CONVERSATION GetQuest_CONVERSATION(int questcode)
    {
        if (m_Dictionary_QuestList_CONVERSATION.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_CONVERSATION[questcode];
        }

        return null;
    }
    public Quest_ROLL GetQuest_ROLL(int questcode)
    {
        if (m_Dictionary_QuestList_ROLL.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_ROLL[questcode];
        }

        return null;
    }
    public Quest_ELIMINATE_MONSTER GetQuest_ELIMINATE_MONSTER(int questcode)
    {
        if (m_Dictionary_QuestList_ELIMINATE_MONSTER.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_ELIMINATE_MONSTER[questcode];
        }

        return null;
    }
    public Quest_ELIMINATE_TYPE GetQuest_ELIMINATE_TYPE(int questcode)
    {
        if (m_Dictionary_QuestList_ELIMINATE_TYPE.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_ELIMINATE_TYPE[questcode];
        }

        return null;
    }

    // Return QuestType
    public E_QUEST_TYPE GetQuestType(int questcode)
    {
        if (m_Dictionary_QuestList_KILL_MONSTER.ContainsKey(questcode) == true)
        {
            return E_QUEST_TYPE.KILL_MONSTER;
        }
        else if (m_Dictionary_QuestList_KILL_TYPE.ContainsKey(questcode) == true)
        {
            return E_QUEST_TYPE.KILL_TYPE;
        }
        else if (m_Dictionary_QuestList_GOAWAY_MONSTER.ContainsKey(questcode) == true)
        {
            return E_QUEST_TYPE.GOAWAY_MONSTER;
        }
        else if (m_Dictionary_QuestList_GOAWAY_TYPE.ContainsKey(questcode) == true)
        {
            return E_QUEST_TYPE.GOAWAY_TYPE;
        }
        else if (m_Dictionary_QuestList_COLLECT.ContainsKey(questcode) == true)
        {
            return E_QUEST_TYPE.COLLECT;
        }
        else if (m_Dictionary_QuestList_CONVERSATION.ContainsKey(questcode) == true)
        {
            return E_QUEST_TYPE.CONVERSATION;
        }
        else if (m_Dictionary_QuestList_ROLL.ContainsKey(questcode) == true)
        {
            return E_QUEST_TYPE.ROLL;
        }
        else if (m_Dictionary_QuestList_ELIMINATE_MONSTER.ContainsKey(questcode) == true)
        {
            return E_QUEST_TYPE.ELIMINATE_MONSTER;
        }
        else if (m_Dictionary_QuestList_ELIMINATE_TYPE.ContainsKey(questcode) == true)
        {
            return E_QUEST_TYPE.ELIMINATE_TYPE;
        }

        return E_QUEST_TYPE.NULL;
    }

    // 해당 퀘스트 정보 분석
    // 퀘스트 수행 사전 조건
    public bool GetQuest_Info_PreCondition(int questcode)
    {
        if (m_Dictionary_QuestList_KILL_MONSTER.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_KILL_MONSTER[questcode].Check_Condition_Total();
        }
        else if (m_Dictionary_QuestList_KILL_TYPE.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_KILL_TYPE[questcode].Check_Condition_Total();
        }
        else if (m_Dictionary_QuestList_GOAWAY_MONSTER.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_GOAWAY_MONSTER[questcode].Check_Condition_Total();
        }
        else if (m_Dictionary_QuestList_GOAWAY_TYPE.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_GOAWAY_TYPE[questcode].Check_Condition_Total();
        }
        else if (m_Dictionary_QuestList_COLLECT.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_COLLECT[questcode].Check_Condition_Total();
        }
        else if (m_Dictionary_QuestList_CONVERSATION.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_CONVERSATION[questcode].Check_Condition_Total();
        }
        else if (m_Dictionary_QuestList_ROLL.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_ROLL[questcode].Check_Condition_Total();
        }
        else if (m_Dictionary_QuestList_ELIMINATE_MONSTER.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_ELIMINATE_MONSTER[questcode].Check_Condition_Total();
        }
        else if (m_Dictionary_QuestList_ELIMINATE_TYPE.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_ELIMINATE_TYPE[questcode].Check_Condition_Total();
        }
        else
            return false;
    }
    // 퀘스트 진행 여부
    public bool GetQuest_Info_ProgressCondition(int questcode)
    {
        if (m_Dictionary_QuestList_KILL_MONSTER.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_KILL_MONSTER[questcode].m_bProcess;
        }
        else if (m_Dictionary_QuestList_KILL_TYPE.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_KILL_TYPE[questcode].m_bProcess;
        }
        else if (m_Dictionary_QuestList_GOAWAY_MONSTER.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_GOAWAY_MONSTER[questcode].m_bProcess;
        }
        else if (m_Dictionary_QuestList_GOAWAY_TYPE.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_GOAWAY_TYPE[questcode].m_bProcess;
        }
        else if (m_Dictionary_QuestList_COLLECT.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_COLLECT[questcode].m_bProcess;
        }
        else if (m_Dictionary_QuestList_CONVERSATION.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_CONVERSATION[questcode].m_bProcess;
        }
        else if (m_Dictionary_QuestList_ROLL.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_ROLL[questcode].m_bProcess;
        }
        else if (m_Dictionary_QuestList_ELIMINATE_MONSTER.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_ELIMINATE_MONSTER[questcode].m_bProcess;
        }
        else if (m_Dictionary_QuestList_ELIMINATE_TYPE.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_ELIMINATE_TYPE[questcode].m_bProcess;
        }
        else
            return false;
    }
    // 퀘스트 클리어 여부
    public bool GetQuest_Info_CurrentCondition (int questcode)
    {
        if (m_Dictionary_QuestList_KILL_MONSTER.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_KILL_MONSTER[questcode].m_bClear;
        }
        else if (m_Dictionary_QuestList_KILL_TYPE.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_KILL_TYPE[questcode].m_bClear;
        }
        else if (m_Dictionary_QuestList_GOAWAY_MONSTER.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_GOAWAY_MONSTER[questcode].m_bClear;
        }
        else if (m_Dictionary_QuestList_GOAWAY_TYPE.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_GOAWAY_TYPE[questcode].m_bClear;
        }
        else if (m_Dictionary_QuestList_COLLECT.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_COLLECT[questcode].m_bClear;
        }
        else if (m_Dictionary_QuestList_CONVERSATION.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_CONVERSATION[questcode].m_bClear;
        }
        else if (m_Dictionary_QuestList_ROLL.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_ROLL[questcode].m_bClear;
        }
        else if (m_Dictionary_QuestList_ELIMINATE_MONSTER.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_ELIMINATE_MONSTER[questcode].m_bClear;
        }
        else if (m_Dictionary_QuestList_ELIMINATE_TYPE.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_ELIMINATE_TYPE[questcode].m_bClear;
        }
        else
            return false;
    }

    // Find Quest Order(수락순서, 클리어순서)
    public int GetQuest_Order(int questcode)
    {
        if (m_Dictionary_QuestList_KILL_MONSTER.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_KILL_MONSTER[questcode].m_nQuestOrder;
        }
        else if (m_Dictionary_QuestList_KILL_TYPE.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_KILL_TYPE[questcode].m_nQuestOrder;
        }
        else if (m_Dictionary_QuestList_GOAWAY_MONSTER.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_GOAWAY_MONSTER[questcode].m_nQuestOrder;
        }
        else if (m_Dictionary_QuestList_GOAWAY_TYPE.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_GOAWAY_TYPE[questcode].m_nQuestOrder;
        }
        else if (m_Dictionary_QuestList_COLLECT.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_COLLECT[questcode].m_nQuestOrder;
        }
        else if (m_Dictionary_QuestList_CONVERSATION.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_CONVERSATION[questcode].m_nQuestOrder;
        }
        else if (m_Dictionary_QuestList_ROLL.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_ROLL[questcode].m_nQuestOrder;
        }
        else if (m_Dictionary_QuestList_ELIMINATE_MONSTER.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_ELIMINATE_MONSTER[questcode].m_nQuestOrder;
        }
        else if (m_Dictionary_QuestList_ELIMINATE_TYPE.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_ELIMINATE_TYPE[questcode].m_nQuestOrder;
        }
        else
        {
            return -1;
        }
    }

    // Find Quest Order(추천순서)
    public int GetQuest_Order_Recommend(int questcode)
    {
        if (m_Dictionary_QuestList_KILL_MONSTER.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_KILL_MONSTER[questcode].m_nQuest_Loadmap_Code;
        }
        else if (m_Dictionary_QuestList_KILL_TYPE.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_KILL_TYPE[questcode].m_nQuest_Loadmap_Code;
        }
        else if (m_Dictionary_QuestList_GOAWAY_MONSTER.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_GOAWAY_MONSTER[questcode].m_nQuest_Loadmap_Code;
        }
        else if (m_Dictionary_QuestList_GOAWAY_TYPE.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_GOAWAY_TYPE[questcode].m_nQuest_Loadmap_Code;
        }
        else if (m_Dictionary_QuestList_COLLECT.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_COLLECT[questcode].m_nQuest_Loadmap_Code;
        }
        else if (m_Dictionary_QuestList_CONVERSATION.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_CONVERSATION[questcode].m_nQuest_Loadmap_Code;
        }
        else if (m_Dictionary_QuestList_ROLL.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_ROLL[questcode].m_nQuest_Loadmap_Code;
        }
        else if (m_Dictionary_QuestList_ELIMINATE_MONSTER.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_ELIMINATE_MONSTER[questcode].m_nQuest_Loadmap_Code;
        }
        else if (m_Dictionary_QuestList_ELIMINATE_TYPE.ContainsKey(questcode) == true)
        {
            return m_Dictionary_QuestList_ELIMINATE_TYPE[questcode].m_nQuest_Loadmap_Code;
        }
        else
        {
            return -1;
        }
    }

}
