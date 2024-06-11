﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Total : MonoBehaviour
{
    // 플레이어에 관한 모든것이 Player_Total.cs를 거친다.
    // 싱글톤패턴을 이용했다.(오직 하나의 개체만 존재, 접근성 용이)
    private static Player_Total instance = null;
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
    public static Player_Total Instance
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

    // Has-a 관계로 Player_Total.cs는 아래 클래스를 포함한다.
    // Player_Total.cs에서 특정 함수가 실행되면 아래 클래스의 특정 함수를 호출한다.
    public Player_Status m_ps_Status;       // 플레이어 스탯(능력치, 평판)
    public Player_Move m_pm_Move;           // 플레이어 움직임
    public Player_Itemslot m_pi_Itemslot;   // 플레이어 인벤토리
    public Player_Equipment m_pe_Equipment; // 플레이어 장비창
    public Player_Effect m_pe_Effect;       // 플레이어 이펙트
    public Player_Quest m_pq_Quest;         // 플레이어 퀘스트
    public Player_Skill m_ps_Skill;         // 플레이어 스킬
    public Player_Camera m_pc_Camera;       // 플레이어 카메라
    public Player_Map m_pm_Map;             // 플레이어 맵

    public int hInput;      // 수평이동 값
    public int vInput;      // 수직이동 값
    public int m_nPosValue; // 플레이어가 바라보는 방향(2D 탑다운 게임이지만 플레이어는 우측과 좌측만 바라보고 게임을 진행한다.)
    
    // 플레이어가 착용한 무기분류에 따라 상이한 공격 패턴(공격범위, 공격력 계수, 공격 타이밍)을 가진다.
    // Sword(검) 공격범위
    GameObject m_gAttack_Area_Sword;
    GameObject m_gAttack1_1_Area_Sword;
    BoxCollider2D m_BoxCollider_Attack1_1_Area_Sword; // Sword(검) 기본 공격1 공격범위
    GameObject m_gAttack1_2_Area_Sword;
    BoxCollider2D m_BoxCollider_Attack1_2_Area_Sword; // Sword(검) 기본 공격2 공격범위
    GameObject m_gAttack1_3_Area_Sword;
    BoxCollider2D m_BoxCollider_Attack1_3_Area_Sword; // Sword(검) 기본 공격3 공격범위
    // Axe(도끼) 공격범위
    GameObject m_gAttack_Area_Axe;
    GameObject m_gAttack1_1_Area_Axe;
    BoxCollider2D m_BoxCollider_Attack1_1_Area_Axe; // Axe(도끼) 기본 공격1 공격범위
    GameObject m_gAttack1_2_Area_Axe;
    BoxCollider2D m_BoxCollider_Attack1_2_Area_Axe; // Axe(도끼) 기본 공격2 공격범위
    GameObject m_gAttack1_3_Area_Axe;
    BoxCollider2D m_BoxCollider_Attack1_3_Area_Axe; // Axe(도끼) 기본 공겨3 공격범위
    // Knife(단검) 공격범위
    GameObject m_gAttack_Area_Knife;
    GameObject m_gAttack1_1_Area_Knife;
    BoxCollider2D m_BoxCollider_Attack1_1_Area_Knife; // Knife(단검) 기본 공격1 공격범위
    GameObject m_gAttack1_2_Area_Knife;
    BoxCollider2D m_BoxCollider_Attack1_2_Area_Knife; // Knife(단검) 기본 공격2 공격범위
    GameObject m_gAttack1_3_Area_Knife;
    BoxCollider2D m_BoxCollider_Attack1_3_Area_Knife; // Knife(단검) 기본 공격3 공격범위
    
    Collider2D[] co2_1;   // 플레이어의 공격 대상(몬스터, 파괴 가능한 오브젝트)의 콜라이더(충돌 처리를 위한 오브젝트)
    Vector3 m_vAttackPos; // 공격 지점(타격 이펙트가 연출되는 지점)
    int AttackDamage;     // 공격 데미지

    Collider2D[] co2_2; // 플레이어의 놓아주기 대상(몬스터)의 콜라이더(충돌 처리를 위한 오브젝트)
    SOC soc2_2;         // 플레이어의 놓아주기가 성공했을때 평판 업데이트를 위한 임시 변수

    Collider2D[] co2_3; // 플레이어의 상호작용ㆍ채집 대상의 콜라이더(충돌 처리를 위한 오브젝트)

    Collider2D[] co2_4;                                     // 아이템 줍기 대상(장비아이템, 소비아이템, 기타아이템, 골드)의 콜라이더(충돌 처리를 위한 오브젝트)
    Vector3 co2_4_Offset = new Vector3(-0.007f, 0.035f, 0); // 아이템 줍기 범위 오프셋
    Vector3 co2_4_BoxSize = new Vector3(0.13f, 0.05f, 0);   // 아이템 줍기 범위

    Vector2 m_vSize = new Vector2(0.25f, 0.35f); // 상호작용 범위

    int nLayer1 = 1 << LayerMask.NameToLayer("Monster") | 1 << LayerMask.NameToLayer("RuinableObject"); // 공격 가능한 대상의 레이어
    int nLayer2 = 1 << LayerMask.NameToLayer("Monster");                                                // 놓아주기 가능한 대상의 레이어
    int nLayer3 = 1 << LayerMask.NameToLayer("NPC") | 1 << LayerMask.NameToLayer("Collection");         // 상호작용 가능한 대상의 레이어
    int nLayer4 = 1 << LayerMask.NameToLayer("Item");                                                   // 아이템의 레이어
    
    int m_nRandomRatio; // 플레이어가 상태이상(암흑) 상태일때 제대로된 공격을 할 수 있을지 결정하는 난수.(상태이상(암흑) 상태에서는 일정 확률로 공격 시 데미지를 1밖에 주지 못한다.)

    Dictionary<int, int> m_Dictionary_SerItemEffect = new Dictionary<int, int>(); // 플레이어에게 적용중인 아이템 세트효과. Dictionary <Key : 아이템 세트효과 코드, Value : 아이템 세트효과 코드(Key)를 가진 아이템 개수>
                                                                                  //  Key == 0 : 세트효과 없음
    
    // 변수 초기화
    public void InitialSet()
    {
        // Player_Total.cs가 포함하는 클래스
        m_ps_Status = this.GetComponent<Player_Status>();
        m_pm_Move = this.GetComponent<Player_Move>();
        m_pi_Itemslot = this.GetComponent<Player_Itemslot>();
        m_pe_Equipment = this.GetComponent<Player_Equipment>();
        m_pe_Effect = this.GetComponent<Player_Effect>();
        m_pq_Quest = this.GetComponent<Player_Quest>();
        m_ps_Skill = this.GetComponent<Player_Skill>();
        m_pc_Camera = this.GetComponent<Player_Camera>();
        m_pm_Map = this.GetComponent<Player_Map>();
        
        // Player_Total.cs가 포함하는 클래스 초기화
        m_ps_Status.InitialSet();
        m_pm_Move.InitialSet();
        m_pi_Itemslot.InitialSet();
        m_pe_Equipment.InitialSet();
        m_pe_Effect.InitialSet();
        m_pq_Quest.InitialSet();
        m_ps_Skill.InitialSet();
        m_pc_Camera.InitialSet();
        m_pm_Map.InitialSet();

        // Sword(검) 공격범위 관련 변수 초기화
        m_gAttack_Area_Sword = transform.Find("Player_Attack_Sword").gameObject;
        m_gAttack1_1_Area_Sword = m_gAttack_Area_Sword.transform.Find("Attack1_1_Sword").gameObject;
        m_BoxCollider_Attack1_1_Area_Sword = m_gAttack1_1_Area_Sword.GetComponent<BoxCollider2D>();
        m_gAttack1_2_Area_Sword = m_gAttack_Area_Sword.transform.Find("Attack1_2_Sword").gameObject;
        m_BoxCollider_Attack1_2_Area_Sword = m_gAttack1_2_Area_Sword.GetComponent<BoxCollider2D>();
        m_gAttack1_3_Area_Sword = m_gAttack_Area_Sword.transform.Find("Attack1_3_Sword").gameObject;
        m_BoxCollider_Attack1_3_Area_Sword = m_gAttack1_3_Area_Sword.GetComponent<BoxCollider2D>();

        // Axe(도끼) 공격범위 관련 변수 초기화
        m_gAttack_Area_Axe = transform.Find("Player_Attack_Axe").gameObject;
        m_gAttack1_1_Area_Axe = m_gAttack_Area_Axe.transform.Find("Attack1_1_Axe").gameObject;
        m_BoxCollider_Attack1_1_Area_Axe = m_gAttack1_1_Area_Axe.GetComponent<BoxCollider2D>();
        m_gAttack1_2_Area_Axe = m_gAttack_Area_Axe.transform.Find("Attack1_2_Axe").gameObject;
        m_BoxCollider_Attack1_2_Area_Axe = m_gAttack1_2_Area_Axe.GetComponent<BoxCollider2D>();
        m_gAttack1_3_Area_Axe = m_gAttack_Area_Axe.transform.Find("Attack1_3_Axe").gameObject;
        m_BoxCollider_Attack1_3_Area_Axe = m_gAttack1_3_Area_Axe.GetComponent<BoxCollider2D>();

        // Knife(단검) 공격범위 관련 변수 초기화
        m_gAttack_Area_Knife = transform.Find("Player_Attack_Knife").gameObject;
        m_gAttack1_1_Area_Knife = m_gAttack_Area_Knife.transform.Find("Attack1_1_Knife").gameObject;
        m_BoxCollider_Attack1_1_Area_Knife = m_gAttack1_1_Area_Knife.GetComponent<BoxCollider2D>();
        m_gAttack1_2_Area_Knife = m_gAttack_Area_Knife.transform.Find("Attack1_2_Knife").gameObject;
        m_BoxCollider_Attack1_2_Area_Knife = m_gAttack1_2_Area_Knife.GetComponent<BoxCollider2D>();
        m_gAttack1_3_Area_Knife = m_gAttack_Area_Knife.transform.Find("Attack1_3_Knife").gameObject;
        m_BoxCollider_Attack1_3_Area_Knife = m_gAttack1_3_Area_Knife.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (Total_Manager.Instance.m_bStart == true && Time.timeScale != 0) // 게임을 시작 했을때, 일시 정지가 아닐때
            Controller(); // 컨트롤러
    }

    // 컨트롤러(키입력 + @) 함수
    public void Controller()
    {
        if (GUIManager_Total.Instance != null)
        {
            if (GUIManager_Total.Instance.m_GUI_Interaction.m_gPanel_ChatBox.activeSelf == false &&
                GUIManager_Total.Instance.m_GUI_ChangeMap.m_gPanel_ChangeMap.activeSelf == false) // NPC와 상호작용 하지 않을때, 맵 변경이 끝났을때
                                                                                                  //
                                                                                                  // ※ GUI상태 FSM을 추가할 예정
                                                                                                  //
            {
                // 플레이어 동작
                InputKey_Move();    // 이동(가만히 있기, 달리기), 방향설정 키입력(↑, ↓, ←, →)
                InputKey_Attack();  // 공격 키입력(A)
                InputKey_Goaway();  // 놓아주기 키입력(D)
                InputKey_Roll();    // 구르기 키입력(S)
                InputKey_GetItem(); // 아이템 줍기(키입력 필요없음)

                // 플레이어 정보GUI
                InputKey_GUI_Quest();             // 퀘스트창GUI 키입력(Q)
                InputKey_GUI_Itemslot();          // 인벤토리GUI 키입력(I)
                InputKey_GUI_ES();                // 상태창(능력치창 + 장비창 + 아이템 세트효과창)GUI. 키입력(E) InputKey_GUI_Equipslot() + InputKey_GUI_Status()
                InputKey_GUI_MonsterDictionary(); // 몬스터 도감GUI 키입력(M)
            }
            else // NPC와 상호작용 하거나, 맵 변경이 끝나지 않았을때
            {
                hInput = 0; vInput = 0;
                m_pm_Move.Cancel_Goaway(); // 놓아주기 취소
                m_pm_Move.Move(0, 0, 0); // 이동(가만히 있기)
                CameraMove(this.gameObject.transform.position); // 카메라 설정 관련 함수(카메라 중심점 판단ㆍ설정)
            }

            InputKey_Interaction(); // NPC와 상호작용ㆍ채집 키입력(SPACE)
            InputKey_GUI_ESC();     // GUI 닫기(비활성화), 일시중지GUI(옵션GUI) 키입력(ESC)
        }
    }

    // 이동(가만히 있기, 달리기), 방향설정 키입력(↑, ↓, ←, →)
    public void InputKey_Move()
    {
        if (m_pm_Move.m_bMove == true) // Player_Move.cs의 이동 관련 변수. 플레이어가 이동 가능할때
        {
            hInput = 0;
            vInput = 0;

            if (Input.GetKey(KeyCode.RightArrow)) // 수평 이동(→ 방향 이동)
            {
                hInput = 1;
                m_nPosValue = 1;
            }
            if (Input.GetKeyUp(KeyCode.RightArrow)) // 수평 이동(→ 방향 이동) 중단
                hInput = 0;
            if (Input.GetKey(KeyCode.LeftArrow)) // 수평 이동(← 방향 이동)
            {
                hInput = -1;
                m_nPosValue = -1;
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow)) // 수평 이동(← 방향 이동) 중단
                hInput = 0;
            if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow)) // 수평 이동 중단
                hInput = 0;

            if (Input.GetKey(KeyCode.UpArrow)) // 수직 이동(↓ 방향 이동)
                vInput = 1;
            if (Input.GetKeyUp(KeyCode.UpArrow)) // 수직 이동(↓ 방향 이동) 중단
                vInput = 0;
            if (Input.GetKey(KeyCode.DownArrow)) // 수직 이동(↓ 방향 이동)
                vInput = -1;
            if (Input.GetKeyUp(KeyCode.DownArrow)) // 수직 이동(↓ 방향 이동) 중단
                vInput = 0;
            if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow)) // 수직 이동 중단
                vInput = 0;

            Move();
        }
        else
        {
            hInput = 0;
            vInput = 0;

            Move();
        }
    }
    // 플레이어 이동(가만히 있기, 달리기), 방향설정 함수
    private void Move()
    {
        Player_Move.E_PLAYER_MOVE_STATE epms = Player_Move.E_PLAYER_MOVE_STATE.NULL; // 플레이어 동작 FSM 임시 변수

        // 플레이어 이동, 방향설정
        if (Player_Status.m_cCondition.ConditionCheck_Bind() == false && Player_Status.m_cCondition.ConditionCheck_Shock() == false) // 플레이어에게 상태이상(속박, 기절)이 적용중이지 않을때
        {
            epms = m_pm_Move.Move(hInput, vInput, m_ps_Status.m_sStatus.GetSTATUS_Speed()); // 플레이어 이동 함수 실행. 플레이어 동작 FSM 상태 반환
        }
        else
        {
            if (Player_Status.m_cCondition.ConditionCheck_Shock() == true) // 플레이어에게 상태이상(기절)이 적용중일때
            {
                epms = m_pm_Move.Move(0, 0, 0); // 이동 불가
            }
            else if (Player_Status.m_cCondition.ConditionCheck_Bind() == true) // 플레이어에게 상태이상(기절)이 적용중이지 않을때 && 때플레이어에게 상태이상(속박)이 적용중일때
                epms = m_pm_Move.Move(hInput, vInput, 0); // 이동 불가. 좌ㆍ우 전환만 가능

        }

        // 카메라 이동(카메라 중심점 설정)
        if (epms != Player_Move.E_PLAYER_MOVE_STATE.NULL) // 플레이어 동작 FSM { NULL(플레이어 이동 불가 상태(기절, 속박 등의 상태이상)) } 상태가 아닐때
        {
            if (epms == Player_Move.E_PLAYER_MOVE_STATE.IDLE || epms == Player_Move.E_PLAYER_MOVE_STATE.RUN ||
                epms == Player_Move.E_PLAYER_MOVE_STATE.ROLL || epms == Player_Move.E_PLAYER_MOVE_STATE.GOAWAY) // 플레이어 동작 FSM { IDEL(가만히 있기), RUN(달리기), ROLL(구르기), GOAWAY(놓아주기) } 상태일때
            {
                switch (epms)
                {
                    case Player_Move.E_PLAYER_MOVE_STATE.IDLE:
                    case Player_Move.E_PLAYER_MOVE_STATE.RUN: // 플레이어 동작 FSM { IDEL(가만히 있기), RUN(달리기) } 상태일때
                        {
                            CameraMove(this.gameObject.transform.position + (m_pm_Move.Get_MoveDir() * m_ps_Status.m_sStatus.GetSTATUS_Speed() * 0.05f * 0.01f * 1f));   // 카메라 이동에 기본 이동 계수(1f) 적용
                        }
                        break;
                    case Player_Move.E_PLAYER_MOVE_STATE.ROLL: // 플레이어 동작 FSM { ROLL(구르기) } 상태일때
                        {
                            CameraMove(this.gameObject.transform.position + (m_pm_Move.Get_MoveDir() * m_ps_Status.m_sStatus.GetSTATUS_Speed() * 0.05f * 0.01f * 1.5f)); // 카메라 이동에 구르기 이동계수(1.5f) 적용
                        }
                        break;
                }
            }
        }
        else // 플레이어 동작 FSM { NULL(플레이어 이동 불가 상태(속박, 기절 등의 상태이상)) } 상태일때
        {
            // 플레이어에게 상태이상(속박, 기절)이 적용되면 플레이어는 능동적으로 움직일 수 없다. 그렇기에 카메라 이동 또한 제한된다.
            // 이러한 경우 유일하게 플레이어가 움직일 수 있는 방법은 몬스터에게 피격당하는것이다. 이때 넉백이 일어나게 되는데 카메라는 넉백된 플레이어를 느리게 따라간다.
            // 이는 유저의 눈(카메라)또한 상태이상에 빠진것처럼 느끼게 하기 위함이다.
            CameraMove(this.gameObject.transform.position + (m_pm_Move.Get_MoveDir() * m_ps_Status.m_sStatus.GetSTATUS_Speed() * 0.05f * 0.01f * 0.5f)); // 카메라 이동에 상태이상(속박, 기절) 계수(0.5f) 적용
        }
    }

    // 공격 키입력(A)
    public void InputKey_Attack()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Attack();
        }
    }
    // 플레이어 공격 함수
    private void Attack()
    {
        if (Player_Status.m_cCondition.ConditionCheck_Shock() == false) // 플레이어에게 상태이상(기절)이 적용중이지 않을때
        {
            m_nAtk = m_pm_Move.Attack(); // 플레이어 공격 함수 실행. 기본 공격 단계 반환
        }
    }

    // 무기분류별 기본 공격 판정 관련 함수
    // 기본 공격 애니메이션의 특정 프레임에서 호출된다. 기본 공격의 종류별로 공격범위, 공격력 계수, 공격 타이밍이 다르다. 
    // Sword(검) 기본 공격 판정
    public void AttackCheck1_1_Sword()
    {
        AttackCheck(E_ITEM_EQUIP_MAINWEAPON_TYPE.SWORD, 1, 1f); // 기본 공격1 판정. 공격력 계수 : 100%
    }
    public void AttackCheck1_2_Sword()
    {
        AttackCheck(E_ITEM_EQUIP_MAINWEAPON_TYPE.SWORD, 2, 2f); // 기본 공격2 판정. 공격력 계수 : 200%
    }
    public void AttackCheck1_3_Sword()
    {
        AttackCheck(E_ITEM_EQUIP_MAINWEAPON_TYPE.SWORD, 3, 4f); // 기본 공격3 판정. 공격력 계수 : 400%
    }
    // Axe(도끼) 기본 공격 판정
    public void AttackCheck1_1_Axe()
    {
        AttackCheck(E_ITEM_EQUIP_MAINWEAPON_TYPE.AXE, 1, 1.5f); // 기본 공격1 판정. 공격력 계수 : 150%
    }
    public void AttackCheck1_2_Axe()
    {
        AttackCheck(E_ITEM_EQUIP_MAINWEAPON_TYPE.AXE, 2, 2f); // 기본 공격2 판정. 공격력 계수 : 200%
    }
    public void AttackCheck1_3_Axe()
    {
        AttackCheck(E_ITEM_EQUIP_MAINWEAPON_TYPE.AXE, 3, 6f); // 기본 공격3 판정. 공격력 계수 : 600%
    }
    // Knife(단검) 기본 공격 판정
    public void AttackCheck1_1_Knife()
    {
        AttackCheck(E_ITEM_EQUIP_MAINWEAPON_TYPE.KNIFE, 1, 1f); // 기본 공격1 판정. 공격력 계수 : 100%
    }
    public void AttackCheck1_2_Knife()
    {
        AttackCheck(E_ITEM_EQUIP_MAINWEAPON_TYPE.KNIFE, 2, 1.5f); // 기본 공격2 판정. 공격력 계수 : 150%
    }
    public void AttackCheck1_3_Knife()
    {
        AttackCheck(E_ITEM_EQUIP_MAINWEAPON_TYPE.KNIFE, 3, 2.5f); // 기본 공격3 판정. 공격력 계수 : 250%
    }

    // 기본 공격 판정 함수
    // 오버랩을 이용해 공격 범위내의 모든 오브젝트(몬스터, 파괴 가능한 오브젝트)에 특정 공격력 계수를 적용한 데미지를 가한다.
    // Physics2D.OverlapBoxAll(Vector2 point, Vector2 size, float angle, int layerMask) // point : 오버랩 지점, size : 오버랩 크기, angle : 각도, layerMask : 오버랩을 적용할 레이어 // return Collider2D[]
    void AttackCheck(E_ITEM_EQUIP_MAINWEAPON_TYPE mt, int attacknumber, float percent) // mt : 무기분류, attacknumber : 기본 공격 단계(1, 2, 3), percent : 공격력 계수
    {
        if (mt == E_ITEM_EQUIP_MAINWEAPON_TYPE.SWORD) // 무기분류 : Sword(검)
        {
            if (attacknumber == 1) // 기본 공격1
            {
                // 공격 방향 설정, 공격 범위내의 모든 오브젝트(몬스터, 파괴 가능한 오브젝트) 배열 반환(Collider2D[])
                if (m_nPosValue == 1) // → 방향 공격
                {
                    co2_1 = Physics2D.OverlapBoxAll(new Vector2(this.transform.position.x + m_BoxCollider_Attack1_1_Area_Sword.offset.x, this.transform.position.y + m_BoxCollider_Attack1_1_Area_Sword.offset.y), m_BoxCollider_Attack1_1_Area_Sword.size, 0, nLayer1);
                }
                else // ← 방향 공격
                {
                    co2_1 = Physics2D.OverlapBoxAll(new Vector2(this.transform.position.x - m_BoxCollider_Attack1_1_Area_Sword.offset.x, this.transform.position.y + m_BoxCollider_Attack1_1_Area_Sword.offset.y), m_BoxCollider_Attack1_1_Area_Sword.size, 0, nLayer1);
                }
            }
            else if (attacknumber == 2) // 기본 공격2
            {
                // 공격 방향 설정, 공격 범위내의 모든 오브젝트(몬스터, 파괴 가능한 오브젝트) 배열 반환(Collider2D[])
                if (m_nPosValue == 1) // → 방향 공격
                {
                    co2_1 = Physics2D.OverlapBoxAll(new Vector2(this.transform.position.x + m_BoxCollider_Attack1_2_Area_Sword.offset.x, this.transform.position.y + m_BoxCollider_Attack1_2_Area_Sword.offset.y), m_BoxCollider_Attack1_2_Area_Sword.size, 0, nLayer1);
                }
                else // ← 방향 공격
                {
                    co2_1 = Physics2D.OverlapBoxAll(new Vector2(this.transform.position.x - m_BoxCollider_Attack1_2_Area_Sword.offset.x, this.transform.position.y + m_BoxCollider_Attack1_2_Area_Sword.offset.y), m_BoxCollider_Attack1_2_Area_Sword.size, 0, nLayer1);
                }
            }
            else if (attacknumber == 3) // 기본 공격3
            {
                // 공격 방향 설정, 공격 범위내의 모든 오브젝트(몬스터, 파괴 가능한 오브젝트) 배열 반환(Collider2D[])
                if (m_nPosValue == 1) // → 방향 공격
                {
                    co2_1 = Physics2D.OverlapBoxAll(new Vector2(this.transform.position.x + m_BoxCollider_Attack1_3_Area_Sword.offset.x, this.transform.position.y + m_BoxCollider_Attack1_3_Area_Sword.offset.y), m_BoxCollider_Attack1_3_Area_Sword.size, 0, nLayer1);
                }
                else // ← 방향 공격
                {
                    co2_1 = Physics2D.OverlapBoxAll(new Vector2(this.transform.position.x - m_BoxCollider_Attack1_3_Area_Sword.offset.x, this.transform.position.y + m_BoxCollider_Attack1_3_Area_Sword.offset.y), m_BoxCollider_Attack1_3_Area_Sword.size, 0, nLayer1);
                }
            }
        }
        else if (mt == E_ITEM_EQUIP_MAINWEAPON_TYPE.AXE) // 무기분류 : Axe(도끼)
        {
            if (attacknumber == 1) // 기본 공격1
            {
                // 공격 방향 설정, 공격 범위내의 모든 오브젝트(몬스터, 파괴 가능한 오브젝트) 배열 반환(Collider2D[])
                if (m_nPosValue == 1) // → 방향 공격
                {
                    co2_1 = Physics2D.OverlapBoxAll(new Vector2(this.transform.position.x + m_BoxCollider_Attack1_1_Area_Axe.offset.x, this.transform.position.y + m_BoxCollider_Attack1_1_Area_Axe.offset.y), m_BoxCollider_Attack1_1_Area_Axe.size, 0, nLayer1);
                }
                else // ← 방향 공격
                {
                    co2_1 = Physics2D.OverlapBoxAll(new Vector2(this.transform.position.x - m_BoxCollider_Attack1_1_Area_Axe.offset.x, this.transform.position.y + m_BoxCollider_Attack1_1_Area_Axe.offset.y), m_BoxCollider_Attack1_1_Area_Axe.size, 0, nLayer1);
                }
            }
            else if (attacknumber == 2) // 기본 공격2
            {
                // 공격 방향 설정, 공격 범위내의 모든 오브젝트(몬스터, 파괴 가능한 오브젝트) 배열 반환(Collider2D[])
                if (m_nPosValue == 1) // → 방향 공격
                {
                    co2_1 = Physics2D.OverlapBoxAll(new Vector2(this.transform.position.x + m_BoxCollider_Attack1_2_Area_Axe.offset.x, this.transform.position.y + m_BoxCollider_Attack1_2_Area_Axe.offset.y), m_BoxCollider_Attack1_2_Area_Axe.size, 0, nLayer1);
                }
                else // ← 방향 공격
                {
                    co2_1 = Physics2D.OverlapBoxAll(new Vector2(this.transform.position.x - m_BoxCollider_Attack1_2_Area_Axe.offset.x, this.transform.position.y + m_BoxCollider_Attack1_2_Area_Axe.offset.y), m_BoxCollider_Attack1_2_Area_Axe.size, 0, nLayer1);
                }
            }
            else if (attacknumber == 3) // 기본 공격3
            {
                // 공격 방향 설정, 공격 범위내의 모든 오브젝트(몬스터, 파괴 가능한 오브젝트) 배열 반환(Collider2D[])
                if (m_nPosValue == 1) // → 방향 공격
                {
                    co2_1 = Physics2D.OverlapBoxAll(new Vector2(this.transform.position.x + m_BoxCollider_Attack1_3_Area_Axe.offset.x, this.transform.position.y + m_BoxCollider_Attack1_3_Area_Axe.offset.y), m_BoxCollider_Attack1_3_Area_Axe.size, 0, nLayer1);
                }
                else // ← 방향 공격
                {
                    co2_1 = Physics2D.OverlapBoxAll(new Vector2(this.transform.position.x - m_BoxCollider_Attack1_3_Area_Axe.offset.x, this.transform.position.y + m_BoxCollider_Attack1_3_Area_Axe.offset.y), m_BoxCollider_Attack1_3_Area_Axe.size, 0, nLayer1);
                }
            }
        }
        else if (mt == E_ITEM_EQUIP_MAINWEAPON_TYPE.KNIFE) // 무기분류 : Knife(단검)
        {
            if (attacknumber == 1) // 기본 공격1
            {
                // 공격 방향 설정, 공격 범위내의 모든 오브젝트(몬스터, 파괴 가능한 오브젝트) 배열 반환(Collider2D[])
                if (m_nPosValue == 1) // → 방향 공격
                {
                    co2_1 = Physics2D.OverlapBoxAll(new Vector2(this.transform.position.x + m_BoxCollider_Attack1_1_Area_Knife.offset.x, this.transform.position.y + m_BoxCollider_Attack1_1_Area_Knife.offset.y), m_BoxCollider_Attack1_1_Area_Knife.size, 0, nLayer1);
                }
                else // ← 방향 공격
                {
                    co2_1 = Physics2D.OverlapBoxAll(new Vector2(this.transform.position.x - m_BoxCollider_Attack1_1_Area_Knife.offset.x, this.transform.position.y + m_BoxCollider_Attack1_1_Area_Knife.offset.y), m_BoxCollider_Attack1_1_Area_Knife.size, 0, nLayer1);
                }
            }
            else if (attacknumber == 2) // 기본 공격2
            {
                // 공격 방향 설정, 공격 범위내의 모든 오브젝트(몬스터, 파괴 가능한 오브젝트) 배열 반환(Collider2D[])
                if (m_nPosValue == 1) // → 방향 공격
                {
                    co2_1 = Physics2D.OverlapBoxAll(new Vector2(this.transform.position.x + m_BoxCollider_Attack1_2_Area_Knife.offset.x, this.transform.position.y + m_BoxCollider_Attack1_2_Area_Knife.offset.y), m_BoxCollider_Attack1_2_Area_Knife.size, 0, nLayer1);
                }
                else // ← 방향 공격
                {
                    co2_1 = Physics2D.OverlapBoxAll(new Vector2(this.transform.position.x - m_BoxCollider_Attack1_2_Area_Knife.offset.x, this.transform.position.y + m_BoxCollider_Attack1_2_Area_Knife.offset.y), m_BoxCollider_Attack1_2_Area_Knife.size, 0, nLayer1);
                }
            }
            else if (attacknumber == 3) // 기본 공격3
            {
                // 공격 방향 설정, 공격 범위내의 모든 오브젝트(몬스터, 파괴 가능한 오브젝트) 배열 반환(Collider2D[])
                if (m_nPosValue == 1) // → 방향 공격
                {
                    co2_1 = Physics2D.OverlapBoxAll(new Vector2(this.transform.position.x + m_BoxCollider_Attack1_3_Area_Knife.offset.x, this.transform.position.y + m_BoxCollider_Attack1_3_Area_Knife.offset.y), m_BoxCollider_Attack1_3_Area_Knife.size, 0, nLayer1);
                }
                else // ← 방향 공격
                {
                    co2_1 = Physics2D.OverlapBoxAll(new Vector2(this.transform.position.x - m_BoxCollider_Attack1_3_Area_Knife.offset.x, this.transform.position.y + m_BoxCollider_Attack1_3_Area_Knife.offset.y), m_BoxCollider_Attack1_3_Area_Knife.size, 0, nLayer1);
                }
            }
        }

        // 상태이상(암흑) 적용에따른 데미지 설정
        if (Player_Status.m_cCondition.ConditionCheck_Dark() == false) // 플레이어에게 상태이상(암흑)이 적용중이지 않을때
        {
            AttackDamage = m_ps_Status.m_sStatus.GetSTATUS_Damage_Total(); // 데미지 = 플레이어의 능력치(데미지)
        }
        else // 플레이어에게 상태이상(암흑)이 적용중일때
        {
            m_nRandomRatio = Random.Range(1, 101); // 상태이상(암흑) 비율(등급) : 1% ~ 100% (1 <= m_nRandomRatio <= 100)

            // 상태이상(암흑) 비율(등급)과 난수(m_nRandomRatio)에 따라 데미지 계산
            if (m_nRandomRatio <= Player_Status.m_cCondition.GetDarkRatio())
            {
                AttackDamage = 1; // 데미지 = 1
            }
            else
            {
                AttackDamage = m_ps_Status.m_sStatus.GetSTATUS_Damage_Total(); // 데미지 = 플레이어의 능력치(데미지)
            }
        }

        // 공격 범위내의 모든 오브젝트에 타격 이펙트를 연출하고 데미지를 가한다.
        if (co2_1.Length > 0) // 공격 범위내의 오브젝트가 하나이상 있을 경우
        {
            for (int i = 0; i < co2_1.Length; i++)
            {
                // 공격 지점 설정
                if (co2_1[i].gameObject.layer == LayerMask.NameToLayer("Monster")) // 몬스터일 경우
                    m_vAttackPos = co2_1[i].gameObject.transform.position;
                else if (co2_1[i].gameObject.layer == LayerMask.NameToLayer("RuinableObject")) // 파괴 가능한 오브젝트일 경우
                {
                    m_vAttackPos = co2_1[i].gameObject.transform.position;
                    m_vAttackPos += new Vector3(0, 0.1f, 0); // 오프셋
                }
                //
                // ※ 공격 지점에 타격 이펙트가 연출된다. 현재 위의 코드에서 알 수 있듯이 단순히 해당 오브젝트의 위치에 타격 이펙트를 연출한다. 그러나 조금 부자연스럽다.
                //    따라서 오브젝트 각각의 타격 지점 오프셋을 정해두고 해당 위치에 타격 이펙트를 연출하도록 변경할 예정이다.
                //

                // 오브젝트에 데미지 정산 (데미지를 가한다.)
                if (co2_1[i].gameObject.tag == "Monster") // 몬스터일 경우
                {
                    if (co2_1[i].GetComponent<Monster_Total>().Attacked((int)(AttackDamage), percent, this.gameObject) == true) // 데미지를 정산하고 공격 여부를 반환
                    {
                        m_pe_Effect.Effect1(co2_1[i].transform.position); // 타격 이펙트 연출
                    }
                }
                else if (co2_1[i].gameObject.tag == "Boss") // 보스몬스터일 경우
                {
                    if (co2_1[i].gameObject.name == "TeSlime") // 보스몬스터(테 슬라임)일 경우
                    {
                        if (co2_1[i].GetComponent<TeSlime_Total>().Attacked((int)(AttackDamage), percent, this.gameObject) == true) // 데미지를 정산하고 공격 여부를 반환
                        {
                            m_pe_Effect.Effect1(co2_1[i].transform.position); // 타격 이펙트 연출
                        }
                    }
                }
            }
        }
    }

    // 몬스터 토벌 시 플레이어 업데이트(스탯(능력치(경험치 + @), 평판), 퀘스트 현황 업데이트)
    public void MobDeath(Monster_Total mt) // mt : 토벌 된 몬스터 정보
    {
        m_ps_Status.MobDeath(mt.m_ms_Status.m_sStatus_Death, mt.m_ms_Status.m_sSoc_Death); // 플레이어 스탯(능력치, 평판) 업데이트
        // 진행중인 퀘스트 현황 업데이트
        m_pq_Quest.QuestUpdate_Kill(mt.m_ms_Status.m_eMonster_Kind, mt.m_ms_Status.m_nMonsterCode);
        m_pq_Quest.QuestUpdate_Eliminate(mt.m_ms_Status.m_eMonster_Kind, mt.m_ms_Status.m_nMonsterCode);

        GUIManager_Total.Instance.Update_SS(); // 스탯GUI 업데이트
        MonsterManager.Instance.Update_Monster_Dictionary(mt.m_ms_Status.m_eMonster_Kind, mt.m_ms_Status.m_nMonsterCode); // 몬스터 도감 업데이트
    }
    public void MobDeath(TeSlime_Total mt) // mt : 토벌 된 몬스터(보스몬스터(테 슬라임)) 정보
    {
        m_ps_Status.MobDeath(mt.m_ts_Status.m_sStatus_Death, mt.m_ts_Status.m_sSoc_Death); // 플레이어 스탯(능력치, 평판) 업데이트
        // 진행중인 퀘스트 현황 업데이트
        m_pq_Quest.QuestUpdate_Kill(mt.m_ts_Status.m_eMonster_Kind, mt.m_ts_Status.m_nMonsterCode);
        m_pq_Quest.QuestUpdate_Eliminate(mt.m_ts_Status.m_eMonster_Kind, mt.m_ts_Status.m_nMonsterCode);
        GUIManager_Total.Instance.Update_SS(); // 스탯GUI 업데이트

        MonsterManager.Instance.Update_Monster_Dictionary(mt.m_ts_Status.m_eMonster_Kind,mt.m_ts_Status.m_nMonsterCode); // 몬스터 도감 업데이트

        // 보스몬스터 전투 종료(토벌 성공)
        BossManager.Instance.End_Battle_Boss_Succes();
        GUIManager_Total.Instance.UnDisplay_GUI_BossInformation(); // 보스몬스터 전투GUI 비활성화
    }

    // 스킬(버프ㆍ디버프, 상태이상) 적용 함수
    public void ApplySkill(Skill skill) // skill : 플레이어에게 적용할 스킬
    {
        m_ps_Status.ApplySkill(skill); // 스킬 적용 시 스탯(능력치, 평판), 버프ㆍ디버프, 상태이상 업데이트.
        GUIManager_Total.Instance.Update_SS(); // 스탯GUI 업데이트

        // 스킬 적용 시 피격(넉백) 적용
        if (skill.m_seSkillEffect.m_cCondition.ConditionCheck_Shock() == true) // 플레이어에게 적용할 스킬에 상태이상(기절)이 존재하는 경우
        {
            Attacked(0, this.gameObject.transform.position, 0.3f, skill.m_sSkillName); // 플레이어 피격(넉백)
                                                                                       // 데미지 : 0, 피격(넉백) 방향 : 플레이어 현재 위치, 넉백 시간 : 0.3f초, 피격 이름(정보) : 해당 스킬 이름
        }
    }

    // 플레이어 피격 함수
    // return true : 플레이어 피격됨 / return false : 플레이어 피격안됨
    public bool Attacked(int damage, Vector3 dir, float time = 0.3f, string attackedname = "???") // damage : 피격 데미지, dir : 피격(넉백) 방향, time : 넉백 시간(기본 0.3f초), attackedname : 피격 이름(정보)
    {
        if (m_pm_Move.Attacked(time, m_ps_Status.m_sStatus.GetSTATUS_Speed(), dir) == true) // 플레이어 피격 함수. 피격 여부 반환
        {
            m_ps_Status.Attacked(damage, dir); // 플레이어 피격 시 피격 데미지 계산 및 출력
            GUIManager_Total.Instance.Update_SS(); // 스탯GUI 업데이트
            Death(attackedname); // 플레이어 사망 판단

            return true;
        }
        return false;
    }

    // 놓아주기 키입력(D)
    public void InputKey_Goaway()
    {
        if (Input.GetKeyUp(KeyCode.D))
        {
            Goaway();
        }
        if (m_pm_Move.m_bGoaway_Success == true) // 놓아주기를 성공 했을때
        {
            GoawayCheck();
        }
    }
    // 플레이어 놓아주기 함수
    private void Goaway()
    {
        if (Player_Status.m_cCondition.ConditionCheck_Bind() == false)
        {
            m_pm_Move.Goaway(); // 플레이어 놓아주기 함수 실행
        }
    }
    // 놓아주기 판정 함수
    // 오버랩을 이용해 놓아주기 범위내의 모든 오브젝트(몬스터)를 놓아준다.
    // Physics2D.OverlapCircleAll(Vector2 point, float radius, int layerMask) // point : 오버랩 지점, radius : 오버랩 반지름, layerMask : 오버랩을 적용할 레이어 // return Collider2D[]
    void GoawayCheck()
    {
        co2_2 = Physics2D.OverlapCircleAll(transform.position, 0.5f, nLayer2); // 놓아주기 범위내의 모든 오브젝트(몬스터) 배열 반환(Collider2D[])

        // 놓아주기 범위내의 모든 오브젝트에 놓아주기 이펙트를 연출하고 오브젝트를 제거한다.
        if (co2_2.Length > 0) // 놓아주기 범위내의 오브젝트가 하나이상 있을 경우
        {
            for (int i = 0; i < co2_2.Length; i++)
            {
                if (co2_2[i].gameObject.tag != "Boss") // 해당 오브젝트의 태그가 "BOSS"가 아닐 경우
                {
                    // 놓아주기를 성공했을때 일정 확률(50%)로 평판을 획득할 수 있다.
                    soc2_2.SetSOC(co2_2[i].gameObject.GetComponent<Monster_Total>().Goaway()); // 임시 변수에 해당 오브젝트의 놓아주기 관련 평판(놓아주기를 성공했을때 획득할 수 있는 평판)을 저장
                    int nrandom = Random.Range(0, 101); // 평판 획득 확률 : 0% ~ 100% (0 <= m_nRandomRatio <= 100)
                    if (nrandom <= 50)
                    {
                        m_ps_Status.Goaway(soc2_2); // 플레이어 평판 변경
                    }

                    // 진행중인 퀘스트 현황 업데이트
                    m_pq_Quest.QuestUpdate_Goaway(co2_2[i].gameObject.GetComponent<Monster_Total>().m_ms_Status.m_eMonster_Kind, co2_2[i].gameObject.GetComponent<Monster_Total>().m_ms_Status.m_nMonsterCode);
                    m_pq_Quest.QuestUpdate_Eliminate(co2_2[i].gameObject.GetComponent<Monster_Total>().m_ms_Status.m_eMonster_Kind, co2_2[i].gameObject.GetComponent<Monster_Total>().m_ms_Status.m_nMonsterCode);

                    if (co2_2[i].gameObject.GetComponent<Monster_Total>().m_ms_Status.m_eMonster_Kind != E_MONSTER_KIND.OBJECT) // 해당 오브젝트(몬스터)의 종족 분류가 오브젝트가 아닐때
                        MonsterManager.Instance.Update_Monster_Dictionary(co2_2[i].gameObject.GetComponent<Monster_Total>().m_ms_Status.m_eMonster_Kind, co2_2[i].gameObject.GetComponent<Monster_Total>().m_ms_Status.m_nMonsterCode); // 몬스터 도감 업데이트
                }
            }
        }

        GUIManager_Total.Instance.Update_SS(); // 스탯GUI 업데이트
    }

    // 구르기 키입력(S)
    public void InputKey_Roll()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Roll();
        }
    }
    // 플레이어 구르기 함수
    public void Roll()
    {
        if (Player_Status.m_cCondition.ConditionCheck_Shock() == false) // 플레이어에게 상태이상(기절)이 적용중이지 않을때
        {
            if (m_pm_Move.Roll() == true) // 플레이어 구르기 함수 실행. 구르기 성공 여부 반환
                m_pq_Quest.QuestUpdate_Roll(); // 진행중인 퀘스트 현황 업데이트
        }
    }

    // 플레이어 사망 판단 함수
    public void Death(string deathname) // deathname : 사망 정보
    {
        // 사망 여부 판단
        if (m_ps_Status.m_sStatus.GetSTATUS_HP_Current() <= 0) // 플레이어의 현재 체력이 0 이하일때
        {
            m_pm_Move.Death(); // 플레이어 사망 함수

            GUIManager_Total.Instance.Display_GUI_ReTry(deathname); // 리트라이(부활)GUI 활성화

            if (BossManager.Instance.m_eBattle_Boss_State == E_BATTLE_BOSS_STATE.BATTLE) // 보스몬스터와 전투 중이었을때
            {
                BossManager.Instance.End_Battle_Boss_Fail(); // 보스몬스터 전투 종료(토벌 실패)
            }
        }
    }

    // NPC와 상호작용ㆍ채집 키입력(SPACE)
    public void InputKey_Interaction()
    {
        if (GUIManager_Total.Instance.m_GUI_Interaction.m_gPanel_ChatBox.activeSelf == false &&
            GUIManager_Total.Instance.m_GUI_ChangeMap.m_gPanel_ChangeMap.activeSelf == false) // NPC와 상호작용 하지 않을때, 맵 변경이 끝났을때
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                Interaction();
            }
        }
        else // NPC와 상호작용 할때
        {
            // 키입력(↑, ↓, SPACE)으로 NPC와 상호작용
            // ↑, ↓ : 상호작용 선택지 선택(상호작용 종류(대화, 퀘스트, 거래) 선택, 다음/이전 선택, 수락/거절 선택), SPACE : 스크립트 스킵, 선택지 확정
            if (Input.GetKeyUp(KeyCode.Space))
            {
                GUIManager_Total.Instance.Interaction_In_SSD(KeyCode.Space); // NPC와 상호작용 시 선택지 확정
            }
            else if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                GUIManager_Total.Instance.Interaction_In_SSD(KeyCode.UpArrow); // NPC와 상호작용 시 선택지 선택
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                GUIManager_Total.Instance.Interaction_In_SSD(KeyCode.DownArrow); // NPC와 상호작용 시 선택지 선택
            }
        }
    }
    // NPC와 상호작용ㆍ채집 함수
    // Physics2D.OverlapBoxAll(Vector2 point, Vector2 size, float angle, int layerMask) // point : 오버랩 지점, size : 오버랩 크기, angle : 각도, layerMask : 오버랩을 적용할 레이어 // return Collider2D[]
    public void Interaction()
    {
        if (Player_Status.m_cCondition.ConditionCheck_Bind() == false && Player_Status.m_cCondition.ConditionCheck_Shock() == false && // 플레이어에게 상태이상(속박, 기절)이 적용중이지 않을때
            GUIManager_Total.Instance.m_nList_UI_Priority.Contains(4) == false && GUIManager_Total.Instance.m_nList_UI_Priority.Contains(29) == false) // 퀘스트GUI(4), 몬스터 도감GUI(29)가 비활성화 되었을때
        {
            // 상호작용ㆍ채집 방향 설정, 범위내의 모든 오브젝트(NPC, 채집물) 배열 반환(Collider2D[])
            if (m_nPosValue == 1) // → 방향 상호작용ㆍ채집
            {
                co2_3 = Physics2D.OverlapBoxAll(transform.position + new Vector3(0.125f, 0.15f, 0), m_vSize, 0, nLayer3);
            }
            else // ← 방향 상호작용ㆍ채집
            {
                co2_3 = Physics2D.OverlapBoxAll(transform.position + new Vector3(-0.125f, 0.15f, 0), m_vSize, 0, nLayer3);
            }

            // 범위내의 모든 오브젝트(NPC, 채집물)중 가장 가까운 하나의 오브젝트(NPC, 채집물)와 상호작용(NPC와 상호작용(대화, 퀘스트, 거래), 채집물 채집) 한다.
            // NPC와 채집물이 동일한 거리(동일한 우선순위)일때 NPC와 상호작용이 채집보다 우선 적용된다.
            if (co2_3.Length > 0) // 범위내의 오브젝트가 하나이상 있을 경우
            {
                for (int i = 0; i < co2_3.Length; i++)
                {
                    // NPC와 상호작용
                    if (co2_3[i].gameObject.tag == "NPC") // 해당 오브젝트의 가 "NPC"인 경우
                    {
                        if (m_pm_Move.Conversation() == true) // NPC와 상호작용 가능 여부 반환
                        {
                            m_pc_Camera.SetCamera_ZOOMIN(co2_3[i].gameObject.transform.position); // 카메라 모드 변경(줌인)
                            GUIManager_Total.Instance.Interaction(co2_3[i].gameObject.GetComponent<NPC_Total>()); // NPC와 상호작용 실행(상호작용GUI 활성화)
                        }
                        break;
                    }
                    // 채집
                    if (co2_3[i].gameObject.tag == "Collection") // 해당 오브젝트의 태그가 "Collection"인 경우
                    {
                        co2_3[i].gameObject.GetComponent<Collection>().DropItem(this.transform.position); // 채집 실행
                        break;
                    }
                }
            }
        }
    }
    // NPC와 상호작용 종료 함수
    public void ExitConversation()
    {
        m_pm_Move.m_ePlayerMoveState = m_pm_Move.SetPlayerMoveState(Player_Move.E_PLAYER_MOVE_STATE.IDLE); // 플레이어 동작 FSM 변경
    }

    // 아이템 줍기(키입력 필요없음)
    public void InputKey_GetItem()
    {
        GetItem();
    }
    // 아이템 줍기 함수
    // Physics2D.OverlapBoxAll(Vector2 point, Vector2 size, float angle, int layerMask) // point : 오버랩 지점, size : 오버랩 크기, angle : 각도, layerMask : 오버랩을 적용할 레이어 // return Collider2D[]
    public void GetItem()
    {
        if (Player_Status.m_cCondition.ConditionCheck_Bind() == false && Player_Status.m_cCondition.ConditionCheck_Shock() == false) // 플레이어에게 상태이상(속박, 기절)이 적용중이지 않을때
        {
            // 상호작용ㆍ채집 방향 설정, 범위내의 모든 오브젝트(NPC, 채집물) 배열 반환(Collider2D[])
            co2_4 = Physics2D.OverlapBoxAll(this.transform.position + co2_4_Offset, co2_4_BoxSize, 0, nLayer4);

            for (int i = 0; i < co2_4.Length; i++)
            {
                if (co2_4[i].gameObject.GetComponent<Item>().m_bPossible_Get == true) // 아이템이 획득 가능한 상태일때
                {
                    // 골드(재화) 획득
                    if (co2_4[i].gameObject.GetComponent<Item>().m_nItemCode == 0) // 아이템 코드 : 0
                    {
                        m_pi_Itemslot.Get_Gold(co2_4[i].gameObject.GetComponent<Item_Gold>().GetGold(co2_4[i].gameObject.GetComponent<Item_Gold>())); // 골드(재화) 획득 함수

                        GUIManager_Total.Instance.UpdateLog("[재화]" + co2_4[i].gameObject.GetComponent<Item>().m_sItemName + " 골드를 획득 하였습니다."); // 로그GUI에 획득한 골드(재화) 정보 출력
                    }
                    // 기타아이템 획득
                    else if (co2_4[i].gameObject.GetComponent<Item>().m_nItemCode < 1000) // 아이템 코드 : 1 <= itemcode < 1000
                    {
                        m_pi_Itemslot.Get_Item_Etc(co2_4[i].gameObject.GetComponent<Item_Etc>().DeleteItem(co2_4[i].gameObject.GetComponent<Item_Etc>())); // 기타아이텝 획득 함수

                        GUIManager_Total.Instance.UpdateLog("[기타 아이템]" + co2_4[i].gameObject.GetComponent<Item>().m_sItemName + " 을(를) 획득 하였습니다."); // 로그GUI에 획득한 기타아이템 정보 출력
                    }
                    // 장비아이템 획득
                    else if (co2_4[i].gameObject.GetComponent<Item>().m_nItemCode < 7000) // 아이템 코드 : 1000 <= itemcode < 7000
                    {
                        if (m_pi_Itemslot.Get_Item_Equip(co2_4[i].gameObject.GetComponent<Item_Equip>().DeleteItem(co2_4[i].gameObject.GetComponent<Item_Equip>())) != -1) // 장비아이텝 획득 함수. 할당된 인벤토리 배열 번호 반환
                        {
                            GUIManager_Total.Instance.UpdateLog("[장비 아이템]" + co2_4[i].gameObject.GetComponent<Item>().m_sItemName + " 을(를) 획득 하였습니다."); // 로그GUI에 획득한 장비아이템 정보 출력
                        }
                        else
                        {
                            GUIManager_Total.Instance.UpdateLog("[장비 아이템]" + co2_4[i].gameObject.GetComponent<Item>().m_sItemName + " 을(를) 획득 할 수 없습니다."); // 로그GUI에 장비아이템 획득 실패 출력
                            Item_Equip item = new Item_Equip(co2_4[i].gameObject.GetComponent<Item_Equip>(), co2_4[i].gameObject.transform.position);
                        }
                    }
                    // 소비아이템 획득
                    else // 아이템 코드 : 7000 <= itemcode
                    {
                        m_pi_Itemslot.Get_Item_Use(co2_4[i].gameObject.GetComponent<Item_Use>().DeleteItem(co2_4[i].gameObject.GetComponent<Item_Use>())); // 소비아이템 획득 함수

                        GUIManager_Total.Instance.UpdateLog("[소비 아이템]" + co2_4[i].gameObject.GetComponent<Item>().m_sItemName + " 을(를) 획득 하였습니다."); // 로그GUI에 획득한 소비아이템 정보 출력
                    }

                    GUIManager_Total.Instance.Update_Itemslot(); // 인벤토리GUI 업데이트
                    m_pq_Quest.QuestUpdate_Collect(co2_4[i].gameObject.GetComponent<Item>()); // 진행중인 퀘스트 현황 업데이트

                    break;
                }
            }
        }
    }

    // 퀘스트창GUI 키입력(Q)
    public void InputKey_GUI_Quest()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            GUIManager_Total.Instance.Display_GUI_Quest(); // 퀘스트창GUI 활성화
        }
    }

    // 인벤토리GUI 키입력(I)
    public void InputKey_GUI_Itemslot()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            GUIManager_Total.Instance.Display_GUI_Itemslot(); // 인벤토리GUI 활성화
        }
    }

    // 상태창(능력치창 + 장비창 + 아이템 세트효과창)GUI 키입력(E)
    public void InputKey_GUI_ES()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            GUIManager_Total.Instance.Display_GUI_ES(); // 상태창(능력치창 + 장비창 + 아이템 세트효과창)GUI 활성화
        }
    }

    // 몬스터 도감GUI 키입력(M)
    public void InputKey_GUI_MonsterDictionary()
    {
        if (Input.GetKeyUp(KeyCode.M))
        {
            GUIManager_Total.Instance.Display_GUI_Dictionary(); // 몬드토 도감GUI 활성화
        }
    }    

    // GUI 닫기(비활성화), 일시중지GUI(옵션GUI) 키입력(ESC)
    // 활성화된 GUI를 우선순위에 따라 하나씩 비활성화 한다. 활성화된 GUI가 존재하지 않을 경우 일시중지GUI(옵션GUI)를 활성화 한다.
    public void InputKey_GUI_ESC()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (GUIManager_Total.Instance.Exit_GUI_Priority() == false) // GUI 닫기(비활성화). 활성화된 GUI 정보 반환
                GUIManager_Total.Instance.Display_GUI_Option(); // 일시중지GUI(옵션GUI) 활성화
        }
    }

    // 퀘스트를 추가하는 함수(퀘스트 최초 수락 시 사용). 함수 오버로딩을 이용
    // 퀘스트 타입 : 특정 몬스터 토벌
    public void AddQuest(Quest_KILL_MONSTER quest) // quest : 추가할 퀘스트 정보
    {
        m_pq_Quest.AddQuest(quest); // 퀘스트 추가(수락)
    }
    // 퀘스트 타입 : 특정 타입의 몬스터 토벌
    public void AddQuest(Quest_KILL_TYPE quest) // quest : 추가할 퀘스트 정보
    {
        m_pq_Quest.AddQuest(quest); // 퀘스트 추가(수락)
    }
    // 퀘스트 타입 : 특정 몬스터 놓아주기
    public void AddQuest(Quest_GOAWAY_MONSTER quest) // quest : 추가할 퀘스트 정보
    {
        m_pq_Quest.AddQuest(quest); // 퀘스트 추가(수락)
    }
    // 퀘스트 타입 : 특정 타입의 몬스터 놓아주기
    public void AddQuest(Quest_GOAWAY_TYPE quest) // quest : 추가할 퀘스트 정보
    {
        m_pq_Quest.AddQuest(quest); // 퀘스트 추가(수락)
    }
    // 퀘스트 타입 : 수집
    public void AddQuest(Quest_COLLECT quest) // quest : 추가할 퀘스트 정보
    {
        m_pq_Quest.AddQuest(quest); // 퀘스트 추가(수락)
        m_pq_Quest.QuestUpdate_Collect(); // 퀘스트(수집 타입) 현황 업데이트
    }
    // 퀘스트 타입 : 대화
    public void AddQuest(Quest_CONVERSATION quest) // quest : 추가할 퀘스트 정보
    {
        m_pq_Quest.AddQuest(quest); // 퀘스트 추가(수락)
    }
    // 퀘스트 타입 : 구르기
    public void AddQuest(Quest_ROLL quest) // quest : 추가할 퀘스트 정보
    {
        m_pq_Quest.AddQuest(quest); // 퀘스트 추가(수락)
    }
    // 퀘스트 타입 : 특정 몬스터 제거
    public void AddQuest(Quest_ELIMINATE_MONSTER quest) // quest : 추가할 퀘스트 정보
    {
        m_pq_Quest.AddQuest(quest); // 퀘스트 추가(수락)
    }
    // 퀘스트 타입 : 특정 타입의 몬스터 제거
    public void AddQuest(Quest_ELIMINATE_TYPE quest) // quest : 추가할 퀘스트 정보
    {
        m_pq_Quest.AddQuest(quest); // 퀘스트 추가(수락)
    }

    // 퀘스트를 제거하는 함수(퀘스트 포기)
    //
    // ※ 미구현
    //
    //public void RemoveQuest(Quest quest)
    //{
    //    m_pq_Quest.RemoveQuest(quest);
    //}

    // 진행중인(완료 가능한) 퀘스트를 완료하는 함수. 함수 오버로딩을 이용
    // 퀘스트 타입 : 특정 몬스터 토벌
    public void GetQuestReward(Quest_KILL_MONSTER quest) // quest : 완료할 퀘스트 정보
    {
        m_pq_Quest.GetQuestReward(quest); // 퀘스트 완료
        m_ps_Status.GetQuestReward(quest); // 퀘스트 완료 보상(능력치, 평판) 수령
        m_pi_Itemslot.GetQuestReward_Item_Equip(quest); // 퀘스트 완료 보상(장비아이템) 수령
        m_pi_Itemslot.GetQuestReward_Item_Use(quest); // 퀘스트 완료 보상(소비아이템) 수령
        m_pi_Itemslot.GetQuestReward_Item_Etc(quest); // 퀘스트 완료 보상(기타아이템) 수령
        m_pq_Quest.QuestUpdate_Collect_NoDisplay(); // 퀘스트(수집 타입) 현황 업데이트
        m_pi_Itemslot.m_nGold += quest.m_nRewardGold; // 퀘스트 완료 보상(골드(재화)) 수령

        GUIManager_Total.Instance.Update_SS(); // 스탯GUI 업데이트
        GUIManager_Total.Instance.Update_Itemslot(); // 인벤토리GUI 업데이트
    }
    // 퀘스트 타입 : 특정 타입의 몬스터 토벌
    public void GetQuestReward(Quest_KILL_TYPE quest) // quest : 완료할 퀘스트 정보
    {
        m_pq_Quest.GetQuestReward(quest); // 퀘스트 완료
        m_ps_Status.GetQuestReward(quest); // 퀘스트 완료 보상(능력치, 평판) 수령
        m_pi_Itemslot.GetQuestReward_Item_Equip(quest); // 퀘스트 완료 보상(장비아이템) 수령
        m_pi_Itemslot.GetQuestReward_Item_Use(quest); // 퀘스트 완료 보상(소비아이템) 수령
        m_pi_Itemslot.GetQuestReward_Item_Etc(quest); // 퀘스트 완료 보상(기타아이템) 수령
        m_pq_Quest.QuestUpdate_Collect_NoDisplay(); // 퀘스트(수집 타입) 현황 업데이트
        m_pi_Itemslot.m_nGold += quest.m_nRewardGold; // 퀘스트 완료 보상(골드(재화)) 수령

        GUIManager_Total.Instance.Update_SS(); // 스탯GUI 업데이트
        GUIManager_Total.Instance.Update_Itemslot(); // 인벤토리GUI 업데이트
    }
    // 퀘스트 타입 : 특정 몬스터 놓아주기
    public void GetQuestReward(Quest_GOAWAY_MONSTER quest) // quest : 완료할 퀘스트 정보
    {
        m_pq_Quest.GetQuestReward(quest); // 퀘스트 완료
        m_ps_Status.GetQuestReward(quest); // 퀘스트 완료 보상(능력치, 평판) 수령
        m_pi_Itemslot.GetQuestReward_Item_Equip(quest); // 퀘스트 완료 보상(장비아이템) 수령
        m_pi_Itemslot.GetQuestReward_Item_Use(quest); // 퀘스트 완료 보상(소비아이템) 수령
        m_pi_Itemslot.GetQuestReward_Item_Etc(quest); // 퀘스트 완료 보상(기타아이템) 수령
        m_pq_Quest.QuestUpdate_Collect_NoDisplay(); // 퀘스트(수집 타입) 현황 업데이트
        m_pi_Itemslot.m_nGold += quest.m_nRewardGold; // 퀘스트 완료 보상(골드(재화)) 수령

        GUIManager_Total.Instance.Update_SS(); // 스탯GUI 업데이트
        GUIManager_Total.Instance.Update_Itemslot(); // 인벤토리GUI 업데이트
    }
    // 퀘스트 타입 : 특정 타입의 몬스터 놓아주기
    public void GetQuestReward(Quest_GOAWAY_TYPE quest) // quest : 완료할 퀘스트 정보
    {
        m_pq_Quest.GetQuestReward(quest); // 퀘스트 완료
        m_ps_Status.GetQuestReward(quest); // 퀘스트 완료 보상(능력치, 평판) 수령
        m_pi_Itemslot.GetQuestReward_Item_Equip(quest); // 퀘스트 완료 보상(장비아이템) 수령
        m_pi_Itemslot.GetQuestReward_Item_Use(quest); // 퀘스트 완료 보상(소비아이템) 수령
        m_pi_Itemslot.GetQuestReward_Item_Etc(quest); // 퀘스트 완료 보상(기타아이템) 수령
        m_pq_Quest.QuestUpdate_Collect_NoDisplay(); // 퀘스트(수집 타입) 현황 업데이트
        m_pi_Itemslot.m_nGold += quest.m_nRewardGold; // 퀘스트 완료 보상(골드(재화)) 수령

        GUIManager_Total.Instance.Update_SS(); // 스탯GUI 업데이트
        GUIManager_Total.Instance.Update_Itemslot(); // 인벤토리GUI 업데이트
    }
    // 퀘스트 타입 : 수집
    // 퀘스트 타입이 COLLECT 일 때 대상 아이템을 제거하기위해.
    public void GetQuestReward(Quest_COLLECT quest) // quest : 완료할 퀘스트 정보
    {
        m_pq_Quest.GetQuestReward(quest); // 퀘스트 완료
        m_ps_Status.GetQuestReward(quest); // 퀘스트 완료 보상(능력치, 평판) 수령
        m_pi_Itemslot.GetQuestReward_Item_Equip(quest); // 퀘스트 완료 보상(장비아이템) 수령
        m_pi_Itemslot.GetQuestReward_Item_Use(quest); // 퀘스트 완료 보상(소비아이템) 수령
        m_pi_Itemslot.GetQuestReward_Item_Etc(quest); // 퀘스트 완료 보상(기타아이템) 수령
        m_pi_Itemslot.DeleteCollectItem(quest);
        m_pq_Quest.QuestUpdate_Collect_NoDisplay(); // 퀘스트(수집 타입) 현황 업데이트
        m_pi_Itemslot.m_nGold += quest.m_nRewardGold; // 퀘스트 완료 보상(골드(재화)) 수령

        GUIManager_Total.Instance.Update_SS(); // 스탯GUI 업데이트
        GUIManager_Total.Instance.Update_Itemslot(); // 인벤토리GUI 업데이트
    }
    // 퀘스트 타입 : 대화
    public void GetQuestReward(Quest_CONVERSATION quest) // quest : 완료할 퀘스트 정보
    {
        m_pq_Quest.GetQuestReward(quest); // 퀘스트 완료
        m_ps_Status.GetQuestReward(quest); // 퀘스트 완료 보상(능력치, 평판) 수령
        m_pi_Itemslot.GetQuestReward_Item_Equip(quest); // 퀘스트 완료 보상(장비아이템) 수령
        m_pi_Itemslot.GetQuestReward_Item_Use(quest); // 퀘스트 완료 보상(소비아이템) 수령
        m_pi_Itemslot.GetQuestReward_Item_Etc(quest); // 퀘스트 완료 보상(기타아이템) 수령
        m_pq_Quest.QuestUpdate_Collect_NoDisplay(); // 퀘스트(수집 타입) 현황 업데이트
        m_pi_Itemslot.m_nGold += quest.m_nRewardGold; // 퀘스트 완료 보상(골드(재화)) 수령

        GUIManager_Total.Instance.Update_SS(); // 스탯GUI 업데이트
        GUIManager_Total.Instance.Update_Itemslot(); // 인벤토리GUI 업데이트
    }
    // 퀘스트 타입 : 구르기
    public void GetQuestReward(Quest_ROLL quest) // quest : 완료할 퀘스트 정보
    {
        m_pq_Quest.GetQuestReward(quest); // 퀘스트 완료
        m_ps_Status.GetQuestReward(quest); // 퀘스트 완료 보상(능력치, 평판) 수령
        m_pi_Itemslot.GetQuestReward_Item_Equip(quest); // 퀘스트 완료 보상(장비아이템) 수령
        m_pi_Itemslot.GetQuestReward_Item_Use(quest); // 퀘스트 완료 보상(소비아이템) 수령
        m_pi_Itemslot.GetQuestReward_Item_Etc(quest); // 퀘스트 완료 보상(기타아이템) 수령
        m_pq_Quest.QuestUpdate_Collect_NoDisplay(); // 퀘스트(수집 타입) 현황 업데이트
        m_pi_Itemslot.m_nGold += quest.m_nRewardGold; // 퀘스트 완료 보상(골드(재화)) 수령

        GUIManager_Total.Instance.Update_SS(); // 스탯GUI 업데이트
        GUIManager_Total.Instance.Update_Itemslot(); // 인벤토리GUI 업데이트
    }
    // 퀘스트 타입 : 특정 몬스터 제거
    public void GetQuestReward(Quest_ELIMINATE_MONSTER quest) // quest : 완료할 퀘스트 정보
    {
        m_pq_Quest.GetQuestReward(quest); // 퀘스트 완료
        m_ps_Status.GetQuestReward(quest); // 퀘스트 완료 보상(능력치, 평판) 수령
        m_pi_Itemslot.GetQuestReward_Item_Equip(quest); // 퀘스트 완료 보상(장비아이템) 수령
        m_pi_Itemslot.GetQuestReward_Item_Use(quest); // 퀘스트 완료 보상(소비아이템) 수령
        m_pi_Itemslot.GetQuestReward_Item_Etc(quest); // 퀘스트 완료 보상(기타아이템) 수령
        m_pq_Quest.QuestUpdate_Collect_NoDisplay(); // 퀘스트(수집 타입) 현황 업데이트
        m_pi_Itemslot.m_nGold += quest.m_nRewardGold; // 퀘스트 완료 보상(골드(재화)) 수령

        GUIManager_Total.Instance.Update_SS(); // 스탯GUI 업데이트
        GUIManager_Total.Instance.Update_Itemslot(); // 인벤토리GUI 업데이트
    }
    // 퀘스트 타입 : 특정 타입의 몬스터 제거
    public void GetQuestReward(Quest_ELIMINATE_TYPE quest) // quest : 완료할 퀘스트 정보
    {
        m_pq_Quest.GetQuestReward(quest); // 퀘스트 완료
        m_ps_Status.GetQuestReward(quest); // 퀘스트 완료 보상(능력치, 평판) 수령
        m_pi_Itemslot.GetQuestReward_Item_Equip(quest); // 퀘스트 완료 보상(장비아이템) 수령 
        m_pi_Itemslot.GetQuestReward_Item_Use(quest); // 퀘스트 완료 보상(소비아이템) 수령
        m_pi_Itemslot.GetQuestReward_Item_Etc(quest); // 퀘스트 완료 보상(기타아이템) 수령
        m_pq_Quest.QuestUpdate_Collect_NoDisplay(); // 퀘스트(수집 타입) 현황 업데이트
        m_pi_Itemslot.m_nGold += quest.m_nRewardGold; // 퀘스트 완료 보상(골드(재화)) 수령

        GUIManager_Total.Instance.Update_SS(); // 스탯GUI 업데이트
        GUIManager_Total.Instance.Update_Itemslot(); // 인벤토리GUI 업데이트
    }

    // 플레이어 장비아이템 착용 관련 함수(장비아이템 착용 조건 판단 + 장비아이템 착용)
    // return true : 장비아이템 착용 성공 / return false : 장비아이템 착용 실패(플레이어 사망 시, 장비아이템 착용 조건 불만족)
    public bool CheckCondition_Item_Equip(Item_Equip item, STATUS playerstatus, SOC playersoc) // item : 착용할 장비아이템, playerstatus : 플레이어 능력치, playersoc : 플레이어 평판
    {
        if (m_pm_Move.m_ePlayerMoveState != Player_Move.E_PLAYER_MOVE_STATE.DEATH) // 플레이어 동작 FSM이 사망상태가 아닌 경우(플레이어 사망 상태가 아닐때)
        {
            if (m_ps_Status.CheckCondition_Item_Equip(item, playerstatus, playersoc) == true) // 장비아이템 착용 조건 판단, 장비아이템 착용 시 스탯(능력치, 평판) 업데이트
            {
                m_pe_Equipment.Equip(item); // 장비아이템 착용
                m_pm_Move.Equip(); // 플레이어 장비아이템 변경 시 연계 공격 초기화 함수

                // 플레이어 착용 무기(Sword(검), Axe(도끼), Knife(단검))별 애니메이션 FSM 변경 함수
                if (item.m_eItemEquipType == E_ITEM_EQUIP_TYPE.MAINWEAPON)
                {
                    switch (m_pe_Equipment.Get_MainWeaponType())
                    {
                        case E_ITEM_EQUIP_MAINWEAPON_TYPE.SWORD:
                            {
                                m_pm_Move.SetAnimation_Weapon(E_ITEM_EQUIP_MAINWEAPON_TYPE.SWORD);
                            }
                            break;
                        case E_ITEM_EQUIP_MAINWEAPON_TYPE.AXE:
                            {
                                m_pm_Move.SetAnimation_Weapon(E_ITEM_EQUIP_MAINWEAPON_TYPE.AXE);
                            }
                            break;
                        case E_ITEM_EQUIP_MAINWEAPON_TYPE.KNIFE:
                            {
                                m_pm_Move.SetAnimation_Weapon(E_ITEM_EQUIP_MAINWEAPON_TYPE.KNIFE);
                            }
                            break;
                        default:
                            {
                                m_pm_Move.SetAnimation_Weapon(E_ITEM_EQUIP_MAINWEAPON_TYPE.SWORD);
                            }
                            break;
                    }
                }

                GUIManager_Total.Instance.Update_Itemslot(); // 인벤토리GUI 업데이트
                GUIManager_Total.Instance.Update_Equipslot(); // 상태창(장비창)GUI 업데이트
                CheckSetItemEffect(); // 아이템 세트효과 판단(적용)
                m_pm_Move.SetAttackSpeed(m_ps_Status.Return_AttackSpeed()); // 플레이어 공격 속도 설정(기본 공격(연계 공격)을 위해 Player_Status.cs의 공격 속도를 Player_Move.cs로 넘겨준다.)
                GUIManager_Total.Instance.Update_SS(); // 스탯 GUI 업데이트

                if (GUIManager_Total.Instance.m_GUI_Status.m_gPanel_DetailStatus.activeSelf == true) // 상태창(능력치창)GUI가 활성화된 상태일 경우(능력치창, 장비창, 아이템 세트효과창은 상태창으로 통합)
                {
                    GUIManager_Total.Instance.m_GUI_Status.UpdateStatus_SetItemEffect(CheckSetItemEffect_UI()); // 상태창(아이템 세트효과창)GUI 업데이트
                }

                return true;
            }
        }

        return false;
    }
    // 착용중인 장비아이템의 착용 조건 판단(장비아이템 변경 시, 버프ㆍ디버프 스킬 적용ㆍ해제 시, 버프포션 적용ㆍ해제 시 실행)
    // return true : 현재 착용중인 장비아이템 착용 조건 충족 / return false : 현재 착용중인 장비아이템 착용 조건 불만족
    //
    // ※ 코드가 꼬여서 불필요하게 함수가 호출되는 경우가 있다. 추후 최적화 예정(문서화 예정)
    //
    // 착용중인 장비아이템(모자) 착용 조건 판단
    public bool CheckCondition_Item_Equip_Hat()
    {
        if (m_ps_Status.CheckCondition_Item_Equip(Player_Equipment.m_gEquipment_Hat, m_ps_Status.m_sStatus, m_ps_Status.m_sSoc) == true) // 장비아이템(모자) 착용 조건 판단, 조건 충족 시 스탯(능력치, 평판) 업데이트
                                                                                                                                         // 장비아이템(모자) 착용 조건을 만족 했을 경우
        {
            GUIManager_Total.Instance.Update_SS(); // 스탯 GUI 업데이트

            return true;
        }
        else // 장비아이템(모자) 착용 조건을 불만족 했을 경우
        {
            // 착용중인 장비아이템(모자) 스탯(능력치, 평판) 초기화
            m_ps_Status.m_sStatus_Extra_Equip_Hat = m_ps_Status.m_sStatus_Null;
            m_ps_Status.m_sSoc_Extra_Equip_Hat = m_ps_Status.m_sSoc_Null;
            m_ps_Status.UpdateStatus_Equip(); // 장비아이템 해제로인한 능력치 업데이트
            m_ps_Status.UpdateSOC(); // 장비아이템 해제로인한 평판 업데이트 
            GUIManager_Total.Instance.Update_SS(); // 스탯 GUI 업데이트

            return false;
        }
    }
    // 착용중인 장비아이템(상의) 착용 조건 판단
    public bool CheckCondition_Item_Equip_Top()
    {
        if (m_ps_Status.CheckCondition_Item_Equip(Player_Equipment.m_gEquipment_Top, m_ps_Status.m_sStatus, m_ps_Status.m_sSoc) == true) // 장비아이템(상의) 착용 조건 판단, 조건 충족 시 스탯(능력치, 평판) 업데이트
                                                                                                                                         // 장비아이템 착용 조건을 만족 했을 경우
        {
            GUIManager_Total.Instance.Update_SS(); // 스탯 GUI 업데이트

            return true;
        }
        else // 장비아이템(상의) 착용 조건을 불만족 했을 경우
        {
            // 착용중인 장비아이템(상의) 스탯(능력치, 평판) 초기화
            m_ps_Status.m_sStatus_Extra_Equip_Hat = m_ps_Status.m_sStatus_Null;
            m_ps_Status.m_sSoc_Extra_Equip_Hat = m_ps_Status.m_sSoc_Null;
            m_ps_Status.UpdateStatus_Equip(); // 장비아이템 해제로인한 능력치 업데이트
            m_ps_Status.UpdateSOC(); // 장비아이템 해제로인한 평판 업데이트 
            GUIManager_Total.Instance.Update_SS(); // 스탯 GUI 업데이트

            return false;
        }
    }
    // 착용중인 장비아이템(하의) 착용 조건 판단
    public bool CheckCondition_Item_Equip_Bottoms()
    {
        if (m_ps_Status.CheckCondition_Item_Equip(Player_Equipment.m_gEquipment_Bottoms, m_ps_Status.m_sStatus, m_ps_Status.m_sSoc) == true) // 장비아이템(하의) 착용 조건 판단, 조건 충족 시 스탯(능력치, 평판) 업데이트
                                                                                                                                             // 장비아이템(하의) 착용 조건을 만족 했을 경우
        {
            GUIManager_Total.Instance.Update_SS(); // 스탯 GUI 업데이트

            return true;
        }
        else // 장비아이템(하의) 착용 조건을 불만족 했을 경우
        {
            // 착용중인 장비아이템(하의) 스탯(능력치, 평판) 초기화
            m_ps_Status.m_sStatus_Extra_Equip_Bottoms = m_ps_Status.m_sStatus_Null;
            m_ps_Status.m_sSoc_Extra_Equip_Bottoms = m_ps_Status.m_sSoc_Null;
            m_ps_Status.UpdateStatus_Equip(); // 장비아이템 해제로인한 능력치 업데이트
            m_ps_Status.UpdateSOC(); // 장비아이템 해제로인한 평판 업데이트 
            GUIManager_Total.Instance.Update_SS(); // 스탯 GUI 업데이트

            return false;
        }
    }
    // 착용중인 장비아이템(신발) 착용 조건 판단
    public bool CheckCondition_Item_Equip_Shose()
    {
        if (m_ps_Status.CheckCondition_Item_Equip(Player_Equipment.m_gEquipment_Shose, m_ps_Status.m_sStatus, m_ps_Status.m_sSoc) == true) // 장비아이템(신발) 착용 조건 판단, 조건 충족 시 스탯(능력치, 평판) 업데이트
                                                                                                                                           // 장비아이템(신발) 착용 조건을 만족 했을 경우
        {
            GUIManager_Total.Instance.Update_SS(); // 스탯 GUI 업데이트

            return true;
        }
        else // 장비아이템(신발) 착용 조건을 불만족 했을 경우
        {
            // 착용중인 장비아이템(신발) 스탯(능력치, 평판) 초기화
            m_ps_Status.m_sStatus_Extra_Equip_Shose = m_ps_Status.m_sStatus_Null;
            m_ps_Status.m_sSoc_Extra_Equip_Shose = m_ps_Status.m_sSoc_Null;
            m_ps_Status.UpdateStatus_Equip(); // 장비아이템 해제로인한 능력치 업데이트
            m_ps_Status.UpdateSOC(); // 장비아이템 해제로인한 평판 업데이트 
            GUIManager_Total.Instance.Update_SS(); // 스탯 GUI 업데이트

            return false;
        }
    }
    // 착용중인 장비아이템(장갑) 착용 조건 판단
    public bool CheckCondition_Item_Equip_Gloves()
    {
        if (m_ps_Status.CheckCondition_Item_Equip(Player_Equipment.m_gEquipment_Gloves, m_ps_Status.m_sStatus, m_ps_Status.m_sSoc) == true) // 장비아이템(장갑) 착용 조건 판단, 조건 충족 시 스탯(능력치, 평판) 업데이트
                                                                                                                                            // 장비아이템(장갑) 착용 조건을 만족 했을 경우
        {
            GUIManager_Total.Instance.Update_SS(); // 스탯 GUI 업데이트

            return true;
        }
        else // 장비아이템(장갑) 착용 조건을 불만족 했을 경우
        {
            // 착용중인 장비아이템(장갑) 스탯(능력치, 평판) 초기화
            m_ps_Status.m_sStatus_Extra_Equip_Gloves = m_ps_Status.m_sStatus_Null;
            m_ps_Status.m_sSoc_Extra_Equip_Gloves = m_ps_Status.m_sSoc_Null;
            m_ps_Status.UpdateStatus_Equip(); // 장비아이템 해제로인한 능력치 업데이트
            m_ps_Status.UpdateSOC(); // 장비아이템 해제로인한 평판 업데이트 
            GUIManager_Total.Instance.Update_SS(); // 스탯 GUI 업데이트

            return false;
        }
    }
    // 착용중인 장비아이템(주무기) 착용 조건 판단
    public bool CheckCondition_Item_Equip_MainWeapon()
    {
        if (m_ps_Status.CheckCondition_Item_Equip(Player_Equipment.m_gEquipment_Mainweapon, m_ps_Status.m_sStatus, m_ps_Status.m_sSoc) == true) // 장비아이템(주무기) 착용 조건 판단, 조건 충족 시 스탯(능력치, 평판) 업데이트
                                                                                                                                                // 장비아이템(주무기) 착용 조건을 만족 했을 경우
        {
            GUIManager_Total.Instance.Update_SS(); // 스탯 GUI 업데이트

            return true;
        }
        else // 장비아이템(주무기) 착용 조건을 불만족 했을 경우
        {
            // 착용중인 장비아이템(주무기) 스탯(능력치, 평판) 초기화
            m_ps_Status.m_sStatus_Extra_Equip_Mainweapon = m_ps_Status.m_sStatus_Null;
            m_ps_Status.m_sSoc_Extra_Equip_Mainweapon = m_ps_Status.m_sSoc_Null;
            m_ps_Status.UpdateStatus_Equip(); // 장비아이템 해제로인한 능력치 업데이트
            m_ps_Status.UpdateSOC(); // 장비아이템 해제로인한 평판 업데이트 
            GUIManager_Total.Instance.Update_SS(); // 스탯 GUI 업데이트

            return false;
        }
    }
    // 착용중인 장비아이템(보조무기) 착용 조건 판단
    public bool CheckCondition_Item_Equip_SubWeapon()
    {
        if (m_ps_Status.CheckCondition_Item_Equip(Player_Equipment.m_gEquipment_Subweapon, m_ps_Status.m_sStatus, m_ps_Status.m_sSoc) == true) // 장비아이템(보조무기) 착용 조건 판단, 조건 충족 시 스탯(능력치, 평판) 업데이트
                                                                                                                                               // 장비아이템(보조무기) 착용 조건을 불만족 했을 경우
        {
            GUIManager_Total.Instance.Update_SS(); // 스탯 GUI 업데이트

            return true;
        }
        else // 장비아이템(보조무기) 착용 조건을 불만족 했을 경우
        {
            // 착용중인 장비아이템(보조무기) 스탯(능력치, 평판) 초기화
            m_ps_Status.m_sStatus_Extra_Equip_Subweapon = m_ps_Status.m_sStatus_Null;
            m_ps_Status.m_sSoc_Extra_Equip_Subweapon = m_ps_Status.m_sSoc_Null;
            m_ps_Status.UpdateStatus_Equip(); // 장비아이템 해제로인한 능력치 업데이트
            m_ps_Status.UpdateSOC(); // 장비아이템 해제로인한 평판 업데이트 
            GUIManager_Total.Instance.Update_SS(); // 스탯 GUI 업데이트

            return false;
        }
    }

    // 플레이어에게 적용할 아이템 세트효과 판단. m_Dictionary_SerItemEffect(플레이어에게 적용중인 아이템 세트효과 딕셔너리) 에 저장
    // Dictionary <Key : 아이템 세트효과 코드 , Value : 아이템 세트효과 코드(Key)를 가진 아이템 개수> Key == 0 (아이템 세트효과 코드 == 0) : 세트효과 없음
    // 장비아이템의 순서(모자, 상의, 하의, 신발, 장갑, 주무기, 보조무기)에 따라 플레이어에게 적용할 아이템 세트효과를 판단한다.
   void CheckSetItemEffect_Dictionary()
    {
        m_Dictionary_SerItemEffect.Clear(); // 플레이어에게 적용중인 아이템 세트효과 딕셔너리 초기화

        int nhat = m_pe_Equipment.CheckSetItemEffect(E_ITEM_EQUIP_TYPE.HAT);               // 착용중인 장비아이템(모자)이 가진 아이템 세트효과 코드(세트효과 없을 시 0)
        int ntop = m_pe_Equipment.CheckSetItemEffect(E_ITEM_EQUIP_TYPE.TOP);               // 착용중인 장비아이템(상의)이 가진 아이템 세트효과 코드(세트효과 없을 시 0)
        int nbottoms = m_pe_Equipment.CheckSetItemEffect(E_ITEM_EQUIP_TYPE.BOTTOMS);       // 착용중인 장비아이템(하의)이 가진 아이템 세트효과 코드(세트효과 없을 시 0)
        int nshose = m_pe_Equipment.CheckSetItemEffect(E_ITEM_EQUIP_TYPE.SHOSE);           // 착용중인 장비아이템(신발)이 가진 아이템 세트효과 코드(세트효과 없을 시 0)
        int ngloves = m_pe_Equipment.CheckSetItemEffect(E_ITEM_EQUIP_TYPE.GLOVES);         // 착용중인 장비아이템(장갑)이 가진 아이템 세트효과 코드(세트효과 없을 시 0)
        int nmainwaepon = m_pe_Equipment.CheckSetItemEffect(E_ITEM_EQUIP_TYPE.MAINWEAPON); // 착용중인 장비아이템(주무기)이 가진 아이템 세트효과 코드(세트효과 없을 시 0)
        int nsubweapon = m_pe_Equipment.CheckSetItemEffect(E_ITEM_EQUIP_TYPE.SUBWEAPON);   // 착용중인 장비아이템(보조무기)이 가진 아이템 세트효과 코드(세트효과 없을 시 0)

        // 장비아이템 순서대로 아이템 세트효과 확인
        // 장비아이템(모자)
        if (Player_Equipment.m_bEquipment_Hat == true) // 착용중인 장비아이템(모자)가 존재할 경우
        {
            if (CheckCondition_Item_Equip_Hat() == true) // 착용중인 장비아이템(모자) 착용 조건 판단
                m_Dictionary_SerItemEffect.Add(nhat, 1);
            else
                m_Dictionary_SerItemEffect.Add(0, 1);
        }
        // 장비아이템(상의)
        if (Player_Equipment.m_bEquipment_Top == true) // 착용중인 장비아이템(상의)가 존재할 경우
        {
            if (m_Dictionary_SerItemEffect.ContainsKey(ntop) == true) // 착용중인 장비아이템(상의)과 동일한 아이템 세트효과 코드를 가진 장비아이템(모자)이 존재할 경우
            {
                if (CheckCondition_Item_Equip_Top() == true) // 착용중인 장비아이템(상의) 착용 조건 판단
                    m_Dictionary_SerItemEffect[ntop] += 1; // m_Dictionary_SerItemEffect<Key, Value> Key : 동일한 아이템 세트효과 코드, Value : 장비아이템 개수 += 1
            }
            else // 착용중인 장비아이템(상의)과 동일한 아이템 세트효과 코드를 가진 장비아이템(모자)이 존재하지 않을 경우
            {
                if (CheckCondition_Item_Equip_Top() == true) // 착용중인 장비아이템(상의) 착용 조건 판단
                    m_Dictionary_SerItemEffect.Add(ntop, 1); // m_Dictionary_SerItemEffect<Key, Value> Key : 새로운 아이템 세트효과 코드, Value : 장비아이템 개수 = 1
            }
        }
        // 장비아이템(하의)
        if (Player_Equipment.m_bEquipment_Bottoms == true) // 착용중인 장비아이템(하의)가 존재할 경우
        {
            if (m_Dictionary_SerItemEffect.ContainsKey(nbottoms) == true) // 착용중인 장비아이템(하의)과 동일한 아이템 세트효과 코드를 가진 장비아이템(모자, 상의)이 존재할 경우
            {
                if (CheckCondition_Item_Equip_Bottoms() == true) // 착용중인 장비아이템(하의) 착용 조건 판단
                    m_Dictionary_SerItemEffect[nbottoms] += 1; // m_Dictionary_SerItemEffect<Key, Value> Key : 동일한 아이템 세트효과 코드, Value : 장비아이템 개수 += 1
            }
            else // 착용중인 장비아이템(하의)과 동일한 아이템 세트효과 코드를 가진 장비아이템(모자, 상의)이 존재하지 않을 경우
            {
                if (CheckCondition_Item_Equip_Bottoms() == true) // 착용중인 장비아이템(하의) 착용 조건 판단
                    m_Dictionary_SerItemEffect.Add(nbottoms, 1); // m_Dictionary_SerItemEffect<Key, Value> Key : 새로운 아이템 세트효과 코드, Value : 장비아이템 개수 = 1
            }
        }
        // 장비아이템(신발)
        if (Player_Equipment.m_bEquipment_Shose == true) // 착용중인 장비아이템(신발)가 존재할 경우
        {
            if (m_Dictionary_SerItemEffect.ContainsKey(nshose) == true) // 착용중인 장비아이템(하의)과 동일한 아이템 세트효과 코드를 가진 장비아이템(모자, 상의, 하의)이 존재할 경우
            {
                if (CheckCondition_Item_Equip_Shose() == true) // 착용중인 장비아이템(신발) 착용 조건 판단
                    m_Dictionary_SerItemEffect[nshose] += 1; // m_Dictionary_SerItemEffect<Key, Value> Key : 동일한 아이템 세트효과 코드, Value : 장비아이템 개수 += 1
            }
            else // 착용중인 장비아이템(신발)과 동일한 아이템 세트효과 코드를 가진 장비아이템(모자, 상의, 하의)이 존재하지 않을 경우
            {
                if (CheckCondition_Item_Equip_Shose() == true) // 착용중인 장비아이템(신발) 착용 조건 판단
                    m_Dictionary_SerItemEffect.Add(nshose, 1); // m_Dictionary_SerItemEffect<Key, Value> Key : 새로운 아이템 세트효과 코드, Value : 장비아이템 개수 = 1
            }
        }
        // 장비아이템(장갑)
        if (Player_Equipment.m_bEquipment_Gloves == true) // 착용중인 장비아이템(장갑)가 존재할 경우
        {
            if (m_Dictionary_SerItemEffect.ContainsKey(ngloves) == true) // 착용중인 장비아이템(하의)과 동일한 아이템 세트효과 코드를 가진 장비아이템(모자, 상의, 하의, 신발)이 존재할 경우
            {
                if (CheckCondition_Item_Equip_Gloves() == true) // 착용중인 장비아이템(장갑) 착용 조건 판단
                    m_Dictionary_SerItemEffect[ngloves] += 1; // m_Dictionary_SerItemEffect<Key, Value> Key : 동일한 아이템 세트효과 코드, Value : 장비아이템 개수 += 1
            }
            else // 착용중인 장비아이템(장갑)과 동일한 아이템 세트효과 코드를 가진 장비아이템(모자, 상의, 하의, 신발)이 존재하지 않을 경우
            {
                if (CheckCondition_Item_Equip_Gloves() == true) // 착용중인 장비아이템(장갑) 착용 조건 판단
                    m_Dictionary_SerItemEffect.Add(ngloves, 1); // m_Dictionary_SerItemEffect<Key, Value> Key : 새로운 아이템 세트효과 코드, Value : 장비아이템 개수 = 1
            }
        }
        // 장비아이템(주무기)
        if (Player_Equipment.m_bEquipment_Mainweapon == true) // 착용중인 장비아이템(주무기)가 존재할 경우
        {
            if (m_Dictionary_SerItemEffect.ContainsKey(nmainwaepon) == true) // 착용중인 장비아이템(하의)과 동일한 아이템 세트효과 코드를 가진 장비아이템(모자, 상의, 하의, 신발, 장갑)이 존재할 경우
            {
                if (CheckCondition_Item_Equip_MainWeapon() == true) // 착용중인 장비아이템(주무기) 착용 조건 판단
                    m_Dictionary_SerItemEffect[nmainwaepon] += 1; // m_Dictionary_SerItemEffect<Key, Value> Key : 동일한 아이템 세트효과 코드, Value : 장비아이템 개수 += 1
            }
            else // 착용중인 장비아이템(주무기)과 동일한 아이템 세트효과 코드를 가진 장비아이템(모자, 상의, 하의, 신발, 장갑)이 존재하지 않을 경우
            {
                if (CheckCondition_Item_Equip_MainWeapon() == true) // 착용중인 장비아이템(주무기) 착용 조건 판단
                    m_Dictionary_SerItemEffect.Add(nmainwaepon, 1); // m_Dictionary_SerItemEffect<Key, Value> Key : 새로운 아이템 세트효과 코드, Value : 장비아이템 개수 = 1
            }
        }
        // 장비아이템(보조무기)
        if (Player_Equipment.m_bEquipment_Subweapon == true) // 착용중인 장비아이템(보조무기)가 존재할 경우
        {
            if (m_Dictionary_SerItemEffect.ContainsKey(nsubweapon) == true) // 착용중인 장비아이템(하의)과 동일한 아이템 세트효과 코드를 가진 장비아이템(모자, 상의, 하의, 신발, 장갑, 주무기)이 존재할 경우
            {
                if (CheckCondition_Item_Equip_SubWeapon() == true) // 착용중인 장비아이템(보조무기) 착용 조건 판단
                    m_Dictionary_SerItemEffect[nsubweapon] += 1; // m_Dictionary_SerItemEffect<Key, Value> Key : 동일한 아이템 세트효과 코드, Value : 장비아이템 개수 += 1
            }
            else // 착용중인 장비아이템(보조무기)과 동일한 아이템 세트효과 코드를 가진 장비아이템(모자, 상의, 하의, 신발, 장갑, 주무기)이 존재하지 않을 경우
            {
                if (CheckCondition_Item_Equip_SubWeapon() == true) // 착용중인 장비아이템(보조무기) 착용 조건 판단
                    m_Dictionary_SerItemEffect.Add(nsubweapon, 1); // m_Dictionary_SerItemEffect<Key, Value> Key : 새로운 아이템 세트효과 코드, Value : 장비아이템 개수 = 1
            }
        }
    }
    // 아이템 세트효과 판단 - 능력치 적용 용도
    public void CheckSetItemEffect()
    {
        CheckSetItemEffect_Dictionary(); // 플레이어에게 적용할 아이템 세트효과 판단

        m_ps_Status.CheckSetItemEffect(m_Dictionary_SerItemEffect); // 장비아이템 세트효과 적용
    }

    // 아이템 세트효과 판단 - GUI 출력 용도
    // 플레이어에게 적용중인 아이템 세트효과 딕셔너리 반환
    public Dictionary<int, int> CheckSetItemEffect_UI()
    {
        CheckSetItemEffect_Dictionary(); // 플레이어에게 적용할 아이템 세트효과 판단

        return m_Dictionary_SerItemEffect;
    }

    // 장비아이템 착용 해제 관련 함수. 인벤토리에 빈칸이 있어야 장비 해제 가능
    // return true : 장비아이템 착용 해제 성공 / return false : 장비아이템 착용 해제 실패
    public bool Remove_Item_Equip(Item_Equip item) // item : 착용 해제할 장비아이템
    {
        if (m_pm_Move.m_ePlayerMoveState != Player_Move.E_PLAYER_MOVE_STATE.DEATH) // 플레이어 동작 FSM이 사망상태가 아닌 경우(플레이어 사망 상태가 아닐때)
        {
            if (m_pi_Itemslot.Get_Item_Equip(item) != -1) // 장비아이템 획득 함수(return -1 : 인벤토리에 빈칸이 없어 장비아이템을 획득(해제)하지 못함)
            {
                // 장비아이템 착용 해제 관련 함수 실행
                m_ps_Status.Remove_Item_Equip(item); // 스탯(능력치, 평판) 업데이트
                m_pe_Equipment.Remove_Equip(item.m_eItemEquipType); // 장비아이템 해제
                m_pm_Move.Equip(); // 플레이어 장비아이템 변경 시 연계 공격 초기화 함수
                if (item.m_eItemEquipType == E_ITEM_EQUIP_TYPE.MAINWEAPON) // 해제할 장비아이템의 분류가 주무기일 경우
                    m_pm_Move.SetAnimation_Weapon(E_ITEM_EQUIP_MAINWEAPON_TYPE.SWORD); // 플레이어 착용 무기별 애니메이션 FSM 변경 함수

                GUIManager_Total.Instance.Update_Itemslot(); // 인벤토리GUI 업데이트
                GUIManager_Total.Instance.Update_Equipslot(); // 상태창(장비창)GUI 업데이트
                CheckSetItemEffect(); // 아이템 세트효과 판단(적용)
                m_pm_Move.SetAttackSpeed(m_ps_Status.Return_AttackSpeed()); // 플레이어 공격 속도 설정(기본 공격(연계 공격)을 위해 Player_Status.cs의 공격 속도를 Player_Move.cs로 넘겨준다.)
                GUIManager_Total.Instance.Update_SS(); // 스탯 GUI 업데이트
                m_pq_Quest.QuestUpdate_Collect(item); // 진행중인 퀘스트 현황을 업데이트하는 함수(퀘스트 타입 : 수집)

                if (GUIManager_Total.Instance.m_GUI_Status.m_gPanel_DetailStatus.activeSelf == true) // 상태창(능력치창 + 장비창 + 아이템 세트효과창)GUI가 활성화 되어 있을 경우
                {
                    GUIManager_Total.Instance.m_GUI_Status.UpdateStatus_SetItemEffect(CheckSetItemEffect_UI()); // 아이템 세트효과GUI(상태창GUI에 포함) 업데이트
                }

                m_ps_Status.CheckLogic(); // 능력치 논리(조건) 판단

                return true;
            }
        }

        return false;
    }

    // 플레이어 소비아이템 사용 관련 함수(소비아이템 사용 조건 판단 + 소비아이템 사용)
    // return 0 : 소비아이템 사용 성공 / return 1 : 소비아이템 사용 실패(소비아이템 사용 조건(스탯(능력치, 평판), 쿨타임) 불만족) / return 2 : 소비아이템(기프트) 사용 실패(인벤토리에 빈칸이 없음)
    public int CheckCondition_Item_Use(Item_Use item, int arynumber) // item : 사용할 소비아이템, arynumber : 사용할 소비아이템의 인벤토리 배열 번호
    {
        if (m_pm_Move.m_ePlayerMoveState != Player_Move.E_PLAYER_MOVE_STATE.DEATH) // 플레이어 동작 FSM이 사망상태가 아닌 경우(플레이어 사망 상태가 아닐때)
        {
            if (m_ps_Status.CheckCondition_Item_Use(item) == 0) // 소비아이템 사용 조건 판단
            {
                // 1. 소비아이템의 분류가 회복포션, 일시적 버프포션, 영구적 버프포션 일 경우
                if (item.m_eItemUseType == E_ITEM_USE_TYPE.RECOVERPOTION ||
                    item.m_eItemUseType == E_ITEM_USE_TYPE.TEMPORARYBUFFPOTION ||
                    item.m_eItemUseType == E_ITEM_USE_TYPE.ETERNALBUFFPOTION)
                {
                    int Item_Use_Code = m_ps_Status.ApplyPotion(item); // 소비아이템 사용 시 스탯(능력치, 평판), 버프ㆍ디버프 업데이트
                    GUIManager_Total.Instance.Update_Itemslot(); // 인벤토리GUI 업데이트
                    GUIManager_Total.Instance.Update_Equipslot(); // 상태창(장비창)GUI 업데이트
                    CheckSetItemEffect(); // 아이템 세트효과 판단(적용)
                    m_pm_Move.SetAttackSpeed(m_ps_Status.Return_AttackSpeed()); // 플레이어 공격 속도 설정(기본 공격(연계 공격)을 위해 Player_Status.cs의 공격 속도를 Player_Move.cs로 넘겨준다.)
                    GUIManager_Total.Instance.Update_SS(); // 스탯 GUI 업데이트

                    return Item_Use_Code;
                }
                // 2. 소비아이템의 분류가 강화서 일 경우
                else if (item.m_eItemUseType == E_ITEM_USE_TYPE.REINFORCEMENT)
                {
                    GUIManager_Total.Instance.Display_GUI_Reinforcement(arynumber, item); // 장비아이템 강화GUI 활성화
                }
                // 3. 소비아이템의 분류가 기프트 일 경우
                else if (item.m_eItemUseType == E_ITEM_USE_TYPE.GIFT)
                {
                    // 소비아이템(기프트) 사용 시 획득할 아이템의 임시 저장소(변수)
                    Item_Equip itemequip;
                    Item_Use itemuse;
                    Item_Etc itemetc;
                    // 소비아이템(기프트) 사용 시 획득할 아이템의 임시 저장소(Dictionary)
                    // 획득할 아이템 종류 Dictionary <Key : 아이템 획득 순서, Value ; 아이템 코드>
                    // 획득할 아이템 종류에 해당하는 아이템 개수 Dictionary <Key : 아이템 획득 순서, Value ; 아이템 개수>
                    // 플레이어는 소비아이템(기프트) 사용 시 '아이템 획득 순서'에 따라 해당하는 '아이템 코드'의 아이템을 '아이템 개수'만큼 획득한다.
                    Dictionary<int, int> Dictionary_GetItem_Equip = new Dictionary<int, int>();       // 획득할 장비아이템 종류(아이템 코드)
                    Dictionary<int, int> Dictionary_GetItem_Equip_Count = new Dictionary<int, int>(); // 종류별 획득할 장비아이템 개수
                    Dictionary<int, int> Dictionary_GetItem_Use = new Dictionary<int, int>();         // 획득할 소비아이템 종류(아이템 코드)
                    Dictionary<int, int> Dictionary_GetItem_Use_Count = new Dictionary<int, int>();   // 종류별 획득할 소비아이템 개수
                    Dictionary<int, int> Dictionary_GetItem_Etc = new Dictionary<int, int>();         // 획득할 기타아이템 종류(아이템 코드)
                    Dictionary<int, int> Dictionary_GetItem_Etc_Count = new Dictionary<int, int>();   // 종류별 획득할 기타아이템 개수
                    int tempnumber;

                    // 3_1. 소비아이템(기프트_고정형) 사용 시 아이템 획득(기프트 구성물품 전부를 획득한다.)
                    if (item.m_eItemUseGiftType == E_ITEM_USE_GIFT_TYPE.FIXEDBOX)
                    {
                        if (CheckCondition_Item_Use_Gift(item, arynumber) == true) // 소비아이템(기프트) 사용 조건 판단(획득할 아이템 만큼 인벤토리에 여유가 있는지 판단)
                        {
                            // 장비아이템 획득
                            for (int i = 0; i < item.m_nDictionary_Gift_Item_Equip_Code.Count; i++) // 획득할 장비아이템 종류
                            {
                                for (int j = 0; j < item.m_nDictionary_Gift_Item_Equip_Count[i]; j++) // 종류별 획득할 장비아이템 개수
                                {
                                    itemequip = ItemManager.instance.m_Dictionary_MonsterDrop_Equip[item.m_nDictionary_Gift_Item_Equip_Code[i]].CreateItem(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[item.m_nDictionary_Gift_Item_Equip_Code[i]]); // 획득할 장비아이템 생성, 임시 변수에 저장
                                    Destroy(itemequip); // 생성된 장비아이템 오브젝트 삭제
                                                        // 
                                                        // ※ 아이템은 생성 시 오브젝트 형태로 게임내에 생성 된다.
                                                        //    그러나 소비아이템(기프트) 사용으로 획득할 아이템의 경우 해당 아이템의 데이터만 가지고 있으면 된다.(인벤토리에 바로 생성되기 때문)
                                                        //    굳이 인게임 내에 오브젝트 형태로 존재 해 메모리를 낭비할 필요가 없는것이다.
                                                        //    그래서 유니티 상의 오브젝트는 삭제하되 데이터는 보존한 후 사용한다.
                                                        //
                                    tempnumber = m_pi_Itemslot.Get_Item_Equip(itemequip); // 장비아이템 획득 함수
                                    if (j == 0)
                                    {
                                        Dictionary_GetItem_Equip.Add(Dictionary_GetItem_Equip.Count, tempnumber);
                                        Dictionary_GetItem_Equip_Count.Add(Dictionary_GetItem_Equip_Count.Count, item.m_nDictionary_Gift_Item_Equip_Count[i]);
                                    }
                                }
                            }
                            for (int i = 0; i < item.m_nDictionary_Gift_Item_Use_Code.Count; i++)
                            {
                                for (int j = 0; j < item.m_nDictionary_Gift_Item_Use_Count[i]; j++)
                                {
                                    itemuse = ItemManager.instance.m_Dictionary_MonsterDrop_Use[item.m_nDictionary_Gift_Item_Use_Code[i]].CreateItem(ItemManager.instance.m_Dictionary_MonsterDrop_Use[item.m_nDictionary_Gift_Item_Use_Code[i]]);
                                    Destroy(itemuse);
                                    tempnumber = m_pi_Itemslot.Get_Item_Use(itemuse);
                                    //if (Dictionary_GetItem_Use_Count.ContainsKey(Dictionary_GetItem_Use_Count.Count) == false)
                                    //    Dictionary_GetItem_Use_Count.Add(Dictionary_GetItem_Use_Count.Count, 1);
                                    //else
                                    //    Dictionary_GetItem_Use_Count[Dictionary_GetItem_Use_Count.Count] += 1;
                                    if (j == 0)
                                    {
                                        Dictionary_GetItem_Use.Add(Dictionary_GetItem_Use.Count, tempnumber);
                                        Dictionary_GetItem_Use_Count.Add(Dictionary_GetItem_Use_Count.Count, item.m_nDictionary_Gift_Item_Use_Count[i]);
                                    }
                                }
                            }
                            for (int i = 0; i < item.m_nDictionary_Gift_Item_Etc_Code.Count; i++)
                            {
                                for (int j = 0; j < item.m_nDictionary_Gift_Item_Etc_Count[i]; j++)
                                {
                                    itemetc = ItemManager.instance.m_Dictionary_MonsterDrop_Etc[item.m_nDictionary_Gift_Item_Etc_Code[i]].CreateItem(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[item.m_nDictionary_Gift_Item_Etc_Code[i]]);
                                    Destroy(itemetc);
                                    tempnumber = m_pi_Itemslot.Get_Item_Etc(itemetc);
                                    //if (Dictionary_GetItem_Etc_Count.ContainsKey(Dictionary_GetItem_Etc_Count.Count) == false)
                                    //    Dictionary_GetItem_Etc_Count.Add(Dictionary_GetItem_Etc_Count.Count, 1);
                                    //else
                                    //    Dictionary_GetItem_Etc_Count[Dictionary_GetItem_Etc_Count.Count] += 1;
                                    if (j == 0)
                                    {
                                        Dictionary_GetItem_Etc.Add(Dictionary_GetItem_Etc.Count, tempnumber);
                                        Dictionary_GetItem_Etc_Count.Add(Dictionary_GetItem_Etc_Count.Count, item.m_nDictionary_Gift_Item_Etc_Count[i]);
                                    }
                                }
                            }

                            GUIManager_Total.Instance.UpdateLog("[소비아이템][" + item.m_sItemName + "] 사용.");

                            GUIManager_Total.Instance.Display_GUI_Gift_GetItem_Info(Dictionary_GetItem_Equip, Dictionary_GetItem_Equip_Count,
                                                                                    Dictionary_GetItem_Use, Dictionary_GetItem_Use_Count,
                                                                                    Dictionary_GetItem_Etc, Dictionary_GetItem_Etc_Count);

                            return 0;
                        }
                        else
                        {
                            GUIManager_Total.Instance.UpdateLog("[소비아이템][" + item.m_sItemName + "] 사용 불가.");
                            return 2;
                        }
                    }
                    else if (item.m_eItemUseGiftType == E_ITEM_USE_GIFT_TYPE.RANDOMBOX_INDEPENDENTTRIAL)
                    {
                        if (CheckCondition_Item_Use_Gift(item, arynumber) == true)
                        {
                            int pickcount = Random.Range(item.m_nRandomBox_PickCount_Min, item.m_nRandomBox_PickCount_Max + 1);

                            List<int> ItemList = new List<int>();
                            List<int> ItemCountList = new List<int>();
                            List<int> ItemProbabilityList = new List<int>();

                            int num = 0;

                            for (int i = 0; i < item.m_nDictionary_Gift_Item_Equip_Code.Count; i++)
                            {
                                ItemList.Add(item.m_nDictionary_Gift_Item_Equip_Code[i]);
                                ItemCountList.Add(item.m_nDictionary_Gift_Item_Equip_Count[i]);
                                if (ItemProbabilityList.Count == 0)
                                {
                                    ItemProbabilityList.Add(item.m_nDictionary_Gift_Item_Equip_Probability[i]);
                                }
                                else
                                {
                                    ItemProbabilityList.Add(ItemProbabilityList[num - 1] + item.m_nDictionary_Gift_Item_Equip_Probability[i]);
                                }
                                num += 1;
                            }
                            for (int i = 0; i < item.m_nDictionary_Gift_Item_Use_Code.Count; i++)
                            {
                                ItemList.Add(item.m_nDictionary_Gift_Item_Use_Code[i]);
                                ItemCountList.Add(item.m_nDictionary_Gift_Item_Use_Count[i]);
                                if (ItemProbabilityList.Count == 0)
                                {
                                    ItemProbabilityList.Add(item.m_nDictionary_Gift_Item_Use_Probability[i]);
                                }
                                else
                                {
                                    ItemProbabilityList.Add(ItemProbabilityList[num - 1] + item.m_nDictionary_Gift_Item_Use_Probability[i]);
                                }
                                num += 1;
                            }
                            for (int i = 0; i < item.m_nDictionary_Gift_Item_Etc_Code.Count; i++)
                            {
                                ItemList.Add(item.m_nDictionary_Gift_Item_Etc_Code[i]);
                                ItemCountList.Add(item.m_nDictionary_Gift_Item_Etc_Count[i]);
                                if (ItemProbabilityList.Count == 0)
                                {
                                    ItemProbabilityList.Add(item.m_nDictionary_Gift_Item_Etc_Probability[i]);
                                }
                                else
                                {
                                    ItemProbabilityList.Add(ItemProbabilityList[num - 1] + item.m_nDictionary_Gift_Item_Etc_Probability[i]);
                                }
                                num += 1;
                            }

                            for (int i = 0; i < pickcount; i++)
                            {
                                int randomnum = Random.Range(1, 10001);
                                int arynum = 0;
                                for (int j = 0; j < ItemList.Count; j++)
                                {
                                    if (ItemProbabilityList[j] < randomnum)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        arynum = j;
                                        break;
                                    }
                                }

                                //Debug.Log("아이템 획득: " + ItemList[arynum] + " / " + ItemProbabilityList[arynum]);
                                if (ItemList[arynum] / 1000 >= 1 && ItemList[arynum] / 1000 <= 7)
                                {
                                    for (int j = 0; j < ItemCountList[arynum]; j++)
                                    {
                                        itemequip = ItemManager.instance.m_Dictionary_MonsterDrop_Equip[ItemList[arynum]].CreateItem(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[ItemList[arynum]]);
                                        Destroy(itemequip);
                                        tempnumber = m_pi_Itemslot.Get_Item_Equip(itemequip);
                                        //Dictionary_GetItem_Equip.Add(Dictionary_GetItem_Equip.Count, m_pi_Itemslot.GetItem_Equip(itemequip));
                                        //if (Dictionary_GetItem_Equip_Count.ContainsKey(Dictionary_GetItem_Equip_Count.Count) == false)
                                        //    Dictionary_GetItem_Equip_Count.Add(Dictionary_GetItem_Equip_Count.Count, 1);
                                        //else
                                        //    Dictionary_GetItem_Equip_Count[Dictionary_GetItem_Equip_Count.Count] += 1;
                                        if (j == 0)
                                        {
                                            Dictionary_GetItem_Equip.Add(Dictionary_GetItem_Equip.Count, tempnumber);
                                            Dictionary_GetItem_Equip_Count.Add(Dictionary_GetItem_Equip_Count.Count, item.m_nDictionary_Gift_Item_Equip_Count[i]);
                                        }
                                    }
                                }
                                else if (ItemList[arynum] / 1000 >= 8 && ItemList[arynum] / 1000 <= 12)
                                {
                                    for (int j = 0; j < ItemCountList[arynum]; j++)
                                    {
                                        itemuse = ItemManager.instance.m_Dictionary_MonsterDrop_Use[ItemList[arynum]].CreateItem(ItemManager.instance.m_Dictionary_MonsterDrop_Use[ItemList[arynum]]);
                                        Destroy(itemuse);
                                        tempnumber = m_pi_Itemslot.Get_Item_Use(itemuse);
                                        //Dictionary_GetItem_Use.Add(Dictionary_GetItem_Use.Count, m_pi_Itemslot.GetItem_Use(itemuse));
                                        //if (Dictionary_GetItem_Use_Count.ContainsKey(Dictionary_GetItem_Use_Count.Count) == false)
                                        //    Dictionary_GetItem_Use_Count.Add(Dictionary_GetItem_Use_Count.Count, 1);
                                        //else
                                        //    Dictionary_GetItem_Use_Count[Dictionary_GetItem_Use_Count.Count] += 1;
                                        if (j == 0)
                                        {
                                            Dictionary_GetItem_Use.Add(Dictionary_GetItem_Use.Count, tempnumber);
                                            Dictionary_GetItem_Use_Count.Add(Dictionary_GetItem_Use_Count.Count, item.m_nDictionary_Gift_Item_Use_Count[i]);
                                        }
                                    }
                                }
                                else if (ItemList[arynum] / 1000 == 0)
                                {
                                    for (int j = 0; j < ItemCountList[arynum]; j++)
                                    {
                                        itemetc = ItemManager.instance.m_Dictionary_MonsterDrop_Etc[ItemList[arynum]].CreateItem(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[ItemList[arynum]]);
                                        Destroy(itemetc);
                                        tempnumber = m_pi_Itemslot.Get_Item_Etc(itemetc);
                                        //Dictionary_GetItem_Etc.Add(Dictionary_GetItem_Etc.Count, m_pi_Itemslot.GetItem_Etc(itemetc));
                                        //if (Dictionary_GetItem_Etc_Count.ContainsKey(Dictionary_GetItem_Etc_Count.Count) == false)
                                        //    Dictionary_GetItem_Etc_Count.Add(Dictionary_GetItem_Etc_Count.Count, 1);
                                        //else
                                        //    Dictionary_GetItem_Etc_Count[Dictionary_GetItem_Etc_Count.Count] += 1;
                                        if (j == 0)
                                        {
                                            Dictionary_GetItem_Etc.Add(Dictionary_GetItem_Etc.Count, tempnumber);
                                            Dictionary_GetItem_Etc_Count.Add(Dictionary_GetItem_Etc_Count.Count, item.m_nDictionary_Gift_Item_Etc_Count[i]);
                                        }
                                    }
                                }
                            }
                            GUIManager_Total.Instance.UpdateLog("[소비아이템][" + item.m_sItemName + "] 사용.");

                            GUIManager_Total.Instance.Display_GUI_Gift_GetItem_Info(Dictionary_GetItem_Equip, Dictionary_GetItem_Equip_Count,
                                                                 Dictionary_GetItem_Use, Dictionary_GetItem_Use_Count,
                                                                 Dictionary_GetItem_Etc, Dictionary_GetItem_Etc_Count);

                            return 0;
                        }
                        else
                        {
                            GUIManager_Total.Instance.UpdateLog("[소비아이템][" + item.m_sItemName + "] 사용 불가.");
                            return 2;
                        }
                    }
                    else if (item.m_eItemUseGiftType == E_ITEM_USE_GIFT_TYPE.RANDOMBOX_DEPENDENTTRIAL)
                    {
                        if (CheckCondition_Item_Use_Gift(item, arynumber) == true)
                        {
                            int pickcount = Random.Range(item.m_nRandomBox_PickCount_Min, item.m_nRandomBox_PickCount_Max + 1);

                            List<int> ItemList = new List<int>();
                            List<int> ItemCountList = new List<int>();
                            List<int> ItemProbabilityList = new List<int>();

                            int num = 0;

                            for (int i = 0; i < item.m_nDictionary_Gift_Item_Equip_Code.Count; i++)
                            {
                                ItemList.Add(item.m_nDictionary_Gift_Item_Equip_Code[i]);
                                ItemCountList.Add(item.m_nDictionary_Gift_Item_Equip_Count[i]);
                                if (ItemProbabilityList.Count == 0)
                                {
                                    ItemProbabilityList.Add(item.m_nDictionary_Gift_Item_Equip_Probability[i]);
                                }
                                else
                                {
                                    ItemProbabilityList.Add(ItemProbabilityList[num - 1] + item.m_nDictionary_Gift_Item_Equip_Probability[i]);
                                }
                                num += 1;
                            }
                            for (int i = 0; i < item.m_nDictionary_Gift_Item_Use_Code.Count; i++)
                            {
                                ItemList.Add(item.m_nDictionary_Gift_Item_Use_Code[i]);
                                ItemCountList.Add(item.m_nDictionary_Gift_Item_Use_Count[i]);
                                if (ItemProbabilityList.Count == 0)
                                {
                                    ItemProbabilityList.Add(item.m_nDictionary_Gift_Item_Use_Probability[i]);
                                }
                                else
                                {
                                    ItemProbabilityList.Add(ItemProbabilityList[num - 1] + item.m_nDictionary_Gift_Item_Use_Probability[i]);
                                }
                                num += 1;
                            }
                            for (int i = 0; i < item.m_nDictionary_Gift_Item_Etc_Code.Count; i++)
                            {
                                ItemList.Add(item.m_nDictionary_Gift_Item_Etc_Code[i]);
                                ItemCountList.Add(item.m_nDictionary_Gift_Item_Etc_Count[i]);
                                if (ItemProbabilityList.Count == 0)
                                {
                                    ItemProbabilityList.Add(item.m_nDictionary_Gift_Item_Etc_Probability[i]);
                                }
                                else
                                {
                                    ItemProbabilityList.Add(ItemProbabilityList[num - 1] + item.m_nDictionary_Gift_Item_Etc_Probability[i]);
                                }
                                num += 1;
                            }

                            List<int> List_Already_get = new List<int>();
                            Debug.Log(pickcount + " 번 아이템을 획득합니다.");
                            for (int i = 0; i < pickcount; i++)
                            {
                                int randomnum = Random.Range(1, 10001);
                                int arynum = -1;
                                for (int j = 0; j < ItemList.Count;)
                                {
                                    if (ItemProbabilityList[j] < randomnum)
                                    {
                                        j++;
                                        continue;
                                    }
                                    else
                                    {
                                        if (List_Already_get.Contains(j) == false)
                                        {
                                            arynum = j;
                                            break;
                                        }
                                        else
                                        {
                                            randomnum = Random.Range(1, 10001);
                                            j = 0;
                                        }
                                    }
                                }

                                Debug.Log("아이템 획득: " + ItemList[arynum] + " / " + ItemCountList[arynum] + " / " + ItemProbabilityList[arynum]);
                                if (ItemList[arynum] / 1000 >= 1 && ItemList[arynum] / 1000 <= 7)
                                {
                                    for (int j = 0; j < ItemCountList[arynum]; j++)
                                    {
                                        itemequip = ItemManager.instance.m_Dictionary_MonsterDrop_Equip[ItemList[arynum]].CreateItem(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[ItemList[arynum]]);
                                        Destroy(itemequip);
                                        tempnumber = m_pi_Itemslot.Get_Item_Equip(itemequip);
                                        //Dictionary_GetItem_Equip.Add(Dictionary_GetItem_Equip.Count, m_pi_Itemslot.GetItem_Equip(itemequip));
                                        //if (Dictionary_GetItem_Equip_Count.ContainsKey(Dictionary_GetItem_Equip_Count.Count) == false)
                                        //    Dictionary_GetItem_Equip_Count.Add(Dictionary_GetItem_Equip_Count.Count, 1);
                                        //else
                                        //    Dictionary_GetItem_Equip_Count[Dictionary_GetItem_Equip_Count.Count] += 1;
                                        if (j == 0)
                                        {
                                            Dictionary_GetItem_Equip.Add(Dictionary_GetItem_Equip.Count, tempnumber);
                                            Dictionary_GetItem_Equip_Count.Add(Dictionary_GetItem_Equip_Count.Count, ItemCountList[arynum]);
                                        }
                                    }
                                }
                                else if (ItemList[arynum] / 1000 >= 8 && ItemList[arynum] / 1000 <= 12)
                                {
                                    for (int j = 0; j < ItemCountList[arynum]; j++)
                                    {
                                        itemuse = ItemManager.instance.m_Dictionary_MonsterDrop_Use[ItemList[arynum]].CreateItem(ItemManager.instance.m_Dictionary_MonsterDrop_Use[ItemList[arynum]]);
                                        Destroy(itemuse);
                                        tempnumber = m_pi_Itemslot.Get_Item_Use(itemuse);
                                        //Dictionary_GetItem_Use.Add(Dictionary_GetItem_Use.Count, m_pi_Itemslot.GetItem_Use(itemuse));
                                        //if (Dictionary_GetItem_Use_Count.ContainsKey(Dictionary_GetItem_Use_Count.Count) == false)
                                        //    Dictionary_GetItem_Use_Count.Add(Dictionary_GetItem_Use_Count.Count, 1);
                                        //else
                                        //    Dictionary_GetItem_Use_Count[Dictionary_GetItem_Use_Count.Count] += 1;
                                        if (j == 0)
                                        {
                                            Dictionary_GetItem_Use.Add(Dictionary_GetItem_Use.Count, tempnumber);
                                            Dictionary_GetItem_Use_Count.Add(Dictionary_GetItem_Use_Count.Count, ItemCountList[arynum]);
                                        }
                                    }
                                }
                                else if (ItemList[arynum] / 1000 == 0)
                                {
                                    for (int j = 0; j < ItemCountList[arynum]; j++)
                                    {
                                        itemetc = ItemManager.instance.m_Dictionary_MonsterDrop_Etc[ItemList[arynum]].CreateItem(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[ItemList[arynum]]);
                                        Destroy(itemetc);
                                        tempnumber = m_pi_Itemslot.Get_Item_Etc(itemetc);
                                        //Dictionary_GetItem_Etc.Add(Dictionary_GetItem_Etc.Count, m_pi_Itemslot.GetItem_Etc(itemetc));
                                        //if (Dictionary_GetItem_Etc_Count.ContainsKey(Dictionary_GetItem_Etc_Count.Count) == false)
                                        //    Dictionary_GetItem_Etc_Count.Add(Dictionary_GetItem_Etc_Count.Count, 1);
                                        //else
                                        //    Dictionary_GetItem_Etc_Count[Dictionary_GetItem_Etc_Count.Count] += 1;
                                        if (j == 0)
                                        {
                                            Dictionary_GetItem_Etc.Add(Dictionary_GetItem_Etc.Count, tempnumber);
                                            Dictionary_GetItem_Etc_Count.Add(Dictionary_GetItem_Etc_Count.Count, ItemCountList[arynum]);
                                        }
                                    }
                                }

                                List_Already_get.Add(arynum);
                            }
                            GUIManager_Total.Instance.UpdateLog("[소비아이템][" + item.m_sItemName + "] 사용.");

                            GUIManager_Total.Instance.Display_GUI_Gift_GetItem_Info(Dictionary_GetItem_Equip, Dictionary_GetItem_Equip_Count,
                                                               Dictionary_GetItem_Use, Dictionary_GetItem_Use_Count,
                                                               Dictionary_GetItem_Etc, Dictionary_GetItem_Etc_Count);

                            return 0;
                        }
                        else
                        {
                            GUIManager_Total.Instance.UpdateLog("[소비아이템][" + item.m_sItemName + "] 사용 불가.");
                            return 2;
                        }

                    }
                }
            }
            else
                return 1;
        }

        return 0;
    }

