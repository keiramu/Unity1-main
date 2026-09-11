using UnityEngine;
//base class for finite state machine > doesnt do anuthing on its own but can be used as a parent class > For any states you wish to make. << state
public class State : MonoBehaviour
{
    [SerializeField]
    private bool hasStarted = false;
    protected Brain brain;

    public void ReceiveBrain(Brain brain)
    {
        this.brain = brain;
    }

    public virtual void StartState()
    {
        hasStarted = true;
    }

    public virtual void UpdateState()
    {
        
    }

    protected virtual void CheckTransitions()
    {

    }

    public virtual void EndState()
    {
        hasStarted = false;
    }
}
