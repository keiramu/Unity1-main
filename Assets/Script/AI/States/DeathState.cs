using UnityEngine;

public class DeathState : State
{
    public override void StartState()
    {
        base.StartState();
        Destroy(gameObject);
    }
    //ww
}
