using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public enum E_QUICKSLOT_CATEGORY { EQUIP, USE, ETC, NULL }

public class Quickslot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject m_gPanel_Quickslot_Key;
    [SerializeField] GameObject m_gTMP_Quickslot_Key;
    [SerializeField] TextMeshProUGUI m_TMP_Quickslot_Key;
    [Space(20)]
    [SerializeField] GameObject m_gPanel_Quickslot_ItemSprite;
    [SerializeField] Image m_IMG_Quickslot_ItemSprite;
    [SerializeField] GameObject m_gTMP_Quickslot_ItemCount;
    [SerializeField] TextMeshProUGUI m_TMP_Quickslot_ItemCount;
    [SerializeField] Image m_IMG_RedMask;
    [SerializeField] Image m_IMG_CoolMask;
    [SerializeField] TextMeshProUGUI m_TMP_CoolTime;

    public int m_nQuickslot_Number;
    public E_QUICKSLOT_CATEGORY m_eQuickslot_Category;
    // 착용할 장비 아이템의 아이템 슬롯 배열 번호.
    public int m_nItem_Equip_AryNumber;
    // 사용할 소비 아이템의 코드 번호.
    public int m_nItem_Use_Code;
    // 확인할 기타 아이템의 코드 번호.
    public int m_nItem_Etc_Code;

    // 퀵슬롯 자체 쿨타임. 1초 보다 조금 김.
    public bool m_bCool;

    private void Awake()
    {
        InitialSet();
    }

    void InitialSet()
    {
        InitialSet_Object();
    }
    void InitialSet_Object()
    {
        m_gPanel_Quickslot_Key = this.gameObject.transform.Find("Panel_Quickslot_Key").gameObject;
        m_gTMP_Quickslot_Key = m_gPanel_Quickslot_Key.transform.Find("TMP_Quickslot_Key").gameObject;
        m_TMP_Quickslot_Key = m_gTMP_Quickslot_Key.GetComponent<TextMeshProUGUI>();

        m_gPanel_Quickslot_ItemSprite = this.gameObject.transform.Find("Panel_Quickslot_ItemSprite").gameObject;
        m_IMG_Quickslot_ItemSprite = m_gPanel_Quickslot_ItemSprite.GetComponent<Image>();
        m_gTMP_Quickslot_ItemCount = this.gameObject.transform.Find("TMP_Quickslot_ItemCount").gameObject;
        m_TMP_Quickslot_ItemCount = m_gTMP_Quickslot_ItemCount.GetComponent<TextMeshProUGUI>();
        m_IMG_RedMask = this.gameObject.transform.Find("Panel_RedMask").gameObject.GetComponent<Image>();
        m_IMG_CoolMask = this.gameObject.transform.Find("Panel_CoolMask").gameObject.GetComponent<Image>();
        m_TMP_CoolTime = this.gameObject.transform.Find("TMP_CoolTime").gameObject.GetComponent<TextMeshProUGUI>();

        m_nQuickslot_Number = this.transform.GetSiblingIndex();

        m_nItem_Equip_AryNumber = -1;
        m_nItem_Use_Code = -1;
        m_nItem_Etc_Code = -1;
        m_eQuickslot_Category = E_QUICKSLOT_CATEGORY.NULL;

        m_bCool = false;
    }

    // 퀵슬롯에 등록된 아이템 사용.
    public void Use_Quickslot()
    {
        if (Player_Total.Instance.m_pm_Move.m_ePlayerMoveState != Player_Move.E_PLAYER_MOVE_STATE.DEATH)
        {
            if (m_bCool == false)
            {
                if (m_eQuickslot_Category == E_QUICKSLOT_CATEGORY.EQUIP)
                {
                    Player_Total.Instance.Use_Quickslot_Item_Equip(E_QUICKSLOT_CATEGORY.EQUIP, m_nItem_Equip_AryNumber, m_nQuickslot_Number);

                    Player_Total.Instance.m_ps_Status.CheckLogic();

                    //m_TMP_Quickslot_ItemCount.text = "1";
                }
                else if (m_eQuickslot_Category == E_QUICKSLOT_CATEGORY.USE)
                {
                    // 강화서, 기프트(전리품상자) 는 퀵슬롯에서 사용 불가.
                    if (m_nItem_Use_Code / 1000 != 11 && m_nItem_Use_Code / 1000 != 12)
                    {
                        if (Player_Total.Instance.Use_Quickslot_Item_Use(E_QUICKSLOT_CATEGORY.USE, m_nItem_Use_Code) == true)
                        {

                            int count = int.Parse(m_TMP_Quickslot_ItemCount.text);
                            if (count > 0)
                                m_TMP_Quickslot_ItemCount.text = (count - 1).ToString();

                            if (m_TMP_Quickslot_ItemCount.text == "0")
                                Set_Quickslot_Blank();
                        }
                    }
                }
                if (m_eQuickslot_Category == E_QUICKSLOT_CATEGORY.ETC)
                {
                    if (m_TMP_Quickslot_ItemCount.text == "0")
                        Set_Quickslot_Blank();
                }

                GUIManager_Total.Instance.Update_SS();
                GUIManager_Total.Instance.Update_Itemslot();
                GUIManager_Total.Instance.Update_Equipslot();
            }
            else
                GUIManager_Total.Instance.UpdateLog("퀵슬롯 쿨타임 적용중.");
        }
    }

    // 퀵슬롯에 아이템 등록.
    // number 매개변수는 퀵슬롯에 등록되는 아이템의 분류에따라 각각 [장비 아이템: 아이템창의 해당 아이템의 배열 넘버][소비 아이템: 아이템코드] 를 의미.
    public bool Set_Quickslot(E_QUICKSLOT_CATEGORY eqc, int number)
    {
        if (m_bCool == false)
        {
            m_eQuickslot_Category = eqc;
            //Debug.Log(eqc + " / " + number);
            switch (m_eQuickslot_Category)
            {
                case E_QUICKSLOT_CATEGORY.EQUIP:
                    {
                        if (Player_Itemslot.m_nary_Itemslot_Equip_Count[number] != 0)
                        {
                            m_nItem_Equip_AryNumber = number;
                            Set_Quickslot_Item(Player_Itemslot.m_gary_Itemslot_Equip[number]);
                            Player_Itemslot.m_bary_Itemslot_Equip_Belong[number] = true;
                        }
                        else
                            Set_Quickslot_Null();
                    }
                    break;
                case E_QUICKSLOT_CATEGORY.USE:
                    {
                        m_nItem_Use_Code = number;
                        Set_Quickslot_Item(ItemManager.instance.m_Dictionary_MonsterDrop_Use[m_nItem_Use_Code], Player_Total.Instance.Return_Quickslot_Item_Count(eqc, number));
                    }
                    break;
                case E_QUICKSLOT_CATEGORY.ETC:
                    {
                        m_nItem_Etc_Code = number;
                        Set_Quickslot_Item(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[m_nItem_Etc_Code], Player_Total.Instance.Return_Quickslot_Item_Count(eqc, number));
                    }
                    break;
            }
            return true;
        }
        return false;
    }
    // 퀵슬롯에 아이템 표시.
    public void Set_Quickslot_Item(Item_Equip item)
    {
        m_IMG_Quickslot_ItemSprite.color = new Color(1, 1, 1, 1);
        m_IMG_Quickslot_ItemSprite.sprite = item.m_sp_Sprite;
        m_TMP_Quickslot_ItemCount.text = "1";
        m_IMG_RedMask.color = new Color(1, 0, 0, 0);

        m_nItem_Use_Code = -1;
        m_nItem_Etc_Code = -1;

        if (m_cProcess_Quickslot_CoolTime != null)
            StopCoroutine(m_cProcess_Quickslot_CoolTime);
        m_cProcess_Quickslot_CoolTime = StartCoroutine(Process_Quickslot_CoolTime());
    }
    public void Set_Quickslot_Item(Item_Use item, int count)
    {
        m_IMG_Quickslot_ItemSprite.color = new Color(1, 1, 1, 1);
        m_IMG_Quickslot_ItemSprite.sprite = item.m_sp_Sprite;
        m_TMP_Quickslot_ItemCount.text = count.ToString();
        m_IMG_RedMask.color = new Color(1, 0, 0, 0);

        m_nItem_Equip_AryNumber = -1;
        m_nItem_Etc_Code = -1;

        if (count == 0)
            Set_Quickslot_Blank();

        if (m_cProcess_Quickslot_CoolTime != null)
            StopCoroutine(m_cProcess_Quickslot_CoolTime);
        m_cProcess_Quickslot_CoolTime = StartCoroutine(Process_Quickslot_CoolTime());
    }
    public void Set_Quickslot_Item(Item_Etc item, int count)
    {
        m_IMG_Quickslot_ItemSprite.color = new Color(1, 1, 1, 1);
        m_IMG_Quickslot_ItemSprite.sprite = item.m_sp_Sprite;
        m_TMP_Quickslot_ItemCount.text = count.ToString();
        m_IMG_RedMask.color = new Color(1, 0, 0, 0);

        m_nItem_Equip_AryNumber = -1;
        m_nItem_Use_Code = -1;

        if (count == 0)
            Set_Quickslot_Blank();

        if (m_cProcess_Quickslot_CoolTime != null)
            StopCoroutine(m_cProcess_Quickslot_CoolTime);
        m_cProcess_Quickslot_CoolTime = StartCoroutine(Process_Quickslot_CoolTime());
    }
    public void Set_Quickslot_Null()
    {
        m_IMG_Quickslot_ItemSprite.color = new Color(0, 0, 0, 0);
        m_IMG_Quickslot_ItemSprite.sprite = null;
        m_TMP_Quickslot_ItemCount.text = "0";
        m_nItem_Equip_AryNumber = -1;
        m_nItem_Use_Code = -1;
        m_nItem_Etc_Code = -1;
        m_eQuickslot_Category = E_QUICKSLOT_CATEGORY.NULL;

        Set_Quickslot_Cool_Exit();

        GUIManager_Total.Instance.m_GUI_Quickslot_Signdown.m_gPanel_Quickslot_Signdown.SetActive(false);
    }
    public void Set_Quickslot_Blank()
    {
        m_TMP_Quickslot_ItemCount.text = "0";
        m_IMG_RedMask.color = new Color(1, 0, 0, 0.33f);
    }

    Coroutine m_cProcess_Quickslot_CoolTime;
    IEnumerator Process_Quickslot_CoolTime()
    {
        Set_Quickslot_Cool();
        m_IMG_CoolMask.fillAmount = 1;
        m_bCool = true;

        float ftime = 1;

        while (ftime > 0)
        {
            ftime -= 0.01f;
            if (Player_Status.m_Dictionary_Coroutine_Item_Use_CoolTime.ContainsKey(m_nItem_Use_Code) == false)
                m_IMG_CoolMask.fillAmount = ftime;
            yield return new WaitForSeconds(0.01f);
        }
        if (m_cProcess_Quickslot_CoolTime != null)
            m_cProcess_Quickslot_CoolTime = null;
        m_bCool = false;
    }

    // 퀵슬롯 쿨타임 설정.
    // 장비 아이템의 경우 장비 교체 쿨타임은 1초.
    // 소비 아이템의 경우 해당 소비 아이템의 사용 쿨타임 설정.
    public void Set_Quickslot_Cool()
    {
        m_IMG_CoolMask.color = new Color(m_IMG_CoolMask.color.r, m_IMG_CoolMask.color.g, m_IMG_CoolMask.color.b, 0.75f);
    }
    public void Set_Quickslot_Cool_Exit()
    {
        m_IMG_CoolMask.color = new Color(m_IMG_CoolMask.color.r, m_IMG_CoolMask.color.g, m_IMG_CoolMask.color.b, 0);
        m_IMG_CoolMask.fillAmount = 0;
        m_TMP_CoolTime.text = "";
    }

    public void Set_Item_Count(int count)
    {
        m_TMP_Quickslot_ItemCount.text = count.ToString();
        if (count < 1)
        {
            Set_Quickslot_Blank();
        }
        else
            m_IMG_RedMask.color = new Color(1, 0, 0, 0);
    }

    // 퀵슬롯 쿨타임.
    float m_fCoolTime_Ratio;
    public void Update_Quickslot_CoolTime()
    {
        if (m_eQuickslot_Category == E_QUICKSLOT_CATEGORY.USE)
        {
            if (m_nItem_Use_Code != -1)
            {
                if (Player_Status.m_Dictionary_Coroutine_Item_Use_CoolTime.ContainsKey(m_nItem_Use_Code) == true)
                {
                    if (Player_Status.m_Dictionary_Item_Use_CoolTime_RemainingTime[m_nItem_Use_Code] > 0)
                    {
                        Set_Quickslot_Cool();

                        m_fCoolTime_Ratio = Player_Status.m_Dictionary_Item_Use_CoolTime_RemainingTime[m_nItem_Use_Code] / ItemManager.instance.m_Dictionary_MonsterDrop_Use[m_nItem_Use_Code].m_fCoolTime;
                        m_IMG_CoolMask.fillAmount = m_fCoolTime_Ratio;
                        m_TMP_CoolTime.text = Mathf.Floor(Player_Status.m_Dictionary_Item_Use_CoolTime_RemainingTime[m_nItem_Use_Code]).ToString();
                    }
                    //GUIManager_Total.Instance.UpdateLog("[" + ItemManager.instance.m_Dictionary_MonsterDrop_Use[m_nItem_Use_Code].m_sItemName + "] 쿨타임: " + Player_Status.m_Dictionary_Item_Use_CoolTime_RemainingTime[m_nItem_Use_Code].ToString());

                }
                else
                {
                    m_IMG_CoolMask.fillAmount = 0;
                    m_TMP_CoolTime.text = "";
                }
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //Debug.Log("퀵슬롯 좌클릭");
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            //Debug.Log("퀵슬롯 우클릭");
            if (GUIManager_Total.Instance != null && GUIManager_Total.Instance.m_GUI_Quickslot_Signdown != null)
            {
                if (m_eQuickslot_Category != E_QUICKSLOT_CATEGORY.NULL)
                {
                    GUIManager_Total.Instance.Display_GUI_Quickslot_Signdown(m_nQuickslot_Number);

                    float m_fPanelCoordination_x;
                    float m_fPanelCoordination_y;

                    m_fPanelCoordination_x = Mathf.Round(this.transform.position.x);
                    m_fPanelCoordination_y = Mathf.Round(this.transform.position.y);
                    GUIManager_Total.Instance.m_GUI_Quickslot_Signdown.m_gPanel_Quickslot_Signdown.transform.position = new Vector2(m_fPanelCoordination_x, m_fPanelCoordination_y);
                }
            }
        }
    }
}
