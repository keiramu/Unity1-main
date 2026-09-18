using Unity.VisualScripting;
using UnityEngine;

public class ChaseState : State
{
    public float updateFrequency = 0.4f;

    public override void StartState()
    {
        base.StartState();
        Repath();
    }
    void Repath()
    {
        brain.agent.SetDestination(brain.target.transform.position);
        SetStateEndTime(updateFrequency);
    }

    protected override void PrivateUpdate()
    {
        if (Time.time > stateEndTime)
        {
            Repath();
        }
    }
}