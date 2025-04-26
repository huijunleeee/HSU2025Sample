/*
 * File :   DistributeUIController.cs
 * Desc :   배급 여부 선택 및 다음 날로 넘어가는 부분
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class DistributeUIController : MonoBehaviour
{
    public Toggle playerWaterToggle;
    public Toggle playerFoodrToggle;
    public Toggle guardWaterToggle;
    public Toggle guardFoodrToggle;
    public Toggle doctorWaterToggle;
    public Toggle doctorFoodrToggle; 
    public Toggle engineerWaterToggle;
    public Toggle engineerFoodrToggle;
    public Button NextDayButton;
    
    private Distribute distributor = new Distribute();
    private NextDay nextDay = new NextDay();

    void Start()
    {
        NextDayButton.onClick.AddListener(() =>
        {
            distributor.DistributeWater(GameManager.Instance.player, playerWaterToggle.isOn);
            distributor.DistributeFood(GameManager.Instance.player, playerFoodrToggle.isOn);

            distributor.DistributeWater(GameManager.Instance.doctor, doctorWaterToggle.isOn);
            distributor.DistributeFood(GameManager.Instance.doctor, doctorFoodrToggle.isOn);

            distributor.DistributeWater(GameManager.Instance.engineer, engineerWaterToggle.isOn);
            distributor.DistributeFood(GameManager.Instance.engineer, engineerFoodrToggle.isOn);

            distributor.DistributeWater(GameManager.Instance.guard, guardWaterToggle.isOn);
            distributor.DistributeFood(GameManager.Instance.guard, guardFoodrToggle.isOn);

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

            Debug.Log($"[Day]: {GameManager.Instance.day}");
        });
    }
}
