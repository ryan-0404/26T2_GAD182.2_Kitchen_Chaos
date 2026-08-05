using UnityEngine;

public enum CherryPhase
{
    Waiting,
    MovingHorizontally,
    Falling,
    Respawning,
    Finished
}

public class CherryState : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private CherryPhase currentPhase =
        CherryPhase.Waiting;

    public CherryPhase CurrentPhase
    {
        get
        {
            return currentPhase;
        }
    }

    public bool CanMoveHorizontally
    {
        get
        {
            return currentPhase ==
                   CherryPhase.MovingHorizontally;
        }
    }

    public bool CanDrop
    {
        get
        {
            return currentPhase ==
                   CherryPhase.MovingHorizontally;
        }
    }

    public bool IsFalling
    {
        get
        {
            return currentPhase == CherryPhase.Falling;
        }
    }

    public bool IsFinished
    {
        get
        {
            return currentPhase == CherryPhase.Finished;
        }
    }

    public void SetPhase(CherryPhase newPhase)
    {
        currentPhase = newPhase;
    }
}