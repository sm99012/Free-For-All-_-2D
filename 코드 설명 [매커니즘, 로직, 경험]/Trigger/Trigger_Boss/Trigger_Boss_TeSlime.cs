using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Boss_TeSlime : Trigger_Boss
{
    protected override void InitialSet_Etc()
    {
        m_sStatus_Necessity_Down.SetSTATUS_LV(12);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player_Total.Instance.m_pm_Move.m_rRigdbody.sleepMode = RigidbodySleepMode2D.NeverSleep;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player_Total.Instance.m_pm_Move.m_rRigdbody.sleepMode = RigidbodySleepMode2D.StartAsleep;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (BossManager.Instance.m_eBattle_Boss_Info == E_BATTLE_BOSS_INFO.NULL)
            {
                m_bTrigger_Progress_Context = false;
                BossManager.m_Dictionary_BossBattle[1].m_bSpawn_Possible = true;
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (Check_Condition_Total() == true)
                {
                    if (BossManager.m_Dictionary_BossBattle[1].m_bSpawn_Possible == true)
                    {
                        GUIManager_Total.Instance.Display_GUI_BossBattleInformation(BossManager.m_Dictionary_BossBattle[1], this.gameObject.transform.position);
                    }
                    //else
                    //{
                    //    int time = BossManager.Instance.Get_Boss_DurationTime_TeSlime();
                    //    int minute = time / 60;
                    //    int second = time % 60;
                    //    GUIManager_Total.Instance.UpdateLog(minute.ToString() + " 분 " + second.ToString() + " 초 를 기다려야 합니다.");
                    //}
                }
                else
                    GUIManager_Total.Instance.UpdateLog("요구조건을 만족하지못해 보스전이 불가능합니다.");
            }
        }
    }
}
