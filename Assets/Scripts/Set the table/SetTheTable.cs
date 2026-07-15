using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SetTheTable : MonoBehaviour
{
    public MiniGameTimerScore miniGameTimerScore;
    public bool gameActive = false;
    public List<Transform> spawns = new List<Transform>();
    public List<GameObject> shadows = new List<GameObject>();
    public List<GameObject> prefab = new List<GameObject>();
    private int Y;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(StartGameAfterDelay());
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            SpawnItems();
        }
    }


    private IEnumerator StartGameAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        gameActive = true;
       int X = Random.Range(0, 4);
        SpawnItems();
        //GameObject firstShadow = Instantiate(shadows[X], spawns[0].position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
        //Instantiate(prefab[X], firstShadow.transform.position, firstShadow.transform.rotation * Quaternion.Euler(0f, 0f ,Random.Range(160f, 195f)));
    }

    private void SpawnItems()
    {
        int X = Random.Range(0, 4);
        GameObject Shadow = Instantiate(shadows[X], spawns[Y].position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
        Instantiate(prefab[X], Shadow.transform.position, Shadow.transform.rotation * Quaternion.Euler(0f, 0f, Random.Range(160f, 195f)));
        Shadow.name = "shadow" + X;
        Y++;
    }
}
