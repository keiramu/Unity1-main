using UnityEngine;
using UnityEngine.EventSystems;

public class HurtState : State
{
    public float recoveryTime = 0.5f;

    [Header("transitions")]
    public State chaseState;
    public float shootDistance = 10;
    public State shootState;


    public override void StartState()
    {
        base.StartState();
        SetStateEndTime(recoveryTime);
    }
    protected override bool CheckTransitions()
    {
        if (Time.time >= stateEndTime)
        {

            if (brain.targetVisible
                && brain.distanceToTarget <= shootDistance)
            {
                brain.ChangeState(shootState);
                return true;
            }
            else
            {
                brain.ChangeState(chaseState);
                return true;
            }
        }
        return base.CheckTransitions();
    }
}