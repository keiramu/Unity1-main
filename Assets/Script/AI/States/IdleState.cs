using UnityEngine;

public class IdleState : State
{
    public State nextState;
    public float transitionRange = 2;
    public override void StartState()
    {
        base.StartState();
        print("started idlign");
    }

    protected override void CheckTransitions()
    {
        if(brain.targetVisible && brain.distanceToTarget <= transitionRange)
        {
            EndState();
            brain.ChangeState(nextState);
        }    
    }
    public override void EndState()
    {
        base.EndState();
        print("stop idel");
    }

}
