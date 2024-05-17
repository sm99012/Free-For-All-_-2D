using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationManager : MonoBehaviour
{
    private static ConversationManager instance = null;

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
    public static ConversationManager Instance
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

    public Dictionary <int, Conversation> m_Dictionary_ConversationList;

    public void InitialSet()
    {
        m_Dictionary_ConversationList = new Dictionary <int, Conversation>();

        Conversation c1 = new Conversation("[대화]\n[길을 잃었는가 젊은이여?]", 1);
        c1.Add_Conversation_Content("끌끌끌...");
        c1.Add_Conversation_Content("젊은이...\n길을 잃었나...?");
        c1.Add_Conversation_Content("이곳은 '드넓은 초원' 변두리의 '깊디깊은숲' 어딘가라네...");
        m_Dictionary_ConversationList.Add(1, c1);

        Conversation c2 = new Conversation("[대화]\n[왈!왈!]", 2);
        c2.Add_Conversation_Content("왈왈! 왈?! 왈~~왈!");
        c2.Add_Conversation_Content("으르르르르르르~~~ 왈!\n왈!\n왈!!!");
        m_Dictionary_ConversationList.Add(2, c2);

        Conversation c3 = new Conversation("[대화]\n['감쟈' 에 대하여..]", 3);
        c3.Add_Conversation_Content("오! 자네로군..!");
        c3.Add_Conversation_Content("용케 '감쟈' 에게 '단단한 뼈' 를 가져다 주었구먼~ 아주 대단허이~");
        c3.Add_Conversation_Content("여전히 '감쟈' 는 자네를 아주 좋아한다네~\n아 참! 동물에관한 평판이 오른다면 아주 유용할걸세~");
        c3.m_ql_Quest_Necessity_Clear.Add(QuestManager.Instance.GetQuest_COLLECT(4001));
        m_Dictionary_ConversationList.Add(3, c3);

        Conversation c4 = new Conversation("[대화]\n[냐~옹]", 4);
        c4.Add_Conversation_Content("냐~~~옹\n\n[고양이가 마른 나뭇가지 위에 위태로이 앉아있습니다.]\n[그러나 정작 고양이는 평온해 보입니다.]");
        m_Dictionary_ConversationList.Add(4, c4);

        Conversation c5 = new Conversation("[대화]\n[훈련만이 살길이다.]", 5);
        c5.Add_Conversation_Content("뭐냐 애송이..");
        c5.Add_Conversation_Content("나는 나약한놈과 대화하지 않는다!\n\n[강해진 후 다시 말을 걸어봅시다.]");
        c5.m_sStatus_Necessity_Up.SetSTATUS_LV(4);
        m_Dictionary_ConversationList.Add(5, c5);

        Conversation c6 = new Conversation("[대화]\n[훈련만이 살길이다.]", 6);
        c6.Add_Conversation_Content("호오 애송이주제에 제법 강해보이는군..!!");
        c6.Add_Conversation_Content("나는 '주식회사 더 슬라' 의 훈련대장이다.\n우리 회사는 능력있는 인재를 종족 상관없이 등용하지.");
        c6.Add_Conversation_Content("너의 능력을 증명해 보도록!");
        c6.m_sStatus_Necessity_Down.SetSTATUS_LV(5);
        m_Dictionary_ConversationList.Add(6, c6);

        Conversation c7 = new Conversation("[대화]\n['드넓은 초원' 으로]", 7);
        c7.Add_Conversation_Content("끌끌끌..");
        c7.Add_Conversation_Content("앞으로의 자네의 모험을 응원하지..!");
        c7.m_ql_Quest_Necessity_Clear.Add(QuestManager.Instance.GetQuest_CONVERSATION(5005));
        m_Dictionary_ConversationList.Add(7, c7);

        Conversation c8 = new Conversation("[대화]\n[배고파...]", 8);
        c8.Add_Conversation_Content("배고파....\n(꼬로록)");
        c8.Add_Conversation_Content("아무리 배가 고프더라도 '주식회사 더 슬라' 의 보급대장으로써 보급품에 손을대는것은.....!!\n(꼬로록)\n아.. 딱 하나만...?");
        m_Dictionary_ConversationList.Add(8, c8);

        Conversation c9 = new Conversation("[대화]\n[ㅋㅋ]", 9);
        c9.Add_Conversation_Content("뭘봐ㅋㅋ\n\n[..?]");
        c9.Add_Conversation_Content("ㅋㅋ\n\n[.....? 당신은 이유없이 비웃음 당해 불쾌해 졌습니다.]");
        m_Dictionary_ConversationList.Add(9, c9);

        Conversation c10 = new Conversation("[대화]\n[ㅋ]", 10);
        c10.Add_Conversation_Content("뭐ㅋ\n\n[....?뭐요]");
        c10.Add_Conversation_Content("아닌데ㅋ\n\n[...?뭐가 아닌데요?]");
        c10.Add_Conversation_Content("어쩌라고ㅋ\n\n[......???? 당신은 이유없이 싸가지없음을 느껴 불쾌해 졌습니다.]");
        m_Dictionary_ConversationList.Add(10, c10);

        Conversation c11 = new Conversation("[대화]\n[ㅋㅋㅋ]", 11);
        c11.Add_Conversation_Content("ㅋㅋㅋ\n\n[..ㅡㅡ]");
        c11.Add_Conversation_Content("ㅋㅋㅋ\n\n[.....ㅡㅡ 당신은 '쪼개는 초원 슬라임' 과 '싸가지없는 초원 슬라임' 에게 농락당한걸 생각하며 분노합니다.]");
        c11.m_ql_Quest_Necessity_Clear.Add(QuestManager.Instance.GetQuest_CONVERSATION(5003));
        m_Dictionary_ConversationList.Add(11, c11);

        Conversation c12 = new Conversation("[대화]\n[행복이란?]", 12);
        c12.Add_Conversation_Content("오늘도 '드넓은 초원' 의 날씨는 좋구나~ 행복해~");
        m_Dictionary_ConversationList.Add(12, c12);

        Conversation c13 = new Conversation("[대화]\n['천년 묵은 짙은 앤트' 와의 대화 1]", 13);
        c13.Add_Conversation_Content("허허.. 오랜만에 보는 사람이구먼....");
        c13.Add_Conversation_Content("난 천년동안 이 '드넓은 초원' 에서 살아가고있다네....");
        c13.Add_Conversation_Content("'드넓은 초원' 에서는 주로 슬라임과 앤트들이 살아가고있다네.....");
        c13.Add_Conversation_Content("예전에는 서로 도우며 잘 살아왔지만.... \n'그' 가 사라지고부터는 서로를 미워하고 있다네.......");
        c13.Add_Conversation_Content("'드넓은 초원' 의 갈등을 하루 빨리 해결해야할텐데....");
        m_Dictionary_ConversationList.Add(13, c13);

        Conversation c14 = new Conversation("[대화]\n['천년 묵은 짙은 앤트' 와의 대화 2]", 14);
        c14.Add_Conversation_Content("허허.. 왜 앤트와 슬라임들이 서로를 미워하는지 궁금한가.....?");
        c14.Add_Conversation_Content("탐욕 때문이지....");
        c14.Add_Conversation_Content("서로의 것을 나누고 베풀며 살아왔던 예전과 달리.....\n'주식회사 더 슬라' 가 우리의 '드넓은 초원' 에 등장하고부터는.....\n많은것이 바뀌었네......");
        c14.Add_Conversation_Content("환경에 적응하며 살아가는 슬라임들이 탐욕스럽게 변하며.... 온순했던 앤트들이 살아남기위해....\n이제는 서로의 것을 탐내고 시기하지.......\n그렇게 우리의 '드넓은 초원' 은 점점 병들어 가는 중일세...");
        c14.Add_Conversation_Content("....");
        c14.m_ql_Quest_Necessity_Clear.Add(QuestManager.Instance.GetQuest_GOAWAY_TYPE(3000));
        m_Dictionary_ConversationList.Add(14, c14);

        Conversation c15 = new Conversation("[대화]\n[세상은 돈이 전부.]", 15);
        c15.Add_Conversation_Content("저는 '주식회사 더 슬라' 소속 '골드 타임 슬라임' 이라고 합니다.\n이곳 '드넓은 초원' 의 지부장을 맡고있습니다.");
        c15.Add_Conversation_Content("세상은 돈이 전부입니다.\n돈은 모든것을 해결해주죠. 관계, 명예 나아가 죽음까지.\n안되는것은 없습니다.");
        c15.Add_Conversation_Content("저희가 구할 수 없는것은 없습니다.\n정보.\n값진 물건.\n심지어 구할수 없는것도 구할 수 있죠...\n당신과 저희의 신뢰 관계가 돈독해 진다면 은밀한 물건도 판매 할 수 있겠군요...");
        m_Dictionary_ConversationList.Add(15, c15);

        Conversation c16 = new Conversation("[대화]\n[방해꾼]", 16);
        c16.Add_Conversation_Content("최근들어 저희 회사의 영업을 방해하는 무리가 나타나고 있습니다.");
        c16.Add_Conversation_Content("슬라임과 앤트의 권리를 주장하더군요. 들어보면 개소리지만요.");
        c16.Add_Conversation_Content("거적대기를 입은 슬라임을 조심하십쇼. 분명 우리의 거래에 방해가 될것입니다.");
        c16.m_ql_Quest_Necessity_Clear.Add(QuestManager.Instance.GetQuest_COLLECT(4003));
        m_Dictionary_ConversationList.Add(16, c16);

        Conversation c17 = new Conversation("[대화]\n[슬라임 인권을 위한 긴 여정]", 18);
        c17.Add_Conversation_Content("안녕! 나는 '슬라임 인권 협회' 소속 '잔디머리 초원 슬라임' !!");
        c17.Add_Conversation_Content("우리 슬라임들의 인권을 위해 노력하는중이야!!!");
        m_Dictionary_ConversationList.Add(17, c17);

        Conversation c18 = new Conversation("[대화]\n[폭력입니다!]", 18);
        c18.Add_Conversation_Content("우리 슬라임들은 약품이 아니다!");
        c18.Add_Conversation_Content("약품이 아니라 폭력이다!\n폭력이 아니라 폭립이란말이다!!!?");
        c18.m_ql_Quest_Necessity_Clear.Add(QuestManager.Instance.GetQuest_KILL_MONSTER(0005));
        m_Dictionary_ConversationList.Add(18, c18);

        Conversation c19 = new Conversation("[대화]\n[. . .]", 19);
        c19.Add_Conversation_Content(". . .");
        c19.m_ql_Quest_Necessity_NonClear.Add(QuestManager.Instance.GetQuest_COLLECT(4005));
        m_Dictionary_ConversationList.Add(19, c19);

        Conversation c20 = new Conversation("[대화]\n[. . .]", 20);
        c20.Add_Conversation_Content(". . .\n\n['작은 바위' 는 더이상 외로워 보이지 않습니다.]");
        c20.m_ql_Quest_Necessity_Clear.Add(QuestManager.Instance.GetQuest_COLLECT(4005));
        m_Dictionary_ConversationList.Add(20, c20);

        Conversation c21 = new Conversation("[대화\n[위대한 누군가를 기리며. . .]", 21);
        c21.Add_Conversation_Content("[관리 되지 않은 비석에는 이렇게 쓰여있다.]");
        c21.Add_Conversation_Content("['XXX X라X'. 이곳에 잠들다.]");
        c21.Add_Conversation_Content("['XXX X라X' 이 누군지는 알 수 없지만 '드넓은 초원' 에 큰 영향을 미친 인물임에는 확실한것 같습니다.]");
        c21.m_ql_Quest_Necessity_Clear.Add(QuestManager.Instance.GetQuest_CONVERSATION(5000));
        m_Dictionary_ConversationList.Add(21, c21);

        Conversation c22 = new Conversation("[대화]\n[. . .]", 22);
        c22.Add_Conversation_Content(". . .");
        m_Dictionary_ConversationList.Add(22, c22);

        Conversation c23 = new Conversation("[대화]\n[. . .]", 23);
        c23.Add_Conversation_Content("[사용하기엔 너무나 낡은 목검입니다.]\n[금방이라도 부스러질 것 같아 드는 것조차 할 수 없습니다.]");
        m_Dictionary_ConversationList.Add(23, c23);

        Conversation c24 = new Conversation("[대화]\n[힘든 초보 모험가의 인생]", 24);
        c24.Add_Conversation_Content("안녕하세요..ㅎ");
        c24.Add_Conversation_Content("모험가라는 직업은 참 여러모로 한계가 많은 것 같네요..\n제가 선택한 모험가지만 요즘 들어 많은 생각이 드네요..\n힘들어요..");
        c24.Add_Conversation_Content("이럴 거면 '드넓은 초원' 에 정착해서 농사나 짓는 게 어떨지..");
        c24.m_ql_Quest_Necessity_NonClear.Add(QuestManager.Instance.GetQuest_COLLECT(4006));
        m_Dictionary_ConversationList.Add(24, c24);

        Conversation c25 = new Conversation("[대화]\n[즐거운 초보 모험가의 인생]", 25);
        c25.Add_Conversation_Content("안녕하세요!");
        c25.Add_Conversation_Content("주로 모험가 관련 일을 하지만 취미로 '드넓은 초원' 전역의 '수풀' 제거 총괄직을 맡고있는 '슬레나르' 입니다!");
        c25.Add_Conversation_Content("모험가님 덕분에 제가 처한 문제를 잘 해결할 수 있었습니다. 감사합니다!");
        c25.m_ql_Quest_Necessity_Clear.Add(QuestManager.Instance.GetQuest_COLLECT(4006));
        m_Dictionary_ConversationList.Add(25, c25);

        Conversation c26 = new Conversation("[대화]\n[인생은 도끼 두자루]", 26);
        c26.Add_Conversation_Content("킁.");
        c26.Add_Conversation_Content("내 인생에서 도끼 두자루는 빼놓을 수 없지!");
        c26.Add_Conversation_Content("난 이 도끼들과 함께 용병의 길을 걸어왔지. 주변 시선에 아랑곳하지 않고 나의 길을..");
        c26.Add_Conversation_Content("그 결과 중급 용병으로 승급했고 내 도끼들과는 더욱더 각별한 사이가 되었지! 도끼는 내 인생이야!");
        m_Dictionary_ConversationList.Add(26, c26);

        Conversation c27 = new Conversation("[대화]\n[어~~~~으!~~이~!~에~~]", 27);
        c27.Add_Conversation_Content("어으!!!~~~~zzz... ~~우으이에~~!~아~~~");
        c27.Add_Conversation_Content("허~.....zzz\n\n['술무라임' 이 너무 취해 있습니다.]");
        m_Dictionary_ConversationList.Add(27, c27);

        Conversation c28 = new Conversation("[대화]\n[그리움]", 28);
        c28.Add_Conversation_Content("용병의 길은 쉽지 않은 길이다 아입니까.");
        c28.Add_Conversation_Content("하지만 사랑하는 가족을 위해서는 단호한 결단력이 필요합니다");
        c28.Add_Conversation_Content("오늘따라 어머니가 보고싶다 안캅니까...");
        m_Dictionary_ConversationList.Add(28, c28);

        Conversation c29 = new Conversation("[대화]\n[에구에구]", 29);
        c29.Add_Conversation_Content("비가 올라 카는강... 허리가 쑤신다 안카나!");
        c29.Add_Conversation_Content("울 아들내미는 어디서 뭘 하는지....");
        m_Dictionary_ConversationList.Add(29, c29);

        Conversation c30 = new Conversation("[대화]\n[반가워!]", 30);
        c30.Add_Conversation_Content("반가워! '드넓은 초원' 에서 사람은 오랜만에 보는구나!!");
        c30.Add_Conversation_Content("나는 장사를 하고있어. 아직은 부족하지만 앞으로 잘 부탁해~");
        m_Dictionary_ConversationList.Add(30, c30);

        Conversation c31 = new Conversation("[대화]\n[. . .]", 31);
        c31.Add_Conversation_Content("[나무 아래에 한 슬라임이 숨어있습니다.]");
        c31.m_ql_Quest_Necessity_NonClear.Add(QuestManager.Instance.GetQuest_GOAWAY_TYPE(3002));
        //c31.m_sStatus_Necessity_Up.SetSTATUS_LV(7);
        m_Dictionary_ConversationList.Add(31, c31);

        Conversation c32 = new Conversation("[대화]\n[. . . 이봐]", 32);
        c32.Add_Conversation_Content("[나무 아래에 숨어있는 슬라임이 말을 걸어옵니다.]");
        c32.Add_Conversation_Content("난 오랬동안 널 봐왔지. '깊디깊은숲' 에서 '드넓은 초원' 으로 나왔을 때부터.");
        c32.Add_Conversation_Content("이젠 제법 강해졌고 영향력이 커졌군...");
        c32.Add_Conversation_Content("그러나 아직 부족하다네... 좀 더 성장하고 영향력을.... 이제 멀지 않았다네...!");
        c32.m_ql_Quest_Necessity_Clear.Add(QuestManager.Instance.GetQuest_GOAWAY_TYPE(3002));
        c32.m_ql_Quest_Necessity_NonClear.Add(QuestManager.Instance.GetQuest_COLLECT(4014));
        c32.m_sStatus_Necessity_Down.SetSTATUS_LV(7);
        //c32.m_sStatus_Necessity_Up.SetSTATUS_LV(8);
        m_Dictionary_ConversationList.Add(32, c32);

        Conversation c33 = new Conversation("[대화]\n['드넓은 초원' 의 진정한 평화]", 33);
        c33.Add_Conversation_Content("! ! !");
        c33.Add_Conversation_Content("때가 되었나...");
        c33.Add_Conversation_Content("나는 '드넓은 초원' 의 영원한 벗 '그래스 슬라임' 님의 제자라네.\n. . . 현재 내 이름을 버리고 '슬라임 협객' 으로 살아가고 있지.");
        c33.Add_Conversation_Content("스승님께서 '깊디깊은숲' 너머로 사라지신 이후로 줄곧 혼자서 '드넓은 초원' 의 진정한 평화를 위해 힘써왔지.");
        c33.Add_Conversation_Content("'드넓은 초원' 의 슬라임들과 앤트들을 핍박하며 돈을 벌고있는 '주식회사 더 슬라'...!");
        c33.Add_Conversation_Content("'주식회사 더 슬라' 의 개가 되어 가난에 찌든 슬라임들과 앤트들을 더러운 일에 끌어들이고 있는 '용병단'...!");
        c33.Add_Conversation_Content("마지막으로...");
        c33.Add_Conversation_Content("'드넓은 초원' 에서 나고 자랐으며 '그래스 슬라임' 님이 가장 아꼈음에도...");
        c33.Add_Conversation_Content("누구보다도 '드넓은 초원' 의 문제를 잘 알고있던 '테 슬라임' 까지..!");
        c33.Add_Conversation_Content("이들을 몰아내고 스승님과 함께 했던 평화로운 시절로 돌아갈것이라네!");
        c33.m_ql_Quest_Necessity_Clear.Add(QuestManager.Instance.GetQuest_COLLECT(4014));
        c33.m_sStatus_Necessity_Down.SetSTATUS_LV(12);
        m_Dictionary_ConversationList.Add(33, c33);

        Conversation c34 = new Conversation("[대화]\n[꼬마 앤트의 꿈]", 34);
        c34.Add_Conversation_Content("콜록! 콜록!");
        c34.Add_Conversation_Content("비록 난 약하지만..");
        c34.Add_Conversation_Content("포기하지 않겠어..!!");
        c34.Add_Conversation_Content("나도 언젠가는 다른 친구들처럼 뛰어 놀 수 있을거야!");
        m_Dictionary_ConversationList.Add(34, c34);

        Conversation c35 = new Conversation("[대화]\n[]", 35);
        c35.Add_Conversation_Content("");
        m_Dictionary_ConversationList.Add(35, c35);

        Conversation c36 = new Conversation("[대화]\n[]", 36);
        c36.Add_Conversation_Content("");
        m_Dictionary_ConversationList.Add(36, c36);

        Conversation c37 = new Conversation("[대화]\n[]", 37);
        c37.Add_Conversation_Content("");
        m_Dictionary_ConversationList.Add(37, c37);

        Conversation c38 = new Conversation("[대화]\n[]", 38);
        c38.Add_Conversation_Content("");
        m_Dictionary_ConversationList.Add(38, c38);


        //Conversation c1 = new Conversation("[대화]\n[???]");
        //c1.Add_Conversation_Content("...");
        //c1.Add_Conversation_Content("...\n드디어 왔구나..");
        //c1.Add_Conversation_Content("너를 아주 오랬동안 기다리고 있었어.");
        //c1.Add_Conversation_Content("부디 이번생에도 니가가진 힘(타임루프)으로 이 세상을 즐겁게 모험하렴.");
        //m_cl_ConversationList.Add(c1);

        //Conversation c0 = new Conversation("[대화]\n[슬라입 협객 과의 대화 1]");
        //c0.Add_Conversation_Content("어서오시게 모험가여...!");
        //c0.Add_Conversation_Content("나는 이곳 '드넓은 초원'의 평화를 지키는 '슬라임 협객'..");
        //c0.Add_Conversation_Content(".........!");
        //c0.Add_Conversation_Content("뭔가 용무라도..?");
        //m_cl_ConversationList.Add(c0);

        //Conversation c1 = new Conversation("[대화]\n[슬라입 협객 과의 대화 2]");
        //c1.Add_Conversation_Content("오 또만났군 모험가여...!");
        //c1.Add_Conversation_Content("사실 이곳 '드넓은 초원' 에는 '그린 슬라임' 이라는 강력한 슬라임이 있었다네..");
        //c1.Add_Conversation_Content("그는 오랜 시간동안 '드넓은 초원'을 조율해왔네..\n때로는 말로..\n때로는 힘으로...\n.........심지어 같은 종족인 슬라임들 또한 예외는 없었다네..");
        //c1.Add_Conversation_Content("그렇다네...\n그는 아주 올곧으며 정의로운 슬라임이었네...");
        //c1.Add_Conversation_Content("하지만 그는 이제 늙고 병들었지...\n그렇기때문에 이몸 '슬라임 협객'께서 '드넓은 초원'의 평화를 되찾기위해....!\n노력중이라네....!!!");
        //c1.Add_Conversation_Content("부디 '드넓은 초원'의 평화를 헤치는 일이 없기를...");
        //c1.m_ql_Quest_Necessity_Clear.Add(QuestManager.Instance.m_ql_QuestList_KILL_MONSTER[0]);
        //m_cl_ConversationList.Add(c1);

        //Conversation c6 = new Conversation("[대화]\n[테 슬라임 과의 대화 1]");
        //c6.Add_Conversation_Content("나는 '일론 머 슬라임 상인단' 의 경비대장 '테 슬라임'.");
        //c6.Add_Conversation_Content("세상은 불합리하다.\n노력해라.\n...바뀔것이다.");
        //c6.Add_Conversation_Content("다친다...\n가라.");
        //m_cl_ConversationList.Add(c6);
    }
}
