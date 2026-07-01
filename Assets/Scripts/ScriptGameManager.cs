using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Run Data")]
    public int totalScore;
    public int lives = 3;
    public int currentMiniGameIndex;
    public int lastMiniGameScore;
    public string lastMiniGameName;

    [Header("Mini Game Scene Names")]
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
        lastMiniGameName = "";

        LoadNextMiniGame();
    }

    public void CompleteMiniGame(int scoreEarned, bool succeeded)
    {
        lastMiniGameScore = scoreEarned;
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

        string sceneToLoad = miniGameScenes[currentMiniGameIndex];
        lastMiniGameName = sceneToLoad;
        currentMiniGameIndex++;

        SceneManager.LoadScene(sceneToLoad);
    }

    public string GetNextMiniGameName()
    {
        if (currentMiniGameIndex >= miniGameScenes.Length)
        {
            return "Results";
        }

        return miniGameScenes[currentMiniGameIndex];
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}