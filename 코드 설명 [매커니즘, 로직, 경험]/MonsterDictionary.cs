﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // 몬스터 도감 데이터 해금 비율 0% : + 몬스터 이름
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
    public List<int> m_nlMonster_Dictionary_Goaway_Reward_Item_Equip;          // 몬스터 제거(놓아주기) 보상 장비아이템 고유코드
    public List<int> m_nlMonster_Dictionary_Goaway_Reward_Item_Equip_DropRate; // 몬스터 제거(놓아주기) 보상 장비아이템 획득 확률
    public List<int> m_nlMonster_Dictionary_Goaway_Reward_Item_Equip_Count;    // 몬스터 제거(놓아주기) 보상 장비아이템 개수
    public List<int> m_nlMonster_Dictionary_Goaway_Reward_Item_Use;            // 몬스터 제거(놓아주기) 보상 소비아이템 고유코드
    public List<int> m_nlMonster_Dictionary_Goaway_Reward_Item_Use_DropRate;   // 몬스터 제거(놓아주기) 보상 소비아이템 획득 확률
    public List<int> m_nlMonster_Dictionary_Goaway_Reward_Item_Use_Count;      // 몬스터 제거(놓아주기) 보상 소비아이템 개수
    public List<int> m_nlMonster_Dictionary_Goaway_Reward_Item_Etc;            // 몬스터 제거(놓아주기) 보상 기타아이템 고유코드
    public List<int> m_nlMonster_Dictionary_Goaway_Reward_Item_Etc_DropRate;   // 몬스터 제거(놓아주기) 보상 기타아이템 획득 확률
    public List<int> m_nlMonster_Dictionary_Goaway_Reward_Item_Etc_Count;      // 몬스터 제거(놓아주기) 보상 기타아이템 개수
    public int m_nMonster_Goaway_Reward_Gold_Min;                              // 몬스터 제거(놓아주기) 보상 골드(재화) 최소
    public int m_nMonster_Goaway_Reward_Gold_Max;                              // 몬스터 제거(놓아주기) 보상 골드(재화) 최대
    public int m_nMonster_Goaway_Reward_Gold_Count;                            // 몬스터 제거(놓아주기) 보상 골드(재화) 개수
    public int m_nMonster_Goaway_Reward_Gold_DropRate;                         // 몬스터 제거(놓아주기) 보상 골드(재화) 획득 확률
    public STATUS m_SMonster_Goaway_Reward_STATUS;                             // 몬스터 제거(놓아주기) 보상 스탯(능력치)
    public SOC m_SMonster_Goaway_Reward_SOC;                                   // 몬스터 제거(놓아주기) 보상 스탯(평판)

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
    public void MonsterDictionary_Add_100P_Death_Reward_Item_Equip_Count(params int[] item) // item(가변인자) : 몬스터 제거(토벌) 보상 장비아이템 개수
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
    // 몬스터 도감 데이터 해금 비율 100% 설정 함수 - 몬스터 제거(놓아주기) 보상 골드(재화)
    public void MonsterDictionary_Add_100P_Death_Reward_Gold(int count = 0, int mingold = 0, int maxgold = 0, int rate = 0) // count : 골드(재화) 개수, mingold : 골드(재화) 최소, maxgold : 골드(재화) 최대, rate : 골드(재화) 획득 확률
    {
        this.m_nMonster_Death_Reward_Gold_Count = count;
        this.m_nMonster_Death_Reward_Gold_Max = maxgold;
        this.m_nMonster_Death_Reward_Gold_Min = mingold;
        this.m_nMonster_Death_Reward_Gold_DropRate = rate;
    }
    // 몬스터 도감 데이터 해금 비율 100% 설정 함수 - 몬스터 제거(놓아주기) 보상 스탯(능력치, 평판)
    public void MonsterDictionary_Add_100P_SS_Death(STATUS rewardstatus, SOC rewardsoc) // rewardstatus : 몬스터 제거(놓아주기) 보상 스탯(능력치), rewardsoc : 몬스터 제거(놓아주기) 보상 스탯(평판)
    {
        this.m_SMonster_Death_Reward_STATUS = rewardstatus;
        this.m_SMonster_Death_Reward_SOC = rewardsoc;
    }
    // 몬스터 도감 데이터 해금 비율 100% 설정 함수 - 몬스터 제거(놓아주기) 보상 장비아이템 고유코드
    public void MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip(params int[] item) // item(가변인자) : 몬스터 제거(놓아주기) 보상 장비아이템 고유코드
    {
        for (int i = 0; i < item.Length; i++)
        {
            m_nlMonster_Dictionary_Goaway_Reward_Item_Equip.Add(item[i]);
        }
    }
    // 몬스터 도감 데이터 해금 비율 100% 설정 함수 - 몬스터 제거(놓아주기) 보상 장비아이템 획득 확률
    public void MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip_DropRate(params int[] item) // item(가변인자) : 몬스터 제거(놓아주기) 보상 장비아이템 획득 확률
    {
        for (int i = 0; i < item.Length; i++)
        {
            m_nlMonster_Dictionary_Goaway_Reward_Item_Equip_DropRate.Add(item[i]);
        }
    }
    // 몬스터 도감 데이터 해금 비율 100% 설정 함수 - 몬스터 제거(놓아주기) 보상 장비아이템 개수
    public void MonsterDictionary_Add_100P_Goaway_Reward_Item_Equip_Count(params int[] item) // item(가변인자) : 몬스터 제거(놓아주기) 보상 장비아이템 개수
    {
        for (int i = 0; i < item.Length; i++)
        {
            m_nlMonster_Dictionary_Goaway_Reward_Item_Equip_Count.Add(item[i]);
        }
    }
    // 몬스터 도감 데이터 해금 비율 100% 설정 함수 - 몬스터 제거(놓아주기) 보상 소비아이템 고유코드
    public void MonsterDictionary_Add_100P_Goaway_Reward_Item_Use(params int[] item) // item(가변인자) : 몬스터 제거(놓아주기) 보상 소비아이템 고유코드
    {
        for (int i = 0; i < item.Length; i++)
        {
            m_nlMonster_Dictionary_Goaway_Reward_Item_Use.Add(item[i]);
        }
    }
    // 몬스터 도감 데이터 해금 비율 100% 설정 함수 - 몬스터 제거(놓아주기) 보상 소비아이템 획득 확률
    public void MonsterDictionary_Add_100P_Goaway_Reward_Item_Use_DropRate(params int[] item) // item(가변인자) : 몬스터 제거(놓아주기) 보상 소비아이템 획득 확률
    {
        for (int i = 0; i < item.Length; i++)
        {
            m_nlMonster_Dictionary_Goaway_Reward_Item_Use_DropRate.Add(item[i]);
        }
    }
    // 몬스터 도감 데이터 해금 비율 100% 설정 함수 - 몬스터 제거(놓아주기) 보상 소비아이템 개수
    public void MonsterDictionary_Add_100P_Goaway_Reward_Item_Use_Count(params int[] item) // item(가변인자) : 몬스터 제거(놓아주기) 보상 소비아이템 개수
    {
        for (int i = 0; i < item.Length; i++)
        {
            m_nlMonster_Dictionary_Goaway_Reward_Item_Use_Count.Add(item[i]);
        }
    }
    // 몬스터 도감 데이터 해금 비율 100% 설정 함수 - 몬스터 제거(놓아주기) 보상 기타아이템 고유코드
    public void MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc(params int[] item) // item(가변인자) : 몬스터 제거(놓아주기) 보상 기타아이템 고유코드
    {
        for (int i = 0; i < item.Length; i++)
        {
            m_nlMonster_Dictionary_Goaway_Reward_Item_Etc.Add(item[i]);
        }
    }
    // 몬스터 도감 데이터 해금 비율 100% 설정 함수 - 몬스터 제거(놓아주기) 보상 기타아이템 획득 확률
    public void MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc_DropRate(params int[] item) // item(가변인자) : 몬스터 제거(놓아주기) 보상 기타아이템 획득 확률
    {
        for (int i = 0; i < item.Length; i++)
        {
            m_nlMonster_Dictionary_Goaway_Reward_Item_Etc_DropRate.Add(item[i]);
        }
    }
    // 몬스터 도감 데이터 해금 비율 100% 설정 함수 - 몬스터 제거(놓아주기) 보상 기타아이템 개수
    public void MonsterDictionary_Add_100P_Goaway_Reward_Item_Etc_Count(params int[] item) // item(가변인자) : 몬스터 제거(놓아주기) 보상 기타아이템 개수
    {
        for (int i = 0; i < item.Length; i++)
        {
            m_nlMonster_Dictionary_Goaway_Reward_Item_Etc_Count.Add(item[i]);
        }
    }
    // 몬스터 도감 데이터 해금 비율 100% 설정 함수 - 몬스터 제거(놓아주기) 보상 골드(재화)
    public void MonsterDictionary_Add_100P_Goaway_Reward_Gold(int count = 0, int mingold = 0, int maxgold = 0, int rate = 0) // count : 골드(재화) 개수, mingold : 골드(재화) 최소, maxgold : 골드(재화) 최대, rate : 골드(재화) 획득 확률
    {
        this.m_nMonster_Goaway_Reward_Gold_Count = count;
        this.m_nMonster_Goaway_Reward_Gold_Max = maxgold;
        this.m_nMonster_Goaway_Reward_Gold_Min = mingold;
        this.m_nMonster_Goaway_Reward_Gold_DropRate = rate;
    }
    // 몬스터 도감 데이터 해금 비율 100% 설정 함수 - 몬스터 제거(놓아주기) 보상 스탯(능력치, 평판)
    public void MonsterDictionary_Add_100P_SS_Goaway(STATUS rewardstatus, SOC rewardsoc) // rewardstatus : 몬스터 제거(놓아주기) 보상 스탯(능력치), rewardsoc : 몬스터 제거(놓아주기) 보상 스탯(평판)
    {
        this.m_SMonster_Goaway_Reward_STATUS = rewardstatus;
        this.m_SMonster_Goaway_Reward_SOC = rewardsoc;
    }
}
