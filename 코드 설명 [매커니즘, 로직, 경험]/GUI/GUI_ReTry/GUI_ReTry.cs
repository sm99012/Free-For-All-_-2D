using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUI_ReTry : MonoBehaviour
{
    [SerializeField] GameObject m_gPanel_ReTry;

    [SerializeField] GameObject m_gPanel_ReTry_Content;

    [SerializeField] GameObject m_gPanel_ReTry_Content_Description;
    [SerializeField] GameObject m_gTMP_ReTry_Content_Description;
    [SerializeField] TextMeshProUGUI m_TMP_ReTry_Content_Description;

    [SerializeField] GameObject m_gPanel_ReTry_Content_CheckBox;
    [SerializeField] GameObject m_gPanel_ReTry_Content_CheckBox_ReTry1;
    [SerializeField] Toggle m_TG_ReTry_Content_CheckBox_ReTry1;
    [SerializeField] GameObject m_gPanel_ReTry_Content_CheckBox_ReTry2;
    [SerializeField] Toggle m_TG_ReTry_Content_CheckBox_ReTry2;
    [SerializeField] GameObject m_gPanel_ReTry2_Rock;
    [SerializeField] GameObject m_gPanel_ReTry_Content_CheckBox_ReTry3;
    [SerializeField] Toggle m_TG_ReTry_Content_CheckBox_ReTry3;
    [SerializeField] GameObject m_gPanel_ReTry3_Rock;
    [SerializeField] GameObject m_gPanel_ReTry_Content_CheckBox_ReTry4;
    [SerializeField] Toggle m_TG_ReTry_Content_CheckBox_ReTry4;
    [SerializeField] GameObject m_gPanel_ReTry4_Rock;

    [SerializeField] GameObject m_gPanel_ReTry_Content_Information;
    [SerializeField] GameObject m_gPanel_ReTry_Content_Information_L;
    [SerializeField] TextMeshProUGUI m_TMP_ReTry_Content_Information_L;
    [SerializeField] GameObject m_gPanel_ReTry_Content_Information_R;
    [SerializeField] TextMeshProUGUI m_TMP_ReTry_Content_Information_R;
    [SerializeField] GameObject m_gPanel_ReTry_Content_Information_D;
    [SerializeField] TextMeshProUGUI m_TMP_ReTry_Content_Information_D;

    [SerializeField] GameObject m_gPanel_ReTry_Content_ReTry;
    [SerializeField] GameObject m_gBTN_ReTry_Content_ReTry;
    [SerializeField] Button m_BTN_ReTry_Content_ReTry;
    [SerializeField] TextMeshProUGUI m_TMP_ReTry_Content_ReTry;

    public int m_nReTryGold;



    public void InitialSet()
    {
        InitialSet_Object();
        InitialSet_Button();
        InitialSet_Toggle();
    }

    void InitialSet_Object()
    {
        m_gPanel_ReTry = GameObject.Find("Canvas_GUI").gameObject.transform.Find("Panel_ReTry").gameObject;

        m_gPanel_ReTry_Content = m_gPanel_ReTry.transform.Find("Panel_ReTry_Content").gameObject;

        m_gPanel_ReTry_Content_Description = m_gPanel_ReTry_Content.transform.Find("Panel_ReTry_Content_Description").gameObject;
        m_gTMP_ReTry_Content_Description = m_gPanel_ReTry_Content_Description.transform.Find("TMP_ReTry_Content_Description").gameObject;
        m_TMP_ReTry_Content_Description = m_gTMP_ReTry_Content_Description.GetComponent<TextMeshProUGUI>();

        m_gPanel_ReTry_Content_CheckBox = m_gPanel_ReTry_Content.transform.Find("Panel_ReTry_Content_CheckBox").gameObject;
        m_gPanel_ReTry_Content_CheckBox_ReTry1 = m_gPanel_ReTry_Content_CheckBox.transform.Find("TG_ReTry_Content_CheckBox_ReTry1").gameObject;
        m_TG_ReTry_Content_CheckBox_ReTry1 = m_gPanel_ReTry_Content_CheckBox_ReTry1.GetComponent<Toggle>();
        m_gPanel_ReTry_Content_CheckBox_ReTry2 = m_gPanel_ReTry_Content_CheckBox.transform.Find("TG_ReTry_Content_CheckBox_ReTry2").gameObject;
        m_TG_ReTry_Content_CheckBox_ReTry2 = m_gPanel_ReTry_Content_CheckBox_ReTry2.GetComponent<Toggle>();
        m_gPanel_ReTry2_Rock = m_gPanel_ReTry_Content_CheckBox.transform.Find("Panel_ReTry2_Rock").gameObject;
        m_gPanel_ReTry_Content_CheckBox_ReTry3 = m_gPanel_ReTry_Content_CheckBox.transform.Find("TG_ReTry_Content_CheckBox_ReTry3").gameObject;
        m_TG_ReTry_Content_CheckBox_ReTry3 = m_gPanel_ReTry_Content_CheckBox_ReTry3.GetComponent<Toggle>();
        m_gPanel_ReTry3_Rock = m_gPanel_ReTry_Content_CheckBox.transform.Find("Panel_ReTry3_Rock").gameObject;
        m_gPanel_ReTry_Content_CheckBox_ReTry4 = m_gPanel_ReTry_Content_CheckBox.transform.Find("TG_ReTry_Content_CheckBox_ReTry4").gameObject;
        m_TG_ReTry_Content_CheckBox_ReTry4 = m_gPanel_ReTry_Content_CheckBox_ReTry4.GetComponent<Toggle>();
        m_gPanel_ReTry4_Rock = m_gPanel_ReTry_Content_CheckBox.transform.Find("Panel_ReTry4_Rock").gameObject;

        m_gPanel_ReTry_Content_Information = m_gPanel_ReTry_Content.transform.Find("Panel_ReTry_Content_Information").gameObject;
        m_gPanel_ReTry_Content_Information_L = m_gPanel_ReTry_Content_Information.transform.Find("Panel_ReTry_Content_Information_L").gameObject;
        m_TMP_ReTry_Content_Information_L = m_gPanel_ReTry_Content_Information_L.transform.Find("TMP_ReTry_Content_Information_L").gameObject.GetComponent<TextMeshProUGUI>();
        m_gPanel_ReTry_Content_Information_R = m_gPanel_ReTry_Content_Information.transform.Find("Panel_ReTry_Content_Information_R").gameObject;
        m_TMP_ReTry_Content_Information_R = m_gPanel_ReTry_Content_Information_R.transform.Find("TMP_ReTry_Content_Information_R").gameObject.GetComponent<TextMeshProUGUI>();
        m_gPanel_ReTry_Content_Information_D = m_gPanel_ReTry_Content_Information.transform.Find("Panel_ReTry_Content_Information_D").gameObject;
        m_TMP_ReTry_Content_Information_D = m_gPanel_ReTry_Content_Information_D.transform.Find("TMP_ReTry_Content_Information_D").gameObject.GetComponent<TextMeshProUGUI>();

        m_gPanel_ReTry_Content_ReTry = m_gPanel_ReTry_Content.transform.Find("Panel_ReTry_Content_ReTry").gameObject;
        m_gBTN_ReTry_Content_ReTry = m_gPanel_ReTry_Content_ReTry.transform.Find("BTN_ReTry_Content_ReTry").gameObject;
        m_BTN_ReTry_Content_ReTry = m_gBTN_ReTry_Content_ReTry.GetComponent<Button>();
        m_TMP_ReTry_Content_ReTry = m_gBTN_ReTry_Content_ReTry.transform.Find("TMP_ReTry_Content_ReTry").gameObject.GetComponent<TextMeshProUGUI>();
    }
    void InitialSet_Button()
    {

    }
    void InitialSet_Toggle()
    {
        m_TG_ReTry_Content_CheckBox_ReTry1.onValueChanged.RemoveAllListeners();
        m_TG_ReTry_Content_CheckBox_ReTry1.onValueChanged.AddListener(delegate { Update_Information(1); });
        m_TG_ReTry_Content_CheckBox_ReTry2.onValueChanged.RemoveAllListeners();
        m_TG_ReTry_Content_CheckBox_ReTry2.onValueChanged.AddListener(delegate { Update_Information(2); });
        m_TG_ReTry_Content_CheckBox_ReTry3.onValueChanged.RemoveAllListeners();
        m_TG_ReTry_Content_CheckBox_ReTry3.onValueChanged.AddListener(delegate { Update_Information(3); });
        m_TG_ReTry_Content_CheckBox_ReTry4.onValueChanged.RemoveAllListeners();
        m_TG_ReTry_Content_CheckBox_ReTry4.onValueChanged.AddListener(delegate { Update_Information(4); });
    }

    // 리트라이 UI 표시.
    public void Display_GUI_ReTry(string deathname)
    {
        StartCoroutine(Process_Display_GUI_ReTry(deathname));
    }

    IEnumerator Process_Display_GUI_ReTry(string deathname)
    {
        yield return new WaitForSeconds(5f);

        m_gPanel_ReTry.SetActive(true);
        m_gPanel_ReTry.transform.SetAsLastSibling();

        m_TMP_ReTry_Content_Description.text = "'" + deathname + "' 에 의한 치명적인 일격으로 쓰러졌습니다!";

        Update_Information(0);
    }

    // 리트라이 단계별 설명 업데이트.
    void Update_Information(int retrynumber)
    {
        Check_TG(retrynumber);
        Check_Btn();

        switch (retrynumber)
        {
            case 0:
                {
                    m_TMP_ReTry_Content_Information_L.text = "리트라이 단계를 선택해 주세요.";
                    m_TMP_ReTry_Content_Information_R.text = "리트라이 단계별 잃어버리는 아이템의 양과 골드가 달라집니다.";
                    m_TMP_ReTry_Content_Information_D.text = "리트라이 단계별로 필요한 골드량이 달라집니다.";
                    m_TMP_ReTry_Content_ReTry.text = "리트라이 단계를 선택해 주세요.";

                    Update_Btn(0);
                } break;
            case 1:
                {
                    m_TMP_ReTry_Content_Information_L.text = "당신의 의식이 희미해져갑니다.\n누군가 당신의 골드와 아이템을 가져가도 할 수 있는 게 없습니다.";
                    m_TMP_ReTry_Content_Information_R.text = "골드: 1% ~ 10%\n장비 아이템: 1 / 5 개\n소비 아이템: 1 / 3 개\n기타 아이템: 1 / 3 개";
                    m_TMP_ReTry_Content_Information_D.text = Return_ReTry_Gold(1).ToString() + " 골드";
                    m_TMP_ReTry_Content_ReTry.text = "1단계 리트라이";

                    Update_Btn(1);
                } break;
            case 2:
                {
                    m_TMP_ReTry_Content_Information_L.text = "당신은 희미해져 가는 의식 속에서 약간의 골드를 꺼내 곁에 두었습니다.\n당신의 골드와 아이템이 많이 사라지지 않기를 기도할 뿐입니다.";
                    m_TMP_ReTry_Content_Information_R.text = "골드: 1% ~ 5%\n장비 아이템: 1 / 10 개\n소비 아이템: 1 / 5 개\n기타 아이템: 1 / 5 개";
                    m_TMP_ReTry_Content_Information_D.text = Return_ReTry_Gold(2).ToString() + " 골드";
                    m_TMP_ReTry_Content_ReTry.text = "2단계 리트라이";

                    Update_Btn(2);
                } break;
            case 3:
                {
                    m_TMP_ReTry_Content_Information_L.text = "당신은 희미해져 가는 의식 속에서 골드를 적당히 꺼내 곁에 두었습니다.\n아마도 당신의 골드와 아이템은 매우 조금만 사라질 것입니다.";
                    m_TMP_ReTry_Content_Information_R.text = "골드: 1%\n장비 아이템: 0 개\n소비 아이템: 1 / 10 개\n기타 아이템: 1 / 10 개";
                    m_TMP_ReTry_Content_Information_D.text = Return_ReTry_Gold(3).ToString() + " 골드";
                    m_TMP_ReTry_Content_ReTry.text = "3단계 리트라이";

                    Update_Btn(3);
                } break;
            case 4:
                {
                    m_TMP_ReTry_Content_Information_L.text = "당신은 희미해져 가는 의식 속에서 많은 양의 골드를 꺼내 곁에 두었습니다.\n누군가 당신의 골드와 아이템 대신 당신이 꺼내둔 골드만을 가져갈 것입니다.";
                    m_TMP_ReTry_Content_Information_R.text = "골드: 0%\n장비 아이템: 0\n소비 아이템: 0 개\n기타 아이템: 0 개";
                    m_TMP_ReTry_Content_Information_D.text = Return_ReTry_Gold(4).ToString() + " 골드";
                    m_TMP_ReTry_Content_ReTry.text = "4단계 리트라이";

                    Update_Btn(4);
                } break;
        }
    }
    // 리트라이 단계별, 플레이어 레벨별 지불할 골드.
    int Return_ReTry_Gold(int multiple)
    {
        multiple -= 1;

        int returngold, playerlv;

        playerlv = Player_Total.Instance.m_ps_Status.m_sStatus.GetSTATUS_LV();

        if (playerlv < 10)
            returngold = playerlv * 5 * multiple;
        else if (playerlv < 20)
            returngold = playerlv * 10 * multiple;
        else if (playerlv < 30)
            returngold = playerlv * 50 * multiple;
        else if (playerlv < 40)
            returngold = playerlv * 250 * multiple;
        else if (playerlv < 50)
            returngold = playerlv * 1000 * multiple;
        else
            returngold = 0;

        m_nReTryGold = returngold;

        return returngold;
    }
    // 리트라이 단계 중복 체크 불가.
    void Check_TG(int tgnumber)
    {
        if (tgnumber == 0)
        {
            m_TG_ReTry_Content_CheckBox_ReTry1.SetIsOnWithoutNotify(false);
            m_TG_ReTry_Content_CheckBox_ReTry2.SetIsOnWithoutNotify(false);
            m_TG_ReTry_Content_CheckBox_ReTry3.SetIsOnWithoutNotify(false);
            m_TG_ReTry_Content_CheckBox_ReTry4.SetIsOnWithoutNotify(false);
        }
        if (tgnumber == 1)
        {
            m_TG_ReTry_Content_CheckBox_ReTry2.SetIsOnWithoutNotify(false);
            m_TG_ReTry_Content_CheckBox_ReTry3.SetIsOnWithoutNotify(false);
            m_TG_ReTry_Content_CheckBox_ReTry4.SetIsOnWithoutNotify(false);
        }
        if (tgnumber == 2)
        {
            m_TG_ReTry_Content_CheckBox_ReTry1.SetIsOnWithoutNotify(false);
            m_TG_ReTry_Content_CheckBox_ReTry3.SetIsOnWithoutNotify(false);
            m_TG_ReTry_Content_CheckBox_ReTry4.SetIsOnWithoutNotify(false);
        }
        if (tgnumber == 3)
        {
            m_TG_ReTry_Content_CheckBox_ReTry1.SetIsOnWithoutNotify(false);
            m_TG_ReTry_Content_CheckBox_ReTry2.SetIsOnWithoutNotify(false);
            m_TG_ReTry_Content_CheckBox_ReTry4.SetIsOnWithoutNotify(false);
        }
        if (tgnumber == 4)
        {
            m_TG_ReTry_Content_CheckBox_ReTry1.SetIsOnWithoutNotify(false);
            m_TG_ReTry_Content_CheckBox_ReTry2.SetIsOnWithoutNotify(false);
            m_TG_ReTry_Content_CheckBox_ReTry3.SetIsOnWithoutNotify(false);
        }
    }
    // 리트라이 단계별 필요 골드 체크. 단계별 필요 골드를 소유중이지 않을 시 해당 리트라이 단계 비활성화.
    void Check_Btn()
    {
        if (Return_ReTry_Gold(2) <= Player_Total.Instance.m_pi_Itemslot.m_nGold)
        {
            m_gPanel_ReTry_Content_CheckBox_ReTry2.SetActive(true);
            m_gPanel_ReTry2_Rock.SetActive(false);
        }
        else
        {
            m_gPanel_ReTry_Content_CheckBox_ReTry2.SetActive(false);
            m_gPanel_ReTry2_Rock.SetActive(true);
        }
        if (Return_ReTry_Gold(3) <= Player_Total.Instance.m_pi_Itemslot.m_nGold)
        {
            m_gPanel_ReTry_Content_CheckBox_ReTry3.SetActive(true);
            m_gPanel_ReTry3_Rock.SetActive(false);
        }
        else
        {
            m_gPanel_ReTry_Content_CheckBox_ReTry3.SetActive(false);
            m_gPanel_ReTry3_Rock.SetActive(true);
        }
        if (Return_ReTry_Gold(4) <= Player_Total.Instance.m_pi_Itemslot.m_nGold)
        {
            m_gPanel_ReTry_Content_CheckBox_ReTry4.SetActive(true);
            m_gPanel_ReTry4_Rock.SetActive(false);
        }
        else
        {
            m_gPanel_ReTry_Content_CheckBox_ReTry4.SetActive(false);
            m_gPanel_ReTry4_Rock.SetActive(true);
        }
    }

    void Update_Btn(int retrynumber)
    {
        m_BTN_ReTry_Content_ReTry.onClick.RemoveAllListeners();
        m_BTN_ReTry_Content_ReTry.onClick.AddListener(delegate { Btn_Press_ReTry(retrynumber); });
    }
    void Btn_Press_ReTry(int retrynumber)
    {
        switch (retrynumber)
        {
            case 0:
                {
                    
                } break;
            case 1:
                {
                    Player_Total.Instance.ReTry(1, m_nReTryGold);
                    m_gPanel_ReTry.SetActive(false);
                } break;
            case 2:
                {
                    Player_Total.Instance.ReTry(2, m_nReTryGold);
                    m_gPanel_ReTry.SetActive(false);
                }
                break;
            case 3:
                {
                    Player_Total.Instance.ReTry(3, m_nReTryGold);
                    m_gPanel_ReTry.SetActive(false);
                }
                break;
            case 4:
                {
                    Player_Total.Instance.ReTry(4, m_nReTryGold);
                    m_gPanel_ReTry.SetActive(false);
                }
                break;
        }

        GUIManager_Total.Instance.UnDisplay_GUI_BossInformation();
        BossManager.Instance.m_eBattle_Boss_State = E_BATTLE_BOSS_STATE.NULL;
    }
}
