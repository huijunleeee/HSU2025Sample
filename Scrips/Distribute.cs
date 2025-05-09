/*
 * File :   Distribute.cs
 * Desc :   물과 음식 배급
 */

using UnityEngine;

public class Distribute
{
    public void DistributeWater(CharacterData character, bool distributed)
    {
        if (character != null)
        {
            if (distributed)
            {
                character.water += 8 * GameManager.Instance.player.waterGainUnit; //기본값 40
                GameManager.Instance.itemData.waterN -= 0.25f;
            }
            else if (character.job != "Player")
            {
                character.affection -= GameManager.Instance.player.affectionLossUnit;
            }
        }

    }

    public void DistributeFood(CharacterData character, bool distributed)
    {
        if (character != null)
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
                GameManager.Instance.itemData.foodN -= 0.25f;
            }
            else if (character.job != "Player")
            {
                character.affection -= GameManager.Instance.player.affectionLossUnit;
            }
        }

    }
}
