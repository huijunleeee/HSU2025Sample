/*
 * File :   Affection.cs
 * Desc :   캐릭터 호감도
 */
using UnityEngine;

public class Affection
{
    public void GainAffection(CharacterData character,int multiple)
    {
        if (character == null)
            return;

        // Player의 affectionGainUnit만큼 증가
        int gain = multiple * GameManager.Instance.player.affectionGainUnit;
        character.affection += gain;
    }

    public void LoseAffection(CharacterData character, int multiple)
    {
        if (character == null || character.job == "Player")
            return;

        // Player의 affectionLossUnit만큼 감소
        int loss = multiple * GameManager.Instance.player.affectionLossUnit;
        character.affection -= loss;
    }

}
