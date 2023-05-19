using UnityEngine;
using UnityEngine.UI;

public class LootBar : MonoBehaviour
{
    [SerializeField] public Image lootBar;
    
    public void UpdateLootBar(float maxLootValue, float currentLootBarValue)
    {
        if (currentLootBarValue > maxLootValue)
            currentLootBarValue = maxLootValue;
        
        if (currentLootBarValue < 0)
            currentLootBarValue = 0;

        lootBar.fillAmount = currentLootBarValue / maxLootValue;
    }
}