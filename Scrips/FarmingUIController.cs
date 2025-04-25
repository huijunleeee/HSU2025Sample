/*
 * File :   FarmingUIController.cs
 * Desc :   파밍 여부에 따른 캐릭터 생성 부분
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FarmingUIController : MonoBehaviour
{
    public Toggle doctorToggle;
    public Toggle engineerToggle;
    public Toggle guardToggle;
    public Button farmingEndButton;

    private CreateCharacter characterCreator = new CreateCharacter();

    void Start()
    {
        farmingEndButton.onClick.AddListener(() =>
        {
            GameManager.Instance.player = characterCreator.CharacterFarmed("Player", true);
            GameManager.Instance.doctor = characterCreator.CharacterFarmed("Doctor", doctorToggle.isOn);
            GameManager.Instance.engineer = characterCreator.CharacterFarmed("Engineer", engineerToggle.isOn);
            GameManager.Instance.guard = characterCreator.CharacterFarmed("Guard", guardToggle.isOn);

            GameManager.Instance.RefreshCharacterList();

            foreach (CharacterData cd in GameManager.Instance.characterList)
            {
                if (cd != null)
                {
                    Debug.Log(
                        $"Job: {cd.job}\n" +
                        $"Food: {cd.food}\n" +
                        $"Water: {cd.water}\n" +
                        $"Electricity: {cd.electricity}\n" +
                        $"Mental: {cd.mental}\n" +
                        $"Affection: {cd.affection}\n" +
                        $"Is Sick: {cd.isSick}\n" +
                        $"Is Alive: {cd.isAlive}\n" +
                        $"Food Gain Unit: {cd.foodGainUnit}\n" +
                        $"Food Loss Unit: {cd.foodLossUnit}\n" +
                        $"Water Gain Unit: {cd.waterGainUnit}\n" +
                        $"Water Loss Unit: {cd.waterLossUnit}\n" +
                        $"Electricity Gain Unit: {cd.electricityGainUnit}\n" +
                        $"Electricity Loss Unit: {cd.electricityLossUnit}\n" +
                        $"Mental Gain Unit: {cd.mentalGainUnit}\n" +
                        $"Mental Loss Unit: {cd.mentalLossUnit}\n" +
                        $"Affection Gain Unit: {cd.affectionGainUnit}\n" +
                        $"Affection Loss Unit: {cd.affectionLossUnit}\n" +
                        $"Facility Fail Probability: {cd.facilityFailProb}\n" +
                        $"Random Acting Probability: {cd.randomActingProb}\n" +
                        $"Repair Probability: {cd.repairProb}\n" +
                        $"Mission Days: {cd.missionDays}\n" +
                        $"Survival Pro: {cd.survivalPro}\n" +
                        $"Good Leader: {cd.goodLeader}\n"
                    );
                }
            }
            
        });
    }
}
