using UnityEngine;
using UnityEngine.UI;

public class CookieLivesUI : MonoBehaviour
{
    [Header("Cookie Images")]
    [SerializeField] private Image[] cookieImages;

    private void Start()
    {
        RefreshLives();
    }

    public void RefreshLives()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogWarning("CookieLivesUI could not find the GameManager.");
            return;
        }

        int currentLives = GameManager.Instance.lives;

        for (int i = 0; i < cookieImages.Length; i++)
        {
            if (cookieImages[i] != null)
            {
                cookieImages[i].gameObject.SetActive(i < currentLives);
            }
        }
    }
}