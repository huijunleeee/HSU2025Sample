/*
 * File :   AffectionUIController.cs
 * Desc :   호감도 이벤트 테스트
 */
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AffectionUIController : MonoBehaviour
{

    public Toggle goodToggle;      // 좋은 선택
    public Toggle badToggle;       // 나쁜 선택

    public Toggle doctorToggle;    // 의사
    public Toggle engineerToggle;  // 수리공
    public Toggle guardToggle;     // 경비원

    public Button fixButton;

    private Affection _affection = new Affection();

    void Awake()
    {
        // 선택지 중 하나만 선택
        goodToggle.onValueChanged.AddListener(isOn => { if (isOn) badToggle.isOn = false; });
        badToggle.onValueChanged.AddListener(isOn => { if (isOn) goodToggle.isOn = false; });

        // 직업 중 하나만 선택
        engineerToggle.onValueChanged.AddListener(isOn => { if (isOn) { guardToggle.isOn = false; doctorToggle.isOn = false; } });
        guardToggle.onValueChanged.AddListener(isOn => { if (isOn) { engineerToggle.isOn = false; doctorToggle.isOn = false; } });
        doctorToggle.onValueChanged.AddListener(isOn => { if (isOn) { engineerToggle.isOn = false; guardToggle.isOn = false; } });

        fixButton.onClick.AddListener(OnFix);
    }

    private void OnFix()
    {
        //선택된 캐릭터 결정
        CharacterData target = null;
        if (engineerToggle.isOn) target = GameManager.Instance.engineer;
        else if (guardToggle.isOn) target = GameManager.Instance.guard;
        else if (doctorToggle.isOn) target = GameManager.Instance.doctor;
        else
        {
            Debug.LogWarning("캐릭터를 하나 선택해주세요.");
            return;
        }

        //좋은/나쁜 선택지에 따라 호감도 조정
        if (goodToggle.isOn)
        {
            _affection.GainAffection(target, 4);
        }
        else if (badToggle.isOn)
        {
            _affection.LoseAffection(target, 2);
        }
        else
        {
            Debug.LogWarning("좋은/나쁜 선택 중 하나를 선택해주세요.");
            return;
        }

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
        //분배 씬으로 이동
        SceneManager.LoadScene("DistributeScene");
    }
}
