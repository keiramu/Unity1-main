using UnityEngine;
using System.Collections.Generic;

public class characterHealth : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth = 100;

    public HealthBarControl healthBar;


    List<IHealthUpdateReceiver> updateReceivers = new List<IHealthUpdateReceiver>();

    private void Start()
    {
        GetComponents(updateReceivers);
        UpdateGui();
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0; //0
            print($"{gameObject.name} was killed");
            for (int i = 0; i < updateReceivers.Count;
               i++)
            {
                updateReceivers[i].Killed();
            }
        }
        else
        {
            for (int i = 0; i < updateReceivers.Count;
                i++)
            {
                updateReceivers[i].Damage(currentHealth, maxHealth);
            }
            //print($"{gameObject.name} took {damage} damage. {currentHealth}");
        }
        UpdateGui();

    }

    public bool Heal(int healing)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += healing;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            UpdateGui();
            return true;
        }
        else
        {
            return false;
        }
    }
    void UpdateGui()
    {
        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
        }
    }
}


