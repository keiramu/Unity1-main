using UnityEngine;
using System.Collections.Generic;

public class characterHealth : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth = 100;


    List<IHealthUpdateReceiver> updateReceivers = new List<IHealthUpdateReceiver>();

    private void Start()
    {
        GetComponents(updateReceivers);
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            print($"{gameObject.name} was killed");
        }
        else
        {
            for (int i = 0; i < updateReceivers.Count;
                i ++ )
            {
                updateReceivers[i].Damage(currentHealth, maxHealth);
            }
            //print($"{gameObject.name} took {damage} damage. {currentHealth}");
        }

    }
}

