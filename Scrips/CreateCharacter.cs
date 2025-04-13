using UnityEngine;

/*
 * File :   CreateCharacter.cs
 * Desc :   캐릭터 생성
 */

public class CreateCharacter
{

    public CharacterData CharacterFarmed(string job, bool farmed)
    {
        switch (job)
        {
            case "Doctor":
                CharacterData doctor = new CharacterData("Doctor");
                if (!farmed)
                    doctor.isAlive = false;
                return doctor;
            case "Engineer":
                CharacterData engineer = new CharacterData("Engineer");
                if (!farmed)
                    engineer.isAlive = false;
                return engineer;
            case "Guard":
                CharacterData guard = new CharacterData("Guard");
                if (!farmed)
                    guard.isAlive = false;
                return guard;
            case "Player":
                CharacterData player = new CharacterData("Player");
                return player;
            default:
                Debug.LogWarning("Invalid job type: " + job);
                return null;
        }

    }
}
