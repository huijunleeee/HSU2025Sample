/*
 * File :   FarmingUIController.cs
 * Desc :   파밍 여부에 따른 캐릭터 생성 및 아이템 개수 설정
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class FarmingUIController : MonoBehaviour
{
    public Toggle doctorToggle;
    public Toggle engineerToggle;
    public Toggle guardToggle;
    public Button farmingEndButton;

    public TMP_InputField waterInput;
    public TMP_InputField foodInput;
    public TMP_InputField diveKitInput;
    public TMP_InputField gameKitInput;
    public TMP_InputField medKitInput;
    public TMP_InputField bookInput;
    public TMP_InputField mapInput;
    public TMP_InputField researchInput;
    public TMP_InputField batteryInput;

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

            GameManager.Instance.itemData.foodN = float.TryParse(foodInput.text, out var foodVal) ? foodVal : 0f;
            GameManager.Instance.itemData.waterN = float.TryParse(waterInput.text, out var waterVal) ? waterVal : 0f;
            GameManager.Instance.itemData.diveKitN = int.TryParse(diveKitInput.text, out var diveKitVal) ? diveKitVal : 0;
            GameManager.Instance.itemData.gameKitN = int.TryParse(gameKitInput.text, out var gameKitVal) ? gameKitVal : 0;
            GameManager.Instance.itemData.medKitN = int.TryParse(medKitInput.text, out var medKitVal) ? medKitVal : 0;
            GameManager.Instance.itemData.bookN = int.TryParse(bookInput.text, out var bookVal) ? bookVal : 0;
            GameManager.Instance.itemData.mapN = int.TryParse(mapInput.text, out var mapVal) ? mapVal : 0;
            GameManager.Instance.itemData.researchN = int.TryParse(researchInput.text, out var researchVal) ? researchVal : 0;
            GameManager.Instance.itemData.batteryN = int.TryParse(batteryInput.text, out var batteryVal) ? batteryVal : 0;
            GameManager.Instance.itemData.medicineN = 0;

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

            var item = GameManager.Instance.itemData;
            Debug.Log(
                $"[ItemData]\n" +
                $"Food: {item.foodN}\n" +
                $"Water: {item.waterN}\n" +
                $"Dive Kit: {item.diveKitN}\n" +
                $"Game Kit: {item.gameKitN}\n" +
                $"Med Kit: {item.medKitN}\n" +
                $"Book: {item.bookN}\n" +
                $"Map: {item.mapN}\n" +
                $"Research: {item.researchN}\n" +
                $"Battery: {item.batteryN}\n" +
                $"Medicine: {item.medicineN}\n"
            );

        });
    }
}
