/*
 * File :   NextDay.cs
 * Desc :   다음 날로 넘어갈 때
 */

using UnityEngine;

public class NextDay
{
    private Die _die = new Die();
    private Affection _affection = new Affection();
    private UsingItem usingItem = new UsingItem();

    public void RunNextDay(CharacterData character, bool goodLeader)
    {
        if (character != null)
        {
            character.food -= 4 * GameManager.Instance.player.foodLossUnit;
            character.water -= 6 * GameManager.Instance.player.waterLossUnit;
            character.mental -= GameManager.Instance.player.mentalLossUnit;

            if (character.job == "Player")
                HandlePlayerTurn(character);
            else
                HandleNPCTurn(character, goodLeader);

            ClampStats(character);

            // 다음 날로 증가
            if (character.job == "Player")
                GameManager.Instance.day++;

            Debug.Log(_die.RemoveDeadCharacters());

            // 30일 차 게임 종료
            if (GameManager.Instance.day >= 30)
                Debug.Log("Game end");
        }
    }

    void HandleNPCTurn(CharacterData cd, bool goodLeader)
    {
        // 호감도 감소 (리더쉽 부족 특성)
        if (!goodLeader)
            _affection.LoseAffection(cd, 1);

        // 음식, 물 수치 일정 이하 시, 건강 수치 감소
        if (cd.food <= 30 && cd.water <= 30)
        {
            cd.health -= 25;
        }else if (cd.food <= 30 ||  cd.water <= 30)
        {
            cd.health -= 10;
        }

        // 아사와 갈사
        TryDeath(cd, cd.food <= 0, "아사");
        TryDeath(cd, cd.water <= 0, "갈사");

        float dp = GameManager.Instance.player.deathProb; //기본 0.5f

        // 정신 이상도에 따른 확률형 사망
        if (cd.mental <= 0) TryChanceDeath(cd, dp, "자살");
        else if (cd.mental <= 30) TryChanceDeath(cd, dp / 5, "자살");

        // 호감도에 따른 확률형 사망
        if (cd.affection <= 0) TryChanceDeath(cd, dp, "나를 피해 떠나감");
        else if (cd.affection <= 20) TryChanceDeath(cd, dp / 5, "나를 피해 떠나감");
        
        // 발병 여부에 따른 확률형 사망
        if (cd.isSick) TryChanceDeath(cd, dp, "병사");

        // 건강 수치에 따른 확률형 발병
        float sp = GameManager.Instance.player.sickProb; // 기본 1f

        if (cd.health <= 0) TryChanceSick(cd, sp);
        else if (cd.health <= 30) TryChanceSick(cd, sp / 3);

    }

    void HandlePlayerTurn(CharacterData pl)
    {
        // 음식, 물 수치 일정 이하 시, 건강 수치 감소
        if (pl.food <= 30 && pl.water <= 30)
        {
            pl.health -= 25;
        }
        else if (pl.food <= 30 || pl.water <= 30)
        {
            pl.health -= 10;
        }

        // 아사와 갈사(위기 대응 전문가 적용)
        TrySurviveOrDeath(pl, pl.food <= 0, "아사", 10);
        TrySurviveOrDeath(pl, pl.water <= 0, "갈사", 10);

        float dp = GameManager.Instance.player.deathProb; //0.5f

        // 정신 이상도에 따른 확률형 사망 및 과민반응
        if (pl.mental <= 0)
        {
            TryChanceDeath(pl, dp, "자살");
            if (GameManager.Instance.player.overreaction) usingItem.DestroyItem(2);
        }
        else if (pl.mental <= 30)
        {
            TryChanceDeath(pl, dp / 5, "자살");
            if (GameManager.Instance.player.overreaction) usingItem.DestroyItem(1);
        }
        // 발병 여부에 따른 확률형 사망
        if (pl.isSick) TryChanceDeath(pl, dp, "병사");

        // 건강 수치에 따른 확률형 발병
        float sp = GameManager.Instance.player.sickProb; // 기본 1f

        if (pl.health <= 0) TryChanceSick(pl, sp);
        else if (pl.health <= 30) TryChanceSick(pl, sp / 3);

        // 전력 소모 및 질식사(전원 사망)
        int eLoss = GameManager.Instance.player.electricityLossUnit;
        pl.electricity -= eLoss;
        if (pl.electricity <= 0)
            GameManager.Instance.characterList.ForEach(c =>
            {
                c.isAlive = false;
                c.deathReason = "질식사";
            });
    }


    // 즉시 사망 처리
    void TryDeath(CharacterData c, bool condition, string reason)
    {
        if (condition)
        {
            c.isAlive = false;
            c.deathReason = reason;
        }
    }

    // 확률 기반 사망 처리
    void TryChanceDeath(CharacterData c, float chance, string reason)
    {
        if (Random.value < chance)
        {
            c.isAlive = false;
            c.deathReason = reason;
        }
    }

    void TryChanceSick(CharacterData c, float chance)
    {
        if (Random.value < chance)
        {
            c.isSick = true;
        }
    }

    // 위기 대응 전문가용 사망 처리(아사, 갈사에 적용)
    void TrySurviveOrDeath(CharacterData c, bool condition, string reason, int reviveValue)
    {
        if (!condition) return;

        if (c.survivalPro)
        {
            c.isAlive = true;
            c.deathReason = null;
            c.survivalPro = false;
            if (c.food <= 0)
            {
                c.food = reviveValue;
            }

            if (c.water <= 0)
            {
                c.water = reviveValue;
            }
        }
        else
        {
            c.isAlive = false;
            c.deathReason = reason;
        }
    }
    // 스탯 0~100 유지
    void ClampStats(CharacterData c)
    {
        c.food = Mathf.Clamp(c.food, 0, 100);
        c.water = Mathf.Clamp(c.water, 0, 100);
        c.mental = Mathf.Clamp(c.mental, 0, 100);
        c.affection = Mathf.Clamp(c.affection, 0, 100);
        c.electricity = Mathf.Clamp(c.electricity, 0, 100);
        c.health = Mathf.Clamp(c.health, 0, 100);
    }

}
