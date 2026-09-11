using UnityEngine;

public class SurpriseState : State
{
    public override void StartState()
    {
        base.StartState();
        print("i am surprised");
    }
}
