using UnityEngine;
using UnityEngine.InputSystem;

public class EatCake : MonoBehaviour
{
    [SerializeField]
    private int eatProgress = 0;

    private bool leftInput = false;
    private bool rightInput = false;

    [SerializeField]
    private bool gameActive = false;

    public MiniGameTimerScore miniGameTimerScore;

    public GameObject right;
    public GameObject left;

    public Sprite[] cakes;
    public SpriteRenderer spriteRenderer;

    public AudioSource audioSource;
    public AudioClip sound;

    public AudioSource winSource;
    public AudioClip winSound;

    void Start()
    {
        left.SetActive(false);
        right.SetActive(false);

        gameActive = false;
        leftInput = false;
        rightInput = false;
    }

    void Update()
    {
        if (gameActive == false &&
            miniGameTimerScore != null &&
            miniGameTimerScore.GameplayStarted == true)
        {
            gameActive = true;
            leftInput = true;
        }

        if (Keyboard.current.leftArrowKey.wasReleasedThisFrame &&
            leftInput == true &&
            gameActive == true)
        {
            eatProgress = eatProgress + 1;

            leftInput = false;
            rightInput = true;

            audioSource.PlayOneShot(sound);
        }

        if (Keyboard.current.rightArrowKey.wasReleasedThisFrame &&
            rightInput == true &&
            gameActive == true)
        {
            eatProgress = eatProgress + 1;

            leftInput = true;
            rightInput = false;

            audioSource.PlayOneShot(sound);
        }

        if (eatProgress == 10)
        {
            spriteRenderer.sprite = cakes[0];
        }
        else if (eatProgress == 20)
        {
            spriteRenderer.sprite = cakes[1];
        }
        else if (eatProgress == 30)
        {
            spriteRenderer.sprite = cakes[2];
        }
        else if (eatProgress == 40)
        {
            spriteRenderer.sprite = cakes[3];
        }

        if (eatProgress >= 50 &&
            gameActive == true)
        {
            spriteRenderer.sprite = cakes[4];

            winSource.PlayOneShot(winSound);

            Debug.Log("ate the cake");

            rightInput = false;
            leftInput = false;
            gameActive = false;

            miniGameTimerScore.CompleteTask();
        }

        if (leftInput == true &&
            gameActive == true)
        {
            left.SetActive(true);
        }
        else
        {
            left.SetActive(false);
        }

        if (rightInput == true &&
            gameActive == true)
        {
            right.SetActive(true);
        }
        else
        {
            right.SetActive(false);
        }
    }
}