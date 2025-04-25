/*
 * File :   CreateCharacter.cs
 * Desc :   캐릭터 생성
 */

using UnityEngine;

public class CreateCharacter
{

    public CharacterData CharacterFarmed(string job, bool farmed)
    {
        switch (job)
        {
            case "Doctor":
                if (!farmed)
                    return null;
                else {
                    CharacterData doctor = new CharacterData("Doctor");
                    return doctor; 
                }
                    
            case "Engineer":
                if (!farmed)
                    return null;
                else{
                    CharacterData engineer = new CharacterData("Engineer");
                    return engineer;
                }
                
            case "Guard":
                if (!farmed)
                    return null;
                else{
                    CharacterData guard = new CharacterData("Guard");
                    return guard;
                }
                
            case "Player":
                CharacterData player = new CharacterData("Player");
                return player;
            default:
                Debug.LogWarning("Invalid job type: " + job);
                return null;
        }

    }
}
