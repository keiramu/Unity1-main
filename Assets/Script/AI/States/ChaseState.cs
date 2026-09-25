using Unity.VisualScripting;
using UnityEngine;

public class ChaseState : State
{
    public float updateFrequency = 0.4f;

    [Header("Transitions")]
    public State shootState;
    public float shootDistance = 10;



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

    protected override bool CheckTransitions()
    {
        if(brain.targetVisible 
            && brain.distanceToTarget <= shootDistance)
        {
            brain.ChangeState(shootState);
            return true;
        }
        return base.CheckTransitions();
    }

    public override void EndState()
    {
        brain.agent.SetDestination(transform.position);
    }
}