using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player Progress")]
    public int totalScore;
    public int lives = 3;
    public int currentMiniGameIndex;

    [Header("Last Mini Game Results")]
    public int lastMiniGameScore;
    public bool lastMiniGameSucceeded;

    [Header("Mini Game Scenes")]
    public string[] miniGameScenes =
    {
        "Knead",
        "Stamp",
        "EatTheCake",
        "SliceThePizza",
        "SetTheTable",
        "ShootTheSauce",
        "ShredTheReceipt",
        "DropTheCherry"
    };

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartNewRun()
    {
        totalScore = 0;
        lives = 3;
        currentMiniGameIndex = 0;
        lastMiniGameScore = 0;
        lastMiniGameSucceeded = false;

        LoadNextMiniGame();
    }

    public void CompleteMiniGame(int scoreEarned, bool succeeded)
    {
        lastMiniGameScore = scoreEarned;
        lastMiniGameSucceeded = succeeded;
        totalScore += scoreEarned;

        if (!succeeded)
        {
            lives--;
        }

        if (lives <= 0)
        {
            SceneManager.LoadScene("ResultsScreen");
            return;
        }

        SceneManager.LoadScene("TransitionScreen");
    }

    public void LoadNextMiniGame()
    {
        if (currentMiniGameIndex >= miniGameScenes.Length)
        {
            SceneManager.LoadScene("ResultsScreen");
            return;
        }

        SceneManager.LoadScene(miniGameScenes[currentMiniGameIndex]);
        currentMiniGameIndex++;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}