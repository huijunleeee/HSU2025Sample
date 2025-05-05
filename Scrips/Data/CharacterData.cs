/*
 * File :   CharacterData.cs
 * Desc :   캐릭터 상태 데이터
 */

using UnityEngine;

public class CharacterData
{
    // 캐릭터 기본 정보 및 수치(정수값 0~100)
    public string job;
    public int food;
    public int water;
    public int mental;
    public int health;
    public bool isSick;
    public bool isAlive;
    public int affection;
    //증가 및 감소 기본 단위(증가율 감소율 조정을 편하게 하기 위해)
    public int foodGainUnit;
    public int foodLossUnit;
    public int waterGainUnit;
    public int waterLossUnit;
    public int electricityGainUnit;
    public int electricityLossUnit;
    public int mentalGainUnit;
    public int mentalLossUnit;
    public int affectionGainUnit;
    public int affectionLossUnit;
    //확률
    public float facilityFailProb;
    public float randomActingProb;
    public float repairProb;
    public float deathProb;
    public float sickProb;
    //탐사 일수
    public int missionDays;
    //죽는 버틸 수 있는가
    public bool survivalPro;
    //리더쉽 여부 (false일 시 매일 호감도 감소)
    public bool goodLeader;
    //약사 (true일 시 약 만들기 가능)
    public bool pharmacist;
    // 과민반응 (true일 시 멘탈이 낮으면 랜덤 아이템 파괴)
    public bool overreaction;
    //남은 전기량 (0이 될 시 산소 시스템 중지로 전멸)
    public int electricity;
    //사인
    public string deathReason;

    public CharacterData(string j) {
        // 상태 초기값
        job = j;
        food = 100;
        water = 100;
        health = 100;

        mental = 100;
        isSick = false;
        isAlive = true;

        deathReason = " ";
        switch (j)
        {
            case "Doctor":
                affection = 50;
                break;
            case "Engineer":
                affection = 50;
                break;
            case "Guard":
                affection = 50;
                break;
            case "Player":
                foodGainUnit = foodLossUnit = waterGainUnit = waterLossUnit = electricityGainUnit = electricityLossUnit = mentalGainUnit = mentalLossUnit = affectionGainUnit = affectionLossUnit = 5;
                electricity = 100;
                facilityFailProb = 0.2f;
                randomActingProb = 0f;
                repairProb = 0.5f;
                deathProb = 0.5f;
                sickProb = 1f;
                missionDays = 3;
                survivalPro = false;
                goodLeader = true;
                pharmacist = false;
                overreaction = false;
                break;
        }
    }
}
