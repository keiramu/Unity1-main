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

    protected override bool CheckTransitions()
    {
        if(brain.targetVisible && brain.distanceToTarget <= transitionRange)
        {
            brain.ChangeState(nextState);
            return true;
        }
        return base.CheckTransitions();
    }
    public override void EndState()
    {
        base.EndState();
        print("stop idel");
    }

}
