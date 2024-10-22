using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// ※ 몬스터 제거(토벌 + 놓아주기) 시 플레이어가 획득 가능한 아이템을 관리하는 Monster_Drop 클래스는 과거 다른 Monster_ㆍㆍㆍ 클래스와 마찬가지로 각종 몬스터의 ㆍㆍㆍ_Drop 클래스를 상속으로 구현했었다.
//    그러나 각각의 몬스터가 각각의 드랍 아이템 저장소를 가지는것은 비효율적이라 생각해 몬스터 드랍 아이템 저장소를 MonsterManager 클래스로 통합했다.
//    그 결과 엄청난 메모리를 절약할 수 있었고, 현재 Monster_Drop 클래스에는 몬스터 제거(토벌 + 놓아주기) 시 아이템을 생성하여 필드에 드랍하는 기능만이 존재한다.
// 

public class Monster_Drop : MonoBehaviour
{
    //public List<GameObject> m_List_DropItem;
    //public List<Item_Equip> m_List_DropItem_Equip;
    //public List<Item_Use> m_List_DropItem_Use;
    //public List<Item_Etc> m_List_DropItem_Etc;
    //public List<int> m_List_DropItem_Gold_Min;
    //public List<int> m_List_DropItem_Gold_Max;

    //public List<int> m_List_DropPercent;
    //public List<int> m_List_DropPercent_Equip;
    //public List<int> m_List_DropPercent_Use;
    //public List<int> m_List_DropPercent_Etc;
    //public List<int> m_List_DropPercent_Gold;

    protected int m_nRandomNum;
    protected int m_nRandomGold;
    protected Vector3 m_vItempos;
    protected Vector3 m_vItemoffset;

    virtual public void Start()
    {
        InitialSet();
    }

    virtual public void InitialSet()
    {
        EssentialSet();
    }
    protected void EssentialSet()
    {
        //m_List_DropItem = new List<GameObject>();
        //m_List_DropItem_Equip = new List<Item_Equip>();
        //m_List_DropItem_Use = new List<Item_Use>();
        //m_List_DropItem_Etc = new List<Item_Etc>();
        //m_List_DropItem_Gold_Min = new List<int>();
        //m_List_DropItem_Gold_Max = new List<int>();

        //m_List_DropPercent = new List<int>();
        //m_List_DropPercent_Equip = new List<int>();
        //m_List_DropPercent_Use = new List<int>();
        //m_List_DropPercent_Etc = new List<int>();
        //m_List_DropPercent_Gold = new List<int>();
    }

    virtual public void DropItem(Vector3 pos)
    {
        //for (int i = 0; i < m_List_DropItem_Equip.Count; i++)
        //{
        //    m_nRandomNum = Random.Range(1, 10001);
        //    if (m_nRandomNum <= m_List_DropPercent_Equip[i])
        //    {
        //        Item item = new Item_Equip(m_List_DropItem_Equip[i], this.transform.position + m_vItempos);
        //        Destroy(item);
        //        m_vItempos += m_vItemoffset;
        //    }
        //}
        //for (int i = 0; i < m_List_DropItem_Use.Count; i++)
        //{
        //    m_nRandomNum = Random.Range(1, 10001);
        //    if (m_nRandomNum <= m_List_DropPercent_Use[i])
        //    {
        //        Item item = new Item_Use(m_List_DropItem_Use[i], this.transform.position + m_vItempos);
        //        Destroy(item);
        //        m_vItempos += m_vItemoffset;
        //    }
        //}
        //for (int i = 0; i < m_List_DropItem_Etc.Count; i++)
        //{
        //    m_nRandomNum = Random.Range(1, 10001);
        //    if (m_nRandomNum <= m_List_DropPercent_Etc[i])
        //    {
        //        Item item = new Item_Etc(m_List_DropItem_Etc[i], this.transform.position + m_vItempos);
        //        Destroy(item);
        //        m_vItempos += m_vItemoffset;
        //    }
        //}
        //for (int i = 0; i < m_List_DropPercent_Gold.Count; i++)
        //{
        //    m_nRandomNum = Random.Range(1, 10001);
        //    m_nRandomGold = Random.Range(m_List_DropItem_Gold_Min[i], m_List_DropItem_Gold_Max[i]);
        //    if (m_nRandomNum <= m_List_DropPercent_Gold[i])
        //    {
        //        Item item = new Item_Gold(m_nRandomGold, this.transform.position + m_vItempos);
        //        Destroy(item);
        //        m_vItempos += m_vItemoffset;
        //    }
        //}
    }

