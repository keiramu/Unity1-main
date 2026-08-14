using UnityEngine;

public interface IHealthUpdateReceiver
{
    void Damage(int currentHealth, int maxhealth);
    void Healed(int currentHealth, int maxhealth);

    void Killed();
}
