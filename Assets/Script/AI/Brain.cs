using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
//manage state transitions and state running << brain 

public class Brain : MonoBehaviour
{
    [Header("state management")]
    public State startState;
    List<State> states = new List<State>();
    State currentState;

    bool newStateStarted = false;

    [Header("sensors")]
    public GameObject target;
    public Vector3 vectorToTarget;
    public float distanceToTarget = 0f;
    public bool targetVisible = false;
    public Transform eyePoint;
    public float eyeRadius = 0.05f;
    public LayerMask sightMask;

    void Start()
    {
        Init();
    }


    void Init()
    {
        //find all states in an object
        gameObject.GetComponents(states);

        foreach (State state in states)
        {
            state.ReceiveBrain(this);       
        }

        if (states.Contains (startState))
        {
            currentState = startState;
        }
        else
        {
            currentState = states[0];
        }
        currentState.StartState();
    }
    // Update is called once per frame
    void Update()
    {
        CheckSensors();
        currentState.UpdateState();
    }

    public void ChangeState(State state)
    {
        currentState = state;
        state.StartState();
    }
    void CheckSensors()
    {
        GetVectorAndDistanceToTarget();
        CheckTargetVisible();
    }

    void GetVectorAndDistanceToTarget()
    {
        Vector3 tempTargetVector = target.transform.position - transform.position;
        distanceToTarget = tempTargetVector.magnitude;
        vectorToTarget = tempTargetVector.normalized;
    }

    void CheckTargetVisible()
    {
        RaycastHit hit;
        bool didhit = Physics.SphereCast(eyePoint.position,
            eyeRadius,
            (target.transform.position - eyePoint.position).normalized,
            out hit,
            distanceToTarget + 1,
            sightMask);
        targetVisible = (didhit = true && hit.collider.gameObject == target);
    }
}
