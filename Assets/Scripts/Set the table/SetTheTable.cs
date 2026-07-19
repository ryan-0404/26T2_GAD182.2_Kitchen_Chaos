using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SetTheTable : MonoBehaviour
{
    public MiniGameTimerScore miniGameTimerScore;
    public bool gameActive = false;
    public List<Transform> spawns = new List<Transform>();
    public List<GameObject> shadows = new List<GameObject>();
    public List<GameObject> prefab = new List<GameObject>();
    private int Y = 0;
    private int X = -1;

    public GameObject Item;
    public GameObject Shadow;
    public GameObject currentItem;
    public GameObject currentShadow;

    private int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(StartGameAfterDelay());
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.leftArrowKey.isPressed && gameActive == true)
        {
           // Debug.Log("left press");
            Rotate(10);
            //Quaternion targetRotation = Quaternion.Euler(0f, 0f, 10f);
            //float smoothness = 10f;
            //currentItem.transform.rotation *= Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothness);
        }
        if (Keyboard.current.rightArrowKey.isPressed && gameActive == true)
        {
           // Debug.Log("right press");
            Rotate(-10);
            //Quaternion targetRotation = Quaternion.Euler(0f, 0f, -10f);
            //float smoothness = 10f;
            //currentItem.transform.rotation *= Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothness);
        }

        if (Keyboard.current.upArrowKey.isPressed && gameActive == true)
        {
            Move(2.7138062f);
        }

        if (Keyboard.current.downArrowKey.isPressed && gameActive == true)
        {
            Move(-2.7138062f);
        }

    }

    private void Move(float amount)
    { 
        Vector3 target = currentShadow.transform.position + new Vector3(-0f, amount, 0f);
        float speed = 4f;
        currentItem.transform.position = Vector3.MoveTowards(currentItem.transform.position, target, Time.deltaTime*speed);
        CheckPosition();
    }

    private void Rotate(float amount)
    {

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, amount);
        float smoothness = 15f;
        currentItem.transform.rotation *= Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothness);

        CheckPosition();
    }


    private IEnumerator StartGameAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        gameActive = true;
        SpawnItems();
    }

    private void SpawnItems()
    {
        X = Random.Range(0, 4);
        Item = Instantiate(prefab[X], spawns[Y].position + new Vector3(0, Random.Range(-2, 2),0), Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
        Shadow = Instantiate(shadows[X], (spawns[Y].position + new Vector3( 0f,0f,1f)), Item.transform.rotation * Quaternion.Euler(0f, 0f, Random.Range(160f, 195f)));
        Item.name = "Item"+Y;
        Shadow.name = "Shadow"+Y;
        currentItem = GameObject.Find("Item"+Y);
        currentShadow = GameObject.Find ("Shadow"+Y);
    }


    private void CheckPosition()
    {
        //Debug.Log(Vector3.Dot(currentItem.transform.up,currentShadow.transform.up));
        float angle = Vector3.Dot(currentItem.transform.up, currentShadow.transform.up);
        Vector2 space = currentItem.transform.position - currentShadow.transform.position;
        //Debug.Log(space);
        //if (space.y > -0.1 && space.y < 0.1 )
        //{
        //    Debug.Log("close");
        //}
        if (angle > 0.99f && space.y > -0.1 && space.y < 0.1)
        {
                score++;
                Y++;
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
