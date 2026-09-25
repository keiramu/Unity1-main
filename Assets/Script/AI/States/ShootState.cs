using UnityEditor.UI;
using UnityEngine;

public class ShootState : State
{
    public int minimumShotCount = 1;
    int shotCount = 0;
    public float shotTimer = 0.5f;

    public float trackingSpeed = 120;

    [Header("Transition")]
    public State chaseState;
    public float chaseDistance = 15f;

    public override void StartState()
    {
        base.StartState();
        SetStateEndTime(shotTimer);
    }


    protected override void PrivateUpdate()
    {
        if(Time.time >= stateEndTime)
        {
            //shoot
            brain.shooting.Shoot();
            shotCount += 1;
            SetStateEndTime(shotTimer);
        }
        else
        {
            //rotate towards target
        }
    }

    void RotateToTarget()
    {
        Vector3 horizontalDirToTarget = new Vector3(
            brain.vectorToTarget.x,
            0,
            brain.vectorToTarget.z);

        float angleToTarget = Vector3.SignedAngle(transform.forward,
            horizontalDirToTarget,
            Vector3.up);

        float nextRotation = trackingSpeed * angleToTarget * Time.deltaTime;

        if(Mathf.Abs(nextRotation) > Mathf.Abs(angleToTarget))
        {
            nextRotation = angleToTarget;
        }

        transform.Rotate(Vector3.up, nextRotation);
    }

    protected override bool CheckTransitions()
    {
        if(shotCount >= minimumShotCount
        
            && brain.distanceToTarget > chaseDistance
            || !brain.targetVisible)
        {
            brain.ChangeState(chaseState);
                return true;
        }

        return base.CheckTransitions();
    }

    public override void EndState()
    {
        base.EndState();
        shotCount = 0;
    }


}
