using UnityEngine;

/*
 * File :   Distribute.cs
 * Desc :   물과 음식 배급
 */

public class Distribute
{
    public void DistributeWater(CharacterData character, bool distributed)
    {
        if (distributed)
        {
            character.water += 8 * GameManager.Instance.player.waterGainUnit; //기본값 40
        }
        else if (character.job != "Player")
        {
            character.affection -= 5 * GameManager.Instance.player.affectionLossUnit;
        }
    }

    public void DistributeFood(CharacterData character, bool distributed)
    {
        if (distributed)
        {
            switch (character.job)
            {
                case "Player":
                    character.food += 7 * GameManager.Instance.player.foodGainUnit; //기본값 35
                    break;
                case "Doctor":
                    character.food += 8 * GameManager.Instance.player.foodGainUnit; //기본값 40
                    break;
                case "Engineer":
                    character.food += 6 * GameManager.Instance.player.foodGainUnit; //기본값 30
                    break;
                case "Guard":
                    character.food += 6 * GameManager.Instance.player.foodGainUnit; //기본값 30
                    break;
                default:
                    break;
            }
        }
        else if (character.job != "Player")
        {
            character.affection -= 5 * GameManager.Instance.player.affectionLossUnit;
        }
    }
}
