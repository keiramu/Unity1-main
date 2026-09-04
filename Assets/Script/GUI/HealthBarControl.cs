using UnityEngine;
using UnityEngine.UI;

public class HealthBarControl : MonoBehaviour
{
    public Image healthBarFill;


    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        float percentage = (float)currentHealth / (float)maxHealth;
        healthBarFill.fillAmount = percentage;
    }
}
