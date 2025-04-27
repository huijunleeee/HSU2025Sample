/*
 * File :   Die.cs
 * Desc :   캐릭터가 사망했을 때
 */
using UnityEngine;
using System.Collections.Generic;

public class Die
{
    public string RemoveDeadCharacters()
    {
        List<CharacterData> toRemove = new List<CharacterData>();

        foreach (CharacterData character in GameManager.Instance.characterList)
        {
            if (character != null && !character.isAlive)
            {
                toRemove.Add(character);
            }
        }

        string message = "";

        foreach (CharacterData character in toRemove)
        {
            string jobName = character.job switch
            {
                "Doctor" => "의사",
                "Engineer" => "정비사",
                "Guard" => "경비",
                "Player" => "플레이어",
                _ => "알 수 없음"
            };

            int idx = GameManager.Instance.characterList.IndexOf(character);
            if (idx >= 0)
                GameManager.Instance.characterList[idx] = null;

            message += $"{jobName}: {character.deathReason}\n";
        }

        return message.TrimEnd();
    }

}
