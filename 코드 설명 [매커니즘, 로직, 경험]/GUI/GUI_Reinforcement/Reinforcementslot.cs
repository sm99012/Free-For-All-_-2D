using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Reinforcementslot : MonoBehaviour, IPointerClickHandler
{
    // Reinforcementslot 에서 강화할 아이템 확인 UI.
    [SerializeField] Image m_IMG_ItemSprite;
    [SerializeField] Image m_IMG_BackgroundSprite;
    [SerializeField] TextMeshProUGUI m_TMP_ItemName;

    [SerializeField] GameObject m_gPanel_Reinforcement_Equip_Information;

    // 각 강화 슬롯 넘버.
    public int m_nAryNumber;

    // 사용 할 강화서의 위치 넘버.
    public int m_nReinforcement_AryNubmer;

    float m_fPanelCoordination_x;
    float m_fPanelCoordination_y;

    private void Awake()
    {
        InitialSet_Object();
    }

    // 초기 Object 설정.
    void InitialSet_Object()
    {
        m_gPanel_Reinforcement_Equip_Information = GameObject.Find("Canvas_GUI").transform.Find("Panel_Reinforcement_Equip_Information").gameObject;

        m_IMG_ItemSprite = this.gameObject.transform.Find("ReinforcementSlot_Panel_ItemSprite").gameObject.GetComponent<Image>();
        m_IMG_BackgroundSprite = this.gameObject.transform.Find("ReinforcementSlot_Panel_BackgroundSprite").gameObject.GetComponent<Image>();
        m_TMP_ItemName = this.gameObject.transform.Find("ReinforcementSlot_TMP_ItemName").gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void SetItem_Equip(Item_Equip item, int arynumber)
    {
        m_IMG_ItemSprite.sprite = item.m_sp_Sprite;
        m_IMG_ItemSprite.color = new Color(1, 1, 1, 1);

        m_nAryNumber = arynumber;

        m_TMP_ItemName.text = Player_Itemslot.m_gary_Itemslot_Equip[m_nAryNumber].m_sItemName + "\n" + Player_Itemslot.m_gary_Itemslot_Equip[m_nAryNumber].m_nReinforcementCount_Current + " / " + Player_Itemslot.m_gary_Itemslot_Equip[m_nAryNumber].m_nReinforcementCount_Max;

    }
    public void SetItem_Null()
    {
        m_IMG_ItemSprite.color = new Color(1, 1, 1, 0);
        m_IMG_ItemSprite.sprite = null;
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        GUIManager_Total.Instance.Update_Reinforcement_Equip_Information(Player_Itemslot.m_gary_Itemslot_Equip[m_nAryNumber], m_nAryNumber, m_nReinforcement_AryNubmer);

        m_gPanel_Reinforcement_Equip_Information.transform.SetAsLastSibling();
        m_gPanel_Reinforcement_Equip_Information.SetActive(true);
        // m_gPanel_Reinforcement_Equip_Information.transform.position = this.gameObject.transform.position;
        // m_gPanel_Reinforcement_Equip_Information.transform.position = GUIManager_Total.Instance.m_GUI_Reinforcement.m_gPanel_Reinforcement.transform.position;
        m_fPanelCoordination_x = Mathf.Round(GUIManager_Total.Instance.m_GUI_Reinforcement.m_gPanel_Reinforcement.transform.position.x);
        m_fPanelCoordination_y = Mathf.Round(GUIManager_Total.Instance.m_GUI_Reinforcement.m_gPanel_Reinforcement.transform.position.y);
        m_gPanel_Reinforcement_Equip_Information.transform.position = new Vector2(m_fPanelCoordination_x, m_fPanelCoordination_y);
    }
}
