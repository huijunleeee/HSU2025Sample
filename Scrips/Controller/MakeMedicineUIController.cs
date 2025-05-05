using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MedCountController : MonoBehaviour
{
    private UsingItem usingItem = new UsingItem();

    public Button plusButton;
    public Button minusButton;
    public Button okButton;

    public TMP_Text countText;

    public TMP_Text waterNumber;
    public TMP_Text foodNumber;

    private float currentWater;
    private float currentFood;

    public int currentCount = 0;
    public int maxCount = 99;

    void Start()
    {
        currentWater = GameManager.Instance.itemData.waterN;
        currentFood = GameManager.Instance.itemData.foodN;

        UpdateText();
        UpdateNumberText();


        plusButton.onClick.AddListener(() =>
        {
            if (currentCount < maxCount)
                currentCount++;
            UpdateText();
            currentWater -= 0.25f;
            currentFood -= 0.25f;
            UpdateNumberText();
            UpdatePlusButtonInteractable();
        });

        minusButton.onClick.AddListener(() =>
        {
            if (currentCount > 0)
                currentCount--;
            UpdateText();
            currentWater += 0.25f;
            currentFood += 0.25f;
            UpdateNumberText();
            UpdatePlusButtonInteractable();
        });

        okButton.onClick.AddListener(() =>
        {
            usingItem.MakeMedicine(currentCount);

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
            SceneManager.LoadScene("DistributeScene");
        });
    }
    void UpdateNumberText()
    {
        waterNumber.text = $"Water: {currentWater:F2}";
        foodNumber.text = $"Food: {currentFood:F2}";
    }
    void UpdatePlusButtonInteractable()
    {
        plusButton.interactable = (currentCount < maxCount && currentWater >= 0.25f && currentFood >= 0.25f);
    }
    void UpdateText()
    {
        countText.text = currentCount.ToString();
    }
}
