using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public void PlayGame()
    {
        GameManager.Instance.StartNewRun();
    }
}
   