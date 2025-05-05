/*
 * File :   UsingItem.cs
 * Desc :   배급을 제외한 아이템 사용
 */

using UnityEngine;
using System.Collections.Generic;

public class UsingItem
{
    public void MakeMedicine(int num)
    {
        if (GameManager.Instance.itemData.foodN > 0 && GameManager.Instance.itemData.waterN > 0)
        {
            GameManager.Instance.itemData.foodN -= num * 0.25f;
            GameManager.Instance.itemData.waterN -= num * 0.25f;
            GameManager.Instance.itemData.medicineN += num;
        }
    }

    public void DestroyItem(int num)
    {
        var item = GameManager.Instance.itemData;

        for (int i = 0; i < num; i++)
        {
            List<System.Action> availableItems = new List<System.Action>();

            if (item.foodN > 0) availableItems.Add(() => item.foodN -= Mathf.Min(1f, item.foodN));
            if (item.waterN > 0) availableItems.Add(() => item.waterN -= Mathf.Min(1f, item.waterN));
            if (item.diveKitN > 0) availableItems.Add(() => item.diveKitN--);
            if (item.gameKitN > 0) availableItems.Add(() => item.gameKitN--);
            if (item.medKitN > 0) availableItems.Add(() => item.medKitN--);
            if (item.bookN > 0) availableItems.Add(() => item.bookN--);
            if (item.mapN > 0) availableItems.Add(() => item.mapN--);
            if (item.researchN > 0) availableItems.Add(() => item.researchN--);
            if (item.batteryN > 0) availableItems.Add(() => item.batteryN--);
            if (item.medicineN > 0) availableItems.Add(() => item.medicineN--);

            if (availableItems.Count == 0)
            {
                Debug.LogWarning("[DestroyItem] 파괴할 수 있는 아이템이 없습니다.");
                break;
            }

            // 랜덤으로 하나 선택 후 실행
            int randIndex = Random.Range(0, availableItems.Count);
            availableItems[randIndex].Invoke();
        }
    }
}
