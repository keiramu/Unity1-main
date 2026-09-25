using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.AI;

//manage state transitions and state running << brain 

public class Brain : MonoBehaviour, IHealthUpdateReceiver
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
    public float visionAngle = 20;

    public NavMeshAgent agent;
    public CharacterShooting shooting;

    [Header("Event States")]
    public State hurtState;
    public State deathState;



    void Start()
    {
        Init();
    }


    void Init()
    {
        //find all states in an object
        gameObject.GetComponents(states);
        agent = GetComponent<NavMeshAgent>();
        shooting = GetComponent<CharacterShooting>();
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
        if (!newStateStarted)
        {
            newStateStarted = true;
            currentState.StartState();
        }
            currentState.UpdateState();
    }

    public void ChangeState(State state)
    {
        currentState = state;
        newStateStarted = false;
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

        Vector3 sightVector = (target.transform.position - eyePoint.position).normalized;

        bool didhit = Physics.SphereCast(eyePoint.position,
            eyeRadius,
            sightVector,
            out hit,
            distanceToTarget + 1,
            sightMask);
        float angle = Vector3.Angle(eyePoint.forward, sightVector);

        
        targetVisible = (didhit = true
            && angle <= visionAngle
            && hit.collider.gameObject == target);
    }

    void IHealthUpdateReceiver.Damage(int currentHealth, int maxhealth)
    {
        currentState.EndState();
        ChangeState(hurtState);
        
        
    }

    void IHealthUpdateReceiver.Healed(int currentHealth, int maxhealth)
    {
        throw new System.NotImplementedException();
    }

    void IHealthUpdateReceiver.Killed()
    {
        currentState.EndState();
        ChangeState(deathState);
    }
}
