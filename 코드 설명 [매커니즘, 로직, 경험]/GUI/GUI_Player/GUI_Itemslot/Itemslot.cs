using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Itemslot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // Itemslot 에서 아이템 확인 UI.
    [SerializeField] Image m_IMG_ItemSprite;
    [SerializeField] Image m_IMG_BackgroundSprite;

    [SerializeField] TextMeshProUGUI m_TMP_ItemCount;

    // Itemslot_Equip_Information 에서 확인 할 수 있는 장비아이템 정보 UI.
    [SerializeField] GameObject m_gPanel_Itemslot_Equip_Information;

    // Itemslot_Use_Information 에서 확인 할 수 있는 소비아이템 정보 UI.
    [SerializeField] GameObject m_gPanel_Itemslot_Use_Information;

    // Itemslot_Etc_Information 에서 확인 할 수 있는 기타아이템 정보 UI.
    [SerializeField] GameObject m_gPanel_Itemslot_Etc_Information;

    // Quickslot 등록 UI.
    [SerializeField] GameObject m_gPanel_Quickslot_Signup;

    float m_fPanelCoordination_x;
    float m_fPanelCoordination_y;

    public int m_nAryNumber;

    public void Awake()
    {
        InitialSet_Object();
    }

    // 초기 Object 불러오기.
    void InitialSet_Object()
    {
        m_gPanel_Itemslot_Equip_Information = GameObject.Find("Canvas_GUI").transform.Find("Panel_Itemslot_Equip_Information").gameObject;
        m_gPanel_Itemslot_Use_Information = GameObject.Find("Canvas_GUI").transform.Find("Panel_Itemslot_Use_Information").gameObject;
        m_gPanel_Itemslot_Etc_Information = GameObject.Find("Canvas_GUI").transform.Find("Panel_Itemslot_Etc_Information").gameObject;
        m_gPanel_Quickslot_Signup = GameObject.Find("Canvas_GUI").transform.Find("Panel_Quickslot_Signup").gameObject;

        m_IMG_ItemSprite = this.gameObject.transform.Find("Panel_ItemSprite").GetComponent<Image>();
        m_IMG_BackgroundSprite = this.gameObject.transform.Find("Panel_BackgroundSprite").GetComponent<Image>();
        m_TMP_ItemCount = this.gameObject.transform.Find("Text_Count").GetComponent<TextMeshProUGUI>();
    }

    public void SetItem_Equip(Item_Equip item, int count)
    {
        //item.ItemInformation();

        m_IMG_ItemSprite.sprite = item.m_sp_Sprite;
        m_IMG_ItemSprite.color = new Color(1, 1, 1, 1);
        m_TMP_ItemCount.text = count.ToString();
    }
    public void SetItem_Use(Item_Use item, int count)
    {
        //item.ItemInformation();

        m_IMG_ItemSprite.sprite = item.m_sp_Sprite;
        m_IMG_ItemSprite.color = new Color(1, 1, 1, 1);
        m_TMP_ItemCount.text = count.ToString();
    }
    public void SetItem_Etc(Item_Etc item, int count)
    {
        //item.ItemInformation();

        m_IMG_ItemSprite.sprite = item.m_sp_Sprite;
        m_IMG_ItemSprite.color = new Color(1, 1, 1, 1);
        m_TMP_ItemCount.text = count.ToString();
    }
    public void SetNull()
    {
        m_IMG_ItemSprite.color = new Color(1, 1, 1, 0);
        m_IMG_ItemSprite.sprite = null;
        m_TMP_ItemCount.text = null;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {

    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        //m_gPanel_Itemslot_Equip_Information.SetActive(false);
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (GUIManager_Total.Instance != null && m_gPanel_Itemslot_Equip_Information != null)
            {
                if (GUIManager_Total.Instance.m_GUI_Itemslot.m_eItemslot == E_ITEMSLOT.EQUIP)
                {
                    if (Player_Itemslot.m_nary_Itemslot_Equip_Count[m_nAryNumber] != 0)
                    {
                        GUIManager_Total.Instance.m_GUI_Itemslot_Equip_Information.m_bEquip_Condition_Check = true;
                        GUIManager_Total.Instance.Update_Itemslot_Equip_Information(Player_Itemslot.m_gary_Itemslot_Equip[m_nAryNumber], m_nAryNumber);

                        m_gPanel_Itemslot_Equip_Information.transform.SetAsLastSibling();
                        m_gPanel_Itemslot_Equip_Information.SetActive(true);
                        //m_gPanel_Itemslot_Equip_Information.transform.position = this.gameObject.transform.position;
                        m_fPanelCoordination_x = Mathf.Round(this.gameObject.transform.position.x);
                        m_fPanelCoordination_y = Mathf.Round(this.gameObject.transform.position.y);
                        m_gPanel_Itemslot_Equip_Information.transform.position = new Vector2(m_fPanelCoordination_x, m_fPanelCoordination_y);

                        if (m_gPanel_Quickslot_Signup.activeSelf == true)
                            m_gPanel_Quickslot_Signup.SetActive(false);
                    }
                    else
                    {
                        m_gPanel_Itemslot_Equip_Information.SetActive(false);
                    }
                }
                else if (GUIManager_Total.Instance.m_GUI_Itemslot.m_eItemslot == E_ITEMSLOT.USE)
                {
                    if (Player_Itemslot.m_nary_Itemslot_Use_Count[m_nAryNumber] != 0)
                    {
                        GUIManager_Total.Instance.m_GUI_Itemslot_Use_Information.m_bUse_Condition_Check = true;
                        GUIManager_Total.Instance.Update_Itemslot_Use_Information(Player_Itemslot.m_gary_Itemslot_Use[m_nAryNumber], m_nAryNumber);

                        m_gPanel_Itemslot_Use_Information.transform.SetAsLastSibling();
                        m_gPanel_Itemslot_Use_Information.SetActive(true);
                        //m_gPanel_Itemslot_Equip_Information.transform.position = this.gameObject.transform.position;
                        m_fPanelCoordination_x = Mathf.Round(this.gameObject.transform.position.x);
                        m_fPanelCoordination_y = Mathf.Round(this.gameObject.transform.position.y);
                        m_gPanel_Itemslot_Use_Information.transform.position = new Vector2(m_fPanelCoordination_x, m_fPanelCoordination_y);

                        if (m_gPanel_Quickslot_Signup.activeSelf == true)
                            m_gPanel_Quickslot_Signup.SetActive(false);
                    }
                    else
                    {
                        m_gPanel_Itemslot_Use_Information.SetActive(false);
                    }
                }
                else if (GUIManager_Total.Instance.m_GUI_Itemslot.m_eItemslot == E_ITEMSLOT.ETC)
                {
                    if (Player_Itemslot.m_nary_Itemslot_Etc_Count[m_nAryNumber] != 0)
                    {
                        GUIManager_Total.Instance.Update_Itemslot_Etc_Information(Player_Itemslot.m_gary_Itemslot_Etc[m_nAryNumber], m_nAryNumber);

                        m_gPanel_Itemslot_Etc_Information.transform.SetAsLastSibling();
                        m_gPanel_Itemslot_Etc_Information.SetActive(true);
                        //m_gPanel_Itemslot_Equip_Information.transform.position = this.gameObject.transform.position;
                        m_fPanelCoordination_x = Mathf.Round(this.gameObject.transform.position.x);
                        m_fPanelCoordination_y = Mathf.Round(this.gameObject.transform.position.y);
                        m_gPanel_Itemslot_Etc_Information.transform.position = new Vector2(m_fPanelCoordination_x, m_fPanelCoordination_y);

                        if (m_gPanel_Quickslot_Signup.activeSelf == true)
                            m_gPanel_Quickslot_Signup.SetActive(false);
                    }
                    else
                    {
                        m_gPanel_Itemslot_Etc_Information.SetActive(false);
                    }
                }
            }
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (GUIManager_Total.Instance.m_GUI_Itemslot.m_eItemslot == E_ITEMSLOT.EQUIP)
                if (Player_Itemslot.m_nary_Itemslot_Equip_Count[m_nAryNumber] != 0)
                    if (GUIManager_Total.Instance != null && GUIManager_Total.Instance.m_GUI_Quickslot_Signup != null)
                    {
                        if (GUIManager_Total.Instance.m_GUI_Itemslot.m_eItemslot == E_ITEMSLOT.EQUIP)
                        {
                            if (Player_Itemslot.m_nary_Itemslot_Equip_Count[m_nAryNumber] != 0)
                            {
                                GUIManager_Total.Instance.Display_GUI_Quickslot_Signup(E_QUICKSLOT_CATEGORY.EQUIP, m_nAryNumber);

                                if (m_gPanel_Itemslot_Equip_Information.activeSelf == true)
                                    m_gPanel_Itemslot_Equip_Information.SetActive(false);
                                if (m_gPanel_Itemslot_Use_Information.activeSelf == true)
                                    m_gPanel_Itemslot_Use_Information.SetActive(false);
                                if (m_gPanel_Itemslot_Etc_Information.activeSelf == true)
                                    m_gPanel_Itemslot_Etc_Information.SetActive(false);
                            }
                            else
                            {
                                m_gPanel_Quickslot_Signup.SetActive(false);
                            }
                        }
                        else if (GUIManager_Total.Instance.m_GUI_Itemslot.m_eItemslot == E_ITEMSLOT.USE)
                        {
                            if (Player_Itemslot.m_nary_Itemslot_Use_Count[m_nAryNumber] != 0)
                            {
                                GUIManager_Total.Instance.Display_GUI_Quickslot_Signup(E_QUICKSLOT_CATEGORY.USE, Player_Itemslot.m_gary_Itemslot_Use[m_nAryNumber].m_nItemCode);

                                if (m_gPanel_Itemslot_Equip_Information.activeSelf == true)
                                    m_gPanel_Itemslot_Equip_Information.SetActive(false);
                                if (m_gPanel_Itemslot_Use_Information.activeSelf == true)
                                    m_gPanel_Itemslot_Use_Information.SetActive(false);
                                if (m_gPanel_Itemslot_Etc_Information.activeSelf == true)
                                    m_gPanel_Itemslot_Etc_Information.SetActive(false);
                            }
                            else
                            {
                                m_gPanel_Quickslot_Signup.SetActive(false);
                            }
                        }
                        else if (GUIManager_Total.Instance.m_GUI_Itemslot.m_eItemslot == E_ITEMSLOT.ETC)
                        {
                            if (Player_Itemslot.m_nary_Itemslot_Etc_Count[m_nAryNumber] != 0)
                            {
                                GUIManager_Total.Instance.Display_GUI_Quickslot_Signup(E_QUICKSLOT_CATEGORY.ETC, Player_Itemslot.m_gary_Itemslot_Etc[m_nAryNumber].m_nItemCode);

                                if (m_gPanel_Itemslot_Equip_Information.activeSelf == true)
                                    m_gPanel_Itemslot_Equip_Information.SetActive(false);
                                if (m_gPanel_Itemslot_Use_Information.activeSelf == true)
                                    m_gPanel_Itemslot_Use_Information.SetActive(false);
                                if (m_gPanel_Itemslot_Etc_Information.activeSelf == true)
                                    m_gPanel_Itemslot_Etc_Information.SetActive(false);
                            }
                            else
                            {
                                m_gPanel_Quickslot_Signup.SetActive(false);
                            }
                        }

                        m_gPanel_Quickslot_Signup.gameObject.transform.SetAsLastSibling();
                        m_gPanel_Quickslot_Signup.SetActive(true);

                        m_fPanelCoordination_x = Mathf.Round(this.transform.position.x);
                        m_fPanelCoordination_y = Mathf.Round(this.transform.position.y);
                        m_gPanel_Quickslot_Signup.transform.position = new Vector2(m_fPanelCoordination_x, m_fPanelCoordination_y);
                    }
            if (GUIManager_Total.Instance.m_GUI_Itemslot.m_eItemslot == E_ITEMSLOT.USE)
                if (Player_Itemslot.m_nary_Itemslot_Use_Count[m_nAryNumber] != 0)
                    if (GUIManager_Total.Instance != null && GUIManager_Total.Instance.m_GUI_Quickslot_Signup != null)
                    {
                        if (GUIManager_Total.Instance.m_GUI_Itemslot.m_eItemslot == E_ITEMSLOT.EQUIP)
                        {
                            if (Player_Itemslot.m_nary_Itemslot_Equip_Count[m_nAryNumber] != 0)
                            {
                                GUIManager_Total.Instance.Display_GUI_Quickslot_Signup(E_QUICKSLOT_CATEGORY.EQUIP, m_nAryNumber);

                                if (m_gPanel_Itemslot_Equip_Information.activeSelf == true)
                                    m_gPanel_Itemslot_Equip_Information.SetActive(false);
                                if (m_gPanel_Itemslot_Use_Information.activeSelf == true)
                                    m_gPanel_Itemslot_Use_Information.SetActive(false);
                                if (m_gPanel_Itemslot_Etc_Information.activeSelf == true)
                                    m_gPanel_Itemslot_Etc_Information.SetActive(false);
                            }
                            else
                            {
                                m_gPanel_Quickslot_Signup.SetActive(false);
                            }
                        }
                        else if (GUIManager_Total.Instance.m_GUI_Itemslot.m_eItemslot == E_ITEMSLOT.USE)
                        {
                            if (Player_Itemslot.m_nary_Itemslot_Use_Count[m_nAryNumber] != 0)
                            {
                                GUIManager_Total.Instance.Display_GUI_Quickslot_Signup(E_QUICKSLOT_CATEGORY.USE, Player_Itemslot.m_gary_Itemslot_Use[m_nAryNumber].m_nItemCode);

                                if (m_gPanel_Itemslot_Equip_Information.activeSelf == true)
                                    m_gPanel_Itemslot_Equip_Information.SetActive(false);
                                if (m_gPanel_Itemslot_Use_Information.activeSelf == true)
                                    m_gPanel_Itemslot_Use_Information.SetActive(false);
                                if (m_gPanel_Itemslot_Etc_Information.activeSelf == true)
                                    m_gPanel_Itemslot_Etc_Information.SetActive(false);
                            }
                            else
                            {
                                m_gPanel_Quickslot_Signup.SetActive(false);
                            }
                        }
                        else if (GUIManager_Total.Instance.m_GUI_Itemslot.m_eItemslot == E_ITEMSLOT.ETC)
                        {
                            if (Player_Itemslot.m_nary_Itemslot_Etc_Count[m_nAryNumber] != 0)
                            {
                                GUIManager_Total.Instance.Display_GUI_Quickslot_Signup(E_QUICKSLOT_CATEGORY.ETC, Player_Itemslot.m_gary_Itemslot_Etc[m_nAryNumber].m_nItemCode);

                                if (m_gPanel_Itemslot_Equip_Information.activeSelf == true)
                                    m_gPanel_Itemslot_Equip_Information.SetActive(false);
                                if (m_gPanel_Itemslot_Use_Information.activeSelf == true)
                                    m_gPanel_Itemslot_Use_Information.SetActive(false);
                                if (m_gPanel_Itemslot_Etc_Information.activeSelf == true)
                                    m_gPanel_Itemslot_Etc_Information.SetActive(false);
                            }
                            else
                            {
                                m_gPanel_Quickslot_Signup.SetActive(false);
                            }
                        }

                        m_gPanel_Quickslot_Signup.gameObject.transform.SetAsLastSibling();
                        m_gPanel_Quickslot_Signup.SetActive(true);

                        m_fPanelCoordination_x = Mathf.Round(this.transform.position.x);
                        m_fPanelCoordination_y = Mathf.Round(this.transform.position.y);
                        m_gPanel_Quickslot_Signup.transform.position = new Vector2(m_fPanelCoordination_x, m_fPanelCoordination_y);
                    }
            if (GUIManager_Total.Instance.m_GUI_Itemslot.m_eItemslot == E_ITEMSLOT.ETC)
                if (Player_Itemslot.m_nary_Itemslot_Etc_Count[m_nAryNumber] != 0)
                    if (GUIManager_Total.Instance != null && GUIManager_Total.Instance.m_GUI_Quickslot_Signup != null)
                    {
                        if (GUIManager_Total.Instance.m_GUI_Itemslot.m_eItemslot == E_ITEMSLOT.EQUIP)
                        {
                            if (Player_Itemslot.m_nary_Itemslot_Equip_Count[m_nAryNumber] != 0)
                            {
                                GUIManager_Total.Instance.Display_GUI_Quickslot_Signup(E_QUICKSLOT_CATEGORY.EQUIP, m_nAryNumber);

                                if (m_gPanel_Itemslot_Equip_Information.activeSelf == true)
                                    m_gPanel_Itemslot_Equip_Information.SetActive(false);
                                if (m_gPanel_Itemslot_Use_Information.activeSelf == true)
                                    m_gPanel_Itemslot_Use_Information.SetActive(false);
                                if (m_gPanel_Itemslot_Etc_Information.activeSelf == true)
                                    m_gPanel_Itemslot_Etc_Information.SetActive(false);
                            }
                            else
                            {
                                m_gPanel_Quickslot_Signup.SetActive(false);
                            }
                        }
                        else if (GUIManager_Total.Instance.m_GUI_Itemslot.m_eItemslot == E_ITEMSLOT.USE)
                        {
                            if (Player_Itemslot.m_nary_Itemslot_Use_Count[m_nAryNumber] != 0)
                            {
                                GUIManager_Total.Instance.Display_GUI_Quickslot_Signup(E_QUICKSLOT_CATEGORY.USE, Player_Itemslot.m_gary_Itemslot_Use[m_nAryNumber].m_nItemCode);

                                if (m_gPanel_Itemslot_Equip_Information.activeSelf == true)
                                    m_gPanel_Itemslot_Equip_Information.SetActive(false);
                                if (m_gPanel_Itemslot_Use_Information.activeSelf == true)
                                    m_gPanel_Itemslot_Use_Information.SetActive(false);
                                if (m_gPanel_Itemslot_Etc_Information.activeSelf == true)
                                    m_gPanel_Itemslot_Etc_Information.SetActive(false);
                            }
                            else
                            {
                                m_gPanel_Quickslot_Signup.SetActive(false);
                            }
                        }
                        else if (GUIManager_Total.Instance.m_GUI_Itemslot.m_eItemslot == E_ITEMSLOT.ETC)
                        {
                            if (Player_Itemslot.m_nary_Itemslot_Etc_Count[m_nAryNumber] != 0)
                            {
                                GUIManager_Total.Instance.Display_GUI_Quickslot_Signup(E_QUICKSLOT_CATEGORY.ETC, Player_Itemslot.m_gary_Itemslot_Etc[m_nAryNumber].m_nItemCode);

                                if (m_gPanel_Itemslot_Equip_Information.activeSelf == true)
                                    m_gPanel_Itemslot_Equip_Information.SetActive(false);
                                if (m_gPanel_Itemslot_Use_Information.activeSelf == true)
                                    m_gPanel_Itemslot_Use_Information.SetActive(false);
                                if (m_gPanel_Itemslot_Etc_Information.activeSelf == true)
                                    m_gPanel_Itemslot_Etc_Information.SetActive(false);
                            }
                            else
                            {
                                m_gPanel_Quickslot_Signup.SetActive(false);
                            }
                        }

                        m_gPanel_Quickslot_Signup.gameObject.transform.SetAsLastSibling();
                        m_gPanel_Quickslot_Signup.SetActive(true);

                        m_fPanelCoordination_x = Mathf.Round(this.transform.position.x);
                        m_fPanelCoordination_y = Mathf.Round(this.transform.position.y);
                        m_gPanel_Quickslot_Signup.transform.position = new Vector2(m_fPanelCoordination_x, m_fPanelCoordination_y);
                    }
        }
    }
}
