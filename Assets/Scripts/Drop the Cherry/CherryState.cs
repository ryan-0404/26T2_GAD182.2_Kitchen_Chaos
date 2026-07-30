using UnityEngine;

public class CherryState : MonoBehaviour
{
    public enum State
    {
        Waiting,
        MovingHorizontally,
        Falling,
        Respawning,
        Finished
    }

    [Header("Debug")]
    [SerializeField] private State currentState = State.Waiting;

    public State CurrentState => currentState;

    public bool CanDrop =>
        currentState == State.MovingHorizontally;

    public bool IsFalling =>
        currentState == State.Falling;

    public bool HasFinished =>
        currentState == State.Finished;

    public void SetState(State newState)
    {
        currentState = newState;
    }
}j