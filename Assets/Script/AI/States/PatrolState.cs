using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class PatrolState : State
{
    [Header("Patrolling")]
    public List<Transform> patrolPoints = new List<Transform>();
    public float pointDetectionRange = 3;

    int currentPatrolPoint = 0;

    [Header("Transitions)")]
    public State alertState;

    public override void StartState()
    {
        base.StartState();
        currentPatrolPoint = GetNearestPatrolPoint();

        brain.agent.SetDestination(patrolPoints[currentPatrolPoint].position);
    }

    int GetNearestPatrolPoint()
    {
        int nearestPoint = -1;

        float nearestDistance = Mathf.Infinity;
        float currentDistance = nearestDistance;
        for (int i = 0;
            i < patrolPoints.Count;
            i++)
        {
            currentDistance = Vector3.Distance(transform.position, patrolPoints[i].position);
            if (currentDistance < nearestDistance)
            {
                nearestPoint = i;
                nearestDistance = currentDistance;
            }
        }
        return nearestPoint;
    
    }
    protected override void PrivateUpdate()
    {
        if(Vector3.Distance(transform.position, patrolPoints[currentPatrolPoint].position)
            < pointDetectionRange)
        {
            currentPatrolPoint++;

            if(currentPatrolPoint >= patrolPoints.Count)
            {
                currentPatrolPoint = 0;
            }
            brain.agent.SetDestination(patrolPoints[currentPatrolPoint].position);
        }
    }

    protected override bool CheckTransitions()
    {
        if(brain.targetVisible)
        {
            brain.ChangeState(alertState);
            return true;
        }
        return base.CheckTransitions();
    }

    public override void EndState()
    {
        base.EndState();
        currentPatrolPoint = -1;
        brain.agent.SetDestination(transform.position);

    }
}