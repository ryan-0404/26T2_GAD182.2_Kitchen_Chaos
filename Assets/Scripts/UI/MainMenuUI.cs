using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public void PlayGame()
    {
        GameManager.Instance.StartNewRun();
    }

    public void OpenHighScores()
    {
        GameManager.Instance.OpenHighScores();
    }
}