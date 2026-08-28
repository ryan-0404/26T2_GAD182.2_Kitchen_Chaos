using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SetTheTable : MonoBehaviour
{
    public MiniGameTimerScore miniGameTimerScore;

    public bool gameActive = false;

    public List<Transform> spawns =
        new List<Transform>();

    public List<GameObject> shadows =
        new List<GameObject>();

    public List<GameObject> prefab =
        new List<GameObject>();

    private int Y = 0;
    private int X = -1;

    public GameObject Item;
    public GameObject Shadow;
    public GameObject currentItem;
    public GameObject currentShadow;

    public AudioSource winSource;
    public AudioClip winSound;

    private int score = 0;

    private bool gameStarted = false;

    void Start()
    {
        gameActive = false;
        gameStarted = false;
    }

    void Update()
    {
        if (gameStarted == false &&
            miniGameTimerScore != null &&
            miniGameTimerScore.GameplayStarted == true)
        {
            gameStarted = true;
            gameActive = true;

            SpawnItems();
        }

        if (gameActive == false)
        {
            return;
        }

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            Rotate(15);
        }

        if (Keyboard.current.rightArrowKey.isPressed)
        {
            Rotate(-15);
        }

        if (Keyboard.current.upArrowKey.isPressed)
        {
            Move(2.7138062f);
        }

        if (Keyboard.current.downArrowKey.isPressed)
        {
            Move(-2.7138062f);
        }
    }

    private void Move(float amount)
    {
        Vector3 target =
            currentShadow.transform.position +
            new Vector3(
                0f,
                amount,
                0f
            );

        float speed = 4f;

        currentItem.transform.position =
            Vector3.MoveTowards(
                currentItem.transform.position,
                target,
                Time.deltaTime * speed
            );

        CheckPosition();
    }

    private void Rotate(float amount)
    {
        Quaternion targetRotation =
            Quaternion.Euler(
                0f,
                0f,
                amount
            );

        float smoothness = 15f;

        currentItem.transform.rotation *=
            Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                Time.deltaTime * smoothness
            );

        CheckPosition();
    }

    private void SpawnItems()
    {
        X = Random.Range(0, 4);

        Item = Instantiate(
            prefab[X],
            spawns[Y].position +
            new Vector3(
                0f,
                Random.Range(-2, 2),
                0f
            ),
            Quaternion.Euler(
                0f,
                0f,
                Random.Range(0f, 360f)
            )
        );

        Shadow = Instantiate(
            shadows[X],
            spawns[Y].position +
            new Vector3(
                0f,
                0f,
                1f
            ),
            Item.transform.rotation *
            Quaternion.Euler(
                0f,
                0f,
                Random.Range(160f, 195f)
            )
        );

        Item.name =
            "Item" + Y;

        Shadow.name =
            "Shadow" + Y;

        currentItem =
            GameObject.Find(
                "Item" + Y
            );

        currentShadow =
            GameObject.Find(
                "Shadow" + Y
            );
    }

    private void CheckPosition()
    {
        float angle =
            Vector3.Dot(
                currentItem.transform.up,
                currentShadow.transform.up
            );

        Vector2 space =
            currentItem.transform.position -
            currentShadow.transform.position;

        if (angle > 0.9999f &&
            space.y > -0.1f &&
            space.y < 0.1f)
        {
            score++;
            Y++;

            winSource.PlayOneShot(
                winSound
            );

            if (score == 3)
            {
                CheckWin();
            }
            else
            {
                SpawnItems();
            }
        }
    }

    private void CheckWin()
    {
        if (score == 3)
        {
            gameActive = false;

            Debug.Log("win");

            miniGameTimerScore.CompleteTask();
        }
    }
}