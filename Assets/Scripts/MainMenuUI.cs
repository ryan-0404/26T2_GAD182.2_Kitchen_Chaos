using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public void PlayGame()
    {
        GameManager.Instance.StartNewRun();
    }

    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }
}