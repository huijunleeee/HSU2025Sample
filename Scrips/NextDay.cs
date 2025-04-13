using UnityEngine;

/*
 * File :   NextDay.cs
 * Desc :   다음 날로 넘어갈 때
 */

public class NextDay
{
    public void RunNextDay(CharacterData character, bool goodLeader)
    {
        character.food -= 4 * GameManager.Instance.player.foodLossUnit;
        character.water -= 6 * GameManager.Instance.player.waterLossUnit;
        character.mental -= GameManager.Instance.player.mentalLossUnit;

        if (character.job != "Player") { 

            if (!goodLeader)
            {
                character.affection -= 4 * GameManager.Instance.player.affectionLossUnit;
            }
        
            if (character.food > 100)
            {
                character.food = 100; // 호감도도 증가?
            }
            else if (character.food <= 0)
            {
                character.isAlive = false;
            }

            if (character.water > 100)
            {
                character.water = 100; // 호감도도 증가?
            }
            else if (character.water <= 0)
            {
                character.isAlive = false;
            }

            if (character.mental > 100)
            {
                character.mental = 100;
            }
            else if (character.mental <= 30)
            {
                if (Random.value < 0.1f) // 10% 확률
                    character.isAlive = false;
            }
            else if (character.mental <= 0)
            {
                character.mental = 0;
                if (Random.value < 0.5f) // 50% 확률
                    character.isAlive = false;
            }

            if (character.affection > 500)
            {
                character.affection = 500;
            }
            else if (character.affection <= 100)
            {
                if (Random.value < 0.1f) // 10% 확률로 떠나감
                    character.isAlive = false;
            }
            else if (character.affection <= 0)
            {
                if (Random.value < 0.5f) // 50% 확률로 떠나감
                    character.isAlive = false;
                character.affection = 0;
            }
        }
        if (character.job == "Player")
        {
            if (character.food > 100)
            {
                character.food = 100;
            }
            else if (character.food <= 0)
            {
                character.isAlive = false;
                if (character.survivalPro)
                {
                    character.isAlive = true;
                    character.survivalPro = false;
                }
            }

            if (character.water > 100)
            {
                character.water = 100;
                if (character.survivalPro)
                {
                    character.isAlive = true;
                    character.survivalPro = false;
                }
            }
            else if (character.water <= 0)
            {
                character.isAlive = false;
            }

            if (character.mental > 100)
            {
                character.mental = 100;
            }
            else if (character.mental <= 30)
            {
                if (Random.value < 0.1f) // 10% 확률
                    character.isAlive = false;
                // 과민반응
            }
            else if (character.mental <= 0)
            {
                character.mental = 0;
                if (Random.value < 0.5f) // 50% 확률
                    character.isAlive = false;
                // 과민반응
            }

            character.electricity -= GameManager.Instance.player.electricityLossUnit;

            if (character.electricity > 100)
            {
                character.electricity = 100;
            }
            else if (character.electricity <= 0)
            {
                GameManager.Instance.characterList.ForEach(character =>
                {
                    if (character != null)
                        character.isAlive = false;
                });
            }

            GameManager.Instance.day++;

            if (GameManager.Instance.day == 30)
            {
                Debug.Log("Game end");
            }
        }
        
    }
}
