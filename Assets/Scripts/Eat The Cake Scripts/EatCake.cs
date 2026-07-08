using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using System.Collections;

public class EatCake : MonoBehaviour
{
    [SerializeField]
    private int eatProgress = 0;
    private bool leftInput = true;
    [SerializeField]
    private bool gameActive=false;
    public MiniGameTimerScore  miniGameTimerScore;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
            Debug.Log("input works");

        }

        if (Keyboard.current.rightArrowKey.wasReleasedThisFrame && leftInput == false && gameActive == true)
        {
            eatProgress = eatProgress + 1;
            leftInput = true;
            Debug.Log("input works");
        }

        if (eatProgress == 25)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        }

        // end of minigame
        if (eatProgress >= 50 && gameActive == true)
        //if (eatProgress >= 5 && gameActive == true)  low score for testing
        {
            Debug.Log(" ate the cake");
            gameActive = false;
            miniGameTimerScore.CompleteTask();
        }



    }
    private IEnumerator StartGameAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        gameActive = true;

    }


}
