using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using System.Collections;

public class EatCake : MonoBehaviour
{
    [SerializeField]
    private int eatProgress = 0;
    private bool leftInput = false;
    private bool rightInput = false;
    [SerializeField]
    private bool gameActive=false;
    public MiniGameTimerScore  miniGameTimerScore;
    public GameObject right;
    public GameObject left;

    public Sprite[] cakes;
    public SpriteRenderer spriteRenderer;

    public AudioSource audioSource;
    public AudioClip sound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        left.SetActive(false);
        right.SetActive(false);
        StartCoroutine(StartGameAfterDelay());
    }

    // Update is called once per frame
    void Update()
    {
        // controls alternate between left and right arrow key
        if (Keyboard.current.leftArrowKey.wasReleasedThisFrame && leftInput == true && gameActive == true)
        {
            eatProgress = eatProgress + 1;
            leftInput = false;
            rightInput = true;
            audioSource.PlayOneShot(sound);
            //Debug.Log("input works");

        }

        if (Keyboard.current.rightArrowKey.wasReleasedThisFrame && rightInput == true && gameActive == true)
        {
            eatProgress = eatProgress + 1;
            leftInput = true;
            rightInput = false;
            audioSource.PlayOneShot(sound);
            //Debug.Log("input works");
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


        // end of minigame
        if (eatProgress >= 50 && gameActive == true)
        //if (eatProgress >= 5 && gameActive == true)  low score for testing
        {
            spriteRenderer.sprite = cakes[4];
            Debug.Log(" ate the cake");
            rightInput = false;
            leftInput = false;
            gameActive = false;
            miniGameTimerScore.CompleteTask();
        }

        if (leftInput == true)
        {
            left.SetActive(true);
        }
        else if (leftInput == false)
        {
            left.SetActive(false);
        }

        if (rightInput == true) 
        {
            right.SetActive(true);
        }
        else if (rightInput == false)
        {
            right.SetActive(false);
        }


    }
    private IEnumerator StartGameAfterDelay()
    {
        yield return new WaitForSeconds(1.5f);
        gameActive = true;
        leftInput = true;
    
    }


}
