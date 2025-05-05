/*
 * File :   DistributeUIController.cs
 * Desc :   배급 여부 선택씬
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;


public class DistributeUIController : MonoBehaviour
{
    public Toggle playerWaterToggle;
    public Toggle playerFoodToggle;
    public Toggle guardWaterToggle;
    public Toggle guardFoodToggle;
    public Toggle doctorWaterToggle;
    public Toggle doctorFoodToggle;
    public Toggle engineerWaterToggle;
    public Toggle engineerFoodToggle;
    public Button NextDayButton;

    public TMP_Text waterNumber;
    public TMP_Text foodNumber;

    private float currentWater;
    private float currentFood;

    public Button MakeMedicineButton;

    private Distribute distributor = new Distribute();
    private NextDay nextDay = new NextDay();

    void Start()
    {
        currentWater = GameManager.Instance.itemData.waterN;
        currentFood = GameManager.Instance.itemData.foodN;

        AddToggleListeners();
        UpdateNumberText();
        EnforceToggleLimits();

        NextDayButton.onClick.AddListener(() =>
        {
            distributor.DistributeWater(GameManager.Instance.player, playerWaterToggle.isOn);
            distributor.DistributeFood(GameManager.Instance.player, playerFoodToggle.isOn);

            distributor.DistributeWater(GameManager.Instance.doctor, doctorWaterToggle.isOn);
            distributor.DistributeFood(GameManager.Instance.doctor, doctorFoodToggle.isOn);

            distributor.DistributeWater(GameManager.Instance.engineer, engineerWaterToggle.isOn);
            distributor.DistributeFood(GameManager.Instance.engineer, engineerFoodToggle.isOn);

            distributor.DistributeWater(GameManager.Instance.guard, guardWaterToggle.isOn);
            distributor.DistributeFood(GameManager.Instance.guard, guardFoodToggle.isOn);

            nextDay.RunNextDay(GameManager.Instance.player, GameManager.Instance.player.goodLeader);
            nextDay.RunNextDay(GameManager.Instance.doctor, GameManager.Instance.player.goodLeader);
            nextDay.RunNextDay(GameManager.Instance.engineer, GameManager.Instance.player.goodLeader);
            nextDay.RunNextDay(GameManager.Instance.guard, GameManager.Instance.player.goodLeader);

            foreach (CharacterData cd in GameManager.Instance.characterList)
            {
                if (cd == null)
                    continue;

                Debug.Log(
                    $"Job: {cd.job}\n" +
                    $"Food: {cd.food}\n" +
                    $"Water: {cd.water}\n" +
                    $"Electricity: {cd.electricity}\n" +
                    $"Mental: {cd.mental}\n" +
                    $"Affection: {cd.affection}\n" +
                    $"Is Sick: {cd.isSick}\n" +
                    $"Is Alive: {cd.isAlive}\n" +
                    $"Death Reason: {cd.deathReason}\n" +
                    // 증가·감소 단위
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
                    // 확률 관련
                    $"Facility Fail Prob: {cd.facilityFailProb}\n" +
                    $"Random Acting Prob: {cd.randomActingProb}\n" +
                    $"Repair Prob: {cd.repairProb}\n" +
                    $"Death Prob: {cd.deathProb}\n" +
                    // 그 외
                    $"Mission Days: {cd.missionDays}\n" +
                    $"Survival Pro: {cd.survivalPro}\n" +
                    $"Good Leader: {cd.goodLeader}"
                );
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
                $"Battery: {item.batteryN}\n"
            );

            SceneManager.LoadScene("AffectionScene");
        });
        MakeMedicineButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MakeMedicineScene");
        });

        void AddToggleListeners()
        {
            playerWaterToggle.onValueChanged.AddListener(val => OnToggleChanged(val, true));
            guardWaterToggle.onValueChanged.AddListener(val => OnToggleChanged(val, true));
            doctorWaterToggle.onValueChanged.AddListener(val => OnToggleChanged(val, true));
            engineerWaterToggle.onValueChanged.AddListener(val => OnToggleChanged(val, true));

            playerFoodToggle.onValueChanged.AddListener(val => OnToggleChanged(val, false));
            guardFoodToggle.onValueChanged.AddListener(val => OnToggleChanged(val, false));
            doctorFoodToggle.onValueChanged.AddListener(val => OnToggleChanged(val, false));
            engineerFoodToggle.onValueChanged.AddListener(val => OnToggleChanged(val, false));
        }

        void OnToggleChanged(bool isOn, bool isWater)
        {
            float change = isOn ? -0.25f : 0.25f;

            if (isWater)
                currentWater += change;
            else
                currentFood += change;

            UpdateNumberText();
            EnforceToggleLimits();
        }

        void UpdateNumberText()
        {
            waterNumber.text = $"Water: {currentWater:F2}";
            foodNumber.text = $"Food: {currentFood:F2}";
            MakeMedicineButton.interactable = (currentFood >= 0.25f && currentWater >= 0.25f && GameManager.Instance.player.pharmacist);
        }

        void EnforceToggleLimits()
        {
            Toggle[] waterToggles = { playerWaterToggle, guardWaterToggle, doctorWaterToggle, engineerWaterToggle };
            Toggle[] foodToggles = { playerFoodToggle, guardFoodToggle, doctorFoodToggle, engineerFoodToggle };

            foreach (var toggle in waterToggles)
            {
                if (!toggle.isOn)
                    toggle.interactable = currentWater >= 0.25f;
            }

            foreach (var toggle in foodToggles)
            {
                if (!toggle.isOn)
                    toggle.interactable = currentFood >= 0.25f;
            }
        }
    }
}