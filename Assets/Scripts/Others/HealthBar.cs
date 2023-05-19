using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] public Image healthBar;
    
    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        
        if (currentHealth < 0)
            currentHealth = 0;

        healthBar.fillAmount = currentHealth / maxHealth;
    }
}
