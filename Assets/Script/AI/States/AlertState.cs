using UnityEngine;

public class AlertState : State
{
    public float alertTime = 1;
    public State chaseState;

    public override void StartState()
    {
        base.StartState();
        SetStateEndTime(alertTime);
        print("alert");

    }

    protected override bool CheckTransitions()
    {
        if (Time.time > stateEndTime)
        {
            brain.ChangeState(chaseState);
            return true;
        }
        return base.CheckTransitions();
    }
}
