using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    [Header("Panels")]
    public GameObject pausePanel;
    public GameObject settingsPanel;

    [Header("Gameplay Objects")]
    public GameObject gameplayAssetsParent;

    [Header("Audio")]
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    [Header("Optional Transition UI")]
    public TransitionUI transitionUI;

    private void Start()
    {
        Time.timeScale = 1f;

        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);

        if (gameplayAssetsParent != null)
        {
            gameplayAssetsParent.SetActive(true);
        }
    }

    public void OpenPauseMenu()
    {
        Time.timeScale = 0f;

        pausePanel.SetActive(true);
        settingsPanel.SetActive(false);

        if (gameplayAssetsParent != null)
        {
            gameplayAssetsParent.SetActive(false);
        }

        if (transitionUI != null)
        {
            transitionUI.PauseTransition();
        }
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);

        if (gameplayAssetsParent != null)
        {
            gameplayAssetsParent.SetActive(true);
        }

        if (transitionUI != null)
        {
            transitionUI.ResumeTransition();
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void OpenSettings()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void BackToPauseMenu()
    {
        settingsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;

        if (gameplayAssetsParent != null)
        {
            gameplayAssetsParent.SetActive(true);
        }

        if (transitionUI != null)
        {
            transitionUI.ReturnToMainMenu();
            return;
        }

        GameManager.Instance.ReturnToMainMenu();
    }
}