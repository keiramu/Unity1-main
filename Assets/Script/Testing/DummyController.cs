using Unity.VisualScripting;
using UnityEngine;

public class DummyController : MonoBehaviour, IHealthUpdateReceiver
{
    void IHealthUpdateReceiver.Damage(int currentHealth, int maxhealth)
    {
        print($"ow  { currentHealth}");
    }

    void IHealthUpdateReceiver.Healed(int currentHealth, int maxhealth)
    {
        
    }

    void IHealthUpdateReceiver.Killed()
    {
        Destroy(gameObject);
        
        
    }
}
