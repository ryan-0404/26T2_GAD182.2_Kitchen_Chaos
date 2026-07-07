using UnityEngine;

public class ExampleMiniGameButton : MonoBehaviour
{
    public MiniGameTimerScore miniGameTimerScore;

    public void CompleteMiniGame()
    {
        miniGameTimerScore.CompleteTask();
    }

    public void FailMiniGame()
    {
        miniGameTimerScore.FailTask();
    }
}