// GUIManager_Total.Instance.UpdateLog("[재화]" + co2_4[i].gameObject.GetComponent<Item>().m_sItemName + " 을(를) 획득 하였습니다.");
// GUIManager_Total.Instance.UpdateLog("[기타 아이템]" + co2_4[i].gameObject.GetComponent<Item>().m_sItemName + " 을(를) 획득 하였습니다.");
// GUIManager_Total.Instance.UpdateLog("[장비 아이템]" + co2_4[i].gameObject.GetComponent<Item>().m_sItemName + " 을(를) 획득 할 수 없습니다.");
// GUIManager_Total.Instance.UpdateLog("[소비 아이템]" + co2_4[i].gameObject.GetComponent<Item>().m_sItemName + " 을(를) 획득 하였습니다.");


    // 소비 아이템 중 기프트타입 사용 조건만 체크. 사용하지 않음.
    public bool CheckCondition_Item_Use_Gift(Item_Use item, int arynumber)
    {
        if (m_pm_Move.m_ePlayerMoveState != Player_Move.E_PLAYER_MOVE_STATE.DEATH)
        {
            if (m_ps_Status.CheckCondition_Item_Use(item) == 0)
            {
                bool item_equip_get_possible = true, item_use_get_possible = true, item_etc_get_possible = true;
                if (item.m_eItemUseGiftType == E_ITEM_USE_GIFT_TYPE.FIXEDBOX)
                {
                    // 장비 아이템을 획득할 장비 아이템 슬롯 공간이 있는지 판단.
                    {
                        int item_equip_count = 0;
                        for (int i = 0; i < item.m_nDictionary_Gift_Item_Equip_Count.Count; i++)
                        {
                            item_equip_count += item.m_nDictionary_Gift_Item_Equip_Count[i];
                        }

                        if (m_pi_Itemslot.Check_Get_Item_Itemslot_Equip(item_equip_count) == true)
                        {
                            //Debug.Log("장비 아이템을 획득할 수 있습니다.");
                        }
                        else
                        {
                            //Debug.Log("장비 아이템 슬롯이 부족합니다.");
                            item_equip_get_possible = false;
                        }
                    }
                    // 소비 아이템을 획득할 소비 아이템 슬롯 공간이 있는지 판단.
                    {
                        if (m_pi_Itemslot.Check_Get_Item_Itemslot_Use(item.m_nDictionary_Gift_Item_Use_Code, item.m_nDictionary_Gift_Item_Use_Count) == true)
                        {
                            //Debug.Log("소비 아이템을 획득할 수 있습니다.");
                        }
                        else
                        {
                            //Debug.Log("소비 아이템 슬롯이 부족합니다.");
                            item_use_get_possible = false;
                        }
                    }
                    // 기타 아이텝을 획득할 기타 아이템 슬롯 공간이 있는지 판단.
                    {
                        if (m_pi_Itemslot.Check_Get_Item_Itemslot_Etc(item.m_nDictionary_Gift_Item_Etc_Code, item.m_nDictionary_Gift_Item_Etc_Count) == true)
                        {
                            //Debug.Log("기타 아이템을 획득할 수 있습니다.");
                        }
                        else
                        {
                            //Debug.Log("기타 아이템 슬롯이 부족합니다.");
                            item_etc_get_possible = false;
                        }
                    }

                    if (item_equip_get_possible == false || item_use_get_possible == false || item_etc_get_possible == false)
                    {
                        return false;
                    }
                    else if (item_equip_get_possible == true && item_use_get_possible == true && item_etc_get_possible == true)
                    {
                        return true;
                    }
                }
                else if (item.m_eItemUseGiftType == E_ITEM_USE_GIFT_TYPE.RANDOMBOX_INDEPENDENTTRIAL)
                {
                    int maxcount = item.m_nRandomBox_PickCount_Max;
                    // 장비 아이템을 획득할 장비 아이템 슬롯 공간이 있는지 판단.
                    // 아이템 슬롯 하나당 장비 아이템 1개.
                    {
                        int item_equip_count = 0;
                        for (int i = 0; i < item.m_nDictionary_Gift_Item_Equip_Count.Count; i++)
                        {
                            if (item_equip_count < item.m_nDictionary_Gift_Item_Equip_Count[i])
                                item_equip_count = item.m_nDictionary_Gift_Item_Equip_Count[i];
                        }

                        maxcount *= item_equip_count;
                        //Debug.Log("최대 획득 가능한 장비 아이템 개수: " + maxcount);
                        if (m_pi_Itemslot.Check_Get_Item_Itemslot_Equip(maxcount) == true)
                        {
                            //Debug.Log("장비 아이템을 획득할 수 있습니다.");
                        }
                        else
                        {
                            //Debug.Log("장비 아이템 슬롯이 부족합니다.");
                            item_equip_get_possible = false;
                        }
                    }
                    maxcount = item.m_nRandomBox_PickCount_Max;
                    // 소비 아이템을 획득할 소비 아이템 슬롯 공간이 있는지 판단.
                    // 아이템 슬롯 하나당 소비 아이템 10개.
                    {
                        int item_use_count = 0;
                        for (int i = 0; i < item.m_nDictionary_Gift_Item_Use_Code.Count; i++)
                        {
                            if (item_use_count < item.m_nDictionary_Gift_Item_Use_Count[i])
                                item_use_count = item.m_nDictionary_Gift_Item_Use_Count[i];
                        }

                        if (item_use_count % 10 == 0)
                        {
                            item_use_count /= 10;
                        }
                        else
                        {
                            item_use_count /= 10;
                            item_use_count += 1;
                        }

                        maxcount *= item_use_count;
                        //Debug.Log("최대 획득 가능한 소비 아이템 개수: " + maxcount);
                        if (m_pi_Itemslot.Check_Get_Item_Itemslot_Use(maxcount) == true)
                        {
                            //Debug.Log("소비 아이템을 획득할 수 있습니다.");
                        }
                        else
                        {
                            //Debug.Log("소비 아이템 슬롯이 부족합니다.");
                            item_use_get_possible = false;
                        }
                    }
                    maxcount = item.m_nRandomBox_PickCount_Max;
                    // 기타 아이텝을 획득할 기타 아이템 슬롯 공간이 있는지 판단.
                    // 아이템 슬롯 하나당 기타 아이템 10개.
                    {
                        int item_etc_count = 0;
                        for (int i = 0; i < item.m_nDictionary_Gift_Item_Etc_Code.Count; i++)
                        {
                            if (item_etc_count < item.m_nDictionary_Gift_Item_Etc_Count[i])
                                item_etc_count = item.m_nDictionary_Gift_Item_Etc_Count[i];
                        }

                        if (item_etc_count % 10 == 0)
                        {
                            item_etc_count /= 10;
                        }
                        else
                        {
                            item_etc_count /= 10;
                            item_etc_count += 1;
                        }

                        maxcount *= item_etc_count;
                        //Debug.Log("최대 획득 가능한 기타 아이템 개수: " + maxcount);
                        if (m_pi_Itemslot.Check_Get_Item_Itemslot_Etc(maxcount) == true)
                        {
                            //Debug.Log("기타 아이템을 획득할 수 있습니다.");
                        }
                        else
                        {
                            //Debug.Log("기타 아이템 슬롯이 부족합니다.");
                            item_etc_get_possible = false;
                        }
                    }

                    if (item_equip_get_possible == false || item_use_get_possible == false || item_etc_get_possible == false)
                    {
                        return false;
                    }
                    else if (item_equip_get_possible == true && item_use_get_possible == true && item_etc_get_possible == true)
                    {
                        return true;
                    }
                }
                else if (item.m_eItemUseGiftType == E_ITEM_USE_GIFT_TYPE.RANDOMBOX_DEPENDENTTRIAL)
                {
                    int maxcount = item.m_nRandomBox_PickCount_Max;
                    // 장비 아이템을 획득할 장비 아이템 슬롯 공간이 있는지 판단.
                    // 아이템 슬롯 하나당 장비 아이템 1개.
                    {
                        int item_equip_count = 0;
                        for (int i = 0; i < item.m_nDictionary_Gift_Item_Equip_Count.Count; i++)
                        {
                            if (item_equip_count < item.m_nDictionary_Gift_Item_Equip_Count[i])
                                item_equip_count = item.m_nDictionary_Gift_Item_Equip_Count[i];
                        }

                        maxcount *= item_equip_count;
                        //Debug.Log("최대 획득 가능한 장비 아이템 개수: " + maxcount);
                        if (m_pi_Itemslot.Check_Get_Item_Itemslot_Equip(maxcount) == true)
                        {
                            //Debug.Log("장비 아이템을 획득할 수 있습니다.");
                        }
                        else
                        {
                            //Debug.Log("장비 아이템 슬롯이 부족합니다.");
                            item_equip_get_possible = false;
                        }
                    }
                    maxcount = item.m_nRandomBox_PickCount_Max;
                    // 소비 아이템을 획득할 소비 아이템 슬롯 공간이 있는지 판단.
                    // 아이템 슬롯 하나당 소비 아이템 10개.
                    {
                        int item_use_count = 0;
                        for (int i = 0; i < item.m_nDictionary_Gift_Item_Use_Code.Count; i++)
                        {
                            if (item_use_count < item.m_nDictionary_Gift_Item_Use_Count[i])
                                item_use_count = item.m_nDictionary_Gift_Item_Use_Count[i];
                        }

                        if (item_use_count % 10 == 0)
                        {
                            item_use_count /= 10;
                        }
                        else
                        {
                            item_use_count /= 10;
                            item_use_count += 1;
                        }

                        maxcount *= item_use_count;

                        //Debug.Log("최대 획득 가능한 소비 아이템 개수: " + maxcount);
                        if (m_pi_Itemslot.Check_Get_Item_Itemslot_Use(maxcount) == true)
                        {
                            //Debug.Log("소비 아이템을 획득할 수 있습니다.");
                        }
                        else
                        {
                            //Debug.Log("소비 아이템 슬롯이 부족합니다.");
                            item_use_get_possible = false;
                        }
                    }
                    maxcount = item.m_nRandomBox_PickCount_Max;
                    // 기타 아이텝을 획득할 기타 아이템 슬롯 공간이 있는지 판단.
                    // 아이템 슬롯 하나당 기타 아이템 10개.
                    {
                        int item_etc_count = 0;
                        for (int i = 0; i < item.m_nDictionary_Gift_Item_Etc_Code.Count; i++)
                        {
                            if (item_etc_count < item.m_nDictionary_Gift_Item_Etc_Count[i])
                                item_etc_count = item.m_nDictionary_Gift_Item_Etc_Count[i];
                        }

                        if (item_etc_count % 10 == 0)
                        {
                            item_etc_count /= 10;
                        }
                        else
                        {
                            item_etc_count /= 10;
                            item_etc_count += 1;
                        }

                        maxcount *= item_etc_count;
                        //Debug.Log("최대 획득 가능한 기타 아이템 개수: " + maxcount);
                        if (m_pi_Itemslot.Check_Get_Item_Itemslot_Etc(maxcount) == true)
                        {
                            //Debug.Log("기타 아이템을 획득할 수 있습니다.");
                        }
                        else
                        {
                            //Debug.Log("기타 아이템 슬롯이 부족합니다.");
                            item_etc_get_possible = false;
                        }
                    }

                    if (item_equip_get_possible == false || item_use_get_possible == false || item_etc_get_possible == false)
                    {
                        return false;
                    }
                    else if (item_equip_get_possible == true && item_use_get_possible == true && item_etc_get_possible == true)
                    {
                        return true;
                    }
                }
                else if (item.m_eItemUseGiftType == E_ITEM_USE_GIFT_TYPE.FUSION)
                {

                }
            }
        }

        return false;
    }

    // 퀵슬롯 전용 장비 아이템 사용 조건 체크.
    //
    // ※ 함수이름 변경 예정. CheckCondition_Item_Use -> CheckCondition_Quickslot_Item_Equip
    //
    public bool CheckCondition_Item_Use(Item_Equip item, int arynumber)
    {
        Item_Equip switchingitem;
        bool itemexit = true;

        switch (item.m_eItemEquipType)
        {
            case E_ITEM_EQUIP_TYPE.HAT:
                {
                    if (Player_Equipment.m_bEquipment_Hat == true)
                    {
                        switchingitem = Player_Equipment.m_gEquipment_Hat;
                    }
                    else
                    {
                        switchingitem = null;
                        itemexit = false;
                    }
                }
                break;
            case E_ITEM_EQUIP_TYPE.TOP:
                {
                    if (Player_Equipment.m_bEquipment_Top == true)
                    {
                        switchingitem = Player_Equipment.m_gEquipment_Top;
                    }
                    else
                    {
                        switchingitem = null;
                        itemexit = false;
                    }
                }
                break;
            case E_ITEM_EQUIP_TYPE.BOTTOMS:
                {
                    if (Player_Equipment.m_bEquipment_Bottoms == true)
                    {
                        switchingitem = Player_Equipment.m_gEquipment_Bottoms;
                    }
                    else
                    {
                        switchingitem = null;
                        itemexit = false;
                    }
                }
                break;
            case E_ITEM_EQUIP_TYPE.SHOSE:
                {
                    if (Player_Equipment.m_bEquipment_Shose == true)
                    {
                        switchingitem = Player_Equipment.m_gEquipment_Shose;
                    }
                    else
                    {
                        switchingitem = null;
                        itemexit = false;
                    }
                }
                break;
            case E_ITEM_EQUIP_TYPE.GLOVES:
                {
                    if (Player_Equipment.m_bEquipment_Gloves == true)
                    {
                        switchingitem = Player_Equipment.m_gEquipment_Gloves;
                    }
                    else
                    {
                        switchingitem = null;
                        itemexit = false;
                    }
                }
                break;
            case E_ITEM_EQUIP_TYPE.MAINWEAPON:
                {
                    if (Player_Equipment.m_bEquipment_Mainweapon == true)
                    {
                        switchingitem = Player_Equipment.m_gEquipment_Mainweapon;
                    }
                    else
                    {
                        switchingitem = null;
                        itemexit = false;
                    }
                }
                break;
            case E_ITEM_EQUIP_TYPE.SUBWEAPON:
                {
                    if (Player_Equipment.m_bEquipment_Subweapon == true)
                    {
                        switchingitem = Player_Equipment.m_gEquipment_Subweapon;
                    }
                    else
                    {
                        switchingitem = null;
                        itemexit = false;
                    }
                }
                break;
            default:
                {
                    switchingitem = null;
                    itemexit = false;
                }
                break;
        }

        if (CheckCondition_Item_Equip(item, m_ps_Status.m_sStatus, m_ps_Status.m_sSoc) == true)
        {
            m_pe_Equipment.Equip(item);
            m_pm_Move.Equip();

            //Player_Itemslot.m_gary_Itemslot_Equip[arynumber] = switchingitem;

            if (itemexit == true)
            {
                Player_Itemslot.m_gary_Itemslot_Equip[arynumber] = switchingitem;
                Player_Itemslot.m_nary_Itemslot_Equip_Count[arynumber] = 1;
            }
            else
            {
                Player_Itemslot.m_gary_Itemslot_Equip[arynumber] = null;
                Player_Itemslot.m_nary_Itemslot_Equip_Count[arynumber] = 0;
                //GUIManager_Total.Instance.Update_Quickslot_Equip(arynumber);
            }

            GUIManager_Total.Instance.Update_Itemslot();
            GUIManager_Total.Instance.Update_Equipslot();
            CheckSetItemEffect();
            m_pm_Move.SetAttackSpeed(Player_Total.Instance.m_ps_Status.Return_AttackSpeed());
            GUIManager_Total.Instance.Update_SS();

            if (GUIManager_Total.Instance.m_GUI_Itemslot_Equip_Information.m_gPanel_Itemslot_Equip_Information.activeSelf == true)
                GUIManager_Total.Instance.m_GUI_Itemslot_Equip_Information.m_gPanel_Itemslot_Equip_Information.SetActive(false);
            if (GUIManager_Total.Instance.m_GUI_Equipslot_Equip_Information.m_gPanel_Equipslot_Equip_Information.activeSelf == true)
                GUIManager_Total.Instance.m_GUI_Equipslot_Equip_Information.m_gPanel_Equipslot_Equip_Information.SetActive(false);

            if (GUIManager_Total.Instance.m_GUI_Status.m_gPanel_DetailStatus.activeSelf == true)
            {
                GUIManager_Total.Instance.m_GUI_Status.UpdateStatus_SetItemEffect(CheckSetItemEffect_UI());
            }

            return true;
        }
        else
        {


            return false;
        }
    }

    // 퀵슬롯 전용 소비 아이템 사용 조건 체크.
    public bool CheckCondition_Item_Use(Item_Use item)
    {
        if (m_ps_Status.CheckCondition_Item_Use(item) == 0)
        {
            if (item.m_eItemUseType == E_ITEM_USE_TYPE.RECOVERPOTION ||
                item.m_eItemUseType == E_ITEM_USE_TYPE.TEMPORARYBUFFPOTION ||
                item.m_eItemUseType == E_ITEM_USE_TYPE.ETERNALBUFFPOTION)
            {
                int Item_Use_Code = m_ps_Status.ApplyPotion(item);
                m_pm_Move.SetAttackSpeed(m_ps_Status.Return_AttackSpeed());

                if (Item_Use_Code == 1)
                {
                    return false;
                }
                else
                {
                    return true;
                }

                //GUIManager_Total.Instance.Update_SS();
                //GUIManager_Total.Instance.Update_Itemslot();
                //GUIManager_Total.Instance.Update_Equipslot();
            }
            else if (item.m_eItemUseType == E_ITEM_USE_TYPE.GIFT)
            {

            }

            return true;
        }

        return false;
    }

    // 퀵슬롯.
    // 퀵슬롯에 등록된 아이템의 총 수량 확인.
    public bool Check_Quickslot_Item(E_QUICKSLOT_CATEGORY eqc, int itemcode)
    {
        if (m_pi_Itemslot.Check_Quickslot_Item(eqc, itemcode) > 0)
            return true;
        else
            return false;
    }
    // 퀵슬롯에 등록된 아이템 수 반환
    public int Return_Quickslot_Item_Count(E_QUICKSLOT_CATEGORY eqc, int itemcode)
    {
        return m_pi_Itemslot.Check_Quickslot_Item(eqc, itemcode);
    }

    // 퀵슬롯에 등록된 아이템 사용.
    public bool Use_Quickslot_Item_Use(E_QUICKSLOT_CATEGORY eqc, int itemcode)
    {
        if (Check_Quickslot_Item(eqc, itemcode) == true)
        {
            if (CheckCondition_Item_Use(ItemManager.instance.m_Dictionary_MonsterDrop_Use[itemcode]) == true)
            {
                m_pi_Itemslot.Use_Quickslot_Item(eqc, itemcode);

                if (Return_Quickslot_Item_Count(E_QUICKSLOT_CATEGORY.USE, itemcode) == 0)
                {
                    if (GUIManager_Total.Instance.m_GUI_Itemslot_Use_Information.m_gPanel_Itemslot_Use_Information.activeSelf == true)
                        GUIManager_Total.Instance.m_GUI_Itemslot_Use_Information.m_gPanel_Itemslot_Use_Information.SetActive(false);
                }

                return true;
            }
            else
                return false;

            //GUIManager_Total.Instance.Update_SS();
            //GUIManager_Total.Instance.Update_Itemslot();
            //GUIManager_Total.Instance.Update_Equipslot();
        }
        else
            return false;
    }
    // 퀵슬롯에 등록된 장비아이템 착용(스위칭).
    public void Use_Quickslot_Item_Equip(E_QUICKSLOT_CATEGORY eqc, int arynumber, int quickslotarynumber)
    {
        if (Check_Quickslot_Item(eqc, arynumber) == true)
        {
            if (GUIManager_Total.Instance.m_GUI_Quickslot.m_qsList_Quickslot[quickslotarynumber].m_nItem_Equip_AryNumber != -1)
            {
                if (CheckCondition_Item_Use(Player_Itemslot.m_gary_Itemslot_Equip[arynumber], arynumber) == true)
                {
                    GUIManager_Total.Instance.Set_Quickslot(quickslotarynumber, eqc, arynumber);

                    m_pi_Itemslot.Use_Quickslot_Item(eqc, arynumber);
                }
            }

            GUIManager_Total.Instance.Update_Itemslot();
            GUIManager_Total.Instance.Update_Equipslot();
            CheckSetItemEffect();
            m_pm_Move.SetAttackSpeed(Player_Total.Instance.m_ps_Status.Return_AttackSpeed());
            GUIManager_Total.Instance.Update_SS();
        }
    }

    //// Test_Respone
    //public void InputKey_F1()
    //{
    //    if (Input.GetKeyUp(KeyCode.F1))
    //    {

    //    }
    //}

    public void ReTry(int retrynumber, int retryprice)
    {
        ReTry_Lost(retrynumber, retryprice);

        GUIManager_Total.Instance.Update_Itemslot();
        GUIManager_Total.Instance.Update_Equipslot();
        CheckSetItemEffect();
        m_pm_Move.SetAttackSpeed(Player_Total.Instance.m_ps_Status.Return_AttackSpeed());
        GUIManager_Total.Instance.Update_SS();
        GUIManager_Total.Instance.Update_Quickslot();
        m_pq_Quest.QuestUpdate_Collect_NoDisplay();
        //GUIManager_Total.Instance.Update_ChangeMap();
    }
    void ReTry_Lost(int retrynumber, int retryprice)
    {
        m_pi_Itemslot.ReTry_Pay_Gold(retryprice);
        int item_equip_count, item_use_count, item_etc_count;
        // 플레이어 리트라이 시 가지고 있는 모든 아이템 목록.
        Dictionary<int, int> dictionary_item_equip, dictionary_item_use, dictionary_item_etc;
        // 플레이어 리트라이 시 잃어버리는 아이템 목록.
        Dictionary<int, int> dictionary_lost_item_equip, dictionary_lost_item_use, dictionary_lost_item_etc;
        // 플레이어 리트라이 시 잃어버리는 아이템 로직 관련.
        List<int> list_lost_item_equip, list_lost_item_use, list_lost_item_etc;

        item_equip_count = 0;

        // 장착중인 장비 아이템 정보.
        // Dictionary <int, int> dictionary_item_etc: <arynumber, itemcount>
        // List <int> list_lost_item_etc: <itemnumber>
        {
            dictionary_item_equip = new Dictionary<int, int>();
            list_lost_item_equip = new List<int>();
            item_use_count = 0;

            // 소유중인 장비 아이템 정보.
            // <int, int> : <itemnumber, itemcode>
            for (int i = 0; i < Player_Itemslot.m_nary_Itemslot_Equip_Count.Length; i++)
            {
                if (Player_Itemslot.m_nary_Itemslot_Equip_Count[i] != 0)
                {
                    item_equip_count += 1;
                    dictionary_item_equip.Add(Player_Itemslot.m_gary_Itemslot_Equip[i].m_nItemNumber, Player_Itemslot.m_gary_Itemslot_Equip[i].m_nItemCode);
                    list_lost_item_equip.Add(Player_Itemslot.m_gary_Itemslot_Equip[i].m_nItemNumber);
                }
            }

            if (Player_Equipment.m_bEquipment_Hat == true)
            {
                item_equip_count += 1;
                dictionary_item_equip.Add(Player_Equipment.m_gEquipment_Hat.m_nItemNumber, Player_Equipment.m_gEquipment_Hat.m_nItemCode);
                list_lost_item_equip.Add(Player_Equipment.m_gEquipment_Hat.m_nItemNumber);
            }
            if (Player_Equipment.m_bEquipment_Top == true)
            {
                item_equip_count += 1;
                dictionary_item_equip.Add(Player_Equipment.m_gEquipment_Top.m_nItemNumber, Player_Equipment.m_gEquipment_Top.m_nItemCode);
                list_lost_item_equip.Add(Player_Equipment.m_gEquipment_Top.m_nItemNumber);
            }
            if (Player_Equipment.m_bEquipment_Bottoms == true)
            {
                item_equip_count += 1;
                dictionary_item_equip.Add(Player_Equipment.m_gEquipment_Bottoms.m_nItemNumber, Player_Equipment.m_gEquipment_Bottoms.m_nItemCode);
                list_lost_item_equip.Add(Player_Equipment.m_gEquipment_Bottoms.m_nItemNumber);
            }
            if (Player_Equipment.m_bEquipment_Gloves == true)
            {
                item_equip_count += 1;
                dictionary_item_equip.Add(Player_Equipment.m_gEquipment_Gloves.m_nItemNumber, Player_Equipment.m_gEquipment_Gloves.m_nItemCode);
                list_lost_item_equip.Add(Player_Equipment.m_gEquipment_Gloves.m_nItemNumber);
            }
            if (Player_Equipment.m_bEquipment_Shose == true)
            {
                item_equip_count += 1;
                dictionary_item_equip.Add(Player_Equipment.m_gEquipment_Shose.m_nItemNumber, Player_Equipment.m_gEquipment_Shose.m_nItemCode);
                list_lost_item_equip.Add(Player_Equipment.m_gEquipment_Shose.m_nItemNumber);
            }
            if (Player_Equipment.m_bEquipment_Mainweapon == true)
            {
                item_equip_count += 1;
                dictionary_item_equip.Add(Player_Equipment.m_gEquipment_Mainweapon.m_nItemNumber, Player_Equipment.m_gEquipment_Mainweapon.m_nItemCode);
                list_lost_item_equip.Add(Player_Equipment.m_gEquipment_Mainweapon.m_nItemNumber);
            }
            if (Player_Equipment.m_bEquipment_Subweapon == true)
            {
                item_equip_count += 1;
                dictionary_item_equip.Add(Player_Equipment.m_gEquipment_Subweapon.m_nItemNumber, Player_Equipment.m_gEquipment_Subweapon.m_nItemCode);
                list_lost_item_equip.Add(Player_Equipment.m_gEquipment_Subweapon.m_nItemNumber);
            }
        }

        // 소유중인 소비 아이템 정보.
        // Dictionary <int, int> dictionary_item_use: <itemcode, itemcount>
        // List <int> list_lost_item_use: <itemcode>
        {
            dictionary_item_use = new Dictionary<int, int>();
            list_lost_item_use = new List<int>();
            item_use_count = 0;
            for (int i = 0; i < Player_Itemslot.m_nary_Itemslot_Use_Count.Length; i++)
            {
                if (Player_Itemslot.m_nary_Itemslot_Use_Count[i] != 0)
                {
                    item_use_count += Player_Itemslot.m_nary_Itemslot_Use_Count[i];
                    if (dictionary_item_use.ContainsKey(Player_Itemslot.m_gary_Itemslot_Use[i].m_nItemCode) == true)
                    {
                        dictionary_item_use[Player_Itemslot.m_gary_Itemslot_Use[i].m_nItemCode] += Player_Itemslot.m_nary_Itemslot_Use_Count[i];
                    }
                    else
                    {
                        dictionary_item_use.Add(Player_Itemslot.m_gary_Itemslot_Use[i].m_nItemCode, Player_Itemslot.m_nary_Itemslot_Use_Count[i]);
                    }
                    for (int j = 0; j < Player_Itemslot.m_nary_Itemslot_Use_Count[i]; j++)
                    {
                        list_lost_item_use.Add(Player_Itemslot.m_gary_Itemslot_Use[i].m_nItemCode);
                    }
                }
            }
        }

        // 소유중인 기타 아이템 정보.
        // Dictionary <int, int> dictionary_item_etc: <itemcode, itemcount>
        // List <int> list_lost_item_etc: <itemcode>
        {
            dictionary_item_etc = new Dictionary<int, int>();
            list_lost_item_etc = new List<int>();
            item_etc_count = 0;
            for (int i = 0; i < Player_Itemslot.m_nary_Itemslot_Etc_Count.Length; i++)
            {
                if (Player_Itemslot.m_nary_Itemslot_Etc_Count[i] != 0)
                {
                    item_etc_count += Player_Itemslot.m_nary_Itemslot_Etc_Count[i];
                    if (dictionary_item_etc.ContainsKey(Player_Itemslot.m_gary_Itemslot_Etc[i].m_nItemCode) == true)
                    {
                        dictionary_item_etc[Player_Itemslot.m_gary_Itemslot_Etc[i].m_nItemCode] += Player_Itemslot.m_nary_Itemslot_Etc_Count[i];
                    }
                    else
                    {
                        dictionary_item_etc.Add(Player_Itemslot.m_gary_Itemslot_Etc[i].m_nItemCode, Player_Itemslot.m_nary_Itemslot_Etc_Count[i]);
                    }
                    for (int j = 0; j < Player_Itemslot.m_nary_Itemslot_Etc_Count[i]; j++)
                    {
                        list_lost_item_etc.Add(Player_Itemslot.m_gary_Itemslot_Etc[i].m_nItemCode);
                    }
                }
            }
        }

        int lostcount_item_equip, lostcount_item_use, lostcount_item_etc, arynumber, itemcode;
        int lostgold_min, lostgold_max;
        int randomnumber;

        // 로스트.
        if (retrynumber == 1)
        {
            lostcount_item_equip = item_equip_count / 5;
            //if (lostcount_item_equip == 0 && item_equip_count != 0)
            //{
            //    randomnumber = Random.RandomRange(0, 5);
            //    if (randomnumber < item_equip_count)
            //    {
            //        lostcount_item_equip = 1;
            //    }
            //}
            lostcount_item_use = item_use_count / 3;
            //if (lostcount_item_use == 0 && item_use_count != 0)
            //{
            //    randomnumber = Random.RandomRange(0, 3);
            //    if (randomnumber < item_use_count)
            //    {
            //        lostcount_item_use = 1;
            //    }
            //}
            lostcount_item_etc = item_etc_count / 3;
            //if (lostcount_item_etc == 0 && item_etc_count != 0)
            //{
            //    randomnumber = Random.RandomRange(0, 3);
            //    if (randomnumber < item_etc_count)
            //    {
            //        lostcount_item_etc = 1;
            //    }
            //}
            lostgold_min = 1; lostgold_max = 10;
        }
        else if (retrynumber == 2)
        {
            lostcount_item_equip = item_equip_count / 10;
            //if (lostcount_item_equip == 0 && item_equip_count != 0)
            //{
            //    randomnumber = Random.RandomRange(0, 10);
            //    if (randomnumber < item_equip_count)
            //    {
            //        lostcount_item_equip = 1;
            //    }
            //}
            lostcount_item_use = item_use_count / 5;
            //if (lostcount_item_use == 0 && item_use_count != 0)
            //{
            //    randomnumber = Random.RandomRange(0, 5);
            //    if (randomnumber < item_use_count)
            //    {
            //        lostcount_item_use = 1;
            //    }
            //}
            lostcount_item_etc = item_etc_count / 5;
            //if (lostcount_item_etc == 0 && item_etc_count != 0)
            //{
            //    randomnumber = Random.RandomRange(0, 5);
            //    if (randomnumber < item_etc_count)
            //    {
            //        lostcount_item_etc = 1;
            //    }
            //}
            lostgold_min = 1; lostgold_max = 5;
        }
        else if (retrynumber == 3)
        {
            lostcount_item_equip = 0;
            lostcount_item_use = item_use_count / 10;
            //if (lostcount_item_use == 0 && item_use_count != 0)
            //{
            //    randomnumber = Random.RandomRange(0, 10);
            //    if (randomnumber < item_use_count)
            //    {
            //        lostcount_item_use = 1;
            //    }
            //}
            lostcount_item_etc = item_etc_count / 10;
            //if (lostcount_item_etc == 0 && item_etc_count != 0)
            //{
            //    randomnumber = Random.RandomRange(0, 10);
            //    if (randomnumber < item_etc_count)
            //    {
            //        lostcount_item_etc = 1;
            //    }
            //}
            lostgold_min = 1; lostgold_max = 1;
        }
        else
        {
            lostcount_item_equip = 0;
            lostcount_item_use = 0;
            lostcount_item_etc = 0;
            lostgold_min = 0; lostgold_max = 0;
        }



        dictionary_lost_item_equip = new Dictionary<int, int>();
        dictionary_lost_item_use = new Dictionary<int, int>();
        dictionary_lost_item_etc = new Dictionary<int, int>();

        for (int i = 0; i < lostcount_item_equip; i++)
        {
            arynumber = Random.Range(0, list_lost_item_equip.Count);

            dictionary_lost_item_equip.Add(list_lost_item_equip[arynumber], dictionary_item_equip[list_lost_item_equip[arynumber]]);

            list_lost_item_equip.RemoveAt(arynumber);
        }
        for (int i = 0; i < lostcount_item_use; i++)
        {
            itemcode = Random.Range(0, list_lost_item_use.Count);

            if (dictionary_lost_item_use.ContainsKey(list_lost_item_use[itemcode]) == true)
            {
                dictionary_lost_item_use[list_lost_item_use[itemcode]] += 1;
            }
            else
            {
                dictionary_lost_item_use.Add(list_lost_item_use[itemcode], 1);
            }

            list_lost_item_use.RemoveAt(itemcode);
        }
        for (int i = 0; i < lostcount_item_etc; i++)
        {
            itemcode = Random.Range(0, list_lost_item_etc.Count);

            if (dictionary_lost_item_etc.ContainsKey(list_lost_item_etc[itemcode]) == true)
            {
                dictionary_lost_item_etc[list_lost_item_etc[itemcode]] += 1;
            }
            else
            {
                dictionary_lost_item_etc.Add(list_lost_item_etc[itemcode], 1);
            }

            list_lost_item_etc.RemoveAt(itemcode);
        }

        m_pi_Itemslot.ReTry_Lost_Item_Equip(dictionary_lost_item_equip);
        m_pe_Equipment.ReTry_Lost_Item_Equip(dictionary_lost_item_equip);
        m_pi_Itemslot.ReTry_Lost_Item_Use(dictionary_lost_item_use);
        m_pi_Itemslot.ReTry_Lost_Item_Etc(dictionary_lost_item_etc);
        int lostgold = m_pi_Itemslot.ReTry_Lost_Gold(lostgold_min, lostgold_max);

        //if (Total_Manager.Instance.m_nSceneNumber == 0)
        //{
        //    m_pm_Map.ChangeMap(MapManager.Instance.m_List_ReTryArea_Tutorial[0].m_Map_ReTry);
        //    this.gameObject.transform.position = MapManager.Instance.m_List_ReTryArea_Tutorial[0].m_vReTryPos;
        //}
        //else if (Total_Manager.Instance.m_nSceneNumber == 1)
        //{
        //    randomnumber = Random.Range(0, MapManager.Instance.m_List_ReTryArea_Chapter1.Count);

        //    m_pm_Map.ChangeMap(MapManager.Instance.m_List_ReTryArea_Chapter1[randomnumber].m_Map_ReTry);
        //    this.gameObject.transform.position = MapManager.Instance.m_List_ReTryArea_Chapter1[randomnumber].m_vReTryPos;
        //}
        //GUIManager_Total.Instance.Update_ChangeMap();
        GUIManager_Total.Instance.Display_GUI_ReTry_Information(dictionary_lost_item_equip, dictionary_lost_item_use, dictionary_lost_item_etc, lostgold);

        //Debug.Log("잃어버리는 아이템 목록");
        //foreach (KeyValuePair<int, int> item in dictionary_lost_item_equip)
        //{
        //    Debug.Log(item.Key + ": " + item.Value);
        //}
        //foreach (KeyValuePair<int, int> item in dictionary_lost_item_use)
        //{
        //    Debug.Log(item.Key + ": " + ItemManager.instance.m_Dictionary_MonsterDrop_Use[item.Key].m_sItemName + " / " + item.Value);
        //}
        //foreach (KeyValuePair<int, int> item in dictionary_lost_item_etc)
        //{
        //    Debug.Log(item.Key + ": " + ItemManager.instance.m_Dictionary_MonsterDrop_Etc[item.Key].m_sItemName + " / " + item.Value);
        //}

        //foreach (KeyValuePair<int, int> item in dictionary_item_equip_es)
        //{
        //    Debug.Log(item.Key + ": " + ItemManager.instance.m_Dictionary_MonsterDrop_Equip[item.Key].m_sItemName);
        //}
        //foreach (KeyValuePair<int, int> item in dictionary_item_equip_is)
        //{
        //    Debug.Log(item.Value + ": " + ItemManager.instance.m_Dictionary_MonsterDrop_Equip[item.Key].m_sItemName);
        //}
        //foreach (KeyValuePair<int, int> item in dictionary_item_use)
        //{
        //    Debug.Log(item.Key + ": " + ItemManager.instance.m_Dictionary_MonsterDrop_Use[item.Key].m_sItemName + " / " + item.Value);
        //}
        //foreach (KeyValuePair<int, int> item in dictionary_item_etc)
        //{
        //    Debug.Log(item.Key + ": " + ItemManager.instance.m_Dictionary_MonsterDrop_Etc[item.Key].m_sItemName + " / " + item.Value);
        //}

        dictionary_item_equip.Clear();
        dictionary_item_use.Clear();
        dictionary_item_etc.Clear();

    }

    // CameraMove
    public void CameraMove(Vector2 pos)
    {
        m_pc_Camera.CameraMove(pos);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (m_nPosValue == 1)
            Gizmos.DrawWireCube(this.transform.position + new Vector3(0.125f, 0.15f, 0), m_vSize);
        else
            Gizmos.DrawWireCube(this.transform.position + new Vector3(-0.125f, 0.15f, 0), m_vSize);

        Gizmos.DrawWireSphere(this.transform.position, 0.5f);
    }

    // Player Map 변경
    public bool ChangeMap(bool condiiton, Map map)
    {
        if (condiiton == true)
        {
            m_pm_Map.ChangeMap(map);
            GUIManager_Total.Instance.Update_MapName(map);

            return true;
        }
        else
        {
            return false;
        }
    }
    public bool ChangeMap(bool condiiton, Map map, Vector3 pos)
    {
        if (condiiton == true)
        {
            m_pm_Map.ChangeMap(map);
            m_pm_Move.ChangeMap(pos);
            GUIManager_Total.Instance.Update_MapName(map);

            return true;
        }
        else
        {
            return false;
        }
    }

    // Animation Test
    void AnimationTest()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            Debug.Log("Test: Player: IDLE");
            m_pm_Move.SetAnimatorParameters("Idle");
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            Debug.Log("Test: Player: RUN");
            m_pm_Move.SetAnimatorParameters("Run");
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            Debug.Log("Test: Player: ATTACK1_1");
            m_pm_Move.SetAnimatorParameters("Attack1_1");
        }
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            Debug.Log("Test: Player: ATTACK1_2");
            m_pm_Move.SetAnimatorParameters("Attack1_2");
        }
        if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            Debug.Log("Test: Player: ATTACK1_3");
            m_pm_Move.SetAnimatorParameters("Attack1_3");
        }
        if (Input.GetKeyUp(KeyCode.Alpha6))
        {
            Debug.Log("Test: Player: ATTACKED");
            m_pm_Move.SetAnimatorParameters("Attacked");
        }
        if (Input.GetKeyUp(KeyCode.Alpha7))
        {
            Debug.Log("Test: Player: DEATH");
            m_pm_Move.SetAnimatorParameters("Death");
        }
        if (Input.GetKeyUp(KeyCode.Alpha8))
        {
            Debug.Log("Test: Player: ROLL");
            m_pm_Move.SetAnimatorParameters("Roll");
        }
        if (Input.GetKeyUp(KeyCode.Alpha9))
        {
            Debug.Log("Test: Player: GOAWAY");
            m_pm_Move.SetAnimatorParameters("Goaway");
        }
    }
}
