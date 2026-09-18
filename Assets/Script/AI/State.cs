using UnityEngine;
//base class for finite state machine > doesnt do anuthing on its own but can be used as a parent class > For any states you wish to make. << state
public class State : MonoBehaviour
{
    [SerializeField]
    private bool hasStarted = false;
    protected Brain brain;
    protected float stateEndTime = 0f;

    public void ReceiveBrain(Brain brain)
    {
        this.brain = brain;
    }

    protected void SetStateEndTime(float endTime)
    {
        stateEndTime = Time.time + endTime;
    }
    public virtual void StartState()
    {
        hasStarted = true;
    }
    public void UpdateState()
    {
        bool didTransition = CheckTransitions();
        if (!didTransition)
        {
            PrivateUpdate();
        }
        else
        {
            EndState();
        }  
    }
    protected virtual void PrivateUpdate()
    {
        
    }

    protected virtual bool CheckTransitions()
    {
        return false;
    }

    public virtual void EndState()
    {
        hasStarted = false;
    }
}