    virtual public void DropItem_Death(int monstercode, Vector3 pos)
    {
        if (MonsterManager.m_Dictionary_Monster.ContainsKey(monstercode) == true)
        {
            for (int i = 0; i < MonsterManager.m_Dictionary_Monster[monstercode].m_nlMonster_Dictionary_Death_Reward_Item_Equip.Count; i++)
            {
                for (int j = 0; j < MonsterManager.m_Dictionary_Monster[monstercode].m_nlMonster_Dictionary_Death_Reward_Item_Equip_Count[i]; j++)
                {
                    m_nRandomNum = Random.Range(1, 10001);
                    if (m_nRandomNum <= MonsterManager.m_Dictionary_Monster[monstercode].m_nlMonster_Dictionary_Death_Reward_Item_Equip_DropRate[i])
                    {
                        Item item = new Item_Equip(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[MonsterManager.m_Dictionary_Monster[monstercode].m_nlMonster_Dictionary_Death_Reward_Item_Equip[i]], this.transform.position + m_vItempos);
                        Destroy(item);
                        m_vItempos += m_vItemoffset;
                    }
                }
            }
            for (int i = 0; i < MonsterManager.m_Dictionary_Monster[monstercode].m_nlMonster_Dictionary_Death_Reward_Item_Use.Count; i++)
            {
                for (int j = 0; j < MonsterManager.m_Dictionary_Monster[monstercode].m_nlMonster_Dictionary_Death_Reward_Item_Use_Count[i]; j++)
                {
                    m_nRandomNum = Random.Range(1, 10001);
                    if (m_nRandomNum <= MonsterManager.m_Dictionary_Monster[monstercode].m_nlMonster_Dictionary_Death_Reward_Item_Use_DropRate[i])
                    {
                        Item item = new Item_Use(ItemManager.instance.m_Dictionary_MonsterDrop_Use[MonsterManager.m_Dictionary_Monster[monstercode].m_nlMonster_Dictionary_Death_Reward_Item_Use[i]], this.transform.position + m_vItempos);
                        Destroy(item);
                        m_vItempos += m_vItemoffset;
                    }
                }
            }
            for (int i = 0; i < MonsterManager.m_Dictionary_Monster[monstercode].m_nlMonster_Dictionary_Death_Reward_Item_Etc.Count; i++)
            {
                for (int j = 0; j < MonsterManager.m_Dictionary_Monster[monstercode].m_nlMonster_Dictionary_Death_Reward_Item_Etc_Count[i]; j++)
                {
                    m_nRandomNum = Random.Range(1, 10001);
                    if (m_nRandomNum <= MonsterManager.m_Dictionary_Monster[monstercode].m_nlMonster_Dictionary_Death_Reward_Item_Etc_DropRate[i])
                    {
                        Item item = new Item_Etc(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[MonsterManager.m_Dictionary_Monster[monstercode].m_nlMonster_Dictionary_Death_Reward_Item_Etc[i]], this.transform.position + m_vItempos);
                        Destroy(item);
                        m_vItempos += m_vItemoffset;
                    }
                }
            }
            for (int i = 0; i < MonsterManager.m_Dictionary_Monster[monstercode].m_nMonster_Death_Reward_Gold_Count; i++)
            {
                m_nRandomNum = Random.Range(1, 10001);
                if (m_nRandomNum <= MonsterManager.m_Dictionary_Monster[monstercode].m_nMonster_Death_Reward_Gold_DropRate)
                {
                    m_nRandomGold = Random.Range(MonsterManager.m_Dictionary_Monster[monstercode].m_nMonster_Death_Reward_Gold_Min, MonsterManager.m_Dictionary_Monster[monstercode].m_nMonster_Death_Reward_Gold_Max + 1);
                    Item item = new Item_Gold(m_nRandomGold, this.transform.position + m_vItempos);
                    Destroy(item);
                    m_vItempos += m_vItemoffset;
                }
            }
        }
    }
    virtual public void DropItem_Goaway(int monstercode, Vector3 pos)
    {
        if (MonsterManager.m_Dictionary_Monster.ContainsKey(monstercode) == true)
        {
            for (int i = 0; i < MonsterManager.m_Dictionary_Monster[monstercode].m_nlMonster_Dictionary_Goaway_Reward_Item_Equip.Count; i++)
            {
                for (int j = 0; j < MonsterManager.m_Dictionary_Monster[monstercode].m_nlMonster_Dictionary_Goaway_Reward_Item_Equip_Count[i]; j++)
                {
                    m_nRandomNum = Random.Range(1, 10001);
                    if (m_nRandomNum <= MonsterManager.m_Dictionary_Monster[monstercode].m_nlMonster_Dictionary_Goaway_Reward_Item_Equip_DropRate[i])
                    {
                        Item item = new Item_Equip(ItemManager.instance.m_Dictionary_MonsterDrop_Equip[MonsterManager.m_Dictionary_Monster[monstercode].m_nlMonster_Dictionary_Goaway_Reward_Item_Equip[i]], this.transform.position + m_vItempos);
                        Destroy(item);
                        m_vItempos += m_vItemoffset;
                    }
                }
            }
            for (int i = 0; i < MonsterManager.m_Dictionary_Monster[monstercode].m_nlMonster_Dictionary_Goaway_Reward_Item_Use.Count; i++)
            {
                for (int j = 0; j < MonsterManager.m_Dictionary_Monster[monstercode].m_nlMonster_Dictionary_Goaway_Reward_Item_Use_Count[i]; j++)
                {
                    m_nRandomNum = Random.Range(1, 10001);
                    if (m_nRandomNum <= MonsterManager.m_Dictionary_Monster[monstercode].m_nlMonster_Dictionary_Goaway_Reward_Item_Use_DropRate[i])
                    {
                        Item item = new Item_Use(ItemManager.instance.m_Dictionary_MonsterDrop_Use[MonsterManager.m_Dictionary_Monster[monstercode].m_nlMonster_Dictionary_Goaway_Reward_Item_Use[i]], this.transform.position + m_vItempos);
                        Destroy(item);
                        m_vItempos += m_vItemoffset;
                    }
                }
            }
            for (int i = 0; i < MonsterManager.m_Dictionary_Monster[monstercode].m_nlMonster_Dictionary_Goaway_Reward_Item_Etc.Count; i++)
            {
                for (int j = 0; j < MonsterManager.m_Dictionary_Monster[monstercode].m_nlMonster_Dictionary_Goaway_Reward_Item_Etc_Count[i]; j++)
                {
                    m_nRandomNum = Random.Range(1, 10001);
                    if (m_nRandomNum <= MonsterManager.m_Dictionary_Monster[monstercode].m_nlMonster_Dictionary_Goaway_Reward_Item_Etc_DropRate[i])
                    {
                        Item item = new Item_Etc(ItemManager.instance.m_Dictionary_MonsterDrop_Etc[MonsterManager.m_Dictionary_Monster[monstercode].m_nlMonster_Dictionary_Goaway_Reward_Item_Etc[i]], this.transform.position + m_vItempos);
                        Destroy(item);
                        m_vItempos += m_vItemoffset;
                    }
                }
            }
            for (int i = 0; i < MonsterManager.m_Dictionary_Monster[monstercode].m_nMonster_Goaway_Reward_Gold_Count; i++)
            {
                m_nRandomNum = Random.Range(1, 10001);
                if (m_nRandomNum <= MonsterManager.m_Dictionary_Monster[monstercode].m_nMonster_Goaway_Reward_Gold_DropRate)
                {
                    m_nRandomGold = Random.Range(MonsterManager.m_Dictionary_Monster[monstercode].m_nMonster_Goaway_Reward_Gold_Min, MonsterManager.m_Dictionary_Monster[monstercode].m_nMonster_Goaway_Reward_Gold_Max + 1);
                    Item item = new Item_Gold(m_nRandomGold, this.transform.position + m_vItempos);
                    Destroy(item);
                    m_vItempos += m_vItemoffset;
                }
            }
        }
    }
}
